/*
 * History:
 * Security Defect CH1 - Added the belowcode to check if the session values is equal to "null".If so assign the  session values to context.
 * PC Phase II changes CH1 - Removed the ImageButton "btnConfirmReissue" attributes and methods.
 * PC Phase II changes CH2 - Modified the Void flow by validating APDS application to existing flow else invoke PC IDPReversal service.
 * PC Phase II changes CH3 - Modified the Back button Flow by validating from PC flow or Existing Flow.
 * PC Security Defect Fix CH1 -Added the below line to re-direct to the first page on success Turn-in
 * PC Security Defect Fix CH2 -Added the below line to re-direct to the first page on success Turn-in
 * PC Security Defect Fix CH3 -Commented the below line to re-direct to the first page on success Turn-in and to retain on the same page for failure turn-in process
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
using IDPReversal;
using System.ServiceModel;
using System.Collections.Generic;

namespace MSCR.Reports
{
	/// <summary>
	/// Summary description for TurnIn_Void_Reissue_Confirmation.
	/// Created by Cognizant on 08/04/2005 - Void and Reissue Confirmation page.
	/// </summary>
	public partial class TurnIn_Void_Reissue_Confirmation : CSAAWeb.WebControls.PageTemplate 
	{
		protected System.Web.UI.WebControls.ImageButton btnReissue;
		protected double dblTotal = 0; //To Calculate the Total Amount
			
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if(!IsPostBack)
			{
				//For displaying the ReceiptDetails
				OrderClasses.ReportCriteria ReceiptCrit1 = new OrderClasses.ReportCriteria();

				if(Session["ReissueInfo"]!= null)
				{
					btnConfirmVoid.Visible = false; 
					tblVoid.Visible = false;
					ReceiptCrit1 = (OrderClasses.ReportCriteria)Session["ReissueInfo"];
					trVoidMessage.Visible = false;

				}
				else if(Session["VoidInfo"]!= null)
				{
                    //PC Phase II changes CH1 - Removed the ImageButton "btnConfirmReissue" attributes and methods.
					//btnConfirmReissue.Visible = false; 
					tblReissue.Visible = false;
					ReceiptCrit1 = (OrderClasses.ReportCriteria)Session["VoidInfo"];
					trVoidMessage.Visible = true;  
				}
					
				if((Session["VoidInfo"]!= null)||(Session["ReissueInfo"]!=null))
				{
					TurninClasses.Service.Turnin InsSvc = new TurninClasses.Service.Turnin();

					DataSet dsReceiptDetails =InsSvc.GetReceiptDetails(ReceiptCrit1);
					DataTable dtbReceiptDetails = dsReceiptDetails.Tables[0];

					if (dtbReceiptDetails.Rows.Count > 0)
					{
						
						lblPaymentType.Text = dtbReceiptDetails.Rows[0]["PaymentName"].ToString();  
						//Update the Datatable with the Receipt Changes Performed during Reissue
						if(Session["ReissueInfo"]!=null)UpdateReceiptInfo(ref dtbReceiptDetails,ReceiptCrit1);
						DataView dv = dtbReceiptDetails.DefaultView;
						ReceiptsRepeater.DataSource = dv;
						ReceiptsRepeater.DataBind();
						
					}
				}


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
        //Begin PC Phase II changes CH1 - Removed the ImageButton "btnConfirmReissue" attributes and methods.
		/* #region btnConfirmReissue_Click
		protected void btnConfirmReissue_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			//Calls the Receipt_Reissue SP
			OrderClasses.ReportCriteria ReceiptCrit1 = (OrderClasses.ReportCriteria)Session["ReissueInfo"];  
			TurninClasses.Service.Turnin TurninService = new TurninClasses.Service.Turnin();
			string strNewReceiptNumber = "";
			strNewReceiptNumber = TurninService.ReceiptReIssue(ReceiptCrit1);

			if (strNewReceiptNumber.Length > 0)
			{
				//Clear all the sessions when the operation is complete.
				Session["ReissueInfo"] = null;
				Session["VoidInfo"] = null;
				Session["PaymentType"] = null;
				Response.Redirect ("Reissue_Confirmation.aspx?ReceiptNumber=" + strNewReceiptNumber.ToString(),true);
			}
			else
			{
				InsRptLibrary InsMessage = new InsRptLibrary();
				InsMessage.SetMessage(lblErrMsg,"Reissue Failed",true);
				lblErrMsg.Visible = true;
			}
		
		}
		#endregion
        */
        //End PC Phase II changes CH1 - Removed the ImageButton "btnConfirmReissue" attributes and methods.
		#region btnConfirmVoid_Click
		protected void btnConfirmVoid_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
		
			//Calls the Receipt_Void SP
			OrderClasses.ReportCriteria ReceiptCrit1 = (OrderClasses.ReportCriteria)Session["VoidInfo"];  
			TurninClasses.Service.Turnin TurninService = new TurninClasses.Service.Turnin();
            //Begin PC Phase II changes CH2 - Modified the Void flow by validating APDS application to existing flow else invoke PC IDPReversal service.           
            string Appname = TurninService.PCVoidFlowCheck(ReceiptCrit1.ReceiptNumber);
            if (Appname.ToUpper().Equals(CSAAWeb.Constants.PC_APPID_PAYMENT_TOOL))
            {
			int NoOfRowsAffected= TurninService.ReceiptVoid(ReceiptCrit1);
			if(NoOfRowsAffected > 0)
			{
				//Clear all the sessions when the operation is complete.
				Session["ReissueInfo"] = null;
				Session["VoidInfo"] = null;
				Session["PaymentType"] = null;
                //PC Security Defect Fix CH1 -Added the below line to re-direct to the first page on success Turn-in
                Response.Redirect("SalesTurnIn_Report.aspx");
				
			}
			else
			{
				InsRptLibrary InsMessage = new InsRptLibrary();
				InsMessage.SetMessage(lblErrMsg,"Void Failed",true);
				lblErrMsg.Visible = true;
			}
		}
            else
            {
                List<string> IDPReversal_Response = new List<string>();
                string msg = "";
                OrderClassesII.IssueDirectPaymentWrapper IDPWrapper = new OrderClassesII.IssueDirectPaymentWrapper();
                IDPReversal_Response = IDPWrapper.IDPReversal(ReceiptCrit1.ReceiptNumber, CSAAWeb.Constants.PC_VOID_STATUS,Page.User.Identity.Name.ToString());
                if (IDPReversal_Response.Count > 0)
                {
                    Session["BackBtnFlow"] = "PCONLINE";
                    btnConfirmVoid.Enabled = false;
                    A1.Disabled = true;
                    if (IDPReversal_Response[0].ToString().Equals("SUCC"))
                    {
                        msg = "The receipt number " + ReceiptCrit1.ReceiptNumber + " has been voided. The voided receipt number is " + IDPReversal_Response[1].ToString();
                        InsRptLibrary InsMessage = new InsRptLibrary();
                        InsMessage.SetMessage(lblErrMsg, msg, true);
                        lblErrMsg.Visible = true;
                        //PC Security Defect Fix CH2 -Added the below line to re-direct to the first page on success Turn-in
                        Response.Redirect("SalesTurnIn_Report.aspx");
                    }
                    else
                    {
                        msg = IDPReversal_Response[0] + " " + IDPReversal_Response[1];
                        InsRptLibrary InsMessage = new InsRptLibrary();
                        InsMessage.SetMessage(lblErrMsg, msg, true);
                        lblErrMsg.Visible = true;
                    }
                }
                
            }
            //PC Security Defect Fix CH3 -Commented the below line to re-direct to the first page on success Turn-in and to retain on the same page for failure turn-in process
            //Response.Redirect("SalesTurnIn_Report.aspx");
            VoidAck.Visible = false;
            lblVoidAck.Visible = false;
            

            //End PC Phase II changes CH2 - Modified the Void flow by validating APDS application to existing flow else invoke PC IDPReversal service.
		}
		#endregion

		#region btnBack_Click
		protected void btnBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			OrderClasses.ReportCriteria ReceiptCrit1;
           //Begin PC Phase II changes CH3 - Modified the Back button Flow by validating from PC flow or Existing Flow.
            if (Session["BackBtnFlow"] != null)
            {
                string flow = (string)Session["BackBtnFlow"];
                if(flow.Equals("PCONLINE"))
                {
                    Session["BackBtnFlow"] = null;
                    Response.Redirect("SalesTurnIn_Report.aspx");
                }
            }
            else
            {
           //End PC Phase II changes CH3 - Modified the Back button Flow by validating from PC flow or Existing Flow.
			if(Session["ReissueInfo"]!= null)
			{
				ReceiptCrit1 = (OrderClasses.ReportCriteria)Session["ReissueInfo"];  
				Response.Redirect("TurnIn_Void_Reissue_Receipt.aspx?ReceiptNumber="+ReceiptCrit1.ReceiptNumber); 
			}
			else if(Session["VoidInfo"]!= null)
			{
				ReceiptCrit1 = (OrderClasses.ReportCriteria)Session["VoidInfo"];  
				Response.Redirect("TurnIn_Void_Reissue_Receipt.aspx?ReceiptNumber="+ReceiptCrit1.ReceiptNumber); 
                }
			}
 		
		}
		#endregion

		#region UpdateReceiptInfo

		///Update the Datatable with the Receipt Information Changes.
		///This is only performed for Reissue operation, since updated information will be
		///shown in the Reissue confirmation page
		///Added by Cognizant on 09/23/2005 Error Fix in Reissue Confirmation Page
		private void UpdateReceiptInfo(ref DataTable dtReceiptDetails,OrderClasses.ReportCriteria ReportCrit1)
		{
			string [] ItemIDs = ReportCrit1.ItemIDs.Split(',');
			if(ReportCrit1.Amounts != "")
			{
				string [] Amounts = ReportCrit1.Amounts.Split(',');   
				for(int i=0;i<ItemIDs.Length;i++)
				{
					DataRow [] dr = dtReceiptDetails.Select("ItemID ="+ItemIDs[i]);
					dr[0]["ItemAmount"] = Convert.ToDecimal(Amounts[i]);
				}
			}
            //Security Defect CH1 -START- Added the belowcode to check if the session values is equal to "null".If so assign the  session values to context.

            if (Session["PaymentType"] == null)
            {
                if (Context.Items.Contains("PaymentType"))
                {
                    Session["PaymentType"] = Context.Items["PaymentType"].ToString();
                }
                
            }
            if (Session["PaymentType"] != null)
                lblPaymentType.Text = Session["PaymentType"].ToString();

            //Security Defect CH1 -End- Added the belowcode to check if the session values is equal to "null".If so assign the  session values to context.
		}
		#endregion

		#region ReceiptsRepeater_ItemDataBound

		/// <summary>
		/// The Total Amount for the ReceiptNumber is calculated here
		/// </summary>
		/// <param name="Sender"> ReceiptsRepeater</param>
		/// <param name="e">Event Aruguments</param>
		public void ReceiptsRepeater_ItemDataBound(Object Sender, RepeaterItemEventArgs e) 
		{
			string strAmount = "0";
			string strTemProduct = "";
			
			if (e.Item.ItemType == ListItemType.Item 
				|| e.Item.ItemType == ListItemType.AlternatingItem)
			{
					//To See the lblItemID Exit and set a Reference for it
					Label lblTemProduct = (Label) e.Item.FindControl("lblItemId");
					//To assign the Property value to a string
					strTemProduct = lblTemProduct.Attributes["ProductName"].ToString(); 
				if(strTemProduct.Equals("Mbr"))
				{
					if (((Label) e.Item.FindControl("lblAssocID")).Text  == "1") 
					{
						((Label)e.Item.FindControl("lblTotalAmount")).Visible = true;
						((Label)e.Item.FindControl("lblAmount")).Visible = false;
						strAmount = ((Label)e.Item.FindControl("lblTotalAmount")).Text; 
							
					}
					else
					{
						((HtmlTableRow)e.Item.FindControl("ReceiptRow")).Visible = false;
						
					}
				}
				else
				{
					strAmount = ((Label)e.Item.FindControl("lblAmount")).Text; 
				}
					
				
					
					dblTotal = dblTotal + Convert.ToDouble(strAmount.Replace("$","").Replace(",",""));
					
			}
			if (e.Item.ItemType == ListItemType.Footer)
			{
				Label lblTotal = (Label) e.Item.FindControl("lblTotal");
				lblTotal.Text = dblTotal.ToString("$##,##0.00");
				return;
			}
		}
		#endregion
        		
	}
}
