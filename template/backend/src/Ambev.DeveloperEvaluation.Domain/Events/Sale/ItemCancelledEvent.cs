using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sale
{
    public class ItemCancelledEvent
    {
        public Guid SaleId { get; set; }
        public Guid ProductId { get; set; }
        public int QuantityCancelled { get; set; }
        public DateTime CancelledDate { get; set; }
    }
}