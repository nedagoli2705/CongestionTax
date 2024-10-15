using CongestionTax.Application.Interfaces;
using CongestionTax.Application.Services;
using CongestionTax.Domain.Services;
using CongestionTax.Domain.Strategies;
using CongestionTax.Domain.ValueObjects;
using CongestionTax.Infrastructure.Persistence;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<ITaxRepository, TaxRepository>();
builder.Services.AddScoped<ITaxStrategy, GothenburgTaxStrategy>();
builder.Services.AddScoped<TaxAppService>();
builder.Services.AddScoped<TaxCalculationService>();
builder.Services.AddHttpClient<HolidayService>();

builder.Services.Configure<List<CityTaxOptions>>(builder.Configuration.GetSection("CityTaxOptions"));

builder.Services.AddControllers();
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
