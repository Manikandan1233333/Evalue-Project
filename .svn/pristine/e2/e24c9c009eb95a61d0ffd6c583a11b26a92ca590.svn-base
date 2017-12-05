<!-- MAIG - CH1 - Modidfied the width from 520 to 100%  -->
<!-- MAIG - CH2 - Added the alignment attribute and a spacing -->
<!-- MAIG - CH3 - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
 <!-- CHG0109406 - CH1 - Change to update ECHECK to ACH - 01202015 -->
<%--CHG0123270 - Added Text Box which will receive the Card number and display the masked Card number in UI--%>
<%@ Control Language="c#" AutoEventWireup="false" Codebehind="echeck.ascx.cs" Inherits="MSC.Controls.echeck" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<!-- MAIG - CH1 - BEGIN - Modidfied the width from 520 to 100%  -->
<table cellSpacing="0" cellPadding="0" width="100%" bgColor="#ffffcc" border="0">
<!-- MAIG - CH1 - END - Modidfied the width from 520 to 100%  -->
	<tr>
	    <!-- CHG0109406 - CH1 - BEGIN - Change to update ECHECK to ACH - 01202015-->
		<td class="arial_11_bold_white" bgColor="#333333" height="22">&nbsp;&nbsp;ACH 
			Information</td>
	    <!-- CHG0109406 - CH1 - END - Change to update ECHECK to ACH - 01202015-->
	</tr>
	<tr>
		<td height="10">
		<%--&nbsp;&nbsp;<span class="arial_11_red">*</span>&nbsp; <span class="arial_11_bold">
				indicates required field</span>--%>
		</td>
		
	</tr>
	<tr>
		<td height="10"></td>
	</tr>
	<tr>
		<td>
			<table cellSpacing="0" cellPadding="0" >
				<tr>					
				<CSAA:validator id="vldAccounttype" ErrorMessage="" runat="server" controltovalidate="_AccountType">&nbsp;&nbsp;&nbsp;Account Type</CSAA:validator>				 
				<%--</tr>
				<tr>--%>
				<td >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
				<asp:dropdownlist id="_AccountType" runat="server" Width="170"
                        datavaluefield="Account_Type_ID" datatextfield="Name"                          
                         ></asp:dropdownlist>
					</td>
					<!-- MAIG - CH2 - BEGIN - Added the alignment attribute and a spacing -->
					<td style="WIDTH: 119px" align="center">
					<asp:Hyperlink ID="Hlnkecheckhelp" runat="server"  class="headerlink:hover" Text="Help" NavigateUrl="~/Images/eCheck_v1_3.jpg" onclick="window.open(this.href,'popupwindow','width=650,height=300,scrollbars,resizable'); return false; ">
                     </asp:Hyperlink>
					</td>					
					<td  style="WIDTH: 119px">					
					<!-- MAIG - CH2 - END - Added the alignment attribute and a spacing -->
					</td>
				<CSAA:Validator ID="Validator1" runat="server" controltovalidate="_AccountType"  ErrorMessage=" " OnServerValidate="ReqValCheck1" DefaultAction="Required"></CSAA:Validator>
				</tr>	
				<tr>
		<td height="10"></td>
		</tr>
		
				
				<tr>
				<td style="WIDTH: 119px">	
				<CSAA:validator id="vldBankidnumber" runat="server" ErrorMessage=" " controltovalidate="_Bankid" tag="span">&nbsp;&nbsp;&nbsp;Routing Number</CSAA:validator>
					<csaa:validator id="vldbankidLength" controltovalidate="_Bankid" errormessage="Bank ID Number(Routing Number) should be 9 characters." Display="None" onservervalidate="ValidbankidLength" runat="server"></csaa:validator>		
					<csaa:validator id="vldBankIdzero" controltovalidate="_Bankid" errormessage="Invalid Routing number. Please use a valid routing number." Display="None" onservervalidate="Validbankidzero" runat="server"></csaa:validator>					
					<csaa:validator id="Vldbanksequence" controltovalidate="_Bankid" errormessage="Check digit for Routing Number is invalid" Display="None" onservervalidate="Validbankidsequence" runat="server"></csaa:validator>				
								
					
					</td>
					<td style="WIDTH: 150px">					
					<csaa:validator id="vldAccNumber" ErrorMessage=" " controltovalidate="_Accountno" runat="server" tag="span">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Account Number</csaa:validator>					
					<csaa:validator id="vldAccountLength" controltovalidate="_Accountno" errormessage="Bank Account Number should be between 4 to 17 characters." Display="None" onservervalidate="ValidAccountLength" runat="server"></csaa:validator>
					<csaa:validator id="vldACCNum" controltovalidate="_Accountno" errormessage="Account Number must be Numeric." display="None" onservervalidate="ValidateNumeric" runat="server"></csaa:validator>
					</td>	
					<td style="WIDTH: 100px">
					</td>				
			</tr>
			
			
			<!-- MAIG - CH3 - BEGIN - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
				<tr> 
					<td>&nbsp;
						<asp:textbox onkeypress="return digits_only_onkeypress(event)" id="_Bankid" runat="server" maxlength="9"></asp:textbox>
					</td>
					<td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;	
                        <%--CHG0123270 - Added Properties to disable Cut Copy Paste in ACH Masking--%>	
                       <%-- CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Security Testing - Added code to enable view state - VA Defect ID - 216 - Start--%>				
						<asp:textbox onkeypress="return digits_only_onkeypress(event)" id="_Accountno" runat="server" maxlength="17" Width="170"  onpaste="return false" oncut="return false" oncopy="return false" EnableViewState="true"></asp:textbox>
                       <%-- CHGXXXXXXX - CH1 - Server Upgrade 4.6.1 - Security Testing - Added code to enable view state - VA Defect ID - 216 - End--%>
                         <%--CHG0123270 - Added Text Box which will receive the Card number and display the masked Card number in UI-Start--%>
                         <asp:TextBox ID="__AccountnoMasked" runat="server" Width="170" value="" />
                        <%--CHG0123270 - Added Text Box which will receive the Card number and display the masked Card number in UI-End--%>
					</td>
				</tr>
			<!-- MAIG - CH3 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
			</table>
		</td>
	</tr>
	<tr>
	<asp:textbox id="HiddenAccountType" Visible="False" runat="server" maxlength="40"></asp:textbox>
	</tr>
</table>
