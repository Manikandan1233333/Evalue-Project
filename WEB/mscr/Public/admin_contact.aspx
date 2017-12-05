<!-- MAIG - CH1 - Modified the verbiage by removing the Sales & Service -->
<!-- MAIG - CH2 - Modified the verbiage by removing the Sales & Service -->
<%@ Page CodeBehind="admin_contact.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="MSCR.Admin.admin_contact" %>
<HTML>
    
	<HEAD>
		<!-- MAIG - CH1 - BEGIN - Modified the verbiage by removing the Sales & Service -->
		<title>Payment Tool</title>
		<!-- MAIG - CH1 - END - Modified the verbiage by removing the Sales & Service -->
	</HEAD>
	<body>
		<form  id="frmAdminContact" runat="server" >
			<table width="750" cellpadding="0" cellspacing="0" runat="server">
				<!-- MAIG - CH2 - BEGIN - Modified the verbiage by removing the Sales & Service -->
				<!--
				<tr>
					<td width="30"></td>
					<td align="left" class="arial_15">Payment Tool &gt; Contact 
						Administrator
					</td>
				</tr>
                -->
                <!-- MAIG - CH2 - END - Modified the verbiage by removing the Sales & Service -->
				<tr>
					<td colspan="2" height="10"></td>
				</tr>
			</table>
			<!-- 3 tables: space:30, information box table:415, space:305 -->
			<table width="750" border="0" cellpadding="0" cellspacing="0" runat="server">
				<tr>
					
					<td width="30">
						<table cellpadding="0" cellspacing="0" border="0">
							<tr>
								<td width="30">
								</td>
							</tr>
						</table>
					</td>
					
					<td>
						<table width="415" height="200" cellpadding="0" cellspacing="0" border="0" rules="none" bgcolor="#ffffcc" >
							<tr>
								<td class="arial_11_bold_white" bgcolor="#333333" colspan="2" height="22">
									&nbsp;&nbsp;Contact Administrator Information</td>
							</tr>
							<tr>
								<td colspan="2" height="30">
								</td>
							</tr>
							<tr>
								<td bgcolor="#ffffcc" width="110" valign="top"></td>
								<td width="305" class="arial_12_bold">Contact Administrator:</td>
							</tr>
							
							<tr>
								<td width="110" bgColor="#ffffcc"></td>
								<td class="arial_12" vAlign="top" bgColor="#ffffcc"><span class="arial_12_bold">For Technical or Application Support</span><br>
									1-877-554-2911</td>
							</tr>
							</table>
					
			        </td> 
                
			<td>
				<table width="305" cellpadding="0" cellspacing="0" >
					<tr>
						<td width="305">
						</td>
					</tr>
				</table>
			</td>
			
			</tr> 
                <!-- end of step 2 table -->
			</table>
		</form>
	</body>
</HTML>
