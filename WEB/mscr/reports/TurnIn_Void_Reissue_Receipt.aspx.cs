/*
 * History:
 * 11/11/2004 JOM Replaced InsuranceClasses.Service.Insurance with TurninClasses.Service.Turnin.
 * Modified by Cognizant on 08/23/2005 for introducing the new Reissue Confirmation Page
 * The Reissue and Void Parameters are copied in to Session and it will be fetched in the 
 * Void/Reissue Confirmation Page
 * Security Defect CH1,CH2 - Added the belowcode to check if the session values is equal to "null".If so assign the  session values to context.
 * // SSO Integration - CH1 -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
 * PC Phase II changes CH1 - Removed the HtmlInputText field "txtAmount" to Label "lblItemAmount" and modified the validations for it.
 * PC Phase II changes CH2 - Removed the ImageButton "btnReissue". So commented the click event method.
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

namespace MSCR.Reports
{
	/// <summary>
	/// The Screen is used to Void or Reissue a Selected ReceiptNumber
	/// CSR 3824-ch1 Modified by Cognizant on 08/30/2005
	/// Update the Datatable with the Receipt Changes Performed during the previous Reissue
	/// the Session will have the Reissue Info and when the user returns back from Void/Reissue Confirmation page
	/// the previous reissue information will be populated back.
	/// </summary>
	public partial class TurnIn_Void_Reissue_Receipt : CSAAWeb.WebControls.PageTemplate 
	{
		protected System.Web.UI.WebControls.Label lblItemId;
        protected System.Web.UI.WebControls.Label lblTotalAmount;
		
		protected string ColumnFlag = "W";				
		protected string strReceiptNumber ="";
		
		protected double dblTotal = 0; //To Calculate the Total Amount


		#region setSelectedIndex
		/// <summary>
		/// To Set the PaymentType of the ReceiptNumber to the PaymentType DropdownList
		/// </summary>
		/// <param name="cboDropDown">DropdownList Name</param>
		/// <param name="Value">The Payment_Type_Id that is to be assigned as selected Item</param>
		private void setSelectedIndex(System.Web.UI.WebControls.DropDownList cboDropDown,string Value)
		{
			int i =0;
			int Count = 0;
			Count = cboDropDown.Items.Count;
			while (i<Count)
			{
				if (Value == cboDropDown.Items[i].Value)
				{
					cboDropDown.SelectedIndex = i;
					return ;
				}
				i = i + 1;
			}
		}
		#endregion

		#region ReceiptsRepeater_ItemDataBound

		/// <summary>
		/// when each felds bind their value the Procedure is called
		/// This Procedure check for member and Turnin Item and 
		/// if the Item is Member then the Amount is not editable
		/// if the item is Turnin then the amount is assigned to a textbox to edit
		/// The Total Amount for the ReceiptNumber is calculated here
		/// </summary>
		/// <param name="Sender"> ReceiptsRepeater</param>
		/// <param name="e">Event Aruguments</param>
		public void ReceiptsRepeater_ItemDataBound(Object Sender, RepeaterItemEventArgs e) 
		{
			// This event is raised for the header, the footer, separators, and items.

			string strTemProduct = "";
		
			// Execute the following logic for Items and Alternating Items.
			if (e.Item.ItemType == ListItemType.Item 
				|| e.Item.ItemType == ListItemType.AlternatingItem
				|| e.Item.ItemType == ListItemType.Footer)
			{

				if (e.Item.ItemType == ListItemType.Footer)
				{
					Label lblTotal = (Label) e.Item.FindControl("lblTotal");
					lblTotal.Text = dblTotal.ToString("####0.00");
					return;
				}

				//To See the lblItemID Exit and set a Reference for it
				Label lblTemProduct = (Label) e.Item.FindControl("lblItemId");
				//To assign the Property value to a string
				strTemProduct = lblTemProduct.Attributes["ProductName"].ToString();  
                //Begin PC Phase II changes CH1 - Removed the HtmlInputText field "txtAmount" to Label "txtAmount" and modified the validations for it.
                /*
				if (e.Item.ItemType != ListItemType.Footer)
				{		
					RequiredFieldValidator vldTemp = (RequiredFieldValidator) e.Item.FindControl("vldPolicyAmount");
					RegularExpressionValidator revTemp = (RegularExpressionValidator) e.Item.FindControl("revPolicyAmount");
					//Assign the Error messages to Validators for Policy Amount.
					
					vldTemp.ErrorMessage = "Enter Amount for " + strTemProduct+" Product";
					revTemp.ErrorMessage = "Enter Valid Amount for "+ strTemProduct+" Product";
					//Assign the Regular Expression to Regular Expression Validator
					revTemp.ValidationExpression = @"(?!^0*$)(?!^0*\.0*$)^\d{1,7}(\.\d{1,2})?$";
				}
                 */
                //End PC Phase II changes CH1 - Removed the HtmlInputText field "txtAmount" to Label "txtAmount" and modified the validations for it.
				string strAmount = "0";
				//Check the Item for Member.if the item is member the amount is not editable
				//so assign it to the label 
				//Else assign it to the textbox so that the user can edit the amount
                //PC Phase II changes CH1 - Removed the HtmlInputText field "txtAmount" to Label "lblItemAmount" and modified the validations for it.
                ((Label)e.Item.FindControl("lblItemAmount")).Text = Convert.ToDouble(((Label)e.Item.FindControl("lblItemAmount")).Text).ToString("####0.00");
                ((Label)e.Item.FindControl("lblTotalAmount")).Text = Convert.ToDouble(((Label)e.Item.FindControl("lblTotalAmount")).Text).ToString("####0.00");
				if (strTemProduct.Equals("Mbr")) 						
				{
					if (((Label) e.Item.FindControl("lblAssocID")).Text  == "1") 
					{
                        //PC Phase II changes CH1 - Removed the HtmlInputText field "txtAmount" to Label "txtAmount" and modified the validations for it.
                        ((Label)e.Item.FindControl("lblItemAmount")).Visible = false;
                        strAmount = ((Label)e.Item.FindControl("lblTotalAmount")).Text;
						dblTotal = dblTotal + Convert.ToDouble(strAmount.Replace("$","").Replace(",",""));	
					}
					else
					{
						((HtmlTableRow)e.Item.FindControl("ReceiptRow")).Visible = false;
					}
				}
				else
				{
                    //PC Phase II changes CH1 - Removed the HtmlInputText field "txtAmount" to Label "lblItemAmount" and modified the validations for it.
                    strAmount = ((Label)e.Item.FindControl("lblItemAmount")).Text;
                    ((Label)e.Item.FindControl("lblTotalAmount")).Visible = false;
					dblTotal = dblTotal + Convert.ToDouble(strAmount.Replace("$","").Replace(",",""));
				}
			}
		}
		#endregion

		#region Page_Load

		/// <summary>
		/// In the Page Load Dropdownlist for PaymentType is added by calling the GetPaymentType in Payment WebService
		/// The Received ReceiptNumber is Passed to the ReportCriteria and the GetReceiptdetails is Called 
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
            // SSO Integration - CH1 Start -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            // SSO Integration - CH1 End -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
			lblErrMsg.Visible = false;
			if (!Page.IsPostBack)
			{
				//for adding the PaymentType to the PaymentType dropdownlist 
				Order DataConnection = new Order(Page);
				DataTable dtbPaymentType;
				dtbPaymentType = DataConnection.LookupDataSet("Payment", "GetPaymentType",new object[] {ColumnFlag,User.Identity.Name}).Tables["PAY_Payment_Type"];
				PaymentType.DataSource= dtbPaymentType ;
				PaymentType.DataBind();
				//For displaying the ReceiptDetails
				OrderClasses.ReportCriteria ReceiptCrit1 = new OrderClasses.ReportCriteria();
				strReceiptNumber = Request.QueryString["ReceiptNumber"];
				ReceiptCrit1.ReceiptNumber = strReceiptNumber;
				TurninClasses.Service.Turnin InsSvc = new TurninClasses.Service.Turnin();
				DataSet dsReceiptDetails =InsSvc.GetReceiptDetails(ReceiptCrit1);
				DataTable dtbReceiptDetails = dsReceiptDetails.Tables[0];
				if (dtbReceiptDetails.Rows.Count > 0)
				{
					lblReceiptDate.Text = dtbReceiptDetails.Rows[0]["ReceiptDate"].ToString();
					lblReceiptNumber.Text = dtbReceiptDetails.Rows[0]["ReceiptNumber"].ToString();
					lblUsers.Text = dtbReceiptDetails.Rows[0]["UFirstName"].ToString() + ", " + dtbReceiptDetails.Rows[0]["ULastName"].ToString() + " ("  + dtbReceiptDetails.Rows[0]["RepID"].ToString() + ") - " + dtbReceiptDetails.Rows[0]["UserName"].ToString() ;
					lblPaymentMethod.Text = dtbReceiptDetails.Rows[0]["PaymentName"].ToString();
					//START CSR 3824-ch1  - Added by Cognizant on 08/30/2005 to
					//Update the Datatable with the Receipt Changes Performed during Reissue
					if(Session["ReissueInfo"]!=null)
					{
					 //Modified on 09/13/2005 to check whether the ReceiptNumber in the Session and Querystring are simillar
					 if(((OrderClasses.ReportCriteria)Session["ReissueInfo"]).ReceiptNumber == strReceiptNumber)
						 UpdateReceiptInfo(ref dtbReceiptDetails,(OrderClasses.ReportCriteria)Session["ReissueInfo"],dtbPaymentType);
					 else
					 {
						 Session["ReissueInfo"] = null;
						 Session["VoidInfo"] = null;
						 Session["PaymentType"]=null;
					 }
					}		

					//END
					setSelectedIndex(PaymentType, dtbReceiptDetails.Rows[0]["PaymentID"].ToString());

					DataView dv = dtbReceiptDetails.DefaultView;
					ReceiptsRepeater.DataSource = dv;
					ReceiptsRepeater.DataBind();
				}
			}
		}
		#endregion

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

		#region btnReissue_Click
        //Begin PC Phase II changes CH2 - Removed the ImageButton "btnReissue". So commented the click event method.
        /*
		/// <summary>
		/// When the ReIssue button is Clicked 
		/// the ItemId and the Corresponding amount displayed for Turnin Item are Passed 
		/// to the ReportCriteria as Comma Separated values
		/// The PaymentType Selected in the Dropdownlist,UserName and the ReceiptNumber are also passed
		/// ReceiptReissue is Called to the Reissue the given ReceiptNumber
		/// </summary>
		protected void btnReissue_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Check ReceiptNumber Exist
			if (lblReceiptNumber.ToString().Length > 0) 
			{	
				string strItemIDs = "";
				string strAmounts = "" ;
				foreach(System.Web.UI.WebControls.RepeaterItem rptrItem in ReceiptsRepeater.Items )
				{
					Label lblItemID = (Label)rptrItem.FindControl("lblItemId");

					//if the item is not member Item then the Amount and the itemId are added to the string
					if (!lblItemID.Attributes["ProductName"].ToString().Equals("Mbr"))
					{
						Label txtAmount = (Label) rptrItem.FindControl("txtAmount");
						strItemIDs = strItemIDs + lblItemID.Attributes["ItemNo"].ToString() + ",";
						strAmounts = strAmounts + txtAmount.Text.Replace("$","").Replace(",","") + ",";
					}	
				}

				//to replace the last comma in the strItemIds and strAmounts
				if (strItemIDs.Length > 0 && strAmounts.Length >0)
				{
					strItemIDs = strItemIDs.Substring(0,strItemIDs.Length -1);	
					strAmounts = strAmounts.Substring(0,strAmounts.Length -1);	
				}
				//Assign the Parameters to the ReportCriteria
                OrderClasses.ReportCriteria ReceiptCrit1 = new OrderClasses.ReportCriteria();
				ReceiptCrit1.CurrentUser = this.User.Identity.Name; 
				ReceiptCrit1.ReceiptNumber = lblReceiptNumber.Text;
				ReceiptCrit1.PaymentType = PaymentType.Items[PaymentType.SelectedIndex].Value;
				ReceiptCrit1.Amounts = strAmounts;
				ReceiptCrit1.ItemIDs = strItemIDs;
				//START Modified by Cognizant on 08/23/2005 to Carry the Reissue information to the Confirmation Page
				Session["ReissueInfo"] = ReceiptCrit1;  
				Session["VoidInfo"] = null;
                //Security Defect CH2 - Added the belowcode to assign the  session values to context.                   
				Session["PaymentType"] = PaymentType.Items[PaymentType.SelectedIndex].Text;
                Context.Items.Add("PaymentType", Session["PaymentType"]);
				Response.Redirect("TurnIn_Void_Reissue_Confirmation.aspx",true); 	
				//END
			}
		}
         **/
        //End PC Phase II changes CH2 - Removed the ImageButton "btnReissue". So commented the click event method.
		#endregion

		#region btnVoid_Click

		/// <summary>
		/// When the Void button is Clicked 
		/// UserName and the ReceiptNumber are passed to the Report Criteria
		/// ReceiptVoid is Called to the Void the given ReceiptNumber
		/// </summary>

		protected void btnVoid_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if (lblReceiptNumber.ToString().Length > 0) 
			{
				//Assign the Parameters to the ReportCriteria
				OrderClasses.ReportCriteria ReceiptCrit1 = new OrderClasses.ReportCriteria();
				ReceiptCrit1.CurrentUser = this.User.Identity.Name; 
				ReceiptCrit1.ReceiptNumber = lblReceiptNumber.Text;
				//START Modified by Cognizant on 08/23/2005 to Carry the Void information to the Confirmation Page
				Session["VoidInfo"] = ReceiptCrit1; 
				Session["ReissueInfo"] = null;  				
				Response.Redirect("TurnIn_Void_Reissue_Confirmation.aspx",true);
				//END
			}
		}
		#endregion
		
		#region UpdateReceiptInfo

		///Added by Cognizant on 08/30/2005 to Populate the Receipt Details for Reissue
		///Update the Datatable with the Receipt Information Changes.
		///This is only performed for Reissue operation, since updated information will be
		///shown in the Reissue confirmation page
		///
		private void UpdateReceiptInfo(ref DataTable dtReceiptDetails,OrderClasses.ReportCriteria ReportCrit1,DataTable dtPaymentType )
		{
			string [] ItemIDs = ReportCrit1.ItemIDs.Split(',');
			string [] Amounts = ReportCrit1.Amounts.Split(',');   
			if(ReportCrit1.Amounts != "")
			for(int i=0;i<ItemIDs.Length;i++)
			{
				DataRow [] dr = dtReceiptDetails.Select("ItemID ="+ItemIDs[i]);
				dr[0]["ItemAmount"] = Convert.ToDecimal(Amounts[i]);
				
			}
			foreach(DataRow drReceipt in dtReceiptDetails.Rows)
			{
                    
				drReceipt["PaymentName"] =  Session["PaymentType"].ToString();
				//Fetch the PaymentID-we have the PaymentName already in Session
				//Security Defect CH1 --START- Added the belowcode to assign the  session values to context.                   
                    if (Session["PaymentType"] == null)
                    {
                        Session["PaymentType"] = PaymentType.Items[PaymentType.SelectedIndex].Text;
                        Context.Items.Add("PaymentType", Session["PaymentType"]);
                        
                    }
                // Security Defect CH1 -END- Added the belowcode to assign the  session values to context.
				DataRow [] drPaymentType =dtPaymentType.Select("Description = '"+Session["PaymentType"].ToString()+"'");  
				drReceipt["PaymentID"]	= drPaymentType[0]["ID"];
			}
		}
		#endregion

	}
}
