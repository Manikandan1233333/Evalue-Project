/*HISTORY
 * PC Security Defect Fix -CH1 - Modified the below code to add logging inside the catch block
 * MAIG - CH1 - Added the Enum to refer the Product Codes

*/
using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Reflection;
using CSAAWeb;
using CSAAWeb.WebControls;
//CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the method with respect to Membership - March 2016
using CSAAWeb.AppLogger;
namespace MSC
{
	/// <summary>
	/// Base class for most of the pages in the site.  Derives from CSAAWeb.WebControls.PageTemplate
	/// to assure that the site's template is followed.  This class captures most of the
	/// events on the page to ensure exception handling.  It contains a control to place
	/// the OrderControl into, allowing access by any page.  It contains properties to 
	/// capture navigation URLs for back and forward navigation from the config file, and
	/// creates new virtual methods for important events that occur and must be handled
	/// by most of the derived classes.
	/// </summary>
	public class SiteTemplate : PageTemplate
	{
		private static string _OnCancelUrl = string.Empty;
		///<summary/>
		public OrderControl Order;
      //MAIG - CH1 - BEGIN - Added the Enum to refer the Product Codes
        public enum ProductCodes
        {
            PA=0,
            HO,
            PU,
            DF,
            WC,
            MC
        }
        //MAIG - CH1 - END - Added the Enum to refer the Product Codes

		private OrderClasses.Service.Order _OrderService=null;
		///<summary/>
		///Modified by Cognizant on 09/06/2004 to View the Design Mode
		
		public OrderClasses.Service.Order OrderService {get {if (_OrderService==null) _OrderService=new OrderClasses.Service.Order(this); return _OrderService;}}

		///<summary/>
		static SiteTemplate() 
		{
			_OnCancelUrl = Config.Setting("SiteTemplate_OnCancelUrl");
		}

		/// <summary>
		/// Checks to see if the static Urls have been retrieved and does so if not.
		/// </summary>
		public SiteTemplate() : base () 
		{
			if (OnBackUrl==null) {
				OnBackUrl="";
				Config.Initialize(this);
			}
		}

		/// <summary>
		/// Returns a static field from the base page type.
		/// </summary>
		private FieldInfo GetStaticField(string Name) 
		{	
			try { return this.GetType().BaseType.GetField(Name, BindingFlags.Static | BindingFlags.NonPublic);
			} catch { throw new Exception("Field " + Name + " not found in Type " + this.GetType().BaseType.Name); }
		}

		/// <summary>
		/// Gets a base page static field value.
		/// </summary>
		private string GetStaticValue(string Name) 
		{
			try {return (string)GetStaticField(Name).GetValue(this);} catch {return "";}
		}

		/// <summary>
		/// Sets a base page static field value.
		/// </summary>
		private void SetStaticValue(string Name, string Value) 
		{
			try 
            {
                if (!string.IsNullOrEmpty(Value))
                {
                    GetStaticField(Name).SetValue(this, Value);
                }
            }
            // PC Security Defect Fix -CH1 -START Modified the below code to add logging inside the catch block 
            catch (Exception ex) { Logger.Log(ex.ToString()); }
            // PC Security Defect Fix -CH1 -END Modified the below code to add logging inside the catch block 
			
		}

		/// <summary>
		/// This Url will be navigated to on a back button click.  
		/// Retrieved from config, or, if nothing in config, then
		/// will use floating value retrieved from context (from GotoPage).
		/// </summary>
		public string OnBackUrl {
			get {
				string st = GetStaticValue("_OnBackUrl");
				return (st!=null && st!="")?st:(string)ViewState["OnBackUrl"];
			}
			set {SetStaticValue("_OnBackUrl", value);}
		}

