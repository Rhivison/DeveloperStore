using MediatR;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale
{
    public class UpdateSaleCommand:  IRequest<UpdateSaleResult>
    {
        public Guid Id { get; set; } // saleId
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<UpdateSaleItemDto> Products { get; set; } = [];
        public uint xmin { get; set; }
    }
}