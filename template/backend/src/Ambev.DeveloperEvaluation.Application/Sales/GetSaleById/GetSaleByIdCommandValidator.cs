using FluentValidation;
namespace Ambev.DeveloperEvaluation.Application.Sales.GetSaleById
{
    public class GetSaleByIdCommandValidator: AbstractValidator<GetSaleByIdCommand>
    {
        public GetSaleByIdCommandValidator()
        {
            RuleFor(x => x.Id)
                .NotEmpty().WithMessage("Sale ID is required.");
        }
    }
}