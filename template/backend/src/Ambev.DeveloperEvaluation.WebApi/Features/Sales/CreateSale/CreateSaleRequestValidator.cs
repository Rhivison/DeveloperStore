using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequestValidator:  AbstractValidator<CreateSaleRequest>
    {
        public CreateSaleRequestValidator()
        {

            RuleFor(x => x.SaleNumber)
                .NotEmpty().WithMessage("SaleNumber is required");

            RuleFor(x => x.SaleDate)
                .NotEmpty().WithMessage("SaleDate is required");

            RuleFor(x => x.Customer)
                .NotNull().WithMessage("Customer is required");

            RuleFor(x => x.Customer.Id)
                .NotEmpty().WithMessage("Customer Id is required")
                .Must(id => id != Guid.Empty).WithMessage("Customer Id must be a valid GUID");

            RuleFor(x => x.Customer.Name)
                .NotEmpty().WithMessage("Customer Name is required");

            RuleFor(x => x.Branch)
                .NotNull().WithMessage("Branch is required");

            RuleFor(x => x.Branch.Id)
                .NotEmpty().WithMessage("Branch Id is required")
                .Must(id => id != Guid.Empty).WithMessage("Branch Id must be a valid GUID");

            RuleFor(x => x.Branch.Name)
                .NotEmpty().WithMessage("Branch Name is required");

            RuleFor(x => x.Items)
                .NotEmpty().WithMessage("At least one item is required");

            RuleForEach(x => x.Items).SetValidator(new CreateSaleItemRequestValidator());
        }
    }
    public class CreateSaleItemRequestValidator : AbstractValidator<CreateSaleItemRequest>
    {
        public CreateSaleItemRequestValidator()
        {
            RuleFor(p => p.ProductId)
                .NotEmpty().WithMessage("ProductId is required")
                .Must(id => id != Guid.Empty).WithMessage("ProductId must be a valid GUID");

            RuleFor(p => p.Quantity)
                .GreaterThan(0).WithMessage("Quantity must be greater than zero")
                .LessThanOrEqualTo(20).WithMessage("Cannot sell more than 20 identical items");
        }
    }
}