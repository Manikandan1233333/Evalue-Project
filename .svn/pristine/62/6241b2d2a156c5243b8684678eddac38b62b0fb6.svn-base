/*	REVISION HISTORY:
 *	MODIFIED BY COGNIZANT
 *	07/19/2005 - For changing the web.config entries pointing to database. This changes 
 *				 are made as part of CSR #3937 implementation.
 *	Changes Done:
 *	CSR#3937.Ch1 : Include the namespace for accessing the StringDictionary object
 *	CSR#3937.Ch2 : Changed the code for getting constant value from Application object (web.config changes)
 *	03/16/2011 PT_echeck Ch1: Added the condition to enable the echeck control for the echeck payment type by cognizant.
 *	03/16/2011 PT_echeck Ch2:Start Added the condition to  get the account type selected item from echeck control for echeck payment type by cognizant.
 *	67811A0  - PCI Remediation for Payment systems CH1:added for displaying the authorisation text for CC type transaction
 *	67811A0  - PCI Remediation for Payment systems CH2:added for displaying the authorisation text for ECheck type transaction
 *	67811A0  - PCI Remediation for Payment systems CH3:added for displaying the authorisation text for cash/check type of transaction
 *	67811A0  - PCI Remediation for Payment systems CH4:Continue button should be Enabled only when the checkbox is checked.
 *	67811A0  - PCI Remediation for Payment systems CH5:Add tooltip to Continue button.
 *	67811A0 - PCI Remediation for Payment systems CH6: Added to clear the cache of the page to prevent the page loading on back button hit after logout
 *	67811A0  - PCI Remediation for Payment systems CH6:Added the condition to add contents to context for clearing the credit card and expiry contents in billing screen as a part of VA sacn defect by cognizant on 01/05/2011.
 *	PCI - Post Production Fix start:CH1 Added the code to add the variable to context for the CC and e-check payment type. by cognizant on 02/22/2012.
 *  MAIG - CH92 - Added the code to save and pass the Company Code and Source System in Context variable
 *  CHG0113938 - Display Policy Status information in Billing summary.
 *  CHG0115410 - CH1 - Added try catch block & Logged the exception to the log file
 *  CHG0116140 - Payment Restricition - Set Payment Restriction PayFlag to Context & ViewState
 */
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CSAAWeb;
using MSC.Controls;

using InsuranceClasses;

//CSR#3937.Ch1 : START - Include the namespace for accessing the StringDictionary object
using System.Collections.Specialized;
using CSAAWeb.AppLogger;
//CSR#3937.Ch1 : END - Include the namespace for accessing the StringDictionary object

namespace MSC.Forms
{
    /// <summary>
    /// Page Created by Cognizant on 11/08/2004
    /// Payment Confirmation page before the Product is Processed.
    /// </summary>
    public partial class PaymentConfirmation : SiteTemplate
    {	///<summary>The Page Validator</summary>
        protected MSC.Controls.PageValidator PageValidator1;
        ///<summary>The Url to navigate on Back button click (from web.config).</summary>
        protected static string _OnBackUrl;
        ///<summary>The pages nav buttons.</summary>
        protected MSC.Controls.Buttons Buttons1;
        ///<summary>The Url to navigate on Continue button click (from web.config).</summary>
        protected static string _OnContinueUrl = string.Empty;
        ///<summary>Store the PaymentType and CardType(which is not available in web.config) in  label)</summary>
        protected System.Web.UI.WebControls.Label lblPaymentDesc;


