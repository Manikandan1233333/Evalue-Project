
///// <Change Log>
//67811A0 - PCI Remediation for Payment systems CH1 : Arcsight logging
//67811A0 - PCI Remediation for Payment systems CH2: Added the if condition to control Arcsight logging. This can be disabled if web config entry is provided with any other values other than true by cognizant on 12/15/2011
//67811A0 - PCI Remediation for Payment systems CH3: Added to log the Arcsight fail event in the logfile without throwing the exception to the UI
//PCI - Post Product Issue fix CH1 - Added the below line to correct the timings in Arcsight logging
//PCI - Post Production Fix -CH2 Commented the below line and moved to Arcsightlog() method to correct the timings in Arcsight logging
//PC Security Defect Fix -CH1,CH2,CH3 - Modified the below code to add logging inside the catch block 

using System;
using System.Text.RegularExpressions;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Web;
using System.Collections;
using Microsoft.Win32;

using CSAAWeb.ArcSightReference;
using System.Net;
using System.Net.Sockets;
using System.Collections.Generic;
using System.Text;
using System.Reflection;



namespace CSAAWeb.AppLogger
{
    /// <summary>
    /// Contains class methods for logging messages.
    /// </summary>
    /// <remarks>
    /// <para>
    /// To use Logger, the module CSAAWeb.Web.LogModule must be installed within the web
    /// application.  See <see cref="Web.LogModule"/> for installation details.
    /// </para>
    /// <para>
    /// Path for logging must be in web.config as ApplicationLogPath.  If this value ends with "\", the 
    /// top level application namespace will be prepended to the file name.  If it is missing, then application 
    /// will attempt to log into the Application Event Log.  In order to allow this the following key must be 
    /// added to the system registry:
    /// </para>
    /// <code>
    ///Windows Registry Editor Version 5.00
    ///[HKEY_LOCAL_MACHINE\SYSTEM\CurrentControlSet\Services\Eventlog\Application\CSAAWeb AppLogger]
    ///"TypesSupported"=dword:00000007
    ///"EventMessageFile"="C:\\WINNT\\Microsoft.NET\\Framework\\v1.0.3705\\EventLogMessages.dll"
    /// </code>
    /// <para>
    /// However, if this entry hasn't been made Logger will instead try to log it in the application log as
    /// an ASP.NET event. 
    /// </para>
    /// <seealso cref="Web.LogModule"/>
    /// <seealso cref="Config.ExpandedSetting"/>
    /// <seealso cref="Queue"/>
    /// </remarks>
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
    ///&lt;/configuration&gt;
    /// </code>
    /// </example>
    public class Logger
    {

        ///<summary>The path to create log files.</summary>
        private static string _LogPath = null;
        private static Regex r1;
        private static Regex r2;
        private static Regex r3;

        ///<summary>The path to create log files.</summary>
        public static string LogPath
        {
            get
            {
                if (_LogPath == null)
                    if (Config.Initialized && Web.LogModule.Instances > 0)
                        _LogPath = Config.ExpandedSetting("ApplicationLogPath");
                    else return "";
                return _LogPath;
            }
        }
        //
        public static string GetIPAddress()
        {

            IPHostEntry host;
            string localIP = "?";
            host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            } return localIP;
        }
        public static bool Sourcereg(string p)
        {
            Regex SAddr = new Regex(@"\b(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\.(25[0-5]|2[0-4][0-9]|[01]?[0-9][0-9]?)\b");
            return SAddr.Match(p).Success;
        }

