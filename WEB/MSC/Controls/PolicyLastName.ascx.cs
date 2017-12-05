/*
 * Modified as a part of MAIG Project on 10/20/2014:
 * MAIG - CH1 - Modified the Regex pattern to support full stop and comma special characters in the Last Name field
 * 
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
	using System.Text.RegularExpressions;

	/// <summary>
	///		Summary description for PolicyLastName.
	/// </summary>
	public partial  class PolicyLastName : ValidatingUserControl
	{
		///<summary></summary>
		protected TextBox _LastName;
		///<summary/>
		protected Validator vldLastNameAlpha;
		///<summary/>
		protected Validator Valid;
		

		///<summary>The amount of the payment as text.</summary>
		public override string Text {
			get{ return _LastName.Text;}
			set{ _LastName.Text=value;}
		}
		//START Changed by Cognizant - Added a new function LastNameInValid() which Highlights the Last Name Label Validator
		public void LastNameInValid()
		{
			Validator LastNameLabelValidator = (Validator)FindControl("LabelValidator"); 
			LastNameLabelValidator.MarkInvalid();  
		}
		//END

        //MAIG - CH1 - BEGIN - Modified the Regex pattern to support full stop and comma sepcial characters in the Last Name field
		private static Regex ExCheckName = new Regex("[^a-z '.,-]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //MAIG - CH1 - END - Modified the Regex pattern to support full stop and comma sepcial characters in the Last Name field
		///<summary>Name check validates that the name has only alpha, digits, space or '</summary>
		protected void CheckName(object source, ValidatorEventArgs e) {
			e.IsValid=!ExCheckName.IsMatch(Text);
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
