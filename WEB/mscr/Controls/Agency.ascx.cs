/*MODIFIED BY COGNIZANT AS PART OF PT MAIG Changes
 * This page has been newly created for PT MAIG Changes.
 * Version 1.0
 * Created Date : 09/20/2014
 */
namespace MSCR.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using OrderClasses.Service;
    using System.Web.Security;
    using System.Security.Principal;
    using AuthenticationClasses;
    using CSAAWeb.AppLogger;  

	public partial  class Agency : System.Web.UI.UserControl
	{
        ///<summary>
        ///Property to Get and Set the Value to the Agency Type
        ///</summary>
        public string AgencyText
        {
            get
            {
                return _txtAgency.Text;
            }
            set
            {
               _txtAgency.Text = value;
            }
        }

        public string RoleName { get; set; }
        public string AgencyID { get; set; }
        /// <summary>
        /// Loads the Controls of the user control
        /// </summary>
        /// <param name="sender">Sender of the event</param>
        /// <param name="e">Event argument</param>
        protected void Page_Load(object sender, System.EventArgs e)
		{
            try
            {
                if (!Page.IsPostBack)
                {
                    //OrderClasses.Service.Order authUserObj = new OrderClasses.Service.Order();
                    //UserInfo userInfoObj = new UserInfo();

                    //userInfoObj = authUserObj.Authenticate(HttpContext.Current.User.Identity.Name, string.Empty, CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL);
//                    if ((userInfoObj.RoleNames.ToLower().Contains("pss")) && (userInfoObj.RoleNames.ToLower().Contains("finance"))) 
                    if ((RoleName.ToLower().Contains("pss")) && (RoleName.ToLower().Contains("finance"))) 
                    {
                        _txtAgency.Text = string.Empty;
                        _txtAgency.MaxLength = 500;
                        _txtAgency.Enabled = true;
                    }
                    else if ((RoleName.ToLower().Contains("pss")))
                    {
                        _txtAgency.Text = AgencyID.ToString();
                        _txtAgency.Enabled = false;
                    }
                    else if (!RoleName.ToLower().Contains("pss"))
                    {
                        _txtAgency.Text = string.Empty;
                        _txtAgency.MaxLength = 500;
                        _txtAgency.Enabled = true;
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.Log("Agency Report" + ex.Message);  
            }
		}

		#region Web Form Designer generated code
        /// <summary>
        /// Inilitializes the Controls and Properties
        /// </summary>
        /// <param name="e">Event argument</param>
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
		}
		#endregion

	}
}
