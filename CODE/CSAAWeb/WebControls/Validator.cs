using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Configuration;
using System.Collections;

namespace CSAAWeb.WebControls
{
	/// <summary>
	/// Validator is a custom control validator control.  It has similarities
	/// to the System.Web.UI.WebControls.CustomValidator, but extends the
	/// capability to check multiple controls, and renders differently, in
	/// that if the Display property is "Static" (default) it will always show
	/// on the page, just using different classes (or colors by default),
	/// to display error or OK.  There is also a default evaluator, which
	/// will work the same as the "RequiredValidator" if no OnServerValidate
	/// delegate is supplied.   The OnServerValidate event receives the control to validate
	/// in its args instead of the text value supplied to the event by CustomValidator.
	/// Note: The public field Validated is initially set to TRUE!  The page's Validate method
	/// should be overriden, and all the validators of this type should have this field set to
	/// false prior to calling base.validate().  This allows any recursive validators to function
	/// properly, while still preventing validation if Page.Validate isn't called, such as from
	/// a button whose CausesValidation property is false.
	/// </summary>
	[ValidationPropertyAttribute("IsValid")]
	public class Validator : System.Web.UI.WebControls.BaseValidator, IHideable
	{
	
       
		private void Initialize() 
		{
			Type T = this.GetType();
			foreach (string s in ConfigurationSettings.AppSettings.AllKeys) 
				if (s.IndexOf("Validator_")==0) {
					System.Reflection.PropertyInfo p = T.GetProperty(s.Replace("Validator_",""));
					if (p==null) throw new Exception("Validator configuration property not found: " + s.Replace("Validator_",""));
					p.SetValue(this,ConfigurationSettings.AppSettings[s],null);
				}
		}

		/// <summary>
		/// Default constructor.
		/// </summary>
		public Validator() : base()
		{
			if (!Initialized) Initialize();
			Initialized = true;
		}
		
		///<summary>Backer for ErrorClass property</summary>
		private string _ErrorClass = string.Empty;
		///<summary>Backer for Tag property</summary>
		private string _Tag = string.Empty;
		///<summary>True if the control should fail for on any invalid control.</summary>
		private bool Any = false;
		/// <summary>
		/// True if the Validate method has been called.
		/// </summary>
		public bool Validated = true;
		///<summary>True if validation is in process (prevents infinite loop in recursion.).</summary>
		private bool Validating = false;
		///<summary>Backer for Hidden</summary>
		private bool _Hidden = false;
		///<summary>True if base class is initialized.</summary>
		private static bool Initialized = false;
		///<summary>Backer for DefaultAction property.</summary>
		private ValidatorDefaultAction _DefaultAction = ValidatorDefaultAction.Required;
		///<summary>Backer for RequiredIndicator property.</summary>
		private static string _RequiredIndicator = " *";
		///<summary>Backer for DefaultClass properyt.</summary>
		private static string _DefaultClass = string.Empty;
		///<summary>Backer for DefaultErrorClass property.</summary>
		private static string _DefaultErrorClass = string.Empty;
		///<summary>Backer for DefaultTag property</summary>
		private static string _DefaultTag = "span";
		/// <summary>
		/// A string appended to all labels that have DefaultAction of Required.
		/// </summary>
		public string RequiredIndicator {get {return _RequiredIndicator;} set {_RequiredIndicator=value;}}
		/// <summary>
		/// The CssClass to use if none is specified.
		/// </summary>
		public string DefaultClass {get {return _DefaultClass;} set {_DefaultClass=value;}}
		/// <summary>
		/// The ErrorClass string to use if none is specified.
		/// </summary>
		public string DefaultErrorClass {get {return _DefaultErrorClass;} set {_DefaultErrorClass=value;}}
		/// <summary>
		/// The tag to use if none is specified.
		/// </summary>
		public string DefaultTag {get {return _DefaultTag;} set {_DefaultTag=value;}}
		/// <summary>
		/// The CSS Class to use when displaying valid control.
		/// </summary>
		public string Class {get {return (CssClass!="")?CssClass:DefaultClass;} set {CssClass=value;}}
		/// <summary>
		/// The CSS Class to use when displaying invalid control.
		/// </summary>
		public string ErrorClass {get {return (_ErrorClass!="")?_ErrorClass:DefaultErrorClass;} set {_ErrorClass=value;}}
		/// <summary>
		/// The HTML Tag that this control will render.
		/// </summary>
		public string Tag {get {return (_Tag!="")?_Tag:DefaultTag;} set {_Tag=value;}}
		/// <summary>
		/// IHideable.Hidden.
		/// </summary>
		public bool Hidden {
			get {return _Hidden;} 
			set { _Hidden=value; 
				if (!ChildControlsCreated) CreateChildControls();
				HiddenControls.HideChildControls(this, value);
			}
		}
			/// <summary>
			/// Determines how validator will occur on multiple controls.  If Any, any
			/// valid control will validate.  If All, all controls must be valid.  Default
			/// is All.
			/// </summary>
			public ValidatorValidateOn ValidateOn 
		{
			get {return (Any)?ValidatorValidateOn.Any:ValidatorValidateOn.All;} 
			set {Any=(value==ValidatorValidateOn.Any);}
		}
		/// <summary>
		/// Determines the type of check to perform by the default evaluator.
		/// </summary>
		public ValidatorDefaultAction DefaultAction {get {return _DefaultAction;} set {_DefaultAction=value;}}
		/// <summary>
		/// Override of base IsValid.  If called prior to Validate(), will call
		/// Validate() first.
		/// </summary>
		public new bool IsValid 
		{
			get {
				if (Validated) return base.IsValid;
				if (!Page.IsPostBack || !Enabled || (Hidden && DefaultAction==ValidatorDefaultAction.Required)) {
					Validated = true;
					base.IsValid = true;
					return true;
				}
				Validate();
				return base.IsValid;
			}
			set {base.IsValid = value;}
		}

