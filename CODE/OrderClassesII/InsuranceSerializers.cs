/*
 * MODIFIED BY COGNIZANT AS PART OF Q4 RETROFIT
 * 12/15/2005 Q4-Retrofit.Ch1:Added a new property IsHUONAuto and its associated private variable _IsHUONAuto 
 *			  for validating the check digit for HUON Auto policies
 * 12/15/2005 Q4-Retrofit.Ch2:Modified ValidPolicy function to validate 9 digit HUON Auto policy number
 * * SR#8494947.Ch1-DuplicatePaymentLogging : Added by cognizant on  Sep 08 2009 to log the policies with duplicate payments.
 * 11/03/2009 Billing and Payments Quick Hits Project- RFC 48347:Added a new variable and property IsOverride to hold the value 0 or 1 for tracking override product types. 
 * 11/27/09 - RFC48347.Ch2 - Modified by COGNIZANT for Billing and Payments Quick Hits project
 *			This change has been done to made the IsDuplicate and IsOverride as optional attributes for XML inputs from other interfacing applications (such as CSAA.com, IVR)
 *			1) Added System.ComponentModel namespace to this class to use the DefaultValue syntax to assign default values for XML attributes
 *			2) Made the variables _IsDuplicate, _IsOverride as private and assigned default values
 *			3) Assigned default values for the XML attributes - IsDuplicate and IsOverride
 * 11/30/209-RFC48347.Ch2 -Modified by COGNIZANT for Billing and Payments Quick Hits project
 * 1)Added a new property SysProductType to the InsuranceLine Item class.This property is used to hold the ProductType 
 * value while navigating forth and back to the other pages as a partof defect 120.
 * 05/27/2020 MP2.0.Ch1 - New method added for Exigen policy validation - by Cognizant
 * 06/01/2010 MP2.0.Ch2 - New boolean validation set for exigen - by Cognizant
 * 05/27/2010 MP2.0.Ch3 - Changes made by cognizant on  for exigen auto policy
 * 07/30/2010 MP 2.0  defect # 904 - new key value pir added to web.conif and corresponding code changes added to fix the bug - by Cognizant
 *  01/10/2011 PUP CH1:Added new validator for validating pup policy in validate policy - by  cognizant
 *  01/12/2011 PUP CH2:Added the condition to check policy number for  valid PUP policy- by cognizant
 *  03/18/2011 PUP CH3:Modified the condition  for converting the first 3 chararcters Toupper case - by cognizant
 *  * 03/18/2011 PUP CH4:Modified the condition  for converting the first 3 chararcters Toupper case - by cognizant
 * 67811A0  - PCI Remediation for Payment systems.CH1 : Call Validation for Partner club products to check for policystate and policy prefix.
 * 67811A0  - PCI Remediation for Payment systems.CH2 : Validation for Western United products to check for missing policystate and policy prefix in order detail info.
 * 67811A0  - PCI Remediation for Payment systems.CH3 : Validate Partner Club Products of lengh between 4 to 10 and numeric. 
 * 67811A0  - PCI Remediation for Payment systems.CH4 : New function to Split Policy number when it it appended with policystate and policy prefix for PCP products.
 * 67811A0  - PCI Remediation for Payment systems.CH5 : Check if it is not a PCP product type.
 * //CSAA.com - added the condition toupper to prevent the policy validation failure for the Leacy and exigen polices by cognizant on 12/23/2011
 * 67811A0  - PCI Remediation for Payment systems.CH6,CH7 : To validate the HO6 policy sequence for CEA product code by cognizant on 02/27/2012
Security Defect - CH1 - Added the max lengh and junk character validation in the below attributes.
Security Defect- CH2 -Added the below code to validate the fields in insurance line item
 * AZ PAS Conversion and PC integration - Added the below code to assign the policy number alone to the Policy variable in case of non PCP transactions from UI
 * CHG0104053 - PAS HO CH1 - Added the below code to allow all states,no validation, since it is available in RBPS  6/26/2014
 * MAIG - CH1 - Modified the logic to Support Common Product Types and to check if it is an valid policy number, Also commenented the unwanted logic
 * MAIG - CH2 - Commeneted the code as part of Common Product Change
 * MAIG - CH3 - Added condition to skip validation if the policy length is 13, for an PAS Home Policy
 * MAIG - CH4 - Added condition to skip validation if the policy length is 13, for an PAS Home Policy
 */
using System;
using System.Collections;
using CSAAWeb.Serializers;
using OrderClasses;
using System.Xml.Serialization;
using System.Text.RegularExpressions;
using System.ComponentModel;
using CSAAWeb;
using CSAAWeb.AppLogger;

