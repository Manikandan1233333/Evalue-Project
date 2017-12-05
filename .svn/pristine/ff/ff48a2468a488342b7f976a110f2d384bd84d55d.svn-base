//67811A0  - PCI Remediation for Payment systems - CH1 - added try catch block to enable logging to identify the paramters causing the data type mismatch by cognizant on 01/09/2011
//PC Phase II changes CH1 - Added the below code for handling Exceptions. For ArrayOfObjects method chnaged the first parameter from int to DataTable.
//PC Phase II changes CH2 - Added the below code to resolve Datatype Conversion Error by mapping the Source List to Destination DataSet by checking the name of the field.
//PC Phase II changes CH3 - Added the below code to retrieve the values by checking the Field name.
//PC Security Defect Fix -CH1 - Modified the below code to add logging inside the catch block
using System;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Text.RegularExpressions;
using System.Xml.Serialization;
using System.Reflection;
using System.IO;
using System.Web.UI;
using System.Collections;
using System.Text;
using System.Xml;
using System.Xml.Schema;
using CSAAWeb;
using CSAAWeb.AppLogger;

namespace CSAAWeb.Serializers {
	/// <summary>
	/// Contains class methods for serializing and deserializing.  Used by serializable
	/// classes defined in this module.
	/// </summary>
	public class Serializer {
		private static ParameterCollectionCache Cache;
		private static Regex r;
		private static Regex ns;

		static Serializer() {
			Cache = new ParameterCollectionCache();
			r = new Regex("(\\<\\?xml[^\\>]*\\>| *xmlns(?:\\:[^=]*)?=[^ \\>]*| *[^ =]*=\"\")", RegexOptions.Compiled);
			ns = new Regex("(\\<[^?][^ \\>]*)", RegexOptions.Compiled);
		}
		/// <summary>
		/// Converts o to an XML string, getting public properties and fields.
		/// </summary>
		/// <param name="o">The object to convert.</param>
		/// <returns>XML string.</returns>
		public static string ToXML(object o) {
			StringWriter s = new StringWriter();
			(new XmlSerializer(o.GetType())).Serialize(s, o);
			string st = s.ToString();
			st = r.Replace(st, "");
			if (st.Substring(0,2)=="\r\n") st=st.Substring(2);
			return st;
		}

		/// <summary>
		/// Returns a new instance of the object from an XML string
		/// </summary>
		/// <param name="T">The type to create.</param>
		/// <param name="xml">XML string to convert from</param>
		/// <returns>Object of type T filled with values from xml.</returns>
		public static object FromXML(Type T, string xml) {
			if (T==null) throw new Exception("Type not provided.");
			try {
				return (!(new Regex("^\\s*\\<(?:\\?xml|"+T.Name+")")).Match(xml).Success)?
					null:(new XmlSerializer(T)).Deserialize(new StringReader(ns.Replace(xml,"$1 xmlns:xsd=\"http://www.w3.org/2001/XMLSchema\" xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\" ",1)));
			} 
			catch (Exception e) {
				throw new Exception(e.Message + "XML: " + xml);
			}
		}

		/// <summary>
		/// Method attempts to determine the type of the caller and creates an
		/// object of that type, then fills it with values from xml.
		/// </summary>
		/// <param name="xml">XML string of object's values.</param>
		/// <returns>Object of caller's type with values from xml.</returns>
		public static object FromXML(string xml) {
			string caller = AppLogger.Logger.Caller();
			caller = caller.Substring(0,caller.LastIndexOf("."));
			Type T = Type.GetType(caller);
			if (T==null) throw new Exception("Type could not be ascertained for " + caller + ".");
			return FromXML(T, xml);
		}

		/// <summary>
		/// Convenience function, checks for missing values (blanks or null)
		/// </summary>
		/// <param name="s">The string to check.</param>
		internal static bool IsMissing(String s) {
			return ((s == null) || (s.Length == 0));
		}

