using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transformese.Data.Migrations
{
    /// <inheritdoc />
    public partial class RefatoracaoFinalEstrutura : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 5, 15, 27, 22, 796, DateTimeKind.Local).AddTicks(5770), "$2a$11$XvyK0jRTWnXLsmw43to1G.7rL6Y0QMAT6korVqdtKxnbNcUfYIpju" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 5, 14, 59, 50, 939, DateTimeKind.Local).AddTicks(275), "$2a$11$95VISpjCryBcR.A3zjtfae1nPFEVUxfNbBy9Ky5Zhsycc3F04nkPi" });
        }
    }
}
