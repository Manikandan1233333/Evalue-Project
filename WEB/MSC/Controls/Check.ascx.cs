/*History: 
 * 
 * CHG0055954-AZ PAS Conversion and PC integration - Added this page to control validations for the check number field in UI
 * MAIG - CH1 - Added logic to check if the given Check Number is Numeric
 * MAIG - CH2 - Added logic to display an error message if the field is empty 11/19/2014
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

    public partial class Check : ValidatingUserControl
    {

        #region WebControls
        protected CSAAWeb.WebControls.Validator vldchknumber;
        protected System.Web.UI.WebControls.TextBox _ChkNumber;

        #endregion

        #region Properties



        #endregion

        public Check()
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
        ///<summary>Check number.</summary>
        public string CheckNumber { get { return ChkNumber.Text.Trim(); } set { ChkNumber.Text = value; } }



        #endregion

        #region LoadEvents

        private void Initialize()
        {
        }
        ///<summary>Called from init event.</summary>
        private void InitializeLBs(object sender, System.EventArgs e)
        {
            if (AccountTypes == null) Initialize();
           
        }


        #endregion

        #region StaticMethods

        
        #endregion

        #region Events & Validators

        //CHG0055954-AZ PAS Conversion and PC integration - Added the below validator to check if the ChkNumber Field is empty
        
        protected void ReqValCheck1(Object source, ValidatorEventArgs args)
        {
            if (ChkNumber.Text == "")
            {
                args.IsValid = (ChkNumber.Text != "");

                if (!args.IsValid)
                {
                    vldchknumber.MarkInvalid();
                    //MAIG - CH2 - BEGIN - Added logic to display an error message if the field is empty 11/19/2014
                    vldchknumber.ErrorMessage = Constants.CHECK_NUMBER_REQ;
                    //MAIG - CH2 - END - Added logic to display an error message if the field is empty 11/19/2014
                }
            }
            //MAIG - CH1 - BEGIN - Added logic to check if the given Check Number is Numeric
            else if(!CSAAWeb.Validate.IsAllNumeric(ChkNumber.Text))
            {
                args.IsValid = false;
                vldchknumber.MarkInvalid();
                vldchknumber.ErrorMessage = "Check Number should be Numeric.";
            }
            //MAIG - CH1 - END - Added logic to check if the given Check Number is Numeric

        }



        //CHG0055954-AZ PAS Conversion and PC integration - Added the below Method to validate the length of the Check Number in UI

        protected void ValidchkLength(Object source, ValidatorEventArgs args)
        {
            if (ChkNumber.Text != "")
            {
                args.IsValid = (ChkNumber.Text.Length <= 6 & ChkNumber.Text.Length >= 1);
                if (!args.IsValid)
                {

                    vldchknumber.MarkInvalid();
                    vldchknumber.ErrorMessage = "";
                   
                }
            }
            
        }




        #endregion

    }

}
