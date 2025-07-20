using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Events.Sale
{
    public class SaleModifiedEvent
    {
        public Guid SaleId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Guid UserId { get; set; }
        public decimal NewTotal { get; set; }
    }
}