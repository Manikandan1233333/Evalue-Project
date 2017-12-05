﻿/* 
 * Creation Date:
 * Created by: Cognizant on 1/18/2013
 * Decsription: This is a new file added as part of Payment Central Phase II.
 * PC Phase II changes for Cash/Check/E-check and Credit card  Transactions.
 * PC Phase II changes - CH1 - Added the method IDPReversal method to call PC IDP Reversal service
 * CHG0072116 - PC Edit Card Details CH1:Overloaded the "Tokenmapping" method with the parameter "PaymentAuthToken" to consume "MaintainPaymentAccount" service.
 * CHG0083477 - Inclusion of RepID in IDP Service.
 * MAIG - CH1 - Modified the Old logic for getting the DataSource
 * MAIG - CH2 - Added the Code to get the Company Code and Product Code for SalesX Interface
 * MAIG - CH3 - Modified the Code to pass the Agent ID & DO to IDP Service
 * MAIG - CH4 - Added logic to send the Email Address to IDP Service
 * MAIG - CH5 - Removed the logic to fetch the Policy State and Policy Prefix from Account Number and included the Policy State as an parameter to IDp Service
 * MAIG - CH6 - Removed the logic to check if it is an PCP product and updated the logic for PUP policies
 * MAIG - CH7 - Fetch the CompanyCode and Source System from OrderDetail Object
 * MAIG - CH8 - Fetch the Check Number from the Optional Order Object Detail Field and remove the append logic. 
 * MAIG - CH9 - Removed the logic to append the details to Account Number field
 * MAIG - CH10 - Added a List to send the Policy details to DB insertion which was appeneded in AccountNumber field earlier
 * MAIGEnhancement CHG0107527 - CH1 - Modified the below code to auto approve for Users having PS User role if the payment response is success
 * MAIGEnhancement CHG0107527 - CH2 - Modified the below code to auto approve for Users having Ps User role if the payment response is failure
 * CHG0112662 - Appended the condition for error code 300 to the existing fault exception handling & Removed Error Code in actual error message.
 * CHGXXXXXXJ - CH2 - Changes made for splitting the token service - 27/06/2017.
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PaymentClasses;
using IDP;
using PC_PaymentService;
using AuthenticationClasses;
using System.ServiceModel;
using CSAAWeb.AppLogger;
using System.Collections.Specialized;
using CSAAWeb;
using System.IO;
using PaymentClasses.Service;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using CSAAWeb.Web;
using OrderClasses.Service;
using System.Xml;
using System.Web.Services.Protocols;
using CSAAWeb.Serializers;
using IDPReversal;
using TurninClasses.WebService;
using System.Net.Security;
using System.Runtime.Remoting.Contexts;

namespace OrderClassesII
{
    public class IssueDirectPaymentWrapper
    {
        PC_PaymentService.DataConnection pcDataconnection = new DataConnection();
        private bool IsPaymentTokenGenerated;
        /// <summary>
        /// Creates an instance of IDP Service
        /// </summary>
        /// <typeparam name="T">Provides the Channel Name</typeparam>
        /// <param name="endpointConfigurationName">Configuration Name</param>
        private T CreateInstance<T>(string endpointConfigurationName)
        {
            ChannelFactory<T> customChannelFactory = new ChannelFactory<T>(endpointConfigurationName);
            return customChannelFactory.CreateChannel();
        }

        /// <summary>
        /// Issue Direct Payment Request and Response Logging
        /// </summary>
        /// <param name="rspObj">Response Object for IDP Service</param>
        /// <param name="issueDirectPaymentRequest1Obj">Request Object for IDP Service</param>
        private void LogServiceRequestResponseForPayment(IssueDirectPaymentResponse1 idp_RspObj, MaintainPaymentAccountResponse token_RspObj)
        {
            System.Xml.Serialization.XmlSerializer serializerReq;
            System.Xml.Serialization.XmlSerializer serializerRes;
            try
            {
                if (token_RspObj != null)
                {
                    serializerReq = new System.Xml.Serialization.XmlSerializer(token_RspObj.GetType());
                    StringWriter token_WriterRes = new StringWriter();
                    serializerReq.Serialize(token_WriterRes, token_RspObj);
                    Logger.Log("Payment response from Payment Central - Payment token Response : \r\n" + token_WriterRes);
                    token_WriterRes.Close();
                }

                if (idp_RspObj != null)
                {
                    serializerRes = new System.Xml.Serialization.XmlSerializer(idp_RspObj.GetType());
                    StringWriter IDP_WriterRes = new StringWriter();
                    serializerRes.Serialize(IDP_WriterRes, idp_RspObj);
                    Logger.Log("Payment Response from Payment Central - IDP Response : \r\n" + IDP_WriterRes);
                    IDP_WriterRes.Close();
                }
            }
            catch (Exception Ex)
            {
                Logger.Log("Error Occurred during logging of Response XML" + Ex.InnerException.ToString());
            }
            finally
            {
                serializerReq = null;
                serializerRes = null;
            }
        }

        /// <summary>
        /// Identify the data source for the selected product type and Policy Length
        /// </summary>
        /// <param name="ProductType"></param>
        /// <param name="iPolLength"></param>
        /// <returns></returns>
        public string DataSource(string ProductType, int iPolLength)
        {
            string PC_DataSource = string.Empty;
            //MAIG - CH1 - BEGIN - Modified the Old logic for getting the DataSource
            //if (iPolLength == 13)
            //{
            //    PC_DataSource = Config.Setting("DataSource.Exigen");
            //}
            //else 
            if ((ProductType.Equals("DF") || ProductType.Equals("MC") || ProductType.Equals("WC")) && (iPolLength>=4 && iPolLength<=9) )
            {
                PC_DataSource = Config.Setting("DataSource.SIS");
            }
            /*
            if (ProductType == "00")
            {
                if (iPolLength == 7)
                {
                    PC_DataSource = Config.Setting("DataSource.POESAuto");
                }
                else if (iPolLength == 13)
                {
                    PC_DataSource = Config.Setting("DataSource.Exigen");
                }
                else if (iPolLength == 9)
                {
                    PC_DataSource = Config.Setting("DataSource.HUON");
                }
            }*/
            else if (ProductType == "10")
            {
                if (iPolLength == 7)
                {
                    PC_DataSource = Config.Setting("DataSource.POESHome");
                }
            }
            else if (ProductType == "WUA")
            {
                PC_DataSource = Config.Setting("DataSource.SIS");
            }
            else if (ProductType.ToUpper().ToString() == Config.Setting(CSAAWeb.Constants.PC_ProductCode_PUP) && iPolLength==7)
            {
                PC_DataSource = Config.Setting("DataSource.PUP");
            }
                /*
            //PAS HO CH7 - START : Added the below code to include the PAS HO to get the PC DataSource 6/13/2014
            else if ((ProductType.Equals("AAASS")) || (ProductType.Equals("AAASSH")))
            //PAS HO CH7 - END : Added the below code to include the PAS HO to get the PC DataSource 6/13/2014
            {
                PC_DataSource = Config.Setting("DataSource.Exigen");
            }
            else if (ProductType == Config.Setting("ProductCode.HighLimit"))
            {
                PC_DataSource = Config.Setting("DataSource.HL");
            }
            else if (ProductType.ToUpper().ToString() == Config.Setting(CSAAWeb.Constants.PC_ProductCode_PUP))
            {
                PC_DataSource = Config.Setting("DataSource.PUP");
            }
            else if ((Config.Setting("PCP.Products")).IndexOf(Convert.ToString(ProductType)) > -1)
            {
                PC_DataSource = Config.Setting("DataSource.SIS");
            }*/
            //MAIG - CH1 - END - Modified the Old logic for getting the DataSource
            return PC_DataSource;

        }

        /// <summary>
        /// To add the DO's of BO and CC to a list
        /// </summary>
        /// <returns>List</returns>
        public List<string> DO_BO_Mapping()
        {
            List<string> DO_BO = new List<string>(Config.Setting("PC_DO.BO").Split(','));
            return DO_BO;
        }
        public List<string> DO_CC_Mapping()
        {
            List<string> DO_CC = new List<string>(Config.Setting("PC_DO.CC").Split(','));
            return DO_CC;
        }
        /// <summary>
        /// Gets the PC Application Context
        /// </summary>
        /// <param name="AppId">Provides the Application ID </param>
        /// <returns>The PC Application Context</returns>
        private string ApplicationContext(string AppId)
        {
            string application = string.Empty;
            if (AppId.ToUpper().Equals(CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL))
            { application = Config.Setting(CSAAWeb.Constants.PC_ApplicationContext_Payment_Tool); }
            else if (AppId.ToUpper().Equals(CSAAWeb.Constants.PC_APPID_SALESX))
            { application = Config.Setting("ApplicationContext_SalesX"); }
            else
            { application = Config.Setting("ApplicationContext_PaymentX"); }
            return application;
        }

        /// <summary>
        /// Gets the Payment Central Fields by looking up the Database.
        /// </summary>
        /// <param name="S">Object for holding a user's authentication session data.</param>
        /// <param name="ProductCode">Provides the Product code</param>
        /// <param name="DataField">Provides the Column name to get either PC Companycode or ProductCode </param>
        /// <returns>The IDP request Writing company or Type</returns>
        private string LookupPCData(SessionInfo sessionInfo, string productCode, string dataField)
        {
            string PCData = string.Empty;
            DataTable type_ProductTypes = new DataTable();
            try
            {
                OrderClasses.Service.Order type_OrderService = new OrderClasses.Service.Order();
                ArrayOfErrorInfo Errorinfo;
                type_ProductTypes = type_OrderService.LookupDataSet(sessionInfo.Token, sessionInfo.CurrentUser, sessionInfo.AppName, "Insurance", "ProductTypes", out Errorinfo).Tables[CSAAWeb.Constants.INS_Product_Type_Table];
                DataRow[] PolicyType = type_ProductTypes.Select("ID LIKE " + "'" + productCode + "'");
                foreach (DataRow myDataRow in PolicyType)
                    PCData = myDataRow[dataField].ToString();
                //MAIG - CH2 - BEGIN - Added the Code to get the Company Code and Product Code for SalesX Interface
                if (PolicyType.Length == 0 && (System.Configuration.ConfigurationSettings.AppSettings["SalesXType"].IndexOf(productCode)>-1))
                {
                    if (dataField.Equals(CSAAWeb.Constants.PaymentCentral_Product_Code_table))
                    {
                        if (productCode.Equals("WUA")) { PCData = "PA"; } else if(productCode.Equals("10")) { PCData = "HO"; }
                    }
                    else
                    {
                        if (productCode.Equals("WUA")) { PCData = "4WUIC"; } else if (productCode.Equals("10")) { PCData = "CSIIB"; }
                    }
                    
                }
                //MAIG - CH2 - END - Added the Code to get the Company Code and Product Code for SalesX Interface
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
                throw e;
            }
            finally
            {
                type_ProductTypes = null;
            }
            return PCData;
        }

        /// <summary>
        /// Mapping Payment tool Objects to Payment Central to invoke Payment token service
        /// </summary>
        /// <param name="OrderInfo">(OrderInfo) The object which contains Order information.</param>
        /// <param name="SessionInfo">The details of the user.</param>
        /// <returns>MaintainPaymentAccountRequest</returns>
        public MaintainPaymentAccountRequest Tokenmapping(OrderClasses.OrderInfo orderInfo, SessionInfo sessionInfo)
        {
            MaintainPaymentAccountRequest accountRequest = new MaintainPaymentAccountRequest();
            try
            {
                paymentAccountMaintainRequest maintainRequest = new paymentAccountMaintainRequest();
                accountRequest.MaintainPaymentAccountRequest1 = maintainRequest;
                maintainRequest.paymentSourceSystem = ApplicationContext(orderInfo.S.AppName);
                maintainRequest.userId = orderInfo.User.UserId;
                maintainRequest.paymentCardHolderName = string.Empty;
                maintainRequest.paymentCardNumber = string.Empty;
                maintainRequest.paymentAccountToken = string.Empty;
                if (orderInfo.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.CreditCard)
                {
                    maintainRequest.paymentCardHolderName = orderInfo.Addresses.BillTo.FirstName + " " + orderInfo.Addresses.BillTo.LastName;
                    maintainRequest.paymentAcctFopType = Config.Setting(CSAAWeb.Constants.PC_CreditCard_Code);
                    maintainRequest.paymentCardNumber = orderInfo.Card.CCNumber;
                    maintainRequest.paymentCardExpirationDateSpecified = true;
                    maintainRequest.paymentCardExpirationDate = Convert.ToDateTime(orderInfo.Card.CCExpMonth + "/" + orderInfo.Card.CCExpYear);
                    maintainRequest.paymentCardCity = orderInfo.Addresses.BillTo.City;
                    maintainRequest.paymentCardState = orderInfo.Addresses.BillTo.State;
                    maintainRequest.paymentCardZip = orderInfo.Addresses.BillTo.Zip;
                }
                else
                {
                    maintainRequest.paymentBankAccountHolderName = orderInfo.Addresses.BillTo.FirstName + " " + orderInfo.Addresses.BillTo.LastName;
                    maintainRequest.paymentAcctFopType = Config.Setting(CSAAWeb.Constants.PC_ElectronicFund_Code);
                    if (orderInfo.echeck.BankAcntType == "S")
                    {
                        maintainRequest.paymentBankAccountType = Config.Setting("EFT_SAVING");
                    }
                    else if (orderInfo.echeck.BankAcntType == "C")
                    {
                        maintainRequest.paymentBankAccountType = Config.Setting("EFT_CHECKING");
                    }
                    maintainRequest.fiRoutingNumber = orderInfo.echeck.BankId;
                    maintainRequest.paymentBankAccountNumber = orderInfo.echeck.BankAcntNo;
                }
                maintainRequest.saveForFuture = false;
                return accountRequest;
            }
            catch (FaultException faultExp)
            {
                return null;
                throw faultExp;
            }

            catch (Exception e)
            {
                Logger.Log(e.ToString());
                return null;
            }
        }

        //CHG0072116 - PC Edit Card Details CH1:START-Overloaded the "Tokenmapping" method with the parameter "PaymentAuthToken" to consume "MaintainPaymentAccount" service.
        /// <summary>
        /// Mapping Payment tool Objects to Payment Central to invoke Payment token service
        /// </summary>
        /// <param name="OrderInfo">(OrderInfo) The object which contains Order information.</param>
        /// <param name="SessionInfo">The details of the user.</param>
        /// <param name="paymentAuthToken">payment token which is retrived from retriveautoenrollment service</param>
        /// <returns>MaintainPaymentAccountRequest</returns>
        public MaintainPaymentAccountRequest Tokenmapping(OrderClasses.OrderInfo orderInfo, SessionInfo sessionInfo, string paymentAuthToken)
        {
            MaintainPaymentAccountRequest accountRequest = new MaintainPaymentAccountRequest();
            try
            {
                paymentAccountMaintainRequest maintainRequest = new paymentAccountMaintainRequest();
                accountRequest.MaintainPaymentAccountRequest1 = maintainRequest;
                maintainRequest.paymentSourceSystem = ApplicationContext(orderInfo.S.AppName);
                maintainRequest.userId = orderInfo.User.UserId;
                maintainRequest.paymentCardHolderName = string.Empty;
                maintainRequest.paymentCardNumber = string.Empty;
                maintainRequest.paymentAccountToken = paymentAuthToken;
                if (orderInfo.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.CreditCard)
                {
                    maintainRequest.paymentCardHolderName = orderInfo.Addresses.BillTo.FirstName + " " + orderInfo.Addresses.BillTo.LastName;
                    maintainRequest.paymentAcctFopType = Config.Setting(CSAAWeb.Constants.PC_CreditCard_Code);
                    maintainRequest.paymentCardExpirationDateSpecified = true;
                    maintainRequest.paymentCardExpirationDate = Convert.ToDateTime(orderInfo.Card.CCExpMonth + "/" + orderInfo.Card.CCExpYear);
                    maintainRequest.paymentCardCity = orderInfo.Addresses.BillTo.City;
                    maintainRequest.paymentCardState = orderInfo.Addresses.BillTo.State;
                    maintainRequest.paymentCardZip = orderInfo.Addresses.BillTo.Zip;
                }
                return accountRequest;
            }
            catch (FaultException faultExp)
            {
                return null;
                throw faultExp;
            }

            catch (Exception e)
            {
                Logger.Log(e.ToString());
                return null;
            }
        }

        //CHG0072116 - PC Edit Card Details CH1:END-Overloaded the "Tokenmapping" method with the parameter "PaymentAuthToken" to consume "MaintainPaymentAccount" service.

        /// <summary>
        /// Method to invoke the Payment token service of Payment Central
        /// </summary>
        /// <param name="MaintainPaymentAccountRequest">Valid MaintainPaymentAccountRequest object is passed </param>
        /// <returns>MaintainPaymentAccountResponse</returns>
        public MaintainPaymentAccountResponse InvokePaymentTokenService(MaintainPaymentAccountRequest req)
        {
            MaintainPaymentAccountResponse rsptoken = null;
            //CHGXXXXXXX- SSL\TLS -Added the below code for reading the security protocol type - Start
            string protocolType = Config.Setting("ProtocolType").ToUpper();
            //CHGXXXXXXX- SSL\TLS -Added the below code for reading the security protocol type - End
            try
            {
                RecordFinancialAccount _tokenProvider = (RecordFinancialAccountChannel)CreateInstance<RecordFinancialAccountChannel>("VaultAjaxEndPointImplPort");
                if (Config.Setting("Logging.MaintainPaymentAccount") == "1")
                {
                    System.Xml.Serialization.XmlSerializer serializerReq = new System.Xml.Serialization.XmlSerializer(req.GetType());
                    StringWriter writerReq = new StringWriter();
                    serializerReq.Serialize(writerReq, req);
                    Logger.Log("Payment request to Payment Central - Payment token Request : \r\n" + writerReq);
                }
                if (req != null)
                {
                    ////CHGXXXXXXX- SSL\TLS - Added the below code for setting the security protocol type - Start
                    if (protocolType == "SSL3")
                    {
                        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Ssl3;
                    }
                    else if (protocolType == "TLS12")
                    {
                        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12;
                    }
                    else if (protocolType == "TLS")
                    {
                        System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls;
                    }
                    ////CHGXXXXXXX- SSL\TLS - Added the below code for setting the security protocol type - End
                    
                    //Payment Token Service call
                    rsptoken = _tokenProvider.MaintainPaymentAccount(req);
                    if (Config.Setting("Logging.MaintainPaymentAccount") == "1")
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(rsptoken.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, rsptoken);
                        Logger.Log("Payment response from Payment Central - Payment token Response : \r\n" + writerRes);
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
                throw e;
            }
            return rsptoken;
        }
        /// <summary>
        /// Mapping of Payment tool Objects to Payment Central 
        /// </summary>
        /// <param name="info">OrderInfo contains all the information that an order needs to retain from page to page.</param>
        /// <param name="S">Class for holding a user's authentication session data.</param>
        /// <param name="Token"></param>
        /// <returns>IssueDirectPaymentRequest</returns>
        public issueDirectPaymentRequest PTtoPCMapping(OrderClasses.OrderInfo info, SessionInfo sessionInfo, string token)
        {
            issueDirectPaymentRequest directPaymentRequest = new issueDirectPaymentRequest();
            try
            {
                //Issue Direct Payment service request object is filled with all needed params
                directPaymentRequest.applicationContext = new IDP.ApplicationContext();
                directPaymentRequest.applicationContext.application = ApplicationContext(sessionInfo.AppName);
                //CHG0083477 - Inclusion of RepID in IDP Service - Start.
				//MAIG - CH3 - BEGIN - Modified the Code to pass the Agent ID & DO to IDP Service
                if(info.User.UserId.Length > 9)
                {
                    directPaymentRequest.agentIdentifier = Convert.ToString(info.User.UserId).Substring(1);
                }
                else if (info.User.RepId != 0)
                {
                        directPaymentRequest.agentIdentifier = Convert.ToString(info.User.RepId);
                }
                    else
                {
                        directPaymentRequest.agentIdentifier = null;
                }
                //CHG0083477 - Inclusion of RepID in IDP Service - End.
                //Added the below mappings for DO similar to Remittance feed file
                string IDP_DO = string.Empty;
                if (string.IsNullOrEmpty(info.User.DO) || (info.User.DO == "0") || (info.User.DO.Length > 3))
                {
                    IDP_DO = CSAAWeb.Constants.PC_Default_DO;
                }
                else
                {
                    IDP_DO = info.User.DO;
                }
                directPaymentRequest.districtOffice = IDP_DO;
				//MAIG - CH3 - END - Modified the Code to pass the Agent ID & DO to IDP Service
                directPaymentRequest.userId = sessionInfo.CurrentUser;
                //MAIG - CH4 - BEGIN - Added logic to send the Email Address to IDP Service
                directPaymentRequest.agency = info.User.AgencyID.ToString();
                if (info.Products["Insurance"] != null)
                {
                    InsuranceClasses.InsuranceInfo InsInfo = (InsuranceClasses.InsuranceInfo)info.Products["Insurance"];
                    if (InsInfo.Addresses.Count>0)
                    {
                        directPaymentRequest.emailTo = InsInfo.Addresses[0].Email;
                    }
                }
                //MAIG - CH4 - END - Added logic to send the Email Address to IDP Service

                info.Detail.MerchantRefNum = pcDataconnection.GetMerchantReference(sessionInfo.AppName);
                directPaymentRequest.externalReferenceNumber = info.Detail.MerchantRefNum;
                directPaymentRequest.totalAmountSpecified = true;
                directPaymentRequest.totalAmount = Convert.ToDecimal(info.Detail.Amount);
                directPaymentRequest.lineItems = new LineItems();
                directPaymentRequest.lineItems.lineItem = new IDP.LineItem[info.Lines.Count];
                for (int i = 0; i < info.Lines.Count; i++)
                {
                    directPaymentRequest.lineItems.lineItem[i] = new IDP.LineItem();
                    directPaymentRequest.lineItems.lineItem[i].amountSpecified = true;
                    directPaymentRequest.lineItems.lineItem[i].amount = Convert.ToDecimal(info.Lines[i].Amount);

                    directPaymentRequest.lineItems.lineItem[i].policyInfo = new PolicyProductSource();
                    //MAIG - CH5 - BEGIN - Removed the logic to fetch the Policy State and Policy Prefix from Account Number and included the Policy State as an parameter to IDp Service
                    string[] appendedData = { };
                    appendedData = info.Lines[i].SubProduct.Split('-');
                    /*string[] strSplitPolicy;
                    strSplitPolicy = info.Lines[i].AccountNumber.Split('-');
                    string Policy = strSplitPolicy[0].Trim(); */

                    //Mapping for Revenue Type

                    directPaymentRequest.lineItems.lineItem[i].policyInfo.riskState = info.Detail.PolicyState;
                    //MAIG - CH5 - END - Removed the logic to fetch the Policy State and Policy Prefix from Account Number and included the Policy State as an parameter to IDp Service

                    if (info.Lines[i].RevenueType.Equals(Constants.PC_REVENUE_ERND))
                    {
                        directPaymentRequest.lineItems.lineItem[i].amountFor = Constants.PC_REVENUETYPE_ERND;
                    }
                    else if (info.Lines[i].RevenueType.Equals(Constants.PC_REVENUE_INST))
                    {
                        directPaymentRequest.lineItems.lineItem[i].amountFor = Constants.PC_REVENUETYPE_INST;
                    }
                    else if (info.Lines[i].RevenueType.Equals(Constants.PC_REVENUE_DOWN))
                    {
                        directPaymentRequest.lineItems.lineItem[i].amountFor = Constants.PC_REVENUETYPE_DOWN;
                    }
                    //Mapping for Authentication Channel
                    if (info.User.DO.Equals(CSAAWeb.Constants.PC_Default_DO))
                    {
                        directPaymentRequest.authenticationChannel = Constants.PC_AUTHCH_CC;
                    }
                    else if (sessionInfo.AppName.Equals(Constants.PC_APPID_PAYMENT_TOOL))
                    {
                        List<string> DO_BO = DO_BO_Mapping();
                        List<string> DO_CC = DO_CC_Mapping();
                        if (info.User.DO.Equals(CSAAWeb.Constants.PC_DO_AUTHIVR))
                        {
                            directPaymentRequest.authenticationChannel = Constants.PC_AUTHCH_IVR;
                        }
                        else if (DO_BO.Contains(info.User.DO))
                        {
                            directPaymentRequest.authenticationChannel = Constants.PC_AUTHCH_BO;
                        }
                        else if (DO_CC.Contains(info.User.DO))
                        {
                            directPaymentRequest.authenticationChannel = Constants.PC_AUTHCH_CC;
                        }
                        else
                        {
                            directPaymentRequest.authenticationChannel = Constants.PC_AUTHCH_F2F;
                        }
                    }
                    else if (sessionInfo.AppName.Equals(Config.Setting("ByPassTokenVldn.OrderService.AppName").ToString()))
                    {
                        directPaymentRequest.authenticationChannel = Constants.PC_AUTHCH_ONL;
                    }
                    else if (sessionInfo.AppName.Equals("IVR"))
                    {
                        directPaymentRequest.authenticationChannel = Constants.PC_AUTHCH_IVR;
                    }
                    else
                    {
                        directPaymentRequest.authenticationChannel = Constants.PC_AUTHCH_F2F;
                    }

                    //MAIG - CH6 - BEGIN - Removed the logic to check if it is an PCP product and updated the logic for PUP policies
                    //Added Policy Prefix 
                    //if ((Config.Setting("PCP.Products").IndexOf(Convert.ToString(info.Lines[i].ProductTypeNew)) > -1))
                    //{
                        directPaymentRequest.lineItems.lineItem[i].policyInfo.policyPrefix = info.Detail.PolicyPrefix;
                        //Policy = strSplitPolicy[2].Trim();
                    /*}
                    else if ((Config.Setting("ProductType.Auto").IndexOf(Convert.ToString(info.Lines[i].ProductTypeNew)) > -1) && (Policy.Length == 9))
                    {
                        directPaymentRequest.lineItems.lineItem[i].policyInfo.policyPrefix = CSAAWeb.Constants.PC_HUONPREFIX;
                    }
                        if (sessionInfo.AppName.ToUpper().Equals(CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL)) //|| (Config.Setting("PCP.Products").IndexOf(Convert.ToString(info.Lines[i].ProductTypeNew)) > -1))
                        {*/
                            directPaymentRequest.lineItems.lineItem[i].policyInfo.policyNumber = info.Lines[i].AccountNumber;
                    /*
                        }
                        //Added the below condition to remove the prefix PUP from the policy number for the PUP product
                        if (info.Lines[i].ProductTypeNew.ToUpper().ToString() == Config.Setting(CSAAWeb.Constants.PC_ProductCode_PUP))
                        {
                            directPaymentRequest.lineItems.lineItem[i].policyInfo.policyNumber = Policy.Substring(3, 7);
                        }
                        else
                        {
                            directPaymentRequest.lineItems.lineItem[i].policyInfo.policyNumber = Policy;
                        }
                    }
                    else
                    {
                        directPaymentRequest.lineItems.lineItem[i].policyInfo.policyNumber = info.Lines[i].AccountNumber;
                    } */
                        //MAIG - CH6 - END - Removed the logic to check if it is an PCP product and updated the logic for PUP policies
                    //MAIG - CH7 - BEGIN - Fetch the CompanyCode and Source System from OrderDetail Object
                    //directPaymentRequest.lineItems.lineItem[i].policyInfo.dataSource =  DataSource(info.Lines[i].ProductTypeNew, Policy.Length);
                        if (appendedData.Length > 2)
                        {
                            if (!string.IsNullOrEmpty(appendedData[2]))
                            {
                                directPaymentRequest.lineItems.lineItem[i].policyInfo.dataSource = appendedData[2];
                            }
                            if (!string.IsNullOrEmpty(appendedData[1]))
                            {
                                directPaymentRequest.lineItems.lineItem[i].policyInfo.writingCompany = appendedData[1];
                            }
                        }
                        else
                        {
                            directPaymentRequest.lineItems.lineItem[i].policyInfo.dataSource = DataSource(info.Lines[i].ProductTypeNew, info.Lines[i].AccountNumber.Length);
                            directPaymentRequest.lineItems.lineItem[i].policyInfo.writingCompany = LookupPCData(sessionInfo, info.Lines[i].ProductTypeNew, "PaymentCentral_Company_Code");
                        }
                    /*if ((sessionInfo.AppName.ToUpper().Equals(CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL) && (Config.Setting("PCP.Products").IndexOf(Convert.ToString(info.Lines[i].ProductTypeNew)) > -1)))
                    {
                        // Account Number has [ Account Number - Policy State - Policy Prefix - Company Code ]
                        directPaymentRequest.lineItems.lineItem[i].policyInfo.writingCompany = strSplitPolicy[3].Trim();
                    }
                    else if (sessionInfo.AppName.ToUpper().Equals(CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL))
                    {
                        // Account Number has [ Account Number - Company Code - Check Number ]
                        if (strSplitPolicy[1].Trim() == "")
                        {
                            strSplitPolicy[1] = CSAAWeb.Constants.PC_COMPANY_CODE;
                        }
                        directPaymentRequest.lineItems.lineItem[i].policyInfo.writingCompany = strSplitPolicy[1].Trim();
                    }
                    else
                    {
                        //Other Interfaces but PCP Products
                        if ((Config.Setting("PCP.Products").IndexOf(Convert.ToString(info.Lines[i].ProductTypeNew)) > -1))
                        {
                            DataConnection get_company_cd = new DataConnection();
                            directPaymentRequest.lineItems.lineItem[i].policyInfo.writingCompany = get_company_cd.Get_Companycode(info.Detail.PolicyPrefix, info.Detail.PolicyState, info.Lines[i].ProductTypeNew);
                            //Appending Company code Field. [ Account Number - Company Code ]
                            info.Lines[i].AccountNumber = info.Lines[i].AccountNumber + "-" + directPaymentRequest.lineItems.lineItem[i].policyInfo.writingCompany;
                        }
                        else
                        {
                            directPaymentRequest.lineItems.lineItem[i].policyInfo.writingCompany = LookupPCData(sessionInfo, info.Lines[i].ProductTypeNew, "PaymentCentral_Company_Code");
                            //Appending Company code Field. [ Account Number - Company Code ]
                            info.Lines[i].AccountNumber = info.Lines[i].AccountNumber + "-" + directPaymentRequest.lineItems.lineItem[i].policyInfo.writingCompany;
                        }
                    } */
                    //MAIG - CH7 - END - Fetch the CompanyCode and Source System from OrderDetail Object
                    directPaymentRequest.lineItems.lineItem[i].policyInfo.type = LookupPCData(sessionInfo, info.Lines[i].ProductTypeNew, CSAAWeb.Constants.PaymentCentral_Product_Code_table);
                    directPaymentRequest.paymentItems = new PaymentItems();
                    directPaymentRequest.paymentItems.paymentItem = new PaymentItem[info.Lines.Count];
                    directPaymentRequest.paymentItems.paymentItem[i] = new PaymentItem();
                    if (info.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.CreditCard)
                    {
                        directPaymentRequest.paymentItems.paymentItem[i].paymentMethod = Config.Setting(CSAAWeb.Constants.PC_CreditCard_Code);
                        directPaymentRequest.paymentItems.paymentItem[i].card = new PaymentCard();
                        directPaymentRequest.paymentItems.paymentItem[i].paymentAccountToken = token;
                        directPaymentRequest.paymentItems.paymentItem[i].card.isCardPresent = true;
                        directPaymentRequest.paymentItems.paymentItem[i].card.isCardPresentSpecified = true;
                        directPaymentRequest.paymentItems.paymentItem[i].card.zipCode = info.Addresses[0].Zip;
                        directPaymentRequest.paymentItems.paymentItem[i].card.expirationDateSpecified = true;
                        string date = info.Card.CCExpYear + "-" + info.Card.CCExpMonth + "-" + 01;
                        directPaymentRequest.paymentItems.paymentItem[i].card.expirationDate = Convert.ToDateTime(info.Card.CCExpMonth + "-" + info.Card.CCExpYear);// DateTime.Parse(date);
                    }
                    else if (info.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.ECheck)
                    {
                        directPaymentRequest.paymentItems.paymentItem[i].paymentMethod = Config.Setting(CSAAWeb.Constants.PC_ElectronicFund_Code);
                        directPaymentRequest.paymentItems.paymentItem[i].paymentAccountToken = token;
                    }
                    else if (info.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.Check)
                    {
                        directPaymentRequest.paymentItems.paymentItem[i].paymentMethod = Config.Setting("CHECK_Code");
                        //MAIG - CH8 - BEGIN - Fetch the Check Number from the Optional Order Object Detail Field and remove the append logic. 
                        if (sessionInfo.AppName.ToUpper().Equals(CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL) && appendedData.Length>0)//!(Config.Setting("PCP.Products").IndexOf(Convert.ToString(info.Lines[i].ProductTypeNew)) > -1)))
                        {
                            //directPaymentRequest.paymentItems.paymentItem[i].checkNumber = strSplitPolicy[2];
                            if (!string.IsNullOrEmpty(appendedData[0]))
                            {
                                directPaymentRequest.paymentItems.paymentItem[i].checkNumber = appendedData[0];
                            }
                        }
                        //MAIG - CH* - END - Fetch the Check Number from the Optional Order Object Detail Field and remove the append logic. 
                        else
                        {
                            directPaymentRequest.paymentItems.paymentItem[i].checkNumber = "1234";
                        }
                    }
                    else
                    {
                        directPaymentRequest.paymentItems.paymentItem[i].paymentMethod = Config.Setting("CASH_Code");
                    }
                    directPaymentRequest.paymentItems.paymentItem[i].amountSpecified = true;
                    directPaymentRequest.paymentItems.paymentItem[i].amount = Convert.ToDecimal(info.Detail.Amount);
                }

            }

            catch (Exception e)
            {
                Logger.Log(e.Message);
                directPaymentRequest = null;
                throw e;

            }
            return directPaymentRequest;

        }


        /// <summary>
        /// Processing of Insurance Transaction by IDP Service
        /// </summary>
        /// <param name="info">OrderInfo contains all the information that an order needs to retain from page to page.</param>
        /// <param name="s">Class for holding a user's authentication session data.</param>
        public void ProcessInsurance(OrderClasses.OrderInfo info, SessionInfo sessionInfo)
        {
            bool errorFlag = false;
            string token = string.Empty;
            IssueDirectPaymentRequest1 issueDirectPaymentRequest1Obj = null;
            issueDirectPaymentRequest1Obj = new IssueDirectPaymentRequest1();
            IssueDirectPaymentResponse1 paymentResponse = new IssueDirectPaymentResponse1();
            MaintainPaymentAccountRequest inputRequest = new MaintainPaymentAccountRequest();
            MaintainPaymentAccountResponse tokenResponse = new MaintainPaymentAccountResponse();
            try
            {
                // CHGXXXXXXJ - CH2 - Changes made for splitting the token service - 27/06/2017.- Begin
                System.Web.HttpContext.Current.Items.Add("CallIDP", "Yes");
                if ((info.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.CreditCard || info.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.ECheck) 
                    && (System.Web.HttpContext.Current.Items["IsPaymentTokenGenerated"] == "No"))
                {
                    //Payment Token Service request is mapped.
                    inputRequest = Tokenmapping(info, sessionInfo);
                    if (inputRequest != null)
                    {
                        //Invokes the Payment Token Service
                        tokenResponse = InvokePaymentTokenService(inputRequest);
                    }
                    if (tokenResponse != null)
                    {
                        token = tokenResponse.MaintainPaymentAccountResponse1.paymentAccountToken;
                    }
                              
                    else
                    {
                        //Added the below code to set Error flag if response object is null
                        Logger.Log(CSAAWeb.Constants.PC_ERR_RESPONSE_NULL);
                        info.Errors[0].Message = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                        errorFlag = true;
                    }

                    System.Web.HttpContext.Current.Items["IsPaymentTokenGenerated"] = "Yes";
                    System.Web.HttpContext.Current.Items["CallIDP"]= "No";
                    // CHGXXXXXXJ - CH2 - Changes made for splitting the token service - 27/06/2017- End
                }
         
                //IDP Service request is mapped.
                if ((!errorFlag) && System.Web.HttpContext.Current.Items["CallIDP"] == "Yes")
                {
                    token = System.Web.HttpContext.Current.Items["token"].ToString();
                    issueDirectPaymentRequest1Obj.issueDirectPaymentRequest = PTtoPCMapping(info, sessionInfo, token);
                    if (Config.Setting("Logging.IssueDirectPayment") == "1")
                    {
                        if (issueDirectPaymentRequest1Obj != null)
                        {
                            System.Xml.Serialization.XmlSerializer serializerReq = new System.Xml.Serialization.XmlSerializer(issueDirectPaymentRequest1Obj.GetType());
                            StringWriter writerReq = new StringWriter();
                            serializerReq.Serialize(writerReq, issueDirectPaymentRequest1Obj);
                            Logger.Log("Payment request to Payment Central - IDP Request : \r\n" + writerReq);
                        }
                    }
                    IssueDirectPayment _paymentProvider = (IssueDirectPaymentChannel)CreateInstance<IssueDirectPaymentChannel>("IssueDirectPaymentSOAPPort");
                    //IDP Service Call.
                    paymentResponse = _paymentProvider.IssueDirectPayment(issueDirectPaymentRequest1Obj);

                    ////// MAIG - Begin - Added logic to check if the CompanyID is NULL in OrderInfo, get CompID from PaymentRequest & set it in OrderInfo
                    if (string.IsNullOrEmpty(info.Lines[0].SubProduct))
                        info.Lines[0].SubProduct = "-" + issueDirectPaymentRequest1Obj.issueDirectPaymentRequest.lineItems.lineItem[0].policyInfo.writingCompany.ToString();                                                                
                    ////// MAIG - End - Added logic to check if the CompanyID is NULL in OrderInfo, get CompID from PaymentRequest & set it in OrderInfo

                    if (Config.Setting("Logging.IssueDirectPayment") == "1" || Config.Setting("Logging.MaintainPaymentAccount") == "1")
                    {
                        if (paymentResponse != null)
                        {
                            System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(paymentResponse.GetType());
                            StringWriter writerRes = new StringWriter();
                            serializerRes.Serialize(writerRes, paymentResponse);
                            Logger.Log("Payment Response from Payment Central - IDP Response : \r\n" + writerRes);
                        }
                        else
                        {
                            //Added the below code to set Error flag if response object is null
                            Logger.Log(CSAAWeb.Constants.PC_ERR_RESPONSE_NULL);
                            info.Errors[0].Message = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                            errorFlag = true;
                        }
                    }
                }
            }
            catch (FaultException<IDP.ErrorInfo> faultExpErrorInfo)
            {
                if (Config.Setting("Logging.IssueDirectPayment") == "1" || Config.Setting("Logging.MaintainPaymentAccount") == "1")
                { LogServiceRequestResponseForPayment(paymentResponse, tokenResponse); }
                if (faultExpErrorInfo.Detail != null)
                {
                    Logger.Log(faultExpErrorInfo.Detail.errorMessageText);
                    info.Errors[0].Code = faultExpErrorInfo.Detail.errorCode;
                    info.Errors[0].Message = faultExpErrorInfo.Detail.errorMessageText;
                    errorFlag = true;
                }
            }
            catch (FaultException faultExp)
            {
                info = HandleFaultException(faultExp, paymentResponse, tokenResponse, info);
                errorFlag = true;

            }
            catch (SoapException soapExp)
            {
                if (Config.Setting("Logging.IssueDirectPayment") == "1" || Config.Setting("Logging.MaintainPaymentAccount") == "1")
                { LogServiceRequestResponseForPayment(paymentResponse, tokenResponse); }
                if (soapExp.Message != null)
                {
                    Logger.Log(soapExp.Message);
                    info.Errors[0].Code = soapExp.Code.ToString();
                    info.Errors[0].Message = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                    errorFlag = true;
                }
            }
            catch (Exception exp)
            {
                if (Config.Setting("Logging.IssueDirectPayment") == "1" || Config.Setting("Logging.MaintainPaymentAccount") == "1")
                { LogServiceRequestResponseForPayment(paymentResponse, tokenResponse); }
                if (exp.Message != null)
                {
                    Logger.Log(exp.Message);
                    info.Errors[0].Message = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                    errorFlag = true;
                }
            }
            try
            {
                //CHGXXXXXXJ - CH2 - Changes made for splitting the token service - 27/06/2017-BEGIN
                if (paymentResponse.issueDirectPaymentResponse != null)
                {
                    info = InsertData(errorFlag, paymentResponse, info, sessionInfo);
                }
                //CHGXXXXXXJ - CH2 - Changes made for splitting the token service - 27/06/2017-END.
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
                info.Errors[0].Message = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
            }
            finally
            {
                //MAIG - CH9 - BEGIN - Removed the logic to append the details to Account Number field
                for (int i = 0; i < info.Lines.Count; i++)
                {
                    /*if (!info.S.AppName.Equals(CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL) && !(Config.Setting("PCP.Products").IndexOf(Convert.ToString(info.Lines[i].ProductTypeNew)) > -1))
                    {
                        if (info.Lines[i].AccountNumber.Contains("-"))
                        {
                            //Removes the Appended values from Account Number before passing the Order Object 
                            info.Lines[i].AccountNumber = info.Lines[i].AccountNumber.Split('-')[0];
                        }
                    }*/
                    info.Lines[i].SubProduct = token;
                }
                //MAIG - CH9 - END - Removed the logic to append the details to Account Number field
                issueDirectPaymentRequest1Obj = null;
                paymentResponse = null;
                inputRequest = null;
                tokenResponse = null;
            }

        }

        private OrderClasses.OrderInfo HandleFaultException(FaultException faultExp, IssueDirectPaymentResponse1 response, MaintainPaymentAccountResponse tokenResponse, OrderClasses.OrderInfo info)
        {
            string errFriendlyMsg = string.Empty, errMsg = string.Empty, errCode = string.Empty, appendErrMsg = string.Empty;
            if (Config.Setting("Logging.IssueDirectPayment") == "1" || Config.Setting("Logging.MaintainPaymentAccount") == "1")
            {
                LogServiceRequestResponseForPayment(response, tokenResponse);
            }
            if (((System.Reflection.MethodInfo)(((System.Exception)(faultExp)).TargetSite)).Name.Trim().ToUpper() == "INVOKEPAYMENTTOKENSERVICE")
            {
                FaultException<aaaie.com.recordfinancialaccount.endpoint.FaultInfo> errInfo = (FaultException<aaaie.com.recordfinancialaccount.endpoint.FaultInfo>)faultExp;
                if (errInfo.Detail != null)
                {
                    foreach (XmlNode node in errInfo.Detail.Nodes)
                    {
                        if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_FRIENDLY_ERROR_MSG))
                            errFriendlyMsg = node.InnerText;
                        if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_ERROR_MSG))
                            errMsg = node.InnerText;
                        if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_ERROR_CODE))
                            errCode = node.InnerText;
                    }
                }

            }
            else
            {
                FaultException<IDP.www.aaancnuit.com.aaancnu_common_version2.ErrorInfo> errInfo = (FaultException<IDP.www.aaancnuit.com.aaancnu_common_version2.ErrorInfo>)faultExp;
                if (errInfo.Detail != null)
                {
                    foreach (XmlNode node in errInfo.Detail.Nodes)
                    {
                        if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_FRIENDLY_ERROR_MSG))
                            errFriendlyMsg = node.InnerText;

                        if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_ERROR_MSG_TEXT))
                            errMsg = node.InnerText;
                        if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_ERROR_CODE))
                            errCode = node.InnerText;
                    }
                }

            }
            //Error Msg Handling - BEGIN - CHG0112662 - Appended the condition for error code 300 to the existing fault exception handling & Removed Error Code in actual error message.
            if (errCode.Contains(CSAAWeb.Constants.ERR_CODE_BUSINESS_EXCEPTION) || errCode.Contains(CSAAWeb.Constants.PC_ERR_CODE_TOKEN_BUSINESS_EXCEPTION)
                                                                                || errCode.Contains(CSAAWeb.Constants.PC_ERR_CODE_ROUTING_NUMBER_NOT_FOUND))
            {
                appendErrMsg = CSAAWeb.Constants.PC_ERR_BUSINESS_EXCEPTION + errMsg;
            }
            else
            {
                appendErrMsg = errCode + "-" + CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
            }
            //Error Msg Handling - END - CHG0112662 - Appended the condition for error code 300 to the existing fault exception handling & Removed Error Code in actual error message.
            ArrayOfErrorInfo arr = new ArrayOfErrorInfo();
            arr.Add(new CSAAWeb.Serializers.ErrorInfo(errCode, appendErrMsg, ""));
            info.Errors = arr;
            Logger.Log("User: " + info.S.CurrentUser + " " + errCode + " " + errMsg);
            return info;

        }


        private OrderClasses.OrderInfo InsertData(Boolean errorFlag, IssueDirectPaymentResponse1 response, OrderClasses.OrderInfo info, SessionInfo sessionInfo)
        {
            //Added the below check since crossSequenceNumber will be NULL for UI and PaymentX transactions 
            if (string.IsNullOrEmpty(info.Detail.CrossReferenceNumber))
            {
                info.Detail.CrossReferenceNumber = string.Empty;
            }
            //Transaction results are inserted in Database 
            //MAIG - CH10 - BEGIN - Added a List to send the Policy details to DB insertion which was appeneded in AccountNumber field earlier
            List<string> InsData = new List<string>();
            if (!string.IsNullOrEmpty(info.Detail.PolicyState))
            {
                InsData.Add(info.Detail.PolicyState);
            }
            else { InsData.Add(""); }
            if (!string.IsNullOrEmpty(info.Detail.PolicyPrefix))
            {
                InsData.Add(info.Detail.PolicyPrefix);
            }
            else { InsData.Add(""); }
            string[] appendedData = { };

            appendedData = info.Lines[0].SubProduct.Split('-');
            if (appendedData.Length>1 && !string.IsNullOrEmpty(appendedData[1])) //CompanyCode
            {
                InsData.Add(appendedData[1]);
            }
            else { InsData.Add(""); }
            if (appendedData.Length>0 && !string.IsNullOrEmpty(appendedData[0]))//CheckNumber
            {
                InsData.Add(appendedData[0]);
            }
            else { InsData.Add(""); }
            if (info.Products["Insurance"] != null)
            {
                InsuranceClasses.InsuranceInfo InsInfo = (InsuranceClasses.InsuranceInfo)info.Products["Insurance"];
                if (InsInfo.Addresses.Count > 0)
                {
                    InsData.Add(InsInfo.Addresses[0].Zip);
                }
                else { InsData.Add(""); }
            }
            if (response != null && errorFlag.Equals(false))
            {
                info.Detail.ReceiptNumber = response.issueDirectPaymentResponse.receiptNumber;
                string Status = CSAAWeb.Constants.PC_SUCESS.ToUpper();
                if (response.issueDirectPaymentResponse.statusDescription.Equals("SUCC"))
                {
                    if (info.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.Cash || info.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.Check)
                    {
                        bool IsAutoApproved = false;
                        //MAIG - CH10 - Added a List to send the Policy details to DB insertion which was appeneded in AccountNumber field earlier
                        if (info.Lines.Count > 0)
                        {
                            //MAIGEnhancement CHG0107527 - CH1 - BEGIN - Modified the below code to auto approve for Users having PS User role and non PS User having a KIC Policy if the payment is success
                            if (info.Lines[0].AccountNumber.Trim().Length == 8 && (info.Lines[0].ProductTypeNew.Equals("PA") || info.Lines[0].ProductTypeNew.Equals("HO")))
                            {
                                IsAutoApproved = true;
                            }

                            if (info.User.RoleNames.ToLower().Contains("pss"))
                            {
                                //If PS User User has processed a Cash/Check policy then mark the Poicy status as Approved directly
                                IsAutoApproved = true;
                            }
                            //MAIGEnhancement CHG0107527 - CH1 - END - Modified the below code to auto approve for Users having Ps User role and non PS User having a KIC Policy if the payment is success
                        }
                        pcDataconnection.pcCashCheck(info.User, sessionInfo.AppName, info.Detail.PaymentType, info.Detail.ReceiptNumber, response.issueDirectPaymentResponse.externalReferenceNumber, new ArrayOfLineItem(info.Lines), Status, info.Detail.CrossReferenceNumber, InsData,IsAutoApproved);
                    }
                    else if (info.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.CreditCard)
                    {
                        //MAIG - CH10 - Added a List to send the Policy details to DB insertion which was appeneded in AccountNumber field earlier
                        pcDataconnection.pcCreditCard(info.User, sessionInfo.AppName, info.Detail.PaymentType, info.Detail.ReceiptNumber, response.issueDirectPaymentResponse.externalReferenceNumber, response.issueDirectPaymentResponse.paymentItems.paymentItem[0].card.paymentAuthorization.returnCode, new CardInfo(info.Card), null, new BillToInfo(info.Addresses.BillTo), new ArrayOfLineItem(info.Lines), CSAAWeb.Constants.PC_SUCESS.ToUpper(), info.Detail.CrossReferenceNumber, InsData);
                        info.Detail.AuthCode = response.issueDirectPaymentResponse.paymentItems.paymentItem[0].card.paymentAuthorization.returnCode;
                    }
                    else if (info.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.ECheck)
                    {
                        pcDataconnection.pcCreditCard(info.User, sessionInfo.AppName, info.Detail.PaymentType, info.Detail.ReceiptNumber, response.issueDirectPaymentResponse.externalReferenceNumber, response.issueDirectPaymentResponse.paymentItems.paymentItem[0].card.paymentAuthorization.returnCode, null, new eCheckInfo(info.echeck), new BillToInfo(info.Addresses.BillTo), new ArrayOfLineItem(info.Lines), CSAAWeb.Constants.PC_SUCESS.ToUpper(), info.Detail.CrossReferenceNumber,InsData);
                        //MAIG - CH10 - Added a List to send the Policy details to DB insertion which was appeneded in AccountNumber field earlier
                        info.Detail.AuthCode = response.issueDirectPaymentResponse.paymentItems.paymentItem[0].card.paymentAuthorization.returnCode;
                    }
                }
            }
            else
            {
                //Added the below check since crossSequenceNumber will be NULL for UI and PaymentX transactions 
                if (string.IsNullOrEmpty(info.Detail.CrossReferenceNumber))
                {
                    info.Detail.CrossReferenceNumber = string.Empty;
                }
                string errorMsg = string.Empty;
                if (info.Errors[0] != null)
                {
                    errorMsg = info.Errors[0].Code + "-" + info.Errors[0].Message;
                }
                if (info.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.Cash || info.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.Check)
                {
                    //MAIG - CH10 - Added a List to send the Policy details to DB insertion which was appeneded in AccountNumber field earlier
                    bool IsAutoApproved = false;
                    if (info.Lines.Count > 0)
                    {
                        //MAIGEnhancement CHG0107527 - CH2 - BEGIN - Modified the below code to auto approve for Users having Ps User role and non PS User having a KIC Policy if the payment response is failure
                        if (info.Lines[0].AccountNumber.Trim().Length == 8 && (info.Lines[0].ProductTypeNew.Equals("PA") || info.Lines[0].ProductTypeNew.Equals("HO")))
                        {
                            IsAutoApproved = true;
                        }

                        if (info.User.RoleNames.ToLower().Contains("pss"))
                        {
                            //If PS User User has processed a Cash/Check policy then mark the Poicy status as Approved directly
                            IsAutoApproved = true;
                        }
                        //MAIGEnhancement CHG0107527 - CH2 - END - Modified the below code to auto approve for Users having Ps User role and non PS User having a KIC Policy if the payment response is failure
                    }
                    pcDataconnection.pcCashCheck(info.User, sessionInfo.AppName, info.Detail.PaymentType, "", info.Detail.MerchantRefNum, new ArrayOfLineItem(info.Lines), errorMsg, info.Detail.CrossReferenceNumber, InsData, IsAutoApproved);
                }
                else if (info.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.CreditCard)
                {
                    //MAIG - CH10 - Added a List to send the Policy details to DB insertion which was appeneded in AccountNumber field earlier
                    pcDataconnection.pcCreditCard(info.User, sessionInfo.AppName, info.Detail.PaymentType, "", info.Detail.MerchantRefNum, "", new CardInfo(info.Card), null, new BillToInfo(info.Addresses.BillTo), new ArrayOfLineItem(info.Lines), errorMsg, info.Detail.CrossReferenceNumber,InsData);
                }
                else if (info.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.ECheck)
                {
                    //MAIG - CH10 - Added a List to send the Policy details to DB insertion which was appeneded in AccountNumber field earlier
                    pcDataconnection.pcCreditCard(info.User, sessionInfo.AppName, info.Detail.PaymentType, "", info.Detail.MerchantRefNum, "", null, new eCheckInfo(info.echeck), new BillToInfo(info.Addresses.BillTo), new ArrayOfLineItem(info.Lines), errorMsg, info.Detail.CrossReferenceNumber,InsData);
                    //MAIG - CH10 - END - Added a List to send the Policy details to DB insertion which was appeneded in AccountNumber field earlier
                }
            }
            return info;
        }
        //PC Phase II changes - CH2 - Start - Added the method IDPReversal method to call PC IDP Reversal service
        public List<string> IDPReversal(string receiptNo, string voidtype, string ModifiedBy)
        {
            List<string> resp_List = new List<string>();
            bool Errflag = false;
            IssueDirectPaymentReversalRequest1 idpReversalRequest = new IssueDirectPaymentReversalRequest1();
            IssueDirectPaymentReversalResponse1 idpReversalResponse = new IssueDirectPaymentReversalResponse1();
            try
            {
                IssueDirectPaymentReversal paymentVoid = (IssueDirectPaymentReversalChannel)CreateInstance<IssueDirectPaymentReversalChannel>("IssueDirectPaymentReversalSOAPPort");
                issueDirectPaymentReversalRequest reversalRequest = new issueDirectPaymentReversalRequest();
                reversalRequest.applicationContext = new IDPReversal.ApplicationContext();
                reversalRequest.applicationContext.application = Config.Setting(CSAAWeb.Constants.PC_ApplicationContext_Payment_Tool);
                Turnin turnin = new Turnin();
                reversalRequest.externalReferenceNumber = pcDataconnection.GetMerchantReference(CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL);
                reversalRequest.reasonCode = CSAAWeb.Constants.PC_IDP_Reversal_ReasonCode;
                if (CSAAWeb.Validate.IsAllNumeric(receiptNo))
                {
                    reversalRequest.paymentReceiptNumber = receiptNo;
                }
                else
                {
                    reversalRequest.paymentExternalRefNumber = receiptNo;
                }
                idpReversalRequest.issueDirectPaymentReversalRequest = reversalRequest;
                if (Config.Setting("Logging.IssueDirectPaymentReversal") == "1")
                {
                    if (idpReversalRequest != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerReq = new System.Xml.Serialization.XmlSerializer(idpReversalRequest.GetType());
                        StringWriter writerReq = new StringWriter();
                        serializerReq.Serialize(writerReq, idpReversalRequest);
                        Logger.Log("Payment request to Payment Central - IDP Reversal Request : \r\n" + writerReq);
                    }
                }
                idpReversalResponse = paymentVoid.IssueDirectPaymentReversal(idpReversalRequest);
                if (Config.Setting("Logging.IssueDirectPaymentReversal") == "1")
                {
                    if (idpReversalResponse != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(idpReversalResponse.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, idpReversalResponse);
                        Logger.Log("Payment request to Payment Central - IDP Reversal Response : \r\n" + writerRes);
                    }
                }
                if (idpReversalResponse != null)
                {
                    if (idpReversalResponse.issueDirectPaymentReversalResponse.status.Equals("SUCC"))
                    {
                        resp_List.Add(idpReversalResponse.issueDirectPaymentReversalResponse.statusDescription);
                        resp_List.Add(idpReversalResponse.issueDirectPaymentReversalResponse.receiptNumber);
                        resp_List.Add(idpReversalResponse.issueDirectPaymentReversalResponse.paymentReversalDateTime.ToString());
                    }
                }
            }
            catch (FaultException<IDPReversal.ErrorInfo> faultExpErrorInfo)
            {
                if (Config.Setting("Logging.IssueDirectPaymentReversal") == "1")
                {
                    if (idpReversalResponse != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(idpReversalResponse.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, idpReversalResponse);
                        Logger.Log("Payment request to Payment Central - IDP Reversal Response : \r\n" + writerRes);
                    }
                }
                if (faultExpErrorInfo.Detail != null)
                {
                    Logger.Log(faultExpErrorInfo.Detail.errorMessageText);
                    resp_List.Add(faultExpErrorInfo.Detail.errorCode);
                    resp_List.Add(faultExpErrorInfo.Detail.errorMessageText);
                }
                Errflag = true;
            }
            catch (FaultException faultExp)
            {
                resp_List = HandleFaultException(faultExp, idpReversalResponse, resp_List);
                Errflag = true;
            }
            catch (SoapException soapExp)
            {
                resp_List = HandleSoapException(soapExp, resp_List, idpReversalResponse);
                Errflag = true;
            }
            catch (Exception exp)
            {
                resp_List = HandleException(exp, resp_List, idpReversalResponse);
                Errflag = true;
            }
            try
            {
                if (!Errflag && resp_List[0].Equals("SUCC"))
                {
                    pcDataconnection.PC_Update_Void(voidtype, receiptNo, resp_List[1], DateTime.Parse(resp_List[2]), ModifiedBy);
                }
            }
            catch (Exception e)
            {
                Logger.Log(e.Message);
                resp_List.Add("");
                resp_List.Add(CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION);
            }
            return resp_List;
        }
        //PC Phase II changes - CH2 - End - Added the method IDPReversal method to call PC IDP Reversal service

        private List<string> HandleFaultException(FaultException faultExp, IssueDirectPaymentReversalResponse1 idpReversalResponse, List<string> resp_List)
        {
            string errFriendlyMsg = "", errMsg = "", errCode = "", appendErrMsg = "";
            if (Config.Setting("Logging.IssueDirectPaymentReversal") == "1")
            {
                if (idpReversalResponse != null)
                {
                    System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(idpReversalResponse.GetType());
                    StringWriter writerRes = new StringWriter();
                    serializerRes.Serialize(writerRes, idpReversalResponse);
                    Logger.Log("Payment request to Payment Central - IDP Reversal Response : \r\n" + writerRes);
                }
            }
            FaultException<IDPReversal.www.aaancnuit.com.aaancnu_common_version2.ErrorInfo> errInfo = (FaultException<IDPReversal.www.aaancnuit.com.aaancnu_common_version2.ErrorInfo>)faultExp;
            if (errInfo.Detail != null)
            {
                foreach (XmlNode node in errInfo.Detail.Nodes)
                {
                    if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_FRIENDLY_ERROR_MSG))
                        errFriendlyMsg = node.InnerText;
                    if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_ERROR_MSG_TEXT))
                        errMsg = node.InnerText;
                    if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_ERROR_CODE))
                        errCode = node.InnerText;
                }
            }
            if (errCode == CSAAWeb.Constants.ERR_CODE_BUSINESS_EXCEPTION)
            {
                appendErrMsg = CSAAWeb.Constants.PC_ERR_BUSINESS_EXCEPTION + CSAAWeb.Constants.ERR_CODE_BUSINESS_EXCEPTION + "-" + errMsg;
            }
            else
            {
                appendErrMsg = errCode + "-" + CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
            }
            Logger.Log(errCode + " " + appendErrMsg);
            resp_List.Add(string.Empty);
            resp_List.Add(appendErrMsg);
            return resp_List;
        }

        private List<string> HandleSoapException(SoapException soapExp, List<string> resp_List, IssueDirectPaymentReversalResponse1 idpReversalResponse)
        {
            if (Config.Setting("Logging.IssueDirectPaymentReversal") == "1")
            {
                if (idpReversalResponse != null)
                {
                    System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(idpReversalResponse.GetType());
                    StringWriter writerRes = new StringWriter();
                    serializerRes.Serialize(writerRes, idpReversalResponse);
                    Logger.Log("Payment request to Payment Central - IDP Reversal Response : \r\n" + writerRes);
                }
            }
            if (soapExp.Message != null)
            {
                Logger.Log(soapExp.Message);
                resp_List.Add(soapExp.Code.ToString());
                resp_List.Add(CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION);
            }
            return resp_List;
        }

        private List<string> HandleException(Exception exp, List<string> resp_List, IssueDirectPaymentReversalResponse1 idpReversalResponse)
        {
            if (Config.Setting("Logging.IssueDirectPaymentReversal") == "1")
            {
                if (idpReversalResponse != null)
                {
                    System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(idpReversalResponse.GetType());
                    StringWriter writerRes = new StringWriter();
                    serializerRes.Serialize(writerRes, idpReversalResponse);
                    Logger.Log("Payment response from Payment Central - IDP Reversal Response : \r\n" + writerRes);
                }
            }
            if (exp.Message != null)
            {
                Logger.Log(exp.Message);
                resp_List.Add("");
                resp_List.Add(CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION);
            }
            return resp_List;
        }

    }
}
