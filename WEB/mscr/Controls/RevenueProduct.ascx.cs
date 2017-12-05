//PAS AZ product configuration: Start added data view to sort the product type to be displayed in the product type drop down by cognizant on 03/15/2012
namespace MSCR.Controls
{
	using System;
	using System.Data;
	using System.Drawing;
	using System.Web;
	using System.Web.UI.WebControls;
	using System.Web.UI.HtmlControls;
	using OrderClasses.Service;

	/// <summary>
	///		Summary description for RevenueProduct.
	/// </summary>
	public partial  class RevenueProduct : System.Web.UI.UserControl
	{
		
		///<summary/>
		private DataTable RevenueTypes 
		{
			get {return (DataTable)Cache["INS_Revenue_Type"];}
		}
		///<summary/>
		private DataTable ProductTypes 
		{
			//get {return (DataTable)Cache["INS_Product_Type"];}
			  get {return (DataTable)Cache["All_INS_Product_Types"];}
		}

		///<summary>Identity for product type.</summary>
		public string ProductType 
		{
			get {return _ProductType.SelectedItem.Value;}
			set {_ProductType.SelectedItem.Value=value;}
		}
		///<summary>Identity for Revenue type.</summary>
		public string RevenueType 
		{
			get {return _RevenueType.SelectedItem.Value;}
			set {_RevenueType.SelectedItem.Value=value;}
		}

		///<summary>Display Text for product type.</summary>
		public string ProductTypeText 
		{
			get {return _ProductType.SelectedItem.Text;}
			set {_ProductType.SelectedItem.Text=value;}
		}
		///<summary>Display Text for Revenue type.</summary>
		public string RevenueTypeText 
		{
			get {return _RevenueType.SelectedItem.Text;}
			set {_RevenueType.SelectedItem.Text=value;}
		}

		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Put user code to initialize the page here
		}

		#region Web Form Designer generated code
		override protected void OnInit(EventArgs e)
		{
			//
			// CODEGEN: This call is required by the ASP.NET Web Form Designer.
			//
			InitializeComponent();
			base.OnInit(e);
			if (ProductTypes==null || RevenueTypes==null) Initialize();
			_ProductType.DataSource=ProductTypes;
			_ProductType.DataBind();
			_RevenueType.DataSource=RevenueTypes;
			_RevenueType.DataBind();

		}
		
		///		Required method for Designer support - do not modify
		///		the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{

		}
		#endregion

		/// <summary>
		/// Retrieves the insurance lookup tables from the web service and caches them.
		/// </summary>
		private void Initialize() 
		{
			lock (typeof(RevenueProduct))
			{
				Order DataConnection = new Order(Page);
				DataTable Dt;
//				if (Cache["INS_Product_Type"]==null) 
//				{
//					Dt = DataConnection.LookupDataSet("Insurance", "ProductTypes").Tables["INS_Product_Type"];
				if (Cache["All_INS_Product_Types"]==null) 
				{
                
					//START Modified by Cognizant on 12/10/2004 
					//To call the WebMethod GetAllProductTypes to fetch all Insurance Product Types (Both IIB and WU products) 
					Dt = DataConnection.LookupDataSet("Insurance", "GetAllProductTypes").Tables["All_INS_Product_Types"];
					//END
                    //PAS AZ product configuration: Start added data view to sort the product type to be displayed in the product type drop down by cognizant on 03/15/2012
                    DataView productView = Dt.DefaultView;
                    productView.Sort = "Description";
                    Dt = productView.ToTable();
                    //PAS AZ product configuration: End added data view to sort the product type to be displayed in the product type drop down by cognizant on 03/15/2012
					if (Dt.Rows.Count>1) 
					{
						DataRow Row = Dt.NewRow();
						Row["ID"]="-1";
						Row["Description"]="All";
						Dt.Rows.InsertAt(Row, 0);
					}
                  
					//Cache["INS_Product_Type"] = Dt;
					  Cache["All_INS_Product_Types"] = Dt;
				}
				if (Cache["INS_Revenue_Type"]==null) 
				{
					Dt = DataConnection.LookupDataSet("Insurance", "RevenueTypes").Tables["INS_Revenue_Type"];
					if (Dt.Rows.Count>1) 
					{
						DataRow Row = Dt.NewRow();
						Row["ID"]="-1";
						Row["Description"]="All";
						Dt.Rows.InsertAt(Row, 0);
					}
					Cache["INS_Revenue_Type"] = Dt;
				}
				DataConnection.Close();
			} 
		}
	}
}
