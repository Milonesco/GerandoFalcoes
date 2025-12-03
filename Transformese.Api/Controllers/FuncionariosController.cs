using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transformese.Data;
using Transformese.Domain.Entities;
using System.Threading.Tasks;
using Transformese.Api.DTOs;

namespace Transformese.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UsuariosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: api/Usuarios
        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostUsuario(Funcionario funcionario)
        {
            // A API recebe o objeto, adiciona no banco e salva
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFuncionario", new { id = funcionario.Id }, funcionario);
        }

        // GET: api/Usuarios/5 (Apenas para o CreatedAtAction funcionar sem erro)
        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> GetUsuario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);
            if (funcionario == null) return NotFound();
            return funcionario;
        }

        [HttpPost("login")] // A rota final será: api/funcionarios/login
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // Procura um funcionário que tenha O MESMO email E A MESMA senha
            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.Email == request.Email && f.Senha == request.Senha);

            if (funcionario == null)
            {
                return Unauthorized("Usuário ou senha inválidos."); // Retorna erro 401
            }

            return Ok(funcionario); // Retorna o funcionário encontrado (Sucesso 200)
        }
    }
}