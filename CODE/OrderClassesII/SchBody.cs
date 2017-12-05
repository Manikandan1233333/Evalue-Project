//CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES
//Added new class containing the Body of the JSON request - OTSP Changes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
namespace OrderClassesII
{
    /// <summary>
    /// Class creating the JSON request parameters for invoking SOA services
    /// The entity will be converted to JSON format and passed to the SOA service
    /// </summary>
    [DataContract]
    public class SchBody
    {
        private retrieveSchedulePaymentRequest retrieveSchedulePolicyNoBody;
        public SchBody()
        {
            retrieveSchedulePolicyNoBody = new retrieveSchedulePaymentRequest();
        }

        /// <summary>
        /// Entity holding the list of Request paramanters for the SOA service
        /// </summary>
        [DataMember]
        public retrieveSchedulePaymentRequest retrieveSchedulePaymentRequest
        {
            get
            {
                return retrieveSchedulePolicyNoBody;
            }
            set
            {
                retrieveSchedulePolicyNoBody = value;
            }
        }
    }

    /// <summary>
    /// Class containing request parameters for Scheduled Payments section and Scheduled Payments History section
    /// AgreementNumber,SourceSystem,ProductType passed to the Body of the request for SOA RetrieveByPolicyNo service
    /// IncludeHistory and PaymentID passed to the Body of the request for SOA RetrieveByPaymentID service
    /// </summary>
    [DataContract]
    public class retrieveSchedulePaymentRequest
    {
        private agreementInfo agreeInfo;
        public retrieveSchedulePaymentRequest()
        {
            agreeInfo = new agreementInfo();
        }
        [DataMember]
        public agreementInfo agreementInfo
        {
            get
            {
                return agreeInfo;
            }
            set
            {
                agreeInfo = value;
            }
        }
        [DataMember]
        public string schedulePaymentStatus { get; set; }

        [DataMember]
        public string paymentId { get; set; }
        [DataMember]
        public bool includeHistory { get; set; }
    }

    /// <summary>
    /// Class containing parameters for SOA RetrieveByPolicyNo service
    /// AgreementInfo class passed to the SOA RetrieveByPolicyNo request
    /// </summary>
    [DataContract]
    public class agreementInfo
    {
        [DataMember]
        public string identifier { get; set; }
        [DataMember]
        public string sourceSystem { get; set; }
        [DataMember]
        public string type { get; set; }
    }

}
