using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transformese.Data.Migrations
{
    /// <inheritdoc />
    public partial class atualizacaodastabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataInscricao",
                table: "Candidatos");

            migrationBuilder.AddColumn<bool>(
                name: "AceitouNovidades",
                table: "Candidatos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "AceitouTermos",
                table: "Candidatos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "CEP",
                table: "Candidatos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CursoInteresse",
                table: "Candidatos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Deficiencia",
                table: "Candidatos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Escolaridade",
                table: "Candidatos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "IdentidadeGenero",
                table: "Candidatos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "JaEstudouNaGF",
                table: "Candidatos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "NomeIndicacao",
                table: "Candidatos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrientacaoSexual",
                table: "Candidatos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PessoasNaCasa",
                table: "Candidatos",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "RacaCor",
                table: "Candidatos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<decimal>(
                name: "RendaFamiliar",
                table: "Candidatos",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<bool>(
                name: "TrabalhaAtualmente",
                table: "Candidatos",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "TurnoPreferido",
                table: "Candidatos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 37, 3, 605, DateTimeKind.Local).AddTicks(6156), "$2a$11$.KPCwRv/jwVBmMQRjsbv3uLNblpsiy48Ag8Chls3nPM5RGf/f8Hwq" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AceitouNovidades",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "AceitouTermos",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "CEP",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "CursoInteresse",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "Deficiencia",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "Escolaridade",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "IdentidadeGenero",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "JaEstudouNaGF",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "NomeIndicacao",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "OrientacaoSexual",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "PessoasNaCasa",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "RacaCor",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "RendaFamiliar",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "TrabalhaAtualmente",
                table: "Candidatos");

            migrationBuilder.DropColumn(
                name: "TurnoPreferido",
                table: "Candidatos");

            migrationBuilder.AddColumn<DateTime>(
                name: "DataInscricao",
                table: "Candidatos",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 3, 22, 6, 41, 172, DateTimeKind.Local).AddTicks(1221), "$2a$11$cJF494NlbMcjL0r4bhKsuuO3rCu65V4YIC1iv0IDUzONu3lCNCxdu" });
        }
    }
}
