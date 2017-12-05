/*
 * HISTORY
 *			11/18/2004 JOM Replaced Convert.ToInt32(Config.Setting("PaymentID.CreditCard")) with 
 *			PaymentClasses.PaymentTypes.CreditCard
 *	MODIFIED BY COGNIZANT  AS PART OF Q4 RETROFIT CHANGES
 *	11/25/2005 Q4-Retrofit.Ch1-Changes have been made to ensure that the Membership numbers
 *		 are displayed with a preceding '429'  
 * Modified as a part of STAR AUTO 2.1 
 * 09/11/2007 STAR AUTO 2.1.Ch1: Modified to hide the Expiration Date for credit card payments in Customer Copy & Duplicate Copy.
 * 
 * Modified by COGNIZANT on 8/25/2008 for Premier Membership project. ID: PT.Ch1
 * This fixes the defect # 97 (Payment Tool) / # 694 (SalesX). The defect pertains to the rate descriptions not getting displayed
 * when any discounts were offered on primary/associate fees.
 * 1) Removed the code to display the label lblPromo, which earlier had the text '(PROMOTION)' that was used to display when the
 * enrollment fee was waived on a membership. As the label is no longer needed and was removed, this code to display the label
 * has also been removed.
 *  MODIFIED AS A PART OF TIME ZONE CHANGE ENHANCEMENT ON 8-31-2010
 *  TimeZoneChange.Ch1-Added Text MST Arizona at the end of the Receipt date and time  by cognizant.
 *  05/03/2011 RFC#135298 PT_echeck Ch1: Added the label control to display the echeck details in the receipt page by cognizant.
 * 05/06/2011 RFC#135298 PT_echeck ch2:Added the condition to display the authorization text, signature and date column for the echeck payment merchant receipt  by cognizant. 
 * //CHG0072116 - Receipt Issue Fix - Added the length check for routing number before finding sub string for routing number
 * MAIG - CH1 - Commented the code to remove the Sales Rep Number
 * CHG0109406 - CH1 - Modified the timezone from "MST Arizona" to Arizona
*/
using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using OrderClasses.Service;
using CSAAWeb;
using CSAAWeb.AppLogger;


