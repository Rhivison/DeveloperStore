using System;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData
{
    public static class UpdateSaleTestData
    {
        public static UpdateSaleCommand CreateValidCommand(Guid? saleId = null, uint xmin = 1)
        {
            return new UpdateSaleCommand
            {
                Id = saleId ?? Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                xmin = xmin,
                Products = new List<UpdateSaleItemDto>
                {
                    new UpdateSaleItemDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 3,
                        xmin = 1
                    },
                    new UpdateSaleItemDto
                    {
                        ProductId = Guid.NewGuid(),
                        Quantity = 5,
                        xmin = 1
                    }
                }
            };
        }

        public static Sale CreateSale(Guid? saleId = null, uint xmin = 1)
        {
            var sale = new Sale
            {
                Id = saleId ?? Guid.NewGuid(),
                UserId = Guid.NewGuid(),
                SaleDate = DateTime.UtcNow.AddDays(-1),
                Customer = "Test Customer",
                Branch = "Test Branch",
                xmin = xmin
            };

            return sale;
        }

        public static Product CreateProduct(Guid? productId = null)
        {
            return new Product
            {
                Id = productId ?? Guid.NewGuid(),
                Title = "Test Product",
                Price = 15.5m
            };
        }
    }
}
