using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Transformese.Data;
using Transformese.Domain.Entities;

namespace Transformese.Data.Repositories
{
    public class CursoRepository
    {
        private readonly ApplicationDbContext _context;

        public CursoRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Create(Curso curso)
        {
            _context.Cursos.Add(curso);
            _context.SaveChanges();
        }

        public List<Curso> Read()
        {
            return _context.Cursos
                .Include(c => c.Unidade)
                .ToList();
        }

        public Curso? Pesquisar(int id)
        {
            return _context.Cursos
                .Include(c => c.Unidade)
                .FirstOrDefault(c => c.IdCurso == id);
        }

        public void Update(Curso curso)
        {
            _context.Cursos.Update(curso);
            _context.SaveChanges();
        }

        public void Delete(int id)
        {
            var curso = _context.Cursos.Find(id);
            if (curso != null)
            {
                _context.Cursos.Remove(curso);
                _context.SaveChanges();
            }
        }
    }
}
