<%@ Page language="c#" Codebehind="TurnIn_Void_Reissue_Confirmation.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.TurnIn_Void_Reissue_Confirmation" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<!-- REVISION HISTORY:
MODIFIED BY COGNIZANT
PC Phase II changes CH1 - Removed the below ImageButton "btnConfirmReissue".

CHG0113938 - To display Name instead of Member Name & Policy Number instead of Policy/Mbr Number in the Payment Void Confirmation Page
 -->
<HTML>
	<HEAD>
		<title>TurnIn_Void_Reissue_Confirmation</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Style.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="TurnIn_Void_Reissue_Confirmation" method="post" runat="server">
			<table id="tblVoid" cellSpacing="0" cellPadding="0" width="750" border="0" runat="server">
				<tr>
					<td colSpan="3" height="10"></td>
				</tr>
				<tr>
					<td width="30">&nbsp;</td>
					<td class="arial_11_bold" align="left" width="520" height="22">Void Acknowledgement</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="3" height="10">&nbsp;</td>
				</tr>
				<tr>
					<td width="30">&nbsp;</td>
					
					<td class="arial_12" align="left">
                       <asp:PlaceHolder runat="server" Visible = "true" ID="VoidAck">
                        <asp:Label ID="lblVoidAck" runat="server" style="background-color:#ffffcc"
                            Text="Your Void has not yet been submitted for processing.Please review the details and confirm."></asp:Label>
                    </asp:PlaceHolder>
                    </td>
					
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="20">&nbsp;</td>
				</tr>
			</table>
			<table id="tblReissue" cellSpacing="0" cellPadding="0" width="750" border="0" runat="server">
				<tr>
					<td colSpan="3" height="10"></td>
				</tr>
				<tr>
					<td width="30">&nbsp;</td>
					<td class="arial_11_bold" align="left" width="520" height="22">Reissue Confirmation
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="3" height="10"></td>
				</tr>
				<tr>
					<td width="30">&nbsp;</td>
					<td class="arial_12" align="left" bgColor="#ffffcc">Your payment has not yet been 
						Reissued. Please review the details and confirm.
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="20">&nbsp;</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="750" border="0" runat="server">
				<tr>
					<td width="30">&nbsp;</td>
					<td class="arial_11_bold" align="left" width="520" height="22">
						<table cellSpacing="0" cellPadding="0" bgColor="#ffffff" border="0">
							<asp:repeater id="ReceiptsRepeater" runat="server" OnItemDataBound="ReceiptsRepeater_ItemDataBound">
								<HeaderTemplate>
									<tr class="arial_11_bold" height="22" bgcolor="#cccccc">
										<td class="border_Up_Down_left" colspan="4">&nbsp;Product and Total Amount 
											Information</td>
										<td class="border_Up_Down_right">&nbsp;</td>
									</tr>
									<tr class="arial_11_bold">
										<td class="border_left">&nbsp;</td>
										<td width="25%" class="border" height="15">Product Type</td>
                                        <%-- CHG0113938 - BEGIN - Modified the Column name from "Policy/Mbr Number" to "Policy Number" --%>
										<td width="30%">Policy Number</td>
                                        <%-- CHG0113938 - END - Modified the Column name from "Policy/Mbr Number" to "Policy Number" --%>
                                         <%-- CHG0113938 - BEGIN - Modified the Column name from "Member Name" to "Name" --%>
										<td width="30%">Name</td>
                                         <%-- CHG0113938 - END - Modified the Column name from "Member Name" to "Name" --%>
										<td class="border_right">Amount</td>
									</tr>
								</HeaderTemplate>
								<ItemTemplate>
									<tr id="ReceiptRow" runat="server" class="arial_11">
										<td class="border_left">&nbsp;</td>
										<td align="left" height="20">
											<%# DataBinder.Eval(Container.DataItem, "ProductName") %>
										</td>
										<td align="left">
											<%# DataBinder.Eval(Container.DataItem, "PolicyNumber") %>
										</td>
										<td align="left">
											<%# DataBinder.Eval(Container.DataItem, "FirstName") %>
											,&nbsp;<%# DataBinder.Eval(Container.DataItem, "LastName") %>
										</td>
										<td align="left" class="border_right">
											<asp:Label ID="lblAmount" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemAmount","{0:c}") %>'>&nbsp;
											</asp:Label>
											<asp:Label ID="lblTotalAmount" Visible="False"  Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAmount","{0:c}") %>' >
											</asp:Label>
											<asp:Label ID="lblAssocID" Runat="server" Visible ="False" Text='<%# DataBinder.Eval(Container.DataItem, "AssocID") %>' >
											</asp:Label>
											<asp:Label ID="lblItemId" Visible ="False" Runat="server" ItemNo= '<%# DataBinder.Eval(Container.DataItem, "ItemID") %>' ProductName = '<%# DataBinder.Eval(Container.DataItem, "ProductName") %>'>
											</asp:Label>
										</td>
									</tr>
								</ItemTemplate>
								<AlternatingItemTemplate>
									<tr id="ReceiptRow" runat="server" class="arial_11">
										<td class="border_left">&nbsp;</td>
										<td align="left" height="20">
											<%# DataBinder.Eval(Container.DataItem, "ProductName") %>
										</td>
										<td align="left">
											<%# DataBinder.Eval(Container.DataItem, "PolicyNumber") %>
										</td>
										<td align="left">
											<%# DataBinder.Eval(Container.DataItem, "FirstName") %>
											,&nbsp;<%# DataBinder.Eval(Container.DataItem, "LastName") %>
										</td>
										<td align="left" class="border_right">
											<asp:Label ID="lblAmount" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemAmount","{0:c}") %>'>&nbsp;
											</asp:Label>
											<asp:Label ID="lblTotalAmount" Visible="False"  Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAmount","{0:c}") %>' >
											</asp:Label>
											<asp:Label ID="lblAssocID" Runat="server" Visible ="False" Text='<%# DataBinder.Eval(Container.DataItem, "AssocID") %>' >
											</asp:Label>
											<asp:Label ID="lblItemId" Visible ="False" Runat="server" ItemNo= '<%# DataBinder.Eval(Container.DataItem, "ItemID") %>' ProductName = '<%# DataBinder.Eval(Container.DataItem, "ProductName") %>'>
											</asp:Label>
										</td>
									</tr>
								</AlternatingItemTemplate>
								<FooterTemplate>
									<tr class="arial_11_bold">
										<td class="border_Down_left">&nbsp;</td>
										<td align="right" class="border_Down" height="15" colspan="3" align="center" width="370">
											Total Amount &nbsp;&nbsp;
										</td>
										<td align="left" class="border_Down_right">
											<asp:Label ID="lblTotal" Runat="server" Visible="True"> &nbsp;
											</asp:Label>
										</td>
									</tr>
								</FooterTemplate>
							</asp:repeater>
						</table>
					</td>
					<td>&nbsp;</td>
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
							<tr>
								<td height="20" colspan="2"></td>
							</tr>
							<tr>
								<td colspan="2"></td>
							</tr>
							<tr>
								<td bgColor="#cccccc" height="22" colspan="2"></td>
							</tr>
							<tr bgColor="#ffffcc">
								<td class="arial_11" height="22" colspan="2"><font class="arial_11_bold">&nbsp;Payment 
										Method:</font>&nbsp;<asp:label id="lblPaymentType" runat="server"></asp:label></td>
							</tr>
							<tr bgColor="#ffffcc" id="trVoidMessage" runat="server">
								<td valign="center">&nbsp;
								 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
								 <img height="17" src="/PaymentToolimages/red_arrow.gif" width="22" id="imgErrorFlag"></td>
								<td class="arial_11_red">&nbsp;Note: Only the payment will be voided and the policy 
									will not be cancelled</td>
							</tr>
							<tr>
								<td vAlign="center" align="right" width="603" bgColor="#cccccc" height="25" colspan="2">
												     <!--External URL Implementation-Modified the below  code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach -->
												     <asp:imagebutton id="btnBack" runat="server" imageurl="../../PaymentToolmsc/images/btn_back.gif" width="51" height="17" onclick="btnBack_Click"></asp:imagebutton>&nbsp;
									<A id="A1" href="SalesTurnIn_Report.aspx" runat="server">
									 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
									 <IMG height="17" src="/PaymentToolimages/btn_cancel.gif" width="51" border="0"></A>&nbsp;
									 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
									 <!-- PC Phase II changes CH1 - Removed the ImageButton "btnConfirmReissue".-->
									 
									 <asp:imagebutton id="btnConfirmVoid" runat="server" imageurl="/PaymentToolimages/btn_ConfirmVoid.gif" height="17" onclick="btnConfirmVoid_Click"></asp:imagebutton>&nbsp;&nbsp;&nbsp;
								</td>
							</tr>
							<tr>
								<td class="arial_12_bold_red" width="604" height="20" colspan="2">&nbsp;&nbsp;<asp:label id="lblErrMsg" runat="server"></asp:label></td>
							</tr>
						</table>
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
		</form>
	</body>
</HTML>
