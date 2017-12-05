using System;
using System.Text.RegularExpressions;

namespace MSCRCore
{
	/// <summary>
	/// Summary description for Vaildators.
	/// </summary>
	public class Validators
	{
		/// <summary>
		/// 
		/// </summary>
		public Validators()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool HasSpecialChar(string s) {
			// TODO: regex probably a better way to do this...
			char [] arySpecials = new char [] {
				'~', '!', '@', '#', '$', '%', '^', '&', '*', 
				'(', ')', '=', '[', ']', '\\', '.', ',', ';', 
				'`', '_', '+', '{', '}', '|', ':', '"', '<', '>', '/'};

			bool bHasChr = false;
			if (s.Length > 0) {
				foreach (char c in arySpecials) {
					if (s.IndexOf(c) > -1) {
						bHasChr = true;
						break;
					}
				}
			}
			return bHasChr;
		}


		/// <summary>
		/// 
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		/// <example>
		/// '' valid email? False
		/// ' ' valid email? False
		/// 'alan_laya@csaa.com' valid email? True
		/// 'test.test@test.com' valid email? True
		/// 'x@x' valid email? False
		/// 'this is not a valid email' valid email? False
		/// 'alan.laya@buzzsaw.com' valid email? True
		///	'x@x.org' valid email? True
		///	'x@x.tv' valid email? True
		///	'test test@ester.com' valid email? False
		///	'test^email@yahoo.com' valid email? False
		///	'1234test@yahoo.com' valid email? True
		///	'xxxxxx@@yahoo.com' valid email? False
		///	'xxxxxx@yahoo..com' valid email? False
		///	'xxxxxx@yahoo.com.' valid email? False 
		/// </example>
		public static bool IsValidEmailAddress(string s) {
			// matches [asmith@mactec.com], [foo12@foo.edu], [bob.smith@foo.tv]
			// does not match:  [chuck], [@foo.com], [a@a]  

			Regex rgEmail = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$" );
			return rgEmail.Match(s).Success;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool IsValidCreditCard(string s) {
			System.Text.RegularExpressions.Regex regx = new System.Text.RegularExpressions.Regex("^[0-9]{16}$");
			return regx.IsMatch(s);
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static bool IsValidPhone(string p) {
			Regex rgPhone = new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}");
			return rgPhone.Match(p).Success;
		}

		/// <summary>
		/// Determines whether a given month/year combination is in the past
		/// </summary>
		/// <remarks>
		/// Since only month and year provided, calculates the days in month 
		/// to create a full date
		/// </remarks>
		/// <param name="mm"></param>
		/// <param name="yy"></param>
		/// <returns></returns>
		public static bool IsDatePassed(int mm, int yy) {

			int daysInMM = DateTime.DaysInMonth(yy, mm);
			DateTime dt = new DateTime(yy, mm, daysInMM);

			// if date in the past
			return (dt.CompareTo(DateTime.Now) < 0);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool IsAllChars(string s) {

			// don't bother if no string
			if (s.Trim().Length == 0)
				return false;

			// TODO: regex can probably do this better
			char [] aryChars = s.ToCharArray();
			foreach (char c in aryChars) {
				if (((c >= 'A') && (c <= 'Z' )) || ((c >= 'a') && (c <= 'z'))) {
				}
				else 
					return false;
			}
			return true;
		}

		/// <summary>
		/// Does the string contain all numerics?
		/// </summary>
		/// <remarks>
		/// Performs strict checking for all numbers (i.e. not for valid numbers)
		/// -123 or 123.5 will return false!
		/// </remarks>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool IsAllNumeric(string s) {

			// don't bother if no string
			if (s.Trim().Length == 0)
				return false;

			// TODO: regex can probably do this better
			char [] aryChars = s.ToCharArray();

			foreach (char c in aryChars) {
				if ((c >= '0') && (c <= '9' )) {
				}
				else 
					return false;
			}
			return true;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="l"></param>
		/// <returns></returns>
		public static bool IsPasswordValidLength(int l) {
			return (l < Constants.MIN_PASSWORD_LENGTH) || (l > Constants.MAX_PASSWORD_LENGTH);

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="pwd"></param>
		/// <returns></returns>
		public static bool IsPasswordValidLength(string pwd) {
			return IsPasswordValidLength(pwd.Length);
		}

		// TODO: implement this!
		public static int DateDiff(DateTime dt1, DateTime dt2) {
			return 0;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string FormatPhone(string s) {

			string sFormatted = string.Empty;

			// assume 3125551212
			if (s.Trim().Length == 10) {
				sFormatted = "(" + GetPhoneAreaCode(s) + ") "
					+ GetPhonePrefix(s) + "-"
					+ GetPhoneSuffix(s);
			}
			else
				sFormatted = s;

			return sFormatted;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="d"></param>
		/// <returns></returns>
		public static string FormatDate(string datestring, string formatspec) {

			try {
				if (datestring.Length > 0 ) {
					System.DateTime dt = System.DateTime.Parse(datestring);
					return dt.ToString(formatspec);
				}
				else {
					return string.Empty;
				}
			}
			catch {
				// assume that we'll get null dates... no biggie
				return string.Empty;
			}

		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string GetPhoneAreaCode(string s) {
			if (s.Length > 3) {
				return s.Substring(0, 3);
			}
				return s;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string GetPhonePrefix(string s) {
			if (s.Length > 6) {
				return s.Substring(2, 3);
			}
			else
				return s;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string GetPhoneSuffix(string s) {
			if (s.Length > 6) {
				return s.Substring(6, 4);
			}
			else
				return s;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="z"></param>
		/// <returns></returns>
		public static bool IsValidZip(string z) {
			Regex rgZip = new Regex(@"\d{5}(-\d{4})?");
			return rgZip.Match(z).Success;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool HasHTMLChars(string s) {
			// TODO: is this it?
			char [] arySpecials = new char [] {'<', '>'};

			bool bHasChr = false;
			if (s.Length > 0) {
				foreach (char c in arySpecials) {
					if (s.IndexOf(c) > -1) {
						bHasChr = true;
						break;
					}
				}
			}
			return bHasChr;
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool IsValidMemberIdLength(string s) {
			// membership number may be 8, 12, or 16 digits
			return ((s.Length == 8) || (s.Length == 12) || (s.Length == 16));
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string ToProperCase(string s) {
			return s.Substring(0,1).ToUpper() + s.Substring(1).ToLower();
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="datestring"></param>
		/// <returns></returns>
		public static bool IsValidDate(string datestring) {

			try {
				System.DateTime.Parse(datestring);
				return true;
			}
			catch {	return false; }
		}

	}
}
