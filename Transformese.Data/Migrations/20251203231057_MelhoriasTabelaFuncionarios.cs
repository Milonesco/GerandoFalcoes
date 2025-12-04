using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transformese.Data.Migrations
{
    /// <inheritdoc />
    public partial class MelhoriasTabelaFuncionarios : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Funcionarios");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataNascimento",
                table: "Funcionarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Sexo",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sobrenome",
                table: "Funcionarios",
                type: "nvarchar(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataNascimento",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "Sexo",
                table: "Funcionarios");

            migrationBuilder.DropColumn(
                name: "Sobrenome",
                table: "Funcionarios");

            migrationBuilder.AddColumn<int>(
                name: "Tipo",
                table: "Funcionarios",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
