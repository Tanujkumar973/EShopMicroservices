

namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductCommand(string Name,List<String> Category,decimal price, string ImageFile, string Description)
         : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler(IDocumentSession session) : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand comandReq, CancellationToken cancellationToken)
        {

            var product = new Product()
            {
                Name = comandReq.Name,
                Category = comandReq.Category,  
                Price = comandReq.price,
                ImageFile = comandReq.ImageFile,
                Description = comandReq.Description

            };
            session.Store(product);
            await session.SaveChangesAsync(cancellationToken);
            return new CreateProductResult(product.Id);
            //business logic
          //  throw new NotImplementedException();
        }
    }
}
