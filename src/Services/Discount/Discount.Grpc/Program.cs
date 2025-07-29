
using Discount.Grpc.Data;
using Discount.Grpc.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddGrpc();

builder.Services.AddSqlite<DiscountContext>(builder.Configuration.GetConnectionString("Database")!);

var app = builder.Build();
app.UseMigrationAutomatic();

// Configure the HTTP request pipeline.
app.MapGrpcService<DiscountService>();
app.MapGet("/", () => "Communication with gRPC endpoints");

app.Run();
