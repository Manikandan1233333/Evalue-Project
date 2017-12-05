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
//CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016
using System.Web.Security;
using CSAAWeb;
using CSAAWeb.WebControls;

namespace MSC.Forms {
	/// <summary>
	/// This page is the start page for the application, and currently
	/// sets up a call to primary with the order primed to enter data for
	/// a new member order.
	/// </summary>
	public partial class New : SiteTemplate {
		///<summary>The Url to navigate on Back button click (from web.config).</summary>
		protected static string _OnBackUrl;
		///<summary>The Url to navigate on Continue button click (from web.config).</summary>
		protected static string _OnContinueUrl=string.Empty;
		///<summary/>
		public override string OnCancelUrl {get {return Request.Path;} set {}}
		/// <summary>
		/// Creates a new order with blank household data and number of associates=0
		/// </summary>
        /// //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016
        //protected override void SavePageData() {
        //    MembershipInfo MemberOrder = new MembershipInfo();
        //    MemberOrder.NumberOfAssociates = 0;
        //    MemberOrder.ShowQuantity=false;
        //    Order.Addresses.Add(new OrderClasses.AddressInfo(OrderClasses.AddressInfoType.Household));
        //    Order.Products.Add(MemberOrder);
        //}
		
		/// <summary>
		/// Authenticates, if authentication turned on, then transfers to primary.aspx.
		/// </summary>
		/// <param name="e"></param>
		protected override void OnInit(EventArgs e) {
			InitializeComponent();
			base.OnInit(e);
			GotoPage(OnContinueUrl, false);
		}

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent() {    
		}
	}
}
