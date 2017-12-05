/* SSO-Integration.Ch1 : Added new constant for handling error messages in SSO integration by Cognizant on 10/11/2010 
 * 01/19/2011 PUP Ch1:Modified by cognizant PUP_CHAR_VALIDATIONS error message for Last seven character validation in Insurance serializers.;
 *3/18/2011 PUP Ch2:Modified the error valiables and the error message description by cognizant
 * 67811A0  - PCI Remediation for Payment systems CH1: Added the constants during PCI implementation.
 * ///RFC 185138 - AD Integration - CH1 START - Added the below constants
 * Refund in UI - Declared the below constants.
 * Security Defect - Declared the below constants.
 * CHG0055954-AZ PAS Conversion and PC integration - Addded the below constants
 * SSO Integration  - Added the below constants as a part of SSO Integration to displaying the error message 
 * PC Phase II CH1 - Added the below Constants for PC Phase II changes
 * CHG0072116 - PC Edit Card Details CH1:Added constants for notification Email
 * CHG0072116 PC Edit Card Details CH2:Added constants for terms and conditions 
 * CHG0072116 - PC Edit Card Details CH3,CH4:Added constants for notification Email
 * CHG0072116 - PC Edit Card Details CH5:Added constants for notification Email
 * CHG0072116 - PC Edit Card Details CH6:Added constants for logging
 * T&C Changes CH1 - Added the below constants for Terms and condtion page change
 * CHG0104053 - PAS HO CH1 - Modified the below code to show the Tooltip for AAA SS Auto/Home Product 6/12/2014
 * CHG0104053 - PAS HO CH2 - Added the below code to show the Tooltip for AAA SS Home Product 6/12/2014
 * MAIG - CH1 - Updated the Constant values 
 * MAIG - CH2 - Added for restricting Other amount to Max Payment Amount
 * MAIG - CH3 - Modify the text as Business advised - 10/02/2014
 * MAIG - CH4 - Added constant to include the message for Credit card whose length is 15
 * MAIG - CH5 - Modified the below constants as part of MAIG
 * CHG0109406 - CH1 - Change to update ECHECK to ACH renaming - 01202015
 * CHG0109406 - CH2 - Change to update ECHECK to ACH renaming - 01202015
 * CHG0109406 - CH3 - Change to update ECHECK to ACH renaming - 01202015
 * CHG0109406 - CH4 - Modified the Error message from "policy retrival service" to "Retrieve Autopay Enrollment Service" 
 * CHG0110069 - CH1 - Added the below constant Error message to display error if KIC is alphanumeric
 * CHG0112662 - Added new constant for Error Code 300 & Commented the constant ERR_Excess_Payment
 * CHG0116140 - Added new constants for payment restriction error message
 * CHG0118686 - PRB0045696 - Message Changes - Start - FR1,FR2,FR3,FR4,FR6
 * CHG0129017 - Server Upgrade 4.6.1
 * CHG0129017 - Added constants for file names accessed in ACL.cs - Sep 2016
 * CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES
 *  */
using System;

namespace CSAAWeb
{
    /// <summary>
    /// Contains application constants.
    /// </summary>
    public class Constants
    {
        ///<summary></summary>
        public static int MIN_USERNAME_LENGTH = 7;
        ///<summary></summary>
        public static int MIN_PASSWORD_LENGTH = 8;
        ///<summary></summary>
        public static int MAX_PASSWORD_LENGTH = 15;

        ///<summary></summary>
        public const int MBR_ID_LENGTH = 8;
        ///<summary></summary>
        public const int CLUB_CODE_LENGTH = 3;
        ///<summary></summary>
        public const int ASSOC_ID_LENGTH = 1;

        ///<summary></summary>
        public const string DATE_FORMAT = "MM/dd/yyyy";

        ///<summary></summary>
        public const string MSG_GENERAL_ERROR = "APPLICATION ERROR. Please contact an administrator.";

        ///<summary></summary>
        public const string IK_STRING = "5iN~.kwSadHH8nt94k13TW9azq99f.s841ajkck#|klkasd@lx083+_*(pt;QedL1^$At'sApsS";
        ///<summary></summary>
        public const string CS_STRING = "u=W9az{ecKabb0aa-7f19x-11d2 978e-pt000.s840f8757e2a53~8X7e2a5|]ps]";

        //SSO-Integration.Ch1 : Start of error messages (Constants) - Start 

        public const string SSO_PASSWORD_IS_MISSING_IN_INPUT_REQUEST = "Password is missing in Input request.";
        public const string SSO_USER_ID_IS_MISSING_IN_INPUT_REQUEST = "User ID is missing in Input request.";
        public const string SSO_EITHER_OPEN_TOKEN_OR_USERID_AND_PASSWORD_IS_NEEDED_IN_THE_INPUT = "Either Open Token or userId and password is needed in the input.";
        //public const string SSO_OPEN_TOKEN_IS_INVALID_OR_EXPIRED = "Open Token is invalid or expired.";
        public const string SSO_OPEN_TOKEN_IS_INVALID_OR_EXPIRED = "There is an error with single signon token. Please contact your system administrator.";
        public const string SSO_AGENT_CONFIG_FILE_PATH_NOT_FOUND = "Agent.Config file path not found.";
        //public const string SSO_USER_ID_IS_MISSING_IN_OPEN_TOKEN = "User ID is missing in Open Token.";
        public const string SSO_USER_ID_IS_MISSING_IN_OPEN_TOKEN = "There is an error with single signon token. Please contact your system administrator.";
        public const string SSO_FAIL = "SSO_FAIL";
        public const string USER_IS_NOT_AUTHORIZED_IN_SSO_SERVER = "User is not authorized in SSO Server. Please contact your manager for access set-up.";
        //SSO-Integration.Ch1 : Start of error messages (Constants) - End
        // PUP Ch1:Start Modified by cognizant PUP_CHAR_VALIDATIONS error message for Last seven character validation in Insurance serializers on 01/19/2011
        //PUP Ch2:Modified the error valiables and the error message description by cognizant on 3/18/2011 
        //MAIG - CH1 - BEGIN - Updated the Constant values
        public const string PUP_NUMERIC_VALID = "The PUP Policy Number must be numeric";
        public const string AUTO_HOME_MESSAGE = "Policy Number should be 7 to 13 characters";
        public const string AUTO_HOME_TOOLTIP = "Enter policy number like 3667894, CAAS100442559, HK27569, WP268578,12346345 ";
        public const string OTHER_TOOLTIP = "Enter policy number like 3667894,619357,1063434";
        //CHG0110069 - CH1 - BEGIN - Added the below constant Error message to display error if KIC is alphanumeric
        public const string KIC_ALPHANUMERIC_INVALID = "KIC policy numbers cannot contain letters";
        //CHG0110069 - CH1 - END - Added the below constant Error message to display error if KIC is alphanumeric
        //Added error message to be displayed if the field is empty 11/19/2014
        public const string NAME_SEARCH_LAST_NAME_REQ = "Last Name is required.";
        public const string NAME_SEARCH_MAILING_ZIP_REQ = "Billing Zip Code is required.";
        public const string FIRST_NAME_REQ = "First Name is required.";
        public const string MAILING_ZIP_CODE_REQ = "Mailing Zip Code is required.";
        public const string PAYMENT_TYPE_REQ = "Payment Method is required.";
        public const string CHECK_NUMBER_REQ = "Check Number is required.";
        public const string PAYMENT_CARD_TYPE = "Card is not of the selected type.";
        public const string PAYMENT_CARD_NUMBER = "Card Number is required.";
        public const string PAYMENT_CARD_EXPIRATION = "Card Expiration Month/Year is required.";
        public const string PAYMENT_ACCOUNT_TYPE_REQ = "Account Type is required.";
        public const string PAYMENT_ACCOUNT_NUMBER_REQ = "Account Number is required.";
        public const string PAYMENT_ROUTING_NUMBER_REQ = "Routing Number is required.";
        //Added error message to be displayed if the field is empty 11/19/2014
        public const string NAME_SEARCH_LAST_NAME_MIM_LENGTH = "Please enter minimum 3 characters in Last Name.";
        public const string NAME_SEARCH_FIRST_NAME_MIM_LENGTH = "Please enter minimum 3 characters in First Name.";
        public const string NAME_SEARCH_LAST_NAME_START_CHAR = "Last Name Search criteria cannot start with a wildcard.";
        public const string NAME_SEARCH_FIRST_NAME_START_CHAR = "First Name Search criteria cannot start with a wildcard.";
        public const string NAME_SEARCH_MAILING_ZIP_INVALID = "Please enter minimum 5 digits in Zip Code.";
        public const string NAME_SEARCH_ERR_MESSAGE = "Please complete or correct the requested information in the field(s) highlighted in red above.";
        public const string NAME_SEARCH_NEW_LINE = "<br />";

