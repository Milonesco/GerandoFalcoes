using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transformese.Data.Migrations
{
    /// <inheritdoc />
    public partial class finalRefact : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataEntrevista",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "ObservacoesGF",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "UnidadeId",
                table: "Candidatos");

            migrationBuilder.RenameColumn(
                name: "ObservacoesONG",
                table: "Candidatos",
                newName: "NomeOngResponsavel");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInscricao",
                table: "Candidatos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataInscricao",
                table: "Candidatos");

            migrationBuilder.RenameColumn(
                name: "NomeOngResponsavel",
                table: "Candidatos",
                newName: "ObservacoesONG");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataEntrevista",
                table: "Candidatos",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObservacoesGF",
                table: "Candidatos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UnidadeId",
                table: "Candidatos",
                type: "int",
                nullable: true);
        }
    }
}
