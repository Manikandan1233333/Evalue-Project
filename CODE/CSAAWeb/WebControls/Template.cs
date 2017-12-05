using System;
using System.Web;
using System.Web.UI;
using System.ComponentModel;
using CSAAWeb.Filter;

namespace CSAAWeb.WebControls
{
	/// <summary>
	/// Template is an extension of the System.Web.UI.Page class which creates a custom
	/// Response Filter before rendering itself, that will insert template elements into
	/// the Response stream to give all pages derived from this class a common look and feel.
	/// </summary>
	public class PageTemplate : Page
	{
		/// <summary>
		/// Constructor adds a PreRender event to the chain.
		/// </summary>
		public PageTemplate() : base() 
		{
			Controls.Add(new DefaultTemplate());
		}

		/// <summary>
		/// Property representing the virtual root of the application.
		/// </summary>
		public string SiteRoot { 
		 get{return Config.SiteRoot;} set{}} 

		/// <summary>
		/// If the page contains a navigation ACL control, this will be it.
		/// </summary>
		public Navigation.ACL NavACL;
	}

	/// <summary>
	/// DefaultTemplate is a base class for a web control that installs the Response Filter.
	/// </summary>
	[ToolboxData("<{0}:DefaultTemplate runat=server/>")]
	public class DefaultTemplate : System.Web.UI.WebControls.WebControl
	{
		/// <summary>
		/// Constructor creates the filter.
		/// </summary>
		
		public DefaultTemplate() : base()
		{
			Filter = new TagFilter(this);
		}

		/// <summary>
		/// The filter.
		/// </summary>
		private TagFilter Filter = null;
		/// <summary>
		/// Reference to the actual output stream.
		/// </summary>
		private HtmlTextWriter Output = null;

		///<summary/>
		protected override void OnLoad(EventArgs e) {
			if (Filter!=null) Filter.DoInit(e);
		}
		/// <summary>
		/// Checks to set if there are any other controls of this type and disables
		/// them if so, to insure that there is only one filter on a page.
		/// </summary>
		protected override void CreateChildControls() 
		{
			if (Filter==null) return;
			foreach (Control C in Page.Controls) 
				if (C.GetType().Name==this.GetType().Name && C!=this) ((DefaultTemplate)C).Disable();
			if (Filter.CheckInstalled(Page)) Disable();
		}

		/// <summary>
		/// Returns true if there is already an instance of this filter in the
		/// Controls collection of ToCheck
		/// </summary>
		public bool CheckInstalled(Control ToCheck) {
			string Name = GetType().Name;
			foreach (Control C in ToCheck.Controls)
				if (C.GetType().Name==Name && C!=this) return true;
			return false;
		}


		/// <summary>
		/// Disables this control.
		/// </summary>
		public void Disable() 
		{
			Controls.Remove(Filter);
			Filter=null;
		}

		/// <summary> 
		/// Render this control to the output parameter specified.
		/// </summary>
		/// <param name="output"> The HTML writer to write out to </param>
		protected override void Render(HtmlTextWriter output)
		{
			Output = output;
			if (Filter==null) return;
			output.InnerWriter = new HttpFilteredWriter((HttpWriter)output.InnerWriter, Filter);
			this.RenderChildren(output);
			if (Site!=null && Site.DesignMode && Filter!=null) 
			{
				output.Write("<table><tr><td style='background:blue; color:white;'>Web Site Default Format Component</td></tr></table>");
			}
		}

		/// <summary>
		/// Insures that the output is flushed and set to null.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnUnload(EventArgs e) 
		{
			if (Output!=null) Output.Flush();
			Output = null;
			base.OnUnload(e);
		}

	}
}
