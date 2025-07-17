using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById
{   
    /// <summary>
    /// Handler for processing GetProductByIdCommand requests
    /// </summary>
    public class GetProductByIdHandler: IRequestHandler<GetProductByIdCommand, GetProductByIdResult?>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of GetProductByIdHandler
        /// </summary>
        /// <param name="userRepository">The user repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="validator">The validator for GetProductByIdCommand</param>
        public GetProductByIdHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the GetProductByIdCommand request
        /// </summary>
        /// <param name="request">The GetProductByIdCommand command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The user details if found</returns>
        public async Task<GetProductByIdResult?> Handle(GetProductByIdCommand request, CancellationToken cancellationToken)
        {

            var validator = new GetProductByIdValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var product = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            return product == null ? null : _mapper.Map<GetProductByIdResult>(product);
        }
        
    }
}