/*
 * REVISION HISTORY
 * 11/11/2004 JOM Replaced InsuranceClasses.Service.Insurance with TurninClasses.Service.Turnin.
 *			  Also removed multiple creations of this service.
 * 	REVISION HISTORY:
 *	MODIFIED BY COGNIZANT
 *	07/19/2005 - For changing the web.config entries pointing to database. This changes 
 *				 are made as part of CSR #3937 implementation.
 *	Changes Done:
 *	CSR#3937.Ch1 : Include the namespace for accessing the StringDictionary object
 *	CSR#3937.Ch2 : Changed the code for getting constant value from Application object (web.config changes)
 *	CSR#3937.Ch3 : Changed the code for getting constant value from Application object (web.config changes)
 
 * MODIFIED BY COGNIZANT AS PART OF Q4 RETROFIT CHANGES
 * 11/30/2005 Q4-Retrofit.Ch1 :  Modified the code to change the Revenue Type for Huon products as "Payments"
 * 11/30/2005 Q4-Retrofit.Ch2 :  Modified the code to call the Renamed Function "PAYWorkflowReport"
 * 12/08/2005 Q4-Retrofit.Ch3 : Changed the code for getting constant value from Application object (web.config changes)
 * 
 * STAR Retrofit II Changes: 
 * Modified as a part of CSR 4852 
 * 1/31/2007 STAR Retrofit II.Ch1: 
 *           The content of the cache "AUTH_DOS" is cleared, so as to populate the cache with updated 
 *			 data,when the page is getting loaded for the first time.
 * 07/09/2010 MP2.0.Ch1 Fixed the Bug as part of MP2.0 testing 
 *Modified by cognizant as a part of .NetMig 3.5  for migration of cross reference report data grid to grid view on 08-12-2010.
*	.NetMig 3.5.Ch1 : Start:Added and Modified by cognizant as a part of .NetMig 3.5 on 08-12-2010 for migration on data grid to grid view.
*	.Net 3.5 Mig.Ch2 code change done by Cognizant on 8/13/2010
*	.Net 3.5 Mig.Ch3 code change done by Cognizant on 8/13/2010
*	.NetMig 3.5.Ch4 code change done by Cognizant on 8/13/2010
*	.NetMig 3.5.Ch5 code change done by Cognizant on 8/13/2010
*	.NetMig 3.5.Ch6 code change done by Cognizant on 8/13/2010	
*	.NetMig 3.5.Ch7 code change done by Cognizant on 8/13/2010
 * MODIFIED BY COGNIZANT AS A PART OF NAME CHANGE PROJECT ON 8/19/2010
 * NameChange.Ch1:Changed the name CSAA to AAA NCNU IE .
 * MODIFIED AS A PART OF TIME ZONE CHANGE ENHANCEMENT ON 8-31-2010
 * TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time  by cognizant.
 * TimeZoneChange.Ch2-Commented to display only date and not time as a part of Time Zone Change  by cognizant.
 //67811A0 - START -PCI Remediation for Payment systems Start: Added to fix the Object reference exception by cognizant on 1/11/2012
//Security Defect - CH1 -START Added the below code to verify if the IP address of the login page and current page is same
 * //Security Defects -CH2 - Added the below code to add session values to the context.
 * SSO Integration - CH1 -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
 * PC Phase II changes CH1 - Modified the below code to handle Date Time Picker
 * CHG0083477 - Name Change from AAA NCNU IE to CSAA IG as part of Name change project on 11/15/2013.
 * CHG0088851 - Penetration defects - Included condition for Cross Reference Report Link button availablity.
 * CHG0104053 - PAS HO CH1 - Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/16/2014
 * CHG0104053 - PAS HO CH2 - Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/16/2014
 * CHG0104053 - PAS HO CH3 - Added the below code to get the Policy number details for validation 7/3/2014
 * CHG0104053 - PAS HO CH4 - Modified the below code to group under PAS Auto, PAS Home 7/02/2014
 * CHG0104053 - PAS HO CH5 - Added the below code to change the label from HomeOwner to Home/HDES 7/3/2014
 * MAIG - CH1 - Modified the below code to default the cashier reconciliation report
 * MAIG - CH2 - Modified the condition to include Null check also
 * MAIG - CH3 - Modified the condition to include Null check also
 * MAIG - CH4 - Modified the condition to include Null check also
 * MAIG - CH5 - Modified the condition to include Null check also
 * MAIG - CH6 - Modified the condition to include Null check also
 * MAIG - CH7 - Modified the condition to include Null check also
 * MAIG - CH8 - Modified the code to support the common product types
 * MAIG - CH9 - Added code to Sort PayType to get PAS Auto,PAS Home,Home/HDES, WU/ACA, Homeowners and Membership Products in order.
 * MAIG - CH10 - Commeneted the un-used Product Types
 * MAIG - CH11 - Modified the condition to include Null check also
 * MAIG - CH12 - Modified the condition to include Null check also
 * CHG0109406 - CH1 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH2 - Modified the colspan value from 11 to 7 to accomodate the TimeZone field
 * CHG0109406 - CH3 - Modified the colspan value from 10 to 6 to accomodate the TimeZone field
 * CHG0110069 - CH1 - Appended the /Time along with the Date Label
*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CSAAWeb;
using CSAAWeb.AppLogger;
using MSCR.Reports;
using OrderClasses.Service;

//CSR#3937.Ch1 : START - Include the namespace for accessing the StringDictionary object
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
//CSR#3937.Ch1 : END - Include the namespace for accessing the StringDictionary object
//PC Phase II changes - Modified the below code for handling Date Time Picker 
namespace MSCR.Reports
{
    // Summary description for CashierReconciliation_Report.
    public partial class CashierReconciliation_Report : CSAAWeb.WebControls.PageTemplate
    {
        //protected MSCR.Controls.Dates Dates;
        protected System.Data.DataTable dtbSummary;
        protected System.Data.DataTable dtbRepeater;
        protected System.Data.DataTable dtbOtherSummary;
        protected System.Data.DataTable dtbOtherRepeater;
        protected System.Data.DataTable dtbTotal;
        protected System.Data.DataTable dtbTotalRepeater;






        //Constant for Report Types - Codes 
        const string CASHIER_RECONCILIATION_REPORT_TYPE = "4";
        const string MEMBERSHIP_REPORT_TYPE = "2";
        const string CROSSREFERENCE_REPORT_TYPE = "3";
        string tmporderid = "";
        // Variable to store the Receipt Number of the previous transaction in the Cross Reference Report datagrid
        string tmpreceiptnum = "";
        double dblTotalMember = 0;
        string CurrentUser;
        //Constants Values for Header Row,Status and WU Products
        const string ROW_CLASS = "item_template_row";
        const string ALTERNATE_ROW_CLASS = "item_template_alt_row";
        const string APPROVED = "Approved";
        const string PENDING_APPROVAL = "Pending Approval";
        const string EARNED_PREMIUM = "EARNED PREMIUM";
        const string MEMBER = "MBR";
        string colour = ALTERNATE_ROW_CLASS;
        // Variable to store the colour of the current row in the Cross Reference Report datagrid
        string crosscolour = ALTERNATE_ROW_CLASS;
        string tmpreceipt = "";
        double dblTotal = 0;
        protected bool bReject = false;
        TurninClasses.Service.Turnin TurninService;
        CollectionSummaryLibrary CollectionSummary = new CollectionSummaryLibrary();
        InsRptLibrary rptLib = new InsRptLibrary();

        #region DOs

        /// <summary>
        /// For filling the RepDo's in the Cache
        /// </summary>
        protected DataTable DOs
        {
            get
            {
                return (DataTable)Cache["AUTH_DOS"];
            }
            set
            {
                Cache["AUTH_DOS"] = value;
            }
        }

        #endregion

        #region Page_Load

        /// <summary>
        /// Adding Report Types,Status Type,UserList,DoList in the respective boxes
        /// </summary>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            // SSO Integration - CH1 Start -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            // SSO Integration - CH1 End -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
            ////Security Defect - CH1 -START Added the below code to verify if the IP address of the login page and current page is same

            //string ipaddr1 = Request.ServerVariables["REMOTE_ADDR"];
            //if (CSAAWeb.AppLogger.Logger.Sourcereg(ipaddr1))
            //{
            //    ipaddr1 = Request.ServerVariables["REMOTE_ADDR"];
            //}
            //else
            //{
            //    ipaddr1 = CSAAWeb.AppLogger.Logger.GetIPAddress();
            //}
            //if (Request.Cookies["AuthToken"] == null)
            //{
            //    Logger.Log(CSAAWeb.Constants.SEC_COOKIE_NULL);
            //    Response.Redirect("/PaymentToolmscr/Forms/login.aspx");

            //}
            //else if (!ipaddr1.Equals(
            //    Request.Cookies["AuthToken"].Value))
            //{
            //    Logger.Log(CSAAWeb.Constants.SEC_COOKIE_NOTEQUAL);
            //    Response.Redirect("/PaymentToolmscr/Forms/login.aspx"); ;

            //}
            ////Security Defect - CH1 -End Added the below code to verify if the IP address of the login page and current page is same
            //else
            //{
                tblCollSummary.Attributes.Add("width", "0");
                tblOtherCollSummary.Attributes.Add("width", "0");

                trRejectLableRow.Visible = false;
                trRejectRow.Visible = false;
                InvisibleUserInfo();
                CurrentUser = Page.User.Identity.Name;
                TurninService = new TurninClasses.Service.Turnin();
                lblErrMsg.Text = "";

                if (!Page.IsPostBack)
                {
                    //STAR Retrofit II.Ch1 - START:The content of the cache "AUTH_DOS" is cleared, so as to populate the cache with updated 
                    //data,when the page is getting loaded for the first time.
                    Cache.Remove("AUTH_DOS");
                    //STAR Retrofit II.Ch1 - END

                    //trDates.Visible = false;
                    //Filling Report Types
                    FillReport();
                    //Filling the Status Combo
                    FillStatus();
                    //Filling the Cache for RepDo 
                    rptLib.FillDOListBox(_RepDO, DOs);

                    //To Get the Current User's Rep DO
                    AuthenticationClasses.Service.Authentication Auth = new AuthenticationClasses.Service.Authentication();
                    DataSet dtsRepIDDO = Auth.GetRepIDDO(CurrentUser);

                    string strRepDO = dtsRepIDDO.Tables[0].Rows[0]["DO ID"].ToString().Trim();

                    setSelectedIndex(_RepDO, strRepDO);

                    rptLib.FillUserListBox(UserList, CurrentUser, "All", "-1", _RepDO.SelectedItem.Value, true, true);
                    //Added by Cognizant on 04/20/2005 to fill Approvers DropDown
                    rptLib.FillApproversDropDown(_Approvers, _RepDO.SelectedItem.Value);

                    //Set the Default Selected Item in Approver's DropDown as the Login User
                    setSelectedIndex(_Approvers, User.Identity.Name);
                }


                if (Cache["Cashier Report Type"] == null)
                {
                    Cache["Cashier Report Type"] = CASHIER_RECONCILIATION_REPORT_TYPE;
                }
                //07/09/2010 Removed the 'else' from condition to fix the Null reference exception - by Cognizant
                if (Cache["Member Report Type"] == null)
                {
                    Cache["Member Report Type"] = MEMBERSHIP_REPORT_TYPE;
                }
                //07/09/2010 Removed the 'else' from condition to fix the Null reference exception - by Cognizant
                if (Cache["Cross Reference Report Type"] == null)
                {
                    Cache["Cross Reference Report Type"] = CROSSREFERENCE_REPORT_TYPE;
                }
                //67811A0 - START -PCI Remediation for Payment systems Start: Added to fix the Object reference exception by cognizant on 1/11/2012
                if (Cache["Pending Approval"] == null)
                {
                    Cache["Pending Approval"] = cboStatus.Items.FindByText(PENDING_APPROVAL).Value;
                }
                if (Cache["Approved"] == null)
                {
                    Cache["Approved"] = cboStatus.Items.FindByText(APPROVED).Value;
                }
                //67811A0 - START -PCI Remediation for Payment systems End: Added to fix the Object reference exception by cognizant on 1/11/2012
                //Modified by Cognizant on 04/21/2005 to Show the "Approver's" list only when the Status is "Approved"
                if (cboStatus.SelectedItem.Value == Cache["Approved"].ToString())
                {
                    lblApprovers.Visible = true;
                    _Approvers.Visible = true;
                }
                else
                {
                    lblApprovers.Visible = false;
                    _Approvers.Visible = false;
                }
                //END
            //}
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.rptCashierApproveRepeater.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.rptCashierApproveRepeater_ItemCommand);

        }
        #endregion

        #region FillReport
        /// <summary>
        ///  Filling the Report Types
        /// </summary>
        private void FillReport()
        {
            DataSet dtsReportTypes = TurninService.GetTurnInReportTypes(CurrentUser, "W");
            cboReportType.DataSource = dtsReportTypes.Tables[0];
            cboReportType.DataBind();
            //MAIG - CH1 - BEGIN - Modified the below code to default the cashier reconciliation report
            foreach (ListItem item in cboReportType.Items)
            {
                if (item.Text.ToLower().Equals("cashier reconciliation"))
                {
                    item.Selected = true;
                    break;
                }
            }
            cboReportType.Enabled = false;
            //MAIG - CH1 - END - Modified the below code to default the cashier reconciliation report
        }

        #endregion

        #region FillStatus
        /// <summary>
        ///  Filling the Status Combo and Assinging the Status to the Cache
        /// </summary>
        private void FillStatus()
        {
            DataSet dtsStatus = TurninService.GetStatus(CurrentUser, cboReportType.SelectedItem.Value.ToString());
            cboStatus.DataSource = dtsStatus.Tables[0];
            cboStatus.DataBind();
            if (Cache["Pending Approval"] == null)
            {
                Cache["Pending Approval"] = cboStatus.Items.FindByText(PENDING_APPROVAL).Value;
            }
            if (Cache["Approved"] == null)
            {
                Cache["Approved"] = cboStatus.Items.FindByText(APPROVED).Value;
            }
        }
        #endregion

        #region cboStatus_SelectedIndexChanged

        /// <summary>
        ///  When the selected item in the status drop down is changed
        /// </summary>
        protected void cboStatus_SelectedIndexChanged(object sender, System.EventArgs e)
        {
			//MAIG - CH2 - BEGIN - Modified the condition to include Null check also
            if((!(string.IsNullOrEmpty(Cache["Approved"].ToString())) && (cboStatus.SelectedItem.Value == Cache["Approved"].ToString())))
			//MAIG - CH2 - END - Modified the condition to include Null check also
            {
                    //PC Phase II changes CH1 - Start - Modified the below code to handle Date Time Picker
                    TextstartDt.Visible = true;
                    TextendDt.Visible = true;
                    Dates.Visible = true;
                    image.Visible = true;
                    image1.Visible = true;

                    //PC Phase II changes CH2 - Start - Modified the below code to Handle Date Time Picker
                    DateTime now = DateTime.Now;
                    DateTime startdate = new DateTime(now.Year, now.Month, 1);
                    TextstartDt.Text = startdate.ToString("MM-dd-yyyy HH:mm:ss");
                    DateTime enddate = DateTime.Today;
                    TextendDt.Text = enddate.ToString("MM-dd-yyyy") + " 23:59:59";
                    //PC Phase II changes CH2 - End - Modified the below code to Handle Date Time Picker
            }
            else
            {
                TextstartDt.Visible = false;
                TextendDt.Visible = false;
                Dates.Visible = false;
                image.Visible = false;
                image1.Visible = false;
            }
            //PC Phase II changes CH1 - End - Modified the below code to handle Date Time Picker
        }

        #endregion

        #region LoadPendingApprovalDetails
        /// <summary>
        ///  To Load the Pending Approval,Approved Details for the Selected Users based on the Selected Report Type
        /// </summary>
        private void LoadPendingApprovalDetails()
        {
            OrderClasses.ReportCriteria rptCrit1 = new OrderClasses.ReportCriteria();
            //Preparing the criteria for retrieving data
            rptCrit1.Status_Link_ID = cboStatus.SelectedItem.Value;
            rptCrit1.ReportType = Convert.ToInt16(cboReportType.SelectedItem.Value);
            //CHG0110069 - CH1 - BEGIN - Appended the /Time along with the Date Label
            lblDateRange.Text = "Run Date/Time:";
            //CHG0109406 - CH1 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
            //TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time  by cognizant.
            lblRunDate.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + " " + "Arizona";

            lblDate.Text = "Date/Time Range:";
            //CHG0110069 - CH1 - END - Appended the /Time along with the Date Label
            //TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time  by cognizant.
            //PC Phase II changes CH1 - Start - Modified the below code to handle Date Time Picker
            lblDateR.Text = TextstartDt.Text + " - " + TextendDt.Text + " " + "Arizona";
            //PC Phase II changes CH1 - End - Modified the below code to handle Date Time Picker
            //CHG0109406 - CH1 - END - Modified the timezone from "MST Arizona" to Arizona
            lblReportStatusName.Text = "Report Status:";
            lblReportStatus.Text = cboStatus.SelectedItem.Text.ToString();
            lblRepDo.Text = "Rep DO:";
            lblRep.Text = _RepDO.SelectedItem.Text;
			//MAIG - CH3 - BEGIN - Modified the condition to include Null check also
            if (!(string.IsNullOrEmpty(Cache["Approved"].ToString())))
			//MAIG - CH3 - END - Modified the condition to include Null check also
            {                
                    trApprover.Visible = true;
                    lblApprover.Text = _Approvers.SelectedItem.Text.Replace(" - " + _Approvers.SelectedItem.Value, "");
                    rptCrit1.Approver = _Approvers.SelectedItem.Value;
            }
            else
                trApprover.Visible = false;
            rptCrit1.RepDO = _RepDO.SelectedItem.Value;
            rptCrit1.Users = GetUsersList();
            rptCrit1.CurrentUser = Page.User.Identity.Name;

            //Check the Length of the Selected Users to not more than 2000
            if (rptCrit1.Users.Length > 2000)
            {
                rptLib.SetMessage(lblErrMsg, "The total length of users should not exceed 2000 characters", true);
                InvisibleUserInfo();
                return;
            }

            //Check the Dates are visible else a default date is passed
            //PC Phase II changes CH1 - Start - Modified the below code to handle Date Time Picker
            if ((TextstartDt.Visible == true) | (TextendDt.Visible == true ))
            {
                trDateRange.Visible = true;
                rptCrit1.StartDate = Convert.ToDateTime(TextstartDt.Text);
                rptCrit1.EndDate = Convert.ToDateTime(TextendDt.Text);
                Session["DateRange"] = lblDateR.Text;
                
            }
            //PC Phase II changes CH1 - End - Modified the below code to handle Date Time Picker
            else
            {
                trDateRange.Visible = false;
                DateTime StartDate = new DateTime(1900, 1, 1);
                DateTime EndDate = new DateTime(1900, 1, 1);
                rptCrit1.StartDate = StartDate;
                rptCrit1.EndDate = EndDate;
                Session["DateRange"] = null;
            }
            //Q4-Retrofit.Ch2 -START 
            //Modified the code to call the Renamed function "PAYWorkflowReport"
            DataSet dtsData = TurninService.PayWorkflowReport(rptCrit1);
            //dgCrossReference.DataSource = dtsData;
            //dgCrossReference.DataBind();
            //test.DataSource = dtsData;
            //test.DataBind();
            //Q4-Retrofit.Ch2 -END
            if (bReject == false)
            {
                //To Hide the Reject Confirmation Caption the below two rows are made to visible false
                trRejectLableRow.Visible = false;
                trRejectRow.Visible = false;
            }
            else
            {
                //To Display the label confirmation caption when reject button is clicked
                trRejectLableRow.Visible = true;
                trRejectRow.Visible = true;
            }
            if (dtsData.Tables[0].Rows.Count == 0)
            {
                rptLib.SetMessage(lblErrMsg, "No Data Found. Please specify a valid criteria.", true);
                InvisibleUserInfo();
                return;
            }
            else
            {
                //Checking for the Selected Report Type
                if (cboReportType.SelectedItem.Value == Cache["Cashier Report Type"].ToString())
                {
                    rptCashierApproveRepeater.DataSource = dtsData;
                    rptCashierApproveRepeater.DataBind();
                    Session["PendingApproval"] = dtsData;
                    // For displaying the collection summary
                    btnUpdateTotal_Click();
                }
                else if (cboReportType.SelectedItem.Value == Cache["Member Report Type"].ToString())
                {
                    rptMembershipDetail.DataSource = dtsData;
                    rptMembershipDetail.DataBind();
                    Session["PendingApproval"] = dtsData;
                    VisibleUserInfo(0);
                }
                else if (cboReportType.SelectedItem.Value == Cache["Cross Reference Report Type"].ToString())
                {
                    Session["PendingApproval"] = dtsData;
                    //.NetMig 3.5.Ch2 code change done by Cognizant on 8/13/2010
                    //dgCrossReference.CurrentPageIndex = 0;
                    dgCrossReference.PageIndex = 0;
                    UpdateDataView();
                    VisibleUserInfo(0);
                }
                //To Put the selected Values in the Session for PrintXls
                Session["RunDate"] = lblRunDate.Text;
                Session["ReportStatus"] = lblReportStatus.Text;
                Session["ReportType"] = cboReportType.SelectedItem.Value.ToString();
                Session["Users"] = lblCsrs.Text;
                Session["RepDO"] = _RepDO.SelectedItem.Text;
                //Added by Cognizant on 04/20/2005 to fill the ApprovedBy value in the session.
                Session["ApprovedBy"] = _Approvers.SelectedItem.Text.Replace(" - " + _Approvers.SelectedItem.Value, "");
                //Security Defects -CH1 - Added the below code to add session values to the context.
                Context.Items.Add("ApprovedBy", Session["ApprovedBy"]);
                Context.Items.Add("ReportType", Session["ReportType"]);
            }
            // Making it to default Status
            bReject = false;
        }

        #endregion

        #region InvisibleUserInfo

        /// <summary>
        ///  Making the controls invisible
        /// </summary>
        private void InvisibleUserInfo()
        {
            rptCashierApproveRepeater.Visible = false;
            tblMemberRepeater.Visible = false;
            tblReportInfo.Visible = false;
            trConvertPrint.Visible = false;
            trCaption.Visible = false;
            trBlankRow1.Visible = false;
            trBlankRow2.Visible = false;
            trBlankRow3.Visible = false;
            trCollSummarySplit.Visible = false;
            trCollSummary.Visible = false;
            trOtherCollSummary.Visible = false;
            trOtherCollSummarySplit.Visible = false;
            tblCrossReference.Visible = false;
            lblPageNum1.Visible = false;
            lblPageNum2.Visible = false;
        }
        #endregion

        #region VisibleUserInfo
        /// <summary>
        ///  Making the controls visible based on the Report Type
        /// </summary>
        private void VisibleUserInfo(double dblTotal)
        {
            if (cboReportType.SelectedItem.Value == Cache["Cashier Report Type"].ToString())
            {
                rptCashierApproveRepeater.Visible = true;
                tblMemberRepeater.Visible = false;
                tblCrossReference.Visible = false;
                lblPageNum1.Visible = false;
                lblPageNum2.Visible = false;
                if (cboStatus.SelectedItem.Value == Cache["Pending Approval"].ToString() && dblTotal > 0)
                    trCaption.Visible = true;
                trBlankRow1.Visible = true;
                trBlankRow2.Visible = true;
                trBlankRow3.Visible = true;
            }
            else if (cboReportType.SelectedItem.Value == Cache["Member Report Type"].ToString())
            {
                rptCashierApproveRepeater.Visible = false;
                tblMemberRepeater.Visible = true;
                lblPageNum1.Visible = false;
                lblPageNum2.Visible = false;
                tblCrossReference.Visible = false;

            }
            else if (cboReportType.SelectedItem.Value == Cache["Cross Reference Report Type"].ToString())
            {
                rptCashierApproveRepeater.Visible = false;
                tblMemberRepeater.Visible = false;
                lblPageNum1.Visible = true;
                lblPageNum2.Visible = true;
                tblCrossReference.Visible = true;
            }
            tblReportInfo.Visible = true;
            trConvertPrint.Visible = true;
        }

        #endregion

        #region rptCashierApproveRepeater_ItemBound

        /// <summary>
        /// When the Items are bounded to the Repeater Control 
        /// The NewRejectedVisibility(e) is Called to Check the Existence of the Check box
        /// The AlternatingColour(e) is Called to Change Alternating Color based on the ReceiptNumber
        /// The CalculateTotal(e) is Called to the Update the Total for Selected Items
        /// </summary>

        protected void rptCashierApproveRepeater_ItemBound(Object Sender, RepeaterItemEventArgs e)
        {
            NewRejectedVisibility(e);
            AlternatingColour(e);
            CalculateTotal(e);
        }

        #endregion

        #region NewRejectedVisibility

        /// <summary>
        ///  Making the Checkbox invisible for Approved status
        /// </summary>
        private void NewRejectedVisibility(RepeaterItemEventArgs e)
        {
            HtmlTableCell tdTemp;
			//MAIG - CH4 - BEGIN - Modified the condition to include Null check also
            if ((!(string.IsNullOrEmpty(Cache["Approved"].ToString())) && (cboStatus.SelectedItem.Value == Cache["Approved"].ToString())))
            {
			//MAIG - CH4 - END - Modified the condition to include Null check also

                    tblRepeater.Width = "750";
                    ImageButton btnTemp;
                    if (e.Item.ItemType == ListItemType.Header)
                    {
                        tdTemp = (HtmlTableCell)e.Item.FindControl("tdChkHead");
                        tdTemp.Visible = false;
                        tdTemp = (HtmlTableCell)e.Item.FindControl("tdHeader1");
                        //CHG0109406 - CH2 - BEGIN - Modified the colspan value from 11 to 7 to accomodate the TimeZone field
                        tdTemp.ColSpan = 7;
                        //CHG0109406 - CH2 - END - Modified the colspan value from 11 to 7 to accomodate the TimeZone field
                    }
                    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        tdTemp = (HtmlTableCell)e.Item.FindControl("tdChkItem");
                        tdTemp.Visible = false;
                    }
                    if (e.Item.ItemType == ListItemType.Footer)
                    {
                        btnTemp = (ImageButton)e.Item.FindControl("btnUpdateButton");
                        btnTemp.Visible = false;
                        btnTemp = (ImageButton)e.Item.FindControl("btnReject");
                        btnTemp.Visible = false;
                        btnTemp = (ImageButton)e.Item.FindControl("btnApprove");
                        btnTemp.Visible = false;
                    }
                ////}
            }
            else
            {
                tblRepeater.Width = "750";
                if (e.Item.ItemType == ListItemType.Header)
                {
                    tdTemp = (HtmlTableCell)e.Item.FindControl("tdApprovedByHead");
                    tdTemp.Visible = false;
                    tdTemp = (HtmlTableCell)e.Item.FindControl("tdApprovedDateHead");
                    tdTemp.Visible = false;
                    tdTemp = (HtmlTableCell)e.Item.FindControl("tdHeader1");
                    //CHG0109406 - CH3 - BEGIN - Modified the colspan value from 10 to 6 to accomodate the TimeZone field
                    tdTemp.ColSpan = 6;
                    //CHG0109406 - CH3 - END - Modified the colspan value from 10 to 6 to accomodate the TimeZone field
                }
                if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                {
                    tdTemp = (HtmlTableCell)e.Item.FindControl("tdApprovedByItem");
                    tdTemp.Visible = false;
                    tdTemp = (HtmlTableCell)e.Item.FindControl("tdApprovedDateItem");
                    tdTemp.Visible = false;
                }

            }
        }

        #endregion

        #region AlternatingColour

        /// <summary>
        /// Displaying the details of the same receipt number in a single colour
        /// Making the Receipt Date,Status, Member Name, checkbox of same receipt number invisible
        /// </summary>
        private void AlternatingColour(RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableRow tr = (HtmlTableRow)e.Item.FindControl("trItemRow");
                Label lblTempReceiptDate = (Label)e.Item.FindControl("lblReceiptDate");
                lblTempReceiptDate.Text = Convert.ToDateTime(lblTempReceiptDate.Text).ToString("MM/dd/yyyy hh:mm:ss tt");
                Label lblTempStatus = (Label)e.Item.FindControl("lblStatus");
                Label lblTempMemberName = (Label)e.Item.FindControl("lblMemberName");
                Label lblTempReceiptNo = (Label)e.Item.FindControl("lblReceiptNo");
                CheckBox cbTempSelect = (CheckBox)e.Item.FindControl("cbSelect");
                string strTempReceiptNo = lblTempReceiptNo.Attributes["value"].ToString();
				//MAIG - CH5 - BEGIN - Modified the condition to include Null check also
                if ((!(string.IsNullOrEmpty(Cache["Approved"].ToString())) && (cboStatus.SelectedItem.Value == Cache["Approved"].ToString())))
				//MAIG - CH5 - END - Modified the condition to include Null check also
                {
                    ////if (cboStatus.SelectedItem.Value == Cache["Approved"].ToString())
                    ////{
                        Label lblTempApprovedDate = (Label)e.Item.FindControl("lblApprovedDate");
                        lblTempApprovedDate.Text = Convert.ToDateTime(lblTempApprovedDate.Text).ToString("MM/dd/yyyy hh:mm:ss tt");
                    ////}
                }
                //Assigning different colour if the receipt numbers are different
                if (strTempReceiptNo != tmpreceipt)
                {
                    AssignClass(colour, tr);
                }
                else
                {
                    tr.Attributes.Add("class", colour);
                    lblTempReceiptDate.Visible = false;
                    lblTempStatus.Visible = false;
                    lblTempMemberName.Visible = false;
                    cbTempSelect.Visible = false;
                    cbTempSelect.Checked = false;
                }
                tmpreceipt = strTempReceiptNo;
            }
        }

        #endregion

        #region AssignClass

        /// <summary>
        /// Swapping of the colour between ALTERNATE_ROW_CLASS and ROW_CLASS
        /// </summary>
        private void AssignClass(string tmpcolour, HtmlTableRow tr)
        {
            if (tmpcolour == ROW_CLASS)
            {
                tr.Attributes.Add("class", ALTERNATE_ROW_CLASS);
                colour = ALTERNATE_ROW_CLASS;
            }
            else if (tmpcolour == ALTERNATE_ROW_CLASS)
            {
                tr.Attributes.Add("class", ROW_CLASS);
                colour = ROW_CLASS;
            }
        }

        #endregion

        #region CalculateTotal
        /// <summary>
        /// Calculating the total of all the items in the Repeater
        /// </summary>
        private void CalculateTotal(RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Footer)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    //Adding the OnClick attribute for Approve button
                    ImageButton ibTempSubmit = (ImageButton)e.Item.FindControl("btnApprove");
                    ibTempSubmit.Attributes.Add("OnClick", "return confirmApprove();");

                    //Added the OnClick attribute for Reject button
                    ImageButton ibTempReject = (ImageButton)e.Item.FindControl("btnReject");
                    ibTempReject.Attributes.Add("OnClick", "return confirmReject();");

                    Label lblTempAmount = (Label)e.Item.FindControl("lblTotal");
                    lblTempAmount.Text = dblTotal.ToString("$##0.00");
                    //Changing the width of the repeater if status is Approved / Pending Approval
					//MAIG - CH6 - BEGIN - Modified the condition to include Null check also
                    if ((!(string.IsNullOrEmpty(Cache["Approved"].ToString())) && (cboStatus.SelectedItem.Value == Cache["Approved"].ToString())))
                    {
					//MAIG - CH6 - END - Modified the condition to include Null check also
                        ////if (cboStatus.SelectedItem.Value == Cache["Approved"].ToString())
                        ////{
                            HtmlTableCell tctotal = (HtmlTableCell)e.Item.FindControl("tdTotalHead");
                            tctotal.Attributes.Add("colspan", "10");
                            tctotal.Attributes.Add("InnerText", "Total");
                        ////}
                    }
                    return;
                }
                //Total all the individual amounts
                HtmlTableCell tdTempAmount = (HtmlTableCell)e.Item.FindControl("tdAmount");
                string strAmount = tdTempAmount.InnerText;
                dblTotal = dblTotal + Convert.ToDouble(strAmount.Replace("$", "").Replace(",", ""));

            }
        }

        #endregion

        #region rptCashierApproveRepeater_ItemCommand
        /// <summary>
        /// Determining which button is clicked (Update,Reject,Approve)
        /// </summary>
        private void rptCashierApproveRepeater_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Footer)
            {
                if (e.CommandName == "UpdateButton")
                {
                    btnUpdateTotal_Click();
                    return;
                }
                if (e.CommandName == "Approve")
                {
                    btnApprove_Click();
                    return;
                }
                if (e.CommandName == "Reject")
                {
                    btnReject_Click();
                    return;
                }
            }
        }

        #endregion

        #region btnReject_Click
        /// <summary>
        /// To Reject the Selected Receipt number by calling the Receipts_Reject
        /// </summary>
        private void btnReject_Click()
        {
            int index;
            int NoOfSelectedReceipts = 0;
            string selreceipts = "";
            for (index = 0; index < rptCashierApproveRepeater.Items.Count; index++)
            {
                // Obtaining the receipt numbers of all the items which is checked
                if (rptCashierApproveRepeater.Items[index].ItemType == ListItemType.Item || rptCashierApproveRepeater.Items[index].ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox cbTempSelect = (CheckBox)rptCashierApproveRepeater.Items[index].FindControl("cbSelect");
                    Label lblTempReceiptNo = (Label)rptCashierApproveRepeater.Items[index].FindControl("lblReceiptNo");
                    if (cbTempSelect.Checked == true)
                    {
                        selreceipts = selreceipts + lblTempReceiptNo.Attributes["value"].ToString() + ",";
                        NoOfSelectedReceipts = NoOfSelectedReceipts + 1;
                    }
                }
            }
            if (selreceipts.Length > 0 && NoOfSelectedReceipts <= 200)
            {
                //Removing the last Comma for receipt
                selreceipts = selreceipts.Remove(selreceipts.Length - 1, 1);
                //Getting the Values for the Parameters Passed
                OrderClasses.ReportCriteria rptCrit1 = new OrderClasses.ReportCriteria();
                rptCrit1.CurrentUser = CurrentUser;
                rptCrit1.ReceiptList = selreceipts;
                //Changing the status for the receipt numbers which has been turned in
                TurninService.CashierReject(rptCrit1);
                bReject = true;
                LoadPendingApprovalDetails();
            }
            else
            {
                //To Check Atleast one ReceiptNumber is Selected Or not more than 200 Receipts is Selected
                if (selreceipts.Length <= 0)
                {
                    rptLib.SetMessage(lblErrMsg, "Please Select atleast one  Receipt Number.", true);
                }
                else if (NoOfSelectedReceipts > 200)
                {
                    rptLib.SetMessage(lblErrMsg, "You currently have " + NoOfSelectedReceipts +
                    " transactions in the batch. You may Reject only a  maximum of 200 transactions" +
                    " at one time. Please uncheck some of the transactions and Reject them later.", true);
                }
                VisibleUserInfo(0);
                return;
            }
        }
        #endregion

        #region btnApprove_Click
        /// <summary>
        /// To Approve the Selected Receipt number by calling the Receipts_Approve
        /// </summary>
        private void btnApprove_Click()
        {
            int index;
            int NoOfSelectedReceipts = 0;
            string selreceipts = "";
            for (index = 0; index < rptCashierApproveRepeater.Items.Count; index++)
            {
                // Obtaining the receipt numbers of all the items which is checked
                if (rptCashierApproveRepeater.Items[index].ItemType == ListItemType.Item || rptCashierApproveRepeater.Items[index].ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox cbTempSelect = (CheckBox)rptCashierApproveRepeater.Items[index].FindControl("cbSelect");
                    Label lblTempReceiptNo = (Label)rptCashierApproveRepeater.Items[index].FindControl("lblReceiptNo");
                    if (cbTempSelect.Checked == true)
                    {
                        selreceipts = selreceipts + lblTempReceiptNo.Attributes["value"].ToString() + ",";
                        NoOfSelectedReceipts = NoOfSelectedReceipts + 1;
                    }
                }
            }
            if (selreceipts.Length > 0 && NoOfSelectedReceipts <= 200)
            {
                PrepareSummaryData();
                //Removing the last Comma for receipt
                selreceipts = selreceipts.Remove(selreceipts.Length - 1, 1);
                //Getting the Values for the Parameters Passed
                OrderClasses.ReportCriteria rptCrit1 = new OrderClasses.ReportCriteria();
                rptCrit1.CurrentUser = CurrentUser;
                rptCrit1.ReceiptList = selreceipts;
                //Changing the status for the receipt numbers which has been turned in
                TurninService.CashierApprove(rptCrit1);
                Session["CashierCSAAGrid"] = dtbRepeater;
                Session["CashierOtherGrid"] = dtbOtherRepeater;
                Session["CashierTotalGrid"] = dtbTotalRepeater;
                Session["UserID"] = CurrentUser;
                Response.Redirect("CashierRecon_Confirmation.aspx");
            }
            else
            {
                //To Check Atleast one ReceiptNumber is Selected Or not more than 200 Receipts is Selected
                if (selreceipts.Length <= 0)
                {
                    rptLib.SetMessage(lblErrMsg, "Please Select atleast one  Receipt Number.", true);
                }
                else if (NoOfSelectedReceipts > 200)
                {
                    rptLib.SetMessage(lblErrMsg, "You currently have " + NoOfSelectedReceipts +
                        " transactions in the batch. You may Approve only a  maximum of 200 transactions" +
                        " at one time. Please uncheck some of the transactions and Approve them later.", true);
                }
                VisibleUserInfo(0);
                return;
            }
        }

        #endregion

        #region  cboReportType_SelectedIndexChanged

        /// <summary>
        /// To Fill the Status based on the Report Type
        /// </summary>
        protected void cboReportType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            FillStatus();
            cboStatus_SelectedIndexChanged(sender, e);
            //Show the "Approver's" list only when the Status is "Approved"
			//MAIG - CH7 - BEGIN - Modified the condition to include Null check also
            if ((!(string.IsNullOrEmpty(Cache["Approved"].ToString())) && (cboStatus.SelectedItem.Value == Cache["Approved"].ToString())))
			//MAIG - CH7 - END - Modified the condition to include Null check also
            {
                ////if (cboStatus.SelectedItem.Value == Cache["Approved"].ToString())
                ////{
                    lblApprovers.Visible = true;
                    _Approvers.Visible = true;
                ////}
            }
            else
            {
                lblApprovers.Visible = false;
                _Approvers.Visible = false;
            }
        }

        #endregion

        #region btnUpdateTotal_Click
        /// <summary>
        /// Create the Collection summary and displays for the Selected Items it
        /// </summary>
        private void btnUpdateTotal_Click()
        {
            PrepareSummaryData();
            //CHG0083477 - Name Change from AAA NCNU IE to CSAA IG as part of Name change project on 11/15/2013.
            //NameChange.ch1-Changed the name CSAA to AAA NCNU IE
            //CHG0104053 - PAS HO CH1 - START : Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/16/2014
            CollectionSummary.CreateHeader(ref tblCollSummary, ref dtbRepeater, "TURN-IN PRODUCTS");
            //CHG0104053 - PAS HO CH1 - END : Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/16/2014
            CollectionSummary.AddCollSummaryData(ref tblCollSummary, ref dtbRepeater);
            double dblTotal = CollectionSummary.AddCollSummaryFooter(ref tblCollSummary, ref dtbRepeater);
            //To Check the Total column Value > 0 
            if (dblTotal > 0)
            {
                trCollSummarySplit.Visible = true;
                trCollSummary.Visible = true;
            }
            else
            {
                trCollSummarySplit.Visible = false;
                trCollSummary.Visible = false;
            }
            //CHG0104053 - PAS HO CH2 - START : Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/16/2014
            CollectionSummary.CreateHeader(ref tblOtherCollSummary, ref dtbOtherRepeater, "WU/ACA PRODUCTS");
            //CHG0104053 - PAS HO CH2 - END : Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/16/2014
            CollectionSummary.AddCollSummaryData(ref tblOtherCollSummary, ref dtbOtherRepeater);
            dblTotal = CollectionSummary.AddCollSummaryFooter(ref tblOtherCollSummary, ref dtbOtherRepeater);
            //To Check the Sub Total column Value > 0 
            if (dblTotal > 0)
            {
                trOtherCollSummary.Visible = true;
                trOtherCollSummarySplit.Visible = true;
            }
            else
            {
                trOtherCollSummary.Visible = false;
                trOtherCollSummarySplit.Visible = false;
            }
            CollectionSummary.CreateTotalHeader(ref tblTotalCollSummary, ref dtbTotalRepeater);
            CollectionSummary.AddCollTotalSummaryData(ref tblTotalCollSummary, ref dtbTotalRepeater);
            dblTotal = CollectionSummary.AddCollTotalSummaryFooter(ref tblTotalCollSummary, ref dtbTotalRepeater);
            VisibleUserInfo(dblTotal);
        }

        #endregion

        #region PrepareSummaryData

        /// <summary>
        /// Creates Data  into the Repeater Table
        /// </summary>
        private void PrepareSummaryData()
        {
            CreateSummaryTable();
            string tmpreceipt_select = "";
            string cacheApproved = string.Empty;
            foreach (RepeaterItem item in rptCashierApproveRepeater.Items)
            {
                //Selecting the details of items which are checked
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox chkSelect = (CheckBox)item.FindControl("cbSelect");
                    Label lblTempReceiptNo = (Label)item.FindControl("lblReceiptNo");
                    if (chkSelect.Checked == true)
                    {
                        tmpreceipt_select = lblTempReceiptNo.Attributes["value"].ToString();
                    }
                    //Q4-Retrofit.Ch3 : Changed the code for getting constant value from Application object (web.config changes)
                    //Insert values into the temporary data table (Summary Table)if the item is Checked / Pending Approval / Approved
                    if (Cache["Approved"] != null)
                    {
                        cacheApproved = Cache["Approved"].ToString();
                    }
                    if ((tmpreceipt_select == lblTempReceiptNo.Attributes["value"].ToString()) || cboStatus.SelectedItem.Value == cacheApproved)
                    {
                        HtmlTableCell tdTempAmount = (HtmlTableCell)item.FindControl("tdAmount");
                        string strAmount = tdTempAmount.InnerText;
                        HtmlTableCell tdTempPymtType = (HtmlTableCell)item.FindControl("tdPymtType");
                        HtmlTableCell tdTempProdType = (HtmlTableCell)item.FindControl("tdProdType");
                        HtmlTableCell tdTempTransType = (HtmlTableCell)item.FindControl("tdTransType");
                        //CHG0104053 - PAS HO CH3 - START : Added the below code to get the Policy number details for validation 7/3/2014
                        HtmlTableCell tdpolicyno = (HtmlTableCell)item.FindControl("tdpolicynumber");
                        int length = tdpolicyno.InnerText.Trim().Length;
                        string prodtype = tdTempProdType.InnerText.Trim();
                        //CHG0104053 - PAS HO CH3 - END : Added the below code to get the Policy number details for validation 7/3/2014
                        //string strUploadType = "," + Config.Setting("UploadType.CSAAProducts") + ",";
                        string strUploadType = "," + Convert.ToString(((StringDictionary)Application["Constants"])["UploadType.CSAAProducts"]) + ",";
                        if (strUploadType.IndexOf("," + tdTempProdType.Attributes["UploadType"].ToString() + ",") >= 0)
                        {
                            //Q4-Retrofit.Ch1-START: Added the code to display "Payments" as Revenue Type for HUON products 
                            //in the CSAA Collection Summary Grid 
                            string strTransType = tdTempTransType.InnerText;
							//MAIG - CH8 - BEGIN - Modified the code to support the common product types
                            //CHG0104053 - PAS HO CH4- BEGIN : Modified the below code to group under PAS Auto, PAS Home 7/02/2014
                            if (length == 13 && (prodtype.ToLower().Equals("auto")))
                            {
                                InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "PAS Auto", strTransType, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                            }
                            else
                            {                               
                                
                                /*if (tdTempProdType.Attributes["UploadType"].ToString() == Convert.ToString(((StringDictionary)Application["Constants"])["UploadType.HUON"]))
                                {
                                    //Getting the Revenue Type for HUON Products from the config 
                                    //strTransType = 	Config.Setting("RevenueTypeDescription.HUON") ;
                                    strTransType = Convert.ToString(((StringDictionary)Application["Constants"])["RevenueTypeDescription.HUON"]);
                                }*/
                                if (length == 13 && prodtype.ToLower().Equals("home"))
                                {
                                    InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "PAS Home", strTransType, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));

                                    ////////// PAS HO CH42-Added to show HDES policy summary both under 'PAS HOME' & 'HODE/HDES'
                                    ////if(length == 7 && prodtype.ToLower().Equals("homeowners"))
                                    ////    InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, tdTempProdType.InnerText, strTransType, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                                }
                                else if (length == 7 && prodtype.ToLower().Equals("home"))
                                {
                                    InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "Home/HDES", strTransType, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                                }
                                else
                                {
                                    //Modified the code to pass strTransType as Parameter instead of tdTempTransType.InnerText
                                    //MAIG  - Modified the below line ref type paramter name from dtbSummary to dtbOtherSummary to display in WU/ACA Products Collection 11/25/2014
                                    InsertSummaryTable(ref dtbOtherSummary, tdTempPymtType.InnerText, Convert.ToString(((StringDictionary)Application["Constants"])["ProductTypeHeader.WU_ACA"]), strTransType, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                                    //Q4-Retrofit.Ch1-END
                                }
                                //CHG0104053 - PAS HO CH4- END : Modified the below code to group under PAS Auto, PAS Home 7/02/2014
                            }
                        }
                        else
                        {
                            if (length == 13 && (prodtype.ToLower().Equals("dwelling fire") || prodtype.ToLower().Equals("personal umbrella")))
                            {
                                InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "PAS Home", tdTempTransType.InnerText, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                            }
                            else
                            {
                                //For Other Products Collection Summary
                                //InsertSummaryTable(ref dtbOtherSummary,tdTempPymtType.InnerText,Config.Setting("ProductTypeHeader.WU_ACA").ToString(),tdTempTransType.InnerText,Convert.ToDecimal(strAmount.Replace("$","").Replace(",","")));
                                InsertSummaryTable(ref dtbOtherSummary, tdTempPymtType.InnerText, Convert.ToString(((StringDictionary)Application["Constants"])["ProductTypeHeader.WU_ACA"]), tdTempTransType.InnerText, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                            }
							//MAIG - CH8 - END - Modified the code to support the common product types
                        }

                        InsertTotalTable(ref dtbTotal, tdTempPymtType.InnerText, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                    }
                    //Q4-Retrofit.Ch3 : END - Changed the code for getting constant value from Application object (web.config changes)
                }
            }

            dtbSummary.DefaultView.Sort = "PayType,ProdType,RevType";
            dtbOtherSummary.DefaultView.Sort = "PayType,ProdType,RevType";
            dtbTotal.DefaultView.Sort = "PayType";
            //MAIG - CH9 - BEGIN - Added code to Sort PayType to get PAS Auto,PAS Home,Home/HDES, WU/ACA, Homeowners and Membership Products in order.
            DataTable mdtbsummary = dtbSummary.Clone();
            List<DataRow> lstpas = dtbSummary.Select("ProdType = 'PAS Auto'").ToList();
            List<DataRow> lstpasHome = dtbSummary.Select("ProdType = 'PAS Home'").ToList();
            List<DataRow> lsthome = dtbSummary.Select("ProdType = 'Home/HDES'").ToList();
            List<DataRow> lstWU_ACA = dtbSummary.Select("ProdType = 'WU/ACA'").ToList();
            if (lstpas.Count > 0)
            {
                foreach (DataRow item in lstpas)
                {
                    mdtbsummary.Rows.Add(item.ItemArray);
                }
            }
            if (lstpasHome.Count > 0)
            {
                foreach (DataRow item in lstpasHome)
                {
                    mdtbsummary.Rows.Add(item.ItemArray);
                }
            }
            if (lsthome.Count > 0)
            {
                foreach (DataRow item in lsthome)
                {
                    mdtbsummary.Rows.Add(item.ItemArray);
                }
            }
            if (lstWU_ACA.Count > 0)
            {
                foreach (DataRow item in lstWU_ACA)
                {
                    mdtbsummary.Rows.Add(item.ItemArray);
                }
            }
            dtbSummary = mdtbsummary.Copy();

            CreateRepeaterTable();
            DataView rowView = new DataView(dtbSummary, "", "", DataViewRowState.CurrentRows);
            DataView colListView = new DataView(dtbSummary, "", "", DataViewRowState.CurrentRows);
            //MAIG - CH9 - END - Added code to Sort PayType to get PAS Auto,PAS Home,Home/HDES, WU/ACA, Homeowners and Membership Products in order.
            DataView rowViewOther = new DataView(dtbOtherSummary, "", "PayType,ProdType,RevType", DataViewRowState.CurrentRows);
            DataView colListViewOther = new DataView(dtbOtherSummary, "", "ProdType,RevType", DataViewRowState.CurrentRows);
            DataView rowViewTotal = new DataView(dtbTotal, "", "PayType", DataViewRowState.CurrentRows);
            DataView colListViewTotal = new DataView(dtbTotal, "", "", DataViewRowState.CurrentRows);
            // Creating header for the repeater in the format ProdType|RevType 
            foreach (DataRowView dr in colListView)
            {
                AddColumnToRepeaterTable(dr["ProdType"].ToString() + "|" + dr["RevType"].ToString(), ref dtbRepeater);
            }
            // Creating header for the repeater in the format ProdType|RevType for other products
            foreach (DataRowView dr in colListViewOther)
            {
                AddColumnToRepeaterTable(dr["ProdType"].ToString() + "|" + dr["RevType"].ToString(), ref dtbOtherRepeater);
            }
            //Inserting data in the Repeater table from the summary table
            foreach (DataRowView dr in rowView)
            {
                InsertRepeaterTable(ref dtbRepeater, dr["PayType"].ToString(), dr["ProdType"].ToString(), dr["RevType"].ToString(), Convert.ToDecimal(dr["Amount"].ToString()));
            }
            //Inserting data in the Repeater table from the other product summary table
            foreach (DataRowView dr in rowViewOther)
            {
                InsertRepeaterTable(ref dtbOtherRepeater, dr["PayType"].ToString(), dr["ProdType"].ToString(), dr["RevType"].ToString(), Convert.ToDecimal(dr["Amount"].ToString()));
            }
            AddColumnToRepeaterTable("Amount", ref dtbTotalRepeater);
            //Inserting data in the Repeater table Total
            foreach (DataRowView dr in rowViewTotal)
            {
                InsertRepeaterTotalTable(ref dtbTotalRepeater, dr["PayType"].ToString(), Convert.ToDecimal(dr["Amount"].ToString()));
            }
        }

        #endregion

        #region CreateSummaryTable
        /// <summary>
        /// Temporary Data Table (Summary Table) is created having the columns PayType,ProdType,RevType and Amount 
        /// with PayType,ProdType,RevType as combined primary key
        /// </summary>
        private void CreateSummaryTable()
        {
            dtbSummary = new DataTable();
            dtbSummary.Columns.Add("PayType");
            dtbSummary.Columns.Add("ProdType");
            dtbSummary.Columns.Add("RevType");
            dtbSummary.Columns.Add("Amount", System.Type.GetType("System.Decimal"));
            dtbSummary.PrimaryKey = new DataColumn[] { dtbSummary.Columns["PayType"], dtbSummary.Columns["ProdType"], dtbSummary.Columns["RevType"] };
            //Created for Storing the Other Products Summary Table
            dtbOtherSummary = new DataTable();
            dtbOtherSummary.Columns.Add("PayType");
            dtbOtherSummary.Columns.Add("ProdType");
            dtbOtherSummary.Columns.Add("RevType");
            dtbOtherSummary.Columns.Add("Amount", System.Type.GetType("System.Decimal"));
            dtbOtherSummary.PrimaryKey = new DataColumn[] { dtbOtherSummary.Columns["PayType"], dtbOtherSummary.Columns["ProdType"], dtbOtherSummary.Columns["RevType"] };
            //Create for Storing the Total Summary Table
            dtbTotal = new DataTable();
            dtbTotal.Columns.Add("PayType");
            dtbTotal.Columns.Add("Amount", System.Type.GetType("System.Decimal"));
            dtbTotal.PrimaryKey = new DataColumn[] { dtbTotal.Columns["PayType"] };
        }
        #endregion

        #region CreateRepeaterTable
        /// <summary>
        /// Temporary Data Table (Repeater Table) is created with the column PayType
        /// </summary>
        private void CreateRepeaterTable()
        {
            dtbRepeater = new DataTable();
            dtbRepeater.Columns.Add("PayType");
            //create Data table to have other product info
            dtbOtherRepeater = new DataTable();
            dtbOtherRepeater.Columns.Add("PayType");
            //Create Data table to have Total Amount
            dtbTotalRepeater = new DataTable();
            dtbTotalRepeater.Columns.Add("PayType");
        }
        #endregion

        #region AddColumnToRepeaterTable
        /// <summary>
        /// Adding a column to the repeater table
        /// </summary>
        private void AddColumnToRepeaterTable(string strColName, ref DataTable dtbReferer)
        {
            if (!dtbReferer.Columns.Contains(strColName))
            {
                DataColumn dtCol = new DataColumn(strColName, System.Type.GetType("System.Decimal"));
                dtCol.DefaultValue = Convert.ToDecimal(0);
                dtbReferer.Columns.Add(dtCol);
            }
        }
        #endregion

        #region InsertRepeaterTotalTable

        /// <summary>
        /// Inserting values to the repeater table from the Total summary table
        /// </summary>
        private void InsertRepeaterTotalTable(ref DataTable dtbReferer, string inPaytype, decimal inAmount)
        {
            string strExpression = string.Empty;
            DataRow dtrRepeater;
            DataRow[] dtrArrar;
            strExpression = strExpression + "PayType = '" + inPaytype + "'";
            dtrArrar = dtbReferer.Select(strExpression);
            //Inserting the payment type as a new row in case it is not already inserted
            if (dtrArrar.GetLength(0) == 0)
            {
                dtrRepeater = dtbReferer.NewRow();
                dtrRepeater["PayType"] = inPaytype;
                dtrRepeater["Amount"] = inAmount;
                dtbReferer.Rows.Add(dtrRepeater);
            }
            //Placing the amount in case the column is already present
            else
            {
                dtrArrar[0]["Amount"] = Convert.ToDecimal(inAmount);
            }
        }

        #endregion

        #region InsertRepeaterTable
        /// <summary>
        /// Inserting values to the repeater table from the summary table
        /// </summary>
        private void InsertRepeaterTable(ref DataTable dtbReferer, string inPaytype, string inProdtype, string inRevtype, decimal inAmount)
        {
            string strExpression = string.Empty;
            DataRow dtrRepeater;
            DataRow[] dtrArrar;
            strExpression = strExpression + "PayType = '" + inPaytype + "'";
            dtrArrar = dtbReferer.Select(strExpression);
            //Inserting the payment type as a new row in case it is not already inserted
            if (dtrArrar.GetLength(0) == 0)
            {
                dtrRepeater = dtbReferer.NewRow();
                dtrRepeater["PayType"] = inPaytype;
                dtrRepeater[inProdtype + "|" + inRevtype] = inAmount;
                dtbReferer.Rows.Add(dtrRepeater);
            }
            //Placing the amount in case the column is already present
            else
            {
                dtrArrar[0][inProdtype + "|" + inRevtype] = Convert.ToDecimal(inAmount);
            }
        }

        #endregion

        #region InsertTotalTable
        /// <summary>
        /// Inserting values into the Total Table
        /// </summary>
        private void InsertTotalTable(ref DataTable dtbTemp, string inPaytype, decimal inAmount)
        {
            DataRow dtrSummary;
            DataRow[] dtrArrar;
            inPaytype = inPaytype.Trim();
            string strExpression = string.Empty;
            strExpression = strExpression + "PayType = '" + inPaytype + "'";
            dtrArrar = dtbTemp.Select(strExpression);
            if (dtrArrar.GetLength(0) == 0)
            {
                dtrSummary = dtbTemp.NewRow();
                dtrSummary["PayType"] = inPaytype;
                dtrSummary["Amount"] = inAmount;
                dtbTemp.Rows.Add(dtrSummary);
            }
            else
            {
                dtrArrar[0]["Amount"] = Convert.ToDecimal(dtrArrar[0]["Amount"].ToString()) + Convert.ToDecimal(inAmount);
            }
        }

        #endregion

        #region InsertSummaryTable

        /// <summary>
        /// Inserting values into the Summary table
        /// </summary>
        private void InsertSummaryTable(ref DataTable dtbTemp, string inPaytype, string inProdtype, string inRevtype, decimal inAmount)
        {
            DataRow dtrSummary;
            DataRow[] dtrArrar;
            inPaytype = inPaytype.Trim();
            inProdtype = inProdtype.Trim();
            //MAIG - CH10 - BEGIN - Commeneted the un-used Product Types
            //PAS HO CH40 - START : Added the below code to change the label from HomeOwner to Home/HDES 7/3/2014
            //if (inProdtype.ToLower().Equals("homeowners"))
            //{
            //    inProdtype = "Home/HDES";
            //}
            //PAS HO CH40 - END : Added the below code to change the label from HomeOwner to Home/HDES 7/3/2014
            inRevtype = inRevtype.Trim();
            //For Revenue Type 'Earned Premium' it is changed to 'Installment Payment'
            if (inRevtype.ToUpper() == EARNED_PREMIUM)
            {
                inRevtype = "Installment Payment";
            }
            /*
            if (inProdtype.ToUpper() == MEMBER)
            {
                inProdtype = "Membership";
            }*/
            //MAIG - CH10 - END - Commeneted the un-used Product Types
            string strExpression = string.Empty;
            strExpression = strExpression + "PayType = '" + inPaytype + "'" + " and ProdType = '" + inProdtype + "'" + " and RevType = '" + inRevtype + "'";
            dtrArrar = dtbTemp.Select(strExpression);
            // If there is no row having the same PayType, ProdType, Revenue Type, add a new row
            if (dtrArrar.GetLength(0) == 0)
            {
                dtrSummary = dtbTemp.NewRow();
                dtrSummary["PayType"] = inPaytype;
                dtrSummary["ProdType"] = inProdtype;
                dtrSummary["RevType"] = inRevtype;
                dtrSummary["Amount"] = inAmount;
                dtbTemp.Rows.Add(dtrSummary);
            }
            // If there is a row having the same PayType, ProdType, Revenue Type update the amount alone
            else
            {
                dtrArrar[0]["Amount"] = Convert.ToDecimal(dtrArrar[0]["Amount"].ToString()) + Convert.ToDecimal(inAmount);
            }
        }

        #endregion

        #region _RepDO_SelectedIndexChanged

        /// <summary>
        /// To Fill the userList for Selected RepDO
        /// </summary>
        protected void _RepDO_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            rptLib.FillUserListBox(UserList, CurrentUser, "All", "-1", _RepDO.SelectedItem.Value, true, true);
            rptLib.FillApproversDropDown(_Approvers, _RepDO.SelectedItem.Value);
            //Set the Default Selected Item in Approver's DropDown as the Login User
            setSelectedIndex(_Approvers, User.Identity.Name);
        }

        #endregion

        #region GetUsersList

        /// <summary>
        /// To Get the selected User List in Comma Separated values
        /// </summary>
        private string GetUsersList()
        {
            string lstUsers = "";
            string lstNames = "";
            lblCsr.Text = "";
            lblCsrs.Text = "";
            if (UserList.SelectedIndex > -1)
            {
                foreach (ListItem li in UserList.Items)
                {
                    if (li.Selected == true)
                    {
                        lstNames = lstNames + li.Text.Replace(" - " + li.Value, "; ");
                        lstUsers = lstUsers + li.Value + ',';
                    }
                }
                if (lstUsers == "All,")
                {
                    lstNames = "All";
                }
                lblCsr.Text = "Created By:";
                lblCsrs.Text = lstNames;
            }
            else
            {
                lstUsers = "All,";
            }
            return lstUsers;
        }

        #endregion

        #region btnSubmit_Click

        /// <summary>
        /// To View the Status report based on the Selected Criteria
        /// </summary>
        protected void btnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {
               //PC Phase II changes CH1 - Start - Modified the below code to handle Date Time Picker
                if ((TextstartDt.Visible == true) && (TextendDt.Visible == true))
                {
                    bool bValid = rptLib.ValidateDate(lblErrMsg, Convert.ToDateTime(TextstartDt.Text), Convert.ToDateTime(TextendDt.Text), -1);
                //PC Phase II changes CH1 - End - Modified the below code to handle Date Time Picker  
                    if (bValid == true)
                        return;
                }
                LoadPendingApprovalDetails();
            }
            catch (FormatException f)
            {
                Logger.Log(f);
                string invalidDate = @"<Script>alert('Select a valid date.')</Script>";
                Response.Write(invalidDate);
            }
        }

        #endregion

        #region rptMembershipDetail_ItemBound
        /// <summary>
        /// Display of the items in the Membership Detail Data Repeater
        /// </summary>
        protected void rptMembershipDetail_ItemBound(Object Sender, RepeaterItemEventArgs e)
        {
            AlternatingColourMembership(e);
            CalculateTotalMembership(e);
        }
        #endregion

        #region AlternatingColourMembership
        /// <summary>
        /// Displaying the details of the same order ID in a single colour
        /// Making the Address,City,State,Zip,Day Phone,Eve Phone,Mbr Number,User(s),Src Code,Base Year,
        /// Prod Type,Effective Date,Amount, PymtType of same order ID invisible for the Associates
        /// </summary>
        private void AlternatingColourMembership(RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
            {
                HtmlTableRow trTempItemRowMember = (HtmlTableRow)e.Item.FindControl("trItemRowMember");
                Label lblTempEffectiveDate = (Label)e.Item.FindControl("lblEffectiveDate");
                //TimeZoneChange.Ch2-Commented to display only date and not time as a part of Time Zone Change  by cognizant.
                //  lblTempEffectiveDate.Text = Convert.ToDateTime(lblTempEffectiveDate.Text.ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");bhanu
                Label lblTempOrderID = (Label)e.Item.FindControl("lblOrderID");
                Label lblTempAddress = (Label)e.Item.FindControl("lblAddress");
                Label lblTempCity = (Label)e.Item.FindControl("lblCity");
                Label lblTempState = (Label)e.Item.FindControl("lblState");
                Label lblTempZip = (Label)e.Item.FindControl("lblZip");
                Label lblTempDayPhone = (Label)e.Item.FindControl("lblDayPhone");
                Label lblTempEvePhone = (Label)e.Item.FindControl("lblEvePhone");
                Label lblTempMemberNumber = (Label)e.Item.FindControl("lblMemberNumber");
                Label lblTempMemberUsers = (Label)e.Item.FindControl("lblMemberUsers");
                Label lblTempSrcCode = (Label)e.Item.FindControl("lblSrcCode");
                Label lblTempBaseYear = (Label)e.Item.FindControl("lblBaseYear");
                Label lblTempProdType = (Label)e.Item.FindControl("lblProdType");
                Label lblTempAmount = (Label)e.Item.FindControl("lblAmount");
                Label lblTempPymtType = (Label)e.Item.FindControl("lblPymtType");
                string strTempOrderID = lblTempOrderID.Attributes["value"].ToString();
                Label lblTempApprovedDate = (Label)e.Item.FindControl("lblMbrApprovedDate");
                if ((lblTempApprovedDate.Text != "") && ((lblTempApprovedDate.Text != null)))
                    lblTempApprovedDate.Text = Convert.ToDateTime(lblTempApprovedDate.Text.ToString()).ToString("MM/dd/yyyy hh:mm:ss tt");

                //Assigning different colour if the Order IDs are different
                if (strTempOrderID != tmporderid)
                {
                    AssignClass(colour, trTempItemRowMember);
                }
                else
                {
                    trTempItemRowMember.Attributes.Add("class", colour);
                    lblTempEffectiveDate.Visible = false;
                    lblTempAddress.Visible = false;
                    lblTempCity.Visible = false;
                    lblTempState.Visible = false;
                    lblTempZip.Visible = false;
                    lblTempDayPhone.Visible = false;
                    lblTempEvePhone.Visible = false;
                    lblTempMemberNumber.Visible = false;
                    lblTempMemberUsers.Visible = false;
                    lblTempSrcCode.Visible = false;
                    lblTempBaseYear.Visible = false;
                    lblTempProdType.Visible = false;
                    lblTempAmount.Visible = false;
                    lblTempAmount.Text = "0";
                    lblTempPymtType.Visible = false;
                }
                tmporderid = strTempOrderID;
            }
            //START Modified by Cognizant on 04/21/2005 to Show the ApprovedBy and ApprovedDate Columns depending upon the Status
			//MAIG - CH11 - BEGIN - Modified the condition to include Null check also
            if ((!(string.IsNullOrEmpty(Cache["Approved"].ToString())) && (cboStatus.SelectedItem.Value != Cache["Approved"].ToString())))
            {
			//MAIG - CH11 - END - Modified the condition to include Null check also
                ////if (cboStatus.SelectedItem.Value != Cache["Approved"].ToString())
                ////{
                    HtmlTableCell tdTemp;

                    if (e.Item.ItemType == ListItemType.Header)
                    {
                        tdTemp = (HtmlTableCell)e.Item.FindControl("tdMbrApprovedByHead");
                        tdTemp.Visible = false;
                        tdTemp = (HtmlTableCell)e.Item.FindControl("tdMbrApprovedDateHead");
                        tdTemp.Visible = false;
                        tdTemp = (HtmlTableCell)e.Item.FindControl("Td1");
                        tdTemp.ColSpan = 20;
                    }
                    if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
                    {
                        tdTemp = (HtmlTableCell)e.Item.FindControl("tdMbrApprovedByItem");
                        tdTemp.Visible = false;
                        tdTemp = (HtmlTableCell)e.Item.FindControl("tdMbrApprovedDateItem");
                        tdTemp.Visible = false;
                    }
                ////}
            }
            //END
        }

        #endregion

        #region CalculateTotalMembership
        /// <summary>
        /// Calculating the total amount of all the items in the Membership Repeater
        /// </summary>
        private void CalculateTotalMembership(RepeaterItemEventArgs e)
        {
            if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Footer)
            {
                if (e.Item.ItemType == ListItemType.Footer)
                {
                    Label lblTempTotalMember = (Label)e.Item.FindControl("lblTotalMember");
                    lblTempTotalMember.Text = dblTotalMember.ToString("$##0.00");
                    //Changing the width of the repeater if status is Approved / Pending Approval
					//MAIG - CH12 - BEGIN - Modified the condition to include Null check also
                    if ((!(string.IsNullOrEmpty(Cache["Approved"].ToString())) && (cboStatus.SelectedItem.Value != Cache["Approved"].ToString())))
                    {
					//MAIG - CH12 - END - Modified the condition to include Null check also
                        ////if (cboStatus.SelectedItem.Value != Cache["Approved"].ToString())
                        ////{
                            HtmlTableCell tctotal = (HtmlTableCell)e.Item.FindControl("tdTotalMemberHead");
                            tctotal.Attributes.Add("colspan", "18");
                            tctotal.Attributes.Add("InnerText", "Total");
                        ////}
                    }
                    return;
                }
                //Total all the individual amounts
                Label lblTempAmount = (Label)e.Item.FindControl("lblAmount");
                lblTempAmount.Text = lblTempAmount.Text.Replace(",", "");
                string strAmountMember = lblTempAmount.Text;
                dblTotalMember = dblTotalMember + Convert.ToDouble(strAmountMember.Replace("$", ""));
            }
        }
        #endregion

        #region CrossReferencePageIndexChanged

        /// <summary>
        /// To Show the Next Page Or the Previous Page Details 
        /// </summary>
        //.NetMig 3.5.Ch3 code change done by Cognizant on 8/13/2010
        public void CrossReferencePageIndexChanged(Object sender, GridViewPageEventArgs e)
        {
            //dgCrossReference.CurrentPageIndex=e.NewPageIndex;
            dgCrossReference.PageIndex = e.NewPageIndex;
            UpdateDataView();
        }
        #endregion

        #region UpdateDataView

        /// <summary>
        /// called to Display Page Numbers
        /// </summary>

        private void UpdateDataView()
        {
            // Retrieves the data
            DataSet dtsTempRepeaterData = (DataSet)Session["PendingApproval"];
            if (dtsTempRepeaterData == null)
            {
                rptLib.SetMessage(lblErrMsg, "Your session has timed out. Please re-submit the page", true);
                return;
            }
            DataView dtvRepeaterData = dtsTempRepeaterData.Tables[0].DefaultView;
            int pageCnt = 1;
            int currPage = 1;
            // Re-bind data 
            if (dtsTempRepeaterData.Tables[0].Rows.Count > 0)
            {
                VisibleUserInfo(0);
                //.NetMig 3.5.Ch1 : Start:Added and Modified by cognizant as a part of .NetMig 3.5 on 08-12-2010 for migration on data grid to grid view.
                DataTable dt;
                DataView dv;
                dt = dtsTempRepeaterData.Tables[0];
                if (dt.Columns[0].ToString() == "Receipt Number")
                {
                    dt.Columns.Remove("Receipt Number");



                }




                dv = new DataView(dt);

                dgCrossReference.DataSource = dtsTempRepeaterData;  //dtvRepeaterData;

                dgCrossReference.DataBind();




                pageCnt = dgCrossReference.PageCount;
                //currPage = dgCrossReference.CurrentPageIndex+1;
                currPage = dgCrossReference.PageIndex + 1;
                if (pageCnt < dgCrossReference.PageCount) pageCnt = dgCrossReference.PageCount;
                //if (currPage < dgCrossReference.CurrentPageIndex+1) currPage = dgCrossReference.CurrentPageIndex+1;
                if (currPage < dgCrossReference.PageIndex + 1) currPage = dgCrossReference.PageIndex + 1;
            }

            if (pageCnt > 1)
            {
                dgCrossReference.PagerSettings.Visible = true;
            }
            if (pageCnt < 2)
            {
                dgCrossReference.PagerSettings.Visible = false;
                //dgCrossReference.PagerStyle.Visible = false;
            }
            lblPageNum1.Text = "Showing Page " + currPage.ToString() + " of " + pageCnt.ToString();
            lblPageNum2.Text = "Showing Page " + currPage.ToString() + " of " + pageCnt.ToString();

            if (currPage == 1)
            {
                // CHG0088851 - Penetration defects - Included condition for Cross Reference Report Link button availablity.
                if (dgCrossReference.BottomPagerRow != null && dgCrossReference.TopPagerRow != null)
                {
                    System.Web.UI.WebControls.LinkButton PageIndicatorLinkPrevBottom = (System.Web.UI.WebControls.LinkButton)dgCrossReference.BottomPagerRow.FindControl("vPrev");
                    System.Web.UI.WebControls.LinkButton PageIndicatorLinkPrevTop = (System.Web.UI.WebControls.LinkButton)dgCrossReference.TopPagerRow.FindControl("vPrev");
                    if (PageIndicatorLinkPrevBottom != null && PageIndicatorLinkPrevTop != null)
                    {
                        PageIndicatorLinkPrevBottom.Enabled = false;
                        PageIndicatorLinkPrevTop.Enabled = false;
                    }
                }
            }
            if (currPage == pageCnt)
            {
                // CHG0088851 - Penetration defects - Included condition for Cross Reference Report Link button availablity.
                if (dgCrossReference.BottomPagerRow != null && dgCrossReference.TopPagerRow != null)
                {
                    System.Web.UI.WebControls.LinkButton PageIndicatorLinkNextBottom = (System.Web.UI.WebControls.LinkButton)dgCrossReference.BottomPagerRow.FindControl("vNext");
                    System.Web.UI.WebControls.LinkButton PageIndicatorLinkNextTop = (System.Web.UI.WebControls.LinkButton)dgCrossReference.TopPagerRow.FindControl("vNext");
                    if (PageIndicatorLinkNextBottom != null && PageIndicatorLinkNextTop != null)
                    {
                        PageIndicatorLinkNextBottom.Enabled = false;
                        PageIndicatorLinkNextTop.Enabled = false;
                    }
                }
            }
            //End:Added and Modified by cognizant as a part of .NetMig 3.5 on 08-12-2010 for migration on data grid to grid view.

        }
        #endregion

        #region SetCrossReferenceGridWidth
        /// <summary>
        /// Setting the width of the columns in the Cross Reference Data Grid
        /// </summary>
        //.NetMig 3.5.Ch4 code change done by Cognizant on 8/13/2010
        protected void SetCrossReferenceGridWidth(GridViewRowEventArgs e)
        {



            e.Row.Cells[2].Width = 110;
            e.Row.Cells[3].Width = 120;
            e.Row.Cells[4].Width = 110;
            e.Row.Cells[5].Width = 134;
            e.Row.Cells[6].Width = 30;
        }
        #endregion

        #region dgCrossReference_ItemBound
        /// <summary>
        /// Display of the items in the Cross Reference Data Grid
        /// </summary>
        protected void dgCrossReference_ItemBound(Object Sender, GridViewRowEventArgs e)
        {//.NetMig 3.5.Ch5 Start:Added and Modified by cognizant as a part of .NetMig 3.5 on 08-12-2010 for migration on data grid to grid view.
            foreach (TableRow row in dgCrossReference.Rows)
            {

                row.Cells[0].Width = new Unit(110, UnitType.Pixel);
                row.Cells[1].Width = new Unit(120, UnitType.Pixel);
                row.Cells[2].Width = new Unit(110, UnitType.Pixel);
                row.Cells[3].Width = new Unit(134, UnitType.Pixel);
                row.Cells[4].Width = new Unit(110, UnitType.Pixel);
                row.Cells[5].Width = new Unit(30, UnitType.Pixel);

            }

            //ColourCrossReferenceReport(e);
            //SetCrossReferenceGridWidth(e);
            //End:Added by cognizant as a part of .NetMig 3.5 on 08-12-2010 for migration on data grid to grid view.
        }
        #endregion

        #region setSelectedIndex
        /// <summary>
        /// To Set the RepDo of the Current User as default
        /// </summary>
        /// <param name="cboDropDown">DropdownList Name</param>
        /// <param name="Value">The Value to the RepDO</param>
        private void setSelectedIndex(System.Web.UI.WebControls.DropDownList cboDropDown, string Value)
        {
            int i = 0;
            int Count = 0;
            Count = cboDropDown.Items.Count;
            while (i < Count)
            {
                if (Value == cboDropDown.Items[i].Value)
                {
                    cboDropDown.SelectedIndex = i;
                    return;
                }
                i = i + 1;
            }
        }
        #endregion

        #region ColourCrossReferenceReport
        /// <summary>
        /// Displaying the details of the Transaction in a single colour
        /// Making the Receipt Date/Time, Membership Number, Users, Rep DO visible only for the
        /// first line item in a transaction for Cross Reference Report
        /// </summary>
        protected void ColourCrossReferenceReport(GridViewRowEventArgs e)
        {
            ////.NetMig 3.5.Ch6 Start:Added and Modified by cognizant as a part of .NetMig 3.5 on 08-12-2010 for migration on data grid to grid view.
            //e.Row.Cells[0].Visible = false;


            if (e.Row.RowType == DataControlRowType.DataRow)
            // if (e.Row.ItemType == ListItemType.Item || e.Row.ItemType == ListItemType.AlternatingItem)
            //End:Added and Modified by cognizant as a part of .NetMig 3.5 on 08-12-2010 for migration on data grid to grid view.
            {
                DataRowView drvItem = (DataRowView)e.Row.DataItem;
                string strTempReceiptNumber = drvItem["Receipt Number"].ToString();

                if (strTempReceiptNumber != tmpreceiptnum)
                {
                    AssignCrossReferenceClass(crosscolour, e);
                }
                else
                {
                    e.Row.Attributes.Add("class", crosscolour);

                    e.Row.Cells[1].Text = "";
                    e.Row.Cells[2].Text = "";
                    e.Row.Cells[5].Text = "";
                    e.Row.Cells[6].Text = "";
                }
                tmpreceiptnum = strTempReceiptNumber;
            }
        }
        #endregion

        #region AssignCrossReferenceClass
        /// <summary>
        /// Swapping of the colour between ALTERNATE_ROW_CLASS and ROW_CLASS for Cross Reference Report
        /// </summary>
        private void AssignCrossReferenceClass(string tmpcolour, GridViewRowEventArgs e)
        {//.NetMig 3.5.Ch7 code change done by Cognizant on 8/13/2010
            if (tmpcolour == ROW_CLASS)
            {
                e.Row.Attributes.Add("class", ALTERNATE_ROW_CLASS);
                crosscolour = ALTERNATE_ROW_CLASS;
            }
            else if (tmpcolour == ALTERNATE_ROW_CLASS)
            {
                e.Row.Attributes.Add("class", ROW_CLASS);
                crosscolour = ROW_CLASS;
            }
        }
        #endregion


    }
}