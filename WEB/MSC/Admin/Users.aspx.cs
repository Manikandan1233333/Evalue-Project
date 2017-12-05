/*
 * HISTORY :
 * STAR Retrofit II Changes: 
 * Modified as a part of CSR 4852
 * 1/25/2007 STAR Retrofit II.Ch1: 
 *           The boolean variable "Initialized" has been commented as it is no longer being used.
 * 1/25/2007 STAR Retrofit II.Ch2 : 
 *			 The condition to check if the Cache "AUTH_REPDO" is null has been removed form the Initialize()
 *			 function has been commented as this condition is now being checked in the Page_Load event.
 * 1/25/2007 STAR Retrofit II.Ch3 : 
 *			 The condition to check if the Cache "AUTH_REPDO" is null has been included in the Page_Load event.If it is
 *			 null,then the Initialize() function is called to populate the cache
 * Modified as a part of CSR#5166
 * 1/31/2007 STAR Retrofit II.Ch4 : Declarations for the new controls and variables.
 * 1/31/2007 STAR Retrofit II.Ch5 : New property added to get and set user status.
 * 1/31/2007 STAR Retrofit II.Ch6 : To set the label control value to blank.
 * 1/31/2007 STAR Retrofit II.Ch7 : GetUsersByStatus method is invoked for getting user details using the search criteria.
 *							        Two new methods namely HideControls and DisplayControls are used to hide and display controls.
 *							        Condition added to check whether both tables are empty.
 * 1/31/2007 STAR Retrofit II.Ch8 : Dummy postback method is commented.
 * 1/31/2007 STAR Retrofit II.Ch9 : Two new methods namely DisplayControls and HideControls are added.
 * 1/31/2007 STAR Retrofit II.Ch10 : Button click event for search functionality is added. AlphaNumeric validation code for UserId is added.
 * 1/31/2007 STAR Retrofit II.Ch11 : A new Cache APP_NAMES is introduced. 
 * 1/31/2007 STAR Retrofit II.Ch12 : Added the HTML hidden variables to show the print version format as per the search criteria submitted.
 * 1/31/2007 STAR Retrofit II.Ch13 : Modified the code to display the default selected value All in the Application, DO dropdown box.
 * Modified as a part of CSR#5157
 * 2/6/2007 STAR Retrofit II.Ch14: Modified to hide user list on page load.
 * Security Defect - CH1 -START Added the below code to verify if the IP address of the login page and current page is same       
 * // SSO Integration - CH1 -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit  
 * 
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
using CSAAWeb.WebControls;
using CSAAWeb.AppLogger;
using System.Net;
using System.Net.Sockets;

namespace MSC.Admin
{
	/// <summary>
	/// Page displays a list of all the users to allow for administration.
	/// </summary>
	public partial class Users : PageTemplate
	{
		private OrderClasses.Service.Order _OrderService=null;
		///<summary/>
		private OrderClasses.Service.Order OrderService {get {if (_OrderService==null) _OrderService=new OrderClasses.Service.Order(this); return _OrderService;}}

		///<summary/>
		protected Label Message;
		///<summary/>
		protected HtmlTable MessageTable;
		///<summary/>
		protected DataGrid UsersGrid;
		///<summary/>
		protected DropDownList _AppName;
		///<summary/>
		protected CheckBox _ShowAll;
		//STAR Retrofit II.Ch4-START Declarations for the components which are newly added
		protected bool blnValid;
		protected string Userid;
		//STAR Retrofit II.Ch12 - START : Added the HTML hidden variables 
		//STAR Retrofit II.Ch12- END
		//STAR Retrofit II.Ch4-END
		///<summary/>
		protected DropDownList _DO;

		///<summary/>
		public string DO {get {return ListC.GetListValue(_DO);} set {ListC.SetListIndex(_DO, value);}}

		///<summary/>
		public string AppName 
		{
			get {return ListC.GetListValue(_AppName);}
			set {ListC.SetListIndex(_AppName, value);}
		}

		//STAR Retrofit II.Ch5-START This Property is added to set and get the status of user from the dropdown list
		public string Status{get{return ListC.GetListValue(_Status);} set{ListC.SetListIndex(_Status,value);}}
		//STAR Retrofit II.Ch5-END


		///<summary/>
		public bool ShowAll {get {return _ShowAll.Checked;} set {_ShowAll.Checked=value;}}

		///<summary/>
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
                //STAR Retrofit II.Ch1 - START :The boolean variable "Initialized" has been commented as it is no longer being used.
                //if (!Initialized) Initialize();
                //STAR Retrofit II.Ch1 - END
                //STAR Retrofit II.Ch3 - START :The condition to check if the Cache "AUTH_REPDO" is null has been included in the Page_Load event.If it is
                //null,then the Initialize() function is called to populate the cache
                if (Cache["AUTH_DO"] == null)
                {
                    Initialize();
                }
                //STAR Retrofit II.Ch3 - END
                if (IsPostBack)
                {
                    //STAR Retrofit II.Ch6-START Label Control value is set to blank during postback
                    Message.Text = "";
                    //STAR Retrofit II.Ch6-END
                    string st = Request.Form["UserRid"];
                    if (st != null && st != "")
                    {
                        Context.Items.Add("UserRid", Convert.ToInt32(st));
                        Context.Items.Add("AppName", AppName);
                        Context.Items.Add("ShowAll", ShowAll);
                        Context.Items.Add("DO", DO);
                        //Context.Items.Add("Apps", _AppName.DataSource);
                        Server.Transfer("User.aspx");
                    }
                    //STAR Retrofit II.Ch14 - START Modified to hide user list on page load.                
                    /*else 
                    {
                        OrderService.AppId = AppName;
                        GetUsers();
                    } */
                    //STAR Retrofit II.Ch14 - END

                }
                else
                {
                    _DO.DataSource = (DataTable)Cache["AUTH_DO"];
                    _DO.DataBind();
                    //STAR Retrofit II.Ch13 - START: Commented the code below not to set the DO on page load
                    /* if (Context.Items.Contains("DO")) {
                        DO = (string)Context.Items["DO"];
                    } else {
                        AuthenticationClasses.UserInfo U = OrderService.GetUserInfo(User.Identity.Name);
                        DO = U.DO;
                    }
                    */
                    //STAR Retrofit II.Ch13 - END
                    //STAR Retrofit II.Ch11-START A new Cache APP_NAMES is introduced to get Application description in Users_PrintXls page.
                    Cache["APP_NAMES"] = GetApps();
                    _AppName.DataSource = (DataTable)Cache["APP_NAMES"];
                    //STAR Retrofit II.Ch11-END
                    _AppName.DataBind();
                    if (Context.Items.Contains("Message"))
                    {
                        Message.Text = (string)Context.Items["Message"];
                        MessageTable.Visible = true;
                    }
                    //STAR Retrofit II.Ch13 - START: Commented the code below not to set application name on page load
                    /* if (Context.Items.Contains("ShowAll")) 
                    {
                        ShowAll=Boolean.Parse((string)Context.Items["ShowAll"]);
                        AppName = (string)Context.Items["AppName"];
                        OrderService.AppId = AppName;
                    } 
				
                    else AppName = OrderService.AppId;
                    */
                    //STAR Retrofit II.Ch13 - END

                    //STAR Retrofit II.Ch14 - START Modified to hide user list on page load.
                    //STAR Retrofit II.Ch12 - START : Assigning the values to hidden HTML variables
                    /*hdnAppName.Value = AppName;
                    hdnDO.Value = DO;
                    hdnStatus.Value = Status;
                    hdnUserId.Value = Userid;
                    //STAR Retrofit II.Ch12 - END	
                    GetUsers();
                    */
                    HideControls();
                    //STAR Retrofit II.Ch14 - END
                }
            //}

		}

		private void GetUsers() 
		{
			if (AppName == "") ShowAll = true;
			//STAR Retrofit II.Ch7-START GetUsersByStatus method is invoked to retrieve user details using the search criteria
			//								 Two new methods namely HideControls and DisplayControls are Used.
			Userid = txtUserId.Text;
			int intStatus = Convert.ToInt16(Status);
			DataSet DS;
			// DS = OrderService.LookupDataSet("Authentication", "GetUsers", new object[] {ShowAll, DO});
			DS = OrderService.LookupDataSet("Authentication", "GetUsersByStatus", new object[] {ShowAll, DO, Userid, intStatus});
			// Condition added to check whether both tables are empty.			
			if(DS.Tables[0].Rows.Count!= 0 && DS.Tables[1].Rows.Count!= 0)
			{
				UsersGrid.DataSource = DS;
				UsersGrid.DataBind();
				DisplayControls();
			}
			else
			{
				lblError.Text = "No Data Found. Please specify a valid criteria.";
				HideControls();
			}
			//STAR Retrofit II.Ch7-END
		}
		private DataTable GetApps() 
		{
			DataTable Dt = OrderService.LookupDataSet("Authentication", "GetApplications").Tables[0];
			AddSelectItem(Dt, "All");
			return Dt;
		}

		///<summary>Dummy method to force postback.</summary>
		//STAR Retrofit II.Ch8-START Dummy method is commented since there is no postback on selecting items from dropdowm list
		//protected void Force_Postback(object sender, System.EventArgs e) {
		//}
		//STAR Retrofit II.Ch8-END

		///<summary/>
		// STAR Retrofit II.Ch1-START :The boolean variable "Initialized" has been commented as it is no longer being used.
		//private static bool Initialized = false; 
		//STAR Retrofit II.Ch1 - END
		/// <summary>
		/// Retrieves the insurance lookup tables from the web service and caches them.
		/// </summary>
		private void Initialize() 
		{
			lock (typeof(User)) 
			{
				// STAR Retrofit II.Ch1-START :The boolean variable "Initialized" has been commented as it is no longer being used.
				//Initialized=true;
				//STAR Retrofit II.Ch1 - END
				DataTable Dt;
				try 
				{
					//STAR Retrofit II.Ch2 - START :The condition to check if the Cache "AUTH_REPDO" is removed.
					//if (Cache["AUTH_DO"]==null) {
					
					Dt = OrderService.LookupDataSet("Authentication", "GetDOs").Tables[0];
					if (Dt.Rows.Count>1) AddSelectItem(Dt, "All");
					Cache["AUTH_DO"] = Dt;
					//}
					//STAR Retrofit II.Ch2 - END 
				} 
				catch 
				{
					//STAR Retrofit II.Ch1 - START :The boolean variable "Initialized" has been commented as it is no longer being used.
					//Initialized=false;
					//STAR Retrofit II.Ch1 - END
					throw;
				}
			}
		}
		/// <summary>
		/// Adds a Select Item row to the table.
		/// </summary>
		private void AddSelectItem(DataTable Dt, string Description) 
		{
			DataRow Row = Dt.NewRow();
			Row["ID"]="";
			Row["Description"]=Description;
			Dt.Rows.InsertAt(Row, 0);
		}
		//STAR Retrofit II.Ch9-START Two methods DisplayControls and HideControls are added to display and hide the controls such as DataGrid, labels and buttons.
		private void DisplayControls()
		{
			UsersGrid.Visible = true;
			trButtons.Visible = true;
			lblMessage.Visible = true;
			lblError.Visible = false;
		}

		private void HideControls()
		{
			trButtons.Visible = false;
			UsersGrid.Visible = false;
			lblMessage.Visible = false;
			lblError.Visible = true;
		} 
		//STAR Retrofit II.Ch9-END

		//STAR Retrofit II.Ch10-START: Button click event is added to enable search functionality
		//								 AlphaNumeric validation is done for UserId.
		protected void btnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			blnValid = true;
			Userid = txtUserId.Text;
			OrderService.AppId = AppName;
			if(Userid!= "")
			{
				blnValid = CSAAWeb.Validate.IsAlphaNumeric(Userid);
				
			}
			if(!blnValid)
			{
				lblError.Text = "UserId Field must be AlphaNumeric.";
				HideControls();
				
			}
			else
			{
				//STAR Retrofit II.Ch12 - START : Assigning the values to hidden HTML variables
				hdnAppName.Value = AppName;
				hdnDO.Value = DO;
				hdnStatus.Value = Status;
				hdnUserId.Value = Userid;
				//STAR Retrofit II.Ch12 - END
				GetUsers();
			}
		}

		
		//STAR Retrofit II.Ch10-END

		#region Web Form Designer generated code
		///<summary/>
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
            this.Load += new System.EventHandler(this.Page_Load);
            this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.btnSubmit_Click);
            
		}
		#endregion
	}
}