		/// <summary>
		/// Copies the values of properties and fields of source into the matching fields or properties in dest.
		/// Loop on members of dest, return dest.
		/// </summary>
		/// <param name="source">Object to copy from.</param>
		/// <param name="dest">Object to copy to.</param>
		/// <returns>dest.</returns>
		public static Object CopyFrom(Object source, Object dest) {
			return CopyFrom(source, dest, true);
		}
		/// <summary>
		/// Copies the values of properties and fields of source into the matching fields or properties in dest.
		/// Loop on members of dest, return dest.
		/// </summary>
		/// <param name="source">Object to copy from.</param>
		/// <param name="dest">Object to copy to.</param>
		/// <param name="DeclaredOnly">If true only copies properties that are declared. If true
		/// also copies properties that are inherited.</param>
		/// <returns>dest.</returns>
		public static Object CopyFrom(Object source, Object dest, bool DeclaredOnly) {
			Type T = GetOType(source);
			foreach (MemberInfo i in GetOType(dest).GetMembers((DeclaredOnly)?LookAt1:LookAt2)) 
				if (i.Name!="Item") {	
					Object V=GetValue(source, GetMember(T, i.Name));
					if (V!=null) SetValue(dest, i, V);
				}
			return dest;
		}

		/// <summary>
		/// Copies the values of Reader to the matching fields or properties of dest.
		/// Loop on Reader, return dest.
		/// </summary>
		/// <param name="Reader">Dataset to copy values from.  Dataset should be open to
		/// a specific record.</param>
		/// <param name="dest">Object to copy values to.</param>
		/// <returns>dest.</returns>
		public static Object CopyFrom(IDataReader Reader, Object dest) {
			Type T = GetOType(dest);
			for (int i=0; i<Reader.FieldCount; i++) {
				Object V=Reader.GetValue(i);
				if (V!=null && V!=DBNull.Value) SetValue(dest, GetMember(T, Reader.GetName(i)), V);
			}
			return dest;
		}


		/// <summary>
		/// Copies the values of Row to the matching fields or properties of dest.
		/// Loop on Row, return dest.
		/// </summary>
		/// <param name="Row">DataRow to copy values from.</param>
		/// <param name="dest">Object to copy values to.</param>
		/// <returns>dest.</returns>
		public static Object CopyFrom(DataRow Row, Object dest) {
			Type T = GetOType(dest);
			for (int i=0; i<Row.ItemArray.Length; i++) {
				Object V=Row.ItemArray[i];
				if (V!=null && V!=DBNull.Value) SetValue(dest, GetMember(T, Row.Table.Columns[i].ColumnName), V);
			}
			return dest;
		}


		/// <summary>
		/// Copies objects form the source list to the dest, creating
		/// new objects to put into dest of dest's underlying type.  Throws
		/// an exception if dest is not a strongly typed collection.
		/// </summary>
		/// <param name="source">Collection to copy items from.</param>
		/// <param name="dest">Collection to copy items to.</param>
		/// <returns>dest.</returns>
		public static Object CopyFrom(IList source, IList dest) {
			return CopyFrom(source, dest, true);
		}

		/// <summary>
		/// Copies objects form the source list to the dest, creating
		/// new objects to put into dest of dest's underlying type.  Throws
		/// an exception if dest is not a strongly typed collection.
		/// </summary>
		/// <param name="source">Collection to copy items from.</param>
		/// <param name="dest">Collection to copy items to.</param>
		/// <param name="DeclaredOnly">If true only copies properties that are declared. If true
		/// also copies properties that are inherited.</param>
		/// <returns>dest.</returns>
		public static Object CopyFrom(IList source, IList dest, bool DeclaredOnly) {
			dest.Clear();
			for (int i=0; i<source.Count; i++ )
				dest.Add((SimpleSerializer)CopyFrom(source[i], Construct(GetListBaseType(dest)), DeclaredOnly));
			return dest;
		}

