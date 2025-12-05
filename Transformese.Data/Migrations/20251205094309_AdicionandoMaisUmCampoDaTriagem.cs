using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transformese.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoMaisUmCampoDaTriagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ObservacoesONG",
                table: "Candidatos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Pontuacao",
                table: "Candidatos",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 5, 6, 43, 9, 129, DateTimeKind.Local).AddTicks(9566), "$2a$11$4LHWGMUAqH1mFegwo7pXm.Gal0yVc3woBL1SQePF.yEZdsoUc9/j2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObservacoesONG",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "Pontuacao",
                table: "Candidatos");

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 5, 6, 34, 28, 280, DateTimeKind.Local).AddTicks(9468), "$2a$11$THyQeeHXVbg.c39HbDfUtuRJmE57iPRbO9GjKtngc9a80mpX1EmQa" });
        }
    }
}
