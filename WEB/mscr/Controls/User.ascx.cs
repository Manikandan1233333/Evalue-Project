/* MODIFIED BY COGNIZANT AS PART OF PT MAIG Changes
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
	using CSAAWeb;
    using System.Security.Principal;
    using AuthenticationClasses; 

	/// <summary>
	///		Summary description for Users.
	/// </summary>
	public partial  class User : System.Web.UI.UserControl
	{
		//protected DataTable UserDt;
        //Commented below lines on 10/16/2014- issue Location dropdown cache old values #Defect 1567
        /////<summary/>
        //private DataTable AppUsers
        //{
        //    get {return (DataTable)Cache["USER"];}
        //}
        ///<summary>Value for AgencyID.</summary>
        public string AgencyID { get; set; }

		/// <summary>
        /// Loads the Controls of the user control
		/// </summary>
		/// <param name="sender">Sender of the event</param>
		/// <param name="e">Event argument</param>
        protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!Page.IsPostBack)
			{
				//Initialize();
                //MAIG - Commented below line on 10/16/2014- issue Location dropdown cache old values #Defect 1567
                //if (AppUsers == null) 
                    InitializeUser();
                //_UserList.DataSource = Dt;
                //_UserList.DataBind();
               
			}           		

		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			InitializeComponent();
			base.OnInit(e);
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
		private void InitializeUser() 
		{
            lock (typeof(User))
            {
                //AgencyID = GetAgencyID();
                Order DataConnection = new Order(Page);
                DataTable UserDt;
                //if (Cache["USER"] == null)
                //{
                    UserDt = DataConnection.LookupDataSet("Insurance", "User", new object[] { AgencyID }).Tables["USER"];
                    if (UserDt.Rows.Count > 1)
                    {
                        DataRow Dr = UserDt.NewRow();
                        Dr["USERList"] = "All";
                        UserDt.Rows.InsertAt(Dr, 0);
                    }
                    _UserList.DataSource = UserDt;
                    _UserList.DataBind();

                    //Cache["USER"] = Dt;
                //}
                DataConnection.Close();

            } 
		}

	}
}
#region commented code
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
//lock (typeof(Users))
//{

//    if (Cache["AppUsers"]==null) 
//    {
//        //FillUserList(Dt, "All", -1, "-1", -1, true ) ;
//    }
//    else 
//    {
//        _UserList.DataSource=AppUsers;
//        _UserList.DataBind();
//        _UserList.SelectedIndex = 0;
//    }
//}

///// <summary>
///// Adds a Select Item row to the table.
///// </summary>
//private void AddSelectItem(DataTable Dt, string Description, string ID ) 
//{
//    DataRow Row = Dt.NewRow();
//    Row["ID"]=ID;
//    Row["Description"]=Description;
//    Dt.Rows.InsertAt(Row, 0);
//}

///// <summary>
///// Fill the user listbox.
///// </summary>
//private void FillUserList(DataTable Dt, string Requester, int Role, string RepDO, int AppId, bool ToCache ) 
//{
//    //Order DataConnection = new Order(Page);
//    //Dt = DataConnection.LookupDataSet("Authentication", "ListAppUsersByRoles", new object[] {Requester, Role, RepDO, AppId} ).Tables[0];
//    ////if (Dt.Rows.Count>1)
//    ////{
//    //    DataRow Dr =  Dt.NewRow();
//    //    Dr["USERNAME"] = "All";
//    //    Dr["USERDEF"] = "All";
//    //    Dt.Rows.InsertAt(Dr,0);
//    ////}
//    //if (ToCache == true)
//    //{
//    //    int CacheDuration = Convert.ToInt32(Config.Setting("CacheDurationForUsers"));
//    //    Cache.Insert("AppUsers", Dt, null,DateTime.Now.AddHours(CacheDuration),TimeSpan.Zero,System.Web.Caching.CacheItemPriority.Low, null);
//    //    //Cache["AppUsers"] = Dt;
//    //    _UserList.DataSource=AppUsers;
//    //}
//    //else _UserList.DataSource= Dt;

//    //DataConnection.Close();
//    //_UserList.DataBind();
//    //_UserList.SelectedIndex = 0;

////if (Dt.Rows.Count>1)
////{
//    DataRow Dr =  Dt.NewRow();
//    Dr["USERNAME"] = "All";
//    Dr["USERDEF"] = "All";
//    Dt.Rows.InsertAt(Dr,0);
////}
//if (ToCache == true)
//{
//    int CacheDuration = Convert.ToInt32(Config.Setting("CacheDurationForUsers"));
//    Cache.Insert("AppUsers", Dt, null,DateTime.Now.AddHours(CacheDuration),TimeSpan.Zero,System.Web.Caching.CacheItemPriority.Low, null);
//    //Cache["AppUsers"] = Dt;
//    _UserList.DataSource=AppUsers;
//}
//else _UserList.DataSource= Dt;

//DataConnection.Close();
//_UserList.DataBind();
//_UserList.SelectedIndex = 0;


//}

/////<summary>UserList Text.</summary>
//public string UserText
//{
//    get {return _UserList.SelectedItem.Text;}
//    set {_UserList.SelectedItem.Text=value;}
//}
/////<summary>UserList Value.</summary>
//public string UserValue
//{
//    get {return _UserList.SelectedItem.Value;}
//    set {_UserList.SelectedItem.Value=value;}
//}

//public string UserNames(bool IsNames)
//{
//    string lstUsers = "";
//    string lstNames = "";
//    if (_UserList.SelectedIndex > -1)
//    {					
//        foreach(ListItem li in _UserList.Items)
//        {
//            if(li.Selected == true)
//            {
//                lstNames = lstNames + li.Text.Replace(" - " + li.Value,"; ");
//                lstUsers = lstUsers + li.Value + ',';
//            }
//        }	
//        //lstNames = lstNames.Replace(" -",";");
//        if (lstUsers == "All,")
//        {
//            lstNames = "All";
//        }
//    }
//    else
//    {
//        lstUsers = "All,";
//    }
//    if (IsNames == true) return lstNames;
//    else return lstUsers;
//}

#endregion