
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductRating;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory
{
    public class GetProductsByCategoryResponse
    {
        public List<ProductByCategoryResponseItem> Data { get; set; } = new();
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

    public class ProductByCategoryResponseItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public ProductRatingResponse Rating { get; set; } = new();
    }
}