/* 
 * History:
 *	Modified By Cognizant
 *	Code Cleanup Activity for CSR #3937
 * 	06/30/2005  -	Removed the Method CheckLockoutCounter. Since this 
 *					Method is no longer used in the application.
 *  Modified By COGNIZANT as part of CSR 4593 
 *  03/17/2006 CSR 4593.Ch1 - To display an error message if the Active users count exceeds
 *             the maximum limit for an application, while adding a new user and log the exceptions in the application log file
 *  03/17/2006 CSR 4593.Ch2 - To display an error message if the Active users count exceeds
 *             the maximum limit for an application, when trying to activate an user and log the exceptions in the application log file
 * 
 * STAR Retrofit II Changes: 
 * Modified as a part of CSR 4852
 * 1/30/2007 STAR Retrofit II.Ch1: 
 *           Created a Method GetDOdetails to retrieve the details of district office for the selected DO code.
 * 1/30/2007 STAR Retrofit II.Ch2 : 
 *			 Created a Method UpdateDO to add or update DOs profile.
 * 1/30/2007 STAR Retrofit II.Ch3 : 
 *			 Created a Method GetAllDOs to retrieve the list of HUB offices. 
 * 1/30/2007 STAR Retrofit II.Ch4 : 
 *           Created a Method GetListofDOs  to retrieve the details of district offices.
 * Modified as a part of CSR#5166
 * 1/30/2007 STAR Retrofit II.Ch5 : 
 *           New web method GetUsersByStatus is added for retrieving user details using the criteria such as Application, District Office, UserId, Status.
 * 
 * STAR Retrofit III Changes:
 * Modified as a part of CSR #5595
 * 04/02/2007 STAR Retrofit III.Ch1:
 *			To display an error message if Admin user try to update user's DO who has active transactions in his turn-in 
 * 
 * RBAC - Payment Tool Integration Changes:
 * Modified as part of RBAC system integration on 09/27/2007
 * RBAC.Ch1: Created a Method GetAllUserDetails() to retrieve the details of all users needed for RBAC system.
 * SSO-Integration.Ch1: Added new method named SSOAuthenticate() for Authenticate in Internal reference for SSO integration by Cognizant on 09/24/2010
 * SSO-Integration.Ch2: Modified the existing Authenticate method to handle the error messages returned by the stored procedure. Changes done by Cognizant on 09/30/2010.
 * SSO-Integration.Bug fix1: Added new condition to avoid encrypting empty string, by Cognizant on 11/11/2010
 * 67811A0  - PCI Remediation for Payment systems CH1: Added code to get value from the output parameter @Result. 
 * 67811A0  - PCI Remediation for Payment systems CH2: Added code to get value from the output parameter @Result.
 * RFC 185138 - AD Integration - CH1 -Commented the lockedout check and password parameter input to the SP in Authenticate  method
 * RFC 185138 - AD Integration - CH2  -Commented the lockedout check and password parameter input to the SP in SSOAuthenticate  method
 * RFC 185138 - AD Integration - CH3,CH4 - Commented the lockedout check and copy of passwordlife span parameter and added the new copyfrom method
 * RFC 185138 - AD Integration - CH5 - Commented copy of passwordlife span parameter and added the new copyfrom method
 //* * RFC 185138 - AD Integration - CH6 - Commented the below line to avoid password insertion in to the table CSAA_USERS on insert of newuser in UI
 */
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using CSAAWeb;
using System.Security.Cryptography;
using System.Text;
using CSAAWeb.Serializers;
using System.Text.RegularExpressions;
using System.Configuration;

//TODO: Need to add Token generating and validation code.
namespace AuthenticationClasses.WebService
{
    /// <summary>
    /// Web service provides authentication services.
    /// </summary>
    [WebService(Namespace = "http://csaa.com/webservices/")]
    public class Authentication : CSAAWeb.Web.SqlWebService
    {
        private static int RetryTime;
        private static int RetryMax;
        private static int SessionTimeout;
        private static int PasswordLifeSpan;
        private static string DefaultPassword;
        private static bool ResetPasswordExpired;
        private static bool NewUserPasswordExpired;

