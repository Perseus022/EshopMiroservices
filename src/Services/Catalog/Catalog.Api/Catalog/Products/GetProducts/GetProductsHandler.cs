namespace Catalog.Api.Catalog.Producrs.GetProducts
{
    public record GetProductsQuery() : IQuery<GetproductResult>;
    public record GetproductResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> loger)
        : IQueryHandler<GetProductsQuery, GetproductResult>
    {
        public async Task<GetproductResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            loger.LogInformation("Handling GetProductsQuery called with {@Query}",request);
            var products = await session.Query<Product>().ToListAsync(cancellationToken);
            return new GetproductResult(products);
        }
    }
}
