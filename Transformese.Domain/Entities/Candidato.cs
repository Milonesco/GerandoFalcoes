using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Transformese.Domain.Entities
{
    public class Candidato
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome completo é obrigatório.")]
        public string NomeCompleto { get; set; }

        [Required(ErrorMessage = "O CPF é obrigatório.")]
        [StringLength(14)]
        public string Cpf { get; set; }

        [Required(ErrorMessage = "O e-mail é obrigatório.")]
        [EmailAddress(ErrorMessage = "E-mail inválido.")]
        public string Email { get; set; }

        public string Telefone { get; set; }

        public string Cidade { get; set; }

        public bool PossuiComputador { get; set; }

        public bool PossuiInternet { get; set; }

        public string? ObservacoesOng { get; set; }

        // Status do processo seletivo
        public int Status { get; set; } = 0;

        public DateTime DataCadastro { get; set; } = DateTime.Now;

        // Chave Estrangeira para a ONG (Unidade)
        public int? UnidadeId { get; set; }

        [ForeignKey("UnidadeId")]
        public virtual Unidade? Unidade { get; set; }
    }
}