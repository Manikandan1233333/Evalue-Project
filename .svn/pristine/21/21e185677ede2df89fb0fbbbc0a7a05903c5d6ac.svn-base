<%@ Page language="c#" Codebehind="SalesTurnIn_Report.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.SalesTurnIn_Report" %>
<%--<%@ Register TagPrefix="uc" TagName="Dates" Src="../Controls/Dates.ascx" %>--%>
<!--
 * History:
 * MODIFIED BY COGNIZANT AS PART OF Q4 RETROFIT CHANGES
 * 12/1/2005 Q4-Retrofit.Ch1 : Added an attribute UploadType in tdProdType to assign [Upload Type] Value
 *			 Changed the td(tdBtnHead) Width from "110" to "10" to display proper width when
 *			 there are no void buttons displayed Added four new table rows for displaying Other
			 Product and Total Collection Summary Grids
 * 12/1/2005 Q4-Retrofit.Ch2 : Removed 3 <P>&nbsp;</P> tags
 * 12/16/2005 Q4-Retrofit.Ch3 : Added the code to display the caption as Collection Summary in Sales Turn-in report.
 * 06/07/2007 STAR Retrofit III.Ch1: 
 *			Modified to left align the amount tag in the report header
 *MODIFIED BY COGNIZANT AS A PART OF TIME ZONE ENHANCEMENT ON 8-31-2010.
 *TimeZoneChange.Ch1-Changed the column header Receipt Date to Receipt Date/Time(MST Arizona).
 SalesTurnin_Report Changes.Ch1 : Added ID for policy number as tdpolicynumber
 PC Phase II changes CH1 - Modified the ImageButton "btnVoidReissue" to "btnVoid" and referred a new GIF image. 
 * PC Phase II changes CH2 - Modified the below code to handle Date Time Picker
 * MAIG - CH1 - Modified the Width from 15 to 27
 * MAIG - CH2 - Added the align left property
 * MAIG - CH3 - Added the table and RepID
 * MAIG - CH4 - Added code to align properly that affects IE 11 alone
 * MAIG - CH5 - Modified colspan from 3 to 4 to properly align in Chrome and Firefox 01/12/2014
 * MAIGEnhancement CHG0107527 - CH1 - Added code to set the max length of the textbox from 11 to 19 to support manual date entry
 * CHG0109406 - CH1 - Appended the timezone Arizona along with the Date Label
 * CHG0109406 - CH2 - Added the td which displays the Timezone information 
 * CHG0109406 - CH3 - Modified the Column name from "Receipt Date/Time(MST Arizona)" to Receipt Date/Time(Arizona)
 * CHG0110069 - CH1 - Appended the /Time along with the Date Label
 * CHG0110069 - CH2 - Appended the /Time along with the Date Label
 * CHG0113938 - To display Name instead of Member Name & Policy Number instead of Policy/Mbr Number in the Sales Turn In report Page.