		/// <summary>
		/// Copies records from the reader to newly created members of the list
		/// dest and returns dest.  Throws an exception if dest is not a strongly
		/// typed collection.
		/// </summary>
		/// <param name="Reader">Dataset to copy records from.</param>
		/// <param name="dest">Collection to copy items to.</param>
		/// <returns>dest.</returns>
		public static Object CopyFrom(IDataReader Reader, IList dest) {
			dest.Clear();
			while (Reader.Read()) 
				dest.Add((SimpleSerializer)CopyFrom(Reader, Construct(GetListBaseType(dest))));
			return dest;
		}

		/// <summary>
		/// Copies records from the Table to newly created members of the list
		/// dest and returns dest.  Throws an exception if dest is not a strongly
		/// typed collection.
		/// </summary>
		/// <param name="Table">Dataset to copy records from.</param>
		/// <param name="dest">Collection to copy items to.</param>
		/// <returns>dest.</returns>
		public static Object CopyFrom(DataTable Table, IList dest) {
			dest.Clear();
			for (int i=0; i<Table.Rows.Count; i++)
				dest.Add((SimpleSerializer)CopyFrom(Table.Rows[i], Construct(GetListBaseType(dest))));
			return dest;
		}

		/// <summary>
		/// Invokes the default constructor of type T and returns an object of
		/// that type.
		/// </summary>
		/// <param name="T">Type to invoice constructor of.</param>
		/// <returns>Object of type T.</returns>
		public static Object Construct(Type T) {
			return T.GetConstructor(Type.EmptyTypes).Invoke(null);
		}

		/// <summary>
		/// Copies the values of properties and fields of source into the matching fields or properties in dest.
		/// Loop on members of source, return dest.
		/// </summary>
		/// <param name="source">Object to copy from.</param>
		/// <param name="dest">Object to copy to.</param>
		/// <returns>dest.</returns>
		public static Object CopyTo(Object source, Object dest) {
			return CopyTo(source, dest, true);
		}

		/// <summary>
		/// Copies the values of properties and fields of source into the matching fields or properties in dest.
		/// Loop on members of source, return dest.
		/// </summary>
		/// <param name="source">Object to copy from.</param>
		/// <param name="dest">Object to copy to.</param>
		/// <param name="DeclaredOnly">True if only declared members to be copied; false if inherited also</param>
		/// <returns>dest.</returns>
		public static Object CopyTo(Object source, Object dest, bool DeclaredOnly) {
			Type T = GetOType(dest);
			foreach (MemberInfo i in GetOType(source).GetMembers((DeclaredOnly)?LookAt1:LookAt2)) {
				Object V=GetValue(source, i);
				if (V!=null) SetValue(dest, GetMember(T, i.Name), V);
			}
			return dest;
		}

		/// <summary>
		/// Copies the values of fields to the matching parameters of a db command
		/// object representing a stored procedure with named parameters.
		/// Loop on dest, return dest.
		/// </summary>
		/// <param name="source">Object to get parameter values from.</param>
		/// <param name="Cmd">Command to copy parameter values to.</param>
		/// <returns>The Command object.</returns>
		public static IDbCommand CopyTo(Object source, IDbCommand Cmd) {
			return CopyTo(source, Cmd, true);
		}

		/// <summary>
		/// Copies the values of fields to the matching parameters of a db command
		/// object representing a stored procedure with named parameters.
		/// Loop on dest, return dest.
		/// </summary>
		/// <param name="source">Object to get parameter values from.</param>
		/// <param name="Cmd">Command to copy parameter values to.</param>
		/// <param name="DeclaredOnly">True if only declared members to be copied; false if inherited also</param>
		/// <returns>The Command object.</returns>
		public static IDbCommand CopyTo(Object source, IDbCommand Cmd, bool DeclaredOnly) {
			if (Cmd.Parameters.Count==0) Cache.GetParameters(Cmd);
			foreach (MemberInfo i in GetOType(source).GetMembers((DeclaredOnly)?LookAt1:LookAt2)) 
				Cache.SetValue(Cmd.Parameters, "@"+i.Name, GetValue(source, i));
			return Cmd;
		}

