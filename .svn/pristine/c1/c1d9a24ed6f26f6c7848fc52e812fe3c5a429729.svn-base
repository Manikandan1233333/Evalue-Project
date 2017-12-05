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

namespace MSC.Forms
{
	/// <summary>
	/// This error page is displayed whenever an unhandled exceptionis thrown if
	/// Show_Errors is false.
	/// </summary>
	public partial class general_error : PageTemplate
	{
		///<summary/>
		protected System.Web.UI.WebControls.Label Name;
		///<summary/>
		//protected System.Web.UI.WebControls.Label Label1;
		///<summary/>
		//Changed(commented email) as part of Tier 3 on 02/9/05
		//protected System.Web.UI.WebControls.Label Email;

		///<summary/>
		private ContactInfo Contact=new ContactInfo(Config.SettingArray("Administrator_ContactInfo"));
		///<summary/>
		protected void Page_Load(object sender, System.EventArgs e)
		{
			Name.Text=Contact.Name;
			Phone.Text=Contact.Phone;
			//Email.Text=Contact.Email;
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
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
	}

	///<summary/>
	internal class ContactInfo 
	{
		///<summary/>
		public ContactInfo() {}
		///<summary/>
		public ContactInfo(ArrayList A) 
		{
			Name=(string)A[0];
			Phone=(string)A[1];
			//Email=(string)A[2];
		}
		///<summary/>
		public string Name=string.Empty;
		///<summary/>
		public string Phone=string.Empty;
		///<summary/>
		//public string Email=string.Empty;
	}
}
