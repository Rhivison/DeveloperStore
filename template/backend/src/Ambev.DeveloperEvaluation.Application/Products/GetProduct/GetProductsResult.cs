using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{   
    /// <summary>
    /// Response model for GetProduct operation
    /// </summary>
    public class GetProductsResult
    {   
        /// <summary>
        /// The List of products
        /// </summary>
        public List<ProductDto> Data { get; set; } = new();

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
}