/*
 * History:
 * 
 * MODIFIED BY: COGNIZANT 
 * MODIFIED ON: 12/16/2005 
 * Q4-Retrofit.Ch1: Rep DO changes-	The xml attribute DO is mapped to Physical location. The set 
 *								function removed to disable the mapping of Physical location to DO.
 * RFC 185138 - AD Integration CH1 -  Added the below method to get the list of users from the LDAP server
 * RFC 185138 - AD Integration CH2 -  Added the below method to validate the credential against the LDAP server
 * //RFC 185138 - AD Integration: Defect 226 Made code changes to display different error message for the user whose user id does not exists in Payment tool data base by cognizant on 05/03/2012
 * SSO Integration - CH1 - Added the below code to retrieve the SSO Response ,decrypt and return the Status back to the login.aspx.cs page
 * CHG0078293 - PT Enhancement - Modified the below condition to check the minimum length of user ID from 5 to 4 digits
 * MAIG - CH1 - CHG0087639 - RepID and UserID Length Expansion - Int to long
 * MAIG - CH2 - Added the below fields to update the AgencyID and AgencyName
 * MAIG - CH3 - CHG0087639 - RepID and UserID Length Expansion - Changed Convert function from Int32 to Int64
 * MAIG - CH4 - Added the Agency ID as part of the userData
 * MAIG - CH5 - Added the Agency ID as part of the userData
 * CHG0116140 - Code changes for Reply attack SSO SAML token validation for NotBefore and NotOnOrAfter nodes 
 * CHG0118686  - Added Logs to validate Reply attack
 * CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - IP Address fix for external URL
 */
using System;
using System.ComponentModel;
using System.Data.SqlClient;
using System.DirectoryServices;
using System.Security.Cryptography.X509Certificates;
using System.Security.Cryptography.Xml;
using System.Text;
using System.Web.Security;
using System.Web.UI;
using System.Xml;
using System.Xml.Serialization;
using CSAAWeb;
using CSAAWeb.AppLogger;
using CSAAWeb.Serializers;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Configuration;
using System.Web;
using System.Linq;

namespace AuthenticationClasses
{
    #region SessionInfo
    /// <summary>
    /// Class for holding a user's authentication session data.
    /// </summary>
    public class SessionInfo : SimpleSerializer
    {
        /// <summary>Default constructor</summary>
        public SessionInfo() : base() { }
        /// <summary>Constructor from another object</summary>
        public SessionInfo(Object O) : base(O) { }
        /// <summary>Xml constructor</summary>
        public SessionInfo(string Xml) : base(Xml) { }
        /// <summary>Constructor with User and App</summary>
        /// <param name="User">(string) UserId for this session.</param>
        /// <param name="App">(string) Application name (code) for this session.</param>
        public SessionInfo(string User, string App)
            : base()
        {
            CurrentUser = User;
            AppName = App;
        }
        /// <summary>Constructor with User, App and Token</summary>
        /// <param name="User">(string) UserId for this session.</param>
        /// <param name="App">(string) Application name (code) for this session.</param>
        /// <param name="T">(string) Authentication provider generated session token.</param>
        public SessionInfo(string User, string App, string T)
            : base()
        {
            CurrentUser = User;
            AppName = App;
            Token = T;
        }
        ///<summary>(string) UserId for this session.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string CurrentUser = "";
        ///<summary>(string) Application name (code) for this session.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string AppName = "";
        ///<summary>(string) Authentication provider generated session token.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string Token = "";
    }
    #endregion
    #region UserInfo
    /// <summary>
    /// Encapsulates site user information.
    /// </summary>

