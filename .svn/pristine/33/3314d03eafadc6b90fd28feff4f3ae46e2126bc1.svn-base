/*
 * * 1/25/2007 STAR Retrofit II - New Screen to add/Update a Parent/Branch Office.
 * .NetMig3.5-Ch1:Added a new function  ReqValCheck to validate the fields on 02-02-2010.
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
using OrderClasses.Service;
using AuthenticationClasses;
using CSAAWeb.WebControls;
using CSAAWeb.Serializers;
using System.Text.RegularExpressions;
//67811A0  - PCI Remediation for Payment systems - Arcsight logging
using CSAAWeb.AppLogger;

namespace MSC.Admin
{
    /// <summary>
    /// Summary description for DO
    /// </summary>
    public partial class DO : SiteTemplate
    {

        #region ASP Page element declarations
        ///<summary/>
        protected TextBox _DOName;
        ///<summary/>
        protected TextBox _DOnum;
        ///<summary/>
        protected DropDownList _HUB;
        ///<summary/>
        protected CheckBox _Active;
        ///<summary/>
        protected ImageButton CancelDO;
        ///<summary/>
        protected Label Caption;
        ///<summary/>
        protected Label lblErrorMsg;
        ///<summary/>
        protected Validator _vldpage;
        ///<summary/>
        protected Validator _vldDOs;
        ///<summary/>
        protected Validator _vldDOs1;
        ///<summary/>
        protected Validator _vldDOnum;
        ///<summary/>
        protected Validator _vldDOnum1;
        ///<summary/>
        protected HiddenInput _DORid;
        /// <summary>
        protected HtmlImage imgErrorFlag;


        #endregion
        #region Public Properties
        ///<summary/>
        public int DORid { get { return (_DORid.Text == "") ? 0 : Convert.ToInt32(_DORid.Text); } set { _DORid.Text = value.ToString(); } }
        ///<summary/>
        public string DOName { get { return _DOName.Text; } set { _DOName.Text = value; } }
        ///<summary/>
        public string DOID { get { return _DOnum.Text; } set { _DOnum.Text = value; } }
        ///<summary/>
        public bool Active { get { return _Active.Checked; } set { _Active.Checked = value; } }
        ///<summary/>
        public string HUB { get { return ListC.GetListValue(_HUB); } set { ListC.SetListIndex(_HUB, value); } }
        #endregion

        private string currentUser = string.Empty;
        private static Regex AlphaNumeric2 = new Regex("[^a-z0-9 ]+", RegexOptions.Compiled | RegexOptions.IgnoreCase);
        AuthenticationClasses.Service.Authentication auth = new AuthenticationClasses.Service.Authentication();

        /// <summary>
        /// Retrieves the lookup tables from the web service and caches them.
        /// </summary>
        private void AddSelectItem(DataTable Dt, string Description)
        {
            DataRow Row = Dt.NewRow();
            Row["ID"] = "";
            Row["Description"] = Description;
            Dt.Rows.InsertAt(Row, 0);
        }

        private void Initialize()
        {
            lock (typeof(User))
            {

                DataTable Dt;
                try
                {
                    Dt = OrderService.LookupDataSet("Authentication", "GetAllDOs").Tables[0];
                    if (Dt.Rows.Count > 1) AddSelectItem(Dt, "None");
                    Cache["AUTH_HUB"] = Dt;
                }
                catch { throw; }
            }
        }


        ///<summary>Returns true is s contains an alphanumericspace sequence</summary>
        private bool IsAlphaNumericSpace(string s)
        {
            return !AlphaNumeric2.IsMatch(s);
        }

        ///<summary>Validator delegate for the page</summary>
        protected void CheckValid(object Source, ValidatorEventArgs e)
        {
            e.IsValid = (_vldpage.ErrorMessage == "");
        }

        ///<summary>Validator delegate for the DO Name</summary>
        protected void CheckDOName(object Source, ValidatorEventArgs e)
        {

            if (_vldDOs.IsValid)

                e.IsValid = IsAlphaNumericSpace(DOName);
            if (!e.IsValid)
                _vldDOs.MarkInvalid();



        }
        protected void CheckLength(object Source, ValidatorEventArgs e)
        {

            if (_DOName.Text == "")
            {



                e.IsValid = false;


                _vldDOs.MarkInvalid();

                _vldDOs.ErrorMessage = "";




            }


        }

        ///<summary>Validator delegate for the DO Number</summary>
        protected void CheckDONum(object Source, ValidatorEventArgs e)
        {


            if (_vldDOnum.IsValid)
                e.IsValid = CSAAWeb.Validate.IsAllNumeric(DOID);
            if (!e.IsValid) _vldDOnum.MarkInvalid();
        }



        /// <summary>
        /// Transfers control back to DOs page, or restores this page with an error message.
        /// </summary>
        /// <param name="ErrorMessage">The error message to display.</param>
        /// <param name="Message">Message to display on transfer.</param>
        private void Continue(string ErrorMessage, string Message)
        {
            if (ErrorMessage == "")
            {
                if (Message != "")
                {
                    Context.Items.Add("Message", Message);
                }
                //67811A0 START - PCI Remediation for Payment systems :Arcsight logging - To log the details of DO's Addition
                else
                {
                    Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DEVICEACTION_DO;
                    Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS;
                    Logger.SourceUserName = User.Identity.Name;
                    Logger.SourceProcessName = CSAAWeb.Constants.PCI_SOURCE_PROCESS_NAME;
                    Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_LOW;
                    Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_DEVICEACTION_DO_ADD;
                    Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_DO_ADD;
                    Logger.ArcsightLog();
                }
                //67811A0 END - PCI Remediation for Payment systems :Arcsight logging - To log the details of DO's Addition

                Context.Items.Add("HUB", HUB);
                ShowAll.SaveContext();
                Server.Transfer("DOs.aspx");
            }
            else
            {
                _vldpage.ErrorMessage = ErrorMessage;
                _vldpage.MarkInvalid();

            }
        }

        protected void Page_Load(object sender, System.EventArgs e)
        {
            currentUser = Page.User.Identity.Name;
            if (Cache["AUTH_HUB"] == null)
            {
                Initialize();//loads the data into cache
            }
            if (!IsPostBack)
            {
                _HUB.DataSource = (DataTable)Cache["AUTH_HUB"];
                _HUB.DataBind();
                DORid = (int)Context.Items["DORid"];
                if (DORid != 0)
                {
                    Caption.Text = "Edit Branch Office";
                    string DO_Code = (string)Context.Items["DOID"];
                    GetDOdetails(DO_Code);
                }
                else if (Context.Items.Contains("HUB"))
                    CSAAWeb.WebControls.ListC.SetListIndex(_HUB, (string)Context.Items["HUB"]);

            }
        }

        private void GetDOdetails(string DO_code)
        {
            DataSet ds = auth.GetDOdetails(DO_code);
            if (ds.Tables[0].Rows.Count != 0)
            {
                bool flag = Convert.ToBoolean(ds.Tables[0].Rows[0]["Enabled"]);
                if (ds.Tables[0].Columns.Count == 3) // HUB office
                {
                    _DOnum.Text = ds.Tables[0].Rows[0]["ID"].ToString();
                    _DOName.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                    if (flag == true)
                        _Active.Checked = true;
                    else
                        _Active.Checked = false;

                    _HUB.Enabled = false;
                    _DOnum.Enabled = false;
                }
                else // Branch Office
                {
                    _DOnum.Text = ds.Tables[0].Rows[0]["ID"].ToString();
                    _DOName.Text = ds.Tables[0].Rows[0]["Description"].ToString();

                    if (flag == true)
                        _Active.Checked = true;
                    else
                        _Active.Checked = false;

                    _HUB.DataValueField = ds.Tables[0].Rows[0]["HUB"].ToString();
                    CSAAWeb.WebControls.ListC.SetListIndex(_HUB, _HUB.DataValueField);
                    _DOnum.Enabled = false;
                }
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

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {

        }
        #endregion

        protected void CancelDO_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            Continue("", "");
        }

        protected void UpdateDO_Click(object sender, System.Web.UI.ImageClickEventArgs e)
        {
            if (Page.IsValid)
            {
                NavACL.ResetNav();
                ArrayOfErrorInfo Result = new ArrayOfErrorInfo();
                Result = auth.UpdateDO(DORid, DOName, DOID, HUB, Active, currentUser);
                if (Result != null)
                {
                    string Msg = Result[0].Message.ToString();
                    //67811A0 START - PCI Remediation for Payment systems :Arcsight logging - To log the details of DO's Addition (on Failure)
                    Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DEVICEACTION_DO;
                    Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_FAILURE;
                    Logger.SourceUserName = currentUser;
                    Logger.SourceProcessName = CSAAWeb.Constants.PCI_SOURCE_PROCESS_NAME;
                    Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_HIGH;
                    Logger.DeviceAction = Msg;
                    Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_DO_ADD_FAILED;
                    Logger.ArcsightLog();
                    //67811A0 END - PCI Remediation for Payment systems :Arcsight logging -To log the details of DO's Addition (on Failure)

                    Continue(Msg, "");

                }
                else
                {
                    //Clear the cache to load the updated data.
                    Cache.Remove("AUTH_HUB");
                    Cache.Remove("AUTH_AllDOs");
                    Cache.Remove("AUTH_DO");
                    Cache.Remove("AUTH_REPDO");
                    //67811A0 START - PCI Remediation for Payment systems :Arcsight logging To log the details of DO's Addition/Editing (on Success)
                    if (this.Caption.Text == "Add Branch Office")
                    {
                        Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DEVICEACTION_DO;
                        Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS;
                        Logger.SourceUserName = currentUser;
                        Logger.SourceProcessName = CSAAWeb.Constants.PCI_SOURCE_PROCESS_NAME;
                        Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_LOW;
                        Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_NAME_ADD_DO;
                        Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_DO_ADD;
                        Logger.ArcsightLog();
                    }
                    else if (this.Caption.Text == "Edit Branch Office")
                    {
                        Logger.DestinationProcessName = CSAAWeb.Constants.PCI_ARC_DEVICEACTION_DO;
                        Logger.DeviceEventCategory = CSAAWeb.Constants.PCI_ARC_DEVICEEVENTCATEGORY_SUCCESS;
                        Logger.SourceProcessName = CSAAWeb.Constants.PCI_SOURCE_PROCESS_NAME;
                        Logger.DeviceSeverity = CSAAWeb.Constants.PCI_ARC_SEVERITY_LOW;
                        Logger.SourceUserName = currentUser;
                        Logger.DeviceAction = CSAAWeb.Constants.PCI_ARC_NAME_EDIT_DO;
                        Logger.Name = CSAAWeb.Constants.PCI_ARC_NAME_DO_EDIT1;

                        Logger.ArcsightLog();
                    }
                    //67811A0 END - PCI Remediation for Payment systems :Arcsight logging - Arcsight logging To log the details of DO's Addition/Editing (on Success)

                    Continue("", DOName + " " + "Branch Office" + ((DORid == 0) ? " has been Created." : " details has been Updated."));
                }

            }

        }


        //.NetMig.Ch1:START - Added function to validate fields for required field
        protected void ReqValCheck(Object source, ValidatorEventArgs args)
        {
            //string strID = ((Validator)source).ID;
            //strID = strID.Substring(0, strID.Length - 1);
            //Validator vldControl = (Validator)(this.FindControl(strID));
            //args.IsValid = (((TextBox)args.Check).Text != "" || false);
            //if (!args.IsValid)
            //{
            //    vldControl.MarkInvalid();
            //    args.IsValid = false;

            //    _vldDOs.MarkInvalid();
            //    _vldDOnum.MarkInvalid();

            //}

            //if (_DOName.Text == "" && _DOnum.Text =="")
            //{
            //    //args.IsValid = false;
            //    _vldDOs.MarkInvalid();
            //    _vldDOnum.MarkInvalid();
            //}
            if (_DOnum.Text == "")
            {
                _vldDOnum.MarkInvalid();


            }

            if (_DOName.Text == "")
            {

                _vldDOs.MarkInvalid();

            }

        }

        //.NetMig.Ch1:END


    }
}
