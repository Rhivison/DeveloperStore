using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct
{
    public class UpdateProductRequestValidator:  AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductRequestValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required")
                .MaximumLength(100);

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero");

            RuleFor(x => x.Description)
                .MaximumLength(500);

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Image is required")
                .Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute))
                .WithMessage("Image must be a valid URL");

            RuleFor(x => x.Rating).NotNull();
            RuleFor(x => x.Rating.Rate).InclusiveBetween(0, 5);
            RuleFor(x => x.Rating.Count).GreaterThanOrEqualTo(0);
        }
    }
}