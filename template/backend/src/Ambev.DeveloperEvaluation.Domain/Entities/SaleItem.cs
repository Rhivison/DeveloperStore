using System;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem: BaseEntity
    {
        public Guid ProductId { get;  set; } 
        public string ProductName { get;  set; } 
        public int Quantity { get;  set; }
        public decimal UnitPrice { get;  set; }
        public decimal Discount { get;  set; }
        public decimal TotalAmount { get;  set; }
        public bool Cancelled { get;  set; }
        public uint xmin { get; set; }
        public Guid SaleId { get; set; }

        public SaleItem() { }

        public SaleItem(Guid productId, string productName, int quantity, decimal unitPrice, decimal discount, decimal totalAmount)
        {
            Id = Guid.NewGuid();
            ProductId = productId;
            ProductName = productName;
            Quantity = quantity;
            UnitPrice = unitPrice;
            Discount = discount;
            TotalAmount = totalAmount;
            Cancelled = false;
        }

        public void Cancel()
        {
            if (Cancelled)
                throw new InvalidOperationException("Item already cancelled");

            Cancelled = true;
        }

    }
}