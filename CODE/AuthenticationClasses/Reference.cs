/*
 * HISTORY:
 * 
 * STAR Retrofit II Changes: 
 * Modified as a part of CSR 4852
 * 1/31/2007 STAR Retrofit II.Ch1: 
 *           Created a reference GetDOdetails to retrieve the details of district office for the selected DO code.
 * 1/31/2007 STAR Retrofit II.Ch2 : 
 *			 Created a reference UpdateDO to add or update DOs profile.
 * 1/31/2007 STAR Retrofit II.Ch3 : 
 *			 Created a Soap reference GetDOdetails to retrieve the details of district office for the selected DO code.
 * 1/31/2007 STAR Retrofit II.Ch4 : 
 *           Created a Soap reference UpdateDO to add or update DOs profile.
 * 1/31/2007 STAR Retrofit II.Ch5 : 
 *           Created a reference GetAllDOs to retrieve the list of HUB offices.
 * 1/31/2007 STAR Retrofit II.Ch6 : 
 *           Created a reference GetListofDOs  to retrieve the details of district offices.
 * 1/31/2007 STAR Retrofit II.Ch7 : 
 *           Created a Soap reference GetAllDOs to retrieve the list of HUB offices.
 * 1/31/2007 STAR Retrofit II.Ch8 : 
 *           Created a Soap reference GetListofDOs  to retrieve the details of district offices.
 * Modified as a part of CSR#5166
 * 1/31/2007 STAR Retrofit II.Ch9 : 
 *           Added a new reference GetUsersByStatus to retrieve user details.
 * 1/31/2007 STAR Retrofit II.Ch10 :
 *           Added a new Soap reference GetUsersByStatus to retrieve user details.
 * 
 * RBAC - Payment Tool Integration Changes:
 * Modified as part of RBAC system integration on 09/27/2007
 * RBAC.Ch1: Created a Reference GetAllUserDetails() to retrieve the details of all users needed for RBAC system.
 * RBAC.Ch2: Created a SOAP Reference GetAllUserDetails() to retrieve the details of all users needed for RBAC system.
 * SSO-Integration.Ch1: Added new method for Authenticate  in Authenticate Class for SSO integration by Cognizant on 09/24/2010
 * SSO-Integration.Ch2: Added new method for Authenticate in Internal reference for SSO integration by Cognizant on 09/24/2010
 */
using System;
using System.Web.Services.Protocols;
using System.Data;
using CSAAWeb;
using CSAAWeb.Serializers;

namespace AuthenticationClasses.Service {
    
	/// <summary>Authentication web service connection.</summary>
	public class Authentication  {
		private SessionInfo S=null;
		private static string AppName;
		private string CurrentUser=string.Empty;
		private AuthenticationClasses.InternalService.Authentication Auth;
		
		///<summary/>
		static Authentication() {
			AppName = Config.Setting("AppName");
		}
		///<summary/>
		public Authentication() : base() {
			S = new SessionInfo("", AppName);
			Auth = new AuthenticationClasses.InternalService.Authentication();
		}

		///<summary>Constructor with current user name</summary>
		public Authentication(string CurrentUser) : base() {
			Auth = new AuthenticationClasses.InternalService.Authentication();
			S = new SessionInfo(CurrentUser, AppName);
		}

		///<summary>Constructor with current user name and application name</summary>
		public Authentication(SessionInfo S) {
			Auth = new AuthenticationClasses.InternalService.Authentication();
			this.S = S;
		}

		/// <summary>
		/// Closes any open references.
		/// </summary>
		public void Close() {
			Auth.Close();
		}
		
		/// <summary>Authenticates the user and returns user information</summary>
		public UserInfo Authenticate(string UserId, string Password) {
			S.CurrentUser=UserId;
			return Auth.Authenticate(Password, S);
		}

