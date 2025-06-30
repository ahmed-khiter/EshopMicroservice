
namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteProductResponse(bool IsSuccess);

    public class DeleteProductEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
            {
                var result = sender.Send(new DeleteRequest(id));

                var response = result.Adapt<DeleteProductResponse>();

                return Results.Ok(response);

            });
        }
    }
}
