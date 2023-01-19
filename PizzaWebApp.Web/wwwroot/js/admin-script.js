const button = document.getElementById("addpizza-btn").addEventListener("click", addPizza);

async function addPizza() {
    const response = await fetch("/api/admin/addpizza", {
        method: "POST",
        headers: { "Accept" : "application/json", "Content-Type" : "application/json"},
        body: JSON.stringify({
            name: document.getElementById("pizzaName").value,
            weight: parseFloat(document.getElementById("pizzaWeight").value),
            size: parseInt(document.getElementById("pizzaSize").value),
            price : parseFloat(document.getElementById("pizzaPrice").value),
        }),
    });

    reset();

    const pizza = await response.json();
    let str = `Pizza : ${pizza.name}, ${pizza.weight}g, ${pizza.size}, $${pizza.price} added to database`;
    alert(str);
}

function reset() {
    document.getElementById("pizzaName").value = "";
    document.getElementById("pizzaWeight").value = "";
    document.getElementById("pizzaPrice").value = "";
}