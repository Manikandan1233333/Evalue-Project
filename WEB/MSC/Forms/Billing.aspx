<!--
		* HISTORY
		* MODIFIED BY COGNIZANT AS PART OF .NET MIGRATION CHANGES
		* CHANGES DONE
		* 05/03/2011 RFC#130547 PT_echeck Ch1 Added the reference to the echeck controls added for getting echeck information details by cognizant.
        <%--67811A0 - PCI Remediation for Payment systems CH1: Added controls for Payment Informations-Policy Number,Product Type, First Name, LastName, Due date, Minimum due,Total balance,Other amount--%>
        <%--67811A0 - PCI Remediation for Payment systems CH2: Removed ordersummary so the td/tr used to hold it are commented --%>
        <%--67811A0 - PCI Remediation for Payment systems CH3: Removed Billing Address label --%>
        <%--67811A0 - PCI Remediation for Payment systems CH4: Added Message to display Name on the Card --%>
        <%--67811A0 - PCI Remediation for Payment systems CH5: Added Duplicate payment Checkbox and message panel --%>
        <%--67811A0 - PCI Remediation for Payment systems CH6: Moved Validation Summary from top to bottom --%>
        AZ PAS Conversion and PC integration - Added the vldexcessamount validator for valdating the HL product type
        MAIG - CH1 - Added the Attribute MaintainScrollPositionOnPostback to page Attribute
		MAIG - CH2 - Added the below CSS classes
		MAIG - CH3 - Modified the table width from 720 to 630
		MAIG - CH4 - Modified the table width from 520 to 630
        MAIG - CH5 - Added the logic to display the recurring Enrolled Status for all policies
		MAIG - CH6 - Modidfied the javascript code in OnKeyPress event to support Firefox browser
		MAIG - CH7 - Error Message verbiage is modified
        MAIG - CH8 - Add and Display the additional fields for Down payment flow
		MAIG - CH9 - Modified the table width from 520 to 630, td width and style
		MAIG - CH10 - Modidfied the javascript code in OnKeyPress event to support Firefox browser
        MAIG - CH11 - Commented the Duplicate payment alert validation code
		MAIG - CH12 - Modified the table width from 520 to 630
		MAIG - CH13 - Commented the logic to display only the recurring Enrolled Status
		MAIG - CH14 - Modified the table width from 720 to 630
        MAIG - CH15 - Added the hidden textbox to store the policy details
		MAIG - CH16 - Removed the width attribute
		MAIG - CH17 - Modified the width attribute and added an td
		MAIG - CH18 - Modified the table width from 520 to 630
		MAIG - CH19 - Modified the table width from 520 to 630
		MAIG - CH20 - Modified the table width from 520 to 630
		MAIG - CH21 - Added an below TR to align properly
		MAIG - CH22 - Modified the table width from 520 to 630
		MAIG - CH23 - Added the code to include validation for Email field
		CHG0110069 - CH1 - Added a new BR tag to fix the incident which displays the tag for a specific SIS policy
        CHG0113938 - Display Policy Status information in Billing summary.
        CHG0116140 - Payment Restriction - Added hidden field to store PaymentRestriction data & Added label to show the payment restriction message to user
    CHG0121437 - Jquery to handle the masking of Credit card number.
    CHG0123270 - Jquery to handle the masking of ACH card number.
-->
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<!-- MAIG - CH1 - START - Added the Attribute MaintainScrollPositionOnPostback to page Attribute -->

<%@ Page Language="c#" CodeBehind="Billing.aspx.cs" AutoEventWireup="True" MaintainScrollPositionOnPostback="true" Inherits="MSC.Forms.Billing" %>

