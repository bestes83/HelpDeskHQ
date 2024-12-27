using HelpDeskHQ.Core;
using HelpDeskHQ.Infrastructure;
using HelpDeskHQ.Persistence;
using HelpDeskHQ.Security;
using Azure.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services
    .RegisterInfrastructure(builder.Configuration)
    .RegisterDataLayer()
    .RegisterServices()
    .AddSwaggerGen();

//builder.Services.Configure<Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration>(config =>
//{
//config.SetAzureTokenCredential(new DefaultAzureCredential());
//});

//builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
//{
//    ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
//});

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
