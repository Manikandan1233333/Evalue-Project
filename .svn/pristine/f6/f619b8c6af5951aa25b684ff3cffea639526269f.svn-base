using System;
using System.Web.UI;
using System.Web;
using System.Web.UI.WebControls;

namespace CSAAWeb.WebControls
{
	/// <summary>
	/// Classes that implement IHideable know how to render themselves and 
	/// their child controls as hidden form elements, and have special handling
	/// of validation.
	/// </summary>
	public interface IHideable
	{
		/// <summary>True if the control should render as a hidden</summary>
		bool Hidden {get; set;}
		/// <summary>Saves values of the control into the Context.Items collection</summary>
		void SaveContext();
	}

	/// <summary>
	/// HiddenControls contains class methods that can be methods used by IHideable
	/// classes to assist in rendering hidden controls.
	/// </summary>
	public class HiddenControls 
	{
		/// <summary>
		/// Sets the child controls' hidden property.
		/// </summary>
		public static void HideChildControls(Control H, bool Hide) 
		{
			foreach (Control C in H.Controls) 
				if (typeof(IHideable).IsInstanceOfType(C))
					((IHideable)C).Hidden=Hide;
		}

		/// <summary>
		/// Calls IHideable.SaveContext on any hideable child controls.
		/// </summary>
		/// <param name="H"></param>
		public static void SaveChildContext(Control H) 
		{
			foreach (Control C in H.Controls) 
				if (typeof(IHideable).IsInstanceOfType(C)) ((IHideable)C).SaveContext();
		}

		/// <summary>
		/// Default tabs for Hidden controls
		/// </summary>
		public static string BeginTag = "\r\t\t";

		/// <summary>
		/// Outputs a hidden control with the specified name and value.
		/// </summary>
		public static void RenderHidden(HtmlTextWriter output, string Name, string Value) 
		{
			output.Write(BeginTag);
			output.WriteBeginTag("input");
			output.WriteAttribute("type", "hidden");
			output.WriteAttribute("name", Name);
			output.WriteAttribute("value", Value, true);
			output.Write(HtmlTextWriter.TagRightChar);
		}

		/// <summary>
		/// Renders a non-hidden, non-IHideable web form control as a hidden element or set
		/// of hidden elements.
		/// </summary>
		/// <param name="output"></param>
		/// <param name="C"></param>
		public static void RenderControlAsHidden(HtmlTextWriter output, Control C) 
		{
			switch (C.GetType().Name) {
				case "TextBox": RenderHidden(output, C.UniqueID, ((TextBox)C).Text); break;
				case "CheckBoxList": RenderList(output, (ListControl)C); break;
				case "RadioButtonList": RenderSingleItemList(output, (ListControl)C); break;
				case "DropDownList": RenderSingleItemList(output, (ListControl)C); break;
				case "ListBox": RenderList(output, (ListControl)C); break;
			}
		}

		/// <summary>
		/// Renders the hidden elements for a single item list.
		/// </summary>
		/// <param name="output">Output stream to render to</param>
		/// <param name="C">The control to render.</param>
		private static void RenderSingleItemList(HtmlTextWriter output, System.Web.UI.WebControls.ListControl C) 
		{
			if (C.SelectedIndex>=0) RenderHidden(output, C.UniqueID, C.SelectedItem.Value);
		}

		/// <summary>
		/// Renders a series of hidden elements for a multi-select type of list.
		/// </summary>
		/// <param name="output">Output stream to render to</param>
		/// <param name="C">The control to render.</param>
		private static void RenderList(HtmlTextWriter output, System.Web.UI.WebControls.ListControl C) 
		{
			foreach (ListItem i in C.Items) 
				if (i.Selected) RenderHidden(output, C.UniqueID, i.Value);
		}

	}
}
