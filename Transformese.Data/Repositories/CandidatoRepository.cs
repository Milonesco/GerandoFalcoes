using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Transformese.Domain.Entities;
using Transformese.Data; // Certifique-se que o namespace do DbContext está aqui

namespace Transformese.Data.Repositories
{
    public class CandidatoRepository
    {
        private readonly ApplicationDbContext _context;

        public CandidatoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // 1. CREATE (Já existia)
        public async Task Adicionar(Candidato candidato)
        {
            _context.Candidatos.Add(candidato);
            await _context.SaveChangesAsync();
        }

        // 2. READ - LISTAR TODOS (Necessário para a tela de Entrevistas)
        public async Task<List<Candidato>> ObterTodos()
        {
            return await _context.Candidatos.ToListAsync();
        }

        // 3. READ - OBTER POR ID (Necessário para carregar detalhes)
        public async Task<Candidato> ObterPorId(int id)
        {
            return await _context.Candidatos.FindAsync(id);
        }

        // 4. UPDATE - ATUALIZAR (Crucial para a Triagem!)
        public async Task Atualizar(Candidato candidato)
        {
            _context.Entry(candidato).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // 5. VALIDATION (Já existia)
        public async Task<bool> ExisteCpf(string cpf)
        {
            return await _context.Candidatos.AnyAsync(c => c.CPF == cpf);
        }
    }
}