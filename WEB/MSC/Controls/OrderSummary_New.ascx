<%--CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the PromoSummary.ascx User control code as part of the Code Clean Up- March 2016--%>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="OrderSummary_New.ascx.cs" Inherits="MSC.Controls.OrderSummary_New" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table width="421" cellpadding="0" cellspacing="0" border="1" bordercolor="#999999" runat="server" id="INS_SummaryTable">
	<tr>
		<td class="arial_11_bold" bgcolor="#cccccc" height="22">
			&nbsp;<asp:label id="INS_SummaryLabel" runat="server" />
		</td>
	</tr>
	<tr>
		<td width="421">
			<asp:datagrid footerstyle-font-bold="True" headerstyle-font-bold="True" width="421px" cssclass="arial_11" id="INS_SummaryView" runat="server" enableviewstate="false" visible="true" autogeneratecolumns="false" showheader="true" enabled="true" showfooter="True">
				<columns>
					<asp:boundcolumn ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" FooterStyle-BorderWidth="1" FooterStyle-CssClass="border_Top_Down" datafield="ProductDesc" headertext="Product Type" ItemStyle-Width="200" />
					<asp:boundcolumn ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" FooterStyle-BorderWidth="1" FooterStyle-CssClass="border_Top_Down" datafield="Price" headertext="Price" itemstyle-horizontalalign="Right" headerstyle-horizontalalign="Right" dataformatstring="{0:C}" />
					<asp:boundcolumn ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" FooterStyle-BorderWidth="1" FooterStyle-CssClass="border_Top_Down" datafield="Policy" headertext="Policy Number" ItemStyle-Width="100" />
					<asp:boundcolumn ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" FooterStyle-BorderWidth="1" FooterStyle-CssClass="border_Top_Down" datafield="Amount" headertext="Payment Amount" itemstyle-horizontalalign="Left" footerstyle-horizontalalign="left" headerstyle-horizontalalign="left" dataformatstring="{0:C}" />
				</columns>
				<footerstyle BorderStyle="Double" />
			</asp:datagrid>
		</td>
	</tr>
</table>
<table width="421" cellpadding="0" cellspacing="0" border="1" bordercolor="#999999" runat="server" id="MBR_SummaryTable">
	<tr>
		<td class="arial_11_bold" bgcolor="#cccccc" height="22">
			&nbsp;<asp:label id="MBR_SummaryLabel" runat="server" />
		</td>
	</tr>
	<tr>
		<td width="421">
			<asp:datagrid footerstyle-font-bold="True" headerstyle-font-bold="True" alternatingitemstyle-backcolor="WhiteSmoke" width="421px" cssclass="arial_11" borderwidth="0" id="MBR_SummaryView" runat="server" enableviewstate="false" visible="true" autogeneratecolumns="false" showheader="true" enabled="true" showfooter="True">
				<columns>
					<asp:boundcolumn datafield="Description" headertext="Description" />
					<asp:boundcolumn datafield="Price" headertext="Price" itemstyle-horizontalalign="Right" headerstyle-horizontalalign="Right" dataformatstring="{0:C}" />
					<asp:boundcolumn datafield="Amount" headertext="Amount" itemstyle-horizontalalign="Right" footerstyle-horizontalalign="Right" headerstyle-horizontalalign="Right" dataformatstring="{0:C}" />
				</columns>
				<footerstyle borderstyle="Double" />
			</asp:datagrid>
		</td>
	</tr>
			<%--CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the PromoSummary.ascx User control code as part of the Code Clean Up
                and removing the table row - March 2016--%>
</table>
<msc:ordercontrol runat="server" id="Order" />
