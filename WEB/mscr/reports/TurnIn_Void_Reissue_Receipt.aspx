<%@ Page language="c#" Codebehind="TurnIn_Void_Reissue_Receipt.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.TurnIn_Void_Reissue_Receipt" %>
<HTML>
	<HEAD>
		<title>Void Or Reissue</title> 
		<!--
HISTORY:
MODIFIED BY COGNIZANT AS PART OF Q4 RETROFIT CHANGES
12/6/2005 Q4-Retrofit.Ch1- Removed open and close braces for alternating item in the repeater
PC Phase II changes CH1 - Removed the ImageButton "btnReissue".
PC Phase II changes CH2 - Removed the HtmlInputText field "txtAmount" to Label "lblItemAmount" and removed the validations.
CHG0113938 - To display Name instead of Member Name & Policy Number instead of Policy/Mbr Number in the Payment Void Receipt page.
-->
		<LINK href="../Style.css" type="text/css" rel="stylesheet">
			<!--START Added by COGNIZANT on 11/04/2004 for Payment Tool enhancements -->
			<!--Javascript function to display a Confirm dialog box when a Receipt is Voided or Reissued -->
			<script language="javascript">
			// Javascript function to display the confirm box when Void or Reissue button is clicked
			function confirmvoidreissue()
			{
				// The Variable Page_IsValid is declared in WebUIValidation.js
				// As WebUIValidation.js is included into this page when this page is rendered, 
				// the Page_IsValid variable has been checked to verify if all validations have succeeded
				if(Page_IsValid)
				{	
					var straction;
					var buttonclicked = document.TurnIn_Void_Reissue_Receipt.hidEvent.value;
					if(buttonclicked == 'btnVoid')
					{
						straction = 'Void';
					}
					else if	(buttonclicked == 'btnReissue')
					{
						straction = 'Reissue';
					}
					Page_IsValid = straction;
					if(Page_IsValid)
					{
						document.TurnIn_Void_Reissue_Receipt.submit();
					}
				}
				return Page_IsValid;
			}
		
			// Javascript function to capture the Name of the Imagebutton when Void / Reissue Imagebutton is clicked
			function captureevent(oSrc, args)
			{
				var occuredevent = event.srcElement.name;
			    document.TurnIn_Void_Reissue_Receipt.hidEvent.value = occuredevent;
				args.IsValid = true;
			}
			//Added by Cognizant on 4-21-05
			//When the Conformation Page is loaded the User will not be able to Move Back through the Back Button in the Browser. 
			//This is made to avoid Data redundancy in the History Table
			window.history.forward(1);
			</script>
		<!-- END Code Added by COGNIZANT -->
	</HEAD>
	<body>
		&nbsp;
		
		<!--START Added by COGNIZANT on 11/04/2004 for Payment Tool enhancements -->
		<!--Javascript function added to the onsubmit of form to display a Confirm dialog box when a Receipt is Voided / Reissued -->
		<form id="TurnIn_Void_Reissue_Receipt" method="post" runat="server" onsubmit="return confirmvoidreissue()">
			<!-- END Added by by COGNIZANT on 11/04/2004 for Payment Tool enhancements -->
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td width="30">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td class="arial_12_bold" align="left">&nbsp;</td>
							</tr>
						</table>
					</td>
					<td>
						<table cellSpacing="0" cellPadding="0" width="604" bgColor="#ffffcc" border="0">
							<tbody>
								<tr>
									<td class="arial_11_bold_white" bgColor="#333333" colSpan="5" height="22">&nbsp;&nbsp; Void&nbsp; Receipt</td>
								</tr>
								<tr>
									<td colSpan="5" height="10"></td>
								</tr>
								<tr>
									<td class="arial_12_bold" width="117" height="22">&nbsp;&nbsp;Receipt Date</td>
									<td width="207"><asp:label id="lblReceiptDate" runat="server" CssClass="arial_12">Label</asp:label></td>
									<td class="arial_12_bold" width="155" height="22">Receipt Number</td>
									<td><asp:label id="lblReceiptNumber" runat="server" CssClass="arial_12">Label</asp:label></td>
								</tr>
								<tr>
									<td colSpan="5" height="10"></td>
								</tr>
								<tr>
									<td class="arial_12_bold" width="117" height="22">&nbsp;&nbsp;User(s)</td>
									<td width="207"><asp:label id="lblUsers" runat="server" CssClass="arial_12">Label</asp:label></td>
									<td class="arial_12_bold" width="155" height="22">Orginal Payment Method</td>
									<td><asp:label id="lblPaymentMethod" runat="server" CssClass="arial_12">Label</asp:label></td>
								</tr>
								<tr>
									<td colSpan="5" height="10"></td>
								</tr>
								<tr>
									<td class="arial_12_bold" width="117" height="22">&nbsp;&nbsp;Payment Method</td>
									<td width="207"><asp:dropdownlist id="PaymentType" runat="server" DataTextField="Description" DataValueField="ID"></asp:dropdownlist></td>
									<td colSpan="3"></td>
								<tr>
									<td colSpan="5" height="10"></td>
								</tr>
								<tr>
									<td bgColor="#cccccc" colSpan="5" height="25"></td>
								</tr>
								<tr>
									<td bgColor="#ffffff" colSpan="5" height="30"></td>
								</tr>
								<tr>
									<td class="arial_12_bold" bgColor="#cccccc" colSpan="5" height="22">&nbsp;&nbsp; Void&nbsp; Receipt</td>
								</tr>
							</tbody></table>
					</td>
					<td>
						<table cellSpacing="0" cellPadding="0" width="116">
							<tr>
								<td width="116"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td width="30">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td width="30"></td>
							</tr>
						</table>
					</td>
					<td>
						<table cellSpacing="0" cellPadding="0" width="604" bgColor="#ffffff" border="0">
							<asp:repeater id="ReceiptsRepeater" runat="server" OnItemDataBound="ReceiptsRepeater_ItemDataBound">
								<HeaderTemplate>
									<tr class="item_template_header_white">
										<td class="border_left">&nbsp;</td>
										<td width="15%" class="border" height="20">Product</td>
										<td width="12%">Trans</td>
                                        <%-- CHG0113938 - BEGIN - Modified the Column name from "Policy/Mbr" to "Policy" --%>
										<td width="14%">Policy</td>
                                        <%-- CHG0113938 - END - Modified the Column name from "Policy/Mbr" to "Policy" --%>
                                        <%-- CHG0113938 - BEGIN - Modified the Column name from "Member" to "Name"--%>
										<td width="25%">Name</td>
                                        <%-- CHG0113938 - END - Modified the Column name from "Member" to "Name"--%>
                                        <%-- CHG0113938 - BEGIN - Modified the Column name from Empty text to "Amount"--%>
										<td class="border_right">Amount</td>
                                        <%-- CHG0113938 - END - Modified the Column name from Empty text to "Amount"--%>
									</tr>
									<tr class="item_template_header_white">
										<td class="border_Down_left">&nbsp;</td>
										<td width="15%" class="border_Down" height="22" valign="top">Type</td>
										<td width="12%" class="border_Down" valign="top">Type</td>
										<td width="14%" class="border_Down" valign="top">Number</td>
                                        <%-- CHG0113938 - BEGIN - Modified the Column name from "Name" to empty text--%>
										<td width="25%" class="border_Down" valign="top">&nbsp;</td>
                                        <%-- CHG0113938 - END - Modified the Column name from "Name" to empty text--%>
                                        <%-- CHG0113938 - BEGIN - Modified the Column name from "Amount" to Empty text--%>
										<td class="border_Down_right" valign="top">&nbsp;</td>
                                        <%-- CHG0113938 - END - Modified the Column name from "Amount" to Empty text--%>
									</tr>
								</HeaderTemplate>
								<ItemTemplate>
									<tr class="item_template_row" id="ReceiptRow" runat="server">
										<td class="border_Down_left">&nbsp;</td>
										<td align="left" class="border_Down" height="35">
											<%# DataBinder.Eval(Container.DataItem, "ProductName") %>
										</td>
										<td align="left" class="border_Down">
											<%# DataBinder.Eval(Container.DataItem, "TransType") %>
										</td>
										<td align="left" class="border_Down">
											<%# DataBinder.Eval(Container.DataItem, "PolicyNumber") %>
										</td>
										<td align="left" class="border_Down">
											<%# DataBinder.Eval(Container.DataItem, "FirstName") %>
											,&nbsp;<%# DataBinder.Eval(Container.DataItem, "LastName") %>
										</td>
										<td align="left" class="border_Down_right">
											
											<!--START Added by COGNIZANT on 11/04/2004 for Payment Tool enhancements -->
											<!--CustomValidator added to capture the button id of the Imagebutton when a Receipt is Voided / Reissued -->
											<asp:CustomValidator ID="custvldPolicyAmount" Runat="server" ClientValidationFunction="captureevent" Display="None"></asp:CustomValidator>
											<!-- END Added by by COGNIZANT on 11/04/2004 for Payment Tool enhancements -->
											<!-- Begin PC Phase II changes CH2 - Removed the HtmlInputText field "txtAmount" to Label "lblItemAmount" and removed the validations. -->
											<asp:Label ID="lblItemAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemAmount") %>'></asp:Label>
											<asp:Label ID="lblTotalAmount" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAmount") %>' >
											<!-- End PC Phase II changes CH2 - Removed the HtmlInputText field "txtAmount" to Label "lblItemAmount" and removed the validations. -->
											</asp:Label>
											<asp:Label ID="lblAssocID" Runat="server" Visible ="False" Text='<%# DataBinder.Eval(Container.DataItem, "AssocID") %>' >
											</asp:Label>
											<asp:Label ID="lblItemId" Visible ="False" Runat="server" ItemNo= '<%# DataBinder.Eval(Container.DataItem, "ItemID") %>' ProductName = '<%# DataBinder.Eval(Container.DataItem, "ProductName") %>'>
											</asp:Label>
										</td>
									</tr>
								</ItemTemplate>
								<AlternatingItemTemplate>
									<tr class="item_template_alt_row" id="ReceiptRow" runat="server">
										<td class="border_Down_left">&nbsp;</td>
										<td align="left" class="border_Down" height="35">
											<%# DataBinder.Eval(Container.DataItem, "ProductName") %>
										</td>
										<td align="left" class="border_Down">
											<%# DataBinder.Eval(Container.DataItem, "TransType") %>
										</td>
										<td align="left" class="border_Down">
											<%# DataBinder.Eval(Container.DataItem, "PolicyNumber") %>
										</td>
										<td align="left" class="border_Down">
											<%# DataBinder.Eval(Container.DataItem, "FirstName") %>
											,&nbsp;<%# DataBinder.Eval(Container.DataItem, "LastName") %>
										</td>
										<td align="left" class="border_Down_right">
											
											<!--START Added by COGNIZANT on 11/04/2004 for Payment Tool enhancements -->
											<!--CustomValidator added to capture the button id of the Imagebutton when a Receipt is Voided / Reissued -->
											<asp:CustomValidator ID="custvldPolicyAmount" Runat="server" ClientValidationFunction="captureevent" Display="None"></asp:CustomValidator>
											<!-- END Added by by COGNIZANT on 11/04/2004 for Payment Tool enhancements -->
											<!-- Begin PC Phase II changes CH2 - Removed the HtmlInputText field "txtAmount" to Label "lblItemAmount" and removed the validations. -->
											<asp:Label ID="lblItemAmount" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ItemAmount") %>'></asp:Label>
											<asp:Label ID="lblTotalAmount" Runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TotalAmount") %>' >
											<!-- End PC Phase II changes CH2 - Removed the HtmlInputText field "txtAmount" to Label "lblItemAmount" and removed the validations. -->
											</asp:Label>
											<asp:Label ID="lblAssocID" Runat="server" Visible ="False" Text='<%# DataBinder.Eval(Container.DataItem, "AssocID") %>' >
											</asp:Label>
											<asp:Label ID="lblItemId" Runat="server" Visible ="False" ItemNo= '<%# DataBinder.Eval(Container.DataItem, "ItemID") %>' ProductName = '<%# DataBinder.Eval(Container.DataItem, "ProductName") %>'>
											</asp:Label>
										</td>
									</tr>
								</AlternatingItemTemplate>
								<FooterTemplate>
									<tr class="item_template_header_white">
										<td class="border_Down_left">&nbsp;</td>
										<td align="right" class="border_Down" height="15" colspan="4">
											TOTAL &nbsp;&nbsp;
										</td>
										<td align="left" class="border_Down_right">
											<asp:Label ID="lblTotal" Runat="server" Visible="True"> &nbsp;
											</asp:Label>
										</td>
									</tr>
								</FooterTemplate>
							</asp:repeater></table>
					</td>
					<td>
						<table cellSpacing="0" cellPadding="0" width="116">
							<tr>
								<td width="116"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td width="30">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td width="30"></td>
							</tr>
						</table>
					</td>
					<td>
						<table cellSpacing="0" cellPadding="0" width="604" border="0">
							<tbody>
								<tr>
									<td vAlign="center" align="right" width="603" bgColor="#cccccc" height="25"><A id="A1" href="SalesTurnIn_Report.aspx" runat="server">
									 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
									 <IMG height="17" src="/PaymentToolimages/btn_cancel.gif" width="51" border="0"></A>&nbsp;
										 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
										 <asp:imagebutton id="btnVoid" runat="server" height="17" width="51" imageurl="/PaymentToolimages/btn_void.gif" onclick="btnVoid_Click"></asp:imagebutton>&nbsp;
										 <!-- Begin PC Phase II changes CH1 - Removed the ImageButton "btnReissue". -->
										 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
										
										<!-- End PC Phase II changes CH1 - Removed the ImageButton "btnReissue". -->
									&nbsp;&nbsp;&nbsp;
									<td></td>
								</tr>
							</tbody></table>
					</td>
					<td>
						<table cellSpacing="0" cellPadding="0" width="116">
							<tr>
								<td width="116"></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td colSpan="3" height="10">
						<asp:ValidationSummary CssClass="arial_12_bold_red" id="ValidationSummary1" runat="server"></asp:ValidationSummary></td>
				</tr>
				<tr>
					<td width="30">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td width="30"></td>
							</tr>
						</table>
					</td>
					<td>
						<table cellSpacing="0" cellPadding="0" width="604" border="0">
							<tr>
								<td class="arial_12_bold_red" width="604" height="20">&nbsp;&nbsp;<asp:label id="lblErrMsg" runat="server"></asp:label></td>
							</tr>
						</table>
						<!--START Added by COGNIZANT on 11/04/2004 for Payment Tool enhancements -->
						<!--Hidden field added to store the button id of the button when a Receipt is Voided / Reissued -->
						<input type="hidden" id="hidEvent" name="hidEvent"> 
						<!-- END Added by by COGNIZANT on 11/04/2004 for Payment Tool enhancements -->
					</td>
					<td>
						<table cellSpacing="0" cellPadding="0" width="116">
							<tr>
								<td width="116">&nbsp;
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
