
using Basket.API.Basket.GetBasket;
using Mapster;

namespace Basket.API.Basket.StoreBasket
{

    public record StoreBasketRequest(ShoppingCart Cart);

    public record StoreBasketResponse(bool IsSuccess);
    public class StoreBasketEndpoints : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapPost("/basket", async (StoreBasketRequest request, ISender sender) =>
            {
                var command = request.Adapt<StoreBasketCommand>();
                var result = await sender.Send(command);
                var response = result.Adapt<StoreBasketResponse>();

                return Results.Created($"/basket/{request.Cart.UserName}", response);

            }).WithName("StoreBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Store Basket")
            .WithDescription("Store Basket");
        }
    }
}
