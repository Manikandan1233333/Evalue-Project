/*
 * History:
 * MODIFIED BY COGNIZANT AS PART OF Q4 RETROFIT CHANGES
 * 12/5/2005 Q4-retrofit.ch1:Modified the Code to create collection summary grids for CSAA 
 *       and Other Product Collection Summary There is also Total Collection summary grid
 *       provided
 * MODIFIED BY COGNIZANT AS A PART OF NAME CHANGE PROJECT ON 08/23/2010
 * NameChange.Ch1:Modified the code to change the caption as "AAA NCNU IE PRODUCTS" instead of "CSAA PRODUCTS".
 * //67811A0  - PCI Remediation for Payment systems CH1: Modified the code to hit the data base again to load the session value which is getting null by cognizant on 1/30/2011
 * //67811A0  - PCI Remediation for Payment systems CH2: Modified the code to check null for session and load the session using database call by cognizant on 1/30/2011
 * CHG0083477 - Name Change from AAA NCNU IE to CSAA IG as part of Name change project on 11/15/2013.
 * CHG0104053 - PAS HO CH1 - Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/16/2014
 * CHG0104053 - PAS HO CH2 - Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/16/2014
 * CHGXXXXXX - Removing btnMbrDetail,btnCrossRef as part of Code Clean Up - March 2016
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

namespace MSCR.Reports
{
    /// <summary>
    /// This confirmation page gets invoked when a Sales Rep turns in transaction
    /// for the Cashier's approval.
    /// This page displays the summary of the transactions which have been turned in
    /// for approval by the Sales Rep. 
    /// This page also has links to print the following reports
    /// 1. Collection Turn in List
    /// 2. Membership Detail Report
    /// 3. Cross Reference Report
    /// </summary>
    public partial class SalesRep_Confirmation : CSAAWeb.WebControls.PageTemplate
    {
        protected DataTable dtbRepeater;
        protected System.Web.UI.WebControls.ImageButton imgbtnCollTurninList;
        protected System.Web.UI.WebControls.ImageButton imgbtnMbrDetail;
        protected System.Web.UI.WebControls.ImageButton imgbtnCrossRef;
        // START - The Report Type codes are being stored as Constants
        const string SALESTURNIN_REPORT_TYPE = "1";
        const string MEMBERSHIP_REPORT_TYPE = "2";
        const string CROSSREFERENCE_REPORT_TYPE = "3";
        // END

        //Q4-retrofit.ch1-START :Added Declarations for Other Product Collection Summary and Total Collection Summary Grids
        protected System.Data.DataTable dtbOtherRepeater;
        protected System.Data.DataTable dtbTotalRepeater;

        //Q4-retrofit.ch1-END 
        CollectionSummaryLibrary CollectionSummary = new CollectionSummaryLibrary();
        //67811A0  - PCI Remediation for Payment systems CH1: Start modified the code to hit the data base again to load the session value which is getting null by cognizant on 1/30/2011

        TurninClasses.Service.Turnin TurninService;
        Hashtable hsTable = new Hashtable();
        string CurrentUser;


        private void loadReportType()
        {
            DataSet dtsReportTypes = TurninService.GetTurnInReportTypes(CurrentUser, "W");
            HidddenReportType.DataSource = dtsReportTypes.Tables[0];
            HidddenReportType.DataTextField = dtsReportTypes.Tables[0].Columns["Description"].ToString();
            HidddenReportType.DataValueField = dtsReportTypes.Tables[0].Columns["ID"].ToString();
            HidddenReportType.DataBind();
            //Assigning the Report Type IDs to the Report Type Description using Hash Table
            for (int indexReportType = 0; indexReportType < HidddenReportType.Items.Count; indexReportType++)
            {
                hsTable[HidddenReportType.Items[indexReportType].Value] = HidddenReportType.Items[indexReportType].Value;
            }

        }
        //67811A0  - PCI Remediation for Payment systems CH1: END modified the code to hit the data base again to load the session value which is getting null by cognizant on 1/30/2011
        #region Page_Load
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //Q4-retrofit.ch1-START :Added the code for Hiding CSAA product and Other Product Collection Summary grids
            tabCollSummary.Visible = false;
            trCollOtherSummary.Visible = false;
            tblCollOtherSummary.Visible = false;
            //Q4-retrofit.ch1-END
            //67811A0  - PCI Remediation for Payment systems CH2: Start modified the code to check null for session and load the session using database call by cognizant on 1/30/2011
            CurrentUser = Page.User.Identity.Name;
            TurninService = new TurninClasses.Service.Turnin();
            if (Session["ReportType"] == null)
            {
                loadReportType();
                Session.Add("ReportType", hsTable);


            }
            else
            {
                hsTable = (Hashtable)Session["ReportType"];
            }
            //67811A0  - PCI Remediation for Payment systems CH2: End modified the code to check null for session and load the session using database call by cognizant on 1/30/2011
            //For initialising the Collection Summary Width to zero in the PostBack
            tabCollSummary.Attributes.Add("Width", "0");
            btnCollTurninList.Attributes.Add("onclick", "return Open_Report('" + hsTable[SALESTURNIN_REPORT_TYPE] + "')");
            //CHGXXXXXXX- Server migration - Removing the buttons (btnMbrDetail,btnCrossRef) as as part of Code Clean Up - March 2016 
            if (!IsPostBack)
            {
                //Load Data and populate the Collection Summary grid
                LoadGridData();
                Session["TurnInGridData"] = null;
            }

        }
        #endregion

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

        #region LoadGridData
        /// <summary>
        /// Load and populate the Collection Summary grid
        /// </summary>
        private void LoadGridData()
        {
            //Q4-Retrofit.ch1-START :Modified the Code to include Other Product Collection Summary
            if (Session["TurnInGridData"] == null || Session["TurnInGridDataOtherProducts"] == null)
            //Q4-Retrofit.ch1-END
            //if(Session["TurnInGridData"] == null) (Removed as part of Q4 Retrofit)
            {
                lblErrMsg.Visible = true;
                lblErrMsg.Text = "Your session has timed out. You cannot view the Collection Summary. ";
                lblErrMsg.Text = lblErrMsg.Text + " Please re-submit the page.";
                return;
            }
            else
            {
                lblErrMsg.Visible = false;
                dtbRepeater = (DataTable)Session["TurnInGridData"];
                //CHG0083477 - Name Change from AAA NCNU IE to CSAA IG as part of Name change project on 11/15/2013.
                //Q4-Retrofit.ch1-START:Modified the code to change the caption as "CSAA Products" instead of "COLLECTION SUMMARY"
                //NameChange.ch1-START:Modified the code to change the caption as "AAA NCNU IE PRODUCTS" instead of "CSAA PRODUCTS" on 8/23/2010.
                //CHG0104053 - PAS HO CH1 - START : Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/16/2014
                CollectionSummary.CreateHeader(ref tabCollSummary, ref dtbRepeater, "TURN-IN PRODUCTS");
                //CHG0104053 - PAS HO CH1 - END : Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/16/2014
                //Q4-Retrofit.ch1-END
                CollectionSummary.AddCollSummaryData(ref tabCollSummary, ref dtbRepeater);
                double dblTotal = CollectionSummary.AddCollSummaryFooter(ref tabCollSummary, ref dtbRepeater);

                //Q4-Retrofit.ch1-START :Modified the code to display the CSAA Summary Grid only when the total Amount is > 0
                if (dblTotal > 0)
                {
                    tabCollSummary.Visible = true;
                    trCollOtherSummary.Visible = true;
                }
                else
                {
                    tabCollSummary.Visible = false;
                    trCollOtherSummary.Visible = false;
                }
                //Q4-Retrofit.ch1-END

                //Q4-Retrofit.ch1-START :Added the Code to create Other Product Collection Summary Grid
                dtbOtherRepeater = (DataTable)Session["TurnInGridDataOtherProducts"];
                //CHG0104053 - PAS HO CH2 - START : Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/16/2014
                CollectionSummary.CreateHeader(ref tblCollOtherSummary, ref dtbOtherRepeater, "WU/ACA PRODUCTS");
                //CHG0104053 - PAS HO CH2 - END : Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/16/2014
                CollectionSummary.AddCollSummaryData(ref tblCollOtherSummary, ref dtbOtherRepeater);
                dblTotal = CollectionSummary.AddCollSummaryFooter(ref tblCollOtherSummary, ref dtbOtherRepeater);

                if (dblTotal > 0)
                {
                    tblCollOtherSummary.Visible = true;
                    trCollTotalSummary.Visible = true;
                }
                else
                {
                    tblCollOtherSummary.Visible = false;
                    trCollTotalSummary.Visible = false;
                }
                //Q4-Retrofit.ch1-END

                //Q4-Retrofit.ch1-START :Added the Code to create Total Collection Summary Grid
                dtbTotalRepeater = (DataTable)Session["TurnInGridDataTotal"];
                CollectionSummary.CreateTotalHeader(ref tblCollTotalSummary, ref dtbTotalRepeater);
                CollectionSummary.AddCollTotalSummaryData(ref tblCollTotalSummary, ref dtbTotalRepeater);
                CollectionSummary.AddCollTotalSummaryFooter(ref tblCollTotalSummary, ref dtbTotalRepeater);
                //Q4-Retrofit.ch1-END
            }
        }
        #endregion


    }
}
