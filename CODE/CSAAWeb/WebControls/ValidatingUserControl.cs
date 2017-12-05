using System;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace CSAAWeb.WebControls
{
	/// <summary>
	///	ValidatingUserControl is base class for combination component 
	///	that includes a validating label and perhaps some other validators.
	/// </summary>
	public class ValidatingUserControl : System.Web.UI.UserControl, IHideable
	{
		///<summary>The validating label for this control.</summary>
        ///Added as a part of .Net migration 3.5
		protected Validator LabelValidator = new Validator();
		private string _Label = "";
		private bool _Required = false;
		private bool _Hidden = false;
		/// <summary>
		/// This is the complete combined value of all the controls contained herein.
		/// </summary>
		public virtual string Text {get {return "";} set {}}
		/// <summary>
		/// Label is the text lable to appear over the combined control
		/// </summary>
		public string Label {get {return _Label;} set {_Label=value;}}
		/// <summary>
		/// IsValid will be true if all the validators in the control are valid
		/// </summary>
		public bool IsValid 
		{
			get {
					foreach (Validator V in Validators) if (!V.IsValid) return false;
					return true;
				}
			set {
					if (!value) foreach (Control C in Controls) 
						if (typeof(ValidatingUserControl).IsInstanceOfType(C))
						{
							((ValidatingUserControl)C).IsValid=false;
							return;
						} else if (typeof(TextBox).IsInstanceOfType(C) || typeof(RadioButtonList).IsInstanceOfType(C)
							|| typeof(CheckBoxList).IsInstanceOfType(C) || typeof(DropDownList).IsInstanceOfType(C)) {
							((WebControl)C).Attributes.Add("invalid","true");
							return;
						}
			}
		}
		/// <summary>
		/// Set this property to true if the control must contain a value.
		/// </summary>
		public bool Required {get {return _Required;} set {_Required = value; if (!value) DisableLabel();}}
		/// <summary>
		/// True if the control should render as hidden elements.  
		/// </summary>
		public bool Hidden {
			get {return _Hidden;} 
			set { _Hidden=value; 
				if (!ChildControlsCreated) CreateChildControls();
				HiddenControls.HideChildControls(this, value);
			}
		}
		/// <summary>
		/// Collection of all the validators associated with this control
		/// </summary>
		protected ValidatorCollection Validators = new ValidatorCollection();

		/// <summary>
		/// CheckValue returns true if the combined control value is a valid value.
		/// Should be overridden in derived classes.
		/// </summary>
		/// <returns></returns>
		protected virtual bool CheckValue() {return true;}

		/// <summary>
		/// IHideable.SaveContext.  Saves Hideable child controls to the context.
		/// </summary>
		public void SaveContext() 
		{
			HiddenControls.SaveChildContext(this);
		}
		/// <summary>
		/// CheckValid is a delegate for a validator control that will call CheckValue
		/// if all the other Validators are valid and the Text property isn't blank.
		/// </summary>
		protected void CheckValid(Object source, ValidatorEventArgs args) 
		{
			foreach (Validator V in Validators) 
				if (source!=V && !V.IsValid) {
					if (V!=LabelValidator && !Hidden) LabelValidator.MarkInvalid();
					return;
				}
			if (Text=="") return;
			args.IsValid = CheckValue();
			if (!args.IsValid && !Hidden) LabelValidator.MarkInvalid();
		}

		/// <summary>
		/// Call to set this control to invalid and highlight its labelvalidator.
		/// </summary>
		public void MarkInvalid() {
			this.IsValid=false;
			if (LabelValidator!=null) LabelValidator.MarkInvalid();
		}
		/// <summary>
		/// Enables or disables all the validators contained herein
		/// </summary>
		public void EnableValidators(bool Enable) 
		{
			if (!ChildControlsCreated) CreateChildControls();
			foreach (Control C in Controls) 
				if (typeof(Validator).IsInstanceOfType(C)) {
					((Validator)C).Enabled = Enable;
				} else if (typeof(ValidatingUserControl).IsInstanceOfType(C))
					((ValidatingUserControl)C).EnableValidators(Enable);
		}

		// Required by the designer
		/// <summary>
		/// Called on page load.  Base implementation does nothing, to be overridden
		/// in derived classes.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		protected virtual void Page_Load(object sender, System.EventArgs e) {}

		/// <summary>
		/// Does some parameter substitution in the ErrorMessage property of
		/// each of the validators, so that the message contains a valid reference 
		/// to which control it is applicable to.
		/// </summary>
		protected void FormatErrorMessage(Validator V) 
		{
			string container = Label;
			if (NamingContainer!=null && NamingContainer.ID!=null) container += " (" + NamingContainer.ID.Replace("_", " ") + ")";
			V.ErrorMessage = V.ErrorMessage.Replace("%Label%", container);
			Validators.Add(V);
			if (Hidden) V.Enabled=false;
		}

		/// <summary>
		/// Called whenever control isn't required.
		/// </summary>
		private void DisableLabel() 
		{
			if (LabelValidator!=null) {
				LabelValidator.Enabled=false;
				LabelValidator.DefaultAction=ValidatorDefaultAction.Succeed;
			}
		}
		/// <summary>
		/// Required by the Designer.
		/// </summary>
		override protected void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (!Required) DisableLabel();
			if (!ChildControlsCreated) CreateChildControls();
			foreach (Control C in Controls) {
				if (typeof(Validator).IsInstanceOfType(C)) FormatErrorMessage((Validator)C);
			}
		}

		/// <summary>
		/// Override of base Render, this method provides for automatic generation of
		/// hidden form elements for contained controls when the hidden property is true.
		/// </summary>
		protected override void Render(HtmlTextWriter output) 
		{
			if (!Hidden) {
				base.Render(output);
			} else foreach (Control C in Controls) 
					if (typeof(IHideable).IsInstanceOfType(C)) {
						C.RenderControl(output);
					} else HiddenControls.RenderControlAsHidden(output, C);
		}

	}
}
