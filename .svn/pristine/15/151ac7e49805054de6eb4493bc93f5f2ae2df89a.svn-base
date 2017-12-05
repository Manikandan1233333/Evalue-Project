<%@ Page language="c#" Codebehind="TurnIn_General_Report.aspx.cs" AutoEventWireup="True" Inherits="MSCR.TurnIn_General_Report" %>
<%@ Register TagPrefix="uc" TagName="Dates" Src="../Controls/Dates.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<!--To make Header back color for turn in reports should be white -->
<%--PC Phase II changes CH1 - Added the below code to handle Date Time Picker--%>
<!-- MAIG - CH1 - Modified the style to align properly -->
<!-- MAIG - CH2 - Modified the td style to align properly -->
<%--MAIGEnhancement CHG0107527 - CH1 - Added code to set the max length of the textbox from 11 to 19 to support manual date entry --%>
<%--CHG0109406 - CH1 - Appended the timezone Arizona along with the Date Label--%>
<!--CHG0109406 - CH2 - Added a label lblHeaderTimeZone to display the timezone for the results displayed  -->
<%--CHG0110069 - CH1 - Appended the /Time along with the Date Label--%>
<%--CHG0110069 - CH2 - Appended the /Time along with the Date Label--%>
<HTML>
	<HEAD>
		<title>Manager Reports</title>
		<LINK href="../Style.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
		function Open_Report(Type)
				{
				var PrintXlsURL="TurnIn_General_PrintXls.aspx?type=" +Type;
				window.open(PrintXlsURL,"");
				return false;
				}	
		
		</script>
		<script type="text/javascript" src="../Reports/CalendarControl.js"></script>   
	    <style type="text/css">
		/* MAIG - CH1 - BEGIN - Modified the style to align properly */
            .style2
            {
                font-family: arial, helvetica, sans-serif;
                color: #000000;
                font-size: 12px;
                width: 80%;
            }
            .style4
            {
                font-family: arial, helvetica, sans-serif;
                color: #000000;
                font-size: 12px;
                font-weight: bold;
                text-decoration: none;
            }
            .style12
            {
                width: 4px;
            }
            .style13
            {
                font-family: arial, helvetica, sans-serif;
                color: #000000;
                font-size: 12px;
                font-weight: bold;
                text-decoration: none;
                width: 4px;
            }
            .style15
            {
                font-family: arial, helvetica, sans-serif;
                color: #000000;
                font-size: 12px;
                font-weight: bold;
                text-decoration: none;
                width: 269px;
            }
			/* MAIG - CH1 - END - Modified the style to align properly */
        </style>
	</HEAD>
	<body>
		<form id="TurnIn_General_Report" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<!-- space to left of search table-->
					<td width="30">
					</td>
					<!-- end of space to left of search table-->
					<!-- select a report table -->
					<td>
						<table cellSpacing="0" cellPadding="0" width="604" bgColor="#ffffcc" border="0">
							<tr>
								<td class="arial_11_bold_white" width="604" bgColor="#333333" colspan="4" height="22">&nbsp;&nbsp; 
									Reports Management
								</td>
							</tr>
							<!-- MAIG - CH2 - BEGIN - Modified the td style to align properly -->
							<tr>								
							    <td class="style1">&nbsp;&nbsp;</td>
								<td class="arial_12_bold" width="45%">Report</td>
								<td class="arial_12_bold" width="42%">Status</td>
								<td class="arial_12_bold" width="10%"></td>
							</tr>
							<tr>
								<td height="22" class="style4">&nbsp;</td>
								<td class="arial_12" height="22"><asp:dropdownlist id="cboReportType" runat="server" DataValueField="ID" DataTextField="Description" Width="223px" size="1" AutoPostBack="True" onselectedindexchanged="cboReportType_SelectedIndexChanged"></asp:dropdownlist>
								<td colspan="2" height="22">
								<asp:dropdownlist id="cboStatus" runat="server" DataValueField="ID" DataTextField="Description" Width="223px" size="1" AutoPostBack="True" onselectedindexchanged="cboStatus_SelectedIndexChanged"></asp:dropdownlist></td>
								<br />
                                </td>
							</tr>
							<!--
							<tr>
								<td width="604" colspan="4">
									<table cellPadding="2" width="100%" align="left" border="0">
										<%--<uc:dates id="Dates" runat="server" visible="false"></uc:dates>--%></table>
								    <br />
								</td>
							</tr>
							-->
							<tr>
							<td class="style4">&nbsp;&nbsp;</td>
							          <td  height="15 px"  colspan="3">							          
							          </td>
							</tr>
							
							<tr>
							<td  id= "Date" runat="server" Visible="false" colspan="4">
							<table style="width: 99%">
							<%--CHG0110069 - CH1 - BEGIN - Appended the /Time along with the Date Label--%>
                            <tr>
								<td class="style1">&nbsp;&nbsp;</td>
								<%--CHG0109406 - CH1 - BEGIN - Appended the timezone Arizona along with the Date Label --%>
								<td class="style4">Start Date/Time (Arizona)</td>
								<td class="style4">End Date/Time (Arizona)								   
								<%--CHG0109406 - CH1 - END - Appended the timezone Arizona along with the Date Label --%>
								</td>
							</tr>
							<%--CHG0110069 - CH1 - END - Appended the /Time along with the Date Label--%>
							<tr>
								<td class="style1">&nbsp;&nbsp;</td>
								<%--MAIGEnhancement CHG0107527 - CH1 - BEGIN - Added code to set the max length of the textbox from 11 to 19 to support manual date entry --%>
								<td class="style15">
								      <asp:TextBox ID="startDt" runat="server" Width="200px" AutoComplete="Off" MaxLength="19"></asp:TextBox>&nbsp;<a
                                    href="javascript:NewCssCal('startDt','MMddyyyy','dropdown',true,'24',true)"><img id ="image" runat ="server" height="16" alt="Pick a date" src="/PaymentToolimages/cal.gif"
                                        width="16" border="0"></a>
								</td>
								<td class="arial_12_bold">
								   <asp:TextBox ID="endDt" runat="server" Width="200px" AutoComplete="Off" MaxLength="19"></asp:TextBox>&nbsp;<a
                                    href="javascript:NewCssCal('endDt','MMddyyyy','dropdown',true,'24',true)"><img id ="image1" runat ="server" height="16" alt="Pick a date" src="/PaymentToolimages/cal.gif"
                                        width="16" border="0"></a>
								</td>
								<%--MAIGEnhancement CHG0107527 - CH1 - END - Added code to set the max length of the textbox from 11 to 19 to support manual date entry --%>
							</tr>
							</table>
							
							</td>
							</tr>								
							
							<tr>
								<td colspan="4" height="10"></td>
							</tr>
							<tr>
							
								<td class="style4"> </td>
								<td class="style4" colspan="3">Rep DO</td>
							</tr>
							<tr>
							<td class="style4"> </td>
								<td class="arial_12" colspan="3"> <asp:dropdownlist id="_RepDO" DataValueField="ID" DataTextField="Description" Width="223px" AutoPostBack="True" Runat="server" onselectedindexchanged="_RepDO_SelectedIndexChanged"></asp:dropdownlist>
								</td>
							</tr>
							<tr>
								<td colspan="4" height="10"></td>
								
							</tr>
							<tr>
							<td class="style4"> </td>
								<td class="style2" colspan="3"><b>User(s)</b> hold down 'Ctrl' to select multiple</td>
								
							</tr>
							<tr>
								<td class="style4"> </td>
								<td class="arial_12" colspan="3"><asp:listbox id="UserList" runat="server" rows="10" selectionmode="Multiple" cssclass="arial_12"></asp:listbox></td>
							</tr>
							<!-- MAIG - CH1 - END - Modified the td style to align properly -->
							<tr>
								<td width="604" colspan="4" height="10"></td>
							</tr>
							<tr>
								<td vAlign="center" align="right" bgColor="#cccccc" colspan="4" height="17"><A id="A1" href="TurnIn_General_Report.aspx" runat="server">
								 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
								 <IMG height="17" src="/PaymentToolimages/btn_reset.gif" width="51" border="0"></A>&nbsp;
									 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
									 <asp:imagebutton id="btnSubmit" runat="server" imageurl="/PaymentToolimages/btn_submit.gif" width="67px" height="17" onclick="btnSubmit_Click"></asp:imagebutton>&nbsp;
								</td>
							</tr>
						</table>
					</td>
					<td width="116">&nbsp;</td>
				</tr>
				<tr>
					<td height="10">&nbsp;</td>
					<td height="10">&nbsp;</td>
					<td height="10">&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td><asp:label class="arial_12_bold_red" id="lblErrMsg" runat="server">&nbsp;</asp:label><asp:label class="arial_12" id="lblPageNum1" runat="server"></asp:label></td>
					<td height="10">&nbsp;</td>
				</tr>
				<tr id="trConvertPrint" runat="server">
					<td>&nbsp;</td>
					<td align="left">
						<table width="604">
							<tr>
								<td align="right" width="604" bgColor="#cccccc"><A id="A3" onclick="return Open_Report('XLS')" href="TurnIn_General_PrintXls.aspx?type=XLS" target="_blank" runat="server">
								 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
								 <IMG height="17" src="/PaymentToolimages/btn_convert.gif" border="0"></A>&nbsp;<A id="A2" onclick="return Open_Report('PRINT')" href="TurnIn_General_PrintXls.aspx?type=PRINT" target="_blank" runat="server"><IMG height="17" src="/PaymentToolimages/btn_print.gif" border="0"></A>&nbsp;</td>
							</tr>
						</table>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td width="604">
						<table id="tblReportInfo" cellSpacing="0" cellPadding="0" border="0" runat="server">
						<%--CHG0110069 - CH2 - BEGIN - Appended the /Time along with the Date Label--%>
							<tr>
								<td class="arial_12_bold" width="120">&nbsp;&nbsp;<asp:label id="lblRunDateName" runat="server">Run Date/Time:</asp:label></td>
								<td><asp:label id="lblRunDate" runat="server" CssClass="arial_12"></asp:label></td>
							</tr>
							<tr id="trRundate" runat="server">
								<td class="arial_12_bold" width="120">&nbsp;&nbsp;<asp:label id="lblDateRangeName" runat="server">Date/Time Range:</asp:label></td>
								<td><asp:label id="lblDateRange" runat="server" CssClass="arial_12"></asp:label></td>
							</tr>
							<%--CHG0110069 - CH2 - END - Appended the /Time along with the Date Label--%>
							<tr>
								<td class="arial_12_bold" width="120">&nbsp;&nbsp;<asp:label id="lblReportStatusName" runat="server">Report Status:</asp:label></td>
								<td><asp:label id="lblReportStatus" runat="server" CssClass="arial_12"></asp:label></td>
							</tr>
							<tr>
								<td class="arial_12_bold" width="120">&nbsp;&nbsp;<asp:label id="lblUsersName" runat="server">User(s):</asp:label></td>
								<td><asp:label id="lblUsers" runat="server" CssClass="arial_12"></asp:label></td>
							</tr>
							<tr>
								<td class="arial_12_bold" width="120">&nbsp;&nbsp;<asp:label id="lblRepDoName" runat="server">Rep DO:</asp:label></td>
								<td><asp:label id="lblRepDo" runat="server" CssClass="arial_12"></asp:label></td>
							</tr>
						</table>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td colspan="2">
						<table WIDTH="604" id="tbReport" cellspacing="0" cellpadding="0" runat="server">
						<!-- CHG0109406 - CH2 - BEGIN - Added a label lblHeaderTimeZone to display the timezone for the results displayed  -->
							<tr>
								<td bgColor="#cccccc">
								<table runat="server" width="100%">
								<tr>
								<td width="30%"><asp:label CssClass="arial_12_bold" style=" text-align:left" id="lblCaption" runat="server"></asp:label></td>
								<td width="80%" align="right"><asp:label id="lblHeaderTimeZone"  text="All timezones are in Arizona" style="font-style:italic;" cssclass="arial_11_bold"  runat="server"></asp:label></td>
								</tr>
								</table>
								
								
								</td>
							</tr>
							    <!-- CHG0109406 - CH2 - END - Added a label lblHeaderTimeZone to display the timezone for the results displayed  -->
							<tr>
								<td>
									<asp:GridView ID="dgReport" OnRowDataBound="ItemDataBound1" runat="server" CssClass="arial_12" PageSize="25" width="604px"
                                            AllowPaging="true" OnPageIndexChanging="PageIndexChanged1" 
                                            PagerSettings-Mode="NextPrevious" PagerSettings-Position="TopAndBottom" 
                                            BorderStyle="none" CellPadding="2" CellSpacing="0">
										    <AlternatingRowStyle BackColor="LightYellow" />
										    <RowStyle CssClass="arial_12" />
										    <HeaderStyle CssClass="arial_12_bold"  HorizontalAlign="Left" />
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
								</td>
							</tr>
							<tr height="20">
								<td bgColor="#cccccc">&nbsp;</td>
							</tr>
							<tr>
								<td><asp:label CssClass="arial_12" id="lblPageNum2" runat="server"></asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
