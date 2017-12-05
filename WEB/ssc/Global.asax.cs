using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using CSAAWeb.Web;

namespace ssc 
{
	/// <summary>
	/// Summary description for Global.
	/// </summary>
	public struct AdministrativeContact 
	{
		public string ContactName;
		public string ContactPhone;
		public string ContactEmail;
	}

	public class Global : System.Web.HttpApplication
	{
		public Global()
		{
			InitializeComponent();
		}
        //67811A0  - PCI Remediation for Payment systems.CH1:start added the below line of code to remover the IIS server version during the response from web service call as a part of VA scan defect by cognizant on 1/5/2011            
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("Server");
        }
        //67811A0  - PCI Remediation for Payment systems.CH1:end added the above line of code to remover the IIS server version during the response from web service call as a part of VA scan defect by cognizant on 1/5/2011
		protected void Application_Start(Object sender, EventArgs e)
		{
			// TODO: eliminate hardcoded values
			AdministrativeContact appAdmin;
			appAdmin.ContactName = "Doreen Earle";
			appAdmin.ContactPhone = "(925) 454-2600 x2557";
			appAdmin.ContactEmail = "doreen_earle@csaa.com";

			Application["AdminContact"]		= appAdmin;
		}
 
		protected void Session_Start(Object sender, EventArgs e)
		{

		}

		protected void Application_BeginRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_EndRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_AuthenticateRequest(Object sender, EventArgs e)
		{

		}

		protected void Application_Error(Object sender, EventArgs e)
		{

		}

		protected void Session_End(Object sender, EventArgs e)
		{

		}

		#region Web Form Designer generated code
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

