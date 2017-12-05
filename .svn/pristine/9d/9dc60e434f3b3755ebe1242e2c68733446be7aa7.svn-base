/*	REVISION HISTORY
 *	MODIFIED BY COGNIZANT
 *	07/19/2005 - For changing the web.config entries pointing to database. This changes 
 *				 are made as part of CSR #3937 implementation.
 *	CHANGES DONE:
 *	CSR#3937.Ch1 : Include the namespace for web.config changes for using StringDictionary and Dataset
 *	CSR#3937.Ch2 : Added the constructor for calling the method to populate the constants(web.config)
 *	CSR#3937.Ch3 : Added a new method for populating the constants data into Application object (for STAR)
 */ 
using System;
using System.Data;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.Xml.Schema;
using System.Configuration;
using System.Collections;
using System.Reflection;
using PaymentClasses;
using PaymentClasses.Service;
using CSAAWeb;
using CSAAWeb.Serializers;
using CSAAWeb.AppLogger;
using AuthenticationClasses;
using AuthenticationClasses.Service;

//CSR#3937.Ch1 : START - Include the namespace for web.config changes for using StringDictionary and Dataset
using System.Data.SqlClient;
using System.Collections.Specialized;
using InsuranceClasses.Service;
//CSR#3937.Ch1 : END - Include the namespace for web.config changes for using StringDictionary and Dataset

namespace PaymentService
{
	/// <summary>
	/// This web service contains the basic payment processing functions.  It does not contain any 
	/// methods that return datasets, as those are .NET specific.  Methods that return datasets should
	/// be provided through another service that inherits this one.
	/// </summary>
	[WebService(Namespace="http://csaa.com/webservices/payment/")]
	public class PaymentGateway : BasePaymentService 
	{
		/// <summary>
		/// Constructor for PaymentGateway class
		/// </summary>
		//CSR#3937.Ch2 : START - Added the constructor for calling the method to populate the constants(web.config)
		public PaymentGateway()
		{
			//CSAAWeb.Web.ClosableModule.SetHandler(this);
			if(Application["Constants"] == null) _GetConstants();
		}
		//CSR#3937.Ch2 : END - Added the constructor for calling the method to populate the constants(web.config)


		/// <summary>
		/// Authorizes and bills (collects funds) from the card.
		/// </summary>
		/// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
		/// <param name="MerchantRefNum">The unique id by which to reference this transaction.</param>
		/// <param name="AppId">(string) The name (code) of the application calling the method.</param>
		/// <param name="BillTo">(BillTo) The customer's bill-to address.</param>
		/// <param name="Card">(CardInfo) The customer's credit card information.</param>
		/// <param name="Items">(ArrayOfLineItem) An array of the items being purchased.</param>
		[WebMethod(Description="")]
		public CCResponse Process(UserInfo User, string AppId, string MerchantRefNum, CardInfo Card, BillToInfo BillTo, ArrayOfLineItem Items) {
			return DoRequest(new Payment(ServiceOperation.Process, AppId, MerchantRefNum, "", "", Card, BillTo, Items, User));
		}

		/// <summary>
		/// Attempts to re-authorize the payment by the most appropriate method.
		/// </summary>
		/// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
		/// <param name="MerchantRefNum">The unique id by which to reference this transaction.</param>
		/// <param name="AppId">(string) The name (code) of the application calling the method.</param>
		/// <param name="BillTo">(BillTo) The customer's bill-to address.</param>
		/// <param name="Card">(CardInfo) The customer's credit card information.</param>
		[WebMethod(Description="Send ICS_Auth request to CyberSource.")]
		public CCResponse ReAuth(UserInfo User, string AppId, string MerchantRefNum, CardInfo Card, BillToInfo BillTo) {
			return Auth(User, AppId, MerchantRefNum, Card, BillTo, null);
		}
		/// <summary>
		/// Authorize the payment on the customer's card.
		/// </summary>
		/// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
		/// <param name="MerchantRefNum">The unique id by which to reference this transaction.</param>
		/// <param name="AppId">(string) The name (code) of the application calling the method.</param>
		/// <param name="BillTo">(BillTo) The customer's bill-to address.</param>
		/// <param name="Card">(CardInfo) The customer's credit card information.</param>
		/// <param name="Items">(ArrayOfLineItem) An array of the items being purchased.</param>
		[WebMethod(Description="Send ICS_Auth request to CyberSource.")]
		public CCResponse Auth(UserInfo User, string AppId, string MerchantRefNum, CardInfo Card, BillToInfo BillTo, ArrayOfLineItem Items) {
			return DoAuth(new Payment(ServiceOperation.Auth, AppId, MerchantRefNum, "","", Card, BillTo, Items, User));
		}

