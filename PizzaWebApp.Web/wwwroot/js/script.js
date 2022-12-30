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

function rowMenu(pizza) {
    const tr = rowPizzaInfo(pizza);
    
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

getPizzas("/api/menu", rowMenu);