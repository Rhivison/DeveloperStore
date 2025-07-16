using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct
{   
    /// <summary>
    /// Command for update a Product by their ID
    /// </summary>
    public class UpdateProductCommand: IRequest<UpdateProductResult>
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public decimal Rate { get; set; }
        public int Count { get; set; }
    }
}