using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{
    public class GetProductResponse
    {
         /// <summary>
        /// The List of products
        /// </summary>
        public List<ProductItem> Data { get; set; } = new();

        /// <summary>
        /// Total of products
        /// </summary>
        public int TotalItems { get; set; }

        /// <summary>
        /// The number of page
        /// </summary>
        public int CurrentPage { get; set; }

        /// <summary>
        /// The total of pages
        /// </summary>
        public int TotalPages { get; set; } 
    }

    public class ProductItem
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public ProductRatingDto Rating { get; set; } = new();
    }
}