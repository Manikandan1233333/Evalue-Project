<!-- MAIG - CH1 - Added Javascript method to avoid multiple clicks of Submit Button -->
<!-- MAIG - CH2 - Added Javascript method to avoid multiple clicks of Submit Button -->
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="UnEnrollRecurringECheck.ascx.cs" Inherits="MSC.Controls.UnEnrollRecurringECheck" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<%@ Register TagPrefix="uc" TagName="EMailAlert" Src="EMailAlertPopup.ascx" %>
<html><head>
   <script type ="text/javascript">
		//MAIG - CH1 - START - Added Javascript method to avoid multiple clicks of Submit Button
       function CheckSubmit() {
           cmdSave = document.getElementById('<%= imgunenroll.ClientID %>');
           cmdSave.style.visibility = 'hidden';
           return true;
       }
		//MAIG - CH1 - END - Added Javascript method to avoid multiple clicks of Submit Button  
   </script>

</head><uc:EMailAlert id="mailalert"  runat="server" /><table cellSpacing="0" cellPadding="0" width="520" bgColor="#ffffcc" border="0">
	<tr>
		<td>
			<table width="620" bgcolor="#ffffcc">
                <tr>
                    <td class="arial_12_bold" width="170">
                   &nbsp;&nbsp;<asp:Label ID="Header" runat ="server" text="Enrollment Payment Details"></asp:Label> 
                        </td>
                    <td class="arial_12_bold" width="170">
                        <asp:LinkButton ID="lnkChangePaymentMethod" runat="server" 
                            onclick="lnkChangePaymentMethod_Click">Change Payment Method</asp:LinkButton>
                    </td>
                </tr>
                <tr>
                    <td height="5">
                    </td>
                </tr>
                <tr>
                    <td class="arial_12_bold" width="170">
                        &nbsp;&nbsp;Payment Method</td>
                    <td class="arial_12">
                        <asp:Label ID="lblPaymentMethod" runat ="server"></asp:Label> 
                    </td>
                </tr>
                 <tr>
                    <td height="3">
                    </td>
                </tr>
                <tr>
                    <td class="arial_12_bold" width="170">
                        &nbsp;&nbsp;Routing Number</td>
                    <td class="arial_12">
                        <asp:Label ID="lblRoutingNumber" runat ="server"></asp:Label> 
                    </td>
                </tr>
                <tr>
                    <td height="5">
                    </td>
                </tr>
                <tr>
                    <td class="arial_12_bold" width="170">
                        &nbsp;&nbsp;Bank Name</td>
                    <td class="arial_12">
                        <asp:Label ID="lblBankName" runat ="server"></asp:Label> 
                    </td>
                </tr>
                <tr>
                    <td height="5">
                    </td>
                </tr>
                <tr>
                    <td class="arial_12_bold" width="170">
                        &nbsp;&nbsp;Account Number</td>
                    <td class="arial_12">
                        <asp:Label ID="lblAccountNumber" runat ="server"></asp:Label> 
                    </td>
                </tr>
                <tr>
                    <td height="5">
                    </td>
                </tr>
                <tr>
                    <td class="arial_12_bold" width="170">
                        &nbsp;&nbsp;Account Holder Name</td>
                    <td class="arial_12">
                        <asp:Label ID="lblAccountHolderName" runat ="server"></asp:Label> 
                    </td>
                </tr>
                <tr>
                    <td height="5">
                    </td>
                </tr>
                <tr>
                    <td class="arial_12_bold" width="170">
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
				    <td colspan="2" height="22">
						<!-- MAIG - CH2 - START - Added Javascript method to avoid multiple clicks of Submit Button-->
					     <asp:imagebutton id="imgunenroll" runat="server" width="168" 
                             alt="UnEnrollRecurring" border="0" OnClientClick="CheckSubmit();" align="right" height="20" 
                             imageurl="/PaymentToolimages/btn_unEnrollForRecurring.gif" 
                             onclick="imgunenroll_Click"></asp:imagebutton>
 						<!-- MAIG - CH2 - END - Added Javascript method to avoid multiple clicks of Submit Button-->
    			    </td>
				</tr>
				<%--<tr>
				<td>
				 <asp:Label ID="lblErrorMsg" runat="server" Text="" CssClass="arial_12_bold_red" Visible="false"></asp:Label>
                    
                    </td></tr>--%>
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
</html>