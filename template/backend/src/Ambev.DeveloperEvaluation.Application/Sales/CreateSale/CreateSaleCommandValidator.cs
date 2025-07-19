using FluentValidation;
using Ambev.DeveloperEvaluation.Application.DTOs;
namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandValidator: AbstractValidator<CreateSaleCommand>
    {
        public CreateSaleCommandValidator()
        {
            RuleFor(x => x.SaleNumber).NotEmpty();
            RuleFor(x => x.SaleDate).NotEmpty();
            RuleFor(x => x.CustomerId).NotEmpty();
            RuleFor(x => x.CustomerName).NotEmpty();
            RuleFor(x => x.BranchId).NotEmpty();
            RuleFor(x => x.BranchName).NotEmpty();
            RuleFor(x => x.Items).NotEmpty();

            RuleForEach(x => x.Items).ChildRules(items =>
            {
                items.RuleFor(i => i.ProductId).NotEmpty();
                items.RuleFor(i => i.Quantity).GreaterThan(0).LessThanOrEqualTo(20);
            });
        }
    }

    
}