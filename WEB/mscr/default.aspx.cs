//MAIG - CH1 - Added code to pass the Session for NewInsurance page alone
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
using System.Web.Security;
using CSAAWeb.AppLogger;

namespace MSCR
{
	/// <summary>
	/// Summary description for _default.
	/// </summary>
	public partial class _default : CSAAWeb.WebControls.PageTemplate
	{
		protected void Page_Load(object sender, System.EventArgs e){
			// this page simply redirects to user's view 
            // SSO Integration - CH1 Start -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            // SSO Integration - CH1 End -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit
		}
		protected override void Render(HtmlTextWriter output) {
			base.Render(output);
			_TransferOnUserRole();
		}

		#region Web Form Designer generated code
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
		}
		#endregion

		/// <summary>
		/// 
		/// </summary>
		private void _TransferOnUserRole() {
			string Url = Request.QueryString["Url"];
			if (Url!=null && Url!="") {
				string Token = Request.QueryString["Token"];
				string UserId = Request.QueryString["User"];
				if (Token!=null && UserId!=null && (new OrderClasses.Service.Order(Page).ValidateToken(Token, UserId))) {
					if (System.IO.File.Exists(Server.MapPath(Url))) Response.Redirect(Url);
					else Response.Redirect(Application["GeneralErrorPage"].ToString());
				}
			}
			if (User.Identity.IsAuthenticated) {
				// get the authentication ticket
				FormsAuthenticationTicket tkt = ((FormsIdentity)User.Identity).Ticket;

				string StartUrl = NavACL.StartUrl((((FormsIdentity)User.Identity).Ticket).UserData);
				//MAIG - CH1 - BEGIN - Added code to pass the Session for NewInsurance page alone
                if (StartUrl.Equals("/PaymentToolmsc/forms/newinsurance.aspx"))
                {
                    //Request.Cookies.Add(new HttpCookie("IsDown", "false"));
                   // Response.AppendCookie(new HttpCookie("IsDown", "false"));
                    //Context.Session.Add("IsDown", "false");
                    Response.Cookies.Add(new HttpCookie("IsDown", "false"));
                }
				//MAIG - CH1 - END - Added code to pass the Session for NewInsurance page alone
				if (StartUrl!=null && StartUrl!="")
					Response.Redirect(StartUrl);
				else {
					Logger.Log("Unable to determine user role!! Roles returned from service (UserData) = " + tkt.UserData);
					Response.Redirect(Application["GeneralErrorPage"].ToString());
				}
			}
			else {
				// force user to log in
				Response.Redirect("Forms/login.aspx?ReturnUrl=" + Request.Url);
			}
		}
	}
}
