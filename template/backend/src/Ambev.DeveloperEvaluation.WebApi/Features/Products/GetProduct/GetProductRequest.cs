using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct
{
    public class GetProductRequest
    {
        // <summary>
        /// Command for retrieving all products with pagination
        /// </summary>
        public int Page { get; set; } = 1;

        /// <summary>
        /// The size of page
        /// </summary>
        public int Size { get; set; } = 10;

        /// <summary>
        /// Sort the page by title, price, category, or description attributes
        /// </summary>
        public string? Order { get; set; }
    }
}