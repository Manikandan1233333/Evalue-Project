using System;
using System.Collections;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Text.RegularExpressions;

namespace CSAAWeb.Filter
{
	/// <summary>
	/// FilterTags encapsulates all the filters for a giving begin or end tag.
	/// </summary>
	internal class FilterTags : ICloneable 
	{
		public string Tag=string.Empty;
		public FilterTag Content;
		public FilterTag Replace;
		public ArrayList Attributes = new ArrayList();
		public bool Multiple = true;
		public bool IsBackup = false;

		/// <summary>
		/// Default constructor for cloning.
		/// </summary>
		public FilterTags() {}
		
		/// <summary>
		/// Primary constructor, for tag with at least one filter.
		/// </summary>
		/// <param name="Filter">The filter to add.</param>
		public FilterTags(FilterTag Filter) 
		{
			Tag = Filter.Tag;
			Add(Filter);
		}

		/// <summary>
		/// Generic object add for cloning.
		/// </summary>
		/// <param name="Filter">The filter to add</param>
		private void Add(object Filter) 
		{
			Add((FilterTag)Filter);
		}

		/// <summary>
		/// Adds the Filter to the filters for this tag.
		/// </summary>
		/// <param name="Filter">The filter to add</param>
		public void Add(FilterTag Filter) 
		{
			switch (Filter.FilterType) 
			{
				case FilterTypeEnum.Start: Content = Filter; break;
				case FilterTypeEnum.End: Content = Filter; break;
				case FilterTypeEnum.Replace: Replace = Filter; break;
				case FilterTypeEnum.Attribute: Attributes.Add(Filter); break;
			}
		}

		/// <summary>
		/// For filters that generate a control from a source file, this checks to
		/// see if the files exists first.  This checks all the filters for the tag
		/// </summary>
		/// <returns>True if there is at least one filter for the tag with a valid source</returns>
		public bool CheckSources() 
		{
			if (Content!=null) 
			{
				Content.Source=FilterList.CheckFile(Content.Source);
				if (Content.Source.Length==0) Content=null;
			}
			if (Replace!=null) 
			{
				Replace.Source=FilterList.CheckFile(Replace.Source);
				if (Replace.Source.Length==0) Replace=null;
			}
			return (Content!=null || Replace!=null || Attributes.Count>0);
		}

		public void Render() 
		{
			if (Content!=null) Content.Render();
			if (Replace!=null) Replace.Render();
		}
		/// <summary>
		/// ICloneable interface
		/// </summary>
		/// <returns>Clone of object</returns>
		public object Clone() 
		{
			FilterTags result = new FilterTags();
			if (Content!=null) result.Add(Content.Clone());
			if (Replace!=null) result.Add(Replace.Clone());
			foreach (FilterTag i in Attributes) result.Add(i.Clone());
			result.Multiple = Multiple;
			result.IsBackup = IsBackup;
			result.Tag = Tag;
			return result;
		}

		/// <summary>
		/// ICloneable.Clone.
		/// </summary>
		/// <param name="_Control">The control to clone.</param>
		/// <returns>A clone of control.</returns>
		public FilterTags Clone(TemplateControl _Control) 
		{
			return ((FilterTags)Clone()).Init(_Control);
		}

		/// <summary>
		/// Initializes the properties and returns this.
		/// </summary>
		/// <param name="_Control">The control to initialize this with.</param>
		/// <returns>this</returns>
		private FilterTags Init(TemplateControl _Control) 
		{
			if (Content!=null) Content.Init(_Control);
			if (Replace!=null) Replace.Init(_Control);
			return this;
		}

		/// <summary>
		/// True if there are no filters here.
		/// </summary>
		public bool Empty 
		{ get { return (Content==null && Replace == null && Attributes.Count==0); }}

		/// <summary>
		/// True if Owner is same type as Content and if so, clears content.
		/// </summary>
		/// <param name="Owner"></param>
		/// <returns></returns>
		public bool CheckMatch(Control Owner) 
		{
			if (Content==null) return false;
			if (Content.Control.GetType().Name==Owner.GetType().Name) 
			{
				Content.Control = null;
				Content = null;
				return true;
			} else return false;
		}

	}

	internal enum FilterTypeEnum {Unknown, Start, End, Replace, Attribute}

	/// <summary>
	/// FilterTag encapsulates all the information needed to filter a specific tag,
	/// in one specific way (before, after, replace, attribute)
	/// </summary>
	internal class FilterTag : ICloneable
	{
		public string Tag = string.Empty;
		public string Source = string.Empty;
		public FilterTypeEnum FilterType = FilterTypeEnum.Unknown;
		public string HTML = string.Empty;
		public System.Web.UI.Control Control = null;
		public string Attribute = string.Empty;
		public bool IsBackup = false;
		public bool Multiple = true;

		/// <summary>
		/// Default constructor for cloning.
		/// </summary>
		internal FilterTag() {}

		/// <summary>
		/// Primary constructor to create from configuration.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="source"></param>
		internal FilterTag(string key, string source) 
		{
			Source = source;
			ExtractInfo(key);
		}

		/// <summary>
		/// Extracts properties from the configuration file key name.
		/// </summary>
		/// <param name="key"></param>
		private void ExtractInfo(string key) 
		{
			key = key.Replace("Template_","");
			Tag = key.Substring(0, key.IndexOf("_")).ToLower();
			key = key.Substring(key.IndexOf("_")+1);
			Multiple = (Tag!="body" && Tag!="head" && Tag!="html");
			switch (key.ToLower()) 
			{
				case "start": FilterType = FilterTypeEnum.Start; break;
				case "end": 
					FilterType = FilterTypeEnum.End; 
					IsBackup = (Tag=="body" || Tag=="html");
					Tag = "/" + Tag; 
					break;
				case "replace": FilterType = FilterTypeEnum.Replace; break;
				default : FilterType = FilterTypeEnum.Attribute; Attribute = key; break;
			}

		}

		/// <summary>
		/// Sets the HTML property by rendering the contained Control.
		/// </summary>
		/// <returns></returns>
		public void Render() 
		{
			if (Control==null) HTML = Source;
			HtmlTextWriter writer = new HtmlTextWriter(new StringWriter());
			Control.RenderControl(writer);
			HTML = writer.InnerWriter.ToString();
			Control = null;
		}

		public void Init(TemplateControl Control) 
		{
			this.Control = Control.LoadControl(Source);
		}

		/// <summary>
		/// ICloneable interface
		/// </summary>
		/// <returns>Clone of object</returns>
		public object Clone() 
		{
			FilterTag result = new FilterTag();
			result.Tag = Tag;
			result.Source = Source;
			result.FilterType = FilterType;
			result.HTML = HTML;
			result.Control = Control;
			result.Attribute = Attribute;
			result.IsBackup = IsBackup;
			result.Multiple = Multiple;
			return result;
		}

		/// <summary>
		/// Performs an attribute substitution on tag.
		/// </summary>
		public void Substitute(ref string tag) 
		{
			Regex R = new Regex(Attribute + "\\s*=\\s*\"?", RegexOptions.IgnoreCase);
			Match M = R.Match(tag);
			if (M.Success) {
				tag = tag.Insert(M.Index + M.Length, Source + ";");
			} else tag = tag.Replace(">", " " + Attribute + "=\"" + Source + "\">");
		}

	}
}
