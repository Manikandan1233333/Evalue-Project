/*
 * HISTORY
 * PC Phase II changes - Added the page as part of Enrollment changes.Confirmation page for enrollment/modify enrollment
 * Created by: Cognizant on 2/21/2013
 * Decsription: This is a new file added as part of Payment Central Phase II.
 * PC Phase II 4/20 - modified the date time format of Echeck enrollment success scenario.
 * CHG0072116 - PC Edit Card Details CH1 - Included the conditions to show the content for email WRT edit card details.
 * CHG0072116 - PC Edit Card Details CH2 - Included the conditions to show the content for email WRT edit card details.
 * CHG0072116 - PC Edit Card Details CH3 - changed the content and subject for email WRT edit/Enrollment details.
 * CHG0072116 - PC Edit Card Details CH4 - changed the content and subject for email WRT edit/Enrollment details.
 * MAIG - CH1 - Added Condition to obtain the Enrolled status of the policy
 * MAIG - CH2 - Added Condition to obtain the BillPlan of the policy
 * MAIG - CH3 - Disabled email comms trigger from Payment Tool
 * CHG0109406 - CH1 - Modified the below timestamp to include the Arizona
*/
using System;
using System.Collections;
using System.Collections.Specialized;  
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using InsuranceClasses;
using System.Web.Security;
using CSAAWeb;
using System.Collections.Generic;
using CSAAWeb.WebControls;
using CSAAWeb.AppLogger;
using System.Web.Services;
using System.Net.Mail;

namespace MSC.Forms
{
	/// <summary>
	/// This page is the start page for the application, and currently
	/// sets up a call to primary with the order primed to enter data for
	/// a new member order.
	/// </summary>
    public partial class RecurringConfirmation : SiteTemplate
    {
        PrintEnrollmentConfirmationCC EnrollDetPrint = new PrintEnrollmentConfirmationCC();
        protected List<String> viewstateEC { get; set; }
        protected List<String> viewstateCC { get; set; }
        protected string PolicyNumber { get; set; }
		///<summary>The Url to navigate on Back button click (from web.config).</summary>
		protected string _OnBackUrl;
		///<summary>The Url to navigate on Continue button click (from web.config).</summary>
		protected string _OnContinueUrl=string.Empty;
		///<summary/>
		public override string OnCancelUrl {get {return Request.Path;} set {}}
		/// <summary>
		/// Creates a new order with blank household data and number of associates=0
		/// </summary>
		protected override void SavePageData() {
         
		}
		
		/// <summary>
		/// Authenticates, if authentication turned on, then transfers to primary.aspx.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e) {
			InitializeComponent();
			base.OnInit(e);
			
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {    
		}

