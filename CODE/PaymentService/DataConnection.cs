/*
 *	REVISION HISTORY:
 *	07/21/2005		:	MODIFIED BY COGNIZANT - AS PART OF CSR#3937	
 *	CSR#3937.Ch1	-	Removed the entry "PAY_Get_Reference_Number" from the PreCacheParametersFor string.
 *						Since this stored procedure was removed from the database.
 *	05/03/2011 RFC#130547 PT_echeck CH1 added the new method GetAccountTypes by cognizant.
 */

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using CSAAWeb;
using CSAAWeb.Serializers;
using CSAAWeb.AppLogger;
using System.Text.RegularExpressions;
using PaymentClasses;

namespace PaymentService
{
    /// <summary>
    /// Summary description for DataConnection.
    /// </summary>
    public class DataConnection : CSAAWeb.Web.SqlWebService
    {
        private static bool LogInsert = false;
        private static DateTime ApplicationInfoStamp;
        /// <summary>
        /// Override of base ConnectionString property forces the name to Payments instead of DataConnection.
        /// </summary>
        protected override string ConnectionString
        {
            get { return Config.Setting("ConnectionString.Payments", ((Config.bSetting("ConnectionString.Payments.Encrypted")) ? Constants.CS_STRING : "")); }
        }
        /// <summary>
        /// These procedures will participate in a transaction, and so must have their
        /// parameters pre-cached.
        /// </summary>
        //CSR#3937.Ch1 : START - Removed the entry "PAY_Get_Reference_Number" from the PreCacheParametersFor string.
        //				Since this stored procedure was removed from the database.
        private static string[] PreCacheParametersFor =
            new string[] {"PAY_Insert_Payment", "PAY_Insert_Item", "PAY_Insert_Card_Detail", "PAY_Check_Card_ReAuth",
			"PAY_Get_Transaction_Details", "PAY_Insert_Card_Request",
			"PAY_Update_Card_Request", "PAY_Update_Payment_User","PAY_Update_Item_AccountNumber",
			"PAY_Get_Payment_ID"};
        //CSR#3937.Ch1 : END - Removed the entry "PAY_Get_Reference_Number" from the PreCacheParametersFor string.
        //				Since this stored procedure was removed from the database.
        static DataConnection()
        {
            LogInsert = Config.bSetting("Payment_LogInsert");
        }
        #region Begin_Transaction
        /// <summary>
        /// Creates the records for the payment in the appropriate tables.  Returns
        /// the paymentID.
        /// </summary>
        /// <param name="Payment">
        /// A record with the complete payment information, with properties including the card, billing
        /// address, line items etc.
        /// </param>
        internal CyberResponse Begin_Transaction(PaymentClasses.Payment Payment)
        {
            //Start the transaction.
            if (LogInsert) CSAAWeb.AppLogger.Logger.Log(Payment.ToString());
            StartTransaction();
            try
            {
                CyberResponse Result = null;
                bool IsNew = false;
                Payment.PaymentId = Insert_Payment(Payment, out IsNew);
                if (IsNew)
                {
                    Insert_Lines(Payment.PaymentId, Payment.LineItems);
                    Insert_Record(GetCommand("PAY_Update_Payment_User"), Payment.PaymentId, Payment.User);
                }
                if (Payment.Card != null)
                    Insert_Record(GetCommand("PAY_Insert_Card_Detail"), Payment.PaymentId, Payment.Card);
                if (Payment.PaymentType == PaymentTypes.CreditCard)
                    Result = InsertCardRequest(Payment);
                else Result = new CyberResponse();
                CompleteTransaction(true);
                Result.ReceiptNumber = Payment.ReceiptNumber;
                return Result;
            }
            catch (SqlException e)
            {

                //Modified by Cognizant to log the SqlException - on 03-31-2005
                Logger.Log(e);

                CompleteTransaction(false);
                if (e.Number != 50000) throw;
                throw new BusinessRuleException(e);
            }
            catch
            {
                CompleteTransaction(false);
                throw;
            }
        }
        #endregion
        #region Private methods for inserting records
        /// <summary>
        /// Inserts records for the line items.
        /// </summary>
        /// <param name="PaymentId">The payment id to insert lines for.</param>
        /// <param name="Lines">An array of the lines to insert.</param>
        private void Insert_Lines(int PaymentId, ArrayOfLineItem Lines)
        {
            SqlCommand Cmd = GetCommand("PAY_Insert_Item");
            for (int i = 0; i < Lines.Count; i++)
                Insert_Record(Cmd, PaymentId, Lines[i]);
        }
        /// <summary>
        /// Inserts a record into the main payment table.
        /// </summary>
        /// <param name="Payment">The payment to insert.</param>
        /// <param name="IsNew">(bool) output parameter indicating if the record is new or already exists.</param>
        private int Insert_Payment(PaymentClasses.Payment Payment, out bool IsNew)
        {
            SqlCommand Cmd = GetCommand("PAY_Insert_Payment");
            Payment.CopyTo(Cmd);
            Cmd.ExecuteNonQuery();
            IsNew = (bool)Cmd.Parameters["@New"].Value;
            if (Cmd.Parameters["@ReceiptNumber"].Value != DBNull.Value)
                Payment.ReceiptNumber = (string)Cmd.Parameters["@ReceiptNumber"].Value;
            return (int)Cmd.Parameters["@PaymentId"].Value;
        }

