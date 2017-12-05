using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Collections;
using System.Reflection;

namespace CSAAWeb.Serializers
{
	/// <summary>
	/// Strongly typed collection for Parameters collection based on the Sql statement
	/// as a key. Provides methods to retrieve parameter collections, and caches
	/// them for repeat use.
	/// </summary>
	/// <remarks>
	/// To extend this class to handle more DbData types beyond SqlClient and OleDbClient,
	/// add appropriate lines to the Add method for the new type.
	/// </remarks>
	internal class ParameterCollectionCache : System.Collections.Specialized.NameObjectCollectionBase 
	{
		/// <summary>
		/// Retrieves the parameters collection from the cache.
		/// </summary>
		/// <param name="Cmd">The command to get parameters for.</param>
		public void GetParameters(IDbCommand Cmd) {
			if (!Contains(Cmd)) 
				Add(Cmd);
			else this[Cmd].CopyTo(Cmd);
		}
		/// <summary>
		/// Sets a parameter value in Parameters C to Value, and if the parameter type is
		/// a string, and the parameter length is greater than the length
		/// of the value, sets the parameter length to the length of the 
		/// value.  This is because Text parameter length is always the max
		/// length which causes an excess memory consumption.
		/// </summary>
		/// <param name="C">The parameter collection</param>
		/// <param name="Name">The parameter to set.</param>
		/// <param name="Value">The value to set it to.</param>
		public void SetValue(IDataParameterCollection C, string Name, Object Value) {
			if (!C.Contains(Name)) return;
			IDataParameter P = (IDataParameter)C[Name];
			if (IsTextField(P)) {
				int Len = (Value==null)?0:((string)Value).Length;
				PropertyInfo M = P.GetType().GetProperty("Size");
				if (M!=null) M.SetValue(P, Len, null);
			}
			if (Value!=null) P.Value=Value;
		}
		/// <summary>
		/// Returns true if the parameter is a text or long string.
		/// </summary>
		/// <param name="P">Parameter to check</param>
		/// <returns>Boolean true if long string</returns>
		private bool IsTextField(IDataParameter P) {
			return ((P is SqlParameter && IsTextField(((SqlParameter)P).SqlDbType)) ||
				(P is OleDbParameter && IsTextField(((OleDbParameter)P).OleDbType)));
		}
		/// <summary>
		/// Returns true if the parameter is a text or long string.
		/// </summary>
		/// <param name="T">Parameter type to check</param>
		/// <returns>Boolean true if long string</returns>
		private bool IsTextField(SqlDbType T) {
			return (T==SqlDbType.Text || T==SqlDbType.NText);
		}
		/// <summary>
		/// Returns true if the parameter is a text or long string.
		/// </summary>
		/// <param name="T">Parameter type to check</param>
		/// <returns>Boolean true if long string</returns>
		private bool IsTextField(OleDbType T) {
			return (T==OleDbType.LongVarChar || T==OleDbType.LongVarWChar);
		}
		/// <summary>
		/// Gets the parameter collection for Cmd if it is cached
		/// </summary>
		public ArrayOfParameterDef this[IDbCommand Cmd] {
			get {return (ArrayOfParameterDef)BaseGet(Cmd.CommandText);}
		}
		/// <summary>
		/// Returns true if the Sql for Cmd is in the collection.
		/// </summary>
		/// <param name="Cmd">The command to look for.</param>
		/// <returns>True if its here.</returns>
		public bool Contains(IDbCommand Cmd) 
		{
			foreach (string st in this.Keys) if (st==Cmd.CommandText) return true;
			return false;
		}

		/// <summary>
		/// Calls the appropriate CommandBuilder.DeriveParameters method
		/// for Cmd, and adds its parameters to the collection. 
		/// Throws an exception if Cmd isn't from SqlClient or OleDb.
		/// </summary>
		/// <param name="Cmd">Command to get parameters of.</param>
		public void Add(IDbCommand Cmd) 
		{
			if (typeof(SqlCommand).IsInstanceOfType(Cmd)) {
				Add((SqlCommand)Cmd);
			} else if(typeof(OleDbCommand).IsInstanceOfType(Cmd)) {
				Add((OleDbCommand)Cmd);
			} else throw new Exception(Cmd.GetType().Name + " not supported.");
		}

		private void Add(SqlCommand Cmd) {
			if (Cmd.Transaction==null) {
				SqlCommandBuilder.DeriveParameters(Cmd);
				BaseAdd(Cmd.CommandText, new ArrayOfParameterDef(Cmd.Parameters));
			} else {
				SqlConnection Conn = new SqlConnection(Cmd.Connection.ConnectionString);
				Conn.Open();
				SqlCommand Cmd1 = new SqlCommand(Cmd.CommandText, Conn);
				Cmd1.CommandType = CommandType.StoredProcedure;
				Add(Cmd1);
				Conn.Close();
				this[Cmd].CopyTo(Cmd);
			}
		}

		private void Add(OleDbCommand Cmd) {
			if (Cmd.Transaction==null) {
				OleDbCommandBuilder.DeriveParameters(Cmd);
				BaseAdd(Cmd.CommandText, new ArrayOfParameterDef(Cmd.Parameters));
			} else {
				OleDbCommand Cmd1 = new OleDbCommand(Cmd.CommandText, new OleDbConnection(Cmd.Connection.ConnectionString));
				Cmd1.CommandType = CommandType.StoredProcedure;
				Cmd1.Connection.Open();
				Add(Cmd1);
				Cmd1.Connection.Close();
				this[Cmd].CopyTo(Cmd);
			}
		}
	}

