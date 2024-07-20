

using Discount.Grpc;

namespace Basket.API.Basket.StoreBasket
{

    public record StoreBasketCommand(ShoppingCart Cart) : ICommand<StoreBasketResult>;
    public record StoreBasketResult(bool IsSuccess);
    public class StoreBasketCommandValidator : AbstractValidator<StoreBasketCommand>
    {
        public StoreBasketCommandValidator()
        {
            RuleFor(x => x.Cart).NotNull().WithMessage("cart cannot be empty");
            RuleFor(x => x.Cart.UserName).NotEmpty().WithMessage("username is required");

        }
    }
    public class StoreBasketCommandHandler(IBasketRepository repository,DiscountProtoService.DiscountProtoServiceClient discountService) : ICommandHandler<StoreBasketCommand, StoreBasketResult>
    {
        public async Task<StoreBasketResult> Handle(StoreBasketCommand command, CancellationToken cancellationToken)
        {
            foreach(var item in command.Cart.Items)
            {
               var coupon =  await discountService.GetDiscountAsync(new GetDiscountRequest() { ProductName = item.ProductName }, cancellationToken: cancellationToken);
                item.Price -= coupon.Amount;

            }
            await repository.StoreBasket(command.Cart, cancellationToken);
            return new StoreBasketResult(true);
        }
    }
}
