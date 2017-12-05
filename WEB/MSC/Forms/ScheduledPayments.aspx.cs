/*Added new aspx for OTSP Changes
 Code to display the Recurring Details of the policy, Scheduled Payments section and Scheduled Payments History section
 * Created new Web Method through AJAX call to display the History section
 * CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES
 */

using CSAAWeb;
using CSAAWeb.AppLogger;
using CSAAWeb.WebControls;
using MSC.Controls;
using OrderClassesII;
using OrderClasses.Service;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Remoting.Contexts;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Linq;
using System.Configuration;
using System.Net;
using InsuranceClasses;
using System.Globalization;


namespace MSC.Forms
{
    /// <summary>
    /// This page will be loaded which will display the Scheduled Payments details for a Policy
    /// The Users can View the list of Scheduled Payments for a policy
    /// </summary>
    public partial class OTSP : SiteTemplate
    {
        private static DataTable ProductTypes = null;
        private Boolean _isValid = true;
        string PolicyNumber = string.Empty;
        public static string OAuth2TokenURL = Convert.ToString(ConfigurationManager.AppSettings["ScheduleAuthjsonURL"]);
        private static string RecurringDate = Convert.ToString(ConfigurationManager.AppSettings["ConvertDate"]);
        static string Base64Encoded = Convert.ToString(ConfigurationManager.AppSettings["Base64Key"]);
        public static string OAuthToken;
        public static string[] PaymentID { get; private set; }
        public int Isactive;

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                lblErrorMsg.Visible = false;
                ScheduledRecurring.Visible = false;
                if (!Page.IsPostBack)
                {
                    BindColumnToGridview();
                    LinkButton lnkCustomerName = (LinkButton)_NameSearch.FindControl("lnkSearchByCustomerName");
                    lnkCustomerName.Visible = false;
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
                    if (ProductTypes.Rows.Count > 1) AddSelectItem(ProductTypes, Constants.PC_SEL_PROD);

                    _ProductType.DataSource = ProductTypes;
                    _ProductType.DataBind();
                }
                if (ViewState["policySourceSystem"] != null)
                {
                    HiddenSelectedDuplicatePolicy.Text = ViewState["policySourceSystem"].ToString();
                }
            }
            catch (Exception ex)
            {
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = Constants.PC_ERR_RUNTIME_EXCEPTION;
                Logger.Log(ex.ToString());
                Logger.Log(lblErrorMsg.Text.ToString());
                _PolicyNumber.Enabled = true;
                _ProductType.Enabled = true;
                throw;
            }
        }

        /// <summary>
        /// Dynamic grid for Scheduled Payments History section
        /// Create separate Datatable and bind the columns to the History Gridview
        /// </summary>
        private void BindColumnToGridview()
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add(Constants.Sch_PymtDate_Time);
                dt.Columns.Add(Constants.Sch_Amt);
                dt.Columns.Add(Constants.Sch_Event);
                dt.Columns.Add(Constants.Sch_Email);
                dt.Columns.Add(Constants.Sch_Initiated);
                dt.Columns.Add(Constants.Sch_Channel);
                dt.Columns.Add(Constants.Sch_PymtMethod);
                dt.Columns.Add(Constants.Sch_Last_4_AccNo);
                dt.Rows.Add();
                grdSchHistory.DataSource = dt;
                grdSchHistory.DataBind();
                grdSchHistory.Rows[0].Visible = false;
            }
            catch (Exception ex)
            {
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = Constants.PC_ERR_RUNTIME_EXCEPTION;
                Logger.Log(ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Search the Scheduled Payments details of the policy
        /// Populates the Recurring Details section, Scheduled Payments section, Scheduled Payments History section
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSearch_Click(object sender, ImageClickEventArgs e)
        {
            try
            {
                string Userid = string.Empty;
                Userid = ViewState["Userid"].ToString();
                PolicyNumber = _PolicyNumber.Text.ToUpper().Trim();

                if (_ProductType.SelectedIndex == 0 || string.IsNullOrEmpty(_PolicyNumber.Text.Trim()))
                {
                    vldSummary.Visible = true;
                }
                //Updated the below condition to avoid displaying the Error message on display of Search results grid - Dec 2016
                else if (PolicyNumber.Length < 4)
                {
                    vldSummary.Visible = false;
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = Constants.PC_INVALID_POLICY;
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
                        PC_ProductCode = myDataRow[Constants.PaymentCentral_Product_Code_table].ToString();
                    }
                    if (ValidPolicyProduct(PolicyNumber, ProductType))
                    {
                        _PolicyNumber.Enabled = false;
                        _ProductType.Enabled = false;
                        IssueDirectPaymentWrapper ObjIssueDirectService = new IssueDirectPaymentWrapper();
                        string SourceSystem = string.Empty;
                        SourceSystem = ObjIssueDirectService.DataSource(ProductType, PolicyNumber.Length);
                        HtmlTable CriteriaParameters = (HtmlTable)_NameSearch.FindControl("table1");
                        BillingLookUp billingSummary = new BillingLookUp();

                        DataTable dtRBPSResponse = null;
                        string srcSystem = ObjIssueDirectService.DataSource(ProductType, PolicyNumber.Length);
                        // Populate the selected item from the grid containing Duplicate policies
                        if (!string.IsNullOrEmpty(HiddenSelectedDuplicatePolicy.Text) && !(CriteriaParameters.Visible))
                        {
                            if (ViewState["DuplicateRpbsData"] != null)
                            {
                                dtRBPSResponse = (DataTable)ViewState["DuplicateRpbsData"];
                            }
                            DataRow[] dr = dtRBPSResponse.Select("SOURCE_SYSTEM ='" + HiddenSelectedDuplicatePolicy.Text + "'");//give null check
                            DataRow newRow = dtRBPSResponse.NewRow();
                            newRow.ItemArray = dr[0].ItemArray;
                            dtRBPSResponse.Rows.Remove(dr[0]);
                            dtRBPSResponse.Rows.InsertAt(newRow, 0);
                            for (int i = dtRBPSResponse.Rows.Count - 1; i >= 0; i--)
                            {
                                DataRow row = dtRBPSResponse.Rows[i];
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
                            dtRBPSResponse = billingSummary.checkPolicy(PolicyNumber, PC_ProductCode, Userid, SourceSystem);
                        }
                        if (dtRBPSResponse != null)
                        {
                            if (dtRBPSResponse.Rows.Count > 1)
                            {
                                //Logic to call the SearchResults PopUp window and select one.
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
                                _NameSearch.PopulateRpbsData(dtRBPSResponse);
                                ViewState.Add("DuplicateData", string.Format("{0}|{1}|{2}", PolicyNumber, PC_ProductCode, srcSystem));
                                ViewState.Add("DuplicateRpbsData", dtRBPSResponse);
                            }
                            else
                            {
                                if (dtRBPSResponse.Rows.Count > 0 && (!dtRBPSResponse.Rows[0]["ErrorCode"].ToString().Equals("1")))
                                {
                                    //Display Recurring Details and Scheduled Payment section for the policy
                                    //Populate Recurring Details and Scheduled Payments and Scheduled Payments History for a policy only if RBPS is successful
                                    if (dtRBPSResponse.Rows[0]["ErrorCode"].ToString().Equals("0"))
                                    {
                                        LoadRecurringDetails(dtRBPSResponse);
                                        //Set the values for the Entity to be used in the Scheduled Payments and Scheduled Payments History section 
                                        ScheduleEntity schEntity = new ScheduleEntity();
                                        schEntity.Policynumber = CheckNull(dtRBPSResponse.Rows[0][Constants.PC_BILL_POL_NUMBER]);
                                        schEntity.SourceSystem = CheckNull(dtRBPSResponse.Rows[0][Constants.PC_BILL_SOURCE_SYSTEM]);
                                        schEntity.ProductType = CheckNull(dtRBPSResponse.Rows[0][Constants.PC_BILL_POL_TYPE]);
                                        //Load Scheduled Payments Grid by calling SOA RetrieveByPolicyNo service
                                        if (OAuthToken == null)
                                        {
                                            GetAccessToken();
                                        }
                                        SchRetrieveByPolicyNo(schEntity);
                                    }
                                    else
                                    {
                                        lblErrorMsg.Visible = true;
                                        lblErrorMsg.Text = dtRBPSResponse.Rows[0]["ERR_MSG"].ToString() + " (" + dtRBPSResponse.Rows[0]["PC_ErrorCode"].ToString() + ")";
                                    }
                                }
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
                                    if (dtRBPSResponse.Rows[0]["ErrorCode"].ToString().Equals("1"))
                                    {
                                        if ((dtRBPSResponse.Rows[0]["setupAutoPayReasonCode"].ToString().Equals("ELIG_NEED_VRFY")))
                                        {
                                            if (KicAuto || KicHome || HDESHome || PUP)
                                            {
                                                ScheduledRecurring.Visible = false;
                                                lblErrorMsg.Visible = true;
                                                lblErrorMsg.Text = Constants.PC_INVALID_POLICY_FLOW_MESSAGE;
                                                _PolicyNumber.Enabled = false;
                                                _ProductType.Enabled = false;
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
                                            }
                                        }
                                        else if (dtRBPSResponse.Rows[0]["cancelAutoPayReasonCode"].ToString().Equals("ELIG_NEED_VRFY"))
                                        {
                                            if (KicAuto || KicHome || HDESHome || PUP)
                                            {
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
                                                ScheduledRecurring.Visible = false;
                                                if (dtRBPSResponse.Rows[0]["PC_ErrorCode"].ToString() == Constants.ERR_CODE_BUSINESS_EXCEPTION)
                                                {
                                                    lblErrorMsg.Visible = true;
                                                    lblErrorMsg.Text = dtRBPSResponse.Rows[0]["ERR_MSG"].ToString() + " (" + dtRBPSResponse.Rows[0]["PC_ErrorCode"].ToString() + ")";
                                                    _PolicyNumber.Enabled = true;
                                                    _ProductType.Enabled = true;
                                                }

                                                ScheduledRecurring.Visible = false;
                                            }
                                        }
                                        else
                                        {
                                            ScheduledRecurring.Visible = false;
                                            lblErrorMsg.Visible = true;
                                            lblErrorMsg.Text = dtRBPSResponse.Rows[0]["ERR_MSG"].ToString() + " (" + dtRBPSResponse.Rows[0]["PC_ErrorCode"].ToString() + ")";
                                            _PolicyNumber.Enabled = true;
                                            _ProductType.Enabled = true;
                                        }
                                    }

                                }
                            }
                        }
                        else
                        {
                            lblErrorMsg.Visible = true;
                            lblErrorMsg.Text = Constants.PC_INVALID_POLICY;

                        }
                    }
                    else
                    {
                        lblErrorMsg.Visible = true;
                        lblErrorMsg.Text = Constants.PC_INVALID_POLICY;

                    }
                }
            }
            catch (Exception ex)
            {
                ScheduledRecurring.Visible = false;
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = Constants.PC_ERR_RUNTIME_EXCEPTION;
                Logger.Log(ex.ToString());
                Logger.Log(lblErrorMsg.Text.ToString());
                _PolicyNumber.Enabled = true;
                _ProductType.Enabled = true;
                throw;
            }

        }

        /// <summary>
        /// Dynamic grid for Scheduled Payments section
        /// Populate Scheduled Payments based on response received from SOA RetrieveByPolcicyNo service
        /// </summary>
        /// <param name="schResp"></param>
        private void LoadScheduledPayments(List<schedulePaymentsDetails> schResp)
        {
            try
            {
                lblErrorMsgSch.Visible = false;
                pnlSch.Visible = true;
                ScheduledPaymentSection.Visible = true;
                DataTable dt = new DataTable();
                //CHG0131014 - Added the below condition to sort the Scheduled Payment Date in descending order
                dt.Columns.Add(Constants.Sch_PymtDate, typeof(DateTime));
                //CHG0131014 - Added the below condition to sort the Scheduled Payment Date in descending order
                dt.Columns.Add(Constants.Sch_Amt);
                dt.Columns.Add(Constants.Sch_Email);
                dt.Columns.Add(Constants.Sch_PymtMethod);
                dt.Columns.Add(Constants.Sch_Last_4_AccNo);
                dt.Columns.Add(Constants.Sch_Pymt_Status);
                dt.Columns.Add(Constants.Sch_Confirmation_No);
                int j = 0;
                PaymentID = new string[schResp.Count()];
                //CHG0131014 - Added the below code to display the Scheduled Payments for all payment id's
                Dictionary<string, DateTime?> objPaymentID = new Dictionary<string, DateTime?>();
                //CHG0131014 - Added the below code to display the Scheduled Payments for all payment id's
                foreach (var item in schResp)
                {
                    if (schResp.Count > 0)
                    {
                        DataRow dr = dt.NewRow();
                        PaymentID[j] = CheckNull(item.schedulePaymentsDetail.schedulePaymentInfo.paymentId);
                        //CHG0131014 - Added the below code to display the Scheduled Payments for all payment id's
                        objPaymentID.Add(PaymentID[j], item.schedulePaymentsDetail.scheduleActivity.paymentDate);
                        //CHG0131014 - Added the below code to display the Scheduled Payments for all payment id's
                        j++;
                        Context.Items["PymtDate"] = CheckNull(item.schedulePaymentsDetail.scheduleActivity.paymentDate);
                        //DateTime pymtDate = Convert.ToDateTime((item.schedulePaymentsDetail.scheduleActivity.paymentDate).ToString());
                        //dr[Constants.Sch_PymtDate] = pymtDate.ToString(RecurringDate);
                        //CHG0131014 - Updated the above condition to sort and set the Scheduled Payment Date
                        dr[Constants.Sch_PymtDate] = item.schedulePaymentsDetail.scheduleActivity.paymentDate;
                        //CHG0131014 - Updated the above condition to sort and set the Scheduled Payment Date
                        dr[Constants.Sch_Amt] = "$" + decimal.Parse(CheckNull(item.schedulePaymentsDetail.scheduleActivity.paymentAmt).ToString()).ToString("0.00");
                        dr[Constants.Sch_Email] = CheckNull(item.schedulePaymentsDetail.scheduleActivity.notificationEmail);
                        dr[Constants.Sch_PymtMethod] = CheckNull(item.schedulePaymentsDetail.scheduleActivity.paymentMethod);
                        if (item.schedulePaymentsDetail.scheduleActivity.bankAccount != null &&
                            !string.IsNullOrEmpty(item.schedulePaymentsDetail.scheduleActivity.bankAccount.numberLast4))
                        {
                            dr[Constants.Sch_Last_4_AccNo] = CheckNull(item.schedulePaymentsDetail.scheduleActivity.bankAccount.numberLast4);
                        }
                        else if (item.schedulePaymentsDetail.scheduleActivity.cardAccount != null &&
                            !string.IsNullOrEmpty(item.schedulePaymentsDetail.scheduleActivity.cardAccount.numberLast4))
                        {
                            dr[Constants.Sch_Last_4_AccNo] = CheckNull(item.schedulePaymentsDetail.scheduleActivity.cardAccount.numberLast4);
                        }
                        dr[Constants.Sch_Pymt_Status] = CheckNull(item.schedulePaymentsDetail.schedulePaymentInfo.status);
                        dr[Constants.Sch_Confirmation_No] = CheckNull(item.schedulePaymentsDetail.schedulePaymentInfo.paymentReferenceNumber);
                        dt.Rows.Add(dr);
                    }
                }


                //DataView dtv = dt.DefaultView;
                //dtv.Sort = "Scheduled PMT Date DESC";
                //Updated code for sorting issue in Pagination - Dec 2016//
                dt.DefaultView.Sort = "Scheduled PMT Date DESC";
                ViewState["SchPymts"] = dt.DefaultView.ToTable();
                //grdScheduledPayments.DataSource = dt.DefaultView;
                //Updated code for sorting issue in Pagination - Dec 2016//
                grdScheduledPayments.DataSource = dt.DefaultView.Table;
                grdScheduledPayments.DataBind();
                //int i = 0;
                //Set the PaymentID for each View History button
                //CHG0131014 - Added the below code to display the Scheduled Payments for all payment id's
                var sortedDict = from entry in objPaymentID orderby entry.Value descending select entry;
                foreach (KeyValuePair<string, DateTime?> item in sortedDict)
                {
                    hdnPymtID.Value = hdnPymtID.Value + item.Key + ";";
                }
                //CHG0131014 - Added the below code to display the Scheduled Payments for all payment id's
                //foreach (GridViewRow rows in grdScheduledPayments.Rows)
                //{
                //    hdnPymtID.Value = hdnPymtID.Value + PaymentID[i] + ";";
                //    i++;

                //}
            }
            catch (Exception ex)
            {
                Logger.Log(ex.Message);
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = Constants.PC_ERR_RUNTIME_EXCEPTION;
                ScheduledPaymentSection.Visible = false;
                pnlSch.Visible = false;
                throw;
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
        /// Load the list of Product types
        /// </summary>
        private void LoadProductTypes()
        {
            ProductTypes = ((SiteTemplate)Page).OrderService.LookupDataSet("Insurance", "ProductTypes").Tables[Constants.INS_Product_Type_Table];
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
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = Constants.PC_ERR_RUNTIME_EXCEPTION;
                Logger.Log(ex.ToString());
                Logger.Log(lblErrorMsg.Text.ToString());
                _PolicyNumber.Enabled = true;
                _ProductType.Enabled = true;
                throw;
            }
        }


        /// <summary>
        /// Validation on Policy number
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        protected void ValidPolicyNumberLength(Object source, ValidatorEventArgs args)
        {
            if (!string.IsNullOrEmpty(_PolicyNumber.Text.Trim()))
            {
                args.IsValid = (_PolicyNumber.Text.Trim().Length >= 4);
                if (!args.IsValid)
                {
                    vldPolicyNumberLength.MarkInvalid();
                    vldPolicyNumberLength.ErrorMessage = Constants.PC_INVALID_POLICY;
                    PolicyReqValidator.ErrorMessage = string.Empty;
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
            if (_ProductType.SelectedItem.Text.Equals(Constants.PC_SEL_PROD))
            {
                args.IsValid = false;
                vldProductType.MarkInvalid();
                vldProductType.ErrorMessage = Constants.PC_PROD_REQ;
                PolicyReqValidator.ErrorMessage = string.Empty;
                _isValid = false;
            }
            if (string.IsNullOrEmpty(_PolicyNumber.Text.Trim()))
            {
                args.IsValid = false;
                vldPolicyNumber.MarkInvalid();
                vldPolicyNumber.ErrorMessage = Constants.PC_POL_REQ;
                PolicyReqValidator.ErrorMessage = string.Empty;
                _isValid = false;
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
        /// Dynamic grid for displaying Recurring details section 
        /// Populate Recurring details grid based on response receieved from RPBS
        /// </summary>
        /// <param name="dtRbpsResponse"></param>
        public void LoadRecurringDetails(DataTable dtRbpsResponse)
        {
            try
            {
                ScheduledRecurring.Visible = true;
                DataTable dtTable = new DataTable();
                dtTable.Columns.Add(Constants.Sch_PolicyNumber);
                dtTable.Columns.Add(Constants.Sch_Recurring_Status);
                dtTable.Columns.Add(Constants.Sch_Enrolled);
                dtTable.Columns.Add(Constants.Sch_Plan);
                dtTable.Columns.Add(Constants.Sch_Due);
                dtTable.Columns.Add(Constants.Sch_Balance);
                dtTable.Columns.Add(Constants.Sch_DueDate);
                dtTable.Columns.Add(Constants.Sch_EffDate);
                dtTable.Columns.Add(Constants.Sch_ExpDate);

                DataRow dr = dtTable.NewRow();
                dr[Constants.Sch_Enrolled] = Constants.PC_POL_NOTENROLL_STATUS;
                dr[Constants.Sch_PolicyNumber] = CheckNull(dtRbpsResponse.Rows[0][Constants.PC_BILL_POL_NUMBER]);
                dr[Constants.Sch_Status] = CheckNull(dtRbpsResponse.Rows[0][Constants.PC_BILL_STATUS_DESCRIPTION].ToString());
                string Payment_Plan = CheckNull(Convert.ToString(dtRbpsResponse.Rows[0][Constants.PC_BIL_PaymentPlan]));
                if ((!string.IsNullOrEmpty(Payment_Plan) && Payment_Plan.ToUpper().Equals("AUTO"))
                    || dtRbpsResponse.Rows[0][Constants.PC_BILL_AUTO_PAY].ToString().ToUpper() == "TRUE")
                {
                    dr[Constants.Sch_Enrolled] = Constants.PC_POL_ENROLL_STATUS;
                }

                if (dtRbpsResponse.Rows[0][Constants.PC_BillingPlan].ToString().Equals(Constants.PC_POL_BILL_MONTHLY_NOTATION))
                {
                    dr[Constants.Sch_Plan] = Constants.PC_POL_BILL_MONTHLY;
                }
                else if (dtRbpsResponse.Rows[0][Constants.PC_BillingPlan].ToString().Equals(Constants.PC_POL_BILL_QUARTERLY_NOTATION))
                {
                    dr[Constants.Sch_Plan] = Constants.PC_POL_BILL_QUARTERLY;
                }
                else if (dtRbpsResponse.Rows[0][Constants.PC_BillingPlan].ToString().Equals(Constants.PC_POL_BILL_SEMI_NOTATION))
                {
                    dr[Constants.Sch_Plan] = Constants.PC_POL_BILL_SEMI;
                }
                else if (dtRbpsResponse.Rows[0][Constants.PC_BillingPlan].ToString().Equals(Constants.PC_POL_BILL_ANNUAL_NOTATION))
                {
                    dr[Constants.Sch_Plan] = Constants.PC_POL_BILL_ANNUAL;
                }
                else
                {
                    dr[Constants.Sch_Plan] = string.Empty;
                }
                if (!string.IsNullOrEmpty(dtRbpsResponse.Rows[0][Constants.PC_BILL_Due_Amount].ToString()))
                {
                    dr[Constants.Sch_Due] = "$" + decimal.Parse(dtRbpsResponse.Rows[0][Constants.PC_BILL_Due_Amount].ToString()).ToString("0.00");
                }
                if (!string.IsNullOrEmpty(dtRbpsResponse.Rows[0][Constants.PC_BILL_Total_Amount].ToString()))
                {
                    dr[Constants.Sch_Balance] = "$" + decimal.Parse(dtRbpsResponse.Rows[0][Constants.PC_BILL_Total_Amount].ToString()).ToString("0.00");
                }
                if (!string.IsNullOrEmpty(dtRbpsResponse.Rows[0][Constants.PC_BILL_Due_Date].ToString()))
                {
                    DateTime dueDate = Convert.ToDateTime(CheckNull(dtRbpsResponse.Rows[0][CSAAWeb.Constants.PC_BILL_Due_Date].ToString()));
                    dr[CSAAWeb.Constants.Sch_DueDate] = dueDate.ToString(RecurringDate);
                }
                else
                {
                    dr[CSAAWeb.Constants.Sch_DueDate] = string.Empty;
                }
                if (!string.IsNullOrEmpty(dtRbpsResponse.Rows[0][Constants.PC_BILL_TermEffectiveDate].ToString()))
                {
                    DateTime effDate = Convert.ToDateTime(CheckNull(dtRbpsResponse.Rows[0][Constants.PC_BILL_TermEffectiveDate].ToString()));
                    dr[Constants.Sch_EffDate] = effDate.ToString(RecurringDate);
                }
                else
                {
                    dr[Constants.Sch_EffDate] = string.Empty;
                }
                if (!string.IsNullOrEmpty(dtRbpsResponse.Rows[0][Constants.PC_TermExpirationDate].ToString()))
                {
                    DateTime expDate = Convert.ToDateTime(CheckNull(dtRbpsResponse.Rows[0][Constants.PC_TermExpirationDate].ToString()));
                    dr[Constants.Sch_ExpDate] = expDate.ToString(RecurringDate);
                }
                else
                {
                    dr[Constants.Sch_ExpDate] = string.Empty;
                }
                dtTable.Rows.Add(dr);
                gridRecurring.DataSource = dtTable.DefaultView;
                gridRecurring.DataBind();
            }
            catch (Exception ex)
            {
                ScheduledRecurring.Visible = false;
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = Constants.PC_ERR_RUNTIME_EXCEPTION;
                Logger.Log(ex.ToString());
                throw;
            }
        }

        #region GetAccessToken
        /// <summary>
        /// Calls OAuth2 with the Client Credentials and returns the Access Token
        /// AccessToken is mandatory which will be passed to SOA RetrieveByPolicyNo and RetrieveByPaymentId services
        /// If the token is expired then SOA OAuth needs to be invoked again to recieve the token
        /// </summary>
        /// <returns>Access Token</returns>
        public void GetAccessToken()
        {

            try
            {

                HttpResponseMessage response;
                using (HttpClient client = new HttpClient())
                {
                    //Setting the response to JSON
                    client.DefaultRequestHeaders.Accept.Clear();
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    //Send the EncodedValue which will be specific to an application
                    client.DefaultRequestHeaders.Add("Authorization", Base64Encoded);

                    //Build the POST data
                    var postdata = new FormUrlEncodedContent(new[]
                        {
                        new KeyValuePair<string, string>("grant_type", "client_credentials"), 
                        new KeyValuePair<string, string>("scope", string.Empty)
                        });

                    //Synchronous Call OAuth to get the token as resposne
                    response = client.PostAsync(OAuth2TokenURL, postdata).Result;
                    if (Config.Setting("Logging.SchedulePaymentService").Equals("1"))
                    {
                        Logger.Log("OAuth Request parameters to call the OAuth Service: " + postdata.Headers + client.DefaultRequestHeaders);
                        Logger.Log("OAuth Response received from OAuth Service: " + response.ToString());
                    }
                }

                if (response != null)
                {
                    //If token has been received
                    if (response.StatusCode == HttpStatusCode.OK)
                    {
                        //return the AccessToken
                        string json = response.Content.ReadAsStringAsync().Result;
                        if (Config.Setting("Logging.SchedulePaymentService").Equals("1"))
                        {
                            Logger.Log("Logging JSON object from OAuth response: " + json);
                        }
                        dynamic responseData = new JavaScriptSerializer().DeserializeObject(json);
                        if (responseData == null)
                        {
                            Logger.Log("Logging Exception on Deserializaion of JSON object: " + responseData);
                        }
                        else
                        {
                            //Save AccessToken which will be passed to Scheduledpayments section through Context and through HiddenField for History section
                            hdnAccessToken.Value = responseData["access_token"];
                            OAuthToken = responseData["access_token"];
                            if (Config.Setting("Logging.SchedulePaymentService").Equals("1"))
                            {
                                Logger.Log("OAuth token received from Deserializing Json object: " + responseData["access_token"]);
                                Logger.Log("OAuthToken Static variable: " + OAuthToken);
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = Constants.PC_ERR_RUNTIME_EXCEPTION;
                throw;
            }
        }
        #endregion

        /// <summary>
        /// Call SOA RetrieveByPolicyNumber service
        /// </summary>
        /// <param name="schEntity">Request parameters for calling the SOA service</param>
        /// <param name="ScheduledPolicyNoResponse">Response received from SOA service</param>
        public void SchRetrieveByPolicyNo(ScheduleEntity schEntity)
        {
            try
            {
                {
                    //if (Context.Items["AccessToken"] == null)

                    //Invoke the SOA service and recieve Access Token
                    //Only if AccessToken is available, then SOA OAuth Token service call will be successfull and will return a unique Token
                    //This Auth token will be passed to invoke RetrieveByPolicyNo, RetrieveByPaymentId services

                    //Populating  Request Body values
                    SchBody schBody = new SchBody();
                    //List of parameters to be passed to the SOA service
                    //Below are the list of mandatory parameters to be sent to SOA RetrieveByPolicyNo request
                    schBody.retrieveSchedulePaymentRequest.agreementInfo.identifier = (!string.IsNullOrEmpty(schEntity.Policynumber.Trim())) ? schEntity.Policynumber.Trim() : default(string);
                    schBody.retrieveSchedulePaymentRequest.agreementInfo.sourceSystem = (!string.IsNullOrEmpty(schEntity.SourceSystem.Trim())) ? schEntity.SourceSystem.Trim() : default(string);
                    schBody.retrieveSchedulePaymentRequest.agreementInfo.type = (!string.IsNullOrEmpty(schEntity.ProductType.Trim())) ? schEntity.ProductType.Trim() : default(string); ;
                    schBody.retrieveSchedulePaymentRequest.schedulePaymentStatus = Convert.ToString(ConfigurationManager.AppSettings["ScheduleStatus"]);
                    //Request to be sent to SOA Retrieve by PolicyNo
                    RetrieveByPolicyNo(schBody, schEntity);
                }

            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = Constants.PC_ERR_RUNTIME_EXCEPTION;
                throw;
            }

        }

        /// <summary>
        /// Set HeaderContext to call the SOA service
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        private static string GetApplicationContext(ScheduleEntity request)
        {
            StringBuilder applicationContext = new StringBuilder();
            applicationContext.Append(@"{");
            applicationContext.AppendFormat(@"""userId"":""{0}"",", request.UserId);
            applicationContext.AppendFormat(@"""transactionType"":""{0}"",", request.transactionType);
            applicationContext.AppendFormat(@"""application"":""{0}"",", request.application);
            applicationContext.AppendFormat(@"""subSystem"":""{0}"",", request.subSystem);
            applicationContext.AppendFormat(@"""correlationId"":""{0}"",", request.correlationId);
            applicationContext.AppendFormat(@"""address"":""{0}""", request.address);
            applicationContext.Append(@"}");

            return applicationContext.ToString();
        }

        /// <summary>
        /// Call the SOA RetrieveByPolicyNo service and recieve response to populate the Scheduled Payments section
        /// Pass the Header,Context and Body of the request and serialize the request to JSON format
        /// Recieved the response by de-serializing the response
        /// </summary>
        /// <param name="schPolicyNoRequest"></param>
        /// <param name="schBody">Body of the JSON request</param>
        /// <param name="schEntity"></param>
        private void RetrieveByPolicyNo(SchBody schBody, ScheduleEntity schEntity)
        {
            string url = Convert.ToString(Config.Setting("SchedulePolicyNojsonURL"));

            try
            {
                using (HttpClient client = new HttpClient())
                {
                    HttpResponseMessage response = GetPolicyResponse(schBody, schEntity, url, client);

                    //if token has expired//
                    if (response.StatusCode == HttpStatusCode.Unauthorized)
                    {
                        //401 - Client Credentials validation failed
                        Logger.Log("Logging Unauthorized Response received from RetrieveByPolicyNo Service: " + response.ToString());
                        if (Config.Setting("Logging.SchedulePaymentService").Equals("1"))
                        {
                            Logger.Log("Calling OAuth service when OAuthToken Static variable is null:  " + response.ToString());
                        }
                        GetAccessToken();
                        //Call the OAuth service again from Global.ascx.cs
                        //gc.GetValue();
                        //Call the OAuth service again from Global.ascx.cs
                        GetPolicyResponse(schBody, schEntity, url, client);
                    }
                    string json = response.Content.ReadAsStringAsync().Result;
                    if (Config.Setting("Logging.SchedulePaymentService").Equals("1"))
                    {
                        Logger.Log("Logging JSON Response after deserializing RetrieveByPolicyNo service response: " + json);
                    }
                    //Deserialize the JSON response
                    JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                    ScheduledPolicyNoResponse resp = jsSerializer.Deserialize<ScheduledPolicyNoResponse>(json);

                    //If JSON response is successful then Load the Scheduled Payments grid based on the response recieved
                    if (resp.retrieveSchedulePaymentResponse != null)
                    {
                        List<schedulePaymentsDetails> respSchPymnt;
                        respSchPymnt = resp != null ? resp.retrieveSchedulePaymentResponse.schedulePaymentsDetails.ToList() : null;
                        if (respSchPymnt != null && respSchPymnt.Count > 0)
                            //Populate Scheduled Payment section
                            LoadScheduledPayments(respSchPymnt);
                    }
                    //If JSON response has the Error Codes namely 400,401,500 then display the MoreInformation text in the UI
                    else
                    {
                        ErrorCode ecSch = jsSerializer.Deserialize<ErrorCode>(json);
                        if (Config.Setting("Logging.SchedulePaymentService").Equals("1"))
                        {
                            Logger.Log("Logging Error recieved from RetrieveByPolicyNo Service:" + ecSch);
                        }
                        //Display the Error Message recieved from SOA service
                        if (ecSch != null)
                        {
                            if (ecSch.httpCode == "401" && ecSch.httpMessage == "Unauthorized")
                            {
                                lblErrorMsgSch.Text = ecSch.moreInformation;
                                lblErrorMsgSch.Visible = true;
                            }
                            else if (ecSch.httpCode == "400" && ecSch.httpMessage == "Bad Request")
                            {
                                lblErrorMsgSch.Text = ecSch.moreInformation;
                                lblErrorMsgSch.Visible = true;
                            }
                            else if (ecSch.httpCode == "500")
                            {
                                lblErrorMsgSch.Text = ecSch.moreInformation;
                                lblErrorMsgSch.Visible = true;
                            }
                        }
                    }

                }

            }
            catch (Exception ex)
            {
                ScheduledRecurring.Visible = false;
                Logger.Log(ex.ToString());
                lblErrorMsg.Visible = true;
                lblErrorMsg.Text = Constants.PC_ERR_RUNTIME_EXCEPTION;
                throw;
            }
        }

        private HttpResponseMessage GetPolicyResponse(SchBody schBody, ScheduleEntity schEntity, string url, HttpClient client)
        {
            string stApplicationContext = GetApplicationContext(schEntity);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            //Add the accessToken and Context which will be passed as Header to the SOA request
            //client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", string.Format("Bearer {0}", HttpContext.Current.Application["OAuthToken"].ToString()));
            client.DefaultRequestHeaders.TryAddWithoutValidation("Authorization", string.Format("Bearer {0}", OAuthToken));
            hdnAccessToken.Value = OAuthToken;
            client.DefaultRequestHeaders.Add("X-ApplicationContext", stApplicationContext);
            if (Config.Setting("Logging.SchedulePaymentService").Equals("1"))
            {
                //Logger.Log("Logging JSON Request Authorization:" + string.Format("Bearer {0}", Context.Items["AccessToken"].ToString()));
                Logger.Log("Logging JSON Request Header for RetrieveByPolicyNo: " + string.Format("Bearer {0}", OAuthToken) + stApplicationContext);
            }
            //Class that will be serialized into Json and posted 

            //Serialize the Entity as JSON and pass the JSON request to the SOA service
            DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(typeof(SchBody));
            MemoryStream ms = new MemoryStream();
            jsonSer.WriteObject(ms, schBody);
            ms.Position = 0;
            StreamReader sr = new StreamReader(ms);
            string s = sr.ReadToEnd();
            StringContent theContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json");
            if (Config.Setting("Logging.SchedulePaymentService").Equals("1"))
            {
                Logger.Log("Logging JSON request sent to RetrieveByPolicyNo Service: " + s);
            }

            //Recieve response from SOA service
            HttpResponseMessage response = client.PostAsync(url, theContent).Result;
            if (Config.Setting("Logging.SchedulePaymentService").Equals("1"))
            {
                Logger.Log("Logging response from RetrieveByPolicyNo Service: " + response);
            }
            return response;

        }


        /// <summary>
        /// Call the SOA RetrievebyPaymentID service and recieve response to populate the History section
        /// Pass the Header,Context, Body of the request  in JSON format by serializing the request
        /// Recieve response from SOA RetrieveByPaymentID in JSON format by de-serializing the request
        /// Recieve Error Code to he displayed for any errors
        /// Invoked from AJAX call
        /// </summary>
        /// <param name="schBody"></param>
        /// <param name="schEntity"></param>
        /// <returns></returns>
        private static List<schedulePaymentsDetail> RetrieveByPaymentID(SchBody schBody, ScheduleEntity schEntity, int skip, int take, string token)
        {
            string url = Convert.ToString(Config.Setting("SchedulePaymentIdjsonURL"));
            List<schedulePaymentsDetail> respSchList = new List<schedulePaymentsDetail>();
            List<ErrorCode> errorList = new List<ErrorCode>();

            try
            {
                HttpResponseMessage response = GetPaymentIDResp(schBody, schEntity, token, url);
                string json = response.Content.ReadAsStringAsync().Result;
                if (Config.Setting("Logging.SchedulePaymentService").Equals("1"))
                {
                    Logger.Log("Logging Response from RetrieveByPaymentID Service: " + response);
                    Logger.Log("Logging JSON Response after deserializing RetrieveByPaymentID Service response: " + json);

                }
                JavaScriptSerializer jsSerializer = new JavaScriptSerializer();
                //Deserialize the JSON response
                ScheduledPolicyNoResponse resp = jsSerializer.Deserialize<ScheduledPolicyNoResponse>(json);
                ErrorCode err = jsSerializer.Deserialize<ErrorCode>(json);
                errorList.Add(err);

                //If JSON response is successful then Load the Scheduled Payments History grid based on the response recieved
                if (resp != null)
                {
                    if (resp.retrieveSchedulePaymentResponse != null)
                    {
                        //Filter the records if CreatedDateTime is less than 1 year
                        //Skip and Take are the parameters used for Paging
                        //CHG0131014 - Updated code to order the Scheduled Payments section in Descending order - Dec 2016
                        List<scheduleActivities> schedul = (from c in resp.retrieveSchedulePaymentResponse.schedulePaymentsDetail.scheduleActivities
                                                            where c.scheduleActivity.creationDatetime > DateTime.Now.AddDays(-365)
                                                            orderby c.scheduleActivity.creationDatetime descending
                                                            select c).Skip(skip).Take(take).ToList();
                        schedulePaymentsDetail schPytDetail = new schedulePaymentsDetail();
                        schPytDetail.scheduleActivities = schedul.ToArray();
                        //Pass the Count to be displayed in the Paging
                        //CHG0131014 - Updated code to order the Scheduled Payments section in Descending order - Dec 2016
                        schPytDetail.SchCount = (from c in resp.retrieveSchedulePaymentResponse.schedulePaymentsDetail.scheduleActivities
                                                 where c.scheduleActivity.creationDatetime > DateTime.Now.AddDays(-365)
                                                 orderby c.scheduleActivity.creationDatetime descending
                                                 select c).Count();

                        respSchList.Add(schPytDetail);
                    }
                    //If JSON response has the Error Codes namely 400,401,500 then display the MoreInformation text in the UI
                    else
                    {
                        retrieveSchedulePaymentResponse retrieveresp = new retrieveSchedulePaymentResponse();
                        retrieveresp.schedulePaymentsDetail = new schedulePaymentsDetail();
                        if (Config.Setting("Logging.SchedulePaymentService").Equals("1"))
                        {
                            Logger.Log("Logging Error recieved from RetrieveByPaymentID Service:" + err);
                        }
                        if (err != null)
                        {
                            retrieveresp.schedulePaymentsDetail.ErrorCode = err;
                            respSchList.Add(retrieveresp.schedulePaymentsDetail);
                        }
                    }
                }


            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
                throw;
            }

            //This list will be passed to the WebMethod and read in the AJAX call to display the response in UI
            return respSchList;
        }

        private static HttpResponseMessage GetPaymentIDResp(SchBody schBody, ScheduleEntity schEntity, string token, string url)
        {
            using (HttpClient client = new HttpClient())
            {
                string stApplicationContext = GetApplicationContext(schEntity);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Add the accessToken and Context which will be passed as Header to the SOA request
                client.DefaultRequestHeaders.Add("Authorization", string.Format("Bearer {0}", token));
                client.DefaultRequestHeaders.Add("X-ApplicationContext", stApplicationContext);
                if (Config.Setting("Logging.SchedulePaymentService").Equals("1"))
                {
                    Logger.Log("Logging JSON Request Header for RetrieveByPaymentId: " + string.Format("Bearer {0}", token) + stApplicationContext);
                }
                //Class that will be serialized into Json and posted 

                //Serialize the Entity as JSON and pass the JSON request to the SOA service
                DataContractJsonSerializer jsonSer = new DataContractJsonSerializer(typeof(SchBody));

                MemoryStream ms = new MemoryStream();
                jsonSer.WriteObject(ms, schBody);
                ms.Position = 0;
                StreamReader sr = new StreamReader(ms);

                string s = sr.ReadToEnd();
                StringContent theContent = new StringContent(s, System.Text.Encoding.UTF8, "application/json");
                if (Config.Setting("Logging.SchedulePaymentService").Equals("1"))
                {
                    Logger.Log("Logging JSON Request sent to RetrieveByPaymentID Service: " + s);
                }


                //Recieve JSON response from SOA service
                HttpResponseMessage response = client.PostAsync(url, theContent).Result;
                return response;
            }
        }


        /// <summary>
        /// Web Method for the AJAX call to display the History section
        /// Pass the token from Hidden field to the SOA RetrieveBy PaymentID
        /// The Skip and Take parameters are used for Paging and the Token is the value from OAuth service
        /// PaymentID is value recieved by hitting the SOA RetrieveByPolicyNo service and passed using Hiddenvariable
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [WebMethod]
        public static List<schedulePaymentsDetail> GetData(int skip, int take, string id, string token)
        {
            //Populating  Request Body values
            SchBody schPolicyNoRequest = new SchBody();
            //Mandatory parameters to be passed to the SOA RetrieveByPaymentID service
            schPolicyNoRequest.retrieveSchedulePaymentRequest.paymentId = id;
            schPolicyNoRequest.retrieveSchedulePaymentRequest.includeHistory = true;
            ScheduleEntity schEntity = new ScheduleEntity();
            List<schedulePaymentsDetail> list = RetrieveByPaymentID(schPolicyNoRequest, schEntity, skip, take, token);

            return list;
        }

        /// <summary>
        /// Displays the footer of the grid
        /// Paging to be displyed in the Scheduled Payments section if number of records is greate than10
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event argument</param>
        protected void GrdScheduledPayments_RowCreated(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.Pager)
                {
                    grdScheduledPayments.PagerSettings.Mode = PagerButtons.NumericFirstLast;
                    grdScheduledPayments.PagerSettings.FirstPageText = "<<";
                    grdScheduledPayments.PagerSettings.LastPageText = ">>";
                    grdScheduledPayments.PagerSettings.PreviousPageText = "<";
                    grdScheduledPayments.PagerSettings.NextPageText = ">";

                    Label control = new Label();
                    control.Text = "Showing Page " + (grdScheduledPayments.PageIndex + 1) + " of " + grdScheduledPayments.PageCount;

                    Table table = e.Row.Cells[0].Controls[0] as Table;
                    table.Attributes.Add("Class", "arial_12");
                    table.Width = Unit.Percentage(100);
                    TableCell newCell = new TableCell();
                    newCell.Attributes.Add("align", "right");
                    newCell.Attributes.Add("Class", "arial_12");
                    newCell.Controls.Add(control);
                    table.Rows[0].Cells.Add(newCell);

                }
            }
            catch (Exception ex)
            {
                Logger.Log("Exception occured upon grdScheduledPayments_RowCreated method : " + ex.ToString());
                throw;
            }
        }

        /// <summary>
        /// Displays when the user selects the Next page. 
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event argument</param>
        protected void GrdScheduledPayments_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            ScheduledRecurring.Visible = true;
            grdScheduledPayments.PageIndex = e.NewPageIndex;
            grdScheduledPayments.DataSource = ViewState["SchPymts"];
            grdScheduledPayments.DataBind();
        }


        /// <summary>
        /// Saves state data.
        /// </summary>
        protected override void SavePageData()
        {
            InsuranceInfo I = new InsuranceInfo();
            I.Lines = new ArrayOfInsuranceLineItem();
            I.Lines.Add(new InsuranceLineItem());
            Order.Products.Add(I);

        }


    }

    /// <summary>
    /// Entity to frame request parameters to call the SOA services
    /// Set value for the list of parameters passed to the Context of the SOA Service
    /// </summary>
    public class ScheduleEntity
    {
        public ScheduleEntity()
        {
            SourceSystem = string.Empty;
            ProductType = string.Empty;
            Policynumber = string.Empty;
            UserId = HttpContext.Current.User.Identity.Name;
            transactionType = Convert.ToString(ConfigurationManager.AppSettings["TransactionType"]);
            application = Convert.ToString(ConfigurationManager.AppSettings["ApplicationContext_Payment_Tool"]);
            subSystem = Convert.ToString(ConfigurationManager.AppSettings["Subsystem"]);
            correlationId = Guid.NewGuid().ToString();
            address = Dns.GetHostEntry(hostname).AddressList[0].ToString();
        }
        public string ProductType { get; set; }
        public string SourceSystem { get; set; }
        public string Policynumber { get; set; }
        public string UserId { get; set; }
        public string transactionType { get; set; }
        public string application { get; set; }
        public string subSystem { get; set; }
        public string correlationId { get; set; }
        public string address { get; set; }
        public string hostname = Dns.GetHostName();
    }



}

