namespace Basket.API.Basket.DeleteBasket
{
    public record DeleteBasketCommand(string userName) : ICommand<DeleteBasketResult>;

    public record DeleteBasketResult(bool IsSuccess);


    public class DeleteBasketCommandValidator : AbstractValidator<DeleteBasketCommand>
    {
        public DeleteBasketCommandValidator()
        {
            RuleFor(x => x.userName).NotEmpty().WithMessage("User name cannot be empty.");
        }
    }


    public class DeleteBasketHandler(IBasketRepository repository) : ICommandHandler<DeleteBasketCommand, DeleteBasketResult>
    {

        public async Task<DeleteBasketResult> Handle(DeleteBasketCommand request, CancellationToken cancellationToken)
        {
            //TODO: Delete the basket for the given user name from cach and databse
            var result = await repository.DeleteBasketAsync(request.userName, cancellationToken);

            return new DeleteBasketResult(result);
        }
    }
}
