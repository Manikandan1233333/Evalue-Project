using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSAAWeb.WebControls;
using System.Text.RegularExpressions;

namespace MSC
{
    public partial class SearchResults : System.Web.UI.Page
    {

        private static Regex regCheckName = new Regex("[^a-z0-9 '-]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void CheckFirstName(object source, CSAAWeb.WebControls.ValidatorEventArgs e)
        {
            e.IsValid = !regCheckName.IsMatch(txtFirstName.Text);
            if (!e.IsValid) vldFirstName.MarkInvalid();
            Validator vldFirst = (Validator)source;
            vldFirst.ErrorMessage = " Member First Name must be alphanumeric";
        }

        protected void ReqValZipCheck(object source, ValidatorEventArgs e)
        {
            e.IsValid = ((txtMailingZip.Text != "" && CSAAWeb.Validate.IsValidZip(txtMailingZip.Text)) || false);
            if (!e.IsValid) vldMailingZip.MarkInvalid();
        }

        protected void CancelButton_Click(object sender, ImageClickEventArgs e)
        {

        }

        protected void ContinueButton_Click(object sender, ImageClickEventArgs e)
        {

        }
    }
}
