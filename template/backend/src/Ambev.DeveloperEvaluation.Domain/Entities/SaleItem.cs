using System;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Domain.Enums;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class SaleItem: BaseEntity
    {
        // Product (External Identity)
        public Product Product { get;  set; } = new();

        // Quantity
        public int Quantity { get;  set; }
        
        // Unit price
        public decimal UnitPrice { get;  set; }
        
        // Discount
        public decimal Discount { get;  set; }
        
        // Total amount for each item
        public decimal TotalAmount { get;  set; }
        
        // Cancelled/Not Cancelled
        public bool IsCancelled { get; set; }

        public SaleItem()
        {
            Id = Guid.NewGuid();
        }
        public SaleItem(Product product, int quantity, decimal unitPrice) : this()
        {
            Product = product ?? throw new ArgumentNullException(nameof(product));
            Quantity = quantity;
            UnitPrice = unitPrice;
            IsCancelled = false;
            
            ApplyDiscount();
            CalculateTotalAmount();
        }
        public void UpdateQuantity(int newQuantity)
        {
            if (newQuantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");

            if (newQuantity > 20)
                throw new ArgumentException("Cannot sell more than 20 identical items");

            Quantity = newQuantity;
            ApplyDiscount();
            CalculateTotalAmount();
        }

        public void Cancel()
        {
            IsCancelled = true;
            TotalAmount = 0;
        }

        private void ApplyDiscount()
        {
            
            Discount = CalculateDiscount(Quantity, UnitPrice);
        }
        private void CalculateTotalAmount()
        {
            if (IsCancelled)
            {
                TotalAmount = 0;
                return;
            }

            var subtotal = Quantity * UnitPrice;
            TotalAmount = subtotal - Discount;
        }

        private decimal CalculateDiscount(int quantity, decimal unitPrice)
        {
            if (quantity < 4)
                return 0; // No discount for less than 4 items

            if (quantity >= 4 && quantity < 10)
                return (quantity * unitPrice) * 0.10m; // 10% discount

            if (quantity >= 10 && quantity <= 20)
                return (quantity * unitPrice) * 0.20m; // 20% discount

            throw new ArgumentException("Cannot sell more than 20 identical items");
        }

    }
}