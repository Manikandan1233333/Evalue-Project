<%@ Reference Control="~/Controls/InsPayment.ascx" %>
<%@ Reference Control="~/Controls/Buttons.ascx" %>
<%@ Reference Control="~/Controls/PageValidator.ascx" %>

<%@ Page Language="c#" CodeBehind="Insurance.aspx.cs" AutoEventWireup="True" Inherits="MSC.Forms.Insurance" %>

<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="uc" TagName="Summary" Src="../Controls/OrderSummary.ascx" %>
<%@ Register TagPrefix="uc" TagName="Buttons" Src="../Controls/Buttons.ascx" %>
<%@ Register TagPrefix="uc" TagName="PageValidator" Src="../Controls/PageValidator.ascx" %>
<!-- Renaming the InsPayment_New.ascx to InsPayment.ascx for Src -->
<%@ Register TagPrefix="uc" TagName="InsPayment" Src="../Controls/InsPayment.ascx" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<!-- PT.Ch1 - Modified by COGNIZANT on 9/12/2008
Modified the static text displayed on the Insurance payment as per request from business
 -->
<!-- SR#8434145.Ch1:Added a new  Image control ,Lable control  and Checkbox on July 15 2009 for duplicate payment alert. -->
<!--Added a new javascript function chkVisibility()to the onload event of body to enable the continue
 button as a part of Billing and Payments Quick Hits Project- RFC 48347 on 30 Nov 2009 for the defect 120 -->
<!--TimeZoneChange.Ch1-Modified by cognizant on 08-31-2010 as a part of Time zone change enhancement-->
<!--CHG0055954-AZ PAS conversion and PC integration-Modified the payment posting alert message as part of time zone change-->
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head>
    <title>Insurance Payment</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <!--SR#8434145 - START: Added by COGNIZANT  on  July 15 2009 for duplicate payment alert -->
    <!--SR#8434145 - END-->
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
    </style>
</head>
<!--Added a javascript function to the body element as a part of Billing and Payments Quick Hits Project- RFC 48347 on 30 Nov 2009 for the defect 120.-->
<body>
    <!-- Modified the form tag to set autocomplete to off by cognizant on 02/03/2011 -->
    <form id="Form1" method="post" runat="server" autocomplete="off">
    <!-- Page title-->
    <table cellpadding="0" cellspacing="0">
        <tr style="width: 920px">
            <td height="10">
            </td>
        </tr>
        <tr>
            <td class="style4">
            </td>
            <td align="left" class="arial_15_bold">
                Insurance Payment
            </td>
        </tr>
        <tr>
            <td class="style4">
            </td>
            <td  height="4" >
            </td>
        </tr>
        <tr>
            <td class="style4">
            </td>
            <td bgcolor="#999999" height="2" style="width: 600px; overflow: auto">
            </td>
        </tr>
        <tr>
            <td class="style4">
            </td>
            <td height="8" class="style2">
                <uc:Summary runat="server" Title="Order Summary" ID="Summary" />
            </td>
        </tr>
        <tr>
            <td colspan="20" height="2">
            </td>
        </tr>
        <!-- 3-column table totaling 750: 30, 520, 200 -->
        <tr>
            <td class="style4">
            </td>
            <td style="width: 140px">
                <!-- START - Renaming the InsPayment_New.ascx to InsPayment.ascx for control file -->
                <MSC:ItemsControl ID="Items" runat="server" hidefirst="False" controlfile="/Controls/InsPayment.ascx" />
                <!-- END - Renaming the InsPayment_New.ascx to InsPayment.ascx for control file -->
            </td>
        </tr>
        <tr>
            <td class="style4">
            </td>
            <td height="3" bgcolor="#ffffcc" class="style1">
            </td>
        </tr>
        <tr>
            <td class="style4">
            </td>
            <td bgcolor="#999999" height="3" class="style1">
            </td>
        </tr>
    </table> 
     
   
    <table id="Table1" cellpadding="0" cellspacing="0" runat="server">
        <!-- 11/21/2005 - START - Code modified for displaying the payment processing message as part of CSR #4254 fix -->
        <!-- 10/19/2005 - START - Code Added for displaying the payment processing message as part of Q4 - CSR changes -->       
         <tr >            
            <td >            
               <uc:PageValidator bgcolor="#ffffcc" runat="server" ID="PageValidator2" />
            </td>
        </tr>   
        <tr>
            <td style="width: 602px">
            </td>
            <td style="width: 27px">
                <uc:Buttons runat="server" ID="Buttons1" />
            </td>
        </tr>
        <tr>
            <td style="background-color: #ffffcc; color: red; font-weight: bold" class="arial_11_Bold">
                <u>Payment Posting Alert</u>
            </td>
        </tr>
        <tr>
            <td style="background-color: #ffffcc" class="arial_11">
		
				<!--67811A0  - PCI Remediation for Payment systems -Changed the SIS payment Payment Posting Alert message as part of QA defect-->
				<!--CHG0055954-AZ PAS conversion and PC integration-Modified the payment posting alert message as part of time zone change-->
                <asp:Label ID="Label1" runat="server"><font color="black"><b>Payments accepted on weekends, holidays and after 8:00 p.m. PST will post the next business day. <br /><br /></font></asp:Label>
                <%--<b>Legacy</b> - Credit Card and e-check payments accepted on weekends, holidays and after 7:00 p.m. PST will post <br />the next business day. Cash and Check payments accepted will post the next business day after the<br /> transaction is approved.<br />--%>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
