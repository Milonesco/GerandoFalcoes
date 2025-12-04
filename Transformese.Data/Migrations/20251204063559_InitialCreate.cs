using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Transformese.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Candidatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NomeSocial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    RaçaEtnia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IdentidadeGenero = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PessoaTransgenero = table.Column<bool>(type: "bit", nullable: false),
                    OrientacaoSexual = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CEP = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Escolaridade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TrabalhaAtualmente = table.Column<bool>(type: "bit", nullable: false),
                    RendaFamiliar = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PessoasNoDomicilio = table.Column<int>(type: "int", nullable: false),
                    PossuiDeficiencia = table.Column<bool>(type: "bit", nullable: false),
                    DescricaoDeficiencia = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CursoDesejado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TurnoPreferencial = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    JaEstudouAntes = table.Column<bool>(type: "bit", nullable: false),
                    IndicadoPor = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConcordaPrograma = table.Column<bool>(type: "bit", nullable: false),
                    AceitaNovidades = table.Column<bool>(type: "bit", nullable: false),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    PossuiComputador = table.Column<bool>(type: "bit", nullable: false),
                    PossuiInternet = table.Column<bool>(type: "bit", nullable: false),
                    PerfilLinkedin = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatos", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_CPF",
                table: "Candidatos",
                column: "CPF",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Candidatos");
        }
    }
}