using System.Configuration; // RFC48347.Ch2 - Added by COGNIZANT 11/27/09 - To assign default value to XML attributes using DefaultValue syntax

namespace InsuranceClasses
{
    #region InsuranceLineItem
    /// <summary>
    /// Line item for an insurance order.
    /// </summary>
    public class InsuranceLineItem : LineItem
    {
        ///<summary>Default constructor</summary>
        public InsuranceLineItem() : base() { SetCodes(); }
        ///<summary>Xml constructor</summary>
        public InsuranceLineItem(string Xml) : base(Xml) { SetCodes(); }
        ///<summary>Constructor from another object</summary>
        public InsuranceLineItem(Object O) : base(O) { SetCodes(); }
        // Modified by cognizant on 2/9/2005 new variable _ProductType added
        private string _ProductType;
        // Modified by cognizant on 2/17/2005 new variable _ProductName added
        private string _ProductName;

        private string StrPrefix;

        //SR#8494947.Ch2-DuplicatePaymentLogging - START: Added by cognizant on  Sep 08 2009 to log the policies with duplicate payments.
        ///<summary>Which is used to hold the Value of duplicate Payment  i.e either '0' or '1'</summary>
        private int _IsDuplicate = 0;	// RFC48347.Ch2 - Modified by COGNIZANT 11/27/09 - Assigned default value and made private


        //Added by cognizant as a part of Billing and Payments Quick Hits Project- RFC 48347  on 3Nov 2009.
        //IsOverride variable is used to hold the value 0 or 1.
        private int _IsOverride = 0;	// RFC48347.Ch2 - Modified by COGNIZANT 11/27/09 - Assigned default value and made private

        //Added by cognizant as a part of fix of  billing and Payments Quick Hits Project- RFC 48347 defect 120 on 30 Nov 2009.
        //The variable is used to assingn the value of product type to the property while navigating to the other pages.
        private string _SysProductType = "";
        ///<summary/>
        private void SetCodes()
        {
            ProductName = "Insurance";
            ProductCode = "INS";
        }
        ///<summary/>
        public string NameCheck
        {
            get
            {
                if (LastName == null) return "";
                if (LastName.Length <= 2) return LastName;
                return LastName.Substring(0, 2).ToUpper();
            }
        }
        ///<summary/>
        ///Added by Cognizant on 26/06/2004 for validating the policy number with respect to WU product
        ///
        private bool validateRequired = true;
        /// <summary>
        /// Function added by Cognizant on 26/06/2004 for Stopping the policy number validation for WU product
        /// </summary>
        public bool PolicyValidate
        {
            set
            {
                validateRequired = value;
            }
            get
            {
                return validateRequired;
            }

        }
        //SR#8494947.Ch2-DuplicatePaymentLogging : Added by cognizant on  Sep 08 2009 to log the policies with duplicate payments.
        ///<summary>Property which is used to hold the Value of duplicate Payment i.e either '0' or '1'</summary>
        [XmlAttribute]
        [DefaultValue(0)]	// RFC48347.Ch2 - Added by COGNIZANT 11/27/09 - Assigned default value
        public int IsDuplicate
        {
            get
            {
                return _IsDuplicate;
            }
            set
            {
                _IsDuplicate = value;
            }
        }
        //Added by cognizant as a part of Billing and Payments Quick Hits Project- RFC 48347  on 3Nov 2009.
        //Property used to hold the value 1 if product type is Overridded else 0.
        [XmlAttribute]
        [DefaultValue(0)]	// RFC48347.Ch2 - Added by COGNIZANT 11/27/09 - Assigned default value
        public int IsOverride
        {
            get
            {
                return _IsOverride;
            }
            set
            {
                _IsOverride = value;
            }
        }

        //Added by cognizant as a part offix of  Billing and Payments Quick Hits Project- RFC 48347 defect  120 on 30 Nov 2009.
        //Property used to hold the value of product Type when navigating to the other pages.
        [XmlAttribute]
        [DefaultValue("")]
        public string SysProductType
        {
            get { return _SysProductType; }
            set
            {
                _SysProductType = value;

            }
        }
        //Q4-Retrofit.Ch1-START:Property to Identify 9 digit HUON Auto policy number
        private bool _IsHUONAuto = false;

        public bool IsHUONAuto
        {
            set
            {
                _IsHUONAuto = value;
            }
            get
            {
                return _IsHUONAuto;
            }

        }


