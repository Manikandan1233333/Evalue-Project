<%@ Control Language="c#" AutoEventWireup="True" Codebehind="Agency.ascx.cs" Inherits="MSCR.Controls.Agency" TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<script>
function filter_onkeypresslocal(allow,event) {
        if (navigator.appName.indexOf("Netscape") == -1) {
            var ch = String.fromCharCode(event.keyCode);
            event.returnValue = (allow.indexOf(ch) >= 0);
        } else {
        //var ch = String.fromCharCode(event.char);
        var key = event.keyCode || event.which;
        var ch = String.fromCharCode(key);
        return (allow.indexOf(ch) >= 0);
        }
    }

    function digits_only_onkeypresscomma(event) {
        return filter_onkeypresslocal("1234567890,\r",event);
    }
</script>		
		<tr>
			<td>&nbsp;&nbsp;</td>
			<td class="arial_12_bold">Agency</td>
			<td colspan="3"></td>
		</tr>
		<tr>
			<td>&nbsp;&nbsp;</td>
			<td colspan="4" class="arial_12">
			<asp:TextBox ID ="_txtAgency" runat ="server" MaxLength = "9" onkeypress="return digits_only_onkeypresscomma(event)"></asp:TextBox>
			
			<%--<asp:RegularExpressionValidator ID = "vldAgency" runat="server" ControlToValidate ="_txtAgency" ErrorMessage ="Numeric only" ValidationExpression ="^[0-9]+$"></asp:RegularExpressionValidator>--%>
			</td>
		</tr>