<!-- MAIG - CH1 - END - Added the Attribute MaintainScrollPositionOnPostback to page Attribute -->
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<%@ Register TagPrefix="uc" TagName="Name" Src="../Controls/Name.ascx" %>
<%@ Register TagPrefix="uc" TagName="Address" Src="../Controls/Address.ascx" %>
<%@ Register TagPrefix="uc" TagName="PageValidator" Src="../Controls/PageValidator.ascx" %>
<%@ Register TagPrefix="uc" TagName="Buttons" Src="../Controls/Buttons.ascx" %>
<%@ Register TagPrefix="uc" TagName="Summary" Src="../Controls/OrderSummary.ascx" %>
<%@ Register TagPrefix="uc" TagName="PaymentInfo" Src="../Controls/PaymentAccount.ascx" %>
<%@ Register TagPrefix="uc" TagName="echeckInfo" Src="../Controls/echeck.ascx" %>
<%@ Register TagPrefix="uc" TagName="checkInfo" Src="../Controls/Check.ascx" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="UC" TagName="LastName" Src="../Controls/PolicyLastName.ascx" %>

<html>
<head>
    <title>Billing</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta name="CODE_LANGUAGE" content="C#">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
</head>
<!-- MAIG - CH2 - START - Added the below CSS classes -->
<style type="text/css">
    .style2 {
        width: 189px;
        font-family: arial, helvetica, sans-serif;
        color: #000000;
        font-size: 12px;
        font-weight: bold;
        text-decoration: none;
    }

    .style22 {
        width: 200px;
    }
