/*
MAIG - CH1 - Added the below variable and commeneted the ReportTypes variable to get the UserRoles from Ticket
MAIG - CH2 - Modified the logic to initialize allways to avoid caching
MAIG - CH3 - Included a method to get the UserRoles and removed the usage of Cache
MAIG - CH4 - Added method to obtain the UserRoles and to display the SSRS Report parameters based upon the selected Report Type
 * CHG0109406 - CH1 - Hidden a label lblHeaderTimeZone that displays the timezone for the results displayed
*/
namespace MSCR.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using OrderClasses.Service;
    using System.Web.Security;
    using System.Security.Principal;

	/// <summary>
	///		Summary description for ReportType.
	/// </summary>
	public partial  class ReportType : System.Web.UI.UserControl
	{
		protected string _AccessType = string.Empty;
		//MAIG - CH1 - BEGIN - Added the below variable and commeneted the ReportTypes variable to get the UserRoles from Ticket
        private string userRoles = string.Empty; 

		///<summary/>
        //private DataTable ReportTypes 
        //{
        //    get {return (DataTable)Cache["INS_Report_Type"];}
        //}
		//MAIG - CH1 - END - Added the below variable and commeneted the ReportTypes variable to get the UserRoles from Ticket
		///<summary>Text for Report type.</summary>
		public string ReportTypeText 
		{
			get {return _ReportType.SelectedItem.Text;}
			set {_ReportType.SelectedItem.Text=value;}
		}
		///<summary>Value for Report type.</summary>
		public string ReportTypeValue 
		{
			get {return _ReportType.SelectedItem.Value;}
			set {_ReportType.SelectedItem.Value=value;}
		}

		///<summary>Value for Report type.</summary>
		public string AccessType
		{
			get {return _AccessType;}
			set {_AccessType=value;}
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
			//MAIG - CH2 - BEGIN - Modified the logic to initialize allways to avoid caching
            Initialize();
            //if (ReportTypes==null) Initialize();
            //_ReportType.DataSource=ReportTypes;
            //_ReportType.DataBind();
			//MAIG - CH2 - END - Modified the logic to initialize allways to avoid caching
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion

		/// <summary>
		/// Retrieves the insurance lookup tables from the web service and caches them.
		/// </summary>
		private void Initialize() 
		{
			lock (typeof(ReportType))
			{
				//MAIG - CH3 - BEGIN - Included a method to get the UserRoles and removed the usage of Cache
                userRoles = GetUserRoles(); 
                Order DataConnection = new Order(Page);
                AccessType = userRoles; 
				DataTable DtReportTypes;
                //if (Cache["INS_Report_Type"]==null) 
                //{
                DtReportTypes = DataConnection.LookupDataSet("Insurance", "ReportTypes", new object[] { AccessType }).Tables["INS_Report_Type"];
                Cache["INS_Report_Type"] = DtReportTypes;

                DataRow Dr = DtReportTypes.NewRow();
                    Dr["Title"] = "Select Report";
                    DtReportTypes.Rows.InsertAt(Dr, 0);

                    _ReportType.DataSource = DtReportTypes;
                   _ReportType.DataBind();
                //}
				//MAIG - CH3 - END - Included a method to get the UserRoles and removed the usage of Cache
				DataConnection.Close();
			} 
		}
		//MAIG - CH4 - BEGIN - Added method to obtain the UserRoles and to display the SSRS Report parameters based upon the selected Report Type
        /// <summary>
        /// Reads the user roles from the cookie and reformats them for use by
        /// navigation.
        /// </summary>
        private string GetUserRoles()
        {
            return GetUserRoles(Page.User);
        }
        /// <summary>
        /// Reads the user roles from the cookie and reformats them for use by
        /// navigation.
        /// </summary>
        public static string GetUserRoles(IPrincipal User)
        {
            try
            {
                return GetUserRoles(((FormsIdentity)User.Identity).Ticket.UserData);
            }
            catch { return ""; }
        }
        /// Reads the user roles from the cookie and reformats them for use by
        /// navigation.
        /// </summary>
        public static string GetUserRoles(string UserData)
        {
            try
            {
                if (UserData.LastIndexOf(",") == UserData.Length - 1)
                    UserData = UserData.Substring(0, UserData.Length - 1);
                string[] A = UserData.ToLower().Split(';');
                UserData = A[0].ToString();
                //string[] A = UserData.ToLower().Split(';');
                //UserData = "";
                //for (int i = 0; i < A.Length; i++) UserData += ((A[i].Length > 3) ? A[i].Substring(0, 3) : A[i]) + ",";
                //if (UserData.Length > 0) UserData = UserData.Substring(0, UserData.Length - 1);
                return UserData;
            }
            catch { return ""; }
        }
        /// <summary>
        /// Depending on the selected Report Type, the corresponding Report filters will be displayed
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void _ReportType_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                string ReportTypeSelection = _ReportType.SelectedItem.Text.Trim().ToString();
                Panel LocPanel = (Panel)this.NamingContainer.FindControl("pnlLocations");
                Panel AgencyPanel = (Panel)this.NamingContainer.FindControl("pnlAgency");
                Panel UsersPanel = (Panel)this.NamingContainer.FindControl("pnlUsers");
                Panel InsPanel = (Panel)this.NamingContainer.FindControl("pnlInsuranceReports");
                Panel displayType = (Panel)this.NamingContainer.FindControl("pnldisplayType");

                HtmlTableCell dateLabels = (HtmlTableCell)this.NamingContainer.FindControl("_datelabels");
                HtmlTableCell dateFilters = (HtmlTableCell)this.NamingContainer.FindControl("_datefilters");
                HtmlTableCell actdateLabel = (HtmlTableCell)this.NamingContainer.FindControl("_activitydatelabel");
                HtmlTableCell actdateFilter = (HtmlTableCell)this.NamingContainer.FindControl("_activitydatefilter");

                dateLabels.Visible = true;
                dateFilters.Visible = true;
                actdateLabel.Visible = false;
                actdateFilter.Visible = false;

                GridView SummaryReport = (GridView)this.NamingContainer.FindControl("dgReport1");
                GridView DetailedReport = (GridView)this.NamingContainer.FindControl("dgReport2");
                Panel pnlSummaryReport = (Panel)this.NamingContainer.FindControl("pnlTitle");
                Label lblPageNum1 = (Label)this.NamingContainer.FindControl("lblPageNum1");
                Label lblPageNum2 = (Label)this.NamingContainer.FindControl("lblPageNum2");
                lblPageNum1.Visible = false;
                lblPageNum2.Visible = false;
                //CHG0109406 - CH1 - BEGIN - Hidden a label lblHeaderTimeZone that displays the timezone for the results displayed
                Label lblHeaderTimeZone = (Label)this.NamingContainer.FindControl("lblHeaderTimeZone");
                lblHeaderTimeZone.Visible = false;
                //CHG0109406 - CH1 - END - Hidden a label lblHeaderTimeZone that displays the timezone for the results displayed


                ListBox locationListBox = (ListBox)this.Parent.FindControl("LocType").FindControl("_Locations");
                ListBox userListBox = (ListBox)this.Parent.FindControl("User").FindControl("_UserList");
                Label lblError = (Label)this.Parent.FindControl("lblErrMsg");
                lblError.Text = "";
                SummaryReport.DataSource = null;
                SummaryReport.DataBind();
                SummaryReport.Visible = false;
                DetailedReport.DataSource = null;
                DetailedReport.DataBind();
                DetailedReport.Visible = false;
                pnlSummaryReport.Visible = false;
                switch (ReportTypeSelection)
                {
                    case "Payment Summary Report by Agency":
                    case "Payment Detail Report by Agency":
                        {
                            AgencyPanel.Visible = true;
                            LocPanel.Visible = false;
                            UsersPanel.Visible = false;
                            InsPanel.Visible = false;
                            displayType.Visible = true;
                            break;
                        }
                    //case "Payment Detail Report by Agency":
                    //    AgencyPanel.Visible = true;
                    //    LocPanel.Visible = false;
                    //    UsersPanel.Visible = false;
                    //    InsPanel.Visible = false;
                    //    Filtervisible.Visible = false;
                    //    displayType.Visible = true;
                    //    break;
                    case "Payment Summary Report by User":
                    case "Payment Detail Report by User":
                        {
                            UsersPanel.Visible = true;
                            userListBox.SelectedIndex = 0;
                            AgencyPanel.Visible = false;
                            LocPanel.Visible = false;
                            InsPanel.Visible = false;
                            displayType.Visible = true;
                            break;
                        }
                    //case "Payment Detail Report by User":
                    //    UsersPanel.Visible = true;
                    //    userListBox.SelectedIndex = 0;
                    //    AgencyPanel.Visible = false;
                    //    LocPanel.Visible = false;
                    //    InsPanel.Visible = false;
                    //    Filtervisible.Visible = false;
                    //    displayType.Visible = true;
                    //    break;
                    case "Payment Summary Report by Location":
                    case "Payment Detail Report by Location":
                        {
                            LocPanel.Visible = true;
                            locationListBox.SelectedIndex = 0;
                            AgencyPanel.Visible = false;
                            UsersPanel.Visible = false;
                            InsPanel.Visible = false;
                            displayType.Visible = true;
                            break;
                        }
                    //case "Payment Detail Report by Location":
                    //    LocPanel.Visible = true;
                    //    locationListBox.SelectedIndex = 0;
                    //    AgencyPanel.Visible = false;
                    //    UsersPanel.Visible = false;
                    //    InsPanel.Visible = false;
                    //    Filtervisible.Visible = false;
                    //    displayType.Visible = true;
                    //    break;
                    case "Insurance Transaction Summary":
                    case "Insurance Transaction Detail":
                        {
                            InsPanel.Visible = true;
                            LocPanel.Visible = false;
                            UsersPanel.Visible = false;
                            AgencyPanel.Visible = false;
                            displayType.Visible = false;
                            break;
                        }
                    //case "Insurance Transaction Detail":
                    //    InsPanel.Visible = true;
                    //    LocPanel.Visible = false;
                    //    UsersPanel.Visible = false;
                    //    AgencyPanel.Visible = false;
                    //    Filtervisible.Visible = false;
                    //    displayType.Visible = false;
                    //    break;
                    case "Settlement Report":
                        {
                            InsPanel.Visible = false;
                            LocPanel.Visible = false;
                            UsersPanel.Visible = false;
                            AgencyPanel.Visible = false;
                            displayType.Visible = true;
                            dateLabels.Visible = false;
                            dateFilters.Visible = false;
                            actdateLabel.Visible = true;
                            actdateFilter.Visible = true;
                            break;
                        }
                    default:
                        {
                            AgencyPanel.Visible = false;
                            LocPanel.Visible = false;
                            UsersPanel.Visible = false;
                            InsPanel.Visible = false;
                            displayType.Visible = false;
                            userListBox.SelectedIndex = 0;
                            locationListBox.SelectedIndex = 0;
                            InsPanel.Visible = false;
                            break;
                        }
                }
            }
            catch (Exception ex)
            {
                CSAAWeb.AppLogger.Logger.Log("Exception occurred in _ReportType_SelectedIndexChanged method" + ex.ToString());
            }
        }
		//MAIG - CH4 - END - Added method to obtain the UserRoles and to display the SSRS Report parameters based upon the selected Report Type
	}
}
