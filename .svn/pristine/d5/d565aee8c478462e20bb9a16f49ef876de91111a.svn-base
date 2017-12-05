/*History
 * 
 *  PC Security Defect Fix -CH1,CH2, CH3 - Modified the below code to add logging inside the catch block 

*/
using System;
using System.Configuration;
using System.Collections;
using System.Reflection;
using System.Collections.Specialized;
using System.Xml;
using System.Text.RegularExpressions;
using System.Web;
using System.IO;
using CSAAWeb.AppLogger;
namespace CSAAWeb
{
	/// <summary>
	/// Contains class functions for retrieving configuration data.
	/// </summary>
	/// <remarks>
	/// <para>
	/// This class should be used for retrieving all configuration data.  Methods are
	/// provided for retreiving ConfigurationSetting.AppSetting values as strings boolean
	/// and integers that provide default values if the key isn't present ("", false and 0 respectively).
	/// ExpandedSetting is a method that retrieves strings with value that need to be
	/// based on specific paths or names relative to the current web application.
	/// </para>
	/// <para>
	/// This class also allows values to be placed in a file, server.config, that is at
	/// the root of the server, that are independent of the normal ASP.NET configuration
	/// path.  This is particularly useful for server-dependent values such as database
	/// connection strings.  This allows application settings in web.config to be copied
	/// along with application files between different environments safely without overwriting
	/// values that are server-dependent.  Values that are in server.config will be retrieved
	/// by calls to Config.Setting (or other methods) seemlessly as if they were in web.config.
	/// Following the standard .NET configuration methodology, values in web.config will
	/// override identical values in server.config.
	/// </para>
	/// <para>
	/// In addition to the &lt;add key="name" value="value"/&gt; syntax used in web.config
	/// server.config has additional syntax options for database connections and web services
	/// to be compatible with the original design from Congnizant for this file.  Additionally,
	/// the root element name is ignored, and all appSetting values are contained within
	/// an "Application" tag within an "Environment" tag.  The selected "Environment" tag
	/// is determined by the "DeployedEnvironment tag, which may either specify a value or
	/// be blank, forcing the selection of the Enviromnent tag matching the MachineName property
	/// of the running web server.
	/// </para>
	/// </remarks>
	/// <example>
	/// <para>
	/// Here is a sample server.config file:
	/// </para>
	/// <code>
	///&lt;?xml version="1.0" encoding="utf-8" ?&gt;
	///&lt;ConnectionStrings&gt;
	///    &lt;DeployedEnvironment&gt;DEVELOPMENT&lt;/DeployedEnvironment&gt;
	///    &lt;Environment Name="DEVELOPMENT"&gt;
	///        &lt;General"&gt;
	///	           &lt;!--Add settings for all applications here--&gt;
	///            &lt;!--add key="Error_Url" value=" /forms/general_error.aspx"/--&gt;
	///        &lt;/General&gt;
	///
	///        &lt;Application Name="Application1"&gt;
	///            &lt;!-- Web services --&gt;
	///            &lt;!-- Normal syntax --&gt;
	///            &lt;add key="WebService.Orders1" value="http://localhost/services/order1.asmx" /&gt;
	///            &lt;add key="WebService.Orders1.Credentials" value="User,Password" /&gt;
	///            &lt;add key="WebService.Orders1.EncryptedCredentials" value="false" /&gt;
	///            &lt;!-- Alternate syntax --&gt;
	///            &lt;WebService Service="WebService.Order2" Url="http://localhost/services/order2.asmx" Credentials="User,Password" EncryptedCredentials="false"/&gt;
	///            &lt;!--add key="Error_Url" value=" /forms/general_error.aspx"/--&gt;
	///        &lt;/Application&gt;
	///
	///        &lt;Application Name="Services"&gt;
	///            &lt;!--Connection strings--&gt;
	///            &lt;!-- Normal syntax --&gt;
	///            &lt;add key="ConnectionString.Orders1" value="server=localhost;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Orders1;"/&gt;
	///            &lt;add key="ConnectionsString.Orders1.Encrypted" value="false"/&gt;
	///            &lt;!-- Alternate syntax --&gt;
	///            &lt;Connection Name=" Orders2" value="server=localhost;Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=Orders2;" encrypted="false"/&gt;
	///        &lt;/Application&gt;
	///    &lt;/Environment&gt;
	///&lt;/ConnectionStrings&gt;
	/// </code>
	/// </example>
	public class Config
	{
		#region Application Properties
		///<summary>The "/" terminated relative Url of the application's root folder. (lowercase)</summary>
		public static string SiteRoot = "";
		///<summary>The "\" terminated directory path of the application's root folder. (lowercase)</summary>
		public static string SiteRootPath = "";
		///<summary>The "\" terminated directory path of the server's root folder. (lowercase)</summary>
		public static string RootPath = "";
		///<summary>The simple friendly name of this web application. (Propercase)</summary>
		public static string AppName = "";
		///<summary>True if the initialization of this component is done.</summary>
		internal static bool Initialized = false;
		static Config() {
			try {
				SiteRoot = HttpRuntime.AppDomainAppVirtualPath.ToLower();
				if (SiteRoot.Length>1) SiteRoot += "/";
				SiteRootPath = HttpRuntime.AppDomainAppPath.ToLower();
				RootPath = SiteRootPath;
				AppName = SiteRoot;
				if (SiteRoot.Length>1) {
					if (HttpRuntime.IsOnUNCShare) throw new Exception("Can not determine location of server root path.");
					int Depth = new Regex("[^/]*").Replace(SiteRoot,"").Length;
					for (int i=1; i<Depth; i++) RootPath = RootPath.Substring(0, RootPath.LastIndexOf("\\", RootPath.Length-2)+1);
					for (int i=2; i<Depth; i++) AppName = AppName.Substring(AppName.IndexOf("/",1));
					AppName = AppName.Replace("/","");
					AppName = AppName.Substring(0,1).ToUpper() + AppName.Substring(1);
				} else AppName = "IISRoot";
			}
            // PC Security Defect Fix -CH3 -START Modified the below code to add logging inside the catch block 
            catch (Exception ex) { Logger.Log(ex.ToString()); }
            // PC Security Defect Fix -CH3 -END Modified the below code to add logging inside the catch block 
			
			Init();
			Initialized = true;
		}
		#endregion
		#region Setting
		
