<!-- MAIG - CH1 - Modified the width from 520 to 620  -->
<!-- MAIG - CH2 - Modified the image URL forward slash with backward slash url-->
<!-- MAIG - CH3 - Modified the image URL forward slash with backward slash url-->
<!-- MAIG - CH4 - Added the code to display the Recurring Button on payment confirmation -->
<!-- MAIG - CH5 - Modified the image URL forward slash with backward slash url-->
<%@ Page language="c#" Codebehind="Confirm.aspx.cs" AutoEventWireup="True" Inherits="MSC.Forms.Confirm" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<%@ Register TagPrefix="uc" TagName="Summary" Src="../Controls/OrderSummary.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<!-- //67811A0  - PCI Remediation for Payment systems CH1: - Change Order Summary to Payment Summary-->
<HTML>
	<HEAD>
		<title>Confirm</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
		<!-- Start Added by Cognizant on 05/24/2004 for calling a javascript function which open's the receipt page-->
		<script language="javascript">
			function Open_Receipt(ReceiptNumber, ReceiptType)
			{
//			      External URL Implementation-Modified the below  code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  
			      
			      var ReceiptURL="../../PaymentToolmsc/Forms/Receipt.aspx?ReceiptNumber=" +ReceiptNumber + "&ReceiptType=" + ReceiptType;
			window.open(ReceiptURL,"Receipt");
				return false;
			}
		</script>
		<!--END-->
	</HEAD>
	<body ms_positioning="GridLayout">
		<form id="Confirm" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td colSpan="2" height="10"></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td class="arial_15" align="left">Payment Confirmation
					</td>
				</tr>
				<tr>
					<td colSpan="2" height="10"></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td>
						<table borderColor="#333333" cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td>
									<table cellSpacing="0" cellPadding="0" bgColor="#ffffcc" border="0">
										<tr>
											<td colSpan="3" height="5"></td>
										</tr>
										<tr>
											<td vAlign="top"><IMG src="../images/red_arrow.gif"></td>
											<td width="10"></td>
											<td class="arial_11_bold" vAlign="top">Success!<br>
												<asp:label id="SuccessLabel" runat="server"></asp:label>
											&nbsp;&nbsp;</td>
										</tr>
										<tr>
											<td colSpan="3" height="5"></td>
										</tr>
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td bgColor="#ffffff" colSpan="2" height="30"></td>
				</tr>
			</table>
			<!-- step 2 table contains 3 tables: space:30, form table:520, space:200 -->
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<!-- space to left of form table-->
					<td width="30">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td width="30"></td>
							</tr>
						</table>
					</td>
					<!-- form table -->
					<td width="520">
						<table cellSpacing="0" cellPadding="0" width="520" border="0">
						<!-- //67811A0  - PCI Remediation for Payment systems CH1: - Change Order Summary to Payment Summary-->
							<tr>
								<td height="100"><uc:summary id="Summary" title="Payment Summary" runat="server"></uc:summary></td>
							</tr>
						<!-- //67811A0  - PCI Remediation for Payment systems CH1: - Change Order Summary to Payment Summary-->
							<tr>
								<td bgColor="#ffffff" height="20"></td>
							</tr>
						</table>
						<%--CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the MembershipContact.ascx User control code as part of the Code Clean Up- March 2016--%>
					</td>
					<!-- space to right of form table -->
					<td width="200">
						<table cellSpacing="0" cellPadding="0" width="200">
							<tr>
								<td width="200"></td>
							</tr>
						</table>
					</td>
					<!-- end of step 2 table --></tr>
			</table>
			<table id="MemberCard" cellSpacing="0" cellPadding="0" width="750" border="0" runat="server" visible="false">
				<tr>
					<td width="30">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td width="30"></td>
							</tr>
						</table>
					</td>
					<td width="520">
						<!--Added by Cognizant on 07/20/2004 
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td colSpan="2" height="30"></td>
							</tr>
						</table>-->
						<table id="tdCard"  cellSpacing="0" cellPadding="0" width="520" bgColor="#ffffff" border="0"  runat="server">
							<tr>
								<td colSpan="2" height="30"></td>
							</tr>
							<tr>
								<td class="arial_12_bold" colSpan="2">Temporary Membership Card(s)</td>
							</tr>
							<tr>
								<td width="286" bgColor="#999999" height="4" style="WIDTH: 286px"></td>
								<td width="320"></td>
							</tr>
							<tr>
								<td colSpan="2" height="10"></td>
							</tr>
							<tr>
								<td class="arial_11_bold" colSpan="2">Primary Member</td>
							</tr>
							<tr>
								<td class="arial_11" colSpan="2">This temporary card can be used until the real 
									card arrives.</td>
							</tr>
							<tr>
								<td></td>
								
								<td class="arial_11" align="middle"><strong> Conditions of Membership </strong>
								</td>
								
							</tr>
							<tr>
								<%--CHGXXXXXX - Removing the MembershipUserControl as part of Code Clean Up--%>
							
								<td>
									<table border="1" bordercolor="#ffffff">
										<tr>
											<td></td>
											<td class="arial_10" bordercolor="#000000">
												When this temporary Membership Card is signed by the applicant it shall 
												constitute an application for a primary membership card (and one or 
												more&nbsp;&nbsp; associate memberships if so indicated) in the California State 
												Automobile Association, also doing business as AAA Nevada and AAA Utah. Member 
												benefits are subject to change. The applicant and associate member(s) agree to 
												abide by the Association's articles of incorporation, by-laws, rules and 
												regulations. Dues for a primary membership include $2.00 for an annual 
												subscription to VIA.&nbsp;&nbsp;
												<Br>
												The applicant and associate member(s) consent to the right of the Association 
												to terminate their respective memberships in accordance with the by-laws and 
												the rules and regulations adopted thereunder. In the event of such termination 
												the members agree to forthwith surrender and return their membership 
												credentials to the Association.</td>
										</tr>
									</table>
								</td>
								
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr height="10">
								<td colspan="2" >&nbsp;</td>
							</tr>
							<tr>
								<td vAlign="center"><asp:hyperlink id="AssociateCards1" runat="server" cssclass="black_navigation">
									<img src="../images/red_arrow.gif" border="0">&nbsp;</asp:hyperlink></td>
								<td><asp:hyperlink id="AssociateCards2" runat="server" cssclass="black_navigation">
									Click here to view All Temporary 
									Membership cards</asp:hyperlink></td>
							</tr>
						</table>
						<br>
						<!--START -Changed by Cognizant on 05/20/2004 for creating Receipt links for Membership Card-->
						<table id="MemberReceiptCard" bgColor="#ffffcc" style="WIDTH: 520px; HEIGHT: 80px" cellSpacing="0" cellPadding="0" width="520" border="3" runat="server">
							<tr>
								<td align="middle" class="arial_13_bold" height="30" colspan="3" bordercolor="#ffffcc">
									Please print out copies of both receipts:
								</td>
							</tr>
							<tr>
								<td borderColor="#ffffcc" align="right" height="20">
									<asp:ImageButton runat="server" id="ibMemberCustomerReceipt" ImageUrl="..\images\btn_cust_recpt.gif" imagealign="Middle" tooltip="Member Customer Receipt">
									</asp:ImageButton> 
								</td>
								<td borderColor="#ffffcc" width="20"></td>
								<td borderColor="#ffffcc">
									<asp:ImageButton runat="server" id="ibMemberMerchantReceipt" ImageUrl="..\images\btn_mer_recpt.gif" imagealign="Middle" tooltip="Member Merchant Receipt">
									</asp:ImageButton>
									
								</td>
							</tr>
						</table>
						<!--END-->
						<table width="520">
							<tr>
								<td height="100" align="middle">
									<asp:hyperlink id="DoAnotherMembership" runat="server">
										<img src="..\images\btn_doanother.gif" border="0"></asp:hyperlink>
										
								</td>
							</tr>
						</table>
					</td>
					<td width="200">
						<table width="200" cellpadding="0" cellspacing="0">
							<tr>
								<td width="200"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table id="InsuranceCard" cellSpacing="0" cellPadding="0" width="750" border="0" runat="server" visible="false">
				<tr>
					<td width="30">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td width="30"></td>
							</tr>
						</table>
					</td>
					<td width="520">
						<!--START -Changed by Cognizant on 05/20/2004 for creating Receipt links for Insurnace Card-->
						<!-- MAIG - CH1 - BEGIN - Modified the width from 520 to 620  -->
						<table id="InsuranceReceiptCard" bgColor="#ffffcc" style="WIDTH: 520px; HEIGHT: 80px" cellSpacing="0" cellPadding="0" width="620" border="3" runat="server">
						<!-- MAIG - CH1 - END - Modified the width from 520 to 620  -->
							<tr>
								<td class="arial_13_bold" borderColor="#ffffcc" align="middle" colSpan="3" height="20">Please 
									print out copies of both receipts:
								</td>
							</tr>
							<tr>
								<td borderColor="#ffffcc" align="right" height="20">
									<!-- MAIG - CH2 - BEGIN - Modified the image URL forward slash with backward slash url-->
									<asp:ImageButton runat="server" id="ibInsuranceCustomerReceipt" ImageUrl="../images/btn_cust_recpt.gif" imagealign="Middle" tooltip="Insurance Customer Receipt">
									</asp:ImageButton> 
									<!-- MAIG - CH2 - END - Modified the image URL forward slash with backward slash url-->
								</td>
								<td borderColor="#ffffcc" width="20"></td>
								<td bordercolor="#ffffcc">
									<!-- MAIG - CH3 - BEGIN - Modified the image URL forward slash with backward slash url-->
									<asp:ImageButton runat="server" id="ibInsuranceMerchantReceipt" ImageUrl="../images/btn_mer_recpt.gif" imagealign="Middle" tooltip="Insurance Merchant Receipt">
									</asp:ImageButton>
									<!-- MAIG - CH3 - END - Modified the image URL forward slash with backward slash url-->
								</td>
							</tr>
                            <!-- MAIG - CH4 - BEGIN - Added the code to display the Recurring Button on payment confirmation -->
							<tr>
							<td class="arial_13_bold" align="center" borderColor="#ffffcc" colspan="3">
						  <table id="RecurringRedirection" bgColor="#ffffcc" visible="false" runat="server">
							<tr>
							<td id="tdRedirection" class="arial_13_bold" align="center" borderColor="#ffffcc" colSpan="3" height="20">Please Use this information to enroll in recurring for future payments <asp:ImageButton runat="server" id="btn_ManageRecurringRedirection" imageurl="/PaymentToolimages/btn_ManageRecurring.gif" onclick="imgManageRecurring_Click" tooltip="MANAGE RECURRING"></asp:ImageButton></td>
							</tr>
							</table>
							</td>
							</tr>
							<!-- MAIG - CH4 - END - Added the code to display the Recurring Button on payment confirmation -->
						</table>
						<table width="520">
							<tr>
								<td height="100" align="middle">
									<!-- MAIG - CH5 - BEGIN - Modified the image URL forward slash with backward slash url-->
									<asp:hyperlink runat="server" id="DoAnotherInsurance">
										<img src="../images/btn_doanothertrans.gif" border="0">
									</asp:hyperlink>
									<!-- MAIG - CH5 - END - Modified the image URL forward slash with backward slash url-->
								</td>
							</tr>
						</table>
						<!--END-->
					</td>
					<td width="200">
						<table width="200" cellpadding="0" cellspacing="0">
							<tr>
								<td width="200"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
