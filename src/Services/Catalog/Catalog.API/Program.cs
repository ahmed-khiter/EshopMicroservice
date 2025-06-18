using BuildingBlocks.Behaviors;
using BuildingBlocks.Exceptions.Handler;
using FluentValidation;

var builder = WebApplication.CreateBuilder(args);
//add Services to  the Container 

var assembly = typeof(Program).Assembly;


builder.Services.AddValidatorsFromAssembly(assembly);

builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
    config.AddOpenBehavior(typeof(ValidationBehavior<,>));
    config.AddOpenBehavior(typeof(LoggingBehaviour<,>));
});

builder.Services.AddCarter();




builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
})
    // use lightWeight it's the best practice in general there are also (IQuerySession- DirtyTrackedSession - IDocumentSession)
    .UseLightweightSessions();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();
//builder.Services.AddProblemDetails(); // optional
var app = builder.Build();

//Configure the http request pipeline
app.MapCarter();


app.UseExceptionHandler(option => { });

var xx = app.Services.GetRequiredService<IEndpointRouteBuilder>();
foreach (var endpoint in app.Services.GetRequiredService<IEndpointRouteBuilder>().DataSources
             .SelectMany(ds => ds.Endpoints))
{
    Console.WriteLine(endpoint.DisplayName);
}


app.Run();


