using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Infra.Context;
using Microsoft.Extensions.Configuration;
using Application.Services;
using ApiRestTemplate.Controllers;
using Application.Services.Interface;
using Infra.Repository.Interface;
using Infra.Repository;
using Domain.Model;
using Domain.Model.Interface;
using ApiRestTemplate.ApiAreas;
using Domain.Utils;
using Domain.Utils.Interface;
using Domain.Model.Settings.Token;
using Application.Services.Token.Interface;
using Application.Services.Token;
using Infra.Response;
using Infra.Interface;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;


var builder = WebApplication.CreateBuilder(args);
Configurations.Initialize(builder.Configuration);
builder.Services.AddControllersWithViews();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(opt =>
{
    // Informações do Swagger
    opt.SwaggerDoc("v1", new OpenApiInfo  
    { Title = "Api Rest Template", 
      Version = "1.0.0",
      Description = "Template for future projects",

      Contact = new OpenApiContact
      {
          Name = "Teste para site qualquer",
          Url = new Uri("https://www.google.com/")
      }
    });
});

builder.Services.AddDbContext<DataContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DbRoute"),
    b => b.MigrationsAssembly("ApiRestTemplate")));

builder.WebHost.UseUrls("https://0.0.0.0:7040");

#region Dependency Injection
// Definição de url padrão para o swagger.
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddScoped<IRepository<User>, Repository<User>>(); // teste
builder.Services.AddScoped<IUserService, UsersService>();
builder.Services.AddSingleton<Utilities>();
builder.Services.AddScoped<IConfig, Configurations>();
builder.Services.AddSingleton<IUtils, Utilities>();
builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ILoginService, LoginService>();
builder.Services.AddHttpClient<IHttpRequest, HttpApi>();
//builder.Services.AddScoped<IUtils, Utilities>();
//builder.Services.AddHttpClient<IHttpRequest, HttpApi>();
#endregion

#region Authentication
//builder.Services.Configure<JwtSettings>(builder.Configuration.GetSection("Jwt"));

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(opt =>
{
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidateAudience = true,
        ValidAudience = builder.Configuration["Jwt:Audience"],
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!)),
        ValidateIssuerSigningKey = true,
    };
});

// CHECK LATER !!!!

//builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).AddCookie(opt =>
//{
//    opt.LoginPath = "/Login";
//    opt.ExpireTimeSpan = TimeSpan.FromHours(5);
//});

#endregion

builder.Services.AddAuthorization();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAuthentication();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Home}/{id?}");

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/")
    {
        context.Response.Redirect("/Home/Login");
        return;
    }
    await next();
});


//app.MapControllerRoute(
//    name: "default",
//    pattern: "/swagger");

app.Run();
