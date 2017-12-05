/*
 * HISTORY :
 * STAR Retrofit II Changes: 
 * Modified as a part of CSR 5157
 * 2/5/2007 STAR Retrofit II.Ch1: Added code for the new variable declaration _RevenueType in the class LineItem.
 * 2/5/2007 STAR Retrofit II.Ch2: Modified the code to set read-write property for RevenueType in the class LineItem.
 * Security Defect -START - Added the below code to validate the fields in the lineitem
 */
using System;
using System.Collections;
using System.Xml.Serialization;
using System.ComponentModel;
using CSAAWeb;
using CSAAWeb.AppLogger;
using CSAAWeb.Serializers;

namespace PaymentClasses
{
	/// <summary>
	/// Represents a collection of line items.
	/// </summary>
	/// <remarks>
	/// Collection can be instantiated from a delimited string (legacy) or xml string.
	/// </remarks>
	public class ArrayOfLineItem : ArrayOfSimpleSerializer {
		const string EXP_MISSING_PRODUCT = "Missing product information";

		///<summary/>
		public ArrayOfLineItem() : base() {}
		/// <summary>Constructor that accepts an xml string.</summary>
		public ArrayOfLineItem(string Xml) : base(Xml) {}
		/// <summary>Constructor to create object from another object.</summary>
		public ArrayOfLineItem(IList source) : base(source) {
			for (int i=0; i<Count; i++) this[i].LineItemNo = i;
		}

		/// <summary>Gets or sets the item at index.</summary>
		public new LineItem this[int index] {
			get {return (LineItem) InnerList[index];}
			set {InnerList[index] = value;}
		}

		/// <summary>Adds item to the collection.</summary>
        /// Changed to AddLi as a part of Migration 3.5
		public void AddLi(ILineItem item) {
			InnerList.Add(new LineItem(item));
		}
		/// <summary>
		/// Removes all the items matching TransactionType
		/// </summary>
		/// <param name="ProductName">The type of items to remove.</param>
		public void Clear(string ProductName) {
			for (int i=this.Count-1; i>=0; i--) 
				if (((ILineItem)InnerList[i]).ProductName==ProductName)
					InnerList.RemoveAt(i);
			for (int i=0; i<Count; i++) this[i].LineItemNo = i;

		}
		///<summary/>
		[XmlIgnore]
		public decimal Total {
			get {
				Decimal Result = 0;
				foreach (ILineItem i in this) Result += i.Amount;
				return Result;
			}
		}
		/// <summary>
		/// Appends the lines from source to this as LineItems.
		/// </summary>
		/// <param name="source"></param>
		public void Append(ArrayOfLineItem source) {
			foreach (ILineItem i in source) AddLi(i);
		}
	}

	/// <summary>
	/// A simple line item for orders or payments
	/// </summary>
	public interface ILineItem {
		/// <summary>LineItemNo - line item number property</summary>
		int LineItemNo {get; set;}
		/// <summary>ProductName - holds product name</summary>
		string ProductName {get; set;}
		/// <summary>Amount - holds a dollar amount for a single line item</summary>
		decimal Amount {get; set;}
		/// <summary>Quantity - holds a quantity for this line item</summary>
		int Quantity {get; set;}
	}

	/// <summary>
	/// Represents a line item for an order
	/// </summary>
	/// <remarks>
	/// Item can be instantiated from a delimited string (legacy) or xml string.
	/// </remarks>
	public class LineItem : ValidatingSerializer, ILineItem
	{
		//*************************************************************************
		//* ... MAGIC VALUES ... MAGIC VALUES ... MAGIC VALUES ... MAGIC VALUES 
		//*************************************************************************
		// per CyberSource spec, these are the fields in an "offer" (line item)
		private static string ICS_OFFER_DEFAULT_SKU	= Config.Setting("csOffer.DefaultSKU");
		private static string ICS_OFFER_DEFAULT_PRODCODE = Config.Setting("csOffer.DefaultProdCode");

		private string	_sSKU			= string.Empty;
		private string	_sProductCode	= string.Empty;

