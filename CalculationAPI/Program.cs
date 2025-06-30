using CalculationAPI.Factory;
using CalculationAPI.Interface;
using CalculationAPI.Operations;
using CalculationAPI.Service;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(option => option.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
builder.Services.AddScoped<IOperationService<decimal>, OperationService>();
builder.Services.AddScoped<Addition<decimal>>();
builder.Services.AddScoped<Substraction<decimal>>();
builder.Services.AddScoped<Multiplication<decimal>>();
builder.Services.AddScoped<Division<decimal>>();

builder.Services.AddApiVersioning(options =>
{
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.ReportApiVersions = true; // Adds headers like api-supported-versions
});

builder.Services.AddScoped<IOperationFactory<decimal>, OperationFactory<decimal>>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
