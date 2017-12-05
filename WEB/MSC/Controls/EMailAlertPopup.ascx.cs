/*
 * HISTORY
 * Created by: Cognizant on 2/20/2013
 * Decsription: This is a new file added as part of Payment Central Phase II.
 *	PC Phase II changes - Added the pages as part of Enrollment changes to get email through Pop up in unenrollment process.
 *  MAIG - CH1 - Adding region which contains un-used code
 *  MAIG - CH2 - Adding variable and set the value based on the entered poicy is valid or invalid
 *  MAIG - CH3 - Logic added to pass the Blaze rule if it is an valid flow
 *  MAIG - CH4 - Including Agent ID, Agency ID & SourceSystem to the Enrollment Request
 *  MAIG - CH5 - Condition added to check if it is an valid policy number
 *  MAIG - CH6 - Adding Email ID value to the REQUEST
 *  MAIG - CH7 - Disabling the PT Email trigger
 *  MAIG - CH8 - Disabling the PT Email trigger
 *  MAIG - CH9 - Disabling the PT Email trigger
 *  MAIG - CH10 - Disabling the PT Email trigger
 *  MAIG - CH11 - Disabling the PT Email trigger and set the Email ID value to PC Request
 *  MAIG - CH12 - Modified code to display the Error Code only once
 *  MAIG - CH13 - Modified code to display the Error Code only once
 * CHG0112662 - Payment Enrollment SOA Service changes
*/
#region Namespace
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using CSAAWeb;
using CSAAWeb.AppLogger;
using System.Collections.Specialized;
using System.Text.RegularExpressions;
using OrderClassesII;
using RecordEnrollment;
using AuthenticationClasses;
#endregion

namespace MSC.Controls
{
    public partial class EMailAlertPopup : System.Web.UI.UserControl
    {
        #region Public properties
        public string TextBoxValue
        {
            get
            {
                return _txtEmailAddress.Text;
            }
            set
            {
                _txtEmailAddress.Text = value;
            }
        }
        public string confirmationNumber { get; set; }
        public string policyNumber { get; set; }
        public string paymentType { get; set; }
        public string CCCustomerName { get; set; }
        public string CCNumber { get; set; }
        public string ECCustomerName { get; set; }
        public string ECAccType { get; set; }
        public string ECBankName { get; set; }
        public string ECAccNumber { get; set; }
        #endregion

        #region Events

        protected void SendMailBtn_Click(object sender, ImageClickEventArgs e)
        {

            string status = string.Empty;
            try
            {
                bool result = true;
                if (!string.IsNullOrEmpty(TextBoxValue))
                {
                    string patternStrict = Constants.PC_EMAIL_REGX;
                    Regex reStrict = new Regex(patternStrict);
                    result = reStrict.IsMatch(TextBoxValue);
                    if (!result)
                    {
                        lblError.Visible = true;
                        lblError.Text = CSAAWeb.Constants.PC_EMAIL_FAIL;
                        Logger.Log("Result value is" + result.ToString() + ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).PolicyNumber);
                    }
                    else
                    {
                        lblError.Visible = false;
                        status = CSAAWeb.Constants.PC_SUCESS;

                    }
                }

                Logger.Log("Result value is" + result.ToString() + ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).PolicyNumber);
                CreateRequestAndGetResponse(result, status);

            }
            catch (System.Threading.ThreadAbortException threadException)
            {
                Logger.Log(threadException.ToString());    
            }

