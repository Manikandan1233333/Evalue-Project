<%@ Control Language="c#" AutoEventWireup="True" Codebehind="PolicyAmount.ascx.cs" Inherits="MSC.Controls.PolicyAmount" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<!--
		* HISTORY
		* MODIFIED BY COGNIZANT AS PART OF .NET MIGRATION CHANGES
		* CHANGES DONE
		* 04/13/2010 .NetMig.Ch1: Removed ErrorMessage and Added DefaultAction property	
-->
<%--<tr>--%>

	<%--<csaa:validator id="LabelValidator1" runat="server" controltovalidate="_Amount" >--%>
	<td class="arial_12_bold" >&nbsp;&nbsp;Payment Amount<font color="#ff0000">*</font>
	</td>
	<%--</csaa:validator>--%>
	<td>
	<!--PUP Ch1 -Modified the Width of the text box and drop down box to display the PUP product type by cognizant on 02/03/2011 -->
		<asp:textbox id="_Amount" columns="28" width="150" runat="server" 
    enableviewstate="False" maxlength="7"/>
	</td>
	<%--<td width="110"></td>--%>
<%--</tr>
<tr>
	<td colspan="3" height="1"></td>
</tr>--%>
<csaa:validator id="vldAmountDecimal" runat="server" display="None" errormessage="Payment Amount%Label% field must be a valid amount." onservervalidate="WholeCents" controltovalidate="_Amount"/>
<csaa:validator id="vldAmountPositive" runat="server" display="None" errormessage="Payment Amount%Label% must be positive." onservervalidate="PositiveAmount" controltovalidate="_Amount" />
<!--.NetMig.Ch1: Removed ErrorMessage property and added Defaultaction="Required" -->
<%--<csaa:validator id="Valid" runat="server" display="None" DefaultAction="Required" onservervalidate="ReqValCheck" />--%>


