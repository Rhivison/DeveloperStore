using AutoMapper;
using MediatR;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Application.DTOs;
namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory
{
    public class GetProductsByCategoryHandler: IRequestHandler<GetProductsByCategoryCommand, GetProductsByCategoryResult>
    {
        private readonly IProductRepository _productRepository;
        private readonly IMapper _mapper;

        public GetProductsByCategoryHandler(IProductRepository repository, IMapper mapper)
        {
            _productRepository = repository;
            _mapper = mapper;
        }

        public async Task<GetProductsByCategoryResult> Handle(GetProductsByCategoryCommand request, CancellationToken cancellationToken)
        {
            var validator = new GetProductsByCategoryValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);
            if (!validationResult.IsValid)
                throw new ValidationException(validationResult.Errors);

            var products = await _productRepository.GetByCategoryAsync(request.Category, cancellationToken);

            if (!string.IsNullOrWhiteSpace(request.OrderBy))
            {
                products = ApplyOrdering(products, request.OrderBy);
            }

            var totalItems = products.Count;

            var skip = (request.Page - 1) * request.Size;
            var paginated = products.Skip(skip).Take(request.Size).ToList();
            var totalPages = (int)Math.Ceiling(totalItems / (double)request.Size);

            var dtoList = _mapper.Map<List<ProductDto>>(paginated);

            return new GetProductsByCategoryResult
            {
                Data = dtoList,
                TotalItems = totalItems,
                CurrentPage = request.Page,
                TotalPages = totalPages
            };
            
        }

        private List<Domain.Entities.Product> ApplyOrdering(List<Domain.Entities.Product> products, string order)
        {
            var orders = order.Split(',', StringSplitOptions.RemoveEmptyEntries)
                              .Select(o => o.Trim().ToLowerInvariant());

            IOrderedEnumerable<Domain.Entities.Product>? ordered = null;

            foreach (var ord in orders)
            {
                var parts = ord.Split(' ', StringSplitOptions.RemoveEmptyEntries);
                var field = parts[0];
                var descending = parts.Length > 1 && parts[1] == "desc";

                Func<Domain.Entities.Product, object> keySelector = field switch
                {
                    "title" => p => p.Title,
                    "price" => p => p.Price,
                    _ => p => p.Title // default
                };

                if (ordered == null)
                {
                    ordered = descending ? products.OrderByDescending(keySelector) : products.OrderBy(keySelector);
                }
                else
                {
                    ordered = descending ? ordered.ThenByDescending(keySelector) : ordered.ThenBy(keySelector);
                }
            }

            return ordered?.ToList() ?? products;
        }
    }
}