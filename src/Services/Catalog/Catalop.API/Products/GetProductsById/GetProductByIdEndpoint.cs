

namespace Catalog.API.Products.GetProductsById
{

    //public record GetProductByIdResponse(Product product);
    public record GetProductByIdResponse(Product Product);
    public class GetProductByIdEndpoint : ICarterModule
    {
        void ICarterModule.AddRoutes(IEndpointRouteBuilder app)
        {
            app.MapGet("/products/{id}", async (Guid id,ISender sender) =>
            {
                var res = await sender.Send(new GetProductByIdQuery(id));
                var response = res.Adapt<GetProductByIdResponse>(); 
                return Results.Ok(response);
            }).
                 WithName("GetProductsById")
            .Produces<GetProductByIdResponse>(StatusCodes.Status200OK)
            .ProducesProblem(StatusCodes.Status400BadRequest)
            .WithSummary("Get products by id")
            .WithDescription("Get products by Id");


        }
    }
}
