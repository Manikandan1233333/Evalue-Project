/*
 * HISTORY
 * PC Phase II changes - Added the page as part of Enrollment changes.CC - un Enroll Page
 * Created by: Cognizant on 2/20/2013
 * Decsription: This is a new file added as part of Payment Central Phase II.
 * CHG0072116 - PC Edit Card Details CH1 - hide the "Edit Card Details" if page gets navigated to enroll confirmation page.
 * CHG0072116 - PC Edit Card Details CH2 - Event to show/hide the required usercontrol when the linkbutton "Edit Card Details" is clicked.
 * MAIG - CH1 - Declared two properties to store the AgencyID and RepID
 * MAIG - CH2 - Added condition to check if the entered policy is valid or not. 
 * MAIG - CH3 - Added logic for invalid policy number
 * MAIG - CH4 - Added condition to check if the entered poicy number is valid and display the link button based on that
 * MAIG - CH5 - Commented the PUP Poicy Prefix as part of MAIG
 * MAIG - CH6 - Added code to check if the entered policy is valid
 * MAIG - CH7 - Added condition to make the policy grid visible only on valid policy
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
    using System.Net.Mail;
    using System.Collections.Specialized;

    /// <summary>
    ///		Code for ECheck Controls.
    /// </summary>
    /// 

    public partial class UnEnrollRecurringCC : ValidatingUserControl
    {
        public bool isPaymentChanged = false;
        #region WebControls
        protected CSAAWeb.WebControls.Validator lblbankid;
        protected System.Web.UI.WebControls.DropDownList _AccountType;
        protected CSAAWeb.WebControls.Validator lblAccounttype;
        protected System.Web.UI.WebControls.TextBox _Accountno;
        protected System.Web.UI.WebControls.TextBox _Bankid;
        protected CSAAWeb.WebControls.Validator lblAccountno;
        #endregion

        #region Properties
		//MAIG - CH1 - START - Declared two properties to store the AgencyID and RepID
        public string AgencyID { get; set; }
        public long RepID { get; set; }
		//MAIG - CH1 - END - Declared two properties to store the AgencyID and RepID
        #endregion

        public UnEnrollRecurringCC()
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
        /// <summary>
        ///		The below method is processed in change payment method click
        /// </summary>
        /// 
        protected void lnkChangePaymentMethod_Click(object sender, EventArgs e)
        {
			//MAIG - CH2 - START - Added condition to check if the entered policy is valid or not. 
            string invalidUnEnrollFlow = string.Empty;

            if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow) != null)
                invalidUnEnrollFlow = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow;

            if (!(invalidUnEnrollFlow.Equals("True")))
			//MAIG - CH2 - END - Added condition to check if the entered policy is valid or not
            {
                if (((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Modifyenrollment[1].Equals(CSAAWeb.Constants.PC_YES_NOTATION))
                {
                    UserControl UC_PaymentMethod = (UserControl)this.NamingContainer.FindControl("PaymentMethod");
                    if (UC_PaymentMethod != null)
                    {
                        UC_PaymentMethod.Visible = true;
                    }
                    DropDownList dropdown_PaymentType = (DropDownList)UC_PaymentMethod.FindControl("_PaymentType");
                    if (dropdown_PaymentType != null)
                    {
                        dropdown_PaymentType.SelectedIndex = 0;
                    }
                    UserControl UC_UnEnrollCC = (UserControl)this.NamingContainer.FindControl("UnEnrollCC");
                    if (UC_UnEnrollCC != null)
                    {
                        UC_UnEnrollCC.Visible = false;
                    }
                    PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl("policySearchDetails");
                    if (PH_PolicyDetail != null)
                    {
                        PH_PolicyDetail.Visible = true;
                    }
                    Label payRestrictMessage = (Label)this.NamingContainer.FindControl("lblPaymentRestrict");
                    if (!string.IsNullOrEmpty(payRestrictMessage.Text))
                    {
                        payRestrictMessage.Visible = true;
                    }
                    isPaymentChanged = true;
                    List<string> lstPaymnetFlag = new List<string>();
                    lstPaymnetFlag.Add(isPaymentChanged.ToString());
                }
                else
                {
                    string errMsg = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ValidationService_Modifyenrollment[0];
                    Logger.Log(errMsg);
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = errMsg;
                }
            }
			//MAIG - CH3 - START - Added logic for invalid policy number
            else
            {
                UserControl UC_PaymentMethod = (UserControl)this.NamingContainer.FindControl("PaymentMethod");
                if (UC_PaymentMethod != null)
                {
                    UC_PaymentMethod.Visible = true;
                }
                DropDownList dropdown_PaymentType = (DropDownList)UC_PaymentMethod.FindControl("_PaymentType");
                if (dropdown_PaymentType != null)
                {
                    dropdown_PaymentType.SelectedIndex = 0;
                }
                UserControl UC_UnEnrollCC = (UserControl)this.NamingContainer.FindControl("UnEnrollCC");
                if (UC_UnEnrollCC != null)
                {
                    UC_UnEnrollCC.Visible = false;
                }
                //////PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl("policySearchDetails");
                //////if (PH_PolicyDetail != null)
                //////{
                //////    PH_PolicyDetail.Visible = true;
                //////}
                Label payRestrictMessage = (Label)this.NamingContainer.FindControl("lblPaymentRestrict");
                if (!string.IsNullOrEmpty(payRestrictMessage.Text))
                {
                    payRestrictMessage.Visible = true;
                }
                isPaymentChanged = true;
                List<string> lstPaymnetFlag = new List<string>();
                lstPaymnetFlag.Add(isPaymentChanged.ToString());
            }
        }
					//MAIG - CH3 - END - Added logic for invalid policy number
        #region Assign values for view enrollment
        public void assignValues()
        {
            lblPaymentMethod.Text = "Credit Card";
            lblCreditCardType.Text = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._CCType;
            lblCardNumberLast4Digit.Text = CSAAWeb.Constants.PC_MASK_TXT + ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._CCNumber;
            lblNameOnCard.Text = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._CCName;
            lblExpirationDate.Text = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._CCExpDate;
            lblEmailID.Visible = false;
            lblEmailAddress.Visible = false;
			//MAIG - CH4 - START - Added condition to check if the entered poicy number is valid and display the link button based on that
            string invalidunenrollflow = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow;

                if (!string.IsNullOrEmpty(invalidunenrollflow) && invalidunenrollflow.Equals("True"))
                    tblLnkButtons.Visible = false;
                else
                    tblLnkButtons.Visible = true;
			//MAIG - CH4 - END - Added condition to check if the entered poicy number is valid and display the link button based on that                
        }
        #endregion
        #region Assign values for confirmation screen while enroll process
        public void assignConfirmationValues(List<string> CCValues)
        {
            imgunenrollCC.Visible = false;
            Header.Visible = false;
            lnkChangePaymentMethod.Visible = false;
            //CHG0072116 - PC Edit Card Details CH1:START - hide the "Edit Card Details" if page gets navigated to enroll confirmation page.
            this.tblLnkButtons.Visible = false;
            lnkbtnEditCCDetails.Visible = false;
            //CHG0072116 - PC Edit Card Details CH1:END - hide the "Edit Card Details" if page gets navigated to enroll confirmation page.
            lblPaymentMethod.Text = "Credit Card";
            lblCardNumberLast4Digit.Text = CCValues[0];
            lblCreditCardType.Text = CCValues[1];
            lblNameOnCard.Text = CCValues[2];
            lblExpirationDate.Text = CCValues[3];
            if (CCValues[7] != null)
                lblEmailAddress.Text = !string.IsNullOrEmpty(CCValues[7])?CCValues[7]:string.Empty;
            
        }
        #endregion
        #region unenrollclick
        protected void imgunenrollCC_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {

            mailalert.FindControl("content").Visible = true;
            HiddenField hdnfldConfirmNum = (HiddenField)mailalert.FindControl("hdnConfirmNum");
            HiddenField hdnfldCCCustomerName = (HiddenField)mailalert.FindControl("hdnCCCustomerName");
            HiddenField hdnfldCCNumber = (HiddenField)mailalert.FindControl("hdnCCNumber");
            HiddenField hdnfldPolicyNumber = (HiddenField)mailalert.FindControl("hdnPolicyNumber");
            HiddenField hdnfldPaymentType = (HiddenField)mailalert.FindControl("hdnPaymentType");
            if (hdnfldCCCustomerName != null)
            {
                hdnfldCCCustomerName.Value = lblNameOnCard.Text;
            }
            if (hdnfldCCNumber != null)
            {
                hdnfldCCNumber.Value = lblCardNumberLast4Digit.Text;
            }
            string PolicyNumber = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page)).PolicyNumber;
            string ProductType = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._ProductType_PC;
            //hdnfldConfirmNum.Value = response[1].ToString();
			//MAIG - CH5 - START - Commented the PUP Poicy Prefix as part of MAIG
            ////if (ProductType == "PU")
            ////{
            ////    hdnfldPolicyNumber.Value = CSAAWeb.Constants.PC_PUP + PolicyNumber;
            ////}
            ////else
            ////{
                hdnfldPolicyNumber.Value = PolicyNumber;
            ////}
			//MAIG - CH5 - END - Commented the PUP Poicy Prefix as part of MAIG
            hdnfldPaymentType.Value = lblPaymentMethod.Text;

        }
        #endregion
        //CHG0072116 - PC Edit Card Details CH2:START - Event to show/hide the required usercontrol when the linkbutton "Edit Card Details" is clicked.
        protected void lnkbtnEditCCDetails_Click(object sender, EventArgs e)
        {
			//MAIG - CH6 - START - Added code to check if the entered policy is valid
            string invalidUnEnrollFlow = string.Empty;

            if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow) != null)
                invalidUnEnrollFlow = ((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow;
                       
			//MAIG - CH6 - END - Added code to check if the entered policy is valid
            UserControl UC_UnEnrollCC = (UserControl)this.NamingContainer.FindControl("UnEnrollCC");
            if (UC_UnEnrollCC != null)
            {
                UC_UnEnrollCC.Visible = false;
            }
			//MAIG - CH7 - START - Added condition to make the policy grid visible only on valid policy
            if (!(invalidUnEnrollFlow.Equals("True")))
            {
                PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl("policySearchDetails");
                if (PH_PolicyDetail != null)
                {
                    PH_PolicyDetail.Visible = true;
                }
            }
			//MAIG - CH7 - END - Added condition to make the policy grid visible only on valid policy
            Label payRestrictMessage = (Label)this.NamingContainer.FindControl("lblPaymentRestrict");
            if (!string.IsNullOrEmpty(payRestrictMessage.Text))
            {
                payRestrictMessage.Visible = true;
            }
            UserControl UC_UpdateCardDetails = (UserControl)this.NamingContainer.FindControl("EditCC");
            if (UC_UpdateCardDetails != null)
            {
                UC_UpdateCardDetails.Visible = true;
            }
        }
        //CHG0072116 - PC Edit Card Details CH2:END - Event to show/hide the required usercontrol when the linkbutton "Edit Card Details" is clicked.





    }
}
