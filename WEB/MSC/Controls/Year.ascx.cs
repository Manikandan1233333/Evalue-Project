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
	///	Textbox for entering a year.
	/// </summary>
	public partial  class Year : ValidatingUserControl
	{

		///<summary></summary>
		protected TextBox _BaseYear;
		///<summary></summary>
		protected Validator vldBase1;
		///<summary></summary>
		protected Validator vldBase2;
		///<summary></summary>
		protected Validator Numeric;
		///<summary></summary>
		protected Validator Valid;
		///<summary></summary>
		public override string Text {
			get {return _BaseYear.Text;} set {_BaseYear.Text=value;}
		}

		///<summary>Checks to insure that the base year is a valid year.</summary>
		protected void CheckBaseYear(Object source, ValidatorEventArgs args) {
			try {
				int Y = Convert.ToInt32(Text);
				int Z = System.DateTime.Today.Year;
				if (Z-120>Y) args.IsValid=false;
			} catch {args.IsValid=false;}
		}

		#region Web Form Designer generated code
		///<summary></summary>
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
