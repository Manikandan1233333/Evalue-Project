using System;
using System.Data.SqlClient;
using System.Collections;
using CSAAWeb;
using CSAAWeb.AppLogger;
using CSAAWeb.Serializers;

namespace PaymentClasses
{
	/// <summary>
	/// A class for keeping application permissions and keys.
	/// </summary>
	public class AppDictionary : DictionaryBase
	{
		/// <summary>
		/// Constructor for data from database
		/// </summary>
		public AppDictionary(SqlDataReader reader) 
		{
			AddData(reader);
		}

		/// <summary>
		/// Default constructor
		/// </summary>
		public AppDictionary()
		{
			App a = new App(true);
			InnerHashtable.Add(a.ApplicationName, a);
		}

		/// <summary>
		/// Adds the data the dictionary from reader
		/// </summary>
		public void AddData(SqlDataReader reader)
		{
			while (reader.Read()) 
			{
				string key = reader.GetString(1);
				if (Contains(key)) InnerHashtable.Remove(key);
				InnerHashtable.Add(key, new App(reader));
			}
			reader.Close();
		}

		/// <summary>
		/// Item property
		/// </summary>
		public App this[string key]
		{
			get {return (App) InnerHashtable[key];}
			set {InnerHashtable[key] = value;}
		}

		///<summary/>
		public bool Contains(string key)
		{
			return InnerHashtable.Contains(key);
		}

		/// <summary>
		/// Returns the Application Name of ApplicationID, if known.
		/// </summary>
		public string LookupID(string ApplicationID)
		{
			foreach (DictionaryEntry i in InnerHashtable)
				if (((App)i.Value).ApplicationID==ApplicationID) return ((App)i.Value).ApplicationName;
			return "Unknown";
		}

		/// <summary>
		/// Returns an XML representation of object.  Primary useful during debugging.
		/// </summary>
		public override string ToString()
		{
			string st = "<"+this.GetType().Name+">\r\n";
			foreach (DictionaryEntry i in InnerHashtable)
				st += "  <Entry key=\""+i.Key.ToString()+"\">\r\n    " + Serializer.ToXML(i.Value).Replace("\n", "\n    ") + "\r\n  </Entry>\r\n";
			st += "</"+this.GetType().Name+">\r\n";
			return st;
		}

	}

	/// <summary>
	/// A Class for containing information about an application that can use the payment service.
	/// </summary>
	public class App
	{
		///<summary/>
		public string ApplicationID=string.Empty;
		///<summary/>
		public string ApplicationName=string.Empty;
		///<summary/>
		public ArrayList PermittedFunctions=null;
		///<summary/>
		public string EncryptionKey=string.Empty;
		///<summary/>
		public string MerchantID=string.Empty;

		/// <summary>
		/// Default constructor required by serializer.
		/// </summary>
		public App() {}

		/// <summary>
		/// Use this constructor for getting default information about the running application
		/// from the config file.
		/// </summary>
		/// <param name="Auto">Dummy parameter to know to use this constuctor instead of default.</param>
		internal App(bool Auto)
		{
			EncryptionKey = Config.Setting("WebService.Payments.AppKey");
			ApplicationName = Logger.AppNameSpace();
			ApplicationID = Config.Setting(ApplicationName + "_APP_ID");
			PermittedFunctions = new ArrayList();
		}

		/// <summary>
		/// Use this constructor for data coming from the database.
		/// </summary>
		/// <param name="reader"></param>
		internal App(SqlDataReader reader)
		{
			ApplicationID = reader.GetString(0);
			ApplicationName = reader.GetString(1);
			PermittedFunctions = SettingArray(reader.GetString(2));
			EncryptionKey = reader.GetString(3);
			MerchantID = reader.GetString(4);
		}

		/// <summary>
		/// Returns an ArrayList from a delimited string.
		/// </summary>
		/// <param name="st"></param>
		/// <returns></returns>
		private static ArrayList SettingArray(string st) 
		{
			ArrayList result = new System.Collections.ArrayList();
			if (st.Length > 0) result.AddRange( st.ToLower().Split(',') );
			return result;
		}
	}
}
