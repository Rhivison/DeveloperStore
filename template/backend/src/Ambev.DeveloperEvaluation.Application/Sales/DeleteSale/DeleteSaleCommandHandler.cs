using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleCommandHandler: IRequestHandler<DeleteSaleCommand, DeleteSaleResult>
    {
        private readonly ISaleRepository _saleRepository;

        public DeleteSaleCommandHandler(ISaleRepository saleRepository)
        {
            _saleRepository = saleRepository;
        }

        public async Task<DeleteSaleResult> Handle(DeleteSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteSaleCommandValidator();
            var validation = await validator.ValidateAsync(request, cancellationToken);
            if (!validation.IsValid)
                throw new ValidationException(validation.Errors);

            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);

            if (sale is null)
                throw new KeyNotFoundException("Sale not found");

            if (sale.xmin != request.xmin)
                throw new DbUpdateConcurrencyException("Sale was modified by another process.");

            sale.Cancel();
            foreach (var item in sale.Items)
            {
                item.Cancelled = true;
            }

            var updated = await _saleRepository.UpdateAsync(sale, cancellationToken);
            if (updated is null)
                throw new InvalidOperationException("Failed to cancel sale.");
                
             return new DeleteSaleResult
            {
                Id = updated.Id,
                Cancelled = updated.Cancelled
            };
        }
    }
}