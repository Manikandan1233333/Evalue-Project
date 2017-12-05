/*
 * Security Defect - Ch1 - Modified the messages in the below valdiator messages
* Security Defect -CH2-  Added the below Validator which will insure that the input string doesn't contain Junk characters
* Security Defect -CH3- Modified the code and Message such that the target field is added in the message and error code of the  corresponding target field is read from he config entry.
 
*/
using System;
using System.Collections;
using System.Reflection;
using System.Xml.Serialization;
using System.ComponentModel;
using CSAAWeb.AppLogger;

namespace CSAAWeb.Serializers {
	#region IValidatingSerializer
	/// <summary>
	/// IValidatingSerializer is an interface that ValidatingSerializer and
	/// ArrayOfValidatingSerializer both inherit allowing them to validate properties
	/// of either type.
	/// </summary>

	public interface IValidatingSerializer {
		/// <summary>
		/// Call this method to validate the object, which will populate its Errors
		/// property with any errors.
		/// </summary>
		void Validate();
		/// <summary>
		/// Calls basic validation and bubbles errors up to V.
		/// </summary>
		void Validate(IValidatingSerializer V, string PropertyName);
		/// <summary>
		/// If E is not null, first checks to see if Errors is null and initializes it,
		/// then adds e to the collection.
		/// </summary>
		void AddError(ErrorInfo E);
		/// <summary>
		/// This property will contain any errors after validation.  It will be null if
		/// there are no errors.  However, errors are always bubbled-up, that is they are
		/// removed from lower level properties, and placed in the highest level 
		/// IValidatingSerializer object that calls validate.
		/// </summary>
		ArrayOfErrorInfo Errors {get; set;}
		/// <summary>Set this property to true if validation is not required.</summary>
		bool SkipValidation {get; set;}
	}
	/// <summary>
	/// Contains static methods used by the IValidatingSerializer interface classes.
	/// </summary>
	internal class IVS {
		/// <summary>
		/// If E is not null, first checks to see if Errors is null and initializes it,
		/// then adds e to the collection.
		/// </summary>
		public static void AddError (IValidatingSerializer I, ErrorInfo E) {
			if (E==null) return;
			if (I.Errors==null) I.Errors = new ArrayOfErrorInfo();
			I.Errors.Add(E);
		}
		/// <summary>Calls basic Validator and bubbles errors up to V.</summary>
		public static void Validate(IValidatingSerializer I, IValidatingSerializer V, string PropertyName) {
			I.Validate();
			if (I.Errors!=null) V.Errors=I.Errors.Bubble(V.Errors, PropertyName);
			I.Errors=null;
		}
		/// <summary>Loops through the Validator attributes provided.</summary>
		public static void Validate(IValidatingSerializer I, MemberInfo M, ValidatingSerializerEventArgs e) {
			ArrayList V = new ArrayList();
			V.AddRange(M.GetCustomAttributes(typeof(ValidatorAttribute), true));
			V.Sort();
			foreach (ValidatorAttribute A in V) {
				A.OnValidate(e);
				I.AddError(e.Error);
				if (I.Errors!=null && I.Errors.Count>0) break;
			}
		}
	}
	#endregion
	#region ValidatingSerializer
	/// <summary>
	/// Derived from SimpleSerializer, ValidatingSerializer contains an additional property,
	/// Errors, and method, Validate, that together allow for easy Validator by looping through
	/// its properties and calling Validate on any validateable properties.
	/// </summary>
	public class ValidatingSerializer : SimpleSerializer, IValidatingSerializer {
		/// <summary>Default constructor</summary>
		public ValidatingSerializer() : base() {}
		/// <summary>Xml constructor</summary>
		public ValidatingSerializer(string Xml) : base(Xml) {}
		/// <summary>Constructor from another Object</summary>
		public ValidatingSerializer(Object O) : base(O) {}

