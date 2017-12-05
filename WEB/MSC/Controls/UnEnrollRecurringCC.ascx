<!-- MAIG - CH1 - Added Javascript method to avoid multiple clicks of Submit Button -->
<!-- MAIG - CH2 - Added Javascript method to avoid multiple clicks of Submit Button -->
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="UnEnrollRecurringCC.ascx.cs" Inherits="MSC.Controls.UnEnrollRecurringCC" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<%@ Register TagPrefix="uc" TagName="EMailAlert" Src="EMailAlertPopup.ascx" %>
<head>

    <script type="text/javascript">
	//MAIG - CH1 - BEGIN - Added Javascript method to avoid multiple clicks of Submit Button
        function CheckSubmit() {
            cmdSave = document.getElementById('<%= imgunenrollCC.ClientID %>');
            cmdSave.style.visibility = 'hidden';
            return true;
        }
	//MAIG - CH1 - END - Added Javascript method to avoid multiple clicks of Submit Button
    </script>

</head>
<uc:EMailAlert id="mailalert"  runat="server" />
<table cellspacing="0" cellpadding="0" width="520" bgcolor="#ffffcc" border="0">
    <tr>
        <td>
            <table width="620" bgcolor="#ffffcc">
                <tr>
                    <td class="arial_12_bold" width="190">
                   &nbsp;&nbsp;<asp:Label ID="Header" runat ="server" text="Enrollment Payment Details"></asp:Label> 
                        </td>
                    <td class="arial_12_bold">
                    <table id="tblLnkButtons" runat="server" visible="true">
                        <tr>
                          
                            <td align="left"  class="arial_12_bold" width="170">
                           <asp:LinkButton ID="lnkChangePaymentMethod" runat="server" 
                            onclick="lnkChangePaymentMethod_Click" CausesValidation="false">Change Payment Method</asp:LinkButton>
                           </td>
                         <td>|&nbsp;&nbsp;</td>
                                            <td align="left"  class="arial_12_bold" >
                                        <asp:LinkButton ID="lnkbtnEditCCDetails" Text="Edit Card Details" runat="server" 
                                        onclick="lnkbtnEditCCDetails_Click" CausesValidation="False"/>
                                    </td>
                            
                         
                        </tr>
                    
                    </table>
                        
                    </td>
                </tr>
                <tr>
                    <td height="5">
                    </td>
                </tr>
                <tr>
                    <td width="190" class="arial_12_bold" >
                        &nbsp;&nbsp;Payment Method</td>
                    <td class="arial_12">
                        <asp:Label ID="lblPaymentMethod" runat ="server"></asp:Label> 
                    </td>
                </tr>
                <tr>
                    <td height="5">
                    </td>
                </tr>
                <tr>
                    <td class="arial_12_bold" width="190">
                        &nbsp;&nbsp;Card Type</td>
                    <td class="arial_12">
                        <asp:Label ID="lblCreditCardType" runat ="server"></asp:Label> 
                    </td>
                </tr>
                <tr>
                    <td height="5">
                    </td>
                </tr>
                <tr>
                    <td class="arial_12_bold" width="190">
                        &nbsp;&nbsp;Card Number</td>
                    <td class="arial_12">
                        <asp:Label ID="lblCardNumberLast4Digit" runat ="server"></asp:Label> 
                    </td>
                </tr>
                <tr>
                    <td height="5">
                    </td>
                </tr>
                <tr>
                    <td class="arial_12_bold" width="190">
                        &nbsp;&nbsp;Name on Card</td>
                    <td class="arial_12">
                        <asp:Label ID="lblNameOnCard" runat ="server"></asp:Label> 
                    </td>
                </tr>
                <tr>
                    <td height="5">
                    </td>
                </tr>
                <tr>
                    <td class="arial_12_bold" width="190">
                        &nbsp;&nbsp;Expiration Date</td>
                    <td class="arial_12">
                        <asp:Label ID="lblExpirationDate" runat ="server"></asp:Label> 
                    </td>
                </tr>
                <tr>
                    <td height="5">
                    </td>
                </tr>
                <tr>
                    <td class="arial_12_bold" width="190">                        
                     &nbsp;&nbsp;<asp:Label ID="lblEmailID" runat ="server" Text="Email Address"></asp:Label> </td>
                    <td class="arial_12">
                        <asp:Label ID="lblEmailAddress" runat ="server"></asp:Label> 
                    </td>
                </tr>
                <tr>
                    <td height="5">
                        <asp:HiddenField ID="HiddenValue" runat="server" />
                    </td>
                </tr> 
                <tr>
                    <td></td>
                </tr>              
            </table>
		</td>
	</tr>
</table>

<table cellspacing="0" cellpadding="0" width="620" bgcolor="#ffffcc" border="0">
<tr id="don">
                <td align="right">
               <table>
                    <tr>
                      <%--  <td height="22" align="right" class="arial_12_bold">
                    <asp:LinkButton ID="lnkbtnEditCCDetails" Text="Edit Card Details" runat="server" 
                                onclick="lnkbtnEditCCDetails_Click" />
                
                </td>--%>
                 <td id="hide" colspan="2"  height="22" align="right">
			 <!-- MAIG - CH2 - START - Added Javascript method to avoid multiple clicks of Submit Button -->
             <asp:ImageButton ID="imgunenrollCC" runat="server" Width="168" alt="UnEnrollRecurring"
                            border="0" align="right" Height="20" OnClientClick="CheckSubmit();" ImageUrl="/PaymentToolimages/btn_unEnrollForRecurring.gif"
                            OnClick="imgunenrollCC_Click"></asp:ImageButton>
			 <!-- MAIG - CH2 - END - Added Javascript method to avoid multiple clicks of Submit Button -->
                    </td>
                </tr>
                <%--<tr>
                    <td>
                        <asp:Label ID="lblErrorMsg" runat="server" Text="" CssClass="arial_12_bold_red" Visible="false"></asp:Label>
                    </td>
                </tr>--%>
            </table>
		</td>
	</tr>
</table>

<table cellspacing="0" cellpadding="0" width="620" bgcolor="#ffffcc" border="0">
<tr>
 <td colspan="3">
      <asp:Label ID="lblErrorMsg" runat="server" Text="" CssClass="arial_12_bold_red" Visible="false"></asp:Label>
 </td>
</tr>
</table>
           
