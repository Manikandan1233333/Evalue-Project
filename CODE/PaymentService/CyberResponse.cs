/*
 *Revision History
 * 1/29/03 JOM Version II.
 * 7/15/03 JOM Modified to use CSAAWeb.
 * 11/1/04 JOM Changed class name to CyberResponse.  This class will now be only used internally.
 * 11/1/04 JOM Removed .Error static method.  Moved to new constructor of PaymentClasses.CCResponse
*/
using System;
using CSAAWeb.AppLogger;
using CSAAWeb.Serializers;
using PaymentClasses;
using System.Xml.Serialization;
using CSAAWeb;
using System.Data.SqlClient;

namespace PaymentService
{
	/// <summary>
	/// CyberResponse class
	/// </summary>
	/// <remarks>
	/// Response class for payment authorizations.  
	/// Translates CyberSource-specific return codes to generic responses.
	/// Used for web service results and for persistence
	/// </remarks>
	public class CyberResponse
	{
		private string _sRequestID	= string.Empty;
		private string _sReturnCode = string.Empty;
		private string _sMessage	= string.Empty;
		private string _sActualMessage	= string.Empty;
		private string _sFlag		= string.Empty;
		private string _sAuthCode	= string.Empty;
		private decimal _Amount = 0;
		private int _ID = 0;
		private bool _IsComplete = false;
		private string _Trans_Ref_No = string.Empty;

