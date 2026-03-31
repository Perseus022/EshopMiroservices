
namespace Catalog.Api.Catalog.Producrs.GetProducts
{
    public record GetProductsRequest (int? PageNumber = 1, int? Pagesize = 10);
    public record GetProductsResponse (IEnumerable<Product> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        void ICarterModule.AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async ([AsParameters] GetProductsRequest request, ISender send) =>
            {
                var query = request.Adapt<GetProductsQuery>();
                var result = await send.Send(query);
                var response = result.Adapt<GetProductsResponse>();

                return Results.Ok(response);
            })
            .WithName("GetProducts")
            .WithSummary("Retrieves all products from the catalog.")
            .WithDescription("This endpoint retrieves a list of all products available in the catalog.")
            .Produces<GetProductsResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
