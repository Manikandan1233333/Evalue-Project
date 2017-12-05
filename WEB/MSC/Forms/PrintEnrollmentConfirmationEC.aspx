<%--CHG0109406 - CH1 - Increased the width to display the Timezone in a single row--%>
<%--CHG0109406 - CH2 - Increased the width to display the Timezone in a single row--%>
<%--CHG0110069 - CH1 - Modified the label text from "Enrolled Date" to "Enrolled Date/Time"--%>
<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PrintEnrollmentConfirmationEC.aspx.cs" Inherits="MSC.Forms.PrintEnrollmentConfirmationCC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" ><HEAD>
		<title>AAA Insurance - Recurring Enrollment</title>
		<LINK href="../style.css" type="text/css" rel="stylesheet">
		<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<title>AAA Insurance - Recurring Enrollment</title>
		<!-- PT.Ch1 - START: Added by COGNIZANT - 7/01/2008 - To enable default printing of the Print Version -->
		<script language="javascript">
		
		</script>
		<!-- PT.Ch1 - END -->
	</HEAD>
	<body>
        <form id="form1" runat="server">
        <table style="margin:10px 0px 0px 10px;font-family: verdana;width:800px;color:#333333;font-size:13px;">
            <tr>
                <td  style="text-align: left;padding:5px 0px 5px 0px;">
                    <img src="/PaymentToolimages/Logo.png" alt="AAA Logo" title="AAA Logo" style="vertical-align:top;margin-right:385px" /><img src="/PaymentToolimages/thank-email.jpg" width="299" height="74" />
                </td>
            </tr>
            <tr>
                <td style="text-align: left;padding:5px 0px 5px 0px;">Dear Member,</td>
            </tr>
            <tr>
                <td style="text-align: left;padding:5px 0px 5px 0px;"><asp:Label ID="lblEnrollMsg" runat="server"></asp:Label></td>
            </tr>
            <tr>
                <td style="text-align: left;padding:5px 0px 15px 0px;">Please review your transaction details as payments for Policy 
                    <asp:Label ID="lbl_ECPolicyNumber" style="font-weight:bold" runat="server"></asp:Label>
                    will be processed as per the below enrollment details.</td></tr>
<tr><td style="text-align: left;margin-top:5px;padding:8px 0px 8px 5px;background-color:#D1EBF1;font-weight:bold;color:#38333A;">Confirmation</td></tr>
<tr><td style="text-align: left;padding:5px 0px 5px 0px;">
		<table style=" width: 400px;margin: 5px 0;">
			<tr><td style="text-align: left;padding:5px 0px 5px 0px;width: 200px;">Transaction Status</td>
			<td style="text-align: left;padding:5px 0px 5px 0px;color:#231F20;font-weight:bold;" >Success <img src="/PaymentToolimages/correct-icon.png" alt="Success"/></td></tr>
			</table>
	</td>
</tr>
<tr><td style="text-align: left;margin-top:5px;padding:8px 0px 8px 5px;background-color:#D1EBF1;font-weight:bold;color:#38333A;">Enrolled Payment Details</td></tr>
<tr><td style="text-align: left;padding:5px 0px 5px 0px; margin-left: 40px;">
		<asp:PlaceHolder ID ="ccdetails" runat ="server">
		<%--CHG0109406 - CH1 - BEGIN - Increased the width to display the Timezone in a single row--%>
		<table style=" width: 600px;margin: 5px 0;">
		<%--CHG0110069 - CH1 - BEGIN - Modified the label text from "Enrolled Date" to "Enrolled Date/Time"--%>
			<tr><td style="text-align: left;padding:5px 0px 5px 0px;width: 200px;">Enrolled Date/Time</td>
			<td style="text-align: left;padding:5px 0px 5px 0px;color:#231F20;font-weight:bold;width:400px;" >
                <asp:Label ID="lbl_ECEnrolledDate" runat="server"></asp:Label>
                </td></tr>
                <%--CHG0110069 - CH1 - END - Modified the label text from "Enrolled Date" to "Enrolled Date/Time"--%>
                <%--CHG0109406 - CH1 - END - Increased the width to display the Timezone in a single row--%>
			<tr><td style="text-align: left;padding:5px 0px 5px 0px;">Name on Card</td>
			<td style="text-align: left;padding:5px 0px 5px 0px;color:#231F20;font-weight:bold;" >
                <asp:Label ID="lbl_CustName" runat="server"></asp:Label>
                </td></tr>
                <tr><td style="text-align: left;padding:5px 0px 5px 0px;">Credit Card Number</td>
			<td style="text-align: left;padding:5px 0px 5px 0px;color:#231F20;font-weight:bold;" >
                <asp:Label ID="lbl_AccNumber" runat="server"></asp:Label>
                </td></tr>
		</table>
		
 </asp:PlaceHolder> 
		<br />
		<asp:PlaceHolder ID ="ecdetails" runat ="server">
		<%--CHG0109406 - CH2 - BEGIN - Increased the width to display the Timezone in a single row--%>
		<table style=" width: 600px;margin: 5px 0;">
			<tr><td style="text-align: left;padding:5px 0px 5px 0px;width: 200px;">Enrolled Date</td>
			<td style="text-align: left;padding:5px 0px 5px 0px;color:#231F20;font-weight:bold;width:400px;" >
                <asp:Label ID="lbl_EcDate" runat="server"></asp:Label>
                </td></tr>
                <%--CHG0109406 - CH2 - END - Increased the width to display the Timezone in a single row--%>
			<!--<tr><td style="text-align: left;padding:5px 0px 5px 0px;">Account Nickname</td>
			<td style="text-align: left;padding:5px 0px 5px 0px;color:#231F20;font-weight:bold;" >My Chase Savings</td></tr>-->
			<tr><td style="text-align: left;padding:5px 0px 5px 0px;">Account Type</td>
			<td style="text-align: left;padding:5px 0px 5px 0px;color:#231F20;font-weight:bold;" >
                <asp:Label ID="lbl_EcAcntType" runat="server"></asp:Label>
                </td></tr>
                <tr><td style="text-align: left;padding:5px 0px 5px 0px;">Bank Name</td>
			<td style="text-align: left;padding:5px 0px 5px 0px;color:#231F20;font-weight:bold;" >
                <asp:Label ID="lbl_EcBankName" runat="server"></asp:Label>
                </td></tr>
			<tr><td style="text-align: left;padding:5px 0px 5px 0px;">Account Number</td>
			<td style="text-align: left;padding:5px 0px 5px 0px;color:#231F20;font-weight:bold;" >
                <asp:Label ID="lbl_EcAccNum" runat="server"></asp:Label>
                </td></tr>
			<tr><td style="text-align: left;padding:5px 0px 5px 0px;">Account Holder Name</td>
			<td style="text-align: left;padding:5px 0px 5px 0px;color:#231F20;font-weight:bold;" >
                <asp:Label ID="lbl_EcName" runat="server"></asp:Label>
                </td></tr>
		</table>
		
 </asp:PlaceHolder> 
	</td>
</tr>
<asp:PlaceHolder ID ="EmailHeader" runat ="server">
<tr><td style="text-align: left;margin-top:5px;padding:8px 0px 8px 5px;background-color:#D1EBF1;font-weight:bold;color:#38333A;">Email Notification</td></tr>
<tr><td style="text-align:left;border-top:1px solid #D9D9D9; padding: 10px 0;">An 
    e-mail confirmation will be sent shortly at
    <asp:Label ID="lblEmailId" runat="server" style="font-weight: 700"></asp:Label>
    </td></tr>
     </asp:PlaceHolder> 
</table>
        </form>
</body>
</html>


