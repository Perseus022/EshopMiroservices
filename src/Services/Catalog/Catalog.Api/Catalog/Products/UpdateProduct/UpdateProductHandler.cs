namespace Catalog.Api.Catalog.Producrs.UpdateProduct;

public record UpdateProductCommand(Guid Id, string Name, string Description, decimal Price, List<string> Category)
    : ICommand<UpdateProductResult>;
public record UpdateProductResult(bool IsSucess);

public class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
        RuleFor(x => x.Name).NotEmpty().WithMessage("Product Name is required.")
                            .Length(2, 150).WithMessage("Product Name must be between 2 and 150 characters.");
        RuleFor(x => x.Price).GreaterThan(0).WithMessage("Product Price must be greater than zero 0.");
    }
}
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
