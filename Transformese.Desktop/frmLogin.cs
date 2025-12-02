using System;
using System.Diagnostics;
using System.Drawing;
using System.Windows.Forms;

namespace Transformese.Desktop
{
    public partial class frmLogin : Form
    {

        public frmLogin()
        {
            InitializeComponent();
        }

        private void Login_Load(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        private void btnCadastrar_Click(object sender, EventArgs e)
        {
            frmCadastrar formulario = new();
            formulario.Show();
            frmLogin frmLogin = new ();
            frmLogin.Hide();
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
                    frmLogin principal = new frmLogin();
                    principal.Show();
                    Close();
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
    }
}
