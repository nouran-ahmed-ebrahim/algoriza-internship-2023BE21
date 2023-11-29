<<<<<<< HEAD
using Core.Domain;
using Core.Repository;
=======
>>>>>>> parent of d8f9c5e (call SpecializationInitializer)
using DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Repository;
using System;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);

// add connection string 
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddDbContext<ApplictationDbContext>(optionBuilder => {
    optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("VezeetaDB"));
});

<<<<<<< HEAD
DependencyConfig.ConfigureDependencies(builder.Services);
builder.Services.AddSwaggerGen();


#region SeeddigLookup
//Resolve the SpecializationInitializer from the DI container
var specializationInitializer = DependencyConfig.serviceProvider.GetService<SpecializationInitializer>();

// Call the Initialize method
specializationInitializer?.Initialize();
#endregion

=======
>>>>>>> parent of d8f9c5e (call SpecializationInitializer)
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
