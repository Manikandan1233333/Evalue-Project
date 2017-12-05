<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Locations.ascx.cs" Inherits="MSCR.Controls.Locations" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
		<tr>
			<td>&nbsp;&nbsp;</td>
			<td class="arial_12_bold">Locations</td>
			<td colspan="3"></td>
		</tr>
		<tr>
			<td>&nbsp;&nbsp;</td>
			<td colspan="4" class="arial_12">
				<asp:listbox id="_Locations" runat="server" datatextfield="Desc" DataValueField ="Code"  rows="10" selectionmode="single" cssclass="arial_12"></asp:listbox>
				
			</td>
		</tr>