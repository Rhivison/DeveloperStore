using System;
using System.Collections.Generic;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.GetSales;
using Ambev.DeveloperEvaluation.Application.DTOs;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales.TestData
{
    public static class GetSalesTestData
    {
        public static List<Sale> CreateSales(int count = 5)
        {
            var sales = new List<Sale>();
            for (int i = 1; i <= count; i++)
            {
                sales.Add(new Sale
                {
                    Id = new Guid(),
                    UserId = Guid.NewGuid(),
                    SaleDate = DateTime.UtcNow.AddDays(-i)
                });
            }
            return sales;
        }

        public static GetSalesCommand CreateValidCommand(
            int page = 1,
            int size = 10,
            string orderBy = null)
        {
            return new GetSalesCommand
            {
                Page = page,
                Size = size,
                OrderBy = orderBy
            };
        }

        public static List<GetSaleDto> CreateSaleDtosFromSales(List<Sale> sales)
        {
            var dtos = new List<GetSaleDto>();
            foreach (var s in sales)
            {
                dtos.Add(new GetSaleDto
                {
                    Id = s.Id,
                    UserId = s.UserId,
                    Date = s.SaleDate
                });
            }
            return dtos;
        }
    }
}
