<%--Added new aspx for OTSP Changes--%>
<%--CHGXXXX - MAR 2017 - AJAX Session timeout--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" >

<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ScheduledPayments.aspx.cs" Inherits="MSC.Forms.OTSP" EnableSessionState="true" %>

<%@ Register TagPrefix="uc" TagName="PageValidator" Src="../Controls/PageValidator.ascx" %>
<%@ Register TagPrefix="CSAA" Namespace="CSAAWeb.WebControls" Assembly="CSAAWeb" %>
<%@ Register TagPrefix="NS" TagName="NameSearch" Src="~/Controls/NameSearch.ascx" %>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>View Scheduled Payments</title>
    <meta name="CODE_LANGUAGE" content="C#" />
    <meta charset="utf-8" />
    <meta name="vs_defaultClientScript" content="JavaScript" />
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5" />
    <%--<meta http-equiv="X-UA-Compatible" content="IE=EGDE" />--%>
    <link href="../style.css" type="text/css" rel="stylesheet">
    <%--CHGxxxx- JQuery Upgrade Changes- 05/29/2017- start--%>
    <script src="../Scripts/jquery-3.2.1.min.js" type="text/javascript"></script>
    <%--CHGxxxx- JQuery Upgrade Changes- 05/29/2017- end--%>
    <script src="../Scripts/json2.js" type="text/javascript"></script>
    <script type="text/javascript">
        var pageSize = 10;
        var currentPageNo = 1;

        function pageData(e, PaymentID) {
            var token = $("#hdnAccessToken").val();

            var skip = e == 1 ? 0 : (e * pageSize) - pageSize;
            debugger;
            $.ajax({
                type: "POST",
                url: "ScheduledPayments.aspx/GetData",
                //CHG0131014 - Updated code to handle the Paging issue in History section  - Dec 2016
                data: JSON.stringify({ 'skip': skip, 'take': pageSize, 'id': PaymentID, 'token': token }),
                //CHG0131014 - Updated code to handle the Paging issue in History section  - Dec 2016
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                async: true,
                cache: false,
                success: function (response) {
                    currentPageNo = e;
                    PopulateScheduleHistory(response);
                    //CHG0131014 - Updated code to handle the Paging issue in History section  - Dec 2016
                    var totalCount = response.d[0].SchCount;
                    $("#paging").text("");
                    var pageTotal = Math.ceil(totalCount / pageSize);
                    if (totalCount > 10) {
                        if (pageTotal != 1) {

                            for (var i = 0; i < pageTotal; i++) {
                                //CHG0131014 - Added the below condition to update color for the visited page
                                if (e == i + 1) {
                                    //CHG0131014 - Added the below condition to update color for the visited page
                                    $("#paging").append("<a class=\"visited\" (" + (i + 1) + "," + PaymentID + ")\">" + (i + 1) + "</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                                }
                                else {

                                    $("#paging").append("<a class=\"notvisited\" href=\"#\" onClick=\"pageData(" + (i + 1) + "," + PaymentID + ")\">" + (i + 1) + "</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                                }
                            }
                            //Updated the below condition to update Paging text in Scheduled Payments History section
                            //$("#paging").append("<span class=\"PagingText\">Showing Page "  + currentPageNo + " of " + pageTotal+" <\span>");
                            //Updated the below condition to update Paging text in Scheduled Payments History section
                        }
                    }
                    //CHG0131014 - Updated code to handle the Paging issue in History section - Dec 2016
                }
            });
            return false;
        }

        var rowIndex = -1;

        $(document).ready(function () {
            $("#pnlHistory").hide();
            $("#pnlSchHistory").hide();

            $('[id$="PymtID"]').click(function (e) {
                e.preventDefault();
                $("#pnlHistory").show();
                $("#pnlSchHistory").show();
                var customID = $("#hdnPymtID").val();


                var token = $("#hdnAccessToken").val();

                var pgLbl = $("#lblPagingText").text();
                var pymtSelectedID = customID.split(';')[rowIndex];
                $('[id$="PymtID"]').prop('disabled', false).css('opacity', 1);
                $(this).prop('disabled', true).css('opacity', 0.4);


                $.ajax({
                    type: "post",
                    url: "ScheduledPayments.aspx/GetData",
                    contentType: "application/json; charset=utf-8",
                    data: JSON.stringify({ 'skip': 0, 'take': pageSize, 'id': pymtSelectedID, 'token': token }),
                    dataType: "json",
                    success: function (response) {
                        var total = 1;
                        var pgLbl = $("#lblPagingText").text();
                        if (total > 0) {
                            PopulateScheduleHistory(response);
                            var totalCount = response.d[0].SchCount;
                            $("#paging").text("");

                            var pageTotal = Math.ceil(totalCount / pageSize);
                            var pageIndex = ("<%=grdSchHistory.PageIndex %>");
                            if (totalCount > 10) {
                                if (pageTotal != 1) {

                                    for (var i = 0; i < pageTotal; i++) {
                                        if (currentPageNo - 1 == i) {
                                            $("#paging").append("<a class=\"visited\" href=\"#\" onClick=\"pageData(" + (i + 1) + "," + pymtSelectedID + ")\">" + (i + 1) + "</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                                        }
                                        else {
                                            $("#paging").append("<a class=\"notvisited\" href=\"#\" onClick=\"pageData(" + (i + 1) + "," + pymtSelectedID + ")\">" + (i + 1) + "</a>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;");
                                        }

                                    }
                                }
                                //Updated the below condition to update Paging text Scheduled Payments History section
                                //$("#paging").append($("#lblPagingText").text("Showing Page " + currentPageNo + " of " + pageTotal));
                                //Updated the below condition to update Paging text Scheduled Payments History section
                            }
                        }
                        $("#totalRecords").text("Total records: " + total);
                    },

                    failure: function (response) {
                        alert(response.d[i].ErrorCode.moreInformation);
                        var lblText = response.d[i].ErrorCode.moreInformation;
                        $("#pnlHistory").hide();
                        $("#pnlSchHistory").hide();
                        $("#lblErrorSchHistory").html(lblText);
                    }
                });
            });
        });

        function SelectRow(lnk) {
            var row = lnk.parentNode.parentNode;
            //Updated the below condition to fix the Pagination issue in Scheduled Payments section for displaying all Payment ID's
            var pageIndex = ("<%=grdScheduledPayments.PageIndex %>");
            rowIndex = Math.ceil((pageIndex) * 10) + (row.rowIndex - 1);
            //Updated the below condition to fix the Pagination issue in Scheduled Payments section for displaying all Payment ID's
        }
        function PopulateScheduleHistory(response, pageSize) {

            if (response != null) {

                $("#grdSchHistory tr:not(:first-child)").html("");
                $("#grdSchHistory tbody").find("tr:gt(0)").remove();
                for (var i = 0; i < response.d.length; i++) {
                    if (response.d[i].scheduleActivities != null) {

                        for (var j = 0; j < response.d[i].scheduleActivities.length; j++) {

                            var accNo;
                            if (response.d[i].scheduleActivities[j].scheduleActivity.bankAccount != null && response.d[i].scheduleActivities[j].scheduleActivity.bankAccount.numberLast4 != null) {
                                accNo = response.d[i].scheduleActivities[j].scheduleActivity.bankAccount.numberLast4;
                            }
                            else if (response.d[i].scheduleActivities[j].scheduleActivity.cardAccount != null && response.d[i].scheduleActivities[j].scheduleActivity.cardAccount.numberLast4 != null) {
                                accNo = response.d[i].scheduleActivities[j].scheduleActivity.cardAccount.numberLast4;
                            }
                            else if (response.d[i].scheduleActivities[j].scheduleActivity.bankAccount != '' ||
                                response.d[i].scheduleActivities[j].scheduleActivity.cardAccount != '') {
                                accNo = "";
                            }
                            if (response.d[i].scheduleActivities[j].scheduleActivity.paymentDate != null) {
                                var pymtDate = response.d[i].scheduleActivities[j].scheduleActivity.paymentDate;
                                var value = new Date(parseInt(pymtDate.replace(/(^.*\()|([+-].*$)/g, '')));
                                var dat = ('0' + (value.getMonth() + 1)).slice(-2) + "/" +
                                ('0' + value.getDate()).slice(-2) + "/" +
                                value.getFullYear();
                            }
                            var amt = "$" + parseFloat(response.d[i].scheduleActivities[j].scheduleActivity.paymentAmt).toFixed(2);
                            var pymtMethod;
                            if (response.d[i].scheduleActivities[j].scheduleActivity.paymentMethod != null) {
                                pymtMethod = response.d[i].scheduleActivities[j].scheduleActivity.paymentMethod;
                            }
                            else if (response.d[i].scheduleActivities[j].scheduleActivity.paymentMethod != '') {
                                pymtMethod = "";
                            }
                            $("#grdSchHistory").append("<tr align='center' valign='middle'><td style='overflow-wrap: break-word;word-wrap: break-word;max-width: 110px !important;height: 30px;'>" + dat +
                                          "</td><td style='overflow-wrap: break-word;word-wrap: break-word;max-width: 110px !important;height: 30px;'>" + amt +
                                          "</td><td style='overflow-wrap: break-word;word-wrap: break-word;max-width: 110px !important;height: 30px;'>" + response.d[i].scheduleActivities[j].scheduleActivity.code +
                                          "</td><td style='overflow-wrap: break-word;word-wrap: break-word;max-width: 150px !important;height: 30px;width:150px'>" + response.d[i].scheduleActivities[j].scheduleActivity.notificationEmail +
                                          "</td><td style='overflow-wrap: break-word;word-wrap: break-word;max-width: 110px !important;height: 30px;'>" + response.d[i].scheduleActivities[j].scheduleActivity.userID +
                                          "</td><td style='overflow-wrap: break-word;word-wrap: break-word;max-width: 110px !important;height: 30px;'>" + response.d[i].scheduleActivities[j].scheduleActivity.schedulingChannel +
                                          "</td><td style='overflow-wrap: break-word;word-wrap: break-word;max-width: 110px !important;height: 30px;'>" + pymtMethod +
                                          "</td><td style='overflow-wrap: break-word;word-wrap: break-word;max-width: 110px !important;height: 30px;'>" + accNo +
                                          "</td></tr>");

                        }
                    }
                    else {
                        $("#pnlHistory").hide();
                        $("#pnlSchHistory").hide();
                        $("#lblErrorSchHistory").html(response.d[i].ErrorCode.moreInformation);
                    }

                }
            }

        }
       <%--Added the below Jquery to fix the Double click issue in Scheduled Payments section- Start--%>
        function setScrollPos() {
            var divY = document.getElementById('divScroll').scrollTop;
            document.cookie = "divPos=!*" + divY + "*!";
        }

        ///Attaching a function on window.onload event.
        window.onload = function () {
            var strCook = document.cookie;
            if (strCook.indexOf("!~") != 0) {
                var intS = strCook.indexOf("!~");
                var intE = strCook.indexOf("~!");
                var strPos = strCook.substring(intS + 2, intE);
                document.body.scrollTop = strPos;
            }
            /// This condition will set scroll position od <div>.
            if (strCook.indexOf("!*") != 0) {
                var intdS = strCook.indexOf("!*");
                var intdE = strCook.indexOf("*!");
                var strdPos = strCook.substring(intdS + 2, intdE);
                document.getElementById('divScroll').scrollTop = strdPos;
            }
        }
        /// Function to set Scroll position of page before postback.
        function SetScrollPosition() {
            var intY = document.body.scrollTop;
            document.cookie = "yPos=!~" + intY + "~!";
        }
        /// Attaching   SetScrollPosition() function to window.onscroll event.
        window.onscroll = SetScrollPosition;
        <%--Added the Jquery to fix the Double click issue in Scheduled Payments section- End--%>

    </script>

</head>
<body>

    <form id="ViewSchPayments" runat="server" autocomplete="off" defaultbutton="btnSearch">
        <asp:Panel ID="pnlPayments" runat="server" Width="700px">
            <div id="Content">
            </div>
            <table width="700px" cellpadding="0" cellspacing="0">
                <tr>
                    <td height="10"></td>
                </tr>
                <tr>
                    <td colspan="2" align="left" class="arial_15_bold">View Scheduled Payments
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#999999" height="2" class="style1"></td>
                </tr>
            </table>
            <br />
            <table width="700px" bgcolor="#ffffcc">
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
                                ErrorMessage=" " Tag="span">&nbsp;&nbsp;&nbsp;&nbsp;Product Type</CSAA:Validator>
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
                            ErrorMessage=" " Tag="span">&nbsp;&nbsp;&nbsp;&nbsp;Policy Number</CSAA:Validator>
                        <CSAA:Validator ID="vldPolicyNumberLength" runat="server" ControlToValidate="_PolicyNumber"
                            Display="None" ErrorMessage="Policy Number should be minimum 7 Alpha-Numeric characters." OnServerValidate="ValidPolicyNumberLength"></CSAA:Validator>


                    </td>
                    <td class="arial_12">

                        <asp:TextBox ID="_PolicyNumber" runat="server" onkeypress="return alphanumeric_onkeypress(event)" class="border_Non_ReadOnly" EnableViewState="true"
                            MaxLength="13" TabIndex="2" Width="200px"></asp:TextBox>


                    </td>

                </tr>
                <tr class="arial_12">
                    <td>
                        <NS:NameSearch ID="_NameSearch" runat="server" Visible="true" />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:TextBox ID="HiddenSelectedDuplicatePolicy" Visible="false" runat="server" Text="" EnableViewState="true"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="#cccccc" colspan="2" height="22">
                        <asp:ImageButton ID="btnSearch" runat="server" align="right" alt="search" border="0"
                            Height="17" ImageUrl="/PaymentToolimages/btn_search.gif" OnClick="btnSearch_Click"
                            TabIndex="3" Width="68" />
                        <a href="ScheduledPayments.aspx">
                            <img align="right" alt="Clear" border="0" height="17" src="../images/btn_cancel.gif"
                                width="51" /></a>
                    </td>
                </tr>

                <tr>
                    <td>
                        <uc:PageValidator runat="server" colspan="3" ID="vldSummary" />
                    </td>
                </tr>

            </table>
            <table width="700px">
                <div>
                    <asp:Label ID="lblErrorMsg" runat="server" Text="" CssClass="arial_12_bold_red"></asp:Label>
                </div>

            </table>
        </asp:Panel>
        <br />

        <asp:PlaceHolder ID="ScheduledRecurring" runat="server" Visible="false">
            <table width="700px" cellpadding="0" cellspacing="0" border="3" bordercolor="#999999"
                align="center">
                <tr>
                    <td colspan="2" class="arial_11_bold_white" bgcolor="#CCCCCC" height="22">
                        <font color="black">&nbsp;&nbsp;
                            <asp:Label ID="lbl_PolicySearch_Details" runat="server" Text="Recurring Details"></asp:Label></font>
                    </td>
                </tr>
                <div runat="server"></div>
                <tr style="background-color: #ffffcc; width: 100%">
                    <td colspan="2" height="10" style="border-bottom-color: #808080; width: 620px" bgcolor="#ffffcc">
                        <div></div>
                        <asp:GridView ID="gridRecurring" runat="server" Width="100%" CssClass="grdRecurring"
                            AlternatingRowStyle-HorizontalAlign="Center" AutoGenerateColumns="True" CellPadding="2" CellSpacing="1"
                            PageSize="20" Visible="true" BorderStyle="None" GridLines="None" CaptionAlign="Left"
                            ItemStyle-BorderWidth="0" HeaderStyle-BorderWidth="0" FooterStyle-BorderWidth="2"
                            FooterStyle-CssClass="border_Top_Down" HeaderStyle-HorizontalAlign="Center">
                            <HeaderStyle Height="10px" BorderWidth="0px" BorderStyle="Solid" BorderColor="#CCCCCC"
                                CssClass="arial_12_bold" BackColor="#ffffcc" />
                            <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                            <FooterStyle BorderStyle="Double" />
                        </asp:GridView>
                    </td>
                </tr>
                <tr>
                    <td bgcolor="ffffcc" height="15" style="width: 615px; border-top-color: #808080; border-color: white; border-style: none"></td>
                </tr>
            </table>
            <table>
                <tr>
                    <td bgcolor="FFFFFF" height="10"></td>
                </tr>
                <tr>
                    <td bgcolor="FFFFFF"></td>
                </tr>
            </table>
        </asp:PlaceHolder>
        <asp:PlaceHolder runat="server" ID="plcErrSch">
            <table width="700px">
                <div id="errSch" runat="server">
                    <asp:Label ID="lblErrorMsgSch" runat="server" CssClass="errorSch"></asp:Label>
                </div>
            </table>
        </asp:PlaceHolder>
        <div width="700px;" runat="server" style="white-space: normal">
            <asp:Panel ID="pnlSch" runat="server" Visible="false" Width="700px">

                <table cellpadding="0" cellspacing="0" style="width: 700px">
                    <tr>
                        <td height="5"></td>
                    </tr>
                    <tr>
                        <td class="arial_11_bold_white" bgcolor="#CCCCCC" height="22" style="border-width: 2px; border-color: #999999; border-style: solid">
                            <font color="black">&nbsp;
                           &nbsp;Scheduled Payments
                            </font>
                        </td>
                    </tr>
                    <%-- <tr>
                        <td bgcolor="#999999" height="2" class="style1"></td>
                    </tr>--%>
                </table>
            </asp:Panel>
            <asp:Panel ID="ScheduledPaymentSection" runat="server" Visible="false" Width="700px" BorderColor="#CCCCCC">
                <table width="700px" align="center" cellpadding="0" cellspacing="0" id="tblSchPayments">
                    <tr style="width: 100px">
                        <td style="white-space: normal; width: 700px">
                            <asp:GridView ID="grdScheduledPayments" runat="server" CssClass="grdRecurring" Width="100%"
                                AlternatingRowStyle-HorizontalAlign="Center" CellPadding="2" BorderStyle="Solid" BorderWidth="2px" BorderColor="#999999" GridLines="None" CaptionAlign="Left"
                                HeaderStyle-HorizontalAlign="Center" HeaderText="Scheduled Payments" RowStyle-CssClass="grdHistory" FooterStyle-BorderWidth="1" OnRowCreated="GrdScheduledPayments_RowCreated"
                                BackColor="#FFFFCC" AllowPaging="true" PageSize="10" OnPageIndexChanging="GrdScheduledPayments_PageIndexChanging" AutoGenerateColumns="false">
                                <HeaderStyle BorderWidth="0px" BorderStyle="Solid" BorderColor="#999999"
                                    CssClass="grdSchHeader" BackColor="#CCCCCC" Height="20px" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Actions">
                                        <ItemTemplate>
                                            <asp:ImageButton ID="PymtID" runat="server" Visible="true"
                                                OnClientClick="SelectRow(this);" border="0"
                                                Height="17" Width="68" ImageUrl="~/Images/VIEWHISTORY.png"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <%--CHG0131014 - Added the below condition to update date format to MM/dd/YYYY--%>
                                    <asp:BoundField DataField="Scheduled PMT Date" HeaderText="Scheduled PMT Date" ItemStyle-CssClass="Col" DataFormatString="{0:MM/dd/yyyy}" />
                                    <%--CHG0131014 - Added the below condition to update date format to MM/dd/YYYY--%>
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-CssClass="Col" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-CssClass="Email" />
                                    <asp:BoundField DataField="PMT Method" HeaderText="PMT Method" ItemStyle-CssClass="Col" />
                                    <asp:BoundField DataField="PMT Acct #" HeaderText="PMT Acct #" ItemStyle-CssClass="Col" />
                                    <asp:BoundField DataField="Status" HeaderText="Status" ItemStyle-CssClass="Col" />
                                    <asp:BoundField DataField="Confirmation Number" HeaderText="Confirmation Number" ItemStyle-CssClass="Col" />
                                </Columns>
                                <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <PagerSettings Mode="NumericFirstLast" FirstPageText="<<" PreviousPageText="<" NextPageText=">" LastPageText=">>" Position="Bottom" PageButtonCount="10" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <br />
            <asp:HiddenField ID="hdnPymtID" runat="server" ClientIDMode="Static" />
        </div>
        <asp:HiddenField ID="hdnAccessToken" runat="server" />
        <div></div>
        <div width="700px;" runat="server" style="white-space: normal">
            <asp:Panel ID="pnlHistory" runat="server" Width="700px">
                <table cellpadding="0" cellspacing="0" style="width: 700px;">
                    <tr>
                        <td height="5"></td>
                    </tr>
                    <tr>
                        <td class="arial_11_bold_white" bgcolor="#CCCCCC" height="22" style="border-width: 2px; border-color: #999999; border-style: solid">
                            <font color="black">&nbsp;
                        &nbsp;Scheduled Payment History
                            </font>
                    </tr>
                    <%--  <tr>
                        <td bgcolor="#999999" height="2" class="style1"></td>
                    </tr>--%>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlSchHistory" runat="server" Width="700px" BorderColor="#CCCCCC">
                <table width="700px" align="center" cellpadding="0" cellspacing="0" id="tblHistory">
                    <tr style="width: 100px">
                        <td style="white-space: normal; width: 700px">
                            <asp:GridView ID="grdSchHistory" runat="server" CssClass="grdRecurring" Width="700px"
                                AlternatingRowStyle-HorizontalAlign="Center" AutoGenerateColumns="false" CellPadding="2"
                                Visible="true" BorderStyle="Solid" BorderWidth="2px" BorderColor="#999999" GridLines="None" CaptionAlign="Left"
                                HeaderStyle-HorizontalAlign="Center" HeaderText="Scheduled Payments" RowStyle-CssClass="grdHistory" FooterStyle-BorderWidth="3"
                                FooterStyle-CssClass="border_Top_Down" BackColor="#ffffcc" AllowPaging="true" PageSize="10">
                                <HeaderStyle BorderWidth="0px" BorderStyle="Solid" BorderColor="#999999"
                                    CssClass="grdScheHistory" BackColor="#CCCCCC" Height="20px" />
                                <RowStyle HorizontalAlign="Center" VerticalAlign="Middle" />
                                <Columns>
                                    <asp:BoundField DataField="PMT Date" HeaderText="PMT Date" ItemStyle-CssClass="Col" />
                                    <asp:BoundField DataField="Amount" HeaderText="Amount" ItemStyle-CssClass="Col" />
                                    <asp:BoundField DataField="Activity / Event" HeaderText="Activity / Event" ItemStyle-CssClass="Col" />
                                    <asp:BoundField DataField="Email" HeaderText="Email" ItemStyle-CssClass="Email" />
                                    <asp:BoundField DataField="Initiated by" HeaderText="Initiated by" ItemStyle-CssClass="Col" />
                                    <asp:BoundField DataField="Channel" HeaderText="Channel" ItemStyle-CssClass="Col" />
                                    <asp:BoundField DataField="PMT Method" HeaderText="PMT Method" ItemStyle-CssClass="Col" />
                                    <asp:BoundField DataField="PMT Acct #" HeaderText="PMT Acct #" ItemStyle-CssClass="Col" />
                                </Columns>
                                <AlternatingRowStyle HorizontalAlign="Center" VerticalAlign="Middle" CssClass="grdSchHis" />

                            </asp:GridView>
                        </td>

                    </tr>
                    <tr>
                        <td>
                            <div id="paging" class="paging"></div>
                        </td>
                    </tr>
                </table>

            </asp:Panel>
        </div>
        <asp:Panel runat="server" ID="plcErrorSchHistory" Width="700px">
            <div id="divErr" runat="server" style="text-align: left">
                <asp:Label ID="lblErrorSchHistory" runat="server" CssClass="errorSch" ClientIDMode="Static"></asp:Label>
            </div>
            <%--Added below label to apply CSS for Paging in History section--%>
            <asp:Label ID="lblPagingText" runat="server" CssClass="PagingText" ClientIDMode="Static"></asp:Label>
            <%--Added below label to apply CSS for Paging in History section--%>
            <asp:Label ID="lblPage" runat="server" CssClass="PagingText" ClientIDMode="Static"></asp:Label>

            <%--Added the below Div to fix the Double click issue in Scheduld Payments section--%>
        </asp:Panel>

        <div id="divScroll" style="overflow: auto; width: 100%; border-top-style: none; border-right-style: none; border-left-style: none; height: 200px; border-bottom-style: none"
            onscroll="setScrollPos();">
        </div>

        <%--Added the below Div to fix the Double click issue in Scheduld Payments section--%>
    </form>
</body>
</html>
