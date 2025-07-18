

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSales
{
    public class GetSalesResponse
    {
        public List<SaleResponse> Data { get; set; } = new();
        public int TotalItems { get; set; }
        public int CurrentPage { get; set; }
        public int TotalPages { get; set; }
    }

    public class SaleResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<SaleProductResponse> Products { get; set; } = new();
    }
    public class SaleProductResponse
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}