        protected void Page_Load(object sender, System.EventArgs e)
        {
            //CHG0115410 - BEGIN - CH1 - Added try catch block & Logged the exception to the log file
            try
            {

                //67811A0 - PCI Remediation for Payment systems CH6 START: Added to clear the cache of the page to prevent the page loading on back button hit after logout
                //Response.Cache.SetCacheability(HttpCacheability.NoCache);
                //Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
                //Response.Cache.SetNoStore();
                //67811A0 - PCI Remediation for Payment systems CH6 END: Added to clear the cache of the page to prevent the page loading on back button hit after logout
                //67811A0  - PCI Remediation for Payment systems CH4:START - Continue button should be Enabled only when the checkbox is checked.
                Control Button_Control = this.FindControl("Buttons1");
                ImageButton continue_click = (ImageButton)Button_Control.FindControl("ContinueButton");
                continue_click.Enabled = false;
                //67811A0  - PCI Remediation for Payment systems CH4:END - Continue button should be Enabled only when the checkbox is checked.

                //67811A0  - PCI Remediation for Payment systems CH5:START -Add tooltip to Continue button.
                continue_click.ToolTip = Constants.PCI_CONFIRMATION_CHECK_TOOLTIP;
                //67811A0  - PCI Remediation for Payment systems CH5:END -Add tooltip to Continue button.

                if (!IsPostBack)
                {
                    if (Context.Items["billplan"] != null)
                    {
                        HiddenBillPLan.Text = Context.Items["billplan"].ToString();
                    }
                    if (Context.Items["PaymentPlan"] != null)
                    {
                        HiddenPaymentPlan.Text = Context.Items["PaymentPlan"].ToString();
                    }
                    if (Context.Items["autoPay"] != null)
                    {
                        hdnAutoPay.Text = Context.Items["autoPay"].ToString();

                    }
                    // Details stored in HidddenAmountSelected
                    if (Context.Items["AmountSelected"] != null)
                    {
                        HidddenAmountSelected.Text = Context.Items["AmountSelected"].ToString();
                    }
                    // Details stored in HiddenDueDetails                      
                    if (Context.Items["DueDetails"] != null)
                    {
                        HiddenDueDetails.Text = Context.Items["DueDetails"].ToString();
                    }

                    //MAIG - CH1 - BEGIN - Added the code to save and pass the Company Code,EmailZip, NameSearchData,DuplicatePolicy and Source System in Context variable
                    if (Context.Items["StoreFields"] != null)
                    {
                        hdnEmailZip.Text = Context.Items["StoreFields"].ToString();
                    }
                    if (Context.Items["SourceCompanyCode"] != null)
                    {
                        hdnSourceCompanyCode.Text = Context.Items["SourceCompanyCode"].ToString();
                    }
                    if (Context.Items["RpbsBillingDetails"] != null)
                    {
                        hdnRpbsBillingDetails.Text = Context.Items["RpbsBillingDetails"].ToString();
                    }
                    if (Context.Items["NameSearchData"] != null)
                    {
                        hdnNameSearchData.Text = Context.Items["NameSearchData"].ToString();
                    }
                    if (Context.Items["DuplicatePolicyData"] != null)
                    {
                        hdnDuplicatePolicyData.Text = Context.Items["DuplicatePolicyData"].ToString();
                    }
                    //CHG0113938 - BEGIN - Add Policy Status Description to ViewState.
                    if (Context.Items[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION] != null && ViewState[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION] == null)
                    {
                        ViewState.Add(CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION, Context.Items[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION].ToString());
                    }
                    //CHG0113938 - END - Add Policy Status Description to ViewState.
                    //CHG0116140 - Payment Restricition - BEGIN - Set the PaymentRestriction to Viewstate variable from Context Variable.
                    if (Context.Items["PaymentRestriction"] != null && ViewState["PaymentRestriction"] == null)
                    {
                        ViewState.Add("PaymentRestriction", Context.Items["PaymentRestriction"].ToString());
                    }
                    //CHG0116140 - Payment Restricition - END - Set the PaymentRestriction to Viewstate variable from Context Variable.
                }
                else
                {
                    //CHG0116140 - Payment Restricition - BEGIN - Set the PaymentRestriction to Context Variable from Viewstate variable.
                    Context.Items.Add("PaymentRestriction", Convert.ToString(ViewState["PaymentRestriction"]));
                    //CHG0116140 - Payment Restricition - END - Set the PaymentRestriction to Context Variable from Viewstate variable.

                    //CHG0113938 - BEGIN - Add Policy Status Description to Context.
                    Context.Items.Add(CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION, Convert.ToString(ViewState[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION]));
                    //CHG0113938 - END - Add Policy Status Description to Context.

                    Context.Items.Add("StoreFields", hdnEmailZip.Text);
                    Context.Items.Add("NameSearchData", hdnNameSearchData.Text);
                    Context.Items.Add("DuplicatePolicyData", hdnDuplicatePolicyData.Text);
                    Context.Items.Add("SourceCompanyCode", hdnSourceCompanyCode.Text);
                    Context.Items.Add("RpbsBillingDetails", hdnRpbsBillingDetails.Text);
                    //MAIG - CH1 - END - Added the code to save and pass the Company Code,EmailZip, NameSearchData,DuplicatePolicy and Source System in Context variable
                    Context.Items.Add("billplan", HiddenBillPLan.Text);
                    Context.Items.Add("PaymentPlan", HiddenPaymentPlan.Text);
                    Context.Items.Add("autoPay", hdnAutoPay.Text);
                    // Details stored back to context to be accessible in Billing and Payment Screen.
                    Context.Items.Add("DueDetails", HiddenDueDetails.Text);
                    Context.Items.Add("AmountSelected", HidddenAmountSelected.Text);
                    //PCI - Post Production Fix start:CH1 Added the code to add the variable to context for the CC and e-check payment type. by cognizant on 02/22/2012.
                    if ((Order.Detail.PaymentType == Convert.ToInt32(((StringDictionary)Application["Constants"])["PaymentID.CreditCard"])) || (Order.Detail.PaymentType == Convert.ToInt32(((StringDictionary)Application["Constants"])["PaymentID.ECheck"])))
                    {

                        Context.Items.Add("Cc_Echeckfailed", Order.Detail.PaymentType);
                    }
                    //PCI - Post Production Fix end:CH1 Added the code to add the variable to context for the CC and e-check payment type. by cognizant on 02/22/2012.
                    //67811A0  - PCI Remediation for Payment systems CH6: Start Added the condition to add contents to context for clearing the credit card and expiry contents in billing screen as a part of VA sacn defect by cognizant on 01/05/2011.
                    if (Order.Detail.PaymentType == Convert.ToInt32(((StringDictionary)Application["Constants"])["PaymentID.CreditCard"]))
                    {

                        Context.Items.Add("Ccfailed", Order.Detail.PaymentType);
                    }
                    //67811A0  - PCI Remediation for Payment systems CH6: End Added the condition to add contents to context for clearing the credit card and expiry contents in billing screen as a part of VA sacn defect by cognizant on 01/05/2011.
                }

                // 67811A0  - PCI Remediation for Payment systems CH4:END -Disable continue button on page load

                //For Credit Card Payment Show the Credit Card Information table
                //CSR#3937.Ch2 : START - Changed the code for getting constant value from Application object (web.config changes)
                if (Order.Detail.PaymentType == Convert.ToInt32(((StringDictionary)Application["Constants"])["PaymentID.CreditCard"]))
                {
                    //CSR#3937.Ch2 : END - Changed the code for getting constant value from Application object (web.config changes)
                    ((HtmlTableRow)Page.FindControl("tbrCCInfo")).Visible = true;
                    ((HtmlTableRow)Page.FindControl("tbrCheckInfo")).Visible = false;
                    //67811A0  - PCI Remediation for Payment systems CH1:added for displaying the authorisation text for CC type transaction
                    LblAuth.Text = Constants.PCI_CONFIRMATION_MESSAGE_ALL_PAYMENT_TYPES_EXCEPT_ECHCEK;
                }
                // PT_echeck Ch1:Start Added the condition to enable the echeck control for the echeck payment type by cognizant on 03/16/2011.
                else if (Order.Detail.PaymentType == Convert.ToInt32(((StringDictionary)Application["Constants"])["PaymentID.ECheck"]))
                {
                    ((HtmlTableRow)Page.FindControl("tbrCheckInfo")).Visible = true;
                    ((HtmlTableRow)Page.FindControl("tbrCCInfo")).Visible = false;
                    //67811A0  - PCI Remediation for Payment systems CH2:added for displaying the authorisation text for ECheck type transaction
                    LblAuth.Text = Constants.PCI_CONFIRMATION_MESSAGE_FOR_ECHCEK_PAYMENT;
                }
                // PT_echeck Ch1:End Added the condition to enable the echeck control for the echeck payment type by cognizant on 03/16/2011.
                else
                {
                    ((HtmlTableRow)Page.FindControl("tbrCCInfo")).Visible = false;
                    ((HtmlTableRow)Page.FindControl("tbrCheckInfo")).Visible = false;
                    //67811A0  - PCI Remediation for Payment systems CH3:added for displaying the authorisation text for cash/check type of transaction
                    LblAuth.Text = Constants.PCI_CONFIRMATION_MESSAGE_ALL_PAYMENT_TYPES_EXCEPT_ECHCEK;
                }
                //Show the Descriptions (Payment Type and Card Type)
                if (!IsPostBack)
                {

                    if (Order.Detail.PaymentType == Convert.ToInt32(((StringDictionary)Application["Constants"])["PaymentID.CreditCard"]))
                    {
                        lblPaymentDesc.Text = Context.Items["PaymentTypeDescription"].ToString();
                        lblPaymentCaption.Text = Context.Items["PaymentTypeDescription"].ToString();
                        if (Context.Items["CardTypeDescription"].ToString() != "")
                            lblCardDesc.Text = Context.Items["CardTypeDescription"].ToString();
                    }

                    //PT_echeck Ch2:Start Added the condition to  get the account type selected item from echeck control for echeck payment type by cognizant on 03/16/2011.
                    else if (Order.Detail.PaymentType == Convert.ToInt32(((StringDictionary)Application["Constants"])["PaymentID.ECheck"]))
                    {
                        lblPaymentDesc.Text = Context.Items["PaymentTypeDescription"].ToString();
                        lblPaymentCaption.Text = Context.Items["PaymentTypeDescription"].ToString();

                        if (Context.Items["CheckAccountTypeDescription"].ToString() != "")
                            lblchecktype.Text = Context.Items["CheckAccountTypeDescription"].ToString();
                        lblcheckdesc.Text = "Check".ToString();
                    }
                    //PT_echeck Ch2:End Added the condition to  get the account type selected item from echeck control for echeck payment type by cognizant on 03/16/2011.
                    else
                    {
                        lblPaymentCaption.Text = Context.Items["PaymentTypeDescription"].ToString();
                    }
                    //Hold the Billing Back URL in the ViewState
                    ViewState["Billing_BackUrl"] = (Context.Items["Billing_BackUrl"] != null) ? (Context.Items["Billing_BackUrl"].ToString()) : null;
                }
                //Put the Billing Backurl in the Context and transfer to Billing  page.
                if (ViewState["Billing_BackUrl"] != null)
                    Context.Items["BillingBackUrl"] = ViewState["Billing_BackUrl"].ToString();
                //if(chk
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
            //CHG0115410 - END - CH1 - Added try catch block & Logged the exception to the log file
        }
        /// <summary>
        /// Attempts to process the order.
        /// </summary>
        public override bool ProcessPage()
        {
            //CHG0115410 - BEGIN - CH1 - Added try catch block & Logged the exception to the log file
            try
            {
                //Stores the Process Order Result		
                bool Result = Order.Process();


                if (!Result)
                {

                    ViewState["Billing_BackUrl"] = "";

                }
                // Modified by Cognizant. Order.Errors[0].Target removed  and inner if added to Disable Continue Button
                //if (Order.Errors!=null && Order.Errors.Count>0 && Order.Errors[0].Target=="")
                if (Order.Errors != null && Order.Errors.Count > 0)
                {
                    if (Order.Errors[0].Target == "")
                    {
                        Buttons1.HideBackButton = true;
                    }
                    Buttons1.HideContinueButton = true;
                }
                return Result;
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
                return false;
            }
            //CHG0115410 - END - CH1 - Added try catch block & Logged the exception to the log file
        }


        #region Web Form Designer generated code
        ///<summary/>
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //

            InitializeComponent();
            base.OnInit(e);



        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
        }
        #endregion



    }
}
