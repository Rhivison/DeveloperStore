using MediatR;
using Ambev.DeveloperEvaluation.Application.DTOs;
namespace Ambev.DeveloperEvaluation.Application.Products.GetProductById
{   
    /// <summary>
    /// Command for retrieving a product by their ID
    /// </summary>
    public class GetProductByIdCommand:  IRequest<ProductDto>
    {   
        /// <summary>
        /// The unique identifier of the product to retrieve
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of GetProductByIdCommand
        /// </summary>
        /// <param name="id">The ID of the product to retrieve</param>
        public GetProductByIdCommand(Guid id)
        {
            Id = id;
        }
    }
}