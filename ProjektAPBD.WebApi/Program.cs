using Microsoft.EntityFrameworkCore;
using ProjektAPBD.WebApi.Configuration;
using ProjektAPBD.WebApi.External.Clients;
using ProjektAPBD.WebApi.Interfaces;
using ProjektAPBD.WebApi.Interfaces.ApiClients;
using ProjektAPBD.WebApi.Models;
using ProjektAPBD.WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services
    .AddDbContext<ManagementDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
    })
    .Configure<ExchangeApiConfiguration>(builder.Configuration.GetSection(ExchangeApiConfiguration.Section))
    .AddSingleton<IExchangeApiClient, NbpExchangeApiClient>()
    .AddScoped<IClientsManagementRepository, ClientsManagementRepository>()
    .AddScoped<IIncomeManagemenRepository, IncomeManagemenRepository>()
    .AddScoped<ISalesManagementRepository, SalesManagementRepository>()
    .AddScoped<ISubscriptionsManagementRepository, SubscriptionsManagementRepository>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.MapControllers();

app.Run();
