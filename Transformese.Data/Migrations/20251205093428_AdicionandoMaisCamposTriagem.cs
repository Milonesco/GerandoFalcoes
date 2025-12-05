using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transformese.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoMaisCamposTriagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 5, 6, 34, 28, 280, DateTimeKind.Local).AddTicks(9468), "$2a$11$THyQeeHXVbg.c39HbDfUtuRJmE57iPRbO9GjKtngc9a80mpX1EmQa" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 5, 6, 27, 56, 251, DateTimeKind.Local).AddTicks(9595), "$2a$11$1IwYUtw7uYApqm8uz8giUersooVJxPCb7F6/GbCgPXMdNAW/nszV2" });
        }
    }
}
