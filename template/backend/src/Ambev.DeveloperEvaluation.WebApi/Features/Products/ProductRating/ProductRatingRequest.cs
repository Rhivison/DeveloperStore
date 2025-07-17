using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.ProductRating
{
    public class ProductRatingRequest
    {
        
        public decimal Rate { get; set; }
        public int Count { get; set; }
    }
}