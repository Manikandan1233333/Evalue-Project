/*
 * HISTORY
 *	//67811A0  - PCI Remediation for Payment systems CH1:- Commented the below code lined to remove the Total Amount field in the payment confirmation screen.
    //67811A0  - PCI Remediation for Payment systems CH2:- set visibility to column1 to true and commented the code which sets visibility to column2 as False.         
    //67811A0  - PCI Remediation for Payment systems CH3: - Change Order Summary to Payment Summary
 *         //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the PromoSummary instantiation - March 2016
 *	
*/

namespace MSC.Controls
{
    using System;
    using System.Data;
    using System.Drawing;
    using System.Web;
    using System.Web.UI.WebControls;
    using System.Web.UI.HtmlControls;
    using InsuranceClasses;
    //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016

    /// <summary>
    /// New OrderSummary Control Created by Cognizant on 29/11/2004 exclusively for 
    /// New Payment Confirmation page and this will Display the Insurance Products only
    ///	OrderSummary encapsulates the calculation and display of the
    ///	order line items.
    /// </summary>
    public partial class OrderSummary_New : System.Web.UI.UserControl
    {
        //Summary Table, Datagrid and Label for Insurance

        //Summary Table, Datagrid and Label for Membership
        //Promotion Summary

        protected InsuranceInfo Insurance;
        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016
        ///<summary/>
        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the PromoSummary instantiation - March 2016
        ///<summary/>
        protected override void OnPreRender(EventArgs e)
        {

            if (Order != null && Order.Lines.Count > 0)
            {
                if (Order.Products["Insurance"] != null)
                {
                    //Hide the Membership table
                    INS_SummaryTable.Visible = true;
                    MBR_SummaryTable.Visible = false;
                    //Assign the label
                    INS_SummaryLabel.Text = "Product and Total Amount Information";
                    InsuranceSummary();
                }
                else if (Order.Products["Membership"] != null)
                {
                    //Hide the Insurance table
                    MBR_SummaryTable.Visible = true;
                    INS_SummaryTable.Visible = false;
                    //Assign the label
                    //67811A0  - PCI Remediation for Payment systems CH3:Start - Change Order Summary to Payment Summary
                    MBR_SummaryLabel.Text = "Payment Summary";
                    //67811A0  - PCI Remediation for Payment systems CH3:End - Change Order Summary to Payment Summary
                    //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the MembershipContact.ascx User control code as part of the Code Clean Up - March 2016
                }


            }
            else
            {
                //If the Order is null then Hide Both the Insurance and Membership Tables
                INS_SummaryTable.Visible = false;
                MBR_SummaryTable.Visible = false;
            }

            base.OnPreRender(e);
        }

        #region Web Form Designer generated code
        ///<summary/>
        override protected void OnInit(EventArgs e)
        {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///<summary>
        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        ///<summary/>
        protected void Page_Load(object sender, System.EventArgs e)
        {
            //Check the Cache for the Availability of Insurance Product Types
            //If not fill the cache.

            DataTable Dt;
            if (Cache["INS_Product_Type"] == null)
            {
                Dt = ((SiteTemplate)Page).OrderService.LookupDataSet("Insurance", "ProductTypes").Tables["INS_Product_Type"];
                Cache["INS_Product_Type"] = Dt;
            }
        }



        private void InsuranceSummary()
        {

            Insurance = (InsuranceInfo)Order.Products["Insurance"];
            if (Order.Lines.Total == 0)
            {
                //67811A0  - PCI Remediation for Payment systems CH2:START- set visibility to column1 to true and commented the code which sets visibility to column2 as False. 
                // INS_SummaryView.Columns[2].Visible=false;
                INS_SummaryView.Columns[1].Visible = false;
                //67811A0  - PCI Remediation for Payment systems CH2:END- set visibility to column1 to true and commented the code which sets visibility to column2 as False.  
            }
            else
            {
                //67811A0  - PCI Remediation for Payment systems CH1: Start - Commented the below code lined to remove the Total Amount field in the payment confirmation screen.
                //INS_SummaryView.Columns[2].FooterText = "Total Amount";
                //INS_SummaryView.Columns[3].FooterText = Insurance.Lines.Total.ToString("C");                
                //67811A0  - PCI Remediation for Payment systems CH1: End - Commented the below code lined to remove the Total Amount field in the payment confirmation screen.

                INS_SummaryView.Columns[1].Visible = false;
            }
            DataTable dtInsSummary = Insurance.Lines.Data;
            DataColumn dcProductDesc = new DataColumn("ProductDesc");
            dtInsSummary.Columns.Add(dcProductDesc);

            foreach (DataRow dr in dtInsSummary.Rows)
            {

                DataRow[] drDesc = ((DataTable)Cache["INS_Product_Type"]).Select("ID = '" + dr["ProductType"] + "'");
                dr["ProductDesc"] = drDesc[0]["Description"].ToString();

            }


            INS_SummaryView.DataSource = dtInsSummary;
            INS_SummaryView.DataBind();
        }


        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the PromoSummary refernce as part of Code Clean Up - March 2016
    }
}
