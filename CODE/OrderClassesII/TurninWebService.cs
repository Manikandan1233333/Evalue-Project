/* 
 * History:
 *	Modified By Cognizant
 *	Code Cleanup Activity for CSR #3937
 * 	06/29/2005   -	Removed the Web Method ApplicationProductTypes. Since this 
 *					Web Method is no longer used in the application
 *	06/30/2005	-	Removed the Web Method ReceiptVoid. Since this web method is no
 *					longer used in the application.
 *	06/30/2005	-	Removed the Web Method ReceiptsTurnIn. Since this web method is
 *					no longer used in the application.
 *	06/30/2005	-	Removed the Web Method CashierApprove. Since this web method is 
 *					no longer used in the appliaction.
 *	06/30/2005	-	Removed the Web Method CashierReject. Since this web method is
 *					no longer used in the application. 
 *	06/30/2005	-	Removed the Web Method GetReceiptNumber. Since this web method is no
 *					longer used now. This web method is used in the Insurance and 
 *					Membership Web service.
 *	07/05/2005	-	Removed the Web Method PayTransReportTypes. Since this web method is 
 *					no where used in the application. 
 *
 			--> Modified by COGNIZANT as part of Q4-retrofit <--
 * 
 * 11/29/2005 Q4-Retrofit.Ch1
 *			  Renamed the Web Method SalesRepCashierWorkflow to PayWorkflowReport and modified it
 *			  to invoke Stored procedure  'PAY_Workflow_Report' instead of 'SalesRep_Cashier_Workflow'.
 * 
 * 11/30/2004 Q4-Retrofit.Ch2
 *			  Added following three new web methods for Finance Users:
			  i). PaySearchVoidReceipts: PaySearchVoidReceipts: New Web method,It returns a Dataset with the list of the Receipts (containing HUON products) with details matching the input search criteria(Used for searching).
		      ii). PayGetVoidReceipt: New WebMethod which returns the Dataset containing the complete Receipt details to void a receipt which matches the input search criteria.
              iii). PayUpdateVoidStatus: This method is used to void a Receipt containing HUON products based on the input search criteria. It returns an integer: Returns '1' if a Receipt is voided and '0' if the Receipt is not voided.
 *  Refund UI CH1 - Added the below method to process the refund operation if it is an credit card/echeck transaction and return the response back.
 * //PC PhaseII Changes - Start - Added the method PCVoidFlowCheck to check the Void flow
 * CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods with respect to Membership - March 2016
 *  CHG0129017 - Removal of Linked server - Sep 2016
 */
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using System.Collections;
using OrderClasses;
using CSAAWeb;
using CSAAWeb.Serializers;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using CSAAWeb.AppLogger;

namespace TurninClasses.WebService {
	/// <summary>
	/// Class that allows connection to the database for insurance transactions.
	/// </summary>
	[WebService(Namespace="http://csaa.com/webservices/")]
	public class Turnin : CSAAWeb.Web.SqlWebService {

		/// <summary>
		/// These procedures will participate in a transaction, and so must have their
		/// parameters pre-cached.
		/// </summary>
		private static string[] PreCacheParametersFor = new string[] {"PAY_Update_Status", "PAY_Reissue_Receipt"} ;

		#region GetReceiptDetails
		/// <summary>
		/// Get Receipt details on the input criteria.
		/// </summary>
		[WebMethod(Description="Returns recordsets of Receiptdetails on the input criteria.",BufferResponse=true)]
		public DataSet GetReceiptDetails(ReportCriteria ReportParams) {
			//SqlCommand Cmd = GetCommand("PAY_Get_Receipt_Details");
            //CHG0129017 - Removal of Linked server - Start//
            DataSet ds = new DataSet();
            try
            {
                string rep = Config.Setting("ConnectionString.ReadonlyDBInstance");
                SqlConnection con = new SqlConnection(rep);
                con.Open();
                SqlCommand Cmd = new SqlCommand("PAY_Get_Receipt_Details", con);
                Cmd.CommandType = CommandType.StoredProcedure;
                //CHG0129017 - Removal of Linked server - End//
                ReportParams.CopyTo(Cmd);
                SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
                sqlDa.Fill(ds);
                con.Close();
            }
            catch(Exception e)
            {
                Logger.Log(e);
            }
            return ds;
		}
		#endregion

