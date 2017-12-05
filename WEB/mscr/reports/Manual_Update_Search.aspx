<!--
REVISION HISTORY:
08/12/2005	: MODIFIED BY COGNIZANT AS PART OF CSR#3937 - BUG FIXING :
			: Included the grey footer section after the resultant datagrid as part of better looking UI.
			
08/14/2007  : MODIFIED BY COGNIZANT AS PART OF STAR AUTO 2.1 :			
			: Modified the design to left align the Status and RepDO columns of the result grid
08/31/2010  :MODIFIED BY COGNIZANT AS A PART OF TIME ZONE CHANGE ENHANCEMENT
TimeZoneChange.Ch1-Changed the Receipt Date to Receipt Date/Time(MST Arizona) in dgVoidPayment.
PC Phase II - CH1 - Modified the code to update the Void Flow change
CHG0109406 - CH1 - Modified the Column name from "Receipt Date/Time(MST Arizona)" to Receipt Date/Time(Arizona)
CHG0109406 - CH2 - Added a label lblHeaderTimeZone to display the timezone for the results displayed
* CHG0113938 - Modified Name instead of Member Name & Policy Number instead of Policy/Mbr Number.
-->
<%@ Page language="c#" Codebehind="Manual_Update_Search.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.Manual_Update_Search" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Manual Update Search</title>
		<LINK href="../Style.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<table cellSpacing="0" cellPadding="0" width="750" border="0">
			<tr>
				<td colSpan="2" height="10"></td>
			</tr>
		</table>
		<form id="Manual_Update_Search" method="post" runat="server">
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
						<table cellSpacing="0" cellPadding="0" bgColor="#ffffff" border="0">
							<tr>
								<td>&nbsp;&nbsp;
								 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
								 <IMG id="imgUpdateFlag" height="20" src="/PaymentToolimages/red_arrow.gif" width="22" runat="server">&nbsp;</td>
								<td class="arial_12_bold">&nbsp;
									<asp:label id="lblUpdateMsg" runat="server"></asp:label>
								</td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="550" bgColor="#ffffcc" border="0">
							<tr>
								<td class="arial_12_bold_white" bgColor="#333333" colSpan="3" height="22">&nbsp;&nbsp;Manual 
									Update Search</td>
							</tr>
							<tr>
								<td style="WIDTH: 205px" width="205" height="10"></td>
								<td width="250"></td>
								<td width="204"></td>
							</tr>
							<tr>
								<td colSpan="3" height="10"></td>
							</tr>
							<tr>
								<td class="arial_12_bold" style="WIDTH: 205px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Receipt 
									Number:</td>
								<td class="arial_12" colSpan="2"><asp:textbox id="txtReceiptNbr" runat="server" maxlength="25" width="200px"></asp:textbox></td>
							</tr>
							<tr>
								<td colSpan="3" height="3"></td>
							<tr>
								<td class="arial_12_bold" style="WIDTH: 205px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Account 
									Number:</td>
								<td class="arial_12" colSpan="2"><asp:textbox id="txtAccountNbr" runat="server" maxlength="16" width="200px"></asp:textbox></td>
							</tr>
							<tr>
								<td colSpan="3" height="10"></td>
							</tr>
							<tr>
								<td vAlign="center" align="right" bgColor="#cccccc" colSpan="3" height="17"><A id="A1" href="Manual_Update_Search.aspx" runat="server">
								 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
								 <IMG height="17" src="/PaymentToolimages/btn_reset.gif" width="51" border="0"></A>&nbsp;
									 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
									 <asp:imagebutton id="btnSubmit" runat="server" width="67px" height="17" imageurl="/PaymentToolimages/btn_submit.gif" onclick="btnSubmit_Click"></asp:imagebutton>&nbsp;
								</td>
							</tr>
						</table>
						<br>
						<asp:label id="lblErrMsg" runat="server" ForeColor="Red" CssClass="arial_12_bold"></asp:label></td>
				</tr>
				<tr>
					<td height="30">&nbsp;</td>
				</tr>
			</table>
			<table id="tblResultPane" borderColor="#cccccc" cellSpacing="0" cellPadding="0" rules="none" border="0" runat="server">
				<tr>
				<%--CHG0109406 - CH2 - BEGIN - Added a label lblHeaderTimeZone to display the timezone for the results displayed --%>
					<td vAlign="center" bgColor="#cccccc" height="17"><asp:label id="lblMbrTitle" runat="server" backcolor="#cccccc" cssclass="arial_12_bold">Manual Update</asp:label>
					<asp:label id="lblHeaderTimeZone"  text="All timezones are in Arizona" style="font-style:italic;text-align:right;padding-left:840px;" cssclass="arial_11_bold"  runat="server"></asp:label>
					</td>
				<%--CHG0109406 - CH2 - END - Added a label lblHeaderTimeZone to display the timezone for the results displayed --%>
				</tr>
				<tr>
					<td><asp:GridView ID="dgSearchResult" runat="server" CssClass="arial_10" 
                            BorderStyle="None" AutoGenerateColumns="False" >
					
					<HeaderStyle Height="22px" CssClass="arial_12_bold"  borderstyle="None" />
					<Columns>
					<%--CHG0109406 - CH1 - BEGIN - Modified the Column name from "Receipt Date/Time(MST Arizona)" to Receipt Date/Time(Arizona)--%>
					<asp:BoundField DataField="Receipt Date/Time(Arizona)" HeaderText="Receipt Date/Time (Arizona)" 
                            HeaderStyle-Wrap="true" >
