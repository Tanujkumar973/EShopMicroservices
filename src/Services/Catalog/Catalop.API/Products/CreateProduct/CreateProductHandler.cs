

using FluentValidation;

namespace Catalog.API.Products.CreateProduct
{

    public record CreateProductCommand(string Name,List<String> Category,decimal price, string ImageFile, string Description)
         : ICommand<CreateProductResult>;
    public record CreateProductResult(Guid Id);

    public class CreateProductCommandValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductCommandValidator() {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Name is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Desscription is required");
            RuleFor(x => x.ImageFile).NotEmpty().WithMessage("Image file is required");
            RuleFor(x => x.price).GreaterThan(0).WithMessage("Price should be greather than 0");
        }
    }
    internal class CreateProductCommandHandler(IDocumentSession session) 
        : ICommandHandler<CreateProductCommand, CreateProductResult>
    {
        public async Task<CreateProductResult> Handle(CreateProductCommand comandReq, CancellationToken cancellationToken)
        {
            //logger.LogInformation("Created product handler");
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
