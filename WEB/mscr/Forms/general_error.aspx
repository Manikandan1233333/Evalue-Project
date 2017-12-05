<%--67811A0 - PCI Remediation for Payment systems CH1: Modified General Error Message --%>
<!-- MAIG - CH1 - Modified the verbiage by removing the Sales & Service -->
<%@ Page CodeBehind="general_error.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="MSCR.Forms.general_error" %>
<HTML>
	<HEAD>
	<!-- MAIG - CH1 - BEGIN - Modified the verbiage by removing the Sales & Service -->
		<title>Payment Tool</title>
	<!-- MAIG - CH1 - END - Modified the verbiage by removing the Sales & Service -->
	</HEAD>
	<body>
		<table width="750" cellpadding="0" cellspacing="0">
			<tr>
				<td width="30"></td>
				<!-- 67811A0 - PCI Remediation for Payment systems CH1: Modified General Error Message -->
				<td align="left" class="arial_15">&nbsp;General Error&nbsp;
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
							<td class="arial_11_bold_white" bgcolor="#333333" colspan="4" height="22">
								&nbsp;&nbsp;System Error</td>
						</tr>
						<!-- 67811A0 - PCI Remediation for Payment systems CH1: Modified General Error Message
						<tr>
							<td colspan="2" height="30">
							</td>
						</tr>
						 -->
						<tr>
							<td align="middle" valign="center" width="32">
							 <!-- External URL Implementation-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
							 	<img src="/PaymentToolimages/red_arrow.gif" width="22" height="17"></td>
								<!-- 67811A0 - PCI Remediation for Payment systems CH1: Modified General Error Message -->
							<td colspan="3" width="428" class="arial_12_bold_red" align="left">An error has occurred. Please contact IT Service desk at <br />1-877-554-2911. Please provide the Policy number and any <br />other details that may help to resolve the incident sooner.</td>
						</tr>
						<!-- 67811A0 - PCI Remediation for Payment systems CH1: Modified General Error Message - Start
						<tr>
							<td bgcolor="#ffffcc" width="110" valign="top"></td>
							<td width="305" class="arial_12_bold">Contact Administrator:</td>
						</tr>
					    10/24/2005 - START - Code Added for updating the contact details in the General Error Page 
						<tr>
							<td width="110" bgColor="#ffffcc"></td>
							<td class="arial_12" vAlign="top" bgColor="#ffffcc"><span class="arial_12_bold">For Technical or Application Support</span><br>
								1-877-554-2911
							</td>
						</tr>
						67811A0 - PCI Remediation for Payment systems CH1: Modified General Error Message - End -->
						<!-- END - Code Added for updating the contact details in the General Error Page -->
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
	</body>
</HTML>