        public const string PC_BILL_POL_STATE = "POL_STATE";
        public const string PC_BILL_COMPANY_ID = "COMPANY_ID";
        public const string PC_BILL_FIRST_NAME = "First_Name";
        public const string PC_BILL_LAST_NAME = "Last_Name";
        public const string PC_BILL_INS_FULL_NAME = "INS_FULL_NME";
        public const string PC_BILL_POL_GROUP = "POL_GROUP";
        public const string PC_BILL_ERR_MSG = "ERR_MSG";
        public const string PC_BILL_STATUS = "Status";
        public const string PC_BILL_CONVERTED_POLICYNUMBER = "Converted_Policynumber";
        public const string PC_BILL_ERROR_CODE = "ErrorCode";
        public const string PC_BILL_ERROR_DESCRIPTION = "ErrorDescription";
        public const string PC_BILL_PC_ERROR_CODE = "PC_ErrorCode";
        public const string PC_BILL_AUTO_PAY= "AutoPay";
        public const string PC_BILL_STATUS_DESCRIPTION = "statusDescription";

        public const string PC_BILL_RESTRICTEDTOPAY= "isRestrictedToPay";
        public const string PC_BILL_SETUPAUTOPAYREASONCODE = "setupAutoPayReasonCode";
        public const string PC_BILL_SETUPAUTOPAYEFFDATE = "setupAutoPayEffectiveDate";
        public const string PC_BILL_UPDATEAUTOPAYREASONCODE = "updateAutoPayReasonCode";
        public const string PC_BILL_UPDATEAUTOPAYEFFDATE= "updateAutoPayEffectiveDate";
        public const string PC_BILL_CANCELAUTOPAYREASONCODE = "cancelAutoPayReasonCode";
        public const string PC_BILL_CANCELAUTOPAYEFFDATE= "canceAutoPayEffectiveDate";
        public const string PC_REQ_TRANSACTION_TYPE = "INCL_ALL_ELIG";
        public const string PMT_RECURRING_ACCOUNTNUMBER = "AccountNumber";
        public const string PMT_RECURRING_PRODUCTTYPE = "ProductType";
        public const string PMT_RECURRING_CCNUMBER = "CCNumber";
        public const string PMT_RECURRING_IS_CC_PAYMENT = "IS_CC_PAYMENT";
        public const string PMT_RECURRING_CCECTOKEN = "CCECToken";
        public const string PMT_RECURRING_CCEXPMONTHYEAR = "CCExpMonthYear";
        public const string PMT_RECURRING_CCFULLNAME = "CCFullName";
        public const string PMT_RECURRING_CCType = "CCType";
        public const string PC_BILL_ADDRESS = "BillingAddress";

        public const string PMT_RECURRING_EMAILID = "EmailID";

        public const string PMT_RECURRING_ECBANKACNTTYPE = "ECBankAcntType";
		public const string PC_SP_UpdateInvalidPolicy = "PAY_Recurring_Insert";
        public const string PC_INVALID_POLICY_FLOW_MESSAGE = "Policy not found. Please provide the additional details.";
        public const string PC_InvalidPolicyFlow = "InvalidPolicyFlow";

        public const string PC_InsuredFullName = "InsuredFullName";

        public const string PC_InvalidUnEnrollFlow = "InvalidUnEnrollFlow";
        public const string PC_InvalidPolicyFName = "InvalidPolicyFName";
        public const string PC_InvalidPolicyLName = "InvalidPolicyLName";
        public const string PC_InvalidPolicyMZip = "InvalidPolicyMZip";
        public const string PC_INVLD_ENROLL_ACTIVE = "Active";
        public const string PC_INVLD_ENROLL_INACTIVE = "InActive";
        public const string PMT_RECURRING_CCZIPCODE = "CCZipCode";
        public const string PMT_RECURRING_ECBANKID = "ECBankId";
        public const string PMT_RECURRING_ECACCOUNTNUMBER = "ECAccountNumber";
        public const string PMT_RECURRING_DOWNFIRSTNAME = "DownFirstName";
        public const string PMT_RECURRING_DOWNLASTNAME = "DownLastName";
        public const string PMT_RECURRING_DOWNMAILINGZIP = "DownMailingZip";
        public const string PMT_RECURRING_DOWNEMAILADDRESS = "DownEmailAddress";
        public const string PMT_RECURRING_INSTSOURCESYSTEM = "INSTSourceSystem";

        public const string PC_CRD_TYPE_AMEX = "AMEX";
        public const string PC_CRD_TYPE_AMEX_EXPANSION = "AMERICAN EXPRESS";
        public const string PC_CRD_TYPE_DISC = "DISC";
        public const string PC_CRD_TYPE_DISC_EXPANSION = "DISCOVER";
        public const string PC_INVALID_POLICY_REASON_CODE = "ELIG_NEED_VRFY";
        public const string PMT_RECURRING_ISDOWNFLOW = "IsDownFlow";
		public const string POLICY_LENGTH_HOME_MESSAGE = "Policy Number should be 7 digits";
        //CHGXXXXXXX - PRB0045696 - Message Changes - Start - FR6//
        public const string ELASTIC_SEARCH_ERROR_MESSAGE = "There is a problem retrieving policy details. A system issue was encountered.  Please try again later. If problem persists contact support. (Search)";
        //CHGXXXXXXX - PRB0045696 - Message Changes - End - FR6//
		//public const string PUP_POLICYNUM = "Policy Number should be 7 characters";
        //MAIG - CH1 - END - Updated the Constant values
        public const string POLICY_LENGTH_AUTO_MESSAGE = "Policy Number should be 7, 9 or 13 characters";
        public const string POLICY_LENGTH_PUP_MESSAGE = "Policy Number should be 10 characters";
        //public const string PUP_NUMERIC_VALID = "Last 7 characters of the policy number must be numeric";
        public const string POLICY_INVALIDCODE = "Invalid Policy Number. Please enter a valid Policy Number and try again";
        // PUP Ch1:END Modified by cognizant PUP_CHAR_VALIDATIONS error message for Last seven character validation in Insurance serializers on 01/19/2011
        // 67811A0  - PCI Remediation for Payment systems CH1:START-Added the constants during PCI implementation.
        public const string PCI_VOIDPAYMENT_REQUIRED_FIELD = "MISSING USER ID OR APP ID OR ReceiptNumber";
        public const string PCI_SIS_POLICY_LENGTH = "Policy number should be between 4 to 9 digits for SIS policies";
        public const string PCI_POLICY_NUMERIC = "Policy Number should be Numeric";
        public const string PCI_INVALID_POLICY_PRODUCT = "The Policy number entered does not match with the selected product type";
        public const string PCI_INVALID_SEQUENCE = "Policy Number is not a valid sequence";
        public const string PCI_EXG_RESTRICTED_POLICY = "The payment for this State - Product combination is not valid";
        public const string PCI_EXG_NUMERIC = "Last 9 characters of the policy number must be numeric";
        public const string PCI_PUP_TOOLTIP = "Enter the Policy number with prefix for PUP policy like PUP1234567";
        public const string PCI_AUTO_TOOLTIP = "Enter the Policy number without prefix for IIB Auto and HUON auto like AE00009, 050288223 and with prefix for PAS auto like CAAS123456789";

