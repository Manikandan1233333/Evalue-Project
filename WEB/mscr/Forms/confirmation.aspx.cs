/*
CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removed the button click events as part of code Clean Up - March 2016
 *
 */
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CSAAWeb.AppLogger;


namespace MSCR.Forms
{
	/// <summary>
	/// Summary description for confirmation.
	/// </summary>
	public partial class confirmation : CSAAWeb.WebControls.PageTemplate
	{
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			string sFormattedAmt = string.Empty;
            //67811A0 - PCI Remediation for Payment systems START: Added to clear the cache of the page to prevent the page loading on browser back button hit 
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            //67811A0 - PCI Remediation for Payment systems END: Added to clear the cache of the page to prevent the page loading on browser back button hit 
			if (!IsPostBack) {
				System.Diagnostics.Debug.Assert(Context.Items["amount"] != null);

				try {
					double dAmt = Convert.ToDouble(Context.Items["amount"].ToString());
					sFormattedAmt = string.Format(" {0:c} ", dAmt);
				}
				catch (Exception ex) {
					Logger.Log(ex);
					// ignore and display whatever it is
					sFormattedAmt = Context.Items["amount"].ToString();
				}
				
				this.lblAmount.Text = sFormattedAmt;
			}
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
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion

        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removed the button click events as part of code Clean Up - March 2016

		private void btnLogout_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
			Server.Transfer("logout.aspx");
		}
	}
}
