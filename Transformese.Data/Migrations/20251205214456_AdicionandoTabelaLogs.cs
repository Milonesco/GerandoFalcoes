using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transformese.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoTabelaLogs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LogsSistema",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Usuario = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Acao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Detalhes = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LogsSistema", x => x.Id);
                });

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 5, 18, 44, 55, 792, DateTimeKind.Local).AddTicks(2048), "$2a$11$zdarzW9NNAKMM/M4d8zYSecdQR2U48ElffJWS6rLvsf6L4npJMqAW" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LogsSistema");

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 5, 17, 13, 35, 955, DateTimeKind.Local).AddTicks(5277), "$2a$11$CCx9vV92mcmHrkU7JvspIOCQV8Uh51BP77Jqlq2e9r/9ZW7MBzuIu" });
        }
    }
}