		/// <summary>
		/// Function for returning a configuration string, expanding %SiteRoot%, %SiteRootPath%, %RootPath% and %AppName%.
		/// </summary>
		/// <param name="Key">The configuration key to get</param>
		/// <returns>the value of key.</returns>
		public static string ExpandedSetting(string Key) {
			string result=Setting(Key);
			result = result
				.Replace("%SiteRoot%", SiteRoot)
				.Replace("%SiteRootPath%", SiteRootPath)
				.Replace("%RootPath%", RootPath)
				.Replace("%AppName%", AppName);
			return result;
		}

		/// <summary>
		/// Function for returning a configuration string.
		/// </summary>
		/// <param name="Key">The configuration key to get</param>
		/// <returns>the value of key.</returns>
		/// <remarks>
		/// Checks web.config first, then if not found, checks server.config if it exists.
		/// Always returns a string; will be "" if key is not found.
		/// </remarks>
		public static string Setting(string Key) 
		{
			string result="";
			result = ConfigurationSettings.AppSettings[Key];
			if (result==null && MachineSettings!=null) result = MachineSettings[Key];
			return (result==null)?"":result.ToString();
		}

		/// <summary>
		/// Returns an appSetting value from web.config.
		/// </summary>
		/// <param name="Key">The configuration key to get</param>
		/// <param name="EncryptionKey">Encryption key to decrypt value</param>
		/// <returns>Value of key.</returns>
		public static string Setting(string Key, string EncryptionKey) 
		{
			string result = Setting(Key);
			if (EncryptionKey!="") result = Cryptor.Decrypt(result, EncryptionKey);
			return result;
		}
		#endregion
		#region MachineSettings
		private static StringDictionary MachineSettings=null;
		private static FileSystemWatcher Watcher = null;

		/// <summary>
		/// Does a case-insensitive search of the attributes and returns the matching value.
		/// </summary>
		private static string GetAttribute(XmlElement El, string Name) {
			Name = Name.ToLower();
			foreach (XmlAttribute A in El.Attributes) 
				if (A.Name.ToLower()==Name) return A.Value;
			return "";
		}
		/// <summary>
		/// Returns the Child element of E with TagName having Name attribute matching Name 
		/// (case insensitive)
		/// </summary>
		private static XmlElement GetElement(XmlElement El, string TagName, string Name) {
			TagName=TagName.ToLower();
			Name = Name.ToLower();
			foreach (XmlNode N in El.ChildNodes)
				if (N.NodeType==XmlNodeType.Element && N.Name.ToLower()==TagName
					&&	(Name=="" || GetAttribute((XmlElement)N, "Name").ToLower()==Name))
					return (XmlElement)N;
			return null;
		}

