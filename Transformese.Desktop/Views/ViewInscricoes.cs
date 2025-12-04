using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transformese.Domain.Entities;
using System.Net.Http.Json;

namespace Transformese.Desktop.Views
{
    public partial class ViewInscricoes : UserControl
    {
        private readonly HttpClient _client;

        public ViewInscricoes()
        {
            InitializeComponent();

            // Configura a conexão com a API
            _client = new HttpClient { BaseAddress = new Uri("https://localhost:5001/") };
        }

        // Este evento roda assim que a tela abre
        private async void ViewInscricoes_Load(object sender, EventArgs e)
        {
            await CarregarCandidatos();
        }

        private async System.Threading.Tasks.Task CarregarCandidatos()
        {
            try
            {
                // 1. Chama a API para pegar a lista
                var response = await _client.GetAsync("api/candidatos");

                if (response.IsSuccessStatusCode)
                {
                    // 2. Converte o JSON em uma lista de objetos Candidato
                    var listaCandidatos = await response.Content.ReadFromJsonAsync<List<Candidato>>();

                    // 3. Limpa a tabela antes de preencher
                    dgvInscricoes.Rows.Clear();

                    // 4. Preenche a tabela linha por linha
                    foreach (var c in listaCandidatos)
                    {
                        // Adiciona na Grid (A ordem deve bater com suas colunas visuais)
                        dgvInscricoes.Rows.Add(
                            c.Id,                   // Coluna oculta (se tiver) ou primeira
                            c.NomeCompleto,         // Coluna Candidato
                            c.Cidade + "/" + c.Estado, // Coluna Cidade
                            c.Status.ToString(),    // Coluna Status
                            c.NomeOngResponsavel ?? "--", // Coluna ONG
                            "Ver Detalhes"          // Coluna Ação (Texto do botão)
                        );
                    }
                }
                else
                {
                    MessageBox.Show("Não foi possível carregar as inscrições.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro de conexão: " + ex.Message);
            }
        }

        // Evento de clique na Tabela (Para abrir a Triagem)
        private void dgvInscricoes_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // Verifica se clicou na coluna de "Ação" (ajuste o índice '5' se necessário)
            // Ou verifica se é um botão
            if (e.RowIndex >= 0)
            {
                // Pega o ID da linha clicada (assumindo que está na coluna 0, mesmo que invisível)
                int idCandidato = Convert.ToInt32(dgvInscricoes.Rows[e.RowIndex].Cells[0].Value);

                // TODO: Aqui vamos chamar a navegação para a ViewTriagem passando esse ID
                MessageBox.Show($"Você clicou para ver o candidato ID: {idCandidato}");
            }
        }

        // Botão de Busca (Lupa)
        private async void btnBuscar_Click(object sender, EventArgs e)
        {
            // Aqui você pode implementar um filtro local ou chamar uma API de busca
            await CarregarCandidatos(); // Por enquanto recarrega tudo
        }
    }
}
