/*
 * Added the belwo comments by COGNIZANT on MAIG Project 2014
 * MAIG - CH1 - Added the code to get the Menu for the current user and roles
 * MAIG - CH2 - Added the code to fetch the Browser version of Mozilla
 * CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Added the below code to close the open DB connection
*/

using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.ComponentModel;
using System.Collections;
using System.Collections.Specialized;
using CSAAWeb.Serializers;
using CSAAWeb;
using System.Web.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Principal;

namespace CSAAWeb.Navigation {
	/// <summary>
	/// Outputs dynamically generated Javascript for to supply to the menu system.
	/// Uses HTTP headers to allow smart browser caching.
	/// </summary>
	public class MenuData : Page, CSAAWeb.Web.IClosableWeb {
		/// <summary></summary>
		static MenuData() {
			re = new Regex("\\],\\[",RegexOptions.Compiled);
			R1 = new Regex(".*MSIE\\s*(\\d+\\.\\d*).*", RegexOptions.Compiled); 
			R2 = new Regex(".*(\\d+\\.\\d*).*", RegexOptions.Compiled); 
		}

		/// <summary></summary>
		public MenuData() {}
	
		///<summary>The database connection object.</summary>
		private SqlConnection oConn;
		///<summary>Database connection string.</summary>
		private static string ConnectionString = Config.Setting("Navigation_ConnectionString");
		///<summary>Comma separated list of user's roles</summary>
		private string UserRoles = string.Empty;
		///<summary>
		///This is set to true if the if-none-match tag doesn't match computed
		///value in OnLoad, and if so, Render will actually render content.
		///</summary>
		private bool HasContent = false;
		///<summary>List of sites requiring notification that navigation content has changed.</summary>
		private static ArrayList Sites = new ArrayList();
		///<summary>The time of last navigation content change.</summary>
		public static string MenuTime=string.Empty;
		
		///<summary>Sets the static time.</summary>
		protected override void OnInit(System.EventArgs e) {
			UserRoles = GetUserRoles();
			if (MenuTime == "") MenuTime = DateTime.Now.ToString();
			if (Page.Request.ServerVariables["HTTP_IF_NONE_MATCH"]!=ETAG) {
				HasContent = true;
				SetHeaders();
			} else 
				Page.Response.Status = "304 Not Modified";
		}

		///<summary>Closes and kills the database connection if its open.</summary>
		public void Close() {
			if (oConn!=null && oConn.State!=ConnectionState.Closed) oConn.Close();
			oConn=null;
		}
		/// <summary>
		/// Resets the menutime and notifies all sites using acl.
		/// </summary>
		public static void Reset() {
			MenuTime = string.Empty;
			foreach (string site in Sites) ACL.Notify(site);
		}
		///<summary></summary>
		protected override void OnLoad(System.EventArgs e) {
			CheckSite(Page.Request.QueryString["SiteRoot"]);
		}

		/// <summary>
		/// Write the content if needed.
		/// </summary>
		protected override void Render(HtmlTextWriter writer) {
			if (HasContent) writer.Write(createMenuData(false));
		}

		/// <summary>
		/// Sets the response headers that control the browser caching.
		/// </summary>
		public void SetHeaders() {
			Response.ClearHeaders();
			Response.AppendHeader("Last-Modified", DateTime.Now.ToString());
			Response.AppendHeader("ETag", ETAG);
			Response.ContentType="text/js";
		}

		/// <summary>
		/// Checks to see Uri is in the Sites list and adds it if not.
		/// </summary>
		/// <param name="Uri">Uri to add to list.</param>
		protected void CheckSite(string Uri) {
			if (Uri!=null) Uri = Uri.Replace("/","");
			if (Uri!=null && Uri!="" && !Sites.Contains(Uri)) Sites.Add(Uri);
		}

		/// <summary>
		/// This string is used for browse cache control.
		/// </summary>
		public string ETAG { get {return Cryptor.Encrypt(Page.User.Identity.Name + ":" + UserRoles + ":" + MenuTime,"EncryptedETAG");}}
		
		/// <summary>
		/// Creates and opens the database connection if necessary.
		/// </summary>
		private void OpenConnection() {
			if (oConn!=null) return;
			oConn = new SqlConnection(ConnectionString);
			oConn.Open();
		}

		/// <summary>
		/// Reads the user roles from the cookie and reformats them for use by
		/// navigation.
		/// </summary>
		private string GetUserRoles() { 
			return GetUserRoles(Page.User);
		}
		/// <summary>
		/// Reads the user roles from the cookie and reformats them for use by
		/// navigation.
		/// </summary>
		public static string GetUserRoles(IPrincipal User) { 
			try {
				return GetUserRoles(((FormsIdentity)User.Identity).Ticket.UserData);
			} catch {return "";}
		}
		/// <summary>
		/// Reads the user roles from the cookie and reformats them for use by
		/// navigation.
		/// </summary>
		public static string GetUserRoles(string UserData) { 
			try {
				if (UserData.LastIndexOf(",")==UserData.Length-1)
					UserData = UserData.Substring(0,UserData.Length-1);
				string [] A = UserData.ToLower().Split(',');
				UserData = "";
				for (int i=0; i<A.Length; i++) UserData += ((A[i].Length>3)?A[i].Substring(0,3):A[i]) + ",";
				if (UserData.Length>0) UserData = UserData.Substring(0,UserData.Length-1);
				return UserData;
			} catch {return "";}
		}

