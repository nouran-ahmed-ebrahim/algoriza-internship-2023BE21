using DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// add connection string 
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddDbContext<ApplicationDbContext>(optionBuilder => {
    optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("VezeetaDB"));
});

DependencyConfig.ConfigureDependencies(builder.Services);
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// to read cookies
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
