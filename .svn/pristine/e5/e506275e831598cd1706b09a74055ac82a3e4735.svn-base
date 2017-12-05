<%@ Register TagPrefix="uc" TagName="Dates" Src="../Controls/Dates.ascx" %>
<%@ Page language="c#" Codebehind="CashierReconciliation_Report.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.CashierReconciliation_Report" %>
<!--
 * HISTORY:
 * MODIFIED BY COGNIZANT AS PART OF Q4 RETROFIT CHANGES
 * 11/30/2005 Q4-Retrofit.Ch1 :Removed the label control lblUploadType from the td (tdChkItem)
 *		Changed the Attribute name from "Enabled" to "UploadType" in td (tdProdType) for
 *		Naming Convention 
 
 * 06/07/2007 STAR Retrofit III.Ch1: 
 *			Modified to left align the amount tag in the report header
 *
 * 11/11/2008 Change ID: Ch2 - Modified by COGNIZANT
 *		The below changes were performed so that when a user clicks on Approve button and then immediately on Reject button, then it 
 *		must not cause unexpected database updates such as status update to Approval in PAY_Payment table and status update to Rejected
 *		in the INS_Order table. This in turn will cause a total mismatch between the POES & the RBE batch files.
 *		1. Added a new CustomValidator in the FooterTemplate of the repeater control: rptCashierApproveRepeater to capture if the 
 *		   Approve / Reject button is clicked and trigger events through Javascript
 *		2. Added a Javascript function captureevent to display confirmation box and disable the Reject button when Approve button
 *		   is clicked and vice versa for the Reject button click event
 * MODIFIED BY COGNIZANT AS A PART OF TIME ZONE ENHANCEMENT ON 8-31-2010.
 * TimeZoneChange.Ch1-Changed the column header Receipt Date to Receipt Date/Time(MST Arizona)Approved Date to Approved Date/Time(MST Arizona)on 8-31-2010.
	* Changes made to display the pager properly,changed to grid view from data grid by Cognizant on 08/13/2010
 
 PC Phase II changes CH1 - Added the below code for handling Date Time Picker 
 MAIG - CH1 - Modified the width from 15 to 27 
 MAIG - CH2 - Modified the below code to remove the &nbsp to td
 MAIG - CH3 - Added code to align properly that affects IE 11 alone
 MAIGEnhancement CHG0107527 - CH1 - Added code to set the max length of the textbox from 11 to 19 to support manual date entry
 CHG0109406 - CH1 - Appended the timezone Arizona along with the Date Label 
 CHG0109406 - CH2 - Added the td which displays the Timezone information 
 CHG0109406 - CH3 - Modified the Column name from "Receipt Date/Time(MST Arizona)" to Receipt Date/Time(Arizona)
 CHG0109406 - CH4 - Modified the Column name from "Approved Date/Time(MST Arizona)" to Approved Date/Time(Arizona)
 CHG0110069 - CH1 - Appended the /Time along with the Date Label
* CHG0113938 - To display Name instead of Member Name & Policy Number instead of Policy/Mbr Number in the Cashier Reconciliation Report.
 -->
