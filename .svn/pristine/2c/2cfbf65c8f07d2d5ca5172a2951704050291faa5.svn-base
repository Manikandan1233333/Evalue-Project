/*
 * HISTORY
 * PC Phase II changes - Added the page as part of Enrollment changes.Terms and condition 
 * Created by: Cognizant  on 2/5/2013
 * Decsription: This is a new file added as part of Payment Central Phase II.
 * CHG0072116 - PC Edit Card Details CH1 - Show the required fields of the "Edit Card Details" with arguments.
 * T&C Changes CH1 - Added the below code to assign the policy number and product type value from the arguments 
 * MAIG - CH1 - As part of Changes to T&C, the Credit Card and Echeck dynamic fields removed. As advised by Business, Only PolicyNumber & Insured Full Name will be displayed.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSAAWeb.AppLogger;
using CSAAWeb;

namespace MSC.Controls
{
    public partial class TermsAndConditions : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                MSC.Encryption.EncryptQueryString args = new MSC.Encryption.EncryptQueryString(Request.QueryString["args"]);

                if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PC_TC_ARG1].ToString()))
                {                    
				//MAIG - CH1 - START - As part of Changes to T&C, the Credit Card and Echeck dynamic fields removed. As advised by Business, Only PolicyNumber & Insured Full Name will be displayed.
                    lblPolicyno.Text = args[CSAAWeb.Constants.PC_TC_ARG1].ToString();
                }

                if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PC_TC_ARG2].ToString()))
                {
                    lblInsName.Text = args[CSAAWeb.Constants.PC_TC_ARG2].ToString();
                }

                    #region Old T&C Dynamic fields - Commented out
                    ////if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PC_TC_ARG8].ToString()))
                    ////{
                    ////    lblProd.Text = args[CSAAWeb.Constants.PC_TC_ARG8].ToString();
                    ////}
                    //T&C Changes CH1 - END - Added the below code to assign the policy number and product type value from the arguments
                    ////if (args[CSAAWeb.Constants.PC_TC_ARG1].ToString() == CSAAWeb.Constants.PC_PTYPE_TC)
                    ////{
                    ////    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PC_TC_ARG2].ToString()))
                    ////    {
                    ////        lblCard.Text = args[CSAAWeb.Constants.PC_TC_ARG2].ToString();
                    ////    }
                    ////    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PC_TC_ARG3].ToString()))
                    ////    {
                    ////        lblCardNum.Text = CSAAWeb.Constants.PC_MASK_TXT + args[CSAAWeb.Constants.PC_TC_ARG3].ToString();
                    ////    }
                    ////    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PC_TC_ARG4].ToString()))
                    ////    {
                    ////        lblExpire.Text = args[CSAAWeb.Constants.PC_TC_ARG4].ToString();
                    ////    }
                    ////    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PC_TC_ARG5].ToString()))
                    ////    {
                    ////        lblCardName.Text = args[CSAAWeb.Constants.PC_TC_ARG5].ToString();
                    ////    }
                    ////    tblBank.Visible = false;
                    ////    tblCustInfo.Visible = true;
                    ////    this.tblUpdateCardDetails.Visible = false; 
                    ////}
                    //CHG0072116 - PC Edit Card Details CH1:START - Show the required fields of the "Edit Card Details" with arguments.
                    ////else if (args[CSAAWeb.Constants.PC_TC_ARG1].ToString() == CSAAWeb.Constants.PC_PTYPE_CC)
                    ////{
                    ////    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PC_TC_ARG2].ToString()))
                    ////    {
                    ////        lblCardNumberLast4Digit.Text = args[CSAAWeb.Constants.PC_TC_ARG2].ToString();
                    ////    }
                    ////    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PC_TC_ARG3].ToString()))
                    ////    {
                    ////        lblCCExpireDate.Text = args[CSAAWeb.Constants.PC_TC_ARG3].ToString();
                    ////    }
                    ////    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PC_TC_ARG4].ToString()))
                    ////    {
                    ////       lblCusName.Text = args[CSAAWeb.Constants.PC_TC_ARG4].ToString();
                    ////    }
                    ////    tblBank.Visible = false;
                    ////    tblCustInfo.Visible = false;
                    ////    tblUpdateCardDetails.Visible = true;
                    ////}
                    //CHG0072116 - PC Edit Card Details CH1:END - Show the required fields of the "Edit Card Details" with arguments.
                    ////else if (args[CSAAWeb.Constants.PC_TC_ARG1].ToString().ToString() == CSAAWeb.Constants.PC_EC_PAYMNT_TYPE)
                    ////{
                    ////    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PC_TC_ARG2].ToString()))
                    ////    {
                    ////        lblAccount.Text = args[CSAAWeb.Constants.PC_TC_ARG2].ToString();
                    ////    }
                    ////    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PC_TC_ARG3].ToString()))
                    ////    {
                    ////        lblBankName.Text = args[CSAAWeb.Constants.PC_TC_ARG3].ToString();
                    ////    }

                    ////    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PC_TC_ARG4].ToString()))
                    ////    {
                    ////        lblRoutingNum.Text = args[CSAAWeb.Constants.PC_TC_ARG4].ToString();
                    ////    }
                    ////    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PC_TC_ARG5].ToString()))
                    ////    {
                    ////        if (args[CSAAWeb.Constants.PC_TC_ARG5].ToString().Length > 4)
                    ////        {
                    ////            lblAccountNum.Text = CSAAWeb.Constants.PC_MASK_TXT + args[CSAAWeb.Constants.PC_TC_ARG5].ToString().ToString().Substring(args["arg5"].ToString().Length - 4, 4);
                    ////        }
                    ////        else if (args[CSAAWeb.Constants.PC_TC_ARG5].ToString().Length >= 1)
                    ////        {
                    ////            lblAccountNum.Text = CSAAWeb.Constants.PC_MASK_TXT + args[CSAAWeb.Constants.PC_TC_ARG5].ToString();
                    ////        }
                    ////    }
                    ////    if (!string.IsNullOrEmpty(args[CSAAWeb.Constants.PC_TC_ARG6].ToString()))
                    ////    {
                    ////        lblAccountNames.Text = args[CSAAWeb.Constants.PC_TC_ARG6].ToString();
                    ////    }
                    ////    tblBank.Visible = true;
                    ////    tblCustInfo.Visible = false;
                    ////    tblUpdateCardDetails.Visible = false;
                    ////}
                    # endregion
                	//MAIG - CH1 - END - As part of Changes to T&C, the Credit Card and Echeck dynamic fields removed. As advised by Business, Only PolicyNumber & Insured Full Name will be displayed.
            }
            catch (Exception exception)
            {
                string errorMessage = string.Empty;
                Logger.Name = CSAAWeb.Constants.PC_TC_LOG_NAME;
                Logger.Log(exception);
            }
        }
    }
}
