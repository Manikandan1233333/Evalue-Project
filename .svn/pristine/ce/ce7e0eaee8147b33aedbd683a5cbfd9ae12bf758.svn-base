<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="PaymentAccount.ascx.cs"
    Inherits="MSC.Controls.PaymentAccount" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<!-- STAR Retrofit II Changes: -->
<!-- 02/01/2007 - Changes done as part of CSR#5166-->
<!-- STAR Retrofit II.Ch1: Added csaa validators for validating the Credit Card Number for numeric characters and LUHN Modulus 10 formula on the Page-->
<!-- STAR Retrofit III Changes: -->
<!-- 04/02/2007 - Changes done as part of CSR #5595-->
<!-- STAR Retrofit III.Ch1: Added an attribute OnserverValidate to the csaa validator vldCardType to invoke the validation method ValidCardType-->
<!-- STAR Retrofit III.Ch2: Added csaa validator vldExpMonth for validating the Credit Card expiration month -->
<!-- STAR Retrofit III.Ch3: Removed hard coded items for Expiration Year drop down list
-->
<!--CVV_ERR.CH1: Commented CCV  field to hide CVV text box and label from the credit card payment screen-->
<!--.Net Mig 3.5.Ch1:Added a new validator  to validate card Type-->
<%--67811A0 - PCI Remediation for Payment systems CH1: Adjusted the width of the CardType to fit to the controls in Billing screen.  --%>
<%--67811A0 - PCI Remediation for Payment systems CH2: Adjusted the width of the Card Number to fit to the controls in Billing screen.  --%>
<%--67811A0 - PCI Remediation for Payment systems CH3: Adjusted the width so that the Text in the _ExpireYear dropdown fit to controls.  --%>
<%--67811A0 - PCI Remediation for Payment systems CH4: Modified message for Credit Card  --%>
<!-- MAIG - CH1 - Modified the width from 520 to 100%  -->
<!-- MAIG - CH2 - Added the server side method to change the max length of teh CC Number field  -->
<!-- MAIG - CH3 - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
<!-- MAIG - CH4 - Removed the Error message from the attribute  -->
<!-- MAIG - CH5 - Removed the Error message from the attribute  -->
<!-- MAIG - CH6 - Added the code to restrict the users to cut, copy and paste -->
<%--CHG0121437 - Jquery to handle the masking of Credit card number--%>
<style type="text/css">
    .style1
    {
        width: 194px;
    }
