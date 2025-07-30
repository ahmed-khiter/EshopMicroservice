
using Basket.API.Data.Persistence;
using BuildingBlocks.Exceptions.Handler;
using Discount.Grpc;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.Extensions.Caching.Distributed;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var assemply = typeof(Program).Assembly;

builder.Services.AddCarter();
builder.Services.AddMediatR(builder =>
{

    builder.RegisterServicesFromAssembly(assemply);
    builder.AddOpenBehavior(typeof(ValidationBehavior<,>));
    builder.AddOpenBehavior(typeof(LoggingBehavior<,>));
});

builder.Services.AddMarten(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Database");

    options.Connection(connectionString!);

    ShippingCartSchema.Configure(options);

}).UseLightweightSessions();

//Add GRPC service 
builder.Services.AddGrpcClient<DiscountProtoService.DiscountProtoServiceClient>(opt =>
{
    opt.Address = new Uri(builder.Configuration["GrpcSettings:DiscountUrl"]!);
});

builder.Services.AddScoped<IBasketRepository, BasketRepository>();
//this is the way to add caching repository as decorator, which is the recommended way to do it in .NET Core 6+
builder.Services.Decorate<IBasketRepository, CachedBasketRepository>();

// this is manual way to add caching repository as decorator.
//builder.Services.AddScoped<IBasketRepository>(provider =>
//{
//    var basketRepository = provider.GetRequiredService<BasketRepository>();
//    return new CachedBasketRepository(basketRepository, provider.GetRequiredService<IDistributedCache>());

//});

builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = builder.Configuration.GetConnectionString("Redis")!;
    //opt.InstanceName = "BasketCacheInstance";
});

builder.Services.AddHealthChecks()
    .AddNpgSql(builder.Configuration.GetConnectionString("Database")!)
    .AddRedis(builder.Configuration.GetConnectionString("Redis")!);

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();
app.MapCarter();
app.UseExceptionHandler(option => { });

app.UseHealthChecks("/health", new HealthCheckOptions
{
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