		/// <summary>
		/// Bill (collect funds) that we previously authorized.
		/// </summary>
		/// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
		/// <param name="MerchantRefNum">The unique id by which to reference this transaction.</param>
		/// <param name="AppId">(string) The name (code) of the application calling the method.</param>
		[WebMethod(Description="Send ISC_Bill request to Cybersource.")]
		public CCResponse Bill(UserInfo User, string MerchantRefNum, string AppId) {
			return DoRequest(new Payment(ServiceOperation.Bill, AppId, MerchantRefNum, "", "",null, null, null, User));
		}

		/// <summary>
		/// Bill (collect funds) that were previously denied with a DCALL code 
		/// based on an authorization obtained over the telephone.
		/// </summary>
		/// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
		/// <param name="MerchantRefNum">The unique id by which to reference this transaction.</param>
		/// <param name="AppId">(string) The name (code) of the application calling the method.</param>
		/// <param name="AuthCode">(string) The authorization code obtained on the telephone.</param>
		[WebMethod(Description="Send ISC_Bill request to Cybersource with verbal auth code.")]
		public CCResponse BillVerbalAuth(UserInfo User, string AppId, string MerchantRefNum, string AuthCode) {
			return DoRequest(new Payment(ServiceOperation.Bill, AppId, MerchantRefNum, AuthCode, "", null, null, null, User));
		}

		/// <summary>
		/// Refund an order.
		/// </summary>
		/// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
		/// <param name="MerchantRefNum">The unique id by which to reference this transaction.</param>
		/// <param name="AppId">(string) The name (code) of the application calling the method.</param>
		/// <param name="BillTo">(BillTo) The customer's bill-to address.</param>
		/// <param name="Card">(CardInfo) The customer's credit card information.</param>
		/// <param name="Items">(ArrayOfLineItem) An array of the items being purchased.</param>
		[WebMethod(Description="Send ICS_Credit request to CyberSource.")]
		public CCResponse Credit(UserInfo User, string AppId, string MerchantRefNum, CardInfo Card, BillToInfo BillTo, ArrayOfLineItem Items) {
			return DoRequest(new Payment(ServiceOperation.Credit, AppId, MerchantRefNum, "", "", Card, BillTo, Items, User));
		}

		/// <summary>
		/// Record a payment other than on-line credit card.
		/// </summary>
		/// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
		/// <param name="AppId">(string) The name (code) of the application calling the method.</param>
		/// <param name="PaymentType">(PaymentTypes) The type of payment.</param>
		/// <param name="Items">(ArrayOfLineItem) An array of the items being purchased.</param>
		/// <param name="ReceiptNumber">(string) The receipt number for this payment.</param>
		[WebMethod(Description="Record a payment other than on-line credit card.")]
		public CCResponse RecordPayment(UserInfo User, string AppId, PaymentTypes PaymentType, string ReceiptNumber, ArrayOfLineItem Items) {
			return new CCResponse(Data.Begin_Transaction(new Payment(PaymentType, AppId, ReceiptNumber, Items, User)));
		}

		/// <summary>
		/// Update the account number on the lines for the payment.  Only one of receipt #
		/// or reference number need be supplied.
		/// </summary>
		/// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
		/// <param name="AppId">(string) The name (code) of the application calling the method.</param>
		/// <param name="PaymentType">(PaymentTypes) The type of payment.</param>
		/// <param name="Items">(ArrayOfLineItem) An array of the items being purchased.</param>
		/// <param name="ReceiptNumber">(string) The receipt number for this payment.</param>
		/// <param name="Reference">Merchant reference number</param>
		[WebMethod(Description="Update the account number on the lines for the payment.")]
		public void UpdateLines(UserInfo User, string AppId, PaymentTypes PaymentType, string ReceiptNumber, string Reference, ArrayOfLineItem Items) {
			Payment P = new Payment(PaymentType, AppId, ReceiptNumber, Items, User);
			P.Reference=Reference;
			Data.UpdateLines(P);
		}

		/// <summary>
		/// Method for populating the constants into the Application object(for STAR application)
		/// </summary>
		//CSR#3937.Ch3 : START - Added a new method for populating the constants data into Application object (for STAR)
		private void _GetConstants()
		{

			DataSet dsConstants = new Insurance().PayGetConstants();
			StringDictionary sdConstants = new StringDictionary();

			foreach (DataRow dr in dsConstants.Tables[0].Rows)
			{
				sdConstants.Add(dr[0].ToString(), dr[1].ToString());
			}

			Application.Add("Constants", sdConstants);

		}
		//CSR#3937.Ch3 : END - Added a new method for populating the constants data into Application object (for STAR)


	}
}
