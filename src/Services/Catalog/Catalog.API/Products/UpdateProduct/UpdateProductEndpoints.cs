
namespace Catalog.API.Products.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, List<string> Category, string Description, string ImageFile, decimal Price);

    public record UpdateProductResponse(bool isSuccess);

    public class UpdateProductEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {

            app.MapPut("/products/{id:guid}", async (UpdateProductRequest request, ISender send) =>
            {

                var product = request.Adapt<UpdateProductCommand>();
                var result = await send.Send(product);

                var response = request.Adapt<UpdateProductResponse>();

                return Results.Ok(response);


            })
            .WithName("UpdateProduct")
            .WithSummary("Updates an existing product")
            .Produces<UpdateProductResult>(StatusCodes.Status200OK)
            .Produces(StatusCodes.Status404NotFound)
            .WithTags("Products");
        }
    }

}