        public const string PCI_HOME_TOOLTIP = "Enter the Policy number without prefix for IIB Home as HE00009";
        public const string PCI_SIS_TOOLTIP = "Enter the Policy number without Prefix for WU Insurance Policies like 3828282";
        public const string PCI_LEGACY_ALPHANUMERIC = "Policy Number must be alphanumeric";
        public const string PCI_REVENUE_INVALID = "The Revenue type should not be Down Payment for the keyed in Policy number";
        public const string PCI_INVALID_POLICY_PRODUCT1 = "The Policy number entered does not match with the selected product type. Do you want to proceed?";
        public const string PCI_CONFIRMATION_CHECK_TOOLTIP = "Please check the checkbox";
        public const string PCI_CONFIRMATION_MESSAGE_ALL_PAYMENT_TYPES_EXCEPT_ECHCEK = "I have reviewed that all payment information entered is correct";
        public const string PCI_CONFIRMATION_MESSAGE_FOR_ECHCEK_PAYMENT = "I authorize CSAA Insurance Group to make a one-time debit to my checkings or savings account.This is permission for a single transaction only, and does not provide authorization for any additional unrelated debits or credits to your account. I understand that because this is an electronic transaction, these funds may be withdrawn from my account as soon as the above noted transaction date.";
        public const string PCI_SIS_LOOKUP_FAILURE = "Policy number entered is not found.";
        public const string PCI_NODUE = "NA";
        public const string PCI_SIS_DOWN = "AS 400 is currently down, please try again later.";

        // 67811A0  - PCI Remediation for Payment systems CH1:End-Added the constants during PCI implementation.
        //67811A0  - START -PCI Remediation for Payment systems - Arcsight logging
        public const string PCI_ARC_PROTOCOL = "HTTPS";
        public const string PCI_ARC_DEVICEVERSION = "3.5";
        public const string PCI_ARC_DEVICETIMEZONE = "Mountain Standard Time";
        public const string PCI_ARC_DEVICEVENDOR = "AAA IE";
        public const string PCI_ARC_DEVICEPRODUCT = "PaymentTool";
        public const string PCI_EVENT = "PCI Event";
        // public const string PCI_ARC_SourceAddress = "172.26.158.40";
        public const string PCI_ARC_WEBCLIENT = "WEB";
        public const string PCI_ARC_SERVICECLIENT = "Service";
        public const string PCI_ARC_DEVICEACTION_AUTH_SUCCESS = "User Authenticated";
        public const string PCI_ARC_EVENTOUTCOME_ADD = "ADD";
        public const string PCI_ARC_EVENTOUTCOME_READ = "READ";
        public const string PCI_ARC_EVENTOUTCOME_DELETE = "DELETE";
        public const string PCI_ARC_EVENTOUTCOME_MODIFY = "MODIFY";
        public const string PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS = "SUCCESS";
        public const string PCI_ARC_SEVERITY_HIGH = "9";
        public const string PCI_ARC_SEVERITY_LOW = "1";
        public const string PCI_ARC_DEVICEEVENTCATEGORY_FAILURE = "FAILURE";
        public const string PCI_ARC_REQUESTURL = "https://payments.csaa.com";
        public const string PCI_ARC_LOGIN_LOCKOUT = "This account has been locked out. Please contact an administrator.";
        public const string PCI_ARC_LOGIN_INCORRECT = "Login incorrect. Please try again.";
        public const string PCI_ARC_LOGOUT = "Login incorrect. Please try again.";
        public const string PCI_ARC_NAME_AUTHENTCATE_SUCCESS = "User Authenticated Successfully";
        public const string PCI_ARC_NAME_AUTHENTCATE_FAILURE = "User Authentication Failed";
        public const string PCI_ARC_NAME_CC_CREATED = "Credit Card/E-check Object Created Successfully";
        public const string PCI_ARC_NAME_CC_DELETED = "Credit Card/E-check Object Deleted Successfully";
        public const string PCI_ARC_NAME_CASH = "Cash/Check Payment request is Successful";
        public const string PCI_ARC_NAME_CASH_FAILURE = "Cash/Check Payment request is Failed";
        public const string PCI_ARC_NAME_OFFLINECREDITCARD_FAILURE = "Offline Credit card Payment request is Failed";
        public const string PCI_ARC_NAME_BILL = "Received a successfull Bill Payment request";
        public const string PCI_ARC_NAME_LOGOUT = "User has successfully Logged out";
        public const string PCI_ARC_NAME_AUTH_SUCCESS = "Credit card Auth Request is Successful";
        public const string PCI_ARC_NAME_AUTH_FAILURE = "Credit card Auth Request is Failed";
        public const string PCI_ARC_NAME_BILL_FAILURE = "Credit card Bill Request is Failed";
        public const string PCI_ARC_NAME_BILL_SUCCESS = "Credit card Bill Request is Successful";
        public const string PCI_ARC_NAME_PROCESS_SUCCESS = "Credit card Process Request is Successful";
        public const string PCI_ARC_NAME_PROCESS_FAILURE = "Credit card Process Request is Failed";
        public const string PCI_ARC_NAME_ACHVERIFY_SUCCESS = "E-check verification Request is Successful";
        public const string PCI_ARC_NAME_ACHVERIFY_FAILURE = "E-check verification Request is Failed";
        public const string PCI_ARC_NAME_ACHDEPOSIT_SUCCESS = "E-check Deposit Request is Successful";
        public const string PCI_ARC_NAME_ACHDEPOSIT_FAILURE = "E-check Request Request is Failed";

        public const string PCI_ARC_LOGOUT_USER = "User has logged out of application";
        public const string PCI_ARC_DESTINATION_PROCESSNAME_PAYMENT = "Payment Transactions";
        public const string PCI_ARC_DEVICEACTION_ORDER = "Received a Cash/Check Payment request";
        public const string PCI_ARC_DEVICEACTION_OFFLINECREDITCARD = "Received a offline credit card Payment request";
        public const string PCI_ARC_DEVICEACTION_CREDIT = "Credit Card/E-check Object Created";
        public const string PCI_ARC_DEVICEACTION_CASHCHECK = "Received a Cash/Check payment for the receipt number ";
        public const string PCI_ARC_DEVICEACTION_CASHCHECK_FAILURE = "The received Cash/Check payment request is Failed ";
        public const string PCI_ARC_DEVICEACTION_ECHECK = "Received a E-check Payment request";
        public const string PCI_ARC_NAME_ECHECK_DEPOSIT = "E-check ACH-Deposit for the receipt number ";

        public const string PCI_ARC_NAME_ECHECK_VERIFY = "E-check ACH-Verify for the receipt number ";

