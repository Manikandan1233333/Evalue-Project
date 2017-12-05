//67811A0  - PCI Remediation for Payment systems CH1: Modified the code to remove the credit card suffix to be validated after removing credit card prefix as a part of security testing by cognizant on 01/09/2011
//MAIG - CH1 - Modified the condition to remove the report type 4 and 5 which is not used 
//MAIG - CH2 - Corrected the Error message with a proper sentence from "The end date must be later than the start date."
using System;
using System.Data;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CSAAWeb;
using CSAAWeb.Serializers;
using AuthenticationClasses.Service;

namespace MSCR.Reports
{


	/// <summary>
	/// Summary description for InsRptLibrary.
	/// </summary>
	public class InsRptLibrary
	{
		public InsRptLibrary()
		{
			//
			// TODO: Add constructor logic here
			//
		}

        
		#region Fill a list box with users.
		/// <summary>
		/// Fill a list box with users.
		/// </summary>
		public void FillUserListBox(ListBox listUser, string CurrentUser, string sRequester, string sRole, string sRepDO, bool bVisible, bool bAllUsers )
		{
			Authentication ua = new Authentication(CurrentUser);
            if (sRole != "")
            {
                DataSet dsUser = ua.ListUsersByRoles(sRequester, Convert.ToInt16(sRole), sRepDO);//RoleId = 6 for DSRs

                DataTable dt = dsUser.Tables[0];
                DataRow dr = dt.NewRow();

                if (bAllUsers == true)
                {
                    dr["USERNAME"] = "All";
                    dr["USERDEF"] = "All";

                    // Add the row to the rows collection.
                    dt.Rows.InsertAt(dr, 0);
                }

                // Set values in the columns:
                dt.Columns.Add("csrUser", typeof(String), "USERDEF");

                listUser.DataSource = dsUser.Tables[0].DefaultView;
                listUser.DataTextField = "csrUser";
                listUser.DataValueField = "USERNAME";
                listUser.DataBind();
                listUser.Visible = bVisible;

                //if (bAllUsers == false)
                listUser.SelectedIndex = 0;
            }
		}
		#endregion

		#region Fill a list box with users.
		/// <summary>
		/// Fill a list box with users - Overloaded to cache on page load.
		/// </summary>
		public DataTable FillUserListBox(ListBox listUser, string CurrentUser, string sRequester, string sRole, string sRepDO, bool bVisible, bool bAllUsers, DataTable Users )
		{
			DataTable dt;
			if (Users == null)
			{
				Authentication ua = new Authentication(CurrentUser);
						
				DataSet dsUser = ua.ListUsersByRoles(sRequester,Convert.ToInt16(sRole), sRepDO);//RoleId = 6 for DSRs

				dt = dsUser.Tables[0];
				DataRow dr =  dt.NewRow();
			
				if (bAllUsers == true)
				{
					dr["USERNAME"] = "All";
					dr["USERDEF"] = "All";

					// Add the row to the rows collection.
					dt.Rows.InsertAt(dr,0);
				}
				
				// Set values in the columns:
				dt.Columns.Add("csrUser", typeof(String), "USERDEF");
				Users = dt;
			}
		
			listUser.DataSource = Users.DefaultView;
			listUser.DataTextField = "csrUser";
			listUser.DataValueField = "USERNAME";
			listUser.DataBind();
			listUser.Visible = bVisible;
			
			//if (bAllUsers == false)
			listUser.SelectedIndex = 0;
			return Users;
		}
		#endregion



		#region Fill a drop-down listbox with Rep DOs.
		/// <summary>
		/// Fill a drop-down listbox with Rep DOs.
		/// </summary>
		public DataTable FillDOListBox(DropDownList listDO, DataTable DOs)
		{
			//OrderClasses.Service.Order DataConnection = new OrderClasses.Service.Order();
			DataTable Dt;
			//Dt = DataConnection.LookupDataSet("Authentication", "GetDOs").Tables[0];
			if (DOs==null) 
			{
				Authentication ua = new Authentication();
				Dt = ua.GetDOs().Tables[0];
				DataRow Row = Dt.NewRow();
				Row["ID"]=-1;
				Row["Description"]="All";
				Dt.Rows.InsertAt(Row, 0);
				DOs = Dt;
			}
			listDO.DataSource=DOs;
			listDO.DataBind();

			return DOs;			
			
		}
		#endregion

		#region Fill a numeric combo box incrementally
		/// <summary>
		/// Fill a numeric combo box incrementally
		/// </summary>
		public void FillNumericComboBox(DropDownList list, int startNum, int endNum)
		{
			string s = String.Empty;
			for (int i = startNum; i <= endNum; i++) 
			{
                s = Convert.ToString(i);
				if (s.Length == 1)
					s = "0" + s; 
				list.Items.Add(s);
			}
		}
		#endregion

		#region Fill a numeric combo box incrementally - Overloaded with selected index
		/// <summary>
		/// Fill a numeric combo box incrementally - Overloaded.
		/// </summary>
		public void FillNumericComboBox(DropDownList list, int startNum, int endNum, int selIndex)
		{
			FillNumericComboBox(list, startNum, endNum);
			list.SelectedIndex = selIndex;
		}
		#endregion