        ///<summary/>
        static Authentication()
        {
            RetryTime = Config.iSetting("AuthenticationRetryTime");
            RetryMax = Config.iSetting("AuthenticationRetryMax", 3);
            SessionTimeout = Config.iSetting("AuthenticationSessionTimeout", 30);
            PasswordLifeSpan = Config.iSetting("PasswordLifeSpan", 60);
            DefaultPassword = Config.Setting("UserDefaultPwd");
            ResetPasswordExpired = Config.bSetting("ResetPasswordExpired", true);
            NewUserPasswordExpired = Config.bSetting("NewUserPasswordExpired", true);
        }
        /// <summary>
        /// Returns a command object.
        /// </summary>
        private SqlCommand GetCommand(string CommandString, SessionInfo S)
        {
            SqlCommand Cmd = GetCommand(CommandString);
            S.CopyTo(Cmd);
            return Cmd;
        }

        /// <summary>
        /// Authenticates a user.
        /// </summary>
        [WebMethod(Description = "Authenticates a user")]
        public UserInfo Authenticate(string Password, SessionInfo S)
        {
            string applicationName = string.Empty;
            UserInfo userInfo = null;
            APDSUserInfo Result = new APDSUserInfo();
            //RFC 185138 - AD Integration - CH1 START - Commented the lockedout check and password parameter input to the SP in Authenticate  method
            //Result.IsLockedOut = CheckLockoutCounterNew(S);
            //// Changed by Cognizant to fix exception while returning lockout is null. Doing a typecast while returning
            //if (Result.IsLockedOut) return new UserInfo(Result);
            SqlCommand Cmd = GetCommand("AUTH_Authenticate", S);
            //Cmd.Parameters["@Password"].Value = MD5Hash(Password);
            //RFC 185138 - AD Integration - CH1 END - Commented the lockedout check and password parameter input to the SP in Authenticate  method
            Cmd.Parameters["@Timeout"].Value = SessionTimeout;
            SqlDataReader Reader = Cmd.ExecuteReader();

            //SSO-Integration.Ch2: Start of Code - Existing code has been commented and New code has been added to handle the error messages returned by the stored procedure

            //if (Reader.Read()) Result.CopyFrom(Reader, PasswordLifeSpan);
            //Reader.Close();
            ////if (Result.Authenticated) ResetLockoutCounter(S.CurrentUser);
            //return new UserInfo(Result);


            if (ConfigurationSettings.AppSettings["SSOIntegratedApplications"] != null && ConfigurationSettings.AppSettings["SSOIntegratedApplications"].ToString() != string.Empty)
            {
                applicationName = ConfigurationSettings.AppSettings["SSOIntegratedApplications"].ToString();
            }
            if (applicationName.IndexOf(S.AppName) >= 0)
            {
                string ErrorInformation = string.Empty;
                ErrorInfo errorInfo = null;

                if (Reader.Read())
                {
                    try
                    {
                        if (Reader.FieldCount > 1)
                        {
                            //RFC 185138 - AD Integration - CH3 Start - Commented the lockedout check and copy of passwordlife span parameter and added the new copyfrom method
                            //Result.CopyFrom(Reader, PasswordLifeSpan);
                            //if (Result.Authenticated) ResetLockoutCounter(S.CurrentUser);
                            Result.CopyFrom(Reader);
                            //RFC 185138 - AD Integration - CH3 END - Commented the lockedout check and copy of passwordlife span parameter and added the new copyfrom method
                            userInfo = new UserInfo(Result);
                        }
                        else
                        {
                            ErrorInformation = Reader["ERRORINFO"].ToString();
                            userInfo = new UserInfo(Result);
                            errorInfo = new ErrorInfo("", ErrorInformation.ToString(), "SalesX");
                            userInfo.AddError(errorInfo);
                        }
                        Reader.Close();
                    }
                    catch (Exception ex)
                    {
                        string logMessage = ex.Message;
                        CSAAWeb.AppLogger.Logger.Log(logMessage);
                    }
                }
            }
            else
            {
                //RFC 185138 - AD Integration - CH4 start - Commented the lockedout check and copy of passwordlife span parameter and added the new copyfrom method
                //if (Reader.Read()) Result.CopyFrom(Reader, PasswordLifeSpan);
                //if (Result.Authenticated) ResetLockoutCounter(S.CurrentUser);
                if (Reader.Read()) Result.CopyFrom(Reader);
                //RFC 185138 - AD Integration - CH4 END - Commented the lockedout check and copy of passwordlife span parameter and added the new copyfrom method
                Reader.Close();
                userInfo = new UserInfo(Result);
            }

            //return new UserInfo(Result);
            return userInfo;
            //SSO-Integration.Ch2: End of Code 
        }

