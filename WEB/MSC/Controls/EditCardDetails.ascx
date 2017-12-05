<!-- MAIG - CH1 - Added the below javascript code to restrict multiple clicks on a submit button -->
<!-- MAIG - CH2 - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
<!-- MAIG - CH3 - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
<!-- MAIG - CH4 - Added an javascript method to block multiple clicks on the Enrollment button -->
<!-- MAIG - CH5 - Added an Hidden field to check the visibility of the Update button-->
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="EditCardDetails.ascx.cs" Inherits="MSC.Controls.EditCardDetails" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<%@ Register TagPrefix="uc" TagName="pagevalidator" Src="~/Controls/PageValidator.ascx"%>
<head>

   <style type="text/css">
        .style2
        {
            width: 620px;
        }
        .style4
        {
            width: 323px;
        }
        .style5
        {
            width: 16px;
        }
        .CCStyle
        {
            width: 190px;
            font-size: 12px;
            font-family: arial, helvetica, sans-serif;            
	        font-weight: bold;
        }
    </style>
	<!-- MAIG - CH1 - BEGIN - Added the below javascript code to restrict multiple clicks on a submit button -->
    <script type="text/javascript">
        function CheckSubmit() {
            cmdSave = document.getElementById('<%= ImgBtnUpdate.ClientID %>');
            cmdSave.style.visibility = 'hidden';
            return true;
        }


        
    
    </script>
	<!-- MAIG - CH1 - END - Added the below javascript code to restrict multiple clicks on a submit button -->
</head>