-->
<HTML>
	<HEAD>
		<title>My Turn-In</title>
		<LINK href="../style.css" type="text/css" rel="stylesheet">
			<script language="javascript">
				function confirmTurnIn()
				{
					var returnval;
					returnval=confirm('Are you sure you want to submit for turn-in?');
					return returnval;
				}
				function Open_Report(Type)
				{
				var PrintXlsURL="TurnIn_SalesRep_PrintXls.aspx?type=" +Type;
				window.open(PrintXlsURL,"MyTurnIn");
				return false;
				}
				window.history.forward(1);
			</script>
			<script type="text/javascript" src="../Reports/CalendarControl.js"></script>   
			<!-- MAIG - CH4 - BEGIN - Added code to align properly that affects IE 11 alone -->
								<style type="text/css">
		 @media screen and (min-width:0\0) { 
    #SalesTurnIn_Report {padding-top:20px;}
}
		</style>
		<!-- MAIG - CH4 - END - Added code to align properly that affects IE 11 alone -->
	</HEAD>
	<body>
		<form id="SalesTurnIn_Report" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<!-- space to left of search table-->
					<!-- MAIG - CH1 - BEGIN - Modified the Width from 15 to 27 -->
					<td width="27%">
					<!-- MAIG - CH1 - END - Modified the Width from 15 to 27 -->
						<table width="30">
							<tr>
								<td>&nbsp;</td>
							</tr>
						</table>
					</td>
					<!-- end of space to left of search table-->
					<!-- MAIG - CH2 - BEGIN - Added the align left property -->
					<td align="left">
					<!-- MAIG - CH2 - END - Added the align left property -->
						<table cellSpacing="0" cellPadding="0" width="604" bgColor="#ffffcc" border="0">
							<tr>
								<td class="arial_11_bold_white" width="100%" bgColor="#333333" colSpan="4" height="22">&nbsp;&nbsp;My 
									Turn-In
								</td>
							</tr>
							<tr>
								<td>&nbsp;&nbsp;</td>
								<td class="arial_12_bold" width="40%">Report</td>
								<td class="arial_12_bold" width="30%">User(s)</td>
								<td class="arial_12_bold">Rep DO</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td class="arial_12"><asp:dropdownlist id="cboReportType" runat="server" Width="200" size="1" AutoPostBack="True" onselectedindexchanged="cboReportType_SelectedIndexChanged"></asp:dropdownlist></td>
								<td><asp:label id="lblUser" runat="server" CssClass="arial_11"></asp:label></td>
								<td><asp:label id="lblRepDO" runat="server" CssClass="arial_11"></asp:label></td>
							</tr>
							<%--PC Phase II changes CH2 - Start - Modified the below code to handle Date Time Picker--%>
							<%--<tr>
								<td width="604" colSpan="4">
									<table cellPadding="0" width="100%" align="left" border="0">
										<uc:dates id="Dates" runat="server" visible="false"></uc:dates></table>
								</td>
							</tr>--%>
							<!-- MAIG - CH5 - BEGIN - Modified colspan from 3 to 4 to properly align in Chrome and Firefox 01/12/2014-->
							<td id= "Date" runat="server" Visible="false" class="arial_12_bold" colSpan="4">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<br />
                            <%-- CHG0109406 - CH1 - BEGIN - Appended the timezone Arizona along with the Date Label --%>
                            <%--CHG0110069 - CH1 - BEGIN - Appended the /Time along with the Date Label--%>
