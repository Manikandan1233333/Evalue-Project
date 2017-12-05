using System;
using System.Web;
using System.Web.UI;
using System.IO;
using System.Configuration;
using System.Collections;
using System.Collections.Specialized;

namespace CSAAWeb.Filter
{
	/// <summary>
	/// Tagfilter is a control who's task is to install a response filter that will
	/// add a configurable list of controls to any page that uses it.
	/// </summary>
	internal class TagFilter : System.Web.UI.Control
	{
		private static FilterList BaseList = null;
		private ArrayList FwdList = new ArrayList();
		private ArrayList BkupList = new ArrayList();
		private Control Owner = null;

		internal TagFilter(Control Creator) 
		{
			Owner = Creator;
			if (Creator.Page==null) 
			{
				if (CheckInstalled(Creator)) return;
				Owner.Controls.Add(this);
			} else {
				if (CheckInstalled(Creator.Page)) return;
				Page = Creator.Page;
				Page.Controls.Add(this);
			}
		}

		/// <summary>
		/// Call this to fire the init event
		/// </summary>
		public void DoInit(EventArgs e) {
			if (!ChildControlsCreated) CreateChildControls();
			OnInit(e);
		}
		/// <summary>
		/// Returns true if there is already an instance of this filter in the
		/// Controls collection of ToCheck
		/// </summary>
		public bool CheckInstalled(Control ToCheck) 
		{
			foreach (Control F in ToCheck.Controls)
				if (F.GetType().Name==this.GetType().Name) return true;
			return false;
		}

		/// <summary>
		/// CreateChildControls is fired on the Init event, and creates the
		/// custom Response Filter for the page.
		/// </summary>
		protected override void CreateChildControls()
		{
			if (BaseList==null) 
				BaseList = (Page.Site!=null && Page.Site.DesignMode)?
					(new FilterList()):(new FilterList(Page));
			foreach (FilterTags F in BaseList.Values) AddFilter(F);
			ChildControlsCreated=true;
			//Page.Response.Filter = new HtmlFilter(Page.Response.Filter, new FilterEvent(DoFilter));
		}

		/// <summary>
		/// Override of base Render method. Renders the child controls contained in
		/// the filters to the HTML property of each filter, but doesn't render to the
		/// output.
		/// </summary>
		protected override void Render(HtmlTextWriter Writer) 
		{
			foreach (FilterTags F in FwdList) F.Render();
			foreach (FilterTags F in BkupList) F.Render();
		}

		/// <summary>
		/// Adds the Filter, initializes the controls in the filter, and adds them
		/// to the child controls.
		/// </summary>
		private void AddFilter(FilterTags Filter) 
		{
			FilterTags F = Filter.Clone(Page);
			// In case the filter itself is an instance of Template.UserControl
			if (F.CheckMatch(Owner) && F.Empty) return;
			((!Filter.IsBackup)?FwdList:BkupList).Add(F);
			if (F.Content!=null) Controls.Add(F.Content.Control);
			if (F.Replace!=null) Controls.Add(F.Replace.Control);
		}

		/// <summary>
		/// This is the actual Response filter.
		/// </summary>
		/// <param name="b"></param>
		public void DoFilter(FilterBuffer b)
		{
			int j; 
			foreach (object o in BkupList) FwdList.Add(o);
			if (FwdList.Count>0) {
				int i=b.IndexOf('<'); 
			//TODO: Add backup.
			//TODO: handle multiple calls.
				while (i < b.Last) {
					j=b.IndexOf('>', i);  
					while (InComment(b, i, j)) j = b.IndexOf('>', j+1);
					if (i<j) i=b.IndexOf('<', HandleTag(b, i, j, b.ToString(i, j), FwdList)); 
				}
			}
			b.Write();
		}

		/// <summary>
		/// Returns true if position in b at i starts a comment that isn't completed
		/// by the close carot at j.
		/// </summary>
		private bool InComment(FilterBuffer b, int i, int j) 
		{
			if (j<i+7 || b.ToString(i,i+3)!="<!--") return false;
			return (b.ToString(j-2, j)!="-->" && j<b.Last);
		}

