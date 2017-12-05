/*
 * History:
 * 11/11/2004 JOM Replaced InsuranceClasses.Service.Insurance with TurninClasses.Service.Turnin.
 *			  Also removed multiple creations of this service.
 * 
 *			  --> Modified by COGNIZANT as part of Q4-retrofit <--
 * 
 * 11/25/2005 Q4-Retrofit.Ch1  
 * 			  Existing Webmethod TurninService.SalesRepCashierWorkflow is renamed as TurninService.PayWorkflowReport	
 *			  for naming Convention. 
 * 
 * 11/25/2005 Q4-Retrofit.Ch2 
 *			  A new column 'Product' is added to Report Grid of status 'void' so,
 *			  the alignment changes are made for the Amount column in Report Grid hence modifying
 *			  the indices of cells of DataGridItem.(Changing cell indices in Amount Alignment).
 * 
 * 11/25/2005 Q4-Retrofit.Ch3 
 *			  A new column 'Product' is added to Report Grid of status 'void' so,
 *			  the alignment changes are made for the other column in Report Grid hence modifying
 *			  the indices of cells of DataGridItem.(Changing cell indices in Status Report Section).
 * STAR Retrofit II Changes: 
 * Modified as a part of CSR 4852
 * 1/29/2007 STAR Retrofit II.Ch1: 
 *           Added a new cache for Manual Void Status like Void Status in FillStatus().
 * 1/29/2007 STAR Retrofit II.Ch2: 
 *           The Amount alignment region is modifed to align Amount column for the Manual Void Report.
 * 1/29/2007 STAR Retrofit II.Ch3: 
 *           The Status Report Section region is modified to set widths for the new columns 
 *			 in Manual Void Report.
 * 1/29/2007 STAR Retrofit II.Ch4: 			  
 *           The content of the cache "AUTH_DOS" is cleared, so as to populate the cache with updated 
 *			 data,when the page is getting loaded for the first time.
 *			 Modified by cognizant as a partt of .NetMigration 3.5 on 04-14-2010.
 * .NetMig 3.5:Modified the   pageindexchanged1 and Update data view methods to display the results properly.
 * .NetMig 3.5.Ch1:Modified by cognizant for displaying the total amount value in bold on on 08-12-2010. 
 * MODIFIED BY COGNIZANT AS A PART OF TIME ZONE CHANGE ENHANCEMENT ON 8-31-2010
 * TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant.
 * TimeZoneChange.Ch2-Added Text MST Arizona at the end of the current date and time  by cognizant.
 * TimeZoneChange.Ch3-Changed the Modified  Date to Modified Date/Time(MST Arizona) by cognizant.
 * TimeZoneChange.Ch4-Changed the Void/Reissue Date to Void/Reissue Date/Time(MST Arizona)
 * 67811A0 - START -PCI Remediation for Payment systems Start: Added to fix the Object reference exception by cognizant on 1/11/2012
 * // SSO Integration - CH1 -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
 * PC Phase II changes CH1 - Added the below code to handle Date Time Picker
 * CHG0109406 - CH1 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH2 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH3 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH4 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH5 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH6 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH7 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH8 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0112662 - Removed the AddDays to end date
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
using CSAAWeb;
using MSCR.Reports;
using OrderClasses.Service;

namespace MSCR
{
	/// <summary>
	/// Summary description for TurnIn_General_Reports.
	/// </summary>
	public partial class TurnIn_General_Report : CSAAWeb.WebControls.PageTemplate
	{
		protected System.Data.DataTable dtbReportData;
		
		
		string CurrentUser;
		//Report Type Codes are made as Constants
		const string  Status_Report = "5";
		const string  Aged_Transactions_Report = "6";
		//protected MSCR.Controls.Dates Dates;
		TurninClasses.Service.Turnin TurninService;
		string tmpReceipt;
		string colour = ALTERNATE_ROW_CLASS;
		const string ROW_CLASS = "item_template_row";
		const string ALTERNATE_ROW_CLASS = "item_template_alt_row";
		

		
	
		InsRptLibrary rptLib = new InsRptLibrary();
		/// <summary>
		/// For Get the RepDo's in the Cache
		/// </summary>
		protected DataTable DOs 
		{
			get 
			{
				return (DataTable)Cache["AUTH_DOS"];
			} 
			set 
			{
				Cache["AUTH_DOS"]=value;
			}
		}
		
		protected void Page_Load(object sender, System.EventArgs e)
		{
            // SSO Integration - CH1 Start -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            // SSO Integration - CH1 End -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
			CurrentUser = Page.User.Identity.Name;
			TurninService=new TurninClasses.Service.Turnin();
			lblErrMsg.Text="";
			
			if (!Page.IsPostBack)
             {	
				//STAR Retrofit II.Ch4 - START :The content of the cache "AUTH_DOS" is cleared, so as to populate the cache with updated 
				//data,when the page is getting loaded for the first time.
				Cache.Remove("AUTH_DOS");
				//STAR Retrofit II.Ch4 - END

				//Make all the controls invisible
				InvisibleUserInfo();
				
				//Filling Report Types
				FillReport();
			
				//Filling the Status Combo
				FillStatus();
				//Filling the Cache for RepDo 
				rptLib.FillDOListBox(_RepDO,DOs);
				
				rptLib.FillUserListBox(UserList,CurrentUser, "All","-1", "-1", true,true);

                //PC Phase II changes CH1 - Start - Modified the below code to Handle Date Time Picker
                DateTime now = DateTime.Now;
                DateTime startdate = new DateTime(now.Year, now.Month, 1);
                startDt.Text = startdate.ToString("MM-dd-yyyy HH:mm:ss");
                DateTime enddate = DateTime.Today;
                endDt.Text = enddate.ToString("MM-dd-yyyy") + " 23:59:59";
                //PC Phase II changes CH1 - End - Modified the below code to Handle Date Time Picker
			}
			LoadDateControl();
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
	

		protected void cboReportType_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		//Filling the Status Combo for the Corresponding Report Type
			FillStatus();
		//Make all the controls invisible since the Report type changed
		InvisibleUserInfo();
		
		}
		//To Fill the userList for Selected RepDO

		protected void _RepDO_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			rptLib.FillUserListBox(UserList,CurrentUser, "All","-1",_RepDO.SelectedItem.Value, true,true);
		}

		protected void btnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			 
			LoadDetails();
		}


		// EVENT HANDLER: ItemDataBound
		public void ItemDataBound1(Object sender, GridViewRowEventArgs e)
		{
			
			DataRowView drvReportData = (DataRowView) e.Row.DataItem;
			if (drvReportData == null)
			return;

			#region Amount Alignment	
			
			//If the selected report is Aged transactions Report
			if(cboReportType.SelectedItem.Value ==Cache["Aged Transactions Report"].ToString())
			{
				e.Row.Cells[5].Attributes.Add("align","right");
                e.Row.Cells[6].Attributes.Add("align", "right");
                e.Row.Cells[7].Attributes.Add("align", "right");
			}
				//If the selected report is  Status Report
			else if(cboReportType.SelectedItem.Value==Cache["Status Report"].ToString())
			{
				//For Void status only
				if(cboStatus.SelectedItem.Value == Cache["Void"].ToString())   
				{
					
					/*
					 * 25/11/2005 Q4-Retrofit.Ch2 - START Added by COGNIZANT 
					 *			  A new column 'Product' is added to Report Grid of status 'void' so,
					 *			  the alignment changes are made for the Amount column in Report Grid hence modifying
					 *			  the indices of cells of DataGridItem.(Changing cell indices in Amount Alignment).
					 */
                    e.Row.Cells[4].Attributes.Add("align", "right");	// Changed the Cell Index from 3 to 4
                    e.Row.Cells[7].Attributes.Add("align", "right");	// Changed the Cell Index from 6 to 7
					/*
					 * 25/11/2005 Q4-Retrofit.Ch2 - END
					 */
				}
				else
				{
					//STAR Retrofit II.Ch2 : START Modified to align Amount column for the Manual Void Report
					if(cboStatus.SelectedItem.Value == Cache["Manual Void"].ToString())   
					{
                        e.Row.Cells[9].Attributes.Add("align", "right");
					}
					else
					{
                        e.Row.Cells[10].Attributes.Add("align", "right");
					}
					//STAR Retrofit II.Ch2 : END
				}
				
			}
			#endregion
			#region Aged Transactions Report Section
			//If the selected report is Aged transactions Report
			if(cboReportType.SelectedItem.Value==Cache["Aged Transactions Report"].ToString())
			{
				dgReport.Width = 604;    
				//Date and Time Cell
                //CHG0109406 - CH3 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                //Start-TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant.
				if(!Convert.IsDBNull(drvReportData["Receipt Date/Time(Arizona)"]))

                    e.Row.Cells[0].Text = Convert.ToDateTime(drvReportData["Receipt Date/Time(Arizona)"]).ToString("MM/dd/yyyy   hh:mm:ss tt");
                //End-TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant.
                //CHG0109406 - CH3 - END - Modified the timezone from "MST Arizona" to Arizona
                e.Row.Cells[0].Width = 80;

				//Asssign the width of  ReceiptNumber Cell
                e.Row.Cells[1].Width = 80;
				//Asssign the width of Policy/Mbr Number Cell
                e.Row.Cells[2].Width = 75;
				//Asssign the width of Users Cell
                e.Row.Cells[3].Width = 100;
				//Asssign the width of Rep Do cell 
                e.Row.Cells[4].Width = 30;
  				//<5 Days Amount 
				if(drvReportData["<5 Days"].ToString()!="0")
                    e.Row.Cells[5].Text = String.Format("{0:c}", drvReportData["<5 Days"]).ToString();
				   
				else
                    e.Row.Cells[5].Text = "";
                e.Row.Cells[5].Width = 73; 
				
				//5 -  10 Days Amount
				if(drvReportData["5 - 10 Days"].ToString()!="0")
                    e.Row.Cells[6].Text = String.Format("{0:c}", drvReportData["5 - 10 Days"]).ToString();	   
				else
                    e.Row.Cells[6].Text = "";
                e.Row.Cells[6].Width = 73;
				
				//>10 Days Amount
				if(drvReportData[">10 Days"].ToString()!="0")
                    e.Row.Cells[7].Text = String.Format("{0:c}", drvReportData[">10 Days"]).ToString();	   
				
				else
                    e.Row.Cells[7].Text = "";
                e.Row.Cells[7].Width = 73;				
				
				//Change the  color Sequence according to the Receipt Numbers
				AlternatingColor(e); 
				if(e.Row.DataItemIndex==dtbReportData.Rows.Count-1)   
				{

                    e.Row.Cells.RemoveAt(4);
                    e.Row.Cells[3].Attributes.Add("colspan", "2");
                    e.Row.Cells[3].Attributes.Add("align", "right");
					
				}
			}
			#endregion

			#region Status Report Section
			//If the selected report is  Status Report
			if(cboReportType.SelectedItem.Value == Cache["Status Report"].ToString())
			{
				
				//For Void status only
				if(cboStatus.SelectedItem.Value == Cache["Void"].ToString())   
				{
					dgReport.Width=604; 
					//Date and Time Cell
                    //CHG0109406 - CH4 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                    //Start-TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant.
					if(!Convert.IsDBNull(drvReportData["Receipt Date/Time(Arizona)"]))
                        e.Row.Cells[0].Text = Convert.ToDateTime(drvReportData["Receipt Date/Time(Arizona)"]).ToString("MM/dd/yyyy   hh:mm:ss tt");
                    //End-TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant.
                    //CHG0109406 - CH4 - END - Modified the timezone from "MST Arizona" to Arizona
                    e.Row.Cells[0].Width = 80;
										
					/*
					 * 25/11/2005 Q4-Retrofit.Ch3 - START - Added by COGNIZANT 
					 *			  A new column 'Product' is added to Report Grid of status 'void' so,
                     *			  the alignment changes are made for the other column in Report Grid hence modifying
				     *			  the indices of cells of DataGridItem.(Changing cell indices in Status Report Section).
					 */
					//Asssign the width of  Customer Name Cell
                    e.Row.Cells[2].Width = 100;		// Changed the Cell Index from 1 to 2
					//Asssign the width of  ReceiptNumber Cell
                    e.Row.Cells[3].Width = 80;			// Changed the Cell Index from 3 to 4
					//Asssign the width of Amount Cell
					if(!Convert.IsDBNull(drvReportData["Original Amount"]))
                        e.Row.Cells[4].Text = String.Format("{0:c}", drvReportData["Original Amount"]).ToString();		// Changed the Cell Index from 4 to 5
                    e.Row.Cells[4].Width = 73;			// Changed the Cell Index from 3 to 4
					//Void/Reisse Date and Time Cell
                    //CHG0109406 - CH5 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                    //Start-TimeZoneChange.Ch4-Changed the Void/Reissue Date to Void/Reissue Date/Time(MST Arizona)
					if(!Convert.IsDBNull(drvReportData["Void/Reissue Date/Time(Arizona)"]))
                        e.Row.Cells[5].Text = Convert.ToDateTime(drvReportData["Void/Reissue Date/Time(Arizona)"]).ToString("MM/dd/yyyy   hh:mm:ss tt");		// Changed the Cell Index from 4 to 5
                    //End-TimeZoneChange.Ch4-Changed the Void/Reissue Date to Void/Reissue Date/Time(MST Arizona)
                    //CHG0109406 - CH5 - END - Modified the timezone from "MST Arizona" to Arizona
                    e.Row.Cells[5].Width = 80;		// Changed the Cell Index from 4 to 5
					//Asssign the width of  New ReceiptNumber Cell
                    e.Row.Cells[6].Width = 80;			// Changed the Cell Index from 5 to 6
					//Asssign the width of New Amount Cell
					if(!Convert.IsDBNull(drvReportData["New Amount"]))
                        e.Row.Cells[7].Text = String.Format("{0:c}", drvReportData["New Amount"]).ToString();		// Changed the Cell Index from 6 to 7
                    e.Row.Cells[7].Width = 73;			// Changed the Cell Index from 6 to 7
					//Asssign the width of Rep Do cell 
                    e.Row.Cells[9].Width = 30;				// Changed the Cell Index from 8 to 9
					AlternatingColor(e);
					if(e.Row.DataItemIndex==dtbReportData.Rows.Count-1)   
					{
                        e.Row.Cells.RemoveAt(5);		// Changed the Cell Index from 4 to 5
                        e.Row.Cells[4].Text = "";			// Changed the Cell Index from 3 to 4
                        e.Row.Cells[5].Attributes.Add("colspan", "2");		// Changed the Cell Index from 4 to 5
                        e.Row.Cells[5].Width = 160;			// Changed the Cell Index from 4 to 5
					}
					
					/*
					 * 25/11/2005 Q4-Retrofit.Ch3 - END
					 */
					

  
				}
				//For Other status
				else
				{	
					dgReport.Width=720; 
					//Date and Time Cell
                    //CHG0109406 - CH2 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                    //Start-TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant.
					if(!Convert.IsDBNull(drvReportData["Receipt Date/Time(Arizona)"]))
                        e.Row.Cells[0].Text = Convert.ToDateTime(drvReportData["Receipt Date/Time(Arizona)"]).ToString("MM/dd/yyyy   hh:mm:ss tt");
                    //End-TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant.
                    //CHG0109406 - CH2 - END - Modified the timezone from "MST Arizona" to Arizona
                    e.Row.Cells[0].Width = 80;
					//Asssign the width of  Customer Name Cell
                    e.Row.Cells[1].Width = 100;
					//Asssign the width of  ReceiptNumber Cell
                    e.Row.Cells[2].Width = 80;
					//Asssign the width of Policy/Mbr Number Cell
                    e.Row.Cells[5].Width = 75;
					//Asssign the width of Users Cell
                    e.Row.Cells[6].Width = 100;
					//Asssign the width of Rep Do cell 
                    e.Row.Cells[7].Width = 30;
					//Asssign the width of Cashier ID Cell
                    e.Row.Cells[8].Width = 100;

					//STAR Retrofit II.Ch3 : START Modified to assign widths for the new columns of Manual Void Report
					//Assign width for Manual Void Reports
					if(cboStatus.SelectedItem.Value == Cache["Manual Void"].ToString())   
					{
						if(!Convert.IsDBNull(drvReportData["Amount"]))
                            e.Row.Cells[9].Text = String.Format("{0:c}", drvReportData["Amount"]).ToString();	   
						//Assign the width of Modified By
                        e.Row.Cells[10].Width = 100;
                        //CHG0109406 - CH6 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                        //Start-TimeZoneChange.Ch3-Changed the Modified  Date to Modified Date/Time(MST Arizona) by cognizant.
						if(!Convert.IsDBNull(drvReportData["Modified Date/Time(Arizona)"]))
                            e.Row.Cells[11].Text = Convert.ToDateTime(drvReportData["Modified Date/Time(Arizona)"]).ToString("MM/dd/yyyy   hh:mm:ss tt");
                        //End-TimeZoneChange.Ch3-Changed the Modified  Date to Modified Date/Time(MST Arizona) by cognizant.
                        //CHG0109406 - CH6 - END - Modified the timezone from "MST Arizona" to Arizona
						//Assign the width of Modified Date/Time 
                        e.Row.Cells[11].Width = 80;
						//Clear the value for last row
                        if (e.Row.DataItemIndex == dtbReportData.Rows.Count - 1)   
						{
                            e.Row.Cells[11].Text = ""; 
						}
						
					}
					else
					{
						//Asssign the width of Amount Cell
						if(!Convert.IsDBNull(drvReportData["Amount"]))
                            e.Row.Cells[10].Text = String.Format("{0:c}", drvReportData["Amount"]).ToString();	   
					}
					//STAR Retrofit II.Ch3 : END
					
					//Change the  color Sequence according to the Receipt Numbers
					AlternatingColor(e); 

				}
			}
			#endregion

            if (e.Row.DataItemIndex == dtbReportData.Rows.Count - 1)   
				{
                    e.Row.Attributes.Add("class", "total_row");
                    e.Row.Attributes.Add("align", "right");   
					//For last Row remove the Receipt Date and time
                    e.Row.Cells[0].Text = "";
                    for (int cellCount = 0; cellCount < e.Row.Cells.Count; cellCount++)
					{
                        //.Net Mig 3.5.ch1-Start:Added by cognizant as a part of .Net Mig 3.5 for displaying the total amount value in bold on on 08-12-2010.
                        e.Row.Cells[cellCount].Attributes.Add("class", "total_cell_arial_12_bold");    
					}
								   					
				}
			
		
	
		}
        public void PageIndexChanged1(Object sender, GridViewPageEventArgs e)
		{
            //commented by cognizant as a part of .NetMig 3.5 on 04-14-2010
            if (e.NewPageIndex == -1)
            {
                dgReport.PageIndex = 0;
                //commented  by cognizant as a part of .NetMig 3.5 on 04-14-2010
                //dgReport1.PageIndex = e.NewPageIndex;
                //dgReport1.CurrentPageIndex = e.NewPageIndex;
            }
            else
            {
                dgReport.PageIndex = e.NewPageIndex;
            }
			//dgReport.PageIndex = e.NewPageIndex;
			UpdateDataView();
		}
		//Displaying the details of the same receipt number in a single colour
		//Making the Receipt Date,Status, Member Name, checkbox of same receipt number invisible
		private void AlternatingColor(GridViewRowEventArgs e)
		{
           // if ((e.Row.DataItem == ListItemType.Item) || (e.Row.DataItem == ListItemType.AlternatingItem)) 
			//{ 
            string strReceiptNumber;
				if(cboStatus.SelectedItem.Value == Cache["Void"].ToString())   
				{
                    strReceiptNumber = e.Row.Cells[dtbReportData.Columns.IndexOf("Original Receipt Number")].Text;
				}
				else
				{
                    strReceiptNumber = e.Row.Cells[dtbReportData.Columns.IndexOf("Receipt Number")].Text;
				}
				if(strReceiptNumber !=tmpReceipt)
				{
					AssignClass(colour,e);
				}
				else
				{	
					if(cboStatus.SelectedItem.Value == Cache["Void"].ToString())   
					{
						// Clear the Receipt number, Original Amount and Receipt Date/Time fields for Void Status
                        e.Row.Cells[dtbReportData.Columns.IndexOf("Original Receipt Number")].Text = "";
                        e.Row.Cells[dtbReportData.Columns.IndexOf("Original Amount")].Text = "";
                        //CHG0109406 - CH7 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                        //TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant.
                        e.Row.Cells[dtbReportData.Columns.IndexOf("Receipt Date/Time(Arizona)")].Text = "";
                        //CHG0109406 - CH7 - END - Modified the timezone from "MST Arizona" to Arizona
					}
					else
					{
						// Clear the Receipt number, Original Amount and Receipt Date/Time fields for Void Status
                        e.Row.Cells[dtbReportData.Columns.IndexOf("Receipt Number")].Text = "";
                        //CHG0109406 - CH8 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                        //TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant.
                        e.Row.Cells[dtbReportData.Columns.IndexOf("Receipt Date/Time(Arizona)")].Text = "";
                        //CHG0109406 - CH8 - END - Modified the timezone from "MST Arizona" to Arizona
					}

					e.Row.Attributes.Add("class",colour);
				}
				tmpReceipt=strReceiptNumber; 
			}
		//}

	

		//Swapping of the colour between ALTERNATE_ROW_CLASS and ROW_CLASS
		private void AssignClass(string tmpcolour,GridViewRowEventArgs e)
		{
			if ( tmpcolour == ROW_CLASS )
			{
                e.Row.Attributes.Add("class", ALTERNATE_ROW_CLASS);
				colour = ALTERNATE_ROW_CLASS;
			}
			else if ( tmpcolour == ALTERNATE_ROW_CLASS )
			{
                e.Row.Attributes.Add("class", ROW_CLASS);
				colour = ROW_CLASS;
			}
		}

		private void LoadDetails()
		{
            try
            {
                OrderClasses.ReportCriteria rptCrit1 = new OrderClasses.ReportCriteria();
                //Preparing the criteria for retrieving data
                rptCrit1.Status_Link_ID = cboStatus.SelectedItem.Value;
                rptCrit1.ReportType = Convert.ToInt16(cboReportType.SelectedItem.Value);
                rptCrit1.RepDO = _RepDO.SelectedItem.Value;
                rptCrit1.Users = GetUsersList();

                //Check the Length of the Selected Users to not more than 2000
                if (rptCrit1.Users.Length > 2000)
                {
                    rptLib.SetMessage(lblErrMsg, "The total length of users should not exceed 2000 characters", true);
                    InvisibleUserInfo();
                    return;
                }
                // PC Phase II changes CH1 - Added the below code to handle Date Time Picker - Start
           
                //If the selected report is Status Report
                if (startDt.Visible == true && endDt.Visible == true)
                {
                    string start_date = startDt.Text;
                    DateTime dt_start_date = DateTime.Parse(start_date);
                    string end_date = endDt.Text;

                    //CHG0112662 - BEGIN - Removed the AddDays to end date
                    //DateTime dt_end_date = DateTime.Parse(end_date).AddDays(1);
                    DateTime dt_end_date = DateTime.Parse(end_date);
                    //CHG0112662 - END - Removed the AddDays to end date

                    //rptCrit1.CurrentUser = Page.User.Identity.Name;
                    rptCrit1.StartDate = dt_start_date;
                    rptCrit1.EndDate = dt_end_date;

                    //Assign the values to the labels
                    AssignLabelValues();
                    bool bValid = rptLib.ValidateDate(lblErrMsg, rptCrit1.StartDate, rptCrit1.EndDate, rptCrit1.ReportType);
                    if (bValid == true)
                    {
                        InvisibleUserInfo();
                        return;
                    }

                }
                //PC Phase II changes CH1 - Added the below code to handle Date Time Picker - End
                //Default Value for date
                else
                {
                    //Assign the values to the labels
                    AssignLabelValues();
                    DateTime StartDate = new DateTime(1900, 1, 1);
                    DateTime EndDate = new DateTime(1900, 1, 1);
                    rptCrit1.StartDate = StartDate;
                    rptCrit1.EndDate = EndDate;
                }
                //Obtaining the dataset for the chosen criteria


                /*
                 * 11/25/2005 Q4-Retrofit.Ch1 - START - Added by COGNIZANT 
                 *			  Existing Webmethod TurninService.SalesRepCashierWorkflow is renamed as TurninService.PayWorkflowReport	
                 *			  for naming Convention 
                 */

                DataSet dtsReportData = TurninService.PayWorkflowReport(rptCrit1);


                /*
                 * 11/25/2005 Q4-Retrofit.Ch1 - END
                 */

                if (dtsReportData.Tables[0].Rows.Count == 1)
                {

                    rptLib.SetMessage(lblErrMsg, "No Data Found. Please specify a valid criteria.", true);
                    InvisibleUserInfo();
                    return;
                }
                else
                {
                    VisibleUserInfo();
                    dtbReportData = dtsReportData.Tables[0];
                    Session["ReportData"] = dtbReportData;
                    dgReport.PageIndex = 0;
                    UpdateDataView();
                }
            }
            catch (FormatException)
            {
                string invalidDate = @"<Script>alert('Select a valid date.')</Script>";
                Response.Write(invalidDate);
                return;
            }
		}
		//To Get the selected User List in Comma Separated values
		private string GetUsersList()
		{
			string lstUsers = "";
			string lstNames = "";
		
			lblUsers.Text =  "";
			//int i = 0;
			if (UserList.SelectedIndex > -1)
			{
					
				foreach(ListItem li in UserList.Items)
				{
					if(li.Selected == true)
					{
						lstNames = lstNames + li.Text.Replace(" - " + li.Value,"; ");
						lstUsers = lstUsers + li.Value + ',';
					}
				}	
				//lstNames = lstNames.Replace(" -",";");
				if (lstUsers == "All,")
				{
					lstNames = "All";
				}
				
				lblUsers.Text =  lstNames;
				
			}
			else
			{
				lstUsers = "All,";
			}
			Session["Users"] = lstNames;  
			return lstUsers;
		}
		// Making the controls invisible
		private void InvisibleUserInfo()
		{
			
			tblReportInfo.Visible=false;
			trConvertPrint.Visible=false;
			tbReport.Visible=false;
			lblPageNum1.Visible=false;
			lblPageNum2.Visible=false;  
			dgReport.Visible=false; 
		
		}
		// Making the controls invisible
		private void VisibleUserInfo()
		{
			
			tblReportInfo.Visible=true;
			trConvertPrint.Visible=true;
			tbReport.Visible=true;
			lblPageNum1.Visible=true;
			lblPageNum2.Visible=true;
			dgReport.Visible=true;
		
		}

		//Assign the values to the labels
		private void AssignLabelValues()
		{
            //CHG0109406 - CH1 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
            //TimeZoneChange.Ch2-Added Text MST Arizona at the end of the current date and time  by cognizant.
            lblRunDate.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + " " + "Arizona";
			//PC Phase II changes CH1 - Start
            if((startDt.Visible==true) && (endDt.Visible==true))
			{
				trRundate.Visible = true;
                //TimeZoneChange.Ch2-Added Text MST Arizona at the end of the current date and time  by cognizant.
                lblDateRange.Text = startDt.Text + " - " + endDt.Text + " " + "Arizona";
                //CHG0109406 - CH1 - END - Modified the timezone from "MST Arizona" to Arizona
			}
          //PC Phase II changes CH1 - End
			else
			{
				trRundate.Visible = false;  
			}
			lblReportStatus.Text=cboStatus.SelectedItem.Text;
			lblRepDo.Text = _RepDO.SelectedItem.Text;
			lblCaption.Text= cboReportType.SelectedItem.Text+" - "+cboStatus.SelectedItem.Text;
			AssignSessionValues(); 

		}
		//Assign the Required values to session
		private void AssignSessionValues()
		{
			if((startDt.Visible==true) && (endDt.Visible == true))
			{
				Session["DateRange"] = startDt.Text+" - "+ endDt.Text;
			}
			Session["ReportStatus"] = cboStatus.SelectedItem.Text;
			Session["ReportType"] = cboReportType.SelectedItem.Text;
			Session["Status"] =  cboStatus.SelectedItem.Text;
			Session["RepDo"] =  _RepDO.SelectedItem.Text;    
		}

		// Filling the Report Types
		private void FillReport()
		{
			DataSet dtsReportTypes = TurninService.GetTurnInReportTypes(CurrentUser,"R");
			cboReportType.DataSource=dtsReportTypes.Tables[0];
			cboReportType.DataBind();

			//Put the Report types in Cache

            if (Cache["Status Report"] == null)
            {
                Cache["Status Report"] = Status_Report;
                Cache["Status Report Desc"] = cboReportType.Items.FindByValue(Status_Report).Text;
            }
            if (Cache["Aged Transactions Report"] == null)
            {
                Cache["Aged Transactions Report"] = Aged_Transactions_Report;
                Cache["Aged Transactions Report Desc"] = cboReportType.Items.FindByValue(Aged_Transactions_Report).Text;
            }
			
			
		}
		// Filling the Status Combo
		private void FillStatus()
		{
			DataSet dtsStatus= TurninService.GetStatus(CurrentUser,cboReportType.SelectedItem.Value.ToString());
			cboStatus.DataSource=dtsStatus.Tables[0];
			cboStatus.DataBind();
			//Fill the cache with the void status
			if (Cache["Void"] == null)
			{
				Cache["Void"] = cboStatus.Items.FindByText("Void").Value;     
				Cache["Void Desc"] = cboStatus.Items.FindByText("Void").Text;     
			}
			//STAR Retrofit II.Ch1 : START Added new Cache for Manual Void
			//Fill the cache for Maual Void Status
			if (Cache["Manual Void"] == null)
			{
				Cache["Manual Void"] = cboStatus.Items.FindByText("Manual Void").Value;
				Cache["Manual Void Desc"] = cboStatus.Items.FindByText("Manual Void").Text; 
				  
			}
			//STAR Retrofit II.Ch1 : END
			
		}
		
		//Updates the DataGrid with the New Data 
		private void UpdateDataView()
		{
						
			int pageCnt=1;
			int currPage=1;
			dtbReportData = (DataTable)Session["ReportData"]; 
			if (dtbReportData == null)
			{
				rptLib.SetMessage(lblErrMsg,"Your session has timed out. Please re-submit the page", true);
				return;
			}
			//For Status Report and For Void status remove the Trans type column in the Table
			if(cboStatus.SelectedItem.Value == Cache["Void"].ToString()&&cboReportType.SelectedItem.Value==Cache["Status Report"].ToString())
			{
				if(dtbReportData.Columns.IndexOf("Trans Type")!=-1) 
				dtbReportData.Columns.RemoveAt(dtbReportData.Columns.IndexOf("Trans Type"));   
				//Hide the New Amounts for the Associates	
				for(int dtRow=0;dtRow<dtbReportData.Rows.Count-1;dtRow++)
				{
					if(dtbReportData.Rows[dtRow]["New Receipt Number"].ToString()==dtbReportData.Rows[dtRow+1]["New Receipt Number"].ToString())   
					{
						dtbReportData.Rows[dtRow+1]["New Amount"] = DBNull.Value;
					}
				}
			}
			if(dtbReportData.Rows.Count>0)
			{	
				dgReport.DataSource=dtbReportData;
                dgReport.DataBind(); 
				pageCnt = dgReport.PageCount;
				currPage = dgReport.PageIndex +1; 

			}
			if(pageCnt > 1)
			{
                //commented  by cognizant as a part of .NetMig 3.5 on 04-14-2010
				//dgReport.PagerStyle.Visible = true;  
			}
			if(pageCnt < 2) 
			{
                //commented  by cognizant as a part of .NetMig 3.5 on 04-14-2010
				//dgReport.PagerStyle.Visible = false; 
			}
			lblPageNum1.Text = "Showing Page " + currPage.ToString() + " of " + pageCnt.ToString();
			lblPageNum2.Text = "Showing Page " + currPage.ToString() + " of " + pageCnt.ToString();
            //Added by cognizant as a part of Mig 3.5 to disable the Prev link button on 03-13-2010.
            if (currPage == 1)
            {
                System.Web.UI.WebControls.LinkButton PageIndicatorLinkPrevBottom = (System.Web.UI.WebControls.LinkButton)dgReport.BottomPagerRow.FindControl("vPrev");
                System.Web.UI.WebControls.LinkButton PageIndicatorLinkPrevTop = (System.Web.UI.WebControls.LinkButton)dgReport.TopPagerRow.FindControl("vPrev");
                if (PageIndicatorLinkPrevBottom != null && PageIndicatorLinkPrevTop != null)
                {
                    PageIndicatorLinkPrevBottom.Enabled = false;
                    PageIndicatorLinkPrevTop.Enabled = false;
                }

            }
            if (currPage == pageCnt)
            {
                System.Web.UI.WebControls.LinkButton PageIndicatorLinkNextBottom = (System.Web.UI.WebControls.LinkButton)dgReport.BottomPagerRow.FindControl("vNext");
                System.Web.UI.WebControls.LinkButton PageIndicatorLinkNextTop = (System.Web.UI.WebControls.LinkButton)dgReport.TopPagerRow.FindControl("vNext");
                if (PageIndicatorLinkNextBottom != null && PageIndicatorLinkNextTop != null)
                {
                    PageIndicatorLinkNextBottom.Enabled = false;
                    PageIndicatorLinkNextTop.Enabled = false;
                }
            }
           
			

		}
		
		private void LoadDateControl()
		{
            ////67811A0 - START -PCI Remediation for Payment systems Start: Added to fix the Object reference exception by cognizant on 1/11/2012
            if (Cache["Status Report"] == null)
            {
                Cache["Status Report"] = Status_Report;
                Cache["Status Report Desc"] = cboReportType.Items.FindByValue(Status_Report).Text;
            }
            if (Cache["Aged Transactions Report"] == null)
            {
                Cache["Aged Transactions Report"] = Aged_Transactions_Report;
                Cache["Aged Transactions Report Desc"] = cboReportType.Items.FindByValue(Aged_Transactions_Report).Text;
            }
            //67811A0 - START -PCI Remediation for Payment systems End: Added to fix the Object reference exception by cognizant on 1/11/2012
            //If the selected report is Aged transactions Report
            //PC Phase II changes CH1 - Start
			if(cboReportType.SelectedItem.Value==Cache["Aged Transactions Report"].ToString())
			{
				Date.Visible = false;
                image.Visible = false;
                image1.Visible = false;
				startDt.Visible=false;
                endDt.Visible = false;
			}
			//If the selected report is Status Report
			else
			{
                Date.Visible = true;
                image.Visible = true;
                image1.Visible = true;
                startDt.Visible = true;
                endDt.Visible=true; 
			}
		}
        //PC Phase II changes CH1 - End
		protected void cboStatus_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//Make all the controls invisible since the Status type changed
			InvisibleUserInfo();
		
		}

	}
}
