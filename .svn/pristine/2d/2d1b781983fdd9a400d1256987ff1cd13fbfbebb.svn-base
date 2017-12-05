/*
History: Created by cognizant on 7/2/2012
 * AZ PAS Conversion and PC integration - Creted this page to fetch the details of the policy number from the billing summary lookup of the Payment Central.
 * AZ PAS Conversion and PC integration -CH1 - Modified the below code to read the error code from the payment central and assign the error message accordingly.Modified the mapping for due amount and total amount
 * AZ PAS Conversion and PC integration -CH2 - Modified the below code to read the error code from the payment central and assign the error message accordingly.
 * //AZ PAS conversion and PC integration-CH3 Added an input parameter named Datasource in the check policy method
 * AZ PAS Conversion and PC integration -CH4 Added the below code to fetch teh product code from service reponse by cognizant on 10/05/2012.
 * PC Phase II -  CH1 -  Modified the code to remap the total amount and minimum amount due.
 * PC Phase II Changes CH2 - Added the below code for updating the Billing Summary Lookup Service by including the Billingplan,PaymentPlan,Autopay,TermEffectiveDate & TermExpirationDate fields
 * MAIG - CH1 - Included the optional field transaction type as part of request
 * MAIG - CH2 - BEGIN - Included the additional fields to retrieve the Validation results in RPBS Service and also logic to handle more than one rows if returned from Rpbs Service. Cleanup of code is done, by moving strings to Constants.
 * CHG0118686 - PRB0045696 - Message Changes - Start - FR1,FR2,FR3,FR4,FR6
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using CSAAWeb.AppLogger;
using System.Reflection;
using CSAAWeb;
namespace OrderClassesII
{
    public class BillingLookUp
    {
        public DataTable checkPolicy(string policyNumber, string productCode, string userID, string Datasource)
        {
            try
            {

                DateTime defaultDateTime = new DateTime();
                BillingSummaryLookup.retrievePolicyBillingSummariesRequest input = new BillingSummaryLookup.retrievePolicyBillingSummariesRequest();
                BillingSummaryLookup.retrievePolicyBillingSummariesResponse output = new BillingSummaryLookup.retrievePolicyBillingSummariesResponse();
                input.policies = new BillingSummaryLookup.PolicyProductSource[1];
                //AZ PAS conversion and PC integration-CH3 - Added an input parameter named Datasource in the check policy method
                input.policies[0] = new BillingSummaryLookup.PolicyProductSource
                {
                    type = productCode,
                    policyNumber = policyNumber,
                    dataSource = Datasource,
                };
                //MAIG - CH1 - BEGIN - Included the optional field transaction type as part of request
                input.applicationContext = new OrderClassesII.BillingSummaryLookup.ApplicationContext();
                input.applicationContext.transactionType = CSAAWeb.Constants.PC_REQ_TRANSACTION_TYPE;
                input.applicationContext.application = Config.Setting(CSAAWeb.Constants.PC_ApplicationContext_Payment_Tool);
                input.applicationContext.address = CSAAWeb.AppLogger.Logger.GetIPAddress();
                //MAIG - CH1 - END - Included the optional field transaction type as part of request
                BillingSummaryLookup.RetrievePolicyBillingSummariesService service = new BillingSummaryLookup.RetrievePolicyBillingSummariesService();
                //Added the below code to log the input request and output response of the Payment central billing summary lookup
                if (Config.Setting("Logging.BillSummary") == "1")
                {
                    System.Xml.Serialization.XmlSerializer serializerReq = new System.Xml.Serialization.XmlSerializer(input.GetType());
                    StringWriter writerReq = new StringWriter();
                    serializerReq.Serialize(writerReq, input);
                    Logger.Log("Payment request to Payment Central - Billing Summary Request : \r\n" + writerReq);
                }
                output = service.RetrievePolicyBillingSummaries(input);
                if (Config.Setting("Logging.BillSummary") == "1")
                {
                    if (output != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(output.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, output);
                        Logger.Log("Payment response from Payment Central - Billing Summary Request : \r\n" + writerRes);
                    }
                }
                //Added the below code to log the input request and output response of the Payment central billing summary lookup


                string errorCode = "";
                DataTable dtPolicyDetail = new DataTable();

                //MAIG - CH2 - BEGIN - Included the additional fields to retrieve the Validation results in RPBS Service and also logic to handle more than one rows if returned from Rpbs Service. Cleanup of code is done, by moving strings to Constants.
                /*dtPolicyDetail.Columns.Add(CSAAWeb.Constants.PC_BILL_POL_TYPE);

              
                dtPolicyDetail.Columns.Add(CSAAWeb.Constants.PC_BILL_POL_NUMBER);
                dtPolicyDetail.Columns.Add("POL_STATE");
                dtPolicyDetail.Columns.Add(CSAAWeb.Constants.PC_BILLPOL_PREFIX);
                dtPolicyDetail.Columns.Add("COMPANY_ID");
                dtPolicyDetail.Columns.Add("First_Name");
                dtPolicyDetail.Columns.Add("Last_Name");
                dtPolicyDetail.Columns.Add("INS_FULL_NME");
                dtPolicyDetail.Columns.Add(CSAAWeb.Constants.PC_BILL_Due_Date);
                dtPolicyDetail.Columns.Add(CSAAWeb.Constants.PC_BILL_PAYMENT_RESTRICTION);
                dtPolicyDetail.Columns.Add(CSAAWeb.Constants.PC_BILL_Due_Amount);
                dtPolicyDetail.Columns.Add(CSAAWeb.Constants.PC_BILL_Total_Amount);
                dtPolicyDetail.Columns.Add("POL_GROUP");
                dtPolicyDetail.Columns.Add("ERR_MSG");
                dtPolicyDetail.Columns.Add("Status");
                dtPolicyDetail.Columns.Add(CSAAWeb.Constants.PC_BILL_SOURCE_SYSTEM);
                dtPolicyDetail.Columns.Add("Converted_Policynumber");
                dtPolicyDetail.Columns.Add("ErrorCode");
                dtPolicyDetail.Columns.Add("ErrorDescription");
                //Begin PC Phase II Changes CH2 - Added the below code for updating the Billing Summary Lookup Service by including the Billingplan,PaymentPlan,Autopay fields
                dtPolicyDetail.Columns.Add("BillingPlan");
                dtPolicyDetail.Columns.Add(CSAAWeb.Constants.PC_BIL_PaymentPlan);
                dtPolicyDetail.Columns.Add("AutoPay");
                dtPolicyDetail.Columns.Add(CSAAWeb.Constants.PC_BILL_TermEffectiveDate);
                dtPolicyDetail.Columns.Add(CSAAWeb.Constants.PC_TermExpirationDate);
                dtPolicyDetail.Columns.Add("PC_ErrorCode");
                dtPolicyDetail.Columns.Add(CSAAWeb.Constants.PC_StatementDate);
                dtPolicyDetail.Columns.Add(CSAAWeb.Constants.PC_BILL_RenewalFlag);*/
                //End PC Phase II Changes CH2 - Added the below code for updating the Billing Summary Lookup Service by including the Billingplan,PaymentPlan,Autopay fields
                string[] RpbsColumns = {CSAAWeb.Constants.PC_BILL_POL_TYPE, CSAAWeb.Constants.PC_BILL_POL_NUMBER, CSAAWeb.Constants.PC_BILL_POL_STATE, CSAAWeb.Constants.PC_BILLPOL_PREFIX,Constants.PC_BILL_COMPANY_ID,Constants.PC_BILL_FIRST_NAME,Constants.PC_BILL_LAST_NAME,Constants.PC_BILL_INS_FULL_NAME,
                                       CSAAWeb.Constants.PC_BILL_Due_Date,CSAAWeb.Constants.PC_BILL_PAYMENT_RESTRICTION,CSAAWeb.Constants.PC_BILL_Due_Amount,CSAAWeb.Constants.PC_BILL_Total_Amount,
                                       Constants.PC_BILL_POL_GROUP,Constants.PC_BILL_ERR_MSG,Constants.PC_BILL_STATUS,CSAAWeb.Constants.PC_BILL_SOURCE_SYSTEM,Constants.PC_BILL_CONVERTED_POLICYNUMBER,Constants.PC_BILL_ERROR_CODE,Constants.PC_BILL_ERROR_DESCRIPTION,
                                       Constants.PC_BillingPlan,CSAAWeb.Constants.PC_BIL_PaymentPlan,Constants.PC_BILL_AUTO_PAY,CSAAWeb.Constants.PC_BILL_TermEffectiveDate,CSAAWeb.Constants.PC_TermExpirationDate,
                                       Constants.PC_BILL_PC_ERROR_CODE,CSAAWeb.Constants.PC_StatementDate,CSAAWeb.Constants.PC_BILL_RenewalFlag,
                                       CSAAWeb.Constants.PC_BILL_RESTRICTEDTOPAY,CSAAWeb.Constants.PC_BILL_SETUPAUTOPAYREASONCODE,CSAAWeb.Constants.PC_BILL_SETUPAUTOPAYEFFDATE,CSAAWeb.Constants.PC_BILL_UPDATEAUTOPAYREASONCODE,CSAAWeb.Constants.PC_BILL_UPDATEAUTOPAYEFFDATE,CSAAWeb.Constants.PC_BILL_CANCELAUTOPAYREASONCODE,CSAAWeb.Constants.PC_BILL_CANCELAUTOPAYEFFDATE,Constants.PC_BILL_ADDRESS,Constants.PC_BILL_STATUS_DESCRIPTION};
                foreach (string field in RpbsColumns)
                {
                    dtPolicyDetail.Columns.Add(field);
                }
                for (int rows = 0; rows < output.billingSummaries.policyBillingSummary.Length; rows++)
                {
                    DataRow drDataRow = dtPolicyDetail.NewRow();
                    drDataRow[CSAAWeb.Constants.PC_BILL_Total_Amount] = string.Empty;
                    drDataRow[CSAAWeb.Constants.PC_BILL_Due_Amount] = string.Empty;
                    drDataRow[Constants.PC_BILL_ERR_MSG] = string.Empty;
                    drDataRow[Constants.PC_BILL_ERROR_CODE] = "0";
                    drDataRow[Constants.PC_BILL_PC_ERROR_CODE] = string.Empty;
                    drDataRow[CSAAWeb.Constants.PC_BILL_SOURCE_SYSTEM] = string.Empty;

                    if (output.billingSummaries.policyBillingSummary[rows].errorInfo != null)
                    {
                        if (output.billingSummaries.policyBillingSummary[rows].errorInfo.errorMessageText != null)
                        {
                            //AZ PAS Conversion and PC integration -CH2 -Start - Modified the below code to read the error code from the payment central and assign the error message accordingly.
                            if (output.billingSummaries.policyBillingSummary[rows].errorInfo.errorCode != null)
                            {
                                errorCode = output.billingSummaries.policyBillingSummary[rows].errorInfo.errorCode.ToString();
                                if (errorCode == CSAAWeb.Constants.ERR_CODE_BUSINESS_EXCEPTION)
                                {
                                    //CHGXXXXXXX - PRB0045696 - Message Changes - FR1,FR2//
                                    if ((output.billingSummaries.policyBillingSummary[rows].errorInfo.errorMessageText == CSAAWeb.Constants.POLICY_Search_InvalidPolicy) ||
                                  (output.billingSummaries.policyBillingSummary[rows].errorInfo.errorMessageText == CSAAWeb.Constants.POLICY_Search_UnavailablePolicy))
                                    {
                                        drDataRow[Constants.PC_BILL_ERR_MSG] = CSAAWeb.Constants.POLICY_SOAB301_Invalid_Unavailable_Policy;
                                    }
                                    //CHGXXXXXXX - PRB0045696 - Message Changes - FR3 - Information Unavailable//
                                    else if (output.billingSummaries.policyBillingSummary[rows].errorInfo.errorMessageText == CSAAWeb.Constants.POLICY_Search_Status_Unavailable)
                                    {
                                        drDataRow[Constants.PC_BILL_ERR_MSG] = CSAAWeb.Constants.POLICY_SOAB301_Status_Unavailable_Policy;
                                    }
                                    //CHG0118686 - PRB0045696 - Message Changes - Default Error Message//
                                    else
                                    {
                                    drDataRow[Constants.PC_BILL_ERR_MSG] = output.billingSummaries.policyBillingSummary[rows].errorInfo.errorMessageText.ToString();
                                    }
                                    //CHG0118686 - PRB0045696 - Message Changes - End - FR1,FR2//
                                    drDataRow[Constants.PC_BILL_PC_ERROR_CODE] = errorCode;
                                    Logger.Log("ErrorCode: " + errorCode + " Error Message: " + drDataRow["ERR_MSG"] + " Friendly Error message :" + output.billingSummaries.policyBillingSummary[rows].errorInfo.friendlyErrorMessage.ToString() + " PolicyNumber: " + policyNumber + " UserID:" + userID);
                                }
                                else
                                {
                                    drDataRow[Constants.PC_BILL_ERR_MSG] = CSAAWeb.Constants.ERR_BILL_LOOKUP_RUNTIME_EXCEPTION;
                                    drDataRow[Constants.PC_BILL_PC_ERROR_CODE] = errorCode;
                                    Logger.Log("ErrorCode: " + errorCode + " Error Message: " + drDataRow["ERR_MSG"] + " Friendly Error message :" + output.billingSummaries.policyBillingSummary[rows].errorInfo.friendlyErrorMessage.ToString() + " PolicyNumber: " + policyNumber + " UserID:" + userID);
                                }
                                if (drDataRow[Constants.PC_BILL_ERR_MSG] != "" && errorCode == CSAAWeb.Constants.ERR_CODE_BUSINESS_EXCEPTION)
                                {
                                    drDataRow[Constants.PC_BILL_ERROR_CODE] = "1";
                                    drDataRow[Constants.PC_BILL_ERROR_DESCRIPTION] = "Policy Number entered is not an active policy number. The policy number is either in cancellation or is invalid. Do you want to proceed?";

                                }
                                //AZ PAS Conversion and PC integration -CH2- End - Modified the below code to read the error code from the payment central and assign the error message accordingly.

                            }

                        }
                    }

                    if (output.billingSummaries.policyBillingSummary[rows].billingSummary != null)
                    {
                        if (output.billingSummaries.policyBillingSummary[rows].billingSummary.isRestrictedToPay != null)
                        {
                            drDataRow[CSAAWeb.Constants.PC_BILL_RESTRICTEDTOPAY] = output.billingSummaries.policyBillingSummary[rows].billingSummary.isRestrictedToPay.ToString();
                        }
                        if (output.billingSummaries.policyBillingSummary[rows].billingSummary.setupAutoPayEffectiveDate != null)
                        {
                            drDataRow[CSAAWeb.Constants.PC_BILL_SETUPAUTOPAYEFFDATE] = output.billingSummaries.policyBillingSummary[rows].billingSummary.setupAutoPayEffectiveDate.ToString();
                        }
                        if (output.billingSummaries.policyBillingSummary[rows].billingSummary.setupAutoPayReasonCode != null)
                        {
                            drDataRow[CSAAWeb.Constants.PC_BILL_SETUPAUTOPAYREASONCODE] = output.billingSummaries.policyBillingSummary[rows].billingSummary.setupAutoPayReasonCode.ToString();
                        }
                        if (output.billingSummaries.policyBillingSummary[rows].billingSummary.updateAutoPayEffectiveDate != null)
                        {
                            drDataRow[CSAAWeb.Constants.PC_BILL_UPDATEAUTOPAYEFFDATE] = output.billingSummaries.policyBillingSummary[rows].billingSummary.updateAutoPayEffectiveDate.ToString();
                        }
                        if (output.billingSummaries.policyBillingSummary[rows].billingSummary.updateAutoPayReasonCode != null)
                        {
                            drDataRow[CSAAWeb.Constants.PC_BILL_UPDATEAUTOPAYREASONCODE] = output.billingSummaries.policyBillingSummary[rows].billingSummary.updateAutoPayReasonCode.ToString();
                        }
                        if (output.billingSummaries.policyBillingSummary[rows].billingSummary.canceAutoPayEffectiveDate != null)
                        {
                            drDataRow[CSAAWeb.Constants.PC_BILL_CANCELAUTOPAYEFFDATE] = output.billingSummaries.policyBillingSummary[rows].billingSummary.canceAutoPayEffectiveDate.ToString();
                        }
                        if (output.billingSummaries.policyBillingSummary[rows].billingSummary.cancelAutoPayReasonCode != null)
                        {
                            drDataRow[CSAAWeb.Constants.PC_BILL_CANCELAUTOPAYREASONCODE] = output.billingSummaries.policyBillingSummary[rows].billingSummary.cancelAutoPayReasonCode.ToString();
                        }
                    }

                    if (drDataRow[Constants.PC_BILL_ERROR_CODE] == "0" && (errorCode != null || errorCode == ""))
                    {
                        if (output.billingSummaries.policyBillingSummary[rows].policy.policyNumber != null)
                        {
                            drDataRow[CSAAWeb.Constants.PC_BILL_POL_NUMBER] = output.billingSummaries.policyBillingSummary[rows].policy.policyNumber.ToString();
                        }
                        //AZ PAS Conversion and PC integration -CH4 Added the below code to fetch teh product code from service reponse by cognizant on 10/05/2012.
                        if (output.billingSummaries.policyBillingSummary[rows].policy.type != null)
                        {
                            drDataRow[CSAAWeb.Constants.PC_BILL_POL_TYPE] = output.billingSummaries.policyBillingSummary[rows].policy.type.ToString();
                        }
                        if (output.billingSummaries.policyBillingSummary[rows].policy.renewalFlag != null)
                        {
                            drDataRow[CSAAWeb.Constants.PC_BILL_RenewalFlag] = output.billingSummaries.policyBillingSummary[rows].policy.renewalFlag.ToString();
                        }
                        //AZ PAS Conversion and PC integration -CH4 Added the below code to fetch teh product code from service reponse by cognizant on 10/05/2012.
                        if (output.billingSummaries.policyBillingSummary[rows].policy.dataSource != null)
                        {
                            drDataRow[CSAAWeb.Constants.PC_BILL_SOURCE_SYSTEM] = output.billingSummaries.policyBillingSummary[rows].policy.dataSource.ToString();
                        }
                        if (output.billingSummaries.policyBillingSummary[rows].policy.riskState != null)
                        {
                            drDataRow[Constants.PC_BILL_POL_STATE] = output.billingSummaries.policyBillingSummary[rows].policy.riskState.ToString();
                        }
                        if (output.billingSummaries.policyBillingSummary[rows].policy.policyPrefix != null)
                        {
                            drDataRow[CSAAWeb.Constants.PC_BILLPOL_PREFIX] = output.billingSummaries.policyBillingSummary[rows].policy.policyPrefix.ToString();
                        }
                        if (output.billingSummaries.policyBillingSummary[rows].policy.writingCompany != null)
                        {
                            drDataRow[Constants.PC_BILL_COMPANY_ID] = output.billingSummaries.policyBillingSummary[rows].policy.writingCompany.ToString();
                        }
                        //Begin PC Phase II Changes CH2 - Added the below code for updating the Billing Summary Lookup Service by including the Billingplan,PaymentPlan,Autopay,TermEffectiveDate & TermExpirationDate fields
                        if (output.billingSummaries.policyBillingSummary[rows].policy != null)
                        {
                            drDataRow[CSAAWeb.Constants.PC_BILL_TermEffectiveDate] = output.billingSummaries.policyBillingSummary[rows].policy.termEffectiveDate.ToString();
                            drDataRow[CSAAWeb.Constants.PC_TermExpirationDate] = output.billingSummaries.policyBillingSummary[rows].policy.termExpirationDate.ToString();
                        }
                        //End PC Phase II Changes CH2 - Added the below code for updating the Billing Summary Lookup Service by including the Billingplan,PaymentPlan,Autopay,TermEffectiveDate & TermExpirationDate fields
                        if (output.billingSummaries.policyBillingSummary[rows].policy.insureds != null)
                        {
                            if (output.billingSummaries.policyBillingSummary[rows].policy.insureds.namedInsuredSummary[0].name.firstName != null)
                            {
                                drDataRow[Constants.PC_BILL_FIRST_NAME] = output.billingSummaries.policyBillingSummary[rows].policy.insureds.namedInsuredSummary[0].name.firstName.ToString();
                            }
                            if (output.billingSummaries.policyBillingSummary[rows].policy.insureds.namedInsuredSummary[0].name.lastName != null)
                            {
                                drDataRow[Constants.PC_BILL_LAST_NAME] = output.billingSummaries.policyBillingSummary[rows].policy.insureds.namedInsuredSummary[0].name.lastName.ToString();
                            }
                            if (output.billingSummaries.policyBillingSummary[rows].policy.insureds.namedInsuredSummary[0].name.fullName != null)
                            {
                                drDataRow[Constants.PC_BILL_INS_FULL_NAME] = output.billingSummaries.policyBillingSummary[rows].policy.insureds.namedInsuredSummary[0].name.fullName.ToString();
                            }


                            if (output.billingSummaries.policyBillingSummary[rows].policy.insureds.namedInsuredSummary[0].preferredPostalAddress != null)
                            {
                                string address=string.Empty;
                                address = string.Format("{0},{1},{2},{3}", output.billingSummaries.policyBillingSummary[rows].policy.insureds.namedInsuredSummary[0].preferredPostalAddress.streetAddressLine,
                                    output.billingSummaries.policyBillingSummary[rows].policy.insureds.namedInsuredSummary[0].preferredPostalAddress.cityName, output.billingSummaries.policyBillingSummary[rows].policy.insureds.namedInsuredSummary[0].preferredPostalAddress.isoRegionCode,
                                    output.billingSummaries.policyBillingSummary[rows].policy.insureds.namedInsuredSummary[0].preferredPostalAddress.zipCode);
                                drDataRow[Constants.PC_BILL_ADDRESS] = address;
                            }

                        }
                        if (output.billingSummaries.policyBillingSummary[rows].billingSummary.bill != null)
                        {
                            if (output.billingSummaries.policyBillingSummary[rows].billingSummary.bill.dueDate != defaultDateTime)
                            {
                                drDataRow[CSAAWeb.Constants.PC_BILL_Due_Date] = output.billingSummaries.policyBillingSummary[rows].billingSummary.bill.dueDate.ToString();
                            }
                            if (output.billingSummaries.policyBillingSummary[rows].billingSummary.bill.statementDate != defaultDateTime)
                            {
                                drDataRow[CSAAWeb.Constants.PC_StatementDate] = output.billingSummaries.policyBillingSummary[rows].billingSummary.bill.statementDate.ToString();
                            }
                        }
                        //AZ PAS Conversion and PC integration -CH1 -START - Modified the mapping for due amount and total amount
                        //PC Phase II - CH1 - Start - Modified the code to remap the total amount and minimum amount due.
                        if (output.billingSummaries.policyBillingSummary[rows].billingSummary != null)
                        {
                            //Begin PC Phase II Changes CH2 - Added the below code for updating the Billing Summary Lookup Service by including the Billingplan,PaymentPlan,Autopay,TermEffectiveDate & TermExpirationDate fields
                            drDataRow[CSAAWeb.Constants.PC_BILL_PAYMENT_RESTRICTION] = output.billingSummaries.policyBillingSummary[rows].billingSummary.paymentRestriction.ToString();
                            //drDataRow[CSAAWeb.Constants.PC_BILL_Due_Amount] = output.billingSummaries.policyBillingSummary[0].billingSummary.currentAmountDue.ToString();
                            //drDataRow[CSAAWeb.Constants.PC_BILL_Total_Amount] = output.billingSummaries.policyBillingSummary[0].billingSummary.bill.totalBillAmountDue.ToString();
                            drDataRow[CSAAWeb.Constants.PC_BILL_Total_Amount] = output.billingSummaries.policyBillingSummary[rows].billingSummary.payOffAmount.ToString();
                            drDataRow[Constants.PC_BILL_AUTO_PAY] = output.billingSummaries.policyBillingSummary[rows].billingSummary.autoPay.ToString();
                            //End PC Phase II Changes CH2 - Added the below code for updating the Billing Summary Lookup Service by including the Billingplan,PaymentPlan,Autopay,TermEffectiveDate & TermExpirationDate fields

                            if (output.billingSummaries.policyBillingSummary[rows].billingSummary.bill != null)
                            {
                                if (output.billingSummaries.policyBillingSummary[rows].billingSummary.currentBalance != null)
                                {
                                    drDataRow[CSAAWeb.Constants.PC_BILL_Due_Amount] = output.billingSummaries.policyBillingSummary[rows].billingSummary.currentBalance.ToString();
                                }
                                //Begin PC Phase II Changes CH2 - Added the below code for updating the Billing Summary Lookup Service by including the Billingplan,PaymentPlan,Autopay,TermEffectiveDate & TermExpirationDate fields
                                if (output.billingSummaries.policyBillingSummary[rows].billingSummary.bill.billingPlan != null)
                                {
                                    drDataRow[Constants.PC_BillingPlan] = output.billingSummaries.policyBillingSummary[rows].billingSummary.bill.billingPlan.ToString();
                                }
                                if (output.billingSummaries.policyBillingSummary[rows].billingSummary.bill.paymentPlan != null)
                                {
                                    drDataRow[CSAAWeb.Constants.PC_BIL_PaymentPlan] = output.billingSummaries.policyBillingSummary[rows].billingSummary.bill.paymentPlan.ToString();
                                }
                                //End PC Phase II Changes CH2 - Added the below code for updating the Billing Summary Lookup Service by including the Billingplan,PaymentPlan,Autopay,TermEffectiveDate & TermExpirationDate fields

                            }
                            
                            
                        }
                        //PC Phase II - CH1 - End - Modified the code to remap the total amount and minimum amount due.
                        //AZ PAS Conversion and PC integration CH1- END- Modified the mapping for due amount and total amount
                        if (output.billingSummaries.policyBillingSummary[rows].policy.type != null)
                        {
                            drDataRow[Constants.PC_BILL_POL_GROUP] = output.billingSummaries.policyBillingSummary[rows].policy.type.ToString();
                        }
                        if (output.billingSummaries.policyBillingSummary[rows].policy.status != null)
                        {
                            drDataRow[Constants.PC_BILL_STATUS] = output.billingSummaries.policyBillingSummary[rows].policy.status.ToString();
                        }
                        if (output.billingSummaries.policyBillingSummary[rows].policy.statusDescription != null)
                        {
                            drDataRow[Constants.PC_BILL_STATUS_DESCRIPTION] = output.billingSummaries.policyBillingSummary[rows].policy.statusDescription.ToString();
                        }
                        if (output.billingSummaries.policyBillingSummary[rows].policy.convertedPolicy != null)
                        {
                            if (output.billingSummaries.policyBillingSummary[rows].policy.convertedPolicy.policyNumber != null)
                            {
                                drDataRow[Constants.PC_BILL_CONVERTED_POLICYNUMBER] = output.billingSummaries.policyBillingSummary[rows].policy.convertedPolicy.policyNumber.ToString();
                            }
                        }

                    }
                    dtPolicyDetail.Rows.Add(drDataRow);
                }
                //MAIG - CH2 - END - Included the additional fields to retrieve the Validation results in RPBS Service and also logic to handle more than one rows if returned from Rpbs Service. Cleanup of code is done, by moving strings to Constants.
                return dtPolicyDetail;
            }
            catch (Exception Ex)
            {
                Logger.Log(Ex.ToString());
                return null;
            }
        }
    }
}
