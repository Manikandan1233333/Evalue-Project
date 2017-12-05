/*
 * HISTORY:
 * 11/2/04	JOM Made ConnectionString property virtual.
 * 11/22/04 JOM Added properties and methods to allow Transaction to be persisted and extended
 *			beyond the web service boundary.  Public changes are the protected method SetExtendedTransaction
 *			which allows derived classes to have methods that create extended transactions, and
 *			the public web service method CompleteTransaction, which allows the caller to commit or
 *			rollback the persisted transaction.	 Note that while this facility allows for transaction
 *			processing across system boundaries, it doesn't perform a two-stage commit process,
 *			so it is possible that unsynchronized data may be created if an exception occurs during
 *			commit from another database.
 * PC Security Defect Fix -CH1 - Modified the below code to add logging inside the catch block 
*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Web;
using System.Web.Services;
using System.Collections.Specialized;
using System.Reflection;
using CSAAWeb.AppLogger;
namespace CSAAWeb.Web
{
	/// <summary>
	/// Summary description for SqlWebService.
	/// </summary>
	public class SqlWebService : System.Web.Services.WebService, IClosableWeb
	{
		///<summary>All the connection strings used by the various services.</summary>
		private static ListDictionary ConnectionCache = null;
		///<summary>Transactions are stored here for multi-step transactions.</summary>
		private static ListDictionary TransCache = null;
		///<summary>Connection object used by the service.</summary>
		private SqlConnection oConn = null;
		///<summary>Index to the Transaction in the cache</summary>
		private int TransIndex = 0;
		///<summary>Set to true if this transaction is extended beyond the web service boundary.</summary>
		protected bool ExtTrans = false;
		///<summary>Optional transaction that can be used by connections.</summary>
		private SqlTransaction Transaction {
			get {return (SqlTransaction)TransCache[TransIndex];}
			set {
				if (value==null) {
					if (TransIndex!=0 && TransCache.Contains(TransIndex))
						TransCache.Remove(TransIndex);
					TransIndex=0;
				} else {
					TransIndex=value.GetHashCode(); 
					TransCache.Add(TransIndex, value);
				}
			}
		}
		///<summary>Connection string for this instance.</summary>
		protected virtual string ConnectionString {
			get {return (string)ConnectionCache[this.GetType().Name];}
		}
		///<summary>This will be true once PreCacheParameters has been called.</summary>
		protected bool ParametersPreCached = false;

		///<summary/>
		static SqlWebService() {
			ConnectionCache = new ListDictionary();
			TransCache = new ListDictionary();
		}
		///<summary>
		///Default constructor, opens the connection.  Checks to see if the class is
		///initialized, and initializes it first if not.
		///</summary>
		public SqlWebService() {
			Type T = this.GetType();
			if (ConnectionString==null) GetConnectionInfo(T.Name); 
			InitializeComponent();
			ClosableModule.SetHandler(this);
		}

		/// <summary>
		/// Opens the database connection if needed
		/// </summary>
		protected void CheckConnection() {
			if (oConn==null) {
				oConn = new SqlConnection(ConnectionString);
				try {
					oConn.Open();
				} catch (Exception e) {
					CSAAWeb.AppLogger.Logger.Log(e);
					oConn=null;
					throw;
				}
			}
		}

		/// <summary>
		/// Closes the connection and rollsback any open transaction.
		/// </summary>
		public void Close(object Source, EventArgs e) {
			Close();
		}

		/// <summary>
		/// Closes the connection and rollsback any open transaction.
		/// </summary>
		public virtual void Close() {
			if (!ExtTrans) {
				if (Transaction!=null) Transaction.Rollback();
				Transaction=null;
				if (oConn!=null && oConn.State!=ConnectionState.Closed) {
					oConn.Close();
				}
			}
			oConn = null;
			TransIndex=0;
			ExtTrans=false;
			ClosableModule.RemoveHandler(this);
		}

		/// <summary>
		/// Gets the connection information for the class and caches the parameters.
		/// </summary>
		private void InitializeClass(Type T) {
			if (ConnectionString==null) {
				GetConnectionInfo(T.Name);
				PreCacheParameters();
			}
		}

		/// <summary>
		/// Looks for the service in web.config and creates an entry in ConnectionCache for it.
		/// </summary>
		private void GetConnectionInfo(string Class) {
			string st = Config.Setting("ConnectionString."+Class, 
			((Config.bSetting("ConnectionString."+Class+".Encrypted"))?Constants.CS_STRING:""));
			if (st==string.Empty) throw new Exception("No connection string found in web.config for " + Class);
			lock (ConnectionCache) ConnectionCache[Class] = st;
		}

		/// <summary>
		/// Retrieves parameter information for
		/// some stored procs that will be called later that have large 
		/// numbers of parameters being retrieved automatically.
		/// </summary>
		protected void PreCacheParameters() {
			Type T = GetType();
			FieldInfo F = T.GetField("PreCacheParametersFor", BindingFlags.Static | BindingFlags.NonPublic);
			if (F!=null) {
                try { CheckConnection(); }
                    //PC Security Defect Fix -CH1 -START Modified the below code to add logging inside the catch block 
                catch (Exception ex)
                {
                    Logger.Log(ex.ToString());
                    return;
                }
                //PC Security Defect Fix -CH1 -END Modified the below code to add logging inside the catch block 
				CacheParameters((string[])F.GetValue(this));
			}
			ParametersPreCached=true;
		}

		/// <summary>
		/// Retrieves parameter information for
		/// some stored procs that will be called later that have large 
		/// numbers of parameters being retrieved automatically.
		/// </summary>
		private void CacheParameters(string [] Procedures) {
			foreach (string st in Procedures) 
				CSAAWeb.Serializers.Serializer.CacheParameters(GetCommand(st));
		}

		/// <summary>
		/// Returns a command object for the stored proc.  Fills the parameters collection
		/// from the previously retrieved parameters collection Ps.
		/// </summary>
		protected SqlCommand GetCommand(string CommandString) {
			CheckConnection();
			SqlCommand Cmd = new SqlCommand(CommandString, oConn);
			Cmd.CommandType = CommandType.StoredProcedure;
			if (Transaction!=null) Cmd.Transaction=Transaction;
			return Cmd;
		}

		/// <summary>
		/// Call this method to begin a new transaction.
		/// </summary>
		protected void StartTransaction() {
			if (!ParametersPreCached) PreCacheParameters();
			Transaction = oConn.BeginTransaction();
		}

		/// <summary>
		/// Call this method to end a transaction.
		/// </summary>
		protected void CompleteTransaction(bool Commit) {
			if (Transaction==null) return;
			try {
				if (Commit) Transaction.Commit();
				else Transaction.Rollback();
			} catch {
				Transaction=null;
				throw;
			}
			Transaction=null;
			ExtTrans=false;
		}

		/// <summary>
		/// call this to extend the existing transaction beyond the web service boundary.
		/// </summary>
		/// <returns>Index of the process transaction for future complete calls.</returns>
		protected int SetExtendedTransaction() {
			ExtTrans=true;
			return TransIndex;
		}

		
		/// <summary>
		/// Completes a previous extended transaction.
		/// </summary>
		/// <param name="Commit">True to commit the transaction, false to rollback.</param>
		/// <param name="Index">
		/// The index of the transaction.  Must have been provided by the
		/// web method that created the transaction, through a call to SetExtendedTransaction()
		/// </param>
		[WebMethod(Description="Completes a previous extended transaction.")]
		public void CompleteTransaction(int Index, bool Commit) {
			if (Index==0) return;
			int I1 = TransIndex;
			TransIndex=Index;
			bool E1 = ExtTrans;
			CompleteTransaction(Commit);
			TransIndex=I1;
			ExtTrans=E1;
		}

		#region Component Designer generated code
		
		//Required by the Web Services Designer 
		private IContainer components = null;
				
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing ) {
			if(disposing && components != null) {
				components.Dispose();
			}
			base.Dispose(disposing);		
		}
		
		#endregion
	}
}
