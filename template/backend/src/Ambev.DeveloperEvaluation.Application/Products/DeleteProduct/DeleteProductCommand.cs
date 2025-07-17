using MediatR;
namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{   
    /// <summary>
    /// Command for delete product by ID
    /// </summary>
    public class DeleteProductCommand: IRequest<DeleteProductResult>
    {   
        /// <summary>
        /// The unique identifier of the product to delete
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// Initializes a new instance of DeleteProductCommand
        /// </summary>
        /// <param name="id">The ID of the product to delete</param>
        public DeleteProductCommand(Guid id)
        {
            Id = id;
        }

    }
}