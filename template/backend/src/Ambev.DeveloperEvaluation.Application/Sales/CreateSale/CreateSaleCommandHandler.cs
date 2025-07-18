using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.AspNetCore.Mvc;



namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandHandler: IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;

        /// <summary>
        /// Initializes a new instance of CreateSaleCommandHandler
        /// </summary>
        public CreateSaleCommandHandler(ISaleRepository saleRepository, IProductRepository productRepository,  IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _productRepository = productRepository;

        }

        /// <summary>
        /// Handles the CreateSaleCommand request
        /// </summary>
        public async Task<CreateSaleResult> Handle(CreateSaleCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleCommandValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var sale = new Sale(command.SaleNumber, command.SaleDate, command.CustomerName, command.BranchName);
            sale.UserId = command.UserId;
            
            foreach (var item in command.Items)
            {
                var product = await _productRepository.GetByIdAsync(item.ProductId, cancellationToken);
                if (product == null)
                    throw new InvalidOperationException($"Product with ID {item.ProductId} not found");

                sale.AddItem(item.ProductId, product.Title, item.Quantity, item.UnitPrice);
            }

            // Persistência
            await _saleRepository.AddAsync(sale, cancellationToken);

            // Mapear entidade para DTO de resposta
            var result = _mapper.Map<CreateSaleResult>(sale);
            return result;
        }
    }
}