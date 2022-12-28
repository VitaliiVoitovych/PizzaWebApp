function loadHeader() {
    fetch("header.html")
        .then(response => response.text())
        .then(text => document.querySelector(".header").innerHTML = text);
}

loadHeader();