		private bool _Skip = false;
		/// <summary>Set this property to true if validation is not required.</summary>
		[XmlAttribute]
		[DefaultValue(false)]
		public virtual bool SkipValidation {get {return _Skip;} set {_Skip=value;}}
		/// <summary>
		/// Recursively calls Validate on any IValidatingSerializer properties,
		/// and appends their Errors to this.  Should be overriden in derived classes
		/// to perform specific Validator on any other properties.
		/// </summary>
		public virtual void Validate() {
			if (SkipValidation) return;
			Type T = this.GetType();
			IVS.Validate(this, T, new ValidatingSerializerEventArgs(this));
			foreach (PropertyInfo P in T.GetProperties()) 
				if (P.Name!="Item") Validate(P.GetValue(this, new object[] {}), P);
			foreach (FieldInfo F in T.GetFields()) 
				Validate(F.GetValue(this), F);
		}

		/// <summary>
		/// If O is a validatingSerializer, calls its validate method with the property name,
		/// otherwise, checks for any ValidatorAttribute attributes and processes them.
		/// </summary>
		private void Validate(object O, MemberInfo M) {
			if (typeof(IValidatingSerializer).IsInstanceOfType(O))
				((IValidatingSerializer)O).Validate(this, M.Name);
			else IVS.Validate(this, M, new ValidatingSerializerEventArgs(this, O, M.Name));
		}

		/// <summary>Adds E to errors collection if is not null, initializing Errors first.</summary>
		public void AddError(ErrorInfo E) {IVS.AddError(this, E);}
		/// <summary>Calls basic Validator and bubbles errors up to V.</summary>
		public void Validate(IValidatingSerializer V, string PropertyName) {
			IVS.Validate(this, V, PropertyName);
		}
		private ArrayOfErrorInfo _Errors = null;
		/// <summary>
		/// After a call to Validate, contains any errors in this object that
		/// haven't been bubbled.
		/// </summary>
		[DefaultValue(null)]
		public ArrayOfErrorInfo Errors {get {return _Errors;} set {_Errors = value;}}
	}

	/// <summary>
	/// Derived from ArrayOfSimpleSerializer, this class implements the IValidatingSerializer interface.
	/// </summary>
	public class ArrayOfValidatingSerializer : ArrayOfSimpleSerializer, IValidatingSerializer {
		///<summary>Default constructor</summary>
		public ArrayOfValidatingSerializer() : base() {}
		///<summary>Xml constructor</summary>
		public ArrayOfValidatingSerializer(string Xml) : base(Xml) {}
		///<summary>Constructor from another collection</summary>
		public ArrayOfValidatingSerializer(IList source) : base(source) {}

		private bool _Skip = false;
		/// <summary>Set this property to true if validation is not required.</summary>
		[DefaultValue(false)]
		public virtual bool SkipValidation {get {return _Skip;} set {_Skip=value;}}

		/// <summary>
		/// Gets or sets the item at index.
		/// </summary>
		private new ValidatingSerializer this[int index] {
			get {return (ValidatingSerializer) InnerList[index];}
			set {InnerList[index] = value;}
		}

		/// <summary>Adds item to the collection.</summary>
		public void Add(ValidatingSerializer item) {InnerList.Add(item);}

		/// <summary>
		/// Recursively calls Validate on all the items in the list 
		/// and appends their Errors to this.  Should be overriden in derived classes
		/// to perform any specific Validator required for the list.
		/// </summary>
		public virtual void Validate() {
			if (SkipValidation) return;
			IVS.Validate(this, this.GetType(), new ValidatingSerializerEventArgs(this));
			int n = 0;
			foreach (ValidatingSerializer S in this) {
				S.Validate(this, "["+n+"]");
				n++;
			}
		}

