/*
This file contains functions that are commonly used as event functions for form elements, 
and forms.
* MAIG - CH1 - Modified and added javascript method to support all browsers and extended validations
*/

// general button onclick function, sets forms Submit value to the button's
// value & submits.  If check is true, first determines if there have been changes to
// the form and confirms action prior to executing.
function button_onclick(b, check) {
	if (!check || !checkChanged(b) || 
		confirm('You have unsaved changes that will be lost, continue?')) {
		if (typeof(b.form.onsubmit)!='function' || b.form.onsubmit()) {
			b.form.Submit.value = b.value;
			if (b.type=='button') b.disabled=true;
			b.form.submit();
		}
	}
}

// Cancel button onclick function, checks for form changes and confirms if exist
// then returns to prior mode
function Cancel_onclick(b) {
	if (!checkChanged(b) || confirm('You have unsaved changes that will be lost, continue?')) {
		b.form.Submit.value=(typeof(b.form.Prior_Submit.value)=='object')?b.form.Prior_Submit.value:'';
		if (b.type=='button') b.disabled=true;
		b.form.submit();
	}
}

// Action for a default add button.
function Add_onclick(b) {
	b.form.Submit.value = "Add"
	if (b.type=='button') b.disabled=true;
	b.form.submit();
}

// Action for a default delete button, includes a confirmation popup.
function Delete_onclick(b) {
	if (confirm('Are you sure? This action cannot be undone.')) {
		b.form.Submit.value = b.value;
		if (b.type=='button') b.disabled=true;
		b.form.submit();
	}
}

// Used as keypressed event for any textboxes that only numeric entry is allowed.
// MAIG - CH1 - BEGIN - Modified and added javascript method to support all browsers and extended validations
function allow_only_onkeypress(allow, event) {
    if (navigator.appName.indexOf("Netscape") == -1) {
        var ch = String.fromCharCode(event.keyCode);
        event.returnValue = (allow.indexOf(ch) >= 0);    ///// works for IE & Chrome
    }
    else {
        var key = event.keyCode || event.which;
        var ch = String.fromCharCode(key);
        if (key == 8) { return true; }
        return (allow.indexOf(ch) >= 0);
    }
}

function digits_only_onkeypress(event) {
    return allow_only_onkeypress("0123456789\r", event);
}
// MAIG - CH1 - END - Modified and added javascript method to support all browsers and extended validations
// returns true if any value on the form has changed.
function checkChanged(el) {
	var j;
	for (var i=0; i<el.form.elements.length; i++) 
	  if (el.form.elements[i]!=el) with (el.form.elements[i]) {
		switch(type) {
		case 'text': 
		case 'textarea': if (defaultValue!=value) {return true;} break;
		case 'file': if (value!='') {return true;} break;
		case 'select-one': j=selectedIndex; 
			if (j>=0 && !options[j].defaultSelected) {return true;} break;
		case 'checkbox': if (defaultChecked!=checked) {return true;} break;
		case 'radio': if (defaultChecked!=checked) {return true;} break;
		}
	}
	return false;
}

// function that can be called if a validation event fails.  el is the element that the
// validation was for; msg is the error message.  Displays the message, focuses the element,
// and returns false allowing this function call to be the return value for the validation
// function in a single statement.
function failValidation(el, msg) {
	alert(msg);
	el.focus();
	if (el.type=='text' && el.value.length>0) el.select();
	return false;
}

// Checks to see if the value entered in a textbox is a valid date.  Error message if not.
function verifyDate(tb) {
	if (tb.value != tb.defaultValue && tb.value != '') {
		if (isNaN(Date.parse(tb.value))) {
			failValidation(tb, 'Please enter a valid date.')
		} else return true;;
	}
}

