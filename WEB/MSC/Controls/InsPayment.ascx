<!-- MAIG - CH1 - Added the below javascript code
	 MAIG - CH2 - Added the id attributes to Table and modified the style attributes
	 MAIG - CH3 - Modified the Width from 270 to 200
	 MAIG - CH4 - Modified the Width from 270 to 200
	 MAIG - CH5 - Added the attribute Align Right
	 MAIG - CH6 - Added the code to display a link button for installment payment
	 MAIG - CH7 - Added the Hidden hdnRevenueType Field to store the Revenue Type value
	 MAIG - CH8 - Added the User Control Name Search as part of MAIG
     JQuery Upgrade Changes- 05/29/2017

-->
<%@ Reference Control="~/Controls/PolicyNumber.ascx" %>
<%@ Control Language="c#" AutoEventWireup="True" CodeBehind="InsPayment.ascx.cs"
    Inherits="MSC.Controls.InsPayment" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Register TagPrefix="UC" TagName="Policy" Src="PolicyNumber.ascx" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<!-- MAIG - CH8 - BEGIN - Added the User Control Name Search as part of MAIG -->
<%@ Register TagPrefix="NS" TagName="NameSearch" Src="~/Controls/NameSearch.ascx" %>
<!-- MAIG - CH8 - END - Added the User Control Name Search as part of MAIG -->
<meta content="Microsoft Visual Studio 7.0" name="GENERATOR">
<meta content="C#" name="CODE_LANGUAGE">
<meta content="JavaScript" name="vs_defaultClientScript">
<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
<style type="text/css">
    .style1
    {
        width: 569px;
    }
    .style2
    {
        height: 20px;
        width: 569px;
    }
</style>
<!-- MAIG - CH1 - BEGIN - Added the below javascript code  -->
<%--CHGxxxx- JQuery Upgrade Changes- 05/29/2017- start--%>
<script src="../Scripts/jquery-3.2.1.min.js" type="text/javascript"></script>
<%--CHGxxxx- JQuery Upgrade Changes- 05/29/2017- end--%>
<script src="../Scripts/jquery.cookie.js" type="text/javascript"></script>

<script type="text/javascript">
    function Openpop() {
        var left = (screen.width * 0.3);
        var top = (screen.height * 0.125);
        return window.open("../Forms/SearchResults.aspx", "SearchPopup", 'toolbar=no, location=no, directories=no, status=no, menubar=no, scrollbars=yes, resizable=no, copyhistory=no, width=' + (screen.width * 0.40) + ', height=' + (screen.height * 0.50) + ', top=' + top + ', left=' + left);
    }

    function AnotherFunction() {
        $("#Mainpage tbody tr td:contains('Product Type'):first").each(function() {
            $(this).css("color", "black");
        });
    }
    function UpdateRevenuType() {
        $(document).ready(function() {
            //var cookieValue = $.cookie('IsDown');
            if ($.cookie('IsDown') == 'true') {
                $('select[id$="_ProductType"] option').each(function(index) {
                    if ($(this).val() != 0 && $(this).val() != 'PA' && $(this).val() != 'HO' && $(this).val() != 'PU') {
                        $(this).remove();
                    }
                });
                $('select[id$="_RevenueType"] option[value="297"]').prop('selected', 'selected');
                $('#<%=hdnRevenuType.ClientID %>').val("297");
               //var theControl = document.getElementById('#<%=_NameSearch.ClientID %>');
//                theControl.style.display = "none";
                //$('#<%=_NameSearch.ClientID %>').css("display", "none");

            }
            else {
                $('select[id$="_RevenueType"] option[value="1291"]').prop('selected', 'selected');
                $('#<%=hdnRevenuType.ClientID %>').val("1291");
                //$('#<%=_NameSearch.ClientID %>').css("display", "block");
//                var theControl = document.getElementById('#<%=_NameSearch.ClientID %>');
//                theControl.style.display = "";
            }

        });
    }

    
    
</script>

