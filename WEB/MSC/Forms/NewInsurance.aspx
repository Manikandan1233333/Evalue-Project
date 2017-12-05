<%@ Page language="c#" Codebehind="NewInsurance.aspx.cs" AutoEventWireup="True" Inherits="MSC.Forms.NewInsurance" %>
<%@ Register TagPrefix="uc" TagName="Summary" Src="../Controls/OrderSummary.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
	<head>
		<title>NewInsurance</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</head>
	<body ms_positioning="GridLayout">
		<form id="NewInsurance" method="post" runat="server">
			<uc:summary runat="server" id="Summary" title="Membership Pricing" />
		</form>
	</body>
</html>
