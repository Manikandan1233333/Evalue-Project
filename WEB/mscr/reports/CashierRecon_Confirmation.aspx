<%@ Page language="c#" Codebehind="CashierRecon_Confirmation.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.CashierRecon_Confirmation" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Cashier's Receonciliation Confirmation</title>
		<LINK href="../Style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body>
		<form id="CashierRecon_Confirmation" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="780" border="0">
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="750" border="0">
							<tr>
								<td width="30" height="30">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td width="30"></td>
										</tr>
									</table>
								</td>
								<td height="30">
									<table borderColor="#333333" height="30" cellSpacing="0" cellPadding="0" bgColor="#ffffcc" border="0">
										<tr>
											<td vAlign="center"><IMG src="../images/red_arrow.gif"></td>
											<td width="10"></td>
											<td class="arial_12_bold" vAlign="center" width="400"><asp:label id="lblMessage" runat="server"></asp:label></td>
										</tr>
									</table>
								</td>
								<td width="30" height="30">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td width="30"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="750" border="0">
							<TR>
								<td height="30" colspan="3"></td>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" width="750" border="0">
							<TR>
								<td width="30"></td>
								<td width="604" class="arial_13_bold">&nbsp; Cashiering Summary Report(Approved)</td>
								<td width="116"></td>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td>
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr id="trCollSummarySplit" runat="server">
								<td colSpan="3" height="20"></td>
							</tr>
							<tr id="trCollSummary" runat="server">
								<td width="30" height="20">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td width="30"></td>
										</tr>
									</table>
								</td>
								<td>
									<!--
									CSAA Products here
									-->
									<table id="tblCollSummary" cellSpacing="0" cellPadding="5" width="604" border="1" style="BORDER-COLLAPSE:collapse" runat="server" bgColor="#cccccc">
									</table>
								</td>
								<td width="30" height="30">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td width="30"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr id="trOtherCollSummarySplit" runat="server">
								<td colSpan="3" height="30"></td>
							</tr>
							<tr id="trOtherCollSummary" runat="server">
								<td width="30" height="30">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td width="30"></td>
										</tr>
									</table>
								</td>
								<td>
									<!--
									All Other Products here
									-->
									<table id="tblOtherCollSummary" cellSpacing="0" cellPadding="5" width="604" border="1" style="BORDER-COLLAPSE:collapse" runat="server" bgColor="#cccccc">
									</table>
								</td>
								<td width="30" height="30">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td width="30"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td colSpan="3" height="30"></td>
							</tr>
							<tr>
								<td width="30" height="30">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td width="30"></td>
										</tr>
									</table>
								</td>
								<td>
									<!--
									Total CSAA Products and Other Products here
									-->
									<table id="tblTotalCollSummary" cellSpacing="0" cellPadding="5" width="300" border="1" style="BORDER-COLLAPSE:collapse" runat="server" bgColor="#cccccc">
									</table>
								</td>
								<td width="30" height="30">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td width="30"></td>
										</tr>
									</table>
								</td>
							</tr>
							<tr>
								<td colSpan="3" height="30"></td>
							</tr>
							<tr>
								<td width="30" height="30">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td width="30"></td>
										</tr>
									</table>
								</td>
								<td>
								 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
								 <asp:imagebutton id="btnBackToReports" ImageAlign="Middle" CausesValidation="False" BorderWidth="0" Width="164" ImageUrl="/PaymentToolimages/btn_backtoreports.gif" Runat="server" onclick="btnBackToReports_Click"></asp:imagebutton></td>
								<td width="30" height="30">
									<table cellSpacing="0" cellPadding="0" border="0">
										<tr>
											<td width="30"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
