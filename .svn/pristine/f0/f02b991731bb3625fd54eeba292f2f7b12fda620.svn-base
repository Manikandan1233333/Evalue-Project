<%--CHG0121437 - Added new class for ACH Enrollment Pilot changes--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ACHEnrollmentPilot.aspx.cs" Inherits="MSC.Forms.ACHEnrollmentPilot" %>

<%@ Register TagPrefix="uc" TagName="PageValidator" Src="../Controls/PageValidator.ascx" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>ACH Enrollment Pilot</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio 7.0">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientSc
        ript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

    <style type="text/css">
        .arial_12_bold_red {
            font-family: arial, helvetica, sans-serif;
            color: #ff0000;
            font-size: 12px;
            font-weight: bold;
            text-decoration: none;
        }

        .grid,
        .grid tr th,
        .grid tr td {
            border: 1px solid black;
            font-family: arial, helvetica, sans-serif;
            color: #000000;
            font-size: 12px;
        }

        @media screen and (min-width:0\0) {
            #Panel1 {
                padding-top: 20px;
            }
        }

        .arial_20 {
            font-family: arial, helvetica, sans-serif;
            color: #000000;
            font-size: 20px;
        }
    </style>

    <script type="text/javascript" language="javascript">

        function ACHPilotConfirm() {

            alert("Submitted Successfully");
            slourldata = "/PaymentToolmsc/Forms/ACHEnrollmentPilot.aspx";
            window.location.assign(slourldata);
        }

    </script>

</head>
<body>
    <form id="ACHEntrollmentPilot" runat="server" autocomplete="off">
        <br />
        <div>
            <asp:Panel ID="pnlEnroll" runat="server" DefaultButton="btnSearch">
                <table width="620" cellpadding="0" cellspacing="0">
                    <tr>
                        <td height="5"></td>
                    </tr>
                    <tr>
                        <td colspan="2" align="left" class="arial_15_bold">ACH Enrollment Pilot
                        </td>
                    </tr>
                    <tr>
                        <td bgcolor="#999999" height="2" class="style1"></td>
                    </tr>
                </table>
                <br />
                <table width="620" bgcolor="#ffffcc" align="center">
                    <tr>
                        <td colspan="3" class="arial_11_bold_white" bgcolor="#333333" height="22">&nbsp;Policy Search
                        </td>
                    </tr>

                    <tr>
                        <td height="5"></td>
                    </tr>
                    <caption>
                        <tr height="2">
                            <td class="arial_12" width="191">
                                <CSAA:Validator ID="vldProductType" runat="server" ControlToValidate="_ProductType"
                                    ErrorMessage=" " Tag="span">&nbsp;&nbsp;Product Type</CSAA:Validator>
                            </td>
                            <td class="arial_12">
                                <asp:DropDownList ID="_ProductType" runat="server" AutoPostBack="true" DataValueField="ID" EnableViewState="true" OnSelectedIndexChanged="_ProductType_SelectedIndexChanged" DataTextField="Description"
                                    TabIndex="1" Width="200">
                                </asp:DropDownList>
                            </td>
                            <CSAA:Validator ID="PolicyReqValidator" runat="server" ControlToValidate="_ProductType"
                                DefaultAction="Required" ErrorMessage="Please enter policy number " OnServerValidate="ReqValCheck1"></CSAA:Validator>
                        </tr>
                    </caption>
                    <tr height="2">
                        <td class="arial_12" width="191">
                            <CSAA:Validator ID="vldPolicyNumber" runat="server" ControlToValidate="_PolicyNumber"
                                ErrorMessage=" " Tag="span">&nbsp;&nbsp;Policy Number</CSAA:Validator>
                            <CSAA:Validator ID="vldPolicyNumberLength" runat="server" ControlToValidate="_PolicyNumber"
                                Display="None" ErrorMessage="Policy Number should be minimum 7 Alpha-Numeric characters." OnServerValidate="ValidPolciyNumberLength"></CSAA:Validator>


                        </td>
                        <td class="arial_12">

                            <asp:TextBox ID="_PolicyNumber" runat="server" onkeypress="return alphanumeric_onkeypress(event)" class="border_Non_ReadOnly" EnableViewState="true"
                                MaxLength="13" TabIndex="2" Width="200px"></asp:TextBox>


                        </td>

                    </tr>


                    <tr>
                        <td bgcolor="#cccccc" colspan="2" height="22">
                            <asp:ImageButton ID="btnSearch" runat="server" align="right" alt="search" border="0"
                                Height="17" ImageUrl="/PaymentToolimages/btn_search.gif" OnClick="btnSearch_Click"
                                TabIndex="3" Width="68" />
                            <a href="ACHEnrollmentPilot.aspx">
                                <img align="right" alt="Clear" border="0" height="17" src="../images/btn_cancel.gif"
                                    width="51" /></a>
                        </td>
                    </tr>

                    <tr>
                        <td>
                            <uc:PageValidator runat="server" colspan="3" ID="vldSumaary" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Label ID="lblErrorMsg" runat="server" Text="" CssClass="arial_12_bold_red"></asp:Label>

            <asp:Panel ID="enrollDetails" runat="server" Visible="false" EnableViewState="true">
                <table width="620" bgcolor="#ffffcc">
                    <tr>


                        <td class="arial_11_bold_white" bgcolor="#333333">&nbsp;Search Result(s)
                        </td>
                    </tr>

                    <tr>
                        <td>

                            <br />

                        </td>

                    </tr>
                    <tr>

                        <td align="center">

                            <asp:DetailsView ID="grdACHEnrollment" runat="server" Width="600" CssClass="arial_12"
                                AlternatingRowStyle-HorizontalAlign="Left" AutoGenerateColumns="True" CellPadding="10" CellSpacing="0"
                                Visible="true" BorderStyle="Solid" BorderColor="Black" GridLines="Both" CaptionAlign="Left"
                                HeaderStyle-HorizontalAlign="Left">
                                <HeaderStyle Width="10px"
                                    CssClass="arial_12_bold" BackColor="#FFFFFF" />
                                <FieldHeaderStyle BackColor="#E9ECF1" Font-Bold="True" Width="150px" />

                                <RowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                                <AlternatingRowStyle HorizontalAlign="Left" VerticalAlign="Middle" />
                            </asp:DetailsView>
                        </td>
                    </tr>


                </table>

                <table width="620" bgcolor="#ffffcc" align="center">
                    <tr>
                        <td>

                            <br />
                        </td>

                    </tr>
                    <tr>
                        <td class="arial_11_bold_white" bgcolor="#333333">&nbsp;Recent Payments
                        </td>
                    </tr>

                    <tr>

                        <td>
                            <br />
                        </td>
                    <tr>
                        <td align="center">
                            <asp:GridView ID="grdRecentPayments" runat="server" AutoGenerateColumns="false" CaptionAlign="Left" CellPadding="10" CssClass="grid" GridLines="Both" HeaderStyle-HorizontalAlign="Left" ShowHeaderWhenEmpty="True" Visible="true" Width="600">
                                <HeaderStyle BackColor="#E9ECF1" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <%--CHG0123980 - ACH Incentive Enhancement - April 2016--%>
                                <Columns>
                                    <asp:TemplateField HeaderText ="Transaction Date/Time">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionDateTime" runat="server" Text='<%# Bind("TransactionDateTime") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Channel">
                                        <ItemTemplate>
                                            <asp:Label ID="lblChannel" runat="server" Text='<%# Bind("Channel") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Payment Method">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPaymentMethod" runat="server" Text='<%# Bind("PaymentMethod") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="Amount">
                                        <ItemTemplate>
                                            <asp:Label ID="lblAmount" runat="server" Text='<%# Bind("Amount") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText ="Transaction Id">
                                        <ItemTemplate>
                                            <asp:Label ID="lblTransactionId" runat="server" Text='<%# Bind("TransactionId") %>'></asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <%--CHG0123980 - ACH Incentive Enhancement - April 2016--%>
                                <EmptyDataTemplate>
                                    No Recent Payments found for the policy
                                </EmptyDataTemplate>
                            </asp:GridView>
                        </td>
                    </tr>
                </table>

                <table width="620" bgcolor="#ffffcc" align="center">
                    <tr>
                        <td>

                            <br />
                        </td>

                    </tr>
                    <%--Commented the below code as a part of ACH Incentive Enhancement - Start--%>
