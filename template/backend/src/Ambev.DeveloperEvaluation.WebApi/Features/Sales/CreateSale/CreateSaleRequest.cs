using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        public string SaleNumber { get; set; }
        public DateTime SaleDate { get; set; }
        public CustomerRequest Customer { get; set; }
        public BranchRequest Branch { get; set; }
        public List<CreateSaleItemRequest> Items { get; set; } = new();
    }

    public class CustomerRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class BranchRequest
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
    }

    public class CreateSaleItemRequest
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
    }
}