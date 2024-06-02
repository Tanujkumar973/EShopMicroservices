using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductsById
{

    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResponse>;

    public record GetProductByIdResponse(Product product);
    public class GetProductByIdQueryHandler(IDocumentSession session, ILogger<GetProductByIdQueryHandler> 
        logger) :
        IQueryHandler<GetProductByIdQuery, GetProductByIdResponse>
    {
        public async Task<GetProductByIdResponse> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            logger.LogInformation($"GetProductByIdQueryHandler.Handler called with {query}");
            var prod = await session.LoadAsync<Product>(query.Id,cancellationToken);
            if(prod is null)
            {
                throw new ProductNotFound();
            }
            return new GetProductByIdResponse(prod);
        }
    }
}
