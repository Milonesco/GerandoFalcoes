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
        }
    }
}