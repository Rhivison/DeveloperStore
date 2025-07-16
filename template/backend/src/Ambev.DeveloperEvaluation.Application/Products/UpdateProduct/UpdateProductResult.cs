using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{   
    /// <summary>
    /// Response model for Update Product operation
    /// </summary>
    public class UpdateProductResult
    {
        public ProductDto Product { get; set; } = new();
    }
}