using System;
using System.Diagnostics;
using System.Drawing;
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
                chkSenha.Text = "Ocultar";
            }
            else
            {
                txtSenha.UseSystemPasswordChar = true;
                chkSenha.Text = "Exibir";
            }
        }

        private void lblEsqueciSenha_Click(object sender, EventArgs e)
        {

        }

        private void lblGerandoFalcoes_Click(object sender, EventArgs e)
        {
            AbrirLink("http://www.gerandofalcoes.com/");
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            try
            {
                var confirmacao = mdSair.Show("Deseja realmente sair?");

                if (confirmacao == DialogResult.Yes)
                {
                    Application.Exit();
                }
            }
            catch (Exception ex)
            {
                mdNotifica.Show($"Erro no sistema: {ex.Message}");
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
                MessageBox.Show("Preencha email e senha!");
                return;
            }

            try
            {
                var loginData = new
                {
                    Email = txtEmail.Text,
                    Senha = txtSenha.Text
                };

                HttpResponseMessage response = await _client.PostAsJsonAsync("api/funcionarios/login", loginData);

                if (response.IsSuccessStatusCode)
                {
                    mdNotifica.Show("Bem-vindo ao Painel Administrativo da Gerando Falcões!");

                    var dashboard = new Dashboard();
                    this.Hide();
                    dashboard.Show();
                    this.Close();
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
        }
    }
}
