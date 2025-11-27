using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Transformese.Domain.Entities;

namespace Transformese.Data
{
    public static class SeedData
    {
        public static void Seed(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TipoUsuario>().HasData(
                new TipoUsuario { IdTipoUsuario = 1, DescricaoTipoUsuario = "Administrador" },
                new TipoUsuario { IdTipoUsuario = 2, DescricaoTipoUsuario = "Candidato" }
            );

            modelBuilder.Entity<Unidade>().HasData(
               new Unidade
               {
                   Id = -1, // CORRIGIDO: Usar 'Id' e valor negativo
                   Nome = "Unidade São Miguel",
                   Endereco = "Rua A, 100",
                   Bairro = "São Miguel",
                   Cidade = "São Paulo",
                   Estado = "SP",
                   Responsavel = "Maria da Silva",
                   UnidadeId = -1 // Se UnidadeId for mantido, use o mesmo valor negativo para consistência
               },
               new Unidade
                {
                    Id = -2,
                    Nome = "Unidade Itaquera",
                    Endereco = "Av B, 200",
                    Bairro = "Itaquera",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Responsavel = "João Santos",
                    UnidadeId = -2
                },
                new Unidade
                {
                    Id = -3,
                    Nome = "Unidade Tatuapé",
                    Endereco = "Rua C, 300",
                    Bairro = "Tatuapé",
                    Cidade = "São Paulo",
                    Estado = "SP",
                    Responsavel = "Fernanda Lima",
                    UnidadeId = -3
                }

            );

            var usuarios = new List<Usuario>();
            usuarios.Add(new Usuario { IdUsuario = 1, Nome = "Admin do Sistema", Email = "admin@sistema.com", Senha = "123456", DataNascimento = new DateTime(1990, 1, 1), TipoUsuarioId = 1, FotoPerfil = "default-user.jpg" });
 
            int id = 6;
            for (int i = 1; i <= 15; i++)
            {
                usuarios.Add(new Usuario
                {
                    IdUsuario = id++,
                    Nome = $"Candidato {i}",
                    Email = $"candidato{i}@sistema.com",
                    Senha = "123456",
                    DataNascimento = new DateTime(2000, 1, 1).AddDays(i),
                    TipoUsuarioId = 2,
                    FotoPerfil = "default-user.jpg"
                });
            }

            modelBuilder.Entity<Usuario>().HasData(usuarios.ToArray());
        }
    }
}
