using Abstracciones.BW;
using Abstracciones.DA;
using Abstracciones.SG;
using Autorizacion.Abstracciones.BW;
using Autorizacion.Middleware;
using BW;
using SG;
using Helpers;
using Autorizacion.DA.Repos;
using System.Threading;
using DA;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//var tokenConfiguration = builder.Configuration.GetSection("Token").Get<TokenConfiguracion>();
//var jwtIssuer = tokenConfiguration.Issuer;
//var jwtAudience = tokenConfiguration.Audience;
//var jwtKey = tokenConfiguration.key;

//builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(
//    options =>
//    {
//        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//        {
//            ValidateIssuer = true,
//            ValidateAudience = true,
//            ValidateLifetime = true,
//            ValidateIssuerSigningKey = true,
//            ValidIssuer = jwtIssuer,
//            ValidAudience = jwtAudience,
//            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
//        };
//    }
//  );

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IRepositorioDapper, DA.Repositorio.RepositorioDapper>();

builder.Services.AddScoped<IListaRecomendadoDA, ListaRecomendadoDA>();
builder.Services.AddScoped<IListaRecomendadoBW, ListaRecomendadoBW>();

builder.Services.AddScoped<IListaxVideoDA, ListaxVideoDA>();
builder.Services.AddScoped<IListaxVideoBW, ListaxVideoBW>();

builder.Services.AddScoped<IVideoBW, VideoBW>();
builder.Services.AddScoped<IVideoSG, VideoSG>();

//Inyección de paquetes del middleware
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

//Se llama al middleware que obtiene los roles del usaurio
//app.AutorizacionClaims();
app.UseAuthorization();

app.MapControllers();

app.Run();
