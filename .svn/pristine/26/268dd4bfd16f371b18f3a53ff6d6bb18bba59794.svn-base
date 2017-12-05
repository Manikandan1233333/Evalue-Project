/*
 * 4/13/2010-MODIFIED BY COGNIZANT AS PART OF HO-6
 * HO-6.ch1:Added a new variable AppId
 * HO-6.ch2:Added a new method GetAppId() for getting AppId
 * HO-6.ch3:Added a a new method SetAppId() for setting the AppId
*/
using System;
using System.Web.UI.WebControls;
using System.Web.UI;
using System.Xml.Serialization;
using System.Collections;
using System.Configuration;
using System.ComponentModel;
using CSAAWeb;
using CSAAWeb.Serializers;
using AuthenticationClasses;
using PaymentClasses.Service;
using OrderClasses;

namespace SalesXReportClasses
{
	#region SalesXReportItem
	/// <summary>
	/// Summary description for SalesXReport Lineitem
	/// </summary>
	public class SalesXReportItem : ValidatingSerializer
	{
		public SalesXReportItem() : base() {}
		///<summary>Constructor from another object</summary>
		public SalesXReportItem(Object O) : base(O) {}
		///<summary>Xml Constructor</summary>
		public SalesXReportItem(string Xml) : base(Xml) {}

		protected override bool DeclaredOnly {get {return false;}}
		///<summary/>
		[XmlAttribute]
		public string PolicyNumber=string.Empty;
		///<summary/>
		[XmlAttribute]
		public string Status=string.Empty;
		///<summary/>
		[XmlAttribute]
		public DateTime TransactionDate;
		///<summary/>
		[XmlAttribute]
		public DateTime ProcessedDate;
		///<summary/>
	}
	#endregion

	#region ArrayOfSalesXReportItem
	/// <summary>
	/// Summary description for ArrayOfSalesXReportItem which contains a collection
	/// of SalesXReport Lineitems
	/// </summary>
	public class ArrayOfSalesXReportItem : ArrayOfValidatingSerializer 
	{
		///<summary>Default constructor</summary>
		public ArrayOfSalesXReportItem() : base() {}
		///<summary>Xml constructor</summary>
		public ArrayOfSalesXReportItem(string Xml) : base(Xml) {}
		///<summary>Constructor from another collection</summary>
		public ArrayOfSalesXReportItem(IList source) : base(source) {}
		///<summary>Constructor from dataset</summary>
		public ArrayOfSalesXReportItem(System.Data.DataSet DS) : base() 
		{
			this.CopyFrom(DS.Tables[0]);
		}

		protected override bool DeclaredOnly {get {return false;}}

		/// <summary>
		/// Gets or sets the item at index.
		/// </summary>
		public new SalesXReportItem this[int index] 
		{
			get {return (SalesXReportItem) InnerList[index];}
			set {InnerList[index] = value;}
		}

		/// <summary>
		/// Adds item to the collection.
		/// </summary>
		public void Add(SalesXReportItem item) 
		{
			InnerList.Add(item);
		}

	}
	#endregion

	#region SalesXReportCriteria
	/// <summary>
	/// Input Class containing the Search Criteria for SalesX Report
	/// </summary>
	public class SalesXReportCriteria : ValidatingSerializer 
	{
		///<summary>Default contructor.</summary>
		public SalesXReportCriteria() : base() {}
		///<summary>Xml Constructor.</summary>
		public SalesXReportCriteria(string Xml) : base (Xml) {}
		///<summary>Contstructor from another object</summary>
		public SalesXReportCriteria(Object O) : base(O) {}

		[XmlAttribute]
		///<summary/>
		public string StartDate;
		///<summary/>
		[XmlAttribute]
		public string EndDate;
		///<summary/>
		//Added by cognizant as a part of HO-6 on 04-13-2010.

		private string AppId;
		///<summary/>
		
		public string GetAppId()
		{
			return AppId;
		}

		public void SetAppId(string ApplicationId)
		{
			if(ApplicationId.Trim() != "")
			{
				AppId = ApplicationId;
			}
		}
	}
	#endregion

	#region SalesXReportResults
	/// <summary>
	/// SalesX Report Output class containing output results
	/// </summary>
	public class SalesXReportResults : ValidatingSerializer
	{
		public SalesXReportResults() : base() {}
		///<summary>Xml Constructor.</summary>
		public SalesXReportResults(string Xml) : base (Xml) {}
		///<summary>Contstructor from another object</summary>
		public SalesXReportResults(Object O) : base(O) {}

		public ArrayOfSalesXReportItem ReportResults = new ArrayOfSalesXReportItem();
		
	}
	#endregion
}