        protected void Page_Load(object sender, System.EventArgs e)
        {
           ManageEnrollment mgmt=new ManageEnrollment();
           List<string> CC = new List<string>();
           List<string> EC = new List<string>();
           List<string> validateMessage = new List<string>();
           List<string> validateMdfyMessage = new List<string>();
           if (!IsPostBack)
           {
               DataTable dtTable = new DataTable();
               dtTable.Columns.Add(CSAAWeb.Constants.PC_GRID_POLICY);
               string t= dtTable.Columns[CSAAWeb.Constants.PC_GRID_POLICY].MaxLength.ToString();
               dtTable.Columns.Add(CSAAWeb.Constants.PC_GRID_Status);
               dtTable.Columns.Add(CSAAWeb.Constants.PC_Enrolled);
               dtTable.Columns.Add(CSAAWeb.Constants.PC_BILL_PLAN);

               DataRow drRow = dtTable.NewRow();
               if (Context.Items.Contains(CSAAWeb.Constants.PC_POLICYNUMBER))
               {
                   drRow[CSAAWeb.Constants.PC_GRID_POLICY] = Context.Items[CSAAWeb.Constants.PC_POLICYNUMBER].ToString().ToUpper();
               }
               drRow[CSAAWeb.Constants.PC_GRID_Status] = CSAAWeb.Constants.PC_POL_ACTIVE;
               
               //MAIG - CH1 - BEGIN - Added Condition to obtain the Enrolled status of the policy
               if ((Context.Items["PC_INVALID_POLICY_FLOW_FLAG"].ToString().ToUpper().Equals("TRUE")) || (Context.Items["PC_INVALID_UNENROLL_FLOW_FLAG"].ToString().ToUpper().Equals("TRUE")))
               {
                   drRow[CSAAWeb.Constants.PC_Enrolled] = CSAAWeb.Constants.PC_POL_ENROLL_STATUS;
               }               
               else 
               { 
               //MAIG - CH1 - END - Added Condition to obtain the Enrolled status of the policy
                   if (Context.Items.Contains(CSAAWeb.Constants.PC_IS_ENROLLED))
                   {
                       drRow[CSAAWeb.Constants.PC_Enrolled] = CSAAWeb.Constants.PC_POL_ENROLL_STATUS;

                       ViewState["IsEnrolled"] = Context.Items[CSAAWeb.Constants.PC_IS_ENROLLED].ToString();
                       if (Context.Items[CSAAWeb.Constants.PC_IS_ENROLLED].ToString().Equals(CSAAWeb.Constants.PC_POL_ENROLL_STATUS))
                       {
                           if (Context.Items.Contains(CSAAWeb.Constants.PC_VLD_MDFY_ENROLLMENT))
                           {
                               var validateEnrollmdfy = Context.Items[CSAAWeb.Constants.PC_VLD_MDFY_ENROLLMENT];
                               validateMdfyMessage = (List<string>)(validateEnrollmdfy);
                               if (validateMdfyMessage.Count >= 2)
                               {
                                   if (validateMdfyMessage[1].Equals(CSAAWeb.Constants.PC_YES_NOTATION) && validateMdfyMessage[0] != string.Empty)
                                   {
                                       lblpcErrMsg.Visible = true;
                                       lblpcErrMsg.Text = validateMdfyMessage[0].ToString();
                                   }
                               }

                           }
                           lblResponse.Text = CSAAWeb.Constants.PC_UPDATE_ENROLL_MSG;
                           //CHG0072116 - PC Edit Card Details CH1:START - Included the conditions to show the content for email WRT edit card details.
                           if(!Context.Items.Contains("Response"))
                           Context.Items.Add("Response", CSAAWeb.Constants.PC_UPDATE_ENROLL_MSG);
                           //CHG0072116 - PC Edit Card Details CH1:END - Included the conditions to show the content for email WRT edit card details.

                       }
                       else
                       {
                           if (Context.Items.Contains(CSAAWeb.Constants.PC_VLD_ENROLLMENT))
                           {
                               var validateEnroll = Context.Items[CSAAWeb.Constants.PC_VLD_ENROLLMENT];
                               validateMessage = (List<string>)(validateEnroll);
                               if (validateMessage.Count >= 2)
                               {
                                   if (validateMessage[1].Equals(CSAAWeb.Constants.PC_YES_NOTATION) && validateMessage[0] != string.Empty)
                                   {
                                       lblpcErrMsg.Visible = true;
                                       lblpcErrMsg.Text = validateMessage[0].ToString();
                                   }
                               }
                           }
                           //CHG0072116 - PC Edit Card Details CH2:START - Included the conditions to show the content for email WRT edit card details.
                           if (!Context.Items.Contains("Response"))
                           Context.Items.Add("Response", CSAAWeb.Constants.PC_THANKU_ENROLL);
                           //CHG0072116 - PC Edit Card Details CH2:END - Included the conditions to show the content for email WRT edit card details.

                       }
                   }

                   if (Context.Items.Contains(CSAAWeb.Constants.PC_BILL_PLAN))
                   {
                      //MAIG - CH2 - BEGIN - Added Condition to obtain the BillPlan of the policy
                       if (!(string.IsNullOrEmpty(Context.Items[CSAAWeb.Constants.PC_BILL_PLAN].ToString())))
                           drRow[CSAAWeb.Constants.PC_BILL_PLAN] = Context.Items[CSAAWeb.Constants.PC_BILL_PLAN].ToString();
                       else
                           drRow[CSAAWeb.Constants.PC_BILL_PLAN] = string.Empty;
                     //MAIG - CH2 - END - Added Condition to obtain the BillPlan of the policy
                   }

               }    

               dtTable.Rows.Add(drRow);
               dtTable.AcceptChanges();
               gridRecurringConfirmation.DataSource = dtTable;
               gridRecurringConfirmation.DataBind();
               //CHG0109406 - CH1 - BEGIN - Modified the below timestamp to include the Arizona
               lblDate.Text = DateTime.Now.ToString(CSAAWeb.Constants.FULL_DATE_TIME_FORMAT) +" Arizona";
               //CHG0109406 - CH1 - END - Modified the below timestamp to include the Arizona
               if (Context.Items.Contains(CSAAWeb.Constants.PC_PAYMENT_TYPE))
               {
                   if (Context.Items[CSAAWeb.Constants.PC_PAYMENT_TYPE].ToString().Equals(CSAAWeb.Constants.PC_CC))
                   {

                       UnEnrollCC.Visible = true;
                       if (Context.Items.Contains(CSAAWeb.Constants.PC_CCNumber))
                       {
                           CC.Add(Context.Items[CSAAWeb.Constants.PC_CCNumber].ToString());
                       }
                       else
                       {
                           CC.Add(string.Empty);
                       }
                       if (Context.Items.Contains(CSAAWeb.Constants.PC_CCType))
                       {
                           CC.Add(Context.Items[CSAAWeb.Constants.PC_CCType].ToString());
                       }
                       else
                       {
                           CC.Add(string.Empty);
                       }
                       if (Context.Items.Contains(CSAAWeb.Constants.PC_CUST_NAME))
                       {
                           CC.Add(Context.Items[CSAAWeb.Constants.PC_CUST_NAME].ToString());
                       }
                       else
                       {
                           CC.Add(string.Empty);
                       }
                       if (Context.Items.Contains(CSAAWeb.Constants.PC_CCEXP))
                       {
                           CC.Add(Context.Items[CSAAWeb.Constants.PC_CCEXP].ToString());
                       }
                       else
                       {
                           CC.Add(string.Empty);
                       }
                       if (Context.Items.Contains(CSAAWeb.Constants.PC_CNFRMNUMBER))
                       {
                           CC.Add(Context.Items[CSAAWeb.Constants.PC_CNFRMNUMBER].ToString());
                       }
                       else
                       {
                           CC.Add(string.Empty);
                       }
                       if (Context.Items.Contains(CSAAWeb.Constants.PC_POLICYNUMBER))
                       {
                           CC.Add(Context.Items[CSAAWeb.Constants.PC_POLICYNUMBER].ToString());                           
                           ViewState[CSAAWeb.Constants.PC_viewstate_PolicyNo] = Context.Items[CSAAWeb.Constants.PC_POLICYNUMBER].ToString(); 
                           
                       }
                       else
                       {
                           CC.Add(string.Empty);
                       }
                       if (ViewState[CSAAWeb.Constants.PC_viewstate_PolicyNo] != null)
                       {
                           if (ViewState[CSAAWeb.Constants.PC_viewstate_PolicyNo].ToString().Length > 0)
                           {
                               PolicyNumber = ViewState[CSAAWeb.Constants.PC_viewstate_PolicyNo].ToString();
                           }
                       }
                       if (Context.Items.Contains(CSAAWeb.Constants.PC_EC_BANK_NAME))
                       {
                           CC.Add(Context.Items[CSAAWeb.Constants.PC_EC_BANK_NAME].ToString());
                       }
                       else
                       {
                           CC.Add(string.Empty);
                       }
                       if (Context.Items.Contains(CSAAWeb.Constants.PC_EMAIL_ID))
                       {
                           CC.Add(Context.Items[CSAAWeb.Constants.PC_EMAIL_ID].ToString());
                       }
                       else
                       {
                           CC.Add(string.Empty);
                       }
                       if (Context.Items.Contains(Constants.PC_EDIT_CC))
                       {
                           CC.Add(Context.Items[Constants.PC_EDIT_CC].ToString());
                       }
                       UnEnrollCC.assignConfirmationValues(CC);                      
                       ViewState["viewstateCC"] = CC;
                       
                   }
                   else
                   {
                       UnEnrollECheck.Visible = true;

                       if (Context.Items.Contains(CSAAWeb.Constants.PC_CUST_NAME))
                       {
                           EC.Add(Context.Items[CSAAWeb.Constants.PC_CUST_NAME].ToString());
                       }
                       else
                       {
                           EC.Add(string.Empty);
                       }
                       if (Context.Items.Contains(CSAAWeb.Constants.PC_EC_ACC_NUMBER))
                       {
                           EC.Add(Context.Items[CSAAWeb.Constants.PC_EC_ACC_NUMBER].ToString());
                       }
                       else
                       {
                           EC.Add(string.Empty);
                       }
                       if (Context.Items.Contains(CSAAWeb.Constants.PC_EC_ACCOUNT_TYPE))
                       {
                           EC.Add(Context.Items[CSAAWeb.Constants.PC_EC_ACCOUNT_TYPE].ToString());
                       }
                       else
                       {
                           EC.Add(string.Empty);
                       }
                       if (Context.Items.Contains(CSAAWeb.Constants.PC_EC_ROUTING_NUMBER))
                       {
                           EC.Add(Context.Items[CSAAWeb.Constants.PC_EC_ROUTING_NUMBER].ToString());
                       }
                       else
                       {
                           EC.Add(string.Empty);
                       }
                       if (Context.Items.Contains(CSAAWeb.Constants.PC_EC_BANK_NAME))
                       {
                           EC.Add(Context.Items[CSAAWeb.Constants.PC_EC_BANK_NAME].ToString());
                       }
                       else
                       {
                           EC.Add(string.Empty);
                       }
                       if (Context.Items.Contains(CSAAWeb.Constants.PC_CNFRMNUMBER))
                       {
                           EC.Add(Context.Items[CSAAWeb.Constants.PC_CNFRMNUMBER].ToString());
                       }
                       else
                       {
                           EC.Add(string.Empty);
                       }
                       if (Context.Items.Contains(CSAAWeb.Constants.PC_POLICYNUMBER))
                       {
                           EC.Add(Context.Items[CSAAWeb.Constants.PC_POLICYNUMBER].ToString());
                           if (ViewState[CSAAWeb.Constants.PC_viewstate_PolicyNo] == null)
                           {
                               ViewState[CSAAWeb.Constants.PC_viewstate_PolicyNo] = Context.Items[CSAAWeb.Constants.PC_POLICYNUMBER].ToString();
                           }
                       }
                       else
                       {
                           EC.Add(string.Empty);
                       }
                       if (ViewState[CSAAWeb.Constants.PC_viewstate_PolicyNo] != null)
                       {
                           if (ViewState[CSAAWeb.Constants.PC_viewstate_PolicyNo].ToString().Length > 0)
                           {
                               PolicyNumber = ViewState[CSAAWeb.Constants.PC_viewstate_PolicyNo].ToString();
                           }
                       }
                       if (Context.Items.Contains(CSAAWeb.Constants.PC_EMAIL_ID))
                       {
                           EC.Add(Context.Items[CSAAWeb.Constants.PC_EMAIL_ID].ToString());
                       }
                       else
                       {
                           EC.Add(string.Empty);
                       }
                       UnEnrollECheck.assignECConfirmValues(EC);
                       
                           ViewState["viewstateEC"] = EC;
                       
                   }
               }			                  
               //MAIG - CH3 - BEGIN - Disabled email comms trigger from Payment Tool
               ////////PC Phase II Ch1 - Start - Modified code as a part trigger mail on enrollment successful
               //////string emailid = string.Empty;
               //////if (Context.Items.Contains(CSAAWeb.Constants.PC_EMAIL_ID))
               //////{
               //////    emailid = Context.Items[CSAAWeb.Constants.PC_EMAIL_ID].ToString();                
               //////    SendMail(emailid);
               //////}
               ////////PC Phase II Ch1 - Start - Modified code as a part trigger mail on enrollment successful   
               //MAIG - CH3 - END - Disabled email comms trigger from Payment Tool
           }
                                              
        }
        protected void gridRecurringConfirmation_DataBound(object sender, GridViewRowEventArgs e)
        {

        }