            catch (Exception ex)
            {
                content.Visible = false;
                Logger.Log(ex.ToString());
                Label Error = (Label)this.Parent.FindControl(Constants.PC_LBLERRORMSG);
                if (Error != null)
                {
                    Error.Visible = true;
                    Error.Text = Constants.PC_ERR_RUNTIME_EXCEPTION;
                }

            }
            finally
            {
                TextBoxValue = string.Empty;
            }
        
        }

        protected void CancelBtn_Click(object sender, ImageClickEventArgs e)
        {

            content.Visible = false;
            TextBoxValue = string.Empty;
        }


        protected void Page_Load(object sender, EventArgs e)
        {
            confirmationNumber = hdnConfirmNum.Value;
            paymentType = hdnPaymentType.Value;
            policyNumber = hdnPolicyNumber.Value;
            if (paymentType.Equals(CSAAWeb.Constants.PC_Payment_CC))
            {

                CCCustomerName = hdnCCCustomerName.Value;
                CCNumber = hdnCCNumber.Value;
            }
            else
            {
                ECCustomerName = hdnECCustomerName.Value;
                ECAccType = hdnECAccType.Value;
                ECBankName = hdnECBankName.Value;
                ECAccNumber = hdnECAccNumber.Value;
            }
            lblError.Visible = false;
            lblError.Text = string.Empty;
            
        }

        #endregion
		//MAIG - CH1 - BEGIN -  Adding region which contains un-used code
        #region Methods - That went UnUsed as part of MAIG Integration
        //PC Phase II Ch1 - Start - Modified code as a part to trigger mail on Unenrollment successful
        /// <summary>
        /// Trigger mail to given mail id on Unenrollment successful
        /// </summary>
        /// <param name="emailid"></param>
        /// <param name="confirmNo"></param>
        protected void SendMail(string emailid, string Status)
        {

            try
            {
                if (!string.IsNullOrEmpty(emailid.Trim()))
                {
                    MailMessage msg = CreateMessage(emailid, Status);
                    SmtpClient SmtpServer = new SmtpClient(Config.Setting(Constants.PC_SMTPSERVER));
                    SmtpServer.Send(msg);
                }
            }
            catch (Exception ex)
            {

                Logger.Log(ex.ToString());
            }


        }

        /// <summary>
        /// Create email body and send the same to mentioned email id
        /// </summary>
        MailMessage CreateMessage(string emailid, string status)
        {
            MailDefinition md = new MailDefinition();
            if (paymentType.Equals(CSAAWeb.Constants.PC_Payment_CC))
            {
                if (status.Equals(CSAAWeb.Constants.PC_SUCESS))
                {
                    md.BodyFileName = CSAAWeb.Constants.PC_UNENROLL_HTML;
                    md.From = Config.Setting("FromAddress");
                    md.Subject = CSAAWeb.Constants.PC_UNENROLL_SUCCESS;
                    md.Priority = MailPriority.Normal;
                    md.IsBodyHtml = true;

                    EmbeddedMailObject logo = new EmbeddedMailObject();
                    logo.Path = Server.MapPath(CSAAWeb.Constants.PC_LOGO_PATH);
                    logo.Name = "logo";
                    md.EmbeddedObjects.Add(logo);

                    EmbeddedMailObject pass = new EmbeddedMailObject();
                    pass.Path = Server.MapPath(CSAAWeb.Constants.PC_CORRECT_ICON_PATH);
                    pass.Name = CSAAWeb.Constants.PASS_MAIL;
                    md.EmbeddedObjects.Add(pass);

                    EmbeddedMailObject thanks = new EmbeddedMailObject();
                    thanks.Path = Server.MapPath("/PaymentToolimages/thank-email.jpg");
                    thanks.Name = "thanks";
                    md.EmbeddedObjects.Add(thanks);
                }
                else
                {
                    md.BodyFileName = "/PaymentToolimages/CCFailed.html";
                    md.From = Config.Setting("FromAddress");
                    md.Subject = "Enrollment Failed";
                    md.Priority = MailPriority.Normal;
                    md.IsBodyHtml = true;

                    EmbeddedMailObject logo = new EmbeddedMailObject();
                    logo.Path = Server.MapPath(CSAAWeb.Constants.PC_LOGO_PATH);
                    logo.Name = "logo";
                    md.EmbeddedObjects.Add(logo);
                    EmbeddedMailObject fail = new EmbeddedMailObject();
                    fail.Path = Server.MapPath("/PaymentToolimages/cancel-icon.png");
                    fail.Name = "fail";
                    md.EmbeddedObjects.Add(fail);
                }
                ListDictionary replacements = new ListDictionary();
                replacements.Add("<%NAME%>", CCCustomerName.ToString());
                replacements.Add("<%DATE%>", DateTime.Now.ToString(CSAAWeb.Constants.FULL_DATE_TIME_FORMAT));
                replacements.Add("<%RECEIPT%>", confirmationNumber.ToString());
                replacements.Add("<%CARDNO%>", CCNumber.ToString());
                replacements.Add("<%POLICY%>", policyNumber.ToString().ToUpper());
                //Clearing context
                Context.Items.Clear();
                System.Net.Mail.MailMessage fileMsg;
                fileMsg = md.CreateMailMessage(emailid, replacements, this);
                return fileMsg;
            }
            else
            {
                if (status.Equals("Success"))
                {
                    md.BodyFileName = "/PaymentToolimages/email-success-ECheckunenroll.html";
                    md.From = Config.Setting("FromAddress");
                    md.Subject = CSAAWeb.Constants.PC_UNENROLL_SUCCESS;
                    md.Priority = MailPriority.Normal;
                    md.IsBodyHtml = true;

                    EmbeddedMailObject logo = new EmbeddedMailObject();
                    logo.Path = Server.MapPath(CSAAWeb.Constants.PC_LOGO_PATH);
                    logo.Name = "logo";
                    md.EmbeddedObjects.Add(logo);

                    EmbeddedMailObject pass = new EmbeddedMailObject();
                    pass.Path = Server.MapPath(CSAAWeb.Constants.PC_CORRECT_ICON_PATH);
                    pass.Name = CSAAWeb.Constants.PASS_MAIL;
                    md.EmbeddedObjects.Add(pass);

                    EmbeddedMailObject thanks = new EmbeddedMailObject();
                    thanks.Path = Server.MapPath("/PaymentToolimages/thank-email.jpg");
                    thanks.Name = "thanks";
                    md.EmbeddedObjects.Add(thanks);
                }
                else
                {
                    md.BodyFileName = "/PaymentToolimages/ECFailed.html";
                    md.From = Config.Setting("FromAddress");
                    md.Subject = "Enrollment Failed";
                    md.Priority = MailPriority.Normal;
                    md.IsBodyHtml = true;

                    EmbeddedMailObject logo = new EmbeddedMailObject();
                    logo.Path = Server.MapPath(CSAAWeb.Constants.PC_LOGO_PATH);
                    logo.Name = "logo";
                    md.EmbeddedObjects.Add(logo);
                    EmbeddedMailObject fail = new EmbeddedMailObject();
                    fail.Path = Server.MapPath("/PaymentToolimages/cancel-icon.png");
                    fail.Name = "fail";
                    md.EmbeddedObjects.Add(fail);
                }
                string[] EC = ECAccNumber.Split(new char[] { '(', ')' });
                ListDictionary replacements = new ListDictionary();
                replacements.Add("<%ACCOUNTHOLDNAME%>", ECCustomerName.ToString());
                replacements.Add("<%DATE%>", DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt"));
                replacements.Add("<%RECEIPT%>", confirmationNumber.ToString());
                replacements.Add("<%ACCOUNTTYPE%>", EC[1].ToString());
                replacements.Add("<%BANKNAME%>", ECBankName.ToString());
                replacements.Add("<%ACCOUNTNUMBER%>", EC[0]);
                replacements.Add("<%POLICYNO%>", policyNumber.ToString().ToUpper());
                System.Net.Mail.MailMessage fileMsg;
                fileMsg = md.CreateMailMessage(emailid, replacements, this);
                return fileMsg;
            }


        }

#endregion 
		//MAIG - CH1 - END -  Adding region which contains un-used code
        /// <summary>
        /// Get the details for Email content
        /// </summary>
        
        private void CreateRequestAndGetResponse(Boolean result,string status)
        {
                      
            if (result)
            {
                List<string> response = new List<string>();
				//MAIG - CH2 - BEGIN -  Adding variable and set the value based on the entered poicy is valid or invalid
                string invalidUnEnrollFlow = string.Empty;

                if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow) != null)
                    invalidUnEnrollFlow = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow;
                
                
                // Included MAIG Iteration2 - start - for getting the UserInfo details
                //OrderClasses.Service.Order authUserObj = new OrderClasses.Service.Order();
                //UserInfo userInfoObj = new UserInfo();

                //userInfoObj = authUserObj.Authenticate(HttpContext.Current.User.Identity.Name, string.Empty, CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL);
                // Included MAIG Iteration2 - end - for getting the UserInfo details
				//MAIG - CH2 - END -  Adding variable and set the value based on the entered poicy is valid or invalid
                PCEnrollmentMapping pCEnroll = new PCEnrollmentMapping();
                //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Renamed the Autpay Enrollment request to Payment Enrollment request
                RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest statusRequest = new RecordPaymentEnrollment.recordPaymentEnrollmentStatusRequest();
                statusRequest.applicationContext = new RecordPaymentEnrollment.ApplicationContext();
                //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Renamed the Autpay Enrollment request to Payment Enrollment request
                statusRequest.applicationContext.application = Config.Setting(CSAAWeb.Constants.PC_ApplicationContext_Payment_Tool);
                statusRequest.applicationContext.address = CSAAWeb.AppLogger.Logger.GetIPAddress();
                statusRequest.userId = HttpContext.Current.User.Identity.Name;
				//MAIG - CH3 - BEGIN -  Logic added to pass the Blaze rule if it is an valid flow
                if (!(invalidUnEnrollFlow.Equals("True")))
                {
                    statusRequest.enrollmentEffectiveDateSpecified = true;
                    statusRequest.enrollmentEffectiveDate = Convert.ToDateTime(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Unenrollment[2]);
                    statusRequest.enrollmentReasonCode = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Unenrollment[3];
                }
				//MAIG - CH3 - END -  Logic added to pass the Blaze rule if it is an valid flow
                //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Added the namespace
                statusRequest.policyInfo = new RecordPaymentEnrollment.PolicyProductSource();
                //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Added the namespace
                statusRequest.policyInfo.policyNumber = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).PolicyNumber;
                statusRequest.policyInfo.type = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ProductType_PC;
                            

                
				//MAIG - CH4 - BEGIN -  Including Agent ID, Agency ID & SourceSystem to the Enrollment Request
                if (string.IsNullOrEmpty(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoAgencyID) || ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoAgencyID.ToString().Equals("0"))
                    statusRequest.agency = string.Empty;
                else
                    statusRequest.agency = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoAgencyID.ToString();
                

                //////if (string.IsNullOrEmpty(userInfoObj.RepId.ToString()) || userInfoObj.RepId.ToString().Equals("0"))
                //////    statusRequest.agentIdentifier = string.Empty;
                //////else
                //////    statusRequest.agentIdentifier = userInfoObj.RepId.ToString();

                if (this.Page.User.Identity.Name.Length > 9)
                {
                    statusRequest.agentIdentifier = Convert.ToString(this.Page.User.Identity.Name).Substring(1);
                }
                else if (string.IsNullOrEmpty(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoRepID) || !((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoRepID.ToString().Equals("0"))
                {
                    statusRequest.agentIdentifier = Convert.ToString(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._UserInfoRepID);
                }
                else
                {
                    statusRequest.agentIdentifier = null;
                }


                /*if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidPolicyFlow == null) && (!(invalidUnEnrollFlow.Equals("True"))))
                {
                    statusRequest.policyInfo.dataSource = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidPolicySourceSystem;
                }
                else
                {
                    IssueDirectPaymentWrapper wrap = new IssueDirectPaymentWrapper();
                    statusRequest.policyInfo.dataSource = wrap.DataSource(((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ProductType_PT, statusRequest.policyInfo.policyNumber.Length);
                }*/
                statusRequest.policyInfo.dataSource = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidPolicySourceSystem;
				//MAIG - CH4 - END -  Including Agent ID, Agency ID & SourceSystem to the Enrollment Request
				//MAIG - CH5 - BEGIN -  Condition added to check if it is an valid policy number
                if (!(invalidUnEnrollFlow.Equals("True")))
                {
				//MAIG - CH5 - END -  Condition added to check if it is an valid policy number

                    //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Added the namespace
                    statusRequest.paymentItem = new RecordPaymentEnrollment.PaymentItemHeader();
                    //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Added the namespace
                    if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Unenrollment[1].Equals(CSAAWeb.Constants.PC_YES_NOTATION)
                        || ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Unenrollment[1].Equals(string.Empty))
                    {
                        if (paymentType.Equals(CSAAWeb.Constants.PC_Payment_CC))
                        {

                            statusRequest.paymentItem.paymentMethod = Config.Setting(CSAAWeb.Constants.PC_CreditCard_Code);
                            //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Added the namespace
                            statusRequest.paymentItem.card = new RecordPaymentEnrollment.PaymentCard();
                            //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Added the namespace
                            statusRequest.paymentItem.card.number = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._CCNumber;
                            statusRequest.paymentItem.card.type = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._CCType;
                        }
                        else
                        {
                            statusRequest.paymentItem.paymentMethod = Config.Setting(CSAAWeb.Constants.PC_ElectronicFund_Code);
                            //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Added the namespace
                            statusRequest.paymentItem.account = new RecordPaymentEnrollment.PaymentAccount();
                            //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Added the namespace
                            statusRequest.paymentItem.account.accountNumber = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ECAccountNo;
                            statusRequest.paymentItem.account.type = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ECaacType;
                        }

                        //MAIG - CH6 - BEGIN -  Adding Email ID value to the REQUEST
                        if (!string.IsNullOrEmpty(TextBoxValue))
                            statusRequest.emailTo = TextBoxValue.ToString();
                        //MAIG - CH6 - END -  Adding Email ID value to the REQUEST

                        Logger.Log("Request sent to un-Enroll for policy number " + statusRequest.policyInfo.policyNumber);
                        //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Renamed the new service method
                        response = pCEnroll.PCPaymentEnrollService(statusRequest);
                        //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Renamed the new service method
                        if (response != null)
                        {
                            if (response[0].ToString().ToUpper().Equals(CSAAWeb.Constants.PC_SUCESS.ToUpper()))
                            {
                                Logger.Log("Success Response to un-Enroll for policy number " + statusRequest.policyInfo.policyNumber);

                                //MAIG - CH7 - BEGIN -  Disabling the PT Email trigger
                                ////if (status.Equals(CSAAWeb.Constants.PC_SUCESS) && !string.IsNullOrEmpty(TextBoxValue))
                                ////{
                                ////    SendMail(TextBoxValue, status);
                                ////}
                                //MAIG - CH7 - END -  Disabling the PT Email trigger

                                Context.Items.Add(Constants.PC_VLD_UNENROLL, ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Unenrollment);
                                Server.Transfer(CSAAWeb.Constants.PC_UNENROLL_CONFIRM_PATH, false);
                            }

                            else
                            {
                                Logger.Log(Constants.PC_LOG_EMAIL_POPUP + statusRequest.policyInfo.policyNumber);
                                //MAIG - CH12 - BEGIN - Modified code to display the Error Code only once
                                //lblError.Text = response[1].ToString() + " (" + response[0].ToString() + ")";
                                lblError.Text = response[1].ToString();
                                //MAIG - CH12 - END - Modified code to display the Error Code only once
                                content.Visible = false;
                                lblError.Visible = false;
                                Label Error = (Label)this.Parent.FindControl(Constants.PC_LBLERRORMSG);
                                if (Error != null)
                                {
                                    Error.Visible = true;
                                    Error.Text = lblError.Text;
                                }
                                Logger.Log(lblError.Text.ToString());

                                //MAIG - CH8 - BEGIN -  Disabling the PT Email trigger
                                ////if (!string.IsNullOrEmpty(TextBoxValue))
                                ////{
                                ////    SendMail(TextBoxValue, CSAAWeb.Constants.PC_FAIL);
                                ////}
                                //MAIG - CH8 - END -  Disabling the PT Email trigger

                            }
                        }
                        else
                        {
                            Label Error = (Label)this.Parent.FindControl(Constants.PC_LBLERRORMSG);
                            content.Visible = false;
                            if (Error != null)
                            {
                                Error.Visible = true;
                                Error.Text = lblError.Text;
                            }
                            Logger.Log(lblError.Text.ToString());

                                //MAIG - CH9 - BEGIN -  Disabling the PT Email trigger
                            ////if (!string.IsNullOrEmpty(TextBoxValue))
                            ////{
                            ////    SendMail(TextBoxValue, CSAAWeb.Constants.PC_FAIL);
                            ////}
                                //MAIG - CH9 - END -  Disabling the PT Email trigger
                        }
                    }


                    else
                    {
                        content.Visible = false;
                        if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Unenrollment[1]).ToString().Equals("N"))
                        {
                            lblError.Text = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Unenrollment[0];
                        }

                        Label Error = (Label)this.Parent.FindControl(Constants.PC_LBLERRORMSG);
                        if (Error != null)
                        {
                            Error.Visible = true;
                            Error.Text = lblError.Text;

                        }
                        Logger.Log(lblError.Text.ToString());

                        //MAIG - CH10 - BEGIN -  Disabling the PT Email trigger
                        ////if (!string.IsNullOrEmpty(TextBoxValue))
                        ////{
                        ////    SendMail(TextBoxValue, CSAAWeb.Constants.PC_FAIL);
                        ////}
                        //MAIG - CH10 - END -  Disabling the PT Email trigger
                    }
                }
                else
                {
                        //MAIG - CH11 - BEGIN -  Disabling the PT Email trigger and set the Email ID value to PC Request
                    if (!string.IsNullOrEmpty(TextBoxValue))
                        statusRequest.emailTo = TextBoxValue.ToString();

                    Logger.Log("Request sent to un-Enroll for policy number " + statusRequest.policyInfo.policyNumber);
                    //CHG0112662 - BEGIN - Record Payment Enrollment SOA Service changes - Renamed the new service method
                    response = pCEnroll.PCPaymentEnrollService(statusRequest);
                    //CHG0112662 - END - Record Payment Enrollment SOA Service changes - Renamed the new service method
                    if (response != null)
                    {
                        if (response[0].ToString().ToUpper().Equals(CSAAWeb.Constants.PC_SUCESS.ToUpper()))
                        {
                            Logger.Log("Success Response to un-Enroll for policy number " + statusRequest.policyInfo.policyNumber);

                            ////if (status.Equals(CSAAWeb.Constants.PC_SUCESS) && !string.IsNullOrEmpty(TextBoxValue))
                            ////{
                            ////    SendMail(TextBoxValue, status);
                            ////}

                            //////Context.Items.Add(Constants.PC_VLD_UNENROLL, ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Unenrollment);
                            Server.Transfer(CSAAWeb.Constants.PC_UNENROLL_CONFIRM_PATH, false);
                        }

                        else
                        {
                            Logger.Log(Constants.PC_LOG_EMAIL_POPUP + statusRequest.policyInfo.policyNumber);
                            //MAIG - CH13 - BEGIN - Modified code to display the Error Code only once
                            //lblError.Text = response[1].ToString() + " (" + response[0].ToString() + ")";
                            lblError.Text = response[1].ToString();
                            //MAIG - CH13 - END - Modified code to display the Error Code only once
                            content.Visible = false;
                            lblError.Visible = false;
                            Label Error = (Label)this.Parent.FindControl(Constants.PC_LBLERRORMSG);
                            if (Error != null)
                            {
                                Error.Visible = true;
                                Error.Text = lblError.Text;
                            }
                            Logger.Log(lblError.Text.ToString());

                            ////if (!string.IsNullOrEmpty(TextBoxValue))
                            ////{
                            ////    SendMail(TextBoxValue, CSAAWeb.Constants.PC_FAIL);
                            ////}

                        }
                    }
                    else
                    {
                        Label Error = (Label)this.Parent.FindControl(Constants.PC_LBLERRORMSG);
                        content.Visible = false;
                        if (Error != null)
                        {
                            Error.Visible = true;
                            Error.Text = lblError.Text;
                        }
                        Logger.Log(lblError.Text.ToString());

                        ////if (!string.IsNullOrEmpty(TextBoxValue))
                        ////{
                        ////    SendMail(TextBoxValue, CSAAWeb.Constants.PC_FAIL);
                        ////}
						//MAIG - CH11 - END -  Disabling the PT Email trigger and set the Email ID value to PC Request
                    }
                }
            }
        }

    }
}