		/// <summary>
		/// Sets specific properties of this control.
		/// </summary>
		protected override void OnLoad(EventArgs e) 
		{
			base.OnLoad(e);
			EnableViewState=false;
			EnableClientScript=false;
			if (ServerValidate!=null) DefaultAction=ValidatorDefaultAction.Succeed;
		}

		/// <summary>
		/// IHideable.SaveContext.  Does nothing.
		/// </summary>
		public void SaveContext() {}
		/// <summary>
		/// Always returns true.  Allows for custom OnServerValidate with controls
		/// that may not have validation attribute, as well as with no control
		/// specified.
		/// </summary>
		protected override bool ControlPropertiesValid() {return true;}

		/// <summary>
		/// Override of default Render insures that control is rendered unless
		/// Display=None or Hidden=true.  Default won't if control is valid.  Also 
		/// allows control to have child controls within its boundaries, including 
		/// server ASP tags, which the default doesn't.
		/// </summary>
		protected override void Render(HtmlTextWriter output) 
		{
			if (Display!=ValidatorDisplay.None && !Hidden) 
			{
				RenderBeginTag(output);
				if (Display==ValidatorDisplay.Static || !IsValid) {
					RenderChildren(output);
					RenderContents(output);
					if (DefaultAction==ValidatorDefaultAction.Required) 
						output.Write(RequiredIndicator);
				}
				RenderEndTag(output);
			}
		}
		
		/// <summary>
		/// Override of vase RenderBeginTag.  Creates the tag just the way we want it
		/// for this type of validator.
		/// </summary>
		public override void RenderBeginTag(HtmlTextWriter output) 
		{
			if (Display==ValidatorDisplay.Dynamic && IsValid)output.AddStyleAttribute("visibility","hidden");
			if (EnableClientScript) output.AddAttribute("id", ID);
			CssClass = (!IsValid && ErrorClass!="")?ErrorClass:Class;
			if (CssClass!="") output.AddAttribute("class", CssClass);
			if (!IsValid && ErrorClass=="") output.AddStyleAttribute("color", ForeColor.ToString());
			output.RenderBeginTag(Tag);
		}