        // MP2.0.Ch3 - New boolean variable added by cognizant on 05/27/2010 for exigen auto policy
        private bool _IsExigenAuto = false;
        //[XmlAttribute]
        //[DefaultValue(false)]	
        //public bool IsExigenAuto
        //{
        //    set
        //    {
        //        _IsExigenAuto = value;
        //    }
        //    get
        //    {
        //        return _IsExigenAuto;
        //    }
        //}


        public void SetIsExigenAuto(bool setValue)
        {
            _IsExigenAuto = setValue;
        }

        public bool GetIsExigenAuto()
        {
            return _IsExigenAuto;
        }

        //Q4-Retrofit.Ch1-END
        ///<summary>Policy number.</summary>
        ///Security Defect - CH1 Start -- Added the max lengh and junk character validation in the below attributes.
        [XmlAttribute]
        [ValidatorRequired]
        //AZ PAS Conversion and Payment Centrl changes - Modified the length from 25 to 26
        [ValidatorMaxLength(Length = 26)]
        //[ValidatorExactLength(Length=7)] Commented by Cognizant on 25/6/2004 for avoiding the Policy validation for WU products
        //[ValidatorAlphaNumeric]
        [Validator(Validation = "ValidPolicy", Code = "50052", Message = "Validation failed in ", Priority = 3)]
        public string Policy
        {
            get { return AccountNumber.Trim(); }
            set { AccountNumber = value; }
        }
        ///<summary>Identity for product type.</summary>
        //Code modified by cognizant on 2/9/2005 changed projectcode to product type
        //Added by Cognizant 2/15/2005 For override product type property using orderclasses.lineitem.ProductTypeNew 


        [XmlAttribute]
        [ValidatorRequired]
        [ValidatorJunk]
        [ValidatorMaxLength(Length = 10)]
        // 67811A0  - PCI Remediation for Payment systems.CH1 :START- Call Validation for Partner club products to check for policystate and policy prefix.
        [Validator(Validation = "ValidatePolicystatePrefix", Code = "MISSING_POLICYSTATEPREFIX", Message = "Validation failed in ", Priority = 3)]
        // 67811A0  - PCI Remediation for Payment systems.CH1 :END
        public string ProductType
        {
            get { return _ProductType; }
            set
            {
                _ProductType = value;
                ProductTypeNew = value;
            }
        }
        //Security Defect - CH1 End -- Added the max lengh and junk character validation in the below attributes.
        // Modified by Cognizant 2/17/2005 New attribute ProductName added. If empty "Insurance" is passed.
        [XmlAttribute]
        [ValidatorMaxLength(Length = 50)]
        [ValidatorJunk]
        public override string ProductName
        {
            get
            {
                return _ProductName.Trim();
            }
            set
            {
                _ProductName = value;
                if (_ProductName == "")
                    _ProductName = "Insurance";

            }
        }
        //		public string ProductType 
        //		{
        //				  get {return ProductCode;}
        //	   			  set {ProductCode=value;}
        //		}


