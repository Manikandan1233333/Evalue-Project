using System;
using CSAAWeb;

/// <summary>
/// 
/// </summary>
public struct ADDRESS {
	public string Address1;
	public string Address2;
	public string City;
	public string StateCode;
	public string ZipCode;
	// assumes country = "US"

	public override string ToString() {
		return Address1 + "|" +
			Address2 + "|" +
			City + "|" + 
			StateCode + "|" +
			ZipCode;
	}
}

namespace CSAAActors
{
	/// <summary>
	/// Person class
	/// </summary>
	/// <remarks>
	/// Represents a generic person
	/// </remarks>
	public class Person : CSAAWeb.Serializers.SimpleSerializer 
	{
		///<summary/>
		protected override bool DeclaredOnly { get { return false; }}

		private string _sTitle			= string.Empty;
		private string _sFirstName		= string.Empty;
		private string _sMidInit		= string.Empty;
		private string _sLastName		= string.Empty;
		private ADDRESS _tAddress;
		private string _sEmail			= string.Empty;
		private string _sDayPhone		= string.Empty;
		private string _sEveningPhone	= string.Empty;

		// TODO: convert this to the appropriate data type
		private string _sBirthDate		= string.Empty;
		private string _sGender			= string.Empty;

		///<summary>Default contructor.</summary>
		public Person() : base() {}
		///<summary>Xml Constructor.</summary>
		public Person(string Xml,bool dummy) : base (Xml) {}
		///<summary>Contstructor from another object</summary>
		public Person(Object O) : base(O) {}

		/// <summary>
		/// Returns Xml string for object.
		/// </summary>
		public string ToString(bool Xml) {
			return base.ToString();
		}
		/// <summary>
		/// 
		/// </summary>
		public string Title	{
			get {return _sTitle;}
			set {_sTitle = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		public string FirstName	{
			get {return _sFirstName;}
			set {_sFirstName = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		public string LastName {
			get {return _sLastName;}
			set {_sLastName = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		public string MiddleInit {
			get {return _sMidInit;}
			set {_sMidInit = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		public string Email {
			get {return _sEmail;}
			set {_sEmail = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		public ADDRESS Address {
			get {return _tAddress;}
		}

		/// <summary>
		/// 
		/// </summary>
		public string Address1 {
			get {return _tAddress.Address1;}
			set {_tAddress.Address1= value;}
		}

		/// <summary>
		/// 
		/// </summary>
		public string Address2 {
			get {return _tAddress.Address2;}
			set {_tAddress.Address2 = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		public string City {
			get {return _tAddress.City;}
			set {_tAddress.City = value;}
		}

		/// <summary>
		/// Two-letter state code (CA, NV, UT)
		/// </summary>
		public string StateCode {
			get {return _tAddress.StateCode;}
			set {_tAddress.StateCode = value;}
		}

		/// <summary>
		/// 
		/// </summary>
		public string ZipCode {
			get {return _tAddress.ZipCode;}
			set {_tAddress.ZipCode = value;}
		}

		/// <summary>
		/// Daytime phone number
		/// </summary>
		public string DayPhone {
			get {return _sDayPhone;}
			set {_sDayPhone = value;}
		}

		/// <summary>
		/// Evening phone number
		/// </summary>
		public string EveningPhone {
			get {return _sEveningPhone;}
			set {_sEveningPhone = value;}
		}

		/// <summary>
		/// Date of birth
		/// </summary>
		public string BirthDate {
			// TODO: convert birthdate to a date type
			get {return _sBirthDate;}
			set {_sBirthDate = value;}
		}

		/// <summary>
		/// Gender
		/// </summary>
		/// <remarks>M, F</remarks>
		public string Gender {
			// TODO: validate this
			get {return _sGender;}
			set {_sGender = value;}
		}
	}
}
