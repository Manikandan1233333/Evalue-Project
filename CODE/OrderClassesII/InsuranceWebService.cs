/* 
 * History:
 *	Modified By Cognizant
 *	Code Cleanup Activity for CSR #3937
 * 	06/30/2005  -	Removed the Web Method SearchOrders. Since this 
 *					Web Method is no longer used in the application.
 *	06/30/2005	-	Removed the Web Method GetInsReports. Since this web 
 *					method is used only in the older version of Insurance
 *					Reports, this is no longer used.
 *	06/30/2005	-	Removed the Web Method GetPayTransReports. Since this 
 *					Web Method is no longer required for the application.
 * 	07/20/2005	-	For changing the web.config entries pointing to database. This changes 
 *					are made as part of CSR #3937 implementation.
 *	CSR#3937.Ch1 :  Added a new webmethod PayGetConstants for getting the constant values from the database
 * 
 *  07/08/2005  -   Added a new web method ManualUpdateSearch for searching insurance and membership transactions 
 *					matching provided criteria as part of CSR#3937
 * 
 *  07/08/2005  -   Added a new web method ManualUpdate to update Rep DO,User Name,Status for the given receipt 
 *					number as part of CSR#3937
 * 
 * MODIFIED BY COGNIZANT AS PART OF Q4 RETROFIT CHANGES
 * 
 * 12/14/2005 Q4-Retrofit.Ch1: Renamed the Webmethod GetPoesReports to GetBatchReports and
 *            modified to returns all POES/HUON Batch Reports based on search Report Type.
 * 12/14/2005 Q4-Retrofit.Ch2:Added two new methods (ValidateHUONCheckDigit,HUONCheckDigit)
 *            for Validating HUON Policy Check Digit
 * 
 * MODIFIED BY COGNIZANT AS A PART OF SR#8434145 ON 06/20/2009
 * SR#8434145.Ch1 : Added a new web method CheckDuplicatePolicy() for checking the duplicate payment type.
 * 11/03/2009  Billing and Payments Quick Hits Project- RFC 48347:Modified method CheckActivePolicy()by cognizant for accepting only policy number  to check in IVR database and return
 * product type from IVR database.
 * 67811A0  - PCI Remediation for Payment systems CH1 : Added the new method to invoke the SP to void the transaction status by cognizant on 08/25/2011.
 *  67811A0  - PCI Remediation for Payment systems CH2 : Modified the method input paramters to accept policy prefix as an additional parameters to policy look up in IVR data base by cognizant on 09/27/2011
 *  67811A0  - PCI Remediation for Payment systems CH3  : Added the new invoke method for policy look up in SIS stored procedure by cognizant on 09/26/2011.
 *  67811A0  - PCI Remediation for Payment systems CH4  :Added to provide input to the CPM for voiding CPM transactions and to log the CC void transactions in ArcSight by cognizant on 1/4/2012
 *  67811A0  - PCI Remediation for Payment systems CH5  : Added the new method to invoke the SP to void the transaction status by cognizant on 1/5/2012.
 *  67811A0  - PCI Remediation for Payment systems CH6 : Added the method to validate the merchant reference number(RegEX Validation) as part of the security team defect fix
 *  AZ PAS conversion and PC integration-CH1 - Added the below method to invoke the policy detail from the billing summary lookup
 *  AZ PAS conversion and PC integration-Ch2 Added an input parameter named Datasource in the check policy method
 *  //PC Phase II Changes - CH1 - Modified Get Membership Reports to call appropotiate SP
 *  PC Phase II 4/20 - Added logging in below code to find the user ID and search criteria for the insurance reports, Transaction search. 
 *  CHG0083477 - Added additional expected characters to the regex pattern for validation of Home policies in the required order. 
 *  MAIG - BEGIN - CH1 - Added the below method to get the Agency, Location and Users Details from DB for displaying in Reports page
 *  CHG0129017 - Removal of Linked server - Sep 2016
 */

using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using OrderClasses;
using CSAAWeb;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using CSAAWeb.Web;
using CSAAWeb.AppLogger;

namespace InsuranceClasses.WebService
{
    /// <summary>
    /// Class that allows connection to the database for insurance transactions.
    /// </summary>
    [WebService(Namespace = "http://csaa.com/webservices/")]
    public class Insurance : CSAAWeb.Web.SqlWebService
    {

        private static bool LogInsert = false;
        /// <summary>
        /// These procedures will participate in a transaction, and so must have their
        /// parameters pre-cached.
        /// </summary>
        private static string[] PreCacheParametersFor =
            new string[] { "INS_Insert_Order", "INS_Insert_Address", "INS_Insert_Item", "INS_Update_Status", "INS_Reissue_Receipt" };

