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
    ( IDocumentSession session)
    : IRequestHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await session.LoadAsync<Product>(command.Id,cancellationToken);
        if (product is null)
        {
            throw new ProductNotFoundException(command.Id);
        }
        product.Name = command.Name;
        product.Description = command.Description;
        product.Price = command.Price;
        product.Category = command.Category;

        session.Update(product);
        await session.SaveChangesAsync();

        return new UpdateProductResult(true);

    }
}
