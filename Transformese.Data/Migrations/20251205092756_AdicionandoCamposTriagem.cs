using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transformese.Data.Migrations
{
    /// <inheritdoc />
    public partial class AdicionandoCamposTriagem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "VagaEncaminhada",
                table: "Candidatos",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 5, 6, 27, 56, 251, DateTimeKind.Local).AddTicks(9595), "$2a$11$1IwYUtw7uYApqm8uz8giUersooVJxPCb7F6/GbCgPXMdNAW/nszV2" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VagaEncaminhada",
                table: "Candidatos");

            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 4, 21, 22, 52, 728, DateTimeKind.Local).AddTicks(1273), "$2a$11$ihQPNNesNKPGLz2LNeuk/.ey.5Vje8ws0xwNewqyJMtoC8/AxrQQK" });
        }
    }
}
