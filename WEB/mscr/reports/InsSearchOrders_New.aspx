<%@ Page language="c#" Codebehind="InsSearchOrders_New.aspx.cs" AutoEventWireup="True" Inherits="MSCR.Reports.InsSearchOrders_New" %>
<!-- Q1-Retrofit.ch1: START - Modified the file name InsSearchOrders.aspx to InsSearchOrders_New.aspx for Transaction Search as part of CSR#4833-->
<!-- STAR Retrofit III Changes: -->
<!-- 04/02/2007 - Changes done as part of CSR #5595-->
<!-- STAR Retrofit III.Ch1: Modified to include javascript Date Validation file-->
<!-- STAR Retrofit III.Ch2: Modified to include validators for validating dates-->
<!--MODIFIED BY COGNIZANT AS A PART OF TIME ZONE CHANGE ON 8-31-2010
TimeZoneChange.Ch1:Changed the width of the td for lblRunDate and lblDateRange
67811A0  - PCI Remediation for Payment systems CH1 : commented the credit card number prefix column as a part of security scan defect by cognizant on 01/09/2011
PC Phase II CH1 - Modified the page such that only limited filters will be available in Search page.
<%--PC Phase II changes CH2 - Added the below code to Handle Date Time Picker --%>
<%--PC Phase II changes CH2 - Added the below code to Handle Date Time Picker --%>
MAIG - CH1 - Modified verbiage to remove the Sales & Service
MAIG - CH2 - Modified the attribute align to Right
MAIG - CH3 - Modified the javascript code in OnKeyPress event to support Firefox browser
MAIG - CH4 - Modified the javascript code in OnKeyPress event to support Firefox browser
MAIG - CH5 - New Text box for entering UserID as Txn Search Criteria
MAIG - CH6 - Modified the javascript code in OnKeyPress event to support Firefox browser
MAIG - CH7 - Modified the javascript code in OnKeyPress event to support Firefox browser
MAIG - CH8 - Added code to set the max length of the textbox from 12 to 19 to support IE 11 browser
CHG0109406 - CH1 - Appended the timezone Arizona along with the Date Label
CHG0109406 - CH2 - Added a label lblHeaderTimeZone to display the timezone for the results displayed 
CHG0109406 - CH3 - Change to update ECHECK to ACH - 01202015
CHG0110069 - CH1 - Appended the /Time along with the Date Label
-->
<HTML>
	<HEAD>
		<!-- MAIG - CH1 - BEGIN - Modified verbiage to remove the Sales & Service  -->
		<title>Payment Tool</title> 
		<!-- MAIG - CH1 - END - Modified verbiage to remove the Sales & Service  -->
		<!-- Q1-Retrofit.ch1: END -->
		<!-- START 
		Code Added by Cognizant on 05/24/2004 for calling a javascript function which open's the receipt page
		-->
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
		<script type="text/javascript" src="../Reports/CalendarControl.js"></script>   
	</HEAD>
	<!-- MAIG - CH2 - BEGIN - Modified the attribute align to Right  -->
	<body align="right">
	<!-- MAIG - CH2 - END - Modified the attribute align to Right  -->
		<table cellSpacing="0" cellPadding="0" width="750" border="0">
			<tr>
				<td colSpan="2" height="10"></td>
			</tr>
		</table>
		<!-- select a report table contains 3 tables: space:30, search table:604, space:116 -->
		<form id="frmSearch" name="form1" runat="server">
			<!--STAR Retrofit III.Ch1: START - Modified to include javascript Date Validation file -->
			<script type="text/javascript" src="../Reports/DateValidate.js"> </script>
			<!--STAR Retrofit III.Ch1: END -->
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
						<!-- select a report table 604: 120, 230, 254-->
						<td>
							<table cellSpacing="0" cellPadding="0" width="644" bgColor="#ffffcc" border="0">
								<tbody>
									<tr>
										<td class="arial_11_bold_white" bgColor="#333333" colSpan="3" height="22">&nbsp;&nbsp;Transaction 
											Search</td>
									</tr>
									<tr>
										<td width="200" height="10"></td>
										<td width="250"></td>
										<td width="204"></td>
									</tr>
									<tr>
										<td class="arial_11" colSpan="3">&nbsp;&nbsp;Search all membership and insurance 
											transactions by one or more criteria:</td>
									</tr>
									<tr>
										<td colSpan="3" height="10"></td>
									</tr>
									<tr>
									<!--CHG0109406 - CH1 - BEGIN - Appended the timezone Arizona along with the Date Label -->
									<%--CHG0110069 - CH1 - BEGIN - Appended the /Time along with the Date Label--%>
										<td class="arial_12_bold">&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;Start Date/Time (Arizona)</td>
										<td class="arial_12_bold">End Date/Time (Arizona)</td>
										<%--CHG0110069 - CH1 - END - Appended the /Time along with the Date Label--%>
									<!--CHG0109406 - CH1 - END - Appended the timezone Arizona along with the Date Label -->
										<td></td>
									</tr>
									<%--MAIG - CH8 - BEGIN - Added code to set the max length of the textbox from 12 to 19 to support IE 11 browser--%>
									<td  class="arial_12" colSpan="5">                                
                                &nbsp;&nbsp;&nbsp;&nbsp;                                
                                <asp:TextBox ID="TxtstartDt" runat="server" CssClass="style1" Width="130px"
                                    AutoComplete="Off" MaxLength="19"></asp:TextBox>&nbsp;<a href="javascript:NewCssCal('TxtstartDt','MMddyyyy','dropdown',true,'24',true)"><img
                                        height="16" alt="Pick a date" src="/PaymentToolimages/cal.gif" width="16"
                                        border="0"></a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;                         
                           
                                
                                <asp:TextBox ID="TxtendDt" runat="server" CssClass="style1" Width="130px"
                                    AutoComplete="Off" MaxLength="19"></asp:TextBox>&nbsp;<a href="javascript:NewCssCal('TxtendDt','MMddyyyy','dropdown',true,'24',true);"><img
                                        height="16" alt="Pick a date" src="/PaymentToolimages/cal.gif" width="16" 
                                        border="0"></a>
									</td>
									<%--MAIG - CH8 - END - Added code to set the max length of the textbox from 12 to 19 to support IE 11 browser--%>
									<%--<tr>
										<td class="arial_12" width="232" height="12">&nbsp; 
											<!--STAR Retrofit III.Ch2: START - Modified to include validator for validating dates -->
											<asp:dropdownlist id="start_date_month" runat="server"></asp:dropdownlist><asp:dropdownlist id="start_date_day" runat="server"></asp:dropdownlist><asp:dropdownlist id="start_date_year" runat="server"></asp:dropdownlist>
											<asp:customvalidator id="CustomStartDateValidatorCtl" runat="server" ClientValidationFunction="CallStartDateFun" ControlToValidate="start_date_day"></asp:customvalidator><asp:customvalidator id="CustomStartMonthValidatorCtl" runat="server" ClientValidationFunction="CallStartDateFun" ControlToValidate="start_date_month"></asp:customvalidator><asp:customvalidator id="CustomStartYearValidatorCtl" runat="server" ClientValidationFunction="CallStartDateFun" ControlToValidate="start_date_year"></asp:customvalidator>
										</td>
										<td class="arial_12" height="12"><asp:dropdownlist id="end_date_month" runat="server"></asp:dropdownlist><asp:dropdownlist id="end_date_day" runat="server"></asp:dropdownlist><asp:dropdownlist id="end_date_year" runat="server"></asp:dropdownlist>
											<asp:customvalidator id="CustomEndDateValidatorCtl" runat="server" ClientValidationFunction="CallEndDateFun" ControlToValidate="end_date_day"></asp:customvalidator><asp:customvalidator id="CustomEndMonthValidatorCtl" runat="server" ClientValidationFunction="CallEndDateFun" ControlToValidate="end_date_month"></asp:customvalidator><asp:customvalidator id="CustomEndYearValidatorCtl" runat="server" ClientValidationFunction="CallEndDateFun" ControlToValidate="end_date_year"></asp:customvalidator>
											<!--STAR Retrofit III.Ch2: END--></td>
										<td></td>
									</tr>--%>
									<TR>
										<TD class="arial_12_bold" colSpan="3" height="6"></TD>
									</TR>
									<TR>
										<%--<TD class="arial_12_bold" colSpan="1">&nbsp;&nbsp;&nbsp;&nbsp; Criteria</TD>--%>
										<TD class="arial_12" colSpan="1"></TD>
										<TD colSpan="1"></TD>
									</TR>
									<TR>
										<TD class="arial_12_bold" colSpan="3" height="8"></TD>
									</TR>
									<!-- START -->
									<!-- Code added by COGNIZANT 05/26/2004	-->
									<!-- New Textbox added to capture Receipt Number as a search criteria -->
									<TR>
										<TD class="arial_12_bold" colSpan="1" height="8">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Application:</TD>
										<TD class="arial_12" colSpan="1" height="8">
											<asp:dropdownlist id="_Application" runat="server" datatextfield="RepDescription" DataValueField="Description"></asp:dropdownlist></TD>
										<TD colSpan="1" height="8"></TD>
									</TR>
									<TR>
										<TD class="arial_12_bold" colSpan="3" height="3"></TD>
									</TR>
									<TR>
										<TD class="arial_12_bold" colSpan="1" height="15">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Product:</TD>
										<TD class="arial_12" colSpan="1" height="15"><asp:dropdownlist id="_ProductType" runat="server" datatextfield="Description" datavaluefield="ID"></asp:dropdownlist></TD>
										<TD colSpan="1" height="15"></TD>
									</TR>
									<!-- END -->
									<TR>
										<TD class="arial_12_bold" colSpan="3" height="3"></TD>
									</TR>									
									<!-- Begin Phase II Changes CH1 - Modified the page such that only limited filters will be available in Search page and removed the following fields Customer Last name, Authorization Number,Credit Card Number,Echeck Account Number, Amount, Merchant Ref Number  -->
									<%--<tr>
										<td class="arial_12_bold">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Customer Last Name:</td>
										<td class="arial_12"><asp:textbox id="txtLastName" runat="server" width="200px" maxlength="40"></asp:textbox></td>
										<td></td>
									</tr>
									<tr>
										<td colSpan="3" height="3"></td>
									</tr>
									<tr>
										<td class="arial_12_bold">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Authorization Number:</td>
										<td class="arial_12"><asp:textbox id="txtCCAuthCode" runat="server" width="200px" maxlength="6"></asp:textbox></td>
										<td></td>
									</tr>
									--%>
									
									<tr>
										<td class="arial_12_bold">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Credit Card Number:</td>
										<td class="arial_12"> XXXX-XXXX-XXXX-
											<!-- MAIG - CH3 - BEGIN - Modified the javascript code in OnKeyPress event to support Firefox browser  -->
											<asp:textbox id="txtCCSuffix" runat="server" onkeypress="return digits_only_onkeypress(event)" width="65px" maxlength="4"></asp:textbox></td>
											<!-- MAIG - CH3 - END - Modified the javascript code in OnKeyPress event to support Firefox browser  -->
										<td></td>
									</tr>
									<tr>
										<td colSpan="3" height="3"></td>
									</tr>
									<tr>
									<!-- CHG0109406 - CH3 - Begin - Change to update ECHECK to ACH - 01202015 -->
										<td class="arial_12_bold">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;ACH Account Number:</td>
									<!-- CHG0109406 - CH3 - End - Change to update ECHECK to ACH - 01202015 -->
										<td class="arial_12"> XXXX-XXXX-XXXX-
											<!-- MAIG - CH4 - BEGIN - Modified the javascript code in OnKeyPress event to support Firefox browser  -->
											<asp:textbox id="txtEcheckAcc" runat="server" onkeypress="return digits_only_onkeypress(event)" width="65px" maxlength="4"></asp:textbox></td>
											<!-- MAIG - CH4 - END - Modified the javascript code in OnKeyPress event to support Firefox browser  -->
										<td></td>
									</tr>
									<tr>
										<td colSpan="3" height="3"></td>
									</tr>
									<!-- MAIG - CH5 - BEGIN - New Text box for entering UserID as Txn Search Criteria -->
									<tr>
									<td colSpan="3" height="3">
									    <table>
									        <tr>	
									            <td class="arial_12_bold" width="190">&nbsp;&nbsp;&nbsp;&nbsp;Agent ID:</td>								        
								                <td><asp:textbox id="txtAgentId" runat="server" MaxLength="10" onkeypress="return digits_only_onkeypress(event)" width="100px"></asp:textbox></td>								
								                <td class="arial_12_bold" width="75" align=right>&nbsp;&nbsp;User ID:&nbsp;&nbsp;</td>								        
								                <td><asp:textbox id="txtUserId" runat="server" MaxLength="10" onkeypress="return alphanumeric_onkeypress(event)" width="100px"></asp:textbox></td>
								            </tr>
								        </table>
								    </td>
							        </tr>
							        <tr>
										<td colSpan="3" height="3"></td>
									</tr>
									<!-- MAIG - CH5 - END - New Text box for entering UserID as Txn Search Criteria -->
									<TR>
										<TD class="arial_12_bold">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Amount:</TD>
										<!-- MAIG - CH6 - BEGIN - Modified the javascript code in OnKeyPress event to support Firefox browser  -->
										<TD class="arial_12"><asp:textbox id="txtAmount" runat="server" onkeypress="return decimalnumeric_only_onkeypress(event)" width="200px" maxlength="7"></asp:textbox></TD>
										<!-- MAIG - CH6 - END - Modified the javascript code in OnKeyPress event to support Firefox browser  -->
										<TD></TD>
									</TR>
									<tr>
										<td colSpan="3" height="3"></td>
									</tr>
									<TR>
										<TD class="arial_12_bold">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Location #:</TD>
										<!-- MAIG - CH7 - BEGIN - Modified the javascript code in OnKeyPress event to support Firefox browser  -->
										<TD class="arial_12"><asp:textbox id="txtDO" runat="server" onkeypress="return digits_only_onkeypress(event)" width="200px" maxlength="10"></asp:textbox></TD>
										<!-- MAIG - CH7 - END - Modified the javascript code in OnKeyPress event to support Firefox browser  -->
										<TD></TD>
									</TR>
									<%--
									<tr>
										<td colSpan="3" height="3"></td>
									</tr>
									<TR>
										<TD class="arial_12_bold">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Merchant Ref. Number:</TD>
										<TD class="arial_12"><asp:textbox id="txtMerchantRefNbr" runat="server" width="200px" maxlength="50"></asp:textbox></TD>
										<TD></TD>
									</TR>
									<tr>
										<td colSpan="3" height="3"></td>
									</tr>  --%>
									<!--End Phase II Changes CH1 - Modified the page such that only limited filters will be available in Search page and removed the following fields Customer Last name, Authorization Number,Credit Card Number,Echeck Account Number, Amount, Merchant Ref Number  -->
									<tr>
										<td colSpan="3" height="3"></td>
									</tr>
									<tr>
										<td class="arial_12_bold">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Account Number:</td>
										<td class="arial_12"><asp:textbox id="txtAccountNbr" runat="server" width="200px" maxlength="16"></asp:textbox></td>
										<td></td>
									</tr>
									<tr>
										<td colSpan="3" height="3"></td>
									</tr>
									<TR>
										<TD class="arial_12_bold">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Receipt Number:</TD>
										<TD class="arial_12"><asp:textbox id="txtReceiptNbr" runat="server" width="200px" maxlength="25"></asp:textbox></TD>
										<TD></TD>
									</TR>
									<tr>
										<td colSpan="3" height="10"></td>
									</tr>
									<tr>
										<!-- Q1-Retrofit.ch1: START - Modified the link InsSearchOrders.aspx to InsSearchOrders_New.aspx for Transaction Search as part of CSR#4833-->
										<td vAlign="center" align="right" bgColor="#cccccc" colSpan="3" height="17"><A id="A1" href="InsSearchOrders_New.aspx" runat="server">
                                            <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
                                             <IMG height="17" src="/PaymentToolimages/btn_reset.gif" width="51" align="middle" 
                                                border="0" style="margin-left: 0px"></A>&nbsp; 
											<!-- Q1-Retrofit.ch1: END -->
											 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
											 <asp:imagebutton id="btnSubmit" runat="server" width="73px" 
                                                imagealign="Middle" imageurl="/PaymentToolimages/btn_submit.gif" height="17px" 
                                                onclick="btnSubmit_Click" ></asp:imagebutton>&nbsp;
											
										</td>
									</tr>
								</tbody></table>
						</td>
						<!-- space to right of select a report table -->
						<td>
							<table cellSpacing="0" cellPadding="0" width="116">
								<tr>
									<td width="116"></td>
								</tr>
							</table>
						</td>
						<!-- end of select a report table --></tr>
				</tbody></table>
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td colSpan="2" height="15"></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td class="arial_12_bold_red">&nbsp;&nbsp;<asp:label id="lblErrMsg" runat="server"></asp:label></td>
				</tr>
				<tr>
					<td width="30"></td>
					<td class="arial_11">&nbsp;&nbsp;<asp:label id="lblPageNum1" runat="server"></asp:label>
					</td>
				</tr>
			</table>
			<!-- 3 td's: 30, 604 (made up of 250, 50, 304), 116 --><asp:panel id="pnlTitle" runat="server"><!-- 5 td's: 30, 604 (made up of 150, 150, 304), 116 -->
				<TABLE cellSpacing="0" cellPadding="0" width="750" border="0">
					<TR>
						<TD width="30"></TD>
						<TD align="right" bgColor="#cccccc" colSpan="5"><A id="A2" href="InsSearchXlsPrint.aspx?type=PRINT" target="_blank" runat="server">
						 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
						 
						 <IMG height="17" src="/PaymentToolimages/btn_print.gif" width="112" border="0">
							</A>&nbsp;<A id="A3" href="InsSearchXlsPrint.aspx?type=XLS" target="_blank" runat="server">
								 <!-- External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
								 <IMG height="17" src="/PaymentToolimages/btn_convert.gif" width="135" border="0">
								</A>&nbsp;</TD>
						<TD width="30"></TD>
					</TR>
					<TR>
						<TD width="30"></TD>
						<TD width="1"></TD>
						<TD class="arial_12_bold" vAlign="top" width="149" bgColor="#ffffff">&nbsp;&nbsp;
							<asp:label id="lblDate" runat="server"></asp:label><BR>
							&nbsp;&nbsp;
							<asp:label id="lblDates" runat="server"></asp:label></TD>
						<TD class="arial_12" vAlign="top" width="400" bgColor="#ffffff" height="22">
							<asp:label id="lblRunDate" runat="server"></asp:label><BR>
							<asp:label id="lblDateRange" runat="server"></asp:label></TD>
						<TD width="149"></TD>
						<TD width="1"></TD>
						<TD width="116"></TD>
					</TR>
					<TR>
						<TD colSpan="7" height="5"></TD>
					</TR>
				</TABLE>
			</asp:panel>
			<!-- results table: 30, 720 -->
			<table cellSpacing="0" cellPadding="0" width="980" border="0">
				<tbody>
					<tr>
						<!--
						<td width="30"></td>
						-->
						<td>
							<!-- header for insurance transactions table -->
							<!--<table bordercolor="#cccccc" cellspacing="0" cellpadding="0" rules="none" width="720" border="0">
				<tr>
					<td valign="center" width="460" bgcolor="#cccccc" height="17">&nbsp;
					<td width="135" bgcolor="#cccccc"><asp:imagebutton id="convert_to_excel" runat="server" imageurl="/PaymentToolimages/btn_convert.gif" imagealign="Middle"></asp:imagebutton></td>
					<td width="3" bgcolor="#cccccc"></td>
					<td width="112" bgcolor="#cccccc"><asp:imagebutton id="print_version" runat="server" imageurl="/PaymentToolimages/btn_print.gif" imagealign="Middle"></asp:imagebutton></td>
					<td width="10" bgcolor="#cccccc"></td>
				</tr>
			</table>-->
							<!-- column names and data rows for insurance transactions table -->
							<!-- header for insurance transactions table -->
							<table borderColor="#cccccc" cellSpacing="0" cellPadding="0" rules="none" border="0">
								<tr>
									<td vAlign="center" bgColor="#cccccc" height="17"><asp:label id="lblInsTitle" runat="server" cssclass="arial_12_bold" backcolor="#cccccc"></asp:label></td>
								</tr>
								<!--</table>-->
								<!--<table bordercolor="#cccccc" cellspacing="0" cellpadding="0" rules="none" border="0">-->
								<tr>
									<td><br>
									</td>
								</tr>
							</table>
							<!-- header for membership transactions table -->
							<table borderColor="#cccccc" cellSpacing="0" cellPadding="0" rules="none" border="0">
								<tr>
									<td vAlign="center"  height="17">
									<!-- CHG0109406 - CH2 - BEGIN - Added a label lblHeaderTimeZone to display the timezone for the results displayed  -->
									<table width="100%" id="tblHeader" visible="false" bgColor="#cccccc" runat="server">
									<tr>
									<td  width="20%"><asp:label id="lblMbrTitle" runat="server" cssclass="arial_12_bold"></asp:label></td>
									<td align="right" width="80%"><asp:Label ID="lblHeaderTimeZone" runat="server" CssClass="arial_12_bold" style="font-style:italic;" Text="All timezones are in Arizona" /></td>
									</tr>
									</table>
									<!-- CHG0109406 - CH2 - END - Added a label lblHeaderTimeZone to display the timezone for the results displayed  -->
									</td>
									</tr>
								<!-- </table> -->
								<!-- column names and data rows for membership transactions table -->
								<!--
							<table bordercolor="#cccccc" cellspacing="0" cellpadding="0" rules="none" border="0">
							-->
								<tr>
									<td><asp:GridView ID="dgSearch2" OnRowDataBound="ItemDataBound2" runat="server" CssClass="arial_9" PageSize="25" 
                                            AllowPaging="true" OnPageIndexChanging="PageIndexChanged2" 
                                            PagerSettings-Mode="NextPrevious" PagerSettings-Position="TopAndBottom" 
                                            BorderStyle="none" CellPadding="2" CellSpacing="0">
										    <AlternatingRowStyle BackColor="LightYellow" />
										    <RowStyle CssClass="arial_10" />
										    <HeaderStyle CssClass="arial_11_bold" BackColor="White" />
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
										<!-- end of results --></td>
								</tr>
							</table>
						</td>
					</tr>
				</tbody></table>
		</form>
		<!-- end of outer border table  </TD></TR></TBODY></TABLE>	-->
		<table cellSpacing="0" cellPadding="0" width="750" border="0">
			<tr>
				<td width="30"></td>
				<td class="arial_11">&nbsp;&nbsp;<asp:label id="lblPageNum2" runat="server"></asp:label></td>
				<td class="arial_10_blue" align="right">&nbsp;&nbsp;</td>
			</tr>
		</table>
		<!-- end of CSR renewal summary table  </TD></TR></TBODY></TABLE>	-->
	</body>
</HTML>
