using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using Transformese.Domain.Entities;

namespace Transformese.Data
{
    public static class SeedData
    {
        public static void Initialize(ApplicationDbContext context)
        {
            context.Database.Migrate();

            if (context.Usuarios.Any()) return;

            Console.WriteLine("🌱 Semeando Banco de Dados...");

            // 1. Tipos de Usuário 
            var tipoAdmin = context.TipoUsuarios.FirstOrDefault(t => t.DescricaoTipoUsuario == "Administrador")
                            ?? new TipoUsuario { IdTipoUsuario = 1, DescricaoTipoUsuario = "Administrador" };

            var tipoOng = context.TipoUsuarios.FirstOrDefault(t => t.DescricaoTipoUsuario == "Professor")
                          ?? new TipoUsuario { IdTipoUsuario = 2, DescricaoTipoUsuario = "Professor" }; 

            if (!context.TipoUsuarios.Any(t => t.IdTipoUsuario == tipoAdmin.IdTipoUsuario))
                context.TipoUsuarios.Add(tipoAdmin);

            if (!context.TipoUsuarios.Any(t => t.IdTipoUsuario == tipoOng.IdTipoUsuario))
                context.TipoUsuarios.Add(tipoOng);

            context.SaveChanges();

            // 2. Unidade
            var unidade = new Unidade
            {
                Nome = "Gerando Falcões - Poá",
                Endereco = "Rua Exemplo, 123",
                Bairro = "Centro",
                Cidade = "Poá",
                Estado = "SP",
                Ativo = true,
                UnidadeId = 0 
            };
            context.Unidades.Add(unidade);
            context.SaveChanges();

            // 3. Usuários
            var admin = new Usuario
            {
                Nome = "Admin Mestre",
                Email = "admin@gerandofalcoes.com",
                Senha = "123",
                TipoUsuarioId = tipoAdmin.IdTipoUsuario, 
                UnidadeId = null
            };

            var parceiro = new Usuario
            {
                Nome = "Líder Comunitário",
                Email = "ong@gerandofalcoes.com",
                Senha = "123",
                TipoUsuarioId = tipoOng.IdTipoUsuario,
                UnidadeId = unidade.Id
            };

            context.Usuarios.AddRange(admin, parceiro);
            context.SaveChanges();

            Console.WriteLine("✅ Seed concluído!");
        }
    }
}