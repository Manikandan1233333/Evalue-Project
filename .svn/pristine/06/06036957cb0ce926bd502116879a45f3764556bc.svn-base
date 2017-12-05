<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<%@ Register TagPrefix="uc" TagName="State" Src="State.ascx" %>
<%@ Register TagPrefix="uc" TagName="Zip" Src="ZipCode.ascx" %>
<%@ Register TagPrefix="uc" TagName="Phone" Src="Phone.ascx" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Address.ascx.cs" Inherits="MSC.Controls.Address" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<!--
		* HISTORY
		* MODIFIED BY COGNIZANT AS PART OF .NET MIGRATION CHANGES
		* CHANGES DONE
		* 04/16/2010 .NetMig.Ch1: Added new validation control.
		67811A0 - PCI Remediation for Payment systems CH1:Street Address not needed for creditcard payment so added condition so that it will not be loaded for 
		        creditcard payment from Billing screen.
-->
<%--67811A0 - PCI Remediation for Payment systems CH1: START - Street Address not needed for creditcard payment so added condition so that it will not be loaded for 
		        creditcard payment from Billing screen.--%>
<tr><%if (!Billing) {%>
<%--67811A0 - PCI Remediation for Payment systems CH1: END --%>
	<%--<CSAA:validator id="vldAddress1" runat="server" controltovalidate="_Address1">
		&nbsp;&nbsp;&nbsp;Street Address 1</CSAA:validator>--%>
		
</tr>
<tr>
	<td>&nbsp;
		<asp:textbox id="_Address1" runat="server" width="480px" EnableViewState="False" MaxLength="40" /></td>
		<!--Added as a part of .NetMig 3.5-->
		<%--<CSAA:Validator ID="valid" runat="server" controltovalidate="_Address1"  ErrorMessage="Test" OnServerValidate="ReqValCheck" ></CSAA:Validator>--%>
</tr>
<tr>
	<td height="5"></td>
</tr><%}%>
<tr><%if (!Billing) {%>
	<td class="arial_12_bold">&nbsp;&nbsp;&nbsp;Street Address 2</td>
</tr>
<tr>
	<td>&nbsp;
		<asp:textbox id="_Address2" runat="server" width="480px" EnableViewState="False" MaxLength="40" /></td>
</tr>
<tr>
	<td height="5"></td>
</tr>
<tr>
	<CSAA:validator id="vldCity" runat="server" controltovalidate="_City">
	&nbsp;&nbsp;&nbsp;City</CSAA:validator>
</tr>
<tr>
	<td>&nbsp;
		<asp:textbox id="_City" runat="server" width="198px" EnableViewState="False" MaxLength="25" /></td>
</tr>
<tr>
	<td height="5"></td>
</tr><%}%>

<tr>
	<td>
		<table border="0" cellpadding="0" cellspacing="0">
			<tr>
				<td style="PADDING-LEFT:8px"><%if (!Billing)
                                   {%><uc:state id="_State" runat="server" label="State" required="true" /></td>
				<td width="15"></td>
				<td><%}%><uc:zip id="_ZipCode" runat="server" label="Zip Code" required="true" use9digit="false" /></td>
			</tr>
		</table>
	</td>
</tr>
<%if (!Billing) {%>
<tr>
	<td>
		<table border="0" cellpadding="0" cellspacing="0">
			<tr>
				<td height="5"></td>
				<td width="45" height="5"></td>
				<td height="5"></td>
			</tr>
			<tr>
				<td style="PADDING-LEFT:8px"><uc:phone id="_DayPhone" runat="server" label="Day Phone" required="true" /></td>
				<td></td>
				<td><uc:phone id="_EveningPhone" runat="server" label="Evening Phone" /></td>
			</tr>
		</table>
	</td>
</tr><%}%>