		// --------------------- Validating --------------------------------------
		
		/// <summary>
		/// Called for each control to be validated.
		/// </summary>
		public event ValidatorEventHandler ServerValidate;

		/// <summary>
		/// Fires the ServerValidate Event(s).  Deligates can be added by using
		/// this method name as an HTML attribute in the server control definition tag
		/// </summary>
		protected virtual void OnServerValidate(ValidatorEventArgs args) 
		{
			ServerValidate(this, args);
		}

		
		/// <summary>
		/// Returns true if the validation event is true for all the controls listed
		/// in ControlToValidate.  Sets Validated to true when finished.
		/// </summary>
		/// <exception cref="ValidatorCircularException">
		/// Thrown if method is called recursively on the same object.  Means that
		/// one of the controls being validated is dependant on itself.  Could be
		/// through multiple levels.
		/// </exception>
		/// <exception cref="ValidatorNotDefinedException">
		/// Thrown if neither OnServerValidate property or ControlToValidate property 
		/// is set.  At least one of the two must be set.  Default value will be used
		/// for OnServerValidate if only ControlToValidate is set.  This control will
		/// be supplied to OnServerValidate delegate if on OnServerValidate is set.
		/// </exception>
		protected override bool EvaluateIsValid() 
		{
			if (Validated) return base.IsValid;
			if (Validating) throw new ValidatorCircularException(ID);
			Validating = true;
			if (ControlToValidate.Length==0) {
				if (ServerValidate==null) throw new ValidatorNotDefinedException(ID);
				return CompleteValidation(EvaluateIsValid(ID));
			} else if (ServerValidate==null) ServerValidate = new ValidatorEventHandler(DefaultOnServerValidate);
			ArrayList ControlsToValidate = new ArrayList();
			ControlsToValidate.AddRange(ControlToValidate.Split(','));
			foreach(string st in ControlsToValidate) 
			{
				if (Any==EvaluateIsValid(st)) return CompleteValidation(Any);
			}
			return CompleteValidation(!Any);
		}
		
		/// <summary>
		/// Builds ValidatorEventArgs calls OnServerValidate with a ValidateEventArgs object
		/// for ControlID.
		/// </summary>
		/// <param name="ControlID">The ID of the control to validate.</param>
		/// <returns>The returned IsValid property from the ValidateEventArgs</returns>
		private bool EvaluateIsValid(string ControlID) 
		{
			ValidatorEventArgs args = new ValidatorEventArgs(FindControl(ControlID));
			if (args.Check==null) throw new ValidatorNotFoundException(ID, ControlID);
			OnServerValidate(args);
			return args.IsValid;
		}

		/// <summary>
		/// Helper function for completing EvaluateIsValid.  Sets the Validating
		/// and Validated properties, then returns result.
		/// </summary>
		private bool CompleteValidation(bool Result)
        {

            Validating = false;
            Validated = true;
                
                return Result;
            
		}