		/// <summary>Adds E to errors collection if is not null, initializing Errors first.</summary>
		public void AddError(ErrorInfo E) {IVS.AddError(this, E);}
		/// <summary>Calls basic Validator and bubbles errors up to V.</summary>
		public void Validate(IValidatingSerializer V, string PropertyName) {
			IVS.Validate(this, V, PropertyName);
		}
		private ArrayOfErrorInfo _Errors = null;
		/// <summary>
		/// After a call to Validate, contains any errors in this object that
		/// haven't been bubbled.
		/// </summary>
		[DefaultValue(null)]
		public ArrayOfErrorInfo Errors {get {return _Errors;} set {_Errors = value;}}
	}
	#endregion
	#region ValidatorAttribute
	/// <summary>
	/// This class of attributes can be applied to members of ValidatingSerializer to
	/// cause certain types of Validator to occur.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property | AttributeTargets.Class)]
	public class ValidatorAttribute : Attribute, IComparable {
		private string _Message = "";
		///<summary>The error message to return.</summary>
		public virtual string Message {get {return _Message;} set {_Message=value;}}
		///<summary>The error code to return</summary>
		private string _Code = "INVALID VALUE";
		///<summary>The error code to return</summary>
		public virtual string Code {get {return _Code;} set {_Code=value;}}
		private int _Priority = 1;
		///<summary>The rank order of this validator.</summary>
		public virtual int Priority {get {return _Priority;} set {_Priority=value;}}
		///<summary>Compares priority order.</summary>
		public int CompareTo (object O) {
			if (O==null) return 1;
			ValidatorAttribute V = O as ValidatorAttribute;
			if (V==null) throw new ArgumentException("Argument is not ValidatorAttribute.");
			if (V.Priority==Priority) return 0;
			if (V.Priority>Priority) return -1;
			return 1;
		}
		///<summary>Validate event function.</summary>
		public virtual void OnValidate(ValidatingSerializerEventArgs e) {
			if (Validation!="") {                
					e.Checking
						.GetType()
						.GetMethod(Validation, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
						.Invoke(e.Checking, new object[] {this, e});
			}
			else e.Error = (Check(e.Value))?null:new ErrorInfo(this, e.Field);
		}
		///<summary>Returns true if value is valid.  Overridden in derived classes.</summary>
		protected virtual bool Check(string Value) {
			return true;
		}
		///<summary>The name of the custom validation function.</summary>
		public string Validation = "";
	}
	/// <summary>Validator will insure that a value has been entered.</summary>
    /// //Security Defect - Ch1 -START - Modified the messages in the below valdiator messages
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class ValidatorRequiredAttribute : ValidatorAttribute {
		///<summary></summary>
		//public override string Message { get{ return "A required value is missing.";}}
        public override string Message { get { return "Validation failed in "; } }
		///<summary></summary>
		public override string Code { get {return "REQUIRED";}}
		///<summary></summary>
		public override int Priority { get {return 0;}}
		///<summary></summary>
		protected override bool Check(string Value) {return (Value.Trim()!="");}
	}
	/// <summary>Validator will insure that the value is numeric.</summary>
	public class ValidatorNumericAttribute : ValidatorAttribute {
		///<summary></summary>
		//public override string Message {get {return "Field must be numeric.";}}
        public override string Message { get { return "Validation failed in "; } }
		///<summary></summary>
		public override string Code { get {return "INVALID_NUMBER";}}
		///<summary></summary>
		protected override bool Check(string Value) {return CSAAWeb.Validate.IsAllNumeric(Value.Trim());}
	}
	///<summary>
	///Validator will insure that the length isn't greater than the length property
	///</summary>
	public class ValidatorMaxLengthAttribute : ValidatorAttribute {
		///<summary>The length to match against for ExactLength or MaxLength</summary>
		public int Length = 0;
		///<summary></summary>
		//public override string Message {get {return "Field must be no longer than "+Length.ToString()+" characters.";}}
        public override string Message { get { return "Validation failed in "; } }
		///<summary></summary>
		public override string Code { get {return "INVALID_LENGTH";}}
		///<summary></summary>
		protected override bool Check(string Value) {return (Value.Trim().Length<=Length);}
	}
    ///<summary>
    ///Security Defect -CH2- Start - Added the below Validator which will insure that the input string doesn't contain Junk characters
    ///</summary>
    public class ValidatorJunk : ValidatorAttribute
    {
        ///<summary></summary>
        public override string Message { get { return "Validation failed in "; } }
        ///<summary></summary>
        public override string Code { get { return "INVALID CHAR"; } }
        ///<summary></summary>
        public static char[] junkchars = "|&;$%@'<>()+\n\r\'\"".ToCharArray();
        ///<summary>Method to check for the junk character in the input string</summary>
        protected override bool Check(string Value)
        {
            Value = Value.Trim();
            return (Value.IndexOfAny(junkchars) <= -1);
        }
    }
    //Security Defect - CH2-End - Added the below Validator which will insure that the input string doesn't contain Junk characters
	///<summary>Validator will insure that the length is exactly the length property</summary>
	public class ValidatorExactLengthAttribute : ValidatorAttribute {
		///<summary>The length to match against for ExactLength or MaxLength</summary>
		public int Length = 0;
		///<summary></summary>
		//public override string Message {get {return "Field must be exactly "+Length.ToString()+" characters.";}}
        public override string Message { get { return "Validation failed in "; } }
		///<summary></summary>
		public override string Code { get {return "INVALID_LENGTH";}}
		///<summary></summary>
		protected override bool Check(string Value) {return (Value.Trim().Length==Length);}
	}
	/// <summary>Validator will insure that the value contains only numbers and letters.</summary>
	public class ValidatorAlphaNumericAttribute : ValidatorAttribute {
		///<summary></summary>
		//public override string Message {get {return "Field must be AlphaNumeric.";}}
        public override string Message { get { return "Validation failed in "; } }
		///<summary></summary>
		public override string Code { get {return "INVALID_CHARACTERS";}}
		///<summary></summary>
		protected override bool Check(string Value) {return CSAAWeb.Validate.IsAlphaNumeric(Value.Trim()); }
	}
	/// <summary>
	/// Validator will insure that the value is a valid number with a decimal.  May
	/// be negative.
	/// </summary>
	public class ValidatorDecimalAttribute : ValidatorAttribute {
		///<summary></summary>
		//public override string Message {get {return "Field must be a valid decimal number.";}}
        public override string Message { get { return "Validation failed in "; } }
		///<summary></summary>
		public override string Code { get {return "INVALID_DECIMAL";}}
		///<summary></summary>
		protected override bool Check(string Value) {return CSAAWeb.Validate.IsDecimal(Value.Trim());}
	}
	/// <summary>
	/// Validator will insure that the value is a valid date.
	/// </summary>
	public class ValidatorDateAttribute : ValidatorAttribute 
	{
		///<summary></summary>
		//public override string Message {get {return "Field must be a valid date.";}}
        public override string Message { get { return "Validation failed in "; } }
		///<summary></summary>
		public override string Code { get {return "INVALID_DATE";}}
		///<summary></summary>
		protected override bool Check(string Value) {return CSAAWeb.Validate.IsValidDate(Value.Trim());}
	}
	/// <summary>
	/// Validator will insure that the value is a valid phone.
	/// </summary>
	public class ValidatorPhoneAttribute : ValidatorAttribute 
	{
		///<summary></summary>
		//public override string Message {get {return "Field must be a valid phone number.";}}
        public override string Message { get { return "Validation failed in "; } }
		///<summary></summary>
		public override string Code { get {return "INVALID_PHONE";}}
		///<summary></summary>
		protected override bool Check(string Value) {return CSAAWeb.Validate.IsValidPhone(Value.Trim());}
	}
	/// <summary>
	/// Validator will insure that the value is a valid email.
	/// </summary>
	public class ValidatorEmailAttribute : ValidatorAttribute 
	{
		///<summary></summary>
		//public override string Message {get {return "Field must be a valid email address.";}}
        public override string Message { get { return "Validation failed in "; } }
		///<summary></summary>
		public override string Code { get {return "INVALID_EMAIL";}}
		///<summary></summary>
		protected override bool Check(string Value) {return CSAAWeb.Validate.IsValidEmailAddress(Value.Trim());}
	}
    //Security Defect - Ch1 -END - Modified the messages in the below valdiator messages
	/// <summary>
	/// Delegate definition for Validator.OnServerValidate Event
	/// </summary>
	public delegate void ValidatingSerializerEventHandler(ValidatorAttribute source, ValidatingSerializerEventArgs args);

