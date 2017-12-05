 <!-- MAIG - CH1 - Added the below javascript code to restrict multiple clicks on a submit button -->
<!-- MAIG - CH2 - 09112014 - Introducing the Change Payment Link button -->
<!-- MAIG - CH3 - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
<!-- MAIG - CH4 - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
<!-- MAIG - CH5 - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
<!-- MAIG - CH6 - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
<!-- MAIG - CH7 - Modidfied the CSS class -->
<!-- MAIG - CH8 - Added the client side validation method CheckSubmit as part of MAIG -->
<!-- MAIG - CH9 - Added the code to include validation for Email field -->
<!-- MAIG - CH10 - Prefixed the Error message with " Re Enter" -->
<%--CHG0123270  - Added Text Box which will receive the Card number and display the masked Card number in UI-End--%>
<%--JQuery Upgrade Changes- 05/29/2017--%>
<%@ Control Language="c#" AutoEventWireup="true" CodeBehind="eCheckEnrollment.ascx.cs" Inherits="MSC.Controls.eCheckEnrollment" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<html>
<head>
    <title></title>
    <!--Include JQuery Style File-->
    <link href="/forms/jquery-ui-1.8.22.custom.css" rel="stylesheet" type="text/css" />
    <!--Include JQuery File-->
<%--CHGxxxx- JQuery Upgrade Changes- 05/29/2017- start--%>
    <script type="text/javascript" language="Javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js"></script>
<%--CHGxxxx- JQuery Upgrade Changes- 05/29/2017- end--%>

    <!--Include JQuery UI File-->

    <script type="text/javascript" language="Javascript" src="https://ajax.googleapis.com/ajax/libs/jqueryui/1.8.21/jquery-ui.min.js"></script>

    <script type="text/javascript">
        

//        function SetTarget() {
//            document.forms[0].target = "_self";

//            document.forms[0].submit();
//            document.forms[0].target = "_blank";

//        }
        // Included the below function to disable validation firing scenario when we clik the link "Terms and Conditions"
        function DisableValidator() {
            alert("m here");
            if (document.getElementById('EcheckReqValidator') != null) {
                alert("sfsd");
                document.getElementById('EcheckReqValidator').setAttribute('disabled', true);
            }
            return false;

        }
		//MAIG - CH1 - BEGIN - Added the below javascript code to restrict multiple clicks on a submit button
        function CheckSubmit() {
            cmdSave = document.getElementById('<%= ImgBtnEcheckEnrollment.ClientID %>');
            cmdSave.style.visibility = 'hidden';
            return true;
        }
		//MAIG - CH1 - END - Added the below javascript code to restrict multiple clicks on a submit button
       
    </script>

    <script type="text/javascript" language="javascript">
        function popUpWindow(URL) {
            newWin = window.open(URL, '', 'directories=0, height=600, location=0, menubar=0, resizable=0, scrollbars=0, status=0, titlebar=0, toolbar=0, width=925');
            newWin.focus();
        }   
	
     </script> 

