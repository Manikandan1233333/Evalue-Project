<!--
08/10/2005	:	MODIFIED BY COGNIZANT AS PART OF CSR#3937 - BUG FIXING
			:	Increased the height from 16px to 22px for td after datagrid display
			:	Added a line break between Update Rep Details - header and Rep DO drop down list.
			:	Added a line break between Update Status - header and Status drop down, also 
				between Status drop down and footer display
			:	Set the fixed width for the drop down lists - Rep DO, UserName and Status.
			
	PC Phase II changes CH1 - Added the below code to handle Manual Update Void Transactions.

* CHG0113938 - Modified Name instead of Member Name & Policy Number instead of Policy/Mbr Number.
-->
<%@ Page language="c#" Codebehind="Manual_Update.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.Manual_Update" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Manual Update</title>
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Style.css" type="text/css" rel="stylesheet">
		<SCRIPT language="javascript">
	    function ConfirmTrans()
	    {
		   var returnval;
		   returnval=confirm('Are you sure you want to Update the transaction?');
		   return returnval;
	    }
		</SCRIPT>
	</HEAD>
	<body>
		<table cellSpacing="0" cellPadding="0" width="750" border="0">
			<tr>
				<td colSpan="2" height="10"></td>
			</tr>
		</table>
		<form id="frmManual_Updation" name="form2" runat="server">
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tbody>
					<tr>
						<!-- space to left of search table-->
						<td width="30">
							<table cellSpacing="0" cellPadding="0" border="0">
								<tr>
									<td width="30"></td>
								</tr>
							</table>
						</td>
						<td>
							<table id="tblManualUpdate" cellSpacing="0" cellPadding="0" width="560" bgColor="#ffffcc" border="0" runat="server">
								<tbody>
									<tr>
										<td class="arial_12_bold_white" bgColor="#333333" colSpan="3" height="22">&nbsp;&nbsp;Manual 
											Update</td>
									</tr>
									<tr>
										<td>
											<table id="ManualUpdate" cellSpacing="0" cellPadding="0" width="560" bgColor="#ffffcc" border="0" runat="server">
												<TBODY>
													<tr>
														<td colSpan="3" height="9"></td>
													</tr>
													<tr>
														<td class="arial_12_bold" width="205" valign="top">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Receipt 
															Number:</td>
														<td class="arial_12" valign="bottom"><asp:label id="lblReceiptNumber" runat="server" CssClass="arial_12" Height="25px"></asp:label>
														</td>
														<td></td>
													</tr>
													<tr>
														<td colSpan="3" height="3"></td>
													</tr>
													<tr>
														<td class="arial_12_bold" width="205" valign="top">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Receipt 
															Date/Time:</td>
														<td class="arial_12" valign="bottom"><asp:label id="lblReceiptDateTime" runat="server" CssClass="arial_12" Height="25px"></asp:label>
														</td>
														<td></td>
													</tr>
													<tr>
														<td colSpan="3" height="3"></td>
													</tr>
													<TR>
														<TD class="arial_12_bold" width="205" valign="top">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Pymt 
															Method:</TD>
														<TD class="arial_12" valign="bottom"><asp:label id="lblPymtMethod" runat="server" CssClass="arial_12" Height="25px"></asp:label></TD>
														<TD></TD>
													</TR>
													<tr>
														<td colSpan="3" height="3"></td>
													</tr>
													<TR>
														<TD class="arial_12_bold" width="205" valign="top">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Status:</TD>
														<TD class="arial_12" valign="bottom"><asp:label id="lblStatus" runat="server" CssClass="arial_12" Height="25px"></asp:label></TD>
														<TD></TD>
													</TR>
													<tr>
														<td colSpan="3" height="3"></td>
													</tr>
													<TR>
														<TD class="arial_12_bold" width="205" valign="top">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rep 
															DO:</TD>
														<TD class="arial_12" valign="bottom"><asp:label id="lblRepDO" runat="server" CssClass="arial_12" Height="25px"></asp:label></TD>
														<TD></TD>
													</TR>
													<tr>
														<td colSpan="3" height="3"></td>
													</tr>
													<TR>
														<TD class="arial_12_bold" width="205" valign="top">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Created 
															by:</TD>
														<TD class="arial_12" valign="bottom"><asp:label id="lblCreatedBy" runat="server" CssClass="arial_12" Height="25px"></asp:label></TD>
														<td></td>
													</TR>
													<tr>
														<td colSpan="3" height="10px"></td>
													</tr>
													<TR>
														<td width="560" colSpan="3" valign="bottom">
															<asp:datagrid id="dgProductDetails" runat="server" CellPadding="2" AutoGenerateColumns="False" borderstyle="None" ShowFooter="False" Width="560px" BorderColor="#cccccc">
																<AlternatingItemStyle BackColor="LightYellow" CssClass="arial_12"></AlternatingItemStyle>
																<ItemStyle CssClass="arial_12" BackColor="White"></ItemStyle>
																<HeaderStyle Height="22px" CssClass="arial_12_bold" BackColor="#cccccc"></HeaderStyle>
																<Columns>
																	<asp:BoundColumn DataField="Product Type" HeaderText="  Product Type"></asp:BoundColumn>
                                                                    <%-- CHG0113938 - BEGIN - Modified the Column name from "Policy/Mbr Number" to "Policy Number" --%>
																	<asp:BoundColumn DataField="Membership/Policy Number" HeaderText="Policy Number"></asp:BoundColumn>
                                                                    <%-- CHG0113938 - END - Modified the Column name from "Policy/Mbr Number" to "Policy Number" --%>
                                                                    <%-- CHG0113938 - BEGIN - Modified the Column name from "Member Name" to "Name" --%>
																	<asp:BoundColumn DataField="Member Name" HeaderText="Name"></asp:BoundColumn>
                                                                    <%-- CHG0113938 - END - Modified the Column name from "Member Name" to "Name" --%>
																	<asp:BoundColumn DataField="Amount" HeaderText="Amount" DataFormatString="{0:c}">
																		<ItemStyle HorizontalAlign="Right" VerticalAlign="Middle"></ItemStyle>
																	</asp:BoundColumn>
																</Columns>
															</asp:datagrid></td>
													</TR>
													<!-- 
													CSR 3937 Bug Fix - Increased the height from 16px to 22px 
													-->
													<tr>
														<td bgColor="#cccccc" colSpan="3" height="22px"></td>
													</tr>
													<!-- 
													END - Increased the height from 16px to 22px 
													-->
													<tr>
														<td bgColor="white" colSpan="3" height="22px"></td>
													</tr>
													<tr>
														<td class="arial_12_bold" bgColor="#cccccc" colSpan="3" height="22">&nbsp;&nbsp;Update 
															Rep Details</td>
														<td></td>
													</tr>
													<!-- 
													Added a break line (space) as part of CSR#3937 - Bug Fixing 
													-->
													<tr>
														<td colSpan="3" height="16"></td>
													</tr>
													<!-- 
													END - Added a break line (space) as part of CSR#3937 - Bug Fixing 
													-->
													<!-- CSR#3937 Bug Fixes - Set the width of the Rep DO dropdown to 300 px -->
													<tr>
														<td class="arial_12_bold" width="205">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Rep DO:
														</td>
														<td class="arial_12"><asp:dropdownlist id="cboRepDO" Runat="server" AutoPostBack="true" Height="20px" Width="300px" onselectedindexchanged="cboRepDO_SelectedIndexChanged"></asp:dropdownlist></td>
													</tr>
													<tr>
														<td colSpan="3" height="16"></td>
													</tr>
													<!-- CSR#3937 Bug Fixes - Set the width of the User Name dropdown to 300 px -->													
													<tr>
														<td class="arial_12_bold" width="205">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;User Name:
														</td>
														<td class="arial_12"><asp:dropdownlist id="cboUserName" Width="300px" Runat="server" Height="20px"></asp:dropdownlist></td>
													</tr>
													<tr>
														<td colSpan="3" height="16"></td>
													</tr>
													<tr>
														<td class="arial_12_bold" bgColor="#cccccc" colSpan="3" height="22">&nbsp;&nbsp;Update 
															Status</td>
														<td></td>
													</tr>
													<!-- 
													Added a break line (space) as part of CSR#3937 - Bug Fixing 
													-->
													<tr>
														<td colSpan="3" height="16"></td>
													</tr>
													<!-- 
													END - Added a break line (space) as part of CSR#3937 - Bug Fixing 
													-->
													<!-- CSR#3937 Bug Fixes - Set the width of the Status dropdown to 300 px -->													
													<tr>
														<td class="arial_12_bold" width="205">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Status:
														</td>
														<td class="arial_12"><asp:dropdownlist id="cboStatus" Runat="server" Height="20px" Width="300px"></asp:dropdownlist></td>
													</tr>
													<!-- 
													Added a break line (space)as part of CSR#3937 - Bug Fixing 
													-->
													<tr>
														<td colSpan="3" height="16"></td>
													</tr>
													<!-- 
													END - Added a break line (space) as part of CSR#3937 - Bug Fixing 
													-->
												</TBODY>
											</table>
										</td>
									</tr>
									<tr>
										<td vAlign="center" align="right" bgColor="#cccccc" colSpan="3" height="17">
											 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
											 <asp:imagebutton id="btnCancel" runat="server" imagealign="Middle" imageurl="/PaymentToolimages/btn_cancel.gif" height="17" width="51" onclick="btnCancel_Click"></asp:imagebutton>&nbsp;
											 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
											 <asp:imagebutton id="btnUpdate" runat="server" imagealign="Middle" imageurl="/PaymentToolimages/btn_manual_update.gif" height="17" width="112px" onclick="btnUpdate_Click"></asp:imagebutton>&nbsp;
										</td>
									</tr>
								</tbody>
							</table>
						</td>
					</tr>
					<tr>
						<td></td>
						<td colSpan="2"><asp:label id="lblMessage" runat="server" CssClass="arial_12_bold_red" Width="560px"></asp:label></td>
					</tr>
				</tbody>
			</table>
		</form>
	</body>
</HTML>
