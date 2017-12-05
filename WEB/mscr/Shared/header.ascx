<!-- MAIG - CH1 - Commented the static keyword -->
<%@ Control Language="c#" AutoEventWireup="false" Inherits="System.Web.UI.UserControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import namespace="CSAAWeb.WebControls"%>
<script runat="Server">
//MAIG - CH1 - BEGIN - Commented the static keyword
//static string Head = "";
string Head = "";
//MAIG - CH1 - END - Commented the static keyword
</script>
<meta http-equiv="Pragma" content="no-cache"></meta>
 <!-- External URL Implementation-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->

<%if (Head=="") Head=Renderer.Render("/Paymenttoolssc/shared/header.aspx", Page);%>
<%=Head%>