        //SSO-Integration.Ch1: Added new method for Authenticate  in Authenticate Class for SSO integration by Cognizant on 09/24/2010 
        /// <summary>Authenticates the user and returns user information</summary>
        public UserInfo SSOAuthenticate(string UserId, string Password, bool IsOpenTokenExist)
        {
            S.CurrentUser = UserId;
            return Auth.SSOAuthenticate(Password, S, IsOpenTokenExist);
        }

		/// <summary>Returns contact information for UserId</summary>
		public UserInfo GetContactInfo(string UserId) {
			return Auth.GetContactInfo(UserId, 0, S);
		}

		/// <summary>Returns contact information for UserId</summary>
		public UserInfo GetContactInfo(int UserRid) {
			return Auth.GetContactInfo(null, UserRid, S);
		}

		/// <summary>Returns all the users.</summary>
		///STAR Retrofit II.Ch9 - START Added a new reference which retrieve user details.
		public DataSet GetUsersByStatus(bool ShowAll, string DO, string Userid, int Status) 
		{
			return Auth.GetUsersByStatus(S, ShowAll, DO, Userid, Status);
		}
		///STAR Retrofit II.Ch9 - END

		/// <summary>Returns all the users.</summary>
		public DataSet GetUsers(bool ShowAll, string DO) 
		{
			return Auth.GetUsers(S, ShowAll, DO);
		}

		/// <summary>Returns a dataset of all the possible roles.</summary>
		public DataSet GetRoles(string ForApp) {
			return Auth.GetRoles(S, ForApp);
		}
		//STAR Retrofit II.Ch6 - START Created a reference to retrieve the details of district offices.
		/// <summary>Returns a dataset of all the user information.</summary>
		public DataSet GetListofDOs (string DOID) 
		{
			return Auth.GetListofDOs (S, DOID);
		}
		//STAR Retrofit II.Ch6 - END

		//STAR Retrofit II.Ch1 - START Created a reference to retrieve the details of district office for the selected DO code.
		/// <summary>Returns a dataset of district office details</summary>
		public DataSet GetDOdetails(string DOID) 
		{
			return Auth.GetDOdetails(DOID);
		}
		//STAR Retrofit II.Ch1 - END

		//RBAC.Ch1 - START Created a Reference to retrieve the details of all users needed for RBAC system.
		/// <summary>Returns a dataset of all users details.</summary>
		public DataSet GetAllUserDetails() 
		{
			return Auth.GetAllUserDetails();
		}
		//RBAC.Ch1 - END


		/// <summary>Returns a dataset of all the possible report roles.</summary>
		public DataSet GetAppRoles(int AppId) 
		{
			return Auth.GetAppRoles(S, AppId);
		}


		/// <summary>Returns a dataset of applications.</summary>
		public DataSet GetApplications() {
			return Auth.GetApplications(S);
		}

		/// <summary>Gets a list of the applications.</summary>
		public DataSet GetApps() 
		{
			return Auth.GetApps(S);
		}

		/// <summary>Deletes user UserRid.</summary>
		public string DeleteUser(int UserRid) {
			return Auth.DeleteUser(UserRid, S);
		}

		/// <summary>Resets UserRid's password.</summary>
		public string ResetPassword(string UserId) {
			return Auth.ResetPassword(UserId, S);
		}

		/// <summary>Adds or updates user's profile.</summary>
		public ArrayOfErrorInfo UpdateUser(UserInfo User) {
			return Auth.UpdateUser(User, S);
		}

		//STAR Retrofit II.Ch2 - START Created a reference to add or update DOs profile.
		/// <summary>Adds or updates DO's profile.</summary>
		public ArrayOfErrorInfo UpdateDO(int DORid,string DOName,string DOnum,string HUB,bool Active,string CurrentUser) 
		{
			return Auth.UpdateDO(DORid,DOName,DOnum,HUB,Active,CurrentUser);
		}
		//STAR Retrofit II.Ch2 - END

		/// <summary>Sets the accounts lockout status.</summary>
		public void SetAccountLockout(string UserId, bool Lock, SessionInfo S) 
		{
			Auth.SetAccountLockout(UserId, Lock, S);
		}

