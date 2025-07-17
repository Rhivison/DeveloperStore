using MediatR;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{   
    /// <summary>
    /// Command for update a Product by their ID
    /// </summary>
    public class UpdateProductCommand: IRequest<UpdateProductResult>
    {
         /// <summary>
        /// Product ID
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Product title
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Product price
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Product description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Product category
        /// </summary>
        public string Category { get; set; }

        /// <summary>
        /// Product image
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Product rating
        /// </summary>
        public ProductRatingDto Rating { get; set; }
    }
}