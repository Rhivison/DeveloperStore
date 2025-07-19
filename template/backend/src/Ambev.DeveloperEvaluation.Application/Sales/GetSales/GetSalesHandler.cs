using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetSales
{   
    /// <summary>
    /// Handles the request to retrieve a paginated list of sales (carts).
    /// </summary>
    public class GetSalesHandler:  IRequestHandler<GetSalesCommand, GetSalesResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of <see cref="GetSalesHandler"/>.
        /// </summary>
        /// <param name="saleRepository">The repository to access sales data.</param>
        /// <param name="mapper">The AutoMapper instance for mapping entities to DTOs.</param>
        public GetSalesHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Handles the query to get a paginated list of sales with optional ordering.
        /// </summary>
        /// <param name="request">The query parameters including page, size, and ordering.</param>
        /// <param name="cancellationToken">Cancellation token.</param>
        /// <returns>A result object containing sales data and pagination metadata.</returns>
        public async Task<GetSalesResult> Handle(GetSalesCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetSalesValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var allSales = await _saleRepository.GetAllAsync(cancellationToken);

            if (!string.IsNullOrEmpty(request.OrderBy))
            {
                var order = request.OrderBy.ToLower();
                if (order.Contains("date desc"))
                    allSales = allSales.OrderByDescending(s => s.SaleDate).ToList();
                else if (order.Contains("date"))
                    allSales = allSales.OrderBy(s => s.SaleDate).ToList();
                else if (order.Contains("id desc"))
                    allSales = allSales.OrderByDescending(s => s.Id).ToList();
                else if (order.Contains("id"))
                    allSales = allSales.OrderBy(s => s.Id).ToList();
                else if (order.Contains("userid desc"))
                    allSales = allSales.OrderByDescending(s => s.UserId).ToList();
                else if (order.Contains("userid"))
                    allSales = allSales.OrderBy(s => s.UserId).ToList();
            }

            var totalItems = allSales.Count;
            var totalPages = (int)Math.Ceiling(totalItems / (double)request.Size);
            var paginatedSales = allSales
                .Skip((request.Page - 1) * request.Size)
                .Take(request.Size)
                .ToList();

            var resultData = _mapper.Map<List<GetSaleDto>>(paginatedSales);
            return new GetSalesResult
            {
                Data = resultData,
                TotalItems = totalItems,
                CurrentPage = request.Page,
                TotalPages = totalPages
            };
        }
        
    }
}