		#region Populate Date Dynamically
		/// <summary>
		/// Populate Date Dynamically
		/// </summary>
		public void DynamicDate(DropDownList listDay, DropDownList listMonth, DropDownList listYear)
		{
			listDay.Items.Clear();
			FillNumericComboBox(listDay,1,28);
			listDay.Items.Remove("29");
			listDay.Items.Remove("30");
			listDay.Items.Remove("31");
			if (listMonth.SelectedIndex == 1)
			{
				if (listYear.SelectedItem.Value.ToString()== "2004" || listYear.SelectedItem.Value.ToString()== "2008")
					listDay.Items.Add("29");
			}
			else if (listMonth.SelectedIndex == 3 || listMonth.SelectedIndex == 5 || listMonth.SelectedIndex == 8 || listMonth.SelectedIndex == 10)
			{
				listDay.Items.Add("29");
				listDay.Items.Add("30");
			}
			else
			{
				listDay.Items.Add("29");
				listDay.Items.Add("30");
				listDay.Items.Add("31");
			}
		}
		#endregion
		
		#region Sets a message.
		/// <summary>
		/// 
		/// </summary>
		/// <param name="msg"></param>
		public void SetMessage(Label lblName, string msg, bool showArrow) 
		{
			lblName.Text = msg;
			lblName.Visible = true;
		}
		#endregion

		#region Date Validation and setting limits on dates.
		/// <summary>
		/// 
		/// </summary>
		/// <param name="dates, reporttype"></param>
		public bool ValidateDate(Label lblName, DateTime startDate, DateTime endDate, int reportType) 
		{			
			if ( endDate < startDate)
			{
                //MAIG - CH2 - BEGIN - Corrected the Error message with a proper sentence from "The end date must be later than the start date."
                SetMessage(lblName, "The end date must be greater than start date.", true);
                //MAIG - CH2 - END - Corrected the Error message with a proper sentence from "The end date must be later than the start date."
				return true;
			}
            // MAIG - 12092014 - Begin - Commented out Date restriction
			/*TimeSpan ts = endDate.Subtract(startDate);
			//MAIG - CH1 - BEGIN - Modified the condition to remove the report type 4 and 5 which is not used 
			if (ts.Days > 95)
			//MAIG - CH1 - END - Modified the condition to remove the report type 4 and 5 which is not used 
			{
				SetMessage(lblName, "Please limit the date range to no more than 3 months.", true); 
				return true;
			}*/
            // MAIG - 12092014 - End - Commented out Date restriction
			return false;
		
		}
		#endregion

		#region Validating CC Number for Search
		/// <summary>
		/// 
		/// </summary>
		/// <param name="CC Prefix, CC Suffix"></param>
        //67811A0  - PCI Remediation for Payment systems CH1:Start Modified the code to remove the credit card suffix to be validated after removing credit card prefix as a part of security testing by cognizant on 01/09/2011
		public bool ValidateCCNumber(Label lblName, string Suffix) 
		{	
			try
			{
				if (Suffix.Length == 0) return false;
				if (Suffix.Length < 4)
				{
					SetMessage(lblName, "If searching by credit card, put in exactly 4 digits for suffix", true); 
					return true;
				}
				else if (Convert.ToDouble(Suffix)  < 0 || Convert.ToDouble(Suffix)  > 9999)
				{
					SetMessage(lblName, "If searching by credit card, put in a numeric value for suffix", true); 
					return true;
				}
				else return false;		
			}
			catch (FormatException)
			{ 
				SetMessage(lblName, "If searching by credit card, put in a numeric value for suffix", true); 
				return true;
			}
		}
		#endregion
        //67811A0  - PCI Remediation for Payment systems CH1:END Modified the code to remove the credit card suffix to be validated after removing credit card prefix as a part of security testing by cognizant on 01/09/2011
        public bool ValidateCCNumber(Label lblName, string Prefix, string Suffix)
        {
            try
            {
                if (Prefix.Length == 0 && Suffix.Length == 0) return false;
                if (Prefix.Length < 4 || Suffix.Length < 4)
                {
                    SetMessage(lblName, "If searching by credit card, put in exactly 4 digits for prefix and suffix", true);
                    return true;
                }
                else if (Convert.ToDouble(Prefix) < 1000 || Convert.ToDouble(Prefix) > 9999 || Convert.ToDouble(Suffix) < 0 || Convert.ToDouble(Suffix) > 9999)
                {
                    SetMessage(lblName, "If searching by credit card, put in a numeric value for prefix and suffix", true);
                    return true;
                }
                else return false;
            }
            catch (FormatException)
            {
                SetMessage(lblName, "If searching by credit card, put in a numeric value for prefix and suffix", true);
                return true;
            }
        }

		#region Fill a Drop Down with Approvers.
		/// <summary>
		/// Code Added by Cognizant on 04/19/2005 as part of Cashier Recon Enhancements
		/// Fill a Drop Down with Approvers 
		/// </summary>
		public void FillApproversDropDown(DropDownList drpApprovers, string sRepDO)
		{
			DataTable Approvers;
			
			Authentication Auth = new Authentication();
						
			DataSet dsApprovers = Auth.ListApprovers(sRepDO);

			Approvers = dsApprovers.Tables[0];
			DataRow dr =  Approvers.NewRow();
			dr["USERNAME"] = "All";
			dr["USERDEF"] = "All";
			// Add the row to the rows collection.
			Approvers.Rows.InsertAt(dr,0);
			// Set values in the columns:
			Approvers.Columns.Add("csrUser", typeof(String), "USERDEF");
			drpApprovers.DataSource = Approvers.DefaultView;
			drpApprovers.DataTextField = "csrUser";
			drpApprovers.DataValueField = "USERNAME";
			drpApprovers.DataBind();
			drpApprovers.SelectedIndex = 0;
			
		}
		#endregion


	}
}
