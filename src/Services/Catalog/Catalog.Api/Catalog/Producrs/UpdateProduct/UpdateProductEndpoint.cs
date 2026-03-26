
namespace Catalog.Api.Catalog.Producrs.UpdateProduct
{
    public record UpdateProductRequest(Guid Id, string Name, string Description, decimal Price, List<string> Category);
    public record UpdateProductResponse(bool IsSucsess);
    public class UpdateProductEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapPut("/products", async (UpdateProductRequest request, ISender sender) =>
            {
                var command = request.Adapt<UpdateProductCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<UpdateProductResponse>();
                return Results.Ok(response);
            })
            .WithName("UpdateProduct")
            .WithSummary("Updates an existing product in the catalog.")
            .WithDescription("This endpoint updates the details of an existing product in the catalog using its unique identifier (GUID).")
            .Produces<UpdateProductResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status500InternalServerError)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
