﻿using Catalog.API.Products.GetProducts;
using Marten.Linq.QueryHandlers;

namespace Catalog.API.Products.GetProductsByCategory
{
    public record GetProductsByCategoryQuery(string Category) : IQuery<GetProductsByCategoryResult>;
    public record GetProductsByCategoryResult(IEnumerable<Product> Products);
    internal class GetProductsByCategoryHandler(IDocumentSession session, ILogger<GetProductsQueryHandler> logger) : 
        IQueryHandler<GetProductsByCategoryQuery, GetProductsByCategoryResult>
    {
        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
        {
            var products = await session.Query<Product>().Where(x => x.Category.Contains(request.Category)).ToListAsync();
            return new GetProductsByCategoryResult(products);
        }
    }
}
