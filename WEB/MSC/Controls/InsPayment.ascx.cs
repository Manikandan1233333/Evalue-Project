
//67811A0  - PCI Remediation for Payment systems : MOdified the Ins_Payment control to hold the Policy number,Product type, revenue type drop down control and its validations.
//PAS AZ Product COnfiguration CH1: Added the code to default the revenue type to Installment type and disable the revenue type drop down box by cognizant on 03/06/2011
//PAS AZ Product COnfiguration CH2: Modified the below code to handle the new policy prefix for PAS AZ policy by cognizant on 03/12/2012
//PAS AZ product configuration defect # 229 -- Added the code to disable the revenue type drop down for AZ signature series product type by cognizant on 03/20/2012
//Company code fix - Added company ID along with the policy state which is returned from the SIS look up
//AZ PAS conversion and PC integration -CH1  -Modified the below code to lookup the policy details from the billing summary lookup
//AZ PAS conversion and PC integration -CH2  - Added the below code to check active status of Highlimit policy else error message will be displayed
//AZ PAS conversion and PC integration -CH3  - Added the below code to valdate the policy for AZ PAS conversion requirement
//AZ PAS conversion and PC integration - CH4 -Added the highlimit product type in the condition to disable the revenue type for HL product type
//AZ PAS conversion and PC integration CH5 -  Added the code to default the revenue type to Installment type and disable the revenue type drop down box by cognizant on 03/06/2011
//AZ PAS conversion and PC integration CH6  - Modified the below code to validate the policy for HL product type
//AZ PAS conversion and PC integration CH7 - Added the company ID to the tmpdue details for non PCP trasnctions
//AZ PAS Conversion and PC integration CH8 - Added the below code to assign the first name and last name from the full name
//AZ PAS Conversion and PC integration -CH9 - Modified the message as part of defect 365.
//AZ PAS Conversion and PC integration - CH10 - Added the below method to identify the data source of the selected product type
//AZ PAS Conversion and PC integration: CH11 Added the below code to assign the product code which is obtained from service for Western united products by cognizant on 10/05/2012.
//CHG0072116 -PAS Conversion Fix - Modified the below code to allow payment for a non- Exigen converted policy which has outstanding balance
//CHG0078293 - PT Enhancement - Added the condition to check for the first two digits of policy number along with the sequence validation for HUON policy
//CHG0083477 - Expansion of Homeowner Policy - Added additional characters (G, I, O, Q, S, Z) to the conditions.
//MAIG - CH1 - Added the below local variables as part of MAIG
//MAIG - CH2 - Added the below controls as part of MAIG
//MAIG - CH3 - Added the Code to display only the Auto and Home products for Down payment
//MAIG - CH4 - Added the logic to add the Decrypt the encoded URl Query String
//MAIG - CH5 - Added the Code to select the TextBox control to retrieve the policy number
//MAIG - CH6 - Added the logic to include Hidden fields and maintain the values after postback
//MAIG - CH7 - Removed the Code to select the TextBox control since it is used in method start
//MAIG - CH8 - Removed the Code to select the TextBox control since it is used in method start
//MAIG - CH9 - Removed the code since the method is not used anywhere
//MAIG - CH10 - Commented the below method as part of Common Product Types change
//MAIG - CH11 - Commented the below method as part of Common Product Types change
//MAIG - CH12 - Commented the code to remove the PUP prefix
//MAIG - CH13 - Commented the below method as part of Common Product Types change
//MAIG - CH14 - Modifed the below code to retain the value on back and forth navigation
//MAIG - CH15 - Updated the logic to send the PolicyState and Prefix. Removed the CompanyCode sending logic. 
//MAIG - CH16 - Modifed the below code to retain the value on back and forth navigation
//MAIG - CH17 - Commented the below method as part of Common Product Types change
//MAIG - CH18 - Removed the code since the method is not used anywhere
//MAIG - CH19 - Commented the Code to change the Tooltip and Revenue Type needs to be disabled.
//MAIG - CH20 - Logic is removed since the Revenue Type is handled in Menu
//MAIG - CH21 - Modified the Old logic for getting the DataSource to RPBS Service
//MAIG - CH22 - Added logic to remove Old Policy Number validations and Added New Policy Number Validation
//MAIG - CH23 - Updated the Common Home Product Type instead of AAASS Home and commented Old logic to get the Product Code from DB
//MAIG - CH24 - Added code to set the data source for Down payment sceanrio and delete a un-selected row if there are multiple response from RPBS Service
//MAIG - CH25 - Logic added to select only one row from the RPBS Service if it has multiple response
//MAIG - CH26 - Commented the Code since the product Type holds the PC Product Type
//MAIG - CH27 - Added the ToString method in the condition to avoid any unhandled exception
//MAIG - CH28 - Added condition to null check to avoid any unhandled exception
//MAIG - CH29 - Modified the Code to change the flow for down
//MAIG - CH30 - Added logic to support the Name Search functionality for Installment Payment
//MAIG - CH31 - Added condition to skip validation if the policy length is 13, for an PAS Home Policy
//MAIG - CH32 - Added condition to skip validation if the policy length is 13, for an PAS Home Policy
//MAIG - CH33 - Commented the code that accomodates the PAS Home DF, PU policies in Home Product.
//MAIG - CH34 - Added code to Set the Product Code retrieved from RPBS Service since PAS Home can DF,PU and HO
//MAIG - CH35 - Creation of new Regular Expression to check the First & Last Names
//MAIG - CH36 - Added logic to pass the Name Search grid data and Duplicate Policy data if the same policy is selected again
//MAIGEnhancement CHG0107527 - CH1 - Modified the below code to allow payments even if the RPBS response IsRestrictedToPay field is true, also this is made configurable
//MAIGEnhancement CHG0107527 - CH2 - Added the below line to change the Page Index to the user selected one if the screen is navigated back & forth for a Duplicate Policy.
//MAIGEnhancement CHG0107527 - CH3 - Added the below line to store the source system of the selected policy in view state. This resolves skipping the RPBS hit for the same policy by considering the Source system, which occurs only if the user navigated back & forth in Name Search
//MAIGEnhancement CHG0107527 - CH4 - Added the below line to store the source system of the selected policy in view state. This resolves skipping the RPBS hit for the same policy by considering the Source system, which occurs only if the user navigated back & forth in Duplicate Policy scenario
//MAIGEnhancement CHG0107527 - CH5 - Added the below condition to append the source system to HidddenPolicyValidate field which helps to skip the RPBS Service hit only if user navigated back & forth in Name Search/Duplicate Policy scenario
//MAIGEnhancement CHG0107527 - CH6 - Modified the below condition to append the source system which helps to skip the RPBS Service hit if user navigated back & forth in Name Search/Duplicate Policy scenario
//MAIGEnhancement CHG0107527 - CH7 - Modified the below condition with >= to resolve Error that happens only for role which has only Installment Payment.
//MAIGEnhancement CHG0107527 - CH8 - Modified the below condition to include the two dots which is the default Error message of the validator that gets displayed for an converted policy which is cancelled. 
//MAIGEnhancement CHG0107527 - CH9 - Modified the below condition to include the two dots which is the default Error message of the validator that gets displayed for an converted policy which is cancelled. 
//CHG0109406 - CH1 - Added the below condition to set Context even if it holds the old value of Rpbs Grid details in case of back and forth scenario. 
//CHG0110069 - CH1 - Modified the below logic to remove the Auto product Type from the drop down
//CHG0110069 - CH2 - Modified the below logic to remove the Auto product Type from the condition since it should not be allowed
//CHG0110069 - CH3 - Added the below logic to only allow KIC policies if they are Numeric for Down payments
//CHG0110069 - CH4 - Modified/Commented the below logic to remove the Auto Policy condition since it is removed. 
//CHG0110069 - CH5 - Removed the below logic to remove the Auto Policy condition since it is removed.
//CHG0110069 - CH6 - Modified the below logic to remove the Auto product Type from the condition since it should not be allowed
//CHG0113938 - Display Policy Status information in Billing summary & Hide Policy Status information for Down payments.
//CHG0116140 - Payment Restricition - Set PaymentRestriction variable for Payment Restriction check & Added Payment Restriction to RPBSBillingDetails in the context 
namespace MSC.Controls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using CSAAWeb;
    using CSAAWeb.WebControls;
    using CSAAWeb.Serializers;
    using InsuranceClasses;
    using OrderClasses.Service;
    using System.Text.RegularExpressions;
    using System.Web.SessionState;
    //SR#8434145.Ch2 : START - Include the namespaces for accessing SQL object
    using System.Data.SqlClient;
    //SR#8434145.Ch2 : END
    //CSR#3937.Ch1 : START - Include the namespaces for accessing StringDictionary object
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Configuration;
    using System.Web.UI;

    public partial class InsPayment : ValidatingUserControl, MSC.IItemControl
    {

        public InsPayment()
            : base()
        {
            this.Init += new System.EventHandler(this.InitializeLBs);
        }

        #region WebControls


        protected MSC.Controls.PolicyNumber _Policy;

        protected DropDownList _RevenueType;

        protected CSAAWeb.WebControls.Validator vldPolicy;

        protected CSAAWeb.WebControls.Validator vldProduct;

        protected CSAAWeb.WebControls.Validator vldRevenue;



        #endregion

        #region Variables

        bool ValidSeq1;
        public int MaxLength = 25;
        public bool ValidIIBProd;
        public bool ValidSIS;
        public string FirstName;
        public string LastName;
        public string Product_Prefix;
        public string ProductTypeNew;
        public int IsOverride = 0;
        public string Control_Values;
        public int iPolLength;
        public string Invalid_Message = string.Empty;
        public int Isactive;
        protected System.Web.UI.WebControls.TextBox HiddenField;
        protected CSAAWeb.WebControls.Validator vldProductType;
        protected System.Web.UI.WebControls.DropDownList _ProductType;
        protected Label LabelN;
        //MAIG - CH1 - BEGIN - Added the below local variables as part of MAIG
        bool IsDown;
        DataTable dsEnteredPolicyDetails = new DataTable();
        //MAIG - CH1 - END - Added the below local variables as part of MAIG
        #endregion

        #region Public Properties
        ///<summary>Quantity of this item.</summary>
        public int Quantity = 1;
        ///<summary>Price of this item.</summary>

        /// <summary>
        /// Changed by Cognizant on 15/8/2004 for Adding FirstName in the Description
        /// </summary> 
        ///<summary>Description of this item.</summary>
        public string Description
        {
            get { return GetListText(_ProductType) + " " + GetListText(_RevenueType) + " Policy #" + Policy + " "; }
            set { }
        }
        private static string GetListText(ListControl C)
        {
            return (C.SelectedItem == null) ? "" : C.SelectedItem.Text;
        }


        public string Productcode
        {
            get { return _ProductType.SelectedItem.Value; }
            set
            {
                _ProductType.SelectedItem.Value = value;

            }
        }



        ///<summary>The policy number.</summary>
        public string Policy
        {

            get { return _Policy.Text.Trim(); }
            set
            {
                _Policy.Text = value;

            }
        }

        ///<summary>Identity for product type.</summary>
        public string ProductType
        {
            get { return ListC.GetListValue(_ProductType); }
            set
            {
                ListC.SetListIndex(_ProductType, value.ToString());

            }
        }
        ///<summary>Identity for Revenue type.</summary>
        public string RevenueType
        {
            get { return ListC.GetListValue(_RevenueType); }
            set
            {
                ListC.SetListIndex(_RevenueType, value);
            }
        }

        ///<summary>The amount of the payment.</summary>


        /// <summary>
        /// Returns the row is INS_Product_Type that corresponds to ProductType.
        /// </summary>
        private static DataTable ProductTypes = null;
        //Serialization stuff
        private InsuranceLineItem Info;
        ///<summary>Xml data for this associate</summary>
        public override string ToString() { return ((InsuranceLineItem)Data).ToString(); }
        ///<summary>The data (InsuranceLineItem) for this associate.</summary>
        public SimpleSerializer Data
        {
            get { if (Info == null) Info = new InsuranceLineItem(); Info.CopyFrom(this); return Info; }
            set { Info = (InsuranceLineItem)value; Info.CopyTo(this); }

        }

        #region RegexExpressions

        ///  Creation of new Regular Expression to check the First & Last Names
        /// </summary>
        //MAIG - CH35 - BEGIN - Creation of new Regular Expression to check the First & Last Names
        private static Regex regCheckName = new Regex("[^a-z '*.,-]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //MAIG - CH35 - END - Creation of new Regular Expression to check the First & Last Names

        #endregion


        ///<summary>Sets values from an Xml string.</summary>
        public void LoadXML(string Xml) { Data = new InsuranceLineItem(Xml); }
        ///<summary/>
        private int _Number = 0;
        ///<summary/>

        public int Number
        {
            get { return _Number; }
            set
            {
                ID = "Item_" + (value + 1).ToString();
                LabelN.Text = (value + 1).ToString();
                _Number = value;
            }
        }


        #endregion

        #region OnLoad events


        private void Initialize()
        {
            //MAIG - CH2 - BEGIN - Added the below controls as part of MAIG
            Control Policy_Control = this.FindControl("_Policy");
            TextBox Policy_Textbox = (TextBox)Policy_Control.FindControl("_Policy");
            //MAIG - CH2 - END - Added the below controls as part of MAIG
            ProductTypes = ((SiteTemplate)Page).OrderService.LookupDataSet("Insurance", "ProductTypes").Tables["INS_Product_Type"];
            if (ProductTypes.Rows.Count > 1) AddSelectItem(ProductTypes, "Select Product Type");

            if (Cache["INS_Product_Type"] == null)
            {
                Cache["INS_Product_Type"] = ProductTypes;
            }
            DataTable Dt = ((SiteTemplate)Page).OrderService.LookupDataSet("Insurance", "RevenueTypesByRole", new object[] { Page.User.Identity.Name, Config.Setting("AppName") }).Tables["INS_Revenue_Type_By_Role"];
            //MAIGEnhancement CHG0107527 - CH7 - BEGIN - Modified the below condition with >= to resolve Error that happens only for role which has only Installment Payment.
            if (Dt.Rows.Count >= 1) AddSelectItem(Dt, "Select Revenue Type");
            //MAIGEnhancement CHG0107527 - CH7 - END - Modified the below condition with >= to resolve Error that happens only for role which has only Installment Payment.
            _RevenueType.DataSource = Dt;
            _RevenueType.DataBind();


            if (Request.Cookies["IsDown"] != null && Request.Cookies["IsDown"].Value.Equals("false"))
            {
                _RevenueType.SelectedValue = "1291";
            }
            else if (Request.Cookies["IsDown"] != null && Request.Cookies["IsDown"].Value.Equals("true"))
            {
                DataTable downPayment = new DataTable();
                //CHG0110069 - CH1 - BEGIN - Modified the below logic to remove the Auto product Type from the drop down
                downPayment = (ProductTypes.Select("ID in ('HO','PU')")).CopyToDataTable();
                //CHG0110069 - CH1 - END - Modified the below logic to remove the Auto product Type from the drop down
                downPayment.DefaultView.Sort = "Description ASC";
                ProductTypes = downPayment.DefaultView.ToTable();
                AddSelectItem(ProductTypes, "Select Product Type");
                _ProductType.DataSource = ProductTypes;
                _ProductType.DataBind();
                _RevenueType.SelectedValue = "297";
            }
        }


        ///<summary>Called from init event.</summary>
        private void InitializeLBs(object sender, System.EventArgs e)
        {
            if (Page.IsPostBack && _RevenueType.Items.Count > 0) return;
            //if (ProductTypes==null)
            //MAIG - CH3 - BEGIN - Added the Code to display only the Auto and Home products for Down payment
            //if (Request.QueryString["IsDown"] != null)
            //Page.ClientScript.RegisterStartupScript(this.GetType(), "UpdateRevenue", "UpdateRevenuType();", true);
            //if (!string.IsNullOrEmpty(MenuSelectedDown.Value))
            //{
            //    //IsDown=bool.Parse(Decrypt(Request.QueryString["IsDown"]));
            //    IsDown = bool.Parse(MenuSelectedDown.Value);
            //}
            Initialize();
            /*if (IsDown)
            {
                DataTable downPayment = new DataTable();

                downPayment = (ProductTypes.Select("ID in ('PA','HO','PU')")).CopyToDataTable();
                downPayment.DefaultView.Sort = "Description ASC";
                ProductTypes = downPayment.DefaultView.ToTable();
                AddSelectItem(ProductTypes, "Select Product Type");
            }*/
            //MAIG - CH3 - END - Added the Code to display only the Auto and Home products for Down payment
            _ProductType.DataSource = ProductTypes;

            _ProductType.DataBind();
        }

        //MAIG - CH4 - BEGIN - Added the logic to add the Decrypt the encoded URl Query String
        #region Cryption
        public static string Decrypt(string val)
        {
            var bytes = Convert.FromBase64String(val);
            var encBytes = System.Security.Cryptography.ProtectedData.Unprotect(bytes, new byte[0], System.Security.Cryptography.DataProtectionScope.LocalMachine);
            return System.Text.Encoding.UTF8.GetString(encBytes);
        }
        #endregion
        //MAIG - CH4 - END - Added the logic to add the Decrypt the encoded URl Query String

        new protected void Page_Load(object sender, EventArgs e)
        {
            //MAIG - CH5 - BEGIN - Added the Code to select the TextBox control to retrieve the policy number
            Control Policy_Control = this.FindControl("_Policy");
            TextBox Policy_Textbox = (TextBox)Policy_Control.FindControl("_Policy");
            //MAIG - CH5 - END - Added the Code to select the TextBox control to retrieve the policy number
            //MAIG - CH6 - BEGIN - Added the logic to include Hidden fields and maintain the values after postback
            if (Context.Items["DueDetails"] != null || !string.IsNullOrEmpty(HidddenDueDetails.Text))
            {
                if (string.IsNullOrEmpty(HidddenDueDetails.Text))
                {
                    HidddenDueDetails.Text = Context.Items["DueDetails"].ToString();
                }
                else
                {
                    if (Context.Items["DueDetails"] == null)
                    {
                        Context.Items.Add("DueDetails", HidddenDueDetails.Text);
                    }
                }
            }


            //Payment Restriction - BEGIN - CHG0116140 - Set PaymentRestriction variable for Payment Restriction check
            if (ViewState["PaymentRestriction"] != null || Context.Items["PaymentRestriction"] != null)
            {
                if (ViewState["PaymentRestriction"] == null)
                {
                    ViewState.Add("PaymentRestriction", Context.Items["PaymentRestriction"].ToString());
                }
                else
                {
                    if (Context.Items["PaymentRestriction"] == null)
                    { Context.Items.Add("PaymentRestriction", ViewState["PaymentRestriction"]); }
                }
            }
            //Payment Restriction - END - CHG0116140 - Set PaymentRestriction for Payment Restriction check
            if (Context.Items["AmountSelected"] != null || !string.IsNullOrEmpty(HiddenAmountSelected.Text))
            {
                if (string.IsNullOrEmpty(HiddenAmountSelected.Text))
                {
                    HiddenAmountSelected.Text = Context.Items["AmountSelected"].ToString();
                }
                else
                {
                    if (Context.Items["AmountSelected"] == null)
                    {
                        Context.Items.Add("AmountSelected", HiddenAmountSelected.Text);
                    }
                }
            }

            //if (Context.Items["AmountSelected"] != null)
            //{
            //    HiddenAmountSelected.Text = Context.Items["AmountSelected"].ToString();
            //}
            if (ViewState["ViewStoreFields"] != null || Context.Items["StoreFields"] != null)
            {
                if (ViewState["ViewStoreFields"] == null)
                {
                    ViewState.Add("ViewStoreFields", Context.Items["StoreFields"].ToString());
                }
                else
                {
                    if (Context.Items["StoreFields"] == null)
                    { Context.Items.Add("StoreFields", ViewState["ViewStoreFields"]); }
                }
            }
            if (ViewState["ViewSourceCompanyCode"] != null || Context.Items["SourceCompanyCode"] != null)
            {
                if (ViewState["ViewSourceCompanyCode"] == null)
                {
                    ViewState.Add("ViewSourceCompanyCode", Context.Items["SourceCompanyCode"].ToString());
                }
                else
                {
                    if (Context.Items["SourceCompanyCode"] == null)
                    { Context.Items.Add("SourceCompanyCode", ViewState["ViewSourceCompanyCode"]); }
                }
            }
            if (ViewState["ViewRpbsBillingDetails"] != null || Context.Items["RpbsBillingDetails"] != null)
            {
                if (ViewState["ViewRpbsBillingDetails"] == null)
                {
                    ViewState.Add("ViewRpbsBillingDetails", Context.Items["RpbsBillingDetails"].ToString());
                }
                else
                {
                    if (Context.Items["RpbsBillingDetails"] == null)
                    { Context.Items.Add("RpbsBillingDetails", ViewState["ViewRpbsBillingDetails"]); }
                }
            }

            if (ViewState["ViewPaymentPlan"] != null || Context.Items["PaymentPlan"] != null)
            {
                if (ViewState["ViewPaymentPlan"] == null)
                {
                    ViewState.Add("ViewPaymentPlan", Context.Items["PaymentPlan"].ToString());
                }
                else
                {
                    if (Context.Items["PaymentPlan"] == null)
                    { Context.Items.Add("PaymentPlan", ViewState["ViewPaymentPlan"]); }
                }
            }

            if (ViewState["Viewbillplan"] != null || Context.Items["billplan"] != null)
            {
                if (ViewState["Viewbillplan"] == null)
                {
                    ViewState.Add("Viewbillplan", Context.Items["billplan"].ToString());
                }
                else
                {
                    if (Context.Items["billplan"] == null)
                    { Context.Items.Add("billplan", ViewState["Viewbillplan"]); }
                }
            }

            if (ViewState["ViewautoPay"] != null || Context.Items["autoPay"] != null)
            {
                if (ViewState["ViewautoPay"] == null)
                {
                    ViewState.Add("ViewautoPay", Context.Items["autoPay"].ToString());
                }
                else
                {
                    if (Context.Items["autoPay"] == null)
                    { Context.Items.Add("autoPay", ViewState["ViewautoPay"]); }
                }
            }

            // CHG0113938 - BEGIN - Add Policy Status Description to Context/ViewState.
            if (ViewState[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION] != null || Context.Items[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION] != null)
            {
                if (ViewState[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION] == null)
                {
                    ViewState.Add(CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION, Context.Items[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION].ToString());
                }
                else
                {
                    if (Context.Items[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION] == null)
                    { Context.Items.Add(CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION, ViewState[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION]); }
                }
            }
            // CHG0113938 - END - Add Policy Status Description to Context/ViewState.

            // if (IsDown)
            //{
            //Down payment
            //_RevenueType.SelectedValue = CSAAWeb.Constants.PC_REVENUE_DOWN;
            //_RevenueType.Enabled = false;
            //Policy_Textbox.Focus();
            // }
            // else
            // {
            //Installment payment
            //_RevenueType.SelectedValue = CSAAWeb.Constants.PC_REVENUE_INST;
            if (!Page.IsPostBack && (_ProductType.SelectedValue.Equals("0") && _RevenueType.SelectedValue.Equals("0")))
            {
                if (Request.Cookies["IsDown"] != null && Request.Cookies["IsDown"].Value.Equals("false"))
                {
                    _RevenueType.SelectedValue = "1291";
                }
                else if (Request.Cookies["IsDown"] != null && Request.Cookies["IsDown"].Value.Equals("true"))
                {
                    //DataTable downPayment = new DataTable();

                    //downPayment = (ProductTypes.Select("ID in ('PA','HO','PU')")).CopyToDataTable();
                    //downPayment.DefaultView.Sort = "Description ASC";
                    //ProductTypes = downPayment.DefaultView.ToTable();
                    //AddSelectItem(ProductTypes, "Select Product Type");
                    //_ProductType.DataSource = ProductTypes;
                    //_ProductType.DataBind();
                    _RevenueType.SelectedValue = "297";
                }
            }
            /* if (!string.IsNullOrEmpty(hdnRevenuType.Value) )
             {
                 _RevenueType.SelectedValue = hdnRevenuType.Value;
             }
             else if (Request.Cookies["IsDown"]!=null && Request.Cookies["IsDown"].Value.Equals("false"))
             {
                 _RevenueType.SelectedValue = "1291";
             }*/
            _RevenueType.Enabled = false;
            Policy_Textbox.Focus();


            //if (pnlNameSearch != null && pnlNameSearch.Visible)
            //{
            if (Context.Items["NameSearchData"] != null && !string.IsNullOrEmpty(Context.Items["NameSearchData"].ToString()))
            {
                Panel pnlNameSearch = (Panel)_NameSearch.FindControl("pnlCustomerNameMain");
                pnlNameSearch.Visible = true;
                string[] SearchCriteria = Context.Items["NameSearchData"].ToString().Split('|');
                TextBox custFirstName = (TextBox)_NameSearch.FindControl("_FirstName");
                TextBox custLastName = (TextBox)_NameSearch.FindControl("_LastName");
                TextBox custMailingZip = (TextBox)_NameSearch.FindControl("_MailingZip");
                custFirstName.Text = SearchCriteria[0];
                custLastName.Text = SearchCriteria[1];
                custMailingZip.Text = SearchCriteria[2];
                _ProductType.Enabled = false;
                TextBox tmpPolicy = (TextBox)_Policy.FindControl("_Policy");
                tmpPolicy.Enabled = false;
                _NameSearch.PopulateGrid();
                GridView NameSearchResult = (GridView)_NameSearch.FindControl("NameSearchResults");
                if (SearchCriteria.Length > 4)
                {
                    NameSearchResult.PageIndex = Convert.ToInt32(SearchCriteria[4]);
                    //MAIGEnhancement CHG0107527 - CH2 - BEGIN - Added the below line to change the Page Index to the user selected one if the screen is navigated back & forth for a Duplicate Policy.
                    NameSearchResult.DataBind();
                    //MAIGEnhancement CHG0107527 - CH2 - END - Added the below line to change the Page Index to the user selected one if navigated back & forth for a Duplicate Policy.
                    RadioButton selectedPolicy = NameSearchResult.Rows[Convert.ToInt32(SearchCriteria[3])].FindControl("rbtnSelect") as RadioButton;
                    selectedPolicy.Checked = true;
                    Policy_Textbox.Text = NameSearchResult.Rows[Convert.ToInt32(SearchCriteria[3])].Cells[1].Text;
                    //MAIGEnhancement CHG0107527 - CH3 - BEGIN - Added the below line to store the source system of the selected policy in view state. This resolves skipping the RPBS hit for the same policy by considering the Source system, which occurs only if the user navigated back & forth in Name Search
                    ViewState["policySourceSystem"] = NameSearchResult.Rows[Convert.ToInt32(SearchCriteria[3])].Cells[2].Text;
                    //MAIGEnhancement CHG0107527 - CH3 - END - Added the below line to store the source system of the selected policy in view state. This resolves skipping the RPBS hit for the same policy by considering the Source system, which occurs only if the user navigated back & forth in Name Search
                    HiddenNameSearch.Text = string.Format("{0}|{1}|{2}|{3}",_ProductType.SelectedValue,Policy_Textbox.Text, SearchCriteria[3], SearchCriteria[4]);
                }
                LinkButton lnkCustomerName = (LinkButton)_NameSearch.FindControl("lnkSearchByCustomerName");
                lnkCustomerName.Enabled = false;
            }

            if (Context.Items["DuplicatePolicyData"] != null && !string.IsNullOrEmpty(Context.Items["DuplicatePolicyData"].ToString()))
            {
                PlaceHolder resultsGrid = (PlaceHolder)_NameSearch.FindControl("Results");
                _NameSearch.Visible = true;
                Panel pnlCustomerName = (Panel)_NameSearch.FindControl("pnlCustomerNameMain");
                LinkButton lnkCustomerName = (LinkButton)_NameSearch.FindControl("lnkSearchByCustomerName");
                HtmlTable CriteriaParameters = (HtmlTable)_NameSearch.FindControl("table1");
                lnkCustomerName.Visible = false;
                pnlCustomerName.Visible = true;
                CriteriaParameters.Visible = false;
                resultsGrid.Visible = true;
                string[] SearchCriteria = Context.Items["DuplicatePolicyData"].ToString().Split('|');
                InsuranceClasses.Service.Insurance I = new InsuranceClasses.Service.Insurance();
                dsEnteredPolicyDetails = new DataTable();
                dsEnteredPolicyDetails = I.CheckPolicy(SearchCriteria[0], SearchCriteria[1], Page.User.Identity.Name, SearchCriteria[2]);
                _NameSearch.PopulateRpbsData(dsEnteredPolicyDetails);
                GridView NameSearchResult = (GridView)_NameSearch.FindControl("NameSearchResults");
                if (SearchCriteria.Length > 3)
                {
                    RadioButton selectedPolicy = NameSearchResult.Rows[Convert.ToInt32(SearchCriteria[3])].FindControl("rbtnSelect") as RadioButton;
                    selectedPolicy.Checked = true;
                    Policy_Textbox.Text = NameSearchResult.Rows[Convert.ToInt32(SearchCriteria[3])].Cells[1].Text;
                    //MAIGEnhancement CHG0107527 - CH4 - BEGIN - Added the below line to store the source system of the selected policy in view state. This resolves skipping the RPBS hit for the same policy by considering the Source system, which occurs only if the user navigated back & forth in Duplicate Policy scenario
                    ViewState["policySourceSystem"] = NameSearchResult.Rows[Convert.ToInt32(SearchCriteria[3])].Cells[2].Text;
                    //MAIGEnhancement CHG0107527 - CH4 - END - Added the below line to store the source system of the selected policy in view state. This resolves skipping the RPBS hit for the same policy by considering the Source system, which occurs only if the user navigated back & forth in Duplicate Policy scenario
                   HiddenNameSearch.Text = string.Format("{0}|{1}|{2}|{3}", _ProductType.SelectedValue, Policy_Textbox.Text, SearchCriteria[3],string.Empty);
                }
                _ProductType.Enabled = false;
                TextBox tmpPolicy = (TextBox)_Policy.FindControl("_Policy");
                tmpPolicy.Enabled = false;
                            ViewState.Add("DuplicateData", string.Format("{0}|{1}|{2}|{3}", SearchCriteria[0], SearchCriteria[1], SearchCriteria[2],SearchCriteria[3]));
                ViewState.Add("DuplicateRpbsData", dsEnteredPolicyDetails);
            }
            //NameSearchData.Value = string.Format("{0}|{1}|{2}", _ProductType.SelectedIndex, _RevenueType.SelectedIndex, Policy.ToString());
            //NameSearchGridData.Value = NameSearch.SearchResult;
            //}


            //Code to assign the values from Name Search Grid
            if (!string.IsNullOrEmpty(HiddenNameSearch.Text))
            {
                string[] policydetails = HiddenNameSearch.Text.Split('|');
                //_ProductType.SelectedValue = policydetails[0];
                //var itm = _ProductType.Items.FindByValue(policydetails[0]);
                //int itemIndex = _ProductType.Items.IndexOf(itm);
                //if (itemIndex >= 0)
                //{
                //    _ProductType.SelectedIndex = itemIndex;
                //}

                _Policy.Text = policydetails[1];
                //GridView grdView=(GridView)_NameSearch.FindControl("NameSearchResults");
                //GridViewRow dr = grdView.Rows[Convert.ToInt32(policydetails[2].ToString())];
                //RadioButton rb = (RadioButton)dr.FindControl("rbtnSelect");
                //rb.Checked = true;
            }

            //}
            //MAIG - CH6 - END - Added the logic to include Hidden fields and maintain the values after postback
            if (!Page.IsPostBack)
            {
                if (string.IsNullOrEmpty(_Policy.Text.Trim()))
                {
                    //MAIG - CH7 - BEGIN - Removed the Code to select the TextBox control since it is used in method start
                    //_RevenueType.Enabled = false;
                    //Control Policy_Control = this.FindControl("_Policy");
                    //TextBox Policy_Textbox = (TextBox)Policy_Control.FindControl("_Policy");
                    //MAIG - CH7 - END - Removed the Code to select the TextBox control since it is used in method start
                    Policy_Textbox.Enabled = false;
                }
                else
                {
                    HidddenPolicyValidate.Text = _Policy.Text.Trim() + _RevenueType.SelectedItem.Value + _ProductType.SelectedItem.Value;
                    //MAIGEnhancement CHG0107527 - CH5 - BEGIN - Added the below condition to append the source system to HidddenPolicyValidate field which helps to skip the RPBS Service hit only if user navigated back & forth in Name Search/Duplicate Policy scenario
                    if (ViewState["policySourceSystem"] != null)
                    {
                        HidddenPolicyValidate.Text = HidddenPolicyValidate.Text + ViewState["policySourceSystem"];
                        HiddenSelectedDuplicatePolicy.Text = ViewState["policySourceSystem"].ToString();
                    }
                    //MAIGEnhancement CHG0107527 - CH5 - END - Added the below condition to append the source system to HidddenPolicyValidate field which helps to skip the RPBS Service hit only if user navigated back & forth in Name Search/Duplicate Policy scenario
                }
                //PAS AZ product configuration defect # 229 -- Added the code to disable the revenue type drop down for AZ signature series product type by cognizant on 03/20/2012
                //AZ PAS conversion and PC integration - CH4 -Added the highlimit product type in the condition to disable the revenue type for HL product type
                //PAS HO CH27 - START : Added the below code to disable the SS Home product type  6/26/2014
                //if ((IsWesternUnitedProduct()) || (ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.ArizonaAuto"])) || (ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.SSHome"])) || (ProductType == (ConfigurationSettings.AppSettings["ProductCode.HighLimit"])))
                //PAS HO CH27 - END : Added the below code to disable the SS Home product type  6/26/2014

                //PAS AZ product configuration defect # 229 -- Added the code to disable the revenue type drop down for AZ signature series product type by cognizant on 03/20/2012

            }
            else
            {
                if (_ProductType.SelectedItem.Text == "Select Product Type")
                {
                    //MAIG - CH8 - BEGIN - Removed the Code to select the TextBox control since it is used in method start
                    //Control Policy_Control = this.FindControl("_Policy");
                    //TextBox Policy_Textbox = (TextBox)Policy_Control.FindControl("_Policy");
                    //MAIG - CH8 - END - Removed the Code to select the TextBox control since it is used in method start
                    Policy_Textbox.Enabled = false;

                }
            }

        }

        #endregion

        #region Web Form Designer generated code
        ///<summary></summary>
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }


        private void InitializeComponent()
        {

            this.Load += new System.EventHandler(this.Page_Load);
        }

        #endregion

        #region Policy Validations
        //MAIG - CH9 - BEGIN - Removed the code since the method is not used anywhere
        ////Mapper to convert SIS Product to WU Product types available in Payment Tool
        //private string SISPolicyMapper(string strSISProductType)
        //{
        //    switch (strSISProductType)
        //    {
        //        case "PA":
        //            return "WUA";
        //        case "MC":
        //            return "WUM";
        //        case "IM":
        //            return "WUW";
        //        case "HO":
        //            return "WUH";
        //        case "DF":
        //            return "WUH";
        //        default:
        //            return "";
        //    }

        //}
        //MAIG - CH9 - END - Removed the code since the method is not used anywhere

        //MAIG - CH10 - BEGIN - Commented the below method as part of Common Product Types change
        //Method to check the Exigen Policy
        /* private string Check_Exigen_Policy()
         {

             string exPolicyNo = string.Empty;
             string stateProductCode = string.Empty;

             string restrictedStateProductCodeValues = string.Empty;
             string allowedExigenPolicyValues = string.Empty;
             string restrictedExigenPolicyValues = string.Empty;
             //PAS AZ Product COnfiguration CH2: Start Modified the below code to handle the new policy prefix for PAS AZ policy by cognizant on 03/12/2012
             string allowedCAExigenPolicyValues = string.Empty;
             string allowedAZExigenPolicyValues = string.Empty;
             //			67811A0  - PCI Remediation for Payment systems - Added Trim condition to the policy number field.
             exPolicyNo = _Policy.Text.Trim().Substring(4, 9);
             stateProductCode = _Policy.Text.Trim().Substring(0, 4);
             if (ConfigurationSettings.AppSettings["AllowedExigenPolicies"] != null && ConfigurationSettings.AppSettings["AllowedExigenPolicies"].ToString() != string.Empty)
             {
                 allowedExigenPolicyValues = ConfigurationSettings.AppSettings["AllowedExigenPolicies"].ToString();
             }

             if (ConfigurationSettings.AppSettings["RestrictedExigenPolicies"] != null && ConfigurationSettings.AppSettings["RestrictedExigenPolicies"].ToString() != string.Empty)
             {
                 restrictedExigenPolicyValues = ConfigurationSettings.AppSettings["RestrictedExigenPolicies"].ToString();
             }
             if (ConfigurationSettings.AppSettings["AllowedCAExigenPolicies"] != null && ConfigurationSettings.AppSettings["AllowedCAExigenPolicies"].ToString() != string.Empty)
             {
                 allowedCAExigenPolicyValues = ConfigurationSettings.AppSettings["AllowedCAExigenPolicies"].ToString();
             }
             if (ConfigurationSettings.AppSettings["AllowedAZExigenPolicies"] != null && ConfigurationSettings.AppSettings["AllowedAZExigenPolicies"].ToString() != string.Empty)
             {
                 allowedAZExigenPolicyValues = ConfigurationSettings.AppSettings["AllowedAZExigenPolicies"].ToString();
             }
             if (allowedExigenPolicyValues.IndexOf(stateProductCode) < 0)
             {
                 Invalid_Message = Constants.POLICY_INVALIDCODE;
             }

             if (restrictedExigenPolicyValues.IndexOf(stateProductCode) >= 0)
             {
                 Invalid_Message = Constants.PCI_EXG_RESTRICTED_POLICY;
             }
             if (!CSAAWeb.Validate.IsAllNumeric(exPolicyNo))
             {
                 Invalid_Message = Constants.PCI_EXG_NUMERIC;
             }
             if ((ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Auto"])) && (allowedCAExigenPolicyValues.IndexOf(stateProductCode) < 0))
             {
                 if (allowedAZExigenPolicyValues.IndexOf(stateProductCode) >= 0)
                 {
                     Invalid_Message = Constants.PCI_INVALID_POLICY_PRODUCT;
                 }
             }
             else if ((ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.ArizonaAuto"])) && (allowedAZExigenPolicyValues.IndexOf(stateProductCode) < 0))
             {
                 if (allowedCAExigenPolicyValues.IndexOf(stateProductCode) >= 0)
                 {
                     Invalid_Message = Constants.PCI_INVALID_POLICY_PRODUCT;
                 }
             }
             //PAS AZ Product COnfiguration CH2:End Modified the below code to handle the new policy prefix for PAS AZ policy by cognizant on 03/12/2012
             return Invalid_Message;
         } */
        //MAIG - CH10 - END - Commented the below method as part of Common Product Types change

        //MAIG - CH11 - BEGIN - Commented the below method as part of Common Product Types change

        //Method to check the Legacy Policy Sequence
        private string Check_Legacy_Policy_Seq_Validation()
        {

            string PolicyNumber = (Convert.ToString(Policy)).Trim().ToUpper();
            string strAlphabet = PolicyNumber.Substring(0, 2);
            string stralphabetp = PolicyNumber.Substring(0, 3);
            Regex objPattern = new Regex("[A-Z]");
            bool IsAlphabet = objPattern.IsMatch(strAlphabet);
            Char[] cr = strAlphabet.ToCharArray();
            string firstchar = Convert.ToString(cr[0]);
            string secondchar = Convert.ToString(cr[1]);

            bool IsValid_Legacy = Regex.IsMatch(PolicyNumber.ToUpper(), @"^[A-Z0-9\-]+$");
            if (IsValid_Legacy)
            {
                ValidSeq1 = (bool)((SiteTemplate)Page).OrderService.Lookup("Insurance", "ValidateCheckDigit", new object[] { _Policy.Text.Trim().ToString() });
                if (ValidSeq1)
                {
                    if ((!ProductType.ToUpper().Equals(MSC.SiteTemplate.ProductCodes.HO.ToString())))
                    {
                        Invalid_Message = Constants.PCI_INVALID_POLICY_PRODUCT;
                    }
                }
                else
                {
                    Invalid_Message = Constants.PCI_INVALID_SEQUENCE;
                }

            }
            else
            {
                Invalid_Message = Constants.PCI_LEGACY_ALPHANUMERIC;
            }
            return Invalid_Message;

        }
        //MAIG - CH11 - END - Commented the below method as part of Common Product Types change

        //Method to check the Legacy Policy Product
        private string Check_Legacy_Policy_Product_Validation()
        {
            string PolicyNumber = (Convert.ToString(Policy)).Trim().ToUpper();
            string strAlphabet = PolicyNumber.Substring(0, 2);
            string stralphabetp = PolicyNumber.Substring(0, 3);
            Regex objPattern = new Regex("[A-Z]");
            bool IsAlphabet = objPattern.IsMatch(strAlphabet);
            Char[] cr = strAlphabet.ToCharArray();
            string firstchar = Convert.ToString(cr[0]);
            string secondchar = Convert.ToString(cr[1]);

            //			67811A0  - PCI Remediation for Payment systems - Added Trim condition to the policy number field.
            if (CSAAWeb.Validate.IsAllNumeric(_Policy.Text.Trim()))
            {
                if (ProductType != Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Home"]))
                {
                    Invalid_Message = Constants.PCI_INVALID_POLICY_PRODUCT1;
                    return Invalid_Message;
                }
            }
            // CHG0083477 - Expansion of Homeowner Policy - Added additional characters (G, I, O, Q, S, Z) to the conditions.
            if ((cr[0] == 'H' || cr[0] == 'V' || cr[0] == 'W' || cr[0] == 'X' || cr[0] == 'G' || cr[0] == 'I' || cr[0] == 'O' || cr[0] == 'Q' || cr[0] == 'S' || cr[0] == 'Z') && (objPattern.IsMatch(secondchar.ToUpper())))
            {
                if (ProductType != Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Home"]))
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
                    if (ProductType != Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Auto"]))
                    {
                        Invalid_Message = Constants.PCI_INVALID_POLICY_PRODUCT1;
                        return Invalid_Message;
                    }
                }

            }

            if ((objPattern.IsMatch(firstchar.ToUpper()) && CSAAWeb.Validate.IsAllNumeric(secondchar)) || (objPattern.IsMatch(secondchar.ToUpper()) && CSAAWeb.Validate.IsAllNumeric(firstchar)))
            {
                if (ProductType != Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Auto"]))
                {
                    Invalid_Message = Constants.PCI_INVALID_POLICY_PRODUCT1;
                    return Invalid_Message;
                }

            }

            return "";
        }

        //Policy sequence, Policy and product type selected for the PUP policy validations
        private string Check_PUP_Policy()
        {

            //MAIG - CH12 - BEGIN - Commented the code to remove the PUP prefix
            string PupPolicyNo = string.Empty;
            //			67811A0  - PCI Remediation for Payment systems - Added Trim condition to the policy number field.
            /*PupPolicyNo = _Policy.Text.Trim().Substring(3, 7);
            string PupCode = string.Empty;
            PupCode = _Policy.Text.Trim().Substring(0, 3);
            //			67811A0  - PCI Remediation for Payment systems - Added Trim condition to the policy number field.
            if (PupCode.ToUpper() != "PUP")
            {
                Invalid_Message = Constants.POLICY_INVALIDCODE;
            }*/
            if (!CSAAWeb.Validate.IsAllNumeric(Policy.Trim()))
            {
                Invalid_Message = Constants.PUP_NUMERIC_VALID;
            }
            else if (Policy.Trim().Length != 7)
            {
                Invalid_Message = Constants.POLICY_LENGTH_HOME_MESSAGE;
            }
            /*if (ProductType != Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.PUP"]))
            {
                Invalid_Message = Constants.PCI_INVALID_POLICY_PRODUCT;
            }*/
            //MAIG - CH12 - END - Commented the code to remove the PUP prefix
            return Invalid_Message;
        }

        //MAIG - CH13 - BEGIN - Commented the below method as part of Common Product Types change
        //Policy sequence, Policy and product type selected for the HUON policy validations
        /*private string Check_HUON_Policy()
        {
            //			67811A0  - PCI Remediation for Payment systems - Added Trim condition to the policy number field.

            if (CSAAWeb.Validate.IsAllNumeric(_Policy.Text.Trim()))
            {
                bool ValidSeq = (bool)((SiteTemplate)Page).OrderService.Lookup("Insurance", "ValidateHUONCheckDigit", new object[] { _Policy.Text.Trim().ToString() });
                //CHG0078293 - PT Enhancement - Added the condition to check for the first two digits of policy number along with the sequence validation for HUON policy
                if (!ValidSeq || !_Policy.Text.Substring(0, 2).Contains(Config.Setting("HUONStartDigit")))
                {
                    Invalid_Message = Constants.PCI_INVALID_SEQUENCE;
                }
            }

            else
            {
                Invalid_Message = Constants.PCI_POLICY_NUMERIC;
            }
            if (ProductType != Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Auto"]))
            {
                Invalid_Message = Constants.PCI_INVALID_POLICY_PRODUCT;
            }
            return Invalid_Message;

        } */
        //MAIG - CH13 - END - Commented the below method as part of Common Product Types change

        //Policy Validation for WU Policy All Numeric, Length between 4 and 9
        private string Check_WU_Policy()
        {
            if (!CSAAWeb.Validate.IsAllNumeric(_Policy.Text.Trim()))
            {
                Invalid_Message = Constants.PCI_POLICY_NUMERIC;

            }
            else if (!((_Policy.Text.Trim().Length >= 4) && (_Policy.Text.Trim().Length <= 9)))
            {
                Invalid_Message = Constants.PCI_SIS_POLICY_LENGTH;
            }

            return Invalid_Message;
        }

        #endregion

        #region Procedures

        /// <summary>
        /// Adds a Select Item row to the table.
        /// </summary>
        /// 
        private void AddSelectItem(DataTable Dt, string Description)
        {
            DataRow Row = Dt.NewRow();
            Row["ID"] = 0;
            Row["Description"] = Description;
            Dt.Rows.InsertAt(Row, 0);

        }

        //Assigning values to the context to be passed to the next screen
        private void StoreLineItemDetails(string strDueDetails)
        {
            //MAIG - CH14 - BEGIN - Modifed the below code to retain the value on back and forth navigation
            if (Context.Items["DueDetails"] != null)
            { Context.Items["DueDetails"] = strDueDetails; }
            else
            { Context.Items.Add("DueDetails", strDueDetails); }
            if (Context.Items["AmountSelected"] != null)
            { Context.Items["AmountSelected"] = HiddenAmountSelected.Text; }
            else
            {
                Context.Items.Add("AmountSelected", HiddenAmountSelected.Text);
            }
            //MAIG - CH14 - END - Modifed the below code to retain the value on back and forth navigation
        }

        private void StoreLineItemDetails(DataTable dtDBResultSet)
        {
            string tmpFirstName = string.Empty;
            string tmpLastName = string.Empty;
            string tmpDueDetails = string.Empty;

            tmpFirstName = dtDBResultSet.Rows[0]["First_Name"].ToString();
            tmpFirstName = tmpFirstName.Replace("'", " ");
            tmpFirstName = tmpFirstName.Replace("/", " ");
            FirstName = tmpFirstName.Trim();

            tmpLastName = dtDBResultSet.Rows[0]["Last_Name"].ToString();
            tmpLastName = tmpLastName.Replace("'", " ");
            tmpLastName = tmpLastName.Replace("/", " ");
            LastName = tmpLastName.Trim();

            if (dtDBResultSet.Rows[0]["Total_Amount"].ToString().Trim() == "0.0000")
            {
                dtDBResultSet.Rows[0]["Total_Amount"] = "0.00";
            }

            if (dtDBResultSet.Rows[0]["Due_Amount"].ToString().Trim() == "0.0000")
            {
                dtDBResultSet.Rows[0]["Due_Amount"] = "0.00";
            }
            tmpDueDetails = FirstName + "|" + LastName + "|" +
                            dtDBResultSet.Rows[0]["Due_Date"].ToString() + "|" +
                            dtDBResultSet.Rows[0]["Total_Amount"].ToString() + "|" +
                            dtDBResultSet.Rows[0]["Due_Amount"].ToString() + "|";
            //MAIG - CH15 - BEGIN - Updated the logic to send the PolicyState and Prefix. Removed the CompanyCode sending logic. 
            if (dtDBResultSet.Columns["POL_STATE"] != null && dtDBResultSet.Rows[0]["POL_PREFIX"] != null /*&& IsWesternUnitedProduct()*/)
            {
                //Only for Western United
                //Company code fix - added company ID
                tmpDueDetails = tmpDueDetails + "|" +
                            dtDBResultSet.Rows[0]["POL_PREFIX"].ToString().Trim() + "|" +
                            dtDBResultSet.Rows[0]["POL_STATE"].ToString().Trim(); //+ "|" + dtDBResultSet.Rows[0]["COMPANY_ID"].ToString().Trim();
            }
            //AZ PAS conversion and PC integration CH7 START- Added teh company ID to the tmpdue details for non PCP trasnctions
            /*else
            {

                tmpDueDetails = tmpDueDetails + "|" + dtDBResultSet.Rows[0]["COMPANY_ID"].ToString().Trim();
            }*/
            //MAIG - CH15 - END - Updated the logic to send the PolicyState and Prefix. Removed the CompanyCode sending logic. 
            //AZ PAS conversion and PC integration CH7 END - Added teh company ID to the tmpdue details for non PCP trasnctions
            //MAIG - CH16 - BEGIN - Modifed the below code to retain the value on back and forth navigation
            if (Context.Items["DueDetails"] != null)
            { Context.Items["DueDetails"] = tmpDueDetails; }
            else
            {
                Context.Items.Add("DueDetails", tmpDueDetails);
            }
            if (dtDBResultSet.Rows[0]["Due_Amount"].ToString() != "" && dtDBResultSet.Rows[0]["Due_Amount"].ToString() != "0.00")
            {
                if (Context.Items["AmountSelected"] != null)
                { Context.Items["AmountSelected"] = "M"; }
                else
                {
                    Context.Items.Add("AmountSelected", "M");
                }
            }
            else if (dtDBResultSet.Rows[0]["Total_Amount"].ToString() != "" && dtDBResultSet.Rows[0]["Total_Amount"].ToString() != "0.00")
            {
                if (Context.Items["AmountSelected"] != null)
                { Context.Items["AmountSelected"] = "T"; }
                else
                {
                    Context.Items.Add("AmountSelected", "T");
                }
            }
            else
            {
                if (Context.Items["AmountSelected"] != null)
                { Context.Items["AmountSelected"] = "O" ;}
                else
                {
                    Context.Items.Add("AmountSelected", "O");
                }
                //MAIG - CH16 - END - Modifed the below code to retain the value on back and forth navigation
            }

        }

        #endregion

        #region Functions
        //method for SIS policy length validation
        private string WesternUnitedPolicyLength()
        {
            string Invalid_Message = string.Empty;
            //			67811A0  - PCI Remediation for Payment systems - Added Trim condition to the policy number field.
            if (_Policy.Text.Trim().Length < 4 || _Policy.Text.Trim().Length > 9)
            {
                Invalid_Message = Constants.PCI_SIS_POLICY_LENGTH;
            }
            else if (!CSAAWeb.Validate.IsAllNumeric(_Policy.Text.Trim()))
            {

                Invalid_Message = Constants.PCI_POLICY_NUMERIC;
            }
            return Invalid_Message;
        }

        //MAIG - CH17 - BEGIN - Commented the below method as part of Common Product Types change
        //Method to determine the selected product type.
        /*private bool IsWesternUnitedProduct()
        {
            if ((Config.Setting("PCP.Products")).IndexOf(Convert.ToString(Productcode)) > -1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }*/
        //MAIG - Ch17 - END - Commented the below method as part of Common Product Types change

        //MAIG - Ch18 - BEGIN - Removed the code since the method is not used anywhere
        //Method to determine the selected product type.
        //private bool SIS_Policy_Product_Validation(string strSISProductType)
        //{
        //    return (SISPolicyMapper(strSISProductType.Trim()) == ProductType.Trim()) ? true : false;
        //}
        //MAIG - CH18 - END - Removed the code since the method is not used anywhere

        #endregion

        #region Events & Validators

        ///<summary>Validator delegate for Type list boxes.</summary>
        protected void CheckSelection(object source, ValidatorEventArgs e)
        {

            string st = ((Validator)source).ID;
            st = st.Substring(0, st.Length - 1);
            Validator V = ((Validator)FindControl(st));

            e.IsValid = (((ListControl)e.Check).Items.Count == 1 || ((ListControl)e.Check).SelectedIndex > 0);
            if (!e.IsValid)
            {
                e.IsValid = false;

                V.MarkInvalid();
            }
        }
        //Tool Tip and the enabling and disabling of revenue type drop down and policy number text box
        protected void _ProductType_SelectedIndexChanged(object sender, EventArgs e)
        {

            if (_ProductType.SelectedItem.Text != "Select Product Type")
            {
                //MAIG - CH19 - BEGIN - Modified/Commented the Code to change the Tooltip and Revenue Type needs to be disabled.
                //_RevenueType.Enabled = true;
                Control Policy_Control = this.FindControl("_Policy");

                TextBox Policy_Textbox = (TextBox)Policy_Control.FindControl("_Policy");

                Policy_Textbox.Enabled = true;
                //_RevenueType.ClearSelection();
                Policy_Textbox.Text = "";

                if (ProductType.Equals(MSC.SiteTemplate.ProductCodes.PA.ToString()) || ProductType.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()))
                {
                    Policy_Textbox.ToolTip = Constants.AUTO_HOME_TOOLTIP;
                }
                else
                {
                    Policy_Textbox.ToolTip = Constants.OTHER_TOOLTIP;
                }

                /*
                if (ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.PUP"]))
                {
                    Policy_Textbox.ToolTip = Constants.PCI_PUP_TOOLTIP;
                }
                if (ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Auto"]))
                {
                    Policy_Textbox.ToolTip = Constants.PCI_AUTO_TOOLTIP;
                }
                if (ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Home"]))
                {
                    Policy_Textbox.ToolTip = Constants.PCI_HOME_TOOLTIP;
                }
                */
                //MAIG - CH19 - END - Modified/Commented the Code to change the Tooltip and Revenue Type needs to be disabled.
                //MAIG - CH20 - BEGIN - Logic is removed since the Revenue Type is handled in Menu
                //if (IsWesternUnitedProduct())
                //{
                //    Policy_Textbox.ToolTip = Constants.PCI_SIS_TOOLTIP;
                //    _RevenueType.SelectedValue = "1291";
                //    _RevenueType.Enabled = false;
                //    Policy_Textbox.Focus();
                //}
                ////AZ PAS conversion and PC integration CH5 START : start Added the code to default the revenue type to Installment type and disable the revenue type drop down box by cognizant on 03/06/2011
                //else if (ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.ArizonaAuto"]))
                //{
                //    Policy_Textbox.ToolTip = Constants.AZAUTO_TOOLTIP;
                //    _RevenueType.SelectedValue = "1291";
                //    _RevenueType.Enabled = false;
                //    Policy_Textbox.Focus();
                //}
                ////AZ PAS conversion and PC integration CH5 END : start Added the code to default the revenue type to Installment type and disable the revenue type drop down box by cognizant on 03/06/2011
                //else if (ProductType == Config.Setting("ProductCode.HighLimit").ToString())
                //{
                //    Policy_Textbox.ToolTip = Constants.PYMT_HLTOOLTIP;
                //    _RevenueType.SelectedValue = "1291";
                //    _RevenueType.Enabled = false;
                //    Policy_Textbox.Focus();
                //}
                ////PAS AZ Product COnfiguration CH1: End Added the code to default the revenue type to Installment type and disable the revenue type drop down box by cognizant on 03/06/2011
                ////PAS HO CH1 - START : Added the code to default the revenue type to Installment type and disable the revenue type drop down box by cognizant on 6/12/2014
                //else if (ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.SSHome"]))
                //{
                //    Policy_Textbox.ToolTip = Constants.SSHOME_TOOLTIP;
                //    _RevenueType.SelectedValue = "1291";
                //    _RevenueType.Enabled = false;
                //    Policy_Textbox.Focus();
                //}
                ////PAS HO CH1 - END : Added the code to default the revenue type to Installment type and disable the revenue type drop down box by cognizant on 6/12/2014
                //else
                //{
                //    _RevenueType.Focus();
                //}
                //MAIG - CH20 - END - Logic is removed since the Revenue Type is handled in Menu

            }

        }
        //AZ PAS Conversion and PC integration -CH10 - START - Added the below method to identify the data source of the selected product type
        public string DataSource(string ProductType, int iPolLength)
        {
            string PC_DataSource = string.Empty;
            //MAIG - CH21 - BEGIN - Modified the Old logic for getting the DataSource to RPBS Service
            //if (Policy.Length == 13)
            //{
            //    PC_DataSource = Config.Setting("DataSource.Exigen");
            //}
            //else
            if (ProductType.Equals(MSC.SiteTemplate.ProductCodes.DF.ToString()) || ProductType.Equals(MSC.SiteTemplate.ProductCodes.MC.ToString()) || ProductType.Equals(MSC.SiteTemplate.ProductCodes.WC.ToString()))
            {
                PC_DataSource = Config.Setting("DataSource.SIS");
            }
            else if (Productcode.ToUpper().ToString() == Config.Setting("ProductCode.PUP"))
            {
                PC_DataSource = Config.Setting("DataSource.PUP");
            }

            /*if (ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Auto"]))
            {
                if (iPolLength == 7)
                {
                    PC_DataSource = Config.Setting("DataSource.POESAuto");
                }
                else if (iPolLength == 13)
                {
                    PC_DataSource = Config.Setting("DataSource.Exigen");
                }
                else if (iPolLength == 9)
                {
                    PC_DataSource = Config.Setting("DataSource.HUON");
                }
            }
            else if (ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Home"]))
            {
                if (iPolLength == 7)
                {
                    PC_DataSource = Config.Setting("DataSource.POESHome");
                }
            }
            //PAS HO CH6 - START : Added the below code to include the PAS HO to get the PC DataSource 6/13/2014
            else if ((ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.ArizonaAuto"])) || (ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.SSHome"])))
            //PAS HO CH6 - END : Added the below code to include the PAS HO to get the PC DataSource 6/13/2014
            {
                PC_DataSource = Config.Setting("DataSource.Exigen");
            }
            else if (ProductType == Config.Setting("ProductCode.HighLimit"))
            {
                PC_DataSource = Config.Setting("DataSource.HL");
            }
            else if (Productcode.ToUpper().ToString() == Config.Setting("ProductCode.PUP"))
            {
                Policy = Policy.Substring(3, 7);
                PC_DataSource = Config.Setting("DataSource.PUP");
            }
            else if ((Config.Setting("PCP.Products")).IndexOf(Convert.ToString(Productcode)) > -1)
            {
                PC_DataSource = Config.Setting("DataSource.SIS");
            }*/
            //MAIG - CH21 - END - Modified the Old logic for getting the DataSource to RPBS Service
            return PC_DataSource;

        }
        //AZ PAS Conversion and PC integration -CH10 END - Added the below method to identify the data source of the selected product type
        //Method performing Policy sequence, length and policy validations, IVR look up.
        protected void CheckPolicyLength(Object Source, CSAAWeb.WebControls.ValidatorEventArgs e)
        {
            //MAIGEnhancement CHG0107527 - CH6 - BEGIN - Modified the below condition to append the source system which helps to skip the RPBS Service hit if user navigated back & forth in Name Search/Duplicate Policy scenario
            string tmpPolicyDetails = _Policy.Text.Trim() + _RevenueType.SelectedItem.Value + _ProductType.SelectedItem.Value+HiddenSelectedDuplicatePolicy.Text;
            //MAIGEnhancement CHG0107527 - CH6 - END - Modified the below condition to append the source system which helps to skip the RPBS Service hit if user navigated back & forth in Name Search/Duplicate Policy scenario
            string confirmMessage = string.Empty;
            bool blnValidIVR = false;
            string confirmMessage1 = string.Empty;
            //Default IsOverride Property to 0 always. This will be set to 1 On Policy Override.


            if (HidddenPolicyValidate.Text != tmpPolicyDetails)
            {
                if (Context.Items["NewPolicy"] == null)
                {
                    Context.Items.Add("NewPolicy", "Y");
                }

                IsOverride = 0;
                //MAIG - CH22 - BEGIN - Modified/Added logic to remove Old Policy Number validations and Added New Policy Number Validation
                Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                //bool blnWesternUnitedProduct = IsWesternUnitedProduct();
                //			67811A0  - PCI Remediation for Payment systems - Added Trim condition to the policy number field.

                HiddenField SkipParentValidation = (HiddenField)_NameSearch.FindControl("SkipParentValidation");
                TextBox txtLastName = (TextBox)_NameSearch.FindControl("_LastName");
                TextBox txtMailingZip = (TextBox)_NameSearch.FindControl("_MailingZip");
                TextBox txtFirstName = (TextBox)_NameSearch.FindControl("_FirstName");
                Panel pnlCustomerName = (Panel)_NameSearch.FindControl("pnlCustomerNameMain");
                LinkButton lnkCustomerName = (LinkButton)_NameSearch.FindControl("lnkSearchByCustomerName");
                HtmlTable CriteriaParameters = (HtmlTable)_NameSearch.FindControl("table1");
                this.Parent.FindControl("PageValidator2").Visible = true;
                Label lblLastName = (Label)_NameSearch.FindControl("lblLastName");
                Label lblFirstName = (Label)_NameSearch.FindControl("lblFirstName");
                lblFirstName.ForeColor = System.Drawing.Color.Black;
                lblLastName.ForeColor = System.Drawing.Color.Black;
                Label lblMailingZip = (Label)_NameSearch.FindControl("lblMailingZip");
                lblMailingZip.ForeColor = System.Drawing.Color.Black;
                if (_ProductType.SelectedIndex == 0) { Policy = string.Empty; }
                if ((_RevenueType.SelectedIndex != 0) && (Policy.Trim() != "") && (_ProductType.SelectedIndex != 0) && !(!string.IsNullOrEmpty(SkipParentValidation.Value) && SkipParentValidation.Value.Equals("true")))
                {
                    ValidIIBProd = false;
                    ValidSIS = false;
                    int iPolLength = Policy.Trim().Length;
                    if (Convert.ToInt32(RevenueType) == Convert.ToInt32(((StringDictionary)Application["Constants"])["RevenueCode.DownPayment"]))
                    {
                        //CHG0110069 - CH2 - BEGIN - Modified the below logic to remove the Auto product Type from the condition since it should not be allowed
                        if (!((ProductType.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString())
                            && iPolLength == 8)
                            || (ProductType.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString())
                            && iPolLength == 7)
                            || (ProductType.Equals(MSC.SiteTemplate.ProductCodes.PU.ToString())
                            && iPolLength == 7)))
                        //CHG0110069 - CH2 - END - Modified the below logic to remove the Auto product Type from the condition since it should not be allowed
                        {
                            lenvld.ErrorMessage = Constants.PCI_REVENUE_INVALID;
                            e.IsValid = false;
                            lenvld.Enabled = true;
                            _Policy.PolicyInValid();
                            lenvld.MarkInvalid();
                            vldPolicyLength.ErrorMessage = "";
                        }
                        //Added below condition to check if the given invalid 7 digit Home policy is a valid HDES sequence or not. 
                        if (iPolLength == 7 && ProductType.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()))
                        {
                            Invalid_Message = Check_Legacy_Policy_Seq_Validation();
                            ValidSIS = false;
                            if (Invalid_Message != "")
                            {
                                ValidIIBProd = true;
                                lenvld = (Validator)_Policy.FindControl("LabelValidator");

                                lenvld.ErrorMessage = Invalid_Message;
                                e.IsValid = false;
                                lenvld.Enabled = true;
                                _Policy.PolicyInValid();
                                lenvld.MarkInvalid();
                                vldPolicyLength.ErrorMessage = "";

                            }
                        }
                        /*if(ProductType.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()) && Validate.IsAllNumeric(Policy.Trim()) && iPolLength==7)
                        {
                            string isValid = CSAAWeb.Cryptor.CheckDigit(Policy.Trim());
                            if (!isValid.Equals("0"))
                            {
                                lenvld.ErrorMessage = Constants.PCI_INVALID_SEQUENCE;
                                e.IsValid = false;
                                lenvld.Enabled = true;
                                _Policy.PolicyInValid();
                                lenvld.MarkInvalid();
                                vldPolicyLength.ErrorMessage = "";
                            }
                        }*/

                    }
                    //MAIG - Removed the Else If Condition to execute the below logic everytime 11/26/2014
                    if (ProductType.Equals(MSC.SiteTemplate.ProductCodes.PA.ToString()) || ProductType.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()))
                    {
                        //CHG0110069 - CH3 - BEGIN - Added the below logic to only allow KIC policies if they are Numeric for Down payments
                        if (Convert.ToInt32(RevenueType) == Convert.ToInt32(((StringDictionary)Application["Constants"])["RevenueCode.DownPayment"])
                            && ProductType.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()) && iPolLength==8
                            && !Validate.IsAllNumeric(Policy.Trim()))
                        {
                            lenvld.ErrorMessage = Constants.KIC_ALPHANUMERIC_INVALID;
                            e.IsValid = false;
                            lenvld.Enabled = true;
                            _Policy.PolicyInValid();
                            lenvld.MarkInvalid();
                            vldPolicyLength.ErrorMessage = "";
                        }
                        //CHG0110069 - CH3 - END - Added the below logic to only allow KIC policies if they are Numeric for Down payments
                        if (!(Validate.IsAlphaNumeric(Policy.Trim())) || (iPolLength < 4 || iPolLength > 13))
                        {
                            lenvld.ErrorMessage = Constants.AUTO_HOME_MESSAGE;
                            e.IsValid = false;
                            lenvld.Enabled = true;
                            _Policy.PolicyInValid();
                            lenvld.MarkInvalid();
                            vldPolicyLength.ErrorMessage = "";
                        }
                    }
                    else if (ProductType.Equals(MSC.SiteTemplate.ProductCodes.PU.ToString()))
                    {
                        //MAIG - CH31 - BEGIN - Added condition to skip validation if the policy length is 13, for an PAS Home Policy
                        if (!(iPolLength == 13 && Validate.IsAlphaNumeric(Policy.Trim())))
                        {
                            Invalid_Message = Check_PUP_Policy();
                        }
                        //MAIG - CH31 - END - Added condition to skip validation if the policy length is 13, for an PAS Home Policy
                        if (!string.IsNullOrEmpty(Invalid_Message))
                        {
                            lenvld.ErrorMessage = Invalid_Message;
                            e.IsValid = false;
                            lenvld.Enabled = true;
                            _Policy.PolicyInValid();
                            lenvld.MarkInvalid();
                            vldPolicyLength.ErrorMessage = "";
                        }
                    }
                    else if (ProductType.Equals(MSC.SiteTemplate.ProductCodes.DF.ToString()) || ProductType.Equals(MSC.SiteTemplate.ProductCodes.MC.ToString()) || ProductType.Equals(MSC.SiteTemplate.ProductCodes.WC.ToString()))
                    {
                        //MAIG - CH32 - BEGIN - Added condition to skip validation if the policy length is 13, for an PAS Home Policy
                        if (!(ProductType.Equals(MSC.SiteTemplate.ProductCodes.DF.ToString()) && iPolLength == 13 && Validate.IsAlphaNumeric(Policy.Trim())))
                        {
                            Invalid_Message = WesternUnitedPolicyLength();
                        }
                        //MAIG - CH32 - END - Added condition to skip validation if the policy length is 13, for an PAS Home Policy
                        if (!string.IsNullOrEmpty(Invalid_Message))
                        {
                            lenvld.ErrorMessage = Invalid_Message;
                            e.IsValid = false;
                            lenvld.Enabled = true;
                            _Policy.PolicyInValid();
                            lenvld.MarkInvalid();
                            vldPolicyLength.ErrorMessage = "";
                        }
                    }
                    else
                    {
                        Invalid_Message = Constants.POLICY_INVALIDCODE;
                        lenvld.ErrorMessage = Invalid_Message;
                        e.IsValid = false;
                        lenvld.Enabled = true;
                        _Policy.PolicyInValid();
                        lenvld.MarkInvalid();
                        vldPolicyLength.ErrorMessage = "";
                    }

                    /* if (blnWesternUnitedProduct)
                    {
                        Invalid_Message = WesternUnitedPolicyLength();
                        if (Invalid_Message != "")
                        {
                            ValidSIS = true;
                            Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                            lenvld.ErrorMessage = Invalid_Message;
                            e.IsValid = false;
                            lenvld.Enabled = true;
                            _Policy.PolicyInValid();
                            lenvld.MarkInvalid();
                            vldPolicyLength.ErrorMessage = "";
                        }

                    }
                    else if (ProductType == Config.Setting("ProductCode.HighLimit"))
                    {
                        ValidSIS = false;
                        if (iPolLength != 10)
                        {
                            Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                            //Included as a part of Defect 346
                            Invalid_Message = CSAAWeb.Constants.PCI_INVALID_POLICY_PRODUCT;
                            lenvld.ErrorMessage = Invalid_Message;
                            e.IsValid = false;
                            lenvld.Enabled = true;
                            _Policy.PolicyInValid();
                            lenvld.MarkInvalid();
                            ValidIIBProd = true;
                            vldPolicyLength.ErrorMessage = "";
                        }
                        else if (!CSAAWeb.Validate.IsAllNumeric(Policy))
                        {
                            Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                            Invalid_Message = CSAAWeb.Constants.PCI_POLICY_NUMERIC;
                            lenvld.ErrorMessage = Invalid_Message;
                            e.IsValid = false;
                            lenvld.Enabled = true;
                            _Policy.PolicyInValid();
                            lenvld.MarkInvalid();
                            ValidIIBProd = true;
                            vldPolicyLength.ErrorMessage = "";
                        }
                    }
                    else if (iPolLength == 7)
                    {
                        Invalid_Message = Check_Legacy_Policy_Seq_Validation();
                        ValidSIS = false;
                        if (Invalid_Message != "")
                        {
                            ValidIIBProd = true;
                            Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");

                            lenvld.ErrorMessage = Invalid_Message;
                            e.IsValid = false;
                            lenvld.Enabled = true;
                            _Policy.PolicyInValid();
                            lenvld.MarkInvalid();
                            vldPolicyLength.ErrorMessage = "";

                        }
                    }
                    else if (iPolLength == 9)
                    {
                        ValidSIS = false;
                        Invalid_Message = Check_HUON_Policy();
                        if (Invalid_Message != "")
                        {
                            ValidIIBProd = true;
                            Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");

                            lenvld.ErrorMessage = Invalid_Message;
                            e.IsValid = false;
                            lenvld.Enabled = true;
                            _Policy.PolicyInValid();
                            lenvld.MarkInvalid();
                            vldPolicyLength.ErrorMessage = "";

                        }
                    }
                    //AZ PAS conversion and PC integration CH6 START - Modified the below code to validate the policy for HL product type
                    else if (iPolLength == 10)
                    {
                        ValidSIS = false;
                        if (ProductType != Config.Setting("ProductCode.HighLimit"))
                        {
                            Invalid_Message = Check_PUP_Policy();
                            if (Invalid_Message != "")
                            {
                                ValidIIBProd = true;
                                Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");

                                lenvld.ErrorMessage = Invalid_Message;
                                e.IsValid = false;
                                lenvld.Enabled = true;


                                _Policy.PolicyInValid();

                                lenvld.MarkInvalid();
                                vldPolicyLength.ErrorMessage = "";

                            }
                        }
                        else
                        {
                            if (!CSAAWeb.Validate.IsAllNumeric(Policy))
                            {
                                Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                Invalid_Message = CSAAWeb.Constants.PCI_POLICY_NUMERIC;
                                lenvld.ErrorMessage = Invalid_Message;
                                e.IsValid = false;
                                lenvld.Enabled = true;
                                _Policy.PolicyInValid();
                                lenvld.MarkInvalid();
                                ValidIIBProd = true;
                                vldPolicyLength.ErrorMessage = "";
                            }
                        }

                    }

                        //AZ PAS conversion and PC integration CH6 END - Modified the below code to validate the policy for HL product type
                    else if (iPolLength == 13)
                    {
                        ValidSIS = false;
                        //PAS HO CH8 - START : Added the below code to skip validations for AAA SS HO policies 6/13/2014
                        if (!(ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.SSHome"])))
                        {
                            Invalid_Message = Check_Exigen_Policy();
                        }
                        //PAS HO CH8 - END : Added the below code to skip validations for AAA SS HO policies 6/13/2014
                        if (Invalid_Message != "")
                        {
                            ValidIIBProd = true;
                            Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");

                            lenvld.ErrorMessage = Invalid_Message;
                            e.IsValid = false;
                            lenvld.Enabled = true;
                            _Policy.PolicyInValid();
                            lenvld.MarkInvalid();
                            vldPolicyLength.ErrorMessage = "";
                        }
                    }
                    else
                    {
                        ValidSIS = false;
                        Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                        ValidIIBProd = true;

                        if (ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.PUP"]))
                        {
                            lenvld.ErrorMessage = Constants.POLICY_LENGTH_PUP_MESSAGE;
                        }
                        else if (ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Auto"]))
                        {
                            lenvld.ErrorMessage = Constants.POLICY_LENGTH_AUTO_MESSAGE;
                        }
                        else if (ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Home"]))
                        {
                            lenvld.ErrorMessage = Constants.POLICY_LENGTH_HOME_MESSAGE;
                        }

                        e.IsValid = false;
                        lenvld.Enabled = true;


                        _Policy.PolicyInValid();

                        lenvld.MarkInvalid();
                        vldPolicyLength.ErrorMessage = "";
                    }*/
                    //MAIG - CH22 - END - Modified/Added logic to remove Old Policy Number validations and Added New Policy Number Validation
                    //Billing summary look up for IIB polices and SIS policies and concadinating the values.
                    //AZ PAS conversion and PC integration -CH1 START -Modified the below code to lookup the policy details from the billing summary lookup
                    if (e.IsValid && iPolLength != 0)
                    {
                        //MAIG - CH23 - BEGIN - Updated the Common Home Product Type instead of AAASS Home and commented Old logic to get the Product Code from DB
                        Panel pnlNameSearch = (Panel)_NameSearch.FindControl("pnlCustomerNameMain");
                        if (pnlNameSearch.Visible && CriteriaParameters.Visible)
                        {
                            TextBox custFirstName = (TextBox)_NameSearch.FindControl("_FirstName");
                            TextBox custLastName = (TextBox)_NameSearch.FindControl("_LastName");
                            TextBox custMailingZip = (TextBox)_NameSearch.FindControl("_MailingZip");
                            TextBox PolicyNumber = (TextBox)_Policy.FindControl("_Policy");
                            GridView NameSearchResult = (GridView)_NameSearch.FindControl("NameSearchResults");
                            PolicyNumber.Enabled = false;
                            custLastName.Focus();
                            //if (Context.Items["NameSearchGridData"] != null)
                            //{
                            //    Context.Items["NameSearchGridData"] = NameSearch.SearchResult;
                            //}
                            //else { Context.Items.Add("NameSearchGridData", NameSearch.SearchResult); }
                            string[] selectedRowArray = HiddenNameSearch.Text.Split('|');
                            if (Context.Items["NameSearchData"] != null)
                            {
                                Context.Items["NameSearchData"] = string.Format("{0}|{1}|{2}|{3}|{4}", custFirstName.Text, custLastName.Text, custMailingZip.Text, selectedRowArray[2], selectedRowArray[3]);
                            }
                            else { Context.Items.Add("NameSearchData", string.Format("{0}|{1}|{2}|{3}|{4}", custFirstName.Text, custLastName.Text, custMailingZip.Text, selectedRowArray[2], selectedRowArray[3])); }
                        }
                        else if (pnlNameSearch.Visible && !CriteriaParameters.Visible)
                        {
                            string[] selectedArray = HiddenNameSearch.Text.Split('|');
                            if (selectedArray.Length > 3)
                            {
                                if (Context.Items["DuplicatePolicyData"] == null)
                                {
                                    Context.Items.Add("DuplicatePolicyData", string.Format("{0}|{1}|{2}|{3}", Policy, _ProductType.SelectedValue, string.Empty, selectedArray[2]));
                                }
                            }

                            //if (Context.Items["DuplicatePolicyData"] == null)
                            //{
                            //    Context.Items.Add("DuplicatePolicyData", ViewState["DuplicateData"]);
                            //}

                        }
                        string PC_ProductCode = string.Empty, convertedPolicynumber = string.Empty;
                        string PC_DataSource = string.Empty;
                        string tempPolicyNumber = string.Empty;
                        float enteredBalancePolicy = 0.0F, convertedBalancePolicy = 0.0F;
                        if (Convert.ToInt32(RevenueType) == Convert.ToInt32(((StringDictionary)Application["Constants"])["RevenueCode.DownPayment"]) && !(Convert.ToBoolean(Convert.ToInt16(ConfigurationSettings.AppSettings["AllowRPBS"].ToString()))))
                        {
                            //CHG0110069 - CH4 - BEGIN - Modified/Commented the below logic to remove the Auto Policy condition since it is removed. 
                            if (ProductType.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()) || ProductType.Equals(MSC.SiteTemplate.ProductCodes.PU.ToString()))
                            //CHG0110069 - CH4 - END - Modified/Commented the below logic to remove the Auto Policy condition since it is removed.
                            {
                                if (Policy.Trim().Length == 8)
                                {
                                    Context.Items.Add("DownPaymentData", "1KICO-KIC");
                                    //Proceed to Billing Screen 1KIC0, KIC
                                }
                                else if (ProductType.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()) && Policy.Trim().Length == 7)
                                {
                                    Context.Items.Add("DownPaymentData", "CSIIB-HDES");
                                    //Proceed to Billing Screen CSIIB, HDES
                                }
                                else if (ProductType.Equals(MSC.SiteTemplate.ProductCodes.PU.ToString()) && Policy.Trim().Length == 7)
                                {
                                    Context.Items.Add("DownPaymentData", "CSIIB-PUPSYS");
                                    //Proceed to Billing Screen CSIIB, PUPSYS
                                }
                                else
                                {
                                    lenvld.ErrorMessage = "Payment is not allowed for an invalid policy";
                                    e.IsValid = false;
                                    lenvld.Enabled = true;
                                    lenvld.MarkInvalid();
                                    vldPolicyLength.ErrorMessage = "";
                                }
                            }
                        }
                        else
                        {
                            //MAIG - CH33 - BEGIN - Commented the code that accomodates the PAS Home DF, PU policies in Home Product.
                            //PAS HO CH29- BEGIN : Added the below code to send the Product Code DF, PU for AAA SS Home policies 6/13/2014
                            /* if (ProductType.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()) && iPolLength == 13)
                             {
                                 if (Policy.Substring(2, 2).Equals("PU"))
                                 {
                                     PC_ProductCode = "PU";
                                 }
                                 else if (Policy.Substring(2, 2).Equals("D3"))
                                 {
                                     PC_ProductCode = "DF";
                                 }
                                 else
                                 {

                                     PC_ProductCode = "HO";
                                 }
                             }
                             else
                             {*/
                            //DataRow[] productDataRows = ProductTypes.Select("ID LIKE " + "'" + Productcode + "'");
                            //foreach (DataRow myDataRow in productDataRows)
                            //PC_ProductCode = myDataRow["PaymentCentral_Product_Code"].ToString();
                            PC_ProductCode = ProductType;
                            //}
                            //MAIG - CH33 - END - Commented the code that accomodates the PAS Home DF, PU policies in Home Product.
                            //PAS HO CH29- END : Added the below code to send the Product Code DF, PU for AAA SS Home policies 6/13/2014
                            //MAIG - CH23 - END - Updated the Common Home Product Type instead of AAASS Home and commented Old logic to get the Product Code from DB
                            InsuranceClasses.Service.Insurance I = new InsuranceClasses.Service.Insurance();
                            //DataSet dsIVRPolicyDetails = new DataSet();
                            //MAIG - CH24 - BEGIN - Added code to set the data source for Down payment sceanrio and delete a un-selected row if there are multiple response from RPBS Service
                            //DataTable dsEnteredPolicyDetails = new DataTable();
                            //Added the below line of code in order to send the source system as input parameter request to the billing summary
                            if (Convert.ToInt32(RevenueType) == Convert.ToInt32(((StringDictionary)Application["Constants"])["RevenueCode.DownPayment"]))
                            {
                                if ((ProductType.ToUpper().ToString() == "HO") && (iPolLength == 7))
                                {
                                    PC_DataSource = Config.Setting("DataSource.POESHome");
                                }
                                //CHG0110069 - CH5 - BEGIN - Removed the below logic to remove the Auto Policy condition since it is removed. 
                                else if (((ProductType.ToUpper().ToString() == "HO")) && (iPolLength == 8))
                                //CHG0110069 - CH5 - END - Removed the below logic to remove the Auto Policy condition since it is removed. 
                                {
                                    PC_DataSource = "KIC";
                                }
                                else if (Productcode.ToUpper().ToString() == Config.Setting("ProductCode.PUP") && (iPolLength == 7))
                                {
                                    PC_DataSource = Config.Setting("DataSource.PUP");
                                }
                            }
                            else
                            {
                                //PC_DataSource = DataSource(ProductType, iPolLength);
                                PC_DataSource = string.Empty;
                            }
                            tempPolicyNumber = Policy;
                            if (!string.IsNullOrEmpty(HiddenSelectedDuplicatePolicy.Text) && !(CriteriaParameters.Visible))
                            {
                                if (ViewState["DuplicateRpbsData"] != null)
                                {
                                    dsEnteredPolicyDetails = (DataTable)ViewState["DuplicateRpbsData"];
                                }
                                DataRow[] dr = dsEnteredPolicyDetails.Select("SOURCE_SYSTEM ='" + HiddenSelectedDuplicatePolicy.Text + "'");
                                DataRow newRow = dsEnteredPolicyDetails.NewRow();
                                newRow.ItemArray = dr[0].ItemArray;
                                dsEnteredPolicyDetails.Rows.Remove(dr[0]);
                                dsEnteredPolicyDetails.Rows.InsertAt(newRow, 0);
                                for (int i = dsEnteredPolicyDetails.Rows.Count - 1; i >= 0; i--)
                                {
                                    DataRow row = dsEnteredPolicyDetails.Rows[i];
                                    if (row["SOURCE_SYSTEM"].ToString().ToLower().Equals(HiddenSelectedDuplicatePolicy.Text.ToLower()))
                                    {
                                        continue;
                                    }
                                    else
                                    {
                                        row.Delete();
                                    }
                                }
                            }
                            else
                            {
                                HiddenSelectedDuplicatePolicy.Text = "";
                                dsEnteredPolicyDetails = I.CheckPolicy(Policy, PC_ProductCode, Page.User.Identity.Name, PC_DataSource);
                            }
                            //MAIG - CH24 - END - Added code to set the data source for Down payment sceanrio and delete a un-selected row if there are multiple response from RPBS Service
                            if (dsEnteredPolicyDetails == null)
                            {
                                //To handle the null response from the payment central service in case of service down.
                                //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                //AZ PAS Conversion and PC integration -CH9 - Modified the message as part of defect 365.
                                lenvld.ErrorMessage = Constants.ERR_BILL_LOOKUP_RUNTIME_EXCEPTION;
                                e.IsValid = false;
                                lenvld.Enabled = true;
                                lenvld.MarkInvalid();
                                vldPolicyLength.ErrorMessage = "";
                            }
                            //MAIG - CH25 - BEGIN - Logic added to select only one row from the RPBS Service if it has multiple response
                            else if (dsEnteredPolicyDetails != null && dsEnteredPolicyDetails.Rows.Count > 1)
                            {
                                //Logic to call the SearchResults PopUp window and selct one.
                                _ProductType.Enabled = false;
                                _ProductType.SelectedValue = "0";
                                TextBox tmpPolicy = (TextBox)_Policy.FindControl("_Policy");
                                tmpPolicy.Enabled = false;
                                tmpPolicy.Text = "";
                                PlaceHolder resultsGrid = (PlaceHolder)_NameSearch.FindControl("Results");
                                _NameSearch.Visible = true;
                                lnkCustomerName.Visible = false;
                                pnlCustomerName.Visible = true;
                                CriteriaParameters.Visible = false;
                                resultsGrid.Visible = true;
                                _NameSearch.PopulateRpbsData(dsEnteredPolicyDetails);
                                vldProductType.IsValid = false;
                                Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "AnotherFunction();", true);
                                this.Parent.FindControl("PageValidator2").Visible = false;

                                if (Context.Items["DuplicatePolicyData"] == null)
                                {
                                    string[] selectedArray = HiddenNameSearch.Text.Split('|');
                                    if (selectedArray.Length > 3)
                                    {
                                        Context.Items.Add("DuplicatePolicyData", string.Format("{0}|{1}|{2}|{3}", tempPolicyNumber, PC_ProductCode, PC_DataSource,selectedArray[2]));
                                    }
                                }
                                //ViewState.Add("DuplicateData", string.Format("{0}|{1}|{2}", tempPolicyNumber, PC_ProductCode, PC_DataSource));
                                ViewState.Add("DuplicateRpbsData", dsEnteredPolicyDetails);
                                // CHG0113938 - BEGIN - Added Policy Status Description to Context variable.
                                if (Convert.ToString(dsEnteredPolicyDetails.Rows[0][CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION]).Length > 0)
                                {
                                    if (Context.Items[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION] == null)
                                    {
                                        Context.Items.Add(CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION, Convert.ToString(dsEnteredPolicyDetails.Rows[0][CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION]));
                                    }
                                    else
                                    {
                                        Context.Items[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION] = Convert.ToString(dsEnteredPolicyDetails.Rows[0][CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION]);
                                    }
                                }
                                // CHG0113938 - END - Added Policy Status Description to Context variable.
                            }
                            //MAIG - CH25 - END - Logic added to select only one row from the RPBS Service if it has multiple response

                            else
                            {
                                // AZ PAS Conversion and PC integration: CH11 Added the below code to assign the product code which is obtained from service for Western united products by cognizant on 10/05/2012.
                                //MAIG - CH26 - BEGIN - Commented the Code since the product Type holds the PC Product Type
                                /*if (blnWesternUnitedProduct)
                                {
                                    string strProductCode = dsEnteredPolicyDetails.Rows[0]["POL_TYPE"].ToString().Trim();
                                    string[] strWProductCode = Config.Setting("PCP.Products").Split(',');
                                    foreach (string strproductCode in strWProductCode)
                                    {
                                        DataRow[] WProductCode = ProductTypes.Select("ID LIKE " + "'" + strproductCode + "'");
                                        foreach (DataRow myDataRow in WProductCode)
                                            if (myDataRow["PaymentCentral_Product_Code"].ToString() == strProductCode)
                                            {
                                                ProductType = myDataRow["ID"].ToString();
                                            }
                                    }

                                }
                                //AZ PAS Conversion and PC integration: CH11 Added the below code to assign the product code which is obtained from service for Western united products by cognizant on 10/05/2012.
                                if (Productcode.ToUpper().ToString() == Config.Setting("ProductCode.PUP"))
                                {
                                    dsEnteredPolicyDetails.Rows[0]["POL_NUMBER"] = "PUP" + Policy;
                                    Policy = "PUP" + Policy;
                                }*/
                                //MAIG - CH26 - END - Commented the Code since the product Type holds the PC Product Type
                                //MAIG - CH27 - BEGIN - Added the ToString method in the condition to avoid any unhandled exception
                                if (dsEnteredPolicyDetails.Rows[0]["Converted_Policynumber"] != null || dsEnteredPolicyDetails.Rows[0]["Converted_Policynumber"].ToString() != "")
                                {
                                    convertedPolicynumber = dsEnteredPolicyDetails.Rows[0]["Converted_Policynumber"].ToString();
                                }
                                if (dsEnteredPolicyDetails.Rows[0]["Total_Amount"] != null || dsEnteredPolicyDetails.Rows[0]["Total_Amount"].ToString() != "")
                                {
                                    float.TryParse(dsEnteredPolicyDetails.Rows[0]["Total_Amount"].ToString(), out enteredBalancePolicy);
                                }
                                //AZ PAS Conversion and PC integration CH8-START- Added the below code to assign the first name and last name from the full name

                                if ((ConfigurationSettings.AppSettings["Fullname.SourceSystem2"]).IndexOf(dsEnteredPolicyDetails.Rows[0]["SOURCE_SYSTEM"].ToString()) > -1)
                                {
                                    if (dsEnteredPolicyDetails.Rows[0]["First_Name"].ToString() == "" && dsEnteredPolicyDetails.Rows[0]["INS_FULL_NME"].ToString() != "")
                                    {
                                        string fullName = dsEnteredPolicyDetails.Rows[0]["INS_FULL_NME"].ToString().Replace(',', ' ').Replace('^', ' ');
                                        if (fullName.Contains(" "))
                                        {
                                            dsEnteredPolicyDetails.Rows[0]["First_Name"] = fullName.Substring(0, fullName.IndexOf(' '));
                                        }
                                    }
                                    if (dsEnteredPolicyDetails.Rows[0]["Last_Name"].ToString() == "" && dsEnteredPolicyDetails.Rows[0]["INS_FULL_NME"].ToString() != "")
                                    {
                                        string fullName = dsEnteredPolicyDetails.Rows[0]["INS_FULL_NME"].ToString().Replace(',', ' ').Replace('^', ' ');
                                        if (fullName.Contains(" "))
                                        {
                                            dsEnteredPolicyDetails.Rows[0]["Last_Name"] = fullName.Substring((fullName.IndexOf(' ')), fullName.Length - (fullName.IndexOf(' ')));
                                        }
                                    }
                                }
                                else if ((ConfigurationSettings.AppSettings["Fullname.SourceSystem1"]).IndexOf(dsEnteredPolicyDetails.Rows[0]["SOURCE_SYSTEM"].ToString()) > -1)
                                {
                                    if (dsEnteredPolicyDetails.Rows[0]["Last_Name"].ToString() == "" && dsEnteredPolicyDetails.Rows[0]["INS_FULL_NME"].ToString() != "")
                                    {
                                        string fullName = dsEnteredPolicyDetails.Rows[0]["INS_FULL_NME"].ToString().Replace(',', ' ').Replace('^', ' ');
                                        if (fullName.Contains(" "))
                                        {
                                            dsEnteredPolicyDetails.Rows[0]["Last_Name"] = fullName.Substring(0, fullName.IndexOf(' '));
                                        }
                                    }
                                    if (dsEnteredPolicyDetails.Rows[0]["First_Name"].ToString() == "" && dsEnteredPolicyDetails.Rows[0]["INS_FULL_NME"].ToString() != "")
                                    {
                                        string fullName = dsEnteredPolicyDetails.Rows[0]["INS_FULL_NME"].ToString().Replace(',', ' ').Replace('^', ' ');
                                        if (fullName.Contains(" "))
                                        {
                                            dsEnteredPolicyDetails.Rows[0]["First_Name"] = fullName.Substring((fullName.IndexOf(' ')), fullName.Length - (fullName.IndexOf(' ')));
                                        }
                                    }
                                    //MAIG - CH27 - END - Added the ToString method in the condition to avoid any unhandled exception
                                }
                                //AZ PAS Conversion and PC integration CH8-END- Added the below code to assign the first name and last name from the full name

                                Context.Items.Add("convertedPolicynumber", convertedPolicynumber);
                                Context.Items.Add("balanceEnteredPolicy", enteredBalancePolicy);
                                //dsIVRPolicyDetails = I.CheckActiveIVRPolicy(Policy, Product_Prefix);
                                //PC Phase II changes - CH1 START- Added the below code to add the billing summary details to context for enrollment
                                //MAIG - CH28 - BEGIN - Added condition to null check to avoid any unhandled exception
                                if (Context.Items["PaymentPlan"] != null)
                                {
                                    Context.Items["PaymentPlan"] = dsEnteredPolicyDetails.Rows[0]["PaymentPlan"].ToString();
                                }
                                else
                                {
                                    Context.Items.Add("PaymentPlan", dsEnteredPolicyDetails.Rows[0]["PaymentPlan"].ToString());
                                }
                                if (Context.Items["billplan"] != null)
                                {
                                    Context.Items["billplan"] = dsEnteredPolicyDetails.Rows[0][CSAAWeb.Constants.PC_BillingPlan].ToString();
                                }
                                else
                                {
                                    Context.Items.Add("billplan", dsEnteredPolicyDetails.Rows[0][CSAAWeb.Constants.PC_BillingPlan].ToString());
                                }
                                if (Context.Items["autoPay"] != null)
                                {
                                    Context.Items["autoPay"] = dsEnteredPolicyDetails.Rows[0]["AutoPay"].ToString();
                                }
                                else
                                {
                                    Context.Items.Add("autoPay", dsEnteredPolicyDetails.Rows[0]["AutoPay"].ToString());
                                }

                                // CHG0113938 - BEGIN - Added Policy Status Description to Context variable.
                                if (Convert.ToString(dsEnteredPolicyDetails.Rows[0][CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION]).Length > 0)
                                {
                                    if (Context.Items[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION] == null)
                                    {
                                        Context.Items.Add(CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION, Convert.ToString(dsEnteredPolicyDetails.Rows[0][CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION]));
                                    }
                                    else
                                    {
                                        Context.Items[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION] = Convert.ToString(dsEnteredPolicyDetails.Rows[0][CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION]);
                                    }
                                }
                                // CHG0113938 - END - Added Policy Status Description to Context variable.

                                //MAIG - CH28 - END - Added condition to null check to avoid any unhandled exception
                                //PC Phase II changes - CH1 END- Added the below code to add the billing summary details to context for enrollment
                                //MAIG - CH29 - BEGIN - Modified the Code to change the flow for down/installment payment
                                if (dsEnteredPolicyDetails.Rows.Count != 0)//Testing(!blnWesternUnitedProduct))
                                {
                                    Isactive = Convert.ToInt32(dsEnteredPolicyDetails.Rows[0]["ErrorCode"].ToString());
                                    string errorMessage = dsEnteredPolicyDetails.Rows[0]["Err_MSG"].ToString();
                                    string errorCode = dsEnteredPolicyDetails.Rows[0]["PC_ErrorCode"].ToString();

                                    bool isRestrictedToPay = false;
                                    if (dsEnteredPolicyDetails.Rows[0][CSAAWeb.Constants.PC_BILL_RESTRICTEDTOPAY] != null && dsEnteredPolicyDetails.Rows[0][CSAAWeb.Constants.PC_BILL_RESTRICTEDTOPAY].ToString() != "")
                                    {
                                        isRestrictedToPay = Convert.ToBoolean(dsEnteredPolicyDetails.Rows[0][CSAAWeb.Constants.PC_BILL_RESTRICTEDTOPAY].ToString());
                                    }
                                    //MAIGEnhancement CHG0107527 - CH1 - BEGIN - Modified the below code to allow payments even if the RPBS response IsRestrictedToPay field is true, also this is made configurable
                                    if (Convert.ToBoolean(Config.Setting("SkipPaymentRestriction")) || !isRestrictedToPay)
                                    //MAIGEnhancement CHG0107527 - CH1 - END - Modified the below code to allow payments even if the RPBS response IsRestrictedToPay field is true, also this is made configurable
                                    {
                                        Dictionary<string, string> RpbsDetails = new Dictionary<string, string>();
                                        //This is for Down payment Flow
                                        if (Convert.ToInt32(RevenueType) == Convert.ToInt32(((StringDictionary)Application["Constants"])["RevenueCode.DownPayment"]))
                                        {
                                            RpbsDetails.Add(Constants.PC_BILL_SETUPAUTOPAYREASONCODE, dsEnteredPolicyDetails.Rows[0][Constants.PC_BILL_SETUPAUTOPAYREASONCODE].ToString());
                                            RpbsDetails.Add(Constants.PC_BILL_SETUPAUTOPAYEFFDATE, dsEnteredPolicyDetails.Rows[0][Constants.PC_BILL_SETUPAUTOPAYEFFDATE].ToString());
                                            if (Context.Items["RpbsBillingDetails"] == null)
                                            {
                                                Context.Items.Add("RpbsBillingDetails", RpbsDetails);
                                            }
                                            else
                                            {
                                                Context.Items["RpbsBillingDetails"] = RpbsDetails;
                                            }
                                            if (Isactive == 0)
                                            {
                                                //Validator lenvld = (Validator)_RevenueType.FindControl("vldRevenueType");
                                                lenvld.ErrorMessage = Constants.PCI_REVENUE_INVALID;
                                                e.IsValid = false;
                                                lenvld.Enabled = true;
                                                lenvld.MarkInvalid();
                                                vldPolicyLength.ErrorMessage = "";
                                            }
                                            else if (Isactive == 1)
                                            {
                                                //CHG0110069 - CH6 - BEGIN - Modified the below logic to remove the Auto product Type from the condition since it should not be allowed
                                                if ((ProductType.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()) || ProductType.Equals(MSC.SiteTemplate.ProductCodes.PU.ToString())))
                                                //CHG0110069 - CH6 - END - Modified the below logic to remove the Auto product Type from the condition since it should not be allowed
                                                {
                                                    if (Policy.Trim().Length == 8)
                                                    {
                                                        Context.Items.Add("DownPaymentData", "1KICO-KIC");
                                                        //Proceed to Billing Screen 1KIC0, KIC

                                                    }
                                                    else if (ProductType.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()) && Policy.Trim().Length == 7)
                                                    {
                                                        Context.Items.Add("DownPaymentData", "CSIIB-HDES");
                                                        //Proceed to Billing Screen CSIIB, HDES
                                                    }
                                                    else if (ProductType.Equals(MSC.SiteTemplate.ProductCodes.PU.ToString()) && Policy.Trim().Length == 7)
                                                    {
                                                        Context.Items.Add("DownPaymentData", "CSIIB-PUPSYS");
                                                        //Proceed to Billing Screen CSIIB, PUPSYS
                                                    }
                                                    else
                                                    {
                                                        lenvld.ErrorMessage = errorCode + ": " + errorMessage;
                                                        e.IsValid = false;
                                                        lenvld.Enabled = true;
                                                        lenvld.MarkInvalid();
                                                        vldPolicyLength.ErrorMessage = "";
                                                    }

                                                }
                                                else
                                                {
                                                    lenvld.ErrorMessage = errorCode + ": " + errorMessage;
                                                    e.IsValid = false;
                                                    lenvld.Enabled = true;
                                                    lenvld.MarkInvalid();
                                                    vldPolicyLength.ErrorMessage = "";
                                                }
                                            }
                                        }
                                        else
                                        //This is for Installment Payment Flow
                                        {
                                            if (Isactive == 0)
                                            {
                                                //MAIG - CH34 - BEGIN - Added code to Set the Product Code retrieved from RPBS Service since PAS Home can DF,PU and HO
                                                if (dsEnteredPolicyDetails.Rows[0][Constants.PC_BILL_POL_TYPE] != null)
                                                {
                                                    ProductType = dsEnteredPolicyDetails.Rows[0][Constants.PC_BILL_POL_TYPE].ToString();
                                                }
                                                //MAIG - CH34 - END - Added code to Set the Product Code retrieved from RPBS Service since PAS Home can DF,PU and HO
                                                //CHG0123270 - INC0443986 Fix - Handle Policy number from RBPS response instead of Textbox - Start
                                                if (dsEnteredPolicyDetails.Rows[0][CSAAWeb.Constants.PC_BILL_POL_NUMBER].ToString() != null)
                                                {
                                                    Policy = dsEnteredPolicyDetails.Rows[0][CSAAWeb.Constants.PC_BILL_POL_NUMBER].ToString();
                                                }
                                                //CHG0123270 - INC0443986 Fix -  Handle Policy number from RBPS response instead of Textbox - End 
                                                //Save the RPBS details for passing to Recurring Page only if not enrolled
                                                if (!(dsEnteredPolicyDetails.Rows[0]["PaymentPlan"].ToString().ToUpper().Equals("AUTO")) || !dsEnteredPolicyDetails.Rows[0]["AutoPay"].ToString().ToUpper().Equals("TRUE"))
                                                {
                                                    RpbsDetails.Add(Constants.PC_BILL_STATUS_DESCRIPTION, dsEnteredPolicyDetails.Rows[0][Constants.PC_BILL_STATUS_DESCRIPTION].ToString());
                                                    RpbsDetails.Add(Constants.PC_BillingPlan, dsEnteredPolicyDetails.Rows[0][Constants.PC_BillingPlan].ToString());
                                                    RpbsDetails.Add(Constants.PC_BILL_Due_Amount, dsEnteredPolicyDetails.Rows[0][Constants.PC_BILL_Due_Amount].ToString());
                                                    RpbsDetails.Add(Constants.PC_BILL_Total_Amount, dsEnteredPolicyDetails.Rows[0][Constants.PC_BILL_Total_Amount].ToString());
                                                    RpbsDetails.Add(Constants.PC_BILL_Due_Date, dsEnteredPolicyDetails.Rows[0][Constants.PC_BILL_Due_Date].ToString());
                                                    RpbsDetails.Add(Constants.PC_BILL_TermEffectiveDate, dsEnteredPolicyDetails.Rows[0][Constants.PC_BILL_TermEffectiveDate].ToString());
                                                    RpbsDetails.Add(Constants.PC_TermExpirationDate, dsEnteredPolicyDetails.Rows[0][Constants.PC_TermExpirationDate].ToString());
                                                    RpbsDetails.Add(Constants.PC_BILL_PAYMENT_RESTRICTION, dsEnteredPolicyDetails.Rows[0][Constants.PC_BILL_PAYMENT_RESTRICTION].ToString());
                                                    RpbsDetails.Add(Constants.PC_BILL_SETUPAUTOPAYREASONCODE, dsEnteredPolicyDetails.Rows[0][Constants.PC_BILL_SETUPAUTOPAYREASONCODE].ToString());
                                                    RpbsDetails.Add(Constants.PC_BILL_SETUPAUTOPAYEFFDATE, dsEnteredPolicyDetails.Rows[0][Constants.PC_BILL_SETUPAUTOPAYEFFDATE].ToString());
                                                    RpbsDetails.Add(Constants.PC_BILL_INS_FULL_NAME, dsEnteredPolicyDetails.Rows[0][Constants.PC_BILL_INS_FULL_NAME].ToString());

                                                    if (Context.Items["RpbsBillingDetails"] == null)
                                                    {
                                                        Context.Items.Add("RpbsBillingDetails", RpbsDetails);
                                                    }
                                                    //CHG0109406 - CH1 - BEGIN - Added the below condition to set Context even if it holds the old value of Rpbs Grid details in case of back and forth scenario. 
                                                    else
                                                    {
                                                        Context.Items["RpbsBillingDetails"] = RpbsDetails;
                                                    }
                                                    //CHG0109406 - CH1 - END - Added the below condition to set Context even if it holds the old value of Rpbs Grid details in case of back and forth scenario.  
                                                }
                                                if (convertedPolicynumber != string.Empty)
                                                {

                                                    //Active and converted policy payment processing
                                                    DataTable dsConvertedPolicyDetails = new DataTable();
                                                    //Added the below line of code in order to send the source system as input parameter request to the billing summary
                                                    PC_DataSource = DataSource(ProductType, convertedPolicynumber.Length);
                                                    dsConvertedPolicyDetails = I.CheckPolicy(convertedPolicynumber, PC_ProductCode, Page.User.Identity.Name, PC_DataSource);
                                                    if (dsConvertedPolicyDetails.Rows[0]["Total_Amount"] != null)
                                                    {
                                                        float.TryParse(dsConvertedPolicyDetails.Rows[0]["Total_Amount"].ToString(), out convertedBalancePolicy);
                                                    }

                                                    //store the CompanyCode and Source System
                                                    Context.Items.Add("InstallmentData", dsEnteredPolicyDetails.Rows[0]["COMPANY_ID"].ToString() + "-" + dsEnteredPolicyDetails.Rows[0]["SOURCE_SYSTEM"].ToString());
                                                    if (Policy.Length == 13)
                                                    {
                                                        //Process for policy in conversion which is Exigen generated policy
                                                        if (dsEnteredPolicyDetails.Rows[0]["Status"].ToString() == "A")
                                                        {
                                                            StoreLineItemDetails(dsEnteredPolicyDetails);
                                                        }
                                                        else
                                                        {
                                                            if (convertedBalancePolicy > 0.0F)
                                                            {
                                                                //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                                                e.IsValid = false;
                                                                lenvld.Enabled = true;
                                                                _Policy.PolicyInValid();
                                                                lenvld.MarkInvalid();
                                                                //MAIGEnhancement CHG0107527 - CH8 - BEGIN - Modified the below condition to include the two dots which is the default Error message of the validator that gets displayed for an converted policy which is cancelled.
                                                                if (string.IsNullOrEmpty(vldPolicyLength.ErrorMessage) || vldPolicyLength.ErrorMessage.Equals(".."))
                                                                //MAIGEnhancement CHG0107527 - CH8 - END - Modified the below condition to include the two dots which is the default Error message of the validator that gets displayed for an converted policy which is cancelled.
                                                                {
                                                                    vldPolicyLength.ErrorMessage = CSAAWeb.Constants.ERR_MSG_M003_FIRST.ToString() + "(" + dsEnteredPolicyDetails.Rows[0]["Converted_Policynumber"].ToString() + ")" + CSAAWeb.Constants.ERR_MSG_M003_LAST.ToString();
                                                                }

                                                            }
                                                            else
                                                            {
                                                                if (enteredBalancePolicy > 0.0F)
                                                                {
                                                                    StoreLineItemDetails(dsEnteredPolicyDetails);
                                                                }
                                                                else
                                                                {
                                                                    //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                                                    e.IsValid = false;
                                                                    lenvld.Enabled = true;
                                                                    _Policy.PolicyInValid();
                                                                    lenvld.MarkInvalid();
                                                                    //MAIGEnhancement CHG0107527 - CH9 - BEGIN - Modified the below condition to include the two dots which is the default Error message of the validator that gets displayed for an converted policy which is cancelled.
                                                                    if (string.IsNullOrEmpty(vldPolicyLength.ErrorMessage) || vldPolicyLength.ErrorMessage.Equals(".."))
                                                                    //MAIGEnhancement CHG0107527 - CH9 - END - Modified the below condition to include the two dots which is the default Error message of the validator that gets displayed for an converted policy which is cancelled.
                                                                    {
                                                                        vldPolicyLength.ErrorMessage = CSAAWeb.Constants.ERR_MSG_M002.ToString();
                                                                    }
                                                                }
                                                            }
                                                        }
                                                    }
                                                    else
                                                    {
                                                        //Process for policy in conversion which is not an Exigen generated policy
                                                        if (dsConvertedPolicyDetails.Rows[0]["Status"].ToString() == "A")
                                                        {
                                                            //CHG0072116 -PAS Conversion Fix START- Modified the below code to allow payment for a non- Exigen converted policy which has outstanding balance
                                                            ////If Exigen policy is active
                                                            //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                                            //e.IsValid = false;
                                                            //lenvld.Enabled = true;
                                                            //_Policy.PolicyInValid();
                                                            //lenvld.MarkInvalid();
                                                            //if (vldPolicyLength.ErrorMessage != "")
                                                            //{
                                                            //    vldPolicyLength.ErrorMessage = CSAAWeb.Constants.ERR_MSG_M001.ToString() + "(" + dsEnteredPolicyDetails.Rows[0]["Converted_Policynumber"].ToString() + ")";
                                                            //}
                                                            StoreLineItemDetails(dsEnteredPolicyDetails);
                                                            //CHG0072116 -PAS Conversion Fix END- Modified the below code to allow payment for a non- Exigen converted policy which has outstanding balance
                                                        }
                                                        else
                                                        {
                                                            //If Exigen policy is not active
                                                            if (enteredBalancePolicy > 0.0F)
                                                            {
                                                                StoreLineItemDetails(dsEnteredPolicyDetails);
                                                            }
                                                            else
                                                            {
                                                                if (convertedBalancePolicy > 0.0F)
                                                                {
                                                                    //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                                                    e.IsValid = false;
                                                                    lenvld.Enabled = true;
                                                                    _Policy.PolicyInValid();
                                                                    lenvld.MarkInvalid();
                                                                    if (vldPolicyLength.ErrorMessage != "")
                                                                    {
                                                                        vldPolicyLength.ErrorMessage = CSAAWeb.Constants.ERR_MSG_M001.ToString() + "(" + dsEnteredPolicyDetails.Rows[0]["Converted_Policynumber"].ToString() + ")";
                                                                    }
                                                                }
                                                                else
                                                                {
                                                                    if (dsEnteredPolicyDetails.Rows[0]["Status"].ToString() == "A")
                                                                    {
                                                                        StoreLineItemDetails(dsEnteredPolicyDetails);
                                                                    }
                                                                    else
                                                                    {
                                                                        //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                                                        e.IsValid = false;
                                                                        lenvld.Enabled = true;
                                                                        _Policy.PolicyInValid();
                                                                        lenvld.MarkInvalid();
                                                                        if (vldPolicyLength.ErrorMessage != "")
                                                                        {
                                                                            vldPolicyLength.ErrorMessage = CSAAWeb.Constants.ERR_MSG_M002.ToString();
                                                                        }
                                                                    }
                                                                }

                                                            }
                                                        }

                                                    }
                                                }
                                                else
                                                {
                                                    //store the CompanyCode and Source System
                                                    Context.Items.Add("InstallmentData", dsEnteredPolicyDetails.Rows[0]["COMPANY_ID"].ToString() + "-" + dsEnteredPolicyDetails.Rows[0]["SOURCE_SYSTEM"].ToString());
                                                    StoreLineItemDetails(dsEnteredPolicyDetails);
                                                }
                                                //CHG0116140 - Payment Restriction - BEGIN - Added Payment Restriction to RPBSBillingDetails in the context 
                                                if (!RpbsDetails.ContainsKey(Constants.PC_BILL_PAYMENT_RESTRICTION))
                                                    RpbsDetails.Add(Constants.PC_BILL_PAYMENT_RESTRICTION, dsEnteredPolicyDetails.Rows[0][Constants.PC_BILL_PAYMENT_RESTRICTION].ToString());
                                                if (Context.Items["RpbsBillingDetails"] == null)
                                                    Context.Items.Add("RpbsBillingDetails", RpbsDetails);
                                                else
                                                    Context.Items["RpbsBillingDetails"] = RpbsDetails;
                                                //CHG0116140 - Payment Restriction - END - Added Payment Restriction to RPBSBillingDetails in the context 
                                            }
                                            else
                                            {
                                                //CHG0118686 - PRB0045696 - Message Changes - Start - FR1,FR2//
                                                lenvld.ErrorMessage = errorMessage + " (" + errorCode + ")";
                                                //CHG0118686 - PRB0045696 - Message Changes - End - FR1,FR2//
                                                e.IsValid = false;
                                                lenvld.Enabled = true;
                                                lenvld.MarkInvalid();
                                                vldPolicyLength.ErrorMessage = "";
                                            }
                                        }
                                    }
                                    else
                                    {
                                        lenvld.ErrorMessage = "Payment is restricted for the entered Policy";
                                        e.IsValid = false;
                                        lenvld.Enabled = true;
                                        lenvld.MarkInvalid();
                                        vldPolicyLength.ErrorMessage = "";
                                    }
                                }


                                /*  if (Convert.ToInt32(RevenueType) != Convert.ToInt32(((StringDictionary)Application["Constants"])["RevenueCode.DownPayment"]))
                                  {
                                      //Revenue Type = Installment Payment
                                      if (iPolLength == 7)
                                      {
                                          //Product validation Required only for Legacy Policy

                                          confirmMessage = (dsEnteredPolicyDetails.Rows[0]["ErrorDescription"].ToString());
                                          confirmMessage1 = Check_Legacy_Policy_Product_Validation();
                                          if (confirmMessage1.ToString().Trim() != "")
                                          {
                                              IsOverride = 1;
                                              blnValidIVR = true;
                                          }
                                          else
                                          {
                                              IsOverride = 0;
                                              blnValidIVR = false;
                                          }

                                      }
                                      //PAS HO CH5 - START : Added the below code to restrict payment if not available in BillingLookup 6/13/2014
                                      else if (ProductType == ((StringDictionary)Application["Constants"])["ProductType.SSHome"])
                                      {
                                          errorMessage = dsEnteredPolicyDetails.Rows[0]["Err_MSG"].ToString();
                                          errorCode = dsEnteredPolicyDetails.Rows[0]["PC_ErrorCode"].ToString();
                                          if (!string.IsNullOrEmpty(errorMessage))
                                          {
                                              //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                              lenvld.ErrorMessage = errorCode + ": " + errorMessage;
                                              e.IsValid = false;
                                              lenvld.Enabled = true;
                                              lenvld.MarkInvalid();
                                              vldPolicyLength.ErrorMessage = "";
                                          }
                                      }
                                      //PAS HO CH5 - END : Added the below code to restrict payment if not available in BillingLookup 6/13/2014
                                      else if (ProductType != Config.Setting("ProductCode.HighLimit"))
                                      {
                                          confirmMessage = (dsEnteredPolicyDetails.Rows[0]["ErrorDescription"].ToString());
                                      }
                                  }
                                  else
                                  {
                                      //Revenue Type = Down Payment
                                      if (iPolLength == 7)
                                      {
                                          //Product validation Required only for Legacy Policy
                                          Invalid_Message = Check_Legacy_Policy_Product_Validation();
                                          if (Invalid_Message.Trim() == "")
                                          {
                                              //Valid Product Type. No messages to be displayed
                                              confirmMessage = "";
                                          }
                                          else
                                          {
                                              //Invalid Product Type. 
                                              confirmMessage = Invalid_Message;
                                          }
                                      }
                                      else
                                      {
                                          confirmMessage = "";
                                      }
                                  }

                                  if (confirmMessage != "")
                                  {
                                      String sResp = "<SCRIPT type='" + "text/javascript" + "'> var Confirm_Box=confirm('" + confirmMessage + "');if (Confirm_Box==true && " + blnValidIVR.ToString().ToLower() + " == true ){if(confirm('" + confirmMessage1 + "') == false){history.go(-1);} ;}if (Confirm_Box==false){history.go(-1);}</SCRIPT>";
                                      Response.Write(sResp);
                                  }
                                  else
                                  {
                                      //No messages to be displayed. Proceed to next screen.
                                  }
                            
                              }
                          }

                          if (blnWesternUnitedProduct)
                          {
                              //Check for Numeric and between 4 to 9 digits.
                              Invalid_Message = Check_WU_Policy();
                              if (!string.IsNullOrEmpty(Invalid_Message))
                              {
                                  ValidIIBProd = true;
                                  //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                  lenvld.ErrorMessage = Invalid_Message;
                                  e.IsValid = false;
                                  lenvld.Enabled = true;
                                  _Policy.PolicyInValid();
                                  lenvld.MarkInvalid();
                                  vldPolicyLength.ErrorMessage = "";
                              }
                              else
                              {

                                  //InsuranceClasses.Service.Insurance I = new InsuranceClasses.Service.Insurance();
                                  //DataTable dtActiveSISPolicy = new DataTable();
                                  string strSISErrorMessage = string.Empty;
                                  //dtActiveSISPolicy = I.CheckActiveSISPolicy(Policy);
                                  if (dsEnteredPolicyDetails.Rows.Count == 0)
                                  {
                                      //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                      lenvld.ErrorMessage = Constants.PCI_SIS_LOOKUP_FAILURE;
                                      e.IsValid = false;
                                      lenvld.Enabled = true;
                                      _Policy.PolicyInValid();
                                      lenvld.MarkInvalid();
                                      vldPolicyLength.ErrorMessage = "";
                                  }
                                  else if (dsEnteredPolicyDetails.Rows[0]["ERR_MSG"].ToString().Trim() != "")
                                  {

                                      strSISErrorMessage = dsEnteredPolicyDetails.Rows[0]["ERR_MSG"].ToString().Trim();
                                      //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                      lenvld.ErrorMessage = strSISErrorMessage.Trim();
                                      e.IsValid = false;
                                      lenvld.Enabled = true;
                                      _Policy.PolicyInValid();
                                      lenvld.MarkInvalid();
                                      vldPolicyLength.ErrorMessage = "";
                                  }
                                  else if (dsEnteredPolicyDetails.Rows[0]["POL_STATE"].ToString().Trim() != "" && dsEnteredPolicyDetails.Rows[0]["POL_PREFIX"].ToString().Trim() != "" && dsEnteredPolicyDetails.Rows[0]["POL_GROUP"].ToString().Trim() != "")
                                  {

                                      strSISErrorMessage = dsEnteredPolicyDetails.Rows[0]["ERR_MSG"].ToString().Trim();
                                      if (strSISErrorMessage.Trim() != "")
                                      {
                                          //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                          lenvld.ErrorMessage = strSISErrorMessage;
                                          e.IsValid = false;
                                          lenvld.Enabled = true;
                                          _Policy.PolicyInValid();
                                          lenvld.MarkInvalid();
                                          vldPolicyLength.ErrorMessage = "";
                                      }


                                  }
                                  else if (dsEnteredPolicyDetails.Rows[0]["SOURCE_SYSTEM"].ToString() != "SIS")
                                  {
                                      strSISErrorMessage = Constants.PCI_INVALID_POLICY_PRODUCT;
                                      //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                      lenvld.ErrorMessage = strSISErrorMessage.Trim();
                                      e.IsValid = false;
                                      lenvld.Enabled = true;
                                      _Policy.PolicyInValid();
                                      lenvld.MarkInvalid();
                                      vldPolicyLength.ErrorMessage = "";
                                  }

                              }
                          }
                          //AZ PAS conversion and PC integration -CH1 END -Modified the below code to lookup the policy details from the billing summary lookup
                          //AZ PAS conversion and PC integration -CH2 START - Added the below code to check active status of Highlimit policy else error message will be displayed

                          if (ProductType == Config.Setting("ProductCode.HighLimit") && Isactive == 1 && iPolLength == 10)
                          {
                              ValidIIBProd = true;
                              //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                              lenvld.ErrorMessage = dsEnteredPolicyDetails.Rows[0]["ERR_MSG"].ToString().Trim();
                              e.IsValid = false;
                              lenvld.Enabled = true;
                              _Policy.PolicyInValid();
                              lenvld.MarkInvalid();
                              vldPolicyLength.ErrorMessage = "";
                          }
                          //AZ PAS conversion and PC integration -CH2 END - Added the below code to check active status of Highlimit policy else error message will be displayed
                          //AZ PAS conversion and PC integration -CH2 START - Added the below code to validate the policy for AZ PAS conversion requirement
                          else if ((!ValidIIBProd) && (iPolLength != 0) && (!blnWesternUnitedProduct) && (Convert.ToInt32(RevenueType) == Convert.ToInt32(((StringDictionary)Application["Constants"])["RevenueCode.DownPayment"])))
                          {
                              //Down Payment Policy Payment processing
                              StoreLineItemDetails(dsEnteredPolicyDetails);
                          }

                          else if (Isactive == 0 && convertedPolicynumber != string.Empty && confirmMessage == "")
                          {
                              //Active and converted policy payment processing
                              DataTable dsConvertedPolicyDetails = new DataTable();
                              //Added the below line of code in order to send the source system as input parameter request to the billing summary
                              PC_DataSource = DataSource(ProductType, convertedPolicynumber.Length);
                              dsConvertedPolicyDetails = I.CheckPolicy(convertedPolicynumber, PC_ProductCode, Page.User.Identity.Name, PC_DataSource);
                              if (dsConvertedPolicyDetails.Rows[0]["Total_Amount"] != null)
                              {
                                  float.TryParse(dsConvertedPolicyDetails.Rows[0]["Total_Amount"].ToString(), out convertedBalancePolicy);
                              }


                              if (Policy.Length == 13)
                              {
                                  //Process for policy in conversion which is Exigen generated policy
                                  if (dsEnteredPolicyDetails.Rows[0]["Status"].ToString() == "A")
                                  {
                                      StoreLineItemDetails(dsEnteredPolicyDetails);
                                  }
                                  else
                                  {
                                      if (convertedBalancePolicy > 0.0F)
                                      {
                                          //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                          e.IsValid = false;
                                          lenvld.Enabled = true;
                                          _Policy.PolicyInValid();
                                          lenvld.MarkInvalid();
                                          if (vldPolicyLength.ErrorMessage == "")
                                          {
                                              vldPolicyLength.ErrorMessage = CSAAWeb.Constants.ERR_MSG_M003_FIRST.ToString() + "(" + dsEnteredPolicyDetails.Rows[0]["Converted_Policynumber"].ToString() + ")" + CSAAWeb.Constants.ERR_MSG_M003_LAST.ToString();
                                          }

                                      }
                                      else
                                      {
                                          if (enteredBalancePolicy > 0.0F)
                                          {
                                              StoreLineItemDetails(dsEnteredPolicyDetails);
                                          }
                                          else
                                          {
                                              //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                              e.IsValid = false;
                                              lenvld.Enabled = true;
                                              _Policy.PolicyInValid();
                                              lenvld.MarkInvalid();
                                              if (vldPolicyLength.ErrorMessage == "")
                                              {
                                                  vldPolicyLength.ErrorMessage = CSAAWeb.Constants.ERR_MSG_M002.ToString();
                                              }
                                          }
                                      }
                                  }
                              }
                              else
                              {
                                  //Process for policy in conversion which is not an Exigen generated policy
                                  if (dsConvertedPolicyDetails.Rows[0]["Status"].ToString() == "A")
                                  {
                                      //CHG0072116 -PAS Conversion Fix START- Modified the below code to allow payment for a non- Exigen converted policy which has outstanding balance
                                      ////If Exigen policy is active
                                      //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                      //e.IsValid = false;
                                      //lenvld.Enabled = true;
                                      //_Policy.PolicyInValid();
                                      //lenvld.MarkInvalid();
                                      //if (vldPolicyLength.ErrorMessage != "")
                                      //{
                                      //    vldPolicyLength.ErrorMessage = CSAAWeb.Constants.ERR_MSG_M001.ToString() + "(" + dsEnteredPolicyDetails.Rows[0]["Converted_Policynumber"].ToString() + ")";
                                      //}
                                      StoreLineItemDetails(dsEnteredPolicyDetails);
                                      //CHG0072116 -PAS Conversion Fix END- Modified the below code to allow payment for a non- Exigen converted policy which has outstanding balance
                                  }
                                  else
                                  {
                                      //If Exigen policy is not active
                                      if (enteredBalancePolicy > 0.0F)
                                      {
                                          StoreLineItemDetails(dsEnteredPolicyDetails);
                                      }
                                      else
                                      {
                                          if (convertedBalancePolicy > 0.0F)
                                          {
                                              //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                              e.IsValid = false;
                                              lenvld.Enabled = true;
                                              _Policy.PolicyInValid();
                                              lenvld.MarkInvalid();
                                              if (vldPolicyLength.ErrorMessage != "")
                                              {
                                                  vldPolicyLength.ErrorMessage = CSAAWeb.Constants.ERR_MSG_M001.ToString() + "(" + dsEnteredPolicyDetails.Rows[0]["Converted_Policynumber"].ToString() + ")";
                                              }
                                          }
                                          else
                                          {
                                              if (dsEnteredPolicyDetails.Rows[0]["Status"].ToString() == "A")
                                              {
                                                  StoreLineItemDetails(dsEnteredPolicyDetails);
                                              }
                                              else
                                              {
                                                  //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                                                  e.IsValid = false;
                                                  lenvld.Enabled = true;
                                                  _Policy.PolicyInValid();
                                                  lenvld.MarkInvalid();
                                                  if (vldPolicyLength.ErrorMessage != "")
                                                  {
                                                      vldPolicyLength.ErrorMessage = CSAAWeb.Constants.ERR_MSG_M002.ToString();
                                                  }
                                              }
                                          }

                                      }
                                  }

                              }
                          }
                            
                          else if (Isactive == 0 && convertedPolicynumber == string.Empty && confirmMessage == "")
                          {
                              //Active Non converted Policy Payment processing
                              StoreLineItemDetails(dsEnteredPolicyDetails);
                          }
                          else if (Isactive == 1 && confirmMessage != "" && convertedPolicynumber == string.Empty && (!ValidIIBProd) && ProductType != Config.Setting("ProductCode.HighLimit"))
                          {
                              ////InActive and valid Non converted Policy Payment processing
                              StoreLineItemDetails(dsEnteredPolicyDetails);
                          }
                      }*/
                                //MAIG - CH29 - END - Modified the Code to change the flow for down/installment payment

                                //if ((ValidIIBProd) && (iPolLength != 0) && (!blnWesternUnitedProduct)&&(Isactive == 0) && (Convert.ToInt32(RevenueType) != Convert.ToInt32(((StringDictionary)Application["Constants"])["RevenueCode.DownPayment"])))
                                //       {
                                //           //Call the StoreLineItemDetails Method to store the data returned from DB.
                                //           StoreLineItemDetails(dsEnteredPolicyDetails);
                                //       }
                                //else if (SIS_Policy_Product_Validation(dsEnteredPolicyDetails.Rows[0]["POL_GROUP"].ToString().Trim()) == true)
                                //    {
                                //        //Call the StoreLineItemDetails Method to store the data returned from DB.
                                //        //Matching Product Code
                                //        IsOverride = 0;
                                //        StoreLineItemDetails(dsEnteredPolicyDetails);
                                //    }
                                //    else
                                //    {
                                //        //Product Code not matching
                                //        //Override the Product Code
                                //        //ProductTypeNew field will be set with this new Product type.
                                //        //ProductType in Lines [0] will be set on Load of Billing screen.
                                //        ProductTypeNew = SISPolicyMapper(dsEnteredPolicyDetails.Rows[0]["POL_GROUP"].ToString().Trim());
                                //        IsOverride = 0;
                                //        StoreLineItemDetails(dsEnteredPolicyDetails);

                                //    }
                                //AZ PAS conversion and PC integration -CH3 END - Added the below code to valdate the policy for AZ PAS conversion requirement
                            }
                        }
                    }
                    //required field validations
                    //			67811A0  - PCI Remediation for Payment systems - Added Trim condition to the policy number field.
                    //MAIG - CH30 - BEGIN - Added logic to support the Name Search functionality for Installment Payment
                }
                else if (pnlCustomerName.Visible == true)
                {
                    string errMessage = string.Empty;
                    PlaceHolder resultsGrid = (PlaceHolder)_NameSearch.FindControl("Results");
                    vldProductType.Enabled = true;
                    vldProductType.IsValid = false;
                    lenvld.IsValid = true;
                    resultsGrid.Visible = false;
                    #region Commented block - Cleanup
                    /*if (string.IsNullOrEmpty(txtLastName.Text) || string.IsNullOrEmpty(txtMailingZip.Text))
                   {
                       vldProductType.Enabled = true;
                       vldProductType.IsValid = false;
                       resultsGrid.Visible = false;
                       lenvld.IsValid = true;
                       //vldProductType.ErrorMessage = "Please complete or correct the requested information in the field(s) highlighted in red above.";
                       //_NameSearch.PromptError();
                       if (string.IsNullOrEmpty(txtLastName.Text))
                       {
                           errMessage = Constants.NAME_SEARCH_LAST_NAME_REQ + Constants.NAME_SEARCH_NEW_LINE;
                           lblLastName.ForeColor = System.Drawing.Color.Red;
                       }
                       if (string.IsNullOrEmpty(txtMailingZip.Text))
                       {
                           errMessage = errMessage + Constants.NAME_SEARCH_MAILING_ZIP_REQ + Constants.NAME_SEARCH_NEW_LINE;
                           lblMailingZip.ForeColor = System.Drawing.Color.Red;
                       }
                       Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "AnotherFunction();", true);
                      // lbl.Attributes.Add("style", "color:red");
                       //lblProductType.Attributes.Add("style", "color:black");//ForeColor = System.Drawing.Color.Black;
                       //lblProductType.ForeColor = System.Drawing.Color.Black;
                   }
                   if (!string.IsNullOrEmpty(txtLastName.Text) || !string.IsNullOrEmpty(txtMailingZip.Text) || !string.IsNullOrEmpty(txtFirstName.Text))
                   {
                       vldProductType.Enabled = true;
                       vldProductType.IsValid = false;

                       lenvld.IsValid = true;
                       //if (!CSAAWeb.Validate.isValidLastName(txtLastName.Text) || !CSAAWeb.Validate.IsValidZip(txtMailingZip.Text))
                       //{
                           resultsGrid.Visible = false;
                           //vldProductType.ErrorMessage = "Please complete or correct the requested information in the field(s) highlighted in red above.";

                           if (regCheckName.IsMatch(txtFirstName.Text))
                           {
                               lblFirstName.ForeColor = System.Drawing.Color.Red;
                               errMessage = "Invalid First Name" + Constants.NAME_SEARCH_NEW_LINE;
                           }
                           if (regCheckName.IsMatch(txtLastName.Text))
                           {
                               lblLastName.ForeColor = System.Drawing.Color.Red;
                               errMessage = "Invalid Last Name" + Constants.NAME_SEARCH_NEW_LINE;
                           }
                       
                           if (!string.IsNullOrEmpty(txtLastName.Text) && txtLastName.Text.Length<3)
                           {
                               errMessage = errMessage + Constants.NAME_SEARCH_LAST_NAME_MIM_LENGTH + Constants.NAME_SEARCH_NEW_LINE;
                               lblLastName.ForeColor = System.Drawing.Color.Red;
                           }
                           if (!string.IsNullOrEmpty(txtLastName.Text) && txtLastName.Text[0]=='*')
                           {
                               errMessage = errMessage + Constants.NAME_SEARCH_LAST_NAME_START_CHAR + Constants.NAME_SEARCH_NEW_LINE;
                               lblLastName.ForeColor = System.Drawing.Color.Red;
                           }

                           if (!string.IsNullOrEmpty(txtFirstName.Text) && txtFirstName.Text.Length < 3)
                           {
                               errMessage = errMessage + Constants.NAME_SEARCH_FIRST_NAME_MIM_LENGTH + Constants.NAME_SEARCH_NEW_LINE;
                               lblFirstName.ForeColor = System.Drawing.Color.Red;
                           }
                           if (!string.IsNullOrEmpty(txtFirstName.Text) && txtFirstName.Text[0] == '*')
                           {
                               errMessage = errMessage + Constants.NAME_SEARCH_FIRST_NAME_START_CHAR + Constants.NAME_SEARCH_NEW_LINE;
                               lblFirstName.ForeColor = System.Drawing.Color.Red;
                           }

                           if (!string.IsNullOrEmpty(txtMailingZip.Text) && !CSAAWeb.Validate.IsValidZip(txtMailingZip.Text))
                           {
                               errMessage = errMessage + Constants.NAME_SEARCH_MAILING_ZIP_INVALID+ Constants.NAME_SEARCH_NEW_LINE;
                               lblMailingZip.ForeColor = System.Drawing.Color.Red;
                           }
                           Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "AnotherFunction();", true);
                       //}
                           
                   }*/
                    #endregion
                    if (!string.IsNullOrEmpty(txtFirstName.Text.Trim()))
                    {
                        if (regCheckName.IsMatch(txtFirstName.Text))
                        {
                            errMessage = "Invalid First Name." + Constants.NAME_SEARCH_NEW_LINE;
                            lblFirstName.ForeColor = System.Drawing.Color.Red;
                        }
                        if (txtFirstName.Text.Length < 3)
                        {
                            errMessage = errMessage + Constants.NAME_SEARCH_FIRST_NAME_MIM_LENGTH + Constants.NAME_SEARCH_NEW_LINE;
                            lblFirstName.ForeColor = System.Drawing.Color.Red;
                        }
                        if (txtFirstName.Text.Trim().StartsWith("*"))
                        {
                            errMessage = errMessage + Constants.NAME_SEARCH_FIRST_NAME_START_CHAR + Constants.NAME_SEARCH_NEW_LINE;
                            lblFirstName.ForeColor = System.Drawing.Color.Red;
                        }
                    }

                    if (!string.IsNullOrEmpty(txtLastName.Text.Trim()))
                    {
                        if (regCheckName.IsMatch(txtLastName.Text))
                        {
                            errMessage = errMessage + "Invalid Last Name." + Constants.NAME_SEARCH_NEW_LINE;
                            lblLastName.ForeColor = System.Drawing.Color.Red;
                        }
                        if (txtLastName.Text.Trim().Length < 3)
                        {
                            errMessage = errMessage + Constants.NAME_SEARCH_LAST_NAME_MIM_LENGTH + Constants.NAME_SEARCH_NEW_LINE;
                            lblLastName.ForeColor = System.Drawing.Color.Red;
                        }
                        if (txtLastName.Text.Trim().StartsWith("*"))
                        {
                            errMessage = errMessage + Constants.NAME_SEARCH_LAST_NAME_START_CHAR + Constants.NAME_SEARCH_NEW_LINE;
                            lblLastName.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        errMessage = errMessage + Constants.NAME_SEARCH_LAST_NAME_REQ + Constants.NAME_SEARCH_NEW_LINE;
                        lblLastName.ForeColor = System.Drawing.Color.Red;
                    }

                    if (!string.IsNullOrEmpty(txtMailingZip.Text.Trim()))
                    {
                        if (!CSAAWeb.Validate.IsValidZip(txtMailingZip.Text))
                        {
                            errMessage = errMessage + Constants.NAME_SEARCH_MAILING_ZIP_INVALID + Constants.NAME_SEARCH_NEW_LINE;
                            lblMailingZip.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    else
                    {
                        errMessage = errMessage + Constants.NAME_SEARCH_MAILING_ZIP_REQ + Constants.NAME_SEARCH_NEW_LINE;
                        lblMailingZip.ForeColor = System.Drawing.Color.Red;
                    }

                    Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "AnotherFunction();", true);
                    if (errMessage.Length > 0)
                    {
                        vldProductType.ErrorMessage = errMessage + Constants.NAME_SEARCH_ERR_MESSAGE + Constants.NAME_SEARCH_NEW_LINE;
                    }
                    else
                    {
                        _Policy.Text = string.Empty;
                        this.Parent.FindControl("PageValidator2").Visible = false;
                        Page.ClientScript.RegisterStartupScript(this.GetType(), "myScript", "AnotherFunction();", true);
                        _NameSearch.PopulateGrid();
                    }

                }
                else if (!string.IsNullOrEmpty(SkipParentValidation.Value) && Convert.ToBoolean(SkipParentValidation.Value))
                {
                    this.Parent.FindControl("PageValidator2").Visible = false;
                }
                else if ((!string.IsNullOrEmpty(HidddenDueDetails.Text)) && (HidddenPolicyValidate.Text == tmpPolicyDetails) && (Convert.ToInt32(RevenueType) != Convert.ToInt32(((StringDictionary)Application["Constants"])["RevenueCode.DownPayment"])))
                {
                    StoreLineItemDetails(HidddenDueDetails.Text);
                }
                //MAIG - CH30 - END - Added logic to support the Name Search functionality for Installment Payment
                else if ((_RevenueType.SelectedIndex == 0) || (_RevenueType.SelectedIndex == 0) || (_Policy.Text.Trim() == ""))
                {
                    if (_RevenueType.SelectedIndex == 0)
                    {
                        vldRevenueType.Enabled = true;

                        vldRevenueType.MarkInvalid();
                        vldRevenueType.ErrorMessage = "";
                    }
                    if (_ProductType.SelectedIndex == 0)
                    {
                        vldProductType.Enabled = true;

                        vldProductType.MarkInvalid();
                        vldProductType.ErrorMessage = "";
                    }
                    //			67811A0  - PCI Remediation for Payment systems - Added Trim condition to the policy number field.
                    if (_Policy.Text.Trim() == "")
                    {
                        //Validator lenvld = (Validator)_Policy.FindControl("LabelValidator");
                        lenvld.Enabled = true;
                        lenvld.MarkInvalid();
                        vldPolicyLength.ErrorMessage = "";
                    }

                }
            }
            //MAIG - CH36 - BEGIN - Added logic to pass the Name Search grid data and Duplicate Policy data if the same policy is selected again
            else
            {
                HtmlTable CriteriaParameters = (HtmlTable)_NameSearch.FindControl("table1");
                Panel pnlNameSearch = (Panel)_NameSearch.FindControl("pnlCustomerNameMain");
                TextBox custFirstName = (TextBox)_NameSearch.FindControl("_FirstName");
                TextBox custLastName = (TextBox)_NameSearch.FindControl("_LastName");
                TextBox custMailingZip = (TextBox)_NameSearch.FindControl("_MailingZip");
                if (pnlNameSearch.Visible && CriteriaParameters.Visible)
                {
                    string[] selectedRowArray = HiddenNameSearch.Text.Split('|');
                    if (Context.Items["NameSearchData"] != null)
                    {
                        Context.Items["NameSearchData"] = string.Format("{0}|{1}|{2}|{3}|{4}", custFirstName.Text, custLastName.Text, custMailingZip.Text, selectedRowArray[2], selectedRowArray[3]);
                    }
                    else { Context.Items.Add("NameSearchData", string.Format("{0}|{1}|{2}|{3}|{4}", custFirstName.Text, custLastName.Text, custMailingZip.Text, selectedRowArray[2], selectedRowArray[3])); }
                }
                else if (pnlNameSearch.Visible && !CriteriaParameters.Visible)
                {
                    string[] selectedRowTempArray = HiddenNameSearch.Text.Split('|');
                    if (Context.Items["DuplicatePolicyData"] == null)
                    {
                        Context.Items.Add("DuplicatePolicyData", string.Format("{0}|{1}|{2}|{3}", Policy, _ProductType.SelectedValue, string.Empty,selectedRowTempArray[2]));
                    }
                }
            }
            //MAIG - CH36 - END - Added logic to pass the Name Search grid data and Duplicate Policy data if the same policy is selected again

        }

        #endregion

    }
}

