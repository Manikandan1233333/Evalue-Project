<%@ Register TagPrefix="MSCR" TagName="Header" Src="InsRptHeader.ascx" %>
<%@ Page language="c#" Codebehind="InsLoadXls.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.InsLoadXls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>LoadXls</title>
		<link rel=stylesheet type=text/css href=../style.css>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="InsPrintRpt" name="frmPrint" runat="server">
			<MSCR:Header id="ctlHeader" runat="server" /><br>
			<table width="606" bgcolor="lightyellow" class="arial_12">
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
			</table>
		</form>
	</body>
</HTML>
