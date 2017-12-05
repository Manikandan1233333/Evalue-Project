/*
 * HISTORY
 *			11/18/2004 JOM Replaced Convert.ToInt32(Config.Setting("PaymentID.CreditCard")) with 
 *			PaymentClasses.PaymentTypes.CreditCard
01/13/2011 PUP Ch1:Modified the input flag to the Get payment type method to display only credit card payment for PUP policy -By cognizant
01/13/2011 PUP ch2:Added the name space to access the string dictionary - by cognizant 
01/13/2011 PUP ch3-added the column flag variable to the Payment type property and assigned the column default flag value to P.
05/03/2011 RFC#130547 PT_echeck Ch1: Added the condition to store the echeck details in the echeck object for the echeck payment type by cognizant.
05/03/2011 RFC#130547 PT_echeck Ch2: Added the condition to restore the echeck details in the echeck object for the echeck payment type by cognizant. 
05/03/2011 RFC#130547 PT_echeck Ch3:Modified the condition to to check the payment type is not echeck payment type by cognizant.
05/03/2011 RFC#130547 PT_echeck Ch4:Modified the condition to to check the payment type is not echeck payment type by cognizant.
05/03/2011 RFC#130547  PT_echeck Ch5:Modified the condition to check the payment type is echeck payment type by cognizant.
05/03/2011 RFC#130547 PT_echeck Ch6:Added the condition to check the payment type is echeck and getting the payment account type control by cognizant. 
05/03/2011 RFC#130547 PT_echeck Ch7:Added the condition to check the payment type is echeck and making the echeck contols to be visible by cognizant.
05/03/2011 RFC#130547 PT_echeck Ch8: Modified the condition to restore the echeck details in the echeck object for the echeck payment type also by cognizant.
05/03/2011 RFC#130547 PT_echeck Ch9: Added the reference echeck control to the billing page.
67811A0 - PCI Remediation for Payment systems CH1: Added the namespace for RegularExpressions
67811A0 - PCI Remediation for Payment systems CH2: Get and Set property for Policy Number
67811A0 - PCI Remediation for Payment systems CH3: Get and Set property for FirstName
67811A0 - PCI Remediation for Payment systems CH4: Get and Set property for LastName
67811A0 - PCI Remediation for Payment systems CH5: Removed existing Get and Set property for Amount
67811A0 - PCI Remediation for Payment systems CH6: Required Field validation for FirstName 
67811A0 - PCI Remediation for Payment systems CH7: validate whether FirstName doesn't contain numeric and special characters.
67811A0 - PCI Remediation for Payment systems CH8: validate FirstName and Lastname. length It can accommodate a maximum of 25 characters total for the name including spaces between names.
67811A0 - PCI Remediation for Payment systems CH9: Required Field validation for amount entered in Other Amount textbox.
67811A0 - PCI Remediation for Payment systems CH10: Positive Amount validation for amount entered in Other Amount textbox.
67811A0 - PCI Remediation for Payment systems CH11: validate whether amount entered in Other Amount textbox doesn't contain numeric and special characters.
67811A0 - PCI Remediation for Payment systems CH12: Required Field validation for Payment Type. 
67811A0 - PCI Remediation for Payment systems CH13: AddedDuplicate Policy Validation.
67811A0 - PCI Remediation for Payment systems CH14: Set order summary visibility to false since it is no longer needed.
67811A0 - PCI Remediation for Payment systems CH15: Added method to display Payment informations to be pre populated on screen load.
67811A0 - PCI Remediation for Payment systems CH16: Commented the below code setting pagevalidator visibility to false.
67811A0 - PCI Remediation for Payment systems CH17: save selected amount,FirstName, Lastname to insurance line item.
67811A0 - PCI Remediation for Payment systems CH18: Set focus to Validation summary error message.
67811A0 - PCI Remediation for Payment systems CH19: Select PAyment Type only Echeck and Credit card for WU products.
67811A0 - PCI Remediation for Payment systems CH20: Added IsDuplicate Property which is used to hold the Value of duplicate Payment.
67811A0 - PCI Remediation for Payment systems CH21: Declared Public variable _IsDuplicate
67811A0 - PCI Remediation for Payment systems CH22: Added dupPolicyHidden Property which is used to store values of duplicate policy.
67811A0 - PCI Remediation for Payment systems CH23: validate whether Mimimumdue is not zero.
67811A0 - PCI Remediation for Payment systems CH24: validate whether Totalbalance is not zero.
67811A0 - PCI Remediation for Payment systems CH25: Creation of new Regular Expression to check the FirstName 
67811A0 - PCI Remediation for Payment systems CH26: Creation of new Regular Expression to check the Positive value 
67811A0 - PCI Remediation for Payment systems CH27: Commented since the code is no longer used.
67811A0 - PCI Remediation for Payment systems CH28: Check if the duplicate alert is checked on postback and then enable the continue button.
67811A0 - PCI Remediation for Payment systems CH29 Start: Clear CC details if a new Policy is selected
67811A0 - PCI Remediation for Payment systems CH30 : Added to clear the cache of the page to prevent the page loading on back button hit after logout
 * PCI - Post Production Fix start:CH1 Added the code to make the merchant reference number to null for the failed transactions on click of back button by cognizant on 02/22/2012
 * PAS AZ Product Configuration Ch1:Modified the below conditions to display on credit card and e-check transactions for PAS AZ product by cognizant on 03/11/2012
 //Company Code fix - Append the Company ID along with the Policy state in the Order.Detail information
 // Company code Fix - Added the company Id along with the state and prefix and assigned it to the hidden variable
 * CHG0055954-AZ PAS Conversion and PC Integration CH1- Added the below code to diplay message for HL product
 * CHG0055954-AZ PAS Conversion and PC integration CH2- Modified the below code to validate other amount field for the HL product type
 * //CHG0055954-AZ PAS Conversion and PC integration CH3 - Added the below code to add the check number and the company code value to the policy state and policy prefix for non PCP transactions.
 * CHG0072116 - PC Clear CC Details when user clicks back button in insurance page CH1 -  clear the requrired field values if context items is null.
 * MAIG - CH1 - Modified the Regex pattern to include full stop and comma
 * MAIG - CH2 - Validation for Zip Code
 * MAIG - CH3 - Removed the logic, since High Limit Product is removed 
 * MAIG - CH4 - Restricting Other amount to Max Payment Amount 
 * MAIG - CH5 - Removed the logic, since High Limit Product is removed 
 * MAIG - CH6 - Modified the verbiage of the error message
 * MAIG - CH7 - Commented the Duplicate payment alert validation code
 * MAIG - CH8 - Added the code to pass the Company Code and Source System
 * MAIG - CH9 - Modified the logic to get the Policy State and Prefix and assign it to Order Object & pdate the CompanyCode and Source System to OrderDetail Object
 * MAIG - CH10 - Added the logic to get the Email Address, Mailing Zip and assign it to Order Object. 
 * MAIG - CH11 - Updated the array length from 7 to 8 for getting the PolicyState and Prefix for all policies. 
 * MAIG - CH12 - Updated the index to [0][3] to [0][0]
 * MAIG - CH13 - Updated the logic to get the PolicyState and Prefix for all policies. 
 * MAIG - CH14 - Commented the logic to skip the PUP prefix for PUP policies
 * MAIG - CH15 - Modified the logic to display only the recurrign Enrolled Status
 * MAIG - CH16 - Added State constraint for checking if the policy falls in the Risk state
 * MAIG - CH17 - Modified the logic to restrict the Payment Type 
 * MAIG - CH18 - Added method that hides teh AMEX and Discover Credit Card types
 * MAIG - CH19 - Added method that hides teh AMEX and Discover Credit Card types
 * MAIGEnhancement CHG0107527 - CH1 - Modified the below code by including a condition to allow Cash/Check for users having PS User role ir-respective of source system
 * MAIGEnhancement CHG0107527 - CH2 - Modified the below code to append the key/value pair with ^ than , which causes issue in Full Name field from RPBS response
 * CHG0109406 - CH1 - Added the below code and condition to set the View state if the policy number is modified and set the Credit card First and Last Name from the current policy than setting it from the old Policy
 * CHG0110069 - CH1 - Modified the below logic to clear the old CC/EC details when the poicy number is modified in the Duplicate Policy Scenario
 * CHG0112662 - Commented the Other amount validation check for SIS Policies
 * CHG0113938 - CH1 - Display Policy Status information in Billing summary.
 * CHG0113938 - CH2 - Hide Policy Status information for Down payments.
 * CHG0113938 - CH3 - Added condition to show cash & check for State 'CA', Product 'DF' & 'PU', Source 'PAS' policies .
 * CHG0116140 - Payment Restricition - Set PaymentRestriction variable for Payment Restriction check & show the payment restriction message to user.
 * CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Added code to clear the ACH Routing number and Account number - VA Defect ID - 216
 * CHGXXXXXXJ - CH2 - Changes made for splitting the token service - 27/06/2017.
 */
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CSAAWeb;
using CSAAWeb.WebControls;
using OrderClasses;
using InsuranceClasses;
using OrderClasses.Service;
//PUP ch2: START Added the name spaceto access the string dictionary by cognizant on 01/13/2011
using System.Collections.Specialized;
using System.Configuration;
//PUP ch2:END Added the name spaceto access the string dictionary by cognizant on 01/13/2011

//67811A0 - PCI Remediation for Payment systems CH1:START- Added the namespace for RegularExpressions
using System.Text.RegularExpressions;
using System.Text;
//67811A0 - PCI Remediation for Payment systems CH1:END- Added the namespace for RegularExpressions
using OrderClassesII;

namespace MSC.Forms
{
    /// <Billing Page>
    /// This page captures the billing information for an order,
    /// and when continue is clicked, if everything is valid, processes
    /// the order by calling Order.Process.
    /// </summary>

    public partial class Billing : SiteTemplate
    {

        #region Variables
        ///<summary>The Url to navigate on Back button click (from web.config).</summary>
        protected static string _OnBackUrl;
        private bool IsPuPproduct;
        private bool IsWUproduct;
        private bool IsAZauto;
        private bool ISHLAuto;
        private string Payflag;
        ///<summary>The Url to navigate on Continue button click (from web.config).</summary>
        protected static string _OnContinueUrl = string.Empty;
        ///<summary>True if the bill-to information should be filled automatically.</summary>
        private static bool AutoFill = Config.bSetting("Billing_AutoFill");
        public int MaxLength = 25;
        public string paymentPlan { get; set; }
        public string billPlan { get; set; }
        //67811A0 - PCI Remediation for Payment systems CH21:START Declared Public variable _IsDuplicate
        public int _IsDuplicate;
        //67811A0 - PCI Remediation for Payment systems CH21:END Declared Public variable _IsDuplicate
        //Manage Recurring Section
        BillingLookUp BillingSummary = new BillingLookUp();
        IssueDirectPaymentWrapper Obj = new IssueDirectPaymentWrapper();
        //Manage Recurring Section

