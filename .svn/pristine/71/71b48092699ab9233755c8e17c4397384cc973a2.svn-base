/*
 * MAIG - CH1 - Creation of new Regular Expression to check the First & Last Names
 * MAIG - CH2 - Including validations for First & Last names
 * MAIG - CH3 - Added logic to display an error message if the field is empty 11/19/2014
 * MAIG - CH4 - Added logic to display an error message if the field is empty 11/19/2014
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
    using System.Text.RegularExpressions;


	/// <summary>
	///	This control encapsulates the entry for a user name.
	/// </summary>
	public partial  class Name : ValidatingUserControl
	{
		///<summary/>
		protected System.Web.UI.WebControls.TextBox _FirstName;
		///<summary/>
		protected System.Web.UI.WebControls.TextBox _MiddleName;
		///<summary/>
		protected Validator vldLastName;
		///<summary/>
		protected Validator vldName;
		///<summary/>
		protected System.Web.UI.WebControls.TextBox _LastName;

		///<summary/>
		public string FirstName {get {return _FirstName.Text;} set {_FirstName.Text=value;}}
		///<summary/>
		public string MiddleName {get {return _MiddleName.Text;} set {_MiddleName.Text=value;}}
		///<summary/>
		public string LastName {get {return _LastName.Text;} set {_LastName.Text=value;}}
		///<summary/>
		public int MaxLength=0;
		///<summary/>
		public string ErrorMessage=string.Empty;

        #region RegexExpressions

        ///  Creation of new Regular Expression to check the First & Last Names
        /// </summary>
        //MAIG - CH1 - START - Creation of new Regular Expression to check the First & Last Names
        private static Regex regCheckName = new Regex("[^a-z '.,-]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        //MAIG - CH1 - END - Creation of new Regular Expression to check the First & Last Names
        
        #endregion

		///<summary>Validator for the total name length</summary>
		protected void CheckLength(Object Source, ValidatorEventArgs e) 
        {
            if (MaxLength==0) return;
			string Name = FirstName + " " + ((MiddleName=="")?"":(MiddleName+" "))
				+ LastName;
			if (Name.Length>MaxLength) {
				e.IsValid=false;
				vldName.ErrorMessage=ErrorMessage;
			}

		}

        //MAIG - CH2 - BEGIN - Including validations for First & Last names
        ///<summary>Validator for the First name </summary>
        protected void CheckFirstName(Object Source, ValidatorEventArgs e)
        {
            //MAIG - CH3 - BEGIN - Added logic to display an error message if the field is empty 11/19/2014
            if (string.IsNullOrEmpty(_FirstName.Text))
            {
                e.IsValid = false;
                vldFirstName.MarkInvalid();
                vldFirstName.ErrorMessage = Constants.FIRST_NAME_REQ;
            }
            //MAIG - CH3 - END - Added logic to display an error message if the field is empty 11/19/2014
            e.IsValid = !regCheckName.IsMatch(_FirstName.Text);
            if (!e.IsValid) vldFirstName.MarkInvalid();
            Validator vldFirst = (Validator)Source;
            vldFirst.ErrorMessage = "Invalid First Name";
        }

        ///<summary>Validator for the Last name </summary>
        protected void CheckLastName(Object Source, ValidatorEventArgs e)
        {
            //MAIG - CH4 - BEGIN - Added logic to display an error message if the field is empty 11/19/2014
            if (string.IsNullOrEmpty(_LastName.Text))
            {
                e.IsValid = false;
                vldLastName.MarkInvalid();
                vldLastName.ErrorMessage = Constants.NAME_SEARCH_LAST_NAME_REQ;
            }
            //MAIG - CH4 - END - Added logic to display an error message if the field is empty 11/19/2014
            e.IsValid = !regCheckName.IsMatch(_LastName.Text);
            if (!e.IsValid) vldLastName.MarkInvalid();
            Validator vldLast = (Validator)Source;
            vldLast.ErrorMessage = "Invalid Last Name";
        }
        //MAIG - CH2 - END - Including validations for First & Last names

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
