<%@ Page Language="c#" AutoEventWireup="false" Inherits="System.Web.UI.Page" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<!--#include file="h.aspx"-->
<script runat="Server">
// Returns true if the current URL==File
bool isPage(string File) {
	return (Request.ServerVariables["URL"].ToLower()==ResolveUrl(File).ToLower());
}
//Returns a link if the current URL is not URL, otherwise a grey string.
string Link(string URL, string Text) {
	return Link(URL, Text, true/*!isPage(URL)*/);
}
//Returns a link if AsLink, otherwise a grey string
string Link(string URL, string Text, bool AsLink) {
	string result = (AsLink)?("<a href=\"" + ResolveUrl(URL) + "\" class=\"nav_rollover\">"):"<font color=\"#666666\">";
	result += Text;
	result += (AsLink)?"</a>":"</font>";
	return result;
}
</script>
