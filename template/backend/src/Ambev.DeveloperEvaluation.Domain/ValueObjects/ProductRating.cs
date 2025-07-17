using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{
    public class ProductRating
    {
        public decimal Rate { get; set; }
        public int Count { get; set; }
    }
}