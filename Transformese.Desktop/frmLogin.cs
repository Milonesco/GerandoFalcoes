using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using Transformese.Domain.Entities; // Certifique-se que o namespace do Funcionario está aqui

namespace Transformese.Desktop
{
    public partial class frmLogin : Form
    {
        private readonly HttpClient _client;

        public frmLogin()
        {
            InitializeComponent();
            _client = new HttpClient();
            // Ignora SSL para localhost (opcional, mas evita erros em dev)
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;
            _client = new HttpClient(handler) { BaseAddress = new Uri("https://localhost:5001/") };
        }

        private void Login_Load(object sender, EventArgs e)
        {
            // Foco inicial no campo de email
            if (txtEmail != null) txtEmail.Focus();
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
                Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            }
            catch (Exception ex)
            {
                mdNotifica.Show($"Erro ao abrir link: {ex.Message}");
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
                btnLogin.Enabled = false;
                btnLogin.Text = "Entrando...";

                var loginData = new
                {
                    Email = txtEmail.Text,
                    Senha = txtSenha.Text
                };

                // Envia para a API
                HttpResponseMessage response = await _client.PostAsJsonAsync("api/funcionarios/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    // Lê o objeto Funcionário retornado pela API
                    var funcionarioLogado = await response.Content.ReadFromJsonAsync<Funcionario>();

                    if (funcionarioLogado != null)
                    {
                        // 1. SALVA NA SESSÃO GLOBAL (Importante para os Logs)
                        SessaoSistema.UsuarioLogado = funcionarioLogado;

                        mdNotifica.Show($"Bem-vindo, {funcionarioLogado.Nome}!");

                        // 2. Abre o Dashboard
                        var dashboard = new frmDashboard(funcionarioLogado);
                        this.Hide();
                        dashboard.Show();
                    }
                    else
                    {
                        mdNotifica.Show("Erro ao ler dados do usuário.");
                    }
                }
                else
                {
                    // Lê a mensagem de erro da API (ex: "Senha inválida")
                    var erro = await response.Content.ReadAsStringAsync();
                    mdNotifica.Show($"Falha no login: {erro}");
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