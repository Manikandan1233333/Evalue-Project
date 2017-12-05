<%@ Page language="c#" Codebehind="PaymentConfirmation.aspx.cs" AutoEventWireup="True" Inherits="MSC.Forms.PaymentConfirmation" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<%@ Register TagPrefix="uc" TagName="Summary" src="../Controls/OrderSummary_New.ascx" %>
<%@ Register TagPrefix="uc" TagName="PageValidator" src="../Controls/PageValidator.ascx" %>
<%@ Register TagPrefix="uc" TagName="Buttons" Src="../Controls/Buttons.ascx" %>
<!--
		* HISTORY
		* MODIFIED BY COGNIZANT AS PART OF .NET MIGRATION CHANGES
		* CHANGES DONE
		* 03/16/2011 PT_echeck Ch1 Added the table to display the echeck information in the payyment confirmation page by cognizant.
		* 67811A0  - PCI Remediation for Payment systems CH1:Renamed the Title as Payment Acknowledgement instead of Payment Confirmation by Cognizant 
		* 67811A0  - PCI Remediation for Payment systems CH2:Added checkbox and a label to display the authorisation text for the transaction done by cognizant.
		* 67811A0  - PCI Remediation for Payment systems CH3:Javascipt to handle the tool tip and disable/enable the Continue button when the checkbox is checked.
		* MAIG - CH1 - Added the code to pass the Company Code and Source System
		* MAIG - CH2 - Added the logic to display the last 4 digits of CC Number
		* MAIG - CH3 - Modified the setAttribute to removeAttribute
