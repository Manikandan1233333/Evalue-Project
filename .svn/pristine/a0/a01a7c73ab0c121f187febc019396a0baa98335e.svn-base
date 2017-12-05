//MAIG - CH1 - Added the code to hide the policy grid for invalid policy number
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
    using System.Net;
    using System.Net.Sockets;
    using CSAAWeb.AppLogger;
    using OrderClasses.Service;

    /// <summary>
    ///		Code for ECheck Controls.
    /// </summary>
    /// 
    public delegate void DropDownSelectionDelegate(object sender, EventArgs e);
    public partial class PaymentMethod : ValidatingUserControl
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
        protected CSAAWeb.WebControls.Validator vldPaymentType;
        public event DropDownSelectionDelegate dropDownChanged;
        private string Payflag;
        public PaymentMethod()
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

        #region Public properties
        #endregion

        #region LoadEvents

        private void Initialize()
        {
           
        }
               
        ///<summary>Called from init event.</summary>
        private void InitializeLBs(object sender, System.EventArgs e)
        {
            Initialize();
        }
        // Added new function to validate fields for required field payment type
        protected void ReqValPaymentTypeCheck(Object source, ValidatorEventArgs e)
        {
            e.IsValid = (_PaymentType.SelectedValue != "" || false);
            if (!e.IsValid) vldPaymentType.MarkInvalid();

        }
        ///<summary>Property to Get and Set the Value to the Payment Type</summary> />	
        public string PaymentType { get { return ListC.GetListValue(_PaymentType); } set { ListC.SetListIndex(_PaymentType, value); } }
        protected void _PaymentType_SelectedIndexChanged(object sender, System.EventArgs e)
        {
            if(this.dropDownChanged != null)
            this.dropDownChanged(sender, e);
		   //MAIG - CH1 - BEGIN - Added the code to hide the policy grid for invalid policy number
            if ((((MSC.Forms.ManageEnrollment)(((SiteTemplate)Page).OrderService.Page))._InvalidUnEnrollFlow) != null)
            {
                PlaceHolder PH_PolicyDetail = (PlaceHolder)this.NamingContainer.FindControl("policySearchDetails");
                if (PH_PolicyDetail != null)
                {
                    PH_PolicyDetail.Visible = false;
                }
            }
		   //MAIG - CH1 - END - Added the code to hide the policy grid for invalid policy number
            
        }
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
        public string isUpdated
        {
            get;
            set;
        }
       
        #endregion
    }
}
