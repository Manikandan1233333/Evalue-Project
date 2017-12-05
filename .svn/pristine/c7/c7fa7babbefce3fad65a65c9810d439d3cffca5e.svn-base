using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections.Specialized;

[assembly:TagPrefix("CSAAWeb,WebControls", "CSAA") ]
namespace CSAAWeb.WebControls
{
	/// <summary>
	/// A Custom control for a hidden element.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:HiddenInput runat=server/>"),
	Designer("CSAAWeb.WebControls.Design.HiddenDesigner")]
	public class HiddenInput : System.Web.UI.WebControls.WebControl, IHideable, IPostBackDataHandler
	{
		/// <summary>Default constructor</summary>
		public HiddenInput() : base () {}
		/// <summary>Constructor accepting id and text properties.</summary>
		/// <param name="id">The ID of the control.</param>
		/// <param name="Value">The value for Text property</param>
		public HiddenInput(string id, string Value) : base() 
		{
			ID = id;
			Text = Value;
		}

		/// <summary>Backer for Text property</summary>
		private string text=string.Empty;
		/// <summary>Backer for AutoRestore property</summary>
		private bool _AutoRestore = false;
		/// <summary>Backer for Encrypted property</summary>
		private bool _Encrypted = false;

		/// <summary>IHideable.Hidden.  Always true for this type.</summary>
		public bool Hidden {get {return true;} set {}}
		/// <summary>True if the value should be encrypted when rendered.</summary>
		public bool Encrypted {get {return _Encrypted;} set {_Encrypted=value;}}
		/// <summary>Returns true if the page is in design mode.</summary>
		private bool DesignMode {
			get {return (Page.Site!=null && Page.Site.DesignMode);}
		}
	
		/// <summary>The value of the hidden element.</summary>
		[Bindable(true), Category("Data"), DefaultValue("")] 
		public virtual string Text {get {return text;} set {text = value;}}

		/// <summary>True if the element should try to find a value in the context after Server.Transfer</summary>
		public bool AutoRestore {get {return _AutoRestore;} set{_AutoRestore=true;}}

		/// <summary>
		/// Saves the value into the context.
		/// </summary>
		public virtual void SaveContext() 
		{
			if (Text!="") Context.Items.Add(UniqueID, Text);
		}
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			HiddenControls.RenderHidden(output, UniqueID, (Encrypted)?Cryptor.Encrypt(Text, Constants.IK_STRING):Text);
		}

		/// <summary>Initialize event method</summary>
		protected override void OnInit(EventArgs e) 
		{
			base.OnInit(e);
			EnableViewState=false;
			if (AutoRestore && !Page.IsPostBack) Text=RestoreValue(UniqueID);
		}

		/// <summary>
		/// IPostBackDataHandler.LoadPostData.  Restores Text from the Request.Form
		/// </summary>
		/// <param name="postDataKey">The key to lookup</param>
		/// <param name="postCollection">Complete collection of postback values</param>
		public virtual bool LoadPostData(string postDataKey, NameValueCollection postCollection) 
		{
			String presentValue = Text;
			String postedValue = postCollection[postDataKey];
			if (Encrypted) postedValue = Cryptor.Decrypt(postedValue, Constants.IK_STRING);
			if (presentValue == null || !presentValue.Equals(postedValue))
			{
				Text = postedValue;
				return true;
			}
			return false;
		}

		/// <summary>
		/// IPostBackDataHandler.RaisePostDataChangedEvent.  Does nothing, since viewstate
		/// is always disabled for this control type.
		/// </summary>
		public virtual void RaisePostDataChangedEvent() {}

		/// <summary>
		/// Restores the value for item from the context items.
		/// </summary>
		/// <param name="item">The item to restore.</param>
		/// <returns>string value.</returns>
		public string RestoreValue(string item) 
		{
			return (Context.Items[item]==null)?"":Context.Items[item].ToString();
		}
	}
}


namespace CSAAWeb.WebControls.Design
{
	/// <summary>
	/// Designer for Hidden Input class.
	/// </summary>
	public class HiddenDesigner : System.Web.UI.Design.ControlDesigner 
	{
		/// <summary>Style string</summary>
		private string Style = "border:2 outset; background-color:gray;color:white; font-size:9; font-family:Arial;";
		/// <summary>
		/// Generates normal design time HTML
		/// </summary>
		/// <returns>HTML string</returns>
		public override string GetDesignTimeHtml() 
		{
			HiddenInput H = (HiddenInput) Component;
			return "<table><tr><td style='"+Style+"'><b>"+H.GetType().Name+": " + H.ID + "</b></td></tr></table>";

		}
		/// <summary>
		/// Generates DesignTime HTML if no properties set.
		/// </summary>
		/// <returns>HTML string</returns>
		protected override string GetEmptyDesignTimeHtml() 
		{
			HiddenInput H = (HiddenInput) Component;
			return "<table><tr><td style='"+Style+"'><b>"+H.GetType().Name+"</b></td></tr></table>";
		}
	}
}

