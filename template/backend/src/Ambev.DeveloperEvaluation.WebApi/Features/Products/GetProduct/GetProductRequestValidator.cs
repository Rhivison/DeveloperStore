using Ambev.DeveloperEvaluation.Domain.Validation;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{   
    /// <summary>
    /// Validator for GetProductRequest that defines validation rules for Product creation.
    /// </summary>
    public class GetProductRequestValidator: AbstractValidator<GetProductRequest>
    {   

        private static readonly string[] AllowedOrderFields =
        {
            "title", "price", "category", "description"
        };
        
        /// <summary>
        /// Initializes a new instance of the GetProductRequestValidator with defined validation rules.
        /// </summary>
        public GetProductRequestValidator()
        {
            RuleFor(x => x.Page)
            .GreaterThanOrEqualTo(1).WithMessage("Page must be at least 1");

            RuleFor(x => x.Size)
                .InclusiveBetween(1, 100).WithMessage("Size must be between 1 and 100");
        }

        private bool BeValidOrdering(string? order)
        {
            if (string.IsNullOrWhiteSpace(order)) return true;

            var fields = order.Split(',')
                            .Select(x => x.Trim().ToLower().Replace(" desc", "").Replace(" asc", ""));

            return fields.All(f => AllowedOrderFields.Contains(f));
        }
    }
}