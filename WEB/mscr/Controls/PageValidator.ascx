<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PageValidator.ascx.cs" Inherits="MSCR.Controls.PageValidator" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
		<tr>
			<td colspan="<%=Colspan%>">
				<table width="421" border="0" cellspacing="0" cellpadding="0">
					<CSAA:DefaultSummary id="dfs0" runat="Server">Please complete or correct the requested information in the field(s) highlighted in red below.</CSAA:DefaultSummary>
					<tr>
						<td width="30" valign="center"><img src="<%=ResolveUrl("../images/red_arrow.gif")%>"></td>
						<td class="arial_11_red"><asp:ValidationSummary id="vs0" CssClass="arial_11_red" ShowSummary="True" DisplayMode="List" Runat="server" EnableClientScript="False"/></td>
					</tr>
				</table>
			</td>
		</tr>
