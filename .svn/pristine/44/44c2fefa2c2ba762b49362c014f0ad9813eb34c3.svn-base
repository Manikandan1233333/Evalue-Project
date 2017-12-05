/* MODIFIED BY COGNIZANT
 * STAR Retrofit II Changes: 
 * Modified as a part of CSR#5166
 * 02/01/2007 STAR Retrofit II.Ch1: Added a new method ValidNumeric() to validate the credit card for numeric characters
 * 02/01/2007 STAR Retrofit II.Ch2: Added a new method ValidCCNumber() to invoke the check digit algorithm in CSAAWeb.Cryptor for validating the credit card number.       
 * 02/01/2007 STAR Retrofit II.Ch3: Created validators vldCardNum and vldCardNum1 to validate the Credit Card Number using LUHN Modulus 10 algorithm
				                   and to validate credit card for numeric characters respectively.
 * STAR Retrofit III Changes: 
 * Modified as a part of CSR #5595
 * 04/02/2007 STAR Retrofit III.Ch1: 
 *				Added a new validator delegate ValidCardType() to validate the card type for the credit card number
 * 04/02/2007 STAR Retrofit III.Ch2: 
 *				Added a new validator delegate ValidExpDate() to validate the credit card expiration month and year
 * 04/02/2007 STAR Retrofit III.Ch3: 
 * *Added code in function InitializeLBs() to populate the expiration year dropdown dynamically
 * .Net Mig 3.5 -Ch1:Changed the methods ValidExpDate to validate the credit card field/
 *               Ch2:Added a new method ReqValueCheck() to check required values.
 *  Modified as a part of Line ID 160-Observation-CVV error message on 09/13/2010
 *  CVV_ERR.CH1: Commented CCV  field to hide CVV text box from the credit card payment screen.
 *  67811A0  - PCI Remediation for Payment systems START -Added to validate the Select year error in payment flow page
 *  MAIG - CH1 - Added the logic to check if it is an Amex card length is 15 digits
 *  MAIG - CH2 - Added the ErrorMessage Text
 *  MAIG - CH3 - Added the logic to validate for 15 digits if it is an Amex card
 *  MAIG - CH4 - Added the logic to validate for 15 digits if it is an Amex card
 *  MAIG - CH5 - Added the code to restrict the Card Digits to 15 for Amex card. 
 *  MAIG - CH6 - Modified the Credit Card validation method that works for all Credit Card types 11/17/2014
 *  MAIG - CH7 - Added logic to display an error message if the field is empty 11/19/2014
 *  MAIG - CH8 - Added logic to display an error message if the field is empty 11/19/2014
 *  MAIG - CH9 - Added logic to display an error message if the field is empty 11/19/2014
 * */
