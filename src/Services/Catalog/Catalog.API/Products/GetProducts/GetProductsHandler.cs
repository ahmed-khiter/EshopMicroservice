using Marten.Linq.QueryHandlers;
using Marten.Pagination;

namespace Catalog.API.Products.GetProducts
{
    public record GetProducts(int? pageSize = 10, int? pageNumber = 1) : IQuery<GetProductsResults>;

    public record GetProductsResults(IEnumerable<Product> Products);

    public class GetProductsHandler(IDocumentSession session) : IQueryHandler<GetProducts, GetProductsResults>
    {
        public async Task<GetProductsResults> Handle(GetProducts request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(request.pageNumber ?? 1, request.pageSize ?? 10, cancellationToken);

            return new GetProductsResults(products);    
        }
    }
}
