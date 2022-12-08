async function getPizzas() {
    const response = await fetch("/api/cart", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const pizzas = await response.json();
        const rows = document.querySelector("tbody");
        pizzas.forEach(pizza => rows.append(rowCart(pizza)));
    }
}

function rowCart(pizza) {
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
    button.classList.add("cart__button");
    button.classList.add("btn");
    button.append("Remove");
    buttonTd.append(button);
    tr.appendChild(buttonTd);

    return tr;
}

getPizzas();