        ///<summary>
        ///Authenticates a user.
        ///</summary>
        ///SSO-Integration.Ch1: Added new method for Authenticate in Internal reference for SSO integration by Cognizant on 09/24/2010
        [WebMethod(Description = "Authenticates a user for a SSO logon")]
        public UserInfo SSOAuthenticate(string Password, SessionInfo S, bool IsOpenTokenExists)
        {
            APDSUserInfo Result = new APDSUserInfo();
            //RFC 185138 - AD Integration - CH2 START- Commented the lockedout check and password parameter input to the SP in SSOAuthenticate  method
            //Result.IsLockedOut = CheckLockoutCounterNew(S);
            //// Changed by Cognizant to fix exception while returning lockout is null. Doing a typecast while returning
            //if (Result.IsLockedOut) return new UserInfo(Result);
            SqlCommand Cmd = GetCommand("AUTH_Authenticate", S);
            //SSO-Integration.Bug fix1: Added new checking for IsOpenTokenExists. If it is true, then assign null alue to @Password. This is to avoid encrypting the empty string
            //if (IsOpenTokenExists == true)
            //{
            //    Cmd.Parameters["@Password"].Value = null;
            //}
            //else
            //{
            //    Cmd.Parameters["@Password"].Value = MD5Hash(Password);
            //}
            //RFC 185138 - AD Integration - CH2 END -Commented the lockedout check and password parameter input to the SP in SSoAuthenticate  method
            Cmd.Parameters["@Timeout"].Value = SessionTimeout;

            if (IsOpenTokenExists == true)
            {
                Cmd.Parameters["@OpenTokenExists"].Value = 1;
            }
            else
            {
                Cmd.Parameters["@OpenTokenExists"].Value = 0;
            }
            SqlDataReader Reader = Cmd.ExecuteReader();
            string ErrorInformation = string.Empty;
            ErrorInfo errorInfo = null;
            UserInfo userInfo = null;
            if (Reader.Read())
            {
                try
                {
                    if (Reader.FieldCount > 1)
                    {
                        //RFC 185138 - AD Integration - CH5 START - Commented copy of passwordlife span parameter and added the new copyfrom method
                        //Result.CopyFrom(Reader, PasswordLifeSpan);
                        Result.CopyFrom(Reader);
                        //RFC 185138 - AD Integration - CH5 END - Commented copy of passwordlife span parameter and added the new copyfrom method
                        userInfo = new UserInfo(Result);
                    }
                    else
                    {
                        ErrorInformation = Reader["ERRORINFO"].ToString();
                        userInfo = new UserInfo(Result);
                        errorInfo = new ErrorInfo("", ErrorInformation.ToString(), "SalesX");
                        userInfo.AddError(errorInfo);
                    }
                    Reader.Close();
                }
                catch (Exception ex)
                {
                    string logMessage = ex.Message;
                    CSAAWeb.AppLogger.Logger.Log(logMessage);
                }
            }
            return userInfo;

        }

        /// <summary>Returns contact information for a user.</summary>
        /// <param name="UserId">The user for whom to get contact information.</param>
        /// <param name="UserRid">Record id of user for whom to get contact information.</param>
        /// <param name="S">Session information of calling user.</param>
        [WebMethod(Description = "Returns contact information for user.")]
        public UserInfo GetContactInfo(string UserId, int UserRid, SessionInfo S)
        {
            SqlCommand Cmd = GetCommand("AUTH_GetUserInfo", S);
            if (UserId != null) Cmd.Parameters["@UserId"].Value = UserId;
            if (UserId == null) Cmd.Parameters["@UserRid"].Value = UserRid;
            SqlDataReader Reader = Cmd.ExecuteReader();
            APDSUserInfo Result = new APDSUserInfo();
            if (Reader.Read()) Result.CopyFrom(Reader);
            Reader.Close();
            return new UserInfo(Result);
        }

