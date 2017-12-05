/*	CREATION HISTORY:
 *	CREATED BY COGNIZANT
 *	01/04/2013 - A STAND ALONE LIVE WEB SERVICE AS PART OF PAYMENT CENTRAL PHASE II TO LOAD PC TRANSACTION DETAILS TO PAYMENT TOOL
 *	Changes Done:
 *	CHGXXXX.Ch1 : Created insertIDPDetail Method Call to load the transaction details from Payment Central to Payment Tool
 *	CHGXXXX.Ch2 : Created updateIDPReversalDetail Method Call to update the transaction details from Payment Central to Payment Tool.
 *	Modified the below code to avoid duplicate receipt number enteries
 *	CHG0072116 -Modified the message for failure in update process
 *  MAIG - CH1 - Modified the logic to get the Product code directly from External Interface
*/
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Text.RegularExpressions;
using System;
using System.Xml;
using System.Web.Services.Protocols;
using System.ServiceModel;
using System.IO;



namespace PaymentIDPProxyService
{
    /// <summary>
    /// A stand alone service to load Payment Central transaction details to Payment Tool
    /// </summary>
    [WebService(Namespace = "http://csaa.com/webservices/")]

    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class PaymentIDPService : System.Web.Services.WebService
    {
        IDPProxyResponse message = null;
        Regex junk_Chars = new Regex(@"[\\\~!@#$%^*()_+{}|""?`;',/[\]]+");
        Logging log = new Logging();        

        //CHGXXXX.Ch1 : Start
        [WebMethod(Description = "Method Call to load the transaction details from Payment Central to Payment Tool")]

        public IDPProxyResponse insertIDPDetail(PaymentIDPProxyInputRequest PaymentIDPProxyInputRequest)
        {
            try
            {
            String Error = "";
            String cardEncrypt = String.Empty;
            int lineItemCount = 0;
            string paymentid = string.Empty;
            InsertPaymentitem obj = new InsertPaymentitem();
            if (ConfigurationSettings.AppSettings["LoggingEntry"].ToString()=="1")
           {
                 LogServiceRequest(PaymentIDPProxyInputRequest);

           }
                if(!string.IsNullOrEmpty(PaymentIDPProxyInputRequest.receiptNumber.ToString()))
                {
                    log.WriteLog("Input Request received for the receipt number: "+PaymentIDPProxyInputRequest.receiptNumber.ToString());
                }
            //Input Field validations
            message = (obj.Validation(PaymentIDPProxyInputRequest));
            if ((message.responseCode == Constant.RESPONSE_FAILURE))
            {
                log.WriteLog(message.responseCode + '-' + message.responseMessage);
                return message;
            }
            if (PaymentIDPProxyInputRequest.paymentMethod.Equals("CRDC") || PaymentIDPProxyInputRequest.paymentMethod.Equals("EFT"))
            {
                message = (obj.CardValidation(PaymentIDPProxyInputRequest));
                if ((message.responseCode == Constant.RESPONSE_FAILURE))
                {
                    log.WriteLog(message.responseCode + '-' + message.responseMessage);
                    return message;
                }
            }
            if (PaymentIDPProxyInputRequest.paymentMethod.Equals("CHCK"))
            {
                message = (obj.CheckValidation(PaymentIDPProxyInputRequest));
                if ((message.responseCode == Constant.RESPONSE_FAILURE))
                {
                    log.WriteLog(message.responseCode + '-' + message.responseMessage);
                    return message;
                }                
            }
            
            if (PaymentIDPProxyInputRequest.LineItem.Count >= 0)
            {
                foreach (LineItem item in PaymentIDPProxyInputRequest.LineItem)
                {
                    message = (obj.ValidationLineItem(item));
                    if (message != null)
                    {
                        if ((message.responseCode == Constant.RESPONSE_FAILURE))
                        {
                            log.WriteLog(message.responseCode + '-' + message.responseMessage);
                            return message;
                        }
                    }
                    else
                    {
                        message = new IDPProxyResponse(Constant.RESPONSE_SUCCESS, "");
                    }
                    
                }
            }                 
            
            if (message.responseCode == Constant.RESPONSE_SUCCESS)
            {
                message = new IDPProxyResponse(Constant.RESPONSE_SUCCESS, Constant.VALIDATION_SUCCESS + PaymentIDPProxyInputRequest.receiptNumber);
                
                    //Mapping the PC Data to PT Corresponding Fields

                    int[] ID_PNE = { 4, 5, 6, 13, 15, 16, 17, 18, 1, 3, 20 };


                    switch (PaymentIDPProxyInputRequest.applicationContext.application)
                    {

                        case "BILLIVR":
                            PaymentIDPProxyInputRequest.applicationContext.application = ID_PNE[2].ToString();
                            break;

                        case "HUON":
                            PaymentIDPProxyInputRequest.applicationContext.application = ID_PNE[3].ToString();
                            break;

                        case "NETPOS":
                            PaymentIDPProxyInputRequest.applicationContext.application = ID_PNE[4].ToString();
                            break;

                        case "PAS":
                            PaymentIDPProxyInputRequest.applicationContext.application = ID_PNE[5].ToString();
                            break;

                        case "AGNTINTRA":
                            PaymentIDPProxyInputRequest.applicationContext.application = ID_PNE[6].ToString();
                            break;

                        case "SIS":
                            PaymentIDPProxyInputRequest.applicationContext.application = ID_PNE[7].ToString();
                            break;

                        case "PCBCKOFF":
                            PaymentIDPProxyInputRequest.applicationContext.application = ID_PNE[10].ToString();
                            break;

                        default:
                            message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.application);
                            log.WriteLog(message.responseCode + '-' + message.responseMessage);
                            return message;

                    }
                    //Payment Method Mapping

                    switch (PaymentIDPProxyInputRequest.paymentMethod)
                    {
                        case "CRDC":
                            PaymentIDPProxyInputRequest.paymentMethod = ID_PNE[8].ToString();
                            break;

                        case "CASH":
                            PaymentIDPProxyInputRequest.paymentMethod = ID_PNE[9].ToString();
                            break;

                        case "CHCK":
                            PaymentIDPProxyInputRequest.paymentMethod = ID_PNE[0].ToString();
                            break;

                        case "EFT":
                            PaymentIDPProxyInputRequest.paymentMethod = ID_PNE[1].ToString();
                            break;

                        default:
                            message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.paymentMethod);
                            log.WriteLog(message.responseCode + '-' + message.responseMessage);
                            return message;

                    }
                    //Method Call to Insert Payment Details            
                     paymentid = obj.InsertPaymentItem(PaymentIDPProxyInputRequest);

                    //Modified the below code to avoid duplicate receipt number enteries
                    if (!string.IsNullOrEmpty(paymentid))
                    {
                        if (paymentid.Trim() == "0")
                        {
                            message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.RECEIPT_DUPLICATE);
                            log.WriteLog(message.responseCode + '-' + message.responseMessage);
                            return message;
                        }
                    }
                    
                 //Changed the Flow for Insertion and removed the if structure for multi line item  
                    foreach (LineItem Item in PaymentIDPProxyInputRequest.LineItem)
                    {

                        if (message.responseCode == Constant.RESPONSE_SUCCESS)
                        {
                            //Mapping for Product Code 
                            message = new IDPProxyResponse(Constant.RESPONSE_SUCCESS, Constant.VALIDATION_SUCCESS + PaymentIDPProxyInputRequest.receiptNumber);
                    		//MAIG - CH1 - BEGIN - Modified the logic to get the Product code directly from External Interface

                            /*if ((Item.dataSource.Equals(Constant.PAS)) & (Item.policyNumber.Length.Equals(13)))
                            {
                                if (Item.policyNumber.Substring(0, 2).Equals(Constant.CA_State))
                                {
                                    Item.productCode = Constant.AUT;
                                }
                                else
                                {
                                    Item.productCode = Constant.AAASS;
                                }
                            }
                            else if (((Item.dataSource.Equals(Constant.HUON)) & (Item.policyNumber.Length.Equals(9)) | ((Item.dataSource.Equals(Constant.POES))&&Item.productCode.Equals(Constant.ProductCode_PA)) | (Item.dataSource.Equals(Constant.ADES))))
                            {
                                Item.productCode = Constant.POES_AUTO;
                            }
                            else if (Item.dataSource.Equals(Constant.HDES) | ((Item.dataSource.Equals(Constant.POES)) && Item.productCode.Equals(Constant.ProductCode_HO)))
                            {
                                Item.productCode = Constant.HME;
                            }
                            else if (Item.dataSource.Equals(Constant.PUPSYS))
                            {
                                Item.productCode = Constant.PUP;
                            }
                            else if ((Item.dataSource.Equals(Constant.SIS)))
                            {
                                if ((Item.productCode.Equals(Constant.ProductCode_PA)))
                                {
                                    Item.productCode = Constant.WUA;
                                }
                                else if (Item.productCode.Equals(Constant.ProductCode_HO))
                                {
                                    Item.productCode = Constant.WUH;
                                }
                                else if ((Item.productCode.Equals(Constant.ProductCode_DF)))
                                {
                                    Item.productCode = Constant.DF;
                                }
                                else if (Item.productCode.Equals(Constant.ProductCode_WC))
                                {
                                    Item.productCode = Constant.WUW;
                                }
                                else if ((Item.productCode.Equals(Constant.ProductCode_MC)))
                                {
                                    Item.productCode = Constant.WUM;
                                }
                                else
                                {
                                    message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.productCode);
                                    log.WriteLog(message.responseCode + '-' + message.responseMessage);
                                    return message;
                                }
                            }
                            else if (Item.dataSource.Equals(Constant.HLSYS))
                            {
                                Item.productCode = Constant.HL;
                            }
                            else
                            {
                                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.dataSource);
                                log.WriteLog(message.responseCode + '-' + message.responseMessage);
                                return message;
                            }*/
                    		//MAIG - CH1 - END - Modified the logic to get the Product code directly from External Interface
                            //Mapping for Revenue Type
                            if (Item.revenueType == Constant.revenuetype_ERND)
                            {
                                Item.revenueType = Constant.Revenue_Earned;
                            }
                            else if (Item.revenueType == Constant.revenuetype_INST)
                            {
                                Item.revenueType = Constant.Revenue_Installment;
                            }
                            else if (Item.revenueType == Constant.revenuetype_DOWN)
                            {
                                Item.revenueType = Constant.Revenue_Down;
                            }
                            else if(!string.IsNullOrEmpty(Item.revenueType))
                            {
                                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.ERR_VALIDATION + Constant.revenueType);
                                log.WriteLog(message.responseCode + '-' + message.responseMessage);
                                return message;

                            }
                          
                            if(string.IsNullOrEmpty(PaymentIDPProxyInputRequest.checkNumber))
                            {
                                if (PaymentIDPProxyInputRequest.paymentMethod.ToUpper().Equals("CASH"))
                                {
                                    PaymentIDPProxyInputRequest.checkNumber = string.Empty;
                                }
                            }
                            Error = obj.InsertItem(Item, paymentid, PaymentIDPProxyInputRequest.checkNumber, PaymentIDPProxyInputRequest.paymentMethod,lineItemCount);
                            if ((Error == "") || (paymentid == ""))
                            {
                                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.VALIDATION_FAILURE);
                                log.WriteLog(message.responseCode + '-' + message.responseMessage);
                                return message;
                            }
                            //Invoked the Echeck/Card Payment Method SP Call
                            if (PaymentIDPProxyInputRequest.paymentMethod.Equals("1") || PaymentIDPProxyInputRequest.paymentMethod.Equals("5"))
                            {
                                message = new IDPProxyResponse(Constant.RESPONSE_SUCCESS, Constant.VALIDATION_SUCCESS + PaymentIDPProxyInputRequest.receiptNumber);
                                cardEncrypt = PaymentIDPProxyInputRequest.card.cardEcheckNumber.ToString();
                                PaymentIDPProxyInputRequest.card.cardEcheckNumber = "XXXXXXXXXXXX" + cardEncrypt.Substring(11, 4).ToString();
                                obj.InsertCardCheck(paymentid, PaymentIDPProxyInputRequest.card);
                                message = new IDPProxyResponse(Constant.RESPONSE_SUCCESS, Constant.VALIDATION_SUCCESS + PaymentIDPProxyInputRequest.receiptNumber);
                            }
                        }
                        else
                        {
                            log.WriteLog(message.responseCode + '-' + message.responseMessage);
                            return message;

                        }
                        lineItemCount++;

                    }
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
                    String Err_Msg = e.ToString();
                    message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Err_Msg);
                    log.WriteLog(message.responseCode + '-' + message.responseMessage);
                    return message;
                }          
                                   

            log.WriteLog(message.responseCode + '-' + message.responseMessage);
            message = new IDPProxyResponse(Constant.RESPONSE_SUCCESS, Constant.VALIDATION_SUCCESS + PaymentIDPProxyInputRequest.receiptNumber);
            return message;
        }

        //CHGXXXX.Ch1 : End

        //CHGXXXX.Ch2 : Start

        [WebMethod(Description = "Method Call to update the transaction details from Payment Central to Payment Tool")]

        public IDPProxyResponse updateIDPReversalDetail(Update update)
        {
            try
            {


                if (ConfigurationSettings.AppSettings["LoggingEntry"].ToString() == "1")
                {

                    System.Xml.Serialization.XmlSerializer serializerReq = new System.Xml.Serialization.XmlSerializer(update.GetType());
                    StringWriter writerReq = new StringWriter();
                    serializerReq.Serialize(writerReq, update);
                    log.WriteLog(" Payment IDP Proxy Service Request for update from Payment Central : \r\n " + writerReq);
                    writerReq.Close();

                }
                log.WriteLog("Updation Request for the Receipt Number :" + update.receiptNumber);
                string flag = string.Empty;
                IDPProxyResponse message = null;
                InsertPaymentitem obj = new InsertPaymentitem();
                //Input Field Validation
                message = (obj.UpdateValidation(update));
                if (message.responseCode == Constant.RESPONSE_SUCCESS)
                {

                    //SP CALL to invoke to update the transaction details

                    flag = obj.PCPayUpdateVoid(update);


                    if (flag == Constant.VALUE)
                    {
                        //CHG0072116 -Modified the message for failure in update process
                        message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Constant.UPDATE_NO_RCPT_NUMBER);
                        log.WriteLog(message.responseCode + '-' + message.responseMessage);
                        return message;
                    }
                    else
                    {
                        message = new IDPProxyResponse(Constant.RESPONSE_SUCCESS, Constant.UPDATE_SUCCESS + update.receiptNumber);
                        log.WriteLog(message.responseCode + '-' + message.responseMessage);
                        return message;
                    }
                }
                else
                {
                    log.WriteLog(message.responseCode + '-' + message.responseMessage);
                    return message;
                }


            }
            catch (FaultException ex)
            {
                log.WriteLog(ex.Message.ToString());
                return null;
            }
            catch (CommunicationException exp)
            {
                log.WriteLog(exp.Message.ToString());
                return null;

            }
            catch (ArgumentException ex)
            {
                String Err_Msg = ex.ToString();
                message = new IDPProxyResponse(Constant.RESPONSE_FAILURE, Err_Msg);
                log.WriteLog(message.responseCode + '-' + message.responseMessage);
                return message;
            }




        }
        //CHGXXXX.Ch2 : End

        // Added Code to Log the Service Request

        private void LogServiceRequest(PaymentIDPProxyInputRequest PaymentIDPProxyInputRequestobj)
        {

           
            try
            {

                if (PaymentIDPProxyInputRequestobj != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerReq = new System.Xml.Serialization.XmlSerializer(PaymentIDPProxyInputRequestobj.GetType());
                        StringWriter writerReq = new StringWriter();
                        serializerReq.Serialize(writerReq, PaymentIDPProxyInputRequestobj);
                        log.WriteLog(" Payment IDP Proxy Service Request from Payment Central : \r\n " + writerReq);                                               
                        writerReq.Close();
                    }                  
                
            }
            catch (Exception Ex)
            {
                log.WriteLog("Error Occurred during logging of PaymentIDPProxy Service Request XML" + Ex.InnerException.ToString());
            }
        }  


    }
}


