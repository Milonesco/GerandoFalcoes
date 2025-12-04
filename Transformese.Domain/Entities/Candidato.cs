using System;
using Transformese.Domain.Enums;

namespace Transformese.Domain.Entities
{
    public class Candidato
    {
        public int Id { get; set; }

        // Dados Pessoais
        public string NomeCompleto { get; set; } = string.Empty;
        public string NomeSocial { get; set; } = string.Empty; // Novo
        public DateTime DataNascimento { get; set; }
        public string CPF { get; set; } = string.Empty;
        public string RaçaEtnia { get; set; } = string.Empty; // Novo
        public string IdentidadeGenero { get; set; } = string.Empty; // Novo
        public bool PessoaTransgenero { get; set; } // Novo
        public string OrientacaoSexual { get; set; } = string.Empty; // Novo

        // Contato e Endereço
        public string Email { get; set; } = string.Empty;
        public string Telefone { get; set; } = string.Empty; // Celular
        public string CEP { get; set; } = string.Empty; // Novo
        public string Estado { get; set; } = string.Empty; // UF
        public string Cidade { get; set; } = string.Empty;

        // Dados Socioeconômicos
        public string Escolaridade { get; set; } = string.Empty; // Novo (ex: Médio Completo)
        public bool TrabalhaAtualmente { get; set; } // Novo
        public string RendaFamiliar { get; set; } = string.Empty; // Novo
        public int PessoasNoDomicilio { get; set; } // Novo
        public bool PossuiDeficiencia { get; set; } // Novo
        public string? DescricaoDeficiencia { get; set; } // Novo (Opcional)

        // Dados do Curso/Programa
        public string CursoDesejado { get; set; } = string.Empty; // Novo
        public string TurnoPreferencial { get; set; } = string.Empty; // Novo
        public bool JaEstudouAntes { get; set; } // Novo
        public string? IndicadoPor { get; set; } // Novo

        // Termos
        public bool ConcordaPrograma { get; set; } // Novo
        public bool AceitaNovidades { get; set; } // Novo

        // Propriedades de Controle (existentes)
        public DateTime DataCadastro { get; set; } = DateTime.Now;
        public StatusCandidato Status { get; set; } = StatusCandidato.Inscrito;

        // Mantive os antigos para compatibilidade, se quiser remover depois, pode.
        public bool PossuiComputador { get; set; }
        public bool PossuiInternet { get; set; }
        public string? PerfilLinkedin { get; set; }
    }
}