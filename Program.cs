using Alfasoft;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

var serverVersion = new MySqlServerVersion(new Version(8, 0, 26));

var connectionString = builder.Configuration.GetConnectionString("MariaDB");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseMySql(connectionString, serverVersion)
);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
         .AddCookie(options =>
         {
             options.LoginPath = "/Account/Login/";

         });

builder.Services.AddIdentity<IdentityUser, IdentityRole>(options =>
{
    options.Password.RequiredLength = 6; // Configure as needed
    options.Password.RequireNonAlphanumeric = false;
})
.AddEntityFrameworkStores<ApplicationDbContext>()
.AddDefaultTokenProviders();

builder.Services.AddMvc();

// Add services to the container.
builder.Services.AddControllersWithViews();
var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var dbContext = services.GetService<ApplicationDbContext>();

    if (dbContext != null && dbContext.Database.GetDbConnection().State != ConnectionState.Open)
    {
        dbContext.Database.OpenConnection();

        // Run EnsureCreated() to create the database and any pending migrations
        dbContext.Database.EnsureCreated();
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "Create",
    pattern: "Home/Create",
    defaults: new { controller = "Home", action = "Create" });

app.MapControllerRoute(
    name: "Details",
    pattern: "Home/Details/{id?}", // o parâmetro {id?} torna o ID opcional
    defaults: new { controller = "Home", action = "Details" });

app.MapControllerRoute(
    name: "Delete",
    pattern: "Home/Delete/{id?}", // o parâmetro {id?} torna o ID opcional
    defaults: new { controller = "Home", action = "Delete" });

app.MapControllerRoute(
    name: "Edit",
    pattern: "Home/Edit/{id?}", // o parâmetro {id?} torna o ID opcional
    defaults: new { controller = "Home", action = "Edit" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
