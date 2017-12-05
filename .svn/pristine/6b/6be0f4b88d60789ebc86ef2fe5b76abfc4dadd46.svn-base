<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EMailAlertPopup.ascx.cs"
    Inherits="MSC.Controls.EMailAlertPopup" %>
<head>
    <style type="text/css">
        h1, h2, h3, h4, h5, h6
        {
            font-family: arial,helvetica,sans-serif;
            font-weight: normal;
            text-decoration: none;
        }
        h1
        {
            font-size: 20pt;
        }
        h2
        {
            font-size: 18pt;
        }
        h3
        {
            font-size: 16pt;
        }
        h4
        {
            font-size: 14pt;
        }
        h5
        {
            font-size: 12pt;
        }
        h6
        {
            font-size: 10pt;
        }
        #content
        {
            position: absolute;
            display: none;
            z-index: 2;
            top: 25%;
            left: 35%;
            background-color: white;
            border: 3px solid #009;
            margin: 0;
            padding: 0;
            width: 350px;
        }
        .arial_12_bold
        {
            font-family: arial, helvetica, sans-serif;
            font-size: 12pt;
            font-weight: bold;
            text-decoration: none;
        }
        .arial_12
        {
            font-family: arial, helvetica, sans-serif;
            font-size: 12pt;
            text-decoration: none;
        }
        .arial_10
        {
            font-family: arial, helvetica, sans-serif;
            font-size: 10pt;
            text-decoration: none;
        }
    </style>
</head>
<div id="content" runat="server" visible="false" style="position: absolute; z-index: 2;
    top: 25%; left: 35%; background-color: #ffffcc; border: 3px solid black">
    <table align="center">
        <tr>
            <td class="arial_12_bold" colspan="2">
                UnEnrollment Details
                <hr />
            </td>
        </tr>
        <tr>
            <td>
                <asp:HiddenField ID="hdnConfirmNum" runat="server" Value="" />
                <asp:HiddenField ID="hdnPolicyNumber" runat="server" Value="" />
                <asp:HiddenField ID="hdnPaymentType" runat="server" Value="" />
                <asp:HiddenField ID="hdnCCCustomerName" runat="server" Value="" />
                <asp:HiddenField ID="hdnCCNumber" runat="server" Value="" />
                <asp:HiddenField ID="hdnECCustomerName" runat="server" Value="" />
                <asp:HiddenField ID="hdnECAccType" runat="server" Value="" />
                <asp:HiddenField ID="hdnECBankName" runat="server" Value="" />
                <asp:HiddenField ID="hdnECAccNumber" runat="server" Value="" />
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <h6>
                    E-Mail Address (Optional)</h6>
            </td>
        </tr>
        <tr>
            <td class="arial_12_bold" colspan="2">
                <asp:TextBox ID="_txtEmailAddress" runat="server" Width="300px" MaxLength="50" EnableViewState="true"
                    class="border_Non_ReadOnly"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="left" width="50%">
                <asp:ImageButton ID="CancelBtn" runat="server" Width="67px" Height="17" ImageUrl="../images/btn_cancel.gif"
                    OnClick="CancelBtn_Click"></asp:ImageButton>
            </td>
            <td align="right" width="50%">
                <asp:ImageButton ID="SendMailBtn" runat="server" Width="67px" Height="17" ImageUrl="../images/btn_submit.gif"
                    OnClick="SendMailBtn_Click"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td class="arial_12" colspan="2">
                <asp:Label ID="lblError" Visible="False" runat="server" Text="" Font-Bold="True"
                    ForeColor="#CC3300"></asp:Label>
            </td>
        </tr>
    </table>
</div>
