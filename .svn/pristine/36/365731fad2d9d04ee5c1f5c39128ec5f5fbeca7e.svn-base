/* 
 * History:
 *	Modified By Cognizant
 *  Code Cleanup Activity for CSR #3937
 * 	06/30/2005   -	Removed the Method SearchOrders from 
 *					InsuranceClasses.Service.Insurance class. Since this Method is no 
 *					longer used in the application.
 *	06/30/2005	-	Removed the Method GetInsReports from 
 *					InsuranceClasses.Service.Insurance class. Since this method is no
 *					longer used in the application.
 *	06/30/2005	-	Removed the Method GetPayTransReports from
 *					InsuranceClasses.Service.Insurance class. Since this method is not
 *					Required any more for the application.
 * 	07/20/2005	-	For changing the web.config entries pointing to database. This changes 
 *					are made as part of CSR #3937 implementation.
 *	CSR#3937.Ch1 :  Added a new proxy class for accessing the PayGetConstants web method
 *  
 * MODIFIED BY COGNIZANT AS PART OF Q4 RETROFIT CHANGES
 * 12/15/2005 Q4-Retrofit.Ch1 :Renamed the Function GetPoesReports to GetBatchReports  
 * 12/15/2005 Q4-Retrofit.Ch2 :Added a new method (ValidateHUONCheckDigit) for validating the check digit for HUON Auto policies
 * 
 * MODIFIED BY COGNIZANT AS A PART OF SR#8434145 ON 06/20/2009
 * SR#8434145.Ch1 : Added a new web method CheckDuplicatePolicy() for checking the duplicate payment type.
 * 11/03/2009  Billing and Payments Quick Hits Project- RFC 48347:Modified method CheckActivePolicy()by cognizant for accepting only policy number  to check in IVR Database and return
 * product type from IVR database.
 * 67811A0  - PCI Remediation for Payment systems CH1 : Added the new Proxy method to invoke the Void payment method to invoke stored procedure to update the transaction status by cognizant on 08/25/2011.
 * 67811A0  - PCI Remediation for Payment systems CH2 : Modified the method input paramters to accept policy prefix as an additional parameters to policy look up in IVR data base by cognizant on 09/27/2011
 * 67811A0  - PCI Remediation for Payment systems CH3  : Added the new invoke method for policy look up in SIS stored procedure by cognizant on 09/26/2011.
 * 67811A0  - PCI Remediation for Payment systems CH4 : Added the method to invoke the new method for regEX validation of the merchant number as part of the security team defect fix
 * ////AZ PAS conversion and PC integration-CH1- Added the below method to invoke the policy detail from the billing summary lookup
* AZ PAS conversion and PC integration-CH2 Added an input parameter named Datasource in the check policy method
 *PC Phase II Changes - CH1 - Added Method GetMemReports to Get Membership Reports to call appropotiate SP
 * MAIG - CH1 - Included the below code to fetch the Location, Agency and USer details from Authentication DB
 */

using System;
using System.Web.Services.Protocols;
using OrderClasses;
using System.Data;

namespace InsuranceClasses.Service
{
    /// <summary>
    /// Proxy class for accessing the Insurance Web service.
    /// </summary>
    public class Insurance : CSAAWeb.Web.SoapHttpClientProtocol
    {

        /// <summary>
        /// Creates the records for the order in the appropriate tables.  Returns
        /// the orderID.
        /// </summary>
        /// <param name="Order">All the information about the order.</param>
        /// <returns>An ID associated with this transaction.</returns>
        [SoapDocumentMethodAttribute]
        public int Begin_Transaction(InsuranceInfo Order)
        {
            return (int)Invoke(new object[] { Order })[0];
        }

        /// <summary>
        /// Completes the transaction started by Begin_Insurance_Transaction
        /// </summary>
        /// <param name="OrderID">The ID of the order returned by Begin_Insurance_Transation</param>
        /// <param name="AuthCode">Auth code returned by Payment Processor.</param>
        [SoapDocumentMethodAttribute]
        public void Complete_Transaction(int OrderId, string AuthCode, string RequestId, string ReceiptNumber)
        {
            Invoke(new object[] { OrderId, AuthCode, RequestId, ReceiptNumber });
        }

        /// <summary>
        /// Returns a recordset of Product Types.
        /// </summary>
        [SoapDocumentMethodAttribute]
        public DataSet ProductTypes()
        {
            return (DataSet)Invoke()[0];
        }

        /// <summary>
        /// Created by Cognizant on 12/10/2004 
        /// Returns a DataSet of all Insurance Product types containing both IIB and WU products.
        /// </summary>
        [SoapDocumentMethodAttribute]
        public DataSet GetAllProductTypes()
        {
            return (DataSet)Invoke()[0];
        }