<!-- MAIG - CH1 - END - Added the below javascript code  -->
<tr>
    <td width="30">
        <td>
            <!-- 3 columns totaling 520: 140, 270, 110 -->
			<!-- MAIG - CH2 - BEGIN - Added the id attributes to Table and modified the style attributes -->
            <table id="Mainpage" cellspacing="0" cellpadding="0" bgcolor="#ffffcc" border="0"
                style="width: 600px; height: 141px; margin-right: 0px;table-layout:fixed;">
			<!-- MAIG - CH2 - END - Added the id attributes to Table and modified the style attributes -->
                <tr>
                    <td class="arial_11_bold_white" bgcolor="#333333" colspan="3" height="22">
                        &nbsp;&nbsp;Product Details
                        <asp:Label ID="LabelN" Visible="False" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td height="5" class="style1">
                    </td>
                    <td width="250" height="5">
                    </td>
                    <td width="100" height="5">
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="3" height="5">
                    </td>
                </tr>
                <tr>
                    <CSAA:Validator ID="vldProductType" runat="server" ControlToValidate="_ProductType"
                        Class="arial_12_bold">
				&nbsp;&nbsp;Product Type</CSAA:Validator>
                    <td align="left" class="style2">
						<!-- MAIG - CH3 - BEGIN - Modified the Width from 270 to 200  -->
                        <asp:DropDownList ID="_ProductType" Width="200" runat="server" EnableViewState="true"
                            DataValueField="ID" DataTextField="Description" AutoPostBack="true" OnSelectedIndexChanged="_ProductType_SelectedIndexChanged">
                        </asp:DropDownList>
						<!-- MAIG - CH3 - END - Modified the Width from 270 to 200  -->
                    </td>
                    <!--.NetMig.Ch1: Added new validator to validate ProductType -->
                    <td style="height: 15px" width="110">
                    </td>
                </tr>
                <tr>
                    <td colspan="3" height="4">
                    </td>
                </tr>
                <tr>
                    <CSAA:Validator ID="vldRevenueType" runat="server" ControlToValidate="_RevenueType"
                        Class="arial_12_bold">
				&nbsp;&nbsp;Revenue Type</CSAA:Validator>
                    <td align="left" class="style1">
						<!-- MAIG - CH4 - BEGIN - Modified the Width from 270 to 200  -->
                        <asp:DropDownList ID="_RevenueType" Width="200" runat="server" EnableViewState="true"
                            DataValueField="ID" DataTextField="Description" AutoPostBack="false">
                        </asp:DropDownList>
						<!-- MAIG - CH4 - END - Modified the Width from 270 to 200  -->
                    </td>
                    <!--.NetMig.Ch2:Added new validator to validate RevenueType -->
                    <td width="110">
                    </td>
                </tr>
                <tr>
                    <td colspan="3" height="3">
                    </td>
                </tr>
                <UC:Policy ID="_Policy" runat="Server" Required="True"></UC:Policy>
                <CSAA:Validator ID="vldPolicyLength" runat="server" ErrorMessage=".." Display="None"
                    OnServerValidate="CheckPolicyLength"></CSAA:Validator>
                <tr>
                    <!--End-->
                </tr>
                <!--END-->
                <tr>
					<!-- MAIG - CH5 - BEGIN - Added the attribute Align Right-->
                    <td colspan="3" align="right" height="5">
                    </td>
					<!-- MAIG - CH5 - END - Added the attribute Align Right-->
                </tr>
                <tr>
                    <td class="arial_9" colspan="3">
                    </td>
                </tr>
                <tr>
                    <!-- MAIG - CH6 - BEGIN - Added the code to display a link button for installment payment -->
                    <td colspan="3" align="right" height="5">
                        <% if (_RevenueType.SelectedValue.Equals(CSAAWeb.Constants.PC_REVENUE_INST) )
                           {%>
                           <NS:NameSearch ID="_NameSearch" runat="server" Visible="true" />
                        <asp:TextBox ID="HiddenNameSearch" Visible="false" runat="server" EnableViewState="true"></asp:TextBox>
                        <asp:TextBox ID="HiddenSelectedDuplicatePolicy" Visible="false" runat="server" Text=""
                            EnableViewState="true"></asp:TextBox>
                        <%} %>
                    </td>
                    <!-- MAIG - CH6 - END - Added the code to display a link button for installment payment -->
                </tr>
                <tr>
                    <td colspan="3" height="5">
					<!-- MAIG - CH7 - BEGIN - Added the Hidden hdnRevenueType Field to store the Revenue Type value-->
                    <asp:HiddenField runat="server" ID="hdnRevenuType" />
					<!-- MAIG - CH7 - END - Added the Hidden hdnRevenueType Field to store the Revenue Type value-->
                        <asp:TextBox ID="HidddenDueDetails" Visible="False" runat="server" MaxLength="40"></asp:TextBox>
                        <asp:TextBox ID="HiddenAmountSelected" Visible="False" runat="server" MaxLength="40"></asp:TextBox>
                        <asp:TextBox ID="HidddenPolicyValidate" Visible="False" runat="server" MaxLength="40"></asp:TextBox>
                        <asp:TextBox ID="Property_value" Visible="False" runat="server" MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </td>
        <td width="200">
        </td>
</tr>
