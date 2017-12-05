/*
MODIFIED BY COGNIZANT AS A PART OF TIME ZONE CHANGE ENHANCEMENT ON 8-31-2010
TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) and added MST Arizona on 8-31-2010 by cognizant. 
 *PC Phase II changes CH1 - Added the below code to check whether transaction is Payment Central or Payment Tool
 *CHG0109406 - CH1 - Modified the timezone from "MST Arizona" to Arizona
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
using System.Data.SqlTypes;
using TurninClasses.WebService;
using OrderClassesII;
using CSAAWeb;
using System.Collections.Generic;


namespace MSCR.Reports
{/// <summary>
	/// Summary description for Manual_Update.
	/// </summary>
	public partial class Manual_Update : CSAAWeb.WebControls.PageTemplate
	{
		AuthenticationClasses.Service.Authentication ua;
		protected string strRepDO;
		string strUserName;
		DataSet dsReceiptDetails;
		string currentUser;
		protected System.Web.UI.WebControls.DataGrid dgCustomerDetails;
		InsuranceClasses.Service.Insurance insObject = new InsuranceClasses.Service.Insurance();
	
		#region Page_Load
		/// <summary>
		/// Call when the page is loaded.
		/// </summary>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			currentUser = Page.User.Identity.Name;
			OrderClasses.SearchCriteria SearchFor = new OrderClasses.SearchCriteria();
			SearchFor.StartDate = Convert.ToDateTime("01/01/1900");
			SearchFor.EndDate = DateTime.Today;

			if(Session["SelectedReceiptNumber"] == null)
			{
				lblMessage.Text = "Your session has timed out. Please try again";
				lblMessage.Visible = true;
				btnUpdate.Visible = false;
			}
			else
			{
				SearchFor.ReceiptNbr = Session["SelectedReceiptNumber"].ToString();
				dsReceiptDetails = insObject.ManualUpdateSearch(SearchFor);
				if(!Page.IsPostBack)
				{
					Session["Updated"] = "No";
					Session["VoidSessionID"] = "No";

					if(dsReceiptDetails.Tables[0].Rows.Count == 0)
					{
						Response.Redirect("Manual_Update_Search.aspx");
					}
					else
					{
						lblReceiptNumber.Text= dsReceiptDetails.Tables[0].Rows[0]["Receipt Number"].ToString().Trim();
                        //CHG0109406 - CH1 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
                        //TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) and added MST Arizona on 8-31-2010 by cognizant. 
                        lblReceiptDateTime.Text = dsReceiptDetails.Tables[0].Rows[0]["Receipt Date/Time(Arizona)"].ToString().Trim() + " " + "Arizona";
                        //CHG0109406 - CH1 - END - Modified the timezone from "MST Arizona" to Arizona
						lblPymtMethod.Text = dsReceiptDetails.Tables[0].Rows[0]["Pymt. Method"].ToString().Trim();
						lblStatus.Text = dsReceiptDetails.Tables[0].Rows[0]["Status"].ToString().Trim();
						lblRepDO.Text = dsReceiptDetails.Tables[0].Rows[0]["Rep DO Description"].ToString().Trim();
						lblCreatedBy.Text = dsReceiptDetails.Tables[0].Rows[0]["User(s)"].ToString().Trim();

						if(cboRepDO == null)
							strRepDO = cboRepDO.SelectedItem.Value;
						else
							strRepDO = dsReceiptDetails.Tables[0].Rows[0]["Rep DO"].ToString().Trim();

						if(cboUserName == null)
							strUserName = "";
						else
							strUserName = dsReceiptDetails.Tables[0].Rows[0]["User ID"].ToString().Trim();

						FillRepDO();
						setSelectedIndex(cboRepDO,strRepDO);
						FillUserName(cboRepDO.SelectedItem.Value);
						setSelectedIndex(cboUserName,strUserName);
						FillStatus();
						dgProductDetails.DataSource =dsReceiptDetails.Tables[0];
						dgProductDetails.DataBind();
						tblManualUpdate.Visible = true;
					}
					ManualUpdate.Visible = true;
					btnUpdate.Visible = true;
					btnUpdate.Attributes.Add("OnClick","return ConfirmTrans();");
				}
			}
		}
		#endregion

		#region FillRepDO
		/// <summary>
		///  Filling the RepDOs in RepDOs drop down list box.
		/// </summary>
		private void FillRepDO()
		{
			DataTable DOs;
			ua = new AuthenticationClasses.Service.Authentication();
			DOs = ua.GetDOs().Tables[0];
			cboRepDO.DataSource=DOs;
			cboRepDO.DataTextField = "Description";
			cboRepDO.DataValueField = "ID";
			cboRepDO.DataBind();
		}
		#endregion

		#region FillUserName
		/// <summary>
		/// Filling the User Name drop down list for the selected Rep DO.
		/// </summary>
		/// <param name="DO">Input the selected RepDO</param>
		private void FillUserName(string DO)
		{
			ua = new AuthenticationClasses.Service.Authentication();
			DataSet dsUser = ua.ListUsersByRoles("All",Convert.ToInt16("-1"), DO);

			DataTable dt = dsUser.Tables[0];
			DataRow dr =  dt.NewRow();
			dr["USERNAME"] = "Select";
			dr["USERDEF"] = "Select";

			// Add the row to the rows collection.
			dt.Rows.InsertAt(dr,0);
			
			// Set values in the columns:
			dt.Columns.Add("csrUser", typeof(String), "USERDEF"); 
		
			cboUserName.DataSource = dsUser.Tables[0].DefaultView;
			cboUserName.DataTextField = "csrUser";
			cboUserName.DataValueField = "USERNAME";
			cboUserName.DataBind();
			cboUserName.SelectedIndex = 0;
			 
		}
		#endregion

		#region FillStatus
		/// <summary>
		/// Filling the Status drop down list box.
		/// </summary>
		private void FillStatus()
		{
			cboStatus.Items.Add(dsReceiptDetails.Tables[0].Rows[0]["Status"].ToString().Trim());
			cboStatus.Items.Add("Manual Void");
			cboStatus.Items.Add("Approved");   
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

		#region cboRepDO_SelectedIndexChanged
		/// <summary>
		/// Call the FillUserName() if the RepDO has changed.
		/// </summary>
		protected void cboRepDO_SelectedIndexChanged(object sender, System.EventArgs e)
		{
			//strRepDO = cboRepDO.SelectedItem.Value;
			FillUserName(cboRepDO.SelectedItem.Value);
			cboUserName.SelectedIndex = 0;
		}
		#endregion

		#region setSelectedIndex
		/// <summary>
		/// To set the selected item.
		/// </summary>
		/// <param name="cboDropDown">Input the drop down list box.</param>
		/// <param name="Value">Input the value to be set as selected.</param>
		private void setSelectedIndex(System.Web.UI.WebControls.DropDownList cboDropDown,string Value)
		{
			int i =0;
			int Count = 0;
			Count = cboDropDown.Items.Count;
			while (i<Count)
			{
				if (Value.Trim() == cboDropDown.Items[i].Value.Trim())
				{
					cboDropDown.SelectedIndex = i;
					return ;
				}
				i = i + 1;
			}
		}
		#endregion

		#region btnUpdate_Click
		/// <summary>
		/// Action to be taken place when the button is clicked.
		/// </summary>
		protected void btnUpdate_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			if(cboUserName.SelectedItem.Value == "Select")
			{
				lblMessage.Text = "Please select valid User Name";
				lblMessage.Visible = true;
			}
			else
			{
                // PC Phase II changes CH1 - Start - Added the below code to check whether transaction is Payment Central or Payment Tool
                Turnin turnin = new Turnin();
                string AppName = turnin.PCVoidFlowCheck(lblReceiptNumber.Text);
                if (cboStatus.SelectedValue.ToString() == CSAAWeb.Constants.PC_MANUALVOID_SELECT && AppName.ToUpper().Equals(Constants.PC_APPID_PAYMENTCENTRAL))
                {

                    List<string> IDPReversal_Response = new List<string>();
                    IssueDirectPaymentWrapper IDP = new IssueDirectPaymentWrapper();
                    string Information = Page.User.Identity.Name.ToString() + "-" + cboUserName.SelectedItem.Value + "-" + cboRepDO.SelectedItem.Value;
                    IDPReversal_Response = IDP.IDPReversal(lblReceiptNumber.Text, CSAAWeb.Constants.PC_MANUALVOID_STATUS, Information);
                    if (IDPReversal_Response.Count > 0)
                    {
                        if (IDPReversal_Response[0].Equals("SUCC"))
                        {
                            //Session["PC_void_display"] = "The receipt number " + lblReceiptNumber.Text + " has been voided. The voided receipt number is " + IDPReversal_Response[1].ToString();
                            //Context.Items.Add("PC_void_display", "The receipt number " + lblReceiptNumber.Text + " has been voided. The voided receipt number is " + IDPReversal_Response[1].ToString());
                            Session["SelectedReceiptNumber"] = lblReceiptNumber.Text + "-"+IDPReversal_Response[1].ToString();
                            Session["Visited"] = "Yes";
                            Session["Updated"] = "Yes";
                            lblMessage.Visible = true;
                            btnUpdate.Visible = false;
                            Response.Redirect("Manual_Update_Search.aspx");
                        }
                        else
                        {
                            Session["Updated"] = "No";
                            lblMessage.Text = IDPReversal_Response[0] + IDPReversal_Response[1];
                            lblMessage.Visible = true;
                            btnUpdate.Visible = false;
                        }


                    }
                    // PC Phase II changes CH1 - End - Added the below code to check whether transaction is Payment Central or Payment Tool
                }
                else
                {
				lblMessage.Visible = false;
				int RecordsAffected = insObject.ManualUpdate(lblReceiptNumber.Text,cboStatus.SelectedItem.Text,currentUser,cboRepDO.SelectedItem.Value,cboUserName.SelectedItem.Value);
				Session["SelectedReceiptNumber"] = lblReceiptNumber.Text;
				Session["Visited"] = "Yes";
				Session["Updated"] = "Yes";
				if(RecordsAffected > 0)
				{
					Response.Redirect("Manual_Update_Search.aspx");
				}
				else
				{
					Session["Updated"] = "No";
					lblMessage.Text = "Unable to update the receipt";
					lblMessage.Visible = true;
					btnUpdate.Visible = false;
                    }
                }



				}

			}

		#endregion

		#region btnCancel_Click
		/// <summary>
		/// Action to be taken when button is clicked.
		/// </summary>
		protected void btnCancel_Click(object sender, System.Web.UI.ImageClickEventArgs e)
		{
			Session["Updated"] = "No";
			Session["Visited"] = "Yes";
			Response.Redirect("Manual_Update_Search.aspx");
		}
		#endregion

	}
}