		internal static CyberResponse New(string xml, ServiceRequest Request, int ID)
		{
			// Commented by Cognizant on 10 Jan 2005, for changing namespace from "Payments" to "PaymentService"
			//CyberResponse r = (CyberResponse) Serializer.FromXML(Type.GetType("Payments.CyberResponse"), xml);

			CyberResponse r = (CyberResponse) Serializer.FromXML(Type.GetType("PaymentService.CyberResponse"), xml);
			r.ID = ID;
			switch (Request.Operation) 
			{
				case ServiceOperation.Bill: 
				case ServiceOperation.Credit: 
				case ServiceOperation.Process: if (r.IsRequestSuccessful) r._IsComplete=true; break;
			}
			return r;
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public CyberResponse() {}

		internal CyberResponse(CyberSource.ICSReply csReply, ServiceRequest Request, int ID)
		{
			RequestID	= GetString(csReply, "request_id");
			ReturnCode = GetString(csReply, "ics_rcode");
			Message	= GetString(csReply, "ics_rmsg");
			ActualMessage	= GetString(csReply, "ics_rmsg");
			Flag		= GetString(csReply, "ics_rflag");
			AuthCode	= GetString(csReply, "auth_auth_code");
			switch (Request.Operation) 
			{
				case ServiceOperation.Auth: 
					_Amount = GetAmount(csReply, "auth_auth_amount"); 
					_Trans_Ref_No = GetString(csReply, "auth_trans_ref_no");
					if (!IsRequestSuccessful && !this.IsReauthCandidate) _IsComplete=true;
					break;
				case ServiceOperation.Bill: 
					_Amount = GetAmount(csReply, "bill_bill_amount"); 
					_Trans_Ref_No = GetString(csReply, "bill_trans_ref_no");
					if (IsRequestSuccessful) _IsComplete=true; 
					break;
				case ServiceOperation.Credit: 
					_Amount = GetAmount(csReply, "credit_credit_amount"); 
					_Trans_Ref_No = GetString(csReply, "credit_trans_ref_no");
					if (IsRequestSuccessful) _IsComplete=true; 
					break;
				case ServiceOperation.Process: 
					_Amount = GetAmount(csReply, "bill_bill_amount"); 
					_Trans_Ref_No = GetString(csReply, "bill_trans_ref_no");
					if (IsRequestSuccessful) _IsComplete=true; 
					break;
				case ServiceOperation.ReAuth: 
					_Amount = GetAmount(csReply, "auth_auth_amount"); 
					_Trans_Ref_No = GetString(csReply, "auth_trans_ref_no");
					break;
				case ServiceOperation.Reverse: 
					_Amount = GetAmount(csReply, "auth_reversal_amount"); 
					_Trans_Ref_No = "";
					break;
			}
			if (!IsRequestSuccessful) 
			{
				string msg = Config.Setting("EM_"+ Flag);
				if (msg.Length==0) msg = Config.Setting("EM_DEFAULT");
				if (msg.Length>0) Message = msg + "\r\n\r\n(Error Code: " + Flag + ")";
				ReturnCode=String.Empty;
			} 
			_ID = ID;
		}

		private decimal GetAmount(CyberSource.ICSReply csReply, string key)
		{
			string Value = csReply[key]; 
			if ((Value == null) || (Value.Length == 0)) return 0;
			if (Value[Value.Length-1]==0x0000) Value=Value.Substring(0,Value.Length-1);
			return Convert.ToDecimal(Value);
		}

		private string GetString(CyberSource.ICSReply csReply, string key)
		{
			string Value = csReply[key]; 
			if ((Value == null) || (Value.Length == 0)) return "";
			if (Value[Value.Length-1]==0x0000) Value=Value.Substring(0,Value.Length-1);
			return Value;
		}


		/// <summary>
		/// The id of the item in the data store.
		/// </summary>
		public int ID 
		{
			get {return _ID;}
			set {_ID = value;}
		}

		/// <summary>
		/// Request id (returned from payment gateway)
		/// </summary>
		/// <remarks>
		/// See CyberSource application programming guide for more info
		/// </remarks>
		public string RequestID 
		{
			get {return _sRequestID;}
			set {_sRequestID = value;}
		}

		/// <summary>
		/// return code from payment gateway
		/// </summary>
		/// <remarks>
		/// See CyberSource application programming guide for more info
		/// </remarks>
		public string ReturnCode 
		{
			get {return _sReturnCode;}
			set {_sReturnCode = value;}
		}

		/// <summary>
		/// translated response message 
		/// </summary>
		/// <remarks>
		/// Defined in web.config
		/// </remarks>
		public string Message 
		{
			get {return _sMessage;}
			set {_sMessage = value;}
		}

		/// <summary>
		/// response message from the payment gateway
		/// </summary>
		/// <remarks>
		/// Still see the CyberSource application programming guide for more info
		/// </remarks>
		public string ActualMessage 
		{
			get {return _sActualMessage;}
			set {_sActualMessage = value;}
		}

		/// <summary>
		/// response flag from payment gateway
		/// </summary>
		/// <remarks>
		/// Ditto re: seeing CyberSource programming guide
		/// </remarks>
		public string Flag 
		{
			get {return _sFlag;}
			set {_sFlag = value;}
		}

		/// <summary>
		/// Authorization code received from payment gateway
		/// </summary>
		public string AuthCode 
		{
			get {return _sAuthCode;}
			set {_sAuthCode = value;}
		}

		/// <summary>
		/// Authorized amount.
		/// </summary>
		public Decimal Amount 
		{
			get {return _Amount;}
			set {_Amount = value;}
		}

		/// <summary>
		/// TransactionReferenceNumber
		/// </summary>
		public string TransactionReferenceNumber
		{
			get {return _Trans_Ref_No;}
			set {_Trans_Ref_No=value;}
		}

		/// <summary>
		/// IsComplete
		/// </summary>
		[XmlIgnore]
		public bool IsComplete 
		{
			get {return _IsComplete;}
			set {}
		}
		/// <summary>
		/// convenience property to determine whether request was OK
		/// </summary>
		public bool IsAuthorized 
		{
			get {return this.Flag == "SOK";}
			set {}
		}

		/// <summary>
		/// convenience property to determine whether request was successfully processed
		/// </summary>
		public bool IsRequestSuccessful 
		{
			get {return this.ReturnCode == "1";}
			set {}
		}

		/// <summary>
		/// True if user data was valid
		/// </summary>
		/// <remarks>
		/// translates to "DINVALIDDATA" in CyberSource reply flag
		/// </remarks>
		public bool IsInvalidData 
		{
			get {return Flag == "DINVALIDDATA";}
			set {}
		}

		/// <summary>
		/// True if declined reason was invalid card.
		/// </summary>
		/// <remarks>
		/// translates to "DINVALIDCARD" in CyberSource reply flag
		/// </remarks>
		public bool IsInvalidCard 
		{
			get {return Flag == "DINVALIDCARD";}
			set {}
		}

		/// <summary>
		/// True if declined reason was AVS failure or CVCC failure.
		/// </summary>
		public bool IsSoftDecline 
		{
			get {return (AuthCode!="" && (Flag == "DCV" || Flag == "DAVSNO"));}
			set {}
		}

		/// <summary>
		/// This is the receipt number created by payment tool
		/// </summary>
		public string ReceiptNumber = "";

		/// <summary>
		/// IsReauthCandidate
		/// </summary>
		[XmlIgnore]
		public bool IsReauthCandidate 
		{
			get {return (IsSoftDecline || Flag == "DCARDEXPIRED" || Flag == "DCARDREFUSED" || Flag == "DINVALIDCARD");}
			set {}
		}

		/// <summary>
		/// To convert as string
		/// </summary>
		/// <returns></returns>
		public override string ToString()
		{
			return Serializer.ToXML(this);
		}
	}
}
