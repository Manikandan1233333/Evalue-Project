<!-- MAIG - CH1 - Modified the verbiage by removing the Sales & Service -->
<!-- MAIG - CH2 - Modified the alignment and logo -->
<!-- MAIG - CH3 - Commented the below tag -->
<!--JQuery Upgrade Changes- 05/29/2017-->
<%@ Page CodeBehind="logout.aspx.cs" Language="c#" AutoEventWireup="True" Inherits="MSCR.Forms.logout" %>
<html>
<head>
	<!-- MAIG - CH1 - BEGIN - Modified the verbiage by removing the Sales & Service -->
    <title>Payment Tool</title>
	<!-- MAIG - CH1 - END - Modified the verbiage by removing the Sales & Service -->
    <%--CHG0121437 - SSO Integration - CH1 - Added the below code to redirect to logout page when the back button is clicked.--%>
<%--CHGxxxx- JQuery Upgrade Changes- 05/29/2017- start--%>
    <script src="../Scripts/jquery-3.2.1.min.js"></script>
<%--CHGxxxx- JQuery Upgrade Changes- 05/29/2017- end--%>

    <script type="text/javascript">

        // Method added to do post request to Login page and pass SLO value in case of PSS USer
  
        function SLOCall() {
           
            var slourldata = $('#hdnLogoutType').val();
            if (slourldata != '') {
                window.location.assign(slourldata);
            }
            else {
                // slourldata = location.protocol + "//" + location.host + "/PaymentToolmscr/Forms/login.aspx";
                slourldata = "/PaymentToolmscr/Forms/login.aspx";
                window.location.assign(slourldata);
                
            }
        }

</script>

<style type="text/css">
        .style2
        {
            height: 56px;
            font-weight: bold;
        }

        .style4 {
            width: 267px;
        }

        .style5 {
            height: 56px;
            font-weight: bold;
            width: 267px;
        }

        .style6 {
        }

        .style8 {
            width: 155px;
        }

        .style9 {
            height: 56px;
            font-weight: bold;
            width: 155px;
        }

        .style10 {
            color: #66CCFF;
            font-weight: bold;
        }

        .style11 {
            width: 155px;
            color: #66CCFF;
            font-weight: bold;
        }

        .style12 {
            font-weight: bold;
        }

        .style15 {
            width: 769px;
        }

        .style16 {
            color: #0033CC;
            font-weight: bold;
        }

        .style17 {
            width: 155px;
            color: #0033CC;
            font-weight: bold;
        }
    </style>

</head>
<BODY>
    <link rel="stylesheet" type="text/css" href="../style.css">
    <form id="frmLogin" name="form1" method="post" runat="server">
    <center>
        <!-- header -->
        <table cellspacing="0" cellpadding="0" width="750" border="0">
            <tr>
                <td width="64">
                </td>
				<!-- MAIG - CH2 - BEGIN - Modified the alignment and logo -->
                <td width="420" bgcolor="#000099" height="59">
                    <img height="59" src="../images/ss_logo.gif" width="420">
                </td>
                <td width="324" bgcolor="#000099" height="59">
                </td>
				<!-- MAIG - CH2 - END - Modified the alignment and logo -->
            </tr>
        </table>
        <!-- end of header -->
        <table cellspacing="0" cellpadding="0" width="750" border="0">
            <tr>
                <td class="style11">
                    &nbsp;
                </td>
                <td class="style6" colspan="3" rowspan="2">
                    <span class="style10">
                        <!--9/24/2012- Changes added as a part of SSO Integration :Added a link to Sign in again-->
                    </span><a class="style12"></a><span class="style10">
						<!-- MAIG - CH3 - BEGIN - Commented the below tag -->
                        <%--<center>
                            <img height="29" src="../images/ss_title.gif" width="381"></center>--%>
						<!-- MAIG - CH3 - END - Commented the below tag -->
                </td>
                </span> </td> </td>
                <!--Redirect to login.aspx and the flow continues.-->
                <!--Please click <a href="../default.aspx"> here </a>	 to Sign in again</td>-->
                </td>
            </tr>
            <tr>
                <td class="style11">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style8">
                    &nbsp;
                </td>
                <td class="style6" colspan="3" rowspan="2">
                    <table cellspacing="0" cellpadding="0" width="750" border="0">
                        <tr>
                            <td class="style17">
                                &nbsp;
                            </td>
                               <td class="style6" rowspan="2">
                                <span class="style16">
                                    <!--9/24/2012- Changes added as a part of SSO Integration :Added a link to Sign in again-->
                                </span><a class="style12"></a><span class="style16">
                                    <br />
                                    <br />
                                     <asp:Label ID="lblErrMessage" runat="server" ></asp:Label>
</span>
                            </td>
                        </tr>
                    </table>
                </td>
                  
            </tr>
            <tr>
                <td class="style8">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td class="style9">
                    &nbsp;
                </td>
                <td >
                    
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;<asp:PlaceHolder 
                        ID="plc_SignInAgain" runat="server">
                        Please click&nbsp;  
                       <a href="#" onclick="SLOCall();">here</a>
                         to Sign in again<br /></asp:PlaceHolder>
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                   
                    <br />
                    <br />
                </td>
                
                <td class="style5">
                    &nbsp;
                </td>
                <td class="style2">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="40" class="style8">
                    &nbsp;
                   
                </td>
                <td height="40" class="style15">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <b>For Technical or Application Support
                        <br />
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</b>
                    1-877-554-2911
                </td>
                <td height="40" class="style4">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td height="40" class="style8">
                    &nbsp;
                </td>
                <td height="40" class="style15">
                    &nbsp;
                </td>
                <td height="40" class="style4">
                    &nbsp;
                </td>
                <td height="40">
                    &nbsp;
                </td>
            </tr>
        </table>
        <%--CHG0121437 - SSO Integration - CH1 - Added the below code to redirect to logout page when the back button is clicked - Start--%>
        <input type="hidden" id="hdnLogoutType" runat ="server" />
         <%--CHG0121437 - SSO Integration - CH1 - Added the below code to redirect to logout page when the back button is clicked - End--%>
        <!-- 179, 393, 178 -->
    </center>
    </form>
</body>
</html>
