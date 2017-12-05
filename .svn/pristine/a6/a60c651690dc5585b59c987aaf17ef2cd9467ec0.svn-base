/*
 *	History:
 *	11/11/2004 JOM Replaced InsuranceClasses.Service.Insurance with TurninClasses.Service.Turnin.
 *			  Also removed multiple creations of this service.
 *	REVISION HISTORY:
 *	MODIFIED BY COGNIZANT
 *	07/19/2005 - For changing the web.config entries pointing to database. This changes 
 *				 are made as part of CSR #3937 implementation.
 *	Changes Done:
 *	CSR#3937.Ch1 : Include the namespace for accessing the StringDictionary object
 *	CSR#3937.Ch2 : Changed the code for getting constant value from Application object (web.config changes)
 
 * MODIFIED BY COGNIZANT AS PART OF Q4 RETROFIT CHANGES
 * 12/2/2005 Q4-Retrofit.Ch1 : Modified the code to Seperate collection summary grid by CSAA 
 *		product and Other Products.There is also Total Collection summary grid provided Added
 *      code to Append a Single Quote (') to the Policy number for XLS Version
 * 12/2/2005 Q4-Retrofit.Ch2 : Modified the Code to Change Revenue Type as "Payments" for all
 *      HUON Products in the CSAA Collection Summary Grid
 * 12/2/2005 Q4-Retrofit.Ch3 :Modified the code to call the Renamed Function "PAYWorkflowReport"
 * 12/8/2005 Q4-Retrofit.Ch4 : Changed the code for getting constant value from Application object (web.config changes)
 * 
 * STAR Retrofit III Changes: 
 * Modified as a part of STAR Retrofit III Changes 
 * 5/25/2007 STAR Retrofit III.Ch1: 
 *           Modified the code to display the "PAYMENT TYPE" header
 * MODIFIED BY COGNIZANT AS A PART OF NAME CHANGE PROJECT ON 8/20/2010
 * NameChange.ch1:Modified the header CSAA PRODUCTS to AAA NCNU IE PRODUCTS as a part of name change project.
 * MODIFIED AS A PART OF TIME ZONE CHANGE ENHANCEMENT ON 8-31-2010.
 * TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time  by cognizant.
 * TimeZoneChange.Ch2-Commented to display only date and not time as a part of Time Zone Change  by cognizant.
 * SalesTurnin_Report Changes.Ch1 : Include the namespace for accessing the List objects.
 * SalesTurnin_Report Changes.Ch2 : Commented tdTransType since it is nomore used
 * SalesTurnin_Report Changes.Ch3 : Modified the code to fetch PAS Auto Summary Separately.
 * SalesTurnin_Report Changes.Ch4 : Added code to Sort PayType to get HUON Auto, Auto Non-Converted, PAS Auto, Homeowners and Membership Products in order.  
 * SalesTurnin_Report Changes.Ch5 : Added signature to the print version of sales turn in report
 * SalesTurnin_Report Changes.Ch6 : Added to remove the row revenue Type from Excel.
 * //Security Defect CH1 -End- Added the belowcode to check if the session values is equal to "null".If so assign the  session values to context.
 * CHG0083477 - Name Change from AAA NCNU IE to CSAA IG as part of Name change project on 11/15/2013.
 * CHG0104053 - PAS HO CH1 - Modified the below code to group the AAA SS Auto under PAS Auto 7/02/2014
 * CHG0104053 - PAS HO CH2 - Commented the below code to group the AAA SS Home and CSAA IG Home Condo under PAS Home 7/02/2014
 * CHG0104053 - PAS HO CH3 - Added the below code to filter only the PAS Home policies 7/02/2014
 * CHG0104053 - PAS HO CH4 - Added the below code to change the label from HomeOwner to Home/HDES 6/16/2014
 * CHG0104053 - PAS HO CH5- Added the below code to add the PAS Home policies 7/02/2014
 * CHG0104053 - PAS HO CH6 - Added the below code to change the label from HomeOwner to Home/HDES 6/13/2014
 * CHG0104053 - PAS HO CH7 - Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/16/2014
 * CHG0104053 - PAS HO CH8 - Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/16/2014
 * MAIG - CH1 - Modified the code to support common product types Auto and Home
 * MAIG - CH2 - Added code to Sort PayType to get PAS Auto,PAS Home,Home/HDES, WU/ACA, Homeowners and Membership Products in order.
 * MAIG - CH3 - Commeneted the un-used Product Types
 * CHG0109406 - CH1 - Modified the timezone from "MST Arizona" to Arizona
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
//CSR#3937.Ch1 : END - Include the namespace for accessing the StringDictionary object

//SalesTurnin_Report Changes.Ch1 :START - Include the namespace for accessing the List objects.
using System.Linq;
using System.Collections.Generic;
//SalesTurnin_Report Changes.Ch1 :END - Include the namespace for accessing the List objects.

namespace MSCR.Reports
{
	/// <summary>
	/// Summary description for TurnIn_SalesRep_PrintXls.
	/// </summary>
	public partial class TurnIn_SalesRep_PrintXls : System.Web.UI.Page
	{
		protected System.Web.UI.HtmlControls.HtmlTable tblPrintSales;
		protected System.Data.DataTable dtbRepeater;
		protected System.Data.DataTable dtbSummary;
		protected DataSet dtsRepeaterData;

		const string ROW_CLASS = "White";
		const string ALTERNATE_ROW_CLASS = "LightYellow";
		const string PENDING_APPROVAL_STATUS_DESC="Pending Approval";
		const string APPROVED_STATUS_DESC="Approved";
		// START - The Report Type codes are being stored as Constants
		const string SALESTURNIN_REPORT_TYPE="1";
		const string MEMBERSHIP_REPORT_TYPE="2";
		const string CROSSREFERENCE_REPORT_TYPE="3";
		// END
		const string MEMBERSHIP="Membership";
		const string EARNED_PREMIUM="Earned Premium";
		const string MEMBER="Mbr";
		const string INSTALLMENT_PAYMENT="Installment Payment";
		string colour = ALTERNATE_ROW_CLASS;

		// Variable to store the colour of the current row in the Cross Reference Report datagrid
		string crosscolour = ALTERNATE_ROW_CLASS;


		string tmpreceipt="";
		double dblTotal=0;
		CollectionSummaryLibrary CollectionSummary  = new CollectionSummaryLibrary();
		string Users;
		string ReportTypeID="";
		string StatusLinkID;
		Hashtable hsReportType;
		string tmporderid="";

		// Variable to store the Receipt Number of the previous transaction in the Cross Reference Report datagrid
		string tmpreceiptnum = "";


		double dblTotalMember=0;

		//Q4-Retrofit.Ch1-START :Added Declarations for generating Other Product Collection Summary and Total Collection Summary Grids
		protected System.Data.DataTable dtbOtherSummary;
		protected System.Data.DataTable dtbTotal;
		protected System.Data.DataTable dtbOtherRepeater;
		protected System.Data.DataTable dtbTotalRepeater;
		//Q4-Retrofit.Ch1-END
				
		private TurninClasses.Service.Turnin TurninService;

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			TurninService=new TurninClasses.Service.Turnin();
			Users=Page.User.Identity.Name;
			DisplayInfo();
			if (Request.QueryString["type"] == "XLS") 
			{
				Response.ContentType = "application/vnd.ms-excel";
				Response.Charset = ""; //Remove the charset from the Content-Type header.
			}
			Page.EnableViewState = false;
            //Security Defect CH1 -START- Added the belowcode to check if the session values is equal to "null".If so assign the  session values to context.

            if (Session["RepeaterData"] == null)
            {
                if (Context.Items.Contains("RepeaterData"))
                {
                    Session["RepeaterData"] = Context.Items["RepeaterData"].ToString();
                }

            }
           
                dtsRepeaterData = (DataSet)Session["RepeaterData"];
            if (Session["ReportType"] == null)
            {
                if (Context.Items.Contains("ReportType"))
                {
                    Session["ReportType"] = Context.Items["ReportType"].ToString();
                }

            }
          
                hsReportType = (Hashtable)Session["ReportType"];

            //Security Defect CH1 -End- Added the belowcode to check if the session values is equal to "null".If so assign the  session values to context.
			
			
			if(dtsRepeaterData != null)
			{
				if(Session["SelectedReportType"].ToString() == hsReportType[SALESTURNIN_REPORT_TYPE].ToString())
				{
					InvisibleControls();
					VisibleControls();
					rptSalesTurnInRepeater.DataSource=dtsRepeaterData;
					rptSalesTurnInRepeater.DataBind();
					PrepareSummaryData();
					//Q4-Retrofit.Ch1-START :Added the Code to display CSAA products,Other products and Total Collection Summary Grids
					PrepareCollectionSummaryData();
					//Q4-Retrofit.Ch1-END
                   //SalesTurnin_Report Changes.Ch5 : START- Added signature to the print version of sales turn in report
                    if (Request.QueryString["type"] == "PRINT")
                    {
                        pnlsignature.Visible = true;
                    }
                   //SalesTurnin_Report Changes.Ch5 : END- Added signature to the print version of sales turn in report
				}
				else if(Session["SelectedReportType"].ToString() == hsReportType[MEMBERSHIP_REPORT_TYPE].ToString())
				{
					InvisibleControls();
					tblMembershipDetail.Visible=true;
					rptMembershipDetail.Visible=true;
					rptMembershipDetail.DataSource=dtsRepeaterData;
					rptMembershipDetail.DataBind();
				}
				else if(Session["SelectedReportType"].ToString() == hsReportType[CROSSREFERENCE_REPORT_TYPE].ToString())
				{
					InvisibleControls();
					tblCrossReference.Visible=true;
					dgCrossReference.DataSource=dtsRepeaterData;
					dgCrossReference.DataBind();
				}
			}
			else
			{
				//"CS" for Confirmation Screen
				if(Request.QueryString["Screen"] == "CS")
				{	
					StatusLinkID=Cache[PENDING_APPROVAL_STATUS_DESC].ToString();
					OrderClasses.ReportCriteria rptCrit1=new OrderClasses.ReportCriteria();
					string []RepDO=lblRepDO.Text.Split('-');			
					rptCrit1.Users=Users;
					rptCrit1.Status_Link_ID=StatusLinkID;
					//RepDO[1] will return the RepID
					rptCrit1.RepDO=RepDO[1].ToString().Trim();
					DateTime StartDate = new DateTime(1900,1,1);
					DateTime EndDate = new DateTime(1900,1,1);
					rptCrit1.StartDate=StartDate;
					rptCrit1.EndDate=EndDate;
					if (Request.QueryString["ReportType"]== hsReportType[SALESTURNIN_REPORT_TYPE].ToString())
					{
						ReportTypeID= hsReportType[SALESTURNIN_REPORT_TYPE].ToString();
						rptCrit1.ReportType=Convert.ToInt32(ReportTypeID);
						//Q4-Retrofit.Ch3-START :Modified the code to call the Renamed function "PayWorkflowReport"
						DataSet dtsData = TurninService.PayWorkflowReport(rptCrit1);
						//Q4-Retrofit.Ch3 -END
						
						if (dtsData.Tables[0].Rows.Count == 0)
						{
							InvisibleControls();
							lblErrMsg.Visible=true;
							lblErrMsg.Text="No Data Found";
							return;
						}
						else
						{
							InvisibleControls();
							VisibleControls();
							rptSalesTurnInRepeater.DataSource=dtsData;
							rptSalesTurnInRepeater.DataBind();
							PrepareSummaryData();

							//Q4-Retrofit.Ch1-START :Added the code to display CSAA products,Other products and Total Collection Summary Grids
							PrepareCollectionSummaryData();
							//Q4-Retrofit.Ch1-END
						}
					}
					else if (Request.QueryString["ReportType"]== hsReportType[MEMBERSHIP_REPORT_TYPE].ToString())
						{
							ReportTypeID= hsReportType[MEMBERSHIP_REPORT_TYPE].ToString();
							rptCrit1.ReportType=Convert.ToInt32(ReportTypeID);

							//Q4-Retrofit.Ch3-START :Modified the code to call the Renamed function "PayWorkflowReport"
							//DataSet dtsData = TurninService.SalesRepCashierWorkFlow(rptCrit1);
							DataSet dtsData = TurninService.PayWorkflowReport(rptCrit1);
							//Q4-Retrofit.Ch3 -END
							if (dtsData.Tables[0].Rows.Count == 0)
							{
								InvisibleControls();
								lblErrMsg.Visible=true;
								lblErrMsg.Text="No Data Found";
								return;
							}
							else
							{
								InvisibleControls();
								tblMembershipDetail.Visible=true;
								rptMembershipDetail.Visible=true;
								rptMembershipDetail.DataSource=dtsData;
								rptMembershipDetail.DataBind();
							}
						}
					else if (Request.QueryString["ReportType"]== hsReportType[CROSSREFERENCE_REPORT_TYPE].ToString())
						{
							ReportTypeID= hsReportType[CROSSREFERENCE_REPORT_TYPE].ToString();
							rptCrit1.ReportType=Convert.ToInt32(ReportTypeID);

							//Q4-Retrofit.Ch3-START :Modified the code to call the Renamed function "PayWorkflowReport"
							DataSet dtsData = TurninService.PayWorkflowReport(rptCrit1);
							//Q4-Retrofit.Ch3 -END
							
							if (dtsData.Tables[0].Rows.Count == 0)
							{
								InvisibleControls();
								lblErrMsg.Visible=true;
								lblErrMsg.Text="No Data Found";
								return;
							}
							else
							{
								InvisibleControls();
								tblCrossReference.Visible=true;
								dgCrossReference.DataSource=dtsData;
								dgCrossReference.DataBind();
							}
						}
				}
				else
				{
					InvisibleControls();
					lblErrMsg.Visible=true;
					lblErrMsg.Text="Your session has timed out. Please re-submit the page";
					return;
				}
			}
		}
					   						
		#region PrepareSummaryData
		/// <summary>
		/// Populating the Temporary Data Table (Summary Data)
		/// </summary>
		private void PrepareSummaryData()
		{
			CreateSummaryTable();
			foreach(RepeaterItem item in rptSalesTurnInRepeater.Items)
			{
				//Q4-Retrofit.Ch4 : Changed the code for getting constant value from Application object (web.config changes)
				//Selecting the details of items which are checked
				if (item.ItemType == ListItemType.Item || item.ItemType == ListItemType.AlternatingItem) 
				{
					//Insert values into the Temporary Data Table (Summary Table)
						HtmlTableCell tdTempAmount = (HtmlTableCell)item.FindControl("tdAmount");
						string strAmount=tdTempAmount.Attributes["amount"].ToString();
						HtmlTableCell tdTempPymtType=(HtmlTableCell)item.FindControl("tdPymtType");
						HtmlTableCell tdTempProdType=(HtmlTableCell)item.FindControl("tdProdType");

                        // SalesTurnin_Report Changes.Ch2 : START - Commented tdTransType since it is nomore used
						// HtmlTableCell tdTempTransType=(HtmlTableCell)item.FindControl("tdTransType");
                        // SalesTurnin_Report Changes.Ch2 : END - Commented tdTransType since it is nomore used

					//Q4-Retrofit.Ch1 -START:Added the code to create CSAA Product and Other Product Collection
					//Summary Grid based on the Upload Type Value.										   
					//string strUploadType = "," + Config.Setting("UploadType.CSAAProducts") + ",";
					string strUploadType = "," + Convert.ToString(((StringDictionary)Application["Constants"])["UploadType.CSAAProducts"]) + ",";
                    HtmlTableCell tdpolicyno = (HtmlTableCell)item.FindControl("tdpolicynumber");
                    string pno = tdpolicyno.InnerText.Trim();
                    if (Request.QueryString["type"] == "XLS")
                    {
                        pno = pno.Replace("'", "").Trim();

                    }
                    string[] pno1 = pno.Split(new char[] { '\t' });
                    pno1[0] = pno1[0].Trim();
                    int length = (pno1[0]).Length;
                    string prodtype = tdTempProdType.InnerText.Trim();		
					if (strUploadType.IndexOf("," + tdTempProdType.Attributes["UploadType"].ToString() + ",") >= 0 )
					{
                        // SalesTurnin_Report Changes.Ch2 : START - Commented tdTransType since it is nomore used
                        //string strTransType = tdTempTransType.InnerText;
                        // SalesTurnin_Report Changes.Ch2 : END - Commented tdTransType since it is nomore used

						//Q4-Retrofit.Ch2-START :Added the code to display "Payments" as Revenue Type for HUON products in the 
						//CSAA Collection Summary Grid 
						//if (tdTempProdType.Attributes["UploadType"].ToString() == Config.Setting("UploadType.HUON"))

                        // SalesTurnin_Report Changes.Ch2 : START - Commented tdTransType since it is nomore used
                        //if (tdTempProdType.Attributes["UploadType"].ToString() == Convert.ToString(((StringDictionary)Application["Constants"])["UploadType.HUON"]))
                        //{
							//Getting the Revenue Type description for HUON Products from the config
							//strTransType = 	Config.Setting("RevenueTypeDescription.HUON") ;
                            //strTransType = Convert.ToString(((StringDictionary)Application["Constants"])["RevenueTypeDescription.HUON"]);
                        //}
						//Q4-Retrofit.Ch2 :End

						//For CSAA products Collection Summary
                        //InsertSummaryTable(ref dtbSummary,tdTempPymtType.InnerText,tdTempProdType.InnerText,strTransType,Convert.ToDecimal(strAmount.Replace("$","").Replace(",","")));
                        // SalesTurnin_Report Changes.Ch2 : END - Commented tdTransType since it is nomore used

                        //SalesTurnin_Report Changes.Ch3: START - Added the below code to fetch PAS Auto Summary Separately.
						//MAIG - CH1 - BEGIN - Modified the code to support common product types Auto and Home
                        //CHG0104053 - PAS HO CH1- BEGIN : Modified the below code to group the AAA SS Auto under PAS Auto 7/02/2014
                        if (length == 13 && (prodtype.ToLower().Equals("auto")))
                        //CHG0104053 - PAS HO CH1- END : Modified the below code to group the AAA SS Auto under PAS Auto 7/02/2014
                        {
                            InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "PAS Auto", "", Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                        }
                        
                        /*else if (length == 9 && prodtype.ToLower().Equals("auto"))
                        {

                            InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "HUON Auto", "", Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                        }*/
                        
                        else
                        {
                            //CHG0104053 - PAS HO CH2- BEGIN : Commented the below code to group the AAA SS Home and CSAA IG Home Condo under PAS Home 7/02/2014
                            ////if ((length == 13 && prodtype.ToLower().Equals("aaa signature series home")) || (length == 7 && prodtype.ToLower().Equals("homeowners")))
                            ////{
                            ////    InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "PAS Home", "", Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                            ////}
                            //////PAS HO CH35- END : Added the below code to group the AAA SS Home and CSAA IG Home Condo under PAS Home 7/02/2014
                            ////InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, tdTempProdType.InnerText, "", Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                            //CHG0104053 - PAS HO CH2- END : Commented the below code to group the AAA SS Home and CSAA IG Home Condo under PAS Home 7/02/2014
                            if ((length == 13 && prodtype.ToLower().Equals("home")))
                            {
                                InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "PAS Home", "", Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));                                
                            }
                            else if (length == 7 && prodtype.ToLower().Equals("home"))
                            {
                                InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "Home/HDES", "", Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                            }
                            else
                            {
                                //Modified the code to pass strTransType as Parameter instead of tdTempTransType.InnerText
                                //MAIG  - Modified the below line ref type paramter name from dtbSummary to dtbOtherSummary to display in WU/ACA Products Collection 11/25/2014
                                InsertSummaryTable(ref dtbOtherSummary, tdTempPymtType.InnerText, Convert.ToString(((StringDictionary)Application["Constants"])["ProductTypeHeader.WU_ACA"]), "", Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                                //Q4-Retrofit.Ch1-END
                            }
                        }

                        //SalesTurnin_Report Changes.Ch3: END - Added the below code to fetch PAS Auto Summary Separately.

					}
					else
					{
                        if (length == 13 && (prodtype.ToLower().Equals("dwelling fire") || prodtype.ToLower().Equals("personal umbrella")))
                        {
                            InsertSummaryTable(ref dtbSummary, tdTempPymtType.InnerText, "PAS Home", "", Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                        }
                        else
                        {
                            //For Other Products Collection Summary
                            //InsertSummaryTable(ref dtbOtherSummary,tdTempPymtType.InnerText,Config.Setting("ProductTypeHeader.WU_ACA").ToString(),tdTempTransType.InnerText,Convert.ToDecimal(strAmount.Replace("$","").Replace(",","")));

                            // SalesTurnin_Report Changes.Ch2 : START - Commented tdTransType since it is nomore used
                            //InsertSummaryTable(ref dtbOtherSummary,tdTempPymtType.InnerText,Convert.ToString(((StringDictionary)Application["Constants"])["ProductTypeHeader.WU_ACA"]),tdTempTransType.InnerText,Convert.ToDecimal(strAmount.Replace("$","").Replace(",","")));
                            InsertSummaryTable(ref dtbOtherSummary, tdTempPymtType.InnerText, Convert.ToString(((StringDictionary)Application["Constants"])["ProductTypeHeader.WU_ACA"]), "", Convert.ToDecimal(strAmount.Replace("$", "").Replace(",", "")));
                            // SalesTurnin_Report Changes.Ch2 : END - Commented tdTransType since it is nomore used
                        }
						//MAIG - CH1 - END - Modified the code to support common product types Auto and Home
					}
					//Q4-Retrofit.Ch1 -END

					//Q4-Retrofit.Ch1-START :Added the Code to generate Total Collection Summary
					InsertTotalTable(ref dtbTotal,tdTempPymtType.InnerText,Convert.ToDecimal(strAmount.Replace("$","").Replace(",","")));						
					//Q4-Retrofit.Ch1-END
				}
				//Q4-Retrofit.Ch4 : END - Changed the code for getting constant value from Application object (web.config changes)
			}
			dtbSummary.DefaultView.Sort = "PayType,ProdType,RevType";
           
			//Q4-Retrofit.Ch1-START :Added the code to Sort Other Product Collection Summary and Total Collection Summary grid
			dtbOtherSummary.DefaultView.Sort = "PayType,ProdType,RevType";
			dtbTotal.DefaultView.Sort = "PayType";
			//Q4-Retrofit.Ch1-END


            // SalesTurnin_Report Changes.Ch4 :START- Added code to Sort PayType to get HUON Auto, Auto Non-Converted, PAS Auto, Homeowners and Membership Products in order.
            //MAIG - CH2 - BEGIN - Added code to Sort PayType to get PAS Auto,PAS Home,Home/HDES, WU/ACA, Homeowners and Membership Products in order.
            DataTable mdtbsummary = dtbSummary.Clone();
            //List<DataRow> lsthuon = dtbSummary.Select("ProdType= 'HUON Auto'").ToList();
            //List<DataRow> lstlegacy = dtbSummary.Select("ProdType = 'Auto Non-Converted'").ToList();
            List<DataRow> lstpas = dtbSummary.Select("ProdType = 'PAS Auto'").ToList();
            //CHG0104053 - PAS HO CH3- BEGIN : Added the below code to filter only the PAS Home policies 7/02/2014
            List<DataRow> lstpasHome = dtbSummary.Select("ProdType = 'PAS Home'").ToList();
            //CHG0104053 - PAS HO CH3- END : Added the below code to filter only the PAS Home policies 7/02/2014
            //CHG0104053 - PAS HO CH4 - BEGIN : Added the below code to change the label from HomeOwner to Home/HDES 6/16/2014
            List<DataRow> lsthome = dtbSummary.Select("ProdType = 'Home/HDES'").ToList();
            //CHG0104053 - PAS HO CH4 - END : Added the below code to change the label from HomeOwner to Home/HDES 6/16/2014
            List<DataRow> lstWU_ACA = dtbSummary.Select("ProdType = 'WU/ACA'").ToList();

            /*if (lsthuon.Count > 0)
            {
                foreach (DataRow item in lsthuon)
                {
                    mdtbsummary.Rows.Add(item.ItemArray);
                }

            }
            if (lstlegacy.Count > 0)
            {
                foreach (DataRow item in lstlegacy)
                {
                    mdtbsummary.Rows.Add(item.ItemArray);
                }
            }*/
            if (lstpas.Count > 0)
            {
                foreach (DataRow item in lstpas)
                {
                    mdtbsummary.Rows.Add(item.ItemArray);
                }
            }
            //CHG0104053 - PAS HO CH5- BEGIN : Added the below code to add the PAS Home policies 7/02/2014
            if (lstpasHome.Count > 0)
            {
                foreach (DataRow item in lstpasHome)
                {
                    mdtbsummary.Rows.Add(item.ItemArray);
                }
            }
            //CHG0104053 - PAS HO CH5- END : Added the below code to add the PAS Home policies 7/02/2014
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
            //MAIG - CH2 - END - Added code to Sort PayType to get PAS Auto,PAS Home,Home/HDES, WU/ACA, Homeowners and Membership Products in order.
            dtbSummary = mdtbsummary.Copy();

            // SalesTurnin_Report Changes.Ch4 :END- Added code to Sort PayType to get HUON Auto, Auto Non-Converted, PAS Auto, Homeowners and Membership Products in order.

            CreateRepeaterTable();
            // SalesTurnin_Report Changes.Ch4 :START- commented existing code and Added code to Sort PayType to get HUON Auto, Auto Non-Converted, PAS Auto, Homeowners and Membership Products in order.
            //DataView rowView = new DataView(dtbSummary,"","PayType,ProdType,RevType",DataViewRowState.CurrentRows);
            //DataView colListView = new DataView(dtbSummary,"","ProdType,RevType",DataViewRowState.CurrentRows);
            DataView rowView = new DataView(dtbSummary, "", "", DataViewRowState.CurrentRows);
            DataView colListView = new DataView(dtbSummary, "", "", DataViewRowState.CurrentRows);
            // SalesTurnin_Report Changes.Ch4 :EN- commented existing code and Added code to Sort PayType to get HUON Auto, Auto Non-Converted, PAS Auto, Homeowners and Membership Products in order.


			//Q4-Retrofit.Ch1 -START:Added the code to generate Other Product Collection Summary and Total Collection Summary Table
			DataView rowViewOther = new DataView(dtbOtherSummary,"","PayType,ProdType,RevType",DataViewRowState.CurrentRows);
			DataView colListViewOther = new DataView(dtbOtherSummary,"","ProdType,RevType",DataViewRowState.CurrentRows);


			DataView rowViewTotal = new DataView(dtbTotal,"","PayType",DataViewRowState.CurrentRows);
			DataView colListViewTotal = new DataView(dtbTotal,"","",DataViewRowState.CurrentRows);
			//Q4-Retrofit.Ch1-END

			// Creating and adding columns in the repeater table in the format ProdType|RevType 
			foreach(DataRowView dr in colListView)
			{
				//Q4-Retrofit.Ch1-START:Added dtbRepeater table to the parameter
				AddColumnToRepeaterTable(dr["ProdType"].ToString() + "|" + dr["RevType"].ToString(),ref dtbRepeater);
				//Q4-Retrofit.Ch1 :END
				
			}
			//Inserting data in the Repeater table from the summary table
			foreach(DataRowView dr in rowView)
			{
				//Q4-Retrofit.Ch1-START :Added dtbRepeater table  to the parameter
				InsertRepeaterTable(ref dtbRepeater, dr["PayType"].ToString(), dr["ProdType"].ToString(), dr["RevType"].ToString(), Convert.ToDecimal(dr["Amount"].ToString()));
				//Q4-Retrofit.Ch1-END
				
			}
			//Q4-Retrofit.Ch1 -START:Added the code to generate Other Product Collection Summary table
			foreach(DataRowView dr in colListViewOther)
			{
				AddColumnToRepeaterTable(dr["ProdType"].ToString() + "|" + dr["RevType"].ToString(),ref dtbOtherRepeater );
			}
			//Q4-Retrofit.Ch1-END
	
			//Q4-Retrofit.Ch1-START :Added the code to generate Other Product Collection Summary table
			foreach(DataRowView dr in rowViewOther)
			{
				InsertRepeaterTable( ref dtbOtherRepeater , dr["PayType"].ToString(), dr["ProdType"].ToString(), dr["RevType"].ToString(), Convert.ToDecimal(dr["Amount"].ToString()));
			}
			//Q4-Retrofit.Ch1-END

				
			//Q4-Retrofit.Ch1-START :Added the Code to generate Total Collection Summary table
			AddColumnToRepeaterTable("Amount",ref dtbTotalRepeater);
			foreach(DataRowView dr in rowViewTotal)
			{
				InsertRepeaterTotalTable(ref dtbTotalRepeater , dr["PayType"].ToString(),Convert.ToDecimal(dr["Amount"].ToString()));
			}
            //Q4-Retrofit.Ch1-END

            // SalesTurnin_Report Changes.Ch4 : START Added code to Sort PayType to get HUON Auto, Auto Non-Converted, PAS Auto, Homeowners and Membership Products in order.  

            dtbRepeater.DefaultView.Sort = "PayType";
            dtbRepeater = dtbRepeater.DefaultView.ToTable(true);

            dtbOtherRepeater.DefaultView.Sort = "PayType";
            dtbOtherRepeater = dtbOtherRepeater.DefaultView.ToTable(true); 

            // SalesTurnin_Report Changes.Ch4 : END Added code to Sort PayType to get HUON Auto, Auto Non-Converted, PAS Auto, Homeowners and Membership Products in order.  

		}
		#endregion

		#region CreateSummaryTable
		/// <summary>
		///  Temporary Data Table (Summary Table) is created having the columns PayType,ProdType,RevType and Amount 
		///  with PayType,ProdType,RevType as combinde primary key
		/// </summary>
		private void CreateSummaryTable()
		{
			dtbSummary = new DataTable();
			dtbSummary.Columns.Add("PayType");
			dtbSummary.Columns.Add("ProdType");
			dtbSummary.Columns.Add("RevType");
			dtbSummary.Columns.Add("Amount",System.Type.GetType("System.Decimal"));
			dtbSummary.PrimaryKey = new DataColumn[]{ dtbSummary.Columns["PayType"],dtbSummary.Columns["ProdType"],dtbSummary.Columns["RevType"] };

			//Q4-Retrofit.Ch1-START :Added the code to create a temporary table for Other product collection summary
			dtbOtherSummary = new DataTable();
			dtbOtherSummary.Columns.Add("PayType");
			dtbOtherSummary.Columns.Add("ProdType");
			dtbOtherSummary.Columns.Add("RevType");
			dtbOtherSummary.Columns.Add("Amount",System.Type.GetType("System.Decimal"));
			dtbOtherSummary.PrimaryKey = new DataColumn[] {dtbOtherSummary.Columns["PayType"],dtbOtherSummary.Columns["ProdType"],dtbOtherSummary.Columns["RevType"] };
			//Q4-Retrofit.Ch1-END

			//Q4-Retrofit.Ch1-START :Added the code to create a temporary table for Total collection summary
			dtbTotal = new DataTable();
			dtbTotal.Columns.Add("PayType");
			dtbTotal.Columns.Add("Amount",System.Type.GetType("System.Decimal"));
			dtbTotal.PrimaryKey = new DataColumn[]{dtbTotal.Columns["PayType"]};
			//Q4-Retrofit.Ch1-END
		}
		#endregion

		#region CreateRepeaterTable
		/// <summary>
		///  Temporary Data Table (Repeater Table) is created with the column PayType
		/// </summary>
		private void CreateRepeaterTable()
		{
			dtbRepeater = new DataTable();
			dtbRepeater.Columns.Add("PayType");
			//Q4-Retrofit.Ch1-START :Added the code to create a temporary table for Other product collection summary		
			dtbOtherRepeater = new DataTable();
			dtbOtherRepeater.Columns.Add("PayType");
			//Added the code to create a temporary table for Total collection summary		
			dtbTotalRepeater = new DataTable();
			dtbTotalRepeater.Columns.Add("PayType");
			//Q4-Retrofit.Ch1-END
		}
		#endregion

		#region AddColumnToRepeaterTable
		/// <summary>
		///  Adding a column to the repeater table
		/// </summary>
		 //Q4-Retrofit.Ch1-START :Modified the Code to have a datatable Parameter
		private void AddColumnToRepeaterTable(string strColName,ref DataTable dtbReferer)
		{
			if ( !dtbReferer.Columns.Contains(strColName) )
			{
				DataColumn dtCol = new DataColumn(strColName,System.Type.GetType("System.Decimal"));
				dtCol.DefaultValue = Convert.ToDecimal(0);
				//Q4-Retrofit.Ch1-START:Modified the Code the add the column to dtbReferer
				dtbReferer.Columns.Add(dtCol);
				//Q4-Retrofit.Ch1-END
			}
		}
		
		#endregion

		#region InsertRepeaterTable
		/// <summary>
		///  Inserting values to the repeater table from the summary table
		/// </summary>
		//Q4-Retrofit.Ch1-START :Modified the Code to have a datatable Parameter
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
		//Q4-Retrofit.Ch1-END
		
		
		#endregion

		#region InsertSummaryTable
		/// <summary>
		///  Inserting values into the Temporary Data Table (Summary table)
		/// </summary>
		//Q4-Retrofit.Ch1-START :Modified the Code to have a datatable Parameter
		private void InsertSummaryTable(ref DataTable dtbTemp,string inPaytype,string inProdtype,string inRevtype,decimal inAmount)
		{
			DataRow dtrSummary;
			DataRow[] dtrArrar;
			inPaytype = inPaytype.Trim(); 
			inProdtype=inProdtype.Trim();
            //MAIG - CH3 - BEGIN - Commeneted the un-used Product Types
            //CHG0104053 - PAS HO CH6 - START : Added the below code to change the label from HomeOwner to Home/HDES 6/13/2014
            //if (inProdtype.ToLower().Equals("homeowners"))
            //{
            //    inProdtype = "Home/HDES";
            //}
            //CHG0104053 - PAS HO CH6 - END : Added the below code to change the label from HomeOwner to Home/HDES 6/13/2014
			inRevtype=inRevtype.Trim();
		
			//For Product Type 'Mbr' it is changed to 'Membership'
			/*if (inProdtype.ToUpper() == MEMBER.ToUpper())
			{
				inProdtype= MEMBERSHIP;
			}*/
            //MAIG - CH3 - END - Commeneted the un-used Product Types
			//For Revenue Type 'Earned Premium' it is changed to 'Installment Payment'
			if(inRevtype.ToUpper() == EARNED_PREMIUM.ToUpper())
			{
				inRevtype=INSTALLMENT_PAYMENT;
			}
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
				
				//Q4-Retrofit.Ch1 :Modified the code to insert the row to the Temporary table
				dtbTemp.Rows.Add(dtrSummary);
				//Q4-Retrofit.Ch1-END
			}
				// If there is a row having the same PayType, ProdType, Revenue Type update the amount alone
			else
			{
				dtrArrar[0]["Amount"] = Convert.ToDecimal(dtrArrar[0]["Amount"].ToString()) + Convert.ToDecimal(inAmount);
			}
		}
		
			#endregion	
		//Q4-Retrofit.Ch1-START :Added the function to generate Total Summary table		
		#region InsertRepeaterTotalTable
		/// <summary>
		/// </summary>
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
		//Q4-Retrofit.Ch1-END

		//Q4-Retrofit.Ch1-START :Added the function to generate Total Summary table
		#region InsertTotalTable
		/// <summary>
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
		//Q4-Retrofit.Ch1-END

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

		private void InvisibleControls()
		{
		trBlankRow1.Visible=false;
		trBlankRow2.Visible=false;
		tblCollSummary.Visible=false;
		trSalesRepeater.Visible=false;
		rptMembershipDetail.Visible=false;
		rptSalesTurnInRepeater.Visible=false;
		tblCrossReference.Visible=false;
		tblMembershipDetail.Visible=false;
		//Q4-Retrofit.Ch1-START :Added the code to hide the Collection Summary Grids 
			tblSalesTurnInList.Visible=false;
		//Q4-Retrofit.Ch1-END
		}

		private void VisibleControls()
		{
			trBlankRow1.Visible=true;
			trBlankRow2.Visible=true;
			tblCollSummary.Visible=true;
			trSalesRepeater.Visible=true;
			rptSalesTurnInRepeater.Visible=true;
			//Q4-Retrofit.Ch1-START:Added the code to Show the Collection Summary Grids 
			tblSalesTurnInList.Visible=true;
			//Q4-Retrofit.Ch1-END
			
		}

		private void DisplayInfo()
		{
			lblRepDO.Text = Session["RepDO"].ToString();
            //CHG0109406 - CH1 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
            //TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time  by cognizant.
            lblRunDate.Text = DateTime.Now.ToString() + " " + "Arizona";
            //CHG0109406 - CH1 - END - Modified the timezone from "MST Arizona" to Arizona
			lblUser.Text= Session["Users"].ToString();
			lblReportStatus.Text = Session["ReportStatus"].ToString();
			if(Session["DateRange"] != null)
			{
				trDateRange.Visible=true;
               
                lblDateRange.Text = Session["DateRange"].ToString();
			}
			else
			{
				trDateRange.Visible=false;
			}
		}
		protected void rptSalesTurnInRepeater_ItemBound(Object Sender, RepeaterItemEventArgs e)
		{
			//Q4-Retrofit.Ch1-START :Added the code to Append a Sigle Quote (') to the Policy number for XLS Version
			if(Request.QueryString["type"] == "XLS")
			{
				if(e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem)
				{
					HtmlTableCell tdTempPolicyNumber = (HtmlTableCell) e.Item.FindControl("tdPolicyNumber");
					tdTempPolicyNumber.InnerHtml = "'" + tdTempPolicyNumber.InnerHtml.ToString();
				}
			}
			//Q4-Retrofit.Ch1-END
			
			AlternatingColour(e);
			CalculateTotal(e);
		}
		protected void rptMembershipDetail_ItemBound(Object Sender, RepeaterItemEventArgs e)
		{
			AlternatingColourMembership(e);
			CalculateTotalMembership(e);
		}

		protected void dgCrossReference_ItemBound(Object Sender, DataGridItemEventArgs e)
		{
			ColourCrossReferenceReport(e);
//			SetCrossReferenceGridWidth(e);
		}

		protected void SetCrossReferenceGridWidth(DataGridItemEventArgs e)
		{
		e.Item.Cells[1].Width=100;
		e.Item.Cells[2].Width=110;
		e.Item.Cells[3].Width=120;
		e.Item.Cells[4].Width=110;
		e.Item.Cells[4].Width=134;
		e.Item.Cells[6].Width=30;
		}
		private void AlternatingColour(RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				HtmlTableRow trTempItemRow=(HtmlTableRow) e.Item.FindControl("trItemRow");
				Label lblTempReceiptDate = (Label) e.Item.FindControl("lblReceiptDate");
				Label lblTempStatus=(Label) e.Item.FindControl("lblStatus");
				Label lblTempMemberName=(Label) e.Item.FindControl("lblMemberName");
				Label lblTempReceiptNo = (Label) e.Item.FindControl("lblReceiptNo");
				HtmlTableCell tcTempReceiptNo = (HtmlTableCell) e.Item.FindControl("tdReceiptNo");
				string strTempReceiptNo = lblTempReceiptNo.Attributes["value"].ToString();
				//Assigning different colour if the receipt numbers are different
				if(strTempReceiptNo!=tmpreceipt)
				{
					AssignClass(colour,trTempItemRow);
				}
				else
				{
					trTempItemRow.Attributes.Add("bgcolor",colour);
					lblTempReceiptDate.Visible=false;
					lblTempStatus.Visible=false;
					lblTempMemberName.Visible=false;
					tcTempReceiptNo.InnerHtml="&nbsp;";
				}
				tmpreceipt=strTempReceiptNo;
			}
		}
		
		private void AlternatingColourMembership(RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				HtmlTableRow trTempItemRowMember=(HtmlTableRow) e.Item.FindControl("trItemRowMember");
				Label lblTempEffectiveDate = (Label) e.Item.FindControl("lblEffectiveDate");
                //TimeZoneChange.Ch2-Commented to display only date and not time as a part of Time Zone Change  by cognizant.
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
					lblTempAmount.Text ="0";
					lblTempAmount.Visible=false;
					lblTempPymtType.Visible=false;
				}
				tmporderid=strTempOrderID;
			}
		}
		
		private void AssignClass(string tmpcolour,HtmlTableRow tr)
		{
			if ( tmpcolour == ROW_CLASS )
			{
				tr.Attributes.Add("bgcolor",ALTERNATE_ROW_CLASS);
				colour = ALTERNATE_ROW_CLASS;
			}
			else if ( tmpcolour == ALTERNATE_ROW_CLASS )
			{
				tr.Attributes.Add("bgcolor",ROW_CLASS);
				colour = ROW_CLASS;
			}
		}
		#region CalculateTotal
		/// <summary>
		/// Calculating the total amount of all the items in the Repeater
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
				string strAmount=tdTempAmount.Attributes["amount"].ToString();
				dblTotal = dblTotal + Convert.ToDouble(strAmount);
			}
		}
		#endregion
		
		private void CalculateTotalMembership(RepeaterItemEventArgs e)
		{
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem ||e.Item.ItemType == ListItemType.Footer) 
			{
				if (e.Item.ItemType == ListItemType.Footer)
				{
					Label lblTempTotalMember = (Label)e.Item.FindControl("lblTotalMember");
					lblTempTotalMember.Text = dblTotalMember.ToString("$##0.00");
					return;
				}
				//Total all the individual amounts
				Label lblTempAmount = (Label)e.Item.FindControl("lblAmount");
				lblTempAmount.Text=lblTempAmount.Text.Replace(",",""); 
				string strAmountMember =  lblTempAmount.Text;
				dblTotalMember = dblTotalMember + Convert.ToDouble(strAmountMember.Replace("$",""));
			}
		}

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
			if ( tmpcolour == ROW_CLASS )
			{
				e.Item.Attributes.Add("bgcolor",ALTERNATE_ROW_CLASS);
				crosscolour = ALTERNATE_ROW_CLASS;
			}
			else if ( tmpcolour == ALTERNATE_ROW_CLASS )
			{
				e.Item.Attributes.Add("bgcolor",ROW_CLASS);
				crosscolour = ROW_CLASS;
			}
		}
		#endregion

		//Q4-Retrofit.Ch1-START :Added the function to generate Collection Summary Data
		#region PrepareCollectionSummaryData
		/// <summary>
		/// Create Summary Table for CSAA Products, Other Product and Total Collection Summary
		/// </summary>
		private void PrepareCollectionSummaryData()
		{
			//Q4-Retrofit.Ch1-START :Modified the code to Change the Caption from "COLLECTION SUMMARY to CSAA PRODUCTS"
            //NameChange.ch1:Modified the header CSAA PRODUCTS to AAA NCNU IE PRODUCTS as a part of name change project on 08/20/2010.
            //SalesTurnin_Report Changes.Ch6 :START- Added to remove the row revenue Type from Excel.
            dtbRepeater.Columns[0].ColumnName = "PAYMENT TYPE|";
            //CHG0083477 - Name Change from AAA NCNU IE to CSAA IG as part of Name change project on 11/15/2013.
            //CHG0104053 - PAS HO CH7 - START : Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/16/2014
            CollectionSummary.CreateHeader(ref tblCollSummary ,ref dtbRepeater ,"TURN-IN PRODUCTS");
            //CHG0104053 - PAS HO CH7 - END : Added the below code to change the label from CSAA IG PRODUCTS to Turn-in Products 6/16/2014
            dtbRepeater.Columns[0].ColumnName = "PayType";
            //SalesTurnin_Report Changes.Ch6 :END- Added to remove the row revenue Type from Excel.
			//Q4-Retrofit.Ch1-END
			CollectionSummary.AddCollSummaryData(ref tblCollSummary ,ref dtbRepeater);

			//Q4-Retrofit.Ch1-START :Added the variable "dbltotal" to get the Total Amount based on which the 
			//Collection Summary table will be shown 
			double dblTotal = CollectionSummary.AddCollSummaryFooter(ref tblCollSummary ,ref dtbRepeater);
			//Q4-Retrofit.Ch1-END

			//Q4-Retrofit.Ch1-START :Added the below code to check the Total based on which the 
			//Collection Summary will be shown 
			if (dblTotal > 0)
			{
				trCollSummary.Visible = true;
				trOtherCollSummarySplit.Visible = true;
				//STAR Retrofit III.Ch1: START - Modified the code to display the "PAYMENT TYPE" header
				//tblCollSummary.Rows[1].Cells[0].InnerHtml ="&nbsp;";
				//STAR Retrofit III.Ch1: END
               
			}	
			else
			{
				trCollSummary.Visible = false;
				trOtherCollSummarySplit.Visible = false;
			}
			//Q4-Retrofit.Ch1-END

			//Q4-Retrofit.Ch1-START Added the code to create Other Product Collection Summary table
            //SalesTurnin_Report Changes.Ch6 :START- Added to remove the row revenue Type from Excel.
            dtbOtherRepeater.Columns[0].ColumnName = "PAYMENT TYPE|";
            //CHG0104053 - PAS HO CH8 - START : Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/16/2014
			CollectionSummary.CreateHeader(ref tblOtherCollSummary,ref dtbOtherRepeater,"WU/ACA PRODUCTS");
            //CHG0104053 - PAS HO CH8 - END : Added the below code to change the label from ALL OTHER PRODUCTS to WU/ACA Products 6/16/2014
            dtbOtherRepeater.Columns[0].ColumnName = "PayType";
            //SalesTurnin_Report Changes.Ch6 :END- Added to remove the row revenue Type from Excel.
			CollectionSummary.AddCollSummaryData(ref tblOtherCollSummary,ref dtbOtherRepeater);
			dblTotal = CollectionSummary.AddCollSummaryFooter(ref tblOtherCollSummary, ref dtbOtherRepeater);
			
			if(dblTotal > 0)
			{
				trOtherCollSummary.Visible = true;
				trTotalCollSummarySplit.Visible= true;
			}
			else
			{
				trOtherCollSummary.Visible =false;
				trTotalCollSummarySplit.Visible =false;
			}
			//Q4-Retrofit.Ch1-END

			//Q4-Retrofit.Ch1-START Added the code to create Total Collection Summary table
			CollectionSummary.CreateTotalHeader(ref tblTotalCollSummary,ref dtbTotalRepeater);
			CollectionSummary.AddCollTotalSummaryData(ref tblTotalCollSummary ,ref dtbTotalRepeater);
			dblTotal = CollectionSummary.AddCollTotalSummaryFooter(ref tblTotalCollSummary,ref dtbTotalRepeater); 
			//Q4-Retrofit.Ch1-END
		}
		#endregion
		//Q4-Retrofit.Ch1-END



	}
}
