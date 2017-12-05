<!--MAIG - CH1 - Added the below javascript methods to support validation -->
<!--MAIG - CH2 - 09112014 - Introducing the Change Payment Link button -->
<!--MAIG - CH3 - Removed the Error message-->
<!--MAIG - CH4 - Added a Server side method for the onselectedindexchanged event -->
<!--MAIG - CH5 - Removed the Error message-->
<!--MAIG - CH6 - Added the code to restrict the users to cut, copy and paste -->
<!--MAIG - CH7 - Added the code to perform a client side validation-->
<!--MAIG - CH8 - Added the ccolspan and align attributes-->
<!--MAIG - CH9 - Added an javascript method to block multiple clicks on the Enrollment button -->
<!--MAIG - CH10 - Modidfied the javascript code to support Firefox browser  -->
<!--MAIG - CH11 - Added the javascript method to support Email Field validation -->
<%--CHG0121437 - Jquery events to handle the masking of Credit card number--%>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="CreditCardEnrollment.ascx.cs" Inherits="MSC.Controls.CreditCardEnrollment" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<html>
<head>
    <title></title>
    <script type="text/javascript" language="javascript">
        //        function load() {
        //            document.forms[0].target = "_self";
        //        }

        function popUpWindow(URL) {
            newWin = window.open(URL, '', 'directories=0, height=600, location=0, menubar=0, resizable=0, scrollbars=0, status=0, titlebar=0, toolbar=0, width=925');
            newWin.focus();
        }


        // Commented for the issue # 927      
        //   function SetTarget() {
        //      
        //       document.forms[0].target = "_self";
        //    
        //       document.forms[0].submit();
        //       document.forms[0].target = "_blank";
        //       
        //          
        //    }
        // Included the below function to disable validation firing scenario when we clik the link "Terms and Conditions"
        function OpenWindow() {
            if (document.getElementById('CCReqFieldValidator') != null) {
                document.getElementById('CCReqFieldValidator').setAttribute('disabled', true);
            }
            return false;
        }
        //MAIG - CH1 - BEGIN - Added the below javascript methods to support validation

        function CheckSubmit() {
            cmdSave = document.getElementById('<%= ImgBtnCCEnrollment.ClientID %>');
            cmdSave.style.visibility = 'hidden';
            return true;
        }


        //MAIG - CH1 - END - Added the below javascript methods to support validation 

        function checkUserAcknowledgment() {
            var chkbox = document.getElementById('chkDuplicate').checked;
            if (!chkbox)
                document.getElementById('btncontrol_ContinueButton').setAttribute('disabled', true);
            else
                document.getElementById('btncontrol_ContinueButton').setAttribute('disabled', false);
        }

    </script>


    <style type="text/css">
        .style2 {
            width: 620px;
        }

        .style4 {
            width: 323px;
        }

        .style5 {
            width: 16px;
        }

        .style7 {
            width: 190px;
            font-size: 12px;
            font-family: arial, helvetica, sans-serif;
            font-weight: bold;
        }
    </style>