		/// <summary>
		/// Default delegate used for validating if none is supplied by the server
		/// control tag.  Performs the action specified by
		/// the DefaultAction property to evalute C.
		/// </summary>
		private void DefaultOnServerValidate(Object source, ValidatorEventArgs args) 
		{
			switch (DefaultAction) 
			{
				case ValidatorDefaultAction.Required:
					if (!RequiredControlValid(args.Check)) args.IsValid=false; break;
				case ValidatorDefaultAction.Numeric:
					CheckControlValidationProperty(args.Check.ID, "ControlToValidate");
					args.IsValid = CSAAWeb.Validate.IsAllNumeric(GetControlValidationValue(args.Check.ID),true);
					break;
				case ValidatorDefaultAction.MaxLength:
					CheckControlValidationProperty(args.Check.ID, "ControlToValidate");
					if (typeof(TextBox).IsInstanceOfType(args.Check)) {
						int len = GetControlValidationValue(args.Check.ID).Length;
						args.IsValid = (((TextBox)args.Check).MaxLength>=len);
					}
					break;
				case ValidatorDefaultAction.ExactLength:
					CheckControlValidationProperty(args.Check.ID, "ControlToValidate");
					if (typeof(TextBox).IsInstanceOfType(args.Check)) {
						int len = GetControlValidationValue(args.Check.ID).Length;
						args.IsValid = (len==0 || ((TextBox)args.Check).MaxLength==len);
					}
					break;
				case ValidatorDefaultAction.AlphaNumeric:
					CheckControlValidationProperty(args.Check.ID, "ControlToValidate");
					args.IsValid = CSAAWeb.Validate.IsAlphaNumeric(GetControlValidationValue(args.Check.ID));
					break;
				case ValidatorDefaultAction.Decimal:
					CheckControlValidationProperty(args.Check.ID, "ControlToValidate");
					args.IsValid = CSAAWeb.Validate.IsDecimal(GetControlValidationValue(args.Check.ID));
					break;
				default: break;
			}
			if (!args.IsValid) MarkInvalid(args.Check);
			
		}

		/// <summary>
		/// If C is a WebControl, will add the invalid=true attribute to it.
		/// </summary>
		private bool MarkInvalid(Control C) 
		{
			if (typeof(WebControl).IsInstanceOfType(C)) {
				((WebControl)C).Attributes.Add("invalid", "true");
				return true;
			}
			return false;
		}

		/// <summary>
		/// Returns true if the control already has an attribute "invalid"
		/// </summary>
		private bool IsMarkedInvalid(Control C) {
			if (typeof(WebControl).IsInstanceOfType(C))
				foreach (string key in ((WebControl)C).Attributes.Keys)
					if (key=="invalid") return true;
			return false;
		}

		/// <summary>
		/// Sets the IsValid property to false, and marks the first control to validate
		/// as invalid.
		/// </summary>
		public void MarkInvalid() 
		{
			IsValid = false;
			ArrayList ControlsToValidate = new ArrayList();
			ControlsToValidate.Reverse();
			ControlsToValidate.AddRange(ControlToValidate.Split(','));
			Control C=null;
			foreach (string st in ControlsToValidate) 
			{
				C = FindControl(st);
				if (IsMarkedInvalid(C)) return;
			}
			ControlsToValidate.Reverse();
			foreach (string st in ControlsToValidate) {
				C = FindControl(st);
				if (MarkInvalid(C)) return;
			}
		}




		/// <summary>
		/// Helper function for validating.  Returns true if the control
		/// contains a value.
		/// </summary>
		private bool RequiredControlValid(Control C) 
		{
			switch (C.GetType().Name) 
			{
				case "RadioButton": return ((RadioButton)C).Checked;
				case "CheckBox": return ((CheckBox)C).Checked;
				case "Validator": return ((Validator)C).IsValid;
				default: 
					if (typeof(ListControl).IsInstanceOfType(C)) {
						try {
							CheckControlValidationProperty(C.ID, "ControlToValidate");
							return GetControlValidationValue(C.ID)!="";
						}
						catch {return ((ListControl)C).SelectedIndex!=-1;}
					} else {
						CheckControlValidationProperty(C.ID, "ControlToValidate");
						return GetControlValidationValue(C.ID)!="";
					}
			}
		}

		/// <summary>
		/// Returns the control referenced by id.  ID may be dot notated to return
		/// a control within a control, but the dot notation may only be one-level deep.
		/// </summary>
		public override Control FindControl(string id) 
		{
			if (id.IndexOf(".")>0) {
				Control C = base.FindControl(id.Substring(0,id.IndexOf(".")));
				if (C==null) throw new ValidatorNotFoundException(ID, id.Substring(0,id.IndexOf("."))+ "(" + id + ")");
				return C.FindControl(id.Substring(id.IndexOf(".")+1));
			} else return base.FindControl(id);
		}
	}