        /// <summary>Returns a dataset of all the users.</summary>
        //STAR Retrofit II.Ch5 - START New web method is added for retrieving user details using the criteria such as Application Name, District Office, UserId, Status.
        [WebMethod(Description = "Returns a dataset of all the users.")]
        public DataSet GetUsersByStatus(SessionInfo S, bool ShowAll, string DO, string Userid, int Status)
        {
            SqlCommand Cmd = GetCommand("AUTH_GetUserInfo", S);
            if (Userid != "") Cmd.Parameters["@UserId"].Value = Userid;
            Cmd.Parameters["@ShowAll"].Value = ShowAll;
            if (DO != "") Cmd.Parameters["@DO"].Value = DO;
            if (Status <= 1) Cmd.Parameters["@Status"].Value = Status;
            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            DS.Relations.Add("Roles", DS.Tables[0].Columns["UserRid"], DS.Tables[1].Columns["UserRid"]);
            return DS;
        }
        //STAR Retrofit II.Ch5 - END

        /// <summary>Returns a dataset of all the users.</summary>
        [WebMethod(Description = "Returns a dataset of all the users.")]
        public DataSet GetUsers(SessionInfo S, bool ShowAll, string DO)
        {
            SqlCommand Cmd = GetCommand("AUTH_GetUserInfo", S);
            Cmd.Parameters["@ShowAll"].Value = ShowAll;
            if (DO != "") Cmd.Parameters["@DO"].Value = DO;
            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            DS.Relations.Add("Roles", DS.Tables[0].Columns["UserRid"], DS.Tables[1].Columns["UserRid"]);
            return DS;
        }

        /// <summary>Returns a dataset of all the possible roles.</summary>
        [WebMethod(Description = "Returns a dataset of all the possible roles.")]
        public DataSet GetRoles(SessionInfo S, string ForApp)
        {
            SqlCommand Cmd = GetCommand("AUTH_GetRoles", S);
            Cmd.Parameters["@ForAppName"].Value = ForApp;
            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            return DS;
        }

        //STAR Retrofit II.Ch4 - START Created a Method to retrieve the details of district offices.
        /// <summary>Returns a dataset of all the possible district offices.</summary>
        [WebMethod(Description = "Returns a dataset of all the possible roles.")]
        public DataSet GetListofDOs(SessionInfo S, string DOID)
        {
            SqlCommand Cmd = GetCommand("AUTH_GetDOInfo", S);
            Cmd.Parameters["@DOID"].Value = DOID;
            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            return DS;
        }
        //STAR Retrofit II.Ch4 - END

        //STAR Retrofit II.Ch1 - START Method created to retrieve the details of district office for the selected DO code.
        /// <summary>Returns a dataset of district office details.</summary>
        [WebMethod(Description = "Returns a dataset of all the possible district offices.")]
        public DataSet GetDOdetails(string DOID)
        {
            SqlCommand Cmd = GetCommand("AUTH_GetDOdetails");
            Cmd.Parameters.Add("@DOID", DOID);
            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            return DS;
        }
        //STAR Retrofit II.Ch1 - END

        //RBAC.Ch1 - START Created a Method to retrieve the details of all users needed for RBAC system.
        /// <summary>Returns a dataset of all users details.</summary>
        [WebMethod(Description = "Returns a dataset of all users details.")]
        public DataSet GetAllUserDetails()
        {
            SqlCommand Cmd = GetCommand("AUTH_GetAllUserDetails");
            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            return DS;
        }
        //RBAC.Ch1 - END

        /// <summary>Returns a dataset of all the possible report roles.</summary>
        [WebMethod(Description = "Returns a dataset of all the possible report roles.")]
        public DataSet GetAppRoles(SessionInfo S, int AppId)
        {
            SqlCommand Cmd = GetCommand("AUTH_GetAppRoles", S);
            Cmd.Parameters["@AppId"].Value = AppId;
            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            return DS;
        }

        /// <summary>Returns a dataset of applications.</summary>
        [WebMethod(Description = "Returns a dataset of applications.")]
        public DataSet GetApplications(SessionInfo S)
        {
            SqlCommand Cmd = GetCommand("AUTH_GetApplications", S);
            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            return DS;
        }

        /// <summary>Gets a list of the applications.</summary>
        [WebMethod(Description = "Gets a list of the applications.")]
        public DataSet GetApps(SessionInfo S)
        {
            SqlCommand Cmd = GetCommand("AUTH_GetApps");
            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            return DS;
        }

