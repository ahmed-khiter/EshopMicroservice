namespace Catalog.API.Products.DeleteProduct
{
    public record DeleteRequest(Guid id) : ICommand<DeleteResult>;
    public record DeleteResult(bool isSuccess);

    public class DeleteProductHandler(IDocumentSession session)
        : ICommandHandler<DeleteRequest, DeleteResult>
    {
        public async Task<DeleteResult> Handle(DeleteRequest request, CancellationToken cancellationToken)
        {
            session.Delete<Product>(request.id);
            await session.SaveChangesAsync();


            return new DeleteResult(true);
        }
    }
}