        static Insurance()
        {
            LogInsert = Config.bSetting("Insurance_LogInsert");
        }

        #region Begin_Transaction
        /// <summary>
        /// Creates the records for the order in the appropriate tables.  Returns
        /// the orderID.
        /// </summary>
        [WebMethod(Description = "Inserts records into INS_Order, INS_Address, INS_Item tables")]
        public int Begin_Transaction(InsuranceInfo Order)
        {
            //Start the transaction.
            if (LogInsert) CSAAWeb.AppLogger.Logger.Log(Order.ToString());
            StartTransaction();
            try
            {
                //START Changed by Cognizant on 05/25/2004 for checking the Payment and Product Type Combination for each line Item.
                for (int i = 0; i < Order.Lines.Count; i++)
                {
                    SqlCommand CmdCheck = GetCommand("Check_Payment_Product");
                    CmdCheck.Parameters.Add("@Payment_Type_ID", Order.Detail.PaymentType);
                    CmdCheck.Parameters.Add("@Product_Type_Code", Order.Lines[i].ProductType);

                    SqlParameter P = CmdCheck.Parameters.Add("@Match", SqlDbType.Int);
                    P.Direction = ParameterDirection.Output;

                    CmdCheck.ExecuteNonQuery();
                    if ((int)CmdCheck.Parameters["@Match"].Value == 0)
                        throw new Exception("Payment Type and Product Type doesn't Match");
                }
                //END
                int OrderId = Insert_Order(Order);
                // Build the command here for next one which is called repeatedly.
                SqlCommand Cmd = GetCommand("INS_Insert_Item");
                for (int i = 0; i < Order.Lines.Count; i++)
                    Insert_Record(Cmd, OrderId, Order.Lines[i]);
                Cmd = GetCommand("INS_Insert_Address");
                for (int i = 0; i < Order.Addresses.Count; i++)
                    Insert_Record(Cmd, OrderId, Order.Addresses[i]);
                CompleteTransaction(true);
                return OrderId;
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
        /// Inserts a record into the customer orders table.
        /// </summary>
        private int Insert_Order(InsuranceInfo Order)
        {
            SqlCommand Cmd = GetCommand("INS_Insert_Order");
            Order.Detail.CopyTo(Cmd);
            Order.User.CopyTo(Cmd);
            Order.S.CopyTo(Cmd);
            Cmd.Parameters["@OrderId"].Direction = ParameterDirection.Output;
            Cmd.ExecuteNonQuery();
            return (int)Cmd.Parameters["@OrderId"].Value;
        }

        /// <summary>
        /// Inserts a record into a table using Cmd
        /// </summary>
        private void Insert_Record(SqlCommand Cmd, int OrderId, CSAAWeb.Serializers.SimpleSerializer O)
        {
            O.CopyTo(Cmd);
            Cmd.Parameters["@OrderId"].Value = OrderId;
            Cmd.ExecuteNonQuery();
        }
        #endregion

        #region Complete_Transaction
        /// <summary>
        /// Completes the transaction started by Begin_Insurance_Transaction
        /// </summary>
        [WebMethod(Description = "Completes the transaction started by Begin_Insurance_Transaction")]
        public void Complete_Transaction(int OrderId, string AuthCode, string RequestId, string ReceiptNumber)
        {
            SqlCommand Cmd = GetCommand("INS_Complete_Order");
            Cmd.Parameters.Add("@OrderId", OrderId);
            Cmd.Parameters.Add("@AuthCode", AuthCode);
            Cmd.Parameters.Add("@RequestId", RequestId);
            Cmd.Parameters.Add("@ReceiptNumber", ReceiptNumber);
            Cmd.ExecuteNonQuery();
        }
        #endregion

        #region Lookups
        /// <summary>
        /// Returns a DataTable of product types.
        /// </summary>
        [WebMethod(Description = "Returns a recordset of product types.")]
        public DataSet ProductTypes()
        {
            DataSet DS = new DataSet();
            new SqlDataAdapter(GetCommand("INS_Get_Product_Types")).Fill(DS, "INS_Product_Type");
            return DS;
        }
        /// <summary>
        /// Created by Cognizant on 12/10/2004 
        /// Returns a DataSet of all Insurance Product types containing both IIB and WU products.
        /// </summary>
        [WebMethod(Description = "Returns a recordset of product types.")]
        public DataSet GetAllProductTypes()
        {
            DataSet DS = new DataSet();
            new SqlDataAdapter(GetCommand("INS_Reports_Get_All_Product_Types")).Fill(DS, "All_INS_Product_Types");
            return DS;
        }
        /// <summary>
        /// Returns a DataTable of revenue types.
        /// </summary>
        [WebMethod(Description = "Returns a recordset of revenue types.")]
        public DataSet RevenueTypes()
        {
            DataSet DS = new DataSet();
            new SqlDataAdapter(GetCommand("INS_Get_Revenue_Types")).Fill(DS, "INS_Revenue_Type");
            return DS;
        }

        /// <summary>
        /// Returns a DataTable of report types.
        /// </summary>
        [WebMethod(Description = "Returns a recordset of report types.")]
        public DataSet RevenueTypesByRole(string CurrentUser, string AppName)
        {
            SqlCommand Cmd = GetCommand("INS_Get_Revenue_Types_By_Role");
            Cmd.Parameters.Add("@CurrentUser", CurrentUser);
            Cmd.Parameters.Add("@AppName", AppName);
            SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds, "INS_Revenue_Type_By_Role");
            return ds;

        }

        /// <summary>
        /// Returns a DataTable of report types.
        /// </summary>
        [WebMethod(Description = "Returns a recordset of report types.")]
        public DataSet ReportTypes(string AccessType)
        {
            SqlCommand Cmd = GetCommand("INS_Get_Report_Types");
            Cmd.Parameters.Add("@AccessType", AccessType);
            SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds, "INS_Report_Type");
            return ds;

        }
        //MAIG - BEGIN - CH1 - Added the below method to get the Agency, Location and Users Details from DB for displaying in Reports page
        /// <summary>
        /// Returns a DataTable of Location.
        /// </summary>
        [WebMethod(Description = "Returns a recordset of Location.")]
        public DataSet Location(string AgencyID)
        {
            SqlCommand Cmd = GetCommand("INS_Get_LocationByAgent");
            Cmd.Parameters.Add("@AgencyID", AgencyID);
            SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds, "LOCATION");
            return ds;

        }
        /// <summary>
        /// Returns a DataTable of User.
        /// </summary>
        [WebMethod(Description = "Returns a recordset of User.")]
        public DataSet User(string AgencyID)
        {
            SqlCommand Cmd = GetCommand("INS_Get_UsersByAgency");
            Cmd.Parameters.Add("@AgencyID", AgencyID);
            SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds, "USER");
            return ds;

        }
        /// <summary>
        /// Returns a DataTable of Location.
        /// </summary>
        [WebMethod(Description = "Returns a recordset of Agency.")]
        public DataSet Agency(string AgencyID)
        {
            SqlCommand Cmd = GetCommand("INS_Get_Agency");
            Cmd.Parameters.Add("@AgencyID", AgencyID);
            SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds, "AGENCY");
            return ds;

        }
        //MAIG - END - CH1 - Added the below method to get the Agency, Location and Users Details from DB for displaying in Reports page
        ///<summary>Encodes the policy string for sending to checkdigit.
        [WebMethod(Description = "Returns an encoded policy number without check digit for policy string.")]
        public string EncodePolicy(string Policy)
        {
            if (Policy.Length != 7)
                throw new BusinessRuleException("Policy (" + Policy + ") must be 7 characters long.");
            if (!RegexPolicy.IsMatch(Policy))
                throw new BusinessRuleException("Policy (" + Policy + ") not a valid sequence.");
            return ConvertPolicyChar(Policy.Substring(0, 1)) + "0" +
                ConvertPolicyChar(Policy.Substring(1, 1)) +
                Policy.Substring(2, 4);
        }

        ///<summary>Returns the check digit for a numeric sequence.
        [WebMethod(Description = "Returns the check digit for st, which must be numeric")]
        public string CheckDigit(string st)
        {
            return Cryptor.CheckDigit(st);
        }

        /// <summary>
        /// Validates the policy's check digit.
        /// </summary>
        /// 
        [WebMethod(Description = "Validates the policy's check digit.")]
        public bool ValidateCheckDigit(string Policy)
        {

            if (Policy.Length != 7 || !RegexPolicy.IsMatch(Policy)) return false;
            return (CheckDigit(EncodePolicy(Policy)) == Policy.Substring(6, 1));
        }

        //CHG0083477 - Added additional expected characters to the regex pattern for validation of Home policies in the required order - Start.
        private static string ConversionTable = "0123456789ABCDEJKLNRPTVWXYFFHMUIOGQSZ";
        //CHG0083477 - Added additional expected characters to the regex pattern for validation of Home policies in the required order - End.
        private static Regex RegexPolicy = new Regex("^[" + ConversionTable + "]{2,2}[0-9]{5,5}$", RegexOptions.Compiled);
        //67811A0  - PCI Remediation for Payment systems CH6-START : Added the method to validate the merchant reference number(RegEX Validation) as part of the security team defect fix
        [WebMethod(Description = "Validates the Merchant Reference check digit")]
        public bool ValidateMerchantNum(string MerchantRefNum)
        {

            if (Convert.ToInt16(MerchantRefNum.Length) > Convert.ToInt16("50") || !RegexmerchantRefNum.IsMatch(MerchantRefNum))
            {
                return false;
            }
            else
                return true;

        }
        //67811A0  - PCI Remediation for Payment systems CH6-END : Added the method to validate the merchant reference number(RegEX Validation) as part of the security team defect fix
        private static Regex RegexmerchantRefNum = new Regex("^[a-zA-Z0-9]+$", RegexOptions.Compiled);
        //private static Regex RegexmerchantRefNum = new Regex("[A-Za-z0-9]*$", RegexOptions.Compiled);
        /// <summary>
        /// Converts a single char according to the policy char conversion table.
        /// </summary>
        /// <param name="ch">Char to convert</param>
        /// <returns>Converted value of char.</returns>

        private string ConvertPolicyChar(string ch)
        {
            string Result = ConversionTable.IndexOf(ch).ToString();
            if (Result.Length == 1) Result = "0" + Result;
            return Result;
        }

        /// <summary>
        /// Returns a DataTable of Revenue Types.
        /// </summary>
        [WebMethod(Description = "Returns a recordset of report types.")]
        public DataSet RevenueTypesByProduct(string ProductTypeCode)
        {
            // .Modified by Cognizant based on new SP changes
            SqlCommand Cmd = GetCommand("PAY_Get_Revenue_Types");
            SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds, "RevenueTypesByProduct");
            return ds;

        }

        #endregion
        // 67811A0  - PCI Remediation for Payment systems CH1 Start : Added the new method to invoke the SP to void the transaction status by cognizant on 08/25/2011.

        //[WebMethod(Description = "Returns the status of the Cash/Check/Offline creditcard transaction.")]
        //public VoidPayments VoidPayment(string UserId, string AppId, string ReceiptNumber)
        //{

        //    VoidPayments Trans_Status = new VoidPayments();
        //    SqlCommand Cmd = GetCommand("Pay_Payment_Void");
        //    Cmd.Parameters.Add("@UserId", UserId);
        //    Cmd.Parameters.Add("@AppID", AppId);
        //    Cmd.Parameters.Add("@ReceiptNumber", ReceiptNumber);
        //    SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
        //    DataSet ds = new DataSet();
        //    sqlDa.Fill(ds, "VoidPayments");

        //    Trans_Status.Flag = ds.Tables["VoidPayments"].Rows[0]["Flag"].ToString();
        //    Trans_Status.Flag_Message = ds.Tables["VoidPayments"].Rows[0]["Flag_Message"].ToString();
        //    Trans_Status.Error_Message = ds.Tables["VoidPayments"].Rows[0]["Error_Message"].ToString();
        //    // 67811A0  - PCI Remediation for Payment systems CH4  :Added to provide input to the CPM for voiding CPM transactions and to log the CC void transactions in ArcSight by cognizant on 1/4/2012
        //    string RevenueType = ds.Tables["VoidPayments"].Rows[0]["Items"].ToString();
        //    string Sequence_Number = ds.Tables["VoidPayments"].Rows[0]["Sequence_Number"].ToString();
        //    string Product_Code = ds.Tables["VoidPayments"].Rows[0]["Product_Code"].ToString();
        //    string Merchant_ID = ds.Tables["VoidPayments"].Rows[0]["Merchant_ID"].ToString();
        //    if (Trans_Status.Flag != "1" && Sequence_Number != "")
        //    {
        //        bool Complete = Convert.ToBoolean(ds.Tables["VoidPayments"].Rows[0]["Complete"]);
        //        if (Complete)
        //        {
        //            CPM_PaymentService.CyberResponse R = new CPM_PaymentService.CyberResponse();
        //            List<string> Response = new List<string>();
        //            Response = R.ccVoid(UserId, AppId, ReceiptNumber, RevenueType, Sequence_Number, Product_Code, Merchant_ID);
        //            VoidPayments V = null;

        //            V = VoidCCPayment(UserId, AppId, ReceiptNumber, Response[0].ToString(), Response[1].ToString(), Response[2].ToString());
        //            string response = Response[2].ToString();
        //            Logger.SourceProcessName = AppId;
        //            Logger.SourceUserName = UserId;
        //            Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DESTINATION_PROCESSNAME_PAYMENT;
        //            Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_NAME_CARD_VOID + ReceiptNumber;
        //            if (response == "0")
        //            {
        //                Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS;
        //                Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_LOW;
        //                Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_VOID_SUCCESS;
        //                Logger.ArcsightLog();
        //            }
        //            else
        //            {
        //                Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
        //                Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
        //                Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_VOID_FAILURE;
        //                Logger.ArcsightLog();
        //            }

        //            return V;
        //        }
        //        return Trans_Status;
        //    }
        //    else
        //        return Trans_Status;
        //    // 67811A0  - PCI Remediation for Payment systems CH4  :Added to provide input to the CPM for voiding CPM transactions and to log the CC void transactions in ArcSight by cognizant on 1/4/2012
        //}

        // 67811A0  - PCI Remediation for Payment systems CH1 END : Added the new method to invoke the SP to void the transaction status by cognizant on 08/25/2011.

        // 67811A0  - PCI Remediation for Payment systems CH5 START : Added the new method to invoke the SP to void the transaction status by cognizant on 1/5/2012.
        [WebMethod(Description = "Returns the status of the CPM Void transactions.")]
        public VoidPayments VoidCCPayment(string UserId, string AppID, string ReceiptNumber, string Sequence_Number, string Message, string Result)
        {

            VoidPayments Trans_Status = new VoidPayments();
            SqlCommand Cmd = GetCommand("Pay_CC_Void_Payment");
            Cmd.Parameters.Add("@UserId", UserId);
            Cmd.Parameters.Add("@AppID", AppID);
            Cmd.Parameters.Add("@ReceiptNumber", ReceiptNumber);
            Cmd.Parameters.Add("@Sequence_Number", Sequence_Number);
            Cmd.Parameters.Add("@Message", Message);
            Cmd.Parameters.Add("@Result", Result);
            SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds, "VoidCCPayments");

            Trans_Status.Flag = ds.Tables["VoidCCPayments"].Rows[0]["Flag"].ToString();
            Trans_Status.Flag_Message = ds.Tables["VoidCCPayments"].Rows[0]["Flag_Message"].ToString();
            Trans_Status.Error_Message = ds.Tables["VoidCCPayments"].Rows[0]["Error_Message"].ToString();

            return Trans_Status;

        }
        // 67811A0  - PCI Remediation for Payment systems CH5 END : Added the new method to invoke the SP to void the transaction status by cognizant on 1/5/2012.
        #region GetInsuranceReports
        /// <summary>
        /// Get Insurance Reports matching provided criteria - Added new method for Version 2.
        /// </summary>
        [WebMethod(Description = "Returns recordsets for insurance reports based on the input criteria.", BufferResponse = true)]
        public DataSet GetInsuranceReports(ReportCriteria ReportParams)
        {

            Logger.Log(CSAAWeb.Constants.INS_REPORTS_SP_START);
            SqlCommand Cmd = GetCommand("INS_Reports");
            ReportParams.CopyTo(Cmd);
            SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds);
            Logger.Log(CSAAWeb.Constants.INS_REPORTS_SP_END);
            return ds;
        }
        #endregion

        #region ReceiptReIssue

        //Code modified by Cognizant on 05/20/2004
        /// <summary>
        /// This function Calls the Receipt_Reissue SP to Reissue a given ReceiptNumber
        /// </summary>
        /// <returns>Index of the extended transaction.</returns>
        [WebMethod(Description = "ReIssue the given ReceiptNumber on the input criteria.", BufferResponse = true)]
        public int ReIssueReceipt(ReportCriteria ReceiptParams, string NewReceiptNumber)
        {
            try
            {
                StartTransaction();
                SqlCommand cmd = GetCommand("INS_Reissue_Receipt");
                ReceiptParams.CopyTo(cmd);
                cmd.Parameters["@NewReceiptNumber"].Value = NewReceiptNumber;
                cmd.ExecuteNonQuery();
                return SetExtendedTransaction();
            }
            catch (Exception e)
            {
                CompleteTransaction(false);
                throw new Exception("Exception occured processing ReIssueReceipt in InsuranceWebService.", e);
            }
        }
        #endregion

        #region UpdateStatus

        //Code added by JOM on 11/10/2004
        /// <summary>
        /// This function calls the INS_Update_Status SP to updates the receipts
        /// </summary>
        /// <returns>Index of the extended transaction.</returns>
        [WebMethod(Description = "Update the status of the Receipts.", BufferResponse = true)]
        public int UpdateStatus(ReportCriteria ReportParams, TurninClasses.ArrayOfReceipt Receipts, string Action)
        {
            try
            {
                StartTransaction();
                SqlCommand Cmd = GetCommand("INS_Update_Status");
                ReportParams.CopyTo(Cmd);
                Cmd.Parameters["@Action"].Value = Action;
                foreach (TurninClasses.Receipt R in Receipts)
                {
                    R.CopyTo(Cmd);
                    Cmd.ExecuteNonQuery();
                }
                return SetExtendedTransaction();
            }
            catch (Exception e)
            {
                CompleteTransaction(false);
                throw new Exception("Exception occured processing UpdateStatus in InsuranceWebService.", e);
            }
        }
        #endregion


        //12/15/2005 Removed as a part of Q4 Retrofit
        /* 
         * #region POES Reports
         * //Added  by Cognizant on 07/13/04 
         * /// <summary>
         * /// Get the POES reports for the Report Criteria
         * /// </summary>
         * /// <param name="ReceiptParams">The Upload_Date will be available in the Receipt Params as a filter</param>
		
         * [WebMethod(Description="Get the POES Reports for the corresponding Report Criteria ",BufferResponse=true)]
         * public DataSet GetPoesReports(ReportCriteria ReceiptParams)
         * {
         * 	SqlCommand Cmd = GetCommand("POES_Report");
         * 	ReceiptParams.CopyTo(Cmd);
         * 	SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
         * 	DataSet ds = new DataSet();
         * 	sqlDa.Fill(ds);
         * 	return ds;
         * }

         * #endregion
        */

        #region NewSearchOrders
        //START - Code addded by COGNIZANT 08/07/2004 - New Transaction Search Orders Function
        /// <summary>
        /// Search for insurance and membership transactions matching provided criteria
        /// </summary>
        [WebMethod(Description = "Case-insensitive search for transactions based on input fields", BufferResponse = true)]
        public DataSet NewSearchOrders(SearchCriteria SearchFor)
        {
            // .Modified by Cognizant based on  SP changes
            // PC Phase II 4/20 - Added logging in below code to find the user ID and search criteria for the insurance reports, Transaction search 
            Logger.Log(CSAAWeb.Constants.PAY_Search_Orders_SP_START);
            DataSet ds = new DataSet();
            //CHG0129017 - Removal of Linked server - Start//
            try
            {
                string rep = Config.Setting("ConnectionString.ReadonlyDBInstance");
                SqlConnection con = new SqlConnection(rep);
                con.Open();
                SqlCommand Cmd = new SqlCommand("PAY_Search_Orders", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                //CHG0129017 - Removal of Linked serve - End//
                //SqlCommand Cmd = GetCommand("PAY_Search_Orders");
                SearchFor.CopyTo(Cmd);
                SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
                sqlDa.Fill(ds);
                con.Close();
                //PC Phase II 4/20 - Added logging in below code to find the user ID and search criteria for the insurance reports, Transaction search 
                Logger.Log(CSAAWeb.Constants.PAY_Search_Orders_SP_END);
            }
            catch (Exception e)
            {
                Logger.Log(e);
            }
            return ds;

        }
        // END
        #endregion

        #region GetUploadType
        /// <summary>
        /// Added by Cognizant on 10/18/2004 for fetching the UploadType for the Corresponding ProductType
        /// </summary>
        /// <param name="ProductType">The ProductType for the corresponding Product</param> 
        /// <returns>UploadType</returns> 
        // .Modified by Cognizant changed as on Tier1
        [WebMethod(Description = "Gets the UploadType for the corresponding ProductType")]
        public string GetUploadType(string ProductType)
        {
            SqlCommand Cmd = GetCommand("INS_Get_Upload_Type");
            Cmd.Parameters.Add("@ProductType", ProductType);
            SqlParameter P = Cmd.Parameters.Add("@Upload_Type", SqlDbType.VarChar, 25);
            P.Direction = ParameterDirection.Output;
            Cmd.ExecuteNonQuery();
            return (string)Cmd.Parameters["@Upload_Type"].Value;
        }
        #endregion

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods with respect to CheckActiveIVRPolicy - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods with respect to CheckActivePolicy - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods with respect to Membership - March 2016

        ////AZ PAS conversion and PC integration-Ch1 - Added the below method to invoke the policy detail from the billing summary lookup
        //AZ PAS conversion and PC integration-Ch2 Added an input parameter named Datasource in the check policy method
        public DataTable CheckPolicy(string Policy, string Productcode, string userID, string Datasource)
        {
            OrderClassesII.BillingLookUp Lookup = new OrderClassesII.BillingLookUp();
            return Lookup.checkPolicy(Policy, Productcode, userID, Datasource);


        }
        ////AZ PAS conversion and PC integration- Added the below method to invoke the policy detail from the billing summary lookup
        //#region CheckDuplicatePayment
        /////<summary> SR#8434145.Ch1 - START: Added by Cognizant on 06/20/2009 for Checking Duplicate payment for Policy
        /////<param name="ProductTypeCode">The ProductType code for the corresponding Product</param> 
        /////<param name="Policy">The Policy Number for the corresponding Product</param>
        /////<param name="Amount">The Amount  for the corresponding policy</param>
        /////<returns>Data set with duplicate payment record</returns>
        //[WebMethod(Description = "Checks whether the Policy is Duplicate or not")]
        //public DataSet CheckDuplicatePolicy(string Policy, string ProductTypeCode, decimal Amount)
        //{
        //    SqlCommand CmdCheck = GetCommand("INS_Check_Duplicate_Payment");
        //    CmdCheck.Parameters.Add("@PolicyNumber", Policy);
        //    CmdCheck.Parameters.Add("@ProductType", ProductTypeCode);
        //    CmdCheck.Parameters.Add("@Amount", Amount);
        //    DataSet ds = new DataSet();

        //    SqlDataAdapter da = new SqlDataAdapter(CmdCheck);
        //    da.Fill(ds, "Duplicate record");

        //    return ds;
        //}
        //SR#8434145.Ch1 - END
        //#endregion

        #region ApplicationProductTypes
        /// <summary>
        /// Returns a DataTable of Application Product types.
        /// </summary>
        // .Modified by Cognizant Added for PCR 29 OffShore
        [WebMethod(Description = "Returns a recordset of Application Product Types.")]
        public DataSet ApplicationProductTypes(int AppId)
        {
            // .Modified by Cognizant based on new SP changes
            SqlCommand Cmd = GetCommand("PAY_Get_Product_Codes");
            SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds, "ApplicationProductTypes");
            return ds;
        }
        #endregion

        #region PayTransReportTypes
        /// <summary>
        /// Returns a DataTable of PayTrans Report Types.
        /// </summary>
        /// .Modified by Cognizant Added for PCR 29 Offshore
        [WebMethod(Description = "Returns a recordset of PayTrans Report Types.")]
        public DataSet PayTransReportTypes()
        {
            SqlCommand Cmd = GetCommand("PAY_Trans_Get_Report_Types");
            SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds, "PayTrans_Report_Types");
            return ds;
        }
        #endregion


        #region GetApplications
        //.Modified by Cognizant based on SP changes
        [WebMethod(Description = "Gets a list of the applications.")]
        public DataSet GetApplications()
        {
            SqlCommand Cmd = GetCommand("PAY_Get_Applications");
            DataSet DS = new DataSet();
            new SqlDataAdapter(Cmd).Fill(DS);
            return DS;
        }
        #endregion

        #region GetReceiptNumber
        /// <summary>
        /// Added by Cognizant on 05/24/2004 for fetching the ReceiptNumber for the Corresponding OrderId
        /// </summary>
        /// <param name="OrderId">OrderId of the Transaction</param> 
        /// <param name="TransactionType">The Transaction Type("Membership")</param> 
        /// <returns>ReceiptNumber</returns> 
        [WebMethod(Description = "Get the Receipt Number for the Corresonding OrderId of the Transaction")]
        public string GetReceiptNumber(int OrderId, string TransactionType)
        {
            //Stored Procedure Get_ReceiptNumber is in Payments Database
            //SqlCommand Cmd = GetCommand("Payments.dbo.Get_ReceiptNumber");
            SqlCommand Cmd = GetCommand("GET_RECEIPTNUMBER");
            Cmd.Parameters.Add("@OrderID", OrderId);
            Cmd.Parameters.Add("@TransactionType", TransactionType);
            SqlParameter P = Cmd.Parameters.Add("@ReceiptNumber", SqlDbType.VarChar, 25);
            P.Direction = ParameterDirection.Output;
            Cmd.ExecuteNonQuery();
            return (string)Cmd.Parameters["@ReceiptNumber"].Value;
        }
        #endregion

        //Q4-Retrofit.Ch2-START: Validates the check digit for HUON Auto policies
        #region ValidateHUONCheckDigit
        /// <summary>
        /// Validates the check digit for HUON Auto policies
        /// </summary>		
        [WebMethod(Description = "Validates the check digit for HUON Auto policies")]
        public bool ValidateHUONCheckDigit(string Policy)
        {
            if ((Policy.Length != 9) || (!CSAAWeb.Validate.IsAllNumeric(Policy)))
                return false;
            return (HUONCheckDigit(Policy.Substring(0, 8)) == Policy.Substring(8, 1));
        }
        #endregion
        //Q4-Retrofit.Ch2-END

        //Q4-Retrofit.Ch2-START: HUON Policy Check Digit Algorithm
        #region HUONCheckDigit
        /// <summary>
        /// This method returns the check digit for a given policy number
        /// </summary>
        /// <param name="policy">first 8 digit of HUON policy number</param> 
        /// <returns>checkdigit</returns>	
        private string HUONCheckDigit(string policy)
        {
            int[] arr_weight;
            // Reversed the array weight sequence as per the onsite request on 31/3/1006
            //  arr_weight = new int[8] {2,5,3,6,4,8,7,10};
            arr_weight = new int[8] { 10, 7, 8, 4, 6, 3, 5, 2 };
            int sum = 0;
            for (int i = policy.Length - 1; i >= 0; i--)
            {
                int prod = Convert.ToInt32(policy.Substring(i, 1)) * arr_weight[i];
                sum += prod;
            }
            return (((sum % 11) > 1)) ? (11 - (sum % 11)).ToString() : (sum % 11).ToString();
        }
        #endregion
        //Q4-Retrofit.Ch2-END

        #region Manual Update Search
        //START - Code added by COGNIZANT - 07/08/2005(CRS #3937) - Function for calling the Manual Update Search
        /// <summary>
        /// Search for insurance and membership transactions matching provided criteria
        /// </summary>
        [WebMethod(Description = "Case-insensitive search for transactions based on input fields", BufferResponse = true)]
        public DataSet ManualUpdateSearch(SearchCriteria SearchFor)
        {
            // .Modified by Cognizant based on  SP changes
            SqlCommand Cmd = GetCommand("PAY_Manual_Update_Search");
            SearchFor.CopyTo(Cmd);
            SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds);
            return ds;
        }
        // END
        #endregion

        #region Manual Update

        //Code added by COGNIZANT - 07/08/2005(CRS #3937) - Function for calling the Manual Update
        /// <summary>
        /// This function calls the PAY_Manual_Update SP to update Rep DO,User Name,Status for the given receipt number
        /// </summary>
        /// <returns>Status of the query result</returns>
        [WebMethod(Description = "Update the status of the Receipts.", BufferResponse = true)]
        public int ManualUpdate(string ReceiptNbr, string status, string currentUser, string DO, string userName)
        {
            try
            {
                SqlCommand Cmd = GetCommand("PAY_Manual_Update");
                Cmd.Parameters.Add("@CurrentUser", currentUser);
                Cmd.Parameters.Add("@ReceiptNumber", ReceiptNbr);
                Cmd.Parameters.Add("@Status", status);
                Cmd.Parameters.Add("@DO", DO);
                Cmd.Parameters.Add("@UserName", userName);
                SqlParameter outPut = Cmd.Parameters.Add("@outParam", SqlDbType.Int);
                outPut.Direction = ParameterDirection.Output;
                Cmd.ExecuteNonQuery();
                return (Convert.ToInt32(outPut.Value));
            }
            catch (Exception e)
            {
                CompleteTransaction(false);
                throw new Exception("Exception occured processing Manual Update in InsuranceWebService.", e);
            }
        }
        #endregion

        //CSR#3937.Ch1 : START - Added a new webmethod PayGetConstants for getting the constant values from the database
        #region PayGetConstants
        ///<summary>
        ///Get the constant key and values from the PAY_Constants table (web.config change)
        ///</summary>
        [WebMethod(Description = "Returns the recordset containing the key - value pairs for config entries from PAY_Constants table.", BufferResponse = true)]
        public DataSet PayGetConstants()
        {
            SqlCommand Cmd = GetCommand("PAY_Get_Constants");
            SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds);
            return ds;
        }
        #endregion
        //CSR#3937.Ch1 : END - Added a new webmethod PayGetConstants for getting the constant values from the database

        //Q4-Retrofit.Ch2-END

        //Q4-Retrofit.Ch2-START: HUON Policy Check Digit Algorithm

    }
}