		/// <summary>Gets a list of the district offices.</summary>
		public DataSet GetDOs() {
			return Auth.GetDOs(S);
		}
		//STAR Retrofit II.Ch5 - START Created a reference to retrieve the list of HUB offices.
		/// <summary>Gets a list of the district offices.</summary>
		public DataSet GetAllDOs() 
		{
			return Auth.GetAllDOs();
		}
		//STAR Retrofit II.Ch5 - END

		/// <summary>Lists users by role</summary>
		public DataSet ListUsersByRoles(string RequesterNm, int RoleId, string RepDO) 
		{
			return Auth.ListUsersByRoles(RequesterNm, RoleId, RepDO, S);
		}

		/// <summary>Lists application users by role</summary>
		public DataSet ListAppUsersByRoles(string RequesterNm, int RoleId, string RepDO, int AppId) 
		{
			return Auth.ListAppUsersByRoles(RequesterNm, RoleId, RepDO, AppId, S);
		}

		/// <summary>
		/// Sets new password for UserId
		/// </summary>
		public bool UpdatePassword (string UserId, string Password, out ArrayOfErrorInfo Errors) {
			return Auth.UpdatePassword(UserId, Password, S, out Errors);
		}

		/// <summary>
		/// Confirms that the session is valid and that it is allowed to call method.
		/// </summary>
		/// <returns>Errors in session or null if none.</returns>
		public ArrayOfErrorInfo ValidateSession(string Method) {
			return Auth.ValidateSession(S, Method);
		}

		/// <summary>
		/// Confirms that the session is valid.
		/// </summary>
		/// <returns>Errors in session or null if none.</returns>
		public ArrayOfErrorInfo ValidateSession() {
			return Auth.ValidateSession(S, "");
		}
		///Changed by Cognizant on 05/18/04 
		/// <summary>
		/// Gets Rep ID and DO for the current user
		/// </summary>
		/// <param name="CurrentUser">Currently logged in User</param>
		/// <returns>Dataset of RepID and DO for the current user</returns>
		public System.Data.DataSet GetRepIDDO(string CurrentUser) 
		{
			return Auth.GetRepIDDO(CurrentUser);
		}

		//Code Added by Cognizant On 07/09/2004
		/// <summary>
		/// To Get District office(New)
		/// </summary>
		public DataSet GetDOsByApp() 
		{
			return Auth.GetDOsByApp();
		}

		/// <summary>Lists users by role By Application</summary>
		public DataSet ListUsersByRolesByApp(string RepDO,int AppId) 
		{
			return Auth.ListUsersByRolesByApp(RepDO,AppId);
		}

		//Code Added By Cognizant on 04/19/2005 as part of Cashier Recon Enhancements
		/// <summary>Lists all Approvers by RepDo</summary>
		public DataSet ListApprovers(string RepDO) 
		{
			return Auth.ListApprovers(RepDO);
		}

	}
}        
namespace AuthenticationClasses.InternalService {
	/// <summary>Authentication web service connection.</summary>
	internal class Authentication : CSAAWeb.Web.SoapHttpClientProtocol {
		/// <summary>Authenticates the user and returns user information</summary>
		[SoapDocumentMethod]
		public UserInfo Authenticate(string Password, SessionInfo S) {
			return (UserInfo) Invoke(new object[] {Password, S})[0];
		}

        //SSO-Integration.Ch2: Added new method for Authenticate in Internal reference for SSO integration by Cognizant on 09/24/2010
        /// <summary>Authenticates the user and returns user information</summary>
        [SoapDocumentMethod]
        public UserInfo SSOAuthenticate(string Password, SessionInfo S, bool IsOpenTokenExists)
        {
            return (UserInfo)Invoke(new object[] { Password, S, IsOpenTokenExists })[0];
        }

