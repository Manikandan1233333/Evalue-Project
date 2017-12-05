/*
 * HISTORY
 * PC Phase II changes - Added the page as part of Enrollment changes.Main page for enrollment.
 * Created by: Cognizant on 2/5/2013
 * Decsription: This is a new file added as part of Payment Central Phase II.
 * CHG0072116 - PC Edit Card Details CH1 - Created Property to assign values to the edit card details usercontrol.
 * CHG0072116 - PC Edit Card Details CH2 - Hide the edit card details usercontrol depends on payment method.
 * CHG0072116 - PC Edit Card Details CH3 - Hide the edit card details usercontrol when user clik the button "search".
 * CHG0072116 - PC Edit Card Details CH4 - Modified the enrollment message to modify enrollment message to get the correct message for modification scenario
 * //T&C Changes CH1 - Added the below code to assign the Produt type selected.
 * //CHG0083477 - Expansion of Homeowner Policy - Added additional characters (G, I, O, Q, S, Z) to the conditions.
 * MAIG - CH1 - Declared properties to pass the policy details to User controls
 * MAIG - CH2 - Added new variable to store the DataSource
 * MAIG - CH3 - Added Regex for Name validation
 * MAIG - CH4 - Added code to get the AgencyId and RepID from Ticket
 * MAIG - CH5 - Added code to identify if the flow is from Payment Confirmation Redirection
 * MAIG - CH6 - Added code to assign the Policy details for invalid policy number
 * MAIG - CH7 - Added code to retain the properties which is accessed in user controls
 * MAIG - CH8 - Added code to retain the properties which is accessed in user controls
 * MAIG - CH9 - Added new method to manage the flow from Payment Confirmation to Manage Enrollment page which pre-populates the payment details
 * MAIG - CH10 - Added code to display the Amex and Discover Credit card types only for KIC
 * MAIG - CH11 - Added code to hide the policy grid it is an invalid policy
 * MAIG - CH12 - Removed the logic that maps the request for ValidateEnrollment Service
 * MAIG - CH13 - Reset the Error message to empty and hide it
 * MAIG - CH14 - Added Source System as an paramter & Reset the Error message to empty and hide it
 * MAIG - CH15 - Added Source System as an paramter to PCRetrieveEnrollmentService method
 * MAIG - CH16 - Added code to hide the invalid flow 
 * MAIG - CH17 - Added code to hide the invalid flow and display the Error Code only once
 * MAIG - CH18 - Added code to hide the invalid flow 
 * MAIG - CH19 - Modified the logic to get the PolicyNumber for PUP products
 * MAIG - CH20 - Modified the logic to remove the Old Product Code SS Home
 * MAIG - CH21 - Removed the logic to add a zero to PCP Products
 * MAIG - CH22 - Added code to delete the non-selected row if there are multiple response from RPBS
 * MAIG - CH23 - Removed the logic that invokes the Validate Enrollment Service and gets the Enrollment validation details from RPBS Service
 * MAIG - CH24 - Removed logic to append 'PUP' prefix to the Policy
 * MAIG - CH25 - Added Source System as an paramter to isEnrollProcess method
 * MAIG - CH26 - Added code to hide the policy grid it is an invalid policy
 * MAIG - CH27 - Added Source System as an paramter to isEnrollProcess method
 * MAIG - CH28 - Added logic to handle if the entered policy number is invalid
 * MAIG - CH29 - Invoked method to validate the FirstName,LastName and Mailing Zip fields
 * MAIG - CH30 - Commented the logic since it is not used.
 * MAIG - CH31 - Commented the logic since it is not used.
 * MAIG - CH32 - Modified the logic to remove PUP prefix and support 7 digits of the policy
 * MAIG - CH33 - Commented the logic since it is not used.
 * MAIG - CH34 - Commented the logic since it is not used.
 * MAIG - CH35 - Commented the logic to Support Common Product Types
 * MAIG - CH36 - Commented the Code to change the Tooltip.
 * MAIG - CH37 - Added method to validate the FirstName, LastName and Mailing Zip details
 * MAIG - CH38 - Added logic to check if the Card Type is Amex or Discover
 * MAIGEnhancement CHG0107527 - CH1 - Modified the below Installment payment flow to use the key/value pair with ^ than , which causes issue in Full Name field from RPBS response
 * CHG0112662 - Payment Enrollment SOA Service changes
 * CHG0113938 - Removed the hardcoded status description and mapped the status description to staus.
 * CHG0116140 - Payment Restricition - Set Payment Restriction message to the label
 * CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES
*/
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services.Protocols;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Xml;
using System.Xml.Linq;
using CSAAWeb;
using CSAAWeb.AppLogger;
using CSAAWeb.WebControls;
using InsuranceClasses;
using MSC.Controls;
using OrderClasses.Service;
using OrderClassesII;
using System.Web.UI.HtmlControls;

namespace MSC.Forms
{
    /// <summary>
    /// This page is the start page for the application, and currently
    /// sets up a call to primary with the order primed to enter data for
    /// a new member order.
    /// </summary>
    public partial class ManageEnrollment : SiteTemplate
    {
        #region Variables
        BillingLookUp BillingSummary = new BillingLookUp();
        CreditCardEnrollment ccEroll = new CreditCardEnrollment();
        IssueDirectPaymentWrapper Obj = new IssueDirectPaymentWrapper();
        Order O = new Order();
        DataTable dtTable = new DataTable();
        PCEnrollmentMapping retrieveEnrollment;
        DateTime expDate;
        string ExpDteMon;
        private static DataTable ProductTypes = null;
        private static DataTable PaymentTypes = null;
        public string PolicyNumber { get; set; }
        public string _ProductType_PT { get; set; }
        public string _ProductType_PC { get; set; }
        public string _CCType { get; set; }
        public string _CCNumber { get; set; }
        public string _CCName { get; set; }
        public string _CCExpDate { get; set; }
        //CHG0072116 - PC Edit Card Details CH1:START - Created Property to assign values to the edit card details usercontrol.
        public string _CCZipCode { get; set; }
        public string _PaymentTokenNumber { get; set; }
        //CHG0072116 - PC Edit Card Details CH1:START - Created Property to assign values to the edit card details usercontrol..
        public string _ProductType_PC_Text { get; set; }
        public string _ECRoutingNo { get; set; }
        public string _ECAccountNo { get; set; }
        public string _ECaacType { get; set; }
        public string _ECBankName { get; set; }
        public string _ECAccountHolderName { get; set; }
        public string _EmailAddress { get; set; }
        public bool _CCValidate { get; set; }
        public bool _ECValidate { get; set; }
        public string _BillPlan { get; set; }
        public string BillingPlan { get; set; }
        public List<string> _ValidationService_enrollment { get; set; }
        public List<string> _ValidationService_Unenrollment { get; set; }
        public List<string> _ValidationService_Modifyenrollment { get; set; }
        public List<string> _IsPaymentMethodChanged { get; set; }
        public string _IsEnrolled { get; set; }

        //MAIG - CH1 - BEGIN - Declared properties to pass the policy details to User controls
        public string _InvalidPolicyFlow { get; set; }
        public string _InvalidPolicyFName { get; set; }
        public string _InvalidPolicyLName { get; set; }
        public string _InvalidPolicyMZip { get; set; }
        public string _ValidPolicySourceSystem { get; set; }
        public string _InvalidUnEnrollFlow { get; set; }

        public string _InsuredFullName { get; set; }

        public string _RecurringRedirectionFlag { get; set; }
        //MAIG - CH1 - END - Declared properties to pass the policy details to User controls
        public string _EnrolledMethod { get; set; }
        private string Payflag, billPlan = string.Empty;
        bool isEnroll = false;
        bool flag = false;
        public static string billingplan = string.Empty;
        private string mAddStringName = "All";
        private static DataTable CardTypes = null;
        ///<summary>The Url to navigate on Back button click (from web.config).</summary>
        protected static string _OnBackUrl;
        ///<summary>The Url to navigate on Continue button click (from web.config).</summary>
        protected static string _OnContinueUrl = string.Empty;
        private Boolean _isValid = true;
        public string Invalid_Message;
        //MAIG - CH2 - BEGIN - Added new variable to store the DataSource
        public string _DataSource { get; set; }
        DataTable dt = new DataTable();
        public string _UserInfoAgencyID { get; set; }
        public string _UserInfoRepID { get; set; }
        //MAIG - CH2 - END - Added new variable to store the DataSource
        #endregion

