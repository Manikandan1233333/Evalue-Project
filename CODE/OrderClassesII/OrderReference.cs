/*	REVISION HISTORY:
 *	MODIFIED BY COGNIZANT
 *	PC Phase II changes CH1 - Added the below code to Lookup Database for mapping IDP Request.
 *	*/
using System;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Web.Security;
using System.Security.Principal;
using System.Xml.Serialization;
using CSAAWeb;
using CSAAWeb.Serializers;
using OrderClasses;
using AuthenticationClasses;
using AuthenticationClasses.Service;

namespace OrderClasses.Service {
	/// <summary>
	/// Class for Order Web Reference
	/// </summary>
	public class Order {
		private string UserId=string.Empty;
		private string Token=string.Empty;
		///<summary/>
		public string AppId=string.Empty;
		private OrderClasses.InternalService.Order O;
		public System.Web.UI.Page Page=null;
		///<summary/>
		public Order() {
			AppId = Config.Setting("AppName");
			O = new OrderClasses.InternalService.Order();
		}
		///<summary/>
		public void Close() {
			if (O!=null) O.Close();
			O=null;
		}
		///<summary/>
		public Order(System.Web.UI.Page Page) : this() {
			this.Page=Page;
			UserInfo U = new UserInfo(Page);
			UserId = U.UserId;
			Token = U.Token;
		}
		private bool HasAuthErrors(ArrayOfErrorInfo Errors) {
			if (Errors!=null)
				foreach (ErrorInfo E in Errors) 
					if (E.Code=="INVALID TOKEN" || E.Code=="TIMEOUT") return true;
			return false;
		}
		private void HandleAuthErrors(ArrayOfErrorInfo Errors) {
			if (HasAuthErrors(Errors)) {
				Page.Response.Clear();
				FormsAuthentication.SignOut();
				Page.Response.Redirect(CSAAWeb.Navigation.ACL.UnauthorizedUrl, true);
			}
		}
		#region GetReference
		///<summary>Returns a new Reference Number</summary>
		public string GetReference() {
			ArrayOfErrorInfo Errors;
			string Result = O.GetReference(Token, UserId, AppId, out Errors);
			HandleAuthErrors(Errors);
			if (Errors!=null && Errors.Count>0) throw new Exception(Errors[0].Message);
			return Result;
		}
		#endregion

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the GetRates - Code Clan Up II- March 2016
		#region CaculateOrderLines
		/// <summary>
		/// Calculates order lines for the product type specified.
		/// </summary>
		/// <param name="Order">The order process</param>
		/// <param name="ProductName">Product type to update lines for, or blank to process all.</param>
		public OrderInfo CalculateOrderLines(OrderInfo Order, string ProductName) {
			OrderInfo Result = O.CalculateOrderLines(Token, UserId, AppId, Order, ProductName);
			HandleAuthErrors(Result.Errors);
			return Result;
		}
		/// <summary>
		/// Calculates order lines for the product type specified.
		/// </summary>
		/// <param name="Order">The order process</param>
		public OrderInfo CalculateOrderLines(OrderInfo Order) {
			return CalculateOrderLines(Order, "");
		}
		#endregion
		#region Process Order
		/// <summary>
		/// This method actually does the work of entering an order.
		/// </summary>
		/// <param name="Order">The order to process</param>
		/// <returns>The updated order</returns>
		public OrderInfo Process(OrderInfo Order) {
			OrderInfo Result = O.ProcessOrder(Token, UserId, AppId, Order);
			HandleAuthErrors(Result.Errors);
			return Result;
		}

		#endregion
		#region Authenticate
		/// <summary>
		/// Authenticates a User, and returns a user info object that contains all of the
		/// available information about the user plus a security token for use with other methods
		/// in this service.
		/// </summary>
		public UserInfo Authenticate(string UserId, string Password, string AppId) {
			return O.Authenticate(UserId, Password, AppId);
		}
		/// <summary>
		/// Validates the user token, and that the user has permission to call the
		/// Method.  Returns errors if there are any.  If the token is valid, and there
		/// currently isn't an authentication token in Page, creates an authentication token
		/// and established it in Page.
		/// </summary>
		public bool ValidateToken(string Token, string UserId) {
			if (Page!=null) {
				this.UserId = UserId;
				this.Token = Token;
				UserInfo U = GetUserInfo(UserId);
				if (U.UserId!="") U.SetAuthentication(this.Page);
				return (U.UserId!="" && (U.Errors==null || U.Errors.Count==0));
			} else return HasAuthErrors(O.ValidateToken(Token, UserId, AppId));
		}
		#endregion
		#region GetUserInfo
		/// <summary>
		/// Returns user information.
		/// </summary>
		/// <param name="UserId">UserId of the logged-in user.</param>
		public UserInfo GetUserInfo(string UserId) {
			ArrayOfErrorInfo Errors;
			UserInfo Result = O.GetUserInfo(Token, UserId, AppId, out Errors);
			HandleAuthErrors(Errors);
			if (Errors!=null && Errors.Count>0) throw new Exception(Errors[0].Message);
			return Result;
		}