        protected void imgManageEnrollment_onclick(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(CSAAWeb.Constants.PC_ENROLL_REDIRECT_URL);
        }

        protected void imgPrint_Click(object sender, ImageClickEventArgs e)
        {
            List<string> CC = new List<string>();
            List<string> EC = new List<string>();
            
            MSC.Encryption.EncryptQueryString args = new MSC.Encryption.EncryptQueryString();

            //Added for implementing the print functionality
            if (ViewState["viewstateEC"] != null)
            {
                if (ViewState["viewstateEC"].ToString().Length > 0)
                {
                    EC = (List<string>)ViewState["viewstateEC"];
                }
            }
            if (EC!=null&&EC.Count>0)
            {
                Context.Items.Add(CSAAWeb.Constants.PC_PAYMT_TYPE, "EC");
                Context.Items.Add(CSAAWeb.Constants.PRINT_EmailID, EC[7]);
                Context.Items.Add("ConfirmNum", EC[5]);
                Context.Items.Add(CSAAWeb.Constants.PRINT_PolicyNum, EC[6]);
                Context.Items.Add(CSAAWeb.Constants.PRINT_AcntType, EC[2]);
                Context.Items.Add(CSAAWeb.Constants.PC_EC_BANK_NAME, EC[4]);
                Context.Items.Add(CSAAWeb.Constants.PRINT_AccNum, EC[1]);
                Context.Items.Add(CSAAWeb.Constants.PRINT_CustName, EC[0]);
                Context.Items.Add(CSAAWeb.Constants.PRINT_EnrolledDate, lblDate.Text);

                args[CSAAWeb.Constants.PRINT_EmailID] = EC[7];
                args["Response"] = CSAAWeb.Constants.PC_EDIT_RESPONSE;
                args["ConfirmNum"] = EC[5];
                args[CSAAWeb.Constants.PC_PAYMT_TYPE] = "EC";
                args[CSAAWeb.Constants.PRINT_EnrolledDate] = lblDate.Text;
                args[CSAAWeb.Constants.PRINT_PolicyNum] = EC[6];
                args[CSAAWeb.Constants.PC_EC_BANK_NAME] = EC[4];
                args[CSAAWeb.Constants.PRINT_AcntType] = EC[2];
                args[CSAAWeb.Constants.PRINT_AccNum] = EC[1];
                args[CSAAWeb.Constants.PRINT_CustName] = EC[0];

            }
            else
            {
                if (ViewState["viewstateCC"] != null)
                {
                    if (ViewState["viewstateCC"].ToString().Length > 0)
                    {
                        CC = (List<string>)ViewState["viewstateCC"];
                    }
                }
                Context.Items.Add(CSAAWeb.Constants.PC_PAYMT_TYPE, CSAAWeb.Constants.PC_CC);
                Context.Items.Add(CSAAWeb.Constants.PRINT_PolicyNum, CC[5]);
                Context.Items.Add("ConfirmNum", CC[4]);
                Context.Items.Add(CSAAWeb.Constants.PRINT_AcntType, CC[1]);
                Context.Items.Add(CSAAWeb.Constants.PC_EC_BANK_NAME, CC[6]);
                Context.Items.Add(CSAAWeb.Constants.PRINT_AccNum, CC[0]);
                Context.Items.Add(CSAAWeb.Constants.PRINT_CustName, CC[2]);
                Context.Items.Add(CSAAWeb.Constants.PRINT_EnrolledDate, lblDate.Text);
                Context.Items.Add(CSAAWeb.Constants.PRINT_ExpDate, CC[3]);
                Context.Items.Add(CSAAWeb.Constants.PRINT_EmailID, CC[7]);

                args[CSAAWeb.Constants.PRINT_EmailID] = CC[7];
                args["Response"] = CSAAWeb.Constants.PC_EDIT_RESPONSE;
                args[CSAAWeb.Constants.PC_PAYMT_TYPE] = CSAAWeb.Constants.PC_CC;
                args[CSAAWeb.Constants.PRINT_EnrolledDate] = lblDate.Text;
                args[CSAAWeb.Constants.PRINT_PolicyNum] = CC[5];

                args[CSAAWeb.Constants.PRINT_AccNum] = CC[0];
                args[CSAAWeb.Constants.PRINT_CustName] = CC[2];


            }
            if (ViewState["IsEnrolled"] != null)
            {
                if (ViewState["IsEnrolled"].ToString() == CSAAWeb.Constants.PC_POL_ENROLL_STATUS)
                {
                    Context.Items.Add("Response", CSAAWeb.Constants.PC_EDIT_RESPONSE);
                }
            }
            

            //Server.Transfer(CSAAWeb.Constants.PRINT_URL);



            string url = String.Format(CSAAWeb.Constants.PRINT_URL, args.ToString());
            Page.ClientScript.RegisterStartupScript(GetType(), Constants.PC_TC_SCRIPT_KEY, Constants.PC_TC_SCRIPT_ONE + url + Constants.PC_TC_SCRIPT_TWO);
           
        }
        //PC Phase II Ch1 - Start - Modified code as a part to trigger mail on enrollment successful
        /// <summary>
        /// Trigger mail to given mail id on enrollment successful
        /// </summary>
        /// <param name="emailid"></param>
        /// <param name="confirmNo"></param>
        protected void SendMail(string emailid)
        {
            
            try
            {
                if (!string.IsNullOrEmpty(emailid.Trim()))
                {
                    MailMessage msg = CreateMessage(emailid);
                    SmtpClient SmtpServer = new SmtpClient(Config.Setting(CSAAWeb.Constants.PC_SMTPSERVER));
                    SmtpServer.Send(msg);
                }
            }
            catch(Exception ex)
            {               
                Logger.Log(ex.ToString());
                Logger.Log(lblErrorMsg.Text.ToString());
            }


        }
        MailMessage CreateMessage(string emailid)
        {
            MailDefinition md = new MailDefinition();
             string PaymentType = Context.Items[CSAAWeb.Constants.PC_PAYMT_TYPE].ToString();
             string PaymentUpdated = Context.Items[CSAAWeb.Constants.PC_IS_ENROLLED].ToString();            
             if (PaymentType == CSAAWeb.Constants.PC_CC)
             {
                 if (PaymentUpdated == CSAAWeb.Constants.PC_POL_NOTENROLL_STATUS)
                 {
                     md.BodyFileName = CSAAWeb.Constants.PC_SUCCESS_HTML;
                     md.From = Config.Setting("FromAddress");
                     md.Subject = CSAAWeb.Constants.ENROLL_SUCCESS_MAIL;
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
                 else if(PaymentUpdated==CSAAWeb.Constants.PC_POL_ENROLL_STATUS)
                 {
                     md.BodyFileName = CSAAWeb.Constants.PC_SUCCESS_HTML;
                     md.From = Config.Setting("FromAddress");
                     md.Subject = CSAAWeb.Constants.MODIFY_SUCCESS_MAIL;                        
                     
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
                 replacements.Add("<%NAME%>", Context.Items[CSAAWeb.Constants.PC_CUST_NAME].ToString());
                 replacements.Add("<%DATE%>", DateTime.Now.ToString(CSAAWeb.Constants.FULL_DATE_TIME_FORMAT));                            
                 replacements.Add("<%CARDNO%>", Context.Items[CSAAWeb.Constants.PC_CCNumber].ToString());
                 replacements.Add("<%POLICY%>", PolicyNumber.ToString().ToUpper());
                 //CHG0072116 - PC Edit Card Details CH3:START - changed the content and subject for email WRT edit/Enrollment details.
                 if (PaymentUpdated == CSAAWeb.Constants.PC_POL_ENROLL_STATUS)
                 {
                     replacements.Add("<%RESPONSE%>", CSAAWeb.Constants.PC_EDIT_RESPONSE);
                 }
                 else
                 {
                     replacements.Add("<%RESPONSE%>", Context.Items["Response"].ToString());
                 }
                 //CHG0072116 - PC Edit Card Details CH3:END - changed the content and subject for email WRT edit/Enrollment details.
                 //Clearing context
                 Context.Items.Clear();
                 System.Net.Mail.MailMessage fileMsg;
                 fileMsg = md.CreateMailMessage(emailid, replacements, this);
                 return fileMsg;             
             }
             else
             {
                 if (PaymentUpdated == CSAAWeb.Constants.PC_POL_NOTENROLL_STATUS)
                 {
                     md.BodyFileName = "/PaymentToolimages/ECheckSuccess.html";
                     md.From = Config.Setting("FromAddress");
                     md.Subject = CSAAWeb.Constants.ENROLL_SUCCESS_MAIL;
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
                 else if (PaymentUpdated == CSAAWeb.Constants.PC_POL_ENROLL_STATUS)
                 {
                     md.BodyFileName = "/PaymentToolimages/ECheckSuccess.html";
                     md.From = Config.Setting("FromAddress");
                     md.Subject = CSAAWeb.Constants.MODIFY_SUCCESS_MAIL;
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
                
                 
                 ListDictionary replacements = new ListDictionary();
                 replacements.Add("<%ACCOUNTHOLDNAME%>", Context.Items[CSAAWeb.Constants.PC_CUST_NAME].ToString());
                 //PC Phase II 4/20 - Modified the date time format if echeck enrollment success sceanrio
                 replacements.Add("<%DATE%>", DateTime.Now.ToString(CSAAWeb.Constants.FULL_DATE_TIME_FORMAT));
                 //replacements.Add("<%RECEIPT%>", Context.Items[CSAAWeb.Constants.PC_CNFRMNUMBER].ToString());
                 replacements.Add("<%ACCOUNTTYPE%>", Context.Items[CSAAWeb.Constants.PC_EC_ACCOUNT_TYPE].ToString());
                 replacements.Add("<%BANKNAME%>", Context.Items[CSAAWeb.Constants.PC_EC_BANK_NAME].ToString());
                 replacements.Add("<%ACCOUNTNUMBER%>", Context.Items[CSAAWeb.Constants.PC_EC_ACC_NUMBER].ToString());                 
                 replacements.Add("<%POLICYNO%>", PolicyNumber.ToString().ToUpper());
                 //CHG0072116 - PC Edit Card Details CH4: START - changed the content and subject for email WRT edit/Enrollment details.
                 if (PaymentUpdated == CSAAWeb.Constants.PC_POL_ENROLL_STATUS)
                 {
                     replacements.Add("<%RESPONSE%>", CSAAWeb.Constants.PC_EDIT_RESPONSE);
                 }
                 else
                 {
                     replacements.Add("<%RESPONSE%>", Context.Items["Response"].ToString());
                 }
                 //CHG0072116 - PC Edit Card Details CH4: END - changed the content and subject for email WRT edit/Enrollment details.
                 System.Net.Mail.MailMessage fileMsg;
                 fileMsg = md.CreateMailMessage(emailid, replacements, this);
                 return fileMsg;             
             }                                     
              
            
        }
        //PC Phase II Ch1 - End - Modified code as a part trigger mail on enrollment successful
        

	}
}
