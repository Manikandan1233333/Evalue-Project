/*
 * Modified as a part of STAR Auto 2.1 on 08/14/2007:
 * STAR AUTO 2.1.Ch1: Added label RepDO in the header of the result grid
 * STAR AUTO 2.1.Ch2: Added a session variable to store the selected RepDO value
 * 
 * PT.Ch1 -- Modified by COGNIZANT on 9/8/2008
 * * Commented few lines to enable LightYellow alternate item color for report types 4 & 5
 * Modified by cognizant as a partt of .NetMigration 3.5 on 04-14-2010.
 * .NetMig 3.5:Modified the btn_submit pageindexchanged1 and Update data view methods to display the results properly.
 * MODIFIED AS A PART OF TIME ZONE CHANGE ENHANCEMENT ON 8-31-2010
 * TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time and Run Date  by cognizant.
 * 67811A0 - PCI Remediation for Payment systems CH1: Change Column Index to accomodate State and Prefix column
 * 67811A0 - PCI Remediation for Payment systems CH2: Added to clear the cache of the page to prevent the page loading on browser back button hit 
 * PC Phase II changes CH1 - Added the below code to include the status value to report criteria 
 * PC Phase II changes CH2 - Added the below code to Handle Date Time Picker 
 * PC Phase II Changes CH3- Commented the below code to display Report Types for only Summary and Detail.
 * PC Phase II 4/20 - Added logging in below code to find the user ID and search criteria for the insurance reports, Transaction search 
 * MAIG - CH1 - Added variable to store the Ticket details
 * MAIG - CH2 - Settlement Report Changes - Initializing the ActivityDate filter to Previous day
 * MAIG - CH3 - Added code to hide all the panels
 * MAIG - CH4 - Added code to fetch the AgencyID, RoleName
 * MAIG - CH5 - Added code to Serialize the SSRS Input for logging
 * MAIG - CH6 - Added code to initilaize the SSRS Reports Utility
 * MAIG - CH7 - Added code to include the PS User Reports
 * CHG0109406 - CH1 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH2 - Set the label lblHeaderTimeZone to display the timezone for the results displayed
 * CHG0109406 - CH3 - Set the below label visible property to false which displays the timezone in results
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
using System.Data.SqlClient;
using System.Configuration;
using CSAAWeb;
using CSAAWeb.Serializers;
using CSAAWeb.AppLogger;
using InsuranceClasses;
using AuthenticationClasses;
using OrderClassesII;
using System.Collections.Generic;
using System.Xml.Serialization;
using System.IO;
using System.Xml;
using System.Text;

namespace MSCR.Reports
{
	/// <summary>
	/// Summary description for InsReport1.
	/// </summary>
	public partial class ins_reports : CSAAWeb.WebControls.PageTemplate {
		protected System.Web.UI.HtmlControls.HtmlForm frmReports;
        int rpt = 0;

		//protected MSCR.Controls.Dates Dates;
		protected MSCR.Controls.ReportType ReportType;
		protected MSCR.Controls.RevenueProduct RevenueProduct;

		//Added By Cognizant for Payment Type
		protected MSCR.Controls.PaymentTypeControl PaymentType;
		//Added By Cognizant for Payment Type

		protected MSCR.Controls.Users Users;
        //MAIG - CH1 - BEGIN - Added variable to store the Ticket details
        public string[] userData  { get; set; }
        //MAIG - CH1 - END - Added variable to store the Ticket details
		protected System.Drawing.Color clr = Color.White;

		//Added By Cognizant for Payment Type
		//Added By Cognizant for Payment Type
		
		InsRptLibrary rptLib = new InsRptLibrary();

		protected void Page_Load(object sender, System.EventArgs e)
		{
             //67811A0 - PCI Remediation for Payment systems CH2 START: Added to clear the cache of the page to prevent the page loading on browser back button hit 
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            // 67811A0 - PCI Remediation for Payment systems CH2 END: Added to clear the cache of the page to prevent the page loading on browser back button hit 
			//_GeneratePrintScript();
			// Put user code to initialize the page here
			//Page.RegisterHiddenField( "__EVENTTARGET", btnSubmit.ClientID );
			if (!Page.IsPostBack)
			{	
				pnlTitle.Visible = false;
                //PC Phase II changes CH2 - Start - Modified the below code to Handle Date Time Picker

                //string now = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss");
                DateTime now = DateTime.Now;
                DateTime startdate = new DateTime(now.Year, now.Month, 1);
                TextstartDt.Text = startdate.ToString("MM-dd-yyyy HH:mm:ss");

                //MAIG - CH2 - BEGIN - Settlement Report Changes - Initializing the ActivityDate filter to Previous day
                TextActivityDt.Text = DateTime.Today.AddDays(-1).ToString("MM-dd-yyyy HH:mm:ss");
                //MAIG - CH2 - END - Settlement Report Changes - Initializing the ActivityDate filter to Previous day

                DateTime enddate = DateTime.Today;
                TextendDt.Text = enddate.ToString("MM-dd-yyyy")+ " 23:59:59";
                //PC Phase II changes CH2 - End - Modified the below code to Handle Date Time Picker
                //MAIG - CH3 - BEGIN - Added code to hide all the panels
                pnlInsuranceReports.Visible = false;
                pnlLocations.Visible = false;
                pnlAgency.Visible = false;
                pnlUsers.Visible = false;
                pnldisplayType.Visible = false;
	              //MAIG - CH3 - END - Added code to hide all the panels
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
				//MAIG - CH4 - BEGIN - Added code to fetch the AgencyID, RoleName
                //OrderClasses.Service.Order authUserObj = new OrderClasses.Service.Order(Page);
                //objUserInfo = authUserObj.GetUserInfo(this.Page.User.Identity.Name.ToString());
                userData= ((System.Web.Security.FormsIdentity)(this.Page.User.Identity)).Ticket.UserData.Split(';');
                if (userData.Length > 0)
                {
                    User.AgencyID = userData[userData.Length - 1].ToString();
                    AgencyType.AgencyID = userData[userData.Length - 1].ToString();
                    AgencyType.RoleName = userData[0].ToString();
                    LocType.AgencyID = userData[userData.Length - 1].ToString();
                }
				//MAIG - CH4 - END - Added code to fetch the AgencyID, RoleName
			MSCR.Controls.ReportType ReportType = (MSCR.Controls.ReportType)FindControl("ReportType");
			ReportType.DataBind();
            //MSCR.Controls.Dates Dates = (MSCR.Controls.Dates)FindControl("Dates");
            //Dates.DataBind();
			MSCR.Controls.RevenueProduct RevenueProduct = (MSCR.Controls.RevenueProduct)FindControl("RevenueProduct");
			RevenueProduct.DataBind();
			MSCR.Controls.Users Users = (MSCR.Controls.Users)FindControl("Users");
			Users.DataBind();
			MSCR.Controls.PaymentTypeControl PaymentType =(MSCR.Controls.PaymentTypeControl)FindControl("PaymentType");
			PaymentType.DataBind();

		}

		//MAIG - CH5 - BEGIN - Added code to Serialize the SSRS Input for logging
        public static string SerializeToXmlString<T>(T ToSerialize)
        {
            string xmlstream = String.Empty;

            using (MemoryStream memstream = new MemoryStream())
            {
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(T));
                XmlTextWriter xmlWriter = new XmlTextWriter(memstream, Encoding.UTF8);

                xmlSerializer.Serialize(xmlWriter, ToSerialize);
                xmlstream = UTF8ByteArrayToString(((MemoryStream)xmlWriter.BaseStream).ToArray());
            }

            return xmlstream;
        }

        // Convert Array to String
        public static String UTF8ByteArrayToString(Byte[] ArrBytes)
        { return new UTF8Encoding().GetString(ArrBytes); }
		//MAIG - CH5 - END - Added code to Serialize the SSRS Input for logging
		protected void btnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
            ReportParameter tranStartDate = new ReportParameter();
            ReportParameter tranEndDate = new ReportParameter();
			try
			{
			//MAIG - CH6 - BEGIN - Added code to initilaize the SSRS Reports Utility
                ReportGeneratorUtilHelper helper = new ReportGeneratorUtilHelper();
                if (!(ReportType.ReportTypeValue.Equals("1") || ReportType.ReportTypeValue.Equals("2")))
                {
                    
                    tranStartDate.Name = "prmTransDateFrom";
                    tranStartDate.Value = TextstartDt.Text;
                    tranStartDate.Type = "DateTime";

                    tranEndDate.Name = "prmTransDateTo";
                    tranEndDate.Value = TextendDt.Text;
                    tranEndDate.Type = "DateTime";
                }
                //OrderClasses.Service.Order authUserObj = new OrderClasses.Service.Order();
                //UserInfo userInfoObj = new UserInfo();
                //userInfoObj = authUserObj.Authenticate(HttpContext.Current.User.Identity.Name, string.Empty, CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL);

                if ((ReportType.ReportTypeValue == "1") || (ReportType.ReportTypeValue == "2"))
                {
			//MAIG - CH6 - END - Added code to initilaize the SSRS Reports Utility
                    //Couldn't help naming one proc after Bart who is fixated on the appearance and disappearance of controls.
				    InvisibleBarts();											
    	
				    OrderClasses.ReportCriteria rptCrit1 = new OrderClasses.ReportCriteria();
                    //PC Phase II changes CH2 - Start - Added the below code to Handle Date Time Picker 
                    rptCrit1.ReportType = Convert.ToInt16(ReportType.ReportTypeValue);
                    rpt=Convert.ToInt16(ReportType.ReportTypeValue);
                    rptCrit1.StartDate = Convert.ToDateTime(TextstartDt.Text);
                    rptCrit1.EndDate = Convert.ToDateTime(TextendDt.Text);
                    //PC Phase II changes CH2 - End - Added the below code to Handle Date Time Picker 
				    rptCrit1.ProductType = RevenueProduct.ProductType;
				    rptCrit1.RevenueType = RevenueProduct.RevenueType;
                    
				    //Added by Cognizant
				    rptCrit1.PaymentType = PaymentType.PaymentType;
				    //Added by Cognizant
				    rptCrit1.App = Users.IdApp;
				    //rptCrit1.Role = Users.Role;
				    rptCrit1.RepDO = Users.RepDO;
				    rptCrit1.Users = Users.UserNames(false);
                    //PC Phase II changes CH1 - Added the below code to include the status value to report criteria 
                    rptCrit1.Status = status.SelectedItem.Value;

				    bool bValid = rptLib.ValidateDate(lblErrMsg, rptCrit1.StartDate, rptCrit1.EndDate,rptCrit1.ReportType);
				    if (bValid == true) 
				    {
					    InvisibleControls();
					    return;
				    }
                    if (ReportType.ReportTypeValue == "2")
                    {
                        TimeSpan ts = Convert.ToDateTime(TextendDt.Text) - Convert.ToDateTime(TextstartDt.Text);
                        if (ts.Days > 2)
                        {
                            InvisibleControls();
                            rptLib.SetMessage(lblErrMsg, "Please limit the date range to not more than 2 Days.", true);
                            return;
                        }
                    }

				    if (rptCrit1.Users.Length > 2000)
				    {
					    rptLib.SetMessage(lblErrMsg,"The total length of users should not exceed 2000 characters", true);
					    InvisibleControls();
					    return;
				    }
    									
				    //Call the insurance web service
				    InsuranceClasses.Service.Insurance InsSvc = new InsuranceClasses.Service.Insurance();
    				
				    // Create and Fill the DataSet
                    //PC Phase II 4/20 - Added logging in below code to find the user ID and search criteria for the insurance reports, Transaction search 
                       Logger.Log(CSAAWeb.Constants.INS_REPORT_USERID_ALL + Page.User.Identity.Name.ToString() + CSAAWeb.Constants.PARAMETER + rptCrit1);
				    DataSet ds = InsSvc.GetInsuranceReports(rptCrit1);
                    Logger.Log(CSAAWeb.Constants.INS_REPORT_DATASET_ALL);
                    //PC Phase II 4/20 - Added logging in below code to find the user ID and search criteria for the insurance reports, Transaction search 
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
                    if (ReportType.ReportTypeValue == "2")
                    {
                        lblHeaderTimeZone.Visible = true;
                    }
                    //CHG0109406 - CH2 - END - Set the label lblHeaderTimeZone to display the timezone for the results displayed
				    Session.Timeout = Convert.ToInt32(Config.Setting("SessionTimeOut"));
				    Session["MyDataSet"] = ds;
				    Session["MyReportType"] = ReportType.ReportTypeValue;
                    dgReport1.PageIndex = 0;
                    //commented as a part of .NetMig 3.5
				    //dgReport1.CurrentPageIndex = 0;
				    dgReport2.PageIndex = 0;
                   
				    UpdateDataView();
				//MAIG - CH7 - BEGIN - Added code to include the PS USer Reports
                }
                else if((ReportType.ReportTypeValue == "3") || (ReportType.ReportTypeValue == "4"))
                {

                    bool bValid = rptLib.ValidateDate(lblErrMsg, Convert.ToDateTime(TextstartDt.Text), Convert.ToDateTime(TextendDt.Text), Convert.ToInt32(ReportType.ReportTypeValue));
                    if (bValid == true)
                    {
                        InvisibleControls();
                        return;
                    }
                    if (ReportType.ReportTypeValue == "4")
                    {
                        TimeSpan ts = Convert.ToDateTime(TextendDt.Text) - Convert.ToDateTime(TextstartDt.Text);
                        if (ts.Days > 2)
                        {
                            InvisibleControls();
                            rptLib.SetMessage(lblErrMsg, "Please limit the date range to not more than 2 Days.", true);
                            return;
                        }
                        else
                        {
                            lblErrMsg.Text = string.Empty;
                        }
                    }

                    TextBox txtAgencyID = (TextBox)this.AgencyType.FindControl("_txtAgency");
                    if (string.IsNullOrEmpty(txtAgencyID.Text))
                    {
                        InvisibleControls();
                        rptLib.SetMessage(lblErrMsg, "Agency ID should not be empty", true);
                        return;
                        /*SSRSReportRequest reportRequest = new SSRSReportRequest();
                        reportRequest.ReportDetails = new List<ReportDetail>();
                        ReportDetail detail = new ReportDetail();
                        detail.SkipForProcessing = false;
                        detail.ReportOffset = "-1";
                        detail.ReportPath = Config.Setting("ReportPath");
                        if (ReportType.ReportTypeValue == "3")
                        {
                            detail.ReportName = Config.Setting("ReportSummaryIdentifier_AgencyName_ALL");
                            detail.ReportDetailIdentifier = Config.Setting("ReportSummaryIdentifier_AgencyName_ALL");
                        }
                        else
                        {
                            detail.ReportName = Config.Setting("ReportDetailIdentifier_AgencyName_ALL");
                            detail.ReportDetailIdentifier = Config.Setting("ReportDetailIdentifier_AgencyName_ALL");
                        }
                        reportRequest.ReportDetails.Add(detail);
                        DistributionDetail resType = new DistributionDetail();
                        resType.OutputFileType = rbDisplayType.SelectedItem.Text;
                        if (ReportType.ReportTypeValue == "3")
                        {
                            resType.FileName = Config.Setting("ReportSummaryIdentifier_AgencyName_ALL") + DateTime.Now;
                        }
                        else
                        {
                            resType.FileName = Config.Setting("ReportDetailIdentifier_AgencyName_ALL") + DateTime.Now;
                        }
                        reportRequest.ReportDetails[0].Distributions.Add(resType);
                        ReportParameter tranStartDate = new ReportParameter();
                        tranStartDate.Name = "prmTransDateFrom";
                        tranStartDate.Value = TextstartDt.Text;
                        tranStartDate.Type = "DateTime";
                        reportRequest.ReportDetails[0].Parameters.Add(tranStartDate);

                        ReportParameter tranEndDate = new ReportParameter();
                        tranEndDate.Name = "prmTransDateTo";
                        tranEndDate.Value = TextendDt.Text;
                        tranEndDate.Type = "DateTime";
                        reportRequest.ReportDetails[0].Parameters.Add(tranEndDate);

                        ReportParameter agencyId = new ReportParameter();
                        agencyId.Name = "prmAgency";
                        agencyId.Value = txtAgencyID.Text.Trim();
                        agencyId.Type = "String";
                        reportRequest.ReportDetails[0].Parameters.Add(agencyId);
                        if (Config.Setting("Logging.SSRSReport")=="1")
                        {
                            Logger.Log("Generating report for" + ReportType.ReportTypeText);
                            Logger.Log(SerializeToXmlString(reportRequest.ReportDetails[0].Parameters));
                        }
                        byte[] Result = null;
                        Result = helper.GenerateAndExecuteDelivery(reportRequest.ReportDetails);

                        if (Result != null)
                        {
                            string fileName = reportRequest.ReportDetails[0].Distributions[0].FileName;
                            fileName += helper.GetFileExtension(reportRequest.ReportDetails[0].Distributions[0].OutputFileType);

                            Response.Buffer = true;
                            Response.Clear();
                            Response.ContentType = resType.OutputFileType;
                            Response.AddHeader("content-disposition", "attachment; filename=" + fileName);
                            Response.BinaryWrite(Result);
                            Response.Flush();
                            Response.End();
                            InvisibleBarts();
                            
                        }
                        else
                        {
                            rptLib.SetMessage(lblErrMsg, "No Reports Found. Please specify a valid criteria.", true);
                            InvisibleControls();
                            return;

                        }*/
                    }
                    //else if (!CSAAWeb.Validate.IsDecimal(txtAgencyID.Text))
                    //{
                    //    InvisibleControls();
                    //    rptLib.SetMessage(lblErrMsg, "Agency ID should be numeric", true);
                    //    return;
                    //}
                    else
                    {
                        SSRSReportRequest reportRequest = new SSRSReportRequest();
                        reportRequest.ReportDetails = new List<ReportDetail>();
                        ReportDetail detail = new ReportDetail();
                        detail.SkipForProcessing = false;
                        detail.ReportOffset = "-1";
                        detail.ReportPath = Config.Setting("ReportPath");
                        if (ReportType.ReportTypeValue == "3")
                        {
                            detail.ReportName = Config.Setting("ReportSummarydentifier_AgencyName_Multiple");
                            detail.ReportDetailIdentifier = Config.Setting("ReportSummarydentifier_AgencyName_Multiple");
                        }
                        else
                        {
                            detail.ReportName = Config.Setting("ReportDetailIdentifier_AgencyName_Multiple");
                            detail.ReportDetailIdentifier = Config.Setting("ReportDetailIdentifier_AgencyName_Multiple");
                        }
                        reportRequest.ReportDetails.Add(detail);
                        DistributionDetail resType = new DistributionDetail();
                        resType.OutputFileType = rbDisplayType.SelectedItem.Text;
                        if (ReportType.ReportTypeValue == "3")
                        {
                            resType.FileName = Config.Setting("ReportSummarydentifier_AgencyName_Multiple") + DateTime.Now;
                        }
                        else
                        {
                            resType.FileName = Config.Setting("ReportDetailIdentifier_AgencyName_Multiple") + DateTime.Now;
                        }
                        reportRequest.ReportDetails[0].Distributions.Add(resType);
                        reportRequest.ReportDetails[0].Parameters.Add(tranStartDate);

                        reportRequest.ReportDetails[0].Parameters.Add(tranEndDate);

                        ReportParameter agencyId = new ReportParameter();
                        agencyId.Name = "prmAgency";
                        agencyId.Value = txtAgencyID.Text.Trim();
                        agencyId.Type = "String";
                        reportRequest.ReportDetails[0].Parameters.Add(agencyId);
                        if (Config.Setting("Logging.SSRSReport") == "1")
                        {
                            Logger.Log("Generating report for" + ReportType.ReportTypeText + ", for LoggedIn User:" + Page.User.Identity.Name.ToString());
                            Logger.Log(SerializeToXmlString(reportRequest.ReportDetails[0].Parameters));
                        }
                        byte[] Result = null;
                        Result = helper.GenerateAndExecuteDelivery(reportRequest.ReportDetails);

                        if (Result != null)
                        {
                            string fileName = reportRequest.ReportDetails[0].Distributions[0].FileName;
                            fileName += helper.GetFileExtension(reportRequest.ReportDetails[0].Distributions[0].OutputFileType);
                            HttpResponse response = HttpContext.Current.Response;
                            response.Clear();
                            response.ClearContent();
                            response.ClearHeaders();
                            response.Buffer = true;
                            response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                            Response.BinaryWrite(Result);
                            InvisibleBarts();
                        }
                        else
                        {
                            rptLib.SetMessage(lblErrMsg, "No Data Found. Please specify a valid criteria.", true);
                            InvisibleControls();
                            return;
                        }
                    }
                }
                else if((ReportType.ReportTypeValue == "5") || (ReportType.ReportTypeValue == "6"))
                {
                    bool bValid = rptLib.ValidateDate(lblErrMsg, Convert.ToDateTime(TextstartDt.Text), Convert.ToDateTime(TextendDt.Text), Convert.ToInt32(ReportType.ReportTypeValue));
                    if (bValid == true)
                    {
                        InvisibleControls();
                        return;
                    }
                    if (ReportType.ReportTypeValue == "6")
                    {
                        TimeSpan ts = Convert.ToDateTime(TextendDt.Text) - Convert.ToDateTime(TextstartDt.Text);
                        if (ts.Days > 2)
                        {
                            InvisibleControls();
                            rptLib.SetMessage(lblErrMsg, "Please limit the date range to not more than 2 Days.", true);
                            return;
                        }
                    }
                    ListBox dstUser = (ListBox)this.User.FindControl("_UserList");
                    if (dstUser.SelectedItem.Text.ToUpper() == "ALL")
                    {
                        SSRSReportRequest reportRequest = new SSRSReportRequest();
                        reportRequest.ReportDetails = new List<ReportDetail>();
                        ReportDetail detail = new ReportDetail();
                        detail.SkipForProcessing = false;
                        detail.ReportOffset = "-1";
                        detail.ReportPath = Config.Setting("ReportPath");
                        if (ReportType.ReportTypeValue == "5")
                        {
                            detail.ReportName = Config.Setting("ReportSummaryIdentifier_User_ALL");
                            detail.ReportDetailIdentifier = "ReportSummaryIdentifier_User_ALL";
                        }
                        else
                        {
                            detail.ReportName = Config.Setting("ReportDetailIdentifier_User_ALL");
                            detail.ReportDetailIdentifier = "ReportDetailIdentifier_User_ALL";
                        }
                        reportRequest.ReportDetails.Add(detail);
                        DistributionDetail resType = new DistributionDetail();
                        resType.OutputFileType = rbDisplayType.SelectedItem.Text;
                        if (ReportType.ReportTypeValue == "5")
                        {
                            resType.FileName = Config.Setting("ReportSummaryIdentifier_User_ALL") + DateTime.Now;
                        }
                        else
                        {
                            resType.FileName = Config.Setting("ReportDetailIdentifier_User_ALL") + DateTime.Now;
                        }
                        reportRequest.ReportDetails[0].Distributions.Add(resType);

                        reportRequest.ReportDetails[0].Parameters.Add(tranStartDate);

                        reportRequest.ReportDetails[0].Parameters.Add(tranEndDate);

                        ReportParameter agencyId = new ReportParameter();
                        agencyId.Name = "prmAgency";
                        agencyId.Value = userData[userData.Length-1].ToString().Trim();
                        agencyId.Type = "String";
                        reportRequest.ReportDetails[0].Parameters.Add(agencyId);
                        if (Config.Setting("Logging.SSRSReport") == "1")
                        {
                            Logger.Log("Generating report for" + ReportType.ReportTypeText + ", for LoggedIn User:" + Page.User.Identity.Name.ToString());
                            Logger.Log(SerializeToXmlString(reportRequest.ReportDetails[0].Parameters));
                        }
                        byte[] Result = null;
                        Result = helper.GenerateAndExecuteDelivery(reportRequest.ReportDetails);

                        if (Result != null)
                        {
                            string fileName = reportRequest.ReportDetails[0].Distributions[0].FileName;
                            fileName += helper.GetFileExtension(reportRequest.ReportDetails[0].Distributions[0].OutputFileType);
                            HttpResponse response = HttpContext.Current.Response;
                            response.Clear();
                            response.ClearContent();
                            response.ClearHeaders();
                            response.Buffer = true;
                            response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                            Response.BinaryWrite(Result);
                            InvisibleBarts();
                        }
                        else
                        {
                            rptLib.SetMessage(lblErrMsg, "No Data Found. Please specify a valid criteria.", true);
                            InvisibleControls();
                            return;
                        }
                    }
                    else
                    {
                        System.Collections.Generic.List<string> selectedItemsList = new System.Collections.Generic.List<string>();
                        foreach (ListItem item in dstUser.Items)
                            if (item.Selected)
                                selectedItemsList.Add(item.Value.Split('-')[0]);
                        //string selectedUserID = string.Join(",", selectedItemsList.ToArray());
                        string selectedUserID = selectedItemsList[0].ToString();
                        SSRSReportRequest reportRequest = new SSRSReportRequest();
                        reportRequest.ReportDetails = new List<ReportDetail>();
                        ReportDetail detail = new ReportDetail();
                        detail.SkipForProcessing = false;
                        detail.ReportOffset = "-1";
                        detail.ReportPath = Config.Setting("ReportPath");
                        if (ReportType.ReportTypeValue == "5")
                        {
                            detail.ReportName = Config.Setting("ReportSummaryIdentifier_User_Multiple");
                            detail.ReportDetailIdentifier = "ReportSummaryIdentifier_User_Multiple";
                        }
                        else
                        {
                            detail.ReportName = Config.Setting("ReportDetailIdentifier_User_Multiple");
                            detail.ReportDetailIdentifier = "ReportDetailIdentifier_User_Multiple";
                        }
                        reportRequest.ReportDetails.Add(detail);
                        DistributionDetail resType = new DistributionDetail();
                        resType.OutputFileType = rbDisplayType.SelectedItem.Text;
                        if (ReportType.ReportTypeValue == "5")
                        {
                            resType.FileName = Config.Setting("ReportSummaryIdentifier_User_Multiple") + DateTime.Now;
                        }
                        else
                        {
                            resType.FileName = Config.Setting("ReportDetailIdentifier_User_Multiple") + DateTime.Now;
                        }
                        reportRequest.ReportDetails[0].Distributions.Add(resType);

                        reportRequest.ReportDetails[0].Parameters.Add(tranStartDate);

                        reportRequest.ReportDetails[0].Parameters.Add(tranEndDate);

                        ReportParameter userId = new ReportParameter();
                        userId.Name = "prmUserId";
                        userId.Value = selectedUserID.Trim();
                        userId.Type = "String";
                        reportRequest.ReportDetails[0].Parameters.Add(userId);

                        ReportParameter agencyId = new ReportParameter();
                        agencyId.Name = "prmAgency";
                        agencyId.Value = userData[userData.Length-1].ToString().Trim();
                        agencyId.Type = "String";
                        reportRequest.ReportDetails[0].Parameters.Add(agencyId);
                        if (Config.Setting("Logging.SSRSReport") == "1")
                        {
                            Logger.Log("Generating report for" + ReportType.ReportTypeText + ", for LoggedIn User:" + Page.User.Identity.Name.ToString());
                            Logger.Log(SerializeToXmlString(reportRequest.ReportDetails[0].Parameters));
                        }
                        byte[] Result = null;
                        Result = helper.GenerateAndExecuteDelivery(reportRequest.ReportDetails);

                        if (Result != null)
                        {
                            string fileName = reportRequest.ReportDetails[0].Distributions[0].FileName;
                            fileName += helper.GetFileExtension(reportRequest.ReportDetails[0].Distributions[0].OutputFileType);
                            HttpResponse response = HttpContext.Current.Response;
                            response.Clear();
                            response.ClearContent();
                            response.ClearHeaders();
                            response.Buffer = true;
                            response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                            Response.BinaryWrite(Result);
                            InvisibleBarts();
                        }
                        else
                        {
                            rptLib.SetMessage(lblErrMsg, "No Data Found. Please specify a valid criteria.", true);
                            InvisibleControls();
                            return;
                        }
                    }
                }
                else if ((ReportType.ReportTypeValue == "7") || (ReportType.ReportTypeValue == "8"))
                {
                    bool bValid = rptLib.ValidateDate(lblErrMsg, Convert.ToDateTime(TextstartDt.Text), Convert.ToDateTime(TextendDt.Text), Convert.ToInt32(ReportType.ReportTypeValue));
                    if (bValid == true)
                    {
                        InvisibleControls();
                        return;
                    }
                    if (ReportType.ReportTypeValue == "8")
                    {
                        TimeSpan ts = Convert.ToDateTime(TextendDt.Text) - Convert.ToDateTime(TextstartDt.Text);
                        if (ts.Days > 2)
                        {
                            InvisibleControls();
                            rptLib.SetMessage(lblErrMsg, "Please limit the date range to not more than 2 Days.", true);
                            return;
                        }
                    }
                    ListBox dstLocation = (ListBox)this.LocType.FindControl("_Locations");
                    if (dstLocation.SelectedItem.Text.ToUpper() == "ALL")
                    {
                        SSRSReportRequest reportRequest = new SSRSReportRequest();
                        reportRequest.ReportDetails = new List<ReportDetail>();
                        ReportDetail detail = new ReportDetail();
                        detail.SkipForProcessing = false;
                        detail.ReportOffset = "-1";
                        detail.ReportPath = Config.Setting("ReportPath");
                        if (ReportType.ReportTypeValue == "7")
                        {
                            detail.ReportName = Config.Setting("ReportSummaryIdentifier_AgencyLocation_ALL");
                            detail.ReportDetailIdentifier = Config.Setting("ReportSummaryIdentifier_AgencyLocation_ALL");
                        }
                        else
                        {
                            detail.ReportName = Config.Setting("ReportDetailIdentifier_AgencyLocation_ALL");
                            detail.ReportDetailIdentifier = Config.Setting("ReportDetailIdentifier_AgencyLocation_ALL");
                        }
                        reportRequest.ReportDetails.Add(detail);
                        DistributionDetail resType = new DistributionDetail();
                        resType.OutputFileType = rbDisplayType.SelectedItem.Text;
                        if (ReportType.ReportTypeValue == "7")
                        {
                            resType.FileName = Config.Setting("ReportSummaryIdentifier_AgencyLocation_ALL") + DateTime.Now;
                        }
                        else
                        {
                            resType.FileName = Config.Setting("ReportDetailIdentifier_AgencyLocation_ALL") + DateTime.Now;
                        }
                        reportRequest.ReportDetails[0].Distributions.Add(resType);

                        reportRequest.ReportDetails[0].Parameters.Add(tranStartDate);

                        reportRequest.ReportDetails[0].Parameters.Add(tranEndDate);

                        ReportParameter agencyID = new ReportParameter();
                        agencyID.Name = "prmAgency";
                        agencyID.Value = userData[userData.Length-1].ToString().Trim();
                        agencyID.Type = "String";
                        reportRequest.ReportDetails[0].Parameters.Add(agencyID);
                        if (Config.Setting("Logging.SSRSReport") == "1")
                        {
                            Logger.Log("Generating report for" + ReportType.ReportTypeText + ", for LoggedIn User:" + Page.User.Identity.Name.ToString());
                            Logger.Log(SerializeToXmlString(reportRequest.ReportDetails[0].Parameters));
                        }
                        byte[] Result = null;
                        Result = helper.GenerateAndExecuteDelivery(reportRequest.ReportDetails);

                        if (Result != null)
                        {
                            string fileName = reportRequest.ReportDetails[0].Distributions[0].FileName;
                            fileName += helper.GetFileExtension(reportRequest.ReportDetails[0].Distributions[0].OutputFileType);
                            HttpResponse response = HttpContext.Current.Response;
                            response.Clear();
                            response.ClearContent();
                            response.ClearHeaders();
                            response.Buffer = true;
                            response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                            Response.BinaryWrite(Result);
                        }
                        else
                        {
                            rptLib.SetMessage(lblErrMsg, "No Data Found. Please specify a valid criteria.", true);
                            InvisibleControls();
                            return;
                        }
                        //pnlfiltervisible.Visible = true;
                        //lblrpttype.Text = "Report Type" + "" + ReportType.ReportTypeText.ToString();
                        //lblstartDate.Text = "Start Date" + "" + TextstartDt.Text;
                        //lblEndDate.Text = "End Date" + "" + TextendDt.Text;
                        //rptselectedvalue.Text = userInfoObj.AgencyID.ToString(); // If user select "ALL" Locations, then we need to pass only Agency ID. 
                        //lblReportURL.Text = "/PaymentCentralReporting/MAIG/PaymentDetail_Location_Agency_PIT";
                        //lblAgencyID.Text = "Agency ID" + " " + userInfoObj.AgencyID.ToString();
                        //lbldisplayType.Text = "display type" + "" + rbDisplayType.SelectedItem.Text;
                        InvisibleBarts();
                    }
                    else
                    {
                        //System.Collections.Generic.List<string> selectedItemsList = new System.Collections.Generic.List<string>();
                        //foreach (ListItem item in dstLocation.Items)
                        //{
                        //    if (item.Selected)
                        //        selectedItemsList.Add(item.Value + ","+ item.Text);
                        //}
                        //string selectedLocationID= string.Join(",", selectedItemsList.ToArray());
                        string selectedLocationID = dstLocation.SelectedItem.Value.ToString();
                        SSRSReportRequest reportRequest = new SSRSReportRequest();
                        reportRequest.ReportDetails = new List<ReportDetail>();
                        ReportDetail detail = new ReportDetail();
                        detail.SkipForProcessing = false;
                        detail.ReportOffset = "-1";
                        detail.ReportPath = Config.Setting("ReportPath");
                        if (ReportType.ReportTypeValue == "7")
                        {
                            detail.ReportName = Config.Setting("ReportSummaryIdentifier_AgencyLocation_Multiple");
                            detail.ReportDetailIdentifier = Config.Setting("ReportSummaryIdentifier_AgencyLocation_Multiple");
                        }
                        else
                        {
                            detail.ReportName = Config.Setting("ReportDetailIdentifier_AgencyLocation_Multiple");
                            detail.ReportDetailIdentifier = Config.Setting("ReportDetailIdentifier_AgencyLocation_Multiple");
                        }
                        reportRequest.ReportDetails.Add(detail);
                        DistributionDetail resType = new DistributionDetail();
                        resType.OutputFileType = rbDisplayType.SelectedItem.Text;
                        if (ReportType.ReportTypeValue == "7")
                        {
                            resType.FileName = Config.Setting("ReportSummaryIdentifier_AgencyLocation_Multiple") + DateTime.Now;
                        }
                        else
                        {
                            resType.FileName = Config.Setting("ReportDetailIdentifier_AgencyLocation_Multiple") + DateTime.Now;
                        }
                        reportRequest.ReportDetails[0].Distributions.Add(resType);

                        reportRequest.ReportDetails[0].Parameters.Add(tranStartDate);

                        reportRequest.ReportDetails[0].Parameters.Add(tranEndDate);

                        ReportParameter locationID = new ReportParameter();
                        locationID.Name = "prmLocNum";
                        locationID.Value = selectedLocationID.Trim();
                        locationID.Type = "String";
                        reportRequest.ReportDetails[0].Parameters.Add(locationID);
                        if (Config.Setting("Logging.SSRSReport") == "1")
                        {
                            Logger.Log("Generating report for" + ReportType.ReportTypeText + ", for LoggedIn User:" + Page.User.Identity.Name.ToString());
                            Logger.Log(SerializeToXmlString(reportRequest.ReportDetails[0].Parameters));
                        }
                        byte[] Result = null;
                        Result = helper.GenerateAndExecuteDelivery(reportRequest.ReportDetails);

                        if (Result != null)
                        {
                            string fileName = reportRequest.ReportDetails[0].Distributions[0].FileName;
                            fileName += helper.GetFileExtension(reportRequest.ReportDetails[0].Distributions[0].OutputFileType);
                            HttpResponse response = HttpContext.Current.Response;
                            response.Clear();
                            response.ClearContent();
                            response.ClearHeaders();
                            response.Buffer = true;
                            response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                            Response.BinaryWrite(Result);
                            InvisibleBarts();
                        }
                        else
                        {
                            rptLib.SetMessage(lblErrMsg, "No Data Found. Please specify a valid criteria.", true);
                            InvisibleControls();
                            return;
                        }
                    }
                }
                else if (ReportType.ReportTypeValue == "9")
                {
                    //// MAIG - Begin - Settlement Report setup

                    if (Convert.ToDateTime(TextActivityDt.Text.ToString()) >= DateTime.Today)
                    {
                        InvisibleControls();
                        rptLib.SetMessage(lblErrMsg, "Please choose Activity Date less than today's date.", true);
                        return;
                    }
                    else
                    {
                        SSRSReportRequest reportRequest = new SSRSReportRequest();
                        reportRequest.ReportDetails = new List<ReportDetail>();
                        ReportDetail detail = new ReportDetail();
                        detail.SkipForProcessing = false;
                        detail.ReportOffset = "-1";
                        detail.ReportPath = Config.Setting("ReportPath");

                        detail.ReportName = Config.Setting("ReportDetailIdentifier_SettlementReport");
                        detail.ReportDetailIdentifier = Config.Setting("ReportDetailIdentifier_SettlementReport");

                        reportRequest.ReportDetails.Add(detail);
                        DistributionDetail resType = new DistributionDetail();
                        resType.OutputFileType = rbDisplayType.SelectedItem.Text;
                        resType.FileName = Config.Setting("ReportDetailIdentifier_SettlementReport") + DateTime.Now;
                        reportRequest.ReportDetails[0].Distributions.Add(resType);

                        ReportParameter activityDate = new ReportParameter();
                        activityDate.Name = "parmActDate";
                        activityDate.Value = TextActivityDt.Text;
                        activityDate.Type = "DateTime";
                        reportRequest.ReportDetails[0].Parameters.Add(activityDate);

                        ReportParameter agencyID = new ReportParameter();
                        agencyID.Name = "parmAgncyId";
                        agencyID.Value = userData[userData.Length-1].ToString().Trim();
                        agencyID.Type = "String";
                        reportRequest.ReportDetails[0].Parameters.Add(agencyID);

                        if (Config.Setting("Logging.SSRSReport") == "1")
                        {
                            Logger.Log("Generating report for" + ReportType.ReportTypeText + ", for LoggedIn User:" + Page.User.Identity.Name.ToString());
                            Logger.Log(SerializeToXmlString(reportRequest.ReportDetails[0].Parameters));
                        }
                        byte[] Result = null;
                        Result = helper.GenerateAndExecuteDelivery(reportRequest.ReportDetails);

                        if (Result != null)
                        {
                            string fileName = reportRequest.ReportDetails[0].Distributions[0].FileName;
                            fileName += helper.GetFileExtension(reportRequest.ReportDetails[0].Distributions[0].OutputFileType);
                            HttpResponse response = HttpContext.Current.Response;
                            response.Clear();
                            response.ClearContent();
                            response.ClearHeaders();
                            response.Buffer = true;
                            response.AddHeader("Content-Disposition", "attachment;filename=" + fileName);
                            Response.BinaryWrite(Result);
                        }
                        else
                        {
                            rptLib.SetMessage(lblErrMsg, "No Data Found. Please specify a valid criteria.", true);
                            InvisibleControls();
                            return;
                        }

                        InvisibleBarts();
                    }

                    //// MAIG - End - Settlement Report setup
                }
                else
                {
                    InvisibleControls();
                    rptLib.SetMessage(lblErrMsg, "Please select the Report Type", true);
                    return;
                }
				//MAIG - CH7 - END - Added code to include the PS USer Reports
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
			int n = int.Parse(ReportType.ReportTypeValue);
            switch (n)
            {
                case 1:
                    e.Row.Cells[0].Width = 250;
                    e.Row.Cells[1].Text = (String.Format("{0:c}", drv["Revenue"])).ToString();
                    //dgReport1.Columns.Remove(dgReport1.Columns[0]);
                    break;
                default:
                    break;
            }
            //Begin PC Phase II Changes CH3- Commented the below code to display Report Types for only Summary and Detail.
