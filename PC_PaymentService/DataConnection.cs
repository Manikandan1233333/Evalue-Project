/* 
 * Creation Date:
 * Created by: Cognizant
 * Decsription: This is a new file added as part of Payment Central Phase II.
 * PC Phase II changes for Database Insertion.
 * PC Phase II changes CH1 - Added the new method for void transcations .
 * MAIG - CH1 - Added the addtional parameter to get the AutoApproved and Accoutn Number details.
 * MAIG - CH2 - Added the below code to check if the length of the error message does not exceed 20 characters
 * MAIG - CH3 - Added the below code to pass the newly added Auto approval flag 
 * MAIG - CH4 - Added the below code to pass the Product Code and Company code for IDP Service if the request is from SalesX Interface
 * MAIG - CH5 - fetch the value from the List and save the Policy State, prefix, Company Code and Check Number
 * MAIG - CH6 - Added the Additional parameter InsData
 * MAIG - CH7 - Added the Policy State, prefix, Company Code details as a parameter.
 * MAIG - CH8 - Added this new Method as part of Invalid Policy Enrollment changes
  */
using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using CSAAWeb;
using CSAAWeb.AppLogger;
using CSAAWeb.Serializers;
using System.Text.RegularExpressions;
using PaymentClasses;
using AuthenticationClasses;
using System.Collections.Generic;

namespace PC_PaymentService
{
    public class DataConnection : CSAAWeb.Web.SqlWebService
    {
        private static bool LogInsert = false;
        /// <summary>
        /// Override of base ConnectionString property forces the name to Payments instead of DataConnection.
        /// </summary>
        protected override string ConnectionString
        {
            get { return Config.Setting("ConnectionString.Payments", ((Config.bSetting("ConnectionString.Payments.Encrypted")) ? Constants.CS_STRING : "")); }
        }
        static DataConnection()
        {
            LogInsert = Config.bSetting("Payment_LogInsert");
        }
        #region PC_Insurance_Applications

