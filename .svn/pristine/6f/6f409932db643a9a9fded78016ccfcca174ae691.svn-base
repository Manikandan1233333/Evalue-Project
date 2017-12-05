<%@ Register TagPrefix="uc" TagName="PrintHeader" Src="../Controls/PrintHeader.ascx"%>
<%@ Page language="c#" Codebehind="LoadXls.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.LoadXls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>LoadXls</title>
		<link rel="stylesheet" type="text/css" href="../style.css">		
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="LoadXls" name="frmXls" runat="server">
			<uc:PrintHeader id="PrintHeader" runat="server"></uc:PrintHeader>
			<br>
			<table width="608" bgcolor="lightyellow" class="arial_12">			
			<asp:datagrid id="dgGrid" runat="server" AllowPaging="False" CssClass="arial_9" Width="606px" AutoGenerateColumns="false" OnItemCreated="ItemCreated" OnItemDataBound="ItemDataBound">
				<ItemStyle CssClass="arial_12"></ItemStyle>
				<AlternatingItemStyle BackColor="LightYellow"></AlternatingItemStyle>				
				<HeaderStyle Height="22px" CssClass="arial_12_bold" BackColor="#CCCCCC"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="USERNAME" HeaderText="User" />
					<asp:BoundColumn DataField="DTCREATED" HeaderText="Date" />
					<asp:BoundColumn DataField="RENEWALCOUNT" HeaderText="Transactions" />
					<asp:BoundColumn DataField="RENEWALREVENUE" HeaderText="Revenue" DataFormatString="{0:c}">
						<itemstyle horizontalalign="right" />
					</asp:BoundColumn>
				</Columns>
			</asp:datagrid>
			<asp:datagrid id="dgGrid1" runat="server" Width="606px" OnItemDataBound="ItemDataBound1" OnItemCreated="ItemCreated1" CssClass="arial_9" AutoGenerateColumns="false" AllowPaging="False">
				<ItemStyle CssClass="arial_12"></ItemStyle>
				<AlternatingItemStyle BackColor="LightYellow"></AlternatingItemStyle>
				<HeaderStyle Height="22px" CssClass="arial_12_bold" BackColor="#CCCCCC"></HeaderStyle>
				<Columns>
					<asp:BoundColumn DataField="DTCREATED" HeaderText="Date" />
					<asp:BoundColumn DataField="USERNAME" HeaderText="User" />
					<asp:BoundColumn DataField="RENEWALCOUNT" HeaderText="Transactions" />
					<asp:BoundColumn DataField="RENEWALREVENUE" HeaderText="Revenue" DataFormatString="{0:c}">
						<itemstyle horizontalalign="right" />
					</asp:BoundColumn>
				</Columns>
			</asp:datagrid>
			<asp:datagrid id="dgGrid2" runat="server" Width="606px" CssClass="arial_9" AllowPaging="False" onitemcreated="ItemCreated2" onitemdatabound="ItemDataBound2">
				<ItemStyle CssClass="arial_12"></ItemStyle>
				<AlternatingItemStyle BackColor="LightYellow"></AlternatingItemStyle>
				<HeaderStyle Height="22px" CssClass="arial_12_bold" BackColor="#CCCCCC"></HeaderStyle>
			</asp:datagrid>
			</table>
		</form>
	</body>
</HTML>
