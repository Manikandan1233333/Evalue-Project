<%--JQuery Upgrade Changes- 05/29/2017--%>
<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CommonMenu.ascx.cs"
    Inherits="SSC.Shared.CommonMenu" %>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <%--CHGxxxx- JQuery Upgrade Changes- 05/29/2017- start--%>
    <script src="../Scripts/jquery-3.2.1.min.js"></script>
    <%--CHGxxxx- JQuery Upgrade Changes- 05/29/2017- end--%>

    <script type="text/javascript">
        $(document).ready(function() {
                var tab = $('#header div a[href^="' + window.location.pathname + '"]');
                $($('#header > table > tbody > tr').children()[($(tab).parents('div').index() - 2) * 2]).find("td").css('background-color', 'Darkgray');
                //$($('#header > table > tbody > tr').children()).find("td").css('width', '100px');
                //$('#header > table').css('width', 'auto');
                //$('#header > table > tbody > tr').append('<td>Hello</td>').css('border', 'solid 1px Black').css('background-color', '#cccccc');
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
            width:100px;
        }
        #header table tbody tr td table tbody tr td a
        {
            z-index: 1;
            text-transform: uppercase;
            font-size: 11px;
            font-weight: bold;
            color: Black;
        }
        #header table{width:auto;}
        #header table tbody tr td table tbody tr td:hover a
        {
            color: Gray;
        }
        #header div
        {
            width: 140px;
        }
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
            width: 100%;
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
            StaticEnableDefaultPopOutImage="false" 
            onmenuitemclick="aspMenu_MenuItemClick">
        </asp:Menu>
    </div>
    </form>

</body>
</html>
