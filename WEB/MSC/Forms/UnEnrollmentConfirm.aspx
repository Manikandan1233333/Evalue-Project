<%@ Page language="c#" Codebehind="UnEnrollmentConfirm.aspx.cs" AutoEventWireup="True" Inherits="MSC.Forms.UnEnrollmentConfirm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%@ Register TagPrefix="uc" TagName="UnEnrollCC" Src="../Controls/UnEnrollRecurringCC.ascx" %>
<%@ Register TagPrefix="uc" TagName="UnEnrollECheck" Src="../Controls/UnEnrollRecurringECheck.ascx" %>
<%@ Register TagPrefix="uc" TagName="PaymentMethod" Src="../Controls/PaymentMethod.ascx" %>
<html>
	<head>
		<title>RecurringConfirmation</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body>
		<form id="ThankYouUnEnrollment" method="post" runat="server">
		    <table width="620" cellpadding="0" cellspacing="0" bgcolor="#ffffcc">
                <tr>
                    <td colspan="2" align="left" class="arial_15_bold" align ="center">
                        Thank You.
                    </td>
                    <td width="30">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" height="2">
                    </td>
                </tr>
                <tr>           
                    <td colspan="2" align="left" class="arial_15_bold">UN-Enrollment was Successful.</td>
                </tr>
                <tr>
                    <td colspan="2" height="2">
                    </td>
                </tr>
                 <tr>           
                    <td colspan="2" align="left" class="arial_12_bold"><font color="#FF0000"> Unenrollment information may take two business days to appear online.</font></td>
                </tr>
                <tr>
                    <td colspan="2" height="8" class="arial_12_bold"><font color="#FF0000"> <asp:Label ID="lblValMessage" runat="server" Visible="true"/></font>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table width="620" bgcolor="#ffffcc" align="left">
                           
                            <tr>
                                <td height="5">
                                </td>
                            </tr>
                            <tr>
                                <td class="arial_12_bold" width="191">
                                    &nbsp;&nbsp;Date & Time
                                </td>
                                <td class="arial_12" width="191">
                                    <asp:Label ID="lblUnDate" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td height="5">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                       
                                    <table width="620" bgcolor="#cccccc">
                                        <tr>
						                    <td bgcolor="#cccccc" height="22">
							                     <asp:imagebutton id="imgUnManageEnrollment" runat="server" width="170" alt="search" border="0" align="right" height="17" imageurl="/PaymentToolimages/btn_ManageAnotherRecurring.gif" OnClick="imgUnManageEnrollment_onclick"></asp:imagebutton>
						                    </td>
						                </tr>
                                    </table> 
                                </td>
                            </tr>
						 </table>  
                    </td>
                </tr>
        </table>
		</form>
	</body>
</html>
