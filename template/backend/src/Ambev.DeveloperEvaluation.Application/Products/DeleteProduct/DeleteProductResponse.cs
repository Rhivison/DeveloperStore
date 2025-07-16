using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.Products.DeleteProduct
{   
    /// <summary>
    /// Response model for DeleteProduct operation
    /// </summary>
    public class DeleteProductResponse
    {
        /// <summary>
        /// Indicates whether the deletion was successful
        /// </summary>
    public bool Success { get; set; }
    }
}