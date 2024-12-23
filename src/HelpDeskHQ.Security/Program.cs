using HelpDeskHQ.Core;
using HelpDeskHQ.Persistence;
using HelpDeskHQ.Security;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
//builder.Services.AddOpenApi();
builder.Services
    .RegisterDataLayer()
    .RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
  //  app.MapOpenApi();
}

//app.MapV1Endpoints();

app.Run();