&nbsp;&nbsp;&nbsp;Start Date/Time (Arizona)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; End Date/Time (Arizona)
                            <%--CHG0110069 - CH1 - END - Appended the /Time along with the Date Label--%>
                            <%-- CHG0109406 - CH1 - END - Appended the timezone Arizona along with the Date Label --%>
                        </td>
                        <tr>
                        <%--MAIGEnhancement CHG0107527 - CH1 - BEGIN - Added code to set the max length of the textbox from 11 to 19 to support manual date entry --%>
                            <td class="arial_12_bold" colSpan="4">
                                &nbsp;&nbsp;
                                <asp:TextBox ID="TextstartDt" Visible = "false" runat="server" Width="190px" AutoComplete="Off" MaxLength="19"></asp:TextBox>&nbsp;<a
                                    href="javascript:NewCssCal('TextstartDt','MMddyyyy','dropdown',true,'24',true)"><img id= "image" runat = "server" Visible = "false" height="16" alt="Pick a date" src="/PaymentToolimages/cal.gif"
                                        width="16" border="0"></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="TextendDt" runat="server" Visible="false" Width="190px" AutoComplete="Off" MaxLength="19"></asp:TextBox>&nbsp;<a
                                    href="javascript:NewCssCal('TextendDt','MMddyyyy','dropdown',true,'24',true)"><img id="image1" runat= "server" Visible = "false" height="16" alt="Pick a date" src="/PaymentToolimages/cal.gif"
                                        width="16" border="0"></a>
                            </td>
                            <%--MAIGEnhancement CHG0107527 - CH1 - END - Added code to set the max length of the textbox from 11 to 19 to support manual date entry --%>
                            <!-- MAIG - CH5 - END - Modified colspan from 3 to 4 to properly align in Chrome and Firefox 01/12/2014-->
                        </tr>
                        <%--PC Phase II changes CH2- End - Modified the below code to handle Date Time Picker--%>
							<tr>
								<td>&nbsp;</td>
								<td>&nbsp;</td>
								<td colSpan="2">&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td class="arial_12_bold">Status</td>
								<td colSpan="2">&nbsp;&nbsp;</td>
							</tr>
							<tr>
								<td>&nbsp;</td>
								<td class="arial_12" height="16"><asp:dropdownlist id="cboStatus" runat="server" Width="200" size="1" AutoPostBack="True" onselectedindexchanged="cboStatus_SelectedIndexChanged"></asp:dropdownlist></td>
								<td colSpan="2" height="16">&nbsp;&nbsp;</td>
							</tr>
							<tr>
								<td width="604" colSpan="4" height="10"></td>
							</tr>
							<tr>
								<td vAlign="center" align="right" bgColor="#cccccc" colSpan="4" height="17"><A href="SalesTurnIn_Report.aspx" runat="server" ID="A1">
								 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
								 <IMG height="17" src="/PaymentToolimages/btn_reset.gif" width="51" border="0"></A>&nbsp;
									 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
									 <asp:imagebutton id="btnSubmit" runat="server" height="17" width="67px" imageurl="/PaymentToolimages/btn_submit.gif" onclick="btnSubmit_Click"></asp:imagebutton>&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
						</table>
					</td>
					<td width="228">&nbsp;</td>
				</tr>
				<tr>
					<td height="10">&nbsp;</td>
					<td height="10">&nbsp;</td>
					<td height="10">&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<!-- MAIG - CH3 - BEGIN - Added the table and RepID -->
					<td  align="center">
					    <table cellSpacing="0" cellPadding="0" width="604" border="0">
					        <tr>
					            <td>
					                <asp:label class="arial_12_bold_red" id="lblErrMsg" runat="server" Width="604">&nbsp;</asp:label>
					                <asp:label class="arial_12_bold_red" id="lblRepID" runat="server" visible="False">&nbsp;</asp:label>
					            </td>
					        </tr>
					    </table>
					</td>
					<!-- MAIG - CH3 - END - Added the table and RepID -->
					<td height="10">&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td class="arial_11">&nbsp;&nbsp;<asp:label id="lblPageNum1" runat="server"></asp:label>
					<td height="10">&nbsp;</td>
				</tr>
				<tr id="trConvertPrint" runat="server">
					<td height="10">&nbsp;</td>
					<td align="right" bgColor="#cccccc"><A onclick="return Open_Report('XLS')" href="TurnIn_SalesRep_PrintXls.aspx" target="_blank"> <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
					<IMG height="17" src="/PaymentToolimages/btn_convert.gif" width="135" border="0"></A>&nbsp;<A onclick="return Open_Report('PRINT')" href="TurnIn_SalesRep_PrintXls.aspx" target="_blank" runat="server" ID="A2"><IMG height="17" src="/PaymentToolimages/btn_print.gif" width="112" border="0"></A>&nbsp;&nbsp;&nbsp;</td>
					<td height="10">&nbsp;</td>
				</tr>
				<tr>
					<td height="10">&nbsp;</td>
					<td height="10">
						<table id="tblReportInfo" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
						<%--CHG0110069 - CH2 - BEGIN - Appended the /Time along with the Date Label--%>
							<tr>
								<td class="arial_12_bold" width="120">&nbsp;Run Date/Time:</td>
								<td><asp:label id="lblRunDate" runat="server" CssClass="arial_12">&nbsp;</asp:label></td>
							</tr>
							<tr id="trDateRange" runat="server">
								<td class="arial_12_bold">&nbsp;Date/Time Range</td>
								<td><asp:label id="lblDateRange" runat="server" CssClass="arial_12">&nbsp;</asp:label></td>
							</tr>
							<%--CHG0110069 - CH2 - END - Appended the /Time along with the Date Label--%>
							<tr>
								<td class="arial_12_bold">&nbsp;Report Status:</td>
								<td><asp:label id="lblReportStatus" runat="server" CssClass="arial_12">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td class="arial_12_bold" height="15">&nbsp;User(s):</td>
								<td height="15"><asp:label id="lblUsers" runat="server" CssClass="arial_12">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td class="arial_12_bold">&nbsp;Rep DO:</td>
								<td><asp:label id="lblRep" runat="server" CssClass="arial_12">&nbsp;</asp:label></td>
							</tr>
						</table>
					</td>
					<td height="10">&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td width="750" colSpan="2">
						<table id="tblRepeater" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0" runat="server">
							<tr>
								<td>
									<table style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="2" width="100%" bgColor="#cccccc" border="1">
										
										<asp:repeater id="rptSalesTurnInRepeater" runat="server" OnItemDataBound="rptSalesTurnInRepeater_ItemBound">
											<HeaderTemplate>
												<tr class="item_template_header">
												<%-- CHG0109406 - CH2 - BEGIN - Added the td which displays the Timezone information --%>
													<td class="arial_12_bold" colspan="6" id="tdHeader1" style="border-style:none" runat="server">
														Sales Collection Turn-In List</td>
														<td class="arial_12_bold" colspan="5" style="font-style:italic;text-align:right;border-style:none" runat="server">All timezones are in Arizona</td>
												<%-- CHG0109406 - CH2 - END - Added the td which displays the Timezone information --%>
												</tr>
												<!-- STAR Retrofit III.Ch1: START - Modified to left align the amount tag in the report header -->
												<!--TimeZoneChange.Ch1-Changed the column header Receipt Date to Receipt Date/Time(MST Arizona)on 8-31-2010-->
												<tr class="item_template_header_white">
													<td class="arial_12_bold" id="tdChkHead" runat="server" width="36">Select</td>
													<%-- CHG0109406 - CH3 - BEGIN - Modified the Column name from "Receipt Date/Time(MST Arizona)" to Receipt Date/Time(Arizona) --%>
													<td class="arial_12_bold" width="100">Receipt Date/Time(Arizona)</td>
													<%-- CHG0109406 - CH3 - END - Modified the Column name from "Receipt Date/Time(MST Arizona)" to Receipt Date/Time(Arizona) --%>
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
													<td class="arial_12_bold" align="left" width="64">Amount</td>
													<td class="arial_12_bold" id="tdBtnHead" runat="server" width="10">&nbsp;</td>
												</tr>
												<!-- STAR Retrofit III.Ch1: END -->
											</HeaderTemplate>
											<ItemTemplate>
												<tr id="trItemRow" runat="server" class="item_template_alt_row">
													<td class="arial_12" align="center" id="tdChkItem" runat="server">
														<asp:Label ID="lblUploadToPoes" Runat ="server" visible="False" value = '<%# DataBinder.Eval(Container.DataItem, "Upload_to_Poes") %>' >
														</asp:Label>
														<table cellpadding="10">
															<tr>
																<td>
																	<asp:CheckBox ID="chkSelect" Runat="server" Checked="True"></asp:CheckBox>
																</td>
															</tr>
															<tr><td>
																	<!--Added by Cognizant on 05/04/2005 to store the Upload_Type inside a hidden label -->
																	<asp:Label ID="lblUploadType" Runat ="server" visible="False" value = '<%# DataBinder.Eval(Container.DataItem, "Upload Type") %>'></asp:label>
															</td></tr>
														</table>
													</td>
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
													<td class="arial_12" id="tdProdType" runat="server" UploadType = '<%# DataBinder.Eval(Container.DataItem, "Upload Type") %>' ><%# DataBinder.Eval(Container.DataItem, "Product Type") %>
													</td>
													<td class="arial_12" id="tdTransType" runat="server"><%# DataBinder.Eval(Container.DataItem, "Trans Type") %>
													</td>
													<%--SalesTurnin_Report Changes.Ch1 :START - Added ID for policy number as tdpolicynumber--%>
													<td class="arial_12" id="tdpolicynumber" ><%# DataBinder.Eval(Container.DataItem, "Policy Number") %>
													<%--SalesTurnin_Report Changes.Ch1 :END - Added ID for policy number as tdpolicynumber--%>
													</td>
													<td class="arial_12" valign="middle"><%# DataBinder.Eval(Container.DataItem, "Receipt Number") %>
														<asp:Label ID="lblReceiptNo" Runat ="server" visible="False" value = '<%# DataBinder.Eval(Container.DataItem, "Receipt Number") %>' >
														</asp:Label>
													</td>
													<td class="arial_12" id="tdPymtType" runat="server"><%# DataBinder.Eval(Container.DataItem, "Pymt Method") %>
													</td>
													<td class="arial_12" id="tdAmount" runat="server" align="right" amount='<%# DataBinder.Eval(Container.DataItem, "Amount") %>'>
														<%# (Convert.ToDecimal(DataBinder.Eval(Container.DataItem, "Amount"))).ToString("$##0.00") %>
													</td>
													<td class="arial_12" id="tdBtnItem" runat="server">
													<!-- PC Phase II changes CH1 - Start - Modified the ImageButton "btnVoidReissue" to "btnVoid" and referred a new GIF image. -->
														 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
														 <asp:imagebutton id="btnVoid" runat="server" height="17" width="50px" imageurl="/PaymentToolimages/btn_void.gif"></asp:imagebutton>
											        <!-- PC Phase II changes CH1 -End - Modified the ImageButton "btnVoidReissue" to "btnVoid" and referred a new GIF image. -->
													</td>
												</tr>
											</ItemTemplate>
											<FooterTemplate>
												<!-- Modified by Cognizant on 4-20-05. Changed the Caption "Total Turn In" to "Total"  -->
												<tr class="item_template_row">
													<td class="total_cell_arial_12_bold" colspan="9" align="right" id="tdTotalHead" runat="server">TOTAL&nbsp;&nbsp;&nbsp;
													</td>
													<td id="tdTotal" runat="server" class="total_cell_arial_12_bold" align="right">
														<asp:Label ID="lblTotal" Runat="server"></asp:Label>
													</td>
													<td id="tdFooter" runat="server">&nbsp;</td>
												</tr>
												<tr>
													<td id="tdTurnIn" runat="server" vAlign="center" align="right" bgColor="#cccccc" colSpan="11">
														 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
														 <asp:imagebutton id="btnUpdateTotal" CommandName="UpdateTotal" runat="server" imageurl="/PaymentToolimages/btn_update_turnin.gif" width="100px" height="17"></asp:imagebutton>&nbsp;
														 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
														 <asp:imagebutton id="btnSubmitTurnIn" CommandName="SubmitTurnIn" runat="server" imageurl="/PaymentToolimages/btn_submit_turnin.gif" width="150px" height="17"></asp:imagebutton>&nbsp;&nbsp;&nbsp;
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
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
				</tr>
				<!-- 12/16/2005 Q4-Retrofit.Ch3: Start - Added a new table rows for Collection Summary caption-->
				<tr id="trCaption" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td class="arial_13_bold" bgColor="#ffffff">Collection Summary</td>
					<td bgColor="#ffffff">&nbsp;</td>
				</tr>
				<!-- 12/16/2005 Q4-Retrofit.Ch3: End -->
				<tr id="trCollSummarySplit" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
				</tr>
				<tr id="trCollSummary" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td colSpan="2">
						<table id="tblCollSummary" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="5" bgColor="#cccccc" border="1" runat="server">
						</table>
					</td>
				</tr>
				<tr id="trOtherCollSummarySplit" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
				</tr>
				<tr id="trOtherCollSummary" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td colSpan="2">
						<table id="tblOtherCollSummary" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="5" bgColor="#cccccc" border="1" runat="server">
						</table>
					</td>
				</tr>
				<tr id="trTotalCollSummarySplit" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
				</tr>
				<tr id="trTotalCollSummary" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td colSpan="2">
						<table id="tblTotalCollSummary" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="5" width="300" bgColor="#cccccc" border="1" runat="server">
						</table>
					</td>
				</tr>
			</table>
			<!--Repeater for Membership Detail-->
			<table id="tblMemberRepeater" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0" runat="server">
				<tr>
					<td>
						<table style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="2" width="100%" bgColor="#cccccc" border="1">
							<asp:repeater id="rptMembershipDetail" runat="server" OnItemDataBound="rptMembershipDetail_ItemBound">
								<HeaderTemplate>
									<tr class="item_template_header">
										<td class="arial_12_bold" colspan="20" id="tdHeaderMember" runat="server">
											Membership Detail Report</td>
									</tr>
									<!-- STAR Retrofit III.Ch1: START - Modified to left align the amount tag in the report header -->
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
										<td class="arial_12_bold" width="8%" align="left">Amount</td>
										<td class="arial_12_bold" width="7%">Pymt Type</td>
									</tr>
									<!-- STAR Retrofit III.Ch1: END -->
								</HeaderTemplate>
								<ItemTemplate>
									<tr id="trItemRowMember" runat="server" class="item_template_alt_row">
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
			<!--End of membership Repeater-->
			<!--Cross Reference Datagrid-->
			<table id="tblCrossReference" cellSpacing="0" width="100%" runat="server">
				<!--Cross Reference Datagrid Header-->
				<tr>
					<td width="15%">&nbsp;</td>
					<td class="arial_12_bold" vAlign="center" width="604" bgColor="#cccccc" height="17">Cross-Reference 
						Report</td>
					<td bgColor="#ffffff"></td>
				</tr>
				<!--End of Cross Reference Datagrid Header-->
				<tr>
					<td>&nbsp;</td>
					<td width="604"><asp:datagrid id="dgCrossReference" runat="server" OnPageIndexChanged="CrossReferencePageIndexChanged" pagesize="25" allowpaging="true" pagerstyle-prevpagetext="PREVIOUS |" pagerstyle-nextpagetext="NEXT" pagerstyle-mode="NextPrev" pagerstyle-position="TopAndBottom" pagerstyle-horizontalalign="Right" borderstyle="None" cssclass="arial_12" CellPadding="2" OnItemDataBound="dgCrossReference_ItemBound">
							<HeaderStyle Height="22px" CssClass="arial_11_bold" BackColor="White"></HeaderStyle>
							<PagerStyle NextPageText="NEXT" PrevPageText="PREVIOUS |" HorizontalAlign="Right" Position="TopAndBottom"></PagerStyle>
						</asp:datagrid></td>
					<td></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td class="arial_11">&nbsp;&nbsp;<asp:label id="lblPageNum2" runat="server"></asp:label></td>
				</tr>
			</table>
			<!--End of Cross Reference Datagrid-->
			</form>
		
	</body>
</HTML>
