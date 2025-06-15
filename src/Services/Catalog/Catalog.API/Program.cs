
using Carter;

var builder = WebApplication.CreateBuilder(args);
//add Services to  the Container 

builder.Services.AddCarter();

builder.Services.AddMarten(opts =>
{
    opts.Connection(builder.Configuration.GetConnectionString("Database")!);
})
    // use lightWeight it's the best practice in general there are also (IQuerySession- DirtyTrackedSession - IDocumentSession)
    .UseLightweightSessions();


var app = builder.Build();

//Configure the http request pipeline
app.MapCarter();


app.Run();


