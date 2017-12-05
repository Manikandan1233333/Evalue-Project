/*
 * MODIFICATION HISTORY:
 * Change Id: Ch1
 * Date: 5/23/2008
 * Modified By: COGNIZANT
 * Modifications: Added a new method called LogInput. This method is used to log the input time stamps for the various methods
 *				  based on the verbose level and the applications set in the web.config
 * Modified by cognizant on 03/26/2009 as a part of IVR LogInputTimeStamp fix.
 * Modified by cognizant on 04/06/2010 as a part of eCheck payment type for HO6.
 * HO6.ch1:Added a new constructor for echeck payment.
 * HO6.ch2:Intialize a echeck class.
 * HO6.ch3:Added a ACHVerify and ACHDeposit to ServiceOperation enumarator.
 * */

using System;
using System.Xml.Serialization;
using System.ComponentModel;
using CSAAWeb.Serializers;
using AuthenticationClasses;

namespace PaymentClasses
{
	/// <summary>
	/// Data class for containing all information about a payment.
	/// </summary>
	public class Payment : SimpleSerializer
	{
		///<summary>Default constructor creates a new instance of Payment.</summary>
		public Payment() : base (){}
		/// <summary>Constructor that accepts an xml string to create a new instanct of Payment.</summary>
		/// <param name="Xml">(string) Xml document containing the Payment.</param>
		public Payment(string Xml) : base (Xml) {}
		/// <summary>Constructor to create a new instanct of Payment from another object.</summary>
		/// <param name="O">Object with matching fields containing data to copy to this new instance.</param>
		public Payment(Object O) : base(O) {}

		/// <summary>
		/// Initializes a new Payment with the requested fields. Generally used for credit card payments.
		/// </summary>
		/// <param name="Operation">(ServiceOperation) The operation to do.</param>
		/// <param name="Application">(string) The name (code) of the calling application.</param>
		/// <param name="Reference">(string) Merchant reference number.</param>
		/// <param name="ReceiptNumber">(string) The reciept number for this payment.</param>
		/// <param name="Card">(CardInfo) The credit card information.</param>
		/// <param name="BillTo">(BillTo) The bill to address.</param>
		/// <param name="Items">(ArrayOfLineItems) An array of Line Items being paid for.</param>
		/// <param name="VerbalAuthorization">(string) An authorization code obtained from the bank over the telephone.</param>
		/// <param name="User">(UserInfo) The logged-in operator making the request.</param>
		public Payment(ServiceOperation Operation, string Application, string Reference, string VerbalAuthorization, 
			string ReceiptNumber, CardInfo Card, BillToInfo BillTo, ArrayOfLineItem Items, UserInfo User) 
		{
			this.Operation=Operation;
			this.Reference=Reference;
			this.ReceiptNumber=ReceiptNumber;
			this.VerbalAuthorization=VerbalAuthorization;
			this.Card=Card;
			this.Application=Application;
			this.BillTo=BillTo;
			this.LineItems=Items;
			this.User=User;
			//			if (Reference!=null)
			//			{
			//				this.ReceiptNumber = "NT12345";
			//			}
			SetAmount();
		}
		///Added by Cognizant on 04/06/2010 for the echeck paymentprocessing as a part of HO6
		//echeck changes - start
		/// <summary>
		/// Initializes a new Payment with the requested fields. Generally used for echeck payments.
		/// </summary>
		/// <param name="PaymentType">(PaymentType)  PaymentType </param>
		/// <param name="Operation">(ServiceOperation) The operation to do.</param>
		/// <param name="Application">(string) The name (code) of the calling application.</param>
		/// <param name="Reference">(string) Merchant reference number.</param>
		/// <param name="ReceiptNumber">(string) The reciept number for this payment.</param>
		/// <param name="echeck">(CardInfo) The credit card information.</param>
		/// <param name="BillTo">(BillTo) The bill to address.</param>
		/// <param name="Items">(ArrayOfLineItems) An array of Line Items being paid for.</param>
		/// <param name="User">(UserInfo) The logged-in operator making the request.</param>
		public Payment(PaymentTypes PaymentType,ServiceOperation Operation, string Application, string Reference, 
			string ReceiptNumber, eCheckInfo echeck, BillToInfo BillTo, ArrayOfLineItem Items, UserInfo User) 
		{
			this.PaymentType = PaymentType;
			this.Operation=Operation;
			this.Reference=Reference;
			this.ReceiptNumber=ReceiptNumber;
			this.echeck = echeck;
			this.Application=Application;
			this.BillTo=BillTo;
			this.LineItems=Items;
			this.User=User;
			SetAmount();
		}
		// echeck changes - end

