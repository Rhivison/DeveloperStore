using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales
{
    public class GetSalesRequestValidator: AbstractValidator<GetSalesRequest>
    {
        public GetSalesRequestValidator()
        {
            RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page must be at least 1");

            RuleFor(x => x.Size)
                .InclusiveBetween(1, 100).WithMessage("Size must be between 1 and 100");
        }
    }
}