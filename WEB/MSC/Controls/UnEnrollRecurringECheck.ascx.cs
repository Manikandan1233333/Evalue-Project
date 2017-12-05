/*
 * HISTORY
 * PC Phase II changes - Added the page as part of Enrollment changes.Echeck - un Enroll Page
 * Created by: Cognizant on 2/20/2013
 * Decsription: This is a new file added as part of Payment Central Phase II.
 * MAIG - CH1 - Added condition to check if the entered policy is valid or not and based on that display the payment method
 * MAIG - CH2 - Commented the PUP Poicy Prefix as part of MAIG
 * MAIG - CH3 - Added condition to check if the entered policy is valid or not and based on that display the controls
 * MAIG - CH4 - Added condition to check if the entered policy is valid or not and based on that display the controls
 * CHG0109406 - CH1 - Change to update ECHECK to ACH - 01202015
 * CHG0109406 - CH1 - Change to update ECHECK to ACH - 01202015
*/
namespace MSC.Controls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using CSAAWeb;
    using CSAAWeb.AppLogger;
    using CSAAWeb.WebControls;
    using OrderClassesII;
    using RecordEnrollment;
    using System.Collections.Generic;
    using System.Web.UI;
using System.Collections.Specialized;
using System.Net.Mail;

    /// <summary>
    ///		Code for ECheck Controls.
    /// </summary>
    /// 

    public partial class UnEnrollRecurringECheck : ValidatingUserControl
    {
        #region WebControls
        protected CSAAWeb.WebControls.Validator lblbankid;
        protected System.Web.UI.WebControls.DropDownList _AccountType;
        protected CSAAWeb.WebControls.Validator lblAccounttype;
        protected System.Web.UI.WebControls.TextBox _Accountno;
        protected System.Web.UI.WebControls.TextBox _Bankid;
        protected CSAAWeb.WebControls.Validator lblAccountno;
        #endregion

        #region Properties
        #endregion

        public UnEnrollRecurringECheck()
            : base()
        {
            this.Init += new System.EventHandler(this.InitializeLBs);
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.Load += new System.EventHandler(this.Page_Load);           
        }
        #endregion

        private void Initialize()
        {
        }

        ///<summary>Called from init event.</summary>
        private void InitializeLBs(object sender, System.EventArgs e)
        {

        }
        #region assign view details
        public void assignValues()
        {
            //CHG0109406 - CH1 - BEGIN - Change to update ECHECK to ACH - 01202015
            lblPaymentMethod.Text = "ACH";
            //CHG0109406 - CH1 - END - Change to update ECHECK to ACH - 01202015

            lblAccountHolderName.Text = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ECAccountHolderName;
            lblAccountNumber.Text = CSAAWeb.Constants.PC_MASK_TXT + ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ECAccountNo + "(" + ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ECaacType + ")";            
            lblRoutingNumber.Text = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ECRoutingNo;
            lblBankName.Text = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ECBankName;
            lblEmailID.Visible = false;
            lblEmailAddress.Visible = false;
			//MAIG - CH1 - START - Added condition to check if the entered policy is valid or not and based on that display the payment method
            string invalidunenrollflow = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow;

            if (!string.IsNullOrEmpty(invalidunenrollflow) && invalidunenrollflow.Equals("True"))
                lnkChangePaymentMethod.Visible = false;
            else
                lnkChangePaymentMethod.Visible = true; 
			//MAIG - CH1 - END - Added condition to check if the entered policy is valid or not and based on that display the payment method
        }
        #endregion
        #region assign confirmation screen values
        public void assignECConfirmValues(List<string> ECValues)
        {
            try
            {
                imgunenroll.Visible = false;
                Header.Visible = false;
                lnkChangePaymentMethod.Visible = false;

                //CHG0109406 - CH2 - BEGIN - Change to update ECHECK to ACH - 01202015
                lblPaymentMethod.Text = "ACH";
                //CHG0109406 - CH2 - END - Change to update ECHECK to ACH - 01202015

                lblAccountHolderName.Text = ECValues[0];
                lblAccountNumber.Text = ECValues[1] + " (" + ECValues[2] +")";
                lblRoutingNumber.Text = ECValues[3];
                lblBankName.Text = ECValues[4];
                if (ECValues[7] != null)
                    lblEmailAddress.Text = ECValues[7];
                else
                    lblEmailAddress.Text = "";
            }
            catch (Exception ex)
            {
                Logger.Log(ex.ToString());
            }
        }
        #endregion
        #region enroll button click
        protected void imgunenroll_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            mailalert.FindControl("content").Visible = true;
            HiddenField hdnfldConfirmNum = (HiddenField)mailalert.FindControl("hdnConfirmNum");
            HiddenField hdnfldPolicyNumber = (HiddenField)mailalert.FindControl("hdnPolicyNumber");
            HiddenField hdnfldPaymentType = (HiddenField)mailalert.FindControl("hdnPaymentType");
            HiddenField hdnfldECCustomerName = (HiddenField)mailalert.FindControl("hdnECCustomerName");
            HiddenField hdnfldECAccType = (HiddenField)mailalert.FindControl("hdnECAccType");
            HiddenField hdnfldECBankName = (HiddenField)mailalert.FindControl("hdnECBankName");
            HiddenField hdnfldECAccNumber = (HiddenField)mailalert.FindControl("hdnECAccNumber");

            string PolicyNumber = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).PolicyNumber;
            string ProductType = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ProductType_PC ;

            if (hdnfldPolicyNumber != null)
            {
				//MAIG - CH2 - START - Commented the PUP Poicy Prefix as part of MAIG
                ////if (ProductType == "PU")
                ////{
                ////    hdnfldPolicyNumber.Value = CSAAWeb.Constants.PC_PUP + PolicyNumber;
                ////}
                ////else
                ////{
                    hdnfldPolicyNumber.Value = PolicyNumber;
                ////}
				//MAIG - CH2 - END - Commented the PUP Poicy Prefix as part of MAIG
            }
            if (hdnfldPaymentType != null)
            {
                hdnfldPaymentType.Value = lblPaymentMethod.Text;
            }
            if (hdnfldECCustomerName != null)
            {
                hdnfldECCustomerName.Value = lblAccountHolderName.Text;
            }
            if (hdnfldECAccType != null)
            {
                hdnfldECAccType.Value = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ECaacType;
            }
            if (hdnfldECBankName != null)
            {
                hdnfldECBankName.Value = lblBankName.Text;
            }
            if (hdnfldECAccNumber != null)
            {
                hdnfldECAccNumber.Value = lblAccountNumber.Text;
            }

        }
        #endregion
        #region change payment method
        protected void lnkChangePaymentMethod_Click(object sender, EventArgs e)
        {
			//MAIG - CH3 - START - Added condition to check if the entered policy is valid or not and based on that display the controls
             string invalidUnEnrollFlow = string.Empty;

            if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow) != null)
                invalidUnEnrollFlow = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow;

            if (!(invalidUnEnrollFlow.Equals("True")))
            {
			//MAIG - CH3 - END - Added condition to check if the entered policy is valid or not and based on that display the controls
                if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Modifyenrollment[1].Equals(CSAAWeb.Constants.PC_YES_NOTATION))
                {
                    UserControl UC_PaymentMethod = (UserControl)this.NamingContainer.FindControl("PaymentMethod");
                    if (UC_PaymentMethod != null)
                    {
                        UC_PaymentMethod.Visible = true;
                    }
                    UserControl UC_UnEnrollEC = (UserControl)this.NamingContainer.FindControl("UnEnrollECheck");
                    if (UC_UnEnrollEC != null)
                    {
                        UC_UnEnrollEC.Visible = false;
                    }
                    PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl("policySearchDetails");
                    if (PH_PolicyDetail != null)
                    {
                        PH_PolicyDetail.Visible = true;
                    }
                    DropDownList dropdown_PaymentType = (DropDownList)UC_PaymentMethod.FindControl("_PaymentType");
                    if (dropdown_PaymentType != null)
                    {
                        dropdown_PaymentType.SelectedIndex = 0;
                    }
                }
                else
                {
                    string errMsg = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Modifyenrollment[0];
                    Logger.Log(errMsg);
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = errMsg;
                }
			//MAIG - CH4 - START - Added condition to check if the entered policy is valid or not and based on that display the controls
            }
            else
            {
                UserControl UC_PaymentMethod = (UserControl)this.NamingContainer.FindControl("PaymentMethod");
                if (UC_PaymentMethod != null)
                {
                    UC_PaymentMethod.Visible = true;
                }
                UserControl UC_UnEnrollEC = (UserControl)this.NamingContainer.FindControl("UnEnrollECheck");
                if (UC_UnEnrollEC != null)
                {
                    UC_UnEnrollEC.Visible = false;
                }
                ////PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl("policySearchDetails");
                ////if (PH_PolicyDetail != null)
                ////{
                ////    PH_PolicyDetail.Visible = true;
                ////}
                DropDownList dropdown_PaymentType = (DropDownList)UC_PaymentMethod.FindControl("_PaymentType");
                if (dropdown_PaymentType != null)
                {
                    dropdown_PaymentType.SelectedIndex = 0;
                }
            }
			//MAIG - CH4 - END - Added condition to check if the entered policy is valid or not and based on that display the controls
        }
        #endregion


    }
}