	/// <summary>
	/// EventArgs for Validator.OnServerValidate Event
	/// </summary>
	public class ValidatingSerializerEventArgs : EventArgs {
		///<summary>Set to an error if the case is invalid.</summary>
		public ErrorInfo Error=null;
		///<summary>The object to check</summary>
		public Object Check = null;
		///<summary>A string representing the value of the object (useful if a simple type).</summary>
		public string Value = "";
		///<summary>The field or property name.</summary>
		public string Field = "";
		///<summary>The validator being checked.</summary>
		public IValidatingSerializer Checking = null;
		///<summary>Constructor accepting the Validator to check</summary>
		public ValidatingSerializerEventArgs(IValidatingSerializer Checking) : this(Checking, Checking, "") {}
		///<summary>Constructor accepting the property to check</summary>
		public ValidatingSerializerEventArgs(IValidatingSerializer Checking, object O, string Name) {
			Check = O;
			Value = (O==null)?"":O.ToString();
			Field = Name;
			this.Checking = Checking;
		}
	}

	#endregion
	#region ErrorInfo
	/// <summary>
	/// Error object.
	/// </summary>
	public class ErrorInfo : SimpleSerializer {
		/// <summary>Default constructor</summary>
		public ErrorInfo() : base() {}
		/// <summary>Xml constructor</summary>
		public ErrorInfo(string Xml) : base(Xml) {}
		/// <summary>Constructor from another Object</summary>
		public ErrorInfo(Object O) : base(O) {}
		/// <summary>
		/// Constructor from another Object, with the property Name of that object to be prepended to
		/// Target.
		/// </summary>
		public ErrorInfo(Object O, string Name) : base(O) {
			if (Target.Substring(0,1)!="[") Name+=".";
			Target = Name + Target;
		}
		/// <summary>
		/// Summary to construct with all the parameters.
		/// </summary>
		/// <param name="Code"></param>
		/// <param name="Message"></param>
		/// <param name="Target"></param>
		public ErrorInfo(String Code, string Message, string Target) : base() {
			this.Code=Code;
			this.Message=Message;
			this.Target=Target;
		}

