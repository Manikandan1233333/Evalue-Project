	<!--MAIG - CH1 - Modidfied the Width of the td from 520 to 600-->
<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Buttons.ascx.cs" Inherits="MSC.Controls.Buttons" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<tr>
	<td height="3" bgcolor="#cccccc">
	</td>
</tr>
<tr>
	<!--MAIG - CH1 - BEGIN - Modidfied the Width of the td from 520 to 600-->
	<td align="right" bgcolor="#cccccc" height="22" valign="bottom" style="WIDTH: 600px">
 	<!--MAIG - CH1 - END - Modidfied the Width of the td from 520 to 600-->
		<asp:imagebutton id="BackButton" runat="server" imageurl="../images/btn_back.gif" onclick="BackButton_Click" />
		<asp:imagebutton id="CancelButton" runat="server" causesvalidation="False" imageurl="../images/btn_cancel.gif" onclick="CancelButton_Click" />
		<asp:imagebutton id="ContinueButton" runat="server" imageurl="../images/btn_continue.gif" onclick="ContinueButton_Click" />&nbsp;&nbsp;
	</td>
</tr>
<input type="hidden" id="CancelUrl" runat="server" />
<script language="javascript"><%if (ContinueButton.Visible) {%>
	setFormEnterButton("<%=ContinueButton.UniqueID%>");<%}%>
	var Clicked='';
	function CheckClicked(b) {
		if (Clicked=='') return false;
		if (Clicked!=b.name) alert("Please wait for page to finish loading.");
		return true;
	}
	function CancelButton_OnClick() {
		if (CheckClicked(this)) return false;
		if (!confirm("Are you sure you want to cancel?")) return false; 
		Clicked=this.name;
		return true;
	}
	function Button_OnClick() {
		if (CheckClicked(this)) return false;
		Clicked=this.name;
		return true;
	}
	document.all("<%=CancelButton.UniqueID%>").onclick=CancelButton_OnClick;
	if (document.all("<%=ContinueButton.UniqueID%>"))
		document.all("<%=ContinueButton.UniqueID%>").onclick=Button_OnClick;
	if (document.all("<%=BackButton.UniqueID%>"))
		document.all("<%=BackButton.UniqueID%>").onclick=Button_OnClick;
</script>
