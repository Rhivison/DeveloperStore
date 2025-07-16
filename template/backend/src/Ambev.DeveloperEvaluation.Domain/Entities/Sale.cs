using System;
using System.Collections.Generic;
using System.Linq;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Enums;


namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale: BaseEntity
    {
        // Sale number
        public string SaleNumber { get; set; } = string.Empty;

        // Date when the sale was made
        public DateTime SaleDate { get; set; }

        // Customer (External Identity)
        public Customer Customer { get; set; } = new();

        // Total sale amount
        public decimal TotalSaleAmount { get;  set; }

        // Branch where the sale was made (External Identity)
        public Branch Branch { get; set; } = new();

        // Products, Quantities, Unit prices, Discounts, Total amount for each item
        private readonly List<SaleItem> _items = new List<SaleItem>();
        public IReadOnlyList<SaleItem> Items => _items.AsReadOnly();
        
        // Cancelled/Not Cancelled
        public bool IsCancelled { get; private set; }

        public Sale()
        {
            Id = Guid.NewGuid();
            SaleDate = DateTime.Now;
        }

        public Sale(string saleNumber, Customer customer, Branch branch) : this()
        {
            SaleNumber = saleNumber ?? throw new ArgumentNullException(nameof(saleNumber));
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            Branch = branch ?? throw new ArgumentNullException(nameof(branch));
            SaleDate = DateTime.UtcNow;
            IsCancelled = false;
            TotalSaleAmount = 0;
        }

        public void AddItem(Product product, int quantity, decimal unitPrice)
        {
            if (IsCancelled)
                throw new InvalidOperationException("Cannot add items to a cancelled sale");

            if (quantity <= 0)
                throw new ArgumentException("Quantity must be greater than zero");

            if (quantity > 20)
                throw new ArgumentException("Cannot sell more than 20 identical items");

            var existingItem = _items.FirstOrDefault(i => i.Product.Id == product.Id && !i.IsCancelled);
            
            if (existingItem != null)
            {
                var newQuantity = existingItem.Quantity + quantity;
                if (newQuantity > 20)
                    throw new ArgumentException("Total quantity cannot exceed 20 items for the same product");
                
                existingItem.UpdateQuantity(newQuantity);
            }
            else
            {
                var newItem = new SaleItem(product, quantity, unitPrice);
                _items.Add(newItem);
            }

            RecalculateTotalSaleAmount();
        }

        public void CancelSale()
        {
            if (IsCancelled)
                throw new InvalidOperationException("Sale is already cancelled");

            IsCancelled = true;
        }

        public void CancelItem(Guid itemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                throw new ArgumentException("Item not found");

            item.Cancel();
            RecalculateTotalSaleAmount();
        }
        private void RecalculateTotalSaleAmount()
        {
            TotalSaleAmount = _items.Where(i => !i.IsCancelled).Sum(i => i.TotalAmount);
        }

    }
}