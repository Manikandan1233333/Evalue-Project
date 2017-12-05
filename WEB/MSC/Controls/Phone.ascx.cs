/*
 * Added a new method CheckValid1() as a part of the .Net Mig 3.5 on 03-13-2010 by cognizant.
 
 * 
 * 
 * 
 * 
 **/
namespace MSC.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using CSAAWeb;
	using CSAAWeb.WebControls;


	/// <summary>
	///	Encapsulates the entry and validation of a phone number as 3 or 4 text boxes.
	/// </summary>
	public partial  class Phone : ValidatingUserControl
	{
		///<summary/>
		protected System.Web.UI.WebControls.TextBox Area_Code;
		///<summary/>
		protected System.Web.UI.WebControls.TextBox Prefix;
		///<summary/>
		protected System.Web.UI.WebControls.TextBox Suffix;
		///<summary/>
		protected System.Web.UI.WebControls.TextBox Extension;
		///<summary/>
		private bool _IncludeExtension = false;
		/// <summary>
		/// Set to true if the number should be formatted as (xxx) xxx-xxxx.  False
		/// as xxxxxxxxxx.
		/// </summary>
		public bool BellFormat = false;
		///<summary/>
		private string AreaCode { 
			get {return (!BellFormat)?Area_Code.Text:((Area_Code.Text=="")?"":("("+Area_Code.Text+") "));}
			set {Area_Code.Text = value.Replace("(","").Replace(")","");}
		}

		///<summary>Set to true to display as two columns instead of two rows.</summary>
		public bool Horizontal = false;
		///<summary>If true, the extension text box will display.</summary>	
		public bool IncludeExtension {get {return _IncludeExtension;} set {_IncludeExtension=value;}}
		///<summary>The complete formatted phone number</summary>
		public override string Text 
		{
			get {
				if (AreaCode=="" && Prefix.Text=="" && Suffix.Text=="" && Extension.Text=="") return "";
				return ((!BellFormat)
					?(AreaCode + Prefix.Text + Suffix.Text)
					:(AreaCode + Prefix.Text + "-" + Suffix.Text)) 
					  + ((IncludeExtension && Extension.Text!="")?("-" + Extension.Text):"");
			}
			set {
				string st = value;
				if (!BellFormat) {
					if (IncludeExtension && value.IndexOf("-")>-0) {
						Extension.Text = st.Substring(st.IndexOf("-")+1);
						st = st.Substring(0, st.IndexOf("-")-1);
					}
					if (st.Length==0) {
						AreaCode="";
						Prefix.Text="";
						Suffix.Text="";
						Extension.Text="";
					} else if (st.Length==7) {
						AreaCode="";
						Prefix.Text=st.Substring(0,3);
						Suffix.Text=st.Substring(3,4);
					} else {
						AreaCode=st.Substring(0,3);
						Prefix.Text=st.Substring(3,3);
						Suffix.Text=st.Substring(6,4);
						if (st.Length>10) Extension.Text=st.Substring(10);
					}
				} else {
					st = st.Replace("(","").Replace(")"," ").Replace("-"," ").Replace("  "," ");
					string [] s = st.Split(' ');
					if (s.Length==1) s = new string[] {"","","",""};
					if (s.Length>4) throw new Exception("Invalid phone number format: " + value);
					AreaCode = (s.Length==4 || (s.Length==3 && s[1].Length==3))?s[0]:"";
					Extension.Text = (s.Length==4 || (s.Length==3 && s[1].Length!=3))?s[s.Length-1]:"";
					Prefix.Text = (s.Length==4)?s[1]:(s.Length==2)?s[0]:(s.Length==3 && s[1].Length==3)?s[1]:s[0];
					Suffix.Text = (s.Length==4)?s[2]:(s.Length==2)?s[1]:(s.Length==3 && s[1].Length==3)?s[2]:s[1];
				}
			}
		}

		///<summary/>
		protected override bool CheckValue() 
		{
			bool CurFormat = BellFormat;
			BellFormat = true;
			bool result = CSAAWeb.Validate.IsValidPhone(Text);
			BellFormat = CurFormat;
			return result;
		}

//Added as a part of .NetMig 3.5
        protected void CheckValid1(object Source, ValidatorEventArgs e)
        {
            if ((Suffix.Text == "" || Prefix.Text == "") )
            {

                e.IsValid = false;
                Valid.ErrorMessage = "";
                Valid.MarkInvalid();

            }
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
}
