using System.Reflection;
using RailFlow.Application;
using Railflow.Core;
using RailFlow.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCore()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);

var app = builder.Build();

app.UseInfrastructure();
app.Run();