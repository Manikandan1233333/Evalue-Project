/*	HISTORY: 
*  REVISION HISTORY:
*	MODIFIED BY COGNIZANT
*PUP CH1:02/25/2011 Added this if condition to disable focus while checking for the disable dropdown in Insurance payment screen by cognizant.
*67811A0  - PCI Remediation for Payment systems :Commented this line to prevent the setting of focus to the Policy number text box by cognizant on 10/03/2011
* CHG0078293 - PT enhancement CH1 - Added validations for Amount Textbox in Transaction search page.
* MAIG - CH1 - Modified and added javascript method to support all browsers and extended validations
* MAIG - CH2 - Commented the code to skip the Javascript menu
*/
var ns = (navigator.appName.indexOf('Microsoft') == -1)
var isMac = (navigator.appVersion.indexOf("Mac") != -1);

// Attempts to navigation to url via an http post, using either menuForm or thisForm, and returns 
// false if so cancelling any default link action.  If neither form can be found, returns true, 
// allowing a default link action to occur.
function post(url) {
    if (typeof (url) == 'object' && typeof (url.href) == 'string') url = url.href;
    var form = document.menuForm;
    if (typeof (form) == 'undefined') form = document.thisForm;
    if (typeof (form) == 'undefined') return true;
    form.action = url;
    for (var i = 1; i < arguments.length; i++)
        eval('form.' + arguments[i].replace(/=/, '.value='));
    form.submit();
    return false;
}

// locates the correct frame gotoURL function and calls it, or sets the location.href to new
// value if not found.
function get(URL) {
    var form;
    if (self == top && gotoURL) {
        gotoURL(URL);
        return;
    } else if (top.gotoURL) {
        top.gotoURL(URL);
        return;
    } else for (var i = 0; i < top.frames.length; i++)
        if (top.frames[i].gotoURL) {
        top.frames[i].gotoURL(URL)
        return;
    }
    location.href = URL;
}

// Used as keypressed event for any textboxes that only numeric entry is allowed.
// MAIG - CH1 - BEGIN - Modified and added javascript method to support all browsers and extended validations
function allow_only_onkeypress(allow,event) {
    if (navigator.appName.indexOf("Netscape") == -1) {
        var ch = String.fromCharCode(event.keyCode);
        event.returnValue = (allow.indexOf(ch) >= 0);    ///// works for IE & Chrome
    }
    else {
        var key = event.keyCode || event.which;
        var ch = String.fromCharCode(key);
        
        if (!event.shiftKey) {
            if (key == 8 || key == 46 || key == 36 || key == 37 || key == 39 || key == 35 || key == 9 || key == 20) { return true; }
        }
        return (allow.indexOf(ch) >= 0);
    }
}

function digits_only_onkeypress(event) {
    return allow_only_onkeypress("0123456789\r",event);
}

function alphanumeric_onkeypress(event) {
    return allow_only_onkeypress("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz1234567890\r", event);
}

function alphabets_onkeypress(event) {
    return allow_only_onkeypress("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz,.*'- \r", event);
}

function alphabetsonly_onkeypress(event) {
    return allow_only_onkeypress("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz'-,. \r", event);
}

// CHG0078293 - PT enhancement CH1 - START - Added validations for Amount Textbox in Transaction search page.
function decimalnumeric_only_onkeypress(event) {
    return allow_only_onkeypress(".0123456789\r",event);
}

function email_Address_onkeypress(event) {
    return allow_only_onkeypress("ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!#$%&'*+-/=?^_`{|}~.@\r", event);
}
// MAIG - CH1 - END - Modified and added javascript method to support all browsers and extended validations
// CHG0078293 - PT enhancement CH1 - END - Added validations for Amount Textbox in Transaction search page.

function default_window_onload() {
    if (typeof (window_onload) == 'function') window_onload();
    focus_element(document.forms[0]);
    // MAIG - CH2 - BEGIN - Commented the code to skip the Javascript menu
    //if (Menus) Menus.initialize();
    // MAIG - CH2 - END - Commented the code to skip the Javascript menu
    if (typeof (trapAllFormBlurs) == 'function') trapAllFormBlurs();
}

function focus_element(form) {
    if (form == null) return;
    var FirstText, el;
    for (var i = 0; i < form.elements.length; i++) {
        el = form.elements[i];
        if (el.invalid != null) break;
        if (FirstText == null && el.type == 'text' && !el.disabled) FirstText = el;
        el = null;
    }
    if (el != null) {
        //PUP CH1 Added this if condition to disable focus while checking for the disable dropdown in Insurance payment screen by cognizant on 02/25/2011.
        if (el.type != "hidden" && el.style.display != "none" && !el.disabled) {
            el.focus();
        }
    }
    //67811A0  - PCI Remediation for Payment systems :Start commented this line to prevent the setting of focus to the Policy number text box by cognizant on 10/03/2011
    //else if (FirstText!=null) FirstText.focus();
    //67811A0  - PCI Remediation for Payment systems :End commented this line to prevent the setting of focus to the Policy number text box by cognizant on 10/03/2011
}
