// SSO Integration - CH1 -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
// PC Phase II - CH1 - Modified the code to update the PC Void Flow change. 
//PC Phase II - CH2 - Commented the below code to clear the session values after void/update process
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

namespace MSCR.Reports
{
	/// <summary>
	/// Summary description for Manual_Update_Search.
	/// </summary>
	public partial class Manual_Update_Search : CSAAWeb.WebControls.PageTemplate
	{
		
		string colour = ALTERNATE_ROW_CLASS;
		const string ROW_CLASS = "item_template_row";
		const string ALTERNATE_ROW_CLASS = "item_template_alt_row";
		

		string tmpreceiptnum ="";
        
		#region PageLoad
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			lblUpdateMsg.Visible = false;
			imgUpdateFlag.Visible = false;
            // SSO Integration - CH1 Start -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            // SSO Integration - CH1 End -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
			if(!IsPostBack)
			{
				lblErrMsg.Visible = false;
				tblResultPane.Visible = false;
				dgSearchResult.Visible = false;
				
				if( Session["Visited"] != null)
				{
					if( Session["Visited"].ToString() == "Yes")
					{
                        //PC Phase II - CH2 START- Commented the below code to clear the session values after void/update process
                        //if (Session["ReceiptNumber"] != null)
                        //    txtReceiptNbr.Text = Session["ReceiptNumber"].ToString();
                        //if (Session["AccountNumber"] != null)
                        //    txtAccountNbr.Text = Session["AccountNumber"].ToString();
                        //btnSubmit_Click(null, null);
						//PC Phase II - CH2 END- Commented the below code to clear the session values after void/update process
						if(Session["Updated"].ToString()=="Yes")
						{
                            if (Session["SelectedReceiptNumber"] != null)
                            {
                                //PC Phase II - CH1- Start - Modified the code to update the PC Void Flow change
                                if (Session["SelectedReceiptNumber"].ToString().Contains("-"))
                                {
                                    string receipt= (Session["SelectedReceiptNumber"].ToString()).Split('-')[0];
                                    string voidReceipt = (Session["SelectedReceiptNumber"].ToString()).Split('-')[1];
                                    lblUpdateMsg.Text = "The receipt number " + receipt + " has been voided. The voided receipt number is " + voidReceipt;
                                }
                                //PC Phase II - CH1- End - Modified the code to update the PC Void Flow change
                                else
                                {
                                    lblUpdateMsg.Text = "Receipt Number " + Session["SelectedReceiptNumber"] + " has been updated";
                                }
                            }
							lblUpdateMsg.Visible = true;
							imgUpdateFlag.Visible = true;
						}
										
						Session["Updated"] = "No";
					}
					Session["Visited"] = "No";

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
            this.dgSearchResult.RowCommand += new System.Web.UI.WebControls.GridViewCommandEventHandler(this.dgSearchResult_ItemCommand);
            this.dgSearchResult.RowDataBound += new System.Web.UI.WebControls.GridViewRowEventHandler(this.dgSearchResult_ItemDataBound);
            this.btnSubmit.Click += new System.Web.UI.ImageClickEventHandler(this.btnSubmit_Click);
           

		}
		#endregion

		#region btnSubmit_Click
		protected void btnSubmit_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			tblResultPane.Visible = false;
			if((txtReceiptNbr.Text.Trim() == "") && (txtAccountNbr.Text.Trim() == ""))
			{
				lblErrMsg.Text = "Please specify a search criteria.";
				lblErrMsg.Visible = true;
				return;
			}
			else
			{
				lblErrMsg.Visible = false;
			}
			OrderClasses.SearchCriteria srchCrit1 = new OrderClasses.SearchCriteria();
			srchCrit1.ReceiptNbr = txtReceiptNbr.Text.Trim();
			srchCrit1.AccountNbr = txtAccountNbr.Text.Trim();
			
			srchCrit1.EndDate = DateTime.Today;
			srchCrit1.StartDate = DateTime.Parse("1/1/1900");
					
			InsuranceClasses.Service.Insurance InsSvc = new InsuranceClasses.Service.Insurance();
			//DataSet ds = InsSvc.NewSearchOrders(srchCrit1);
			DataSet ds = InsSvc.ManualUpdateSearch(srchCrit1);

			if (ds.Tables[0].Rows.Count == 0) 
			{
				//rptLib.SetMessage(lblErrMsg, "No Data Found. Please specify a valid criteria.", true);
				//InvisibleControls();
				lblErrMsg.Text = "No Data Found. Please specify a valid criteria.";
				lblErrMsg.Visible = true;
				return;
			}
			else
			{
				dgSearchResult.DataSource = ds.Tables[0];
				dgSearchResult.DataBind();
				lblErrMsg.Visible = false;
				tblResultPane.Visible = true;
				dgSearchResult.Visible = true;
			}
		}
		#endregion

