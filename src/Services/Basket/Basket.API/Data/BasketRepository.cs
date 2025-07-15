
using Basket.API.Exceptions;

namespace Basket.API.Data
{
    public class BasketRepository(IDocumentSession session)
        : IBasketRepository
    {
        public async Task<ShippingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default)
        {
            // Logic to retrieve the basket for the given user name
            // For now, returning an empty basket as a placeholder
            var basket = await session.LoadAsync<ShippingCart>(userName, cancellationToken);

            return basket is null ? throw new BasketNotFoundException(userName) : basket;
        }
        public async Task<ShippingCart> StoreBasketAsync(ShippingCart basket, CancellationToken cancellationToken = default)
        {
            session.Store(basket);
            await session.SaveChangesAsync(cancellationToken);
            return basket;
        }
        public Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default)
        {

            // Logic to delete the basket for the given user name
            session.Delete<ShippingCart>(userName);
            return session.SaveChangesAsync(cancellationToken).ContinueWith(t => t.IsCompletedSuccessfully, cancellationToken);
        }
    }
}
