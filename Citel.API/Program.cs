using Data.Repository;
using Domain.Interfaces.Repository;
using Domain.Interfaces.Services;
using Domain.Services;
using Infra.Context;
using Microsoft.EntityFrameworkCore;
using System;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var mySqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddDbContext<DbContextBase>(options =>
                    options.UseMySql(mySqlConnection,
                    ServerVersion.AutoDetect(mySqlConnection)));

builder.Services.AddSingleton<ICategoriaRepository, CategoriaRepository>();
builder.Services.AddSingleton<IProdutoRepository, ProdutoRepository>();

builder.Services.AddSingleton<ICategoriaServices, CategoriaServices>();
builder.Services.AddSingleton<IProdutoServices, ProdutoServices>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
var devCliente = "http://localhost:44370";
app.UseCors(x =>
x.AllowAnyOrigin()
.AllowAnyMethod()
.AllowAnyHeader()
.WithOrigins(devCliente));

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();