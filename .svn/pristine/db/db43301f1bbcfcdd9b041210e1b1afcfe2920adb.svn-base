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
using System.Diagnostics;
using System.Configuration;
using CSAAWeb;
using CSAAWeb.AppLogger;

namespace MSCR.Reports
{
	/// <summary>
	/// Summary description for PrintRpt.
	/// </summary>
	public partial class InsPrintRpt : System.Web.UI.Page
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
				DataView dv2 = dsExport.Tables[1].DefaultView;
				DataView dv3 = dsExport.Tables[2].DefaultView;

				Page.EnableViewState = false; //Turn off the view	state.

				Reports.InsRptHeader ctlHeader;
				ctlHeader = (Reports.InsRptHeader)FindControl("ctlHeader");
				ctlHeader.DataBind();

				if (dsExport.Tables[0].Rows.Count > 0)
				{
					dgReport1.DataSource = dv1;
					dgReport1.DataBind();
					//dgReport1.RenderControl(hw);
				}
				if (dsExport.Tables[1].Rows.Count > 0)
				{
					dgReport2.DataSource = dv2;
					dgReport2.DataBind();
					//dgReport2.RenderControl(hw);
				}
				if (dsExport.Tables[2].Rows.Count > 0)
				{
					dgReport3.DataSource = dv3;
					dgReport3.DataBind();
					//dgReport3.RenderControl(hw);
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
					e.Item.Cells[0].Width = 250;
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
				case 4:
					e.Item.BackColor = clr;
					if (drv["Date"].ToString() == "")
					{
						e.Item.BackColor = clr;
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Sub-total";
			
					}
					if (drv["User"].ToString() == "")
					{
						e.Item.BackColor = Color.LightGray;
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Report Total";
				
					}
					if (e.Item.Cells[0].Text == "Sub-total")
					{
						if (e.Item.BackColor == Color.White)
							clr = Color.LightYellow;
						else
							clr = Color.White;
					}
					e.Item.Cells[2].HorizontalAlign =  HorizontalAlign.Right;
					e.Item.Cells[3].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
					e.Item.Cells[3].HorizontalAlign =  HorizontalAlign.Right;					break;
				case 5:
					e.Item.BackColor = clr;
					if (drv["User"].ToString() == "")
					{
						e.Item.BackColor = clr;
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Sub-total";
				
					}
					if (drv["Date"].ToString() == "")
					{
						e.Item.BackColor = Color.LightGray;
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Report Total";
			
					}
					if (e.Item.Cells[0].Text == "Sub-total")
					{
						if (e.Item.BackColor == Color.White)
							clr = Color.LightYellow;
						else
							clr = Color.White;
					}
					e.Item.Cells[2].HorizontalAlign =  HorizontalAlign.Right;
					e.Item.Cells[3].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
					e.Item.Cells[3].HorizontalAlign =  HorizontalAlign.Right;					break;
				case 6:
					e.Item.Cells[8].Text = (String.Format("{0:c}",drv["Total"])).ToString();
					e.Item.Cells[8].HorizontalAlign =  HorizontalAlign.Right;
					break;
				case 7:
					e.Item.Cells[6].Text = (String.Format("{0:c}",drv["Amount"])).ToString();
					e.Item.Cells[6].HorizontalAlign =  HorizontalAlign.Right;
					lblCyber.Visible = true;
					break;
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
				case 1:
					e.Item.Cells[0].Width = 250;
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
				case 4:
					e.Item.BackColor = clr;
					if (drv["Date"].ToString() == "")
					{
						e.Item.BackColor = clr;
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Sub-total";
			
					}
					if (drv["User"].ToString() == "")
					{
						e.Item.BackColor = Color.LightGray;
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Report Total";
				
					}
					if (e.Item.Cells[0].Text == "Sub-total")
					{
						if (e.Item.BackColor == Color.White)
							clr = Color.LightYellow;
						else
							clr = Color.White;
					}
					e.Item.Cells[2].HorizontalAlign =  HorizontalAlign.Right;
					e.Item.Cells[3].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
					e.Item.Cells[3].HorizontalAlign =  HorizontalAlign.Right;					break;
				case 5:
					e.Item.BackColor = clr;
					if (drv["User"].ToString() == "")
					{
						e.Item.BackColor = clr;
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Sub-total";
				
					}
					if (drv["Date"].ToString() == "")
					{
						e.Item.BackColor = Color.LightGray;
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Report Total";
			
					}
					if (e.Item.Cells[0].Text == "Sub-total")
					{
						if (e.Item.BackColor == Color.White)
							clr = Color.LightYellow;
						else
							clr = Color.White;
					}
					e.Item.Cells[2].HorizontalAlign =  HorizontalAlign.Right;
					e.Item.Cells[3].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
					e.Item.Cells[3].HorizontalAlign =  HorizontalAlign.Right;					break;
				case 6:
					e.Item.Cells[8].Text = (String.Format("{0:c}",drv["Total"])).ToString();
					e.Item.Cells[8].HorizontalAlign =  HorizontalAlign.Right;
					break;
				case 7:
					e.Item.Cells[7].Text = Cryptor.Decrypt(drv["Credit Card Number"].ToString(),Config.Setting("CSAA_ORDERS_KEY"));
					e.Item.Cells[8].Text = (String.Format("{0:c}",drv["Amount"])).ToString();
					e.Item.Cells[8].HorizontalAlign =  HorizontalAlign.Right;
					lblAPDS.Visible = true;
					break;
			}
		}

		// EVENT HANDLER: ItemDataBound
		public void ItemDataBound3(Object sender, DataGridItemEventArgs e)
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
					e.Item.Cells[0].Width = 250;
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
				case 4:
					e.Item.BackColor = clr;
					if (drv["Date"].ToString() == "")
					{
						e.Item.BackColor = clr;
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Sub-total";
			
					}
					if (drv["User"].ToString() == "")
					{
						e.Item.BackColor = Color.LightGray;
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Report Total";
				
					}
					if (e.Item.Cells[0].Text == "Sub-total")
					{
						if (e.Item.BackColor == Color.White)
							clr = Color.LightYellow;
						else
							clr = Color.White;
					}
					e.Item.Cells[2].HorizontalAlign =  HorizontalAlign.Right;
					e.Item.Cells[3].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
					e.Item.Cells[3].HorizontalAlign =  HorizontalAlign.Right;					break;
				case 5:
					e.Item.BackColor = clr;
					if (drv["User"].ToString() == "")
					{
						e.Item.BackColor = clr;
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Sub-total";
				
					}
					if (drv["Date"].ToString() == "")
					{
						e.Item.BackColor = Color.LightGray;
						e.Item.Font.Bold = true; 
						e.Item.Cells[0].Text = "Report Total";
			
					}
					if (e.Item.Cells[0].Text == "Sub-total")
					{
						if (e.Item.BackColor == Color.White)
							clr = Color.LightYellow;
						else
							clr = Color.White;
					}
					e.Item.Cells[2].HorizontalAlign =  HorizontalAlign.Right;
					e.Item.Cells[3].Text = (String.Format("{0:c}",drv["Revenue"])).ToString();
					e.Item.Cells[3].HorizontalAlign =  HorizontalAlign.Right;					break;
				case 6:
					e.Item.Cells[8].Text = (String.Format("{0:c}",drv["Total"])).ToString();
					e.Item.Cells[8].HorizontalAlign =  HorizontalAlign.Right;
					break;

			}
		}


	}
}
