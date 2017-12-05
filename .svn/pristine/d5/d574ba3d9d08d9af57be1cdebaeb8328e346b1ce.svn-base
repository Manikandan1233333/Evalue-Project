/*	REVISION HISTORY:
 *	MODIFIED BY COGNIZANT
 *	07/19/2005 - For changing the web.config entries pointing to database. This changes 
 *				 are made as part of CSR #3937 implementation.
 *	Changes Done:
 *	CSR#3937.Ch1 : Include the namespace for accessing the StringDictionary object	
 *	CSR#3937.Ch2 : Changed the code for getting constant value from Application object (web.config changes)
 *  CSR#3937.Ch3 : Changed the code for getting constant value from Application object (web.config changes)
 *	MODIFIED BY COGNIZANT AS PART OF Q4 RETROFIT CHANGES
 * 
 * 12/1/2005 Q4-Retrofit.Ch1: Modified the code to change the Revenue Type for Huon Products as "Payments"
 * 12/1/2005 Q4-Retrofit.Ch2: Added code to Append a Sigle Quote (') to the Policy number
 * 12/8/2005 Q4-Retrofit.Ch3 : Changed the code for getting constant value from Application object (web.config changes)
 *  
 * STAR Retrofit III Changes:
 * 04/04/2007 STAR Retrofit III.Ch1: 
 *			Reduced the colspan for footer of membership report's repeater
 * 04/04/2007 STAR Retrofit III.Ch2:
 *			Modified the code to invisible the caption for cashier reconciliation summary for approved status
 *			
 * 08/18/2010 Tech upgrade Ch1:
 *        Replaced the datagrid to grid view and commented the ColourCrossReferenceReport function and made alternative colouring in the grid view properties itself.
 * MODIFIED BY COGNIZANT AS A PART OF NAME CHANGE PROJECT ON 8/20/2010
 * NameChange.ch1:Modified the header CSAA PRODUCTS to AAA NCNU IE PRODUCTS as a part of name change project.
 * MODIFIED AS A PART OF TIME ZONE CHANGE ENHANCEMENT ON 8-31-2010
 * TimeZoneChange.Ch1-Commented to display only date and not time as a part of Time Zone Change  by cognizant.
 * //Security Defect - CH1 - Added the below code to verify if the session values is getting null
 * //CHG0083477 - Name Change from AAA NCNU IE to CSAA IG as part of Name change project on 11/15/2013.
 * CHG0104053 - PAS HO CH1 - Added the below code to get the Policy number details for validation 7/3/2014
 * CHG0104053 - PAS HO CH2-  Modified the below code to group under PAS Auto, PAS Home 7/02/2014
 * CHG0104053 - PAS HO CH3 - Modified the code to display PAS Home
 * CHG0104053 - PAS HO CH4 - Added the below code to change the label from HomeOwner to Home/HDES 7/3/2014
 * CHG0104053 - PAS HO CH5 - Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/16/2014
 * CHG0104053 - PAS HO CH6 - Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/16/2014
 *   MAIG - CH1 - Modified the below code to support Common Product Type of Home, Auto
 *   MAIG - CH2 - Added code to Sort PayType to get PAS Auto,PAS Home,Home/HDES, WU/ACA, Homeowners and Membership Products in order.
 *   MAIG - CH3 - Commeneted the un-used Product Types
 *   CHG0109406 - CH1 - Modified the colspan value from 9 to 5 to accomodate the TimeZone field
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

//CSR#3937.Ch1 : START - Include the namespace for accessing the StringDictionary object
using System.Collections.Specialized;
using System.Collections.Generic;
using System.Linq;
//CSR#3937.Ch1 : END - Include the namespace for accessing the StringDictionary object
namespace MSCR.Reports
{
	/// <summary>
	/// Summary description for CashierRecon_Printxls.
	/// </summary>
	public partial class CashierRecon_Printxls : System.Web.UI.Page
	{

		protected System.Data.DataTable dtbSummary;
		protected System.Data.DataTable	dtbOtherSummary;
		protected System.Data.DataTable	dtbTotal;

		protected System.Data.DataTable dtbRepeater;
		protected System.Data.DataTable dtbOtherRepeater;
		protected System.Data.DataTable dtbTotalRepeater;



		protected CollectionSummaryLibrary CollectionSummary  = new CollectionSummaryLibrary();

		const string ROW_COLOR = "White";
		const string ALTERNATE_ROW_COLOR = "LightYellow";
		const string MEMBER = "MBR";
		const string MEMBERSHIP = "Membership";
		const string EARNED_PREMIUM = "EARNED PREMIUM";
		const string INSTALLMENT_PAYMENT = "Installment Payment";
		const string APPROVED = "Approved";
		string tmporderid="";

		// Variable to store the Receipt Number of the previous transaction in the Cross Reference Report datagrid
		string tmpreceiptnum = "";
		//.Modified by Cognizant Product Code changed to Upload Type based on Tier1 
		double dblTotalMember=0;

		string tmpreceipt="";
		string colour = ALTERNATE_ROW_COLOR;

		// Variable to store the colour of the current row in the Cross Reference Report datagrid
		string crosscolour = ALTERNATE_ROW_COLOR;

		protected System.Web.UI.HtmlControls.HtmlTable tblCaption;
		protected System.Web.UI.HtmlControls.HtmlTableRow Tr1;
		protected System.Web.UI.HtmlControls.HtmlTableCell tdCaption;
		protected System.Web.UI.HtmlControls.HtmlTableRow trBlankRow2;
		double dblTotal=0;
	
		protected void Page_Load(object sender, System.EventArgs e)
		{

			// Put user code to initialize the page here
			DataSet dtsData = (DataSet) Session["PendingApproval"]; 
			
			if (dtsData == null)
			{
				Response.Write("Your session has timed out. Please re-submit the page");
				tblGeneral.Visible = false;
				tblRepeater.Visible = false;
				tblCollectionSummary.Visible = false;
				tblMemberRepeater.Visible = false;
				tblCrossReference.Visible = false;
				return;
			}

			tblGeneral.Visible = true;

			if (Request.QueryString["type"] == "XLS") 
			{
				Response.ContentType = "application/vnd.ms-excel";
				Response.Charset = ""; //Remove the charset from the Content-Type header.
			}

			Page.EnableViewState = false;
		
			lblTitle.Text = "My Reports";
			lblRunDate.Text = (String) Session["RunDate"];
			lblReportStatus.Text = (String) Session["ReportStatus"];
			if (Session["DateRange"] != null)
			{
				trDateRange.Visible = true;
				lblDateRange.Text = (String) Session["DateRange"];
			}
			else
			{
				trDateRange.Visible = false;
			}

			lblUsers.Text =  (String) Session["Users"];

			lblRepDO.Text =  (String) Session["RepDO"];
			//START Added by Cognizant on 04/20/2005 to display the Approver
			if((Session["ReportStatus"].ToString() == APPROVED))
			{
                //Security Defect - CH1 - Added the below code to verify if the session values is getting null
                if (Session["ApprovedBy"] == null)
                {
                    if (Context.Items.Contains("ApprovedBy"))
                    {
                        Session["ApprovedBy"] = Context.Items["ApprovedBy"];
                    }
                }
                //Security Defect - CH1 - Added the below code to verify if the session values is getting null
				lblApprover.Text = ((String)Session["ApprovedBy"]);
			}
			else
			{
				trApprover.Visible =  false; 
			}
			//END
            //Security Defect - CH2 START - Added the below code to verify if the session values is getting null
            if (Session["ReportType"] == null)
            {
                if (Context.Items.Contains("ReportType"))
                {
                    Session["ReportType"] = Context.Items["ReportType"];
                }
            }
            //Security Defect - CH2 END - Added the below code to verify if the session values is getting null  
			if (Cache["Cashier Report Type"].ToString() == Session["ReportType"].ToString())
			{
				//STAR Retrofit III.ch2: START - Modified the code to invisible the caption for cashier reconciliation summary for approved status
				if(Session["ReportStatus"].ToString() == APPROVED)
				{
					trCaption.Visible = false;
				}
				//STAR Retrofit III.ch2: ENd
				rptCashierApproveRepeater.DataSource =  dtsData;
				rptCashierApproveRepeater.DataBind();
				PrepareCollectionSummary();
				tblRepeater.Visible = true;
				tblCollectionSummary.Visible = true;
				tblMemberRepeater.Visible = false;
				tblCrossReference.Visible = false;

			}
			else if (Cache["Member Report Type"].ToString() == Session["ReportType"].ToString())
			{
				rptMembershipDetail.DataSource = dtsData;
				rptMembershipDetail.DataBind();
				tblRepeater.Visible = false;
				tblCollectionSummary.Visible = false;
				tblMemberRepeater.Visible = true;
				tblCrossReference.Visible = false;
			}
			else if (Cache["Cross Reference Report Type"].ToString() == Session["ReportType"].ToString())
			{
				dgCrossReference.DataSource = dtsData;
				dgCrossReference.DataBind();
				tblRepeater.Visible = false;
				tblCollectionSummary.Visible = false;
				tblMemberRepeater.Visible = false;
				tblCrossReference.Visible = true;
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

		#region rptCashierApproveRepeater_ItemBound
		/// <summary>
		/// Checking when the datas are bound to the repeater
		/// </summary>
		/// <param name="Sender"></param>
		/// <param name="e"></param>
		protected void rptCashierApproveRepeater_ItemBound(Object Sender, RepeaterItemEventArgs e)
		{
			//Q4-Retrofit.Ch2-START :Added the code to Append a Sigle Quote (') to the Policy number for XLS Version
			if(Request.QueryString["type"] == "XLS")
			{
				if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
				{
					HtmlTableCell tdTempPolicyNumber = (HtmlTableCell) e.Item.FindControl("tdPolicyNumber");
					tdTempPolicyNumber.InnerHtml = "'" + tdTempPolicyNumber.InnerHtml.ToString();
				}
			}
			//Q4-Retrofit.Ch2-END 
			AlternatingColour(e);
			CalculateTotal(e);
		}

		#endregion

		#region AlternatingColour
		/// <summary>
		/// Displaying the details of the same receipt number in a single colour
		/// Making the Receipt Date,Status, Member Name, checkbox of same receipt number invisible
		/// </summary>
		private void AlternatingColour(RepeaterItemEventArgs e)
		{
			
			if(e.Item.ItemType == ListItemType.Header)
			{   //START Added by Cognizant on 04/20/2005 to assign the CSS for new header
				if((Session["ReportStatus"].ToString() == APPROVED))
				{
					HtmlTableRow trHeaderRow=(HtmlTableRow) e.Item.FindControl("trHeaderRow");
					trHeaderRow.Attributes.Add("class","arial_11_bold");
				}
				//END
			}

			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{

				HtmlTableRow trTempRow=(HtmlTableRow) e.Item.FindControl("trItemRow");
				Label lblTempReceiptDate = (Label) e.Item.FindControl("lblReceiptDate");
				lblTempReceiptDate.Text = Convert.ToDateTime(lblTempReceiptDate.Text).ToString("MM/dd/yyyy hh:mm:ss tt");
				Label lblTempStatus=(Label) e.Item.FindControl("lblStatus");
				Label lblTempMemberName=(Label) e.Item.FindControl("lblMemberName");
				Label lblTempReceiptNo = (Label) e.Item.FindControl("lblReceiptNo");
				string strTempReceiptNo = lblTempReceiptNo.Attributes["value"].ToString();
				//START Added by Cognizant on 04/20/2005 to display the Approved date and assign the style to the date
				if((Session["ReportStatus"].ToString() == APPROVED))
				{
					Label lblTempApprovedDate = (Label)e.Item.FindControl("lblApprovedDate");
					lblTempApprovedDate.Text = Convert.ToDateTime(lblTempApprovedDate.Text).ToString("MM/dd/yyyy hh:mm:ss tt");
					trTempRow.Attributes.Add("class","arial_11");
				}
				//END
				//Assigning different colour if the receipt numbers are different
				if(strTempReceiptNo!=tmpreceipt)
				{
					AssignClass(colour,trTempRow);
				}
				else
				{
					//if the Receipt Number has Multiple Items,the Receipt Date,Receipt No,Status,MemberName
					//are made visible false
					trTempRow.Attributes.Add("bgcolor",colour);
					lblTempReceiptDate.Visible=false;
					lblTempStatus.Visible=false;
					lblTempMemberName.Visible=false;
					lblTempReceiptNo.Visible = false;
				}

				tmpreceipt=strTempReceiptNo;
			}
					
			if((Session["ReportStatus"].ToString() != APPROVED))
				{
				HtmlTableCell tdTemp;
					if (e.Item.ItemType == ListItemType.Header) 
					{
						tdTemp=(HtmlTableCell) e.Item.FindControl("tdApprovedByHead");
						tdTemp.Visible = false;
     					tdTemp=(HtmlTableCell) e.Item.FindControl("tdApprovedDateHead");
						tdTemp.Visible = false;
						tdTemp=(HtmlTableCell) e.Item.FindControl("tdHeader1");
                        //CHG0109406 - CH1 - BEGIN - Modified the colspan value from 9 to 5 to accomodate the TimeZone field
						tdTemp.ColSpan=5;
                        //CHG0109406 - CH1 - END - Modified the colspan value from 9 to 5 to accomodate the TimeZone field
					}
					if (e.Item.ItemType ==ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
					{
						tdTemp=(HtmlTableCell) e.Item.FindControl("tdApprovedByItem");
						tdTemp.Visible=false;
						tdTemp=(HtmlTableCell) e.Item.FindControl("tdApprovedDateItem");
						tdTemp.Visible = false;
					}
					if (e.Item.ItemType == ListItemType.Footer)
					{	
						tdTemp=(HtmlTableCell) e.Item.FindControl("tdTotalHead");
						tdTemp.Attributes.Add("colspan","8");
						tdTemp=(HtmlTableCell) e.Item.FindControl("tdFooter");
						tdTemp.ColSpan = 9;

						
							
					}
				}
			 //END
			
		}
		#endregion

		#region CalculateTotal
		/// <summary>
		/// Calculating the total of all the items in the Repeater
		/// </summary>
		private void CalculateTotal(RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem ||e.Item.ItemType == ListItemType.Footer) 
			{
				if (e.Item.ItemType == ListItemType.Footer)
				{
					Label lblTempAmount = (Label)e.Item.FindControl("lblTotal");
					lblTempAmount.Text = dblTotal.ToString("$##0.00");
					return;
				}
				//Total all the individual amounts
				HtmlTableCell tdTempAmount = (HtmlTableCell)e.Item.FindControl("tdAmount");
				string strAmount =  tdTempAmount.InnerText;
				dblTotal = dblTotal + Convert.ToDouble(strAmount.Replace("$","").Replace(",",""));
			}
		}
		#endregion

		#region AssignClass
		/// <summary>
		/// To Change the Color for the Row
		/// </summary>
		private void AssignClass(string tmpcolour,HtmlTableRow tr)
		{
			if ( tmpcolour == ROW_COLOR )
			{
				tr.Attributes.Add("bgcolor",ALTERNATE_ROW_COLOR);
				colour = ALTERNATE_ROW_COLOR;
			}
			else if ( tmpcolour == ALTERNATE_ROW_COLOR )
			{
				tr.Attributes.Add("bgcolor",ROW_COLOR);
				colour = ROW_COLOR;
			}
		}

		#endregion

		#region PrepareSummaryData
		/// <summary>
		/// Creates Data into the Repeater Table
		/// </summary>
		private void PrepareSummaryData()
		{
			CreateSummaryTable();

			foreach(RepeaterItem item in rptCashierApproveRepeater.Items)
			{
				if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem) 
				{
					Label lblTempReceiptNo = (Label) item.FindControl("lblReceiptNo");
					HtmlTableCell tdTempAmount = (HtmlTableCell)item.FindControl("tdAmount");
					string strAmount =  tdTempAmount.InnerText;
					HtmlTableCell tdTempPymtType=(HtmlTableCell)item.FindControl("tdPymtType");
					HtmlTableCell tdTempProdType=(HtmlTableCell)item.FindControl("tdProdType");
					HtmlTableCell tdTempTransType=(HtmlTableCell)item.FindControl("tdTransType");
                    //CHG0104053 - PAS HO CH1 - START : Added the below code to get the Policy number details for validation 7/3/2014
                    HtmlTableCell tdpolicyno = (HtmlTableCell)item.FindControl("tdpolicynumber");
                    if (Request.QueryString["type"] == "XLS")
                    {
                        tdpolicyno.InnerText = tdpolicyno.InnerText.Replace("'", "").Trim();
                    }
                    int length = tdpolicyno.InnerText.Trim().Length;

                    string prodtype = tdTempProdType.InnerText.Trim();
                    //CHG0104053 - PAS HO CH1 - END : Added the below code to get the Policy number details for validation 7/3/2014
					//Q4-Retrofit.Ch3 : Changed the code for getting constant value from Application object (web.config changes)
					// Q4-Retrofit.Ch1-START: Added the code to get the Upload Type value from the config
					//string strUploadType = "," + Config.Setting("UploadType.CSAAProducts") + ",";
					string strUploadType = "," + Convert.ToString(((StringDictionary)Application["Constants"])["UploadType.CSAAProducts"]) + ",";
					if (strUploadType.IndexOf("," + tdTempProdType.Attributes["UploadType"].ToString() + ",") >= 0 )
					{
						//Added the code to display "Payments" as Revenue Type for HUON products in the 
						// CSAA Collection Summary Grid 
						string strTransType = tdTempTransType.InnerText;
						//MAIG - CH1 - BEGIN - Modified the below code to support Common Product Type of Home, Auto
                        //CHG0104053 - PAS HO CH2- BEGIN : Modified the below code to group under PAS Auto, PAS Home 7/02/2014
                        if (length == 13 && (prodtype.ToLower().Equals("auto")))
                        {
                            InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "PAS Auto", strTransType, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                        }
                        else
                        {
                            //////if ((length == 13 && prodtype.ToLower().Equals("aaa signature series home")) || (length == 7 && prodtype.ToLower().Equals("homeowners")))
                            //////{
                            //////    InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "PAS Home", strTransType, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                            //////}
                            //CHG0104053 - PAS HO CH2- END : Modified the below code to group under PAS Auto, PAS Home 7/02/2014
                            //if (tdTempProdType.Attributes["UploadType"].ToString() == Config.Setting("UploadType.HUON"))
                            /*if (tdTempProdType.Attributes["UploadType"].ToString() == Convert.ToString(((StringDictionary)Application["Constants"])["UploadType.HUON"]))
                            {
                                //Getting the Revenue Type for HUON Products from the config
                                //strTransType = 	Config.Setting("RevenueTypeDescription.HUON") ;
                                strTransType = Convert.ToString(((StringDictionary)Application["Constants"])["RevenueTypeDescription.HUON"]);
                            }*/

                            if (length == 13 && prodtype.ToLower().Equals("home"))
                            {
                                InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "PAS Home", strTransType, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));

                                //CHG0104053 - PAS HO CH3- BEGIN :-Modified the code to display PAS Home
                                //////if (length == 7 && prodtype.ToLower().Equals("homeowners"))
                                //////    InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, tdTempProdType.InnerText, strTransType, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                            }
                            else if (length == 7 && prodtype.ToLower().Equals("home"))
                            {
                                InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "Home/HDES", strTransType, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                            }
                            else
                            {
                                //Modified the code to pass strTransType as Parameter instead of tdTempTransType.InnerText
                                //MAIG  - Modified the below line ref type paramter name from dtbSummary to dtbOtherSummary to display in WU/ACA Products Collection 11/25/2014
                                InsertSummaryTable(ref dtbOtherSummary, tdTempPymtType.InnerText, Convert.ToString(((StringDictionary)Application["Constants"])["ProductTypeHeader.WU_ACA"]), strTransType, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                                //Q4-Retrofit.Ch1-END
                            }
                            ////////Modified the code to pass strTransType as parameter instead of tdTempTransType.InnerText
                            //////InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, tdTempProdType.InnerText, strTransType, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                            //CHG0104053 - PAS HO CH3 - END :-Modified the code to display PAS Home
                        }
					}
					else 
					{
                        if (length == 13 && (prodtype.ToLower().Equals("dwelling fire") || prodtype.ToLower().Equals("personal umbrella")))
                        {
                            InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "PAS Home", tdTempTransType.InnerText, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                        }
                        else
                        {
                            //For Other Products Collection Summary
                            //InsertSummaryTable(ref dtbOtherSummary,tdTempPymtType.InnerText,Config.Setting("ProductTypeHeader.WU_ACA").ToString(),tdTempTransType.InnerText,Convert.ToDecimal(strAmount.Replace("$","").Replace(",","")));
                            InsertSummaryTable(ref dtbOtherSummary, tdTempPymtType.InnerText, Convert.ToString(((StringDictionary)Application["Constants"])["ProductTypeHeader.WU_ACA"]), tdTempTransType.InnerText, Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                        }
					//MAIG - CH1 - END - Modified the below code to support Common Product Type of Home, Auto
					}
					//To Insert values into the Total Table
					InsertTotalTable(ref dtbTotal,tdTempPymtType.InnerText,Convert.ToDecimal(strAmount.Replace("$","").Replace(",","")));
				}
			}
			//Q4-Retrofit.Ch1-End
			//Q4-Retrofit.Ch3 : END - Changed the code for getting constant value from Application object (web.config changes)
			dtbSummary.DefaultView.Sort =  "PayType,ProdType,RevType";
			dtbOtherSummary.DefaultView.Sort = "PayType,ProdType,RevType";
			dtbTotal.DefaultView.Sort = "PayType";
            //MAIG - CH2 - BEGIN - Added code to Sort PayType to get PAS Auto,PAS Home,Home/HDES, WU/ACA, Homeowners and Membership Products in order.
            DataTable mdtbsummary = dtbSummary.Clone();
            List<DataRow> lstpas = dtbSummary.Select("ProdType = 'PAS Auto'").ToList();
            List<DataRow> lstpasHome = dtbSummary.Select("ProdType = 'PAS Home'").ToList();
            List<DataRow> lsthome = dtbSummary.Select("ProdType = 'Home/HDES'").ToList();
            List<DataRow> lstWU_ACA = dtbSummary.Select("ProdType = 'WU/ACA'").ToList();
            if (lstpas.Count > 0)
            {
                foreach (DataRow item in lstpas)
                {
                    mdtbsummary.Rows.Add(item.ItemArray);
                }
            }
            if (lstpasHome.Count > 0)
            {
                foreach (DataRow item in lstpasHome)
                {
                    mdtbsummary.Rows.Add(item.ItemArray);
                }
            }
            if (lsthome.Count > 0)
            {
                foreach (DataRow item in lsthome)
                {
                    mdtbsummary.Rows.Add(item.ItemArray);
                }
            }
            if (lstWU_ACA.Count > 0)
            {
                foreach (DataRow item in lstWU_ACA)
                {
                    mdtbsummary.Rows.Add(item.ItemArray);
                }
            }
            dtbSummary = mdtbsummary.Copy();
			CreateRepeaterTable();
			DataView rowView = new DataView(dtbSummary,"","",DataViewRowState.CurrentRows);
			DataView colListView = new DataView(dtbSummary,"","",DataViewRowState.CurrentRows);
            //MAIG - CH2 - END - Added code to Sort PayType to get PAS Auto,PAS Home,Home/HDES, WU/ACA, Homeowners and Membership Products in order.
			DataView rowViewOther = new DataView(dtbOtherSummary,"","PayType,ProdType,RevType",DataViewRowState.CurrentRows);
			DataView colListViewOther = new DataView(dtbOtherSummary,"","ProdType,RevType",DataViewRowState.CurrentRows);
			DataView rowViewTotal = new DataView(dtbTotal,"","PayType",DataViewRowState.CurrentRows);
			DataView colListViewTotal = new DataView(dtbTotal,"","",DataViewRowState.CurrentRows);
			// Creating header for the repeater in the format ProdType|RevType 
			foreach(DataRowView dr in colListView)
			{
				AddColumnToRepeaterTable(dr["ProdType"].ToString() + "|" + dr["RevType"].ToString(),ref dtbRepeater);
			}
			// Creating header for the repeater in the format ProdType|RevType for other products
			foreach(DataRowView dr in colListViewOther)
			{
				AddColumnToRepeaterTable(dr["ProdType"].ToString() + "|" + dr["RevType"].ToString(),ref dtbOtherRepeater );
			}
			//Inserting data in the Repeater table from the summary table
			foreach(DataRowView dr in rowView)
			{
				InsertRepeaterTable( ref dtbRepeater, dr["PayType"].ToString(), dr["ProdType"].ToString(), dr["RevType"].ToString(), Convert.ToDecimal(dr["Amount"].ToString()));
			}
			//Inserting data in the Repeater table from the other product summary table
			foreach(DataRowView dr in rowViewOther)
			{
				InsertRepeaterTable( ref dtbOtherRepeater , dr["PayType"].ToString(), dr["ProdType"].ToString(), dr["RevType"].ToString(), Convert.ToDecimal(dr["Amount"].ToString()));
			}
			AddColumnToRepeaterTable("Amount",ref dtbTotalRepeater );
			//Inserting data in the Repeater table Total
			foreach(DataRowView dr in rowViewTotal)
			{
				InsertRepeaterTotalTable(ref dtbTotalRepeater , dr["PayType"].ToString(),Convert.ToDecimal(dr["Amount"].ToString()));
			}
		}

		#endregion

		#region CreateSummaryTable

		/// <summary>
		/// Temporary Data Table (Summary Table) is created having the columns PayType,ProdType,RevType and Amount 
		/// with PayType,ProdType,RevType as combinde primary key
		/// </summary>
		private void CreateSummaryTable()
		{
			dtbSummary = new DataTable();
			dtbSummary.Columns.Add("PayType");
			dtbSummary.Columns.Add("ProdType");
			dtbSummary.Columns.Add("RevType");
			dtbSummary.Columns.Add("Amount",System.Type.GetType("System.Decimal"));
			dtbSummary.PrimaryKey = new DataColumn[]{ dtbSummary.Columns["PayType"],dtbSummary.Columns["ProdType"],dtbSummary.Columns["RevType"] };

			//Created for Storing the Other Products Summary Table
			dtbOtherSummary = new DataTable();
			dtbOtherSummary.Columns.Add("PayType");
			dtbOtherSummary.Columns.Add("ProdType");
			dtbOtherSummary.Columns.Add("RevType");
			dtbOtherSummary.Columns.Add("Amount",System.Type.GetType("System.Decimal"));
			dtbOtherSummary.PrimaryKey = new DataColumn[]{ dtbOtherSummary.Columns["PayType"],dtbOtherSummary.Columns["ProdType"],dtbOtherSummary.Columns["RevType"] };

			//Create for Storing the Total Summary Table
			dtbTotal = new DataTable();
			dtbTotal.Columns.Add("PayType");
			dtbTotal.Columns.Add("Amount",System.Type.GetType("System.Decimal"));
			dtbTotal.PrimaryKey = new DataColumn[]{dtbTotal.Columns["PayType"]};

		}

		#endregion

		#region CreateRepeaterTable

		/// <summary>
		/// Temporary Data Table (Repeater Table) is created with the column PayType
		/// </summary>
		private void CreateRepeaterTable()
		{
			dtbRepeater = new DataTable();
			dtbRepeater.Columns.Add("PayType");
			//create Data table to have other product info
			dtbOtherRepeater = new DataTable();
			dtbOtherRepeater.Columns.Add("PayType");
			//Create Data table to have Total Amount
			dtbTotalRepeater = new DataTable();
			dtbTotalRepeater.Columns.Add("PayType");
		}

		#endregion

		#region AddColumnToRepeaterTable

		/// <summary>
		/// Adding a column to the repeater table
		/// </summary>
		private void AddColumnToRepeaterTable(string strColName,ref DataTable dtbReferer)
		{
			if ( !dtbReferer.Columns.Contains(strColName) )
			{
				DataColumn dtCol = new DataColumn(strColName,System.Type.GetType("System.Decimal"));
				dtCol.DefaultValue = Convert.ToDecimal(0);
				dtbReferer.Columns.Add(dtCol);
			}
		}

		#endregion

		#region InsertRepeaterTable

		/// <summary>
		/// Inserting values to the repeater table from the summary table
		/// </summary>
		private void InsertRepeaterTable(ref DataTable dtbReferer,string inPaytype,string inProdtype,string inRevtype,decimal inAmount)
		{
			string strExpression = string.Empty;
			DataRow dtrRepeater;
			DataRow[] dtrArrar;
			strExpression = strExpression + "PayType = '" + inPaytype + "'";
			dtrArrar = dtbReferer.Select(strExpression);
			//Inserting the payment type as a new row in case it is not already inserted
			if ( dtrArrar.GetLength(0) == 0 )
			{
				dtrRepeater = dtbReferer.NewRow();
				dtrRepeater["PayType"] = inPaytype;
				dtrRepeater[inProdtype + "|" + inRevtype] = inAmount;
				dtbReferer.Rows.Add(dtrRepeater);
			}
				//Placing the amount in case the column is already present
			else
			{
				dtrArrar[0][inProdtype + "|" + inRevtype] = Convert.ToDecimal(inAmount);
			}
		}		

		#endregion

		#region InsertRepeaterTotalTable
		private void InsertRepeaterTotalTable(ref DataTable dtbReferer,string inPaytype,decimal inAmount)
		{
			string strExpression = string.Empty;
			DataRow dtrRepeater;
			DataRow[] dtrArrar;
			strExpression = strExpression + "PayType = '" + inPaytype + "'";
			dtrArrar = dtbReferer.Select(strExpression);
			//Inserting the payment type as a new row in case it is not already inserted
			if ( dtrArrar.GetLength(0) == 0 )
			{
				dtrRepeater = dtbReferer.NewRow();
				dtrRepeater["PayType"] = inPaytype;
				dtrRepeater["Amount"] = inAmount;
				dtbReferer.Rows.Add(dtrRepeater);
			}
				//Placing the amount in case the column is already present
			else
			{
				dtrArrar[0]["Amount"]  = Convert.ToDecimal(inAmount);
			}
		}
		#endregion

		#region InsertSummaryTable

		/// <summary>
		/// Inserting values into the Summary table
		/// </summary>
		private void InsertSummaryTable(ref DataTable dtbTemp, string inPaytype,string inProdtype,string inRevtype,decimal inAmount)
		{
			DataRow dtrSummary;
			DataRow[] dtrArrar;
			inPaytype = inPaytype.Trim(); 
			inProdtype  = inProdtype.Trim();

			//MAIG - CH3 - BEGIN - Commeneted the un-used Product Types
            //PAS HO CH45 - START : Added the below code to change the label from HomeOwner to Home/HDES 7/3/2014
            //if (inProdtype.ToLower().Equals("homeowners"))
            //{
            //    inProdtype = "Home/HDES";
            //}
            //PAS HO CH45 - END : Added the below code to change the label from HomeOwner to Home/HDES 7/3/2014
			inRevtype=inRevtype.Trim();
			//For Revenue Type 'Earned Premium' it is changed to 'Installment Payment'
			if(inRevtype.ToUpper() == EARNED_PREMIUM)
			{
				inRevtype= INSTALLMENT_PAYMENT;
			}
            /*
			if(inProdtype.ToUpper() == MEMBER)
			{
				inProdtype=MEMBERSHIP;
			}*/
            //MAIG - CH3 - END - Commeneted the un-used Product Types
			string strExpression = string.Empty;
			strExpression = strExpression + "PayType = '" + inPaytype + "'" + " and ProdType = '" + inProdtype + "'" + " and RevType = '" + inRevtype + "'";
			dtrArrar = dtbTemp.Select(strExpression);
			// If there is no row having the same PayType, ProdType, Revenue Type, add a new row
			if ( dtrArrar.GetLength(0) == 0 )
			{
				dtrSummary = dtbTemp.NewRow();
				dtrSummary["PayType"] = inPaytype;
				dtrSummary["ProdType"] = inProdtype;
				dtrSummary["RevType"] = inRevtype;
				dtrSummary["Amount"] = inAmount;
				dtbTemp.Rows.Add(dtrSummary);
			}
				// If there is a row having the same PayType, ProdType, Revenue Type update the amount alone
			else
			{
				dtrArrar[0]["Amount"] = Convert.ToDecimal(dtrArrar[0]["Amount"].ToString()) + Convert.ToDecimal(inAmount);
			}
		}
		#endregion

		#region InsertTotalTable

		/// <summary>
		/// Inserting values into the Total Table
		/// </summary>
		private void InsertTotalTable(ref DataTable dtbTemp, string inPaytype,decimal inAmount)
		{
			DataRow dtrSummary;
			DataRow[] dtrArrar;
			inPaytype = inPaytype.Trim();
			string strExpression = string.Empty;
			strExpression = strExpression + "PayType = '" + inPaytype + "'";
			dtrArrar = dtbTemp.Select(strExpression);
			if ( dtrArrar.GetLength(0) == 0 )
			{
				dtrSummary = dtbTemp.NewRow();
				dtrSummary["PayType"] = inPaytype;
				dtrSummary["Amount"] = inAmount;
				dtbTemp.Rows.Add(dtrSummary);
			}
			else
			{
				dtrArrar[0]["Amount"] = Convert.ToDecimal(dtrArrar[0]["Amount"].ToString()) + Convert.ToDecimal(inAmount);
			}
		}
		#endregion

		#region PrepareCollectionSummary

		private void PrepareCollectionSummary()
		{
			PrepareSummaryData();
            //CHG0083477 - Name Change from AAA NCNU IE to CSAA IG as part of Name change project on 11/15/2013.
            //NameChange.ch1:Modified the header CSAA PRODUCTS to AAA NCNU IE PRODUCTS as a part of name change project on 08/20/2010.
            //CHG0104053 - PAS HO CH5 - START : Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/16/2014
			CollectionSummary.CreateHeader(ref tblCollSummary, ref dtbRepeater,"TURN-IN PRODUCTS");
            //CHG0104053 - PAS HO CH5 - END : Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/16/2014
			CollectionSummary.AddCollSummaryData(ref tblCollSummary, ref dtbRepeater);
			double dblTotal = CollectionSummary.AddCollSummaryFooter(ref tblCollSummary, ref dtbRepeater);
			//To Check the Total column Value > 0 
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
            //CHG0104053 - PAS HO CH6 - START : Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/16/2014
            CollectionSummary.CreateHeader(ref tblOtherCollSummary, ref dtbOtherRepeater, "WU/ACA PRODUCTS");
            //CHG0104053 - PAS HO CH6 - END : Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/16/2014
			CollectionSummary.AddCollSummaryData(ref tblOtherCollSummary, ref dtbOtherRepeater);
			dblTotal = CollectionSummary.AddCollSummaryFooter(ref tblOtherCollSummary, ref dtbOtherRepeater);
			//To Check the Sub Total column Value > 0 
			if (dblTotal > 0)
			{
				trOtherCollSummarySplit.Visible = true;					
				trOtherCollSummary.Visible = true;
			}
			else
			{
				trOtherCollSummarySplit.Visible = false;
				trOtherCollSummary.Visible = false;
			}
			CollectionSummary.CreateTotalHeader(ref tblTotalCollSummary,ref dtbTotalRepeater);
			CollectionSummary.AddCollTotalSummaryData(ref tblTotalCollSummary ,ref dtbTotalRepeater);
			dblTotal = CollectionSummary.AddCollTotalSummaryFooter(ref tblTotalCollSummary,ref dtbTotalRepeater); 
		}

		#endregion

		#region rptMembershipDetail_ItemBound
		/// <summary>
		/// Display of the items in the Membership Detail Data Repeater
		/// </summary>
		protected void rptMembershipDetail_ItemBound(Object Sender, RepeaterItemEventArgs e)
		{
			AlternatingColourMembership(e);
			CalculateTotalMembership(e);
		}
		#endregion

		#region AlternatingColourMembership
		/// <summary>
		/// Displaying the details of the same order ID in a single colour
		/// Making the Address,City,State,Zip,Day Phone,Eve Phone,Mbr Number,User(s),Src Code,Base Year,
		/// Prod Type,Effective Date,Amount, PymtType of same order ID invisible
		/// </summary>
		private void AlternatingColourMembership(RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				HtmlTableRow trTempItemRowMember=(HtmlTableRow) e.Item.FindControl("trItemRowMember");
				Label lblTempEffectiveDate = (Label) e.Item.FindControl("lblEffectiveDate");
                //TimeZoneChange.Ch1-Commented to display only date and not time as a part of Time Zone Change  by cognizant.
				//lblTempEffectiveDate.Text=Convert.ToDateTime(lblTempEffectiveDate.Text.ToString()).ToString ("MM/dd/yyyy hh:mm:ss tt");
				Label lblTempOrderID=(Label) e.Item.FindControl("lblOrderID");
				Label lblTempAddress=(Label) e.Item.FindControl("lblAddress");
				Label lblTempCity = (Label) e.Item.FindControl("lblCity");
				Label lblTempState = (Label) e.Item.FindControl("lblState");
				Label lblTempZip = (Label) e.Item.FindControl("lblZip");
				Label lblTempDayPhone = (Label) e.Item.FindControl("lblDayPhone");
				Label lblTempEvePhone = (Label) e.Item.FindControl("lblEvePhone");
				Label lblTempMemberNumber = (Label) e.Item.FindControl("lblMemberNumber");
				Label lblTempMemberUsers = (Label) e.Item.FindControl("lblMemberUsers");
				Label lblTempSrcCode = (Label) e.Item.FindControl("lblSrcCode");
				Label lblTempBaseYear = (Label) e.Item.FindControl("lblBaseYear");
				Label lblTempProdType = (Label) e.Item.FindControl("lblProdType");
				Label lblTempAmount = (Label) e.Item.FindControl("lblAmount");
				Label lblTempPymtType = (Label) e.Item.FindControl("lblPymtType");
				string strTempOrderID = lblTempOrderID.Attributes["value"].ToString();
				Label lblTempApprovedDate = (Label) e.Item.FindControl("lblMbrApprovedDate");
				if((lblTempApprovedDate.Text!="")&&((lblTempApprovedDate.Text!=null)))
					lblTempApprovedDate.Text=Convert.ToDateTime(lblTempApprovedDate.Text.ToString()).ToString ("MM/dd/yyyy hh:mm:ss tt");
				
				//Assigning different colour if the Order IDs are different
				if(strTempOrderID!=tmporderid)
				{
					AssignClass(colour,trTempItemRowMember);
				}
				else
				{
					trTempItemRowMember.Attributes.Add("bgcolor",colour);
					lblTempEffectiveDate.Visible=false;
					lblTempAddress.Visible=false;
					lblTempCity.Visible=false;
					lblTempState.Visible=false;
					lblTempZip.Visible=false;
					lblTempDayPhone.Visible=false;
					lblTempEvePhone.Visible=false;
					lblTempMemberNumber.Visible=false;
					lblTempMemberUsers.Visible=false;
					lblTempSrcCode.Visible=false;
					lblTempBaseYear.Visible=false;
					lblTempProdType.Visible=false;
					lblTempAmount.Visible=false;
					lblTempAmount.Text = "0";
					lblTempPymtType.Visible=false;
				}
				tmporderid=strTempOrderID;
			}
			//START Modified by Cognizant on 04/21/2005 to Show the ApprovedBy and ApprovedDate Columns depending upon the Session value
			if ((Session["ReportStatus"].ToString() != APPROVED))
			{
				HtmlTableCell tdTemp;
				
				if (e.Item.ItemType == ListItemType.Header) 
				{
					tdTemp=(HtmlTableCell) e.Item.FindControl("tdMbrApprovedByHead");
					tdTemp.Visible = false;
					tdTemp=(HtmlTableCell) e.Item.FindControl("tdMbrApprovedDateHead");
					tdTemp.Visible = false;
					tdTemp=(HtmlTableCell) e.Item.FindControl("Td1");
					tdTemp.ColSpan=20;
				}
				if (e.Item.ItemType ==ListItemType.Item || e.Item.ItemType==ListItemType.AlternatingItem)
				{
					tdTemp=(HtmlTableCell) e.Item.FindControl("tdMbrApprovedByItem");
					tdTemp.Visible=false;
					tdTemp=(HtmlTableCell) e.Item.FindControl("tdMbrApprovedDateItem");
					tdTemp.Visible = false;
				}
				//STAR Retrofit III.Ch1: START - Reduced colspan for footer
				if(e.Item.ItemType == ListItemType.Footer)
				{
					tdTemp = (HtmlTableCell) e.Item.FindControl("tdTotalFooter");
					tdTemp.ColSpan = 20;
				}
				//STAR Retrofit III.Ch1: END
			}
			//END
		}
		
		#endregion

		#region CalculateTotalMembership
		/// <summary>
		/// Calculating the total amount of all the items in the Membership Repeater
		/// </summary>
		private void CalculateTotalMembership(RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem ||e.Item.ItemType == ListItemType.Footer) 
			{
				if (e.Item.ItemType == ListItemType.Footer)
				{
					Label lblTempTotalMember = (Label)e.Item.FindControl("lblTotalMember");
					lblTempTotalMember.Text = dblTotalMember.ToString("$##0.00");
					//Changing the width of the repeater if status is Approved / Pending Approval
					if ((Session["ReportStatus"].ToString() != APPROVED))
					{
						HtmlTableCell tctotal=(HtmlTableCell) e.Item.FindControl("tdTotalMemberHead");
						tctotal.Attributes.Add("colspan","18");
						tctotal.Attributes.Add("InnerText","Total"); 
					}
					return;
				}
				//Total all the individual amounts
				Label lblTempAmount = (Label)e.Item.FindControl("lblAmount");
				lblTempAmount.Text=lblTempAmount.Text.Replace(",",""); 
				string strAmountMember =  lblTempAmount.Text;
				dblTotalMember = dblTotalMember + Convert.ToDouble(strAmountMember.Replace("$",""));
			}
		}
		#endregion

		#region SetCrossReferenceGridWidth
		/// <summary>
		/// Setting the width of the columns in the Cross Reference Data Grid
		/// </summary>
        protected void SetCrossReferenceGridWidth(GridViewRowEventArgs e)
		{
            //.Net Mig 3.5:Ch1 Replaced the property of data grid width setting to Grid view width setting as a part of tech upgrade by cognizant started on 08/18/2010.
            foreach (TableRow row in dgCrossReference.Rows)
            {

                row.Cells[0].Width = new Unit(110, UnitType.Pixel);
                row.Cells[1].Width = new Unit(120, UnitType.Pixel);
                row.Cells[2].Width = new Unit(110, UnitType.Pixel);
                row.Cells[3].Width = new Unit(134, UnitType.Pixel);
                row.Cells[4].Width = new Unit(110, UnitType.Pixel);
                row.Cells[5].Width = new Unit(30, UnitType.Pixel);

            }
        
            //e.Item.Cells[0].Width = 30;
            //e.Item.Cells[1].Width=100;
            //e.Item.Cells[2].Width=110;
            //e.Item.Cells[3].Width=120;
            //e.Item.Cells[4].Width=110;
            //e.Item.Cells[5].Width=134;
			//e.Item.Cells[6].Width=30;
        // Ch1 drid view width change property ends here.
		}
		#endregion

		#region dgCrossReference_ItemBound
		/// <summary>
		/// Display of the items in the Cross Reference Data Grid
		/// </summary>
        /// 

        protected void dgCrossReference_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            SetCrossReferenceGridWidth(e);
        }
        //Ch1 Replaced the property of data grid to grid view as a part of tech upgrade by Cognizant Started on 08/18/2010

        //protected void dgCrossReference_ItemBound(Object Sender, DataGridItemEventArgs e)
        //{
        //    //ColourCrossReferenceReport(e);
        //    SetCrossReferenceGridWidth(e);
        //}
        // Chi Data grid property ends here
		#endregion

		#region ColourCrossReferenceReport
		/// <summary>
		/// Displaying the details of the Transaction in a single colour
		/// Making the Receipt Date/Time, Membership Number, Users, Rep DO visible only for the
		/// first line item in a transaction for Cross Reference Report
		/// </summary>
        protected void ColourCrossReferenceReport(DataGridItemEventArgs e)
		{
			e.Item.Cells[0].Visible = false;

			if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
			{
				DataRowView drvItem = (DataRowView) e.Item.DataItem;
				string strTempReceiptNumber = drvItem["Receipt Number"].ToString();          
   				if(strTempReceiptNumber != tmpreceiptnum)
				{
					AssignCrossReferenceClass(crosscolour, e);
				}
				else
				{
					e.Item.Attributes.Add("bgcolor",crosscolour);
					e.Item.Cells[1].Text = "";
					e.Item.Cells[2].Text = "";
					e.Item.Cells[5].Text = "";
					e.Item.Cells[6].Text = "";
				}
				tmpreceiptnum = strTempReceiptNumber;
			}
		}
		#endregion

		#region AssignCrossReferenceClass
		/// <summary>
		/// Swapping of the colour between ALTERNATE_ROW_CLASS and ROW_CLASS for Cross Reference Report
		/// </summary>
		private void AssignCrossReferenceClass(string tmpcolour,DataGridItemEventArgs e)
		{
			if ( tmpcolour == ROW_COLOR )
			{
				e.Item.Attributes.Add("bgcolor",ALTERNATE_ROW_COLOR);
				
				crosscolour = ALTERNATE_ROW_COLOR;
			}
			else if ( tmpcolour == ALTERNATE_ROW_COLOR )
			{
				e.Item.Attributes.Add("bgcolor",ROW_COLOR);
				crosscolour = ROW_COLOR;
			}
		}
		#endregion

	}
}
