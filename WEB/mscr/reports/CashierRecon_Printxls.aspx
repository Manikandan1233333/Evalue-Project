<%@ Page language="c#" Codebehind="CashierRecon_Printxls.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.CashierRecon_Printxls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<Title>Cashier Reconciliation PrintXls</Title> <!--

 * HISTORY

 * MODIFIED BY COGNIZANT AS PART OF Q4 RETROFIT CHANGES

 * 12/1/2005 Q4-Retrofit.ch1 : Changed the Attribute name from "Enabled" to "UploadType" 

 *                                    in td tdProdType for Naming Convention Added the Attribute Height to TD<Title>

 *                                    Added the Attribute Id and Runat to the td Policy Number 

 *                                    and Removed the appended Single Quote in the HTML
 * STAR Retrofit III Changes:
 * 3/27/2007 STAR Retrofit III.Ch1 : 
			Added caption for cashier reconcilation summary	
 * 06/07/2007 STAR Retrofit III.Ch2 :
            Modified to left align the amount tag in the report header	
 * 08/18/2010 Crossreference report Ch1:
 *          Modified by cognizant replced data grid t gridview as a part of tech up grade.
 * MODIFIED BY COGNIZANT AS A PART OF TIME ZONE ENHANCEMENT ON 8-31-2010.
 * TimeZoneChange.Ch1-Changed the column header Receipt Date to Receipt Date/Time(MST Arizona).
 * CHG0109406 - CH1 - Added the td which displays the Timezone information
 * CHG0109406 - CH2 - Modified the Column name from "Receipt Date/Time(MST Arizona)" to Receipt Date/Time(Arizona)
 * CHG0109406 - CH3 - Modified the Column name from "Approved Date/Time(MST Arizona)" to Approved Date/Time(Arizona)
 * CHG0110069 - CH1 - Appended the /Time along with the Date Label
