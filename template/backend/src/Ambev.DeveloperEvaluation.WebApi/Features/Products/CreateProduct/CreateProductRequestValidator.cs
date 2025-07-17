
using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{   
    /// <summary>
    /// Validator for CreateProductRequest that defines validation rules for Product creation.
    /// </summary>
    public class CreateProductRequestValidator: AbstractValidator<CreateProductRequest>
    {   
        

        /// <summary>
        /// Initializes a new instance of the CreateProductRequestValidator with defined validation rules.
        /// </summary>
        public CreateProductRequestValidator()
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