/*
 Revision History:
 1/29/03 Jeff McEwen Version II.
 * 7/15/03 JOM Modified to use CSAAWeb.
 * RFC#130547 PT_eCheck Ch1 Added the new dataset Bank Account types reference for getting the bank account type details- by cognizant on 05/3/2011.
*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using System.Configuration;
using PaymentClasses;
using CSAAWeb.AppLogger;
using CSAAWeb;
using AuthenticationClasses;



namespace PaymentService
{
    /// <summary>
    /// This is a web service that contains the basic payment processing functions, plus some additional
    /// lookup methods that return .NET datasets.
    /// </summary>
    [WebService(Namespace = "http://csaa.com/webservices/")]
    public class Service : PaymentGateway
    {

        /// <summary>
        /// Returns a unique Merchant Reference Number that may be used for a transaction.
        /// </summary>
        /// <param name="AppId">(string) The calling application.</param>
        [WebMethod(Description = "Returns a unique MerchantRefNum ID for this transaction.")]
        public string GetMerchantReference(string AppId)
        {
            return Data.GetMerchantReference(AppId);
        }

        /// <summary>
        /// Returns dataset containing accepted credit card names and numeric codes (Name, Code)
        /// </summary>
        /// <param name="includePrompt">True if the first row should be a prompt.</param>
        [WebMethod(Description = "Returns dataset containing accepted credit card names and numeric codes (Name, Code)")]
        public DataSet GetCreditCards(bool includePrompt)
        {
            return Data.GetCardTypes(includePrompt);
        }
        /// <summary>
        /// RFC#130547 PT_eCheck Ch1 Added the new dataset Bank Account types reference for getting the bank account type details- by cognizant on 05/3/2011.
        /// Returns dataset containing accepted Account names and numeric codes (Name, Code)
        /// </summary>
        /// <param name="includePrompt">True if the first row should be a prompt.</param>
        [WebMethod(Description = "Returns dataset containing accepted Account names and numeric codes (Name, Code)")]
        public DataSet GetBankAccount(bool includePrompt)
        {
            return Data.GetAccountTypes(includePrompt);
        }

        /// <summary>
        /// Added by Cognizant on 05/17/2004 for Adding a new Method GetPaymentType
        /// </summary> 
        /// <param name="ColumnFlag">Screen type(Payments(P) or Reports(R) or Workflow(W))</param>
        /// <param name="CurrentUser">The user name of the current user.</param>
        /// <returns>Dataset of all Payment Types</returns>
        [WebMethod(Description = "Returns a DataSets of Payment Types.")]
        public DataSet GetPaymentType(string ColumnFlag, string CurrentUser)
        {
            return Data.GetPaymentType(ColumnFlag, CurrentUser);
        }


        /// <summary>Gets list of states for use with billing addresses</summary>
        [WebMethod(Description = "Gets list of states for use with billing addresses")]
        public DataSet GetStateCodes()
        {
            return Data.GetStateCodes();
        }

    }
}