    public class UserInfo : ValidatingSerializer
    {
        /// <summary>Default constructor</summary>
        public UserInfo() : base() { }
        /// <summary>Constructor from another object</summary>
        public UserInfo(Object O) : base(O) { }
        /// <summary>Xml constructor</summary>
        public UserInfo(string Xml) : base(Xml) { }
        /// <summary>Constructor for userinfo from the web page, using the page's IPrinciple.</summary>
        /// <param name="Page">The Web Page</param>
        /// <remarks>
        /// Note that this constructor replaces an older one passing the page's user object.  This allows
        /// for further access to the page's other properties, since the principle may not contain all the
        /// required data.  
        /// </remarks>
        public UserInfo(System.Web.UI.Page Page)
            : base()
        {
            UserId = Page.User.Identity.Name;
            UserIdentityManager.FromPage(this, Page);
        }
        #region Data Attributes
        ///<summary>(int) Record Id for this user.  This attribute is deprecated, and its use should
        ///be avoided, because not all providers support it.</summary>
        [XmlAttribute]
        [DefaultValue(0)]
        public int UserRid = 0;
        ///<summary>(string) Unique alphanumeric id for the user.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorRequired]
        [ValidatorAlphaNumeric]
        [Validator(Validation = "ValidateUserId", Code = "INVALID_USERID", Message = "Field must have at least 5 characters.", Priority = 3)]
        public string UserId = string.Empty;
        ///<summary>(string) Given name of user.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorRequired]
        public string FirstName = string.Empty;
        ///<summary>(string) Surname of user.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorRequired]
        public string LastName = string.Empty;
        ///<summary>(string) Email address of user.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorRequired]
        public string Email = string.Empty;
        ///<summary>(string) Phone number of user.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        [ValidatorRequired]
        public string Phone = string.Empty;
        ///<summary>(string) Session token for user from authentication provider.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string Token = string.Empty;
        ///<summary>(boolean) Indicator of whether record is active (enabled).</summary>
        [XmlAttribute]
        [DefaultValue(false)]
        public bool Active = false;
        ///<summary>(string) The district office number.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string DO = string.Empty;
        ///<summary>(string) The physical office location, corresponds to DO.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string PhysicalLocation
        {
            get { return DO; }
            //Q4-Retrofit.Ch1: START - The below line commented so that DO is mapped to physical location and the reverse is disabled.
            //set {DO=value;}
            //Q4-Retrofit.Ch1: END
        }
        ///<summary>(string) The financial office location.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string FinancialLocation = string.Empty;
        //MAIG - CH1 - BEGIN - CHG0087639 - RepID and UserID Length Expansion - Int to long
        ///<summary>(int) The sales rep number.</summary>
        [XmlAttribute]
        [DefaultValue(0)]
        public long RepId = 0;
        //MAIG - CH1 - END - CHG0087639 - RepID and UserID Length Expansion - Int to long
        ///<summary>(int) The employee Id.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string EmployeeId = "";
        ///<summary>(string) User's role ids, comma separated.  Use is deprecated because not all providers support this.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string Roles = string.Empty;
        ///<summary>(string) User's role names, comma separated.</summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string RoleNames = string.Empty;
        /// <summary>(boolean) True following authentication attempt if password is expired.</summary>
        [XmlAttribute]
        [DefaultValue(false)]
        public bool IsPasswordExpired = false;
        /// <summary>(boolean) True following authentication attempt if account is locked-out.</summary>
        [XmlAttribute]
        [DefaultValue(false)]
        public bool IsLockedOut = false;
        /// <summary>(boolean) True following authentication attempt if authentication was successful.</summary>
        [XmlAttribute]
        [DefaultValue(false)]
        public bool Authenticated = false;
        //MAIG - CH2 - Begin - Added the below fields to update the AgencyID and AgencyName
        /// <summary>
        /// (Long) Agency ID detail will be stored for the User 
        /// </summary>
        [XmlAttribute]
        [DefaultValue(0)]
        public long AgencyID = 0;
        /// <summary>
        /// (string) Agency Name detail will be stored for the User
        /// </summary>
        [XmlAttribute]
        [DefaultValue("")]
        public string AgencyName = string.Empty;
        //MAIG - CH2 - End - Added the below fields to update the AgencyID and AgencyName
        #endregion