//                case 2:
//                    e.Row.Cells[1].HorizontalAlign =  HorizontalAlign.Right;
//                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
//                    e.Row.Cells[2].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
//                    if (drv["Date"].ToString() == "")
//                    {
//                        e.Row.BackColor = Color.LightGray;
//                        e.Row.Font.Bold = true; 
//                        e.Row.Cells[0].Text = "Total";
//                    }
//                    break;
//                case 3:
//                    e.Row.Cells[1].HorizontalAlign =  HorizontalAlign.Right;
//                    e.Row.Cells[2].HorizontalAlign = HorizontalAlign.Right;
//                    e.Row.Cells[2].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
//                    if (drv["User"].ToString() == "")
//                    {
//                        e.Row.BackColor = Color.LightGray;
//                        e.Row.Font.Bold = true; 
//                        e.Row.Cells[0].Text = "Total";							
//                    }
//                    break;
//                case 4:
////					e.Row.BackColor = clr;//PT.Ch1 - Commented by COGNIZANT on 9/8/08 to enable LightYellow alternate item color for report type 4
//                    if (drv["Date"].ToString() == "")
//                    {
//                        e.Row.BackColor = clr;
//                        e.Row.Font.Bold = true; 
//                        e.Row.Cells[0].Text = "Sub-total";
			
