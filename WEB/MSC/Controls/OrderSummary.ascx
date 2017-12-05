<%--CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the PromoSummary User Control reference - March 2016--%>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="OrderSummary.ascx.cs" Inherits="MSC.Controls.OrderSummary" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<table width="421" cellpadding="0" cellspacing="0" border="1" bordercolor="#999999" runat="server" id="SummaryTable">
	<tr>
		<td class="arial_11_bold" bgcolor="#cccccc" height="22">
			&nbsp;<asp:label id="SummaryLabel" runat="server" />
		</td>
	</tr>
	<tr>
		<td width="421">
			<asp:datagrid footerstyle-font-bold="True" headerstyle-font-bold="True" alternatingitemstyle-backcolor="WhiteSmoke" width="421px" cssclass="arial_11" borderwidth="0" id="SummaryView" runat="server" enableviewstate="false" visible="true" autogeneratecolumns="false" showheader="true" enabled="true" showfooter="True" onselectedindexchanged="SummaryView_SelectedIndexChanged">
				<columns>
					<asp:boundcolumn datafield="Description" headertext="Description" />
					<asp:boundcolumn datafield="Price" headertext="Price" itemstyle-horizontalalign="Right" headerstyle-horizontalalign="Right" dataformatstring="{0:C}" />
					<asp:boundcolumn datafield="Amount" headertext="Amount" itemstyle-horizontalalign="Right" footerstyle-horizontalalign="Right" headerstyle-horizontalalign="Right" dataformatstring="{0:C}" />
				</columns>
				<footerstyle borderstyle="Double" />
			</asp:datagrid>
		</td>
	</tr>
	<tr runat="server" id="PromoSummary">
        <%--CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the PromoSummary User Control reference - March 2016--%>
	</tr>
</table>
<msc:ordercontrol runat="server" id="Order" />
