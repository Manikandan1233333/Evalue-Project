	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for InsRptHeader.
	/// </summary>
	/// 
namespace MSCR.Reports
{
	public partial  class InsRptHeader : System.Web.UI.UserControl
	{

		protected void Page_Load(object sender, System.EventArgs e)
		{
			lblTitle.Text = (string)Session["RptTitle"];
			lblRunDate.Text = DateTime.Now.ToString();
			lblDateRange.Text = (string)Session["DateRange"];

			string csrs = (string) Session["CSRs"];
			if (csrs != String.Empty)
			{
				lblCsrTxt.Text = "Users:";
				lblCsrVal.Text = csrs;
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
