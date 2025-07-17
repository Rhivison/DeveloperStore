using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        /// <summary>
        /// Gets or sets the Rate of the product  to be created.
        /// </summary>
        public decimal Rate { get; set; }

        /// <summary>
        /// Gets or sets the Count of the product  to be created.
        /// </summary>
        public int Count { get; set; }
    }
}