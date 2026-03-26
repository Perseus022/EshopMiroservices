
namespace Catalog.Api.Catalog.Producrs.GetProductByCategory
{
    public record GetProductByCategoryQuery(string Category) : IQuery<GetProductByCategoryResult>;
    public record GetProductByCategoryResult(IEnumerable<Product> Products);

    public class GetProductByCategoryQueryHandler(IDocumentSession session, ILogger<GetProductByCategoryQueryHandler> loger)
        : IQueryHandler<GetProductByCategoryQuery, GetProductByCategoryResult>
    {
        public async Task<GetProductByCategoryResult> Handle(GetProductByCategoryQuery request, CancellationToken cancellationToken)
        {
            loger.LogInformation("Getting products by category: {Category}", request.Category);
            var products = await session.Query<Product>()
                .Where(q => q.Category.Contains(request.Category))
                .ToListAsync();

            if (products == null)
            {
                loger.LogWarning("No products found for category: {Category}", request.Category);
                throw new ProductNotFoundException(request.Category);
            }

            return new GetProductByCategoryResult(products);
        }
    }
}
