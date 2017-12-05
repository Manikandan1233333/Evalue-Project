/*
 * 11/1/04	JOM Removed CCResponse and moved to PaymentClasses namespace.
 * 05/03/2011 RFC#130547 PT_eCheck Ch1 Added the new dataset Bank Account types reference for getting the bank account type details- by cognizant.
*/
using System;
using System.Web.Services.Protocols;
using System.Web.Services;
using System.Data;
using System.Collections;
using CSAAWeb.Serializers;
using System.Xml.Serialization;
using AuthenticationClasses;
using System.ComponentModel;

namespace PaymentClasses.Service
{
    /// <summary>
    /// Proxy class for accessing the Insurance Web service.
    /// </summary>
    public class Payments
    {
        private SessionInfo S = new SessionInfo();
        private InternalService.Payments Pmt = null;
        private UserInfo User = null;

        ///<summary/>
        public Payments()
            : base()
        {
            Pmt = new InternalService.Payments();
        }

        ///<summary/>
        public Payments(SessionInfo S, UserInfo User)
            : base()
        {
            this.S = S;
            Pmt = new InternalService.Payments();
            this.User = User;
        }

        ///<summary>Closes the internal web service.</summary>
        public void Close()
        {
            Pmt.Close();
        }
        /// <remarks/>
        public CCResponse Process(string MerchantRefNum, object Card, object BillTo, IList Items)
        {
            CardInfo C = new CardInfo(Card);
            C.Application = S.AppName;
            return Pmt.Process(User, S.AppName, MerchantRefNum, C, new BillToInfo(BillTo), new ArrayOfLineItem(Items));
        }

        /// <remarks/>
        public CCResponse Auth(string MerchantRefNum, object Card, object BillTo)
        {
            return Auth(MerchantRefNum, Card, new BillToInfo(BillTo));
        }

        /// <remarks/>
        public CCResponse Auth(string MerchantRefNum, object Card, object BillTo, IList Items, bool IgnoreAVS)
        {
            BillToInfo B = new BillToInfo(BillTo);
            B.IgnoreAVS = IgnoreAVS;
            CardInfo C = new CardInfo(Card);
            C.Application = S.AppName;
            return Pmt.Auth(User, S.AppName, MerchantRefNum, C, B, new ArrayOfLineItem(Items));
        }

        /// <remarks/>
        public CCResponse Bill(string MerchantRefNum)
        {
            return Pmt.Bill(User, MerchantRefNum, S.AppName);
        }

        /// <remarks/>
        public CCResponse BillVerbalAuth(string MerchantRefNum, string AuthCode)
        {
            return Pmt.BillVerbalAuth(User, S.AppName, MerchantRefNum, AuthCode);
        }

        /// <remarks/>
        public CCResponse Credit(string MerchantRefNum, object Card, object BillTo, IList Items)
        {
            CardInfo C = new CardInfo(Card);
            C.Application = S.AppName;
            return Pmt.Credit(User, S.AppName, MerchantRefNum, C, new BillToInfo(BillTo), new ArrayOfLineItem(Items));
        }

        /// <summary>
        /// Record a payment other than on-line credit card.
        /// </summary>
        /// <param name="PaymentType">(PaymentTypes) The type of payment.</param>
        /// <param name="Items">(ArrayOfLineItem) An array of the items being purchased.</param>
        /// <param name="ReceiptNumber">(string) The receipt number for this payment.</param>
        public CCResponse RecordPayment(PaymentTypes PaymentType, string ReceiptNumber, IList Items)
        {
            return Pmt.RecordPayment(User, S.AppName, PaymentType, ReceiptNumber, new ArrayOfLineItem(Items));
        }

        /// <summary>
        /// Update the account number on the lines for the payment.  Only one of receipt #
        /// or reference number need be supplied.
        /// </summary>
        /// <param name="User">(UserInfo) Information about the logged-in (internal) user.</param>
        /// <param name="PaymentType">(PaymentTypes) The type of payment.</param>
        /// <param name="Items">(ArrayOfLineItem) An array of the items being purchased.</param>
        /// <param name="ReceiptNumber">(string) The receipt number for this payment.</param>
        /// <param name="Reference">Merchant reference number</param>
        public void UpdateLines(UserInfo User, PaymentTypes PaymentType, string ReceiptNumber, string Reference, IList Items)
        {
            Pmt.UpdateLines(User, S.AppName, PaymentType, ReceiptNumber, Reference, new ArrayOfLineItem(Items));
        }

        /// <remarks/>
        public string GetReferenceNumber()
        {
            if (S == null) throw new Exception("Payments service initialized without Session information!");
            return Pmt.GetMerchantReference(S.AppName);
        }


        /// <summary>
        /// Added by Cognizant on 05/17/2004 for Adding a new Method GetPaymentType which invokes the Webservice
        /// </summary> 
        /// <param name="ColumnFlag">Screen type(Payments(P) or Reports(R)or Workflow(W))</param>
        /// <param name="CurrentUser"> Get Current User to fetch PaymentType based on the roles for the user</param> 
        /// <returns>Dataset of all Payment Types</returns>
        public DataSet GetPaymentType(string ColumnFlag, string CurrentUser)
        {
            return Pmt.GetPaymentType(ColumnFlag, CurrentUser);
        }


        /// <remarks/>
        public DataSet GetCreditCards(bool includePrompt)
        {
            return Pmt.GetCreditCards(includePrompt);
        }

        //Echeck changes  added as a part of HO6 on 04/06/2010
        /// <remarks/>
        //RFC#130547 PT_eCheck Ch1 Added the new dataset Bank Account types reference for getting the bank account type details- by cognizant on 05/3/2011.
        public DataSet GetBankAccount(bool includePrompt)
        {
            return Pmt.GetBankAccount(includePrompt);
        }
        /// <remarks/>
        public DataSet GetStateCodes()
        {
            return Pmt.GetStateCodes();
        }

    }
}
