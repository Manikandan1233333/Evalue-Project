using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.ComponentModel;
using CSAAWeb;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Xsl;
using System.IO;
using System.Data;
using System.Data.SqlClient;
using System.Runtime.InteropServices;
using System.Reflection;

namespace CSAAWeb.WebControls
{
	/// <summary>
	/// Summary description for XmlReport.
	/// </summary>
	[ToolboxData("<{0}:XmlReport runat=server></{0}:XmlReport>")]
	public class AdoXmlReport : BaseXmlReport {
		private string ConnectionString = string.Empty;
		//private static Type TConn = typeof(ADODB.ConnectionClass);
		//private static Type TRs = typeof(ADODB.RecordsetClass);

		///<summary>Stored procedure name to execute.</summary>
		public string Sql = "";
		///<summary>Name of the the connection string key in web.config.</summary>
		public string ConnectionName = "ConnectionString";
		///<summary>An open recordset which can be supplied instead of Sql.</summary>
		public ADODB.Recordset oRs = null;
		
		/// <summary>
		/// Ensures that any internal objects are released.
		/// </summary>
		~AdoXmlReport() {
			oRs=null;
		}

		/// <summary>
		/// Creates the connection object and opens it with the specified connection string.
		/// </summary>
		private void OpenConnection(ref ADODB.Connection oConn) {
			ConnectionString = Config.Setting(ConnectionName);
			if (Config.bSetting(ConnectionName + ".Encrypted"))
				ConnectionString = CSAAWeb.Cryptor.Decrypt(ConnectionString, CSAAWeb.Constants.CS_STRING);
			if (ConnectionString.ToLower().IndexOf("dsn=")==-1 &&
				ConnectionString.ToLower().IndexOf("provider=")==-1)
				ConnectionString = "Provider=SQLOLEDB.1;" + ConnectionString; 
			oConn = new ADODB.ConnectionClass();
			oConn.GetType().InvokeMember("Open", BindingFlags.InvokeMethod, null, oConn, new object[] {ConnectionString});
		}

		/// <summary>
		/// Creates a recordset object, creates a command object and opens the recordset
		/// with the command.
		/// </summary>
		/// <param name="oCmd"></param>
		private void OpenRecordset(ADODB.Command oCmd) {
			oRs = new ADODB.RecordsetClass();
			oRs.GetType().InvokeMember("Open", BindingFlags.InvokeMethod, null, oRs, new object[] {oCmd});
		}

		///<summary>
		///Creates and returns a command object on oConn
		///from the stored procedure specified in the Sql property.
		///Fills the stored procedure parameters from either the request.querystring
		///or request.form collection depending on whether the page is post or get.
		///</summary>
		private ADODB.Command CreateCommand(ref ADODB.Connection oConn) {
			OpenConnection(ref oConn);
			ADODB.Command oCmd = new ADODB.CommandClass();
			oCmd.ActiveConnection=oConn;
			oCmd.CommandType = ADODB.CommandTypeEnum.adCmdStoredProc;
			oCmd.CommandText = Sql;
			oCmd.Parameters.Refresh();
			NameValueCollection Req = (Page.Request.HttpMethod=="POST")?Page.Request.Form:Page.Request.QueryString;
			foreach (ADODB.Parameter P in oCmd.Parameters) {
				string st = Req[P.Name.Substring(1)];
				if (st!=null && st!="") P.Value=st;
			}
			return oCmd;
		}

		/// <summary>
		/// Extracts the data contained in the ADO recordset and stores it in 
		/// XmlSource.  If there is another recordset, then the first was actually
		/// Extended attributes metadata, so that document is move to XmlExtendedAttributes,
		/// and the data in the second recordset is store into XmlSource.
		/// </summary>
		private void GetSource() {
			ADODB.Connection oConn = null;
			if (oRs==null) OpenRecordset(CreateCommand(ref oConn));
			MSXML2.DOMDocument D1 = new MSXML2.DOMDocumentClass();
			// load the 1st dataset
			oRs.Save(D1, ADODB.PersistFormatEnum.adPersistXML);
			XmlSource = new XmlDocument();
			XmlSource.LoadXml(D1.xml);

			object RecordsAffected;
			oRs=oRs.NextRecordset(out RecordsAffected);
			if (oRs!=null && oRs.State==1) {
				XmlExtendedAttributes = XmlSource;
				// Load the 2nd dataset
				oRs.Save(D1, ADODB.PersistFormatEnum.adPersistXML);
				XmlSource = new XmlDocument();
				XmlSource.LoadXml(D1.xml);
			}
			if (oRs!=null && oRs.State==1) oRs.Close();
			if (oConn!=null) oConn.Close();
		}

		/// <summary>
		/// Generates the report from the supplied recordset or Sql Proc name.
		/// </summary>
		protected override void OnPreRender(EventArgs e) {
			if (Sql=="" && oRs==null) return;
			GetSource();
			base.OnPreRender(e);
		}

	}

}
