
using Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductRating;
namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    public class CreateProductResponse
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
        public ProductRatingResponse Rating { get; set; } = new();
    }
}