		/// <summary>
		/// Checks the located tag for a match, and does the required operation if found.
		/// Returns the next position to check.
		/// </summary>
		private int HandleTag(FilterBuffer b, int i, int j, string tag, ArrayList FilterList) 
		{
			foreach (FilterTags Filter in FilterList) {
				if (String.Compare(tag,1,Filter.Tag,0,Filter.Tag.Length,true)==0) {
					if (!Filter.Multiple) FilterList.Remove(Filter);
					if (tag[1]!='/') {
						OpenTag(b, i, j, tag, Filter); 
						j=b.offset;
					} else CloseTag(b, i, Filter);
					if (FilterList.Count==0) return b.Last;
					break;
				}
			}
			return j+1;
		}

		/// <summary>
		/// Handles the filters for closing html tags
		/// </summary>
		private void CloseTag(FilterBuffer b, int i, FilterTags Filters) 
		{
			i = b.SkipWhiteSpace(i, true);
			b.Write(i);
			b.Write(Filters.Content.HTML);
			b.Seek(i);
		}

		/// <summary>
		/// Handles the filters for opening html tags
		/// </summary>
		private void OpenTag(FilterBuffer b, int i, int j, string tag, FilterTags Filters) 
		{
			b.Write(i);
			if (Filters.Replace!=null) tag = Filters.Replace.HTML;
			foreach (FilterTag F in Filters.Attributes) F.Substitute(ref tag);
			b.Write(tag);
			if (Filters.Content!=null) b.Write(Filters.Content.HTML);
			b.Seek(j+1);
		}

	}

	/// <summary>
	/// FilterList class is used to capture the base filtering information.
	/// </summary>
	internal class FilterList : ListDictionary 
	{
		private static HttpServerUtility _Server = null;
		private static string _SiteRoot = string.Empty;
		private static string[,] DefaultTemplates = new string[,] {
				{"Template_Head_End", "file=%SiteRoot%shared/head.ascx"},
				{"Template_Body_Start", "file=%SiteRoot%shared/header.ascx"},
				{"Template_Body_End", "file=%SiteRoot%shared/footer.ascx"}
																  };
		public FilterList() 
		{
			Init();
		}

		public FilterList(Page _Page) 
		{
			_Server = _Page.Server;
			Init();
		}

		private void Init() 
		{
			_SiteRoot=Config.SiteRoot;
			for (int i=0; i<DefaultTemplates.GetLength(0); i++)
				AddFilter(DefaultTemplates[i,0], DefaultTemplates[i,1]);
			foreach (string key in ConfigurationSettings.AppSettings.AllKeys)
				if (key.IndexOf("Template_")==0) AddFilter(key, ConfigurationSettings.AppSettings[key]);
			ArrayList RemoveMe = new ArrayList();
			foreach (DictionaryEntry entry in this) 
				if (!((FilterTags)entry.Value).CheckSources()) RemoveMe.Add(entry.Key);
			foreach (object key in RemoveMe) Remove((string)key);
		}

		private static string MapPath(string path) 
		{
			return _Server.MapPath(path);
		}

		/// <summary>
		/// Adds a filter to the unrendered chain of filters.
		/// </summary>
		/// <param name="key"></param>
		/// <param name="source"></param>
		private void AddFilter(string key, string source) 
		{
			FilterTag Tag1 = new FilterTag(key, source);
			FilterTags Tag = (FilterTags)this[Tag1.Tag];
			if (Tag!=null) 
			{
				Tag.Add(Tag1);
			} 
			else this[Tag1.Tag] = new FilterTags(Tag1);
		}

		/// <summary>
		/// Checks to see if the filter definition is a control file, and if so, checks to
		/// see if it actually exists.
		/// </summary>
		/// <param name="VPath"></param>
		/// <returns></returns>
		public static string CheckFile(string VPath) 
		{
			if (VPath.ToLower().IndexOf("file=")<0) return VPath;
			VPath = VPath.Substring(VPath.ToLower().IndexOf("file=")+5);
			VPath = VPath.Replace("%SiteRoot%", _SiteRoot);
			if (VPath.Length==0) return "";
			if (!File.Exists(MapPath(VPath))) VPath = "";
			return VPath;
		}

		/// <summary>
		/// Property representing the virtual root of the application.
		/// </summary>
		public static string SiteRoot 
		{get{return _SiteRoot;} set {_SiteRoot=value;}}
	}

}
