/*
 * MODIFIED AS A PART OF TIME ZONE CHANGE ENHANCEMENT ON 8-31-2010.
 * TimeZoneChange.Ch1-Modified the Transaction Date to Transaction Date/Time(MST Arizona) by cognizant.
 
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
	/// Summary description for LoadXls.
	/// </summary>
	public partial class LoadXls : System.Web.UI.Page
	{
	
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

				Session["ProductName"] = Session["RevenueName"] = Session["AppName"] = null;

				MSCR.Controls.PrintHeader PrintHeader;
				PrintHeader = (MSCR.Controls.PrintHeader)FindControl("PrintHeader");
				PrintHeader.DataBind();

				Response.ContentType = "application/vnd.ms-excel";
				Response.Charset = ""; //Remove the charset from the			Content-Type header.
				Page.EnableViewState = false; //Turn off the view	state.

				if (erpt == "4" || erpt == "10") 
				{	
					dgGrid.DataSource = dv;
					dgGrid.DataBind();
				}
				else if (erpt == "5" || erpt == "11") 
				{
					dgGrid1.DataSource = dv;
					dgGrid1.DataBind();
				}
				else
				{
					dgGrid2.DataSource = dv;
					dgGrid2.DataBind();
				}

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
						//e.Item.BackColor = Color.LightYellow;
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

			// Check here the app-specific way to detect whether the 
			// current row is a summary row
			if (drv["DTCREATED"].ToString() == "")
			{
				//e.Item.BackColor = Color.LightYellow;
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
						//e.Item.BackColor = Color.LightYellow;
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

			// Check here the app-specific way to detect whether the 
			// current row is a summary row
			if (drv["USERNAME"].ToString() == "")
			{
				//e.Item.BackColor = Color.LightYellow;
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

		public void ItemCreated2(Object sender, DataGridItemEventArgs e)
		{
			string rptType = (string) Session["MyReportType"];
			// Get the type of the newly created item
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
							e.Item.Cells[5].Text = "'" + drv["Request_ID"];
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
                            //TimeZoneChange.Ch1-Modified the Transaction Date to Transaction Date/Time(MST Arizona) by cognizant on 8-31-2010 as a part of Time Zone Change enhancement.
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
					e.Item.Cells[5].Text = "'" + drv["Request_ID"];
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
