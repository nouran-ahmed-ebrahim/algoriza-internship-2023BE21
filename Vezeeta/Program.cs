using Core.Domain;
using DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


DependencyConfig.ConfigureDependencies(builder.Services);
builder.Services.AddSwaggerGen();

// add connection string 
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddDbContext<ApplictationDbContext>(optionBuilder => {
    optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("VezeetaDB"));
});

// Resolve the SpecializationInitializer from the DI container
var specializationInitializer = DependencyConfig.serviceProvider.GetService<SpecializationInitializer>();

// Call the Initialize method
specializationInitializer?.Initialize();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
