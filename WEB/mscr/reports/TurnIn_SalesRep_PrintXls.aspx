<%@ Page language="c#" Codebehind="TurnIn_SalesRep_PrintXls.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.TurnIn_SalesRep_PrintXls" %>
<HTML>
	<HEAD>
		<title>My Turn-In</title> 
		<!--
 * History:
 * MODIFIED BY COGNIZANT AS PART OF Q4 RETROFIT CHANGES
 * 12/2/2005 Q4-Retrofit.ch1 : Added an attribute UploadType in tdProdType to assign [Upload Type] Value
 *			 Added RunAt and Id Attribute to td Policy Number.Added Runat and Cell spacing
 *           Attributes to tblSalesTurnInList table.Added a table row trCollSummary in the 
 *			 tblSalesTurnInList table.
 *
 * STAR Retrofit III Changes: 
 * 04/04/2007 - Changes done as part of CSR #5595 
 * STAR Retrofit III.Ch1: Added caption for summary report
 * MODIFIED BY COGNIZANT AS A PART OF TIME ZONE CHANGE ENHANCEMENT ON 8-31-2010
* TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) in dgVoidPayment on 8-31-2010 by cognizant.
* SalesTurnin_Report Changes.Ch1 : Added signature to the print version of sales turn in report
* CHG0109406 - CH1 - Added the td which displays the Timezone information
* CHG0109406 - CH2 - Modified the Column name from "Receipt Date/Time(MST Arizona)" to Receipt Date/Time(Arizona)
* CHG0110069 - CH1 - Appended the /Time along with the Date Label
* CHG0113938 - To display Name instead of Member Name & Policy Number instead of Policy/Mbr Number in the Sales Turn In report Page.
-->
<LINK href="../style.css" type="text/css" rel="stylesheet">
<!-- PT.Ch1 - START: Added by COGNIZANT - 7/01/2008 - To enable default printing of the Print Version -->
		<script language="javascript">
		var strquery = window.location.search.substring(1);
		var strparams = strquery.split('&');
		for(var i=0;i<strparams.length;i++)
		{
		  var strkval = strparams[i].split('=');
		  if((strkval[0] == "type" && strkval[1] == "PRINT") || (strkval[0] == "Screen" && strkval[1] == "CS"))
		  {
		  	window.print();
		  }
		}
		</script>
		<!-- PT.Ch1 - END -->
	</HEAD>
	<body leftMargin="15">
		<form id="TurnIn_SalesRep_PrintXls" method="post" runat="server">
			<table id="tblHeading" cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td class="arial_11_bold_white" width="604" colSpan="2">
						<table width="100%" border="0">
							<tr>
								<td class="arial_12_bold" align="left"><IMG src="../images/msc_logo_sm.gif" width="604" border="0">
								</td>
							</tr>
							<tr>
								<td height="25">&nbsp;</td>
							</tr>
							<tr>
								<td class="arial_11_bold_white" width="100%" bgColor="#333333" colSpan="2" height="22">&nbsp;&nbsp;My 
									Turn-In
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
						<table border="0">
							<tr>
								<td colSpan="2"><asp:label class="arial_12_bold_red" id="lblErrMsg" runat="server" visible="False">&nbsp;</asp:label></td>
							</tr>
							<%--CHG0110069 - CH1 - BEGIN - Appended the /Time along with the Date Label--%>
							<tr>
								<td class="arial_12_bold" width="120">Run Date/Time:</td>
								<td align="left"><asp:label id="lblRunDate" runat="server" CssClass="arial_12" Width="250"></asp:label></td>
							</tr>
							<tr id="trDateRange" runat="server">
								<td class="arial_12_bold">Date/Time Range:</td>
								<td><asp:label id="lblDateRange" runat="server" CssClass="arial_12">&nbsp;</asp:label></td>
							</tr>
							<%--CHG0110069 - CH1 - END - Appended the /Time along with the Date Label--%>
							<tr>
								<td class="arial_12_bold">Report Status:</td>
								<td><asp:label id="lblReportStatus" runat="server" CssClass="arial_12">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td class="arial_12_bold">User(s):</td>
								<td><asp:label id="lblUser" runat="server" CssClass="arial_12">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td class="arial_12_bold">Rep DO:</td>
								<td><asp:label id="lblRepDO" runat="server" CssClass="arial_12">&nbsp;</asp:label></td>
							</tr>
						</table>
					</td>
					<td></td>
				</tr>
				<tr>
					<td colSpan="3" height="10"></td>
				</tr>
				<tr id="trSalesRepeater" runat="server">
					<td width="604" colSpan="3">
						<table id="tblRepeater" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0" runat="server">
							<tr>
								<td colSpan="9">
									<table id="tblInnerRepeater" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="2" width="100%" border="1">
										<asp:repeater id="rptSalesTurnInRepeater" runat="server" OnItemDataBound="rptSalesTurnInRepeater_ItemBound">
											<HeaderTemplate>
												<tr class="item_template_header">
												<!-- CHG0109406 - CH1 - BEGIN - Added the td which displays the Timezone information -->
													<td class="arial_12_bold" colspan="4" id="tdHeader1" style="border-style:none;" runat="server" width="40%" bgColor="#cccccc">
														Sales Collection Turn-In List</td>
														<td class="arial_12_bold" colspan="5" style="font-style:italic;text-align:right;border-style:none" runat="server">All timezones are in Arizona</td>
												<!-- CHG0109406 - CH1 - END - Added the td which displays the Timezone information -->
												</tr>
												<tr class="item_template_header_white">
												<!-- CHG0109406 - CH2 - BEGIN - Modified the Column name from "Receipt Date/Time(MST Arizona)" to Receipt Date/Time(Arizona) -->
													<td class="arial_12_bold" width="100">Receipt Date/Time(Arizona)</td>
												<!-- CHG0109406 - CH2 - END - Modified the Column name from "Receipt Date/Time(MST Arizona)" to Receipt Date/Time(Arizona) -->
													<td class="arial_12_bold" width="60">Status</td>
                                                    <%-- CHG0113938 - BEGIN - Modified the Column name from "Member Name" to "Name" --%>
													<td class="arial_12_bold" width="80">Name</td>
                                                    <%-- CHG0113938 - End - Modified the Column name from "Member Name" to "Name" --%>
													<td class="arial_12_bold" width="80">Product Type</td>
													<td class="arial_12_bold" width="60">Trans Type</td>
                                                    <%-- CHG0113938 - BEGIN - Modified the Column name from "Policy/Mbr Number" to "Policy Number" --%>
													<td class="arial_12_bold" width="50">Policy Number</td>
                                                    <%-- CHG0113938 - END - Modified the Column name from "Policy/Mbr Number" to "Policy Number" --%>
													<td class="arial_12_bold" width="70">Receipt Number</td>
													<td class="arial_12_bold" width="40">Pymt Method</td>
													<td class="arial_12_bold" width="64">Amount</td>
												</tr>
											</HeaderTemplate>
											<ItemTemplate>
												<tr id="trItemRow" runat="server">
													<td class="arial_12">
														<asp:Label ID="lblReceiptDate" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Receipt Date") %>' >
														</asp:Label>&nbsp;
													</td>
													<td class="arial_12">
														<asp:Label ID="lblStatus" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Status") %>' >
														</asp:Label>&nbsp;
													</td>
													<td class="arial_12">
														<asp:Label ID="lblMemberName" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Member Name") %>' >
														</asp:Label>&nbsp;
													</td>
													<td class="arial_12" id="tdProdType" runat="server" UploadType = '<%# DataBinder.Eval(Container.DataItem, "Upload Type") %>'><%# DataBinder.Eval(Container.DataItem, "Product Type") %>
													</td>
													<td class="arial_12" id="tdTransType" runat="server"><%# DataBinder.Eval(Container.DataItem, "Trans Type") %>
													</td>
													<td class="arial_12" id="tdPolicyNumber" runat="Server"><%# DataBinder.Eval(Container.DataItem, "Policy Number") %>
													</td>
													<td class="arial_12" id="tdReceiptNo" valign="middle"><%# DataBinder.Eval(Container.DataItem, "Receipt Number") %>
														<asp:Label ID="lblReceiptNo" Runat ="server" visible="False" value = '<%# DataBinder.Eval(Container.DataItem, "Receipt Number") %>' >
														</asp:Label>
													</td>
													<td class="arial_12" id="tdPymtType" runat="server"><%# DataBinder.Eval(Container.DataItem, "Pymt Method") %>
													</td>
													<td class="arial_12" id="tdAmount" runat="server" align="right" amount='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'>
														<%# (Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Amount"))).ToString("$##0.00") %>
													</td>
												</tr>
											</ItemTemplate>
											<FooterTemplate>
												<!-- Modified by Cognizant on 4-21-05. Changed the Caption "Total Turn In" to "Total"-->
												<tr class="item_template_row">
													<td class="total_cell_arial_12_bold" colspan="8" align="right" id="tdTotalHead" runat="server">TOTAL 
														&nbsp;&nbsp;&nbsp;
													</td>
													<td id="tdTotal" runat="server" class="total_cell_arial_12_bold" align="right">
														<asp:Label ID="lblTotal" Runat="server"></asp:Label>
													</td>
												</tr>
												<tr>
													<td bgColor="#cccccc" colSpan="9" height="17">&nbsp;
													</td>
												</tr>
											</FooterTemplate>
										</asp:repeater></table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr id="trBlankRow1" runat="server">
					<td bgColor="#ffffff" colSpan="3">&nbsp;</td>
				</tr>
			</table>
			<table id="tblSalesTurnInList" cellSpacing="0" cellPadding="0" width="750" runat="server">
				<!--STAR Retrofit III.Ch1: START - Added caption for summary report-->
				<tr id="trCaptionSummary" runat="server">
					<td class="arial_13_bold" bgColor="#ffffff">Collection Summary</td>
					<td bgColor="#ffffff">&nbsp;</td>
				</tr>
				<tr id="trBlankRow2" runat="server">
					<td bgColor="#ffffff" colSpan="3">&nbsp;</td>
				</tr>
				<!--STAR Retrofit III.Ch1: END -->
				<tr id="trCollSummary" runat="server">
					<td>
						<table id="tblCollSummary" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="5"  width="604" bgColor="#cccccc" border="1" runat="server" FixedWidth="604">
						</table>
					</td>
					<td></td>
				</tr>
				<tr id="trOtherCollSummarySplit" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
				</tr>
				<tr id="trOtherCollSummary" runat="server">
					<td>
						<table id="tblOtherCollSummary" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="5" width="604" bgColor="#cccccc" border="1" runat="server" FixedWidth="604">
						</table>
					</td>
					<td></td>
				</tr>
				<tr id="trTotalCollSummarySplit" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
				</tr>
				<tr id="trTotalCollSummary" runat="server">
					<td>
						<table id="tblTotalCollSummary" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="5" width="300" bgColor="#cccccc" border="1" runat="server">
						</table>
					</td>
					<td></td>
				</tr>
			</table>
			<!--Repeater for Membership Detail-->
			<table id="tblMembershipDetail" cellSpacing="0" cellPadding="0" width="100%" align="left" bgColor="#ffffff" border="0" runat="server">
				<tr>
					<td colSpan="2">
						<table style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="2" width="100%" align="left" border="1">
							<asp:repeater id="rptMembershipDetail" runat="server" OnItemDataBound="rptMembershipDetail_ItemBound">
								<HeaderTemplate>
									<tr class="item_template_header">
										<td class="arial_12_bold" colspan="20" id="tdHeaderMember" runat="server" bgColor="#cccccc">
											Membership Detail Report</td>
									</tr>
									<tr class="item_template_header_white">
										<td class="arial_12_bold" width="7%">Member Last Name</td>
										<td class="arial_12_bold" width="7%">Member First Name</td>
										<td class="arial_12_bold" width="2%">M.I.</td>
										<td class="arial_12_bold" width="8%">Address</td>
										<td class="arial_12_bold" width="9%">City</td>
										<td class="arial_12_bold" width="2%">State</td>
										<td class="arial_12_bold" width="6%">Zip</td>
										<td class="arial_12_bold" width="6%">DOB</td>
										<td class="arial_12_bold" width="2%">Gen der</td>
										<td class="arial_12_bold" width="5%">Day Phone</td>
										<td class="arial_12_bold" width="5%">Eve Phone</td>
										<td class="arial_12_bold" width="7%">Mbr Number</td>
										<td class="arial_12_bold" width="7%">Role</td>
										<td class="arial_12_bold" width="4%">User(s)</td>
										<td class="arial_12_bold" width="2%">Src Code</td>
										<td class="arial_12_bold" width="2%">Base Year</td>
										<td class="arial_12_bold" width="2%">Prod Type</td>
										<td class="arial_12_bold" width="2%">Effective Date</td>
										<td class="arial_12_bold" width="8%">Amount</td>
										<td class="arial_12_bold" width="7%">Pymt Type</td>
									</tr>
								</HeaderTemplate>
								<ItemTemplate>
									<tr id="trItemRowMember" runat="server">
										<td class="arial_12" id="tdMemberLastname" runat="server"><%# DataBinder.Eval(Container.DataItem, "Member Last Name") %>
											<asp:Label ID="lblOrderID" Runat ="server" visible="False" value = '<%# DataBinder.Eval(Container.DataItem, "Order ID") %>' >
											</asp:Label>
										</td>
										<td class="arial_12" id="tdMemberFirstName" runat="server"><%# DataBinder.Eval(Container.DataItem, "Member First Name") %>
										</td>
										<td class="arial_12" id="tdMI" runat="server"><%# DataBinder.Eval(Container.DataItem, "MI") %>
										</td>
										<td class="arial_12">
											<asp:Label ID="lblAddress" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Address") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblCity" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "City") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblState" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "State") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblZip" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Zip") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12" id="tdDOB" runat="server"><%# DataBinder.Eval(Container.DataItem, "DOB") %>
										</td>
										<td class="arial_12" id="tdGender" runat="server"><%# DataBinder.Eval(Container.DataItem, "Gender") %>
										</td>
										<td class="arial_12">
											<asp:Label ID="lblDayPhone" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Day Phone") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblEvePhone" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Eve Phone") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblMemberNumber" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Mbr Number") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12" id="tdRole" runat="server"><%# DataBinder.Eval(Container.DataItem, "Role") %>
										</td>
										<td class="arial_12">
											<asp:Label ID="lblMemberUsers" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Users") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblSrcCode" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Src Code") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblBaseYear" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Base Year") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblProdType" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Prod Type") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblEffectiveDate" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Effective Date") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12" align="right">
											<asp:Label ID="lblAmount" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Amount","{0:c}") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblPymtType" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Pymt Type") %>' >
											</asp:Label>&nbsp;
										</td>
									</tr>
								</ItemTemplate>
								<FooterTemplate>
									<tr class="item_template_row">
										<td class="total_cell_arial_12_bold" colspan="18" align="right" id="tdTotalMemberHead" runat="server">TOTAL 
											&nbsp;&nbsp;&nbsp;
										</td>
										<td id="tdTotalMember" runat="server" class="total_cell_arial_12_bold" width="10" align="right">
											<asp:Label ID="lblTotalMember" Runat="server"></asp:Label>
										</td>
										<td>&nbsp;</td>
									</tr>
									<tr>
										<td bgColor="#cccccc" colSpan="20" height="17">
										</td>
									</tr>
								</FooterTemplate>
							</asp:repeater></table>
					</td>
				</tr>
			</table>
			<br>
			<!--End of membership Repeater-->
			<!--Cross Reference Datagrid-->
			<table id="tblCrossReference" cellSpacing="0" cellPadding="2" width="604" runat="server">
				<!--Cross Reference Datagrid Header-->
				<tr>
					<td class="arial_12_bold" vAlign="center" bgColor="#cccccc" height="17">Cross-Reference 
						Report</td>
				</tr>
				<!--End of Cross Reference Datagrid Header-->
				<tr>
					<td><asp:datagrid id="dgCrossReference" runat="server" OnItemDataBound="dgCrossReference_ItemBound" width="604px" CellPadding="2" borderstyle="None" cssclass="arial_12">
							<headerstyle height="22px" CssClass="arial_12_bold" backcolor="#ffffff"></headerstyle>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td class="arial_11">&nbsp;&nbsp;<asp:label id="lblPageNum2" runat="server"></asp:label></td>
				</tr>
			</table>
			<!--End of Cross Reference Datagrid-->
			<%--SalesTurnin_Report Changes.Ch1 : START - Added signature to the print version of sales turn in report--%>
			<br>
			<br>
			<asp:Panel ID = "pnlsignature" runat="server" Visible="false">
			<table>			
			<tr>
				<td class="arial_12_bold"  align="left">Sales Rep/Service Rep:</td>
				<td align="left"><asp:label id="lblsalesrep" Text="______________________________" runat="server" CssClass="arial_12" Width="250"></asp:label></td>
			</tr>
			</table>
			<br>
			<br>
			<table>
			<tr>
			    <td class="arial_12_bold"  align="left">Branch Cashier:</td>
				<td align="left"><asp:label id="lblbranchcashier" Text="______________________________" runat="server" CssClass="arial_12" Width="250"></asp:label></td>
			</tr>
			</table>
			</asp:Panel>			
			<br>
			<br>
			
			<%--SalesTurnin_Report Changes.Ch1 : END - Added signature to the print version of sales turn in report--%>
			
			
			
			</form>
	</body>
</HTML>
