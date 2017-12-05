/*  
 * History:
 *	Modified By Cognizant

* 05/03/2011 RFC#130547 PT_echeck CH1: modified by cognizant to Return a  dataset containing accepted Account names and numeric codes (Name, Code)

 */
using System;
using System.Web.Services.Protocols;
using System.Web.Services;
using System.Data;
using System.Collections;
using CSAAWeb.Serializers;
using System.Xml.Serialization;
using AuthenticationClasses;

namespace PaymentClasses.InternalService 
{
	/// <summary>
	/// Proxy class for accessing the Insurance Web service.
	/// </summary>
	public class Payments : CSAAWeb.Web.SoapHttpClientProtocol 
	{
		/// <summary>
		/// The assembly qualified name of the underlying web service.
		/// </summary>
		protected override string ServiceClass { get { return "PaymentService.Service, PaymentService, Culture=neutral, PublicKeyToken=null"; }}
		///<summary>Default constructor</summary>
		public Payments() : base() {} 
		/// <summary>
		/// Authorizes and bills (collects funds) from the card.
		/// </summary>
		/// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
		/// <param name="MerchantRefNum">The unique id by which to reference this transaction.</param>
		/// <param name="AppId">(string) The name (code) of the application calling the method.</param>
		/// <param name="BillTo">(BillTo) The customer's bill-to address.</param>
		/// <param name="Card">(CardInfo) The customer's credit card information.</param>
		/// <param name="Items">(ArrayOfLineItem) An array of the items being purchased.</param>
		[SoapDocumentMethodAttribute]
		public CCResponse Process(UserInfo User, string AppId, string MerchantRefNum, CardInfo Card, BillToInfo BillTo, ArrayOfLineItem Items) {
			return (CCResponse)Invoke(new object[] {User, AppId, MerchantRefNum, Card, BillTo, Items})[0];
		}
        
		/// <summary>
		/// Attempts to re-authorize the payment by the most appropriate method.
		/// </summary>
		/// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
		/// <param name="MerchantRefNum">The unique id by which to reference this transaction.</param>
		/// <param name="AppId">(string) The name (code) of the application calling the method.</param>
		/// <param name="BillTo">(BillTo) The customer's bill-to address.</param>
		/// <param name="Card">(CardInfo) The customer's credit card information.</param>
		[SoapDocumentMethodAttribute]
		public CCResponse ReAuth(UserInfo User, string AppId, string MerchantRefNum, CardInfo Card, BillToInfo BillTo) {
			return (CCResponse)Invoke(new object[] {User, AppId, MerchantRefNum, Card, BillTo})[0];
		}
        
		/// <summary>
		/// Authorize the payment on the customer's card.
		/// </summary>
		/// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
		/// <param name="MerchantRefNum">The unique id by which to reference this transaction.</param>
		/// <param name="AppId">(string) The name (code) of the application calling the method.</param>
		/// <param name="BillTo">(BillTo) The customer's bill-to address.</param>
		/// <param name="Card">(CardInfo) The customer's credit card information.</param>
		/// <param name="Items">(ArrayOfLineItem) An array of the items being purchased.</param>
		[WebMethod(Description="Send ICS_Auth request to CyberSource.")]
		[SoapDocumentMethodAttribute]
		public CCResponse Auth(UserInfo User, string AppId, string MerchantRefNum, CardInfo Card, BillToInfo BillTo, ArrayOfLineItem Items) {
			return (CCResponse)Invoke(new object[] {User, AppId, MerchantRefNum, Card, BillTo, Items})[0];
		}
        
		/// <summary>
		/// Bill (collect funds) that we previously authorized.
		/// </summary>
		/// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
		/// <param name="MerchantRefNum">The unique id by which to reference this transaction.</param>
		/// <param name="AppId">(string) The name (code) of the application calling the method.</param>
		[SoapDocumentMethodAttribute]
		public CCResponse Bill(UserInfo User, string MerchantRefNum, string AppId) {
			return (CCResponse)Invoke(new object[] {User, MerchantRefNum, AppId})[0];
		}
        
		/// <summary>
		/// Bill (collect funds) that were previously denied with a DCALL code 
		/// based on an authorization obtained over the telephone.
		/// </summary>
		/// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
		/// <param name="MerchantRefNum">The unique id by which to reference this transaction.</param>
		/// <param name="AppId">(string) The name (code) of the application calling the method.</param>
		/// <param name="AuthCode">(string) The authorization code obtained on the telephone.</param>
		[SoapDocumentMethodAttribute]
		public CCResponse BillVerbalAuth(UserInfo User, string AppId, string MerchantRefNum, string AuthCode) {
			return (CCResponse)Invoke(new object[] {User, MerchantRefNum, AppId, AuthCode})[0];
		}
        