		/// <summary>LineItemNo - line item number property</summary>
		private int _LineItemNo = 0;
		/// <summary>ProductName - holds product name</summary>
		private string _ProductName = "";
		/// <summary>Amount - holds a dollar amount for a single line item</summary>
		private decimal _Amount = 0;
		/// <summary>Quantity - holds a quantity for this line item</summary>
		private int _Quantity = 0;
		//STAR Retrofit II.Ch1 - START : Declared a new variable to retrieve the value of RevenueType.
		private string _RevenueType = "";
        //STAR Retrofit II.Ch1 - END


        ////test PCI
        //private string _DueDate = "";


		///<summary/>
		public LineItem() : base (){}

		/// <summary>
		/// Constructor that accepts an xml string
		/// </summary>
		public LineItem(string st) : base (st) {}

		/// <summary>
		/// Constructor to construct from another object.
		/// </summary>
		public LineItem(Object O) : base(O) {}
		/// <summary>
		/// Constructor with some common attributes
		/// </summary>
		public LineItem(int LineItemNo, string ProductName, decimal Amount, int Quantity) {
			this.LineItemNo	= LineItemNo;
			this.ProductName	= ProductName;
			this.Amount		= Amount;
			this.Quantity		= Quantity;
		}

		/// <summary>LineItemNo - line item number property</summary>
		[XmlAttribute]
		public int LineItemNo {get {return _LineItemNo;} set { _LineItemNo=value;}}

		
		/// <summary>Description - fills product name</summary>
		[XmlIgnore]
		public string Description {get {return ProductName;} set {ProductName=value;}}

		/// <summary>ProductName - holds product name</summary>
		[XmlAttribute]
		public string ProductName {get {return _ProductName.Trim();} set {_ProductName = value;}}

		/// <summary>SKU - holds stock keeping unit for a line item</summary>
		[XmlAttribute]
		public string SKU {
			get {return (_sSKU.Length > 0)?_sSKU:ICS_OFFER_DEFAULT_SKU;}
			set {_sSKU = value;}
		}

		/// <summary>ProductCode - holds a product code</summary>
		[XmlAttribute]
		public string ProductCode {
			get {return (_sProductCode.Length > 0)?_sProductCode:ICS_OFFER_DEFAULT_PRODCODE;}
			set {_sProductCode = value;}
		}

		/// <summary>Amount - holds a dollar amount for a single line item</summary>
		[XmlAttribute]
		public decimal Amount { get {return _Amount;} set {_Amount=value;}}

		/// <summary>Tax_Amount - holds a dollar amount for the total tax to apply to the product</summary>
		[XmlAttribute]
		public decimal Tax_Amount = 0;

		/// <summary>Quantity - holds a quantity for this line item</summary>
		[XmlAttribute]
		public int Quantity { get {return _Quantity;} set { _Quantity = value;}}

		/// <summary>Account holder's first name</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string FirstName = "";

		/// <summary>Account holder's last name</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string LastName = "";

		/// <summary>Account number of line item</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string AccountNumber = "";

		/// <summary>Account number for a membership line item</summary>
		[XmlIgnore]
		[DefaultValue("")]
		public string MemberID {
			get {return AccountNumber;} 
			set {AccountNumber=value;}
		}

		/// <summary>Account number for an insurance line item</summary>
		[XmlIgnore]
		[DefaultValue("")]
		public string Policy {
			get {return AccountNumber;} 
			set {AccountNumber=value;}
		}
		/// <summary>Revenue type for this line item</summary>
		[XmlAttribute]
		[DefaultValue("")]
		//STAR Retrofit II.Ch2 - START : Modified the code to set read-write property for RevenueType
		public string RevenueType {get {return _RevenueType;} set {_RevenueType = value;}}
		//STAR Retrofit II.Ch2 - END

		/// <summary>Revenue code for this line item</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string RevenueCode = "";

		/// <summary>Sub product code for this line item</summary>
		[XmlAttribute]
		[DefaultValue("")]
		public string SubProduct = "";
		
