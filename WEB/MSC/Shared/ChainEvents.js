/*
This file contains functions that allow events to be chained.  That is, more than one 
event function can be supplied for an event, and event functions can be added through 
script to event even if the events are define in html.
*/

// this function adds newAction to the onAction event of onObject.
// If the event is defined, the text will be pre-pended to the
// event.  The text should include the semi-colon, unless for
// example it is an if statement.  
function stuffOnAction(onObject, newAction, onAction) {
	if (onObject.type == 'hidden') return;
	if (typeof(eval('onObject.'+onAction)) == "function") {
		var st = eval('onObject.'+onAction+'.toString()');
		st = st.replace(/{/, "{ " + newAction + " ");
	} else st = 'function anonymous() {' + newAction + '}';
	eval("onObject."+onAction+" = " + st);
}

// call this function with a function to add to object's event chain for event.
function linkToEvent(object, eventStr, newAction, toFront) {
	if (arguments.length <4) var toFront = false;
	var chain = eval('object.chain_' + eventStr);
	var f = eval('object.' + eventStr);
	if (typeof(chain) == 'undefined') {
		eval('object.chain_' + eventStr + ' = new Array()');
		chain = eval('object.chain_' + eventStr);
		eval('object.' + eventStr + ' = _chainOfEvents');
		if (typeof(f) == 'function') chain[0] = f;
	} 
	if (toFront && chain.length > 0) {
		var a = new Array();
		a[0] = newAction;
		chain = a.concat(chain);
	} else chain[chain.length] = newAction;
	eval('object.chain_' + eventStr + ' = chain');
}

//This function is used for the event function for any objects's events that have 
//new events linked through linkToEvent.  Loops through all the functions in the 
//chain associated with e, calling each.  If any fails, stops looping and returns false, 
//otherwise returns the last result.
function _chainOfEvents(e) {
    if (arguments.length == 0 && navigator.appName.indexOf("Netscape") == -1) 
		var e = event;
    var result;
    var etype = (e.type=='delayedblur')?'blur':e.type;
	var chain = eval('this.chain_on' + etype);
	for (var i = 0; i < chain.length; i++) {
		this.tempevent = chain[i];
		result = this.tempevent(e);
		if (typeof(result) == 'boolean' && !result) return false;
	}
	return result;	
}
