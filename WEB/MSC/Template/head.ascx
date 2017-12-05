<!-- MAIG - CH1 - Commeneted the below line to skip the Javascript Menu load -->
<%@ Control Language="c#" AutoEventWireup="false" Inherits="System.Web.UI.UserControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<%@ Import namespace="CSAAWeb.WebControls"%>
<%@ Register tagprefix="Nav" Namespace="CSAAWeb.Navigation" assembly="CSAAWeb"%>
 <!-- External URL Implementation-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  -->

<Nav:ACL runat="Server" ServicePath="/Paymenttoolssc/navigation/"/>
<script runat="server">
static string Nav = "";
static string Head = "";
</script>
<%
 //External URL Implementation-START-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  
//MAIG - CH1 - BEGIN - Commeneted the below line to skip the Javascript Menu load
//if (Nav=="") Nav=Renderer.Render("/Paymenttoolssc/Navigation/Navigation.aspx", Page);
//MAIG - CH1 - END - Commeneted the below line to skip the Javascript Menu load
if (Head=="") Head=Renderer.Render("/Paymenttoolssc/shared/head.aspx", Page);
    
// External URL Implementation-END-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach  

%>
<%=Nav%>
<%=Head%>
