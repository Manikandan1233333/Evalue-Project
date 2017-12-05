/* 
 * HISTORY:
 * PT.Ch1 - Modified by COGNIANT on June 24 2009  - to log the  input XML and  output XML is written to SOAP log files with credit card field masked.
 * Defect5037:Ch1 Modified the Process stream method  to assign the fault string for Soap1.2 by cognizant on 02/01/2011
 * Defect5037:Ch2 added a new string regex which is cabaple of reading the text between soap XML tags for soap 1.2 by cognizant on 02/01/2011
 * Added the expression to mask cvv number and also to mask credit card and cvv number for process renewal web method input logging call by cognizant on 07/24/2011 as a part of August12,2011 releases by cognizant.
 * PC Security Defect Fix -CH1 - Modified the below code to add logging inside the catch block
 */
using System;
using System.Web;
using System.Web.Services;
using System.Web.Services.Protocols;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Reflection;
using CSAAWeb.Serializers;

namespace CSAAWeb.AppLogger 
{
	/// <summary>
	/// Soap extenstion allows for automatic logging of exceptions in web services.
	/// </summary>
	/// <remarks>
	/// To use this Soap Extension Filter, you must also install the CSAAWeb.Web.LogModule.
	/// See <see cref="Web.LogModule"/> for installation details.
	/// <seealso cref="Web.LogModule"/>
	/// <seealso cref="AppLogger.Logger"/>
	/// </remarks>
	/// <example>
	/// <para>Here is a typical web.config for using this extension:</para>
	/// <code>
	///&lt;?xml version="1.0" encoding="utf-8" ?&gt;
	///&lt;configuration&gt;
	///
	///   &lt;appSettings&gt;
	///      &lt;!-- Soap Logging --&gt;
	///      &lt;add key="Service_LogSoapCalls" value="true"/&gt;
	///      &lt;add key="Service_LogSoapComplexTypes" value="false"/&gt;
	///      &lt;add key="Service_LogSoapOutput" value="false"/&gt;
	///      &lt;add key="Service_LogSoapInput" value="false"/&gt;
	///      &lt;add key="Service_SoapFaultActor" value="false"/&gt;
	///   &lt;/appSettings&gt;
	///   
	///   &lt;system.web&gt;
	///	    &lt;webServices&gt;
	///	      &lt;soapExtensionTypes&gt;
	///	        &lt;add type="CSAAWeb.AppLogger.SoapLogger, csaaweb" priority="1" group="0" /&gt;
	///	      &lt;/soapExtensionTypes&gt;
	///	    &lt;/webServices&gt;
	///	  &lt;/system.web&gt;
	///	  
	///&lt;/configuration&gt;
	///	</code>
	/// </example>
	public class SoapLogger : SoapExtension 
	{
		/// <summary>
		/// If not blank will cause the exception detail and stack trace information to be replaced
		/// by its value on Soap calls.  This property can be set by the web.config key:
		/// Service_SoapErrorMessage
		/// </summary>
		public static string SoapErrorMessage;
		/// <summary>
		/// If true will cause all soap input to be logged.  Use this setting
		///	only for debugging.  This property can be set by the web.config key:
		///	Service_LogSoapInput.
		/// </summary>
		/// <remarks>
		/// WARNING: This will log any input data including passwords
		/// and credit card values as unencrypted text.  This setting should
		/// only be used during debugging and on non-production servers!
		/// </remarks>
		public static bool LogSoapInput;
		/// <summary>
		/// If true will cause all soap output to be logged.  Use this setting
		///	only for debugging.  This property can be set by the web.config key:
		///	Service_LogSoapOutput.
		/// </summary>
		public static bool LogSoapOutput;
		/// <summary>
		/// If true will cause every soap call to be logged along with its parameters.
		/// This property can be set by the web.config key: Service_LogSoapCalls.
		/// </summary>
		/// <remarks>
		/// If LogComplexTypes is false, complex parameters will be noted in the log as
		/// existing, but the actual value will not be stored, preserving the integrity
		/// of passwords and credit card numbers passed-in.  Simple parameters will be
		/// logged however.  The value of specific parameter name "password" (not 
		/// case-sensitive) will be masked and not logged, but any other simple parameters
		/// will be.  To preserve the security of credit card numbers when logging soap 
		/// messages, these should be buried in complex types that are not logged.
		/// </remarks>
		public static bool LogSoapCalls;
		/// <summary>
		/// If true, when logging soap calls, will serialize parameters of types SimpleSerializer 
		/// and ArrayOfSimpleSerializer, otherwise, will simply name the type.  Use this
		/// setting only for debugging.  This property can be set by the web.config key:
		/// Service_LogSoapComplexTypes
		/// </summary>
		/// <remarks>
		/// WARNING: This will log any input parameters stored in complex data types
		/// including passwords and credit card values as unencrypted text.  
		/// This setting should only be used during debugging and on non-production 
		/// servers!
		/// </remarks>
		public static bool LogSoapComplexTypes;
		/// <summary>
		/// If present, this value will be entered into the output stream as the Actor property for any 
		/// SOAP Fault.  This property can be set by the web.config key: Service_SoapFaultActor
		/// </summary>
		public static string SoapFaultActor;
		private Stream OldStream=null;
		private MemoryStream NewStream=null;
		private SoapMessageStage Stage = SoapMessageStage.BeforeDeserialize;
		private string CallLog = "";
		private bool ServerMessage = false;
		private string InputString = "";

