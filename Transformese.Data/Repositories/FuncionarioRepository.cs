using System.Linq;
using Transformese.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Transformese.Data.Repositories
{
    public class FuncionarioRepository
    {
        private readonly ApplicationDbContext _context;

        public FuncionarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public void Adicionar(Funcionario funcionario)
        {
            _context.Funcionarios.Add(funcionario);
            _context.SaveChanges();
        }

        public Funcionario ObterPorEmail(string email)
        {
            return _context.Funcionarios.AsNoTracking()
                           .FirstOrDefault(f => f.Email == email);
        }

        public Funcionario ValidarLogin(string email, string senha)
        {
            var funcionario = _context.Funcionarios.FirstOrDefault(f => f.Email == email);

            if (funcionario != null && funcionario.Senha == senha)
            {
                return funcionario;
            }
            return null;
        }
    }
}