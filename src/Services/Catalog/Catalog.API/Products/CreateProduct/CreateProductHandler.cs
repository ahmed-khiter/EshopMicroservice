
using BuildingBlocks.CQRS;
using Catalog.API.Models;

namespace Catalog.API.Products.CreateProduct
{
    public record CreateProductCommand(string Name, List<string> Category, string Description, decimal Price, string iImageFile) : ICommand<CreateProductResult>;

    public record CreateProductResult(Guid Id);


    internal class CreateProductCommandHandler : ICommandHandler<CreateProductCommand, CreateProductResult>
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
            //return CreateProductResult result 


            return new CreateProductResult(new Guid());

        }
    }
}