* CHG0113938 - To display Name instead of Member Name & Policy Number instead of Policy/Mbr Number in the Cashier Reconciliation Exported Data.
--><LINK href="../Style.css" type="text/css" rel="stylesheet">
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
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="CashierRecon_Printxls" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="606">
				<tr>
					<td class="arial_12_bold" align="left"><IMG src="../images/msc_logo_sm.gif" border="0"></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
			</table>
			<table id="tblGeneral" cellSpacing="0" cellPadding="0" width="606" runat="server">
				<tr>
					<td bgColor="#333333" colSpan="4" height="20"><b><asp:label id="lblTitle" runat="server" CssClass="arial_12_bold_white"></asp:label></b></td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
				<%--CHG0110069 - CH1 - BEGIN - Appended the /Time along with the Date Label--%>
				<tr>
					<td class="arial_12_bold">Run Date/Time:</td>
					<td align="left" colSpan="3"><asp:label id="lblRunDate" runat="server" CssClass="arial_12"></asp:label></td>
				</tr>
				<tr id="trDateRange" runat="server">
					<td class="arial_12_bold">Date/Time Range:</td>
					<td align="left" colSpan="3"><asp:label id="lblDateRange" runat="server" CssClass="arial_12"></asp:label></td>
				</tr>
				<%--CHG0110069 - CH1 - END - Appended the /Time along with the Date Label--%>
				<tr>
					<td class="arial_12_bold">Report Status:</td>
					<td colSpan="3"><asp:label id="lblReportStatus" runat="server" CssClass="arial_12"></asp:label></td>
				</tr>
				<tr>
					<td class="arial_12_bold">Created By:</td>
					<td colSpan="3"><asp:label id="lblUsers" runat="server" CssClass="arial_12"></asp:label></td>
				</tr>
				<tr>
					<td class="arial_12_bold">Rep DO:</td>
					<td colSpan="3"><asp:label id="lblRepDO" runat="server" CssClass="arial_12"></asp:label></td>
				</tr>
				<tr id="trApprover" runat="server">
					<td class="arial_12_bold" width="120">Approved By:</td>
					<td><asp:label id="lblApprover" runat="server" CssClass="arial_12"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="4">&nbsp;</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="606">
				<tr>
					<td colSpan="3">
						<table id="tblRepeater" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0" runat="server">
							<tr>
								<td>
									<table style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="2" width="100%" border="1">
										<asp:repeater id="rptCashierApproveRepeater" runat="server" OnItemDataBound="rptCashierApproveRepeater_ItemBound">
											<HeaderTemplate>
												<tr class="item_template_header" bgcolor="#cccccc">
												<%-- CHG0109406 - CH1 - BEGIN - Added the td which displays the Timezone information --%>
													<td height="20" class="arial_12_bold" colspan="6" style="border-style:none" id="tdHeader1" runat="server">
														Cashier's Reconciliation Reject or Approve/Upload</td>
														<td class="arial_12_bold" colspan="5" style="font-style:italic;text-align:right;border-style:none" runat="server">All timezones are in Arizona</td>
												<%-- CHG0109406 - CH1 - END - Added the td which displays the Timezone information --%>
												</tr>
												<!-- STAR Retrofit III.Ch2: START - Modified to left align the amount tag in the report header -->
												<!--TimeZoneChange.Ch1-Changed the column header Receipt Date to Receipt Date/Time(MST Arizona)and Approved Date to Approved Date/Time(MST Arizona)on 8-31-2010-->
												<tr id="trHeaderRow" runat="server" class="arial_12_bold">
												    <%-- CHG0109406 - CH2 - BEGIN - Modified the Column name from "Receipt Date/Time(MST Arizona)" to Receipt Date/Time(Arizona) --%>
													<td width="100">Receipt Date/Time(Arizona)</td>
													<%-- CHG0109406 - CH2 - END - Modified the Column name from "Receipt Date/Time(MST Arizona)" to Receipt Date/Time(Arizona) --%>
													<td width="60">Status</td>
                                                    <%-- CHG0113938 - BEGIN - Modified the Column name from "Member Name" to "Name" --%>
													<td width="80">Name</td>
                                                    <%-- CHG0113938 - End - Modified the Column name from "Member Name" to "Name" --%>
													<td width="80">Product Type</td>
													<td width="60">Trans Type</td>
                                                    <%-- CHG0113938 - BEGIN - Modified the Column name from "Policy/Mbr Number" to "Policy Number" --%>
													<td width="50">Policy Number</td>
                                                    <%-- CHG0113938 - END - Modified the Column name from "Policy/Mbr Number" to "Policy Number" --%>
													<td width="70">Receipt Number</td>
													<td width="40">Pymt Method</td>
													<td width="80" id="tdApprovedByHead" runat="server">Approved By</td>
													<%-- CHG0109406 - CH3 - BEGIN - Modified the Column name from "Approved Date/Time(MST Arizona)" to Approved Date/Time(Arizona) --%>
													<td width="40" id="tdApprovedDateHead" runat="server">Approved Date/Time(Arizona)</td>
													<td align="left">Amount</td>
												</tr>
												<!-- STAR Retrofit III.Ch2: END -->
											</HeaderTemplate>
											<ItemTemplate>
												<tr id="trItemRow" runat="server" class="arial_12">
													<td>
														<asp:Label ID="lblReceiptDate" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Receipt Date") %>' >
														</asp:Label>&nbsp;
													</td>
													<td>
														<asp:Label ID="lblStatus" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Status") %>' >
														</asp:Label>&nbsp;
													</td>
													<td>
														<asp:Label ID="lblMemberName" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Member Name") %>' >
														</asp:Label>&nbsp;
													</td>
													<td id="tdProdType" runat="server" UploadType = '<%# DataBinder.Eval(Container.DataItem, "Upload Type") %>' ><%# DataBinder.Eval(Container.DataItem, "Product Type") %>
													</td>
													<td id="tdTransType" runat="server"><%# DataBinder.Eval(Container.DataItem, "Trans Type") %>
													</td>
													<td id="tdPolicyNumber" runat="server"><%# DataBinder.Eval(Container.DataItem, "Policy Number") %>
													</td>
													<td valign="middle">
														<asp:Label ID="lblReceiptNo" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Receipt Number") %>' value = '<%# DataBinder.Eval(Container.DataItem, "Receipt Number") %>' >
														</asp:Label>
													</td>
													<td id="tdPymtType" runat="server"><%# DataBinder.Eval(Container.DataItem, "Pymt Method") %>
													</td>
													<td id="tdApprovedByItem" runat="server">
														<asp:Label ID="lblApprovedBy" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Approved By") %>' >
														</asp:Label>&nbsp;
													</td>
													<td id="tdApprovedDateItem" runat="server">
														<asp:Label ID="lblApprovedDate" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Approved Date") %>' >
														</asp:Label>&nbsp;
													</td>
													<td align="right" id="tdAmount" runat="server"><%# DataBinder.Eval(Container.DataItem, "Amount","{0:c}") %>
													</td>
												</tr>
											</ItemTemplate>
											<FooterTemplate>
												<tr class="item_template_row">
													<td class="total_cell_arial_12_bold" colspan="10" align="right" id="tdTotalHead" runat="server">TOTAL 
														&nbsp;&nbsp;&nbsp;
													</td>
													<td id="tdTotal" runat="server" class="total_cell_arial_12_bold" align="right">
														<asp:Label ID="lblTotal" Runat="server"></asp:Label>
													</td>
												</tr>
												<tr bgColor="#cccccc" height="20">
													<td id="tdFooter" runat="server" colspan="11"></td>
												</tr>
											</FooterTemplate>
										</asp:repeater></table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table id="tblCollectionSummary" style="WIDTH: 591px; HEIGHT: 140px" width="591" runat="server">
				<!-- STAR Retrofit III.Ch1: START - Added caption for cashier reconcilation summary and height of "trCollSummarySplit" row is removed -->
				<tr id="trBlankRow1" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
				</tr>
				<tr id="trCaption" runat="server">
					<td class="arial_13_bold" width="100%" bgColor="#ffffff" colSpan="3">Cashiering 
						Summary Report(Pending Approval)</td>
				</tr>
				<tr id="trCollSummarySplit" runat="server">
					<td colSpan="3"></td>
				</tr>
				<!-- STAR Retrofit III.Ch1: END -->
				<tr id="trCollSummary" runat="server">
					<td colSpan="3">
						<table id="tblCollSummary" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="5" width="600" bgColor="#cccccc" border="1" runat="server" FixedWidth="600">
						</table>
					</td>
				</tr>
				<tr id="trOtherCollSummarySplit" runat="server">
					<td colSpan="3" height="10"></td>
				</tr>
				<tr id="trOtherCollSummary" runat="server">
					<td colSpan="3">
						<table id="tblOtherCollSummary" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="5" width="600" bgColor="#cccccc" border="1" runat="server" FixedWidth="600">
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="3" height="10"></td>
				</tr>
				<tr>
					<td colSpan="3">
						<table id="tblTotalCollSummary" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="5" width="300" bgColor="#cccccc" border="1" runat="server">
						</table>
					</td>
				</tr>
			</table>
			<table id="tblMemberRepeater" cellSpacing="0" cellPadding="0" width="606" bgColor="#ffffff" border="0" runat="server">
				<tr>
					<td>
						<table style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="2" width="606" border="1">
							<asp:repeater id="rptMembershipDetail" runat="server" OnItemDataBound="rptMembershipDetail_ItemBound">
								<HeaderTemplate>
									<tr class="item_template_header">
										<td bgcolor="#cccccc" class="arial_12_bold" colspan="22" ID="Td1" runat="server">
											Membership Detail Report</td>
									</tr>
									<!-- STAR Retrofit III.Ch2: START - Modified to left align the amount tag in the report header -->
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
										<td class="arial_12_bold" width="80" id="tdMbrApprovedByHead" runat="server">Approved 
											By</td>
										<td class="arial_12_bold" width="40" id="tdMbrApprovedDateHead" runat="server">Approved 
											Date/Time(MST Arizona)</td>
										<td class="arial_12_bold" align="left">Amount</td>
										<td class="arial_12_bold" width="7%">Pymt Type</td>
									</tr>
									<!-- STAR Retrofit III.Ch2: END -->
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
										<td class="arial_12" id="tdMbrApprovedByItem" runat="server">
											<asp:Label ID="lblMbrApprovedBy" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Approved By") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12" id="tdMbrApprovedDateItem" runat="server">
											<asp:Label ID="lblMbrApprovedDate" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Approved Date") %>' >
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
										<td class="total_cell_arial_12_bold" colspan="20" align="right" id="tdTotalMemberHead" runat="server">TOTAL 
											&nbsp;&nbsp;&nbsp;
										</td>
										<td id="tdTotalMember" runat="server" class="total_cell_arial_12_bold" align="right">
											<asp:Label ID="lblTotalMember" Runat="server"></asp:Label>
										</td>
										<td>&nbsp;</td>
									</tr>
									<tr>
										<td id="tdTotalFooter" runat="server" bgColor="#cccccc" colSpan="22" height="17">
										</td>
									</tr>
								</FooterTemplate>
							</asp:repeater></table>
					</td>
				</tr>
			</table>
			<table id="tblCrossReference" cellSpacing="0" cellPadding="0" width="604" bgColor="#ffffff" border="0" runat="server">
				<tr>
					<td class="arial_12_bold" bgColor="#cccccc">Cross Reference Report</td>
				</tr>
				<tr>
					<td>
					<asp:GridView ID="dgCrossReference" runat="server" 
                            onrowdatabound="dgCrossReference_RowDataBound" CssClass="arial_11">
                        <AlternatingRowStyle BackColor="LightYellow" />
					</asp:GridView>
					</td>
					</tr>
                   <%-- <%# DataBinder.Eval(Container.DataItem, "Product Type") %>--%>
				
			</table>
		</form>
	</body>
</HTML>
