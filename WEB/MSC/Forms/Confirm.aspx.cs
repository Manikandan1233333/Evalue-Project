/*
 * MAIG - CH1 - Added code to store the Details which are passed to Manage Recurring page. 
 * MAIG - CH2 - Added the code to display the Recurring Button on payment confirmation based on the below Criteria
 * MAIG - CH3 - Added the method Recurring Click which redirects to Manage Enrollment Page
 * MAIG - CH4 - Enhanced the Logging to log if redirection is performed
 * MAIGEnhancement CHG0107527 - CH1 - Added the below code to avoid exception while assigning the next Active Page value to a property _ContinueUrl
 * MAIGEnhancement CHG0107527 - CH2 - Modified the below Down payment flow to use the key/value pair with ^ than , which causes issue in Full Name field from RPBS response
 * MAIGEnhancement CHG0107527 - CH3 - Modified the below Installment payment flow to use the key/value pair with ^ than , which causes issue in Full Name field from RPBS response
 * CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the MembershipContact.ascx User control code as part of the Code Clean Up - March 2016
*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CSAAWeb;
using CSAAWeb.WebControls;
//CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016
using System.Collections.Generic;


namespace MSC.Forms
{
	/// <summary>
	/// This page displays the order confirmation.
	/// </summary>
	public partial class Confirm : SiteTemplate
	{
		///<summary/>
		protected Label SuccessLabel;
		///<summary/>
		///START Commented by Cognizant on 07/20/2004 for Temp Membership Project
		//protected MSC.Controls.MembershipCard MembershipCard0;
		//END
		///<summary/>
		protected System.Web.UI.WebControls.HyperLink AssociateCards1;
		///<summary/>
		protected System.Web.UI.WebControls.HyperLink AssociateCards2;
		///<summary/>
		protected System.Web.UI.WebControls.HyperLink DoAnotherInsurance;
		///<summary/>
		protected System.Web.UI.WebControls.HyperLink DoAnotherMembership;
		///<summary/>
        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016
		///<summary/>
        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the MembershipContact.ascx User control code as part of the Code Clean Up - March 2016
		///<summary/>
		protected HtmlTable MemberCard;
		///<summary/>
		protected HtmlTable InsuranceCard;

        //MAIGEnhancement CHG0107527 - CH1 - BEGIN - Added the below code to avoid exception while assigning the next Active Page value to a property _ContinueUrl
        protected static string _OnContinueUrl = string.Empty;
        //MAIGEnhancement CHG0107527 - CH1 - END - Added the below code to avoid exception while assigning the next Active Page value to a property _ContinueUrl

        //MAIG - BEGIN - CH1 - Added code to store the Details which are passed to Manage Recurring page. 
        private System.Collections.Generic.Dictionary<string, string> ccRecurring = new System.Collections.Generic.Dictionary<string, string>();
        //MAIG - END - CH1 - Added code to store the Details which are passed to Manage Recurring page. 
		/// <summary>
		/// Added by Cognizant on 05/20/2004 for creating a string variable which displays the PaymentType				///	Description in Confirm.aspx page 
		/// </summary>
 		
		public string strReceiptNumber;


		/// <summary>
		/// Page_Load sets the properties of the membership card image, and if
		/// there is a charge to the member's card and an email address was given,
		/// creates and sends a confirming email using the ConfirmingEmail control 
		/// to generate the email's text.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected void Page_Load(object sender, System.EventArgs e)
		{	
			
		
			if (Order.Products["Insurance"]!=null) HandleInsurance();
			//if (Order.Products["Membership"]!=null) HandleMembership();
			//START Changed on 05/24/2004 for assigning the ReceiptNumber and the ReceiptType to the QueryString Modified the ASP:HyperLink into the ASP:ImageButtons on 05/27/2004
			//The Parameters are Receipt number and Receipt Type(CC for customer Copy, MC for Merchant Copy).	
 			((ImageButton)this.FindControl("ibInsuranceCustomerReceipt")).Attributes.Add("onclick","return Open_Receipt('" + Order.Detail.ReceiptNumber + "','" + "CC" + "')");
			((ImageButton)this.FindControl("ibInsuranceCustomerReceipt")).CausesValidation=false; 
			
			((ImageButton)this.FindControl("ibInsuranceMerchantReceipt")).Attributes.Add("onclick","return Open_Receipt('" + Order.Detail.ReceiptNumber + "','" + "MC" + "')");
			((ImageButton)this.FindControl("ibInsuranceMerchantReceipt")).CausesValidation=false;

			((ImageButton)this.FindControl("ibMemberCustomerReceipt")).Attributes.Add("onclick","return Open_Receipt('" + Order.Detail.ReceiptNumber + "','" + "CC" + "')");
			((ImageButton)this.FindControl("ibMemberCustomerReceipt")).CausesValidation=false;

			((ImageButton)this.FindControl("ibMemberMerchantReceipt")).Attributes.Add("onclick","return  Open_Receipt('" + Order.Detail.ReceiptNumber + "','" + "MC" + "')");
			((ImageButton)this.FindControl("ibMemberMerchantReceipt")).CausesValidation=false;

			//END
			//START Added by Cognizant on 07/20/2004 for Temp Membership Project
			((HtmlTable)FindControl("tdCard")).Visible = false;  
			//END
		}


		private void HandleInsurance() {
			/// .Modified by Cognizant Added for PCR168
			SuccessLabel.Text = "The insurance payment has been accepted ";
			InsuranceCard.Visible=true;
			DoAnotherInsurance.NavigateUrl = OnCancelUrl;
            //MAIG - CH2 - BEGIN - Added the code to display the Recurring Button on payment confirmation based on the below Criteria
            try
            {
                if (!IsPostBack)
                {
                    if (Order.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.CreditCard || Order.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.ECheck)
                    {
                        if (Order.Lines.Count > 0)
                        {
                            ccRecurring.Add(Constants.PMT_RECURRING_ACCOUNTNUMBER, Order.Lines[0].AccountNumber);
                            ccRecurring.Add(Constants.PMT_RECURRING_PRODUCTTYPE, Order.Lines[0].ProductTypeNew);
                            ccRecurring.Add(Constants.PMT_RECURRING_CCECTOKEN, Order.Lines[0].SubProduct);
                            Order.Lines[0].SubProduct = "";
                            ccRecurring.Add(Constants.PMT_RECURRING_CCZIPCODE, Order.Addresses[0].Zip);

                            if (Order.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.CreditCard)
                            {
                                ccRecurring.Add(Constants.PMT_RECURRING_IS_CC_PAYMENT, "True");
                                ccRecurring.Add(Constants.PMT_RECURRING_CCType, Order.Card.CCType);
                                ccRecurring.Add(Constants.PMT_RECURRING_CCFULLNAME, Order.Addresses.BillTo.FirstName + " " + Order.Addresses.BillTo.LastName);
                                ccRecurring.Add(Constants.PMT_RECURRING_CCNUMBER, Order.Card.CCNumber);
                                ccRecurring.Add(Constants.PMT_RECURRING_CCEXPMONTHYEAR, Order.Card.CCExpMonth + "/" + Order.Card.CCExpYear);
                            }
                            else
                            {
                                ccRecurring.Add(Constants.PMT_RECURRING_IS_CC_PAYMENT, "False");
                                ccRecurring.Add(Constants.PMT_RECURRING_ECBANKACNTTYPE, Order.echeck.BankAcntType);
                                ccRecurring.Add(Constants.PMT_RECURRING_ECBANKID, Order.echeck.BankId);
                                ccRecurring.Add(Constants.PMT_RECURRING_CCFULLNAME, Order.echeck.CustomerName);
                                ccRecurring.Add(Constants.PMT_RECURRING_ECACCOUNTNUMBER, Order.echeck.BankAcntNo);
                            }
                            string RpbsAllowed = string.Empty;
                            if (Order.Lines[0].RevenueType.Equals(CSAAWeb.Constants.PC_REVENUE_DOWN))
                            {
                                string[] sourcesystem = Context.Items["SourceCompanyCode"].ToString().Split('-');
                                if (sourcesystem.Length == 2)
                                {
                                    ccRecurring.Add(Constants.PMT_RECURRING_INSTSOURCESYSTEM, sourcesystem[1]);
                                }
                                else
                                {
                                    ccRecurring.Add(Constants.PMT_RECURRING_INSTSOURCESYSTEM, "");
                                }
                                ccRecurring.Add(Constants.PMT_RECURRING_ISDOWNFLOW, "true");
                                ccRecurring.Add(Constants.PMT_RECURRING_DOWNFIRSTNAME, Order.Lines[0].FirstName);
                                ccRecurring.Add(Constants.PMT_RECURRING_DOWNLASTNAME, Order.Lines[0].LastName);
                                if (Context.Items["RpbsBillingDetails"] != null)
                                {
                                    ccRecurring.Add("RpbsBillingDetails", Context.Items["RpbsBillingDetails"].ToString());
                                    //MAIGEnhancement CHG0107527 - CH2 - BEGIN - Modified the below Down payment flow to use the key/value pair with ^ than , which causes issue in Full Name field from RPBS response
                                    var RpbsRow = Context.Items["RpbsBillingDetails"].ToString().Substring(0, Context.Items["RpbsBillingDetails"].ToString().Length - 1).Split('^');
                                    //MAIGEnhancement CHG0107527 - CH2 - END - Modified the below Down payment flow to use the key/value pair with ^ than , which causes issue in Full Name field from RPBS response
                                    Dictionary<string, string> RpbsData = RpbsRow.Select(rec => rec.Split('*'))
                            .ToDictionary(rec => rec[0].Trim(),
                            rec => rec[1].Trim());
                                    RpbsAllowed = RpbsData[Constants.PC_BILL_SETUPAUTOPAYREASONCODE];
                                }
                                if (Order.Products["Insurance"] != null)
                                {
                                    InsuranceClasses.InsuranceInfo InsInfo = (InsuranceClasses.InsuranceInfo)Order.Products["Insurance"];
                                    if (InsInfo.Addresses.Count > 0)
                                    {
                                        ccRecurring.Add(Constants.PMT_RECURRING_DOWNMAILINGZIP, InsInfo.Addresses[0].Zip);
                                        ccRecurring.Add(Constants.PMT_RECURRING_DOWNEMAILADDRESS, InsInfo.Addresses[0].Email);
                                        InsInfo.Addresses = null;
                                    }
                                }

                                bool KicAuto = false;
                                bool KicHome = false;
                                bool HDESHome = false;
                                bool PUP = false;
                                if (Config.Setting("Invalid_KIC_Auto_Allowed").ToString().Equals("1"))
                                {
                                    KicAuto = (Order.Lines[0].AccountNumber.Trim().Length == 8 && Order.Lines[0].ProductTypeNew.Equals(MSC.SiteTemplate.ProductCodes.PA.ToString())) ? true : false;
                                }
                                if (Config.Setting("Invalid_KIC_Home_Allowed").ToString().Equals("1"))
                                {
                                    KicHome = (Order.Lines[0].AccountNumber.Trim().Length == 8 && Order.Lines[0].ProductTypeNew.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString())) ? true : false;
                                }
                                if (Config.Setting("Invalid_HDES_Home_Allowed").ToString().Equals("1"))
                                {
                                    HDESHome = (Order.Lines[0].AccountNumber.Trim().Length == 7 && Order.Lines[0].ProductTypeNew.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString())) ? true : false;
                                }
                                if (Config.Setting("Invalid_PUP_Allowed").ToString().Equals("1"))
                                {
                                    PUP = (Order.Lines[0].AccountNumber.Trim().Length == 7 && Order.Lines[0].ProductTypeNew.Equals(MSC.SiteTemplate.ProductCodes.PU.ToString())) ? true : false;
                                }

                                //if ((Order.Lines[0].ProductTypeNew.Equals(MSC.SiteTemplate.ProductCodes.PA.ToString()) && Order.Lines[0].AccountNumber.Length == 8)
                                //    || ((Order.Lines[0].ProductTypeNew.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()) 
                                //    || Order.Lines[0].ProductTypeNew.Equals(MSC.SiteTemplate.ProductCodes.PU.ToString()))
                                //    && Order.Lines[0].AccountNumber.Length == 7) 
                                if ((KicAuto || KicHome || HDESHome || PUP)
                                && (RpbsAllowed.ToLower().StartsWith("elig")
                                    || !Convert.ToBoolean(Convert.ToInt16(System.Configuration.ConfigurationSettings.AppSettings["AllowRPBS"].ToString()))))
                                {
                                    if (Context.Items["RecurringRedirection"] == null)
                                    {
                                        Context.Items.Add("RecurringRedirection", ccRecurring);
                                    }
                                    RecurringRedirection.Visible = true;
                                    ViewState.Add("RecurringRedirection", Context.Items["RecurringRedirection"]);
                                }
                                else
                                {
                                    RecurringRedirection.Visible = false;
                                }
                            }
                            else
                            {
                                ccRecurring.Add(Constants.PMT_RECURRING_ISDOWNFLOW, "false");
                                if (Context.Items["SourceCompanyCode"] != null)
                                {
                                    string[] sourcesystem = Context.Items["SourceCompanyCode"].ToString().Split('-');
                                    if (sourcesystem.Length == 2)
                                    {
                                        ccRecurring.Add(Constants.PMT_RECURRING_INSTSOURCESYSTEM, sourcesystem[1]);
                                    }
                                    else
                                    {
                                        ccRecurring.Add(Constants.PMT_RECURRING_INSTSOURCESYSTEM, "");
                                    }

                                }
                                if (Context.Items["RpbsBillingDetails"] != null)
                                {
                                    ccRecurring.Add("RpbsBillingDetails", Context.Items["RpbsBillingDetails"].ToString());
                                    if (!string.IsNullOrEmpty(Context.Items["RpbsBillingDetails"].ToString()))
                                    {
                                        //MAIGEnhancement CHG0107527 - CH3 - BEGIN - Modified the below Installment payment flow to use the key/value pair with ^ than , which causes issue in Full Name field from RPBS response
                                        var RpbsRow = Context.Items["RpbsBillingDetails"].ToString().Substring(0, Context.Items["RpbsBillingDetails"].ToString().Length - 1).Split('^');
                                        //MAIGEnhancement CHG0107527 - CH3 - END - Modified the below Installment payment flow to use the key/value pair with ^ than , which causes issue in Full Name field from RPBS response
                                        Dictionary<string, string> RpbsData = RpbsRow.Select(rec => rec.Split('*'))
                                .ToDictionary(rec => rec[0].Trim(),
                                rec => rec[1].Trim());
                                        RpbsAllowed = RpbsData[Constants.PC_BILL_SETUPAUTOPAYREASONCODE];
                                    }
                                }
                                if (Order.Products["Insurance"] != null)
                                {
                                    InsuranceClasses.InsuranceInfo InsInfo = (InsuranceClasses.InsuranceInfo)Order.Products["Insurance"];
                                    if (InsInfo.Addresses.Count > 0)
                                    {
                                        ccRecurring.Add(Constants.PMT_RECURRING_DOWNEMAILADDRESS, InsInfo.Addresses[0].Email);
                                        InsInfo.Addresses = null;
                                    }
                                }
                                string[] SourceSystem = { };
                                if (Context.Items["PaymentPlan"] != null && Context.Items["autoPay"] != null)
                                {
                                    ViewState["PaymentPlan"] = Context.Items["PaymentPlan"];
                                    if (Context.Items["PaymentPlan"].ToString().ToUpper().Equals("DIRECT") && RpbsAllowed.ToLower().StartsWith("elig"))
                                    {
                                        if (Context.Items["RecurringRedirection"] == null)
                                        {
                                            Context.Items.Add("RecurringRedirection", ccRecurring);
                                        }
                                        RecurringRedirection.Visible = true;
                                        ViewState.Add("RecurringRedirection", Context.Items["RecurringRedirection"]);
                                    }
                                    else if ((Context.Items["PaymentPlan"].ToString().ToUpper().Equals("AUTO")) || Context.Items["autoPay"].ToString().ToUpper().Equals("TRUE"))
                                    {
                                        RecurringRedirection.Visible = false;
                                    }
                                }
                            }
                        }

                    }
                    else
                    {
                        RecurringRedirection.Visible = false;
                    }
                }
                else
                {
                    if (Context.Items["RecurringRedirection"] == null)
                    {
                        Context.Items.Add("RecurringRedirection", ViewState["RecurringRedirection"]);
                    }
                }

            }
            catch (Exception ex)
            {
                CSAAWeb.AppLogger.Logger.Log("Exception occurred in HandleInsurance method" + ex.ToString());
            }
			//MAIG - CH2 - END - Added the code to display the Recurring Button on payment confirmation based on the below Criteria
		}

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016

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
        //MAIG - CH3 - BEGIN - Added the method Recurring Click which redirects to Manage Enrollment Page
        protected void imgManageRecurring_Click(object sender, ImageClickEventArgs e)
        {
           /* ccRecurring.Add(Constants.PMT_RECURRING_ACCOUNTNUMBER, Order.Lines[0].AccountNumber);
            ccRecurring.Add(Constants.PMT_RECURRING_PRODUCTTYPE, Order.Lines[0].ProductTypeNew);
            ccRecurring.Add(Constants.PMT_RECURRING_CCNUMBER, Order.Card.CCNumber);
            ccRecurring.Add(Constants.PMT_RECURRING_CCECTOKEN, Order.Lines[0].SubProduct);
            Order.Lines[0].SubProduct = "";
            ccRecurring.Add(Constants.PMT_RECURRING_CCEXPMONTHYEAR, Order.Card.CCExpMonth+"/"+Order.Card.CCExpYear);
            ccRecurring.Add(Constants.PMT_RECURRING_CCFULLNAME, Order.Addresses.BillTo.FirstName + " " + Order.Addresses.BillTo.LastName);
            ccRecurring.Add(Constants.PMT_RECURRING_CCZIPCODE, Order.Addresses[0].Zip);*/
            //ccRecurring.Add("AccountNumber"
            //Session.Add("temp", "value");
            //Context.Items.Add("PaymentPlan", ViewState["PaymentPlan"]);
            //MAIG - CH4 - BEGIN - Enhanced the Logging to log if redirection is performed
            CSAAWeb.AppLogger.Logger.Log("Payment re-direction is made with pre-populated payment details to Enrollment page. UserID: "+Page.User.Identity.Name);
            if (Config.Setting("Logging.PaymentRedirection").Equals("1"))
            {
                if(Context.Items["RecurringRedirection"]!=null)
                {
                    System.Collections.Generic.Dictionary<string, string> LogRecurringDetails = (System.Collections.Generic.Dictionary<string, string>)Context.Items["RecurringRedirection"];
                    string combinedData = string.Join(";", LogRecurringDetails.Select(x => x.Key + "=" + x.Value).ToArray());
                    CSAAWeb.AppLogger.Logger.Log("Payment details sent to Recurring Page : " + combinedData);
                }
            }
            //MAIG - CH4 - END - Enhanced the Logging to log if redirection is performed
            GotoPage(CSAAWeb.Constants.PC_ENROLL_REDIRECT_URL);
            //ThisPage.GotoPage(ThisPage.OnContinueUrl);// CSAAWeb.Constants.PC_ENROLL_REDIRECT_URL);
            //Response.Redirect(CSAAWeb.Constants.PC_ENROLL_REDIRECT_URL);
            //Server.Transfer(CSAAWeb.Constants.PC_ENROLL_REDIRECT_URL);
        }
        //MAIG - CH3 - END - Added the method Recurring Click which redirects to Manage Enrollment Page
	}
}
