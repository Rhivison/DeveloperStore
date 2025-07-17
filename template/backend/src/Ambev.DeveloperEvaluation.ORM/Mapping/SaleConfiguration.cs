using Ambev.DeveloperEvaluation.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ambev.DeveloperEvaluation.ORM.Mapping
{
    public class SaleConfiguration: IEntityTypeConfiguration<Sale>
    {
        public void Configure(EntityTypeBuilder<Sale> builder)
        {
            builder.ToTable("Sales");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.SaleNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.SaleDate)
                .IsRequired();

            builder.Property(s => s.Customer)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(s => s.Branch)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Cancelled)
                .IsRequired();

            // Configura o relacionamento 1:N entre Sale e SaleItem
            builder.HasMany(s => s.Items)
                .WithOne()
                .HasForeignKey("SaleId")  // FK na tabela SaleItems
                .IsRequired()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
    public class SaleItemConfiguration : IEntityTypeConfiguration<SaleItem>
    {
        public void Configure(EntityTypeBuilder<SaleItem> builder)
        {
            builder.ToTable("SaleItems");

            builder.HasKey(i => i.Id);

            builder.Property(i => i.ProductId)
                .IsRequired();

            builder.Property(i => i.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(i => i.Quantity)
                .IsRequired();

            builder.Property(i => i.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.Discount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.TotalAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(i => i.Cancelled)
                .IsRequired();
        }
    }
}