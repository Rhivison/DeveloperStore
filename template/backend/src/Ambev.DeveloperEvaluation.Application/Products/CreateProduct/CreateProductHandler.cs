using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using System.Numerics;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{   
    /// <summary>
    /// Handler for processing CreateProductCommand requests
    /// </summary>
    public class CreateProductHandler: IRequestHandler<CreateProductCommand, CreateProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of CreateProductHandler
        /// </summary>
        /// <param name="productRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        public CreateProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the CreateProductCommand request
        /// </summary>
        /// <param name="command">The CreateProduct command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The created product details</returns>
        public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
        {
            var validator = new CreateProductValidator();
            var validationResult = await validator.ValidateAsync(command, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var product = new Product
            {
                Title = command.Title,
                Description = command.Description,
                Category = command.Category,
                Image = command.Image,
                Price = command.Price,
                Rating = new ProductRating
                {
                    Rate = command.Rating.Rate,
                    Count = command.Rating.Count
                }
            };

            var createdProduct = await _productRepository.CreateAsync(product, cancellationToken);
            var result = _mapper.Map<CreateProductResult>(createdProduct);
            return result;
        }
        
    }
}