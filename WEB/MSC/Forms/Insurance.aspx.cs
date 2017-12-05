/*
 * HISTORY
 *	11/18/2004	JOM Changed reference to RevenueType property of InsPayment_New control from int to string
 *	07/26/2005	MODIFIED BY COGNIZANT 
 *	CSR#3937.Ch1 : Changed Renaming the InsPayment_New to InsPayment
 *	67811A0  - PCI Remediation for Payment systems CH1: Code was cleaned up and unwanted code was removed.
 *	67811A0 - PCI Remediation for Payment systems CH2 : Added to clear the cache of the page to prevent the page loading on back button hit after logout
 *	CHG0072116 - PC Clear CC Details when user clicks back button in insurance page CH1 -  Create context items if page gets postback.
*/
using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CSAAWeb.WebControls;
using InsuranceClasses;
using OrderClasses;

namespace MSC.Forms
{
	/// <summary>
	/// This page captures policy and amount information for an insurance order.
	/// </summary>
	public partial class Insurance : SiteTemplate
	{
		///<summary>The Url to navigate on Back button click (from web.config).</summary>
		protected static string _OnBackUrl;
		///<summary>The Url to navigate on Continue button click (from web.config).</summary>
		protected static string _OnContinueUrl=string.Empty;
		
		///<summary/>
		protected System.Web.UI.WebControls.ImageButton Add;
		///<summary/>
		protected ItemsControl Items;
		///<summary/>
		protected Validator vldMissMatch;
		///<summary/>
		protected MSC.Controls.Buttons Buttons1;

		/// <summary>
		/// Restores state data.
		/// </summary>
        protected override void RestorePageData()
        {
            InsuranceInfo I = (InsuranceInfo)Order["Insurance"];
            if (I == null) return;
            Items.CopyFrom(I.Lines);
            
        }

		/// <summary>
		/// Saves state data.
		/// </summary>
        protected override void SavePageData()
        {
            InsuranceInfo I = (InsuranceInfo)Order["Insurance"];
            if (I == null)
            {
                I = new InsuranceInfo();
                Order.Products.Add(I);
            }
            I.Lines = (ArrayOfInsuranceLineItem)Items.CopyTo(new ArrayOfInsuranceLineItem());
            Order.UpdateLines("Insurance");           
        }

		
		/// <summary>
		/// Update order lines and return true.
		/// </summary>
        public override bool ProcessPage()
        {
            return Order.IsValid;
        }

		protected void Page_Load(object sender, System.EventArgs e)
		{
            Control Summary_Control = this.FindControl("Summary");
            HtmlTable Summary_Table = (HtmlTable)Summary_Control.FindControl("SummaryTable");
            Summary_Table.Visible = false;
            //Commented the below lines to load the page on invalid policy cancel button click
            //67811A0 - PCI Remediation for Payment systems CH2 Start: Added to clear the cache of the page to prevent the page loading on back button hit after logout
            //Response.Cache.SetCacheability(HttpCacheability.NoCache);
            //Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            //Response.Cache.SetNoStore();
            //67811A0 - PCI Remediation for Payment systems CH2 End: Added to clear the cache of the page to prevent the page loading on back button hit after logout

            //CHG0072116 - PC Clear CC Details when user clicks back button in insurance page CH1:START -  Create context items if page gets postback.
            if (IsPostBack)
            {
                if (Context.Items["IsCC"] == null)
                {
                    Context.Items["IsCC"] = true;
                }
            }
            //CHG0072116 - PC Clear CC Details when user clicks back button in insurance page CH1:END -  Create context items if page gets postback.
            
            
            
		}

		#region Web Form Designer generated code
		///<summary></summary>
		override protected void OnInit(EventArgs e)
		{
			
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.			
			InitializeComponent();
			base.OnInit(e);
		}
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    
			this.PreRender += new EventHandler(this.Page_PreRender);

		}
		#endregion

		/// <summary>
		/// Updates the order lines.
		/// </summary>
		protected void Page_PreRender(Object source, EventArgs e) 
		{
			if (IsPostBack) 
			{
				((InsuranceInfo)Order["Insurance"]).Lines = (ArrayOfInsuranceLineItem)Items.CopyTo(new ArrayOfInsuranceLineItem());
				Order.UpdateLines("Insurance");
			}
		}
               
		private HybridDictionary CheckList = new HybridDictionary();
		
        
	}
}
