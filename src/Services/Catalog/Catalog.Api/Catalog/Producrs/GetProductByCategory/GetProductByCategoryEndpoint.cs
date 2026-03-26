
using Catalog.Api.Catalog.Producrs.GetProductById;

namespace Catalog.Api.Catalog.Producrs.GetProductByCategory
{
    //public record GetProductByCategoryRequest(string Category);
    public record GetproductbyCategoryResponse(IEnumerable<Product> Products);
    public class GetProductByCategoryEndpoint : ICarterModule
    {
        void ICarterModule.AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var query = new GetProductByCategoryQuery(category);
                var result = await sender.Send(query);
                return Results.Ok(new GetproductbyCategoryResponse(result.Products));
            })
            .WithName("GetProductsByCategory")
            .WithSummary("Retrieves all products by their category.")
            .WithDescription("This endpoint retrieves all products from the catalog using the Product Category.")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status400BadRequest);

        }
    }
}
