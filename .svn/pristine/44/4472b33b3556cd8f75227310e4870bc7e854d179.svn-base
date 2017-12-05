	<!--MAIG - CH1 - Modidfied the Width of the td from 520 to 100%-->
	<!--MAIG - CH2 - Modidfied the javascript code to support Firefox browser -->
<%@ Control Language="C#" AutoEventWireup="false" CodeBehind="Check.ascx.cs" Inherits="MSC.Controls.Check" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<style type="text/css">
    .style1
    {
        width: 78px;
    }
</style>
	<!--MAIG - CH1 - BEGIN - Modidfied the Width of the td from 520 to 100%-->
<table cellspacing="0" cellpadding="0" width="100%" bgcolor="#ffffcc" border="0">
	<!--MAIG - CH1 - END - Modidfied the Width of the td from 520 to 100%-->
	<tr>
		<td class="arial_11_bold_white" bgcolor="#333333" height="22">&nbsp;&nbsp;Check 
			Information</td>
	</tr>	
	<tr>
		<td>
			<table cellspacing="0" cellpadding="0" >
				
				<tr>
		<td height="10"></td>
		</tr>
		
				
				<tr>
				<td style="WIDTH: 119px">	
				<CSAA:validator id="vldchknumber" runat="server"  controltovalidate="ChkNumber" Errormessage="Please fill the Check Number field" onservervalidate="ReqValCheck1" tag="span">&nbsp;&nbsp;&nbsp;Check Number</CSAA:validator>
					<csaa:validator id="vldchkLength" controltovalidate="ChkNumber" errormessage="Check Number should be of maximum 6 characters." Display="None" onservervalidate="ValidchkLength" runat="server"></csaa:validator>		
					
								
					
					</td>
					<td class="style1">					
					    &nbsp;</td>	
					<td style="WIDTH: 100px">
						<!--MAIG - CH2 - BEGIN - Modidfied the javascript code to support Firefox browser -->
						<asp:textbox onkeypress="return digits_only_onkeypress(event)" id="ChkNumber" 
                            runat="server" maxlength="6" Width="174px"></asp:textbox>
						<!--MAIG - CH2 - END - Modidfied the javascript code to support Firefox browser -->
					</td>				
			</tr>
			</table>
		</td>	
</table>
