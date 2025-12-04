using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transformese.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicaoDoAdministrador : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "EhAdministrador",
                table: "Funcionarios",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.InsertData(
                table: "Funcionarios",
                columns: new[] { "Id", "Ativo", "DataCadastro", "DataNascimento", "EhAdministrador", "Email", "Nome", "Senha", "Sexo", "Sobrenome" },
                values: new object[] { 1, true, new DateTime(2025, 12, 3, 22, 6, 41, 172, DateTimeKind.Local).AddTicks(1221), new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), true, "admin@admingerando.falcoes", "Admin", "$2a$11$cJF494NlbMcjL0r4bhKsuuO3rCu65V4YIC1iv0IDUzONu3lCNCxdu", "Outro", "Geral" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "EhAdministrador",
                table: "Funcionarios");
        }
    }
}