	/// <summary>
	/// Generic parameter class used for caching parameters
	/// </summary>
	/// <remarks>
	/// Note: to extend this class to handle parameters other than SqlParameter and
	/// OleDbParameter, add a property of the correct name just like SqlDbType.  This
	/// assumes that the new parameter type has a 10-value constructor in the same form
	/// as SqlParameter, if not there will be more coding.
	/// </remarks>
	public class ParameterDef : SimpleSerializer 
	{
		/// <summary>Default constructor.</summary>
		public ParameterDef() : base() {}
		/// <summary>Xml constructor</summary>
		public ParameterDef(string Xml) : base (Xml) {}
		/// <summary>Object constructor</summary>
		public ParameterDef(Object O) : base(O) {
			Type T = O.GetType();
			foreach (ConstructorInfo i in T.GetConstructors())
				if (i.GetParameters().Length==10) ParameterConstructor=i;
			if (ParameterConstructor==null) throw new Exception(T.Name + " not supported for caching.");
			if (typeof(SqlParameter).IsInstanceOfType(O))
				CheckText((SqlParameter)O);
			else if (typeof(OleDbParameter).IsInstanceOfType(0))
				CheckText((OleDbParameter)O);
		}

		private void CheckText(SqlParameter P) {
			if (P.SqlDbType==SqlDbType.VarChar && P.Size>8000) {
				P.SqlDbType=SqlDbType.Text;
				SqlDbType=SqlDbType.Text;
			} else if (P.SqlDbType==SqlDbType.NVarChar && P.Size>4000) {
				SqlDbType=SqlDbType.NText;
				P.SqlDbType=SqlDbType.NText;
			}
		}

		private void CheckText(OleDbParameter P) {
			if (P.OleDbType==OleDbType.VarChar && P.Size>8000) {
				P.OleDbType=OleDbType.LongVarChar;
				OleDbType=OleDbType.LongVarChar;
			} else if (P.OleDbType==OleDbType.VarWChar&& P.Size>4000) {
				P.OleDbType=OleDbType.LongVarChar;
				OleDbType=OleDbType.LongVarWChar;
			}
		}

		///<summary>The constructor for the original parameter type</summary>
		public ConstructorInfo ParameterConstructor;
		///<summary>The Type of the original parameter</summary>
		public string ParameterName; 
		/// <summary>DataType of the parameter</summary>
		public object BaseDbType;
		/// <summary>SqlDataType of the parameter</summary>
		public SqlDbType SqlDbType {set {BaseDbType=value;} get {return (SqlDbType)BaseDbType;}}
		/// <summary>OleDbDataType of the parameter</summary>
		public OleDbType OleDbType {set {BaseDbType=value;} get {return (OleDbType)BaseDbType;}}
		/// <summary>Size of the parameter</summary>
		public int Size; 
		/// <summary>Direction of the parameter</summary>
		public ParameterDirection Direction;
		/// <summary>True if the parameter can contain null values</summary>
		public bool IsNullable;
		/// <summary>Precision of the parameter</summary>
		public byte Precision;
		/// <summary>Scale of the value of the parameter</summary>
		public byte Scale;
		/// <summary>The source column</summary>
		public string SourceColumn;
		/// <summary>Source version</summary>
		public DataRowVersion SourceVersion; 
		/// <summary>The value.</summary>
		public object Value;

		/// <summary>
		/// An array of objects suitable for passing to a parameter constructor.
		/// </summary>
		private object[] Detail {
			get {return new object[] {ParameterName, BaseDbType, Size, Direction, IsNullable, Precision, Scale, SourceColumn, SourceVersion, null};}
		}
		/// <summary>
		/// Returns a real parameter of the correct Db type based on the values of this.
		/// </summary>
		/// <returns>IDataParameter of appropriate type</returns>
		public IDataParameter Clone() 
		{
			IDataParameter P = (IDataParameter)this.ParameterConstructor.Invoke(Detail);
			return P;
		}
	}

	/// <summary>
	/// Array of generic parameters used to cache parameters of various types.
	/// </summary>
	internal class ArrayOfParameterDef : ArrayOfSimpleSerializer 
	{
		/// <summary>Default constructor.</summary>
		public ArrayOfParameterDef() {}
		///<summary>Constructor accepts IDataParameterCollection</summary>
		public ArrayOfParameterDef(IDataParameterCollection Source) 
		{
			CopyFrom(Source);
		}
		/// <summary>
		/// Copies records from a parameter collection.
		/// </summary>
		/// <param name="Source">The parameter collection</param>
		private void CopyFrom(IDataParameterCollection Source) 
		{
			IDataParameter[] A = new IDataParameter[Source.Count];
			Source.GetType().InvokeMember("CopyTo", 
				BindingFlags.InvokeMethod | BindingFlags.Public | BindingFlags.Instance, 
				null, Source, new object[] {A,0});
			for (int i=0; i<A.Length; i++) Add(new ParameterDef(Source[i]));
		}
		/// <summary>
		/// Adds the parameters in collection to Cmd.Parameter.
		/// </summary>
		/// <param name="Cmd"></param>
		public void CopyTo(IDbCommand Cmd) 
		{
			foreach (ParameterDef i in this) Cmd.Parameters.Add(i.Clone());
		}


	}
}
