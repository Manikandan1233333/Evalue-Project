<%@ Control Language="c#" AutoEventWireup="True" Codebehind="RevenueProduct.ascx.cs" Inherits="MSCR.Controls.RevenueProduct" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<tr>
	<td colspan="5" height="10"></td>
</tr>
<tr>
	<td>&nbsp;&nbsp;</td>
	<td colspan="2" class="arial_12_bold">Product Type</td>
	<td colspan="2" class="arial_12_bold">&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Revenue Type</td>
</tr>
<tr>
	<td>&nbsp;&nbsp;</td>
	<td colspan="2">
	<!--PUP Ch1 -Modified the Width of the text box and drop down box to display the PUP product type by cognizant on 02/03/2011 -->
		<asp:dropdownlist id="_ProductType" width="210px" runat="server" 
    datatextfield="Description" datavaluefield="ID" /></td>
	<td colspan="2">
		&nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:dropdownlist id="_RevenueType" width="200" runat="server" datatextfield="Description" datavaluefield="ID" /></td>
</tr>