        ///<summary>Validator delegate to validate userid</summary>
        protected void ValidateUserId(ValidatorAttribute Source, ValidatingSerializerEventArgs e)
        {
            //CHG0078293 - PT Enhancement - Modified the below condition to check the minimum length of user ID from 5 to 4 digits
            if (UserId.Length < 4) e.Error = new ErrorInfo(Source, e.Field);
        }

        /// <summary>
        /// Sets the forms authentication cookie to this user.
        /// </summary>
        public void SetAuthentication(Page Page)
        {
            UserIdentityManager.SetAuthentication(this, Page);
        }

        // SSO Integration - CH1 - START - Added the below code to retrieve the SSO Response ,decrypt and return the Status back to the login.aspx.cs page
        /// <summary>
        /// Function that that will return the SSO Request Status to login page
        /// </summary>
        /// <param name="samlResponse"></param>
        /// <returns>samlStatusCode and UserName</returns>
        public string ProcessSSOResponse(string samlResponse)
        {
            string encodedSamlResult = null;
            string samlStatusCode = null;
            string samlReturnNodes = null;
            string samlUserName = null;

            //The decrypted token is loded into the xml document to get the value from the nodes.
            XmlDocument SamlXML = new XmlDocument();


            if (samlResponse != string.Empty || samlResponse != null)
            {
                try
                {
                    byte[] encodedSamlResponse = System.Convert.FromBase64String(samlResponse);
                    encodedSamlResult = System.Text.Encoding.UTF8.GetString(encodedSamlResponse);
                    SamlXML.PreserveWhitespace = true;
                    SamlXML.LoadXml(encodedSamlResult);
                    //SAML name space is added to xml name space manager to read the nodes from the XML
                    XmlNamespaceManager ssoResponseNamespaceManager = new XmlNamespaceManager(SamlXML.NameTable);
                    ssoResponseNamespaceManager.AddNamespace("ssoProtocolResponse", Config.Setting("SSO_PROTOCOL"));
                    ssoResponseNamespaceManager.AddNamespace("ssoAssertionResponse", Config.Setting("SSO_ASSERTION"));


                    //The status is obtained from the SAML XML
                    XmlNode samlResponseNodes = SamlXML.SelectSingleNode(Config.Setting("SSO_STATUS_NODE_RESPONSE"), ssoResponseNamespaceManager);

                    if (samlResponseNodes != null)
                    {
                        samlStatusCode = samlResponseNodes.Value;
                    }

                    samlResponseNodes = SamlXML.SelectSingleNode(Config.Setting("SSO_USERNAME_NODE_RESPONSE"), ssoResponseNamespaceManager);
                    if (samlResponseNodes != null)
                    {
                        samlUserName = samlResponseNodes.InnerText;

                    }

                    // CHG0116140 - Begin Code changes for Reply attack SSO SAML token validation for NotBefore and NotOnOrAfter nodes 

                    string replyAttackCheck = Config.Setting("ReplyAttackCheck");
                    if (replyAttackCheck.ToLower().Equals("true"))
                    {
                        //CHG0118686  - Begin - Added Logs to validate Reply attack
                        Logger.Log("Replay attack check");
                        //CHG0118686  - End - Added Logs to validate Reply attack
                        XmlNamespaceManager xMan = new XmlNamespaceManager(SamlXML.NameTable);
                        xMan.AddNamespace("samlp", "urn:oasis:names:tc:SAML:2.0:protocol");
                        xMan.AddNamespace("saml", "urn:oasis:names:tc:SAML:2.0:assertion");
                        XmlNode xNode = SamlXML.SelectSingleNode("/samlp:Response/saml:Assertion/saml:Conditions", xMan);
                        string NotBefore = string.Empty;
                        string NotOnOrAfter = string.Empty;
                        if (xNode != null)
                        {
                            NotBefore = xNode.Attributes["NotBefore"].Value;
                            NotOnOrAfter = xNode.Attributes["NotOnOrAfter"].Value;

                            string format = "MM/dd/yyyy HH:mm:ss";
                            string ssoNotBefore = Convert.ToDateTime(NotBefore).ToString(format);
                            string ssoNotOnOrAfter = Convert.ToDateTime(NotOnOrAfter).ToString(format);
                            string datetimenow = DateTime.Now.ToString(format);
                            //CHG0118686  - Begin - Added Logs to validate Reply attack
                            Logger.Log("NotBefore" + Convert.ToDateTime(ssoNotBefore).ToString());
                            Logger.Log("NotOnOrAfter" + Convert.ToDateTime(ssoNotOnOrAfter).ToString());
                            Logger.Log("CurrentDate" + Convert.ToDateTime(datetimenow).ToString());
                            //CHG0118686  - End - Added Logs to validate Reply attack
                            if (!(Convert.ToDateTime(datetimenow) >= Convert.ToDateTime(ssoNotBefore) && Convert.ToDateTime(datetimenow) <= Convert.ToDateTime(ssoNotOnOrAfter)))
                            {
                                samlStatusCode = CSAAWeb.Constants.SAML_FAIL_RESPONSE;
                                //CHG0118686  - Begin - Added Logs to validate Reply attack
                                Logger.Log("NotBefore" + NotBefore);
                                Logger.Log("Replay Attack");
                                //CHG0118686  - End - Added Logs to validate Reply attack
                            }
                        }
                    }
                    // CHG0116140 End of code changes for Reply attack


                    samlReturnNodes = samlStatusCode.ToString() + "-" + samlUserName.ToString();
                }
                catch (Exception ex)
                {

                    Logger.Log(ex);

                }
            }
            //SAML Validation - CH1 - BEGIN - Added the below code to validate the SAML repsonse using check signature method.
            if (Config.Setting("SAMLValidation.Enable") == "1")
            {

                if (isValidSignature(SamlXML))
                {

                    return samlReturnNodes;
                }
                else
                {
                    //Failure response
                    Logger.Log(CSAAWeb.Constants.MSG_INVALIDSAML_RESPONSE + " " + UserId);
                    return CSAAWeb.Constants.SAML_FAIL_RESPONSE;
                }
            }
            else
            {
                return samlReturnNodes;
            }
        }
        public static bool isValidSignature(XmlDocument xmlDocument)
        {
            // Create a new SignedXml object and pass the XML document class.
            SignedXml signedXml = new SignedXml(xmlDocument.DocumentElement);
            X509Certificate2 cert = new X509Certificate2();
            // Find the "Signature" node and create a new XmlNodeList object.
            xmlDocument.PreserveWhitespace = true;
            XmlNodeList nodeList = xmlDocument.GetElementsByTagName("Signature");
            // Handling Signature and ds:Signature temporarily
            if (nodeList.Count == 0)
            {
                nodeList = xmlDocument.GetElementsByTagName("ds:Signature");
            }
            // Load the signature node.
            signedXml.LoadXml((XmlElement)nodeList[0]);
            // Check the signature and return the result.
            X509Store store = new X509Store(StoreName.TrustedPeople, StoreLocation.LocalMachine);
            // Open the store.
            store.Open(OpenFlags.ReadOnly | OpenFlags.OpenExistingOnly);
            // Get the certs from the store.
            X509Certificate2Collection CertificateCollection = store.Certificates;
            string flag = "";
            foreach (X509Certificate2 c in CertificateCollection)
            {
                string sub = Westwind.Utilities.StringUtils.ExtractString(c.Subject, "CN=", ",", true, true);
                if (Config.Setting("Certificate.Logging") == "1")
                {
                    Logger.Log("The certificate present in the certificate store is : " + sub);
                    Logger.Log("The certificate subject name to be validated is : " + cert);
                }
                if (Config.Setting("SSOCertificate.Subject").IndexOf(sub) >= 0)
                {
                    cert = c;
                    flag = "true";
                    break;
                }
            }
            if (flag == "true")
            {
                return signedXml.CheckSignature(cert, false);
            }
            else
            {
                return false;
            }

        }
        //SAML Validation - CH1 - END - Added the below code to validate the SAML repsonse using check signature method.

    }
    //Added the below code to validate the SAML repsonse using check signature method.
    // SSO Integration - CH1 - END - Added the below code to retrieve the SSO Response ,decrypt and return the Status back to the login.aspx.cs page


