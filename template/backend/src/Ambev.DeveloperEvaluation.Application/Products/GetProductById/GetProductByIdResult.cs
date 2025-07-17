using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById
{
    public class GetProductByIdResult
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = default!;
        public decimal Price { get; set; }
        public string Description { get; set; } = default!;
        public string Category { get; set; } = default!;
        public string Image { get; set; } = default!;

        public ProductRatingDto Rating { get; set; } = new();
    }
}