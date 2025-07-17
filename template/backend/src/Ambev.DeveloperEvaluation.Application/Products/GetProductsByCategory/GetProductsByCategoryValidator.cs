using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory
{
    public class GetProductsByCategoryValidator: AbstractValidator<GetProductsByCategoryCommand>
    {
        public GetProductsByCategoryValidator()
        {
            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required.")
                .MaximumLength(100).WithMessage("Category must not exceed 100 characters.");

            RuleFor(x => x.Page)
                .GreaterThan(0).WithMessage("Page must be greater than 0.");

            RuleFor(x => x.Size)
                .InclusiveBetween(1, 100).WithMessage("Size must be between 1 and 100.");

            RuleFor(x => x.OrderBy)
                .MaximumLength(100).When(x => !string.IsNullOrWhiteSpace(x.OrderBy))
                .WithMessage("OrderBy string is too long.");
        }
    }
}