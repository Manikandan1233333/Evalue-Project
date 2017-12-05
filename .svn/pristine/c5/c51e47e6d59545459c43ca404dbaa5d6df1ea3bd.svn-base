//Global.asax.cs file Added by Cognizant on 10/10/2005 to catch the viewstate errors and redirect to login page.
//67811A0  - PCI Remediation for Payment systems.CH1:start added the code to remover the IIS server version during the response from web service call as a part of VA scan defect by cognizant on 1/5/2011 
//MAIG - CH1 - Enhanced the Logging to log the Outer Exception details also
using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
using CSAAWeb; 

namespace MSC {


	public class Global : System.Web.HttpApplication
	{
        //67811A0  - PCI Remediation for Payment systems.CH1:start added the below line of code to remover the IIS server version during the response from web service call as a part of VA scan defect by cognizant on 1/5/2011            
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("Server");
        }
        //67811A0  - PCI Remediation for Payment systems.CH1:end added the above line of code to remover the IIS server version during the response from web service call as a part of VA scan defect by cognizant on 1/5/2011
		protected void Application_Error(Object sender, EventArgs e) 
		{	//Added by cognizant on 10/10/2005 to catch the viewstate errors	
            //MAIG - CH1 - BEGIN - Enhanced the Logging to log the Outer Exception details also
            Exception ex = Context.Error;
			CSAAWeb.AppLogger.Logger.Log(ex);
			CSAAWeb.AppLogger.Logger.Log(((HttpApplication)sender).Request.Path+","+ex.Message+", User:"+((HttpApplication)sender).User.Identity.Name);
            ex = Context.Error.GetBaseException();
            //MAIG - CH1 - END - Enhanced the Logging to log the Outer Exception details also
   			if(ex.Message.IndexOf("View State")>0)
			{FormsAuthentication.SignOut();
                      //External URL Implementation-Modified the below  code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach 
                          Response.Redirect("/PaymentToolmscr/Forms/login.aspx");}
				
		}
	}
}