</head>
<table cellspacing="0" cellpadding="0" width="520" bgcolor="#ffffcc" border="0">
    <tr>
        <td>
            <table width="620" bgcolor="#ffffcc" border ="0">
            
            <!--  MAIG - CH2 - BEGIN - 09112014 - Introducing the Change Payment Link button -->
			<tr>
			     <td>
                 </td>
			     <td class="arial_12_bold">
                    <table id="tblRecRedirectLnkButtons" runat="server" visible="false">
                                <tr>
                                    <td class="arial_12_bold">
                                        <asp:LinkButton ID="lnkRecRedirectChangePayment" runat="server" 
                                            CausesValidation="false" onclick="lnkRecRedirectChangePayment_Click">
                                            <b>Change Payment Method</b></asp:LinkButton>
                                    </td>
                                </tr>
                     </table>
                    </td>
			</tr>
            <!--  MAIG - CH2 - END - 09112014 - Introducing the Change Payment Link button -->
			
                <tr>
                    <td class="arial_12_bold" width="85">
                        &nbsp;&nbsp;Account Type
                    </td>
                    <td class="arial_12_bold" width="180">
                        <asp:RadioButton ID="rbCheckingAccount" Checked="true" runat="server" GroupName="Amount" >
                        </asp:RadioButton>
                        Checking<font color="#ff0000"></font>
                        <asp:RadioButton ID="rbSavingsAccount" runat="server" GroupName="Amount" >
                        </asp:RadioButton>
                        Savings<font color="#ff0000"></font>
                    </td>
                     <%--<CSAA:Validator ID="Validator1234" runat="server" controltovalidate="lstCheckingAccount"  ErrorMessage=" " OnServerValidate="ReqValCheck1" DefaultAction="Required"></CSAA:Validator>	--%>
                </tr>
                <tr> 
                    <td class="arial_12_bold" width="150">
                    <csaa:validator id="vldBankidnumber" runat="server" ErrorMessage=" " controltovalidate="_Bankid" tag="span">&nbsp;&nbsp;Routing Number</CSAA:validator>
					<csaa:validator id="vldbankidLength" controltovalidate="_Bankid" errormessage="Bank ID Number(Routing Number) should be 9 characters." Display="None" onservervalidate="ValidbankidLength" runat="server"></csaa:validator>		
					<csaa:validator id="vldBankIdzero" controltovalidate="_Bankid" errormessage="Invalid Routing number. Please use a valid routing number." Display="None" onservervalidate="ValidateBankIdZero" runat="server"></csaa:validator>					
					<csaa:validator id="Vldbanksequence" controltovalidate="_Bankid" errormessage="Check digit for Routing Number is invalid" Display="None" onservervalidate="ValidateBankIdSequence" runat="server"></csaa:validator>				
                    </td>
                    <td class="arial_12">
					<!-- MAIG - CH3 - START - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                        <asp:TextBox ID="_Bankid" onkeypress="return digits_only_onkeypress(event)" runat="server" OnTextChanged="Bankid_TextChanged" Width="267px" MaxLength="9" EnableViewState="true" AutoPostBack="true"
                            class="border_Non_ReadOnly"></asp:TextBox>
					<!-- MAIG - CH3 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                    </td>
                    <CSAA:Validator ID="EcheckReqValidator" runat="server" controltovalidate="_Bankid"  ErrorMessage=" " OnServerValidate="ReqValCheck1" DefaultAction="Required"></CSAA:Validator>	
                    </tr>
                <tr>
                    <td class="arial_12_bold" width="90">
                        &nbsp;&nbsp;Bank Name
                    </td>
                    <td class="arial_12">
                        <asp:Label ID="displayBankName" runat="server"></asp:Label>
                    </td>
                    </tr>
                <tr>
                    <td class="arial_12_bold">
                    <csaa:validator id="vldAccNumber" ErrorMessage=" " controltovalidate="_Accountno" runat="server" tag="span">&nbsp;&nbsp;Account Number</csaa:validator>					
					<csaa:validator id="vldAccountLength" controltovalidate="_Accountno" errormessage="Bank Account Number should be between 4 to 17 characters." Display="None" onservervalidate="ValidAccountLength" runat="server"></csaa:validator>
					<csaa:validator id="vldACCNum" controltovalidate="_Accountno" errormessage="Account Number must be Numeric." display="None" onservervalidate="ValidateNumeric" runat="server"></csaa:validator>
                    </td>
                    <td>
						<!-- MAIG - CH4 - START - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                        <%--CHG0123270  - Added Properties to disable Cut Copy Paste in ACH Masking--%>
                        <asp:TextBox ID="_Accountno" onkeypress="return digits_only_onkeypress(event)" runat="server" Width="267px" MaxLength="17" EnableViewState="true"
                            class="border_Non_ReadOnly" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>
                         <%--CHG0123270  - Added Text Box which will receive the Card number and display the masked Card number in UI-Start--%>
                         <asp:TextBox ID="__AccountnoMasked" runat="server" Width="267px" value="" />
                        <%--CHG0123270  - Added Text Box which will receive the Card number and display the masked Card number in UI-End--%>
						<!-- MAIG - CH4 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                    </td>
				</tr>
                
                <tr>
                    
                    <td class="arial_12_bold" width="190px">
                    <csaa:validator id="vldReAccNumber" ErrorMessage=" " controltovalidate="_ReAccountno" runat="server" tag="span">&nbsp;&nbsp;Re Enter Account Number</csaa:validator>					
                    <csaa:validator id="vldReAccountLength" controltovalidate="_ReAccountno" errormessage="Re Enter Account Number should be between 4 to 17 characters." Display="None" onservervalidate="ValidAccountLength" runat="server"></csaa:validator>
                    <!-- MAIG - CH10 - BEGIN - Prefixed the Error message with " Re Enter" -->
					<csaa:validator id="vldReACCNum" controltovalidate="_ReAccountno" errormessage="Re Enter Account Number must be Numeric." display="None" onservervalidate="ValidateNumeric" runat="server"></csaa:validator>
					<!-- MAIG - CH10 - END - Prefixed the Error message with " Re Enter" -->
					<csaa:validator id="vldReAccNumVerify" controltovalidate="_ReAccountno" errormessage="" display="None" onservervalidate="VerifyAccountNumber" runat="server"></csaa:validator>
                    </td>
                    <td class="arial_12">
						<!-- MAIG - CH5 - START - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                         <%--CHG0123270 - Added Properties to disable Cut Copy Paste in ACH Masking--%>
                        <asp:TextBox ID="_ReAccountNo" onkeypress="return digits_only_onkeypress(event)" runat="server" Width="267px" MaxLength="17" EnableViewState="true"
						 class="border_Non_ReadOnly" onpaste="return false" oncut="return false" oncopy="return false"></asp:TextBox>
                       <%--CHG0123270 - Added Text Box which will receive the Card number and display the masked Card number in UI-Start--%>
                        <asp:TextBox ID="__ReAccountNoCardMasked" runat="server" Width="267px" value="" />
                        <%--CHG0123270 - Added Text Box which will receive the Card number and display the masked Card number in UI-End--%>
						 <!-- MAIG - CH5 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->                            
                    </td>
                </tr>
                <tr>
                    <td class="arial_12_bold" >
                    <csaa:validator id="vldAccHolderName" ErrorMessage=" " controltovalidate="_txtCustomerName" runat="server" tag="span">&nbsp;&nbsp;Account Holder Name</csaa:validator>
                    <CSAA:Validator ID="vldAccName" runat ="server" ControlToValidate="_txtCustomerName" ErrorMessage=""
                    Display="None" OnServerValidate="ValidName"></CSAA:Validator>					
            <%--            &nbsp;&nbsp;Account Holder Name<font color="#ff0000">&nbsp;*</font>--%>
                    </td>
                    <td class="arial_12">
						<!-- MAIG - CH6 - BEGIN - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->                            
                        <asp:TextBox ID="_txtCustomerName" runat="server" Width="267px" onkeypress="return alphabetsonly_onkeypress(event);" MaxLength="25" EnableViewState="true"
                            class="border_Non_ReadOnly"></asp:TextBox>
						<!-- MAIG - CH6 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->                            
                    </td>
                </tr>
                <%--                <tr>
                    <td class="arial_12_bold" width="191">
                    <csaa:validator id="vldEmailAddress" ErrorMessage=" " controltovalidate="_txtEmailAddress" runat="server" tag="span">&nbsp;&nbsp;Email Address</csaa:validator>					
                      
                    </td>
                    <td class="arial_12">
                            <asp:TextBox ID="_txtEmailAddress" runat="server" Width="267px" MaxLength="25" EnableViewState="true"
                            class="border_Non_ReadOnly"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <td class="arial_12_bold">
                        &nbsp;&nbsp;Email Address</td>
                        
                    <td class="style4">
					<!-- MAIG - CH9 - BEGIN - Added the code to include validation for Email field -->
                        <asp:TextBox ID="_txtEmailAddress" runat="server" Width="267px" MaxLength="50" 
                            enableviewstate="true" onkeypress="return email_Address_onkeypress(event)" class="border_Non_ReadOnly" ></asp:TextBox>
					<!-- MAIG - CH9 - END - Added the code to include validation for Email field -->
                    </td>
                    <csaa:validator id="vldEmailAddress" controltovalidate="_txtEmailAddress" errormessage="Invalid email address." display="None" onservervalidate="ValidateEmailAddress" runat="server"></csaa:validator>
                </tr>
                <tr>
                    <td>
                    </td>
					<!-- MAIG - CH7 - BEGIN - Modidfied the CSS class -->
                    <td class="arial_12_bold" valign="top">
					<!-- MAIG - CH7 - END - Modidfied the CSS class -->
                            <%--<asp:LinkButton ID="Terms_Condition" runat="server" OnClick="EcTermsAndConditions_btn_OnClick" OnClientClick="javascript:DisableValidator(); return false;"  CausesValidation="false" AutoPostBack="false"><b>Terms and Conditions</b></asp:LinkButton>--%>
                   <%-- <asp:LinkButton ID="Terms_Condition" runat="server"  
                                OnClick ="EcTermsAndConditions_btn_OnClick" OnClientClick="javascript:DisableValidator(); return false;"  CausesValidation="false" 
                                AutoPostBack="false"><b>Terms and Conditions</b></asp:LinkButton>
                              --%>
                            <asp:LinkButton ID="Terms_Condition" runat="server" OnClick="EcTermsAndConditions_btn_OnClick" OnClientClick="" AutoPostBack="false" CausesValidation="false"><b>Terms and Conditions</b></asp:LinkButton>
                    </td>
                </tr>
                <table>
                <asp:PlaceHolder ID ="ContactCenterUser" runat = "server" Visible = "false"> 
                <tr>
                    <td colspan = "2" class="arial_11_bold">
                        <asp:Label ID="lblContactCenter" Text="" runat="server" style="text-align:justify"></asp:Label>
                        </td>
                        </tr>
                </asp:PlaceHolder>
                        </table>
                </tr>
                <asp:PlaceHolder ID ="F2Fuser" runat = "server" Visible = "true">
                <tr>
                    <td colspan = "2" class="arial_11_bold">
                        <asp:CheckBox ID="chkF2Fuser" Text="" runat="server" AutoPostBack="False" />
                        <CSAA:Validator ID="vldF2F" ControlToValidate="chkF2Fuser" ErrorMessage=""
                        Display="None" runat="server"></CSAA:Validator>
                        <asp:Label ID="lbl_Terms" runat="server"><font color="black"><b></font></asp:Label>
                    </td>
                </tr>
                </asp:PlaceHolder>
                <tr>
                    <td bgcolor="#cccccc" colspan="2" height="22">
						<!-- MAIG - CH8 - BEGIN - Added the client side validation method CheckSubmit as part of MAIG -->
                        <asp:ImageButton ID="ImgBtnEcheckEnrollment" runat="server" Width="68" 
                            alt="search" border="0"
                            align="right" Height="17" ImageUrl="/PaymentToolimages/btn_submit.gif" OnClientClick="CheckSubmit();" 
                            onclick="ImgBtnEcheckEnrollment_Click"></asp:ImageButton>
						<!-- MAIG - CH8 - END - Added the client side validation method CheckSubmit as part of MAIG -->
                        <asp:imagebutton id="ImgBtnCancel" runat="server" width="51" alt="cancel" 
                             border="0" align="right" height="17" 
                             imageurl="../images/btn_cancel.gif" OnClick="ImgBtnCancel_Click" CausesValidation="false"></asp:imagebutton>
                    </td>
                </tr>
            <%--</table>--%>
          
	<tr>
	<td>	
	<asp:Label ID="lblErrMessage" runat="server" visible="false" CssClass="arial_12_bold" ForeColor="Red"></asp:Label>
        </td></tr>
        
</table>
</html>
