

using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{
    public record StoreBasketCommand(ShippingCart Cart) : ICommand<StoreBasketResult>;

    public record StoreBasketResult(string UserName);

    // Validation logic can be added here if needed
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("Cart cannot be null.");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("UserName must exisit.");
        }
    }
    public class StoreBasketHandler(IBasketRepository basketRepository, DiscountProtoService.DiscountProtoServiceClient discountProto) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            //Commnucation with discount using GRPC and calculate the latest prices of product
            await DeductDiscount(command.Cart, cancellationToken);

            //TODO: store basket in database (use marten upsert - if exists update, if not insert)
            //TODO: update cach
            await basketRepository.StoreBasketAsync(command.Cart, cancellationToken);

            return new StoreBasketResult(command.Cart.UserName);
        }

        private async Task DeductDiscount(ShippingCart cart, CancellationToken cancellationToken)
        {
            foreach (var item in cart.Items)
            {
                var discount = await discountProto.GetDiscountAsync(new GetDiscountRequest { ProductName = item.ProductName });

                item.Price -= discount.Amount;

            }
        }
    }
}
