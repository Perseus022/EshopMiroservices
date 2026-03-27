namespace Catalog.Api.Catalog.Producrs.CreateProduct
{
    public record CreateProductRequest(
        string Name, List<string> Category, string Description, string ImageFile, decimal Price
    );
    public record CreateProductResponse(Guid Id);

    public class CreateProductEndpoint : ICarterModule
    {
        void ICarterModule.AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/products", async (CreateProductRequest request, ISender send) =>
            {
                var command = request.Adapt<CreateProductCommand>();

                var result = await send.Send(command);

                var response = result.Adapt<CreateProductResponse>();

                return Results.Created($"/products/{response.Id}", response);
            })
            .WithName("CreateProduct")
            .WithSummary("Create a new product")
            .WithDescription("Creates a new product in the catalog.")
            .Produces<CreateProductResponse>(StatusCodes.Status201Created)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .ProducesProblem(StatusCodes.Status500InternalServerError);
        }
    }
}