        /// <summary>
        /// Inserts Cash/Check Transaction data in Database 
        /// </summary>
        /// <param name="user">Encapsulates site user information.</param>
        /// <param name="AppName">The name of the application calling the method.</param>
        /// <param name="PaymentType">The type of payment.</param>
        /// <param name="ReceiptNo">The Receipt number for this Transaction.</param>
        /// <param name="MerchantRefNumber">The Merchant Reference Number for the Transaction</param>
        /// <param name="Items">Represents a collection of line items.</param>
        /// <param name="status">Transaction Status</param>
        /// <returns>Payment ID</returns>
		//MAIG - CH1 - BEGIN - Added the addtional parameter to get the AutoApproved and Accoutn Number details.
        public int pcCashCheck(UserInfo user, string AppName, int PaymentType, string ReceiptNo, string MerchantRefNumber, ArrayOfLineItem Items, string status, string crossSequenceNumber, List<string> InsData, bool IsAutoApproved)
		//MAIG - CH1 - END - Added the addtional parameter to get the AutoApproved and Accoutn Number details.
        {
            decimal TotalAmount = 0;
            int PaymentId = 0;
            try
            {
                int ApplicationID=0;
                SqlCommand Cmd_Insert_Payment = GetCommand(CSAAWeb.Constants.PC_SP_Pay_InsertPayment);
                if (AppName.ToUpper().Equals(CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL))
                {
                    ApplicationID = 4;
                }
                else if (AppName.ToUpper().Equals(CSAAWeb.Constants.PC_APPID_SALESX))
                {
                    ApplicationID = 5;
                }
                else
                {
                    ApplicationID = 12;
                }
                Cmd_Insert_Payment.Parameters.AddWithValue("applicationID", ApplicationID);
                Cmd_Insert_Payment.Parameters.AddWithValue("@receiptNumber", ReceiptNo);
                Cmd_Insert_Payment.Parameters.AddWithValue("@merchantRefNumber", MerchantRefNumber);
                Cmd_Insert_Payment.Parameters.AddWithValue("@paymentTypeID", PaymentType);
                foreach (LineItem line in Items)
                {
                    TotalAmount = TotalAmount + line.Amount;
                }
                Cmd_Insert_Payment.Parameters.AddWithValue("@amount", TotalAmount);
                Cmd_Insert_Payment.Parameters.AddWithValue("@userID", user.UserId);
				//MAIG - CH2 - BEGIN - Added the below code to check if the length of the error message does not exceed 20 characters
                if (status.Length >= 20)
                {
                    Cmd_Insert_Payment.Parameters.AddWithValue("@PC_Status", status.Substring(0, 20));
                }
                else
                {
                    Cmd_Insert_Payment.Parameters.AddWithValue("@PC_Status", status);
                }
				//MAIG - CH2 - END - Added the below code to check if the length of the error message does not exceed 20 characters
                Cmd_Insert_Payment.Parameters.AddWithValue("@physicalLocation", user.PhysicalLocation);
                Cmd_Insert_Payment.Parameters.AddWithValue("@financialLocation", user.FinancialLocation);
                //Added crossSequenceNumber in the input parameter list 
                Cmd_Insert_Payment.Parameters.AddWithValue("@crossSequenceNumber", crossSequenceNumber);
				//MAIG - CH3 - BEGIN - Added the below code to pass the newly added Auto approval flag 
                Cmd_Insert_Payment.Parameters.AddWithValue("@IsAutoApproved", IsAutoApproved);
				//MAIG - CH3 - END - Added the below code to pass the newly added Auto approval flag 
                SqlParameter Paymentid = new SqlParameter("@PaymentId", System.Data.SqlDbType.VarChar, 15);
                Paymentid.Direction = ParameterDirection.Output;
                Cmd_Insert_Payment.Parameters.Add(Paymentid);
                Cmd_Insert_Payment.ExecuteNonQuery();
                PaymentId = int.Parse(Paymentid.Value.ToString());

                SqlCommand Cmd_Insert_Item = GetCommand(CSAAWeb.Constants.PC_SP_Pay_Insert_Item);
                for (int i = 0; i < Items.Count; i++)
                {
                    Cmd_Insert_Item.Parameters.AddWithValue("@PaymentId", PaymentId);
                    Cmd_Insert_Item.Parameters.AddWithValue("@LineItemNo", Items[i].LineItemNo);
				//MAIG - CH4 - BEGIN - Added the below code to pass the Product Code and Company code for IDP Service if the request is from SalesX Interface
                    if (!string.IsNullOrEmpty(Items[i].ProductCode) && Items[i].ProductCode.Equals("10"))
                    {
                        Cmd_Insert_Item.Parameters.AddWithValue("@productCode", "HO");
                    }
                    else if (!string.IsNullOrEmpty(Items[i].ProductCode) && Items[i].ProductCode.Equals("WUA"))
                    {
                        Cmd_Insert_Item.Parameters.AddWithValue("@productCode", "PA");
                    }
                    else
                    {
                        Cmd_Insert_Item.Parameters.AddWithValue("@productCode", Items[i].ProductCode);
                    }
				//MAIG - CH4 - END - Added the below code to pass the Product Code and Company code for IDP Service if the request is from SalesX Interface
                    Cmd_Insert_Item.Parameters.AddWithValue("@Amount", Items[i].Amount);
                    Cmd_Insert_Item.Parameters.AddWithValue("@lastName", Items[i].LastName);
                    Cmd_Insert_Item.Parameters.AddWithValue("@firstName", Items[i].FirstName);
                    Cmd_Insert_Item.Parameters.AddWithValue("@revenueType", Items[i].RevenueType);

                    //MAIG - CH5 - BEGIN - fetch the value from the List and save the Policy State, prefix, Company Code and Check Number
                    /*
                    string[] StrSplitPolicy;
                    string Policystate = "";
                    string PolicyPrefix = "";
                    string CompanyID = "";
                    string AccountNumber = "";
                    string checkNumber = "";
                    StrSplitPolicy = Items[i].AccountNumber.Split('-');
                    // For all PCP Products
                    if (Config.Setting("PCP.Products").IndexOf(Convert.ToString(Items[i].ProductTypeNew)) > -1)
                    {
                        AccountNumber =StrSplitPolicy[2];
                        Policystate = StrSplitPolicy[0];
                        PolicyPrefix = StrSplitPolicy[1];
                        CompanyID = StrSplitPolicy[3];
                    }
                    else 
                    {
                        //Payment tool UI - Check Transactions
                        if (AppName.ToUpper().Equals(CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL) && (PaymentType == 4))
                        {
                            AccountNumber = StrSplitPolicy[0];
                            CompanyID = StrSplitPolicy[1];
                            //Added the below condition for assigning CompanyID - inactive policy and UI check transactions
                            if (CompanyID == string.Empty)
                            {
                                CompanyID = CSAAWeb.Constants.PC_COMPANY_CODE;
                            }
                            checkNumber = StrSplitPolicy[2];
                        }
                            //All other Transactions
                        else
                        {
                            AccountNumber = StrSplitPolicy[0];
                            if (StrSplitPolicy.Length > 1)
                            {
                                if (StrSplitPolicy[1] == string.Empty)
                                {
                                    CompanyID = CSAAWeb.Constants.PC_COMPANY_CODE;
                                }
                                else
                                {
                                    CompanyID = StrSplitPolicy[1];

                                }
                            }
                                
                        }
                    }*/

                    Cmd_Insert_Item.Parameters.AddWithValue("@policyState", InsData[0]);
                    Cmd_Insert_Item.Parameters.AddWithValue("@policyPrefix", InsData[1]);
                    Cmd_Insert_Item.Parameters.AddWithValue("@companyID", InsData[2]);
                    Cmd_Insert_Item.Parameters.AddWithValue("@policyNumber", Items[i].AccountNumber);
                    Cmd_Insert_Item.Parameters.AddWithValue("@checkNumber", InsData[3]);
                    Cmd_Insert_Item.Parameters.AddWithValue("@mailingZip", InsData[4]);
                    //MAIG - CH5 - END - fetch the value from the List and save the Policy State, prefix, Company Code and Check Number
                    Cmd_Insert_Item.Parameters.AddWithValue("@paymentTypeID", PaymentType);
                    Cmd_Insert_Item.ExecuteNonQuery();
                    Cmd_Insert_Item.Parameters.Clear();
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message + e.StackTrace + e.InnerException);
                throw e;
            }
            return PaymentId;
        }