</head>
</html>
<table cellspacing="0" cellpadding="0" width="620" bgcolor="#ffffcc" border="0">
    <tr>
        <td>
            <table width="640" bgcolor="#ffffcc" class="style2">

                <!--  MAIG - CH2 - START - 09112014 - Introducing the Change Payment Link button -->
                <tr>
                    <td></td>
                    <td class="arial_12_bold">
                        <table id="tblRecRedirectLnkButtons" runat="server" visible="false">
                            <tr>
                                <td class="arial_12_bold">
                                    <asp:LinkButton ID="lnkRecRedirectChangePayment" runat="server"
                                        CausesValidation="false" OnClick="lnkRecRedirectChangePayment_Click">
                                                <b>Change Payment Method</b></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <!--  MAIG - CH2 - END - 09112014 - Introducing the Change Payment Link button -->

                <tr>
                    <%--    <td class="arial_12_bold" width="220px">
                        &nbsp;&nbsp;Credit Card<font color="#ff0000">&nbsp;*</font></td>--%>
                    <td class="style7">
                        <CSAA:Validator ID="vldCardType" ControlToValidate="_CardType" runat="server" Tag="span">&nbsp;&nbsp;Credit Card</CSAA:Validator>
                        <!--  MAIG - CH3 - START - Removed the Error message-->
                        <CSAA:Validator ID="vldCCType" ControlToValidate="_CardType" ErrorMessage=""
                            Display="None" OnServerValidate="ValidCardType" runat="server"></CSAA:Validator>
                        <!--  MAIG - CH3 - END - Removed the Error message-->
                    </td>
                    <td class="style4">
                        <!--  MAIG - CH4 - START - Added a Server side method for the onselectedindexchanged event -->
                        <asp:DropDownList ID="_CardType" runat="server" DataValueField="Code" DataTextField="Name"
                            Width="170">
                        </asp:DropDownList>
                        <!--  MAIG - CH4 - END - Added a Server side method for the onselectedindexchanged event -->
                    </td>
                    <!-- STAR Retrofit III.Ch1: END -->
                    <!--Added as a part of .NetMig 3.5-->
                    <CSAA:Validator ID="CCReqFieldValidator" runat="server" ControlToValidate="_CardType" ErrorMessage=" "
                        OnServerValidate="ReqValCheck" DefaultAction="Required"></CSAA:Validator>
                </tr>
                <tr>
                    <%--                    <td class="arial_12_bold">
                        &nbsp;&nbsp;Card Number<font color="#ff0000">&nbsp;*</font>
                       <CSAA:Validator ID="Validator2" ErrorMessage=" " ControlToValidate="_CardNumber"
                runat="server" Tag="span">&nbsp;&nbsp;Card Number</CSAA:Validator>
                    </td>--%>
                    <td class="style7">

                        <CSAA:Validator ID="vldCardNumber" ErrorMessage=" " ControlToValidate="_CardNumber"
                            runat="server" Tag="span">&nbsp;&nbsp;Card Number</CSAA:Validator>
                    </td>
                    <!--MAIG - CH5 - START - Removed the Error message-->
                    <CSAA:Validator ID="vldCCExactLength" ControlToValidate="_CardNumber" ErrorMessage=""
                        Display="None" OnServerValidate="ValidLength" runat="server"></CSAA:Validator>
                    <!--MAIG - CH5 - END - Removed the Error message-->
                    <CSAA:Validator ID="vldCardNum" ControlToValidate="_CardNumber" ErrorMessage="Credit Card Number is Invalid."
                        Display="None" OnServerValidate="ValidCCNumber" runat="server"></CSAA:Validator>
                    <%-- <CSAA:Validator ID="vldCardNum1" ControlToValidate="_CardNumber" ErrorMessage="Credit Card Number must be Numeric."
                    Display="None" OnServerValidate="ValidNumeric" runat="server"></CSAA:Validator>      --%>
                    <td class="style4">
                        <!--MAIG - CH6 - BEGIN - Added the code to restrict the users to cut, copy and paste -->
                        
                        <asp:TextBox onkeypress="return digits_only_onkeypress(event)" ID="_CardNumber" runat="server" Width="170" oncopy="return false" MaxLength="16" onpaste="return false" oncut="return false" EnableViewState="true" class="border_Non_ReadOnly"></asp:TextBox>
                        <!--MAIG - CH6 - END - Added the code to restrict the users to cut, copy and paste  -->
                        <!--CHG0121437 - Jquery events to handle the masking of Credit card number - Start-->
                        <asp:TextBox ID="__CardMasked" runat="server" Width="170" value="" />
                        <!--CHG0121437 - Jquery events to handle the masking of Credit card number - End-->
                    </td>
                </tr>
                <tr>
                    <%--                    <td class="arial_12_bold">
                        &nbsp;&nbsp;Expiration Date<font color="#ff0000">&nbsp;*</font>
                    </td>--%>
                    <td class="style7">
                        <CSAA:Validator ID="vldExp1" ErrorMessage=" " ControlToValidate="_ExpireMonth,_ExpireYear"
                            runat="server" Tag="span">&nbsp;&nbsp;Expiration Date</CSAA:Validator>
                    </td>
                    <td class="style4" align="left">
                        <asp:DropDownList ID="_ExpireMonth" runat="server" Width="110px">
                            <asp:ListItem Value="" Selected="True">Select Month</asp:ListItem>
                            <asp:ListItem Value="01">01 January</asp:ListItem>
                            <asp:ListItem Value="02">02 February</asp:ListItem>
                            <asp:ListItem Value="03">03 March</asp:ListItem>
                            <asp:ListItem Value="04">04 April</asp:ListItem>
                            <asp:ListItem Value="05">05 May</asp:ListItem>
                            <asp:ListItem Value="06">06 June</asp:ListItem>
                            <asp:ListItem Value="07">07 July</asp:ListItem>
                            <asp:ListItem Value="08">08 August</asp:ListItem>
                            <asp:ListItem Value="09">09 September</asp:ListItem>
                            <asp:ListItem Value="10">10 October</asp:ListItem>
                            <asp:ListItem Value="11">11 November</asp:ListItem>
                            <asp:ListItem Value="12">12 December</asp:ListItem>
                        </asp:DropDownList>
                        &nbsp;
                    <asp:DropDownList ID="_ExpireYear" runat="server" Width="100px">
                    </asp:DropDownList>
                        &nbsp;
                    </td>
                    <td class="style5">
                        <CSAA:Validator ID="vldExp" ErrorMessage=" " ControlToValidate="_ExpireMonth,_ExpireYear"
                            OnServerValidate="ValidExpDate" runat="server"></CSAA:Validator>
                        <!-- STAR Retrofit III.Ch2: START - Added csaa validator vldExpMonth for validating the Credit Card expiration month-->
                        <CSAA:Validator ID="vldExpMonth" ControlToValidate="_ExpireMonth,_ExpireYear" Display="None"
                            runat="server"></CSAA:Validator>
                    </td>
                </tr>
                <tr>

                    <td class="style7">
                        <CSAA:Validator ID="vldNameCheck" runat="server" ControlToValidate="_Name" Tag="span">&nbsp;&nbsp;Name on Card</CSAA:Validator>
                        <CSAA:Validator ID="vldName" runat="server" ControlToValidate="_Name" ErrorMessage=""
                            Display="None" OnServerValidate="ValidName"></CSAA:Validator>
                    </td>
                    <td class="arial_12" colspan="2">
                        <!--MAIG - CH7 - BEGIN - Added the code to perform a client side validation-->
                        <asp:TextBox ID="_Name" runat="server" Width="267px" MaxLength="40" onkeypress="return alphabetsonly_onkeypress(event)"
                            EnableViewState="true" class="border_Non_ReadOnly"></asp:TextBox>
                        <!--MAIG - CH7 - END - Added the code to perform a client side validation-->
                    </td>
                </tr>
                <tr>
                    <td class="style7">
                        <CSAA:Validator ID="vldZip" ErrorMessage=" " ControlToValidate="_txtZipCode"
                            runat="server" Tag="span">&nbsp;&nbsp;Billing ZipCode</CSAA:Validator>
                    </td>
                    <CSAA:Validator ID="vldzipLength" runat="server" ControlToValidate="_txtZipCode" ErrorMessage=""
                        Display="None" OnServerValidate="ValidZipLength"></CSAA:Validator>
                    <td class="style4">
                        <!--MAIG - CH10 - BEGIN - Modidfied the javascript code to support Firefox browser  -->
                        <asp:TextBox onkeypress="return digits_only_onkeypress(event)" ID="_txtZipCode" runat="server" Width="70px" MaxLength="5"
                            EnableViewState="true" class="border_Non_ReadOnly"></asp:TextBox>
                        <!--MAIG - CH10 - END - Modidfied the javascript code to support Firefox browser  -->
                    </td>
                </tr>
                <tr>
                    <td class="style7">&nbsp;&nbsp;Email Address</td>
                    <td class="style4">
                        <!--MAIG - CH11 - BEGIN - Added the javascript method to support Email Field validation -->
                        <asp:TextBox ID="_txtEmailAddress" onkeypress="return email_Address_onkeypress(event)" runat="server" Width="267px" MaxLength="50"
                            EnableViewState="true" class="border_Non_ReadOnly"></asp:TextBox>
                        <!--MAIG - CH11 - END - Added the javascript method to support Email Field validation -->
                    </td>
                    <CSAA:Validator ID="vldEmailAddress" ControlToValidate="_txtEmailAddress" ErrorMessage="Invalid email address." Display="None" OnServerValidate="ValidateEmailAddress" runat="server"></CSAA:Validator>
                </tr>
                <tr>
                    <!--MAIG - CH8 - BEGIN - Added the ccolspan and align attributes-->
                    <td class="arial_12_bold" colspan="2" align="center">
                        <!--MAIG - CH8 - END - Added the ccolspan and align attributes-->
                        <asp:LinkButton ID="Terms_Condition" runat="server" OnClientClick="" OnClick="CcTermsAndConditions_btn_OnClick" CausesValidation="false" AutoPostBack="false"><b>Terms and Conditions</b></asp:LinkButton>
                    </td>
                </tr>
                <table>
                    <td colspan="2" class="arial_11_bold">
                        <asp:PlaceHolder ID="ContactCenterUser" runat="server" Visible="false">
                            <asp:Label ID="lblContactCenter" Text="" runat="server" Style="text-align: right">
                            </asp:Label>
                        </asp:PlaceHolder>
                    </td>
                </table>
    </tr>


    <asp:PlaceHolder ID="F2Fuser" runat="server" Visible="true">
        <tr>
            <td colspan="2" class="arial_11_bold">
                <asp:CheckBox ID="chkF2Fuser" Text="" runat="server" AutoPostBack="False" />
                <CSAA:Validator ID="vldF2F" ControlToValidate="chkF2Fuser" ErrorMessage=""
                    Display="None" runat="server"></CSAA:Validator>
                <asp:Label ID="lbl_Terms" runat="server"><font color="black"><b></b></font></asp:Label>
            </td>
        </tr>
    </asp:PlaceHolder>
    <tr>
        <td bgcolor="#cccccc" colspan="2" height="22">
            <!--MAIG - CH9 - BEGIN - Added an javascript method to block multiple clicks on the Enrollment button -->
            <asp:ImageButton ID="ImgBtnCCEnrollment" runat="server" Width="68" alt="search"
                border="0" align="right" Height="17"
                ImageUrl="/PaymentToolimages/btn_submit.gif" OnClientClick="CheckSubmit();" OnClick="ImgBtnCCEnrollment_Click"></asp:ImageButton>
            <!--MAIG - CH9 - END - Added an javascript method to block multiple clicks on the Enrollment button -->
            <asp:ImageButton ID="ImgBtnCancel" runat="server" Width="51" alt="cancel"
                border="0" align="right" Height="17"
                ImageUrl="../images/btn_cancel.gif" OnClick="ImgBtnCancel_Click" CausesValidation="false"></asp:ImageButton>

        </td>
    </tr>
</table>

</td>
	</tr>
	<tr>
        <td>
            <asp:Label ID="lblErrMessage" runat="server" Visible="false" CssClass="arial_12_bold" ForeColor="Red"></asp:Label>
        </td>
    </tr>
</table>