        public const string PCI_ARC_DEVICEACTION_CARD = "Received a Credit Card Payment request";
        public const string PCI_ARC_NAME_CARD_AUTH = "Credit Card Authorization for the receipt number ";
        public const string PCI_ARC_NAME_CARD_VOID = "Credit Card Void request for the receipt number ";
        public const string PCI_ARC_NAME_VOID_SUCCESS = "Credit Card Void Request Successful";
        public const string PCI_ARC_NAME_VOID_FAILURE = "Credit Card Void Request Failed";


        public const string PCI_ARC_NAME_CARD_BILL = "Credit Card Bill request for the receipt number ";
        public const string PCI_ARC_NAME_CARD_PROCESS = "Credit Card Process request for the receipt number ";
        public const string PCI_ARC_DEVICEACTION_AUTH_REQUEST = "Received a Payment Request from ";
        public const string PCI_ARC_DEVICEACTION_BILL_REQUEST = "Received a Bill Payment Request from ";
        public const string PCI_ARC_NAME_BILL_REQUEST = "Received an Bill Payment Request";
        public const string PCI_ARC_DESTINATION_PROCESSNAME_LOGIN = "User Authentication";
        public const string PCI_ARC_DEVICEACTION_DO = "DO office";
        public const string PCI_SOURCE_PROCESS_NAME = "APDS";
        public const string PCI_ARC_DEVICEACTION_DO_ADD = "New DO office is Added";
        public const string PCI_ARC_NAME_DO_ADD = "New DO office has been Added Successfully";
        public const string PCI_ARC_NAME_DO_ADD_FAILED = "DO office Addition/Update is Failed";
        public const string PCI_ARC_NAME_DO_EDIT = "DO office is Updated or Modified";
        public const string PCI_ARC_DEVICEACTION_UPDATE_DO = "District Office ";
        public const string PCI_ARC_NAME_ADD_DO = "DO office has been Added";
        public const string PCI_ARC_NAME_EDIT_DO = "DO office has been Updated";
        public const string PCI_ARC_NAME_DO_EDIT1 = "DO office update is Successful";
        public const string PCI_ARC_DO_VIEW = "District office";
        public const string PCI_ARC_DO_NAME = "District and HUB office is Viewed";
        public const string PCI_ARC_DO_VIEW_NAME = "District and HUB office is Viewed Successfully";
        public const string PCI_ARC_DESTINATION_USER = "User Administration";
        public const string PCI_ARC_NAME_PASSWORD = " Password is Reset by ";
        public const string PCI_ARC_NAME_PASSWORD_RESET = "Password reset was Successful";
        public const string PCI_ARC_NAME_DELETE_USER_SUCCESS = "User Deletion is Successful";
        public const string PCI_ARC_NAME_DELETE_USER_FAILED = "User Deletion has Failed";
        public const string PCI_ARC_NAME_ADD_USER = "User Update/Addition is Failed";
        public const string PCI_ARC_NAME_ADD_USER_SUCCESS = "New user has been Added Successfully";
        public const string PCI_ARC_NAME_MODIFY_USER_SUCCESS = "User has been Modified Successfully";
        public const string PCI_MESSAGE_OFFLINECC = "Offline Credit card is not an acceptable payment method. Cancel and select another payment method";
        public const string PCI_ACTUALMESSAGE_OFFLINECC = "Invalid Payment Method: Offline Credit Card";
        public const string PCI_FLAG_OFFLINECC = "CSAA_RULE";
        //public const string PCI_ARC_DESTINATION_PROCESSNAME = "Delete user";
        public const string PCI_ARC_NAME_USER = " is deleted by the user ";
        public const string PCI_ARC_DESTINATION_PROCESSNAME_USER = "User Administration ";
        public const string PCI_ARC_NAME_ADDUSER_UPDATE = " details has been modified by the user ";
        public const string PCI_ARC_NAME_ADDUSER_NEW = " details has been added by the user ";
        public const string PCI_ARC_DEVICEACTION_ORDER_NULL = "Credit card/E-check object Deleted";
        public const string PCI_ARC_NAME_ORDER_NULL = "Payment response/Credit object is Cleared";
        public const string PCI_ARC_APPLICATION_USERID = "SVC_TENT_PaymentTool";
        public const string PCI_ARC_APPLICATION_APPNAME = "Payment Tool";
        public const string PCI_ARC_APPLICATION_SUBSYSTEM = "Payment Processing System";
        public const string PCI_ARC_APPLICATION_TRANSTYPE = "Online";
        //public const string PCI_ARC_SOURCEIP = "127.0.0.1";
        //67811A0  - END -PCI Remediation for Payment systems - Arcsight logging
        public const string PCI_MESSAGE_MERCHANT_REF_NUM = "Merchant Reference Number should be less than 50 Characters and AlphaNumeric";
        //67811A0  - START -PCI Remediation for Payment systems - Amount Zero,membership number and NetPos user ID validation Error messages
        public const string PCI_ERR_NETPOS_INVALID_USERID = "You must have access to the Payment Tool application to take a payment. Please complete a work order with ITSC.";
        public const string PCI_ERR_ZERO_PRICEFIELD = "Price field  should be greater than Zero";

        ///RFC 185138 - AD Integration - CH1 START - Added the below constants
        public const string AD_ERR_INVALID = "User has invalid credentials to perform transaction. Contact the IT Service Desk at 877-554-2911.";
        public const string AD_ERR_LOCKED = "User account is locked. Use SAM RESET to unlock your ENT user ID and password account.";
        public const string AD_ERR_ACNTEXPIRED = "User account has expired. Contact the IT Service Desk at 877-554-2911.";
        public const string AD_ERR_DISABLED = "User account has been disabled. Contact the IT Service Desk at 877-554-2911.";
        public const string AD_ERR_PWDEXPIRED = "User password has expired. Use SAM RESET to change your password.";
        public const string AD_ERR_ADAUTH = "Authentication failed in Active Directory.";
        public const string AD_ERR_NOTFOUND = "User does not have permission to log into Payment Tool. Contact the IT Service Desk at 877-554-2911.";
        public const string AD_ERR_NOTPERMITTEDATTHISTIME = "Contact the IT Service Desk at 877-554-2911.";
        public const string AD_ERR_NOTPERMITTEDFROMTHISCOMP = "User is currently logged onto another computer.";
        public const string AD_ERR_RESETPASSWORD = "User password must be reset. Use SAM RESET to reset your ENT user ID and password.";


        public const string AD_AUTH_SUCCESS = "Success";
        public const string AD_AUTH_FAILURE = "Failed";
        public const string AD_AUTH_NONSSOFAILURE = "Invalid user in Payment tool";
        public const string APPLICATION_ERROR = "APPLICATION ERROR. Please contact an administrator.";
        ///RFC 185138 - AD Integration - CH1 END - Added the below constants
        //PAS AZ Product Configuration : Added the below error message for the payment type selection drop down box based upon the product selection by cognizant on 03/12/2012
        //CHG0109406 - CH1 - BEGIN - Change to update ECHECK to ACH renaming - 01202015
        public const string PYMT_PUP = "Payment method is restricted to Credit Card and ACH for Personal Umbrella Policy Product.";
        public const string PYMT_WU = "Payment method is restricted to Credit Card and ACH for Western United Products.";
        //CHG0104053 - PAS HO CH1 - START - Modified the below code to show the Tooltip for AAA SS Auto/Home Product 6/12/2014
        public const string PYMT_AZAuto = "Payment method is restricted to Credit Card and ACH for Signature Series Products.";
        //CHG0109406 - CH1 - END - Change to update ECHECK to ACH renaming - 01202015
        //CHG0104053 - PAS HO CH1 - END - Modified the below code to show the Tooltip for AAA SS Auto/Home Product 6/12/2014
        public const string AZAUTO_TOOLTIP = "Enter the Policy number  with prefix for PAS AZ Auto like AZSS123456789";
        //CHG0104053 - PAS HO CH2 - START - Added the below code to show the Tooltip for AAA SS Home Product 6/12/2014
        public const string SSHOME_TOOLTIP = "Enter the Policy number  with prefix for AAA SS Home like AZSS123456789";
        //CHG0104053 - PAS HO CH2 - END - Added the below code to show the Tooltip for AAA SS Home Product 6/12/2014