        protected InsuranceInfo Insurance;

        #endregion

        #region Controls

        ///<summary>Bill to name user control</summary>
        protected MSC.Controls.Name BillToName;
        ///<summary>Bill to address user control</summary>
        protected MSC.Controls.Address BillToAddress;
        ///<summary>Credit card entry control</summary>
        protected MSC.Controls.PaymentAccount Card;

        //05/03/2011 RFC#130547 PT_echeck Ch9: Added the reference echeck control to the billing page.
        ///<summary>eCheck entry control</summary>
        protected MSC.Controls.echeck echeck;

        // Added for the check number changes
        ///<summary>Check entry control</summary>
        protected MSC.Controls.Check Check;
        ///<summary>The pages nav buttons.</summary>
        protected MSC.Controls.Buttons btncontrol;

        ///<summary>Validator vldFirstName</summary>
        protected CSAAWeb.WebControls.Validator vldFirstName;
        ///<summary>Validator vldFirstNameAlpha</summary>
        protected Validator vldFirstNameAlpha;
        ///<summary>Validator vldName</summary>
        protected Validator vldName;
        ///<summary>Validator vldCheckDuplicatePolicy</summary>
        protected Validator vldCheckDuplicatePolicy;


        protected System.Web.UI.WebControls.TextBox HiddenField;
        protected System.Web.UI.WebControls.TextBox hdntxtdupPolicy;


        /// <summary>
        /// Added by Cognizant on 05/18/2004 for creating new HtmlTable trBilling
        /// </summary> 
        ///<summary>HtmlTable trBilling</summary>
        protected System.Web.UI.HtmlControls.HtmlTable trBilling;
        /// <summary>
        /// Added by Cognizant on 05/18/2004 for creating new dropdown(Payment Type)
        /// </summary> 
        ///<summary>PaymentType Control</summary>  
        public System.Web.UI.WebControls.DropDownList _PaymentType;
        /// <summary>
        /// Added by Cognizant on 05/18/2004 for creating new Validator(vldPaymentType)
        /// </summary> 
        ///<summary>PaymentType Control validator</summary> 
        protected CSAAWeb.WebControls.Validator vldPaymentType;

        protected MSC.Controls.PageValidator vldSumaary;
        protected CSAAWeb.WebControls.Validator vldminimuamount;

        #endregion

        #region Properties

        ///<summary>Billing first name.</summary>
        public string FirstName { get { return BillToName.FirstName; } set { BillToName.FirstName = value; } }
        ///<summary>Billing middle name.</summary>
        public string MiddleName { get { return BillToName.MiddleName; } set { BillToName.MiddleName = value; } }
        ///<summary>Billing last name</summary>
        public string LastName { get { return BillToName.LastName; } set { BillToName.LastName = value; } }

        /// <summary>
        /// Added by Cognizant on 05/18/2004 for creating a new PaymentType Property
        /// </summary> 
        ///<summary>Property to Get and Set the Value to the Payment Type</summary> />	
        public string PaymentType { get { return ListC.GetListValue(_PaymentType); } set { ListC.SetListIndex(_PaymentType, value); } }

        #endregion

        #region RegexExpressions

        // 67811A0 - PCI Remediation for Payment systems CH25:START- Creation of new Regular Expression to check the FirstName 
        ///  Creation of new Regular Expression to check the FirstName 
        /// </summary>
        //MAIG - CH1 - START - Modified the Regex pattern to include full stop and comma
        private static Regex regCheckName = new Regex("[^a-z '.,-]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //MAIG - CH1 - END - Modified the Regex pattern to include full stop and comma
        // 67811A0 - PCI Remediation for Payment systems CH25:END- Creation of new Regular Expression to check the FirstName 

        //67811A0 - PCI Remediation for Payment systems CH26:START Creation of new Regular Expression to check the Positive value
        private static Regex regDecimal = new Regex("(?:^\\-{0,1}[0-9]+\\.{0,1}[0-9]{0,4}$|^\\-?[0-9]*\\.?[0-9]{1,2}$)", RegexOptions.Compiled);
        //67811A0 - PCI Remediation for Payment systems CH26:END Creation of new Regular Expression to check the Positive value

        #endregion

        #region PropertyDeclaration

        //67811A0 - PCI Remediation for Payment systems CH2:START Get and Set property for Policy Number
        ///<summary>The policy number.</summary>
        public string strPolicy
        {
            get { return lblPolicyNumber.Text.Trim(); }
            set
            {
                lblPolicyNumber.Text = value;
                //Added by COGNIZANT for duplicate payment alert for SR#8434145
                // if (Amount > 0) dupPolicyHidden = Policy + "," + Convert.ToString(Amount);
            }
        }
        //67811A0 - PCI Remediation for Payment systems CH2:END Get and Set property for Policy Number

        ///<summary>Identity for product type.</summary>
        public string ProductType
        {
            get { return hdntxtProductCode.Text.Trim(); }
            set
            {
                hdntxtProductCode.Text = value;
            }
        }

        //67811A0 - PCI Remediation for Payment systems CH3:START Get and Set property for FirstName


        public string PayFirstName
        {
            get { return txtFirstName.Text; }
            set { txtFirstName.Text = value; }
        }
        //67811A0 - PCI Remediation for Payment systems CH3:END Get and Set property for FirstName

        //67811A0 - PCI Remediation for Payment systems CH4:START Get and Set property for LastName
        ///<summary>Policy holder's last name.</summary>
        public string PayLastName
        {
            get { return txtLastName.Text; }
            set { txtLastName.Text = value; }
        }
        //67811A0 - PCI Remediation for Payment systems CH4:END Get and Set property for LastName

        //67811A0 - PCI Remediation for Payment systems CH20:START -Added IsDuplicate Property which is used to hold the Value of duplicate Payment.
        ///<summary>Property which is used to hold the Value of duplicate Payment i.e either '0' or '1'</summary>
        public int IsDuplicate
        {
            get { return _IsDuplicate; }
            set { _IsDuplicate = value; }
        }
        // 67811A0 - PCI Remediation for Payment systems CH20:END -Added IsDuplicate Property which is used to hold the Value of duplicate Payment.



        //67811A0 - PCI Remediation for Payment systems CH22:START Added dupPolicyHidden Property which is used to store values of duplicate policy.
        public string dupPolicyHidden
        {
            get { return hdntxtdupPolicy.Text; }
            set { hdntxtdupPolicy.Text = value; }
        }
        //67811A0 - PCI Remediation for Payment systems CH22:END Added dupPolicyHidden Property which is used to store values of duplicate policy.

        // PUP ch3-added the column flag variable to the Payment type property and assigned the column default flag value to P by cognizant on 01/13/2011.
        public string ColumnFlag
        {
            get
            {
                if (Payflag == null)
                {
                    Payflag = "P";
                }
                return Payflag.ToString();
            }
            set
            {
                Payflag = value;
            }
        }

        #endregion

        #region Validations


        //67811A0 - PCI Remediation for Payment systems CH6:START Required Field validation for FirstName 
        // Added new function to validate fields for required field
        protected void ReqValCheck(Object source, ValidatorEventArgs e)
        {
            if (string.IsNullOrEmpty(txtFirstName.Text))
            {
                e.IsValid = false;
                vldFirstName.MarkInvalid();
                vldFirstName.ErrorMessage = Constants.FIRST_NAME_REQ;
            }

            if (string.IsNullOrEmpty(txtLastName.Text))
            {
                e.IsValid = false;
                Validator LastNameLabelValidator = (Validator)txtLastName.FindControl("LabelValidator");
                LastNameLabelValidator.MarkInvalid();
                LastNameLabelValidator.ErrorMessage = Constants.NAME_SEARCH_LAST_NAME_REQ;
            }

            if (string.IsNullOrEmpty(txtMailingZip.Text))
            {
                e.IsValid = false;
                vldMailingZip.MarkInvalid();
                vldMailingZip.ErrorMessage = Constants.MAILING_ZIP_CODE_REQ;
            }

            if (string.IsNullOrEmpty(_PaymentType.SelectedValue))
            {
                e.IsValid = false;
                vldPaymentType.MarkInvalid();
                vldPaymentType.ErrorMessage = Constants.PAYMENT_TYPE_REQ;
            }

            e.IsValid = (txtFirstName.Text != "" || false);
            if (!e.IsValid) vldFirstName.MarkInvalid();

        }

        //MAIG - CH2 - BEGIN - Validation for Zip Code
        protected void ReqValZipCheck(Object source, ValidatorEventArgs e)
        {
            e.IsValid = (CSAAWeb.Validate.IsValidZip(txtMailingZip.Text));
            if (string.IsNullOrEmpty(txtMailingZip.Text))
            {
                e.IsValid = true;
            }
            if (!e.IsValid)
            {
                Validator vldZip = (Validator)source;
                //MAIG - CH6 - BEGIN - Modified the verbiage of the error message
                vldZip.ErrorMessage = "Invalid Mailing Zip";
                //MAIG - CH6 - END - Modified the verbiage of the error message
                vldMailingZip.MarkInvalid();
            }

        }


        protected void ReqValEmailCheck(Object source, ValidatorEventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmailAddress.Text))
            {
                e.IsValid = (CSAAWeb.Validate.IsValidEmailAddress(txtEmailAddress.Text) || false);
                if (!e.IsValid)
                {
                    vldEmailAddress.MarkInvalid();
                    vldEmailAddress.ErrorMessage = "";// Constants.PC_ERR_INVALID_EMAILL_ADDR;
                    txtEmailAddress.Focus();
                }
            }

        }
        //MAIG - CH2 - END - Validation for Zip Code

        //END
        //67811A0 - PCI Remediation for Payment systems CH6:END Required Field validation for FirstName 

        //67811A0 - PCI Remediation for Payment systems CH12:START Required Field validation for Payment Type.
        // Added new function to validate fields for required field payment type
        protected void ReqValPaymentTypeCheck(Object source, ValidatorEventArgs e)
        {
            e.IsValid = (_PaymentType.SelectedValue != "" || false);
            if (!e.IsValid) vldPaymentType.MarkInvalid();

        }
        //END
        //67811A0 - PCI Remediation for Payment systems CH12:END Required Field validation for Payment Type.

