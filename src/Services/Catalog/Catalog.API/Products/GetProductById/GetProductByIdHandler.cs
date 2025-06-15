using Catalog.API.Exceptions;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductById
{
    public record GetProductById(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);

    public class GetProductByIdHandler(IDocumentSession session) : IQueryHandler<GetProductById, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductById request, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(request.Id, cancellationToken);

            if (product == null)
            {
                throw new ProductNotFoundException(request.Id);

            }

            return new GetProductByIdResult(product);
        }
    }
}
