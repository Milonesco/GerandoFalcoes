using Transformese.Domain.Enums;
using System;

namespace Transformese.Domain.Entities
{
    public class Funcionario
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sobrenome { get; set; }
        public string Email { get; set; }
        public string Senha { get; set; }
        public string Sexo { get; set; }
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public bool Ativo { get; set; } = true;
        public DateTime DataNascimento { get; set; }
        public bool EhAdministrador { get; set; }
    }
}