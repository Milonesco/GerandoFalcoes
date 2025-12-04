using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Transformese.Desktop
{
    public partial class frmEsqueciSenha : Form
    {
        private readonly HttpClient _client;
        public frmEsqueciSenha()
        {
            InitializeComponent();

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;

            _client = new HttpClient(handler);
            _client.BaseAddress = new Uri("https://localhost:5001/");
        }

        private void chkSenha_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSenha.Checked)
            {
                txtSenha.UseSystemPasswordChar = false;
                txtNovaSenha.UseSystemPasswordChar = false;
                chkSenha.Text = "Ocultar senha";
            }
            else
            {
                txtSenha.UseSystemPasswordChar = true;
                txtNovaSenha.UseSystemPasswordChar = true;
                chkSenha.Text = "Exibir senha";
            }
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void btnAtualizarSenha_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) ||
                            string.IsNullOrWhiteSpace(txtSenha.Text) ||
                            string.IsNullOrWhiteSpace(txtNovaSenha.Text))
            {
                mdNotifica.Show("Por favor, preencha todos os campos.");
                return;
            }

            // 2. Valida se as senhas batem
            if (txtSenha.Text != txtNovaSenha.Text)
            {
                mdNotifica.Show("As senhas não conferem. Verifique e tente novamente.");
                txtNovaSenha.Clear();
                txtNovaSenha.Focus();
                return;
            }

            // 3. SEGURANÇA: Chama o Painel do Administrador para autorizar
            // Isso abre a janelinha do admin e espera ele dar OK
            frmPainelAdmin painelAdmin = new frmPainelAdmin();
            DialogResult resultadoAuth = painelAdmin.ShowDialog();

            if (resultadoAuth != DialogResult.OK)
            {
                mdNotifica.Show("Operação cancelada ou não autorizada pelo administrador.");
                return; // Para tudo aqui se o admin não autorizou
            }

            // --- Se chegou aqui, o Admin autorizou! ---

            try
            {
                // 4. Prepara os dados para enviar pra API
                var dadosReset = new
                {
                    Email = txtEmail.Text,
                    NovaSenha = txtNovaSenha.Text
                };

                // 5. Chama a API (Rota que criamos no Passo 1)
                HttpResponseMessage response = await _client.PutAsJsonAsync("api/funcionarios/reset-senha", dadosReset);

                if (response.IsSuccessStatusCode)
                {
                    mdNotifica.Show("Sucesso! A senha foi redefinida.");
                    this.Close(); // Fecha a tela de esqueci senha
                }
                else
                {
                    // Lê o erro que veio da API (ex: "Email não encontrado")
                    string erro = await response.Content.ReadAsStringAsync();
                    mdNotifica.Show($"Não foi possível atualizar: {erro}");
                }
            }
            catch (Exception ex)
            {
                mdNotifica.Show($"Erro de conexão com o servidor: {ex.Message}");
            }
        }
    }
}