        /// <summary>Deletes user UserRid.</summary>
        [WebMethod(Description = "Deletes user UserRid.")]
        public string DeleteUser(int UserRid, SessionInfo S)
        {
            SqlCommand Cmd = GetCommand("AUTH_DeleteUser", S);
            Cmd.Parameters["@UserRid"].Value = UserRid;
            try
            {
                Cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {

                //Modified by Cognizant on 12/03/2005 
                //To handle if Admin user try to delete him/her self
                if (e.Class == 15 || e.Class == 16) return e.Message;
                throw;
            }
            return "";
        }

        /// <summary>Resets UserRid's password.</summary>
        [WebMethod(Description = "Resets UserRid's password.")]
        public string ResetPassword(string UserId, SessionInfo S)
        {
            //SetAccountLockout(UserId, false, S);
            SqlCommand Cmd = GetCommand("AUTH_UpdatePassword", S);
            Cmd.Parameters["@UserId"].Value = UserId;
            string newPwd = AddPassword(Cmd, ResetPasswordExpired, "");
            // 67811A0  - PCI Remediation for Payment systems CH1:START -Added code to get value from the output parameter @Result. 
            Cmd.ExecuteNonQuery();
            Cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
            // if (Cmd.ExecuteNonQuery()>0) {
            if ((int)Cmd.Parameters["@Result"].Value == 0)
            {
                return "User " + UserId + "'s password has been reset to '" + newPwd + "'";
            }
            else
            {
                return "ERROR: Could not reset " + UserId + "'s password.";
            }
            // 67811A0  - PCI Remediation for Payment systems CH1:END
        }
        /// <summary>Adds or updates user's profile.</summary>
        [WebMethod(Description = "Adds or updates user's profile.")]
        public ArrayOfErrorInfo UpdateUser(UserInfo User, SessionInfo S)
        {
            User.Validate();
            //START Changed by Cognizant on 22/6/2004 for Displaying an Error message for Duplicate UserID
            ArrayOfErrorInfo Result = new ArrayOfErrorInfo();
            //END
            if (User.Errors != null && User.Errors.Count > 0) return User.Errors;
            SqlCommand Cmd = GetCommand("AUTH_UpdateUser", S);
            User.CopyTo(Cmd);
            //* * RFC 185138 - AD Integration - CH6 - Commented the below line to avoid password insertion in to the table CSAA_USERS on insert of newuser in UI
            //if (User.UserRid == 0) AddPassword(Cmd, NewUserPasswordExpired, "");
            try
            {
                Cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (e.Class == 15)
                {
                    if (e.Message.IndexOf("exists") > 0)
                    {
                        //START Code Modified by Cognizant on 22/6/2004 for Displaying an Error message for Duplicate UserID
                        Result.Add(new ErrorInfo("USER", e.Message, "User.UserId"));
                        //User.Errors.Add(new ErrorInfo("USER", e.Message, "User.UserId"));
                        //END
                    }
                    else
                    {
                        //START Code Modified by Cognizant on 22/6/2004 for Displaying an Error message for Duplicate UserID
                        Result.Add(new ErrorInfo("USER", e.Message, "User.Roles"));
                        //User.Errors.Add(new ErrorInfo("USER", e.Message, "User.Roles"));
                        //END
                    }
                    return Result;
                }
                //CSR 4593.Ch1-START:To display an error message if the Active users count exceeds the maximum limit for an application, while adding a new user and log the exceptions in the application log file
                if (e.Class == 13)
                {
                    string message = e.Message;
                    string logMessage = message.Replace("User", "UserId(" + User.UserId + ")");
                    CSAAWeb.AppLogger.Logger.Log(logMessage);
                    Result.Add(new ErrorInfo("", e.Message, ""));
                    return Result;
                }
                //CSR 4593.Ch1-END
                //CSR 4593.Ch2-START  To display an error message if the Active users count exceeds the maximum limit for an application, when trying to activate an user and log the exceptions in the application log file
                if (e.Class == 14)
                {
                    string message = e.Message;
                    string logMessage = message.Replace("User", "UserId(" + User.UserId + ")");
                    CSAAWeb.AppLogger.Logger.Log(logMessage);
                    Result.Add(new ErrorInfo("", e.Message, ""));
                    return Result;
                }
                //CSR 4593.Ch2-END

                //START Code Modified by Cognizant on 12/03/2005 
                //for displaying an error if Admin user try to remove him/her own Administrator role
                if (e.Class == 16)
                {
                    Result.Add(new ErrorInfo("USER", e.Message, ""));
                    return Result;
                }
                //END

                //STAR Retrofit III.Ch1 - START To display an error message if Admin user try to update user's DO who has active transactions in his turn-in
                if (e.Class == 11)
                {
                    Result.Add(new ErrorInfo("", e.Message, ""));
                    return Result;
                }
                //STAR Retrofit III.Ch1 - END
                throw;
            }
            return null;
        }

        //STAR Retrofit II.Ch2 - START Method created to add or update DOs profile.
        /// <summary>Adds or updates DO's profile.</summary>
        [WebMethod(Description = "Adds or updates DO's profile.")]
        public ArrayOfErrorInfo UpdateDO(int DORid, string DOName, string DOnum, string HUB, bool Active, string CurrentUser)
        {

            ArrayOfErrorInfo Result = new ArrayOfErrorInfo();
            SqlCommand Cmd = GetCommand("AUTH_UpdateDO");
            Cmd.Parameters.Add("@DORid", DORid);
            Cmd.Parameters.Add("@DOName", DOName);
            Cmd.Parameters.Add("@DOID", DOnum);
            Cmd.Parameters.Add("@HUB", HUB);
            Cmd.Parameters.Add("@Active", Active);
            Cmd.Parameters.Add("@CurrentUser", CurrentUser);
            try
            {
                Cmd.ExecuteNonQuery();
            }
            catch (SqlException e)
            {
                if (e.Class == 11 || e.Class == 12 || e.Class == 13 || e.Class == 14 || e.Class == 15 || e.Class == 16)
                {
                    Result.Add(new ErrorInfo("", e.Message, ""));
                    return Result;
                }
                throw;
            }
            return null;
        }
        //STAR Retrofit II.Ch2 - END

        /// <summary>
        /// Adds the new password related parameters to Cmd
        /// </summary>
        /// <param name="Cmd">The command object to which to add the parameters.</param>
        /// <param name="BackDate">true if the update date should be backdated</param>
        /// <param name="Password">The password to use.</param>
        /// <returns>The unencypted password.</returns>
        private string AddPassword(SqlCommand Cmd, bool BackDate, string Password)
        {
            if (Password == "") Password = DefaultPassword;
            DateTime UpdateDate = DateTime.Now;
            if (BackDate) UpdateDate = UpdateDate.AddDays(-1 * PasswordLifeSpan);
            Cmd.Parameters["@NewPassword"].Value = MD5Hash(Password);
            Cmd.Parameters["@UpdateDate"].Value = UpdateDate;
            return Password;
        }

        /// <summary>
        /// Sets the account lockout flag.
        /// </summary>
        [WebMethod(Description = "Sets account lockout flag.")]
        public void SetAccountLockout(string UserId, bool Lock, SessionInfo S)
        {
            if (!Lock) ResetLockoutCounter(UserId);
            SqlCommand Cmd = GetCommand("AUTH_SetLockoutStatus", S);
            Cmd.Parameters["@UserId"].Value = UserId;
            Cmd.Parameters["@Lock"].Value = Lock;
            Cmd.ExecuteNonQuery();
        }

        /// <summary>Gets a list of the district offices.</summary>
        [WebMethod(Description = "Gets a list of the district offices.")]
        public DataSet GetDOs(SessionInfo S)
        {
            SqlCommand Cmd = GetCommand("AUTH_GetDOs");
            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            return DS;
        }

        //STAR Retrofit II.Ch3 - START Created a Method to retrieve the list of HUB offices.
        /// <summary>Gets a list of the all district offices.</summary>
        [WebMethod(Description = "Gets a list of the district offices.")]
        public DataSet GetAllDOs()
        {
            SqlCommand Cmd = GetCommand("AUTH_GetALLDOs");
            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            return DS;
        }
        //STAR Retrofit II.Ch3 - END

        /// <summary>Lists users by role</summary>
        [WebMethod(Description = "Lists users by role.")]
        public DataSet ListUsersByRoles(string RequesterNm, int RoleId, string RepDO, SessionInfo S)
        {
            SqlCommand Cmd = GetCommand("AUTH_ListAppUsersByRoles", S);

            Cmd.Parameters["@RequesterNm"].Value = RequesterNm;
            Cmd.Parameters["@RoleId"].Value = RoleId;
            Cmd.Parameters["@RepDO"].Value = RepDO;

            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            return DS;
        }

        /// <summary>Lists application users by role Application.</summary>
        [WebMethod(Description = "Lists application users by role.")]
        public DataSet ListAppUsersByRoles(string RequesterNm, int RoleId, string RepDO, int AppId, SessionInfo S)
        {
            SqlCommand Cmd = GetCommand("AUTH_ListAppUsersByRoles", S);

            Cmd.Parameters["@RequesterNm"].Value = RequesterNm;
            Cmd.Parameters["@RoleId"].Value = RoleId;
            Cmd.Parameters["@RepDO"].Value = RepDO;
            Cmd.Parameters["@AppId"].Value = AppId;

            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            return DS;
        }

        /// <summary>
        /// Sets new password for UserId
        /// </summary>
        [WebMethod(Description = "Sets new password for UserId")]
        public bool UpdatePassword(string UserId, string Password, SessionInfo S, out ArrayOfErrorInfo Errors)
        {
            //bool IsValidPassword = EnforcePwdRules(Password);
            Errors = EnforcePwdRules(UserId, Password);
            if (Errors != null) return false;
            SqlCommand Cmd = GetCommand("AUTH_UpdatePassword", S);
            Cmd.Parameters["@UserId"].Value = UserId;
            Cmd.Parameters["@NewPassword"].Value = MD5Hash(Password);
            // 67811A0  - PCI Remediation for Payment systems CH2:START -Added code to get value from the output parameter @Result. 
            //return (Cmd.ExecuteNonQuery()>0); 
            Cmd.ExecuteNonQuery();
            //if (Cmd.ExecuteNonQuery()>0) Result.Add(new ErrorInfo("VALID_PASSWORD", "Valid Password.",""));
            //else  Result.Add(new ErrorInfo("INVALID_PASSWORD", "Inalid Password.",""));
            //return Result;
            Cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
            if ((int)Cmd.Parameters["@Result"].Value == 0)
            {
                return true;
            }
            else
            {
                Errors = new ArrayOfErrorInfo();
                Errors.Add(new ErrorInfo("INVALID_PASSWORD", "The New Password and the Current Password cannot be the same.Please use a valid Password", ""));
                return false;
            }
            //67811A0  - PCI Remediation for Payment systems CH2:END
        }

        /// <summary>
        /// Confirms that the token is valid and that the user/app has permission on the method.
        /// </summary>
        [WebMethod(Description = "Confirms that the token is valid and that the user/app has permission on the method.")]
        public ArrayOfErrorInfo ValidateSession(SessionInfo S, string Method)
        {
            ArrayOfErrorInfo Result = null;
            SqlCommand Cmd = GetCommand("AUTH_VerifyToken", S);
            Cmd.Parameters["@Timeout"].Value = SessionTimeout;
            Cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
            int i;
            try
            {
                Cmd.ExecuteNonQuery();
                i = (int)Cmd.Parameters["@Result"].Value;
            }
            catch (Exception e)
            {
                if (e.Message == "Syntax error converting from a character string to uniqueidentifier.")
                    i = 2;
                else throw;
            }
            if (i > 0) Result = new ArrayOfErrorInfo();
            if (i == 1)
                Result.Add(new ErrorInfo("TIMEOUT", "Session has timed out.", ""));
            else if (i == 2)
                Result.Add(new ErrorInfo("INVALID TOKEN", "Invalid token.", ""));
            if (Result != null && Result.Count > 0)
                CSAAWeb.AppLogger.Logger.LogToFile("SoapLog", "Token: " + Result[0].Message + " " + S.ToString());
            return Result;
        }

        /// <summary>
        /// Determinines if the account has tried to log in more than the allowed number of
        /// times, and locks it out if so. Returns true if locked out.
        /// </summary>
        private bool CheckLockoutCounterNew(SessionInfo S)
        {
            SqlCommand Cmd = GetCommand("AUTH_CheckLockoutCounter", S);
            Cmd.Parameters["@Result"].Direction = ParameterDirection.Output;
            int i;
            try
            {
                Cmd.ExecuteNonQuery();
                i = (int)Cmd.Parameters["@Result"].Value;
                if (i > 0)
                {
                    CSAAWeb.AppLogger.Logger.Log("Account " + S.CurrentUser + " locked out.");
                    return true;
                }
                else return false;
            }
            catch (Exception e)
            {
                CSAAWeb.AppLogger.Logger.Log(e.Message);
                return false;
            }

        }


        /// <summary/>
        private void ResetLockoutCounter(string username)
        {
            Context.Cache.Remove("Authentication.Service.lockoutcount$" + username);
        }

        /// <summary>
        /// Used to encrypt passwords.
        /// </summary>
        private string MD5Hash(string str)
        {
            return Convert.ToBase64String(new MD5CryptoServiceProvider().ComputeHash(ASCIIEncoding.ASCII.GetBytes(str)));
        }


        ///Changed by Cognizant on 05/18/04 
        /// <summary>
        /// Gets Rep ID and DO for the current user
        /// </summary>
        /// <param name="CurrentUser">Currently logged in User</param>
        /// <returns>Dataset of RepID and DO for the current user</returns>
        [WebMethod(Description = "Returns recordset for Rep ID and DO depending on the CurrentUser.", BufferResponse = true)]
        public DataSet GetRepIDDO(string CurrentUser)
        {
            SqlCommand Cmd = GetCommand("AUTH_GetRepIDDO");
            Cmd.Parameters.Add("@CurrentUser", CurrentUser);
            SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds);
            return ds;
        }

