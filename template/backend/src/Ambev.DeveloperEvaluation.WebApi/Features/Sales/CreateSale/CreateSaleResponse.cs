using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleResponse
    {
        public Guid UserId { get; set; }
        public DateTime Date { get; set; }
        public List<ProductResponse> Products { get; set; } = new();
    }
    public class ProductResponse
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}