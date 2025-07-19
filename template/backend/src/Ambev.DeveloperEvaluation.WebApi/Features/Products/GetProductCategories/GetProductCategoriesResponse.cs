using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductCategories
{
    public class GetProductCategoriesResponse
    {
        public List<string> Categories { get; set; } = new();
    }
}