		#region ReceiptReIssue

		//Code modified by Cognizant on 05/20/2004
		/// <summary>
		/// This function Calls the Receipt_Reissue SP to Reissue a given ReceiptNumber
		/// </summary>
		[WebMethod(Description="ReIssue the given ReceiptNumber on the input criteria.",BufferResponse=true)]
		public string ReceiptReIssue(ReportCriteria ReportParams) {
			InsuranceClasses.Service.Insurance I = new InsuranceClasses.Service.Insurance();
            //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods with respect to Membership - March 2016
			int i=0;
			int m=0;
			try {
				StartTransaction();
				SqlCommand Cmd = GetCommand("PAY_Reissue_Receipt");
				ReportParams.CopyTo(Cmd);
				Cmd.Parameters["@Application"].Value="APDS";
				Cmd.ExecuteNonQuery();
				string NewReceiptNumber = (string) Cmd.Parameters["@NewReceiptNumber"].Value;
				if (NewReceiptNumber!=null && NewReceiptNumber.Length>0) {
					i=I.ReIssueReceipt(ReportParams, NewReceiptNumber);
					I.CompleteTransaction(i, true);  // Modified by Cognizant on 3/3/2005 to commint transaction
                    //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods with respect to Membership - March 2016
				}
				CompleteTransaction(true);
				return NewReceiptNumber;
			} catch (Exception e) {
				try {if (i!=0) I.CompleteTransaction(i, false);} catch {}
                //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods with respect to Membership - March 2016
				try {CompleteTransaction(false);} catch {}
				throw new Exception("Exception occured processing ReceiptReIssue in TurninWebService.",e);
			}
		}
		#endregion

		#region UpdateStatus 
		/// <summary>
		/// Updates the status of the receipts.
		/// </summary>
		/// <param name="ReceiptParams">Other parameters.</param>
		/// <param name="Action">The update action to perform.</param>
		[WebMethod(Description="Updates the status of the receipts",BufferResponse=true)]
		public int UpdateStatus(ReportCriteria ReportParams, string Action) {
			InsuranceClasses.Service.Insurance I = new InsuranceClasses.Service.Insurance();
            //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods with respect to Membership - March 2016
			int i=0;
			int m=0;
			try {
				
				StartTransaction();
				SqlCommand Cmd = GetCommand("PAY_Update_Status");
				ReportParams.CopyTo(Cmd);
				Cmd.Parameters["@Application"].Value="APDS";
				Cmd.Parameters["@Action"].Value=Action;
				SqlDataReader Reader = Cmd.ExecuteReader();
				ArrayOfReceipt Receipts = new ArrayOfReceipt(Reader);
				Reader.Close();
				i = I.UpdateStatus(ReportParams, Receipts, Action);
                //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods with respect to Membership - March 2016
				I.CompleteTransaction(i, true);
                //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods with respect to Membership - March 2016
				CompleteTransaction(true);
				return Receipts.Count;
			} catch (Exception e){
				try {if (i!=0) I.CompleteTransaction(i, false);} catch {}
                //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods with respect to Membership - March 2016
				try {CompleteTransaction(false);} catch {}
				throw new Exception("Exception occured processing UpdateStatus in TurninWebService.",e);
			}
		}
		#endregion
	
		#region GetTurnInReportTypes
		///Changed by Cognizant on 05/18/04 
		/// <summary>
		/// Gets the report types for a particular screen and role
		/// </summary>
		/// <param name="CurrentUser">Currently logged in User</param>
		/// <param name="AccessType">An identifier for the calling screen (R for Reports and W for WorkFlow)</param>
		/// <returns>Dataset of Report Types for a particular screen and role</returns>
		[WebMethod(Description="Returns recordsets of the report ids and report descriptions available for the Current User and Access Type",BufferResponse=true)]
		public DataSet GetTurnInReportTypes(string CurrentUser,string AccessType) {
			SqlCommand Cmd = GetCommand("Get_TurnInReport_Types");
			Cmd.Parameters.Add("@CurrentUser",CurrentUser);
			Cmd.Parameters.Add("@AccessType",AccessType);
			SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();
			sqlDa.Fill(ds);
			return ds;
		}
		#endregion

