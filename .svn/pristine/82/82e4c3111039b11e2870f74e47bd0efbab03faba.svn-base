using System;
using System.Web;
using CSAAWeb.AppLogger;
using System.Web.Services.Protocols;
using System.IO;

namespace CSAAWeb.Web
{
	/// <summary>
	/// Module intercepts and logs all unhandled exceptions on pages and web services.
	/// </summary>
	/// 
	/// <remarks>
	/// <para>
	/// LogModule must be installed within an application through web.config, in the httpModules
	/// section of the system.web configuration section.  CustomErrors must also be turned
	/// off.  There are several settings in the appSettions section that control operation.  See
	/// the example web.config file below.
	/// </para>
	/// <para>
	/// See <see cref="AppLogger.Logger"/> for more information about logging, and for registry values
	/// to permit logging to the Application Event log instead of to a file.
	/// </para>
	/// <seealso cref="AppLogger.Logger"/>
	/// <seealso cref="Config.ExpandedSetting"/>
	/// <seealso cref="Queue"/>
	/// </remarks>
	/// 
	/// <example>
	/// <para>Here is a sample web.config file for using LogModule:</para>
	/// <code>
	///&lt;?xml version="1.0" encoding="utf-8" ?&gt;
	///&lt;configuration&gt;
	///
	///    &lt;appSettings&gt;
	///        &lt;!--ApplicationLogPath determines where LogModule will place log files. It can be an absolute path,
	///        or it can be an expanded path.  Acceptable variables for expansion are %SiteRootPath% which will	
	///        be expanded to a path under the virtual directory of the application or %RootPath% which will be
	///        expanded to a path under the web server root. If this value ends with "\", the top level application namespace
	///        will be prepended to the file name.  If it is missing, then application will attempt to
	///        log into the Application Event Log.  
	///        NOTE: VERY IMPORTANT! ASP.NET must have write permissions to this directory!--&gt;
	///        &lt;add key="ApplicationLogPath" value="%SiteRootPath%data\Logs\" /&gt;
	///
	///        &lt;!-- If Error_Url is supplied, then LogModule will redirect to this page after logging
	///        any unhandled exception. --&gt;
	///        &lt;add key="Error_Url" value="%SiteRoot%error.aspx"/&gt;
	///
	///        &lt;!-- Log_Errors determines if LogModule will automatically log unhandled exceptions.  By default,
	///        Log_Errors is true, so it only needs to be in web.config if you want it to be false.
	///        &lt;add key="Log_Errors" value="true"/&gt;
	///    &lt;/appSettings&gt;
	///
	///    &lt;system.web&gt;
	///        &lt;!--Install LogModule with the following tag:--&gt;
	///
	///        &lt;httpModules&gt;
	///            &lt;add name="LogModule" type="CSAAWeb.Web.LogModule, CSAAWeb"/&gt;
	///        &lt;/httpModules&gt;
	///
	///        &lt;!-- customErrors must be set to off to allow LogModule to function.  If it is On or Remote,
	///        the ASP.NET built-in Exception handling will execute before LogModule, and LogModule will never
	///        be called. --&gt;
	///        &lt;customErrors mode="Off" /&gt; 
	///
	///    &lt;/system.web&gt;
	///
	///&lt;/configuration&gt;
	/// </code>
	/// </example>
	public class LogModule : IHttpModule
	{
		///<summary>Default Constructor for LogModule.</summary>
		public LogModule() : base() {}
		///<summary>Constructs instance of LogModule and calls Init.</summary>
		public LogModule(System.Web.HttpApplication Application) : base() {
			Init(Application);
		}
		private static bool LogErrors = false;
		private static string ErrorUrl = "";
		private static string SoapErrorMessage = "";
		internal static int Instances = 0;

		///<summary/>
		static LogModule() {
			LogErrors = (Config.bSetting("Log_Errors") || Config.Setting("Log_Errors")=="");
			ErrorUrl = Config.ExpandedSetting("Error_Url");
			SoapErrorMessage = Config.Setting("Service_SoapErrorMessage");
		}

