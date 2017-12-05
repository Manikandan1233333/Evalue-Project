/* 
 * History:
 *	Modified By Cognizant
 *  Code Cleanup Activity for CSR #3937
 * 	06/29/2005   -	Removed the Method ApplicationProductTypes from 
 *					TurninClasses.Service.Turnin class. Since this Method is no 
 *					longer used in the application
 * 	06/29/2005   -	Removed the Method ApplicationProductTypes from 
 *					TurninClasses.InternalService.Turnin class. Since this Method 
 *					is no longer used in the application
 *	06/30/2005	-	Removed the Method RevenueTypesByProduct from 
 *					TurninClasses.InternalService.Turnin class. Since this Method 
 *					is no longer used in the application.
 *	06/30/2005	-	Removed the Method RevenueTypesByProduct from	
 *					TurninClasses.Service.Turnin class. Since this Method is no
 *					longer used in the application.
 *	06/30/2005	-	Removed the Method GetReceiptNumber from 
 *					TurninClasses.InternalService.Turnin class. Since this Method 
 *					is no longer used in the application.
 *	06/30/2005	-	Removed the Method GetReceiptNumber from 
 *					TurninClasses.Service.Turnin class. Since this Method which is 
 *					invoking the GetReceiptNumber web method is no longer used.
 *	07/05/2005	-	Removed the Method PayTransReportTypes from 
 *					TurninClasses.Service.Turnin class. Since this method is no
 *					longer used in the application.
 *	07/05/2005	-	Removed the Method PayTransReportTypes from 
 *					TurninClasses.InternalService.Turnin class. Since this method
 *					is no longer called anywhere in the application.
 * 
 *		--> Modified by COGNIZANT as part of Q4-retrofit <--
 * 
 * 12/1/2005 Q4-Retrofit.Ch1
 *			  Renamed the method 'SalesRepCashierWorkflow' to PayWorkflowReport' in the classes TurninClasses.InternalService.Turnin & TurninClasses.Service.Turnin 
 *			  because the corresponding web method in TurninWebservice.cs has been renamed.
 * 
 * 12/1/2005 Q4-Retrofit.Ch2
 *			  Added Three new method in the classes TurninClasses.InternalService.Turnin & TurninClasses.Service.Turnin 
 *			  to invoke the corresponding new web methods in TurninWebService.cs(for Finance Users):
			  i). PaySearchVoidReceipts
			  ii). PayGetVoidReceipt
			  iii). PayUpdateVoidStatus
 *          Refund UI CH1 - Added the below dataset to process the refund operation if it is an credit card/echeck transaction and return the response back.
 *          Refund UI CH2 - Added the below method to process the refund operation if it is an credit card/echeck transaction and return the response back.
 * PC Phase II Changes -  Added the method PCVoidFlowCheck to check the Void flow 
 */
using System;
using System.Web.Services.Protocols;
using OrderClasses;
using System.Data;
using System.Collections.Generic;

namespace TurninClasses.Service {
	/// <summary>
	/// Proxy class for accessing the Turnin Web service.
	/// </summary>
	public class Turnin  {
		private InternalService.Turnin T = new InternalService.Turnin();

		#region Workflow
		/// <summary>
		/// Invoke ReceiptReIssue 
		/// </summary>
		public string ReceiptReIssue(ReportCriteria ReportParams) {
			return T.ReceiptReIssue(ReportParams);
		}

		/// <summary>
		/// Invoke ReceiptVoid 
		/// </summary>
		public int ReceiptVoid(ReportCriteria ReportParams) {
			return T.UpdateStatus(ReportParams, "Void");
		}

		/// <summary>
		/// Updates the status of the receipts which have been turned in. 
		/// <param name="ReceiptParams">The Receipt List which has to be turned in and the Current User</param>
		/// </summary>
		public void ReceiptsTurnIn(ReportCriteria ReportParams) {
			T.UpdateStatus(ReportParams, "Turnin");
		}


		/// <summary>
		/// Approves all the selected Receipt Numbers which are passed as comma Seperated Values
		/// </summary>
		public void CashierApprove(ReportCriteria ReportParams) {
			T.UpdateStatus(ReportParams, "Approve");
		}

