/*
 * History:
 *         --> Modified by COGNIZANT as part of Q4-retrofit <--
 * 
 * 11/28/2005 Q4-Retrofit.Ch1
 *			  A new column 'Product' is added to Report Grid of status 'void' so,
 *			  the alignment changes are made for the Amount column in Report Grid hence modifying
 *			  the indices of cells of DataGridItem.(Changing cell indices in Amount Alignment).
 * 
 * 11/28/2005 Q4-Retrofit.Ch2 
 *			  A new column 'Product' is added to Report Grid of status 'void' so,
 *			  the alignment changes are made for the other column in Report Grid hence modifying
 *			  the indices of cells of DataGridItem.(Changing cell indices in Status Report Section). 
 * 
 * 11/29/2005 Q4-Retrofit.Ch3
 *			  This code fixes the alignment issue for Membership & Policy Numbers in the XLS view.
 *			  A single quote is prefixed with the Policy & Membership Number to left align them in the XLS view.
 * 
 * STAR Retrofit II Changes: 
 * Modified as a part of CSR 4852
 * 1/29/2007 STAR Retrofit II.Ch1: 
 *           The Amount alignment region is modifed to align Amount column for the Manual Void Report.
 * 1/29/2007 STAR Retrofit II.Ch2: 
 *           The Status Report Section region is modified to set widths for the new columns.
 *			 in Manual Void Report.
 * MODIFIED BY COGNIZANT AS A PART OF TIME ZONE CHANGE ENHANCEMENT ON 8-31-2010
 * TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant.
 * TimeZoneChange.Ch2-Added Text MST Arizona at the end of the current date and time  by cognizant.
 * TimeZoneChange.Ch3-Changed the Modified  Date to Modified Date/Time(MST Arizona) by cognizant.
 * TimeZoneChange.Ch4-Changed the Void/Reissue Date to Void/Reissue Date/Time(MST Arizona)
 * CHG0109406 - CH1 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH2 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH3 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH4 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH5 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH6 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH7 - Modified the timezone from "MST Arizona" to Arizona
 * CHG0109406 - CH8 - Displayed the header message if the session has data
 * CHG0109406 - CH9 - Hidden the header message if the session has timed out
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
	/// Summary description for TurnIn_General_PrintXls.
	/// </summary>
	public partial class TurnIn_General_PrintXls : System.Web.UI.Page
	{
		
		
		
		
		protected DataTable dtbReportData;
		string tmpReceipt;
		string colour = ALTERNATE_ROW_CLASS;
		const string ROW_CLASS = "White";
		const string ALTERNATE_ROW_CLASS = "LightYellow";
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			// Put user code to initialize the page here
			

			if (Request.QueryString["type"] == "XLS") 
			{
				Response.ContentType = "application/vnd.ms-excel";
				Response.Charset = ""; //Remove the charset from the Content-Type header.
			}
			Page.EnableViewState = false;
		 
			dtbReportData= (DataTable) Session["ReportData"];
			if(dtbReportData != null)
			{
                //CHG0109406 - CH8 - BEGIN - Displayed the header message if the session has data
                trCaption.Visible = true;
                //CHG0109406 - CH8 - END - Displayed the header message if the session has data
				if(Convert.ToString(Session["ReportType"])==Cache["Status Report Desc"].ToString()&&(Convert.ToString(Session["Status"])==Cache["Void Desc"].ToString()))
				{
					//Hide the New Amounts for the Associates	
					for(int dtRow=0;dtRow<dtbReportData.Rows.Count-1;dtRow++)
					{
						if(dtbReportData.Rows[dtRow]["New Receipt Number"].ToString()==dtbReportData.Rows[dtRow+1]["New Receipt Number"].ToString())   
						{
							dtbReportData.Rows[dtRow+1]["New Amount"] = DBNull.Value;
						}
					}
				}

				DisplayInfo();
				dgReport.DataSource=dtbReportData;
				dgReport.DataBind();
				//dtbReportData.Rows[dtbReportData.Rows.Count-1].Delete();    
			}
			else
			{
				Response.Write("Your session has timed out. Please re-submit the page");
                //CHG0109406 - CH9 - BEGIN - Hidden the header message if the session has timed out
                trCaption.Visible = false;
                //CHG0109406 - CH9 - END - Hidden the header message if the session has timed out
				return;
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

		//Assign the session values to the labels
		private void DisplayInfo()
		{
			lblCaption.Text = Session["ReportType"].ToString()+" - "+Session["Status"].ToString();
			
			lblRepDO.Text = Session["RepDo"].ToString();
			//If the selected report is Aged transactions Report
			if(Convert.ToString(Session["ReportType"])==Cache["Aged Transactions Report Desc"].ToString())
			{
				trDateRange.Visible=false;  
			}
			else
			{
                //CHG0109406 - CH1 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                //TimeZoneChange.Ch2-Added Text MST Arizona at the end of the current date and time  by cognizant.
                lblDateRange.Text = Session["DateRange"].ToString() + " " + "Arizona";
			}
            //TimeZoneChange.Ch2-Added Text MST Arizona at the end of the current date and time  by cognizant.
            lblRunDate.Text = DateTime.Now.ToString("MM/dd/yyyy hh:mm:ss tt") + " " + "Arizona";
            //CHG0109406 - CH1 - END - Modified the timezone from "MST Arizona" to Arizona
			lblUser.Text= Session["Users"].ToString(); 
			lblReportStatus.Text = Session["Status"].ToString();    
		}


		public void ItemDataBound1(Object sender, DataGridItemEventArgs e)
		{
			
			DataRowView drvReportData = (DataRowView) e.Item.DataItem;
			if(e.Item.ItemType == ListItemType.Header)
			{
				e.Item.Cells[5].Text = e.Item.Cells[5].Text.Replace("<","&LT;");
				e.Item.Cells[7].Text = e.Item.Cells[7].Text.Replace(">","&GT;");
			}  
			if (drvReportData == null)
			return;
			
			#region Amount Alignment	
			
			//If the selected report is Aged transactions Report

			if(Convert.ToString(Session["ReportType"])==Cache["Aged Transactions Report Desc"].ToString())
			{
				e.Item.Cells[5].Attributes.Add("align","right");    
				e.Item.Cells[6].Attributes.Add("align","right");
				e.Item.Cells[7].Attributes.Add("align","right");
			}
				//If the selected report is  Status Report
			else if(Convert.ToString(Session["ReportType"])==Cache["Status Report Desc"].ToString())
			{
				//For Void status only
				if(Convert.ToString(Session["Status"])==Cache["Void Desc"].ToString())   
				{
					
					/*
					 * 11/28/2005 Q4-Retrofit.Ch1 - START Added by COGNIZANT 
					 *			  A new column 'Product' is added to Report Grid of status 'void' so,
					 *			  the alignment changes are made for the Amount column in Report Grid hence modifying
					 *			  the indices of cells of DataGridItem.(Changing cell indices in Amount Alignment).
					 */
					e.Item.Cells[4].Attributes.Add("align","right");	// Changed the Cell Index from 3 to 4
					e.Item.Cells[7].Attributes.Add("align","right");	// Changed the Cell Index from 6 to 7
					/*
					 * 11/28/2005 Q4-Retrofit.Ch1 - END
					 */
				}
				else
				{
					//STAR Retrofit II.Ch1 : START Modified to align amount column for Manual Void Report
					if(Convert.ToString(Session["Status"]) == Cache["Manual Void Desc"].ToString())   
					{
						e.Item.Cells[9].Attributes.Add("align","right");
					}
					else
					{
						e.Item.Cells[10].Attributes.Add("align","right");
					}
					//STAR Retrofit II.Ch1 : END
				}
				
			}
			#endregion
	
			#region Aged Transactions Report Section
			//If the selected report is Aged transactions Report
			if(Convert.ToString(Session["ReportType"])==Cache["Aged Transactions Report Desc"].ToString())
			{
				dgReport.Width = 604;    
				//Date and Time Cell
                //CHG0109406 - CH2 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                //Start-TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant on 8-31-2010 as a part on Time Zone Change Enhancement.
				if(!Convert.IsDBNull(drvReportData["Receipt Date/Time(Arizona)"])) 
				e.Item.Cells[0].Text = Convert.ToDateTime(drvReportData["Receipt Date/Time(Arizona)"]).ToString("MM/dd/yyyy   hh:mm:ss tt");
                //End-TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant on 8-31-2010 as a part on Time Zone Change Enhancement.			
                //CHG0109406 - CH2 - END - Modified the timezone from "MST Arizona" to Arizona
				e.Item.Cells[0].Width = 80;

				//Asssign the width of  ReceiptNumber Cell
				e.Item.Cells[1].Width = 80;
				//Asssign the width of Policy/Mbr Number Cell
				e.Item.Cells[2].Width = 75;
				
				/*
				 * 11/29/2005 Q4-Retrofit.Ch3 - START - Added by COGNIZANT 
                 *			  This code fixes the alignment issue for Membership & Policy Numbers in the XLS view.
                 *			  A single quote is prefixed with the Policy & Membership Number to left align them in the XLS view.
                 */
				if ((Request.QueryString["type"] == "XLS") && (e.Item.ItemIndex != (dtbReportData.Rows.Count-1)))
				{
					e.Item.Cells[2].Text = "'" + e.Item.Cells[2].Text;
				}
				/*
				 *11/29/2005 Q4-Retrofit.Ch3 - END
				 */ 	

				//Asssign the width of Users Cell
				e.Item.Cells[3].Width = 100;
				//Asssign the width of Rep Do cell 
				e.Item.Cells[4].Width = 30;
  				//<5 Days Amount 
				if(drvReportData["<5 Days"].ToString()!="0") 
					e.Item.Cells[5].Text = String.Format("{0:c}",drvReportData["<5 Days"]).ToString();
				else 
					e.Item.Cells[5].Text = "";	
				e.Item.Cells[5].Width = 73; 
				
				//5 -  10 Days Amount
				if(drvReportData["5 - 10 Days"].ToString()!="0") 
					e.Item.Cells[6].Text = String.Format("{0:c}",drvReportData["5 - 10 Days"]).ToString();	   
				else 
					e.Item.Cells[6].Text = "";	
				e.Item.Cells[6].Width = 73;
				
				//>10 Days Amount
				if(drvReportData[">10 Days"].ToString()!="0") 
					e.Item.Cells[7].Text = String.Format("{0:c}",drvReportData[">10 Days"]).ToString();	   
				else 
					e.Item.Cells[7].Text = "";	
				e.Item.Cells[7].Width = 73;				
				
				//Change the  color Sequence according to the Receipt Numbers
				AlternatingColor(e); 
				if(e.Item.ItemIndex==(dtbReportData.Rows.Count-1))       
				{
					e.Item.Cells.RemoveAt(4);
					e.Item.Cells[3].Attributes.Add("colspan","2");
					e.Item.Cells[3].Attributes.Add("align","right");
		
				}
			}
			#endregion

			#region Status Report Section
			//If the selected report is  Status Report
			if(Convert.ToString(Session["ReportType"])==Cache["Status Report Desc"].ToString())
			{
				
				//For Void status only
				if(Convert.ToString(Session["Status"])==Cache["Void Desc"].ToString())
				{
					dgReport.Width=604; 
					//Date and Time Cell
                    //CHG0109406 - CH3 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                    //Start-TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant on 8-31-2010 as a part on Time Zone Change Enhancement.
					if(!Convert.IsDBNull(drvReportData["Receipt Date/Time(Arizona)"])) 
					e.Item.Cells[0].Text = Convert.ToDateTime(drvReportData["Receipt Date/Time(Arizona)"]).ToString("MM/dd/yyyy   hh:mm:ss tt");
                    //End-TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant on 8-31-2010 as a part on Time Zone Change Enhancement.
                    //CHG0109406 - CH3 - END - Modified the timezone from "MST Arizona" to Arizona
					e.Item.Cells[0].Width = 80;
					
					
					/*
					 * 11/28/2005 Q4-Retrofit.Ch2 - START - Added by COGNIZANT 
					 *			  A new column 'Product' is added to Report Grid of status 'void' so,
					 *			  the alignment changes are made for the other column in Report Grid hence modifying
					 *			  the indices of cells of DataGridItem.(Changing cell indices in Status Report Section).
					 */

					//Asssign the width of  Customer Name Cell
					e.Item.Cells[2].Width = 100;	// Changed the Cell Index from 1 to 2
					//Asssign the width of  ReceiptNumber Cell
					e.Item.Cells[3].Width = 80;		// Changed the Cell Index from 2 to 3
					//Asssign the width of Amount Cell
					if(!Convert.IsDBNull(drvReportData["Original Amount"]))
						e.Item.Cells[4].Text = String.Format("{0:c}",drvReportData["Original Amount"]).ToString();	// Changed the Cell Index from 3 to 4
					e.Item.Cells[4].Width = 73;   // Changed the Cell Index from 3 to 4
					//Void/Reisse Date and Time Cell
                    //CHG0109406 - CH4 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                    //Start-TimeZoneChange.Ch4-Changed the Void/Reissue Date to Void/Reissue Date/Time(MST Arizona)
					if(!Convert.IsDBNull(drvReportData["Void/Reissue Date/Time(Arizona)"]))
						e.Item.Cells[5].Text = Convert.ToDateTime(drvReportData["Void/Reissue Date/Time(Arizona)"]).ToString("MM/dd/yyyy   hh:mm:ss tt");	// Changed the Cell Index from 4 to 5
                    //End-TimeZoneChange.Ch4-Changed the Void/Reissue Date to Void/Reissue Date/Time(MST Arizona)
                    //CHG0109406 - CH4 - END - Modified the timezone from "MST Arizona" to Arizona
					e.Item.Cells[5].Width = 80;	// Changed the Cell Index from 4 to 5
					//Asssign the width of  New ReceiptNumber Cell
					e.Item.Cells[6].Width = 80;	// Changed the Cell Index from 5 to 6
					//Asssign the width of New Amount Cell
					if(!Convert.IsDBNull(drvReportData["New Amount"]))
						e.Item.Cells[7].Text = String.Format("{0:c}",drvReportData["New Amount"]).ToString();	// Changed the Cell Index from 6 to 7
					e.Item.Cells[7].Width = 73;		// Changed the Cell Index from 6 to 7
					//Asssign the width of Rep Do cell 
					e.Item.Cells[9].Width = 30;	// Changed the Cell Index from 8 to 9
					AlternatingColor(e);
					if(e.Item.DataSetIndex==dtbReportData.Rows.Count-1)   
					{
						e.Item.Cells.RemoveAt(5);	// Changed the Cell Index from 4 to 5
						e.Item.Cells[4].Text="";   // Changed the Cell Index from 3 to 4
						e.Item.Cells[5].Attributes.Add("colspan","2");	// Changed the Cell Index from 4 to 5
						e.Item.Cells[5].Width=160;	// Changed the Cell Index from 4 to 5
					}
					
					/*
					 * 11/28/2005 Q4-Retrofit.Ch2 - END
					 */
  
				}
				//For Other statuses
				else
				{	
					dgReport.Width=720; 
					//Date and Time Cell
                    //CHG0109406 - CH5 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                    //Start-TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant on 8-31-2010 as a part on Time Zone Change Enhancement.
					if(!Convert.IsDBNull(drvReportData["Receipt Date/Time(Arizona)"]))  
					e.Item.Cells[0].Text = Convert.ToDateTime(drvReportData["Receipt Date/Time(Arizona)"]).ToString("MM/dd/yyyy   hh:mm:ss tt");
                    //End-TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant on 8-31-2010 as a part on Time Zone Change Enhancement.
                    //CHG0109406 - CH5 - END - Modified the timezone from "MST Arizona" to Arizona
					e.Item.Cells[0].Width = 80;
					//Asssign the width of  Customer Name Cell
					e.Item.Cells[1].Width = 100;
					//Asssign the width of  ReceiptNumber Cell
					e.Item.Cells[2].Width = 80;
					//Asssign the width of Policy/Mbr Number Cell
					e.Item.Cells[5].Width = 75;
					/*
				     * 11/29/2005 Q4-Retrofit.Ch3 - START - Added by COGNIZANT 
				     *			  This code fixes the alignment issue for Membership & Policy Numbers in the XLS view.
				     *			  A single quote is prefixed with the Policy & Membership Number to left align them in the XLS view.
				     */
					if ((Request.QueryString["type"] == "XLS") && (e.Item.ItemIndex != (dtbReportData.Rows.Count-1)))
					{
						e.Item.Cells[5].Text = "'" + e.Item.Cells[5].Text;
					}
					/*
					 * 11/29/2005 Q4-Retrofit.Ch3 - END
					 */ 	

					//Asssign the width of Users Cell
					e.Item.Cells[6].Width = 100;
					//Asssign the width of Rep Do cell 
					e.Item.Cells[7].Width = 30;
					//Asssign the width of Cashier ID Cell
					e.Item.Cells[8].Width = 100;
					
					//STAR Retrofit II.Ch2 : START Modified to assign widths for the new columns of Manual Void Report
					//Asssign the width of Amount Cell
					if(Convert.ToString(Session["Status"]) == Cache["Manual Void Desc"].ToString() )   
					{
						if(!Convert.IsDBNull(drvReportData["Amount"]))
							e.Item.Cells[9].Text = String.Format("{0:c}",drvReportData["Amount"]).ToString();	   
						e.Item.Cells[10].Width = 100;
                        //CHG0109406 - CH6 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                        //Start-TimeZoneChange.Ch3-Changed the Modified  Date to Modified Date/Time(MST Arizona) by cognizant.
						if(!Convert.IsDBNull(drvReportData["Modified Date/Time(Arizona)"]))
							e.Item.Cells[11].Text = Convert.ToDateTime(drvReportData["Modified Date/Time(Arizona)"]).ToString("MM/dd/yyyy   hh:mm:ss tt");
                        //End-TimeZoneChange.Ch3-Changed the Modified  Date to Modified Date/Time(MST Arizona) by cognizant.
                        //CHG0109406 - CH6 - END - Modified the timezone from "MST Arizona" to Arizona
						e.Item.Cells[11].Width = 80;
						if(e.Item.DataSetIndex==dtbReportData.Rows.Count-1)   
						{
							e.Item.Cells[11].Text = ""; 
						}
						
					}
					else
					{
						if(!Convert.IsDBNull(drvReportData["Amount"]))
							e.Item.Cells[10].Text = String.Format("{0:c}",drvReportData["Amount"]).ToString();	   
					}
					//STAR Retrofit II.Ch2 : END
					
					//Change the  color Sequence according to the Receipt Numbers
					AlternatingColor(e); 

				}
			
			}
			#endregion

			if(e.Item.ItemIndex==(dtbReportData.Rows.Count-1))       
			{
				e.Item.Attributes.Add("class","total_row"); 
				e.Item.Attributes.Add("align","right");
				e.Item.Attributes.Add("bgcolor",ROW_CLASS);  
				//For last Row remove the Receipt Date and time
				e.Item.Cells[0].Text = "";  
				for(int cellCount=0;cellCount<e.Item.Cells.Count;cellCount++)
				{
					e.Item.Cells[cellCount].Attributes.Add("class","total_cell");    
				}
								   					
			}
		}

		//Displaying the details of the same receipt number in a single colour
		//Making the Receipt Date,Status, Member Name, checkbox of same receipt number invisible
		private void AlternatingColor(DataGridItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				string strReceiptNumber;
				if(Convert.ToString(Session["Status"]) == Cache["Void Desc"].ToString())   
				{
					strReceiptNumber = e.Item.Cells[dtbReportData.Columns.IndexOf("Original Receipt Number")].Text;
				}
				else
				{
					strReceiptNumber = e.Item.Cells[dtbReportData.Columns.IndexOf("Receipt Number")].Text;
				}
				if(strReceiptNumber !=tmpReceipt)
				{
					AssignClass(colour,e);
				}
				else
				{	
					if(Convert.ToString(Session["Status"])== Cache["Void Desc"].ToString())   
					{
						// Clear the Receipt number, Original Amount and Receipt Date/Time fields for Void Status
						e.Item.Cells[dtbReportData.Columns.IndexOf("Original Receipt Number")].Text = "";
						e.Item.Cells[dtbReportData.Columns.IndexOf("Original Amount")].Text = "";
                        //CHG0109406 - CH7 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                        //TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) by cognizant on 8-31-2010 as a part on Time Zone Change Enhancement.
						e.Item.Cells[dtbReportData.Columns.IndexOf("Receipt Date/Time(Arizona)")].Text = "";
					}
					else
					{
						// Clear the Receipt number, Original Amount and Receipt Date/Time fields for Void Status
						e.Item.Cells[dtbReportData.Columns.IndexOf("Receipt Number")].Text = "";
						e.Item.Cells[dtbReportData.Columns.IndexOf("Receipt Date/Time(Arizona)")].Text = "";
                        //CHG0109406 - CH7 - END - Modified the timezone from "MST Arizona" to Arizona
					}
					e.Item.Attributes.Add("bgcolor",colour);
				}
				tmpReceipt=strReceiptNumber; 
			}
		}

	

		//Swapping of the colour between ALTERNATE_ROW_CLASS and ROW_CLASS
		private void AssignClass(string tmpcolour,DataGridItemEventArgs e)
		{
			if ( tmpcolour == ROW_CLASS )
			{
				e.Item.Attributes.Add("bgcolor",ALTERNATE_ROW_CLASS);
				colour = ALTERNATE_ROW_CLASS;
			}
			else if ( tmpcolour == ALTERNATE_ROW_CLASS )
			{
				e.Item.Attributes.Add("bgcolor",ROW_CLASS);
				colour = ROW_CLASS;
			}
		}

		
	
	}
}
