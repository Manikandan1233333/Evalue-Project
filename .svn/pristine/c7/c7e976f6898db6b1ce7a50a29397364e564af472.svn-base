<%@ Page language="c#" Codebehind="Receipt.aspx.cs" AutoEventWireup="True" Inherits="MSC.Reports.Receipt" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<!-- 
Revision: PT.Ch2
Modified by COGNIZANT on 8/25/2008 for Premier Membership
These changes have been performed to fix the defect # 97 (Payment Tool) / #694 (SalesX)
This fix makes sure that the rate descriptions are displayed correctly for promotion codes 
that offer discounts on primary & associate fees also.
The discounts are displayed as "Premier Enrollment (Promo rate)" or "Premier Primary (Promo rate)" or "Premier Associate (Promo rate)"
This is applicable for Classic and Plus memberships also.
1) Removed the label: lblPromo which was used to display the text (PROMOTION) when enrollment fee is waived
2) Changed the widths of the enrollment, primary, associate rows and the sub-total rows to align the text and the amounts
Modified by COGNIZANT on 2/11/2011 for Name change project.
1)Changed the text CSAA to AAA in tblLogoTitle tab
Old text: California State Automobile Association Inter-Insurance Bureau
New text: AAA Northern California, Nevada & Utah Insurance Exchange
* 05/03/2011 RFC#135298 PT_ececk CH1:Added the control to display the bank account no and the routing number in the receipt page by cognizant.
*05/06/2011 RFC#135298 PT_echeck CH2: Added the authorization text ,signature  and date column space in the receipt which needs to be displayed for echeck payment merchant receipt by cognizant.
//67811A0  - PCI Remediation for Payment systems CH1: - Change Order Summary to Payment Summary
//CHG0083477 - Name Change - Header for Receipt is removed.
MAIG - CH1 - Added the code to remove the Sales Rep Number and modified the alignment of the td
MAIG - CH2 - Added the code to change the Receipt Verbiage
CHG0109406 - CH1 - Change to update ECHECK to ACH - 01202015
PRB0048070 – Remove Authorization Code From Printed and Reprinted Payment Receipts - 04202017
-->
<HTML>
	<HEAD>
		<title>Receipt</title>
		<meta content="True" name="vs_showGrid">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
		<!-- PT.Ch1 - START: Added by COGNIZANT - 7/01/2008 - To enable default printing of the Print Version -->
		<script language="javascript">
		    window.print();
		</script>
		<!-- PT.Ch1 - END -->
	    <style type="text/css">
            .style1
            {
                width: 90px;
            }
            .style2
            {
                font-family: arial, helvetica, sans-serif;
                color: #000000;
                font-size: 12px;
                font-weight: bold;
                text-decoration: none;
                width: 137px;
            }
            .style3
            {
                width: 684px;
            }
            .style4
            {
                width: 680px;
            }
            .style5
            {
                font-family: arial, helvetica, sans-serif;
                color: #000000;
                font-size: 14px;
                font-weight: bold;
                text-decoration: none;
                width: 680px;
            }
            .style8
            {
                width: 104%;
            }
            #tbecheckmerchantcopy
            {
                height: 165px;
            }
            .style9
            {
                font-family: arial, helvetica, sans-serif;
                color: #000000;
                font-size: 12px;
                font-weight: bold;
                text-decoration: none;
                height: 75px;
            }
            .style10
            {
                font-family: arial, helvetica, sans-serif;
                color: #000000;
                font-size: 14px;
                font-weight: bold;
                text-decoration: none;
                height: 44px;
            }
        </style>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Receipt" method="post" runat="server">
			<TABLE id="tblReceipts" cellSpacing="1" cellPadding="1" width="560" border="0" 
                class="style3">
				<TR>
					<TD class="style4">
						<TABLE id="tblLogoTitle" style="HEIGHT: 56px" cellSpacing="1" cellPadding="1" border="0">
							<TR>
								<TD class="arial_18_bold" style="WIDTH: 133px">
								      <!-- External URL Implementation-Modified the below  code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
								      <IMG src="/PaymentToolmsc/Images/recpt_aaa_logo.gif" border="0">
								</TD>
								<%--<TD class="arial_18_bold">CSAA Insurance Group<br>
									
								</TD>--%>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="style4">
						<TABLE id="tblReceiptType" cellSpacing="0" cellPadding="0" width="100%" border="0">
							<TR>
								<TD>
									<HR noShade SIZE="2">
								</TD>
							</TR>
							<TR>
								<TD class="arial_18_bold" align="middle">P A Y M E N T&nbsp;&nbsp;R E C E I P T
									<br>
									<asp:label id="lblReceiptType" runat="server" CssClass="arial_18_bold" ForeColor="Red"></asp:label></TD>
							</TR>
							<TR>
								<TD>
									<HR noShade SIZE="2">
								</TD>
							</TR>
							<TR>
								<TD height="18"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="style4">
						<TABLE cellSpacing="1" cellPadding="1" border="0">
							<TR>
							<!-- MAIG - CH1 - BEGIN - Added the code to remove the Sales Rep Number and modified the alignment of the td -->
								<!-- <TD class="arial_12_bold" width="240">Sales Rep Number
								</TD>
								<TD WIDTH="141" class="style1"><asp:label id="lblSalesRepNumber" runat="server" CssClass="arial_12" Width="90px" Height="11px">""</asp:label></TD> -->
								<TD class="arial_12_bold" width="200">&nbsp;&nbsp; Receipt Number
								</TD>
								<TD WIDTH="141" class="style1"><asp:label id="lblReceiptNumber" runat="server" CssClass="arial_12" Width="90px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="arial_12_bold" width="200">&nbsp;&nbsp;&nbsp;District Office Number
								</TD>
							<!-- MAIG - CH1 - END - Added the code to remove the Sales Rep Number and modified the alignment of the td -->	
								<TD WIDTH="141" class="style1"><asp:label id="lblDistrictOfficeNumber" runat="server" CssClass="arial_12" Width="90px" Height="16px">""</asp:label>
								</TD>
								<TD class="style2" width="253">&nbsp;&nbsp; Receipt Date and Time
								</TD>
								<TD ><asp:label id="lblReceiptDateTime" runat="server" CssClass="arial_12" Width="200px"></asp:label>
								</TD>
							</TR>
							<TR>
								<td height="50">
								</td>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="style4">
						<table cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0">
							<asp:repeater id="ReceiptsRepeater" runat="server" OnItemDataBound="ReceiptsRepeater_ItemDataBound">
								<HeaderTemplate>
								<!--//67811A0  - PCI Remediation for Payment systems CH1 Start - Change Order Summary to Payment Summary-->
									<tr class="item_template_header">
										<td height="20" colspan="2" align="left">&nbsp;&nbsp;Payment Summary</td>
									</tr>
							    <!--//67811A0  - PCI Remediation for Payment systems CH1: Change Order Summary to Payment Summary-->
									<tr class="item_template_header_white">
										<td width="80%" height="20" class="border_Down_left">&nbsp;&nbsp;Description</td>
										<td height="20" class="border_Down_right" align="right">Amount&nbsp;&nbsp;</td>
									</tr>
								</HeaderTemplate>
								<ItemTemplate>
									<tr class="item_template_row" ID="ReceiptRow" runat="server">
										<td align="left" class="border_Down_left" height="23">
											&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "ProductDescription") %>
											<%# DataBinder.Eval(Container.DataItem, "TransType") %>
											#<%# DataBinder.Eval(Container.DataItem, "PolicyNumber") %>
											<%# DataBinder.Eval(Container.DataItem, "FirstName")%>
											&nbsp;<%# DataBinder.Eval(Container.DataItem, "LastName")%>
										</td>
										<td align="right" class="border_Down_right">
											<asp:Label ID="lblAmount" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemAmount","{0:c}") %>' >
											</asp:Label>
											<asp:Label ID="lblItemId" Runat="server" Visible ="False" ItemNo= '<%# DataBinder.Eval(Container.DataItem, "ItemID") %>' ProductName = '<%# DataBinder.Eval(Container.DataItem, "ProductName") %>' AssocID= '<%# DataBinder.Eval(Container.DataItem, "AssocID") %>' TransType= '<%# DataBinder.Eval(Container.DataItem, "TransType") %>'>
											</asp:Label>&nbsp;
										</td>
									</tr>
									<tr class="item_template_row" ID="ReceiptMbrRow" runat="server" Visible="False">
										<td align="left" class="border_Down_left" height="23">
											&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "ProductDescription") %>
											<%# DataBinder.Eval(Container.DataItem, "TransType") %>
											#<%# DataBinder.Eval(Container.DataItem, "PolicyNumber") %>
											<%# DataBinder.Eval(Container.DataItem, "FirstName")%>
											&nbsp;<%# DataBinder.Eval(Container.DataItem, "LastName")%>
										</td>
										<td align="right" class="border_Down_right">
											<asp:Label ID="lblMbrTotal" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAmount","{0:c}") %>' >
											</asp:Label>&nbsp;&nbsp;
										</td>
									</tr>
									<tr class="item_template_row" ID="ReceiptEnrolRow" runat="server" Visible="False">
										<td align="right" class="border_left" height="23">
											<table width="90%">
												<tr>
													<td width="55%" class="arial_12"><!-- PT.Ch2 Increased width to 55% to accomodate new descriptions-->
														&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "EnrollmentType") %>
													</td>
													<td class="arial_12" align="right" width="15%"><!-- PT.Ch2 Increased width to 15% to right align all amounts-->
														<asp:Label ID="lblEnrolment" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EnrollmentFee","{0:c}") %>'>
														</asp:Label>
													</td>
													<td class="arial_12">
														&nbsp;
													</td>
												</tr>
											</table>
										</td>
										<td align="left" class="border_right">
											&nbsp;
										</td>
									</tr>
									<tr class="item_template_row" ID="ReceiptInRow" runat="server">
										<td align="right" class="border_left" height="23">
											<table width="90%">
												<tr>
													<td width="55%" class="arial_12"><!-- PT.Ch2 Increased width to 55% to accomodate new descriptions-->
														&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "RateType") %>
													</td>
													<td class="arial_12" align="right" width="15%"><!-- PT.Ch2 Increased width to 15% to right align all amounts-->
														<%# DataBinder.Eval(Container.DataItem, "ItemAmount","{0:c}") %>
													</td>
													<td class="arial_12">
														&nbsp;
													</td>
												</tr>
											</table>
										</td>
										<td align="left" class="border_right">
											&nbsp;
										</td>
									</tr>
								</ItemTemplate>
								<FooterTemplate>
									<tr class="item_template_row" ID="ReceiptSubRow" runat="server" Visible="False">
										<td align="right" class="border_Down_left" height="23">
											<table width="90%">
												<tr>
													<td width="55%" class="arial_12_bold"><!-- PT.Ch2 Increased width to 55% to align with the new descriptions-->
														&nbsp;&nbsp;Subtotal
													</td>
													<td class="arial_12_bold" align="right" width="15%"><!-- PT.Ch2 Increased width to 15% to right align all amounts-->
														<asp:Label ID="lblSubtotal" Runat="server"></asp:Label>
													</td>
													<td class="arial_12">
														&nbsp;
													</td>
												</tr>
											</table>
										</td>
										<td align="left" class="border_Down_right">
											&nbsp;
										</td>
									</tr>
									<tr class="item_template_header_white">
										<td align="left" class="border_Down_left" height="23">
											&nbsp;&nbsp;Total Paid
										</td>
										<td align="right" class="border_Down_right">
											&nbsp;
											<asp:Label ID="lblTotal" Runat="server" Visible="True"></asp:Label>&nbsp;&nbsp;
										</td>
									</tr>
								</FooterTemplate>
							</asp:repeater></table>
					</TD>
				</TR>
				<TR>
					<TD class="style4">
						<TABLE cellSpacing="1" cellPadding="1" width="100%" border="0" height="50">
							<TR>
								<TD class="arial_12_bold" WIDTH="139">Payment Method</TD>
								<TD><asp:label id="lblPaymentMethod" runat="server" CssClass="arial_12" Width="152px"></asp:label></TD>
							</TR>
						</TABLE>
						<TABLE id="tblCardDetails" cellSpacing="1" cellPadding="1" width="100%" border="0" runat="server">
							<TR>
								<TD class="arial_12_bold" WIDTH="139">Credit Card Type</TD>
								<TD WIDTH="461"><asp:label id="lblCreditCardType" runat="server" CssClass="arial_12" Width="64px" Height="16px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="arial_12_bold" WIDTH="139">Credit Card Number</TD>
								<TD WIDTH="461"><asp:label id="lblCreditCardNumber" runat="server" CssClass="arial_12" Width="64px" Height="16px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="arial_12_bold" WIDTH="139">Expiration Date</TD>
								<TD WIDTH="461"><asp:label id="lblExpirationDate" runat="server" CssClass="arial_12" Width="64px" Height="16px"></asp:label></TD>
							</TR>
                            <%--PRB0048070 – CH1- Remove Authorization Code From Printed and Reprinted Payment Receipts - start--%>
							<%--<TR>
								<TD colSpan="2">
									<HR noShade SIZE="2">
								</TD>
							</TR>--%>
							<TR>
								<%--<TD class="arial_12_bold" style="WIDTH: 150px; HEIGHT: 28px">Authorization Code</TD>--%>
								<TD><asp:label id="lblAuthorizationCode" runat="server" CssClass="arial_12" Width="64px" Height="16px" Visible="false"></asp:label></TD>
							</TR>
							<%--<TR>
								<TD colSpan="2">
									<HR noShade SIZE="2">
								</TD>
							</TR>--%>
                            <%--PRB0048070 – CH1- Remove Authorization Code From Printed and Reprinted Payment Receipts - end--%>
						</TABLE>
					<!--RFC#135298 PT_ececk CH1:Added the control to display the bank account no and the routing number in the receipt page by cognizant on 03/16/2011.-->
						<TABLE id="tbleCheckDetails" cellSpacing="1" cellPadding="1" width="100%" border="0" runat="server">
							<TR>
								<TD class="arial_12_bold" WIDTH="139">Routing Number</TD>
								<TD WIDTH="461"><asp:label id="lblBankId" runat="server" CssClass="arial_12" Width="64px" Height="16px"></asp:label></TD>
							</TR>
							<TR>
								<TD class="arial_12_bold" WIDTH="139">Account Number</TD>
								<TD WIDTH="461"><asp:label id="lblACNo" runat="server" CssClass="arial_12" Width="64px" Height="16px"></asp:label></TD>
							</TR>
							<TR>
								<TD colSpan="2">
									<HR noShade SIZE="2">
								</TD>
							</TR>
						</TABLE>
						<!--RFC#135298 PT_ececk CH1:Added the control to display the bank account no and the routing number in the receipt page by cognizant on 03/16/2011.-->
						<!--05/06/2011 RFC#135298 PT_echeck CH2: Added the authorization text ,signature  and date column space in the receipt which needs to be displayed for echeck payment merchant receipt by cognizant.-->
					<!-- CHG0109406 - CH1 - BEGIN - Change to update ECHECK to ACH - 01202015 -->
					<TABLE id="tbecheckmerchantcopy" cellSpacing="1" cellPadding="1" width="100%" border="0" runat="server">
							<TR>							
								<TD class="arial_14_bold" colspan="2" align ="center">One Time ACH Payment Authorization</TD>							    						
							</TR>
							<TR>
								<TD class="style9" colspan="2"> I authorize CSAA Insurance Group to make a one-time debit to my checking or savings account.  This is permission for a single transaction only, and does not provide authorization for any additional unrelated debits or credits to your account. I understand that because this is an electronic transaction, these funds may be withdrawn from my account as soon as the above noted transaction date.</TD>
								
							</TR>
							
								<TR>
								<TD class="style10"  align ="left">SIGNATURE :_________________________</TD>								
								
								<TD class="style10"  align ="left">DATE :_____________________ </TD>								
							
							</TR>
							<TR>
								<TD colSpan="2" class="style8">
									<HR noShade SIZE="5" style="height: -9px">
								</TD>
								</TR>
						</TABLE>
					<!-- CHG0109406 - CH1 - END - Change to update ECHECK to ACH - 01202015 -->	
				</TR>
				<TR>
				<!-- MAIG - CH2 - BEGIN - Added the code to change the Receipt Verbiage -->
					<TD align="center" class="style5" height="80px">
						<!-- <I>Thank you very much for your payment</I> -->
						<I>Thank you for choosing AAA Insurance</I>
					</TD>
				<!-- MAIG - CH2 - END -  Added the code to change the Receipt Verbiage -->
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
