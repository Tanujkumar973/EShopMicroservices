
using Newtonsoft.Json.Serialization;

namespace Catalog.API.Products.UpdateProduct
{

    public record UpdateProductCommand(Guid Id, List<string> Category, string Name, string Description, decimal Price, string ImageFile)
        : ICommand<UpdateProductResult>;

    public record UpdateProductResult(bool IsSuccess);

    public class UpdateProductValidator : AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id).NotEmpty().WithMessage("Product Id is required");
            RuleFor(X => X.Name).NotEmpty().WithMessage("Product Name is required").Length(2, 200).WithMessage("Length shold be min 2 and max 200");
            RuleFor(x => x.Price).GreaterThan(0).WithMessage("Producct price should be greather than 0");
        }
    }
    public class UpdateProductHandler( IDocumentSession session) :
      ICommandHandler<UpdateProductCommand, UpdateProductResult>
    {
        public async Task<UpdateProductResult> Handle(UpdateProductCommand commandReq, CancellationToken cancellationToken)
        {
            var product = await session.LoadAsync<Product>(commandReq.Id);
            if (product is null)
            {
                throw new ProductNotFoundException(commandReq.Id);
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