        static Logger()
        {
            r1 = new Regex("^\\s*at\\sSystem\\..*\n*", RegexOptions.Multiline | RegexOptions.Compiled);
            r2 = new Regex("^\\s*at\\s([^\\(]*).*", RegexOptions.Multiline | RegexOptions.Compiled);
            r3 = new Regex("\\n", RegexOptions.Multiline | RegexOptions.Compiled);
            //arcsightlog default mandatory values
            //PCI - Post Production Fix -CH2 Commented the below line and moved to Arcsightlog() method to correct the timings in Arcsight logging
            //Time = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff");


            DeviceTimeZone = CSAAWeb.Constants.PCI_ARC_DEVICETIMEZONE;
            DeviceVendor = CSAAWeb.Constants.PCI_ARC_DEVICEVENDOR;
            DeviceProduct = CSAAWeb.Constants.PCI_ARC_DEVICEPRODUCT;
            DeviceVersion = CSAAWeb.Constants.PCI_ARC_DEVICEVERSION;
            ApplicationProtocol = CSAAWeb.Constants.PCI_ARC_PROTOCOL;
            string strHostName = "";
            strHostName = System.Net.Dns.GetHostName();
            IPHostEntry ipEntry = System.Net.Dns.GetHostEntry(strHostName);
            IPAddress[] addr = ipEntry.AddressList;
            DestinationHostName = ipEntry.HostName;
            DestinationAddress = GetIPAddress();
            //SourceAddress = CSAAWeb.Constants.PCI_ARC_Load_Balancer_IP;
            HttpRequest Request = HttpContext.Current.Request;
            SourceAddress = Request.ServerVariables["REMOTE_ADDR"];
            if (Sourcereg(SourceAddress))
                SourceAddress = Request.ServerVariables["REMOTE_ADDR"];
            else
                SourceAddress = GetIPAddress();




        }

        /// <summary>
        /// Parses the stack trace to retrieve namespace and simple trace information.
        /// </summary>
        /// <param name="Service">Initially contains Environment.StackTrace, returns short trace string.</param>
        /// <param name="Process">Returns with Top level namespace.</param>
        private static void Trace(ref string Service, ref string Process)
        {
            if (Process == null)
            {
                Process = "";
                Service = "";
                return;
            }
            Process = r2.Replace(r1.Replace(Process, ""), "$1");
            if (Process == "")
            {
                Service = "";
                return;
            }
            if (Process.Substring(Process.Length - 1) == "\n")
                Process = Process.Substring(0, Process.Length - 1);
            while (Process.IndexOf("CSAAWeb.AppLogger.") == 0)
                Process = Process.Substring(Process.IndexOf("\n") + 1);
            int i = Process.LastIndexOf("\n") + 1;
            Service = Process.Substring(i, Process.IndexOf(".", i) + 1 - i);
            Process = Process.Replace(Service, "");
            Process = r3.Replace(Process, " <-- ");
            Service = Service.Substring(0, Service.Length - 1);
        }

        /// <summary>
        /// Gets the full name of the calling procedure.
        /// </summary>
        /// <returns>Calling procedure</returns>
        public static string Caller()
        {
            string Process = Environment.StackTrace;
            string Service = string.Empty;
            Trace(ref Service, ref Process);
            int i = Process.IndexOf("<--");
            if (i > 0) Process = Process.Substring(i + 4);
            i = Process.IndexOf("<--");
            if (i > 0) Process = Process.Substring(0, i - 1);
            //if (Process.IndexOf(Service)!=0) Process = Service + "." + Process;
            return Process;
        }

        /// <summary>
        /// Gets the top level application namespace.
        /// </summary>
        /// <returns>Application namespace</returns>
        public static string AppNameSpace()
        {
            string Process = Environment.StackTrace;
            string Service = string.Empty;
            Trace(ref Service, ref Process);
            return Service;
        }

        /// <summary>
        /// Logs Message to the currect log file, with stack trace information.
        /// </summary>
        /// <param name="Message">Message to log.</param>
        public static void Log(string Message)
        {
            /*string Process=Environment.StackTrace;
            string Service=string.Empty;
            Trace(ref Service, ref Process);
            LogApplicationMessage(LogPath, Service, Process, Message);
            if (Service=="") Service=Config.AppName;*/
            LogApplicationMessage(LogPath, Caller(), "", Message);
        }

        /// <summary>
        /// Logs Exception to the currect log file, with stack trace information.
        /// </summary>
        /// <param name="e">Exception to log.</param>
        public static void Log(Exception e)
        {
            if (e.Message == "Thread was being aborted.") return;
            if (e.InnerException != null) Log(e.InnerException, true);
            // Get trace info for both this call and e.  Merge them to get true
            // Process information.
            string Process1 = Environment.StackTrace;
            string Process2 = e.StackTrace;
            string Service1 = string.Empty;
            string Service2 = string.Empty;
            Trace(ref Service2, ref Process2);
            Trace(ref Service1, ref Process1);
            int i = Process2.LastIndexOf("<--");
            i = (i == -1) ? 0 : i + 4;
            string common = Process2.Substring(i);
            if (Service1 != Service2) common = Service2 + "." + common;
            i = Process1.IndexOf(common);
            if (i != -1)
            {
                i += common.Length;
                Process1 = Process2 + ((i < Process1.Length - 1) ? Process1.Substring(i) : "");
            }
            LogApplicationMessage(LogPath, Service1, Process1, e);
        }