        #region GetDOsByApp

        //Code Added by Cognizant on 09/07/2004
        /// <summary>Gets a list of the district offices by (Application Not Now)</summary>
        [WebMethod(Description = "Gets a list of the district offices.")]
        public DataSet GetDOsByApp()
        {
            SqlCommand Cmd = GetCommand("AUTH_GetDOsByApp");
            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            return DS;
        }

        #endregion

        #region ListUsersByRolesByApp
        //Code Added By Cognizant on 07/09/2004
        /// <summary>Lists users by role(Created for Later Purpose)</summary>
        [WebMethod(Description = "Lists users by role and Application Id")]
        public DataSet ListUsersByRolesByApp(string RepDO, int AppId)
        {
            SqlCommand Cmd = GetCommand("AUTH_ListAppUsersByRolesApp");
            Cmd.Parameters.Add("@RepDO", RepDO);
            Cmd.Parameters.Add("@AppId", AppId);
            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            return DS;
        }

        #endregion

        #region ListApprovers
        //Code Added By Cognizant on 04/18/2005 as part of Cashier Recon Enhancements
        /// <summary>Lists all Approvers by RepDo</summary>
        [WebMethod(Description = "Lists all Approvers by RepDo")]
        public DataSet ListApprovers(string RepDO)
        {
            SqlCommand Cmd = GetCommand("AUTH_ListApprovers");
            Cmd.Parameters.Add("@RepDO", RepDO);
            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            return DS;
        }
        #endregion


