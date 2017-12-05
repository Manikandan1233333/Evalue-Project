/*
 * HISTORY
 *			11/18/2004 JOM Replaced Convert.ToInt32(Config.Setting("PaymentID.CreditCard")) with 
 *			PaymentClasses.PaymentTypes.CreditCard
 *			PC Security Defect Fix -CH1 - Modified the below code to add logging inside the catch block
*/
namespace MSC.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using System.IO;
    //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016
	using CSAAWeb;
	using CSAAWeb.WebControls;
    using CSAAWeb.AppLogger;

	/// <summary>
	///	This control generates the text for an email confirmation.
	/// </summary>
	public partial  class ConfirmingEmail : System.Web.UI.UserControl, IConfirmingEmail 
	{

		///<summary/>
		protected Label DefaultFrom;
		///<summary/>
		protected Label Subject;
		///<summary/>
		protected Label SendEmail;
		///<summary/>
		private static string MailFrom = Config.Setting("Confirm_MailFrom");

		private OrderControl _Order;

		/// <summary>
		/// The Order to confirm.
		/// </summary>
		public OrderControl Order {get {return _Order;} set {_Order=value;}}
		
		/// <summary>
		/// The URL to see the card images.
		/// </summary>
        //protected string CardURL
        //{
        //    get 
        //    {
        //        return Config.Setting("AssociateCards_Url") + 
        //            "?" + Cryptor.Encrypt(((MembershipInfo)Order["Membership"]).OrderID.ToString(), 
        //            Config.Setting("CSAA_ORDERS_KEY"));
        //    }
        //}

		/// <summary>
		/// Renders this control to a string.
		/// </summary>
		public string Render() 
		{
			return Renderer.Render(this);
		}

		/// <summary>
		/// Sends this email.
		/// </summary>
		public void Send(OrderControl O) {
			Order = O;
			if (!ChildControlsCreated) CreateChildControls();
            if (SendEmail != null)
            {
                try { if (!Boolean.Parse(SendEmail.Text)) return; }
                catch (Exception ex)
                {
                    // ignore errors and move values as they are
                    //PC Security Defect Fix -CH1 - START Modified the below code to add logging inside the catch block
                    Logger.Log(ex.Message);
                    //PC Security Defect Fix -CH1 - END Modified the below code to add logging inside the catch block

                }
            }
			string From = MailFrom;
			string subject = "CSAA order confirmation";
			if (Subject!=null) subject = Subject.Text;
			if (DefaultFrom!=null && DefaultFrom.Text!="") 
				From = DefaultFrom.Text;
			if (From!="") {
				//START Added by Cognizant For Blocking email for Payment Types other than Online Credit Card
				if(O.Detail.PaymentType == (int)PaymentClasses.PaymentTypes.CreditCard)   
				System.Web.Mail.SmtpMail.Send(From, Order.Addresses.Household.Email, subject, Render());
				//END
			}
		}

		protected void Page_Load(object sender, System.EventArgs e) {}

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
		
		///	<summary>
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
		}
		#endregion
	}
}
