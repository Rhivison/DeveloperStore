using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductValidator: AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(255);
            RuleFor(x => x.Price).GreaterThanOrEqualTo(0);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.Image).NotEmpty().MaximumLength(500);
            RuleFor(x => x.Rating).NotNull();
            RuleFor(x => x.Rating.Rate).InclusiveBetween(0, 5);
            RuleFor(x => x.Rating.Count).GreaterThanOrEqualTo(0);
        }
    }
}