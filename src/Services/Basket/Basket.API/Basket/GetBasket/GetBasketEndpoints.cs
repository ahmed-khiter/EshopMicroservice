

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketResponse(ShippingCart Cart);

    public class GetBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender mediator) =>
            {

                var result = await mediator.Send(new GetBasketQuery(userName));

                var response = result.Adapt<GetBasketResponse>();

                return Results.Ok(response);

            }).WithName("Get Basket by user name ")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Basket by user name")
            .WithDescription("Get basket by user name");
        }
    }
}
