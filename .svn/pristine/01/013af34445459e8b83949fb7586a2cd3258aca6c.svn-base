/*	REVISION HISTORY:
 *	07/26/2005	-	MODIFIED BY COGNIZANT
 *	Changes Done:
 *	CSR#3937.Ch1 :	Renamed the class InsSearchXlsPrint_New to InsSearchXlsPrint
 *  CSR#3824.Ch1 : Modified to add a new Status Column in the Transaction Search
 * 
 * STAR Retrofit III Changes: 
 * Modified as a part of CSR #5595
 *  3/27/2007 STAR Retrofit III.Ch1: 
 *				Modified to display two new columns Approved By and Approved date in the Transaction Search result grid.
 * Modified as part of HO6 on 04-21-2010.
 * HO6.Ch1:Changed the  column name to display the eCheck account number
 * 
 * Modified by cognizant as a part of removing encrypt and decrypt oncredit card and echek on 07-21-2010.
 * Ch1 : Modified ItemDatabound1 event
 * MODIFIED AS A PART OF TIME ZONE CHANGE ENHANCEMENT ON 8-31-2010
 * TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time and Run Date  by cognizant.
 * MAIG - CH1 - 09/15/2014 - Adjusted cell count to accomodate newly added Policy State & Txn Type columns  - MAIG
 * MAIG - CH2 - Modified the width from 70 to 30
 * CHG0109406 - CH1 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH2 - Change to update ECHECK to ACH - 01202015
 * CHG0109406 - CH3 - Change to update ECHECK to ACH - 01202015
 * CHG0109406 - CH4 - Change to update the title for the Print page in Search Transaction
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
using System.Configuration;
using CSAAWeb;
using CSAAWeb.AppLogger;


namespace MSCR.Reports
{
	/// <summary>
	/// Summary description for InsSearchXlsPrint.
	/// </summary>
	
	//CSR#3937.Ch1 : START - Renamed the class InsSearchXlsPrint_New to InsSearchXlsPrint
	public partial class InsSearchXlsPrint : System.Web.UI.Page
	//CSR#3937.Ch1 : END - Renamed the class InsSearchXlsPrint_New to InsSearchXlsPrint
	{

//		protected DataSet dsSearch;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here

			try
			{
				// Put user code to initialize the page here
				/*
				START - Code added by COGNIZANT 06/17/2004
				This modification has been done to prevent the background colour of the label from getting displayed
				when no insurance or membership search results are present.
				Assigned the ID tblIns for the table containing the Insurance datagrid and
				tblMbr for the table containing Membership datagrid and made both the tables as Server controls
				Upon page load both the tables containing the insurance and membership datagrids are hidden
				*/
				tblIns.Visible = false;
				tblMbr.Visible = false;
				//END

				DataSet dsSearch = (DataSet) Session["MyDataSet"];
				if (dsSearch == null)
				{
					Response.Write("Your session has timed out. Please re-submit the page");
					return;
				}
                //CHG0109406 - CH1 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                //Start-TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time and Run Date  by cognizant.
                lblRunDate.Text = DateTime.Now.ToString() + " " + "Arizona";
                lblDateRange.Text = (string)Session["DateRange"] + " " + "Arizona";
                //End-TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time and Run Date  by cognizant.
                //CHG0109406 - CH1 - END - Modified the timezone from "MST Arizona" to Arizona
				DataView dv1 = dsSearch.Tables[0].DefaultView;
				// .Modified by Cognizant based on new SP changes				
				if (Request.QueryString["type"] == "XLS") 
				{
					Response.ContentType = "application/vnd.ms-excel";
					Response.Charset = ""; //Remove the charset from the Content-Type header.
				}
				
				Page.EnableViewState = false; //Turn off the view	state.

				if (dsSearch.Tables[0].Rows.Count > 0)
				{
					dgSearch1.DataSource = dv1;
					dgSearch1.DataBind();
                    //CHG0109406 - CH4 - BEGIN - Change to update the title for the Print page in Search Transaction
					//lblInsTitle.Text = "STAR Transactions and Non STAR Transactions";
                    lblInsTitle.Text = "Search Result(s)";
                    //CHG0109406 - CH4 - END - Change to update the title for the Print page in Search Transaction
                    
					// START - Code added by COGNIZANT 07/16/2004
					// To display the table containing the Insurance results and the label for the insurance search results
					tblIns.Visible = true;
					lblInsTitle.Visible = true;
					// END

				}
			}
			catch  (Exception ex) 
			{
				Logger.Log(ex);
				
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

		// EVENT HANDLER: ItemDataBound
		public void ItemDataBound1(Object sender, DataGridItemEventArgs e)
		{
			DataRowView drv = (DataRowView) e.Item.DataItem;
			if (drv == null)
				return;

			SetSTARWidths(e);

             //MAIG - CH1 - BEGIN - 09/15/2014 - Adjusted cell count to accomodate newly added Policy State & Txn Type columns  - MAIG
			e.Item.Cells[10].Text = (String.Format("{0:c}",drv["Amount"])).ToString();
			e.Item.Cells[11].Text = (String.Format("{0:c}",drv["Order Total"])).ToString();
			//HO6.Ch1:Changed the  column name to display the eCheck account number as a part of HO6 on 04-20-2010.
            //CHG0109406 - CH2 - BEGIN - Change to update ECHECK to ACH - 01202015
			if (drv["Credit Card / ACH Number"].ToString() != "")	
				//HO6.Ch1:Changed the  column name to display the eCheck account number as a part of HO6 on 04-20-2010.
                //Ch1 Start
                //Modified by cognizant as a part of removing encrypt and decrypt oncredit card and echek on 07-21-2010.
				//e.Item.Cells[12].Text = Cryptor.Decrypt(drv["Credit Card / eCheck Number"].ToString(),Config.Setting("CSAA_ORDERS_KEY"));
                e.Item.Cells[14].Text = drv["Credit Card / ACH Number"].ToString();
                //Ch1 End
            //CHG0109406 - CH2 - END - Change to update ECHECK to ACH - 01202015
			if (Request.QueryString["type"] == "XLS")  
			{
                ////if(!drv["Request ID"].ToString().Trim().Equals(""))
                ////    e.Item.Cells[13].Text = "'" + drv["Request ID"].ToString();
	
				if(!drv["Merchant Ref."].ToString().Trim().Equals(""))
					e.Item.Cells[15].Text = "'" + drv["Merchant Ref."].ToString();
//HO6.Ch1Changed the  column name to display the eCheck account number as a part of HO6 on 04-20-2010.
                //CHG0109406 - CH3 - BEGIN - Change to update ECHECK to ACH - 01202015
				if(!drv["Credit Card / ACH Number"].ToString().Trim().Equals(""))
					e.Item.Cells[14].Text = "'" + e.Item.Cells[14].Text;
                //CHG0109406 - CH3 - END - Change to update ECHECK to ACH - 01202015
                /////// MAIG - Begin - Commented out this section to avoid displaying Policy State in the Receipt number column
                ////// Added by Cognizant 2/8/2005 to enable proper format in excel sheet.
                ////if(!drv["Policy Number"].ToString().Trim().Equals(""))
                ////    e.Item.Cells[5].Text = "'" + e.Item.Cells[4].Text;
                /////// MAIG - End - Commented out this section to avoid displaying Policy State in the Receipt number column
			}
             //MAIG - CH1 - END - 09/15/2014 - Adjusted cell count to accomodate newly added Policy State & Txn Type columns  - MAIG


		}
		//START - Code added by COGNIZANT 07/16/2004 - To specify widths for the STAR transactions datagrid
		private void SetSTARWidths(DataGridItemEventArgs e)
		{
			e.Item.Cells[0].Width = 70;	//Application
			e.Item.Cells[1].Width = 50;	//Product
            //MAIG - CH2 - BEGIN - Modified the width from 70 to 30
			e.Item.Cells[2].Width = 30;	//Policy / Member Number
            //MAIG - CH2 - END - Modified the width from 70 to 30
			e.Item.Cells[3].Width = 70;	//Receipt Number
			e.Item.Cells[4].Width = 50;	//Customer Name
			e.Item.Cells[5].Width = 73;	//Transaction Date
			e.Item.Cells[6].Width = 40;	//Transaction Type
			e.Item.Cells[7].Width = 60;	//Amount
			e.Item.Cells[8].Width = 60;//Order Total 
			e.Item.Cells[9].Width = 30;	//Pymt. Method
			e.Item.Cells[10].Width = 100;//Request ID
			e.Item.Cells[11].Width = 30;//Auth Code
			e.Item.Cells[12].Width = 110;// Credit Card Number 
			e.Item.Cells[13].Width = 75;// Merchant Reference 
			e.Item.Cells[14].Width = 85;// Users 
			//CSR 3824:ch1 - Modified by Cognizant on 08/05/2005 to display the Status Column in Transaction Search
			e.Item.Cells[15].Width = 73;// Status Column
			//STAR Retrofit III.Ch1: - START Modified to display two new columns Approved By and Approved date in the Transaction Search result grid.
			e.Item.Cells[16].Width = 80;//Approved By 
			e.Item.Cells[17].Width = 73;//Approved Date 
			e.Item.Cells[18].Width = 20;//Rep DO
			//STAR Retrofit III.Ch1: - END

		}

	}
}
