using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public string Description { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public string Image { get; set; } = string.Empty;
        public ProductRatingDto Rating { get; set; } = new();
    }
}