using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.DeleteSale
{
    public class DeleteSaleResponse
    {
        public Guid Id { get; set; }
        public bool Cancelled { get; set; }
    }
}