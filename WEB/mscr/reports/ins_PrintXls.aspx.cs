/*
 * MODIFICATION HISTORY:
 * Revision # 1: PT.Ch1  -- Modified by COGNIZANT on 8/19/2008
 * Modified to left align the Policy Number for Insurance Transaction Detail report in the datagrid on the excel version.
 * Added a single quote for the Policy Number column in the excel version only.
 * Revision # 2: PT.Ch1 -- Modified by COGNIZANT on 9/8/2008
 * Commented few lines to enable LightYellow alternate item color for report types 4 & 5
 * 67811A0 - PCI Remediation for Payment systems CH1: Changed the cell number to display the policy prefix and policy state in the excel version of  Insurance Transaction Detail report.
 * CHG0109406 - CH1 - Added below code to hide/display lblHeaderTimeZone to display the timezone 
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
	/// Summary description for LoadXls.
	/// </summary>
	public partial class ins_PrintXls : System.Web.UI.Page
	{
	
		protected System.Drawing.Color clr = Color.White;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{
			try
			{
				// Put user code to initialize the page here
				DataSet dsExport = (DataSet) Session["MyDataSet"];
				if (dsExport == null)
				{
					Response.Write("Your session has timed out. Please re-submit the page");
					return;
				}
				DataView dv1 = dsExport.Tables[0].DefaultView;
                //DataView dv2 = dsExport.Tables[1].DefaultView;
                //CHG0109406 - CH1 - BEGIN - Added below code to hide/display lblHeaderTimeZone to display the timezone 
                string rptType = (string)Session["MyReportType"];
                if (rptType.Equals("2"))
                {
                    lblHeaderTimeZone.Visible = true;
                }
                else
                {
                    lblHeaderTimeZone.Visible = false;
                }
                //CHG0109406 - CH1 - END - Added below code to hide/display lblHeaderTimeZone to display the timezone 
                if (Request.QueryString["type"] == "XLS")
                {


                    
                    Response.ContentType = "application/vnd.ms-excel";
                    Response.Charset = ""; //Remove the charset from the Content-Type header.

                    
                }
								
				Page.EnableViewState = false; //Turn off the view	state.
	
				MSCR.Controls.PrintHeader PrintHeader;
				PrintHeader = (MSCR.Controls.PrintHeader)FindControl("PrintHeader");
				PrintHeader.DataBind();

				if (dsExport.Tables[0].Rows.Count > 0)
                {
                    
					dgReport1.DataSource = dv1;
					dgReport1.DataBind();
                   
					//dgReport1.RenderControl(hw);
				}
                //if (dsExport.Tables[1].Rows.Count > 0)
                //{
                    
                //    dgReport2.DataSource = dv2;
                //    dgReport2.DataBind();
                //    //dgReport2.RenderControl(hw);
                //}
                
						
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
			string rptType = (string) Session["MyReportType"];
			int n = int.Parse(rptType);

			// Retrieve the data linked through the relation
			// Given the structure of the data ONLY ONE row is retrieved
			DataRowView drv = (DataRowView) e.Item.DataItem;
			if (drv == null)
				return;

			// Check here the app-specific way to detect whether the 
			// current row is a summary row
			switch(n)
			{
				case 1:
					e.Item.Cells[1].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();						
					break;
               
                case 2:
                    // START - PT.Ch1 - Modified by COGNIZANT 8/19/08
                    // To left align the Policy Number in excel version of Insurance Transaction Detail report. Prefixed a single quote
                    if (Request.QueryString["type"] == "XLS") e.Item.Cells[7].Text = "'" + drv["Policy Number"];
                    // END - PT.Ch1
                    // 67811A0 - PCI Remediation for Payment systems CH1: Start - Changed the cell number to display the policy prefix and policy state in the excel version of  Insurance Transaction Detail report.
                    if (Request.QueryString["type"] == "XLS") e.Item.Cells[10].Text = "'" + drv["Request ID"];
                    //e.Item.Cells[12].Text = (String.Format("{0:c}", drv["Total"])).ToString();
                    e.Item.Cells[12].HorizontalAlign = HorizontalAlign.Right;
                    // 67811A0 - PCI Remediation for Payment systems CH1: End - Changed the cell number to display the policy prefix and policy state in the excel version of  Insurance Transaction Detail report.
                    break;

                default:
                    break;
//                case 2:
//                    e.Item.Cells[1].HorizontalAlign =  HorizontalAlign.Right;
//                    e.Item.Cells[2].HorizontalAlign = HorizontalAlign.Right;
//                    e.Item.Cells[2].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
//                    if (drv["Date"].ToString() == "")
//                    {
//                        e.Item.BackColor = Color.LightGray;
//                        e.Item.Font.Bold = true; 
//                        e.Item.Cells[0].Text = "Total";
//                    }
//                    break;
//                case 3:
//                    e.Item.Cells[1].HorizontalAlign =  HorizontalAlign.Right;
//                    e.Item.Cells[2].HorizontalAlign = HorizontalAlign.Right;
//                    e.Item.Cells[2].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
//                    if (drv["User"].ToString() == "")
//                    {
//                        e.Item.BackColor = Color.LightGray;
//                        e.Item.Font.Bold = true; 
//                        e.Item.Cells[0].Text = "Total";							
//                    }
//                    break;
//                case 4:
////					e.Item.BackColor = clr; //PT.Ch2 - Commented by COGNIZANT on 9/8/08 to enable LightYellow alternate item color for report type 4
//                    if (drv["Date"].ToString() == "")
//                    {
//                        e.Item.BackColor = clr;
//                        e.Item.Font.Bold = true; 
//                        e.Item.Cells[0].Text = "Sub-total";
			
//                    }
//                    if (drv["User"].ToString() == "")
//                    {
//                        e.Item.BackColor = Color.LightGray;
//                        e.Item.Font.Bold = true; 
//                        e.Item.Cells[0].Text = "Report Total";
				
//                    }
////					START - PT.Ch2 - Commented by COGNIZANT on 9/8/08 to enable LightYellow alternate item color for report type 4
////					if (e.Item.Cells[0].Text == "Sub-total")
////					{
////						if (e.Item.BackColor == Color.White)
////							clr = Color.LightYellow;
////						else
////							clr = Color.White;
////					}
////					END - PT.Ch2
//                    e.Item.Cells[2].HorizontalAlign =  HorizontalAlign.Right;
//                    e.Item.Cells[3].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
//                    e.Item.Cells[3].HorizontalAlign =  HorizontalAlign.Right;					break;
//                case 5:
////					e.Item.BackColor = clr;//PT.Ch2 - Commented by COGNIZANT on 9/8/08 to enable LightYellow alternate item color for report type 5
//                    if (drv["User"].ToString() == "")
//                    {
//                        e.Item.BackColor = clr;
//                        e.Item.Font.Bold = true; 
//                        e.Item.Cells[0].Text = "Sub-total";
				
//                    }
//                    if (drv["Date"].ToString() == "")
//                    {
//                        e.Item.BackColor = Color.LightGray;
//                        e.Item.Font.Bold = true; 
//                        e.Item.Cells[0].Text = "Report Total";
			
//                    }
////					START - PT.Ch2 - Commented by COGNIZANT on 9/8/08 to enable LightYellow alternate item color for report type 5
////					if (e.Item.Cells[0].Text == "Sub-total")
////					{
////						if (e.Item.BackColor == Color.White)
////							clr = Color.LightYellow;
////						else
////							clr = Color.White;
////					}
////					END - PT.Ch2
//                    e.Item.Cells[2].HorizontalAlign =  HorizontalAlign.Right;
//                    e.Item.Cells[3].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
//                    e.Item.Cells[3].HorizontalAlign =  HorizontalAlign.Right;					
//                    break;
//                case 6:
//                    // START - PT.Ch1 - Modified by COGNIZANT 8/19/08
//                    // To left align the Policy Number in excel version of Insurance Transaction Detail report. Prefixed a single quote
//                    if (Request.QueryString["type"] == "XLS")  e.Item.Cells[7].Text = "'" + drv["Policy Number"];
//                    // END - PT.Ch1
//                    // 67811A0 - PCI Remediation for Payment systems CH1: Start - Changed the cell number to display the policy prefix and policy state in the excel version of  Insurance Transaction Detail report.
//                    if (Request.QueryString["type"] == "XLS")  e.Item.Cells[10].Text = "'" + drv["Request ID"];
//                    e.Item.Cells[12].Text = (String.Format("{0:c}",drv["Total"])).ToString();
//                    e.Item.Cells[12].HorizontalAlign =  HorizontalAlign.Right;
//                    // 67811A0 - PCI Remediation for Payment systems CH1: End - Changed the cell number to display the policy prefix and policy state in the excel version of  Insurance Transaction Detail report.
//                    break;
//                case 7:
//                    if (Request.QueryString["type"] == "XLS")  e.Item.Cells[1].Text = "'" + drv["Request ID"];
//                    e.Item.Cells[6].Text = (String.Format("{0:c}",drv["Amount"])).ToString();
//                    e.Item.Cells[6].HorizontalAlign =  HorizontalAlign.Right;
//                    lblCyber.Visible = true;
//                    break;
			}
		}


		// EVENT HANDLER: ItemDataBound
		public void ItemDataBound2(Object sender, DataGridItemEventArgs e)
		{
			string rptType = (string) Session["MyReportType"];
			int n = int.Parse(rptType);
			// Retrieve the data linked through the relation
			// Given the structure of the data ONLY ONE row is retrieved
			DataRowView drv = (DataRowView) e.Item.DataItem;
			if (drv == null)
				return;

			// Check here the app-specific way to detect whether the 
			// current row is a summary row
			switch(n)
			{
				case 7:
					if (Request.QueryString["type"] == "XLS")  e.Item.Cells[8].Text = "'" + drv["Request ID"];
					e.Item.Cells[10].Text = Cryptor.Decrypt(drv["Credit Card Number"].ToString(),Config.Setting("CSAA_ORDERS_KEY"));
					e.Item.Cells[11].Text = (String.Format("{0:c}",drv["Amount"])).ToString();
					e.Item.Cells[11].HorizontalAlign =  HorizontalAlign.Right;
					lblAPDS.Visible = true;
					break;
			}
		}

	
	}
}
