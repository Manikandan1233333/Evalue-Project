// This contains classes useful for serialization of data to pass from one form
// to the next.
/*
 * History
 * 11/1/04	JOM Modified reference for CCResponse to PaymentClasses.CCResponse
 * 5/15/05	AZL Added RenewalOrderSummary and OrderDetailInfo2 for Membership IVR
 * MODIFIED BY COGNIZANT AS PART OF Q4 - RETROFIT CHANGES
 * 12/6/2005  Q4-Retrofit.ch1 :Added two new attributes(PageNbr & CurrentUser) to 
 *			 the SearchCriteria Class
 * MODIFIED BY COGNIZANT AS PART OF CSR #4593
 * 03/17/2006 CSR4593.Ch1 : Modified the code to call CPM based on the applications in 
 *							Web.Config
 * 03/17/2006 CSR4593.Ch2 : Added a new overloaded method Bill with the parameter as 
 *							CPM_Payments(Duplicate of existing Bill Method)
 * 03/17/2006 CSR4593.Ch3 : Added a new overloaded method CompleteOrder with the parameter 
 *							as CPM_Payments(Duplicate of existing CompleteOrder Method)
 * 03/17/2006 CSR4593.Ch4 : Added a new overloaded method Credit with the parameter as 
 *							CPM_Payments(Duplicate of existing Credit Method)
 * 03/17/2006 CSR4593.Ch5 : Added a new overloaded method RecordPayment with the parameter 
 *							as CPM_Payments(Duplicate of existing RecordPayment Method)
 * 03/17/2006 CSR4593.Ch6 : The code segment to invoke CPM and Cybersource have been grouped 
 *							as two different functions,
 *                          CPM_Process and Cybersource_Process respectively.
 * 03/17/2006 CSR4593.Ch7 : Modified CYBER_ to CPM_ in complete order to display the CPM 
 *							exceptions with prefix CPM_
 * 
 * MODIFIED BY COGNIZANT AS PART OF Q1 RETROFIT CHANGES
 * 04/26/2006 - Q1-Retrofit
 * Q1-Retrofit.Ch1 : Declared a variable _RevenueType for RevenueType in the class LineItem
 *					 (changes done as a part of CSR #4833).
 * Q1-Retrofit.Ch2 : Modified the code to set read-write property for RevenueType in 
 *					 the class LineItem(changes done as a part of CSR #4833).
 * 
 * STAR Retrofit II Changes: 
 * Modified as a part of CSR#5166
 * 02/01/2007 STAR Retrofit II.Ch1: Created a new function ValidateCreditCard() in CardInfo class to 
 *                                  validate the credit card number using the CheckDigit algorithm implemented 
 *                                  in CSAAWeb\Cryptor.cs.
 * 02/01/2007 STAR Retrofit II.Ch2: Added a validator to the XML attribute CCNumber in CardInfo class 
 *                                  to invoke the credit card validation method and to display appropriate 
 *                                  error message if the card number is invalid.
 *
 * Modified as a part of CSR#5157
 * 02/02/2007 STAR Retrofit II.Ch3: Modified the code in the function process() to call CPM based on the application name. 
 * 
 * Modified as a part of the HO6
 * 04/06/2010 :Modified the code in the function CPM_Processing and added eCheck() method  to process the eCheck payment.
 * 04/06/2010: Modified the order info class ,added eCheck Info object to the order class.
 * 04/06/2010: New class is eCheckInfo created as a part of eCheck payment.
 * 06/03/2010:Modified the condition for echeck in CPM_Payment() for Defect 146 as a part of HO6.
 * 06/04/2010:Modified a condition to check the application is allowed to process ACHVerify method.
 * 26/05/2010 MP2.0.Ch1 - added 2 parameters in signature int PayType, object eCheck - by Cognizant.
 * 06/21/2010: HO6.Ch6 - Modified by COGNIZANT
 * Added the constants for the error message that will be sent in the response when any validation errors 
 * (missing value, invalid length, invalid sequence for routing numbers) are noticed on the Bank account number or Bank Routing number
 * 06/22/2010: HO6.Ch7 - Modified by COGNIZANT
 * Defect Fix for netPOSitive defect # 315
 * Added code to send the correct Target field, when different validation messages for eCheck are encountered
 * RFC#135294 CH1: Modified the code logic in CPM_process method for processing the e-Check transactions for the configured application product type and to process Record payment for non configured applications by cognizant -As a part of e-check implementation for SalesX application on  04/29/2011
 * CSAA.com CH1: commented below line since city and state are optional fields
 * CSAA.COM CH2: Commented the [ValidatorRequired] check condition since Address1 is optional
 * 67811A0  - PCI Remediation for Payment systems CH1 : Added the new class TransactionUpdate for the void web method input and Output parametrs by cognizant on 08/25/2011.
 * 67811A0  - PCI Remediation for Payment systems.CH2 : Declared a variable PolicyState .
 * 67811A0  - PCI Remediation for Payment systems.CH3 : Declared a variable PolicyPrefix .
 * 67811A0  - PCI Remediation for Payment systems.CH4 : Added the new if condition to avoid updating the line item detail and inserting data's to memebership tables for membership processing application configured in web config by cognizant on 10/10/2011
 * 67811A0  - PCI Remediation for Payment systems.CH5 :  Added new Procedure to create Lines Object for Membership Payment Transactions
 * CSAA.com CH1 defect 76: Added the error message for the E-check customer name mandatory field by cognizant on 10/24/2011.
 * 67811A0  - PCI Remediation for Payment systems CH6 : Added code to enable modified web config key (to read the list of application Id which are enabled to process using Orderwebservice) to access using Process Request by cognizant on 12/20/2011
 * Security defect -Ch1 Start - Modified the below code to add the valdiations for the fields in CardInfo
Security defect - Ch2-START - Modified the below code to add the valdiations for the fields in OrderDetailInfo
Security defect - Ch3-START - Modified the below code to add the valdiations for the fields in Promotioninfo
Security defect - Ch4-START - Modified the below code to add the valdiations for the fields in Lineitem
Security defect - Ch5-START - Modified the below code to add the valdiations for the fields in AddressInfo
 * PC Phase II changes CH1 - Added the below code for handling Insurance Transaction through Payment Central using IDP service.
 * PC Phase II changes CH2 - Added the below code to include the status attribute and comment Upload Date Attribute in the Report Criteria 
 * CHG0078293 - PT enhancement - Added the branch number in the search criteria
 * MAIG - CH1 - Modified the attributes to allow 15 digits CC card.
 * MAIG - CH2 - Added the below attributes AgentID and UserID
 * MAIG - CH3 - Added the below attributes AgencyText
 * MAIG - CH4 - Modified the Credit Card validation method that works for all Credit Card types 11/17/2014
 * CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the instance and method with respect to Membership - March 2016
 * CHGXXXXXXJ - CH2 - Changes made in order to save the token generated - 27/06/2017.
 */
using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml.Serialization;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using CSAAWeb;
using CSAAWeb.AppLogger;
using CSAAWeb.Serializers;
using AuthenticationClasses;
using PaymentClasses.Service;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using OrderClassesII;
namespace OrderClasses
{
    #region OrderInfo
    /// <summary>
    /// OrderInfo contains all the information that an order needs to retain from
    /// page to page.
    /// </summary>
    public class OrderInfo : ValidatingSerializer
    {
        /// <summary>Default constructor</summary>
        public OrderInfo() : base() { }
        /// <summary>Xml constructor</summary>
        public OrderInfo(string Xml) : base(Xml) { }
        /// <summary>Constructor from another Object</summary>
        public OrderInfo(Object O) : base(O) { }