		/// <summary>
		/// Returns a DataTable generated from source
		/// </summary>
		/// <param name="source">The collection to convert.</param>
		/// <returns>DataTable.</returns>
		public static DataTable CopyFrom(IList source) {
			return CopyFrom(source, true);
		}

		/// <summary>
		/// Returns a DataTable generated from source
		/// </summary>
		/// <param name="source">The collection to convert.</param>
		/// <param name="DeclaredOnly">True if only declared members to be copied; false if inherited also</param>
		/// <returns>DataTable.</returns>
		public static DataTable CopyFrom(IList source, bool DeclaredOnly) {
			Type T = GetListBaseType(source);
			DataTable Result = new DataTable(T.Name);
            //67811A0  - PCI Remediation for Payment systems - CH1 -start added try catch block to enable logging to identify the paramters causing the data type mismatch by cognizant on 01/09/2011
            //Begin PC Phase II changes CH1 - Added the below code for handling Exceptions. For ArrayOfObjects method chnaged the first parameter from int to DataTable
            try
            {
                foreach (MemberInfo i in T.GetMembers((DeclaredOnly) ? LookAt1 : LookAt2))
                {
                    //Logger.Log("Column ::: Name: " + i.Name + " IsPropertyOrField :::  " + IsPropertyOrField(i) + Environment.NewLine);
                    if (IsPropertyOrField(i))
                    {
                        Result.Columns.Add(i.Name, GetMemberType(i));
                        //Logger.Log("Added Column::: " + i.Name + " Added Column Type::: " + Convert.ToString(GetMemberType(i)) + Environment.NewLine);
                    }
                }
                for (int i = 0; i < source.Count; i++)
                {
                    //Logger.Log("Source logging - " + source[i].ToString() + source[i].GetType());
                    Result.Rows.Add(ArrayOfObjects(Result, T, source[i], DeclaredOnly));                    
                }
                   
            }
            catch (Exception e)
            {
                Logger.Log("Stack Trace:" + e.Message + e.StackTrace + e.InnerException);
            }
            //End PC Phase II changes CH1 - Added the below code for handling Exceptions.               
            //67811A0  - PCI Remediation for Payment systems - CH1 -END added try catch block to enable logging to identify the paramters causing the data type mismatch by cognizant on 01/09/2011
			return Result;
		}

		/// <summary>
		/// Gets the parameters for Cmd and puts them into the cache.  This
		/// allows for pre-caching of parameters before they are needed, such
		/// as if the command is to participate in a transaction.
		/// </summary>
		/// <param name="Cmd">The command to get parameters for.</param>
		public static void CacheParameters(IDbCommand Cmd) {
			Cache.GetParameters(Cmd);
		}

		/// <summary>
		/// Returns a Javascript compatible string representation of Reader
		/// </summary>
		/// <param name="Reader">The recordset to convert.</param>
		/// <returns>Javascript string.</returns>
		public static string ToJsString(IDataReader Reader) {
			try {
				object[] O = new Object[Reader.FieldCount];
				StringBuilder dest = new StringBuilder("[");
				while (Reader.Read()) {
					Reader.GetValues(O);
					if (dest.Length>1) dest.Append(',');
					dest.Append(ToJsString(O));
				}
				dest.Append(']');
				return dest.ToString();
			} finally {
				Reader.Close();
			}
		}

		/// <summary>
		/// Returns a Javascript compatible string representation of O
		/// </summary>
		/// <param name="O">The array to convert.</param>
		/// <returns>Javascript string.</returns>
		public static string ToJsString(object[] O) {
			StringBuilder dest = new StringBuilder("[");
			for (int i=0; i<O.Length; i++) {
				if (dest.Length>1) dest.Append(',');
				dest.Append(ToJsString(O[i]));
			}
			dest.Append(']');
			return dest.ToString();
		}

