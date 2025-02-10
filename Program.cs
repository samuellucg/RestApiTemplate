using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Infra.Context;
using Microsoft.Extensions.Configuration;
using Application.Services;
using ApiRestTemplate.Controllers;
using Application.Services.Interface;


var builder = WebApplication.CreateBuilder(args);

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

// Definição do banco de dados
builder.Services.AddDbContext<DbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("DbRoute"),
    b => b.MigrationsAssembly("ApiRestTemplate")));

// Definição de url padrão para o swagger.
builder.WebHost.UseUrls("https://0.0.0.0:7040");
builder.Services.AddScoped<IUserService, UsersService>();
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

app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

//app.MapControllerRoute(
//    name: "default",
//    pattern: "/swagger");

app.Run();
