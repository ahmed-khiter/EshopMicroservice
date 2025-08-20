using Ordering.API;
using Ordering.Application;
using Ordering.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.

//*------------------------* 
//Infrastructure  - EF core
//Application - MediatR 
//API - Carter - HealthCheck, ...etc

builder.Services
        .AddApplicationServices()
        .AddInfrastructureServices(builder.Configuration)
        .AddApiServices();




var app = builder.Build();
app.UseApiServices();


app.MapGet("/", () => "Hello World!");

app.Run();
