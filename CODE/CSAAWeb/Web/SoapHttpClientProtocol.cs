/*HISTORY
 * PC Security Defect Fix -CH1 - Modified the below code to add logging inside the catch block
 * 

*/
using System;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Web.Services.Protocols;
using System.Web.Services;
using CSAAWeb.Serializers;
using CSAAWeb.AppLogger;
using System.Collections.Specialized;
using System.Collections;
using System.Net;
using System.Reflection;

namespace CSAAWeb.Web {
	/// <summary>
	/// Base class for Soap clients that has improved exception handling and
	/// more automatic method invokation.  Will also try to make call locally if
	/// possible rather than through web service if the service is located in the
	/// same directory and assembly.
	/// </summary>
	/// <remarks>
	/// <para>Action is determined by several entries in appSettings config section.</para>
	/// <code>
	/// &lt;?xml version="1.0" encoding="utf-8" ?&gt;
	///	&lt;configuration&gt;
	///	    &lt;appSettings&gt;
	///	        &lt;!--This is the default credential used for all webservice based on this type--&gt;
	///	        &lt;add key="WebService.Credentials" value="userid,password"/&gt;
	///	        &lt;!--If true, the default credential is encrypted using Constants.CS_STRING--&gt;
	///	        &lt;add key="WebService.EncryptedCredentials" value="false"/&gt;
	///	        &lt;!--The default setting for whether internal web service invoke should be logged.--&gt;
	///	        &lt;add key="WebService.LogInternalInvoke" value="false"/&gt;
	///	        &lt;!--The Url for a specific web service--&gt;
	///	        &lt;add key="WebService.[Type name]" value="http://WebserviceUrl"/&gt;
	///	        &lt;!--The credential for a specific web service.--&gt;
	///	        &lt;add key="WebService.[Type name].Credentials" value="userid,password"/&gt;
	///	        &lt;!--True if the credential for the service is encrypted using Constants.CS_STRING--&gt;
	///	        &lt;add key="WebService.[Type name].EncryptedCredentials" value="false"/&gt;
	///	        &lt;!--True if internal invoke calls should be logged.--&gt;
	///	        &lt;add key="WebService.[Type name].LogInternalInvoke" value="false"/&gt;
	///	    &lt;/appSettings&gt;
	/// &lt;/configuration&gt;
	/// </code>
	/// 
	/// </remarks>
	[System.Diagnostics.DebuggerStepThroughAttribute()]
	[System.ComponentModel.DesignerCategoryAttribute("code")]
	[SoapDocumentServiceAttribute(Use=System.Web.Services.Description.SoapBindingUse.Literal, ParameterStyle=SoapParameterStyle.Wrapped)]
	[XmlIncludeAttribute(typeof(SimpleSerializer))]
	[WebServiceBindingAttribute(Namespace="http://csaa.com/webservices/")]
	public class SoapHttpClientProtocol : System.Web.Services.Protocols.SoapHttpClientProtocol, IClosableWeb {
		private static Regex r1;
		private static Regex r2; 
		private static Regex ClassR;
		private static ListDictionary UrlCache;
		private WebService LocalService = null;
		private bool LogInternalInvoke = false;

		/// <summary>
		/// Static constructor get the default credential information.
		/// </summary>
		static SoapHttpClientProtocol() {
			r1 = new Regex("^\\s*at\\sSystem\\..*\n*", RegexOptions.Multiline|RegexOptions.Compiled);
			r2 = new Regex("^\\s*at\\s([^\\(]*).*", RegexOptions.Multiline|RegexOptions.Compiled);
			ClassR = new Regex("\\.[^.]*\\.", RegexOptions.Compiled);
			UrlCache = new ListDictionary();
			UrlCache["DefaultCredential"] = new UrlInfo("DefaultCredential", GetCredential(""));
		}
		
		/// <summary>
		/// The AssemblyQualifiedName of the underlying web service.  Used to attempt to 
		/// instantiate the service directly if no Url is provided.  This is generated
		/// automatically, but can be overridden in derived classes if a different formulation
		/// is required.
		/// </summary>
		protected virtual string ServiceClass {
			get {return ClassR.Replace(GetType().AssemblyQualifiedName, ".WebService.", 1);}
		}
		
		/// <summary>
		/// Looks for the class in web.config and creates an entry in UrlCache for it.
		/// </summary>
		/// <param name="T"></param>
		private void GetUrlInfo(Type T) {
			LogInternalInvoke = Config.bSetting("WebService.LogInternalInvoke");
			string st = Config.Setting("WebService."+T.Name+".LogInternalInvoke");
			if (st!="") LogInternalInvoke = Config.bSetting("WebService."+T.Name+".LogInternalInvoke");
			st = Config.Setting("WebService."+T.Name);
			if (st==string.Empty) {
				try {
					ConstructorInfo C = Type.GetType(ServiceClass, true, true).GetConstructor(new Type[] {});
					if (C==null) {
						throw new Exception("Could not construct web service.");
					}
					lock (UrlCache) UrlCache[T.Name] = new UrlInfo(C, LogInternalInvoke);
				} catch (Exception e) {
					AppLogger.Logger.Log(e);
					throw new Exception("No WebService Uri found in web.config for " + T.Name);
				}
			} else {
				lock (UrlCache) UrlCache[T.Name] = new UrlInfo(st, GetCredential(T.Name+"."));
			}
		}