</style>
<!-- MAIG - CH2 - END - Added the below CSS classes -->
<body>
    <!-- Modified the form tag to set autocomplete to off by cognizant on 02/03/2011 -->
    <form id="Billing" method="post" runat="server" autocomplete="off">
        <!-- MAIG - CH3 - START - Modified the table width from 720 to 630 -->
        <table width="630" cellpadding="0" cellspacing="0">
            <!-- MAIG - CH3 - END - Modified the table width from 720 to 630 -->
            <%-- <tr>
            <td>--%>
            <tr>
                <td height="10"></td>
            </tr>
            <tr>
                <td align="left" class="arial_15_bold">Insurance Payment
                </td>
                <td width="30"></td>
            </tr>
            <tr>
                <td height="2"></td>
            </tr>
            <tr>
                <td bgcolor="#999999" height="2" class="style1"></td>
            </tr>
            <tr>
                <td height="8"></td>
            </tr>
            <%--67811A0 - PCI Remediation for Payment systems CH1:START-Added controls for Payment Informations-Policy Number,Product Type, First Name, LastName, Due date, Minimum due,Total balance,Other amount--%>
            <tr>
                <td>
                    <!-- MAIG - CH4 - START - Modified the table width from 520 to 630 -->
                    <table id="tblBilling" width="630" bgcolor="#ffffcc">
                        <!-- MAIG - CH4 - END - Modified the table width from 520 to 630 -->
                        <tr>
                            <td colspan="2" class="arial_11_bold_white" bgcolor="#333333" height="22">&nbsp;&nbsp;Payment Information
                            </td>
                        </tr>
                        <tr>
                            <td height="5"></td>
                        </tr>
                        <tr>
                            <td class="arial_12_bold" width="191">&nbsp;&nbsp;Policy Number<font color="#ff0000">&nbsp;*</font>
                            </td>
                            <td class="arial_12">
                                <asp:Label ID="lblPolicyNumber" runat="server" Width="165" class="border_Non_ReadOnly"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td height="1"></td>
                        </tr>
                        <tr>
                            <td class="arial_12_bold">&nbsp;&nbsp;Product Type<font color="#ff0000">&nbsp;*</font>
                            </td>
                            <td class="arial_12">
                                <asp:Label ID="lblProductType" runat="server" Width="165"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                        </tr>
                        <!-- CHG0113938 - BEGIN - Display Policy Status information in Billing summary.-->
                        <tr id="tdPolicyStatus" runat="server">
                            <td class="arial_12_bold" width="191">&nbsp;&nbsp;Policy Status<font color="#ff0000"></font>
                            </td>
                            <td class="arial_12">
                                <asp:Label ID="lblPolicyStatus" Text="" runat="server" Width="165"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                        </tr>
                        <!-- CHG0113938 - END - Display Policy Status information in Billing summary.-->
                        <!--MAIG - CH5 - BEGIN - Added the logic to display the recurring Enrolled Status for all policies -->
                        <tr>
                            <td class="arial_12_bold" width="191">&nbsp;&nbsp;Recurring Enrolled<font color="#ff0000"></font>
                            </td>
                            <td class="arial_12">
                                <asp:Label ID="lblRecurringEnrolledAll" Text="NO" runat="server" Width="165"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2"></td>
                        </tr>
                        <!--MAIG - CH5 - END - Added the logic to display the recurring Enrolled Status for all policies -->
                        <tr>
                            <CSAA:Validator ID="vldFirstName" runat="server" ControlToValidate="txtFirstName">
				                   &nbsp;&nbsp;First Name</CSAA:Validator>
                            <td class="arial_12">
                                <!-- MAIG - CH6 - BEGIN - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                                <asp:TextBox ID="txtFirstName" runat="server" Width="165" MaxLength="25" onkeypress="return alphabetsonly_onkeypress(event);" EnableViewState="true" class="border_Non_ReadOnly"></asp:TextBox>
                                <!-- MAIG - CH6 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                            </td>
                            <CSAA:Validator ID="vldFirstNameReq" runat="server"
                                OnServerValidate="ReqValCheck"></CSAA:Validator>
                            <!-- MAIG - CH7 - BEGIN - Error Message verbiage is modified-->
                            <CSAA:Validator ID="vldFirstNameAlpha" runat="server" Display="None" OnServerValidate="CheckFirstName"
                                ErrorMessage="Invalid First Name"></CSAA:Validator>
                            <!-- MAIG - CH7 - END - Error Message verbiage is modified-->
                            <%--<CSAA:Validator ID="vldName" runat="server" Display="None" OnServerValidate="CheckLength"
                                    ErrorMessage="The name is too long.It can accommodate a maximum of 25 characters total for the name including spaces between names."></CSAA:Validator>--%>
                        </tr>
                        <UC:LastName ID="txtLastName" runat="Server" Required="True"></UC:LastName>
                        <!-- MAIG - CH8 - BEGIN - Add and Display the additional fields for Down payment flow-->
                        <asp:Panel ID="pnlDownPayment" Visible="false" runat="server">
                            <tr>
                                <CSAA:Validator ID="vldMailingZip" runat="server" ControlToValidate="txtMailingZip">
				                   &nbsp;&nbsp;Mailing Zip</CSAA:Validator>
                                <td class="arial_12">
                                    <asp:TextBox ID="txtMailingZip" runat="server" Width="165" MaxLength="5" EnableViewState="true" onkeypress="return digits_only_onkeypress(event)" class="border_Non_ReadOnly"></asp:TextBox>
                                </td>
                                <CSAA:Validator ID="vldMailingZipReq" runat="server"
                                    OnServerValidate="ReqValZipCheck"></CSAA:Validator>
                            </tr>
                        </asp:Panel>
                        <tr>
                            <CSAA:Validator ID="vldEmailAddress" runat="server" DefaultAction="Succeed" ControlToValidate="txtEmailAddress">
				                   &nbsp;&nbsp;Email Address</CSAA:Validator>
                            <td class="arial_12">
                                <!-- MAIG - CH23 - BEGIN - Added the code to include validation for Email field -->
                                <asp:TextBox ID="txtEmailAddress" runat="server" Width="165" onkeypress="return email_Address_onkeypress(event)" MaxLength="50" EnableViewState="true" class="border_Non_ReadOnly"></asp:TextBox>
                                <!-- MAIG - CH23 - END - Added the code to include validation for Email field -->
                            </td>
                            <CSAA:Validator ID="vldEmailAddressFormat" runat="server" ControlToValidate="txtEmailAddress" ErrorMessage="Invalid Email Address." Display="None"
                                OnServerValidate="ReqValEmailCheck"></CSAA:Validator>
                        </tr>
                        <!-- MAIG - CH8 - END - Add and Display the additional fields for Down payment flow-->

                        <tr>
                            <td class="arial_12_bold">&nbsp;&nbsp;Due Date
                            </td>
                            <td class="arial_12">
                                <asp:Label ID="lblDuedate" runat="server" Width="165"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td class="arial_12_bold">
                                <asp:RadioButton ID="rbnMinimumdue" runat="server" GroupName="Amount"
                                    OnClick='javascript:validateamountchecked();'></asp:RadioButton>
                                Minimum Due<font color="#ff0000">&nbsp;*</font>
                            </td>
                            <td class="arial_12">
                                <asp:Label ID="lblMinimumdue" runat="server" Width="165" EnableViewState="true"></asp:Label>
                            </td>
                            <CSAA:Validator ID="vldminimuamount" runat="server" ErrorMessage="Minimum Due amount is less than or equal to zero"
                                OnServerValidate="vldminimumdue" Display="None"></CSAA:Validator>
                        </tr>
                        <tr>
                            <td class="arial_12_bold">
                                <asp:RadioButton ID="rbnTotalbalance" runat="server" GroupName="Amount" OnClick='javascript:validateamountchecked();'></asp:RadioButton>
                                Total Balance<font color="#ff0000">&nbsp;*</font>
                            </td>
                            <td class="arial_12">
                                <asp:Label ID="lblTotalbalance" runat="server" Width="165" EnableViewState="true"></asp:Label>
                            </td>
                            <CSAA:Validator ID="vldTotalbalanceamount" runat="server" ErrorMessage="Total Balance amount is less than or equal to zero"
                                OnServerValidate="VldTotalbalance" Display="None"></CSAA:Validator>
                        </tr>
                    </table>
                    <!-- MAIG - CH9 - BEGIN - Modified the table width from 520 to 630, td width and style-->
                    <table width="630" bgcolor="#ffffcc">
                        <tr>
                            <td class="style2" style="width: 20px;">
                                <asp:RadioButton ID="rbnOtherAmount" runat="server" GroupName="Amount" OnClick='javascript:validateamountchecked();'></asp:RadioButton>
                            </td>
                            <CSAA:Validator ID="VldAmount" runat="server" ControlToValidate="txtAmount" DefaultAction="Succeed">Other Amount&nbsp;<font color="#ff0000">*</font></CSAA:Validator>
                            <td style="padding-right: 60px;">
                                <!-- MAIG - CH9 - END - Modified the table width from 520 to 630, td width and style-->
                                <!-- MAIG - CH10 - BEGIN - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                                <asp:TextBox ID="txtAmount" Columns="28" Width="165" onkeypress="return decimalnumeric_only_onkeypress(event)" runat="server"
                                    MaxLength="7" Onchange='javascript:CheckAmount();' />
                                <!-- MAIG - CH10 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                            </td>

                            <CSAA:Validator ID="vldAmountDecimal" runat="server" OnServerValidate="WholeCents" Display="None" ErrorMessage="Payment Other Amount field must be a valid amount."></CSAA:Validator>
                            <CSAA:Validator ID="vldAmountPositive" runat="server" ErrorMessage="Other Amount field must be positive."
                                OnServerValidate="PositiveAmount" Display="None"></CSAA:Validator>
                            <CSAA:Validator ID="ValidOtherAmount" runat="server"
                                OnServerValidate="ReqValCheckAmount" Display="None"></CSAA:Validator>
                            <!-- MAIG - BEGIN - CH11 - Commented the Duplicate payment alert validation code -->

                            <!-- MAIG - END - CH11 - Commented the Duplicate payment alert validation code -->
                            <CSAA:Validator ID="vldExcessAmount" runat="server" Display="None" ErrorMessage="The amount entered must be equal to the total due amount available for this policy."
                                DefaultAction="Succeed" OnServerValidate="ReqValCheckAmount"></CSAA:Validator>
                        </tr>
                    </table>
                    <!-- MAIG - CH12 - START - Modified the table width from 720 to 630 -->
                    <table width="630" bgcolor="#ffffcc">
                        <!-- MAIG - CH12 - END - Modified the table width from 720 to 630 -->
                        <tr>
                            <td colspan="3" height="5">
                                <asp:TextBox ID="hdntxtdupPolicy" Visible="False" runat="server" EnableViewState="true"></asp:TextBox>
                                <asp:TextBox ID="hdntxtProductCode" Visible="False" runat="server" EnableViewState="true"></asp:TextBox>
                                <asp:TextBox ID="hdntxtConvertedPolicy" Visible="False" runat="server" EnableViewState="true"></asp:TextBox>
                                <asp:TextBox ID="hdnBillPlan" Visible="False" runat="server" EnableViewState="true"></asp:TextBox>
                                <asp:TextBox ID="hdnPaymentPlan" Visible="False" runat="server" EnableViewState="true"></asp:TextBox>
                                <asp:TextBox ID="hdntxtProductType" Visible="False" runat="server" EnableViewState="true"></asp:TextBox>
                                <asp:TextBox ID="hdnAutoPay" Visible="False" runat="server" EnableViewState="true"></asp:TextBox>
                                <!--Payment Restriction - BEGIN - CHG0116140 - Added hidden field to store PaymentRestriction data-->
                                <asp:TextBox ID="hdnPayFlag" Visible="False" runat="server" EnableViewState="true"></asp:TextBox>
                                <!--Payment Restriction - END - CHG0116140 - Added hidden field to store PaymentRestriction data-->
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--67811A0 - PCI Remediation for Payment systems CH1:END	--%>
            <%--67811A0 - PCI Remediation for Payment systems CH2: START Removed ordersummary so the td/tr used to hold it are commented --%>
            <%--<tr>					
							
								<td colspan="2" height="100">--%>
            <UC:summary runat="server" title="Order Summary" id="Summary" />
            <%--</td>
								<td width="30"></td>--%>
            <%--</tr>--%>
            <%--<tr>
								<td colspan="3" height="10">
								</td>
							</tr>--%>
            <%--67811A0 - PCI Remediation for Payment systems CH2: END Removed ordersummary so the td/tr used to hold it are commented --%>
            <!-- step 2 table contains 3 tables: space:30, form table:520, space:200 -->
            <!--START Changed by Cognizant on 05/18/2004 for creating a new table which holds the PaymentTypeCombo and Validator-->
            <!--MAIG - CH13 - BEGIN - Commented the logic to display only the recurring Enrolled Status -->
            <!-- <asp:PlaceHolder ID="ManageRecurring" runat ="server" Visible ="false">
                <tr>
                    <td>
                        <table width="520" bgcolor="#ffffcc">
                           
                             <tr>
                               <td class="arial_11_bold_white" bgcolor="#333333" height="22" colspan="2" >
                                &nbsp;&nbsp;Recurring Information
                                </td>
                            </tr>
                            <tr>   
                                <td class="arial_12_bold"  style ="width:350px">
                              <font color="#FF0000"> This policy is already enrolled for recurring payment</font>
                                </td>
                               
                            </tr>
                            <tr>
                                <td height="5">
                                </td>
                            </tr> 
                            <tr>   
                                <td class="arial_12_bold" width ="191">
                                &nbsp;&nbsp;Recurring Enrolled<font color="#ff0000"></font>
                                </td>
                                <td class="arial_12">
                                    <asp:Label ID="lblRecurringEnrolled" runat="server" Width="165"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td height="5">
                                </td>
                            </tr> 
                            <tr>   
                                <td class="arial_12_bold" width ="191">
                                &nbsp;&nbsp;Bill Plan<font color="#ff0000"></font>
                                </td>
                                <td class="arial_12">
                                    <asp:Label ID="lblBillPlan" runat="server" Width="165"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td bgcolor="#cccccc" colspan="2" height="22">
                                    <asp:imagebutton id="imgManageRecurring" runat="server" width="130" alt="ManageRecurring" border="0" align="right" height="20" imageurl="/PaymentToolimages/btn_ManageRecurring.gif" onclick="imgManageRecurring_Click"></asp:imagebutton>
                                </td>
                            </tr>                           
                        </table>
		            </td>
                </tr>
                </asp:PlaceHolder>  -->
            <!--MAIG - CH13 - END - Commented the logic to display only the recurring Enrolled Status -->
            <!--Payment Restriction - BEGIN - CHG0116140 - Added label to show the payment restriction message to user-->
            <tr>
                <td>
                    <asp:Label ID="lblPaymentRestrict" runat="server" Text="" CssClass="arial_12_bold_red"
                        Visible="false"></asp:Label>
                </td>
            </tr>
            <!--Payment Restriction - BEGIN - CHG0116140 - Added label to show the payment restriction message to user-->
            <tr>
                <td>
                    <!-- MAIG - CH14 - START - Modified the table width from 720 to 630 -->
                    <table width="630" bgcolor="#ffffcc">
                        <!-- MAIG - CH14 - END - Modified the table width from 720 to 630 -->
                        <tr>
                            <td class="arial_11_bold_white" bgcolor="#333333" height="22" colspan="2">&nbsp;&nbsp;Payment Method
                            </td>
                        </tr>
                        <tr>
                            <td height="5"></td>
                        </tr>
                        <!--PUP Ch1: Added the lable to display the warning message for displaying credit card payment type while processing more than one line item which containd atleast one PUP policy by cognizant on 01/13/2011-->
                        <tr>
                            <td bgcolor="#ffffcc" colspan="2">
                                <asp:Label ID="lblWarning" Visible="False" runat="server" CssClass="arial_11_red"></asp:Label>
                                <%--<asp:Label ID="lblPupWarning" Visible="False" runat="server" Text="Payment method is restricted to Credit Card and ECheck for Personal Umbrella Policy Products."
                                    CssClass="arial_11_red"></asp:Label>--%>
                                <%--<asp:Label ID="lblWuwarning" Visible="False" runat="server" Text="Payment method is restricted to Credit Card and ECheck for Western United Products."
                                    CssClass="arial_11_red"></asp:Label> --%>
                            </td>
                        </tr>
                        <tr>
                            <td height="5"></td>
                            <asp:TextBox ID="HidddenStatePrefix" Visible="False" runat="server" MaxLength="40"></asp:TextBox>
                            <!-- MAIG - CH15 - BEGIN - Added the hidden textbox to store the policy details  -->
                            <asp:TextBox ID="HiddenPolicyDetail" Visible="false" runat="server" MaxLength="40"></asp:TextBox>
                            <asp:TextBox ID="HiddenRpbsBillingDetails" Visible="false" runat="server"></asp:TextBox>
                            <asp:TextBox ID="HiddenNameSearchDetails" Visible="false" runat="server"></asp:TextBox>
                            <asp:TextBox ID="HiddenDuplicatePolicyDetails" Visible="false" runat="server"></asp:TextBox>
                            <!-- MAIG - CH15 - END - Added the hidden textbox to store the policy details  -->
                        </tr>
                        <tr>
                            <CSAA:Validator ID="vldPaymentType" runat="server" ControlToValidate="_PaymentType"
                                OnServerValidate="ReqValPaymentTypeCheck">&nbsp;&nbsp;&nbsp;Select a Payment Method</CSAA:Validator>
                            <!-- MAIG - CH16 - BEGIN - Removed the width attribute  -->
                            <td>
                                <!-- MAIG - CH16 - END - Removed the width attribute  -->
                                &nbsp;&nbsp; &nbsp;&nbsp;<asp:DropDownList ID="_PaymentType" Width="170" runat="server" DataTextField="Description"
                                    DataValueField="ID" AutoPostBack="true" OnSelectedIndexChanged="_PaymentType_SelectedIndexChanged" />



                            </td>
                        </tr>
                        <tr>
                            <!-- MAIG - CH17 - BEGIN - Modified the width attribute and added an td  -->
                            <td width="168" height="10"></td>
                            <td></td>
                            <!-- MAIG - CH17 - END - Modified the width attribute and added an td -->
                        </tr>
                    </table>
                </td>
            </tr>
            <!--START-->
            <tr>
                <!-- space to left of form table-->
                <!--<td width="30">
									<table cellpadding="0" cellspacing="0" border="0" >
										<tr>
											<td width="30">
											</td>
										</tr
									</table>
								</td>-->
                <!-- form table -->
                <!-- MAIG - CH18 - START - Modified the table width from 520 to 630 -->
                <td width="630">
                    <!-- MAIG - CH18 - END - Modified the table width from 520 to 630 -->
                    <UC:paymentinfo runat="server" id="Card" />
                    <!-- 05/03/2011 RFC#130547 PT_echeck Ch1 Added the reference to the echeck controls added for getting echeck information details by cognizant.-->
                    <UC:echeckInfo runat="Server" id="echeck" />

                    <UC:checkInfo runat="server" id="Check" />

                    <!-- MAIG - CH19 - START - Modified the table width from 520 to 630 -->
                    <table width="630" cellpadding="0" cellspacing="0" bgcolor="#ffffcc">
                        <!--START Changed by Cognizant on 05/18/2004 for creating a new table which holds the Billing Address label-->
                        <tr>
                            <td>
                                <table runat="server" id="trBilling" width="630">
                                    <!--End-->
                                    <!-- MAIG - CH19 - END - Modified the table width from 520 to 630 -->
                                    <tr>
                                        <td height="10"></td>
                                    </tr>
                                    <%--67811A0 - PCI Remediation for Payment systems CH3:START Removed Billing Address label --%>
                                    <%--<tr>
														<td class="arial_11_bold_white" bgcolor="#333333" height="22">
															&nbsp;&nbsp;Billing Address</td>
													</tr>--%>
                                    <%--<tr>
														<td height="5"></td>
													</tr>--%>
                                    <%--67811A0 - PCI Remediation for Payment systems CH3:END Removed Billing Address label --%>
                                    <tr>
                                        <%--67811A0 - PCI Remediation for Payment systems CH4:START Added Message to display Name on the Card --%>
                                        <%--<td height="10" class="arial_11">&nbsp;&nbsp;&nbsp;The information below must match 
															the information on the new Member's credit card statement.</td>--%>
                                        <td class="arial_12_bold">&nbsp;&nbsp;<asp:Label ID="lblName" runat="server" Width="150" class="arial_12_bold"></asp:Label>
                                        </td>
                                        <%--67811A0 - PCI Remediation for Payment systems CH4:END Added Message to display Name on the Card --%>
                                    </tr>
                                    <tr>
                                        <td height="10"></td>
                                    </tr>
                                    <!--START Changed by Cognizant on 05/18/2004 for creating a new table which holds the Billing Address label-->
                                </table>
                            </td>
                        </tr>
                        <!--END-->
                        <tr>
                            <td style="padding-left: 8px">
                                <UC:name id="BillToName" runat="server" required="true" />
                            </td>
                        </tr>
                        <UC:address runat="server" id="BillToAddress" billing="true" />
                        <tr>
                            <td height="10"></td>
                        </tr>
                        <tr>
                            <td bgcolor="#999999" height="2" class="style1"></td>
                        </tr>
                        <tr>
                            <td height="10"></td>
                        </tr>
                        <%--67811A0 - PCI Remediation for Payment systems CH5:START Added Duplicate payment Checkbox and message panel --%>
                        <tr>
                            <td class="arial_11_bold">
                                <asp:Image ID="imgDuplicatePymt" runat="server" Visible="False" ImageUrl="../images/red_arrow.gif" />&nbsp;<asp:Label
                                    ID="lbldupMessage" runat="server" Visible="False"><font color="red">Alert : A payment in the amount above has been received today.Only proceed if you would like to make another payment for the same amount.</font></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <%--<td></td>--%>
                            <td class="arial_11_bold" valign="top">
                                <asp:CheckBox ID="chkDuplicate" onclick="javascript:checkUserAcknowledgment()" Text="Yes, I want to continue. " runat="server" Visible="False"
                                    AutoPostBack="False" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <!-- MAIG - CH20 - START - Modified the table width from 520 to 630 -->
                                <table width="630" bgcolor="#ffffcc">
                                    <!-- MAIG - CH20 - END - Modified the table width from 520 to 630 -->
                                    <%--67811A0 - PCI Remediation for Payment systems CH6:START Moved Validation Summary from top to bottom --%>
                                    <tr height="10">
                                        <td>
                                            <UC:pagevalidator runat="server" colspan="3" id="vldSumaary" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <%--67811A0 - PCI Remediation for Payment systems CH6:END Moved Validation Summary from top to bottom --%>
                        <%--67811A0 - PCI Remediation for Payment systems CH5:END Added Duplicate payment Checkbox and message panel --%>
                        <!-- MAIG - CH21 - START - Added an below TR to align properly-->
                        <tr>
                            <td align="right" style="width: auto">
                                <UC:buttons runat="server" id="btncontrol" />
                            </td>
                        </tr>
                        <!-- MAIG - CH21 - END - Added an below TR to align properly-->
                    </table>
                    <!-- MAIG - CH22 - START - Modified the table width from 520 to 630 -->
                    <table width="630" cellpadding="0" cellspacing="0" bgcolor="#ffffcc">
                        <!-- MAIG - CH22 - END - Modified the table width from 520 to 630 -->
                        <%--<tr><td width="30"></td>--%>
                        <%--</tr>--%>
                    </table>
                </td>
                <!-- space to right of form table -->


                <!-- end of step 2 table -->
            </tr>
            <%--</td>
        </tr>--%>
        </table>
    </form>
    <%--CHG0110069 - CH1 - BEGIN - Added a new BR tag to fix the incident which displays the tag for a specific SIS policy--%>
    <br />
    <%--CHG0110069 - CH1 - END - Added a new BR tag to fix the incident which displays the tag for a specific SIS policy--%>

    <%--CHG0116140 - CH1 - BEGIN - Added a new BR tag to fix body tag issue which is displayed for a specific SIS policy--%>
    <br />
    <%--CHG0116140 - CH1 - END - Added a new BR tag to fix body tag issue which is displayed for a specific SIS policy--%>
