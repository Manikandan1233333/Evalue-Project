// This contains classes useful for serialization of data to pass from one form
// to the next.
//Security Defect - Returns true if input string contains junk characters.
using System;
using System.Data;
using System.Collections;
using System.Reflection;
using System.Xml.Serialization;

namespace CSAAWeb.Serializers
{
	/// <summary>
	/// SimpleSerializer is a base class for messaging classes that are used to transport
	/// data between application tiers.  It provides three primary capabilities:
	/// 1) Ability to serialize into and deserialize from XML strings.  2) Ability to
	/// Copy from and to other unrelated types that have properties or fields with matching
	/// names. 3) Ability to copy from a dataset with matching field names and to
	/// stored procedure command objects with matching parameter names.
	/// </summary>
	[Serializable]
	[XmlType(IncludeInSchema=false)]
	public class SimpleSerializer 
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public SimpleSerializer() {}
		/// <summary>
		/// Constructs SimpleSerializer with properties/fields from O.
		/// </summary>
		/// <param name="O">Object to copy properties/fields from.</param>
		public SimpleSerializer(Object O) { CopyFrom(O); }
		/// <summary>
		/// Constructs SimpleSerializer from an XML string.
		/// </summary>
		/// <param name="Xml">The XML string.</param>
		public SimpleSerializer(string Xml) 
		{
			if (Xml!=null && Xml!="") LoadXML(Xml);
		}

		/// <summary>
		/// Derived classes can override this to allow copying inherited fields and properties.
		/// </summary>
		protected virtual bool DeclaredOnly { get { return true; }}
		/// <summary>Loads properties from an XML string.</summary>
		protected bool LoadXML(string xml) {
			object NewO = Serializer.FromXML(this.GetType(), xml);
			if (NewO==null) return false;
			Serializer.CopyFrom(NewO, this, DeclaredOnly);
			return true;
		}
		/// <summary>Returns true if s is blank or null.</summary>
		public static bool IsMissing(string s) {return Serializer.IsMissing(s);}
		/// <summary>Security Defect - Returns true if input string contains junk characters.</summary>
        public bool junkValidation(string input)
        {
            char[] junkChar = "|&;$%@’”<>()+\n\r\'\"".ToCharArray();
            return (input.IndexOfAny(junkChar) > -1);
        }
		        /// <summary>Serializes the object into an XML string</summary>
		public override string ToString() {return Serializer.ToXML(this);}

		/// <summary>
		/// Copies the values of properties/fields of O into the matching fields in this.
		/// </summary>
		/// <param name="O">Object to copy properties/fields from.</param>
		public virtual void CopyFrom(Object O)
		{
			Serializer.CopyFrom(O, this, DeclaredOnly);
		}
		
		/// <summary>
		/// Copies the values of properties of Reader into the matching fields in this.
		/// Loops on this.
		/// </summary>
		/// <param name="Reader">DataReader to copy fields from.</param>
		public void CopyFrom(IDataReader Reader)
		{
			Serializer.CopyFrom(Reader, this);
		}
		
		/// <summary>
		/// Copies the values of fields of this into the matching properties of O.
		/// Loops on this.
		/// </summary>
		/// <param name="O">Object to copy properties/fields to.</param>
		public void CopyTo(Object O)
		{
			Serializer.CopyTo(this, O, DeclaredOnly);
		}

