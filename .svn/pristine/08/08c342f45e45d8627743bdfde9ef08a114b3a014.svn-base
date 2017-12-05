using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using CSAAWeb;
using System.Collections.Specialized;
using System.Xml;
using System.Xml.Xsl;
using System.IO;

namespace CSAAWeb.WebControls
{
	/// <summary>
	/// Provides a control that generates a report through an xml transform.
	/// </summary>
	public class BaseXmlReport : System.Web.UI.WebControls.WebControl {
		private string SiteRoot = string.Empty;
		private static string ExtendedAttributes = string.Empty;
		private static string DefaultXslt = string.Empty;

		///<summary>The data to transform.</summary>
		public XmlDocument XmlSource;
		///<summary>Optional Extended attribute data.</summary>
		public XmlDocument XmlExtendedAttributes;
		///<summary>Set to true if the component should not clear the Response stream.</summary>
		public bool Embedded=false;
		///<summary>The XSLT stylesheet used to perform the transform.</summary>
		public string Xslt = string.Empty;
		///<summary>The title to insert into the transformed document.</summary>
		public string Title = "";
		
		/// <summary>
		/// Gets the default locations for the default transforms.
		/// </summary>
		static BaseXmlReport () {
			DefaultXslt = Config.Setting("XmlReport_Default_Transform");
			if (DefaultXslt=="") DefaultXslt = "reports/report.xsl";
			ExtendedAttributes = Config.Setting("XmlReport_Default_Transform");
			if (ExtendedAttributes=="") ExtendedAttributes = "reports/extendedattributes.xsl";
		}

		/// <summary>
		/// Gets the locations for the transforms.
		/// </summary>
		protected override void OnInit(EventArgs e) {
			SiteRoot = Page.Request.ApplicationPath + "/";
			if (Xslt=="") Xslt = DefaultXslt;
			if (Xslt.IndexOf(SiteRoot)==-1 && Xslt.Substring(0,1)!="/") 
				Xslt = SiteRoot + Xslt;
			if (ExtendedAttributes.IndexOf(SiteRoot)==-1 && ExtendedAttributes.Substring(0,1)!="/") 
				ExtendedAttributes = SiteRoot + ExtendedAttributes;
			base.OnInit(e);
		}

		/// <summary>
		/// Generates the report from the supplied recordset or Sql Proc name.
		/// </summary>
		protected override void Render(HtmlTextWriter output) {
			if (XmlSource==null) throw new Exception("No Xml source provided.");

			XslTransform Transformer = new XslTransform();
			XsltArgumentList Args = new XsltArgumentList();

			if (!(Embedded)) Page.Response.Clear();

			if (XmlExtendedAttributes!=null) {
				//This is a multi-dataset procedure, the 1st one has extended
				// parameters that need inserting into the second via Xslt 
				// transformations.
				// - get the stylesheet to transform the 1st dataset into 
				// - another transform stylesheet.
				Transformer.Load(Page.MapPath(ExtendedAttributes));

				// Transform the recordset into a new transformer.				
				Transformer.Load(Transformer.Transform(XmlExtendedAttributes, Args));

				// transform it into Source, now including extended column attributes
				StringWriter St2 = new StringWriter();
				Transformer.Transform(XmlSource, Args, St2);
				XmlSource.LoadXml(St2.ToString());
			}
			// Load the XSL 
			Transformer.Load(Page.MapPath(Xslt));

			// Set the parameters
			Args.AddParam("siteRoot","", SiteRoot);
			Args.AddParam("doc-Title", "", Title);

			// Transform Source to output
			Transformer.Transform(XmlSource, Args, output);

			if (!Embedded) Page.Response.End();
		}

	}
}
