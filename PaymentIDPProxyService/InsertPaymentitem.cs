/*	CREATED BY COGNIZANT
 *	01/04/2013 -  HOLDS METHODS DEFINITION FOR LIVE WEB SERVICE AS PART OF PAYMENT CENTRAL PHASE II
 *  MAIG - CH1 - Added the below code to support the extra parameter for Auto approval
*   MAIG - CH2 - Added the below code to support the newly added mailing Zip parameter
 *
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Text.RegularExpressions;
using System.ServiceModel;
using System.Web.Services.Protocols;

namespace PaymentIDPProxyService
{
    /// <summary>
    /// Class to Hold method definition to Load  and Update Transsaction Details from Payment Central to Payment Tool
    /// </summary>
    public class InsertPaymentitem
    {

        string connstring = ConfigurationManager.AppSettings["ConnectionString.Payments"].ToString();
        Logging log = new Logging();

        /// <summary>
        /// Method to load the common transaction details from Payment Central to Payment Tool
        /// </summary>
        /// <param name="PaymentIDPProxyInputRequest"></param>
        /// <returns></returns>
        public string InsertPaymentItem(PaymentIDPProxyInputRequest PaymentIDPProxyInputRequest)
        {
            try
            {
                if (PaymentIDPProxyInputRequest.branchHubNumber == null)
                {
                    PaymentIDPProxyInputRequest.branchHubNumber = string.Empty;
                }
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand();

                    comm.Connection = conn;
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    //Calls PC_Pay_InsertPayment Stored Procedure
                    comm.CommandText = Constant.SP_INSERT_PAYMENT;
                    //Parameters
                    comm.Parameters.AddWithValue("@applicationID", PaymentIDPProxyInputRequest.applicationContext.application);
                    comm.Parameters.AddWithValue("@merchantRefNumber", PaymentIDPProxyInputRequest.externalReferenceNumber);
                    comm.Parameters.AddWithValue("@paymentTypeID", PaymentIDPProxyInputRequest.paymentMethod);
                    comm.Parameters.AddWithValue("@userID", PaymentIDPProxyInputRequest.userId);
                    comm.Parameters.AddWithValue("@receiptNumber", PaymentIDPProxyInputRequest.receiptNumber);
                    comm.Parameters.AddWithValue("@physicalLocation", PaymentIDPProxyInputRequest.districtOffice);
                    comm.Parameters.AddWithValue("@amount", PaymentIDPProxyInputRequest.totalAmount);
                    comm.Parameters.AddWithValue("@financialLocation", PaymentIDPProxyInputRequest.branchHubNumber);
                    comm.Parameters.AddWithValue("@pc_status", Constant.RESPONSE_SUCCESS);
                    comm.Parameters.AddWithValue("@crossSequenceNumber","" );
					//MAIG - CH1 - BEGIN - Added the below code to support the extra parameter for Auto approval
                    comm.Parameters.AddWithValue("@IsAutoApproved", false);
					//MAIG - CH1 - END - Added the below code to support the extra parameter for Auto approval
                    SqlParameter Paymentid = new SqlParameter("@PaymentId", System.Data.SqlDbType.Int);
                    Paymentid.Direction = ParameterDirection.Output;
                    comm.Parameters.Add(Paymentid);
                    comm.ExecuteScalar();
                    return Paymentid.Value.ToString();//returns Payment id as  output
                }
            }
            catch (FaultException ex)
            {
                log.WriteLog(ex.Message.ToString());
            }
            catch (CommunicationException exp)
            {

                log.WriteLog(exp.Message.ToString());
            }
            catch (Exception e)
            {
                string Error = e.Message.ToString() + e.StackTrace.ToString();
                log.WriteLog(Error.ToString());
            }
            
            return string.Empty;
        }

        ///<summary>
        ///Included the method to insert the Echeck/Cc Transactions
        /// </summary>        
        public void InsertCardCheck(string paymentid, card card)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {

                    conn.Open();
                    SqlCommand comm = new SqlCommand();

                    comm.Connection = conn;
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    //Stored Procedure Call
                    comm.CommandText = Constant.SP_INSERT_CARD;
                    //Input Parameters
                    comm.Parameters.AddWithValue("@paymentId", paymentid);
                    comm.Parameters.AddWithValue("@Result_Code", "");
                    comm.Parameters.AddWithValue("@Auth_Code", card.returnCode);
                    comm.Parameters.AddWithValue("@CCType", "");
                    comm.Parameters.AddWithValue("@CCExpYear", "");
                    comm.Parameters.AddWithValue("@CCExpMonth", "");
                    comm.Parameters.AddWithValue("@ReAuth", "");
                    comm.Parameters.AddWithValue("@BillToAddress", "");
                    comm.Parameters.AddWithValue("@Items", "");
                    comm.Parameters.AddWithValue("@RequestID",card.sequenceNumber );
                    comm.Parameters.AddWithValue("@VerbalAuthorization", "");
                    comm.Parameters.AddWithValue("@BankId", "");
                    comm.Parameters.AddWithValue("@signature", card.cardEcheckNumber);
                    comm.Parameters.AddWithValue("@BankAcntType", "");
                    comm.Parameters.AddWithValue("@CustomerName", "");
                    comm.ExecuteNonQuery();
                }
            }
            catch (FaultException ex)
            {
                log.WriteLog(ex.Message.ToString());
            }
            catch (CommunicationException exp)
            {

                log.WriteLog(exp.Message.ToString());
            }
            catch (Exception e)
            {
                string Error = e.Message.ToString() + e.StackTrace.ToString();
                log.WriteLog(Error.ToString());
            }
        }

        /// <summary>
        /// Included the method to update the voided receipt Number
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public string PCPayUpdateVoid(Update update)
        {

            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {
                    conn.Open();
                    SqlCommand comm = new SqlCommand();

                    comm.Connection = conn;
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.CommandText = Constant.SP_UPDATE_VOID;
                    comm.Parameters.AddWithValue("@voidType", "V");
                    comm.Parameters.AddWithValue("@ReceiptNumber", update.receiptNumber);
                    comm.Parameters.AddWithValue("@VoidReceiptNumber", update.voidReceiptNumber);
                    comm.Parameters.AddWithValue("@lastModifiedDate", update.lastModifiedDate);
                    comm.Parameters.AddWithValue("@ModifiedBy", "");

                    SqlParameter Flag = new SqlParameter("@flag", System.Data.SqlDbType.Int);
                    Flag.Direction = ParameterDirection.Output;
                    comm.Parameters.Add(Flag);
                    comm.ExecuteNonQuery();
                    return Flag.Value.ToString();
                }
            }
            catch (FaultException ex)
            {
                log.WriteLog(ex.Message.ToString());
            }
            catch (CommunicationException exp)
            {

                log.WriteLog(exp.Message.ToString());
            }
            catch (Exception e)
            {
                string Error = e.Message.ToString() + e.StackTrace.ToString();
                log.WriteLog(Error.ToString());
            }
            return string.Empty;
            
        }

        /// <summary>
        /// Method to insert the items based on line items
        /// </summary>
        /// <param name="input"></param>
        /// <param name="paymentid"></param>
        /// <param name="checknumber"></param>
        /// <param name="paymentMethod"></param>
        /// <returns></returns>

        public string InsertItem(LineItem input, string paymentid, string checknumber, string paymentMethod,int lineItemCount)
        {
            try
            {
                using (SqlConnection conn = new SqlConnection(connstring))
                {

                    if (input.revenueType == null)
                    {
                        input.revenueType = string.Empty;
                    }
                    if (input.policyState == null)
                    {
                        input.policyState = string.Empty;
                    }
                    if (input.policyPrefix == null)
                    {
                        input.policyPrefix = string.Empty;
                    }
                    if (checknumber == null)
                    {
                        checknumber = string.Empty;
                    }

                       conn.Open();
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = conn;
                    comm.CommandType = System.Data.CommandType.StoredProcedure;
                    comm.CommandText = Constant.SP_INSERT_ITEM;

                    comm.Parameters.AddWithValue("@PaymentId", paymentid);
                    comm.Parameters.AddWithValue("@LineItemNo", lineItemCount);
                    comm.Parameters.AddWithValue("@productCode", input.productCode);
                    comm.Parameters.AddWithValue("@Amount", input.Amount);
                    comm.Parameters.AddWithValue("@lastName", "");
                    comm.Parameters.AddWithValue("@firstName", "");
                    comm.Parameters.AddWithValue("@policyNumber", input.policyNumber);
                    comm.Parameters.AddWithValue("@revenueType", input.revenueType);
                    comm.Parameters.AddWithValue("@policyState", input.policyState);
                    comm.Parameters.AddWithValue("@policyPrefix", input.policyPrefix);
                    comm.Parameters.AddWithValue("@companyID", input.writingCompany);
                    comm.Parameters.AddWithValue("@checkNumber", checknumber);
                    //MAIG - CH2 - BEGIN - Added the below code to support the newly added mailing Zip parameter
                    comm.Parameters.AddWithValue("@mailingZip", "");
                    //MAIG - CH2 - END - Added the below code to support the newly added mailing Zip parameter
                    comm.Parameters.AddWithValue("@paymentTypeID", paymentMethod);

                    string result = (comm.ExecuteNonQuery()).ToString();
                    return result;
                }
            }
            catch (FaultException ex)
            {
                log.WriteLog(ex.Message.ToString());
            }
            catch (CommunicationException exp)
            {
                log.WriteLog(exp.Message.ToString());
            }
            catch (Exception e)
            {
                string Error = e.Message.ToString() + e.StackTrace.ToString();
                log.WriteLog(Error.ToString());
                return Error;
            }
            return string.Empty;
        }

        /// <summary>
        /// Line Item Input Validation
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public IDPProxyResponse ValidationLineItem(LineItem input)
        {
            IDPProxyResponse message = null;

            Regex junk_Chars = new Regex(@"[\\\~!@#$%^*()_+{}|""?`;',/[\]]+");

            //Line Item Validation
            if (string.IsNullOrEmpty(input.dataSource) | junk_Chars.IsMatch(input.dataSource))
            {
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.dataSource);
            }
              else if (!(input.dataSource.Equals(Constant.PAS) | input.dataSource.Equals(Constant.SIS) | input.dataSource.Equals(Constant.HDES) | input.dataSource.Equals(Constant.ADES)| input.dataSource.Equals(Constant.HUON) | input.dataSource.Equals(Constant.PUPSYS) | input.dataSource.Equals(Constant.HLSYS)))
            {
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.dataSource);
            }
            else if (string.IsNullOrEmpty(input.writingCompany) | junk_Chars.IsMatch(input.writingCompany))
            {
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.writingCompany);
            }
            else if (!(input.writingCompany.Equals(Constant.writingcompany_CSIIB) | (input.writingCompany.Equals(Constant.writingcompany_4WUIC))| (input.writingCompany.Equals(Constant.writingcompany_1MWIC))))
            {
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.writingCompany);
            }
            else if (string.IsNullOrEmpty(input.productCode) | junk_Chars.IsMatch(input.productCode))
            {
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.productCode);
            }
            else if (!(input.productCode.Equals(Constant.ProductCode_PA) | input.productCode.Equals(Constant.ProductCode_HO) | input.productCode.Equals(Constant.ProductCode_HL)| input.productCode.Equals(Constant.ProductCode_PU) | input.productCode.Equals(Constant.ProductCode_DF) | input.productCode.Equals(Constant.ProductCode_MC)| input.productCode.Equals(Constant.ProductCode_WC) | input.productCode.Equals(Constant.ProductCode_IM)))
            {
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.productCode);
            }
              
            else if (!string.IsNullOrEmpty(input.policyPrefix))
            {                
                if (junk_Chars.IsMatch(input.policyPrefix))
                {
                    message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.policyPrefix);
                }
            }
            else if (string.IsNullOrEmpty(input.policyNumber) | junk_Chars.IsMatch(input.policyNumber))
            {
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.policyNumber);
            }
            else if (!string.IsNullOrEmpty(input.policyState))
            {
                    if (junk_Chars.IsMatch(input.policyState))
                    {
                         message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.policyState);
                    }
                
            }
            else if (string.IsNullOrEmpty(input.Amount.ToString()) | junk_Chars.IsMatch(input.Amount.ToString()))
            {
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.Amount);
            }
            else if (!string.IsNullOrEmpty(input.revenueType))
            {                
                    if (junk_Chars.IsMatch(input.revenueType))
                    {
                     message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.revenueType);
                    }
                
            }
            else
            {
                message = new IDPProxyResponse(Constant.RESPONSE_SUCCESS, "");
            }


            return message;

        }

        /// <summary>
        /// Input Validation for Common Attributes
        /// </summary>
        /// <param name="PaymentIDPProxyInputRequest"></param>
        /// <returns></returns>
        public IDPProxyResponse Validation(PaymentIDPProxyInputRequest PaymentIDPProxyInputRequest)
        {
            IDPProxyResponse message = null;
            message = new IDPProxyResponse(Constant.RESPONSE_SUCCESS, Constant.VALIDATION_SUCCESS + PaymentIDPProxyInputRequest.receiptNumber);
            Regex junk_Chars = new Regex(@"[\\\~!@#$%^*()_+{}|""?`;',/[\]]+");
            decimal amount = 0.0M;
            decimal TotAmt = 0.0M;
            if ((PaymentIDPProxyInputRequest.applicationContext.application == Constant.APP_SALESX) | (PaymentIDPProxyInputRequest.applicationContext.application == Constant.APP_PMTTOOL) | (PaymentIDPProxyInputRequest.applicationContext.application == Constant.APP_PAYMENTX))
            {                
                
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.application);                
            }
            else if (string.IsNullOrEmpty(PaymentIDPProxyInputRequest.applicationContext.application) | junk_Chars.IsMatch(PaymentIDPProxyInputRequest.applicationContext.application))
            {
               
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.application);              
            }
            else if (string.IsNullOrEmpty(PaymentIDPProxyInputRequest.externalReferenceNumber)|junk_Chars.IsMatch(PaymentIDPProxyInputRequest.externalReferenceNumber))
           {
             
               message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.externalReferenceNumber);               
           }
            else if (string.IsNullOrEmpty(PaymentIDPProxyInputRequest.districtOffice) | junk_Chars.IsMatch(PaymentIDPProxyInputRequest.districtOffice))
            {
               
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.districtOffice);                
            }
            else if (!string.IsNullOrEmpty(PaymentIDPProxyInputRequest.branchHubNumber))
            {
                
                    if (junk_Chars.IsMatch(PaymentIDPProxyInputRequest.branchHubNumber))
                    {
                        message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.branchHubNumber);                
                    }
                
            }
            else if (string.IsNullOrEmpty(PaymentIDPProxyInputRequest.userId) | junk_Chars.IsMatch(PaymentIDPProxyInputRequest.userId))
            {
                
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.userId);                
            }
            else if (string.IsNullOrEmpty(PaymentIDPProxyInputRequest.receiptNumber) | junk_Chars.IsMatch(PaymentIDPProxyInputRequest.receiptNumber))
            {
                
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.receiptNumber);
                
            }
            else if (PaymentIDPProxyInputRequest.createdDate.ToString() == "")     
            {
                
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.createdDate);                
            }
            else if ((PaymentIDPProxyInputRequest.totalAmount == 0) | junk_Chars.IsMatch(PaymentIDPProxyInputRequest.totalAmount.ToString()))
            {               
               message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.totalAmount);                
            }
            
            else if (!((PaymentIDPProxyInputRequest.totalAmount == 0) | junk_Chars.IsMatch(PaymentIDPProxyInputRequest.totalAmount.ToString())))
            {
                if (PaymentIDPProxyInputRequest.LineItem.Count == 1 )
                {
                if (!(Decimal.Equals(PaymentIDPProxyInputRequest.totalAmount, PaymentIDPProxyInputRequest.LineItem[0].Amount)))
                {
                    message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.totalAmount);
                } 
                }          

            else if(PaymentIDPProxyInputRequest.LineItem.Count >1)        
            {
                              
                foreach (LineItem item in PaymentIDPProxyInputRequest.LineItem)
                {
                    TotAmt = TotAmt + item.Amount;                                                                
                    amount = TotAmt;                    
                }
                if(Decimal.Equals(PaymentIDPProxyInputRequest.totalAmount,TotAmt))
                {
                    PaymentIDPProxyInputRequest.totalAmount = TotAmt;
                }
                if (!(Decimal.Equals(PaymentIDPProxyInputRequest.totalAmount, amount)))
                {
                    message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.totalAmount);
                }               
            }                 
              
            }                    
               
            if (string.IsNullOrEmpty(PaymentIDPProxyInputRequest.paymentMethod) | junk_Chars.IsMatch(PaymentIDPProxyInputRequest.paymentMethod))
            {
                
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.paymentMethod);               
            }
            //
            else if (PaymentIDPProxyInputRequest.paymentMethod.ToUpper().Equals("CHCK"))
            {
                if ((string.IsNullOrEmpty(PaymentIDPProxyInputRequest.checkNumber)))
                {
                    message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.checkNumber);
                }
            }
            return message;            
        }

        /// <summary>
        /// Card Input Field Validation
        /// </summary>
        /// <param name="card"></param>
        /// <returns></returns>
        public IDPProxyResponse CardValidation(PaymentIDPProxyInputRequest PaymentIDPProxyInputRequest)
        {
            IDPProxyResponse message = null;

            Regex junk_Chars = new Regex(@"[\\\~!@#$%^*()_+{}|""?`;',/[\]]+");

            if (string.IsNullOrEmpty(PaymentIDPProxyInputRequest.card.cardEcheckNumber) | junk_Chars.IsMatch(PaymentIDPProxyInputRequest.card.cardEcheckNumber) | PaymentIDPProxyInputRequest.card.cardEcheckNumber.Length!=16)
            {

                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.cardEcheckNumber);
            }
            else if (string.IsNullOrEmpty(PaymentIDPProxyInputRequest.card.returnCode) | junk_Chars.IsMatch(PaymentIDPProxyInputRequest.card.returnCode))
            {

                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.returnCode);
            }
            else if (string.IsNullOrEmpty(PaymentIDPProxyInputRequest.card.authorizationCode) | junk_Chars.IsMatch(PaymentIDPProxyInputRequest.card.authorizationCode))
            {

                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.authorizationCode);

            }
            else
            {
                message = new IDPProxyResponse(Constant.RESPONSE_SUCCESS, Constant.VALIDATION_SUCCESS + PaymentIDPProxyInputRequest.receiptNumber);
            }
            return message;
        }
        public IDPProxyResponse CheckValidation(PaymentIDPProxyInputRequest PaymentIDPProxyInputRequest)
        {
            IDPProxyResponse message = null;

            Regex junk_Chars = new Regex(@"[\\\~!@#$%^*()_+{}|""?`;',/[\]]+");

            if (string.IsNullOrEmpty(PaymentIDPProxyInputRequest.checkNumber) | junk_Chars.IsMatch(PaymentIDPProxyInputRequest.checkNumber))
            {
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.checkNumber);

            }
            else
            {
                message = new IDPProxyResponse(Constant.RESPONSE_SUCCESS, Constant.VALIDATION_SUCCESS + PaymentIDPProxyInputRequest.receiptNumber);
            }
            return message;
        }


        /// <summary>
        /// Input Validation for Updating 
        /// </summary>
        /// <param name="update"></param>
        /// <returns></returns>
        public IDPProxyResponse UpdateValidation(Update update)
        {
            IDPProxyResponse message = null;

            Regex junk_Chars = new Regex(@"[\\\~!@#$%^*()_+{}|""?`;',/[\]]+");

            if (string.IsNullOrEmpty(update.receiptNumber) | junk_Chars.IsMatch(update.receiptNumber))
            {

                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.receiptNumber);

            }
            else if (string.IsNullOrEmpty(update.voidReceiptNumber) | junk_Chars.IsMatch(update.voidReceiptNumber))
            {

                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.voidReceiptNumber);

            }
            else if (update.lastModifiedDate.ToString() == "")
            {

                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.lastModifiedDate);

            }
            else
            {
                message = new IDPProxyResponse(Constant.RESPONSE_SUCCESS, Constant.VALIDATION_SUCCESS + update.receiptNumber);
            }

            return message;
        }
    }
}

