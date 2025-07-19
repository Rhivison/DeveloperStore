using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequestValidator: AbstractValidator<UpdateSaleRequest>
    {
        public UpdateSaleRequestValidator()
        {
            RuleFor(x => x.Date).NotEmpty().WithMessage("Date is required.");

            RuleForEach(x => x.Products).ChildRules(product =>
            {
                product.RuleFor(p => p.ProductId).NotEmpty().WithMessage("ProductId is required.");
                product.RuleFor(p => p.Quantity)
                    .GreaterThan(0).WithMessage("Quantity must be greater than 0.")
                    .LessThanOrEqualTo(20).WithMessage("You cannot sell more than 20 identical items.");
            });
        }
    }
}