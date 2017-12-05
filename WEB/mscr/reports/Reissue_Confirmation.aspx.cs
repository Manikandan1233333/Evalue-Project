/*
 * History:
 * 11/11/2004 JOM Replaced InsuranceClasses.Service.Insurance with TurninClasses.Service.Turnin.
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
using CSAAWeb.WebControls;
using CSAAWeb.AppLogger;
//CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016

namespace MSCR.Reports
{
	/// <summary>
	/// Summary description for Reissue Confirmation.
	/// </summary>
	public partial class Reissue_Confirmation:CSAAWeb.WebControls.PageTemplate 
	{







		protected int Count = 4;
		protected double dblTotal = 0;
		protected string strReceiptNumber=""; 
		protected string strMbrIns = null;
		protected string strCardType = null;

		/// <summary>
		/// To Calculate Amount and to identify Associate and Primary Memember
		/// </summary>
		public void ReceiptsRepeater_ItemDataBound(Object Sender, RepeaterItemEventArgs e) 
		{
				// This event is raised for the header, the footer, separators, and items.
				string strTemProduct = "";

				// Execute the following logic for Items and Alternating Items.
				if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem || e.Item.ItemType == ListItemType.Footer)
				{
					if (e.Item.ItemType == ListItemType.Footer)
					{
						Label lblTotal = (Label) e.Item.FindControl("lblTotal");
						lblTotal.Text = dblTotal.ToString("$##,##0.00");
						return;
					}
					
  					//To See the lblItemID Exit and set a Reference for it
					Label lblTemProduct = (Label) e.Item.FindControl("lblItemId");

					//To assign the Property value to a string
					strTemProduct = lblTemProduct.Attributes["ProductName"].ToString();  
					string sAssocID = lblTemProduct.Attributes["AssocID"].ToString();  
					string strAmount = ((Label)e.Item.FindControl("lblAmount")).Text;
					string strTransType = lblTemProduct.Attributes["TransType"].ToString();
					
					if (strTemProduct.Equals("Mbr")) 						
					{

						//Modified by Cognizant on 4-25-05
						//to display the confirmation message properly after re-issuing 
						if (strMbrIns == null)
							strMbrIns = "The membership ";
						else if(strMbrIns.IndexOf("membership") < 0)
							strMbrIns = strMbrIns + " / membership ";
												
						//else if (strMbrIns != "The membership ")
						//	strMbrIns = strMbrIns + " / insurance ";

						if(sAssocID == "1")
						{



							tblViewCards.Visible = true;
							tblMember.Visible = true;
							AssocCaptionRow.Visible = false;
							AssocLastRow.Visible = false;

							lblName.Text = lblTemProduct.Attributes["FirstName"].ToString() + ' ';
							if(lblTemProduct.Attributes["MiddleName"].ToString().Length > 0)
								lblName.Text = lblName.Text + lblTemProduct.Attributes["MiddleName"].ToString() + ". ";
							lblName.Text = lblName.Text + lblTemProduct.Attributes["LastName"].ToString();

							lblAddress.Text = "";
							if(lblTemProduct.Attributes["Address1"].ToString().Length > 0) 
								lblAddress.Text = lblTemProduct.Attributes["Address1"].ToString() + ' ';
							if (lblTemProduct.Attributes["Address2"].ToString().Length > 0) 
								lblAddress.Text = lblAddress.Text + lblTemProduct.Attributes["Address2"].ToString();							

							lblCityStateZip.Text = lblTemProduct.Attributes["City"].ToString() + " , " ;
							lblCityStateZip.Text = lblCityStateZip.Text  + lblTemProduct.Attributes["State"].ToString() + ' ';
							lblCityStateZip.Text = lblCityStateZip.Text  + lblTemProduct.Attributes["ZipCode"].ToString();

							lblPhone.Text = lblTemProduct.Attributes["DayPhone"].ToString();
							lblDob.Text = Convert.ToDateTime(lblTemProduct.Attributes["Dob"].ToString()).ToString("MM/dd/yyyy");

							//Generate the CheckSum digit for Membership ID
							string strMembershipID=  lblTemProduct.Attributes["MemberID"].ToString();
                            //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016  
							//dtbReceiptDetails.Rows[0]["PolicyNumber"]= dtbReceiptDetails.Rows[0]["PolicyNumber"].ToString().Replace(" ","")+CSAAWeb.Cryptor.CheckDigit(strID[0].ToString()+strMembershipID);   
                            //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016

							strAmount = ((Label)e.Item.FindControl("lblMbrTotal")).Text;
							
							dblTotal = dblTotal + Convert.ToDouble(strAmount.Replace("$","").Replace(",",""));

							((HtmlTableRow)e.Item.FindControl("trReceiptMbrRow")).Visible = false;
							
							if (strTransType.Trim()  == "New")
								((HtmlTableRow)e.Item.FindControl("trReceiptEnrolRow")).Visible = true;

							((HtmlTableRow)e.Item.FindControl("ReceiptRow")).Visible = false;

                                 //External URL Implementation-Modified the below  code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach 
                            //          string Url = "/PaymentToolmsc/forms/AssociateCards.aspx?MemberID=" + strMembershipID ; //+ Cryptor.Encrypt(MemberOrder.OrderID.ToString(), Config.Setting("CSAA_ORDERS_KEY"));
                            //AssociateCards1.NavigateUrl = Url;
                            //AssociateCards2.NavigateUrl = Url;

						}
						else
						{

							Count = Count + 1;
							AssocCaptionRow.Visible = true;
							AssocLastRow.Visible = true;							

							Label lblAssocName = new Label();
							Label lblAssocNumber = new Label();

							lblAssocName.CssClass = "arial_11";
							lblAssocName.Visible = true;
							lblAssocName.Text = lblTemProduct.Attributes["FirstName"].ToString() + ' ';
							if(lblTemProduct.Attributes["MiddleName"].ToString().Length > 0)
								lblAssocName.Text = lblAssocName.Text + lblTemProduct.Attributes["MiddleName"].ToString() + ". ";
							lblAssocName.Text = lblAssocName.Text + lblTemProduct.Attributes["LastName"].ToString();

							lblAssocNumber.CssClass = "arial_11";
							lblAssocNumber.Visible = true;
//							lblAssocNumber.Text = lblTemProduct.Attributes["PolicyNumber"].ToString();

							//Generate the CheckSum digit for Membership ID
                            //string strMembershipID=  lblTemProduct.Attributes["MemberID"].ToString();
                            //MembershipClasses.Member MBRStartDigit = new MembershipClasses.Member();
                            //string[] strID =  MBRStartDigit.FullMemberID.Split(' ');  
							//dtbReceiptDetails.Rows[0]["PolicyNumber"]= dtbReceiptDetails.Rows[0]["PolicyNumber"].ToString().Replace(" ","")+CSAAWeb.Cryptor.CheckDigit(strID[0].ToString()+strMembershipID);   
							//lblAssocNumber.Text =  strID[0].ToString() + " " + lblTemProduct.Attributes["PolicyNumber"].ToString() + " " +  CSAAWeb.Cryptor.CheckDigit(strID[0].ToString()+strMembershipID);

							HtmlTableRow AssocRow = new HtmlTableRow();

							HtmlTableCell AssocCell1 = new HtmlTableCell(); //For Associate Name
							AssocCell1.Visible = true;
							AssocCell1.Controls.Add(lblAssocName);

							HtmlTableCell AssocCell2 = new HtmlTableCell(); //For Member Name	
							AssocCell2.Visible = true;
							AssocCell2.Attributes.Add("Class","arial_11");
							AssocCell2.InnerHtml = "<B>Membership Number:</B>&nbsp;"; 
							AssocCell2.Controls.Add(lblAssocNumber);

							AssocRow.Cells.Insert(0,AssocCell1);
							AssocRow.Cells.Insert(1,AssocCell2);
						
							tblMember.Rows.Insert(Count,AssocRow);

							((HtmlTableRow)e.Item.FindControl("ReceiptRow")).Visible = false;
						}
					}
					else
					{
						//Modified by Cognizant on 4-25-05
						//to display the confirmation message properly after re-issuing 
						if (strMbrIns == null)
							strMbrIns = "The insurance ";
						else if(strMbrIns.IndexOf("insurance") < 0)
							strMbrIns = strMbrIns + " / insurance ";

						//else if (strMbrIns != "The insurance ")
						//	strMbrIns = strMbrIns + " / membership ";

						dblTotal = dblTotal + Convert.ToDouble(strAmount.Replace("$","").Replace(",",""));
						//((Label)e.Item.FindControl("lblInAmount")).Visible = false;
						((HtmlTableRow)e.Item.FindControl("trReceiptInRow")).Visible = false;
						tblMember.Visible = false;
						tblViewCards.Visible = false;
					}
					//Modified by Cognizant on 4-21-05
					lblSuccessLabel.Text = strMbrIns + "payment has been accepted";
					//lblSuccessLabel.Text = strMbrIns + "has been updated and the " + strCardType + 
					//					" has been billed " + dblTotal.ToString("$##,###.00");
				}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			//For displaying the ReceiptDetails
			OrderClasses.ReportCriteria ReceiptCrit1 = new OrderClasses.ReportCriteria();
			strReceiptNumber = Request.QueryString["ReceiptNumber"];
			ReceiptCrit1.ReceiptNumber = strReceiptNumber;

			TurninClasses.Service.Turnin TurninService = new TurninClasses.Service.Turnin();
			DataSet dtsReceiptDetails =TurninService.GetReceiptDetails(ReceiptCrit1);
			DataTable dtbReceiptDetails = dtsReceiptDetails.Tables[0];


			if (dtbReceiptDetails.Rows.Count > 0)
			{
				strCardType = dtbReceiptDetails.Rows[0]["PaymentName"].ToString();
				DataView dv = dtbReceiptDetails.DefaultView;
				ReceiptsRepeater.DataSource = dv;
				ReceiptsRepeater.DataBind();
				//The Parameters are Receipt number and Receipt Type(CC for customer Copy, MC for Merchant Copy).
				btnCustomerReceipt.Attributes.Add("onclick","Open_Receipt('" + strReceiptNumber + "','CC'); return false;");
				btnMerchantReceipt.Attributes.Add("onclick","Open_Receipt('" + strReceiptNumber + "','MC'); return false;");
//				btnBack.Attributes.Add("onclick","location.href='SalesTurnIn_Report.aspx';");
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

		protected void btnBack_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Response.Redirect("SalesTurnIn_Report.aspx");	
		}

	}
}
