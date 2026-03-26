
namespace Catalog.Api.Catalog.Producrs.GetProductById
{
    //public record GetProductByIdRequest(Guid Id);
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        void ICarterModule.AddRoutes(IEndpointRouteBuilder app)
        {
           app.MapGet("/products/{id:guid}", async (Guid id, ISender send) =>
            {
                var result = await send.Send(new GetProductByIdQuery(id));
                var response = result.Adapt<GetProductByIdResponse>();
                return Results.Ok(response);
            })
            .WithName("GetProductById")
            .WithSummary("Retrieves a product by its unique identifier.")
            .WithDescription("This endpoint retrieves a specific product from the catalog using its unique identifier (GUID).")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status404NotFound)
            .ProducesProblem(StatusCodes.Status400BadRequest);
        }
    }
}