        /*		
        ///<summary>Policy holder's last name.</summary>
        [XmlAttribute]
        [ValidatorRequired]
        [Validator(Validation="CheckName", Code="INVALID_NAME", Message="Last Name should be alphanumeric.", Priority=3)]
        public string LastName;

        /// <summary>
        /// Added by Cognizant on 15/8/2004 for Adding a new XML attribute FirstName
        /// </summary> 
        ///<summary>Policy holder's First name.</summary>
        [XmlAttribute]
        public string FirstName;
*/
        private static Regex ExCheckName = new Regex("[^a-z0-9 '-]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        ///<summary>Name check validates that the name has only alpha, digits, space or '</summary>
        protected void CheckName(ValidatorAttribute Source, ValidatingSerializerEventArgs e)
        {
            if (ExCheckName.IsMatch(LastName) == true) e.Error = new ErrorInfo(Source, e.Field);
        }
        //67811A0 - PCI Remediation for Payment systems.CH2: START- Validation for Western United products to check for missing policystate and policy prefix in order detail info
        ///<summary>Validates the Partner club products to check for policystate and policy prefix</summary>
        protected void ValidatePolicystatePrefix(ValidatorAttribute Source, ValidatingSerializerEventArgs e)
        {
            //MAIG - BEGIN - CH2 - Commeneted the code as part of Common Product Change
            /*if ((ConfigurationSettings.AppSettings["PCP.Products"]).IndexOf(Convert.ToString(ProductType)) > -1)
            {
                string[] strsplitpolicy;
                strsplitpolicy = Policy.Split('-');
                string PolicyState = strsplitpolicy[0].Trim();
                string PolicyPrefix = strsplitpolicy[1].Trim();
                if (PolicyPrefix == "" || PolicyState == "") e.Error = new ErrorInfo(Source, e.Field);
            }*/
            //MAIG - END - CH2 - Commeneted the code as part of Common Product Change
        }
        //67811A0 - PCI Remediation for Payment systems.CH2: END

        ///<summary>Splits the Account Number</summary>
        // 67811A0  - PCI Remediation for Payment systems.CH4:START -New function to Split Policy number when it it appended with policystate and policy prefix for PCP products.
        public string Splitpolicynumber(string PolicyNumber)
        {
            string[] StrSplitPolicy;
            string StrPolicy;
            StrSplitPolicy = PolicyNumber.Split('-');
            StrPrefix = StrSplitPolicy[1].Trim();
            StrPolicy = StrSplitPolicy[2].Trim();
            return StrPolicy;
        }
        // 67811A0  - PCI Remediation for Payment systems.CH4:END 

        ///<summary>Validates the policy number check digit</summary>
        protected void ValidPolicy(ValidatorAttribute Source, ValidatingSerializerEventArgs e)
        {
            //CSAA.com - added the condition toupper to prevent the policy validation failure for the Leacy and exigen polices by cognizant on 12/23/2011
            Policy = Policy.ToUpper();
            bool IsValid = true;
            bool IsExigenValid = true;
            bool IsPupValid = true;
            //Security Defect- CH2 -START -Added the below code to validate the fields in insurance line item
            if (FirstName.Trim() == "")
            {
                AddError(new ErrorInfo("50008", "Valdiation failed in FirstName", "FirstName"));
            }
            if (LastName.Trim() == "")
            {
                AddError(new ErrorInfo("50009", "Valdiation failed in LastName", "LastName"));
            }
            if (Description.Trim().Length > 100)
            {
                AddError(new ErrorInfo("50004", "Valdiation failed in Description", "Description"));
            }
            if (Price.ToString() == "" || Price <= 0 || Price > 25000 || !CSAAWeb.Validate.IsDecimal(Price.ToString()))
            {
                AddError(new ErrorInfo("50016", "Valdiation failed in Price", "Price"));
            }
            if (Amount.ToString() == "" || Amount <= 0 || Amount > 25000 || !CSAAWeb.Validate.IsDecimal(Amount.ToString()))
            {
                AddError(new ErrorInfo("50016", "Valdiation failed in Amount", "Amount"));
            }
            //if (Tax_Amount.ToString() == "" || Tax_Amount <= 0 || Tax_Amount > 25000 || !CSAAWeb.Validate.IsDecimal(Tax_Amount.ToString()))
            //{
            //    AddError(new ErrorInfo("50016", "Valdiation failed in Tax_Amount", "Tax_Amount"));
            //}
            if (Quantity.ToString() == "" || Quantity <= 0 || Quantity > 10 || !CSAAWeb.Validate.IsDecimal(Quantity.ToString()))
            {
                AddError(new ErrorInfo("50017", "Valdiation failed in Quantity", "Quantity"));
            }
            if (RevenueType == "" || RevenueType.Length > 40 || junkValidation(RevenueType.ToString()))
            {
                AddError(new ErrorInfo("50018", "Valdiation failed in RevenueType", "RevenueType"));
            }
            //MAIG - CH1 - BEGIN - Modified the logic to Support Common Product Types and to check if it is an valid policy number, Also commenented the unwanted logic
            //Security Defect- CH2 -END -Added the below code to validate the fields in insurance line item
            //AZ PAS Conversion and PC integration - START - Added the below code to assign the policy number alone to the Policy variable in case of non PCP transactions from UI
            Policy=Policy.Trim();

            if (ProductType.Equals("PA") || ProductType.Equals("HO"))
            {
                if (!(CSAAWeb.Validate.IsAlphaNumeric(Policy.Trim())) || (Policy.Length < 4 || Policy.Length > 13))
                {
                    IsValid = false;
                }
            }
            else if (ProductType.Equals("PU"))
            {
                //MAIG - CH3 - BEGIN - Added condition to skip validation if the policy length is 13, for an PAS Home Policy
                if (!(Policy.Trim().Length == 13 && CSAAWeb.Validate.IsAlphaNumeric(Policy.Trim())))
                {
                    if (!CSAAWeb.Validate.IsAllNumeric(Policy.Trim()))
                    {
                        IsValid = false;
                        e.Error = new ErrorInfo("INVALID_CHARACTERS", CSAAWeb.Constants.PUP_NUMERIC_VALID, "Policy");
                        return;
                    }
                    else if (Policy.Length != 7)
                    {
                        IsValid = false;
                        e.Error = new ErrorInfo("INVALID_LENGTH", CSAAWeb.Constants.POLICY_LENGTH_HOME_MESSAGE, "Policy");
                        return;
                    }
                }
                //MAIG - CH3 - END - Added condition to skip validation if the policy length is 13, for an PAS Home Policy
            }
            else if (ProductType.Equals("DF") || ProductType.Equals("MC") || ProductType.Equals("WC"))
            {
                //MAIG - CH4 - BEGIN - Added condition to skip validation if the policy length is 13, for an PAS Home Policy
                if (!(ProductType.Equals("DF") && Policy.Trim().Length == 13 && CSAAWeb.Validate.IsAlphaNumeric(Policy.Trim())))
                {
                    if (Policy.Length < 4 || Policy.Length > 9)
                    {
                        IsValid = false;
                    }
                    else if (!CSAAWeb.Validate.IsAllNumeric(Policy))
                    {
                        IsValid = false;
                        e.Error = new ErrorInfo("INVALID_CHARACTERS", "Field must be Numeric.", "Policy");
                        return;
                    }
                }
                //MAIG - CH4 - END - Added condition to skip validation if the policy length is 13, for an PAS Home Policy
            }
            /*

            if ((ConfigurationSettings.AppSettings["PCP.Products"]).IndexOf(Convert.ToString(ProductType)) <= -1)
            {
                string[] StrSplitPolicy;
                StrSplitPolicy = Policy.Split('-');
                if (StrSplitPolicy[0] != null)
                {
                    Policy = StrSplitPolicy[0].Trim();
                }
            }
            //AZ PAS Conversion and PC integration -END - Added the below code to assign the policy number alone to the Policy variable in case of non PCP transactions from UI
            //if (Policy.Length >16)
            //{
            //    AddError(new ErrorInfo("50052", "Valdiation failed in Policy", "Policy"));
            //}
            // 67811A0  - PCI Remediation for Payment systems.CH3:START- Validate Partner Club Products of lengh between 4 to 10 and numeric
            if ((ConfigurationSettings.AppSettings["PCP.Products"]).IndexOf(Convert.ToString(ProductType)) > -1 )
            {
                if (Splitpolicynumber(Policy).Length >= 4 && Splitpolicynumber(Policy).Length <= 9)
                {
                    //* 67811A0  - PCI Remediation for Payment systems.CH6 : To validate the HO6 policy sequence for CEA product code
                    if (((ConfigurationSettings.AppSettings["PCP.StatePrefixCEA"]).IndexOf(Convert.ToString(StrPrefix)) > -1) && ((ConfigurationSettings.AppSettings["PCP.ProductTypeCEA"]).IndexOf(Convert.ToString(ProductType)) > -1))
                    //if ((StrPrefix == "HO6") && (ProductType.Trim() == "CEA"))
                    {
                        InsuranceClasses.Service.Insurance I = new InsuranceClasses.Service.Insurance();


                        if ((bool)I.ValidateCheckDigit(Splitpolicynumber(Policy)))
                        {
                            IsValid = true;
                        }
                        else
                        {
                            IsValid = false;
                            e.Error = new ErrorInfo("INVALID_Policy", "Policy Number Sequence is Invalid for Home policy", "Policy");

                            return;
                        }

                    }
                    else if (CSAAWeb.Validate.IsAllNumeric(Splitpolicynumber(Policy)))
                    {
                        IsValid = true;
                    }
                    else
                    {
                        IsValid = false;
                        e.Error = new ErrorInfo("INVALID_Policy", "Policy Number must be Numeric for Western United Products.", "Policy");
                        return;
                    }


                }
                else
                {
                    IsValid = false;
                    e.Error = new ErrorInfo("INVALID_LENGTH", "Policy Number Length must be  between 4 to 9 for Western United Products.", "Policy");
                    return;
                    //* 67811A0  - PCI Remediation for Payment systems.CH6 : To validate the HO6 policy sequence for CEA product code
                }
            }
            // 67811A0  - PCI Remediation for Payment systems.CH3:END
            //Start Added by Cognizant on 11/8/2004 
            else if (Policy != "" && validateRequired)
            {
                //Q4-Retrofit.Ch2-START: Added to check IsHUONAuto property to identify if it is HUON Auto 
                if (IsHUONAuto)
                {
                    //For HUON Auto products, the policy number is checked to see if it is purely numeric
                    IsValid = CSAAWeb.Validate.IsAllNumeric(Policy);
                    if (!IsValid)
                    {
                        e.Error = new ErrorInfo("INVALID_CHARACTERS", "Field must be Numeric.", "Policy");
                        return;
                    }
                }
                //PUP CH1:Start added new validator for validating pup policy by  cognizant on 1/10/2011
                //PUP CH4:Modified the condition  for converting the first 3 chararcters Toupper case - by cognizant on 03/18/2011 
                else if (Policy.Length == 10 && Policy.Substring(0, 3).ToUpper() == "PUP")
                {
                    string puPolicyno = Policy.Substring(3, 7);
                    if (!CSAAWeb.Validate.IsAllNumeric(puPolicyno))
                    {
                        IsPupValid = false;
                        e.Error = new ErrorInfo("INVALID_CHARACTERS", CSAAWeb.Constants.PUP_NUMERIC_VALID, "Policy");
                        return;
                    }
                    IsPupValid = true;
                }
                //PUP CH1:END added new validator for validating pup policy by  cognizant on 1/10/2011
                else if (GetIsExigenAuto() == true)
                {
                    // MP2.0.Ch2 - Start of Code - New Validation has been added for exigen on 1/6/2010 by Cognizant
                    if (Policy.Length == 13)
                    {

                        string stateCode = Policy.Substring(0, 2);
                        string prodCode = Policy.Substring(2, 2);
                        string exPolicyNo = Policy.Substring(4, 9);

                        string stateCodeValues = string.Empty;
                        string prodCodeValues = string.Empty;

                        // MP 2.0  defect # 904 - new code added to fix the bug - by Cognizant on 07/30/2010
                        string allowedExigenPolicyValues = string.Empty;
                        string stateProductCode = string.Empty;
                        stateProductCode = Policy.Substring(0, 4);


                        if (ConfigurationSettings.AppSettings["AllowedExigenPolicies"] != null && ConfigurationSettings.AppSettings["AllowedExigenPolicies"].ToString() != string.Empty)
                        {
                            allowedExigenPolicyValues = ConfigurationSettings.AppSettings["AllowedExigenPolicies"].ToString();
                        }

                        //CHG0104053 - PAS HO CH1 - START : Added the below code to allow all states,no validation, since it is available in RBPS  6/26/2014
                        if (!ProductType.Equals("AAASSH"))
                        {
                            if (allowedExigenPolicyValues.IndexOf(stateProductCode) < 0)
                            {
                                IsExigenValid = false;
                                e.Error = new ErrorInfo("INVALID_POLICY", "Invalid Policy. Please enter a valid policy and try again", "Policy");
                                return;
                            }
                        }
                        //CHG0104053 - PAS HO CH1 - END : Added the below code to allow all states,no validation, since it is available in RBPS  6/26/2014
                        //End Issue 904

                        if (!CSAAWeb.Validate.IsAllNumeric(exPolicyNo))
                        {
                            IsExigenValid = false;
                            e.Error = new ErrorInfo("INVALID_CHARACTERS", "The last 9 characters of the Policy No must be Numeric.", "Policy");
                            return;
                        }

                    }
                    else
                    {
                        IsExigenValid = false;
                        e.Error = new ErrorInfo("INVALID_LENGTH", "Policy Number must be of length 13", "Policy");
                        return;
                    }
                    // MP2.0.Ch2 - End of Code - New Validation has been added for exigen on 1/6/2010 by Cognizant
                }
                else
                {
                    //AlphaNumeric validation with Dashes 
                    IsValid = Regex.IsMatch(Policy.ToUpper(), @"^[A-Z0-9\-]+$");
                    if (!IsValid)
                    {
                        e.Error = new ErrorInfo("INVALID_CHARACTERS", "Field must be AlphaNumeric.", "Policy");
                        return;
                    }
                    //Q4-Retrofit.Ch2-END

                    //Format Validation with dashes(ex:"12-34-56-7")

                    if ((Policy.Replace("-", "").Length == 7) && (Policy.IndexOf("-") >= 0))
                    {
                        //Match the format "12-34-56-7"	
                        IsValid = (Regex.IsMatch(Policy.ToUpper(), @"^[0123456789ABCDEJKLNRPTVWXYFFHMU]{2,2}-[0-9]{2,2}-[0-9]{2,2}-[0-9]{1,1}$"));

                        if (IsValid)
                            Policy = Policy.Replace("-", "");
                        else
                        {
                            e.Error = new ErrorInfo("INVALID_POLICY", "The policy format is not valid.", "Policy");
                            return;
                        }
                    }
                }


            }
            //END
            if (Policy != "")
            {
                InsuranceClasses.Service.Insurance I = new InsuranceClasses.Service.Insurance();
                //Start Added by Cognizant on 24-06-2004 for stopping validation for WU products
                //Q4-Retrofit.Ch2-START: Added a check for IsHUONAuto to call HUON Check digit validation if it is HUON Auto product
                if (validateRequired)
                {

                    if (IsHUONAuto)
                        IsValid = (bool)I.ValidateHUONCheckDigit(Policy);
                    else if (GetIsExigenAuto() == true) //  MP2.0.Ch1 - new method added for Exigen policy validation - added by Cognizant on 27/05/2020
                        IsValid = IsExigenValid;
                    //PUP CH2:Added the condition to check policy number for  valid PUP policy by cognizant on 01/12/2011.
                    //PUP CH3:Modified the condition  for converting the first 3 chararcters Toupper case - by cognizant on 03/18/2011 
                    else if (Policy.Length == 10 && Policy.Substring(0, 3).ToUpper() == "PUP")
                    {
                        IsValid = IsPupValid;
                    }
                    //AZ PAS conversion and PC integration - CH2 - Added the below code to validate Highlimit policy
                    else if (Policy.Length == 10 && ProductType == Config.Setting("ProductCode.HighLimit") && CSAAWeb.Validate.IsAllNumeric((Policy)))
                    {
                        IsValid = true;
                    }
                    //AZ PAS conversion and PC integration - CH2 - Added the below code to validate Highlimit policy
                    // * 67811A0  - PCI Remediation for Payment systems.CH5  START Check if it is not a PCP product type.
                    else if ((ConfigurationSettings.AppSettings["PCP.Products"]).IndexOf(Convert.ToString(ProductType)) < 0)
                    // * 67811A0  - PCI Remediation for Payment systems.CH5  End Check if it is not a PCP product type.
                    {
                        IsValid = (bool)I.ValidateCheckDigit(Policy);
                    }
                }
                //Q4-Retrofit.Ch2-END
                //else if(Policy.Length==12)
                // * 67811A0  - PCI Remediation for Payment systems.CH5 :START Check if it is not a PCP product type.
                else if ((Policy.Length == 12) && (!validateRequired) && (ConfigurationSettings.AppSettings["PCP.Products"]).IndexOf(Convert.ToString(ProductType)) < 0)
                    IsValid = true;
                // * 67811A0  - PCI Remediation for Payment systems.CH5 :END
                // 67811A0  - PCI Remediation for Payment systems.CH3:START- Validate Partner Club Products of lengh between 4 to 10 and numeric
                else if ((ConfigurationSettings.AppSettings["PCP.Products"]).IndexOf(Convert.ToString(ProductType)) > -1)
                {
                    //* 67811A0  - PCI Remediation for Payment systems.CH7 Start : To validate the HO6 policy sequence for CEA product code
                    if (((ConfigurationSettings.AppSettings["PCP.StatePrefixCEA"]).IndexOf(Convert.ToString(StrPrefix.ToUpper())) > -1) && ((ConfigurationSettings.AppSettings["PCP.ProductTypeCEA"]).IndexOf(Convert.ToString(ProductType.ToUpper())) > -1))
                    //if ((StrPrefix == "HO6") && (ProductType == "CEA"))
                    {
                        // InsuranceClasses.Service.Insurance I = new InsuranceClasses.Service.Insurance();


                        if ((bool)I.ValidateCheckDigit(Splitpolicynumber(Policy)))
                        {
                            IsValid = true;
                        }
                        else
                        {
                            IsValid = false;
                            e.Error = new ErrorInfo("INVALID_Policy", "Policy Number Sequence is Invalid for Home policy", "Policy");
                            return;
                        }

                    }
                    else if (CSAAWeb.Validate.IsAllNumeric(Splitpolicynumber(Policy)))
                    {
                        IsValid = true;
                    }
                    else
                    {
                        IsValid = false;
                        e.Error = new ErrorInfo("INVALID_Policy", "Policy Number must be Numeric for Western United Products.", "Policy");
                        return;
                    }


                    //* 67811A0  - PCI Remediation for Payment systems.CH7 END : To validate the HO6 policy sequence for CEA product code       
                }
                // 67811A0  - PCI Remediation for Payment systems.CH3:END

                else
                {
                    IsValid = false;
                    //throw new CSAAWeb.BusinessRuleException("Policy (" + Policy + ") not valid.");
                }


                //END
                I.Close();
            } */
            //MAIG - CH1 - END - Modified the logic to Support Common Product Types and to check if it is an valid policy number, Also commenented the unwanted logic
            if (IsValid == false) e.Error = new ErrorInfo(Source, e.Field);
        }
    }