		#region GetStatus
		///Changed by Cognizant on 05/18/04 
		/// <summary>
		/// Gets  the statuses that need to displayed for a particular report type and for a particular user based on his role. 
		/// </summary>
		/// <param name="CurrentUser">Currently logged in User</param>
		/// <param name="ReportType">The ID for the current report</param>
		/// <returns>Dataset of status ids and status descriptions available for the current report type</returns>
		[WebMethod(Description="Returns recordsets of status ids and status descriptions available for the current report type",BufferResponse=true)]
		public DataSet GetStatus(string CurrentUser,string ReportType) {
			SqlCommand Cmd = GetCommand("INS_Get_Status");
			Cmd.Parameters.Add("@CurrentUser",CurrentUser);
			Cmd.Parameters.Add("@ReportType",ReportType);
			SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();
			sqlDa.Fill(ds);
			return ds;
		}
		#endregion
	
		//Q4-Retrofit.Ch1 : START - Renamed the Web Method SalesRepCashierWorkflow to PayWorkflowReport and modified it to invoke Stored procedure  'PAY_Workflow_Report' instead of 'SalesRep_Cashier_Workflow'.		
				
		#region PAYWorkflowReport
		/// <summary>
		/// The Function is used in Sales Turn In,Cashier Reconciliation,Finance and Manager Reports
		/// Based on the chosen criteria, the details will be returned.
		/// </summary>
		/// <param name="ReportParams">The Criteria for obtaining data</param>
		/// <returns>Returns the recordset for a given criteria</returns>
		[WebMethod(Description="Returns the recordset for the criteria given in ReportParams",BufferResponse=true)]
		public DataSet PayWorkflowReport(ReportCriteria ReportParams) 
		{
						
			//Changed the SP Call from SalesRep_Cashier_WorkFlow to PAY_Workflow_Report
			SqlCommand Cmd = GetCommand("PAY_Workflow_Report");
			ReportParams.CopyTo(Cmd);
			SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();
			sqlDa.Fill(ds);
			return ds;
		}
		#endregion
		
		//Q4-Retrofit.Ch1 : END
		

		#region GetPayTransReports
		/// <summary>
		/// Get Payment Transactions Reports matching provided criteria.
		/// </summary>
		[WebMethod(Description="Returns recordsets for PayTrans reports based on the input criteria.",BufferResponse=true)]
		public DataSet GetPayTransReports(ReportCriteria ReportParams) {
			// .Modified by Cognizant based on new SP changes
			SqlCommand Cmd = GetCommand("PAY_Reports");
			ReportParams.CopyTo(Cmd);
			SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();
			sqlDa.Fill(ds);
			return ds;
		}
		#endregion	

		/// .Modified by Cognizant Added for PCR 29 Offshore from here
		
		//Q4-Retrofit.Ch2: START -  i). PaySearchVoidReceipts: New Web method,It returns a Dataset with the list of the Receipts (containing HUON products) with details matching the input search criteria(Used for searching).

		#region PaySearchVoidReceipts
		/// <summary>
		/// Returns a list of the Receipts with details matching the input search criteria.
		/// </summary>
		/// <param name="SearchFor">Search Criteria</param>
		/// <returns>Dataset containing the Search Results</returns>
		[WebMethod(Description="Returns a list of the Receipts with details matching the input search criteria",BufferResponse=true)]
		public DataSet PaySearchVoidReceipts(SearchCriteria SearchFor) 
		{
			// .Modified by Cognizant based on  SP changes
			SqlCommand Cmd = GetCommand("PAY_Search_Void_Receipts");
			SearchFor.CopyTo(Cmd);
			SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();
			sqlDa.Fill(ds);
			return ds;
		}
		#endregion

		
		  //Q4-Retrofit.Ch2 - END

        // Q4-Retrofit.Ch2: START - ii). PayGetVoidReceipt: New WebMethod which returns the Dataset containing the complete Receipt details to void a receipt which matches the input search criteria.			
		
		#region PayGetVoidReceipts
		/// <summary>
		/// Returns the complete Receipt details for voiding a receipt.
		/// </summary>
		/// <param name="SearchFor">The search criteria containing the Receipt Number whose details are required</param>
		/// <returns>Dataset containing the Receipt details for the input Receipt Number</returns>
		[WebMethod(Description="Returns the complete Receipt details for voiding a receipt",BufferResponse=true)]
		public DataSet PayGetVoidReceipt(SearchCriteria SearchFor) 
		{
			// .Modified by Cognizant based on  SP changes
			SqlCommand Cmd = GetCommand("PAY_Get_Void_Receipt");
			SearchFor.CopyTo(Cmd);
			SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
			DataSet ds = new DataSet();
			sqlDa.Fill(ds);
			return ds;
		}
		#endregion
		

