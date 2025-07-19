using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommandValidator: AbstractValidator<UpdateSaleCommand>
    {
        public UpdateSaleCommandValidator()
        {
            RuleFor(c => c.Id)
            .NotEmpty().WithMessage("Cart ID is required.");

            RuleFor(c => c.UserId)
                .NotEmpty().WithMessage("User ID is required.");

            RuleFor(c => c.Date)
                .NotEmpty().WithMessage("Date is required.");

            RuleFor(c => c.Products)
                .NotNull().WithMessage("Product list cannot be null.")
                .Must(p => p.Count > 0).WithMessage("Product list must contain at least one item.");

            RuleForEach(c => c.Products).ChildRules(product =>
            {
                product.RuleFor(p => p.ProductId)
                    .NotEmpty().WithMessage("Product ID is required.");

                product.RuleFor(p => p.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than zero.")
                    .LessThanOrEqualTo(20).WithMessage("Maximum quantity allowed per item is 20.");
            });
        }
    }
}