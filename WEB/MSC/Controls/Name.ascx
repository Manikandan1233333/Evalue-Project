<!-- MAIG - CH1 - Modidfied the javascript code in OnKeyPress event to support Firefox browser
 	 MAIG - CH2 - Modidfied the javascript code in OnKeyPress event to support Firefox browser
	 MAIG - CH3 - Modidfied the javascript code in OnKeyPress event to support Firefox browser
  -->
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Name.ascx.cs" Inherits="MSC.Controls.Name" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>



<table width="512" border="0" cellpadding="0" cellspacing="0">
	<colgroup>
		<col width="215">
		<col width="36">
		<col width="261">
		<tr>
		<!-- //67811A0  - PCI Remediation for Payment systems - Changed the validator ID to validate the first name-->
			<CSAA:validator id="vldFirstName" runat="server" controltovalidate="_FirstName">First Name</CSAA:validator>
			<td class="arial_12_bold">M.I.</td>
			<CSAA:validator id="vldLastName" runat="server"  controltovalidate="_LastName">Last Name</CSAA:validator>
		</tr>
		<tr>
			<td>
				<!-- MAIG - CH1 - BEGIN - Modidfied the javascript code in OnKeyPress event to support Firefox browser -->
				<asp:textbox id="_FirstName" runat="server" width="210px" onkeypress="return alphabetsonly_onkeypress(event);" enableviewstate="False" MaxLength="40" /></td>
				<!-- MAIG - CH1 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser -->
			<td>
				<!-- MAIG - CH2 - BEGIN - Modidfied the javascript code in OnKeyPress event to support Firefox browser -->
				<asp:textbox id="_MiddleName" runat="server" width="30px" onkeypress="return alphabetsonly_onkeypress(event);" MaxLength="1" enableviewstate="False" /></td>
				<!-- MAIG - CH2 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser -->
			<td>
				<!-- MAIG - CH3 - BEGIN - Modidfied the javascript code in OnKeyPress event to support Firefox browser -->
				<asp:textbox id="_LastName" runat="server" width="210px" onkeypress="return alphabetsonly_onkeypress(event);" enableviewstate="False" MaxLength="40" /></td>
				<!-- MAIG - CH3 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser -->
		</tr>
		<tr>
        <td height="10" class="style1">
        <CSAA:Validator ID="vldFirstNameReq" runat="server" OnServerValidate="CheckFirstName"></CSAA:Validator>
        <CSAA:Validator ID="vldLastNameReq" runat="server" OnServerValidate="CheckLastName"></CSAA:Validator>
        </td>
        </tr>
</table>
<CSAA:validator id="vldName" runat="server" onservervalidate="CheckLength"
ErrorMessage="The name is too long."/>
<CSAA:Validator id="Valid" runat="server" Display="None" ErrorMessage="Please enter a valid %Label%." OnServerValidate="CheckValid"/>
