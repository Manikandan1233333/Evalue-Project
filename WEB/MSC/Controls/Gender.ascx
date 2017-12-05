<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Gender.ascx.cs" Inherits="MSC.Controls.Gender" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table border="0" cellpadding="0" cellspacing="0">
	<tr>
		<CSAA:validator id="LabelValidator" controltovalidate="_Gender" runat="server">
			<%=Label%>
		</CSAA:validator>
		<td class="arial_12_bold">&nbsp;&nbsp;<asp:radiobuttonlist id="_Gender" runat="server" repeatdirection="Horizontal" repeatlayout="Flow" enableviewstate="False">
				<asp:listitem value="Male">Male</asp:listitem>
				<asp:listitem value="Female">Female</asp:listitem>
			</asp:radiobuttonlist></td>
	</tr>
</table>
