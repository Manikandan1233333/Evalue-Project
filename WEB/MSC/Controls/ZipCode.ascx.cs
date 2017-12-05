/*
 *History:
 *Added a new method ReqValCheck() to validate the zip code as a part of cognizant on 04-02-2010.
 * MAIG - CH1 - Added logic to display an error message if the field is empty 11/19/2014
 */
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
	///	Encapsulates the entry and validation for a zip code.
	/// </summary>
	public partial  class Zip : ValidatingUserControl
	{
		///<summary/>
		protected System.Web.UI.WebControls.TextBox ZipCode;
		///<summary/>
		protected System.Web.UI.WebControls.TextBox ExtendedZipCode;
		///<summary/>
		protected Validator Valid;
		///<summary/>
		public override string Text {
			get { return ZipCode.Text + ((Use9Digit && ExtendedZipCode.Text!="")?("-"+ExtendedZipCode.Text):"");}
			set {
				string [] s = value.Split('-');
				ZipCode.Text = s[0].Replace(" ","");
				ExtendedZipCode.Text = (s.Length==1)?"":s[1].Replace(" ","");
			}
		}
		/// <summary>
		/// True if the zip code should be validated against the club's service area.
		/// </summary>
		public bool VerifyClub = false;
		///<summary>True if the extended zip code box should display</summary>
		public bool Use9Digit = false;
		/// <summary>
		/// Set to the state code if the zip should be matched to see if the state
		/// matches.
		/// </summary>
		public string StateToCheck = "";

		///<summary>
		///Checks the zip code for a number of conditions.  First validates the format.
		///Then, if the StateToCheck property is set, checks to see if the zip and state
		///match.  Finally, if the VerifyClub property is true, verifies that the zip
		///is in the club's service area.  If any of theses fail, returns false.  May
		///change the error message for the validator Valid to reflect the actual error.
		///</summary>
        //protected override bool CheckValue()
        //{
        //    if (!CSAAWeb.Validate.IsValidZip(Text)) return false;
        //    if (!VerifyClub || StateToCheck=="") return true;
        //    MembershipClasses.Service.Membership S = new MembershipClasses.Service.Membership();
        //    if (StateToCheck!="") {
        //        MembershipClasses.CityState C = S.GetCityState(Text);
        //        if (C.State!=StateToCheck) {
        //            Valid.ErrorMessage = "Zip code is not in the selected state.";
        //            S.Close();
        //            return false;
        //        }
        //    }
        //    if (S.Check_Zip(Text)=="") {
        //        Valid.ErrorMessage = "Zip code is not in the club service area.";
        //        S.Close();
        //        return false;
        //    }
        //    S.Close();
        //    return true;
        //}
        //Added a new method as a part of .NetMig 3.5 0n 04-02-2010.
        protected void ReqValCheck(Object source, ValidatorEventArgs args)
        {
            
            if (ZipCode.Text == "")
            {
                LabelValidator.MarkInvalid();
                //MAIG - CH1 - BEGIN - Added logic to display an error message if the field is empty 11/19/2014
                LabelValidator.ErrorMessage = Constants.NAME_SEARCH_MAILING_ZIP_REQ;
                //MAIG - CH1 - END - Added logic to display an error message if the field is empty 11/19/2014
                args.IsValid = false;
               
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
