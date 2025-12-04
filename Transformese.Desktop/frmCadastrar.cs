using System;
using System.Diagnostics;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using Transformese.Domain.Entities;

namespace Transformese.Desktop
{
    public partial class frmCadastrar : Form
    {
        private readonly HttpClient _client;
        public frmCadastrar()
        {
            InitializeComponent();
            _client = new HttpClient();
            _client.BaseAddress = new Uri("https://localhost:5001/");
        }

        private async void btnCadastrar_ClickAsync(object sender, System.EventArgs e)
        {
            if (!chkTermos.Checked)
            {
                mdTermos.Show("Você deve declarar ter lido e aceito os Termos de Uso e Política de Privacidade para continuar com o cadastro.");
                chkTermos.Focus();

                return;
            }

            string senha = txtSenha.Text;
            string confirmacaoSenha = txtConfirmacaoSenha.Text;

            if (string.IsNullOrEmpty(senha) || string.IsNullOrEmpty(confirmacaoSenha))
            {
                mdNotifica.Show("Os campos Senha e Confirmação de Senha são obrigatórios.");
                txtSenha.Focus();
                return;
            }

            if (senha != confirmacaoSenha)
            {
                if (txtSenha.Text != txtConfirmacaoSenha.Text)
                {
                    mdNotifica.Show("As senhas não conferem. Verifique o campo de confirmação.");

                    txtConfirmacaoSenha.Clear();
                    txtConfirmacaoSenha.Focus(); 
                    return;
                }
            }

            try
            {
                var novoFuncionario = new Funcionario
                {
                    Nome = txtNome.Text,
                    Sobrenome = txtSobrenome.Text,
                    Email = txtEmail.Text,
                    Senha = txtSenha.Text,
                    Sexo = cboSexo.Text,
                    DataCadastro = DateTime.Now,
                    DataNascimento = dtpDataNascimento.Value,
                    Ativo = true
                };
                // Enviamos para a API (O endereço "api/funcionarios" depende do seu Controller na API)
                HttpResponseMessage response = await _client.PostAsJsonAsync("api/funcionarios", novoFuncionario);

                if (response.IsSuccessStatusCode)
                {
                    mdNotifica.Show("Funcionário cadastrado com sucesso!");
                    LimparCampos();
                }
                else
                {
                    // Mostra o erro que a API retornou (caso tenha dado erro lá no servidor)
                    mdNotifica.Show($"Erro na API: {response.ReasonPhrase}");
                }
            }
            catch (Exception ex)
            {
                // Erro de conexão (ex: API desligada)
                mdNotifica.Show($"Erro ao conectar na API: {ex.Message}");
            }
        }

        private void LimparCampos()
        {
            txtNome.Text = "";
            txtEmail.Text = "";
            txtSenha.Text = "";
            cboSexo.Text = "";
            // Resetar radio buttons, etc.
        }

        private void lblGerandoFalcoes_Click(object sender, System.EventArgs e)
        {
            AbrirLink("http://www.gerandofalcoes.com/");
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

        private void chkTermos_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void chkSenha_CheckedChanged(object sender, EventArgs e)
        {
            if (chkSenha.Checked)
            {
                txtSenha.UseSystemPasswordChar = false;
                txtConfirmacaoSenha.UseSystemPasswordChar = false;
                chkSenha.Text = "Ocultar senha";
            }
            else
            {
                txtSenha.UseSystemPasswordChar = true;
                txtConfirmacaoSenha.UseSystemPasswordChar = true;
                chkSenha.Text = "Exibir senha";
            }
        }

        private void frmCadastrar_Load(object sender, EventArgs e)
        {
            cboSexo.Items.Add("Homem Cisgênero");
            cboSexo.Items.Add("Mulher Cisgênero");
            cboSexo.Items.Add("Homem Transgênero");
            cboSexo.Items.Add("Mulher Transgênero");
            cboSexo.Items.Add("Não-binário");
            cboSexo.Items.Add("Prefiro não informar");
            cboSexo.Items.Add("Outro");
        }

        private void txtConfirmacaoSenha_TextChanged(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtConfirmacaoSenha.Text))
            {
                txtConfirmacaoSenha.ForeColor = System.Drawing.Color.Black;
                return;
            }

            if (txtConfirmacaoSenha.Text == txtSenha.Text)
            {
                txtConfirmacaoSenha.ForeColor = System.Drawing.Color.Green;
            }
            else
            {
                txtConfirmacaoSenha.ForeColor = System.Drawing.Color.Red;
            }
        }
    }
}
