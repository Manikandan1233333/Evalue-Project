<%-- CHG0109406 - CH1 - Added a label lblHeaderTimeZone to display the timezone for the results displayed --%>
<%-- CHG0110069 - CH1 - Appended the /Time along with the Date Label--%>
<%@ Page language="c#" Codebehind="TurnIn_General_PrintXls.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.TurnIn_General_PrintXls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Manager Reports</title>
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
	<body leftmargin="15">
		<form id="TurnIn_General_PrintXls" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td class="arial_11_bold_white" width="604" colSpan="2">
						<table width="100%" border="0">
							<tr height="41">
								<td class="arial_12_bold" align="left" colspan="2"><IMG src="../images/msc_logo_sm.gif" width="604" border="0"></td>
							</tr>
							<tr>
								<td height="10" colspan="2">&nbsp;</td>
							</tr>
							<tr>
								<td class="arial_11_bold_white" width="100%" bgColor="#333333" height="22" colspan="2">&nbsp;&nbsp;Reports 
									Management
								</td>
							</tr>
						</table>
					</td>
					<td width="146"></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="2">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<tr>
								<td colspan="2">
									<table>
									<%--CHG0110069 - CH1 - BEGIN - Appended the /Time along with the Date Label--%>
										<tr>
											<td class="arial_12_bold" width="100">Run Date/Time:</td>
											<td align="left" colspan="2"><asp:label id="lblRunDate" runat="server" CssClass="arial_12"></asp:label></td>
										</tr>
										<tr id="trDateRange" runat="server">
											<td class="arial_12_bold">Date/Time Range:</td>
											<td align="left" colspan="2"><asp:label id="lblDateRange" runat="server" CssClass="arial_12"></asp:label></td>
										</tr>
										<%--CHG0110069 - CH1 - END - Appended the /Time along with the Date Label--%>
										<tr>
											<td class="arial_12_bold">Report Status:</td>
											<td align="left" colspan="2"><asp:label id="lblReportStatus" runat="server" CssClass="arial_12"></asp:label></td>
										</tr>
										<tr>
											<td class="arial_12_bold">User(s):</td>
											<td align="left" colspan="2"><asp:label id="lblUser" runat="server" CssClass="arial_12"></asp:label></td>
										</tr>
										<tr>
											<td class="arial_12_bold">Rep DO:</td>
											<td align="left" colspan="2"><asp:label id="lblRepDO" runat="server" CssClass="arial_12"></asp:label></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="3" height="10">&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td width="604" colSpan="2"></td>
				</tr>
				<tr>
				<tr>
					<td colSpan="2">
						<table cellSpacing="0" cellPadding="0">
						<%-- CHG0109406 - CH1 - BEGIN - Added a label lblHeaderTimeZone to display the timezone for the results displayed --%>
							<tr id="trCaption" runat="server">
								<td bgColor="#cccccc">
								<table runat="server" width="100%">
								<tr>
								<td width="30%"><asp:label id="lblCaption" runat="server" CssClass="arial_12_bold"></asp:label></td>
								<td width="70%" align="right"><asp:label id="lblHeaderTimeZone"  text="All timezones are in Arizona" style="font-style:italic;" cssclass="arial_11_bold"  runat="server"></asp:label></td>
								</tr>
								</table>

								</td>
							</tr>
							<%-- CHG0109406 - CH1 - END - Added a label lblHeaderTimeZone to display the timezone for the results displayed --%>
							<tr>
								<td><asp:datagrid id="dgReport" CssClass="arial_12" CellPadding="2" Runat="server" OnItemDataBound="ItemDataBound1">
										<HeaderStyle CssClass="arial_12_bold"></HeaderStyle>
										<FooterStyle HorizontalAlign="Center" BackColor="#CCCCCC"></FooterStyle>
									</asp:datagrid></td>
							</tr>
							<tr height="20">
								<td bgColor="#cccccc">&nbsp;</td>
							</tr>
						</table>
					</td>
					<td>&nbsp;</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
