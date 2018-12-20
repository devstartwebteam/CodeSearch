
/* Side Nav Menu */

function openNav() {
    var sideNav = document.getElementById("w-sidenav");
    if (typeof (sideNav) != "undefined" && sideNav != null) {
        sideNav.style.width = "100%";
        sideNav.style.visibility = "visible";
    }

    var smallNav = document.getElementById("w-sidenav-small");
    if (typeof (smallNav) != "undefined" && smallNav != null) {
        smallNav.style.width = "16.666666666666664%";
        smallNav.style.visibility = "visible";
    }
}

function closeNav() {
    var sideNav = document.getElementById("w-sidenav");
    if (typeof (sideNav) != "undefined" && sideNav != null) {
        sideNav.style.width = "0";
        sideNav.style.visibility = "hidden";
    }
    
    var smallNav = document.getElementById("w-sidenav-small");
    if (typeof (smallNav) != "undefined" && smallNav != null) {
        smallNav.style.width = "0";
        smallNav.style.visibility = "hidden";
    }
}

$(function() {
    var homeLogin = document.getElementById("w-home-login");
        if (typeof (homeLogin) != "undefined" && homeLogin != null) {
            homeLogin.addEventListener("click", closeNav);
        }
    
    var navBar = document.getElementById("navbar");
        if (typeof (navBar) != "undefined" && navBar != null) {
            navBar.addEventListener("click", closeNav);
        }
    
    var dashboard = document.getElementById("w-dashboard");
        if (typeof (dashboard) != "undefined" && dashboard != null) {
            dashboard.addEventListener("click", closeNav);
        }
});
