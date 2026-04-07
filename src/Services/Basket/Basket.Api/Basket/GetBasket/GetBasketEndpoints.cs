namespace Basket.Api.Basket.GetBasket;

//public record GetBasketRequest();

public record GetBasketResponse(ShoppingCart? Cart);

public class GetBasketEndpoints : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
        {
            var result = await sender.Send(new GetBasketQuery(userName));
            var response = result.Adapt<GetBasketResponse>();
            return Results.Ok(response);
        })
        .WithName("GetBasket")
        .WithSummary("Retrieves the Basket.")
        .WithDescription("This endpoint retrieves the Basket of the specified UserName.")
        .Produces<GetBasketResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status500InternalServerError)
        .ProducesProblem(StatusCodes.Status400BadRequest);
    }
}