namespace MSC.Controls 
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using CSAAWeb;
	using CSAAWeb.WebControls;
	//STAR Retrofit III.Ch1: START - Added namespace for a new validator delegate ValidCardType() to validate the card type for the credit card number
	using System.Text.RegularExpressions;
	//STAR Retrofit III.Ch1: END

	/// <summary>
	///		Summary description for PaymentAccount.
	/// </summary>
	public partial  class PaymentAccount : ValidatingUserControl
	{
		//STAR Retrofit II.Ch1: START - Added code to validate the credit card for numeric characters. 
		protected void ValidNumeric(Object source, ValidatorEventArgs args) 
		{
			if(CCNumber != "")
			{
				args.IsValid = Validate.IsAllNumeric(CCNumber);
				if(!args.IsValid) 
					vldCardNumber.MarkInvalid(); 
			}
		}
		//STAR Retrofit II.Ch1: END

		/// <summary>
		/// ValidLength function added by Cognizant to validate the length
		/// of the CC and corresponding label validator is fired.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="args"></param>
		protected void ValidLength(Object source, ValidatorEventArgs args) 
		{
			if (CCNumber!="") 
			{
                //MAIG - CH1 - BEGIN - Added the logic to check if it is an Amex card length is 15 digits
                if (_CardType.SelectedItem.Text.ToUpper().Equals(Constants.PC_CRD_TYPE_AMEX_EXPANSION))
                {
                    args.IsValid = (CCNumber.Length == 15);
                    if (!args.IsValid)
                    {
                        vldCardNumber.MarkInvalid();
                        vldCardNumber.ErrorMessage = "Field Length must be exactly 15 characters.";
                    }
                }
                else
                {
                    args.IsValid = (CCNumber.Length == 16);
                    if (!args.IsValid)
                    {
                        vldCardNumber.MarkInvalid();
                        vldCardNumber.ErrorMessage = "Field Length must be exactly 16 characters.";
                    }
                }
                //MAIG - CH1 - END - Added the logic to check if it is an Amex card length is 15 digits
 
			}
		
		}
        //Added a new method as a part of .NetMig 3.5 on 02-10-2010.
        protected void ReqValCheck(Object source, ValidatorEventArgs args)
        {

            if (_CardType.SelectedItem.Text=="Select One")
            {
                args.IsValid = false;
                vldCardType.MarkInvalid();
				//MAIG - CH2 - BEGIN - Added the ErrorMessage Text
                vldCardType.ErrorMessage = Constants.PAYMENT_CARD_TYPE;
                //Validator1.ErrorMessage = "Card is not of the selected type.";
				//MAIG - CH2 - END - Added the ErrorMessage Text
            }
            if (_CardNumber.Text == "")
            {
                args.IsValid = false;
                vldCardNumber.MarkInvalid();
                //MAIG - CH9 - BEGIN - Added logic to display an error message if the field is empty 11/19/2014
                vldCardNumber.ErrorMessage = Constants.PAYMENT_CARD_NUMBER;
                //MAIG - CH9 - END - Added logic to display an error message if the field is empty 11/19/2014
                Validator1.ErrorMessage = "";
                
            }
        }
		//STAR Retrofit II.Ch2: START - Added code to invoke the check digit algorithm for validating the credit card number.
		/// <summary>
		/// ValidCCNumber function added by Cognizant to validate the credit card 
		/// number and corresponding label validator is fired.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="args"></param>
		protected void ValidCCNumber(Object source, ValidatorEventArgs args) 
		{
            //MAIG - CH3 - BEGIN - Added the logic to validate for 15 digits if it is an Amex card
            bool isValidCard = false;
            if (_CardType.SelectedItem.Text.ToUpper().Equals(Constants.PC_CRD_TYPE_AMEX_EXPANSION))
            {
                isValidCard = Validate.IsValidAmexCard(CCNumber);
            }
            else
            {
                isValidCard = Validate.IsValidCreditCard(CCNumber);
            }
			//if (Validate.IsValidCreditCard(CCNumber)) 
            if(isValidCard)
			{
                //MAIG - CH6 - BEGIN - Modified the Credit Card validation method that works for all Credit Card types 11/17/2014
                string ChkDigit = Cryptor.CreditCardCheckDigit(CCNumber);
                //MAIG - CH6 - END - Modified the Credit Card validation method that works for all Credit Card types 11/17/2014
                bool CardFormat = false;
                foreach (DataRow Row in CardTypes.Rows)
                    if (Row["Code"].ToString() == this.CCType)
                    {
                        string st = (string)Row["Regex"];
                        if (st != "")
                        {
                            Regex R = new Regex(st);
                            CardFormat = R.IsMatch(this.CCNumber);
                        }
                    }
				args.IsValid  = ((ChkDigit == "0" && CardFormat)?true:false);
	            //MAIG - CH3 - END - Added the logic to validate for 15 digits if it is an Amex card
                if (!args.IsValid)
                {
                    //Start-Added as a part of .Net Mig 3.5
                    Validator1.ErrorMessage = "";
                    vldCardNumber.ErrorMessage = "";
                    //End-Added as a part of .Net Mig 3.5
                    vldCardNumber.MarkInvalid();
                }
 			}
		}
		//STAR Retrofit II.Ch2: END

		//STAR Retrofit III.Ch1: START - Added a new validator delegate to validate the card type for the credit card number
		protected void ValidCardType(Object source, ValidatorEventArgs args) 
		{
            //MAIG - CH4 - BEGIN - Added the logic to validate for 15 digits if it is an Amex card
            bool isValidCard = false;
            if (_CardType.SelectedItem.Text.ToUpper().Equals(Constants.PC_CRD_TYPE_AMEX_EXPANSION))
            {
                isValidCard = Validate.IsValidAmexCard(CCNumber);
            }
            else
            {
                isValidCard = Validate.IsValidCreditCard(CCNumber);
            }
			//if (Validate.IsValidCreditCard(CCNumber))
            if (isValidCard)
            //MAIG - CH4 - END - Added the logic to validate for 15 digits if it is an Amex card
			{
				foreach (DataRow Row in CardTypes.Rows)
					if (Row["Code"].ToString()==this.CCType) 
					{
						string st = (string)Row["Regex"];
						if (st!="") 
						{
							Regex R = new Regex(st);
							args.IsValid = R.IsMatch(this.CCNumber);
                            if (!args.IsValid)
                                //vldCardType.MarkInvalid();
							vldCardNumber.MarkInvalid();
													
						}
					}
			}
		}
        //STAR Retrofit III.Ch1: END

		//STAR Retrofit III.Ch2: START - Added a new validator delegate to validate the credit card expiration month
		protected void ValidExpDate(Object source, ValidatorEventArgs args) 
		{
			if(CCExpYear != "" && CCExpMonth != "")
			{
				if ((System.Convert.ToInt16(CCExpYear) == System.DateTime.Now.Year))
				{
					if ((System.Convert.ToInt16(CCExpMonth) < System.DateTime.Now.Month)) 
					{
						DateTime dt = new DateTime(1990,Convert.ToInt16(CCExpMonth),01);
						args.IsValid = false;			     
						vldExp.MarkInvalid();
                        Validator1.MarkInvalid();
                        //.Net Mig-start-Added for displaying credit card invalid error message.
						vldExp.ErrorMessage = vldExp.ErrorMessage+"\n"+"Invalid Month: " + dt.ToString("MMMM");
                      //  vldExp.ErrorMessage = "Please complete or correct the requested information in the field(s) highlighted in red below.<br>" + "Invalid Month: " + dt.ToString("MMMM");
                       vldExpMonth.ErrorMessage = "Invalid Month: " + dt.ToString("MMMM");
                        //.NetMig-end
                       Validator2.MarkInvalid();
                       Validator2.ErrorMessage = "";
                       Validator1.ErrorMessage = "";
					}
				}
			}
            //Added as part of .Net migration 3.5  to display error message when a month is not selected.
            if (_ExpireMonth.SelectedItem.Text == "Select Month")
            {
                args.IsValid = false;
                vldExp.MarkInvalid();
                //MAIG - CH7 - BEGIN - Added logic to display an error message if the field is empty 11/19/2014
                vldExp.ErrorMessage = Constants.PAYMENT_CARD_EXPIRATION;
                //MAIG - CH7 - END - Added logic to display an error message if the field is empty 11/19/2014
                Validator1.ErrorMessage = "";

                Validator2.MarkInvalid();
                Validator2.ErrorMessage = "";
            }

            if (_ExpireYear.SelectedItem.Text == "Select Year")
            {
                args.IsValid = false;
                vldExp.MarkInvalid();
                //MAIG - CH8 - BEGIN - Added logic to display an error message if the field is empty 11/19/2014
                vldExp.ErrorMessage = Constants.PAYMENT_CARD_EXPIRATION;
                //MAIG - CH8 - END - Added logic to display an error message if the field is empty 11/19/2014
                //67811A0  - PCI Remediation for Payment systems START -Added to validate the Select year error in payment flow page
                Validator2.MarkInvalid();
                Validator2.ErrorMessage = "";
                //67811A0  - PCI Remediation for Payment systems END -Added to validate the Select year error in payment flow page


            }
            //.Net Mig 3.5-Changes End			
		}
		//STAR Retrofit III.Ch2: END

		///<summary/>
		public PaymentAccount() : base() 
		{
			this.Init += new System.EventHandler(this.InitializeLBs);
		}
		/// <summary>
		/// Retrieves the insurance lookup tables from the web service and caches them.
		/// </summary>
		private void Initialize() {
			CardTypes = ((SiteTemplate)Page).OrderService.LookupDataSet("Payment", "GetCreditCards", new object[] {true}).Tables["CREDITCARD_TABLE"];
		}

		//STAR Retrofit III.Ch1: START - Added csaa validator vldCardType to validate the card type for the credit card number
		//STAR Retrofit III.Ch1: END

		//STAR Retrofit III.Ch2: START - Added csaa validator vldExpMonth for validating the Credit Card expiration month
		//STAR Retrofit III.Ch2: END

		private static DataTable CardTypes = null;
		///<summary>Called from init event.</summary>
		private void InitializeLBs(object sender, System.EventArgs e) {
			if (CardTypes==null) Initialize();
			_CardType.DataSource=CardTypes;
			_CardType.DataBind();

			//STAR Retrofit III.Ch3: START - Added code to populate the expiration year dropdown dynamically
			int CurrentYear = DateTime.Now.Year;
			string[] arr_year = new string[11]; 
			arr_year[0] = "Select Year";
			for (int i=1; i<arr_year.Length; i++)
			{
				arr_year[i] = Convert.ToString(CurrentYear + (i-1));
			}
			_ExpireYear.DataSource = arr_year;
			_ExpireYear.DataBind();
			_ExpireYear.Items[0].Value = ""; 
			//STAR Retrofit III.Ch3: END
		}

		#region Web controls
		///<summary>Card type select box</summary>
		protected System.Web.UI.WebControls.DropDownList _CardType;
		///<summary>Expiration month select box</summary>
		protected System.Web.UI.WebControls.DropDownList _ExpireMonth;
		///<summary>Expiration year select box</summary>
		protected System.Web.UI.WebControls.DropDownList _ExpireYear;
        //CVV_ERR.CH1: START - Commented CCV  field to hide CVV text box from the credit card payment screen.
		///<summary>CCCV number text box</summary>        
     	//protected System.Web.UI.WebControls.TextBox _CVV;
        //CVV_ERR.CH1: END
		///<summary>Credit card number test box</summary>
		protected System.Web.UI.WebControls.TextBox _CardNumber;
		///<summary>Credit card number test box</summary>
		protected System.Web.UI.WebControls.TextBox _AuthCode;
		///<summary/>
		protected System.Web.UI.HtmlControls.HtmlTableCell AC1;
		///<summary/>
		protected CSAAWeb.WebControls.Validator vldCardNumber;
		///<summary/>
		protected CSAAWeb.WebControls.Validator vldCardType;
		///<summary/>
		protected CSAAWeb.WebControls.Validator vldExp;
		///<summary/>
		protected CSAAWeb.WebControls.Validator vldCVV;
		///<summary/>
		protected CSAAWeb.WebControls.Validator vldAuthCode;
		///<summary/>
		protected CSAAWeb.WebControls.Validator vldAuthCode1;
		///<summary/>
		//STAR Retrofit II.Ch3: START - Created validators to validate the card number for numeric characters and LUHN modulus 10 algorithm. 
		protected CSAAWeb.WebControls.Validator vldCardNum;
		//STAR Retrofit II.Ch3: END
		#endregion
		#region Public properties
		///<summary>Billing credit card number.</summary>
		public string CCNumber {get {return _CardNumber.Text;} set {_CardNumber.Text=value;}}
        //CVV_ERR.CH1: START - Commented CCV  field to hide CVV text box from the credit card payment screen.
		///<summary>Billing CCCV number</summary>
		//public string CCCVNumber {get {return _CVV.Text;} set {_CVV.Text=value;}}
        //CVV_ERR.CH1: END
		///<summary>Billing expiration month</summary>
		public string CCExpMonth {get {return ListC.GetListValue(_ExpireMonth);} set {ListC.SetListIndex(_ExpireMonth, value);}}
		///<summary>Billing expiration year</summary>
		public string CCExpYear {get {return ListC.GetListValue(_ExpireYear);} set {ListC.SetListIndex(_ExpireYear, value);}}
		///<summary>Billing card type.</summary>
		public string CCType {get {return ListC.GetListValue(_CardType);} set {ListC.SetListIndex(_CardType, value);}}
		///<summary>Verbal auth code.</summary>
		public string AuthCode {get {return _AuthCode.Text;} set {_AuthCode.Text=value;}}
		#endregion

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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
		///<summary/>
		protected override void OnPreRender(EventArgs e) {
			CSAAWeb.Serializers.ArrayOfErrorInfo E = ((SiteTemplate)Page).Order.Errors;
			if (E!=null && E.Count>0) 
				if (E[0].Code=="CYBER_DCALL") {
					_AuthCode.Visible=true;
					vldAuthCode.Hidden=false;
					vldAuthCode.DefaultAction=ValidatorDefaultAction.Required;
					vldAuthCode.MarkInvalid();
					AC1.Visible=true;
				} else switch(E[0].Target) {
					case "Card.CCNumber":
						this.vldCardNumber.MarkInvalid();
						break;
					case "Card.CCExpMonth":
						this.vldExp.MarkInvalid();
						break;
					case "Card.CCExpYear":
						this.vldExp.MarkInvalid();
						break;
					case "Card.CCType":
						this.vldCardType.MarkInvalid();
						break;
                    //CVV_ERR.CH1: START - Commented CCV  field to hide CVV text box from the credit card payment screen.
                    //case "Card.CCCVNumber":
                    //    this.vldCVV.MarkInvalid();
                    //    break;
				     //CVV_ERR.CH1: END
				}
		}

        //MAIG - CH5 - BEGIN - Added the code to restrict the Card Digits to 15 for Amex card. 
        protected void _CardType_SelectedIndexChanged(object sender, EventArgs e)
        {
            _ExpireMonth.SelectedValue = "";
            _ExpireYear.SelectedValue = "";
            _CardNumber.Text = "";
            if (_CardType.SelectedItem.Text.ToUpper().Equals(Constants.PC_CRD_TYPE_AMEX_EXPANSION))
            {
                _CardNumber.MaxLength = 15;
            }
            else
            {
                _CardNumber.MaxLength = 16;
            }
        }
        //MAIG - CH5 - END - Added the code to restrict the Card Digits to 15 for Amex card. 
	}
}
