/*
 *	REVISION HISTORY:
 *	07/29/2005		-	MODIFIED BY COGNIZANT:
 *	CSR#3937.Ch1	-	Modified for handling the failure Credit card transaction 
 *						(resubmission of failed transactions)
 */ 

using System;
using System.Web;
using System.Web.Services;
using System.Data;
using System.Collections;
using System.ComponentModel;
using PaymentClasses;
using CSAAWeb;
using CSAAWeb.Web;
using CSAAWeb.AppLogger;

namespace PaymentService
{
	/// <summary>
	/// BasePaymentService contains methods that perform the basic payment processing functions.
	/// Most of these functions are not exposed as web service methods, but are used by other methods
	/// that do expose them.
	/// </summary>
	public class BasePaymentService : System.Web.Services.WebService, CSAAWeb.Web.IClosableWeb {

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if(disposing && components != null) {
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion
		// Exception messages
		const string EXP_MISSING_MERCHANT = "Missing Merchant MerchantRefNum number";

		private static ArrayOfLineItem Reauth_Items = new ArrayOfLineItem();
		private static ArrayList Cards_Reversible;
		private static DataTable CreditCards;
		///<summary></summary>
		protected DataConnection Data = null;
		/// <summary>
		/// static constructor
		/// </summary>
		static BasePaymentService() {
			Reauth_Items.Add(new LineItem(Config.Setting("PAYMENT_REAUTH_LINE_ITEM")));
			DataConnection Data = new DataConnection();
			Data.GetApplications(CardInfo.Applications);
			CreditCards = Data.GetCardTypes(false).Tables["CREDITCARD_TABLE"];
			Data.Close();
			Cards_Reversible = Config.SettingArray("PAYMENT_CARDS_REVERSABLE");
		}

		/// <summary>
		/// The default no arg constructor
		/// </summary>
		public BasePaymentService() {
			Data = new DataConnection();
			//CODEGEN: This call is required by the ASP.NET Web Services Designer
			InitializeComponent();
			CSAAWeb.Web.ClosableModule.SetHandler(this);
		}

		/// <summary>
		/// Closes the connection and rollsback any open transaction.
		/// </summary>
		public void Close(object Source, EventArgs e) {
			Close();
		}

		/// <summary>
		/// Cleans up card file.
		/// </summary>
		public virtual void Close() {
			if (Data!=null) Data.Close();
			CardFile.CleanUp();
			CSAAWeb.Web.ClosableModule.RemoveHandler(this);
		}


		/// <summary>
		/// Actual code for processing credit card payments.
		/// </summary>
		/// <param name="Payment">The payment to process.</param>
		/// <returns>CCResponse instance with result data.</returns>
        protected CCResponse DoRequest(Payment Payment)
        {
            try
            {
                string RequestID = string.Empty;
                CheckAppAllowed(Payment.Application);
                if (IsMissing(Payment.Reference)) throw new Exception(EXP_MISSING_MERCHANT);
                CyberResponse Response = null;
                if (Payment.Operation == ServiceOperation.Bill || Payment.Operation == ServiceOperation.Reverse)
                {
                    Response = Data.GetTransactionDetails(Payment);
                    RequestID = Response.RequestID;
                }
                else Payment.Card.ValidateFields(CreditCards);
                Payment.BillTo.ValidateFields();
                CheckEmail(Payment.Application, Payment.BillTo);
                if (!IsMissing(RequestID)) Payment.RequestID = RequestID;
                ServiceRequest s = new ServiceRequest(Payment);
                Payment.Function = s.Function;
                if (Response != null) s.CheckAuthErrors(Response);
                Response = Data.Begin_Transaction(Payment);
                string ReceiptNumber = Response.ReceiptNumber;
                if (Response.Flag != "New") throw s.Ex("Attempted duplicate transaction.");
                //Response = s.Send(Response.ID);
                CCResponse Result = new CCResponse(Response);
                Data.Complete_Transaction(Payment, Result);
                Result.ReceiptNumber = ReceiptNumber;
                return Result;
            }
            catch (Exception e) { return new CCResponse(e); }
        }

		/// <summary>
		/// Attempts to re-authorize the payment by the most appropriate method.
		/// </summary>
		/// <param name="Payment">The payment to re-authorize</param>
		protected CCResponse DoAuth(Payment Payment) {
			// Modified by cognizant 2/7/2005 to validate amount in star transaction.
			foreach (LineItem I in Payment.LineItems) 
			{
				if(I.Amount <0)
				{
					return new CCResponse(new BusinessRuleException("Amount should be positive"));
				}
			
			}
			if(Payment.Amount <0)
			{
				return new CCResponse(new BusinessRuleException("Total Amount should be positive"));
			}

			Payment.BillTo.ValidateFields();
			CheckEmail(Payment.Application, Payment.BillTo);
			/*
			 * CSR#3937.Ch1 - START : Modified as part of CSR#3937
			 * To fix the error, which occurs when we try to process a failed Credit Card transaction again
			 * Moved the try statement from the line
			 * if (Card.CCType!=string.Empty && Cards_Reversible.Contains.........   to the line
			 * if (!Data.CheckReAuth(Payment))..........
			 */
			try 
			{
				//Check if payment is a candidate for reauth, if not just do auth.
				if (!Data.CheckReAuth(Payment)) return DoRequest(Payment);
				CardInfo Card = Payment.Card;
				Payment.Card=null;
				
				//Make use of Reverse_Auth where permitted.
				if (Card.CCType!=string.Empty && Cards_Reversible.Contains(Card.CCType) && ServiceRequest.Permits(ServiceOperation.Reverse, Payment.Application)) {
					Payment.Operation=ServiceOperation.Reverse;
					CCResponse Response =  DoRequest(Payment);
					if (Response.IsRequestSuccessful) {
						Payment.Card=Card;
						Payment.Operation=ServiceOperation.Auth;
						return DoRequest(Payment);
					} else return Response;
				} 
				else {
					Payment.Card=Card;
					Payment.Operation=ServiceOperation.ReAuth;
					Payment.LineItems=Reauth_Items;
					return DoRequest(Payment);
				}
			}
			//CSR#3937.Ch1 - END : Handling the failed credit card transactions(resubmission)
			catch (Exception e) {return new CCResponse(e);}
		}

		#region Private methods
		/// <summary>
		/// Applications that do not require an email address may use this as default
		/// </summary>
		/// <param name="appid">(string) The calling application.</param>
		[WebMethod(Description="Applications that do not require an email address may use this as default") ]
		private string GetDefaultEmail(string appid) {
			// not checking application id, return same dummy email address for everyone
			return Config.Setting("PAYMENT_DEFAULT_EMAIL");

		}

		/// <summary>
		/// Convenience function, checks for missing values (blanks or nulls)
		/// </summary>
		/// <param name="s"></param>
		/// <returns>True if s is blank or null</returns>
		private bool IsMissing(String s) {
			return ((s == null) || (s.Length == 0));
		}

		/// <summary>
		/// Check against list of apps which are allowed to use this service.  Log
		/// event and throw exception if not.
		/// </summary>
		/// <param name="Application">The ID of the calling application</param>
		private void CheckAppAllowed(string Application) {
			if (!CardInfo.Applications.Contains(Application)) {
				Logger.Log("application id " + Application + " not allowed");
				throw new Exception("Application not allowed to use this service.");
			}
		}

		/// <summary>
		/// Checks to see if an email address is supplied, fills in application's default if not
		/// </summary>
		/// <param name="Application"></param>
		/// <param name="BillTo"></param>
		private void CheckEmail(string Application, BillToInfo BillTo) {
			// using a dummy email address. Make a note of it in case it's interesting to someone
			if (IsMissing(BillTo.Email)) {
				//Logger.Log("Using default email for " + BillTo.FirstName + " " + BillTo.LastName + " order.");
				BillTo.Email = this.GetDefaultEmail(Application);
			}
		}
		#endregion
	}
}
