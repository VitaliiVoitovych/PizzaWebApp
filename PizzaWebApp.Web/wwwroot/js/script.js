const headerButton = document.querySelector(".header__btn");
const headerMenu = document.querySelector(".header__menu");

button.onclick = clicked;

function clicked() {
    headerMenu.classList.toggle('active-menu');
    headerMenu.classList.toggle('deactive-menu');
    headerButton.classList.toggle('active-btn');
}