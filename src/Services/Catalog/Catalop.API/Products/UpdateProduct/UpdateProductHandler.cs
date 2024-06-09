
namespace Catalog.API.Products.UpdateProduct
{

    public record UpdateProductCommand(Guid Id, List<string> Category, string Name, string Description, decimal Price, string ImageFile)
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);
    public class UpdateProductHandler(ILogger<UpdateProductHandler> logger, IDocumentSession session) :
      ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand commandReq, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(commandReq.Id);
            if (product is null)
            {
                throw new ProductNotFoundException();
            }
            product.Name = commandReq.Name;
            product.Description = commandReq.Description;
            product.Price = commandReq.Price;
            product.ImageFile = commandReq.ImageFile;
            product.Category = commandReq.Category;
             session.Update(product);
            await session.SaveChangesAsync(cancellationToken);
            return new UpdateProductResult(true);
        }
    }
}
