<%@ Page language="c#" Codebehind="SalesRep_Confirmation.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.SalesRep_Confirmation" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
 <!--
 * History:
 * MODIFIED BY COGNIZANT
 * 12/5/2005 Q4-retrofit.ch1: Added trCollOtherSummary and trCollTotalSummary Rows to generate 
 *			  Other Products and Total Collection Summary Grids
 * 67811A0  - PCI Remediation for Payment systems CH1: Added hidden drop down control to load the data report type for the current user by cognizant on 1/30/2011
* MAIG - CH1 - Modified the Verbiage to remove the Sales & Service
* CHGXXXXXX - Removing btnMbrDetail,btnCrossRef as part of Code Clean Up - March 2016
-->
<HTML>
	<HEAD>
		<title>Payment Tool</title>
		<LINK href="../Style.css" type="text/css" rel="stylesheet">
			<script language="javascript">
		  function Open_Report(ReportType)
			{
			var PrintXlsURL="TurnIn_SalesRep_PrintXls.aspx?ReportType=" + ReportType + "&Screen=" +"CS";
			window.open(PrintXlsURL,"MyTurnIn");
			return false;
			}
			</script>
	</HEAD>
	<body>
		<form id="SalesRep_Confirmation" method="post" runat="server">
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
									<table height="30" borderColor="#333333" cellSpacing="0" cellPadding="0" bgColor="#ffffcc" border="0">
										<tr>
											<td vAlign="center"><IMG src="../images/red_arrow.gif"></td>
											<td width="10"></td>
											<td width="400" class="arial_13_bold" vAlign="center">Transactions have been 
												submitted to Cashier for approval.
											</td>
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
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td colspan="3" height="30"></td>
							</tr>
							<tr>
								<td width="30"></td>
								<td colspan="2"><asp:label class="arial_12_bold_red" id="lblErrMsg" runat="server">&nbsp;</asp:label></td>
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
									<table>
										<tr>
											<td>
												<table id="tabCollSummary" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="5" width="604" border="1" runat="server">
												</table>
											</td>
										</tr>
										<tr id="trCollOtherSummary" runat="server">
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td>
												<table id="tblCollOtherSummary" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="5" width="604" border="1" runat="server">
												</table>
											</td>
										</tr>
										<tr id="trCollTotalSummary" runat="server">
											<td>&nbsp;</td>
										</tr>
										<tr>
											<td>
												<table id="tblCollTotalSummary" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="5" width="300" border="1" runat="server">
												</table>
											</td>
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
												<table borderColor="#cccccc" cellSpacing="0" cellPadding="0" width="620" bgColor="#ffffcc" border="2">
													<tr>
														<td>
															<table borderColor="#cccccc" cellSpacing="0" cellPadding="0" width="620" bgColor="#ffffcc" border="0">
																<tr>
																	<td colSpan="3" height="10"></td>
																</tr>
																<tr>
																	<td class="arial_13_bold" align="middle" colSpan="3">Please print the following 
																		reports:
																	</td>
																</tr>
																<tr>
																	<td colSpan="3" height="10"></td>
																</tr>
																<tr>
																	<td width="40"></td>
																	<td valign="top" align="middle">
																		<asp:ImageButton ID="btnCollTurninList" Runat="server" ImageUrl="/PaymentToolimages/btn_coll_turnin.gif" Width="168" BorderWidth="0" CausesValidation="False" ImageAlign="Middle"></asp:ImageButton>
																		&nbsp;
																		 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
																		<!--CHGXXXXXXX- Server migration - Commented the btnMbrDetail,btnCrossRef button as as part of Code Clean Up - Start - March 2016 --> 
																		&nbsp;
																		 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
                                                                        <!--CHGXXXXXXX- Server migration - Commented the btnMbrDetail,btnCrossRef button as as part of Code Clean Up- End- March 2016 -->
																	</td>
																	<td width="40"></td>
																</tr>
																<tr>
																	<td colSpan="3" height="10"></td>
																</tr>
															</table>
														</td>
													</tr>
												</table>
											</td>
											<td width="30" height="30">
												<table cellspacing="0" cellpadding="0" border="0">
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
						<asp:DropDownList ID="HidddenReportType" Visible="False" runat="server" MaxLength="40"></asp:DropDownList>
		</form>
	</body>
</HTML>
