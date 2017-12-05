<!-- STAR Retrofit II Changes: -->
<!-- 01/31/2007 - Changes done as part of CSR#5166-->
<!-- STAR Retrofit II.Ch1: Function added to open Users_PrintXls page in new window-->
<!-- STAR Retrofit II.Ch2: Commented to hide the arrow and to change font style -->
<!-- STAR Retrofit II.Ch3: To include Application,District office,Status,UserId controls and removed autopostback for controls -->
<!--                       Added buttons for Submit,Reset,ConverttoExcel,PrintVersion and labels to display messages -->
<!-- STAR Retrofit II.Ch4: Increased the TD width as that of Datagrid, Two Itemtemplates added for showing User's Status and last login date-->
<!-- STAR Retrofit II.Ch5: Added the HTML hidden variables to show the print version format as per the search criteria submitted -->			
<!--MAIG - CH1 - Added code to align properly that affects IE 11 alone-->
<%--CHG0109406 - CH1 - Appended the timezone Arizona along with the Date Field--%>
<%--CHG0110069 - CH1 - Appended the /Time along with the Date Label--%>
<%@ Page language="c#" Codebehind="Users.aspx.cs" AutoEventWireup="True" Inherits="MSC.Admin.Users" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Sales &amp; Service Payment Tool User Admininistration</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<script language="javascript">
    function doPost(UserRid) {
		document.thisForm.UserRid.value=UserRid;
		document.thisForm.submit();
    }
		//STAR Retrofit II.Ch1:START Function added to open Users_PrintXls page in new window
			function Open_Report(Type)
			{
				var Appln=document.getElementById("hdnAppName").value;
				var DO=document.getElementById("hdnDO").value;
				var Status=document.getElementById("hdnStatus").value;
				var UserId=document.getElementById("hdnUserId").value;
				var PrintXlsURL="Users_PrintXls.aspx?type=" +Type+ "&Appln=" +Appln+ "&DO=" +DO+ "&Status=" +Status+ "&UserId=" +UserId;
				window.open(PrintXlsURL,"Users_PrintXls");
				return false;
			}
			//STAR Retrofit II.Ch1:END
		</script>
	</head>
	<body>
	<!--MAIG - CH1 - BEGIN - Added code to align properly that affects IE 11 alone-->
			<style type="text/css">
		 @media screen and (min-width:0\0) { 
    #thisForm {padding-top:20px;}
}
		</style>
		<!--MAIG - CH1 - END - Added code to align properly that affects IE 11 alone-->
		<form id="thisForm" method="post" runat="server">
			<input type="hidden" name="UserRid">
			<!--STAR Retrofit II.Ch5 - START: Added the HTML hidden variables to show the print version format as per the search criteria submitted -->
			<input type="hidden" name="hdnAppName" id="hdnAppName" runat="server"> <input type="hidden" name="hdnDO" id="hdnDO" runat="server">
			<input type="hidden" name="hdnStatus" id="hdnStatus" runat="server"> <input type="hidden" name="hdnUserId" id="hdnUserId" runat="server">
			<!--STAR Retrofit II.Ch5 : END -->
			<table width="750" cellpadding="0" cellspacing="0">
				<tr>
					<td width="30"></td>
					<td align="left" class="arial_20">Admin
					</td>
				</tr>
				<tr>
					<td width="30"></td>
					<td bgcolor="#cccccc" height="6"></td>
				</tr>
				<tr>
					<td colspan="2" height="10"></td>
				</tr>
			</table>
			<table id="MessageTable" visible="false" width="750" border="0" cellpadding="0" cellspacing="0" runat="server">
				<tr>
					<td colspan="3" height="15"></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td colspan="2" class="arial_12_bold_red">
						<asp:label id="Message" runat="server" />
					</td>
				</tr>
			</table>
			<!-- 5 td's: 30, 14, 180, 291, 235 -->
			<table width="750" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<td width="30" height="15"></td>
					<td width="14" height="15"></td>
					<td width="180" height="15"></td>
					<td width="291" height="15"></td>
					<td width="235" height="15"></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td colspan="3" height="3" bgcolor="#cccccc"></td>
					<td></td>
				</tr>
				<tr>
					<td colspan="5" height="5"></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td width="14"><img src="../images/double_arrow.gif"></td>
					<td class="arial_13_bold" align="left" colspan="3">Create a New User</td>
				</tr>
				<tr>
					<td colspan="5" height="5"></td>
				</tr>
				<tr>
					<td colspan="2"></td>
					<td class="arial_12" valign="top">Click button to add a New User.</td>
					<td colspan="2" valign="bottom">
						<asp:hyperlink width="76" height="17" navigateurl="javascript:doPost('0');" imageurl="../images/btn_adduser.gif" runat="server" id="Hyperlink2" /></ASP:HYPERLINK"></td>
				<tr>
					<td colspan="5" height="5"></td>
				</tr>
				<tr designtimesp="12981">
					<td width="30"></td>
					<td colspan="3" height="3" bgcolor="#cccccc"></td>
					<td></td>
				</tr>
				<tr>
					<td colspan="5" height="20"></td>
				</tr>
				<tr>
					<td width="30"></td>
					<!--STAR Retrofit II.Ch2:START Commented to hide the arrow and to change font style -->
					<!--<td width="14"><img src="../images/double_arrow.gif"></td>-->
					<td class="arial_11_bold_white" align="left" bgColor="#333333" colSpan="3" height="20">&nbsp;&nbsp;Manage 
						Existing Users
					</td>
					<td></td>
					<!-- STAR Retrofit II.Ch2:END -->
				</tr>
				<tr>
				</tr>
					<td width="30"></td>
					<!--STAR Retrofit II.Ch3:START To include Application,District office,Status,UserId controls and removed autopostback for controls -->
					<!--                    Added buttons for Submit,Reset,ConverttoExcel,PrintVersion and labels to display messages -->
					<td colSpan="3">
						<table width="100%" bgColor="#ffffcc">
							<tr>
								<td colSpan="2" height="3"></td>
							</tr>
							<tr>
								<td class="arial_12_bold" align="left">&nbsp;Application</td>
								<!--<td class="arial_12">
									<asp:checkbox autopostback="True" id="_ShowAll" runat="server" checked="false" text="Include none" />
								</td>-->
								<!-- Commented as part of STAR Retrofit II.Ch3 -->
								<!-- <td class="arial_12">District Office</td> -->
								<!-- <td class="arial_12">With Roles</td> -->
								<!-- <td class="arial_12">Application</td> -->
								<td><asp:dropdownlist id="_AppName" runat="server" datatextfield="Description" datavaluefield="ID"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td colSpan="2" height="3"></td>
							</tr>
							<tr>
								<td class="arial_12_bold" align="left">&nbsp;District Office</td>
								<!--<td class="arial_12">With Roles</td>-->
								<td><asp:dropdownlist id="_DO" runat="server" datatextfield="Description" datavaluefield="ID"></asp:dropdownlist></td>
								<!-- Commented as part of STAR Retrofit II.Ch3 -->
								<!--<td class="arial_12">
								<asp:dropdownlist autopostback="true" runat="server" datatextfield="Description" datavaluefield="ID" />
								</td>-->
								<!--<td class="arial_12">
								<asp:checkbox autopostback="True" runat="server" checked="false" text="Include none" />
								</td>-->
								<!--<td class="arial_12">
								<asp:dropdownlist autopostback="true" runat="server" datatextfield="Description" datavaluefield="ID" />
								</td>--></tr>
							<tr>
								<td colSpan="2" height="3"></td>
							</tr>
							<tr>
								<td class="arial_12_bold" align="left">&nbsp;Status</td>
								<!-- New Dropdown list for User Status -->
								<td><asp:dropdownlist id="_Status" AutoPostBack="False" Runat="server" DataTextField="Description" DataValueField="ID">
										<asp:ListItem Value="2">All</asp:ListItem>
										<asp:ListItem Value="1">Active</asp:ListItem>
										<asp:ListItem Value="0">InActive</asp:ListItem>
									</asp:dropdownlist></td>
							</tr>
							<tr>
								<td colSpan="2" height="3"></td>
							</tr>
							<tr>
								<td class="arial_12_bold" align="left">&nbsp;User ID
								</td>
								<!-- New Text box for entering UserID -->
								<td><asp:textbox id="txtUserId" runat="server" MaxLength="50"></asp:textbox></td>
							</tr>
							<tr>
								<td colSpan="2" height="3"></td>
							</tr>
						</table>
					</td>
					<td></td>
				</TR>
				<tr>
					<td></td>
					<td vAlign="center" align="right" bgColor="#cccccc" colSpan="3"><A id="A1" href="Users.aspx" runat="server"><IMG height="17" src="../images/btn_reset.gif" width="51" border="0">
						</A>&nbsp;
						<asp:imagebutton id="btnSubmit" runat="server" width="67px" height="17" imageurl="../images/btn_submit.gif" btnSubmit_Click="btnSubmit_Click"></asp:imagebutton>&nbsp;</td>
					<td></td>
				</tr>
				<tr>
					<td colSpan="5" height="20"></td>
				</tr>
				<tr id="trButtons" runat="server">
					<td width="30"></td>
					<td align="right" bgColor="#cccccc" colSpan="3">
						<!-- Two Buttons for PrintVersion and ExcelConversion are added --><A onclick="return Open_Report('XLS')" href="Users_PrintXls.aspx" target="_blank"><IMG height="17" src="../images/btn_convert.gif" width="135" border="0"></A>&nbsp;<A id="A2" onclick="return Open_Report('PRINT')" href="Users_PrintXls.aspx" target="_blank" runat="server"><IMG height="17" src="../images/btn_print.gif" width="112" border="0"></A>&nbsp;
					</td>
					<td></td>
				</tr>
				<tr>
					<!-- Commented As part of STAR Retrofit II.Ch3  
					<td width="30" colspan="2"></td>
					<td colspan="3" class="arial_12">
						Select the appropriate User by clicking any of their info:
					</td>
					-->
					<td colSpan="5" height="5"></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td class="arial_12" colSpan="4"><asp:label id="lblMessage" Runat="server">Select the appropriate User by clicking any of their info:</asp:label></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td colSpan="4"><asp:label id="lblError" Runat="server" cssclass="arial_12_bold_red"></asp:label></td>
				</tr>
				<!-- STAR Retrofit II.Ch3:END -->
				<tr>
					<td colspan="5" height="5"></td>
				</tr>
			</table>
	<!--STAR Retrofit II.Ch4:START Increased the TD width as that of Datagrid, Two Itemtemplates added for showing User's Status and last login date-->
			<!--								3 td's: 30, 485, 235 have been modified to 2 td's: 30, 720-->
			<!--								User roles have been retrieved as "Description" in the template column "User Type" -->
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td width="30"></td>
					<td width="720"><asp:datagrid id="UsersGrid" runat="server" width="720" bordercolor="#cccccc" borderwidth="1px" cellpadding="3" gridlines="None" borderstyle="Solid" bgcolor="#ffffff" enableviewstate="False" autogeneratecolumns="False">
							<alternatingitemstyle Wrap="False" borderstyle="None" backcolor="#ffffcc" />
							<itemstyle borderstyle="None" cssclass="arial_12" />
							<headerstyle cssclass="arial_11_bold_white" backcolor="#333333" />
							<columns>
								<asp:templatecolumn headertext="Name">
									<itemtemplate>
										<asp:HyperLink Runat="server" CssClass="arial_12_blue_link" NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "UserRid", "javascript:doPost({0});")%>' ID="Hyperlink1" NAME="Hyperlink1">
											<%#DataBinder.Eval(Container.DataItem, "Name")%>
										</asp:HyperLink>
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headertext="User ID">
									<itemtemplate>
										<asp:HyperLink Runat="server" CssClass="arial_12_blue_link" NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "UserRid", "javascript:doPost({0});")%>' ID="Hyperlink3" NAME="Hyperlink3">
											<%#DataBinder.Eval(Container.DataItem, "UserId")%>
										</asp:HyperLink>
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headertext="User Type">
									<itemtemplate>
										<asp:HyperLink Runat="server" CssClass="arial_12_blue_link" NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "UserRid", "javascript:doPost({0});")%>' ID="Hyperlink4" NAME="Hyperlink4">
											<%# CSAAWeb.WebControls.DataBinder.Eval(Container.DataItem, "Description", ", ", "Roles") %>
										</asp:HyperLink>
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headertext="Status">
									<itemtemplate>
										<asp:HyperLink Runat="server" CssClass="arial_12_blue_link" NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "UserRid", "javascript:doPost({0});")%>' ID="Hyperlink5" NAME="Hyperlink5">
											<%#DataBinder.Eval(Container.DataItem, "Status")%>
										</asp:HyperLink>
									</itemtemplate>
								</asp:templatecolumn>
								<%--CHG0109406 - CH1 - BEGIN - Appended the timezone Arizona along with the Date Field--%>
								<%--CHG0110069 - CH1 - BEGIN - Appended the /Time along with the Date Label--%>
								<asp:templatecolumn headertext="Last Login Date/Time (Arizona)">
								<%--CHG0110069 - CH1 - END - Appended the /Time along with the Date Label--%>
								<%--CHG0109406 - CH1 - END - Appended the timezone Arizona along with the Date Field--%>
									<itemtemplate>
										<asp:HyperLink ID="hyp_Last_Login_Date" Runat="server" CssClass="arial_12_blue_link" NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "UserRid", "javascript:doPost({0});")%>' >
											<%#DataBinder.Eval(Container.DataItem, "Last_Login_Date","{0:MM/dd/yyyy   hh:mm:ss tt}")%>
										</asp:HyperLink>
									</itemtemplate>
								</asp:templatecolumn>
							</columns>
						</asp:datagrid></td>
					<!-- <td width="235"></td> --></tr>
			</table>
			<!-- STAR Retrofit II.Ch4:END --></form>
	</body>
</html>
