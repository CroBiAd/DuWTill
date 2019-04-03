//Chrome insists on keeping stale/old javascript code and won't reload, but I think ctl-shift-r does a "hard reload," ignoring any cache.
//Cache-Buster:
//Every web developer knows the issue. You want to have your static assets be cached by the visitor’s browser with a very long expiry date. This accelerates the site on the visitor’s site, 
//reduces the strain and bandwidth usage on your server, and gives you good PageSpeed scores. 
//However, when you start making changes to your site, you have to force a hard refresh in your browser (or even worse, reset your static caching subsystem) to get your browser to actually 
//download that changed version of the asset. But don't expect users to use Ctrl+R/F5!!!!!!!!!!!!!!!!!!!!!!!!!!
//That’s why you want to include a “cache busting” system, that suggests to the browser that, when you made a change in your static asset, that new file is actually different and should be freshly downloaded.
//There are different approaches to achieve this: https://www.alainschlesser.com/bust-cache-content-hash/
//Here I've just changed file names of Site.js & SIte.css to include version number:
//Bundle.config:     <include path="~/Content/Site.1.2.css" />
//SiteMaster:        <%: Scripts.Render("~/Scripts/Site.1.2.js") %>

//Test for cookies - wether they turned cookies off
function setCookie(cname, cvalue, exdays) {
    var d = new Date();
    d.setTime(d.getTime() + (exdays*24*60*60*1000));
    var expires = "expires=" + d.toGMTString();
    document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}
function getCookie(cname) {
    var name = cname + "=";
    var decodedCookie = decodeURIComponent(document.cookie);
    var ca = decodedCookie.split(';');
    for(var i = 0; i < ca.length; i++) {
        var c = ca[i];
        while (c.charAt(0) == ' ') {
            c = c.substring(1);
        }
        if (c.indexOf(name) == 0) {
            return c.substring(name.length, c.length);
        }
    }
    return null;
}
function checkCookie() {
    var r = false;
    setCookie("duwtillcookietest", "Hi!", 1);
    if (getCookie("duwtillcookietest") != null) {
        r = true;
        setCookie("duwtillcookietest", "", -1); //delete cookies; this test is only on 'Log in'
    }
    return r;
}

//Disable two menu links, they must be unclicable
//window.onload = function () {
//    var element = document.getElementById('mustlogin');
//    if (typeof (element) != 'undefined' && element != null) {//exists.
//        $('#lisearch').addClass('disabled');
//        $('#liblast').addClass('disabled');
//    }
//};
