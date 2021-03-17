var mainNav = document.getElementById("mainNav");
var menuBtn = document.getElementById("menu-btn");
var menuOpen = false;

menuBtn.addEventListener('click', openMenu);

function openMenu() {
    if (!menuOpen) {
        menuBtn.classList.add("open");
        mainNav.classList.add("show");
        menuOpen = true;
    } else {
        menuBtn.classList.remove("open");
        mainNav.classList.remove("show");
        menuOpen = false;
    }
}