<HeaderStyle Wrap="True"></HeaderStyle>
                        </asp:BoundField>
                        <%--CHG0109406 - CH1 - END - Modified the Column name from "Receipt Date/Time(MST Arizona)" to Receipt Date/Time(Arizona)--%>
                        <%-- CHG0113938 - BEGIN - Modified the Column name from "Customer Name" to "Name" --%>
					    <asp:BoundField DataField="Member Name" HeaderText="Name" 
                            HeaderStyle-Wrap="True">
<HeaderStyle Wrap="True"></HeaderStyle>
                        </asp:BoundField>
                        <%-- CHG0113938 - End - Modified the Column name from "Customer Name" to "Name" --%>
								<asp:BoundField DataField="Product Type" HeaderText="Product Type" 
                            HeaderStyle-Wrap="True">
<HeaderStyle Wrap="True"></HeaderStyle>
                        </asp:BoundField>
                        <%-- CHG0113938 - BEGIN - Modified the Column name from "Policy/Mbr Number" to "Policy Number" --%>
								<asp:BoundField DataField="Membership/Policy Number" 
                            HeaderText="Policy Number" HeaderStyle-Wrap="True">
<HeaderStyle Wrap="True"></HeaderStyle>
                        </asp:BoundField>
                        <%-- CHG0113938 - END - Modified the Column name from "Policy/Mbr Number" to "Policy Number" --%>
								<asp:BoundField DataField="Receipt Number" HeaderText="Receipt Number" 
                            HeaderStyle-Wrap="True">
<HeaderStyle Wrap="True"></HeaderStyle>
                        </asp:BoundField>
								<asp:BoundField DataField="Pymt. Method" HeaderText="Pymt Method" 
                            HeaderStyle-Wrap="True">
<HeaderStyle Wrap="True"></HeaderStyle>
                        </asp:BoundField>
								<asp:BoundField DataField="Amount" HeaderText="Amount" 
                            ItemStyle-HorizontalAlign="Right" HeaderStyle-Wrap="True">
<HeaderStyle Wrap="True"></HeaderStyle>

<ItemStyle HorizontalAlign="Right"></ItemStyle>
                        </asp:BoundField>
								<asp:BoundField DataField="Status" HeaderText="Status" 
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True">
<HeaderStyle Wrap="True"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
								<asp:BoundField DataField="Rep DO" HeaderText="Rep DO" 
                            ItemStyle-HorizontalAlign="Left" HeaderStyle-Wrap="True">
<HeaderStyle Wrap="True"></HeaderStyle>

<ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundField>
								<asp:BoundField DataField="User(s)" HeaderText="Created By"/>
								<asp:TemplateField Visible ="true"  HeaderStyle-Width="80 px" ItemStyle-HorizontalAlign="Center" ItemStyle-VerticalAlign="Middle">
									<ItemTemplate>
										 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
										 <asp:imagebutton Visible="true" id="btnManualUpdate1" runat="server" imagealign="Middle" imageurl="/PaymentToolimages/btn_manual_update.gif"  CommandArgument='<%# Container.DataItemIndex %>' CommandName="Click"></asp:imagebutton>
									</ItemTemplate>
<HeaderStyle Width="80px"></HeaderStyle>
<ItemStyle HorizontalAlign="Center" VerticalAlign="Middle"></ItemStyle>
								</asp:TemplateField>
					</Columns>
					</asp:GridView>
					</td>
				</tr>
				<!--
					CSR#3937 - Bug Fixing -Included the footer section after the datagrid.
				-->
				<tr height="17">
					<td bgColor="#cccccc">&nbsp;</td>
				</tr>
				<!--
					CSR#3937 - Bug Fixing -END - Included the footer section after the datagrid.
				-->
			</table>
		</form>
	</body>
</HTML>
