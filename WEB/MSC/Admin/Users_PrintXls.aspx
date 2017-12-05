<!--1/31/2007 STAR Retrofit II Created as a part of CSR#5166. New page to display Users List in Excel or HTML view.-->
<%--CHG0109406 - CH1 - Appended the timezone Arizona along with the Date Field--%>
<%@ Page language="c#" Codebehind="Users_PrintXls.aspx.cs" AutoEventWireup="True" Inherits="MSC.Admin.Users_PrintXls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN"> 
<HTML>
	<HEAD>
		<title>Users List</title>
		      <!-- External URL Implementation-Modified the below  code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
		      <LINK href="../../PaymentToolmscr/style.css" type="text/css" rel="stylesheet">
		<!-- PT.Ch1 - START: Added by COGNIZANT - 7/01/2008 - To enable default printing of the Print Version -->
		<script language="javascript">
		var strquery = window.location.search.substring(1);
		var strparams = strquery.split('&');
		for(var i=0;i<strparams.length;i++)
		{
		  var strkval = strparams[i].split('=');
		  if(strkval[0] == "type" && strkval[1] == "PRINT")
		  {
		  	window.print();
		  }
		}
		</script>
		<!-- PT.Ch1 - END -->
	</HEAD>
	<body leftmargin="15">
		<form id="Users_PrintXls" method="post" runat="server">
			<table id="tblHeading" cellSpacing="0" cellPadding="0" width="750" border="0">
				<tr>
					<td class="arial_11_bold_white" width="604" colSpan="2">
						<table width="100%" border="0">
							<tr height="41">
								<td class="arial_12_bold" align="left">     
								 <!-- External URL Implementation-Modified the below  code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->
								<IMG src="../../PaymentToolmscr/images/msc_logo_sm.gif" width="604" border="0">
								</td>
							</tr>
							<tr>
								<td height="10">&nbsp;</td>
							</tr>
							<tr>
								<td class="arial_11_bold_white" width="604" bgColor="#333333" colSpan="2" height="22">&nbsp;&nbsp;Manage 
									Existing Users
								</td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="3" height="10"></td>
				</tr>
				<tr>
					<td colSpan="2">
						<table border="0">
							<tr>
								<td class="arial_12_bold">&nbsp;&nbsp;&nbsp;Application :</td>
								<td align="left"><asp:label id="lblAppname" runat="server" CssClass="arial_12">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td class="arial_12_bold">&nbsp;&nbsp;&nbsp;Rep DO :</td>
								<td align="left"><asp:label id="lblRepdo" runat="server" CssClass="arial_12">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td class="arial_12_bold">&nbsp;&nbsp;&nbsp;Status :</td>
								<td align="left"><asp:label id="lblStatus" runat="server" CssClass="arial_12">&nbsp;</asp:label></td>
							</tr>
							<tr>
								<td class="arial_12_bold">&nbsp;&nbsp;&nbsp;User(s):</td>
								<td align="left"><asp:label id="lblUsers" runat="server" CssClass="arial_12">&nbsp;</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
				<tr>
					<td colSpan="2" height="10"></td>
				</tr>
			</table>
			<table id="tblRepeater" cellSpacing="0" cellPadding="0" width="500" bgColor="#ffffff" border="0" runat="server">
				<tr>
					<td colSpan="5">
						<table id="tblInnerRepeater" style="BORDER-COLLAPSE: collapse" cellSpacing="0" cellPadding="2" width="500" border="1">
							<asp:repeater id="rptUserRepeater" runat="server" OnItemDataBound="rptUserRepeater_ItemBound">
								<HeaderTemplate>
									<tr class="item_template_header_white">
										<td class="arial_11_bold_white" bgColor="#333333">Name</td>
										<td class="arial_11_bold_white" bgColor="#333333">User ID</td>
										<td class="arial_11_bold_white" bgColor="#333333">User Type</td>
										<td class="arial_11_bold_white" bgColor="#333333">Status</td>
										<%--CHG0109406 - CH1 - BEGIN - Appended the timezone Arizona along with the Date Field--%>
										<td class="arial_11_bold_white" bgColor="#333333">Last Login Date (Arizona)</td>
										<%--CHG0109406 - CH1 - END - Appended the timezone Arizona along with the Date Field--%>
									</tr>
								</HeaderTemplate>
								<ItemTemplate>
									<tr id="trItemRow" runat="server">
										<td class="arial_12">
											<asp:Label ID="lblName" Runat="server" text = '<%# DataBinder.Eval(Container.DataItem, "Name") %>' >
											</asp:Label>
										</td>
										<td class="arial_12">
											<asp:Label ID="lblUserId" Runat="server" text = '<%#DataBinder.Eval(Container.DataItem, "UserId")%>' >
											</asp:Label>
										</td>
										<td class="arial_12">
											<asp:Label ID="lblUserType" Runat="server" text = '<%# CSAAWeb.WebControls.DataBinder.Eval(Container.DataItem, "Description", ", ", "Roles") %>' >
											</asp:Label>
										</td>
										<td class="arial_12">
											<asp:Label ID="lblUserStatus" Runat="server" text = '<%#DataBinder.Eval(Container.DataItem, "Status")%>' >
											</asp:Label>
										</td>
										<td class="arial_12">
											<asp:Label ID="lblLastLogin" Runat="server" text = '<%#DataBinder.Eval(Container.DataItem, "Last_Login_Date","{0:MM/dd/yyyy   hh:mm:ss tt}")%>' >
											</asp:Label>
										</td>
									</tr>
								</ItemTemplate>
								<FooterTemplate>
									<tr>
										<td bgColor="#cccccc" colSpan="5" height="14">&nbsp;
										</td>
									</tr>
								</FooterTemplate>
							</asp:repeater></table>
					</td>
				</tr>
			</table>
		</form>
	</body>
</HTML>