        // 67811A0 - PCI Remediation for Payment systems CH9:START Required Field validation for amount entered in Other Amount textbox.
        //.NetMig.Ch3: Added function for checking for required field - START
        protected void ReqValCheckAmount(Object source, ValidatorEventArgs e)
        {
            //CHG0055954-AZ PAS Conversion and PC integration CH2-START Modified the below code to validate other amount field for the HL product type and excess payment for non exigen active status with balance policy
            if (rbnOtherAmount.Checked == true)
            {
                if (txtAmount.Text != "")
                {
                    float otherAmount = 0.0F, totalAmount = 0.0F, dueAmount = 0.0F;
                    string convertPolNumber = string.Empty, totalBalance, mindueBalance;
                    int policyLength = 0;
                    if (hdntxtConvertedPolicy.Text != null)
                    {
                        policyLength = hdntxtConvertedPolicy.Text.ToString().Length;
                        convertPolNumber = hdntxtConvertedPolicy.Text.ToString();

                    }
                    float.TryParse(txtAmount.Text.ToString(), out otherAmount);
                    totalBalance = lblTotalbalance.Text.ToString().Replace("$", "");
                    float.TryParse(totalBalance, out totalAmount);

                    if (Convert.ToDouble(otherAmount) > CSAAWeb.Constants.MAX_PAYMENT_AMOUNT)
                    {
                        e.IsValid = false;
                        vldExcessAmount.ErrorMessage = CSAAWeb.Constants.ERR_EXCEEDS_MAX_PAYMENT;
                        vldExcessAmount.MarkInvalid();
                    }
                    //MAIG - CH4 - END - Restricting Other amount to Max Payment Amount 
                }
                else
                {
                    e.IsValid = false;
                }
            }
            //CHG0055954-AZ PAS Conversion and PC integration CH2-END -Modified the below code to validate other amount field for the HL product type and excess payment for non exigen active status with balance policy
            else if (rbnTotalbalance.Checked == true)
            {
                e.IsValid = (lblTotalbalance.Text != Constants.PCI_NODUE || false);
            }
            else if (rbnMinimumdue.Checked == true)
            {
                //CHG0055954-AZ PAS Conversion and PC integration -CH3 -START Modified the below code to validate other amount field for the HL product type
                float totalAmount = 0.0F, dueAmount = 0.0F;
                float.TryParse(lblMinimumdue.Text.ToString().Replace("$", ""), out dueAmount);
                string totalBalance = lblTotalbalance.Text.ToString().Replace("$", "");
                float.TryParse(totalBalance, out totalAmount);
                //MAIG - CH5 - BEGIN - Removed the logic, since High Limit Product is removed 
                /*if ((dueAmount != totalAmount) && ProductType.ToString() == Config.Setting("ProductCode.HighLimit"))
                {

                    e.IsValid = false;
                    vldExcessAmount.MarkInvalid();
                } */
                //CHG0055954-AZ PAS Conversion and PC integration -CH3 -END - Modified the below code to validate other amount field for the HL product type
                //MAIG - CH5 - END - Removed the logic, since High Limit Product is removed 
                e.IsValid = (lblMinimumdue.Text != Constants.PCI_NODUE || false);

            }

            if (!e.IsValid) VldAmount.MarkInvalid();

        }
        // 67811A0 - PCI Remediation for Payment systems CH9:END Required Field validation for amount entered in Other Amount textbox.
        //.NetMig.Ch1 - END

        ///<summary>The amount of the payment.</summary>
        public decimal Value
        {
            get { return (txtAmount.Text != string.Empty && regDecimal.IsMatch(txtAmount.Text)) ? Convert.ToDecimal(txtAmount.Text) : 0; }
            set { txtAmount.Text = (value == 0) ? "" : value.ToString(); }
        }


        //67811A0 - PCI Remediation for Payment systems CH10:START Positive Amount validation for amount entered in Other Amount textbox. 
        ///<summary>Validator function insurse that amount is positive.</summary>
        protected void PositiveAmount(Object Source, ValidatorEventArgs e)
        {
            if (txtAmount.Text != "") e.IsValid = (!vldAmountDecimal.IsValid || Value > 0);
            //.NetMig.Ch2: Added new condition to mark amount as invalid
            if (!e.IsValid) VldAmount.MarkInvalid();
        }
        //67811A0 - PCI Remediation for Payment systems CH10:END Positive Amount validation for amount entered in Other Amount textbox. 


        // 67811A0 - PCI Remediation for Payment systems CH23:START validate whether mimimumdue is not zero.

        protected void vldminimumdue(Object Source, ValidatorEventArgs e)
        {
            if (lblMinimumdue.Text != Constants.PCI_NODUE && rbnMinimumdue.Checked == true)
            {
                e.IsValid = (MinimumdueValue > 0);
            }
            //.NetMig.Ch2: Added new condition to mark amount as invalid
            if (!e.IsValid) vldminimuamount.MarkInvalid();
        }


        public decimal MinimumdueValue
        {
            get { return (lblMinimumdue.Text != string.Empty && regDecimal.IsMatch(lblMinimumdue.Text.Replace("$", ""))) ? Convert.ToDecimal(lblMinimumdue.Text.Replace("$", "")) : 0; }
            set { lblMinimumdue.Text = (value == 0) ? "" : value.ToString(); }
        }

        // 67811A0 - PCI Remediation for Payment systems CH23:END validate whether mimimumdue is not zero.


        //67811A0 - PCI Remediation for Payment systems CH24:START - validate whether Totalbalance is not zero.
        protected void VldTotalbalance(Object Source, ValidatorEventArgs e)
        {
            if (lblTotalbalance.Text != Constants.PCI_NODUE && rbnTotalbalance.Checked == true)
            {
                e.IsValid = (Totalbalancevalue > 0);
            }
            //.NetMig.Ch2: Added new condition to mark amount as invalid
            if (!e.IsValid) vldTotalbalanceamount.MarkInvalid();
        }
        //67811A0 - PCI Remediation for Payment systems CH24:END - validate whether Totalbalance is not zero.


        public decimal Totalbalancevalue
        {
            get { return (lblTotalbalance.Text != string.Empty && regDecimal.IsMatch(lblTotalbalance.Text.Replace("$", ""))) ? Convert.ToDecimal(lblTotalbalance.Text.Replace("$", "")) : 0; }
            set { lblTotalbalance.Text = (value == 0) ? "" : value.ToString(); }
        }


        //67811A0 - PCI Remediation for Payment systems CH11:START validate whether amount entered in Other Amount textbox doesn't contain numeric and special characters.
        ///<summary>Validator function insurse that amount is positive.</summary>
        protected void WholeCents(Object Source, ValidatorEventArgs e)
        {
            if (txtAmount.Text != "") e.IsValid = (regDecimal.IsMatch(txtAmount.Text));
            //.NetMig.Ch2: Added new condition to mark amount as invalid
            if (!e.IsValid)
            {
                VldAmount.MarkInvalid();
            }
        }
        //67811A0 - PCI Remediation for Payment systems CH11:END validate whether amount entered in Other Amount textbox doesn't contain numeric and special characters.


        //67811A0 - PCI Remediation for Payment systems CH7:START- validate whether FirstName doesn't contain numeric and special characters.
        ///<summary> Creation of Name check validates that the First Name has only alpha, digits, space or '</summary>
        protected void CheckFirstName(object source, ValidatorEventArgs e)
        {
            e.IsValid = !regCheckName.IsMatch(txtFirstName.Text);
            if (!e.IsValid) vldFirstName.MarkInvalid();
            Validator vldFirst = (Validator)source;
            //MAIG - CH6 - BEGIN - Modified the verbiage of the error message
            vldFirst.ErrorMessage = "Invalid First Name";
            //MAIG - CH6 - END - Modified the verbiage of the error message
        }
        //END
        #endregion

        #region RestorePageData

        /// <summary>
        /// Fills the BillTo from the Address the first time, then from the
        /// BillTo property of the order, and the Card from the Card property 
        /// of the order.
        /// </summary>
        protected override void RestorePageData()
        {
            if (Order.NoBillTo)
            {
                if (Order.Products["Membership"] != null)
                {
                    AddressInfo A = (Order.Addresses.Giftgiver != null) ? Order.Addresses.Giftgiver : Order.Addresses.Household;
                    A.CopyTo(this);
                    A.CopyTo(BillToAddress);

                    if (!AutoFill)
                    {
                        BillToAddress.Address1 = "";
                        BillToAddress.Zip = "";
                    }
                }
                else if (Order.Products["Insurance"] != null)
                {
                    ((InsuranceInfo)Order.Products["Insurance"]).Lines[0].CopyTo(this);
                }
            }
            else
            {
                //67811A0 - PCI Remediation for Payment systems CH29 Start: Clear CC details if a new Policy is selected
                if (Context.Items["NewPolicy"] != null && Context.Items["NewPolicy"] == "Y")
                {
                    ((InsuranceInfo)Order.Products["Insurance"]).Lines[0].CopyTo(this);
                    Context.Items["NewPolicy"] = "";
                    //CHG0109406 - CH1 - BEGIN - Added the below code and condition to set the View state if the policy number is modified and set the Credit card First and Last Name from the current policy than setting it from the old Policy
                    ViewState["PolicyModified"] = "true";
                }
                else
                {
                    if (ViewState["PolicyModified"] != null)
                    {
                        ((InsuranceInfo)Order.Products["Insurance"]).Lines[0].CopyTo(this);
                        //CHG0110069 - CH1 - BEGIN - Modified the below logic to clear the old CC/EC details when the poicy number is modified in the Duplicate Policy Scenario
                        //Set the CC/EC values to null
                        Order.Card = null;
                        Order.echeck = null;
                    }
                    else
                    {
                        Order.Addresses.BillTo.CopyTo(this);
                        Order.Addresses.BillTo.CopyTo(BillToAddress);
                        //Set the Credit card Number and Expiration details to Empty. All the other details will be retained. 
                        Order.Card.CCExpMonth = string.Empty;
                        Order.Card.CCExpYear = string.Empty;
                        Order.Card.CCNumber = string.Empty;

                        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Added code to clear the ACH Routing number and Account number - VA Defect ID - 216  - Begin
                        Order.echeck.BankAcntNo = string.Empty;
                        Order.echeck.BankId = string.Empty;
                        Order.echeck.BankAcntType = string.Empty;
                        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Added code to clear the ACH Routing number and Account number - VA Defect ID - 216  - End

                        //CHG0109406 - CH1 - END - Added the below code and condition to set the View state if the policy number is modified and set the Credit card First and Last Name from the current policy than setting it from the old Policy
                        Order.Card.CopyTo(this);
                        Order.Card.CopyTo(Card);

                        //RFC#130547 PT_echeck Ch2: Added the condition to restore the echeck details in the echeck object for the echeck payment type by cognizant on 05/03/2011.
                        Order.echeck.CopyTo(this);
                        Order.echeck.CopyTo(echeck);
                    }
                    //CHG0110069 - CH1 - END - Modified the below logic to clear the old CC/EC details when the poicy number is modified in the Duplicate Policy Scenario
                    //START Added by Cognizant on  05/21/2004 not to assign the PaymentType on postback
                    if (!IsPostBack)
                    {
                        PaymentType = Order.Detail.PaymentType.ToString();
                    }
                    //END
                }
                //67811A0 - PCI Remediation for Payment systems CH29  End: Clear CC details if a new Policy is selected
            }
        }