		/// <summary>Returns contact information for UserId</summary>
		[SoapDocumentMethod]
		public UserInfo GetContactInfo(string UserId, int UserRid, SessionInfo S) {
			return (UserInfo)Invoke(new object[] {UserId, UserRid, S})[0];
		}

		/// <summary>Returns all the users.</summary>
		///STAR Retrofit II.Ch10 - START Added a new Soap reference to retrieve user details.
		[SoapDocumentMethod]
		public DataSet GetUsersByStatus(SessionInfo S, bool ShowAll, string DO,string Userid, int Status) 
		{
			return (DataSet)Invoke(new object[]{S, ShowAll, DO, Userid, Status})[0];
		}
		///STAR Retrofit II.Ch10 - END

		/// <summary>Returns all the users.</summary>
		[SoapDocumentMethod]
		public DataSet GetUsers(SessionInfo S, bool ShowAll, string DO) {
			return (DataSet)Invoke(new object[]{S, ShowAll, DO})[0];
		}

		/// <summary>Returns a dataset of all the possible roles.</summary>
		[SoapDocumentMethod]
		public DataSet GetRoles(SessionInfo S, string ForApp) {
			return (DataSet)Invoke(new object[]{S, ForApp})[0];
		}

		//STAR Retrofit II.Ch8 - START Created to retrieve the details of district offices.
		/// <summary>Returns a dataset of the districe office information.</summary>
		[SoapDocumentMethod]
		public DataSet GetListofDOs (SessionInfo S, string DOID) 
		{
			return (DataSet)Invoke(new object[]{S, DOID})[0];
		}
		//STAR Retrofit II.Ch8 - END
        
		//STAR Retrofit II.Ch3 - START Created a Soap reference to retrieve the details of district office for the selected DO code.
		/// <summary>Returns a dataset of district office details</summary>
		[SoapDocumentMethod]
		public DataSet GetDOdetails(string DOID) 
		{
			return (DataSet)Invoke(new object[]{DOID})[0];
		}
		//STAR Retrofit II.Ch3 - END

		//RBAC.Ch2 - START Created a SOAP Reference to retrieve the details of all users needed for RBAC system.
		/// <summary>Returns a dataset of all users details.</summary>
		[SoapDocumentMethod]
		public DataSet GetAllUserDetails() 
		{
			return (DataSet)Invoke(new object[]{})[0];
		}
		//RBAC.Ch2 - END
		
		//STAR Retrofit II.Ch4 - START Created a Soap reference to add or update DOs profile.
		[SoapDocumentMethod]
		public ArrayOfErrorInfo UpdateDO(int DORid,string DOName,string DOnum,string HUB,bool Active,string CurrentUser) 
		{
			return (ArrayOfErrorInfo)Invoke(new object[]{DORid,DOName,DOnum,HUB,Active,CurrentUser})[0];
		}
		//STAR Retrofit II.Ch4 - END

		/// <summary>Returns a dataset of all the possible report roles.</summary>
		[SoapDocumentMethod]
		public DataSet GetAppRoles(SessionInfo S, int AppId) 
		{
			return (DataSet)Invoke(new object[]{S, AppId})[0];
		}

		/// <summary>Returns a dataset of applications.</summary>
		[SoapDocumentMethod]
		public DataSet GetApplications(SessionInfo S) {
			return (DataSet)Invoke(new object[]{S})[0];
		}
		//STAR Retrofit II.Ch7 - START Created to retrieve the list of HUB offices.
		/// <summary>Returns a dataset of district offices.</summary>
		[SoapDocumentMethod]
		public DataSet GetAllDOs()
		{
			return (DataSet)Invoke(new object[]{})[0];
		}
		//STAR Retrofit II.Ch7 - END

		/// <summary>Gets a list of the applications.</summary>
		[SoapDocumentMethod]
		public DataSet GetApps(SessionInfo S) 
		{
			return (DataSet)Invoke(new object[]{S})[0];
		}