        //Refund in UI - Declared the below constants.
        public const string REFUND_STATUS = "13";
        public const string VOID_STATUS = "4";
        public const string PAYMODE_CASH = "cash";
        public const string PAYMODE_CHECK = "check";
        public const string PAYMODE_CCARD = "credit card";
        //CHG0109406 - CH2 - BEGIN - Change to update ECHECK to ACH renaming - 01202015
        public const string PAYMODE_ECHECK = "ACH";
        //CHG0109406 - CH2 - END - Change to update ECHECK to ACH renaming - 01202015
        public const string REFUND_ERR_MESSAGE = "Transaction has been already Refunded/Voided. Enter another receipt transaction. ";
        public const string REFUND_NO_TRANSACTION_FOUND = "No Receipt Transaction Found. Enter another receipt transaction. ";
        //Security Defect - Declared the below constants.
        public const string ERR_AUTHVALIDATION = "Validation failed in ";
        public const string ERR_CODE = " - Error code: ";
        public const string SEC_COOKIE_NOTEQUAL = "Redirected to login page since the ipaddress in cookie Authtoken is not same as that of the login page";
        public const string SEC_COOKIE_NULL = "Redirected to login page since the ipaddress is null in the cookie AuthToken";
        //CHG0055954-AZ PAS Conversion and PC integration - Addded the below constants
        public const string ERR_MSG_M001 = "Payment cannot be accepted due to policy status.  Payment can be made to PAS policy ";
        public const string ERR_MSG_M002 = "Payment cannot be accepted due to policy status.";
        public const string ERR_MSG_M003_FIRST = "Please pay outstanding balance on the current term ";
        public const string ERR_MSG_M003_LAST = "prior to making the renewal payment.";
        //CHG0109406 - CH3 - BEGIN - Change to update ECHECK to ACH renaming - 01202015
        public const string PYMT_HLAUTO = "Payment method is restricted to Credit Card and ACH for High Limit Product.";
        //CHG0109406 - CH3 - END - Change to update ECHECK to ACH renaming - 01202015
        public const string PYMT_HLTOOLTIP = "Enter the Policy number with 10 digit numeric number like 1234567890 for HL policy";
        public const string POLICY_LENGTH_HL_MESSAGE = "Policy Number should be 10 digits";

        //CHG0112662 - BEGIN - Commented the constant ERR_Excess_Payment
        //public const string ERR_EXCESS_PAYMENT = "The amount entered must be less than or equal to the total due amount available for this policy.";
        //CHG0112662 - END - Commented the constant ERR_Excess_Payment

        //MAIG - CH2 - BEGIN - Added for restricting Other amount to Max Payment Amount 
        public const double MAX_PAYMENT_AMOUNT = 25000.00;
        public const string ERR_EXCEEDS_MAX_PAYMENT = "The amount entered must be less than or equal to $25000.00";
        //MAIG - CH2 - END - Added for restricting Other amount to Max Payment Amount 

        public const string ERR_BILL_LOOKUP_RUNTIME_EXCEPTION = "A system error has occurred.If error continues please contact IT Services Desk at 1-877-554-2911.";
        public const string ERR_CODE_BUSINESS_EXCEPTION = "SOAB301";

        //SSO Integration  - Added the below constants as a part of SSO Integration to displaying the error message 
        public const string SSO_INVALID_ERR_MESSAGE = "Invalid User Credentials.Please try again.";
        public const string SSO_PT_DB_INVALID_ERR_MESSAGE = "Login failed. Please try again.";
        public const string SSO_SAML_INVALID_STAUSUSERNAME_ERR_MESSAGE = "Invalid Status Code and UserName from SSO Server ";
        public const string MSG_INVALIDSAML_RESPONSE = "The trusted source of  SAML Token cannot be verified at this moment.";
        //MAIG - CH3 - BEGIN - Modify the text as Business advised - 10/02/2014
        public const string MSG_LOGOUT = "Your session has expired / You have logged out of the application.";
        //MAIG - CH3 - END - Modify the text as Business advised - 10/02/2014
        public const string SAML_FAIL_RESPONSE = "SSOSAMLFAILURE";

        //Begin PC Phase II CH1 - Added the below Constants for PC Phase II changes
        public const string PC_ERR_RUNTIME_EXCEPTION = " A system error has occurred.If error continues please contact IT Service Desk at 1-877-554-2911.";
        public const string PC_ERR_BUSINESS_EXCEPTION = " The payment processing service has returned  the following error message: ";
        public const string PC_ERR_BUSINESS_EXCEPTION_ENROLL = " The service has returned  the following error message: ";
        public const string PC_ERR_RESPONSE_NULL = "Response object is null.";
        public const string PC_ERR_NODE_FRIENDLY_ERROR_MSG = "friendlyErrorMessage";
        public const string PC_ERR_NODE_ERROR_MSG_TEXT = "errorMessageText";
        public const string PC_ERR_NODE_ERROR_MSG = "errorMessage";
        public const string PC_ERR_NODE_ERROR_CODE = "errorCode";
        public const string PC_ERR_CODE_TOKEN_BUSINESS_EXCEPTION = "-200";
        public const string PC_ERR_CODE_VALIDATE_ENROLLMENT_BUSINESS_EXCEPTION = "-200";
        //Error Msg Handling - BEGIN - CHG0112662 - Added new constant for Error Code 300
        public const string PC_ERR_CODE_ROUTING_NUMBER_NOT_FOUND = "-300";
        //Error Msg Handling - END - CHG0112662 - Added new constant for Error Code 300

