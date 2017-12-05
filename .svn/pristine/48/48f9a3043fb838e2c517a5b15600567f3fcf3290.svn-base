<!-- CHG0109406 - CH1 - Added a label lblHeaderTimeZone to display the timezone for the results displayed  -->
<%-- CHG0110069 - CH1 - Appended the /Time along with the Date Label--%>
<%@ Page language="c#" Codebehind="InsSearchXlsPrint.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.InsSearchXlsPrint" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Transaction Search</title>
		<LINK href="../style.css" type="text/css" rel="stylesheet">
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
	<body MS_POSITIONING="GridLayout">
		<form id="InsSearchXlsPrint" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="910" border="0">
				<tr height="41">
					<td class="arial_12_bold" align="left"><IMG src="../images/msc_logo_sm.gif" align="left" border="0"></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td></td>
				</tr>
				<tr>
					<td class="arial_12_bold" align="left">Transaction Search</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
			</table>
			<table class="arial_12" cellSpacing="0" cellPadding="0" rules="none" border="0">
			<%--CHG0110069 - CH1 - BEGIN - Appended the /Time along with the Date Label--%>
				<tr>
					<td style="WIDTH: 105px">Run Date/Time:</td>
					<td align="left"><asp:label id="lblRunDate" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td style="WIDTH: 105px">Date/Time Range:</td>
					<td align="left" colspan="3"><asp:label id="lblDateRange" Width="500" runat="server"></asp:label></td>
				</tr>
				<%--CHG0110069 - CH1 - END - Appended the /Time along with the Date Label--%>
				<tr>
					<td colSpan="3">&nbsp;</td>
				</tr>
				<!--
				<tr>
					<td valign="center" height="17">
						<asp:label id="Label1" backcolor="#cccccc" runat="server" cssclass="arial_12_bold"></asp:label></td>
				</tr>
				--></table>
			<!-- <table>
			<tr>
				<td>-->
			<table cellSpacing="0" cellPadding="0" width="910" border="0">
				<tr>
					<td>
						<table id="tblIns" borderColor="#cccccc" cellSpacing="0" cellPadding="0" rules="none" border="0" runat="server">
							<tr>
								<td vAlign="center" bgColor="#cccccc" height="17">
								<!-- CHG0109406 - CH1 - BEGIN - Added a label lblHeaderTimeZone to display the timezone for the results displayed  -->
								<table width="100%" runat="server">
									<tr>
									<td width="20%"><asp:label id="lblInsTitle" runat="server" Width="465" backcolor="#cccccc" Visible="False" cssclass="arial_12_bold"></asp:label></td>
									<td align="right" width="80%"><asp:Label ID="lblHeaderTimeZone" runat="server"  CssClass="arial_12_bold" style="font-style:italic;text-align:right;padding-left:66%" Text="All timezones are in Arizona" /></td>
									</tr>
									</table>
								<!-- CHG0109406 - CH1 - END - Added a label lblHeaderTimeZone to display the timezone for the results displayed  -->
								</td>
							</tr>
							<tr>
								<td><asp:datagrid id="dgSearch1" runat="server" cssclass="arial_9" CellPadding="2" borderstyle="None" allowpaging="false" onitemdatabound="ItemDataBound1">
										<alternatingitemstyle backcolor="LightYellow"></alternatingitemstyle>
										<itemstyle cssclass="arial_12"></itemstyle>
										<headerstyle height="22px" cssclass="arial_12_bold" backcolor="#ffffff"></headerstyle>
									</asp:datagrid><br>
								</td>
							</tr>
						</table>
						<!-- header for membership transactions table -->
						<table id="tblMbr" borderColor="#cccccc" cellSpacing="0" cellPadding="0" rules="none" border="0" runat="server">
							<tr>
								<td vAlign="center" bgColor="#cccccc" height="17"><asp:label id="lblMbrTitle" runat="server" Width="910" backcolor="#cccccc" Visible="False" cssclass="arial_12_bold"></asp:label></td>
							</tr>
							<!-- column names and data rows for membership transactions table -->
							<!-- <table bordercolor="#cccccc" cellspacing="0" cellpadding="0" rules="none" border="0"> -->
							<tr>
								<td>
									<!-- end of results --></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<!--
					</td>
				</tr>
			</table>
			--></form>
	</body>
</HTML>
