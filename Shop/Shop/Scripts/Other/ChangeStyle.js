
function setStyle(title) {
    var a, i;
    for (i = 0; (a = document.getElementsByTagName("link")[i]); i++) {
        if (a.getAttribute("rel").indexOf("style") != -1 &&
            a.getAttribute("title")) {
            a.disabled = true;
            if (a.getAttribute("title") == title)
                a.disabled = false;
        }
    }
}

function getStyle() {
    var a, i;
    for (i = 0; (a = document.getElementsByTagName("link")[i]); i++) {
        if (a.getAttribute("rel").indexOf("style")
            != -1 && a.getAttribute("title") && !a.disabled)
            return a.getAttribute("title");
    }
    return null;
}

function getPreferredStyle() {
    var i, a;
    for (i = 0; (a = document.getElementsByTagName("link")[i]); i++) {
        if (a.getAttribute("rel").indexOf("style") != -1
            && a.getAttribute("rel").indexOf("alt") == -1
            && a.getAttribute("title")
        ) return a.getAttribute("title");
    }
    return null;
}

function createCookie(name, value, days) {
    if (days) {
        var date = new Date();
        date.setTime(date.getTime() + (days * 24 * 60 * 60 * 1000));
        var expires = "; expires=" + date.toGMTString();
    }
    else expires = "";
    document.cookie = name + "=" + value + expires + "; path=/";
}

function readCookie(name) {
    var nameE = name + "=";
    var ca = document.cookie.split(';');
    for (var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1, c.length);
        }
        if (c.indexOf(nameE) == 0)
            return c.substring(nameE.length, c.length);
    }
    return null;
}

window.onload = function (e) {
    if (readCookie("styleHighContrast")) {
        var title = readCookie("styleHighContrast")
        setStyle(title);
    }
    else {
        var title = getPreferredStyle();
        setStyle(title);
    }
}

window.onunload = function (e) {
    createCookie("styleHighContrast", getStyle(), 180);
}

