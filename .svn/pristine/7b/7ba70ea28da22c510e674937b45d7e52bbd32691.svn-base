/// <reference path="jquery-3.2.1.min.js" />
//CHGxxxx- JQuery Upgrade Changes- 05/29/2017

// CHG0123270 - Jquery events to handle the masking of ACH card number - Start//
$(document).ready(function () {
    //CHGxxxx- JQuery Upgrade Changes- 05/29/2017- start
    // window.onload = function (e) {
   // CHGxxxx- JQuery Upgrade Changes- 05/29/2017- end
        var vAccountNoRecurring = document.getElementById('EnrollECheck__Accountno');
        var vAccountNoHiddenRecurring = document.getElementById('EnrollECheck___AccountnoMasked');
        var vReAccountNo = document.getElementById('EnrollECheck__ReAccountNo');
        var vReAccountNoHidden = document.getElementById('EnrollECheck___ReAccountNoCardMasked');
        var vAccountNoBilling = document.getElementById('echeck__Accountno');
        var vAccountNoBillingHidden = document.getElementById('echeck___AccountnoMasked');

        $(vAccountNoHiddenRecurring).hide();
        $(vReAccountNoHidden).hide();
        $(vAccountNoBillingHidden).hide();

        if (vAccountNoRecurring != null) {

            if (vAccountNoRecurring.value.length > 4) {

                vAccountNoHiddenRecurring.value = vAccountNoRecurring.value.replace(/\d(?=\d{4})/g, "*");;
            }
            else {
                vAccountNoHiddenRecurring.value = vAccountNoRecurring.value;
            }
            $(vAccountNoHiddenRecurring).show();
            $(vAccountNoRecurring).hide();



            $(vAccountNoRecurring).focus(function () {
                $(vAccountNoHiddenRecurring).hide();
                this.value = '';
            });
            $(vAccountNoRecurring).blur(function () {
                if (vAccountNoRecurring.value.length > 4) {

                    vAccountNoHiddenRecurring.value = vAccountNoRecurring.value.replace(/\d(?=\d{4})/g, "*");;
                }
                else {
                    vAccountNoHiddenRecurring.value = vAccountNoRecurring.value;
                }
                $(vAccountNoHiddenRecurring).show();
                $(vAccountNoRecurring).hide();
            });
            $(vAccountNoHiddenRecurring).focus(function () {
                $(vAccountNoRecurring).show();
                $(vAccountNoHiddenRecurring).hide();
                // Added code for clearing the text box values on click of Account number or Re-enter account number
                if (vAccountNoHiddenRecurring.value != "" && vReAccountNoHidden.value != "")
                {
                    $(vReAccountNo).show();
                    $(vReAccountNoHidden).hide();
                    $(vReAccountNo).focus();
                    $(vAccountNoRecurring).focus();
                }
                $(vAccountNoRecurring).focus();
                // Added code for clearing the text box values on click of Account number or Re-enter account number
            });
        }
        if (vReAccountNo != null) {

            if (vReAccountNo.value.length > 4) {

                vReAccountNoHidden.value = vReAccountNo.value.replace(/\d(?=\d{4})/g, "*");;
            }
            else {
                vReAccountNoHidden.value = vReAccountNo.value;
            }
            $(vReAccountNoHidden).show();
            $(vReAccountNo).hide();



            $(vReAccountNo).focus(function () {
                $(vReAccountNoHidden).hide();
                this.value = '';
            });
            $(vReAccountNo).blur(function () {
                if (vReAccountNo.value.length > 4) {

                    vReAccountNoHidden.value = vReAccountNo.value.replace(/\d(?=\d{4})/g, "*");;
                }
                else {
                    vReAccountNoHidden.value = vReAccountNo.value;
                }
                $(vReAccountNoHidden).show();
                $(vReAccountNo).hide();
            });
            $(vReAccountNoHidden).focus(function () {
                $(vReAccountNo).show();
                $(vReAccountNoHidden).hide();
                // Added code for clearing the text box values on click of Account number or Re-enter account number
                if (vAccountNoHiddenRecurring.value != "" && vReAccountNoHidden.value != "")
                {
                    $(vAccountNoRecurring).show();
                    $(vAccountNoHiddenRecurring).hide();
                    $(vAccountNoRecurring).focus();
                    $(vReAccountNo).focus();
                }
                $(vReAccountNo).focus();
                // Added code for clearing the text box values on click of Account number or Re-enter account number
            });
        }
        if (vAccountNoBilling != null) {

            if (vAccountNoBilling.value.length > 4) {

                vAccountNoBillingHidden.value = vAccountNoBilling.value.replace(/\d(?=\d{4})/g, "*");;
            }
            else {
                vAccountNoBillingHidden.value = vAccountNoBilling.value;
            }
            $(vAccountNoBillingHidden).show();
            $(vAccountNoBilling).hide();



            $(vAccountNoBilling).focus(function () {
                $(vAccountNoBillingHidden).hide();
                this.value = '';
            });
            $(vAccountNoBilling).blur(function () {
                if (vAccountNoBilling.value.length > 4) {

                    vAccountNoBillingHidden.value = vAccountNoBilling.value.replace(/\d(?=\d{4})/g, "*");;
                }
                else {
                    vAccountNoBillingHidden.value = vAccountNoBilling.value;
                }
                $(vAccountNoBillingHidden).show();
                $(vAccountNoBilling).hide();
            });
            $(vAccountNoBillingHidden).focus(function () {
                $(vAccountNoBilling).show();
                $(vAccountNoBillingHidden).hide();
                $(vAccountNoBilling).focus();
            });

        }
    //CHGxxxx- JQuery Upgrade Changes- 05/29/2017- start
    // }
    // CHGxxxx- JQuery Upgrade Changes- 05/29/2017- end
});
// CHG0123270 - Jquery events to handle the masking of ACH card number - End//