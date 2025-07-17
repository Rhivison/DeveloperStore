using System;
using System.Collections.Generic;
using System.Linq;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Services;


namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale: BaseEntity
    {
        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public string Customer { get; private set; }
        public string Branch { get; private set; }
        public bool Cancelled { get; private set; }

        private readonly List<SaleItem> _items = new();
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();
        public decimal TotalAmount => _items.Where(i => !i.Cancelled).Sum(i => i.TotalAmount);

        protected Sale() { }

        public Sale(string saleNumber, DateTime saleDate, string customer, string branch)
        {
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            Customer = customer;
            Branch = branch;
            Cancelled = false;
        }

        public void AddItem(SaleItem item)
        {
            _items.Add(item);
        }

        public void AddItem(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            if (quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items");

            decimal discount = DiscountCalculator.Calculate(quantity, unitPrice);
            decimal total = (quantity * unitPrice) - discount;

            var item = new SaleItem(productId, productName, quantity, unitPrice, discount, total);
            _items.Add(item);
        }

        public void Cancel()
        {
            if (Cancelled)
                throw new InvalidOperationException("Sale already cancelled");

            Cancelled = true;
        }

        public void CancelItem(Guid itemId)
        {
            var item = _items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                throw new InvalidOperationException("Item not found");

            item.Cancel();
        }

    }
}