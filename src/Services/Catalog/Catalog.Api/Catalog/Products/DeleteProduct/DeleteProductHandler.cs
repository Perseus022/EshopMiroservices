
namespace Catalog.Api.Catalog.Producrs.DeleteProduct;

public record DeleteProductCommand(Guid Id) : IRequest<DeleteProductResult>;
public record DeleteProductResult(bool IsDeleted);

public class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommand>
{
    public DeleteProductCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required.");
    }
}

public class DeleteProducCommandtHandler(IDocumentSession session)
    : IRequestHandler<DeleteProductCommand, DeleteProductResult>
{
    public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
    {
         session.Delete<Product>(command.Id);
        await session.SaveChangesAsync(cancellationToken);

        return new DeleteProductResult(true);
    }
}
