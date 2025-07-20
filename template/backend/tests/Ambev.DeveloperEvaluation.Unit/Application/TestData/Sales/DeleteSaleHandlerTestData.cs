
using System.Reflection;
using Bogus;
using Ambev.DeveloperEvaluation.Application.Sales.DeleteSale;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData.Sales
{
    /// <summary>
    /// Helper methods to generate data for <see cref="DeleteSaleCommandHandler"/> unit tests.
    /// Uses Bogus to create realistic values.
    /// </summary>
    public static class DeleteSaleHandlerTestData
    {
        private static readonly Faker Faker = new();

        /// <summary>
        /// Generates a valid <see cref="DeleteSaleCommand"/>.
        /// </summary>
        public static DeleteSaleCommand GenerateValidCommand(Guid? id = null, uint? xmin = null)
        {
            return new DeleteSaleCommand
            {
                Id = id ?? Guid.NewGuid(),
                xmin = xmin ?? (uint)Faker.Random.Int(100, 999)
            };
        }

        /// <summary>
        /// Generates a <see cref="Sale"/> that is *not* yet cancelled and contains <paramref name="itemCount"/> active items.
        /// </summary>
        public static Sale GenerateSale(Guid? id = null, uint? xmin = null, int itemCount = 3)
        {
            var sale = new Sale(
                saleNumber: Faker.Commerce.Ean13(), 
                saleDate: DateTime.UtcNow, 
                customer: Faker.Person.FullName, 
                branch: Faker.Company.CompanyName())
            {
                UserId = Guid.NewGuid(),
                Cancelled = false,
                xmin = xmin ?? (uint)Faker.Random.Int(100, 999)
            };

            if (id.HasValue)
                SetEntityId(sale, id.Value);

            for (var i = 0; i < itemCount; i++)
            {
                var qty = Faker.Random.Int(1, 5);
                var price = Faker.Random.Decimal(10, 100);
                sale.AddItem(
                    productId: Guid.NewGuid(),
                    productName: Faker.Commerce.ProductName(),
                    quantity: qty,
                    unitPrice: price
                );
            }

            return sale;
        }

        /// <summary>
        /// Generates a sale already canceled (sale + all items).
        /// </summary>
        public static Sale GenerateCancelledSale(Guid? id = null, uint? xmin = null, int itemCount = 3)
        {
            var sale = GenerateSale(id, xmin, itemCount);
            sale.Cancelled = true;
            foreach (var item in sale.Items)
                item.Cancelled = true;
            return sale;
        }

        /// <summary>
        /// Utility to force-set the Id on an entity that inherits from BaseEntity.
        /// Uses reflection because many DDD-style bases make Id protected.
        /// </summary>
        private static void SetEntityId(object entity, Guid id)
        {
            // tenta propriedade pública/protegida
            var prop = entity.GetType()
                .GetProperty("Id", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
            if (prop is not null && prop.CanWrite)
            {
                prop.SetValue(entity, id);
                return;
            }
            var field = entity.GetType()
                .GetField("<Id>k__BackingField", BindingFlags.NonPublic | BindingFlags.Instance)
                ?? entity.GetType().GetField("Id", BindingFlags.NonPublic | BindingFlags.Instance);

            if (field is not null)
            {
                field.SetValue(entity, id);
                return;
            }

            throw new InvalidOperationException($"Unable to set Id on type {entity.GetType().FullName} for test data.");
        }
    }
}
