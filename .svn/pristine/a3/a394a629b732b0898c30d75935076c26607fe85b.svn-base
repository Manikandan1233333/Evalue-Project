<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchResults.aspx.cs" Inherits="MSC.SearchResults" %>
<%@ Register TagPrefix="ln" TagName="LastName" Src="../Controls/PolicyLastName.ascx" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%--<%@ Register TagPrefix="btn" TagName="Buttons" Src="../Controls/Buttons.ascx" %>
--%><!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Policy Name Search</title>
    <link href="../style.css" rel="stylesheet" type="text/css" />
    <style type="text/css">
        .style1
        {
            width: 1138px;
        }
        .style2
        {
            width: 550px;
        }
        .style4
        {
            width: 170px;
        }
        .style5
        {
            width: 562px;
        }
        .divMenu
        {
            text-align:left;
            }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="divMenu">
    <table width="520" bgcolor="#ffffcc">
    <tr>
                    <td class="arial_11_bold_white" bgcolor="#333333" colspan="3" height="22">
                        &nbsp;&nbsp;Policy Name Search
                        <asp:Label ID="LabelN" Visible="False" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="5" class="style1">
                    </td>
                    <td width="250" height="5">
                    </td>
                    <td width="100" height="5">
                    </td>
                </tr>
                <tr>
                    <td>                       
                    </td>
                </tr>
                <tr>
                    <td colspan="3" height="5">
                    </td>
                </tr>
    <tr>
                                <CSAA:Validator ID="vldFirstName" runat="server" Width="100px" ControlToValidate="txtFirstName" >
				                   &nbsp;&nbsp;First Name</CSAA:Validator>
                                <td class="arial_12" align="left">
                                <asp:TextBox ID="txtFirstName" runat="server" Width="165" MaxLength="25" enableviewstate="true"  class="border_Non_ReadOnly" ></asp:TextBox>
                                </td>
                                <CSAA:Validator ID="vldFirstNameAlpha" runat="server" Display="None" OnServerValidate="CheckFirstName"
                                    ErrorMessage="Member First Name must be alphanumeric"></CSAA:Validator>
                                <%--<CSAA:Validator ID="vldName" runat="server" Display="None" OnServerValidate="CheckLength"
                                    ErrorMessage="The name is too long.It can accommodate a maximum of 25 characters total for the name including spaces between names."></CSAA:Validator>--%>
                            </tr>
                            <ln:LastName ID="txtLastName" runat="Server" Required="True" />
                            <tr>
                            <CSAA:Validator ID="vldMailingZip" runat="server" ControlToValidate="txtMailingZip" >
				                   &nbsp;&nbsp;Mailing Zip</CSAA:Validator>
                                <td class="arial_12">
                                <asp:TextBox ID="txtMailingZip" runat="server" Width="165" MaxLength="5" enableviewstate="true"  class="border_Non_ReadOnly" ></asp:TextBox>
                                </td>
                                <CSAA:Validator ID="vldMailingZipReq" runat="server"
                                    OnServerValidate="ReqValZipCheck"></CSAA:Validator>
                            </tr>
             <tr>
            <td class="style4">
            </td>
        </tr>
    </table>
    <table id="Table1" cellpadding="0" cellspacing="0" runat="server">     
<tr>
	<td height="3" bgcolor="#cccccc">
	</td>
</tr>
<tr>
	<td align="right" bgcolor="#cccccc" height="22" valign="bottom" style="WIDTH: 520px">
		<asp:imagebutton id="CancelButton" runat="server" causesvalidation="False" imageurl="../images/btn_cancel.gif" onclick="CancelButton_Click" />
		<asp:imagebutton id="ContinueButton" runat="server" imageurl="../images/btn_continue.gif" onclick="ContinueButton_Click" />&nbsp;&nbsp;
	</td>
</tr>
        </table>
    </div>
    </form>
</body>
</html>