		/// <summary>
		/// Adds or replaces Key value pair.
		/// </summary>
		private static void AddSetting(string Key, string Value) {
			if (MachineSettings[Key]==null) MachineSettings.Add(Key, Value);
			else MachineSettings[Key]=Value;
		}

		private static Regex R = new Regex("provider=[^;]*?;",RegexOptions.Compiled | RegexOptions.IgnoreCase);

		/// <summary>
		/// Reads the server.config file and loads appropriate
		/// settings into MachineSettings dictionary.
		/// </summary>
		private static void Init() {
			string Path=RootPath + "server.config";
			if (System.IO.File.Exists(Path)) {
				if (MachineSettings!=null) return;
				InitWatcher(RootPath, "server.config");
				XmlDocument Doc = new XmlDocument();
				Doc.Load(Path);
				MachineSettings = new StringDictionary();
				XmlElement El = GetElement(Doc.DocumentElement, "DeployedEnvironment", "");
				string DeployedEnvironment = (El==null)?"":El.InnerText;
				if (DeployedEnvironment=="") DeployedEnvironment=System.Windows.Forms.SystemInformation.ComputerName.ToLower();
				El = GetElement(Doc.DocumentElement, "Environment", DeployedEnvironment);
				if (El==null) return;
				AddSettings(GetElement(El, "General", ""));
				AddSettings(GetElement(El, "Application", AppName));
			}
		}

		/// <summary>
		/// Adds the settings from El into the app's config settings.
		/// </summary>
		/// <param name="El">Xml Element to add settings from.</param>
		private static void AddSettings(XmlElement El) {
			if (El==null) return;
			foreach (XmlNode e in El.ChildNodes) {
				if (e.NodeType!=XmlNodeType.Element) continue;
				XmlElement E = (XmlElement)e;
				switch (E.Name) {
					case "Connection":
						AddSetting("ConnectionString." + GetAttribute(E, "Name"), R.Replace(GetAttribute(E, "Value"), ""));
						if (E.Attributes["Encyrpted"]!=null)
							AddSetting("ConnectionString." + GetAttribute(E, "Name") + ".Encrypted", "true");
						break;
					case "WebService":
						AddSetting("WebService." + GetAttribute(E, "Service"), GetAttribute(E, "Url"));
						if (E.Attributes["Credentials"]!=null)
							AddSetting("WebService." + GetAttribute(E, "Service") + ".Credentials", GetAttribute(E, "Credentials"));
						if (E.Attributes["EncryptedCredentials"]!=null)
							AddSetting("WebService." + GetAttribute(E, "Service") + ".EncryptedCredentials", "true");
						break;
					case "add":
						AddSetting(GetAttribute(E, "key"), GetAttribute(E, "value"));
						break;
					default:
						AddSetting(E.Name, E.InnerText);
						break;
				}
			}
		}

		/// <summary>
		/// Creates the watcher and starts it to watching for changes in server.config
		/// </summary>
		private static void InitWatcher(string Path, string Name) {
			Watcher = new FileSystemWatcher();
			Watcher.Path = Path;
			Watcher.Filter = Name;
			Watcher.Created += new FileSystemEventHandler(OnChanged);
			Watcher.Changed += new FileSystemEventHandler(OnChanged);
			Watcher.EnableRaisingEvents = true;
		}

		/// <summary>
		/// Creates a semaphore in the bin directory, causing the application to unload.
		/// </summary>
		/// <remarks>
		/// Note that this requires the ASPNET to have write and modify privliges for
		/// the bin directory, which is a security concern.  So on production systems
		/// This will fail, and it will be necessary to do iisreset to affect changes.
		/// Any other exception is logged.
		/// </remarks>
		protected static void OnChanged(object source, FileSystemEventArgs e) {
			Watcher=null;
			try {
				File.CreateText(HttpRuntime.BinDirectory + "Stop.txt").Close();
				File.Delete(HttpRuntime.BinDirectory + "Stop.txt");
			} catch (Exception E) {
				if (E.Message.IndexOf("ccess")!=0) AppLogger.Logger.Log(E);
			}
		}

		#endregion
		#region iSetting and bSetting
		///<summary>Gets an integer setting value.</summary>
		public static int iSetting(string Key) {
			return iSetting(Key, 0);
		}
			
