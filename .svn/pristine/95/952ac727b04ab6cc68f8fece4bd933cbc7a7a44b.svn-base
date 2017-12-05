/*
 * HISTORY
 * PC Phase II changes - Added the page as part of Enrollment changes.
 * Created by: Cognizant on 4/2/2013
 * Decsription: This is a new file added as part of Payment Central Phase II.
 * PC Security Defect Fix -CH1 - Modified the below code to assign the correct reason code and date in the process of enrollment/ modify enrollment
 * PC Security Defect Fix -CH2 - Added the below code to set the save for future flag to True for enrollment
 * CHG0072116 - Cancel button Fix- Added the below method to Navigate back to CC View details/Payment method selection when the cancel button is clicked.
 * T&C Changes CH1 - Added the below code to assign the policy number and product type value to the arguments
 * MAIG - CH1 - Added the below local varaiable as part of MAIG
 * MAIG - CH2 - Added the below logix to hide the Policy Details grid populated with data from  RPBS Service
 * MAIG - CH3 - Added logic to skip initialization in case of Redirection from Payment
 * MAIG - CH4 - 09112014 - Skipping validation of CC for the redirection
 * MAIG - CH5 - Added the logic to validate for 15 digits if it is an Amex card and also added logic to skip validations on Redirection from Payment Confirm page 
 * MAIG - CH6 - 09112014 - Skipping validation of CC for the redirection
 * MAIG - CH7 - 09112014 - Skipping validation of CC for the redirection and also adding the AMEX card validations as part of MAIG
 * MAIG - CH8 - Modified the below code to pass the details to terms and Condition was per the MAIG requirement
 * MAIG - CH9 - Added new method to display the Recurring information pre-populated
 * MAIG - CH10 - Added the below logic to pass the below details to DB and hide the policy details grid if it is an invalid flow. 
 * MAIG - CH11 - Commeneted the unused code. 
 * MAIG - CH12 - Added logic to skip Token Service in case of Redirection from Payment and re-use the toekn for invalid flow
 * MAIG - CH13 - Added logic to save the Policyholder information into DB and pass the Card details to next page
 * MAIG - CH14 - Added logic to hide the Policy Search Details grid for invalid flow
 * MAIG - CH15 - Added logic to reset the CC Enrollment as Enabled and Terms and Conditions check box
 * MAIG - CH16 - Removed the below object and re-named the object name to dsRepDODetails 
 * MAIG - CH17 - Added code to hide the Policy details grid for invalid policy flow
 * MAIG - CH18 - Added code to pass the Effective date and Reason code based upon the valid or invalid flow and get the datasource from the Properties
 * MAIG - CH19 - Added logic to accomodate additional card types AMEX which is 15 digits
 * MAIG - CH20 - Added logic to accomodate additional card types AMEX,Discover
 * MAIG - CH21 - Added condition to check if the current poicy number is invalid or not
 * MAIG - CH22 - Added logic to display the Payment Type and hide the Grid view if it is null
 * MAIG - CH23 - Added logic to restrict the max digit for AMEX card and added functionality for changing the Payment method for Redirection flow
 * MAIG - CH24 - Modified the below logic to check if the Page is Valid
 * MAIG - CH25 - Modified code to display the Error Code only once
 * MAIG - CH26 - Added logic to check if the Zip code is Numeric
 * MAIG - CH27 - Modified logic to check for the Text name than the index value
 * MAIG - CH28 - Modified the Credit Card validation method that works for all Credit Card types 11/17/2014
 * CHG0112662 - Appended the condition for error code 300 to the existing fault exception handling , Removed Error Code in actual error message & Payment Enrollment SOA Service changes
 * CHG0116140 - Payment Restricition - Set the visibility for Payment Restriction label to false
 * CHGXXXXXXX - Added condition to mask the AMEX Card to 4 digits in Payment Recurring Re-direction flow
*/
namespace MSC.Controls
{
    #region Namespace
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using CSAAWeb;
    using CSAAWeb.WebControls;
    using AuthenticationClasses;
    using OrderClassesII;
    using CSAAWeb.AppLogger;
    using System.Text.RegularExpressions;
    using System.Collections.Generic;
    using RecordEnrollment;
    using System.ServiceModel;
    using System.Xml;
    using InsuranceClasses;
    using System.Linq;
    using MSC.Forms;
    using System.Net.Mail;
    using PC_PaymentService;
    using AuthenticationClasses.WebService;
    using System.Text;
    #endregion

    /// <summary>
    ///		Code for ECheck Controls.
    /// </summary>
    /// 

    public partial class CreditCardEnrollment : ValidatingUserControl
    {
        #region Variables
        private static DataTable CardTypes = null;
        //MAIG - CH1 - BEGIN - Added the below local varaiable as part of MAIG
        public Boolean _isValid = true;
        private Authentication authObj = new Authentication();
        private DataSet dsRepDODetails = new DataSet();
        /// <summary>
        // Included MAIG Iteration2 - start
        private string Invalid_FirstName;
        private string Invalid_LastName;
        private string Invalid_MailingZip;
        // Included MAIG Iteration2 - end
        /// </summary>
        //MAIG - CH1 - END - Added the below local varaiable as part of MAIG
        #endregion

        #region WebControls
        protected CSAAWeb.WebControls.Validator lblbankid;
        protected System.Web.UI.WebControls.DropDownList _AccountType;
        protected CSAAWeb.WebControls.Validator lblAccounttype;
        protected System.Web.UI.WebControls.TextBox _Accountno;
        protected System.Web.UI.WebControls.TextBox _Bankid;
        protected CSAAWeb.WebControls.Validator lblAccountno;
        ///<summary>Credit card number test box</summary>
        protected System.Web.UI.WebControls.TextBox _AuthCode;
        protected System.Web.UI.WebControls.TextBox _Name;
        protected System.Web.UI.WebControls.DropDownList _CardType;
        AuthenticationClasses.Service.Authentication ua;
        ///<summary/>
        protected CSAAWeb.WebControls.Validator vldCardNumber;
        ///<summary/>
        protected CSAAWeb.WebControls.Validator vldAuthCode;
        ///<summary/>
        protected System.Web.UI.HtmlControls.HtmlTableCell AC1;
        #endregion

        #region Properties
        private static DataTable ProductTypes = null;
        public string CCNumber { get { return _CardNumber.Text; } set { _CardNumber.Text = value; } }
        public string Name { get { return _Name.Text; } set { _Name.Text = value; } }
        public string CCExpMonth { get { return ListC.GetListValue(_ExpireMonth); } set { ListC.SetListIndex(_ExpireMonth, value); } }
        ///<summary>Billing expiration year</summary>
        public string CCExpYear { get { return ListC.GetListValue(_ExpireYear); } set { ListC.SetListIndex(_ExpireYear, value); } }
        ///<summary>Billing card type.</summary>
        public string CCType { get { return ListC.GetListValue(_CardType); } set { ListC.SetListIndex(_CardType, value); } }
        ///<summary>Verbal auth code.</summary>
        public string AuthCode { get { return _AuthCode.Text; } set { _AuthCode.Text = value; } }
        ///<summary/>
        public string ZipCode { get { return _txtZipCode.Text; } set { _txtZipCode.Text = value; } }

        ///<summary>Email Address Validation</summary>
        public string EmailAddress { get { return _txtEmailAddress.Text.Trim(); } set { _txtEmailAddress.Text = value; } }
        public int MaxLength = 0;
        ///<summary/>
        public string ErrorMessage = string.Empty;

        //Temp
        public string BillingPlan { get; set; }

        #endregion

        #region Constructor
        public CreditCardEnrollment()
            : base()
        {
            this.Init += new System.EventHandler(this.InitializeLBs);
        }
        #endregion

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        #endregion

