using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transformese.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoDataEntrevista : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataEntrevista",
                table: "Candidatos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 13, 35, 955, DateTimeKind.Local).AddTicks(5277), "$2a$11$CCx9vV92mcmHrkU7JvspIOCQV8Uh51BP77Jqlq2e9r/9ZW7MBzuIu" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataEntrevista",
                table: "Candidatos");

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 5, 15, 27, 22, 796, DateTimeKind.Local).AddTicks(5770), "$2a$11$XvyK0jRTWnXLsmw43to1G.7rL6Y0QMAT6korVqdtKxnbNcUfYIpju" });
        }
    }
}
