/*	REVISION HISTORY:
 *	MODIFIED BY COGNIZANT
 *	07/19/2005 - For changing the web.config entries pointing to database. This changes 
 *				 are made as part of CSR #3937 implementation.
 *	Changes Done:
 *	CSR#3937.Ch1 : Include the namespace for accessing the StringDictionary object
 *	CSR#3937.Ch2 : Changed the code for the new way of accessing the constants (web.config changes)
 *	07/26/2005	-	For renaming the files ending with _New.
 *  CSR#3937.Ch3 : Renamed the class InsSearchOrders_New to InsSearchOrders
 *  CSR#3824.Ch1 : Modified to add a new Status Column in the Transaction Search
 * 
 * MODIFIED BY COGNIZANT AS PART OF Q4 RETROFIT CHANGES
 * 11/29/2005 Q4-Retrofit.Ch1 : Removed the condition (drv["Appl."].ToString().Trim() != Config.Setting("ApplicationType.Star")) 
 *          in the IF loop This change has been made to display the Duplicate Receipt button for the STAR CUI transactions
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
using System.Data.SqlClient;
using System.Configuration;
using CSAAWeb;
using CSAAWeb.Serializers;
using CSAAWeb.AppLogger;
using OrderClasses.Service;
using InsuranceClasses;

//CSR#3937.Ch1 : START - Include the namespace for accessing the StringDictionary object
using System.Collections.Specialized;
//CSR#3937.Ch1 : END - Include the namespace for accessing the StringDictionary object

namespace MSCR.Reports
{
	/// <summary>
	/// Summary description for InsReport1.
	/// </summary>
	//CSR#3937.Ch3 : START - Renamed the class InsSearchOrders_New to InsSearchOrders
	public partial class InsSearchOrders : CSAAWeb.WebControls.PageTemplate
		//CSR#3937.Ch3 : END - Renamed the class InsSearchOrders_New to InsSearchOrders
	{
		
		string sCCSignature=string.Empty;
		string start_date, end_date;

		protected DataTable Dt;

		InsRptLibrary rptLib = new InsRptLibrary();

		protected void Page_Load(object sender, System.EventArgs e)
		{

			// Put user code to initialize the page here
			if (!Page.IsPostBack)
			{	
				Session.Contents.Clear();
				Session.Abandon();

				rptLib.FillNumericComboBox(start_date_month,1,12);
				rptLib.FillNumericComboBox(end_date_month,1,12);

				start_date_month.SelectedIndex = DateTime.Now.Month - 1;
				end_date_month.SelectedIndex = DateTime.Now.Month - 1;

				start_date_year.SelectedIndex=  DateTime.Now.Year - 2002;
				end_date_year.SelectedIndex = DateTime.Now.Year - 2002;

				rptLib.DynamicDate(start_date_day, start_date_month, start_date_year);
				rptLib.DynamicDate(end_date_day, end_date_month, end_date_year);
				end_date_day.SelectedIndex = end_date_day.Items.Count - 1;

				lblInsTitle.Visible = false;
				lblMbrTitle.Visible = false;
				pnlTitle.Visible = false;
				Initialize();
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
			//START - Code added by COGNIZANT 07/07/2004
			//Initialize(); // To Load the Datas into Cache
			//BindData();   // Bind the Data from the Cache to DropdownList
			//END

		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		/// <summary>
		/// Binding the user controls.
		/// </summary>
		private void Initialize()
		{    

			Order DataConnection = new Order(Page);
			
			if (Cache["ApplicationsNew"]==null) 
			{
				// .Modified by Cognizant Authentication changed to Insurance
				Dt = DataConnection.LookupDataSet("Insurance", "GetApplications").Tables[0];
				if (Dt.Rows.Count>1) 
				{
					//AddSelectItem(Dt, "All","-1");
					DataRow Row = Dt.NewRow();
					Row["Description"]="-1";
					Row["RepDescription"]="All";
					Dt.Rows.InsertAt(Row, 0);
				}
				Cache["ApplicationsNew"]=Dt;
				BindData();
			}
			else
			{
				BindData();
			}
			
			// To Add product Types based on Application Ids
			Dt = DataConnection.LookupDataSet("Insurance", "ApplicationProductTypes",new object[] {-1}).Tables["ApplicationProductTypes"];
			if (Dt.Rows.Count>1) AddSelectItem(Dt,"All","-1");
			_ProductType.DataSource = Dt;
			_ProductType.DataBind();
			
			DataConnection.Close();
		}

		/// <summary>
		/// Binds the data to the list boxes.
		/// </summary>
		private void BindData() 
		{
			_Application.DataSource=Applications;
			_Application.DataBind();			
		}
		#endregion


		protected void btnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			try
			{
				
				dgSearch2.Dispose();
				
				dgSearch2.CurrentPageIndex = 0;

			
				dgSearch2.Visible = false;
				lblInsTitle.Visible = false;
				lblMbrTitle.Visible = false;
			
				lblErrMsg.Visible = false;
				pnlTitle.Visible = false;
             		
				start_date = this.start_date_month.SelectedItem.Value+'/'+this.start_date_day.SelectedItem.Value+'/'+this.start_date_year.SelectedItem.Value;
				DateTime dt_start_date = DateTime.Parse(start_date);
	
				end_date = this.end_date_month.SelectedItem.Value+'/'+this.end_date_day.SelectedItem.Value+'/'+this.end_date_year.SelectedItem.Value;
				DateTime dt_end_date = DateTime.Parse(end_date).AddDays(1);		
					
				bool bValid = rptLib.ValidateDate(lblErrMsg, dt_start_date, dt_end_date, 99);
				if (bValid == true) 
				{
					InvisibleControls();
					return;
				}	
					
				if (txtCCPrefix.Text.Length > 0 || txtCCSuffix.Text.Length > 0 )
				{
					bValid = rptLib.ValidateCCNumber(lblErrMsg, txtCCPrefix.Text, txtCCSuffix.Text);
					if (bValid == true) 
					{
						InvisibleControls();
						return;
					}
					else
					{												
						PaymentClasses.CardInfo Card = new PaymentClasses.CardInfo();
						Card.CCNumber = this.txtCCPrefix.Text.ToString() + "XXXXXXXX" + this.txtCCSuffix.Text.ToString();
						sCCSignature = Card.Signature;
					}
				}
				// .Modified by Cognizant based on new SP changes
				if((txtAmount.Text.Length == 0) && (txtCCAuthCode.Text.Length == 0) && (txtCCPrefix.Text.Length == 0) && (txtCCSuffix.Text.Length == 0) && (txtLastName.Text.Length == 0) &&  (txtMerchantRefNbr.Text.Length == 0) && (txtAccountNbr.Text.Length == 0) && (txtReceiptNbr.Text.Length == 0))
				{
					rptLib.SetMessage(lblErrMsg, "Please specify a search criteria.", true);
					InvisibleControls();
					return;
				}
						
				OrderClasses.SearchCriteria srchCrit1 = new OrderClasses.SearchCriteria();
				srchCrit1.StartDate = dt_start_date;
				srchCrit1.EndDate = dt_end_date;

				//START --> Code added by COGNIZANT - 07/08/2004 - for adding Receipt Number in Search Criteria
				srchCrit1.ReceiptNbr = this.txtReceiptNbr.Text;
				//END
				//START --> Code added by COGNIZANT - 07/08/2004
				//Changed by Cognizant to search based on App_Name and Description
				/* Modified by Cognizant on Feb 1 2005 to search based on App_Name and display test as Description */
				
				//if (this._Application.SelectedItem.Text =="All")
				//	srchCrit1.App = "-1";
				//else
				//	srchCrit1.App = Convert.ToString(this._Application.SelectedItem.Text);
				srchCrit1.App = Convert.ToString(this._Application.SelectedItem.Value);
				srchCrit1.ProductCode = Convert.ToString(this._ProductType.SelectedItem.Value);
				if(this.txtAmount.Text != "")
				{
					/// .Modified by Cognizant Added for PCR 29 Offshore
					try
					{
						if(Convert.ToDecimal(this.txtAmount.Text) <= 0)
						{
							throw new FormatException();
						}
						else
						{
							// START - Code added by COGNIZANT 08/19/2004
							// To check if the input Date Range is only for a single day when a postive value of amount
							// is entered in the input search criteria
							if((dt_start_date.AddDays(1)).Equals(dt_end_date))
							{
								srchCrit1.Amount = Convert.ToDecimal(this.txtAmount.Text);
							}
							else
							{
								rptLib.SetMessage(lblErrMsg, "Please limit the date range to one day for searching by Amount", true);
								InvisibleControls();
								return;
							}
							// END
						}
					}

						// START - Code added by COGNIZANT 07/08/2004 to handle invalid amount from being entered
					catch(FormatException)
					{
						rptLib.SetMessage(lblErrMsg, "Please specify a valid amount", true);
						InvisibleControls();
						return;
					}
					// END
				}
				srchCrit1.MerchantRef = this.txtMerchantRefNbr.Text;
				//END
				// .Modified by Cognizant based on new SP Changes
				srchCrit1.AccountNbr = this.txtAccountNbr.Text;
				srchCrit1.LastName = this.txtLastName.Text;
				srchCrit1.CCAuthCode = this.txtCCAuthCode.Text;
				srchCrit1.CCNumber = sCCSignature;
            				
				InsuranceClasses.Service.Insurance InsSvc = new InsuranceClasses.Service.Insurance();

				DataSet ds = InsSvc.NewSearchOrders(srchCrit1);
				//.Modified by Cognizant based on new SP Changes
				if (ds.Tables[0].Rows.Count == 0) 
				{
					rptLib.SetMessage(lblErrMsg, "No Data Found. Please specify a valid criteria.", true);
					InvisibleControls();
					return;
				}
				else
				{
					VisibleControls();
				}
			
				Session["MyDataSet"] = ds;

				lblRunDate.Text = DateTime.Now.ToString();
				lblDateRange.Text = srchCrit1.StartDate + " - " + srchCrit1.EndDate.AddSeconds(-1);
				lblDate.Text = "Run Date:";
				lblDates.Text = "Date Range:";
							
				// For printing and converting to Excel only, folowing session variables are declared.
				Session["DateRange"] = dt_start_date + " - " + dt_end_date.AddSeconds(-1);

				UpdateDataView();
		
			}

	
			catch (FormatException)
			{ 
				string invalidDate = @"<Script>alert('Please put in a valid date.')</Script>"; 
				Response.Write(invalidDate); 
				return;
			}
			catch  (Exception ex) 
			{
				rptLib.SetMessage(lblErrMsg, MSCRCore.Constants.MSG_GENERAL_ERROR, true);
				Logger.Log(ex);
			}
		
		}

		// EVENT HANDLER: ItemDataBound
		public void ItemDataBound2(Object sender, DataGridItemEventArgs e)
		{
			DataRowView drv = (DataRowView) e.Item.DataItem;
			if (drv == null)
				return;

			SetNonSTARWidths(e);
			
			e.Item.Cells[7].Text = (String.Format("{0:c}",drv["Amount"])).ToString();
			e.Item.Cells[8].Text = (String.Format("{0:c}",drv["Order Total"])).ToString();

			if (drv["Credit Card Number"].ToString() != "")			
				e.Item.Cells[12].Text = Cryptor.Decrypt(drv["Credit Card Number"].ToString(),Config.Setting("CSAA_ORDERS_KEY"));
			//.Changed by Cognizant to hide Duplicate button for STAR transaction
			//CSR#3937.Ch2 - START - Changed the code for the new way of accessing the constants (web.config changes)
			//if((!Convert.IsDBNull(drv["Receipt Number"])) && (drv["Receipt Number"].ToString().Trim() != "") && (drv["Appl."].ToString().Trim() != ((StringDictionary)Application["Constants"])["ApplicationType.Star"].Trim()))

			//Q4-Retrofit.Ch1 -START
			/*Removed the condition (drv["Appl."].ToString().Trim() != Config.Setting("ApplicationType.Star")) in the IF loop
			to display the Duplicate Receipt button for the STAR CUI transactions*/

			if((!Convert.IsDBNull(drv["Receipt Number"])) && (drv["Receipt Number"].ToString().Trim() != ""))

				//Q4-Retrofit.Ch1-END 

				//CSR#3937.Ch2 - END - Changed the code for the new way of accessing the constants (web.config changes)
			
			{
				ImageButton btnDuplicateReceipt = new ImageButton();
				btnDuplicateReceipt.CausesValidation = false;
				btnDuplicateReceipt.ToolTip = "Print Duplicate Receipt for this transaction";
				btnDuplicateReceipt.ImageUrl = "\\images\\btn_dup_recpt.gif";
				//The Parameters for the javascript function Open_Receipt are Receipt number and Receipt Type(DC for Duplicate Copy)
				btnDuplicateReceipt.Attributes.Add("onclick","return Open_Receipt('" + (drv["Receipt Number"].ToString()) + "','" + "DC" + "')");
				//CSR 3824:ch1 - Modified by Cognizant on 08/04/2005 to display the Status Column in Transaction Search
				e.Item.Cells[17].Controls.Add(btnDuplicateReceipt);
				//End
			}
		}

		public void PageIndexChanged2(Object sender, DataGridPageChangedEventArgs e)
		{
			dgSearch2.CurrentPageIndex = e.NewPageIndex;
			UpdateDataView();
		}
		
		private void UpdateDataView()
		{
			
			// Retrieves the data
			DataSet ods = (DataSet) Session["MyDataSet"];

			if (ods == null)
			{
				rptLib.SetMessage(lblErrMsg,"Your session has timed out. Please re-submit the page", true);
				return;
			}
			
			//START - Code added by COGNIZANT 07/16/2004 - For adding the column to display the Duplicate 
			//	Receipt button column
			if (ods.Tables[0].Rows.Count > 0)
			{
				ods.Tables[0].Columns.Add(" ");
			}
			

			DataView dv1 = ods.Tables[0].DefaultView;
			//DataView dv2 = ods.Tables[1].DefaultView;
					
			int pageCnt  = 1;
			int currPage = 1;	
			
			if (ods.Tables[0].Rows.Count > 0)
			{
				dgSearch2.DataSource = dv1;
				dgSearch2.DataBind();
				
				//START - Code added by COGNIZANT 07/16/2004 - To remove the Duplicate Receipt button column
				//	from the dataset.
				ods.Tables[0].Columns.Remove(" ");
				//END


				dgSearch2.Visible = true;
				lblMbrTitle.Visible = true;
				lblMbrTitle.Text = "STAR Transactions and Non STAR Transactions";
				if (pageCnt < dgSearch2.PageCount) pageCnt = dgSearch2.PageCount;
				if (currPage < dgSearch2.CurrentPageIndex+1) currPage = dgSearch2.CurrentPageIndex+1;
			}
	
			lblPageNum1.Text = "Showing Page " + currPage.ToString() + " of " + pageCnt.ToString();
			lblPageNum2.Text = "Showing Page " + currPage.ToString() + " of " + pageCnt.ToString();

		}

		protected void start_date_month_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			rptLib.DynamicDate(start_date_day, start_date_month, start_date_year);

			
			//START - Code added by COGNIZANT 07/16/2004 - To retain the previous search results when
			//the selected start date month is changed.
			DataSet ods = (DataSet) Session["MyDataSet"];

			if (ods != null)
			{
				UpdateDataView();
			}
			//END
			
		}

		protected void end_date_month_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			rptLib.DynamicDate(end_date_day, end_date_month, end_date_year);
			end_date_day.SelectedIndex = end_date_day.Items.Count - 1;

			//START - Code added by COGNIZANT 07/16/2004 - To retain the previous search results when
			//the selected end date month is changed.
			DataSet ods = (DataSet) Session["MyDataSet"];

			if (ods != null)
			{
				UpdateDataView();
			}
			//END
		}


		private void VisibleControls()
		{
			pnlTitle.Visible = true;
			lblPageNum1.Visible = true;
			lblPageNum2.Visible = true;
			lblDate.Visible = true;
			lblRunDate.Visible = true;
			lblDates.Visible = true;
			lblDateRange.Visible = true;
		}
		private void InvisibleControls()
		{
			lblErrMsg.Visible = true;
			//lblErrMsg.Text = "No Data Found. Please specify a valid criteria.";
			lblPageNum1.Visible = false;
			lblPageNum2.Visible = false;
			lblDate.Visible = false;
			lblRunDate.Visible = false;
			lblDates.Visible = false;
			lblDateRange.Visible = false;
			//START - Code added by COGNIZANT 07/16/2004 - To clear the previous search results when
			//	some error is encountered in the current search
			Session.Contents.Clear();
			Session.Abandon();
			//END
		}

		
		//START - Code added by COGNIZANT 07/16/2004 - To specify widths for the Non STAR transactions datagrid		
		private void SetNonSTARWidths(DataGridItemEventArgs e)
		{
			e.Item.Cells[0].Width = 70;	//Application
			e.Item.Cells[1].Width = 50;	//Product
			e.Item.Cells[2].Width = 70;	//Policy / Member Number
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
			e.Item.Cells[15].Width = 20;// Rep DO
			//CSR 3824: ch1 Modified by Cognizant 08/05/2005 to display the Status Column in Transaction Search
			e.Item.Cells[16].Width = 73;// Status Column
			e.Item.Cells[17].Width = 130;// Duplicate Receipt button
			//END
		}
		//END

		#region Cache
		private DataTable Applications
		{
			get {return (DataTable)Cache["ApplicationsNew"];}
		}
		#endregion

		#region AddSelectItem
		/// <summary>
		/// Adds a Select Item row to the table.
		/// </summary>
		private void AddSelectItem(DataTable Dt, string Description, string ID ) 
		{
			DataRow Row = Dt.NewRow();
			Row["ID"]=ID;
			Row["Description"]=Description;
			Dt.Rows.InsertAt(Row, 0);
		}
		#endregion

			
		#region _Application_SelectedIndexChanged
		// .Modified by Cognizant Changed based on SP changes
		//		private void _Application_SelectedIndexChanged(object sender, System.EventArgs e)
		//		{
		//			// To Add product Types based on Application Ids
		//
		//			Order DataConnection = new Order(Page);
		//
		//			Dt = DataConnection.LookupDataSet("Insurance", "ApplicationProductTypes",new object[] {Convert.ToInt32(_Application.SelectedItem.Value)}).Tables["ApplicationProductTypes"];
		//			if (Dt.Rows.Count>1) AddSelectItem(Dt,"All","-1");
		//			_ProductType.DataSource = Dt;
		//			_ProductType.DataBind();
		//			DataConnection.Close();
		//
		//			//START - Code added by COGNIZANT 07/16/2004 - To retain the previous search results when
		//			//the selected start date month is changed.
		//			DataSet ods = (DataSet) Session["MyDataSet"];
		//
		//			if (ods != null)
		//			{
		//				UpdateDataView();
		//			}
		//			//END
		//		}

		#endregion

	}
}
