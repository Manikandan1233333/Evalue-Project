/*
 CHG0121437 - Added new class for ACH Enrollment Pilot changes
 * Added code for retrieving the Policy details from RPBS and displaying the Policy status along with the Enrollment status 
 * and Eligibility status of the Policy
 * Recent payments performed on the policy will be fetched from PC
 * Added code for capturing the User actions in the screen and saving the details to the Payments Database
 * CHG0123980 - ACH Incentive Enhancement - April 2016
 * CHG0129017 - Removal of Linked server - Sep 2016
 */
using CSAAWeb;
using CSAAWeb.AppLogger;
using CSAAWeb.WebControls;
using InsuranceClasses;
using OrderClasses.Service;
using OrderClassesII;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace MSC.Forms
{
    /// <summary>
    /// This page will be loaded which captures the Policy Enrollment details
    /// and determines the Eligibility status of the Policy
    /// </summary>
    public partial class ACHEnrollmentPilot : SiteTemplate
    {
        private SqlConnection oConn;
        private Boolean _isValid = true;
        private static DataTable ProductTypes = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                enrollDetails.Visible = false;
                lblErrorMsg.Visible = false;
                if (!Page.IsPostBack)
                {
                    _PolicyNumber.Enabled = false;
                    if (HttpContext.Current.User.Identity.Name != null)
                    {
                        ViewState["Userid"] = HttpContext.Current.User.Identity.Name;
                    }
                    else
                    {
                        ViewState["Userid"] = string.Empty;
                    }
                    LoadProductTypes();
                    if (ProductTypes.Rows.Count > 1) AddSelectItem(ProductTypes, CSAAWeb.Constants.PC_SEL_PROD);

                    _ProductType.DataSource = ProductTypes;
                    _ProductType.DataBind();

                }
            }
            catch (Exception ex)
            {

                enrollDetails.Visible = false;
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                Logger.Log(ex.ToString());
                Logger.Log(lblErrorMsg.Text.ToString());
                _PolicyNumber.Enabled = true;
                _ProductType.Enabled = true;
            }
        }

        /// <summary>
        /// Search the Enrollment and Eligibility details of the policy
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            //CHG0123980 - ACH Incentive Enhancement - April 2016 - Start - Disable button
            btnSearch.Enabled = false;
            //CHG0123980 - ACH Incentive Enhancement - April 2016 - End - Disable button
            try
            {
                ACHInfo achIn = new ACHInfo();
                achIn.Userid = ViewState["Userid"].ToString();

                achIn.Policynumber = _PolicyNumber.Text.ToUpper().Trim();

                if (_ProductType.SelectedIndex == 0 || _PolicyNumber.Text.Trim().Equals(string.Empty))
                {
                    vldSumaary.Visible = true;
                    _isValid = false;

                }
                else if (achIn.Policynumber.Length < 5)
                {
                    vldSumaary.Visible = false;
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = Constants.PC_INVALID_POLICY;
                    enrollDetails.Visible = false;
                    _isValid = false;

                }
                if (_isValid)
                {


                    string ProductType, PC_ProductCode = string.Empty;
                    ProductType = _ProductType.SelectedItem.Value;
                    if (ProductTypes == null)
                    {
                        LoadProductTypes();
                    }
                    DataRow[] productDataRows = ProductTypes.Select("ID LIKE " + "'" + ProductType + "'");
                    foreach (DataRow myDataRow in productDataRows)
                    {
                        PC_ProductCode = myDataRow[CSAAWeb.Constants.PaymentCentral_Product_Code_table].ToString();
                    }

                    if (ValidPolicyProduct(achIn.Policynumber, ProductType))
                    {
                        _PolicyNumber.Enabled = false;
                        _ProductType.Enabled = false;
                        enrollDetails.Visible = true;
                        //Read the source system
                        IssueDirectPaymentWrapper ObjIssueDirectService = new IssueDirectPaymentWrapper();
                        achIn.SourceSystem = ObjIssueDirectService.DataSource(ProductType, achIn.Policynumber.Length);

                        //RBPS hit
                        BillingLookUp billingSummary = new BillingLookUp();
                        DataTable dtRBPSResponse = billingSummary.checkPolicy(achIn.Policynumber, PC_ProductCode, achIn.Userid, achIn.SourceSystem);


                        if (dtRBPSResponse != null && dtRBPSResponse.Rows.Count > 0 && (dtRBPSResponse.Rows[0]["ErrorCode"].ToString() != "1"))
                        {

                            GetZipCode(achIn, dtRBPSResponse.Rows[0][CSAAWeb.Constants.PC_BILL_ADDRESS].ToString());
                            //Get the enrollment details and recent payments from PT DB
                            DataSet dsDetails = GetEnrollmentAndPaymentDetails(achIn.Policynumber, achIn.Zipcode);
                            //Loading entity
                            LoadReqDetailsToEntity(dtRBPSResponse, dsDetails, ref achIn);
                            CheckEligiblity(ref achIn);
                        }

                        else
                        {
                            achIn.EligiblityStatus = "Not Found";
                            if (dtRBPSResponse != null)
                            {

                                achIn.Response = dtRBPSResponse.Rows[0][Constants.PC_BILL_ERROR_DESCRIPTION].ToString().PadRight(300);

                            }
                        }

                        //Search Results
                        LoadSearhResultGrid(achIn);
                        //Recent Payments
                        LoadRecentPaymentsGrid(achIn);

                        SaveActionToDB(achIn, "INS");
                    }
                    else
                    {
                        lblErrorMsg.Visible = true;
                        lblErrorMsg.Text = Constants.PC_INVALID_POLICY;

                    }

                }

                btnCheckIncentive.Enabled = true;
            }
            catch (Exception ex)
            {
                enrollDetails.Visible = false;
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                Logger.Log(ex.ToString());
                Logger.Log(lblErrorMsg.Text.ToString());
                _PolicyNumber.Enabled = true;
                _ProductType.Enabled = true;
            }
        }

        /// <summary>
        /// Capture and split the Zipcode from RBPS response and validate the Zipcode availability in Pilot_ACH_Zipcode
        /// </summary>
        /// <param name="achIn">Entity</param>
        /// <param name="address">Address retrieved from RPBS</param>
        private void GetZipCode(ACHInfo achIn, string address)
        {
            string[] billAdd = address.Split(',');
            if (billAdd.Length > 0)
            {
                string[] ziparr = billAdd[billAdd.Length - 1].Split('-');

                if (ziparr.Length > 0)
                {
                    int iOutZip;
                    if (int.TryParse(ziparr[0], out iOutZip))
                    {
                        if (ziparr[0].Length == 5)
                        {
                            achIn.Zipcode = ziparr[0];
                        }
                    }

                }
            }
        }

        /// <summary>
        /// Load the list of Product types
        /// </summary>
        private void LoadProductTypes()
        {
            ProductTypes = ((SiteTemplate)Page).OrderService.LookupDataSet("Insurance", "ProductTypes").Tables[CSAAWeb.Constants.INS_Product_Type_Table];
        }

        /// <summary>
        /// Populate the list of Product types
        /// </summary>
        /// <param name="Dt"></param>
        /// <param name="Description"></param>
        private void AddSelectItem(DataTable Dt, string Description)
        {
            DataRow Row = Dt.NewRow();
            Row["ID"] = 0;
            Row["Description"] = Description;
            Dt.Rows.InsertAt(Row, 0);

        }

        /// <summary>
        /// Determine the Eligibility criteria of the Policy
        /// </summary>
        /// Eligibility criteria for the policy is calculated based on:
        /// Policy being searched should belong to the list of States NJ,PA,DE,MD,VA,DC. The policy being searched should of Source system PAS and of any Product type and KIC Auto only.
        /// The last payment on the policy should be done with Credit Card and ACH restriction should not be enabled on the Policy.
        /// The policy should not be enrolled with ACH.
        /// <param name="achIn">Entity</param>
        private void CheckEligiblity(ref ACHInfo achIn)
        {
            achIn.EligiblityStatus = "Not Eligible";
            Logger.Log("---------------------CheckEligiblity---------------");

            Logger.Log("achIn.PolicyState " + achIn.PolicyState);
            Logger.Log("achIn.SourceSystem.ToUpper() " + achIn.SourceSystem.ToUpper());
            Logger.Log("achIn.ProductType.ToUpper() " + achIn.ProductType.ToUpper());

            Logger.Log("achIn.SourceSystem.ToUpper() " + achIn.SourceSystem.ToUpper());
            Logger.Log("achIn.PaymentMethod.ToUpper() " + achIn.PaymentMethod.ToUpper());

            Logger.Log("achIn.PaymentRestriction.ToUpper() " + achIn.PaymentRestriction.ToUpper());
            Logger.Log("achIn.PaymentRestriction " + achIn.PaymentRestriction);

            Logger.Log("achIn.EnrollmentType " + achIn.EnrollmentType);
            Logger.Log("achIn.bZipCodeExists " + achIn.bZipCodeExists);
            //CHG0123980 - ACH Incentive Enhancement - April 2016
            if (achIn.enrollStates.Contains(achIn.PolicyState) && ((achIn.SourceSystem.ToUpper() == "KIC" && achIn.ProductType.ToUpper() == "PA") || achIn.SourceSystem.ToUpper() == "PAS") &&
               (achIn.PaymentMethod.ToUpper() == "CREDIT CARD") && (!(achIn.PaymentRestriction.ToUpper().Equals("TRUE")) && !string.IsNullOrEmpty(achIn.PaymentRestriction)) && (!(achIn.EnrollmentType == "EFT"))
                && (achIn.bZipCodeExists) && (achIn.IncentiveStatus.ToUpper() != "IS"))
            {
                achIn.EligiblityStatus = "Eligible";
            }
            else if (achIn.IncentiveStatus.ToUpper() == "IS")
            {
                achIn.EligiblityStatus = "Not eligible - Incentive already issued";
            }
            //CHG0123980 - ACH Incentive Enhancement - April 2016
        }

        /// <summary>
        /// Load the Recent payments grid of the policy 
        /// </summary>
        /// <param name="achIn">Entity</param>
        private void LoadRecentPaymentsGrid(ACHInfo achIn)
        {
            grdRecentPayments.DataSource = achIn.dtRecentPayments;
            grdRecentPayments.DataBind();
        }

        /// <summary>
        /// Load the Search results containing the Policy details and Recent payments details for the policy
        /// </summary>
        /// <param name="achIn">Entity</param>
        private void LoadSearhResultGrid(ACHInfo achIn)
        {
            DataTable dtTable = new DataTable();
            //header
            dtTable.Columns.Add(CSAAWeb.Constants.PC_ACH_PolicyNumber);
            dtTable.Columns.Add(CSAAWeb.Constants.PC_ACH_Product);
            dtTable.Columns.Add(CSAAWeb.Constants.PC_ACH_BillSystem);
            dtTable.Columns.Add(CSAAWeb.Constants.PC_ACH_Cus_Name);
            dtTable.Columns.Add(CSAAWeb.Constants.PC_ACH_Enroll_Status);
            dtTable.Columns.Add(CSAAWeb.Constants.PC_ACH_Eligibilty_Status);

            //Values
            DataRow dr = dtTable.NewRow();
            dr[CSAAWeb.Constants.PC_ACH_PolicyNumber] = achIn.Policynumber;
            dr[CSAAWeb.Constants.PC_ACH_Product] = achIn.ProductType;
            dr[CSAAWeb.Constants.PC_ACH_BillSystem] = achIn.SourceSystem;
            dr[CSAAWeb.Constants.PC_ACH_Cus_Name] = achIn.CustomerName;
            dr[CSAAWeb.Constants.PC_ACH_Enroll_Status] = achIn.EnrollmentDescription;
            dr[CSAAWeb.Constants.PC_ACH_Eligibilty_Status] = achIn.EligiblityStatus;
            dtTable.Rows.Add(dr);
            //  grdACHEnrollment.Rows[0].Cells[0].Width = 20;


            grdACHEnrollment.DataSource = dtTable.DefaultView;
            grdACHEnrollment.DataBind();
            grdACHEnrollment.Rows[5].Cells[1].ForeColor = Color.White;
            grdACHEnrollment.Rows[5].Cells[1].Font.Bold = true;
            if (achIn.EligiblityStatus == "Eligible")
            {

                grdACHEnrollment.Rows[5].Cells[1].BackColor = Color.Green;
            }
            else if (achIn.EligiblityStatus == "Not Eligible")
            {
                grdACHEnrollment.Rows[5].Cells[1].BackColor = Color.Red;
            }
            //CHG0123980 - ACH Incentive Enhancement - April 2016
            else if (achIn.EligiblityStatus == "Not eligible - Incentive already issued")
            {
                grdACHEnrollment.Rows[5].Cells[1].BackColor = Color.Red;
            }
            //CHG0123980 - ACH Incentive Enhancement - April 2016
            else
            {

                grdACHEnrollment.Rows[5].Cells[1].BackColor = Color.Gray;
                grdACHEnrollment.Rows[5].Cells[1].Text = "Not Found";
            }

        }

        /// <summary>
        /// Handle Null check for every value received from RPBS
        /// </summary>
        /// <param name="Value"></param>
        /// <returns></returns>
        private string CheckNull(object Value)
        {
            if (Value == null)
            {
                return string.Empty;
            }
            return Value.ToString();
        }

        /// <summary>
        /// Retreive the Enrollment and Payments details of the policy by querying Payment Central
        /// </summary>
        /// <param name="PolicyNumber">Input Paramater - Policynumber</param>
        /// <param name="zipCode">Input Paramater - Zipcode</param>
        /// <returns></returns>
        private DataSet GetEnrollmentAndPaymentDetails(string PolicyNumber, string zipCode)
        {
            //OpenConnection();
            //CHG0129017 - Removal of Linked server - Start// 
            string rep = Config.Setting("ConnectionString.ReadonlyDBInstance");
            SqlConnection con = new SqlConnection(rep);
            con.Open();
            //CHG0129017 - Removal of Linked server - End//
            SqlCommand Cmd = new SqlCommand("GET_Pilot_Enrollment_Payment_details", con);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.AddWithValue("@PolicyNumber", PolicyNumber);
            Cmd.Parameters.AddWithValue("@ZIPCode", zipCode);
            SqlDataAdapter da = new SqlDataAdapter();
            da.SelectCommand = Cmd;
            DataSet dsEntrollment = new DataSet();
            da.Fill(dsEntrollment);
            //CloseConnection();
            //CHG0129017 - Removal of Linked server - Start//
            con.Close();
            //CHG0129017 - Removal of Linked server - End//    
            return dsEntrollment;

        }

        /// <summary>
        /// Select the Product type in drop down
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void _ProductType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
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
                            //pnlEnroll.Visible = false;
                            btnSearch.Enabled = true;
                            Policy_Textbox.Enabled = true;
                            Policy_Textbox.Text = string.Empty;
                            string ProductType = _ProductType.SelectedItem.Value;
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

                        }
                    }
                }
                else
                {

                    _PolicyNumber.Enabled = false;
                }
            }
            catch (Exception ex)
            {
                enrollDetails.Visible = false;
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                Logger.Log(ex.ToString());
                Logger.Log(lblErrorMsg.Text.ToString());
                _PolicyNumber.Enabled = true;
                _ProductType.Enabled = true;
            }
        }

        /// <summary>
        /// Validate the Policy number based on Product types
        /// </summary>
        /// <param name="policy"></param>
        /// <param name="productType"></param>
        /// <returns></returns>
        private bool ValidPolicyProduct(string policy, string productType)
        {
            bool validpolicyproduct = true;
            string Invalid_Message = string.Empty;
            int iPolLength = policy.Trim().Length;
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

            return validpolicyproduct;
        }

        /// <summary>
        /// Validate the PUP Policies
        /// </summary>
        /// <returns></returns>
        private string Check_PUP_Policy()
        {

            if (!CSAAWeb.Validate.IsAllNumeric(_PolicyNumber.Text.Trim()))
            {
                return Constants.PUP_NUMERIC_VALID;
            }
            else if (_PolicyNumber.Text.Trim().Length != 7)
            {
                return Constants.POLICY_LENGTH_HOME_MESSAGE;
            }
            return string.Empty;
        }

        /// <summary>
        /// Policy number validation
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// Post the cancel event
        /// </summary>
        public override string OnCancelUrl { get { return Request.Path; } set { } }

        /// <summary>
        /// Capture the User Action and refresh the page
        /// List of User Actions captured in screen:Check, Credit To Policy, No Incentive
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnIncentive_Click(object sender, ImageClickEventArgs e)
        {

            //CHG0123980 - ACH Incentive Enhancement - April 2016- Added two buttons to capture Check Incentive
            //if (rblActions.SelectedIndex < 0)
            //{
            //    RadioButtonListValidator.IsValid = false;
            //    RadioButtonListValidator.MarkInvalid();
            //    _isValid = false;
            //    RadioButtonListValidator.ErrorMessage = "Please select any of the 'Action Taken' and Click on Submit button";
            //    rblActions.ForeColor = Color.Red;
            //    vldSumaary.Visible = true;
            //    enrollDetails.Visible = true;
            //}
            //CHG0123980 - ACH Incentive Enhancement - April 2016- Added two buttons to capture Check Incentive
            if (Page.IsPostBack)
            {
                btnCheckIncentive.Enabled = false;
            }


            if (_isValid)
            {

                try
                {
                    ACHInfo achInfo = new ACHInfo();
                    achInfo.ActionType = "Check";

                    achInfo.SourceId = ViewState["id"].ToString();
                    if (SaveActionToDB(achInfo, "UPD") > 0)
                    {
                        ScriptManager.RegisterStartupScript(this.Page, this.GetType(), "tmp", "<script type='text/javascript'>ACHPilotConfirm();</script>", false);
                        enrollDetails.Visible = false;
                        lblErrorMsg.Visible = false;
                    }
                }
                catch (Exception ex)
                {
                    enrollDetails.Visible = false;
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                    Logger.Log(ex.ToString());
                    Logger.Log(lblErrorMsg.Text.ToString());
                }
            }

        }

        /// <summary>
        /// Load the Entity based on the response from RBPS, Enrollment details from PC
        /// </summary>
        /// <param name="dtRBPSResponse">RBPS response</param>
        /// <param name="dsDetails">Retrieve the Enrollment and Recent Payments of the policy</param>
        /// <param name="achIn">Entity</param>
        private void LoadReqDetailsToEntity(DataTable dtRBPSResponse, DataSet dsDetails, ref ACHInfo achIn)
        {
            achIn.enrollStates = Convert.ToString(System.Configuration.ConfigurationSettings.AppSettings["Eligility_States"]);
            achIn.EnrollmentTypeCount = dsDetails.Tables[0].Rows.Count;
            if (achIn.EnrollmentTypeCount > 0)
            {
                achIn.EnrollmentType = CheckNull(dsDetails.Tables[0].Rows[0]["FOP"]);
                switch (achIn.EnrollmentType.ToUpper())
                {
                    case "CRDC":
                        achIn.EnrollmentDescription = "Enrolled with Credit Card";
                        achIn.EnrollmentStatus = true;
                        break;
                    case "EFT":
                        achIn.EnrollmentStatus = true;
                        achIn.EnrollmentDescription = "Enrolled with ACH";
                        break;
                    default:
                        break;
                }
            }

            achIn.PaymentRestriction = CheckNull(dtRBPSResponse.Rows[0][CSAAWeb.Constants.PC_BILL_PAYMENT_RESTRICTION]);
            achIn.Policynumber = CheckNull(dtRBPSResponse.Rows[0][CSAAWeb.Constants.PC_BILL_POL_NUMBER]);
            achIn.SourceSystem = CheckNull(dtRBPSResponse.Rows[0][Constants.PC_BILL_SOURCE_SYSTEM]);
            achIn.CustomerName = CheckNull(dtRBPSResponse.Rows[0]["INS_FULL_NME"]);
            achIn.PolicyState = CheckNull(dtRBPSResponse.Rows[0][Constants.PC_BILL_POL_STATE]);
            achIn.ProductType = CheckNull(dtRBPSResponse.Rows[0][CSAAWeb.Constants.PC_BILL_POL_TYPE]);

            achIn.dtRecentPayments = dsDetails.Tables[1];

            //CHG0123980 - ACH Incentive Enhancement - April 2016 - Added code for displaying the Card Type - Start
            if (achIn.dtRecentPayments.Rows.Count > 0)
            {
                achIn.PaymentMethod = CheckNull(dsDetails.Tables[1].Rows[0]["PaymentMethod"]);
                achIn.CardType = CheckNull(dsDetails.Tables[1].Rows[0]["CARD_TYPE"]);
                for (int i = 0; i < achIn.dtRecentPayments.Rows.Count; i++)
                {
                    if (achIn.dtRecentPayments.Rows[i][2].ToString().ToUpper() == "CRDC")
                    {
                        if (achIn.dtRecentPayments.Rows[i][5].ToString().ToUpper() == "DEBIT")
                        {
                            achIn.dtRecentPayments.Rows[i][2] = "Debit Card";
                        }
                        else if ((achIn.dtRecentPayments.Rows[i][5].ToString().ToUpper() == "CREDIT"
                           || string.IsNullOrEmpty(achIn.dtRecentPayments.Rows[i][5].ToString().ToUpper()))
                           && achIn.dtRecentPayments.Rows[i][2].ToString().ToUpper() == "CRDC")
                        {
                            achIn.dtRecentPayments.Rows[i][2] = "Credit Card";
                        }
                        achIn.PaymentMethod = CheckNull(dsDetails.Tables[1].Rows[0]["PaymentMethod"]);
                    }
                }
            }
            //CHG0123980 - ACH Incentive Enhancement - April 2016 -Added code for displaying the Card Type - End
            achIn.Response = "Success";

            if (dsDetails.Tables[2].Rows.Count > 0)
            {
                achIn.bZipCodeExists = true;
            }
            //CHG0123980 - ACH Incentive Enhancement - April 2016 - Added  code for checking if Incentive is already applied - Start
            if (dsDetails.Tables[3].Rows.Count > 0)
            {
                achIn.IncentiveStatus = CheckNull(dsDetails.Tables[3].Rows[0]["IS"]);
            }
            //CHG0123980 - ACH Incentive Enhancement - April 2016 - Added code for checking if Incentive is already applied - End
        }

        /// <summary>
        /// Validation on Policy number
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
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

        /// <summary>
        /// Mandatory field validation in the screen for Product Type and Policy number
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
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




        }
        #region DB Operation
        protected override void SavePageData()
        {
            InsuranceInfo I = new InsuranceInfo();
            I.Lines = new ArrayOfInsuranceLineItem();
            I.Lines.Add(new InsuranceLineItem());
            Order.Products.Add(I);

        }

        /// <summary>
        /// Retrieve the Connection string and Open the Database connection
        /// </summary>
        private void OpenConnection()
        {
            if (oConn != null) return;
            {
                string ConnectionString = Config.Setting("ConnectionString.Payments");
                oConn = new SqlConnection(ConnectionString);
            }
            oConn.Open();
        }


        /// <summary>
        /// Save the Enrollment,Eligibility and SOA response of the Policy in the Payments Databased
        /// Details will be saved in Pilot_ACH_Enrollment
        /// </summary>
        /// <param name="achInfo"></param>
        /// <param name="SaveType"></param>
        /// <returns></returns>
        protected int SaveActionToDB(ACHInfo achInfo, string SaveType)
        {
            try
            {


                OpenConnection();
                SqlCommand Cmd = new SqlCommand("Save_Pilot_ACH_Enrollment", oConn);
                Cmd.Parameters.AddWithValue("@UserId", achInfo.Userid);
                Cmd.Parameters.AddWithValue("@PolicyNumber", achInfo.Policynumber);
                Cmd.Parameters.AddWithValue("@Enrolled", achInfo.EnrollmentStatus);
                Cmd.Parameters.AddWithValue("@EnrollmentType", achInfo.EnrollmentType);
                //CHG0123980 - ACH Incentive Enhancement - April 2016 - Start - Saving original Payment type
                if (achInfo.SaveType.ToUpper() == "INS" && (achInfo.CardType.ToString().ToUpper() == "CREDIT" ||
                    string.IsNullOrEmpty(achInfo.CardType.ToString().ToUpper())) && achInfo.PaymentMethod.ToUpper() =="CREDIT CARD")
                {
                    achInfo.PaymentMethod = "CRDC";
                }
                else if (achInfo.SaveType.ToUpper() == "INS" && achInfo.CardType.ToString().ToUpper() == "DEBIT")
                {
                    achInfo.PaymentMethod = "DBTC";
                }
                //CHG0123980 - ACH Incentive Enhancement - April 2016 - End - Saving original Payment type
                Cmd.Parameters.AddWithValue("@PaymentMethod", achInfo.PaymentMethod);
                Cmd.Parameters.AddWithValue("@Action", achInfo.Action);
                Cmd.Parameters.AddWithValue("@UserActionType", achInfo.ActionType);
                Cmd.Parameters.AddWithValue("@SearchResponse", achInfo.Response);
                Cmd.Parameters.AddWithValue("@SaveType", SaveType);
                Cmd.Parameters.AddWithValue("@SourceID", achInfo.SourceId);
                Cmd.Parameters.AddWithValue("@EligibilityStatus", achInfo.EligiblityStatus);
                SqlParameter OutSourceId = Cmd.Parameters.Add("@InsertedID", SqlDbType.NVarChar, 20);
                OutSourceId.Direction = ParameterDirection.Output;
                Cmd.CommandType = CommandType.StoredProcedure;
                int iOut = Cmd.ExecuteNonQuery();
                ViewState["id"] = OutSourceId.Value;
                CloseConnection();
                return iOut;
            }
            catch (Exception ex)
            {
                enrollDetails.Visible = false;
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                Logger.Log(ex.ToString());
                Logger.Log(lblErrorMsg.Text.ToString());
                return 0;
            }
        }

        /// <summary>
        /// Close the Database connection
        /// </summary>
        public void CloseConnection()
        {
            if (oConn != null && oConn.State != ConnectionState.Closed)
            {
                oConn.Close();
            }
            oConn = null;
        }
        #endregion

    }

    /// <summary>
    /// Entity to fetch and retain values
    /// </summary>
    public class ACHInfo
    {
        public ACHInfo()
        {
            Userid = string.Empty;
            Policynumber = string.Empty;
            EnrollmentType = string.Empty;
            PaymentMethod = string.Empty;
            Action = "Search";
            ActionType = string.Empty;
            Response = string.Empty;
            SourceId = string.Empty;
            CustomerName = string.Empty;
            SourceSystem = string.Empty;
            EligiblityStatus = string.Empty;
            SaveType = "INS";
            enrollStates = string.Empty;
            PaymentRestriction = string.Empty;
            ProductType = string.Empty;
            PolicyState = string.Empty;
            EnrollmentDescription = "Policy is not Enrolled";
            Zipcode = string.Empty;
            EnrollmentStatus = false;
            bZipCodeExists = false;
            //CHG0123980 - ACH Incentive Enhancement - April 2016 Added Card Type and Incentive Status to Entity - Start
            CardType = string.Empty;
            IncentiveStatus = string.Empty;
            //CHG0123980 - ACH Incentive Enhancement - April 2016 Added Card Type and Incentive Status to Entity - End
        }
        public string EnrollmentDescription { get; set; }
        public string Userid { get; set; }
        public string Policynumber { get; set; }
        public bool EnrollmentStatus { get; set; }
        public string EnrollmentType { get; set; }
        public string PaymentMethod { get; set; }
        public string Action { get; set; }
        public string ActionType { get; set; }
        public string SaveType { get; set; }
        public string Response { get; set; }
        public string SourceId { get; set; }
        public string enrollStates { get; set; }
        public string PaymentRestriction { get; set; }
        public string ProductType { get; set; }
        //Search Results//
        public string CustomerName { get; set; }
        public string SourceSystem { get; set; }
        public string EligiblityStatus { get; set; }
        public string PolicyState { get; set; }
        public DataTable dtRecentPayments { get; set; }
        public int EnrollmentTypeCount { get; set; }
        public string Zipcode { get; set; }
        public bool bZipCodeExists { get; set; }
        //CHG0123980 - ACH Incentive Enhancement - April 2016 Added Card Type and Incentive Status to Entity - Start
        public string CardType { get; set; }
        public string IncentiveStatus { get; set; }
        //CHG0123980 - ACH Incentive Enhancement - April 2016 Added Card Type and Incentive Status to Entity - End
    }



}


