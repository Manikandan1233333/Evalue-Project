
/*	REVISION HISTORY:
 *	MODIFIED BY COGNIZANT
 *	07/19/2005 - For changing the web.config entries pointing to database. This changes 
 *				 are made as part of CSR #3937 implementation.
 *	Changes Done:
 *	CSR#3937.Ch1 : Include more namespaces as part of web.config changes - for StringDictionary and Dataset
 *	CSR#3937.Ch2 : Calling a method which populates the constants into application object
 *	CSR#3937.Ch3 : Changed the code for accessing the Application object for WU and ACA
 *  CSR#3937.Ch4 : Added a new method used for populating constants from db into application object
 * 
 *  MODIFIED BY COGNIZANT AS PART OF Q4 RETROFIT CHANGES
 *  12/16/2005 Q4-Retrofit.Ch1 : Modified the code for identifying 9 digit HUON Auto policy
 *  12/19/2005 Q4-Retrofit.Ch2 : Modified the code for accessing the Application object for ProductType.Auto
 * 
 *  MODIFIED BY COGNIZANT AS PART OF Q1 RETROFIT CHANGES
 *  04/26/2006 - Q1-Retrofit
 *  Q1-Retrofit.Ch1: Changed the BuildRenewalFromMember function to add a year with Expiration 
 *					 date while renewing a Membership through MIVR(changes done as a part of CSR #4833).
 *  Q1-Retrofit.Ch2: Added the code to mark the revenue type as Renewal Payment for Membership 
 *					 Renewals made through MIVR(changes done as a part of CSR #4833).
 * 
 * STAR Retrofit II Changes: 
 * Modified as a part of CSR 5157 
 * 2/6/2007 STAR Retrofit II.Ch1:  Modified the ProcessOrder function to send the 22 digit RequestID for CPM transactions. 
 * Modified by COGNIZANT AS PART OF Premier Membership Changes
 * 05/22/2008 Premier Membership.Ch1: Added a condition to check the Premier flag
 * 05/22/2008 Premier Membership.Ch2: Added a condition to check the Premier flag
 * 05/22/2008 Premier Membership.Ch3: Added a condition to check the Premier flag
 * 
 * MODIFIED BY COGNIZANT AS PART OF HO-6 PROJECT
 * HO-6.ch1:Modified on 4/13/2010 - Modified the method to get the AppId in SalesXReport() method.
 * HO-6.ch2:Modified on 5/11/2010 - Modified on Assigned the echeck object as null for payment inputs all interfaces other than netPOSitive.
 * This is needed so that an empty <echeck> XML tag is not sent from Payment Tool in the response to 
 * the other interfaces (apart from netPOSitive)
 * HO-6.ch3:Modiifed on 5/18/2010 - Added code to check if the User Id is present in the input payment request from netPOSitive
 * If it is missing, an error message will be sent in the response to netPOSitive.
 * 06/01/2010 MP2.0.Ch1 - New boolean validation set for exigen - by Cognizant
 * Modified by cognizant  as a part of MP-Story131 on 06-17-2010.
 * MP-Story131.Ch1:Commented a code in GetMemberOrder () method which is calculating the age and if age is greater than 21 years excluding from the associate list in renewal.
 * MP-Story131.Ch2:Commented a code in BuildRenewalFromMember() method which is calculating the age and if is greater than 21 years then excluding from the order object for renewals.
 * SSO-Integration.Ch1: Added New SoapHeader attribute to accept the Header information on 09/22/2010 by Cognizant.
 * SSO-Integration.Ch2: Added New Code to handle the Single Sign on process on 09/22/2010 by Cognizant.
 * Fix for Security defect 3489.CH1:Added a try catch block to handle the exception duing the insert of null values in AUTH_LogActivity stored procedure on 12/21/2010 by cognizant. 
 * RFC#135294 CH1 04/29/2011: Modified the code logic in process order method for assigning the e-Check object to null for the other application product which is not configured in the web config file by cognizant -As a part of e-check implementation for SalesX application.
 * CSAA.COM CH1: Bypassed token validation for CSAA.COM
 * 67811A0  - PCI Remediation for Payment systems CH1: Added the new web method to perform void functionality for cash sweep payments processed from AI by cognizant on 08/25/2011.
 * 67811A0  - PCI Remediation for Payment systems.CH2: Token Validation for AI with Generic userid.
 * 67811A0  - PCI Remediation for Payment systems.CH3: The Agent Code as the User Id in the User Info field for AI .
 * 67811A0  - PCI Remediation for Payment systems.CH4: Append Policystate and policyprefix to policy when product type is PCP.
 * 67811A0  - PCI Remediation for Payment systems.CH5: For all PCP Products policy number will be appended with Policystate and policyprefix so in the response it should removed from policy number. 
 *           Added condition to check that remove from policy number only if the application id is not APDS so PCP payments from UI will not be affected.
 * 67811A0  - PCI Remediation for Payment systems.CH6: Added the condition to check the mandatory fields for Memebership transactions  procesing from applications which are not configured in web config by cognizant on 10/10/2011
 * //CSAA.com - added the condition toupper to prevent the APP id  validation failure for the  by cognizant on 12/23/2011
 * 67811A0  - PCI Remediation for Payment systems.CH7:start added the code to remover the IIS server version during the response from web service call as a part of VA scan defect by cognizant on 1/5/2011 
 * 67811A0  - PCI Remediation for Payment systems.CH8:start added the code to remover the IIS server version during the response from web service call as a part of VA scan defect by cognizant on 1/5/2011 
 *67811A0  - PCI Remediation for Payment systems - CH9 -END  Made code changes to handle the user id validation for Netpos ENT user and all numeric for the independent agents by cognizant on 01/09/2011.
 *67811A0  - PCI Remediation for Payment systems - CH10 -END  Made code changes to set policy validate to true for the IIB polices by cognizant on 01/09/2011
 *67811A0 Ch3 - END - PCI Remediation for Payment systems :CH11 added the code to check the amount to be greater than for the Payment transaction by cognizant on 01/12/2011
 *67811A0 - START - PCI Remediation for Payment systems start CH12 : Added the code to verify the merchnat reference number to be numeric or alpha numeric and its length validation as a part of security defect fix by cognizant on 1/10/2012
 //RFC 185138 - AD Integration - CH1 : Modified the code to validate the User ID and password against the Active Directory on SSO valid open token event
 *RFC 185138 - AD Integration - CH2 :Added the code to validate the User ID and password against the Active Directory for SSO Applications first time authentication
 *RFC 185138 - AD Integration - CH3 -Commented the  locked out check event
 *RFC 185138 - AD Integration - CH4 -Commented the  locked out check event
 *RFC 185138 - AD Integration - CH5 -Added the error info message in case of invalid user in payment tool
 *RFC 185138 - AD Integration - CH6 - Added below code to allow specific salesX user to avoid AD and SSO authentication steps
 *PAS AZ Product Configuration Ch1:Modified the below conditions to set Isexigen flag to true for PAS AZ product by cognizant on 03/11/2012
 Company Code Fix - Seperate the company ID from the policy state and assign it at the end of Account number only if it is a UI PCP transaction
Security Defect - CH1 - Added the below code to validate the userid,appid,token
Security Defect - CH2 - Added the below code to validate the userid,appid,token,fullmemberid
Security Defect - CH3 - Added the below code to assign the policy number alone in the insurance line item filed in case of validation failure
 //Security Defect - CH4 -Removed the alphanumeric condition verification in the userID
//Security Defect -CH5 - Added the below line to set the echeck object as null for credit card payment type.
//Security Defect -CH6 -  Added the below line to validate the Order object when it is echeck payment type.
 Security Defect -CH7 -  Commented the below code to validate the address and billto for the Cash/Check Payment Mode
 Security Defect -CH8 Commented the below line since the echeck object is made null in other line for the other payment types.
 * Security defect CH9- Added the below code to trim all the fields present in the Order
 * //Security Defect - CH10 -Added the below code to skip the addressinfo validation since the zip code is null for APDS cash and check trasnactions.
 * //AZ PAS conversion and PC integration- CH1-  Added the below code to append the company code and check number
//AZ PAS conversion and PC integration- CH2 - Added the below code to assign the account number back to the order and insinfo since the value is  splitted and stored in validation of policy
 * PC Phase II changes CH1 - Added the below code to skip token validation for PaymentX
 * //PC Phase II changes CH2 - Start - Modified Void Payment Web Method to check the Flow whether Payment Central or Payment Tool Transaction
 * CHG0088851 - Penetration defects - Removed the condition for PaymentX to include it in the validation process.
 * CHG0088851 - Penetration defects - Create method is removed as it is not used.
 * CHG0088851 - Added customized SOAP error message for invalid token.
 * CHG0104053 - PAS HO CH1 - Added the code to accept the AAA SS Home product type as an Exigen Policy  6/12/2014
 * MAIG - CH1 - CHG0087639 - RepID and UserID Length Expansion - Changed Convert function from Int32 to Int64
 * MAIG - CH2 - CHG0087639 - RepID and UserID Length Expansion - Changed Convert function from Int32 to Int64
 * MAIG - CH3 - Removed the logic to append the Policy State and Policy Prefix with Account Number for PCP Products
 * MAIG - CH4 - Removed the logic to append the Policy State and Policy Prefix with Account Number for PCP Products
 * MAIG - CH5 - Removed the logic to append the Policy State and Policy Prefix with Account Number for PCP Products
 * MAIG - CH6 - Removed the logic to get the Policy Number alone from Appeneded Account Number field
 * CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods MemberSearch,MemberLookUp,BuildRenewalFromMember with respect to MembershipSerializers.cs - March 2016
 * CHGXXXXXXJ - CH2 - Changes made for splitting the token service - 27/06/2017
 */

