<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PolicyNumber.ascx.cs" Inherits="MSC.Controls.PolicyNumber" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<!--
		* HISTORY
		* MODIFIED BY COGNIZANT AS PART OF .NET MIGRATION CHANGES
		* CHANGES DONE
		* 04/16/2010 .NetMig.Ch1: Removed ErrorMessage and added DefaultAction and ControlToValidate properties	
		<%--67811A0 - PCI Remediation for Payment systems CH1: Adjusted the width of the _Policy to fit to the controls in Insurance screen.  --%>
		* MAIG - CH1 - Modidfied the javascript code in OnKeyPress event to support Firefox browser
-->


<tr>
<%--67811A0 - PCI Remediation for Payment systems CH1:START Adjusted the width of the _Policy to fit to the controls in Insurance screen.  --%>
<!-- MAIG - CH1 - BEGIN - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
<csaa:validator id="LabelValidator" controltovalidate="_Policy" runat="server" class="arial_12_bold">&nbsp;&nbsp;Policy Number</csaa:validator><td align="left"><asp:textbox id="_Policy" runat="server" enableviewstate="False" maxlength="13" onkeypress="return alphanumeric_onkeypress(event)" width="200" columns="40"></asp:textbox></td>
<!-- MAIG - CH1 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
<%--<asp:RequiredFieldValidator runat="server" id="ReqPolicy" controltovalidate="_Policy" errormessage="" />--%>
<%--67811A0 - PCI Remediation for Payment systems CH1:END Adjusted the width of the _Policy to fit to the controls in Insurance screen.  --%>
	<td width="110"></td>
</tr>
<tr>
	<td height="1" colspan="3"></td>
</tr>

