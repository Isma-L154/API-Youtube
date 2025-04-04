
using Autorizacion.Abstracciones.BW;
using Autorizacion.Abstracciones.DA;
using Autorizacion.BW;
using Autorizacion.DA;
using Autorizacion.DA.Repos;
using Autorizacion.Middleware;
using BC;
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

//Configuracion autenticacion

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(options =>
{
    options.LoginPath = "/cuenta/login";
    options.AccessDeniedPath = "/cuenta/AccessDenied";
    }
);

// Add services to the container.
builder.Services.AddRazorPages();

builder.Services.AddScoped<Configuracion>();

builder.Services.AddTransient<IAutorizacionBW, AutorizacionBW>();
builder.Services.AddTransient<ISeguridadDA, SeguridadDA>();
builder.Services.AddTransient<IRepositorioDapper, RepositorioDapper>();

builder.Services.AddScoped<Configuracion>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.AutorizacionClaims();
app.UseAuthorization();

app.MapRazorPages();

app.Run();
