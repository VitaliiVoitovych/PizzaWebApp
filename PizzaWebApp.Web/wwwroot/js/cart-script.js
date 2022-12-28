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

async function getPrice() {
    const response = await fetch("/api/cart/price", {
        method: "GET",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const price = await response.json();
        const priceTitle = document.querySelector(".cart__price");
        priceTitle.textContent = `${price}$`;
    }
}

async function removeFromCart(id) {
    const response = await fetch(`/api/cart/${id}`, {
        method: "DELETE",
        headers: { "Accept": "application/json" }
    });
    if (response.ok === true) {
        const pizza = await response.json();
        document.querySelector(`tr[data-rowid='${pizza.pizzaId}']`).remove();
        getPrice();
    }
    else {
        const error = await response.json();
        console.log(error.message);
    }
}

async function payment() {
    const response = await fetch("/api/cart/payment", {
        method: "POST",
        headers: { "Accept": "application/json"}
    });
    if (response.ok === true) {
        const  pizzas = await response.json();
        pizzas.forEach(pizza => removeFromCart(pizza.pizzaId));
    }
    else {
        const error = await response.json();
        alert(error.message);
    }
}

function rowCart(pizza) {
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
    button.classList.add("cart__button");
    button.classList.add("btn");
    button.append("Remove");
    button.addEventListener("click", async() => await removeFromCart(pizza.pizzaId));
    buttonTd.append(button);
    tr.appendChild(buttonTd);

    return tr;
}

const paymentBtn = document.querySelector('.cart__payment-btn');
paymentBtn.addEventListener("click", async() => await payment());

getPizzas();
getPrice();