		/// <summary>
		/// Rejects all the selected Receipt Numbers which are passed as comma Seperated Values
		/// </summary>
		public void CashierReject(ReportCriteria ReportParams) {
			T.UpdateStatus(ReportParams, "Reject");
		}
		#endregion
		/// <summary>
		/// Get Turnin Reports matching provided criteria
		/// </summary>
		public System.Data.DataSet GetTurninReports(ReportCriteria ReportParams) {
			return T.GetTurninReports(ReportParams);
		}

		/// <summary>
		/// Returns a dataset of Receiptdetails.
		/// </summary>
		public System.Data.DataSet GetReceiptDetails(ReportCriteria ReportParams) {
			return T.GetReceiptDetails(ReportParams);
		}	

		//Q4-Retrofit.Ch2: START - New method PaySearchVoidReceipts is added to the classes TurninClasses.InternalService.Turnin & TurninClasses.Service.Turnin to invoke the corresponding new web methods in TurninWebService.cs.

		/// <summary>
		/// Returns a list of the Receipts with details matching the input search criteria.
		/// </summary>
		/// <param name="SearchFor">The Search Criteria</param>
		/// <returns>Dataset containing the Search Results</returns>
		public System.Data.DataSet PaySearchVoidReceipts(SearchCriteria SearchFor) 
		{
			return T.PaySearchVoidReceipts(SearchFor);
		}

		//Q4-Retrofit.Ch2 - END

		//Q4-Retrofit.Ch2: START - New method PayGetVoidReceipt is added to the classes TurninClasses.InternalService.Turnin & TurninClasses.Service.Turnin to invoke the corresponding new web methods in TurninWebService.cs.

		/// <summary>
		/// Returns the complete Receipt details for voiding a receipt.
		/// </summary>
		/// <param name="SearchFor">The search criteria containing the Receipt Number whose details are required</param>
		/// <returns>Dataset containing the Receipt details for the input Receipt Number</returns>
        // Refund UI CH1 -START- Added the below dataset to process the refund operation if it is an credit card/echeck transaction and return the response back.
        public System.Data.DataSet PayGetVoidReceipt(SearchCriteria SearchFor) 
		{
			return T.PayGetVoidReceipt(SearchFor);
		}
        // Refund UI CH1 -END- Added the below dataset to process the refund operation if it is an credit card/echeck transaction and return the response back.


        public List<string> PayPaymentRefund(string ReceiptNumber,string userid)
        {
            return T.PayPaymentRefund(ReceiptNumber,userid);
        }


		//Q4-Retrofit.Ch2 - END

        //Q4-Retrofit.Ch2: START - New method  PayUpdateVoidStatus is added to the classes TurninClasses.InternalService.Turnin & TurninClasses.Service.Turnin to invoke the corresponding new web methods in TurninWebService.cs.

		/// <summary>
		/// Voids a receipt
		/// </summary>
		/// <param name="SearchFor">The search criteria containing the Receipt Number which has to be voided<</param>
		/// <returns>The number of receipts voided - This can be either 1 or 0</returns>
		public int PayUpdateVoidStatus(SearchCriteria SearchFor) 
		{
			return T.PayUpdateVoidStatus(SearchFor);
		}

		//Q4-Retrofit.Ch2 - END


		/// <summary>
		/// Gets the report types for a particular screen and role
		/// </summary>
		/// <param name="CurrentUser">Currently logged in User</param>
		/// <param name="AccessType">An identifier for the calling screen (R for Reports and W for WorkFlow)</param>
		/// <returns>Dataset of Report Types for a particular screen and role</returns>

		public System.Data.DataSet GetTurnInReportTypes(string CurrentUser,string AccessType) 
		{
			return T.GetTurnInReportTypes(CurrentUser,AccessType);
		}


		/// <summary>
		/// Gets  the statuses that need to displayed for a particular report type and for a particular user based on his role. 
		/// </summary>
		/// <param name="CurrentUser">Currently logged in User</param>
		/// <param name="ReportType">The ID for the current report</param>
		/// <returns>Dataset of status ids and status descriptions available for the current report type</returns>
		public System.Data.DataSet GetStatus(string CurrentUser,string ReportType) {
			return T.GetStatus(CurrentUser,ReportType);
		}

		//Q4-Retrofit.Ch1: START - Renamed the method 'SalesRepCashierWorkflow' to PayWorkflowReport' in the classes TurninClasses.InternalService.Turnin & TurninClasses.Service.Turnin because the corresponding web method in TurninWebservice.cs has been renamed.
		
