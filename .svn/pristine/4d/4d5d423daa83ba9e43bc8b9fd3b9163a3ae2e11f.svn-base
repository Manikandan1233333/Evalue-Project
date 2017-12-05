//Revision History
//
//3/11/03 JOM modified to set ignore_avs flag if BillTo.IgnoreAVS is true when
//		  doing Auth as well as Process.
//10/29/04 JOM added CopyTo(IDbCommand) method to allow this type to participate in
//		  automatic copy to data objects.  Moved ServiceOperation enum to PaymentClasses.
//		  Modified reference to CCResponse class to CyberResponse.
//5/27/05 AZL Added conditional logic in VerbalAuthorization function to prevent
//				auth_type from being set if auth_code does not contain a value
using System;
using System.Collections;
using PaymentClasses;
using CSAAWeb;
using AuthenticationClasses;

namespace PaymentService
{
	/// <summary>
	/// Encapsulates functionality for talking to Cybersource
	/// </summary>
	internal class ServiceRequest: CyberSource.ICSRequest
	{
		const string EXP_ICS_FUNCTION_NOT_ALLOWED = "Requested payment function not allowed currently.";
		private CyberSource.ICSClient csClient=null;
		private BillToInfo _BillTo=null;
		private CardInfo _Card=null;
		private ArrayOfLineItem _LineItems=null;
		private ArrayList PermittedFunctions = null;
		private ServiceOperation _Operation=ServiceOperation.NoOp;

		/// <summary>
		/// Returns a string representation of the enum value
		/// </summary>
		private static string TranslateOperation(ServiceOperation Operation) 
		{
			switch (Operation) 
			{
				case ServiceOperation.Auth: return "ics_auth";
				case ServiceOperation.Bill: return "ics_bill";
				case ServiceOperation.Credit: return "ics_credit";
				case ServiceOperation.Process: return "ics_auth, ics_bill";
				case ServiceOperation.ReAuth: return "ics_auth";
				case ServiceOperation.Reverse: return "ics_auth_reversal";
				default: return "";
			}
		}

		/// <summary>
		/// Returns true if the enum value is in the permitted list.
		/// </summary>
		/// <param name="op">The operation being checked.</param>
		/// <param name="Application">The calling application</param>
		/// <returns></returns>
		public static bool Permits(ServiceOperation op, string Application)
		{
			return CardInfo.Applications[Application].PermittedFunctions.Contains(TranslateOperation(op));
		}

		/// <summary>
		/// Initializes a new ServiceRequest from Payment
		/// </summary>
		/// <param name="Payment">The payment to create a service request from</param>
		public ServiceRequest(Payment Payment) {
			PermittedFunctions = CardInfo.Applications[Payment.Application].PermittedFunctions;
			Payment.CopyTo(this);
		}

		/// <summary>
		/// The application making the request.
		/// </summary>
		public string Application="";
		/// <summary>
		/// The user making the request.
		/// </summary>
		public UserInfo User = null;

		/// <summary>
		/// The customer's billing address
		/// </summary>
		public BillToInfo BillTo
		{
			get {return _BillTo;}
			set 
			{
				_BillTo = value;
				// contact information
				this["customer_firstname"]	= value.FirstName;
				this["customer_lastname"]	= value.LastName;
				this["customer_email"]		= value.Email;
				this["customer_phone"]		= value.Phone;

				// billing address
				if ((this.Operation==ServiceOperation.Process || this.Operation==ServiceOperation.Auth)&& value.IgnoreAVS) this["ignore_avs"]="yes";
				this["bill_address1"]		= value.Address1;
				if (!IsMissing(value.Address2))
					this["bill_address2"]	= value.Address2;
				this["bill_city"]			= value.City;
				this["bill_state"]			= value.State;
				this["bill_zip"]			= value.Zip;
				this["bill_country"]		= value.Country;
				// Misc
				this["currency"]			= value.Currency;
			}
		}

		/// <summary>
		/// Gets or sets the customer's credit card information.
		/// </summary>
		public CardInfo Card
		{
			get {return _Card;}
			set
			{
				_Card = value;
				if (value.CCNumber!="") 
				{
					this["customer_cc_number"]	= value.CCNumber;
					this["customer_cc_expmo"]	= value.CCExpMonth;
					this["customer_cc_expyr"]	= value.CCExpYear;
					this["customer_cc_cv_number"]= value.CCCVNumber;
					if (this.Operation==ServiceOperation.Process && value.IgnoreCCCV) this["ignore_bad_cv"]="yes";
				}
			}
		}

		/// <summary>
		/// Gets or sets the VerbalAuthorization code.
		/// </summary>
		public string VerbalAuthorization
		{
			// MIVR - CyberSource fix to prevent DINVALIDDATA error
			set 
			{
				if (value != "") 
				{
					this["auth_code"] = value;
					this["auth_type"]="verbal";
				}
			}
			get {return (this["auth_type"]=="verbal")?this["auth_code"]:"";}
		}

		/// <summary>
		/// Gets or sets the Cybersource RequestID
		/// </summary>
		public string RequestID
		{
			set {this["auth_request_id"]= value;}
			get {return this["auth_request_id"];}
		}

		/// <summary>
		/// Gets or sets the merchant reference number.
		/// </summary>
		public string Reference
		{
			get {return this["merchant_ref_number"];}
			set	{this["merchant_ref_number"]=value;}
		}

		/// <summary>
		/// Gets the cybersource functions request.
		/// </summary>
		public string Function
		{
			get {return this["ics_applications"];}
			set {}
		}

