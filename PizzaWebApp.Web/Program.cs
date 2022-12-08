using Microsoft.EntityFrameworkCore;
using PizzaWebApp.DAL.EfStructures;
using PizzaWebApp.Models.Entities;
using PizzaWebApp.Models;

var list = new List<Pizza>
{
    new Pizza() { PizzaId = 1, Name = "Salami", Size = PizzaSize.Medium, Weight = 404, Price  = 32 },
    new Pizza() { PizzaId = 2, Name = "Cheese", Size = PizzaSize.Medium, Weight = 343, Price  = 37 },
    new Pizza() { PizzaId = 3, Name = "Beef", Size = PizzaSize.Large, Weight = 564, Price  = 45 },
    new Pizza() { PizzaId = 4, Name = "Chicken", Size = PizzaSize.Small, Weight = 244, Price  = 25 },
    new Pizza() { PizzaId = 5, Name = "Salami", Size = PizzaSize.Medium, Weight = 404, Price  = 32 },
};

var cart = new Cart();
cart.AddItem(list[0]);
cart.AddItem(list[0]);
cart.AddItem(list[0]);

var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<PizzaWebAppDbContext>(options => options.UseSqlServer(connectionString));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseDefaultFiles();
app.UseStaticFiles();

app.Map("/home", () => Results.Redirect("/"));

app.MapGet("/api/menu", () => list);
app.MapGet("/api/cart", () => cart);

app.Run();