-->


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
<!-- Added the code to disable the browser back button after payment confirmation screen by cognizant on 07/25/2011 as a part of August 12 2011 releases.-->
  <script language="javascript">
      window.history.forward(1);                        
                  </script> 

	<HEAD>
	<!-- 67811A0  - PCI Remediation for Payment systems CH1:START -Renamed the Title as Payment Acknowledgement instead of Payment Confirmation by Cognizant -->
		<title>Payment Acknowledgement</title>
	<!-- 67811A0  - PCI Remediation for Payment systems CH1:END -Renamed the Title as Payment Acknowledgement instead of Payment Confirmation by Cognizant -->
	
		<meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../style.css" type="text/css" rel="stylesheet">
	
	    <style type="text/css">
            .style1
            {
                font-family: arial, helvetica, sans-serif;
                color: #000000;
                font-size: 15px;
                width: 520px;
            }
            .style2
            {
                width: 520px;
            }
        </style>
	
	</HEAD>
	<body ms_positioning="GridLayout">
		<form id="PaymentConfirmation" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="690" border="0">
				<tr>
					<td colSpan="3" height="10"></td>
				</tr>
				<tr>
                    <td width="30" >
                    </td>
                </tr>
                <tr>
                <!-- Added a horizontal line beneath the Payment Acknowledgemetn as part of QA defects-->
                    <td colspan="3" align="left" class="arial_15_bold">
                        Payment Acknowledgement
                    </td>
                    <td width="30">
                    </td>
                </tr>
                <tr>
                    <td colspan="3" height="2">
                    </td>
                </tr>
                <tr>           
                    <td bgcolor="#999999" height="2" class="style1">
                    </td>
                 </tr>
                 <tr>
                    <td colspan="3" height="8">
                    </td>
                </tr>
							
				<tr>
					<td>
					<!-- Added the table to include the red colour arrow for the Payment confirmation message by cognizant on 02/03/2011 -->
					<table cellSpacing="0" cellPadding="0" bgColor="#ffffcc" border="0" width="520">
										<tr>
											<td colSpan="3" height="5"></td>
										</tr>
										<tr>
											<td vAlign="top"><IMG src="../images/red_arrow.gif"></td>
											<td width="10"></td>
											<td class="arial_11_bold" vAlign="top">Your payment has not yet been submitted for processing. Please review the  details and confirm the payment<br>
												</td>
												<asp:TextBox ID="HidddenAmountSelected" Visible="False" runat="server" MaxLength="40"></asp:TextBox>
												<asp:TextBox ID="HiddenDueDetails" Visible="False" runat="server" MaxLength="100"></asp:TextBox>
												<asp:TextBox ID="HiddenBillPLan" Visible="False" runat="server" MaxLength="40"></asp:TextBox>
												<asp:TextBox ID="HiddenPaymentPlan" Visible="False" runat="server" MaxLength="40"></asp:TextBox>
												<asp:TextBox ID="hdnAutoPay" Visible="False" runat="server" EnableViewState="true"></asp:TextBox>  
												<!-- MAIG - CH1 - BEGIN - Added the code to pass the Company Code and Source System -->
												<asp:TextBox ID="hdnEmailZip" Visible="False" runat="server" EnableViewState="true"></asp:TextBox> 
												<asp:TextBox ID="hdnSourceCompanyCode" Visible="False" runat="server" EnableViewState="true"></asp:TextBox> 
												<asp:TextBox ID="hdnRpbsBillingDetails" Visible="False" runat="server" EnableViewState="true"></asp:TextBox> 
												<asp:TextBox ID="hdnNameSearchData" Visible="False" runat="server" EnableViewState="true"></asp:TextBox> 
												<asp:TextBox ID="hdnDuplicatePolicyData" Visible="False" runat="server" EnableViewState="true"></asp:TextBox> 
												<!-- MAIG - CH1 - END - Added the code to pass the Company Code and Source System -->
										</tr>
										<tr>
											<td colSpan="3" height="5"></td>
										</tr>
									</table><!-- Added the table to include the red colour arrow for the Payment confirmation message by cognizant on 02/03/2011 -->
					</td>
					
				</tr>
			
			</table>
			<!-- step 2 table contains 3 tables: space:30, form table:520, space:200 -->
			<table cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<!-- space to left of form table-->
					<td width="30">
						<table cellSpacing="0" cellPadding="0" border="0">
							<tr>
								<td width="30"></td>
							</tr>
						</table>
					</td>
					<!-- form table -->
					<td width="520">
						<table cellSpacing="0" cellPadding="0" width="520" border="0">
							<tr>
								<td height="20"></td>
							</tr>
							
							<tr>
								<td><uc:summary id="Summary" runat="server"></uc:summary></td>
							</tr>
							
							<tr>
								<td height="10"></td>
							</tr>
							<uc:pagevalidator runat="server" colspan="3" id="PageValidator1" />
							<tr>
								<td height="10"></td>
							</tr>
							<tr>
								<td bgColor="#cccccc" height="22"></td>
							</tr>
							<tr  bgColor="#ffffcc">
								<td height="22" class="arial_11">
									<font class="arial_11_bold">&nbsp;Payment Method:</font>&nbsp;<asp:label id="lblPaymentCaption" runat="server"></asp:label></td>
							</tr>
							
							
							<tr id="tbrCCInfo" runat="server"  bgColor="#ffffcc">
								<td>
									<table cellSpacing="0" cellPadding="0" >
										<tr>
											<td height="15"></td>
										</tr>
										<tr>
											<td class="arial_11_bold"  height="22">
												&nbsp;<asp:label id="lblPaymentDesc" runat="server"></asp:label> Information</td>
										</tr>
										<tr>
										<td class="arial_11">
											&nbsp;Card Type:&nbsp;<asp:label id="lblCardDesc" runat="server"></asp:label><br>
											<!-- Security testing - To mask the credit card number displayed -->
											<!-- MAIG - CH2 - BEGIN - Added the logic to display the last 4 digits of CC Number -->
											&nbsp;Credit Card Number:&nbsp;<%if(Order.Card.CCType.Equals("2")){%>xxxxxxxxxxx<%=Order.Card.CCNumber.Substring(11,4)%><%}else {%>xxxxxxxxxxxx<%=Order.Card.CCNumber.Substring(12,4)%><%} %><br>
											<!-- MAIG - CH2 - END - Added the logic to display the last 4 digits of CC Number -->
											&nbsp;Expiration Date:&nbsp;<%=Order.Card.CCExpMonth%>/<%=Order.Card.CCExpYear%>
										</td>
										
										</tr>
										</table>	
								</td>
							</tr>
							<!--PT_echeck Ch1 Added the table to display the echeck information in the payyment confirmation page by cognizant on 03/16/2011 -->
							<tr id="tbrCheckInfo" runat="server"  bgColor="#ffffcc">
								<td>
									<table cellSpacing="0" cellPadding="0" >
										<!--<tr>
											<td height="15"></td>
			
										</tr>
										<tr>
											<td class="arial_11_bold"  height="22">
												&nbsp;<asp:label id="lblPaymentDesc1" runat="server"></asp:label> Information</td>
										</tr>-->
										<tr>
										<td class="arial_11_bold"  height="22">
												&nbsp;<asp:label id="lblcheckdesc" runat="server"></asp:label> Information:</td>
												</tr>
												<tr>
										<td class="arial_11">
										&nbsp;Account Type:&nbsp;<asp:label id="lblchecktype" runat="server"></asp:label><br>
											&nbsp;Routing Number:&nbsp;<%=Order.echeck.BankId%><br>
											&nbsp;Account Number:&nbsp;xxxxxxxxxxxxx<%=Order.echeck.BankAcntNo.Substring((Order.echeck.BankAcntNo.Length-4),4)%>
										</td>
										</tr>
										
									</table>	
								</td>
							</tr>
							<!--67811A0  - PCI Remediation for Payment systems CH2:START-Added checkbox and a label to display the authorisation text for the transaction done by cognizant.-->
							<tr  bgColor="#ffffcc">
						    <td class="arial_11">
								<asp:CheckBox ID="CBAuth" runat="server" onclick="javascript:checkUserAcknowledgment()" /><asp:Label ID="LblAuth" runat="server"></asp:Label>
							</td>
							</tr>
							<!--67811A0  - PCI Remediation for Payment systems CH2:EN-Added checkbox and a label to display the authorisation text for the transaction done by cognizant.-->							
							<tr>
								<td>
									<uc:buttons runat="server" id="Buttons1" />
								</td>
							</tr>
							</table>					
						
					<!-- space to right of form table -->
					<td width="200">
						<table cellSpacing="0" cellPadding="0" width="200">
							<tr>
								<td width="200"></td>
							</tr>
						</table>
					</td>
					<!-- end of step 2 table --></tr>
			</table>
			
		</form>
	</body>
</HTML>
<script language="javascript">
    //* 67811A0  - PCI Remediation for Payment systems CH3:START Javascipt to handle the tool tip and disable/enable the Continue button when the checkbox is checked.
    function checkUserAcknowledgment() {
        var chkbox = document.getElementById('CBAuth').checked;
        if (!chkbox) {
            document.getElementById('Buttons1_ContinueButton').setAttribute('disabled', true);
            document.getElementById('Buttons1_ContinueButton').title = "Please check the checkbox";
        }
        else {
			<!-- MAIG - CH3 - BEGIN - Modified the setAttribute to removeAttribute -->
            document.getElementById('Buttons1_ContinueButton').removeAttribute('disabled');
			<!-- MAIG - CH3 - END - Modified the setAttribute to removeAttribute -->
            document.getElementById('Buttons1_ContinueButton').title = "";
        }
    }
    //* 67811A0  - PCI Remediation for Payment systems CH3:END Javascipt to handle the tool tip and disable/enable the Continue button when the checkbox is checked.

</script>
