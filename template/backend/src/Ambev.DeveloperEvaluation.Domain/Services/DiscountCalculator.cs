using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
    public class DiscountCalculator
    {
        public static decimal Calculate(int quantity, decimal unitPrice)
        {
            if (quantity < 4)
                return 0m;
            if (quantity >= 4 && quantity < 10)
                return quantity * unitPrice * 0.10m;
            if (quantity <= 20)
                return quantity * unitPrice * 0.20m;

            throw new InvalidOperationException("Cannot sell more than 20 identical items");
        }
    }
}