        /// <summary>
        /// Inserts Credit/E-Check Transaction data to Database 
        /// </summary>
        /// <param name="user">Encapsulates site user information.</param>
        /// <param name="AppName">The name of the application calling the method.</param>
        /// <param name="PaymentType">The type of payment.</param>
        /// <param name="ReceiptNo">The Receipt number for this Transaction.</param>
        /// <param name="MerchantRefNumber">The Merchant Reference Number for the Transaction</param>
        /// <param name="ReturnCode">Response code from Payment Central</param>
        /// <param name="Card">Description for CardInfo.</param>
        /// <param name="Echeck">Capture the echeck information</param>
        /// <param name="Billto">BillToInfo encapsulates all the customer's billing information.</param>
        /// <param name="Items">Represents a collection of line items.</param>
        /// <param name="status">Transaction Status</param>
		//MAIG - CH6 - BEGIN - Added the Additional parameter InsData
        public void pcCreditCard(UserInfo user, string AppName, int PaymentType, string ReceiptNo, string MerchantRefNumber, string ReturnCode, CardInfo Card,eCheckInfo Echeck, BillToInfo Billto, ArrayOfLineItem Items,string status,string crossReferenceNumber, List<string> InsData)
		//MAIG - CH6 - END - Added the Additional parameter InsData
        {
            try
            {
                int PaymentId = 0;
                //MAIG - CH7 - BEGIN - Added the Policy State, prefix, Company Code details as a parameter.
                PaymentId = pcCashCheck(user, AppName, PaymentType, ReceiptNo, MerchantRefNumber, Items, status, crossReferenceNumber,InsData,false);
                //MAIG - CH7 - END - Added the Policy State, prefix, Company Code details as a parameter.
                SqlCommand Cmd_Insert_Card = GetCommand(CSAAWeb.Constants.PC_SP_Pay_InsertCardCheck);
                Cmd_Insert_Card.Parameters.AddWithValue("@paymentId", PaymentId);
                Cmd_Insert_Card.Parameters.AddWithValue("@Result_Code", "");
                Cmd_Insert_Card.Parameters.AddWithValue("@ReAuth", "");
                Cmd_Insert_Card.Parameters.AddWithValue("@BillToAddress", Billto.ToString());
                Cmd_Insert_Card.Parameters.AddWithValue("@Items", Items.ToString());
                Cmd_Insert_Card.Parameters.AddWithValue("@VerbalAuthorization", "");
                if (PaymentType == 5)
                {
                    Cmd_Insert_Card.Parameters.AddWithValue("@RequestID", "");
                    Cmd_Insert_Card.Parameters.AddWithValue("@Auth_Code", "");
                    Cmd_Insert_Card.Parameters.AddWithValue("@BankId", Echeck.BankId);
                    Cmd_Insert_Card.Parameters.AddWithValue("@signature", Echeck.signature);
                    Cmd_Insert_Card.Parameters.AddWithValue("@BankAcntType", Echeck.BankAcntType);
                    Cmd_Insert_Card.Parameters.AddWithValue("@CustomerName", Echeck.CustomerName);
                    Cmd_Insert_Card.Parameters.AddWithValue("@CCType", 0);
                    Cmd_Insert_Card.Parameters.AddWithValue("@CCExpYear", 0);
                    Cmd_Insert_Card.Parameters.AddWithValue("@CCExpMonth", 0);
                }
                else
                {
                    Cmd_Insert_Card.Parameters.AddWithValue("@RequestID", "");
                    Cmd_Insert_Card.Parameters.AddWithValue("@Auth_Code", ReturnCode);
                    Cmd_Insert_Card.Parameters.AddWithValue("@CCType", int.Parse(Card.CCType));
                    Cmd_Insert_Card.Parameters.AddWithValue("@CCExpYear", Card.CCExpYear);
                    Cmd_Insert_Card.Parameters.AddWithValue("@CCExpMonth", Card.CCExpMonth);
                    Cmd_Insert_Card.Parameters.AddWithValue("@BankId", "");
                    Cmd_Insert_Card.Parameters.AddWithValue("@signature", Card.Signature);
                    Cmd_Insert_Card.Parameters.AddWithValue("@BankAcntType", "");
                    Cmd_Insert_Card.Parameters.AddWithValue("@CustomerName", "");
                }
                Cmd_Insert_Card.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message + e.StackTrace + e.InnerException);
                throw e;
            }
        }

