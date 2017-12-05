/*
 * HISTORY
 * PC Phase II changes - Added the page as part of Enrollment changes.Confirmation page for un-enrollment
 * Created by: Cognizant on 2/22/2013
 * Decsription: This is a new file added as part of Payment Central Phase II.
*/
//PC Phase II Ch1 - Modified code as a part to trigger mail on UnEnrollment 
//CHG0109406 - CH1 - Modified the below timestamp to include the Arizona
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;
using CSAAWeb;

namespace MSC.Forms
{
    public partial class UnEnrollmentConfirm : SiteTemplate
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            List<string> validateMessage;
            //CHG0109406 - CH1 - BEGIN - Modified the below timestamp to include the Arizona
            lblUnDate.Text = DateTime.Now.ToString(CSAAWeb.Constants.FULL_DATE_TIME_FORMAT)+ " Arizona";
            //CHG0109406 - CH1 - END - Modified the below timestamp to include the Arizona
            if (Context.Items.Contains("validateUnEnroll"))
            {
                var validateEnroll = Context.Items["validateUnEnroll"];
                validateMessage = (List<string>)(validateEnroll);
                if (validateMessage.Count >= 2)
                {
                    if (validateMessage[1].Equals(CSAAWeb.Constants.PC_YES_NOTATION) && validateMessage[0] != string.Empty)
                    {
                        lblValMessage.Visible = true;
                        lblValMessage.Text = validateMessage[0].ToString();
                    }
                }


            }
        }
        protected void imgUnManageEnrollment_onclick(object sender, ImageClickEventArgs e)
        {
            Response.Redirect(CSAAWeb.Constants.PC_ENROLL_REDIRECT_URL);
        }
       
    }
}
