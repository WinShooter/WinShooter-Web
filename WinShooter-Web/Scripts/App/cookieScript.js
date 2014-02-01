$(document).ready(function () {
    var strCookieName = "cookie-compliance";
    var strApprovedVal = "approved";

    var cookieVal = readCookie(strCookieName);
    var $displayMsg = $('#cookieMessageWrapper');

    if (cookieVal != strApprovedVal) {
        setTimeout(function() {
             $displayMsg.slideDown(200);
        }, 200);
    } else if (!$displayMsg.is(':hidden')) {
        $displayMsg.slideUp();
    };

    $('#cookieClose').click(function () {
        $displayMsg.slideUp();
        createCookie(strCookieName, strApprovedVal, 365);
    });
});

//Cookie functions
function createCookie(name, value, days) {
    var expires = "";
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        expires = "; expires=" + date.toGMTString();
    }

    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameEq = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') c = c.substring(1, c.length);
        if (c.indexOf(nameEq) == 0) return c.substring(nameEq.length, c.length);
    }

    return null;
}

function eraseCookie(name) {
    createCookie(name, "", -1);
}