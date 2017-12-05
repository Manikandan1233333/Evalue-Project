/* REVISION HISTORY:
 *	MODIFIED BY COGNIZANT
 *  STAR Retrofit III Changes: 
 *  Modified as a part of CSR #5595
 *  04/02/2007 STAR Retrofit III.Ch1: 
 *				Code added in page load event to populate Year drop down with current year + past 2 years
 *	04/02/2007 STAR Retrofit III.Ch2: 
 *				Modified the index parameter to set the current year selected by default
 *                Modified the filling of date drop down list to use FillNumericComboBox() instead of DynamicDate().
 *  04/02/2007 STAR Retrofit III.Ch3:
 *				Commented the SelectedIndexChanged events of month selection to prevent roundtrips to server
 *	04/02/2007 STAR Retrofit III.Ch4:
 *				Added custom validators for start date(day,month,year) and end date(day,month,year)dropdown control.
 *	04/02/2007 STAR Retrofit III.Ch5: 
 *				Modified the index parameter for the day dropdown of end date selector to set the last day of the current month 
 *                and year selected by default during page load.
 * */
namespace MSCR.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;

	/// <summary>
	///		Summary description for DateTime.
	/// </summary>
	public partial  class Dates : System.Web.UI.UserControl
	{
		//STAR Retrofit III.Ch4: START Added custom validators to invoke date validation function 
		//STAR Retrofit III.Ch4: END


		MSCR.Reports.InsRptLibrary rptLib = new MSCR.Reports.InsRptLibrary();

		///<summary/>
		public DateTime StartDate {get {
			string start_date = start_date_month.SelectedItem.Value+'/'+start_date_day.SelectedItem.Value +'/'+ start_date_year.SelectedItem.Value;
			DateTime dt_start_date = DateTime.Parse(start_date);
			double dt_start_hour	= Convert.ToDouble(start_time_hour.SelectedItem.Value);
			double dt_start_minute  = Convert.ToDouble(start_time_minute.SelectedItem.Value);
			return dt_start_date.AddHours(dt_start_hour).AddMinutes(dt_start_minute);
		} 
		set {StartDate=value;}
		}
		///<summary/>
		public DateTime EndDate 
		{
			get {
				string end_date = end_date_month.SelectedItem.Value+'/'+end_date_day.SelectedItem.Value+'/'+end_date_year.SelectedItem.Value;
				DateTime dt_end_date = DateTime.Parse(end_date);
				double dt_end_hour	= Convert.ToDouble(end_time_hour.SelectedItem.Value);
				double dt_end_minute  = Convert.ToDouble(end_time_minute.SelectedItem.Value);
				return dt_end_date.AddHours(dt_end_hour).AddMinutes(dt_end_minute);
				//return EndDate;
				} 
			set {EndDate=value;}
		}
				
		///<summary>
		///True if this is a billing address, in which case Address2, City and
		///state aren't displayed
		///</summary>

		protected void Page_Load(object sender, System.EventArgs e)
		{
			if (!Page.IsPostBack)
			{
				//STAR Retrofit III.Ch1: START - Code added to populate Year drop down with current year + past 2 years
				int CurrentYear = DateTime.Now.Year;
				int[] arr_year = new int[3]; 
				for(int i=0;i<arr_year.Length;i++)
				{
					arr_year[i] = CurrentYear - 2;
					CurrentYear++;
				}
				start_date_year.DataSource = arr_year;
				start_date_year.DataBind();

				end_date_year.DataSource = arr_year;
				end_date_year.DataBind();
				//STAR Retrofit III.Ch1: END

				rptLib.FillNumericComboBox(start_date_month,1,12);
				rptLib.FillNumericComboBox(end_date_month,1,12);

				start_date_month.SelectedIndex = DateTime.Now.Month - 1;
				end_date_month.SelectedIndex = DateTime.Now.Month - 1;

				//STAR Retrofit III.Ch2: START - Modified the index parameter to set the current year selected by default
				//                      Modified the filling of date drop down list.
				start_date_year.SelectedIndex= 2;
				end_date_year.SelectedIndex = 2;

				rptLib.FillNumericComboBox(start_date_day,1,31);
				rptLib.FillNumericComboBox(end_date_day,1,31);
			
				//rptLib.DynamicDate(start_date_day, start_date_month, start_date_year);
				//rptLib.DynamicDate(end_date_day, end_date_month, end_date_year);
				
				//STAR Retrofit III.Ch2: END
				
				//STAR Retrofit III.Ch5: START - Modified the index parameter to set the last day of the current month and year 
				int numberOfDays = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
				DateTime lastDay = new DateTime(DateTime.Now.Year,DateTime.Now.Month, numberOfDays);
				int last_date=lastDay.Day;
				end_date_day.SelectedIndex = end_date_day.Items.IndexOf(end_date_day.Items.FindByText(last_date.ToString()));
				//end_date_day.SelectedIndex = end_date_day.Items.Count - 1;
				//STAR Retrofit III.Ch5: END

				rptLib.FillNumericComboBox(start_time_hour,0,23);
				rptLib.FillNumericComboBox(end_time_hour,0,23,23);
				rptLib.FillNumericComboBox(start_time_minute,0,59);
				rptLib.FillNumericComboBox(end_time_minute,0,59,59);
			}
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
			// Put user code to initialize the page here

		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		//STAR Retrofit III.Ch3: START - Commented the SelectedIndexChanged events of month selection to prevent roundtrips to server
//		private void start_date_month_SelectedIndexChanged(object sender, System.EventArgs e)
//		{
//			rptLib.DynamicDate(start_date_day, start_date_month, start_date_year);
//		}
//
//		private void end_date_month_SelectedIndexChanged(object sender, System.EventArgs e)
//		{
//			rptLib.DynamicDate(end_date_day, end_date_month, end_date_year);
//			end_date_day.SelectedIndex = end_date_day.Items.Count - 1;
//		}
		//STAR Retrofit III.Ch3: END


	}
}
