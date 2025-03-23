using MediatR;

namespace Catalog.API.Products.CreateProduct
{


    public record CreateProductRequest(string Name, List<string> Category, string Description, decimal Price, string iImageFile):IRequest<CreateProductResult>;

    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler : IRequestHandler<CreateProductRequest, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductRequest request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
