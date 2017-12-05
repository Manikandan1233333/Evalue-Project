/*
 * Modified as a part of STAR Auto 2.1 on 08/17/2007:
 * STAR AUTO 2.1.Ch1: Modified the code to assign value to the label "lblRepDOName"
 * 
 * Modified on 09/07/2007
 * STAR AUTO 2.1.Ch2: Added a condition to avoid displaying RepDO in Print and Excel version of MyReports page.
 * MODIFIED AS A PART OF TIME ZONE CHANGE ENHANCEMENT ON 8-31-2010.
 * TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time and DateRange by cognizant.
 * //CHG0109406 - CH1 - Modified the timezone from "MST Arizona" to Arizona
*/
namespace MSCR.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for PrintHeader.
	/// </summary>
	public partial  class PrintHeader : System.Web.UI.UserControl
	{
 //Added by Cognizant

		protected void Page_Load(object sender, System.EventArgs e)
        {
            // Put user code to initialize the page here
            lblTitle.Text = (string)Session["RptTitle"];
            //CHG0109406 - CH1 - BEGIN - Modified the timezone from "MST Arizona" to Arizona
            //TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time and DateRange by cognizant.
            lblRunDate.Text = DateTime.Now.ToString() + " " + "Arizona";

            //Added by Cognizant
            lblPayment.Text = (string)Session["PaymentName"];
            //Added by Cognizant
            //TimeZoneChange.Ch1-Added Text MST Arizona at the end of the current date and time and DateRange by cognizant.
            lblDateRange.Text = (string)Session["DateRange"] + " " + "Arizona";
            //CHG0109406 - CH1 - END - Modified the timezone from "MST Arizona" to Arizona
            if ((Session["ProductName"] == null) || ((string)Session["ProductName"] == String.Empty) || ((string)Session["ProductName"] == ""))
                trProductType.Visible = false;
            else
                lblProduct.Text = (string)Session["ProductName"];

            if ((Session["RevenueName"] == null) || ((string)Session["RevenueName"] == String.Empty) || ((string)Session["RevenueName"] == ""))
                trRevenueType.Visible = false;
            else
                lblRevenue.Text = (string)Session["RevenueName"];

            if ((Session["AppName"] == null) || ((string)Session["AppName"] == String.Empty) || ((string)Session["AppName"] == ""))
                trApplication.Visible = false;
            else
                lblApp.Text = (string)Session["AppName"];

            //STAR AUTO 2.1.Ch2: START - Added a condition to avoid displaying RepDO in Print and Excel version of MyReports page.
            if (Session["RepDOName"] != null)
            {
                //STAR AUTO 2.1.Ch1: START - Modified the code to assign value to the label "lblRepDOName"
                lblRepDOName.Text = (string)Session["RepDOName"];
                //STAR AUTO 2.1.Ch1: END
            }
            else
            {
                trRepDO.Visible = false;
            }
            //STAR AUTO 2.1.Ch2: END
            string csrs = (string)Session["CSRs"];
            if (csrs != String.Empty)
            {
                lblCsrTxt.Text = "Users:";
                lblCsrVal.Text = csrs;
            }
            if ((string)Session["StatusType"] == string.Empty)
            {
                lblStatus.Visible = false;
            }
            else
            {
                string status = (string)Session["StatusType"];
                lblStatus.Text = status;
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
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion
	}
}
