/* History
 * RFC 185138 - AD Integration - CH1  Added the below code to validate the user credentials against the AD
* RFC 185138 - AD Integration - CH2 Commented the below code of locked out check in Authentication DB
* RFC 185138 - AD Integration CH3 Commented the error message to enable the message from AD to display in case of invalid credentials.
 * * RFC 185138 - AD Integration CH4  Commented the below code of password Expired Check in Authentication DB
 * RFC 185138 - AD Integration CH5 - Added the belowcode to log the events in Arcsight in case of AD authentication failure
 * Security Testing - CH1 - Added the below code to set the secure attribute of the cookie
 * Security TEsting - CH2 - To set the secure and HttpOnly attribute of the cookie
 * Security Defect -CH3 - Added the below code to store the  IP address of the login page in a cookie
 * Security Defect CH4 - Modified the expire hours from 12 to 1 to ensure the persistent property of th cookie is secuer
 * SSO Integration - CH1 - Added the below code to declare variables for requesting SSO request and Processing SSO REsponse
 * SSO Integration - CH2 - Added the below code to request to SSO Server and send the response to ProcessSSOResponse() Method
 * SSO Integration - CH3 - Added the below code to retrieve the SSO Status and validating with credentials with Payment Tool DB
 * SSO Integration - CH4 - Added the below code to display the error message when authorization part failed in Payemnt Tool DB
 * SSO Integration - CH5 - Added the below code to display the error message when authentication failed in  SSO Server
 * SSO Integration - CH6 - Added the below code to redirect to logout page when the session gets expired
 * SSO Integration - CH7 - Added for SSO session and SSO login page handling.  
 * SSO Integration - CH8 - Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
 * SSO Integration - SAML Validation CH1 - Modified the below code to validate the SAML reponse source against the certficates in personal store
 * MAIG - CH1 - Enhanced the Logging to include the logged-in UserName.
 * MAIG - CH2 - Enhanced the Logging to log if the Session is abandoned due to Authentication failure
 * MAIG - CH3 - Enhanced the Logging to log the SSO response received
 * MAIG - CH4 - Enhanced the Logging to log if the SSO has a failure reponse 
 * MAIG - CH5 - Enhanced the Logging to log if the logged-in user details are retrieved from database
 * MAIG - CH6 - Enhanced the Logging to log the User ID from the SSO Response
 * CHG0118686 - SLO changes for PSS Users
 * CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Security Testing - Added the below code to set the secure attribute of the cookie - VA DefectID - 210
 * Changed the below code to clear the cache of the page to prevent the page loading on browser back button hit 
 * CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Defect 210 - Set HttpOnly for Cookie
 */
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Collections.Generic;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Text;
using AuthenticationClasses;
using OrderClasses.Service;
using CSAAWeb;
using CSAAWeb.AppLogger;
using System.Net.Security;
using System.Xml;
using System.Configuration;
using System.Text.RegularExpressions;
using System.Net;
using System.IO;
using System.Collections.Specialized;
using System.Net.Sockets;
using System.Reflection;

namespace MSCR.Forms
{
    /// <summary>
    /// Summary description for login.
    /// </summary>
    public partial class login : System.Web.UI.Page
    {

