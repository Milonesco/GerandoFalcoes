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

        // Read (Todos)
        public async Task<List<Candidato>> ObterTodos()
        {
            return await _context.Candidatos
                .Include(c => c.Unidade) // Traz os dados da ONG junto
                .OrderByDescending(c => c.DataCadastro)
                .ToListAsync();
        }

        // Read (Por ID)
        public async Task<Candidato?> ObterPorId(int id)
        {
            return await _context.Candidatos
                .Include(c => c.Unidade)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Candidato?> ObterPorCpf(string cpf)
        {
            return await _context.Candidatos
                .Include(c => c.Unidade)
                .FirstOrDefaultAsync(c => c.CPF == cpf);
        }

        // Read (Por Unidade - Para a ONG ver só os dela)
        public async Task<List<Candidato>> ObterPorUnidade(int unidadeId)
        {
            return await _context.Candidatos
                .Where(c => c.UnidadeId == unidadeId)
                .Include(c => c.Unidade)
                .ToListAsync();
        }

        // Read (Verificar Duplicidade de CPF)
        public async Task<bool> ExisteCpf(string cpf)
        {
            return await _context.Candidatos.AnyAsync(c => c.CPF == cpf);
        }

        // Update
        public async Task Atualizar(Candidato candidato)
        {
            _context.Candidatos.Update(candidato);
            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task Excluir(int id)
        {
            var candidato = await ObterPorId(id);
            if (candidato != null)
            {
                _context.Candidatos.Remove(candidato);
                await _context.SaveChangesAsync();
            }
        }
    }
}