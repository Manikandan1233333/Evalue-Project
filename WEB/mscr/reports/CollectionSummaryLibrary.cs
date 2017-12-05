/*
 * History:
 *			  --> Modified by COGNIZANT as part of Q4-retrofit <--
 * 
 * 12/16/2005 Q4-Retrofit.Ch1
 *            Code added to show the heading "PAYMENT TYPE" and "SUBTOTAL" in the "TOTAL CSAA PRODUCTS AND ALL OTHER PRODUCTS" grid.
 *  MODIFIED BY COGNIZANT AS A PART OF NAME CHANGE ON 8/19/2010
 *  *  NameChange.Ch1:changed CSAA to AAA NCNU IE as a part of namechange project.
 *  SalesTurnin_Report Changes.Ch1 : Added code not to add the header row for revenue Type in Excel.
 *  CHG0083477 - Name Change from AAA NCNU IE to CSAA IG as part of Name change project on 11/15/2013.
 */



using System;
using System.Data;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;

namespace MSCR.Reports
{
	/// <summary>
	/// Summary description for CollectionSummary.
	/// </summary>
	public class CollectionSummaryLibrary
	{
		// Adding column to the HTML table
		private void AddColumn(ref HtmlTableRow hr, string ColValue, int ColSpan,int RowSpan, string style, string width,string align )		
		{
			string strStyle="arial_12";
			HtmlTableCell hCol = new HtmlTableCell();
			if (ColValue == " ")
			{
				hCol.InnerHtml = "&nbsp;";
			}
			else
			{
				hCol.InnerText = ColValue;
			}
			hCol.ColSpan = ColSpan;
			hCol.RowSpan = RowSpan;
			if(width.Length > 0 )
			{
				hCol.Width = width.ToString();
			}
			if(style.Trim() != "") 
			{
				strStyle = style; 
			}
			hCol.Attributes.Add("class",strStyle);
			hCol.Attributes.Add("align",align);
			hr.Cells.Add(hCol);
		}

		// Adding column to the HTML table
		private void AddColumn(ref HtmlTableRow hr, string ColValue, int ColSpan,int RowSpan, string style, string width )		
		{
			AddColumn(ref hr, ColValue,ColSpan,RowSpan,style,width,"center");		
		}
		
		// Adding column to the HTML table

		private void AddColumn(ref HtmlTableRow hr, string ColValue, int ColSpan,int RowSpan, string style )		
		{
			AddColumn(ref hr, ColValue, ColSpan,RowSpan, style,"");
		}
		
		// Adding column to the HTML table

		private void AddColumn(ref HtmlTableRow hr, string ColValue, int ColSpan,int RowSpan)//, string style )
		{
			string style="arial_12";
			AddColumn(ref hr,ColValue,ColSpan,RowSpan,style);		
		}
		
		// Adding column to the HTML table

		private void AddColumn(ref HtmlTableRow hr, string ColValue, int ColSpan)
		{
			AddColumn(ref hr, ColValue, ColSpan,1);
		}

		// Adding column to the HTML table
		private void AddColumn(ref HtmlTableRow hr, string ColValue,int ColSpan,string style)
		{
			AddColumn(ref hr, ColValue, ColSpan,1,style);
		}
		
		//Creating the HTML header

