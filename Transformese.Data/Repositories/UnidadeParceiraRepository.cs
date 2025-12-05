using System.Collections.Generic;
using System.Linq;
using Transformese.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Transformese.Data.Repositories
{
    public class UnidadeParceiraRepository
    {
        private readonly ApplicationDbContext _context;

        public UnidadeParceiraRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Adicionar(UnidadeParceira unidade)
        {
            _context.UnidadesParceiras.Add(unidade);
            _context.SaveChanges();
        }

        // Você vai precisar desse método para preencher os Combos na Triagem
        public List<UnidadeParceira> ObterTodas()
        {
            return _context.UnidadesParceiras.AsNoTracking().ToList();
        }
        // Adicione esses dois métodos na classe UnidadeParceiraRepository
        public void Atualizar(UnidadeParceira unidade)
        {
            _context.Entry(unidade).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void Remover(int id)
        {
            var unidade = _context.UnidadesParceiras.Find(id);
            if (unidade != null)
            {
                _context.UnidadesParceiras.Remove(unidade);
                _context.SaveChanges();
            }
        }
    }
}