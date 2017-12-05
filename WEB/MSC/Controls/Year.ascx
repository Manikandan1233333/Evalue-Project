<!-- MAIG - CH1 - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Year.ascx.cs" Inherits="MSC.Controls.Year" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
	<tr>
		<csaa:validator align="left" runat="server" id="LabelValidator" controltovalidate="_BaseYear">&nbsp;&nbsp;&nbsp;<%=Label%></csaa:validator>
	</tr>
	<tr>
		<!-- MAIG - CH1 - START - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
		<td>&nbsp;&nbsp;<asp:TextBox runat="server" ID="_BaseYear" width="40px" maxlength="4" onkeypress="return digits_only_onkeypress(event)" /></td>
		<!-- MAIG - CH1 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
	</tr>
<csaa:validator runat="server" id="vldBase2" errormessage="%Label% is out of exceptable range." onservervalidate="CheckBaseYear" controltovalidate="_BaseYear" />
<csaa:validator runat="server" id="vldBase3" errormessage="%Label% must be 4 digits." defaultaction="ExactLength" controltovalidate="_BaseYear" />
<CSAA:validator id="Numeric" runat="server" display="None" errormessage="%Label% field contains non-numeric characters." defaultaction="Numeric" controltovalidate="_BaseYear" />
<CSAA:validator id="Valid" runat="server" display="None" errormessage="Please enter a valid %Label%." onservervalidate="CheckValid" />
