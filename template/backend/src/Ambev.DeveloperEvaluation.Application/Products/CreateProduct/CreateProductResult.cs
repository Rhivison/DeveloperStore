using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct
{   
    /// <summary>
    /// Represents the response returned after successfully creating a new product.
    /// </summary>
    /// <remarks>
    /// This response contains the unique identifier of the newly created product,
    /// which can be used for subsequent operations or reference.
    /// </remarks>
    public class CreateProductResult
    {   
        /// <summary>
        /// Gets or sets the Title of the product to be created.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Description of the product  to be created.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Category of the product  to be created.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Image of the product  to be created.
        /// </summary>
        public string Image { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the Price of the product  to be created.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the Rate of the product  to be created.
        /// </summary>
        public ProductRatingDto Rating { get; set; } = new();
    }
}