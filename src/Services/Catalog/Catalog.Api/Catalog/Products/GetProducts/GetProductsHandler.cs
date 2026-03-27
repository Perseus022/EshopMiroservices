namespace Catalog.Api.Catalog.Producrs.GetProducts
{
    public record GetProductsQuery() : IQuery<GetproductResult>;
    public record GetproductResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductsQuery, GetproductResult>
    {
        public async Task<GetproductResult> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToListAsync(cancellationToken);
            return new GetproductResult(products);
        }
    }
}
