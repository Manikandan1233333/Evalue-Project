using System;
using System.Data;
using CSAAWeb.Serializers;

namespace TurninClasses
{
	/// <summary>
	/// Class for holding a receipt number.
	/// </summary>
	public class Receipt : SimpleSerializer {
		/// <summary>
		/// Initializes a new instance of Receipt with default values.
		/// </summary>
		public Receipt() : base() {}
		/// <summary>
		/// The number of the receipt.
		/// </summary>
		public string Receipt_Number="";
	}
	/// <summary>
	/// Class for holding an array of receipts.
	/// </summary>
	public class ArrayOfReceipt : ArrayOfSimpleSerializer {
		/// <summary>
		/// Initializes a new instance of ArrayOfReceipt with no members.
		/// </summary>
		public ArrayOfReceipt() : base() {}
		/// <summary>
		/// Initializes a new instance of ArrayOfReceipts from the reader.
		/// </summary>
		/// <param name="Reader">IDataReader to get the data from.</param>
		public ArrayOfReceipt(IDataReader Reader) : base() {
			CopyFrom(Reader);
		}

		/// <summary>
		/// Override of this property
		/// </summary>
		public new Receipt this[int i] {
			get {return (Receipt)InnerList[i];}
			set {InnerList[i]=value;}
		}
	}
}
