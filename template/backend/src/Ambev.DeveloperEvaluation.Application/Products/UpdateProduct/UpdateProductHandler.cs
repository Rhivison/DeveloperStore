using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{
    public class UpdateProductHandler: IRequestHandler<UpdateProductCommand, UpdateProductResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;
        
        public UpdateProductHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;

        }
        public async Task<UpdateProductResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if(!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var existingProduct = await _productRepository.GetByIdAsync(request.Id, cancellationToken);
            if(existingProduct is null)
                throw new KeyNotFoundException("Product not found.");
            
             _mapper.Map(request, existingProduct);
            await _productRepository.UpdateAsync(existingProduct, cancellationToken);
            return _mapper.Map<UpdateProductResult>(existingProduct);
        }
    }
}