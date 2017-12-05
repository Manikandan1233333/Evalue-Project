using System;
using System.Data.SqlClient;
/*
 * History:
 * 10/29/04 JOM Added Level property, new constructor and BusinessRuleLevel Enum.
*/

namespace CSAAWeb
{
	/// <summary>
	/// Used for non-critical (application) exceptions that are required by
	/// business rules but that do not need to be logged by automatic logging
	/// faclilties.
	/// </summary>
	public class BusinessRuleException : Exception
	{
		private string _Message = "";
		private BusinessRuleLevel _Level=BusinessRuleLevel.Warning;

		/// <summary>
		/// Gets the BusinessRuleLevel for this exception.
		/// </summary>
		public BusinessRuleLevel Level {get {return _Level;}}
		///<summary>Creates a new instance of BusinessRuleException with message and Level=Warning</summary>
		///<param name="Message">The error message;</param>
		public BusinessRuleException(string Message)
		{
			_Message = Message;
		}
		///<summary>Creates a new instance of BusinessRuleException with message and Level</summary>
		///<param name="Message">The error message;</param>
		///<param name="Level">The BusinessRuleLevel of this exception.</param>
		public BusinessRuleException(string Message, BusinessRuleLevel Level) {
			_Message = Message;
			_Level = Level;
		}
		///<summary>Creates a new instance of from an SqlException</summary>
		///<param name="Exception">The original SqlException</param>
		///<remarks>Level will be Warning if Exception.Class&lt;16) otherwise Severe</remarks>
		public BusinessRuleException(SqlException Exception) {
			_Message = Exception.Message;
			_Level = (Exception.Class<16)?BusinessRuleLevel.Warning:BusinessRuleLevel.Severe;
		}
		///<summary>The actual message of the exception.</summary>
		public override string Message {get {return _Message;}}
	}

	/// <summary>
	/// Enum for the BusinessRuleException Level property.
	/// </summary>
	public enum BusinessRuleLevel {
		///<summary>This Exception is a warning, the transaction may be recoverable with corrective action.</summary>
		Warning,
		///<summary>This Exception is severe, the transaction can not be recovered.</summary>
		Severe
	}
}