        public const string PC_PRODUCT_CODE = "INSURANCE";
        public const string PC_COMPANY_CODE = "CSIIB";
        public const string PC_APPID_PAYMENT_TOOL = "APDS";
        public const string PC_APPID_PAYMENTCENTRAL = "PCONLINE";
        public const string PC_APPID_SALESX = "SALESX";
        public const string PC_SP_Pay_InsertPayment = "PC_Pay_InsertPayment";
        public const string PC_SP_Pay_Insert_Item = "PC_Pay_InsertItem";
        public const string PC_SP_Pay_InsertCardCheck = "PC_Pay_InsertCardCheck";
        public const string PC_SP_Pay_Get_WU_CompanyCode = "Pay_Get_WU_CompanyCode";
        public const string PC_SP_Get_Running_Number = "Get_Running_Number";
        public const string PC_SP_PC_Pay_Update_Void = "PC_Pay_Update_Void";
        public const string PC_HUONPREFIX = "PAA";
        public const string PC_Default_DO = "500";
        public const string PC_IDP_Reversal_ReasonCode = "RV5";
        public const string PCI_VOIDPAYMENT_APPNAME = "APDS";
        public const string PCI_VOIDPAYMENT_PC = "Payment Central processed transaction cannot be voided .Please Try with another receipt Number";
        public const string PC_VOID_STATUS = "V";
        public const string PC_MANUALVOID_STATUS = "M";
        public const string PC_MANUALVOID_SELECT = "Manual Void";
        public const string PC_REVENUETYPE_ERND = "ERND";
        public const string PC_REVENUETYPE_INST = "INST";
        public const string PC_REVENUETYPE_DOWN = "DOWN";
        public const string PC_REVENUE_ERND = "291";
        public const string PC_REVENUE_INST = "1291";
        public const string PC_REVENUE_DOWN = "297";
        public const string PC_AUTHCH_CC = "CC";
        public const string PC_AUTHCH_IVR = "IVR";
        public const string PC_AUTHCH_BO = "BO";        
        public const string PC_AUTHCH_F2F = "F2F";
        public const string PC_AUTHCH_ONL = "ONL";
        public const string PC_INVALIDEMAIL = "Please Enter a valid email address(For example: sample@sample.com)";
        public const string PC_VALIDATEENROLLMENTSERVICE = "ENROLL";
        public const string PC_VALIDATEUNENROLLMENTSERVICE = "UNENROLL";
        public const string PC_VALIDATEMODIFYENROLLMENTSERVICE = "MODIFY";
        public const string PC_CB_TANDC_CONTACTCENTER = "I acknowledge that CSAA Insurance Group received the customer's authorization over the telephone to enroll or modify in Recurring payments. ";
        public const string PC_CB_TANDC_F2F = "I acknowledge that CSAA Insurance Group received the customer's signed authorization form to enroll or modify in Recurring payments. "; 
		//End PC Phase II CH1 - Added the below Constants for PC Phase II changes 
        public const string PC_PAYMENT_RESTRICTION = "Policy is in Payment Restriction and  cannot be enrolled using EFT as a payment method. Please use a different form of payment for enrollment.";
        //Payment Restriction - BEGIN - CHG0116140 - Added new constants for payment restriction error message
        public const string PC_PAYMENT_RESTRICTION_ERROR_MSG = "ACH payment is not available for selected policy, please pay by Credit Card.";
        public const string PC_PAYMENT_RESTRICTION_ERROR_MSG1 = "Policy is on restriction.  Payment methods are limited for this policy.";
        //Payment Restriction - BEGIN - CHG0116140 - Added new constants for payment restriction error message
        public const string PC_Contact_Center_Script = "By providing your information and acknowledging this conversation you authorize CSAA Insurance Group to initiate a recurring debit to your" +
                        "< credit card/debit card or savings/checking> account.  Please be sure your account has enough available credit or funds to ensure that your payment is not returned unpaid to avoid bank charges.  The debit will be initiated on or after your payment due date for the amount due on your policy at that time. We may re-submit returned payments up to the number of times permitted. The recurring terms and conditions are available to you.";

