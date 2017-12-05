<!-- .NetMig.Ch:Added ValidateRequest = "false" to avoid run time error during membership renewal -->
<%--CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removed the button click events as part of code Clean Up - March 2016 --%>
<%@ Page CodeBehind="confirmation.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="MSCR.Forms.confirmation" ValidateRequest="false" %>
<html>
	<head>
		<title>Sales &amp; Service Payment Tool</title>
	</head>
	<body>
		<form runat="server" name="frmConfirmation" method="post">


			<!-- 3 tables: space:30, 460, space:260 -->
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
					<td width="460">
						<table width="460" cellpadding="0" cellspacing="0" border="1" rules="none" bgcolor="#ffffcc">
							<tbody>
								<tr>
									<td height="5" colspan="4"></td>
								</tr>
								<tr>
									<td align="middle" valign="center" width="32">
										 <!-- External URL Implementation-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
										 <img src="/PaymentToolimages/red_arrow.gif" width="22" height="17"></td>
									<td colspan="3" width="428" class="arial_12_bold_red" align="left">Success!</td>
								</tr>
								<tr>
									<td height="15" colspan="4"></td>
								</tr>
								<tr>
									<td colspan="4" class="arial_12_bold">
										&nbsp;&nbsp;The membership has been renewed and the credit card has been 
										billed.<br>
										<br>
										&nbsp;&nbsp;The credit card was billed:
										<asp:label id="lblAmount" runat="server"></asp:label>
									</td>
								</tr>
								<tr>
									<td height="15" colspan="4"></td>
								</tr>
				</tr>
				<tr>
					<td colspan="4">
						<table cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td height="5" bgcolor="#cccccc"></td>
							</tr>
							<tr>
								<!--<td colspan="3" bgcolor="#cccccc" width="250" height="17" align="right" valign="bottom">
									<asp:imagebutton id="btnLogout" runat="server" imageurl="/PaymentToolimages/btn_logout.gif" width="72" height="17"></asp:imagebutton>
								</td>-->
								<td bgcolor="#cccccc" width="460" height="17" align="right" valign="bottom">
									 <!-- External URL Implementation-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
									<%--CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removed the button click events as part of code Clean Up - March 2016 --%>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			</TD> 
			<!-- space to right of search table -->
			<td>
				<table width="260" cellpadding="0" cellspacing="0">
					<tr>
						<td width="260">
						</td>
					</tr>
				</table>
			</td>
			<!-- end of step 2 table --> </TR></TBODY></TABLE>
		</form>
	</body>
</html>
