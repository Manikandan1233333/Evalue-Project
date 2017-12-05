<%--CHG0110069 - CH1 - Appended the /Time along with the Date Label--%>
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PrintHeader.ascx.cs" Inherits="MSCR.Controls.PrintHeader" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %> <!--Modified as part of STAR AUTO 2.1 on 08/17/2007	STAR AUTO 2.1.CH1: Added RepDO field in the header	STAR AUTO 2.1.CH2: Added id - trRepDO to the table row RepDO field-->
<table height="14" cellSpacing="0" cellPadding="0" width="604" border="0">
	<tr>
		<td class="arial_12_bold" style="COLOR: white" align="left"><IMG height="41" src="../images/msc_logo_sm.gif" align="left" border="0"></td>
	</tr>
	<tr>
		<td></td>
	</tr>
	<tr>
		<td></td>
	</tr>
</table>
<table class="arial_12" id="tblDetails" width="606" runat="server">
	<tr>
		<td colSpan="3"><b><asp:label id="lblTitle" runat="server"></asp:label></b></td>
	</tr>
	<tr>
		<td colSpan="3">&nbsp;</td>
	</tr>
	<%--CHG0110069 - CH1 - BEGIN - Appended the /Time along with the Date Label--%>
	<tr>
		<td><b>Run Date/Time:</b></td>
		<td align="left"><asp:label id="lblRunDate" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td><b>Date/Time Range:</b></td>
		<td colSpan="3"><asp:label id="lblDateRange" runat="server"></asp:label></td>
	</tr>
	<%--CHG0110069 - CH1 - END - Appended the /Time along with the Date Label--%>
	<tr>
		<td><b>Payment Type:</b></td>
		<td colSpan="3"><asp:label id="lblPayment" runat="server"></asp:label></td>
	</tr>
	<tr id="trProductType" runat="server">
		<td><b>Product Type:</b></td>
		<td colSpan="3"><asp:label id="lblProduct" runat="server"></asp:label></td>
	</tr>
	<tr id="trRevenueType" runat="server">
		<td><b>Revenue Type:</b></td>
		<td colSpan="3"><asp:label id="lblRevenue" runat="server"></asp:label></td>
	</tr>
	<tr id="trApplication" runat="server">
		<td><b>Application:</b></td>
		<td align="left"><asp:label id="lblApp" runat="server"></asp:label></td>
	</tr>
	<tr>
		<td><b><asp:label id="lblCsrTxt" runat="server"></asp:label></b></td>
		<td colSpan="3"><asp:label id="lblCsrVal" runat="server"></asp:label></td>
	</tr> <!--STAR AUTO 2.1.CH1: START - Added RepDO field in the header-->
	<tr id="trRepDO" runat="server">
		<td><b>Rep DO:</b></td>
		<td colSpan="3"><asp:label id="lblRepDOName" runat="server"></asp:label></td>
	</tr> <!--STAR AUTO 2.1.CH1: END-->
	<tr id="tr1" runat="server">
		<td><b>Status:</b></td>
		<td colSpan="3"><asp:label id="lblStatus" runat="server"></asp:label></td>
	</tr></table>
