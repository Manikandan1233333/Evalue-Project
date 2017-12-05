namespace MSCR.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for PageValidator.
	/// </summary>
	public partial  class PageValidator : System.Web.UI.UserControl
	{

		///<summary/>
		public int Colspan = 1;
		///<summary/>
		private bool Enabled = true;

		///<summary/>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		///<summary/>
		protected override void OnPreRender(EventArgs e) 
		{
			try {if (!IsPostBack || Page.IsValid) Enabled=false;;}
			catch {Enabled=false;}
			base.OnPreRender(e);
		}

		///<summary/>
		protected override void Render(System.Web.UI.HtmlTextWriter output) 
		{
			if (Enabled) base.Render(output);
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
	}
}