		/// <summary>
		/// Summary to construct from validaing attribute  the parameters.
		/// </summary>
		/// <param name="A"></param>
		/// <param name="Target"></param>
		public ErrorInfo(ValidatorAttribute A, string Target) : base() {
			//Security Defect -CH3- Start - Modified the code and Message such that the target field is added in the message and error code of the  corresponding target field is read from he config entry.
            //this.Code=A.Code;
            //this.Message = A.Message;
            string errCode = "ERRCDE_" + Target.ToUpper();
            this.Code = Config.Setting(errCode).ToString();
            this.Message = A.Message + Target +".";
            //Security Defect -CH3- End - Modified the code and Message such that the target field is added in the message and error code of the  corresponding target field is read from he config entry.
			this.Target=Target;
            Logger.Log("Validation failed in " + Target + this.Code);
		}
		/// <summary>Error code</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string Code="";
		/// <summary>The error message</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string Message="";
		/// <summary>The target of the error</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string Target="";

	}

	/// <summary>
	/// Collection of errors.
	/// </summary>
	public class ArrayOfErrorInfo : ArrayOfSimpleSerializer {
		///<summary>Default constructor</summary>
		public ArrayOfErrorInfo() : base() {}
		///<summary>Xml constructor</summary>
		public ArrayOfErrorInfo(string Xml) : base(Xml) {}
		///<summary>Constructor from another collection</summary>
		public ArrayOfErrorInfo(IList source) : base(source) {}

		/// <summary>
		/// Gets or sets the item at index.
		/// </summary>
		public new ErrorInfo this[int index] {
			get {return (ErrorInfo) InnerList[index];}
			set {InnerList[index] = value;}
		}

		/// <summary>
		/// Adds item to the collection.
		/// </summary>
		public void Add(ErrorInfo item) {
			InnerList.Add(item);
		}

		/// <summary>
		/// Appends the lines from source to this.
		/// </summary>
		public void Append(ArrayOfErrorInfo source, string Name) {
			foreach (ErrorInfo i in source) Add(new ErrorInfo(i, Name));
		}

		/// <summary>
		/// Bubbles this into E and returns E.  If E is null, will create a new
		/// array first.
		/// </summary>
		/// <param name="E">The error array to bubble into.</param>
		/// <param name="PropertyName">The name of the property that this belongs to.</param>
		public ArrayOfErrorInfo Bubble(ArrayOfErrorInfo E, string PropertyName) {
			if (this.Count>0) {
				if (E==null) E=new ArrayOfErrorInfo();
				E.Append(this, PropertyName);
				this.Clear();
			}
			return E;
		}
	}
	#endregion
}
