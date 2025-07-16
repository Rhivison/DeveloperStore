using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductValidator: AbstractValidator<UpdateProductCommand>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.Title).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.Image).NotEmpty();
            RuleFor(x => x.Rate).InclusiveBetween(0, 5);
            RuleFor(x => x.Count).GreaterThanOrEqualTo(0);
        }
    }
}