        /// <summary>
        /// Logs Exception to the currect log file, with stack trace information.  If
        /// IgnoreCaller is true, only looks at exception stack trace.
        /// </summary>
        /// <param name="e">Exception to log.</param>
        /// <param name="IgnoreCaller">True if stack trace considered only from exception.</param>
        public static void Log(Exception e, bool IgnoreCaller)
        {
            if (!IgnoreCaller)
            {
                Log(e);
                return;
            }
            if (e.InnerException != null) Log(e.InnerException, true);
            string Process = e.StackTrace;
            string Service = string.Empty;
            Trace(ref Service, ref Process);
            LogApplicationMessage(LogPath, Service, Process, e);
        }

        private static bool TriedToRegister = false;
        /// <summary>
        /// Writes msg to the file specified.
        /// </summary>
        /// <param name="path">File path to create log file.</param>
        /// <param name="module">Name of module</param>
        /// <param name="location">Location within module</param>
        /// <param name="msg">Message to write.</param>
        public static void LogApplicationMessage(string path, string module, string location, string msg)
        {
            if (msg == null) return;
            // if path isn't blank, log to that path 
            if (path.Length > 0)
            {
                msg = "Source:" + module + ", " + ((location != "") ? (location + "\r\n   ") : "Message:") + msg;
                LogToFile(path, Config.AppName, msg);
                // otherwise check to see if logger is registered to the event log, and log there.
            }
            else
            {
                string EventSource = "CSAAWeb AppLogger";
                if (!EventLog.SourceExists(EventSource) && !TriedToRegister)
                {
                    TriedToRegister = true;
                    //This will attempt to register the event source, but will most likely
                    //fail due to permissions problems.
                    try
                    {
                        EventLog.CreateEventSource(EventSource, "Application");
                        /*
                        string Key = "SYSTEM\\CurrentControlSet\\Services\\Eventlog\\Application\\CSAAWeb AppLogger";
                        RegistryKey K = Registry.LocalMachine.OpenSubKey(Key, true);
                        if (K==null) K = Registry.LocalMachine.CreateSubKey(Key);
                        K.SetValue("EventMessageFile",HttpRuntime.ClrInstallDirectory + "\\EventLogMessages.dll");
                        long D = 7;
                        K.SetValue("TypesSupported",D);
                        K.Close();
                        */
                    }
                    //PC Security Defect Fix -CH1 -START Modified the below code to add logging inside the catch block 
                    catch { Logger.Log("Error occured while creating Event Source"); }
                    //PC Security Defect Fix -CH1 -END Modified the below code to add logging inside the catch block 
                }
                if (!EventLog.SourceExists(EventSource)) EventSource = "ASP.NET 1.0.3705.288";
                //TODO: Add Version 1.1 definition here!
                //if (!EventLog.SourceExists(EventSource)) EventSource = ""; 
                if (EventLog.SourceExists(EventSource))
                {
                    EventLogEntryType T = (msg.IndexOf("Exception=") == 0) ? EventLogEntryType.Error : EventLogEntryType.Information;
                    EventLog.WriteEntry(EventSource, "Source: " + module + ", " + location + msg, T);
                }
            }
        }

        /// <summary>
        /// Writes msg to path.  Creates a critical section to allow multiple threads accessing the file.
        /// </summary>
        /// <param name="path">The path where the file exists.</param>
        /// <param name="file">The base file name</param>
        /// <param name="msg">The message to write.</param>
        public static void LogToFile(string path, string file, string msg)
        {
            try
            {
                if (path.Substring(path.Length - 1, 1) == "\\") path += file;
                path += DateTime.Now.ToString("yyyyMMdd") + ".log";
                if (Web.LogModule.Instances > 0)
                    Queue.Enqueue(new LogRecord(path, msg));
            }
            //PC Security Defect Fix -CH2 -START Modified the below code to add logging inside the catch block 
            catch { Logger.Log("Error writing to file"); }
            //PC Security Defect Fix -CH2 -END Modified the below code to add logging inside the catch block 
        }

