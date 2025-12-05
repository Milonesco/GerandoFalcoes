using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transformese.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoTabelasParaONGS : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UnidadesParceiras",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeUnidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeResponsavel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeVaga = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    QuantidadeVagas = table.Column<int>(type: "int", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnidadesParceiras", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 5, 14, 59, 50, 939, DateTimeKind.Local).AddTicks(275), "$2a$11$95VISpjCryBcR.A3zjtfae1nPFEVUxfNbBy9Ky5Zhsycc3F04nkPi" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UnidadesParceiras");

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 5, 6, 43, 9, 129, DateTimeKind.Local).AddTicks(9566), "$2a$11$4LHWGMUAqH1mFegwo7pXm.Gal0yVc3woBL1SQePF.yEZdsoUc9/j2" });
        }
    }
}
