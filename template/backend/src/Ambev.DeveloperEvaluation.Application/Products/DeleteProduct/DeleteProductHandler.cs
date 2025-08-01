using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{   
    /// <summary>
    /// Handler for processing DeleteProductCommand requests
    /// </summary>
    public class DeleteProductHandler: IRequestHandler<DeleteProductCommand, DeleteProductResult>
    {
        private readonly IProductRepository _productRepository;

        public DeleteProductHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        /// <summary>
        /// Handles the DeleteProductCommand request
        /// </summary>
        /// <param name="request">The DeleteProductCommand command</param>
        /// <param name="cancellationToken">Cancellation token</param>
        /// <returns>The result of the delete operation</returns>
        public async Task<DeleteProductResult> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteProductValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var success = await _productRepository.DeleteAsync(request.Id, cancellationToken);
            if(!success)
                throw new KeyNotFoundException($"Product with ID {request.Id} not found");

            return new DeleteProductResult { Success = true };

        }
    }
}