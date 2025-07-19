using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations
{
    /// <inheritdoc />
    public partial class xminSalesFixUint : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<uint>(
                name: "xmin",
                table: "Sales",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldNullable: true);

            migrationBuilder.AlterColumn<uint>(
                name: "xmin",
                table: "SaleItems",
                type: "xid",
                rowVersion: true,
                nullable: false,
                defaultValue: 0u,
                oldClrType: typeof(byte[]),
                oldType: "bytea",
                oldRowVersion: true,
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte[]>(
                name: "xmin",
                table: "Sales",
                type: "bytea",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "xid",
                oldRowVersion: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "xmin",
                table: "SaleItems",
                type: "bytea",
                rowVersion: true,
                nullable: true,
                oldClrType: typeof(uint),
                oldType: "xid",
                oldRowVersion: true);
        }
    }
}
