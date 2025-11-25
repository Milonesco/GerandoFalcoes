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

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Unidade> Unidades { get; set; }
        public DbSet<Curso> Cursos { get; set; }
        public DbSet<TipoUsuario> TipoUsuarios { get; set; }
        public DbSet<Candidato> Candidatos { get; set; }
        public DbSet<CandidatoLog> CandidatoLogs { get; set; }

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

            // Relacionamento Candidato -> Logs
            modelBuilder.Entity<Candidato>()
                .HasMany(c => c.HistoricoLogs)
                .WithOne(l => l.Candidato)
                .HasForeignKey(l => l.CandidatoId)
                .OnDelete(DeleteBehavior.Cascade);

            SeedData.Seed(modelBuilder);
        }
    }
}