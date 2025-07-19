

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById
{
    public class GetSaleByIdResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<SaleItemResponse> Products { get; set; } = new();
        public uint xmin { get; set; }
    }
    public class SaleItemResponse
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}