        /// <summary>The order line items.</summary>
        public ArrayOfLineItem Lines = new ArrayOfLineItem();
        /// <summary>The rates for this </summary>
        public ArrayOfAddressInfo Addresses = new ArrayOfAddressInfo();
        /// <summary>The associates for this </summary>
        public CardInfo Card = new CardInfo();
        /// <summary>The eCheck account details</summary>
        public eCheckInfo echeck = new eCheckInfo();//Added by Cognizant on 04/06/2010 for the echeck payment to hold the echeck information as a part of HO6.
        /// <summary>Miscellaneous order detail.</summary>
        public OrderDetailInfo Detail = new OrderDetailInfo();
        /// <summary>The authentication Session.</summary>
        [XmlIgnore]
        public SessionInfo S;
        /// <summary>The logged-in user.</summary>
        [XmlIgnore]
        public UserInfo User;
        /// <summary>/// The products to handle in this transaction/// </summary>
        [XmlArray]
        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the instance for Membership - March 2016
        [XmlArrayItem(typeof(InsuranceClasses.InsuranceInfo))]
        public ArrayOfProductInfo Products = new ArrayOfProductInfo();
        ///<summary/>
        public bool IsValid { get { return Errors == null || Errors.Count == 0; } }
        ///<summary>True if the bill to address record hasn't been created.</summary>
        public bool NoBillTo { get { return (Addresses.BillTo == null); } }

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the GetRates - Code Clan Up II- March 2016
        #region CalculateOrderLines
        /// <summary>
        /// Calculates order lines for the product type specified.
        /// </summary>
        public void CalculateOrderLines(string ProductName)
        {
            foreach (ProductInfo P in Products)
                if (ProductName == "")
                {
                    P.UpdateLines(this);
                }
                else
                    if (P.ProductName == ProductName) P.UpdateLines(this);
        }
        #endregion
        #region Process
        //Modified by J McEwen 10/25/2004, changed Payments constructor for STAR.
        /// <summary>
        /// Processes the order.
        /// </summary>
        public void Process()
        {
            //If the current application has no entry in the web.config file then Cybersource is accessed
            //Begin PC Phase II changes CH1 - Added the below code for handling Insurance Transaction through Payment Central using IDP service.
            if (this.Products["Membership"] == null
                && Convert.ToString(Config.Setting("AppName.PCInsuranceApplications")).IndexOf(S.AppName.ToUpper()) > -1)
            {
                if (this.Lines[0].ProductName.ToUpper() == CSAAWeb.Constants.PC_PRODUCT_CODE)
                {
                    //CHGXXXXXXJ - CH2 - Changes made in order to save the token generated - 27/06/2017.- BEGIN.
                    System.Web.HttpContext.Current.Items.Add("token", this.Lines[0].SubProduct);
                    //System.Web.HttpContext.Current.Items.Add("CardType", MaintainPaymentAccountResponse.paymentCardSubType);
                    //CHGXXXXXXJ - CH2 - Changes made in order to save the token generated - 27/06/2017. - END.
                    this.Lines.Clear();
                    foreach (ProductInfo P1 in this.Products) P1.UpdateLines(this);
                    this.Detail.Amount = this.Lines.Total;
                    IssueDirectPaymentWrapper service = new IssueDirectPaymentWrapper();
                    Logger.Log("Payment Central flow starts");
                    service.ProcessInsurance(this, S);
                    Logger.Log("Payment Central flow ends");
                }
            }
            //End PC Phase II changes CH1 - Added the below code for handling Insurance Transaction through Payment Central using IDP service.
            //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method CPM_Process with respect to CPM_InternalService,CPM_Reference.cs- March 2016;
            //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the Cybersource_Process - Code Clan Up II- March 2016

            //CSR4593.Ch1 : END
            //STAR Retrofit II.Ch3: END
        }

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the Cybersource_Process - Code Clan Up II- March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the UpdateMemLines method with respect to Membership - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods CPM_Process with respect to CPM_InternalService.cs,CPM_Reference.cs - March 2016

        /// <summary>
        /// Records non-credit card payments.
        /// Receipt number is passed as parameter.
        /// </summary>
        public void RecordPayment(Payments CC)
        {
            //PaymentClasses.CCResponse R = CC.RecordPayment((PaymentClasses.PaymentTypes)Detail.PaymentType,"", Lines);
            PaymentClasses.CCResponse R = CC.RecordPayment((PaymentClasses.PaymentTypes)Detail.PaymentType, Detail.ReceiptNumber, Lines);
            Detail.ReceiptNumber = R.ReceiptNumber;
            CompleteOrder("", "0", null, CC);
        }

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods RecordPayment with respect to CPM_InteralService.cs,CPM_Reference.cs - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods BillVerbal - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods Credit - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods Credit with respect to CPM_InternalService,CPM_Reference.cs - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method Bill with respect to CPM_InternalService.cs,CPM_Reference.cs - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method Bill with respect to CPM_InternalService.cs,CPM_Reference.cs - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods eCheck with respect to CPM_InternalService,CPM_Reference.cs- March 2016
        /// <summary>
        /// Appropriately handles any payment errors and returns true if there weren't any.
        /// </summary>
        private void CompleteOrder(string AuthCode, string RequestID, PaymentClasses.CCResponse R, Payments CC)
        {
            if (R != null)
            {
                if (Errors == null) Errors = new ArrayOfErrorInfo();
                string Code = "CYBER_" + R.Flag;
                string Target = "Card.CCNumber";
                switch (R.Flag)
                {
                    case "CSAA_RULE": Code += "PAYMENT_SERVICE_CSAA_RULE"; break;
                    case "EXCEPTION": Code += "PAYMENT_SERVICE_EXCEPTION"; Target = ""; break;
                    case "DCARDEXPIRED": Target = "Card.CCExpMonth"; break;
                    case "DCALL": Target = "Detail.AuthCode"; break;
                    case "ETIMEOUT": Target = ""; break;
                    case "ESYSTEM": Target = ""; break;
                    case "": Target = ""; Code = "PAYMENT_SERVICE"; break;
                }
                Errors.Add(new ErrorInfo(Code, (R.Message != "") ? R.Message : R.ActualMessage, Target));
                CSAAWeb.AppLogger.Logger.Log(R.ActualMessage);
            }
            foreach (ProductInfo P in Products)
            {
                P.Complete_Transaction(AuthCode, RequestID, (R == null), Detail.ReceiptNumber);
                P.UpdateLines(this);
            }
            CC.UpdateLines(User, (PaymentClasses.PaymentTypes)Detail.PaymentType, Detail.ReceiptNumber, Detail.MerchantRefNum, Lines);

        }
        //START  - HO6.Ch6 - eCheck Changes - Added the below constants that will be used for the various error messages
        ///<summary/>
        const string EXP_MISSING_BankID = "Missing or Invalid Bank Routing Number";

        ///<summary/>
        const string EXP_INVALID_LENGTH_BankID = "Routing number is not of 9 digits in length";

        ///<summary/>
        const string EXP_INVALID_CHECKDIGIT_BankID = "Check Digit for the Routing number is invalid";

        ///<summary/>
        const string EXP_MISSING_ACNo = "Missing or Invalid Bank Account number";

        // Added error message for length validation for Account Number.
        ///<summary/>
        const string EXP_INVALID_LENGTH_ACNo = "Bank Account number should be between 4 and 17 digits";

        // Added error message for numeric validation for Account Number.
        ///<summary/>
        const string EXP_NON_NUMERIC_ACNo = "Bank Account number should be numeric";