<HTML>
	<HEAD>
		<title>Cashier Reconciliation</title>
		<LINK href="../Style.css" type="text/css" rel="stylesheet">
			<script language="javascript">
				//	START: Change ID: Ch2 - Added by COGNIZANT on 11/11/2008
				function captureevent(oSrc, args)
				{
					var occuredevent = event.srcElement.id;
					var strAction, returnval, btnToDisable;
					if(occuredevent.indexOf('btnApprove') > -1) 
						{
							strAction = "approve";
							btnToDisable = occuredevent.replace('btnApprove','btnReject');
						}
					else if(occuredevent.indexOf('btnReject') > -1) 
						{
							strAction = "reject";
							btnToDisable = occuredevent.replace('btnReject','btnApprove');
						}
					else
						{
							args.IsValid = true;
							return
						}
					//returnval = confirm('Are you sure you want to ' + strAction + ' the selected transaction(s)?');
					//if(returnval == true)
					//{
					document.getElementById(btnToDisable).disabled = true;
					args.IsValid = true;
					//}
					//else
					//{
					//args.IsValid = false;
					//}
				}
				// END - Change ID: Ch2
				
				function Open_Report(Type)
				{
				
				var PrintXlsURL="CashierRecon_Printxls.aspx?type=" +Type;
				window.open(PrintXlsURL,"MyReports");
				return false;
				}	
				
				window.history.forward(1);				
			</script>
			<script type="text/javascript" src="../Reports/CalendarControl.js"></script>   
	    <style type="text/css">
            .style1
            {
                width: 147px;
            }
            /*MAIG - CH3 - BEGIN - Added code to align properly that affects IE 11 alone */
            		 @media screen and (min-width:0\0) { 
    #CashierReconciliation_Report {padding-top:20px;}
}
/*MAIG - CH3 - END - Added code to align properly that affects IE 11 alone */
            </style>
	</HEAD>
	<body>
		<form id="CashierReconciliation_Report" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<!-- space to left of search table-->
					<!-- MAIG - CH1 - BEGIN - Modified the width from 15 to 27  -->
					<td width="27%">
					<!-- MAIG - CH1 - END - Modified the width from 15 to 27  -->
						<table width="30">
							<tr>
								<td>&nbsp;</td>
							</tr>
						</table>
					</td>
					<!-- end of space to left of search table-->
					<!-- select a report table -->
					<!--Product Code changed to Upload Type - .Modified by Cognizant Tier1  Line 227 in tdProdType-->
					<!--Product Code changed to Upload Type - .Modified by Cognizant Tier1 line 205 lblProductCode -->
					<td>
						<table cellSpacing="0" cellPadding="0" width="606" bgColor="#ffffcc" border="0">
							<tr id="trRejectLableRow" runat="server">
								<td class="arial_13_bold" colSpan="3" height="22"><IMG src="../images/red_arrow.gif" align="textTop">
									&nbsp;&nbsp;Items rejected have been removed and the summary has been updated.</td>
								<td bgColor="#ffffff"></td>
							</tr>
							<tr id="trRejectRow" bgColor="#ffffff" runat="server">
								<td colSpan="4" height="22"></td>
							</tr>
							<tr>
								<td class="arial_11_bold_white" width="604" bgColor="#333333" colSpan="4" height="22">&nbsp;&nbsp; 
									My Reports
								</td>
							</tr>
							<tr>
								<td class="style1">&nbsp;&nbsp;</td>
								<td class="arial_12_bold" width="45%">Report</td>
								<td class="arial_12_bold" width="42%">Status</td>
								<td class="arial_12_bold" width="10%"></td>
							</tr>
							<tr>
								<td height="22" class="style1">&nbsp;</td>
								<td class="arial_12" height="22"><asp:dropdownlist id="cboReportType" runat="server" AutoPostBack="True" size="1" Width="223px" DataTextField="Description" DataValueField="ID" onselectedindexchanged="cboReportType_SelectedIndexChanged"></asp:dropdownlist></td>
								<td colSpan="2" height="22"><asp:dropdownlist id="cboStatus" runat="server" AutoPostBack="True" size="1" Width="223px" DataTextField="Description" DataValueField="ID" onselectedindexchanged="cboStatus_SelectedIndexChanged"></asp:dropdownlist>
                                    <br />
                                </td>
							</tr>
							<%--PC Phase II changes CH1 - Start - Modified the below code to handle Date Time Picker--%>
							<%--<tr id="trDates" runat="server">
								<td colSpan="4">
									<table cellSpacing="0" cellPadding="1" width="100%" align="left">
										<tr>
											<td>&nbsp;</td>
											<td align="left">
												<table cellSpacing="0" cellPadding="0" width="100%" align="left" border="0">
													<uc:dates id="Dates" runat="server" visible="false"></uc:dates>
													</table>
											</td>
											
										</tr>
									</table>
								</td>
							</tr>--%>
									<!-- MAIG - CH2 - BEGIN - Modified the below code to remove the &nbsp to td -->
							          <tr>
							          <td  height="15 px">
							          
							          </td>
							          </tr>      
                        <tr>
                            <td class="arial_12_bold" colSpan="4">
                            <table>
                            <!-- CHG0109406 - CH1 - BEGIN - Appended the timezone Arizona along with the Date Label -->
                            <%--CHG0110069 - CH1 - BEGIN - Appended the /Time along with the Date Label--%>
                            <tr id= "Dates" runat="server" Visible="false" class="arial_12_bold">
								<td class="style1" ></td>
								<td class="arial_12_bold" width="45%">Start Date/Time (Arizona)								   
								</td>
								<td class="arial_12_bold" width="42%">End Date/Time (Arizona)								   
								</td>
								<td class="arial_12_bold" width="10%"></td>
							</tr>
							<%--CHG0110069 - CH1 - END - Appended the /Time along with the Date Label--%>
							<!-- CHG0109406 - CH1 - END - Appended the timezone Arizona along with the Date Label -->
							<tr>
								<td class="style1"></td>
								<%--MAIGEnhancement CHG0107527 - CH1 - BEGIN - Added code to set the max length of the textbox from 11 to 19 to support manual date entry --%>
								<td class="arial_12_bold" width="45%">
								     <asp:TextBox ID="TextstartDt" Visible = "false" runat="server" Width="222px" 
                                    AutoComplete="Off" MaxLength="19"></asp:TextBox>&nbsp;<a
                                    href="javascript:NewCssCal('TextstartDt','MMddyyyy','dropdown',true,'24',true)"><img id= "image" runat = "server" Visible = "false" height="16" alt="Pick a date" src="/PaymentToolimages/cal.gif"
                                        width="16" border="0"></a>
								</td>
								<td class="arial_12_bold" width="42%">
								    <asp:TextBox ID="TextendDt" runat="server" Visible="false" Width="209px" 
                                    AutoComplete="Off" MaxLength="19"></asp:TextBox>&nbsp;<a
                                    href="javascript:NewCssCal('TextendDt','MMddyyyy','dropdown',true,'24',true)"><img id="image1" runat= "server" Visible = "false" height="16" alt="Pick a date" src="/PaymentToolimages/cal.gif"
                                        width="16" border="0"></a>
								</td>
								<%--MAIGEnhancement CHG0107527 - CH1 - END - Added code to set the max length of the textbox from 11 to 19 to support manual date entry --%>
								<td class="arial_12_bold" width="10%"></td>
							</tr>
							</table>
							<!-- MAIG - CH2 - END - Modified the below code to remove the &nbsp to td -->
                            </td>
                        </tr>
                        <%--PC Phase II changes CH1 - End - Modified the below code to handle Date Time Picker--%>
							<tr>
								<td colSpan="4" height="10"></td>
							</tr>
							<tr>
								<td class="style1">&nbsp;</td>
								<td class="arial_12_bold">Rep DO</td>
								<td colSpan="2">&nbsp;&nbsp;</td>
							</tr>
							<tr>
								<td class="style1">&nbsp;</td>
								<td class="arial_12" colSpan="3"><asp:dropdownlist id="_RepDO" AutoPostBack="True" Width="223px" DataTextField="Description" DataValueField="ID" Runat="server" onselectedindexchanged="_RepDO_SelectedIndexChanged"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td colSpan="4" height="10"></td>
							</tr>
							<tr>
								<td class="style1">&nbsp;</td>
								<td class="arial_12"><b>Created By</b> hold down 'Ctrl' to select multiple</td>
								<td colSpan="2" class="arial_12_bold">&nbsp;&nbsp;&nbsp;<asp:Label id="lblApprovers" runat="server" Visible="true">Approved By</asp:Label></td>
							</tr>
							<tr>
								<td class="style1">&nbsp;</td>
								<td class="arial_12"><asp:listbox id="UserList" runat="server" cssclass="arial_12" selectionmode="Multiple" rows="10" Width="260px"></asp:listbox></td>
								<td class="arial_12" colSpan="2" valign="top">
									&nbsp;&nbsp;&nbsp;<asp:dropdownlist id="_Approvers" size="1" runat="server" cssclass="arial_12" DataValueField="ID" Visible="true" DataTextField="Description" Width="223px"></asp:dropdownlist></td>
							</tr>
							<tr>
								<td colSpan="4" height="10"></td>
							</tr>
							<tr>
								<td vAlign="center" align="right" bgColor="#cccccc" colSpan="4" height="17"><A id="A1" href="CashierReconciliation_Report.aspx" runat="server">
								 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
								 <IMG height="17" src="/PaymentToolimages/btn_reset.gif" width="51" border="0"></A>&nbsp;
									 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
									 <asp:imagebutton id="btnSubmit" runat="server" imageurl="/PaymentToolimages/btn_submit.gif" width="67px" height="17" onclick="btnSubmit_Click"></asp:imagebutton>&nbsp;
								</td>
							</tr>
						</table>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td colSpan="3" height="10">&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td><asp:label class="arial_12_bold_red" id="lblErrMsg" runat="server">&nbsp;</asp:label></td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td class="arial_11">&nbsp;&nbsp;<asp:label id="lblPageNum1" runat="server"></asp:label>
					<td height="10">&nbsp;</td>
				</tr>
				<tr id="trConvertPrint" runat="server">
					<td>&nbsp;</td>
					<td align="left">
						<table width="604">
							<tr>
								<td align="right" bgColor="#cccccc"><A onclick="return Open_Report('XLS')" href="CashierRecon_Printxls.aspx?type=XLS" target="_blank" runat="server">
								 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
								 <IMG height="17" src="/PaymentToolimages/btn_convert.gif" width="135" border="0"></A>&nbsp;
									<A onclick="return Open_Report('PRINT')" href="CashierRecon_Printxls.aspx?type=PRINT" target="_blank" runat="server">
										 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
										 <IMG height="17" src="/PaymentToolimages/btn_print.gif" width="112" border="0"></A>&nbsp;
								</td>
							</tr>
						</table>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td height="22">&nbsp;</td>
					<td>
						<table id="tblReportInfo" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
							<tr>
								<td class="arial_12_bold" width="120">&nbsp;&nbsp;<asp:label id="lblDateRange" runat="server"></asp:label></td>
								<td><asp:label id="lblRunDate" runat="server" CssClass="arial_12"></asp:label></td>
							</tr>
							<tr id="trDateRange" runat="server">
								<td class="arial_12_bold" width="120">&nbsp;&nbsp;<asp:label id="lblDate" runat="server"></asp:label></td>
								<td><asp:label id="lblDateR" runat="server" CssClass="arial_12"></asp:label></td>
							</tr>
							<tr>
								<td class="arial_12_bold" width="120">&nbsp;&nbsp;<asp:label id="lblReportStatusName" runat="server"></asp:label></td>
								<td><asp:label id="lblReportStatus" runat="server" CssClass="arial_12"></asp:label></td>
							</tr>
							<tr>
								<td class="arial_12_bold" width="120">&nbsp;&nbsp;<asp:label id="lblCsr" runat="server"></asp:label></td>
								<td><asp:label id="lblCsrs" runat="server" CssClass="arial_12"></asp:label></td>
							</tr>
							<tr>
								<td class="arial_12_bold" width="120">&nbsp;&nbsp;<asp:label id="lblRepDo" runat="server"></asp:label></td>
								<td><asp:label id="lblRep" runat="server" CssClass="arial_12"></asp:label></td>
							</tr>
							<tr id="trApprover" runat="server">
								<td class="arial_12_bold" width="120">&nbsp;&nbsp;Approved By:</td>
								<td><asp:label id="lblApprover" runat="server" CssClass="arial_12"></asp:label></td>
							</tr>
						</table>
					</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>&nbsp;</td>
					<td colSpan="2">
						<table id="tblRepeater" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0" runat="server">
							<tr>
								<td>
									<table style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="2" width="100%" bgColor="#cccccc" border="1">
										<asp:repeater id="rptCashierApproveRepeater" runat="server" OnItemDataBound="rptCashierApproveRepeater_ItemBound">
											<HeaderTemplate>
												<tr class="item_template_header">
												<%-- CHG0109406 - CH2 - BEGIN - Added the td which displays the Timezone information --%>
													<td class="arial_12_bold" colspan="7" style="border-style:none" id="tdHeader1" runat="server">
														Cashier's Reconciliation Reject or Approve/Upload</td>
														<td class="arial_12_bold" colspan="5" style="font-style:italic;text-align:right;border-style:none" runat="server">All timezones are in Arizona</td>
												<%-- CHG0109406 - CH2 - END - Added the td which displays the Timezone information --%>
												</tr>
												<!-- STAR Retrofit III.Ch1: START - Modified to left align the amount tag in the report header -->
												<!--TimeZoneChange.Ch1-Changed the column header Receipt Date to Receipt Date/Time(MST Arizona)on 8-31-2010-->
												<tr class="item_template_header_white">
													<td class="arial_12_bold" width="10" id="tdChkHead" runat="server">Select</td>
													<%-- CHG0109406 - CH3 - BEGIN - Modified the Column name from "Receipt Date/Time(MST Arizona)" to Receipt Date/Time(Arizona) --%>
													<td class="arial_12_bold" width="100">Receipt Date/Time(Arizona)</td>
													<%-- CHG0109406 - CH3 - END - Modified the Column name from "Receipt Date/Time(MST Arizona)" to Receipt Date/Time(Arizona) --%>
													<td class="arial_12_bold" width="60">Status</td>
                                                    <%-- CHG0113938 - BEGIN - Modified the Column name from "Member Name" to "Name" --%>
													<td class="arial_12_bold" width="80">Name</td>
                                                    <%-- CHG0113938 - End - Modified the Column name from "Member Name" to "Name" --%>
													<td class="arial_12_bold" width="80">Product Type</td>
													<td class="arial_12_bold" width="60">Trans Type</td>
                                                    <%-- CHG0113938 - BEGIN - Modified the Column name from "Policy/Mbr Number" to "Policy Number" --%>
													<td class="arial_12_bold" width="50">Policy Number</td>
                                                    <%-- CHG0113938 - END - Modified the Column name from "Policy/Mbr Number" to "Policy Number" --%>
													<td class="arial_12_bold" width="70">Receipt Number</td>
													<td class="arial_12_bold" width="40">Pymt Method</td>
													<td class="arial_12_bold" width="80" id="tdApprovedByHead" runat="server">Approved 
														By</td>
														<%-- CHG0109406 - CH4 - BEGIN - Modified the Column name from "Approved Date/Time(MST Arizona)" to Approved Date/Time(Arizona) --%>
													<td class="arial_12_bold" width="40" id="tdApprovedDateHead" runat="server">Approved 
														Date/Time(Arizona)</td>
														<%-- CHG0109406 - CH4 - END - Modified the Column name from "Approved Date/Time(MST Arizona)" to Approved Date/Time(Arizona) --%>
													<td class="arial_12_bold" align="left">Amount</td>
												</tr>
												<!-- STAR Retrofit III.Ch1: END -->
											</HeaderTemplate>
											<ItemTemplate>
												<tr id="trItemRow" runat="server" class="item_template_alt_row">
													<td class="arial_12" align="center" id="tdChkItem" runat="server">
														<table cellpadding="10">
															<tr>
																<td>
																	<asp:CheckBox ID="cbSelect" Runat="server" Checked="True"></asp:CheckBox>
																</td>
															</tr>
														</table>
													</td>
													<td class="arial_12">
														<asp:Label ID="lblReceiptDate" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Receipt Date") %>' >
														</asp:Label>&nbsp;
													</td>
													<td class="arial_12">
														<asp:Label ID="lblStatus" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Status") %>' >
														</asp:Label>&nbsp;
													</td>
													<td class="arial_12">
														<asp:Label ID="lblMemberName" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Member Name") %>' >
														</asp:Label>&nbsp;
													</td>
													<td class="arial_12" id="tdProdType" runat="server" UploadType = '<%# DataBinder.Eval(Container.DataItem, "Upload Type") %>' ><%# DataBinder.Eval(Container.DataItem, "Product Type") %>
													</td>
													<td class="arial_12" id="tdTransType" runat="server"><%# DataBinder.Eval(Container.DataItem, "Trans Type") %>
													</td>
													<td class="arial_12" id="tdpolicynumber" runat="server"><%# DataBinder.Eval(Container.DataItem, "Policy Number") %>
													</td>
													<td class="arial_12" valign="middle"><%# DataBinder.Eval(Container.DataItem, "Receipt Number") %>
														<asp:Label ID="lblReceiptNo" Runat ="server" visible="False" value = '<%# DataBinder.Eval(Container.DataItem, "Receipt Number") %>' >
														</asp:Label>&nbsp;
													</td>
													<td class="arial_12" id="tdPymtType" runat="server"><%# DataBinder.Eval(Container.DataItem, "Pymt Method") %>
													</td>
													<td class="arial_12" id="tdApprovedByItem" runat="server">
														<asp:Label ID="lblApprovedBy" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Approved By") %>' >
														</asp:Label>&nbsp;
													</td>
													<td class="arial_12" id="tdApprovedDateItem" runat="server">
														<asp:Label ID="lblApprovedDate" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Approved Date") %>' >
														</asp:Label>&nbsp;
													</td>
													<td class="arial_12" align="right" id="tdAmount" runat="server"><%# DataBinder.Eval(Container.DataItem, "Amount","{0:c}") %>
													</td>
												</tr>
											</ItemTemplate>
											<FooterTemplate>
												<tr class="item_template_row">
													<td class="total_cell_arial_12_bold" colspan="9" align="right" id="tdTotalHead" runat="server">TOTAL 
														&nbsp;&nbsp;&nbsp;
													</td>
													<td id="tdTotal" runat="server" class="total_cell_arial_12_bold" align="right">
														<asp:Label ID="lblTotal" Runat="server"></asp:Label>
													</td>
												</tr>
												<tr>
													<td id="tdTurnIn" runat="server" vAlign="middle" align="right" bgColor="#cccccc" colSpan="12">
														<asp:CustomValidator ID="custvldApproveReject" Runat="server" ClientValidationFunction="captureevent" Display="None"></asp:CustomValidator>
														 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
														 <asp:imagebutton id="btnUpdateButton" CommandName="UpdateButton" runat="server" imageurl="/PaymentToolimages/btn_update_turnin.gif" causesvalidation="False"></asp:imagebutton>&nbsp;
														 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
														 <asp:imagebutton id="btnReject" CommandName="Reject" runat="server" imageurl="/PaymentToolimages/btn_reject.gif" width="64px" height="17"></asp:imagebutton>&nbsp;
														 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
														 <asp:imagebutton id="btnApprove" CommandName="Approve" runat="server" imageurl="/PaymentToolimages/btn_approve.gif" width="78px" height="17"></asp:imagebutton>&nbsp;
													</td>
												</tr>
											</FooterTemplate>
										</asp:repeater></table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr id="trBlankRow1" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
				</tr>
				<tr id="trCaption" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td class="arial_13_bold" bgColor="#ffffff">Cashiering Summary Report(Pending 
						Approval)</td>
					<td bgColor="#ffffff">&nbsp;</td>
				</tr>
				<tr id="trCollSummarySplit" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
				</tr>
				<tr id="trCollSummary" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td colSpan="2">
						<table id="tblCollSummary" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="5" bgColor="#cccccc" border="1" runat="server">
						</table>
					</td>
				</tr>
				<tr id="trOtherCollSummarySplit" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
				</tr>
				<tr id="trOtherCollSummary" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td colSpan="2">
						<table id="tblOtherCollSummary" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="5" bgColor="#cccccc" border="1" runat="server">
						</table>
					</td>
				</tr>
				<tr id="trBlankRow2" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
					<td bgColor="#ffffff">&nbsp;</td>
				</tr>
				<tr id="trBlankRow3" runat="server">
					<td bgColor="#ffffff">&nbsp;</td>
					<td colSpan="2">
						<table id="tblTotalCollSummary" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="5" width="300" bgColor="#cccccc" border="1" runat="server">
						</table>
					</td>
				</tr>
			</table>
			<!--Repeater for Membership Detail-->
			<table id="tblMemberRepeater" cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffff" border="0" runat="server">
				<tr>
					<td>
						<table style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="2" width="100%" bgColor="#cccccc" border="1">
							<asp:repeater id="rptMembershipDetail" runat="server" OnItemDataBound="rptMembershipDetail_ItemBound">
								<HeaderTemplate>
									<tr class="item_template_header">
										<td class="arial_12_bold" colspan="22" runat="server" ID="Td1" NAME="Td1">
											Membership Detail Report</td>
									</tr>
									<!-- STAR Retrofit III.Ch1: START - Modified to left align the amount tag in the report header -->
									<tr class="item_template_header_white">
										<td class="arial_12_bold" width="7%">Member Last Name</td>
										<td class="arial_12_bold" width="7%">Member First Name</td>
										<td class="arial_12_bold" width="2%">M.I.</td>
										<td class="arial_12_bold" width="8%">Address</td>
										<td class="arial_12_bold" width="9%">City</td>
										<td class="arial_12_bold" width="2%">State</td>
										<td class="arial_12_bold" width="6%">Zip</td>
										<td class="arial_12_bold" width="6%">DOB</td>
										<td class="arial_12_bold" width="2%">Gen der</td>
										<td class="arial_12_bold" width="5%">Day Phone</td>
										<td class="arial_12_bold" width="5%">Eve Phone</td>
										<td class="arial_12_bold" width="7%">Mbr Number</td>
										<td class="arial_12_bold" width="7%">Role</td>
										<td class="arial_12_bold" width="4%">User(s)</td>
										<td class="arial_12_bold" width="2%">Src Code</td>
										<td class="arial_12_bold" width="2%">Base Year</td>
										<td class="arial_12_bold" width="2%">Prod Type</td>
										<td class="arial_12_bold" width="2%">Effective Date</td>
										<td class="arial_12_bold" width="80" id="tdMbrApprovedByHead" runat="server">Approved 
											By</td>
										<td class="arial_12_bold" width="40" id="tdMbrApprovedDateHead" runat="server">Approved 
											Date/Time(MST Arizona)</td>
										<td class="arial_12_bold" align="left">Amount</td>
										<td class="arial_12_bold" width="7%">Pymt Type</td>
									</tr>
									<!-- STAR Retrofit III.Ch1: END -->
								</HeaderTemplate>
								<ItemTemplate>
									<tr id="trItemRowMember" runat="server" class="item_template_alt_row">
										<td class="arial_12" id="tdMemberLastname" runat="server"><%# DataBinder.Eval(Container.DataItem, "Member Last Name") %>
											<asp:Label ID="lblOrderID" Runat ="server" visible="False" value = '<%# DataBinder.Eval(Container.DataItem, "Order ID") %>' >
											</asp:Label>
										</td>
										<td class="arial_12" id="tdMemberFirstName" runat="server"><%# DataBinder.Eval(Container.DataItem, "Member First Name") %>
										</td>
										<td class="arial_12" id="tdMI" runat="server"><%# DataBinder.Eval(Container.DataItem, "MI") %>
										</td>
										<td class="arial_12">
											<asp:Label ID="lblAddress" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Address") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblCity" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "City") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblState" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "State") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblZip" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Zip") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12" id="tdDOB" runat="server"><%# DataBinder.Eval(Container.DataItem, "DOB") %>
										</td>
										<td class="arial_12" id="tdGender" runat="server"><%# DataBinder.Eval(Container.DataItem, "Gender") %>
										</td>
										<td class="arial_12">
											<asp:Label ID="lblDayPhone" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Day Phone") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblEvePhone" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Eve Phone") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblMemberNumber" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Mbr Number") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12" id="tdRole" runat="server"><%# DataBinder.Eval(Container.DataItem, "Role") %>
										</td>
										<td class="arial_12">
											<asp:Label ID="lblMemberUsers" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Users") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblSrcCode" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Src Code") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblBaseYear" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Base Year") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblProdType" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Prod Type") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblEffectiveDate" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Effective Date") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12" id="tdMbrApprovedByItem" runat="server">
											<asp:Label ID="lblMbrApprovedBy" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Approved By") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12" id="tdMbrApprovedDateItem" runat="server">
											<asp:Label ID="lblMbrApprovedDate" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Approved Date") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12" align="right">
											<asp:Label ID="lblAmount" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Amount","{0:c}") %>' >
											</asp:Label>&nbsp;
										</td>
										<td class="arial_12">
											<asp:Label ID="lblPymtType" Runat ="server" text = '<%# DataBinder.Eval(Container.DataItem, "Pymt Type") %>' >
											</asp:Label>&nbsp;
										</td>
									</tr>
								</ItemTemplate>
								<FooterTemplate>
									<tr class="item_template_row">
										<td class="total_cell_arial_12_bold" colspan="20" align="right" id="tdTotalMemberHead" runat="server">TOTAL 
											&nbsp;&nbsp;&nbsp;
										</td>
										<td id="tdTotalMember" runat="server" class="total_cell_arial_12_bold" align="right">
											<asp:Label ID="lblTotalMember" Runat="server"></asp:Label>
										</td>
										<td>&nbsp;</td>
									</tr>
									<tr>
										<td bgColor="#cccccc" colSpan="22" height="17">
										</td>
									</tr>
								</FooterTemplate>
							</asp:repeater></table>
					</td>
				</tr>
			</table>
			<!--End of membership Repeater-->
			<!--Cross Reference Datagrid-->
			<table id="tblCrossReference" cellSpacing="0" cellPadding="0" width="100%" border="0" runat="server">
				<tr>
					<td width="15%">&nbsp;</td>
					<td class="arial_12_bold" width="604" bgColor="#cccccc">Cross Reference Report</td>
					<td>&nbsp;</td>
				</tr>
				<tr>
