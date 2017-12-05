/// <reference path="jquery-3.2.1.min.js" />
//CHGxxxx- JQuery Upgrade Changes- 05/29/2017
// CHG0121437 - Jquery events to handle the masking of Credit card number - Start//

$(document).ready(function () {
    var vCardBilling = document.getElementById('Card__CardNumber');
    var vCardBillingHidden = document.getElementById('Card___CardMasked');
    var vCardManageRecurring = document.getElementById('EnrollCC__CardNumber');
    var vCardManageRecurringHidden = document.getElementById('EnrollCC___CardMasked');
    $(vCardBillingHidden).hide();
    $(vCardManageRecurring).hide();
    if (vCardBilling != null) {

        if (vCardBilling.value.length > 4) {

            vCardBillingHidden.value = vCardBilling.value.replace(/\d(?=\d{4})/g, "*");;
        }
        else {
            vCardBillingHidden.value = vCardBilling.value;
        }
        $(vCardBillingHidden).show();
        $(vCardBilling).hide();



        $(vCardBilling).focus(function () {
            $(vCardBillingHidden).hide();
            this.value = '';
        });
        $(vCardBilling).blur(function () {
            if (vCardBilling.value.length > 4) {

                vCardBillingHidden.value = vCardBilling.value.replace(/\d(?=\d{4})/g, "*");;
            }
            else {
                vCardBillingHidden.value = vCardBilling.value;
            }
            $(vCardBillingHidden).show();
            $(vCardBilling).hide();
        });
        $(vCardBillingHidden).focus(function () {
            $(vCardBilling).show();
            $(vCardBillingHidden).hide();
            $(vCardBilling).focus();
        });
    }
    if (vCardManageRecurring != null) {

        if (vCardManageRecurring.value.length > 4) {

            vCardManageRecurringHidden.value = vCardManageRecurring.value.replace(/\d(?=\d{4})/g, "*");;
        }
        else {
            vCardManageRecurringHidden.value = vCardManageRecurring.value;
        }
        $(vCardManageRecurringHidden).show();
        $(vCardManageRecurring).hide();



        $(vCardManageRecurring).focus(function () {
            $(vCardManageRecurringHidden).hide();
            this.value = '';
        });
        $(vCardManageRecurring).blur(function () {
            if (vCardManageRecurring.value.length > 4) {

                vCardManageRecurringHidden.value = vCardManageRecurring.value.replace(/\d(?=\d{4})/g, "*");;
            }
            else {
                vCardManageRecurringHidden.value = vCardManageRecurring.value;
            }
            $(vCardManageRecurringHidden).show();
            $(vCardManageRecurring).hide();
        });
        $(vCardManageRecurringHidden).focus(function () {
            $(vCardManageRecurring).show();
            $(vCardManageRecurringHidden).hide();
            $(vCardManageRecurring).focus();
        });
    }
});





// CHG0121437 - Jquery events to handle the masking of Credit card number - End//