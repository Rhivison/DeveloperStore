using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductsValidator: AbstractValidator<GetProductCommand>
    {   

        private static readonly string[] AllowedOrderFields =
        {
            "title", "price", "category", "description"
        };
        /// <summary>
        /// Initializes validation rules for GetProductCommand
        /// </summary>
        public GetProductsValidator()
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