</style>
<!--MAIG - CH1 - BEGIN - Modified the width from 520 to 100%  -->
<table cellspacing="0" cellpadding="0" width="100%" bgcolor="#ffffcc" >
<!--MAIG - CH1 - END - Modified the width from 520 to 100%  -->
    <tr>
        <td class="arial_11_bold_white" bgcolor="#333333" height="22" colspan="2" >
            &nbsp;&nbsp;Credit Card Information
        </td>
    </tr>
    <%--<tr>
		<td height="10">&nbsp;&nbsp;<span class="arial_11_red">*</span>&nbsp; <span class="arial_11_bold">
				indicates required field</span></td>
	</tr>--%>
    <tr>
        <td height="10" class="style1">
        </td>
    </tr>
    <tr>
        <td class="style1">
            <%--<table cellSpacing="0" cellPadding="0" border="0">--%>
            <tr>
                <CSAA:Validator ID="vldCardType" ControlToValidate="_CardType" runat="server">&nbsp;&nbsp;&nbsp;Credit Card</CSAA:Validator>
                <td>
                <%--67811A0 - PCI Remediation for Payment systems CH1:START Adjusted the width of the CardType to fit to the controls in Billing screen.  --%>
					<!-- MAIG - CH2 - BEGIN - Added the server side method to change the max length of teh CC Number field  -->
                    <asp:DropDownList ID="_CardType" runat="server" DataValueField="Code" DataTextField="Name"
                        Width="170" >
                    </asp:DropDownList>
					<!-- MAIG - CH2 - END - Added the server side method to change the max length of teh CC Number field  -->
                <%--67811A0 - PCI Remediation for Payment systems CH1:END Adjusted the width of the CardType to fit to the controls in Billing screen.  --%>

                </td>
                <td id="AC1" runat="server" visible="false">
				<!-- MAIG - CH3 - BEGIN - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                <asp:TextBox onkeypress="return digits_only_onkeypress(event)" ID="_AuthCode" runat="server"
                    MaxLength="6" Width="124px" ></asp:TextBox>
                <!-- MAIG - CH3 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                </td>
                <!-- STAR Retrofit III.Ch1: START - Added an attribute OnserverValidate to the csaa validator vldCardType to invoke the validation method ValidCardType -->
				<!-- MAIG - CH4 - BEGIN - Removed the Error message from the attribute  -->
                <CSAA:Validator ID="vldCCType" ControlToValidate="_CardType" ErrorMessage=""
                    Display="None" OnServerValidate="ValidCardType" runat="server"></CSAA:Validator>
				<!-- MAIG - CH4 - END - Removed the Error message from the attribute  -->
                <!-- STAR Retrofit III.Ch1: END -->
                <!--Added as a part of .NetMig 3.5-->
                <CSAA:Validator ID="Validator1" runat="server" ControlToValidate="_CardType" ErrorMessage=" "
                    OnServerValidate="ReqValCheck" DefaultAction="Required"></CSAA:Validator>
                <CSAA:Validator ID="vldAuthCode" ControlToValidate="_AuthCode" runat="server" DefaultAction="Succeed"
                    Hidden="true">&nbsp;&nbsp;&nbsp;Authorization Code</CSAA:Validator><CSAA:Validator
                        ID="vldAuthCode1" ControlToValidate="_AuthCode" runat="server" DefaultAction="Numeric"
                        ErrorMessage="Verbal Authorization Code must be numeric." Display="none"></CSAA:Validator></tr>
            <%--<tr>--%>
           <%-- <td class="style1">
                &nbsp;&nbsp;
            </td>--%>
            
            <%--</tr>--%>
            <%--</table>--%>
        </td>
    </tr>
    <tr>
        <td height="10" class="style1">
        </td>
    </tr>
    <tr>
        <td class="style1">
            <CSAA:Validator ID="vldCardNumber" ErrorMessage=" " ControlToValidate="_CardNumber"
                runat="server" Tag="span">&nbsp;&nbsp;&nbsp;Card Number</CSAA:Validator><%--(16 
			digits; do not enter spaces or hyphens)--%>
        </td>
        <!--START Added by Cognizant on 12/1/2004 for validating the length of the Credit Card on the Page-->
		<!-- MAIG - CH5 - BEGIN - Removed the Error message from the attribute  -->
        <CSAA:Validator ID="vldCCExactLength" ControlToValidate="_CardNumber" ErrorMessage=""
            Display="None" OnServerValidate="ValidLength" runat="server"></CSAA:Validator>
		<!-- MAIG - CH5 - END - Removed the Error message from the attribute  -->
        <!--STAR Retrofit II.Ch1: START - Added csaa validators for validating the Credit Card Number using LUHN Modulus 10 formula on the Page-->
        <%--67811A0 - PCI Remediation for Payment systems CH4: Modified message for Credit Card  --%>
        <CSAA:Validator ID="vldCardNum" ControlToValidate="_CardNumber" ErrorMessage="Credit Card Number is Invalid."
            Display="None" OnServerValidate="ValidCCNumber" runat="server"></CSAA:Validator>
        <CSAA:Validator ID="vldCardNum1" ControlToValidate="_CardNumber" ErrorMessage="Field must be Numeric."
            Display="None" OnServerValidate="ValidNumeric" runat="server"></CSAA:Validator>
        <!--STAR Retrofit II.Ch1: END-->
        <%--</tr>
	<tr>--%>
        <td>
        
        <%--67811A0 - PCI Remediation for Payment systems CH2:START Adjusted the width of the Card Number to fit to the controls in Billing screen.  --%>
         <!--MAIG - CH6 - BEGIN - Added the code to restrict the users to cut, copy and paste -->
            
            <asp:TextBox onkeypress="return digits_only_onkeypress(event)" ID="_CardNumber" runat="server"
                MaxLength="16" oncopy="return false" onpaste="return false" oncut="return false" Width="170" 
                 ></asp:TextBox>
                <!--MAIG - CH6 - END - Added the code to restrict the users to cut, copy and paste  -->
        <%--67811A0 - PCI Remediation for Payment systems CH2:END Adjusted the width of the Card Number to fit to the controls in Billing screen.  --%>
            <%--CHG0121437 - Jquery events to handle the masking of Credit card number - Start--%>
               <asp:TextBox ID="__CardMasked" runat="server" Width="170"  value=""  />
                <!--CHG0121437 - Jquery events to handle the masking of Credit card number - End-->
        </td>
    </tr>
    <tr>
        <td height="10" class="style1">
        </td>
    </tr>
    <tr>
        <td class="style1">
            <%--<table align="left">
                <colgroup>
                    <col width="119">
                    <col width="5">
                    <col>
                </colgroup>--%>
                </td>
            <tr>                
               
                <!--CVV_ERR.CH1: Commented CCV  field to hide CVV text box and label from the credit card payment screen-->
                <%-- <csaa:validator id="vldCVV" controltovalidate="_CVV" runat="server" defaultaction="Succeed">&nbsp;&nbsp;CVV Code (3-4 digit code on back of card)</csaa:validator>--%></tr>
            
            
            <CSAA:Validator ID="Validator2" ControlToValidate="_CardType" runat="server">&nbsp;&nbsp;&nbsp;Expiration Date</CSAA:Validator>
                <%--<td class="style1" align="left">
                    &nbsp;
                    <!--START Changed by Cognizant on 10/29/2004 to include the Numeric month-->
                </td>--%>
                <!--End-->
                <!-- STAR Retrofit III.Ch3: START - Removed hard coded items for Expiration Year drop down list-->
                <td class="arial_12_bold" align="left">
                    <asp:DropDownList ID="_ExpireMonth" runat="server" Width="110px">
                        <asp:ListItem Value="" Selected="True">Select Month</asp:ListItem>
                        <asp:ListItem Value="1">01 January</asp:ListItem>
                        <asp:ListItem Value="2">02 February</asp:ListItem>
                        <asp:ListItem Value="3">03 March</asp:ListItem>
                        <asp:ListItem Value="4">04 April</asp:ListItem>
                        <asp:ListItem Value="5">05 May</asp:ListItem>
                        <asp:ListItem Value="6">06 June</asp:ListItem>
                        <asp:ListItem Value="7">07 July</asp:ListItem>
                        <asp:ListItem Value="8">08 August</asp:ListItem>
                        <asp:ListItem Value="9">09 September</asp:ListItem>
                        <asp:ListItem Value="10">10 October</asp:ListItem>
                        <asp:ListItem Value="11">11 November</asp:ListItem>
                        <asp:ListItem Value="12">12 December</asp:ListItem>
                    </asp:DropDownList>
                    &nbsp;
                    <%--67811A0 - PCI Remediation for Payment systems CH3:START Adjusted the width so that the Text in the _ExpireYear dropdown fit to controls.  --%>
                    <asp:DropDownList ID="_ExpireYear" runat="server" Width="100px">
                    </asp:DropDownList>
                    <%--67811A0 - PCI Remediation for Payment systems CH3:END Adjusted the width so that the Text in the _ExpireYeardropdown fit to controls.  --%>
                    &nbsp;
                </td>
                <!-- STAR Retrofit III.Ch3: END -->
                <td align="left">
                    <!--CVV_ERR.CH1: Commented CCV  field to hide CVV text box and label from the credit card payment screen-->
                    <%--<asp:textbox id="_CVV" runat="server" maxlength="4" onkeypress="return digits_only_onkeypress(event)"></asp:textbox>--%>
                </td>
                 <CSAA:Validator ID="vldExp" ErrorMessage=" " ControlToValidate="_ExpireMonth,_ExpireYear"
                    OnServerValidate="ValidExpDate" runat="server"></CSAA:Validator>
                <!-- STAR Retrofit III.Ch2: START - Added csaa validator vldExpMonth for validating the Credit Card expiration month-->
                <CSAA:Validator ID="vldExpMonth" ControlToValidate="_ExpireMonth,_ExpireYear" Display="None"
                    runat="server"></CSAA:Validator>
                <!-- STAR Retrofit III.Ch2: END -->
            </tr>
            <%--</table>--%>
        </td>
    </tr>
</table>
