/*
//67811A0 PCI Remediation for Payment systems :Arcsight logging
 * SSO Integration - CH1 -START- Added the below code to display the page expired message when the back button is clicked. 
 * SSO Integration - CH2 - Commented as a part of SSO Integration to stop redirecting to login.aspx page
 * SSO Integration CH3- START - Added the below code to verify the SSO Response Status and display the message accordingly.
 * CHG0118686 - SLO changes for PSS Users
 * CHG0121437 - Handling the Identity exception during Session timeout
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
using System.Web.Security;
//67811A0 PCI Remediation for Payment systems :Arcsight logging
using CSAAWeb;
using CSAAWeb.AppLogger;
//CHG0118686 - SLO changes for PSS Users
using System.Configuration;
using System.Text;
using System.Net;
using System.IO;
//CHG0118686 - SLO changes for PSS Users

namespace MSCR.Forms
{
	/// <summary>
	/// Summary description for login.
	/// </summary>
	public partial class logout : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator1;
		protected System.Web.UI.WebControls.RequiredFieldValidator RequiredFieldValidator2;
        //67811A0 START - PCI Remediation for Payment systems :Arcsight logging -Logout
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //  CHG0121437  - Begin - SLO changes for PSS Users
            bool isPSSUser = false;
            hdnLogoutType.Value = string.Empty; 
            try
            {
                // SLO changes updating hidden variable for PSS User
                // Reading roles from cookie and use it for SLO url call
                  string strAuthCookie  = string.Empty ;
                  string userData = string.Empty ;
                  HttpCookie aCookie = null;
                  if (Convert.ToBoolean(Convert.ToString(ConfigurationSettings.AppSettings["RedirectLogoutToSSO"])))
                  {
                      if (HttpContext.Current.Request.Cookies[".ASPXAUTH"] != null)
                      {
                          aCookie = Request.Cookies[".ASPXAUTH"];
                      }
                      if (HttpContext.Current.Request.Cookies[".ASPXFORMSAUTH"] != null)
                      {
                          aCookie = Request.Cookies[".ASPXFORMSAUTH"];
                      }
                      if (aCookie != null)
                      {
                          strAuthCookie = Server.HtmlEncode(aCookie.Value);
                          FormsAuthenticationTicket T = FormsAuthentication.Decrypt(strAuthCookie);
                          userData = T.UserData.ToString();
                          if (userData.Length > 0)
                          {
                              if (userData.ToLower().Contains("pss"))
                              {
                                  //isPSSUser = true;
                                  string sloURL = Convert.ToString(ConfigurationSettings.AppSettings["SSO_LOGOUTURL"]);
                                  hdnLogoutType.Value = sloURL;
                              }
                          }
                          else
                          {
                              hdnLogoutType.Value = string.Empty;
                          }
                      }
                  }
                  else
                  {
                      hdnLogoutType.Value =string.Empty;
                  }

            }
            catch (Exception ex)
            {
                hdnLogoutType.Value = string.Empty;
                CSAAWeb.AppLogger.Logger.Log("Error occured while Logout of User:" + ex.Message.ToString());
            }
            //  CHG0121437  - End- SLO changes for PSS Users
            //  SSO Integration - CH1 -START- Added the below code to display the page expired message when the back button is clicked. 
            Response.ClearHeaders();
            Response.AppendHeader("Cache-Control", "no-cache"); //HTTP 1.1 
            Response.AppendHeader("Cache-Control", "private"); // HTTP 1.1  
            Response.AppendHeader("Cache-Control", "no-store"); // HTTP 1.1
            Response.AppendHeader("Cache-Control", "must-revalidate"); // HTTP 1.1
            Response.AppendHeader("Cache-Control", "max-stale=0"); // HTTP 1.1    
            Response.AppendHeader("Cache-Control", "post-check=0"); // HTTP 1.1  
            Response.AppendHeader("Cache-Control", "pre-check=0"); // HTTP 1.1   
            Response.AppendHeader("Pragma", "no-cache"); // HTTP 1.1   
            Response.AppendHeader("Keep-Alive", "timeout=3, max=993"); // HTTP 1.1  
            Response.AppendHeader("Expires", "Mon, 26 Jul 1997 05:00:00 GMT");
            // SSO Integration - CH1 - END-Added the below code to display the page expired message when the back button is clicked. 
            //SSO Integration CH3- START - Added the below code to verify the SSO Response Status and display the message accordingly.
            string status = Request.QueryString["SSOResponse"];
        
            if (status == "Failure")
            {
                lblErrMessage.Text = CSAAWeb.Constants.MSG_INVALIDSAML_RESPONSE;
                plc_SignInAgain.Visible = false;

            }
            else
            {
                lblErrMessage.Text = CSAAWeb.Constants.MSG_LOGOUT;
            }
            //SSO Integration CH3- END - Added the below code to verify the SSO Response Status and display the message accordingly.
            
 
            if (Page.User.Identity.Name != string.Empty)
            {
                    Logger.SourceUserName = Page.User.Identity.Name;
                    Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DESTINATION_PROCESSNAME_LOGIN;
                    Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_LOW;
                    Logger.SourceProcessName = CSAAWeb.Constants.PCI_SOURCE_PROCESS_NAME;
                    Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS;

                    Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_LOGOUT_USER;
                    Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_LOGOUT;
                    Logger.ArcsightLog();
                    //67811A0 END - PCI Remediation for Payment systems :Arcsight logging -Logout
                    FormsAuthentication.SignOut();
                //  CHG0118686 - Begin - SLO changes for PSS Users
                 // if(!isPSSUser)
                    Session.Abandon();
                //  CHG0118686 - End - SLO changes for PSS Users
                // Session.Abandon();
            }
            // SSO Integration - CH2 - Commented as a part of SSO Integration to stop redirecting to login.aspx page
            //else
            //{
            //    Response.Redirect("../default.aspx");
            //}
           




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

       

	}
}
