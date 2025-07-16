using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct
{
    public class GetProductCommand: IRequest<GetProductsResult>
    {
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string? Order { get; set; }

    }
}