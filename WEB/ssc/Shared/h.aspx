<!-- History
* RFC 185138 - AD Integration CH1 - Commented the Change password link 
* MAIG - CH1 - Added the code to get the Menu details from an user control
* MAIG - CH2 - Added the code to invoke the user control
-->
<!-- header -->
        <%--MAIG - CH1 - BEGIN - Added the code to get the Menu details from an user control--%>
<%@ Register TagPrefix="MenuControl" Src="~/Shared/CommonMenu.ascx" TagName="CommonMenu" %>
        <%--MAIG - CH1 - END - Added the code to get the Menu details from an user control--%>
<head>
    <meta http-equiv="Pragma" content="no-cache"></meta>
</head>
<center>
    <table cellspacing="0" cellpadding="0" width="750" bgcolor="#000099" border="0">
        <tr>
            <td width="420" bgcolor="#000099" height="59">
                <!-- External URL Implementation-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
                <img height="59" src="<%=ResolveUrl("/PaymentToolssc/images/ss_logo.gif")%>" width="420"
                    align="left">
            </td>
            <td width="324">
                <table height="59" cellspacing="0" cellpadding="0" width="324" border="0">
                    <tr>
                        <td height="5">
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" align="right">
                           
                            <!--External URL Implementation-Modified the below  code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach -->
                            <%=Link("/PaymentToolmscr/Public/admin_contact.aspx", "Contact Administrator")%>
                            <!--RFC 185138 - AD Integration CH1 Start - Commented the Change password link -->
                            <!--font color="#cccccc" size="1"><b>|</b></font>
							<%=Link("/PaymentToolmscr/Forms/password_change.aspx", "Change Password")%>-->
                            <!--RFC 185138 - AD Integration CH1 End - Commented the Change password link -->
                            <font color="#cccccc" size="1"><b>|</b></font>
                            <!--External URL Implementation-Modified the below  code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach -->
                            <%=Link("/PaymentToolmscr/Forms/logout.aspx", "Logout")%>&nbsp;&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <!-- START - Modified the link InsSearchOrders_New.aspx to InsSearchOrders.aspx for Transaction Search -->
                        <!-- Q1-Retrofit.ch1: START - Modified the link InsSearchOrders.aspx to InsSearchOrders_New.aspx for Transaction Search as part of CSR #4833-->
                        <!--				     External URL Implementation-Modified the below  code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach -->
                        <td align="right">
                            <%=Link("/PaymentToolmscr/Reports/InsSearchOrders_New.aspx", ">> Transaction Search")%>
                            <!-- Q1-Retrofit.ch1: END -->
                            <!-- END - Modified the link InsSearchOrders_New.aspx to InsSearchOrders.aspx for Transaction Search -->
                            &nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
                <%--MAIG - CH2 - BEGIN - Added the code to invoke the user control--%>
        <tr>
            <td colspan="2" style="padding-top: 5px; background-color: White;">
                <MenuControl:CommonMenu ID="CommonMenu1" runat="server" />
            </td>
        </tr>

    </table>
<%--	<table height="23" cellspacing="0" cellpadding="0" width="750" border="0">
		<tr>
			<td height="2"></td>
		</tr>
		<tr>
			<td>
				<table border="0" align=left cellpadding="0" cellspacing="0">
					<tr><td id=mainMenu></td></tr>
				</table>
				<table align=right border="0" cellpadding="0" cellspacing="0">
					<tr>
						<td><img src="<%=ResolveUrl("../navigation/images/pixel.gif")%>" width="2px"></td>
						<td class="MenuFiller" width="100%">&nbsp;</td>
					</tr>
				</table>
			</td>
		</tr>
	</table>--%>
<!-- End header -->
        <%--MAIG - CH2 - END - Added the code to invoke the user control--%>
