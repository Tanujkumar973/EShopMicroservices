﻿using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductsById
{

    public record GetProductByIdQuery(Guid Id) : IQuery<GetProductByIdResult>;

    public record GetProductByIdResult(Product Product);
    public class GetProductByIdQueryHandler(IDocumentSession session) :
        IQueryHandler<GetProductByIdQuery, GetProductByIdResult>
    {
        public async Task<GetProductByIdResult> Handle(GetProductByIdQuery query, CancellationToken cancellationToken)
        {
            var prod = await session.LoadAsync<Product>(query.Id,cancellationToken);
            if(prod is null)
            {
                throw new ProductNotFoundException(query.Id);
            }
            return new GetProductByIdResult(prod);
        }
    }
}