    #endregion
    #region UserIdentityManager
    /// <summary>
    /// This class contains static methods for converting between UserInfo object and Identity objects.
    /// </summary>
    public class UserIdentityManager
    {
        /// <summary>Gets data from the page.</summary>
        /// <param name="User">UserInfo object to fill with data.</param>
        /// <param name="Page">The web page object.</param>
        public static void FromPage(UserInfo User, Page Page)
        {
            switch (Page.User.Identity.AuthenticationType)
            {
                case "Forms": FromFormsIdentity(User, (FormsIdentity)Page.User.Identity); break;
            }
        }
        /// <summary>Gets data from formsidentity</summary>
        /// <param name="U">UserInfo object to fill with data.</param>
        /// <param name="Identity">The identity object</param>
        private static void FromFormsIdentity(UserInfo U, FormsIdentity Identity)
        {
            string[] st = Identity.Ticket.UserData.Split(';');
            U.RoleNames = st[0];
            U.Token = st[1];
            //MAIG - CH3 - BEGIN - CHG0087639 - RepID and UserID Length Expansion - Changed Convert function from Int32 to Int64
            U.RepId = Convert.ToInt64(st[2]);
            //MAIG - CH3 - END - CHG0087639 - RepID and UserID Length Expansion - Changed Convert function from Int32 to Int64
            U.EmployeeId = st[3];
            U.DO = st[4];
            U.FinancialLocation = st[5];
            //MAIG - CH4 - BEGIN - Added the Agency ID as part of the userData
            U.AgencyID = Convert.ToInt64(st[6]);
            //MAIG - CH4 - END - Added the Agency ID as part of the userData
        }

