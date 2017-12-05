/*	Revision History:
 *	4/15/03 - JOM Modified CheckPageAccess to correct problem in logic that only
 *  allowed a user to have a single role.
 *  
 *	MODIFIED BY COGNIZANT 
 *	07/15/2005 - For changing the web.config entries pointing to database. This changes 
 *				 are made as part of CSR #3937 implementation.
 *	CSR#3937.Ch1 - Include more namespaces for using the StringDictionary object and Dataset
 *  CSR#3937.Ch2 - Check for Constants (Application object) exists in application, if not populate it.
 *  CSR#3937.Ch3 - Added a new private method for populating the constants into the Application object
 *  CSR#3824.ch1 - 10/10/2005 - Handle the viewstate errors and mark it in the log file
 *  67811A0  - PCI Remediation for Payment systems.CH1:start added the code to remover the IIS server version during the response from web service call as a part of VA scan defect by cognizant on 1/5/2011 
 *  MAIG - CH1 - Enhanced the Logging to log the Outer Exception details also
*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Web;
using System.Web.SessionState;
using System.Web.Security;
//CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016
using CSAAWeb;

//CSR#3937.Ch1  :	Include more namespaces for using the StringDictionary object and Dataset
using System.Data;
using System.Data.SqlClient;
using System.Collections.Specialized;
using InsuranceClasses.Service;
//END CSR#3937.Ch1 :  Include more namespaces for using the StringDictionary object and Dataset
//CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Added the below namespace for fixing the SSO SHA 256 XML Signature support
using System.Security.Cryptography;
using System.Deployment.Internal.CodeSigning;
//CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Added the below namespace for fixing the SSO SHA 256 XML Signature support

namespace MSCR
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

        // cached dataset...
        public static string LOOKUP_CACHE = "LOOKUP_CACHE";
        // ... containing these lookup tables
        public static string STATECODE_TABLE = "STATES_TABLE";
        public static string CREDITCARD_TABLE = "CREDITCARD_TABLE";


        public Global()
        {
            InitializeComponent();
        }
        //67811A0  - PCI Remediation for Payment systems.CH1:end added the above line of code to remover the IIS server version during the response from web service call as a part of VA scan defect by cognizant on 1/5/2011
        protected void Application_PreSendRequestHeaders(object sender, EventArgs e)
        {
            HttpContext.Current.Response.Headers.Remove("Server");
        }
        //67811A0  - PCI Remediation for Payment systems.CH11:end added the above line of code to remover the IIS server version during the response from web service call as a part of VA scan defect by cognizant on 1/5/2011

        protected void Application_Start(Object sender, EventArgs e)
        {




            try
            {
                // create empty dataset for lookup tables
                Context.Cache.Insert(LOOKUP_CACHE, new System.Data.DataSet());

                //_SetMemberDatabase();

                Application["PaymentProcessorKey"] = Config.Setting("WebService.Payments.AppKey");

                // TODO: eliminate hardcoded values
                AdministrativeContact appAdmin;
                appAdmin.ContactName = "Doreen Earle";
                appAdmin.ContactPhone = "(925) 454-2600 x2557";
                appAdmin.ContactEmail = "doreen_earle@csaa.com";

                Application["AdminContact"] = appAdmin;

                Application["GeneralErrorPage"] = Config.Setting("MSCR_GENERAL_ERROR_PAGE");

                Application["MemberSearchMaxRows"] = Config.iSetting("MemberSearchMaxRows");

                // for debugging use only, preserves forms field values in cookies
                Application["SAVE_SETTINGS"] = Config.Setting("SAVE_SETTINGS");

                Application["CSAAOrdersKey"] = Config.Setting("CSAA_ORDERS_KEY");

                //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Added the below code for fixing the SSO SHA 256 XML Signature support
                CryptoConfig.AddAlgorithm(typeof(RSAPKCS1SHA256SignatureDescription), "http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");
                //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Added the below code for fixing the SSO SHA 256 XML Signature support

            }
            catch
            {
                throw;
            }

        }

        protected void Session_Start(Object sender, EventArgs e)
        {

        }

        protected void Application_BeginRequest(Object sender, EventArgs e)
        {
            //if (Application["MemberConnectionString"] == null) _SetMemberDatabase();

            //CSR#3937.Ch2 : Check for Constants (Application object) exists in application, if not populate it.
            if (Application["Constants"] == null) _GetConstants();
            //END CSR#3937.Ch2 : Check for Constants (Application object) exists in application, if not populate it.
        }

        protected void Application_EndRequest(Object sender, EventArgs e)
        {

        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
        }

        protected void Application_Error(Object sender, EventArgs e)
        {
            //CSR#3824.ch1 Handle the viewstate errors and log the error in log file
            //MAIG - CH1 - BEGIN - Enhanced the Logging to log the Outer Exception details also
            Exception ex = Context.Error;
            CSAAWeb.AppLogger.Logger.Log(ex);
            CSAAWeb.AppLogger.Logger.Log(((HttpApplication)sender).Request.Path + "," + ex.Message + ", User:" + ((HttpApplication)sender).User.Identity.Name);
            ex = Context.Error.GetBaseException();
            //MAIG - CH1 - END - Enhanced the Logging to log the Outer Exception details also
            if (ex.Message.IndexOf("View State") > 0)
            {
                FormsAuthentication.SignOut();
                //External URL Implementation-Modified the below  code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  
                Response.Redirect("/PaymentToolmscr/Forms/logout.aspx");
            }
            //CSR#3824.ch1 - end
        }

        protected void Session_End(Object sender, EventArgs e)
        {

        }

        //private void _SetMemberDatabase()
        //{

        //    try
        //    {
        //        //Changed as part of .Net Migration to 3.5
        //        // get locatsion of member data from search service

        //        //	string s=new MembershipClasses.Service.Membership().GetMemberDatabase(); 
        //        string s = new MembershipClasses.WebService.Membership().GetMemberDatabase();
        //        Application["MemberConnectionString"] = s;
        //    }
        //    catch { }

        //}

        //CSR#3937.Ch3 : Added a new private method for populating the constants into the Application object
        private void _GetConstants()
        {
            try
            {
                DataSet dsConstants = new Insurance().PayGetConstants();
                StringDictionary sdConstants = new StringDictionary();

                foreach (DataRow dr in dsConstants.Tables[0].Rows)
                {
                    //Populate the value from dataset to StringDictionary object
                    sdConstants.Add(dr[0].ToString(), dr[1].ToString());
                }

                //Added the resultant stringdictionary object into Application object.
                Application.Add("Constants", sdConstants);

            }
            catch { }

        }
        //END CSR#3937.Ch3 : Added a new private method for populating the constants into the Application object

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

