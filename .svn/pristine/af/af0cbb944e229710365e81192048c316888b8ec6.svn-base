<!-- MAIG - CH1 - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Three_Box_Date.ascx.cs" Inherits="MSC.Controls.Three_Box_Date" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<tr>
	<CSAA:validator id="LabelValidator" runat="server" controltovalidate="MM,DD,YYYY">&nbsp;&nbsp;&nbsp;<%=Label%></CSAA:validator>
</tr>
<tr>
	<td>&nbsp;
		<!-- MAIG - CH1 - START - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
		<asp:textbox id="MM" runat="server" width="25px" maxlength="2" onkeypress="return digits_only_onkeypress(event)" enableviewstate="False" />&nbsp;&nbsp;/&nbsp;&nbsp;
		<asp:textbox id="DD" runat="server" width="25px" maxlength="2" onkeypress="return digits_only_onkeypress(event)" enableviewstate="False" />&nbsp;&nbsp;/&nbsp;
		<asp:textbox id="YYYY" runat="server" width="40px" maxlength="4" onkeypress="return digits_only_onkeypress(event)" enableviewstate="False" />
		<!-- MAIG - CH1 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
	</td>
</tr>
<CSAA:validator id="Length1" runat="server" display="None" errormessage="%Label% field contains too many digits." defaultaction="maxLength" controltovalidate="MM,DD" />
<CSAA:validator id="Length2" runat="server" display="None" errormessage="%Label% year must be exactly 4 digits." defaultaction="ExactLength" controltovalidate="YYYY" />
<CSAA:validator id="Numeric" runat="server" display="None" errormessage="%Label% field contains non-numeric characters." defaultaction="Numeric" controltovalidate="MM,DD,YYYY" />
<CSAA:validator id="Valid" runat="server" display="None" errormessage="Please enter a valid %Label%." onservervalidate="CheckValid" />