        /// <summary>
        /// Sets the forms authentication cookie to this user.  Packs the needed data into the
        /// UserData property of the ticket.
        /// </summary>
        /// <param name="U">The userinfo object to get the data from.</param>
        /// <param name="Page">The page object to insert the cookie.</param>
        public static void SetAuthentication(UserInfo U, System.Web.UI.Page Page)
        {
            // Create a cookie with a generic auth ticket, so that we can use the
            // timeout value specified in web.config.  We must do it this way because
            // .NET doesn't provide a method to directly read the timeout value, and
            // doesn't provide a method to generate a cookie using this value that also
            // also contains userdata.
            System.Web.HttpCookie C = FormsAuthentication.GetAuthCookie(U.UserId, false, "/");
            // Get the ticket.
            FormsAuthenticationTicket T = FormsAuthentication.Decrypt(C.Value);
            // Build a new ticket containing our properties as userdata concatenated by ";"
            //MAIG - CH5 - BEGIN - Added the Agency ID as part of the userData
            string UserData = String.Join(";", new String[] { U.RoleNames.ToUpper(), U.Token, U.RepId.ToString(), U.EmployeeId, U.DO, U.FinancialLocation, U.AgencyID.ToString() });
            //MAIG - CH5 - END - Added the Agency ID as part of the userData
            T = new FormsAuthenticationTicket(
                T.Version, T.Name, T.IssueDate, T.Expiration, T.IsPersistent, UserData);
            // Set the cookie value to our new ticket, and add it to the cookies collection.
            C.Value = FormsAuthentication.Encrypt(T);
            Page.Response.Cookies.Add(C);
            //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES  - IP Address fix for external URL - Start
            if (Config.Setting("GetIP").Equals("1"))
            {
                string userInfoCookie = C.Value;
                //string ForwardedKey = HttpContext.Current.Request.ServerVariables[Convert.ToString(ConfigurationManager.AppSettings["HttpForwardedKey"])];
                string RemoteKey = HttpContext.Current.Request.ServerVariables[Convert.ToString(ConfigurationManager.AppSettings["GetServerIP"])];
                var ip = RemoteKey;
                if (ip != null)
                {
                    if (ip.Contains(","))
                        ip = ip.Split(',').First().Trim();
                }
                string userInfo = userInfoCookie + '&' + ip;

                string encryptUserInfo = CSAAWeb.Cryptor.Encrypt(userInfo, Convert.ToString(ConfigurationManager.AppSettings["CryptorKey"]));

                System.Web.HttpCookie ASP_MyCookie = new System.Web.HttpCookie("ASP.NET_Cookie");
                ASP_MyCookie.Value = encryptUserInfo;
                ASP_MyCookie.HttpOnly = true;
                Page.Response.Cookies.Add(ASP_MyCookie);
            }
            //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES  - IP Address fix for external URL - End
        }

    }