        /// <summary>
        /// Returns a recordset of Revenue Types.
        /// </summary>
        [SoapDocumentMethodAttribute]
        public DataSet RevenueTypes()
        {
            return (DataSet)Invoke()[0];
        }


        [SoapDocumentMethodAttribute]
        public DataSet RevenueTypesByRole(string CurrentUser, string AppName)
        {
            return (DataSet)Invoke(new object[] { CurrentUser, AppName })[0];
        }

        /// Returns a recordset of Report Types.
        /// </summary>
        [SoapDocumentMethodAttribute]
        public DataSet ReportTypes(string AccessType)
        {
            return (DataSet)Invoke(new object[] { AccessType })[0];
        }
       //MAIG - CH1 - BEGIN - Included the below code to fetch the Location, Agency and USer details from Authentication DB
        /// Returns a recordset of Location.
        /// </summary>
        [SoapDocumentMethodAttribute]
        public DataSet Location(string AgencyID)
        {
            return (DataSet)Invoke(new object[] { AgencyID })[0];
        }
        /// Returns a recordset of User.
        /// </summary>
        [SoapDocumentMethodAttribute]
        public DataSet User(string AgencyID)
        {
            return (DataSet)Invoke(new object[] { AgencyID })[0];
        }
        /// Returns a recordset of Agency.
        /// </summary>
        [SoapDocumentMethodAttribute]
        public DataSet Agency(string AgencyID)
        {
            return (DataSet)Invoke(new object[] { AgencyID })[0];
        }
       //MAIG - CH1 - END - Included the below code to fetch the Location, Agency and USer details from Authentication DB

        /// <summary>
        /// Validates the policy's check digit.
        /// </summary>
        [SoapDocumentMethodAttribute]
        public bool ValidateCheckDigit(string Policy)
        {
            return (bool)Invoke(new object[] { Policy })[0];
        }

        /// <summary>
        /// Get Insurance Reports matching provided criteria
        /// </summary>
        [SoapDocumentMethodAttribute]
        public System.Data.DataSet GetInsuranceReports(ReportCriteria ReportParams)
        {
            return (System.Data.DataSet)Invoke(new object[] { ReportParams })[0];
        }
        //67811A0  - PCI Remediation for Payment systems CH4-START : Added the method to invoke the new method for regEX validation of the merchant number as part of the security team defect fix
        [SoapDocumentMethodAttribute]
        public bool ValidateMerchantNum(string MerchantRefNum)
        {
            return (bool)Invoke(new object[] { MerchantRefNum })[0];
        }
        //67811A0  - PCI Remediation for Payment systems CH4-END : Added the method to invoke the new method for regEX validation of the merchant number as part of the security team defect fix
        /// <summary>
        /// Code Modified by Cognizant on 05/20/2004
        /// Invoke ReceiptReIssue 
        /// </summary>
        /// <returns>Index of the extended transaction.</returns>
        [SoapDocumentMethodAttribute]
        public int ReIssueReceipt(ReportCriteria ReportParams, string NewReceiptNumber)
        {
            return (int)Invoke(new object[] { ReportParams, NewReceiptNumber })[0];
        }

        /// <summary>
        /// Invoke ReceiptReIssue 
        /// </summary>
        /// <returns>Index of the extended transaction.</returns>
        [SoapDocumentMethodAttribute]
        public int UpdateStatus(ReportCriteria ReportParams, TurninClasses.ArrayOfReceipt Receipts, string Action)
        {
            return (int)Invoke(new object[] { ReportParams, Receipts, Action })[0];
        }

        //Code Added by Cognizant on 07/19/2004
        /// <summary>
        /// To Get Revenue Types based on product
        /// </summary>
        [SoapDocumentMethodAttribute]
        public DataSet RevenueTypesByProduct(string ProductTypeCode)
        {
            return (DataSet)Invoke(new object[] { ProductTypeCode })[0];
        }


        //12/15/2005 Removed as a part of Q4 Retrofit
        /* //Code Added by Cognizant on 07/19/2004
         * /// <summary>
         * /// Code Added by Cognizant on 07/07/2004
         * /// Returns a dataset of POES Reports
         * /// </summary>
         * [SoapDocumentMethodAttribute]
         * public System.Data.DataSet GetPoesReports(ReportCriteria ReportParams) 
         * {
         * 	return (System.Data.DataSet)Invoke(new object[] {ReportParams})[0];
         * }
         */

