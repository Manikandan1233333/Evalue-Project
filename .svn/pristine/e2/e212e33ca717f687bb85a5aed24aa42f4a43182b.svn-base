/* CREATED BY COGNIZANT 
 * 05/02/2010: New file created for the payment type eCheck processing as a part of HO6. 
 * 
 * MODIFICATION HISTORY:
 * 06/18/2010: HO6.Ch2 - Modified by COGNIZANT
 * Added constants to hold the error messages to validate lengths for Bank account number
 * and to validate if Account and Routing numbers are numeric.
 * 06/22/2010: HO6.Ch3 - Modified by COGNIZANT
 * Defect Fix for netPOSitive defect # 315
 * Added validation conditions to check if Account number is between 4 and 17 digits in length.
 * Added validation conditions to check if Account Number and routing numbers are numeric.
 * 
 * 06/30/2010:Modified by cognizant to validate routing number if all zeros present as a part of HO6.
 * HO6.Ch1:dded a new constant for error message is all zeros present in Bank routing number.
 * HO6.Ch2:Added a new method to check whether the routing number contains all zeros.
 * 07-21-2010 Changed as a part of removing eCheck encryption and decryption.
 *  Ch1 : removed Encrypt/decrypt in Signature
 *  *  CSAA.COM CH1: - Added Customer name and account Type.
 * CSAA.com CH1 defect 76:Start Modified the if condition to check the E-check customer name mandatory field by cognizant on 10/24/2011.
 * Security Defect - Added the below code to validate the Echeck field.
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
    /// Created new class eCheckInfo inorder to capture the echeck information by Cognizant
    ///  on 05/06/2009 for the echeck payment processing 
    /// </summary>
    public class eCheckInfo : SimpleSerializer
    {
        //eCheck Changes
        ///<summary/>
        const string EXP_MISSING_BankID = "Missing or Invalid Bank Routing Number";

        ///<summary/>
        const string EXP_INVALID_LENGTH_BankID = "Routing number is not of 9 digits in length";

        ///<summary/>
        const string EXP_INVALID_CHECKDIGIT_BankID = "Check Digit for the Routing number is invalid";

        ///<summary/>
        const string EXP_MISSING_ACNo = "Missing or Invalid Bank Account number";

        const string EXP_MISSING_CUST_NAME = "Missing Customer Name";
        // START - HO6.Ch2 - Added error message for length validation for Account Number.
        ///<summary/>
        const string EXP_INVALID_LENGTH_ACNo = "Bank Account number should be between 4 and 17 digits";
        // END  - HO6.cH2

        // START - HO6.Ch2 - Added error message for numeric validation for Account Number.
        ///<summary/>
        const string EXP_NON_NUMERIC_ACNo = "Bank Account number should be numeric";
        // END  - HO6.Ch2

        // START - HO6.Ch2 - Added error message for numeric validation for Routing Number.
        ///<summary/>
        const string EXP_NON_NUMERIC_BankID = "Bank Routing number should be numeric";
        // END  - HO6.Ch2

        ///<summary/>
        //HO6.Ch1:Added a new constant for error message is all zeros present in Bank routing number on 06-30-2010.
        const string EXP_INVALID_BankID = "Invalid Routing number. Please use a valid routing number.";
        ///<summary/>
        public static AppDictionary Applications = new AppDictionary();

        ///<summary/>
        public static string ApplicationID
        {
            get { return Logger.AppNameSpace(); }
        }

        /// <summary>
        /// Default no value constructor
        /// </summary>
        public eCheckInfo() : base() { Application = eCheckInfo.ApplicationID; }

        /// <summary>
        /// Constructor for reconstituting from XML, or an empty check object with just the app name..
        /// </summary>
        public eCheckInfo(string st)
        {
            if (!LoadXML(st)) Application = st;
        }

        /// <summary>
        /// Constructor with all fields
        /// </summary>
        //public eCheckInfo(string BankID, string ACNo, string ACType,string SMethod,string VLevel, string CustName)
        //CSAA.COM CH1: START - Added Customer name and account Type.
        public eCheckInfo(string BankID, string ACNo, string ACType, string CustName)
        {
            Application = ApplicationID;
            if (Applications[Application] == null) throw new Exception("Unrecognized Application: " + Application + ".");
            this.BankId = BankID;
            this.BankAcntNo = ACNo;
            this.BankAcntType = ACType;
            //this.SettlementMethod=SMethod;
            //this.VerificationLevel=VLevel;

            
            this.CustomerName = CustName;
            //CSAA.COM CH1: END - Added Customer name and account Type.			
            //			ValidateFields();
        }

        /// <summary>
        /// Constructor to get fields from another object.
        /// </summary>
        public eCheckInfo(Object O)
            : this()
        {
            Serializer.CopyTo(O, this);

        }

        ///<summary/>
        [XmlAttribute]
        public string BankId = string.Empty;

        ///<summary/>
        [XmlAttribute]
        public string BankAcntNo = string.Empty;

        ///<summary/>
        [XmlAttribute]
        public string BankAcntType = string.Empty;

        ///<summary/>
        [XmlAttribute]
        public string Application = string.Empty;

        ///<summary/>
        //[XmlAttribute]		
        //public string SettlementMethod = string.Empty;

        ///<summary/>
        //[XmlAttribute]      
        //public string VerificationLevel = string.Empty;

        ///<summary/>
        [XmlAttribute]
        public string CustomerName = string.Empty;



        ///Added by Cognizant on 05/07/2009 for the echeck payment
        ///used to encrypt and decrypt the bank account number.
        [XmlIgnore]
        public string signature
        {
            get
            {
                if (BankAcntNo.Length == 0) return "";
                //Ch1 Start commented  and changed as a part of removing eCheck encryption and decryption on 07-21-2010.
                //return Encrypt(BankAcntNo.Substring(BankAcntNo.Length - 4).PadLeft(17, 'X'));//added for padding account number on Oct 20 2009.
                return BankAcntNo.Substring(BankAcntNo.Length - 4).PadLeft(17, 'X');
                //Ch1 End 
            }
            set
            {
                //Ch1 Start 
                BankAcntNo = value;//Decrypt(value); 
                //Ch1 End
            }
        }
        /// <summary>
        /// decrypts the Bank Account number
        /// </summary>
        /// <param name="sValue"></param>
        /// <returns></returns>
        private string Decrypt(string sValue)
        {
            return (sValue == "") ? "" : Cryptor.Decrypt(sValue, Config.Setting("CSAA_ORDERS_KEY"));
        }

        /// <summary>
        /// Encyrpts Bank Account number
        /// </summary>
        private string Encrypt(string sValue)
        {
            return (sValue == "") ? "" : Cryptor.Encrypt(sValue, Config.Setting("CSAA_ORDERS_KEY"));
        }
        //echeck ends
        /// <summary>
        /// Ensures that all required fields are present.
        /// </summary>

        public CCResponse ValidateFields()
        {
            BankId = BankId.Trim();
            BankAcntNo = BankAcntNo.Trim();
            BankAcntType = BankAcntType.Trim();
            Application = Application.Trim();
            CustomerName = CustomerName.Trim();
            //Security Defect - START - Added the below code to validate the Echeck field.
            CCResponse c = new CCResponse();
            if (IsMissing(BankId) || (BankId.Length != 9) || !CSAAWeb.Validate.IsAllNumeric(BankId.Trim()) || (BankId.Substring(8, 1) != RoutingNumberCheckDigit(BankId.Substring(0, 8))))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "BankId";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_BANKID");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            if (IsMissing(BankAcntNo) || (BankAcntNo.Length > 17) || (BankAcntNo.Length < 4) || !CSAAWeb.Validate.IsAllNumeric(BankAcntNo.Trim()))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "BankAcntNo";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_BANKACNTNO");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            if (IsMissing(BankAcntType) || (BankAcntType.Length != 1) || !CSAAWeb.Validate.IsAllChars(BankAcntType.Trim()))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "BankAcntType";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_BANKACNTYPE");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            if (IsMissing(Application) || (Application.Length > 25) || !CSAAWeb.Validate.IsAllChars(Application.Trim()))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "Application";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_APPLICATION");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            //Security defect - Removed the length validation and junk character validation in Customer name
            if (IsMissing(CustomerName))
            {
                //throw new BusinessRuleException(CSAAWeb.Constants.ERR_AUTHVALIDATION + "CustomerName" + CSAAWeb.Constants.ERR_CODE + Config.Setting("ERRCDE_CUSTOMERNAME"));
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "CustomerName";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_CUSTOMERNAME");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            else
            {
                return null;
            }
        
            //Security Defect - END - Added the below code to validate the Echeck field.
            //CSAA.com CH1 defect 76:Start Modified the if condition to check the E-check customer name mandatory field by cognizant on 10/24/2011.
            /*
             * if ((IsMissing(BankId)) || (IsMissing(BankAcntNo)) || (IsMissing(CustomerName)))
            {
                // Modified the code below so that echeck number will not be logged into the log.
                Logger.Log("eCheck details missing: Routing Number=" + BankId + ", Bank Account Number=" + signature);
                if (IsMissing(BankAcntNo))
                {
                    //Logger.Log(Applications.ToString());
                    Logger.Log("BankAcntNo is missing");
                    //throw new BusinessRuleException(EXP_MISSING_ACNo);
                }
                else if (IsMissing(BankId))
                {
                    Logger.Log("Bank ID missing");
                    //throw new BusinessRuleException(EXP_MISSING_BankID);
                }
                else if (IsMissing(CustomerName))
                {
                    Logger.Log("Customer name is missing");
                    //throw new BusinessRuleException(EXP_MISSING_CUST_NAME);

                }
            }
            //CSAA.com CH1 defect 76:End Modified the if condition to check the E-check customer name mandatory field by cognizant on 10/24/2011.
            // START - HO6.Ch3
            // Added length validations for Bank Account Number
            if (BankAcntNo.Trim().Length < 4 || BankAcntNo.Trim().Length > 17)
            {
                Logger.Log(EXP_INVALID_LENGTH_ACNo);
                //throw new BusinessRuleException(EXP_INVALID_LENGTH_ACNo);
            }

            // Added validations to check if Bank Account Number is all numeric
            if (!CSAAWeb.Validate.IsAllNumeric(BankAcntNo.Trim()))
            {
                Logger.Log(EXP_NON_NUMERIC_ACNo);
               // throw new BusinessRuleException(EXP_NON_NUMERIC_ACNo);
            }

            // Added validations to check if Bank Routing Number is all numeric
            if (!CSAAWeb.Validate.IsAllNumeric(BankId.Trim()))
            {
                Logger.Log(EXP_NON_NUMERIC_BankID);
                //throw new BusinessRuleException(EXP_NON_NUMERIC_BankID);
            }
            // END - HO6.Ch3

			// Check digit validation for Routing Number
            if (BankId.Length == 9)
            {
                if (BankId.Substring(8, 1) != RoutingNumberCheckDigit(BankId.Substring(0, 8)))
                {
                    Logger.Log("Check digit for Routing Number is invalid");
                    //throw new BusinessRuleException(EXP_INVALID_CHECKDIGIT_BankID);
                }
            }
            else
            {
				// Validation message to log and respond if Routing number is not exactly 9 digits
                Logger.Log("eCheck details invalid: Routing Number is " + Convert.ToString(BankId.Length) + " in length.");
                //throw new BusinessRuleException(EXP_INVALID_LENGTH_BankID);
            }
			//HO6.Ch2:Modified by cognizant to check whether all zeros are present in routing number and through the error message on 06-30-2010.
            if (IsAllZeros(BankId))
            {
                Logger.Log("eCheck details invalid: Routing Number is " + Convert.ToString(BankId) + " in valid.");
                //throw new BusinessRuleException(EXP_INVALID_BankID);
            }* */

        }
             
        //HO6.Ch2:Added a new method to check whether the routing number contains all zeros on 06-30-2010.
        public static bool IsAllZeros(string s)
        {


            // TODO: regex can probably do this better
            char[] aryChars = s.ToCharArray();

            foreach (char c in aryChars)
            {
                if (c == '0')
                {
                }
                else
                    return false;
            }
            return true;
        }
        private static string RoutingNumberCheckDigit(string transitNumber)
        {

            Int64 lRoutingNumSum = 0;
            Int16 iNextHighestMultiple = 0;
            if (transitNumber.Trim().Length != 8)
                transitNumber = FormatField(transitNumber, 8, "Routing Number Check Digit");
            lRoutingNumSum = ((Convert.ToInt16(transitNumber.Substring(0, 1)) * 3) +
                (Convert.ToInt16(transitNumber.Substring(1, 1)) * 7) +
                (Convert.ToInt16(transitNumber.Substring(2, 1)) * 1) +
                (Convert.ToInt16(transitNumber.Substring(3, 1)) * 3) +
                (Convert.ToInt16(transitNumber.Substring(4, 1)) * 7) +
                (Convert.ToInt16(transitNumber.Substring(5, 1)) * 1) +
                (Convert.ToInt16(transitNumber.Substring(6, 1)) * 3) +
                (Convert.ToInt16(transitNumber.Substring(7, 1)) * 7));
            iNextHighestMultiple = Convert.ToInt16(RoundToNextNumber(Convert.ToDecimal(lRoutingNumSum) / 10) * 10);
            return Convert.ToString(iNextHighestMultiple - lRoutingNumSum);

        }
        public static string FormatField(string inStr, Int16 size, string fldName)
        {
            try
            {

                string outStr = inStr.Trim();
                //check the size first.
                if (outStr.Length > size)
                    throw new ArgumentOutOfRangeException(string.Concat("Field ", fldName, " : ", outStr, " is > then ", size.ToString()));
                //CSAAWeb.Validate.IsAllNumeric(_Policy.Text)
                //if (type == NUMERIC)
                if (CSAAWeb.Validate.IsAllNumeric(inStr.ToString()))
                {
                    //field is numeric, right justify and pad with 0
                    outStr = outStr.PadLeft(size, '0');
                }
                else
                {
                    //field is alphanumeric, left justify and pad with spaces
                    outStr = outStr.PadRight(size).ToUpper();
                }
                return outStr;
            }
            catch (Exception e)
            {
                Logger.Log("Error during the field " + fldName + " formating. Field value is: " + inStr);//,Logger.Log(e,true));
                Logger.Log(e);
                return string.Empty;
            }
        }

        //-------------Round To Next whole number --------
        public static Int64 RoundToNextNumber(Decimal inNum)
        {
            Int64 outNum = 0;
            outNum = Convert.ToInt64(System.Math.Round(inNum));

            if (outNum < inNum)
                outNum++;

            return outNum;

        }





        public void ValidateFields(DataTable DT)
        {
            ValidateFields();

        }
    }
}
