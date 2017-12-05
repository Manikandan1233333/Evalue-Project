/*
 * HISTORY
 * PC Phase II changes - Added the page as part of Enrollment changes.Print page for enrollment/modify enrollment
 * Created by: Cognizant on 2/18/2013
 * Decsription: This is a new file added as part of Payment Central Phase II.
 * //CHG0072116 -Print Page Text change - Added the below code to display the message accordingly in print page for enrollment and modify process
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing; 
using System.Drawing.Printing;

namespace MSC.Forms
{
    public partial class PrintEnrollmentConfirmationCC : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            #region OLDCODE
            /*

            if (Context.Items[CSAAWeb.Constants.PRINT_EmailID].ToString()==string.Empty)
            {
                EmailHeader.Visible = false;
            }
            //CHG0072116 -Print Page Text change -START Added the below code to display the message accordingly in print page for enrollment and modify process
            if (Context.Items.Contains("Response"))
            {
                lblEnrollMsg.Text = Context.Items["Response"].ToString();
            }
            else
            {
                lblEnrollMsg.Text = CSAAWeb.Constants.PC_THANKU_ENROLL;
            }
            //CHG0072116 -Print Page Text change -END Added the below code to display the message accordingly in print page for enrollment and modify process
            if (Context.Items.Contains(CSAAWeb.Constants.PC_PAYMT_TYPE))
            {
            if (Context.Items[CSAAWeb.Constants.PC_PAYMT_TYPE].ToString()==CSAAWeb.Constants.PC_CC)
            {
                //Added for CC
                ccdetails.Visible = true;
                ecdetails.Visible = false;
                if (Context.Items.Contains(CSAAWeb.Constants.PRINT_EnrolledDate))
                {
                    lbl_ECEnrolledDate.Text = Context.Items[CSAAWeb.Constants.PRINT_EnrolledDate].ToString();
                }
                if (Context.Items.Contains(CSAAWeb.Constants.PRINT_PolicyNum))
                {
                    lbl_ECPolicyNumber.Text = Context.Items[CSAAWeb.Constants.PRINT_PolicyNum].ToString().ToUpper();
                }
                if (Context.Items.Contains(CSAAWeb.Constants.PRINT_AccNum))
                {
                    lbl_AccNumber.Text = Context.Items[CSAAWeb.Constants.PRINT_AccNum].ToString();
                }
                if (Context.Items.Contains(CSAAWeb.Constants.PRINT_CustName))
                {
                    lbl_CustName.Text = Context.Items[CSAAWeb.Constants.PRINT_CustName].ToString();
                }
                if (Context.Items.Contains(CSAAWeb.Constants.PRINT_EmailID))
                {
                    lblEmailId.Text = Context.Items[CSAAWeb.Constants.PRINT_EmailID].ToString();
                }

            }
            else
            {
                //Added for EC
                ccdetails.Visible = false;
                ecdetails.Visible = true;
                if (Context.Items.Contains(CSAAWeb.Constants.PRINT_EnrolledDate))
                {
                    lbl_EcDate.Text = Context.Items[CSAAWeb.Constants.PRINT_EnrolledDate].ToString();
                }
                if (Context.Items.Contains(CSAAWeb.Constants.PRINT_PolicyNum))
                {
                    lbl_ECPolicyNumber.Text = Context.Items[CSAAWeb.Constants.PRINT_PolicyNum].ToString().ToUpper();
                }
                if (Context.Items.Contains(CSAAWeb.Constants.PC_EC_BANK_NAME))
                {
                    lbl_EcBankName.Text = Context.Items[CSAAWeb.Constants.PC_EC_BANK_NAME].ToString();
                }
                if (Context.Items.Contains(CSAAWeb.Constants.PRINT_AcntType))
                {
                    lbl_EcAcntType.Text = Context.Items[CSAAWeb.Constants.PRINT_AcntType].ToString();
                }
                if (Context.Items.Contains(CSAAWeb.Constants.PRINT_AccNum))
                {
                    lbl_EcAccNum.Text = Context.Items[CSAAWeb.Constants.PRINT_AccNum].ToString();
                }
                if (Context.Items.Contains(CSAAWeb.Constants.PRINT_CustName))
                {
                    lbl_EcName.Text = Context.Items[CSAAWeb.Constants.PRINT_CustName].ToString();
                }
                if (Context.Items.Contains(CSAAWeb.Constants.PRINT_EmailID))
                {
                    lblEmailId.Text = Context.Items[CSAAWeb.Constants.PRINT_EmailID].ToString();
                }
            }
            }
              */

            #endregion

            MSC.Encryption.EncryptQueryString args = new MSC.Encryption.EncryptQueryString(Request.QueryString["args"]);

            if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PRINT_PolicyNum].ToString()))
            {

                lbl_ECPolicyNumber.Text = args[CSAAWeb.Constants.PRINT_PolicyNum].ToString().ToUpper();

                if (args[CSAAWeb.Constants.PRINT_EmailID].ToString() == string.Empty)
                {
                    EmailHeader.Visible = false;
                }

                if (!string.IsNullOrEmpty(args["Response"].ToString()))
                {
                    lblEnrollMsg.Text = args["Response"];
                }
                else
                {
                    lblEnrollMsg.Text = CSAAWeb.Constants.PC_THANKU_ENROLL;
                }

                if (args[CSAAWeb.Constants.PC_PAYMT_TYPE].ToString() == CSAAWeb.Constants.PC_CC)
                {
                    //Added for CC
                    ccdetails.Visible = true;
                    ecdetails.Visible = false;

                    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PRINT_EnrolledDate].ToString()))
                    {
                        lbl_ECEnrolledDate.Text = args[CSAAWeb.Constants.PRINT_EnrolledDate].ToString();
                    }
                    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PRINT_AccNum].ToString()))
                    {
                        lbl_AccNumber.Text = args[CSAAWeb.Constants.PRINT_AccNum].ToString();
                    }
                    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PRINT_CustName].ToString()))
                    {
                        lbl_CustName.Text = args[CSAAWeb.Constants.PRINT_CustName].ToString();
                    }
                    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PRINT_EmailID].ToString()))
                    {
                        lblEmailId.Text = args[CSAAWeb.Constants.PRINT_EmailID].ToString();
                    }

                }

                else
                {
                    //Added for EC
                    ccdetails.Visible = false;
                    ecdetails.Visible = true;
                    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PRINT_EnrolledDate].ToString()))
                    {
                        lbl_EcDate.Text = args[CSAAWeb.Constants.PRINT_EnrolledDate].ToString();
                    }
                    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PC_EC_BANK_NAME].ToString()))
                    {
                        lbl_EcBankName.Text = args[CSAAWeb.Constants.PC_EC_BANK_NAME].ToString();
                    }
                    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PRINT_AcntType].ToString()))
                    {
                        lbl_EcAcntType.Text = args[CSAAWeb.Constants.PRINT_AcntType].ToString();
                    }
                    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PRINT_AccNum].ToString()))
                    {
                        lbl_EcAccNum.Text = args[CSAAWeb.Constants.PRINT_AccNum].ToString();
                    }
                    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PRINT_CustName].ToString()))
                    {
                        lbl_EcName.Text = args[CSAAWeb.Constants.PRINT_CustName].ToString();
                    }
                    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PRINT_EmailID].ToString()))
                    {
                        lblEmailId.Text = args[CSAAWeb.Constants.PRINT_EmailID].ToString();
                    }
                }
            }
        ScriptManager.RegisterClientScriptBlock(this.Page, typeof(string), "print", "window.print();", true);

                   
        }       
      
    }
}

     