        /// <summary>
        /// Inserts a record into a table using Cmd
        /// </summary>
        /// <param name="Cmd">The Sql Command object to execute.</param>
        /// <param name="PaymentId">The payment id to which to assign the record.</param>
        /// <param name="Record">The record to insert.</param>
        private void Insert_Record(SqlCommand Cmd, int PaymentId, SimpleSerializer Record)
        {
            if (Record == null) throw new Exception("Missing data for: " + Cmd.CommandText);
            Record.CopyTo(Cmd);
            Cmd.Parameters["@PaymentId"].Value = PaymentId;
            Cmd.ExecuteNonQuery();
        }

        /// <summary>
        /// Inserts a cyber request record into the table.
        /// </summary>
        /// <param name="Payment">The payment to insert.</param>
        private CyberResponse InsertCardRequest(Payment Payment)
        {
            SqlCommand Cmd = GetCommand("PAY_Insert_Card_Request");
            Payment.CopyTo(Cmd);
            Cmd.Parameters["@ReAuth"].Value = (Payment.Operation == ServiceOperation.ReAuth);
            Cmd.Parameters["@BillToAddress"].Value = Payment.BillTo.ToString();
            Cmd.Parameters["@Items"].Value = Payment.LineItems.ToString();
            SqlDataReader Reader = Cmd.ExecuteReader(CommandBehavior.SingleRow);
            Reader.Read();
            return ReadResponse(Reader);
        }

        /// <summary>
        /// Returns a constructed CCResponse object with properties filled from 
        /// the database Reader.
        /// </summary>
        /// <param name="Reader">Dataset with saved response properties.</param>
        private CyberResponse ReadResponse(SqlDataReader Reader)
        {
            CyberResponse R1 = new CyberResponse();
            CSAAWeb.Serializers.Serializer.CopyFrom(Reader, R1);
            Reader.Close();
            if (R1.Flag == "SOK") R1.ReturnCode = "1";
            return R1;
        }
        #endregion
        #region Complete_Transaction
        /// <summary>
        /// Completes the transaction started by Begin_Insurance_Transaction
        /// </summary>
        public void Complete_Transaction(Payment Payment, CCResponse Response)
        {
            try
            {
                SqlCommand Cmd = GetCommand("PAY_Update_Card_Request");
                Response.CopyTo(Cmd);
                Cmd.Parameters["@App_Timestamp"].Value = ApplicationInfoStamp;
                Cmd.Parameters["@PaymentId"].Value = Payment.PaymentId;

                Cmd.ExecuteNonQuery();

                if (Payment.Operation == ServiceOperation.Auth || Payment.Operation == ServiceOperation.ReAuth)
                    if (Response.IsReauthCandidate)
                    {
                        CardFile.Add(Payment);
                    }
                    else if (Response.IsRequestSuccessful) CardFile.Remove(Payment);
            }
            catch (Exception e)
            {
                // Don't throw exceptions from this method.  The payment transaction has been
                // completed successfully, and we don't want to pass any local problems to the
                // application when the payment probably was OK.  Just log the exeception.
                if (e.GetType().Name == "SqlException" && ((SqlException)e).Number == 50000)
                {
                    // This error indicates that the Application information in the database has
                    // changed; the local copy must be updated and then the function re-tried.
                    GetApplications(CardInfo.Applications);
                }
                else
                {
                    Logger.Log(e);
                    Logger.Log("The Response that preceeded the previous exception was:\r\n" + Response.ToString() +
                        "\r\nNote that this exception resulted in an incomplete datalog for the transaction, but wasn't passed to the caller.");
                }
            }
        }
        #endregion
        #region GetTransactionDetails
        internal CyberResponse GetTransactionDetails(Payment Payment)
        {
            try
            {
                SqlCommand Cmd = GetCommand("PAY_Get_Transaction_Details");
                Payment.CopyTo(Cmd);
                SqlDataReader Reader = Cmd.ExecuteReader(CommandBehavior.SingleRow);
                Reader.Read();
                Payment.LineItems = new ArrayOfLineItem(Reader.GetString(5));
                Payment.BillTo = new BillToInfo(Reader.GetString(6));

                return ReadResponse(Reader);
            }
            catch (SqlException e)
            {
                if (e.Number != 50000) throw;
                throw new BusinessRuleException(e);
            }
        }
        #endregion
        #region UpdateLines
        internal void UpdateLines(PaymentClasses.Payment Payment)
        {
            //Start the transaction.
            StartTransaction();
            try
            {
                SqlCommand Cmd = GetCommand("PAY_Get_Payment_ID");
                Payment.CopyTo(Cmd);
                Cmd.ExecuteNonQuery();
                Payment.PaymentId = (int)Cmd.Parameters["@PaymentId"].Value;
                Cmd = GetCommand("PAY_Update_Item_AccountNumber");
                for (int i = 0; i < Payment.LineItems.Count; i++)
                    Insert_Record(Cmd, Payment.PaymentId, Payment.LineItems[i]);
                CompleteTransaction(true);
            }
            catch (SqlException e)
            {
                CompleteTransaction(false);
                if (e.Number != 50000) throw;
                throw new BusinessRuleException(e);
            }
            catch
            {
                CompleteTransaction(false);
                throw;
            }
        }
        #endregion


