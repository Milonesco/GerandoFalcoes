using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;

namespace Transformese.Desktop
{
    public partial class frmLogin : Form
    {
        private readonly HttpClient _client;

        public frmLogin()
        {
            InitializeComponent();
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:5001/");
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }
        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            frmCadastrar formulario = new();
            formulario.Show();
        }

        private void chkSenha_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSenha.Checked)
            {
                txtSenha.UseSystemPasswordChar = false;
                chkSenha.Text = "Ocultar senha";
            }
            else
            {
                txtSenha.UseSystemPasswordChar = true;
                chkSenha.Text = "Exibir senha";
            }
        }

        private void lblEsqueciSenha_Click(object sender, EventArgs e)
        {
            frmEsqueciSenha frmEsqueciSenha = new();
            frmEsqueciSenha.Show();
        }

        private void lblGerandoFalcoes_Click(object sender, EventArgs e)
        {
            AbrirLink("http://www.gerandofalcoes.com/");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
                var confirmacao = mdSair.Show("Deseja realmente sair?");

                if (confirmacao == DialogResult.Yes)
                {
                    Application.Exit();
                }
        }

        private void AbrirLink(string url)
        {
            try
            {
                Process.Start(new ProcessStartInfo(url)
                { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                mdNotifica.Show("Erro", $"erro: {ex.Message}");
            }
        }

        private async void btnLogin_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtSenha.Text))
            {
                mdNotifica.Show("Preencha email e senha!");
                return;
            }

            try
            {
                // Desabilita o botão para evitar clique duplo
                btnLogin.Enabled = false;
                btnLogin.Text = "Entrando...";

                var loginData = new
                {
                    Email = txtEmail.Text,
                    Senha = txtSenha.Text
                };

                HttpResponseMessage response = await _client.PostAsJsonAsync("api/funcionarios/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    // 1. Tenta ler o funcionário que a API devolveu
                    // (Certifique-se que sua API retorna o objeto Funcionario no login)
                    var funcionarioLogado = await response.Content.ReadFromJsonAsync<Transformese.Domain.Entities.Funcionario>();

                    mdNotifica.Show($"Bem-vindo, {funcionarioLogado.Nome}!");

                    // 2. Passa o funcionário para o Dashboard
                    var dashboard = new frmDashboard(funcionarioLogado);
                    this.Hide();
                    dashboard.Show();
                    // Não damos Close() aqui para manter a app rodando, usamos o Hide
                }
                else
                {
                    mdNotifica.Show("Email ou senha incorretos.");
                }
            }
            catch (Exception ex)
            {
                mdNotifica.Show("Erro de conexão com a API: " + ex.Message);
            }
            finally
            {
                btnLogin.Enabled = true;
                btnLogin.Text = "ENTRAR";
            }
        }
    }
}
