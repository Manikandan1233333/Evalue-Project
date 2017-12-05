using System;
using System.Web;
using CSAAWeb.AppLogger;
using System.Web.Services.Protocols;
using System.Collections;

namespace CSAAWeb.Web
{
	/// <summary>Module allows for forcing https and requiring client certificates.</summary>
	/// 
	/// <remarks>
	/// <para>
	/// HttpsModule must be installed within an application through web.config, in the httpModules
	/// section of the system.web configuration section.  
	/// The ForceSecure settings in the appSettions section that controls operation.  See
	/// the example web.config file below.  See <see cref="Web.RequireHttps"/> for the possible
	/// values for ForceSecure.
	/// The RequireCertificate setting in the appSettins section controls when a client certificate
	/// is required.
	/// The Certificates setting is a comma-delimited list of certificate serial numbers that will be accepted.
	/// </para>
	/// <seealso cref="Web.RequireHttps"/>
	/// </remarks>
	/// 
	/// <example>
	/// <para>Here is a sample web.config file for using HttpsModule:</para>
	/// <code>
	///&lt;?xml version="1.0" encoding="utf-8" ?&gt;
	///&lt;configuration&gt;
	///
	///    &lt;appSettings&gt;
	///        &lt;!-- https --&gt;
	///        &lt;add key="ForceSecure" value="Remote"/&gt;
	///        &lt;add key="RequireCertificate" value="RemoteService"/&gt;
	///        &lt;add key="Certificates" value=""/&gt;
	///    &lt;/appSettings&gt;
	///    
	///    &lt;system.web&gt;
	///        &lt;!--Install HttpsModule with the following tag:--&gt;
	///
	///        &lt;httpModules&gt;
	///            &lt;add name="HttpsModule" type="CSAAWeb.Web.HttpsModule, CSAAWeb"/&gt;
	///        &lt;/httpModules&gt;
	///
	///     &lt;/system.web&gt;
	///
	///&lt;/configuration&gt;
	///</code>
	///</example>
	public class HttpsModule : IHttpModule {
		private static ArrayList _Certificates = null;
		///<summary>Setting specifies when to require a certificate.</summary>
		public static ArrayList Certificates {get{return _Certificates;}}
		private static RequireHttps _RequireCertificate = RequireHttps.None;
		///<summary>Setting specifies when to require a certificate.</summary>
		public static RequireHttps RequireCertificate {get{return _RequireCertificate;}}
		private static RequireHttps _ForceSecure = RequireHttps.None;
		///<summary>Setting specifies when to force https mode.</summary>
		public static RequireHttps ForceSecure {get{return _ForceSecure;}}
		///<summary>Default Constructor for HttpsModule</summary>
		public HttpsModule() : base() {}
		///<summary>Constructs instance of HttpsModule and calls Init.</summary>
		public HttpsModule(System.Web.HttpApplication Application) : base() {
			Init(Application);
		}

		///<summary>Implements IHttpModule.Init.  Hooks to Application_OnBeginRequest.</summary>
		public void Init(System.Web.HttpApplication Application) {
			if (ForceSecure!=RequireHttps.None || RequireCertificate!=RequireHttps.None)
				Application.BeginRequest+=new EventHandler(OnBeginRequest);
		}

		/// <summary>
		/// Static constructor
		/// </summary>
		static HttpsModule() {
			_ForceSecure = 
				(Config.Setting("ForceSecure")=="")?RequireHttps.RemoteService:
				(RequireHttps)Enum.Parse(typeof(RequireHttps), Config.Setting("ForceSecure"), true);
			_RequireCertificate = 
				(Config.Setting("RequireCertificate")=="")?RequireHttps.None:
				(RequireHttps)Enum.Parse(typeof(RequireHttps), Config.Setting("RequireCertificate"), true);
			_Certificates = Config.SettingArray("Certificates");
		}
		
		///<summary>Implements IHttpModule.Dispose.</summary>
		public void Dispose() {
		}

		/// <summary>
		/// BeginRequest event handler checks to insure that the call is
		/// using https unless its localhost.
		/// </summary>
		private void OnBeginRequest(object Source, EventArgs e)
		{
			// Try catch added by Cognizant on 01/21/2005 asper onsite request
			try
			{

				System.Web.HttpApplication Application = (System.Web.HttpApplication)Source;
				bool IsService = Application.Request.Url.LocalPath.ToLower().EndsWith(".asmx");
				bool IsRemote = Application.Request.ServerVariables["LOCAL_ADDR"]!=Application.Request.ServerVariables["REMOTE_ADDR"];
				if (!(Application.Request.IsSecureConnection || ForceSecure==RequireHttps.None)) 
				{
					switch (ForceSecure) 
					{
						case RequireHttps.All: 
							if (IsService) throw new CSAAWeb.BusinessRuleException("Must use secure connection.");
							else Redirect(Application);
							break;
						case RequireHttps.AllService: 
							if (IsService) throw new CSAAWeb.BusinessRuleException("Must use secure connection.");
							break;
						case RequireHttps.Remote: 
							if (IsRemote) 
								if (IsService) throw new CSAAWeb.BusinessRuleException("Must use secure connection except on local machine.");
								else Redirect(Application);
							break;
						case RequireHttps.RemoteService: 
							if (IsRemote && IsService) throw new CSAAWeb.BusinessRuleException("Must use secure connection except on local machine.");
							break;
					}
				}
				if (RequireCertificate!=RequireHttps.None) 
				{
					HttpClientCertificate Cert = Application.Context.Request.ClientCertificate;
					switch (RequireCertificate) 
					{
						case RequireHttps.All: 
							CheckCertificate(Cert); 
							break;
						case RequireHttps.AllService: 
							if (IsService) CheckCertificate(Cert);
							break;
						case RequireHttps.Remote: 
							if (IsRemote) CheckCertificate(Cert);
							break;
						case RequireHttps.RemoteService: 
							if (IsRemote && IsService) CheckCertificate(Cert);
							break;
					}
				}
			}
			catch(CSAAWeb.BusinessRuleException ex)
			{
				CSAAWeb.AppLogger.Logger.Log(ex);
				throw ex;
			}
		}

		private void CheckCertificate(HttpClientCertificate Cert) {
			if (!Cert.IsPresent) throw new CSAAWeb.BusinessRuleException("Certificate must be provided.");
			if (!Cert.IsValid) throw new CSAAWeb.BusinessRuleException("Certificate is not valid.");
			if (Cert.ValidUntil.CompareTo(DateTime.Now)<0) throw new CSAAWeb.BusinessRuleException("Certificate has expired.");
			//if (Certificates.Contains(Cert.SerialNumber)) throw new CSAAWeb.BusinessRuleException("Certificate is not installed.");
			//Changed by cognizant on Feb 10 2005 as onsite suggestion
			  if (!Certificates.Contains(Cert.SerialNumber))throw new CSAAWeb.BusinessRuleException("Certificate is not installed.");
		}

		/// <summary>Redirects the request to a secure connection.</summary>
		private void Redirect(System.Web.HttpApplication Application) {
			Application.Response.Redirect(Application.Request.Url.AbsoluteUri.Replace("http:", "https:"), true);
		}
	}

	///<summary>Specifies when Https is required for the application.</summary>
	public enum RequireHttps {
		///<summary>Require Https for all requests</summary>
		All,
		///<summary>Require Https for all service requests.</summary>
		AllService,
		///<summary>Require Https only for requests not from same computer.</summary>
		Remote,
		///<summary>Require Https only for service requests not from same computer.</summary>
		RemoteService,
		///<summary>Don't require Https for any request.  (Default)</summary>
		None
	}

}
