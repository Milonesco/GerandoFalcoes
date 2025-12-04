using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transformese.Data;
using Transformese.Domain.Entities;
using Transformese.Api.DTOs;

namespace Transformese.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionariosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FuncionariosController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpPost]
        public async Task<ActionResult<Funcionario>> PostFuncionario(Funcionario funcionario)
        {
            funcionario.Senha = BCrypt.Net.BCrypt.HashPassword(funcionario.Senha);
            _context.Funcionarios.Add(funcionario);
            await _context.SaveChangesAsync();
            return CreatedAtAction(nameof(GetFuncionario), new { id = funcionario.Id }, funcionario);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> GetFuncionario(int id)
        {
            var funcionario = await _context.Funcionarios.FindAsync(id);

            if (funcionario == null)
            {
                return NotFound();
            }

            return funcionario;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            // 1. Busca APENAS pelo e-mail
            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.Email == request.Email);

            // Se não achou o email, tchau
            if (funcionario == null)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            // 2. Verifica a senha usando o BCrypt
            // O método Verify pega a senha "123" (request), faz o hash dela na hora 
            // e vê se bate com o hash salvo no banco (funcionario.Senha).
            bool senhaValida = BCrypt.Net.BCrypt.Verify(request.Senha, funcionario.Senha);

            if (!senhaValida)
            {
                return Unauthorized("Usuário ou senha inválidos.");
            }

            return Ok(funcionario);
        }

        [HttpPut("reset-senha")]
        public async Task<IActionResult> ResetSenha([FromBody] ResetSenhaRequest request)
        {
            // 1. Busca o funcionário pelo e-mail
            var funcionario = await _context.Funcionarios
                .FirstOrDefaultAsync(f => f.Email == request.Email);

            if (funcionario == null)
            {
                return NotFound("E-mail não encontrado na base de dados.");
            }

            // 2. Criptografa a nova senha antes de salvar
            funcionario.Senha = BCrypt.Net.BCrypt.HashPassword(request.NovaSenha);

            // 3. Salva as alterações no banco
            _context.Entry(funcionario).State = EntityState.Modified;
            await _context.SaveChangesAsync();

            return Ok("Senha atualizada com sucesso.");
        }
    }
}
