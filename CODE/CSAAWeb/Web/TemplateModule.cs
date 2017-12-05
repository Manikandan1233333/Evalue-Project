using System;
using System.Web;
using CSAAWeb.AppLogger;
using System.Web.Services.Protocols;

namespace CSAAWeb.Web {
	/// <summary>Module allows for forcing Template.</summary>
	/// 
	/// <remarks>
	/// <para>
	/// TemplateModule must be installed within an application through web.config, in the httpModules
	/// section of the system.web configuration section.  
	/// Several settings in the appSettions section control the operation.  See
	/// the example web.config file below.  
	/// </para>
	/// </remarks>
	/// 
	/// <example>
	/// <para>Here is a sample web.config file for using TemplateModule:</para>
	/// <code>
	///&lt;?xml version="1.0" encoding="utf-8" ?&gt;
	///&lt;configuration&gt;
	///
	///    &lt;appSettings&gt;
	///        &lt;!-- Template --&gt;
	///        &lt;add key="Template_Head_End" value="file=%SiteRoot%code/shared/head.ascx"/&gt; 
	///        &lt;add key="Template_Body_Start" value="file=%SiteRoot%code/shared/header.ascx"/&gt;
	///        &lt;add key="Template_Body_End" value="file=%SiteRoot%code/shared/footer.ascx"/&gt;
	///        &lt;add key="Template_Body_onload" value="head_onload()"/&gt;
	///        &lt;add key="Template_Body_onunload" value="rememberLocation()"/&gt;
	///    &lt;/appSettings&gt;
	///    
	///    &lt;system.web&gt;
	///        &lt;!--Install TemplateModule with the following tag:--&gt;
	///
	///        &lt;httpModules&gt;
	///            &lt;add name="TemplateModule" type="CSAAWeb.Web.TemplateModule, CSAAWeb"/&gt;
	///        &lt;/httpModules&gt;
	///
	///     &lt;/system.web&gt;
	///
	///&lt;/configuration&gt;
	///</code>
	///</example>
	public class TemplateModule : IHttpModule {
		///<summary>Default Constructor for TemplateModule</summary>
		public TemplateModule() : base() {}
		///<summary>Constructs instance of TemplateModule and calls Init.</summary>
		public TemplateModule(System.Web.HttpApplication Application) : base() {
			Init(Application);
		}

		///<summary>Implements IHttpModule.Init.  Hooks to Application_OnPreRequestHanderExecute.</summary>
		public void Init(System.Web.HttpApplication Application) {
			Application.PreRequestHandlerExecute+=new EventHandler(OnPreRequestHandlerExecute);
		}

		///<summary>Implements IHttpModule.Dispose.</summary>
		public void Dispose() {
		}

		/// <summary>
		/// OnPreRequestHandler adds a DefaultTemplate control to every page that
		/// isn't already an instance of PageTemplate and which doesn't implement
		/// INoTemplate
		/// </summary>
		private void OnPreRequestHandlerExecute(object Source, EventArgs e) {
			System.Web.HttpApplication Application = (System.Web.HttpApplication)Source;
			System.Web.UI.Page Page = Application.Context.Handler as System.Web.UI.Page;
			if (Page!=null && !typeof(WebControls.PageTemplate).IsInstanceOfType(Page) &&
				!typeof(INoTemplate).IsInstanceOfType(Page)) {
					Page.Init += new EventHandler(OnInit);
				}
		}
		private void OnInit(object Source, EventArgs e) {
			System.Web.UI.Page Page = Source as System.Web.UI.Page;
			if (Page==null) return;
			WebControls.DefaultTemplate T = new WebControls.DefaultTemplate();
//			if (!T.CheckInstalled(Page)) Page.Controls.Add(T);
//			Code Modified by Cognizant on 06/21/2004 as Requested by the Client on 6/15/2004
			if (!T.CheckInstalled(Page)) Page.Controls.AddAt(0,T);
		}
	}


	/// <summary>
	/// Marker interface for pages that must not be templated in applications using
	/// Template Module.
	/// </summary>
	public interface INoTemplate {
	}

}