		/// <summary>
		/// Looks for the class credential information in web.config and creates the credential
		/// if found.  Returns the created credential.  If not found, and a default exists,
		/// returns the default instead.
		/// </summary>
		private static ICredentials GetCredential(string Class) {
			ICredentials C=CredentialCache.DefaultCredentials;
			ArrayList A = Config.SettingArray("WebService."+Class+"Credentials",
				((Config.bSetting("WebService."+Class+".EncryptedCredentials"))?Constants.CS_STRING:""));
			if (A.Count==2) C = new NetworkCredential((string)A[0], (string)A[1]);
			if (A.Count==3) C = new NetworkCredential((string)A[0], (string)A[1], (string)A[2]);
			if (Class!="" && C==null && A.Count==0) C=((UrlInfo)UrlCache["DefaultCredential"]).Credential;
			return C;
		}

		/// <summary>
		/// Constructor gets the Service name and credentials from the config file.
		/// </summary>
		public SoapHttpClientProtocol() : base() {
			ClosableModule.SetHandler(this);
			Type T = this.GetType();
			if (UrlCache[T.Name]==null) GetUrlInfo(T);
			Initialize();
		}

		private void Initialize() {
			UrlInfo U = (UrlInfo)UrlCache[GetType().Name];
			if (U.Url!="") {
				this.Url = U.Url;
				this.Credentials = U.Credential;
			} else {
				try {
					LocalService = (WebService)U.Local.Invoke(new object[] {});
					LogInternalInvoke = U.LogInternalInvoke;
				} catch (Exception e) {throw new Exception(e.InnerException.Message, e.InnerException);}
			}
		}

		private bool InCall = false;
		private bool CloseCalled = false;
		/// <summary>Closes any open connections.</summary>
		public void Close() {
			if (LocalService!=null) {
				if (!InCall) {
					if (typeof(IClosableWeb).IsInstanceOfType(LocalService))
						((IClosableWeb)LocalService).Close();
					LocalService=null;
				} else CloseCalled = true;
			}
			ClosableModule.RemoveHandler(this);
		}

		/// <summary>
		/// Calls Invoke.
		/// </summary>
		protected object[] Invoke() {
			return Invoke(Caller(), new object[] {});
		}

		
		/// <summary>
		/// Invokes the web service method.
		/// </summary>
		/// <param name="Parameters">Parameters to pass to service.</param>
		protected object[] Invoke(object[] Parameters) {
			return Invoke(Parameters, false);
		}

		/// <summary>
		/// Invokes the web service method.
		/// </summary>
		/// <param name="MethodName">Method to invoke.</param>
		/// <param name="Parameters">Parameters to pass to service.</param>
		protected new object[] Invoke(string MethodName, object[] Parameters) {
			return Invoke(MethodName, Parameters, false);
		}

		/// <summary>
		/// Invokes the web service method.
		/// </summary>
		/// <param name="Parameters">Parameters to pass to service.</param>
		/// <param name="SkipLog">True if internal logging should be omitted.</param>
		protected object[] Invoke(object[] Parameters, bool SkipLog) {
			return Invoke(Caller(), Parameters, SkipLog);
		}

		/// <summary>
		/// Invokes the web service method.
		/// </summary>
		/// <param name="MethodName">Method to invoke.</param>
		/// <param name="Parameters">Parameters to pass to service.</param>
		/// <param name="SkipLog">True if internal logging should be omitted.</param>
		protected object[] Invoke(string MethodName, object[] Parameters, bool SkipLog) {
			if (LocalService==null && Url=="") Initialize();
			if (LocalService!=null) {
				return InternalInvoke(MethodName, Parameters, SkipLog);
			} else 
				try {return base.Invoke(MethodName, Parameters);}
				catch (SoapException e) {throw HandleSoapException(e);}
		}

		/// <summary>
		/// Invokes the method through a direct instantiation of the WebService object
		/// then calling the method through reflection.
		/// </summary>
		private object[] InternalInvoke(string MethodName, object[] Parameters, bool SkipLog) {
			MethodInfo M = LocalService.GetType().GetMethod(MethodName, BindingFlags.Instance | BindingFlags.Public );//| BindingFlags.ExactBinding, null, Types, null);
			if (M==null) throw new Exception("Method not found: " + MethodName);
			InCall=true;
			try {
				ParameterInfo[] P = M.GetParameters();
				bool HasOutput = HasOutputParameters(P);
				Parameters = FixInputParameters(P, Parameters, HasOutput);
				object[] Result = FixOutputParameters(M.Invoke(LocalService, Parameters), P, Parameters, HasOutput);
				if (LogInternalInvoke && !SkipLog) Log(M, Parameters, true);
				InCall=false;
				if (CloseCalled) Close();
				return Result;
			} catch (Exception e) {
				Log(M, Parameters, false);
				InCall=false;
				if (CloseCalled) Close();
				if (e.InnerException==null) throw;
				e=e.InnerException;
				while (e.InnerException!=null && e.InnerException.Message==
						"Exception has been thrown by the target of an invocation.")
					e=e.InnerException;
				throw e;
			}
		}

