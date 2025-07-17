using MediatR;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductCategories
{
    public class GetProductCategoriesHandler: IRequestHandler<GetProductCategoriesCommand, GetProductCategoriesResult>
    {
        private readonly IProductRepository _productRepository;

        public GetProductCategoriesHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GetProductCategoriesResult> Handle(GetProductCategoriesCommand request, CancellationToken cancellationToken)
        {
            var categories = await _productRepository.GetAllCategoriesAsync(cancellationToken);
            return new GetProductCategoriesResult
            {
                Categories = categories.ToList()
            };
        }
    }
}