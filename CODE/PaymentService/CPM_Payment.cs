//Created by Cognizant on 07/04/2005 - Declaration of the Payment object and the assignment
//takes place here
//Modified by Cognizant on 09/15/2005 to Include the Capture Function Call.
//09/22/2005 Added the UserID in to the field ID_USER_SOURCE_NAME for the CPM

using System;
using ATLPAYMENTLib;
using PaymentClasses;
using CSAAWeb;

namespace CPM_PaymentService
{
	/// <summary>
	/// Summary description for CPM_Payment.
	/// </summary>
	public class CPM_Payment
	{
		#region Payment Constants Declaration
		
		//*    Transaction Type Ids
		//*********************************************
		///* Credit Card Functions */
		public const short ID_AUTHORIZATION    = 100 ; //Authorization
		//Added by Cognizant on 09/15/2005 to Invoke Capture Function Call immediately after Auth
		public const short ID_CAPTURE	= 102; //Capture
		///* Transcation Type Variable
		public string TransactionID;
		///
		
		//*********************************************
		//*    Field Ids
		//*********************************************
		///* Extended Customer Information */
		public const short  ID_CUSTOMER_PHONE   = 171 ; //Customer Phone Number
		public const short  ID_CUSTOMER_CITY   = 172 ; //Customer City
		public const short  ID_CUSTOMER_STATE   = 173 ; //Customer State
		public const short  ID_CUSTOMER_COUNTRY   = 174 ; //Customer Country
		public const short  ID_CUSTOMER_LAST_NAME   = 175 ; //Customer Last Name
		public const short  ID_CUSTOMER_FIRST_NAME   = 176 ; //Customer First Name
		public const short  ID_CUSTOMER_EMAIL   = 466 ; //Customer Email
		public const short  ID_CUSTOMER_STREET   = 151 ; //Customer Street
		public const short  ID_CUSTOMER_ZIP   = 152 ; //Customer Zip
		public const short  ID_CUSTOMER_STREET1   = 153 ; //Customer Street 1
		
		///* B e Group */
		public const short  ID_ACCOUNT_NUMBER   = 101 ; //Account Number
		public const short  ID_EXPIRATION_DATE   = 102 ; //Expiration Date
		public const short  ID_AMOUNT   = 103 ; //Amount
		public const short  ID_MERCHANT_ID   = 51 ; //Merchant Id
		public const short  ID_SEQUENCE_NUMBER   = 105 ; //Sequence Number
		public const short  ID_APPROVAL_CODE   = 106 ; //Approval Code
		public const short  ID_AUTH_RESPONSE_CODE   = 107 ; //Authorization Response Code
		public const short  ID_AUTH_RESPONSE_MESSAGE   = 108 ; //Authorization Response
		public const short  ID_RETURN_CODE_MESSAGE   = 109 ; //Return Code Message


		///* CVV Information */
		public const short  ID_CVV   = 165 ; //CVV
		public const short  ID_CVV_INDICATOR   = 167 ; //CVV Indicator
		public const short  ID_CARD_TYPE   = 104 ; //Card Type
		

		///* Level III Purch ing Card Line Item Detail Group */
		public const short  ID_ITEM_DESCRIPTION   = 10000 ; //Item Description
		public const short  ID_ITEM_PRODUCT_CODE   = 11000 ; //Item Product Code
		public const short  ID_ITEM_QUANTITY   = 12000 ; //Item Quantity
		public const short  ID_ITEM_TAX_AMOUNT   = 14000 ; //Item Tax Amount
		public const short  ID_ITEM_TOTAL_AMOUNT   = 16000 ; //Item Total Amount
		
		///* Purch ing Card Group */
		public const short  ID_MERCHANT_PRODUCT_SKU   = 159 ; //Merchant Product SKU
		///* User Defined Fields */
		public const short  ID_USER_SEQUENCE_NUMBER   = 200 ; //User Sequence Number
		//Added by cognizant on 09/22/2005 the ID_USER_SOURCE_NAME to store the Userid
		public const short  ID_USER_SOURCE_NAME   = 296 ; //User Source Name
		
		///* PS/2000 Group */
		public const short  ID_ORDER_NUMBER   = 150 ; //Order Number
		
		///* Extended Information Group */
		public const short  ID_CURRENT_AMOUNT   = 134 ; //Current Amount

		

		///* Transaction Syntax Errors */
		public const short  ERR_INVALID_FIELD   = 101 ; //Missing or invalid field
		public const short  ERR_FIELD_TOO_LONG   = 102 ; //Field too long
		//Added by Cognizant on 09/19/2005 - Two new Error identifiers
		public const short  ERR_INVALID_FIELD_VALUE = 138; //The data for the field is either too large or too small
		public const short  ERR_INVALID_DATA_TYPE = 139; // The data for the field does not match the fields data type
		///* Processor Specific Group */
		public const short  ID_PROCESSOR_AUTH_RESPONSE_CODE   = 401 ; //Processor Auth Response Code
		//END
		///* AVS Response */
		public const short  ID_ADDRESS_MATCH   = 140 ; //Address Match
		
		#endregion

		
		
		public CPM_Payment()
		{
			
			//
			// TODO: Add constructor logic here
			//
		}
		//Sets the value to a field in Payment Object
		public static void SetValue(ref LCCPaymentClass LCCPayment,int FieldId,string strValue)
		{
			string refString = strValue;
			LCCPayment.SetValue(FieldId,ref refString);
  
		}

		//Sets the value to a field in Payment Object
		public static string GetValue(ref LCCPaymentClass LCCPayment,int FieldId)
		{
			
			return LCCPayment.GetValue(FieldId); 
  
		}

		
		//Format the Decimal Amount to the DDDDDDDDCC Format
        public static string DDCC_Format(decimal Amount)
		{
			
			if(Amount.ToString().IndexOf(".")!= -1) 
			{
				return Amount.ToString().Replace(".","");
 			}
			else
				return Amount.ToString()+"00";
  
		}


		//Format the DDDDDDDDCC Format to Decimal Amount 
		public static decimal Decimal_Format(string DDCC)
		{
			
			return(Convert.ToDecimal(DDCC.Insert((DDCC.Length-2),".")));  
  
		}
	


	}
}