        // Added error message for numeric validation for Routing Number.
        ///<summary/>
        const string EXP_NON_NUMERIC_BankID = "Bank Routing number should be numeric";
        const string EXP_INVALID_BankID = "Invalid Routing number. Please use a valid routing number.";
        //END  - HO6.Ch6 - eCheck Changes
        //CSAA.com CH1 defect 76:Start Added the error message for the E-check customer name mandatory field by cognizant on 10/24/2011.
        const string EXP_MISSING_CUST_NAME = "Missing Customer Name";
        //CSAA.com CH1 defect 76:End Added the error message for the E-check customer name mandatory field by cognizant on 10/24/2011.

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods CompleteOrder with respect to CPM_InternalService,CPM_Reference.cs- March 2016

        #endregion


    }
    #endregion
    #region CardInfo
    /// <summary>
    /// Class contains information about a credit card.
    /// </summary>
    public class CardInfo : ValidatingSerializer
    {
        ///<summary>Default constructor</summary>
        public CardInfo() : base() { }
        ///<summary>Xml constructor</summary>
        public CardInfo(string Xml) : base(Xml) { }
        ///<summary>Constructor from another object</summary>
        public CardInfo(Object O) : base(O) { }
        ///<summary>The credit card number</summary>
        ////Security defect -CH1 Start - Modified the below code to add the valdiations for the fields in CardInfo
        [XmlAttribute]
        [ValidatorRequired]
        //MAIG - CH1 - BEGIN - Modified the attributes to allow 15 digits CC card.
        //[ValidatorExactLength(Length = 1)]
        [ValidatorMaxLength(Length = 16)]
        //MAIG - CH1 - END - Modified the attributes to allow 15 digits CC card.
        [ValidatorNumeric]
        //STAR Retrofit II.Ch2: START - Added validator to invoke the credit card validation method and display error message accordingly  
        //Security Defect -Modified the below message.
        [Validator(Validation = "ValidateCreditCard", Code = "CPM_CSAA_RULEPAYMENT_SERVICE_CSAA_RULE", Message = "Validation failed in ", Priority = 3)]
        //STAR Retrofit II.Ch2: END
        [DefaultValue("")]
        public string CCNumber;
        ///<summary>The CCCV Number</summary>
        [XmlAttribute]
        //[ValidatorMaxLength(Length = 4)]
        //[ValidatorNumeric]
        [DefaultValue("")]
        [Validator(Validation = "ValidateCCCVNumber", Code = "50033", Message = "Validation failed in ", Priority = 3)]
        public string CCCVNumber;
        ///<summary>The month of expiration</summary>
        [XmlAttribute]
        [ValidatorRequired]
        [ValidatorNumeric]
        [ValidatorMaxLength(Length = 2)]
        [Validator(Validation = "ValidateCCExpMonth", Code = "50034", Message = "Validation failed in ", Priority = 3)]
        [DefaultValue("")]
        public string CCExpMonth;
        ///<summary>The year of expiration</summary>
        [XmlAttribute]
        [ValidatorRequired]
        [ValidatorNumeric]
        [ValidatorExactLength(Length = 4)]
        [DefaultValue("")]
        [Validator(Validation = "ValidateCCExpYear", Code = "50034", Message = "Validation failed in ", Priority = 3)]
        public string CCExpYear;
        ///<summary>The card type.</summary>
        [XmlAttribute]
        [ValidatorRequired]
        [ValidatorExactLength(Length = 1)]
        [ValidatorNumeric]
        [DefaultValue("")]
        public string CCType;

        //STAR Retrofit II.Ch1: START -  Created a new function to validate credit card number using CheckDigit 
        //                               algorithm implemented in CSAAWeb\Cryptor.cs
        protected void ValidateCreditCard(ValidatorAttribute Source, ValidatingSerializerEventArgs e)
        {
            //MAIG - CH4 - BEGIN - Modified the Credit Card validation method that works for all Credit Card types 11/17/2014
            string ChkDigit = Cryptor.CreditCardCheckDigit(CCNumber.Trim());
            //MAIG - CH4 - END - Modified the Credit Card validation method that works for all Credit Card types 11/17/2014
            bool vldCCNumber = ((ChkDigit == "0") ? true : false);
            if (!vldCCNumber)
            {
                e.Error = new ErrorInfo(Source, e.Field);
            }

        }
        protected void ValidateCCExpMonth(ValidatorAttribute Source, ValidatingSerializerEventArgs e)
        {
            if ((System.Convert.ToInt64(CCExpMonth) > 12) || (System.Convert.ToInt64(CCExpMonth) < 1))
            {
                if (CCExpYear != "")
                {
                    if ((System.Convert.ToInt64(CCExpYear) == System.DateTime.Now.Year) && ((System.Convert.ToInt64(CCExpMonth) < System.DateTime.Now.Month)))
                    {
                        e.Error = new ErrorInfo(Source, e.Field);
                    }
                }
                e.Error = new ErrorInfo(Source, e.Field);

            }
        }
        protected void ValidateCCCVNumber(ValidatorAttribute Source, ValidatingSerializerEventArgs e)
        {
            if (e.Value != "")
            {
                if (e.Value.Length > 4 || !CSAAWeb.Validate.IsAllNumeric(e.Value))
                {
                    e.Error = new ErrorInfo(Source, e.Field);
                }
            }

        }
        protected void ValidateCCExpYear(ValidatorAttribute Source, ValidatingSerializerEventArgs e)
        {
            if (e.Value == "" || (e.Value.Length != 4) || !CSAAWeb.Validate.IsAllNumeric(e.Value) || (System.Convert.ToInt64(CCExpYear) < System.DateTime.Now.Year))
            {
                e.Error = new ErrorInfo(Source, e.Field);
            }


        }
        //Security defect - Ch1- End - Modified the below code to add the valdiations for the fields in CardInfo
        //STAR Retrofit II.Ch1 - END

    }
    #endregion

    //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the class RenewalOrderSummary - March 2016


    #region OrderDetailInfo
    /// <summary>
    /// Contains miscellaneous detail about an order.
    /// </summary>
    public class OrderDetailInfo : ValidatingSerializer
    {
        ///<summary>Default constructor</summary>
        public OrderDetailInfo() : base() { }
        ///<summary>Xml constructor</summary>
        public OrderDetailInfo(string Xml) : base(Xml) { }
        ///<summary>Constructor from another object</summary>
        public OrderDetailInfo(Object O) : base(O) { }
        ///Override of base CopyFrom, copies inherited values as well.
        //		public override void CopyFrom(Object O) {
        //			Serializer.CopyFrom(O, this, false);
        //		}
        protected override bool DeclaredOnly { get { return false; } }

        ///<summary>Merchant reference number</summary>
        [XmlAttribute]
        [ValidatorJunk]
        [ValidatorMaxLength(Length = 50)]
        [DefaultValue("")]
        public string MerchantRefNum = string.Empty;
        ///<summary>Payment Request ID.</summary>
        [XmlAttribute]
        [ValidatorJunk]
        [DefaultValue("")]
        public string RequestID = string.Empty;
        ///<summary>Payment authorization code.</summary>
        [XmlAttribute]
        [ValidatorJunk]
        [DefaultValue("")]
        public string AuthCode = string.Empty;
        ///<summary>The total amount of the order.</summary>
        [XmlAttribute]
        [DefaultValue(typeof(System.Decimal), "0")]
        [Validator(Validation = "PositiveAmount", Code = "50016", Message = "Validation failed in ", Priority = 3)]
        public decimal Amount = 0;
        /// <summary>
        /// Added by Cognizant on 05/18/2004 for Adding a new XML attribute PaymentType
        /// </summary> 
        ///<summary>The Payment Type.</summary>
        [XmlAttribute]
        [DefaultValue((int)PaymentClasses.PaymentTypes.CreditCard)]
        public int PaymentType = (int)PaymentClasses.PaymentTypes.CreditCard;

