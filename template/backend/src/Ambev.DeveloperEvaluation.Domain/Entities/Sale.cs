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
        public Guid UserId { get; set; }
        public string SaleNumber { get;  set; }
        public DateTime SaleDate { get;  set; }
        public string Customer { get;  set; }
        public string Branch { get;  set; }
        public bool Cancelled { get;  set; }
        public uint xmin { get; set; }

        private readonly List<SaleItem> _items = new();
        public IReadOnlyCollection<SaleItem> Items => _items.AsReadOnly();
        public decimal TotalAmount { get; set; }

        public Sale() { }

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
            RecalculateTotal();
        }

        public void RemoveItem(SaleItem item)
        {
            _items.Remove(item);
        }
        public void AddItem(Guid productId, string productName, int quantity, decimal unitPrice)
        {
            Console.WriteLine($"{productId} - {productName} - {quantity} - {unitPrice}");
            if (quantity > 20)
                throw new InvalidOperationException("Cannot sell more than 20 identical items");

            decimal discount = DiscountCalculator.Calculate(quantity, unitPrice);
            decimal total = (quantity * unitPrice) - discount;

            var item = new SaleItem(productId, productName, quantity, unitPrice, discount, total);
            Console.WriteLine($"{productId} - {productName} - {quantity} - {unitPrice} - {discount} - {total}");
            _items.Add(item);
            RecalculateTotal();
        }

        public void UpdateSaleItems(List<(Guid productId, int quantity, string productName, decimal unitPrice, uint xmin)> updatedItems)
        {
            var updatedIds = updatedItems.Select(i => i.productId).ToHashSet();

            // Marca como cancelado itens que não estão mais
            foreach (var item in _items)
            {
                if (!updatedIds.Contains(item.ProductId))
                    item.Cancelled = true;
                else
                    item.Cancelled = false;
            }

            // Atualiza itens existentes e adiciona novos
            foreach (var updated in updatedItems)
            {
                var existingItem = _items.FirstOrDefault(i => i.ProductId == updated.productId);

                if (existingItem != null)
                {
                    existingItem.Quantity = updated.quantity;
                    decimal discount = DiscountCalculator.Calculate(updated.quantity, updated.unitPrice);
                    decimal total = (updated.quantity * updated.unitPrice) - discount;
                    existingItem.Discount = discount;
                    existingItem.TotalAmount = total;
                    existingItem.Cancelled = false;
                    
                }
                else
                {
                    if (updated.quantity > 20)
                        throw new InvalidOperationException("Cannot sell more than 20 identical items");

                    decimal discount = DiscountCalculator.Calculate(updated.quantity, updated.unitPrice);
                    decimal total = (updated.quantity * updated.unitPrice) - discount;

                    var newItem = new SaleItem(updated.productId, updated.productName, updated.quantity, updated.unitPrice, discount, total)
                    {
                        Cancelled = false,
                    };
                    _items.Add(newItem);
                }
            }

            foreach (var item in _items)
            {
                Console.WriteLine($"Item {item.ProductId}, xmin: {item.xmin}");
            }

            // Recalcula total da venda
            RecalculateTotal();
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

        private void RecalculateTotal()
        {
            TotalAmount = _items.Where(i => !i.Cancelled).Sum(i => i.TotalAmount);
        }

    }
}