using FluentValidation;
using EspacoTech.Areas.Identity.Data;
using EspacoTech.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("EspacoTechContextConnection") ?? throw new InvalidOperationException("Connection string 'EspacoTechContextConnection' not found.");



builder.Services.AddDefaultIdentity<Usuario>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<EspacoTechLoginContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<EspacoTechLoginContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddAuthorization(options =>
{
    // Builder de autorização
    options.FallbackPolicy = new AuthorizationPolicyBuilder()
        .RequireAuthenticatedUser()
        .Build();
});

builder.Services.AddScoped<IValidator<Reserva>, ReservaValidation>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.MapRazorPages();

app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
