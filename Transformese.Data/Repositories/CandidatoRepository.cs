using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transformese.Domain.Entities;

namespace Transformese.Data.Repositories
{
    public class CandidatoRepository
    {
        private readonly ApplicationDbContext _context;

        public CandidatoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Create
        public async Task Adicionar(Candidato candidato)
        {
            _context.Candidatos.Add(candidato);
            await _context.SaveChangesAsync();
        }

        // Read (Verificar Duplicidade de CPF)
        public async Task<bool> ExisteCpf(string cpf)
        {
            return await _context.Candidatos.AnyAsync(c => c.CPF == cpf);
        }
    }
}