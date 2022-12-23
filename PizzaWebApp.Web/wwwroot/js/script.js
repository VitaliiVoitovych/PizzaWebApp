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

async function addToCart(id) {
    const response = await fetch(`/api/menu/${id}`, {
        method: "POST",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const pizza = await response.json();
    }
    else {
        const error = await response.json();
        console.log(error);
    }
}

function row(pizza) {
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

    const buttonTd = document.createElement("td");
    const button = document.createElement("button");
    button.classList.add("menu__button");
    button.classList.add("btn");

    button.addEventListener("click", async () => await addToCart(pizza.pizzaId));

    button.append("Buy");
    buttonTd.append(button);
    tr.appendChild(buttonTd);

    return tr;
}

getPizzas();