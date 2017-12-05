<%--PC Phase II Changes CH1 - Modified the UI Code to disable Roles DropDown list--%>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Users.ascx.cs" Inherits="MSCR.Controls.Users" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<tr>
	<td>&nbsp;&nbsp;</td>
	<td colspan="2" class="arial_12_bold">
   Application</td>
	<%--<td colspan="2" class="arial_12_bold">Role</td>
</tr>--%>
<tr>
	<td>&nbsp;&nbsp;</td>
	<td colspan="2">
	<!--PUP Ch1 -Modified the Width of the text box and drop down box to display the PUP product type by cognizant on 02/03/2011 -->
		<asp:dropdownlist id="_Application" width="250" runat="server" AutoPostBack="True" datatextfield="Description" datavaluefield="ID" onselectedindexchanged="_Application_SelectedIndexChanged" /></td>
	<td colspan="2">
		<asp:dropdownlist id="_Role" width="200" runat="server" Visible="false" AutoPostBack="True" datatextfield="Description" datavaluefield="ID" onselectedindexchanged="_Role_SelectedIndexChanged" /></td>
</tr>
<tr>
	<td colspan="5" height="10"></td>
</tr>
<tr>
	<td>&nbsp;&nbsp;</td>
	<td class="arial_12_bold" colspan="4">Rep DO</td>
</tr>
<tr>
	<td>&nbsp;&nbsp;</td>
	<td colspan="4">
		<asp:dropdownlist id="_RepDO" width="250" runat="server" AutoPostBack="True" datatextfield="Description" datavaluefield="ID" onselectedindexchanged="_RepDO_SelectedIndexChanged" /></td>
</tr>
<tr>
	<td colspan="5" height="10"></td>
</tr>
<tr>
	<td>&nbsp;&nbsp;</td>
	<td class="arial_12" colspan="4"><asp:label id="lblCsrCtrl" text="<b>Users</b> hold down 'Ctrl' to select multiple" runat="server"></asp:label></td>
</tr>
<tr>
	<td>&nbsp;&nbsp;</td>
	<td colspan="4" height="1"><asp:listbox id="_UserList" runat="server" datatextfield="USERDEF" datavaluefield="USERNAME" rows="10" selectionmode="Multiple" cssclass="arial_12"></asp:listbox></td>
	<td></td>
</tr>
