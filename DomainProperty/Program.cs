using DomainProperty.Repositories;
using AutoMapper;
using DomainProperty.Services;
using DomainProperty.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var config = new MapperConfiguration(cfg => cfg.AddProfile<AutoMapperProfile>());
var mapper = config.CreateMapper();
builder.Services.AddSingleton(mapper);
builder.Services.AddMemoryCache();

builder.Services.AddSingleton<IDataRepository>(new CsvDataRepository("PropertyValuesTest.csv"));
builder.Services.AddScoped<IPropertyService, PropertyService>();


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