        /// <summary>
        /// Added by Cognizant on 05/19/2004 for Adding a new XML attribute ReceiptNumber
        /// </summary> 
        ///<summary>The Receipt Number.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string ReceiptNumber = "";
        /// <summary>
        /// Added by Cognizant on 05/27/2004 for Adding a new XML attribute CrossReferenceNumber
        /// </summary> 
        ///<summary>The CrossReferenceNumber.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorJunk]
        [ValidatorMaxLength(Length = 25)]
        public string CrossReferenceNumber;
        /// <summary>
        /// Added by Cognizant on 05/27/2004 for Adding a new XML attribute UploadToPOES
        /// </summary> 
        ///<summary>The UploadToPOES.</summary>
        [XmlAttribute]
        [DefaultValue(1)]
        public int UploadToPOES = 1;

        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorMaxLength(Length = 6)]
        [ValidatorJunk]
        // 67811A0 - PCI Remediation for Payment systems.CH2:-START Declared a variable PolicyState .
        public string PolicyState;
        // 67811A0 - PCI Remediation for Payment systems.CH2:-END

        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorMaxLength(Length = 5)]
        [ValidatorJunk]
        // 67811A0 - PCI Remediation for Payment systems.CH3:-START Declared a variable PolicyPrefix .
        public string PolicyPrefix;
        // 67811A0 - PCI Remediation for Payment systems.CH3:-END
        protected void PositiveAmount(ValidatorAttribute Source, ValidatingSerializerEventArgs e)
        {
            if (Convert.ToDecimal(e.Value) < 0 || Convert.ToDecimal(e.Value) > 25000) e.Error = new ErrorInfo(Source, e.Field);
        }


    }
    #endregion
    #region OrderDetailInfo2
    //MIVR
    /// <summary>
    /// Contains miscellaneous detail about an order.
    /// </summary>
    public class OrderDetailInfo2 : ValidatingSerializer
    {
        ///<summary>Default constructor</summary>
        public OrderDetailInfo2() : base() { }
        ///<summary>Xml constructor</summary>
        public OrderDetailInfo2(string Xml) : base(Xml) { }
        ///<summary>Constructor from another object</summary>
        public OrderDetailInfo2(Object O) : base(O) { }
        ///Override of base CopyFrom, copies inherited values as well.
        //		public override void CopyFrom(Object O) {
        //			Serializer.CopyFrom(O, this, false);
        //		}
        protected override bool DeclaredOnly { get { return false; } }
        //Security defect - Ch2-START - Modified the below code to add the valdiations for the fields in OrderDetailInfo
        ///<summary>Merchant reference number</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorMaxLength(Length = 50)]
        public string MerchantRefNum = string.Empty;
        ///<summary>Payment Request ID.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorJunk]
        public string RequestID = string.Empty;
        ///<summary>Payment authorization code.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorJunk]
        public string AuthCode = string.Empty;
        ///<summary>The total amount of the order.</summary>
        [XmlAttribute]
        [DefaultValue(typeof(System.Decimal), "0")]
        public decimal Amount = 0;
        /// <summary>
        /// Added by Cognizant on 05/18/2004 for Adding a new XML attribute PaymentType
        /// </summary> 
        ///<summary>The Payment Type.</summary>
        [XmlAttribute]
        [ValidatorMaxLength(Length = 2)]
        [ValidatorJunk]
        [DefaultValue((int)PaymentClasses.PaymentTypes.CreditCard)]
        public int PaymentType = (int)PaymentClasses.PaymentTypes.CreditCard;
        //Security defect - Ch2-End - Modified the below code to add the valdiations for the fields in OrderDetailInfo
        /// <summary>
        /// Added by Cognizant on 05/19/2004 for Adding a new XML attribute ReceiptNumber
        /// </summary> 
        ///<summary>The Receipt Number.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string ReceiptNumber = "";
        /// <summary>
        /// Added by Cognizant on 05/27/2004 for Adding a new XML attribute CrossReferenceNumber
        /// </summary> 
        ///<summary>The CrossReferenceNumber.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string CrossReferenceNumber;
        /// <summary>
        /// Added by Cognizant on 05/27/2004 for Adding a new XML attribute UploadToPOES
        /// </summary> 
        ///<summary>The UploadToPOES.</summary>
        [XmlAttribute]
        [DefaultValue(1)]
        public int UploadToPOES = 1;
    }
    #endregion

    //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the class PromotionInfo - March 2016

    #region LineItem
    /// <summary>
    /// Class for serializing an order line
    /// </summary>
    public class LineItem : ValidatingSerializer, PaymentClasses.ILineItem
    {
        ///<summary>Default constructor</summary>
        public LineItem() : base() { }
        ///<summary>Xml constructor</summary>
        public LineItem(string Xml) : base(Xml) { }
        ///<summary>Constructor from another object</summary>
        public LineItem(Object O) : base(O) { }
        ///Override of base CopyFrom, copies inherited values as well.
        //		public override void CopyFrom(Object O) {
        //			Serializer.CopyFrom(O, this, false);
        //		}
        protected override bool DeclaredOnly { get { return false; } }

        /// <summary>LineItemNo - line item number property</summary>
        private int _LineItemNo = 0;
        /// <summary>ProductName - holds product name</summary>
        private string _ProductName = "";
        /// <summary>Quantity - holds a quantity for this line item</summary>
        private int _Quantity = 0;
        // Q1-Retrofit.Ch1 : START - Declared a variable _RevenueType for RevenueType(changes done as a part of CSR #4833)
        private string _RevenueType = "";
        // Q1-Retrofit.Ch1 : END
        //Security defect - Ch4-START - Modified the below code to add the valdiations for the fields in Lineitem
        ///<summary/>Security Defect - Added the range validation and numeric validation
        [XmlAttribute]
        [ValidatorMaxLength(Length = 2)]
        [ValidatorNumeric]
        [Validator(Validation = "Rangevalidation", Code = "50014", Message = "Validation failed in ", Priority = 3)]
        public int LineItemNo { get { return _LineItemNo; } set { _LineItemNo = value; } }
        ///<summary/>Security Defect - Added the junk character validation and max length validation
        //Virtual Keyword added to ProductName to override it in insurance lineitem.
        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorJunk]
        [ValidatorMaxLength(Length = 50)]
        public virtual string ProductName { get { return _ProductName.Trim(); } set { _ProductName = value; } }
        ///<summary/>
        [XmlAttribute]
        [DefaultValue("")]
        //[ValidatorJunk]
        [ValidatorMaxLength(Length = 100)]
        public string Description = string.Empty;
        ///<summary/>
        [XmlAttribute]
        [DefaultValue(typeof(System.Decimal), "0")]
        [ValidatorDecimal]
        [Validator(Validation = "PositiveAmount", Code = "50016", Message = "Validation failed in ", Priority = 3)]
        public decimal Price = 0;
        ///<summary/>
        [XmlAttribute]
        [DefaultValue(typeof(System.Decimal), "0")]
        [ValidatorDecimal]
        [Validator(Validation = "PositiveAmount", Code = "50016", Message = "Validation failed in ", Priority = 3)]
        public decimal Amount { get { return Price * Quantity; } set { } }
        ///<summary/>
        [XmlAttribute]
        [DefaultValue(typeof(System.Decimal), "0")]
        [ValidatorDecimal]
        [Validator(Validation = "PositiveAmount", Code = "50016", Message = "Validation failed in ", Priority = 3)]
        public decimal Tax_Amount = 0;
        ///<summary/>
        [XmlAttribute]
        [DefaultValue(0)]
        [ValidatorNumeric]
        [Validator(Validation = "Rangevalidation", Code = "50017", Message = "Validation failed in ", Priority = 3)]
        public int Quantity { get { return _Quantity; } set { _Quantity = value; } }
        ///<summary>Product code for Cybersource.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorMaxLength(Length = 10)]
        [ValidatorJunk]
        public string ProductCode = string.Empty;

        /// <summary>Account holder's first name</summary>
        [XmlAttribute]
        [DefaultValue("")]
        //[ValidatorMaxLength(Length = 40)]
        //[ValidatorJunk]
        public string FirstName = "";

        /// <summary>Account holder's last name</summary>
        [XmlAttribute]
        [DefaultValue("")]
        //[ValidatorMaxLength(Length = 40)]
        //[ValidatorJunk]
        public string LastName = "";

        /// <summary>Account number of line item</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorMaxLength(Length = 25)]
        [ValidatorJunk]
        public string AccountNumber = "";

        /// <summary>Revenue type for this line item</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorMaxLength(Length = 40)]
        [ValidatorJunk]
        //[ValidatorRequired]
        // Q1-Retrofit.Ch2 : START - Modified the code to set read-write property for RevenueType(changes done as a part of CSR #4833)
        public string RevenueType
        { get { return _RevenueType; } set { _RevenueType = value; } }
        // Q1-Retrofit.Ch2 : END

        /// <summary>Revenue code for this line item</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorMaxLength(Length = 10)]
        [ValidatorJunk]
        public string RevenueCode = "";

        /// <summary>Sub product code for this line item</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorMaxLength(Length = 25)]
        [ValidatorJunk]
        public string SubProduct = "";

        /// <summary>Club code for this line item</summary>
        [XmlAttribute]
        [DefaultValue("005")]
        [ValidatorMaxLength(Length = 3)]
        [ValidatorJunk]
        public string ClubCode = "005";


        ///<summary>Validator function insurse that amount is zero or positive.</summary>
        //JOM 11/20/03 - Zero is a valid amount for a line item.
        protected void PositiveAmount(ValidatorAttribute Source, ValidatingSerializerEventArgs e)
        {
            if (Convert.ToDecimal(e.Value) < 0 || Convert.ToDecimal(e.Value) > 25000) e.Error = new ErrorInfo(Source, e.Field);
        }
        //Security Defect - Added the below validation for Line item number verification
        protected void Rangevalidation(ValidatorAttribute Source, ValidatingSerializerEventArgs e)
        {
            if (e.Value.ToString().Length > 2 || Convert.ToInt64(e.Value) > 10 || Convert.ToInt64(e.Value) < 0) e.Error = new ErrorInfo(Source, e.Field);
        }
        //Added by Cognizant 2/15/2005 For override product type property 
        //Security defect - Ch4-end - Modified the below code to add the valdiations for the fields in Lineitem
        public string ProductTypeNew;
    }

    /// <summary>
    /// Collection of order line items.
    /// </summary>
    public class ArrayOfLineItem : ArrayOfValidatingSerializer
    {
        ///<summary>Default constructor</summary>
        public ArrayOfLineItem() : base() { }
        ///<summary>Xml constructor</summary>
        public ArrayOfLineItem(string Xml) : base(Xml) { }
        ///<summary>Constructor from another collection</summary>
        public ArrayOfLineItem(IList source) : base(source) { }
        ///Override of base CopyFrom, copies inherited values as well.
        //		public override void CopyFrom(Object O) {
        //			Serializer.CopyFrom(O, this, false);
        //		}
        protected override bool DeclaredOnly { get { return false; } }

        /// <summary>
        /// Gets or sets the item at index.
        /// </summary>
        public new LineItem this[int index]
        {
            get { return (LineItem)InnerList[index]; }
            set { InnerList[index] = value; }
        }

        /// <summary>
        /// Removes all the items matching TransactionType
        /// </summary>
        /// <param name="ProductName">The type of items to remove.</param>
        public void Clear(string ProductName)
        {
            for (int i = this.Count - 1; i >= 0; i--)
                if (((LineItem)InnerList[i]).ProductName == ProductName)
                    InnerList.RemoveAt(i);
            for (int i = 0; i < Count; i++) this[i].LineItemNo = i;

        }
        /// <summary>
        /// Adds item to the collection.
        /// </summary>
        public void Add(LineItem item)
        {
            item.LineItemNo = InnerList.Count;
            InnerList.Add(item);
            this[this.Count - 1].LineItemNo = this.Count;
        }

        ///<summary/>
        [XmlIgnore]
        public decimal Total
        {
            get
            {
                Decimal Result = 0;
                foreach (LineItem i in this) Result += i.Amount;
                return Result;
            }
        }

        /// <summary>
        /// Appends the lines from source to this as LineItems.
        /// </summary>
        /// <param name="source"></param>
        public void Append(ArrayOfLineItem source)
        {
            foreach (LineItem i in source) Add(new LineItem(i));
        }

    }
    #endregion
    #region AddressInfo
    /// <summary>
    /// Serializing class for the household information from webform Primary 
    /// </summary>
    public class AddressInfo : ValidatingSerializer
    {
        /// <summary>Default constructor</summary>
        public AddressInfo() : base() { }
        /// <summary>Constructor from another object</summary>
        public AddressInfo(Object O) : base(O) { }
        /// <summary>Xml constructor</summary>
        public AddressInfo(string Xml) : base(Xml) { }
        /// <summary>Constructor creates new record with action add</summary>
        /// <summary>Constructor for record of type</summary>
        public AddressInfo(AddressInfoType type)
            : base()
        {
            AddressType = type;
        }
        /// <summary>First name</summary>
        /// //Security defect - Ch5-START - Modified the below code to add the valdiations for the fields in AddressInfo
        [XmlAttribute]
        [ValidatorRequired]
        [ValidatorMaxLength(Length = 40)]
        //[ValidatorJunk]
        [DefaultValue("")]
        public string FirstName = string.Empty;
        /// <summary>Middle name</summary>
        [XmlAttribute]
        [ValidatorMaxLength(Length = 1)]
        //[ValidatorJunk]
        [DefaultValue("")]
        public string MiddleName = string.Empty;
        /// <summary>Last name</summary>
        [XmlAttribute]
        //[ValidatorRequired]
        [ValidatorMaxLength(Length = 40)]
        //[ValidatorJunk]
        [DefaultValue("")]
        public string LastName = string.Empty;


        /// <summary>1st line of address</summary>
        [XmlAttribute]
        // CSAA.COM CH2:START- Commented the [ValidatorRequired] check condition since Address1 is optional
        // [ValidatorRequired]
        //CSAA.COM CH2:END- Commented the [ValidatorRequired] check condition since Address1 is optional
        [ValidatorMaxLength(Length = 40)]
        //[ValidatorJunk]
        [DefaultValue("")]
        public string Address1 = string.Empty;
        /// <summary>2nd line of address</summary>
        [XmlAttribute]
        [ValidatorMaxLength(Length = 40)]
        //[ValidatorJunk]--Security defect - Removed the junk validation in address1
        [DefaultValue("")]
        public string Address2 = string.Empty;
        /// <summary>City</summary>
        [XmlAttribute]
        //[ValidatorRequired] --Security defect - Removed the junk validation in address1
        [ValidatorMaxLength(Length = 25)]
        [ValidatorJunk]
        [DefaultValue("")]
        public string City = string.Empty;
        /// <summary>State code</summary>
        [XmlAttribute]
        //[ValidatorRequired]
        [ValidatorMaxLength(Length = 25)]
        [ValidatorJunk]
        [DefaultValue("")]
        public string State = string.Empty;
        /// <summary>Email address</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [Validator(Validation = "ValidateEmail", Code = "INVALID_EMAIL", Message = "Validation failed in ", Priority = 3)]
        public string Email = string.Empty;
        /// <summary>Day time phone</summary>
        [XmlAttribute]
        //[ValidatorPhone]
        [DefaultValue("")]
        [Validator(Validation = "ValidatePhone", Code = "INVALID_PHONE", Message = "Validation failed in ", Priority = 3)]
        public string DayPhone = string.Empty;
        /// <summary>Evening phone</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [Validator(Validation = "ValidatePhone", Code = "INVALID_PHONE", Message = "Validation failed in ", Priority = 3)]
        public string EveningPhone = string.Empty;

        /// <summary>Zip code</summary>
        [XmlAttribute]
        [ValidatorRequired]
        [Validator(Validation = "ValidateZip", Code = "INVALID_ZIP", Message = "Validation failed in ", Priority = 3)]
        [DefaultValue("")]
        public string Zip = string.Empty;
        /// <summary>Type of address info this is</summary>
        [XmlAttribute]
        [ValidatorRequired]
        [ValidatorJunk]
        [ValidatorMaxLength(Length = 10)]
        [Validator(Validation = "ValidateCityState", Code = "MISSING_CITYSTATE", Message = "Validation failed in ", Priority = 3)]
        public AddressInfoType AddressType = AddressInfoType.Household;
        /// <summary>First letter of the type.</summary>
        /// //Security defect - Ch5-END - Modified the below code to add the valdiations for the fields in AddressInfo
        [XmlAttribute]
        public string AddType { get { return AddressType.ToString().Substring(0, 1); } }

        ///<summary>Validator delegate to validate email entry format</summary>
        protected void ValidateEmail(ValidatorAttribute Source, ValidatingSerializerEventArgs e)
        {
            if (CSAAWeb.Validate.IsValidEmailAddress(Email) == false && Email != "") e.Error = new ErrorInfo(Source, e.Field);
        }
        ///<summary>Validator delegate to validate phone entry format</summary>
        protected void ValidatePhone(ValidatorAttribute Source, ValidatingSerializerEventArgs e)
        {
            if (e.Value != "" && e.Value.Length != 10 && CSAAWeb.Validate.IsAllNumeric(e.Value, true)) e.Error = new ErrorInfo(Source, e.Field);
        }
        ///<summary>Validator delegate to validate zipcode format</summary>
        protected void ValidateZip(ValidatorAttribute Source, ValidatingSerializerEventArgs e)
        {
            if (CSAAWeb.Validate.IsValidZip(Zip) == false && Zip != "") e.Error = new ErrorInfo(Source, e.Field);
        }
        ///<summary>Validator delegate to validate phone entry format</summary>
        protected void ValidateCityState(ValidatorAttribute Source, ValidatingSerializerEventArgs e)
        {
            if (AddressType == AddressInfoType.Household)
            {
                //CSAA.com CH1:START commented below line since city and state are optional fields
                // if (City == "" || State == "") e.Error = new ErrorInfo(Source, e.Field);
                //CSAA.com CH1:END commented below line since city and state are optional fields
            }
        }
    }
    ///<summary>Types of address info records.</summary>
    public enum AddressInfoType
    {
        ///<summary>Billing address</summary>
        BillTo,
        ///<summary>Gift giver address</summary>
        Giftgiver,
        ///<summary>Household</summary>
        Household
    }
    ///<summary>Collection of Address Info Records.</summary>
    public class ArrayOfAddressInfo : ArrayOfValidatingSerializer
    {
        ///<summary>Default constructor</summary>
        public ArrayOfAddressInfo() : base() { }
        ///<summary>Construct from an Xml string</summary>
        public ArrayOfAddressInfo(string Xml) : base(Xml) { }
        ///<summary>Construct from another collection</summary>
        public ArrayOfAddressInfo(IList source) : base(source) { }
        /// <summary>The household address record.</summary>
        [XmlIgnore]
        public AddressInfo Household { get { return Find(AddressInfoType.Household, true); } }
        /// <summary>The BillTo address record.</summary>
        [XmlIgnore]
        public AddressInfo BillTo { get { return Find(AddressInfoType.BillTo, false); } }
        /// <summary>The Giftgiver address record.</summary>
        [XmlIgnore]
        public AddressInfo Giftgiver { get { return Find(AddressInfoType.Giftgiver, false); } }
        /// <summary>
        /// Finds and returns the record of type.
        /// </summary>
        /// <param name="type">The AddressInfoType to return.</param>
        /// <param name="Create">If true, will create it if not found.</param>
        /// <returns>AddressInfo record.</returns>
        private AddressInfo Find(AddressInfoType type, bool Create)
        {
            foreach (AddressInfo A in this) if (A.AddressType == type) return A;
            if (!Create) return null;
            Add(new AddressInfo(type));
            return Find(type, false);
        }
        /// <summary>
        /// Gets or sets the item at index.
        /// </summary>
        public new AddressInfo this[int index]
        {
            get { return (AddressInfo)InnerList[index]; }
            set { InnerList[index] = value; }
        }

        /// <summary>
        /// Adds item to the collection.
        /// </summary>
        public void Add(AddressInfo item)
        {
            InnerList.Add(item);
        }

        /// <summary>
        /// Adds blank item of type to the collection.
        /// </summary>
        public void Add(AddressInfoType type)
        {
            Add(new AddressInfo(type));
        }
        /// <summary>
        /// Recursively calls Validate on all the items in the list 
        /// and appends their Errors to this.  Should be overriden in derived classes
        /// to perform any specific validation required for the list.
        /// </summary>
        public override void Validate()
        {
            foreach (AddressInfo A in this)
            {
                A.Validate(this, "[" + A.AddressType.ToString() + "]");
            }
        }
    }

    #endregion
    #region ProductInfo
    /// <summary>
    /// Forms the base class for products such as membership, insurance.
    /// </summary>
    public abstract class ProductInfo : ValidatingSerializer
    {
        ///<summary>Copyfrom inherited members too.</summary>
        protected override bool DeclaredOnly { get { return false; } }
        /// <summary>Default constructor</summary>
        public ProductInfo() : base() { }
        /// <summary>Constructor from another object</summary>
        public ProductInfo(Object O) : base(O) { }
        /// <summary>Xml constructor</summary>
        public ProductInfo(string Xml) : base(Xml) { }
        /// <summary>The rates for this order.</summary>
        public ArrayOfAddressInfo Addresses;
        /// <summary>The associates for this order.</summary>
        public CardInfo Card;
        /// <summary> a class contains the information about echeck</summary>
        /// <summary> Added by Cognizant on 04/06/2010 for the echeck paymentprocessing  as a part of HO6</summary>
        public eCheckInfo echeck;
        /// <summary>The order ID.</summary>
        public UserInfo User;
        /// <summary>The authentication Session.</summary>
        public SessionInfo S;
        /// <summary>
        /// When implemented by a derived class, this method is called to obtain
        /// basic rate information for the product.
        /// </summary>
        /// <param name="Order">Order to update.</param>
        public abstract void GetRates(OrderInfo Order);
        /// <summary>
        /// When implemented by a derived class, this method is called to generate
        /// line items for this product.  This method is required to be implemented
        /// for any real product, and must clear Order.Lines for it's product type
        /// and append new ones.
        /// </summary>
        /// <param name="Order">Reference to the order.</param>
        public abstract void UpdateLines(OrderInfo Order);
        /// <summary>
        /// When implemented by a derived class, this method is called to begin
        /// the actual order transaction for this product.  This method is required
        /// to be implemented by derived classes for real products.
        /// </summary>
        public abstract void Begin_Transaction(OrderInfo Order);
        /// <summary>
        /// When implemented by derived classes, this method completes processing 
        /// this part of the order after payment authorization.  This method is required
        /// to be implemented by derived classes for real products.
        /// </summary>
        /// <param name="AuthCode">
        /// The payment authorization code.  This may be a real auth code; blank, in which
        /// case the auth request failed, and successful will be false; or an error code, in
        /// which case successful will also be false.  Real auth codes will always begin 
        /// with a digit, error codes with a letter.
        /// </param>
        /// <param name="RequestID">The payment processor Request ID</param>
        /// <param name="Successful">True if payment was successful.</param>
        /// <param name="ReceiptNumber">The receipt number</param>
        public abstract void Complete_Transaction(string AuthCode, string RequestID, bool Successful, string ReceiptNumber);
        ///<summary>Holds the OrderID that gets returned from Begin_Transaction.</summary>
        [XmlAttribute]
        [DefaultValue(0)]
        public int OrderID = 0;
        /// <summary>The name of the product</summary>
        [XmlIgnore]
        public virtual string ProductName { get { return GetType().Name; } }
    }

    /// <summary>
    /// Collection of Product Info
    /// </summary>
    public class ArrayOfProductInfo : ArrayOfValidatingSerializer
    {
        ///<summary>Default constructor</summary>
        public ArrayOfProductInfo() : base() { }
        ///<summary>Construct from an Xml string</summary>
        public ArrayOfProductInfo(string Xml) : base(Xml) { }
        ///<summary>Construct from another collection</summary>
        public ArrayOfProductInfo(IList source) : base(source) { }

        /// <summary>
        /// Gets or sets the item ProductName.
        /// </summary>
        [XmlIgnore]
        public ProductInfo this[string ProductName]
        {
            get
            {
                foreach (ProductInfo P in InnerList)
                    if (P.ProductName == ProductName) return P;
                return null;
            }
            set
            {
                for (int i = 0; i < InnerList.Count; i++)
                    if (((ProductInfo)InnerList[i]).ProductName == ProductName)
                    {
                        InnerList[i] = value;
                        break;
                    }
                Add(value);
            }
        }

        /// <summary>
        /// Gets or sets the item at index for serialization.  To access the actual
        /// item, use alternate format with second boolean indexer.
        /// </summary>
        public new ProductInfo this[int index]
        {
            get { return ((ProductInfo)InnerList[index]); }
            set { InnerList[index] = value; }
        }

        /// <summary>
        /// Adds item to the collection.
        /// </summary>
        public void Add(ProductInfo item)
        {
            InnerList.Add(item);
        }

        /// <summary>
        /// Recursively calls Validate on all the items in the list 
        /// and appends their Errors to this.  Should be overriden in derived classes
        /// to perform any specific validation required for the list.
        /// </summary>
        public override void Validate()
        {
            foreach (ProductInfo S in this)
            {
                S.Validate(this, "[" + S.ProductName + "]");
            }
        }
    }
    #endregion
    #region SearchCriteria
    /// <summary>
    /// Encapsulates the criteria for Searching Transactions.
    /// </summary>
    public class SearchCriteria : SimpleSerializer
    {
        ///<summary>Default contructor.</summary>
        public SearchCriteria() : base() { }
        ///<summary>Xml Constructor.</summary>
        public SearchCriteria(string Xml) : base(Xml) { }
        ///<summary>Contstructor from another object</summary>
        public SearchCriteria(Object O) : base(O) { }

        [XmlAttribute]
        ///<summary/>
        public DateTime StartDate;
        ///<summary/>
        [XmlAttribute]
        public DateTime EndDate;
        ///<summary/>
        //START --> Code added by COGNIZANT - 05/26/2004 - for adding Receipt Number in Search Criteria
        [XmlAttribute]
        [DefaultValue("")]
        public string ReceiptNbr = string.Empty;
        ///<summary/>
        //END
        //START --> Code added by COGNIZANT - 07/07/2004 - for adding Application in Search Criteria
        [XmlAttribute]
        [DefaultValue("")]
        public string App = string.Empty;
        ///<summary/>
        //END
        //START --> Code added by COGNIZANT - 07/07/2004 - for adding Product in Search Criteria
        [XmlAttribute]
        [DefaultValue("")]
        public string ProductCode = string.Empty;
        ///<summary/>
        //END
        //START --> Code added by COGNIZANT - 07/07/2004 - for adding Merchant Reference in Search Criteria
        [XmlAttribute]
        [DefaultValue("")]
        public string MerchantRef = string.Empty;
        ///<summary/>
        //END
        //MAIG - CH2 - BEGIN - Added the below attributes AgentID and UserID
        //START --> Code added by COGNIZANT - 09/15/2014 - for adding Agent & User IDs in Search Criteria - MAIG
        [XmlAttribute]
        [DefaultValue("")]
        public string AgentID = string.Empty;

        [XmlAttribute]
        [DefaultValue("")]
        public string UserID = string.Empty;
        ///<summary/>
        //END
        //MAIG - CH2 - END - Added the below attributes AgentID and UserID

        //START --> Code added by COGNIZANT - 07/07/2004 - for adding Amount in Search Criteria
        [XmlAttribute]
        [DefaultValue(0)]
        public decimal Amount = 0;
        ///<summary/>
        //END
        // .Modified by Cognizant based on new SP Changes


        [XmlAttribute]
        [DefaultValue("")]
        public string AccountNbr = string.Empty;
        ///<summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string LastName = string.Empty;
        ///<summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string CCAuthCode = string.Empty;
        ///<summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string CCNumber = string.Empty;

        //Q4-Retrofit.ch1-START :Added two new Attributes to use in Receipts_Void_Search page
        /// <summary>
        /// Page Number 
        /// </summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string eChckAccNbr = string.Empty;
        //Q4-Retrofit.ch1-START :Added two new Attributes to use in Receipts_Void_Search page
        /// <summary>
        /// Page Number 
        /// </summary>
        [XmlAttribute]
        [DefaultValue(1)]
        public int PageNbr = 1;

        /// <summary>
        /// Current User
        /// </summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string CurrentUser = string.Empty;
        //Q4-Retrofit.ch1-END
        // CHG0078293 - PT enhancement - Added the branch number in the search criteria
        [XmlAttribute]
        [DefaultValue("")]
        public string DO = string.Empty;

    }
    #endregion
    #region ReportCriteria
    /// <summary>
    /// Encapsulates the criteria for Reports.
    /// </summary>
    public class ReportCriteria : SimpleSerializer
    {
        ///<summary>Default contructor.</summary>
        public ReportCriteria() : base() { }
        ///<summary>Xml Constructor.</summary>
        public ReportCriteria(string Xml) : base(Xml) { }
        ///<summary>Contstructor from another object</summary>
        public ReportCriteria(Object O) : base(O) { }

        ///<summary/>
        [XmlAttribute]
        public int ReportType;
        ///<summary/>
        [XmlAttribute]
        public DateTime StartDate;
        ///<summary/>
        [XmlAttribute]
        public DateTime EndDate;
        ///<summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string ProductType = string.Empty;
        ///<summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string RevenueType = string.Empty;
        ///<summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string App = string.Empty;
        ///<summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string Role = string.Empty;
        ///<summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string RepDO = string.Empty;
        ///<summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string Users = string.Empty;

        ///<summary> Code Modified by Cognizant for Status Link ID Property on 05/28/2004</summary><summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string Status_Link_ID = string.Empty;
        ///<summary> Code Modified by Cognizant for Status Link ID Property on 05/28/2004</summary><summary/>


        ///<summary> Code Modified by Cognizant for CurrentUser Property</summary><summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string CurrentUser = string.Empty;
        ///<summary> Code Modified by Cognizant for CurrentUser Property</summary><summary/>

        ///<summary> Code Modified by Cognizant for PaymentType Property</summary><summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string PaymentType = string.Empty;
        ///<summary> Code Modified by Cognizant for PaymentType Property</summary><summary/>

        ///<summary> Code Modified by Cognizant for ReceiptNumber Property on 05/20/2004</summary><summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string ReceiptNumber = string.Empty;
        ///<summary> Code Modified by Cognizant for ReceiptNumber Property on 05/20/2004</summary>		


        ///<summary> Code Modified by Cognizant for ReceiptList Property on 05/20/2004</summary><summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string ReceiptList = string.Empty;
        ///<summary> Code Modified by Cognizant for ReceiptList Property on 05/20/2004</summary>		


        ///<summary> Code Modified by Cognizant for Amounts Property on 05/20/2004</summary><summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string Amounts = string.Empty;
        ///<summary> Code Modified by Cognizant for Amounts Property on 05/20/2004</summary><summary/>

        ///<summary> Code Modified by Cognizant for ItemIDs Property on 05/20/2004</summary><summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string ItemIDs = string.Empty;
        ///<summary> Code Modified by Cognizant for ItemIDs Property on 05/20/2004</summary><summary/>

        //PC Phase II changes CH2 -Start- Commented the below code to include the status attribute in the Report Criteria

        ///<summary> Code Modified by Cognizant for Upload_Date Property on 07/07/2004</summary><summary/>
        //[XmlAttribute]
        //public DateTime Upload_Date;
        ///<summary> Code Modified by Cognizant for Upload_Date Property on 07/07/2004</summary><summary/>

        //PC Phase II changes CH2 -END- Commented the below code to include the status attribute in the Report Criteria

        ///<summary> Code Modified by Cognizant for Approver Property on 19/04/2005</summary><summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string Approver = string.Empty;
        ///<summary> Code Modified by Cognizant for Approver Property</summary><summary/>

        //PC Phase II changes CH2 -START- Added the below code to include the status attribute in the Report Criteria


        ///<summary> Code Modified by Cognizant for Approver Property on 19/04/2005</summary><summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string Status = string.Empty;
        ///<summary> Code Modified by Cognizant for Approver Property</summary><summary/>

        //PC Phase II changes CH2 -END- Added the below code to include the status attribute in the Report Criteria
        //MAIG - CH3 - BEGIN - Added the below attributes AgencyText
        ///<summary> Code Modified by Cognizant for Approver Property on 19/04/2005</summary><summary/>
        [XmlAttribute]
        [DefaultValue("")]
        public string AgencyText = string.Empty;
        //MAIG - CH3 - END - Added the below attributes AgencyText
    }
    #endregion
    #region LookupItem
    /// <summary>
    /// Class for retrieving data for filling select boxes.
    /// </summary>
    public class LookupItem : SimpleSerializer
    {
        /// <summary>Default constructor</summary>
        public LookupItem() : base() { }
        /// <summary>Xml constructor</summary>
        public LookupItem(string Xml) : base(Xml) { }
        /// <summary>Constructor from another Object</summary>
        public LookupItem(Object O) : base(O) { }

        ///<summary>The lookup value.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string Value;
        ///<summary>Set property only, converts int to string.</summary>
        [XmlIgnore]
        public int ID { set { Value = value.ToString(); } }
        ///<summary>The lookup descriptive text</summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string Description;

    }

    /// <summary>
    /// Collection of errors.
    /// </summary>
    public class ArrayOfLookupItem : ArrayOfSimpleSerializer
    {
        ///<summary>Default constructor</summary>
        public ArrayOfLookupItem() : base() { }
        ///<summary>Xml constructor</summary>
        public ArrayOfLookupItem(string Xml) : base(Xml) { }
        ///<summary>Constructor from another collection</summary>
        public ArrayOfLookupItem(IList source) : base(source) { }

        /// <summary>
        /// Gets or sets the item at index.
        /// </summary>
        public new LookupItem this[int index]
        {
            get { return (LookupItem)InnerList[index]; }
            set { InnerList[index] = value; }
        }

        /// <summary>
        /// Adds item to the collection.
        /// </summary>
        public void Add(LookupItem item)
        {
            InnerList.Add(item);
        }

        /// <summary>
        /// Appends the lines from source to this.
        /// </summary>
        /// <param name="source"></param>
        public void Append(ArrayOfLookupItem source)
        {
            foreach (LookupItem i in source) Add(new LookupItem(i));
        }
    }
    #endregion
    #region eCheckInfo
    /// <summary>
    /// Class contains information about a echeck .
    ///Added by Cognizant on 04/06/2010 for the echeck payment processing as a part of HO6
    /// </summary>
    public class eCheckInfo : ValidatingSerializer
    {
        //eCheck Changes
        ///<summary>Default constructor</summary>
        public eCheckInfo() : base() { }
        ///<summary>Xml constructor</summary>
        public eCheckInfo(string Xml) : base(Xml) { }
        ///<summary>Constructor from another object</summary>
        public eCheckInfo(Object O) : base(O) { }
        ///<summary>The BankAcntNo Number</summary>
        /////Security defect - Ch6-START - Modified the below code to add the valdiations for the fields in Echeck
        [XmlAttribute]
        [ValidatorRequired]
        [DefaultValue("")]
        [ValidatorNumeric]
        [ValidatorMaxLength(Length = 17)]
        public string BankAcntNo;
        ///<summary>The Bank ID</summary>
        [XmlAttribute]
        [ValidatorRequired]
        //[ValidatorMaxLength(Length = 11)]
        [DefaultValue("")]
        [ValidatorExactLength(Length = 9)]
        [ValidatorNumeric]

        public string BankId;
        ///<summary>The Bank Acc Type</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorRequired]
        [ValidatorExactLength(Length = 1)]
        [ValidatorJunk]
        public string BankAcntType;
        ///<summary> </summary>
        //[XmlAttribute]
        //[DefaultValue("A")]
        //public string SettlementMethod;
        ///<summary> </summary>
        //[XmlAttribute]
        //[DefaultValue("1")]
        //public string VerificationLevel;
        ///<summary></summary>
        [XmlAttribute]
        [DefaultValueAttribute("")]
        [ValidatorRequired]
        //[ValidatorJunk]
        //[ValidatorMaxLength(Length = 25)]
        public string CustomerName;
        //Security defect - Ch6-END - Modified the below code to add the valdiations for the fields in Echeck
    }
    #endregion
    // 67811A0  - PCI Remediation for Payment systems CH1 Start: Added the new class TransactionUpdate for the void web method input and Output parametrs by cognizant on 08/25/2011.
    public class VoidPayments : ValidatingSerializer
    {
        // Changes
        ///<summary>Default constructor</summary>
        public VoidPayments() : base() { }
        ///<summary>Xml constructor</summary>
        public VoidPayments(string Xml) : base(Xml) { }
        ///<summary>Constructor from another object</summary>
        public VoidPayments(Object O) : base(O) { }
        ///<summary>The User ID</summary>
        [XmlAttribute]
        //[ValidatorRequired]
        [DefaultValue("")]
        public string UserId;

        ///<summary>Application ID</summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string AppId;

        ///<summary>ReceiptNumber</summary>
        [DefaultValue("")]
        public string ReceiptNumber;
        ///<summary> </summary>       

        [XmlAttribute]
        [DefaultValue("")]
        public string Flag;

        [XmlAttribute]
        [DefaultValue("")]
        public string Flag_Message;

        [XmlAttribute]
        [DefaultValue("")]
        public string Error_Message;


    }
    // 67811A0  - PCI Remediation for Payment systems CH1 END: Added the new class TransactionUpdate for the void web method input and Output parametrs by cognizant on 08/25/2011.
}



