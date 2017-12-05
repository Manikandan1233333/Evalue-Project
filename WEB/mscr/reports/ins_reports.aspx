<!--
MAIG - CH1 - Added code to register the Location,User and Agency User controls
MAIG - CH2 - Modified the verbiage to remove Sales & Service
MAIG - CH3 - Modified the code to remove the PaymentType User control
MAIG - CH4 - Modified the code to  include the id and runat attributes
MAIG - CH5 - Modified the code to  include the id and runat attributes
MAIG - CH6 - Added code to display the Activity Date field for Settlement Report
MAIG - CH7 - Added code to create a panel and added the Payment Type User control
MAIG - CH8 - Added code to remove the Height attribute which resolved the display issue in Chrome & Firefox
MAIG - CH9 - Added code to add panel for displaying the Location, User and Agency Criteria's
MAIG - CH10 - Added code to reset the Error message in Click
MAIG - CH11 - Modified the MaxLength property from 11 to 19 to support IE 11 and editable
MAIG - CH12 - Modified the MaxLength property from 11 to 19 to support IE 11 and editable
-->
<%--CHG0109406 - CH1 - Appended the timezone Arizona along with the Date Label--%>
<%--CHG0109406 - CH2 - Appended the timezone Arizona along with the Date Label--%>
<%--CHG0109406 - CH3 - Added a label lblHeaderTimeZone to display the timezone for the results displayed --%>
<%-- CHG0110069 - CH1 - Appended the /Time along with the Date Label --%>
<%-- CHG0110069 - CH2 - Appended the /Time along with the Date Label --%>
<%@ Register TagPrefix="uc" TagName="Users" Src="../Controls/Users.ascx" %>
<%@ Register TagPrefix="uc" TagName="PaymentType" Src="../Controls/PaymentTypeControl.ascx" %>
<%@ Register TagPrefix="uc" TagName="RevenueProduct" Src="../Controls/RevenueProduct.ascx" %>
<%@ Register TagPrefix="uc" TagName="Dates" Src="../Controls/Dates.ascx" %>
<%@ Register TagPrefix="uc" TagName="ReportType" Src="../Controls/ReportType.ascx" %>
<!-- MAIG - CH1 - BEGIN - Added code to register the Location,User and Agency User controls -->
<%@ Register TagPrefix="uc" TagName="LocType" Src="../Controls/Locations.ascx" %>
<%@ Register TagPrefix="uc" TagName="AgencyType" Src="../Controls/Agency.ascx" %>
<%@ Register TagPrefix="uc" TagName="User" Src="../Controls/User.ascx" %>
<!-- MAIG - CH1 - END - Added code to register the Location,User and Agency User controls -->
<%@ Page Language="c#" CodeBehind="ins_reports.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.ins_reports" %>
<!--PC Phase II changes CH1 -START- Added the below code to include the status Drop down box in UI -->
<html>
<head>
		<!-- MAIG - CH2 - BEGIN - Modified the verbiage to remove Sales & Service -->
		<title>Payment Tool</title> 
		<!-- MAIG - CH2 - END - Modified the verbiage to remove Sales & Service -->
		<!--
