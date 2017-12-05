<!-- MAIG - CH1 - Added the table to align properly -->
<!-- MAIG - CH2 - Added the table closing tag and tr to align properly --> 
<!-- CHG0109406 - CH1 - Change to update ECHECK to ACH - 01202015 -->
<%@ Control Language="c#" AutoEventWireup="false" CodeBehind="PaymentMethod.ascx.cs"
    Inherits="MSC.Controls.PaymentMethod" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<html>

<table cellspacing="0" cellpadding="0" width="620" bgcolor="#ffffcc" border ="0">
    <tr>
        <td>
        <!-- MAIG - CH1 - BEGIN - Added the table to align properly -->
        <table cellspacing="0" cellpadding="0" width="620" bgcolor="#ffffcc" border ="0">
           <tr>
                <td colspan="2" class="arial_11_bold_white"  bgcolor="#333333" height="22" >
                    &nbsp; Choose Payment Method
                </td>
           </tr>
            <tr>
                            <td height="7">
        <!-- MAIG - CH1 - END - Added the table to align properly -->
                            </td>
                        </tr>
           <tr>
                <td class="arial_12_bold" width="198">
                    &nbsp;&nbsp; Payment Method
                </td>
                <td>
                <!-- CHG0109406 - CH1 - BEGIN - Change to update ECHECK to ACH - 01202015 -->
                    <asp:DropDownList id="_PaymentType" Width="170" runat="server" AutoPostBack="true" OnSelectedIndexChanged="_PaymentType_SelectedIndexChanged">
                    <asp:ListItem Text="Select type" Value="0" Selected="true" />
                    <asp:ListItem Text="Credit Card" Value="3"/>                     
                    <asp:ListItem Text="ACH" Value="4" />                     
                </asp:DropDownList>
                <!-- CHG0109406 - CH1 - END - Change to update ECHECK to ACH - 01202015 -->
                </td>
                <td>
                    <CSAA:Validator ID="vldPaymentType" runat="server" ControlToValidate="_PaymentType" 
                        OnServerValidate="ReqValPaymentTypeCheck"></CSAA:Validator>
                </td>
               
                
           </tr>   
		   <!-- MAIG - CH2 - BEGIN - Added the table closing tag and tr to align properly --> 
             <tr>
                            <td height="3">
                            </td>
                        </tr> 
           </table>
		   <!-- MAIG - CH2 - END - Added the table closing tag and tr to align properly --> 
        </td>
    </tr>
</table>
</html>
 