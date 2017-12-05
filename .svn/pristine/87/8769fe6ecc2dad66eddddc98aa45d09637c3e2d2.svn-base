<!-- MAIG - CH1 - Modified verbiage to remove the Sales & Service -->
<!-- MAIG - CH2 - Added code to align properly that affects IE 11 alone-->
<%--MAIGEnhancement CHG0107527 - CH1 - Added code to set the max length of the textbox from 11 to 19 to support manual date entry --%>
<%-- CHG0109406 - CH1 - Appended the timezone Arizona along with the Date Label --%>
<!-- CHG0109406 - CH2 - Added a label lblHeaderTimeZone to display the timezone for the results displayed  -->
<%--CHG0110069 - CH1 - Appended the /Time along with the Date Label--%>
<%@ Register TagPrefix="uc" TagName="RevenueProduct" Src="../Controls/RevenueProduct.ascx" %>
<%@ Register TagPrefix="uc" TagName="Dates" Src="../Controls/Dates.ascx" %>
<%@ Register TagPrefix="uc" TagName="PaymentType" Src="../Controls/PaymentTypeControl.ascx" %>
<%@ Page language="c#" Codebehind="ins_usr_reports.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.ins_usr_reports" %>
  <!--PC Phase II changes CH1 - Added the below code to include the status Drop down box in UI -->
  <%--PC Phase II changes CH2 - Added the below code to Handle Date Time Picker --%>
<HTML>
	<HEAD>
		<!-- MAIG - CH1 - BEGIN - Modified verbiage to remove the Sales & Service -->
		<title>Payment Tool</title>
		<!-- MAIG - CH1 - END - Modified verbiage to remove the Sales & Service -->		
		<script type="text/javascript" src="../Reports/CalendarControl.js"></script>
		<!-- MAIG - CH2 - BEGIN - Added code to align properly that affects IE 11 alone-->
		<style type="text/css">
		        @media screen and (min-width:0\0) { 
    #frmInsReports {padding-top:20px;}
}
		</style>
		<!-- MAIG - CH2 - END - Added code to align properly that affects IE 11 alone-->
	    </HEAD>
	<body>
		<!-- select a report table contains 3 tables: space:30, search table:604, space:116 -->
		<form id="frmInsReports" name="form1" runat="server">
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<!-- space to left of search table-->
					<td width="30">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td width="30"></td>
							</tr>
						</table>
					</td>
					<!-- select a report table -->
					<td>
						<table cellSpacing="0" cellPadding="0" width="604" bgColor="#ffffcc" border="0">
							<tbody>
								<tr>
									<td class="arial_11_bold_white" bgColor="#333333" colSpan="4" height="22">&nbsp;&nbsp;My 
										Insurance Reports</td>
								</tr>
								<tr>
									<td colSpan="4" height="10"></td>
								</tr>
								<tr>
									<td class="arial_12" colSpan="4" height="22">&nbsp;&nbsp;Select a report to view 
										using the pull down menus below</td>
								</tr>
								<tr>
									<td colSpan="4" height="10"></td>
								</tr>
								<tr>
									
									<td class="arial_12_bold">&nbsp;&nbsp;&nbsp; Report Type</td>
									<td colSpan="2"></td>
								</tr>
								<tr>
									
									<td class="arial_12_bold" colSpan="3">&nbsp;&nbsp;&nbsp; <asp:dropdownlist id="report_type" runat="server" size="1">
											<asp:listitem value="2" selected="True">Insurance Transaction Detail</asp:listitem>
										</asp:dropdownlist>
                                        <br />
                                    </td>
								</tr>
								<td class="arial_12_bold" colSpan="4">
                              &nbsp;&nbsp;
                              <%-- CHG0109406 - CH1 - BEGIN - Appended the timezone Arizona along with the Date Label --%>
                              <%--CHG0110069 - CH1 - BEGIN - Appended the /Time along with the Date Label--%>
                                    <br />
                    &nbsp;&nbsp; Start Date/Time (Arizona)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; End Date/Time (Arizona)                         </td>
                                <%--CHG0110069 - CH1 - END - Appended the /Time along with the Date Label--%>
                               <%-- CHG0109406 - CH1 - END - Appended the timezone Arizona along with the Date Label --%>
                        <tr>
                        <%--MAIGEnhancement CHG0107527 - CH1 - BEGIN - Added code to set the max length of the textbox from 11 to 19 to support manual date entry --%>
                            <td class="arial_12_bold" colSpan="4">&nbsp;&nbsp; <asp:TextBox ID="TextstartDate" runat="server" Width="200px" AutoComplete="Off" MaxLength="19"></asp:TextBox>&nbsp;<a
                                    href="javascript:NewCssCal('TextstartDate','MMddyyyy','dropdown',true,'24',true)"><img height="16" alt="Pick a date" src="/PaymentToolimages/cal.gif"
                                        width="16" border="0"></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:TextBox ID="TextendDate" runat="server" Width="200px" AutoComplete="Off" MaxLength="19"></asp:TextBox>&nbsp;<a
                                    href="javascript:NewCssCal('TextendDate','MMddyyyy','dropdown',true,'24',true)"><img height="16" alt="Pick a date" src="/PaymentToolimages/cal.gif"
                                        width="16" border="0"></a>
                            </td>
                            <%--MAIGEnhancement CHG0107527 - CH1 - END - Added code to set the max length of the textbox from 11 to 19 to support manual date entry --%>
                        </tr>								
								<!--- Added by CTS -->								
								<tr>
                            <td colspan="3" width="100%">
                               <table cellspacing="0" align="left">
								<uc:PaymentType id="PaymentType" runat="server"></uc:PaymentType>
                                </table>
                            </td>
                           </tr>                       
								<%--<uc:dates id="Dates" runat="server"></uc:dates>--%>
								<tr>
                            <td colspan="3" width="100%">
                                <table cellspacing="0" align="left">
								<uc:revenueproduct id="RevenueProduct" runat="server" />
                                </table>
                            </td>
                           </tr>
								<tr>
									<td colSpan="4" height="10">
									</td>
								
									</tr>
									<tr><td class="arial_12_bold">&nbsp;&nbsp;&nbsp; Status</td></tr>
								<tr colspan="5">									
									<td>
									
									&nbsp;&nbsp;
									
									<asp:DropDownList ID="Status" runat="server" size="1" AutoPostBack="True"
                                    Height="16px" Width="206px">
                                    <asp:ListItem Value="-1">All</asp:ListItem>
                                    <asp:ListItem Value="1">New</asp:ListItem>
                                    <asp:ListItem Value="3">Pending Approval</asp:ListItem>
                                    <asp:ListItem Value="7">Approved</asp:ListItem>
                                    <asp:ListItem Value="12">Manually Voided</asp:ListItem>
                                    <asp:ListItem Value="13">Refunded </asp:ListItem>
                                    <asp:ListItem Value="6">Rejected</asp:ListItem>
                                    <asp:ListItem Value="4">Void</asp:ListItem>
                                </asp:DropDownList>
                                  <!--PC Phase II changes CH1 -END- Added the below code to include the status Drop down box in UI -->
                                
                                </td>
								</tr>
								<tr>
									  <!--PC Phase II changes CH1 -START- Added the below code to include the status Drop down box in UI -->
									
									<td class="arial_12_bold">&nbsp;&nbsp;&nbsp; 
                                        <br />
