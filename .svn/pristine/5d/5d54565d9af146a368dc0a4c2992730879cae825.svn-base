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
 *			  null,then the Initialize() function is called to populate the cache
 *	.NetMig3.5:Modified as a part of .Net Mig 3.5 on 03-20-2010	added a new method CheckLength to check the length of the email and modified the CheckEmail() to validate the required fields in the control.
 *	67811A0 - PCI Remediation for Payment systems CH1 : Arcsight logging
 * //Security Defect - CH1 -START Added the below code to verify if the IP address of the login page and current page is same
 *	MAIG - CH1 - CHG0087639 - RepID and UserID Length Expansion - Changed Convert function from Int32 to Int64, Int to long
 *  MAIG - CH2 - Included AgencyID & AgencyName to the User Admin screen for all Users
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
using OrderClasses.Service;
using AuthenticationClasses;
using CSAAWeb.WebControls;
using CSAAWeb.Serializers;
//67811A0  - START -PCI Remediation for Payment systems - Arcsight logging
using CSAAWeb.AppLogger;

namespace MSC.Admin
{
	/// <summary>
	/// Displays a form for creating and updating user profiles.
	/// </summary>
	public partial class User : SiteTemplate
	{

		#region ASP Page element declarations
		///<summary/>
		protected TextBox _UserId;
		///<summary/>
		protected TextBox _FirstName;
		///<summary/>
		protected TextBox _LastName;
		///<summary/>
		protected MSC.Controls.Phone _Phone;
		///<summary/>
		protected TextBox _Email;
		///<summary/>
		protected DropDownList _AppName;
		///<summary/>
		protected DropDownList _DO;
		///<summary/>
		protected TextBox _RepId;
		///<summary/>
		protected HiddenInput _UserRid;
		///<summary/>
		protected CheckBox _Active;
		///<summary/>
		protected ImageButton ResetPassword;
		///<summary/>
		protected ImageButton Delete;
		///<summary/>
		protected ImageButton Update;
		///<summary/>
		protected ImageButton AddUser;
		///<summary/>
		protected Label Caption;
		///<summary/>
		protected CheckBoxList _Roles;
		///<summary/>
		protected Validator Valid;
		///<summary/>
		protected Validator vldUserId;
		///<summary/>
		protected Validator vldEmail;
		///<summary/>
		protected HiddenInput ShowAll;
		#endregion
		#region Public Properties
		///<summary/>
		public string UserId {get {return _UserId.Text;} set {_UserId.Text=value;}}
		///<summary/>
		public string FirstName {get {return _FirstName.Text;} set {_FirstName.Text=value;}}
		///<summary/>
		public string LastName {get {return _LastName.Text;} set {_LastName.Text=value;}}
		///<summary/>
		public string Phone {get {return _Phone.Text;} set {_Phone.Text=value;}}
		///<summary/>
		public string Email {get {return _Email.Text;} set {_Email.Text=value;}}
		///<summary/>
		public int UserRid {get {return (_UserRid.Text=="")?0:Convert.ToInt32(_UserRid.Text);} set {_UserRid.Text=value.ToString();}}
		///<summary/>
		public bool Active {get {return _Active.Checked;} set {_Active.Checked=value;}}
		///<summary/>
		public string DO {get {return ListC.GetListValue(_DO);} set {ListC.SetListIndex(_DO, value);}}
		///<summary/>
        //MAIG - CH1 - BEGIN - CHG0087639 - RepID and UserID Length Expansion - Changed Convert function from Int32 to Int64, Int to long
        public long RepId {get {return (_RepId.Text=="")?0:Convert.ToInt64(_RepId.Text);} set {_RepId.Text=value.ToString();}}
        //MAIG - CH1 - END - CHG0087639 - RepID and UserID Length Expansion - Changed Convert function from Int32 to Int64, Int to long
        ///<summary/>

        //MAIG - CH2 - BEGIN - Included AgencyID & AgencyName to the User Admin screen for all Users
        public long AgencyID { get { return (_AgencyID.Text == "") ? 0 : Convert.ToInt64(_AgencyID.Text); } set { _AgencyID.Text = value.ToString(); } }
        public string AgencyName { get { return _AgencyName.Text; } set { _AgencyName.Text = value; } }
        //MAIG - CH2 - END - Included AgencyID & AgencyName to the User Admin screen for all Users