        #endregion

        #region DataManagement
        /// <summary>
        /// Stores the BillTo and Card information in the appropriate properties 
        /// of the order.
        /// </summary>
        protected override void SavePageData()
        {
            if (Context.Items["billplan"] == null)
            {
                Context.Items.Add("billplan", hdnBillPlan.Text);
            }
            if (Context.Items["PaymentPlan"] == null)
            {
                Context.Items.Add("PaymentPlan", hdnPaymentPlan.Text);
            }
            if (Context.Items["autoPay"] == null)
            {
                Context.Items.Add("autoPay", hdnAutoPay.Text);
            }
            //MAIG - CH8 - BEGIN - Added the code to pass the Company Code and Source System,RpbsDetails
            if (Context.Items["SourceCompanyCode"] == null)
            {
                Context.Items.Add("SourceCompanyCode", HiddenPolicyDetail.Text);
            }
            if (Context.Items["RpbsBillingDetails"] == null)
            {
                Context.Items.Add("RpbsBillingDetails", HiddenRpbsBillingDetails.Text);
            }
            if (Context.Items["NameSearchData"] == null)
            {
                Context.Items.Add("NameSearchData", HiddenNameSearchDetails.Text);
            }
            if (Context.Items["DuplicatePolicyData"] == null)
            {
                Context.Items.Add("DuplicatePolicyData", HiddenDuplicatePolicyDetails.Text);
            }

            //CHG0116140 - Payment Restriction - BEGIN - Add PaymentRestriction to the Context from hidden field
            if (Context.Items["PaymentRestriction"] == null)
            {
                Context.Items.Add("PaymentRestriction", hdnPayFlag.Text);
            }
            //CHG0116140 - Payment Restriction - END - Add PaymentRestriction to the Context from hidden field

            //MAIG - CH8 - END - Added the code to pass the Company Code and Source System,RpbsDetails

            if (Order.NoBillTo) Order.Addresses.Add(AddressInfoType.BillTo);
            if (PaymentType == ((int)PaymentClasses.PaymentTypes.CreditCard).ToString())
            {
                Order.Card.CopyFrom(this);
                Order.Card.CopyFrom(Card);
            }
            //RFC#130547 PT_echeck Ch1:Start Added the condition to store the echeck details in the echeck object for the echeck payment type by cognizant on 05/03/2011
            else if (PaymentType == ((int)PaymentClasses.PaymentTypes.ECheck).ToString())
            {
                Order.echeck.CopyFrom(this);
                Order.echeck.CopyFrom(echeck);
                if (BillToName.FirstName.Trim() != "" && BillToName.LastName.Trim() != "")
                {
                    Order.echeck.CustomerName = BillToName.FirstName.Trim() + " " + BillToName.LastName.Trim();
                }
                else
                {
                    Order.echeck.CustomerName = "";
                }
            }
            //RFC#130547 PT_echeck Ch1:End Added the condition to store the echeck details in the echeck object for the echeck payment type by cognizant on 05/3/2011 


            //67811A0 - PCI Remediation for Payment systems CH17:START save selected amount,FirstName, Lastname to insurance line item.
            if ((InsuranceInfo)Order.Products["Insurance"] != null)
            {
                Insurance = (InsuranceInfo)Order.Products["Insurance"];
                Insurance.Lines[0].Tax_Amount = 0;
                if (rbnTotalbalance.Checked)
                {
                    Insurance.Lines[0].Price = Convert.ToDecimal(lblTotalbalance.Text.Replace("$", ""));
                    //Saving to context to retrin Radio button selection on page navigation

                    if (Context.Items["AmountSelected"] != null)
                    {
                        Context.Items["AmountSelected"] = "T";
                    }
                    else
                    {
                        Context.Items.Add("AmountSelected", "T");
                    }
                }
                else if (rbnMinimumdue.Checked)
                {
                    Insurance.Lines[0].Price = Convert.ToDecimal(lblMinimumdue.Text.Replace("$", ""));
                    //Saving to context to retrin Radio button selection on page navigation
                    if (Context.Items["AmountSelected"] != null)
                    {
                        Context.Items["AmountSelected"] = "M";
                    }
                    else
                    {
                        Context.Items.Add("AmountSelected", "M");
                    }
                }
                else if (rbnOtherAmount.Checked)
                {
                    if (txtAmount.Text != "")
                    {
                        Insurance.Lines[0].Price = Convert.ToDecimal(txtAmount.Text);
                    }

                    //Saving to context to retrin Radio button selection on page navigation                    
                    if (Context.Items["AmountSelected"] != null)
                    {
                        Context.Items["AmountSelected"] = "O";
                    }
                    else
                    {
                        Context.Items.Add("AmountSelected", "O");
                    }
                }
                Insurance.Lines[0].FirstName = txtFirstName.Text;
                Insurance.Lines[0].LastName = txtLastName.Text;

                if (Context.Items["DueDetails"] != null)
                {
                    Context.Items["DueDetails"] = txtFirstName.Text + "|" +
                                     txtLastName.Text + "|" +
                                     lblDuedate.Text + "|" +
                                     lblTotalbalance.Text.Replace("$", "") + "|" +
                                     lblMinimumdue.Text.Replace("$", "") + "|" +
                                      txtAmount.Text + "|" + HidddenStatePrefix.Text;
                }
                else
                {
                    Context.Items.Add("DueDetails", txtFirstName.Text + "|" +
                                    txtLastName.Text + "|" +
                                    lblDuedate.Text + "|" +
                                    lblTotalbalance.Text.Replace("$", "") + "|" +
                                    lblMinimumdue.Text.Replace("$", "") + "|" +
                                     txtAmount.Text + "|" + HidddenStatePrefix.Text);

                }
                // CHG0113938 - BEGIN - Add/Update Policy Status Description to Context variable.
                if (Context.Items[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION] != null)
                {
                    Context.Items[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION] = lblPolicyStatus.Text;
                }
                else
                {
                    Context.Items.Add(CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION, lblPolicyStatus.Text);
                }
                // CHG0113938 - END - Add/Update Policy Status Description to Context variable.
                //MAIG - CH9 - BEGIN - Modified the logic to get the Policy State and Prefix and assign it to Order Object & Update the CompanyCode and Source System to OrderDetail Object
                if (Context.Items["StoreFields"] != null)
                {
                    Context.Items["StoreFields"] = txtMailingZip.Text + "|" + txtEmailAddress.Text;
                }
                else
                {
                    Context.Items.Add("StoreFields", txtMailingZip.Text + "|" + txtEmailAddress.Text);
                }
                // Only for Western United Products - Start
                if (HidddenStatePrefix.Text.ToString().Trim() != "")//&& (ConfigurationSettings.AppSettings["PCP.Products"]).IndexOf(Convert.ToString(hdntxtProductCode.Text.Trim())) > -1 )
                {
                    //Company Code fix START- Append the Company ID along with the Policy state in the Order.Detail information
                    string[] arrStatePrefix = new string[2];
                    {
                        arrStatePrefix = HidddenStatePrefix.Text.ToString().Split('|');
                    }
                    Order.Detail.PolicyPrefix = arrStatePrefix[0];
                    Order.Detail.PolicyState = arrStatePrefix[1];//+"-"+arrStatePrefix[2];
                    //Company Code fix END- Append the Company ID along with the Policy state in the Order.Detail information
                }
                // Only for Western United Products - End
                //CHG0055954-AZ PAS Conversion and PC integration CH3 - start - Added the below code to add the check number and the company code value to the policy state and policy prefix for non PCP transactions.
                if (_PaymentType.Text == "4")
                {
                    /* string[] arrStatePrefix = new string[3];
                     arrStatePrefix = HidddenStatePrefix.Text.ToString().Split('|');
                     if (arrStatePrefix[0] != null)
                     {
                         Order.Detail.PolicyPrefix = arrStatePrefix[0];
                     } */
                    //CompanyCode|SourceSystem
                    Insurance.Lines[0].SubProduct = Check.CheckNumber.ToString() + "-" + HiddenPolicyDetail.Text.ToString();
                }
                else
                {
                    Insurance.Lines[0].SubProduct = "-" + HiddenPolicyDetail.Text.ToString();
                }

                /*else
                {
                    string[] arrStatePrefix = new string[3];
                    arrStatePrefix = HidddenStatePrefix.Text.ToString().Split('|');
                    Order.Detail.PolicyPrefix = arrStatePrefix[0];
                }*/
                //CHG0055954-AZ PAS Conversion and PC integration CH3 - end- Added the below code to add the check number and the company code value to the policy state and policy prefix for non PCP transactions.
                //MAIG - CH9 - END - Modified the logic to get the Policy State and Prefix and assign it to Order Object & Update the CompanyCode and Source System to OrderDetail Object

                //MAIG - CH10 - BEGIN - Added the logic to get the Email Address, Mailing Zip and assign it to Order Object. 
                if (Insurance.Lines[0].RevenueType.Equals(CSAAWeb.Constants.PC_REVENUE_DOWN))
                {
                    AddressInfo DownAddress = new AddressInfo();
                    if (!string.IsNullOrEmpty(txtEmailAddress.Text))
                    {
                        DownAddress.Email = txtEmailAddress.Text.Trim();
                    }
                    if (!string.IsNullOrEmpty(txtMailingZip.Text))
                    {
                        DownAddress.Zip = txtMailingZip.Text.Trim();
                    }
                    DownAddress.SkipValidation = true;
                    Insurance.Addresses = new ArrayOfAddressInfo();
                    Insurance.Addresses.Add(DownAddress);
                }
                else
                {
                    AddressInfo DownAddress = new AddressInfo();
                    if (!string.IsNullOrEmpty(txtEmailAddress.Text))
                    {
                        DownAddress.Email = txtEmailAddress.Text.Trim();
                    }
                    DownAddress.SkipValidation = true;
                    Insurance.Addresses = new ArrayOfAddressInfo();
                    Insurance.Addresses.Add(DownAddress);
                }
                //MAIG - CH10 - END - Added the logic to get the Email Address, Mailing Zip and assign it to Order Object. 

            }

            //67811A0 - PCI Remediation for Payment systems CH17:END save selected amount,FirstName, Lastname to insurance line item.


            //START Added by Cognizant on 05/21/2004 to assign the attribute PaymentType with the Selected item in the combo            
            if (PaymentType != "")
                Order.Detail.PaymentType = Convert.ToInt32(PaymentType);
            //Added by Cognizant on 08/21/2004
            else
                Order.Detail.PaymentType = 0;
            //END

            if (Card.AuthCode != "") Order.Detail.AuthCode = Card.AuthCode;
            AddressInfo B = Order.Addresses.BillTo;
            B.CopyFrom(this);
            B.CopyFrom(BillToAddress);
            //START Changed by Cognizant on  05/19/2004 to assign the First,Last Name if the PaymentType!=Online Credit Card
            //RFC#130547 PT_echeck Ch3:Modified the condition to tocheck the payment type is not echeck payment type by cognizant on 05/03/2011
            if (((Order.Detail.PaymentType != (int)PaymentClasses.PaymentTypes.CreditCard) && (Order.Detail.PaymentType != (int)PaymentClasses.PaymentTypes.ECheck)) && Order.Products["Insurance"] != null)
            {
                ((InsuranceInfo)Order.Products["Insurance"]).Lines[0].CopyTo(B);
            }
            //END
            if (Order.Products["Membership"] != null)
            {
                AddressInfo A = (Order.Addresses.Giftgiver != null) ? Order.Addresses.Giftgiver : Order.Addresses.Household;
                B.DayPhone = A.DayPhone;
                B.EveningPhone = A.EveningPhone;
                B.Email = A.Email;
                //RFC#130547 PT_echeck Ch4:Modified the condition to tocheck the payment type is not echeck payment type by cognizant on 05/03/2011
                if ((Order.Detail.PaymentType != (int)PaymentClasses.PaymentTypes.CreditCard) && (Order.Detail.PaymentType != (int)PaymentClasses.PaymentTypes.ECheck))
                {
                    B.FirstName = A.FirstName;
                    B.LastName = A.LastName;
                    B.Address1 = A.Address1;
                    B.MiddleName = A.MiddleName;
                    B.Zip = A.Zip;
                }
                //START Changed by Cognizant on  05/19/2004 to assign the First,Last Name Address and Zip if the PaymentType!=Online Credit Card
                if (Order.Detail.PaymentType != (int)PaymentClasses.PaymentTypes.CreditCard)
                {
                    B.FirstName = A.FirstName;
                    B.LastName = A.LastName;
                    B.Address1 = A.Address1;
                    B.MiddleName = A.MiddleName;
                    B.Zip = A.Zip;
                }
                //END
            }
            //67811A0 - PCI Remediation for Payment systems CH27:START- Commented since the code is no longer used.
            // B.Address2 = B.Address2;
            //67811A0 - PCI Remediation for Payment systems CH27:END- Commented since the code is no longer used.
        }

