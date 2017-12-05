<!--#include file="savestate.asp"-->
<%if Application("siteRoot") = "" then getStartup()
Scripts.addFile siteRoot & "shared/common.js"
Scripts.addFile siteRoot & "shared/chainEvents.js"
Scripts.addFile siteRoot & "shared/formElementEvents.js"
Scripts.addScript "var appName='" & Replace(getAppName(),"-","%2D") & "', siteRoot='" & siteRoot & "';"
Scripts.addFile siteRoot & "shared/formEvents.js"
%>

<script language=javascript runat=server>
var isFrames=getGlobal('isFrames'), iFrames = false, scriptName = Request.ServerVariables("SCRIPT_NAME");
if (isFrames=='') {isFrames=false} else eval(isFrames.toLowerCase());
// returns the name of the application (virtual siteRoot name)
function browserMatch(s) {
// Returns an adjusted value for s based on the browser & font family
// used in class TextBox.  Used to insure matching textbox sizes between
// IE & Netscape if variable pitched font-face used for these inputs
	if (!(new String(Request.ServerVariables("HTTP_USER_AGENT")).indexOf('MSIE') > 0)) {
			s = s * 0.65;
	}
	return s;
}

function getAppName() {
	if (Application("siteRoot") != "/") {
		var result = new String(Request.ServerVariables("SCRIPT_NAME"));
		result = result.substring(1,result.indexOf("/",1));
	} else result = "siteRoot";
	return result;
}

var siteRoot = Application("siteRoot")
var ns = ((new String(Request.ServerVariables("HTTP_USER_AGENT"))).indexOf('MSIE') == -1)
// returns a string with critical chars escaped to allow exporting to client
function safeString(st) {
	return (typeof(st) != 'string')?'':
		"'" + st.replace(/\\/g,'\\\\').replace(/'/g,"\\'").replace(/\r/g,"\\r").
			replace(/\n/g,"\\n").replace(/\</g,'<\'+\'') + "'";
}

// remove any HTML tags from st.  Allows displayed title to be used also
// for <title> tag
function stripTitleTags(st) {
	return st.replace(/<[^>]*>/g,' ');
}

// Scripts_Class defines a class for an object that allows include files to provide
// client side javascript that appears in the document head rather than randomly
// (especially before the HTML tag!)
// The object is instantiated as Scripts
// Two methods are provided:
// addFile(url) where URL is the URL of a .js file
// addScript(s) where s is the text of the script.
function Scripts_Class() {
	this.scriptElements=new Array();
	this.scriptFiles=new Array();
	this.styleSheets=new Array();
	this.addFile = function(url) {
		this.scriptFiles[this.scriptFiles.length] = url;
	}
	this.addScript = function(s) {
		this.scriptElements[this.scriptElements.length] = '\t'+s.toString().replace(/\r\n}/g,'\r\n\t}').replace(/\r\n([^\t])/g,'\r\n\t\t$1');
	}
	this.addStyleSheet = function(url,id) {
		if (arguments.length==1) var id = null;
		this.styleSheets[this.styleSheets.length] = [url,id];
	}
	this.list = function() {
		var st='';
		if (this.scriptElements.length>0) {
			st+='<SC'+'RIPT type="text/Javascript">\r\n';
			for (var i=0; i<this.scriptElements.length; i++)
				st+=this.scriptElements[i]+'\r\n\r\n';
			st+='</S'+'CRIPT>\r\n';
		}
		for (var i=0; i<this.scriptFiles.length; i++)
				st+='<SC'+'RIPT type="text/Javascript" src="'+this.scriptFiles[i]+'"></S'+'CRIPT>\r\n';
		for (var i=0; i<this.styleSheets.length; i++)
				st+='<link rel="stylesheet" type="text/css" href="'+this.styleSheets[i][0]+((this.styleSheets[i][1]!=null)?('" id="'+this.styleSheets[i][1]):'')+'">\r\n';
		return st;
	}
}
var Scripts = new Scripts_Class();

function getStartup() {
	eval(Application("Startup"));
	Startup.start();
	siteRoot = Application("siteRoot");
}
var agent = new String(Request.ServerVariables("HTTP_USER_AGENT")); 
var browser='', browserVer=0, browserPlatform='other';

if (agent.indexOf('compatible; MSIE')>0) {
	browser = 'ie';
	browserVer = parseFloat(agent.replace(/.*MSIE\s*(\d+\.\d*).*/,'$1'));
	if (agent.indexOf('Windows NT')>0 || agent.indexOf('Windows XP')>0) {
		browserPlatform = 'NT';
	} else if (agent.indexOf('Windows 9')>0 || agent.indexOf('Windows M')>0) {
		browserPlatform = '9X';
	} 
} else if (agent.indexOf('Mozilla')!=-1) {
	if (agent.indexOf('Netscape')>0) {
		browser = 'ns';
		browserVer = parseFloat(agent.replace(/.*(\d+\.\d*).*/,'$1'));
	} 
}

</SCRIPT>