             // Refund UI CH1 -END- Added the below method to process the refund operation if it is an credit card/echeck transaction and return the response back.

        
        [WebMethod(Description = "Updates the status of the CPM Refund transactions.")]
        public List<string> RefundCCPayment( string userid,string AppID, string ReceiptNumber, string Sequence_Number, string Message, string Result)
        {

            SqlCommand Cmd = GetCommand("Pay_CC_Void_Payment");
            Cmd.Parameters.Add("@AppID", AppID);
            Cmd.Parameters.Add("@UserID", userid);
            Cmd.Parameters.Add("@ReceiptNumber", ReceiptNumber);
            Cmd.Parameters.Add("@Sequence_Number", Sequence_Number);
            Cmd.Parameters.Add("@Message", Message);
            Cmd.Parameters.Add("@Result", Result);
            SqlDataAdapter sqlDa = new SqlDataAdapter(Cmd);
            DataSet ds = new DataSet();
            sqlDa.Fill(ds, "RefundCCPayments");

            string Flag = ds.Tables["RefundCCPayments"].Rows[0]["Flag"].ToString();
            string Flag_Message = ds.Tables["RefundCCPayments"].Rows[0]["Flag_Message"].ToString();
            string Error_Message = ds.Tables["RefundCCPayments"].Rows[0]["Error_Message"].ToString();
            List<string> Trans_Status = new List<string>(new string[] { Flag, Flag_Message, Error_Message });
            return Trans_Status;

        }
		//Q4-Retrofit.Ch2 - END
				
		//Q4-Retrofit.Ch2: START - iii). PayUpdateVoidStatus: This method is used to void a Receipt containing HUON products based on the input search criteria. It returns an integer: Returns '1' if a Receipt is voided and '0' if the Receipt is not voided.

		#region PayUpdateVoidStatus
		/// <summary>
		/// Voids a Receipt.
		/// </summary>
		/// <param name="SearchFor">The search criteria containing the Receipt Number which has to be voided</param>
		/// <returns>The number of receipts voided - This can be either 1 or 0</returns>
		[WebMethod(Description="Voids a Receipt",BufferResponse=true)]
		public int PayUpdateVoidStatus(SearchCriteria SearchFor) 
		{
			// .Modified by Cognizant based on  SP changes
			SqlCommand Cmd = GetCommand("PAY_Update_Void_Status");
			SearchFor.CopyTo(Cmd);
			Cmd.Parameters[0].Direction = ParameterDirection.ReturnValue;
			Cmd.ExecuteNonQuery();
			return Convert.ToInt16(Cmd.Parameters[0].Value);
		}
		#endregion

		//Q4-Retrofit.Ch2 - END

        //PC PhaseII Changes - Start - Added the method PCVoidFlowCheck to check the Void flow
        #region PC_Void_Flow
        /// <summary>
        /// Decides the Void flow 
        /// </summary>
        /// <param name="Receipt">Unique Receipt Number returned from a Payment Transaction</param>
        /// <returns>Application name</returns>
        [WebMethod(Description = "Checks the given Receipt Number should call PC Void or PT void method.")]
        public string PCVoidFlowCheck(string ReceiptNo)
        {
            string ApplicationId = string.Empty;
            try
            {
                SqlCommand Cmd = GetCommand("Pay_Get_Void_Flow");
                Cmd.Parameters.AddWithValue("@receiptNumber", ReceiptNo);
                SqlParameter AppId = new SqlParameter("@AppName", System.Data.SqlDbType.VarChar, 100);
                AppId.Direction = ParameterDirection.Output;
                Cmd.Parameters.Add(AppId);
                Cmd.ExecuteScalar();
                ApplicationId = AppId.Value.ToString();
            }
            catch(Exception e)
            {
                Logger.Log(e);
                throw new Exception(Constants.PC_ERR_RUNTIME_EXCEPTION);               

            }
            return ApplicationId;
        }
        #endregion
        //PC PhaseII Changes - End - Added the method PCVoidFlowCheck to check the Void flow
	}

}