		static SoapLogger() 
		{
			SoapErrorMessage = Config.Setting("Service_SoapErrorMessage");
			LogSoapInput = Config.bSetting("Service_LogSoapInput");
			LogSoapOutput = Config.bSetting("Service_LogSoapOutput");
			LogSoapCalls = Config.bSetting("Service_LogSoapCalls");
			LogSoapComplexTypes = Config.bSetting("Service_LogSoapComplexTypes");
			SoapFaultActor = Config.Setting("Service_SoapFaultActor");
			if (SoapFaultActor!="") SoapFaultActor = "<faultactor>" + SoapFaultActor + "</faultactor>      \r\n";
		}
		///<summary>Captures the stream if we are not passing through exception information.</summary>
		public override Stream ChainStream(Stream stream) 
		{
			if (!ServerMessage || SoapErrorMessage=="") return stream;
			OldStream = stream;
			NewStream = new MemoryStream();
			return NewStream;
		}

		///<summary>Required method does nothing in this class</summary>
		public override object GetInitializer(Type serviceType) 
		{
			return null;
		}
		///<summary>Required method does nothing in this class</summary>
		public override object GetInitializer(LogicalMethodInfo methodInfo,SoapExtensionAttribute attribute) 
		{
			return null;
		}
		///<summary>Required method does nothing in this class</summary>
		public override void Initialize(object initializer) {}