        #region Lookups
        /// <summary>
        /// Wrapper for Check_ReAuth stored procedure.  This procedure
        /// will raise an exception if its a duplicate.
        /// </summary>
        /// <param name="Payment">Service Request to check</param>
        public bool CheckReAuth(Payment Payment)
        {
            try
            {
                SqlCommand Cmd = GetCommand("PAY_Check_Card_ReAuth");
                Payment.CopyTo(Cmd);
                Cmd.Parameters["@CheckCard"].Value = CardFile.CheckCard(Payment);
                Cmd.Parameters["@BillTo"].Value = Payment.BillTo.ToString();
                if (Payment.LineItems != null) Cmd.Parameters["@Items"].Value = Payment.LineItems.ToString();
                return (Cmd.ExecuteScalar().ToString().Length != 0);
            }
            catch (SqlException e)
            {
                if (e.Number == 50000) throw new BusinessRuleException(e);
                throw;
            }
        }

        /// <summary>
        /// Get a recordset with Credit Card types.
        /// </summary>
        /// <returns></returns>
        public DataSet GetCardTypes(bool IncludePrompt)
        {
            SqlCommand cmd = GetCommand("PAY_Get_Card_Types");
            cmd.Parameters.Add("@IncludePrompt", IncludePrompt);
            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sqlda.Fill(ds, "CREDITCARD_TABLE");
            return ds;
        }
        /// <summary>
        ///RFC#130547 PT_echeck: Added by Cognizant on 05/03/2011 for Adding a new Method GetAccountTypes
        /// Get a recordset with Account types.
        /// </summary>
        /// <returns></returns>
        public DataSet GetAccountTypes(bool IncludePrompt)
        {
            SqlCommand cmd = GetCommand("PAY_Get_Account_Types");
            cmd.Parameters.Add("@IncludePrompt", IncludePrompt);
            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sqlda.Fill(ds, "BANKACCOUNT_TABLE");
            return ds;
        }
        /// <summary>
        /// Added by Cognizant on 05/17/2004 for Adding a new Method GetPaymentType
        /// </summary> 
        /// <param name="ColumnFlag">Screen type(Payments(P) or Reports(R) or Workflow(W))</param>
        /// <param name="CurrentUser">To Get the Payment Types that are applicable for the user</param>
        /// <returns>Dataset of all Payment Types</returns>
        public DataSet GetPaymentType(string ColumnFlag, string CurrentUser)
        {
            SqlCommand cmd = GetCommand("PAY_Get_Payment_Types");
            cmd.Parameters.Add("@ColumnFlag", ColumnFlag);
            cmd.Parameters.Add("@CurrentUser", CurrentUser);
            SqlDataAdapter sqlda = new SqlDataAdapter(cmd);
            DataSet ds = new DataSet();
            sqlda.Fill(ds, "PAY_Payment_Type");
            return ds;
        }

        /// <summary>
        /// Get a recordset with state codes.
        /// </summary>
        /// <returns>Dataset of the state codes that can be used</returns>
        public DataSet GetStateCodes()
        {
            SqlCommand Cmd = GetCommand("PAY_Get_State_Codes");
            SqlDataAdapter sqlda = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet("STATES");
            sqlda.Fill(ds, "STATES_TABLE");
            return ds;
        }

        /// <summary>
        /// Get a unique merchant reference number.
        /// </summary>
        /// <param name="Application">The ID of the calling application.</param>
        /// <returns>Merchant reference number</returns>
        public string GetMerchantReference(string Application)
        {
            SqlCommand cmd = GetCommand("Get_Running_Number");
            (new SimpleSerializer()).CopyTo(cmd);
            cmd.Parameters["@AppName"].Value = Application;
            cmd.Parameters["@NumberType"].Value = "REFERENCE";
            cmd.Parameters["@CurrentUser"].Value = "";
            cmd.Parameters["@RunningNumber"].Value = "";
            cmd.ExecuteNonQuery();
            return cmd.Parameters["@RunningNumber"].Value.ToString();
        }
        #endregion
        #region Applications
        /// <summary>
        /// Fills Applications with data from the Application_IDs table.
        /// </summary>
        /// <param name="Applications"></param>
        public void GetApplications(AppDictionary Applications)
        {
            try
            {
                SqlCommand Cmd = GetCommand("PAY_Get_Applications");
                Applications.AddData(Cmd.ExecuteReader());
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            finally
            {
                ApplicationInfoStamp = DateTime.Now;
            }
        }
        #endregion

    }
}
