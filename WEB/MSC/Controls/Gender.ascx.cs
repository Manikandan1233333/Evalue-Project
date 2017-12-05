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
	///	Control encapsulates the entry for gender.
	/// </summary>
	public partial  class Gender : ValidatingUserControl
	{
		///<summary/>
		protected System.Web.UI.WebControls.RadioButtonList _Gender;
		
		// Added by Cognizant on 07/13/2004 for Adding a validator for Gender
		// Removed by J McEwen on 10/25 because this property already exists in the base class.
		//<summary>Gender Validator</summary>
		//protected CSAAWeb.WebControls.Validator LabelValidator;

		///<summary>Male or Female</summary>
		public override string Text {get {return ListC.GetListValue(_Gender);} set {ListC.SetListIndex(_Gender, value);}}

		#region Web Form Designer generated code
		/// <summary/>
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