		#endregion
		#region Lookup
		/// <summary>
		/// Performs specified lookup function.
		/// </summary>
		/// <param name="Service">The service from which to lookup the value.</param>
		/// <param name="What">The name of the value to lookup</param>
		/// <param name="Errors"></param>
		public object Lookup(string Service, string What, out ArrayOfErrorInfo Errors) {
			return O.Lookup(Token, UserId, AppId, Service, What, out Errors, new object[] {});
		}

		/// <summary>
		/// Performs specified lookup function.
		/// </summary>
		/// <param name="Service">The service from which to lookup the value.</param>
		/// <param name="What">The name of the value to lookup</param>
		public object Lookup(string Service, string What) {
			ArrayOfErrorInfo Errors;
			object Result = Lookup(Service, What, out Errors);
			HandleAuthErrors(Errors);
			if (Errors!=null && Errors.Count!=0) throw new Exception(Errors[0].Message);
			return Result;
		}

		/// <summary>
		/// Performs specified lookup function.
		/// </summary>
		/// <param name="Service">The service from which to lookup the value.</param>
		/// <param name="What">The name of the value to lookup</param>
		/// <param name="Params">The parameters to provide to the lookup function</param>
		/// <param name="Errors"></param>
		public object Lookup(string Service, string What, out ArrayOfErrorInfo Errors, object[] Params) {
			return O.Lookup(Token, UserId, AppId, Service, What, out Errors, Params);
		}

		/// <summary>
		/// Performs specified lookup function.
		/// </summary>
		/// <param name="Service">The service from which to lookup the value.</param>
		/// <param name="What">The name of the value to lookup</param>
		/// <param name="Params">The parameters to provide to the lookup function</param>
		public object Lookup(string Service, string What, object[] Params) {
			ArrayOfErrorInfo Errors;
			object Result = Lookup(Service, What, out Errors, Params);
			HandleAuthErrors(Errors);
			if (Errors!=null && Errors.Count!=0) throw new Exception(Errors[0].Message);
			return Result;
		}

        //Begin PC Phase II changes CH1 - Added the below code to Lookup Database for mapping IDP Request.
        /// <summary>
        /// Performs specified lookup function.
        /// </summary>
        /// <param name="Token">Security token provided by authenticate</param>
        /// <param name="UserId">UserId of the logged-in user.</param>
        /// <param name="AppId">Name of the calling application.</param>
        /// <param name="Service">The service from which to lookup the value.</param>
        /// <param name="What">The name of the value to lookup</param>
        /// <param name="Errors"> Outs the Collection of errorrs logged in the method</param>
        public DataSet LookupDataSet(string Token, string UserId,string AppId,string Service, string What, out ArrayOfErrorInfo Errors) 
            {
             return O.LookupDataSet(Token, UserId, AppId, Service, What, out Errors, new object[] { });
            }
        //End PC Phase II changes CH1 - Added the below code to Lookup Database for mapping IDP Request.
		/// <summary>
		/// Performs specified lookup function.
		/// </summary>
		/// <param name="Service">The service from which to lookup the value.</param>
		/// <param name="What">The name of the value to lookup</param>
		/// <param name="Errors"></param>
		public DataSet LookupDataSet(string Service, string What, out ArrayOfErrorInfo Errors) {
			return O.LookupDataSet(Token, UserId, AppId, Service, What, out Errors, new object[] {});
		}

		/// <summary>
		/// Performs specified lookup function.
		/// </summary>
		/// <param name="Service">The service from which to lookup the value.</param>
		/// <param name="What">The name of the value to lookup</param>
		public DataSet LookupDataSet(string Service, string What) {
			ArrayOfErrorInfo Errors;
			DataSet Result = LookupDataSet(Service, What, out Errors);
			HandleAuthErrors(Errors);
			if (Errors!=null && Errors.Count!=0) throw new Exception(Errors[0].Message);
			return Result;
		}

