using System;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem: BaseEntity
    {
        public Guid ProductId { get; private set; }       // referência externa
        public string ProductName { get; private set; }     // descrição denormalizada
        public int Quantity { get; private set; }
        public decimal UnitPrice { get; private set; }
        public decimal Discount { get; private set; }
        public decimal TotalAmount { get; private set; }
        public bool Cancelled { get; private set; }

        protected SaleItem() { }

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