        /// <summary>
        /// Writes msg to file.
        /// </summary>
        public static void LogToFile(string file, string msg)
        {
            string path = LogPath;
            if (path.Substring(path.Length - 1, 1) != "\\") path = path.Substring(0, path.LastIndexOf("\\") + 1);
            LogToFile(path, file, msg);
        }

        /// <summary>
        /// Writes the exception information to the file specified.
        /// </summary>
        /// <param name="path">File path to create log file.</param>
        /// <param name="module">Name of module</param>
        /// <param name="location">Location within module</param>
        /// <param name="ex">The exception to log</param>
        public static void LogApplicationMessage(string path, string module, string location, Exception ex)
        {
            if (ex != null)
            {
                string Msg = ((ex.Message == null) ? "" : ("Exception=" + ex.Message.ToString())) +
                    ((ex.StackTrace == null) ? "" : ("\r\n" + ex.StackTrace.ToString()));
                LogApplicationMessage(path, module, location, Msg);
            }
        }

        /// <summary>
        /// Returns true if e is one of the types of exceptions that shouldn't be
        /// automatically logged.
        /// </summary>
        /// <param name="e"></param>
        /// <returns>Boolean true if e shouldn't be logged.</returns>
        public static bool IsIgnoreable(Exception e)
        {
            if (e == null) return true;
            if (typeof(BusinessRuleException).IsInstanceOfType(e)) return true;
            if (typeof(System.Data.SqlClient.SqlException).IsInstanceOfType(e) && ((System.Data.SqlClient.SqlException)e).Number == 50000) return true;
            return false;
        }

        /// <summary>
        /// //67811A0 - PCI Remediation for Payment systems CH1 : Added for Arcsight logging - Properties for Arcsight service
        /// 
        /// </summary>
        /// 

        #region Arcsight Logging Properties

        public static string Time { get; set; }

        public static string DeviceTimeZone { get; set; }

        public static string DeviceVendor { get; set; }

        public static string DeviceProduct { get; set; }

        public static string DeviceVersion { get; set; }

        public static string ApplicationProtocol { get; set; }

        public static string FullyQualifiedDomainSourceHostName { get; set; }

        //public static System.Net.IPAddress SourceAddress { get; set; }

        public static string SourceAddress { get; set; }
        //public static string SourceUserId { get; set; }

        public static string SourceUserName { get; set; }

        //public static string SourceUserPrivileges { get; set; }

        //public static string RequestClientApplication { get; set; }

        //public static string RequestMethod { get; set; }

        //public static string FullyQualifiedDomainHostName { get; set; }

        //public static string DeviceAddress { get; set; }

        public static string DestinationHostName { get; set; }

        public static string DestinationAddress { get; set; }

        public static string DestinationProcessName { get; set; }

        public static string FileName { get; set; }

        public static string DeviceAction { get; set; }

        // public static string EventOutcome { get; set; }

        public static string DeviceEventCategory { get; set; }

        //public static string RequestURL { get; set; }

        public static string Message { get; set; }

        public static string Name { get; set; }

        public static string DeviceSeverity { get; set; }

        public static string SourceProcessName { get; set; }

        //public static string DeviceProcessName { get; set; }

        //public static string DeviceFacility { get; set; }

        //public static string DestinationUserId { get; set; }

        //public static string DestinationUserName { get; set; }

        //public static string DestinationUserPrivileges { get; set; }

        //public static string RequestCookies { get; set; }

        #endregion

        /// <summary>
        /// //67811A0 - PCI Remediation for Payment systems CH1 : Added for Arcsight logging
        /// Invoke the RecordApplicationEventAsyncService and Log the details.
        /// </summary>
        /// 