		/// <summary>
		/// Performs specified lookup function.
		/// </summary>
		/// <param name="Service">The service from which to lookup the value.</param>
		/// <param name="What">The name of the value to lookup</param>
		/// <param name="Params">The parameters to provide to the lookup function</param>
		/// <param name="Errors"></param>
		public DataSet LookupDataSet(string Service, string What, out ArrayOfErrorInfo Errors, object[] Params) {
			return O.LookupDataSet(Token, UserId, AppId, Service, What, out Errors, Params);
		}

		/// <summary>
		/// Performs specified lookup function.
		/// </summary>
		/// <param name="Service">The service from which to lookup the value.</param>
		/// <param name="What">The name of the value to lookup</param>
		/// <param name="Params">The parameters to provide to the lookup function</param>
		public DataSet LookupDataSet(string Service, string What, object[] Params) {
			ArrayOfErrorInfo Errors;
			DataSet Result = LookupDataSet(Service, What, out Errors, Params);
			HandleAuthErrors(Errors);
			if (Errors!=null && Errors.Count!=0) throw new Exception(Errors[0].Message);
			return Result;
		}

		#endregion
	}
}
namespace OrderClasses.InternalService {
	/// <summary>
	/// Class for Order Web Reference
	/// </summary>
	internal class Order : CSAAWeb.Web.SoapHttpClientProtocol {
		#region GetReference
		///<summary>Returns a new Reference Number</summary>
		/// <param name="AppId">Name of the calling application.</param>
		/// <param name="Token">Security token provided by authenticate</param>
		/// <param name="UserId">UserId of the logged-in user.</param>
		/// <param name="Errors"></param>
		[SoapDocumentMethod]
		public string GetReference(string Token, string UserId, string AppId, out ArrayOfErrorInfo Errors) {
			object[] Result = Invoke(new Object[] {Token, UserId, AppId});
			Errors = (Result.Length==1)?null:(ArrayOfErrorInfo)Result[1];
			return (string) Result[0];
		}
		///<summary>Returns a new Reference Number</summary>
		/// <param name="AppId">Name of the calling application.</param>
		/// <param name="Token">Security token provided by authenticate</param>
		/// <param name="UserId">UserId of the logged-in user.</param>
		public string GetReference(string Token, string UserId, string AppId) {
			ArrayOfErrorInfo Errors;
			string Result = GetReference(Token, UserId, AppId, out Errors);
			if (Errors!=null && Errors.Count>0) throw new Exception(Errors[0].Message);
			return Result;
		}
		#endregion
		#region Get Rates
		/// <summary>
		/// Gets information about the promo selected and gets the applicable rates.
		/// </summary>
		/// <param name="AppId">Name of the calling application.</param>
		/// <param name="Token">Security token provided by authenticate</param>
		/// <param name="UserId">UserId of the logged-in user.</param>
		/// <param name="Order">The order process</param>
		/// <param name="ProductName">Product type to get rates for, or blank to process all.</param>
		[SoapDocumentMethod]
		public OrderInfo GetRates(string Token, string UserId, string AppId, OrderInfo Order, string ProductName) {
			return (OrderInfo)Invoke(new Object[] {Token, UserId, AppId, Order, ProductName})[0];
		}
		#endregion
		#region CaculateOrderLines
		/// <summary>
		/// Calculates order lines for the product type specified.
		/// </summary>
		/// <param name="AppId">Name of the calling application.</param>
		/// <param name="Token">Security token provided by authenticate</param>
		/// <param name="UserId">UserId of the logged-in user.</param>
		/// <param name="Order">The order process</param>
		/// <param name="ProductName">Product type to update lines for, or blank to process all.</param>
		[SoapDocumentMethod]
		public OrderInfo CalculateOrderLines(string Token, string UserId, string AppId, OrderInfo Order, string ProductName) {
			return (OrderInfo)Invoke(new Object[] {Token, UserId, AppId, Order, ProductName})[0];
		}
		#endregion
		#region Process Order
		/// <summary>
		/// This method actually does the work of entering an order.
		/// </summary>
		/// <param name="AppId">Name of the calling application.</param>
		/// <param name="Token">Security token provided by authenticate</param>
		/// <param name="UserId">UserId of the logged-in user.</param>
		/// <param name="Order">The order to process</param>
		/// <returns>The updated order</returns>
		[SoapDocumentMethod]
		public OrderInfo ProcessOrder(string Token, string UserId, string AppId, OrderInfo Order) {
			return (OrderInfo)Invoke(new Object[] {Token, UserId, AppId, Order})[0];
		}

