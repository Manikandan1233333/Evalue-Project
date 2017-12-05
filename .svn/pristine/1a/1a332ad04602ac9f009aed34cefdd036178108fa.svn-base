<!-- 1/25/2007 STAR Retrofit II - Added New Screen as a part of CSR4852 for Branch Office administration-->
<!--MAIG - CH1 - Modified the Title hy removing the Sales & Service word-->
<!--MAIG - CH2 - Added code to align properly that affects IE 11 alone-->
<%@ Page language="c#" Codebehind="DOs.aspx.cs" AutoEventWireup="True" Inherits="MSC.Admin.DOs" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<!--MAIG - CH1 - BEGIN - Modified the Title hy removing the Sales & Service word-->
		<title>Payment Tool Branch Administration</title>
		<!--MAIG - CH1 - END - Modified the Title hy removing the Sales & Service word-->
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<script language="javascript">
		function doPost(DO_Id)
		{
		    document.thisForm.DO_Id.value = DO_Id;
			document.thisForm.submit();
		}
		</script>
	</HEAD>
	<body>
	<!--MAIG - CH2 - BEGIN - Added code to align properly that affects IE 11 alone-->
					<style type="text/css">
		 @media screen and (min-width:0\0) { 
    #thisForm {padding-top:20px;}
}
		</style>
		<!--MAIG - CH2 - END - Added code to align properly that affects IE 11 alone-->
		<form id="thisForm" method="post" runat="server">
			<input type="hidden" name="DO_Id">
			<table cellSpacing="0" cellPadding="0" width="750">
				<tr>
					<td width="30"></td>
					<td class="arial_20" align="left">Admin
					</td>
				</tr>
				<tr>
					<td width="30"></td>
					<td bgColor="#cccccc" height="6"></td>
				</tr>
				<tr>
					<td colSpan="2" height="10"></td>
				</tr>
			</table>
			<table id="MessageTable" cellSpacing="0" cellPadding="0" width="750" border="0" runat="server" visible="false">
				<tr>
					<td colSpan="3" height="15"></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td class="arial_12_bold_red" colSpan="2"><asp:label id="Message" runat="server"></asp:label></td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td width="30" height="15"></td>
					<td style="WIDTH: 1px" width="1" height="15"></td>
					<td style="WIDTH: 263px" width="263" height="15"></td>
					<td width="291" height="15"></td>
					<td width="235" height="15"></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td bgColor="#cccccc" colSpan="3" height="3"></td>
					<td></td>
				</tr>
				<tr>
					<td colSpan="5" height="5"></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td style="WIDTH: 1px" width="1"><IMG src="../images/double_arrow.gif"></td>
					<td class="arial_13_bold" align="left" colSpan="3">&nbsp;&nbsp;Create a New Branch 
						Office</td>
				</tr>
				<tr>
					<td colSpan="5" height="5"></td>
				</tr>
				<tr>
					<td style="WIDTH: 27px" colSpan="2"></td>
					<td class="arial_12" style="WIDTH: 263px" vAlign="top">&nbsp;&nbsp; Click button to 
						add a New Branch Office.</td>
					<td vAlign="bottom" colSpan="2"><asp:hyperlink id="Hyperlink2" runat="server" navigateurl="javascript:doPost('ins');" width="76" height="17" imageurl="../images/btn_addbranchoffice.gif"></asp:hyperlink></td>
				</tr>
				<TR>
					<TD colSpan="5" height="5"></TD>
				</TR>
				<TR designtimesp="12981">
					<td width="30"></td>
					<td bgColor="#cccccc" colSpan="3" height="3"></td>
					<td></td>
				</TR>
				<tr>
					<td colSpan="5" height="20"></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td style="WIDTH: 1px" width="1"><IMG src="../images/double_arrow.gif"></td>
					<td class="arial_13_bold" align="left" colSpan="3">&nbsp;&nbsp;Manage Existing 
						Branch Office's</td>
				</tr>
				<tr>
				</tr>
				<TR>
					<td width="30"></td>
					<td colSpan="3">
						<table width="100%">
							<tr>
								<td class="arial_12">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;HUB&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:dropdownlist id="_DO" runat="server" AutoPostBack="True" OnSelectedIndexChanged="Force_Postback" datatextfield="Description" datavaluefield="ID" Width="156px"></asp:dropdownlist></td>
								<td class="arial_12"></td>
							<tr>
								<td class="arial_12"></td>
								<td class="arial_12"></td>
							</tr>
						</table>
					</td>
					<td></td>
				</TR>
				<tr>
					<td style="HEIGHT: 19px" colSpan="5" height="19"></td>
				</tr>
				<tr>
					<td>&nbsp;&nbsp;</td>
					<td class="arial_12" align="left" colSpan="3"><asp:label id="lblmsg" runat="server">Select the
							appropriate Branch Office by clicking any of their info:</asp:label></td>
				</tr>
				<tr>
					<td style="WIDTH: 27px" width="27" colSpan="2"></td>
					<td class="arial_12_bold_red" colSpan="3"><asp:label id="lblErrMsg" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td colSpan="5" height="5"></td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td width="30">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
					<td width="485"><asp:datagrid id="DOsGrid" runat="server" width="485" bordercolor="#cccccc" borderwidth="1px" cellpadding="3" gridlines="Both" borderstyle="Solid" bgcolor="#ffffff" enableviewstate="False" autogeneratecolumns="False">
							<alternatingitemstyle borderstyle="None" backcolor="#ffffcc" />
							<itemstyle borderstyle="None" cssclass="arial_12" />
							<headerstyle cssclass="arial_11_bold_white" backcolor="#333333" />
							<columns>
								<asp:templatecolumn headertext="Branch Office">
									<itemtemplate>
										<asp:HyperLink Runat="server" CssClass="arial_12_blue_link" NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "DO_Id", "javascript:doPost({0});")%>' ID="Hyperlink1">
											<%#DataBinder.Eval(Container.DataItem, "Branch Office")%>
										</asp:HyperLink>
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headertext="Branch Office Number">
									<itemtemplate>
										<asp:HyperLink Runat="server" CssClass="arial_12_blue_link" NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "DO_Id", "javascript:doPost({0});")%>' ID="Hyperlink3">
											<%#DataBinder.Eval(Container.DataItem, "DO_Id")%>
										</asp:HyperLink>
									</itemtemplate>
								</asp:templatecolumn>
								<asp:templatecolumn headertext="HUB">
									<itemtemplate>
										<asp:HyperLink Runat="server" CssClass="arial_12_blue_link" NavigateUrl='<%#DataBinder.Eval(Container.DataItem, "DO_Id", "javascript:doPost({0});")%>' ID="Hyperlink4">
											<%#DataBinder.Eval(Container.DataItem, "HUB")%>
										</asp:HyperLink>
									</itemtemplate>
								</asp:templatecolumn>
							</columns>
						</asp:datagrid></td>
					<td width="485"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