		/// <summary>
		/// This Url will be navigated to on a continue button click.  Retrieved from config.
		/// </summary>
		public string OnContinueUrl {get {return GetStaticValue("_OnContinueUrl");} set {SetStaticValue("_OnContinueUrl", value);}}
		private string _Cancel="";
		/// <summary>
		/// This Url will be navigated to on a cancel button click.  Retrieved from config.
		/// </summary>
		public virtual string OnCancelUrl {get {return (_Cancel=="")?_OnCancelUrl:_Cancel;} set {_Cancel=value;}}

		/// <summary>
		/// Returns a value for item from either the Request.Form or the Context as appropriate.
		/// If the item doesn't exist, returns ""
		/// </summary>
		protected string RestoreValue(string item) 
		{
			if (IsPostBack) return (Request.Form[item]==null)?"":Request.Form[item].ToString();
			return (Context.Items[item]==null)?"":Context.Items[item].ToString();
		}

		/// <summary>
		/// Call this method to navigate to another page.
		/// </summary>
		/// <param name="Url"></param>
		public void GotoPage(string Url)
		{
			GotoPage(Url, true);
		}

		/// <summary>
		/// Call this method to navigate to another page.
		/// </summary>
		/// <param name="Url"></param>
		/// <param name="Returnable">True if this Url will be saved for the back button.</param>
		public void GotoPage(string Url, bool Returnable) {
			SavePageData();
			Order.SaveContext();
			OrderService.Close();
			if (Returnable) Context.Items["OnBackUrl"]=Request.FilePath;
			Context.Items.Add("OnCancelUrl", OnCancelUrl);
			Server.Transfer(Url);
		}

		/// <summary>
		/// To be overriden in derived classes, this method saves all the data on
		/// the page to the context.
		/// </summary>
		protected virtual void SavePageData() {}

		/// <summary>
		/// To be overridden in derived classes, this method restores the data from
		/// the context that is need for the page.  Some of the data from the context
		/// may be retrieved by controls on the page instead of through this method,
		/// if the page itself doesn't need it.
		/// </summary>
		protected virtual void RestorePageData() {}

		/// <summary>
		/// To be overridden in derived classes, this method handles any processing that the
		/// page requires and returns true if the process is successful.
		/// </summary>
		public virtual bool ProcessPage() 
		{
			return true;
		}

		/// <summary>
		/// Load event handler.  Checks to insure that pages aren't directly navigated to.
		/// Restores page data if not a postback.  Get the floating back url from
		/// the context, unless its the same as the oncancelurl
		/// </summary>
		protected override void OnLoad(EventArgs e)
		{ 
			if (Context.Items.Contains("OnCancelUrl")) OnCancelUrl=(string)Context.Items["OnCancelUrl"];
			if (!IsPostBack && Request.CurrentExecutionFilePath==Request.FilePath && Request.CurrentExecutionFilePath.IndexOf(OnCancelUrl)<0) 
				Response.Redirect(OnCancelUrl);
			if (!IsPostBack) {
				RestorePageData();
				ViewState["OnBackUrl"]=(Context.Items["OnBackUrl"]==null)?"":Context.Items["OnBackUrl"].ToString();
			}
			base.OnLoad(e);
		}

		/// <summary>
		/// Returns true if the request Url is Url.
		/// </summary>
		protected bool IsPage(string Url) 
		{
			return (Request.FilePath.ToLower().IndexOf(Url.ToLower())>0);
		}

		/// <summary>
		/// To be overridden in derived classes, this method allows a page to
		/// perform any processing necessary prior to validating.
		/// </summary>
		protected virtual void OnBeforeValidate() {}
		
		/// <summary>
		/// Override of base Validate, sets all the CustomValidators.Validator's Validated
		/// field to false to allow validation to occur.
		/// </summary>
		public override void Validate() 
		{
			OnBeforeValidate();
			foreach (BaseValidator V in Validators)
				if (typeof(Validator).IsInstanceOfType(V)) ((Validator)V).Validated=false;
			base.Validate();
		}

	}

}
