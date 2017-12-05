/*
 * 1/25/2007 STAR Retrofit II - New Screen to administrate Branch Office.
 * //67811A0 - PCI Remediation for Payment systems :Arcsight logging
 * SSO Integration - CH1 -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
 */
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
using CSAAWeb.WebControls;
//67811A0 - PCI Remediation for Payment systems :Arcsight logging
using CSAAWeb.AppLogger;

namespace MSC.Admin
{
    /// <summary>
    /// Summary description for DOs.
    /// </summary>
    public partial class DOs : PageTemplate
    {
        private OrderClasses.Service.Order _OrderService = null;
        private OrderClasses.Service.Order OrderService { get { if (_OrderService == null) _OrderService = new OrderClasses.Service.Order(this); return _OrderService; } }
        protected System.Web.UI.WebControls.DataGrid UsersGrid;

        ///<summary>DO value.</summary>
        public string DO
        {
            get { return ListC.GetListValue(_DO); }
            set { ListC.SetListIndex(_DO, value); }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            // SSO Integration - CH1 Start -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
            Response.Cache.SetCacheability(HttpCacheability.NoCache);
            Response.Cache.SetExpires(DateTime.Now.AddSeconds(-1));
            Response.Cache.SetNoStore();
            // SSO Integration - CH1 End -Added the below code to clear the cache of the page to prevent the page loading on browser back button hit 
            if (Cache["AUTH_AllDOs"] == null)
            {
                Initialize();
            }
            if (IsPostBack)
            {
                string st = Request.Form["DO_Id"];
                if (st != null && st != "")
                {
                    if (st == "ins")
                    {
                        Context.Items.Add("DORid", Convert.ToInt32("0"));
                        Context.Items.Add("DOID", "");
                    }
                    else
                    {
                        Context.Items.Add("DORid", Convert.ToInt32("1"));
                        Context.Items.Add("DOID", st);
                    }
                    Context.Items.Add("DO", DO);
                    Server.Transfer("DO.aspx");
                }
                else
                {
                    GetInfo();
                }
            }
            else
            {
                _DO.DataSource = (DataTable)Cache["AUTH_AllDOs"];
                _DO.DataBind();
                lblmsg.Visible = true;
                if (Context.Items.Contains("DO"))
                {
                    DO = (string)Context.Items["DO"];
                }
                if (Context.Items.Contains("Message"))
                {
                    Message.Text = (string)Context.Items["Message"];
                    MessageTable.Visible = true;
                }
                GetInfo();
            }
        }
        /// <summary>
        /// Retrieves the Authentication look up tables from the web service and binds the data 
        /// to the datagrid. This function also controls the display of error messages.
        /// </summary>
        private void GetInfo()
        {
            DataSet DS = OrderService.LookupDataSet("Authentication", "GetListofDOs", new object[] { DO });
            lblErrMsg.Visible = false;
            lblmsg.Visible = true;
            DOsGrid.DataSource = DS;
            DOsGrid.DataBind();
            //67811A0 START - PCI Remediation for Payment systems :Arcsight logging - DO Addition details logging
            Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DEVICEACTION_DO;
            Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS;
            Logger.SourceProcessName = CSAAWeb.Constants.PCI_SOURCE_PROCESS_NAME;
            Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_LOW;
            Logger.SourceUserName = User.Identity.Name;
            Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_DO_NAME;
            Logger.Name = CSAAWeb.Constants.PCI_ARC_DO_VIEW_NAME;
            Logger.ArcsightLog();
            //67811A0 END - PCI Remediation for Payment systems :Arcsight logging - DO Addition details logging

        }
        ///<summary>Dummy method to force postback.This method is triggered whenever a HUB is selected from the drop down.</summary>
        protected void Force_Postback(object sender, System.EventArgs e)
        {
        }
        /// <summary>
        /// Retrieves the insurance lookup tables from the web service and caches them.
        /// </summary>
        private void Initialize()
        {
            lock (typeof(User))
            {
                DataTable Dt;
                try
                {
                    Dt = OrderService.LookupDataSet("Authentication", "GetAllDOs").Tables[0];
                    if (Dt.Rows.Count > 1) AddSelectItem(Dt, "All");
                    Cache["AUTH_AllDOs"] = Dt;
                }
                catch { throw; }
            }
        }
        /// <summary>
        /// Adds a Selected Item row to the table.
        /// </summary>
        private void AddSelectItem(DataTable Dt, string Description)
        {
            DataRow Row = Dt.NewRow();
            Row["ID"] = "";
            Row["Description"] = Description;
            Dt.Rows.InsertAt(Row, 0);
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ID = "DOs";

        }
        #endregion
    }
}