		/// <summary>
		/// Refund an order.
		/// </summary>
		/// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
		/// <param name="MerchantRefNum">The unique id by which to reference this transaction.</param>
		/// <param name="AppId">(string) The name (code) of the application calling the method.</param>
		/// <param name="BillTo">(BillTo) The customer's bill-to address.</param>
		/// <param name="Card">(CardInfo) The customer's credit card information.</param>
		/// <param name="Items">(ArrayOfLineItem) An array of the items being purchased.</param>
		[SoapDocumentMethodAttribute]
		public CCResponse Credit(UserInfo User, string AppId, string MerchantRefNum, CardInfo Card, BillToInfo BillTo, ArrayOfLineItem Items) {
			return (CCResponse)Invoke(new object[] {User, AppId, MerchantRefNum, Card,BillTo, Items})[0];
		}
        
		/// <summary>
		/// Record a payment other than on-line credit card.
		/// </summary>
		/// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
		/// <param name="AppId">(string) The name (code) of the application calling the method.</param>
		/// <param name="PaymentType">(PaymentTypes) The type of payment.</param>
		/// <param name="Items">(ArrayOfLineItem) An array of the items being purchased.</param>
		/// <param name="ReceiptNumber">(string) The receipt number for this payment.</param>
		[SoapDocumentMethodAttribute]
		public CCResponse RecordPayment(UserInfo User, string AppId, PaymentTypes PaymentType, string ReceiptNumber, ArrayOfLineItem Items) {
			return (CCResponse)Invoke(new object[] {User, AppId, PaymentType, ReceiptNumber, Items})[0];
		}

		/// <summary>
		/// Update the account number on the lines for the payment.  Only one of receipt #
		/// or reference number need be supplied.
		/// </summary>
		/// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
		/// <param name="AppId">(string) The name (code) of the application calling the method.</param>
		/// <param name="PaymentType">(PaymentTypes) The type of payment.</param>
		/// <param name="Items">(ArrayOfLineItem) An array of the items being purchased.</param>
		/// <param name="ReceiptNumber">(string) The receipt number for this payment.</param>
		/// <param name="Reference">Merchant reference number</param>
		[SoapDocumentMethodAttribute]
		public void UpdateLines(UserInfo User, string AppId, PaymentTypes PaymentType, string ReceiptNumber, string Reference, ArrayOfLineItem Items) {
			Invoke(new object[] {User, AppId, PaymentType, ReceiptNumber, Reference, Items});
		}

		/// <summary>
		/// Returns a unique Merchant Reference Number that may be used for a transaction.
		/// </summary>
		/// <param name="AppId">(string) The calling application.</param>
		[SoapDocumentMethodAttribute]
		public string GetMerchantReference(string AppId) {
			return (string)Invoke(new object[] {AppId})[0];
		}
        

		/// <summary>
		/// Added by Cognizant on 05/17/2004 for Adding a new Method GetPaymentType which invokes the Webservice
		/// </summary> 
		/// <param name="ColumnFlag">Screen type(Payments(P) or Reports(R)or Workflow(W))</param>
		/// <param name="CurrentUser"> Get Current User to fetch PaymentType based on the roles for the user</param> 
		/// <returns>Dataset of all Payment Types</returns>
		[SoapDocumentMethodAttribute]
		public DataSet GetPaymentType(string ColumnFlag,string CurrentUser) {
			return (DataSet)Invoke(new object[] {ColumnFlag,CurrentUser})[0];
		}	

		/// <summary>
		/// Returns dataset containing accepted credit card names and numeric codes (Name, Code)
		/// </summary>
		/// <param name="includePrompt">True if the first row should be a prompt.</param>
		[SoapDocumentMethodAttribute]
		public DataSet GetCreditCards(bool includePrompt) 
		{
			return (DataSet)Invoke(new object[] {includePrompt})[0];
		}
		//commented as a part of removing echeck UI on 04-13-2010
		//Echeck Changes - 04/06/2010 added as a part of HO6
		/// <summary>
		/// Returns dataset containing accepted Account names and numeric codes (Name, Code)
		/// </summary>
		/// <param name="includePrompt">True if the first row should be a prompt.</param>
	//commented as a part of removing echeck UI on 04-13-2010
//		[SoapDocumentMethodAttribute]
//		public DataSet GetBankAccount(bool includePrompt) 
//		{
//			return (DataSet)Invoke(new object[] {includePrompt})[0];
//		}
        
		/// <summary>Gets list of states for use with billing addresses</summary>
		[SoapDocumentMethodAttribute]
        public DataSet GetBankAccount(bool includePrompt)
        {
            return (DataSet)Invoke(new object[] { includePrompt })[0];
        }
        
		/// <summary>Gets list of states for use with billing addresses</summary>
		[SoapDocumentMethodAttribute]
		public DataSet GetStateCodes() 
		{
			return (DataSet)Invoke(new object[] {})[0];
		}
        
	}
}