		/// <summary>
		/// Copies the values of fields to the matching parameters of a db command
		/// object representing a stored procedure with named parameters.
		/// </summary>
		/// <param name="Cmd">The database command object who's parameters will be set.</param>
		public void CopyTo(IDbCommand Cmd) 
		{
			Serializer.CopyTo(this, Cmd, DeclaredOnly);
		}

	}

	/// <summary>
	/// Base class for collection of message objects.  Provides these fundamental capabilities:
	/// 1) Ability to serialize into and deserialize from XML strings.  2) Ability to
	/// Copy from and to other unrelated collections with elements that have properties or fields with matching
	/// names to the Message class contained herein. 3) Ability to copy from a dataset
	/// with rows that have matching fields.
	/// </summary>
	[Serializable]
	[XmlType(IncludeInSchema=false)]
	public class ArrayOfSimpleSerializer : CollectionBase
	{
		/// <summary>
		/// Default constructor.
		/// </summary>
		public ArrayOfSimpleSerializer() {}

		/// <summary>
		/// Constructor that accepts an xml string.
		/// </summary>
		/// <param name="Xml">The XML string.</param>
		public ArrayOfSimpleSerializer(string Xml)
		{
			if (Xml!=null && Xml!="") LoadXML(Xml);
		}

		/// <summary>
		/// Constructor that accects another list.  Copies items from that list.
		/// </summary>
		/// <param name="source">The collection to copy items from.</param>
		public ArrayOfSimpleSerializer(IList source) 
		{
			CopyFrom(source);
		}

		/// <summary>
		/// Derived classes can override this to allow copying inherited fields and properties.
		/// </summary>

		protected virtual bool DeclaredOnly { get { return true; }}
		/// <summary>Loads properties from an XML string.</summary>
		protected bool LoadXML(string xml) {
			ArrayOfSimpleSerializer NewO = (ArrayOfSimpleSerializer)Serializer.FromXML(this.GetType(), xml);
			if (NewO==null) return false;
			foreach (object item in NewO) this.Add(item);
			foreach (PropertyInfo i in this.GetType().GetProperties()) {
				if (i.Name!="Item" && i.Name!="Count" && i.GetValue(NewO,null)!=null)
					try {i.SetValue(this, i.GetValue(NewO, null), null);}
					catch (Exception e) {if (e.Message!="Property set method not found.") throw;}
			}
			return true;
		}
		/// <summary>Returns true if s is blank or null.</summary>
		public static bool IsMissing(string s) {return Serializer.IsMissing(s);}
		/// <summary>Serializes the object into an XML string</summary>
		public override string ToString() {return Serializer.ToXML(this);}
		/// <summary>Adds item to the collection.</summary>
		internal void Add(object item) {InnerList.Add(item);}

		/// <summary>
		/// Adds item to the collection.
		/// </summary>
		/// <param name="item">The item to add.</param>
		public void Add(SimpleSerializer item)
		{
			InnerList.Add(item);
		}

		/// <summary>
		/// Fills collection with records from reader.
		/// </summary>
		/// <param name="Reader">The dataset to copy items from.</param>
		public void CopyFrom(IDataReader Reader) 
		{
			Serializer.CopyFrom(Reader, this);
		}

		/// <summary>
		/// Fills collection with records from Table.
		/// </summary>
		/// <param name="Table">The dataset to copy items from.</param>
		public void CopyFrom(DataTable Table) {
			Serializer.CopyFrom(Table, this);
		}

		/// <summary>
		/// Fills collection with records from source.
		/// </summary>
		/// <param name="source">The collection to copy items from.</param>
		public virtual void CopyFrom(IList source)
		{
			Serializer.CopyFrom(source, this, DeclaredOnly);
		}

		/// <summary>
		/// Gets or sets the item at index.
		/// </summary>
		public SimpleSerializer this[int index] {
			get {return (SimpleSerializer) InnerList[index];}
			set {InnerList[index] = value;}
		}

		/// <summary>
		/// Returns a DataTable representation of this collection.
		/// </summary>
		public DataTable Data {get {return Serializer.CopyFrom(this, DeclaredOnly);}}

		///<summary>Sorts this list on the selected field.</summary>
		public ArrayOfSimpleSerializer Sort(string Field) {
			object[] A = this.InnerList.ToArray();
			Array.Sort(GetKeys(Field), A);
			this.InnerList.Clear();
			foreach (SimpleSerializer S in A) this.InnerList.Add(S);
			return this;
		}

		///<summary>Returns an array of keys for this using Field as the key.</summary>
		private object[] GetKeys(string Field) {
			ArrayList A = new ArrayList(this.Count);
			Type T = Serializer.GetListBaseType(this);
			System.Reflection.MemberInfo M = Serializer.GetMember(T, Field);
			foreach (SimpleSerializer S in this) A.Add(Serializer.GetValue(S, M));
			return A.ToArray();
		}

		/// <summary>Reverses the order of the elements.</summary>
		public void Reverse() {
			InnerList.Reverse();
		}

		///<summary>Appends the elements of Source.</summary>
		public void Append(ArrayOfSimpleSerializer Source) {
			foreach (SimpleSerializer S in Source) Add(S);
		}
	}

}
