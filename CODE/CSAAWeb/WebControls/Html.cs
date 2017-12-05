using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using System.Collections.Specialized;
using System.Text.RegularExpressions;

namespace CSAAWeb.WebControls
{
	/// <summary>
	/// A Custom control for server Html.
	/// </summary>
	[DefaultProperty("Text"), 
	ToolboxData("<{0}:Html runat=server/>")]
	public class Html : System.Web.UI.WebControls.WebControl
	{
		/// <summary>Default constructor</summary>
		public Html() : base () {}
		/// <summary>Constructor accepting id and text properties.</summary>
		/// <param name="id">The ID of the control.</param>
		/// <param name="Value">The value for Text property</param>
		public Html(string id, string Value) : base() {
			ID = id;
			Text = Value;
		}

		/// <summary>The value of the hidden element.</summary>
		[Bindable(true), Category("Data"), DefaultValue("")] 
		public string Text = string.Empty;

		///<summary>If greater than 0, will chop the output.</summary>
		public int MaxLength = 0;

		///<summary>If false will remove any html tags.</summary>
		public bool AllowHtml = true;

		private Regex r = new Regex("<.*?>", RegexOptions.Singleline | RegexOptions.Compiled);
		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output) {
			if (!AllowHtml)
				Text = r.Replace(Text,"");
			if (MaxLength>0 && Text.Length>MaxLength) 
				Text=Text.Substring(0,MaxLength);
			output.Write(Text);
		}

	}
}