        #region Events
        protected override void OnPreRender(EventArgs e)
        {

        }
        ///<summary>Called from init event.</summary>
        private void InitializeLBs(object sender, System.EventArgs e)
        {
            if (CardTypes == null) Initialize();
            _CardType.DataSource = CardTypes;
            _CardType.DataBind();
            int CurrentYear = DateTime.Now.Year;
            string[] arr_year = new string[11];
            arr_year[0] = Constants.PC_CC_SELECT_YEAR;
            for (int i = 1; i < arr_year.Length; i++)
            {
                arr_year[i] = Convert.ToString(CurrentYear + (i - 1));
            }

            _ExpireYear.DataSource = arr_year;
            _ExpireYear.DataBind();
            _ExpireYear.Items[0].Value = "";
        }

        #region Page Load
        protected override void Page_Load(object sender, System.EventArgs e)
        {
            //MAIG - CH2 - BEGIN - Added the below logix to hide the Policy Details grid populated with data from  RPBS Service
            if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow) != null)
            {
                PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl("policySearchDetails");
                if (PH_PolicyDetail != null)
                {
                    PH_PolicyDetail.Visible = false;
                }
            }
            //MAIG - CH2 - END - Added the below logix to hide the Policy Details grid populated with data from  RPBS Service
            if (!Page.IsPostBack)
            {
                //MAIG - CH3 - BEGIN - Added logic to skip initialization in case of Redirection from Payment
                if (ViewState[Constants.PMT_RECURRING_CCECTOKEN] == null)
                {
                    _CardNumber.Text = string.Empty;
                    _CardType.SelectedIndex = 0;
                    _ExpireMonth.SelectedIndex = 0;
                    _ExpireYear.SelectedIndex = 0;
                    _Name.Text = string.Empty;
                    _txtZipCode.Text = string.Empty;
                    _txtEmailAddress.Text = string.Empty;
                    lblErrMessage.Text = string.Empty;
                }
                //MAIG - CH3 - END - Added logic to skip initialization in case of Redirection from Payment
            }
        }
        #endregion

        #region Controls validation when the corresponding label validator is fired.
        //Added a new method as a part of .NetMig 3.5 on 02-10-2010.
        /// <summary>
        /// Validate required controls values
        /// </summary>
        protected void ReqValCheck(Object source, ValidatorEventArgs args)
        {

            if (_CardType.SelectedItem.Text == Constants.PC_CC_DEFAULT_TEXT)
            {
                args.IsValid = false;
                vldCardType.MarkInvalid();
                vldCardType.ErrorMessage = Constants.PC_CC_ERR_INVALID_CARDTYPE;
                CCReqFieldValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }
            if (string.IsNullOrEmpty(_CardNumber.Text))
            {
                // MAIG - CH4 - START - 09112014 - Skipping validation of CC for the redirection
                if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._RecurringRedirectionFlag == null || (ViewState["ResetCCCheck"] != null && Convert.ToBoolean(ViewState["ResetCCCheck"])))
                {
                    args.IsValid = false;
                    vldCardNumber.MarkInvalid();
                    vldCardNumber.ErrorMessage = Constants.PC_CC_ERR_CARDNUMBER_REQ;
                    CCReqFieldValidator.ErrorMessage = string.Empty;
                    _isValid = false;
                }
                // MAIG - CH4 - END - 09112014 - Skipping validation of CC for the redirection
            }
            if (_ExpireMonth.SelectedItem.Text == Constants.PC_CC_SELECT_MONTH)
            {
                args.IsValid = false;
                vldExp1.MarkInvalid();
                vldExp1.ErrorMessage = Constants.PC_CC_ERR_INVALID_EXP_MONTH;
                CCReqFieldValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }

            if (_ExpireYear.SelectedItem.Text == Constants.PC_CC_SELECT_YEAR)
            {
                args.IsValid = false;
                vldExp1.MarkInvalid();
                vldExp1.ErrorMessage = Constants.PC_CC_ERR_SELECT_EXP_YEAR;
                CCReqFieldValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }
            if (string.IsNullOrEmpty(_Name.Text))
            {
                args.IsValid = false;
                vldNameCheck.MarkInvalid();
                vldNameCheck.ErrorMessage = Constants.PC_CC_ERR_NAME_REQ;
                CCReqFieldValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }
            if (string.IsNullOrEmpty(_txtZipCode.Text))
            {
                args.IsValid = false;
                vldZip.MarkInvalid();
                vldZip.ErrorMessage = Constants.PC_CC_ERR_EMPTY_ZIPCODE;
                CCReqFieldValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }
            if (!chkF2Fuser.Checked)
            {
                args.IsValid = false;
                vldF2F.MarkInvalid();
                vldF2F.ErrorMessage = Constants.PC_ERR_CHK_TC;
                CCReqFieldValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }
        }

        /// validate CC Number        
        protected void ValidCCNumber(Object source, ValidatorEventArgs args)
        {
            //MAIG - CH5 - END - Added the logic to validate for 15 digits if it is an Amex card and also added logic to skip validations on Redirection from Payment Confirm page 
            if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._RecurringRedirectionFlag == null || (ViewState["ResetCCCheck"] != null && Convert.ToBoolean(ViewState["ResetCCCheck"])))
            {

                bool isValidCard = false;
                if (_CardType.SelectedItem.Text.ToUpper().Equals(Constants.PC_CRD_TYPE_AMEX_EXPANSION))
                {
                    isValidCard = Validate.IsValidAmexCard(CCNumber);
                }
                else
                {
                    isValidCard = Validate.IsValidCreditCard(CCNumber);
                }

                if (isValidCard)
                {
                    //MAIG - CH28 - BEGIN - Modified the Credit Card validation method that works for all Credit Card types 11/17/2014
                    string chkDigit = Cryptor.CreditCardCheckDigit(CCNumber);
                    //MAIG - CH28 - END - Modified the Credit Card validation method that works for all Credit Card types 11/17/2014
                    bool CardFormat = false;
                    foreach (DataRow Row in CardTypes.Rows)
                        if (Row["Code"].ToString() == this.CCType)
                        {
                            string st = (string)Row["Regex"];
                            if (st != "")
                            {
                                Regex R = new Regex(st);
                                CardFormat = R.IsMatch(this.CCNumber);
                            }
                        }
                    args.IsValid = ((chkDigit == "0" && CardFormat) ? true : false);
                    if (!args.IsValid)
                    {
                        //Start-Added as a part of .Net Mig 3.5
                        CCReqFieldValidator.ErrorMessage = string.Empty;
                        vldCardNumber.ErrorMessage = "";
                        //End-Added as a part of .Net Mig 3.5
                        vldCardNumber.MarkInvalid();
                        CCReqFieldValidator.ErrorMessage = string.Empty;
                        _isValid = false;
                    }
                    //MAIG - CH5 - END - Added the logic to validate for 15 digits if it is an Amex card and also added logic to skip validations on Redirection from Payment Confirm page 
                }
            }
        }


        /// <summary>
        /// validate Card Type
        /// </summary>
        protected void ValidCardType(Object source, ValidatorEventArgs args)
        {
            // MAIG - CH6 - START - 09112014 - Skipping validation of CC for the redirection
            if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._RecurringRedirectionFlag == null || (ViewState["ResetCCCheck"] != null && Convert.ToBoolean(ViewState["ResetCCCheck"])))
            {
                if (Validate.IsValidCreditCard(CCNumber))
                {
                    foreach (DataRow Row in CardTypes.Rows)
                        if (Row[Constants.PC_CC_CODE].ToString() == this.CCType)
                        {
                            string st = (string)Row[Constants.PC_CC_REGX];
                            if (!string.IsNullOrEmpty(st))
                            {
                                Regex R = new Regex(st);
                                args.IsValid = R.IsMatch(this.CCNumber);
                                if (!args.IsValid)
                                {
                                    // vldCardType.MarkInvalid();
                                    vldCardNumber.MarkInvalid();
                                    CCReqFieldValidator.ErrorMessage = string.Empty;
                                    _isValid = false;
                                }
                            }
                        }
                }
            }
            // MAIG - CH6 - END - 09112014 - Skipping validation of CC for the redirection
        }

        //STAR Retrofit III.Ch2: START - Added a new validator delegate to validate the credit card expiration month
        /// <summary>
        /// Valida Expiration Date
        /// </summary>
        protected void ValidExpDate(Object source, ValidatorEventArgs args)
        {
            if (!string.IsNullOrEmpty(CCExpYear) && !string.IsNullOrEmpty(CCExpMonth))
            {
                if ((System.Convert.ToInt16(CCExpYear) == System.DateTime.Now.Year))
                {
                    if ((System.Convert.ToInt16(CCExpMonth) < System.DateTime.Now.Month))
                    {
                        DateTime dt = new DateTime(1990, Convert.ToInt16(CCExpMonth), 01);
                        args.IsValid = false;
                        vldExp1.MarkInvalid();
                        CCReqFieldValidator.MarkInvalid();
                        vldExp1.ErrorMessage = Constants.PC_CC_INVALID_EXP_YEAR;
                        CCReqFieldValidator.ErrorMessage = string.Empty;
                        _isValid = false;
                    }
                }
            }
            if (_ExpireMonth.SelectedItem.Text == Constants.PC_CC_SELECT_MONTH)
            {
                args.IsValid = false;
                vldExp.MarkInvalid();
                vldExp.ErrorMessage = string.Empty;
                CCReqFieldValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }

            if (_ExpireYear.SelectedItem.Text == Constants.PC_CC_SELECT_YEAR)
            {
                args.IsValid = false;
                vldExp.MarkInvalid();

                vldExp.ErrorMessage = string.Empty;
                _isValid = false;

            }

        }



        /// <summary>
        /// validate the length of the CC and corresponding label validator is fired.
        /// </summary>
        protected void ValidLength(Object source, ValidatorEventArgs args)
        {
            // MAIG - CH7 - START - 09112014 - Skipping validation of CC for the redirection and also adding the AMEX card validations as part of MAIG
            if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._RecurringRedirectionFlag == null || (ViewState["ResetCCCheck"] != null && Convert.ToBoolean(ViewState["ResetCCCheck"])))
            {
                if (CCNumber != "")
                {
                    bool isValidCard = false;
                    if (_CardType.SelectedItem.Text.ToUpper().Equals(Constants.PC_CRD_TYPE_AMEX_EXPANSION))
                    {
                        isValidCard = Validate.IsValidAmexCard(CCNumber);
                    }
                    else
                    {
                        isValidCard = Validate.IsValidCreditCard(CCNumber);
                    }
                    args.IsValid = isValidCard;
                    if (!args.IsValid)
                    {
                        if (_CardType.SelectedItem.Text.ToUpper().Equals(Constants.PC_CRD_TYPE_AMEX_EXPANSION))
                        {
                            vldCardNumber.ErrorMessage = Constants.PC_CC_ERR_INVALID_CARDAMEX;
                        }
                        else
                        {
                            vldCardNumber.ErrorMessage = Constants.PC_CC_ERR_INVALID_CARDOTHERS;
                        }
                        vldCardNumber.MarkInvalid();
                        _isValid = false;
                    }
                }
            }
            // MAIG - CH7 - END - 09112014 - Skipping validation of CC for the redirection and also adding the AMEX card validations as part of MAIG
        }

        /// <summary>
        /// validate Zip Code when the corresponding label validator is fired.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void ValidZipLength(Object source, ValidatorEventArgs args)
        {
            if (!string.IsNullOrEmpty(ZipCode))
            {

                //MAIG - CH26 - BEGIN - Added logic to check if the Zip code is Numeric
                args.IsValid = (ZipCode.Length == 5) && CSAAWeb.Validate.IsAllNumeric(ZipCode);
                if (!args.IsValid)
                {
                    vldZip.MarkInvalid();
                    vldZip.ErrorMessage = "Invalid Zip Code.";
                    //MAIG - CH26 - END - Added logic to check if the Zip code is Numeric
                    CCReqFieldValidator.ErrorMessage = string.Empty;
                    _isValid = false;
                }
            }
        }

        /// <summary>
        /// validate Email Address when the corresponding label validator is fired.
        /// </summary>
        protected void ValidateEmailAddress(object source, ValidatorEventArgs args)
        {
            if (!string.IsNullOrEmpty(EmailAddress))
            {
                string patternStrict = Constants.PC_EMAIL_REGX;
                Regex reStrict = new Regex(patternStrict);
                args.IsValid = reStrict.IsMatch(EmailAddress);
                if (!args.IsValid)
                {
                    vldEmailAddress.MarkInvalid();
                    vldEmailAddress.ErrorMessage = Constants.PC_ERR_INVALID_EMAILL_ADDR;
                    CCReqFieldValidator.ErrorMessage = string.Empty;
                    _isValid = false;
                }
            }
        }

        /// <summary>
        /// validate name when the corresponding label validator is fired.
        /// </summary>
        protected void ValidName(object source, ValidatorEventArgs args)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                Regex R = new Regex(Constants.PC_CC_NUMERIC_REGX, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                args.IsValid = !R.IsMatch(this.Name);
                if (!args.IsValid)
                {
                    vldNameCheck.MarkInvalid();
                    vldNameCheck.ErrorMessage = Constants.PC_CC_ERR_INVALID_NAME;
                    CCReqFieldValidator.ErrorMessage = string.Empty;
                    _isValid = false;
                }
            }
        }

        #endregion

        /// <summary>
        /// Defined business rules while the link button "Terms and Conditions" is clicked.
        /// </summary>
        protected void CcTermsAndConditions_btn_OnClick(Object sender, EventArgs e)
        {
            try
            {
                // MAIG - CH8 - START - Modified the below code to pass the details to terms and Condition was per the MAIG requirement
                ////string cardType = (!(string.IsNullOrEmpty(_CardType.SelectedItem.Text.ToString()) || _CardType.SelectedIndex == 0) ? _CardType.SelectedItem.Text.ToString() : string.Empty).ToString();
                ////string cardNumber = string.Empty;
                ////if (!_CardNumber.Text.Equals(string.Empty))
                ////{ cardNumber = _CardNumber.Text.Substring(_CardNumber.Text.Length - 4).ToString(); }
                ////else
                ////{
                ////    cardNumber = string.Empty;
                ////}
                ////string expirationDate = (!string.IsNullOrEmpty(_ExpireMonth.SelectedValue.ToString() + "-" + _ExpireYear.SelectedValue.ToString()) ? _ExpireMonth.SelectedValue + "-" + _ExpireYear.SelectedValue : string.Empty).ToString();
                string insFName = string.Empty;

                if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFlow != null)
                {
                    insFName = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFName.ToString() + " "
                                    + ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyLName.ToString();
                }
                else
                    insFName = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InsuredFullName.ToString();

                MSC.Encryption.EncryptQueryString args = new MSC.Encryption.EncryptQueryString();
                //args[Constants.PC_TC_ARG1] = Constants.PC_PTYPE_TC;
                //args[Constants.PC_TC_ARG2] = cardType;
                //args[Constants.PC_TC_ARG3] = cardNumber;
                //args[Constants.PC_TC_ARG4] = expirationDate;
                args[Constants.PC_TC_ARG2] = insFName;
                //T&C Changes CH1 - START - Added the below code to assign the policy number and product type value to the arguments
                args[Constants.PC_TC_ARG1] = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).PolicyNumber.ToString();
                //args[Constants.PC_TC_ARG8] = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ProductType_PC_Text.ToString();
                //T&C Changes CH1 - END - Added the below code to assign the policy number and product type value to the arguments
                // MAIG - CH8 - END - Modified the below code to pass the details to terms and Condition was per the MAIG requirement
                string url = String.Format(Constants.PC_TC_URL, args.ToString());
                Page.ClientScript.RegisterStartupScript(GetType(), Constants.PC_TC_SCRIPT_KEY, Constants.PC_TC_SCRIPT_ONE + url + Constants.PC_TC_SCRIPT_TWO);
            }
            catch (Exception exception)
            {
                Logger.Name = Constants.PC_TC_LOG_NAME;
                Logger.Log(exception);
            }
        }

        //MAIG - CH9 - BEGIN - Added new method to display the Recurring information pre-populated
        public void PopulateRecurringDetailsFromPayment(List<string> data)
        {
            //CHG0123270 - Added condition to mask the AMEX Card to 4 digits in Payment Recurring Re-direction flow - Start
            if (data[0].ToString().Length == 15)
            {
                _CardNumber.Text = "XXXXXXXXXXXX" + data[0].ToString().Substring(11);
            }
            //CHG0123270 - Added condition to mask the AMEX Card to 4 digits in Payment Recurring Re-direction flow - End
            ////_CardNumber.Text = data[0].ToString();
            //if (data[1].ToString().Equals(Constants.PC_CRD_TYPE_AMEX))
            //{
            //    if (data[0].ToString().Length > 11)
            //    {
            //        _CardNumber.Text = "XXXXXXXXXXX" + data[0].ToString().Substring(11);
            //    }
            //}
            else
            {
                if (data[0].ToString().Length > 12)
                {

                    _CardNumber.Text = "XXXXXXXXXXXX" + data[0].ToString().Substring(12);
                }
            }

            _CardType.SelectedValue = data[1].ToString();
            _ExpireMonth.SelectedValue = data[2].Split('/')[0].Length == 1 ? "0" + data[2].Split('/')[0].ToString() : data[2].Split('/')[0];
            _ExpireYear.SelectedValue = data[2].Split('/')[1].ToString();
            _Name.Text = data[3].ToString();
            _txtZipCode.Text = data[4].ToString();
            if (ViewState[Constants.PMT_RECURRING_CCECTOKEN] == null)
            {
                ViewState[Constants.PMT_RECURRING_CCECTOKEN] = data[5].ToString();
            }
            _txtEmailAddress.Text = data[6].ToString();


            /////// MAIG - Start - 09112014
            /////// Showing the Change Payment link button
            tblRecRedirectLnkButtons.Visible = true;

            ////// Diasbling all fields to restrict from changing the Payment details
            _CardType.Enabled = false;
            // CHGXXXXXXX - To disable Masked Credit Card number in Payment Recurring Redirection Flow - Start
            __CardMasked.Enabled = false;
            // CHGXXXXXXX - To disable Masked Credit Card number in Payment Recurring Redirection Flow - End
            _ExpireMonth.Enabled = false;
            _ExpireYear.Enabled = false;
            _Name.Enabled = false;
            _txtZipCode.Enabled = false;
            /////_txtEmailAddress.Enabled = false;
            /////// MAIG - End - 09112014
        }
        //MAIG - CH9 - END - Added new method to display the Recurring information pre-populated


        /// <summary>
        /// Defined business rules while the image button "Submit" is clicked.
        /// </summary>
        protected void ImgBtnCCEnrollment_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl(Constants.PC_CNTRL_NAME);

            //MAIG - CH10 - START - Added the below logic to pass the below details to DB and hide the policy details grid if it is an invalid flow. 

            if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFlow != null)
            {
                Invalid_FirstName = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFName.ToString();
                Invalid_LastName = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyLName.ToString();
                Invalid_MailingZip = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyMZip.ToString();
            }
            else
            {
                if (PH_PolicyDetail != null)
                {
                    PH_PolicyDetail.Visible = true;
                }
            }
            //MAIG - CH10 - END - Added the below logic to pass the below details to DB and hide the policy details grid if it is an invalid flow. 

            string PC_ProductCode = string.Empty;
            //MAIG - CH24 - BEGIN - Modified the below logic to check if the Page is Valid
            if (_isValid && Page.IsValid)
            //MAIG - CH24 - END - Modified the below logic to check if the Page is Valid
            {
                try
                {
                    //MAIG - CH11 - BEGIN - Commeneted the unused code. 
                    ImgBtnCCEnrollment.Enabled = false;
                    //OrderClasses.Service.Order authUserObj = new OrderClasses.Service.Order();
                    //UserInfo userInfoObj = new UserInfo();

                    //userInfoObj = authUserObj.Authenticate(HttpContext.Current.User.Identity.Name, string.Empty, CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL);

                    List<string> response = new List<string>();
                    OrderClasses.OrderInfo ordderInfo = new OrderClasses.OrderInfo();
                    ordderInfo.User = new UserInfo();

                    SessionInfo sessionInfo = new SessionInfo();
                    IssueDirectPaymentWrapper actObj = new IssueDirectPaymentWrapper();
                    string agentIDSR = string.Empty;

                    DataConnection dsObj = new DataConnection();
                    //MAIG - CH11 - END - Commeneted the unused code. 

                    //MAIG - CH12 - BEGIN - Added logic to skip Token Service in case of Redirection from Payment and re-use the toekn for invalid flow
                    string token = string.Empty;
                    if (ViewState[Constants.PMT_RECURRING_CCECTOKEN] == null)
                    {

                        ordderInfo.S = new SessionInfo();
                        ordderInfo.S.AppName = CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL;
                        ordderInfo.User.UserId = HttpContext.Current.User.Identity.Name;
                        ordderInfo.Card = new OrderClasses.CardInfo();
                        ordderInfo.Card.CCExpMonth = _ExpireMonth.SelectedValue.ToString();
                        ordderInfo.Card.CCExpYear = _ExpireYear.SelectedValue.ToString();
                        ordderInfo.Card.CCNumber = _CardNumber.Text;
                        ordderInfo.Detail = new OrderClasses.OrderDetailInfo();
                        ordderInfo.Detail.PaymentType = 1;
                        OrderClasses.AddressInfo addressInfo = new OrderClasses.AddressInfo();
                        addressInfo.FirstName = _Name.Text;
                        addressInfo.LastName = " ";
                        addressInfo.Zip = _txtZipCode.Text;
                        addressInfo.City = " ";
                        addressInfo.State = " ";
                        addressInfo.AddressType = OrderClasses.AddressInfoType.BillTo;
                        ordderInfo.Addresses.Add(addressInfo);

                        MaintainPaymentAccountRequest inputRequest = new MaintainPaymentAccountRequest();
                        MaintainPaymentAccountResponse tokenResponse = new MaintainPaymentAccountResponse();
                        inputRequest = actObj.Tokenmapping(ordderInfo, sessionInfo);
                        //PC Security Defect Fix -CH2 - Added the below code to set the save for future flag to True for enrollment
                        inputRequest.MaintainPaymentAccountRequest1.saveForFuture = true;
                        if (inputRequest != null)
                        {
                            //Invokes the Payment Token Service
                            tokenResponse = actObj.InvokePaymentTokenService(inputRequest);
                        }
                        if (tokenResponse != null)
                        {
                            token = tokenResponse.MaintainPaymentAccountResponse1.paymentAccountToken;
                        }
                        else
                        {
                            //Added the below code to set Error flag if response object is null
                            Logger.Log(CSAAWeb.Constants.PC_ERR_RESPONSE_NULL);
                            lblErrMessage.Visible = true;
                            lblErrMessage.Text = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;

                            if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow) != null)
                            {
                                if (PH_PolicyDetail != null)
                                {
                                    PH_PolicyDetail.Visible = false;
                                }
                            }
                        }
                    }
                    else
                    {
                        token = ViewState[Constants.PMT_RECURRING_CCECTOKEN].ToString();
                    }

                    //MAIG - CH12 - END - Added logic to skip Token Service in case of Redirection from Payment and re-use the toekn for invalid flow
                    PCEnrollmentMapping pCEnroll = new PCEnrollmentMapping();
                    //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes
                    RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest statusRequest = new RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest();
                    statusRequest = CreatePaymentEnrollmentStatusRequest(token);
                    //CHG0112662 - END - Record Payment Enrollment SOA Service changes

                    //MAIG - CH13 - BEGIN - Added logic to save the Policyholder information into DB and pass the Card details to next page
                    if (string.IsNullOrEmpty(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoAgencyID) || ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoAgencyID.ToString().Equals("0"))
                        statusRequest.agency = string.Empty;
                    else
                        statusRequest.agency = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoAgencyID.ToString();

                    ////if (string.IsNullOrEmpty(userInfoObj.RepId.ToString()) || userInfoObj.RepId.ToString().Equals("0"))
                    ////    statusRequest.agentIdentifier = string.Empty;
                    ////else
                    ////{
                    ////    statusRequest.agentIdentifier = userInfoObj.RepId.ToString();
                    ////    agentIDSR = userInfoObj.RepId.ToString();
                    ////}

                    if (this.Page.User.Identity.Name.Length > 9)
                    {
                        statusRequest.agentIdentifier = Convert.ToString(this.Page.User.Identity.Name).Substring(1);
                    }
                    else if (string.IsNullOrEmpty(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoRepID) || !((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoRepID.ToString().Equals("0"))
                    {
                        statusRequest.agentIdentifier = Convert.ToString(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoRepID);
                    }
                    else
                    {
                        statusRequest.agentIdentifier = null;
                    }



                    if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFlow != null)
                    {
                        dsObj.PT_Update_InvalidPolicy(statusRequest.policyInfo.policyNumber.ToString(), 0, agentIDSR,
                                                            Invalid_LastName, Invalid_FirstName, Invalid_MailingZip, Constants.PC_INVLD_ENROLL_INACTIVE, statusRequest.policyInfo.type,
                                                                       statusRequest.policyInfo.dataSource, 0);
                    }

                    ////// Included MAIG Iteration2 - end - Including Agent & Agency ID to the Enrollment Request

                    //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Renamed the Method
                    response = pCEnroll.PCPaymentEnrollService(statusRequest);
                    //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Renamed the Method
                    if (response[0].Equals(CSAAWeb.Constants.PC_SUCESS.ToUpper()))
                    {
                        Logger.Log(Constants.PC_LOG_STATUS_REQUEST_SUCCESS + statusRequest.policyInfo.policyNumber + Constants.PC_LOG_CNFRM_NUMBER + response[1].ToString());
                        Context.Items.Add(Constants.PC_CNFRMNUMBER, response[1].ToString());

                        if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFlow != null)
                        {
                            dsObj.PT_Update_InvalidPolicy(statusRequest.policyInfo.policyNumber.ToString(), Convert.ToInt64(response[1].ToString()), agentIDSR, string.Empty, string.Empty, string.Empty,
                                                                        Constants.PC_INVLD_ENROLL_ACTIVE, string.Empty, string.Empty, 1);

                            Context.Items.Add("PC_INVALID_POLICY_FLOW_FLAG", true);
                        }
                        else
                        {
                            if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow != null)
                            {
                                Context.Items.Add("PC_INVALID_UNENROLL_FLOW_FLAG", true);
                                Context.Items.Add("PC_INVALID_POLICY_FLOW_FLAG", false);
                            }
                            else
                            {
                                Context.Items.Add("PC_INVALID_UNENROLL_FLOW_FLAG", false);
                                Context.Items.Add("PC_INVALID_POLICY_FLOW_FLAG", false);
                            }
                        }

                        ///// Removing the logic to prefix the PUP for PUP Policies - start
                        ////if (statusRequest.policyInfo.type == Constants.PC_TYPE_PUP)
                        ////{
                        ////    Context.Items.Add(Constants.PC_POLICYNUMBER, Constants.PC_PUP + statusRequest.policyInfo.policyNumber.ToString());
                        ////}
                        ////else
                        ////{
                        Context.Items.Add(Constants.PC_POLICYNUMBER, statusRequest.policyInfo.policyNumber.ToString());
                        ////}
                        ///// Removing the logic to prefix the PUP for PUP Policies - start

                        Context.Items.Add(Constants.PC_CCNumber, Constants.PC_MASK_TXT + statusRequest.paymentItem.card.number);
                        if (statusRequest.paymentItem.card.type.ToUpper().Equals(CSAAWeb.Constants.PC_CRD_TYPE_MASTER))
                        {
                            statusRequest.paymentItem.card.type = CSAAWeb.Constants.PC_CRD_TYPE_MASTER_EXPANSION;
                        }
                        else if (statusRequest.paymentItem.card.type.ToUpper().Equals(CSAAWeb.Constants.PC_CRD_TYPE_AMEX))
                        {
                            statusRequest.paymentItem.card.type = CSAAWeb.Constants.PC_CRD_TYPE_AMEX_EXPANSION;
                        }
                        else if (statusRequest.paymentItem.card.type.ToUpper().Equals(CSAAWeb.Constants.PC_CRD_TYPE_DISC))
                        {
                            statusRequest.paymentItem.card.type = CSAAWeb.Constants.PC_CRD_TYPE_DISC_EXPANSION;
                        }
                        Context.Items.Add(Constants.PC_CCType, statusRequest.paymentItem.card.type.ToUpper());

                        if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow == null)
                        {
                            Context.Items.Add(Constants.PC_BILL_PLAN, ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).BillingPlan);
                            Context.Items.Add(Constants.PC_IS_ENROLLED, ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._IsEnrolled);
                            Context.Items.Add(Constants.PC_VLD_MDFY_ENROLLMENT, ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Modifyenrollment);
                            Context.Items.Add(Constants.PC_VLD_ENROLLMENT, ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_enrollment);
                        }
                        //MAIG - CH13 - END - Added logic to save the Policyholder information into DB and pass the Card details to next page
                        if ((_ExpireMonth.SelectedValue.Length) != 2)
                        {
                            Context.Items.Add(Constants.PC_CCEXP, "0" + _ExpireMonth.SelectedValue + "/" + _ExpireYear.SelectedValue);
                        }
                        else
                        {
                            Context.Items.Add(Constants.PC_CCEXP, _ExpireMonth.SelectedValue + "/" + _ExpireYear.SelectedValue);
                        }
                        Context.Items.Add(Constants.PC_PAYMT_TYPE, Constants.PC_CC);
                        Context.Items.Add(Constants.PC_CUST_NAME, _Name.Text);
                        //PC Phase II Ch1 - Start - Modified code as a part to trigger mail on enrollment successful
                        Context.Items.Add(Constants.PC_EMAIL_ID, _txtEmailAddress.Text.ToString());
                        //PC Phase II Ch1 - End - Modified code as a part to trigger mail on enrollment successful

                        Server.Transfer(Constants.PC_RECURR_CNFRM_URL);
                    }
                    else
                    {
                        Logger.Log(Constants.PC_LOG_FAIL + statusRequest.policyInfo.policyNumber);
                        lblErrMessage.Visible = true;
                        //MAIG - CH25 - BEGIN - Modified code to display the Error Code only once
                        //lblErrMessage.Text = response[1].ToString() + " (" + response[0].ToString() + ")";
                        lblErrMessage.Text = response[1].ToString();
                        //MAIG - CH25 - END - Modified code to display the Error Code only once
                        //MAIG - CH14 - BEGIN - Added logic to hide the Policy Search Details grid for invalid flow
                        if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFlow != null)
                        {
                            PH_PolicyDetail.Visible = false;
                        }
                        ImgBtnCCEnrollment.Enabled = true;
                        //MAIG - CH14 - END - Added logic to hide the Policy Search Details grid for invalid flow
                    }
                }
                catch (FaultException faultExp)
                {
                    DisplayException(faultExp);
                }

            }

        }

        #endregion

        #region Methods

        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);

        }

        private void Initialize()
        {
            CardTypes = ((SiteTemplate)Page).OrderService.LookupDataSet(Constants.PC_PAYMENT, Constants.PC_GET_CREDITCARDS, new object[] { true }).Tables[Constants.PC_TBL_CC];

        }



        #region Clear Control Values
        /// <summary>
        /// Clear the fields whenuser control gets loaded.
        /// </summary>
        public void ClearFields()
        {
            _CardNumber.Text = string.Empty;
            _CardType.SelectedIndex = 0;
            _ExpireMonth.SelectedIndex = 0;
            _ExpireYear.SelectedIndex = 0;
            _Name.Text = string.Empty;
            _txtZipCode.Text = string.Empty;
            _txtEmailAddress.Text = string.Empty;
            lblErrMessage.Text = string.Empty;
            //MAIG - CH15 - BEGIN - Added logic to reset the CC Enrollment as Enabled and Terms and Conditions check box
            ImgBtnCCEnrollment.Enabled = true;
            chkF2Fuser.Checked = false;
            //MAIG - CH15 - END - Added logic to reset the CC Enrollment as Enabled and Terms and Conditions check box
        }
        #endregion

        #region Display Terms
        /// <summary>
        /// Display the terms depends on the DO
        /// </summary>
        public void DisplayTerms()
        {
            //MAIG - CH16 - BEGIN - Removed the below object and re-named the object name to dsRepDODetails
            ////AuthenticationClasses.WebService.Authentication authObj = new AuthenticationClasses.WebService.Authentication();
            OrderClassesII.IssueDirectPaymentWrapper issueDirect = new IssueDirectPaymentWrapper();
            try
            {
                string DO = string.Empty;
                dsRepDODetails = authObj.GetRepIDDO(HttpContext.Current.User.Identity.Name.ToString());
                if (dsRepDODetails.Tables[0].Rows.Count > 0)
                {
                    DO = (dsRepDODetails.Tables[0].Rows[0][Constants.PC_DO_ID].ToString()).Trim();
                    //MAIG - CH16 - END - Removed the below object and re-named the object name to dsRepDODetails
                    List<string> DO_CC = issueDirect.DO_CC_Mapping();

                    if (DO_CC.Contains(DO) || DO == string.Empty)
                    {
                        ContactCenterUser.Visible = true;
                        lblContactCenter.Visible = true;
                        lblContactCenter.Text = CSAAWeb.Constants.PC_Contact_Center_Script;
                        lbl_Terms.Text = CSAAWeb.Constants.PC_CB_TANDC_CONTACTCENTER;
                    }
                    else
                    {
                        lblContactCenter.Visible = false;
                        lbl_Terms.Text = CSAAWeb.Constants.PC_CB_TANDC_F2F;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
            finally
            {
                authObj = null;
                issueDirect = null;
            }
        }
        #endregion


        /// <summary>
        /// Display fault exception inside label 
        /// </summary>
        private void DisplayException(FaultException faultExp)
        {
            FaultException<aaaie.com.recordfinancialaccount.endpoint.FaultInfo> errInfo = (FaultException<aaaie.com.recordfinancialaccount.endpoint.FaultInfo>)faultExp;
            string errFriendlyMsg = string.Empty,
                   errMsg = string.Empty,
                   errCode = string.Empty;
            if (errInfo.Detail != null)
            {
                foreach (XmlNode node in errInfo.Detail.Nodes)
                {
                    if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_FRIENDLY_ERROR_MSG))
                        errFriendlyMsg = node.InnerText;
                    if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_ERROR_MSG))
                        errMsg = node.InnerText;
                    if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_ERROR_CODE))
                        errCode = node.InnerText;
                }
                lblErrMessage.Visible = true;
                //MAIG - CH17 - BEGIN - Added code to hide the Policy details grid for invalid policy flow 
                // Included MAIG Iteration2 - start
                ImgBtnCCEnrollment.Enabled = true;
                // Included MAIG Iteration2 - end


                if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow) != null)
                {
                    PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl(Constants.PC_CNTRL_NAME);

                    if (PH_PolicyDetail != null)
                    {
                        PH_PolicyDetail.Visible = false;
                    }
                }
                //MAIG - CH17 - END - Added code to hide the Policy details grid for invalid policy flow

                //Error Msg Handling - BEGIN - CHG0112662 - Appended the condition for error code 300 to the existing fault exception handling & Removed Error Code in actual error message.
                if (errCode.Contains(CSAAWeb.Constants.ERR_CODE_BUSINESS_EXCEPTION) || errCode.Contains(CSAAWeb.Constants.PC_ERR_CODE_TOKEN_BUSINESS_EXCEPTION)
                                                                                    || errCode.Contains(CSAAWeb.Constants.PC_ERR_CODE_ROUTING_NUMBER_NOT_FOUND))
                {
                    lblErrMessage.Text = CSAAWeb.Constants.PC_ERR_BUSINESS_EXCEPTION + ": " + errMsg;
                }
                else
                {
                    lblErrMessage.Text = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION + " (" + errCode + ")";
                }
                //Error Msg Handling - END - CHG0112662 - Appended the condition for error code 300 to the existing fault exception handling & Removed Error Code in actual error message.
            }
        }

        /// <summary>
        /// Create Request to enroll/modify the policy number
        /// </summary>
        private recordAutoPayEnrollmentStatusRequest CreateStatusRequest(string token)
        {
            recordAutoPayEnrollmentStatusRequest statusRequest = new recordAutoPayEnrollmentStatusRequest();
            //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Added the namespace
            statusRequest.applicationContext = new RecordEnrollment.ApplicationContext();
            //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Added the namespace
            statusRequest.applicationContext.application = Config.Setting(CSAAWeb.Constants.PC_ApplicationContext_Payment_Tool);
            statusRequest.applicationContext.address = CSAAWeb.AppLogger.Logger.GetIPAddress();
            statusRequest.userId = HttpContext.Current.User.Identity.Name;
            //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Added the namespace
            statusRequest.policyInfo = new RecordEnrollment.PolicyProductSource();
            //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Added the namespace
            statusRequest.policyInfo.policyNumber = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).PolicyNumber;
            statusRequest.policyInfo.type = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ProductType_PC;
            statusRequest.enrollmentEffectiveDateSpecified = true;

            //MAIG - CH18 - BEGIN - Added code to pass the Effective date and Reason code based upon the valid or invalid flow and get the datasource from the Properties
            if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFlow == null) && (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow == null))
            {
                //PC Security Defect Fix -CH1 START- Modified the below code to assign the correct reason code and date in the process of enrollment/ modify enrollment
                if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._IsEnrolled.ToUpper().Equals(CSAAWeb.Constants.PC_POL_ENROLL_STATUS.ToUpper()))
                {
                    statusRequest.enrollmentEffectiveDate = Convert.ToDateTime(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Modifyenrollment[2]);
                    statusRequest.enrollmentReasonCode = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Modifyenrollment[3];
                }
                else
                {
                    statusRequest.enrollmentEffectiveDate = Convert.ToDateTime(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_enrollment[2]);
                    statusRequest.enrollmentReasonCode = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_enrollment[3];
                }
            }
            else
            {
                statusRequest.enrollmentEffectiveDate = DateTime.Now;
                statusRequest.enrollmentReasonCode = Constants.PC_INVALID_POLICY_REASON_CODE;
            }
            //PC Security Defect Fix -CH1 END- Modified the below code to assign the correct reason code and date in the process of enrollment/ modify enrollment


            /*if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFlow == null) && (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow == null))
            {
                statusRequest.policyInfo.dataSource = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidPolicySourceSystem;
            }
            else
            {
                IssueDirectPaymentWrapper wrap = new IssueDirectPaymentWrapper();
                statusRequest.policyInfo.dataSource = wrap.DataSource(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ProductType_PT, statusRequest.policyInfo.policyNumber.Length);
            }*/
            statusRequest.policyInfo.dataSource = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidPolicySourceSystem;
            //MAIG - CH18 - END - Added code to pass the Effective date and Reason code based upon the valid or invalid flow and get the datasource from the Properties

            //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Added the namespace
            statusRequest.paymentItem = new RecordEnrollment.PaymentItemHeader();
            //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Added the namespace
            statusRequest.paymentItem.paymentMethod = Config.Setting(CSAAWeb.Constants.PC_CreditCard_Code);
            statusRequest.paymentItem.paymentAccountToken = token;
            //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Added the namespace
            statusRequest.paymentItem.card = new RecordEnrollment.PaymentCard();
            //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Added the namespace
            //MAIG - CH19 - BEGIN - Added logic to accomodate additional card types AMEX which is 15 digits
            statusRequest.paymentItem.card.number = (_CardNumber.Text.Length == 16) ? _CardNumber.Text.Substring(12) : _CardNumber.Text.Substring(11);
            //MAIG - CH19 - END - Added logic to accomodate additional card types AMEX which is 15 digits
            statusRequest.paymentItem.card.isCardPresentSpecified = true;
            statusRequest.paymentItem.card.isCardPresent = true;
            statusRequest.paymentItem.card.expirationDateSpecified = true;
            statusRequest.paymentItem.card.expirationDate = Convert.ToDateTime(_ExpireMonth.SelectedValue + "-" + _ExpireYear.SelectedValue);
            statusRequest.paymentItem.card.printedName = _Name.Text;
            //MAIG - CH27 - BEGIN - Modified logic to check for the Text name than the index value
            if (_CardType.SelectedItem.Text.ToUpper().Equals(Constants.PC_CRD_TYPE_VISA))
            {
                statusRequest.paymentItem.card.type = Constants.PC_CRD_TYPE_VISA;
            }
            //MAIG - CH20 - BEGIN - Added logic to accomodate additional card types AMEX,Discover
            else if (_CardType.SelectedItem.Text.ToUpper().Equals(Constants.PC_CRD_TYPE_MASTER_EXPANSION_DROPDOWN))
            {
                statusRequest.paymentItem.card.type = Constants.PC_CRD_TYPE_MASTER;
            }
            else if (_CardType.SelectedItem.Text.ToUpper().Equals(Constants.PC_CRD_TYPE_AMEX_EXPANSION))
            {
                statusRequest.paymentItem.card.type = Constants.PC_CRD_TYPE_AMEX;
            }
            //MAIG - CH27 - END - Modified logic to check for the Text name than the index value
            else if (_CardType.SelectedItem.Text.ToUpper().Equals(Constants.PC_CRD_TYPE_DISC_EXPANSION))
            {
                statusRequest.paymentItem.card.type = Constants.PC_CRD_TYPE_DISC;
            }

            if (!string.IsNullOrEmpty(_txtEmailAddress.Text.ToString().Trim()))
                statusRequest.emailTo = _txtEmailAddress.Text.ToString().Trim();

            //MAIG - CH20 - END - Added logic to accomodate additional card types AMEX,Discover
            Logger.Log(Constants.PC_LOG_REQ_SENT + statusRequest.policyInfo.policyNumber);
            return statusRequest;
        }


        /// <summary>
        /// Create Payement Enrollment Request to enroll/modify the policy number
        /// CHG0112662 - Record Payment Enrollment SOA Service changes - Added this method
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest CreatePaymentEnrollmentStatusRequest(string token)
        {
            RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest statusRequest = new RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest();
            statusRequest.applicationContext = new RecordPaymentEnrollment.ApplicationContext();
            statusRequest.applicationContext.application = Config.Setting(CSAAWeb.Constants.PC_ApplicationContext_Payment_Tool);
            statusRequest.applicationContext.address = CSAAWeb.AppLogger.Logger.GetIPAddress();
            statusRequest.userId = HttpContext.Current.User.Identity.Name;
            statusRequest.policyInfo = new RecordPaymentEnrollment.PolicyProductSource();
            statusRequest.policyInfo.policyNumber = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).PolicyNumber;
            statusRequest.policyInfo.type = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ProductType_PC;
            statusRequest.enrollmentEffectiveDateSpecified = true;

            //MAIG - CH18 - BEGIN - Added code to pass the Effective date and Reason code based upon the valid or invalid flow and get the datasource from the Properties
            if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFlow == null) && (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow == null))
            {
                //PC Security Defect Fix -CH1 START- Modified the below code to assign the correct reason code and date in the process of enrollment/ modify enrollment
                if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._IsEnrolled.ToUpper().Equals(CSAAWeb.Constants.PC_POL_ENROLL_STATUS.ToUpper()))
                {
                    statusRequest.enrollmentEffectiveDate = Convert.ToDateTime(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Modifyenrollment[2]);
                    statusRequest.enrollmentReasonCode = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Modifyenrollment[3];
                }
                else
                {
                    statusRequest.enrollmentEffectiveDate = Convert.ToDateTime(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_enrollment[2]);
                    statusRequest.enrollmentReasonCode = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_enrollment[3];
                }
            }
            else
            {
                statusRequest.enrollmentEffectiveDate = DateTime.Now;
                statusRequest.enrollmentReasonCode = Constants.PC_INVALID_POLICY_REASON_CODE;
            }
            //PC Security Defect Fix -CH1 END- Modified the below code to assign the correct reason code and date in the process of enrollment/ modify enrollment


            /*if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFlow == null) && (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow == null))
            {
                statusRequest.policyInfo.dataSource = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidPolicySourceSystem;
            }
            else
            {
                IssueDirectPaymentWrapper wrap = new IssueDirectPaymentWrapper();
                statusRequest.policyInfo.dataSource = wrap.DataSource(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ProductType_PT, statusRequest.policyInfo.policyNumber.Length);
            }*/
            statusRequest.policyInfo.dataSource = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidPolicySourceSystem;
            //MAIG - CH18 - END - Added code to pass the Effective date and Reason code based upon the valid or invalid flow and get the datasource from the Properties

            statusRequest.paymentItem = new RecordPaymentEnrollment.PaymentItemHeader();
            statusRequest.paymentItem.paymentMethod = Config.Setting(CSAAWeb.Constants.PC_CreditCard_Code);
            statusRequest.paymentItem.paymentAccountToken = token;
            statusRequest.paymentItem.card = new RecordPaymentEnrollment.PaymentCard();
            //MAIG - CH19 - BEGIN - Added logic to accomodate additional card types AMEX which is 15 digits
            statusRequest.paymentItem.card.number = (_CardNumber.Text.Length == 16) ? _CardNumber.Text.Substring(12) : _CardNumber.Text.Substring(11);
            //MAIG - CH19 - END - Added logic to accomodate additional card types AMEX which is 15 digits
            statusRequest.paymentItem.card.isCardPresentSpecified = true;
            statusRequest.paymentItem.card.isCardPresent = true;
            statusRequest.paymentItem.card.expirationDateSpecified = true;
            statusRequest.paymentItem.card.expirationDate = Convert.ToDateTime(_ExpireMonth.SelectedValue + "-" + _ExpireYear.SelectedValue);
            statusRequest.paymentItem.card.printedName = _Name.Text;
            //MAIG - CH27 - BEGIN - Modified logic to check for the Text name than the index value
            if (_CardType.SelectedItem.Text.ToUpper().Equals(Constants.PC_CRD_TYPE_VISA))
            {
                statusRequest.paymentItem.card.type = Constants.PC_CRD_TYPE_VISA;
            }
            //MAIG - CH20 - BEGIN - Added logic to accomodate additional card types AMEX,Discover
            else if (_CardType.SelectedItem.Text.ToUpper().Equals(Constants.PC_CRD_TYPE_MASTER_EXPANSION_DROPDOWN))
            {
                statusRequest.paymentItem.card.type = Constants.PC_CRD_TYPE_MASTER;
            }
            else if (_CardType.SelectedItem.Text.ToUpper().Equals(Constants.PC_CRD_TYPE_AMEX_EXPANSION))
            {
                statusRequest.paymentItem.card.type = Constants.PC_CRD_TYPE_AMEX;
            }
            //MAIG - CH27 - END - Modified logic to check for the Text name than the index value
            else if (_CardType.SelectedItem.Text.ToUpper().Equals(Constants.PC_CRD_TYPE_DISC_EXPANSION))
            {
                statusRequest.paymentItem.card.type = Constants.PC_CRD_TYPE_DISC;
            }

            if (!string.IsNullOrEmpty(_txtEmailAddress.Text.ToString().Trim()))
                statusRequest.emailTo = _txtEmailAddress.Text.ToString().Trim();

            //MAIG - CH20 - END - Added logic to accomodate additional card types AMEX,Discover
            Logger.Log(Constants.PC_LOG_REQ_SENT + statusRequest.policyInfo.policyNumber);
            return statusRequest;
        }

        #endregion
        #region Cancel Click event
        /// <summary>
        /// CHG0072116 - Cancel button Fix- Added the below method to Navigate back to CC View details/Payment method selection when the cancel button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImgBtnCancel_Click(object sender, ImageClickEventArgs e)
        {
            PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl("policySearchDetails");
            PaymentMethod selectPayment = (PaymentMethod)this.NamingContainer.FindControl("PaymentMethod");
            this.Visible = false;
            lblErrMessage.Visible = false;
            //MAIG - CH21 - BEGIN - Added condition to check if the current poicy number is invalid or not
            ImgBtnCCEnrollment.Enabled = true;
            if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._RecurringRedirectionFlag != null)
            {
                ViewState["ResetCCCheck"] = true;
            }
            EnableAllFields();
            /////// Hiding the Change Payment link button
            tblRecRedirectLnkButtons.Visible = false;
            if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFlow == null) && (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow == null))
            {
                if (PH_PolicyDetail != null)
                {
                    if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow) == null)
                        //MAIG - CH21 - END - Added condition to check if the current poicy number is invalid or not
                        PH_PolicyDetail.Visible = true;
                }
                //CHG0116140 - Payment Restricition - BEGIN - Set the visibility for Payment Restriction label to false
                Label payRestrictMessage = (Label)this.NamingContainer.FindControl("lblPaymentRestrict");
                if (string.IsNullOrEmpty(payRestrictMessage.Text))
                {
                    payRestrictMessage.Visible = false;
                }
                //{
                //    payRestrictMessage.Visible = true;
                //}
                //CHG0116140 - Payment Restricition - END - Set the visibility for Payment Restriction label to false

                UserControl UC_UnEnrollCC = (UserControl)this.NamingContainer.FindControl("UnEnrollCC");
                UserControl UC_UnEnrollEC = (UserControl)this.NamingContainer.FindControl("UnEnrollECheck");
                if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._IsEnrolled.ToUpper().Equals(CSAAWeb.Constants.PC_POL_ENROLL_STATUS.ToUpper()) && UC_UnEnrollCC != null)
                {
                    if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._EnrolledMethod.Equals(Config.Setting(CSAAWeb.Constants.PC_CreditCard_Code)))
                    {
                        UC_UnEnrollCC.Visible = true;
                    }
                    else
                    {
                        UC_UnEnrollEC.Visible = true;
                    }
                    if (selectPayment != null)
                    {
                        selectPayment.Visible = false;
                    }
                }

                if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._IsEnrolled.ToUpper().Equals(CSAAWeb.Constants.PC_POL_NOTENROLL_STATUS.ToUpper()))
                {
                    if (selectPayment != null)
                    {
                        selectPayment.Visible = true;
                        DropDownList dropdown_PaymentType = (DropDownList)selectPayment.FindControl("_PaymentType");
                        dropdown_PaymentType.Enabled = true;
                        if (dropdown_PaymentType != null)
                        {
                            dropdown_PaymentType.SelectedIndex = 0;
                        }
                    }
                }
                //MAIG - CH22 - BEGIN - Added logic to display the Payment Type and hide the Grid view if it is null
            }
            else
            {
                if (selectPayment != null)
                {
                    selectPayment.Visible = true;
                    DropDownList dropdown_PaymentType = (DropDownList)selectPayment.FindControl("_PaymentType");
                    dropdown_PaymentType.Enabled = true;
                    if (dropdown_PaymentType != null)
                    {
                        dropdown_PaymentType.SelectedIndex = 0;
                    }
                }
                if (PH_PolicyDetail != null)
                {
                    PH_PolicyDetail.Visible = false;
                }
            }
            //MAIG - CH22 - END - Added logic to display the Payment Type and hide the Grid view if it is null

        }

        #endregion
        //MAIG - CH23 - BEGIN - Added logic to restrict the max digit for AMEX card and added functionality for changing the Payment method for Redirection flow
        protected void _CardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ExpireMonth.SelectedValue = "";
            _ExpireYear.SelectedValue = "";
            _CardNumber.Text = "";
            if (_CardType.SelectedItem.Text.ToUpper().Equals(Constants.PC_CRD_TYPE_AMEX_EXPANSION))
            {
                _CardNumber.MaxLength = 15;
            }
            else
            {
                _CardNumber.MaxLength = 16;
            }
        }

        /////// MAIG - Start - 09112014
        protected void lnkRecRedirectChangePayment_Click(object sender, EventArgs e)
        {
            try
            {
                UserControl UC_PaymentMethod = (UserControl)this.NamingContainer.FindControl("PaymentMethod");
                if (UC_PaymentMethod != null)
                {
                    UC_PaymentMethod.Visible = true;
                }
                DropDownList dropdown_PaymentType = (DropDownList)UC_PaymentMethod.FindControl("_PaymentType");
                if (dropdown_PaymentType != null)
                {
                    dropdown_PaymentType.SelectedIndex = 0;
                }
                ClearFields();
                dropdown_PaymentType.Enabled = true;
                ViewState["ResetCCCheck"] = true;
                EnableAllFields();
                UserControl UC_CCEnroll = (UserControl)this.NamingContainer.FindControl("EnrollCC");
                if (UC_CCEnroll != null)
                {
                    UC_CCEnroll.Visible = false;
                }

                /////// Hiding the Change Payment link button
                tblRecRedirectLnkButtons.Visible = false;

                ////// Nullify the token (that came from Payments) - since the User is changing the Payment method for Enrollment
                ViewState[Constants.PMT_RECURRING_CCECTOKEN] = null;

            }
            catch (Exception ex)
            {
                Logger.Log("Exception occurred in lnkRecRedirectChangePayment_Click method" + ex.ToString());
            }
        }
        /////// MAIG - End - 09112014

        /////// MAIG - Start - 09112014
        private void EnableAllFields()
        {
            ////// Enabling back all fields to allow changing the Payment details
            _CardType.Enabled = true;
            //CHGXXXXXX - Server Upgrade 4.1 - Updated text box to enable Card number in Payment Recurring Redirection flow - Start
            __CardMasked.Enabled = true;
            //CHGXXXXXX - Server Upgrade 4.1 - Updated text box to enable Card number in Payment Recurring Redirection flow - End
            _ExpireMonth.Enabled = true;
            _ExpireYear.Enabled = true;
            _Name.Enabled = true;
            _txtZipCode.Enabled = true;
            _txtEmailAddress.Enabled = true;

        }
        /////// MAIG - End - 09112014

        //MAIG - CH23 - END - Added logic to restrict the max digit for AMEX card and added functionality for changing the Payment method for Redirection flow

    }
}