</body>
</html>
<%--CHG0121437 - Jquery events to handle the masking of Credit card number - Adding the JS reference--%>
<script  language="javascript" type="text/javascript" src="../Scripts/CCMasking.js" > </script>
<%--CHG0123270 - Jquery events to handle the masking of ACH card number - Adding the JS reference--%>
<script  language="javascript" type="text/javascript" src="../Scripts/ACHMasking.js" > </script>
<script language="javascript"  type="text/javascript" >

    /*   
    Name : validateamountchecked() 
    Description: On click of other radiobutton disables the otheramount testbox. 
    */

    function validateamountchecked() {
        if (document.getElementById('rbnMinimumdue').checked == true) {
            document.getElementById('txtAmount').disabled = true;
            document.getElementById('txtAmount').value = '';
        }
        else if (document.getElementById('rbnTotalbalance').checked == true) {
            document.getElementById('txtAmount').disabled = true;
            document.getElementById('txtAmount').value = '';
        }
        else if (document.getElementById('rbnOtherAmount').checked == true) {
            document.getElementById('txtAmount').disabled = false;
        }
    }

    /*   
    Name : CheckAmount() 
    Description: overpayment, underpayment and Zeropayment validations. 
    */

    function CheckAmount() {
        var strAmount;
        var strminAmount;
        var strTotalbalance;

        strAmount = document.getElementById('txtAmount').value;
        strminAmount = document.getElementById('lblMinimumdue').innerText;
        strTotalbalance = document.getElementById('lblTotalbalance').innerText;


        strminAmount = strminAmount.replace('$', '');
        strTotalbalance = strTotalbalance.replace('$', '');
        strTotalbalance = parseInt(strTotalbalance);
        strminAmount = parseInt(strminAmount);
        strAmount = parseInt(strAmount);


        if (strAmount < strminAmount) {
            strReturn = confirm('Payment amount keyed is not equal to amount billed.');
            if (strReturn) {
                setTimeout(function () { document.getElementById('_PaymentType').focus(); }, 10);
            }
            else {
                setTimeout(function () { document.getElementById('txtAmount').focus(); }, 10);
            }
            return;
        }

        if (strAmount > strTotalbalance) {
            strReturn = confirm('Payment amount keyed is not equal to amount billed.');
            if (strReturn) {
                setTimeout(function () { document.getElementById('_PaymentType').focus(); }, 10);
            }
            else {
                setTimeout(function () { document.getElementById('txtAmount').focus(); }, 10);
            }
            return;
        }

        if (strTotalbalance == 0 || strminAmount == 0) {
            strReturn = confirm('Payment amount keyed is not equal to amount billed.');
            if (strReturn) {
                setTimeout(function () { document.getElementById('_PaymentType').focus(); }, 10);
            }
            else {
                setTimeout(function () { document.getElementById('txtAmount').focus(); }, 10);
            }
            return;
        }

    }

    

    function checkUserAcknowledgment() {
        var chkbox = document.getElementById('chkDuplicate').checked;
        if (!chkbox)
            document.getElementById('btncontrol_ContinueButton').setAttribute('disabled', true);
        else
            document.getElementById('btncontrol_ContinueButton').setAttribute('disabled', false);
    }


</script>

