//This file contains some miscellaneous classes for manipulating controls.
using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;
using System.Reflection;

namespace CSAAWeb.WebControls
{
	#region ListC
	/// <summary>
	/// Class contains methods for setting and retrieving the "Value Property" of
	/// ListControl Controls, which cannot natively do this.
	/// </summary>
	public class ListC
	{
		/// <summary>
		/// Returns the value of the selected item.
		/// </summary>
		/// <param name="C">Control to get value from</param>
		/// <returns>The Value of the control.</returns>
		public static string GetListValue(ListControl C) 
		{
			return (C.SelectedItem==null)?"":C.SelectedItem.Value;
		}
		
		/// <summary>
		/// Sets the selectedindex of the control to the item that
		/// matches value, if any.
		/// </summary>
		/// <param name="C">The control to set the value of.</param>
		/// <param name="Value">Value to match</param>
		public static void SetListIndex(ListControl C, string Value) 
		{
			for (int i=0; i<C.Items.Count; i++)
				if (C.Items[i].Value==Value) {C.SelectedIndex=i; return;}
			C.SelectedIndex=-1;
		}
	}
	#endregion
	#region Renderer
	/// <summary>
	/// Provides a static method that allows a control to be rendered to a string.
	/// </summary>
	public class Renderer 
	{
		/// <summary>
		/// Renders C to a string.
		/// </summary>
		/// <param name="C">The control to render.</param>
		/// <returns>String of rendered control.</returns>
		public static string Render(Control C) 
		{
			HtmlTextWriter writer = new HtmlTextWriter(new StringWriter());
			C.RenderControl(writer);
			string Result = writer.InnerWriter.ToString();
			return Result;
		}

		/// <summary>
		/// Renders URL to a string by calling Server.Execute()
		/// </summary>
		/// <param name="Url">The URL to execute</param>
		/// <param name="Page">The executing page to render from.</param>
		/// <returns>String of rendered control.</returns>
		public static string Render(string Url, System.Web.UI.Page Page) {
			HtmlTextWriter writer = new HtmlTextWriter(new StringWriter());
			Page.Server.Execute(Url, writer);
			string Result = writer.InnerWriter.ToString();
			return Result;
		}
	}
	#endregion
	#region DataBinder
	/// <summary>
	/// Provides a static method for rendering columns from child rows in a container's 
	/// DataItem.
	/// </summary>
	public class DataBinder {
		private static Regex Re;
		///<summary/>
		static DataBinder() {
			Re = new Regex("\\{.*\\}", RegexOptions.Compiled);
		}
		/// <summary>
		/// Evaluates data-binding expressions at runtime and formats the result as text to be 
		/// displayed in the requesting browser.
		/// </summary>
		/// <param name="DataItem">
		/// The object reference against which the expression is evaluated. 
		/// This must be a valild object identifier in the page's specified language,
		/// and furthermore, this must evaluate to a DataRowView which contains
		/// a relation (Relationship) to another DataTable in the DataSet.
		/// </param>
		/// <param name="Column">The column name of the column to use in the related DataTable</param>
		/// <param name="Format">Format string to use, including the separator.</param>
		/// <param name="Relationship">Name of the relationship between the two data tables.</param>
		/// <returns>String concatenating the values from the related table.</returns>
		public static string Eval(object DataItem, string Column, string Format, string Relationship) {
			string Result = "";
			string Separator = Re.Replace(Format, "");
			foreach (DataRow Row in ((DataRowView)DataItem).Row.GetChildRows(Relationship)) {
				if (Format.Length==Separator.Length) {
					Result += Row[Column].ToString() + Separator;
				} else {
					object o = Row[Column];
					MethodInfo M = o.GetType().GetMethod("ToString", new Type[] {typeof(string)});
					Result += ((M==null)?(o.ToString() + Separator):M.Invoke(o, new object[] {Format}));
				}
			}
			if (Result.Length>0) Result = Result.Substring(0,Result.Length-Separator.Length);
			return Result;
		}
	}
	#endregion
}
