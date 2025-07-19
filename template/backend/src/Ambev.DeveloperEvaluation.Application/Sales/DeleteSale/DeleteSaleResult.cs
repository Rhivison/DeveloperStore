using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Sales.DeleteSale
{
    public class DeleteSaleResult
    {
        public Guid Id { get; set; }
        public bool Cancelled { get; set; }
    }
}