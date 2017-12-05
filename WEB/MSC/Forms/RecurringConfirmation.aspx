<%@ Page language="c#" Codebehind="RecurringConfirmation.aspx.cs" AutoEventWireup="True" Inherits="MSC.Forms.RecurringConfirmation" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%@ Register TagPrefix="uc" TagName="echeckEnroll" Src="../Controls/eCheckEnrollment.ascx" %>
<%@ Register TagPrefix="uc" TagName="CreditCardEnroll" Src="../Controls/CreditCardEnrollment.ascx" %>
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
		<script type="text/javascript">


		    function popUpWindow(URL) {
		        newWin = window.open(URL, '', 'directories=0, height=600, location=0, menubar=0, resizable=0, scrollbars=0, status=0, titlebar=0, toolbar=0, width=925');
		        newWin.focus();
		    }
    </script>
	<!-- MAIG - CH1 - BEGIN - Added the code to align properly in IE 11 browser-->
    <style type="text/css">
        #gridRecurringConfirmation tbody tr th {text-align:left}
    </style>
	<!-- MAIG - CH1 - END - Added the code to align properly in IE 11 browser-->
	</head>
	<body>
		<form id="ThankYouEnrollment" method="post" runat="server">
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
                    <td colspan="2" align="left" class="arial_15_bold"><asp:label ID="lblResponse" Text="Enrollment was Successful." runat="server"></asp:label></td>
                </tr>
                <tr>
                    <td colspan="2" height="8">
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="left" class="arial_12_bold" height="2"> <font color="#FF0000"> Enrollment information may take two business days to appear online.</font></td>
                </tr>
                <tr>
                    <td colspan="2" align="left" class="arial_12_bold" height="2"><font color="#FF0000"><asp:label ID="lblpcErrMsg" runat="server" Visible="false" /></font></td>
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
                                    <asp:Label ID="lblDate" runat="server"></asp:Label>
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
                    <tr>
                            <td bgcolor="FFFFFF" height="15"></td></tr>
						     </tr>
                        <table width="620" border="3" bordercolor="#999999">                        
                            <tr>
                               <td colspan="2" class="arial_11_bold_white"  bgcolor="#CCCCCC" height="22" >
                                  <font color="black"> Policy Details</font>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" height="10" bgcolor="FFFFFF">			            			
								     <asp:GridView id="gridRecurringConfirmation" runat="server" width="620px" OnRowDataBound="gridRecurringConfirmation_DataBound" cssclass="arial_12"  AlternatingRowStyle-HorizontalAlign="Left"
                                         autogeneratecolumns="True" cellpadding="2" pagesize="20" visible="true" BorderStyle="None" GridLines="None" CaptionAlign="Left" 
                                          ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" FooterStyle-BorderWidth="1" FooterStyle-CssClass="border_Top_Down" HeaderStyle-HorizontalAlign="Left">
	    		            			<HeaderStyle height="10px" borderwidth="0px" borderstyle="Solid" bordercolor="#CCCCCC" cssclass="arial_12_bold" backcolor="#FFFFFF" />
						                <footerstyle BorderStyle="Double" />
						                </asp:GridView>
						                </td>
						                </tr>							  
								  <tr>
                            <td bgcolor="FFFFFF" height="15"></td></tr>
						    
						     </table>
						     <table>
						     <tr>
                            <td bgcolor="FFFFFF" height="15"></td></tr>
				            <tr>
                                <td colspan="2" class="arial_11_bold_white"  bgcolor="#333333" height="22" >
                                    Recurring Payment Details
                                </td>
                            </tr>
                                        <tr>
                                        <td>
                                <uc:UnEnrollCC runat="Server" Visible="false" id="UnEnrollCC" />
                                <uc:UnEnrollECheck runat="Server" Visible="false" id="UnEnrollECheck" />
                                </td></tr>
                                    </table>
                                    <table width="620" bgcolor="#cccccc">
                                        <tr>
						                    <td bgcolor="#cccccc" height="22">
							                     
							                     <asp:imagebutton id="imgPrint" runat="server" width="60px" alt="search" border="0" align="right" height="17px" 
imageurl="/PaymentToolimages/btn_Print_Version.gif" onclick="imgPrint_Click"  OnClientClick=""></asp:imagebutton>

							                     <asp:imagebutton id="imgManageEnrollment" runat="server" width="170" alt="search" border="0" align="right" height="17" imageurl="/PaymentToolimages/btn_ManageAnotherRecurring.gif" OnClick="imgManageEnrollment_onclick"></asp:imagebutton>
						                    </td>
						                </tr>
                                    </table>                                
						 </table>                     
                <tr>
                   <td>
                   <asp:Label ID="lblErrorMsg" runat="server" Text="" class="arial_12_red" Visible="false" ></asp:Label>
                   </td>
                   </tr>
		</form>
	</body>
</html>
