using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transformese.Data.Migrations
{
    /// <inheritdoc />
    public partial class AtualizacaoTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 4, 21, 22, 52, 728, DateTimeKind.Local).AddTicks(1273), "$2a$11$ihQPNNesNKPGLz2LNeuk/.ey.5Vje8ws0xwNewqyJMtoC8/AxrQQK" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Funcionarios",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DataCadastro", "Senha" },
                values: new object[] { new DateTime(2025, 12, 4, 14, 37, 3, 605, DateTimeKind.Local).AddTicks(6156), "$2a$11$.KPCwRv/jwVBmMQRjsbv3uLNblpsiy48Ag8Chls3nPM5RGf/f8Hwq" });
        }
    }
}
