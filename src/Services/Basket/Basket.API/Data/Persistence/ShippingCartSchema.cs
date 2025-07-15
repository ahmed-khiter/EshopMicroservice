namespace Basket.API.Data.Persistence
{
    public static class ShippingCartSchema
    {
        //Configuration Model or domain classes 
        public static void Configure(StoreOptions options)
        {
            options.Schema.For<ShippingCart>()
                .Identity(x => x.UserName);
        }
    }
}
