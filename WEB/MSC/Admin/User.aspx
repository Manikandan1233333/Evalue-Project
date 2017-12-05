<!--
		* HISTORY
		* MODIFIED BY COGNIZANT AS PART OF Q4 RETROFIT CHANGES
		* 12/07/2005 Q4-Retrofit.ch1 : Changed the Repeatcolumns value to 3 in _Roles Checkboxlist 
		*		for aligning the roles properly
		* RFC 185138 - AD Integration CH1  Commented the reset password button in user administration page 
		* MAIG - CH1 - Modified the length of the Textbox
		* MAIG - CH2 - Included AgencyID & AgencyName to the User Admin screen for all Users
        * MAIG - CH3 - Removed the valign=top property
!-->
<%@ Page language="c#" Codebehind="User.aspx.cs" AutoEventWireup="True" Inherits="MSC.Admin.User" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="uc" TagName="PageValidator" Src="../Controls/PageValidator.ascx" %>
<%@ Register TagPrefix="uc" TagName="Phone" Src="../Controls/Phone.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>Sales &amp; Service Payment Tool Add/Update User</title>
		<script language="javascript">
			function Delete_onclick(b) {
				return confirm('Are you sure you want to delete ' + this.form._UserId.value + '?');
			}
			function window_onload() {
				if (document.all("Delete")) document.all("Delete").onclick=Delete_onclick;
				var els=document.thisForm.elements;
				for (i=0; i<els.length; i++) {
					switch (els[i].type) {
						case 'text': els[i].onchange=stop_app; break;
						case 'select-one': if (!els[i].onchange) els[i].onchange=stop_app; break;
						case 'checkbox': els[i].onclick=stop_app; break;
					}
				}
			}
			function stop_app() {
				var el = this.form["_AppName"];
				if (el.Selected) return;
				el.Selected = el.selectedIndex;
				el.onchange=function () {
					this.selectedIndex=this.Selected; 
					alert("Application cannot be changed after other changes have been made to the form.");
				}
			}
		</script>
	</head>
	<body>
		<form id="thisForm" method="post" runat="server">
			<csaa:hiddeninput id="ShowAll" runat="Server" autorestore="true" />
			<table cellspacing="0" cellpadding="0" width="750">
				<tr>
					<td height="10"></td>
				</tr>
				</tr>
				<uc:pagevalidator runat="server" colspan="2" id="PageValidator1" />
				<csaa:validator id="Valid" runat="server" onservervalidate="CheckValid" />
				<tr>
					<td height="10"></td>
				</tr>
			</table>
			<!-- 3 tables: space:30, 485, space:235 -->
			<table cellspacing="0" cellpadding="0" width="750" border="0">
				<tr>
					<!-- space to left of information box -->
					<td width="30">
						<table cellspacing="0" cellpadding="0" border="0">
							<tr>
								<td width="30"></td>
							</tr>
						</table>
					</td>
					<!-- search table -->
					<td width="485">
						<csaa:hiddeninput id="_UserRid" runat="Server" />
						<table cellspacing="0" cellpadding="0" rules="none" width="485" bgcolor="#ffffcc" border="0">
							<colgroup>
								<col style="PADDING-LEFT:8px">
									<col>
							</colgroup>
							<tr>
								<td class="arial_11_bold_white" bgcolor="#333333" colspan="2" height="22">
									<asp:label id="Caption" runat="server">Add</asp:label>
									User
								</td>
							</tr>
							<tr>
								<td colspan="2" height="5"></td>
							</tr>
							<tr>
								<td class="arial_11" colspan="2"><font color="#ff0000">*</font> indicates 
									required field</td>
							</tr>
							<tr>
								<td colspan="2" valign="center">&nbsp; 
								 <!-- External URL Implementation-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
                <img id="imgErrorFlag" height="20" src="/PaymentToolimages/red_arrow.gif" width="22" style="DISPLAY: none" runat="server">
									<asp:label id="lblErrorMsg" height="20" runat="server" cssclass="arial_12_bold_red" width="433px"></asp:label>
								</td>
							</tr>
							<tr>
								<td colspan="2" height="5"></td>
							</tr>
							<tr>
								<csaa:validator id="_vldApps" runat="server" ErrorMessage="Please complete or correct the requested information in the field(s) highlighted in red below." controltovalidate="_AppName" valign="middle" width="104" bgcolor="#ffffcc">
								Application</csaa:validator>
								<td valign="center" align="left" width="375">
									<asp:dropdownlist autopostback="true" onselectedindexchanged="_AppName_SelectedIndexChanged" id="_AppName" runat="server" datatextfield="Description" datavaluefield="ID" /></td>
							</tr>
							<tr>
								<td colspan="2" height="5"></td>
							</tr>
							<tr>
								<csaa:validator id="_vldRoles" runat="server" controltovalidate="_Roles" valign="middle" width="104" bgcolor="#ffffcc">
								Role(s)</csaa:validator>
								<td valign="center" align="left" width="375" class="arial_10">
									<asp:checkboxlist id="_Roles" repeatcolumns="3" repeatdirection="Horizontal" runat="server" datatextfield="Description" datavaluefield="Role" font-size="10" />
								</td>
							</tr>
							<tr>
								<td colspan="2" height="5"></td>
							</tr>
							<tr>
								<csaa:validator id="vldUserId" runat="server" controltovalidate="_UserId" valign="middle" width="104" bgcolor="#ffffcc">
								User ID</csaa:validator>
								<td valign="center" align="left" width="375">
									<asp:textbox id="_UserId" maxlength="50" size="35" runat="server" />
								</td>
							</tr>
							<tr>
								<td colspan="2" height="5"></td>
							</tr>
							<tr>
								<csaa:validator id="vldDO" runat="server" controltovalidate="_DO" valign="middle" width="104" bgcolor="#ffffcc">
								District Office</csaa:validator>
								<td valign="center" align="left" width="375">
									<asp:dropdownlist id="_DO" runat="server" datatextfield="Description" datavaluefield="ID" /></td>
							</tr>
							<tr>
								<td colspan="2" height="5"></td>
							</tr>
							<tr>
								<csaa:validator id="vldRepId1" runat="server" controltovalidate="_RepId" defaultaction="numeric" display="none" errormessage="Rep ID must be numeric." />
								<csaa:validator id="vldRepId" runat="server" controltovalidate="_RepId" valign="middle" width="104" bgcolor="#ffffcc">
								Rep ID</csaa:validator>
								<td valign="center" align="left" width="375">
									<!--MAIG - CH1 - BEGIN - Modified the length of the Textbox-->
									<asp:textbox id="_RepId" maxlength="10" size="10" runat="server" onkeypress="return digits_only_onkeypress(event)" /></td>
									<!--MAIG - CH1 - END - Modified the length of the Textbox-->
							</tr>
							<tr>
								<td colspan="2" height="5"></td>
							</tr>
							<tr>
								<csaa:validator id="vldFirstName" runat="server" controltovalidate="_FirstName" valign="middle" width="104" bgcolor="#ffffcc">
								First Name</csaa:validator>
								<td valign="center" align="left" width="375">
									<asp:textbox id="_FirstName" maxlength="100" size="35" runat="server" />
								</td>
							</tr>
							<tr>
								<td colspan="2" height="5"></td>
							</tr>
							<tr>
								<csaa:validator id="vldLastName" runat="server" controltovalidate="_LastName" valign="middle" width="104" bgcolor="#ffffcc">
								Last Name</csaa:validator>
								<td valign="center" align="left" width="375">
									<asp:textbox id="_LastName" maxlength="100" size="35" runat="server" />
								</td>
							</tr>
							<tr>
								<td colspan="2" height="5"></td>
							</tr>
							
							<tr>
							
								
								<csaa:validator id="vldEmail" runat="server" ErrorMessage="test" controltovalidate="_Email" valign="middle" width="104" bgcolor="#ffffcc">
								E-mail Address</csaa:validator>
								<td valign="center" align="left" width="375">
									<asp:textbox id="_Email" maxlength="255" size="35" runat="server" />
								</td>
								<CSAA:Validator ID="vld" runat="server" OnServerValidate="CheckLength" ErrorMessage="" ControlToValidate="_Email"></CSAA:Validator>
								<csaa:validator id="vldEmail1" runat="server" onservervalidate="CheckEmail" errormessage="Email Address is not valid" display="none" />
							</tr>
							<tr>
								<td colspan="2" height="5"></td>
							</tr>
							<uc:phone runat="server" id="_Phone" required="true" horizontal="true"  bellformat="true" includeextension="true" />

							<!--MAIG - CH2 - BEGIN - Included AgencyID & AgencyName to the User Admin screen for all Users -->
							 <tr>
								<td colspan="2" height="5"></td>
							</tr>
							
							<tr>
							    <td class="arial_12_bold" align="left" bgcolor="#ffffcc"> Agency ID
								</td>
								<td valign="center" align="left" width="375">
									<asp:textbox id="_AgencyID" maxlength="10" size="10" onkeypress="return digits_only_onkeypress(event)" runat="server" />
								</td>
							</tr>
							<tr>
								<td colspan="2" height="5"></td>
							</tr>
							<tr>
								<td class="arial_12_bold" align="left" bgcolor="#ffffcc"> Agency Name</td>
								<td valign="center" align="left" width="375">
									<asp:textbox id="_AgencyName" maxlength="100" size="35" runat="server" />
								</td>
							</tr>
							<tr>
								<td colspan="2" height="5"></td>
							</tr>
							<!--MAIG - CH2 - END - Included AgencyID & AgencyName to the User Admin screen for all Users -->




							<tr>
								<td colspan="2" height="5"></td>
							</tr>
							<tr>
								<td colspan="2" class="arial_12_bold" align="left" bgcolor="#ffffcc">Active
									<asp:checkbox id="_Active" checked="True" runat="server" /></td>
							</tr>
							<tr>
								<td colspan="2" height="15"></td>
							</tr>
							<tr>
								<td colspan="2" height="15"></td>
							</tr>
							<tr>
								<td height="3" bgcolor="#cccccc" colspan="2"></td>
							</tr>
							<tr>
								<td colspan="2" bgcolor="#cccccc">
									<table cellspacing="0" cellpadding="0" border="0">
										<tr height="22">
										<!--RFC 185138 - AD Integration CH1 Start - Commented the reset password button in user administration page -->
											<!--td align="middle" width="135" height="22">
												<asp:imagebutton id="ResetPassword" runat="server" imageurl="/PaymentToolimages/btn_resetpassword.gif" ResetPassword_onclick="ResetPassword_onclick" visible="False" />
											</td-->
											<!--RFC 185138 - AD Integration CH1 End - Commented the reset password button in user administration page -->
											<td align="middle" width="108" height="22">
												 <!-- External URL Implementation-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
                                            <asp:imagebutton id="Delete" runat="server" imageurl="/PaymentToolimages/btn_deleteuser.gif" Delete_onclick="Delete_onclick" visible="False" />
											</td>
											<td width="65"></td>
											<td align="middle" width="70" height="22">
												 <!-- External URL Implementation-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
                                            <asp:imagebutton id="Cancel" runat="server" imageurl="/PaymentToolimages/btn_cancel.gif" Cancel_onclick="Cancel_onclick" />
											</td>
											<!--MAIG - CH3 - BEGIN - Removed the valign=top property-->
											<td align="middle" width="110" height="22">
											<!--MAIG - CH3 - END - Removed the valign=top property-->
												 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
                                                 <asp:imagebutton id="Update" runat="Server" imageurl="/PaymentToolimages/btn_updateuser.gif" Update_onclick="Update_onclick" causesvalidation="True" visible="False" />
												<asp:imagebutton id="AddUser" runat="Server" imageurl="/PaymentToolimages/btn_continue.gif" AddUser_onclick="Update_onclick" causesvalidation="True" />
											 <!-- External URL Implementation-END-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
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
</html>