		///<summary>
		///Implements IHttpModule.Init.  Hooks to Application_OnError event and allows
		///Queue to start operation.
		///</summary>
		///<param name="Application">The HttpApplication instance to which this module belongs.</param>
		public void Init(System.Web.HttpApplication Application) {
			Application.Error += new EventHandler(OnError);
			lock(typeof(LogModule)) Instances ++;
		}

		///<summary>
		///Implements IHttpModule.Dispose.  Allows for stopping Queue's thread when the last
		///instance of LogModule is released.
		///</summary>
		public void Dispose() {
			lock(typeof(LogModule)) Instances --;
			if (Instances==0) Queue.StopAll();
		}

		/// <summary>
		/// Error event handler that captures and logs if required any unhandled
		/// exceptions.
		/// </summary>
		private void OnError(object Source, EventArgs e) {
			System.Web.HttpApplication Application = (System.Web.HttpApplication)Source;
			Exception ex = Application.Server.GetLastError();
			while (ex!=null 
				&& (typeof(HttpUnhandledException).IsInstanceOfType(ex)
				|| typeof(HttpException).IsInstanceOfType(ex))
				&& !typeof(HttpCompileException).IsInstanceOfType(ex)
				&& !typeof(HttpParseException).IsInstanceOfType(ex))
				ex=ex.InnerException;
			if (ex!=null) {
				if (LogErrors && !Logger.IsIgnoreable(ex)) 
					try {
						Logger.Log(ex, true);
					} catch (Exception e1) {Application.Response.Write(e1.Message); Application.Response.End();}
				if (IsSoapServiceRequest(Application.Request)) {
					if (!typeof(SoapException).IsInstanceOfType(ex)) SoapFault(ex, Application);
				} else if (ErrorUrl!="") 
					Application.Response.Redirect(ErrorUrl);
			}
		}

		/// <summary>Returns true if the request appears to be a soap request.</summary>
		private bool IsSoapServiceRequest(HttpRequest Request) {
			//<soap:Envelope
			return (Request.Url.LocalPath.ToLower().EndsWith(".asmx") &&
				Request.HttpMethod == "POST" &&
				ReadStream(Request.InputStream).IndexOf("<soap:Envelope")>0);
		}

		/// <summary>Returns the contents of stream S as a string</summary>
		private string ReadStream(Stream S) {
			if (S==null) return "";
			long P = S.Position;
			S.Seek(0, SeekOrigin.Begin);
			string st = new StreamReader(S).ReadToEnd();
			S.Seek(P, SeekOrigin.Begin);
			return st;
		}

		/// <summary>
		/// Outputs e as a SOAP fault.
		/// </summary>
		private void SoapFault(Exception e, System.Web.HttpApplication Application) {
			string Result = "<?xml version=\"1.0\" encoding=\"utf-8\"?>\r\n<soap:Envelope xmlns:soap=\"http://schemas.xmlsoap.org/soap/envelope/\">\r\n  <soap:Body>\r\n    <soap:Fault>\r\n      <faultcode>soap:Server</faultcode>\r\n      <faultstring>";
			if (SoapErrorMessage=="") {
				Result += "System.Web.Services.Protocols.SoapException: Server was unable to process request. ---&gt; ";
				while (e!=null) {
					Result += Application.Server.HtmlEncode(e.GetType().FullName + " : " + e.Message + "\r\n" + e.StackTrace);
					Result += "\r\n   --- End of inner exception stack trace ---";
					e=e.InnerException;
				}
			} else Result += Application.Server.HtmlEncode(SoapErrorMessage);
			Result += "</faultstring>\r\n      <detail/>\r\n    </soap:Fault>\r\n  </soap:Body>\r\n</soap:Envelope>";
			Application.Server.ClearError();
			Application.Response.ContentType = "text/xml";
			Application.Response.Clear();
			Application.Response.Write(Result);
			Application.Response.End();
		}
	}
}
