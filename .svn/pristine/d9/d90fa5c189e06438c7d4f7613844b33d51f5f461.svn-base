<!--#include file="common.asp"-->
<!--#include file="recordsetclass.asp"-->
<!--#include file="../navigation/navigation.asp"-->
<!--#include file="webreference.asp"-->
<%
CheckAccess
'This include contains subs that generates the HTML for the header part 
'and the footer of the HTML document, including the banners for the site.  

sub header(title)
Response.CacheControl="Public"
Scripts.addScript "var iFrames=" & lcase(iFrames) & ";"
if title = "" then title = "" 'The displayed title of the page
'if Authentication.loggedIn() then currentUser=Authentication.User.UserID
Scripts.addFile siteRoot & "shared/framecheck.js"
Scripts.addStyleSheet(siteRoot&"shared/style.css")
NavigationScripts()
%>
<html>
<head>
<%=Scripts.list()%>
<%if title<>"" then%><title><%=stripTitleTags(title)%></title><%end if%>
<meta http-equiv="Pragma" content="no-cache"></meta>
</head>

<body <%if iFrames then%>scroll="no" <%end if%>onload="default_window_onload()">
<!-- document header -->
<!-- #include file="h.aspx"-->
<form name="navForm" method="post" ><input type=hidden name=dummy></form>
<table width="750"><tr><td>
<!-- Page content starts here -->
<%end sub

sub footer()%>
<!-- Page content ends here -->
</td></tr>
</table>
</body>
</html>
<%
ADO.free()
end sub%>


<script language=javascript runat=server>
forPrint=false, showTitle=true, width=600, height=getGlobal("height");
// returns the name of the application (virtual siteRoot name)
// Returns true if the current URL==File
function isPage(File) {
	return ((new String(Request.ServerVariables("URL"))).toLowerCase()==ResolveUrl(File).toLowerCase());
}
//Returns a link if the current URL is not URL, otherwise a grey string.
function Link(URL, Text) {
	return Link1(URL, Text, !isPage(URL));
}
//Returns a link if AsLink, otherwise a grey string
function Link1(URL, Text, AsLink) {
	var result = (AsLink)?("<a href=\"" + ResolveUrl(URL) + "\" class=\"arial_11_bold_white\">"):"<font color=\"#666666\">";
	result += Text;
	result += (AsLink)?"</a>":"</font>";
	return result;
}

function ResolveUrl(URL) {
	return URL.replace(/\.\.\//,siteRoot);
}

function CheckAccess() {
	 //External URL Implementation-Modified the below code in order to make the site work from both Internal and External URL as a part of 'Virtual Directory renaming' Approach 

    var S = new WebReference("/Paymenttoolssc/navigation/service.asmx");
	var Result = S.sendRequest("CheckAccess", ["Url",(new String(Request.ServerVariables("URL"))).toString()],["EncryptedTicket",(new String(Request.Cookies(".ASPXAUTH"))).toString()]);
	if (!eval(Result.Authorized)) Response.Redirect(Result.RedirectUrl);
}

</SCRIPT>