    #endregion
    #region APDSUserInfo
    /// <summary>
    /// A UserInfo type that contains methods for getting data from the native APDS authentication
    /// Database..
    /// </summary>
    public class APDSUserInfo : UserInfo
    {
        /// <summary>Default constructor</summary>
        public APDSUserInfo() : base() { }
        /// <summary>Constructor from another object</summary>
        public APDSUserInfo(Object O) : base(O) { }
        /// <summary>Xml constructor</summary>
        public APDSUserInfo(string Xml) : base(Xml) { }
        /// <summary>Copies data from a DataReader</summary>
        public void CopyFrom(SqlDataReader Reader)
        {
            base.CopyFrom(Reader);
            if (Reader.NextResult()) AppendData(Reader);
            Authenticated = true;

        }
        /// <summary>Copies data from a DataReader</summary>
        /// <param name="Reader">The open DataReader containing the data</param>
        /// <param name="PasswordLife">The number of days for password life.</param>
        public void CopyFrom(SqlDataReader Reader, int PasswordLife)
        {
            IsPasswordExpired = CheckExpired(Reader["DATE_PWD_UPDATED"].ToString(), PasswordLife);
            CopyFrom(Reader);
            Authenticated = !IsLockedOut;
            if (IsLockedOut)
            {
                CopyFrom(new UserInfo());
                IsLockedOut = true;
            }
        }

        /// <summary>
        /// Checks to see if the password has expired.
        /// </summary>
        /// <param name="dateLastUpdated">String containing the date password last updated.</param>
        /// <param name="lifeSpan">Number of days allowed before password must be updated.</param>
        /// <returns>True if the password has expired.</returns>
        private bool CheckExpired(string dateLastUpdated, int lifeSpan)
        {
            return ((dateLastUpdated.Length <= 0) ||
                (DateTime.Parse(dateLastUpdated).AddDays(lifeSpan) < DateTime.Now));
        }


        // RFC 185138 - AD Integration CH1 - Start - Added the below method to get the list of users from the LDAP server
        /// <summary>
        /// Function that that will attempt a logon based on the users credentials
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        private static DirectoryEntry GetDirectoryObject(string UserName, string Password)
        {
            try
            {
                DirectoryEntry oDE;

                oDE = new DirectoryEntry(Config.Setting("ADServer"), UserName, Password, AuthenticationTypes.Secure);

                return oDE;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return null;
            }
        }
        // RFC 185138 - AD Integration CH1 - End - Added the below method to get the list of users from the LDAP server

