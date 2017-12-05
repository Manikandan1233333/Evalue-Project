using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Web;
using System.Web.Services;


namespace CSAAWeb.Navigation {
	/// <summary>
	/// This service provides remote access to navigation information.
	/// </summary>
	[WebService(Description="This service provides remote access to navigation information.", Namespace="http://csaa.com/webservices")]
	public class Service : System.Web.Services.WebService {
		///<summary></summary>
		public Service() {
			InitializeComponent();
		}

	#region Component Designer generated code
	
		//Required by the Web Services Designer 
		private IContainer components = null;
			
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if(disposing && components != null) {
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
	
	#endregion
		/// <summary>
		/// Returns information about the user's access to Url.
		/// </summary>
		/// <param name="Url">The Url to check for access.</param>
		/// <param name="EncryptedTicket">The ticket cookie (.ASPXAUTH)</param>
		[WebMethod(Description="Returns information about the user's access to Url.")]
		public AccessInfo CheckAccess(string Url, string EncryptedTicket) {
			CSAAWeb.Navigation.ACL.OnInit(this.Context.Request.ApplicationPath);
			return new AccessInfo(Url, EncryptedTicket);
		}
		/// <summary>
		/// Returns information about the user's access to Url.
		/// </summary>
		/// <param name="Url">The Url to check for access.</param>
		/// <param name="UserID">The User to check.</param>
		/// <param name="UserRoles">Comma separated list of user roles</param>
		[WebMethod(Description="Returns information about the user's access to Url.",MessageName="CheckAccess2")]
		public bool CheckAccess(string Url, string UserID, string UserRoles) {
			CSAAWeb.Navigation.ACL.OnInit(this.Context.Request.ApplicationPath);
			ArrayList Roles = new ArrayList();
			Roles.AddRange(UserRoles.Split(','));
			return CSAAWeb.Navigation.ACL.CheckAccess(Url, UserID, Roles);
		}
		/// <summary>
		/// Resets the navigation system.
		/// </summary>
		[WebMethod(Description="Resets the navigation system.")]
		public void Reset() {
			CSAAWeb.Navigation.MenuData.Reset();
		}

		/// <summary>
		/// Returns the URL Access Control List.
		/// </summary>
		[WebMethod(Description="Returns the URL Access Control List")]
		public ArrayOfACLEntry ACL() {
			return CSAAWeb.Navigation.ACL.List;
		}
		
		/// <summary>
		/// Returns the URL for redirection on an unauthorized page request.
		/// </summary>
		[WebMethod(Description="Returns the URL for redirection on an unauthorized page request.")]
		public string UnauthorizedUrl() {
			return CSAAWeb.Navigation.ACL.UnauthorizedUrl;
		}

		/// <summary>
		/// Returns the starting Url for a list of roles.
		/// </summary>
		[WebMethod(Description="Returns the starting Url for a list of roles.")]
		public string StartUrl(string UserRoles) {
			return CSAAWeb.Navigation.ACL.GetStartUrl(UserRoles);
		}
	}
}
