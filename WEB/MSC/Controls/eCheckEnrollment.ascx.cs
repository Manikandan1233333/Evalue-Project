/*
 * HISTORY
 *	PC Phase II changes - Added the page as part of Enrollment changes.
 *	Created by: Cognizant on 4/2/2013
 *  Decsription: This is a new file added as part of Payment Central Phase II.
 *  PC Security Defect Fix -CH1 - Modified the below code to assign the correct reason code and date in the process of enrollment/ modify enrollment
 *  CHG0072116 - Cancel button Fix- Added the below method to Navigate back to EC View details/Payment method selection when the cancel button is clicked.
 *  T&C Changes CH1 - Added the below code to assign the policy number and product type value to the arguments
 *  MAIG - CH1 - Added the below variables to support Invalid Policy flow
 *  MAIG - CH2 - Added the logic to hide the policy grid if the entered policy number is invalid
 *  MAIG - CH3 - Added logic to skip initialization in case of Redirection from Payment
 *  MAIG - CH4 - Added logic to enable ECheckEnrollment and Reset the Terms and condition
 *  MAIG - CH5 - Added logic to check if the policy entered is an invalid policy then get the value of first name, last name and zip to store the deatails to db.
 *  MAIG - CH6 - Modified to get the bank name From PC Service
 *  MAIG - CH7 - Added new method to get the bank name From PC Service
 *  MAIG - CH8 - Added new method to display the Recurring information pre-populated
 *  MAIG - CH9 - Commented/Modified the code to pass the updated details to Terms and Condition page
 *  MAIG - CH10 - Added code to check if the entered policy is an invalid, if so then get the First name, Last name and Zip to store in Db.
 *  MAIG - CH11 - Commented unnecessary code and Initialized variable as part of MAIG
 *  MAIG - CH12 - Added logic to skip Token Generation Service in case of Payment Flow to recurring
 *  MAIG - CH13 - Added logic to fetch the Agency details and store the invalid policy details to DB. 
 *  MAIG - CH14 - Added logic to update the status of the invalid policy details to DB.  
 *  MAIG - CH15 - Added logic to pass the Blaze Rules to Recurring Confirmation page
 *  MAIG - CH16 - Hidden the Policy Search Details Grid for Invalid Policy
 *  MAIG - CH17 - Hidden the Policy Search Details Grid for Invalid Policy
 *  MAIG - CH18 - Added logic to pass the Blaze Rules to Enrollment Service for a valid policy
 *  MAIG - CH19 - Added logic to pass the Source System for both valid and Invalid policy
 *  MAIG - CH20 - Added logic to pass the Email ID to Enrollment Service if it is not null 
 *  MAIG - CH21 - Added logic to check if the entered policy is a valid policy and else condition added
 *  MAIG - CH22 - Added logic to change the Payment method for Redirection flow
 *  MAIG - CH23 - Modified the below logic to check if the Page is Valid
 *  MAIG - CH24 - Modified code to display the Error Code only once
 *  MAIG - CH25 - Modified code to display the Error Code only once
 * CHG0112662 - Appended the condition for error code 300 to the existing fault exception handling, Removed Error Code in actual error message & Payment Enrollment SOA Service changes
 * CHG0123270 - To disable ACH Card number in Payment Recurring Redirection Flow
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
    using System.Collections.Generic;
    using AuthenticationClasses;
    using OrderClassesII;
    using RecordEnrollment;
    using CSAAWeb.AppLogger;
    using System.ServiceModel;
    using System.Xml;
    using System.Text.RegularExpressions;
    using System.Linq;
    using PC_PaymentService;
    using AuthenticationClasses.WebService;
    #endregion
    /// <summary>
    ///		Code for ECheck Controls.
    /// </summary>
    /// 

    public partial class eCheckEnrollment : ValidatingUserControl
    {
        private Boolean _isValid = true;
        //MAIG - CH1 - BEGIN - Added the below variables to support Invalid Policy flow
        /// <summary>
        // Included MAIG Iteration2 - start
        private string Invalid_FirstName;
        private string Invalid_LastName;
        private string Invalid_MailingZip;
        private Authentication authObj = new Authentication();
        private DataSet dsRepDODetails = new DataSet();
        //MAIG - CH1 - END - Added the below variables to support Invalid Policy flow

        /// </summary>

        #region WebControls
        protected CSAAWeb.WebControls.Validator lblbankid;
        protected System.Web.UI.WebControls.DropDownList _AccountType;
        protected CSAAWeb.WebControls.Validator lblAccounttype;
        protected System.Web.UI.WebControls.TextBox _Accountno;
        protected System.Web.UI.WebControls.TextBox _ReAccountNo;
        protected System.Web.UI.WebControls.TextBox _Bankid;
        protected CSAAWeb.WebControls.Validator lblAccountno;
        #endregion

        #region Public properties
        ///<summary>Billing Bank Account number.</summary>
        public string BankAcntNo { get { return _Accountno.Text.Trim(); } set { _Accountno.Text = value; } }
        ///<summary>Billing Bank Re Enter Account number.</summary>
        public string ReBankAcntNo { get { return _ReAccountNo.Text.Trim(); } set { _ReAccountNo.Text = value; } }
        ///<summary>Billing Bank Routine number</summary>
        public string BankId { get { return _Bankid.Text.Trim(); } set { _Bankid.Text = value; } }
        ///<summary>Billing Account type.</summary>
        public string BankAcntType { get { return ListC.GetListValue(_AccountType); } set { ListC.SetListIndex(_AccountType, value); } }

        public string Name { get { return _txtCustomerName.Text.Trim(); } set { _txtCustomerName.Text = value; } }

        public string EmailAddress { get { return _txtEmailAddress.Text.Trim(); } set { _txtEmailAddress.Text=value;} }

        #endregion

        public eCheckEnrollment()
            : base()
        {
            this.Init += new System.EventHandler(this.InitializeLBs);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }
        /// <summary>
        /// Page Load Event Overrided
        /// </summary>
        /// <param name="sender">Sender Object</param>
        /// <param name="e">Event Argument</param>
        protected override void Page_Load(object sender, System.EventArgs e)
        {
            //MAIG - CH2 - BEGIN - Added the logic to hide the policy grid if the entered policy number is invalid
            if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow) != null)
            {
                PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl("policySearchDetails");
                if (PH_PolicyDetail != null)
                {
                    PH_PolicyDetail.Visible = false;
                }
            }
            //MAIG - CH2 - END - Added the logic to hide the policy grid if the entered policy number is invalid
            if (!Page.IsPostBack)
            {
                //MAIG - CH3 - BEGIN - Added logic to skip initialization in case of Redirection from Payment
                if (ViewState[Constants.PMT_RECURRING_CCECTOKEN] == null)
                {
                    _Bankid.Text = string.Empty;
                    displayBankName.Text = string.Empty;
                    _txtCustomerName.Text = string.Empty;
                    _ReAccountNo.Text = string.Empty;
                    _txtEmailAddress.Text = string.Empty;
                    _Accountno.Text = string.Empty;
                    lblErrMessage.Text = string.Empty;
                }
                //MAIG - CH3 - END - Added logic to skip initialization in case of Redirection from Payment
            }

        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion


        #region LoadEvents

        private void Initialize()
        {

        }

        ///<summary>Called from init event.</summary>
        private void InitializeLBs(object sender, System.EventArgs e)
        {

        }
        public void DisplayTerms()
        {
            AuthenticationClasses.WebService.Authentication authObj = new AuthenticationClasses.WebService.Authentication();
            OrderClassesII.IssueDirectPaymentWrapper issueDirect = new IssueDirectPaymentWrapper();
            try
            {
                DataSet ds = new DataSet();
                string DO = string.Empty;
                ds = authObj.GetRepIDDO(HttpContext.Current.User.Identity.Name.ToString());
                if (ds.Tables[0].Rows.Count > 0)
                {
                    DO = (ds.Tables[0].Rows[0][Constants.PC_DO_ID].ToString()).Trim();
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
                        ContactCenterUser.Visible = false;
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

        /// <summary>
        /// Clear fields
        /// </summary>
        public void ClearFields()
        {
            _Bankid.Text = string.Empty;
            displayBankName.Text = string.Empty;
            _txtCustomerName.Text = string.Empty;
            _ReAccountNo.Text = string.Empty;
            _txtEmailAddress.Text = string.Empty;
            _Accountno.Text = string.Empty;
            lblErrMessage.Text = string.Empty;

            //MAIG - CH4 - BEGIN - Added logic to enable ECheckEnrollment and Reset the Terms and condition
            ImgBtnEcheckEnrollment.Enabled = true;
            chkF2Fuser.Checked = false;
            //MAIG - CH4 - END - Added logic to enable ECheckEnrollment and Reset the Terms and condition
        }
        /// <summary>
        /// Validate bank id Length
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void ValidbankidLength(Object source, ValidatorEventArgs args)
        {
            if (!string.IsNullOrEmpty(BankId))
            {
                args.IsValid = (BankId.Length == 9);
                if (!args.IsValid)
                {
                    vldBankidnumber.MarkInvalid();
                    vldBankidnumber.ErrorMessage = string.Empty;
                    _isValid = false;


                }

            }

        }

        /// <summary>
        /// validate name length
        /// </summary>
        protected void ValidName(object source, ValidatorEventArgs args)
        {
            if (!string.IsNullOrEmpty(Name))
            {
                Regex R = new Regex(Constants.PC_CC_NUMERIC_REGX, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                args.IsValid = !R.IsMatch(this.Name);
                if (!args.IsValid)
                {
                    vldAccHolderName.MarkInvalid();
                    vldAccHolderName.ErrorMessage = Constants.PC_ERR_INVALID_ACCHOLDER_NAME;
                    EcheckReqValidator.ErrorMessage = string.Empty;
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
                    EcheckReqValidator.ErrorMessage = string.Empty;
                    _isValid = false;

                }

            }

        }

        /// <summary>
        /// validate account number length when the corresponding label validator is fired.
        /// </summary>
        protected void ValidAccountLength(Object source, ValidatorEventArgs args)
        {
            if (!string.IsNullOrEmpty(BankAcntNo))
            {
                if (BankAcntNo.Trim().Length < 4 || BankAcntNo.Trim().Length > 17)
                {
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
                if (!args.IsValid)
                {
                    vldAccNumber.MarkInvalid();

                    vldAccNumber.ErrorMessage = string.Empty;
                    EcheckReqValidator.ErrorMessage = string.Empty;
                    _isValid = false;

                }

            }
            if (!string.IsNullOrEmpty(ReBankAcntNo))
            {
                if (ReBankAcntNo.Trim().Length < 4 || ReBankAcntNo.Trim().Length > 17)
                {
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
                if (!args.IsValid)
                {
                    vldReAccNumber.MarkInvalid();

                    vldReAccNumber.ErrorMessage = string.Empty;
                    EcheckReqValidator.ErrorMessage = string.Empty;
                    _isValid = false;

                }

            }
        }

        /// <summary>
        /// validate account number is numeric
        /// </summary>
        protected void ValidateNumeric(Object source, ValidatorEventArgs args)
        {
            if (!string.IsNullOrEmpty(BankAcntNo))
            {
                args.IsValid = Validate.IsAllNumeric(BankAcntNo);
                if (!args.IsValid)
                {
                    vldAccNumber.MarkInvalid();
                    vldAccNumber.ErrorMessage = string.Empty;
                    EcheckReqValidator.ErrorMessage = string.Empty;
                    _isValid = false;

                }


            }
            if (!string.IsNullOrEmpty(ReBankAcntNo))
            {
                args.IsValid = Validate.IsAllNumeric(ReBankAcntNo);
                if (!args.IsValid)
                {
                    vldReAccNumber.MarkInvalid();
                    vldReAccNumber.ErrorMessage = string.Empty;
                    EcheckReqValidator.ErrorMessage = string.Empty;
                    _isValid = false;

                }


            }
        }

        /// <summary>
        /// validate routing number
        /// </summary>
        protected void ValidateBankIdSequence(Object source, ValidatorEventArgs args)
        {
            if (!string.IsNullOrEmpty(BankId.Trim()))
            {
                if ((BankId.Length == 9) && (CSAAWeb.Validate.IsAllNumeric(BankId)))
                    args.IsValid = !(BankId.Substring(8, 1) != RoutingNumberCheckDigit(BankId.Substring(0, 8)));
                else args.IsValid = false;

                if (!args.IsValid)
                {

                    vldBankidnumber.MarkInvalid();
                    vldBankidnumber.ErrorMessage = string.Empty;
                    EcheckReqValidator.ErrorMessage = string.Empty;
                    _isValid = false;

                }

            }
        }


        private static string RoutingNumberCheckDigit(string transitNumber)
        {

            Int64 lRoutingNumSum = 0;
            Int16 iNextHighestMultiple = 0;
            if (transitNumber.Trim().Length != 8)
                transitNumber = FormatField(transitNumber, 8, Constants.PC_EC_RNUMBER_CHKDIGIT);
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
        public static Int64 RoundToNextNumber(Decimal inNum)
        {
            Int64 outNum = 0;
            outNum = Convert.ToInt64(System.Math.Round(inNum));

            if (outNum < inNum)
                outNum++;

            return outNum;

        }
        public static string FormatField(string inStr, Int16 size, string fldName)
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


        /// <summary>
        /// validate the value of bank id is zero
        /// </summary>
        protected void ValidateBankIdZero(Object source, ValidatorEventArgs args)
        {
            if (!string.IsNullOrEmpty(BankId))
            {
                args.IsValid = !IsAllZeros(BankId);
                if (!args.IsValid)
                {
                    vldBankidnumber.MarkInvalid();
                    vldBankidnumber.ErrorMessage = string.Empty;
                    EcheckReqValidator.ErrorMessage = string.Empty;
                    _isValid = false;


                }

            }
        }

        /// <summary>
        /// Verify the Re-Entered account is same as account number
        /// </summary>
        protected void VerifyAccountNumber(Object source, ValidatorEventArgs args)
        {
            if (_ReAccountNo.Text.ToString().Equals(_Accountno.Text.ToString()))
            {
                args.IsValid = true;
            }
            else
            {
                args.IsValid = false;
                vldReAccNumber.MarkInvalid();
                vldReAccNumber.ErrorMessage = Constants.PC_EC_ERR_INVALID_REENTERED_AC;
                EcheckReqValidator.ErrorMessage = string.Empty;
                _isValid = false;

            }
        }

        public static bool IsAllZeros(string s)
        {
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

        /// <summary>
        /// Validate required controls values
        /// </summary>
        protected void ReqValCheck1(Object source, ValidatorEventArgs args)
        {
            if (string.IsNullOrEmpty(_Accountno.Text.Trim()))
            {
                args.IsValid = false;
                vldAccNumber.MarkInvalid();
                vldAccNumber.ErrorMessage = Constants.PC_EC_ERR_ACNUMBER_REQ;
                EcheckReqValidator.ErrorMessage = string.Empty;
                _isValid = false;

            }
            if (string.IsNullOrEmpty(_ReAccountNo.Text.Trim()))
            {
                args.IsValid = false;
                vldReAccNumber.MarkInvalid();
                vldReAccNumber.ErrorMessage = Constants.PC_EC_ERR_REENTEREDAC_REQ;
                EcheckReqValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }
            if (string.IsNullOrEmpty(_Bankid.Text.Trim()))
            {
                args.IsValid = false;
                vldBankidnumber.MarkInvalid();
                vldBankidnumber.ErrorMessage = Constants.PC_EC_ERR_BANK_ID_REQ;
                EcheckReqValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }
            if (string.IsNullOrEmpty(_txtCustomerName.Text.Trim()))
            {
                args.IsValid = false;
                vldAccHolderName.MarkInvalid();
                vldAccHolderName.ErrorMessage = Constants.PC_EC_ERR_ACCHOLDER_REQ;
                EcheckReqValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }
            if (!chkF2Fuser.Checked)
            {
                args.IsValid = false;
                vldF2F.MarkInvalid();
                vldF2F.ErrorMessage = Constants.PC_ERR_CHK_TC;
                EcheckReqValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }
        }
        #endregion

        #region BanknamePopulate
        /// <summary>
        /// Change  text of the bank name label when routing number's text is changed.
        /// </summary>
        protected void Bankid_TextChanged(object sender, EventArgs e)
        {
            PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl(Constants.PC_CNTRL_NAME);

            //MAIG - CH5 - BEGIN - Added logic to check if the policy entered is an invalid policy then get the value of first name, last name and zip to store the deatails to db.
            if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFlow != null)
            {
                Invalid_FirstName = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFName.ToString();
                Invalid_LastName = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyLName.ToString();
                Invalid_MailingZip = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyMZip.ToString();
            }
            else
            {
                //MAIG - CH5 - END - Added logic to check if the policy entered is an invalid policy then get the value of first name, last name and zip to store the deatails to db.
                if (PH_PolicyDetail != null)
                {
                    PH_PolicyDetail.Visible = true;
                }
            }

            //MAIG - CH6 - BEGIN - Modified to get the bank name From PC Service
            BankNameRetrieval(BankId);
            //MAIG - CH6 - END - Modified to get the bank name From PC Service
        }

        //MAIG - CH7 - BEGIN - Added new method to get the bank name From PC Service
        private void BankNameRetrieval(string bankID)
        {
            bool flag = true;
            List<string> response = new List<string>();
            if (BankId.Trim().Length < 9 || BankId.Trim().Length > 9)
                flag = false;
            if ((BankId.Trim().Length == 9) && (CSAAWeb.Validate.IsAllNumeric(BankId.Trim())))
                flag = !(BankId.Substring(8, 1) != RoutingNumberCheckDigit(BankId.Substring(0, 8)));
            if (flag)
            {
                PCEnrollmentMapping pCEnroll = new PCEnrollmentMapping();
                response = pCEnroll.PCBankName(_Bankid.Text);
                if (response[0].Equals(CSAAWeb.Constants.PC_SUCESS.ToUpper()))
                {
                    displayBankName.Text = response[1].ToString();
                    lblErrMessage.Visible = false;
                }
                else if (!string.IsNullOrEmpty(_Bankid.Text.Trim()))
                {
                    lblErrMessage.Visible = true;
                    //MAIG - CH24 - BEGIN - Modified code to display the Error Code only once
                    //lblErrMessage.Text = response[0].ToString() + " - " + response[1].ToString();
                    lblErrMessage.Text = response[1].ToString();
                    //MAIG - CH24 - END - Modified code to display the Error Code only once
                }
            }
            else
            {
                displayBankName.Text = string.Empty;
            }

            if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow) != null)
            {
                PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl(Constants.PC_CNTRL_NAME);

                if (PH_PolicyDetail != null)
                {
                    PH_PolicyDetail.Visible = false;
                }
            }
        }
        //MAIG - CH7 - END - Added new method to get the bank name From PC Service

        #endregion

        //MAIG - CH8 - BEGIN - Added new method to display the Recurring information pre-populated
        public void PopulateRecurringDetailsFromPayment(List<string> data)
        {
            _Bankid.Text = data[0].ToString();
            _txtCustomerName.Text = data[1].ToString();
            _ReAccountNo.Text = data[2].ToString();
            _Accountno.Text = data[2].ToString();
            BankNameRetrieval(_Bankid.Text.Trim());
            if (data[3].ToString().ToUpper().Equals("C"))
            {
                rbCheckingAccount.Checked = true;
            }
            else
            {
                rbSavingsAccount.Checked = true;
            }
            if (ViewState[Constants.PMT_RECURRING_CCECTOKEN] == null)
            {
                ViewState[Constants.PMT_RECURRING_CCECTOKEN] = data[4].ToString();
            }
            _txtEmailAddress.Text = data[5].ToString();


            /////// MAIG - Start - 09112014
            /////// Showing the Change Payment link button
            tblRecRedirectLnkButtons.Visible = true;

            ////// Diasbling all fields to restrict from changing the Payment details
            _Bankid.Enabled = false;
            _txtCustomerName.Enabled = false;
            // CHG0123270 - To disable ACH Masked Card number in Payment Recurring Redirection Flow - Start
            //_ReAccountNo.Enabled = false;
            //_Accountno.Enabled = false;
            __AccountnoMasked.Enabled = false;
            __ReAccountNoCardMasked.Enabled = false;
            //CHG0123270 - To disable ACH Masked Card number in Payment Recurring Redirection Flow - End
            rbCheckingAccount.Enabled = false;
            rbSavingsAccount.Enabled = false;
            /////_txtEmailAddress.Enabled = false;
            /////// MAIG - End - 09112014

        }
        //MAIG - CH8 - END - Added new method to display the Recurring information pre-populated

        protected void EcTermsAndConditions_btn_OnClick(object sender, EventArgs e)
        {
            try
            {
                //MAIG - CH9 - BEGIN - Commented/Modified the code to pass the updated details to Terms and Condition page
                string insFName = string.Empty;

                PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl(Constants.PC_CNTRL_NAME);

                // Included MAIG Iteration2 - start
                if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFlow != null)
                {
                    ////Invalid_FirstName = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFName.ToString();
                    ////Invalid_LastName = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyLName.ToString();
                    ////Invalid_MailingZip = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyMZip.ToString();
                    insFName = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFName.ToString() + " "
                                    + ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyLName.ToString();
                }
                else
                {
                    insFName = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InsuredFullName.ToString();
                    if (PH_PolicyDetail != null)
                    {
                        PH_PolicyDetail.Visible = true;
                    }
                }
                // Included MAIG Iteration2 - end

                ////string accountType = (rbCheckingAccount.Checked) ? Constants.PC_EC_CHKG : Constants.PC_EC_SAVG;
                ////string bankName = (!string.IsNullOrEmpty(displayBankName.Text.ToString())?displayBankName.Text.ToString():string.Empty).ToString();

                ////string routingNo = (!string.IsNullOrEmpty(_Bankid.Text.ToString())?_Bankid.Text.ToString():string.Empty).ToString();
                ////string accountNo = (!string.IsNullOrEmpty(_Accountno.Text)?_Accountno.Text:string.Empty).ToString();
                ////string name = (!string.IsNullOrEmpty(_txtCustomerName.Text.ToString()) ? _txtCustomerName.Text.ToString() : string.Empty).ToString();
                MSC.Encryption.EncryptQueryString args = new MSC.Encryption.EncryptQueryString();
                ////args[Constants.PC_TC_ARG1] = Constants.PC_EC_PAYMNT_TYPE;
                ////args[Constants.PC_TC_ARG2] = accountType;
                ////args[Constants.PC_TC_ARG3] = bankName;
                ////args[Constants.PC_TC_ARG4] = routingNo;
                ////args[Constants.PC_TC_ARG5] = accountNo;
                args[Constants.PC_TC_ARG2] = insFName;
                //T&C Changes CH1 - START - Added the below code to assign the policy number and product type value to the arguments
                args[Constants.PC_TC_ARG1] = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).PolicyNumber.ToString();
                ////args[Constants.PC_TC_ARG8] = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ProductType_PC_Text.ToString();
                //T&C Changes CH1 - END - Added the below code to assign the policy number and product type value to the arguments
                //MAIG - CH9 - END - Commented/Modified the code to pass the updated details to Terms and Condition page
                string url = String.Format(Constants.PC_TC_URL, args.ToString());
                Page.ClientScript.RegisterStartupScript(GetType(), Constants.PC_TC_SCRIPT_KEY, Constants.PC_TC_SCRIPT_ONE + url + Constants.PC_TC_SCRIPT_TWO);
            }
            catch (Exception exception)
            {
                Logger.Name = Constants.PC_TC_LOG_NAME;
                Logger.Log(exception);
            }



        }

        #region Echeck Enroll Click Event
        protected void ImgBtnEcheckEnrollment_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl(Constants.PC_CNTRL_NAME);

            //MAIG - CH10 - BEGIN - Added code to check if the entered policy is an invalid, if so then get the First name, Last name and Zip to store in Db.
            if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFlow != null)
            {
                Invalid_FirstName = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFName.ToString();
                Invalid_LastName = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyLName.ToString();
                Invalid_MailingZip = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyMZip.ToString();
            }
            else
            {
                //MAIG - CH10 - END - Added code to check if the entered policy is an invalid, if so then get the First name, Last name and Zip to store in Db.
                if (PH_PolicyDetail != null)
                {
                    PH_PolicyDetail.Visible = true;
                }
            }


            string accounttype = string.Empty;
            string token = string.Empty;
            //MAIG - CH23 - BEGIN - Modified the below logic to check if the Page is Valid
            if (_isValid && Page.IsValid)
            //MAIG - CH23 - END - Modified the below logic to check if the Page is Valid
            {
                try
                {
                    //MAIG - CH11 - BEGIN - Commented unnecessary code and Initialized variable as part of MAIG
                    ImgBtnEcheckEnrollment.Enabled = false;
                    // Included MAIG Iteration2 - start - for getting the UserInfo details
                    //OrderClasses.Service.Order authUserObj = new OrderClasses.Service.Order();
                    //UserInfo userInfoObj = new UserInfo();

                    //userInfoObj = authUserObj.Authenticate(HttpContext.Current.User.Identity.Name, string.Empty, CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL);
                    // Included MAIG Iteration2 - end - for getting the UserInfo details

                    List<string> response = new List<string>();
                    string paymentType = string.Empty;
                    OrderClasses.OrderInfo orderInfo = new OrderClasses.OrderInfo();
                    orderInfo.User = new UserInfo();
                    SessionInfo sessionInfo = new SessionInfo();
                    IssueDirectPaymentWrapper ActObj = new IssueDirectPaymentWrapper();
                    string agentIDSR = string.Empty;


                    DataConnection dsObj = new DataConnection();
                    //MAIG - CH11 - END - Commented unnecessary code and Initialized variable as part of MAIG

                    //MAIG - CH12 - BEGIN - Added logic to skip Token Generation Service in case of Payment Flow to recurring
                    if (ViewState[Constants.PMT_RECURRING_CCECTOKEN] == null)
                    {

                        orderInfo.S = new SessionInfo();
                        orderInfo.S.AppName = CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL;
                        orderInfo.User.UserId = HttpContext.Current.User.Identity.Name;


                        orderInfo.Detail = new OrderClasses.OrderDetailInfo();
                        orderInfo.Detail.PaymentType = 5;
                        orderInfo.Addresses = new OrderClasses.ArrayOfAddressInfo();
                        OrderClasses.AddressInfo addressInfo = new OrderClasses.AddressInfo();
                        addressInfo.FirstName = _txtCustomerName.Text;
                        addressInfo.LastName = " ";
                        addressInfo.AddressType = OrderClasses.AddressInfoType.BillTo;
                        orderInfo.Addresses.Add(addressInfo);
                        orderInfo.echeck = new OrderClasses.eCheckInfo();
                        if (rbCheckingAccount.Checked)
                        {
                            accounttype = Constants.PC_EC_CHKG;
                            orderInfo.echeck.BankAcntType = Constants.PC_EC_CHKG_PREFIX;
                            paymentType = Config.Setting("EFT_CHECKING");
                        }
                        else
                        {
                            accounttype = Constants.PC_EC_SAVG;
                            orderInfo.echeck.BankAcntType = Constants.PC_EC_SAVG_PREFIX;
                            paymentType = Config.Setting("EFT_SAVING");
                        }
                        orderInfo.echeck.BankAcntNo = _Accountno.Text;
                        orderInfo.echeck.BankId = _Bankid.Text;

                        MaintainPaymentAccountRequest inputRequest = new MaintainPaymentAccountRequest();
                        MaintainPaymentAccountResponse tokenResponse = new MaintainPaymentAccountResponse();
                        inputRequest = ActObj.Tokenmapping(orderInfo, sessionInfo);
                        //PC Security Defect Fix -CH2 - Added the below code to set the save for future flag to True for enrollment
                        inputRequest.MaintainPaymentAccountRequest1.saveForFuture = true;
                        if (inputRequest != null)
                        {
                            //Invokes the Payment Token Service
                            tokenResponse = ActObj.InvokePaymentTokenService(inputRequest);
                        }
                        ////// This Loop need to be removed - This is not necessary at all
                        if (tokenResponse != null)
                        {
                            if (rbCheckingAccount.Checked)
                            {
                                accounttype = Config.Setting("EFT_CHECKING");
                            }
                            else
                            {
                                accounttype = Config.Setting("EFT_SAVING");
                            }
                            token = tokenResponse.MaintainPaymentAccountResponse1.paymentAccountToken;
                        }
                        else
                        {
                            //Added the below code to set Error flag if response object is null
                            Logger.Log(CSAAWeb.Constants.PC_ERR_RESPONSE_NULL);
                            lblErrMessage.Visible = true;
                            lblErrMessage.Text = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                        }
                    }
                    else
                    {
                        if (rbCheckingAccount.Checked)
                        {
                            accounttype = Constants.PC_EC_CHKG;
                            orderInfo.echeck.BankAcntType = Constants.PC_EC_CHKG_PREFIX;
                            paymentType = Config.Setting("EFT_CHECKING");
                        }
                        else
                        {
                            accounttype = Constants.PC_EC_SAVG;
                            orderInfo.echeck.BankAcntType = Constants.PC_EC_SAVG_PREFIX;
                            paymentType = Config.Setting("EFT_SAVING");
                        }
                        token = ViewState[Constants.PMT_RECURRING_CCECTOKEN].ToString();
                    }
                    //MAIG - CH12 - END - Added logic to skip Token Generation Service in case of Payment Flow to recurring
                    PCEnrollmentMapping pCEnroll = new PCEnrollmentMapping();
                    //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Renamed the Autpay Enrollment request to Payment Enrollment request
                    RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest statusRequest = new RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest();
                    statusRequest = CreatePaymentStatusRequest(token, paymentType);
                    //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Renamed the Autpay Enrollment request to Payment Enrollment request

                    //MAIG - CH13 - BEGIN - Added logic to fetch the Agency details and store the invalid policy details to DB. 
                    if (string.IsNullOrEmpty(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoAgencyID) || ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).ToString().Equals("0"))
                        statusRequest.agency = string.Empty;
                    else
                        statusRequest.agency = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoAgencyID.ToString();

                    ////////if (string.IsNullOrEmpty(userInfoObj.RepId.ToString()) || userInfoObj.RepId.ToString().Equals("0"))
                    ////////    statusRequest.agentIdentifier = string.Empty;
                    ////////else
                    ////////{
                    ////////    statusRequest.agentIdentifier = userInfoObj.RepId.ToString();
                    ////////    agentIDSR = userInfoObj.RepId.ToString();
                    ////////} 

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

                        ////Invalid_FirstName = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFName.ToString();
                        ////Invalid_LastName = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyLName.ToString();
                        ////Invalid_MailingZip = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyMZip.ToString();                       

                        //// setting agentID to 0 for time being 
                        ///statusRequest.agentIdentifier = "0";

                        dsObj.PT_Update_InvalidPolicy(statusRequest.policyInfo.policyNumber.ToString(), 0, agentIDSR,
                                                            Invalid_LastName, Invalid_FirstName, Invalid_MailingZip, Constants.PC_INVLD_ENROLL_INACTIVE, statusRequest.policyInfo.type,
                                                                       statusRequest.policyInfo.dataSource, 0);
                    }
                    //MAIG - CH13 - END - Added logic to fetch the Agency details and store the invalid policy details to DB. 

                    //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Renamed the method
                    response = pCEnroll.PCPaymentEnrollService(statusRequest);
                    //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Renamed the method
                    if (response[0].Equals(CSAAWeb.Constants.PC_SUCESS.ToUpper()))
                    {
                        Logger.Log(Constants.PC_LOG_STATUS_REQUEST_SUCCESS + statusRequest.policyInfo.policyNumber + Constants.PC_LOG_CNFRM_NUMBER + response[1].ToString());
                        Context.Items.Add(Constants.PC_CNFRMNUMBER, response[1].ToString());

                        //MAIG - CH14 - BEGIN - Added logic to update the status of the invalid policy details to DB. 
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
                        //MAIG - CH14 - END - Added logic to update the status of the invalid policy details to DB. 

                        Context.Items.Add(Constants.PC_EC_ACC_NUMBER, Constants.PC_MASK_TXT + _Accountno.Text.Substring(_Accountno.Text.Length - 4));
                        Context.Items.Add(Constants.PC_EC_BANK_NAME, displayBankName.Text);
                        Context.Items.Add(Constants.PC_CUST_NAME, _txtCustomerName.Text);
                        Context.Items.Add(Constants.PC_EC_ROUTING_NUMBER, _Bankid.Text);
                        Context.Items.Add(Constants.PC_EC_ACCOUNT_TYPE, accounttype);
                        //MAIG - CH15 - BEGIN - Added logic to pass the Blaze Rules to Recurring Confirmation page
                        if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow == null)
                        {
                            Context.Items.Add(CSAAWeb.Constants.PC_BILL_PLAN, ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).BillingPlan);
                            Context.Items.Add(CSAAWeb.Constants.PC_IS_ENROLLED, ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._IsEnrolled);
                            Context.Items.Add(Constants.PC_VLD_MDFY_ENROLLMENT, ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Modifyenrollment);
                            Context.Items.Add(Constants.PC_VLD_ENROLLMENT, ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_enrollment);
                        }
                        //MAIG - CH15 - END - Added logic to pass the Blaze Rules to Recurring Confirmation page
                        Context.Items.Add(Constants.PC_PAYMENT_TYPE, "EC");
                        Context.Items.Add(Constants.PC_EMAIL_ID, _txtEmailAddress.Text);
                        Server.Transfer(CSAAWeb.Constants.PC_RECURR_CNFRM_URL, true);
                    }
                    else
                    {
                        Logger.Log(Constants.PC_LOG_FAIL + statusRequest.policyInfo.policyNumber);
                        lblErrMessage.Visible = true;
                        //MAIG - CH25 - BEGIN - Modified code to display the Error Code only once
                        //lblErrMessage.Text = response[1].ToString() + " (" + response[0].ToString() + ")";
                        lblErrMessage.Text = response[1].ToString();
                        //MAIG - CH25 - END - Modified code to display the Error Code only once

                    }
                    //MAIG - CH16 - BEGIN - Hidden the Policy Search Details Grid for Invalid Policy
                    if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFlow != null)
                    {
                        PH_PolicyDetail.Visible = false;
                    }
                    //MAIG - CH16 - END - Hidden the Policy Search Details Grid for Invalid Policy
                }
                catch (FaultException faultExp)
                {
                    DisplayException(faultExp);
                }
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

                //MAIG - CH17 - BEGIN - Hidden the Policy Search Details Grid for Invalid Policy
                ImgBtnEcheckEnrollment.Enabled = true;
                // Included MAIG Iteration2 - end

                if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow) != null)
                {
                    PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl(Constants.PC_CNTRL_NAME);

                    if (PH_PolicyDetail != null)
                    {
                        PH_PolicyDetail.Visible = false;
                    }
                }
                //MAIG - CH17 - END - Hidden the Policy Search Details Grid for Invalid Policy
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
        private recordAutoPayEnrollmentStatusRequest CreateStatusRequest(string token,string accountType)
        {
            recordAutoPayEnrollmentStatusRequest statusRequest = new recordAutoPayEnrollmentStatusRequest();
            statusRequest.applicationContext = new ApplicationContext();
            statusRequest.applicationContext.application = Config.Setting(CSAAWeb.Constants.PC_ApplicationContext_Payment_Tool);
            statusRequest.applicationContext.address = CSAAWeb.AppLogger.Logger.GetIPAddress();
            statusRequest.userId = HttpContext.Current.User.Identity.Name;
            statusRequest.enrollmentEffectiveDateSpecified = true;

            //MAIG - CH18 - BEGIN - Added logic to pass the Blaze Rules to Enrollment Service for a valid policy
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
            //MAIG - CH18 - END - Added logic to pass the Blaze Rules to Enrollment Service for a valid policy

            statusRequest.policyInfo = new PolicyProductSource();
            statusRequest.policyInfo.policyNumber = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).PolicyNumber;
            statusRequest.policyInfo.type = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ProductType_PC;

            //MAIG - CH19 - BEGIN - Added logic to pass the Source System for both valid and Invalid policy
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
            //MAIG - CH19 - END - Added logic to pass the Source System for both valid and Invalid policy

            statusRequest.paymentItem = new PaymentItemHeader();
            statusRequest.paymentItem.paymentMethod = Config.Setting(CSAAWeb.Constants.PC_ElectronicFund_Code);
            statusRequest.paymentItem.paymentAccountToken = token;
            statusRequest.paymentItem.account = new RecordEnrollment.PaymentAccount();
            statusRequest.paymentItem.account.accountNumber = _Accountno.Text.Substring(_Accountno.Text.Length - 4);
            statusRequest.paymentItem.account.accountHolderName = _txtCustomerName.Text;
            //Req.paymentItem.account.routingNumber = _Bankid.Text.ToString();
            statusRequest.paymentItem.account.ABANumber = _Bankid.Text.ToString();
            statusRequest.paymentItem.account.type = accountType;
            //MAIG - CH20 - BEGIN - Added logic to pass the Email ID to Enrollment Service if it is not null 
            if (!string.IsNullOrEmpty(_txtEmailAddress.Text.ToString().Trim()))
                statusRequest.emailTo = _txtEmailAddress.Text.ToString().Trim();
            //MAIG - CH20 - END - Added logic to pass the Email ID to Enrollment Service if it is not null 
            Logger.Log(Constants.PC_LOG_REQ_SENT + statusRequest.policyInfo.policyNumber);

            return statusRequest;
        }

        /// <summary>
        /// Create Request to enroll/modify the policy number
        /// CHG0112662 - Record Payment Enrollment SOA Service changes - Added this method
        /// </summary>
        private RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest CreatePaymentStatusRequest(string token, string accountType)
        {
            RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest statusRequest = new RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest();
            statusRequest.applicationContext = new RecordPaymentEnrollment.ApplicationContext();
            statusRequest.applicationContext.application = Config.Setting(CSAAWeb.Constants.PC_ApplicationContext_Payment_Tool);
            statusRequest.applicationContext.address = CSAAWeb.AppLogger.Logger.GetIPAddress();
            statusRequest.userId = HttpContext.Current.User.Identity.Name;
            statusRequest.enrollmentEffectiveDateSpecified = true;

            //MAIG - CH18 - BEGIN - Added logic to pass the Blaze Rules to Enrollment Service for a valid policy
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
            //MAIG - CH18 - END - Added logic to pass the Blaze Rules to Enrollment Service for a valid policy

            statusRequest.policyInfo = new RecordPaymentEnrollment.PolicyProductSource();
            statusRequest.policyInfo.policyNumber = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).PolicyNumber;
            statusRequest.policyInfo.type = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ProductType_PC;

            //MAIG - CH19 - BEGIN - Added logic to pass the Source System for both valid and Invalid policy
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
            //MAIG - CH19 - END - Added logic to pass the Source System for both valid and Invalid policy

            statusRequest.paymentItem = new RecordPaymentEnrollment.PaymentItemHeader();
            statusRequest.paymentItem.paymentMethod = Config.Setting(CSAAWeb.Constants.PC_ElectronicFund_Code);
            statusRequest.paymentItem.paymentAccountToken = token;
            statusRequest.paymentItem.account = new RecordPaymentEnrollment.PaymentAccount();
            statusRequest.paymentItem.account.accountNumber = _Accountno.Text.Substring(_Accountno.Text.Length - 4);
            statusRequest.paymentItem.account.accountHolderName = _txtCustomerName.Text;
            //Req.paymentItem.account.routingNumber = _Bankid.Text.ToString();
            statusRequest.paymentItem.account.ABANumber = _Bankid.Text.ToString();
            statusRequest.paymentItem.account.type = accountType;
            //MAIG - CH20 - BEGIN - Added logic to pass the Email ID to Enrollment Service if it is not null 
            if (!string.IsNullOrEmpty(_txtEmailAddress.Text.ToString().Trim()))
                statusRequest.emailTo = _txtEmailAddress.Text.ToString().Trim();
            //MAIG - CH20 - END - Added logic to pass the Email ID to Enrollment Service if it is not null 
            Logger.Log(Constants.PC_LOG_REQ_SENT + statusRequest.policyInfo.policyNumber);

            return statusRequest;
        }
        #region Cancel Click event
        /// <summary>
        /// CHG0072116 - Cancel button Fix- Added the below method to Navigate back to EC View details/Payment method selection when the cancel button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImgBtnCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl("policySearchDetails");
            PaymentMethod selectPayment = (PaymentMethod)this.NamingContainer.FindControl("PaymentMethod");
            this.Visible = false;
            lblErrMessage.Visible = false;

            //MAIG - CH21 - BEGIN - Added logic to check if the entered policy is a valid policy and else condition added
            ImgBtnEcheckEnrollment.Enabled = true;
            EnableAllFields();
            /////// Hiding the Change Payment link button
            tblRecRedirectLnkButtons.Visible = false;
            if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFlow == null) && (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow == null))
            {
                if (PH_PolicyDetail != null)
                {
                    PH_PolicyDetail.Visible = true;
                }
                Label payRestrictMessage = (Label)this.NamingContainer.FindControl("lblPaymentRestrict");
                if (!string.IsNullOrEmpty(payRestrictMessage.Text))
                {
                    payRestrictMessage.Visible = true;
                }
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
            //MAIG - CH21 - END - Added logic to check if the entered policy is a valid policy and else condition added
        }
        #endregion

        //MAIG - CH22 - EBGIN - Added logic to change the Payment method for Redirection flow
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
                dropdown_PaymentType.Enabled = true;
                ClearFields();

                EnableAllFields();

                UserControl UC_ECEnroll = (UserControl)this.NamingContainer.FindControl("EnrollECheck");
                if (UC_ECEnroll != null)
                {
                    UC_ECEnroll.Visible = false;
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
            _Bankid.Enabled = true;
            displayBankName.Text = string.Empty;
            _txtCustomerName.Enabled = true;
            //CHGXXXXXX - Server Upgrade 4.1 - Updated text box to enable Card number in Payment Recurring Redirection flow - Start
            __ReAccountNoCardMasked.Enabled = true;
            //CHGXXXXXX - Server Upgrade 4.1 - Updated text box to enable Card number in Payment Recurring Redirection flow - End
            _txtEmailAddress.Enabled = true;
            //CHGXXXXXX - Server Upgrade 4.1 - Updated text box to enable Card number in Payment Recurring Redirection flow - Start
            __AccountnoMasked.Enabled = true;
            //CHGXXXXXX - Server Upgrade 4.1 - Updated text box to enable Card number in Payment Recurring Redirection flow - End
            rbCheckingAccount.Enabled = true;
            rbSavingsAccount.Enabled = true;
            rbCheckingAccount.Checked = true;

        }
        //MAIG - CH22 - END - Added logic to change the Payment method for Redirection flow
    }
}
