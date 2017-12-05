/*
 * STAR Retrofit II :
 * 1/31/2007  New Page to display Users list in Excel or HTML view is created 
 *            as a part of CSR#5166
 
 *  STAR Retrofit III Changes: 
 *  Modified as a part of STAR Retrofit III changes
 *  05/24/2007 STAR Retrofit III.Ch1: 
 *				Modified the code to hide the duplicate entry of DO Number in the Print and Excel versions
 * 
 * */

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using CSAAWeb;  
using CSAAWeb.WebControls;

using System.Collections.Specialized;

namespace MSC.Admin
{
	/// <summary>
	/// Summary description for Users_PrintXls.
	/// </summary>
	public partial class Users_PrintXls : System.Web.UI.Page
	{
		protected System.Web.UI.WebControls.Label lblUser;
		protected System.Web.UI.WebControls.Label lblUserType;
		private OrderClasses.Service.Order _OrderService=null;
		private OrderClasses.Service.Order OrderService {get {if (_OrderService==null) _OrderService=new OrderClasses.Service.Order(this); return _OrderService;}}
		const string ROW_CLASS = "White";
		const string ALTERNATE_ROW_CLASS = "LightYellow";
		string colour = ALTERNATE_ROW_CLASS;
		string Appln=string.Empty,DO=string.Empty,Status=string.Empty,UserId=string.Empty;
        string ApplName=string.Empty;
		int intStatus;
		bool Showall;
		protected void Page_Load(object sender, System.EventArgs e)
		{
			// Change Content type if user requests Excel format
			if (Request.QueryString["type"] == "XLS") 
			{
				Response.ContentType = "application/vnd.ms-excel";
				Response.Charset = ""; //Remove the charset from the Content-Type header.
			} 
			GetUsers();	
			Page.EnableViewState = false;
			
		}

		protected void rptUserRepeater_ItemBound(Object Sender, RepeaterItemEventArgs e)
		{
			
			AlternatingColour(e);
			
		}

		#region Get the User list
		private void GetUsers()
		{
			Appln=Request.QueryString["Appln"].ToString();
			OrderService.AppId = Appln;
			//Display All if there is no Application Name.
			if(Appln=="")
			{ 
				Showall=true;
				ApplName="All";
			}
			else
			{
				Showall=false;
				//Get the Application Description from the Cache.
				ApplName=((DataRow[])((DataTable)Cache["APP_NAMES"]).Select("ID='"+Appln+"'"))[0].ItemArray.GetValue(1).ToString();

			}
			
			DO=Request.QueryString["DO"].ToString();
			intStatus=Convert.ToInt16(Request.QueryString["Status"]);	
			UserId=Request.QueryString["UserId"].ToString();
			DataSet dsUsers;
			dsUsers = OrderService.LookupDataSet("Authentication", "GetUsersByStatus", new object[] {Showall, DO, UserId, intStatus});
			rptUserRepeater.DataSource=dsUsers;
			rptUserRepeater.DataBind();
				
			switch(intStatus)
			{
				case 0:  Status="InActive";
						 break;
				case 1:  Status="Active";
						 break;
				default: Status="All";
						 break;
			}
			
			if(UserId=="")
			{
				UserId="All";
			}
			//Get the District Office Name from the Cache, If its not empty.
			
			if(DO=="")
			{
				DO="All";
			}
			else
			{
				//STAR Retrofit III.Ch1: START - Modified the code to hide the duplicate entry of DO Number in the Print and Excel versions
				DO=((DataRow[])((DataTable)Cache["AUTH_DO"]).Select("ID='"+DO+"'"))[0].ItemArray.GetValue(1).ToString(); //+" - "+DO;
				//STAR Retrofit III.Ch1: END
			}
			
			lblAppname.Text=ApplName;
			lblRepdo.Text=DO;
			lblStatus.Text=Status;		
			lblUsers.Text=UserId;

		}
		#endregion
		
		#region Alternating color
		private void AlternatingColour(RepeaterItemEventArgs e)
		{
			
			if (e.Item.ItemType == ListItemType.Item || e.Item.ItemType == ListItemType.AlternatingItem) 
			{
				HtmlTableRow trTempItemRow=(HtmlTableRow) e.Item.FindControl("trItemRow");
				
				if ( colour == ROW_CLASS )
				{
					trTempItemRow.Attributes.Add("bgcolor",ALTERNATE_ROW_CLASS);
					colour = ALTERNATE_ROW_CLASS;
				}
				else if ( colour == ALTERNATE_ROW_CLASS )
				{
					trTempItemRow.Attributes.Add("bgcolor",ROW_CLASS);
					colour = ROW_CLASS;
				}
				
			}
		}
		#endregion
		
		#region Web Form Designer generated code
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
            this.Load += new System.EventHandler(this.Page_Load);
		}
		#endregion
	}
}