    /// <summary>
    /// Array of insurance order line items.
    /// </summary>
    [Validator(Validation = "CheckDuplicate", Code = "DUPLICATES_EXIST", Message = "There cannot be more than one entry for the same policy number, product type and revenue type.")]
    public class ArrayOfInsuranceLineItem : ArrayOfLineItem
    {
        ///<summary>Default constructor</summary>
        public ArrayOfInsuranceLineItem() : base() { }
        ///<summary>Xml constructor</summary>
        public ArrayOfInsuranceLineItem(string Xml) : base(Xml) { }
        ///<summary>Constructor from another collection</summary>
        public ArrayOfInsuranceLineItem(IList source) : base(source) { }

        /// <summary>
        /// Gets or sets the item at index.
        /// </summary>
        public new InsuranceLineItem this[int index]
        {
            get { return (InsuranceLineItem)InnerList[index]; }
            set { InnerList[index] = value; }
        }

        ///<summary>Validator deligate checks that there are no duplicate entries.</summary>
        protected void CheckDuplicate(ValidatorAttribute Source, ValidatingSerializerEventArgs e)
        {
            bool IsValid = true;
            string key = String.Empty;

            System.Collections.Specialized.HybridDictionary CheckList = new System.Collections.Specialized.HybridDictionary();

            foreach (InsuranceLineItem C in this)
            {
                key = C.ProductType.ToString() + " " + C.Policy + " " + C.RevenueType.ToString();
                if (CheckList.Contains(key)) IsValid = false;
                else CheckList.Add(key, C.RevenueType);

                if (IsValid == false) e.Error = new ErrorInfo(Source, "Duplicates");
            }
        }

    }
    #endregion
    #region InsuranceInfo
    /// <summary>
    /// Class used to prepare data for sending to web service.
    /// </summary>
    public class InsuranceInfo : ProductInfo
    {
        /// <summary>Default constructor</summary>
        public InsuranceInfo() : base() { }
        /// <summary>Xml constructor</summary>
        public InsuranceInfo(string Xml) : base(Xml) { }
        /// <summary>Constructor from another Object</summary>
        public InsuranceInfo(Object O) : base(O) { }

