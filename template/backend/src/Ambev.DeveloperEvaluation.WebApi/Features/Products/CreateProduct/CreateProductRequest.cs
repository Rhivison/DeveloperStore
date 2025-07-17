

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct
{
    public class CreateProductRequest
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

        public ProductRatingRequest Rating { get; set; } = new();
    }
    public class ProductRatingRequest
    {
        public decimal Rate { get; set; }
        public int Count { get; set; }
    }
}