		#endregion
		#region Authenticate
		/// <summary>
		/// Authenticates a User, and returns a user info object that contains all of the
		/// available information about the user plus a security token for use with other methods
		/// in this service.
		/// </summary>
		[SoapDocumentMethod]
		public UserInfo Authenticate(string UserId, string Password, string AppId) {
			return (UserInfo)Invoke(new Object[] {UserId, Password, AppId})[0];
		}
		/// <summary>
		/// Validates the user token, and that the user has permission to call the
		/// Method.  Returns errors if there are any.
		/// </summary>
		[SoapDocumentMethod]
		public ArrayOfErrorInfo ValidateToken(string Token, string UserId, string AppId) {
			return (ArrayOfErrorInfo)Invoke(new object[] {Token, UserId, AppId})[0];
		}
		#endregion
		#region GetUserInfo
		/// <summary>
		/// Returns user information.
		/// </summary>
		/// <param name="AppId">Name of the calling application.</param>
		/// <param name="Token">Security token provided by authenticate</param>
		/// <param name="UserId">UserId of the logged-in user.</param>
		/// <param name="Errors"></param>
		[SoapDocumentMethod]
		public UserInfo GetUserInfo(string Token, string UserId, string AppId, out ArrayOfErrorInfo Errors) {
			object[] Result = Invoke(new Object[] {Token, UserId, AppId, null});
			Errors = (Result.Length==1)?null:(ArrayOfErrorInfo)Result[1];
			return (UserInfo) Result[0];
		}

		/// <summary>
		/// Returns user information.
		/// </summary>
		/// <param name="AppId">Name of the calling application.</param>
		/// <param name="Token">Security token provided by authenticate</param>
		/// <param name="UserId">UserId of the logged-in user.</param>
		public UserInfo GetUserInfo(string Token, string UserId, string AppId) {
			ArrayOfErrorInfo Errors;
			UserInfo Result = GetUserInfo(Token, UserId, AppId, out Errors);
			if (Errors!=null && Errors.Count>0) throw new Exception(Errors[0].Message);
			return Result;
		}

		#endregion
		#region Lookup
		/// <summary>
		/// Performs specified lookup function.
		/// </summary>
		/// <param name="AppId">Name of the calling application.</param>
		/// <param name="Token">Security token provided by authenticate</param>
		/// <param name="UserId">UserId of the logged-in user.</param>
		/// <param name="Service">The service from which to lookup the value.</param>
		/// <param name="What">The name of the value to lookup</param>
		/// <param name="Params">The parameters to provide to the lookup function</param>
		/// <param name="Errors"></param>
		[SoapDocumentMethod]
		public DataSet LookupDataSet(string Token, string UserId, string AppId, string Service, string What, out ArrayOfErrorInfo Errors, object[] Params) {
			object[] Result = Invoke(new object[] {Token, UserId, AppId, Service, What, Params}, true);
			Errors = (ArrayOfErrorInfo)((Result.Length>1)?null:Result[1]);
			return (DataSet)Result[0];
		}

		/// <summary>
		/// Performs specified lookup function.
		/// </summary>
		/// <param name="AppId">Name of the calling application.</param>
		/// <param name="Token">Security token provided by authenticate</param>
		/// <param name="UserId">UserId of the logged-in user.</param>
		/// <param name="Service">The service from which to lookup the value.</param>
		/// <param name="What">The name of the value to lookup</param>
		/// <param name="Params">The parameters to provide to the lookup function</param>
		/// <param name="Errors"></param>
		[SoapDocumentMethod]
		public object Lookup(string Token, string UserId, string AppId, string Service, string What, out ArrayOfErrorInfo Errors, object[] Params) {
			object[] Result = Invoke(new object[] {Token, UserId, AppId, Service, What, Params}, true);
			//Errors = (ArrayOfErrorInfo)((Result.Length>1)?null:Result[1]);
			Errors = (ArrayOfErrorInfo)((Result.Length>1)?Result[1]:null);
			return (Result.Length==0)?null:Result[0];
		}
		#endregion
	}
}
