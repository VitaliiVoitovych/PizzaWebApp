const headerButton = document.querySelector(".header__btn");
const headerMenu = document.querySelector(".header__menu");

headerButton.onclick = clicked;


function clicked() {
    headerMenu.classList.toggle('active-menu');
    headerMenu.classList.toggle('deactive-menu');
    headerButton.classList.toggle('active-btn');
}

async function getPizzas() {
    const response = await fetch("/api/menu", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const pizzas = await response.json();
        const rows = document.querySelector("tbody");
        pizzas.forEach(pizza => rows.append(row(pizza)));
    }
}

function row(pizza) {
    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", pizza.id);

    const nameTd = document.createElement("td");
    nameTd.append(pizza.name);
    tr.append(nameTd);

    const sizeTd = document.createElement("td");
    sizeTd.append(pizza.size);
    tr.append(sizeTd);

    const weightTd = document.createElement("td");
    weightTd.append(pizza.weight);
    tr.append(weightTd);

    const priceTd = document.createElement("td");
    priceTd.append(pizza.price);
    tr.append(priceTd);

    const buttonTd = document.createElement("td");
    const button = document.createElement("button");
    button.classList.add("menu__button");
    button.classList.add("btn");
    button.append("Buy");
    buttonTd.append(button);
    tr.appendChild(buttonTd);

    return tr;
}

getPizzas();