/*Added BY COGNIZANT AS PART OF PT MAIG Changes
 * This page has been newly created for PT MAIG Changes.
 * Version 1.0
 * Created Date : 10/12/2014
 * CHG0115410 - Modified all the classes in the SearchResultResponse.cs based on the new response from the Customer Search API
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
    public class SearchResultResponse
    {
        [DataMember(Order = 1, Name = "responseJson")]
        public Responsejson responseJson { get; set; }
    }

    [DataContract]
    public class Responsejson
    {
        [DataMember(Order = 1, Name = "facets")]
        public Facets facets { get; set; }
        [DataMember(Order = 2, Name = "searchedPolicies")]
        public Searchedpolicy[] searchedPolicies { get; set; }
        [DataMember(Order = 3, Name = "searchedQuotes")]
        public object[] searchedQuotes { get; set; }
        [DataMember(Order = 4, Name = "totalHits")]
        public int totalHits { get; set; }
    }

    [DataContract]
    public class Facets
    {
        [DataMember(Order = 1, Name = "totalCountByDocType")]
        public Totalcountbydoctype totalCountByDocType { get; set; }
    }

    [DataContract]
    public class SearchFacets
    {
        [DataMember(Order = 1)]
        public Totalcountbydoctype totalCountByDocType { get; set; }
        [DataMember(Order = 2)]
        public int totalCountByDocTypeCount { get; set; }
        [DataMember(Order = 3)]
        public Totalcountbypolicystatus totalCountByPolicyStatus { get; set; }
        [DataMember(Order = 4)]
        public int totalCountByPolicyStatusCount { get; set; }
        [DataMember(Order = 5)]
        public Totalcountbyprodtypecode totalCountByProdTypeCode { get; set; }
        [DataMember(Order = 6)]
        public int totalCountByProdTypeCodeCount { get; set; }
        [DataMember(Order = 7)]
        public int totalCountByQuoteStatusCount { get; set; }
        [DataMember(Order = 8)]
        public Totalcountbyriskstate totalCountByRiskState { get; set; }
        [DataMember(Order = 9)]
        public int totalCountByRiskStateCount { get; set; }
    }

    [DataContract]
    public class Totalcountbydoctype
    {
        [DataMember(Order = 1)]
        public int POLICY { get; set; }
    }

    [DataContract]
    public class Totalcountbypolicystatus
    {
        [DataMember(Order = 1, Name = "ACTIVE")]
        public int ACTIVE { get; set; }
        [DataMember(Order = 2, Name = "ACTIVERENEWED")]
        public int ACTIVERENEWED { get; set; }
        [DataMember(Order = 3, Name = "CANCAFTEREXPDTUPLOAD")]
        public int CANCAFTEREXPDTUPLOAD { get; set; }
        [DataMember(Order = 4, Name = "CANCELLED")]
        public int CANCELLED { get; set; }
        [DataMember(Order = 5, Name = "CANCELLEDPRORATA")]
        public int CANCELLEDPRORATA { get; set; }
        [DataMember(Order = 6, Name = "NONRENEWED")]
        public int NONRENEWED { get; set; }
        [DataMember(Order = 7, Name = "PENDINGISSUED")]
        public int PENDINGISSUED { get; set; }
    }


    [DataContract]
    public class Totalcountbyprodtypecode
    {
        [DataMember(Order = 1, Name = "AU")]
        public int AU { get; set; }
        [DataMember(Order = 2, Name = "HO")]
        public int HO { get; set; }
        [DataMember(Order = 3, Name = "MSHP")]
        public int MSHP { get; set; }
        [DataMember(Order = 4, Name = "PU")]
        public int PU { get; set; }
        [DataMember(Order = 5, Name = "PUP")]
        public int PUP { get; set; }
    }

    [DataContract]
    public class Totalcountbyriskstate
    {
        [DataMember(Order = 1)]
        public int _4 { get; set; }
        [DataMember(Order = 2)]
        public int CA { get; set; }
        [DataMember(Order = 3)]
        public int MD { get; set; }
        [DataMember(Order = 4)]
        public int NJ { get; set; }
        [DataMember(Order = 5)]
        public int OK { get; set; }
        [DataMember(Order = 6)]
        public int PA { get; set; }
    }

    [DataContract]
    public class Searchedpolicy
    {
        [DataMember(Order = 1)]
        public string agency { get; set; }
        [DataMember(Order = 2)]
        public string agent { get; set; }
        [DataMember(Order = 3)]
        public Partylist[] partyList { get; set; }
        [DataMember(Order = 4)]
        public string policyInceptionDate { get; set; }
        [DataMember(Order = 5)]
        public string policyNumber { get; set; }
        [DataMember(Order = 6)]
        public string policyStatus { get; set; }
        [DataMember(Order = 7)]
        public string prodTypeCode { get; set; }
        [DataMember(Order = 8)]
        public string productCode { get; set; }
        [DataMember(Order = 9)]
        public string riskState { get; set; }
        [DataMember(Order = 10)]
        public Sortedfields sortedFields { get; set; }
        [DataMember(Order = 11)]
        public string sourceSystem { get; set; }
        [DataMember(Order = 12)]
        public string termEffectiveDate { get; set; }
        [DataMember(Order = 13)]
        public string termExpirationDate { get; set; }
        [DataMember(Order = 14)]
        public string ubiFlag { get; set; }
        [DataMember(Order = 15)]
        public Contractaddress[] contractAddress { get; set; }
    }

    [DataContract]
    public class Sortedfields
    {
    }

    [DataContract]
    public class Partylist
    {
        [DataMember(Order = 1)]
        public Partyaddress[] partyAddress { get; set; }
        [DataMember(Order = 2)]
        public Partyrole[] partyRoles { get; set; }
        [DataMember(Order = 3)]
        public Phonelist[] phoneList { get; set; }
        [DataMember(Order = 4)]
        public string customerStatus { get; set; }
        [DataMember(Order = 5)]
        public string firstName { get; set; }
        [DataMember(Order = 6)]
        public string lastName { get; set; }
        [DataMember(Order = 7)]
        public bool matchFlag { get; set; }
        [DataMember(Order = 8)]
        public string mdmid { get; set; }
        [DataMember(Order = 9)]
        public string dob { get; set; }
        [DataMember(Order = 10)]
        public string dobYear { get; set; }
        [DataMember(Order = 11)]
        public string driversLicenseNumber { get; set; }
        [DataMember(Order = 12)]
        public string gender { get; set; }
        [DataMember(Order = 13)]
        public string maritalStatus { get; set; }
        [DataMember(Order = 14)]
        public string middleName { get; set; }
        [DataMember(Order = 15)]
        public string email { get; set; }
        [DataMember(Order = 16)]
        public string empInd { get; set; }
        [DataMember(Order = 17)]
        public string registrationId { get; set; }
        [DataMember(Order = 18)]
        public string memDt { get; set; }
        [DataMember(Order = 19)]
        public string memNum { get; set; }
    }

    [DataContract]
    public class Partyaddress
    {
        [DataMember(Order = 1)]
        public string address1 { get; set; }
        [DataMember(Order = 2)]
        public string addressType { get; set; }
        [DataMember(Order = 3)]
        public string city { get; set; }
        [DataMember(Order = 4)]
        public string state { get; set; }
        [DataMember(Order = 5)]
        public string zipCode { get; set; }
    }

    [DataContract]
    public class Partyrole
    {
        [DataMember(Order = 1)]
        public string roleType { get; set; }
        [DataMember(Order = 2)]
        public string sourceId { get; set; }
    }

    [DataContract]
    public class Phonelist
    {
        [DataMember(Order = 1)]
        public string phoneAreaCode { get; set; }
        [DataMember(Order = 2)]
        public string phoneExchange { get; set; }
        [DataMember(Order = 3)]
        public string phoneFullNumber { get; set; }
        [DataMember(Order = 4)]
        public string phoneNumber { get; set; }
        [DataMember(Order = 5)]
        public string phoneType { get; set; }
        [DataMember(Order = 6)]
        public string phoneExtension { get; set; }
    }

    [DataContract]
    public class Contractaddress
    {
        [DataMember(Order = 1)]
        public string address1 { get; set; }
        [DataMember(Order = 2)]
        public string addressType { get; set; }
        [DataMember(Order = 3)]
        public string city { get; set; }
        [DataMember(Order = 4)]
        public string state { get; set; }
        [DataMember(Order = 5)]
        public string zipCode { get; set; }
    }

}
