﻿





namespace Basket.API.Basket.GetBasket
{
    public record GetBasketResponse(ShoppingCart cart);
    public class GetBasketEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/basket/{userName}", async (string userName, ISender sender) =>
            {
                var res = await sender.Send(new GetBasketQuery(userName));
                var resp = res.Adapt<GetBasketResponse>();
                return Results.Ok(resp);

            }).WithName("GetBasket")
            .Produces<GetBasketResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get Basket")
            .WithDescription("Get Basket");
        }
    }
}
