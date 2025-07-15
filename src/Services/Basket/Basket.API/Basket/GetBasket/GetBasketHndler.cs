
using Basket.API.Data;

namespace Basket.API.Basket.GetBasket
{
    public record GetBasketQuery(string UserName) : IQuery<GetBasketResult>;

    public record GetBasketResult(ShippingCart Cart);
    public class GetBasketHndler(IBasketRepository repository)
        : IQueryHandler<GetBasketQuery, GetBasketResult>
    {
        public async Task<GetBasketResult> Handle(GetBasketQuery request, CancellationToken cancellationToken)
        {
            var basket = await repository.GetBasketAsync(request.UserName, cancellationToken);

            return new GetBasketResult(basket);
        }
    }

}
