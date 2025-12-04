using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using Transformese.Domain.Entities;

namespace Transformese.Desktop
{
    public partial class frmPainelAdmin : Form
    {
        // Propriedade para saber se a autorização foi dada
        public bool Autorizado { get; private set; } = false;
        private readonly HttpClient _client;

        public frmPainelAdmin()
        {
            InitializeComponent();

            // Configuração padrão do HttpClient (igual ao que fizemos antes)
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;
            _client = new HttpClient(handler) { BaseAddress = new Uri("https://localhost:5001/") };
        }

        private async void btnLiberarAcesso_Click(object sender, EventArgs e)
        {
            try
            {
                var loginRequest = new
                {
                    Email = txtEmailAdmin.Text,
                    Senha = txtSenhaAdmin.Text
                };

                // Tenta logar com as credenciais digitadas
                var response = await _client.PostAsJsonAsync("api/funcionarios/login", loginRequest);

                if (response.IsSuccessStatusCode)
                {
                    // Lê os dados do funcionário que retornou
                    var admin = await response.Content.ReadFromJsonAsync<Funcionario>();

                    // Verifica se esse usuário REALMENTE é um Admin
                    if (admin.EhAdministrador)
                    {
                        Autorizado = true;
                        this.DialogResult = DialogResult.OK; // Fecha o form retornando Sucesso
                        this.Close();
                    }
                    else
                    {
                        MessageBox.Show("Este usuário não tem permissão de Administrador.");
                    }
                }
                else
                {
                    MessageBox.Show("Credenciais administrativas inválidas.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro de conexão: {ex.Message}");
            }
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}