		/// <summary>
		/// The Function is used in Sales Turn In,Cashier Reconciliation,Finance and Manager Reports
		/// Based on the chosen criteria, the details will be returned.
		/// </summary>
		/// <param name="ReportParams">The Criteria for obtaining data</param>
		/// <returns>Returns the recordset for a given criteria</returns>
		public System.Data.DataSet PayWorkflowReport(ReportCriteria ReportParams) 
		{
			return T.PayWorkflowReport(ReportParams);
		}

		//Q4-Retrofit.Ch1 - END

		/// <summary>
		/// Get Star Reports matching provided criteria
		/// </summary>
		public System.Data.DataSet GetPayTransReports(ReportCriteria ReportParams) 
		{
			return T.GetPayTransReports(ReportParams);
		}
        //PC PhaseII Changes - Start - Added the method PCVoidFlowCheck to check the Void flow
        public string PCVoidFlowCheck(string ReceiptNo)
        {
            return T.PCVoidFlowCheck(ReceiptNo);
        }
        //PC PhaseII Changes - End - Added the method PCVoidFlowCheck to check the Void flow
	}
}

	#region InternalTurnin
namespace TurninClasses.InternalService {
	/// <summary>
	/// Proxy class for accessing the Turnin Web service.
	/// </summary>
	public class Turnin : CSAAWeb.Web.SoapHttpClientProtocol {

		/// <summary>
		/// Get Turnin Reports matching provided criteria
		/// </summary>
		[SoapDocumentMethodAttribute]
		public System.Data.DataSet GetTurninReports(ReportCriteria ReportParams) {
			return (System.Data.DataSet)Invoke(new object[] {ReportParams})[0];
		}

		/// <summary>
		/// Code Added by Cognizant on 05/20/2004
		/// Returns a dataset of Receiptdetails.
		/// </summary>
		[SoapDocumentMethodAttribute]
		public System.Data.DataSet GetReceiptDetails(ReportCriteria ReportParams) {
			return (System.Data.DataSet)Invoke(new object[] {ReportParams})[0];
		}	
		// Code Added by Cognizant on 05/20/2004

		/// <summary>
		/// Code Modified by Cognizant on 05/20/2004
		/// Invoke ReceiptReIssue 
		/// </summary>
		[SoapDocumentMethodAttribute]
		public string ReceiptReIssue(ReportCriteria ReportParams) {
			return (string)Invoke(new object[] {ReportParams})[0];
		}
		// Code Added by Cognizant on 05/20/2004

		/// <summary>
		/// Code Added by JOM on 11/10/2004
		/// Invoke UpdateStatus
		/// </summary>
		[SoapDocumentMethodAttribute]
		public int UpdateStatus(ReportCriteria ReportParams, string Action) {
			return (int)Invoke(new object[] {ReportParams, Action})[0];
		}
		// Code Added by JOM on 11/10/2004

		///Changed by Cognizant on 05/18/04 
		/// <summary>
		/// Gets the report types for a particular screen and role
		/// </summary>
		/// <param name="CurrentUser">Currently logged in User</param>
		/// <param name="AccessType">An identifier for the calling screen (R for Reports and W for WorkFlow)</param>
		/// <returns>Dataset of Report Types for a particular screen and role</returns>

		[SoapDocumentMethodAttribute]
		public System.Data.DataSet GetTurnInReportTypes(string CurrentUser,string AccessType) {
			return (System.Data.DataSet)Invoke(new object[] {CurrentUser,AccessType})[0];
		}

		//Q4-Retrofit.Ch2: START - New method PaySearchVoidReceipts is added to the classes TurninClasses.InternalService.Turnin & TurninClasses.Service.Turnin to invoke the corresponding new web methods in TurninWebService.cs.

		/// <summary>
		/// Returns a list of the Receipts with details matching the input search criteria.
		/// </summary>
		/// <param name="SearchFor">Search Criteria</param>
		/// <returns>Dataset containing the Search Results</returns>
		[SoapDocumentMethodAttribute]
		public System.Data.DataSet PaySearchVoidReceipts(SearchCriteria SearchFor) 
		{
			return (System.Data.DataSet)Invoke(new object[] {SearchFor})[0];			
		}

