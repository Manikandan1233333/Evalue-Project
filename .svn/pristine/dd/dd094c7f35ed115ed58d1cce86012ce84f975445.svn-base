<!-- MAIG - CH1 - Modified the verbiage by removing the Sales & Service -->
<!-- MAIG - CH2 - Commented the below table and modified the next table to display the new logo for Payment Tool -->
<!-- MAIG - CH3 - Added spacing to align properly -->
<%@ Page CodeBehind="login.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="MSCR.Forms.login" %>
<%@ Register TagPrefix="uc" TagName="PageValidator" src="../Controls/PageValidator.ascx" %>

<!--SSO Integration -CH1-Commented the Validator,UserName and Password Textboxes-->
<!--SSO Integration -CH2-Added a Label to display the error messgae-->
<HTML>
	<HEAD>
		<!-- MAIG - CH1 - BEGIN - Modified the verbiage by removing the Sales & Service -->
		<title>Payment Tool</title>
		<!-- MAIG - CH1 - END - Modified the verbiage by removing the Sales & Service -->
		<link href="../style.css" type="text/css" rel="stylesheet">	    
	</HEAD>
	<body>
		<form id="frmLogin" name="form1" method="post" autocomplete="Off" runat="server">
			<center>
			<uc:pagevalidator runat="server" colspan="3" id="PageValidator1" />
				<!-- header -->
				<!-- MAIG - CH2 - BEGIN - Commented the below table and modified the next table to display the new logo for Payment Tool -->
				<%--<table cellspacing="0" cellpadding="0" width="750" border="0">
					<tr>
						<td width="64"></td>
						<td width="622"><img height="53" src="../images/ss_logo.gif" width="622"></td>
						<td width="64" bgcolor="#000099"></td>
					</tr>
				</table>--%>
				<table cellspacing="0" cellpadding="0" width="750" border="0">
					<tr>
                        <td width="64">
                        </td>
                        <td width="420" bgcolor="#000099" height="59">
                            <img height="59" src="../images/ss_logo.gif" width="420">
                        </td>
                        <td width="324" bgcolor="#000099" height="59">
				<!-- MAIG - CH2 - END - Commented the below table and modified the next table to display the new logo for Payment Tool -->
                        </td>
                    </tr>
				</table>
				<!-- end of header -->

				<table cellspacing="0" cellpadding="0" width="750" border="0">
					<TBODY>
						<tr>
						</tr>
						<tr>
						<!-- MAIG - CH3 - BEGIN - Added spacing to align properly -->
						<td width="182" height="50"></td>
						</tr>
						<tr>
						<!-- MAIG - CH3 - END - Added spacing to align properly -->
						<td width="182"></td>
							<td colspan="2" height="20">
							<!--SSO Integration -CH2-START-Added a Label to display the error messgae-->
							
							<asp:Label ID="Error_Message" runat="server" 
                                    Text="" Visible="False" 
                                Font-Bold="True" Font-Size="Medium"></asp:Label></td>
                                
                                <!--SSO Integration -CH2-END-Added a Label to display the error messgae-->
						</tr>
						<!-- 10/24/2005 - Changes made as part of Q4 CSR - START : Removed the existing contact details and update the new contact details -->
						<tr>
						<td width="182">&nbsp;</td>
							<td colspan="2" height="20">&nbsp;</td>
						</tr>
						<tr>
						<td width="182">&nbsp;</td>
							<td colspan="2" height="20">&nbsp;</td>
						</tr>
						<tr>
							<td width="179"></td>
							<td class="arial_12_bold" width="393">
							&nbsp;&nbsp;For Technical or Application Support
							<td width="178"></td>
						</tr>
						<tr>
							<td width="179"></td>
							<td class="arial_11" width="393">&nbsp;
								<asp:label id="lblPhone" text="1-877-554-2911" runat="server" width="161px"></asp:label>
							</td>
							<td width="178"></td>
						</tr>
						<!-- 10/24/2005 - Changes made as part of Q4 CSR - END : Removed the existing contact and update the new contact details --></center>
			</TBODY></TABLE>
			</center>
		</form>
	</body>
</HTML>
