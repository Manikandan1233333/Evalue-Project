namespace MSC.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using OrderClasses.Service;

	/// <summary>
	///	Summary description for PaymentType.
	/// </summary>
	
//	public delegate void test(object sender,System.EventArgs e);

	public partial  class PaymentTypeControl : System.Web.UI.UserControl
	{
		private string mColumnFlag;
		private string mAddStringName = "All";
//		public event test testEvent;
//		protected virtual void OntestEvent(object sender,System.EventArgs e)
//		{
//			testEvent(sender,e);
//		}

		///<summary>To Store Pay_Payment_Type data in th Cache</summary> />
		private DataTable GetPaymentType 
		{
			get 
			{
				return (DataTable)Cache["PAY_Payment_Type"];
			}
		}

		///<summary>Property to Get and Set the Value to the Payment Type</summary> />	
		public string PaymentType
		{
			get 
			{
				return _PaymentType.SelectedItem.Value ;
			}
			set 
			{
				_PaymentType.SelectedItem.Value = value;
			}
		}

		///<summary>Property to Get and Set the Text to the Payment Type</summary> />	
		public string PaymentTypeText 
		{
			get 
			{
				return _PaymentType.SelectedItem.Text  ;
			}
			set 
			{
				_PaymentType.SelectedItem.Text  = value;
			}
		}

		///<summary>Property to Get the ColumnFlag for which the Payment Type Gets added to the Control </summary> />	
		public string ColumnFlag
		{
			set
			{
				mColumnFlag  = value;
			}
		}

		///<summary>Property to Get whether to Add String Name in the dropdown List </summary> />	
		public string AddStringName
		{
			set
			{
				mAddStringName = value ;
			}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
			if (mColumnFlag != "P")
			{
				lblPaymentName.Visible = false;
				lblReportName.Visible = true;
			}
			else
			{
				lblPaymentName.Visible = true;
				lblReportName.Visible = false;				
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
		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		/// <summary>
		/// Retrieves the Payment Types Data from the web service and caches them.
		/// </summary>
		private void Initialize() 
		{
			Order DataConnection = new Order(Page);
			DataTable dtbPaymentType;
			// if ColumnFlag is null then default Report column is added
			if (mColumnFlag == null) 
				mColumnFlag = "R";

			dtbPaymentType = DataConnection.LookupDataSet("Payment", "GetPaymentType",new object[] {mColumnFlag}).Tables["PAY_Payment_Type"];
			if (dtbPaymentType.Rows.Count>1) 
			{
				DataRow drPaymentType = dtbPaymentType.NewRow();
				drPaymentType["ID"]="-1";
				drPaymentType["Description"]=  mAddStringName;
				dtbPaymentType.Rows.InsertAt(drPaymentType, 0);
			}
			Cache["PAY_Payment_Type"] = dtbPaymentType;
			DataConnection.Close();
		}

		protected void _PaymentType_PreRender(object sender, System.EventArgs e)
		{
			Initialize();
			_PaymentType.DataSource= GetPaymentType ;
			_PaymentType.DataBind();
		}

//		private void _paymenttype_selectedindexchanged(object sender, system.eventargs e)
//		{
//			ontestevent(this,e);
//		}
	}
}