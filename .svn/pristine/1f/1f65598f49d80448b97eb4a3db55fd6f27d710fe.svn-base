<!--
		* HISTORY
		* MODIFIED BY COGNIZANT AS PART OF .NET MIGRATION CHANGES
		* CHANGES DONE
		* 04/20/2010 .NetMig.Ch1: Added OnServerValidate Property		
		* 04/20/2010 .NetMig.Ch2: Added new validator for require field
		* 07/30/2010 Removed an Additional 0 at the end
		* MAIG - CH1 - Modidfied the javascript code in OnKeyPress event to support Firefox browser
		-->




<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Phone.ascx.cs" Inherits="MSC.Controls.Phone" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%if (!Horizontal) {%>
<table border="0" cellpadding="0" cellspacing="0">
	<%}%>
	<tr>
		<CSAA:validator id="LabelValidator" runat="server" controltovalidate="Area_Code,Prefix,Suffix">
			Phone Number
		</CSAA:validator><%if (!Horizontal) {%>
	</tr>
	<tr>
		<%}%>
		<td class="arial_12_bold">
			<!-- MAIG - CH1 - BEGIN - Modidfied the javascript code in OnKeyPress event to support Firefox browser -->
			<asp:textbox id="Area_Code" runat="server" width="35px" maxlength="3" onkeypress="return digits_only_onkeypress(event)" enableviewstate="False"></asp:textbox>&nbsp;-&nbsp;
			<asp:textbox id="Prefix" runat="server" width="35px" maxlength="3" onkeypress="return digits_only_onkeypress(event)" enableviewstate="False"></asp:textbox>&nbsp;&nbsp;-&nbsp;
			<asp:textbox id="Suffix" runat="server" width="40px" maxlength="4" onkeypress="return digits_only_onkeypress(event)" enableviewstate="False"></asp:textbox><%if (IncludeExtension) {%>
			&nbsp;&nbsp;Extn:<asp:textbox id="Extension" runat="server" width="40px" maxlength="4" onkeypress="return digits_only_onkeypress(event)" enableviewstate="False"></asp:textbox><%}%>
			<!-- MAIG - CH1 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser -->
		</td>
	</tr>
	<%if (!Horizontal) {%>
</table>
<%}%>
<CSAA:validator id="Length2" runat="server" display="None" errormessage="Please enter a valid Phone Number." defaultaction="ExactLength" controltovalidate="Area_Code,Prefix,Suffix" />
<CSAA:validator id="Numeric" runat="server" display="None" errormessage="%Label% field contains non-numeric characters." defaultaction="Numeric" controltovalidate="Area_Code,Prefix,Suffix" />
<CSAA:validator id="Valid" runat="server" display="None" errormessage=" " onservervalidate="CheckValid" />
<CSAA:validator id="Validator1" runat="server" display="None" errormessage=" " onservervalidate="CheckValid1" />