		private void LogTest(int n) {
			if (LogInternalInvoke) AppLogger.Logger.Log(n.ToString() + ") LocalService ("+GetType().Name+") is null? " + (LocalService==null).ToString());
		}

		/// <summary>
		/// Extracts the original exception from a soap call, so that the calling code
		/// does not see the exception as a soap exception.
		/// </summary>
		private SoapException HandleSoapException(SoapException e) {
			try {
				string st = e.Message;
				int i = st.IndexOf("--->")+5;
				int j = st.IndexOf(":",i)-1;
				string ExceptionName = st.Substring(i,j-i);
				i = j + 3;
				j = st.IndexOf("\n", i) - i;
				st = st.Substring(i,j);
				Exception e1;
				try {e1 = (Exception)Type.GetType(ExceptionName).GetConstructor(new Type[] {typeof(string)}).Invoke(new object[] {st});}
				catch {e1 = new Exception(st);}
				e = new System.Web.Services.Protocols.SoapException(e.Message.Replace("\n","\r\n").Replace("--->","\r\n   --->"), e.Code, e.Actor, e.Detail, e1);
			}
            // PC Security Defect Fix -CH1 -START Modified the below code to add logging inside the catch block 
            catch (Exception ex) { Logger.Log(ex.ToString()); }
            // PC Security Defect Fix -CH1 -END Modified the below code to add logging inside the catch block 			
			return e;
		}

		/// <summary>
		/// Gets the name of the calling method.
		/// </summary>
		private string Caller() {
			string Process=Environment.StackTrace;
			Process = r2.Replace(r1.Replace(Process,""),"$1");
			if (Process.Substring(Process.Length-1)=="\n")
				Process = Process.Substring(0, Process.Length-1);
			while (Process.IndexOf("CSAAWeb.Web.Soap")==0)
				Process = Process.Substring(Process.IndexOf("\n")+1);
			int i = Process.IndexOf("\n");
			if (i>0) Process = Process.Substring(0,i);
			i = Process.LastIndexOf(".");
			if (i>0) Process = Process.Substring(i+1);
			return Process;
		}

		/// <summary>
		/// This method logs web service method calls that have been called through
		/// internal invoke rather than Url calls.
		/// </summary>
		/// <param name="M">MethodInfo object for the method being called</param>
		/// <param name="Parameters">The actual parameters being passed.</param>
		/// <param name="Success">True if the call succeeded, false if failed.</param>
		private void Log(MethodInfo M, object[] Parameters, bool Success) {
			bool LogComplexTypes = (SoapLogger.LogSoapComplexTypes || !Success || SoapLogger.LogSoapInput);
			int i = 0;
			string st = LocalService.GetType().FullName + " " + M.Name + "(";
			foreach (ParameterInfo P in M.GetParameters()) {
				st+= ((P.IsOut && !P.IsIn)?
					"out " + P.Name:
					SoapLogger.LogInParameter(P, Parameters[i], LogComplexTypes))
					+ ",";
				i++;
			}
			if (st.Substring(st.Length-1,1)==",") st = st.Substring(0, st.Length-1);
			st += ")" + ((Success)?" OK":" Failed");
			Logger.LogToFile("SoapLog", st);
		}

		private bool HasOutputParameters(ParameterInfo[] M) {
			foreach (ParameterInfo P in M) if (!P.IsIn) return true;
			return false;
		}

		private object[] FixInputParameters(ParameterInfo[] M, object[] Parameters, bool HasOutput) {
			if (!HasOutput) return Parameters;
			object[] Result = new object[M.Length];
			int i=0;
			foreach (ParameterInfo P in M) {
				if (!P.IsOut || P.IsIn) {
					Result[P.Position] = Parameters[i];
					i++;
				}
			}
			return Result;
		}

		private object[] FixOutputParameters(object Result, ParameterInfo[] M, object[] Parameters, bool HasOutput) {
			if (!HasOutput) return new object[] {Result};
			ArrayList Output = new ArrayList();
			Output.Add(Result);
			foreach (ParameterInfo P in M)
				if (P.IsOut) Output.Add(Parameters[P.Position]);
			return Output.ToArray();
		}
	}



	/// <summary>
	/// Class used to save Urls and credential for connecting to web services.
	/// </summary>
	public class UrlInfo {
		///<summary/>
		public string Url = string.Empty;
		///<summary/>
		public ICredentials Credential;
		///<summary/>
		public bool LogInternalInvoke = false;
		///<summary/>
		public ConstructorInfo Local = null;
		///<summary/>
		public UrlInfo(string url, ICredentials credential) {
			this.Url = url;
			this.Credential = credential;
		}
		///<summary/>
		public UrlInfo(ConstructorInfo C, bool LogInternalInvoke) {
			Local = C;
			this.LogInternalInvoke = LogInternalInvoke;
		}
	}
}
