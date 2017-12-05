//CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES
//Added new class containing the JSON Header and Context parameters - OTSP Changes
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace OrderClassesII
{
    /// <summary>
    /// Class to save the response from SOA RetrieveByPolicyNo and RetrieveByPaymentID
    /// </summary>
    public class ScheduledPolicyNoResponse
    {
        public retrieveSchedulePaymentResponse retrieveSchedulePaymentResponse { get; set; }
    }

    /// <summary>
    /// Entity to save the JSON response from SOA RetrieveByPolocyno and RetrieveByPaymentID services
    /// Entity to save Errorcode from SOA service
    /// </summary>
    public class retrieveSchedulePaymentResponse
    {
        public schedulePaymentsDetail schedulePaymentsDetail { get; set; }
        public schedulePaymentsDetails[] schedulePaymentsDetails { get; set; }
        public string httpCode { get; set; }
        public string httpMessage { get; set; }

    }

    /// <summary>
    /// Entity to save the JSON response
    /// This entity will hold the list of Subentities
    /// </summary>
    public class schedulePaymentsDetails
    {

        public schedulePaymentsDetail schedulePaymentsDetail { get; set; }

    }

    /// <summary>
    /// Entity to save the body of the SOA request
    /// ScheduledActivities will contain the list of Scheduled Activities corresponding to PaymentID
    /// ScheduledActivity will contain the list of parameteres for a policy
    /// Count of the ScheduledActivities will be used for Paging in the History screen
    /// </summary>
    public class schedulePaymentsDetail
    {
        public string sequenceNo { get; set; }
        public schedulePaymentInfo schedulePaymentInfo { get; set; }
        public agreeInfo agreementInfo { get; set; }
        public scheduleActivity scheduleActivity { get; set; }
        public scheduleActivities[] scheduleActivities { get; set; }
        public ErrorCode ErrorCode { get; set; }
        public int SchCount { get; set; }
    }


    /// <summary>
    /// Entity to hold response parameteres for schedulePaymentInfo class
    /// Sub entity containing the list of response parameteres to be displayed in Scheduled Payments section
    /// </summary>
    public class schedulePaymentInfo
    {
        public string paymentId { get; set; }
        public int? paymentReferenceNumber { get; set; }
        public string status { get; set; }
    }

    /// <summary>
    /// Entity to hold response parameteres for agreeInfo class
    /// </summary>
    public class agreeInfo
    {
        public string identifier { get; set; }
        public string sourceSystem { get; set; }
        public string type { get; set; }
        public string writingCompany { get; set; }
        public string riskState { get; set; }
    }

    /// <summary>
    /// Entity to hold response parameteres for scheduleActivity class
    /// The parameteres available in this class will be displayed Scheduled Payments section and Scheduled Payments History section
    /// </summary>
    public class scheduleActivity
    {
        public string identifier { get; set; }
        public string schedulingChannel { get; set; }
        public string code { get; set; }
        public string paymentAccountToken { get; set; }
        public string paymentMethod { get; set; }
        public string paymentMethodSubType { get; set; }
        public cardAccount cardAccount { get; set; }
        public bankAccount bankAccount { get; set; }
        public agencyInfo agencyInfo { get; set; }
        public string userID { get; set; }
        public string notificationEmail { get; set; }
        public string paymentAmt { get; set; }
        public DateTime paymentDate { get; set; }
        public DateTime creationDatetime { get; set; }
    }

    /// <summary>
    /// Entity to hold response parameteres for bankAccount class
    /// This entity holds the parameters displayed in both Scheduled Payments section and Scheduled Payments History section
    /// </summary>
    public class bankAccount
    {
        public string numberLast4 { get; set; }
        public string routingNumber { get; set; }
        public string accountHolderName { get; set; }

    }

    /// <summary>
    ///  Entity to hold response parameteres for agencyInfo class
    ///  This entity holds the parameters displayed in both Scheduled Payments section and Scheduled Payments History section
    /// </summary>
    public class agencyInfo
    {
        public string agentId { get; set; }
        public string agencyId { get; set; }
        public string branchHubNumber { get; set; }
    }

    /// <summary>
    /// Entity to hold request parameteres for cardAccount class
    ///  This entity holds the parameters displayed in both Scheduled Payments section and Scheduled Payments History section
    /// </summary>
    public class cardAccount
    {
        public string numberLast4 { get; set; }
        public string printedName { get; set; }
        public DateTime expirationDate { get; set; }
        public string streetAddressLine { get; set; }
        public string extendedStreetAddressLine { get; set; }
        public string cityName { get; set; }
        public string isoRegionCode { get; set; }
        public string zipCode { get; set; }
    }

    /// <summary>
    /// Entity to hold request parameteres for scheduleActivities class
    /// This entity contains a list of ScheduledActivities corresponding to each PaymentID
    /// The entity will be used in the Scheduled Payment History section
    /// </summary>
    public class scheduleActivities
    {
        public scheduleActivity scheduleActivity { get; set; }

    }

    /// <summary>
    /// Entity to hold request parameteres for ErrorCode class
    /// More Information will be displayed in the UI among the list of parameteres
    /// </summary>
    public class ErrorCode
    {
        public string httpCode { get; set; }
        public string httpMessage { get; set; }
        public string moreInformation { get; set; }
        public string timeStamp { get; set; }
    }



}
