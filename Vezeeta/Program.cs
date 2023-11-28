using DependencyInjection;
using Microsoft.EntityFrameworkCore;
using Repository;
using System.Configuration;

var builder = WebApplication.CreateBuilder(args);


DependencyConfig.ConfigureDependencies(builder.Services);
builder.Services.AddSwaggerGen();

// add connection string 
builder.Configuration.AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
builder.Services.AddDbContext<ApplictaionDbContext>(optionBuilder => {
    optionBuilder.UseSqlServer(builder.Configuration.GetConnectionString("VezeetaDB"));
});

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