		public void CreateHeader(ref System.Web.UI.HtmlControls.HtmlTable tblSummary, ref DataTable dtbTemp,string strHeading)
		{
			HtmlTableRow head0 = new HtmlTableRow();
			HtmlTableRow head1 = new HtmlTableRow();
			HtmlTableRow head2 = new HtmlTableRow();
			string tempColName = string.Empty;
			int tempSpan = 0;
            //SalesTurnin_Report Changes.Ch1 :START - Added code not to add the header row for revenue Type in Excel.
            string sales_excel = string.Empty;
            //SalesTurnin_Report Changes.Ch1 :END - Added code not to add the header row for revenue Type in Excel.


			AddColumn(ref head0,strHeading,dtbTemp.Columns.Count + 1,1,"","","left");

			
			//Creating the header by seperating the names using '|' seperator

			foreach(DataColumn dtCol in dtbTemp.Columns)
			{
				string colLeft = string.Empty;
				string colRight;
				string[] strTempArr;

				strTempArr = dtCol.ColumnName.Split('|');

				if ( strTempArr.Length > 1 )
				{
					colLeft = strTempArr[0].ToString();
                    //SalesTurnin_Report Changes.Ch1 :START - Added code not to add the header row for revenue Type in Excel.
                    if (colLeft == "PAYMENT TYPE")
                    {
                        sales_excel = colLeft;
                    }
                    //SalesTurnin_Report Changes.Ch1 :END - Added code not to add the header row for revenue Type in Excel.
					colRight = strTempArr[1].ToString();
				}
				else
				{
					AddColumn(ref head1,"PAYMENT TYPE",1,2,"item_template_header_white");
					continue;
				}

				tempSpan++;
				if ( colLeft != tempColName )
				{
					if ( tempColName != string.Empty )
					{
						AddColumn(ref head1,tempColName,tempSpan - 1,"item_template_header_white");
					}
					tempColName = colLeft;
					tempSpan = 1;
				}
                //AddColumn(ref head2, colRight, 1, "item_template_header_white");
                //SalesTurnin_Report Changes.Ch1 :START - Added code not to add the header row for revenue Type in Excel.
                if (sales_excel != "PAYMENT TYPE")
                {
                    AddColumn(ref head2, colRight, 1, "item_template_header_white");
                }
                //SalesTurnin_Report Changes.Ch1 :END - Added code not to add the header row for revenue Type in Excel.
			}

			if ( dtbTemp.Columns.Count > 0 )
			{
				AddColumn(ref head1,tempColName,tempSpan,"item_template_header_white" );
				//AddColumn(ref head1,"SUBTOTAL",1,2,"collection_summary_footer");
                //SalesTurnin_Report Changes.Ch1 :START - Added code not to add the header row for revenue Type in Excel.
                if (sales_excel != "PAYMENT TYPE")
                {
                    AddColumn(ref head1, "SUBTOTAL", 1, 2, "collection_summary_footer");
                }
                else
                {
                    AddColumn(ref head1, "SUBTOTAL", 1, "item_template_header_white");
                }
                //SalesTurnin_Report Changes.Ch1 :END - Added code not to add the header row for revenue Type in Excel.
			}
			head0.Attributes.Add("class","item_template_header");
			head1.Attributes.Add("class","arial_12_bold");
			head1.Attributes.Add("align","center");
			head2.Attributes.Add("class","arial_12_bold");
			tblSummary.Rows.Add(head0);
			tblSummary.Rows.Add(head1);
			//tblSummary.Rows.Add(head2);
            //SalesTurnin_Report Changes.Ch1 :START - Added code not to add the header row for revenue Type in Excel.
            if (sales_excel != "PAYMENT TYPE")
            {
                tblSummary.Rows.Add(head2);
            }
            //SalesTurnin_Report Changes.Ch1 :END - Added code not to add the header row for revenue Type in Excel.
		}

		//Header for Total Summary
		public void CreateTotalHeader(ref System.Web.UI.HtmlControls.HtmlTable tblSummary, ref DataTable dtbTemp )
		{
			HtmlTableRow header = new HtmlTableRow();
            //CHG0083477 - Name Change from AAA NCNU IE to CSAA IG as part of Name change project on 11/15/2013.
            //NameChange.Ch1:changed CSAA to AAA NCNU IE as a part of namechange project on 8/19/2010.
            //CHG0104053 - PAS HO CH1 - START : Added the below code to change the label from TOTAL CSAA IG PRODUCTS AND ALL OTHER PRODUCTS to Total Sales Rep Turn In 6/13/2014
			AddColumn(ref header,"TOTAL SALES REP TURN IN",dtbTemp.Columns.Count,1,"arial_11_bold_white","0","left");
            //CHG0104053 - PAS HO CH1 - END : Added the below code to change the label from TOTAL CSAA IG PRODUCTS AND ALL OTHER PRODUCTS to Total Sales Rep Turn In 6/13/2014
			header.BgColor = "#333333";
			//header.Attributes.Add("class","arial_11_bold_white");
			tblSummary.Rows.Add(header);
		}

		public void AddCollTotalSummaryData(ref System.Web.UI.HtmlControls.HtmlTable tblSummary,ref DataTable dtbTemp)
		{
			//12/16/2005 Q4-Retrofit.Ch1: Added the code to create the heading Payment Type and SubTotal
			HtmlTableRow hRow = new HtmlTableRow();
			AddColumn(ref hRow,"PAYMENT TYPE",1,1,"item_template_header_white","0","center");
			AddColumn(ref hRow,"SUBTOTAL",1,1,"item_template_header_white","0","center");
			tblSummary.Rows.Add(hRow);
			//12/16/2005 Q4-Retrofit.Ch1: End
			foreach(DataRow dr in dtbTemp.Rows)
			{
				hRow = new HtmlTableRow();
				decimal dcTot = 0;
				foreach(DataColumn dtCol in dtbTemp.Columns)
				{
					if (dtCol.ColumnName != "PayType")
					{
						dcTot = dcTot + Convert.ToDecimal( dr[dtCol.ColumnName.ToString()].ToString());
						AddColumn(ref hRow, Convert.ToDecimal(dr[dtCol.ColumnName.ToString()].ToString()).ToString("$##0.00"), 1,1,"item_template_header_white_nobold","0","right");
					}
					else
					{
						AddColumn(ref hRow, "Total " + dr[dtCol.ColumnName.ToString()].ToString(), 1,1,"item_template_header_white","0","left");
					}
				}
				//				AddColumn(ref hRow, Convert.ToDecimal(dcTot.ToString()).ToString("$##0.00"), 1,"collection_summary_footer");
				tblSummary.Rows.Add(hRow);
			}
		}

