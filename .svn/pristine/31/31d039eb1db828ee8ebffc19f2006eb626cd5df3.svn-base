/* This script will take all of the elements in all of the forms, and do several things:
	1) Any text boxes, or text areas that have onblur event handlers defined, will
	have those event handlers revoked, but saved in a delayed blur event handler to
	prevent infinite loops in Netscape, and to prevent the onblur from occuring
	when the window is blurred.
	2) Form elements, except for reset buttons, and buttons whose names are Cancel
	or delete will get onfocus event handlers that keep track of where the focus came
	from. In this event handler, if the focus came from an element that had an onblur
	event, then that event will be invoked.
	3) Text, radio, checkbox & select boxes will have an additional onfocus & onblur event
	chained which captures the keypress event and checks for the enter key.  If there is 
	a submit button or an image submit defined for the form containing that element.  That
	button will be clicked.  If there are multiple submits, then the first one will be clicked.
	
*/
/*
This method is added to the keypress chain for most form elements in ie, or is 
made the window.onKeyPRess event in ns.  Checks the key pressed in e, if it's not 
CR, returns true, otherwise, if the element isn't a textarea (this will only happen in ns) 
it moves the focus to the Window.Enter button and sets a timer to click it after the 
current event completes.
*/
function _formEvents_keypress(e) { 
	var ns = navigator.appName.indexOf("Netscape") >= 0;
	key = (ns) ? e.which : e.keyCode;
	var el = window.focusedElement;
	if (el.type == 'textarea') { // only can happen in ns.
		return (el.actual_onkeypress != null)?el.actual_onkeypress(e):true;
	} else if (key!=13) {
		return true;
	} else {
   		if (window.enterButton != null) {
	   		window.enterButton.focus();
   			setTimeout("window.enterButton.click()",50);
		}
		return false;
	}  
}

/*
This method is made the onclick function for a button when that button becomes the 
Window.Enter button.  It insures that the button has the focus before it allows the 
click to continue;  this insures that any blurs from other elements occur first.  
If the click can continue, it calls the buttons original onclick if there was one.
*/
function _enterOnClick() {
	var result;
	if (this == window.focusedElement) {
		if ((this.originalonclick) != 'undefined' && this.originalonclick != null)
			result =  this.originalonclick() 
		else result = true
	} else result = false;
	return result;
}

//Makes button the Window.enter button.
function captureEnter(button) {
	window.enterButton = button;
	if (typeof(button.originalonclick) != 'function')
		button.originalonclick = button.onclick;
	button.onclick = _enterOnClick;
}

/*
This method is linked to every element's onblur event, and causes the 
Window.Enter button to be released when focus leaves the element.
*/
function _releaseEnter() {
	if (typeof(window.enterButton) != 'undefined' && window.enterButton != null && typeof(window.enterButton.originalonclick) == 'function') {
		window.enterButton.onclick = window.enterButton.originalonclick;
		window.enterButton.originalonclick = null;
	}
}

/*
This method is linked to every element's onfocus event.  So upon focusing, 
the element captures the Window.Enter button as the button that will be 
clicked from the element if enter is pressed.
*/
function _captureEnterToElement() { 
	_releaseEnter();
	var button = null;
	var button1 = null;
	var el;
	for (var i=0; i< this.form.elements.length; i++) {
		el = this.form.elements[i]
		if (el.type == 'submit' || el.type == 'image') {
			button = el; break;
		} else if (el.type == 'button' && button1 == null) button1 = el;
	}
	if (button == null && button1 != null) button = button1;
	if (button != null) captureEnter(button);
}

//linkToEvent (window, 'onload', trapAllFormBlurs); //, true);
// Initializes this module.  Sets up keypress, blur and focus events 
// for all user input form elements on the page.
function trapAllFormBlurs() { 
	window.focusedElement = self;
	window.enterButton = null;
	var ns = (navigator.appName.indexOf("Netscape") >= 0);
	for (var j = 0; j < document.forms.length; j++) with (document.forms[j]) {
		for (var i = 0; i < elements.length; i++) {
			if (elements[i].name.toLowerCase() != 'cancel' && elements[i].name.toLowerCase() != 'delete' &&
				elements[i].type != 'reset') {
					linkToEvent(elements[i], "onfocus", _all_elements_onfocus);
			}
			switch (elements[i].type) {
				case 'text' : 
				case 'textarea' : 
				case 'password':
				case 'file':
					if (typeof(elements[i].onblur) == 'function') {
						elements[i].ondelayedblur = elements[i].onblur;
						elements[i].onblur = null;
					}
			}

			switch (elements[i].type) {
				case 'textarea': 
					if (ns) {
						if (typeof(elements[i].onkeypress) == 'function') {
							elements[i].actual_onkeypress = elements[i].onkeypress;
							elements[i].onkeypress = null;
						} else elements[i].actual_onkeypress = null;
					}
					linkToEvent(elements[i], "onfocus", _captureEnterToElement);
					linkToEvent(elements[i], "onblur", _releaseEnter);
					break;
				case 'radio':
				case 'checkbox':
				case 'select one':
				case 'select multiple':
				case 'text':
				case 'password':
				case 'file':
					if (!ns) linkToEvent(elements[i], "onkeypress", _formEvents_keypress, true);
					linkToEvent(elements[i], "onfocus", _captureEnterToElement);
					linkToEvent(elements[i], "onblur", _releaseEnter);
			}
		}
	}
	if (ns) {
		window.captureEvents(Event.KEYPRESS);
		window.onKeyPress = _formEvents_keypress;
	}
}

/*
This method is added to the focus event for all form elements except reset buttons, 
and elements whose name are cancel or delete.  It insures that the previously focused 
element's blur event occurs before the new element's focus event, and if the previous 
element's blur event fails, restores the focus to the previous element instead of 
allowing the focus to proceed to the new element.  It also sets window.focusedElement to 
itself.
*/
function _all_elements_onfocus() {
	var fE = window.focusedElement;
	window.focusedElement = this;
	if (fE.name!=this.name && typeof(fE.ondelayedblur) != 'undefined') {
		var e1 = new Object();
		e1.type = 'delayedblur'
		fE.ondelayedblur(e1);
	}
}

