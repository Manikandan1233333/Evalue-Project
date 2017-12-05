<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PolicyLastName.ascx.cs" Inherits="MSC.Controls.PolicyLastName" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<%--67811A0 - PCI Remediation for Payment systems CH1: Adjusted the width of the _LastName to fit to the controls in Billing screen.  --%>
<!-- MAIG - CH1 - Modidfied the javascript code in OnKeyPress event to support Firefox browser -->
<!-- MAIG - CH2 - Modified the verbiage in the below Error Message attribute -->

<tr>
	<csaa:validator id="LabelValidator" runat="server" Width="190" controltovalidate="_LastName">
				&nbsp;&nbsp;Last Name</csaa:validator>
	<td class="arial_12">
	<%--67811A0 - PCI Remediation for Payment systems CH1:START Adjusted the width of the _LastName to fit to the controls in Billing screen.  --%>
	<!--PUP Ch1 -Modified the Width of the text box and drop down box to display the PUP product type by cognizant on 02/03/2011 -->
	<!-- MAIG - CH1 - BEGIN - Modidfied the javascript code in OnKeyPress event to support Firefox browser -->
	<asp:textbox id="_LastName"  width="165" runat="server" onkeypress="return alphabetsonly_onkeypress(event);" enableviewstate="False" MaxLength="25" /></td>
	<!-- MAIG - CH1 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser -->
	<%--<td width="110"></td>--%>
	<%--67811A0 - PCI Remediation for Payment systems CH1:END Adjusted the width of the _LastName to fit to the controls in Billing screen.  --%>
</tr>
<!-- MAIG - CH2 - BEGIN - Modified the verbiage in the below Error Message attribute -->
<csaa:validator id="vldLastNameAlpha" runat="server" display="None" errormessage="Invalid Last Name" onservervalidate="CheckName" />
<!-- MAIG - CH2 - END - Modified the verbiage in the below Error Message attribute -->
<csaa:validator id="Valid" runat="server" display="None" errormessage="Please enter a valid Last Name." onservervalidate="CheckValid" />