        public const string PC_INVALID_POLICY = "There is no matching policy found for the given search criteria. Please verify and enter valid Policy details.";
        public const string PC_VALID_SERVICE_FAILURE = "Validation service returns unexpected reason code";
        public const string PC_EMAIL_FAIL = "Enter an valid email address.";
        public const string PC_SUCESS = "Success";
        public const string PC_FAIL = "Failed";
        public const string PC_Payment_CC = "Credit Card";
        public const string PC_UNENROLL_SUCCESS = "UnEnrollment Successful";
        public const string PC_UNENROLL_CONFIRM_PATH = "../forms/UnEnrollmentConfirm.aspx";
        public const string PC_LOGO_PATH = "/PaymentToolimages/logo.png";
        public const string PC_UNENROLL_HTML = "/PaymentToolimages/email-success-CCunenroll.html";
        public const string PC_SUCCESS_HTML = "/PaymentToolimages/success.html";
        //CHG0072116 - PC Edit Card Details CH1:Added constants for notification Email
        public const string PC_EDIT_SUCCESS_HTML = "/PaymentToolimages/EditCardDetails_Success.html";  
        public const string PC_CORRECT_ICON_PATH = "/PaymentToolimages/correct-icon.png";
        public const string PC_EC_SAVG = "Savings";
        public const string PC_EC_CHKG = "Checking";
        public const string PC_DO_ID = "DO ID";
        public const string PC_DO_CC = "PC_DO.CC";
        public const string PC_SEL_PROD = "Select Product Type";
        public const string PC_PROD_REQ = "Product Type is required.";
        public const string PC_POL_REQ = "Policy Number is required.";
        public const string PC_POL_ACTIVE = "Active";
        public const string PC_POL_ACTIVE_NOTATION = "A";
        public const string PC_POL_LAPSED = "Lapsed";
        public const string PC_POL_LAPSED_NOTATION = "L";
        public const string PC_POL_CANCEL = "Cancelled";
        public const string PC_POL_CANCEL_NOTATION = "C";
        public const string PC_POL_BILL_MONTHLY = "Monthly";
        public const string PC_POL_BILL_MONTHLY_NOTATION = "MTH";
        public const string PC_POL_BILL_QUARTERLY="Quarterly";
        public const string PC_POL_BILL_QUARTERLY_NOTATION = "QTR";
        public const string PC_POL_BILL_SEMI="Semi-annual";
        public const string PC_POL_BILL_SEMI_NOTATION = "SAN";
        public const string PC_POL_BILL_ANNUAL = "Annual";
        public const string PC_POL_BILL_ANNUAL_NOTATION = "ANN";
        public const string PC_POL_ENROLL_STATUS = "Yes";
        public const string PC_POL_NOTENROLL_STATUS = "No";
        public const string PC_CC_DEFAULT_TEXT = "Select One";
        public const string PC_CC_SELECT_MONTH = "Select Month";
        public const string PC_CC_SELECT_YEAR = "Select Year";
        public const string PC_CC_ERR_INVALID_CARDTYPE = "Select Valid Card Type";
		//MAIG - CH4 - BEGIN - Added constant to include the message for Credit card whose length is 15
        public const string PC_CC_ERR_INVALID_CARDAMEX = "Credit Card Number Length must be exactly 15 characters.";
        public const string PC_CC_ERR_INVALID_CARDOTHERS = "Credit Card Number Length must be exactly 16 characters.";
		//MAIG - CH4 - END - Added constant to include the message for Credit card whose length is 15
        public const string PC_CC_ERR_CARDNUMBER_REQ = "Card Number is required.";
        public const string PC_CC_ERR_INVALID_EXP_MONTH = "Select valid Expiration Month";
        public const string PC_CC_ERR_SELECT_EXP_YEAR = "Select valid Expiration Year";
        public const string PC_CC_ERR_NAME_REQ = "Name on Card is required.";
        public const string PC_CC_ERR_EMPTY_ZIPCODE = "Enter valid Zip Code.";
        public const string PC_ERR_CHK_TC = "Please review and accept Terms & Conditions to continue.";
        public const string PC_CC_CODE = "Code";
        public const string PC_CC_REGX = "Regex";
        public const string PC_CC_INVALID_EXP_YEAR = "Expiration Date is invalid.";
        public const string PC_EMAIL_REGX = @"^(([^<>()[\]\\.,;:\s@\""]+"
                                        + @"(\.[^<>()[\]\\.,;:\s@\""]+)*)|(\"".+\""))@"
                                        + @"((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}"
                                        + @"\.[0-9]{1,3}\])|(([a-zA-Z\-0-9]+\.)+"
                                        + @"[a-zA-Z]{2,}))$";
        public const string PC_ERR_INVALID_EMAILL_ADDR = "Enter valid email address.";
        public const string PC_CC_ERR_INVALID_NAME = "Enter a Valid Name on Card";
        public const string PC_PTYPE_TC = "CRDC";
        //CHG0072116 PC Edit Card Details CH2:Added constants for terms and conditions 
        public const string PC_PTYPE_CC = "CCUPDATE";
        public const string PC_TC_ARG1 = "arg1";
        public const string PC_TC_ARG2 = "arg2";
        public const string PC_TC_ARG3 = "arg3";
        public const string PC_TC_ARG4 = "arg4";
        public const string PC_TC_ARG5 = "arg5";
        public const string PC_TC_ARG6 = "arg6";
        //T&C Changes CH1 - START - Added the below constants for Terms and condtion page change
        public const string PC_TC_ARG7 = "arg7";
        public const string PC_TC_ARG8 = "arg8";
        //T&C Changes CH1 - END - Added the below constants for Terms and condtion page change
        public const string PC_TC_URL="../Controls/TermsAndConditions.aspx?args={0}";
        public const string PC_TC_SCRIPT_KEY = "opewindow";
        public const string PC_TC_SCRIPT_ONE = "<script type=text/javascript> window.open('";
        public const string PC_TC_SCRIPT_TWO = "','termsAndContions','resizable=1,scrollbars=1'); </script>";
        public const string PC_TC_LOG_NAME = "Terms and Conditions";
        public const string PC_CNTRL_NAME = "policySearchDetails";
		//MAIG - CH5 - BEGIN - Modified the below constants as part of MAIG
		public const string PC_CC_NUMERIC_REGX = "[^a-z '.,-]+";
		public const string PRINT_URL = "../forms/PrintEnrollmentConfirmationEC.aspx?args={0}";
		public const string PC_EDIT_RESPONSE = "Enrollment was successful.";
		//MAIG - CH5 - END - Modified the below constants as part of MAIG
        public const string PC_LOG_STATUS_REQUEST_SUCCESS = "Success Response to enroll/modify for policy number ";
        public const string PC_LOG_CNFRM_NUMBER="Confirmation number: ";
        
        public const string PC_RECUR_DETAIL = "Recurring Details";
        public const string PC_CreditCard_Code = "CreditCard_Code";
        public const string PC_ElectronicFund_Code = "ElectronicFund_Code";
        public const string PC_ApplicationContext_Payment_Tool = "ApplicationContext_Payment_Tool";
        public const string PC_VALIDENROLL_ERR = "No proper response from validation service";
        public const string INS_Product_Type_Table = "INS_Product_Type";
        public const string PaymentCentral_Product_Code_table= "PaymentCentral_Product_Code";
        public const string PC_YES_NOTATION = "Y";
        public const string PC_NO_NOTATION = "N";
        public const string PC_ProductType_PC = "_ProductType_PC";
        public const string PC_BillingPlan = "BillingPlan";
        public const string PC_VWSTATE_PolicyNumber = "vsPolicyNumber";
        public const string viewstate_ValidationService_enrollment = "viewstate_ValidationService_enrollment";
        public const string viewstate_ValidationService_Modifyenrollment = "viewstate_ValidationService_Modifyenrollment";
        public const string viewstate_ValidationService_Unenrollment = "viewstate_ValidationService_Unenrollment";
        public const string PC_CNFRMNUMBER = "ConfirmNo";
        public const string PC_POLICYNUMBER = "PolicyNo";
        public const string PC_CCNumber = "CCNumber";
        public const string PC_CCType = "CCType";
        public const string PC_IS_ENROLLED = "IsEnrolled";
        public const string PC_VLD_MDFY_ENROLLMENT = "validateModifyEnrollment";
        public const string PC_VLD_ENROLLMENT = "validateEnrollment";
        public const string PC_CCEXP = "CCExp";
        public const string PC_PAYMT_TYPE = "PaymentType";
        public const string PC_CUST_NAME = "CustomerName";
        public const string PC_EMAIL_ID = "Emailid";
        public const string PC_EDIT_CC = "EditCC";
        //CHG0072116 - PC Edit Card Details CH3:Added constants for notification Email
        public const string PC_EDIT_CONST_RESPONSE = "Response";
        //CHG0072116 - PC Edit Card Details CH4:Added constants for notification Email
        public const string PC_RECURR_CNFRM_URL="../Forms/RecurringConfirmation.aspx";
        public const string PC_TYPE_PUP = "PU";
        public const string PC_PUP = "PUP";
        public const string PC_CC = "CC";
        public const string PC_MASK_TXT = "XXXXXXXXXXXX";
        public const string PC_EC_ACC_NUMBER = "AccNumber";
        public const string PC_EC_BANK_NAME = "BankName";
        public const string PC_EC_ROUTING_NUMBER="RoutingNumber";
        public const string PC_EC_ACCOUNT_TYPE = "AccType";
        public const string PC_LOG_FAIL = "Failure response received to enroll/modify for policy number ";
        public const string PC_ENROLL_REDIRECT_URL = "/PaymentToolmsc/Forms/ManageEnrollment.aspx";
        public const string PC_BILL_PLAN = "Bill Plan";
        public const string BillPlan = "BillPlan";
        public const string PC_ProductCode_PUP="ProductCode.PUP";
        public const string PC_PAYMENT = "Payment";
        public const string PC_PAYMENT_TYPE = "PaymentType";
        public const string PC_GET_CREDITCARDS = "GetCreditCards";
        public const string PC_TBL_CC = "CREDITCARD_TABLE";
        public const string PC_CRD_TYPE_VISA = "VISA";
        public const string PC_CRD_TYPE_MASTER = "MASTR";
        public const string PC_CRD_TYPE_MASTER_EXPANSION = "MASTER";
        public const string PC_CRD_TYPE_MASTER_EXPANSION_DROPDOWN = "MASTERCARD";
        public const string PC_LOG_REQ_SENT = "Request sent to enroll/modify for policy number ";
        public const string PC_DO_AUTHIVR = "98";
        public const string PC_GRID_POLICY = "Policy Number";
        public const string PC_GRID_Status = "Status";
        public const string PC_ERR_INVALID_ACCHOLDER_NAME = "Enter a Valid Account Holder Name ";
        public const string PC_EC_INVALID_RNUMBER="Check digit for Routing Number is invalid";
        public const string PC_EC_RNUMBER_CHKDIGIT="Routing Number Check Digit";
        public const string PC_Enrolled = "Enrolled";
        public const string PC_MinDue = "Min Due";
        public const string PC_TotalBalance = "Total Balance";
        public const string PC_Due_Date = "Due Date";
        public const string PC_Eff_Date = "Eff Date";
        public const string PC_Exp_Date = "Exp Date";
        public const string PC_VALIDENROLL_FAILMSG = "Validation service failure message: ";
        public const string PC_EC_ERR_INVALID_REENTERED_AC = "The account numbers entered don't match.Please re-enter.";
        public const string PC_UPDATE_ENROLL_MSG = "Enrollment details were updated successfully.";
        public const string PC_EC_ERR_ACNUMBER_REQ = "Account Number is required.";
        public const string PC_THANKU_ENROLL="Thank you for enrolling in recurring payment.";
        public const string PC_EC_ERR_REENTEREDAC_REQ = "Re-enter Account Number is required.";
        public const string PC_EC_ERR_BANK_ID_REQ = "Bank ID Number(Routing Number) is required. ";
        public const string PC_EC_ERR_ACCHOLDER_REQ = "Account Holder Name is required.";
        public const string PC_EC_PAYMNT_TYPE = "EFT";
        public const string PC_EC_CHKG_PREFIX = "C";
        public const string PC_EC_SAVG_PREFIX = "S";
        public const string PC_BILL_POL_NUMBER = "POL_NUMBER";
        public const string PC_BILL_POL_TYPE = "POL_TYPE";
        public const string PC_BILLPOL_PREFIX = "POL_PREFIX";
        public const string PC_BILL_SOURCE_SYSTEM = "SOURCE_SYSTEM";
        public const string PC_BILL_Due_Amount = "Due_Amount";
        public const string PC_BIL_PaymentPlan = "PaymentPlan";
        public const string PC_BILL_Due_Date = "Due_Date";
        public const string PC_TermExpirationDate = "TermExpirationDate";
        public const string PC_StatementDate = "StatementDate";
        public const string PC_BILL_PAYMENT_RESTRICTION = "PAYMENT_RESTRICTION";
        public const string PC_BILL_TermEffectiveDate = "TermEffectiveDate";
        public const string PC_BILL_RenewalFlag = "RenewalFlag";
        public const string PC_BILL_Total_Amount = "Total_Amount";
        public const string PC_LOG_EMAIL_POPUP = "Failure response received to unenroll for policy number";
        public const string PC_VLD_UNENROLL="validateUnEnroll";
        public const string PC_LBLERRORMSG = "lblErrorMsg";
        public const string FULL_DATE_TIME_FORMAT = "MM/dd/yyyy hh:mm:ss tt";
        public const string PC_SMTPSERVER="SMTPServer";
        public const string PC_viewstate_PolicyNo = "viewstate_PolicyNo";
        public const string PRINT_EnrolledDate = "EnrolledDate";
        public const string PRINT_PolicyNum = "PolicyNum";
        public const string PRINT_AcntType = "AcntType";
        public const string PRINT_AccNum = "AccNum";
        public const string PRINT_CustName = "CustName";
        public const string PRINT_ExpDate = "ExpDate";
        public const string PRINT_EmailID = "EmailID";
        
        public const string ENROLL_SUCCESS_MAIL = "Enrollment Successful";
        public const string MODIFY_SUCCESS_MAIL = "Enrollment Update Successful";
        //CHG0072116 - PC Edit Card Details CH5:Added constants for notification Email
        public const string UPDATE_SUCCESS_MAIL = "Your AAA Enrollment Confirmation";
        public const string PASS_MAIL = "pass";
        public const string INS_REPORTS_SP_START = "SP Execution starts - INS_Reports.";
        public const string INS_REPORTS_SP_END = "SP Execution Ends - INS_Reports.";
        public const string PAY_Search_Orders_SP_START = "SP Execution starts - PAY_Search_Orders.";
        public const string PAY_Search_Orders_SP_END="SP Execution Ends - PAY_Search_Orders.";
        public const string TRANS_SEARCH_USERID = "Transaction search Dataset creation call. UserID: ";
        public const string PARAMETER = "Parameter :";
        public const string TRANS_SEARCH_DATASET= "Transaction search dataset created";
        //CHG0072116 - PC Edit Card Details CH6:Added constants for logging
        //CHG0109406 - CH4 - BEGIN - Modified the Error message from "policy retrival service" to "Retrieve Autopay Enrollment Service" 
        public const string PC_UPCC_PToken_Empty = "Payment token is empty in the response of Retrieve Autopay Enrollment service.";
        //CHG0109406 - CH4 - END - Modified the Error message from "policy retrival service" to "Retrieve Autopay Enrollment Service"
        public const string INS_REPORT_USERID = "Insurance My reports Dataset creation call. UserID: ";
        public const string INS_REPORT_DATASET = "Insurance My reports dataset created";
        public const string INS_REPORT_USERID_ALL = "Insurance reports Dataset creation call. UserID: ";
        public const string INS_REPORT_DATASET_ALL = "Insurance reports dataset created";
        //CHG0118686 - PRB0045696 - Message Changes - Start - FR1,FR2,FR3,FR4//
        public const string POLICY_Search_InvalidPolicy = "INVALID POLICY NUMBER";
        public const string POLICY_Search_UnavailablePolicy = "Policy Not Found.";
        public const string POLICY_Search_Status_Unavailable = "Information unavailable for this item";
        public const string POLICY_SOAB301_Invalid_Unavailable_Policy = "There is no matching policy found for the given search criteria. Please verify and enter valid policy details.";
        public const string POLICY_SOAB301_Status_Unavailable_Policy = "There is a problem retrieving policy details. Please verify and enter valid policy detail. If problem persists contact support.";
        public const string POLICY_Payment_Declined = "We are unable to complete your payment request due to a problem with the payment card.";
        //CHG0118686 - PRB0045696 - Message Changes - Start - FR1,FR2,FR3,FR4//
        //Added Constants for ACH ENrollment Pilot changes//
        public const string PC_ACH_PolicyNumber = "Policy #";
        public const string PC_ACH_Product = "Product";
        public const string PC_ACH_BillSystem = "Billing System";
        public const string PC_ACH_Cus_Name = "Customer Name";
        public const string PC_ACH_Enroll_Status = "Enrollment Status";
        public const string PC_ACH_Eligibilty_Status = "Eligibility";

        //CHG0129017 - Server Upgrade 4.6.1
        //Added constants for file names accessed in CheckAccess() in ACL.cs
        public const string ACL_Check_Insurance = "insurance";
        public const string ACL_Check_User = "user";
        public const string ACL_Check_DO = "do";
        public const string ACL_Check_Billing = "billing";
        public const string ACL_Check_Err = "general_error";
        public const string ACL_Check_PayConfirmation = "paymentconfirmation";
        public const string ACL_Check_Confirmation = "confirm";
        public const string ACL_Check_Recurring = "recurringconfirmation";
        public const string ACL_Check_UnEnroll = "unenrollmentconfirm";
        public const string ACL_Check_Reissue_Receipt = "turnin_void_reissue_receipt";
        public const string ACL_Check_Reissue_Confirmation = "turnin_void_reissue_confirmation";
        public const string ACL_Check_ManualUpdate = "manual_update";
        public const string ACL_Check_SalesRep_Confirmation = "salesrep_confirmation";
        public const string ACL_Check_CashierRecon = "cashierrecon_confirmation";
        public const string ACL_Check_Admin = "admin_contact";      

        //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Start
        public const string Sch_PolicyNumber = "Policy Number";
        public const string Sch_Recurring_Status = "Status";
        public const string Sch_Enrolled = "Enrolled";
        public const string Sch_Plan = "Bill Plan";
        public const string Sch_Due = "Min Due";
        public const string Sch_Balance = "Total Balance";
        public const string Sch_DueDate = "Due Date";
        public const string Sch_EffDate = "Eff Date";
        public const string Sch_ExpDate = "Exp Date";
        //Scheduled Payments Section
        public const string Sch_PymtDate = "Scheduled PMT Date";
        public const string Sch_Amt = "Amount";
        public const string Sch_Email = "Email";
        public const string Sch_PymtMethod = "PMT Method";
        public const string Sch_Last_4_AccNo = "PMT Acct #";
        public const string Sch_Pymt_Status = "Status";
        public const string Sch_Confirmation_No = "Confirmation Number";
        //Scheduled Payments History Section
        public const string Sch_PymtDate_Time = "PMT Date";
        public const string Sch_Event = "Activity / Event";
        public const string Sch_Status = "Status";
        public const string Sch_Initiated = "Initiated by";
        public const string Sch_Source = "Source System";
        public const string Sch_Channel = "Channel";
        public const string Sch_PymntID = "PMT ID";

        public const string Server_Variable = "HttpContext.Current.Request.ServerVariables";
        //CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - End

    }
}
