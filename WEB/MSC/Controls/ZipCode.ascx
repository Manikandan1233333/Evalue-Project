<!-- MAIG - CH1 - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="ZipCode.ascx.cs" Inherits="MSC.Controls.Zip" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<!--
		* HISTORY
		* MODIFIED BY COGNIZANT AS PART OF .NET MIGRATION CHANGES
		* CHANGES DONE
		* 04/16/2010 .NetMig.Ch1: Added new validation control validator1.
		<%--67811A0 - PCI Remediation for Payment systems CH1: Changed label ZipCode as Billing ZipCode--%>
-->
<table border="0" cellpadding="0" cellspacing="0">
	<tr>
	<%--67811A0 - PCI Remediation for Payment systems CH1:START Changed label ZipCode as Billing ZipCode--%>
		<CSAA:Validator id="LabelValidator" runat="server" ControlToValidate="ZipCode">Billing ZipCode</CSAA:Validator>
	<%--67811A0 - PCI Remediation for Payment systems CH1:END Changed label ZipCode as Billing ZipCode--%>
	</tr>
	<tr>
		<td valign="top">
			<!-- MAIG - CH1 - START - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
			<asp:textbox id="ZipCode" runat="server" width="125px" maxlength="5" onkeypress="return digits_only_onkeypress(event)" EnableViewState="False"></asp:textbox><%if (Use9Digit) {%>
			<asp:textbox id="ExtendedZipCode" runat="server" width="125px" maxlength="4" onkeypress="return digits_only_onkeypress(event)" EnableViewState="False"></asp:textbox><%}%>
			<!-- MAIG - CH1 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
		</td>
	</tr>
</table>
<CSAA:Validator id="Numeric" runat="server" Display="None" ErrorMessage="%Label% field contains non-numeric characters." DefaultAction="Numeric" ControlToValidate="ZipCode"/>
<CSAA:Validator id="Valid" runat="server" Display="None" ErrorMessage="Please enter a valid %Label%." OnServerValidate="CheckValid"/>
<!--Added as a part of .NetMig 3.5-->
		<CSAA:Validator ID="Validator1" runat="server" controltovalidate="ZipCode"   OnServerValidate="ReqValCheck" DefaultAction="Required"></CSAA:Validator>