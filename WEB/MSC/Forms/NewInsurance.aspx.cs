/*
 * HISTORY
 *	67811A0 - PCI Remediation for Payment systems : Added to clear the cache of the page to prevent the page loading on back button hit after logout
Security Defect - CH1 -START Added the below code to verify if the IP address of the login page and current page is same
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
using InsuranceClasses;
using System.Web.Security;
using CSAAWeb;
using System.Net;
using System.Net.Sockets;
using CSAAWeb.WebControls;
using CSAAWeb.AppLogger;

namespace MSC.Forms
{
	/// <summary>
	/// This page is the start page for the application, and currently
	/// sets up a call to primary with the order primed to enter data for
	/// a new member order.
	/// </summary>
	public partial class NewInsurance : SiteTemplate {
		///<summary>The Url to navigate on Back button click (from web.config).</summary>
		protected static string _OnBackUrl;
		///<summary>The Url to navigate on Continue button click (from web.config).</summary>
		protected static string _OnContinueUrl=string.Empty;
		///<summary/>
		public override string OnCancelUrl {get {return Request.Path;} set {}}
		/// <summary>
		/// Creates a new order with blank household data and number of associates=0
		/// </summary>
		protected override void SavePageData() {
			InsuranceInfo I = new InsuranceInfo();
			I.Lines = new ArrayOfInsuranceLineItem();
			I.Lines.Add(new InsuranceLineItem());
			Order.Products.Add(I);

		}
		
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
        //67811A0 - PCI Remediation for Payment systems Start: Added to clear the cache of the page to prevent the page loading on back button hit after logout
        protected void Page_Load(object sender, System.EventArgs e)
        {
            ////Security Defect - CH1 -START Added the below code to verify if the IP address of the login page and current page is same
            //string ipaddr1 = Request.ServerVariables["REMOTE_ADDR"];
            //if (CSAAWeb.AppLogger.Logger.Sourcereg(ipaddr1))
            //{
            //    ipaddr1 = Request.ServerVariables["REMOTE_ADDR"];
            //}
            //else
            //{
            //    ipaddr1 = CSAAWeb.AppLogger.Logger.GetIPAddress();
            //}
            //if (Request.Cookies["AuthToken"] == null)
            //{
            //    Logger.Log(CSAAWeb.Constants.SEC_COOKIE_NULL);
            //    Response.Redirect("/PaymentToolmscr/Forms/login.aspx");

            //}
            //else if (!ipaddr1.Equals(
            //    Request.Cookies["AuthToken"].Value))
            //{
            //    Logger.Log(CSAAWeb.Constants.SEC_COOKIE_NOTEQUAL);
            //    Response.Redirect("/PaymentToolmscr/Forms/login.aspx"); ;

            //}
            ////Security Defect - CH1 -START Added the below code to verify if the IP address of the login page and current page is same
            //else
            //{
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
			}
        //}
        //67811A0 - PCI Remediation for Payment systems End: Added to clear the cache of the page to prevent the page loading on back button hit after logout

	}
}
