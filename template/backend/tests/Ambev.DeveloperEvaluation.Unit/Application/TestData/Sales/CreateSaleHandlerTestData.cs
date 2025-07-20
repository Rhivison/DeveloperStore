using System;
using System.Collections.Generic;
using Bogus;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.DTOs;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales
{
    public class CreateSaleHandlerTestData
    {
        public static class DataGenerator
        {
            private static readonly Faker Faker = new();

            public static CreateSaleCommand GenerateValidCommand()
            {
                var productId = Guid.NewGuid();
                return new CreateSaleCommand
                {
                    UserId = Guid.NewGuid(),
                    SaleNumber = Faker.Commerce.Ean13(),
                    SaleDate = DateTime.UtcNow,
                    CustomerName = Faker.Person.FullName,
                    BranchName = Faker.Company.CompanyName(),
                    Items = new List<CreateSaleItemDto>
                    {
                        new CreateSaleItemDto
                        {
                            ProductId = productId,
                            Quantity = Faker.Random.Int(1, 10)
                        }
                    }
                };
            }

            public static Product GenerateProduct(Guid productId)
            {
                return new Product
                {
                    Id = productId,
                    Title = Faker.Commerce.ProductName(),
                    Description = Faker.Lorem.Sentence(),
                    Category = Faker.Commerce.Categories(1)[0],
                    Image = Faker.Image.PicsumUrl(),
                    Price = Faker.Random.Decimal(10, 100),
                    Rating = new ProductRating { 
                        Rate = Faker.Random.Decimal(0, 5),
                        Count = Faker.Random.Int(1, 100)
                    },
                    
                };
            }
        }
    }
}