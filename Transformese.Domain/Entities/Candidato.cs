using System;
using System.Collections.Generic;
using Transformese.Domain.Enums; 

namespace Transformese.Domain.Entities
{
    public class Candidato : BaseEntity
    {
        public string NomeCompleto { get; set; }
        public string CPF { get; set; }
        public DateTime DataNascimento { get; set; }

        // Contato
        public string Email { get; set; }
        public string Telefone { get; set; }

        // Socioeconômico (Requisitos GF)
        public string Cidade { get; set; }
        public string Estado { get; set; }
        public bool PossuiComputador { get; set; }
        public bool PossuiInternet { get; set; }
        public string? PerfilLinkedin { get; set; }

        // Controle de Processo
        public DateTime DataCadastro { get; set; } = DateTime.Now;

        // Chaves Estrangeiras
        public int? UnidadeId { get; set; }

        public StatusCandidato Status { get; set; } = StatusCandidato.Inscrito;
    }
}