using System;

namespace MSCRCore
{
	/// <summary>
	/// Holds messages that are displayed to the user, and system messages that are logged during errors
	/// </summary>
	public class MessageConstants
	{
		// User-friendly application messages.
		public const string LOGIN_LOCKOUT		=  "This account has been locked out. Please contact an administrator.";
		public const string LOGIN_INCORRECT		= "Login incorrect. Please try again.";
		public const string PASSWORD_RESET_DFLT = "Please change your password.";
		public const string PASSWORD_CHANGED_OK	= "Password was changed.";
		public const string PASSWORD_ERR_USERNM = "Password cannot be the same as your user name.";
		public const string PASSWORD_ERR_COMBO  = "Password must be a combination of letters and numbers.";
		public const string PASSWORD_ERR_REQ	= "Both password fields are required.";
		public const string PASSWORD_ERR_NOMATCH= "Passwords do not match!";
		public const string ADMIN_NO_DELETE		= "This user cannot be deleted. Uncheck the Active flag to disable this user's account.";
		public const string APPLICATION_ERROR	= "APPLICATION ERROR. Please contact an administrator.";
		public const string MEMBER_DETAIL_VERIFY_BILLING = "Please verify the member's billing information below.";
		public const string MEMBER_DETAIL_CORRECT_ERRS = "Please correct any invalid or missing fields highlighted below.";
		public const string DOB_DEFAULT_PROMPT	= "Please supply the Date of Birth of associate(s) before continuing with renewal.";

		// System error messages.  Use codes to determine location in code!
		public const string ERROR_0000_PAYMENT_NO_CC		= "Unable to get credit cards from Payment service";
		public const string ERROR_0000_PAYMENT_NO_STATES	= "Unable to get states from Payment service";

		public MessageConstants()
		{
		}
	}
}
