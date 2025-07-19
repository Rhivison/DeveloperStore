using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductsByCategory
{
    public class GetProductsByCategoryRequest
    {   
        public int Page { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string? OrderBy { get; set; }
    }
}