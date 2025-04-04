using Abstracciones.BC;
using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.Modelo;
using BC;
using BW;
using DA;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Autorizacion.Abstracciones.BW;
using DA.Repositorios;
using Autorizacion.Middleware;

var builder = WebApplication.CreateBuilder(args);

//Variables para la config del Token
var tokenConfiguration = builder.Configuration.GetSection("Token").Get<TokenConfiguracion>();
var jwtIssuer = tokenConfiguration.Issuer;
var jwtAudience = tokenConfiguration.Audience;
var jwtKey = tokenConfiguration.key;

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
    options => { //Validaciones para el JWT
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {          
            //Lo que va tener que validar
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            
            //Los valores que son validos para estas validaciones, que vienen desde el AppSettings
            ValidIssuer = jwtIssuer,
            ValidAudience = jwtAudience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    }
  );

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Dapper
builder.Services.AddScoped<IRepositorioDapper, RepositorioDapper>();

//Usuario
builder.Services.AddScoped<IUsuarioBW, UsuarioBW>();
builder.Services.AddScoped<IUsuarioDA, UsuarioDA>();

//Autenticacion
builder.Services.AddScoped<IAutenticacionBC, AutenticacionBC>();
builder.Services.AddScoped<IAutenticacionBW, AutenticacionBW>();

builder.Services.AddTransient<IAutorizacionBW, Autorizacion.BW.AutorizacionBW>();
builder.Services.AddTransient<Autorizacion.Abstracciones.DA.ISeguridadDA, Autorizacion.DA.SeguridadDA>();
builder.Services.AddTransient<Autorizacion.Abstracciones.DA.IRepositorioDapper, Autorizacion.DA.Repos.RepositorioDapper>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.AutorizacionClaims(); //Autorizacion desde nuestro servicio
app.UseAuthorization(); 

app.MapControllers();

app.Run();
