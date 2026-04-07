
namespace Basket.Api.Basket.StoreBasket;

public record StoreBasketRequest(ShoppingCart Cart);
public record StoreBasketResponse(string UserName);
public class StoreBasketEndpoints : ICarterModule
{
    void ICarterModule.AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
        {
            var command = request.Adapt<StoreBasketCommand>();

            var result = await sender.Send(command);

            var response = result.Adapt<StoreBasketResponse>();

            return Results.Created($"/basket/{response.UserName}", response);
        })
        .WithName("StoreBasket")
        .WithTags("Basket")
        .Produces<StoreBasketResponse>(StatusCodes.Status201Created)
        .Produces<StoreBasketResponse>(StatusCodes.Status400BadRequest)
        .WithSummary("Stores the shopping cart for a user.")
        .WithDescription("This endpoint allows you to store the shopping cart for a user. " +
                         "The request body should contain the shopping cart details, " +
                         "and the response will include the username associated with the stored cart.");

    }
}