using System;
using System.ComponentModel;
using System.Data;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.Xml.Serialization;
using System.Reflection;
using CSAAWeb;
using CSAAWeb.Serializers;
using OrderClasses;
using AuthenticationClasses;
using AuthenticationClasses.Service;
using SalesXReportClasses;
using MSCRCore;
using CSAAActors;
//CSR#3937.Ch1 : START - Include more namespaces as part of web.config changes - for StringDictionary and Dataset
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Specialized;
using InsuranceClasses.Service;
using opentoken;
using System.Collections.Generic;
using System.Configuration;
using CSAAWeb.AppLogger;
using System.Xml;
using System.IO;
//END CSR#3937.Ch1 : END - Include more namespaces as part of web.config changes - for StringDictionary and Dataset

namespace OrderClasses.WebService
{
    /// <summary>
    /// Class for a webservice that allows for processing orders
    /// </summary>
    [WebService(Namespace = "http://csaa.com/webservices/")]
    public class Order : System.Web.Services.WebService, CSAAWeb.Web.IClosableWeb
    {
        ///<summary/>
        private static int SessionLength = 30;
        private Authentication Auth = null;
        private string UserId = "";
        private string Token = "";
        private string AppId = "";
        private bool Verified = false;
        public OpenToken openToken;
        ///<summary/>
        static Order()
        {
            //67811A0  - PCI Remediation for Payment systems.CH7:start added the below line of code to remover the IIS server version during the response from web service call as a part of VA scan defect by cognizant on 1/5/2011 
            HttpContext.Current.Response.Headers.Remove("Server");
            //67811A0  - PCI Remediation for Payment systems.CH7:end added the above line of code to remover the IIS server version during the response from web service call as a part of VA scan defect by cognizant on 1/5/2011
            string st = Config.Setting("Order_SessionLength");
            if (st != "") SessionLength = Convert.ToInt32(st);
        }

        private Authentication CheckAuth(string UserId, string AppId)
        {
            if (Auth == null) Auth = new Authentication(new SessionInfo(UserId, AppId));
            return Auth;
        }
        private Authentication CheckAuth(string UserId, string AppId, string Token)
        {
            if (Auth == null) Auth = new Authentication(new SessionInfo(UserId, AppId, Token));
            return Auth;
        }
        private Authentication CheckAuth()
        {
            return CheckAuth(UserId, AppId, Token);
        }

        private void CloseAuth()
        {
            if (Auth != null) Auth.Close();
            Auth = null;
        }
        ///<summary/>
        public void Close()
        {
            CloseAuth();
            UserId = "";
            Token = "";
            AppId = "";
            Verified = false;
            CSAAWeb.Web.ClosableModule.RemoveHandler(this);
        }

        /// <summary>
        /// Closes the connection and rollsback any open transaction.
        /// </summary>
        public void Close(object Source, EventArgs e)
        {
            Close();
        }

        ///<summary/>
        public Order()
        {
            //67811A0  - PCI Remediation for Payment systems.CH8:start added the below line of code to remover the IIS server version during the response from web service call as a part of VA scan defect by cognizant on 1/5/2011 
            HttpContext.Current.Response.Headers.Remove("Server");
            //67811A0  - PCI Remediation for Payment systems.CH8:end added the above line of code to remover the IIS server version during the response from web service call as a part of VA scan defect by cognizant on 1/5/2011

            CSAAWeb.Web.ClosableModule.SetHandler(this);
            //CSR#3937.Ch2 : START - Calling a method which populates the constants into application object
            if (Application["Constants"] == null) _GetConstants();
            //END CSR#3937.Ch2 : END - Calling a method which populates the constants into application object
        }
        #region Component Designer generated code

