using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class GetSaleDto
    {
         public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<GetSaleItemDto> Products { get; set; } = new();
    }
}