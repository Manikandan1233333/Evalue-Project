<!--
		* HISTORY
		* MODIFIED BY COGNIZANT AS PART OF .NET MIGRATION CHANGES
		* CHANGES DONE
        CHG0116140 - Modified the EmptyDataText message in the grid
-->
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="NameSearch.ascx.cs" Inherits="MSC.Controls.NameSearch" %>
<%@ Register TagPrefix="MSC" Namespace="MSC" Assembly="MSC" %>
<style type="text/css">
    .style1
    {
        width: 103px;
    }
</style>
<%--<style type="text/css">
    .footer 
    td
    {
        border-top-style:dashed;
        border-bottom-style :none;
        border-left-style : none;
        border-right-style:none;
    }
    
 
</style>--%>
<script type="text/javascript">
    function SkipValidation() {
        if (document.getElementById("<%=LnkSearchByCustomerName.ClientID %>").disabled == false) {
            document.getElementById("<%=SkipParentValidation.ClientID%>").value = "true";
        }
    }
   
</script>
<tr>
                <!-- MAIG - CH88 - BEGIN - Added the code to display a link button for installment payment -->
                    <td colspan="3" align="right" height="20">
                    <asp:LinkButton ID="LnkSearchByCustomerName" EnableViewState="true" runat="server" Style="text-decoration: underline;
                            font-weight: bold; font-family: arial, helvetica, sans-serif; font-size: 12px;
                            color: #000000;" OnClientClick="SkipValidation();" OnClick="LnkSearchByCustomerName_Click" 
                             >Search By Customer Name</asp:LinkButton>
                             <asp:HiddenField ID="SkipParentValidation" runat="server" Value="" EnableViewState="true" />
                             </td>
                    <!-- MAIG - CH88 - END - Added the code to display a link button for installment payment -->
                </tr>
                <%--PT MAIG -Customer Name Search--%>
                <asp:Panel ID="pnlCustomerNameMain" runat="server" Visible="false">
                    <tr>
                        <td colspan="3" align="left">
                            <table width="600" cellpadding="4" cellspacing="0" bgcolor="#ffffcc" runat="server" align="center"
                                id="table1">
                                <tr>
                                    <td class="arial_11_bold_white" bgcolor="#333333" colspan="2" height="22">
                                        &nbsp;&nbsp;Customer Name Search
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" height="5">
                                    </td>
                                </tr>
                                <tr>
                                    <td class="style1">
                                        &nbsp;&nbsp;<asp:Label ID="lblFirstName" Text="First Name" runat="server" Style="font-weight: bold;
                                            font-family: arial, helvetica, sans-serif; font-size: 12px;"></asp:Label>
                                    </td>
                                    <td class="arial_12" align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="_FirstName" onkeypress="return alphabets_onkeypress(event)" runat="server" Width="200" MaxLength="25" EnableViewState="true"
                                            class="border_Non_ReadOnly"></asp:TextBox>
                                        &nbsp;&nbsp;(Use * as wildcard)
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td>
                            &nbsp;&nbsp;<asp:Label ID ="lblLastName" Text = "Last Name" runat ="server" style="font-weight:bold;font-family: arial, helvetica, sans-serif;font-size: 12px;color:#000000;">*</asp:Label>
                        </td>--%>
                                    <%--<CSAA:Validator ID="vldLastName" runat="server" ControlToValidate="_LastName">&nbsp;&nbsp;LastName</CSAA:Validator>--%>
                                    <td class="style1">
                                        &nbsp;&nbsp;<asp:Label ID="lblLastName" Text="Last Name" runat="server" Style="font-weight: bold; font-family: arial, helvetica, sans-serif; font-size: 12px;"></asp:Label>&nbsp;<span class="arial_11_red">*</span>
                                    </td>
                                    <td class="arial_12" align="left" height="10">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="_LastName" onkeypress="return alphabets_onkeypress(event)" runat="server" Width="200" MaxLength="25" EnableViewState="true"
                                            class="border_Non_ReadOnly" ontextchanged="_LastName_TextChanged"></asp:TextBox>
                                        &nbsp;&nbsp;(Use * as wildcard)
                                        <%--<CSAA:Validator ID="vldLastNameReq" runat="server" OnServerValidate="ReqValLastName"></CSAA:Validator>--%>
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td>
                            &nbsp;&nbsp;<asp:Label ID ="lblMailingZip" Text = "Mailing Zip" runat ="server" style="font-weight:bold;font-family: arial, helvetica, sans-serif;font-size: 12px;color:#000000;"></asp:Label>
                        </td>--%>
                                    <%--<CSAA:Validator ID="vldMailingZip" runat="server" ControlToValidate="_MailingZip">&nbsp;&nbsp;Mailing Zip</CSAA:Validator>--%>
                                    <td class="style1">
                                        &nbsp;&nbsp;<asp:Label ID="lblMailingZip" Text="Billing Zip" runat="server" Style="font-weight: bold; font-family: arial, helvetica, sans-serif; font-size: 12px;"></asp:Label>&nbsp;<span class="arial_11_red">*</span>
                                    </td>
                                    <td class="arial_12" align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="_MailingZip" onkeypress="return digits_only_onkeypress(event)" runat="server" Width="200" MaxLength="5" EnableViewState="true"
                                            class="border_Non_ReadOnly" ontextchanged="_MailingZip_TextChanged"></asp:TextBox>
                                     <%--<CSAA:Validator ID="vldMailingZipCheck" runat="server" Display="None" OnServerValidate="CheckMailingZip"
                                    ErrorMessage="Mailing Zip is mandatory"></CSAA:Validator>--%>
                                       <%-- <CSAA:Validator ID="vldMailingZipReq" runat="server" OnServerValidate="ReqValZipCheck"></CSAA:Validator>--%>
                                    </td>
                                </tr>
                            </table>
                            <table>
                            <%--<CSAA:Validator ID="vldData" runat="server" ErrorMessage=".." Display="None"
                    OnServerValidate="CheckValidation"></CSAA:Validator>--%>
                            </table>
                            <asp:PlaceHolder ID="Results" runat="server" Visible ="false">
                                <table width="600" cellpadding="0" cellspacing="0" bgcolor="#ffffcc" align="center">
                                    <tr>
                                        <td colspan="2" style="padding-left:10px;" class="arial_11_bold_white" bgcolor="#333333" colspan="2" height="22">
                                            
                                                <asp:Label ID="lblCustomerNameSearch" runat="server" Text="  Search Results"
                                                    Visible="false"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblerrorMessage" runat="server" Text="" Visible="false" CssClass="arial_12_bold" ForeColor="Red"></asp:Label>
                                            <%--CHG0116140 - BEGIN - Modified the EmptyDataText message in the grid--%>
                                            <asp:GridView runat="server" ID="NameSearchResults" AutoGenerateColumns="false" AllowPaging="true"
                                                CssClass="arial_12" PageSize="10" RowStyle-Height="30" GridLines="Both" Width="600px" OnRowCreated = "NameSearchResults_RowCreated"
                                                OnPageIndexChanging="NameSearchResults_PageIndexChanging" CellPadding="8" EmptyDataText="There are no records for the given search criteria">
                                                <EmptyDataRowStyle Font-Bold ="true" ForeColor ="Red" /> 
                                                <HeaderStyle Font-Bold="true" BackColor="ActiveBorder" />
                                                <Columns>
                                                    <asp:TemplateField HeaderText="Select">
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="rbtnSelect" GroupName="SelectPolicy" AutoPostBack="true" runat="server" OnCheckedChanged="RbtnSelect_CheckedChanged" />
                                                        </ItemTemplate>
                                                    </asp:TemplateField>
                                                 <asp:BoundField runat="server" DataField="policyNumber" HeaderText="Policy Number"
                                                        HtmlEncode="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                    <asp:BoundField runat="server" DataField="sourceSystem" HeaderText="Source System"
                                                        HtmlEncode="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                    <asp:BoundField runat="server" DataField="productCode" HeaderText="Product" HtmlEncode="false"
                                                    ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                    <asp:BoundField runat="server" DataField="policystatus" HeaderText="Status" HtmlEncode="false"
                                                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                    <asp:BoundField runat="server" DataField="NamedInsured" HeaderText="Named Insured"
                                                        HtmlEncode="false" ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                    <%--<asp:BoundField runat="server" DataField="lastName" HeaderText="Last Name" HtmlEncode="false" ItemStyle-HorizontalAlign ="Center" ></asp:BoundField>--%>
                                                    <asp:BoundField runat="server" DataField="address" HeaderText="Address" HtmlEncode="false"
                                                        ItemStyle-HorizontalAlign="Center"></asp:BoundField>
                                                </Columns>
                                                <%--<FooterStyle HorizontalAlign="Center"/>--%>
                                                <%--<PagerStyle HorizontalAlign="right" />--%>
                                                <%--<pagersettings mode="NextPreviousFirstLast" firstpagetext="First" lastpagetext="Last" pagebuttoncount="5" position="Bottom"/>--%>
                                                <PagerSettings Mode="NumericFirstLast" FirstPageText ="<<" PreviousPageText ="<" NextPageText =">" LastPageText =">>"  Position="Bottom" PageButtonCount="5"/>
                                            </asp:GridView>
                                            <%--CHG0116140 - END - Modified the EmptyDataText message in the grid--%>
                                        </td>
                                    </tr>
                                </table>
                            </asp:PlaceHolder>
                        </td>
                    </tr>
                    </asp:Panel>
                <%--PT MAIG -Customer Name Search--%>