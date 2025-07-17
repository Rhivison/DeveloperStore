using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.DeleteProduct
{   
    /// <summary>
    /// Request model to delete a product.
    /// </summary>
    public class DeleteProductRequest
    {
        public Guid Id { get; set; }
    }
}