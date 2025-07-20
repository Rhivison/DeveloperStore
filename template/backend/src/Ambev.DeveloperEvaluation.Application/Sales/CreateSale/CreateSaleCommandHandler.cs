using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using System.Text.Json;



namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleCommandHandler: IRequestHandler<CreateSaleCommand, CreateSaleResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly IProductRepository _productRepository;
        private readonly IEventPublisher _eventPublisher;


        /// <summary>
        /// Initializes a new instance of CreateSaleCommandHandler
        /// </summary>
        public CreateSaleCommandHandler(ISaleRepository saleRepository, IProductRepository productRepository,  IMapper mapper, IEventPublisher eventPublisher)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _productRepository = productRepository;
            _eventPublisher = eventPublisher;

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

                sale.AddItem(product.Id, product.Title, item.Quantity, product.Price);
            }

            await _saleRepository.AddAsync(sale, cancellationToken);

            var saleCreatedEvent = new SaleCreatedEvent
            {
                SaleId = sale.Id,
                CustomerId = sale.Customer,
                Date = sale.SaleDate,
                Total = sale.TotalAmount,
                Items = sale.Items.Select(i => new SaleItemData
                {
                    ProductId = i.ProductId,
                    Quantity = i.Quantity,
                    UnitPrice = i.UnitPrice,
                    Discount = i.Discount
                }).ToList()
            };

            await _eventPublisher.PublishAsync("sale-created", saleCreatedEvent);
           
            var result = _mapper.Map<CreateSaleResult>(sale);
            return result;
        }
    }
}