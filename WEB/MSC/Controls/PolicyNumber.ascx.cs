/* 
 * MODIFIED BY COGNIZANT AS PART OF .NET MIGRATION CHANGES
 * CHANGES DONE
 * 02/20/2010 .NetMig.Ch1: Added new function for validating for require field
 * 02/20/2010 .NetMig.Ch2: Added new condition to mark policy as invalid
 *02/20/2010 .NetMig.Ch3: Added new condition to assign IsValid Property
 * 01/07/2011 PUP Ch1-Validation change  for PUP policy'&& Text.Trim().Length != 10 -by cognizant.
 * 67811A0  - PCI Remediation for Payment systems : Code was cleaned up and unwanted code was removed.
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
    //Added by Cognizant on 10/29/2004 for allowing the Policy Number with dashes
    using System.Text.RegularExpressions;

    /// <summary>
    ///	User control for entering policy number.
    /// </summary>
    public partial class PolicyNumber : ValidatingUserControl
    {

        ///<summary></summary>
        protected TextBox _Policy;
        ///<summary/>  
        protected Validator Valid;

        ///<summary/>
        public override string Text
        {
            get { return _Policy.Text.ToUpper(); }
            set { _Policy.Text = value; }
        }

        //START Added by Cognizant on 11/3/2004 - Added a new function PolicyInValid() which Highlights the Policy Validator
        public void PolicyInValid()
        {
            Validator PolicyLabelValidator = (Validator)FindControl("LabelValidator");
            PolicyLabelValidator.MarkInvalid();

        }
        //END

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
    }
}