Modified as part of STAR Auto 2.1 on 08/14/2007:
STAR Auto 2.1.Ch1: Added label RepDO in the header of the result grid
PC Phase II changes CH1 - Added the below code for handling Insurance Reports 
-->
	    <style type="text/css">
            .arial_9
            {
                margin-left: 0px;
            }
            .style1
            {
                height: 14px;
            }
        </style>
    <script type="text/javascript" src="../Reports/CalendarControl.js"></script>  
	</head>
	<body>
		<table cellspacing="0" cellpadding="0" width="750" border="0">
			<tr>
				<td colspan="2" height="10"></td>
			</tr>
			<tr>
				<td colspan="2" height="10"></td>
			</tr>
		</table>
		<!-- select a report table contains 3 tables: space:30, search table:604, space:116 -->
		<form id="frmInsReports" name="form1" runat="server">
			<table cellspacing="0" cellpadding="0" width="750" border="0">
				<tr>
					<!-- space to left of search table-->
					<td width="30">
						<table cellspacing="0" cellpadding="0" border="0">
							<tr>
								<td width="30"></td>
							</tr>
						</table>
					</td>
					<!-- select a report table -->
					<td>
						<table cellspacing="0" cellpadding="0" width="604" bgcolor="#ffffcc" border="0">
							<tbody>
								<tr>
									<td class="arial_11_bold_white" bgcolor="#333333" height="22">
										&nbsp;&nbsp;Insurance Reports</td>
								</tr>
								<tr>
									<td height="10"></td>
								</tr>
								<tr>
									<td class="arial_12" align="left">
										&nbsp;&nbsp;Select a report to view using the pull down menus below.</td>
								</tr>
								<tr>
									<td height="5"></td>
								</tr>
								<tr>
									<td height="10"></td>
								</tr>
                        <tr>
                            <td width="100%">
						<!-- MAIG - CH3 - BEGIN - Modified the code to remove the PaymentType User control -->
                                <table>
                                    <uc:ReportType ID="ReportType" runat="server" />
                                </table>
                            </td>
							<!-- MAIG - CH3 - END - Modified the code to remove the PaymentType User control -->
                          <tr>
                            <td height="10">
                            </td>
                        </tr>
						<!-- MAIG - CH4 - BEGIN - Modified the code to  include the id and runat attributes -->
                        <td class="arial_12_bold" id="_datelabels" runat="server">
						<!-- MAIG - CH4 - END - Modified the code to  include the id and runat attributes -->
    						<%-- CHG0109406 - CH1 - BEGIN - Appended the timezone Arizona along with the Date Label --%>
    						<%-- CHG0110069 - CH1 - BEGIN - Appended the /Time along with the Date Label --%>
                            &nbsp;&nbsp;&nbsp; Start Date/Time (Arizona)&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; End Date/Time (Arizona)
                            <%-- CHG0110069 - CH1 - END - Appended the /Time along with the Date Label --%>
                            <%-- CHG0109406 - CH1 - END - Appended the timezone Arizona along with the Date Label --%>
                        </td>
                        <tr>
							<!-- MAIG - CH5 - BEGIN - Modified the code to  include the id and runat attributes -->
                            <td id="_datefilters" runat="server">
							<!-- MAIG - CH5 - END - Modified the code to  include the id and runat attributes -->
							<!-- MAIG - CH11 - BEGIN - Modified the MaxLength property from 11 to 19 to support IE 11 and editable -->
                                &nbsp;&nbsp;
                                <asp:TextBox ID="TextstartDt" runat="server" Width="200px" AutoComplete="Off" MaxLength="19"></asp:TextBox>&nbsp;<a
                                    href="javascript:NewCssCal('TextstartDt','MMddyyyy','dropdown',true,'24',true)"><img height="16" alt="Pick a date" src="/PaymentToolimages/cal.gif"
                                        width="16" border="0"></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:TextBox ID="TextendDt" runat="server" Width="200px" AutoComplete="Off" MaxLength="19"></asp:TextBox>&nbsp;<a
                                    href="javascript:NewCssCal('TextendDt','MMddyyyy','dropdown',true,'24',true)"><img height="16" alt="Pick a date" src="/PaymentToolimages/cal.gif"
                                        width="16" border="0"></a>
                            <!-- MAIG - CH11 - END - Modified the MaxLength property from 11 to 19 to support IE 11 and editable -->
                            </td>
                        </tr>
						<!-- MAIG - CH6 - BEGIN - Added code to display the Activity Date field for Settlement Report -->
						<%-- CHG0109406 - CH2 - BEGIN - Appended the timezone Arizona along with the Date Label --%>
						<%-- CHG0110069 - CH2 - BEGIN - Appended the /Time along with the Date Label --%>
                        <td class="arial_12_bold" id="_activitydatelabel" runat="server" visible="false">
                            &nbsp;&nbsp;&nbsp; Activity Date/Time (Arizona)
                        </td>
                        <%-- CHG0110069 - CH2 - END - Appended the /Time along with the Date Label --%>
                        <%-- CHG0109406 - CH2 - END - Appended the timezone Arizona along with the Date Label --%>
                        <tr>
                            <td id="_activitydatefilter" runat="server" visible="false">
                            <!-- MAIG - CH12 - BEGIN - Modified the MaxLength property from 11 to 19 to support IE 11 and editable -->
                                &nbsp;&nbsp;
                                <asp:TextBox ID="TextActivityDt" runat="server" Width="200px" AutoComplete="Off" MaxLength="19"></asp:TextBox>&nbsp;<a
                                    href="javascript:NewCssCal('TextActivityDt','MMddyyyy','dropdown',true,'24',true)"><img height="16" alt="Pick a date" src="/PaymentToolimages/cal.gif"
                                        width="16" border="0"></a>
                             <!-- MAIG - CH12 - END - Modified the MaxLength property from 11 to 19 to support IE 11 and editable -->
                            </td>
                        </tr>
						<!-- MAIG - CH6 - END - Added code to display the Activity Date field for Settlement Report -->
                        <%--<uc:dates id="Dates" runat="server" />--%>
                        <%--<uc:revenueproduct id="RevenueProduct" runat="server" />
								<uc:users id="Users" runat="server" />--%>
						<!-- MAIG - CH7 - BEGIN - Added code to create a panel and added the Payment Type User control -->
                        <asp:Panel ID ="pnlInsuranceReports" runat ="server">
                        <tr>
                            <td width="100%">
                                <table cellspacing="0" align="left">
								<uc:PaymentType ID="PaymentType" runat="server"></uc:PaymentType>
                                </table>
                            </td>
                        </tr>
						<!-- MAIG - CH7 - END - Added code to create a panel and added the Payment Type User control -->
                        <tr>
                            <td width="100%">
                                <table cellspacing="0" align="left">
								<uc:revenueproduct id="RevenueProduct" runat="server" />
                                </table>
                            </td>
                        </tr>
                        <!--PC Phase II changes CH1 -START- Added the below code to include the status Drop down box in UI -->
                        <td class="arial_12_bold">
                            &nbsp;<br />
                            &nbsp;&nbsp;&nbsp;&nbsp; Status
                        </td>
                        <tr>
                            <td class="style1" height="12">
								<!-- MAIG - CH8 - BEGIN - Added code to remove the Height attribute which resolved the display issue in Chrome & Firefox -->
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="status" runat="server" size="1" AutoPostBack="True"
                                     Width="207px">
                                    <asp:ListItem Value="-1">All</asp:ListItem>
                                     <asp:ListItem Value="1">New</asp:ListItem>
                                    <asp:ListItem Value="7">Approved</asp:ListItem>
                                    <asp:ListItem Value="12">Manually Voided</asp:ListItem>
                                    <asp:ListItem Value="3">Pending Approval</asp:ListItem>
                                    <asp:ListItem Value="13">Refunded </asp:ListItem>
                                    <asp:ListItem Value="6">Rejected</asp:ListItem>
                                    <asp:ListItem Value="4">Void</asp:ListItem>
                                </asp:DropDownList>
								<!-- MAIG - CH8 - END - Added code to remove the Height attribute which resolved the display issue in Chrome & Firefox -->
                            </td>
                            <!--PC Phase II changes CH1 -END- Added the below code to include the status Drop down box in UI -->
                        </tr>
                          <%--<tr>
                            <td class="arial_12_bold">
                                &nbsp;&nbsp;&nbsp;&nbsp;<br />
                                &nbsp;&nbsp;&nbsp; Application </td>
                        </tr>
                         <td class="style1" height="12">
                            &nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="application" runat="server" size="1"
                             autopostback="True" Height="16px" Width="192px">
                               <asp:ListItem Value="-1">All</asp:ListItem>
                               <asp:ListItem Value="6">Payments IVR</asp:ListItem>
                                <asp:ListItem Value="4">Payment Tool</asp:ListItem>
                                <asp:ListItem Value="13">MemberPoint</asp:ListItem>
                                <asp:ListItem Value="5">SalesX</asp:ListItem>
                                <asp:ListItem Value="15">netPOSitive</asp:ListItem>
                                <asp:ListItem Value="16">Exigen</asp:ListItem>                                                   
                                                                                        
                          </asp:DropDownList>
                        </td>--%>
                        <tr>
                            <td>
                                <table>
								<uc:users id="Users" runat="server" />
                                </table>
                            </td>
                        </tr>
						<!-- MAIG - CH9 - BEGIN - Added code to add panel for displaying the Location, User and Agency Criteria's -->
                        </asp:Panel>
                        <asp:Panel ID ="pnlUsers" runat ="server">
                        <tr>
                            <td>
                                <table>
								<uc:user id="User" runat="server" />
                                </table>
                            </td>
                        </tr>
                        </asp:Panel> 
                        <asp:Panel ID ="pnlLocations" runat ="server">
                        <tr>
                            <td>
                                <table>
                                    <uc:LocType id="LocType" runat="server" />    
                                </table>
                            </td>
                        </tr>
                        </asp:Panel>
                        <asp:Panel ID ="pnlAgency" runat ="server">
                        <tr>
                            <td>
                                <table>
                                    <uc:AgencyType id="AgencyType" runat="server" />    
                                </table>
                            </td>
                        </tr>
                        </asp:Panel>  
                        <asp:Panel ID = "pnldisplayType" runat ="server" >
                        <tr>
						    <td height="10"></td>
                        </tr>
                        <td class="arial_12_bold">
                            &nbsp;<br />
                            &nbsp;&nbsp;&nbsp;&nbsp; Display Type
                            &nbsp;<br />
                            
                        </td>
                        <tr>
                            <td class="arial_12_bold">&nbsp;&nbsp;
                                <asp:RadioButtonList ID="rbDisplayType" runat="server" repeatdirection="Horizontal" repeatlayout="Flow" enableviewstate="False">
                                    <asp:ListItem Text="Excel" Value="excel" Selected ="True" />
                                    <asp:ListItem Text="Pdf" Value="pdf" />
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        </asp:Panel>
   						<!-- MAIG - CH9 - END - Added code to add panel for displaying the Location, User and Agency Criteria's -->
								<tr>
									<td height="10"></td>
								</tr>
								<tr>
									<td bgcolor="#cccccc" height="3"></td>
								</tr>
								<tr>
									<td valign="center" align="right" bgcolor="#cccccc" height="17"><a href="ins_reports.aspx" runat="server">
									 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
									 <img height="17" src="/PaymentToolimages/btn_reset.gif" width="51" border="0"></a>&nbsp;
										 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
				 						<!-- MAIG - CH10 - BEGIN - Added code to reset the Error message in Click -->
										 <asp:imagebutton id="btnSubmit" runat="server" imageurl="/PaymentToolimages/btn_submit.gif" width="67px" height="17" OnClientClick="document.getElementById('lblErrMsg').innerHTML=''" onclick="btnSubmit_Click"></asp:imagebutton>&nbsp;
				 						<!-- MAIG - CH10 - END - Added code to reset the Error message in Click -->
									</td>
								</tr>
							</tbody></table>
					</td>
					<!-- space to right of select a report table -->
					<td>
						<table cellspacing="0" cellpadding="0" width="116">
							<tr>
								<td width="116"></td>
							</tr>
						</table>
					</td>
					<!-- end of select a report table --></tr>
			</table>
		    
		    
		    
			<!-- CSR renewal summary report table has 2 td's 30 & 685. 685 contains table with results -->
			<table cellspacing="0" cellpadding="0" width="750" border="0">
				<tr>
					<td colspan="2" height="15"></td>
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
			
			<!-- 5 td's: 30, 604 (made up of 250, 50, 304), 116 -->
			<asp:panel id="pnlTitle" runat="server">
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
						<TD align="right" width="354" bgColor="#cccccc">
						 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
						 <A href="ins_PrintXls.aspx?type=PRINT" target="_blank" runat="server"><IMG height="17" src="/PaymentToolimages/btn_print.gif" width="112" border="0"></A>&nbsp;<A href="ins_PrintXls.aspx?type=XLS" target="_blank" runat="server"><IMG height="17" src="/PaymentToolimages/btn_convert.gif" width="135" border="0"></A>&nbsp;</TD>
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
							&nbsp;&nbsp; <!-- Added By CTS -->
							<asp:label id="lblPayment" runat="server"></asp:label><BR>
							&nbsp;&nbsp; 
							<!-- Added By CTS -->
							<asp:label id="lblProduct" runat="server"></asp:label><BR>
							&nbsp;&nbsp;
							<asp:label id="lblRevenue" runat="server"></asp:label><BR>
							&nbsp;&nbsp;
							<asp:label id="lblApp" runat="server"></asp:label><BR>
							&nbsp;&nbsp;
							<asp:label id="lblCsr" runat="server"></asp:label><BR>
							&nbsp;&nbsp;
                    <asp:Label ID="lblRepDO" runat="server"></asp:Label><br/>
                    &nbsp;&nbsp;
                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                </td>
						<TD class="arial_12" vAlign="top" width="404" bgColor="#ffffff" height="22">
							<asp:label id="lblRunDate" runat="server"></asp:label><BR>
							<asp:label id="lblDateRange" runat="server"></asp:label><BR> <!-- Added By CTS -->
							<asp:label id="lblPaymentName" runat="server"></asp:label><BR> <!-- Added By CTS -->
							<asp:label id="lblProductName" runat="server"></asp:label><BR>
							<asp:label id="lblRevenueName" runat="server"></asp:label><BR>
							<asp:label id="lblAppName" runat="server"></asp:label><BR>
							<asp:label id="lblCsrs" runat="server"></asp:label><BR>
                    <asp:Label ID="lblRepDOName" runat="server"></asp:Label><br>
                    <asp:Label ID="lblStatusType" runat="server"></asp:Label>
                </td>
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
			<table cellspacing="0" cellpadding="0" width="750" border="0">
				<!-- this row's middle TD will contain a table whose sole purpose is a 1 pixel gray border.
						 Inside is the results table -->
				<tbody>
					<tr>
						<td width="25"></td>
						<td width="604">
							<!-- outer border table -->
							<table bordercolor="#cccccc" cellspacing="0" cellpadding="0" rules="none" width="604" border="0">
								<tr>
									<td>
										<!--										<!-- results --
										<table cellspacing="0" cellpadding="0" width="604" border="0">
										  <tr>
										    <td>-->
										    <!-- CHG0109406 - CH3 - BEGIN - Added a label lblHeaderTimeZone to display the timezone for the results displayed  -->
										    <table runat="server" width="100%">
										    <tr>
										    <td width="20%"><asp:label id="lblCyber" text="In CyberSource: Not in APDS" cssclass="arial_11_bold" visible="False" runat="server"></asp:label></td>
										    <td width="80%" align="right"><asp:label id="lblHeaderTimeZone"  text="All timezones are in Arizona" style="font-style:italic;text-align:right;" cssclass="arial_11_bold" visible="false" runat="server"></asp:label></td>
										    </tr>
										    </table>
										<!-- CHG0109406 - CH3 - END - Added a label lblHeaderTimeZone to display the timezone for the results displayed  -->
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
                                            <FooterStyle CssClass="arial_12_bold" BackColor="#cccccc" Font-Bold="True" HorizontalAlign="Center" />
                                            </asp:GridView>
										<br>
										<asp:label id="lblAPDS" text="In APDS: Not in CyberSource" cssclass="arial_11_bold" visible="False" runat="server"></asp:label>
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
										<!-- end of results -->
									</td>
									<td width="116"></td>
								</tr>
							</table>
		</form>
		<!-- end of outer border table --> </TD></TR></TBODY></TABLE>
		<table cellspacing="0" cellpadding="0" width="750" border="0">
			<tr>
				<td width="30"></td>
				<td class="arial_11">&nbsp;&nbsp;<asp:label id="lblPageNum2" runat="server"></asp:label></td>
				<td class="arial_10_blue" align="right">&nbsp;&nbsp;</td>
			</tr>
		</table>
		<!-- end of CSR renewal summary table -->
   <%--CHG0112662 - BEGIN - Added a new BR tag to fix tag issue, which displays the tag for a specific policy--%>
    <br />
    <%--CHG0112662 - END - Added a new BR tag to fix tag issue, which displays the tag for a specific policy--%>
	</body>
</HTML>