		#region ItemDataBound
		private void dgSearchResult_ItemDataBound(object sender, GridViewRowEventArgs e)
		{
            
			Decimal stTemp;

            if (e.Row.RowType == DataControlRowType.DataRow)
            { 
                
            stTemp = Convert.ToDecimal(e.Row.Cells[6].Text.ToString());
            e.Row.Cells[6].Text = stTemp.ToString("$##0.00");

            string receiptnum = e.Row.Cells[4].Text;
           
				if(receiptnum!=tmpreceiptnum)
				{
					AssignClass(colour,e);
				}
				else
				{
                    e.Row.Cells[10].Controls.Remove(e.Row.FindControl("btnManualUpdate"));
					e.Row.Attributes.Add("class",colour);
				}
				
				tmpreceiptnum = receiptnum;
               //ImageButton btnManualUpdate = new ImageButton();
               //btnManualUpdate.CommandName = "Click";
               //btnManualUpdate.CausesValidation = false;
               //// btnDuplicateReceipt.ToolTip = "Print Duplicate Receipt for this transaction";
               //btnManualUpdate.ImageUrl = "\\images\\btn_manual_update.gif";
               // //The Parameters for the javascript function Open_Receipt are Receipt number and Receipt Type(DC for Duplicate Copy)
               // //btnDuplicateReceipt.Attributes.Add("onclick", "return Open_Receipt('" + (drv["Receipt Number"].ToString()) + "','" + "DC" + "')");
               //btnManualUpdate.Attributes.Add("onclick", "return dgSearchResult_ItemCommand() ");
               // //CSR 3824:ch1 - Modified by Cognizant on 08/04/2005 to display the Status Column in Transaction Search
               // //STAR Retrofit III.Ch4: START - Modified the cell index of DuplicateReceipt column.
               //e.Row.Cells[10].Controls.Add(btnManualUpdate);		
            }
 
			
		}
		#endregion
		
		#region AssignClass
		/// <summary>
		/// Swapping of the colour
		/// </summary>
        private void AssignClass(string tmpcolour, GridViewRowEventArgs e)
		{
			if ( tmpcolour == ROW_CLASS )
			{
				e.Row.Attributes.Add("class",ALTERNATE_ROW_CLASS);
				colour = ALTERNATE_ROW_CLASS;
			}
			else if ( tmpcolour == ALTERNATE_ROW_CLASS )
			{
                e.Row.Attributes.Add("class", ROW_CLASS);
				colour = ROW_CLASS;
			}
		}
		#endregion

		#region ItemCommand
        private void dgSearchResult_ItemCommand(object source, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Click")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow grItem = dgSearchResult.Rows[index];
                if (grItem.RowType == DataControlRowType.DataRow)
                {
                    Session["SelectedReceiptNumber"] = grItem.Cells[4].Text;
                }
                Session["ReceiptNumber"] = txtReceiptNbr.Text;
                Session["AccountNumber"] = txtAccountNbr.Text;

                Response.Redirect("Manual_Update.aspx", false);
            }


        
		}
        

		#endregion 

	}
}
