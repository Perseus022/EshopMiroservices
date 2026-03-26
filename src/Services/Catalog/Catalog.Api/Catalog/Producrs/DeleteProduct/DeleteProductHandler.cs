
namespace Catalog.Api.Catalog.Producrs.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<DeleteProductResult>;
public record DeleteProductResult(bool IsDeleted);

public class DeleteProducCommandtHandler(IDocumentSession session, ILogger<DeleteProducCommandtHandler> logger)
    : IRequestHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling DeleteProductRequest for Product Id: {ProductId}", command.Id);
 
        session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);
        logger.LogInformation("Product with Id: {ProductId} deleted successfully", command.Id);

        return new DeleteProductResult(true);
    }
}
