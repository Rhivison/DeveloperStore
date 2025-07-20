using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sale
{
    public class SaleCancelledEvent
    {
        public Guid SaleId { get; set; }
        public DateTime CancelledDate { get; set; }
    }
}