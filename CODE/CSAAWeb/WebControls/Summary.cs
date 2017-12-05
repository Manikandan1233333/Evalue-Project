using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections;
using System.IO;
using System.Configuration;

namespace CSAAWeb.WebControls
{
	/// <summary>
	/// Validator that checks all the other validators on the page.  This only looks
	/// at Validators that have no ErrorMessage set.  If any exists that isn't valid,
	/// this validator will be Not Valid, allowing its error message to show if there
	/// is a summary control on the page.
	/// </summary>
	public class DefaultSummary : Validator
	{
		/// <summary>
		/// Set in config, if true this field appends the validator control id
		/// of the validator that caused this one to be invalid.  Useful for
		/// debugging.  Default is false.
		/// </summary>
		private static bool ShowFailingControl = false;

		/// <summary>
		/// Static constructor gets the ShowFailingControl value from the config.
		/// </summary>
		static DefaultSummary () 
		{
			ShowFailingControl = Config.bSetting("DefaultSummary_ShowFailingControl");
		}
		/// <summary>
		/// Sets specific properties of this control.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e) 
		{
			base.OnInit(e);
			this.Display=ValidatorDisplay.None;
			this.ServerValidate += new ValidatorEventHandler(EmptyValidatorValidate);
			ErrorMessage = ErrorMessage;
		}

		/// <summary>
		/// Performs another validation, to insure that any validators that changed state
		/// after page load have been checked.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnPreRender(EventArgs e) {
			Validated = false;
			Validate();
			base.OnPreRender(e);
		}

		/// <summary>
		/// Overrides base ErrorMessage property to allow ErrorMessage to be generated within
		/// begin and end tags if not set by ErrorMessage property.  
		/// </summary>
		public new string ErrorMessage 
		{ 
			get
			{
				if (base.ErrorMessage!="") return base.ErrorMessage;
				if (Text!="") return Text;
				HtmlTextWriter writer = new HtmlTextWriter(new StringWriter());
				RenderChildren(writer);
				RenderContents(writer);
				return writer.InnerWriter.ToString();
			}
			set {base.ErrorMessage = value;}
		}

		/// <summary>
		/// The validation function.
		/// </summary>
		/// <param name="source"></param>
		/// <param name="args"></param>
		private void EmptyValidatorValidate(Object source, ValidatorEventArgs args) 
		{
			foreach (Control C in Page.Validators)
				if (typeof(Validator).IsInstanceOfType(C) && ((Validator)C).ErrorMessage=="" && C!=args.Check && !((Validator)C).IsValid) 
				{
					if (ShowFailingControl) base.ErrorMessage += " (" + C.UniqueID + ")";
					args.IsValid = false;
				}
		}
	}
}
