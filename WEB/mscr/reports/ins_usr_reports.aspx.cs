/* 
 * Modified as a part of STAR Auto 2.1 on 09/07/2007:
 * STAR AUTO 2.1.Ch1: Added a session variable RepDOName with null value to avoid displaying RepDO 
 * *                    in Print and Excel version
 * Modified by cognizant as a partt of .NetMigration 3.5 on 04-14-2010.
 * .NetMig 3.5:Modified the btn_submit pageindexchanged1 and Update data view methods to display the results properly.
 * MODIFIED AS A PART OF TIME ZONE CHANGE ENHANCEMENT ON 8-31-2010.
 * * TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time and DateRange by cognizant.
 * * 67811A0 - PCI Remediation for Payment systems CH1: Change Column Index to accomodate State and Prefix column
 * 67811A0 - PCI Remediation for Payment systems CH2: Added to clear the cache of the page to prevent the page loading on browser back button hit 
 * PC Phase II changes CH1 - Added the below code to include the status value to report criteria 
 * //PC Phase II changes CH2 - Added the below code to Handle Date Time Picker 
 * PC Phase II Changes CH3- Commented the below code to display Report Types for only Summary and Detail.
 * PC Phase II 4/20 - Added logging to find the user ID and search criteria for the insurance reports, Transaction search.
 * CHG0109406 - CH1 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH2 - Set the label lblHeaderTimeZone to display the timezone for the results displayed
 * CHG0110069 - CH1 - Appended the /Time along with the Date Label
 */

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing ;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Configuration;
using CSAAWeb;
using CSAAWeb.Serializers;
using CSAAWeb.AppLogger;
using InsuranceClasses;

namespace MSCR.Reports
{
	/// <summary>
	/// Summary description for InsReport1.
	/// </summary>
	public partial class ins_usr_reports : CSAAWeb.WebControls.PageTemplate 
	{
		protected System.Web.UI.HtmlControls.HtmlForm frmReports;

		//protected MSCR.Controls.Dates Dates;
		protected MSCR.Controls.RevenueProduct RevenueProduct;

		//Code Added by Cognizant
		protected MSCR.Controls.PaymentTypeControl PaymentType;
		//Code Added by Cognizant
	
		protected System.Drawing.Color clr = Color.White;

		//Code Added by Cognizant
		//Code Added by Cognizant
		
		InsRptLibrary rptLib = new InsRptLibrary();

