using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{   
    // <summary>
    /// Command for retrieving all products with pagination
    /// </summary>
    public class GetProductCommand: IRequest<GetProductsResult>
    {   
        /// <summary>
        /// The number of pages in the request
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// The size of page
        /// </summary>
        public int Size { get; set; } = 10;

        /// <summary>
        /// Sort the page by title, price, category, or description attributes
        /// </summary>
        public string? Order { get; set; }

    }
}