		/// <summary>
		/// Initializes a new Payment with the requested fields. Generally used for non-credit card payments.
		/// </summary>
		/// <param name="PaymentType">(PaymentTypes) The type of payment.</param>
		/// <param name="Application">(string) The name (code) of the calling application.</param>
		/// <param name="ReceiptNumber">(string) The reciept number for this payment.</param>
		/// <param name="Items">(ArrayOfLineItems) An array of Line Items being paid for.</param>
		/// <param name="User">(UserInfo) The logged-in operator making the request.</param>
		public Payment(PaymentTypes PaymentType, string Application, string ReceiptNumber, ArrayOfLineItem Items, UserInfo User) 
		{
			this.PaymentType=PaymentType;
			this.Application=Application;
			this.ReceiptNumber=ReceiptNumber;
			this.LineItems=Items;
			this.User=User;
			SetAmount();
		}

		///<summary>(BillToInfo) The billing address.</summary>
		[XmlElement]
		[DefaultValue(null)]
		public BillToInfo BillTo;
		///<summary>(CardInfo) The credit card information.</summary>
		///<remarks>This is only supplied for payments of type: On-Line card.</remarks>
		[XmlElement]
		[DefaultValue(null)]
		public CardInfo Card;
		//echeck changes - start
		///Added by Cognizant on 04/06/2010 for the echeck paymentprocessing as a part of HO6
		///<summary> (eCheckInfo) The echeck information</summary>
		///<remarks>This is only supplied for payments of type: echeck.</remarks>		
		[XmlElement]
		[DefaultValue(null)]
		public eCheckInfo echeck;
		//echeck changes - end
		///<summary>(ArrayOfLineItems) An array of Line Items being paid for.</summary>
		[XmlElement]
		[DefaultValue(null)]
		public ArrayOfLineItem LineItems;
		///<summary>(UserInfo) The logged-in operator making the request.</summary>
		///<remarks>This could be a service account for customer-facing systems.</remarks>
		[XmlElement]
		[DefaultValue(null)]
		public UserInfo User;
		///<summary>(string) An authorization code obtained from the bank over the telephone.</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string VerbalAuthorization;
		///<summary>(string) Unique identifier supplied by the card processor.</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string RequestID;
		///<summary>(string) Unique indentifier for this transaction supplied by the merchant.</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string Reference;
		///<summary>(string) Unique identifier for the reciept supplied by APDS.</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string ReceiptNumber;
		///<summary>(string) The function requested to the card processor.</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string Function;
		///<summary>(ServiceOperation enum) The action requested to APDS.</summary>
		[XmlAttribute]
		[DefaultValue(ServiceOperation.NoOp)]
		public ServiceOperation Operation;
		///<summary>(PaymentTypes) The type of payment.</summary>
		[XmlAttribute]
		[DefaultValue(PaymentTypes.CreditCard)]
		public PaymentTypes PaymentType=PaymentTypes.CreditCard;
		///<summary>(string) Name (code) of application making the request.</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string Application;
		///<summary>(int) Id of the payment in the data store.</summary>
		[XmlIgnore]
		[DefaultValue(0)]
		public int PaymentId;
		///<summary>(int) The amount of the payment.</summary>
		[XmlIgnore]
		public Decimal Amount=0;

