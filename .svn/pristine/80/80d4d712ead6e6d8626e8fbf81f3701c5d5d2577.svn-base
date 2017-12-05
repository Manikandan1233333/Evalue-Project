/*	CREATION HISTORY:
 *	CREATED BY COGNIZANT
 *	01/04/2013 -  PaymentIDPProxyInputRequest Assigned FOR LIVE WEB SERVICE AS PART OF PAYMENT CENTRAL PHASE II
 *		
 *
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.Collections;
using System.ComponentModel;
using System.Xml.Serialization;

namespace PaymentIDPProxyService
{
    public class PaymentIDPProxyInputRequest
    {
        public applicationContext applicationContext;
        public string districtOffice;
        public string branchHubNumber;
        public string userId;
        public string receiptNumber;
        public string externalReferenceNumber;
        public DateTime createdDate;
        public decimal totalAmount;        
        public string paymentMethod;
        public List<LineItem> LineItem;
        public card card;
        public string checkNumber;
    }
    public class Update
    {
        public string receiptNumber;
        public string voidReceiptNumber;
        public DateTime lastModifiedDate;
        
        }
        public class applicationContext
        {
            private string _application;
            /// <summary>Default constructor</summary>
            public applicationContext() : base() { }

            [XmlAttribute]
            public string application
            {
                get { return _application; }
                set { _application = value; }
            }
        }        
        public class card
        {
            private string _cardEcheckNumber;
            private string _returnCode;
            private string _authorizationCode;
            private string _sequenceNumber;

            /// <summary>Default constructor</summary>
            public card() : base() { }

            [XmlAttribute]
            [DefaultValue("")]
            public string cardEcheckNumber
            {
                get { return _cardEcheckNumber; }
                set { _cardEcheckNumber = value; }
            }

            [XmlAttribute]
            [DefaultValue("")]
            public string returnCode
            {
                get { return _returnCode; }
                set { _returnCode = value; }
            }

            [XmlAttribute]
            [DefaultValue("")]
            public string authorizationCode
            {
                get { return _authorizationCode; }
                set { _authorizationCode = value; }
            }

            [XmlAttribute]
            [DefaultValue("")]
            public string sequenceNumber
            {
                get { return _sequenceNumber; }
                set { _sequenceNumber = value; }
            } 
        }
         /// <summary>
        /// Represents a line item for an order
        /// </summary>
        /// <remarks>
        /// Collection can be instantiated from a delimited string (legacy) or xml string.
        /// </remarks>    
            
        public class LineItem 
        {
            private string _dataSource;
            private string _LineItemNo;
            private string _writingCompany;
            private string _productCode;
            private string _policyPrefix;
            private string _policyNumber;
            private string _policyState;
            private decimal _Amount;
            private string _revenueType;
            ///<summary/>
            public LineItem() : base() { }

            /// <summary>dataSource - dataSource number property</summary>
            [XmlAttribute]
            public string dataSource
            {
                get { return _dataSource; }
                set { _dataSource = value; }
            }
            /// <summary>LineItemNo - line item number property</summary>
            [XmlAttribute]
            public string LineItemNo
            {
                get { return _LineItemNo; }
                set { _LineItemNo = value; }
            }
            /// <summary>writingCompany - holds writingCompany</summary>
            [XmlAttribute]
            [DefaultValue("")]
            public string writingCompany
            {
                get { return _writingCompany; }
                set { _writingCompany = value; }
            }
            /// <summary>productCode - holds product Code</summary>
            [XmlAttribute]
            public string productCode
            {
                get { return _productCode; }
                set { _productCode = value; }
            }
            /// <summary>policyPrefix - holds policy Prefix </summary>
            [XmlAttribute]
            public string policyPrefix
            {
                get { return _policyPrefix; }
                set { _policyPrefix = value; }
            }
            /// <summary>policyNumber - holds policy Number</summary>
            [XmlAttribute]
            public string policyNumber
            {
                get { return _policyNumber; }
                set { _policyNumber = value; }
            }
            /// <summary>policyState - holds policy State</summary>
            [XmlAttribute]
            public string policyState
            {
                get { return _policyState; }
                set { _policyState = value; }
            }
            /// <summary>Amount - holds a dollar amount for a single line item</summary>
            [XmlAttribute]
            public decimal Amount
            {
                get { return _Amount; }
                set { _Amount = value; }
            }
            /// <summary>revenueType - holds a revenueType for this line item</summary>
            [XmlAttribute]
            public string revenueType
            {
                get { return _revenueType; }
                set { _revenueType = value; }
            }

        }

    /// <summary>
    /// Service Response 
    /// </summary>
    
     public class IDPProxyResponse 
        {

            private string _responseCode;
            private string _responseMessage;

            public IDPProxyResponse() : base() { }

            public IDPProxyResponse(string responseCode, string responseMessage)
            {
                this.responseCode = responseCode;
                this.responseMessage = responseMessage;

            }
            [XmlAttribute]
            [DefaultValue("")]
            public string responseCode
            {
                get { return _responseCode; }
                set { _responseCode = value; }
            }

            [XmlAttribute]
            [DefaultValue("")]
            public string responseMessage
            {
                get { return _responseMessage; }
                set { _responseMessage = value; }
            }


        }
    
}