        // RFC 185138 - AD Integration CH2 - Start - Added the below method to validate the credential against the LDAP server
        /// <summary>
        /// Method which will perfrom query based on combination of username and password
        /// This is used with the login process to validate the user credentials and return the response of the LDAP server validation.
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="Password"></param>
        /// <returns></returns>
        public static string GetUser(string UserName, string Password)
        {

            try
            {
                DirectoryEntry de = GetDirectoryObject(UserName, Password);
                if (de != null)
                {
                    DirectorySearcher deSearch = new DirectorySearcher();
                    deSearch.SearchRoot = de;
                    deSearch.Filter = "(&(objectClass=user)(sAMAccountName=" + UserName + "))";
                    deSearch.SearchScope = SearchScope.Subtree;
                    SearchResult results = deSearch.FindOne();

                    if (results != null)
                    {
                        return CSAAWeb.Constants.AD_AUTH_SUCCESS;
                    }
                    else
                    {
                        return CSAAWeb.Constants.AD_AUTH_FAILURE;
                    }
                }
                else
                {
                    return CSAAWeb.Constants.APPLICATION_ERROR;
                }
            }
            catch (DirectoryServicesCOMException ex)
            {

                string ADAuthErrMsg;
                string ADMessage;
                ADMessage = ex.ExtendedErrorMessage;
                if (ADMessage.Contains("52e"))
                {
                    //RFC 185138 - AD Integration start : Defect 226 Made code changes to display different error message for the user whose user id does not exists in Payment tool data base by cognizant on 05/03/2012
                    AuthenticationClasses.WebService.Authentication auth = new AuthenticationClasses.WebService.Authentication();
                    SessionInfo s = new SessionInfo(UserName, "APDS");

                    UserInfo U = auth.GetContactInfo(UserName, 0, s);
                    if (string.IsNullOrEmpty(U.UserId))
                    {
                        ADAuthErrMsg = CSAAWeb.Constants.AD_ERR_NOTFOUND;

                    }
                    else
                    {
                        ADAuthErrMsg = CSAAWeb.Constants.AD_ERR_INVALID;
                    }
                    //RFC 185138 - AD Integration end: Defect 226 Made code changes to display different error message for the user whose user id does not exists in Payment tool data base by cognizant on 05/03/2012
                }
                else if (ADMessage.Contains("775"))
                {
                    ADAuthErrMsg = CSAAWeb.Constants.AD_ERR_LOCKED;
                }
                else if (ADMessage.Contains("701"))
                {
                    ADAuthErrMsg = CSAAWeb.Constants.AD_ERR_ACNTEXPIRED;
                }
                else if (ADMessage.Contains("533"))
                {
                    ADAuthErrMsg = CSAAWeb.Constants.AD_ERR_DISABLED;
                }
                else if (ADMessage.Contains("532"))
                {
                    ADAuthErrMsg = CSAAWeb.Constants.AD_ERR_PWDEXPIRED;
                }
                else if (ADMessage.Contains("525"))
                {
                    ADAuthErrMsg = CSAAWeb.Constants.AD_ERR_NOTFOUND;
                }
                else if (ADMessage.Contains("530"))
                {
                    ADAuthErrMsg = CSAAWeb.Constants.AD_ERR_NOTPERMITTEDATTHISTIME;
                }
                else if (ADMessage.Contains("531"))
                {
                    ADAuthErrMsg = CSAAWeb.Constants.AD_ERR_NOTPERMITTEDFROMTHISCOMP;
                }
                else if (ADMessage.Contains("773"))
                {
                    ADAuthErrMsg = CSAAWeb.Constants.AD_ERR_RESETPASSWORD;
                }
                else
                {
                    ADAuthErrMsg = CSAAWeb.Constants.AD_ERR_ADAUTH;
                }

                return ADAuthErrMsg;

            }


        }
        // RFC 185138 - AD Integration CH2 - End - Added the below method to validate the credential against the LDAP server
        /// <summary>
        /// Fills properties that must be obtained from recordsets following the original.  Namely,
        /// the Role Ids and Role names.
        /// </summary>
        /// <param name="Reader">The dataReader containing the data, already read for the primary dataset.</param>
        private void AppendData(SqlDataReader Reader)
        {
            StringBuilder Ids = new StringBuilder("[");
            StringBuilder Names = new StringBuilder("");
            while (Reader.Read())
            {
                if (Ids.Length > 1)
                {
                    Ids.Append(',');
                    Names.Append(',');
                }
                Ids.Append('[' + Reader.GetInt32(0).ToString() + ']');
                Names.Append(Reader.GetString(1));
            }
            Ids.Append("]");
            Roles = Ids.ToString();
            RoleNames = Names.ToString();
        }

    }

    #endregion
}