		///<summary>Gets an integer setting value, with default value Result.</summary>
		public static int iSetting(string Key, int Result) {
			string st = Setting(Key);
			if (st=="") return 0;
			try {Result = Convert.ToInt32(st);}
               // PC Security Defect Fix -CH1 -START Modified the below code to add logging inside the catch block 
            catch (Exception ex) { Logger.Log(ex.ToString()); }
            // PC Security Defect Fix -CH1 -END Modified the below code to add logging inside the catch block 
			return Result;
		}

		///<summary>Gets a boolean setting value.</summary>
		public static bool bSetting(string Key) {
			return bSetting(Key, false);
		}

		///<summary>Gets a boolean setting value, with default value Result.</summary>
		public static bool bSetting(string Key, bool Result) {
			string st = Setting(Key);
            try
            {
                // PC Security Defect Fix -CH2 -START Modified the below code to add logging inside the catch block 
                if (!string.IsNullOrEmpty(st))
                    Result = Boolean.Parse(st);
            }
            
            catch (Exception ex) { Logger.Log(ex.ToString()); }
            // PC Security Defect Fix -CH2 -END Modified the below code to add logging inside the catch block 
			return Result;
		}
		#endregion
		#region SettingArray
		/// <summary>
		/// Get an arraylist of a configuration setting based on the value in web.config
		/// named Name split on ','
		/// </summary>
		/// <param name="Key">The configuration key to get</param>
		/// <param name="EncryptionKey">Encryption key to decrypt value</param>
		/// <returns>Array of Values in key.</returns>
		public static ArrayList SettingArray(string Key, string EncryptionKey) {return SettingArray(Key, ',', false, EncryptionKey);}

		/// <summary>
		/// Get an arraylist of a configuration setting from web.config.
		/// </summary>
		/// <param name="Key">The configuration key to get</param>
		/// <returns>Array of Values in key.</returns>
		public static ArrayList SettingArray(string Key) {return SettingArray(Key, ',', false, "");}

		/// <summary>
		/// Get an arraylist of a configuration setting from web.config.
		/// </summary>
		/// <param name="Key">The configuration key to get</param>
		/// <param name="Splitter">Char on which to split the array.</param>
		/// <param name="ToLower">True if values should be converted to lower case.</param>
		/// <returns>Array of Values in key.</returns>
		public static ArrayList SettingArray(string Key, char Splitter, bool ToLower) 
		{
			return SettingArray(Key, Splitter, ToLower, "");
		}
		
		/// <summary>
		/// Get an arraylist of a configuration setting, based on the value split into
		/// an array on splitter.  
		/// </summary>
		/// <param name="Key">The config string Key.</param>
		/// <param name="Splitter">The char to spit the string on.</param>
		/// <param name="ToLower">Set to true to change all values to lower case.</param>
		/// <param name="EncryptionKey">Encryption key to decrypt value</param>
		/// <returns>Array of Values in key.</returns>
		public static ArrayList SettingArray(string Key, char Splitter, bool ToLower, string EncryptionKey) 
		{

			string delimlist = Config.Setting(Key, EncryptionKey);
			if (ToLower) delimlist = delimlist.ToLower();
			ArrayList aryApps = new System.Collections.ArrayList();

			if (delimlist.Length > 0) 
			{
				aryApps.AddRange( delimlist.Split(Splitter) );
			}

			return aryApps;
		}
		#endregion
		#region Initialize
		/// <summary>
		/// Initializes default values from the config file; matches config
		/// values with O.GetType().Name + "_" + [Field/Property Name]
		/// </summary>
		/// <param name="O">Object to initialize values for.</param>
		public static void Initialize(System.Web.UI.Control O) 
		{
			if (O.Site!=null && O.Site.DesignMode) return;
			Type T = O.GetType().BaseType;
			string st = T.Name + "_";
			foreach (string s in ConfigurationSettings.AppSettings.AllKeys) 
				if (s.IndexOf(st)==0) 
				{
					try 
					{	PropertyInfo P = T.GetProperty(s.Replace(st,""));
						if (P!=null)
							if (P.PropertyType==typeof(string))
								P.SetValue(O,Config.Setting(s),null);
							else if (P.PropertyType==typeof(bool))
								P.SetValue(O,Config.bSetting(s),null);
					} 
					catch (Exception e)
					{
						throw new Exception(e.Message + ": " + T.Name + "." + s.Replace(st,""));
					}
				}
		}
		#endregion	
	}
}