        //Q4-Retrofit.Ch1-START: Renamed the reference GetBatchReports from GetPoesReports

        /// <summary>
        /// The function name modified to GetBatchReports from GetPoesReports
        /// Returns a dataset of POES/HUON Batch Reports depends on the report type
        /// </summary>
        [SoapDocumentMethodAttribute]
        public System.Data.DataSet GetBatchReports(ReportCriteria ReportParams)
        {
            return (System.Data.DataSet)Invoke(new object[] { ReportParams })[0];
        }
        //Q4-Retrofit.Ch1-END

        // START - Code added by COGNIZANT - 08/07/2004 - Function for calling the New Transaction Search
        /// <summary>
        /// Search for insurance and membership transactions matching provided criteria
        /// </summary>
        [SoapDocumentMethodAttribute]
        public System.Data.DataSet NewSearchOrders(SearchCriteria SearchFor)
        {
            return (System.Data.DataSet)Invoke(new object[] { SearchFor })[0];
        }
        [SoapDocumentMethodAttribute]
        public int PayUpdateVoidStatus(SearchCriteria SearchFor)
        {
            return (int)Invoke(new object[] { SearchFor })[0];
        }

        // END

        /// <summary>
        /// Complete a transaction that has been extended beyond the web service boundary.
        /// </summary>
        /// <param name="Commit">True to commit the transaction, false to rollback.</param>
        /// <param name="Index">
        /// The index of the transaction.  Must have been provided by the
        /// web method that created the transaction, through a call to SetExtendedTransaction()
        /// </param>
        [SoapDocumentMethodAttribute]
        public void CompleteTransaction(int Index, bool Commit)
        {
            Invoke(new object[] { Index, Commit });
        }
        /// <summary>
        ///  Code Added by Cognizant on 10/18/2004
        /// Invoke GetUploadType WebMethod
        /// </summary>
        /// <param name="ProductType">The ProductType for the corresponding Product</param> 
        /// <returns>UploadType</returns> 
        /// .Modified by Cognizant changed as on Tier1
        [SoapDocumentMethodAttribute]
        public string GetUploadType(string ProductType)
        {
            return (string)Invoke(new object[] { ProductType })[0];
        }
        ///<summary>Added by Cognizant on 11/2/2004 for Checking Active Policy 
        ///<param name="ProductType">The ProductType for the corresponding Product</param> 
        ///<param name="Policy">The Policy Number for the corresponding Product</param>
        ///<returns>Active or Not(Boolean)</returns>  
        /// .Modified by Cognizant changed as on Tier1
        /// Modified by cognizant as a part of Billing and Payments Quick Hits Project- RFC 48347  on 3Nov 2009.
        /// To take only policy number and check whether active or not and get product type.

        [SoapDocumentMethodAttribute]
        public DataSet CheckActivePolicy(string Policy)
        {
            object[] Result = Invoke(new Object[] { Policy });
            return (DataSet)Result[0];
        }
        ///<summary>Added by Cognizant on 09/17/2009 for Checking Active IVR Policy for payment
        ///<param name="Policy">The Policy Number for the corresponding Product</param>
        ///<returns>Data set with duplicate payment record</returns>
        ///
        //	67811A0  - PCI Remediation for Payment systems CH2 Start : Modified the method input paramters to accept policy prefix as an additional parameters to policy look up in IVR data base by cognizant on 09/27/2011
        [SoapDocumentMethodAttribute]
        public DataSet CheckActiveIVRPolicy(string Policy, string Policy_Prefix)
        {
            object[] Result = Invoke(new Object[] { Policy, Policy_Prefix });

            return (DataSet)Result[0];
        }
        //	67811A0  - PCI Remediation for Payment systems CH2 END : Modified the method input paramters to accept policy prefix as an additional parameters to policy look up in IVR data base by cognizant on 09/27/2011
        //	67811A0  - PCI Remediation for Payment systems CH3 Start : Added the new invoke method for policy look up in SIS stored procedure by cognizant on 09/26/2011.
        [SoapDocumentMethodAttribute]
        public DataTable CheckActiveSISPolicy(string Policy)
        {
            object[] Result = Invoke(new Object[] { Policy });

            return (DataTable)Result[0];
        }
        //	67811A0  - PCI Remediation for Payment systems CH3 END : Added the new invoke method for policy look up in SIS stored procedure by cognizant on 09/26/2011.
        ////AZ PAS conversion and PC integration-Ch1 Added the below method to invoke the policy detail from the billing summary lookup
        //AZ PAS conversion and PC integration-CH2 Added an input parameter named Datasource in the check policy method
        [SoapDocumentMethodAttribute]
        public DataTable CheckPolicy(string Policy, string Productcode, string userID, string Datasource)
        {
            object[] Result = Invoke(new Object[] { Policy, Productcode, userID, Datasource });

            return (DataTable)Result[0];
        }
        ////AZ PAS conversion and PC integration- Added the below method to invoke the policy detail from the billing summary lookup
        /////<summary>SR#8434145.Ch1 - START: Added by Cognizant on 06/20/2009 for Checking Duplicate Policy for payment
        /////<param name="ProductTypeCode">The ProductType code for the corresponding Product</param> 
        /////<param name="Policy">The Policy Number for the corresponding Product</param>
        /////<param name="Amount">The Amount  for the corresponding policy</param>
        /////<returns>Data set with duplicate payment record</returns>
        //[SoapDocumentMethodAttribute]
        //public DataSet CheckDuplicatePolicy(string Policy, string ProductTypeCode, decimal Amount)
        //{
        //    object[] Result = Invoke(new Object[] { Policy, ProductTypeCode, Amount });

