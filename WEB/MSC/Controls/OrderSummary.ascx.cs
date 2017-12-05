//CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the PromoSummary instantiation - March 2016
namespace MSC.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016

	/// <summary>
	///	OrderSummary encapsulates the calculation and display of the
	///	order line items.
	/// </summary>
	public partial  class OrderSummary : System.Web.UI.UserControl
	{
		///<summary/>
		protected DataGrid SummaryView;
		///<summary/>
		protected Label SummaryLabel;
		///<summary/>
		protected HtmlTable SummaryTable;
		///<summary/>
		protected HtmlTableRow PromoSummary;
		///<summary/>
        //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016
		///<summary/>

		///<summary/>
		public string Title {get {return SummaryLabel.Text;} set {SummaryLabel.Text=value;}}
		///<summary/>
		protected override void OnPreRender(EventArgs e) 
		{
			if (Order!=null && Order.Lines.Count>0) 
			{
				//CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016
				if (Order.Lines.Total==0) {
					SummaryView.Columns[2].Visible=false;
				} else {
					SummaryView.Columns[0].FooterText = "Total";
					SummaryView.Columns[2].FooterText = Order.Lines.Total.ToString("C");
					SummaryView.Columns[1].Visible=false;
				}
				SummaryView.DataSource = Order.Lines.Data;
				SummaryView.DataBind();
                //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016
				
			} else SummaryTable.Visible=false;

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
		protected void Page_Load(object sender, System.EventArgs e) {}

		protected void SummaryView_SelectedIndexChanged(object sender, System.EventArgs e)
		{
		
		}
	}
}
