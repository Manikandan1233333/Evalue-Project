<!-- MAIG - CH1 - Removed the title verbiage 'Sales & Service' -->
<%@ Page CodeBehind="general_error.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="MSC.Forms.general_error" %>
<HTML>
	<HEAD>
		<!-- MAIG - CH1 - BEGIN - Removed the title verbiage 'Sales & Service' -->
		<title>Payment Tool</title>
		<!-- MAIG - CH1 - END - Removed the title verbiage 'Sales & Service' -->
	</HEAD>
	<body>
		<center>
			<table width="750" cellpadding="0" cellspacing="0">
				<tr>
					<td colspan="2" height="10"></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td align="left" class="arial_15">New Member &gt;&nbsp;General Error&nbsp;
					</td>
				</tr>
				<tr>
					<td colspan="2" height="10"></td>
				</tr>
			</table>
			<!-- 3 tables: space:30, information box table:415, space:305 -->
			<table width="750" border="0" cellpadding="0" cellspacing="0">
				<tr>
					<!-- space to left of information box -->
					<td width="30">
						<table cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td width="30">
								</td>
							</tr>
						</table>
					</td>
					<!-- search table -->
					<td>
						<table width="415" height="200" cellpadding="0" cellspacing="0" border="1" rules="none" bgcolor="#ffffcc">
							<tr>
								<td class="arial_11_bold_white" bgcolor="#333333" colspan="2" height="22">
									&nbsp;&nbsp;Contact Administrator Information</td>
							</tr>
							<tr>
								<td colspan="2" height="30">
								</td>
							</tr>
							<tr>
								<td align="middle" valign="center" width="32">
									 <!-- External URL Implementation-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
                                <img src="/PaymentToolimages/red_arrow.gif" width="22" height="17"></td>
								<td colspan="3" width="428" class="arial_12_bold_red" align="left">An error has 
									occurred.</td>
							</tr>
							<tr>
								<td bgcolor="#ffffcc" width="110" valign="top"></td>
								<td width="305" class="arial_12_bold">Contact Administrator:&nbsp;<br>
								</td>
							</tr>
							<!-- 10/21/2005 - Changes made as part of Q4 CSR - START : Removed the existing contact and update the new contact details -->
							<tr>
								<td bgcolor="#ffffcc" width="110"></td>
								<td bgcolor="#ffffcc" class="arial_12" valign="top"><span class="arial_12_bold"><asp:label id="Name" runat="server" />
									</span><br>
									<asp:label id="Phone" runat="server" /><br>
								</td>
							</tr>
							<!-- 10/21/2005 - Changes made as part of Q4 CSR - END : Removed the existing contact and update the new contact details -->
						</table>
					</td>
					<!-- space to right of search table -->
					<td>
						<table width="305" cellpadding="0" cellspacing="0">
							<tr>
								<td width="305">
								</td>
							</tr>
						</table>
					</td>
					<!-- end of step 2 table -->
				</tr>
			</table>
		</center>
	</body>
</HTML>
