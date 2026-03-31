namespace Catalog.Api.Catalog.Producrs.GetProducts
{
    public record GetProductsQuery(int? PageNumber = 1, int? Pagesize = 10) : IQuery<GetproductResult>;
    public record GetproductResult(IEnumerable<Product> Products);
    internal class GetProductsQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductsQuery, GetproductResult>
    {
        public async Task<GetproductResult> Handle(GetProductsQuery query, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().ToPagedListAsync(query.PageNumber ?? 1, query.Pagesize ?? 10, cancellationToken);
            return new GetproductResult(products);
        }
    }
}
