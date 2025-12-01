using System;
using Transformese.Domain.Enums;

namespace Transformese.Domain.Entities
{
    public class Candidato
    {
        public int Id { get; set; }
        public string NomeCompleto { get; set; } = string.Empty;
        public string CPF { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;

            // Propriedades usadas pela API/MVC (Devolvidas!)
        public DateTime DataNascimento { get; set; }
        public bool PossuiComputador { get; set; }
        public bool PossuiInternet { get; set; }
        public string? PerfilLinkedin { get; set; }
        public DateTime DataCadastro { get; set; }
        // Propriedade usada pelo Desktop
        public string? NomeOngResponsavel { get; set; }
        public DateTime DataInscricao { get; set; }
        public StatusCandidato Status { get; set; }
        public Candidato() { }
        // Construtor completo para o Desktop
        public Candidato(string nome, string cidade, StatusCandidato status)
        {
            NomeCompleto = nome;
            Cidade = cidade;
            Status = status;
            DataInscricao = DateTime.Now;
            NomeOngResponsavel = "--";

            // Valores padrão para evitar nulos na API se usar esse construtor


            DataCadastro = DateTime.Now;
            DataNascimento = DateTime.MinValue;


        }
    }

}
