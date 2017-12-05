/* 
 * History:
 *	Modified By Cognizant

* 05/03/2011 RFC#130547 PT_echeck CH1: Added the echeck object to get the values from the Echeck control page and return the value by ccognizant.
 */
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;
using CSAAWeb;
using CSAAWeb.WebControls;
using CSAAWeb.Serializers;
using OrderClasses;
using OrderClasses.Service;



namespace MSC
{
    /// <summary>
    /// Encapsulates the functionality for a member transaction.  Encludes the persistence
    /// for the transaction information, and public methods that allow performing
    /// the operations necessary to process it.
    /// </summary>
    [ToolboxData("<{0}:OrderControl runat=server/>")]
    public class OrderControl : HiddenInput
    {
        private static Regex r = new Regex(" *\\<[^ ]* \\/\\n>", RegexOptions.Compiled);
        ///<summary/>
        protected Validator Valid;
        ///<summary/>
        private static bool EncryptHidden = true;
        ///<summary/>
        private bool MarkedInvalid = false;
        ///<summary/>
        private static string AppId = string.Empty;

        private OrderInfo O = new OrderClasses.OrderInfo();
        private Order OSvc { get { return ((SiteTemplate)Page).OrderService; } }

        #region Public properties
        ///<summary/>
        public ArrayOfLineItem Lines { get { return O.Lines; } set { O.Lines = value; } }
        ///<summary/>
        public ArrayOfAddressInfo Addresses { get { return O.Addresses; } set { O.Addresses = value; } }
        ///<summary/>
        public CardInfo Card { get { return O.Card; } set { O.Card = value; } }
        //RFC#130547 PT_echeck CH1: Added the echeck object to get the values from the Echeck control page and return the value by cognizant on 05/03/2011.
        ///<summary/>
        public eCheckInfo echeck { get { return O.echeck; } set { O.echeck = value; } }
        ///<summary/>
        //PTecheck
        ///<summary/>
        public OrderDetailInfo Detail { get { return O.Detail; } set { O.Detail = value; } }
        ///<summary/>
        public ArrayOfProductInfo Products { get { return O.Products; } set { O.Products = value; } }
        ///<summary/>
        public ArrayOfErrorInfo Errors { get { return O.Errors; } set { O.Errors = value; } }
        ///<summary/>
        private string UserId;
        ///<summary/>
        public string Token = string.Empty;
        ///<summary/>
        public bool IsValid { get { return Valid.IsValid; } set { Valid.IsValid = value; } }
        ///<summary/>
        public ProductInfo this[string ProductName]
        {
            get { return (ProductInfo)this.Products[ProductName]; }
        }


        ///<summary>Xml representation of all the data in this control</summary>
        public override string Text
        {
            get { return r.Replace(O.ToString(), ""); }
            set { O = new OrderInfo(value); }
        }

        ///<summary/>
        public bool ShowItemCount = false;

        ///<summary>True if the bill to address record hasn't been created.</summary>
        public bool NoBillTo { get { return (Addresses.BillTo == null); } }


        #endregion
        #region Initialization
        ///<summary/>
        static OrderControl()
        {
            AppId = Config.Setting("AppName");
            EncryptHidden = Config.bSetting("OrderControl_EncyrptHidden");
        }
        ///<summary>Creates and initializes the order validator.</summary>
        protected override void CreateChildControls()
        {
            Valid = new Validator();
            Valid.Display = ValidatorDisplay.None;
            Valid.ServerValidate += new ValidatorEventHandler(this.CheckValid);
            Valid.ID = "Valid";
            Controls.Add(Valid);
            ChildControlsCreated = true;
            base.CreateChildControls();
        }

        ///<summary/>
        protected override void OnInit(EventArgs e)
        {
            Encrypted = OrderControl.EncryptHidden;
            AutoRestore = true;
            base.OnInit(e);
            ((SiteTemplate)Page).Order = this;
            UserId = Page.User.Identity.Name;
            //START Changed by Cognizant 05/18/2004 for Commenting the MerchantRefNum assignment 
            //if (!Page.IsPostBack && Detail.MerchantRefNum=="") 
            //Detail.MerchantRefNum = (OSvc.GetReference());
            //END
        }
        #endregion
        #region Validate

        /// <summary>
        /// Sets the validator to invalid and its error message to ErrorMessage
        /// </summary>
        /// <param name="ErrorMessage">The error message</param>
        /// <param name="ValidatorID">ID for a validator to mark as invalid.</param>
        public void MarkInvalid(string ErrorMessage, string ValidatorID)
        {
            if (ValidatorID != "")
            {
                Validator V = ((Validator)Page.FindControl(ValidatorID));
                if (V != null) V.MarkInvalid();
            }
            MarkInvalid(ErrorMessage);
        }
        /// <summary>
        /// Sets the validator to invalid and its error message to ErrorMessage
        /// </summary>
        /// <param name="ErrorMessage">The error message</param>
        public void MarkInvalid(string ErrorMessage)
        {
            if (!ChildControlsCreated) CreateChildControls();
            MarkedInvalid = true;
            Valid.ErrorMessage = ErrorMessage;
            Valid.IsValid = false;
        }
        /// <summary>
        /// Onload event.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            if (!ChildControlsCreated) CreateChildControls();
            // This occurs only on the first load of primary.
            //if (Lines.Count == 0) UpdateRates();
        }

        /// <summary>
        /// This doesn't actually do anything, but is required by the control as a 
        /// placeholder.  The actual validation is performed during the various method calls.
        /// </summary>
        public void CheckValid(Object Source, ValidatorEventArgs args)
        {
            args.IsValid = !MarkedInvalid;
        }
        #endregion

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the UpdateRates - Code Clan Up II- March 2016

        #region UpdateLines
        /// <summary>
        /// Updates order lines for all the products
        /// </summary>
        public void UpdateLines()
        {
            O = OSvc.CalculateOrderLines(O);
            if (O.Errors != null && O.Errors.Count > 0)
                MarkInvalid(O.Errors[0].Message, O.Errors[0].Target);
        }
        /// <summary>
        /// Updates order lines for the product type specified.
        /// </summary>
        /// <param name="ProductName">Product type to update lines for.</param>
        public void UpdateLines(string ProductName)
        {
            O = OSvc.CalculateOrderLines(O, ProductName);
            if (O.Errors != null && O.Errors.Count > 0)
                MarkInvalid(O.Errors[0].Message, O.Errors[0].Target);
        }
        #endregion
        #region Process Order
        /// <summary>
        /// This method actually does the work of entering an O.
        /// </summary>
        /// <returns></returns>
        public bool Process()
        {
            O = OSvc.Process(O);
            if (O.Errors != null)
                if (O.Errors.Count == 1)
                    MarkInvalid(O.Errors[0].Message, O.Errors[0].Target);
                else if (O.Errors.Count > 1)
                    Valid.IsValid = false;
            if (Valid.IsValid && Lines.Total > 0 && Addresses.Household.Email != "" && Addresses.Household.Email != null)
                ((IConfirmingEmail)Page.LoadControl("../Controls/ConfirmingEmail.ascx")).Send(this);
            return Valid.IsValid;
        }

        #endregion
    }

    /// <summary>
    /// Interface to allow confirming email to be manipulated without having the
    /// entire control definition.
    /// </summary>
    public interface IConfirmingEmail
    {
        /// <summary>
        /// The order to confirm.
        /// </summary>
        OrderControl Order { get; set; }
        /// <summary>
        /// Render the control to a string.
        /// </summary>
        /// <returns></returns>
        string Render();
        ///<summary>Sends the email</summary>
        void Send(OrderControl Order);
    }
}
