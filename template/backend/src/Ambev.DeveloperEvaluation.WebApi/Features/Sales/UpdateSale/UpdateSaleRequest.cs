using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    public class UpdateSaleRequest
    {
        public DateTime Date { get; set; }
        public List<UpdateSaleProductRequest> Products { get; set; } = new();
    }

    public class UpdateSaleProductRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}