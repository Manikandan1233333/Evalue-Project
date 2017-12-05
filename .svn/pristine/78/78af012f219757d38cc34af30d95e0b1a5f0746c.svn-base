<%@ Page aspcompat="true" language="c#" Inherits="System.Web.UI.Page" %>
<%@ Register TagPrefix="CSAAWeb" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<SCRIPT language="C#" runat="server">
protected override void OnInit(EventArgs e) {
	switch (Request.QueryString["Rpt"]) {
		case "1":
			Rpt.Sql="CYBER_Export"; 
			Rpt.ConnectionName="ConnectionString.Payments";
			break;
		case "2":
			Rpt.Sql="MSC_ORDER_EXPORT"; 
			Rpt.ConnectionName="ConnectionString.Membership";
			break;
		case "3":
			Rpt.Sql="MSC_MBR_EXPORT"; 
			Rpt.ConnectionName="ConnectionString.Membership";
			break;
		case "4":
			Rpt.Sql="INS_Export";
			Rpt.ConnectionName="ConnectionString.Payments";
			break;
		default: throw new Exception("Unrecognized Report number."); break;
	}
	Response.ContentType="text/plain";
	base.OnInit(e);
}
</SCRIPT>
<CSAAWeb:AdoXmlReport id="Rpt" runat="server" Xslt="reports/csv.xsl" />