namespace MSC.Reports
{
    /// <summary>
    /// Summary description for Receipt.
    /// </summary>
    public partial class Receipt : System.Web.UI.Page
    {
        protected double dblTotal = 0;
        protected double dblSubTotal = 0;
        //Added as a part of HO6 to dispaly echeck payment on 05-19-2010.
        protected System.Web.UI.HtmlControls.HtmlTable tbleCheckDetails;
        /// <summary>
        /// when each felds bind their value the Procedure is called
        /// This Procedure check for member and Insurance Item and 
        /// if the Item is Member then the Amount is not editable
        /// if the item is insurance then the amount is assigned to a textbox to edit
        /// The Total Amount for the ReceiptNumber is calculated here
        /// </summary>
        /// <param name="Sender"> ReceiptsRepeater</param>
        /// <param name="e">Event Aruguments</param>
        public void ReceiptsRepeater_ItemDataBound(Object Sender, RepeaterItemEventArgs e)
        {
            try
            {
                // This event is raised for the header, the footer, separators, and items.

                string strTemProduct = "";

                // Execute the following logic for Items and Alternating Items.
                if (e.Item.ItemType == ListItemType.Item
                    || e.Item.ItemType == ListItemType.AlternatingItem
                    || e.Item.ItemType == ListItemType.Footer)
                {
                    if (e.Item.ItemType == ListItemType.Footer)
                    {
                        Label lblTotal = (Label)e.Item.FindControl("lblTotal");
                        lblTotal.Text = dblTotal.ToString("$##,##0.00");
                        if (dblSubTotal > 0)
                        {
                            Label lblSubTotal = (Label)e.Item.FindControl("lblSubtotal");
                            lblSubTotal.Text = dblSubTotal.ToString("$##,##0.00");
                            ((HtmlTableRow)e.Item.FindControl("ReceiptSubRow")).Visible = true;
                        }
                        //((HtmlTableRow)e.Item.FindControl("ReceiptSubRow")).Visible = true;
                        return;

                    }

                    //To See the lblItemID Exit and set a Reference for it
                    Label lblTemProduct = (Label)e.Item.FindControl("lblItemId");

                    //To assign the Property value to a string
                    strTemProduct = lblTemProduct.Attributes["ProductName"].ToString();
                    string sAssocID = lblTemProduct.Attributes["AssocID"].ToString();
                    string strAmount = ((Label)e.Item.FindControl("lblAmount")).Text;
                    string strEnrolAmount = ((Label)e.Item.FindControl("lblEnrolment")).Text;


                    //Check the Item for Member.if the item is member the amount is not editable
                    //so assign it to the label 
                    //Else assign it to the textbox so that the user can edit the amount
                    //((Label)e.Item.FindControl("lblInAmount")).Text = Convert.ToDouble(((Label)e.Item.FindControl("lblInAmount")).Text).ToString("##,##0.00");
                    //((Label)e.Item.FindControl("lblAmount")).Text = Convert.ToDouble(((Label)e.Item.FindControl("lblAmount")).Text).ToString("##,##0.00");

                    if (strTemProduct.Equals("Mbr"))
                    {
                        if (sAssocID == "1")
                        {
                            string strTransType = lblTemProduct.Attributes["TransType"].ToString();
                            strAmount = ((Label)e.Item.FindControl("lblMbrTotal")).Text;

                            dblTotal = dblTotal + Convert.ToDouble(strAmount.Replace("$", "").Replace(",", ""));
                            dblSubTotal = Convert.ToDouble(strAmount.Replace("$", "").Replace(",", ""));

                            ((HtmlTableRow)e.Item.FindControl("ReceiptMbrRow")).Visible = true;
                            if (strTransType.Trim().Equals("New"))
                            {
                                // PT.Ch1 - Removed the code to display the label: lblPromo, which in turn has been removed
                                ((HtmlTableRow)e.Item.FindControl("ReceiptEnrolRow")).Visible = true;
                            }
                            ((HtmlTableRow)e.Item.FindControl("ReceiptRow")).Visible = false;


                        }
                        else
                        {
                            ((HtmlTableRow)e.Item.FindControl("ReceiptRow")).Visible = false;
                        }

                    }
                    else
                    {
                        dblTotal = dblTotal + Convert.ToDouble(strAmount.Replace("$", "").Replace(",", ""));
                        //((Label)e.Item.FindControl("lblInAmount")).Visible = false;
                        ((HtmlTableRow)e.Item.FindControl("ReceiptInRow")).Visible = false;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Log(ex);
            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here 

            if (!Page.IsPostBack)
            {

                //for adding the PaymentType to the PaymentType dropdownlist 
                Order DataConnection = new Order(Page);


                //For displaying the ReceiptDetails
                OrderClasses.ReportCriteria rptCrit1 = new OrderClasses.ReportCriteria();
                //Start Changed by Cognizant 05/21/2004 for assigning the Receipt number and the Receipt Type from the query String

                rptCrit1.ReceiptNumber = Request.QueryString["ReceiptNumber"].ToString();

                //END
                //Start Added by Cognizant 05/27/2004 for assigning the ReceiptType.
                if (Request.QueryString["ReceiptType"].ToString() == "CC")
                    lblReceiptType.Text = "Customer Copy";
                if (Request.QueryString["ReceiptType"].ToString() == "MC")
                    lblReceiptType.Text = "Merchant Copy";
                if (Request.QueryString["ReceiptType"].ToString() == "DC")
                    lblReceiptType.Text = "Duplicate Copy";
                //END


                TurninClasses.Service.Turnin TurninService = new TurninClasses.Service.Turnin();
                DataSet dsReceiptDetails = TurninService.GetReceiptDetails(rptCrit1);
                DataTable dtbReceiptDetails = dsReceiptDetails.Tables[0];
                if (dtbReceiptDetails.Rows.Count > 0)
                {
                    //TimeZoneChange.Ch1-Added Text MST Arizona at the end of the Receipt date and time  by cognizant.
                    //CHG0109406 - CH1 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                    lblReceiptDateTime.Text = Convert.ToDateTime(dtbReceiptDetails.Rows[0]["ReceiptDateTime"].ToString()).ToString("MM/dd/yyyy   hh:mm tt") + " " + "Arizona";
                    //CHG0109406 - CH1 - END - Modified the timezone from "MST Arizona" to Arizona
                    lblReceiptNumber.Text = dtbReceiptDetails.Rows[0]["ReceiptNumber"].ToString();
                    //MAIG - CH1 - BEGIN - Commented the code to remove the Sales Rep Number
                    //lblSalesRepNumber.Text = dtbReceiptDetails.Rows[0]["RepID"].ToString();
                    //MAIG - CH1 - END - Commented the code to remove the Sales Rep Number
                    lblDistrictOfficeNumber.Text = dtbReceiptDetails.Rows[0]["Do"].ToString();
                    lblPaymentMethod.Text = dtbReceiptDetails.Rows[0]["PaymentName"].ToString();
                    lblCreditCardType.Text = dtbReceiptDetails.Rows[0]["CardType"].ToString();
                    if (dtbReceiptDetails.Rows[0]["ExpiryDate"].ToString().Length > 0)
                        lblExpirationDate.Text = Convert.ToDateTime(dtbReceiptDetails.Rows[0]["ExpiryDate"].ToString()).ToString("MM/yyyy");

                    //Generate the CheckSum digit for Membership ID
                    //Q4-Retrofit.Ch1- START:Code has been removed by Cognizant as a part of Q4 Retrofit
                    //if(dtbReceiptDetails.Rows[0]["ProductName"].ToString().Trim()=="Mbr")
                    // Code has been added to ensure that the Membership Numbers are displayed with a  preceding '429'
                    DataRow[] drarrMbrDetails = dtbReceiptDetails.Select(@"ProductName = 'Mbr'", "AssocID");
                    if (drarrMbrDetails.Length > 0)
                    {
                        //Modified as part of Q4 Retrofit Changes
                        //string strMembershipID=dtbReceiptDetails.Rows[0]["MemberID"].ToString();
                        //string strMembershipID = drarrMbrDetails[0]["MemberID"].ToString();
                        //MembershipClasses.Member MBRStartDigit = new MembershipClasses.Member();
                        //string[] strID = MBRStartDigit.FullMemberID.Split(' ');
                        //Modified as part of Q4 Retrofit Changes
                        //dtbReceiptDetails.Rows[0]["PolicyNumber"]=strID[0]+" "+dtbReceiptDetails.Rows[0]["PolicyNumber"].ToString()+" "+CSAAWeb.Cryptor.CheckDigit(strID[0].ToString()+strMembershipID);   
                        //drarrMbrDetails[0]["PolicyNumber"] = strID[0] + " " + drarrMbrDetails[0]["PolicyNumber"].ToString() + " " + CSAAWeb.Cryptor.CheckDigit(strID[0].ToString() + strMembershipID);

                    }
                    //Q4-Retrofit.Ch1- END
                    ReceiptsRepeater.DataSource = dtbReceiptDetails;
                    ReceiptsRepeater.DataBind();
                    //Show the Credit card details for Credit Card Payment Type 
                    if (Convert.ToInt32(dtbReceiptDetails.Rows[0]["PaymentID"]) == (int)PaymentClasses.PaymentTypes.CreditCard)
                    {
                        string strCreditCardNumber;
                        tblCardDetails.Visible = true;
                        tbleCheckDetails.Visible = false;
                        tbecheckmerchantcopy.Visible = false;
                        //Modified by cognizant as a part of removing encrypt and decrypt of credit card and echeck numbers on 07-21-2010.
                        //strCreditCardNumber=Cryptor.Decrypt(dtbReceiptDetails.Rows[0]["cardNumber"].ToString(),Config.Setting("CSAA_ORDERS_KEY"));
                        strCreditCardNumber = dtbReceiptDetails.Rows[0]["cardNumber"].ToString();
                        if (strCreditCardNumber.Length > 12)
                        {
                            lblCreditCardNumber.Text = "XXXXXXXXXXXX" + strCreditCardNumber.Substring(12);
                        }
                        lblAuthorizationCode.Text = dtbReceiptDetails.Rows[0]["AuthCode"].ToString();
                        //STAR AUTO 2.1.Ch1: START - Modified to hide the Expiration Date for credit card payments in Customer Copy & Duplicate Copy.
                        if (Request.QueryString["ReceiptType"].ToString() == "CC" || Request.QueryString["ReceiptType"].ToString() == "DC")
                            tblCardDetails.Rows[2].Visible = false;
                        //STAR AUTO 2.1.Ch1: END
                    }
                    //05/03/2011 RFC#135298 PT_echeck Ch1:Start Added the label control to display the echeck details in the receipt page by cognizant on 05/03/2011.
                    else if (Convert.ToInt32(dtbReceiptDetails.Rows[0]["PaymentID"]) == (int)PaymentClasses.PaymentTypes.ECheck)
                    {

                        tbecheckmerchantcopy.Visible = false;
                        string strBankAccNumber, strBankid;
                        // For echeck Bank acc number decrypt and padding on Oct 20 2009.
                        tblCardDetails.Visible = false;
                        lblAuthorizationCode.Text = dtbReceiptDetails.Rows[0]["AuthCode"].ToString();
                        tbleCheckDetails.Visible = true;
                        strBankid = dtbReceiptDetails.Rows[0]["BankId"].ToString();
                        //CHG0072116 - Receipt Issue Fix START- Added the length check for routing number before finding sub string for routing number
                        if (strBankid.Length > 5)
                        {
                            lblBankId.Text = "XXXXX" + strBankid.Substring(5); //Value from receipt details data set  
                        }
                        //CHG0072116 - Receipt Issue Fix END- Added the length check for routing number before finding sub string for routing number
                        ////strBankAccNumber = Cryptor.Decrypt(dtbReceiptDetails.Rows[0]["BankAcntNo"].ToString(), Config.Setting("CSAA_ORDERS_KEY"));// For echeck Bank acc number decrypt and padding on Oct 20 2009
                        strBankAccNumber = dtbReceiptDetails.Rows[0]["BankAcntNo"].ToString();
                        //lblACNo.Text = dtbReceiptDetails.Rows[0]["BankAcntNo"].ToString(); //Value from receipt details data set  *///commented for echeck Bank acc number decrypt and padding on Oct 20 2009
                        if (strBankAccNumber.Length > 12)
                        {
                            lblACNo.Text = "XXXXXXXXXXXX" + strBankAccNumber.Substring(12);//echeck Bank acc number decrypt and padding added on Oct 20 2009
                        }
                        //05/06/2011 RFC#135298 PT_echeck ch2:Added the condition to display the authorization text, signature and date column for the echeck payment merchant receipt copy by cognizant.
                        if (Request.QueryString["ReceiptType"].ToString() == "MC")
                        {
                            tbecheckmerchantcopy.Visible = true;
                        }
                    }
                    //05/03/2011 RFC#135298 PT_echeck Ch1:END Added the label control to display the echeck details in the receipt page by cognizant on 05/03/2011.
                    else
                    {
                        tblCardDetails.Visible = false;
                        tbleCheckDetails.Visible = false;
                        tbecheckmerchantcopy.Visible = false;
                    }
                }
            }
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion
    }
}
