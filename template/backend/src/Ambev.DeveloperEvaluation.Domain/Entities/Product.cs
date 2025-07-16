using System;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Services;
namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Product: BaseEntity
    {
        /// <summary>
        /// Gets or sets the product title or name.
        /// </summary>
        public string Title { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the product description.
        /// </summary>
        public string Description { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the product category.
        /// </summary>
        public string Category { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the image URL of the product.
        /// </summary>
        public string Image { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets the price of the product.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Gets or sets the product rating details.
        /// </summary>
        public ProductRating Rating { get; set; } = new();

        /// <summary>
        /// Validates the product using domain rules.
        /// </summary>
    }
}