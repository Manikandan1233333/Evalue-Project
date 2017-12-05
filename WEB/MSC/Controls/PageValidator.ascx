<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PageValidator.ascx.cs" Inherits="MSC.Controls.PageValidator" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<!--
		* HISTORY
		* MODIFIED BY COGNIZANT 
		* CHANGES DONE
		67811A0 - PCI Remediation for Payment systems CH1:Changed Text for Defaultsummary from "Please complete or correct the requested information in the field(s) highlighted in red below."
		to "Please complete or correct the requested information in the field(s) highlighted in red above."
-->
		<tr>
			<td colspan="<%=Colspan%>">
				<table width="421" border="0" cellspacing="0" cellpadding="0">
				<%--67811A0 - PCI Remediation for Payment systems CH1:Changed Text for Defaultsummary from "Please complete or correct the requested information in the field(s) highlighted in red below."
		to "Please complete or correct the requested information in the field(s) highlighted in red above."--%>
					<CSAA:DefaultSummary id="dfs0" runat="Server" MaintainScrollPositionOnPostback = "true" >Please complete or correct the requested information in the field(s) highlighted in red above.</CSAA:DefaultSummary>
					<tr>
						<td width="30" valign="center"><img src="<%=ResolveUrl("../images/red_arrow.gif")%>"></td>
						<td class="arial_11_red"><asp:ValidationSummary id="vs0" CssClass="arial_11_red" ShowSummary="True" DisplayMode="List" Runat="server" EnableClientScript="False" MaintainScrollPositionOnPostback = "true"/></td>
					</tr>
				</table>
			</td>
		</tr>
<asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
    ControlToValidate="dfs0" ErrorMessage="RequiredFieldValidator"></asp:RequiredFieldValidator>

