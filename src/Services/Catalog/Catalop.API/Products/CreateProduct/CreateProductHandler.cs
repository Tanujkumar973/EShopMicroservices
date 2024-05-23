using MediatR;

namespace Catalop.API.Products.CreateProduct
{

    public record CreateProductCommand(string Name,List<String> Category,decimal price, string ImageFile, string Description)
         : IRequest<CreateProductResult>;
    public record CreateProductResult(Guid Id);
    internal class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        public Task<CreateProductResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
        {
            //business logic
            throw new NotImplementedException();
        }
    }
}