		protected void Page_Load(object sender, System.EventArgs e)
		{
            //67811A0 - PCI Remediation for Payment systems CH2 START: Added to clear the cache of the page to prevent the page loading on browser back button hit 
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            //67811A0 - PCI Remediation for Payment systems CH2 END: Added to clear the cache of the page to prevent the page loading on browser back button hit 
			if (!Page.IsPostBack)
			{	
				pnlTitle.Visible = false;
				string CurrentUser = Page.User.Identity.Name;
				rptLib.FillUserListBox(UserList, CurrentUser,CurrentUser,"-1", "-1", true,false);

                //PC Phase II changes CH2 - Start - Modified the below code to Handle Date Time Picker
                DateTime now = DateTime.Now;
                DateTime startdate = new DateTime(now.Year, now.Month, 1);
                TextstartDate.Text = startdate.ToString("MM-dd-yyyy HH:mm:ss");
                DateTime enddate = DateTime.Today;
                TextendDate.Text = enddate.ToString("MM-dd-yyyy") + " 23:59:59";
                //PC Phase II changes CH2 - End - Modified the below code to Handle Date Time Picker
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
			Initialize();
			//	ReportType.AccessType = "-1";
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

		/// <summary>
		/// Binding the user controls.
		/// </summary>
		private void Initialize()
		{    
			//MSCR.Controls.Dates Dates = (MSCR.Controls.Dates)FindControl("Dates");
			//Dates.DataBind();
			MSCR.Controls.RevenueProduct RevenueProduct = (MSCR.Controls.RevenueProduct)FindControl("RevenueProduct");
			RevenueProduct.DataBind();

			//Code Added by Cognizant
			MSCR.Controls.PaymentTypeControl PaymentType = (MSCR.Controls.PaymentTypeControl)FindControl("PaymentType");
			PaymentType.DataBind();
			//Code Added by Cognizant
		}


		protected void btnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{				
				//Couldn't help naming one proc after Bart who is fixated on the appearance and disappearance of controls.
				InvisibleBarts();	
				
			//	string lstNames = UserList.SelectedItem.Text.Replace(" - " + UserList.SelectedItem.Value," ");
			//	string lstUsers = UserList.SelectedItem.Value;		
	
				OrderClasses.ReportCriteria rptCrit1 = new OrderClasses.ReportCriteria();
				rptCrit1.ReportType = Convert.ToInt32(report_type.SelectedItem.Value);
                //PC Phase II changes CH2 - Start - Added the below code to Handle Date Time Picker 
                rptCrit1.StartDate = Convert.ToDateTime(TextstartDate.Text);
                rptCrit1.EndDate = Convert.ToDateTime(TextendDate.Text);
                //PC Phase II changes CH2 - End - Added the below code to Handle Date Time Picker 
				rptCrit1.ProductType = RevenueProduct.ProductType;
				rptCrit1.RevenueType = RevenueProduct.RevenueType;

				//Code Added by Cognizant
				rptCrit1.PaymentType = PaymentType.PaymentType;
				//Code Added by Cognizant

				rptCrit1.App = "-1";
				rptCrit1.Role = "-1";
				rptCrit1.RepDO = "-1";
				rptCrit1.Users = UserList.SelectedItem.Value;

                //PC Phase II changes CH1 - Start - Added the below code to include the status value to report criteria
                rptCrit1.Status = Status.SelectedItem.Value;
                //PC Phase II changes CH1 - End - Added the below code to include the status value to report criteria
				bool bValid = rptLib.ValidateDate(lblErrMsg, rptCrit1.StartDate, rptCrit1.EndDate,rptCrit1.ReportType);
				if (bValid == true) 
				{
					InvisibleControls();
					return;
				}

				if (rptCrit1.Users.Length > 2000)
				{
					rptLib.SetMessage(lblErrMsg,"The total length of users should not exceed 2000 characters", true);
					InvisibleControls();
					return;
				}
                if (report_type.SelectedItem.Value == "2")
                {
                    TimeSpan ts = Convert.ToDateTime(TextendDate.Text) - Convert.ToDateTime(TextstartDate.Text);
                    if (ts.Days > 2)
                    {
                        InvisibleControls();
                        rptLib.SetMessage(lblErrMsg, "Please limit the date range to not more than 2 Days.", true);
                        return;
                    }
                }				
				//Call the insurance web service
                //PC Phase II 4/20 - START-Added logging to find the user ID and search criteria for the insurance reports, Transaction search
                    InsuranceClasses.Service.Insurance InsSvc = new InsuranceClasses.Service.Insurance();
                Logger.Log(CSAAWeb.Constants.INS_REPORT_USERID + Page.User.Identity.Name.ToString() + CSAAWeb.Constants.PARAMETER+ rptCrit1);
                //PC Phase II 4/20 -END- Added logging to find the user ID and search criteria for the insurance reports, Transaction search
				// Create and Fill the DataSet
				DataSet ds = InsSvc.GetInsuranceReports(rptCrit1);
                Logger.Log(CSAAWeb.Constants.INS_REPORT_DATASET);
				//PC Phase II Changes CH3- Commented the below code to display Report Types for only Summary and Detail.
				//if (ds.Tables[0].Rows.Count == 0 & ds.Tables[1].Rows.Count == 0)
                if (ds.Tables[0].Rows.Count == 0)
				{
					rptLib.SetMessage(lblErrMsg, "No Data Found. Please specify a valid criteria.", true);
					InvisibleControls();
					return;
				}
				else
				{
					VisibleControls();
				}

				SetHeaders();
                //CHG0109406 - CH2 - BEGIN - Set the label lblHeaderTimeZone to display the timezone for the results displayed
                    lblHeaderTimeZone.Visible = true;
                //CHG0109406 - CH2 - END - Set the label lblHeaderTimeZone to display the timezone for the results displayed
				Session.Timeout = Convert.ToInt32(Config.Setting("SessionTimeOut"));
				Session["MyDataSet"] = ds;
				Session["MyReportType"] = report_type.SelectedItem.Value;
                //Added as a part of .NetMig 3.5
				dgReport1.PageIndex = 0;
				dgReport2.PageIndex = 0;			
				
				UpdateDataView();
		
			}
			catch (FormatException f)
			{ 
				Logger.Log(f);
				string invalidDate = @"<Script>alert('Select a valid date.')</Script>"; 
				Response.Write(invalidDate);
                lblPageNum1.Visible = false;
                lblPageNum2.Visible = false;
				return;
			}
			catch  (Exception ex) 
			{
				Logger.Log(ex);
                rptLib.SetMessage(lblErrMsg, MSCRCore.Constants.MSG_GENERAL_ERROR, true);
			}
		
		}

	
		// EVENT HANDLER: ItemDataBound
        public void ItemDataBound1(Object sender, GridViewRowEventArgs e)
		{
			// Retrieve the data linked through the relation
			// Given the structure of the data ONLY ONE row is retrieved
			DataRowView drv = (DataRowView) e.Row.DataItem;
			if (drv == null)
				return;
            
			// Check here the app-specific way to detect whether the 
			// current row is a summary row
			int n = int.Parse(report_type.SelectedItem.Value.ToString());
			switch(n)
			{
				case 6:
                    //67811A0 - PCI Remediation for Payment systems CH1: Start Change Column Index to accomodate State and Prefix column
				    //e.Row.Cells[10].Text = (String.Format("{0:c}",drv["Total"])).ToString();
					//e.Row.Cells[10].HorizontalAlign =  HorizontalAlign.Right;
                    e.Row.Cells[12].Text = (String.Format("{0:c}", drv["Total"])).ToString();
                    e.Row.Cells[12].HorizontalAlign =  HorizontalAlign.Right;
                    //67811A0 - PCI Remediation for Payment systems CH1: End Change Column Index to accomodate State and Prefix column
					break;
			}
		}


		// EVENT HANDLER: ItemDataBound
        public void ItemDataBound2(Object sender, GridViewRowEventArgs e)
		{
			// Retrieve the data linked through the relation
			// Given the structure of the data ONLY ONE row is retrieved
			DataRowView drv = (DataRowView) e.Row.DataItem;
			if (drv == null)
				return;
		}


        public void PageIndexChanged1(Object sender, GridViewPageEventArgs e)
        {		//Modified as a part of .NetMig 3.5
            if (e.NewPageIndex == -1)
            {  
                dgReport1.PageIndex = 0;
                //commented as a part of .NetMig 3.5
                //dgReport1.PageIndex = e.NewPageIndex;
                //dgReport1.CurrentPageIndex = e.NewPageIndex;
            }
            else
            {
                dgReport1.PageIndex = e.NewPageIndex;
            }
            UpdateDataView();
            //dgReport1.PageIndex = e.NewPageIndex;
            //UpdateDataView();
		}
        
        public void PageIndexChanged2(Object sender, GridViewPageEventArgs e)
        {	//Modified as a part of .NetMig 3.5
            if (e.NewPageIndex == -1)
            {
                dgReport2.PageIndex = 0;
            }
            else
            {
                dgReport2.PageIndex = e.NewPageIndex;
            }
            UpdateDataView();
            //dgReport2.PageIndex = e.NewPageIndex;
            //UpdateDataView();
		}
		
		private void UpdateDataView()
		{
			// Retrieves the data
			DataSet ods = (DataSet) Session["MyDataSet"];
			
			if (ods == null)
			{
				rptLib.SetMessage(lblErrMsg,"Your session has timed out. Please re-submit the page", true);
				return;
			}

			DataView dv1 = ods.Tables[0].DefaultView;
            //PC Phase II Changes CH3- Commented the below code to display Report Types for only Summary and Detail.
			//DataView dv2 = ods.Tables[1].DefaultView;
				
			string orpt = (string) Session["MyReportType"]; 

			int pageCnt  = 1;
			int currPage = 1;

			// Re-bind data 

			if (ods.Tables[0].Rows.Count > 0)
			{
				try
				{
					dgReport1.DataSource = dv1;
					dgReport1.DataBind();
					dgReport1.Visible = true;
					pageCnt  = dgReport1.PageCount;
					currPage = dgReport1.PageIndex+1;
					if (ods.Tables[0].Rows.Count >= 500 && orpt == "6") rptLib.SetMessage(lblErrMsg, "The first 500 rows are displayed.", true);
				}
				catch  (Exception ex) 
				{
					Logger.Log(ex);
				}
			}
            // Begin PC Phase II Changes CH3- Commented the below code to display Report Types for only Summary and Detail.
            //if (ods.Tables[1].Rows.Count > 0)
            //{
            //    try
            //    {
            //        dgReport2.DataSource = dv2;
            //        dgReport2.DataBind();
            //        dgReport2.Visible = true;
            //        if (pageCnt < dgReport2.PageCount) pageCnt  = dgReport2.PageCount;
            //        if (currPage < dgReport2.PageIndex+1) currPage = dgReport2.PageIndex+1;

            //    }
            //    catch  (Exception ex) 
            //    {
            //        Logger.Log(ex);
            //    }
            //}
            //End PC Phase II Changes CH3- Commented the below code to display Report Types for only Summary and Detail.
			if (pageCnt > 1)
            {	//commented as a part of .NetMig 3.5			
				//dgReport1.PagerStyle.Visible = true;
				//if (orpt == "7") dgReport2.PagerStyle.Visible = true;
			}

			if (pageCnt < 2)
			{//bhnau
				//dgReport1.PagerStyle.Visible = false;
				//dgReport2.PagerStyle.Visible = false;
			}
	
			lblPageNum1.Text = "Showing Page " + currPage.ToString() + " of " + pageCnt.ToString();
			lblPageNum2.Text = "Showing Page " + currPage.ToString() + " of " + pageCnt.ToString();
            //Added as a part of the .Net Mig 3.5 to disable the prev link button on 02-10-2010.
            if (currPage == 1)
            {
                System.Web.UI.WebControls.LinkButton PageIndicatorLinkPrevBottom = (System.Web.UI.WebControls.LinkButton)dgReport1.BottomPagerRow.FindControl("vPrev");
                System.Web.UI.WebControls.LinkButton PageIndicatorLinkPrevTop = (System.Web.UI.WebControls.LinkButton)dgReport1.TopPagerRow.FindControl("vPrev");
                if (PageIndicatorLinkPrevBottom != null && PageIndicatorLinkPrevTop != null)
                {
                    PageIndicatorLinkPrevBottom.Enabled = false;
                    PageIndicatorLinkPrevTop.Enabled = false;
                }

            }
            if (currPage == pageCnt)
            {
                System.Web.UI.WebControls.LinkButton PageIndicatorLinkNextBottom = (System.Web.UI.WebControls.LinkButton)dgReport1.BottomPagerRow.FindControl("vNext");
                System.Web.UI.WebControls.LinkButton PageIndicatorLinkNextTop = (System.Web.UI.WebControls.LinkButton)dgReport1.TopPagerRow.FindControl("vNext");
                if (PageIndicatorLinkNextBottom != null && PageIndicatorLinkNextTop != null)
                {
                    PageIndicatorLinkNextBottom.Enabled = false;
                    PageIndicatorLinkNextTop.Enabled = false;
                }
            }
           

		}

		private void SetHeaders()
		{
			lblReportTitle.Text = report_type.SelectedItem.Text;
				
			string UserNames = UserList.SelectedItem.Text.Replace(" - " + UserList.SelectedItem.Value," ");
			//For display on the start page.
            //CHG0110069 - CH1 - BEGIN - Appended the /Time along with the Date Label
			lblDate.Text = "Run Date/Time:";
			lblDates.Text = "Date/Time Range:";
            //CHG0110069 - CH1 - END - Appended the /Time along with the Date Label
			lblCsr.Text = "Users:";
			lblApp.Text = "Application";

			//Code Added by Cognizant
			lblPayment.Text = "Payment Type";
			//Code Added by Cognizant

			lblProduct.Text = "Product Type";
			lblRevenue.Text = "Revenue Type";
            lblStatus.Text = "Status";
            //CHG0109406 - CH1 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
            //Start-TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time and DateRange by cognizant.           
            lblRunDate.Text = DateTime.Now.ToString() + " " + "Arizona";
            //PC Phase II changes CH2 - Start - Added the below code to Handle Date Time Picker 
            lblDateRange.Text = TextstartDate.Text + " - " + TextendDate.Text + " " + "Arizona";
            //PC Phase II changes CH2 - End - Added the below code to Handle Date Time Picker 
            //End-TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time and DateRange by cognizant.
            //CHG0109406 - CH1 - END - Modified the timezone from "MST Arizona" to Arizona
			lblCsrs.Text =  UserNames;
			lblAppName.Text = "All";

			//Code Added by Cognizant
			lblPaymentName.Text = PaymentType.PaymentTypeText;
			//Code Added by Cognizant

			lblProductName.Text = RevenueProduct.ProductTypeText;
			lblRevenueName.Text = RevenueProduct.RevenueTypeText;
            lblStatusName.Text = Status.SelectedItem.Text;

			// For printing and converting to Excel only, folowing session variables are declared.
			// Not a good way. Will try to remove them later.
					
			Session["RptTitle"] = report_type.SelectedItem.Text;
            //PC Phase II changes CH2 - Start - Added the below code to Handle Date Time Picker 
            Session["DateRange"] = TextstartDate.Text + " - " + TextendDate.Text;
            //PC Phase II changes CH2 - End - Added the below code to Handle Date Time Picker 
			Session["CSRs"]		= UserNames;
			Session["AppName"] = "All";
			Session["ProductName"] = RevenueProduct.ProductTypeText;
			Session["RevenueName"] = RevenueProduct.RevenueTypeText;

			//Code Added by Cognizant
			Session["PaymentName"] = PaymentType.PaymentTypeText;
            //Code Added by Cognizant

			//STAR AUTO 2.1.Ch1: START - Added a session variable RepDOName with null value to avoid displaying RepDO in Print and Excel version
			Session["RepDOName"] = null;
			//STAR AUTO 2.1.Ch1: END
            //Added by Cognizant
            Session["StatusType"] = Status.SelectedItem.Text;
		
		}


		private void VisibleControls()
		{
			pnlTitle.Visible = true;
			lblPageNum1.Visible = true;
			lblPageNum2.Visible = true;
		}
		private void InvisibleControls()
		{
			lblErrMsg.Visible = true;
			//lblErrMsg.Text = "No Data Found. Please specify a valid criteria.";
			lblPageNum1.Visible = false;
			lblPageNum2.Visible = false;
	
		}

		private void InvisibleBarts()
		{
			dgReport1.Visible = false;
			dgReport2.Visible = false;
           lblErrMsg.Visible = false;
			lblAPDS.Visible	  = false;
			lblCyber.Visible = false;
			pnlTitle.Visible = false;

		}

	}
}