		private void SetAmount() 
		{
			if (LineItems!=null) 
			{
				Amount=0;
				foreach (LineItem I in LineItems) Amount += I.Amount;
			}
		}

		// START - Ch1 - Added by COGNIZANT ON 05/23/2008 - New method to log the input time stamps
		#region LogInput
		/// <summary>
		/// Writes the input timestamp for each invocation along with the payment information and the 
		/// calling method information into the project's log file
		/// </summary>
		/// <param name="mbCaller">MethodBase of the invoking method</param>
		/// <param name="strAction">Custom Message</param>
		/// <param name="verboselevel">The verbose level for logging time stamps</param>
		public void LogInput(System.Reflection.MethodBase mbCaller, string strAction, int verboselevel)
		{
			try
			{
				if(PaymentType != PaymentTypes.CreditCard)
					return;
				if(verboselevel <= Convert.ToInt32(CSAAWeb.Config.Setting("LogTimeVerboseLevel")))
					if(Convert.ToBoolean(CSAAWeb.Config.Setting("LogInputTimeStamp")) == true)
						if((CSAAWeb.Config.Setting("LogInputforApps")).IndexOf(Convert.ToString(Application)) > -1)//Modified by cognizant on 03/26/2009 as a part of IVR LogInputTimeStamp fix.
						{
							string ActNum, MerchantRef;
							ActNum = MerchantRef = "";
							if(Operation == ServiceOperation.Auth || Operation == ServiceOperation.ReAuth || Operation == ServiceOperation.Reverse)
							{
								ActNum = LineItems[0].AccountNumber;
							}
							MerchantRef = Reference;
							string strTextToLog = "<TimeNode><AccountNumber>";
							strTextToLog += ActNum.ToString();
							strTextToLog += "</AccountNumber><MerchantReference>";
							strTextToLog += MerchantRef.ToString();
							strTextToLog += "</MerchantReference><ClassMethodName>";
							strTextToLog += mbCaller.ReflectedType.FullName.ToString() + "." + mbCaller.Name.ToString();
							strTextToLog += "</ClassMethodName><Action>";
							strTextToLog += strAction;
							strTextToLog += "</Action><CurrentTime>";
							strTextToLog += DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss.fff tt");
							strTextToLog += "</CurrentTime></TimeNode>";
							CSAAWeb.AppLogger.Logger.Log(strTextToLog);
						}
			}
			catch (Exception e)
			{
				CSAAWeb.AppLogger.Logger.Log(e);
			}
		}
		#endregion
		// END - Ch1

	}

	/// <summary>Enumeration of the payment operations available.</summary>
	public enum ServiceOperation 
	{
		/// <summary>Credit card authorization request.</summary>
		Auth, 
		/// <summary>Credit card bill or collect request.</summary>
		Bill, 
		/// <summary>Credit card credit request.</summary>
		Credit, 
		/// <summary>Combined credit card authorization and bill request.</summary>
		Process, 
		/// <summary>Credit card re-authorize request.</summary>
		ReAuth, 
		/// <summary>Credit card reverse authorization request.</summary>
		Reverse, 
		/// <summary>Added by Cognizant on 04/06/2010 for the echeck payment processing as a part of HO6 .</summary>
		/// <summary>echeck Verification request</summary>
		ACHVerify,
		/// <summary>echeck  collect request.</summary>
		ACHDeposit,
		/// <summary>No credit card operation.</summary>
		NoOp,
        Void
	}

	/// <summary>Enumeration of the types of payments available.</summary>
	public enum PaymentTypes 
	{
		/// <summary>Online real-time credit card.</summary>
		CreditCard=1,
		/// <summary>Credit card payment already processed offline.</summary>
		OfflineCreditCard=2,
		/// <summary>Cash payment.</summary>
		Cash=3,
		/// <summary>Check payment.</summary>
		Check=4,
		/// <summary>ACH payment.</summary>
		ECheck=5,
		/// <summary>ACH credit card?</summary>
		ECard=6
	}

}

