

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductById
{
    public class GetProductByIdResponse
    {   
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public string Category { get; set; } = default!;
        public decimal Price { get; set; }
        public string Description { get; set; } = default!;
        public string Image { get; set; } = default!;
        public ProductRatingResponse Rating { get; set; } = new();
    }

    public class ProductRatingResponse
    {
        public decimal Rate { get; set; }
        public int Count { get; set; }
    }
}