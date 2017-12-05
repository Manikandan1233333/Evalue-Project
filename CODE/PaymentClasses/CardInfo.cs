/* MODIFIED BY COGNIZANT
 * STAR Retrofit II Changes: 
 * Modified as a part of CSR#5166
 * 02/01/2007 STAR Retrofit II.Ch1: Added code in the function ValidateFields() to invoke the 
 *                                  check digit algorithm in Cryptor.cs for validating credit card number. 
 * Modified as a part of CSR#4297
 * 02/07/2007 STAR Retrofit II.Ch2: Modified the code so that Credit card number will not be logged into the log.
 * 
 * 
 * Changed as a part of removing eCheck encryption and decryption on 07-21-2010
 * Ch1: Modified Signature function
 *67811A0  - PCI Remediation for Payment systems CH1: Modified the code to mask the cc number to send only last 4 digit to database for CC transaction as a part of VA scan defect by cognizant by 1/05/2011
 *Security Defects- Added the below code to perform valdiations on card field.
 *Security Defect -CH1,CH2,CH3,CH4,CH5,CH6- Commented the below line
 *Security Defect -CH1,CH2- Modified the below line
 *MAIG - CH1 - Modified the Credit Card validation method that works for all Credit Card types 11/17/2014
 */
using System;
using System.Xml.Serialization;
using CSAAWeb;
using CSAAWeb.AppLogger;
using CSAAWeb.Serializers;
using System.Data;
using System.Text.RegularExpressions;

namespace PaymentClasses
{
	/// <summary>
	/// Summary description for CardInfo.
	/// </summary>
    public class CardInfo : SimpleSerializer
	{
		///<summary/>
		const string EXP_MISSING_CC = "Missing or invalid credit card information";

		///<summary/>
		public static AppDictionary Applications = new AppDictionary();

		///<summary/>
		public static string ApplicationID
		{
			get {return Logger.AppNameSpace();}
		}


		/// <summary>
		/// Default no value constructor
		/// </summary>
		public CardInfo() : base() {Application =  CardInfo.ApplicationID;}

		/// <summary>
		/// Constructor for reconstituting from XML, or an empty card with just the app name..
		/// </summary>
		public CardInfo(string st)
		{
			if (!LoadXML(st)) Application = st;
		}

		/// <summary>
		/// Constructor with all fields except card type
		/// </summary>
		public CardInfo(string CCNumber, bool isEncrypted, string CCCVNumber, string CCExpMonth, string CCExpYear)
		{
			Application =  ApplicationID;
			if (Applications[Application]==null) throw new Exception("Unrecognized Application: " + Application + ".");
			if (isEncrypted) throw new Exception("Card may no longer be excrypted.");
			this.CCNumber=CCNumber;
			this.CCCVNumber=CCCVNumber;
			this.CCExpMonth=CCExpMonth;
			this.CCExpYear=CCExpYear;
		}

		/// <summary>
		/// Constructor with all fields
		/// </summary>
		public CardInfo(string CCNumber, bool isEncrypted, string CCCVNumber, string CCExpMonth, string CCExpYear, string CCType)
			:this(CCNumber, isEncrypted, CCCVNumber, CCExpMonth, CCExpYear)
		{
			this.CCType=CCType;
		}


		/// <summary>
		/// Constructor to get fields from another object.
		/// </summary>
		public CardInfo(Object O) : this() 
		{
			Serializer.CopyTo(O, this);
		}

		///<summary/>
		[XmlAttribute]
		public string CCNumber = string.Empty;

		///<summary/>
		[XmlAttribute]
		public string CCCVNumber = string.Empty;

		///<summary/>
		[XmlAttribute]
		public string CCExpMonth = string.Empty;

		///<summary/>
		[XmlAttribute]
        public string CCExpYear = string.Empty;

		///<summary/>
		[XmlAttribute]
        public string CCType = string.Empty;
		
		///<summary/>
		///Added by Cognizant on 07/1/2005 for the CPM Payment Tool integration
		///New Attribute to hold the CCType
		[XmlAttribute]
		public string CPM_CCType = string.Empty;
		

