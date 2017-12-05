<%--CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Removing the MembershipCard.ascx User control code as part of the Code Clean Up- March 2016--%>
<%@ Page language="c#" Codebehind="Reissue_Confirmation.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.Reissue_Confirmation" %>
<HTML>
	<HEAD>
		<TITLE>Reissue Confirmation</TITLE>
		<LINK href="../Style.css" type="text/css" rel="stylesheet">
			<script language="javascript">
			function Open_Receipt(ReceiptNumber, ReceiptType)
			{
//			     External URL Implementation-Modified the below  code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach 
			      
			      var ReceiptURL="../../PaymentToolmsc/Forms/Receipt.aspx?ReceiptNumber=" +ReceiptNumber + "&ReceiptType=" + ReceiptType;
		 	window.open(ReceiptURL,"Receipt");
			return false;
			}
			</script>
	</HEAD>
	<body>
		<form id="Confirm" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td colSpan="2" height="10"></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td class="arial_15" align="left">Order Confirmation</td>
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
												<asp:label id="lblSuccessLabel" runat="server"></asp:label>.&nbsp;&nbsp;</td>
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
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td width="30"></td>
					<td>
						<table cellSpacing="0" cellPadding="0" width="421" bgColor="#ffffff" border="0">
							<asp:repeater id="ReceiptsRepeater" runat="server" OnItemDataBound="ReceiptsRepeater_ItemDataBound">
								<HeaderTemplate>
									<tr class="arial_11_bold" bgColor="#cccccc">
										<td width="421" height="15" colspan="2" align="left" class="border_up_Down_left_right">&nbsp;&nbsp;Order 
											Summary</td>
									</tr>
									<tr class="arial_11_bold">
										<td width="80%" height="15" class="border_left">&nbsp;&nbsp;Description</td>
										<td height="15" class="border_right" align="right">Amount&nbsp;&nbsp;</td>
									</tr>
								</HeaderTemplate>
								<ItemTemplate>
									<tr class="arial_11" ID="ReceiptRow" runat="server">
										<td align="left" class="border_left" height="15">
											&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "ProductName") %>
											<%# DataBinder.Eval(Container.DataItem, "TransType") %>
											#<%# DataBinder.Eval(Container.DataItem, "PolicyNumber") %>
											(<%# DataBinder.Eval(Container.DataItem, "FirstName")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "LastName")%>)
										</td>
										<td align="right" class="border_right">
											<asp:Label ID="lblAmount" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemAmount","{0:c}") %>' >
											</asp:Label>
											<asp:Label ID="lblItemId" Runat="server" Visible ="False" ItemNo= '<%# DataBinder.Eval(Container.DataItem, "ItemID") %>' ProductName = '<%# DataBinder.Eval(Container.DataItem, "ProductName") %>' AssocID= '<%# DataBinder.Eval(Container.DataItem, "AssocID") %>' TransType = '<%# DataBinder.Eval(Container.DataItem, "TransType") %>' FirstName = '<%# DataBinder.Eval(Container.DataItem, "FirstName") %>' MiddleName = '<%# DataBinder.Eval(Container.DataItem, "MiddleName") %>' LastName = '<%# DataBinder.Eval(Container.DataItem, "LastName") %>' Address1 = '<%# DataBinder.Eval(Container.DataItem, "Address1") %>' Address2 = '<%# DataBinder.Eval(Container.DataItem, "Address2") %>' City = '<%# DataBinder.Eval(Container.DataItem, "City") %>' State = '<%# DataBinder.Eval(Container.DataItem, "State") %>' ZipCode = '<%# DataBinder.Eval(Container.DataItem, "ZipCode") %>' DayPhone = '<%# DataBinder.Eval(Container.DataItem, "DayPhone") %>' Dob = '<%# DataBinder.Eval(Container.DataItem, "Dob") %>' PolicyNumber = '<%# DataBinder.Eval(Container.DataItem, "PolicyNumber") %>' MemberExpiryDate= '<%# DataBinder.Eval(Container.DataItem, "MemberExpiryDate") %>' MemberID = '<%# DataBinder.Eval(Container.DataItem, "MemberID") %>' >
											</asp:Label>&nbsp;
										</td>
									</tr>
									<tr class="arial_11" ID="trReceiptMbrRow" runat="server" Visible="False">
										<td align="left" class="border_left" height="15">
											&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "ProductName") %>
											<%# DataBinder.Eval(Container.DataItem, "TransType") %>
											#<%# DataBinder.Eval(Container.DataItem, "PolicyNumber") %>
											(<%# DataBinder.Eval(Container.DataItem, "FirstName")%>&nbsp;<%# DataBinder.Eval(Container.DataItem, "LastName")%>)
										</td>
										<td align="right" class="border_right">
											<asp:Label ID="lblMbrTotal" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAmount","{0:c}") %>' >
											</asp:Label>&nbsp;&nbsp;
										</td>
									</tr>
									<tr class="arial_11" ID="trReceiptEnrolRow" runat="server" Visible="False">
										<td width="80%" height="15" class="border_left">&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "EnrollmentType") %></td>
										<td height="15" class="border_right" align="right"><%# DataBinder.Eval(Container.DataItem, "EnrollmentFee","{0:c}") %>&nbsp;&nbsp;</td>
									</tr>
									<tr class="arial_11" ID="trReceiptInRow" runat="server">
										<td width="80%" height="15" class="border_left">&nbsp;&nbsp;<%# DataBinder.Eval(Container.DataItem, "RateType") %></td>
										<td height="10" class="border_right" align="right"><%# DataBinder.Eval(Container.DataItem, "ItemAmount","{0:c}") %>&nbsp;&nbsp;</td>
									</tr>
								</ItemTemplate>
								<FooterTemplate>
									<tr class="arial_11_bold">
										<td align="left" class="border_Down_left" height="15">
											&nbsp;&nbsp;Total
										</td>
										<td align="right" class="border_Down_right">
											&nbsp;
											<asp:Label ID="lblTotal" Runat="server" Visible="True"></asp:Label>&nbsp;&nbsp;
										</td>
									</tr>
								</FooterTemplate>
							</asp:repeater></table>
					</td>
					<td width="299"></td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td width="30"></td>
					<td>
						<table id="tblMember" cellSpacing="0" cellPadding="0" width="520" bgColor="#ffffcc" border="0" runat="server">
							<tr bgColor="#ffffff">
								<td colSpan="2" height="20"></td>
							</tr>
							<tr>
								<td class="arial_11_bold_white" bgColor="#333333" colSpan="2" height="22">&nbsp;&nbsp;Primary 
									Member Contact Information</td>
							</tr>
							<tr>
								<td class="arial_11"><asp:label id="lblName" runat="server">Label</asp:label><br>
									<asp:label id="lblAddress" runat="server">Label</asp:label><br>
									<asp:label id="lblCityStateZip" runat="server">Label</asp:label></td>
								<TD class="arial_11"><b>Day Phone:</b>&nbsp;<asp:label id="lblPhone" runat="server">Label</asp:label><br>
									<b>Date Of Birth:</b>&nbsp;<asp:label class="arial_11" id="lblDob" runat="server">Label</asp:label><br>
									<b>Membership Number:</b>&nbsp;<asp:label class="arial_11" id="lblMembershipNumber" runat="server">Label</asp:label>
								</TD>
							</tr>
							<tr bgColor="#ffffff">
								<td colSpan="2" height="20"></td>
							</tr>
							<tr id="AssocCaptionRow" runat="server">
								<td class="arial_11_bold_white" bgColor="#333333" colSpan="2" height="22">&nbsp;&nbsp;Associate 
									Member Information</td>
							</tr>
							<tr id="AssocLastRow" bgColor="#ffffff" runat="server">
								<td colSpan="2" height="20">&nbsp;</td>
							</tr>
						</table>
					</td>
					<td width="200"></td>
				</tr>
			</table>
			<table id="tblViewCards" runat="server" cellSpacing="0" cellPadding="0" width="750">
				<tr>
					<td width="30"></td>
					<td>
						<table cellSpacing="0" cellPadding="0" width="520" border="0">
							<tr>
								<td vAlign="center"><asp:hyperlink id="AssociateCards1" runat="server" cssclass="black_navigation">
									<img src="../images/red_arrow.gif" border="0">&nbsp;</asp:hyperlink></td>
								<td><asp:hyperlink id="AssociateCards2" runat="server" cssclass="black_navigation">
									Click here to view All Temporary Membership cards</asp:hyperlink></td>
							</tr>
						</table>
					</td>
					<td width="200"></td>
				</tr>
			</table>
			<table id="tblPrint" cellSpacing="0" cellPadding="0" width="750" bgColor="#ffffff" border="0" runat="server">
				<tr>
					<td colspan="3" height="20"></td>
				</tr>
				<tr>
					<td width="30" bgColor="#ffffff"></td>
					<td width="520">
						<table style="WIDTH: 520px; HEIGHT: 80px" cellSpacing="0" cellPadding="0" width="520" bgColor="#ffffcc" border="3" runat="server">
							<tr>
								<td class="arial_13_bold" borderColor="#ffffcc" align="middle" height="30">Please 
									print out copies of both receipts:</td>
							</tr>
							<tr>
								<td borderColor="#ffffcc" align="middle" colSpan="2" height="40">
								 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
								 <asp:imagebutton id="btnCustomerReceipt" runat="server" imageurl="/PaymentToolimages/btn_cust_recpt.gif"></asp:imagebutton>&nbsp;
									 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
									 <asp:imagebutton id="btnMerchantReceipt" runat="server" imageurl="/PaymentToolimages/btn_mer_recpt.gif"></asp:imagebutton>&nbsp;
								</td>
							</tr>
						</table>
						<table width="520">
							<tr>
								<td align="middle" colSpan="2" height="100">
								 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
								 <asp:imagebutton id="btnBack" runat="server" imageurl="/PaymentToolimages/btn_backtoreports.gif" onclick="btnBack_Click"></asp:imagebutton>&nbsp;
								</td>
							</tr>
						</table>
					</td>
					<td width="200" bgColor="#ffffff"></td>
				</tr>
			</table>
		</form>
		</TR></TBODY></TABLE>
	</body>
</HTML>