        public static void ArcsightLog()
        {
            //67811A0 - PCI Remediation for Payment systems CH2: Added the if condition to control Arcsight logging. This can be disabled if web config entry is provided with any other values other than true. 
            if (Config.Setting("ArcsightReference.Enabled").ToUpper() == "TRUE")
            {
                //PCI - Post Product Issue fix CH1 - Added the below line to correct the timings in Arcsight logging
                Time = DateTime.Now.ToString("yyyy.MM.dd HH:mm:ss.fff");
                recordApplicationEventAsyncRequest LogRequest = new recordApplicationEventAsyncRequest();

                LogRequest.applicationContext = new ApplicationContext
                {
                    address = DestinationAddress,
                    application = CSAAWeb.Constants.PCI_ARC_APPLICATION_APPNAME,
                    subSystem = CSAAWeb.Constants.PCI_ARC_APPLICATION_SUBSYSTEM,
                    transactionType = CSAAWeb.Constants.PCI_ARC_APPLICATION_TRANSTYPE,
                    userId = CSAAWeb.Constants.PCI_ARC_APPLICATION_USERID,

                };
                LogRequest.requestorName = CSAAWeb.Constants.PCI_ARC_DEVICEPRODUCT;

                LogRequest.logLevel = "PCI";

                CEFEvent cef = new CEFEvent
                {

                    version = "0",
                    deviceVendor = DeviceVendor,
                    deviceProduct = DeviceProduct,
                    deviceVersion = DeviceVersion,
                    signatureId = CSAAWeb.Constants.PCI_EVENT,
                    severity = DeviceSeverity,
                    name = DeviceAction,

                    extension = FillExtension()

                };

                LogRequest.recordApplicationEventAsync_choice = new recordApplicationEventAsync_choice { @event = cef };



                RecordApplicationEventAsyncService LogService = new RecordApplicationEventAsyncService();

                try
                {
                    LogService.RecordApplicationEventAsync(LogRequest);
                }
                catch (Exception exp)
                {
                    //67811A0 - PCI Remediation for Payment systems CH3:Added to log the Arcsight fail event in the logfile without throwing the exception to the UI
                    //CHG0129017 - Added log to catch the exception - Start
                    Logger.Log("Calling SOA UTIL Service caused Exception:" + " " + exp.Message);
                    //CHG0129017 - Added log to catch the exception - End
                }

            }
        }

        /// <summary>
        /// Fill the key value pairs in extensions
        /// </summary>
        /// <returns></returns>
        private static ObjectProperty[] FillExtension()
        {
            char pipe = '|';
            StringBuilder extensionValue = new StringBuilder();
            extensionValue.Append(pipe + Time.ToString() + pipe + DeviceTimeZone + pipe + DeviceVendor + pipe + DeviceProduct + pipe + DeviceVersion + pipe +
                ApplicationProtocol + pipe + FullyQualifiedDomainSourceHostName + pipe + SourceAddress);
            extensionValue.Append(pipe + SourceUserName + pipe + SourceProcessName);
            extensionValue.Append(pipe + DestinationHostName + pipe + DestinationAddress + pipe + DestinationProcessName + pipe + FileName);
            extensionValue.Append(pipe + DeviceAction + pipe + DeviceEventCategory);
            extensionValue.Append(pipe + Name + pipe + pipe + pipe + pipe);


            List<ObjectProperty> extensions = new List<ObjectProperty>();
            extensions.Add(new ObjectProperty { name = "PCICEFValue", value = extensionValue.ToString() });

            return extensions.ToArray();
        }

    }

    /// <summary>
    /// Class used to queue file logging messages
    /// </summary>
    internal class LogRecord : QueueItem
    {
        /// <summary/>
        public LogRecord(string Path, string Message)
        {
            this.Path = Path;
            this.Message = DateTime.Now.ToString("T") + ": " + Message;
        }
        /// <summary/>
        private string Path;
        /// <summary/>
        private string Message;
        /// <summary/>
        public override void Run()
        {
            StreamWriter sw = null;
            try
            {
                sw = new StreamWriter(new FileStream(Path, FileMode.Append, FileAccess.Write));
                sw.WriteLine(Message);
                Status = QueuedResult.Complete;
            }
            finally
            {
                try { sw.Close(); }
                //PC Security Defect Fix -CH3 -START Modified the below code to add logging inside the catch block 
                catch { Logger.Log("Error writing to file"); }
                //PC Security Defect Fix -CH3 -END Modified the below code to add logging inside the catch block 
            }
        }
    }

}
