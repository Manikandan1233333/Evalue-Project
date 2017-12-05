////Security Defect - CH1 -START Added the below code to verify if the IP address of the login page and current page is same
// SSO Integration - CH1  -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
/*
CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removed the navigation to member_search.aspx as part of code Clean Up - March 2016
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
using System.Net;
using System.Net.Sockets;
using CSAAWeb.AppLogger;


namespace MSCR.Admin
{
	/// <summary>
	/// Summary description for admin_contact.

	/// </summary>
    /// 
    public partial class admin_contact : CSAAWeb.WebControls.PageTemplate
  
    {
		protected void Page_Load(object sender, System.EventArgs e)
		{
            // SSO Integration - CH1 Start -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();

            // SSO Integration - CH1 End -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit
            

////Security Defect - CH1 -START Added the below code to verify if the IP address of the login page and current page is same

//            string ipaddr1 = Request.ServerVariables["REMOTE_ADDR"];
//            if (CSAAWeb.AppLogger.Logger.Sourcereg(ipaddr1))
//            {
//                ipaddr1 = Request.ServerVariables["REMOTE_ADDR"];
//            }
//            else
//            {
//                ipaddr1 = CSAAWeb.AppLogger.Logger.GetIPAddress();
//            }
//            if (Request.Cookies["AuthToken"] == null)
//            {
//                Logger.Log(CSAAWeb.Constants.SEC_COOKIE_NULL);
//                Response.Redirect("/PaymentToolmscr/Forms/login.aspx");

//            }
//            else if (!ipaddr1.Equals(
//                Request.Cookies["AuthToken"].Value))
//            {
//                Logger.Log(CSAAWeb.Constants.SEC_COOKIE_NOTEQUAL);
//                Response.Redirect("/PaymentToolmscr/Forms/login.aspx"); ;

//            }
////Security Defect - CH1 -End Added the below code to verify if the IP address of the login page and current page is same
            
			// Put user code to initialize the page here
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

		//CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removed the navigation to member_search.aspx as part of code Clean Up - March 2016
	}
}