<table cellspacing="0" cellpadding="0" width="620" bgcolor="#ffffcc" border="0">
	<tr>
		<td>
			<table width="640" bgcolor="#ffffcc" class="style2">
                <tr>
                    <td width="190" class="CCStyle" height="22" >
                        &nbsp;&nbsp;Payment Method</td>
                    <td class="arial_12">
                        <asp:Label ID="lblPaymentMethod" runat ="server"></asp:Label> 
                    </td>
                      
                </tr>
                <tr>
                    <td class="CCStyle" width="190" height="22">
                        &nbsp;&nbsp;Card Number</td>
                    <td class="arial_12">
                        <asp:Label ID="lblCardNumberLast4Digit" runat ="server" ></asp:Label> 
                    </td>
                </tr>
                <tr>

                    <td class="CCStyle">
                        <CSAA:Validator ID="vldExpDate" ErrorMessage=" " ControlToValidate="_ExpireMonth,_ExpireYear"
                             runat="server" Tag="span" DefaultAction="Required">&nbsp;&nbsp;Expiration Date</CSAA:Validator>
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
                        <CSAA:Validator ID="vldExp" ErrorMessage="" ControlToValidate="_ExpireMonth,_ExpireYear"
                            OnServerValidate="ValidExpDate" runat="server"></CSAA:Validator>
                <!-- STAR Retrofit III.Ch2: START - Added csaa validator vldExpMonth for validating the Credit Card expiration month-->
                        <CSAA:Validator ID="vldExpMonth" ControlToValidate="_ExpireMonth,_ExpireYear" Display="None"
                            runat="server"></CSAA:Validator>
                    </td>
                     
                       
                </tr>
                <tr>

                     <td class="CCStyle">
                        <CSAA:validator id="vldNameCheck" runat="server" ControlToValidate="_Name" Tag="span">&nbsp;&nbsp;Name on Card</CSAA:validator>                        
                        <CSAA:Validator ID="vldName" runat ="server" ControlToValidate="_Name" ErrorMessage=""
                    Display="None" OnServerValidate="ValidName"></CSAA:Validator>
                    </td>
                    <td class="arial_12" colspan ="2">
					<!-- MAIG - CH2 - BEGIN - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                        <asp:TextBox ID="_Name" runat="server" Width="267px" MaxLength="40" onkeypress="return alphabetsonly_onkeypress(event);" 
                            enableviewstate="true"  class="border_Non_ReadOnly" ></asp:TextBox>
					<!-- MAIG - CH2 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                    </td> 
                    
                        <CSAA:Validator ID="UpdateCCReqFieldValidator" runat="server" ControlToValidate="_Name" ErrorMessage=" "
                            OnServerValidate="ReqValCheck" DefaultAction="Required"></CSAA:Validator>         
                </tr>
                <tr>
                    <td class="CCStyle">
                   <CSAA:Validator ID="vldZip" ErrorMessage=" " ControlToValidate="_txtZipCode"
                    runat="server" Tag="span">&nbsp;&nbsp;Billing ZipCode</CSAA:Validator>
                    </td>
                     <CSAA:Validator ID="vldzipLength" runat ="server" ControlToValidate="_txtZipCode" ErrorMessage="Billing ZipCode Length must be exactly 5 characters."
                    Display="None" OnServerValidate="ValidZipLength"></CSAA:Validator>
                        <td class="style4">
						<!-- MAIG - CH3 - BEGIN - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                        <asp:TextBox onkeypress="return digits_only_onkeypress(event)" ID="_txtZipCode" runat="server" Width="70px" MaxLength="5" 
                            enableviewstate="true"  class="border_Non_ReadOnly" ></asp:TextBox>
						<!-- MAIG - CH3 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                    </td>
                </tr>
                <tr>
                    <td class="CCStyle">
                        &nbsp;&nbsp;Email Address</td>
                    <td class="style4">
                        <asp:TextBox ID="_txtEmailAddress" runat="server" Width="267px" MaxLength="50" 
                            enableviewstate="true"  class="border_Non_ReadOnly" ></asp:TextBox>
                    </td>
                    <csaa:validator id="vldEmailAddress" controltovalidate="_txtEmailAddress" errormessage="Invalid email address." display="None" onservervalidate="ValidateEmailAddress" runat="server"></csaa:validator>
                </tr>
                <tr>
                    <td class="CCStyle">
                    </td>
                    <td class="arial_12_bold">
                  <asp:LinkButton ID="Terms_Condition" runat="server" OnClientClick="" OnClick ="CcTermsAndConditions_btn_OnClick" CausesValidation="false" AutoPostBack="false" ><b>Terms and Conditions</b></asp:LinkButton>
                    </td>
                </tr>
                
                <table>
                <tr>
                        <td colspan ="2"  class="arial_11_bold">
                            <asp:PlaceHolder ID ="ContactCenterUser" runat = "server" Visible = "false"> 
                            <asp:Label ID="lblContactCenter" Text="" runat="server" style="text-align:right">
                            </asp:Label>
                             </asp:PlaceHolder>
                            </td>
                            </tr>
                  </table>
                 
               
                <asp:PlaceHolder ID ="F2Fuser" runat = "server" Visible = "true">
                <tr>
                    <td colspan = "2" class="arial_11_bold">
                        <asp:CheckBox ID="chkF2Fuser" Text="" runat="server" AutoPostBack="False" />
                        <CSAA:Validator ID="vldF2F" ControlToValidate="chkF2Fuser" ErrorMessage=""
                        Display="None" runat="server"></CSAA:Validator>
                        <asp:Label ID="lbl_Terms" runat="server"><font color="black"><b></b></font></asp:Label>
                    </td>
                </tr>
                </asp:PlaceHolder>
                <tr>
				    <td bgcolor="#cccccc" colspan="2">
						<!--MAIG - CH4 - BEGIN - Added an javascript method to block multiple clicks on the Enrollment button -->
					     <asp:imagebutton id="ImgBtnUpdate" runat="server" alt="search" 
                             border="0" align="right" imageurl="/PaymentToolimages/btn_update.gif" 
                             onclick="ImgBtnUpdate_Click" OnClientClick="CheckSubmit();"></asp:imagebutton>
						<!--MAIG - CH4 - END - Added an javascript method to block multiple clicks on the Enrollment button -->
					    <%--<a href="#"><img height="17" alt="Clear" src="../images/btn_cancel.gif" align="right" width="51" border="0" /></a>--%>
					    <asp:ImageButton ID="ImgBtnCancel" runat="server" alt="cancel" border="0" 
                             align="right" imageurl="../images/btn_cancel.gif" onclick="ImgBtnCancel_Click"/>
				    </td>
				</tr>
            </table>
           
		</td>
	</tr>
	<tr>
	    <td>	
	    <asp:Label ID="lblErrMessage" runat="server" visible="false" CssClass="arial_12_bold" ForeColor="Red"></asp:Label>
                 <asp:HiddenField ID="hdnPaymentToken" Value="" runat="server" />
                 <asp:HiddenField ID="hdnCardType" Value="" runat="server" />
				<!--MAIG - CH5 - BEGIN - Added an Hidden field to check the visibility of the Update button-->
                 <asp:HiddenField ID="updatebtndisable" Value="" runat="server" />
 				<!--MAIG - CH5 - END - Added an Hidden field to check the visibility of the Update button-->
                  <uc:PageValidator runat="server" colspan="3" ID="vldSumaary" />
            </td>
     </tr>
</table>


           