        protected void Page_Load(object sender, System.EventArgs e)
        {
            //Changed the below code to clear the cache of the page to prevent the page loading on browser back button hit - Start
            Response.Cache.SetExpires(DateTime.Parse(DateTime.Now.ToString()));
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetNoStore();
            //Changed the below code to clear the cache of the page to prevent the page loading on browser back button hit - End
            if (Page.ClientQueryString != "")
            {
                Logger.Log("Session Expired/Redirecting to logout Page " + Page.User.Identity.Name);
                Server.Transfer("logout.aspx");

            }
            string ssoResponse = string.Empty;
            string ssoResponseStatus = string.Empty;
            string samlResponse = string.Empty;
            string[] samlResponseList = null;
            string ssoResponseUserName = string.Empty;
            UserInfo userInformation = new UserInfo();
            //MAIG - CH1 - BEGIN - Enhanced the Logging to include the logged-in UserName.
            //MAIG - CH1 - END - Enhanced the Logging to include the logged-in UserName.
            // Security TEsting - START - To Clear the sessionID on logout event  and confirming the SessionID is not generated on user's browser 
            // SSO Integration - CH1 - START-Added the below code to declare variables for requesting SSO request and Processing SSO REsponse
            if ((Page.Request.Form.Keys.Count == 0) && Page.User.Identity.Name != null)
            {
                Logger.Log("Request sent to SSO " + Page.User.Identity.Name);
                Response.Redirect(ConfigurationSettings.AppSettings["SSO_URL"].ToString());
            }
            // SSO Integration - CH1 -END- Added the below code to declare variables for requesting SSO request and Processing SSO REsponse
            //SSO Integration - CH6 -START- Added the below code to redirect to logout page when the session gets expired 

            if (!Page.User.Identity.IsAuthenticated)
            {
                if (Page.Request.Cookies["ASP.NET_SessionId"] != null)
                {
                    Response.Cookies["ASP.NET_SessionId"].Expires = DateTime.Now.AddHours(-20);
                    // Security Testing - CH1 - Added the below code to set the secure attribute of the cookie
                    Response.Cookies["ASP.NET_SessionId"].Secure = true;
                    //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Security Testing - Added the below code to set the HttpOnly Attribute to Cookie - VA DefectID - 210 - Start
                    //CHG0129017 - Commented the HttpOnly attribute - Sep 2016
                    //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Start
                    Response.Cookies["ASP.NET_SessionId"].HttpOnly = true;
                    //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES- End
                    //CHG0129017 - Commented the HttpOnly attribute - Sep 2016
                    //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Security Testing - Added the below code to set the HttpOnly Attribute to Cookie - VA DefectID - 210 - End
                }
                //MAIG - CH2 - BEGIN - Enhanced the Logging to log if the Session is abandoned due to Authentication failure
                Logger.Log("The User is not Authenticated, So Session is Abandoned ");
                //MAIG - CH2 - END - Enhanced the Logging to log if the Session is abandoned due to Authentication failure
                Session.Abandon();
            }
            //SSO Integration - CH2 - START - Added the below code to request to SSO Server and send the response to ProcessSSOResponse() Method

            NameValueCollection pageCollection = Page.Request.Form;
            string[] starryPageKey = pageCollection.AllKeys;
            List<string> lstPageKey =
                new List<string>(starryPageKey);
            if (!lstPageKey.Contains("SAMLResponse"))
            {
                Logger.Log("Request sent again to SSO since the SAMLResponse is not present in the response.");
                Response.Redirect(ConfigurationSettings.AppSettings["SSO_URL"].ToString());
            }
            else
            {
                //MAIG - CH3 - BEGIN - Enhanced the Logging to log the SSO response received
                Logger.Log("Response received from SSO. ");
                if (Config.Setting("Logging.SSO").Equals("1"))
                {
                    Logger.Log("SSO Response: " + Page.Request.Form["SAMLResponse"].ToString());
                }
                //MAIG - CH3 - END - Enhanced the Logging to log the SSO response received
            }
            //SSO Integration - CH7 - END-Added for SSO session and SSO login page handling.    

            samlResponse = Page.Request.Form["SAMLResponse"].ToString();
            ssoResponse = userInformation.ProcessSSOResponse(samlResponse);
            // SSO Integration - CH2 - END - Added the below code to request to SSO Server and send the response to ProcessSSOResponse() Method
            //SSO Integration -SAML Validation - CH1 - START - Added the below code to redirect the user to logout page when the validation of SAML fails
            if (ssoResponse == CSAAWeb.Constants.SAML_FAIL_RESPONSE)
            {
                //MAIG - CH4 - BEGIN - Enhanced the Logging to log if the SSO has a failure reponse 
                Logger.Log("Failure Response received from SSO.");
                //MAIG - CH4 - END - Enhanced the Logging to log if the SSO has a failure reponse 
                //Sending the SSO REsponse Status to logout.aspx Page
                Response.Redirect("Forms/logout.aspx?" + "&SSOResponse=Failure");

            }
            //SSO Integration - SAML Validation - CH1 - START - Added the below code to redirect the user to logout page when the validation of SAML fails
            if (ssoResponse.Contains("-"))
            {
                samlResponseList = ssoResponse.Split('-');
                //MAIG - CH6 - BEGIN - Enhanced the Logging to log the User ID from the SSO Response
                if (samlResponseList.Length > 1)
                {
                    Logger.Log("SSO UserID: " + samlResponseList[1]);
                }
                //MAIG - CH6 - END - Enhanced the Logging to log the User ID from the SSO Response
                if (samlResponseList[0] != null)
                {
                    ssoResponseStatus = samlResponseList[0].Trim();
                }
                else
                {

                    Error_Message.Visible = true;
                    Error_Message.Text = CSAAWeb.Constants.SSO_SAML_INVALID_STAUSUSERNAME_ERR_MESSAGE;
                }

                if (samlResponseList[1] != null)
                {
                    ssoResponseUserName = samlResponseList[1].Trim();
                }
                else
                {
                    Error_Message.Visible = true;
                    Error_Message.Text = CSAAWeb.Constants.SSO_SAML_INVALID_STAUSUSERNAME_ERR_MESSAGE;
                }
                if ((ssoResponseStatus == Config.Setting("SSO_STATUS")))
                {
                    try
                    {
                        //Order service Authenticate web method is invoked to get the user details.
                        userInformation = new Order().Authenticate(ssoResponseUserName, "", Config.Setting("AppName"));
                        if (!IsPostBack)
                        {
                            if (userInformation.Authenticated)
                            {
                                //MAIG - CH5 - BEGIN - Enhanced the Logging to log if the logged-in user details are retrieved from database
                                Logger.Log("Successfully retrieved the Logged-in User details from DB. UserID: " + userInformation.UserId);
                                //MAIG - CH5 - END - Enhanced the Logging to log if the logged-in user details are retrieved from database
                                // build authentication ticket and sends user somewhere
                                userInformation.SetAuthentication(this);
                                Response.Redirect(FormsAuthentication.GetRedirectUrl(userInformation.UserId, false));
                            }
                            else
                            {
                                //SSO Integration - CH4 - START-Added the below code to display the error message when authorization part failed in Payemnt Tool DB
                                Error_Message.Visible = true;
                                Error_Message.Text = CSAAWeb.Constants.SSO_PT_DB_INVALID_ERR_MESSAGE;
                                //SSO Integration - CH4 - END-Added the below code to display the error message when authorization part failed in Payemnt Tool DB
                                Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DESTINATION_PROCESSNAME_LOGIN;
                                Logger.SourceProcessName = "APDS";
                                Logger.SourceUserName = ssoResponseUserName;
                                Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                                Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                                Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_LOGIN_INCORRECT;
                                Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;
                                Logger.ArcsightLog();
                            }
                        }
                        else
                        {
                            Response.Redirect("Forms/logout.aspx");
                        }
                    }
                    catch (Exception ex)
                    {
                        //Response.Write(ex.Message);
                        Error_Message.Visible = true;
                        Error_Message.Text = MSCRCore.MessageConstants.APPLICATION_ERROR;
                        Logger.Log(ex);
                        //_SetStatusMessage(MSCRCore.MessageConstants.APPLICATION_ERROR, true);
                    }
                }


                //SSO Integration - CH5 - START- Added the below code to display the error message when authentication failed in  SSO Server
                else
                {
                    Logger.Log(CSAAWeb.Constants.SSO_SAML_INVALID_STAUSUSERNAME_ERR_MESSAGE);
                    Error_Message.Visible = true;
                    Error_Message.Text = CSAAWeb.Constants.SSO_SAML_INVALID_STAUSUSERNAME_ERR_MESSAGE;
                }
            }
            else
            {
                Logger.Log(CSAAWeb.Constants.SSO_SAML_INVALID_STAUSUSERNAME_ERR_MESSAGE);
                Error_Message.Visible = true;
                Error_Message.Text = CSAAWeb.Constants.SSO_SAML_INVALID_STAUSUSERNAME_ERR_MESSAGE;
            }
            //SSO Integration - CH5 - END- Added the below code to display the error message when authentication failed in  SSO Server
            // SSO Integration - CH2 - END - Added the below code to retrieve the SSO Status and validating with credentials with Payment Tool DB




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

            //this.btnLogin.Click += new System.Web.UI.ImageClickEventHandler(this.btnLogin_Click);
            // this.Load += new System.EventHandler(this.Page_Load);

        }
        #endregion

    }
}