        // Refund UI CH2 -START- Added the below method to process the refund operation if it is an credit card/echeck transaction and return the response back.
        [SoapDocumentMethodAttribute]
        public List<string> PayPaymentRefund(string ReceiptNumber,string userid)
        {
            return (List<string>)Invoke(new object[] { ReceiptNumber,userid })[0];
        }
        // Refund UI CH2 -END- Added the below method to process the refund operation if it is an credit card/echeck transaction and return the response back.



		//Q4-Retrofit.Ch2 - END
		 

		//Q4-Retrofit.Ch2: START - New method PayGetVoidReceipt is added to the classes TurninClasses.InternalService.Turnin & TurninClasses.Service.Turnin to invoke the corresponding new web methods in TurninWebService.cs.

		/// </summary>
		/// <param name="SearchFor">The search criteria containing the Receipt Number whose details are required</param>
		/// <returns>Dataset containing the Receipt details for the input Receipt Number</returns>
		[SoapDocumentMethodAttribute]
		public System.Data.DataSet PayGetVoidReceipt(SearchCriteria SearchFor) 
		{
			return (System.Data.DataSet)Invoke(new object[] {SearchFor})[0];			
		}

		//Q4-Retrofit.Ch2 - END
		 

		//Q4-Retrofit.Ch2: START - New method  PayUpdateVoidStatus is added to the classes TurninClasses.InternalService.Turnin & TurninClasses.Service.Turnin to invoke the corresponding new web methods in TurninWebService.cs.
			
		/// <summary>
		/// Voids a receipt
		/// </summary>
		/// <param name="SearchFor">The search criteria containing the Receipt Number which has to be voided</param>
		/// <returns>The number of receipts voided - This can be either 1 or 0</returns>
		[SoapDocumentMethodAttribute]
		public int PayUpdateVoidStatus(SearchCriteria SearchFor) 
		{
			return (int)Invoke(new object[] {SearchFor})[0];			
		}

		//Q4-Retrofit.Ch2 - END
		


		///Changed by Cognizant on 05/18/04 
		/// <summary>
		/// Gets  the statuses that need to displayed for a particular report type and for a particular user based on his role. 
		/// </summary>
		/// <param name="CurrentUser">Currently logged in User</param>
		/// <param name="ReportType">The ID for the current report</param>
		/// <returns>Dataset of status ids and status descriptions available for the current report type</returns>
		[SoapDocumentMethodAttribute]
		public System.Data.DataSet GetStatus(string CurrentUser,string ReportType) {
			return (System.Data.DataSet)Invoke(new object[] {CurrentUser,ReportType})[0];
		}

		
		//Q4-Retrofit.Ch1: START - Renamed the method 'SalesRepCashierWorkflow' to PayWorkflowReport' in the classes TurninClasses.InternalService.Turnin & TurninClasses.Service.Turnin because the corresponding web method in TurninWebservice.cs has been renamed.

		/// <summary>
		/// The Function is used in Sales Turn In,Cashier Reconciliation,Finance and Manager Reports
		/// Based on the chosen criteria, the details will be returned.
		/// </summary>
		/// <param name="ReportParams">The Criteria for obtaining data</param>
		/// <returns>Returns the recordset for a given criteria</returns>
		[SoapDocumentMethodAttribute]
		public System.Data.DataSet PayWorkflowReport(ReportCriteria ReportParams) 
		{
			return (System.Data.DataSet)Invoke(new object[] {ReportParams})[0];
		}

		//Q4-Retrofit.Ch1 - END
		 

		//Code Added by Cognizant on 07/06/2004
		/// <summary>
		/// Get Star Reports matching provided criteria
		/// </summary>
		[SoapDocumentMethodAttribute]
		public System.Data.DataSet GetPayTransReports(ReportCriteria ReportParams) {
			return (System.Data.DataSet)Invoke(new object[] {ReportParams})[0];
		}
		//Code Added by Cognizant on 07/06/2004

        //Begin PC Phase II changes CH1 - Added to retrieve the Application name which decides Void flow
        /// <summary>
        /// Retrieves the Application name which decides the Void flow
        /// </summary>
        /// <param name="ReceiptNo">Unique Receipt Number returned from a Payment Transaction</param>
        /// <returns>Application name</returns>
        [SoapDocumentMethodAttribute]
        public string PCVoidFlowCheck(string ReceiptNo)
        {
            return (string)Invoke(new object[] { ReceiptNo })[0];
        }
        //End PC Phase II changes CH1 - Added to retrieve the Application name which decides Void flow
	}

	#endregion
}
