/*
 * MAIG - CH1 - Added logic to display an error message if the field is empty 11/19/2014
 * MAIG - CH2 - Added logic to display an error message if the field is empty 11/19/2014
 * MAIG - CH3 - Added logic to display an error message if the field is empty 11/19/2014
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
    using CSAAWeb.WebControls;

    /// <summary>
    ///		Code for ECheck Controls.
    /// </summary>
    /// 

    public partial class echeck : ValidatingUserControl
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

        public string SysAccountType
        {
            get { return HiddenAccountType.Text; }
            set { HiddenAccountType.Text = value; }
        }

        #endregion

        public echeck()
            : base()
        {
            this.Init += new System.EventHandler(this.InitializeLBs);
        }

        private static DataTable AccountTypes = null;

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

        #region Public properties
        ///<summary>Billing Bank Account number.</summary>
        public string BankAcntNo { get { return _Accountno.Text.Trim(); } set { _Accountno.Text = value; } }
        ///<summary>Billing Bank Routine number</summary>
        public string BankId { get { return _Bankid.Text.Trim(); } set { _Bankid.Text = value; } }
        ///<summary>Billing Account type.</summary>
        public string BankAcntType { get { return ListC.GetListValue(_AccountType); } set { ListC.SetListIndex(_AccountType, value); } }

        public string CustName;


        #endregion

        #region LoadEvents

        private void Initialize()
        {

            AccountTypes = ((SiteTemplate)Page).OrderService.LookupDataSet("Payment", "GetBankAccount", new object[] { true }).Tables["BANKACCOUNT_TABLE"];
        }
               
        ///<summary>Called from init event.</summary>
        private void InitializeLBs(object sender, System.EventArgs e)
        {
            if (AccountTypes == null) Initialize();
            _AccountType.DataSource = AccountTypes;
            _AccountType.DataBind();
        }

        #endregion

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Added code to clear the ACH Routing number and Account number - VA Defect ID - 216 - Start 
		protected override void Page_Load(object sender, System.EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (ViewState[Constants.PMT_RECURRING_CCECTOKEN] == null)
                {
                    _Bankid.Text = string.Empty;
                    _Accountno.Text = string.Empty;
                    __AccountnoMasked.Text = string.Empty;
                }
                
            }
        }
        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Added code to clear the ACH Routing number and Account number - VA Defect ID - 216  - End

        #region StaticMethods

        public static string FormatField(string inStr, Int16 size, string fldName)
        {

            string outStr = inStr.Trim();
            //check the size first.
            if (outStr.Length > size)
                throw new ArgumentOutOfRangeException(string.Concat("Field ", fldName, " : ", outStr, " is > then ", size.ToString()));
            //CSAAWeb.Validate.IsAllNumeric(_Policy.Text)
            //if (type == NUMERIC)
            if (CSAAWeb.Validate.IsAllNumeric(inStr.ToString()))
            {
                //field is numeric, right justify and pad with 0
                outStr = outStr.PadLeft(size, '0');
            }
            else
            {
                //field is alphanumeric, left justify and pad with spaces
                outStr = outStr.PadRight(size).ToUpper();
            }
            return outStr;
        }

        private static string RoutingNumberCheckDigit(string transitNumber)
        {

            Int64 lRoutingNumSum = 0;
            Int16 iNextHighestMultiple = 0;
            if (transitNumber.Trim().Length != 8)
                transitNumber = FormatField(transitNumber, 8, "Routing Number Check Digit");
            lRoutingNumSum = ((Convert.ToInt16(transitNumber.Substring(0, 1)) * 3) +
                (Convert.ToInt16(transitNumber.Substring(1, 1)) * 7) +
                (Convert.ToInt16(transitNumber.Substring(2, 1)) * 1) +
                (Convert.ToInt16(transitNumber.Substring(3, 1)) * 3) +
                (Convert.ToInt16(transitNumber.Substring(4, 1)) * 7) +
                (Convert.ToInt16(transitNumber.Substring(5, 1)) * 1) +
                (Convert.ToInt16(transitNumber.Substring(6, 1)) * 3) +
                (Convert.ToInt16(transitNumber.Substring(7, 1)) * 7));
            iNextHighestMultiple = Convert.ToInt16(RoundToNextNumber(Convert.ToDecimal(lRoutingNumSum) / 10) * 10);
            return Convert.ToString(iNextHighestMultiple - lRoutingNumSum);

        }

        public static Int64 RoundToNextNumber(Decimal inNum)
        {
            Int64 outNum = 0;
            outNum = Convert.ToInt64(System.Math.Round(inNum));

            if (outNum < inNum)
                outNum++;

            return outNum;

        }

        public static bool IsAllZeros(string s)
        {
            char[] aryChars = s.ToCharArray();
            foreach (char c in aryChars)
            {
                if (c == '0')
                {
                }
                else
                    return false;
            }
            return true;
        }
        #endregion

        #region Events & Validators

        protected void ReqValCheck1(Object source, ValidatorEventArgs args)
        {
            if (_AccountType.SelectedItem.Text == "Select One")
            {
                args.IsValid = false;
                vldAccounttype.MarkInvalid();
                //MAIG - CH1 - BEGIN - Added logic to display an error message if the field is empty 11/19/2014
                vldAccounttype.ErrorMessage = Constants.PAYMENT_ACCOUNT_TYPE_REQ;
                //MAIG - CH1 - END - Added logic to display an error message if the field is empty 11/19/2014
                Validator1.ErrorMessage = "";


            }
            if (_Accountno.Text == "")
            {
                args.IsValid = false;
                vldAccNumber.MarkInvalid();
                //MAIG - CH2 - BEGIN - Added logic to display an error message if the field is empty 11/19/2014
                vldAccNumber.ErrorMessage = Constants.PAYMENT_ACCOUNT_NUMBER_REQ;
                //MAIG - CH2 - END - Added logic to display an error message if the field is empty 11/19/2014
                Validator1.ErrorMessage = "";

            }
            if (_Bankid.Text == "")
            {
                args.IsValid = false;
                vldBankidnumber.MarkInvalid();
                //MAIG - CH3 - BEGIN - Added logic to display an error message if the field is empty 11/19/2014
                vldBankidnumber.ErrorMessage = Constants.PAYMENT_ROUTING_NUMBER_REQ;
                //MAIG - CH3 - END - Added logic to display an error message if the field is empty 11/19/2014
                Validator1.ErrorMessage = "";
            }
        }

        protected void ValidAccountLength(Object source, ValidatorEventArgs args)
        {
            if (BankAcntNo != "")
            {
                if (BankAcntNo.Trim().Length < 4 || BankAcntNo.Trim().Length > 17)
                {
                    args.IsValid = false;
                }
                else
                {
                    args.IsValid = true;
                }
                if (!args.IsValid)
                {
                    vldAccNumber.MarkInvalid();

                    vldAccNumber.ErrorMessage = "";
                }

            }
        }

        protected void ValidateNumeric(Object source, ValidatorEventArgs args)
        {
            if (BankAcntNo != "")
            {
                args.IsValid = Validate.IsAllNumeric(BankAcntNo);
                if (!args.IsValid)
                    vldAccNumber.MarkInvalid();
                vldAccNumber.ErrorMessage = "";
            }
        }

        protected void ValidbankidLength(Object source, ValidatorEventArgs args)
        {
            if (BankId != "")
            {
                args.IsValid = (BankId.Length == 9);
                if (!args.IsValid)
                {
                    vldBankidnumber.MarkInvalid();
                    vldBankidnumber.ErrorMessage = "";
                }
            }

        }

        protected void Validbankidsequence(Object source, ValidatorEventArgs args)
        {
            if (BankId.Trim() != "")
            {
                if ((BankId.Length == 9) && (CSAAWeb.Validate.IsAllNumeric(BankId)))
                    args.IsValid = !(BankId.Substring(8, 1) != RoutingNumberCheckDigit(BankId.Substring(0, 8)));
                else args.IsValid = false;

                if (!args.IsValid)
                {
                    vldBankidnumber.MarkInvalid();
                    vldBankidnumber.ErrorMessage = "";
                }
            }
        }

        protected void Validbankidzero(Object source, ValidatorEventArgs args)
        {
            if (BankId != "")
            {
                args.IsValid = !IsAllZeros(BankId);
                if (!args.IsValid)
                {
                    Validator1.ErrorMessage = "";
                    vldBankidnumber.ErrorMessage = "";
                    vldBankidnumber.MarkInvalid();


                }
            }
        }

        #endregion

    }
}
