namespace Catalog.Api.Catalog.Producrs.GetProductById;

public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;
public record GetProductByIdResult(Product Product);
internal class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> loger)
    : IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
{
    public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
    {
        loger.LogInformation("Handling {QueryName} with Id: {Id}", nameof(GetProductByIdQuery), query.Id);
        var product = await session.LoadAsync<Product>(query.Id, cancellationToken);
        if (product is null)
        {
            loger.LogWarning("Product with Id: {Id} not found", query.Id);
            throw new ProductNotFoundException(query.Id);
        }

        return new GetProductByIdResult(product);
    }
}
