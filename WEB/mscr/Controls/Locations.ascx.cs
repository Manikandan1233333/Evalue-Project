/*MODIFIED BY COGNIZANT AS PART OF PT MAIG Changes
 * This page has been newly created for PT MAIG Changes.
 * Version 1.0
 * Created Date : 09/20/2014
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
    using AuthenticationClasses; 

	/// <summary>
	///		Summary description for ReportType.
	/// </summary>
	public partial  class Locations : System.Web.UI.UserControl
	{

        ///<summary/>
        //Commented below lines on 10/16/2014- issue Location dropdown cache old values #Defect 1567
        //private DataTable Location
        //{
        //    get { return (DataTable)Cache["Location"]; }
        //}
        ///<summary>Value for Agency ID.</summary>
        public string AgencyID { get; set; }
        

        /// <summary>
        /// Page Load event that loads the controls
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event argument</param>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            if (!Page.IsPostBack)
            {
                // Put user code to initialize the page here
                
                    //Initialize();
                //Commented below lines on 10/16/2014- issue Location dropdown cache old values #Defect 1567
                //if (Location == null) InitializeLocation();
                //    _Locations.DataSource = Location;
                //    _Locations.DataBind();

             InitializeLocation();
            }
		}

		#region Web Form Designer generated code
        /// <summary>
        /// Initializes the Controls
        /// </summary>
        /// <param name="e">event argument</param>
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
            /*if (Location == null) Initialize();
            _Locations.DataSource = Location;
            _Locations.DataBind();*/
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
		private void InitializeLocation() 
		{
            try
            {
                lock (typeof(Locations))
                {
                    //AgencyID = GetAgencyID(); 
                    Order DataConnection = new Order(Page);
                    DataTable Dt;
                    //Commented below lines on 10/16/2014- issue Location dropdown cache old values #Defect 1567
                    //if (Cache["Location"]==null) 
                    //{
                    Dt = DataConnection.LookupDataSet("Insurance", "Location", new object[] { AgencyID }).Tables["LOCATION"];
                    DataRow Dr = Dt.NewRow();
                    Dr["Code"] = "All";
                    Dr["Desc"] = "All";
                    Dt.Rows.InsertAt(Dr, 0);

                    //Cache["Location"] = Dt;
                    _Locations.DataSource = Dt;
                    _Locations.DataBind();

                    //Commented below line on 10/16/2014- issue Location dropdown cache old values #Defect 1567
                    //}
                    DataConnection.Close();
                }
            }
            catch (Exception ex)
            {
                CSAAWeb.AppLogger.Logger.Log("Exception occurred in InitializeLocation method" + ex.ToString());
            }
		}
       
        /*/// <summary>
        /// Reads the AgencyID from cookie userinfo object.
        /// </summary>
        private string GetAgencyID()
        {
            OrderClasses.Service.Order authUserObj = new OrderClasses.Service.Order();
            UserInfo userInfoObj = new UserInfo();

            userInfoObj = authUserObj.Authenticate(HttpContext.Current.User.Identity.Name, string.Empty, CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL);
            return userInfoObj.AgencyID.ToString(); 
        }*/
	}
}
