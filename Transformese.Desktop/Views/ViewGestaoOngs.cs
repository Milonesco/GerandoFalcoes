using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using Transformese.Domain.Entities;

namespace Transformese.Desktop.Views
{
    public partial class ViewGestaoOngs : UserControl
    {
        private readonly HttpClient _client;

        public ViewGestaoOngs()
        {
            InitializeComponent();

            // Configura a conexão com a API
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;
            _client = new HttpClient(handler) { BaseAddress = new Uri("https://localhost:5001/") };
        }

        private async void btnSalvarOng_Click(object sender, EventArgs e)
        {
            // 1. Validação Simples
            if (string.IsNullOrWhiteSpace(txtResponsavel.Text))
            {
                MessageBox.Show("O nome da Unidade/ONG é obrigatório.");
                return;
            }

            try
            {
                // 2. Cria o objeto com os dados da tela
                var novaUnidade = new UnidadeParceira
                {
                    NomeUnidade = txtResponsavel.Text,
                    NomeResponsavel = txtResponsavelOng.Text,
                    Email = txtEmailOng.Text,
                    Cidade = txtCidadeOng.Text,
                    NomeVaga = txtNomeVaga.Text,
                    // Se o numQtdVagas for um NumericUpDown, usamos .Value, se for TextBox, convertemos int.Parse
                    QuantidadeVagas = (int)numVagas.Value
                };

                // 3. Envia para a API (POST)
                // Certifique-se de que sua API tem um Controller para "api/unidades"
                var response = await _client.PostAsJsonAsync("api/unidades", novaUnidade);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show("Unidade cadastrada com sucesso!");
                    LimparCampos();
                }
                else
                {
                    var erro = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Erro ao cadastrar: {erro}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro de conexão: {ex.Message}");
            }
        }

        private void LimparCampos()
        {
            txtResponsavel.Clear();
            txtResponsavelOng.Clear();
            txtEmailOng.Clear();
            txtCidadeOng.Clear();
            txtNomeVaga.Clear();
            if (numVagas != null) numVagas.Value = 0;

            txtResponsavel.Focus();
        }

        // Método vazio gerado pelo designer (pode deixar ou apagar se remover o evento lá)
        private void guna2Separator2_Click(object sender, EventArgs e) { }
    }
}