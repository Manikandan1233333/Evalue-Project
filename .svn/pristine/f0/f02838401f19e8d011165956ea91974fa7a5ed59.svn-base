/*
 MODIFIED BY COGNIZANT AS A PART OF NAME CHANGE PROJECT ON 8/20/2010
 * NameChange.Ch1:Changed the header text from CSAA to AAA NCNU IE 
 //67811A0 - START -PCI Remediation for Payment systems Start: Added to fix the Object reference exception by cognizant on 1/11/2012
 //CHG0083477 - Name Change from AAA NCNU IE to CSAA IG as part of Name change project on 11/15/2013.
 * CHG0104053 - PAS HO CH1 - Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/16/2014
 * CHG0104053 - PAS HO CH2 - Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/16/2014
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
using MSCR.Reports;
using CSAAWeb;


namespace MSCR.Reports
{
	/// <summary>
	/// Summary description for CashierRecon_Confirmation.
	/// </summary>
	public partial class CashierRecon_Confirmation : CSAAWeb.WebControls.PageTemplate
	{

		protected System.Data.DataTable dtbRepeater;
		protected System.Data.DataTable dtbOtherRepeater;
		protected System.Data.DataTable dtbTotalRepeater;


		protected CollectionSummaryLibrary CollectionSummary = new 	CollectionSummaryLibrary();		
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			tblCollSummary.Attributes.Add("width","0");
			tblOtherCollSummary.Attributes.Add("width","0");
			lblMessage.Text = "Transactions have been approved by Cashier ";
            //67811A0 - START -PCI Remediation for Payment systems Start: Added to fix the Object reference exception by cognizant on 1/11/2012
            Session["UserID"] = Page.User.Identity.Name;
            //67811A0 - START -PCI Remediation for Payment systems End: Added to fix the Object reference exception by cognizant on 1/11/2012
			lblMessage.Text = lblMessage.Text + Session["UserID"].ToString().ToUpper();
			LoadGridData();
		}

		private void LoadGridData()
		{

			dtbRepeater = (DataTable)Session["CashierCSAAGrid"];
			dtbOtherRepeater = (DataTable)Session["CashierOtherGrid"];
			dtbTotalRepeater = (DataTable)Session["CashierTotalGrid"];
            //CHG0083477 - Name Change from AAA NCNU IE to CSAA IG as part of Name change project on 11/15/2013.
            //NameChange.Ch1:Modified the header  to AAA NCNU IE from CSAA by cognizant as a part of name change project on 8/20/2010.
            //CHG0104053 - PAS HO CH1 - START : Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/16/2014
            CollectionSummary.CreateHeader(ref tblCollSummary, ref dtbRepeater, "TURN-IN PRODUCTS");
            //CHG0104053 - PAS HO CH1 - END : Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/16/2014
			CollectionSummary.AddCollSummaryData(ref tblCollSummary, ref dtbRepeater);
			double dblTotal = CollectionSummary.AddCollSummaryFooter(ref tblCollSummary, ref dtbRepeater);

			if (dblTotal > 0)
			{
				trCollSummarySplit.Visible = true;
				trCollSummary.Visible = true;
			}
			else
			{
				trCollSummarySplit.Visible = false;
				trCollSummary.Visible = false;
			}
            //CHG0104053 - PAS HO CH2 - START : Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/16/2014
			CollectionSummary.CreateHeader(ref tblOtherCollSummary, ref dtbOtherRepeater,"WU/ACA PRODUCTS");
            //CHG0104053 - PAS HO CH2 - END : Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/16/2014
			CollectionSummary.AddCollSummaryData(ref tblOtherCollSummary, ref dtbOtherRepeater);
			dblTotal = CollectionSummary.AddCollSummaryFooter(ref tblOtherCollSummary, ref dtbOtherRepeater);

			//To Check the Sub Total column Value > 0 
			if (dblTotal > 0)
			{
				trOtherCollSummary.Visible = true;
				trOtherCollSummarySplit.Visible = true;
			}
			else
			{
				trOtherCollSummary.Visible = false;
				trOtherCollSummarySplit.Visible = false;
			}

			CollectionSummary.CreateTotalHeader(ref tblTotalCollSummary,ref dtbTotalRepeater);
			CollectionSummary.AddCollTotalSummaryData(ref tblTotalCollSummary ,ref dtbTotalRepeater);
			dblTotal = CollectionSummary.AddCollTotalSummaryFooter(ref tblTotalCollSummary,ref dtbTotalRepeater); 

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

		protected void btnBackToReports_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("CashierReconciliation_Report.aspx");
		}
	}
}
