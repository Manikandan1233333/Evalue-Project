using System;
using System.Web;
using CSAAWeb.AppLogger;
using System.Web.Services.Protocols;
using System.Reflection;
using System.Collections;

namespace CSAAWeb.Web
{
	/// <summary>
	/// Base class for a web application that permits the programmatic addition
	/// of new HttpModules via the ModulesX property <see cref="Web.ExtendableModuleCollection"/>.  
	/// This allows applications to be built that require specific modules and will install them
	/// even if not provided in web.config.
	/// </summary>
	public class ExtendableHttpApplication : System.Web.HttpApplication
	{
		/// <summary>
		/// All the modules in this, included ones added internally
		/// </summary>
		public ExtendableModuleCollection ModulesX;
		/// <summary>Disposes of any extra modules.</summary>
		public override void Dispose() {
			ModulesX.Dispose();
			ModulesX=null;
		}
	}

	/// <summary>
	/// HttpApplication Class that automatically adds HttpsModule, LogModule and ClosableModule
	/// </summary>
	public class HttpApplication : ExtendableHttpApplication {
		/// <summary>
		/// Init adds BeginRequest and Error events.
		/// </summary>
		public override void Init() {
			ModulesX = new ExtendableModuleCollection(Modules);
			if (!ModulesX.Contains("LogModule")) ModulesX.Add(new LogModule(this));
			if (!ModulesX.Contains("HttpsModule")) ModulesX.Add(new HttpsModule(this));
			if (!ModulesX.Contains("ClosableModule")) ModulesX.Add(new ClosableModule(this));
		}

		/// <summary>Application OnEnd event.  Artifact from previous implementation.</summary>
		protected virtual void Application_End(object Source, EventArgs e) {

		}

	}
}