		///<summary/>
		[XmlIgnore]
		public string Signature
		{
			get {
				int i = CCNumber.Length;
                if (i == 0) return "";
                ////Ch1 Start 
				//return Encrypt(CCNumber.Substring(0,4).PadRight(i-4,'X') + CCNumber.Substring(i-4));
                //commented  and changed as a part of removing eCheck encryption and decryption on 07-21-2010.
                //67811A0  - PCI Remediation for Payment systems CH1: Start Modified the code to mask the cc number to send only last 4 digit to database for CC transaction as a part of VA scan defect by cognizant by 1/05/2011
               // return CCNumber.Substring(0, 4).PadRight(i - 4, 'X') + CCNumber.Substring(i - 4);
                return "XXXXXXXXXXXX" + CCNumber.Substring(i - 4);
                //67811A0  - PCI Remediation for Payment systems CH1: END Modified the code to mask the cc number to send only last 4 digit to database for CC transaction as a part of VA scan defect by cognizant by 1/05/2011
                //Ch1 End
				}
            set
            {
                //Ch1 Start 
                CCNumber = value;//Decrypt(value);
                //Ch1 End
            }
		}
		///<summary/>
		[XmlAttribute]
		public string Application = string.Empty;

		///<summary>Set to false if you don't want to ignore CV errors.</summary>
		[XmlAttribute]
		public bool IgnoreCCCV = true;
        // Public Methods

