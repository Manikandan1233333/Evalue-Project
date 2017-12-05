<%@ Page language="c#" Codebehind="PrintRpt.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.PrintRpt" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<!-- 
PT.Ch1 - Added by COGNIZANT on 9/8/08
Added AlternatingItemStyle to the datagrids set the alternating color as LightYellow
 -->
<html>
	<head>
		<title>PrintRpt</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body ms_positioning="GridLayout">
		<form id="PrintRpt" name="frmPrint" runat="server">
			<asp:table id="tblHeader" runat="server" width="604px" height="14px" cellpadding="0" cellspacing="0">
				<asp:tablerow>
					<asp:tablecell horizontalalign="Left" text="MSC Reports" cssclass="arial_12_bold" forecolor="White">
						<img height="41" src="../images/msc_logo_sm.gif" width="604" align="left" border="0"></asp:tablecell>
				</asp:tablerow>
			</asp:table><br>
			<!--PT.Ch1 - COGNIZANT - 9/8/08 START - Added AlternatingItemStyle to the datagrids set the alternating color as LightYellow-->			
			<asp:datagrid id="dgGrid" runat="server" allowpaging="False" cssclass="arial_12_blue_link" width="606px" autogeneratecolumns="false" onitemcreated="ItemCreated" onitemdatabound="ItemDataBound">
				<itemstyle cssclass="arial_12"></itemstyle>
				<alternatingitemstyle backcolor="LightYellow"></alternatingitemstyle>
				<headerstyle height="22px" cssclass="arial_12_bold" backcolor="#CCCCCC"></headerstyle>
				<columns>
					<asp:boundcolumn datafield="USERNAME" headertext="User" />
					<asp:boundcolumn datafield="DTCREATED" headertext="Date" />
					<asp:boundcolumn datafield="RENEWALCOUNT" headertext="Transactions">
						<itemstyle horizontalalign="right" />
					</asp:boundcolumn>
					<asp:boundcolumn datafield="RENEWALREVENUE" headertext="Revenue" dataformatstring="{0:c}">
						<itemstyle horizontalalign="right" />
					</asp:boundcolumn>
				</columns>
			</asp:datagrid>
			<asp:datagrid id="dgGrid1" runat="server" width="606px" onitemdatabound="ItemDataBound1" onitemcreated="ItemCreated1" cssclass="arial_12_blue_link" autogeneratecolumns="false" allowpaging="False">
				<itemstyle cssclass="arial_12"></itemstyle>
				<alternatingitemstyle backcolor="LightYellow"></alternatingitemstyle>
				<headerstyle height="22px" cssclass="arial_12_bold" backcolor="#CCCCCC"></headerstyle>
				<columns>
					<asp:boundcolumn datafield="DTCREATED" headertext="Date" />
					<asp:boundcolumn datafield="USERNAME" headertext="User" />
					<asp:boundcolumn datafield="RENEWALCOUNT" headertext="Transactions">
						<itemstyle horizontalalign="right" />
					</asp:boundcolumn>
					<asp:boundcolumn datafield="RENEWALREVENUE" headertext="Revenue" dataformatstring="{0:c}">
						<itemstyle horizontalalign="right" />
					</asp:boundcolumn>
				</columns>
			</asp:datagrid>
			<!--PT.Ch1 - COGNIZANT - 9/8/08 END - Added AlternatingItemStyle to the datagrids set the alternating color as LightYellow-->							
			<asp:datagrid id="dgGrid2" runat="server" width="606px" cssclass="arial_12_blue_link" allowpaging="False" onitemcreated="ItemCreated2" onitemdatabound="ItemDataBound2">
				<alternatingitemstyle backcolor="LightYellow"></alternatingitemstyle>
				<itemstyle cssclass="arial_12"></itemstyle>
				<headerstyle height="22px" cssclass="arial_12_bold" backcolor="#CCCCCC"></headerstyle>
			</asp:datagrid>
		</form>
	</body>
</html>