&nbsp;&nbsp;&nbsp;&nbsp; User Name</td>
									
								</tr>
								
								<tr>
									<td colSpan="4" height="10">&nbsp;&nbsp;&nbsp; <asp:listbox id="UserList" runat="server" cssclass="arial_12" selectionmode="Multiple" rows="1"></asp:listbox></td>
								</tr>
								<tr>
									<td bgColor="#cccccc" colSpan="4" height="3"></td>
								</tr>
								<tr>
									<td vAlign="center" align="right" bgColor="#cccccc" colSpan="4" height="17"><A id="A1" href="ins_usr_reports.aspx" runat="server">
									 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
									 <IMG height="17" src="/PaymentToolimages/btn_reset.gif" width="51" border="0"></A>&nbsp;
										 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach javascript:ValidateDateDifference(); return false; -->
										 <asp:imagebutton id="btnSubmit" runat="server" height="17" width="67px" imageurl="/PaymentToolimages/btn_submit.gif" onclick="btnSubmit_Click"></asp:imagebutton>&nbsp;
									</td>
								</tr>
							</tbody></table>
					</td>
					<!-- space to right of select a report table -->
					<td>
						<table cellSpacing="0" cellPadding="0" width="116">
							<tr>
								<td width="116"></td>
							</tr>
						</table>
					</td>
					<!-- end of select a report table --></tr>
			</table>
			
			<!-- CSR renewal summary report table has 2 td's 30 & 685. 685 contains table with results -->
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td colSpan="2" height="15"></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td class="arial_12_bold_red">&nbsp;&nbsp;<asp:label id="lblErrMsg" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td class="arial_11">&nbsp;&nbsp;<asp:label id="lblPageNum1" runat="server"></asp:label>
					</td>
				</tr>
			</table>
			<!-- 5 td's: 30, 604 (made up of 250, 50, 304), 116 --><asp:panel id="pnlTitle" runat="server">
				<TABLE cellSpacing="0" cellPadding="0" width="750" border="0">
					<TR>
						<TD width="30"></TD>
						<TD width="604" bgColor="#cccccc" colSpan="2" height="3"></TD>
						<TD width="116"></TD>
					</TR>
					<TR>
						<TD width="30"></TD>
						<TD class="arial_12_bold" width="250" bgColor="#cccccc" height="22">&nbsp;&nbsp;
							<asp:label id="lblReportTitle" runat="server"></asp:label></TD>
						<TD align="right" width="354" bgColor="#cccccc"><A id="A2" href="ins_PrintXls.aspx?type=PRINT" target="_blank" runat="server">
						 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
						 <IMG height="17" src="/PaymentToolimages/btn_print.gif" width="112" border="0"></A>&nbsp;<A id="A3" href="ins_PrintXls.aspx?type=XLS" target="_blank" runat="server"><IMG height="17" src="/PaymentToolimages/btn_convert.gif" width="135" border="0"></A>&nbsp;</TD>
						<TD width="116"></TD>
					</TR> <!--<TR>
							<TD width="30"></TD>
							<TD width="604" bgColor="#ffffff" colSpan="2" height="5"></TD>
							<TD width="116"></TD>
						</TR>--></TABLE> <!-- 5 td's: 30, 604 (made up of 150, 150, 304), 116 -->
				<TABLE cellSpacing="0" cellPadding="0" width="750" border="0">
					<TR>
						<TD width="30"></TD>
						<TD width="1"></TD>
						<TD class="arial_12_bold" vAlign="top" width="149" bgColor="#ffffff">&nbsp;&nbsp;
							<asp:label id="lblDate" runat="server"></asp:label><BR>
							&nbsp;&nbsp;
							<asp:label id="lblDates" runat="server"></asp:label><BR>
							&nbsp;&nbsp; <!--- Added by CTS -->
							<asp:label id="lblPayment" runat="server"></asp:label><BR>
							&nbsp;&nbsp; 
							<!--- Added by CTS -->
							<asp:label id="lblProduct" runat="server"></asp:label><BR>
							&nbsp;&nbsp;
							<asp:label id="lblRevenue" runat="server"></asp:label><BR>
							&nbsp;&nbsp;
							<asp:label id="lblApp" runat="server"></asp:label><BR>
							&nbsp;&nbsp;
							<asp:label id="lblCsr" runat="server"></asp:label><br />
							&nbsp;&nbsp;
							<asp:label id="lblStatus" runat="server"></asp:label></TD>
						<TD class="arial_12" vAlign="top" width="404" bgColor="#ffffff" height="22">
							<asp:label id="lblRunDate" runat="server"></asp:label><BR>
							<asp:label id="lblDateRange" runat="server"></asp:label><BR> <!--- Added by CTS -->
							<asp:label id="lblPaymentName" runat="server"></asp:label><BR> <!--- Added by CTS -->
							<asp:label id="lblProductName" runat="server"></asp:label><BR>
							<asp:label id="lblRevenueName" runat="server"></asp:label><BR>
							<asp:label id="lblAppName" runat="server"></asp:label><BR>
							<asp:label id="lblCsrs" runat="server"></asp:label><BR>
							<asp:label id="lblStatusName" runat="server"></asp:label></TD>
						<TD width="49"></TD>
						<TD width="1"></TD> <!--<TD><IMG height="50" src="/PaymentToolimages/vert_rule_grey.gif" width="1"></TD>-->
						<TD width="116"></TD>
					</TR>
					<TR>
						<TD colSpan="7" height="5"></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!-- results table: 30, 604, 116 -->
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<!-- this row's middle TD will contain a table whose sole purpose is a 1 pixel gray border.
						 Inside is the results table -->
				<tbody>
					<tr>
						<td width="25"></td>
						<td width="604">
							<!-- outer border table -->
							<table borderColor="#cccccc" cellSpacing="0" cellPadding="0" rules="none" width="604" border="0">
								<tr>
									<td>
										<!--										<!-- results --
										<table cellspacing="0" cellpadding="0" width="604" border="0">
										  <tr>
										    <td>-->
										 <!-- CHG0109406 - CH2 - BEGIN - Added a label lblHeaderTimeZone to display the timezone for the results displayed  -->
										    <table runat="server" width="100%">
										    <tr>
										    <td width="20%"><asp:label id="lblCyber" runat="server" cssclass="arial_11_bold" visible="False" text="In CyberSource: Not in APDS"></asp:label></td>
										    <td width="80%" align="right"><asp:label id="lblHeaderTimeZone"  text="All timezones are in Arizona" style="font-style:italic;text-align:right;" cssclass="arial_11_bold" visible="false" runat="server"></asp:label></td>
										    </tr>
										    </table>
										
										    <!-- CHG0109406 - CH2 - END - Added a label lblHeaderTimeZone to display the timezone for the results displayed  -->
										    <asp:GridView ID="dgReport1" OnRowDataBound="ItemDataBound1" runat="server" CssClass="arial_12" PageSize="25" width="604px"
                                            AllowPaging="true" OnPageIndexChanging="PageIndexChanged1" 
                                            PagerSettings-Mode="NextPrevious" PagerSettings-Position="TopAndBottom" 
                                            BorderStyle="none" CellPadding="2" CellSpacing="0">
										    <AlternatingRowStyle BackColor="LightYellow" />
										    <RowStyle CssClass="arial_12" />
										    <HeaderStyle CssClass="arial_12_bold" BackColor="#CCCCCC" HorizontalAlign="Left" />
                                            <PagerTemplate>
                                            <table id="tbPager" width="100%" style="background-color: #eeeeee; color: Blue; font-size: 7pt;
                                                border: none;">
                                                <tr>
                                                    <td align="right">
                                                        <asp:LinkButton runat="server" ID="vPrev" CommandName="Page" CommandArgument="Prev"
                                                            Text="PREVIOUS" />
                                                        <asp:LinkButton runat="server" ID="seperator" CommandName="Page" CommandArgument="Prev"
                                                            Text="|" Enabled="false" />
                                                        <asp:LinkButton runat="server" ID="vNext" CommandName="Page" CommandArgument="Next"
                                                            Text="NEXT" />
                                                    </td>
                                                </tr>
                                            </table>
                                            </PagerTemplate>
                                            </asp:GridView>
										<br>
										<asp:label id="lblAPDS" runat="server" cssclass="arial_11_bold" visible="False" text="In APDS: Not in CyberSource"></asp:label>
										<asp:GridView ID="dgReport2" OnRowDataBound="ItemDataBound2" runat="server" CssClass="arial_12" PageSize="25" 
                                            AllowPaging="true" OnPageIndexChanging="PageIndexChanged2" 
                                            PagerSettings-Mode="NextPrevious" PagerSettings-Position="TopAndBottom" 
                                            BorderStyle="none" CellPadding="2" CellSpacing="0">
										    <AlternatingRowStyle BackColor="LightYellow" />
										    <RowStyle CssClass="arial_12" />
										    <HeaderStyle CssClass="arial_12_bold" BackColor="#CCCCCC" HorizontalAlign="Left" />
                                            <PagerTemplate>
                                            <table id="tbPager" width="100%" style="background-color: #eeeeee; color: Blue; font-size: 7pt;
                                                border: none;">
                                                <tr>
                                                    <td align="right">
                                                        <asp:LinkButton runat="server" ID="vPrev1" CommandName="Page" CommandArgument="Prev"
                                                            Text="PREVIOUS" />
                                                        <asp:LinkButton runat="server" ID="seperator1" CommandName="Page" CommandArgument="Prev"
                                                            Text="|" Enabled="false" />
                                                        <asp:LinkButton runat="server" ID="vNext1" CommandName="Page" CommandArgument="Next"
                                                            Text="NEXT" />
                                                    </td>
                                                </tr>
                                            </table>
                                            </PagerTemplate>
                                            </asp:GridView>
										<!--												</td>
											</tr>
										</table>
										<!-- end of results --></td>
									<td width="116"></td>
								</tr>
							</table>
		</form>
		<!-- end of outer border table --> </TD></TR></TBODY></TABLE>
		<table cellSpacing="0" cellPadding="0" width="750" border="0">
			<tr>
				<td width="30"></td>
				<td class="arial_11">&nbsp;&nbsp;<asp:label id="lblPageNum2" runat="server"></asp:label></td>
				<td class="arial_10_blue" align="right">&nbsp;&nbsp;</td>
			</tr>
		</table>
		<!-- end of CSR renewal summary table -->
	</body>
</HTML>
