using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Transformese.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
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
                    IdUnidade = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unidades", x => x.IdUnidade);
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
                name: "Cursos",
                columns: table => new
                {
                    IdCurso = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Descricao = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Imagem = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    UnidadeId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cursos", x => x.IdCurso);
                    table.ForeignKey(
                        name: "FK_Cursos_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidades",
                        principalColumn: "IdUnidade",
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
                    CursoId = table.Column<int>(type: "int", nullable: true),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidatos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Candidatos_Cursos_CursoId",
                        column: x => x.CursoId,
                        principalTable: "Cursos",
                        principalColumn: "IdCurso");
                    table.ForeignKey(
                        name: "FK_Candidatos_Unidades_UnidadeId",
                        column: x => x.UnidadeId,
                        principalTable: "Unidades",
                        principalColumn: "IdUnidade");
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
                    { 2, "Professor" },
                    { 3, "Aluno" }
                });

            migrationBuilder.InsertData(
                table: "Unidades",
                columns: new[] { "IdUnidade", "Endereco", "Nome" },
                values: new object[,]
                {
                    { 1, "Rua A, 100", "Unidade São Miguel" },
                    { 2, "Av B, 200", "Unidade Itaquera" },
                    { 3, "Rua C, 300", "Unidade Tatuapé" },
                    { 4, "Rua D, 400", "Unidade Penha" },
                    { 5, "Av E, 500", "Unidade São Mateus" }
                });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "IdCurso", "Descricao", "Imagem", "Nome", "UnidadeId" },
                values: new object[,]
                {
                    { 1, "Conceitos iniciais de computação", "default-course.jpg", "Informática Básica", 1 },
                    { 2, "Lógica e orientação a objetos", "default-course.jpg", "Programação C#", 1 },
                    { 3, "Ferramentas visuais e criatividade", "default-course.jpg", "Design Gráfico", 2 },
                    { 4, "Infraestrutura e comunicação", "default-course.jpg", "Redes de Computadores", 2 },
                    { 5, "Automação e análise de dados", "default-course.jpg", "Excel Avançado", 3 },
                    { 6, "HTML, CSS e JavaScript", "default-course.jpg", "Desenvolvimento Web", 3 },
                    { 7, "Modelagem e SQL", "default-course.jpg", "Banco de Dados", 4 },
                    { 8, "Edição e manipulação de imagens", "default-course.jpg", "Photoshop", 4 },
                    { 9, "Estratégias e campanhas online", "default-course.jpg", "Marketing Digital", 5 },
                    { 10, "Visualização e dashboard", "default-course.jpg", "Power BI", 5 }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "IdUsuario", "DataNascimento", "Email", "FotoPerfil", "Nome", "Senha", "TipoUsuarioId" },
                values: new object[,]
                {
                    { 1, new DateTime(1990, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "admin@sistema.com", "default-user.jpg", "Admin do Sistema", "123456", 1 },
                    { 2, new DateTime(1985, 5, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "carlos.prof@sistema.com", "default-user.jpg", "Carlos Henrique", "123456", 2 },
                    { 3, new DateTime(1987, 8, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "marina.prof@sistema.com", "default-user.jpg", "Marina Lopes", "123456", 2 },
                    { 4, new DateTime(1982, 2, 20, 0, 0, 0, 0, DateTimeKind.Unspecified), "joao.prof@sistema.com", "default-user.jpg", "João Batista", "123456", 2 },
                    { 5, new DateTime(1990, 9, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "patricia.prof@sistema.com", "default-user.jpg", "Patrícia Santos", "123456", 2 },
                    { 6, new DateTime(2000, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), "aluno1@sistema.com", "default-user.jpg", "Aluno 1", "123456", 3 },
                    { 7, new DateTime(2000, 1, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), "aluno2@sistema.com", "default-user.jpg", "Aluno 2", "123456", 3 },
                    { 8, new DateTime(2000, 1, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "aluno3@sistema.com", "default-user.jpg", "Aluno 3", "123456", 3 },
                    { 9, new DateTime(2000, 1, 5, 0, 0, 0, 0, DateTimeKind.Unspecified), "aluno4@sistema.com", "default-user.jpg", "Aluno 4", "123456", 3 },
                    { 10, new DateTime(2000, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "aluno5@sistema.com", "default-user.jpg", "Aluno 5", "123456", 3 },
                    { 11, new DateTime(2000, 1, 7, 0, 0, 0, 0, DateTimeKind.Unspecified), "aluno6@sistema.com", "default-user.jpg", "Aluno 6", "123456", 3 },
                    { 12, new DateTime(2000, 1, 8, 0, 0, 0, 0, DateTimeKind.Unspecified), "aluno7@sistema.com", "default-user.jpg", "Aluno 7", "123456", 3 },
                    { 13, new DateTime(2000, 1, 9, 0, 0, 0, 0, DateTimeKind.Unspecified), "aluno8@sistema.com", "default-user.jpg", "Aluno 8", "123456", 3 },
                    { 14, new DateTime(2000, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "aluno9@sistema.com", "default-user.jpg", "Aluno 9", "123456", 3 },
                    { 15, new DateTime(2000, 1, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "aluno10@sistema.com", "default-user.jpg", "Aluno 10", "123456", 3 },
                    { 16, new DateTime(2000, 1, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "aluno11@sistema.com", "default-user.jpg", "Aluno 11", "123456", 3 },
                    { 17, new DateTime(2000, 1, 13, 0, 0, 0, 0, DateTimeKind.Unspecified), "aluno12@sistema.com", "default-user.jpg", "Aluno 12", "123456", 3 },
                    { 18, new DateTime(2000, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), "aluno13@sistema.com", "default-user.jpg", "Aluno 13", "123456", 3 },
                    { 19, new DateTime(2000, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "aluno14@sistema.com", "default-user.jpg", "Aluno 14", "123456", 3 },
                    { 20, new DateTime(2000, 1, 16, 0, 0, 0, 0, DateTimeKind.Unspecified), "aluno15@sistema.com", "default-user.jpg", "Aluno 15", "123456", 3 }
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
                name: "IX_Candidatos_CursoId",
                table: "Candidatos",
                column: "CursoId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidatos_UnidadeId",
                table: "Candidatos",
                column: "UnidadeId");

            migrationBuilder.CreateIndex(
                name: "IX_Cursos_UnidadeId",
                table: "Cursos",
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
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "TipoUsuarios");

            migrationBuilder.DropTable(
                name: "Unidades");
        }
    }
}