        /// <summary>
        /// Retrieves the Company code for Other Interfaces and it is a PCP Product
        /// </summary>
        /// <param name="PolicyPrefix">Contains Policy Prefix</param>
        /// <param name="PolicyState">Contains Policy State</param>
        /// <param name="ProductCode">Contains Product code</param>
        /// <returns></returns>
        public string Get_Companycode(string PolicyPrefix,string PolicyState,string ProductCode)
        {
            try
            {
                SqlCommand Cmd_GetCompany = GetCommand(CSAAWeb.Constants.PC_SP_Pay_Get_WU_CompanyCode);
                Cmd_GetCompany.Parameters.AddWithValue("@policyPrefix", PolicyPrefix);
                Cmd_GetCompany.Parameters.AddWithValue("@policyState", PolicyState);
                Cmd_GetCompany.Parameters.AddWithValue("@productCode", ProductCode);
                SqlParameter CompanyId = new SqlParameter("@CompanyID", System.Data.SqlDbType.VarChar, 10);
                CompanyId.Direction = ParameterDirection.Output;
                Cmd_GetCompany.Parameters.Add(CompanyId);
                Cmd_GetCompany.ExecuteScalar();
                return CompanyId.Value.ToString();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message + e.StackTrace + e.InnerException);
                throw e;
            }
                
        }
        /// <summary>
        /// Get a unique merchant reference number.
        /// </summary>
        /// <param name="Application">The ID of the calling application.</param>
        /// <returns>Merchant reference number</returns>
        public string GetMerchantReference(string Application)
        {
            try
            {
                SqlCommand cmd = GetCommand(CSAAWeb.Constants.PC_SP_Get_Running_Number);
                (new SimpleSerializer()).CopyTo(cmd);
                cmd.Parameters["@AppName"].Value = Application;
                cmd.Parameters["@NumberType"].Value = "REFERENCE";
                cmd.Parameters["@CurrentUser"].Value = "";
                cmd.Parameters["@RunningNumber"].Value = "";
                cmd.ExecuteNonQuery();
                return cmd.Parameters["@RunningNumber"].Value.ToString();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message + e.StackTrace + e.InnerException);
                throw e;
            }
        }

        /// <summary>
        /// PC Phase II changes CH1 - Start - Updates the Payment table with Voided receipt number and last modified Date 
        /// </summary>
        /// <param name="voidType">Type of Void either Manaul/ Void</param>
        /// <param name="ReceiptNumber">Receipt number which is Voided</param>
        /// <param name="VoidReceiptNumber">Receipt number for the Void transaction</param>
        /// <param name="lastModifiedDate">Date for this Voided transaction</param>
        /// <returns>Status of Void</returns>
        public int PC_Update_Void(string voidType, string ReceiptNumber, string VoidReceiptNumber, DateTime lastModifiedDate, string ModifiedBy)
        {
            try
            {
                SqlCommand cmd_UpdateVoid=GetCommand(CSAAWeb.Constants.PC_SP_PC_Pay_Update_Void);
                                cmd_UpdateVoid.Parameters.AddWithValue("@voidType", voidType);
                cmd_UpdateVoid.Parameters.AddWithValue("@ReceiptNumber", ReceiptNumber);
                cmd_UpdateVoid.Parameters.AddWithValue("@VoidReceiptNumber", VoidReceiptNumber);
                cmd_UpdateVoid.Parameters.AddWithValue("@lastModifiedDate", lastModifiedDate);
                cmd_UpdateVoid.Parameters.AddWithValue("@ModifiedBy", ModifiedBy);
                SqlParameter out_Flag = new SqlParameter("@flag", System.Data.SqlDbType.Int, 10);
                out_Flag.Direction = ParameterDirection.Output;
                cmd_UpdateVoid.Parameters.Add(out_Flag);
                cmd_UpdateVoid.ExecuteScalar();
                return (int)out_Flag.Value;

            }
            catch (Exception e)
            {
                Logger.Log(e.Message + e.StackTrace + e.InnerException);
                throw e;
            }
        }
        //PC Phase II changes CH1 - End - Updates the Payment table with Voided receipt number and last modified Date 
        #endregion

		//MAIG - CH8 - BEGIN - Added this new Method as part of Invalid Policy Enrollment changes
        /// <summary>
        /// 
        /// Inserts/Updates the Enrollment status in PT DB for invalid policy details
        /// </summary>
        public void PT_Update_InvalidPolicy(string PolicyNumber, Int64 ConfirmationNumber, string AgentID, string LastName, string FirstName, string ZipCode, string Status, string ProductType, string SourceSystem, int IsEnrolled)
        {
            try
            {
                SqlCommand Cmd_UpdateInvalid = GetCommand(CSAAWeb.Constants.PC_SP_UpdateInvalidPolicy);
                Cmd_UpdateInvalid.Parameters.AddWithValue("@Policy", PolicyNumber);
                Cmd_UpdateInvalid.Parameters.AddWithValue("@ConfirmationNo", ConfirmationNumber);
                Cmd_UpdateInvalid.Parameters.AddWithValue("@Agent_ID", AgentID);
                Cmd_UpdateInvalid.Parameters.AddWithValue("@Last_Name", LastName);
                Cmd_UpdateInvalid.Parameters.AddWithValue("@First_Name", FirstName);
                Cmd_UpdateInvalid.Parameters.AddWithValue("@Zip_Code", ZipCode);
                Cmd_UpdateInvalid.Parameters.AddWithValue("@Status", Status);
                Cmd_UpdateInvalid.Parameters.AddWithValue("@Product_Type", ProductType);
                Cmd_UpdateInvalid.Parameters.AddWithValue("@Source_System", SourceSystem);
                Cmd_UpdateInvalid.Parameters.AddWithValue("@IsEnrolled", IsEnrolled);
                Cmd_UpdateInvalid.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Logger.Log(e.Message + e.StackTrace + e.InnerException);
                throw e;
            }

        }
	  //MAIG - CH8 - END - Added this new Method as part of Invalid Policy Enrollment changes
    }
}
