
namespace Catalog.Api.Catalog.Producrs.GetProducts
{
    //public record GetProductsRequest ();
    public record GetProductsResponse (IEnumerable<Product> Products);

    public class GetProductsEndpoint : ICarterModule
    {
        void ICarterModule.AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products", async (ISender send) =>
            {
                var result = await send.Send(new GetProductsQuery());
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
