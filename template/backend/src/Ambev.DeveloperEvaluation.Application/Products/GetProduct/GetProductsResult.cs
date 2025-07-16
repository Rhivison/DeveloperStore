using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductsResult
    {
        public List<ProductDto> Data { get; set; } = new();
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }
}