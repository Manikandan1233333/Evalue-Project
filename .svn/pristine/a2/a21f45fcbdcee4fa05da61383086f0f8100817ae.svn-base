
/*
 * MODIFIED BY COGNIZANT AS A PART OF SR#8434145 ON June 20 2009
 * SR#8434145.Ch1 : Added a new condition to ContinueButton_Click for checking the duplicate payment.
 * 67811A0 - PCI Remediation for Payment systems CH1: Check if the duplicate alert is checked on postback and then enable the continue button.
 */
namespace MSC.Controls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;

    /// <summary>
    ///	Encapsulates the display of the common navigation buttons.
    /// </summary>
    public partial class Buttons : System.Web.UI.UserControl
    {
        ///<summary/>
        protected System.Web.UI.WebControls.ImageButton CancelButton;
        ///<summary/>
        protected System.Web.UI.WebControls.ImageButton BackButton;
        ///<summary/>
        protected System.Web.UI.WebControls.ImageButton ContinueButton;
        ///<summary/>
        protected SiteTemplate ThisPage { get { return (SiteTemplate)Page; } }
        ///<summary/>
        protected HtmlInputHidden CancelUrl;
        ///<summary>Set this to true to hide the back button.</summary>
        public bool HideBackButton = false;
        ///<summary>Set this to true to hide the continue button.</summary>
        public bool HideContinueButton = false;

        ///<summary/>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            if (ThisPage.OnBackUrl == "") BackButton.Visible = false;
            if (ThisPage.OnContinueUrl == "") ContinueButton.Visible = false;
            if (ThisPage.OnCancelUrl == "") CancelButton.Visible = false;
            if (!IsPostBack) CancelUrl.Value = ThisPage.OnCancelUrl;
            else ThisPage.OnCancelUrl = CancelUrl.Value;
        }

        ///<summary/>
        protected override void OnPreRender(EventArgs e)
        {
            if (HideBackButton) BackButton.Visible = false;
            if (HideContinueButton) ContinueButton.Visible = false;
        }

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

        ///<summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        ///<summary/>
        protected void BackButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (ThisPage.OnBackUrl != "") ThisPage.GotoPage(ThisPage.OnBackUrl, false);
        }

        ///<summary/>
        protected void CancelButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (ThisPage.OnCancelUrl != "") Response.Redirect(ThisPage.OnCancelUrl);
        }

        ///<summary/>
        protected void ContinueButton_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            //SR#8434145.Ch1 - START: Added by COGNIZANT for duplicate payment alert - July 15 2009.
            Label lbl1 = (Label)Page.FindControl("lbldupMessage");
            //67811A0 - PCI Remediation for Payment systems CH1:START- Check if the duplicate alert is checked on postback and then enable the continue button.                                  
            CheckBox chkDuplicate = (CheckBox)Page.FindControl("chkDuplicate");
            if ((lbl1 != null) && (lbl1.Visible == true) && (chkDuplicate != null) && (chkDuplicate.Checked == false))
            {
                ContinueButton.Enabled = false;
            }
            else
            {
                ContinueButton.Enabled = true;
            }           
            //67811A0 - PCI Remediation for Payment systems CH1:END- Check if the duplicate alert is checked on postback and then enable the continue button.
            //SR#8434145.Ch1 - END
            if (ThisPage.OnContinueUrl != "" && Page.IsValid && ThisPage.ProcessPage())
                ThisPage.GotoPage(ThisPage.OnContinueUrl);
        }

    }
}
