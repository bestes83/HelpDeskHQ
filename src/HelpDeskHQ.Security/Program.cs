using HelpDeskHQ.Core;
using HelpDeskHQ.Infrastructure;
using HelpDeskHQ.Persistence;
using HelpDeskHQ.Security;
using Azure.Identity;
using HelpDeskHQ.Core.Helpers.Config;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services
    .RegisterInfrastructure(builder.Configuration)
    .RegisterDataLayer()
    .RegisterServices(builder.Configuration);

builder.Services
    //.AddEndpointsApiExplorer()
    .AddSwaggerGen();

builder.Services.Configure<Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration>(config =>
{
    config.SetAzureTokenCredential(new DefaultAzureCredential());
});

builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
{
    //ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
    ConnectionString = "InstrumentationKey=25edfd7c-07b7-4f8b-8cde-6ac4e09ced89;IngestionEndpoint=https://southcentralus-3.in.applicationinsights.azure.com/;LiveEndpoint=https://southcentralus.livediagnostics.monitor.azure.com/;ApplicationId=3eac1e8d-9291-4783-bd78-a6754c631ef6"
});

var jwtConfig = builder.Configuration.GetSection(ConfigHelper.JwtSection).Get<Jwt>();
var jwtIssuer = jwtConfig.Issuer;
var jwtAudience = jwtConfig.Audience;
var jwtKey = jwtConfig.SecreteKey;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidIssuer = jwtIssuer,
        ValidAudience = jwtAudience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtKey)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
    }
);

builder.Logging
    .ClearProviders()
    .AddApplicationInsights()
    .AddConsole()
    .AddDebug();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  //  app.MapOpenApi();
  app.MapSwagger();
  app.UseSwaggerUI();
}

app.MapV1Endpoints();

app.Run();
