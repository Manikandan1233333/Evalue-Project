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
	///	A component that encapsulates the entry and validation of a date as 3 text boxes.
	/// </summary>
	public partial  class Three_Box_Date : ValidatingUserControl
	{
		///<summary/>
		protected System.Web.UI.WebControls.TextBox MM;
		///<summary/>
		protected System.Web.UI.WebControls.TextBox DD;
		///<summary/>
		protected System.Web.UI.WebControls.TextBox YYYY;
		///<summary>Set to true to allow entry of future dates.</summary>
		public bool AllowFuture = false;
		///<summary>Set to false to disallow entry of past dates.</summary>
		public bool AllowPast = true;
		///<summary>The complete date</summary>
		public override string Text {
			get { 
				if (MM.Text=="" && DD.Text=="" && YYYY.Text=="") return "";
				return MM.Text + "/" + DD.Text + "/" + YYYY.Text;
			}
			set {
				string [] s = value.Split('/');
				MM.Text = s[0].Replace(" ","");
				DD.Text = (s.Length<=1)?"":s[1].Replace(" ","");
				YYYY.Text = (s.Length<=2)?"":s[2].Replace(" ","");
			}
		}

		///<summary/>
		protected override bool CheckValue() 
		{
			if (!CSAAWeb.Validate.IsValidDate(Text)) return false;
			DateTime Today = DateTime.Today;
			DateTime Date = DateTime.Parse(Text);
			bool IsPast = Date.CompareTo(Today)<0 && Date.CompareTo(Today.AddYears(-120))>0;
			bool IsFuture = Date.CompareTo(Today)>0 && Date.CompareTo(Today.AddYears(+1))<0;
			return ((AllowFuture && IsFuture) || (AllowPast && IsPast) || (Date.CompareTo(Today)==0));
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
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}
}
