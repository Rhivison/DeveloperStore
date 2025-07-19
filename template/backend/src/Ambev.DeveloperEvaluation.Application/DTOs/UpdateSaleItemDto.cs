

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class UpdateSaleItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public uint xmin { get; set; }
    }
}