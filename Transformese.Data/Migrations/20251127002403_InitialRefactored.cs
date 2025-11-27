using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Transformese.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialRefactored : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TipoUsuarios",
                columns: table => new
                {
                    IdTipoUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DescricaoTipoUsuario = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoUsuarios", x => x.IdTipoUsuario);
                });

            migrationBuilder.CreateTable(
                name: "Unidades",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Bairro = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Responsavel = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Senha = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    FotoPerfil = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TipoUsuarioId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.IdUsuario);
                    table.ForeignKey(
                        name: "FK_Usuarios_TipoUsuarios_TipoUsuarioId",
                        column: x => x.TipoUsuarioId,
                        principalTable: "TipoUsuarios",
                        principalColumn: "IdTipoUsuario",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Candidatos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NomeCompleto = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CPF = table.Column<string>(type: "nvarchar(14)", maxLength: 14, nullable: false),
                    DataNascimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Cidade = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Estado = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PossuiComputador = table.Column<bool>(type: "bit", nullable: false),
                    PossuiInternet = table.Column<bool>(type: "bit", nullable: false),
                    PerfilLinkedin = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataCadastro = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataEntrevista = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ObservacoesONG = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ObservacoesGF = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnidadeId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidatos_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidades",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidatoLogs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CandidatoId = table.Column<int>(type: "int", nullable: false),
                    UsuarioId = table.Column<int>(type: "int", nullable: true),
                    StatusAnterior = table.Column<int>(type: "int", nullable: false),
                    NovoStatus = table.Column<int>(type: "int", nullable: false),
                    Acao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Observacao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DataHora = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidatoLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CandidatoLogs_Candidatos_CandidatoId",
                        column: x => x.CandidatoId,
                        principalTable: "Candidatos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidatoLogs_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "IdUsuario");
                });

            migrationBuilder.InsertData(
                table: "TipoUsuarios",
                columns: new[] { "IdTipoUsuario", "DescricaoTipoUsuario" },
                values: new object[,]
                {
                    { 1, "Administrador" },
                    { 2, "Candidato" }
                });

            migrationBuilder.InsertData(
                table: "Unidades",
                columns: new[] { "Id", "Bairro", "Cidade", "Endereco", "Estado", "Nome", "Responsavel", "UnidadeId" },
                values: new object[,]
                {
                    { -3, "Tatuapé", "São Paulo", "Rua C, 300", "SP", "Unidade Tatuapé", "Fernanda Lima", -3 },
                    { -2, "Itaquera", "São Paulo", "Av B, 200", "SP", "Unidade Itaquera", "João Santos", -2 },
                    { -1, "São Miguel", "São Paulo", "Rua A, 100", "SP", "Unidade São Miguel", "Maria da Silva", -1 }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "IdUsuario", "DataNascimento", "Email", "FotoPerfil", "Nome", "Senha", "TipoUsuarioId" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@sistema.com", "default-user.jpg", "Admin do Sistema", "123456", 1 },
                    { 6, new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidato1@sistema.com", "default-user.jpg", "Candidato 1", "123456", 2 },
                    { 7, new DateTime(2000, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidato2@sistema.com", "default-user.jpg", "Candidato 2", "123456", 2 },
                    { 8, new DateTime(2000, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidato3@sistema.com", "default-user.jpg", "Candidato 3", "123456", 2 },
                    { 9, new DateTime(2000, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidato4@sistema.com", "default-user.jpg", "Candidato 4", "123456", 2 },
                    { 10, new DateTime(2000, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidato5@sistema.com", "default-user.jpg", "Candidato 5", "123456", 2 },
                    { 11, new DateTime(2000, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidato6@sistema.com", "default-user.jpg", "Candidato 6", "123456", 2 },
                    { 12, new DateTime(2000, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidato7@sistema.com", "default-user.jpg", "Candidato 7", "123456", 2 },
                    { 13, new DateTime(2000, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidato8@sistema.com", "default-user.jpg", "Candidato 8", "123456", 2 },
                    { 14, new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidato9@sistema.com", "default-user.jpg", "Candidato 9", "123456", 2 },
                    { 15, new DateTime(2000, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidato10@sistema.com", "default-user.jpg", "Candidato 10", "123456", 2 },
                    { 16, new DateTime(2000, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidato11@sistema.com", "default-user.jpg", "Candidato 11", "123456", 2 },
                    { 17, new DateTime(2000, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidato12@sistema.com", "default-user.jpg", "Candidato 12", "123456", 2 },
                    { 18, new DateTime(2000, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidato13@sistema.com", "default-user.jpg", "Candidato 13", "123456", 2 },
                    { 19, new DateTime(2000, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidato14@sistema.com", "default-user.jpg", "Candidato 14", "123456", 2 },
                    { 20, new DateTime(2000, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "candidato15@sistema.com", "default-user.jpg", "Candidato 15", "123456", 2 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_CandidatoLogs_CandidatoId",
                table: "CandidatoLogs",
                column: "CandidatoId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidatoLogs_UsuarioId",
                table: "CandidatoLogs",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_CPF",
                table: "Candidatos",
                column: "CPF",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_UnidadeId",
                table: "Candidatos",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuarios_TipoUsuarioId",
                table: "Usuarios",
                column: "TipoUsuarioId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CandidatoLogs");

            migrationBuilder.DropTable(
                name: "Candidatos");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Unidades");

            migrationBuilder.DropTable(
                name: "TipoUsuarios");
        }
    }
}
