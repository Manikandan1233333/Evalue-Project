/*	REVISION HISTORY:
 *	MODIFIED BY COGNIZANT
 *	07/19/2005 - For changing the web.config entries pointing to database. This changes 
 *				 are made as part of CSR #3937 implementation.
 *	Changes Done:
 *	CSR#3937.Ch1 : Include the namespace for accessing the StringDictionary object
 *	CSR#3937.Ch2 : Changed the code for the new way of accessing the constants (web.config changes)
 *	07/26/2005	-	For renaming the files ending with _New.
 *  CSR#3937.Ch3 : Renamed the class InsSearchOrders_New to InsSearchOrders
 *  CSR#3824.Ch1 : Modified to add a new Status Column in the Transaction Search
 * 
 * MODIFIED BY COGNIZANT AS PART OF Q4 RETROFIT CHANGES
 * 11/29/2005 Q4-Retrofit.Ch1 : Removed the condition (drv["Appl."].ToString().Trim() != Config.Setting("ApplicationType.Star")) 
 *          in the IF loop This change has been made to display the Duplicate Receipt button for the STAR CUI transactions
 * 
 *  04/26/2006 Q1-Retrofit.Ch1 : Renamed the class InsSearchOrders to InsSearchOrders_New as part of CSR #4833
 *  
 *  MODIFIED BY COGNIZANT AS PART OF STAR RETROFIT II
 *  2/07/2007 STAR Retrofit II.Ch1: Modified code to validate start date and end date before adding one day to the end date.
 * 
 * STAR Retrofit III Changes: 
 * Modified as a part of CSR #5595
 *  4/02/2007 STAR Retrofit III.Ch1: 
 *				Modified to fill the year dropdown box dynamically.
 *  4/02/2007 STAR Retrofit III.Ch2: 
 *				Modified the filling of date drop down list to use FillNumericComboBox() instead of DynamicDate().
 *                Modified the index parameter to set the current year selected by default.
 *				  Added new custom validators for validating dates
 *  4/02/2007 STAR Retrofit III.Ch3: 
 *				Commented the SelectedIndexChanged events of month selection to prevent roundtrips to server.
 *  4/02/2007 STAR Retrofit III.Ch4: 
 *				Modified the cell index of DuplicateReceipt column.
 *  4/02/2007 STAR Retrofit III.Ch5: 
 *				Modified to display two new columns Approved By and Approved date in the Transaction Search result grid.
 *  4/02/2007 STAR Retrofit III.Ch6: 
 *				Modified the index parameter for the day dropdown of end date selector to set the last day of the current month 
 * 
 *                and year selected by default during page load.
 * SR # 8494956 :Added  method  InvisibleControls() to btn_submitclick  by cognizant on Oct 20 2009 as a part of Transaction Search Deadlock issue
 * Modified as a part of HO6 on 04-21-2010.
 * HO6.Ch1:Added encrypted echeck account number.
 * HO6.Ch2:Added eCheck account number for search creteria.
 * HO6.Ch3:Added a condition to check echeck account number length.
 * HO6.Ch4:Added a string to hold the encrypted echeck account number.
 * Remove Encrypt/Decrypt.Ch1 : Modified by cognizant as a part of removing encript and decrypt on Credit card and echeck on 07-21-2010.
 * MODIFIED AS A PART OF TIME ZONE CHANGE ENHANCEMENT ON 8-31-2010
 * TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time and Run Date  by cognizant.
 * // SSO Integration - CH1 -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
 * PC Phase II Changes CH1 - Removed the Relevant code for the Fields removed for Transaction Search UI
 * //PC Phase II changes CH2 - Modified the below code to Handle Date Time Picker
 * PC Phase II 4/20 - Added logging to find the user ID and search criteria for the insurance reports, Transaction search.
 * CHG0078293 - PT enhancement CH1 - Added the new fields added withe existing Acount number and receipt number condition
 * CHG0078293 - PT enhancement CH2 - Added the below condition to restrict the date condition to two days to search with the new search fields and assign the values  to the search criteria
 * CHG0078293 - PT enhancement CH3 - Added regex condition for Receipt Number and Account Number Textbox validations.
 * CHG0078293 - PT enhancement CH4 - Added regex condition for Receipt Number and Account Number Textbox validations.
 * -----------------------------------------------------------------------------------------------------------------------
 * 
 * MAIG - 09/15/2014 -- Code modified by COGNIZANT with following changes:
 * MAIG - CH1 - Added Trim to defer any white space
 * MAIG - CH2 - Added Trim to defer any white space
 * MAIG - CH3 - for adding Agent & User IDs in Search Criteria and trim functionality
 * MAIG - CH4 - Added Trim to defer any white space
 * MAIG - CH5 - for adding Agent & User IDs in Search Criteria and trim functionality
 * MAIG - CH6 - Added Trim to defer any white space
 * MAIG - CH7 - for adding Agent & User IDs in Search Criteria and trim functionality
 * MAIG - CH8 - for adding Agent & User IDs in Search Criteria and trim functionality
 * MAIG - CH9 - Added Trim to defer any white space
 * MAIG - CH10 - Adjusted cell count to accomodate newly added Policy State & Txn Type columns
 * MAIG - CH11 - for displaying ALT Text for Firefox Print button issue
 * MAIG - CH12 - for displaying ALT Text for Firefox Print button issue & adjsuted the cell size
 * MAIG - CH13 - Gave a column name for the last column - MAIG
 * MAIG - CH14 - Gave a column name for the last column - MAIG
 * MAIG - CH15 - Renamed the Report Header - MAIG
 * MAIG - CH16 - Modified the Width from 70 to 30
 * CHG0109406 - CH1 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH2 - Set the below label visible property to false which displays the timezone in results
 * CHG0109406 - CH3 - Set the below label visible property to false which displays the timezone in results
 * CHG0109406 - CH4 - Set the below label visible property to True which displays the timezone in results
 * CHG0109406 - CH5 - Change to update ECHECK to ACH - 01202015
 * CHG0109406 - CH6 - Change to update ECHECK to ACH - 01202015
 * CHG0109406 - CH7 - Updated the table to be visible when the results are shown
 * CHG0109406 - CH8 - Updated the table to be hidden when the results are shown
 * CHG0110069 - CH1 - Appended the /Time along with the Date Label
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
using System.Data.SqlClient;
using System.Configuration;
using CSAAWeb;
using CSAAWeb.Serializers;
using CSAAWeb.AppLogger;
using OrderClasses.Service;
using InsuranceClasses;

//CSR#3937.Ch1 : START - Include the namespace for accessing the StringDictionary object
using System.Collections.Specialized;
using System.Text.RegularExpressions;
//CSR#3937.Ch1 : END - Include the namespace for accessing the StringDictionary object

namespace MSCR.Reports
{
    /// <summary>
    /// Summary description for InsReport1.
    /// </summary>
    //CSR#3937.Ch3 : START - Renamed the class InsSearchOrders_New to InsSearchOrders
    //Q1-Retrofit.Ch1 : START - Renamed the class InsSearchOrders to InsSearchOrders_New
    public partial class InsSearchOrders_New : CSAAWeb.WebControls.PageTemplate
    //Q1-Retrofit.Ch1 : END
    //CSR#3937.Ch3 : END - Renamed the class InsSearchOrders_New to InsSearchOrders
    {
        //PC Phase II Changes CH1 - Removed the Relevant code for the Fields removed for Transaction Search UI
        //string sCCSignature=string.Empty;
        //HO6.Ch4:Added to hold the encrypted format of eCheck account number as a part of HO6 on 04-21-2010.
        //string sEcheckSignature=string.Empty;
        string start_date, end_date;

        protected DataTable Dt;
        //PC Phase II Changes CH1 - Removed the Relevant code for the Fields removed for Transaction Search UI
        //STAR Retrofit III.Ch2: START - Added new custom validator to validate Start and End dates
        //protected System.Web.UI.WebControls.TextBox txtEcheckAcc;
        //STAR Retrofit III.Ch2: END

        InsRptLibrary rptLib = new InsRptLibrary();

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // SSO Integration - CH1 Start -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            // SSO Integration - CH1 End -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
            // Put user code to initialize the page here
            if (!Page.IsPostBack)
            {
                Session.Contents.Clear();
                Session.Abandon();

                //STAR Retrofit III.Ch1: START - Modified to fill the year dropdown box dynamically.
                //int CurrentYear = DateTime.Now.Year;
                //int[] arr_year = new int[3]; 
                //for(int i=0;i<arr_year.Length;i++)
                //{
                //    arr_year[i] = CurrentYear - 2;
                //    CurrentYear++;
                //}
                //start_date_year.DataSource = arr_year;
                //start_date_year.DataBind();

                //end_date_year.DataSource = arr_year;
                //end_date_year.DataBind();
                ////STAR Retrofit III.Ch1: END

                //rptLib.FillNumericComboBox(start_date_month,1,12);
                //rptLib.FillNumericComboBox(end_date_month,1,12);

                ////STAR Retrofit III.Ch2: START - Modified the filling of date drop down list to use FillNumericComboBox.
                //rptLib.FillNumericComboBox(start_date_day,1,31);
                //rptLib.FillNumericComboBox(end_date_day,1,31);
                ////STAR Retrofit III.Ch2: END

                //start_date_month.SelectedIndex = DateTime.Now.Month - 1;
                //end_date_month.SelectedIndex = DateTime.Now.Month - 1;

                ////STAR Retrofit III.Ch2: START - START Modified the index parameter to set the current year selected by default. 
                //start_date_year.SelectedIndex=  2;
                //end_date_year.SelectedIndex = 2;

                ////rptLib.DynamicDate(start_date_day, start_date_month, start_date_year);
                ////rptLib.DynamicDate(end_date_day, end_date_month, end_date_year);
                ////STAR Retrofit III.Ch2: END

                ////STAR Retrofit III.Ch6: START - Modified the index parameter to set the last day of the current month and year 
                //int numberOfDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
                //DateTime lastDay = new DateTime(DateTime.Now.Year,DateTime.Now.Month, numberOfDays);
                //int last_date=lastDay.Day;
                //end_date_day.SelectedIndex = end_date_day.Items.IndexOf(end_date_day.Items.FindByText(last_date.ToString()));
                ////end_date_day.SelectedIndex = end_date_day.Items.Count - 1;
                //STAR Retrofit III.Ch6: END

                lblInsTitle.Visible = false;
                lblMbrTitle.Visible = false;
                //CHG0109406 - CH2 - BEGIN - Set the below label visible property to false which displays the timezone in results
                lblHeaderTimeZone.Visible = false;
                //CHG0109406 - CH2 - END - Set the below label visible property to false which displays the timezone in results
                pnlTitle.Visible = false;
                Initialize();
                //PC Phase II changes CH2 - Start - Modified the below code to Handle Date Time Picker
                DateTime now = DateTime.Now;
                DateTime startdate = new DateTime(now.Year, now.Month, 1);
                TxtstartDt.Text = startdate.ToString("MM-dd-yyyy HH:mm:ss");
                DateTime enddate = DateTime.Today;
                TxtendDt.Text = enddate.ToString("MM-dd-yyyy") + " 23:59:59";
                //PC Phase II changes CH2 - End - Modified the below code to Handle Date Time Picker
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
            //START - Code added by COGNIZANT 07/07/2004
            //Initialize(); // To Load the Datas into Cache
            //BindData();   // Bind the Data from the Cache to DropdownList
            //END

        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            //This event is not required as per the tech upgrade changes - done by Cognizant on 08/18/2010
            //this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.btnSubmit_Click);


        }
        /// <summary>
        /// Binding the user controls.
        /// </summary>
        private void Initialize()
        {

            Order DataConnection = new Order(Page);

            if (Cache["ApplicationsNew"] == null)
            {
                // .Modified by Cognizant Authentication changed to Insurance
                Dt = DataConnection.LookupDataSet("Insurance", "GetApplications").Tables[0];
                if (Dt.Rows.Count > 1)
                {
                    //AddSelectItem(Dt, "All","-1");
                    DataRow Row = Dt.NewRow();
                    Row["Description"] = "-1";
                    Row["RepDescription"] = "All";
                    Dt.Rows.InsertAt(Row, 0);
                }
                Cache["ApplicationsNew"] = Dt;
                BindData();
            }
            else
            {
                BindData();
            }

            // To Add product Types based on Application Ids
            Dt = DataConnection.LookupDataSet("Insurance", "ApplicationProductTypes", new object[] { -1 }).Tables["ApplicationProductTypes"];
            if (Dt.Rows.Count > 1) AddSelectItem(Dt, "All", "-1");
            _ProductType.DataSource = Dt;
            _ProductType.DataBind();

            DataConnection.Close();
        }

        /// <summary>
        /// Binds the data to the list boxes.
        /// </summary>
        private void BindData()
        {
            _Application.DataSource = Applications;
            _Application.DataBind();
        }
        #endregion


        protected void btnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            try
            {

                dgSearch2.Dispose();

                dgSearch2.PageIndex = 0;


                dgSearch2.Visible = false;
                lblInsTitle.Visible = false;
                lblMbrTitle.Visible = false;
                //CHG0109406 - CH3 - BEGIN - Set the below label visible property to false which displays the timezone in results
                lblHeaderTimeZone.Visible = false;
                //CHG0109406 - CH3 - END - Set the below label visible property to false which displays the timezone in results
                lblErrMsg.Visible = false;
                pnlTitle.Visible = false;
                //PC Phase II changes CH2 -Start- Added the below code to Handle Date Time Picker
				//MAIG - CH1 - BEGIN - Added Trim to defer any white space
                start_date = TxtstartDt.Text.Trim();
				//MAIG - CH1 - END - Added Trim to defer any white space
                DateTime dt_start_date = DateTime.Parse(start_date);
				//MAIG - CH2 - BEGIN - Added Trim to defer any white space
                end_date = TxtendDt.Text.Trim();
				//MAIG - CH2 - END - Added Trim to defer any white space
                //STAR Retrofit II.Ch1 - START -Added logging to find the user ID and search criteria for the insurance reports, Transaction search 
                DateTime end_date_check = DateTime.Parse(end_date);
                //PC Phase II 4/20 - START-modified the Date Range in Transaction search to fix one day a head issue.
                //Modified the below code to display the exact end date seelcted in drop down combo.
                DateTime dt_end_date = DateTime.Parse(end_date);
                //PC Phase II 4/20 -END-Added logging to find the user ID and search criteria for the insurance reports, Transaction search

                bool bValid = rptLib.ValidateDate(lblErrMsg, dt_start_date, end_date_check, 99);
                //STAR Retrofit II.Ch1 - END
                if (bValid == true)
                {
                    InvisibleControls();
                    return;
                }
                //Begin PC Phase II Changes CH1 - Removed the Relevant code for the Fields removed for Transaction Search UI
                //if (txtCCSuffix.Text.Length > 0 )
                //{
                //    //67811A0  - PCI Remediation for Payment systems CH1:Start Modified the code to remove the credit card prefix filed in UI and the text box by as a part of security testing by cognizant on 01/09/2011
                //    bValid = rptLib.ValidateCCNumber(lblErrMsg, txtCCSuffix.Text);

                //    //bValid = rptLib.ValidateCCNumber(lblErrMsg, txtCCPrefix.Text, txtCCSuffix.Text);
                //    //67811A0  - PCI Remediation for Payment systems CH1:end Modified the code to remove the credit card prefix filed in UI and the text box by as a part of security testing by cognizant on 01/09/2011
                //    if (bValid == true) 
                //    {
                //        InvisibleControls();
                //        return;
                //    }
                //    else
                //    {												
                //        PaymentClasses.CardInfo Card = new PaymentClasses.CardInfo();
                //        Card.CCNumber = "XXXXXXXXXXXX" + this.txtCCSuffix.Text.ToString();
                //        sCCSignature = Card.Signature;
                //    }
                //}

                //HO6.Ch3:Added to check whether the echeck account number is greater than zero as a part of HO6 on 04-21-2010.
                //if(txtEcheckAcc.Text.Length>0)
                //{
                //PaymentClasses.eCheckInfo echeck= new PaymentClasses.eCheckInfo();
                //    echeck.BankAcntNo=this.txtEcheckAcc.Text.ToString();
                // sEcheckSignature=echeck.signature;
                //PaymentClasses.CardInfo Card = new PaymentClasses.CardInfo();
                //Card.CCCVNumber=sEcheckSignature;
                //}
                // .Modified by Cognizant based on new SP changes
                //HO6.Ch2:Modified to check the search creteria for echeck account number also as a part of HO6 on 04-21-2010.
                //if((txtAmount.Text.Length == 0)&&(txtEcheckAcc.Text.Length==0) && (txtCCAuthCode.Text.Length == 0) && (txtCCPrefix.Text.Length == 0) && (txtCCSuffix.Text.Length == 0) && (txtLastName.Text.Length == 0) &&  (txtMerchantRefNbr.Text.Length == 0) && (txtAccountNbr.Text.Length == 0) && (txtReceiptNbr.Text.Length == 0))
                //if ((txtAmount.Text.Length == 0) && (txtEcheckAcc.Text.Length == 0) && (txtCCAuthCode.Text.Length == 0) && (txtCCSuffix.Text.Length == 0) && (txtLastName.Text.Length == 0) && (txtMerchantRefNbr.Text.Length == 0) && 
                //End PC Phase II Changes CH1 - Removed the Relevant code for the Fields removed for Transaction Search UI
                //CHG0078293 - PT enhancement CH1 START- Added the new fields added withe existing Acount number and receipt number condition
                //MAIG - CH3 - BEGIN - for adding Agent & User IDs in Search Criteria and trim functionality
                if ((txtAccountNbr.Text.Trim().Length == 0) && (txtReceiptNbr.Text.Trim().Length == 0) &&
                    (txtAmount.Text.Trim().Length == 0) && (txtEcheckAcc.Text.Trim().Length == 0) &&
                    (txtDO.Text.Trim().Length == 0) && (txtCCSuffix.Text.Trim().Length == 0) &&  (txtAgentId.Text.Trim().Length ==0) && (txtUserId.Text.Trim().Length == 0))
                //MAIG - CH3 - END - for adding Agent & User IDs in Search Criteria and trim functionality
                //CHG0078293 - PT enhancement CH1 END- Added the new fields added withe existing Acount number and receipt number condition
                {
                    rptLib.SetMessage(lblErrMsg, "Please specify a search criteria.", true);
                    InvisibleControls();
                    return;
                }

                OrderClasses.SearchCriteria srchCrit1 = new OrderClasses.SearchCriteria();
                srchCrit1.StartDate = dt_start_date;
                srchCrit1.EndDate = dt_end_date;

                //CHG0078293 - PT enhancement CH3 START- Added regex condition for Receipt Number and Account Number Textbox validations.
                Regex rx = new Regex(@"^(?=.*\d)[a-zA-Z0-9]+$");
                //MAIG - CH4 - BEGIN - Added Trim to defer any white space
                if (this.txtReceiptNbr.Text.Trim() == string.Empty || (rx.IsMatch(this.txtReceiptNbr.Text.Trim())))
                {
                    //START --> Code added by COGNIZANT - 07/08/2004 - for adding Receipt Number in Search Criteria
                    srchCrit1.ReceiptNbr = this.txtReceiptNbr.Text.Trim();
	                //MAIG - CH4 - END - Added Trim to defer any white space
                    //END
                    //START --> Code added by COGNIZANT - 07/08/2004
                    //Changed by Cognizant to search based on App_Name and Description
                    /* Modified by Cognizant on Feb 1 2005 to search based on App_Name and display test as Description */
                }
                else
                {
                    rptLib.SetMessage(lblErrMsg, "Please specify a valid Receipt Number", true);
                    InvisibleControls();
                    return;
                }
                //CHG0078293 - PT enhancement CH3 END- Added regex condition for Receipt Number and Account Number Textbox validations.


                if (this._Application.SelectedItem.Text == "All")
                    srchCrit1.App = "-1";
                else
                    srchCrit1.App = Convert.ToString(this._Application.SelectedItem.Text.Trim());
                srchCrit1.App = Convert.ToString(this._Application.SelectedItem.Value);
                srchCrit1.ProductCode = Convert.ToString(this._ProductType.SelectedItem.Value);
                //Begin PC Phase II Changes CH1 - Removed the Relevant code for the Fields removed for Transaction Search UI
                //CHG0078293 - PT enhancement CH2 START- Added the below condition to restrict the date condition to two days to search with the new search fields and assign the values  to the search criteria
                //MAIG - CH5 - BEGIN - for adding Agent & User IDs in Search Criteria and trim functionality
                if (this.txtAmount.Text.Trim() != "" || this.txtEcheckAcc.Text.Trim() != "" || this.txtCCSuffix.Text.Trim() != "" || this.txtDO.Text.Trim() != "" || this.txtAgentId.Text.Trim() != "" || this.txtUserId.Text.Trim() != "")
                //MAIG - CH5 - END - for adding Agent & User IDs in Search Criteria and trim functionality
                {

                    try
                    {
						//MAIG - CH6 - BEGIN - Added Trim to defer any white space
                        if (this.txtAmount.Text.Trim() != "" && Convert.ToDecimal(this.txtAmount.Text.Trim()) <= 0)
						//MAIG - CH6 - END - Added Trim to defer any white space
                        {
                            throw new FormatException();
                        }
                        else
                        {

                            if ((dt_start_date.AddDays(2)) >= (dt_end_date))
                            {
                                if (this.txtAmount.Text.Trim() != "")
                                {
                                    srchCrit1.Amount = Convert.ToDecimal(this.txtAmount.Text.Trim());
                                }
                                if (this.txtCCSuffix.Text.Trim() != "")
                                {
                                    srchCrit1.CCNumber = this.txtCCSuffix.Text.Trim();
                                }
                                if (this.txtEcheckAcc.Text.Trim() != "")
                                {
                                    srchCrit1.eChckAccNbr = this.txtEcheckAcc.Text.Trim();
                                }
                                if (this.txtDO.Text.Trim() != "")
                                {
                                    srchCrit1.DO = this.txtDO.Text.Trim();
                                }

                                //MAIG - CH7 - BEGIN - for adding Agent & User IDs in Search Criteria and trim functionality
                                if (this.txtAgentId.Text.Trim() != "")
                                {
                                    srchCrit1.AgentID = this.txtAgentId.Text.Trim();
                                }
                                if (this.txtUserId.Text.Trim() != "")
                                {
                                    srchCrit1.UserID = this.txtUserId.Text.Trim();
                                }
                                //MAIG - CH7 - END - for adding Agent & User IDs in Search Criteria and trim functionality
                            }
                            else if (this.txtAmount.Text.Trim() != "")
                            {
                                rptLib.SetMessage(lblErrMsg, "Please limit the date range to two days for searching by Amount", true);
                                InvisibleControls();
                                return;
                            }
                            else if (this.txtCCSuffix.Text.Trim() != "")
                            {
                                rptLib.SetMessage(lblErrMsg, "Please limit the date range to two days for searching by Credit card number", true);
                                InvisibleControls();
                                return;
                            }
                            else if (this.txtEcheckAcc.Text.Trim() != "")
                            {
                                //CHG0109406 - CH5 - Begin - Change to update ECHECK to ACH - 01202015
                                rptLib.SetMessage(lblErrMsg, "Please limit the date range to two days for searching by ACH Account number", true);
                                //CHG0109406 - CH5 - End - Change to update ECHECK to ACH - 01202015
                                InvisibleControls();
                                return;
                            }
                            else if (this.txtDO.Text.Trim() != "")
                            {
                                rptLib.SetMessage(lblErrMsg, "Please limit the date range to two days for searching by Location", true);
                                InvisibleControls();
                                return;
                            }
                            //MAIG - CH8 - BEGIN - for adding Agent & User IDs in Search Criteria and trim functionality
                            else if (this.txtAgentId.Text.Trim() != "")
                            {
                                rptLib.SetMessage(lblErrMsg, "Please limit the date range to two days for searching by AgentID", true);
                                InvisibleControls();
                                return;
                            }
                            else if (this.txtUserId.Text.Trim() != "")
                            {
                                rptLib.SetMessage(lblErrMsg, "Please limit the date range to two days for searching by UserID", true);
                                InvisibleControls();
                                return;
                            }
                            //MAIG - CH8 - END - for adding Agent & User IDs in Search Criteria and trim functionality
                            // END
                        }
                    }
                    catch (FormatException)
                    {
                        rptLib.SetMessage(lblErrMsg, "Please specify a valid amount", true);
                        InvisibleControls();
                        return;
                    }
                    //CHG0078293 - PT enhancement CH2 END- Added the below condition to restrict the date condition to two days to search with the new search fields and assign the values  to the search criteria
                }
                //End PC Phase II Changes CH1 - Removed the Relevant code for the Fields removed for Transaction Search UI
                //srchCrit1.MerchantRef = this.txtMerchantRefNbr.Text;
                //END
                // .Modified by Cognizant based on new SP Changes
                //CHG0078293 - PT enhancement CH4 START- Added regex condition for Receipt Number and Account Number Textbox validations.
				//MAIG - CH9 - BEGIN - Added Trim to defer any white space
                if (this.txtAccountNbr.Text.Trim() == string.Empty || rx.IsMatch(this.txtAccountNbr.Text.Trim()))
                {
                    srchCrit1.AccountNbr = this.txtAccountNbr.Text.Trim();
					//MAIG - CH9 - END - Added Trim to defer any white space
                }
                else
                {
                    rptLib.SetMessage(lblErrMsg, "Please specify a valid Account Number", true);
                    InvisibleControls();
                    return;
                }
                //CHG0078293 - PT enhancement CH4 END- Added regex condition for Receipt Number and Account Number Textbox validations.
                //PC Phase II Changes CH1 - Removed the Relevant code for the Fields removed for Transaction Search UI
                //srchCrit1.LastName = this.txtLastName.Text;
                //srchCrit1.CCAuthCode = this.txtCCAuthCode.Text;
                //srchCrit1.CCNumber = sCCSignature;
                //HO6.Ch1:Added echeck account number to search with account number with encrypted format as a part of HO6 on 04-21-2010.
                //srchCrit1.eChckAccNbr=sEcheckSignature;
                InsuranceClasses.Service.Insurance InsSvc = new InsuranceClasses.Service.Insurance();
                //Transaction Search Log Enable - Start.
                if (ConfigurationManager.AppSettings["TransactionSearchLogEnable"] != "0")
                {
                    Logger.Log(CSAAWeb.Constants.TRANS_SEARCH_USERID + Page.User.Identity.Name.ToString() + CSAAWeb.Constants.PARAMETER + srchCrit1);
                }
                //Transaction Search Log Enable - End.
                DataSet ds = InsSvc.NewSearchOrders(srchCrit1);
                Logger.Log(CSAAWeb.Constants.TRANS_SEARCH_DATASET);
                //.Modified by Cognizant based on new SP Changes
                if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
                {                    
                    rptLib.SetMessage(lblErrMsg, "No Data Found. Please specify a valid criteria.", true);
                    InvisibleControls();
                    return;
                }
                else
                {
                    VisibleControls();
                }

                Session["MyDataSet"] = ds;
                //CHG0109406 - CH1 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                //Start-TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time  by cognizant.
                lblRunDate.Text = DateTime.Now.ToString() + " " + "Arizona";
                lblDateRange.Text = srchCrit1.StartDate + " - " + srchCrit1.EndDate.AddSeconds(-1) + " " + "Arizona";
                //End-TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time  by cognizant.
                //CHG0109406 - CH1 - END - Modified the timezone from "MST Arizona" to Arizona
                //CHG0110069 - CH1 - BEGIN - Appended the /Time along with the Date Label
                lblDate.Text = "Run Date/Time:";
                lblDates.Text = "Date/Time Range:";
                //CHG0110069 - CH1 - END - Appended the /Time along with the Date Label
                // For printing and converting to Excel only, folowing session variables are declared.
                Session["DateRange"] = dt_start_date + " - " + dt_end_date.AddSeconds(-1);
                // START - Added by Cognizant on 13/12/2010 as part of .NET 3.5 Migration
                this.dgSearch2.RowDataBound += new GridViewRowEventHandler(this.ItemDataBound2);
                this.dgSearch2.PageIndexChanging += new GridViewPageEventHandler(this.PageIndexChanged2);
                // END
                UpdateDataView();

            }


            catch (FormatException)
            {
                string invalidDate = @"<Script>alert('Please put in a valid date.')</Script>";
                Response.Write(invalidDate);
                lblPageNum1.Visible = false;
                lblPageNum2.Visible = false;
                return;
            }
            catch (Exception ex)
            {
                rptLib.SetMessage(lblErrMsg, MSCRCore.Constants.MSG_GENERAL_ERROR, true);
                //SR # 8494956 :Added  method  InvisibleControls() to btn_submitclick  by cognizant on Oct 20 2009 as a part of Transaction Search Deadlock issue.
                InvisibleControls();
                Logger.Log(ex);
            }

        }

        // EVENT HANDLER: ItemDataBound
        public void ItemDataBound2(Object sender, GridViewRowEventArgs e)
        {
            DataRowView drv = (DataRowView)e.Row.DataItem;
            if (drv == null)
                return;

            SetNonSTARWidths(e);

            //MAIG - CH10 - START - Adjusted cell count to accomodate newly added Policy State & Txn Type columns
            e.Row.Cells[10].Text = (String.Format("{0:c}", drv["Amount"])).ToString();
            e.Row.Cells[11].Text = (String.Format("{0:c}", drv["Order Total"])).ToString();

            //CHG0109406 - CH6 - BEGIN - Change to update ECHECK to ACH - 01202015
            if (drv["Credit Card / ACH Number"].ToString() != "")
                //Remove Encrypt/Decrypt.Ch1 : Modified by cognizant as a part of removing encript and decrypt on Credit card and echeck on 07-21-2010.
                //e.Row.Cells[12].Text = Cryptor.Decrypt(drv["Credit Card / eCheck Number"].ToString(),Config.Setting("CSAA_ORDERS_KEY"));
                e.Row.Cells[14].Text = drv["Credit Card / ACH Number"].ToString();
	            //MAIG - CH10 - END - Adjusted cell count to accomodate newly added Policy State & Txn Type columns
            //CHG0109406 - CH6 - END - Change to update ECHECK to ACH - 01202015
            //.Changed by Cognizant to hide Duplicate button for STAR transaction
            //CSR#3937.Ch2 - START - Changed the code for the new way of accessing the constants (web.config changes)
            //if((!Convert.IsDBNull(drv["Receipt Number"])) && (drv["Receipt Number"].ToString().Trim() != "") && (drv["Appl."].ToString().Trim() != ((StringDictionary)Application["Constants"])["ApplicationType.Star"].Trim()))

            //Q4-Retrofit.Ch1 -START
            /*Removed the condition (drv["Appl."].ToString().Trim() != Config.Setting("ApplicationType.Star")) in the IF loop
            to display the Duplicate Receipt button for the STAR CUI transactions*/

            if ((!Convert.IsDBNull(drv["Receipt Number"])) && (drv["Receipt Number"].ToString().Trim() != ""))

            //Q4-Retrofit.Ch1-END 

            //CSR#3937.Ch2 - END - Changed the code for the new way of accessing the constants (web.config changes)
            {
                ImageButton btnDuplicateReceipt = new ImageButton();
                btnDuplicateReceipt.CausesValidation = false;
                btnDuplicateReceipt.ToolTip = "Print Duplicate Receipt for this transaction";
                //External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
				//MAIG - CH11 - BEGIN - for displaying ALT Text for Firefox Print button issue
                btnDuplicateReceipt.ImageUrl = "\\PaymentToolimages\\btn_Print_Version.gif";

                btnDuplicateReceipt.AlternateText = "Print Receipt";
                //MAIG - CH11 - END - for displaying ALT Text for Firefox Print button issue

                //The Parameters for the javascript function Open_Receipt are Receipt number and Receipt Type(DC for Duplicate Copy)
                btnDuplicateReceipt.Attributes.Add("onclick", "return Open_Receipt('" + (drv["Receipt Number"].ToString()) + "','" + "DC" + "')");
                //CSR 3824:ch1 - Modified by Cognizant on 08/04/2005 to display the Status Column in Transaction Search
                //STAR Retrofit III.Ch4: START - Modified the cell index of DuplicateReceipt column.
                //MAIG - CH12 - BEGIN - for displaying ALT Text for Firefox Print button issue & adjsuted the cell size
                e.Row.Cells[21].Controls.Add(btnDuplicateReceipt);

                e.Row.Cells[21].Font.Name = "arial,helvetica,sans-serif";
                e.Row.Cells[21].Font.Size = 2;
                e.Row.Cells[21].Font.Underline = true;
                //MAIG - CH12 - END - for displaying ALT Text for Firefox Print button issue & adjsuted the cell size

                //STAR Retrofit III.Ch4: END
                //End

            }
        }

        public void PageIndexChanged2(Object sender, GridViewPageEventArgs e)
        {
            if (e.NewPageIndex == -1)
            {
                dgSearch2.PageIndex = 0;
            }
            else
            {

                dgSearch2.PageIndex = e.NewPageIndex;
            }
            UpdateDataView();
        }

        private void UpdateDataView()
        {

            // Retrieves the data
            DataSet ods = (DataSet)Session["MyDataSet"];

            if (ods == null)
            {
                rptLib.SetMessage(lblErrMsg, "Your session has timed out. Please re-submit the page", true);
                return;
            }

            //START - Code added by COGNIZANT 07/16/2004 - For adding the column to display the Duplicate 
            //	Receipt button column
            if (ods.Tables[0].Rows.Count > 0)
            {
                //MAIG - CH13 - BEGIN - Gave a column name for the last column - MAIG
                ods.Tables[0].Columns.Add("Duplicate Receipt");
                //MAIG - CH13 - END - Gave a column name for the last column - MAIG
            }


            DataView dv1 = ods.Tables[0].DefaultView;
            //DataView dv2 = ods.Tables[1].DefaultView;

            int pageCnt = 1;
            int currPage = 1;

            if (ods.Tables[0].Rows.Count > 0)
            {
                dgSearch2.DataSource = dv1;
                dgSearch2.DataBind();

                //START - Code added by COGNIZANT 07/16/2004 - To remove the Duplicate Receipt button column
                //	from the dataset.
                //MAIG - CH14 - BEGIN - Gave a column name for the last column - MAIG
                ods.Tables[0].Columns.Remove("Duplicate Receipt");
                //MAIG - CH14 - END - Gave a column name for the last column - MAIG
                //END


                dgSearch2.Visible = true;
                lblMbrTitle.Visible = true;
                //CHG0109406 - CH4 - BEGIN - Set the below label visible property to True which displays the timezone in results
                lblHeaderTimeZone.Visible = true;
                //CHG0109406 - CH4 - END - Set the below label visible property to True which displays the timezone in results
                //MAIG - CH15 - BEGIN - Renamed the Report Header - MAIG
                lblMbrTitle.Text = "Search Result(s)";
                //MAIG - CH15 - END - Renamed the Report Header - MAIG

                if (pageCnt < dgSearch2.PageCount) pageCnt = dgSearch2.PageCount;
                if (currPage < dgSearch2.PageIndex + 1) currPage = dgSearch2.PageIndex + 1;
            }

            lblPageNum1.Text = "Showing Page " + currPage.ToString() + " of " + pageCnt.ToString();
            lblPageNum2.Text = "Showing Page " + currPage.ToString() + " of " + pageCnt.ToString();

        }


        //STAR Retrofit III.Ch3: START - Commented the SelectedIndexChanged events of month selection to prevent roundtrips to server.
        //		private void start_date_month_SelectedIndexChanged(object sender, System.EventArgs e)
        //		{
        //			rptLib.DynamicDate(start_date_day, start_date_month, start_date_year);
        //
        //			
        //			//START - Code added by COGNIZANT 07/16/2004 - To retain the previous search results when
        //			//the selected start date month is changed.
        //			DataSet ods = (DataSet) Session["MyDataSet"];
        //
        //			if (ods != null)
        //			{
        //				UpdateDataView();
        //			}
        //			//END
        //			
        //		}
        //
        //		private void end_date_month_SelectedIndexChanged(object sender, System.EventArgs e)
        //		{
        //			rptLib.DynamicDate(end_date_day, end_date_month, end_date_year);
        //			end_date_day.SelectedIndex = end_date_day.Items.Count - 1;
        //
        //			//START - Code added by COGNIZANT 07/16/2004 - To retain the previous search results when
        //			//the selected end date month is changed.
        //			DataSet ods = (DataSet) Session["MyDataSet"];
        //
        //			if (ods != null)
        //			{
        //				UpdateDataView();
        //			}
        //			//END
        //		}
        //STAR Retrofit III.Ch3: END

        private void VisibleControls()
        {
            //CHG0109406 - CH7 - BEGIN - Updated the table to be visible when the results are shown
            tblHeader.Visible = true;
            //CHG0109406 - CH7 - END - Updated the table to be visible when the results are shown
            pnlTitle.Visible = true;
            lblPageNum1.Visible = true;
            lblPageNum2.Visible = true;
            lblDate.Visible = true;
            lblRunDate.Visible = true;
            lblDates.Visible = true;
            lblDateRange.Visible = true;
        }
        private void InvisibleControls()
        {
            lblErrMsg.Visible = true;
            //lblErrMsg.Text = "No Data Found. Please specify a valid criteria.";
            //CHG0109406 - CH8 - BEGIN - Updated the table to be hidden when the results are shown
            tblHeader.Visible = false;
            //CHG0109406 - CH8 - END - Updated the table to be hidden when the results are shown
            lblPageNum1.Visible = false;
            lblPageNum2.Visible = false;
            lblDate.Visible = false;
            lblRunDate.Visible = false;
            lblDates.Visible = false;
            lblDateRange.Visible = false;
            //START - Code added by COGNIZANT 07/16/2004 - To clear the previous search results when
            //	some error is encountered in the current search
            Session.Contents.Clear();
            Session.Abandon();
            //END
        }


        //START - Code added by COGNIZANT 07/16/2004 - To specify widths for the Non STAR transactions datagrid		
        private void SetNonSTARWidths(GridViewRowEventArgs e)
        {
            e.Row.Cells[0].Width = 70;	//Application
            e.Row.Cells[1].Width = 50;	//Product
			//MAIG - CH16 - BEGIN - Modified the Width from 70 to 30
            e.Row.Cells[2].Width = 30;	//Policy / Member Number
			//MAIG - CH16 - END - Modified the Width from 70 to 30
            e.Row.Cells[3].Width = 70;	//Receipt Number
            e.Row.Cells[4].Width = 50;	//Customer Name
            e.Row.Cells[5].Width = 73;	//Transaction Date
            e.Row.Cells[6].Width = 40;	//Transaction Type
            e.Row.Cells[7].Width = 60;	//Amount
            e.Row.Cells[8].Width = 60;//Order Total 
            e.Row.Cells[9].Width = 30;	//Pymt. Method
            e.Row.Cells[10].Width = 100;//Request ID
            e.Row.Cells[11].Width = 30;//Auth Code
            e.Row.Cells[12].Width = 110;// Credit Card Number 
            e.Row.Cells[13].Width = 75;// Merchant Reference 
            e.Row.Cells[14].Width = 85;// Users 
            e.Row.Cells[15].Width = 20;// Rep DO
            //CSR 3824: ch1 Modified by Cognizant 08/05/2005 to display the Status Column in Transaction Search
            e.Row.Cells[16].Width = 73;// Status Column
            //STAR Retrofit III.Ch5: - START Modified to display two new columns Approved By and Approved date in the Transaction Search result grid.
            e.Row.Cells[17].Width = 80;//Approved By 
            e.Row.Cells[18].Width = 73;//Approved Date 
            e.Row.Cells[19].Width = 130;// Duplicate Receipt button
            //STAR Retrofit III.Ch5: END
            //END
        }
        //END

        #region Cache
        private DataTable Applications
        {
            get { return (DataTable)Cache["ApplicationsNew"]; }
        }
        #endregion

        #region AddSelectItem
        /// <summary>
        /// Adds a Select Item row to the table.
        /// </summary>
        private void AddSelectItem(DataTable Dt, string Description, string ID)
        {
            DataRow Row = Dt.NewRow();
            Row["ID"] = ID;
            Row["Description"] = Description;
            Dt.Rows.InsertAt(Row, 0);
        }
        #endregion


        #region _Application_SelectedIndexChanged
        // .Modified by Cognizant Changed based on SP changes
        //		private void _Application_SelectedIndexChanged(object sender, System.EventArgs e)
        //		{
        //			// To Add product Types based on Application Ids
        //
        //			Order DataConnection = new Order(Page);
        //
        //			Dt = DataConnection.LookupDataSet("Insurance", "ApplicationProductTypes",new object[] {Convert.ToInt32(_Application.SelectedItem.Value)}).Tables["ApplicationProductTypes"];
        //			if (Dt.Rows.Count>1) AddSelectItem(Dt,"All","-1");
        //			_ProductType.DataSource = Dt;
        //			_ProductType.DataBind();
        //			DataConnection.Close();
        //
        //			//START - Code added by COGNIZANT 07/16/2004 - To retain the previous search results when
        //			//the selected start date month is changed.
        //			DataSet ods = (DataSet) Session["MyDataSet"];
        //
        //			if (ods != null)
        //			{
        //				UpdateDataView();
        //			}
        //			//END
        //		}

        #endregion

    }
}
