<!-- 1/25/2007 STAR Retrofit II - New Screen to add/Update a Parent/Branch Office.-->
<!--MAIG - CH1 - Modified the Title hy removing the Sales & Service word-->
<%--CHG0110069 - CH1 - Increased the DO ID maxlength from 3 to 9--%>
<%@ Register TagPrefix="uc" TagName="PageValidator" Src="../Controls/PageValidator.ascx" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Page language="c#" Codebehind="DO.aspx.cs" AutoEventWireup="True" Inherits="MSC.Admin.DO" %>
<HTML>
	<HEAD>
	<!--MAIG - CH1 - BEGIN - Modified the Title hy removing the Sales & Service word-->
		<title>Payment Tool Add/Update Branch Office</title>
	<!--MAIG - CH1 - END - Modified the Title hy removing the Sales & Service word-->
	</HEAD>
	<body>
		<form id="BO" method="post" runat="server">
			<csaa:hiddeninput id="ShowAll" runat="Server" autorestore="true" />
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td colspan="2" height="10"></td>
				</tr>
				<uc:pagevalidator runat="server" colspan="2" id="PageValidator1" />
				<csaa:validator id="_vldpage" runat="server"  onservervalidate="CheckValid" />
				<tr>
					<td colspan="2" height="10"></td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" border="0">
				<tr>
					<!-- space to left of information box -->
					<td width="30">
						<table cellspacing="0" cellpadding="0" border="0">
							<tr>
								<td width="30"></td>
							</tr>
						</table>
					</td>
					<td width="485">
						<csaa:hiddeninput id="_DORid" runat="Server" />
						<table cellSpacing="0" cellPadding="0" rules="none" border="0" bgcolor="#ffffcc">
							<colgroup>
								<col style="PADDING-LEFT:8px">
									<col>
							</colgroup>
							<tr>
								<td class="arial_11_bold_white" bgColor="#333333" colSpan="2" height="22"><asp:label id="Caption" runat="server">Add Branch Office</asp:label></td>
							</tr>
							<tr>
								<td colSpan="2" height="5"></td>
							</tr>
							<tr>
								<td class="arial_11" bgColor="#ffffcc" colSpan="2"><font color="#ff0000">*</font> indicates 
									required field</td>
							</tr>
							<tr>
								<td colSpan="2" height="5"></td>
							</tr>
							<CSAA:Validator ID="_vldDOs2" runat="server"  ErrorMessage="" onservervalidate="ReqValCheck" controltovalidate="_DOName" DefaultAction="required"></CSAA:Validator>		
							                    <tr>
                        <td colspan="3">
                            <table style="width:100%" border="0">
							<tr>
							 <%-- <td align="left" style="margin-right:0">--%>
								<csaa:validator id="vldDOs1" runat="server" onservervalidate="CheckDOName" controltovalidate="_DOName" errormessage="The Branch Office Name should be Alpha/Numeric, no special character" display="none" />
								<csaa:validator id="_vldDOs" runat="server" ErrorMessage=""  controltovalidate="_DOName"  >
								Branch Office</csaa:validator><%--</td>--%>
								<td vAlign="center" align="left" bgColor="#ffffcc" width="132px"><asp:textbox id="_DOName" maxlength="50" runat="server" Width="200px"></asp:textbox></td>
							</tr>
							<tr>
								<td colSpan="2" height="1"></td>
							</tr>
							
							<tr>
							
								<%--<td align="left">--%>
								<csaa:validator id="_vldDOnum" runat="server" ErrorMessage="" controltovalidate="_DOnum"  >
								Branch Office Number</csaa:validator><%--</td>--%>
								<%--CHG0110069 - CH1 - BEGIN - Increased the DO ID maxlength from 3 to 9--%>
								<td vAlign="center" align="left" bgColor="#ffffcc" width="132px"><asp:textbox onkeypress="return digits_only_onkeypress(event)" id="_DOnum" runat="server" maxlength="9" size="10"></asp:textbox></td>
								<%--CHG0110069 - CH1 - END - Increased the DO ID maxlength from 3 to 9--%>
								<CSAA:Validator ID="_vldDOnum2" runat="server"  ErrorMessage="test" onservervalidate="ReqValCheck" controltovalidate="_DOnum" DefaultAction="required"></CSAA:Validator>		
								<csaa:validator id="vldDOnum1" runat="server" onservervalidate="CheckDONum" controltovalidate="_DOnum" errormessage="The Branch Office Number should be Numeric, no special character" display="none" />
							</tr>
							<tr>
								<td colSpan="2" height="1"></td>
							</tr>
							<tr>
								<%--<td></td>--%><td width="135px"><asp:label id="_vldHUB1" runat="server" valign="middle" class="arial_12_bold" bgcolor="#ffffcc">HUB</asp:label><font class="arial_11_red">
										*</font></td>
								<td vAlign="center" align="left" bgColor="#ffffcc" width="132px"><asp:dropdownlist id="_HUB" runat="server" datavaluefield="ID" datatextfield="Description"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td colSpan="2" height="1"></td>
							</tr>
							<tr>
								<td class="arial_12_bold" align="left" bgColor="#ffffcc" colSpan="2">Active
									<asp:checkbox id="_Active" runat="server" checked="True"></asp:checkbox></td>
							</tr>
							   </table>
                        </td>
                    </tr>
							<tr>
								<td colSpan="2" height="6"></td>
							</tr>
							<tr>
								<td height="3" bgcolor="#cccccc" colspan="2"></td>
							</tr>
							<tr>
								<td colspan="2" bgcolor="#cccccc">
									<table cellspacing="0" cellpadding="0" border="0">
										<tr height="22">
											<td width="500"></td>
											<td align="middle" width="70" height="22">
												<asp:ImageButton ID="CancelDO" Runat="server" ImageUrl="../Images/btn_cancel.gif" onclick="CancelDO_Click"></asp:ImageButton>
											</td>
											<td valign="top" align="middle" width="110" height="22">
												<asp:ImageButton id="UpdateDO" runat="server" ImageUrl="../Images/btn_continue.gif" onclick="UpdateDO_Click"></asp:ImageButton>
											</td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
					<td width="235"></td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