		public double AddCollTotalSummaryFooter(ref System.Web.UI.HtmlControls.HtmlTable tblSummary, ref DataTable dtbTemp)
		{
			HtmlTableRow hRow = new HtmlTableRow();
			decimal dcGrandTot = 0;
			foreach(DataColumn dtCol in dtbTemp.Columns)
			{
				string strColName = dtCol.ColumnName.ToString();
				decimal dcTot = 0;
				if (strColName != "PayType")
				{
					foreach(DataRow dr in dtbTemp.Rows)				
					{
						dcTot = dcTot + Convert.ToDecimal( dr[strColName].ToString());
					}
					AddColumn(ref hRow, Convert.ToDecimal(dcTot.ToString()).ToString("$##0.00"), 1,1,"","","right");
					dcGrandTot = dcGrandTot + dcTot;
				}
				else
				{
					AddColumn(ref hRow, "GRAND TOTAL",1,1,"","","left");
				}
			}
			//			AddColumn(ref hRow, Convert.ToDecimal(dcGrandTot.ToString()).ToString("$##0.00"), 1);
			hRow.Attributes.Add("class","collection_summary_footer");
			tblSummary.Rows.Add(hRow);
			
			if (dcGrandTot <= 0)
			{
				tblSummary.Visible=false;
			}
			else
			{
				tblSummary.Visible=true;
			}

			return  Convert.ToDouble(dcGrandTot) ;
		}

		//Add the data to the HTML table
		public void AddCollSummaryData(ref System.Web.UI.HtmlControls.HtmlTable tblSummary,ref DataTable dtbTemp)
		{
			int intColCount = dtbTemp.Columns.Count - 1;
			// 22% width for payment type column, 15 % width for Total Column
			// 65% is shared by all other data columns
			if (intColCount !=0)
			{
				//For Print Version, Width is assigned in the page itself.
				//For all other pages, Width is explicitly made to "0" in the page load.
				int TableWidth=Convert.ToInt32(tblSummary.Attributes["Width"].ToString());

				int FixedWidth = 0;

				if  (tblSummary.Attributes["FixedWidth"] == null)
				{
					FixedWidth = ((intColCount*91)+225);
				}
				else
				{
					FixedWidth=Convert.ToInt32(tblSummary.Attributes["FixedWidth"].ToString());
				}

				if(TableWidth ==0)
				{
					tblSummary.Attributes.Add("Width", Convert.ToString((intColCount*91)+225));
				}
				else if (((intColCount*91)+225) < FixedWidth )
				{
					tblSummary.Attributes.Add("Width", Convert.ToString((intColCount*91)+225));
				}
				else
				{
					tblSummary.Attributes.Add("Width",FixedWidth.ToString());
				}

				intColCount = Convert.ToInt32(63 / intColCount);
				if (intColCount > 16) intColCount = 16;
			}
			string strWidth = intColCount.ToString() + "%";

			foreach(DataRow dr in dtbTemp.Rows)
			{
				HtmlTableRow hRow = new HtmlTableRow();
				decimal dcTot = 0;
				foreach(DataColumn dtCol in dtbTemp.Columns)
				{
					if (dtCol.ColumnName != "PayType")
					{
						dcTot = dcTot + Convert.ToDecimal( dr[dtCol.ColumnName.ToString()].ToString());
						AddColumn(ref hRow, Convert.ToDecimal(dr[dtCol.ColumnName.ToString()].ToString()).ToString("$##0.00") , 1,1,"item_template_header_white_nobold",strWidth,"right");
					}
					else
					{
						AddColumn(ref hRow, "Total " + dr[dtCol.ColumnName.ToString()].ToString(), 1,1,"item_template_header_white","135","left");
					}
				}
				AddColumn(ref hRow, Convert.ToDecimal(dcTot.ToString()).ToString("$##0.00"), 1 ,1,"collection_summary_footer","","right");
				tblSummary.Rows.Add(hRow);
				
			}
		}

		//Adding the HTML footer

		public double AddCollSummaryFooter(ref System.Web.UI.HtmlControls.HtmlTable tblSummary, ref DataTable dtbTemp)
		{
			HtmlTableRow hRow = new HtmlTableRow();
			decimal dcGrandTot = 0;
			foreach(DataColumn dtCol in dtbTemp.Columns)
			{
				string strColName = dtCol.ColumnName.ToString();
				decimal dcTot = 0;
				if (strColName != "PayType")
				{
					foreach(DataRow dr in dtbTemp.Rows)				
					{
						dcTot = dcTot + Convert.ToDecimal( dr[strColName].ToString());
					}
					AddColumn(ref hRow, Convert.ToDecimal(dcTot.ToString()).ToString("$##0.00"), 1,1,"","","right");
					dcGrandTot = dcGrandTot + dcTot;
				}
				else
				{
					AddColumn(ref hRow, "GRAND TOTAL",1,1,"","","left");
				}
			}
			AddColumn(ref hRow, Convert.ToDecimal(dcGrandTot.ToString()).ToString("$##0.00"), 1,1,"","","right");
			hRow.Attributes.Add("class","collection_summary_footer");
			tblSummary.Rows.Add(hRow);
			
			if (dcGrandTot <= 0)
			{
				tblSummary.Visible=false;
			}
			else
			{
				tblSummary.Visible=true;
			}
			return  Convert.ToDouble(dcGrandTot);
		}
	}
}