		/// <summary>
		/// Ensures that all required fields are present.
		/// </summary>
		public CCResponse ValidateFields()
		{
            //Security Defect - Added the below code to trim all the fields
            CCNumber = CCNumber.Trim();
            CCExpMonth = CCExpMonth.Trim();
            CCExpYear = CCExpYear.Trim();
            CCType = CCType.Trim();
            CCCVNumber = CCCVNumber.Trim();
            //Security Defect - Added the below code to trim all the fields
            CCResponse c = new CCResponse();
            //Security Defects- START - Added the below code to perform valdiations on card field.
            if (IsMissing(CCNumber) || (CCNumber.Length != 16) || !CSAAWeb.Validate.IsAllNumeric(CCNumber))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "CCNumber";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_CCNUMBER");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            if (IsMissing(CCExpMonth) || (CCExpMonth.Length > 2) || !CSAAWeb.Validate.IsAllNumeric(CCExpMonth) || (System.Convert.ToInt16(CCExpMonth) > 12) || (System.Convert.ToInt16(CCExpMonth) < 1) || ((System.Convert.ToInt16(CCExpYear) == System.DateTime.Now.Year) && (System.Convert.ToInt16(CCExpMonth) < System.DateTime.Now.Month)))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "CCExpMonth";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_CCEXPMONTH");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            if (IsMissing(CCExpYear) || (CCExpYear.Length != 4) || !CSAAWeb.Validate.IsAllNumeric(CCExpYear) || System.Convert.ToInt16(CCExpYear) < System.DateTime.Now.Year)
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "CCExpYear";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_CCEXPYEAR");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            if (IsMissing(CCType) || (CCType.Length != 1) || !CSAAWeb.Validate.IsAllNumeric(CCType))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "CCType";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_CCTYPE");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            if (!IsMissing(CCCVNumber))
            {
                if ((CCCVNumber.Length > 4) || !CSAAWeb.Validate.IsAllNumeric(CCCVNumber))
                {
                    c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "CCVNumber";
                    c.ActualMessage = c.Message;
                    c.Flag = Config.Setting("ERRCDE_CCVNUMBER");
                    Logger.Log(c.Message + c.Flag);
                    return c;
                }
            }
			//Security Defects- END - Added the below code to perform valdiations on card field.
            /*Security Defect - CH2 -START - Commented the below code lines
			if ((IsMissing(CCNumber)) || (IsMissing(CCExpMonth)) || (IsMissing(CCExpYear))) 
			{
				//STAR Retrofit II.Ch2: START - Modified the code below so that Credit card number will not be logged into the log.
				//Logger.Log("CC fields missing, CCNumber=" + CCNumber + ", CCExpMonth=" + CCExpMonth + ", CCExpYear=" + CCExpYear);
				string strCCNumber = IsMissing(CCNumber)?"" : "****";
				Logger.Log("CC fields missing, CCNumber=" + strCCNumber + ", CCExpMonth=" + CCExpMonth + ", CCExpYear=" + CCExpYear);
				//STAR Retrofit II.Ch2: END 
				if (IsMissing(CCNumber))
					Logger.Log(Applications.ToString());
                //Security Defect - CH1 - Commented the below line
				//throw new BusinessRuleException(EXP_MISSING_CC);
			}
			else if (!IgnoreCCCV && IsMissing(CCCVNumber)) 
			{
				Logger.Log("CC_CV missing");
                //Security Defect - CH2- Commented the below line
				//throw new BusinessRuleException(EXP_MISSING_CC);
			}
			else 
			{
				// additional validation for credit card done Modified by Cognizant 
				if ((System.Convert.ToInt16(CCExpMonth) > 12) || (System.Convert.ToInt16(CCExpMonth) < 1)) 
				{
					Logger.Log("bad CC info, CCExpMonth=" + CCExpMonth );
                    //Security Defect - CH3 - Commented the below line
					//throw new BusinessRuleException("Invalid month: " + CCExpMonth);
				}
//				if ((System.Convert.ToInt16(CCExpYear) > 3000) || (System.Convert.ToInt16(CCExpYear) < 2000)) 
//				{
//					Logger.Log("bad CC info, CCExpYear=" + CCExpYear);
//					throw new BusinessRuleException("Invalid year: " + CCExpYear);
//				}

				if ((System.Convert.ToInt16(CCExpYear) < System.DateTime.Now.Year) || (System.Convert.ToInt16(CCExpYear) > 3000)) 
				{
					Logger.Log("bad CC info, CCExpYear=" + CCExpYear);
                    //Security Defect -CH4- Commented the below line
                    //throw new BusinessRuleException("Invalid year: " + CCExpYear);
				}
				if ((System.Convert.ToInt16(CCExpYear) == System.DateTime.Now.Year))
				{
					if ((System.Convert.ToInt16(CCExpMonth) < System.DateTime.Now.Month)) 
					{
						DateTime dt = new DateTime(1990,Convert.ToInt16(CCExpMonth),01);
						Logger.Log("bad CC info, CCExpMonth=" + CCExpMonth);
                        //Security Defect -CH5- Commented the below line
						//throw new BusinessRuleException("Invalid Month: " + dt.ToString("MMMM"));
					}
				}
				
			}
			//STAR Retrofit II.Ch1: START - Added code to invoke the check digit algorithm in Cryptor.cs for validating credit card number. 
            //Security Defect -CH6- Commented the below line
            //if(!CSAAWeb.Validate.IsValidCreditCard(CCNumber))
            //    throw new BusinessRuleException("Invalid Card Number");
            */
            //MAIG - CH1 - BEGIN - Modified the Credit Card validation method that works for all Credit Card types 11/17/2014
            string ChkDigit = Cryptor.CreditCardCheckDigit(CCNumber);
            //MAIG - CH1 - END - Modified the Credit Card validation method that works for all Credit Card types 11/17/2014
			bool vldCCNumber = ((ChkDigit == "0")?true:false);
            if (!vldCCNumber)
            {
                //STAR Retrofit II.Ch2: START - Modified the code below so that Credit card number will not be logged into the log.
                //Logger.Log("Invalid Card Number: " + CCNumber);
                Logger.Log("Invalid Card Number: ****");
                //STAR Retrofit II.Ch2: END
                //Security Defect -CH1 - Modified the below message
                //throw new BusinessRuleException(CSAAWeb.Constants.ERR_AUTHVALIDATION + "CCNumber" + CSAAWeb.Constants.ERR_CODE + Config.Setting("ERRCDE_CCNUMBER"));
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "CCNumber";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_CCNUMBER");
                return c;
            }
            else
            {
                return null;
            }
			//STAR Retrofit II.Ch1: END
		}

		/// <summary>
		/// Ensures that all required fields are present and checks card number against type.
		/// </summary>
		public void ValidateFields(DataTable DT) {
			ValidateFields();
			foreach (DataRow Row in DT.Rows) 
				if (Row["Code"].ToString()==this.CCType) {
					string st = (string)Row["Regex"];
					if (st!="") {
						Regex R = new Regex(st);
						if (!R.IsMatch(this.CCNumber))
						{
						     //Security Defect -CH2 - Modified the below message
                            throw new BusinessRuleException("Card is not of the selected type." + CSAAWeb.Constants.ERR_CODE + Config.Setting("ERRCDE_CCNUMBER")); ;
							}
					}
				}
		}

		// Private methods

		/// <summary>
		/// Decyrpts sValue using the key sKey
		/// </summary>
		private string Decrypt(string sValue) 
		{
			return (sValue=="")?"":Cryptor.Decrypt(sValue, Config.Setting("CSAA_Orders_Key"));
		}

		/// <summary>
		/// Encyrpts sValue using the key sKey
		/// </summary>
		private string Encrypt(string sValue) 
		{
			return (sValue=="")?"":Cryptor.Encrypt(sValue, Config.Setting("CSAA_Orders_Key"));
		}

	}
}
