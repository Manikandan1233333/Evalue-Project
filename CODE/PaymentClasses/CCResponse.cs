using System;
using System.Xml.Serialization;
using System.ComponentModel;
using System.Data.SqlClient;
using CSAAWeb;
using CSAAWeb.AppLogger;

namespace PaymentClasses
{
	/// <summary>
	/// Message class for response from the Payment processor.
	/// </summary>
	public class CCResponse : CSAAWeb.Serializers.SimpleSerializer {

		///<summary>Initializes a new instance of CCResponse with default properties.</summary>
		public CCResponse() : base () {}
		///<summary>Initializes a new instance of CCResponse from the Xml string.</summary>
		public CCResponse(string Xml) : base (Xml) {}
		///<summary>Initializes a new instance of CCResponse, copying matching properties and fields from O.</summary>
		public CCResponse(object O) : base (O) {}
		///<summary>Initializes a new instance of CCResponse with the exception information in e.</summary>
		public CCResponse(Exception e) : base () {
			try {
				bool IsWarning =  
					(typeof(BusinessRuleException).IsInstanceOfType(e) && ((BusinessRuleException)e).Level==BusinessRuleLevel.Warning);
				if (!IsWarning) Logger.Log(e);
				ActualMessage = e.Message;
				Message = (IsWarning)?ActualMessage:Config.Setting("EM_DEFAULT");
				Flag = (IsWarning)?"CSAA_RULE":"EXCEPTION";
			} catch {}
		}
		/// <summary>The id of the item in the data store.</summary>
		[XmlIgnore]
		[DefaultValue(0)]
		public int ID;

		/// <summary>Unique ID returned by processor.</summary>
		[XmlAttribute]
		public string RequestID;
        
		/// <summary>1 if successful, 0 if not</summary>
		[XmlAttribute]
		public string ReturnCode;
        
		/// <summary>Error message from web service.</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string Message;
        
		/// <summary>Error message from payment processor.</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string ActualMessage;
        
		/// <summary>Result code.</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string Flag;
        
		/// <summary>Unique authorization ID if successfully authed.</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string AuthCode;
        
		/// <summary>Amount authorized.</summary>
		[XmlAttribute]
		public System.Decimal Amount;
        
		/// <summary>Unique reference number from processor.</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string TransactionReferenceNumber;
        
		/// <summary>True if successful and has auth code</summary>
		[XmlAttribute]
		[DefaultValue(false)]
		public bool IsAuthorized;
        
		/// <summary>True if successful.</summary>
		[XmlAttribute]
		[DefaultValue(false)]
		public bool IsRequestSuccessful;
        
		/// <summary>True if flag was DINVALIDDATA</summary>
		[XmlAttribute]
		[DefaultValue(false)]
		public bool IsInvalidData;
        
		/// <summary>True if flag was DINVALIDCARD</summary>
		[XmlAttribute]
		[DefaultValue(false)]
		public bool IsInvalidCard;
        
		/// <summary>True if DAVSNO or DCCV error but authorization code given.</summary>
		[XmlAttribute]
		[DefaultValue(false)]
		public bool IsSoftDecline;

		/// <summary>This is the receipt number created by payment tool</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string ReceiptNumber = "";
		
		/// <summary>True if soft decline, DCARDEXPIRED, DCARDREFUSED, DINVALIDCARD (typing errors).</summary>
		[XmlIgnore]
		[DefaultValue(false)]
		public bool IsReauthCandidate;

		/// <summary>True if the transaction appears complete.</summary>
		[XmlIgnore]
		[DefaultValue(false)]
		public bool IsComplete ;
	}

}
