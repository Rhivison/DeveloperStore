using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleResponse
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<UpdateSaleProductResponse> Products { get; set; } = new();
    }
    public class UpdateSaleProductResponse
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }

}