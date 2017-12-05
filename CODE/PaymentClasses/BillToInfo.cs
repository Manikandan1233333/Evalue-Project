using System;
using System.Xml.Serialization;
using CSAAWeb;
using CSAAWeb.AppLogger;
using CSAAWeb.Serializers;

// CSAA.COM CH1: Removed Address1 from required field check.
//Security Defects - Added the below lines to validate the fields in the BillToInfo
//Security Defects -CH1,CH2 Commented the below line
//Security defects -CH3 -Removed junk validation in BillToInfo field
//Security Defects-CH4- Commented the required field check for lastname since Empty spaces are coming from EXG in this field.
namespace PaymentClasses
{
	/// <summary>
	/// BillToInfo encapsulates all the customer's billing information.  It does not contain
	/// credit card information or any order information.  It does contain a field for a
	/// signature of the card that is derived from the card but that cannot be used to 
	/// obtain card information, but can be used to determine from one transaction to 
	/// the next if the card has changed.
	/// </summary>
	public class BillToInfo : SimpleSerializer
	{
		///<summary/>
		const string EXP_MISSING_CONTACT	= "Missing first name, last name, email, or phone";
		///<summary/>
		const string EXP_MISSING_ADDRESS	= "Missing or incomplete address";

		///<summary/>
		private string _FirstName=string.Empty;
		///<summary/>
		private string _LastName=string.Empty;
		///<summary/>
		private string _Email=string.Empty;
		///<summary/>
		private string _Phone=string.Empty;
		///<summary/>
		private string _Address1=string.Empty;
		///<summary/>
		private string _Address2=string.Empty;
		///<summary/>
		private string _City=string.Empty;
		///<summary/>
		private string _State=string.Empty;
		///<summary/>
		private string _Zip=string.Empty;
		///<summary/>
		private string _Country=string.Empty;
		///<summary/>
		private string _Currency=string.Empty;
		///<summary/>
		private string _Signature=string.Empty;
		///<summary/>
		private bool _IgnoreAVS=false;

		///<summary/>
		private static string Default_Country=Config.Setting("PAYMENT_DEFAULT_COUNTRY");
		///<summary/>
		private static string Default_Currency=Config.Setting("PAYMENT_DEFAULT_CURRENCY");

		/// <summary>
		/// default no arg constructor
		/// </summary>
		public BillToInfo() : base () {}
		
		/// <summary>
		/// Constructor to reconstruct from XML
		/// </summary>
		public BillToInfo(string xml) : base(xml) {}
		/// <summary>
		/// Constructor to get fields from another object.
		/// </summary>
		public BillToInfo(Object O) : base(O) {}

		/// <summary>
		/// Constructor with all the arguments
		/// </summary>
		public BillToInfo(string FirstName, string LastName, string Email, string Phone,
			string Address1, string Address2, string City, string State, string Zip,
			string Country, string Currency)
		{
			_FirstName=FirstName;
			_LastName=LastName;
			_Email=Email;
			_Phone=Phone;
			_Address1=Address1;
			_Address2=Address2;
			_City=City;
			_State=State;
			_Zip=Zip;
			_Country=Country;
			_Currency=Currency;
		}

		// Public properties

		///<summary/>
		[XmlAttribute]
		public string LastName 
		{
			get {return _LastName;}
			set {_LastName = value;}
		}
		///<summary/>
		[XmlAttribute]
		public string FirstName 
		{
			get {return _FirstName;}
			set {_FirstName = value;}
		}
		///<summary/>
		[XmlAttribute]
		public string Email 
		{
			get {return _Email;}
			set {_Email = value;}
		}
		///<summary/>
		[XmlAttribute]
		public string Phone 
		{
			get {return _Phone;}
			set {_Phone = value;}
		}
		///<summary/>
		[XmlAttribute]
		public string Address1 
		{
			get {return _Address1;}
			set {_Address1 = value;}
		}
		///<summary/>
		[XmlAttribute]
		public string Address2 
		{
			get {return _Address2;}
			set {_Address2 = value;}
		}
		///<summary/>
		[XmlAttribute]
		public string City 
		{
			get {return _City;}
			set {_City = value;}
		}
		///<summary/>
		[XmlAttribute]
		public string State 
		{
			get {return _State;}
			set {_State = value;}
		}
		///<summary/>
		[XmlAttribute]
		public string Zip 
		{
			get {return _Zip;}
			set {_Zip = value;}
		}
		///<summary/>
		[XmlAttribute]
		public string Country 
		{
			get {return _Country;}
			set {_Country = value;}
		}
		///<summary/>
		[XmlAttribute]
		public string Currency 
		{
			get {return _Currency;}
			set {_Currency = value;}
		}
		///<summary/>
		[XmlAttribute]
		public bool IgnoreAVS
		{
			get {return _IgnoreAVS;}
			set {_IgnoreAVS = value;}
		}
		