        /// <summary>Miscellaneous order detail.</summary>
        [CustomCopy(DeepCopy = true)]
        public OrderDetailInfo Detail;
        ///<summary>The order line items.</summary>
        [CustomCopy(Ignore = true)]
        public ArrayOfInsuranceLineItem Lines;
        ///<summary></summary>
        public override string ProductName { get { return "Insurance"; } }

        private Service.Insurance DataConnection;

        /// <summary>
        /// Gets basic rate info for the object.  There are none for this product,
        /// so just returns true.
        /// </summary>
        public override void GetRates(OrderInfo Order) { }
        /// <summary>
        /// Sets the Order lines for each of the lines in the line items.
        /// </summary>
        public override void UpdateLines(OrderInfo Order)
        {
            Order.Lines.Clear(ProductName);
            Order.Lines.Append(Lines);
        }
        /// <summary>
        /// Creates an object that is prepared for entering an insurance order.
        /// </summary>
        /// <param name="Order">Order object to get data from.</param>
        public InsuranceInfo Prepare(OrderInfo Order)
        {
            InsuranceInfo Result = new InsuranceInfo(this);
            Result.CopyFrom(Order);
            Result.Detail = new OrderDetailInfo(Order.Detail);
            Result.Detail.Amount = Lines.Total;
            return Result;
        }
        /// <summary>Process this part of the order, prior to payment authorization</summary>
        public override void Begin_Transaction(OrderInfo Order)
        {
            DataConnection = new Service.Insurance();
            OrderID = DataConnection.Begin_Transaction(this.Prepare(Order));

            DataConnection.Close();
            DataConnection = null;
        }
        /// <summary>Complete processing this part of the order after payment authorization.</summary>
        /// <param name="AuthCode">The payment authorization code</param>
        /// <param name="RequestID">The payment processor Request ID</param>
        /// <param name="Successful">True if the payment was successful.</param>
        /// <param name="ReceiptNumber">The receipt number</param>
        public override void Complete_Transaction(string AuthCode, string RequestID, bool Successful, string ReceiptNumber)
        {
            if (DataConnection == null) DataConnection = new Service.Insurance();

            DataConnection.Complete_Transaction(OrderID, AuthCode, RequestID, ReceiptNumber);
            if (Detail != null) Detail.ReceiptNumber = ReceiptNumber;
            if (Successful || AuthCode != "DCALL") OrderID = 0;
            DataConnection.Close();
            DataConnection = null;
        }
    }
    #endregion
}
