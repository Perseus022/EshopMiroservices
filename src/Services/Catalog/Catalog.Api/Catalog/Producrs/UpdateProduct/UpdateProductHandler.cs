namespace Catalog.Api.Catalog.Producrs.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, List<string> Category)
    : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSucess);

internal class UpdateProductCommandHandler
    ( IDocumentSession session, ILogger<UpdateProductCommandHandler> logger)
    : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        logger.LogInformation("Handling UpdateProductCommand for Product Id: {Id}", command.Id);
        var product = await session.LoadAsync<Product>(command.Id,cancellationToken);
        if (product is null)
        {
            logger.LogWarning("Product with Id: {Id} not found for update", command.Id);
            throw new ProductNotFoundException(command.Id);
        }
        product.Name = command.Name;
        product.Description = command.Description;
        product.Price = command.Price;
        product.Category = command.Category;

        session.Update(product);
        await session.SaveChangesAsync();
        logger.LogInformation("Product with Id: {Id} updated successfully", command.Id);

        return new UpdateProductResult(true);

    }
}
