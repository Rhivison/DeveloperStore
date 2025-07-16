using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById
{   
    /// <summary>
    /// Validator for GetProductByIdCommand
    /// </summary>
    public class GetProductByIdValidator: AbstractValidator<GetProductByIdCommand>
    {
        /// <summary>
        /// Initializes validation rules for GetProductByIdCommand
        /// </summary>
        public GetProductByIdValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Product ID must be provided.");
        }
    }
}