		/// <summary>Club code for this line item</summary>
		[XmlAttribute]
		[DefaultValue("005")]
		public string ClubCode = "005";

        //test
        [XmlAttribute]
        [DefaultValue("test")]
        private string DueDate = "test";
		
		//Added by Cognizant 2/15/2005 For override product type property using orderclasses.lineitem.ProductTypeNew 
		//Product code attribute is overridden with the product type available from orderclasses.lineitem.ProductTypeNew 
		/// <summary>
		/// To store the product type of the line item
		/// </summary>
		public string ProductTypeNew
		{
			get {return ProductCode;} 
			set {ProductCode=value;}
		}
        //Security Defect -START - Added the below code to validate the fields in the lineitem
        public CCResponse ValidateFields()
        {
            //Security Defect - Added the below code to trim all the fields
            ProductCode = ProductCode.Trim();
            ProductName = ProductName.Trim();
            ClubCode = ClubCode.Trim();
            SubProduct = SubProduct.Trim();
            AccountNumber = AccountNumber.Trim();
            LastName = LastName.Trim();
            FirstName = FirstName.Trim();
            SKU = SKU.Trim();
            RevenueCode = RevenueCode.Trim();
            RevenueType = RevenueType.Trim();
            //Security Defect - Added the below code to trim all the fields
            CCResponse c = new CCResponse();

            if ((ProductCode.Length > 10) || junkValidation(ProductCode))
            {
               c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "ProductCode";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_PRODUCTCODE");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            if ((ProductName.Length > 50) || junkValidation(ProductName))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "ProductName";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_PRODUCTNAME");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            if ((ClubCode.Length > 50) || junkValidation(ClubCode))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "ClubCode";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_CLUBCODE");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            if ((SubProduct.Length > 25) || junkValidation(SubProduct))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "SubProduct";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_SUBPRODUCT");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            if (IsMissing(AccountNumber) || (AccountNumber.Length > 25) || junkValidation(AccountNumber))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "AccountNumber";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_ACCOUNTNUMBER");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            //if (IsMissing(LastName))
            //{
            //    c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "LastName";
            //    c.ActualMessage = c.Message;
            //    c.Flag = Config.Setting("ERRCDE_LASTNAME");
            //    Logger.Log(c.Message + c.Flag);
            //    return c;
            //}
            //if (IsMissing(FirstName))
            //{
            //    c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "FirstName";
            //    c.ActualMessage = c.Message;
            //    c.Flag = Config.Setting("ERRCDE_FIRSTNAME");
            //    Logger.Log(c.Message + c.Flag);
            //    return c;
            //}
            if ((SKU.Length >3) || junkValidation(SKU))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "SKU";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_SKU");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            if ((RevenueCode.Length > 10) || junkValidation(RevenueCode))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "RevenueCode";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_REVENUECODE");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            if (IsMissing(RevenueType) || (RevenueType.Length > 20) || junkValidation(RevenueType))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "RevenueType";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_REVENUETYPE");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            if ((Amount < 0) || (Amount > 25000)|| junkValidation(Amount.ToString()))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "Amount";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_AMOUNT");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            if ((Tax_Amount <0) || (Tax_Amount > 25000) || junkValidation(Tax_Amount.ToString()))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "Tax_Amount";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_TAXAMOUNT");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            //if ((LineItemNo > 10) || !CSAAWeb.Validate.IsAllNumeric(LineItemNo.ToString()))
            //{
            //    c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "LineItemNo";
            //    c.ActualMessage = c.Message;
            //    c.Flag = Config.Setting("ERRCDE_LINEITEMNO");
            //    Logger.Log(c.Message + c.Flag);
            //    return c;
            //}
            if ((Quantity > 10) || !CSAAWeb.Validate.IsAllNumeric(Quantity.ToString()))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "Quantity";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_QUANTITY");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            else
            {
                return null;
            }
            //Security Defect -END - Added the below code to validate the fields in the lineitem
        }


	}
}
