function loadHeader() {
    fetch("header.html")
        .then(response => response.text())
        .then(text => document.querySelector(".header").innerHTML = text);
    setAdminPanelButton();
}

function rowPizzaInfo(pizza) {
    const tr = document.createElement("tr");
    tr.setAttribute("data-rowid", pizza.pizzaId);

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

    return tr;
}

/**
 * 
 * @param {String} request 
 */
async function getPizzas(request, rowFunc) {
    const response = await fetch(request, {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const pizzas = await response.json();
        const rows = document.querySelector("tbody");
        pizzas.forEach(pizza => rows.append(rowFunc(pizza)));
    }
}

async function setAdminPanelButton() {
    const response = await fetch("/api/isadmin", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    // ???
    let isAdmin = await response.json();
    if (isAdmin) {
        var adminBtn = document.querySelector(".header__admin");
        console.log(adminBtn);
        if (adminBtn == null) {
            await setAdminPanelButton();
        }
        else {
            adminBtn.style.display = "block";
        }
    }
}

loadHeader();