	/// <summary>
	/// Delegate definition for Validator.OnServerValidate Event
	/// </summary>
	public delegate void ValidatorEventHandler(Object source, ValidatorEventArgs args);

	/// <summary>
	/// EventArgs for Validator.OnServerValidate Event
	/// </summary>
	public class ValidatorEventArgs : EventArgs
	{
		///<summary>True if the case is valid.</summary>
		public bool IsValid=true;
		///<summary>The control to check</summary>
		public Control Check = null;

		///<summary>Constructor accepting the control to check</summary>
		public ValidatorEventArgs(Control C) 
		{
			Check = C;
		}
	}

	/// <summary>
	/// Enum for Validator.ValidateOn property.
	/// </summary>
	public enum ValidatorValidateOn {
		/// <summary>
		/// Validates if any of the ControlToCheck controls are valid.
		/// </summary>
		Any, 
		/// <summary>
		/// All ControlToCheck must be valid to validate.
		/// </summary>
		All
	}

	/// <summary>
	/// Enum for Validator.DefaultAction property.
	/// </summary>
	public enum ValidatorDefaultAction {
		/// <summary>
		/// Validator will insure that a value has been entered.
		/// </summary>
		Required, 
		/// <summary>
		/// Validator will insure that the value is numeric.
		/// </summary>
		Numeric, 
		/// <summary>
		/// Validator will always succeed.
		/// </summary>
		Succeed,
		///<summary>
		///Validator will insure that the length isn't greater than 
		///the max length property
		///</summary>
		MaxLength,
		///<summary>
		///Validator will insure that the length is exactly
		///the max length property
		///</summary>
		ExactLength,
		/// <summary>
		/// Validator will insure that the value contains only numbers and letters.
		/// </summary>
		AlphaNumeric,
		/// <summary>
		/// Validator will insure that the value is a valid number with a decimal.  May
		/// be negative.
		/// </summary>
		Decimal
	}

	/// <summary>
	/// Thrown if method is called recursively on the same object.  Means that
	/// one of the controls being validated is dependant on itself.  Could be
	/// through multiple levels.
	/// </summary>
	public class ValidatorCircularException : System.Web.HttpException 
	{
		///<summary>The id of the offending control.</summary>
		private string ID = string.Empty;
		///<summary>The error message</summary>
		public override string Message {get {return "Circular reference validating " + ID;} }
		///<summary>Constructor accepting offending control's ID.</summary>
		public ValidatorCircularException(string ID) : base() {this.ID = ID;}
	}

	/// <summary>
	/// Thrown if neither OnServerValidate property or ControlToValidate property 
	/// is set.  At least one of the two must be set.  Default value will be used
	/// for OnServerValidate if only ControlToValidate is set.  This control will
	/// be supplied to OnServerValidate delegate if on OnServerValidate is set.
	/// </summary>
	public class ValidatorNotDefinedException : System.Web.HttpException
	{
		///<summary>The offending control's ID></summary>
		private string ID = string.Empty;
		///<summary>The error message</summary>
		public override string Message {get {return "Validator " + ID + " has neither OnServerValidate nor ControlToValidate defined.";} }
		///<summary>Constructor accepting the offending control's ID.</summary>
		public ValidatorNotDefinedException(string ID) : base() {this.ID = ID;}
	}

	/// <summary>
	/// Thrown while looping through the items in ControlToValidate if an item can't
	/// be found.
	/// </summary>
	public class ValidatorNotFoundException : System.Web.HttpException
	{
		///<summary>The offending control's ID.</summary>
		private string ID = string.Empty;
		///<summary>The ID that was being looked for.</summary>
		private string ControlID = string.Empty;
		///<summary>The error message</summary>
		public override string Message {get {return 
			"Unable to find control id '"+ControlID+"' referenced by the 'ControlToValidate' property of '"+ID+"'.";} }
		///<summary>Constructor accepting the offending control's ID and the ID being sought</summary>
		public ValidatorNotFoundException(string ID, string ControlID) : base() {this.ID = ID; this.ControlID=ControlID;}
	}
}
