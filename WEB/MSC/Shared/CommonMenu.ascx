<%--CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES--%>
<%--JQuery Upgrade Changes- 05/29/2017--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonMenu.ascx.cs"
    Inherits="MSC.Shared.CommonMenu" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">

<%--CHGxxxx- JQuery Upgrade Changes- 05/29/2017- start--%>
    <script src="../Scripts/jquery-3.2.1.min.js"></script>
<%--CHGxxxx- JQuery Upgrade Changes- 05/29/2017- end--%>

    <script src="../Scripts/jquery.cookie.js" type="text/javascript"></script>

    <%--<script src="http://crypto-js.googlecode.com/svn/tags/3.1.2/build/rollups/sha3.js" type="text/javascript"></script>--%>

    <script type="text/javascript">
        $(document).ready(function() {
            //var url = window.location.pathname + window.location.search;
            var tab = $('#header div a[href^="' + window.location.pathname + '"]');
            $($('#header > table > tbody > tr').children()[($(tab).parents('div').index() - 2) * 2]).find("td").css('background-color', 'Darkgray');
            //$($('#header > table > tbody > tr').children()).find("td").css('width', '100px');
            //            $('#header > table > tbody > tr').append('<td>Hello</td>').css('border', 'solid 1px Black').css('background-color', '#cccccc');
        });
        $(document).ready(function() {
            //var result = $('#header div a[href^="/PaymentToolmsc/forms/newinsurance.aspx"]');
            if ($('#header div a[href^="/PaymentToolmsc/forms/newinsurance.aspx"]').length > 0) {
                $('#header div a[href^="/PaymentToolmsc/forms/newinsurance.aspx"]').click(function() {
                    //alert(this.childNodes[0].data);
                    //                        $.cookie("pn", this.childNodes[0].data);
//                    var date = new Date();
//                    var minutes = 15;
//                    date.setTime(date.getTime() + (minutes * 60 * 1000));
                    if (this.childNodes[0].data == "Down Payment") {
                        //var hash = CryptoJS.SHA3("true", { outputLength: 512 });
                        $.cookie('IsDown', 'true', {secure:true , path: '/' });
                    } else if (this.childNodes[0].data == "Installment Payment") {
                        $.cookie('IsDown', 'false', { secure: true, path: '/' });
                    }
                });
            }

        });


       
    </script>

    <style type="text/css">
        #header
        {
            text-decoration: none;
            font-weight: bold;
            font-family: arial;
            color: Black;
            background-color: #cccccc;
        }
        #header table tbody tr td table tbody tr td
        {
            border: solid 1px Black;
            background-color: #cccccc;
            text-align: center;
            padding: 3px 0px 3px 0px;
            width: 100px;
        }
        #header table tbody tr td table tbody tr td a
        {
            z-index: 1;
            text-transform: uppercase;
            font-size: 11px;
            font-weight: bold;
            color: Black;
        }
        #header table tbody tr td table tbody tr td:hover a
        {
            color: Gray;
        }
        #header table
        {
            width: auto;
        }
        /*CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Updated width for OTSP change*/
        #header div
        {
            width: 155px;
        }
         /*CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Updated width for OTSP change*/
        #header div table
        {
            width: 100%;
        }
        #header div table tbody tr td table tbody tr td
        {
            text-align: left;
            border-left-width: 0;
            border-top-width: 0;
            padding: 0px 0px 0px 0px;
        }
        #header div table tbody tr td table tbody tr td a
        {
            text-transform: none;
            color: Black;
            display: block;
            font-size: 11px;
            font-weight: bold;
           /*CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Updated width for OTSP change*/
            width: 155px;
            /*CHG0131014 - OTSP/SUBRO/PRB0047380/SECURITY FIXES - Updated width for OTSP change*/
            height: 19px;
            padding: 3px 0px 0px 3px;
        }
        #header div table tbody tr td table tbody tr td a:hover
        {
            background-color: Darkblue;
            color: White;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div id="header">
        <asp:Menu ID="aspMenu" Width="100%" runat="server" autopostback="true" Orientation="Horizontal"
            StaticEnableDefaultPopOutImage="false" OnMenuItemClick="aspMenu_MenuItemClick">
        </asp:Menu>
    </div>
    </form>
</body>
</html>
