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
                new TipoUsuario { IdTipoUsuario = 2, DescricaoTipoUsuario = "Professor" },
                new TipoUsuario { IdTipoUsuario = 3, DescricaoTipoUsuario = "Aluno" }
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

            modelBuilder.Entity<Curso>().HasData(
                new Curso { IdCurso = 1, Nome = "Informática Básica", Descricao = "Conceitos iniciais de computação", UnidadeId = 1, Imagem = "default-course.jpg" },
                new Curso { IdCurso = 2, Nome = "Programação C#", Descricao = "Lógica e orientação a objetos", UnidadeId = 1, Imagem = "default-course.jpg" },
                new Curso { IdCurso = 3, Nome = "Design Gráfico", Descricao = "Ferramentas visuais e criatividade", UnidadeId = 2, Imagem = "default-course.jpg" },
                new Curso { IdCurso = 4, Nome = "Redes de Computadores", Descricao = "Infraestrutura e comunicação", UnidadeId = 2, Imagem = "default-course.jpg" },
                new Curso { IdCurso = 5, Nome = "Excel Avançado", Descricao = "Automação e análise de dados", UnidadeId = 3, Imagem = "default-course.jpg" },
                new Curso { IdCurso = 6, Nome = "Desenvolvimento Web", Descricao = "HTML, CSS e JavaScript", UnidadeId = 3, Imagem = "default-course.jpg" },
                new Curso { IdCurso = 7, Nome = "Banco de Dados", Descricao = "Modelagem e SQL", UnidadeId = 4, Imagem = "default-course.jpg" },
                new Curso { IdCurso = 8, Nome = "Photoshop", Descricao = "Edição e manipulação de imagens", UnidadeId = 4, Imagem = "default-course.jpg" },
                new Curso { IdCurso = 9, Nome = "Marketing Digital", Descricao = "Estratégias e campanhas online", UnidadeId = 5, Imagem = "default-course.jpg" },
                new Curso { IdCurso = 10, Nome = "Power BI", Descricao = "Visualização e dashboard", UnidadeId = 5, Imagem = "default-course.jpg" }
            );

            var usuarios = new List<Usuario>();
            usuarios.Add(new Usuario { IdUsuario = 1, Nome = "Admin do Sistema", Email = "admin@sistema.com", Senha = "123456", DataNascimento = new DateTime(1990, 1, 1), TipoUsuarioId = 1, FotoPerfil = "default-user.jpg" });

            usuarios.Add(new Usuario { IdUsuario = 2, Nome = "Carlos Henrique", Email = "carlos.prof@sistema.com", Senha = "123456", DataNascimento = new DateTime(1985, 5, 10), TipoUsuarioId = 2, FotoPerfil = "default-user.jpg" });
            usuarios.Add(new Usuario { IdUsuario = 3, Nome = "Marina Lopes", Email = "marina.prof@sistema.com", Senha = "123456", DataNascimento = new DateTime(1987, 8, 3), TipoUsuarioId = 2, FotoPerfil = "default-user.jpg" });
            usuarios.Add(new Usuario { IdUsuario = 4, Nome = "João Batista", Email = "joao.prof@sistema.com", Senha = "123456", DataNascimento = new DateTime(1982, 2, 20), TipoUsuarioId = 2, FotoPerfil = "default-user.jpg" });
            usuarios.Add(new Usuario { IdUsuario = 5, Nome = "Patrícia Santos", Email = "patricia.prof@sistema.com", Senha = "123456", DataNascimento = new DateTime(1990, 9, 15), TipoUsuarioId = 2, FotoPerfil = "default-user.jpg" });

            int id = 6;
            for (int i = 1; i <= 15; i++)
            {
                usuarios.Add(new Usuario
                {
                    IdUsuario = id++,
                    Nome = $"Aluno {i}",
                    Email = $"aluno{i}@sistema.com",
                    Senha = "123456",
                    DataNascimento = new DateTime(2000, 1, 1).AddDays(i),
                    TipoUsuarioId = 3,
                    FotoPerfil = "default-user.jpg"
                });
            }

            modelBuilder.Entity<Usuario>().HasData(usuarios.ToArray());
        }
    }
}