<%--Changes made to display the pager properly,changed to grid view from data grid by Cognizant on 08/13/2010--%>
					<td width="15%">&nbsp;</td><%--OnRowDataBound="dgCrossReference_ItemBound"--%>
					<td width="604">
                        <asp:GridView ID="dgCrossReference"  runat="server" 
                            CssClass="arial_11" PageSize="25" 
                                            AllowPaging="true" OnPageIndexChanging="CrossReferencePageIndexChanged" 
                                            PagerSettings-Mode="NextPrevious" PagerSettings-Position="TopAndBottom" 
                                            BorderStyle="none" CellPadding="2" CellSpacing="0" 
                            onrowdatabound="dgCrossReference_ItemBound">
										    <AlternatingRowStyle BackColor="LightYellow" />
<PagerSettings Mode="NextPrevious" Position="TopAndBottom"></PagerSettings>

										    <RowStyle CssClass="arial_10" />
										    <HeaderStyle CssClass="arial_11_bold" BackColor="#ffffff" />
                                            <PagerTemplate>
                                            <table id="tbPager" width="100%" style="background-color: #eeeeee; color: Blue; font-size: 7pt;
                                                border: none;">
                                                <tr>
                                                    <td align="right">
                                                        <asp:LinkButton runat="server" ID="vPrev" CommandName="Page" CommandArgument="Prev"
                                                            Text="PREVIOUS" />
                                                        <asp:LinkButton runat="server" ID="seperator" CommandName="Page" CommandArgument="Prev"
                                                            Text="|" Enabled="false" />
                                                        <asp:LinkButton runat="server" ID="vNext" CommandName="Page" CommandArgument="Next"
                                                            Text="NEXT" />
                                                    </td>
                                                </tr>
                                            </table>
                                            </PagerTemplate>
                                            </asp:GridView>
					</td>
					<td></td>
				</tr>
				<tr>
					<td width="15%">&nbsp;</td>
					<td class="arial_11">&nbsp;&nbsp;<asp:label id="lblPageNum2" runat="server"></asp:label>
					</td>
					<td></td>
				</tr>
			</table>
			<!--End of Cross Reference Datagrid--></form>
	</body>
</HTML>