	// Public Methods

	/// <summary>
	/// Ensures that all required fields are present.
	/// </summary>
		public CCResponse ValidateFields()
		{
            //Security Defect - Added the below code to trim all the fields
            FirstName = FirstName.Trim();
            LastName = LastName.Trim();
            City = City.Trim();
            Zip = Zip.Trim();
            Email = Email.Trim();
            State = State.Trim();
            Address1 = Address1.Trim();
            Address2 = Address2.Trim();
            Country = Country.Trim();
            //Security Defect - Added the below code to trim all the fields
            CCResponse c = new CCResponse();
		//Security Defects - START - Added the below lines to validate the fields in the BillToInfo
		if (IsMissing(FirstName))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "FirstName";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_FIRSTNAME");
                Logger.Log(c.Message + c.Flag);
                return c;          
               

            }
            //Security Defects- CH4 -Commented the required field check for lastname since Empty spaces are coming from EXG in this field.
            //else if (IsMissing(LastName))
            //{
            //    c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "LastName";
            //    c.ActualMessage = c.Message;
            //    c.Flag = Config.Setting("ERRCDE_LASTNAME");
            //    Logger.Log(c.Message + c.Flag);
            //    return c; 
            //}
        //Security Defects-CH4 - Commented the required field check for lastname since Empty spaces are coming from EXG in this field.
            else if (IsMissing(City) || (City.Length > 25) || junkValidation(City))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "City";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_CITY");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            else if (IsMissing(Zip) || (Zip.Length > 10) || junkValidation(Zip)||!CSAAWeb.Validate.IsValidZip(Zip))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "Zip";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_CITY");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
        else if ((Email.Length > 90) || (Email!="" &&!CSAAWeb.Validate.IsValidEmailAddress(Email)))
        {
            c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "Email";
            c.ActualMessage = c.Message;
            c.Flag = Config.Setting("ERRCDE_EMAIL");
            Logger.Log(c.Message + c.Flag);
            return c;
        }
            else if (IsMissing(State) || (State.Length > 2) || junkValidation(State))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "State";
                c.ActualMessage = c.Message;
                c.Flag = Config.Setting("ERRCDE_STATE");
                Logger.Log(c.Message + c.Flag);
                return c;
            }
            //Security defects -Ch3-Removed junk validation in BillToInfo field
        else if (IsMissing(Address1) || (Address1.Length > 40))
            {
                c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "Address1";
                c.Flag = Config.Setting("ERRCDE_ADDRESS1");
                c.ActualMessage = c.Message;
                Logger.Log(c.Message + c.Flag);
                return c;
            }
        //Security defects -Ch3-Removed junk validation in BillToInfo field
        else if ((Address2.Length > 40))
        {
            c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "Address2";
            c.ActualMessage = c.Message;
            c.Flag = Config.Setting("ERRCDE_ADDRESS2");
            return c;
        }
        else if ((Country.Length > 2) || junkValidation(Country))
        {
            c.Message = CSAAWeb.Constants.ERR_AUTHVALIDATION + "Country";
            c.ActualMessage = c.Message;
            c.Flag = Config.Setting("ERRCDE_COUNTRY");
            return c;
        }
        //Security Defects -CH1 - END- Added the below lines to validate the fields in the BillToInfo
        /*Security Defects - CH2 - sTART - Commented the below code
    else if (IsMissing(FirstName) || IsMissing(LastName)) 
        {
            Logger.Log("Field missing, FirstName=" + FirstName + ", LastName=" + LastName);
            return null;
            throw new BusinessRuleException(EXP_MISSING_CONTACT);
        }

        // CSAA.COM CH1:START- Removed Address1 from required field check .
        // if (IsMissing(Address1) || IsMissing(City) || IsMissing(State) || IsMissing(Zip)) 
        else if ( IsMissing(City) || IsMissing(State) || IsMissing(Zip)) 
        {
            //Logger.Log("Field missing, Address1=" + Address1 + ", City=" + City + ", State=" + State + ", Zip=" + Zip);
            Logger.Log("Field missing,  City=" + City + ", State=" + State + ", Zip=" + Zip);
            return null;
            
            throw new BusinessRuleException(EXP_MISSING_ADDRESS);
        }
        // CSAA.COM CH1:END-//Security Defects - CH2 - Commented the below code */
        else if (IsMissing(Country))
        {
            _Country = Default_Country;
            return null;
        }
        else if (IsMissing(Currency))
        {
            _Currency = Default_Currency;
            return null;
        }
        else
        {
            return null;
        }
		}
	
	}
}
