
using BuildingBlocks.CQRS;
using Catalog.API.Models;
using FluentValidation;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, decimal Price, string iImageFile) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);

    public class CreateProductValidator : AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Product name is required.");
            RuleFor(x => x.Category).NotEmpty().WithMessage("At least one category is required.");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Product description is required.");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Price must be greater than zero.");
            RuleFor(x => x.iImageFile).NotEmpty().WithMessage("Image file is required.");
        }
    }

    internal class CreateProductCommandHandler(IDocumentSession session)
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {

            //Create entity object from command object
            var product = new Product()
            {
                Name = command.Name,
                Category = command.Category,
                Description = command.Description,
                ImageFile = command.iImageFile,
                Price = command.Price
            };

            //TODO:
            //Save to database
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);


            return new CreateProductResult(product.Id);

        }
    }
}
