/*
 * Modified by Cognizant as a part of Enabling Default Printing on 7/02/2008:
 * PT.Ch1 - To enable the default printing of the Print Version to print the Reports
 * 
 * Revision # 2: PT.Ch1 -- Modified by COGNIZANT on 9/8/2008
 * Commented few lines to enable LightYellow alternate item color for report types 4, 5, 10 & 11
 * 
 * MODIFIED AS A PART OF TIME ZONE CHANGE ENHANCEMENT ON 8-31-2010.
 * TimeZoneChange.Ch1-Modified the Transaction Date to Transaction Date/Time(MST Arizona) by cognizant.
 * TimeZoneChange.Ch2-Added Text MST Arizona at the end of the current date and time and DateRange by cognizant.
 
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
using CSAAWeb.AppLogger;

namespace MSCR.Reports
{
	/// <summary>
	/// Summary description for PrintRpt.
	/// </summary>
	public partial class PrintRpt : System.Web.UI.Page
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

				DataView dv = dsExport.Tables[0].DefaultView;

				string erpt = (string) Session["MyReportType"]; 
				string csrs = (string) Session["CSRs"]; 

				Page.EnableViewState = false; //Turn off the view	state.

				System.IO.StringWriter tw = new		System.IO.StringWriter();
				System.Web.UI.HtmlTextWriter hw = new	System.Web.UI.HtmlTextWriter(tw);

				hw.Write("<HTML><HEAD>	<title>Sales &amp; Service Payment Tool</title>");				
				hw.Write("<link rel=stylesheet type=text/css href=../style.css>");
				//PT.Ch1 - START: Added by COGNIZANT - 7/02/2008 - To enable default printing of the Print Version
				hw.Write("<script language=javascript>");
				hw.Write("window.print();");
				hw.Write("</script>");
				//PT.Ch1 - END
				hw.Write("</HEAD>");
				hw.Write("<body>");
			
				tblHeader.RenderControl(hw);

				hw.Write("<table width=606 bgcolor=LightYellow class=arial_12>");
				hw.Write("<tr><td colspan=2><b>");
				hw.Write(Session["RptTitle"]);
				hw.Write("</b></td></tr>");
				hw.Write("<tr><td colspan=2>&nbsp;</td></tr>");
				hw.Write("<tr><td>");
				hw.Write("<b>Run Date:</b>");
				hw.Write("</td><td>");
                //TimeZoneChange.Ch2-Added Text MST Arizona at the end of the current date and time and DateRange by cognizant.
                hw.Write(DateTime.Now.ToString() + " " + "MST Arizona");
				hw.Write("</td></tr>");		
				hw.Write("<tr><td>");
				hw.Write("<b>Date Range:</b>");
				hw.Write("</td><td>");
                //TimeZoneChange.Ch2-Added Text MST Arizona at the end of the current date and time and DateRange by cognizant.
                hw.Write(Session["DateRange"]+ " "+"MST Arizona");
				hw.Write("</td></tr>");

				//Code Added by Cognizant for Payment Type
				hw.Write("<tr><td>");
				hw.Write("<b>Payment Type:</b>");
				hw.Write("</td><td>");
				hw.Write(Session["PaymentName"]);
				hw.Write("</td></tr>");
				//Code Added by Cognizant for Payment Type

				if (csrs != String.Empty)
				{
					hw.Write("<tr><td>");
					hw.Write("<b>Users:</b>");
					hw.Write("</td><td>");
					hw.Write(Session["CSRs"]);
					hw.Write("</td></tr>");
				}
				hw.Write("</table>");
			
				if (erpt == "4" || erpt == "10") 
				{	
					dgGrid.DataSource = dv;
					dgGrid.DataBind();
					dgGrid.RenderControl(hw);
				}
				else if (erpt == "5" || erpt == "11") 
				{
					dgGrid1.DataSource = dv;
					dgGrid1.DataBind();
					dgGrid1.RenderControl(hw);
				}
				else
				{
					dgGrid2.DataSource = dv;
					dgGrid2.DataBind();
					dgGrid2.RenderControl(hw);
				}
		
				hw.Write("</body></html>");
			
				Response.Write(tw.ToString()); //Write the HTML back			to the browser.
				Response.End(); 
			}
			catch  (Exception ex) 
			{
				Logger.Log(ex);
				//Response.Write("Please try clicking the submit button and loading the report again. If that doesn't work , try closing the browser and run the report again.");
				//throw;
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

		public void ItemCreated(Object sender, DataGridItemEventArgs e)
		{
			// Get the type of the newly created item
			ListItemType itemType = e.Item.ItemType;
			if (itemType == ListItemType.Item || 
				itemType == ListItemType.AlternatingItem) 
			{
				// Get the data bound to the current row
				DataRowView drv = (DataRowView) e.Item.DataItem;
				if (drv != null)
				{
					if (drv["DTCREATED"].ToString() == "")
					{
//						e.Item.BackColor = Color.LightYellow;//PT.Ch2 - Commented by COGNIZANT on 9/8/08 to enable LightYellow alternate item color for report types 4 & 10
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Sub-total";
						
					}
					if (drv["USERNAME"].ToString() == "")
					{
						e.Item.BackColor = Color.LightGray;
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Report Total";
						
					}
				}


			}
		}

		// EVENT HANDLER: ItemDataBound
		public void ItemDataBound(Object sender, DataGridItemEventArgs e)
		{
			// Retrieve the data linked through the relation
			// Given the structure of the data ONLY ONE row is retrieved
			DataRowView drv = (DataRowView) e.Item.DataItem;
			if (drv == null)
				return;

//			e.Item.BackColor = clr;//PT.Ch2 - Commented by COGNIZANT on 9/8/08 to enable LightYellow alternate item color for report types 4 & 10
			// Check here the app-specific way to detect whether the 
			// current row is a summary row
			if (drv["DTCREATED"].ToString() == "")
			{
//				e.Item.BackColor = clr;//PT.Ch2 - Commented by COGNIZANT on 9/8/08 to enable LightYellow alternate item color for report types 4 & 10
				e.Item.Font.Bold = true; 
				e.Item.Cells[0].Text = "Sub-total";
			
			}
			if (drv["USERNAME"].ToString() == "")
			{
				e.Item.BackColor = Color.LightGray;
				e.Item.Font.Bold = true; 
				e.Item.Cells[0].Text = "Report Total";
				
			}
//			START - PT.Ch2 - Commented by COGNIZANT on 9/8/08 to enable LightYellow alternate item color for report type 4 & 10
//			if (e.Item.Cells[0].Text == "Sub-total")
//			{
//				if (e.Item.BackColor == Color.White)
//					clr = Color.LightYellow;
//				else
//					clr = Color.White;
//			}
//			END - PT.Ch2

		}

		public void ItemCreated1(Object sender, DataGridItemEventArgs e)
		{
			// Get the type of the newly created item
			ListItemType itemType = e.Item.ItemType;
			if (itemType == ListItemType.Item || 
				itemType == ListItemType.AlternatingItem) 
			{
				// Get the data bound to the current row
				DataRowView drv = (DataRowView) e.Item.DataItem;
				if (drv != null)
				{
					if (drv["USERNAME"].ToString() == "")
					{
//						e.Item.BackColor = Color.LightYellow;//PT.Ch2 - Commented by COGNIZANT on 9/8/08 to enable LightYellow alternate item color for report types 5 & 11
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Sub-total";
						
					}
					if (drv["DTCREATED"].ToString() == "")
					{
						e.Item.BackColor = Color.LightGray;
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Report Total";
						
					}
				}


			}
		}

		// EVENT HANDLER: ItemDataBound
		public void ItemDataBound1(Object sender, DataGridItemEventArgs e)
		{
			// Retrieve the data linked through the relation
			// Given the structure of the data ONLY ONE row is retrieved
			DataRowView drv = (DataRowView) e.Item.DataItem;
			if (drv == null)
				return;

//				e.Item.BackColor = clr;//PT.Ch2 - Commented by COGNIZANT on 9/8/08 to enable LightYellow alternate item color for report types 5 & 11
			// Check here the app-specific way to detect whether the 
			// current row is a summary row
			if (drv["USERNAME"].ToString() == "")
			{
//				e.Item.BackColor = clr;//PT.Ch2 - Commented by COGNIZANT on 9/8/08 to enable LightYellow alternate item color for report types 5 & 11
				e.Item.Font.Bold = true; 
				e.Item.Cells[0].Text = "Sub-total";
				
			}
			if (drv["DTCREATED"].ToString() == "")
			{
				e.Item.BackColor = Color.LightGray;
				e.Item.Font.Bold = true; 
				e.Item.Cells[0].Text = "Report Total";
			
			}
//			START - PT.Ch2 - Commented by COGNIZANT on 9/8/08 to enable LightYellow alternate item color for report type 5 & 11
//			if (e.Item.Cells[0].Text == "Sub-total")
//			{
//				if (e.Item.BackColor == Color.White)
//					clr = Color.LightYellow;
//				else
//					clr = Color.White;
//			}
//			END - PT.Ch2

		}
		public void ItemCreated2(Object sender, DataGridItemEventArgs e)
		{
			// Get the type of the newly created item
			string rptType = (string) Session["MyReportType"];

			ListItemType itemType = e.Item.ItemType;
			if (itemType == ListItemType.Item || 
				itemType == ListItemType.AlternatingItem) 
			{
				// Get the data bound to the current row
				DataRowView drv = (DataRowView) e.Item.DataItem;
				if (drv != null)
				{
					int n = int.Parse(rptType);
					switch(n)
					{
						case 1:
							e.Item.Cells[1].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();						
							break;
						case 2:
							e.Item.Cells[1].HorizontalAlign =  HorizontalAlign.Right;
							e.Item.Cells[2].HorizontalAlign = HorizontalAlign.Right;
							e.Item.Cells[2].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
							if (drv["Date"].ToString() == "")
							{
								e.Item.BackColor = Color.LightGray;
								e.Item.Font.Bold = true; 
								e.Item.Cells[0].Text = "Total";
							}
							break;
						case 3:
							e.Item.Cells[1].HorizontalAlign =  HorizontalAlign.Right;
							e.Item.Cells[2].HorizontalAlign = HorizontalAlign.Right;
							e.Item.Cells[2].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
							if (drv["User"].ToString() == "")
							{
								e.Item.BackColor = Color.LightGray;
								e.Item.Font.Bold = true; 
								e.Item.Cells[0].Text = "Total";							
							}
							break;
						case 6:
							e.Item.Cells[7].Text = (String.Format("{0:c}",drv["Total"])).ToString();
							e.Item.Cells[7].HorizontalAlign =  HorizontalAlign.Right;
							break;
						case 7:
							goto case 1;
						case 8:
							goto case 2;
						case 9:
							goto case 3;
						case 12:
							goto case 6;
						case 13:
                            //TimeZoneChange.Ch1-Modified the Transaction Date to Transaction Date/Time(MST Arizona) by cognizant.
                            if (drv["Transaction Date/Time(MST Arizona)"].ToString() == "")
							{
								e.Item.BackColor = Color.LightYellow;
								e.Item.Font.Bold = true; 
								e.Item.Cells[1].HorizontalAlign =  HorizontalAlign.Right;
							}
							break;
                    }
				}


			}
		}

		// EVENT HANDLER: ItemDataBound
		public void ItemDataBound2(Object sender, DataGridItemEventArgs e)
		{
			string rptType = (string) Session["MyReportType"];
			// Retrieve the data linked through the relation
			// Given the structure of the data ONLY ONE row is retrieved
			DataRowView drv = (DataRowView) e.Item.DataItem;
			if (drv == null)
				return;

			// Check here the app-specific way to detect whether the 
			// current row is a summary row
			int n = int.Parse(rptType);
			switch(n)
			{
				case 1:
					e.Item.Cells[1].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();						
					break;
				case 2:
					e.Item.Cells[1].HorizontalAlign =  HorizontalAlign.Right;
					e.Item.Cells[2].HorizontalAlign = HorizontalAlign.Right;
					e.Item.Cells[2].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
					if (drv["Date"].ToString() == "")
					{
						e.Item.BackColor = Color.LightGray;
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Total";
					}
					break;
				case 3:
					e.Item.Cells[1].HorizontalAlign =  HorizontalAlign.Right;
					e.Item.Cells[2].HorizontalAlign = HorizontalAlign.Right;
					e.Item.Cells[2].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
					if (drv["User"].ToString() == "")
					{
						e.Item.BackColor = Color.LightGray;
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Total";							
					}
					break;
				case 6:
					e.Item.Cells[7].Text = (String.Format("{0:c}",drv["Total"])).ToString();
					e.Item.Cells[7].HorizontalAlign =  HorizontalAlign.Right;
					break;
				case 7:
					goto case 1;
				case 8:
					goto case 2;
				case 9:
					goto case 3;
				case 12:
					goto case 6;
				case 13:
                    //TimeZoneChange.Ch1-Modified the Transaction Date to Transaction Date/Time(MST Arizona) by cognizant.
                    if (drv["Transaction Date/Time(MST Arizona)"].ToString() == "")
					{
						e.Item.BackColor = Color.LightYellow;
						e.Item.Font.Bold = true; 
						e.Item.Cells[1].HorizontalAlign =  HorizontalAlign.Right;
					}
					break;
            }
		}


	}
}