		/// <summary>
		/// Returns true if the function is a new request type (Auth, Credit, Process)
		/// </summary>
		public bool isNew
		{
			get {return (Operation==ServiceOperation.Auth || Operation==ServiceOperation.Credit || Operation==ServiceOperation.Process);}
		}

		/// <summary>
		/// Gets or sets the operation type.
		/// </summary>
		public ServiceOperation Operation
		{
			get {return _Operation;}
			set 
			{
				_Operation = value;
				this["ics_applications"] = TranslateOperation(value);
				CheckICSPermission();
			}
		}

		/// <summary>
		/// Gets or sets the items begin purchased.
		/// </summary>
		public ArrayOfLineItem LineItems
		{
			get {return _LineItems;}
			set 
			{
				_LineItems = value;
				int n = 0;
				foreach (LineItem item in value) 
					SetOffer(n++, new ServiceLineItem(item));
			}
		}

		/// <summary>
		/// Throws and exception if the function isn't permitted.
		/// </summary>
		private void CheckICSPermission() 
		{
			string [] req = Function.ToLower().Replace(" ","").Split(',');
			foreach (string st in req) 
				if (!PermittedFunctions.Contains(st)) throw Ex(EXP_ICS_FUNCTION_NOT_ALLOWED + "(" + st + ")");
		}

		/// <summary>
		/// Builds an ICSClient object and initializes per config settings
		/// </summary>
		private void InitClient() 
		{
			csClient = new CyberSource.ICSClient();

			// get config settings from web.config
			csClient.MerchantId		= CardInfo.Applications[Application].MerchantID;
			csClient.ServerHost		= Config.Setting("csClient.ServerHost");
			csClient.LogLevel		= Config.Setting("csClient.LogLevel");
			csClient.LogFile		= Config.ExpandedSetting("csClient.LogFile");
			csClient.LogMaxSize		= Config.iSetting("csClient.LogMaxSize");
			csClient.KeysDir		= Config.Setting("csClient.KeysDir");
			csClient.RetryEnabled	= Config.Setting("csClient.RetryEnabled");
			csClient.RetryStart		= Config.iSetting("csClient.RetryStart");
			csClient.HTTPProxyPassword = Config.Setting("csClient.HTTPProxyPassword");
			csClient.HTTPProxyURL	= Config.Setting("csClient.HTTPProxyURL");
			csClient.HTTPProxyUsername = Config.Setting("csClient.HTTPProxyUsername");
			csClient.ServerId		= Config.Setting("csClient.ServerId");
			csClient.ServerPort		= Config.iSetting("csClient.ServerPort");
		}

		/// <summary>
		/// Checks to see if there were authorization errors, throws exceptions unders certain conditions.
		/// </summary>
		/// <param name="Response">The Cybersource response.</param>
		public void CheckAuthErrors(CyberResponse Response) 
		{
			if (!Response.IsRequestSuccessful) 
			{
				if (!Response.IsSoftDecline)
					throw Ex("Attempt to proceed with transaction that was declined.");
				if (Response.Flag=="DCV" && !Card.IgnoreCCCV)
					throw Ex("Authorization failed with CC_CV error, but request requires CC_CV verification.");
				if (Response.Flag=="DAVSNO" && !BillTo.IgnoreAVS)
					throw Ex("Authorization failed with AVS error, but request requires AVS verification.");
			}
		}

		/// <summary>
		/// Creates an exception based on the message.
		/// </summary>
		/// <param name="msg">Message to create exception for.</param>
		/// <returns>(Exception)</returns>
		public Exception Ex(string msg) 
		{
			msg += "\r\n   Reference: " + this.Reference + ". Function: " + this.Function + ".";
			return new Exception(msg);
		}

		/// <summary>
		/// Convenience function, checks for missing values (blanks or null)
		/// </summary>
		protected bool IsMissing(String s) 
		{
			return ((s == null) || (s.Length == 0));
		}

		/// <summary>
		/// Copies the values of fields to the matching parameters of a db command
		/// object representing a stored procedure with named parameters.
		/// </summary>
		/// <param name="Cmd">The database command object who's parameters will be set.</param>
		/// <remarks>
		/// This is a new method that allows this type to behave as if it were a SimpleSerializer
		/// with respect to Data Command objects.
		/// </remarks>
		public void CopyTo(System.Data.IDbCommand Cmd) {
			CSAAWeb.Serializers.Serializer.CopyTo(this, Cmd, true);
		}

	}

	
	/// <summary>
	/// Encapsulates and allows for easy creation of Cybersource offer lines.
	/// </summary>
	internal class ServiceLineItem : CyberSource.ICSOffer
	{
		/// <summary>
		/// Default no value constructor
		/// </summary>
		public ServiceLineItem()
		{}

		/// <summary>
		/// Constructor with the actual line item data
		/// </summary>
		/// <param name="item">Data to pass</param>
		public ServiceLineItem(LineItem item)
		{
			Clear();
			Item = item;
		}

		/// <summary>
		/// Sets the line item
		/// </summary>
		public LineItem Item
		{
			set 
			{
				this["product_name"] = value.ProductName;
				this["merchant_product_sku"] = value.SKU;
				this["product_code"] = value.ProductCode;
				this["amount"] = Convert.ToString(value.Amount);
				if (value.Tax_Amount>0)
					this["tax_amount"] = Convert.ToString(value.Tax_Amount);
				this["quantity"] = Convert.ToString(value.Quantity);
			}
		}
	}
}
