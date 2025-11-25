using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Transformese.Data;
using Transformese.Domain.Entities;

namespace Transformese.Data.Repositories
{
    public class UsuarioRepository
    {
        private readonly ApplicationDbContext _context;

        public UsuarioRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Add (async)
        public async Task AddAsync(Usuario usuario)
        {
            // Em produção, armazene a senha com hash (ex: BCrypt) antes de salvar.
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
        }

        // Get all
        public async Task<List<Usuario>> GetAllAsync()
        {
            return await _context.Usuarios
                .Include(u => u.TipoUsuario)
                .AsNoTracking()
                .ToListAsync();
        }

        // Get by id
        public async Task<Usuario?> GetByIdAsync(int id)
        {
            return await _context.Usuarios
                .Include(u => u.TipoUsuario)
                .AsNoTracking()
                .FirstOrDefaultAsync(u => u.IdUsuario == id);
        }

        // Update
        public async Task UpdateAsync(Usuario usuario)
        {
            var existente = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.IdUsuario == usuario.IdUsuario);

            if (existente == null)
                throw new Exception("Usuário não encontrado.");

            // Atualize apenas os campos desejados
            existente.Nome = usuario.Nome;
            existente.Email = usuario.Email;
            existente.DataNascimento = usuario.DataNascimento;
            existente.FotoPerfil = usuario.FotoPerfil;
            existente.TipoUsuarioId = usuario.TipoUsuarioId;

            // Só atualize a senha se uma nova senha for fornecida (e com hash).
            if (!string.IsNullOrWhiteSpace(usuario.Senha))
            {
                existente.Senha = usuario.Senha; // substituir por hash em produção
            }

            await _context.SaveChangesAsync();
        }

        // Delete
        public async Task DeleteAsync(int id)
        {
            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.IdUsuario == id);

            if (usuario == null)
                throw new Exception("Usuário não encontrado.");

            _context.Usuarios.Remove(usuario);
            await _context.SaveChangesAsync();
        }

        // Autenticar (por email + senha)
        public async Task<Usuario?> AutenticarAsync(string email, string senha)
        {
            // Em produção compare hashes (ex: BCrypt.Verify)
            return await _context.Usuarios
                .Include(u => u.TipoUsuario)
                .FirstOrDefaultAsync(u => u.Email == email && u.Senha == senha);
        }

        // Retornar tipos de usuário
        public async Task<List<TipoUsuario>> GetTiposAsync()
        {
            return await _context.TipoUsuarios
                .AsNoTracking()
                .ToListAsync();
        }
    }
}
