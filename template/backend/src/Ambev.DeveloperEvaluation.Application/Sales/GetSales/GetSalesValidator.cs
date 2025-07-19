using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{   
    /// <summary>
    /// Validates the GetSalesQuery request
    /// </summary>
    public class GetSalesValidator: AbstractValidator<GetSalesCommand>
    {
        public GetSalesValidator()
        {
            RuleFor(x => x.Page)
                .GreaterThan(0)
                .WithMessage("Page must be greater than 0");

            RuleFor(x => x.Size)
                .GreaterThan(0)
                .WithMessage("Size must be greater than 0");

            RuleFor(x => x.OrderBy)
                .Matches(@"^[a-zA-Z0-9_,\s]+$") // Exemplo básico, pode customizar
                .When(x => !string.IsNullOrWhiteSpace(x.OrderBy))
                .WithMessage("OrderBy format is invalid");
        }
    }
}