		public string Roles 
		{
			get 
			{
				if (!this.IsPostBack) return ""; 
				string st="";
				foreach (ListItem I in _Roles.Items)
					if (I.Selected) st += I.Value + ",";
				if (st.Length>0) st=st.Substring(0,st.Length-1);
				return st;
			}
			set {foreach (ListItem Item in _Roles.Items) Item.Selected = value.IndexOf(Item.Value)!=-1;}
		}
		///<summary/>
		public string AppName 
		{
			get {return ListC.GetListValue(_AppName);}
			set {ListC.SetListIndex(_AppName, value);}
		}
		#endregion

		///<summary/>
		private string CurrentUser=string.Empty;

		///<summary/>
		//STAR Retrofit II.Ch1 - START :The boolean variable "Initialized" has been commented as it is no longer being used.
		//private static bool Initialized = false;
		//STAR Retrofit II.Ch1 - END
		/// <summary>
		/// Retrieves the insurance lookup tables from the web service and caches them.
		/// </summary>
		private void Initialize() 
		{
			lock (typeof(User)) 
			{
				//STAR Retrofit II.Ch1 - START :The boolean variable "Initialized" has been commented as it is no longer being used.
				//Initialized=true;
				//STAR Retrofit II.Ch1 - END
				DataTable Dt;
				try 
				{
					//STAR Retrofit II.Ch2 - START :The condition to check if the Cache "AUTH_REPDO" is removed.
					//if (Cache["AUTH_REPDO"]==null) 
					//{
					//STAR Retrofit II.Ch2 - END
						Dt = OrderService.LookupDataSet("Authentication", "GetDOs").Tables[0];
						if (Dt.Rows.Count>1) AddSelectItem(Dt, "Select District Office");
						Cache["AUTH_REPDO"] = Dt;
					//STAR Retrofit II.Ch2 - START :The condition to check if the Cache "AUTH_REPDO" is removed.
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

		///<summary>Validator delegate for the page</summary>
		protected void CheckValid(object Source, ValidatorEventArgs e) 
		{
			e.IsValid=(Valid.ErrorMessage=="");
		}
        //Added as a part of .NetMig 3.5 to check the email field.
        protected void CheckLength(object Source, ValidatorEventArgs e)
        {
            if (_Email.Text == "")
            {
                vldEmail.IsValid = false;
                vldEmail.MarkInvalid();
                vldEmail.ErrorMessage = "";
                //vld.MarkInvalid();
            }
           // e.IsValid = false;
        }
		///<summary>Validator delegate for the email format.</summary>
		protected void CheckEmail(object Source, ValidatorEventArgs e)
        {
            //vldLastName.MarkInvalid();
			if (vldEmail.IsValid)
				e.IsValid=CSAAWeb.Validate.IsValidEmailAddress(Email);
            if (!e.IsValid) { vldEmail.MarkInvalid(); vldEmail.ErrorMessage = ""; }
//Start-Added the fields to check require value as a part of .NetMig 3.5
            if (_FirstName.Text == "")
                vldFirstName.MarkInvalid();


            if (_LastName.Text == "")
                vldLastName.MarkInvalid();

            if (_Email.Text == "")
               vldEmail.MarkInvalid();


            if (_Phone.Text == "")
            {
                Validator lblvld = (Validator)_Phone.FindControl("Valid");
                
                lblvld.MarkInvalid();
                _Phone.MarkInvalid();
            }
              
                 
            if (_RepId.Text == "")
                vldRepId.MarkInvalid();


            if (_DO.SelectedItem.Value=="")
                vldDO.MarkInvalid();

            if (_UserId.Text == "")
                vldUserId.MarkInvalid();

            if (_Roles.SelectedIndex==-1)
                _vldRoles.MarkInvalid();

            //End-Added the fields to check require value as a part of .NetMig 3.5       
            

		}

		/// <summary>
		/// Called when the page loads, prepares to handle any operations by setting up
		/// page objects.  If not postback and a UserRid is supplied, gets the data
		/// from the web service to fill the page.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e) 
		{
            Response.Cache.SetExpires(DateTime.Parse(DateTime.Now.ToString()));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            
            ////Security Defect - CH1 -START Added the below code to verify if the IP address of the login page and current page is same

            //string ipaddr1 = Request.UserHostAddress;
            //if (Request.Cookies["AuthToken"] == null)
            //{
            //    Response.Redirect("/PaymentToolmscr/Forms/login.aspx");
            //}
            //else if (!ipaddr1.Equals(
            //    Request.Cookies["AuthToken"].Value))
            //{
            //    // redirect to the login page in real application
            //    Response.Redirect("/PaymentToolmscr/Forms/login.aspx");

            //}
            ////Security Defect - CH1 -End Added the below code to verify if the IP address of the login page and current page is same
            //else
            //{
                string CurrentUser = Page.User.Identity.Name;
                //STAR Retrofit II.Ch1 - START :The boolean variable "Initialized" has been commented as it is no longer being used.
                //if (!Initialized) Initialize(); 
                //STAR Retrofit II.Ch1 - END
                //STAR Retrofit II.Ch3 - START :The condition to check if the Cache "AUTH_REPDO" is null has been included in the Page_Load event.If it is
                //null,then the Initialize() function is called to populate the cache
                if (Cache["AUTH_REPDO"] == null)
                {
                    Initialize();
                }
                //STAR Retrofit II.Ch3 - END
                if (!IsPostBack)
                {
                    _DO.DataSource = (DataTable)Cache["AUTH_REPDO"];
                    _DO.DataBind();
                    _AppName.DataSource = GetApps(); //(DataTable)Context.Items["Apps"];
                    _AppName.DataBind();
                    AppName = (string)Context.Items["AppName"];
                    OrderService.AppId = AppName;
                    _Roles.DataSource = OrderService.LookupDataSet("Authentication", "GetRoles", new object[] { AppName });
                    _Roles.DataBind();
                    UserRid = (int)Context.Items["UserRid"];
                    if (UserRid != 0) GetUserInfo();
                    else if (Context.Items.Contains("DO"))
                        CSAAWeb.WebControls.ListC.SetListIndex(_DO, (string)Context.Items["DO"]);

                }
            //}
		}

		private DataTable GetApps() 
		{
			DataTable Dt = OrderService.LookupDataSet("Authentication", "GetApplications").Tables[0];
			return Dt;
		}
		private void GetUserInfo() 
		{
			((UserInfo)OrderService.Lookup("Authentication", "GetContactInfo", new object[] {UserRid})).CopyTo(this);
			Update.Visible=true;
			AddUser.Visible=false;
			Caption.Text = "Edit";
			Delete.Visible = (CurrentUser!=UserId);
			ResetPassword.Visible=Delete.Visible;
			_UserId.Enabled=false;
		}

		/// <summary>
		/// Transfers control back to Users page, or restores this page with an error message.
		/// </summary>
		/// <param name="ErrorMessage">The error message to display.</param>
		/// <param name="Message">Message to display on transfer.</param>
		private void Continue(string ErrorMessage, string Message) 
		{
			if (ErrorMessage=="") 
			{
				if (Message!="") Context.Items.Add("Message", Message);
				Context.Items.Add("AppName", AppName);
				Context.Items.Add("DO", DO);
				ShowAll.SaveContext();
				Server.Transfer("Users.aspx");
			} 
			else 
			{
				Valid.ErrorMessage=ErrorMessage;
				Valid.MarkInvalid();
			}
		}

		/// <summary>
		/// Calls the ResetPassword web method.
		/// </summary>
		protected void ResetPassword_onclick(object sender, ImageClickEventArgs e) 
		{
			string Msg=(string)OrderService.Lookup("Authentication", "ResetPassword", new object[] {UserId});
            //67811A0 - PCI Remediation for Payment systems CH1 : Start Arcsight logging -Password Reset
            Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DESTINATION_USER;
            Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS;
            Logger.SourceUserName = Page.User.Identity.Name;
            Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_LOW;
            Logger.DeviceAction ="User "+ UserId + CSAAWeb.Constants.PCI_ARC_NAME_PASSWORD + Page.User.Identity.Name;
            Logger.SourceProcessName = CSAAWeb.Constants.PCI_SOURCE_PROCESS_NAME;
            Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_PASSWORD_RESET;
            Logger.ArcsightLog();
            //67811A0 - PCI Remediation for Payment systems CH1 : End Arcsight logging - Password Reset

			Continue((Msg.IndexOf("ERROR")<0)?"":Msg, Msg);
		}

		/// <summary>
		/// Calls the Delete User web method.
		/// </summary>
		protected void Delete_onclick(object sender, ImageClickEventArgs e) 
		{
			NavACL.ResetNav();
			OrderService.AppId = AppName;
			string Result = (string)OrderService.Lookup("Authentication", "DeleteUser", new object[] {UserRid});
			if (Result!="") GetUserInfo();
            //67811A0 - PCI Remediation for Payment systems CH1 : Start Arcsight logging - Delete user
            Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DESTINATION_USER;
            Logger.SourceUserName = this.User.Identity.Name;
            Logger.SourceProcessName = CSAAWeb.Constants.PCI_SOURCE_PROCESS_NAME;

            if (Result != "")
            {
                Logger.DeviceAction = Result;
                Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_DELETE_USER_FAILED;
            }
            else
            {
                Logger.DeviceAction = "User " + UserId + CSAAWeb.Constants.PCI_ARC_NAME_USER + this.User.Identity.Name;
                Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS;
                Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_LOW;
                Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_DELETE_USER_SUCCESS;
            }


            Logger.ArcsightLog();
            //67811A0 - PCI Remediation for Payment systems CH1 : End Arcsight logging - Delete user

			Continue(Result, "User " + UserId + " was deleted.");
		}

		/// <summary>
		/// Returns to the list of all users without any action.
		/// </summary>
		protected void Cancel_onclick(object sender, ImageClickEventArgs e) 
		{
			Continue("","");
		}
		
		/// <summary>
		/// Calls the Update User web service method if the page is validated.
		/// </summary>
		protected void Update_onclick(object sender, ImageClickEventArgs e) 
		{
			if (Page.IsValid) 
			{
				NavACL.ResetNav();
				OrderService.AppId = AppName;
				ArrayOfErrorInfo Result;
				OrderService.Lookup("Authentication", "UpdateUser", out Result, new object[] {new UserInfo((object)this)});
				if (Result != null) 
				{
					string Msg = Result[0].Target.ToString() + " " + Result[0].Message.ToString();
                    //67811A0 - PCI Remediation for Payment systems CH1 : Start Arcsight logging -Update user's profile
                    Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DESTINATION_USER;
                    Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                    Logger.SourceProcessName = CSAAWeb.Constants.PCI_SOURCE_PROCESS_NAME;
                    Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                    Logger.SourceUserName = this.User.Identity.Name;
                    Logger.DeviceAction = Msg;
                    Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_ADD_USER;
                    Logger.ArcsightLog();
                    //67811A0 - PCI Remediation for Payment systems CH1 : End Arcsight logging -Update user's profile

					Continue(Msg, "User " + UserId + "'s profile could not be " + ((UserRid==0)?"created.":"updated."));
					//vldUserId.MarkInvalid();					
				}
                else
                {
                    //67811A0 - PCI Remediation for Payment systems CH1 : Start Arcsight logging -Update user's profile
                    Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DESTINATION_PROCESSNAME_USER;
                    Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS;
                    Logger.SourceUserName = this.User.Identity.Name;
                    Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_LOW;
                    Logger.SourceProcessName = CSAAWeb.Constants.PCI_SOURCE_PROCESS_NAME;
                    if (this.Caption.Text == "Edit")
                    {
                        Logger.DeviceAction = "User " + UserId + CSAAWeb.Constants.PCI_ARC_NAME_ADDUSER_UPDATE + this.User.Identity.Name;
                        Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_MODIFY_USER_SUCCESS;
                    }
                    else
                    {
                        Logger.DeviceAction = "User " + UserId + CSAAWeb.Constants.PCI_ARC_NAME_ADDUSER_NEW + this.User.Identity.Name;
                        Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_ADD_USER_SUCCESS;
                    }
                    Logger.ArcsightLog();
                    //67811A0 - PCI Remediation for Payment systems CH1 : End Arcsight logging -Update user's profile
                    Continue("", "User " + UserId + "'s profile was " + ((UserRid == 0) ? "created." : "updated."));
                }

		
			}
		}

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
            this.Delete.Click += new System.Web.UI.ImageClickEventHandler(this.Delete_onclick);
            this.Cancel.Click += new System.Web.UI.ImageClickEventHandler(this.Cancel_onclick);
            this.ResetPassword.Click += new System.Web.UI.ImageClickEventHandler(this.ResetPassword_onclick);
            this.Update.Click += new System.Web.UI.ImageClickEventHandler(this.Update_onclick);
            this.AddUser.Click += new System.Web.UI.ImageClickEventHandler(this.Update_onclick);//Added a event as a part of .NetMig3.5
         
		}
		#endregion

		///<summary>Dummy method to force postback.</summary>
		protected void _AppName_SelectedIndexChanged(object sender, System.EventArgs e) 
		{
			OrderService.AppId = AppName;
			_Roles.DataSource=OrderService.LookupDataSet("Authentication", "GetRoles", new object[] {AppName});
			_Roles.DataBind();
			if (UserRid!=0) GetUserInfo();
		}
	}
}