        /// <summary>
        /// Attempts to process the order.
        /// </summary>
        public override bool ProcessPage()
        {
            SavePageData();
            //START Changed by Cognizant on  05/18/2004 for assigning the MerchantRefNum only when CreditCard selected
            //if (Order.Detail.MerchantRefNum == "" && Order.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.CreditCard)
            //RFC#130547 PT_echeck Ch5:Modified the condition to check the payment type is echeck payment type by cognizant on 05/03/2011
            if (Order.Detail.MerchantRefNum == "" && ((Order.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.CreditCard) || (Order.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.ECheck)))
            //if (Order.Detail.MerchantRefNum == "" && Order.Detail.PaymentType == Convert.ToInt32(Config.Setting("PaymentID.CreditCard")))  
            {
                Order.Detail.MerchantRefNum = (this.OrderService.GetReference());

            }
            //END

            Context.Items.Add("PaymentTypeDescription", _PaymentType.SelectedItem.Text);
            Context.Items.Add("CardTypeDescription", ((DropDownList)Card.FindControl("_CardType")).SelectedItem.Text);
            //RFC#130547 PT_echeck Ch6:Added the condition to check the payment type is echeck and getting the payment account type control by cognizant on 05/03/2011
            if (PaymentType == ((int)PaymentClasses.PaymentTypes.ECheck).ToString())
            {
                Context.Items.Add("CheckAccountTypeDescription", ((DropDownList)echeck.FindControl("_AccountType")).SelectedItem.Text);
            }
            //Added the below code to store the check number when the payment type is Check
            if (PaymentType == ((int)PaymentClasses.PaymentTypes.Check).ToString())
            {
                Context.Items.Add("CheckNumber", ((TextBox)Check.FindControl("ChkNumber")).Text);
            }
            // CHGXXXXXXJ - CH2 - Changes made for splitting the token service - 27/06/2017- Begin
            if ((PaymentType == ((int)PaymentClasses.PaymentTypes.CreditCard).ToString()) || (PaymentType == ((int)PaymentClasses.PaymentTypes.ECard).ToString()))//validate for ACH payments
            {
                Context.Items.Add("IsPaymentTokenGenerated", "No");
                bool Result = Order.Process();
            }
            // CHGXXXXXXJ - CH2 - Changes made for splitting the token service - 27/06/2017- end
            //Put the Billing Backurl in the Context and transfer to Payment Confirmation page.
            if (ViewState["Billing_BackUrl"] != null)
                Context.Items["Billing_BackUrl"] = ViewState["Billing_BackUrl"].ToString();

            return true;
            //END

        }

