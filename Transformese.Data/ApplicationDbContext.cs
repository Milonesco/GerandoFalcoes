using System;
using Microsoft.EntityFrameworkCore;
using Transformese.Domain.Entities;
using Transformese.Domain.Enums;

namespace Transformese.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Candidato> Candidatos { get; set; }

        public DbSet<Funcionario> Funcionarios { get; set; }
        public DbSet<UnidadeParceira> UnidadesParceiras { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuração Candidato
            modelBuilder.Entity<Candidato>()
                .HasIndex(c => c.CPF)
                .IsUnique();

            modelBuilder.Entity<Candidato>()
                .Property(c => c.CPF)
                .IsRequired()
                .HasMaxLength(14);

            modelBuilder.Entity<Candidato>()
                .Property(c => c.RendaFamiliar)
                .HasColumnType("decimal(18,2)");

            modelBuilder.Entity<Funcionario>().HasData(new Funcionario
            {
                Id = 1,
                Nome = "Admin",
                Sobrenome = "Geral",
                Email = "admin@admingerando.falcoes",
                Senha = BCrypt.Net.BCrypt.HashPassword("admingf123"),
                EhAdministrador = true,
                Ativo = true,
                DataCadastro = DateTime.Now,
                Sexo = "Outro"
            });
        }
    }
}