//                    }
//                    if (drv["User"].ToString() == "")
//                    {
//                        e.Row.BackColor = Color.LightGray;
//                        e.Row.Font.Bold = true; 
//                        e.Row.Cells[0].Text = "Report Total";
				
//                    }
//					START - PT.Ch1 - Commented by COGNIZANT on 9/8/08 to enable LightYellow alternate item color for report type 4
//					if (e.Row.Cells[0].Text == "Sub-total")
//					{
//						if (e.Row.BackColor == Color.White)
//							clr = Color.LightYellow;
//						else
//							clr = Color.White;
//					}
//					END - PT.Ch1
//                    e.Row.Cells[2].HorizontalAlign =  HorizontalAlign.Right;
//                    e.Row.Cells[3].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
//                    e.Row.Cells[3].HorizontalAlign =  HorizontalAlign.Right;
//                    break;
//                case 5:
////					e.Row.BackColor = clr;//PT.Ch1 - Commented by COGNIZANT on 9/8/08 to enable LightYellow alternate item color for report type 4
//                    if (drv["User"].ToString() == "")
//                    {
//                        e.Row.BackColor = clr;
//                        e.Row.Font.Bold = true; 
//                        e.Row.Cells[0].Text = "Sub-total";
				
//                    }
//                    if (drv["Date"].ToString() == "")
//                    {
//                        e.Row.BackColor = Color.LightGray;
//                        e.Row.Font.Bold = true; 
//                        e.Row.Cells[0].Text = "Report Total";
			