<%--                    <tr>
                        <td class="arial_11_bold_white" bgcolor="#333333">&nbsp;Action Taken
                        </td>
                    </tr>--%>
                    <%--Commented the below code as a part of ACH Incentive Enhancement - End--%>
                    <tr>
                        <td align="right" class="arial_11_bold_white" bgcolor="#cccccc" height ="22">
                            <%--CHG0123980 - ACH Incentive Enhancement - April 2016- Added two buttons to capture Check Incentive --%>
                             <asp:ImageButton ID="btnCheckIncentive" runat="server" Width="80px" Height="17" ImageUrl="~/Images/btn_IssueCheck2.gif" OnClick="btnIncentive_Click"
                                ></asp:ImageButton>
                             <a href="ACHEnrollmentPilot.aspx">
                             <img alt="Clear" border="0" height="17" src="../Images/btn_Search Again 2.gif"
                                    width="80" /></a>
                            
                           <%-- <img ID="btnSearchAgain" alt="Clear" runat="server" Width="80px" Height="17" src="~/Images/btn_Search Again 2.gif" />
                                 </a>--%>
                            <%--CHG0123980 - ACH Incentive Enhancement - April 2016 -  Added two buttons to capture Check Incentive--%>
                           <%-- <asp:RadioButtonList ID="rblActions" class="arial_12_bold" RepeatDirection="Horizontal" ValidationGroup="ActionTaken" CausesValidation="true"
                                RepeatLayout="Table" runat="server">
                                <asp:ListItem Value="Check">Check</asp:ListItem>
                                <asp:ListItem Value="Credit Policy">Credit Policy</asp:ListItem>
                                <asp:ListItem Value="No Incentive">No Incentive</asp:ListItem>

                            </asp:RadioButtonList>
                            <CSAA:Validator ID="RadioButtonListValidator" runat="server" ControlToValidate="rblActions"
                                DefaultAction="Required" ErrorMessage="Please Select Action taken" OnServerValidate="ReqValCheck1"></CSAA:Validator>--%>



                        </td>
                    </tr>

<%--                    <tr>
                        <td bgcolor="#cccccc" height="22" align="right">
                            <asp:ImageButton ID="btnSubmit" runat="server" Width="67px" Height="17" ImageUrl="../images/btn_submit.gif" OnClick="btnSubmit_Click"></asp:ImageButton>

                        </td>
                    </tr>--%>
                </table>

            </asp:Panel>
        </div>

    </form>
</body>
</html>