        //    return (DataSet)Result[0];
        //}
        //SR#8434145.Ch1 - END
        /// <summary>
        /// Used to Get Application Product Types 
        /// .Modified by Cognizant Added from PCR 29 Offshore
        /// </summary>
        [SoapDocumentMethodAttribute]
        public DataSet ApplicationProductTypes(int AppId)
        {
            return (DataSet)Invoke(new object[] { AppId })[0];
        }

        /// <summary>
        /// Used to Get PayTrans Report Types 
        /// </summary>
        /// .Modified by Cognizant Added from PCR 29 Offshore
        [SoapDocumentMethodAttribute]
        public DataSet PayTransReportTypes()
        {
            return (DataSet)Invoke()[0];
        }

        /// <summary>
        /// Get Application ID 
        /// </summary>
        /// .Modified by Cognizant based on new SP changes
        [SoapDocumentMethodAttribute]
        public System.Data.DataSet GetApplications()
        {
            return (System.Data.DataSet)Invoke()[0];
        }

        // START - Code added by COGNIZANT - 07/08/2005(CRS #3937) - Function for calling the Manual Update Search
        /// <summary>
        /// Search for insurance and membership transactions matching provided criteria
        /// </summary>
        [SoapDocumentMethodAttribute]
        public System.Data.DataSet ManualUpdateSearch(SearchCriteria SearchFor)
        {
            return (System.Data.DataSet)Invoke(new object[] { SearchFor })[0];
        }
        // END

        // START - Code added by COGNIZANT - 07/08/2005(CRS #3937) - Function for calling the Manual Update
        /// <summary>
        /// Update the Status,Rep DO and User Name for insurance and membership transactions
        /// </summary>
        [SoapDocumentMethodAttribute]
        public System.Int32 ManualUpdate(string ReceiptNbr, string status, string currentUser, string DO, string userName)
        {
            return (System.Int32)Invoke(new object[] { ReceiptNbr, status, currentUser, DO, userName })[0];
        }
        // END

        //CSR#3937.Ch1 : START - Added a new proxy class for accessing the PayGetConstants web method
        /// <summary>
        /// Get Constants from PAY_Constants table for web.config changes
        /// </summary>
        [SoapDocumentMethod]
        public DataSet PayGetConstants()
        {
            return (DataSet)Invoke()[0];
        }
        //CSR#3937.Ch1 : END - Added a new proxy class for accessing the PayGetConstants web method

        //Q4-Retrofit.Ch2-START :Added a new method 'ValidateHUONCheckDigit' to invoke the web method to validate the HUON Auto policies
        #region ValidateHUONCheckDigit
        /// <summary>
        /// Validates the check digit for HUON Auto policies.
        /// </summary>
        [SoapDocumentMethodAttribute]
        public bool ValidateHUONCheckDigit(string Policy)
        {
            return (bool)Invoke(new object[] { Policy })[0];
        }
        #endregion
        //Q4-Retrofit.Ch2-END
        // 67811A0  - PCI Remediation for Payment systems CH1 START : Added the new Proxy method to invoke the Void payment method to invoke stored procedure to update the transaction status by cognizant on 08/25/2011.
        
        public VoidPayments VoidPayment(string UserId, string ReceiptNumber, String AppId)
        {
            return (VoidPayments)Invoke(new object[] { UserId, AppId, ReceiptNumber })[0];
        }
        
        // 67811A0  - PCI Remediation for Payment systems CH1 END : Added the new Proxy method to invoke the Void payment method to invoke stored procedure to update the transaction status by cognizant on 08/25/2011.
    }
}
