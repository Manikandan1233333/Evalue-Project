
/*
 * HISTORY:
 * 
 * STAR Retrofit II Changes:  
 * Modified as a part of CSR 4852
 * 1/31/2007 STAR Retrofit II.Ch1: 
 *           The content of the cache "RepDOs" is cleared, so as to populate the cache with updated 
 *			 data,when the page is getting loaded for the first time.
 * 
 * Modified as part of STAR AUTO 2.1
 * 08/14/2007 STAR AUTO 2.1.Ch1:
 * 			 Added a property RepDOText to get the selected District Office Name
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

	/// <summary>
	///		Summary description for Users.
	/// </summary>
	public partial  class Users : System.Web.UI.UserControl
	{
		protected DataTable Dt;
		
		///<summary/>
		private DataTable Applications
		{
			get {return (DataTable)Cache["Applications"];}
		}
		///<summary/>
		private DataTable Roles
		{
			get {return (DataTable)Cache["Roles"];}
		}
		///<summary/>
		private DataTable RepDOs
		{
			get {return (DataTable)Cache["RepDOs"];}
		}
		///<summary/>
		private DataTable AppUsers
		{
			get {return (DataTable)Cache["AppUsers"];}
		}


		///<summary>Application value.</summary>
		public string IdApp
		{
			get {return _Application.SelectedItem.Value;}
			set {_Application.SelectedItem.Value=value;}
		}
		///<summary>Application display text.</summary>
		public string NameApp
		{
			get {return _Application.SelectedItem.Text;}
			set {_Application.SelectedItem.Text=value;}
		}

		///<summary>Role Value.</summary>
		public string Role 
		{
			get {return _Role.SelectedItem.Value;}
			set {_Role.SelectedItem.Value=value;}
		}
		///<summary>Rep DO value.</summary>
		public string RepDO 
		{
			get {return _RepDO.SelectedItem.Value;}
			set {_RepDO.SelectedItem.Value=value;}
		}

		//STAR AUTO 2.1.Ch1: START - Added a property RepDOText to get the selected District Office Name
		/// <summary>Rep DO Name.</summary>
		public string RepDOText 
		{
			get {return _RepDO.SelectedItem.Text;}
			set {_RepDO.SelectedItem.Text=value;}
		}
		//STAR AUTO 2.1.Ch1: END

		///<summary>UserList Text.</summary>
		public string UserText
		{
			get {return _UserList.SelectedItem.Text;}
			set {_UserList.SelectedItem.Text=value;}
		}
		///<summary>UserList Value.</summary>
		public string UserValue
		{
			get {return _UserList.SelectedItem.Value;}
			set {_UserList.SelectedItem.Value=value;}
		}

		public string UserNames(bool IsNames)
		{
			string lstUsers = "";
			string lstNames = "";
			if (_UserList.SelectedIndex > -1)
			{					
				foreach(ListItem li in _UserList.Items)
				{
					if(li.Selected == true)
					{
						lstNames = lstNames + li.Text.Replace(" - " + li.Value,"; ");
						lstUsers = lstUsers + li.Value + ',';
					}
				}	
				//lstNames = lstNames.Replace(" -",";");
				if (lstUsers == "All,")
				{
					lstNames = "All";
				}
			}
			else
			{
				lstUsers = "All,";
			}
			if (IsNames == true) return lstNames;
			else return lstUsers;
		}


		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (!Page.IsPostBack)
			{
				//STAR Retrofit II.Ch1 - START The content of the cache "RepDOs" is cleared, so as to populate the cache with updated 
				//data,when the page is getting loaded for the first time.
				Cache.Remove("RepDOs"); 
				//STAR Retrofit II.Ch1 - END
				Initialize();
				BindData();
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
		private void Initialize() 
		{
			lock (typeof(Users))
			{
				Order DataConnection = new Order(Page);
				if (Cache["Applications"]==null) 
				{
					Dt = DataConnection.LookupDataSet("Authentication", "GetApps").Tables[0];
					if (Dt.Rows.Count>1) AddSelectItem(Dt, "All","-1");
					Cache["Applications"] = Dt;
				}
				if (Cache["Roles"]==null) 
				{
					Dt = DataConnection.LookupDataSet("Authentication", "GetAppRoles", new object[] {-1} ).Tables[0];
					if (Dt.Rows.Count>1) AddSelectItem(Dt, "All","-1");
					Cache["Roles"] = Dt;
				}
				if (Cache["RepDOs"]==null) 
				{
					Dt = DataConnection.LookupDataSet("Authentication", "GetDOs").Tables[0];
					AddSelectItem(Dt, "All","-1");
					Cache["RepDOs"] = Dt;
				}
				DataConnection.Close();
				if (Cache["AppUsers"]==null) 
				{
					FillUserList(Dt, "All", -1, "-1", -1, true ) ;
				}
				else 
				{
					_UserList.DataSource=AppUsers;
					_UserList.DataBind();
					_UserList.SelectedIndex = 0;
				}
    		}
		
		}

		/// <summary>
		/// Binds the data to the list boxes.
		/// </summary>
		private void BindData() 
		{
			_Application.DataSource=Applications;
			_Application.DataBind();
			_Role.DataSource= Roles;
			_Role.DataBind();
			_RepDO.DataSource=RepDOs;
			_RepDO.DataBind();
		}


		/// <summary>
		/// Adds a Select Item row to the table.
		/// </summary>
		private void AddSelectItem(DataTable Dt, string Description, string ID ) 
		{
			DataRow Row = Dt.NewRow();
			Row["ID"]=ID;
			Row["Description"]=Description;
			Dt.Rows.InsertAt(Row, 0);
		}

		protected void _Application_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			Order DataConnection = new Order(Page);
			Dt = DataConnection.LookupDataSet("Authentication", "GetAppRoles", new object[] {Convert.ToInt32(IdApp)} ).Tables[0];
            if (Dt.Rows.Count > 1)
            {
                AddSelectItem(Dt, "All", "-1");
            }
                _Role.DataSource = Dt;
                _Role.DataBind();            
			DataConnection.Close();
			FillUserList(Dt, "All", Convert.ToInt32(Role), RepDO, Convert.ToInt32(IdApp), false ) ;

		}

		protected void _Role_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillUserList(Dt, "All", Convert.ToInt32(Role), RepDO, Convert.ToInt32(IdApp), false ) ;
		}

		protected void _RepDO_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			FillUserList(Dt, "All", Convert.ToInt32(Role), RepDO, Convert.ToInt32(IdApp), false ) ;			//BindData();
		}

		
		/// <summary>
		/// Fill the user listbox.
		/// </summary>
		private void FillUserList(DataTable Dt, string Requester, int Role, string RepDO, int AppId, bool ToCache ) 
		{
			Order DataConnection = new Order(Page);
			Dt = DataConnection.LookupDataSet("Authentication", "ListAppUsersByRoles", new object[] {Requester, Role, RepDO, AppId} ).Tables[0];
			//if (Dt.Rows.Count>1)
			//{
				DataRow Dr =  Dt.NewRow();
				Dr["USERNAME"] = "All";
				Dr["USERDEF"] = "All";
				Dt.Rows.InsertAt(Dr,0);
			//}
			if (ToCache == true)
			{
				int CacheDuration = Convert.ToInt32(Config.Setting("CacheDurationForUsers"));
				Cache.Insert("AppUsers", Dt, null,DateTime.Now.AddHours(CacheDuration),TimeSpan.Zero,System.Web.Caching.CacheItemPriority.Low, null);
				//Cache["AppUsers"] = Dt;
				_UserList.DataSource=AppUsers;
			}
			else _UserList.DataSource= Dt;

			DataConnection.Close();
			_UserList.DataBind();
			_UserList.SelectedIndex = 0;
		}

	}
}
