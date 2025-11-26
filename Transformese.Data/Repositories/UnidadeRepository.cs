using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Transformese.Data;
using Transformese.Domain.Entities;

namespace Transformese.Data.Repositories
{
    public class UnidadeRepository
    {
        private readonly ApplicationDbContext _context;

        public UnidadeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // CREATE
        public void Create(Unidade unidade)
        {
            _context.Unidades.Add(unidade);
            _context.SaveChanges();
        }

        // READ (listar todas)
        public List<Unidade> Read()
        {
            return _context.Unidades
                .AsNoTracking()
                .ToList();
        }

        // READ (pesquisar por ID)
        public Unidade? Pesquisar(int id)
        {
            return _context.Unidades
                .AsNoTracking()
                .FirstOrDefault(u => u.UnidadeId == id);
        }

        // UPDATE
        public void Update(Unidade unidade)
        {
            var existe = _context.Unidades.Find(unidade.UnidadeId);
            if (existe == null)
                throw new Exception("Unidade não encontrada.");

            existe.Nome = unidade.Nome;
            existe.Endereco = unidade.Endereco;

            _context.SaveChanges();
        }

        // DELETE
        public void Delete(int id)
        {
            var unidade = _context.Unidades.Find(id);
            if (unidade == null)
                throw new Exception("Unidade não encontrada.");

            _context.Unidades.Remove(unidade);
            _context.SaveChanges();
        }

        // CONTAR
        public int ContarUnidades()
        {
            return _context.Unidades.Count();
        }
    }
}
