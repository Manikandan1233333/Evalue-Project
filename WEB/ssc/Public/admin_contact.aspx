<%@ Page CodeBehind="admin_contact.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="ssc.Admin.admin_contact" %>
<html>
	<head>
		<title>Sales &amp; Service Payment Tool</title>
		<link rel="stylesheet" type="text/css" href="../style.css">
	</head>
	<body>
		<form runat="server" id="frmAdminContact">
			<table width="750" cellpadding="0" cellspacing="0">
				<tr>
					<td colspan="2" height="10"></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td align="left" class="arial_15">Straight Through Renewal &gt; Contact 
						Administrator
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
							<!-- START MODIFIED BY COGNIZANT ON 10/26/2005 - AS PART OF Q4 CSR - Updating the Contact Details -->
							<tr>
								<td bgcolor="#ffffcc" width="110" valign="top"></td>
								<td width="305" class="arial_12_bold"><span class="arial_12_bold">For Technical or Application Support</span><br>
									1-877-554-2911</td>
							</tr>
							<!-- END MODIFIED BY COGNIZANT ON 10/26/2005 - AS PART OF Q4 CSR - Updating the Contact Details -->
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
		</form>
	</body>
</html>