		/// <summary>Deletes user UserRid.</summary>
		[SoapDocumentMethod]
		public string DeleteUser(int UserRid, SessionInfo S) {
			return (string)Invoke(new object[]{UserRid, S})[0];
		}

		/// <summary>Resets UserRid's password.</summary>
		[SoapDocumentMethod]
		public string ResetPassword(string UserId, SessionInfo S) {
			return (string)Invoke(new object[]{UserId, S})[0];
		}

		/// <summary>Adds or updates user's profile.</summary>
		[SoapDocumentMethod]
		public ArrayOfErrorInfo UpdateUser(UserInfo User, SessionInfo S) {
			return (ArrayOfErrorInfo)Invoke(new object[]{User, S})[0];
		}

		/// <summary>Sets the accounts lockout status.</summary>
		[SoapDocumentMethod]
		public void SetAccountLockout(string UserId, bool Lock, SessionInfo S) {
			Invoke(new object[]{UserId, Lock, S});
		}

		/// <summary>Gets a list of the district offices.</summary>
		[SoapDocumentMethod]
		public DataSet GetDOs(SessionInfo S) {
			return (DataSet)Invoke(new object[]{S})[0];
		}

		/// <summary>Lists users by role</summary>
		[SoapDocumentMethod]
		public DataSet ListUsersByRoles(string RequesterNm, int RoleId, string RepDO, SessionInfo S ) 
		{
			return (DataSet)Invoke(new object[] {RequesterNm, RoleId, RepDO, S })[0];
		}
		/// <summary>Lists application users by role.</summary>
		[SoapDocumentMethod]
		public DataSet ListAppUsersByRoles(string RequesterNm, int RoleId, string RepDO, int AppId, SessionInfo S ) 
		{
			return (DataSet)Invoke(new object[] {RequesterNm, RoleId, RepDO, AppId, S })[0];
		}


		/// <summary>
		/// Sets new password for UserId
		/// </summary>
		[SoapDocumentMethod]
		public bool UpdatePassword (string UserId, string Password, SessionInfo S, out ArrayOfErrorInfo Errors) {
			object[] Result = Invoke(new object[] {UserId, Password, S});
			Errors = (ArrayOfErrorInfo)Result[1];
			return (bool)Result[0];
		}

		/// <summary>
		/// Confirms that the token is valid and that the user/app has permission on the method.
		/// </summary>
		[SoapDocumentMethod]
		public ArrayOfErrorInfo ValidateSession(SessionInfo S, string Method) {
			return (ArrayOfErrorInfo)Invoke(new object[] {S, Method})[0];
		}
		///Changed by Cognizant on 05/18/04 
		/// <summary>
		/// Gets Rep ID and DO for the current user
		/// </summary>
		/// <param name="CurrentUser">Currently logged in User</param>
		/// <returns>Dataset of RepID and DO for the current user</returns>
		[SoapDocumentMethodAttribute]
		public System.Data.DataSet GetRepIDDO(string CurrentUser) 
		{
			return (System.Data.DataSet)Invoke(new object[] {CurrentUser})[0];
		}

		//Code Added by Cognizant on 07/09/2004
		/// <summary>Gets a list of the district offices by (Application Not Now)</summary>
		[SoapDocumentMethod]
		public DataSet GetDOsByApp() 
		{
			return (DataSet)Invoke(new object[]{})[0];
		}

		//Code Added By Cognizant on 07/09/2004
		/// <summary>Lists users by role(Created for Later Purpose)</summary>
		[SoapDocumentMethod]
		public DataSet ListUsersByRolesByApp(string RepDO,int AppId ) 
		{
			return (DataSet)Invoke(new object[] {RepDO,AppId})[0];
		}

		//Code Added By Cognizant on 04/18/2005 as part of Cashier Recon Enhancements
		/// <summary>Lists all Approvers by RepDo</summary>
		[SoapDocumentMethod]
		public DataSet ListApprovers(string RepDO) 
		{
		return (DataSet)Invoke(new object[] {RepDO})[0];
		}

	}
}