        private void LoadPaymentDetail()
        {

            string strAmountSelected = string.Empty;
            DateTime duedate;
            decimal minidue;
            decimal Totbalance;
            string strLineDescription = string.Empty;
            string strnewProductDescription = string.Empty;
            DataTable dt = new DataTable();
            string strpolicynum = string.Empty;
            if (!Page.IsPostBack)
            {


                if ((InsuranceInfo)Order.Products["Insurance"] != null)
                {
                    //MAIG - CH11 - BEGIN - Updated the array length from 7 to 8 for getting the PolicyState and Prefix for all policies. 
                    //Company Code fix - modified 6 to 7
                    string[] arrDueDetails = new string[8];
                    //MAIG - CH11 - END - Updated the array length from 7 to 8 for getting the PolicyState and Prefix for all policies. 
                    if (Context.Items["DueDetails"] != null)
                    {
                        arrDueDetails = Context.Items["DueDetails"].ToString().Split('|');
                    }
                    //CHG0113938 - BEGIN - Display Policy Status information in Billing summary.
                    if (Context.Items[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION] != null)
                    {
                        lblPolicyStatus.Text = Convert.ToString(Context.Items[CSAAWeb.Constants.PC_BILL_STATUS_DESCRIPTION]);
                    }
                    //CHG0113938 - END - Display Policy Status information in Billing summary.

                    Insurance = (InsuranceInfo)Order.Products["Insurance"];

                    lblPolicyNumber.Text = (Insurance.Lines[0].Policy);
                    hdntxtProductCode.Text = (Insurance.Lines[0].ProductTypeNew);
                    strpolicynum = lblPolicyNumber.Text.ToString();

                    // Set ProductType = ProductTypeNew
                    // This is to handle Product Override from First Screen.
                    // If Overridden from First screen, only ProductTypeNew will have latest value.
                    if (Insurance.Lines[0].ProductTypeNew.ToString() != Insurance.Lines[0].ProductType.ToString())
                    {
                        //Product Overridden in First Screen
                        //Logic to change Description and Product Type
                        Insurance.Lines[0].ProductType = Insurance.Lines[0].ProductTypeNew;
                    }


                    DataTable DtRevType;
                    if (Cache["INS_Revenue_Type"] == null)
                    {
                        DtRevType = ((SiteTemplate)Page).OrderService.LookupDataSet("Insurance", "RevenueTypes").Tables["INS_Revenue_Type"];
                        Cache["INS_Revenue_Type"] = DtRevType;
                    }

                    DataTable dtInsSummary = Insurance.Lines.Data;
                    DataRow[] drDesc;


                    foreach (DataRow dr in dtInsSummary.Rows)
                    {
                        drDesc = ((DataTable)Cache["INS_Product_Type"]).Select("ID = '" + dr["ProductType"] + "'");
                        strnewProductDescription = drDesc[0]["Description"].ToString();
                        lblProductType.Text = strnewProductDescription;
                        //MAIG - CH12 - BEGIN - Updated the index to [0][3] to [0][0]
                        hdntxtProductType.Text = drDesc[0][0].ToString();
                        //MAIG - CH12 - END - Updated the index to [0][3] to [0][0]
                        drDesc = ((DataTable)Cache["INS_Revenue_Type"]).Select("ID = '" + dr["RevenueType"] + "'");
                        strLineDescription = drDesc[0]["Description"].ToString();
                        strLineDescription += (" Policy #" + Insurance.Lines[0].Policy);
                        Insurance.Lines[0].Description = strnewProductDescription + " " + strLineDescription;
                    }

                    if (arrDueDetails[0] != "" && arrDueDetails[0] != null)
                    {
                        //First Name
                        txtFirstName.Text = Convert.ToString(arrDueDetails[0].Trim());
                    }
                    else
                    {
                        txtFirstName.Text = "";
                    }

                    if (arrDueDetails[1] != "" && arrDueDetails[1] != null)
                    {
                        //Last Name
                        txtLastName.Text = Convert.ToString(arrDueDetails[1].Trim());
                    }
                    else
                    {
                        txtLastName.Text = "";
                    }

                    if (arrDueDetails[2] != "" && arrDueDetails[2] != null && arrDueDetails[2] != Constants.PCI_NODUE)
                    {
                        //Due date
                        duedate = Convert.ToDateTime(arrDueDetails[2].ToString());
                        if (duedate.ToShortDateString().ToString() == "1/1/1900")
                        {
                            lblDuedate.Text = Constants.PCI_NODUE;
                        }
                        else
                        {
                            lblDuedate.Text = duedate.ToShortDateString().ToString();
                        }
                    }
                    else
                    {
                        lblDuedate.Text = Constants.PCI_NODUE;
                    }

                    if (arrDueDetails[4] != "" && arrDueDetails[4] != null && arrDueDetails[4] != Constants.PCI_NODUE)
                    {
                        //Minimum Due Amount
                        minidue = Convert.ToDecimal(arrDueDetails[4].ToString());
                        lblMinimumdue.Text = "$" + Math.Round(minidue, 2).ToString();

                    }
                    else
                    {
                        lblMinimumdue.Text = Constants.PCI_NODUE;

                    }


                    if (arrDueDetails[3] != "" && arrDueDetails[3] != null && arrDueDetails[3] != Constants.PCI_NODUE)
                    {
                        //Total Balance
                        Totbalance = Convert.ToDecimal(arrDueDetails[3].ToString());
                        lblTotalbalance.Text = "$" + Math.Round(Totbalance, 2).ToString();

                    }
                    else
                    {
                        lblTotalbalance.Text = Constants.PCI_NODUE;
                    }

                    if (arrDueDetails[5] != "" && arrDueDetails[5] != null)
                    {
                        //Other Amount
                        txtAmount.Text = Convert.ToString(arrDueDetails[5]);
                    }
                    else
                    {
                        txtAmount.Text = "";
                    }

                    // Only for Western United Products - Start
                    // Remove comments once SIS look up is ready
                    //MAIG - CH13 - BEGIN - Updated the logic to get the PolicyState and Prefix for all policies. 

                    string[] arrEmailZip = new string[2];
                    if (Context.Items["StoreFields"] != null)
                    {
                        arrEmailZip = Context.Items["StoreFields"].ToString().Split('|');
                    }
                    //Mailing Zip
                    if (arrEmailZip[0] != null && arrEmailZip[0] != "")
                    {
                        txtMailingZip.Text = arrEmailZip[0];
                    }
                    else
                    {
                        txtMailingZip.Text = "";
                    }

                    //Email Address
                    if (arrEmailZip[1] != null && arrEmailZip[1] != "")
                    {
                        txtEmailAddress.Text = arrEmailZip[1];
                    }
                    else
                    {
                        txtEmailAddress.Text = "";
                    }

                    if (arrDueDetails[6] != "" || arrDueDetails[7] != "")
                    {
                        HidddenStatePrefix.Text = Convert.ToString(arrDueDetails[6]) + "|" + Convert.ToString(arrDueDetails[7]);
                    }
                    else
                    {
                        HidddenStatePrefix.Text = "|";
                    }
                    
                    //MAIG - CH13 - END - Updated the logic to get the PolicyState and Prefix for all policies. 
                    if (Context.Items["AmountSelected"] != null)
                    //When navigating from confirmation page, the Radio button selection must be retained.
                    {
                        strAmountSelected = Context.Items["AmountSelected"].ToString();

                        if (strAmountSelected == "T")
                        {
                            rbnTotalbalance.Checked = true;
                            rbnMinimumdue.Checked = false;
                            if (lblMinimumdue.Text == Constants.PCI_NODUE || lblMinimumdue.Text == "$0.00")
                            {
                                rbnMinimumdue.Enabled = false;
                            }
                            rbnOtherAmount.Checked = false;
                            txtAmount.Enabled = false;
                        }
                        else if (strAmountSelected == "M")
                        {
                            rbnMinimumdue.Checked = true;
                            rbnTotalbalance.Checked = false;
                            if (lblTotalbalance.Text == Constants.PCI_NODUE || lblTotalbalance.Text == "$0.00")
                            {
                                rbnTotalbalance.Enabled = false;
                            }
                            rbnOtherAmount.Checked = false;
                            txtAmount.Enabled = false;
                        }
                        else
                        {
                            DefaultOtherAmount();
                        }
                    }
                    else
                    {
                        DefaultOtherAmount();
                    }

                }
                
                if (hdnAutoPay.Text == null)
                {
                    hdnAutoPay.Text = string.Empty;
                }
                if (string.IsNullOrEmpty(hdnPaymentPlan.Text))
                {
                    hdnPaymentPlan.Text = string.Empty;
                }
                //MAIG - CH15 - BEGIN - Commented/Modified the logic to display only the recurrign Enrolled Status
                if (((InsuranceInfo)Order.Products["Insurance"]).Lines[0].RevenueType.Equals(CSAAWeb.Constants.PC_REVENUE_DOWN))
                {
                    lblRecurringEnrolledAll.Text = "NO";
                    //CHG0113938 - BEGIN - Hide Policy Status information for Down payments.
                    tdPolicyStatus.Visible = false;
                    //CHG0113938 - END - Hide Policy Status information for Down payments.
                }
                else
                {
                    if (hdnPaymentPlan.Text.ToString().ToUpper().Equals("DIRECT"))
                    {
                        lblRecurringEnrolledAll.Text = "NO";
                    }
                    else if ((hdnPaymentPlan.Text.ToString().ToUpper().Equals("AUTO")) || hdnAutoPay.Text.ToUpper().Equals("TRUE"))
                    {
                        lblRecurringEnrolledAll.Text = "YES";
                       
                        //MAIG - CH15 - END - Modified the logic to display only the recurrign Enrolled Status
                    }
                }

            }

        }
        ///<summary/>
        protected void imgManageRecurring_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Response.Redirect(CSAAWeb.Constants.PC_ENROLL_REDIRECT_URL);
        }
        private void DefaultOtherAmount()
        {
            rbnMinimumdue.Checked = false;
            if (lblMinimumdue.Text == Constants.PCI_NODUE || lblMinimumdue.Text == "$0.00")
            {
                rbnMinimumdue.Enabled = false;
            }
            rbnTotalbalance.Checked = false;
            if (lblTotalbalance.Text == Constants.PCI_NODUE || lblTotalbalance.Text == "$0.00")
            {
                rbnTotalbalance.Enabled = false;
            }
            rbnOtherAmount.Checked = true;
            txtAmount.Enabled = true;
        }

        #endregion

        #region Web Form Designer generated code
        /// <summary/>
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

            //START Changed on 05/18/2004 for binding the PaymentType combo with the Dataset
            Initialize();

            //END
        }
        #endregion

        #region PageEvents



        /// <summary/>
        protected void Page_Load(object sender, System.EventArgs e)
        {



            //67811A0 - PCI Remediation for Payment systems CH30 START : Added to clear the cache of the page to prevent the page loading on back button hit after logout
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            //Response.Cache.SetNoStore();
            //67811A0 - PCI Remediation for Payment systems CH30 END : Added to clear the cache of the page to prevent the page loading on back button hit after logout
            // 67811A0 - PCI Remediation for Payment systems CH14:START Set order summary visibility to false since it is no longer needed.
            Control Summary_Control = this.FindControl("Summary");
            HtmlTable Summary_Table = (HtmlTable)Summary_Control.FindControl("SummaryTable");
            Summary_Table.Visible = false;
            // 67811A0 - PCI Remediation for Payment systems CH14:END Set order summary visibility to false since it is no longer needed.
            if (Context.Items["convertedPolicynumber"] != null)
            {
                hdntxtConvertedPolicy.Text = Context.Items["convertedPolicynumber"].ToString();
            }
            if (Context.Items["PaymentPlan"] != null)
            {
                hdnPaymentPlan.Text = Context.Items["PaymentPlan"].ToString();
            }
            if (Context.Items["billplan"] != null)
            {
                hdnBillPlan.Text = Context.Items["billplan"].ToString();

            }
            if (Context.Items["autoPay"] != null)
            {
                hdnAutoPay.Text = Context.Items["autoPay"].ToString();

            }
            //CHG0116140 - Payment Restriction - BEGIN - Add PaymentRestriction to the hidden field from Context
            if (Context.Items["PaymentRestriction"] != null)
            {
                hdnPayFlag.Text = Context.Items["PaymentRestriction"].ToString();
            }
            //CHG0116140 - Payment Restriction - END - Add PaymentRestriction to the hidden field from Context

            //Load payment information from Insurance Line item
            LoadPaymentDetail();
            //PCI - Post Production Fix start:CH1 Added the code to make the merchant reference number to null for the failed transactions on click of back button by cognizant on 02/22/2012
            if (Context.Items["Cc_Echeckfailed"] != null)
            {
                Order.Detail.MerchantRefNum = "";

            }
            //67811A0  - PCI Remediation for Payment systems CH30:Start Added the condition to check the contents in the context for clearing the credit card and expiry contents in billing screen as a part of VA sacn defect by cognizant on 01/05/2011.
            if (Context.Items["Ccfailed"] != null)
            {
                TextBox txtCcNum = (TextBox)Card.FindControl("_CardNumber");
                txtCcNum.Text = "";
                DropDownList dpListMonth = (DropDownList)Card.FindControl("_ExpireMonth");
                dpListMonth.ClearSelection();
                DropDownList dpListYear = (DropDownList)Card.FindControl("_ExpireYear");
                dpListYear.ClearSelection();

            }
            //67811A0  - PCI Remediation for Payment systems CH30:END Added the condition to check the contents in the context for clearing the credit card and expiry contents in billing screen as a part of VA sacn defect by cognizant on 01/05/2011.
            //START Modified by Cognizant on 01/07/2004 - Switch Case for Enabiling the Controls depend on the PaymentType			
            if (PaymentType == ((int)PaymentClasses.PaymentTypes.CreditCard).ToString())
            //if(PaymentType==Config.Setting("PaymentID.CreditCard").ToString())
            {

                //Payment Type Credit card
                BillToName.Visible = true;
                BillToAddress.Visible = true;
                Card.Visible = true;
                lblName.Text = "Name on the Card";
                trBilling.Visible = true;
                echeck.Visible = false;
                //Added the below code to make the visibilty for the check control
                Check.Visible = false;
                //67811A0 - PCI Remediation for Payment systems CH16:START Commented the below code setting pagevalidator visibility to false.
                // PageValidator1.Visible = false;
                //67811A0 - PCI Remediation for Payment systems CH16:END Commented the below code setting pagevalidator visibility to false.

                //CHG0072116 - PC Clear CC Details when user clicks back button in insurance page CH1:START -  clear the requrired field values if context items is null.
                if (Context.Items["IsCC"] != null)
                {
                    TextBox txtCcNum = (TextBox)Card.FindControl("_CardNumber");
                    txtCcNum.Text = string.Empty;
                    DropDownList dpListMonth = (DropDownList)Card.FindControl("_ExpireMonth");
                    dpListMonth.ClearSelection();
                    DropDownList dpListYear = (DropDownList)Card.FindControl("_ExpireYear");
                    dpListYear.ClearSelection();
                }
                //CHG0072116 - PC Clear CC Details when user clicks back button in insurance page CH1:END -  clear the requrired field values if context items is null.
            }
            //RFC#130547 PT_echeck Ch7: start Added the condition to check the payment type is echeck and making the echeck contols to be visible by cognizant on 05/03/2011
            else if (PaymentType == ((int)PaymentClasses.PaymentTypes.ECheck).ToString())
            {

                BillToName.Visible = true;
                BillToAddress.Visible = true;
                Card.Visible = false;
                echeck.Visible = true;
                ////Added the below code to make the visibilty for the check control
                Check.Visible = false;
                lblName.Text = "Name on the Account";
                trBilling.Visible = true;
                //67811A0 - PCI Remediation for Payment systems CH16:START Commented the below code setting pagevalidator visibility to false.
                // PageValidator1.Visible = false;
                //67811A0 - PCI Remediation for Payment systems CH16:END Commented the below code setting pagevalidator visibility to false.
            }
            //CHG0055954-AZ PAS Conversion and PC Integration - Added the below code to make check visibility to true if it is a check payment type
            else if (PaymentType == ((int)PaymentClasses.PaymentTypes.Check).ToString())
            {
                BillToName.Visible = false;
                BillToAddress.Visible = false;
                Card.Visible = false;
                echeck.Visible = false;
                Check.Visible = true;
                lblName.Visible = false;
                trBilling.Visible = true;


            }
            //CHG0055954-AZ PAS Conversion and PC Integration - Added the below code to make check visibility to true if it is a check payment type
            //RFC#130547 PT_echeck Ch7: End Added the condition to check the payment type is echeck and making the echeck contols to be visible by cognizant on 05/03/2011
            else
            {
                //For other Payment Types 
                BillToName.Visible = false;
                //67811A0 - PCI Remediation for Payment systems CH16:START Commented the below code setting pagevalidator visibility to false.
                // PageValidator1.Visible = false;
                //67811A0 - PCI Remediation for Payment systems CH16:END Commented the below code setting pagevalidator visibility to false.
                BillToAddress.Visible = false;
                Card.Visible = false;
                trBilling.Visible = false;
                echeck.Visible = false;
                // Added the below code to make the check control invisible
                Check.Visible = false;
            }
            if (_PaymentType.SelectedItem.Text == "Select One")
            {
                //Without selecting any item
                BillToName.Visible = false;
                BillToAddress.Visible = false;
                Card.Visible = false;
                // Added the below code to make the check control invisible
                Check.Visible = false;
                trBilling.Visible = false;
                vldSumaary.Visible = true;
            }
            //END

            //START Modified by Cognizant on 11/18/2004 - Hold the Back URL in the ViewState
            if ((Context.Items["OnBackUrl"] != null) && (!IsPostBack))
            {
                OnBackUrl = Context.Items["OnBackUrl"].ToString();
                ViewState["Billing_BackUrl"] = Context.Items["OnBackUrl"].ToString();
            }

            //This Context Holds the Billing BackUrl coming from Payment Confirmation page
            if (Context.Items["BillingBackUrl"] != null)
                OnBackUrl = Context.Items["BillingBackUrl"].ToString();

            //END

            if (rbnOtherAmount.Checked == true)
            {
                txtAmount.Enabled = true;
            }

            //67811A0 - PCI Remediation for Payment systems CH18:START- Set focus to Validation summary error message.
            StringBuilder bldr = new StringBuilder();
            bldr.Append("<script language='javascript'> ");
            bldr.Append("function setfocus(){");
            bldr.Append("if (document.all['vldSumaary_vs0'] != null) {");
            bldr.Append("document.all['vldSumaary_vs0'].scrollIntoView(true);} }");
            bldr.Append("</script>");

            Page.RegisterClientScriptBlock("setfocus", bldr.ToString());

            StringBuilder sb = new StringBuilder();
            sb.Append("<script language='javascript'> ");
            sb.Append("setfocus();");
            sb.Append("</script>");

            Page.RegisterStartupScript("dofocus", sb.ToString());

            //67811A0 - PCI Remediation for Payment systems CH18:END- Set focus to Validation summary error message.


        }



        //67811A0 - PCI Remediation for Payment systems CH15:END Added method to display Payment informations to be pre populated on screen load.

        /// <summary>
        /// Changed by Cognizant on 05/18/2004 for creating a new function Initialize
        /// </summary> 
        ///<summary>Initialize the PaymentType control  </summary> />	
        private void Initialize()
        {
            Order DataConnection = new Order(Page);
            DataTable dtbPaymentType;
            //MAIG - CH16 - BEGIN - Added State constraint for checking if the policy falls in the Risk state
            string[] Producttype = null;
            string polNumber = string.Empty;
            string riskStatesCashCheck = "CA,NV,UT";

            //CHG0116140 - Payment Restriction - BEGIN - Set the PaymentRestriction from the Context or Hidden field
            bool paymentRestriction = false;
            if (Context.Items["PaymentRestriction"] != null & Convert.ToString(Context.Items["PaymentRestriction"]).Length > 0)
                Payflag = Convert.ToString(Context.Items["PaymentRestriction"]);
            else if (Convert.ToString(hdnPayFlag.Text).Length > 0)
                Payflag = Convert.ToString(hdnPayFlag.Text);
            //CHG0116140 - Payment Restriction - END - Set the PaymentRestriction from the Context or Hidden field

            //MAIG - CH16 - END - Added State constraint for checking if the policy falls in the Risk state

            //PUP Ch1: Start Modified the input flag to the Get payment type method to filter credit card payment type alone for PUP policy and to display only credit card payment type for line items which contains atleast on PUP policy transaction modified by cognizant on 01/13/2011
            //PAS AZ Product Configuration Ch1:Start Modified the below conditions to display on credit card and e-check transactions for PAS AZ product by cognizant on 03/11/2012
            if (!Page.IsPostBack)
            {
                //test check

                if (Order != null)
                {
                    int count = ((InsuranceInfo)Order.Products["Insurance"]).Lines.Count;
                    if (count != 0)
                    {
                        Producttype = new string[count];
                        for (int i = 0; i < count; i++)
                        {
                            Producttype[i] = ((InsuranceInfo)Order.Products["Insurance"]).Lines[i].ProductType.ToString();
                        }
                        //MAIG - CH17 - BEGIN - Modified the logic to restrict the Payment Type 
                        string polState = string.Empty;
                        string[] arrDetail = { };
                        string[] arrDueDetails = { };
                        if (Context.Items["DownPaymentData"] != null)
                        {
                            arrDetail = Context.Items["DownPaymentData"].ToString().Split('-');
                            HiddenPolicyDetail.Text = Context.Items["DownPaymentData"].ToString();
                        }
                        else if (Context.Items["InstallmentData"] != null)
                        {
                            arrDetail = Context.Items["InstallmentData"].ToString().Split('-');
                            HiddenPolicyDetail.Text = Context.Items["InstallmentData"].ToString();
                        }
                        else if (Context.Items["SourceCompanyCode"] != null)
                        {
                            arrDetail = Context.Items["SourceCompanyCode"].ToString().Split('-');
                            HiddenPolicyDetail.Text = Context.Items["SourceCompanyCode"].ToString();
                        }
                        if (Context.Items["NameSearchData"] != null)
                        {
                            HiddenNameSearchDetails.Text = Context.Items["NameSearchData"].ToString();
                        }
                        if (Context.Items["DuplicatePolicyData"] != null)
                        {
                            HiddenDuplicatePolicyDetails.Text = Context.Items["DuplicatePolicyData"].ToString();
                        }
                        if (Context.Items["RpbsBillingDetails"] != null)
                        {
                            if (!(Context.Items["RpbsBillingDetails"].GetType().FullName.Equals("System.String")))
                            {
                                string hdndata = string.Empty;
                                foreach (var item in (Dictionary<string, string>)Context.Items["RpbsBillingDetails"])
                                {
                                    //MAIGEnhancement CHG0107527 - CH2 - BEGIN - Modified the below code to append the key/value pair with ^ than , which causes issue in Full Name field from RPBS response
                                    hdndata = hdndata + string.Format("{0}*{1}^", item.Key, item.Value);
                                    //MAIGEnhancement CHG0107527 - CH2 - END - Modified the below code to append the key/value pair with ^ than , which causes issue in Full Name field from RPBS response
                                    //CHG0116140 - Payment Restriction - BEGIN - CH1 - Check the Payment Restriction value from RPBS Billing Summary data and Payment Restriction flag
                                    if (item.Key.ToUpper().Equals(Constants.PC_BILL_PAYMENT_RESTRICTION))
                                    {
                                        if (item.Value.ToUpper().Equals("TRUE"))
                                            paymentRestriction = true;
                                    }
                                    //Payment Restriction - END - CHG0116140 - CH1 - Check the Payment Restriction value from RPBS Billing Summary data and Payment Restriction flag
                                }
                                HiddenRpbsBillingDetails.Text = hdndata;
                            }
                            else
                            {
                                HiddenRpbsBillingDetails.Text = Context.Items["RpbsBillingDetails"].ToString();
                                //CHG0116140 - Payment Restriction - BEGIN - CH2 - Check the Payment Restriction value from RPBS Billing Summary data and Payment Restriction flag
                                string[] items = Context.Items["RpbsBillingDetails"].ToString().Split('^');
                                foreach (var item in items)
                                {
                                    string[] data = item.Split('*');
                                    if (data.Length == 2 && data[0].Equals(Constants.PC_BILL_PAYMENT_RESTRICTION))
                                    {
                                        if (data[1].ToUpper().Equals("TRUE"))
                                            paymentRestriction = true;
                                    }
                                }
                                //CHG0116140 - Payment Restriction - END - CH2 - Check the Payment Restriction value from RPBS Billing Summary data and Payment Restriction flag
                            }
                        }
                        if (Context.Items["DueDetails"] != null)
                        {
                            arrDueDetails = Context.Items["DueDetails"].ToString().Split('|');

                            if (arrDueDetails[7] != "")
                            {
                                polState = Convert.ToString(arrDueDetails[7]);
                            }
                        }
                        for (int j = 0; j < count; j++)
                        {
                            polNumber = ((InsuranceInfo)Order.Products["Insurance"]).Lines[j].Policy.Trim();
                            if (((InsuranceInfo)Order.Products["Insurance"]).Detail != null)
                            {
                                polState = ((InsuranceInfo)Order.Products["Insurance"]).Detail.PolicyState;
                            }
                            //CHG0116140 - Payment Restriction - BEGIN - Set the PayFlag based on the User & Source System.
                            if (((InsuranceInfo)Order.Products["Insurance"]).Lines[j].RevenueType.Equals(CSAAWeb.Constants.PC_REVENUE_DOWN))
                            {
                                pnlDownPayment.Visible = true;
                                if ((Producttype[j].Equals(MSC.SiteTemplate.ProductCodes.PA.ToString()) && (polNumber.Length == 8)) ||
                                    (Producttype[j].Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()) &&
                                    (polNumber.Length == 8 || polNumber.Length == 7)))
                                {
                                    if (paymentRestriction)
                                        Payflag = "RP";//Restriction PSUsers - Flag for PSuser with Payment Restriction
                                    else
                                        Payflag = "P";

                                }
                                else
                                {
                                    if (paymentRestriction)
                                        Payflag = "RO";//Restriction Others - Flag for Other Users with Payment Restriction
                                    else
                                        Payflag = "U";
                                }
                            }
                            else if (((InsuranceInfo)Order.Products["Insurance"]).Lines[j].RevenueType.Equals(CSAAWeb.Constants.PC_REVENUE_INST))
                            {
                                pnlDownPayment.Visible = false;
                                string sourceSystem = (arrDetail.Length == 2) ? arrDetail[1] : "";
                                //MAIGEnhancement CHG0107527 - CH1 - BEGIN - Modified the below code by including a condition to allow Cash/Check for users having PS User role ir-respective of source system
                                bool isPsUser = false;
                                //Get the User information which is maintained accross applications via Ticket
                                string[] userData = ((System.Web.Security.FormsIdentity)(this.Page.User.Identity)).Ticket.UserData.Split(';');
                                if (userData.Length > 0)
                                {
                                    if (userData[0].ToLower().Contains("pss"))
                                    {
                                        isPsUser = true;
                                    }
                                }
                                //If isPsUser is True, then skip all the other conditions, else process the other conditions 
                                //CHG0113938 - BEGIN - Added condition to show cash & check for State 'CA', Product 'DF' & 'PU', Source 'PAS' policies .
                                if (isPsUser || (Producttype[j].Equals(MSC.SiteTemplate.ProductCodes.PA.ToString()) &&
                                  (((polNumber.Length >= 4 && polNumber.Length <= 13) && sourceSystem.Equals("PAS") &&
                                   riskStatesCashCheck.IndexOf(polState) >= 0)))
                                   || (Producttype[j].Equals(MSC.SiteTemplate.ProductCodes.HO.ToString()) &&
                                    (((polNumber.Length >= 4 && polNumber.Length <= 13) && (sourceSystem.Equals("PAS") || sourceSystem.Equals("HDES")) &&
                                   riskStatesCashCheck.IndexOf(polState) >= 0)))
                                   || (Producttype[j].Equals(MSC.SiteTemplate.ProductCodes.DF.ToString()) &&
                                    ((polNumber.Length == 13 && sourceSystem.Equals("PAS")) && riskStatesCashCheck.IndexOf(polState) >= 0))
                                   || (Producttype[j].Equals(MSC.SiteTemplate.ProductCodes.PU.ToString()) &&
                                    ((polNumber.Length == 13 && sourceSystem.Equals("PAS")) && riskStatesCashCheck.IndexOf(polState) >= 0))
                                    )
                                //CHG0113938 - END - Added condition to show cash & check for State 'CA', Product 'DF' & 'PU', Source 'PAS' policies .
                                //MAIGEnhancement CHG0107527 - CH1 - END - Modified the below code by including a condition to allow Cash/Check for users having PS User role ir-respective of source system
                                {
                                    if (paymentRestriction)
                                        Payflag = "RP";//Restriction PSUsers - Flag for PSuser with Payment Restriction
                                    else
                                        Payflag = "P";
                                }
                                else
                                {
                                    if (paymentRestriction)
                                        Payflag = "RO";//Restriction PSUsers - Flag for Other user with Payment Restriction
                                    else
                                        Payflag = "U";
                                }
                            }
                            if (Context.Items["PaymentRestriction"] == null)
                            {
                                Context.Items.Add("PaymentRestriction", Payflag);
                            }
                            hdnPayFlag.Text = Payflag;
                            //CHG0116140 - Payment Restriction - END - Set the PayFlag based on the User & Source System.

                            HideCreditcardTypes(polNumber, Producttype[j], HiddenPolicyDetail.Text.Split('-')[0]);
                        }

                    }
                }


                if (IsPuPproduct == true)
                {
                    lblWarning.Visible = true;
                    lblWarning.Text = CSAAWeb.Constants.PYMT_PUP;

                }
                
                //MAIG - CH17 - END - Modified the logic to restrict the Payment Type 
            }

            //CHG0116140 - Payment Restriction - BEGIN - Set the payment Restriction message to label.
            if (paymentRestriction)
            {
                lblPaymentRestrict.Text = Constants.PC_PAYMENT_RESTRICTION_ERROR_MSG1;
                lblPaymentRestrict.Visible = true;
            }
            //CHG0116140 - Payment Restriction - END - Set the payment Restriction message to label.
            dtbPaymentType = DataConnection.LookupDataSet("Payment", "GetPaymentType", new object[] { Convert.ToString(ColumnFlag.ToString()), Page.User.Identity.Name }).Tables["PAY_Payment_Type"];
            //PUP Ch1: END Modified the input flag to the Get payment type method to filter credit card payment type alone for PUP policy and to display only credit card payment type for line items which contains atleast on PUP policy transaction modified by cognizant on 01/13/2011
            if (dtbPaymentType.Rows.Count > 1)
            {
                DataRow drPaymentType = dtbPaymentType.NewRow();
                drPaymentType["Description"] = "Select One";
                dtbPaymentType.Rows.InsertAt(drPaymentType, 0);
            }
            DataConnection.Close();
            _PaymentType.DataSource = dtbPaymentType;
            _PaymentType.DataBind();

        }
        /// <summary>
        /// Added by Cognizant on 05/18/2004 for creating a new Event
        /// </summary> 
        ///<summary>_PaymentType_SelectedIndexChanged Event for PaymentType control </summary> />	
        protected void _PaymentType_SelectedIndexChanged(object sender, System.EventArgs e)
        {

            if (rbnOtherAmount.Checked == true)
            {
                txtAmount.Enabled = true;
            }

            //Copy the Firstname and LastName to the textboxes only if the Credit Card selected
            //START Added on 05/19/2004 to assign the First,Last Name Address and Zip if the PaymentType!=Online Credit Card
            //RFC#130547 PT_echeck Ch8: Modified the condition to restore the echeck details in the echeck object for the echeck payment type also by cognizant on 05/03/2011.		  
            if (PaymentType == ((int)PaymentClasses.PaymentTypes.CreditCard).ToString() || PaymentType == ((int)PaymentClasses.PaymentTypes.ECheck).ToString())
            {
                RestorePageData();
                if (_PaymentType.SelectedValue == "1")
                {
                    Control paymentDetails = this.FindControl("Card");
                    DropDownList cardType = (DropDownList)paymentDetails.FindControl("_CardType");
                    //MAIG - CH18 - BEGIN - Added method that hides teh AMEX and Discover Credit Card types
                    cardType.SelectedValue = "";
                    HideCreditcardTypes(lblPolicyNumber.Text, hdntxtProductType.Text, HiddenPolicyDetail.Text.Split('-')[0]);
                    //MAIG - CH18 - END - Added method that hides teh AMEX and Discover Credit Card types
                    if (cardType != null)
                    {
                        cardType.Focus();
                    }
                }
                if (_PaymentType.SelectedValue == "5")
                {
                    Control echeckDetails = this.FindControl("echeck");
                    DropDownList accType = (DropDownList)echeckDetails.FindControl("_AccountType");

                    if (accType != null)
                    {
                        accType.Focus();
                    }
                }
                //Added the below code to get focus on the check number if the check payment type is selected
                if (_PaymentType.SelectedValue == "4")
                {
                    Control paymentDetails = this.FindControl("Check");
                    TextBox CheckNumber = (TextBox)paymentDetails.FindControl("ChkNumber");
                    if (CheckNumber != null)
                    {
                        CheckNumber.Focus();
                    }
                }

            }

            //END

            // 67811A0 - PCI Remediation for Payment systems CH28:START- Check if the duplicate alert is checked on postback and then enable the continue button.
            CheckBox chkDuplicate = (CheckBox)Page.FindControl("chkDuplicate");
            Control Button_Control = this.FindControl("btncontrol");
            ImageButton Continuebutton = (ImageButton)Button_Control.FindControl("ContinueButton");

            if (chkDuplicate != null)
            {
                if (chkDuplicate.Checked == true && chkDuplicate.Visible == true)
                    Continuebutton.Enabled = true;
                else if (chkDuplicate.Checked == false && chkDuplicate.Visible == true)
                    Continuebutton.Enabled = false;
            }


            // 67811A0 - PCI Remediation for Payment systems CH28:END- Check if the duplicate alert is checked on postback and then enable the continue button.
        }
        //MAIG - CH19 - BEGIN - Added method that hides the AMEX and Discover Credit Card types
        public void HideCreditcardTypes(string policy, string type, string sourceSystem)
        {
            //string ProductType = string.Empty;
            //string CompanyCode = HiddenPolicyDetail.Text.Split('-')[0];
            //if (((InsuranceInfo)Order.Products["Insurance"]).Lines.Count > 0) { ProductType = ((InsuranceInfo)Order.Products["Insurance"]).Lines[0].ProductType; }
            try
            {
                Control paymentDetails = this.FindControl("Card");
                DropDownList cardType = (DropDownList)paymentDetails.FindControl("_CardType");
                if (!(policy.Trim().Length == 8 && (type.Equals(MSC.SiteTemplate.ProductCodes.PA.ToString())
                    || type.Equals(MSC.SiteTemplate.ProductCodes.HO.ToString())))
                    && !(sourceSystem.ToUpper().Equals("KIC")))
                {
                    for (int i = cardType.Items.Count - 1; i >= 1; i--)
                    {
                        ListItem row = cardType.Items[i];
                        if (Config.Setting("CreditCardTypesDisabled").ToString().IndexOf(row.Value.ToString()) < 0)
                        {
                            continue;
                        }
                        else
                        {
                            cardType.Items.RemoveAt(i);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                CSAAWeb.AppLogger.Logger.Log("Exception occurred in HideCreditcardTypes method" + ex.ToString());
            }
        }
        //MAIG - CH19 - END - Added method that hides the AMEX and Discover Credit Card types
        #endregion


    }
}
