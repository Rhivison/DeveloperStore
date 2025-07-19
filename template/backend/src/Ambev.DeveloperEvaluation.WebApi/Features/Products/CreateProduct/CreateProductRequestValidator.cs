
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
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required");

            RuleFor(x => x.Category)
                .NotEmpty().WithMessage("Category is required");

            RuleFor(x => x.Image)
                .NotEmpty().WithMessage("Image is required");

            RuleFor(x => x.Price)
                .GreaterThan(0).WithMessage("Price must be greater than zero");

            RuleFor(x => x.Rate)
                .InclusiveBetween(0, 5).WithMessage("Rate must be between 0 and 5");

            RuleFor(x => x.Count)
                .GreaterThanOrEqualTo(0).WithMessage("Count must be 0 or more");
        }

        

    }
}