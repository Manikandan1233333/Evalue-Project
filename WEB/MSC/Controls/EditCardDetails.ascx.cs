/*
 * HISTORY
 * CHG0072116 - PC Edit Card Details - Added the page as part of edit the card details
 * Created by: Cognizant on 05/01/2013
 * Decsription: This is a new file added as part of Payment Central Phase II - Edit card Details CR.
 * MAIG - CH1 - Added logic to Enable the Update button after postbacks
 * MAIG - CH2 - Modified logic to set the hdnCardType value from the Property if not equals to Master, Amex, Discover
 * MAIG - CH3 - Modified/Commented logic to pass the policy details to Terms and Condition page
 * MAIG - CH4 - Logic to set the Update button Enabled property to false
 * MAIG - CH5 - Including Agent & Agency ID to the Enrollment Request
 * MAIG - CH6 - Logic to set the Invalid Unenroll flag passed to Confirmation page and removing PUP Prefix
 * MAIG - CH7 - Condition added to check if the policy is an valid policy
 * MAIG - CH8 - Condition added to enable the Update Button
 * MAIG - CH9 - Logic added to hide the Policy grid details for an invalid policy
 * MAIG - CH10 - Logic added to pass the datasourec for all policies and set the Blaze rules for valid policy
 * MAIG - CH11 - Adding Email ID value to the REQUEST
 * MAIG - CH12 - Adding condition to set the Policy grid details visible for a valid policy
 * MAIG - CH13 - Modified the below logic to check if the Page is Valid
 * MAIG - CH14 - Modified code to display the Error Code only once
 * MAIG - CH15 - Added logic to add the condition for Amex and Discover
 * CHG0112662 - Appended the condition for error code 300 to the existing fault exception handling, Removed Error Code in actual error message & Payment Enrollment SOA Service changes
 * CHG0116140 - Payment Restricition - Commented the code which is used for enabling the Payment Restriction message.
*/
namespace MSC.Controls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using CSAAWeb;
    using CSAAWeb.AppLogger;
    using CSAAWeb.WebControls;
    using OrderClassesII;
    using RecordEnrollment;
    using System.Collections.Generic;
    using System.Web.UI;
    using System.Net.Mail;
    using System.Collections.Specialized;
    using System.Text.RegularExpressions;
    using AuthenticationClasses;
    using System.ServiceModel;
    using System.Xml;

    /// <summary>
    ///		Code for Update/Edit Card Details.
    /// </summary>
    /// 

    public partial class EditCardDetails : ValidatingUserControl
    {
        #region Variables
        private Boolean _isValid = true;
        private string paymentTokenNumber = string.Empty;
        #endregion

        #region WebControls
        protected CSAAWeb.WebControls.Validator lblbankid;
        protected System.Web.UI.WebControls.DropDownList _AccountType;
        protected CSAAWeb.WebControls.Validator lblAccounttype;
        protected System.Web.UI.WebControls.TextBox _Accountno;
        protected System.Web.UI.WebControls.TextBox _Bankid;
        protected CSAAWeb.WebControls.Validator lblAccountno;

        protected CSAAWeb.WebControls.Validator vldExpDate;
        AuthenticationClasses.Service.Authentication ua;
        #endregion

        #region Properties
        public string CCNumber { get { return lblCardNumberLast4Digit.Text.ToString().Trim(); } set { lblCardNumberLast4Digit.Text = value; } }
        public string Name { get { return _Name.Text.Trim(); } set { _Name.Text = value; } }
        public string CCExpMonth { get { return ListC.GetListValue(_ExpireMonth); } set { ListC.SetListIndex(_ExpireMonth, value); } }
        ///<summary>Billing expiration year</summary>
        public string CCExpYear { get { return ListC.GetListValue(_ExpireYear); } set { ListC.SetListIndex(_ExpireYear, value); } }

        public string ZipCode { get { return _txtZipCode.Text.Trim(); } set { _txtZipCode.Text = value; } }

        ///<summary>Email Address Validation</summary>
        public string EmailAddress { get { return _txtEmailAddress.Text.Trim(); } set { _txtEmailAddress.Text = value; } }
        #endregion

        public EditCardDetails()
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

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);
        }
        #endregion

        private void Initialize()
        {
        }

        ///<summary>Called from init event.</summary>
        private void InitializeLBs(object sender, System.EventArgs e)
        {
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

            if (!Page.IsPostBack)
            {
                lblCardNumberLast4Digit.Text = string.Empty;
                hdnPaymentToken.Value = string.Empty;
                hdnCardType.Value = string.Empty;
                _ExpireMonth.SelectedIndex = 0;
                _ExpireYear.SelectedIndex = 0;
                _Name.Text = string.Empty;
                _txtZipCode.Text = string.Empty;
                _txtEmailAddress.Text = string.Empty;
                lblErrMessage.Text = string.Empty;

            }
			//MAIG - CH1 - BEGIN - Added logic to Enable the Update button after postbacks
            else
                ImgBtnUpdate.Enabled = true;            
                
			//MAIG - CH1 - END - Added logic to Enable the Update button after postbacks
        }
        #endregion

        #region Assign values for view enrollment
        /// <summary>
        /// Assign values to the controls when policy details are retrived in mange enrollment page.
        /// </summary>
        public void assignValues()
        {
            try
            {
                lblPaymentMethod.Text = "Credit Card";
                lblCardNumberLast4Digit.Text = CSAAWeb.Constants.PC_MASK_TXT + ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._CCNumber;
                this._Name.Text = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._CCName;
                _ExpireMonth.SelectedValue = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._CCExpDate.Split('/')[0].ToString();
                _ExpireYear.SelectedValue = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._CCExpDate.Split('/')[1].ToString();
                this._txtZipCode.Text = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._CCZipCode;
                hdnPaymentToken.Value = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._PaymentTokenNumber;
				//MAIG - CH2 - BEGIN - Modified logic to set the hdnCardType value from the Property if not equals to Master, Amex, Discover
              //  hdnCardType.Value = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._CCType == CSAAWeb.Constants.PC_CRD_TYPE_MASTER_EXPANSION ? CSAAWeb.Constants.PC_CRD_TYPE_MASTER : CSAAWeb.Constants.PC_CRD_TYPE_MASTER;
                hdnCardType.Value = (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._CCType.Equals(CSAAWeb.Constants.PC_CRD_TYPE_MASTER_EXPANSION)) ? CSAAWeb.Constants.PC_CRD_TYPE_MASTER : (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._CCType.Equals(Constants.PC_CRD_TYPE_AMEX_EXPANSION)) ? CSAAWeb.Constants.PC_CRD_TYPE_AMEX : (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._CCType.Equals(Constants.PC_CRD_TYPE_DISC_EXPANSION)) ? CSAAWeb.Constants.PC_CRD_TYPE_DISC : ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._CCType;
                //MAIG - CH2 - END - Modified logic to set the hdnCardType value from the Property if not equals to Master, Amex, Discover
            }
            catch(Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        
        }
        #endregion
        
        #region Display Terms
        /// <summary>
        /// Display the terms depends on the DO
        /// </summary>
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

                    if (DO_CC.Contains(DO) || string.IsNullOrEmpty(DO))
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

        #region Controls validation when the corresponding label validator is fired.
        //Added a new method as a part of .NetMig 3.5 on 02-10-2010.
        /// <summary>
        /// Validate required controls values
        /// </summary>
        protected void ReqValCheck(Object source, ValidatorEventArgs args)
        {

            if (_ExpireMonth.SelectedItem.Text == Constants.PC_CC_SELECT_MONTH)
            {
                args.IsValid = false;
                vldExpDate.MarkInvalid();
                vldExpDate.ErrorMessage = Constants.PC_CC_ERR_INVALID_EXP_MONTH;
                UpdateCCReqFieldValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }

            if (_ExpireYear.SelectedItem.Text == Constants.PC_CC_SELECT_YEAR)
            {
                args.IsValid = false;
                vldExpDate.MarkInvalid();
                vldExpDate.ErrorMessage = Constants.PC_CC_ERR_SELECT_EXP_YEAR;
                UpdateCCReqFieldValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }
            if (string.IsNullOrEmpty(_Name.Text.Trim()))
            {
                args.IsValid = false;
                vldNameCheck.MarkInvalid();
                vldNameCheck.ErrorMessage = Constants.PC_CC_ERR_NAME_REQ;
                UpdateCCReqFieldValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }
            if (string.IsNullOrEmpty(_txtZipCode.Text.Trim()))
            {
                args.IsValid = false;
                vldZip.MarkInvalid();
                vldZip.ErrorMessage = Constants.PC_CC_ERR_EMPTY_ZIPCODE;
                UpdateCCReqFieldValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }
            if (!chkF2Fuser.Checked)
            {
                args.IsValid = false;
                vldF2F.MarkInvalid();
                vldF2F.ErrorMessage = Constants.PC_ERR_CHK_TC;
                UpdateCCReqFieldValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }
        }



        
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
                        vldExpDate.MarkInvalid();
                        UpdateCCReqFieldValidator.MarkInvalid();
                        vldExpDate.ErrorMessage = Constants.PC_CC_INVALID_EXP_YEAR;
                        UpdateCCReqFieldValidator.ErrorMessage = string.Empty;
                        _isValid = false;
                    }
                }
            }
            if (_ExpireMonth.SelectedItem.Text == Constants.PC_CC_SELECT_MONTH)
            {
                args.IsValid = false;
                vldExp.MarkInvalid();
                vldExp.ErrorMessage = string.Empty;
                UpdateCCReqFieldValidator.ErrorMessage = string.Empty;
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
        /// validate Zip Code when the corresponding label validator is fired.
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void ValidZipLength(Object source, ValidatorEventArgs args)
        {
            
            if (!string.IsNullOrEmpty(ZipCode))
            {
                args.IsValid = (ZipCode.Length == 5);
                if (!args.IsValid)
                {
                    vldZip.MarkInvalid();
                    vldZip.ErrorMessage = string.Empty;
                    UpdateCCReqFieldValidator.ErrorMessage = string.Empty;
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
                    UpdateCCReqFieldValidator.ErrorMessage = string.Empty;
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
                    UpdateCCReqFieldValidator.ErrorMessage = string.Empty;
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
				//MAIG - CH3 - BEGIN - Modified/Commented logic to pass the policy details to Terms and Condition page
                string insFName = string.Empty;
                ////string expirationDate = (!string.IsNullOrEmpty(_ExpireMonth.SelectedValue.ToString() + "-" + _ExpireYear.SelectedValue.ToString()) ? _ExpireMonth.SelectedValue + "-" + _ExpireYear.SelectedValue : string.Empty).ToString();
                ////string name = (!string.IsNullOrEmpty(_Name.Text.ToString()) ? _Name.Text.ToString() : string.Empty).ToString();
                insFName = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InsuredFullName.ToString();

                MSC.Encryption.EncryptQueryString args = new MSC.Encryption.EncryptQueryString();
                ////args[Constants.PC_TC_ARG1] = Constants.PC_PTYPE_CC;
                ////args[Constants.PC_TC_ARG2] = lblCardNumberLast4Digit.Text.ToString();
                ////args[Constants.PC_TC_ARG3] = expirationDate;
                args[Constants.PC_TC_ARG2] = insFName;
                //T&C Changes CH1 - START - Added the below code to assign the policy number and product type value to the arguments
                args[Constants.PC_TC_ARG1] = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).PolicyNumber.ToString();
                ////args[Constants.PC_TC_ARG8] = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ProductType_PC_Text.ToString();
				//MAIG - CH3 - END - Modified/Commented logic to pass the policy details to Terms and Condition page
                //T&C Changes CH1 - END - Added the below code to assign the policy number and product type value to the arguments
                string url = String.Format(Constants.PC_TC_URL, args.ToString());
                Page.ClientScript.RegisterStartupScript(GetType(), Constants.PC_TC_SCRIPT_KEY, Constants.PC_TC_SCRIPT_ONE + url + Constants.PC_TC_SCRIPT_TWO);
            }
            catch (Exception exception)
            {
                Logger.Name = Constants.PC_TC_LOG_NAME;
                Logger.Log(exception);
            }
        }
        /// <summary>
        /// Update credit card details when the update button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImgBtnUpdate_Click(object sender, ImageClickEventArgs e)
        {

            



                PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl(Constants.PC_CNTRL_NAME);
                if (PH_PolicyDetail != null)
                {
                    PH_PolicyDetail.Visible = true;
                }
                string PC_ProductCode = string.Empty;
                //MAIG - CH13 - BEGIN - Modified the below logic to check if the Page is Valid
                if (_isValid && Page.IsValid)
                //MAIG - CH13 - END - Modified the below logic to check if the Page is Valid
                {
                    try
                    {
                    	//MAIG - CH4 - BEGIN - Logic to set the Update button Enabled property to false
                        ImgBtnUpdate.Enabled = false;
                        //OrderClasses.Service.Order authUserObj = new OrderClasses.Service.Order();
                        //UserInfo userInfoObj = new UserInfo();

                        //userInfoObj = authUserObj.Authenticate(HttpContext.Current.User.Identity.Name, string.Empty, CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL);
                    	//MAIG - CH4 - END - Logic to set the Update button Enabled property to false
                        if (!string.IsNullOrEmpty(hdnPaymentToken.Value))
                        {
                            List<string> response = new List<string>();
                            OrderClasses.OrderInfo ordderInfo = new OrderClasses.OrderInfo();
                            SessionInfo sessionInfo = new SessionInfo();
                            IssueDirectPaymentWrapper actObj = new IssueDirectPaymentWrapper();
                            string token = string.Empty;
                            ordderInfo.S = new SessionInfo();
                            ordderInfo.S.AppName = CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL;
                            ordderInfo.User = new UserInfo();
                            ordderInfo.User.UserId = HttpContext.Current.User.Identity.Name;
                            ordderInfo.Card = new OrderClasses.CardInfo();
                            ordderInfo.Card.CCExpMonth = _ExpireMonth.SelectedValue.ToString();
                            ordderInfo.Card.CCExpYear = _ExpireYear.SelectedValue.ToString();
                            ordderInfo.Card.CCNumber = lblCardNumberLast4Digit.Text;
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
                            inputRequest = actObj.Tokenmapping(ordderInfo, sessionInfo, hdnPaymentToken.Value);
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
                            }

                            PCEnrollmentMapping pCEnroll = new PCEnrollmentMapping();
                            //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Renamed the Autpay Enrollment request to Payment Enrollment request
                            RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest statusRequest = new RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest();
                            statusRequest = CreatePaymentStatusRequest(token);
                            //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Renamed the Autpay Enrollment request to Payment Enrollment request

							//MAIG - CH5 - BEGIN -  Including Agent & Agency ID to the Enrollment Request
                            if (string.IsNullOrEmpty(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoAgencyID) || ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoAgencyID.ToString().Equals("0"))
                                statusRequest.agency = string.Empty;
                            else
                                statusRequest.agency = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoAgencyID.ToString();

                            //////if (string.IsNullOrEmpty(userInfoObj.RepId.ToString()) || userInfoObj.RepId.ToString().Equals("0"))
                            //////    statusRequest.agentIdentifier = string.Empty;
                            //////else
                            //////    statusRequest.agentIdentifier = userInfoObj.RepId.ToString();

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
							//MAIG - CH5 - END -  Including Agent & Agency ID to the Enrollment Request

                            //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Renamed the method
                            response = pCEnroll.PCPaymentEnrollService(statusRequest);
                            //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Renamed the method

                            if (response[0].Equals(CSAAWeb.Constants.PC_SUCESS.ToUpper()))
                            {
                                Logger.Log(Constants.PC_LOG_STATUS_REQUEST_SUCCESS + statusRequest.policyInfo.policyNumber + Constants.PC_LOG_CNFRM_NUMBER + response[1].ToString());
                                Context.Items.Add(Constants.PC_CNFRMNUMBER, response[1].ToString());

								//MAIG - CH6 - BEGIN -  Logic to set the Invalid Unenroll flag passed to Confirmation page and removing PUP Prefix
                                ////// Removing the logic to prefix the PUP for PUP Policies - start
                                ////////if (statusRequest.policyInfo.type == Constants.PC_TYPE_PUP)
                                ////////{
                                ////////    Context.Items.Add(Constants.PC_POLICYNUMBER, Constants.PC_PUP + statusRequest.policyInfo.policyNumber.ToString());
                                ////////}
                                ////////else
                                ////////{
                                Context.Items.Add(Constants.PC_POLICYNUMBER, statusRequest.policyInfo.policyNumber.ToString());
                                ////////}                            
                                ///// Removing the logic to prefix the PUP for PUP Policies - end

                                ////// Context.Items.Add("PC_INVALID_POLICY_FLOW_FLAG", false);
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
								//MAIG - CH6 - END -  Logic to set the Invalid Unenroll flag passed to Confirmation page and removing PUP Prefix

                                Context.Items.Add(Constants.PC_CCNumber, Constants.PC_MASK_TXT + statusRequest.paymentItem.card.number);
                                //MAIG - CH15 - BEGIN - Added logic to add the condition for Amex and Discover
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
                                //MAIG - CH15 - END - Added logic to add the condition for Amex and Discover
                                Context.Items.Add(Constants.PC_CCType, statusRequest.paymentItem.card.type.ToUpper());
								//MAIG - CH7 - BEGIN -  Condition added to check if the policy is an valid policy
                                if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow == null)
                                {
                                    Context.Items.Add(Constants.PC_BILL_PLAN, ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).BillingPlan);
                                    Context.Items.Add(Constants.PC_IS_ENROLLED, ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._IsEnrolled);
                                    Context.Items.Add(Constants.PC_VLD_MDFY_ENROLLMENT, ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Modifyenrollment);
                                    Context.Items.Add(Constants.PC_VLD_ENROLLMENT, ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_enrollment);
                                }
								//MAIG - CH7 - END -  Condition added to check if the policy is an valid policy
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
                                Context.Items.Add(Constants.PC_EMAIL_ID, _txtEmailAddress.Text.ToString());
                                Context.Items.Add(Constants.PC_EDIT_CC, Constants.PC_EDIT_CC);
                                Context.Items.Add(Constants.PC_EDIT_CONST_RESPONSE, Constants.PC_EDIT_RESPONSE);
                                Server.Transfer(Constants.PC_RECURR_CNFRM_URL);
                            }
                            else
                            {
                                Logger.Log(Constants.PC_LOG_FAIL + statusRequest.policyInfo.policyNumber);
                                lblErrMessage.Visible = true;
                                //MAIG - CH14 - BEGIN - Modified code to display the Error Code only once
                                //lblErrMessage.Text = response[1].ToString() + " (" + response[0].ToString() + ")";
                                lblErrMessage.Text = response[1].ToString();
                                //MAIG - CH14 - END - Modified code to display the Error Code only once
                            }
                        }
                        else
                        {
                            lblErrMessage.Visible = true;
                            lblErrMessage.Text = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                            Logger.Log(Constants.PC_UPCC_PToken_Empty + ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).PolicyNumber);
                        }
                    }
                    catch (FaultException faultExp)
                    {
                        DisplayException(faultExp);
						//MAIG - CH8 - BEGIN -  Condition added to enable the Update Button
                        ImgBtnUpdate.Enabled = true;
						//MAIG - CH8 - END -  Condition added to enable the Update Button
                    }

                }
            
        }
        /// <summary>
        /// Display fault exception inside label.
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
				//MAIG - CH9 - BEGIN -  Logic added to hide the Policy grid details for an invalid policy
                if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow) != null)
                {
                    PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl(Constants.PC_CNTRL_NAME);

                    if (PH_PolicyDetail != null)
                    {
                        PH_PolicyDetail.Visible = false;
                    }
                }
				//MAIG - CH9 - END -  Logic added to hide the Policy grid details for an invalid policy

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
        /// Method created to consume the service "recordAutoPayEnrollmentStatus" to save the updated credit card details.
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private recordAutoPayEnrollmentStatusRequest CreateStatusRequest(string token)
        {
            recordAutoPayEnrollmentStatusRequest statusRequest = new recordAutoPayEnrollmentStatusRequest();
            statusRequest.applicationContext = new ApplicationContext();
            statusRequest.applicationContext.application = Config.Setting(CSAAWeb.Constants.PC_ApplicationContext_Payment_Tool);
            statusRequest.applicationContext.address = CSAAWeb.AppLogger.Logger.GetIPAddress();
            statusRequest.userId = HttpContext.Current.User.Identity.Name;
            statusRequest.policyInfo = new PolicyProductSource();
            statusRequest.policyInfo.policyNumber = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).PolicyNumber;
            statusRequest.policyInfo.type = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ProductType_PC;
			//MAIG - CH10 - BEGIN -  Logic added to pass the datasourec for all policies and set the Blaze rules for valid policy
            if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow == null))
            {
                statusRequest.enrollmentEffectiveDateSpecified = true;
                statusRequest.enrollmentEffectiveDate = Convert.ToDateTime(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Modifyenrollment[2]);
                statusRequest.enrollmentReasonCode = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Modifyenrollment[3];
            }
            
            // Included MAIG Iteration2 - start
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
			//MAIG - CH10 - END -  Logic added to pass the datasourec for all policies and set the Blaze rules for valid policy

            statusRequest.paymentItem = new PaymentItemHeader();
            statusRequest.paymentItem.paymentMethod = Config.Setting(CSAAWeb.Constants.PC_CreditCard_Code);
            statusRequest.paymentItem.paymentAccountToken = token;
            statusRequest.paymentItem.card = new PaymentCard();
            statusRequest.paymentItem.card.number = lblCardNumberLast4Digit.Text.Substring(12);
            statusRequest.paymentItem.card.isCardPresentSpecified = true;
            statusRequest.paymentItem.card.isCardPresent = true;
            statusRequest.paymentItem.card.expirationDateSpecified = true;
            statusRequest.paymentItem.card.expirationDate = Convert.ToDateTime(_ExpireMonth.SelectedValue + "-" + _ExpireYear.SelectedValue);
            statusRequest.paymentItem.card.printedName = _Name.Text;
            statusRequest.paymentItem.card.type = hdnCardType.Value.ToString();

			//MAIG - CH11 - BEGIN -  Adding Email ID value to the REQUEST
            if(!(string.IsNullOrEmpty(_txtEmailAddress.Text.ToString())))
                statusRequest.emailTo = _txtEmailAddress.Text.ToString().Trim();
			//MAIG - CH11 - END -  Adding Email ID value to the REQUEST

            Logger.Log(Constants.PC_LOG_REQ_SENT + statusRequest.policyInfo.policyNumber);
            return statusRequest;
        }

        /// <summary>
        /// Method created to consume the service "recordAutoPayEnrollmentStatus" to save the updated credit card details.
        /// CHG0112662 - Record Payment Enrollment SOA Service changes - Added this method
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        private RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest CreatePaymentStatusRequest(string token)
        {
            RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest statusRequest = new RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest();
            statusRequest.applicationContext = new RecordPaymentEnrollment.ApplicationContext();
            statusRequest.applicationContext.application = Config.Setting(CSAAWeb.Constants.PC_ApplicationContext_Payment_Tool);
            statusRequest.applicationContext.address = CSAAWeb.AppLogger.Logger.GetIPAddress();
            statusRequest.userId = HttpContext.Current.User.Identity.Name;
            statusRequest.policyInfo = new RecordPaymentEnrollment.PolicyProductSource();
            statusRequest.policyInfo.policyNumber = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).PolicyNumber;
            statusRequest.policyInfo.type = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ProductType_PC;
            //MAIG - CH10 - BEGIN -  Logic added to pass the datasourec for all policies and set the Blaze rules for valid policy
            if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow == null))
            {
                statusRequest.enrollmentEffectiveDateSpecified = true;
                statusRequest.enrollmentEffectiveDate = Convert.ToDateTime(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Modifyenrollment[2]);
                statusRequest.enrollmentReasonCode = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Modifyenrollment[3];
            }

            // Included MAIG Iteration2 - start
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
            //MAIG - CH10 - END -  Logic added to pass the datasourec for all policies and set the Blaze rules for valid policy

            statusRequest.paymentItem = new RecordPaymentEnrollment.PaymentItemHeader();
            statusRequest.paymentItem.paymentMethod = Config.Setting(CSAAWeb.Constants.PC_CreditCard_Code);
            statusRequest.paymentItem.paymentAccountToken = token;
            statusRequest.paymentItem.card = new RecordPaymentEnrollment.PaymentCard();
            statusRequest.paymentItem.card.number = lblCardNumberLast4Digit.Text.Substring(12);
            statusRequest.paymentItem.card.isCardPresentSpecified = true;
            statusRequest.paymentItem.card.isCardPresent = true;
            statusRequest.paymentItem.card.expirationDateSpecified = true;
            statusRequest.paymentItem.card.expirationDate = Convert.ToDateTime(_ExpireMonth.SelectedValue + "-" + _ExpireYear.SelectedValue);
            statusRequest.paymentItem.card.printedName = _Name.Text;
            statusRequest.paymentItem.card.type = hdnCardType.Value.ToString();

            //MAIG - CH11 - BEGIN -  Adding Email ID value to the REQUEST
            if (!(string.IsNullOrEmpty(_txtEmailAddress.Text.ToString())))
                statusRequest.emailTo = _txtEmailAddress.Text.ToString().Trim();
            //MAIG - CH11 - END -  Adding Email ID value to the REQUEST

            Logger.Log(Constants.PC_LOG_REQ_SENT + statusRequest.policyInfo.policyNumber);
            return statusRequest;
        }

        /// <summary>
        /// Navigate back to CC View details when the cancel button is clicked.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ImgBtnCancel_Click(object sender, ImageClickEventArgs e)
        {
            UserControl UC_UnEnrollCC = (UserControl)this.NamingContainer.FindControl("UnEnrollCC");
            if (UC_UnEnrollCC != null)
            {
                UC_UnEnrollCC.Visible = true;
            }

            UserControl UC_UpdateCardDetails = (UserControl)this.NamingContainer.FindControl("EditCC");
            if (UC_UpdateCardDetails != null)
            {
                UC_UpdateCardDetails.Visible = false;
            }
			//MAIG - CH12 - BEGIN -  Adding condition to set the Policy grid details visible for a valid policy
            if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow) == null)
            {
                PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl("policySearchDetails");
                if (PH_PolicyDetail != null)
                {
                    PH_PolicyDetail.Visible = true;
                }
            }
            //MAIG - CH12 - END -  Adding condition to set the Policy grid details visible for a valid policy

            //CHG0116140 - Payment Restricition - BEGIN - Commented the code which is used for enabling the Payment Restriction message.
            //Label payRestrictMessage = (Label)this.NamingContainer.FindControl("lblPaymentRestrict");
            //if (!string.IsNullOrEmpty(payRestrictMessage.Text))
            //{
            //    payRestrictMessage.Visible = true;
            //}
            //CHG0116140 - Payment Restricition - END - Commented the code which is used for enabling the Payment Restriction message.
        }
    }

        


    
}
