using Microsoft.EntityFrameworkCore;
using PizzaWebApp.DAL.EfStructures;
using PizzaWebApp.Models.Entities;
using PizzaWebApp.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using System.Text.Json;
using PizzaWebApp.Web.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/login";
    });
builder.Services.AddAuthorization();

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

app.UseAuthentication();
app.UseAuthorization();

app.Map("/home", () => Results.Redirect("/"));
app.Map("/cart", [Authorize(Roles = "User")] async (HttpContext context) =>
    await context.Response.WriteAsync(File.ReadAllText("wwwroot/cart.html")));
app.Map("/signup", async (HttpContext context) =>
    await context.Response.WriteAsync(File.ReadAllText("wwwroot/signup.html")));

app.Map("/admin", [Authorize(Roles = "Admin")] async(HttpContext context) =>
    await context.Response.WriteAsync(File.ReadAllText("wwwroot/admin.html")));

app.MapPost("/signup", (HttpContext context, PizzaWebAppDbContext db) =>
{
    var form = context.Request.Form;
    if (string.IsNullOrWhiteSpace(form["firstname"]) ||
        string.IsNullOrWhiteSpace(form["lastname"]) ||
        string.IsNullOrWhiteSpace(form["email"]) ||
        string.IsNullOrWhiteSpace(form["password"]))
    {
        return Results.BadRequest("The field(s) is empty");
    }

    var person = db.People.FirstOrDefault(c => c.Email == form["email"].ToString());
    if (person is not null) return Results.Problem("This user already exists");

    person = new Person
    {
        FirstName = form["firstname"].ToString(),
        LastName = form["lastname"].ToString(),
        Email = form["email"].ToString(),
        Password = form["password"].ToString(),
    };

    db.People.Add(person);
    db.SaveChanges();

    return Results.Redirect("/");
});

app.MapGet("/api/menu", async (PizzaWebAppDbContext db) => await db.Pizzas.ToListAsync());
app.MapGet("/api/cart", (Cart cart) => cart);
app.MapGet("/api/cart/price", (Cart cart) => cart.Price);


app.MapPost("/api/menu/{id:int}", [Authorize(Roles = "User")] async(int id, Cart cart, PizzaWebAppDbContext db) =>
{
    Pizza? pizza = await db.Pizzas.FirstOrDefaultAsync(p => p.PizzaId == id);
    if (pizza == null) return Results.NotFound(new { Message = "Not found" });

    cart.AddItem(pizza);

    return Results.Json(pizza);
});

app.MapDelete("/api/cart/{id:int}", (int id, Cart cart) =>
{
    Pizza? pizza = cart.FirstOrDefault(p => p.PizzaId == id);
    if (pizza == null) return Results.NotFound(new { Message = "Not found" });

    cart.RemoveItem(pizza);

    return Results.Json(pizza);
});

app.MapPost("/api/cart/payment", async(Cart cart, HttpContext context, PizzaWebAppDbContext db) =>
{
    if (cart.Any() == false)
    {
        return Results.BadRequest(new { Message = "Cart is empty"});
    }

    var login = context.User.Identity?.Name;
    Person person = await db.People.FirstAsync(c => c.Email == login);

    var order = new Order
    {
        PersonId = person.PersonId,
        OrderDate = DateTime.Now,
    };

    db.Orders.Add(order);
    db.SaveChanges();

    foreach (var item in cart)
    {
        var payment = new Payment
        {
            OrderId = order.OrderId,
            PizzaId = item.PizzaId,
            PaymentDate = DateTime.Now,
        };
        db.Payments.Add(payment);
    }

    db.SaveChanges();

    return Results.Json(cart);
});

app.MapGet("/login", async (HttpContext context) =>
    await context.Response.WriteAsync(File.ReadAllText("wwwroot/login.html")));

app.MapGet("/logout", async (HttpContext context) =>
{
    await context.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
    return Results.Redirect("/login");
});

app.MapPost("/login", async (HttpContext context, PizzaWebAppDbContext db) =>
{
    var form = context.Request.Form;

    if (string.IsNullOrWhiteSpace(form["email"]) ||
        string.IsNullOrWhiteSpace(form["password"]))
    {
        return Results.BadRequest("The field(s) is empty");
    }
    string email = form["email"].ToString();
    string password = form["password"].ToString();

    Person? person = db.People.FirstOrDefault(p => p.Email == email && p.Password == password);
    if (person is null) return Results.Unauthorized();

    var claims = new List<Claim> 
    { 
        new Claim(ClaimTypes.Name, person.Email),
        new Claim(ClaimTypes.Role, person.Role),
    };
    ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
    await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
    return Results.Redirect("/");
});

app.MapPost("/api/admin/addpizza", async (HttpContext context, PizzaWebAppDbContext db) =>
{
    Pizza? pizza;
    try
    {
        var jsonOptions = new JsonSerializerOptions();
        jsonOptions.Converters.Add(new PizzaConverter());
        pizza = await context.Request.ReadFromJsonAsync<Pizza>(jsonOptions);
        if (pizza is null) return Results.BadRequest(new { Message = "Pizza is null" });
    }
    catch (Exception e)
    {
        return Results.BadRequest(new { e.Message });
    }

    db.Pizzas.Add(pizza);
    db.SaveChanges();

    return Results.Json(pizza);
});

app.MapGet("/api/isadmin", (HttpContext context) =>
{
    var isAdmin = context.User.IsInRole("Admin");
    return Results.Json(isAdmin);
});

app.Run();
