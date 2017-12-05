/*Added BY COGNIZANT AS PART OF PT MAIG Changes
 * This page has been newly created for PT MAIG Changes.
 * Version 1.0
 * Created Date : 10/12/2014
 * CHG0115410 - Modified the SearchResultRequest class data members & data contracts according to new API
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;
using System.Web;
using System.Runtime.Serialization.Formatters;
using System.Runtime.Serialization;
//using System.Web.Script.Serialization;
using System.Xml.Serialization;

namespace OrderClassesII
{
    [DataContract]
    public class SearchResultRequest
    {
        private SeachRequestParameter seachRequestParameter;
        public SearchResultRequest()
        {
            seachRequestParameter = new SeachRequestParameter();
        }

        [DataMember(Name = "searchRequest")]
        public SeachRequestParameter Parameter
        {
            get
            {
                return seachRequestParameter;
            }
            set
            {
                seachRequestParameter = value;
            }
        }
    }

    [DataContract]
    public class SeachRequestParameter
    {

        private RequestParam requestParam;
        private Header header;
        public SeachRequestParameter()
        {
            requestParam = new RequestParam();
            header = new Header();
        }

        [DataMember(Order = 1, Name = "requestParam")]
        public RequestParam RequestParam
        {
            get
            {
                return requestParam;
            }
            set
            {
                requestParam = value;
            }
        }
        [DataMember(Order = 2, Name = "header")]
        public Header Header
        {
            get
            {
                return header;
            }
            set
            {
                header = value;
            }
        }
    }

    [DataContract]
    public class RequestParam
    {
        //CHG0115410 - BEGIN - Added a private variable for intializing zipcode within the constructor
        private zipCode zipcode;
        public RequestParam()
        {
            zipcode = new zipCode();
        }
        //CHG0115410 - END - Added a private variable for intializing zipcode within the constructor

        [DataMember(Order = 1, Name = "search")]
        public string Search { get; set; }
        [DataMember(Order = 2, Name = "firstName", EmitDefaultValue = false)]
        public string FirstName { get; set; }
        [DataMember(Order = 3, Name = "lastName", EmitDefaultValue = false)]
        public string LastName { get; set; }

        //CHG0115410 - BEGIN - Modified the return type of zipcode from string to zipcode class
        [DataMember(Order = 4, Name = "zipCode", EmitDefaultValue = false)]
        public zipCode ZipCode
        {
            get
            {
                return zipcode;
            }
            set
            {
                zipcode = value;
            }
        }
        //CHG0115410 - END - Modified the return type of zipcode from string to zipcode class

        [DataMember(Order = 5, Name = "policyNumber", EmitDefaultValue = false)]
        public string PolicyNumber { get; set; }
        [DataMember(Order = 6, Name = "docType", EmitDefaultValue = false)]
        public string DocType { get; set; }
    }

    [DataContract]
    public class Header
    {
        [DataMember(Order = 1, Name = "channelType")]
        public string ChannelType { get; set; }
        [DataMember(Order = 2, Name = "agencyCode")]
        public string AgencyCode { get; set; }
        [DataMember(Order = 3, Name = "requestType")]
        public string RequestType { get; set; }
        [DataMember(Order = 4, Name = "agentid")]
        public string AgentId { get; set; }
    }

    //CHG0115410 - BEGIN - Added a zip code class with required data members
    /// <summary>
    /// Used for passing value & type of zipcode along with the input request to the API
    /// </summary>
    [DataContract]
    public class zipCode
    {
        [DataMember(Order = 1, Name = "value", EmitDefaultValue = false)]
        public string value { get; set; }
        [DataMember(Order = 2, Name = "type", EmitDefaultValue = false)]
        public string[] Type { get; set; }
    }
    //CHG0115410 - END - Added a zip code class with required data members
}