        //Required by the Web Services Designer 
        private IContainer components = null;

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        protected override void Dispose(bool disposing)
        {
            if (disposing && components != null)
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #endregion
        #region GetReference
        //Modified by J McEwen 10/25/2004, changed Payments constructor for STAR, changed GetReferenceNumber call.
        ///<summary>Returns a new Reference Number</summary>
        /// <param name="AppId">Name of the calling application.</param>
        /// <param name="Token">Security token provided by authenticate</param>
        /// <param name="UserId">UserId of the logged-in user.</param>
        /// <param name="Errors"></param>
        [WebMethod(Description = "Returns a new Reference Number.  This would be the first call.")]
        public string GetReference(string Token, string UserId, string AppId, out ArrayOfErrorInfo Errors)
        {
            Errors = ValidateToken(Token, UserId, AppId);
            if (Errors != null) return "";
            PaymentClasses.Service.Payments P = new PaymentClasses.Service.Payments(new SessionInfo(UserId, AppId, Token), null);
            string Result = P.GetReferenceNumber();
            P.Close();
            return Result;
        }
        #endregion
        // CHG0088851 - Penetration defect - This method is commented as not used.
        //#region Create
        ///// <summary>
        ///// 
        ///// </summary>
        //[WebMethod(Description = "Creates a new order object with the selected products.")]
        //public OrderInfo Create(string ProductTypes)
        //{
        //    OrderInfo Result = new OrderInfo();
        //    string[] P = ProductTypes.Split(',');
        //    foreach (string S in P)
        //    {
        //        string ClassName = "SvcClasses.SvcInfo, OrderClassesII".Replace("Svc", S);
        //        Type T = Type.GetType(ClassName, false, true);
        //        if (T == null)
        //            throw new Exception("Product type " + S + " not supported.");
        //        ProductInfo I = (ProductInfo)T.GetConstructor(new Type[] { }).Invoke(new object[] { });
        //        Result.Products.Add(I);
        //    }
        //    return Result;
        //}

        //#endregion

        #region CaculateOrderLines
        /// <summary>
        /// Calculates order lines for the product type specified.
        /// </summary>
        /// <param name="AppId">Name of the calling application.</param>
        /// <param name="Token">Security token provided by authenticate</param>
        /// <param name="UserId">UserId of the logged-in user.</param>
        /// <param name="Order">The order process</param>
        /// <param name="ProductName">Product type to update lines for, or blank to process all.</param>
        [WebMethod(Description = "Calculates order lines for the product type specified, or all if blank.  This would be the third call.")]
        public OrderInfo CalculateOrderLines(string Token, string UserId, string AppId, OrderInfo Order, string ProductName)
        {
            if (ProductName == null) ProductName = "";
            Order.Errors = ValidateToken(Token, UserId, AppId);
            CloseAuth(); // Added to speed up relinquishing this connection.
            if (Order.Errors == null) Order.CalculateOrderLines(ProductName);
            return Order;
        }
        #endregion

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods GetMemberOrder with respect to Member.cs - March 2016

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods GetEligibilityErrors with respect to Member.cs - March 2016

        #region Process Order
        /// <summary>
        /// This method actually does the work of entering an order.
        /// </summary>
        /// <param name="AppId">Name of the calling application.</param>
        /// <param name="Token">Security token provided by authenticate</param>
        /// <param name="UserId">UserId of the logged-in user.</param>
        /// <param name="Order">The order to process</param>
        /// <returns>The updated order</returns>
        [WebMethod(Description = "Processes the order.  This would be the fourth and final call.")]
        public OrderInfo ProcessOrder(string Token, string UserId, string AppId, OrderInfo Order)
        {
            //Security defect - Added the below code to trim all the fields present in the Order
            string strOrder = Order.ToString();
            string accountNumber = string.Empty;
            XmlDataDocument xmldocOrder = new XmlDataDocument();
            XmlNodeList lineItem;
            xmldocOrder.LoadXml(strOrder);
            lineItem = xmldocOrder.GetElementsByTagName("LineItem");
            if (lineItem != null)
            {
                for (int k = 0; k <= lineItem.Count - 1; k++)
                {
                    foreach (XmlAttribute lineAttributes in lineItem[k].Attributes)
                    {
                        lineAttributes.Value = lineAttributes.Value.Trim();
                    }
                }
            }
            XmlNodeList addressInfo;
            addressInfo = xmldocOrder.GetElementsByTagName("AddressInfo");
            if (addressInfo != null)
            {
                for (int k = 0; k <= addressInfo.Count - 1; k++)
                {
                    foreach (XmlAttribute addressAttributes in addressInfo[k].Attributes)
                    {
                        addressAttributes.Value = addressAttributes.Value.Trim();
                    }
                }
            }
            XmlNodeList card;
            card = xmldocOrder.GetElementsByTagName("Card");
            if (card != null)
            {
                for (int k = 0; k <= card.Count - 1; k++)
                {
                    foreach (XmlAttribute cardAttributes in card[k].Attributes)
                    {
                        cardAttributes.Value = cardAttributes.Value.Trim();
                    }
                }
            }
            XmlNodeList detail;
            detail = xmldocOrder.GetElementsByTagName("Detail");
            if (detail != null)
            {
                for (int k = 0; k <= detail.Count - 1; k++)
                {
                    foreach (XmlAttribute detailAttribute in detail[k].Attributes)
                    {
                        detailAttribute.Value = detailAttribute.Value.Trim();
                    }
                }
            }
            XmlNodeList echeck;
            echeck = xmldocOrder.GetElementsByTagName("Echeck");
            if (echeck != null)
            {
                for (int k = 0; k <= echeck.Count - 1; k++)
                {
                    foreach (XmlAttribute echeckAttribute in echeck[k].Attributes)
                    {
                        echeckAttribute.Value = echeckAttribute.Value.Trim();
                    }
                }
            }
            XmlNodeList insurancelineItem;
            insurancelineItem = xmldocOrder.GetElementsByTagName("InsuranceLineItem");
            if (insurancelineItem != null)
            {
                for (int k = 0; k <= insurancelineItem.Count - 1; k++)
                {
                    foreach (XmlAttribute insuranceAttribute in insurancelineItem[k].Attributes)
                    {
                        insuranceAttribute.Value = insuranceAttribute.Value.Trim();
                    }
                }
            }

            using (TextReader reader = new StringReader(xmldocOrder.InnerXml.ToString()))
            {
                try
                {
                    Order = (OrderInfo)new XmlSerializer(typeof(OrderInfo)).Deserialize(reader);
                }
                catch (Exception ex)
                {
                    Logger.Log(ex);
                }

            }
            //Security defect - Added the below code to trim all the fields present in the Order

            //CSAA.com - added the condition toupper to prevent the APP id  validation failure for the  by cognizant on 12/23/2011
            AppId = AppId.Trim();
            //Order.Errors = ValidateToken(Token, UserId, AppId);
            //Added by cognizant as a part of HO6 on 04-05-2010.
            //RFC#135294 CH1:BEGIN Modified the code logic in process order method for assigning the e-Check object to null for the other application product which is not configured in the web config file by cognizant -As a part of e-check implementation for SalesX application on  04/29/2011
            //Security Defect - CH1 -START - Added the below code to validate the userid,appid,token
            if (AppId.Trim() == "" || AppId.Trim().Length > 50 || !CSAAWeb.Validate.IsAllChars(AppId.Trim()))
            {
                ArrayOfErrorInfo arErrMerchantLength = new ArrayOfErrorInfo();
                arErrMerchantLength.Add(new ErrorInfo(Config.Setting("ERRCDE_APPID"), CSAAWeb.Constants.ERR_AUTHVALIDATION + "AppId", "AppId"));
                Order.Errors = arErrMerchantLength;
                return Order;
            }
            //Security Defect - Removed the alphanumeric condition verification in the userID
            if (UserId == "" || UserId.Length > 50)
            {

                ArrayOfErrorInfo arErrMerchantLength = new ArrayOfErrorInfo();
                arErrMerchantLength.Add(new ErrorInfo(Config.Setting("ERRCDE_USERID"), CSAAWeb.Constants.ERR_AUTHVALIDATION + "UserId", "UserId"));
                Order.Errors = arErrMerchantLength;
                return Order;
            }
            if (Token.Length > 50)
            {

                ArrayOfErrorInfo arErrMerchantLength = new ArrayOfErrorInfo();
                arErrMerchantLength.Add(new ErrorInfo(Config.Setting("ERRCDE_TOKEN"), CSAAWeb.Constants.ERR_AUTHVALIDATION + "Token", "Token"));
                Order.Errors = arErrMerchantLength;
                return Order;
            }
            //Security Defect - CH1 -END - Added the below code to validate the userid,appid,token
            //67811A0 - START -PCI Remediation for Payment systems :Arcsight logging - Added statement to throw error message while using Offline credit card from interfacing applications by cognizant on 11/29/2011
            if (Order.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.OfflineCreditCard)
            {

                ArrayOfErrorInfo arErrOffCC = new ArrayOfErrorInfo();
                arErrOffCC.Add(new ErrorInfo("INVALID_PYMT_METHOD", CSAAWeb.Constants.PCI_MESSAGE_OFFLINECC, "Detail.PaymentType"));
                Order.Errors = arErrOffCC;
                Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DESTINATION_PROCESSNAME_PAYMENT;
                Logger.SourceUserName = UserId;
                Logger.SourceProcessName = AppId;
                Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_DEVICEACTION_OFFLINECREDITCARD;
                Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_OFFLINECREDITCARD_FAILURE;
                Logger.ArcsightLog();
                return Order;
            }
            //67811A0 - END - PCI Remediation for Payment systems :Arcsight logging - Added statement to throw error message while using Offline credit card from from interfacing applications by cognizant on 11/29/2011
            //67811A0 - START - PCI Remediation for Payment systems :Arcsight logging - Object Creation
            if ((Order.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.CreditCard) || (Order.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.ECheck))
            {
                //67811A0 - START - PCI Remediation for Payment systems start CH12 : Added the code to verify the merchnat reference number to be numeric or alpha numeric and its length validation as a part of security defect fix by cognizant on 1/10/2012
                InsuranceClasses.Service.Insurance I = new InsuranceClasses.Service.Insurance();
                if ((!I.ValidateMerchantNum(Order.Detail.MerchantRefNum)) && (AppId.ToUpper() != "IVR"))
                {

                    ArrayOfErrorInfo arErrMerchantLength = new ArrayOfErrorInfo();
                    arErrMerchantLength.Add(new ErrorInfo("INVALID_MERCHANT_REF_NUMBER", CSAAWeb.Constants.PCI_MESSAGE_MERCHANT_REF_NUM, "Detail.MerchantRefNumber"));
                    Order.Errors = arErrMerchantLength;
                    return Order;
                }

                //67811A0 - START - PCI Remediation for Payment systems End CH12: Added the code to verify the merchnat reference number to be numeric or alpha numeric and its length validation as a part of security defect fix by cognizant on 1/10/2012
                Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DESTINATION_PROCESSNAME_PAYMENT;
                Logger.SourceUserName = UserId;
                Logger.SourceProcessName = AppId;
                Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_LOW;
                Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS;
                Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_DEVICEACTION_CREDIT;
                Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_CC_CREATED;
                Logger.ArcsightLog();
            }

            //67811A0 Ch3 - END - PCI Remediation for Payment systems :Arcsight logging - - Object Creation

            if (Order.Products["Insurance"] != null)
            {
                InsuranceClasses.InsuranceInfo Ins_Info = (InsuranceClasses.InsuranceInfo)Order.Products["Insurance"];
                //67811A0 Ch3 - END - PCI Remediation for Payment systems :CH11 added the code to check the amount to be greater than for the Payment transaction by cognizant on 01/12/2011
                for (int i = 0; i < Ins_Info.Lines.Count; i++)
                {
                    if (Ins_Info.Lines[i].Amount <= 0)
                    {
                        ArrayOfErrorInfo arErrAmount = new ArrayOfErrorInfo();
                        arErrAmount.Add(new ErrorInfo("MISSING Amount", CSAAWeb.Constants.PCI_ERR_ZERO_PRICEFIELD, "Price"));
                        Order.Errors = arErrAmount;
                        return Order;
                    }
                }
            }

            //67811A0 Ch3 - END - PCI Remediation for Payment systems :CH11 added the code to check the amount to be greater than for the Payment transaction by cognizant on 01/12/2011
            if (Convert.ToString(Config.Setting("cpm.eCheckApplications")).IndexOf(AppId) > -1)
            {
                if (AppId == Config.Setting("AppName.NetPos"))
                {
                    // START - HO-6.ch3 - Added code to check if the User Id is present in the input payment request from netPOSitive
                    if (UserId == null)
                    {
                        ArrayOfErrorInfo arErrUser = new ArrayOfErrorInfo();
                        arErrUser.Add(new ErrorInfo("MISSING USER", "User ID is missing in the request.", "UserId"));
                        Order.Errors = arErrUser;
                        return Order;
                    }
                    // END - HO-6.ch3
                    string uid = Config.Setting("NetPos.GenericUserId").ToString();
                    Order.Errors = ValidateToken(Token, uid, AppId);
                }
                //67811A0  - PCI Remediation for Payment systems.CH2:START- Token Validation for AI with Generic userid.
                else if (AppId == Config.Setting("AppName.AI"))
                {
                    if (UserId == null || UserId.Trim() == "")
                    {
                        ArrayOfErrorInfo arErrUser = new ArrayOfErrorInfo();
                        arErrUser.Add(new ErrorInfo("MISSING USER", "User ID is missing in the request.", "UserId"));
                        Order.Errors = arErrUser;
                        return Order;
                    }

                    string uid = Config.Setting("AI.GenericUserId").ToString();
                    Order.Errors = ValidateToken(Token, uid, AppId);

                }
                else if (AppId.Trim() == Config.Setting("ByPassTokenVldn.OrderService.AppName"))
                {
                    Order.Errors = null;
                    Auth = new Authentication(new SessionInfo(UserId, AppId, Token));
                }
                else if (Order.Detail.PaymentType != (int)PaymentClasses.PaymentTypes.ECheck)
                {
                    //Security Defect -CH8- Commented the below line since the echeck object is made null in other line for the other payment types.
                    //Order.echeck = null;
                    Order.Errors = ValidateToken(Token, UserId, AppId);
                }
                else
                {
                    Order.Errors = ValidateToken(Token, UserId, AppId);
                }
            }
            //67811A0  - PCI Remediation for Payment systems.CH2:END- Token Validation for AI with Generic userid.
            //67811A0  - PCI Remediation for Payment systems - CH3:START- Added Statement to process IVR transactions by Cognizant on 11/30/2011
            else
            {
                Order.Errors = ValidateToken(Token, UserId, AppId);
            }
            //67811A0  - PCI Remediation for Payment systems - CH3:END - Added Statement to process IVR transactions by Cognizant on 11/30/2011

            //RFC#135294 CH1:END Modified the code logic in process order method for assigning the e-Check object to null for the other application product which is not configured in the web config file by cognizant -As a part of e-check implementation for SalesX application on  04/29/2011


            if (Order.User == null) Order.User = Auth.GetContactInfo(UserId);
            Order.User.SkipValidation = true;

            //67811A0  - PCI Remediation for Payment systems - CH9 -Start Made code changes to handle the user id validation for Netpos ENT user and all numeric for the independent agents by cognizant on 01/09/2011.
            if (AppId == Config.Setting("AppName.NetPos"))
            {
                if ((Order.Products["Insurance"] != null))
                {
                    InsuranceClasses.InsuranceInfo InsInfo = (InsuranceClasses.InsuranceInfo)Order.Products["Insurance"];
                    if (!CSAAWeb.Validate.IsAllNumeric(UserId))
                    {
                        if ((Order.User.UserId != ""))
                        {

                        }
                        else if (InsInfo.Lines[0].ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Home"]))
                        {
                            ArrayOfErrorInfo arInvalidUser = new ArrayOfErrorInfo();
                            arInvalidUser.Add(new ErrorInfo("MISSING USER", CSAAWeb.Constants.PCI_ERR_NETPOS_INVALID_USERID, "UserId"));
                            Order.Errors = arInvalidUser;
                            return Order;
                        }
                        else
                            Order.User.UserId = UserId;
                    }
                    else
                    {
                        Order.User.UserId = UserId;
                        //MAIG - CH1 - BEGIN - CHG0087639 - RepID and UserID Length Expansion - Changed Convert function from Int32 to Int64
                        Order.User.RepId = Convert.ToInt64(UserId);
                        //MAIG - CH1 - END - CHG0087639 - RepID and UserID Length Expansion - Changed Convert function from Int32 to Int64
                    }

                }
                else
                {
                    Order.User.UserId = UserId;
                }
            }
            //67811A0  - PCI Remediation for Payment systems - CH9 -END  Made code changes to handle the user id validation for Netpos ENT user and all numeric for the independent agents by cognizant on 01/09/2011.
            // 67811A0  - PCI Remediation for Payment systems.CH3: START The Agent Code as the User Id in the User Info field for AI 
            if (AppId == Config.Setting("AppName.AI"))
            {
                if (!CSAAWeb.Validate.IsAllNumeric(UserId))
                {
                    Order.User.UserId = UserId;
                }
                else
                {
                    Order.User.UserId = UserId;
                    //MAIG - CH2 - BEGIN - CHG0087639 - RepID and UserID Length Expansion - Changed Convert function from Int32 to Int64
                    Order.User.RepId = Convert.ToInt64(UserId);
                    //MAIG - CH2 - END - CHG0087639 - RepID and UserID Length Expansion - Changed Convert function from Int32 to Int64
                }
            }
            // 67811A0  - PCI Remediation for Payment systems.CH3: END The Agent Code as the User Id in the User Info field for AI 

            //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the commented code 67811A0 - PCI Remediation - March 2016

            //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the code 67811A0 - PCI Remediation - Membership Changes - March 2016
            if (Order.Errors == null)
            {
                Order.S = new SessionInfo(UserId, AppId, Token);
                if (Order.Detail.RequestID != "" && Order.Detail.AuthCode != "")
                {
                    CloseAuth(); // Added to speed up relinquishing this connection.
                    //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the BillVerbal - March 2016
                    //Order.BillVerbal();
                }
                else
                {

                    //Start Changed by Cognizant on 06/24/2004 for Blocking the Policy Number validation for WU Produts
                    // .Modified by Cognizant changed as on Tier1
                    //Q4-Retrofit.Ch1-START:Modified to change the If loop for STAR Legacy Interfaces Project
                    if (Order.Products["Insurance"] != null)
                    //Q4-Retrofit.Ch1-END
                    {

                        InsuranceClasses.InsuranceInfo InsInfo = (InsuranceClasses.InsuranceInfo)Order.Products["Insurance"];
                        if (Order.Detail.UploadToPOES == 0)
                        {
                            //Added by cognizant on 10/18/2004 for Fetching the UploadType for the Corresponding ProductType
                            InsuranceClasses.Service.Insurance InsSvc = new InsuranceClasses.Service.Insurance();
                            for (int i = 0; i < InsInfo.Lines.Count; i++)
                            {
                                //Commented by cognizant on 10/18/2004 to Identify the Western United product by Upload_Type. Fetch the Upload_Type for the 
                                //corresponding ProductType and compare with the Web.config key(UploadType.WesternUnited).
                                //if(InsInfo.Lines[i].ProductType ==Convert.ToString(Config.Setting("ProductCode.WesternUnited"))) 
                                //Modified by Cognizant on 05/04/2005 for Adding 12 digit policy validation for ACA Product

                                //CSR#3937.Ch3 : START - Changed the code for accessing the Application object for WU and ACA


                                if ((InsSvc.GetUploadType(InsInfo.Lines[i].ProductType) == Convert.ToString(((StringDictionary)Application["Constants"])["UploadType.WesternUnited"]))
                                    || (InsSvc.GetUploadType(InsInfo.Lines[i].ProductType) == Convert.ToString(((StringDictionary)Application["Constants"])["UploadType.ACA"])))
                                {
                                    //END CSR#3937.Ch3 : END - Changed the code for accessing the Application object for WU and ACA
                                    InsInfo.Lines[i].PolicyValidate = false;
                                }
                                //67811A0  - PCI Remediation for Payment systems - CH10 -start  Made code changes to set policy validate to true for the IIB polices by cognizant on 01/09/2011
                                else
                                {
                                    InsInfo.Lines[i].PolicyValidate = true;
                                }
                                //67811A0  - PCI Remediation for Payment systems - CH10 -END  Made code changes to set policy validate to true for the IIB polices by cognizant on 01/09/2011
                            }
                        }
                        //END
                        //Q4-Retrofit.Ch2 - START : Modified the code for accessing the Application object for ProductType.Auto
                        //Q4-Retrofit.Ch1-START:Added to identify the HUON products for STAR Legacy Interfaces project
                        for (int j = 0; j < InsInfo.Lines.Count; j++)
                        {
                            //if the product type is Auto and policy number is 9 digit then it is HUON Auto product
                            //if((InsInfo.Lines[j].ProductType == Config.Setting("ProductType.Auto")) && (InsInfo.Lines[j].Policy.Length==9))
                            string[] StrSplitPolicy;
                            StrSplitPolicy = InsInfo.Lines[j].Policy.Split('-');
                            string Policy = StrSplitPolicy[0].Trim();
                            if ((InsInfo.Lines[j].ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Auto"])) && (Policy.Length == 9))
                            {
                                InsInfo.Lines[j].IsHUONAuto = true;
                            }
                            // MP2.0.Ch1 - New checking has been aded to determine wither the LineItem is of type ExigenAuto on 1/6/2010 by Cognizant
                            // Checks if the line item's Product Type is = Auto and policy length is 13, set the property IsExigen as true.
                            //PAS AZ Product Configuration Ch1:Start Modified the below conditions to set Isexigen flag to true for PAS AZ product by cognizant on 03/11/2012
                            //CHG0104053 - PAS HO CH1 - START - Added the code to accept the AAA SS Home product type as an Exigen Policy  6/12/2014
                            else if (((InsInfo.Lines[j].ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.Auto"])) || (InsInfo.Lines[j].ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.ArizonaAuto"])) || (InsInfo.Lines[j].ProductType == Convert.ToString(((StringDictionary)Application["Constants"])["ProductType.SSHome"]))) && (Policy.Length == 13))
                            {
                                InsInfo.Lines[j].SetIsExigenAuto(true);
                            }
                            //CHG0104053 - PAS HO CH1 - END - Added the code to accept the AAA SS Home product type as an Exigen Policy  6/12/2014
                            //PAS AZ Product Configuration Ch1:End Modified the below conditions to set Isexigen flag to true for PAS AZ product by cognizant on 03/11/2012
                        }
                        //Q4-Retrofit.Ch1-END
                        //Q4-Retrofit.Ch2 - END : Modified the code for accessing the Application object for ProductType.Auto
                    }



                    //START Changed by Cognizant on 05/18/2004 for selective validation

                    if (Order.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.CreditCard)
                    {
                        //Security Defect -CH5 - Added the below line to set the echeck object as null for credit card payment type.
                        Order.echeck = null;
                        //CHGXXXXXXJ - CH2 - Changes made for splitting the token service - 27/06/2017-BEGIN.
                        if (System.Web.HttpContext.Current.Items["IsPaymentTokenGenerated"] == "No")
                        {
                        Order.Validate(); 
                        }
                        //CHGXXXXXXJ - CH2 - Changes made for splitting the token service - 27/06/2017-END.
                    }
                    //Security Defect -Ch6 - START - Added the below line to validate the Order object when it is echeck payment type.
                    else if (Order.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.ECheck)
                    {
                        Order.Card.SkipValidation = true;
                        Order.Validate();
                    }
                    //Security Defect -Ch6-END - Added the below line to validate the Order object when it is echeck payment type.
                    else
                    {

                        //Skip Address and Card Validation
                        //Security Defect -CH7 -START - Commented the below code to validate the address and billto for the Cash/Check Payment Mode
                        //Order.Addresses.SkipValidation = true;
                        //Order.Addresses.BillTo.SkipValidation = true;
                        //Security Defect -CH7 -End - Commented the below code to validate the address and billto for the Cash/Check Payment Mode
                        //Security Defect - CH10 -Added the below code to skip the addressinfo validation since the zip code is null for APDS cash and check trasnactions.
                        if (AppId == Config.Setting("AppName.APDS"))
                        {
                            Order.Addresses.BillTo.SkipValidation = true;
                        }
                        //Security Defect - CH10 -Added the below code to skip the addressinfo validation since the zip code is null for APDS cash and check trasnactions.

                        Order.Card.SkipValidation = true;
                        Order.echeck.SkipValidation = true;
                        Order.echeck = null;
                        Order.Validate();
                    }

                    //End
                    if (Order.Errors == null)
                    {
                        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the Lookup method with respect to MembershipReferences.cs - March 2016
                        CloseAuth(); // Added to speed up relinquishing this connection.
                        //AZ PAS conversion and PC integration- CH2 START- Added the below code to assign the account number back to the order and insinfo since the value is  splitted and stored in validation of policy
                        if (Order.Products["Insurance"] != null && (ConfigurationSettings.AppSettings["AppName.CompanyID"]).IndexOf(AppId) > -1)
                        {
                            InsuranceClasses.InsuranceInfo InsInfoPCP = (InsuranceClasses.InsuranceInfo)Order.Products["Insurance"];

                            //MAIG - BEGIN - CH4 - Removed the logic to append the Policy State and Policy Prefix with Account Number for PCP Products
                            /*for (int i = 0; i < InsInfoPCP.Lines.Count; i++)
                            {
                                if ((ConfigurationSettings.AppSettings["PCP.Products"]).IndexOf(Convert.ToString(InsInfoPCP.Lines[i].ProductTypeNew)) <= -1)
                                {
                                    Order.Lines[i].AccountNumber = accountNumber;
                                    InsInfoPCP.Lines[i].AccountNumber = accountNumber;
                                }
                            }*/
                            //MAIG - END - CH4 - Removed the logic to append the Policy State and Policy Prefix with Account Number for PCP Products
                        }
                        //AZ PAS conversion and PC integration- CH2 END- Added the below code to assign the account number back to the order and insinfo since the value is  splitted and stored in validation of policy
                        Order.Process();
                    }
                }
            }
            //CHG0088851 - Added customized SOAP error message for invalid token - Start.
            else
            {
                if (Order.Errors.ToString().Contains("INVALID TOKEN") || Order.Errors.ToString().Contains("TIMEOUT"))
                {
                    SoapLogger.SoapErrorMessage = "Your session has timed out and is now expired.  Please log out and log back in again.";
                    ArrayOfErrorInfo arErrUser = new ArrayOfErrorInfo();
                    arErrUser.Add(new ErrorInfo("INVALID TOKEN", "Your session has timed out and is now expired.  Please log out and log back in again.", ""));
                    Order.Errors = arErrUser;
                    return Order;
                }
            }
            //CHG0088851 - Added customized SOAP error message for invalid token - End.
            //STAR Retrofit II.Ch1 - START: Modified to append Zeros infront of RequestID to match the IVR Criteria for 22 digits
            if (Order.S.AppName == "IVR")
            {
                if ((Order.Detail.RequestID.Length < 22) && (Order.Detail.RequestID.Length > 0))
                {
                    int diff = 22 - Order.Detail.RequestID.Length;
                    for (int i = 0; i < diff; i++)
                        Order.Detail.RequestID = "0" + Order.Detail.RequestID;
                }
            }
            //STAR Retrofit II.Ch1 - END
            //67811A0 Ch4 - Start - PCI Remediation for Payment systems :Arcsight logging - Success/Failure mesaages and Object Deletion

            try
            {
                if ((Order.Detail.PaymentType != (int)PaymentClasses.PaymentTypes.CreditCard) && (Order.Detail.PaymentType != (int)PaymentClasses.PaymentTypes.ECheck))
                {
                    Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DESTINATION_PROCESSNAME_PAYMENT;
                    Logger.SourceUserName = UserId;
                    Logger.SourceProcessName = AppId;
                    if (Order.Errors == null)
                    {
                        Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_LOW;
                        Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS;
                        Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_DEVICEACTION_CASHCHECK + Order.Detail.ReceiptNumber;
                        Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_CASH;
                        Logger.ArcsightLog();
                    }
                    else
                    {
                        Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                        Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                        Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_DEVICEACTION_CASHCHECK_FAILURE;
                        Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_CASH_FAILURE;
                        Logger.ArcsightLog();
                    }
                }

                return Order;

            }
            finally
            {
                if ((Order.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.CreditCard) || (Order.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.ECheck))
                {
                    //Order.Card = null;
                    if (AppId.Trim().ToUpper() == "APDS")
                    {
                    }
                    else
                    {

                        Order.Card = null;
                        Order.echeck = null;
                    }
                    Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DESTINATION_PROCESSNAME_PAYMENT;
                    Logger.SourceUserName = UserId;
                    Logger.SourceProcessName = AppId;
                    Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_LOW;
                    Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS;
                    Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_DEVICEACTION_ORDER_NULL;
                    Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_CC_DELETED;
                    Logger.ArcsightLog();
                }

            }

            //67811A0 Ch4 - Start - PCI Remediation for Payment systems :Arcsight logging - Success/Failure mesaages and Object Deletion
        }

        #endregion

        #region Authenticate
        private static Regex RoleReg = new Regex("[\\[\\]]", RegexOptions.Compiled);
        /// <summary>
        /// Authenticates a User, and returns a user info object that contains all of the
        /// available information about the user plus a security token for use with other methods
        /// in this service.       
        /// </summary>        
        [WebMethod(Description = "Authenticates User")]
        //SSO-Integration.Ch1:Added new Soap header Attribute to the method to receive the Soap header information by Cognizant on 09/22/20101
        [SoapHeader("openToken", Direction = SoapHeaderDirection.In)]
        public UserInfo Authenticate(string UserId, string Password, string AppId)
        {

            //SSO-Integration.Ch2: Start of Code- added by Cognizant to handle the Single sign on process by Cognizant on 09/22/2010.
            string userId = string.Empty;
            string securityToken = string.Empty;
            string applicationName = string.Empty;
            string agentConfigPath = string.Empty;
            UserInfo userInfo = null;
            ErrorInfo errorInfo = null;
            //67811A0 - CH5- START- PCI Remediation for Payment systems :Arcsight logging

            Logger.SourceUserName = UserId;
            Logger.SourceProcessName = AppId;
            Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DESTINATION_PROCESSNAME_LOGIN;


            if (ConfigurationSettings.AppSettings["SSOIntegratedApplications"] != null && ConfigurationSettings.AppSettings["SSOIntegratedApplications"].ToString() != string.Empty)
            {
                applicationName = ConfigurationSettings.AppSettings["SSOIntegratedApplications"].ToString();
            }
            try
            {
                if (applicationName.IndexOf(AppId) >= 0)
                {

                    if (openToken != null && openToken.Token.ToString() != string.Empty)
                    {
                        try
                        {
                            userInfo = new UserInfo();
                            securityToken = openToken.Token.ToString();

                            //Provide the path for Agent.config file that is received from PIT-SSO Server SSO.AgentConfigPath
                            if (ConfigurationSettings.AppSettings["SSO.AgentConfigPath"] != null && ConfigurationSettings.AppSettings["SSO.AgentConfigPath"].ToString() != string.Empty)
                            {
                                agentConfigPath = ConfigurationSettings.AppSettings["SSO.AgentConfigPath"].ToString();
                                Agent agent = new Agent(agentConfigPath);
                                IDictionary tokenDictionary = new Dictionary<string, string>();
                                tokenDictionary = agent.ReadToken(securityToken);
                                if (tokenDictionary != null)
                                {
                                    userId = (string)tokenDictionary[Agent.TOKEN_SUBJECT];
                                    if (userId != string.Empty)
                                    {
                                        if (userId != CSAAWeb.Constants.SSO_FAIL)
                                        {
                                            if (Auth == null) Auth = new Authentication(new SessionInfo(userId, AppId));
                                            userInfo = CheckAuth(UserId, AppId).SSOAuthenticate(userId, Password, true);
                                            //67811A0 Ch5 - START - PCI Remediation for Payment systems :Arcsight logging

                                            Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS;
                                            Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_DEVICEACTION_AUTH_SUCCESS;
                                            Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_SUCCESS;
                                            Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_LOW;
                                        }

                                        else
                                        {
                                            errorInfo = new ErrorInfo("", CSAAWeb.Constants.USER_IS_NOT_AUTHORIZED_IN_SSO_SERVER, AppId.ToString());
                                            Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                                            Logger.DeviceAction = CSAAWeb.Constants.USER_IS_NOT_AUTHORIZED_IN_SSO_SERVER;
                                            Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;
                                            Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                                            userInfo.AddError(errorInfo);
                                        }
                                    }
                                    else
                                    {
                                        errorInfo = new ErrorInfo("", CSAAWeb.Constants.SSO_USER_ID_IS_MISSING_IN_OPEN_TOKEN /*"User ID is missing in Open Token"*/, AppId.ToString());
                                        Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                                        Logger.DeviceAction = CSAAWeb.Constants.SSO_USER_ID_IS_MISSING_IN_OPEN_TOKEN;
                                        Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;
                                        Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                                        userInfo.AddError(errorInfo);
                                    }
                                }
                                else
                                {
                                    errorInfo = new ErrorInfo("", CSAAWeb.Constants.SSO_OPEN_TOKEN_IS_INVALID_OR_EXPIRED /*"Open Token is invalid or expired"*/, AppId.ToString());
                                    Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                                    Logger.DeviceAction = CSAAWeb.Constants.SSO_OPEN_TOKEN_IS_INVALID_OR_EXPIRED;
                                    Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;
                                    Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                                    userInfo.AddError(errorInfo);
                                }
                            }
                            else
                            {
                                errorInfo = new ErrorInfo("", CSAAWeb.Constants.SSO_AGENT_CONFIG_FILE_PATH_NOT_FOUND /*"Agent.Config file path not found."*/, AppId.ToString());
                                Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                                Logger.DeviceAction = CSAAWeb.Constants.SSO_AGENT_CONFIG_FILE_PATH_NOT_FOUND;
                                Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;
                                Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                                userInfo.AddError(errorInfo);
                            }
                        }
                        catch (TokenExpiredException expiredToken)
                        {
                            string msg = expiredToken.ToString();
                            errorInfo = new ErrorInfo("", CSAAWeb.Constants.SSO_OPEN_TOKEN_IS_INVALID_OR_EXPIRED /*"Open Token is invalid or expired"*/, AppId.ToString());
                            Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                            Logger.DeviceAction = CSAAWeb.Constants.SSO_OPEN_TOKEN_IS_INVALID_OR_EXPIRED;
                            Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;
                            Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                            Logger.ArcsightLog();
                            userInfo.AddError(errorInfo);
                            Logger.Log(expiredToken);
                        }
                        catch (TokenException tokenException)
                        {
                            string errorMessage = string.Empty;
                            if (ConfigurationSettings.AppSettings["EM_DEFAULT"] != null && ConfigurationSettings.AppSettings["EM_DEFAULT"].ToString() != string.Empty)
                            {
                                errorMessage = ConfigurationSettings.AppSettings["EM_DEFAULT"].ToString();
                            }
                            errorInfo = new ErrorInfo("", errorMessage, AppId.ToString());
                            userInfo.AddError(errorInfo);
                            Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                            Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;

                            Logger.DeviceAction = errorMessage;
                            Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;
                            Logger.ArcsightLog();
                            Logger.Log(tokenException);
                        }

                    }

                    else
                    {
                        userInfo = new UserInfo();
                        if (UserId == string.Empty && Password == string.Empty)
                        {
                            errorInfo = new ErrorInfo("", CSAAWeb.Constants.SSO_EITHER_OPEN_TOKEN_OR_USERID_AND_PASSWORD_IS_NEEDED_IN_THE_INPUT /*"Either Open Token or userId and password is needed in the input"*/, AppId.ToString());
                            Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                            Logger.DeviceAction = CSAAWeb.Constants.SSO_EITHER_OPEN_TOKEN_OR_USERID_AND_PASSWORD_IS_NEEDED_IN_THE_INPUT;
                            Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;
                            Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                            userInfo.AddError(errorInfo);
                        }
                        else if (UserId == string.Empty)
                        {
                            errorInfo = new ErrorInfo("", CSAAWeb.Constants.SSO_USER_ID_IS_MISSING_IN_INPUT_REQUEST /*"User ID is missing in Input request"*/, AppId.ToString());
                            Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                            Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                            Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;
                            Logger.DeviceAction = CSAAWeb.Constants.SSO_USER_ID_IS_MISSING_IN_INPUT_REQUEST;

                            userInfo.AddError(errorInfo);
                        }
                        else if (Password == string.Empty)
                        {
                            errorInfo = new ErrorInfo("", CSAAWeb.Constants.SSO_PASSWORD_IS_MISSING_IN_INPUT_REQUEST /*"Password is missing in Input request"*/, AppId.ToString());
                            Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                            Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                            Logger.DeviceAction = CSAAWeb.Constants.SSO_PASSWORD_IS_MISSING_IN_INPUT_REQUEST;
                            Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;
                            userInfo.AddError(errorInfo);
                        }
                        else
                        {
                            if (Auth == null) Auth = new Authentication(new SessionInfo(UserId, AppId));
                            //RFC 185138 - AD Integration - CH2 START :Added/modified the code to validate the User ID and password against the Active Directory for SSO Applications first time authentication
                            string AD_Auth = AuthenticationClasses.APDSUserInfo.GetUser(UserId, Password);
                            if (AD_Auth == CSAAWeb.Constants.AD_AUTH_SUCCESS)
                            {
                                userInfo = CheckAuth(UserId, AppId).Authenticate(UserId, Password);

                                if (userInfo.Authenticated)
                                {
                                    Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS;
                                    Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_LOW;
                                    Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_DEVICEACTION_AUTH_SUCCESS;
                                    Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_SUCCESS;


                                }
                            }
                            //RFC 185138 - AD Integration - CH3 START-Commented the  locked out check event
                            //else if (userInfo.IsLockedOut)
                            //{

                                //    Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                            //    Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                            //    Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_LOGIN_LOCKOUT;
                            //    Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;

                                //}
                            //RFC 185138 - AD Integration - CH3 END-Commented the  locked out check event
                            else
                            {
                                errorInfo = new ErrorInfo("", AD_Auth, AppId.ToString());
                                userInfo.AddError(errorInfo);
                                Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                                Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                                Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_LOGIN_INCORRECT;
                                Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;
                                Logger.Log(AD_Auth + " - " + UserId + " - " + AppId);
                            }
                            //RFC 185138 - AD Integration - CH2 END :Added/modified the code to validate the User ID and password against the Active Directory for SSO Applications first time authentication

                        }
                    }

                }
                else
                {

                    if (Auth == null) Auth = new Authentication(new SessionInfo(UserId, AppId));
                    //RFC 185138 - AD Integration - Added the conditon to check if the userId used to login is the Service account created for RBAC.
                    if ((Convert.ToString(Config.Setting("Services.ActiveDirectory")).IndexOf(AppId.Trim().ToUpper()) > -1))
                    {
                        string AD_Auth = AuthenticationClasses.APDSUserInfo.GetUser(UserId, Password);
                        if (AD_Auth == CSAAWeb.Constants.AD_AUTH_SUCCESS)
                        {
                            userInfo = CheckAuth(UserId, AppId).Authenticate(UserId, Password);

                            if (userInfo.Authenticated)
                            {
                                Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS;
                                Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_LOW;
                                Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_DEVICEACTION_AUTH_SUCCESS;
                                Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_SUCCESS;


                            }
                        }
                        else
                        {

                            Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                            Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                            Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;
                            Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;
                            Logger.Log(AD_Auth + " - " + UserId + " - " + AppId);

                        }
                    }
                    else
                    {
                        userInfo = CheckAuth(UserId, AppId).Authenticate(UserId, Password);
                        Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DESTINATION_PROCESSNAME_LOGIN;
                        Logger.SourceProcessName = AppId;
                        Logger.SourceUserName = UserId;
                    }
                    if (userInfo.Authenticated)
                    {

                        Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS;
                        Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_LOW;
                        Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_DEVICEACTION_AUTH_SUCCESS;
                        Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_SUCCESS;

                    }
                    //RFC 185138 - AD Integration - CH4 START-Commented the  locked out check event
                    //else if (userInfo.IsLockedOut)
                    //{
                    //    Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                    //    Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                    //    Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_LOGIN_LOCKOUT;
                    //    Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;
                    //}
                    //RFC 185138 - AD Integration - CH4 END-Commented the  locked out check event
                    else
                    {
                        //RFC 185138 - AD Integration - CH5 START-Added the error info message in case of invalid user in payment tool
                        string errorMessage = string.Empty;
                        if (Config.Setting("EM_DEFAULT") != null && Config.Setting("EM_DEFAULT").ToString() != string.Empty)
                        {
                            errorMessage = Config.Setting("EM_DEFAULT").ToString();
                        }
                        errorInfo = new ErrorInfo("", errorMessage, AppId.ToString());
                        userInfo.AddError(errorInfo);
                        //RFC 185138 - AD Integration - CH5 END-Added the error info message in case of invalid user in payment tool
                        Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                        Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                        Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_LOGIN_INCORRECT;
                        Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;
                        Logger.Log(CSAAWeb.Constants.AD_AUTH_NONSSOFAILURE + " - " + UserId + " - " + AppId);
                    }


                }
            }
            //Fix for Security defect 3489.CH1: Start Added a try catch block to handle the exception duing the insert of null values in AUTH_LogActivity stored procedure on 12/21/2010 by cognizant. 
            // The below code will ensure that the generic error message is sent in the output
            // when any exception is triggered during Authentication
            catch (Exception exception)
            {
                string errorMessage = string.Empty;
                if (Config.Setting("EM_DEFAULT") != null && Config.Setting("EM_DEFAULT").ToString() != string.Empty)
                {
                    errorMessage = Config.Setting("EM_DEFAULT").ToString();
                }
                errorInfo = new ErrorInfo("", errorMessage, AppId.ToString());
                userInfo = new UserInfo();
                userInfo.AddError(errorInfo);
                Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                Logger.DeviceAction = errorMessage;
                Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_AUTHENTCATE_FAILURE;
                Logger.Log(exception);
            }
            //Fix for Security defect 3489.CH1: End
            Logger.ArcsightLog();
            //67811A0 Ch5 - END - PCI Remediation for Payment systems :Arcsight logging
            return userInfo;
            //SSO-Integration.Ch2: End of Code- 
        }

        /// <summary>
        /// Validates the user token, and that the user has permission to call the
        /// Method.  Returns errors if there are any.
        /// </summary>
        [WebMethod(Description = "Validates token.")]
        public ArrayOfErrorInfo ValidateToken(string Token, string UserId, string AppId)
        {
            if (Verified && Token == this.Token && UserId == this.UserId && AppId == this.AppId) return null;
            ArrayOfErrorInfo Result = CheckAuth(UserId, AppId, Token).ValidateSession(CSAAWeb.AppLogger.Logger.Caller());

            if (Result == null || Result.Count == 0)
            {
                this.Verified = true;
                this.Token = Token;
                this.UserId = UserId;
                this.AppId = AppId;
            }
            return Result;
        }
        #endregion
        #region GetUserInfo
        /// <summary>
        /// Returns user information.
        /// </summary>
        /// <param name="AppId">Name of the calling application.</param>
        /// <param name="Token">Security token provided by authenticate</param>
        /// <param name="UserId">UserId of the logged-in user.</param>
        /// <param name="Errors"></param>
        [WebMethod(Description = "Returns user information.")]
        public UserInfo GetUserInfo(string Token, string UserId, string AppId, out ArrayOfErrorInfo Errors)
        {
            Errors = ValidateToken(Token, UserId, AppId);
            if (Errors != null) return null;
            return CheckAuth().GetContactInfo(UserId);
        }

        #endregion
        #region Lookup
        /// <summary>
        /// Performs specified lookup function.
        /// </summary>
        /// <param name="AppId">Name of the calling application.</param>
        /// <param name="Token">Security token provided by authenticate</param>
        /// <param name="UserId">UserId of the logged-in user.</param>
        /// <param name="Service">The service from which to lookup the value.</param>
        /// <param name="What">The name of the value to lookup</param>
        /// <param name="Params">The parameters to provide to the lookup function</param>
        /// <param name="Errors"></param>
        [WebMethod(Description = "Performs specified lookup function.")]
        public DataSet LookupDataSet(string Token, string UserId, string AppId, string Service, string What, out ArrayOfErrorInfo Errors, object[] Params)
        {
            //Begin PC Phase II changes CH1 - Added the below code to skip token validation for PaymentX
            Errors = null;
            // CHG0088851 - Penetration defects - Removed the condition for PaymentX to include it in the validation process.
            //if (AppId.ToUpper().Trim() != Config.Setting("ByPassTokenVldn.OrderService.AppName"))
            //{
            Errors = ValidateToken(Token, UserId, AppId);
            //}
            //End PC Phase II changes CH1 - Added the below code to skip token validation for PaymentX
            if (Errors != null) return null;
            object Result = Lookup(Service, What, Params);
            if (typeof(ArrayOfErrorInfo).IsInstanceOfType(Result)) Errors = (ArrayOfErrorInfo)Result;
            if (Errors != null) return null;
            if (!typeof(DataSet).IsInstanceOfType(Result))
                throw new Exception("Service " + Service + " lookup " + What + " does not return a dataset; Use Lookup instead.");
            return (DataSet)Result;
        }

        /// <summary>
        /// Performs specified lookup function.
        /// </summary>
        /// <param name="AppId">Name of the calling application.</param>
        /// <param name="Token">Security token provided by authenticate</param>
        /// <param name="UserId">UserId of the logged-in user.</param>
        /// <param name="Service">The service from which to lookup the value.</param>
        /// <param name="What">The name of the value to lookup</param>
        /// <param name="Params">The parameters to provide to the lookup function</param>
        /// <param name="Errors"></param>
        [WebMethod(Description = "Performs specified lookup function.")]
        public object Lookup(string Token, string UserId, string AppId, string Service, string What, out ArrayOfErrorInfo Errors, object[] Params)
        {
            Errors = ValidateToken(Token, UserId, AppId);
            if (Errors != null) return null;
            object Result = Lookup(Service, What, Params);
            if (typeof(ArrayOfErrorInfo).IsInstanceOfType(Result)) Errors = (ArrayOfErrorInfo)Result;
            if (Errors != null) return null;
            if (typeof(DataSet).IsInstanceOfType(Result))
            {
                ArrayOfLookupItem A = new ArrayOfLookupItem();
                A.CopyFrom(((DataSet)Result).Tables[0]);
                return A;
            }
            else return Result;
        }

        /// <summary>
        /// Performs specified lookup function.
        /// </summary>
        /// <param name="Service">The service from which to lookup the value.</param>
        /// <param name="What">The name of the value to lookup</param>
        /// <param name="Params">The parameters to provide to the lookup function</param>
        private object Lookup(string Service, string What, object[] Params)
        {
            string ClassName;
            //START - Code added by COGNIZANT 05/31/2004
            //To resolve the class name for the SalesXReportClass WebService
            if (Service == "SalesXReportClass")
                ClassName = "SalesXReportClasses.Service.SalesXReportClass";
            else
                ClassName = "SvcClasses.Service.Svc".Replace("Svc", Service);
            //END

            if (Service == "Payment") ClassName += "s, PaymentClasses";
            Type T = (Service == "Authentication") ? CheckAuth().GetType() : Type.GetType(ClassName, false, true);
            if (T == null)
                throw new Exception("Service " + Service + " not supported.");
            Type[] Types = new Type[Params.Length];
            for (int i = 0; i < Params.Length; i++) Types[i] = Params[i].GetType();
            MethodInfo M = T.GetMethod(What, Types);
            if (M == null)
                throw new Exception("Service " + Service + " does not support " + What + ".");
            object Svc = (Service == "Authentication") ? Auth : T.GetConstructor(new Type[] { }).Invoke(new object[] { });
            object Result = M.Invoke(Svc, Params);
            if (typeof(CSAAWeb.Web.IClosableWeb).IsInstanceOfType(Svc) && Service != "Authentication")
                ((CSAAWeb.Web.IClosableWeb)Svc).Close();
            CloseAuth();
            return Result;
        }
        #endregion

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method GetRates - March 2016
        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method UpdatePassword - March 2016

        #region VoidPayments
        //67811A0  - PCI Remediation for Payment systems CH1 START: Added the new web method to perform void functionality for cash sweep payments processed from AI by cognizant on 08/25/2011.
        /// <summary>
        /// Updates the transaction status to void.
        /// </summary>
        /// <param name="AppId">Name of the calling application.</param>
        /// <param name="Token">Security token provided by authenticate</param>
        /// <param name="UserId">User Id of the user Calling this web method </param>
        /// <param name="ReceiptNumber">The Receipt number to be voided</param>

        [WebMethod(Description = "void the cash, check Payments")]

        public VoidPayments VoidPayment(string Token, string UserId, string AppId, string Receiptnumber)
        {

            InsuranceClasses.Service.Insurance Void_Payment = new InsuranceClasses.Service.Insurance();
            TurninClasses.WebService.Turnin VoidFlowCheck = new TurninClasses.WebService.Turnin();
            String AppName;
            VoidPayments Empty_Info = new VoidPayments();
            if (string.IsNullOrEmpty(UserId) || string.IsNullOrEmpty(AppId) || string.IsNullOrEmpty(Receiptnumber))
            {

                Empty_Info.Error_Message = CSAAWeb.Constants.PCI_VOIDPAYMENT_REQUIRED_FIELD;

                return Empty_Info;
            }
            //PC Phase II changes CH2 - Start - Modified Void Payment Web Method to check the Flow whether Payment Central or Payment Tool Transaction

            AppName = VoidFlowCheck.PCVoidFlowCheck(Receiptnumber);
            if (AppName == CSAAWeb.Constants.PCI_VOIDPAYMENT_APPNAME)
            {
                return Void_Payment.VoidPayment(UserId, Receiptnumber, AppId);
            }
            else
            {
                Empty_Info.Error_Message = CSAAWeb.Constants.PCI_VOIDPAYMENT_PC;
                return Empty_Info;
            }


        }
        #endregion
        //PC Phase II changes CH2 - End - Modified Void Payment Web Method to check the Flow whether Payment Central or Payment Tool Transaction
        //67811A0  - PCI Remediation for Payment systems CH1 END: Added the new web method to perform void functionality for cash sweep payments processed from AI by cognizant on 08/25/2011.

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the methods MemberSearch,MemberLookUp with respect to MembershipSerializers.cs - March 2016

        #region SalesXReport
        /// <summary>
        /// Function SalesXReport - Added by COGNIZANT 05/31/2004
        /// This function is used to invoke the method SalesXReport of the
        /// SalesXReportWebService.cs
        /// </summary>
        /// <param name="AppId">Name of the calling application.</param>
        /// <param name="Token">Security token provided by authenticate</param>
        /// <param name="UserId">UserId of the logged-in user.</param>
        /// <param name="SearchFor">The criteria to look for.</param>
        /// <returns>SalesXReportClasses.SalesXReportResults instance containing the output SalesX transactions</returns>
        [WebMethod(Description = "Generate SalesX transactions Report based on the input dates")]
        public SalesXReportClasses.SalesXReportResults SalesXReport(string Token, string UserId, string AppId, SalesXReportCriteria SearchFor)
        {
            SalesXReportClasses.SalesXReportResults Report = null;

            #region ValidateToken
            ArrayOfErrorInfo Errors = ValidateToken(Token, UserId, AppId);

            if (Errors != null && Errors.Count > 0)
            {
                Report = new SalesXReportClasses.SalesXReportResults();
                Report.Errors = Errors;
                return Report;
            }
            #endregion
            #region Validate Dates
            try
            {
                DateTime InputDate = Convert.ToDateTime(SearchFor.StartDate);
            }
            catch (Exception e)
            {
                if ((e.GetType().Equals((new ArgumentException()).GetType())) || (e.GetType().Equals((new FormatException()).GetType())) || (e.GetType().Equals((new OverflowException()).GetType())))
                {
                    Errors = new ArrayOfErrorInfo();
                    Errors.Add(new ErrorInfo("INVALID_DATE", "The date field is not valid.", "SearchFor.StartDate"));
                }
                else
                {
                    throw new Exception("Web request failed", e);
                }
            }

            try
            {
                DateTime InputDate = Convert.ToDateTime(SearchFor.EndDate);
            }
            catch (Exception e)
            {
                if ((e.GetType().Equals((new ArgumentException()).GetType())) || (e.GetType().Equals((new FormatException()).GetType())) || (e.GetType().Equals((new OverflowException()).GetType())))
                {
                    if (Errors == null)
                    {
                        Errors = new ArrayOfErrorInfo();
                    }
                    Errors.Add(new ErrorInfo("INVALID_DATE", "The date field is not valid.", "SearchFor.EndDate"));
                }
                else
                {
                    throw new Exception("Web request failed", e);
                }
            }

            if (Errors != null && Errors.Count > 0)
            {
                Report = new SalesXReportClasses.SalesXReportResults();
                Report.Errors = Errors;
                return Report;
            }
            #endregion
            //Added by cognizant as a part of HO-6 on 4-13-2010.
            SearchFor.SetAppId(AppId);

            DataSet Result = LookupDataSet(Token, UserId, AppId, "SalesXReportClass", "SalesXReport", out Errors, new object[] { SearchFor });
            if (Errors != null && Errors.Count > 0)
            {
                Report = new SalesXReportClasses.SalesXReportResults();
                Report.Errors = Errors;
                return Report;
            }

            Report = new SalesXReportClasses.SalesXReportResults();
            Report.ReportResults = new SalesXReportClasses.ArrayOfSalesXReportItem(Result);
            return Report;
        }
        #endregion

        //CSR#3937.Ch4 : START - Added a new method used for populating constants from db into application object
        private void _GetConstants()
        {
            DataSet dsConstants = new Insurance().PayGetConstants();
            StringDictionary sdConstants = new StringDictionary();

            foreach (DataRow dr in dsConstants.Tables[0].Rows)
            {
                sdConstants.Add(dr[0].ToString(), dr[1].ToString());
            }

            Application.Add("Constants", sdConstants);

        }

        //END CSR#3937.Ch4 : END - Added a new method used for populating constants from db into application object
    }
}
