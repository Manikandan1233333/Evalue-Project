/*
 * MAIG - CH1 - Added the logic to validate for 15 digits if it is an Amex card
 * MAIG - CH2 - Added the logic to validate for 15 digits if it is an Amex card
 * MAIG - CH3 - Added the logic to validate if the Last name in Name Search is valid using Regex
*/

using System;
using System.Text.RegularExpressions;

namespace CSAAWeb
{
	/// <summary>
	/// Summary description for Vaildators.
	/// </summary>
	public class Validate
	{
		///<summary/>
		static Validate() {
			rgEmail = new Regex(@"^([a-zA-Z0-9_\-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([a-zA-Z0-9\-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$", RegexOptions.Compiled );
			regx = new Regex("^[0-9]{16}$", RegexOptions.Compiled);
            //MAIG - CH1 - BEGIN - Added the logic to validate for 15 digits if it is an Amex card
            regxAmexCC = new Regex("^[0-9]{15}$", RegexOptions.Compiled);
            //MAIG - CH1 - END - Added the logic to validate for 15 digits if it is an Amex card
			AlphaNumeric1 = new Regex("[^a-z0-9]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
			rgPhone = new Regex(@"((\(\d{3}\) ?)|(\d{3}-))?\d{3}-\d{4}", RegexOptions.Compiled);
			RegexDecimal = new Regex("(?:^\\-{0,1}[0-9]+\\.{0,1}[0-9]*$|^\\-?[0-9]*\\.?[0-9]+$)", RegexOptions.Compiled);
		}
		///<summary/>
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


		private static Regex rgEmail;
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

			return rgEmail.Match(s).Success;
		}

		private static Regex regx;

		/// <summary>
		/// Returns true if the string contains a valid credit card number.
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static bool IsValidCreditCard(string s) {
			return regx.IsMatch(s);
		}

        //MAIG - CH2 - BEGIN - Added the logic to validate for 15 digits if it is an Amex card
        private static Regex regxAmexCC;
        /// <summary>
        /// Returns true if the string contains a valid AMEX credit card number.
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValidAmexCard(string s)
        {
            return regxAmexCC.IsMatch(s);
        }
        //MAIG - CH2 - ENd - Added the logic to validate for 15 digits if it is an Amex card

		private static Regex AlphaNumeric1;
		/// <summary>
		/// Returns true is s contains an alphanumeric sequence.
		/// </summary>
		public static bool IsAlphaNumeric(string s) {
			return !AlphaNumeric1.IsMatch(s);
		}

		private static Regex rgPhone;
		/// <summary>
		/// 
		/// </summary>
		/// <param name="p"></param>
		/// <returns></returns>
		public static bool IsValidPhone(string p) {
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

        //MAIG - CH3 - BEGIN - Added the logic to validate if the Last name in Name Search is valid using Regex
        private static Regex RegexLastName = new Regex(@"^[a-zA-Z]{1}[*a-zA-Z*]+$");
        /// <summary>
        /// Returns true if is a valid Last name
        /// </summary>
        public static bool isValidLastName(string s)
        {
            // don't bother if no string
            if (s.Trim().Length == 0)
                return false;
            return RegexLastName.IsMatch(s);
        }
        //MAIG - CH3 - END - Added the logic to validate if the Last name in Name Search is valid using Regex
		private static Regex RegexDecimal=new Regex(@"^[0-9]([\.\,][0-9]{1,3})?$");
		/// <summary>
		/// Returns true if s contains a valid decimal number.
		/// </summary>
		public static bool IsDecimal(string s) {
			if (s.Length==0) return true;
			if (s.Substring(0,1)=="-") 
				s=s.Substring(1);
			else if (s.IndexOf("-")==s.Length-1) s=s.Substring(0,s.Length-1);
			return RegexDecimal.IsMatch(s);
		}

		/// <summary>
		/// Returns true if the value is entirely numeric.
		/// </summary>
		/// <param name="s">The string to check</param>
		/// <returns>True if its numeric</returns>
		public static bool IsAllNumeric(string s) 
		{
			return IsAllNumeric(s, false);
		}
		/// <summary>
		/// Does the string contain all numerics?
		/// </summary>
		/// <remarks>
		/// Performs strict checking for all numbers (i.e. not for valid numbers)
		/// -123 or 123.5 will return false!
		/// </remarks>
		/// <param name="s">The string to check.</param>
		/// <param name="OnBlank">The value to return if its blank.</param>
		/// <returns>True if numeric.</returns>
		public static bool IsAllNumeric(string s, bool OnBlank) 
		{

			// don't bother if no string
			if (s.Trim().Length == 0)
				return OnBlank;

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

		// TODO: implement this!
		/// <summary>
		/// Supposed to return the time difference between two dates.
		/// Not yet implemented.
		/// </summary>
		/// <param name="dt1">Date 1</param>
		/// <param name="dt2">Date 2</param>
		/// <returns>Difference between Date 1 and Date 2</returns>
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
		/// Reformats a date string to an specific format.
		/// </summary>
		/// <param name="datestring">The date string to reformat</param>
		/// <param name="formatspec">The format spec to use.</param>
		/// <returns>string</returns>
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
            if (z.Length == 10 && CSAAWeb.Validate.IsAllNumeric(z))
            {
                return false;
            }
            else
            {
                return rgZip.Match(z).Success;
            }
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
                if (datestring == "")
                {
                    return true;
                }

                 
				System.DateTime.Parse(datestring);
				return true;
			}
			catch {	return false; }
		}

	}
}
