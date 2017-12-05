<!-- CHG0109406 - CH1 - Modified the Table Width and Added a label lblHeaderTimeZone to display the timezone for the results displayed  -->
<%@ Page language="c#" Codebehind="ins_PrintXls.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.ins_PrintXls" %>
<%@ Register TagPrefix="uc" TagName="PrintHeader" Src="../Controls/PrintHeader.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>LoadXls</title>
		<link rel="stylesheet" type="text/css" href="../style.css">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<!-- PT.Ch1 - START: Added by COGNIZANT - 7/01/2008 - To enable default printing of the Print Version -->
		<script language="javascript">
		var strquery = window.location.search.substring(1);
		var strparams = strquery.split('&');
		for(var i=0;i<strparams.length;i++)
		{
		  var strkval = strparams[i].split('=');
		  if(strkval[0] == "type" && strkval[1] == "PRINT")
		  {
		  	window.print();
		  }
		}
		</script>
		<!-- PT.Ch1 - END -->
	</HEAD>
	<body>
		<form id="ins_PrintXls" name="frmPrintXls" runat="server">
			<uc:PrintHeader id="PrintHeader" runat="server" /><br>
			<!-- CHG0109406 - CH1 - BEGIN - Modified the Table Width and Added a label lblHeaderTimeZone to display the timezone for the results displayed  -->
			<table width="819" class="arial_12">
			<tr>
				<td width="50%"><asp:label id="lblCyber" text="In CyberSource: Not in APDS" CssClass="arial_11_bold" Visible="False" runat="server"></asp:label></td>
				<td width="50%" align="right"><asp:label id="lblHeaderTimeZone"  text="All timezones are in Arizona" style="font-style:italic;text-align:right;padding-right:100px;" cssclass="arial_11_bold" visible="false" runat="server"></asp:label></td>
				</tr>
				<tr>
				<td colspan="2" width="100%">
    			<!-- CHG0109406 - CH1 - END - Modified the Table Width and Added a label lblHeaderTimeZone to display the timezone for the results displayed  -->
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
				</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
