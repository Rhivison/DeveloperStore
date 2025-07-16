using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{   
    /// <summary>
    /// Handler for processing GetProductsCommand requests
    /// </summary>
    public class GetProductsHandler: IRequestHandler<GetProductCommand, GetProductsResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of GetProductsHandler
        /// </summary>
        /// <param name="productRepository">The product repository</param>
        /// <param name="mapper">The AutoMapper instance</param>
        /// <param name="validator">The validator for GetProductCommand</param>
        public GetProductsHandler(IProductRepository productRepository, IMapper mapper)
        {
            _productRepository = productRepository;
            _mapper = mapper;
        }

        public async Task<GetProductsResult> Handle(GetProductCommand request, CancellationToken cancellationToken)
        {
            var allProducts = await _productRepository.GetAllAsync(cancellationToken);
            if (!string.IsNullOrEmpty(request.Order))
            {
                var order = request.Order.ToLower();
                if (order.Contains("price desc"))
                    allProducts = allProducts.OrderByDescending(p => p.Price).ToList();
                else if (order.Contains("price"))
                    allProducts = allProducts.OrderBy(p => p.Price).ToList();
                else if (order.Contains("title desc"))
                    allProducts = allProducts.OrderByDescending(p => p.Title).ToList();
                else if (order.Contains("title"))
                    allProducts = allProducts.OrderBy(p => p.Title).ToList();
            }
            var totalItems = allProducts.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)request.Size);
            var paginatedProducts = allProducts
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size)
                .ToList();
            var resultData = _mapper.Map<List<ProductDto>>(paginatedProducts);
            return new GetProductsResult
            {
                Data = resultData,
                TotalItems = totalItems,
                CurrentPage = request.Page,
                TotalPages = totalPages
            };
        }
    }
}