        //MAIG - CH3 - BEGIN - Added Regex for Name validation
        ///  Creation of new Regular Expression to check the FirstName 
        /// </summary>
        private static Regex regCheckName = new Regex("[^a-z '.,-]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //MAIG - CH3 - END - Added Regex for Name validation

        ///<summary/>  
        protected Validator Valid;

        ///<summary/>
        public override string OnCancelUrl { get { return Request.Path; } set { } }
        /// <summary>
        /// Creates a new order with blank household data and number of associates=0
        /// </summary>
        protected override void SavePageData()
        {
            InsuranceInfo I = new InsuranceInfo();
            I.Lines = new ArrayOfInsuranceLineItem();
            I.Lines.Add(new InsuranceLineItem());
            Order.Products.Add(I);

        }
        ///<summary>PaymentType Control validator</summary> 
        protected CSAAWeb.WebControls.Validator vldPaymentType;
        /// <summary>
        /// Authenticates, if authentication turned on, then transfers to primary.aspx.
        /// </summary>
        /// <param name="e"></param>
        protected override void OnInit(EventArgs e)
        {
            base.OnInit(e);

        }

        #region pageload
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                //MAIG - CH4 - BEGIN - Added code to get the AgencyId and RepID from Ticket
                //OrderClasses.Service.Order authUserObj = new OrderClasses.Service.Order(Page);
                //AuthenticationClasses.UserInfo userInfoObj = new AuthenticationClasses.UserInfo();
                //userInfoObj = authUserObj.GetUserInfo(this.Page.User.Identity.Name.ToString());
                Logger.Log("Machine IP:" + Page.Request.ServerVariables["REMOTE_ADDR"]);
                string[] userData = ((System.Web.Security.FormsIdentity)(this.Page.User.Identity)).Ticket.UserData.Split(';');
                if (userData.Length > 0)
                {
                    _UserInfoAgencyID = userData[userData.Length - 1].ToString();
                    ViewState["UserInfoAgencyID"] = _UserInfoAgencyID;
                    _UserInfoRepID = userData[userData.Length - 5].ToString();
                    ViewState["UserInfoRepID"] = _UserInfoRepID;
                }
                //MAIG - CH4 - END - Added code to get the AgencyId and RepID from Ticket
                ProductTypes = ((SiteTemplate)Page).OrderService.LookupDataSet("Insurance", "ProductTypes").Tables[CSAAWeb.Constants.INS_Product_Type_Table];
                if (ProductTypes.Rows.Count > 1) AddSelectItem(ProductTypes, CSAAWeb.Constants.PC_SEL_PROD);

                if (Cache[CSAAWeb.Constants.INS_Product_Type_Table] == null)
                {
                    Cache[CSAAWeb.Constants.INS_Product_Type_Table] = ProductTypes;
                }

                _ProductType.DataSource = ProductTypes;
                _ProductType.DataBind();
                //MAIG - CH5 - BEGIN - Added code to identify if the flow is from Payment Confirmation Redirection
                if (Context.Items["RecurringRedirection"] != null)
                {
                   
                    ViewState["RecurringRedirectionFlag"] = true;

                    _RecurringRedirectionFlag = ViewState["RecurringRedirectionFlag"].ToString();

                    PopulateRecurringDetails();
                }
                LinkButton lnkCustomerName = (LinkButton)_NameSearch.FindControl("lnkSearchByCustomerName");
                lnkCustomerName.Visible = false;
                _PolicyNumber.Enabled = false;
                //MAIG - CH5 - END - Added code to identify if the flow is from Payment Confirmation Redirection
            }
            else
            {
                _ProductType_PT = _ProductType.SelectedItem.Value;
                _BillPlan = hdnBillingPlan.Text;
                ProductTypes = ((SiteTemplate)Page).OrderService.LookupDataSet("Insurance", "ProductTypes").Tables[CSAAWeb.Constants.INS_Product_Type_Table];
                DataRow[] productDataRows = ProductTypes.Select("ID LIKE " + "'" + _ProductType_PT + "'");
                foreach (DataRow myDataRow in productDataRows)
                    _ProductType_PC = myDataRow[CSAAWeb.Constants.PaymentCentral_Product_Code_table].ToString();
                policySearchDetails.Visible = true;

                //MAIG - CH6 - BEGIN - Added code to assign the Policy details for invalid policy number
                if (ViewState[CSAAWeb.Constants.PC_InvalidPolicyFlow] != null)
                {
                    if (ViewState[CSAAWeb.Constants.PC_InvalidPolicyFlow].ToString().Equals("True"))
                    {
                        //if (ValidateAdditionalDetails())
                        //{
                        ViewState[CSAAWeb.Constants.PC_InvalidPolicyFName] = txtFirstName.Text;

                        ViewState[CSAAWeb.Constants.PC_InvalidPolicyLName] = txtLastName.Text;

                        ViewState[CSAAWeb.Constants.PC_InvalidPolicyMZip] = txtMailingZip.Text;

                        ViewState[CSAAWeb.Constants.PC_ProductType_PC] = _ProductType.SelectedItem.Value.ToString();

                        policySearchDetails.Visible = false;

                        //}
                    }
                }
                else
                {
                    if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow) == null)
                        policySearchDetails.Visible = true;
                    else
                        policySearchDetails.Visible = false;
                }
                //MAIG - CH6 - END - Added code to assign the Policy details for invalid policy number

            }
            if (ViewState[CSAAWeb.Constants.PC_ProductType_PC] != null)
            {
                if (ViewState[CSAAWeb.Constants.PC_ProductType_PC].ToString().Length > 0)
                {
                    //T&C Changes CH1 - START - Added the below code to assign the Produt type selected.
                    if (_ProductType_PC.Equals(ViewState[CSAAWeb.Constants.PC_ProductType_PC].ToString()))
                    {
                        _ProductType_PC_Text = _ProductType.SelectedItem.Text;
                    }
                    else
                    {
                        DataRow[] productDataRows = ProductTypes.Select("Product_Prefix LIKE " + "'" + "W" + "' AND " + CSAAWeb.Constants.PaymentCentral_Product_Code_table + " LIKE " + "'" + _ProductType_PC + "'");
                        foreach (DataRow myDataRow in productDataRows)
                            _ProductType_PC_Text = myDataRow["Description"].ToString();
                    }
                    //T&C Changes CH1 - END - Added the below code to assign the Produt type selected.
                    _ProductType_PC = ViewState[CSAAWeb.Constants.PC_ProductType_PC].ToString();
                }
            }
            if (ViewState["EnrolledMethod"] != null)
            {
                if (ViewState["EnrolledMethod"].ToString().Length > 0)
                {
                    _EnrolledMethod = ViewState["EnrolledMethod"].ToString();
                }
            }
            if (string.IsNullOrEmpty(BillingPlan))
            {
                if (ViewState[CSAAWeb.Constants.PC_BillingPlan] != null)
                {
                    if (ViewState[CSAAWeb.Constants.PC_BillingPlan].ToString().Length > 0)
                    {

                        BillingPlan = ViewState[CSAAWeb.Constants.PC_BillingPlan].ToString();

                    }
                }
            }
            if (string.IsNullOrEmpty(PolicyNumber))
            {
                if (ViewState[CSAAWeb.Constants.PC_VWSTATE_PolicyNumber] != null)
                {
                    if (ViewState[CSAAWeb.Constants.PC_VWSTATE_PolicyNumber].ToString().Length > 0)
                    {
                        PolicyNumber = ViewState[CSAAWeb.Constants.PC_VWSTATE_PolicyNumber].ToString();

                    }
                }
            }

            //MAIG - CH7 - BEGIN - Added code to retain the properties which is accessed in user controls
            if (_ValidPolicySourceSystem == null)
            {
                if (ViewState["Policy_Source_System"] != null)
                {
                    _ValidPolicySourceSystem = ViewState["Policy_Source_System"].ToString();
                }
            }

            if (_UserInfoAgencyID == null)
            {
                if (ViewState["UserInfoAgencyID"] != null)
                {
                    _UserInfoAgencyID = ViewState["UserInfoAgencyID"].ToString();
                }
            }

            if (string.IsNullOrEmpty(_UserInfoRepID))
            {
                if (ViewState["UserInfoRepID"] != null)
                {
                    _UserInfoRepID = ViewState["UserInfoRepID"].ToString();
                }
            }

            //MAIG - CH7 - END - Added code to retain the properties which is accessed in user controls

            if (_ValidationService_enrollment == null)
            {
                if (ViewState[CSAAWeb.Constants.viewstate_ValidationService_enrollment] != null)
                {
                    if (ViewState[CSAAWeb.Constants.viewstate_ValidationService_enrollment].ToString().Length > 0)
                    {

                        _ValidationService_enrollment = (List<string>)ViewState[CSAAWeb.Constants.viewstate_ValidationService_enrollment];

                    }
                }
            }
            if (_ValidationService_Unenrollment == null)
            {
                if (ViewState[CSAAWeb.Constants.viewstate_ValidationService_Unenrollment] != null)
                {
                    if (ViewState[CSAAWeb.Constants.viewstate_ValidationService_Unenrollment].ToString().Length > 0)
                    {

                        _ValidationService_Unenrollment = (List<string>)ViewState[CSAAWeb.Constants.viewstate_ValidationService_Unenrollment];

                    }
                }
            }
            if (_ValidationService_Modifyenrollment == null)
            {
                if (ViewState[CSAAWeb.Constants.viewstate_ValidationService_Modifyenrollment] != null)
                {
                    if (ViewState[CSAAWeb.Constants.viewstate_ValidationService_Modifyenrollment].ToString().Length > 0)
                    {

                        _ValidationService_Modifyenrollment = (List<string>)ViewState[CSAAWeb.Constants.viewstate_ValidationService_Modifyenrollment];

                    }
                }
            }
            lblErrorMsg.Visible = false;

            if (string.IsNullOrEmpty(_IsEnrolled))
            {
                if (ViewState[CSAAWeb.Constants.PC_IS_ENROLLED] != null)
                {
                    if (ViewState[CSAAWeb.Constants.PC_IS_ENROLLED].ToString().Length > 0)
                    {
                        _IsEnrolled = ViewState[CSAAWeb.Constants.PC_IS_ENROLLED].ToString();

                    }
                }
            }
            //MAIG - CH8 - BEGIN - Added code to retain the properties which is accessed in user controls
            if (string.IsNullOrEmpty(_InvalidPolicyFlow))
            {
                if (ViewState[CSAAWeb.Constants.PC_InvalidPolicyFlow] != null)
                {
                    _InvalidPolicyFlow = ViewState[CSAAWeb.Constants.PC_InvalidPolicyFlow].ToString();
                }
            }

            if (string.IsNullOrEmpty(_InsuredFullName))
            {
                if (ViewState[CSAAWeb.Constants.PC_InsuredFullName] != null)
                {
                    _InsuredFullName = ViewState[CSAAWeb.Constants.PC_InsuredFullName].ToString();
                }
            }

            if (string.IsNullOrEmpty(_InvalidPolicyFName))
            {
                if (ViewState[CSAAWeb.Constants.PC_InvalidPolicyFName] != null)
                {
                    _InvalidPolicyFName = ViewState[CSAAWeb.Constants.PC_InvalidPolicyFName].ToString();
                }
            }
            if (string.IsNullOrEmpty(_InvalidPolicyLName))
            {
                if (ViewState[CSAAWeb.Constants.PC_InvalidPolicyLName] != null)
                {
                    _InvalidPolicyLName = ViewState[CSAAWeb.Constants.PC_InvalidPolicyLName].ToString();
                }
            }
            if (string.IsNullOrEmpty(_InvalidPolicyMZip))
            {
                if (ViewState[CSAAWeb.Constants.PC_InvalidPolicyMZip] != null)
                {
                    _InvalidPolicyMZip = ViewState[CSAAWeb.Constants.PC_InvalidPolicyMZip].ToString();
                }
            }

            if (string.IsNullOrEmpty(_InvalidUnEnrollFlow))
            {
                if (ViewState[CSAAWeb.Constants.PC_InvalidUnEnrollFlow] != null)
                {
                    _InvalidUnEnrollFlow = ViewState[CSAAWeb.Constants.PC_InvalidUnEnrollFlow].ToString();
                    policySearchDetails.Visible = false;
                }
            }

            if (string.IsNullOrEmpty(_RecurringRedirectionFlag))
            {
                if (ViewState["RecurringRedirectionFlag"] != null)
                {
                    if (ViewState["RecurringRedirectionFlag"].ToString().Length > 0)
                    {
                        _RecurringRedirectionFlag = ViewState["RecurringRedirectionFlag"].ToString();
                    }
                }
            }
            //MAIG - CH8 - END - Added code to retain the properties which is accessed in user controls
        }
        #endregion

        //MAIG - CH9 - BEGIN - Added new method to manage the flow from Payment Confirmation to Manage Enrollment page which pre-populates the payment details
        #region DownPaymentToRecurring
        private void PopulateRecurringDetails()
        {
            Dictionary<string, string> PaymentDetails = null;
            if (Context.Items["RecurringRedirection"] != null)
            {
                PaymentDetails = ((Dictionary<string, string>)Context.Items["RecurringRedirection"]);
                List<string> ConfirmationDetails = new List<string>();
                //((Dictionary<string, string>)Context.Items["RecurringRedirection"])["AccountNumber"]
                _ProductType.SelectedValue = PaymentDetails[Constants.PMT_RECURRING_PRODUCTTYPE];
                _ProductType.Enabled = false;
                _PolicyNumber.Text = PaymentDetails[Constants.PMT_RECURRING_ACCOUNTNUMBER];
                PolicyNumber = PaymentDetails[Constants.PMT_RECURRING_ACCOUNTNUMBER];
                //if (ViewState[CSAAWeb.Constants.PC_VWSTATE_PolicyNumber] == null)
                //{
                ViewState[CSAAWeb.Constants.PC_VWSTATE_PolicyNumber] = PolicyNumber;
                //}
                _IsEnrolled = "No";
                //if (ViewState[CSAAWeb.Constants.PC_IS_ENROLLED] == null)
                //{
                ViewState[CSAAWeb.Constants.PC_IS_ENROLLED] = _IsEnrolled;
                //}
                _ValidPolicySourceSystem = PaymentDetails[Constants.PMT_RECURRING_INSTSOURCESYSTEM];
                //if (ViewState["Policy_Source_System"] == null)
                //{
                ViewState["Policy_Source_System"] = PaymentDetails[Constants.PMT_RECURRING_INSTSOURCESYSTEM];
                //}
                _PolicyNumber.Enabled = false;
                PaymentMethod.Visible = true;
                DropDownList dropdown_PaymentType = (DropDownList)PaymentMethod.FindControl("_PaymentType");
                if (!Convert.ToBoolean(PaymentDetails[Constants.PMT_RECURRING_ISDOWNFLOW]))
                {
                    policySearchDetails.Visible = true;
                    invalidPolicyDetails.Visible = false;
                    dtTable.Columns.Add(CSAAWeb.Constants.PC_GRID_POLICY);
                    dtTable.Columns.Add(CSAAWeb.Constants.PC_GRID_Status);
                    dtTable.Columns.Add(CSAAWeb.Constants.PC_Enrolled);
                    dtTable.Columns.Add(CSAAWeb.Constants.PC_BILL_PLAN);
                    dtTable.Columns.Add(CSAAWeb.Constants.PC_MinDue);
                    dtTable.Columns.Add(CSAAWeb.Constants.PC_TotalBalance);
                    dtTable.Columns.Add(CSAAWeb.Constants.PC_Due_Date);
                    dtTable.Columns.Add(CSAAWeb.Constants.PC_Eff_Date);
                    dtTable.Columns.Add(CSAAWeb.Constants.PC_Exp_Date);
                    DataRow drRow = dtTable.NewRow();
                    //MAIGEnhancement CHG0107527 - CH1 - BEGIN - Modified the below Installment payment flow to use the key/value pair with ^ than , which causes issue in Full Name field from RPBS response
                    var RpbsRow = PaymentDetails["RpbsBillingDetails"].Substring(0, PaymentDetails["RpbsBillingDetails"].Length - 1).Split('^');
                    //MAIGEnhancement CHG0107527 - CH1 - END - Modified the below Installment payment flow to use the key/value pair with ^ than , which causes issue in Full Name field from RPBS response
                    Dictionary<string, string> RpbsData = RpbsRow.Select(rec => rec.Split('*'))
                        .ToDictionary(rec => rec[0].Trim(),
                        rec => rec[1].Trim());
                    ViewState[CSAAWeb.Constants.PC_InsuredFullName] = RpbsData[CSAAWeb.Constants.PC_BILL_INS_FULL_NAME];
                    drRow[CSAAWeb.Constants.PC_GRID_POLICY] = PaymentDetails[Constants.PMT_RECURRING_ACCOUNTNUMBER];
                    drRow[CSAAWeb.Constants.PC_GRID_Status] = RpbsData[Constants.PC_BILL_STATUS_DESCRIPTION];
                    /*if (RpbsData[Constants.PC_BILL_STATUS_DESCRIPTION].ToString().Equals(CSAAWeb.Constants.PC_POL_ACTIVE_NOTATION))
                    {
                        drRow[CSAAWeb.Constants.PC_GRID_Status] = CSAAWeb.Constants.PC_POL_ACTIVE;
                    }
                    else if (RpbsData[Constants.PC_BILL_STATUS].ToString().Equals(CSAAWeb.Constants.PC_POL_LAPSED_NOTATION))
                    {
                        drRow[CSAAWeb.Constants.PC_GRID_Status] = CSAAWeb.Constants.PC_POL_LAPSED;
                    }
                    else if (RpbsData[Constants.PC_BILL_STATUS].ToString().Equals(CSAAWeb.Constants.PC_POL_CANCEL_NOTATION))
                    {
                        drRow[CSAAWeb.Constants.PC_GRID_Status] = CSAAWeb.Constants.PC_POL_CANCEL;
                    }*/
                    drRow[CSAAWeb.Constants.PC_Enrolled] = CSAAWeb.Constants.PC_POL_NOTENROLL_STATUS;
                    ViewState[CSAAWeb.Constants.PC_IS_ENROLLED] = CSAAWeb.Constants.PC_POL_NOTENROLL_STATUS;

                    if (RpbsData[Constants.PC_BillingPlan].ToString().Equals(CSAAWeb.Constants.PC_POL_BILL_MONTHLY_NOTATION))
                    {
                        drRow[CSAAWeb.Constants.PC_BILL_PLAN] = CSAAWeb.Constants.PC_POL_BILL_MONTHLY;
                    }
                    else if (RpbsData[Constants.PC_BillingPlan].ToString().Equals(CSAAWeb.Constants.PC_POL_BILL_QUARTERLY_NOTATION))
                    {
                        drRow[CSAAWeb.Constants.PC_BILL_PLAN] = CSAAWeb.Constants.PC_POL_BILL_QUARTERLY;
                    }
                    else if (RpbsData[Constants.PC_BillingPlan].ToString().Equals(CSAAWeb.Constants.PC_POL_BILL_SEMI_NOTATION))
                    {
                        drRow[CSAAWeb.Constants.PC_BILL_PLAN] = CSAAWeb.Constants.PC_POL_BILL_SEMI;
                    }
                    else if (RpbsData[Constants.PC_BillingPlan].ToString().Equals(CSAAWeb.Constants.PC_POL_BILL_ANNUAL_NOTATION))
                    {
                        drRow[CSAAWeb.Constants.PC_BILL_PLAN] = CSAAWeb.Constants.PC_POL_BILL_ANNUAL;
                    }
                    billingplan = drRow[CSAAWeb.Constants.PC_BILL_PLAN].ToString();
                    BillingPlan = billingplan;
                    Context.Items.Add(CSAAWeb.Constants.PC_BillingPlan, BillingPlan);
                    ViewState[CSAAWeb.Constants.PC_BillingPlan] = BillingPlan;
                    drRow[CSAAWeb.Constants.PC_MinDue] = "$" + decimal.Parse(RpbsData[Constants.PC_BILL_Due_Amount].ToString()).ToString("0.00");
                    drRow[CSAAWeb.Constants.PC_TotalBalance] = "$" + decimal.Parse(RpbsData[Constants.PC_BILL_Total_Amount].ToString()).ToString("0.00");
                    _ProductType_PC = PaymentDetails[Constants.PMT_RECURRING_PRODUCTTYPE].ToString().Trim();
                    ViewState[CSAAWeb.Constants.PC_ProductType_PC] = _ProductType_PC;
                    string paymentRestriction = RpbsData[Constants.PC_BILL_PAYMENT_RESTRICTION].ToString();
                    if (RpbsData[Constants.PC_BILL_PAYMENT_RESTRICTION].ToString().ToUpper().Equals("TRUE"))
                    {
                        //CHG0116140 - Payment Restriction - BEGIN - Set the Payment Restriction to label & Disabled the ACH payments
                        lblPaymentRestrict.Text = CSAAWeb.Constants.PC_PAYMENT_RESTRICTION_ERROR_MSG;
                        lblPaymentRestrict.Visible = true;
                        dropdown_PaymentType.Items[2].Enabled = false;
                        //CHG0116140 - Payment Restriction - END - Set the Payment Restriction to label & Disabled the ACH payments
                    }
                    if (!string.IsNullOrEmpty(RpbsData[Constants.PC_BILL_TermEffectiveDate].ToString()))
                    {
                        drRow[Constants.PC_Eff_Date] = Convert.ToDateTime(RpbsData[Constants.PC_BILL_TermEffectiveDate]).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        drRow[Constants.PC_Eff_Date] = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(RpbsData[Constants.PC_BILL_Due_Date].ToString()))
                    {
                        drRow[Constants.PC_Due_Date] = Convert.ToDateTime(RpbsData[Constants.PC_BILL_Due_Date]).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        drRow[CSAAWeb.Constants.PC_Due_Date] = string.Empty;
                    }
                    if (!string.IsNullOrEmpty(RpbsData[Constants.PC_TermExpirationDate].ToString()))
                    {
                        drRow[CSAAWeb.Constants.PC_Exp_Date] = Convert.ToDateTime(RpbsData[Constants.PC_TermExpirationDate]).ToString("MM/dd/yyyy");
                    }
                    else
                    {
                        drRow[CSAAWeb.Constants.PC_Exp_Date] = string.Empty;
                    }
                    dtTable.Rows.Add(drRow);
                    gridEnrollment.DataSource = dtTable.DefaultView;
                    gridEnrollment.DataBind();

                    _ValidationService_enrollment = getValidationDetailsFlow(RpbsData[Constants.PC_BILL_SETUPAUTOPAYREASONCODE], CSAAWeb.Constants.PC_VALIDATEENROLLMENTSERVICE);
                    _ValidationService_enrollment.Add(RpbsData[Constants.PC_BILL_SETUPAUTOPAYEFFDATE]);
                    _ValidationService_enrollment.Add(RpbsData[Constants.PC_BILL_SETUPAUTOPAYREASONCODE]);
                    ViewState[CSAAWeb.Constants.viewstate_ValidationService_enrollment] = _ValidationService_enrollment;

                    if (Convert.ToBoolean(PaymentDetails[Constants.PMT_RECURRING_IS_CC_PAYMENT]))
                    {
                        dropdown_PaymentType.SelectedIndex = 1;
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_CCNUMBER]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_CCType]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_CCEXPMONTHYEAR]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_CCFULLNAME]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_CCZIPCODE]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_CCECTOKEN]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_DOWNEMAILADDRESS]);
                        EnrollCC.ClearFields();
                        dropdown_PaymentType.Enabled = false;
                        EnrollCC.PopulateRecurringDetailsFromPayment(ConfirmationDetails);
                        EnrollCC.DisplayTerms();
                        /*_CCType = PaymentDetails[Constants.PMT_RECURRING_CCType];
                        _CCNumber = PaymentDetails[Constants.PMT_RECURRING_CCNUMBER];
                        _CCName = PaymentDetails[Constants.PMT_RECURRING_CCFULLNAME];
                        _CCExpDate = PaymentDetails[Constants.PMT_RECURRING_CCEXPMONTHYEAR];
                        _CCZipCode = PaymentDetails[Constants.PMT_RECURRING_CCZIPCODE];
                        _PaymentTokenNumber = PaymentDetails[Constants.PMT_RECURRING_CCECTOKEN];
                        UnEnrollCC.assignConfirmationValues(ConfirmationDetails);*/
                        EnrollCC.Visible = true;
                        EnrollECheck.Visible = false;
                        UnEnrollCC.Visible = false;
                        UnEnrollECheck.Visible = false;
                    }
                    else
                    {
                        dropdown_PaymentType.SelectedIndex = 2;
                        //dropdown_PaymentType.Items[1].Enabled = false;
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_ECBANKID]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_CCFULLNAME]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_ECACCOUNTNUMBER]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_ECBANKACNTTYPE]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_CCECTOKEN]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_DOWNEMAILADDRESS]);
                        EnrollECheck.ClearFields();
                        dropdown_PaymentType.Enabled = false;
                        EnrollECheck.PopulateRecurringDetailsFromPayment(ConfirmationDetails);
                        EnrollECheck.DisplayTerms();
                        /*//invoke the bank name
                        _ECRoutingNo = PaymentDetails[Constants.PMT_RECURRING_ECBANKID];
                        _ECAccountNo = PaymentDetails[Constants.PMT_RECURRING_ECACCOUNTNUMBER];
                        _ECaacType = PaymentDetails[Constants.PMT_RECURRING_ECBANKACNTTYPE];
                        _ECAccountHolderName = PaymentDetails[Constants.PMT_RECURRING_CCFULLNAME];
                        _PaymentTokenNumber = PaymentDetails[Constants.PMT_RECURRING_CCECTOKEN];
                        UnEnrollECheck.assignECConfirmValues(ConfirmationDetails);*/
                        EnrollECheck.Visible = true;
                        EnrollCC.Visible = false;
                        UnEnrollCC.Visible = false;
                        UnEnrollECheck.Visible = false;
                    }
                }
                else
                {
                    invalidPolicyDetails.Visible = true;
                    _InvalidPolicyFlow = "True";
                    //if (ViewState[Constants.PC_InvalidPolicyFlow] == null)
                    //{
                    ViewState[Constants.PC_InvalidPolicyFlow] = _InvalidPolicyFlow;
                    //}
                    txtLastName.Text = PaymentDetails[Constants.PMT_RECURRING_DOWNLASTNAME];
                    _InvalidPolicyLName = txtLastName.Text;
                    //if (ViewState[Constants.PC_InvalidPolicyLName] == null)
                    //{
                    ViewState[Constants.PC_InvalidPolicyLName] = _InvalidPolicyLName;
                    //}
                    txtFirstName.Text = PaymentDetails[Constants.PMT_RECURRING_DOWNFIRSTNAME];
                    _InvalidPolicyFName = txtFirstName.Text;
                    //if (ViewState[Constants.PC_InvalidPolicyFName] == null)
                    //{
                    ViewState[Constants.PC_InvalidPolicyFName] = _InvalidPolicyFName;
                    //}
                    txtMailingZip.Text = PaymentDetails[Constants.PMT_RECURRING_DOWNMAILINGZIP];
                    _InvalidPolicyMZip = txtMailingZip.Text;
                    //if (ViewState[Constants.PC_InvalidPolicyMZip] == null)
                    //{
                    ViewState[Constants.PC_InvalidPolicyMZip] = _InvalidPolicyMZip;
                    //}
                    //Display the panel for Invalid policies
                    policySearchDetails.Visible = false;
                    if (Convert.ToBoolean(PaymentDetails[Constants.PMT_RECURRING_IS_CC_PAYMENT]))
                    {
                        dropdown_PaymentType.SelectedIndex = 1;


                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_CCNUMBER]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_CCType]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_CCEXPMONTHYEAR]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_CCFULLNAME]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_CCZIPCODE]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_CCECTOKEN]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_DOWNEMAILADDRESS]);

                        EnrollCC.ClearFields();
                        dropdown_PaymentType.Enabled = false;
                        EnrollCC.PopulateRecurringDetailsFromPayment(ConfirmationDetails);
                        EnrollCC.DisplayTerms();
                        /*_CCType = PaymentDetails[Constants.PMT_RECURRING_CCType];
                        _CCNumber = PaymentDetails[Constants.PMT_RECURRING_CCNUMBER];
                        _CCName = PaymentDetails[Constants.PMT_RECURRING_CCFULLNAME];
                        _CCExpDate = PaymentDetails[Constants.PMT_RECURRING_CCEXPMONTHYEAR];
                        _CCZipCode = PaymentDetails[Constants.PMT_RECURRING_CCZIPCODE];
                        _PaymentTokenNumber = PaymentDetails[Constants.PMT_RECURRING_CCECTOKEN];*/

                        //UnEnrollCC.assignConfirmationValues(ConfirmationDetails);
                        EnrollCC.Visible = true;
                        EnrollECheck.Visible = false;
                        UnEnrollCC.Visible = false;
                        UnEnrollECheck.Visible = false;
                    }
                    else
                    {
                        dropdown_PaymentType.SelectedIndex = 2;
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_ECBANKID]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_CCFULLNAME]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_ECACCOUNTNUMBER]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_ECBANKACNTTYPE]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_CCECTOKEN]);
                        ConfirmationDetails.Add(PaymentDetails[Constants.PMT_RECURRING_DOWNEMAILADDRESS]);
                        EnrollECheck.ClearFields();
                        dropdown_PaymentType.Enabled = false;
                        EnrollECheck.PopulateRecurringDetailsFromPayment(ConfirmationDetails);
                        EnrollECheck.DisplayTerms();
                        /*_ECRoutingNo = PaymentDetails[Constants.PMT_RECURRING_ECBANKID];
                        _ECAccountNo = PaymentDetails[Constants.PMT_RECURRING_ECACCOUNTNUMBER];
                        _ECaacType = PaymentDetails[Constants.PMT_RECURRING_ECBANKACNTTYPE];
                        _ECAccountHolderName = PaymentDetails[Constants.PMT_RECURRING_CCFULLNAME];
                        _PaymentTokenNumber = PaymentDetails[Constants.PMT_RECURRING_CCECTOKEN];
                        UnEnrollECheck.assignECConfirmValues(ConfirmationDetails);*/
                        EnrollECheck.Visible = true;
                        EnrollCC.Visible = false;
                        UnEnrollCC.Visible = false;
                        UnEnrollECheck.Visible = false;
                    }
                }
            }
        }
        #endregion
        //MAIG - CH9 - END - Added new method to manage the flow from Payment Confirmation to Manage Enrollment page which pre-populates the payment details
        private void AddSelectItem(DataTable Dt, string Description)
        {
            DataRow Row = Dt.NewRow();
            Row["ID"] = 0;
            Row["Description"] = Description;
            Dt.Rows.InsertAt(Row, 0);

        }
        #region PaymentType_Index change
        protected void ManageEnrollment_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            DropDownList payment_Type = new DropDownList();
            payment_Type = (DropDownList)sender;
            //CHG0072116 - PC Edit Card Details CH2:START - Hide the edit card details usercontrol depends on payment method.
            if (Convert.ToInt32(payment_Type.SelectedValue) == 3)
            {
                //MAIG - CH10 - BEGIN - Added code to display the Amex and Discover Credit card types only for KIC
                DropDownList Card_Type = (DropDownList)EnrollCC.FindControl("_CardType");
                string ProductType = string.Empty;
                string SourceSystem = _ValidPolicySourceSystem;
                if (!string.IsNullOrEmpty(_ProductType.SelectedItem.Value)) { ProductType = _ProductType.SelectedItem.Value; }
                if (!(_PolicyNumber.Text.Trim().Length == 8 && (ProductType.Equals(MSC.SiteTemplate.ProductCodes.PA.ToString())
                    || ProductType.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString())))
                    && !(SourceSystem.ToUpper().Equals("KIC")))
                {
                    for (int i = Card_Type.Items.Count - 1; i >= 1; i--)
                    {
                        ListItem row = Card_Type.Items[i];
                        if (Config.Setting("CreditCardTypesDisabled").ToString().IndexOf(row.Value.ToString()) < 0)
                        {
                            continue;
                        }
                        else
                        {
                            Card_Type.Items.RemoveAt(i);
                        }
                    }
                }


                if (invalidPolicyDetails.Visible == true)
                    policySearchDetails.Visible = false;
                else
                    policySearchDetails.Visible = true;
                //MAIG - CH10 - END - Added code to display the Amex and Discover Credit card types only for KIC

                EnrollECheck.Visible = false;
                EnrollCC.Visible = true;
                vldSumaary.Visible = true;
                UnEnrollCC.Visible = false;
                UnEnrollECheck.Visible = false;
                EditCC.Visible = false;
                EnrollCC.DisplayTerms();
                EnrollCC.ClearFields();
                lblErrorMsg.Visible = false;
                lblErrorMsg.Text = string.Empty;
            }
            else if (Convert.ToInt32(payment_Type.SelectedValue) == 4)
            {
                //MAIG - CH11 - BEGIN - Added code to hide the policy grid it is an invalid policy
                if (invalidPolicyDetails.Visible == true)
                    policySearchDetails.Visible = false;
                else
                    policySearchDetails.Visible = true;
                //MAIG - CH11 - END - Added code to hide the policy grid it is an invalid policy

                EnrollCC.Visible = false;
                EnrollECheck.Visible = true;
                vldSumaary.Visible = true;
                UnEnrollCC.Visible = false;
                EditCC.Visible = false;
                UnEnrollECheck.Visible = false;
                EnrollECheck.DisplayTerms();
                EnrollECheck.ClearFields();
                lblErrorMsg.Visible = false;
                lblErrorMsg.Text = string.Empty;
                //CHG0072116 - PC Edit Card Details CH2:END - Hide the edit card details usercontrol depends on payment method.
            }
            else if (Convert.ToInt32(payment_Type.SelectedValue) == 0)
            {
                PaymentMethod.Visible = true;
                EnrollECheck.Visible = false;
                EnrollCC.Visible = false;
                lblErrorMsg.Visible = false;
                lblErrorMsg.Text = string.Empty;
            }

        }
        #endregion

        #region ValidateEnrollmentRequestMapping
        #region isnotEnrollProcess
        private void isnotEnrollProcess(string paymentRestriction)
        {
            DropDownList dropdown_PaymentType = (DropDownList)PaymentMethod.FindControl("_PaymentType");
            //MAIG - CH13 - BEGIN - Reset the Error message to empty and hide it
            lblPaymentRestrict.Text = string.Empty;
            lblPaymentRestrict.Visible = false;
            //MAIG - CH13 - END - Reset the Error message to empty and hide it
            if (paymentRestriction.ToUpper().Equals("TRUE"))
            {
                policySearchDetails.Visible = true;
                PaymentMethod.Visible = true;
                if (dropdown_PaymentType != null)
                {
                    dropdown_PaymentType.Items[2].Enabled = false;
                }
                //CHG0116140 - Payment Restriction - BEGIN - Set the Payment Restriction to label
                lblPaymentRestrict.Visible = true;
                lblPaymentRestrict.Text = Constants.PC_PAYMENT_RESTRICTION_ERROR_MSG;
                //CHG0116140 - Payment Restriction - END - Set the Payment Restriction to label
            }
            else
            {
                policySearchDetails.Visible = true;
                PaymentMethod.Visible = true;
            }
            _PolicyNumber.Enabled = false;
            _ProductType.Enabled = false;
        }
        #endregion
        #region isenrollprocess
        //MAIG - CH14 - BEGIN - Added Source System as an paramter & Reset the Error message to empty and hide it
        private void isEnrollProcess(string PolicyNumber, string ProductType, string _ProductType_PC, string paymentRestriction, string sourceSystem)
        {
            lblPaymentRestrict.Text = string.Empty;
            lblPaymentRestrict.Visible = false;
            //MAIG - CH14 - END - Added Source System as an paramter & Reset the Error message to empty and hide it
            DropDownList dropdown_PaymentType = (DropDownList)PaymentMethod.FindControl("_PaymentType");
            if (paymentRestriction.ToUpper().Equals("TRUE"))
            {
                policySearchDetails.Visible = true;
                dropdown_PaymentType.Items[2].Enabled = false;
                //CHG0116140 - Payment Restriction - BEGIN - Set the Payment Restriction to label
                lblPaymentRestrict.Visible = true;
                lblPaymentRestrict.Text = Constants.PC_PAYMENT_RESTRICTION_ERROR_MSG;
                //CHG0116140 - Payment Restriction - BEGIN - Set the Payment Restriction to label

            }
            lbl_PolicySearch_Details.Text = CSAAWeb.Constants.PC_RECUR_DETAIL;
            policySearchDetails.Visible = true;
            retrieveEnrollment = new PCEnrollmentMapping();
            //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Renamed the Autpay Enrollment Response object to Payment Enrollment Response object
            RetrievePaymentEnrollment.retrievePaymentEnrollmentDetailResponse resp = new RetrievePaymentEnrollment.retrievePaymentEnrollmentDetailResponse();
            //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Renamed the Autpay Enrollment Response object to Payment Enrollment Response object
            try
            {
                //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Renamed the Retrieve Autpay Enrollment service method to Retrieve Payment Enrollment Service
                resp = retrieveEnrollment.PCRetrievePaymentEnrollmentService(PolicyNumber, ProductType, _ProductType_PC, sourceSystem);
                //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Renamed the Retrieve Autpay Enrollment service method to Retrieve Payment Enrollment Service
                if (resp != null && !string.IsNullOrEmpty(resp.enrollmentRecord.paymentItem.paymentMethod))
                {
                    _EnrolledMethod = resp.enrollmentRecord.paymentItem.paymentMethod;
                    ViewState["EnrolledMethod"] = _EnrolledMethod;
                    if (resp.enrollmentRecord.paymentItem.paymentMethod.Equals(Config.Setting(CSAAWeb.Constants.PC_CreditCard_Code)))
                    {

                        //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - View Payment Enroll CC
                        UnEnrollPaymentCCView(resp);
                        //CHG0112662 - END - Record Payment Enrollment SOA Service changes - View Payment Enroll CC
                        _PolicyNumber.Enabled = false;
                        _ProductType.Enabled = false;
                    }
                    else
                    {
                        //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - View Payment Enroll E Check
                        UnEnrollPaymentECView(resp);
                        //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - View Payment Enroll E Check
                        _PolicyNumber.Enabled = false;
                        _ProductType.Enabled = false;
                    }
                }
                else
                {
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                    //MAIG - CH16 - BEGIN - Added code to hide the invalid flow 
                    invalidPolicyDetails.Visible = false;
                    //MAIG - CH16 - END - Added code to hide the invalid flow 
                    _PolicyNumber.Enabled = true;
                    _ProductType.Enabled = true;
                }
            }
            catch (FaultException faultExp)
            {
                string errFriendlyMsg = string.Empty, errMsg = string.Empty, errCode = string.Empty, appendErrMsg = string.Empty;
                if (Config.Setting("Logging.RetrieveEnrollment") == "1")
                {
                    if (resp != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(resp.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, resp);
                        Logger.Log("Enrollment Response from Payment Central - Retrieve Enrollment Response : \r\n" + writerRes);
                    }
                }
                //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Added new namespace to ErrorInfo object
                FaultException<RetrievePaymentEnrollment.www.aaancnuit.com.aaancnu_common_version2.ErrorInfo> errInfo = (FaultException<RetrievePaymentEnrollment.www.aaancnuit.com.aaancnu_common_version2.ErrorInfo>)faultExp;
                //CHG0112662 - End - Record Payment Enrollment SOA Service changes - Added new namespace to ErrorInfo object
                if (errInfo.Detail != null)
                {
                    foreach (XmlNode node in errInfo.Detail.Nodes)
                    {
                        if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_FRIENDLY_ERROR_MSG))
                            errFriendlyMsg = node.InnerText;
                        if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_ERROR_MSG_TEXT))
                            errMsg = node.InnerText;
                        if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_ERROR_CODE))
                            errCode = node.InnerText;
                    }
                }
                if (errCode == CSAAWeb.Constants.ERR_CODE_BUSINESS_EXCEPTION)
                {
                    appendErrMsg = CSAAWeb.Constants.PC_ERR_BUSINESS_EXCEPTION_ENROLL + CSAAWeb.Constants.ERR_CODE_BUSINESS_EXCEPTION + "-" + errMsg;
                }
                else
                {
                    appendErrMsg = errCode + "-" + CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                }
                Logger.Log(errCode + " " + appendErrMsg);
                lblErrorMsg.Visible = true;
                //MAIG - CH17 - BEGIN - Added code to hide the invalid flow and display the Error Code only once
                //lblErrorMsg.Text = appendErrMsg.ToString() + " (" + errCode.ToString() + ")";
                lblErrorMsg.Text = appendErrMsg.ToString();
                invalidPolicyDetails.Visible = false;
                //MAIG - CH17 - END - Added code to hide the invalid flow and display the Error Code only once
                _PolicyNumber.Enabled = true;
                _ProductType.Enabled = true;
            }
            catch (SoapException soapExp)
            {
                if (Config.Setting("Logging.RetrieveEnrollment") == "1")
                {
                    if (resp != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(resp.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, resp);
                        Logger.Log("Enrollment Response from Payment Central - Retrieve Enrollment Response : \r\n" + writerRes);
                    }
                }
                if (soapExp.Message != null)
                {
                    Logger.Log(soapExp.Message);
                    lblErrorMsg.Text = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                }
                _PolicyNumber.Enabled = true;
                _ProductType.Enabled = true;
            }
            catch (Exception exp)
            {
                if (Config.Setting("Logging.RetrieveEnrollment") == "1")
                {
                    if (resp != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(resp.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, resp);
                        Logger.Log("Enrollment Response from Payment Central - Retrieve Enrollment Response : \r\n" + writerRes);
                    }
                }
                if (exp.Message != null)
                {
                    Logger.Log(exp.Message);
                    lblErrorMsg.Text = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                    _PolicyNumber.Enabled = true;
                    _ProductType.Enabled = true;
                }
            }
        }
        #endregion
        #region  Button search click
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            policySearchDetails.Visible = false;
            EnrollECheck.Visible = false;
            EnrollCC.Visible = false;
            UnEnrollCC.Visible = false;
            UnEnrollECheck.Visible = false;
            //CHG0072116 - PC Edit Card Details CH3:START - Hide the edit card details usercontrol when user clik the button "search".
            EditCC.Visible = false;
            //CHG0072116 - PC Edit Card Details CH3:END - Hide the edit card details usercontrol when user clik the button "search".

            DropDownList drpdownPaymentType = (DropDownList)PaymentMethod.FindControl("_PaymentType");
            bool flag = true;
            if (drpdownPaymentType != null)
            {
                drpdownPaymentType.SelectedIndex = 0;
            }
            if (_ProductType.SelectedIndex == 0 || _PolicyNumber.Text.Trim().Equals(string.Empty))
            {
                vldSumaary.Visible = true;

            }
            // CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Updated the below condition to avoid displaying the Error message on display of Search results grid - Dec 2016
            else if (_PolicyNumber.Text.Length < 4)
            {
                vldSumaary.Visible = false;
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = Constants.PC_INVALID_POLICY;
                //MAIG - CH18 - BEGIN - Added code to hide the invalid flow 
                invalidPolicyDetails.Visible = false;
                //MAIG - CH18 - END - Added code to hide the invalid flow 
            }
            // CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Updated the below condition to avoid displaying the Error message on display of Search results grid - Dec 2016
            else
            {
                vldSumaary.Visible = false;
            }
            List<string> validateEnrollmentResponseList = new List<string>();
            List<string> validateFlowResponse = new List<string>();
            // Call the Billing Summary Lookup Service and then bind in a DataGrid gridEnrollment
            if (_isValid)
            {
                try
                {
                    DateTime effDate = DateTime.MinValue;
                    DateTime dueDate = DateTime.MinValue;
                    DateTime expDate = DateTime.MinValue;
                    string ProductType, PC_ProductCode = string.Empty;
                    ProductType = _ProductType.SelectedItem.Value;
                    PolicyNumber = _PolicyNumber.Text.Trim();
                    
                    flag = validPolicyProduct(_PolicyNumber.Text.ToUpper().Trim(), _ProductType.SelectedItem.Value);
                    
                    if (flag)
                    {
                        DataRow[] productDataRows = ProductTypes.Select("ID LIKE " + "'" + ProductType + "'");
                        foreach (DataRow myDataRow in productDataRows)
                            PC_ProductCode = myDataRow[CSAAWeb.Constants.PaymentCentral_Product_Code_table].ToString();
                        string Payment_Plan = string.Empty;
                        _ProductType_PT = PC_ProductCode;
                        string srcSystem = Obj.DataSource(ProductType, PolicyNumber.Length);
                        HtmlTable CriteriaParameters = (HtmlTable)_NameSearch.FindControl("table1");
                        if (!string.IsNullOrEmpty(HiddenSelectedDuplicatePolicy.Text) && !(CriteriaParameters.Visible))
                        {
                            if (ViewState["DuplicateRpbsData"] != null)
                            {
                                dt = (DataTable)ViewState["DuplicateRpbsData"];
                            }
                            DataRow[] dr = dt.Select("SOURCE_SYSTEM ='" + HiddenSelectedDuplicatePolicy.Text + "'");
                            DataRow newRow = dt.NewRow();
                            newRow.ItemArray = dr[0].ItemArray;
                            dt.Rows.Remove(dr[0]);
                            dt.Rows.InsertAt(newRow, 0);
                            for (int i = dt.Rows.Count - 1; i >= 0; i--)
                            {
                                DataRow row = dt.Rows[i];
                                if (row["SOURCE_SYSTEM"].ToString().ToLower().Equals(HiddenSelectedDuplicatePolicy.Text.ToLower()))
                                {
                                    continue;
                                }
                                else
                                {
                                    row.Delete();
                                }
                            }
                            _NameSearch.Visible = false;
                        }
                        else
                        {
                            HiddenSelectedDuplicatePolicy.Text = "";
                            dt = BillingSummary.checkPolicy(PolicyNumber, PC_ProductCode, Page.User.Identity.Name, srcSystem);
                        }

                        ViewState[CSAAWeb.Constants.PC_VWSTATE_PolicyNumber] = PolicyNumber;
                        if (dt != null && dt.Rows.Count > 1)
                        {
                            //Logic to call the SearchResults PopUp window and selct one.
                            Panel pnlCustomerName = (Panel)_NameSearch.FindControl("pnlCustomerNameMain");
                            LinkButton lnkCustomerName = (LinkButton)_NameSearch.FindControl("lnkSearchByCustomerName");
                            _ProductType.Enabled = false;
                            _ProductType.SelectedValue = "0";
                            _PolicyNumber.Enabled = false;
                            _PolicyNumber.Text = "";
                            PlaceHolder resultsGrid = (PlaceHolder)_NameSearch.FindControl("Results");
                            _NameSearch.Visible = true;
                            lnkCustomerName.Visible = false;
                            pnlCustomerName.Visible = true;
                            CriteriaParameters.Visible = false;
                            resultsGrid.Visible = true;
                            _NameSearch.PopulateRpbsData(dt);
                            //vldProductType.IsValid = false;
                            //Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "AnotherFunction();", true);
                            //this.Parent.FindControl("PageValidator2").Visible = false;
                            // Context.Items.Add("DuplicatePolicyData", string.Format("{0}|{1}|{2}", tempPolicyNumber, PC_ProductCode, PC_DataSource));
                            ViewState.Add("DuplicateData", string.Format("{0}|{1}|{2}", PolicyNumber, PC_ProductCode, srcSystem));
                            ViewState.Add("DuplicateRpbsData", dt);

                            //Logic to display popup if there are many records returned from RPBS Service
                        }
                        else
                        {
                            //MAIG - CH22 - END - Added code to delete the non-selected row if there are multiple response from RPBS
                            if (dt.Rows.Count > 0 && !dt.Rows[0]["ErrorCode"].ToString().Equals("1"))
                            {
                                if (dt != null)
                                {
                                    ViewState[CSAAWeb.Constants.PC_VWSTATE_PolicyNumber] = dt.Rows[0][CSAAWeb.Constants.PC_BILL_POL_NUMBER].ToString();
                                    //MAIG - CH23 - BEGIN - Removed the logic that invokes the Validate Enrollment Service and gets the Enrollment validation details from RPBS Service
                                    ViewState[CSAAWeb.Constants.PC_InsuredFullName] = dt.Rows[0]["INS_FULL_NME"].ToString();

                                    ViewState["Policy_Source_System"] = dt.Rows[0]["Source_System"].ToString();
                                    /*retrieveEnrollment = new PCEnrollmentMapping();
                                    validateEnrollmentRequest req = new validateEnrollmentRequest();
                                    req = ValidateEnrollmentRequestMapping(dt);
                                    //Validate Enrollment Service                         
                                    validateEnrollmentResponseList = retrieveEnrollment.PCEnrollmentValidation(req);*/
                                    validateEnrollmentResponseList.Add(dt.Rows[0][Constants.PC_BILL_SETUPAUTOPAYREASONCODE].ToString());
                                    validateEnrollmentResponseList.Add(dt.Rows[0][Constants.PC_BILL_UPDATEAUTOPAYREASONCODE].ToString());
                                    validateEnrollmentResponseList.Add(dt.Rows[0][Constants.PC_BILL_CANCELAUTOPAYREASONCODE].ToString());
                                    validateEnrollmentResponseList.Add(dt.Rows[0][Constants.PC_BILL_SETUPAUTOPAYEFFDATE].ToString());
                                    validateEnrollmentResponseList.Add(dt.Rows[0][Constants.PC_BILL_UPDATEAUTOPAYEFFDATE].ToString());
                                    validateEnrollmentResponseList.Add(dt.Rows[0][Constants.PC_BILL_CANCELAUTOPAYEFFDATE].ToString());
                                    _DataSource = dt.Rows[0][Constants.PC_BILL_SOURCE_SYSTEM].ToString();
                                    //MAIG - CH23 - END - Removed the logic that invokes the Validate Enrollment Service and gets the Enrollment validation details from RPBS Service
                                    if (validateEnrollmentResponseList != null && !(validateEnrollmentResponseList.Count < 3))
                                    {
                                        _ValidationService_enrollment = getValidationDetailsFlow(validateEnrollmentResponseList[0], CSAAWeb.Constants.PC_VALIDATEENROLLMENTSERVICE);
                                        _ValidationService_enrollment.Add(validateEnrollmentResponseList[3]);
                                        _ValidationService_enrollment.Add(validateEnrollmentResponseList[0]);
                                        _ValidationService_Modifyenrollment = getValidationDetailsFlow(validateEnrollmentResponseList[1], CSAAWeb.Constants.PC_VALIDATEMODIFYENROLLMENTSERVICE);
                                        _ValidationService_Modifyenrollment.Add(validateEnrollmentResponseList[4]);
                                        _ValidationService_Modifyenrollment.Add(validateEnrollmentResponseList[1]);
                                        _ValidationService_Unenrollment = getValidationDetailsFlow(validateEnrollmentResponseList[2], CSAAWeb.Constants.PC_VALIDATEUNENROLLMENTSERVICE);
                                        _ValidationService_Unenrollment.Add(validateEnrollmentResponseList[5]);
                                        _ValidationService_Unenrollment.Add(validateEnrollmentResponseList[2]);
                                        ViewState[CSAAWeb.Constants.viewstate_ValidationService_enrollment] = _ValidationService_enrollment;
                                        ViewState[CSAAWeb.Constants.viewstate_ValidationService_Modifyenrollment] = _ValidationService_Modifyenrollment;
                                        ViewState[CSAAWeb.Constants.viewstate_ValidationService_Unenrollment] = _ValidationService_Unenrollment;


                                        Payment_Plan = Convert.ToString(dt.Rows[0][CSAAWeb.Constants.PC_BIL_PaymentPlan]);
                                        if ((string.IsNullOrEmpty(Payment_Plan) && Payment_Plan.ToUpper().Equals("AUTO")) || dt.Rows[0]["AutoPay"].ToString().ToUpper() == "TRUE")
                                        {
                                            isEnroll = true;
                                        }
                                        dtTable.Columns.Add(CSAAWeb.Constants.PC_GRID_POLICY);
                                        dtTable.Columns.Add(CSAAWeb.Constants.PC_GRID_Status);
                                        dtTable.Columns.Add(CSAAWeb.Constants.PC_Enrolled);
                                        dtTable.Columns.Add(CSAAWeb.Constants.PC_BILL_PLAN);
                                        dtTable.Columns.Add(CSAAWeb.Constants.PC_MinDue);
                                        dtTable.Columns.Add(CSAAWeb.Constants.PC_TotalBalance);
                                        dtTable.Columns.Add(CSAAWeb.Constants.PC_Due_Date);
                                        dtTable.Columns.Add(CSAAWeb.Constants.PC_Eff_Date);
                                        dtTable.Columns.Add(CSAAWeb.Constants.PC_Exp_Date);
                                        DataRow drRow = dtTable.NewRow();

                                        drRow[CSAAWeb.Constants.PC_GRID_POLICY] = dt.Rows[0][CSAAWeb.Constants.PC_BILL_POL_NUMBER].ToString();

                                        drRow[CSAAWeb.Constants.PC_GRID_Status] = Convert.ToString(dt.Rows[0][Constants.PC_BILL_STATUS_DESCRIPTION]);

                                        if (!isEnroll)
                                        {
                                            drRow[CSAAWeb.Constants.PC_Enrolled] = CSAAWeb.Constants.PC_POL_NOTENROLL_STATUS;
                                            ViewState[CSAAWeb.Constants.PC_IS_ENROLLED] = CSAAWeb.Constants.PC_POL_NOTENROLL_STATUS;
                                        }
                                        else
                                        {
                                            drRow[CSAAWeb.Constants.PC_Enrolled] = CSAAWeb.Constants.PC_POL_ENROLL_STATUS;
                                            ViewState[CSAAWeb.Constants.PC_IS_ENROLLED] = CSAAWeb.Constants.PC_POL_ENROLL_STATUS;
                                        }
                                        if (dt.Rows[0][CSAAWeb.Constants.PC_BillingPlan].ToString().Equals(CSAAWeb.Constants.PC_POL_BILL_MONTHLY_NOTATION))
                                        {
                                            drRow[CSAAWeb.Constants.PC_BILL_PLAN] = CSAAWeb.Constants.PC_POL_BILL_MONTHLY;
                                        }
                                        else if (dt.Rows[0][CSAAWeb.Constants.PC_BillingPlan].ToString().Equals(CSAAWeb.Constants.PC_POL_BILL_QUARTERLY_NOTATION))
                                        {
                                            drRow[CSAAWeb.Constants.PC_BILL_PLAN] = CSAAWeb.Constants.PC_POL_BILL_QUARTERLY;
                                        }
                                        else if (dt.Rows[0][CSAAWeb.Constants.PC_BillingPlan].ToString().Equals(CSAAWeb.Constants.PC_POL_BILL_SEMI_NOTATION))
                                        {
                                            drRow[CSAAWeb.Constants.PC_BILL_PLAN] = CSAAWeb.Constants.PC_POL_BILL_SEMI;
                                        }
                                        else if (dt.Rows[0][CSAAWeb.Constants.PC_BillingPlan].ToString().Equals(CSAAWeb.Constants.PC_POL_BILL_ANNUAL_NOTATION))
                                        {
                                            drRow[CSAAWeb.Constants.PC_BILL_PLAN] = CSAAWeb.Constants.PC_POL_BILL_ANNUAL;
                                        }
                                        billingplan = drRow[CSAAWeb.Constants.PC_BILL_PLAN].ToString();
                                        BillingPlan = billingplan;
                                        Context.Items.Add(CSAAWeb.Constants.PC_BillingPlan, BillingPlan);
                                        ViewState[CSAAWeb.Constants.PC_BillingPlan] = BillingPlan;
                                        drRow[CSAAWeb.Constants.PC_MinDue] = "$" + decimal.Parse(dt.Rows[0][CSAAWeb.Constants.PC_BILL_Due_Amount].ToString()).ToString("0.00");
                                        drRow[CSAAWeb.Constants.PC_TotalBalance] = "$" + decimal.Parse(dt.Rows[0][CSAAWeb.Constants.PC_BILL_Total_Amount].ToString()).ToString("0.00");
                                        _ProductType_PC = dt.Rows[0][CSAAWeb.Constants.PC_BILL_POL_TYPE].ToString().Trim();
                                        ViewState[CSAAWeb.Constants.PC_ProductType_PC] = _ProductType_PC;
                                        string paymentRestriction = dt.Rows[0][CSAAWeb.Constants.PC_BILL_PAYMENT_RESTRICTION].ToString();
                                        if (dt.Rows[0][CSAAWeb.Constants.PC_BILL_PAYMENT_RESTRICTION].ToString().ToUpper().Equals("TRUE"))
                                        {
                                             //CHG0116140 -Begin- Payment Restricition - Set Payment Restriction message to the label
                                            lblPaymentRestrict.Text = CSAAWeb.Constants.PC_PAYMENT_RESTRICTION_ERROR_MSG;
                                            //CHG0116140 -End- Payment Restricition - Set Payment Restriction message to the label
                                        }
                                        if (!string.IsNullOrEmpty(dt.Rows[0][CSAAWeb.Constants.PC_BILL_TermEffectiveDate].ToString()))
                                        {
                                            effDate = Convert.ToDateTime(dt.Rows[0][CSAAWeb.Constants.PC_BILL_TermEffectiveDate].ToString());
                                            drRow[CSAAWeb.Constants.PC_Eff_Date] = effDate.ToString("MM/dd/yyyy");
                                        }
                                        else
                                        {
                                            drRow[CSAAWeb.Constants.PC_Eff_Date] = string.Empty;
                                        }
                                        if (!string.IsNullOrEmpty(dt.Rows[0][CSAAWeb.Constants.PC_BILL_Due_Date].ToString()))
                                        {
                                            dueDate = Convert.ToDateTime(dt.Rows[0][CSAAWeb.Constants.PC_BILL_Due_Date].ToString());
                                            drRow[CSAAWeb.Constants.PC_Due_Date] = dueDate.ToString("MM/dd/yyyy");
                                        }
                                        else
                                        {
                                            drRow[CSAAWeb.Constants.PC_Due_Date] = string.Empty;
                                        }
                                        if (!string.IsNullOrEmpty(dt.Rows[0][CSAAWeb.Constants.PC_TermExpirationDate].ToString()))
                                        {
                                            expDate = Convert.ToDateTime(dt.Rows[0][CSAAWeb.Constants.PC_TermExpirationDate].ToString());
                                            drRow[CSAAWeb.Constants.PC_Exp_Date] = expDate.ToString("MM/dd/yyyy");
                                        }
                                        else
                                        {
                                            drRow[CSAAWeb.Constants.PC_Exp_Date] = string.Empty;
                                        }


                                        dtTable.Rows.Add(drRow);
                                        gridEnrollment.DataSource = dtTable.DefaultView;
                                        gridEnrollment.DataBind();
                                        if (!isEnroll && _ValidationService_enrollment[1].Equals(CSAAWeb.Constants.PC_YES_NOTATION))
                                        {
                                            isnotEnrollProcess(paymentRestriction);
                                        }
                                        else if (isEnroll && _ValidationService_Modifyenrollment[1].Equals(CSAAWeb.Constants.PC_YES_NOTATION))
                                        {
                                            //MAIG - CH25 - BEGIN - Added Source System as an paramter to isEnrollProcess method
                                            //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Start
                                            isEnrollProcess(dt.Rows[0][CSAAWeb.Constants.PC_BILL_POL_NUMBER].ToString(), ProductType, _ProductType_PC, paymentRestriction, dt.Rows[0]["Source_System"].ToString());
                                            //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - End
                                            //MAIG - CH25 - END - Added Source System as an paramter to isEnrollProcess method
                                        }
                                        else
                                        {
                                            policySearchDetails.Visible = true;
                                            PaymentMethod.Visible = false;
                                            string errMsg = string.Empty;
                                            if (!isEnroll)
                                            {
                                                if (_ValidationService_enrollment[1].Equals(CSAAWeb.Constants.PC_NO_NOTATION))
                                                {
                                                    errMsg = _ValidationService_enrollment[0];
                                                    Logger.Log(CSAAWeb.Constants.PC_VALIDENROLL_FAILMSG + errMsg);
                                                    //MAIG - CH26 - BEGIN - Added Source System as an paramter to isEnrollProcess method
                                                    lblPaymentRestrict.Visible = true;
                                                    lblPaymentRestrict.Text = errMsg;
                                                    _PolicyNumber.Enabled = true;
                                                    _ProductType.Enabled = true;
                                                }
                                                else
                                                {
                                                    isnotEnrollProcess(paymentRestriction);
                                                }
                                            }
                                            else
                                            {
                                                if (_ValidationService_Modifyenrollment[1].Equals(CSAAWeb.Constants.PC_NO_NOTATION))
                                                {
                                                    //CHG0072116 - PC Edit Card Details CH4 - Modified the enrollment message to modify enrollment message to get the correct message for modification scenario
                                                    errMsg = _ValidationService_Modifyenrollment[0];
                                                    Logger.Log(CSAAWeb.Constants.PC_VALIDENROLL_FAILMSG + errMsg);
                                                    lblErrorMsg.Visible = true;
                                                    lblErrorMsg.Text = errMsg;
                                                    _PolicyNumber.Enabled = true;
                                                    _ProductType.Enabled = true;
                                                }
                                                else
                                                {
                                                    //MAIG - CH27 - BEGIN - Added Source System as an paramter to isEnrollProcess method
                                                    //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Start
                                                    isEnrollProcess(dt.Rows[0][CSAAWeb.Constants.PC_BILL_POL_NUMBER].ToString(), ProductType, _ProductType_PC, paymentRestriction, dt.Rows[0]["Source_System"].ToString());
                                                    //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - End
                                                    //MAIG - CH27 - END - Added Source System as an paramter to isEnrollProcess method

                                                }
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lblErrorMsg.Visible = true;
                                        if (validateEnrollmentResponseList.Count == 2)
                                        {
                                            lblErrorMsg.Text = validateEnrollmentResponseList[1].ToString() + " (" + validateEnrollmentResponseList[0].ToString() + ")";
                                            Logger.Log(lblErrorMsg.Text.ToString());
                                        }
                                        else
                                        {
                                            Logger.Log(CSAAWeb.Constants.PC_VALIDENROLL_ERR);
                                            lblErrorMsg.Text = Constants.PC_ERR_RUNTIME_EXCEPTION;
                                        }

                                        _PolicyNumber.Enabled = true;
                                        _ProductType.Enabled = true;
                                    }
                                }
                            }
                            //MAIG - CH28 - BEGIN - Added logic to handle if the entered policy number is invalid
                            else
                            {
                                bool KicAuto = false;
                                bool KicHome = false;
                                bool HDESHome = false;
                                bool PUP = false;
                                if (Config.Setting("Invalid_KIC_Auto_Allowed").ToString().Equals("1"))
                                {
                                    KicAuto = (PolicyNumber.Trim().Length == 8 && PC_ProductCode.Equals(MSC.SiteTemplate.ProductCodes.PA.ToString())) ? true : false;
                                }
                                if (Config.Setting("Invalid_KIC_Home_Allowed").ToString().Equals("1"))
                                {
                                    KicHome = (PolicyNumber.Trim().Length == 8 && PC_ProductCode.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString())) ? true : false;
                                }
                                if (Config.Setting("Invalid_HDES_Home_Allowed").ToString().Equals("1"))
                                {
                                    HDESHome = (PolicyNumber.Trim().Length == 7 && PC_ProductCode.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString())) ? true : false;
                                }
                                if (Config.Setting("Invalid_PUP_Allowed").ToString().Equals("1"))
                                {
                                    PUP = (PolicyNumber.Trim().Length == 7 && PC_ProductCode.Equals(MSC.SiteTemplate.ProductCodes.PU.ToString())) ? true : false;
                                }
                                if ((dt.Rows[0]["ErrorCode"].ToString().Equals("1")) && (dt.Rows[0]["setupAutoPayReasonCode"].ToString().Equals("ELIG_NEED_VRFY")))
                                {
                                    //if ((PolicyNumber.Length == 7 && ((PC_ProductCode.Equals("PU")) || (PC_ProductCode.Equals("HO")))) || 
                                    //   (PolicyNumber.Length == 8 && (PC_ProductCode.Equals("PA"))))
                                    if (KicAuto || KicHome || HDESHome || PUP)
                                    {
                                        invalidPolicyDetails.Visible = true;
                                        policySearchDetails.Visible = false;
                                        PaymentMethod.Visible = true;
                                        lblErrorMsg.Visible = true;
                                        lblErrorMsg.Text = Constants.PC_INVALID_POLICY_FLOW_MESSAGE;
                                        _PolicyNumber.Enabled = false;
                                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "AnotherFunction();", true);
                                        _ProductType.Enabled = false;
                                        ////Context.Items.Add("Invalid_PolicyFlow", "True");
                                        ViewState[CSAAWeb.Constants.PC_InvalidPolicyFlow] = "True";
                                        string srcsystem = string.Empty;
                                        if (PC_ProductCode.ToUpper().ToString().Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()) && (PolicyNumber.Length == 7))
                                        {
                                            srcsystem = Config.Setting("DataSource.POESHome");
                                        }
                                        else if ((PC_ProductCode.ToUpper().ToString().Equals(MSC.SiteTemplate.ProductCodes.PA.ToString()) || PC_ProductCode.ToUpper().ToString().Equals(MSC.SiteTemplate.ProductCodes.HO.ToString())) && (PolicyNumber.Length == 8))
                                        {
                                            srcsystem = "KIC";
                                        }
                                        else if (PC_ProductCode.ToUpper().ToString().Equals(MSC.SiteTemplate.ProductCodes.PU.ToString()) && (PolicyNumber.Length == 7))
                                        {
                                            srcsystem = Config.Setting("DataSource.PUP");
                                        }
                                        _ValidPolicySourceSystem = srcsystem;
                                        //if (ViewState["Policy_Source_System"] == null)
                                        //{
                                        ViewState["Policy_Source_System"] = srcsystem;
                                        //}
                                    }
                                }
                                else if ((dt.Rows[0]["ErrorCode"].ToString().Equals("1")) && (dt.Rows[0]["cancelAutoPayReasonCode"].ToString().Equals("ELIG_NEED_VRFY")))
                                {
                                    //if ((PolicyNumber.Length == 7 && ((PC_ProductCode.Equals("PU")) || (PC_ProductCode.Equals("HO")))) ||
                                    //  (PolicyNumber.Length == 8 && (PC_ProductCode.Equals("PA"))))
                                    if (KicAuto || KicHome || HDESHome || PUP)
                                    {
                                        #region Grid for Invalid Policy - Hiding since not much info will be available

                                        #endregion

                                        ViewState[CSAAWeb.Constants.PC_InvalidUnEnrollFlow] = "True";
                                        _InvalidUnEnrollFlow = "True";

                                        //string sourceSystem = Obj.DataSource(ProductType, PolicyNumber.Length);
                                        string srcsystem = string.Empty;
                                        if (PC_ProductCode.ToUpper().ToString().Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()) && (PolicyNumber.Length == 7))
                                        {
                                            srcsystem = Config.Setting("DataSource.POESHome");
                                        }
                                        else if ((PC_ProductCode.ToUpper().ToString().Equals(MSC.SiteTemplate.ProductCodes.PA.ToString()) || PC_ProductCode.ToUpper().ToString().Equals(MSC.SiteTemplate.ProductCodes.HO.ToString())) && (PolicyNumber.Length == 8))
                                        {
                                            srcsystem = "KIC";
                                        }
                                        else if (PC_ProductCode.ToUpper().ToString().Equals(MSC.SiteTemplate.ProductCodes.PU.ToString()) && (PolicyNumber.Length == 7))
                                        {
                                            srcsystem = Config.Setting("DataSource.PUP");
                                        }
                                        _ValidPolicySourceSystem = srcsystem;
                                        //if (ViewState["Policy_Source_System"] == null)
                                        //{
                                        ViewState["Policy_Source_System"] = srcsystem;
                                        //}
                                        //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Start
                                        isEnrollProcess(dt.Rows[0][CSAAWeb.Constants.PC_BILL_POL_NUMBER].ToString(), ProductType, _ProductType_PC, dt.Rows[0]["Payment_Restriction"].ToString(), dt.Rows[0]["Source_System"].ToString());
                                        //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - End
                                        policySearchDetails.Visible = false;
                                    }
                                }
                                else
                                {
                                    //Displays the Error message
                                    PaymentMethod.Visible = false;
                                    policySearchDetails.Visible = false;
                                    lblErrorMsg.Visible = true;
                                    //CHG0118686 - PRB0045696 - Message Changes - Start - FR1,FR2//
                                    lblErrorMsg.Text = dt.Rows[0]["ERR_MSG"].ToString() + " (" + dt.Rows[0]["PC_ErrorCode"].ToString() + ")";
                                    //CHG0118686 - PRB0045696 - Message Changes - Start - FR1,FR2//
                                    _PolicyNumber.Enabled = true;
                                    _ProductType.Enabled = true;
                                }
                                //MAIG - CH28 - END - Added logic to handle if the entered policy number is invalid
                            }
                        }
                    }
                    else
                    {
                        lblErrorMsg.Visible = true;
                        lblErrorMsg.Text = Constants.PC_INVALID_POLICY;

                    }

                }
                catch (Exception ex)
                {
                    policySearchDetails.Visible = false;
                    PaymentMethod.Visible = false;
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                    Logger.Log(ex.ToString());
                    Logger.Log(lblErrorMsg.Text.ToString());
                    _PolicyNumber.Enabled = true;
                    _ProductType.Enabled = true;
                }
            }

        }
        #endregion
        #region Get validation flow
        private List<string> getValidationDetailsFlow(string res, string operation)
        {
            List<string> stringReturn = new List<string>();
            string[] sArray = new string[5];
            string msg = string.Empty, action = string.Empty;
            XmlDataDocument xmldocOrder = new XmlDataDocument();
            XDocument loaded = XDocument.Load(Config.Setting("ValidateXml"));
            List<string> fromXML = (from c in loaded.Descendants("Code")
                                    where c.Element("Value").Value.Trim().ToUpper() == res &&
                                          c.Element("Operation").Value.Trim().ToUpper() == operation.Trim().ToUpper()
                                    select
                                       c.Element("Message").Value + "/" + c.Element("Action").Value
            ).ToList<string>();

            if (fromXML.Count > 0)
            {

                foreach (string s in fromXML)
                {
                    if (!string.IsNullOrEmpty(s))
                    {
                        if (s.Contains("/"))
                        {
                            sArray = s.Trim().Split('/');
                            stringReturn = sArray.ToList<string>();
                        }
                    }
                }
            }
            return stringReturn;
        }
        #endregion
        #region View Enroll CC details
        private void UnEnrollCCView(RetrieveEnrollment.retrieveAutoPayEnrollmentDetailResponse res)
        {
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.card.type))
            {
                _CCType = res.enrollmentRecord.paymentItem.card.type.ToUpper();
                if (_CCType.Equals(CSAAWeb.Constants.PC_CRD_TYPE_MASTER))
                {
                    _CCType = CSAAWeb.Constants.PC_CRD_TYPE_MASTER_EXPANSION;
                }
                //MAIG - CH38 - BEGIN - Added logic to check if the Card Type is Amex or Discover
                else if (_CCType.Equals(CSAAWeb.Constants.PC_CRD_TYPE_AMEX))
                {
                    _CCType = CSAAWeb.Constants.PC_CRD_TYPE_AMEX_EXPANSION;
                }
                else if (_CCType.Equals(CSAAWeb.Constants.PC_CRD_TYPE_DISC))
                {
                    _CCType = CSAAWeb.Constants.PC_CRD_TYPE_DISC_EXPANSION;
                }
                //MAIG - CH38 - END - Added logic to check if the Card Type is Amex or Discover
            }
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.card.number))
            {
                _CCNumber = res.enrollmentRecord.paymentItem.card.number;

            }
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.card.printedName))
            {
                _CCName = res.enrollmentRecord.paymentItem.card.printedName;
            }
            if (res.enrollmentRecord.paymentItem.card.expirationDate != null)
            {
                expDate = Convert.ToDateTime(res.enrollmentRecord.paymentItem.card.expirationDate.ToString());
            }
            ExpDteMon = expDate.Month.ToString();
            if (ExpDteMon.Length == 2)
            {
                _CCExpDate = Convert.ToDateTime(expDate.Month + "/" + expDate.Year).ToString("MM/yyyy");
            }
            {
                _CCExpDate = Convert.ToDateTime(expDate.Month + "/" + expDate.Year).ToString("MM/yyyy");
            }
            //CHG0072116 - PC Edit Card Details CH4:START - Assign values to the controls edit card details usercontrol.
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.card.zipCode))
            {
                _CCZipCode = res.enrollmentRecord.paymentItem.card.zipCode;
            }
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.paymentAccountToken.ToString()))
            {
                _PaymentTokenNumber = res.enrollmentRecord.paymentItem.paymentAccountToken.ToString();
            }

            policySearchDetails.Visible = true;
            UnEnrollCC.Visible = true;
            UnEnrollCC.assignValues();
            EditCC.assignValues();
            EditCC.DisplayTerms();
            EditCC.Visible = false;
            UnEnrollECheck.Visible = false;
            PaymentMethod.Visible = false;
            //CHG0072116 - PC Edit Card Details CH4:END - Assign values to the controls edit card details usercontrol.
        }
        #endregion
        #region View Enroll CC details
        /// <summary>
        /// CHG0112662 - Record Payment Enrollment SOA Service changes - Added this method for Viewing CC enrollment
        /// </summary>
        /// <param name="res"></param>
        private void UnEnrollPaymentCCView(RetrievePaymentEnrollment.retrievePaymentEnrollmentDetailResponse res)
        {
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.card.type))
            {
                _CCType = res.enrollmentRecord.paymentItem.card.type.ToUpper();
                if (_CCType.Equals(CSAAWeb.Constants.PC_CRD_TYPE_MASTER))
                {
                    _CCType = CSAAWeb.Constants.PC_CRD_TYPE_MASTER_EXPANSION;
                }
                //MAIG - CH38 - BEGIN - Added logic to check if the Card Type is Amex or Discover
                else if (_CCType.Equals(CSAAWeb.Constants.PC_CRD_TYPE_AMEX))
                {
                    _CCType = CSAAWeb.Constants.PC_CRD_TYPE_AMEX_EXPANSION;
                }
                else if (_CCType.Equals(CSAAWeb.Constants.PC_CRD_TYPE_DISC))
                {
                    _CCType = CSAAWeb.Constants.PC_CRD_TYPE_DISC_EXPANSION;
                }
                //MAIG - CH38 - END - Added logic to check if the Card Type is Amex or Discover
            }
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.card.number))
            {
                _CCNumber = res.enrollmentRecord.paymentItem.card.number;

            }
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.card.printedName))
            {
                _CCName = res.enrollmentRecord.paymentItem.card.printedName;
            }
            if (res.enrollmentRecord.paymentItem.card.expirationDate != null)
            {
                expDate = Convert.ToDateTime(res.enrollmentRecord.paymentItem.card.expirationDate.ToString());
            }
            ExpDteMon = expDate.Month.ToString();
            if (ExpDteMon.Length == 2)
            {
                _CCExpDate = Convert.ToDateTime(expDate.Month + "/" + expDate.Year).ToString("MM/yyyy");
            }
            {
                _CCExpDate = Convert.ToDateTime(expDate.Month + "/" + expDate.Year).ToString("MM/yyyy");
            }
            //CHG0072116 - PC Edit Card Details CH4:START - Assign values to the controls edit card details usercontrol.
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.card.zipCode))
            {
                _CCZipCode = res.enrollmentRecord.paymentItem.card.zipCode;
            }
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.paymentAccountToken.ToString()))
            {
                _PaymentTokenNumber = res.enrollmentRecord.paymentItem.paymentAccountToken.ToString();
            }

            policySearchDetails.Visible = true;
            UnEnrollCC.Visible = true;
            UnEnrollCC.assignValues();
            EditCC.assignValues();
            EditCC.DisplayTerms();
            EditCC.Visible = false;
            UnEnrollECheck.Visible = false;
            PaymentMethod.Visible = false;
            //CHG0072116 - PC Edit Card Details CH4:END - Assign values to the controls edit card details usercontrol.
        }
        #endregion
        #region View Enroll Echeck details
        /// <summary>
        /// CHG0112662 - Record Payment Enrollment SOA Service changes - Added this method for Viewing EC enrollment
        /// </summary>
        /// <param name="res"></param>
        private void UnEnrollPaymentECView(RetrievePaymentEnrollment.retrievePaymentEnrollmentDetailResponse res)
        {
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.account.ABANumber))
            {
                _ECRoutingNo = res.enrollmentRecord.paymentItem.account.ABANumber;
            }
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.account.accountNumber))
            {
                _ECAccountNo = res.enrollmentRecord.paymentItem.account.accountNumber;
            }
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.account.type))
            {
                _ECaacType = res.enrollmentRecord.paymentItem.account.type;

                if (_ECaacType.Equals(Config.Setting("EFT_SAVING")))
                {
                    _ECaacType = CSAAWeb.Constants.PC_EC_SAVG;
                }
                else
                {
                    _ECaacType = CSAAWeb.Constants.PC_EC_CHKG;
                }
            }
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.account.bankName))
            {
                _ECBankName = res.enrollmentRecord.paymentItem.account.bankName;
            }
            policySearchDetails.Visible = true;
            List<string> response = new List<string>();
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.account.accountHolderName))
            {
                _ECAccountHolderName = res.enrollmentRecord.paymentItem.account.accountHolderName;
            }
            UnEnrollCC.Visible = false;
            UnEnrollECheck.Visible = true;
            UnEnrollECheck.assignValues();
            PaymentMethod.Visible = false;

        }
        #endregion

        #region View Enroll Echeck details
        private void UnEnrollECView(RetrieveEnrollment.retrieveAutoPayEnrollmentDetailResponse res)
        {
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.account.ABANumber))
            {
                _ECRoutingNo = res.enrollmentRecord.paymentItem.account.ABANumber;
            }
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.account.accountNumber))
            {
                _ECAccountNo = res.enrollmentRecord.paymentItem.account.accountNumber;
            }
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.account.type))
            {
                _ECaacType = res.enrollmentRecord.paymentItem.account.type;

                if (_ECaacType.Equals(Config.Setting("EFT_SAVING")))
                {
                    _ECaacType = CSAAWeb.Constants.PC_EC_SAVG;
                }
                else
                {
                    _ECaacType = CSAAWeb.Constants.PC_EC_CHKG;
                }
            }
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.account.bankName))
            {
                _ECBankName = res.enrollmentRecord.paymentItem.account.bankName;
            }
            policySearchDetails.Visible = true;
            List<string> response = new List<string>();
            if (!string.IsNullOrEmpty(res.enrollmentRecord.paymentItem.account.accountHolderName))
            {
                _ECAccountHolderName = res.enrollmentRecord.paymentItem.account.accountHolderName;
            }
            UnEnrollCC.Visible = false;
            UnEnrollECheck.Visible = true;
            UnEnrollECheck.assignValues();
            PaymentMethod.Visible = false;

        }
        #endregion
        #region Required field validator
        protected void ReqValCheck1(Object source, ValidatorEventArgs args)
        {
            if (_ProductType.SelectedItem.Text.Equals(CSAAWeb.Constants.PC_SEL_PROD))
            {
                args.IsValid = false;
                vldProductType.MarkInvalid();
                vldProductType.ErrorMessage = CSAAWeb.Constants.PC_PROD_REQ;
                PolicyReqValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }
            if (_PolicyNumber.Text.Trim() == string.Empty)
            {
                args.IsValid = false;
                vldPolicyNumber.MarkInvalid();
                vldPolicyNumber.ErrorMessage = CSAAWeb.Constants.PC_POL_REQ;
                PolicyReqValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }
            //MAIG - CH29 - BEGIN - Invoked method to validate the FirstName,LastName and Mailing Zip fields
            ValidateAdditionalDetails();
            //MAIG - CH29 - END - Invoked method to validate the FirstName,LastName and Mailing Zip fields
        }
        protected void ValidPolciyNumberLength(Object source, ValidatorEventArgs args)
        {
            if (_PolicyNumber.Text.Trim() != string.Empty)
            {
                args.IsValid = (_PolicyNumber.Text.Trim().Length >= 4);
                if (!args.IsValid)
                {
                    vldPolicyNumberLength.MarkInvalid();
                    vldPolicyNumberLength.ErrorMessage = Constants.PC_INVALID_POLICY;
                    PolicyReqValidator.ErrorMessage = string.Empty;
                    _isValid = false;
                }

            }

        }

        //Method to check the Legacy Policy Product
        private string Check_Legacy_Policy_Product_Validation()
        {
            string PolicyNumber = (Convert.ToString(_PolicyNumber.Text)).Trim().ToUpper();
            string strAlphabet = PolicyNumber.Substring(0, 2);
            string stralphabetp = PolicyNumber.Substring(0, 3);
            Regex objPattern = new Regex("[A-Z]");
            bool IsAlphabet = objPattern.IsMatch(strAlphabet);
            Char[] cr = strAlphabet.ToCharArray();
            string firstchar = Convert.ToString(cr[0]);
            string secondchar = Convert.ToString(cr[1]);

            if (CSAAWeb.Validate.IsAllNumeric(_PolicyNumber.Text.Trim()))
            {
                if (_ProductType.SelectedItem.Value != Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Home"]))
                {
                    Invalid_Message = Constants.PCI_INVALID_POLICY_PRODUCT1;
                    return Invalid_Message;
                }
            }
            // CHG0083477 - Expansion of Homeowner Policy - Added additional characters (G, I, O, Q, S, Z) to the conditions.
            if ((cr[0] == 'H' || cr[0] == 'V' || cr[0] == 'W' || cr[0] == 'X' || cr[0] == 'G' || cr[0] == 'I' || cr[0] == 'O' || cr[0] == 'Q' || cr[0] == 'S' || cr[0] == 'Z') && (objPattern.IsMatch(secondchar.ToUpper())))
            {
                if (_ProductType.SelectedItem.Value != Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Home"]))
                {
                    Invalid_Message = Constants.PCI_INVALID_POLICY_PRODUCT1;
                    return Invalid_Message;
                }

            }

            // CHG0083477 - Expansion of Homeowner Policy - Added additional characters (G, I, O, Q, S, Z) to the conditions.
            if (cr[0] != 'H' && cr[0] != 'V' && cr[0] != 'W' && cr[0] != 'X' && cr[0] != 'G' && cr[0] != 'I' && cr[0] != 'O' && cr[0] != 'Q' && cr[0] != 'S' && cr[0] != 'Z')
            {
                if ((objPattern.IsMatch(firstchar.ToUpper())) && (objPattern.IsMatch(secondchar.ToUpper())))
                {
                    if (_ProductType.SelectedItem.Value != Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Auto"]))
                    {
                        Invalid_Message = Constants.PCI_INVALID_POLICY_PRODUCT1;
                        return Invalid_Message;
                    }
                }

            }

            if ((objPattern.IsMatch(firstchar.ToUpper()) && CSAAWeb.Validate.IsAllNumeric(secondchar)) || (objPattern.IsMatch(secondchar.ToUpper()) && CSAAWeb.Validate.IsAllNumeric(firstchar)))
            {
                if (_ProductType.SelectedItem.Value != Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Auto"]))
                {
                    Invalid_Message = Constants.PCI_INVALID_POLICY_PRODUCT1;
                    return Invalid_Message;
                }

            }

            return string.Empty;
        }

        //Policy sequence, Policy and product type selected for the PUP policy validations
        private string Check_PUP_Policy()
        {

            //MAIG - CH32 - BEGIN - Modified the logic to remove PUP prefix and support 7 digits of the policy
            
            if (!CSAAWeb.Validate.IsAllNumeric(_PolicyNumber.Text.Trim()))
            {
                Invalid_Message = Constants.PUP_NUMERIC_VALID;
            }
            else if (_PolicyNumber.Text.Trim().Length != 7)
            {
                Invalid_Message = Constants.POLICY_LENGTH_HOME_MESSAGE;
            }
            //MAIG - CH32 - END - Modified the logic to remove PUP prefix and support 7 digits of the policy
            return Invalid_Message;
        }

        //MAIG - CH33 - BEGIN - Commented the logic since it is not used.

        //Policy Validation for WU Policy All Numeric, Length between 4 and 9
        private string Check_WU_Policy()
        {
            if (!CSAAWeb.Validate.IsAllNumeric(_PolicyNumber.Text.Trim()))
            {
                Invalid_Message = Constants.PCI_POLICY_NUMERIC;

            }
            else if (!((_PolicyNumber.Text.Trim().Length >= 4) && (_PolicyNumber.Text.Trim().Length <= 9)))
            {
                Invalid_Message = Constants.PCI_SIS_POLICY_LENGTH;
            }

            return Invalid_Message;
        }
        //method for SIS policy length validation
        private string WesternUnitedPolicyLength()
        {
            string Invalid_Message = string.Empty;
            if (_PolicyNumber.Text.Trim().Length < 4 || _PolicyNumber.Text.Trim().Length > 9)
            {
                Invalid_Message = Constants.PCI_SIS_POLICY_LENGTH;
            }
            else if (!CSAAWeb.Validate.IsAllNumeric(_PolicyNumber.Text.Trim()))
            {

                Invalid_Message = Constants.PCI_POLICY_NUMERIC;
            }
            return Invalid_Message;
        }

        private bool validPolicyProduct(string policy, string productType)
        {
            bool validpolicyproduct = true;

            bool ValidIIBProd = false;
            bool ValidSIS = false;
            int iPolLength = policy.Trim().Length;
            //MAIG - CH35 - BEGIN - Modified/Commented the logic to Support Common Product Types
            if (productType.Equals(MSC.SiteTemplate.ProductCodes.PA.ToString()) || productType.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()))
            {
                if (!(CSAAWeb.Validate.IsAlphaNumeric(policy.Trim())) || (iPolLength < 4 || iPolLength > 13))
                {
                    Invalid_Message = Constants.AUTO_HOME_MESSAGE;
                    validpolicyproduct = false;
                }
            }
            else if (productType.Equals(MSC.SiteTemplate.ProductCodes.PU.ToString()))
            {
                if (!(iPolLength == 13 && CSAAWeb.Validate.IsAlphaNumeric(policy.Trim())))
                {
                    Invalid_Message = Check_PUP_Policy();
                }
                if (!string.IsNullOrEmpty(Invalid_Message))
                {
                    validpolicyproduct = false;
                }
            }
            else if (productType.Equals(MSC.SiteTemplate.ProductCodes.DF.ToString()) || productType.Equals(MSC.SiteTemplate.ProductCodes.MC.ToString()) || productType.Equals(MSC.SiteTemplate.ProductCodes.WC.ToString()))
            {
                if (!(productType.Equals(MSC.SiteTemplate.ProductCodes.DF.ToString()) && iPolLength == 13 && CSAAWeb.Validate.IsAlphaNumeric(policy.Trim())))
                {
                    Invalid_Message = WesternUnitedPolicyLength();
                }
                if (!string.IsNullOrEmpty(Invalid_Message))
                {
                    validpolicyproduct = false;
                }
            }
            else
            {
                validpolicyproduct = false;
            }

            //MAIG - CH35 - END - Modified/Commented the logic to Support Common Product Types
            return validpolicyproduct;
        }
        #endregion
        #endregion
        protected void _ProductType_SelectedIndexChanged(object sender, EventArgs e)
        {

            _PolicyNumber.Text = string.Empty;
            if (_ProductType.SelectedItem.Text != "Select Product Type")
            {
                Control Policy_Control = this.FindControl("_PolicyNumber");
                if (Policy_Control != null)
                {
                    TextBox Policy_Textbox = (TextBox)Policy_Control.FindControl("_PolicyNumber");
                    if (Policy_Textbox != null)
                    {
                        policySearchDetails.Visible = false;
                        Policy_Textbox.Enabled = true;
                        Policy_Textbox.Text = string.Empty;
                        string ProductType = _ProductType.SelectedItem.Value;
                        //MAIG - CH36 - BEGIN - Modified/Commented the Code to change the Tooltip.
                        if (ProductType.Equals(MSC.SiteTemplate.ProductCodes.PA.ToString()) || ProductType.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()))
                        {
                            Policy_Textbox.ToolTip = Constants.AUTO_HOME_TOOLTIP;
                            Policy_Textbox.Focus();
                        }
                        else
                        {
                            Policy_Textbox.ToolTip = Constants.OTHER_TOOLTIP;
                            Policy_Textbox.Focus();
                        }

                        //MAIG - CH36 - END - Modified/Commented the Code to change the Tooltip.
                    }
                }
            }
            else
            {
                policySearchDetails.Visible = false;
                _PolicyNumber.Enabled = false;
            }
        }

        //MAIG - CH37 - BEGIN - Added method to validate the FirstName, LastName and Mailing Zip details
        protected bool ValidateAdditionalDetails()
        {
            bool result = true;
            if (txtFirstName.Text == "")
            {
                vldFirstName.MarkInvalid();
                vldFirstName.ErrorMessage = "First Name is Required";
                result = false;
            }

            if (txtMailingZip.Text == "")
            {
                vldMailingZip.MarkInvalid();
                vldMailingZip.ErrorMessage = "Mailing Zip Code is Required";
                result = false;
            }

            Validator lastName = (Validator)txtLastName.FindControl("LabelValidator");
            TextBox txLastName = (TextBox)txtLastName.FindControl("_LastName");
            if (txLastName.Text == "")
            {
                lastName.MarkInvalid();
                lastName.ErrorMessage = "Last Name is Required";
                result = false;
            }

            return result;
        }

        //MAIG - CH2 - BEGIN - Including validations for First name
        ///<summary>Validator for the First name </summary>
        protected void CheckFirstName(Object Source, ValidatorEventArgs e)
        {
            e.IsValid = !regCheckName.IsMatch(txtFirstName.Text);
            if (!e.IsValid)
            {
                vldFirstName.MarkInvalid();
                Validator vldFirst = (Validator)Source;
                vldFirst.ErrorMessage = "Invalid First Name";
            }
        }
        protected void ReqValZipCheck(Object source, ValidatorEventArgs e)
        {
            if (string.IsNullOrEmpty(txtMailingZip.Text))
            {
                e.IsValid = true;
            }
            else
            {
                e.IsValid = (CSAAWeb.Validate.IsValidZip(txtMailingZip.Text));
            }
            if (!e.IsValid)
            {
                Validator vldZip = (Validator)source;
                //MAIG - CH6 - BEGIN - Modified the verbiage of the error message
                vldZip.ErrorMessage = "Invalid Mailing Zip";
                //MAIG - CH6 - END - Modified the verbiage of the error message
                vldMailingZip.MarkInvalid();
            }

        }
        //MAIG - CH37 - END - Added method to validate the FirstName, LastName and Mailing Zip details
    }
}
