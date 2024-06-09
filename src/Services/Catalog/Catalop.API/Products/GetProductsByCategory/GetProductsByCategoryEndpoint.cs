﻿
using static Catalog.API.Products.CreateProduct.CreateProductEndpoint;

namespace Catalog.API.Products.GetProductsByCategory
{

    public record GetProductByCategoryResult(IEnumerable<Product> Products);
    public class GetProductsByCategoryEndpoint : ICarterModule
    {
        public void AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/category/{category}", async (string category, ISender sender) =>
            {
                var result = await sender.Send(new GetProductsByCategoryQuery(category));
                var response = result.Adapt<GetProductByCategoryResult>();
                return Results.Ok(response);
            }).WithName("GetProductsByCategory")
        .Produces<CreateProductResponse>(StatusCodes.Status201Created)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products by category")
        .WithDescription("Get products by category");
        }
    }
}
