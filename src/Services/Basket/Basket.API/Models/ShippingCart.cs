namespace Basket.API.Models
{
    public class ShippingCart
    {
        //[Identity]
        public string UserName { get; set; } = default!;

        public List<ShoppingCartItem> Items { get; set; } = new();

        public decimal TotalPrice => Items.Sum(x => x.Quantity * x.Price);

        public ShippingCart(string userName)
        {
            UserName = userName;
        }

        //Required for mapping
        public ShippingCart()
        {
        }
    }
}
