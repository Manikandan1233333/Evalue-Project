/*
 * HISTORY
 *	PC Phase II changes - Added the pages as part of Enrollment changes.
 *	PC Phase II 4/20 - Added condition such that based on config entry the response is getting logged in log file. 
 *	PC Security Defect Fix -CH1 - Modified the below code to log the bank response only on config entry
 * 	MAIG - CH1 - Added the code to pass the source system as one of the parameters.
 *  MAIG - CH2 - Added the code to pass the source system to PC Service
 *	MAIG - CH3 - Commented the code which invokes the Validate Enrollment Service as part of MAIG
 *	CHG0112662 - Record Payment Enrollment SOA Service changes
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RecordEnrollment;
using System.Web;
using System.ServiceModel;
using System.IO;
using CSAAWeb;
using CSAAWeb.AppLogger;
using System.Xml;
using System.Web.Services.Protocols;
using System.Data;
using RetrievePaymentEnrollment;

namespace OrderClassesII
{
    public class PCEnrollmentMapping
    {
        #region EnrollService
        public List<string> PCEnrollService(recordAutoPayEnrollmentStatusRequest EnrollRequest)
        {
            List<string> resp_List = new List<string>();
            DataSet ds = new DataSet();
            string DO = string.Empty;            
            bool Errflag = false;
            RecordAutoPayEnrollmentStatusRequest1 ReqObj = new RecordAutoPayEnrollmentStatusRequest1();     
            recordAutoPayEnrollmentStatusResponse EnrollResp = new recordAutoPayEnrollmentStatusResponse();
            RecordAutoPayEnrollmentStatusResponse1 RespObj = new RecordAutoPayEnrollmentStatusResponse1();
            AuthenticationClasses.WebService.Authentication authObj = new AuthenticationClasses.WebService.Authentication();
            ds = authObj.GetRepIDDO(HttpContext.Current.User.Identity.Name.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                DO = (ds.Tables[0].Rows[0]["DO ID"].ToString()).Trim();
            }
            try
            {
                RecordAutoPayEnrollmentStatus _RecordEnrollProvider = (RecordAutoPayEnrollmentStatusChannel)CreateInstance<RecordAutoPayEnrollmentStatusChannel>("RecordAutoPayEnrollmentStatusSOAPPort");
                if (DO == string.Empty)
                {
                    EnrollRequest.authenticationChannel = Constants.PC_AUTHCH_CC;
                }
                else 
                {

                    if (DO.Equals(CSAAWeb.Constants.PC_DO_AUTHIVR))
                    {
                        EnrollRequest.authenticationChannel = Constants.PC_AUTHCH_IVR;
                    }
                    else if ((Config.Setting("PC_DO.BO").ToString().IndexOf(DO)) > -1 && DO.Length > 1)
                    {
                        EnrollRequest.authenticationChannel = Constants.PC_AUTHCH_BO;
                    }
                    else if ((Config.Setting("PC_DO.CC").IndexOf(DO)) > -1 && DO.Length > 1)
                    {
                        EnrollRequest.authenticationChannel = Constants.PC_AUTHCH_CC;
                    }
                    else
                    {
                        EnrollRequest.authenticationChannel = Constants.PC_AUTHCH_F2F;
                    }
                }
                ReqObj.recordAutoPayEnrollmentStatusRequest = EnrollRequest;
                if (ReqObj != null && (Config.Setting("Logging.RecordEnrollment") == "1"))
                {
                    System.Xml.Serialization.XmlSerializer serializerReq = new System.Xml.Serialization.XmlSerializer(ReqObj.GetType());
                    StringWriter writerReq = new StringWriter();
                    serializerReq.Serialize(writerReq, ReqObj);
                    Logger.Log("Enrollment request to Payment Central - Record Enrollment Request : \r\n" + writerReq);
                }
                RespObj = _RecordEnrollProvider.RecordAutoPayEnrollmentStatus(ReqObj);
                EnrollResp = RespObj.recordAutoPayEnrollmentStatusResponse;
                if (EnrollResp != null)
                {
                    string Trnxid = string.Empty;
                    if (EnrollResp.status.ToUpper() == CSAAWeb.Constants.PC_SUCESS.ToUpper())
                    {
                        resp_List.Add(EnrollResp.status.ToUpper());
                        resp_List.Add(EnrollResp.receiptNumber);
                        //return resp_List;
                    }                  
                }
                if (EnrollResp != null && (Config.Setting("Logging.RecordEnrollment") == "1"))
                {
                    System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(EnrollResp.GetType());
                    StringWriter writerRes = new StringWriter();
                    serializerRes.Serialize(writerRes, EnrollResp);
                    Logger.Log("Enrollment Response from Payment Central - Record Enrollment Response : \r\n" + writerRes);
                }
                else
                {
                    if (EnrollResp == null)
                    {
                        Logger.Log("Enrollment Response is NULL");
                    }
                }
               // PC Phase II 4/20 - Added condition such that based on config entry the response is getting logged in log file. 
                               
            }
            catch (FaultException<RecordEnrollment.ErrorInfo> faultExpErrorInfo)
            {
                if (Config.Setting("Logging.RecordEnrollment") == "1")
                {
                    if (EnrollResp != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(EnrollResp.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, EnrollResp);
                        Logger.Log("Enrollment Response from Payment Central - Record Enrollment Response : \r\n" + writerRes);
                    }
                }
                if (faultExpErrorInfo.Detail != null)
                {
                    Logger.Log(faultExpErrorInfo.Detail.errorMessageText);
                    resp_List.Add(faultExpErrorInfo.Detail.errorCode);
                    resp_List.Add(faultExpErrorInfo.Detail.errorMessageText);
                }
                Errflag = true;
                return resp_List;
            }
            catch (FaultException faultExp)
            {
                string errFriendlyMsg = "", errMsg = "", errCode = "", appendErrMsg = "";
                //PC Phase II 4/20 - Added condition such that based on config entry the response is getting logged in log file. 
                if (Config.Setting("Logging.RecordEnrollment") == "1")
                {
                    if (EnrollResp != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(EnrollResp.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, EnrollResp);
                        Logger.Log("Enrollment Response from Payment Central - Record Enrollment Response : \r\n" + writerRes);
                    }
                }
                //PC Phase II 4/20 - Added condition such that based on config entry the response is getting logged in log file. 
                FaultException<RecordEnrollment.www.aaancnuit.com.aaancnu_common_version2.ErrorInfo> errInfo = (FaultException<RecordEnrollment.www.aaancnuit.com.aaancnu_common_version2.ErrorInfo>)faultExp;
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
                    appendErrMsg = CSAAWeb.Constants.PC_ERR_BUSINESS_EXCEPTION_ENROLL + CSAAWeb.Constants.ERR_CODE_BUSINESS_EXCEPTION + "-" + errMsg;
                }
                else
                {
                    appendErrMsg = errCode + "-" + CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                }
                Logger.Log(errCode + " " + appendErrMsg);
                resp_List.Add(errCode);
                resp_List.Add(appendErrMsg);
                Errflag = true;
                return resp_List;
            }
            catch (SoapException soapExp)
            {
                //PC Phase II 4/20 - Added condition such that based on config entry the response is getting logged in log file. 
                if (Config.Setting("Logging.RecordEnrollment") == "1")
                {
                    if (EnrollResp != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(EnrollResp.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, EnrollResp);
                        Logger.Log("Enrollment Response from Payment Central - Record Enrollment Response : \r\n" + writerRes);
                    }
                }
                //PC Phase II 4/20 - Added condition such that based on config entry the response is getting logged in log file. 
                if (soapExp.Message != null)
                {
                    Logger.Log(soapExp.Message);
                    resp_List.Add(soapExp.Code.ToString());
                    resp_List.Add(CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION);
                }
                Errflag = true;
                return resp_List;
            }
            catch (Exception exp)
            {
                //PC Phase II 4/20 - Added condition such that based on config entry the response is getting logged in log file. 
                if (Config.Setting("Logging.RecordEnrollment") == "1")
                {
                    if (EnrollResp != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(EnrollResp.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, EnrollResp);
                        Logger.Log("Enrollment Response from Payment Central - Record Enrollment Response : \r\n" + writerRes);
                    }
                }
                if (exp.Message != null)
                {
                    Logger.Log(exp.Message);
                    resp_List.Add("");
                    resp_List.Add(CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION);
                }
                Errflag = true;
                return resp_List;
            }
            return resp_List;
        }
        #endregion
        #region RetreiveEnrollService
		//MAIG - CH1 - BEGIN - Added the code to pass the source system as one of the parameters.
        public RetrieveEnrollment.retrieveAutoPayEnrollmentDetailResponse PCRetrieveEnrollmentService(string policyNo, string type, string PC_ProductType, string sourceSystem)
		//MAIG - CH1 - END - Added the code to pass the source system as one of the parameters.
        {
            List<string> resp_List = new List<string>();
            bool Errflag = false;
            DataTable ProductTypes = new DataTable();
            RetrieveEnrollment.RetrieveAutoPayEnrollmentDetailRequest1 reqObj = new RetrieveEnrollment.RetrieveAutoPayEnrollmentDetailRequest1();
            RetrieveEnrollment.retrieveAutoPayEnrollmentDetailRequest retrieveReq = new RetrieveEnrollment.retrieveAutoPayEnrollmentDetailRequest();
            RetrieveEnrollment.RetrieveAutoPayEnrollmentDetailResponse1 resObj = new RetrieveEnrollment.RetrieveAutoPayEnrollmentDetailResponse1();
            //try
            //{
                RetrieveEnrollment.RetrieveAutoPayEnrollmentDetail _RetrieveEnrollmentprovider = (RetrieveEnrollment.RetrieveAutoPayEnrollmentDetailChannel)CreateInstance<RetrieveEnrollment.RetrieveAutoPayEnrollmentDetailChannel>("RetrieveAutoPayEnrollmentDetailSOAPPort");
                retrieveReq.applicationContext = new RetrieveEnrollment.ApplicationContext();
                retrieveReq.applicationContext.address = CSAAWeb.AppLogger.Logger.GetIPAddress();
                retrieveReq.applicationContext.application = Config.Setting(CSAAWeb.Constants.PC_ApplicationContext_Payment_Tool);
                retrieveReq.policy = new RetrieveEnrollment.PolicyProductSource();
                retrieveReq.policy.type = PC_ProductType;
                retrieveReq.policy.policyNumber = policyNo;
			   //MAIG - CH2 - BEGIN - Added the code to pass the source system to PC Service
                //////  MAIG Iteration2 - start - Method modified with an Additional parameter SourceSystem
                /////   Commented the old code for getting SOurcesystem & 
                ////    the request will be set with the sourcesystem passed from ManageEnrollment
                ////IssueDirectPaymentWrapper srcsystem = new IssueDirectPaymentWrapper();
                ////retrieveReq.policy.dataSource = srcsystem.DataSource(type, policyNo.Length);
                retrieveReq.policy.dataSource = sourceSystem;
			   //MAIG - CH2 - END - Added the code to pass the source system to PC Service

                reqObj.retrieveAutoPayEnrollmentDetailRequest = retrieveReq;
                if (reqObj != null && Config.Setting("Logging.RetrieveEnrollment") == "1")
                {
                    System.Xml.Serialization.XmlSerializer serializerReq = new System.Xml.Serialization.XmlSerializer(reqObj.retrieveAutoPayEnrollmentDetailRequest.GetType());
                    StringWriter writerReq = new StringWriter();
                    serializerReq.Serialize(writerReq, reqObj.retrieveAutoPayEnrollmentDetailRequest);
                    Logger.Log("Enrollment Request from Payment Central - Retrieve Enrollment Request : \r\n" + writerReq);
                    writerReq.Close();
                    
                }
                resObj = _RetrieveEnrollmentprovider.RetrieveAutoPayEnrollmentDetail(reqObj);
               if (resObj != null && Config.Setting("Logging.RetrieveEnrollment") == "1")
                {
                    System.Xml.Serialization.XmlSerializer serializerReq = new System.Xml.Serialization.XmlSerializer(resObj.retrieveAutoPayEnrollmentDetailResponse.GetType());
                    StringWriter writerReq = new StringWriter();
                    serializerReq.Serialize(writerReq, resObj.retrieveAutoPayEnrollmentDetailResponse);
                    Logger.Log("Enrollment Response from Payment Central - Retrieve Enrollment Response : \r\n" + writerReq);
                    writerReq.Close();
                    
                    
                }
                return resObj.retrieveAutoPayEnrollmentDetailResponse;
            
        }
        #endregion

        //MAIG - CH3 - BEGIN - Commented the code which invokes the Validate Enrollment Service as part of MAIG
        /*#region Validate Enrollment Service
        public List<string> PCEnrollmentValidation(validateEnrollmentRequest  req)
        {
            processEnrollmentValidationRequest request=new processEnrollmentValidationRequest();
            processEnrollmentValidationResponse response = new processEnrollmentValidationResponse();
            List<string> listResponse = new List<string>();
            try 
            {
                ValidateEnrollment _validateEnrollmentProvider = (ValidateEnrollment)CreateInstance<ValidateEnrollmentChannel>("ValidateEnrollmentEndPointImplPort");
                request.validateEnrollmentRequest = req;
                if (request != null && Config.Setting("Logging.ValidateEnrollment") == "1")
                {
                    System.Xml.Serialization.XmlSerializer serializerReq = new System.Xml.Serialization.XmlSerializer(request.GetType());
                    StringWriter writerReq = new StringWriter();
                    serializerReq.Serialize(writerReq, request);
                    Logger.Log("Enrollment Request from Payment Central - Validate Enrollment Request : \r\n" + writerReq);
                }
                response=_validateEnrollmentProvider.processEnrollmentValidation(request);
               if (response != null && Config.Setting("Logging.ValidateEnrollment") == "1")
                {
                    System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(response.GetType());
                    StringWriter writerRes = new StringWriter();
                    serializerRes.Serialize(writerRes, response);
                    Logger.Log("Enrollment Response from Payment Central - Validate Enrollment Response : \r\n" + writerRes);
                }
                if (response.validateEnrollmentResponse != null)
                {
                    if (response.validateEnrollmentResponse.enrollmentReasonCode != null)
                    {
                        listResponse.Add(response.validateEnrollmentResponse.enrollmentReasonCode);
                    }
                    else
                    {
                        listResponse.Add(string.Empty);
                    }
                    if (response.validateEnrollmentResponse.modifyReasonCode != null)
                    {
                        listResponse.Add(response.validateEnrollmentResponse.modifyReasonCode);
                    }
                    else
                    {
                        listResponse.Add(string.Empty);
                    }
                    if (response.validateEnrollmentResponse.unenrollReasonCode != null)
                    {
                        listResponse.Add(response.validateEnrollmentResponse.unenrollReasonCode);
                    }
                    else
                    {
                        listResponse.Add(string.Empty);
                    }
                    if (response.validateEnrollmentResponse.enrollmentEffectiveDate != null)
                    {
                        listResponse.Add(response.validateEnrollmentResponse.enrollmentEffectiveDate.ToString());
                    }
                    else
                    {
                        listResponse.Add(string.Empty);
                    }
                    if (response.validateEnrollmentResponse.modifyEffectiveDate != null)
                    {
                        listResponse.Add(response.validateEnrollmentResponse.modifyEffectiveDate.ToString());
                    }
                    else
                    {
                        listResponse.Add(string.Empty);
                    }
                    if (response.validateEnrollmentResponse.unenrollEffectiveDate != null)
                    {
                        listResponse.Add(response.validateEnrollmentResponse.unenrollEffectiveDate.ToString());
                    }
                    else
                    {
                        listResponse.Add(string.Empty);
                    }
                }
                return listResponse;
            }
            catch (FaultException<aaaie.com.pmtctrl.validateenrollment.endpoint.FaultInfo> faultExpErrorInfo)
            {
                string errFriendlyMsg = string.Empty, errMsg = string.Empty, errCode = string.Empty, appendErrMsg = string.Empty;
                if (Config.Setting("Logging.ValidateEnrollment") == "1")
                {
                    if (response != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(response.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, response);
                        Logger.Log("Enrollment Response from Payment Central - Validate Enrollment Response : \r\n" + writerRes);
                    }
                }
                if (faultExpErrorInfo.Detail != null)
                {

                    foreach (XmlNode node in faultExpErrorInfo.Detail.Nodes)
                        {
                            if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_FRIENDLY_ERROR_MSG))
                                errFriendlyMsg = node.InnerText;
                            if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_ERROR_MSG))
                                errMsg = node.InnerText;
                            if (node.Name.Contains(CSAAWeb.Constants.PC_ERR_NODE_ERROR_CODE))
                                errCode = node.InnerText;
                        }

                }
                if (errCode.Contains(CSAAWeb.Constants.PC_ERR_CODE_VALIDATE_ENROLLMENT_BUSINESS_EXCEPTION))
                {
                    appendErrMsg = CSAAWeb.Constants.PC_ERR_BUSINESS_EXCEPTION_ENROLL + errMsg;
                }
                else
                {
                    appendErrMsg = CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                }
                Logger.Log(errCode + " " + appendErrMsg);
                listResponse.Add(errCode);
                listResponse.Add(appendErrMsg);
                return listResponse;
            }
            catch (SoapException soapExp)
            {
                if (Config.Setting("Logging.ValidateEnrollment") == "1")
                {
                    if (response != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(response.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, response);
                        Logger.Log("Enrollment Response from Payment Central - Validate Enrollment Response : \r\n" + writerRes);
                    }
                }
                if (soapExp.Message != null)
                {
                    Logger.Log(soapExp.Message);
                    listResponse.Add(soapExp.Code.ToString());
                    listResponse.Add(CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION);
                }
                return listResponse;
            }
            catch (Exception exp)
            {
                if (Config.Setting("Logging.ValidateEnrollment") == "1")
                {
                    if (response != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(response.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, response);
                        Logger.Log("Enrollment Response from Payment Central - Validate Enrollment Response : \r\n" + writerRes);
                    }
                }
                if (exp.Message != null)
                {
                    Logger.Log(exp.Message);
                    listResponse.Add("");
                    listResponse.Add(CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION);
                }
                return listResponse;
            }
        }
        #endregion */
        //MAIG - CH3 - END - Commented the code which invokes the Validate Enrollment Service as part of MAIG
        #region RetreiveBankNameService
        public List<string> PCBankName(string routingNumber)
        {
            List<string> resp_List = new List<string>();
            bool Errflag = false;
            RecordBanknameRetrieval.RetrieveFinancialInstitutionDetailRequest1 bankReq = new RecordBanknameRetrieval.RetrieveFinancialInstitutionDetailRequest1();
            RecordBanknameRetrieval.retrieveFinancialInstitutionDetailRequest bankRequest = new RecordBanknameRetrieval.retrieveFinancialInstitutionDetailRequest();
            RecordBanknameRetrieval.RetrieveFinancialInstitutionDetailResponse1 bankRes = new RecordBanknameRetrieval.RetrieveFinancialInstitutionDetailResponse1();
            try
            {
                RecordBanknameRetrieval.RetrieveFinancialInstitutionDetail _RetrieveBankNameProvider = (RecordBanknameRetrieval.RetrieveFinancialInstitutionDetailChannel)CreateInstance<RecordBanknameRetrieval.RetrieveFinancialInstitutionDetailChannel>("RetrieveFinancialInstitutionDetailSOAPPort");               
                bankRequest.applicationContext = new RecordBanknameRetrieval.ApplicationContext();
                bankRequest.applicationContext.application = Config.Setting(CSAAWeb.Constants.PC_ApplicationContext_Payment_Tool).ToString();
                bankRequest.routingNumber = routingNumber;
                bankReq.retrieveFinancialInstitutionDetailRequest = bankRequest;
                if (Config.Setting("Logging.RetrieveEnrollment") == "1")
                {
                    System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(bankReq.GetType());
                    StringWriter writerRes = new StringWriter();
                    serializerRes.Serialize(writerRes, bankReq);
                    Logger.Log("Enrollment Request to Payment Central - Retrieve Bank name Request : \r\n" + writerRes);
                }   
                bankRes = _RetrieveBankNameProvider.RetrieveFinancialInstitutionDetail(bankReq);
                if (bankRes != null)
                {
                    //PC Security Defect Fix -CH1 START- Modified the below code to log the bank response only on config entry
                    if (Config.Setting("Logging.RetrieveEnrollment") == "1")
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(bankRes.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, bankRes);
                        Logger.Log("Enrollment Response from Payment Central - Retrieve Bank name Response : \r\n" + writerRes);
                    }
                    if (!string.IsNullOrEmpty(bankRes.retrieveFinancialInstitutionDetailResponse.financialInstitution.shortName))
                    {
                        resp_List.Add(CSAAWeb.Constants.PC_SUCESS.ToUpper());
                        resp_List.Add(bankRes.retrieveFinancialInstitutionDetailResponse.financialInstitution.shortName);
                    }
                    //PC Security Defect Fix -CH1 END- Modified the below code to log the bank response only on config entry
                }
                return resp_List;
            }
            catch (FaultException<RecordBanknameRetrieval.ErrorInfo> faultExpErrorInfo)
            {
                if (Config.Setting("Logging.RetrieveEnrollment") == "1")
                {
                    if (bankRes != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(bankRes.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, bankRes);
                        Logger.Log("Enrollment Response from Payment Central - Retrieve Bank name Response : \r\n" + writerRes);
                    }
                }
                if (faultExpErrorInfo.Detail != null)
                {
                    Logger.Log(faultExpErrorInfo.Detail.errorMessageText);
                    resp_List.Add(faultExpErrorInfo.Detail.errorCode);
                    resp_List.Add(faultExpErrorInfo.Detail.errorMessageText);
                }
                Errflag = true;
                return resp_List;
            }
            catch (FaultException faultExp)
            {
                string errFriendlyMsg = "", errMsg = "", errCode = "", appendErrMsg = "";
                if (Config.Setting("Logging.RetrieveEnrollment") == "1")
                {
                    if (bankRes != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(bankRes.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, bankRes);
                        Logger.Log("Enrollment Response from Payment Central - Retrieve Bank name Response : \r\n" + writerRes);
                    }
                }
                FaultException<RecordBanknameRetrieval.www.aaancnuit.com.aaancnu_common_version2.ErrorInfo> errInfo = (FaultException<RecordBanknameRetrieval.www.aaancnuit.com.aaancnu_common_version2.ErrorInfo>)faultExp;
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
                    appendErrMsg = CSAAWeb.Constants.PC_ERR_BUSINESS_EXCEPTION_ENROLL + CSAAWeb.Constants.ERR_CODE_BUSINESS_EXCEPTION + "-" + errMsg;
                }
                else
                {
                    appendErrMsg = errCode + "-" + CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                }
                Logger.Log(errCode + " " + appendErrMsg);
                resp_List.Add(errCode);
                resp_List.Add(appendErrMsg);
                Errflag = true;
                return resp_List;
            }
            catch (SoapException soapExp)
            {
                if (Config.Setting("Logging.RetrieveEnrollment") == "1")
                {
                    if (bankRes != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(bankRes.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, bankRes);
                        Logger.Log("Enrollment Response from Payment Central - Retrieve Bank name Response : \r\n" + writerRes);
                    }
                }
                if (soapExp.Message != null)
                {
                    Logger.Log(soapExp.Message);
                    resp_List.Add(soapExp.Code.ToString());
                    resp_List.Add(CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION);
                }
                Errflag = true;
                return resp_List;
            }
            catch (Exception exp)
            {
                if (Config.Setting("Logging.RetrieveEnrollment") == "1")
                {
                    if (bankRes != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(bankRes.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, bankRes);
                        Logger.Log("Enrollment Response from Payment Central - Retrieve Bank name Response : \r\n" + writerRes);
                    }
                }
                if (exp.Message != null)
                {
                    Logger.Log(exp.Message);
                    resp_List.Add("");
                    resp_List.Add(CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION);
                }
                Errflag = true;
                return resp_List;
            }
        }
        #endregion
        private T CreateInstance<T>(string endpointConfigurationName)
        {
            ChannelFactory<T> customChannelFactory = new ChannelFactory<T>(endpointConfigurationName);
            return customChannelFactory.CreateChannel();
        }


        #region Payment EnrollService
        /// <summary>
        /// CHG0112662 - Record Payment Enrollment SOA Service changes - new method to invoke payment enrollment service
        /// </summary>
        /// <param name="EnrollRequest"></param>
        /// <returns></returns>
        public List<string> PCPaymentEnrollService(RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest EnrollRequest)
        {
            List<string> resp_List = new List<string>();
            DataSet ds = new DataSet();
            string DO = string.Empty;
            bool Errflag = false;
            RecordPaymentEnrollment.RecordPaymentEnrollmentStatusRequest1 ReqObj = new RecordPaymentEnrollment.RecordPaymentEnrollmentStatusRequest1();
            RecordPaymentEnrollment.recordPaymentEnrollmentStatusResponse EnrollResp = new RecordPaymentEnrollment.recordPaymentEnrollmentStatusResponse();
            RecordPaymentEnrollment.RecordPaymentEnrollmentStatusResponse1 RespObj = new RecordPaymentEnrollment.RecordPaymentEnrollmentStatusResponse1();
            AuthenticationClasses.WebService.Authentication authObj = new AuthenticationClasses.WebService.Authentication();
            ds = authObj.GetRepIDDO(HttpContext.Current.User.Identity.Name.ToString());
            if (ds.Tables[0].Rows.Count > 0)
            {
                DO = (ds.Tables[0].Rows[0]["DO ID"].ToString()).Trim();
            }
            try
            {
                RecordPaymentEnrollment.RecordPaymentEnrollmentStatus _RecordEnrollProvider = (RecordPaymentEnrollment.RecordPaymentEnrollmentStatusChannel)CreateInstance<RecordPaymentEnrollment.RecordPaymentEnrollmentStatusChannel>("RecordPaymentEnrollmentStatusSOAPPort");
                if (DO == string.Empty)
                {
                    EnrollRequest.authenticationChannel = Constants.PC_AUTHCH_CC;
                }
                else
                {

                    if (DO.Equals(CSAAWeb.Constants.PC_DO_AUTHIVR))
                    {
                        EnrollRequest.authenticationChannel = Constants.PC_AUTHCH_IVR;
                    }
                    else if ((Config.Setting("PC_DO.BO").ToString().IndexOf(DO)) > -1 && DO.Length > 1)
                    {
                        EnrollRequest.authenticationChannel = Constants.PC_AUTHCH_BO;
                    }
                    else if ((Config.Setting("PC_DO.CC").IndexOf(DO)) > -1 && DO.Length > 1)
                    {
                        EnrollRequest.authenticationChannel = Constants.PC_AUTHCH_CC;
                    }
                    else
                    {
                        EnrollRequest.authenticationChannel = Constants.PC_AUTHCH_F2F;
                    }
                }
                ReqObj.recordPaymentEnrollmentStatusRequest = EnrollRequest;
                if (ReqObj != null && (Config.Setting("Logging.RecordEnrollment") == "1"))
                {
                    System.Xml.Serialization.XmlSerializer serializerReq = new System.Xml.Serialization.XmlSerializer(ReqObj.GetType());
                    StringWriter writerReq = new StringWriter();
                    serializerReq.Serialize(writerReq, ReqObj);
                    Logger.Log("Payment Enrollment request to Payment Central - Record Payment Enrollment Request : \r\n" + writerReq);
                }
                RespObj = _RecordEnrollProvider.RecordPaymentEnrollmentStatus(ReqObj);
                EnrollResp = RespObj.recordPaymentEnrollmentStatusResponse;
                if (EnrollResp != null)
                {
                    string Trnxid = string.Empty;
                    if (EnrollResp.status.ToUpper() == CSAAWeb.Constants.PC_SUCESS.ToUpper())
                    {
                        resp_List.Add(EnrollResp.status.ToUpper());
                        resp_List.Add(EnrollResp.receiptNumber);
                        //return resp_List;
                    }
                }
                if (EnrollResp != null && (Config.Setting("Logging.RecordEnrollment") == "1"))
                {
                    System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(EnrollResp.GetType());
                    StringWriter writerRes = new StringWriter();
                    serializerRes.Serialize(writerRes, EnrollResp);
                    Logger.Log("Payment Enrollment Response from Payment Central - Record Payment Enrollment Response : \r\n" + writerRes);
                }
                else
                {
                    if (EnrollResp == null)
                    {
                        Logger.Log("Payment Enrollment Response is NULL");
                    }
                }
                // PC Phase II 4/20 - Added condition such that based on config entry the response is getting logged in log file. 

            }
            catch (FaultException<RecordPaymentEnrollment.ErrorInfo> faultExpErrorInfo)
            {
                if (Config.Setting("Logging.RecordEnrollment") == "1")
                {
                    if (EnrollResp != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(EnrollResp.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, EnrollResp);
                        Logger.Log("Payment Enrollment Response from Payment Central - Record Payment Enrollment Response : \r\n" + writerRes);
                    }
                }
                if (faultExpErrorInfo.Detail != null)
                {
                    Logger.Log(faultExpErrorInfo.Detail.errorMessageText);
                    resp_List.Add(faultExpErrorInfo.Detail.errorCode);
                    resp_List.Add(faultExpErrorInfo.Detail.errorMessageText);
                }
                Errflag = true;
                return resp_List;
            }
            catch (FaultException faultExp)
            {
                string errFriendlyMsg = "", errMsg = "", errCode = "", appendErrMsg = "";
                //PC Phase II 4/20 - Added condition such that based on config entry the response is getting logged in log file. 
                if (Config.Setting("Logging.RecordEnrollment") == "1")
                {
                    if (EnrollResp != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(EnrollResp.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, EnrollResp);
                        Logger.Log("Enrollment Response from Payment Central - Record Enrollment Response : \r\n" + writerRes);
                    }
                }
                //PC Phase II 4/20 - Added condition such that based on config entry the response is getting logged in log file. 
                FaultException<RecordPaymentEnrollment.www.aaancnuit.com.aaancnu_common_version2.ErrorInfo> errInfo = (FaultException<RecordPaymentEnrollment.www.aaancnuit.com.aaancnu_common_version2.ErrorInfo>)faultExp;
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
                    appendErrMsg = CSAAWeb.Constants.PC_ERR_BUSINESS_EXCEPTION_ENROLL + CSAAWeb.Constants.ERR_CODE_BUSINESS_EXCEPTION + "-" + errMsg;
                }
                else
                {
                    appendErrMsg = errCode + "-" + CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION;
                }
                Logger.Log(errCode + " " + appendErrMsg);
                resp_List.Add(errCode);
                resp_List.Add(appendErrMsg);
                Errflag = true;
                return resp_List;
            }
            catch (SoapException soapExp)
            {
                //PC Phase II 4/20 - Added condition such that based on config entry the response is getting logged in log file. 
                if (Config.Setting("Logging.RecordEnrollment") == "1")
                {
                    if (EnrollResp != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(EnrollResp.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, EnrollResp);
                        Logger.Log("Enrollment Response from Payment Central - Record Enrollment Response : \r\n" + writerRes);
                    }
                }
                //PC Phase II 4/20 - Added condition such that based on config entry the response is getting logged in log file. 
                if (soapExp.Message != null)
                {
                    Logger.Log(soapExp.Message);
                    resp_List.Add(soapExp.Code.ToString());
                    resp_List.Add(CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION);
                }
                Errflag = true;
                return resp_List;
            }
            catch (Exception exp)
            {
                //PC Phase II 4/20 - Added condition such that based on config entry the response is getting logged in log file. 
                if (Config.Setting("Logging.RecordEnrollment") == "1")
                {
                    if (EnrollResp != null)
                    {
                        System.Xml.Serialization.XmlSerializer serializerRes = new System.Xml.Serialization.XmlSerializer(EnrollResp.GetType());
                        StringWriter writerRes = new StringWriter();
                        serializerRes.Serialize(writerRes, EnrollResp);
                        Logger.Log("Enrollment Response from Payment Central - Record Enrollment Response : \r\n" + writerRes);
                    }
                }
                if (exp.Message != null)
                {
                    Logger.Log(exp.Message);
                    resp_List.Add("");
                    resp_List.Add(CSAAWeb.Constants.PC_ERR_RUNTIME_EXCEPTION);
                }
                Errflag = true;
                return resp_List;
            }
            return resp_List;
        }
        #endregion

        #region Retreive Payment Enroll Service
        /// <summary>
        /// CHG0112662 - Record Payment Enrollment SOA Service changes - new method to invoke Retrieve enrollment service
        /// </summary>
        /// <param name="policyNo"></param>
        /// <param name="type"></param>
        /// <param name="PC_ProductType"></param>
        /// <param name="sourceSystem"></param>
        /// <returns></returns>
        public retrievePaymentEnrollmentDetailResponse PCRetrievePaymentEnrollmentService(string policyNo, string type, string PC_ProductType, string sourceSystem)
        {
            List<string> resp_List = new List<string>();
            bool Errflag = false;
            DataTable ProductTypes = new DataTable();
            RetrievePaymentEnrollmentDetailRequest1 reqObj = new RetrievePaymentEnrollmentDetailRequest1();
            retrievePaymentEnrollmentDetailRequest retrieveReq = new retrievePaymentEnrollmentDetailRequest();
            RetrievePaymentEnrollmentDetailResponse1 resObj = new RetrievePaymentEnrollmentDetailResponse1();
            //try
            //{
            RetrievePaymentEnrollmentDetailChannel _RetrieveEnrollmentprovider = (RetrievePaymentEnrollmentDetailChannel)CreateInstance<RetrievePaymentEnrollmentDetailChannel>("RetrievePaymentEnrollmentDetailSOAPPort");
            retrieveReq.applicationContext = new RetrievePaymentEnrollment.ApplicationContext();
            retrieveReq.applicationContext.address = CSAAWeb.AppLogger.Logger.GetIPAddress();
            retrieveReq.applicationContext.application = Config.Setting(CSAAWeb.Constants.PC_ApplicationContext_Payment_Tool);
            retrieveReq.policy = new RetrievePaymentEnrollment.PolicyProductSource();
            retrieveReq.policy.type = PC_ProductType;
            retrieveReq.policy.policyNumber = policyNo;
            //MAIG - CH2 - BEGIN - Added the code to pass the source system to PC Service
            //////  MAIG Iteration2 - start - Method modified with an Additional parameter SourceSystem
            /////   Commented the old code for getting SOurcesystem & 
            ////    the request will be set with the sourcesystem passed from ManageEnrollment
            ////IssueDirectPaymentWrapper srcsystem = new IssueDirectPaymentWrapper();
            ////retrieveReq.policy.dataSource = srcsystem.DataSource(type, policyNo.Length);
            retrieveReq.policy.dataSource = sourceSystem;
            //MAIG - CH2 - END - Added the code to pass the source system to PC Service

            reqObj.retrievePaymentEnrollmentDetailRequest = retrieveReq;
            if (reqObj != null && Config.Setting("Logging.RetrieveEnrollment") == "1")
            {
                System.Xml.Serialization.XmlSerializer serializerReq = new System.Xml.Serialization.XmlSerializer(reqObj.retrievePaymentEnrollmentDetailRequest.GetType());
                StringWriter writerReq = new StringWriter();
                serializerReq.Serialize(writerReq, reqObj.retrievePaymentEnrollmentDetailRequest);
                Logger.Log("Enrollment Request from Payment Central - Retrieve Enrollment Request : \r\n" + writerReq);
                writerReq.Close();

            }
            resObj = _RetrieveEnrollmentprovider.RetrievePaymentEnrollmentDetail(reqObj);
            if (resObj != null && Config.Setting("Logging.RetrieveEnrollment") == "1")
            {
                System.Xml.Serialization.XmlSerializer serializerReq = new System.Xml.Serialization.XmlSerializer(resObj.retrievePaymentEnrollmentDetailResponse.GetType());
                StringWriter writerReq = new StringWriter();
                serializerReq.Serialize(writerReq, resObj.retrievePaymentEnrollmentDetailResponse);
                Logger.Log("Enrollment Response from Payment Central - Retrieve Enrollment Response : \r\n" + writerReq);
                writerReq.Close();


            }
            return resObj.retrievePaymentEnrollmentDetailResponse;

        }
        #endregion
    }
}
