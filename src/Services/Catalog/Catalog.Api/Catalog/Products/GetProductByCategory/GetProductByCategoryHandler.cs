
namespace Catalog.Api.Catalog.Producrs.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    public class GetProductByCategoryQueryHandler(IDocumentSession session)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>()
                .Where(q => q.Category.Contains(request.Category))
                .ToListAsync();

            if (products == null)
            {
                throw new ProductNotFoundException(request.Category);
            }

            return new GetProductByCategoryResult(products);
        }
    }
}
