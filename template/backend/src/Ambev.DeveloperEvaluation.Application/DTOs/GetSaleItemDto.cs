using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ambev.DeveloperEvaluation.Application.DTOs
{
    public class GetSaleItemDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public uint xmin { get; set; }
    }
}