using Microsoft.EntityFrameworkCore;
using PizzaWebApp.DAL.EfStructures;
using PizzaWebApp.Models.Entities;
using PizzaWebApp.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PizzaWebAppDbContext>(options => options.UseSqlServer(connectionString));
builder.Services.AddSingleton<Cart>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.Map("/home", () => Results.Redirect("/"));

app.MapGet("/api/menu", async (PizzaWebAppDbContext db) => await db.Pizzas?.ToListAsync());
app.MapGet("/api/cart", (Cart cart) => cart);
app.MapGet("/api/cart/price", (Cart cart) => cart.Price);


app.MapPost("/api/menu/{id:int}", (int id, Cart cart) =>
{
    Pizza? pizza = list.FirstOrDefault(p => p.PizzaId == id);

    if (pizza == null) return Results.NotFound(new { Message = "Не знайдено"});

    cart.AddItem(pizza);

    return Results.Json(pizza);
});

app.MapDelete("/api/cart/{id:int}", (int id, Cart cart) =>
{
    Pizza? pizza = cart.FirstOrDefault(p => p.PizzaId == id);

    if (pizza == null) return Results.NotFound(new { Message = "Не знайдено" });

    cart.RemoveItem(pizza);

    return Results.Json(pizza);
});

app.Run();
