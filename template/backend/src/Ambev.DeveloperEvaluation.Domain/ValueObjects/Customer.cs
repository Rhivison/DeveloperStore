using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class Customer
    {
        public Guid CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerDocument { get; set; }

        public Customer() { }

        public Customer(Guid customerId, string customerName, string customerDocument)
        {
            CustomerId = customerId;
            CustomerName = customerName ?? throw new ArgumentNullException(nameof(customerName));
            CustomerDocument = customerDocument ?? throw new ArgumentNullException(nameof(customerDocument));
        }
    }
}