		/// <summary>
		/// Gets the menu information from the database for the current user and roles
		/// </summary>
		/// <param name="forAdmin">True if this list is for menu admin purposes</param>
		/// <returns>Javascript string</returns>
		private string getMenus(bool forAdmin) {
			SqlCommand Cmd = new SqlCommand("Navigation_GetMenus", oConn);
			Cmd.CommandType = CommandType.StoredProcedure;
			Cmd.Parameters.Add("@CurrentUser", Page.User.Identity.Name);
			Cmd.Parameters.Add("@ForAdmin", forAdmin);
			Cmd.Parameters.Add("@UserRoles", UserRoles);
			return Serializer.ToJsString(Cmd.ExecuteReader());
		}

        //MAIG - CH1 - BEGIN - Added the code to get the Menu for the current user and roles
        /// <summary>
        /// Gets the menu information from the database for the current user and roles
        /// </summary>
        /// <returns>Javascript string</returns>
        public string CommonMenuGenerate()
        {
            OpenConnection();
            SqlCommand Cmd = new SqlCommand("Navigation_GetMenus", oConn);
            Cmd.CommandType = CommandType.StoredProcedure;
            Cmd.Parameters.Add("@CurrentUser", Page.User.Identity.Name);
            Cmd.Parameters.Add("@ForAdmin", false);
            Cmd.Parameters.Add("@UserRoles", GetUserRoles(Page.User));
            //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Added the below code to close the open DB connection - Begin
            string result = Serializer.ToJsString(Cmd.ExecuteReader());
            Close();
            return result;
            //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Commented the below code to close the DB connection  
            //return Serializer.ToJsString(Cmd.ExecuteReader());
            //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Added code to close the open DB connection - End
        }
        //MAIG - CH1 - END - Added the code to get the Menu for the current user and roles

		private static Regex re;

		/// <summary>
		/// Reads the database to get the menu information for the current user plus
		/// the menu properties.
		/// </summary>
		/// <param name="forAdmin">True if this list is for menu admin purposes</param>
		/// <returns>Javascript string</returns>
		private string createMenuData(bool forAdmin) {
			OpenConnection();
			string navRoot = Page.Request.ServerVariables["SCRIPT_NAME"];
			navRoot = navRoot.Substring(0,navRoot.LastIndexOf("/",navRoot.LastIndexOf("/")-1)+1);
			if (forAdmin) navRoot = navRoot.Substring(0,navRoot.LastIndexOf("/",navRoot.Length-1)+1);
			return "menuSource="+re.Replace(getMenus(forAdmin),"],\r\n[") + ";\r\n" + getMenuProperties() + 
				"navRoot = '" + navRoot + "';\r\n" +
				"browserVer = " + BrowserVer() + ";\r\n";
		}

		/// <summary>
		/// Gets the menu properties from the database.
		/// </summary>
		/// <returns>Javascript string</returns>
		private string getMenuProperties() {
			StringBuilder Result = new StringBuilder();
			SqlCommand Cmd = new SqlCommand("SELECT Property, isString, Value FROM MenuProperties", oConn);
			Cmd.CommandType = CommandType.Text;
			SqlDataReader Reader = Cmd.ExecuteReader();
			while (Reader.Read()) {
				Result.Append(Reader.GetString(0) + " = ");
				if (Reader.GetBoolean(1) && Reader.GetString(2) != null)
					Result.Append("'" + Reader.GetString(2) + "';\r\n");
				else if (!Reader.GetBoolean(1) && Reader.GetString(2) == "")
					Result.Append("null;\r\n");
				else
					Result.Append(Reader.GetString(2) + ";\r\n");
			}
			return Result.ToString();
		}

		private static Regex R1; 
		private static Regex R2; 

		/// <summary>
		/// Returns the current browser version.
		/// </summary>
		private string BrowserVer() {
			string agent = Page.Request.ServerVariables["HTTP_USER_AGENT"]; 
			//			string browser="";
			decimal browserVer=0;
			//			string browserPlatform="other";

			if (agent.IndexOf("compatible; MSIE")>0) {
				//				browser = "ie";
				browserVer = Convert.ToDecimal(R1.Replace(agent,"$1"));
				if (agent.IndexOf("Windows NT")>0 || agent.IndexOf("Windows XP")>0) {
					//					browserPlatform = "NT";
				} else if (agent.IndexOf("Windows 9")>0 || agent.IndexOf("Windows M")>0) {
					//					browserPlatform = "9X";
				}
                //MAIG - CH2 - BEGIN - Added the code to fetch the Browser version of Mozilla
			} else if (agent.IndexOf("Mozilla")!=-1) {
                browserVer = Convert.ToDecimal(R2.Replace(agent, "$1"));
                //MAIG - CH2 - END - Added the code to fetch the Browser version of Mozilla
                if (agent.IndexOf("Netscape") > 0)
                {
					//					browser = "ns";
				} 
			}
			return browserVer.ToString();
		}
	}

}