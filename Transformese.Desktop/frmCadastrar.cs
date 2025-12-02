using System.Diagnostics;
using System;
using System.Windows.Forms;

namespace Transformese.Desktop
{
    public partial class frmCadastrar : Form
    {
        public frmCadastrar()
        {
            InitializeComponent();
        }

        private void btnCadastrar_Click(object sender, System.EventArgs e)
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
                mdNotifica.Show("As senhas digitadas não conferem. Por favor, verifique e tente novamente.");

                txtSenha.Clear();
                txtConfirmacaoSenha.Clear();
                txtSenha.Focus();

                return;
            }
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
    }
}
