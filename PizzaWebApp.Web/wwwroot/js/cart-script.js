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
    const tr = rowPizzaInfo(pizza);

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

getPizzas("/api/cart", rowCart);
getPrice();