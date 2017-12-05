/*MODIFIED BY COGNIZANT AS PART OF .NET MIGRATION CHANGES
 * CHANGES DONE
 * 05/16/2010 .NetMig.Ch1: Added a new method ReqValCheck to validate the address.
 *.NetMig:Ch2:Added a default values to UnRequired().
 * 67811A0 - PCI Remediation for Payment systems CH1: Commmented Address validations.
 */
namespace MSC.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
    //CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016
	using CSAAWeb;
	using CSAAWeb.WebControls;


	/// <summary>
	///	Address user control for inputting address data.
	/// </summary>
	public partial  class Address : ValidatingUserControl
	{
		///<summary/>
		protected System.Web.UI.WebControls.TextBox _Address1;
		///<summary/>
		protected System.Web.UI.WebControls.TextBox _Address2;
		///<summary/>
		protected System.Web.UI.WebControls.TextBox _City;
		///<summary/>
		protected MSC.Controls.State _State;
		///<summary/>
		protected MSC.Controls.Zip _ZipCode;
		///<summary/>
		protected MSC.Controls.Phone _DayPhone;
		///<summary/>
		protected MSC.Controls.Phone _EveningPhone;
		///<summary/>
		protected string _Email = string.Empty;
		///<summary/>
		protected Validator vldA1;
		///<summary/>
		protected Validator vldCity;

		///<summary/>
		public string Address1 {get {return _Address1.Text;} set {_Address1.Text=value;}}
		///<summary/>
		public string Address2 {get {return _Address2.Text;} set {_Address2.Text=value;}}
		///<summary/>
		public string City {get {return _City.Text;} set {_City.Text=value;}}
		///<summary/>
		public string State {get {return _State.Text;} set {_State.Text=value;}}
		///<summary/>
		public string DayPhone {get {return _DayPhone.Text;} set {_DayPhone.Text=value;}}
		///<summary/>
		public string EveningPhone {get {return _EveningPhone.Text;} set {_EveningPhone.Text=value;}}
		///<summary/>
		public string Zip {get {return _ZipCode.Text;} set {_ZipCode.Text=value;}}
		///<summary/>
		public string Email {get {return _Email;} set {_Email=value;}}
		///<summary/>
		private bool _Billing = false;
		///<summary>
		///True if this is a billing address, in which case Address2, City and
		///state aren't displayed
		///</summary>
		public bool Billing {get {return _Billing;} set {_Billing=value; if (value) AutoZip=true;}}
		/// <summary>
		/// True if the City and state should be looked up by zip rather than entered.
		/// </summary>
		public bool AutoZip = false;
		/// <summary>
		/// True if the zip code should be validated against the club's service area.
		/// </summary>
		public bool VerifyClub {get {return _ZipCode.VerifyClub;} set {_ZipCode.VerifyClub=value;}}

		///<summary>Returns the Xml data for this control.</summary>
		public override string ToString() {return Data.ToString();}
		///<summary>The data (AddressInfo) for this control.</summary>
		public OrderClasses.AddressInfo Data {get {return (new OrderClasses.AddressInfo(this));} set {value.CopyTo(this);}}
		///<summary>Loads the control from an Xml string.</summary>
		public void LoadXML(string Xml) {Data = new OrderClasses.AddressInfo(Xml);}

		#region Web Form Designer generated code
		///<summary/>
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			UnRequired();
			base.OnInit(e);
		}
        //.NetMig:Ch1:Added a new method to check address field as a part od .NetMig 3.5
        protected void ReqValCheck(Object source, ValidatorEventArgs args)
        {
            if (_Address1.Text == "")
            {
               // 67811A0 - PCI Remediation for Payment systems CH1:START Commmented Address validations.
                //args.IsValid = false;
                //vldAddress1.MarkInvalid();
                //valid.ErrorMessage = "";   
               // 67811A0 - PCI Remediation for Payment systems CH1:END Commmented Address validations.
                
            }
            //if (_ZipCode.Text == "")
            //{
            //    args.IsValid = false;
            //    vldCity.MarkInvalid();
            //}
        }
		
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{    

		}
		#endregion
		///<summary/>
		protected override void Page_Load(Object source, EventArgs e) {
			if (!AutoZip) _ZipCode.StateToCheck = State;
		}
		///<summary/>
		private void UnRequired() 
		{
			if (AutoZip)
            {
                //.NetMig:Ch2:Start-Added a values to suppply to the phone.ascx as a part of .NetMig 3.5
                _DayPhone.Text = "0000000000";
                _EveningPhone.Text = "0000000000";
                //End-Added a values to suppply to the phone.ascx as a part of .NetMig 3.5
                _DayPhone.Required=false;
				_State.Required=false;
				vldCity.Enabled = false;
			}
		}
	}

}
