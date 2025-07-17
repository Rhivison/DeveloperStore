using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{   /// <summary>
    /// Validator for CreateProductCommand that defines validation rules for user creation command.
    /// </summary>
    public class CreateProductValidator: AbstractValidator<CreateProductCommand>
    {
        public CreateProductValidator()
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

            RuleFor(x => x.Rating).NotNull().WithMessage("Rating is required");

            RuleFor(x => x.Rating.Rate)
                .InclusiveBetween(0, 5).WithMessage("Rate must be between 0 and 5");

            RuleFor(x => x.Rating.Count)
                .GreaterThanOrEqualTo(0).WithMessage("Count must be 0 or more");
        }
    }
}