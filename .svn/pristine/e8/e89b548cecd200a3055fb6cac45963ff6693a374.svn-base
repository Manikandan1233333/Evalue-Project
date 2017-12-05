<!-- 
MAIG - CH1 - Added the Attribute MaintainScrollPositionOnPostback to page Attribute
MAIG - CH2 - Add the Name Search control to handle Multi value from RPBS Service and Last name control for invalid policy flow
MAIG - CH3 - Added jquery to make the Invalid flow text to black color
MAIG - CH4 - Modified the Width from 270 to 200
MAIG - CH5 - Modidfied the javascript code in OnKeyPress event to support Firefox browser
MAIG - CH6 - Added code to invoke the Name Search User Control
MAIG - CH7 - Add and Display the additional fields for Invalid Policy Enrollment flow
MAIG - CH7 - Added code to align properly that affects IE 11 alone
CHG0112662 - Added a new BR tag to fix the body tag issue for a specific SIS policy
JQuery Upgrade Changes- 05/29/2017
-->
<!--MAIG - CH1 - BEGIN - Added the Attribute MaintainScrollPositionOnPostback to page Attribute -->
 <%--CHG0121437 - Jquery to handle the masking of Credit card number--%>
<%@ Page Language="c#" CodeBehind="ManageEnrollment.aspx.cs" MaintainScrollPositionOnPostback="true" AutoEventWireup="True"
    Inherits="MSC.Forms.ManageEnrollment" EnableSessionState="true" %>

<!--MAIG - CH1 - END - Added the Attribute MaintainScrollPositionOnPostback to page Attribute -->
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%@ Register TagPrefix="uc" TagName="ECheckEnroll" Src="../Controls/eCheckEnrollment.ascx" %>
<%@ Register TagPrefix="uc" TagName="CreditCardEnroll" Src="../Controls/CreditCardEnrollment.ascx" %>
<%@ Register TagPrefix="uc" TagName="PageValidator" Src="../Controls/PageValidator.ascx" %>
<%@ Register TagPrefix="uc" TagName="UnEnrollCC" Src="../Controls/UnEnrollRecurringCC.ascx" %>
<%@ Register TagPrefix="uc" TagName="UnEnrollECheck" Src="../Controls/UnEnrollRecurringECheck.ascx" %>
<%@ Register TagPrefix="uc" TagName="PaymentMethod" Src="../Controls/PaymentMethod.ascx" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="uc" TagName="EditCC" Src="../Controls/EditCardDetails.ascx" %>
<!-- MAIG - CH2 - BEGIN - Add the Name Search control to handle Multi value from RPBS Service and Last name control for invalid policy flow-->
<%@ Register TagPrefix="NS" TagName="NameSearch" Src="~/Controls/NameSearch.ascx" %>
<%@ Register TagPrefix="UC" TagName="LastName" Src="../Controls/PolicyLastName.ascx" %>
<!-- MAIG - CH2 - END - Add the Name Search control to handle Multi value from RPBS Service and Last name control for invalid policy flow-->
<html>
<head>
    <title>ManageEnrollment</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0" />
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <style type="text/css">
        .arial_12_bold_red {
            font-family: arial, helvetica, sans-serif;
            color: #ff0000;
            font-size: 12px;
            font-weight: bold;
            text-decoration: none;
        }
        /*MAIG - CH7 - BEGIN - Added code to align properly that affects IE 11 alone */
        @media screen and (min-width:0\0) {
            #Panel1 {
                padding-top: 20px;
            }
        }
        /*MAIG - CH7 - END - Added code to align properly that affects IE 11 alone */
    </style>
    <!-- MAIG - CH3 - BEGIN - Added jquery to make the Invalid flow text to black color-->
<%--CHGxxxx- JQuery Upgrade Changes- 05/29/2017- start--%>
    <script  language="javascript" type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/3.2.1/jquery.min.js">
    </script>
<%--CHGxxxx- JQuery Upgrade Changes- 05/29/2017- end--%>
    <%--CHG0121437 - Jquery events to handle the masking of Credit card number - Adding the JS reference--%>
    <script  language="javascript" type="text/javascript" src="../Scripts/CCMasking.js" > 
    </script>
    <%--CHG0123270 - Jquery events to handle the masking of ACH card number - Adding the JS reference--%>
     <script  language="javascript" type="text/javascript" src="../Scripts/ACHMasking.js" > 
    </script>
    <script language="javascript" type="text/javascript">
        window.history.forward(1);
        function AnotherFunction() {
            $("#tblInvalidFlow tbody tr td:contains('First Name'):first").each(function () {
                $(this).css("color", "black");
            });
            $("#tblInvalidFlow tbody tr td:contains('Last Name'):first").each(function () {
                $(this).css("color", "black");
            });
            $("#tblInvalidFlow tbody tr td:contains('Mailing Zip'):first").each(function () {
                $(this).css("color", "black");
            });
        }
      
    </script>
    <!-- MAIG - CH3 - END - Added jquery to make the Invalid flow text to black color-->
