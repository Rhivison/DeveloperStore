using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory
{
    public class GetProductsByCategoryRequestValidator: AbstractValidator<GetProductsByCategoryRequest>
    {
        public GetProductsByCategoryRequestValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Page must be greater than 0.");

            RuleFor(x => x.Size)
                .InclusiveBetween(1, 100).WithMessage("Page size must be between 1 and 100.");
        }
    }
}