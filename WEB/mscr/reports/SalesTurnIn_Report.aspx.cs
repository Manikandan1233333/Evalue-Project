
/*
 *	History:
 *	11/11/2004 JOM Replaced InsuranceClasses.Service.Insurance with TurninClasses.Service.Turnin.
 *			  Also removed multiple creations of this service.
 *	REVISION HISTORY:
 *	MODIFIED BY COGNIZANT
 *	07/19/2005 - For changing the web.config entries pointing to database. This changes 
 *				 are made as part of CSR #3937 implementation.
 *	Changes Done:
 *	CSR#3937.Ch1 : Include the namespace for accessing the StringDictionary object
 *	CSR#3937.Ch2 : Changed the code for getting constant value from Application object (web.config changes)
 *	CSR#3824.Ch1 : Changed the code to clear up the session["ReissueInfo"] for the new void/reissue confirmation page
 *		
 *	MODIFIED BY COGNIZANT AS  PART OF Q4 RETROFIT CHANGES
 * 
 *  12/1/2005 Q4-Retrofit.Ch1 : Modified the code to Seperate collection summary grid by CSAA
 *		 product and Other Products.There is also Total Collection summary grid provided 
 *  12/1/2005 Q4-Retrofit.Ch2 : Modified the Code to display Revenue Type as "Payments" for
 *		 HUON Product in  CSAA Collection Summary Grid 
 *  12/1/2005 Q4-Retrofit.Ch3 : Modified the code to call the Renamed Function "PAYWorkflowReport"
 *  12/8/2005 Q4-Retrofit.Ch4 : Changed the code for getting constant value from Application object (web.config changes)
 * 
 *	MODIFIED BY COGNIZANT AS  PART OF Q1 RETROFIT CHANGES
 *  04/25/2006 Q1-Retrofit.Ch1 : Modified the code to display the header "PAYMENT TYPE" in
 *	 CSAA PRODUCTS summary grid.
 *	 MODIFIED BY COGNIZANT AS PART OF NAME CHANGE PROJECT ON on 08/19/2010
 *	 NameChange.Ch1:Changed the name CSAA to AAA NCNU IE .
 *	 
 *	 MODIFIED AS A PART OF TIME ZONE CHANGE ENHANCEMENT ON 8-31-2010
 * TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time  by cognizant.
 * TimeZoneChange.Ch2-Commented to display only date and not time as a part of Time Zone Change  by cognizant.
 * SalesTurnin_Report Changes.Ch1 : Include the namespace for accessing the List objects.
 * SalesTurnin_Report Changes.Ch2 : Commented tdTransType since it is nomore used
 * SalesTurnin_Report Changes.Ch3 : Modified the code to fetch PAS Auto Summary Separately.
 * SalesTurnin_Report Changes.Ch4 : Added code to Sort PayType to get HUON Auto, Auto Non-Converted, PAS Auto, Homeowners and Membership Products in order.
 * Security Defect CH1,ch2- Added the belowcode to assign the session values to the context.
 * SSO Integration - CH1 Start -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
 * PC Phase II changes CH1 - Modified the ImageButton "btnTempVoidReissue" to "btnTempVoid".
 * PC Phase II changes CH2 - Modified the below code to handle Date Time Picker.
 * CHG0083477 - Name Change from AAA NCNU IE to CSAA IG as part of Name change project on 11/15/2013.
 * CHG0104053 - PAS HO CH1 - Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/13/2014
 * CHG0104053 - PAS HO CH2 - Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/13/2014
 * CHG0104053 - PAS HO CH3-  Modified the below code to group the AAA SS Auto under PAS Auto 7/02/2014
 * CHG0104053 - PAS HO CH4- Added the below code to group the AAA SS Home and CSAA IG Home Condo under PAS Home 7/02/2014                          
 * CHG0104053 - PAS HO CH5- Added the below code to filter only the PAS Home policies 7/02/2014
 * CHG0104053 - PAS HO CH6 - Added the below code to change the label from HomeOwner to Home/HDES 6/16/2014
 * CHG0104053 - PAS HO CH7 - Added the below code to add the PAS Home policies 7/02/2014
 * CHG0104053 - PAS HO CH8 - Added the below code to change the label from HomeOwner to Home/HDES 6/13/2014
 * MAIG - CH1 - Added code to default the Sales collection report
 * MAIG - CH2 - Modified code to support teh Common Product Type Auto and Home
 * MAIG - CH3 - Added code to Sort PayType to get PAS Auto,PAS Home,Home/HDES, WU/ACA, Homeowners and Membership Products in order.
 * MAIG - CH4 - commeneted the un-used Product Types
 * CHG0109406 - CH1 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH2 - Modified the colspan value from 9 to 6 to accomodate the TimeZone field
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
using MSCR.Reports;
using CSAAWeb;
using CSAAWeb.AppLogger;
// PC Phase II changes CH1 - Modified the below code to handle Date Time Picker
//CSR#3937.Ch1 : START - Include the namespace for accessing the StringDictionary object
using System.Collections.Specialized;
//CSR#3937.Ch1 : END - Include the namespace for accessing the StringDictionary object

//SalesTurnin_Report Changes.Ch1 :START - Include the namespace for accessing the List objects.
using System.Linq;
using System.Collections.Generic;
//SalesTurnin_Report Changes.Ch1 :END - Include the namespace for accessing the List objects.


namespace MSCR.Reports
{
	/// <summary>
	/// This screen is used by the Sales Rep to Turn-In particular transactions and to View and Print 
	/// Sales Collection List, Membership Detail Report and Cross Reference Report
	/// </summary>
	//Declarations

	public partial class SalesTurnIn_Report : CSAAWeb.WebControls.PageTemplate
	{
		//protected MSCR.Controls.Dates Dates;
		protected System.Data.DataTable dtbSummary;
		protected System.Data.DataTable dtbRepeater;
		string CurrentUser;
		const string ROW_CLASS = "item_template_row";
		const string ALTERNATE_ROW_CLASS = "item_template_alt_row";
		const string NEW_REJECTED_STATUS_DESC="New and Rejected";
		const string PENDING_APPROVAL_STATUS_DESC="Pending Approval";
		const string APPROVED_STATUS_DESC="Approved";
		const string MEMBERSHIP="Membership";
		const string EARNED_PREMIUM="Earned Premium";
		const string MEMBER="Mbr";
		const string INSTALLMENT_PAYMENT="Installment Payment";
		//Q4-Retrofit.Ch1-START :Modified the Constant from "0" to "1"
		const int UPLOAD_TO_POES=1;
		//Q4-Retrofit.Ch1 -END
		
		const int MAX_ALLOWED_TRANSACTION = 200;
		string colour = ALTERNATE_ROW_CLASS;
		string tmpreceipt="";
		string tmporderid="";
		// Variable to store the Receipt Number of the previous transaction in the Cross Reference Report datagrid
		string tmpreceiptnum = "";

		// Variable to store the colour of the current row in the Cross Reference Report datagrid
		string crosscolour = ALTERNATE_ROW_CLASS;

		double dblTotal=0;
		TurninClasses.Service.Turnin TurninService;
		protected System.Web.UI.HtmlControls.HtmlAnchor A3;
		CollectionSummaryLibrary CollectionSummary  = new CollectionSummaryLibrary();
        //Begin PC Phase II changes CH1 - Modified the ImageButton "btnTempVoidReissue" to "btnTempVoid".
		ImageButton btnTempVoid;
        //End PC Phase II changes CH1 - Modified the ImageButton "btnTempVoidReissue" to "btnTempVoid".
		InsRptLibrary rptLib = new InsRptLibrary();
		// START - The Report Type codes are being stored as Constants
		const string SALESTURNIN_REPORT_TYPE="1";
		const string MEMBERSHIP_REPORT_TYPE="2";
		const string CROSSREFERENCE_REPORT_TYPE="3";
		// END
		protected System.Web.UI.WebControls.Label lblMemberUsers;
		Hashtable hsReportType = new Hashtable();
		double dblTotalMember=0;

		//Q4-Retrofit.Ch1-START
		//Added Declarations for creating Other Product and Total Collection Summary Grids
		protected System.Data.DataTable dtbOtherSummary;
		protected System.Data.DataTable dtbTotal;
		protected System.Data.DataTable dtbOtherRepeater;
        protected System.Data.DataTable dtbTotalRepeater;
        //Q4-Retrofit.Ch1 :END
	
		
		
		#region Page_Load
		protected void Page_Load(object sender, System.EventArgs e)
		{
            // SSO Integration - CH1 Start -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            // SSO Integration - CH1 End -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
            ////Security Defect - CH1 -START Added the below code to verify if the IP address of the login page and current page is same

        //    string ipaddr1 = Request.ServerVariables["REMOTE_ADDR"];
        //    if (CSAAWeb.AppLogger.Logger.Sourcereg(ipaddr1))
        //    {
        //        ipaddr1 = Request.ServerVariables["REMOTE_ADDR"];
        //    }
        //    else
        //    {
        //        ipaddr1 = CSAAWeb.AppLogger.Logger.GetIPAddress();
        //    }
        //    if (Request.Cookies["AuthToken"] == null)
        //    {
        //        Logger.Log(CSAAWeb.Constants.SEC_COOKIE_NULL);
        //        Response.Redirect("/PaymentToolmscr/Forms/login.aspx");

        //    }
        //    else if (!ipaddr1.Equals(
        //        Request.Cookies["AuthToken"].Value))
        //    {
        //        Logger.Log(CSAAWeb.Constants.SEC_COOKIE_NOTEQUAL);
        //        Response.Redirect("/PaymentToolmscr/Forms/login.aspx"); ;

        //    }
        //    //Security Defect - CH1 -End Added the below code to verify if the IP address of the login page and current page is same
        //    else{
			//For initialising the Collection Summary Width to zero in the PostBack
			tblCollSummary.Attributes.Add("Width","0");
			//Q4-Retrofit.Ch1-START: Added the code to initialize Other Product Collection Summary width to zero on PostBack
			tblOtherCollSummary.Attributes.Add("width","0");
			//Q4-Retrofit.Ch1-END
			CurrentUser = Page.User.Identity.Name;
			TurninService=new TurninClasses.Service.Turnin();
			lblErrMsg.Text="";
			if (!Page.IsPostBack)
			{
				//Filling Report Types
				FillReport();
				//Filling the Status Combo
				FillStatus();
				//Filling RepDO and user
				FillRepUser();
				//Displaying the data on the grid
				SubmitSalesCollection();
			}
			else
			{
				//Making the Button and UserInfo invisible
				InvisibleUserInfo();
			}
			//Assigning the Report Type IDs to the Report Type Description using Hash Table
			for(int indexReportType=0;indexReportType<cboReportType.Items.Count;indexReportType++)
			{
				hsReportType[cboReportType.Items[indexReportType].Value] = cboReportType.Items[indexReportType].Value;
			}
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
			this.rptSalesTurnInRepeater.ItemCommand += new System.Web.UI.WebControls.RepeaterCommandEventHandler(this.rptSalesTurnInRepeater_ItemCommand);

		}
		#endregion
		
		#region FillUserInfo
		/// <summary>
		/// Populating the UserInfo (Run Date, Report Status, User, Rep DO
		/// </summary>
		private void FillUserInfo()
		{
            //CHG0109406 - CH1 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
            //TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time  by cognizant.
            lblRunDate.Text = DateTime.Now.ToString("MM/dd/yyyy  hh:mm:ss tt") + " " + "Arizona";
			lblReportStatus.Text=cboStatus.SelectedItem.Text.ToString();
			lblUsers.Text=lblUser.Text;
			lblRep.Text=lblRepDO.Text;
			if(cboStatus.SelectedItem.Value == Cache[APPROVED_STATUS_DESC].ToString())
			{
				trDateRange.Visible=true;
                //PC Phase II changes CH2 - Start - Modified the below code to handle Date Time Picker
                lblDateRange.Text = Convert.ToDateTime(TextstartDt.Text) + " - ";
                //TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time  by cognizant.
                lblDateRange.Text = lblDateRange.Text + Convert.ToDateTime(TextendDt.Text) + " " + "Arizona";
				//PC Phase II changes CH2 - End - Modified the below code to handle Date Time Picker
                //CHG0109406 - CH1 - END - Modified the timezone from "MST Arizona" to Arizona
                Session["DateRange"]=lblDateRange.Text;
			}
			else
			{
				trDateRange.Visible=false;
				Session["DateRange"]=null;
			}
		}
		#endregion

		#region FillRepUser
		/// <summary>
		/// Filling RepDO and user
		/// </summary>
		private void FillRepUser()
		{
            try
            {
                AuthenticationClasses.Service.Authentication Auth = new AuthenticationClasses.Service.Authentication();
                DataSet dtsRepIDDO = Auth.GetRepIDDO(CurrentUser);
                lblRepID.Text = dtsRepIDDO.Tables[0].Rows[0]["DO ID"].ToString();
                string RepDO = dtsRepIDDO.Tables[0].Rows[0]["DO Description"].ToString() + "  -  " + dtsRepIDDO.Tables[0].Rows[0]["DO ID"].ToString();
                string Users = "  " + dtsRepIDDO.Tables[0].Rows[0]["Last Name"].ToString() + ", " + dtsRepIDDO.Tables[0].Rows[0]["First Name"].ToString() + " (" + dtsRepIDDO.Tables[0].Rows[0]["Rep ID"].ToString() + ") - " + CurrentUser.ToUpper();
                lblRepDO.Text = RepDO;
                lblUser.Text = Users;
            }
            catch (Exception ex)
            {
                Logger.Log("Error Occurred while fetching REPIDDO for UserID: " + CurrentUser + " . Error Details: " + ex.ToString());                
            }
		}
		#endregion

		#region FillReport
		/// <summary>
		/// Filling Report Types
		/// </summary>
		private void FillReport()
		{
			DataSet dtsReportTypes = TurninService.GetTurnInReportTypes(CurrentUser,"W");
			cboReportType.DataSource=dtsReportTypes.Tables[0];
			cboReportType.DataTextField=dtsReportTypes.Tables[0].Columns["Description"].ToString();
			cboReportType.DataValueField =dtsReportTypes.Tables[0].Columns["ID"].ToString();
			cboReportType.DataBind();
			//Assigning the Report Type IDs to the Report Type Description using Hash Table
            //MAIG - CH1 - BEGIN - Added code to default the Sales collection report
            foreach (ListItem item in cboReportType.Items)
            {
                if (item.Text.ToLower().Equals("sales collection turn-in list"))
                {
                    item.Selected = true;
                    break;
                }
            }
            cboReportType.Enabled = false;
            //MAIG - CH1 - END - Added code to default the Sales collection report

			for(int indexReportType=0;indexReportType<cboReportType.Items.Count;indexReportType++)
			{
				hsReportType[cboReportType.Items[indexReportType].Value] = cboReportType.Items[indexReportType].Value;
			}
		}
		#endregion

		#region FillStatus
		/// <summary>
		/// Filling the Status Combo
		/// </summary>
		private void FillStatus()
		{
			DataSet dtsStatus= TurninService.GetStatus(CurrentUser,cboReportType.SelectedItem.Value.ToString());
			cboStatus.DataSource=dtsStatus.Tables[0];
			cboStatus.DataTextField=dtsStatus.Tables[0].Columns["Description"].ToString();
			cboStatus.DataValueField=dtsStatus.Tables[0].Columns["ID"].ToString();
			cboStatus.DataBind();
			if (Cache[NEW_REJECTED_STATUS_DESC] == null)
			{
				Cache[NEW_REJECTED_STATUS_DESC]=cboStatus.Items.FindByText(NEW_REJECTED_STATUS_DESC).Value;
			}
			if (Cache[PENDING_APPROVAL_STATUS_DESC] == null)
			{
				Cache[PENDING_APPROVAL_STATUS_DESC] = cboStatus.Items.FindByText(PENDING_APPROVAL_STATUS_DESC).Value;
			}
			if (Cache[APPROVED_STATUS_DESC] == null)
			{
				Cache[APPROVED_STATUS_DESC] = cboStatus.Items.FindByText(APPROVED_STATUS_DESC).Value;
			}
		}

		#endregion

		#region cboStatus_SelectedIndexChanged
		/// <summary>
		/// When the selected item in the status drop down is changed
		/// </summary>
		protected void cboStatus_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			if(cboStatus.SelectedItem.Value == Cache[APPROVED_STATUS_DESC].ToString())
			{
                //PC Phase II changes CH2 -Start- Modified the below code to handle Date Time Picker
                TextstartDt.Visible = true;
                TextendDt.Visible = true;
                Date.Visible = true;
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
                Date.Visible = false;
                image.Visible = false;
                image1.Visible = false;
			}
            //PC Phase II changes CH2 - End - Modified the below code to handle Date Time Picker
		}
		#endregion

		#region btnSubmit_Click
		/// <summary>
		/// Clicking the Submit Button
		/// </summary>
		protected void btnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			SubmitSalesCollection();
		}
		#endregion

		#region SubmitSalesCollection
		/// <summary>
		/// For retrieving the data for the chosen criteria
		/// </summary>
		private void SubmitSalesCollection()
		{
            try
            {
                OrderClasses.ReportCriteria rptCrit1 = new OrderClasses.ReportCriteria();
                //Preparing the criteria for retrieving data
                rptCrit1.Users = CurrentUser;
                rptCrit1.Status_Link_ID = cboStatus.SelectedItem.Value;
                rptCrit1.ReportType = Convert.ToInt32(cboReportType.SelectedItem.Value);
                rptCrit1.RepDO = lblRepID.Text.ToString();
                //PC Phase II changes CH2 - Start - Modified the below code to handle Date Time Picker
                if ((TextstartDt.Visible == true) && (TextendDt.Visible == true))
                {
                    rptCrit1.StartDate = Convert.ToDateTime(TextstartDt.Text);
                    rptCrit1.EndDate = Convert.ToDateTime(TextendDt.Text);
                    bool bValid = rptLib.ValidateDate(lblErrMsg, rptCrit1.StartDate, rptCrit1.EndDate, rptCrit1.ReportType);
                //PC Phase II changes CH2 - End - Modified the below code to handle Date Time Picker
                    if (bValid == true)
                    {
                        InvisibleUserInfo();
                        return;
                    }
                }
                else
                {
                    DateTime StartDate = new DateTime(1900, 1, 1);
                    DateTime EndDate = new DateTime(1900, 1, 1);
                    rptCrit1.StartDate = StartDate;
                    rptCrit1.EndDate = EndDate;
                }
                //Q4-Retrofit.Ch3-START :Modified the code to call the Renamed function "PAYWorkflowReport"
                //Obtaining the dataset for the chosen criteria
                //DataSet dtsData = TurninService.SalesRepCashierWorkFlow(rptCrit1);(Removed as part of Q4 Retrofit)
                DataSet dtsData = TurninService.PayWorkflowReport(rptCrit1);
                //Q4-Retrofit.Ch3-END

                Session["RepeaterData"] = dtsData;
                //Security Defect CH1- Added the belowcode to assign the session values to the context.
                Context.Items.Add("RepeaterData", Session["RepeaterData"]);
                if (dtsData.Tables[0].Rows.Count == 0)
                {
                    rptLib.SetMessage(lblErrMsg, "No Data Found. Please specify a valid criteria.", true);
                    InvisibleUserInfo();
                    return;
                }
                else
                {
                    FillUserInfo();
                    VisibleUserInfo();
                    Session["ReportType"] = hsReportType;
                    // Security Defect ch2- Added the belowcode to assign the session values to the context.
                    Context.Items.Add("ReportType", Session["ReportType"]);
                    //PCI added the code to check the null condition for session for qa testing on 01/25/2011 by cognizant.
                    if (Convert.ToInt16(hsReportType.Count) < Convert.ToInt16(1))
                    {
                        Logger.Log("hsReportType hash table is Null in Sales TurnIn Page ");
                    }
                    //Session.Add("ReportType", hsReportType);
                    //Session.Add("SalesTurnINReport", hsReportType);
                    //PCI added the code to check the null condition for session for qa testing on 01/25/2011 by cognizant.
                    if (cboReportType.SelectedItem.Value == hsReportType[SALESTURNIN_REPORT_TYPE].ToString())
                    {
                        tblCrossReference.Visible = false;
                        rptMembershipDetail.Visible = false;
                        rptSalesTurnInRepeater.Visible = true;
                        trBlankRow1.Visible = true;
                        trCollSummary.Visible = true;
                        //Q4-Retrofit.Ch1-START:Added the code to display Other product collection summary and Total collection summary Grids
                        trCollSummarySplit.Visible = true;
                        trOtherCollSummary.Visible = true;
                        trOtherCollSummarySplit.Visible = true;
                        trTotalCollSummarySplit.Visible = true;
                        trTotalCollSummary.Visible = true;
                        tblMemberRepeater.Visible = false;
                        //Q4-Retrofit.Ch1-END
                        rptSalesTurnInRepeater.DataSource = dtsData;
                        rptSalesTurnInRepeater.DataBind();
                        // For displaying the collection summary
                        btnUpdateTotal_Click();
                    }
                    else
                    {
                        if (cboReportType.SelectedItem.Value == hsReportType[MEMBERSHIP_REPORT_TYPE].ToString())
                        {
                            rptSalesTurnInRepeater.Visible = false;
                            tblCollSummary.Visible = false;
                            trBlankRow1.Visible = false;
                            trCollSummary.Visible = false;
                            rptMembershipDetail.Visible = true;
                            //Q4-Retrofit.Ch1-START:Added the code to display Other product collection summary and Total collection summary Grids
                            trCollSummarySplit.Visible = false;
                            trOtherCollSummary.Visible = false;
                            trOtherCollSummarySplit.Visible = false;
                            trTotalCollSummarySplit.Visible = false;
                            trTotalCollSummary.Visible = false;
                            tblCrossReference.Visible = false;
                            tblMemberRepeater.Visible = true;
                            trCaption.Visible = false;
                            //Q4-Retrofit.Ch1-END 
                            rptMembershipDetail.DataSource = dtsData;
                            rptMembershipDetail.DataBind();

                        }
                        if (cboReportType.SelectedItem.Value == hsReportType[CROSSREFERENCE_REPORT_TYPE].ToString())
                        {
                            rptSalesTurnInRepeater.Visible = false;
                            tblCollSummary.Visible = false;
                            trBlankRow1.Visible = false;
                            rptMembershipDetail.Visible = false;
                            trCollSummary.Visible = false;
                            //Q4-Retrofit.Ch1-START:Added the code to display Other product collection summary and Total collection summary Grids
                            trCollSummarySplit.Visible = false;
                            trOtherCollSummary.Visible = false;
                            trOtherCollSummarySplit.Visible = false;
                            trTotalCollSummarySplit.Visible = false;
                            trTotalCollSummary.Visible = false;
                            tblMemberRepeater.Visible = false;
                            trCaption.Visible = false;
                            //Q4-Retrofit.Ch1-END  
                            tblCrossReference.Visible = true;
                            dgCrossReference.CurrentPageIndex = 0;
                            UpdateDataView();
                        }
                    }

                    Session["Users"] = lblUsers.Text;
                    Session["RepDO"] = lblRep.Text;
                    Session["ReportStatus"] = lblReportStatus.Text;
                    Session["SelectedReportType"] = cboReportType.SelectedItem.Value.ToString();
                }
            }
            catch (FormatException f)
            {
                Logger.Log(f);
                string invalidDate = @"<Script>alert('Select a valid date.')</Script>";
                Response.Write(invalidDate);
            }
			
		}
		#endregion
		
		#region CrossReferencePageIndexChanged
		/// <summary>
		/// Navigating to the next page in the Cross Reference Datagrid
		/// </summary>
		public void CrossReferencePageIndexChanged(Object sender, DataGridPageChangedEventArgs e)
		{
			dgCrossReference.CurrentPageIndex=e.NewPageIndex;
			UpdateDataView();
		}
		#endregion

		#region UpdateDataView
		/// <summary>
		/// Refreshing the data when the page is changed in the Cross Reference Datagrid
		/// </summary>
		private void UpdateDataView()
		{
			// Retrieves the data
			DataSet dtsTempRepeaterData = (DataSet) Session["RepeaterData"];
			if (dtsTempRepeaterData == null)
			{
				rptLib.SetMessage(lblErrMsg,"Your session has timed out. Please re-submit the page", true);
				return;
			}
			DataView dtvRepeaterData = dtsTempRepeaterData.Tables[0].DefaultView;
			int pageCnt  = 1;
			int currPage = 1;
			// Re-bind data 
			if (dtsTempRepeaterData.Tables[0].Rows.Count > 0)
			{
				VisibleUserInfo();
				rptSalesTurnInRepeater.Visible=false;
				dgCrossReference.DataSource=dtvRepeaterData;
				dgCrossReference.DataBind();
				tblCrossReference.Visible=true;
				pageCnt  = dgCrossReference.PageCount;
				currPage = dgCrossReference.CurrentPageIndex+1;
				if (pageCnt < dgCrossReference.PageCount) pageCnt = dgCrossReference.PageCount;
				if (currPage < dgCrossReference.CurrentPageIndex+1) currPage = dgCrossReference.CurrentPageIndex+1;
			}			
			if (pageCnt > 1)
			{				
				dgCrossReference.PagerStyle.Visible = true;
			}
			if (pageCnt < 2)
			{
				dgCrossReference.PagerStyle.Visible = false;
			}
			lblPageNum1.Visible=true;
			lblPageNum2.Visible=true;
			lblPageNum1.Text = "Showing Page " + currPage.ToString() + " of " + pageCnt.ToString();
			lblPageNum2.Text = "Showing Page " + currPage.ToString() + " of " + pageCnt.ToString();
		}
		#endregion

		#region InvisibleUserInfo
		/// <summary>
		/// Making the controls invisible
		/// </summary>
		private void InvisibleUserInfo()
		{
			rptSalesTurnInRepeater.Visible=false;
			//Q4-Retrofit.Ch1-START: Added the code to hide the collection summary grids
			trBlankRow1.Visible = false;
			trCollSummarySplit.Visible = false;
			trCollSummary.Visible = false;
			trOtherCollSummarySplit.Visible = false;
			trOtherCollSummary.Visible = false;
			trTotalCollSummary.Visible =false;
			trTotalCollSummarySplit.Visible =false;
			tblMemberRepeater.Visible=false;
			trCaption.Visible = false;
			//Q4-Retrofit.Ch1-END
			rptMembershipDetail.Visible=false;
			tblReportInfo.Visible=false;
			trConvertPrint.Visible=false;
			tblCrossReference.Visible=false;
			lblPageNum1.Visible=false;
			lblPageNum2.Visible=false;
								
		}
		#endregion

		#region VisibleUserInfo
		/// <summary>
		/// Making the controls visible
		/// </summary>
		private void VisibleUserInfo()
		{
			rptSalesTurnInRepeater.Visible=true;
			tblReportInfo.Visible=true;
			trConvertPrint.Visible=true;
			//trCaption.Visible = true ;
		}
		#endregion

		#region rptSalesTurnInRepeater_ItemBound
		/// <summary>
		/// Display of the items in the Sales Turn-In Data Repeater
		/// </summary>
		protected void rptSalesTurnInRepeater_ItemBound(Object Sender, RepeaterItemEventArgs e)
		{
			NewRejectedVisibility(e);
			NonNewRejVisibility(e);
			AlternatingColour(e);
			CalculateTotal(e);
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

		#region dgCrossReference_ItemBound
		/// <summary>
		/// Display of the items in the Cross Reference Data Grid
		/// </summary>
		protected void dgCrossReference_ItemBound(Object Sender, DataGridItemEventArgs e)
		{
			ColourCrossReferenceReport(e);
			SetCrossReferenceGridWidth(e);
		}
		#endregion

		#region SetCrossReferenceGridWidth
		/// <summary>
		/// Setting the width of the columns in the Cross Reference Data Grid
		/// </summary>
		protected void SetCrossReferenceGridWidth(DataGridItemEventArgs e)
		{
			e.Item.Cells[1].Width=100;
			e.Item.Cells[2].Width=110;
			e.Item.Cells[3].Width=120;
			e.Item.Cells[4].Width=110;
			e.Item.Cells[5].Width=134;
			e.Item.Cells[6].Width=30;
		}
		#endregion

		#region NewRejectedVisibility
		/// <summary>
		/// Making the Checkbox and the Void/Reissue 
		/// Button invisible for Peding Approval and Approved status
		/// </summary>
		private void NewRejectedVisibility(RepeaterItemEventArgs e)
		{
			if (cboStatus.SelectedItem.Value  == Cache[PENDING_APPROVAL_STATUS_DESC].ToString() || cboStatus.SelectedItem.Value== Cache[APPROVED_STATUS_DESC].ToString())
			{
				HtmlTableCell tdTemp;
				tblRepeater.Width="604";
				tblCollSummary.CellPadding=2;
				ImageButton  btnTemp;
                if (e.Item.ItemType == ListItemType.Header)
                {
                    tdTemp = (HtmlTableCell)e.Item.FindControl("tdChkHead");
                    tdTemp.Visible = false;
                    tdTemp = (HtmlTableCell)e.Item.FindControl("tdBtnHead");
                    tdTemp.Visible = false;
                    tdTemp = (HtmlTableCell)e.Item.FindControl("tdHeader1");
                    //CHG0109406 - CH2 - BEGIN - Modified the colspan value from 9 to 6 to accomodate the TimeZone field
                    tdTemp.ColSpan = 6;
                    //CHG0109406 - CH2 - END - Modified the colspan value from 9 to 6 to accomodate the TimeZone field
                }
				if (e.Item.ItemType ==ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
				{
					tdTemp=(HtmlTableCell) e.Item.FindControl("tdChkItem");
					tdTemp.Visible=false;
					tdTemp=(HtmlTableCell) e.Item.FindControl("tdBtnItem");
					tdTemp.Visible=false;
				}
				if (e.Item.ItemType == ListItemType.Footer)
				{
					btnTemp=(ImageButton)e.Item.FindControl("btnUpdateTotal");
					btnTemp.Visible=false;
					btnTemp=(ImageButton)e.Item.FindControl("btnSubmitTurnIn");
					btnTemp.Visible=false;
					tdTemp=(HtmlTableCell) e.Item.FindControl("tdFooter");
					tdTemp.Visible=false;
				}
			}
			else
			{
				tblRepeater.Width="750";
			}
		}
		#endregion

		#region NonNewRejVisibility
		/// <summary>
		/// Checking for WU products in New and Rejected Status
		/// </summary>
		private void NonNewRejVisibility(RepeaterItemEventArgs e)
		{
			if (cboStatus.SelectedItem.Value  == Cache[NEW_REJECTED_STATUS_DESC].ToString())
			{
				if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
				{
					WUVisibility(e);	
				
				}
			}
		}
		#endregion

		#region WUVisibility
		/// <summary>
		/// Making the Void/Reissue button invisible for WU products
		/// </summary>
		private void WUVisibility(RepeaterItemEventArgs e)
		{
			Label lblTempProductCode = (Label) e.Item.FindControl("lblUploadToPoes");
			string strTempProductCode = lblTempProductCode.Attributes["value"].ToString();
            //Begin PC Phase II changes CH1 - Modified the ImageButton "btnTempVoidReissue" to "btnTempVoid".
			btnTempVoid=(ImageButton) e.Item.FindControl("btnVoid");
            //End PC Phase II changes CH1 - Modified the ImageButton "btnTempVoidReissue" to "btnTempVoid".
			//Q4-Retrofit.Ch1-START:Modified the code to show the Void and Reissue Button when Upload_To_POES == 1(POES)
			if((strTempProductCode == UPLOAD_TO_POES.ToString()))
			{
                //Begin PC Phase II changes CH1 - Modified the ImageButton "btnTempVoidReissue" to "btnTempVoid".
				btnTempVoid.Visible=true;
                //End PC Phase II changes CH1 - Modified the ImageButton "btnTempVoidReissue" to "btnTempVoid".
			}
			else
            {	//Begin PC Phase II changes CH1 - Modified the ImageButton "btnTempVoidReissue" to "btnTempVoid".
				btnTempVoid.Visible=false;
                //End PC Phase II changes CH1 - Modified the ImageButton "btnTempVoidReissue" to "btnTempVoid".
			}
			//Q4-Retrofit.Ch1-END
		}
		#endregion

		#region AlternatingColour
		/// <summary>
		/// //Displaying the details of the same receipt number in a single colour
		/// Making the Receipt Date,Status, Member Name, checkbox and Void Reissue button of 
		/// same receipt number invisible
		/// </summary>
		private void AlternatingColour(RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				HtmlTableRow tr=(HtmlTableRow) e.Item.FindControl("trItemRow");
				Label lblTempReceiptDate = (Label) e.Item.FindControl("lblReceiptDate");
				lblTempReceiptDate.Text=Convert.ToDateTime(lblTempReceiptDate.Text.ToString()).ToString ("MM/dd/yyyy hh:mm:ss tt");
				Label lblTempStatus=(Label) e.Item.FindControl("lblStatus");
				Label lblTempMemberName=(Label) e.Item.FindControl("lblMemberName");
				Label lblTempReceiptNo = (Label) e.Item.FindControl("lblReceiptNo");
				CheckBox chkTempSelect =(CheckBox)e.Item.FindControl("chkSelect");
                //Begin PC Phase II changes CH1 - Modified the ImageButton "btnTempVoidReissue" to "btnTempVoid".
                btnTempVoid = (ImageButton)e.Item.FindControl("btnVoid");
                //End PC Phase II changes CH1 - Modified the ImageButton "btnTempVoidReissue" to "btnTempVoid".
				string strTempReceiptNo = lblTempReceiptNo.Attributes["value"].ToString();
				//Assigning different colour if the receipt numbers are different
				if(strTempReceiptNo!=tmpreceipt)
				{
					AssignClass(colour,tr);
				}
				else
				{
					tr.Attributes.Add("class",colour);
					lblTempReceiptDate.Visible=false;
					lblTempStatus.Visible=false;
					lblTempMemberName.Visible=false;
					chkTempSelect.Visible=false;
					chkTempSelect.Checked=false;
                    //Begin PC Phase II changes CH1 - Modified the ImageButton "btnTempVoidReissue" to "btnTempVoid".
					btnTempVoid.Visible=false;
                    //End PC Phase II changes CH1 - Modified the ImageButton "btnTempVoidReissue" to "btnTempVoid".
				}
				tmpreceipt=strTempReceiptNo;
			}
		}
		#endregion
		
		#region AlternatingColourMembership
		/// <summary>
		/// Displaying the details of the same order ID in a single colour
		/// Making the Address,City,State,Zip,Day Phone,Eve Phone,Mbr Number,User(s),Src Code,Base Year,
		/// Prod Type,Effective Date,Amount, PymtType of same order ID invisible
		/// </summary>
		private void AlternatingColourMembership(RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				HtmlTableRow trTempItemRowMember=(HtmlTableRow) e.Item.FindControl("trItemRowMember");
				Label lblTempEffectiveDate = (Label) e.Item.FindControl("lblEffectiveDate");
                //TimeZoneChange.Ch2-Commented to display only date and not time as a part of Time Zone Change  by cognizant.
				//lblTempEffectiveDate.Text=Convert.ToDateTime(lblTempEffectiveDate.Text.ToString()).ToString ("MM/dd/yyyy hh:mm:ss tt");
				Label lblTempOrderID=(Label) e.Item.FindControl("lblOrderID");
				Label lblTempAddress=(Label) e.Item.FindControl("lblAddress");
				Label lblTempCity = (Label) e.Item.FindControl("lblCity");
				Label lblTempState = (Label) e.Item.FindControl("lblState");
				Label lblTempZip = (Label) e.Item.FindControl("lblZip");
				Label lblTempDayPhone = (Label) e.Item.FindControl("lblDayPhone");
				Label lblTempEvePhone = (Label) e.Item.FindControl("lblEvePhone");
				Label lblTempMemberNumber = (Label) e.Item.FindControl("lblMemberNumber");
				Label lblTempMemberUsers = (Label) e.Item.FindControl("lblMemberUsers");
				Label lblTempSrcCode = (Label) e.Item.FindControl("lblSrcCode");
				Label lblTempBaseYear = (Label) e.Item.FindControl("lblBaseYear");
				Label lblTempProdType = (Label) e.Item.FindControl("lblProdType");
				Label lblTempAmount = (Label) e.Item.FindControl("lblAmount");
				Label lblTempPymtType = (Label) e.Item.FindControl("lblPymtType");
				string strTempOrderID = lblTempOrderID.Attributes["value"].ToString();
				//Assigning different colour if the Order IDs are different
				if(strTempOrderID!=tmporderid)
				{
					AssignClass(colour,trTempItemRowMember);
				}
				else
				{
					trTempItemRowMember.Attributes.Add("class",colour);
					lblTempEffectiveDate.Visible=false;
					lblTempAddress.Visible=false;
					lblTempCity.Visible=false;
					lblTempState.Visible=false;
					lblTempZip.Visible=false;
					lblTempDayPhone.Visible=false;
					lblTempEvePhone.Visible=false;
					lblTempMemberNumber.Visible=false;
					lblTempMemberUsers.Visible=false;
					lblTempSrcCode.Visible=false;
					lblTempBaseYear.Visible=false;
					lblTempProdType.Visible=false;
					lblTempAmount.Text="0";
					lblTempAmount.Visible=false;
					lblTempPymtType.Visible=false;
				}
				tmporderid=strTempOrderID;
			}
		}
		
		#endregion

		#region AssignClass
		/// <summary>
		/// Swapping of the colour between ALTERNATE_ROW_CLASS and ROW_CLASS
		/// </summary>
		private void AssignClass(string tmpcolour,HtmlTableRow tr)
		{
			if ( tmpcolour == ROW_CLASS )
			{
				tr.Attributes.Add("class",ALTERNATE_ROW_CLASS);
				colour = ALTERNATE_ROW_CLASS;
			}
			else if ( tmpcolour == ALTERNATE_ROW_CLASS )
			{
				tr.Attributes.Add("class",ROW_CLASS);
				colour = ROW_CLASS;
			}
		}
		#endregion

		#region CalculateTotal
		/// <summary>
		/// Calculating the total amount of all the items in the Repeater
		/// </summary>
		private void CalculateTotal(RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem ||e.Item.ItemType == ListItemType.Footer) 
			{
				if (e.Item.ItemType == ListItemType.Footer)
				{
					//Adding the OnClick attribute to Submit for Turn In button
					ImageButton ibTempSubmit = (ImageButton) e.Item.FindControl("btnSubmitTurnIn");
					ibTempSubmit.Attributes.Add("OnClick","return confirmTurnIn();");
					Label lblTempAmount = (Label)e.Item.FindControl("lblTotal");
					lblTempAmount.Text = dblTotal.ToString("$##0.00");
					//Changing the width of the repeater if status is Approved / Pending Approval
					if (cboStatus.SelectedItem.Value == Cache [PENDING_APPROVAL_STATUS_DESC].ToString() || cboStatus.SelectedItem.Value==Cache[APPROVED_STATUS_DESC].ToString())
					{
						HtmlTableCell tctotal=(HtmlTableCell) e.Item.FindControl("tdTotalHead");
						tctotal.ColSpan=8;
						tctotal.InnerHtml ="Total &nbsp;&nbsp;&nbsp;";
					}
					return;
				}
				//Total all the individual amounts
				HtmlTableCell tdTempAmount = (HtmlTableCell)e.Item.FindControl("tdAmount");
				string strAmount=tdTempAmount.Attributes["amount"].ToString();
				dblTotal = dblTotal + Convert.ToDouble(strAmount);
			}
		}
		#endregion
		
		#region CalculateTotalMembership
		/// <summary>
		/// Calculating the total amount of all the items in the Membership Repeater
		/// </summary>
		private void CalculateTotalMembership(RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem ||e.Item.ItemType == ListItemType.Footer) 
			{
				if (e.Item.ItemType == ListItemType.Footer)
				{
					Label lblTempTotalMember = (Label)e.Item.FindControl("lblTotalMember");
					lblTempTotalMember.Text = dblTotalMember.ToString("$##0.00");
					return;
				}
				//Total all the individual amounts
				Label lblTempAmount = (Label)e.Item.FindControl("lblAmount");
				lblTempAmount.Text=lblTempAmount.Text.Replace(",",""); 
				string strAmountMember =  lblTempAmount.Text;
				dblTotalMember = dblTotalMember + Convert.ToDouble(strAmountMember.Replace("$",""));
			}
		}
		#endregion
		
		
		#region rptSalesTurnInRepeater_ItemCommand
		/// <summary>
		/// Determining whether Submit Turn In, Cancel or Update Total is clicked
		/// </summary>
		private void rptSalesTurnInRepeater_ItemCommand(object source, System.Web.UI.WebControls.RepeaterCommandEventArgs e)
		{
			//Determining which button is clicked (Cancel, Update Total, Submit for Turn In)
			if(e.Item.ItemType ==ListItemType.Footer)
			{
				if(e.CommandName == "UpdateTotal")
				{
					btnUpdateTotal_Click();
					return;
				}
				if(e.CommandName == "SubmitTurnIn")
				{
					btnSubmitTurnIn_Click();
					return;
				}
			}
			//Clicking the Void/Reissue button in the repeater
			Label lblTempVoidReissueReceiptNo = (Label) e.Item.FindControl("lblReceiptNo");
			//CSR#3824.Ch1 - Modified by Cognizant on 10/10/2005 for CSR 3824  - For New Void/Reissue Confirmation page
			Session["ReissueInfo"] = null;
			//END
			Response.Redirect("TurnIn_Void_Reissue_Receipt.aspx?ReceiptNumber=" + lblTempVoidReissueReceiptNo.Attributes["value"].ToString(),true);
		}
		#endregion
		
		#region btnSubmitTurnIn_Click
		/// <summary>
		/// Turning In all the selected receipts
		/// </summary>
		private void btnSubmitTurnIn_Click()
		{
			string selreceipts="";
			PrepareSummaryData();
			int index;
			for(index = 0;index<rptSalesTurnInRepeater.Items.Count;index++)
			{
				// Obtaining the receipt numbers of all the items which is checked
				if (rptSalesTurnInRepeater.Items[index].ItemType  == ListItemType.Item || rptSalesTurnInRepeater.Items[index].ItemType  == ListItemType.AlternatingItem)
				{
					CheckBox chkTempSelect=(CheckBox)rptSalesTurnInRepeater.Items[index].FindControl("chkSelect");
					Label lblTempReceiptNo = (Label)rptSalesTurnInRepeater.Items[index].FindControl("lblReceiptNo");
					if (chkTempSelect.Checked==true)
					{
						selreceipts=selreceipts + lblTempReceiptNo.Attributes["value"].ToString()+ ",";
					}
				}
			}
			//Removing the last Comma for receipt
			if(selreceipts.Length == 0)
			{
				lblErrMsg.Text ="Please Choose Altleast 1 Receipt Number to Turn-In";
				VisibleUserInfo();
				//Q4-Retrofit.Ch1-START:Added as part of Q4 Retrofit
				trBlankRow1.Visible = false;
				//Q4-Retrofit.Ch1-END
				return;
			}
			selreceipts=selreceipts.Remove(selreceipts.Length-1,1);	
			//Determining the number of receipts which are checked
			string []splitselreceipts= selreceipts.Split(',');
			//Do not allow Turn-In if the number of transactions exceed the allowed value
			
			if (splitselreceipts.Length > MAX_ALLOWED_TRANSACTION)
			{
				lblErrMsg.Text = "You currently have " + splitselreceipts.Length.ToString() + " transactions in the batch. ";
				lblErrMsg.Text =lblErrMsg.Text + "You may upload only a maximum of "+ MAX_ALLOWED_TRANSACTION + " transactions at one time. ";
				lblErrMsg.Text =lblErrMsg.Text + "Please uncheck some of the transactions and upload them later";
				return;
			}
			
			else
			{
				//Changing the status for the receipt numbers which has been turned in
				OrderClasses.ReportCriteria rptCrit1=new OrderClasses.ReportCriteria();
				rptCrit1.CurrentUser=CurrentUser;
				rptCrit1.ReceiptList=selreceipts;
				TurninService.ReceiptsTurnIn(rptCrit1);
				Hashtable hsReportType = new Hashtable();
				for(int indexReportType=0;indexReportType<cboReportType.Items.Count;indexReportType++)
				{
					hsReportType[cboReportType.Items[indexReportType].Value] = cboReportType.Items[indexReportType].Value;
				}
				Session ["RepeaterData"]=null;
				Session["ReportStatus"]=PENDING_APPROVAL_STATUS_DESC;
				Session["TurnInGridData"]=dtbRepeater; 
				//Q4-Retrofit.Ch1-START :Added the code to store Other Product Collection Summary and Total Collection Summary Grid in the Session
				Session["TurnInGridDataOtherProducts"]=dtbOtherRepeater; 
				Session["TurnInGridDataTotal"]=dtbTotalRepeater; 
				//Q4-Retrofit.Ch1-END
                //PCI added the code to check the null condition for session for qa testing on 01/25/2011 by cognizant.
                if (Session["ReportType"] == null)
                {
                    Logger.Log("Session Report type is null in Sales Collection turn in page");
                }
                if (Session["SalesTurnINReport"] == null)
                {
                    Logger.Log("Session SalesTurnINReport is null in Sales Collection turn in page");
                }
                //PCI added the code to check the null condition for session for qa testing on 01/25/2011 by cognizant.
                   
				Response.Redirect("SalesRep_Confirmation.aspx");
			}
		}
		#endregion
		
		#region cboReportType_SelectedIndexChanged
		/// <summary>
		/// Changing the selection in Report Type combo
		/// </summary>
		protected void cboReportType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillStatus();
			cboStatus_SelectedIndexChanged(sender,e);
		}
		#endregion
	
		#region btnUpdateTotal_Click
		/// <summary>
		/// Create the Collection summary and display it
		/// </summary>
		private void btnUpdateTotal_Click()
		{
			PrepareSummaryData();
            //CHG0083477 - Name Change from AAA NCNU IE to CSAA IG as part of Name change project on 11/15/2013.
			//Q4-Retrofit.Ch1-START :Modified the code to change the caption from "COLLECTION SUMMARY" to "CSAA PRODUCTS"
            //NameChange.Ch1-Changed the CSAA to AAA NCNU IE as a part of name change project on 8/19/2010.
            //CHG0104053 - PAS HO CH1 - START : Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/13/2014
			CollectionSummary.CreateHeader(ref tblCollSummary ,ref dtbRepeater ,"TURN-IN PRODUCTS");
            //CHG0104053 - PAS HO CH1 - END : Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/13/2014
			//Q4-Retrofit.Ch1-END
			CollectionSummary.AddCollSummaryData(ref tblCollSummary ,ref dtbRepeater);
			//Q4-Retrofit.Ch1-START:Added the variable "dbltotal" to get the Total Amount based on which the Collection Summary table will be shown 
			double dblTotal = CollectionSummary.AddCollSummaryFooter(ref tblCollSummary ,ref dtbRepeater);
			//Added the below code to check the Total based on which the  Collection Summary table will be shown
			if (dblTotal > 0)
			{
				trCollSummarySplit.Visible= true;
				trCollSummary.Visible = true;
				//Q4-Retrofit.Ch1-START: Added to make Collection Summary caption visible
				trCaption.Visible=true;
				//Q4-Retrofit.Ch1-END
				//Q1-Retrofit.Ch1 - START: Commented the line below to display the header
				//"PAYMENT TYPE"
				//tblCollSummary.Rows[1].Cells[0].InnerHtml ="&nbsp;";
				//Q1-Retrofit.Ch1 - END
			}
			else
			{
				trCollSummarySplit.Visible= false;
				//Q4-Retrofit.Ch1-START: Added to dissable Collection Summary caption 
				trCaption.Visible=false;
				//Q4-Retrofit.Ch1-END
				trCollSummary.Visible = false;
				
			}
			//Added the code to create Other Product Collection Summary table
            //CHG0104053 - PAS HO CH2 - START : Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/13/2014
			CollectionSummary.CreateHeader(ref tblOtherCollSummary,ref dtbOtherRepeater,"WU/ACA PRODUCTS");
            //CHG0104053 - PAS HO CH2 - END : Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/13/2014
			CollectionSummary.AddCollSummaryData(ref tblOtherCollSummary,ref dtbOtherRepeater);
			dblTotal = CollectionSummary.AddCollSummaryFooter(ref tblOtherCollSummary, ref dtbOtherRepeater);
			if(dblTotal > 0)
			{
				trOtherCollSummary.Visible = true;
				//Q4-Retrofit.Ch1-START: Added to make Collection Summary caption visible
				trCaption.Visible=true;
				//Q4-Retrofit.Ch1-END
				trOtherCollSummarySplit.Visible = true;
				
			}
			else
			{
				trOtherCollSummary.Visible =false;
				//Q4-Retrofit.Ch1-START: Added to dissable Collection Summary caption
				trCaption.Visible=false;
				//Q4-Retrofit.Ch1-END
				trOtherCollSummarySplit.Visible = false;
				
			}
			//Added the code to create Total Collection Summary table
			CollectionSummary.CreateTotalHeader(ref tblTotalCollSummary,ref dtbTotalRepeater);
			CollectionSummary.AddCollTotalSummaryData(ref tblTotalCollSummary ,ref dtbTotalRepeater);
			dblTotal = CollectionSummary.AddCollTotalSummaryFooter(ref tblTotalCollSummary,ref dtbTotalRepeater); 
			if (dblTotal > 0)
			{
				trTotalCollSummary.Visible = true;
				//Q4-Retrofit.Ch1-START: Added to make Collection Summary caption visible
				trCaption.Visible=true;
				//Q4-Retrofit.Ch1-END
				trTotalCollSummarySplit.Visible = true;
			}
			else
			{
				trTotalCollSummary.Visible = false;
				//Q4-Retrofit.Ch1-START: Added to dissable Collection Summary caption
				trCaption.Visible=false;
				//Q4-Retrofit.Ch1-END
				trTotalCollSummarySplit.Visible = false;
				
			}
			//Q4-Retrofit.Ch1-END
			VisibleUserInfo(); 
		}
		#endregion
		
		#region PrepareSummaryData
		/// <summary>
		/// Populating the Temporary Data Table (Summary Data)
		/// </summary>
		private void PrepareSummaryData()
		{
			CreateSummaryTable();
			string tmpreceipt_select="";
            foreach (RepeaterItem item in rptSalesTurnInRepeater.Items)
            {
                //Selecting the details of items which are checked
                if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem)
                {
                    CheckBox chkSelect = (CheckBox)item.FindControl("chkSelect");
                    Label lblTempReceiptNo = (Label)item.FindControl("lblReceiptNo");
                    if (chkSelect.Checked == true)
                    {
                        tmpreceipt_select = lblTempReceiptNo.Attributes["value"].ToString();
                    }
                    //Q4-Retrofit.Ch4 : Changed the code for getting constant value from Application object (web.config changes)
                    //Insert values into the Temporary Data Table (Summary Table)if the item is Checked / Pending Approval / Approved
                    if ((tmpreceipt_select == lblTempReceiptNo.Attributes["value"].ToString()) || cboStatus.SelectedItem.Value == Cache[PENDING_APPROVAL_STATUS_DESC].ToString() || cboStatus.SelectedItem.Value == Cache[APPROVED_STATUS_DESC].ToString())
                    {
                        HtmlTableCell tdTempAmount = (HtmlTableCell)item.FindControl("tdAmount");
                        string strAmount = tdTempAmount.Attributes["amount"].ToString();
                        HtmlTableCell tdTempPymtType = (HtmlTableCell)item.FindControl("tdPymtType");
                        HtmlTableCell tdTempProdType = (HtmlTableCell)item.FindControl("tdProdType");

                        // SalesTurnin_Report Changes.Ch2 : START - Commented tdTransType since it is nomore used
                        // HtmlTableCell tdTempTransType=(HtmlTableCell)item.FindControl("tdTransType");
                        // SalesTurnin_Report Changes.Ch2 : END - Commented tdTransType since it is nomore used

                        //Added the code to create CSAA Product and Other Product Collection
                        // Summary Grid based on the Upload Type Value.	

                        //string strUploadType = "," + Config.Setting("UploadType.CSAAProducts") + ",";
                        string strUploadType = "," + Convert.ToString(((StringDictionary)Application["Constants"])["UploadType.CSAAProducts"]) + ",";
                        HtmlTableCell tdpolicyno = (HtmlTableCell)item.FindControl("tdpolicynumber");
                        string pno = tdpolicyno.InnerText.Trim();
                        string[] pno1 = pno.Split(new char[] { '\t' });
                        pno1[0] = pno1[0].Trim();
                        int length = (pno1[0]).Length;
                        string prodtype = tdTempProdType.InnerText.Trim();

                        if (strUploadType.IndexOf("," + tdTempProdType.Attributes["UploadType"].ToString() + ",") >= 0)
                        {
                            // SalesTurnin_Report Changes.Ch2 : START - Commented tdTransType since it is nomore used
                            // string strTransType = tdTempTransType.InnerText;
                            // SalesTurnin_Report Changes.Ch2 : END - Commented tdTransType since it is nomore used

                            //Q4-Retrofit.Ch2-START:Added the code to display "Payments" as Revenue Type for HUON products
                            //in the CSAA Collection Summary Grid 
                            //if (tdTempProdType.Attributes["UploadType"].ToString() == Config.Setting("UploadType.HUON"))

                            // SalesTurnin_Report Changes.Ch2 : START - Commented tdTransType since it is nomore used
                            //if (tdTempProdType.Attributes["UploadType"].ToString() == Convert.ToString(((StringDictionary)Application["Constants"])["UploadType.HUON"]))
                            //{
                            //Getting the Revenue Type description for HUON Products from the config
                            //strTransType = 	Config.Setting("RevenueTypeDescription.HUON") ;
                            //strTransType = Convert.ToString(((StringDictionary)Application["Constants"])["RevenueTypeDescription.HUON"]);
                            //}

                            //Q4-Retrofit.Ch2:End
                            //For CSAA products Collection Summary
                            //InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, tdTempProdType.InnerText, strTransType, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                            // SalesTurnin_Report Changes.Ch2 : END - Commented tdTransType since it is nomore used

                            //SalesTurnin_Report Changes.Ch3: START - Added the below code to fetch PAS Auto Summary Separately.
                            //MAIG - CH2 - BEGIN - Modified code to support the Common Product Type Auto and Home
                            //CHG0104053 - PAS HO CH3- BEGIN : Modified the below code to group the AAA SS Auto under PAS Auto 7/02/2014
                            if (length == 13 && (prodtype.ToLower().Equals("auto")))
                            //CHG0104053 - PAS HO CH3- END : Modified the below code to group the AAA SS Auto under PAS Auto 7/02/2014
                            {
                                InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "PAS Auto", "", Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                            }
                            /*else if (length == 9 && prodtype.ToLower().Equals("auto"))
                            {

                                InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "HUON Auto", "", Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                            }*/

                            else
                            {
                                //CHG0104053 - PAS HO CH4- BEGIN : Added the below code to group the AAA SS Home and CSAA IG Home Condo under PAS Home 7/02/2014                          

                                if (length == 13 && prodtype.ToLower().Equals("home"))
                                {
                                    InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "PAS Home", "", Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));

                                    ////////// PAS HO CH42-Added to show HDES policy summary both under 'PAS HOME' & 'HODE/HDES'
                                    ////if(length == 7 && prodtype.ToLower().Equals("homeowners"))
                                    ////    InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, tdTempProdType.InnerText, strTransType, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                                }
                                else if (length == 7 && prodtype.ToLower().Equals("home"))
                                {
                                    InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "Home/HDES", "", Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                                }
                                else
                                {
                                    //Modified the code to pass strTransType as Parameter instead of tdTempTransType.InnerText
                                    //MAIG  - Modified the below line ref type paramter name from dtbSummary to dtbOtherSummary to display in WU/ACA Products Collection 11/25/2014
                                    InsertSummaryTable(ref dtbOtherSummary, tdTempPymtType.InnerText, Convert.ToString(((StringDictionary)Application["Constants"])["ProductTypeHeader.WU_ACA"]), "", Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                                    //Q4-Retrofit.Ch1-END
                                }
                                //PAS HO CH31- END : Added the below code to group the AAA SS Home and CSAA IG Home Condo under PAS Home 7/02/2014
                            }

                            //SalesTurnin_Report Changes.Ch3: END - Added the below code to fetch PAS Auto Summary Separately.
                        }
                        else
                        {
                            if (length == 13 && (prodtype.ToLower().Equals("dwelling fire") || prodtype.ToLower().Equals("personal umbrella")))
                            {
                                InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "PAS Home", "", Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                            }
                            else
                            {
                                //For Other Products Collection Summary
                                //InsertSummaryTable(ref dtbOtherSummary,tdTempPymtType.InnerText,Config.Setting("ProductTypeHeader.WU_ACA").ToString(),tdTempTransType.InnerText,Convert.ToDecimal(strAmount.Replace("$","").Replace(",","")));
                                // SalesTurnin_Report Changes.Ch2 : START - Commented tdTransType since it is nomore used
                                //InsertSummaryTable(ref dtbOtherSummary,tdTempPymtType.InnerText,Convert.ToString(((StringDictionary)Application["Constants"])["ProductTypeHeader.WU_ACA"]),tdTempTransType.InnerText,Convert.ToDecimal(strAmount.Replace("$","").Replace(",","")));
                                InsertSummaryTable(ref dtbOtherSummary, tdTempPymtType.InnerText, Convert.ToString(((StringDictionary)Application["Constants"])["ProductTypeHeader.WU_ACA"]), "", Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                                // SalesTurnin_Report Changes.Ch2 : END - Commented tdTransType since it is nomore used
                                //CHG0104053 - PAS HO CH4- END : Added the below code to group the AAA SS Home and CSAA IG Home Condo under PAS Home 7/02/2014                          
                            }
                                //MAIG - CH2 - END - Modified code to support the Common Product Type Auto and Home
                            }
                            //Added the Code to create Total Collection Summary Grid
                            InsertTotalTable(ref dtbTotal, tdTempPymtType.InnerText, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                            //Q4-Retrofit.Ch1-END
                        }
                        //Q4-Retrofit.Ch4 : END - Changed the code for getting constant value from Application object (web.config changes)
                    }
                }
			dtbSummary.DefaultView.Sort = "PayType,ProdType,RevType";
           

			//Q4-Retrofit.Ch1-START :Added the code to Sort Other Product Collection Summary and Total Collection Summary grid
			dtbOtherSummary.DefaultView.Sort = "PayType,ProdType,RevType";
			dtbTotal.DefaultView.Sort = "PayType";
			//Q4-Retrofit.Ch1-END

            // SalesTurnin_Report Changes.Ch4 :START- Added code to Sort PayType to get HUON Auto, Auto Non-Converted, PAS Auto, Homeowners and Membership Products in order.
            //MAIG - CH3 - BEGIN - Added code to Sort PayType to get PAS Auto,PAS Home,Home/HDES, WU/ACA, Homeowners and Membership Products in order.

            DataTable mdtbsummary = dtbSummary.Clone();
            //List<DataRow> lsthuon = dtbSummary.Select("ProdType= 'HUON Auto'").ToList();
            //List<DataRow> lstlegacy = dtbSummary.Select("ProdType = 'Auto Non-Converted'").ToList();
            List<DataRow> lstpas = dtbSummary.Select("ProdType = 'PAS Auto'").ToList();
            //CHG0104053 - PAS HO CH5- BEGIN : Added the below code to filter only the PAS Home policies 7/02/2014
            List<DataRow> lstpasHome = dtbSummary.Select("ProdType = 'PAS Home'").ToList();
            //CHG0104053 - PAS HO CH5- END : Added the below code to filter only the PAS Home policies 7/02/2014
            //CHG0104053 - PAS HO CH6 - BEGIN : Added the below code to change the label from HomeOwner to Home/HDES 6/16/2014
            List<DataRow> lsthome = dtbSummary.Select("ProdType = 'Home/HDES'").ToList();
            //CHG0104053 - PAS HO CH6 - END : Added the below code to change the label from HomeOwner to Home/HDES 6/16/2014
            List<DataRow> lstWU_ACA = dtbSummary.Select("ProdType = 'WU/ACA'").ToList();

            if (lstpas.Count > 0)
            {
                foreach (DataRow item in lstpas)
                {
                    mdtbsummary.Rows.Add(item.ItemArray);
                }
            }
            //CHG0104053 - PAS HO CH7- BEGIN : Added the below code to add the PAS Home policies 7/02/2014
            if (lstpasHome.Count > 0)
            {
                foreach (DataRow item in lstpasHome)
                {
                    mdtbsummary.Rows.Add(item.ItemArray);
                }
            }
            //CHG0104053 - PAS HO CH7- END : Added the below code to add the PAS Home policies 7/02/2014
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
            /*
            if (lstlegacy.Count > 0)
            {
                foreach (DataRow item in lstlegacy)
                {
                    mdtbsummary.Rows.Add(item.ItemArray);
                }
            }
            if (lstmem.Count > 0)
            {
                foreach (DataRow item in lstmem)
                {
                    mdtbsummary.Rows.Add(item.ItemArray);
                }
            }*/
            dtbSummary = mdtbsummary.Copy();
            //MAIG - CH3 - END - Added code to Sort PayType to get PAS Auto,PAS Home,Home/HDES, WU/ACA, Homeowners and Membership Products in order.
            // SalesTurnin_Report Changes.Ch4 :END- Added code to Sort PayType to get HUON Auto, Auto Non-Converted, PAS Auto, Homeowners and Membership Products in order.

			CreateRepeaterTable();
            // SalesTurnin_Report Changes.Ch4 :START- commented existing code and Added code to Sort PayType to get HUON Auto, Auto Non-Converted, PAS Auto, Homeowners and Membership Products in order.
            //DataView rowView = new DataView(dtbSummary,"","PayType,ProdType,RevType",DataViewRowState.CurrentRows);
            //DataView colListView = new DataView(dtbSummary,"","ProdType,RevType",DataViewRowState.CurrentRows);
            DataView rowView = new DataView(dtbSummary, "", "", DataViewRowState.CurrentRows);
            DataView colListView = new DataView(dtbSummary, "", "", DataViewRowState.CurrentRows);
            // SalesTurnin_Report Changes.Ch4 :EN- commented existing code and Added code to Sort PayType to get HUON Auto, Auto Non-Converted, PAS Auto, Homeowners and Membership Products in order.


			//Q4-Retrofit.Ch1-START :Added the code to generate Other Product Collection Summary and Total Collection Summary Table
			DataView rowViewOther = new DataView(dtbOtherSummary,"","PayType,ProdType,RevType",DataViewRowState.CurrentRows);
			DataView colListViewOther = new DataView(dtbOtherSummary,"","ProdType,RevType",DataViewRowState.CurrentRows);

			DataView rowViewTotal = new DataView(dtbTotal,"","PayType",DataViewRowState.CurrentRows);
			DataView colListViewTotal = new DataView(dtbTotal,"","",DataViewRowState.CurrentRows);
			//Q4-Retrofit.Ch1-END

			// Creating and adding columns in the repeater table in the format ProdType|RevType 
			foreach(DataRowView dr in colListView)
			{
				AddColumnToRepeaterTable(dr["ProdType"].ToString() + "|" + dr["RevType"].ToString(),ref dtbRepeater);
			}

			//Q4-Retrofit.Ch1-START :Added the code to generate Other Product Collection Summary table
			foreach(DataRowView dr in colListViewOther)
			{
				AddColumnToRepeaterTable(dr["ProdType"].ToString() + "|" + dr["RevType"].ToString(),ref dtbOtherRepeater );
			}
			//Q4-Retrofit.Ch1-END

			//Inserting data in the Repeater table from the summary table
			foreach(DataRowView dr in rowView)
			{
				InsertRepeaterTable(ref dtbRepeater, dr["PayType"].ToString(), dr["ProdType"].ToString(), dr["RevType"].ToString(), Convert.ToDecimal(dr["Amount"].ToString()));
			}

			//Q4-Retrofit.Ch1-START:Added the code to generate Other Product Collection Summary table
			foreach(DataRowView dr in rowViewOther)
			{
				InsertRepeaterTable( ref dtbOtherRepeater , dr["PayType"].ToString(), dr["ProdType"].ToString(), dr["RevType"].ToString(), Convert.ToDecimal(dr["Amount"].ToString()));
			}
			//Q4-Retrofit.Ch1-END

			//Q4-Retrofit.Ch1-START:Added the Code to generate Total Collection Summary table
			AddColumnToRepeaterTable("Amount",ref dtbTotalRepeater);
			foreach(DataRowView dr in rowViewTotal)
			{
				InsertRepeaterTotalTable(ref dtbTotalRepeater , dr["PayType"].ToString(),Convert.ToDecimal(dr["Amount"].ToString()));
			}
			//Q4-Retrofit.Ch1-END

            // SalesTurnin_Report Changes.Ch4 :START- Added code to Sort PayType to get HUON Auto, Auto Non-Converted, PAS Auto, Homeowners and Membership Products in order.

            dtbRepeater.DefaultView.Sort = "PayType";
            dtbRepeater = dtbRepeater.DefaultView.ToTable(true);

            dtbOtherRepeater.DefaultView.Sort = "PayType";
            dtbOtherRepeater = dtbOtherRepeater.DefaultView.ToTable(true); 


            // SalesTurnin_Report Changes.Ch4 :END- Added code to Sort PayType to get HUON Auto, Auto Non-Converted, PAS Auto, Homeowners and Membership Products in order.




		}
		#endregion

		#region CreateSummaryTable
		/// <summary>
		///  Temporary Data Table (Summary Table) is created having the columns PayType,ProdType,RevType and Amount 
		///  with PayType,ProdType,RevType as combinde primary key
		/// </summary>
		private void CreateSummaryTable()
		{
			dtbSummary = new DataTable();
			dtbSummary.Columns.Add("PayType");
			dtbSummary.Columns.Add("ProdType");
			dtbSummary.Columns.Add("RevType");
			dtbSummary.Columns.Add("Amount",System.Type.GetType("System.Decimal"));
			dtbSummary.PrimaryKey = new DataColumn[]{ dtbSummary.Columns["PayType"],dtbSummary.Columns["ProdType"],dtbSummary.Columns["RevType"] };

			//Q4-Retrofit.Ch1-START :Added the code to create a temporary table for Other product collection summary
			dtbOtherSummary = new DataTable();
			dtbOtherSummary.Columns.Add("PayType");
			dtbOtherSummary.Columns.Add("ProdType");
			dtbOtherSummary.Columns.Add("RevType");
			dtbOtherSummary.Columns.Add("Amount",System.Type.GetType("System.Decimal"));
			dtbOtherSummary.PrimaryKey = new DataColumn[] {dtbOtherSummary.Columns["PayType"],dtbOtherSummary.Columns["ProdType"],dtbOtherSummary.Columns["RevType"] };
			

			//Added the code to create a temporary table for Total collection summary
			dtbTotal = new DataTable();
			dtbTotal.Columns.Add("PayType");
			dtbTotal.Columns.Add("Amount",System.Type.GetType("System.Decimal"));
			dtbTotal.PrimaryKey = new DataColumn[]{dtbTotal.Columns["PayType"]};
			//Q4-Retrofit.Ch1-END
			
		}
		#endregion

		#region CreateRepeaterTable
		/// <summary>
		///  Temporary Data Table (Repeater Table) is created with the column PayType
		/// </summary>
		private void CreateRepeaterTable()
		{
			dtbRepeater = new DataTable();
			dtbRepeater.Columns.Add("PayType");

			//Q4-Retrofit.Ch1-START:Added the code to create a temporary table for Other product collection summary		
			dtbOtherRepeater = new DataTable();
			dtbOtherRepeater.Columns.Add("PayType");
			//Added the code to create a temporary table for Total collection summary		
			dtbTotalRepeater = new DataTable();
			dtbTotalRepeater.Columns.Add("PayType");
			//Q4-Retrofit.Ch1-END
			
		}
		#endregion

		#region AddColumnToRepeaterTable
		/// <summary>
		///  Adding a column to the repeater table
		/// </summary>
		
		private void AddColumnToRepeaterTable(string strColName,ref DataTable dtbReferer)
		{
			if ( !dtbReferer.Columns.Contains(strColName) )
			{
				DataColumn dtCol = new DataColumn(strColName,System.Type.GetType("System.Decimal"));
				dtCol.DefaultValue = Convert.ToDecimal(0);;
				dtbReferer.Columns.Add(dtCol);
			}
		}
		#endregion

		#region InsertRepeaterTable
		/// <summary>
		///  Inserting values to the repeater table from the summary table
		/// </summary>
		//Q4-Retrofit.Ch1-START-Modified the Code to have a datatable Parameter
		
		private void InsertRepeaterTable(ref DataTable dtbReferer,string inPaytype,string inProdtype,string inRevtype,decimal inAmount)
		{
			string strExpression = string.Empty;
			DataRow dtrRepeater;
			DataRow[] dtrArrar;
			strExpression = strExpression + "PayType = '" + inPaytype + "'";
			dtrArrar = dtbReferer.Select(strExpression);
			//Inserting the payment type as a new row in case it is not already inserted
			if ( dtrArrar.GetLength(0) == 0 )
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
			
			//Q4-Retrofit.Ch1-END
		}
		#endregion

		#region InsertSummaryTable
		/// <summary>
		///  Inserting values into the Temporary Data Table (Summary table)
		/// </summary>
		//Q4-Retrofit.Ch1-START:Modified the Code to have a datatable (dtbTemp) Parameter
		private void InsertSummaryTable(ref DataTable dtbTemp,string inPaytype,string inProdtype,string inRevtype,decimal inAmount)
		{
			DataRow dtrSummary;
			DataRow[] dtrArrar;
			inPaytype = inPaytype.Trim(); 
			inProdtype=inProdtype.Trim();
            //MAIG - CH4 - BEGIN - ommeneted the un-used Product Types
            //CHG0104053 - PAS HO CH8 - START : Added the below code to change the label from HomeOwner to Home/HDES 6/13/2014
            //if(inProdtype.ToLower().Equals("homeowners"))
            //{
            //    inProdtype = "Home/HDES";
            //}
            //CHG0104053 - PAS HO CH8 - END : Added the below code to change the label from HomeOwner to Home/HDES 6/13/2014
			inRevtype=inRevtype.Trim();
			//For Product Type 'Mbr' it is changed to 'Membership'
			/*if (inProdtype.ToUpper() == MEMBER.ToUpper())
			{
				inProdtype= MEMBERSHIP;
			}*/
            //MAIG - CH4 - END - Commeneted the un-used Product Types
			//For Revenue Type 'Earned Premium' it is changed to 'Installment Payment'
			if(inRevtype.ToUpper() == EARNED_PREMIUM.ToUpper())
			{
				inRevtype=INSTALLMENT_PAYMENT;
			}
			string strExpression = string.Empty;
			strExpression = strExpression + "PayType = '" + inPaytype + "'" + " and ProdType = '" + inProdtype + "'" + " and RevType = '" + inRevtype + "'";
			dtrArrar = dtbTemp.Select(strExpression);
			// If there is no row having the same PayType, ProdType, Revenue Type, add a new row
			if ( dtrArrar.GetLength(0) == 0 )
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
		//Q4-Retrofit.Ch1-END
			#endregion

		#region ColourCrossReferenceReport
		/// <summary>
		/// Displaying the details of the Transaction in a single colour
		/// Making the Receipt Date/Time, Membership Number, Users, Rep DO visible only for the
		/// first line item in a transaction for Cross Reference Report
		/// </summary>
		protected void ColourCrossReferenceReport(DataGridItemEventArgs e)
		{
			e.Item.Cells[0].Visible = false;

			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView drvItem = (DataRowView) e.Item.DataItem;
				string strTempReceiptNumber = drvItem["Receipt Number"].ToString();

				if(strTempReceiptNumber != tmpreceiptnum)
				{
					AssignCrossReferenceClass(crosscolour, e);
				}
				else
				{
					e.Item.Attributes.Add("class",crosscolour);
					e.Item.Cells[1].Text = "";
					e.Item.Cells[2].Text = "";
					e.Item.Cells[5].Text = "";
					e.Item.Cells[6].Text = "";
				}
				tmpreceiptnum = strTempReceiptNumber;
			}
		}
		#endregion

		#region AssignCrossReferenceClass
		/// <summary>
		/// Swapping of the colour between ALTERNATE_ROW_CLASS and ROW_CLASS for Cross Reference Report
		/// </summary>
		private void AssignCrossReferenceClass(string tmpcolour,DataGridItemEventArgs e)
		{
			if ( tmpcolour == ROW_CLASS )
			{
				e.Item.Attributes.Add("class",ALTERNATE_ROW_CLASS);
				crosscolour = ALTERNATE_ROW_CLASS;
			}
			else if ( tmpcolour == ALTERNATE_ROW_CLASS )
			{
				e.Item.Attributes.Add("class",ROW_CLASS);
				crosscolour = ROW_CLASS;
			}
		}
		#endregion

		//Q4-Retrofit.Ch1-START :Added the function to generate Total Summary table
		
		#region InsertTotalTable

		private void InsertTotalTable(ref DataTable dtbTemp, string inPaytype,decimal inAmount)
		{
			DataRow dtrSummary;
			DataRow[] dtrArrar;
			inPaytype = inPaytype.Trim();
			string strExpression = string.Empty;
			strExpression = strExpression + "PayType = '" + inPaytype + "'";
			dtrArrar = dtbTemp.Select(strExpression);
			if ( dtrArrar.GetLength(0) == 0 )
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

		//Q4-Retrofit.Ch1-END

		//Q4-Retrofit.Ch1-START :Added the function to generate Total Summary table

		#region InsertRepeaterTotalTable

		private void InsertRepeaterTotalTable(ref DataTable dtbReferer,string inPaytype,decimal inAmount)
		{
			string strExpression = string.Empty;
			DataRow dtrRepeater;
			DataRow[] dtrArrar;
			strExpression = strExpression + "PayType = '" + inPaytype + "'";
			dtrArrar = dtbReferer.Select(strExpression);
			//Inserting the payment type as a new row in case it is not already inserted
			if ( dtrArrar.GetLength(0) == 0 )
			{
				dtrRepeater = dtbReferer.NewRow();
				dtrRepeater["PayType"] = inPaytype;
				dtrRepeater["Amount"] = inAmount;
				dtbReferer.Rows.Add(dtrRepeater);
			}
				//Placing the amount in case the column is already present
			else
			{
				dtrArrar[0]["Amount"]  = Convert.ToDecimal(inAmount);
			}
		}

		#endregion

		//Q4-Retrofit.Ch1-END

	}
}