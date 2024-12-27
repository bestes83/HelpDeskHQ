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

builder.Services.Configure<Microsoft.ApplicationInsights.Extensibility.TelemetryConfiguration>(config =>
{
    config.SetAzureTokenCredential(new DefaultAzureCredential());
});

builder.Services.AddApplicationInsightsTelemetry(new Microsoft.ApplicationInsights.AspNetCore.Extensions.ApplicationInsightsServiceOptions
{
    //ConnectionString = builder.Configuration["APPLICATIONINSIGHTS_CONNECTION_STRING"]
    ConnectionString = "InstrumentationKey=25edfd7c-07b7-4f8b-8cde-6ac4e09ced89;IngestionEndpoint=https://southcentralus-3.in.applicationinsights.azure.com/;LiveEndpoint=https://southcentralus.livediagnostics.monitor.azure.com/;ApplicationId=3eac1e8d-9291-4783-bd78-a6754c631ef6"
});

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
