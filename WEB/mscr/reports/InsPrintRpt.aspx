<%@ Register TagPrefix="MSCR" TagName="Header" Src="InsRptHeader.ascx" %>
<%@ Page language="c#" Codebehind="InsPrintRpt.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.InsPrintRpt" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>PrintRpt</title>
		<link rel=stylesheet type=text/css href=../style.css>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="InsPrintRpt" name="frmPrint" runat="server">
			<MSCR:Header id="ctlHeader" runat="server" /><br>
			<asp:label id="lblCyber" text="In CyberSource: Not in APDS" CssClass="arial_11_bold" Visible="False" runat="server"></asp:label>
			<asp:datagrid id="dgReport1" runat="server" cssclass="arial_9" width="604px" AllowPaging="False" onitemdatabound="ItemDataBound1">
				<AlternatingItemStyle BackColor="LightYellow"></AlternatingItemStyle>
				<ItemStyle CssClass="arial_12"></ItemStyle>
				<HeaderStyle Height="22px" CssClass="arial_12_bold" BackColor="#CCCCCC"></HeaderStyle>
			</asp:datagrid>
			<br>
			<br>
			<asp:label id="lblAPDS" text="In APDS: Not in CyberSource" CssClass="arial_11_bold" Visible="False" runat="server"></asp:label>
			<asp:datagrid id="dgReport2" runat="server" cssclass="arial_9" width="604px" AllowPaging="False" onitemdatabound="ItemDataBound2">
				<AlternatingItemStyle BackColor="LightYellow"></AlternatingItemStyle>
				<ItemStyle CssClass="arial_12"></ItemStyle>
				<HeaderStyle Height="22px" CssClass="arial_12_bold" BackColor="#CCCCCC"></HeaderStyle>
			</asp:datagrid>
			<br>
			<br>
			<asp:datagrid id="dgReport3" runat="server" cssclass="arial_9" width="604px" AllowPaging="False" onitemdatabound="ItemDataBound3">
				<AlternatingItemStyle BackColor="LightYellow"></AlternatingItemStyle>
				<ItemStyle CssClass="arial_12"></ItemStyle>
				<HeaderStyle Height="22px" CssClass="arial_12_bold" BackColor="#CCCCCC"></HeaderStyle>
			</asp:datagrid>
		</form>
	</body>
</HTML>