//                    }
//					START - PT.Ch1 - Commented by COGNIZANT on 9/8/08 to enable LightYellow alternate item color for report type 5
//					if (e.Row.Cells[0].Text == "Sub-total")
//					{
//						if (e.Row.BackColor == Color.White)
//							clr = Color.LightYellow;
//						else
//							clr = Color.White;
//					}
//					END - PT.Ch1
            //        e.Row.Cells[2].HorizontalAlign =  HorizontalAlign.Right;
            //        e.Row.Cells[3].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
            //        e.Row.Cells[3].HorizontalAlign =  HorizontalAlign.Right;
            //        break;
            //    case 6:
            //        //67811A0 - PCI Remediation for Payment systems CH1: Start Change Column Index to accomodate State and Prefix column
            //        //e.Row.Cells[10].Text = (String.Format("{0:c}", drv["Total"])).ToString();
            //        //e.Row.Cells[10].HorizontalAlign = HorizontalAlign.Right;
            //        e.Row.Cells[12].Text = (String.Format("{0:c}",drv["Total"])).ToString();
            //        e.Row.Cells[12].HorizontalAlign =  HorizontalAlign.Right;
            //        //67811A0 - PCI Remediation for Payment systems CH1: End Change Column Index to accomodate State and Prefix column
            //        break;
            //    case 7:
            //        e.Row.Cells[6].Text = (String.Format("{0:c}",drv["Amount"])).ToString();
            //        e.Row.Cells[6].HorizontalAlign =  HorizontalAlign.Right;
            //        lblCyber.Visible = true;
            //        break;
            //}
            // End PC Phase II Changes CH3- Commented the below code to display Report Types for only Summary and Detail.
		}


		// EVENT HANDLER: ItemDataBound
        public void ItemDataBound2(Object sender, GridViewRowEventArgs e)
        {
            // Retrieve the data linked through the relation
            // Given the structure of the data ONLY ONE row is retrieved
            DataRowView drv = (DataRowView)e.Row.DataItem;
            if (drv == null)
                return;

            // Check here the app-specific way to detect whether the 
            // current row is a summary row
            int n = int.Parse(ReportType.ReportTypeValue);
            switch (n)
            {
                case 7:
                    if (drv["Credit Card Number"].ToString() != "")
                        e.Row.Cells[10].Text = Cryptor.Decrypt(drv["Credit Card Number"].ToString(), Config.Setting("CSAA_ORDERS_KEY"));
                    e.Row.Cells[11].Text = (String.Format("{0:c}", drv["Amount"])).ToString();
                    e.Row.Cells[11].HorizontalAlign = HorizontalAlign.Right;
                    lblAPDS.Visible = true;
                    break;
            }
        }


        public void PageIndexChanged1(Object sender, GridViewPageEventArgs e)
		{
            
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
		}

        public void PageIndexChanged2(Object sender, GridViewPageEventArgs e)
		{
            if (e.NewPageIndex == -1)
            {
                dgReport2.PageIndex = 0;
            }
            else
            {
                dgReport2.PageIndex = e.NewPageIndex;
            }
			UpdateDataView();
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
                    //commented as a part of .NetMig 3.5
					//currPage = dgReport1.CurrentPageIndex+1;
                    currPage = dgReport1.PageIndex + 1;
					if (ods.Tables[0].Rows.Count >= 500 && orpt == "6") rptLib.SetMessage(lblErrMsg, "The first 500 rows are displayed.", true);
				}
				catch  (Exception ex) 
				{
					Logger.Log(ex);
				}
			}
            //Begin PC Phase II Changes CH3- Commented the below code to display Report Types for only Summary and Detail.
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
			{
                //commented as a part of .NetMig 3.5
               // Session["pagesize"] = pageCnt;
				//dgReport1.PagerStyle.Visible = true;
				//if (orpt == "7") dgReport2.PagerStyle.Visible = true;
			}

			if (pageCnt < 2)
			{
                //commented as a part of .NetMig 3.5
               // Session["pagesize"] = pageCnt;

				//dgReport1.PagerStyle.Visible = false;
				//dgReport2.PagerStyle.Visible = false;
			}
            if (rpt != 1)
            {
                lblPageNum1.Visible = true;
                lblPageNum2.Visible = true;
                    lblPageNum1.Text = "Showing Page " + currPage.ToString() + " of " + pageCnt.ToString();
                lblPageNum2.Text = "Showing Page " + currPage.ToString() + " of " + pageCnt.ToString();
            }
      

            //Added as a part of .Net Mig 3.5 to to enable or disable the previous link buttons.
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
			lblReportTitle.Text = ReportType.ReportTypeText;//report_type.SelectedItem.Text;
				
			string UserNames = Users.UserNames(true);
			//For display on the start page.
            //CHG0110069 - CH1 - BEGIN - Appended the /Time along with the Date Label
			lblDate.Text = "Run Date/Time:";
			lblDates.Text = "Date/Time Range:";
            //CHG0110069 - CH1 - END - Appended the /Time along with the Date Label
			lblCsr.Text = "Users:";
			lblApp.Text = "Application:";
			lblProduct.Text = "Product Type:";
			lblRevenue.Text = "Revenue Type:";
            //PC Phase II changes CH2 - Start - Modified the below code for handling DateTime 
            DateTime StartDate = Convert.ToDateTime(TextstartDt.Text);
            DateTime EndDate = Convert.ToDateTime(TextendDt.Text);
            //PC Phase II changes CH2 - End - Modified the below code for handling DateTime
			//Added by Cognizant
			lblPayment.Text = "Payment Type:";
			//Added by Cognizant

			//STAR AUTO 2.1.Ch1: START - Added label RepDO in the header of the result grid
			lblRepDO.Text = "Rep DO:";
            lblStatus.Text = "Status:";
			lblRepDOName.Text = Users.RepDOText;
			//STAR AUTO 2.1.Ch1: END
            //CHG0109406 - CH1 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
            //Start-TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time and Run Date  by cognizant.
            lblRunDate.Text = DateTime.Now.ToString() + " " + "Arizona";
            lblDateRange.Text = StartDate + " - " + EndDate + " " + "Arizona";
            //End-TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time and Run Date  by cognizant.
            //CHG0109406 - CH1 - END - Modified the timezone from "MST Arizona" to Arizona
			lblCsrs.Text =  UserNames;
			lblAppName.Text = Users.NameApp;
            
			//Added by Cognizant
			lblPaymentName.Text = PaymentType.PaymentTypeText;
			//Added by Cognizant

			lblProductName.Text = RevenueProduct.ProductTypeText;
			lblRevenueName.Text = RevenueProduct.RevenueTypeText;
            lblStatusType.Text = status.SelectedItem.Text;
			// For printing and converting to Excel only, folowing session variables are declared.
			// Not a good way. Will try to remove them later.
					
			Session["RptTitle"] = ReportType.ReportTypeText;
			Session["DateRange"] = StartDate + " - " + EndDate;			
			Session["CSRs"]		= UserNames;
			Session["AppName"] = Users.NameApp;
			Session["ProductName"] = RevenueProduct.ProductTypeText;
			Session["RevenueName"] = RevenueProduct.RevenueTypeText;
			
			//STAR AUTO 2.1.Ch2: START - Added a session variable to store the selected RepDO value
			Session["RepDOName"] = Users.RepDOText;
			//STAR AUTO 2.1.Ch2: END

			//Added by Cognizant 
			Session["PaymentName"] = PaymentType.PaymentTypeText;
			//Added by Cognizant
            Session["StatusType"] = status.SelectedItem.Text;
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
            //CHG0109406 - CH3 - BEGIN - Set the below label visible property to false which displays the timezone in results
            lblHeaderTimeZone.Visible = false;
            //CHG0109406 - CH3 - END - Set the below label visible property to false which displays the timezone in results
			lblErrMsg.Visible = false;
			lblAPDS.Visible	  = false;
			lblCyber.Visible = false;
			pnlTitle.Visible = false;

		}

       

	}
}