        /// <summary>
        /// Used to enforce password rules.
        /// </summary>
        private ArrayOfErrorInfo EnforcePwdRules(string UserId, string Password)
        {
            ArrayOfErrorInfo Result = null;
            Result = new ArrayOfErrorInfo();

            if (Password.Length < 8 || Password.Length > 14)
            {
                Result.Add(new ErrorInfo("INVALID_PWD_LENGTH", "Password length should be between 8 and 14 characters.", ""));
                return Result;
            }

            string Pattern = "CSAA|" + UserId;
            Regex rgReserved = new Regex(Pattern, RegexOptions.IgnoreCase);
            bool Reserved = rgReserved.Match(Password).Success;
            if (Reserved == true)
            {
                Result.Add(new ErrorInfo("RESERVED_WORDS", "Password should not contain any reserved words or UserId.", ""));
                return Result;
            }

            Regex rgNonAlpha = new Regex("[^a-zA-Z]", RegexOptions.Compiled);
            bool NonAlpha = rgNonAlpha.Match(Password).Success;
            Regex rgAlpha = new Regex("[^a-zA-Z]", RegexOptions.Compiled);
            bool Alpha = rgAlpha.Match(Password).Success;
            Regex rgNonCaps = new Regex("[a-z]", RegexOptions.Compiled);
            bool NonCaps = rgNonCaps.Match(Password).Success;
            Regex rgCaps = new Regex("[A-Z]", RegexOptions.Compiled);
            bool Caps = rgCaps.Match(Password).Success;
            if ((NonAlpha == true && Alpha == true) || (Caps == true && NonCaps == true))
                return null;
            else
            {
                Result.Add(new ErrorInfo("VIOLATES_RULES", "Password must be a mix of upper/lower case OR mix of alpha/numeric OR mix of alpha/special chararcters.", ""));
                return Result;
            }
            //return null;
        }

    }
}
