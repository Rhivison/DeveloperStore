using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{
    public class CreateProductValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
        {
            RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Description).NotEmpty();
            RuleFor(x => x.Price).GreaterThan(0);
            RuleFor(x => x.Category).NotEmpty();
            RuleFor(x => x.Image).Must(uri => Uri.IsWellFormedUriString(uri, UriKind.Absolute));
            RuleFor(x => x.Rate).InclusiveBetween(0, 5);
            RuleFor(x => x.Count).GreaterThanOrEqualTo(0);
        }
    }
}