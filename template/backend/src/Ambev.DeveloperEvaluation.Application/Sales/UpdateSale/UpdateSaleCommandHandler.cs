using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.DTOs;
using Microsoft.EntityFrameworkCore;
namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{   
    /// <summary>
    /// Handles the update of a Sale (Cart) entity, including updating its metadata and replacing all items.
    /// </summary>
    public class UpdateSaleCommandHandler: IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
    {   
        private readonly ISaleRepository _saleRepository;
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="UpdateSaleCommandHandler"/> class.
        /// </summary>
        /// <param name="saleRepository">Repository to access sales.</param>
        /// <param name="productRepository">Repository to fetch product details.</param>
        /// <param name="mapper">Mapper for converting domain entities to result objects.</param>
         public UpdateSaleCommandHandler(ISaleRepository saleRepository, IProductRepository productRepository, IMapper mapper)
         {
            _saleRepository = saleRepository;
            _productRepository = productRepository;
            _mapper = mapper;
         }

        /// <summary>
        /// Handles the update sale command.
        /// Fetches the existing sale, replaces user and date info,
        /// clears existing items, and adds new ones based on product IDs and quantities.
        /// </summary>
        /// <param name="request">The update command with new data.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>An <see cref="UpdateSaleResult"/> with the updated data.</returns>
        /// <exception cref="KeyNotFoundException">Thrown if sale or product does not exist.</exception>
        public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
            if (sale is null)
                throw new KeyNotFoundException("Sale not found.");

            // Verifica se o xmin bate
            if (sale.xmin != request.xmin)
                throw new DbUpdateConcurrencyException("The sale has been modified by another process.");
            Console.WriteLine($"{sale.xmin} - {request.xmin}");
            
            // Atualiza campos da venda
            sale.SaleDate = request.Date;
            sale.UserId = request.UserId;

            var productDtos = request.Products;
            var products = new Dictionary<Guid, Product>();
            foreach (var prodDto in productDtos)
            {
                var product = await _productRepository.GetByIdAsync(prodDto.ProductId, cancellationToken);
                if (product == null)
                    throw new KeyNotFoundException($"Product {prodDto.ProductId} not found.");
                products[prodDto.ProductId] = product;
            }

            var updatedItems = productDtos.Select(p => (
                productId: p.ProductId,
                quantity: p.Quantity,
                productName: products[p.ProductId].Title,
                unitPrice: products[p.ProductId].Price,
                xmin: p.xmin
            )).ToList();

            sale.UpdateSaleItems(updatedItems);
            var updatedSale = await _saleRepository.UpdateAsync(sale, cancellationToken);
            if (updatedSale == null)
                throw new InvalidOperationException("Failed to update sale.");
            return _mapper.Map<UpdateSaleResult>(updatedSale);
        }
    }
}