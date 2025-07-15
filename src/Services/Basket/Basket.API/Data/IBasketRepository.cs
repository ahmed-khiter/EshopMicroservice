namespace Basket.API.Data
{
    public interface IBasketRepository
    {
        Task<ShippingCart> GetBasketAsync(string userName, CancellationToken cancellationToken = default);
        Task<ShippingCart> StoreBasketAsync(ShippingCart cart, CancellationToken cancellationToken = default);
        Task<bool> DeleteBasketAsync(string userName, CancellationToken cancellationToken = default);
    }
}
