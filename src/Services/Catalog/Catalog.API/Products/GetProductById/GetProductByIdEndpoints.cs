﻿
namespace Catalog.API.Products.GetProductById
{

    public record GetProductByIdResponse(Product Product);

    public class GetProductByIdEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = await sender.Send(new GetProductById(id));

                var response = result.Adapt<GetProductByIdResponse>();

                return Results.Ok(response);

            }).WithName("GetProductById")
        .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");

        }
    }
}
