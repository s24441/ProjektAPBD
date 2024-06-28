using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using ProjektAPBD.WebApi.Configuration;
using ProjektAPBD.WebApi.External.Clients;
using ProjektAPBD.WebApi.Interfaces;
using ProjektAPBD.WebApi.Interfaces.ApiClients;
using ProjektAPBD.WebApi.Middleware;
using ProjektAPBD.WebApi.Models;
using ProjektAPBD.WebApi.Repositories;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options => {
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "JWT Bearer token (from login method: example users [admin:admin1] or [user1:usr123]):",
        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    options.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { 
            jwtSecurityScheme, 
            Array.Empty<string>() 
        }
    });
});
builder.Services
    .AddDbContext<ManagementDbContext>(options =>
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("Database"));
    })
    .AddDbContext<AuthDbContext>(options =>
    {
        options.UseInMemoryDatabase(databaseName: "Auth");
    })
    .Configure<ExchangeApiConfiguration>(builder.Configuration.GetSection(ExchangeApiConfiguration.Section))
    .AddSingleton<IExchangeApiClient, NbpExchangeApiClient>()
    .AddScoped<IAuthRepository, AuthRepository>()
    .AddScoped<IClientsManagementRepository, ClientsManagementRepository>()
    .AddScoped<IIncomeManagemenRepository, IncomeManagemenRepository>()
    .AddScoped<ISalesManagementRepository, SalesManagementRepository>()
    .AddScoped<ISubscriptionsManagementRepository, SubscriptionsManagementRepository>();

builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,   //by who
            ValidateAudience = true, //for whom
            ValidateLifetime = true,
            ClockSkew = TimeSpan.FromMinutes(2),
            ValidIssuer = "https://localhost:7232", //should come from configuration
            ValidAudience = "https://localhost:7232", //should come from configuration
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[AuthSettings.SecretSection]))
        };

        opt.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
                {
                    context.Response.Headers.Add("Token-expired", "true");
                }
                return Task.CompletedTask;
            }
        };
    }).AddJwtBearer("IgnoreTokenExpirationScheme", opt =>
    {
        opt.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,   //by who
            ValidateAudience = true, //for whom
            ValidateLifetime = false,
            ClockSkew = TimeSpan.FromMinutes(2),
            ValidIssuer = "https://localhost:7232", //should come from configuration
            ValidAudience = "https://localhost:7232", //should come from configuration
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration[AuthSettings.SecretSection]))
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthentication();
app.UseAuthorization();
app.UseMiddleware<ExceptionHandler>();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