		/// <summary>
		/// Returns a Javascript compatible string representation of O
		/// </summary>
		/// <param name="O">The object to convert.</param>
		/// <returns>Javascript string.</returns>
		public static string ToJsString(object O) {
			if (O==null) return "null";
			if (typeof(string).IsInstanceOfType(O))
				return "'" + O.ToString().Replace("'","\\'").Replace("\r","\\r").Replace("\n","\\n") + "'";
			if (typeof(DateTime).IsInstanceOfType(O))
				return "'" + ((DateTime)O).ToUniversalTime() + "'";
			return O.ToString();
		}

		#region Private Helper Methods
		internal static BindingFlags LookAt1 = BindingFlags.DeclaredOnly | BindingFlags.Public | BindingFlags.Instance;
		internal static BindingFlags LookAt2 = BindingFlags.Public | BindingFlags.Instance;

        //Begin PC Phase II changes CH2 - Added the below code to resolve Datatype Conversion Error by mapping the Source List to Destination DataSet by checking the name of the field.
		/// <summary>
		/// Helper method returns an array of Objects from O.
		/// </summary>
		/// <param name="Cols">Number of Columns to create.</param>
		/// <param name="T">The type to get values for.</param>
		/// <param name="O">The object to get the values from.</param>
		/// <param name="DeclaredOnly">True if only declared members to be copied; false if inherited also</param>
		/// <returns>Array with the values.</returns>
		internal static Object[] ArrayOfObjects(DataTable Cols, Type T, Object O, bool DeclaredOnly) {
			Object[] Result = new Object [Cols.Columns.Count];
			int j = 0;
            /*Alternative Fix 2- Conversion Error
            foreach (DataColumn dc in Cols.Columns)
            {
                foreach (MemberInfo i in T.GetMembers((DeclaredOnly) ? LookAt1 : LookAt2))
                {
                    if (IsPropertyOrField(i))
                    {
                        if (dc.ColumnName == i.Name)
                        {
                            Result[j] = GetValue(O, i);
                            j++;
                            break;
                        }
                    }
                }
            }*/
            foreach (MemberInfo i in T.GetMembers((DeclaredOnly) ? LookAt1 : LookAt2))
            {
                //Logger.Log("Column ::: Name: " + i.Name + " IsPropertyOrField :::  " + IsPropertyOrField(i) + Environment.NewLine);
                if (IsPropertyOrField(i))
                {
                    if (Cols.Columns[j].ColumnName == i.Name)
                    {
                        Result[j] = GetValue(O, i);
                        //Logger.Log("Result ::: Name: " + i.Name + " Value :::" + Convert.ToString(Result[j]) + " Type ::: " + Result[j].GetType() + Environment.NewLine);
                        j++;
                    }
                    else
                    {
                        //Alternative Fix 1 - Conversion Error
                        foreach (MemberInfo k in T.GetMembers((DeclaredOnly) ? LookAt1 : LookAt2))
                        {
                            if (IsPropertyOrField(k))
                            {
                                if (Cols.Columns[j].ColumnName == k.Name)
                                {
                                    Result[j] = GetValue(O, k);
                                    j++;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
			return Result;
		}
        //End PC Phase II changes CH2 - Added the below code to resolve Datatype Conversion Error by mapping the Source List to Destination DataSet by checking the name of the field.

		/// <summary>
		/// Helper method returns the property or field type of M
		/// </summary>
		/// <param name="M">The member to get the type of.</param>
		/// <returns>The type of the member.</returns>
		internal static Type GetMemberType(MemberInfo M) {
			if (M.MemberType == MemberTypes.Property) {
				return ((PropertyInfo)M).PropertyType;
			} else if (M.MemberType == MemberTypes.Field) {
				return ((FieldInfo)M).FieldType;
			} else return null;
		}

		/// <summary>
		/// Helper method attempts to determine the type of the default property of the IList and
		/// returns that Type.  Throws an exception if the type can't be determinied.
		/// </summary>
		/// <param name="C">The collection to check.</param>
		/// <returns>The type of the default property.</returns>
		internal static Type GetListBaseType(IList C) {
			return GetListBaseType(C.GetType());
		}

		/// <summary>
		/// Helper method attempts to determine the type of the default property of an IList type and
		/// returns that Type.  Throws an exception if the type can't be determinied.
		/// </summary>
		/// <param name="T">The type to check.</param>
		/// <returns>The type of the default property.</returns>
		internal static Type GetListBaseType(Type T) {
			MemberInfo [] M = T.GetDefaultMembers();
			if (M.Length==0) throw new Exception(T.Name + " is not a strongly-typed collection; can't determine base list type.");
			if (M[0].MemberType!=MemberTypes.Property)
				throw new Exception(T.Name + "'s default member is not a property; can't determine base list type.");
			return ((PropertyInfo)M[0]).PropertyType;
		}

		/// <summary>
		/// Helper method returns the type of O, unless O is a TemplateControl, in
		/// which case returns the underlying type of O.
		/// </summary>
		/// <param name="O">Object to get the type of.</param>
		/// <returns>Type of O.</returns>
		internal static Type GetOType(Object O) {
			Type T = O.GetType();
			return(typeof(TemplateControl).IsInstanceOfType(O))?T.BaseType:T;
		}

		/// <summary>
		/// Helper method returns a MemberInfo object from T called Name representing a
		/// field or a property if it exists.
		/// Throws an exception if there is more than one field or property member with this name.
		/// </summary>
		/// <param name="T">Type type to get the member from.</param>
		/// <param name="Name">The member name to get type of.</param>
		/// <returns>MemberInfo Object.</returns>
		internal static MemberInfo GetMember(Type T, string Name) {
			MemberInfo [] M = T.GetMember(Name);
			if (M.Length==0 || !IsPropertyOrField(M[0])) return null;
			if (M.Length>1) throw new Exception(T.Name + " has more than one member with the name " + Name);
			return M[0];
		}

		/// <summary>
		/// Helper method returns true if the M is a property or a field
		/// </summary>
		/// <param name="M">The member to check.</param>
		/// <returns>True if member is a property or field.</returns>
		internal static bool IsPropertyOrField(MemberInfo M) {
			return ((M.MemberType & (MemberTypes.Property | MemberTypes.Field)) != 0);
		}

		/// <summary>
		/// Helper method returns true if the M is a property or a field
		/// and if Property is both a read and write property.
		/// </summary>
		/// <param name="M">The member to check.</param>
		/// <returns>True if member is a property or field and if
		/// property, is both read and write.</returns>
		internal static bool IsRWPropertyOrField(MemberInfo M) {
			if ((M.MemberType & MemberTypes.Field)!=0) return true;
			if ((M.MemberType & MemberTypes.Property)==0) return false;
			return (((PropertyInfo)M).CanRead && ((PropertyInfo)M).CanWrite);
		}

        //Begin PC Phase II changes CH3 - Added the below code to retrieve the values by checking the Field name.
		/// <summary>
		/// Helper method gets the value from O represented by the property or field M.  Returns
		/// null if M isn't a property or field or M is null or is a write-only property.
		/// </summary>
		/// <param name="O">Object to get value from.</param>
		/// <param name="M">MemberInfo to get the value.</param>
		/// <returns>The value.</returns>
		internal static Object GetValue(Object O, MemberInfo M) {
			if (M==null) return null;
			if (M.MemberType==MemberTypes.Property) {
                //Logger.Log("GetValue - Property MemberType - " + ((((PropertyInfo)M).CanRead) ? (O.GetType().GetProperty(M.Name.ToString()).GetValue(O, null)) : "MYVALUE").ToString());
                return (((PropertyInfo)M).CanRead)?(O.GetType().GetProperty(M.Name.ToString()).GetValue(O, null)):null;
			//return (((PropertyInfo)M).CanRead)?((PropertyInfo)M).GetValue(O, null):null;
			} else if (M.MemberType==MemberTypes.Field) {
               // Logger.Log(("GetValue - Field Membertype - " + O.GetType().GetField(M.Name.ToString()).GetValue(O)).ToString());
                return O.GetType().GetField(M.Name.ToString()).GetValue(O);
				//return ((FieldInfo)M).GetValue(O);
			} return null;
		}
        //End PC Phase II changes CH3 - Added the below code to retrieve the values by checking the Field name.

		/// <summary>
		/// Helper Method sets the value of Property or Field M in O to V.  Throws
		/// an exception if M isn't a property or field.  Ignores M=null or M
		/// is a non-writable property.
		/// </summary>
		/// <param name="O">Object to set value of.</param>
		/// <param name="M">Member of object to set.</param>
		/// <param name="Value">Value to set.</param>
		internal static void SetValue(Object O, MemberInfo M, Object Value) {
			try {
				if (M==null) return;
				SimpleSetValue(O, M, Value);
			} catch (ArgumentException e) {
				object[] attrs = M.GetCustomAttributes(typeof(CustomCopyAttribute), true);
				if (attrs.Length>0 && HandleCustomCopy((CustomCopyAttribute)attrs[0], O, M, Value)) return;
				string DestType;
				if (M.MemberType==MemberTypes.Property) {
					DestType = ((PropertyInfo)M).PropertyType.Name;
				} else DestType = ((FieldInfo)M).FieldType.Name;
				throw new Exception(e.Message + "Setting [" + M.Name + "] in type " + O.GetType().Name +
					".  Source type: " + Value.GetType().Name + ", Dest type: " + DestType);
			}
		}

		internal static bool HandleCustomCopy(CustomCopyAttribute C, Object O, MemberInfo M, Object Value) {
			if (C.Ignore) return true;
			if (C.DeepCopy) 
				try {
					Object o = Construct(GetMemberType(M));
					if (typeof(SimpleSerializer).IsInstanceOfType(o)) {
						((SimpleSerializer)o).CopyFrom(Value);
						SimpleSetValue(O,M,o);
						return true;
					} else if (typeof(ArrayOfSimpleSerializer).IsInstanceOfType(o)) {
						((ArrayOfSimpleSerializer)o).CopyFrom((IList)Value);
						SimpleSetValue(O,M,o);
						return true;
					}
				}
                    //PC Security Defect Fix -CH1 START- Modified the below code to add logging inside the catch block 
                catch (Exception ex) { Logger.Log(ex.ToString()); }
            //PC Security Defect Fix -CH1 END- Modified the below code to add logging inside the catch block
			return false;
		}

		/// <summary>
		/// Helper Method sets the value of Property or Field M in O to V.  Throws
		/// an exception if M isn't a property or field.  Ignores M=null or M
		/// is a non-writable property.
		/// </summary>
		/// <param name="O">Object to set value of.</param>
		/// <param name="M">Member of object to set.</param>
		/// <param name="Value">Value to set.</param>
		internal static void SimpleSetValue(Object O, MemberInfo M, Object Value) {
			if (M.MemberType==MemberTypes.Property) {
				if (((PropertyInfo)M).CanWrite) ((PropertyInfo)M).SetValue(O, Value, null);
			} else if (M.MemberType==MemberTypes.Field) {
				((FieldInfo)M).SetValue(O, Value);
			} else throw new Exception(O.GetType().Name + " member " + M.Name + " is not a property or a field.");
		}

		#endregion

	}

	/// <summary>
	/// CustomCopyAttribute can be applied to any serializeable
	/// property or field and will determine how it is copied when
	/// there is not an exact type match.
	/// </summary>
	[AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
	public class CustomCopyAttribute : Attribute {
		///<summary/>
		public CustomCopyAttribute() : base () {
		}
		/// <summary>
		/// Ignore, when specified true will bypass the field if it can't be 
		/// copied.
		/// </summary>
		public bool Ignore = false;
		/// <summary>
		/// DeepCopy, when specified true will create a new object of
		/// the required type and copy that object.
		/// </summary>
		public bool DeepCopy = false;

	}


}