		/// <summary>
		/// When called with a message that contains an exception.  Logs the exception.
		/// </summary>
		public override void ProcessMessage(SoapMessage Message) 
		{
			Stage = Message.Stage;
			ServerMessage=typeof(SoapServerMessage).IsInstanceOfType(Message);
			if (!ServerMessage) return;
			switch (Stage) 
			{
				case SoapMessageStage.AfterSerialize: 
					if (!Logger.IsIgnoreable(Message.Exception)) Log(Message.Exception);
					if (CallLog=="") Log(Message);
					if (NewStream!=null) 
						ProcessStream(Message.Exception);
					else Log(Message.Exception==null);
					OldStream = null;
					NewStream = null;
					break;
				case SoapMessageStage.AfterDeserialize:
					if (LogSoapCalls) Log(Message);
					break;
				case SoapMessageStage.BeforeDeserialize:
					InputString = ReadStream(Message.Stream);
					// PT.Ch1 - Modified by COGNIANT - 6/24/09 - Added Mask.Replace to mask the CCNumber field (Credit Card number) if input XML is written to SOAP log files
					if (LogSoapInput) Log(Mask.Replace(InputString,"$1****"));
					break;
				default: break;
			}
		}
		static Regex R = new Regex("<faultstring>.*<\\/faultstring>",RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
		static Regex R1 = new Regex("<faultstring>",RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //Defect5037:Ch2 added a new string regex which is cabaple of reading the text between soap XML tags for soap 1.2 by cognizant on 02/01/2011
        static Regex R2 = new Regex("<soap:Reason>.*<\\/soap:Reason>", RegexOptions.Singleline | RegexOptions.Compiled | RegexOptions.IgnoreCase);
		/// <summary>
		/// If we are not passing exception information (Service_SoapErrorMessage!="" in web.config),
		/// this is required to copy the result to the actual output in the case of no exception,
		/// or to transform the exception faultstring to our default message if there was an exception.
		/// </summary>
		private void ProcessStream(Exception e) 
		{
			if (e!=null) 
			{
				NewStream.Seek(0, SeekOrigin.Begin);
				StreamWriter s = new StreamWriter(OldStream);
				string st = new StreamReader(NewStream).ReadToEnd();
				// PT.Ch1 - Modified by COGNIANT - 6/24/09 - Added Mask.Replace to mask the CCNumber field (Credit Card number) if output XML is written to SOAP log files
				if (LogSoapOutput) Logger.LogToFile("SoapLog","Failed soap message --> " + Mask.Replace(st,"$1****"));
				if (SoapErrorMessage!="")
                    //Defect5037:Ch1 start Modified the Process stream method  to assign the fault string for Soap1.2 by cognizant on 02/01/2011
                    if (st.IndexOf("<soap:Reason") > -1)
                        s.Write(R2.Replace(st, "<soap:Reason><soap:Text xml:lang=\"en\">" + SoapErrorMessage + "</soap:Text></soap:Reason>"));
                    else
                        //* Defect5037:Ch1 END Modified the Process stream method  to assign the fault string for Soap1.2 by cognizant on 02/01/2011
                        s.Write(R.Replace(st, SoapFaultActor + "<faultstring>" + SoapErrorMessage + "</faultstring>"));
				else 
					s.Write(R1.Replace(st, SoapFaultActor+"<faultstring>"));
				s.Flush();
				Log(false);
            } 
			else 
			{
				NewStream.WriteTo(OldStream);
				Log(true);
				if (LogSoapOutput) 
				{
					NewStream.Seek(0, SeekOrigin.Begin);
					// PT.Ch1 - Modified by COGNIANT - 6/24/09 - Added Mask.Replace to mask the CCNumber field (Credit Card number) if output XML is written to SOAP log files
					Logger.LogToFile("SoapLog","Successful soap message --> " + Mask.Replace(new StreamReader(NewStream).ReadToEnd(),"$1****"));
				}
			}
		}
		/// <summary>Logs the stream.</summary>
		private void Log(string st) 
		{
			Logger.LogToFile("SoapLog", st);
		}
		/// <summary>
		/// Reads and returns the contents of the stream, resetting it to its original position.
		/// </summary>
		private string ReadStream(Stream S) 
		{
			if (S==null) return "";
			long P = S.Position;
			S.Seek(0, SeekOrigin.Begin);
			string st = new StreamReader(S).ReadToEnd();
			S.Seek(P, SeekOrigin.Begin);
			return st;
		}
		/// <summary>
		/// Logs the current call.
		/// </summary>
		private void Log(bool Success) 
		{
			if (LogSoapCalls) 
			{
				// PT.Ch1 - Modified by COGNIANT - 6/24/09 - Added Mask.Replace to mask the CCNumber field (Credit Card number) if input XML is written to SOAP log files
				Logger.LogToFile("SoapLog", CallLog + ((Success)?" OK":(" Failed"+((LogSoapInput)?"":("\r\n"+Mask.Replace(InputString,"$1****"))))));
			}
		}
		/// <summary>
		/// This is called to log an exception.
		/// </summary>
		/// <param name="e"></param>
		private void Log(Exception e) 
		{
			if (typeof(SoapException).IsInstanceOfType(e))
				Logger.Log(e.InnerException, true);
			else Logger.Log(e, true);
		}

		/// <summary>Returns the beginning of the log message.</summary>
		private string LogHeader(SoapMessage Msg) 
		{
			HttpRequest Request = HttpContext.Current.Request;
			string st = Request.ServerVariables["REMOTE_ADDR"] + " " + Request.Path + " ";
			try {st += Msg.Action.Substring(Msg.Action.LastIndexOf("/")+1);}
            // PC Security Defect Fix -CH1 -START Modified the below code to add logging inside the catch block 
            catch (Exception ex) { Logger.Log(ex.ToString()); }
            // PC Security Defect Fix -CH1 -END Modified the below code to add logging inside the catch block 			
			return st;
		}

		/// <summary>
		/// This method logs soap method calls.
		/// </summary>
		/// <param name="Msg"></param>
		private void Log(SoapMessage Msg) 
		{
			CallLog = this.LogHeader(Msg) + "(";
			if (Msg.Stage==SoapMessageStage.AfterDeserialize) 
			{
				LogicalMethodInfo M = Msg.MethodInfo;
				int n = 0;
				foreach (ParameterInfo P in M.InParameters) 
				{
					CallLog += LogInParameter(P, Msg.GetInParameterValue(n), LogSoapComplexTypes) + ",";
					n++;
				}
				if (CallLog.Substring(CallLog.Length-1,1)==",") CallLog = CallLog.Substring(0, CallLog.Length-1);
			} 
			CallLog += ")";
		}
        // Added the expression to mask cvv number and also to mask credit card and cvv number for process renewal web method input logging call by cognizant on 07/24/2011 as a part of August12,2011 releases by cognizant. 

        private static Regex Mask = new Regex("(password=[\"]?|ccnumber=[\"]?|cccvnumber=[\"]?|<CCNumber>?|<CCCVNumber>?|CCNumber>[0-9]$?|CCCVNumber>[0-9]$?)[^,\" ><]*", RegexOptions.Compiled | RegexOptions.IgnoreCase);
		//private static Regex Mask = new Regex("(password=[\"]?|ccnumber=[\"]?)[^,\" ><]*",RegexOptions.Compiled | RegexOptions.IgnoreCase);
		/// <summary>
		/// Returns a string representing the input parameter P with its value.
		/// </summary>
		/// <param name="P">ParameterInfo for the parameter</param>
		/// <param name="Value">Value of the parameter</param>
		/// <param name="LogComplexTypes">True if complex types should have value logged.</param>
		/// <returns>String</returns>
		public static string LogInParameter(ParameterInfo P, object Value, bool LogComplexTypes) 
		{
			string st = P.Name + "=";
			if ((P.ParameterType.IsSubclassOf(typeof(SimpleSerializer)) ||
				P.ParameterType.IsSubclassOf(typeof(ArrayOfSimpleSerializer))) &&
				!LogComplexTypes) 
			{
				st += P.ParameterType.Name;
			} //else if (P.Name.ToLower()=="password") 
				//st += "*****";
			else 
			{
				try {st += (Value==null)?"null":Value.ToString();}
				catch {st += P.ParameterType.Name;} 
			}
			return Mask.Replace(st,"$1****");
		}

	}


	/// <summary>
	/// This attribute is applied to a web service method to cause automatic
	/// logging of any unhandled exception.
	/// </summary>
	[AttributeUsage(AttributeTargets.Method)]
	public class LogExtensionAttribute : SoapExtensionAttribute 
	{

		private int priority;

		///<summary/>
		public override Type ExtensionType 
		{
			get { return typeof(SoapLogger); }
		}

		///<summary/>
		public override int Priority 
		{
			get { return priority; }
			set { priority = value; }
		}

	}


}