</head>
<body>
    <form id="ManageEnrollment" method="post" runat="server" autocomplete="off">
        <%--Heading--%>
        <asp:Panel ID="Panel1" runat="server" DefaultButton="btnSearch">
            <table width="620" cellpadding="0" cellspacing="0">
                <tr>
                    <td colspan="2" align="left" class="arial_15_bold">Manage Recurring
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#999999" height="2" class="style1"></td>
                </tr>
                <tr>
                    <td>
                        <%--Policy Search start --%>
                        <table width="620" bgcolor="#ffffcc" align="center">
                            <tr>
                                <td colspan="2" class="arial_11_bold_white" bgcolor="#333333" height="22">&nbsp;Enrollment Search
                                </td>
                            </tr>
                            <tr>
                                <td height="5"></td>
                            </tr>
                            <caption>
                                <br />
                                <tr>
                                    <td class="arial_12_bold" width="191">
                                        <%-- &nbsp;&nbsp;Product Type<font color="#ff0000">&nbsp;*</font>--%>
                                        <CSAA:Validator ID="vldProductType" runat="server" ControlToValidate="_ProductType"
                                            ErrorMessage=" " Tag="span">&nbsp;&nbsp;Product Type</CSAA:Validator>
                                    </td>
                                    <td class="arial_12">
                                        <!-- MAIG - CH4 - BEGIN - Modified the Width from 270 to 200 -->
                                        <asp:DropDownList ID="_ProductType" runat="server" AutoPostBack="true" DataTextField="Description"
                                            DataValueField="ID" EnableViewState="true" OnSelectedIndexChanged="_ProductType_SelectedIndexChanged"
                                            TabIndex="1" Width="200">
                                        </asp:DropDownList>
                                        <!-- MAIG - CH4 - END - Modified the Width from 270 to 200 -->
                                    </td>
                                    <CSAA:Validator ID="PolicyReqValidator" runat="server" ControlToValidate="_ProductType"
                                        DefaultAction="Required" ErrorMessage="Please enter policy number " OnServerValidate="ReqValCheck1"></CSAA:Validator>
                                </tr>
                            </caption>
                            <tr>
                                <td class="arial_12_bold" width="191">
                                    <%--&nbsp;&nbsp;Policy Number<font color="#ff0000">&nbsp;*</font>--%>
                                    <CSAA:Validator ID="vldPolicyNumber" runat="server" ControlToValidate="_PolicyNumber"
                                        ErrorMessage=" " Tag="span">&nbsp;&nbsp;Policy Number</CSAA:Validator>
                                    <CSAA:Validator ID="vldPolicyNumberLength" runat="server" ControlToValidate="_PolicyNumber"
                                        Display="None" ErrorMessage="Policy Number should be minimum 7 Alpha-Numeric characters."
                                        OnServerValidate="ValidPolciyNumberLength"></CSAA:Validator>
                                </td>
                                <td class="arial_12">
                                    <!-- MAIG - CH5 - BEGIN - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                                    <asp:TextBox ID="_PolicyNumber" runat="server" onkeypress="return alphanumeric_onkeypress(event)" class="border_Non_ReadOnly" EnableViewState="true"
                                        MaxLength="13" TabIndex="2" Width="200px"></asp:TextBox>
                                    
                                    <!-- MAIG - CH5 - END - Modidfied the javascript code in OnKeyPress event to support Firefox browser  -->
                                </td>
                                
                                <td class="arial_12">
                                    <asp:TextBox ID="hdnBillingPlan" runat="server" Enabled="false" EnableViewState="false"
                                        MaxLength="25" Visible="false" Width="267px"></asp:TextBox>
                                </td>
                                <!-- MAIG - CH6 - BEGIN - Added code to invoke the Name Search User Control -->
                            </tr>
                            <tr class="arial_12">
                                <td>
                                    <NS:NameSearch ID="_NameSearch" runat="server" Visible="true" />
                                </td>
                                <td>
                                    <asp:TextBox ID="HiddenSelectedDuplicatePolicy" Visible="false" runat="server" Text="" EnableViewState="true"></asp:TextBox>
                                </td>
                            </tr>
                            <!-- MAIG - CH6 - END - Added code to invoke the Name Search User Control -->
                            <tr>
                                <td bgcolor="#cccccc" colspan="2" height="22">
                                    <asp:ImageButton ID="btnSearch" runat="server" align="right" alt="search" border="0"
                                        Height="17" ImageUrl="/PaymentToolimages/btn_search.gif" OnClick="btnSearch_Click"
                                        TabIndex="3" Width="68" />
                                    <a href="ManageEnrollment.aspx">
                                        <img align="right" alt="Clear" border="0" height="17" src="../images/btn_cancel.gif"
                                            width="51" /></a>
                                </td>
                            </tr>
                        </table>
        </asp:Panel>
        <!-- MAIG - CH7 - BEGIN - Add and Display the additional fields for Invalid Policy Enrollment flow-->
        <asp:Label ID="lblErrorMsg" runat="server" Text="" CssClass="arial_12_bold_red" Visible="false"></asp:Label>
        <%--<input type="hidden" id="mngHidnValue" runat="server" />--%>
        <asp:PlaceHolder ID="invalidPolicyDetails" runat="server" Visible="false" EnableViewState="true">
            <table>
                <tr>
                    <td></td>
                </tr>
                <tr>
                    <td>
                        <table id="tblInvalidFlow" width="620" bgcolor="#ffffcc" align="center">
                            <tr>
                                <td colspan="2" class="arial_11_bold_white" bgcolor="#333333" height="22">&nbsp;Insured Details
                                </td>
                            </tr>
                            <tr>
                                <td height="4"></td>
                            </tr>
                            <tr>
                                <CSAA:Validator ID="vldFirstName" runat="server" ControlToValidate="txtFirstName">
				                   &nbsp;&nbsp;First Name</CSAA:Validator>
                                <td class="arial_12">
                                    <asp:TextBox ID="txtFirstName" runat="server" Width="165" onkeypress="return alphabetsonly_onkeypress(event);" MaxLength="25" EnableViewState="true"
                                        class="border_Non_ReadOnly"></asp:TextBox>
                                </td>
                                <CSAA:Validator ID="vldFirstNameReq" runat="server"
                                    OnServerValidate="CheckFirstName"></CSAA:Validator>
                            </tr>
                            <UC:LastName ID="txtLastName" runat="Server" Required="True" />

                            <tr>
                                <CSAA:Validator ID="vldMailingZip" runat="server" ControlToValidate="txtMailingZip">
				                   &nbsp;&nbsp;Mailing Zip</CSAA:Validator>
                                <td class="arial_12">
                                    <asp:TextBox ID="txtMailingZip" runat="server" Width="165" MaxLength="5" EnableViewState="true" onkeypress="return digits_only_onkeypress(event)"
                                        class="border_Non_ReadOnly"></asp:TextBox>
                                </td>
                                <CSAA:Validator ID="vldMailingZipLengthCheck" runat="server"
                                    OnServerValidate="ReqValZipCheck"></CSAA:Validator>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
        <!-- MAIG - CH7 - END - Add and Display the additional fields for Invalid Policy Enrollment flow-->
        <%--Policy Search end --%>
        <asp:PlaceHolder ID="policySearchDetails" runat="server" Visible="false" EnableViewState="true">
            <br />
            <table width="620" cellpadding="0" cellspacing="0" border="3" bordercolor="#999999"
                align="center">
                <tr>
                    <td colspan="2" class="arial_11_bold_white" bgcolor="#CCCCCC" height="22">
                        <font color="black">
                            <asp:Label ID="lbl_PolicySearch_Details" runat="server" Text="Policy Billing Details"></asp:Label></font>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" height="10">
                        <asp:GridView ID="gridEnrollment" runat="server" Width="620px" CssClass="arial_12"
                            AlternatingRowStyle-HorizontalAlign="Left" AutoGenerateColumns="True" CellPadding="2"
                            PageSize="20" Visible="true" BorderStyle="None" GridLines="None" CaptionAlign="Left"
                            ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" FooterStyle-BorderWidth="1"
                            FooterStyle-CssClass="border_Top_Down" HeaderStyle-HorizontalAlign="Left">
                            <HeaderStyle Height="10px" BorderWidth="0px" BorderStyle="Solid" BorderColor="#CCCCCC"
                                CssClass="arial_12_bold" BackColor="#FFFFFF" />
                            <FooterStyle BorderStyle="Double" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="FFFFFF" height="15"></td>
                </tr>
            </table>
            <table>
                <tr>
                    <td bgcolor="FFFFFF" height="10"></td>
                </tr>
                <tr>
                    <td bgcolor="FFFFFF"></td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="lblPaymentRestrict" runat="server" Text="" CssClass="arial_12_bold_red"
                            Visible="false"></asp:Label>
                    </td>
                </tr>
            </table>
        </asp:PlaceHolder>
        <UC:PaymentMethod runat="server" Visible="false" ID="PaymentMethod" OndropDownChanged="ManageEnrollment_SelectedIndexChanged" />
        <UC:ECheckEnroll runat="Server" Visible="false" ID="EnrollECheck" />
        <UC:CreditCardEnroll runat="Server" Visible="false" ID="EnrollCC" />
        <UC:UnEnrollCC runat="Server" Visible="false" ID="UnEnrollCC" />
        <UC:UnEnrollECheck runat="Server" Visible="false" ID="UnEnrollECheck" />
        <UC:EditCC runat="server" Visible="false" ID="EditCC" Required="true" />
        </td>
    <td>
        <UC:PageValidator runat="server" colspan="3" ID="vldSumaary" />
    </td>
        <td>
            <asp:TextBox ID="hdntext" runat="server" Width="267px" MaxLength="25" EnableViewState="true"
                class="border_Non_ReadOnly" Visible="false"></asp:TextBox>
        </td>
        </tr> </table>
    </form>
    <%--CHG0112662 - BEGIN - Added a new BR tag to fix the body tag issue for a specific SIS policy--%>
    <br />
    <%--CHG0112662 - END - Added a new BR tag to fix the body tag issue for a specific SIS policy--%>
</body>
</html>
