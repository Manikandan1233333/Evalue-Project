
/* 
 * MODIFIED BY COGNIZANT AS PART OF .NET MIGRATION CHANGES
 * CHANGES DONE
 * 04/04/2010 .NetMig.Ch1: Added new function for validating for require field
 * 04/04/2010 .NetMig.Ch2: Added new condition to mark policy as invalid
 *04/04/2010 .NetMig.Ch3: Added new condition to assign IsValid Property
 * 
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
	///		Summary description for PolicyAmount.
	/// </summary>
	public partial  class PolicyAmount : ValidatingUserControl
	{
		///<summary></summary>
		protected TextBox _Amount;
		///<summary/>
		protected Validator vldAmountDecimal;
		///<summary/>
		protected Validator vldAmountPositive;
		///<summary/>
		protected Validator Valid;

		///<summary>The amount of the payment as text.</summary>
		public override string Text {
			get{ return _Amount.Text;}
			set{ _Amount.Text=value;}
		}


        //.NetMig.Ch1: Added function to mark Amount label as invalid  - START
        public void AmountInValid()
        {
            Validator AmountLabelValidator = (Validator)FindControl("LabelValidator");
            AmountLabelValidator.MarkInvalid();
        }
        //.NetMig.Ch1 - END
		///<summary>The amount of the payment.</summary>
		public decimal Value {get {return (Text!=string.Empty && RegexDecimal.IsMatch(Text))?Convert.ToDecimal(Text):0;}
			set {Text=(value==0)?"":value.ToString();}
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
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		///<summary>Validator function insurse that amount is positive.</summary>
		protected void PositiveAmount(Object Source, ValidatorEventArgs args) {
			if (Text!="") args.IsValid = (!vldAmountDecimal.IsValid || Value>0);
            //.NetMig.Ch2: Added new condition to mark amount as invalid
            if (!args.IsValid) AmountInValid();
		}

        //pci
        //.NetMig.Ch3: Added function for checking for required field - START
        //protected void ReqValCheck(Object source, ValidatorEventArgs args)
        //{
                     
        //    args.IsValid = (Text != "" || false);
        //    if (!args.IsValid) AmountInValid();
        //}

        //.NetMig.Ch3 - END

		private static Regex RegexDecimal = new Regex("(?:^\\-{0,1}[0-9]+\\.{0,1}[0-9]{0,2}$|^\\-?[0-9]*\\.?[0-9]{1,2}$)", RegexOptions.Compiled);
		///<summary>Validator function insurse that amount is positive.</summary>
		protected void WholeCents(Object Source, ValidatorEventArgs args) {
            if (Text != "") args.IsValid = ( RegexDecimal.IsMatch(Text));
            //.NetMig.Ch2: Added new condition to mark amount as invalid
            if (!args.IsValid) AmountInValid();
		}

            
       

	}
}
