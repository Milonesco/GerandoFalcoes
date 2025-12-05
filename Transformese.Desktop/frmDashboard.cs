using System;
using System.Drawing;
using System.Windows.Forms;
using Transformese.Desktop.Views;
using Transformese.Domain.Entities;

namespace Transformese.Desktop
{
    public partial class frmDashboard : Form
    {
        private Funcionario _usuarioLogado;

        public frmDashboard(Funcionario funcionario)
        {
            InitializeComponent();
            _usuarioLogado = funcionario;
            ConfigurarDashboard();
        }

        public frmDashboard()
        {
            InitializeComponent();
            // Usuário padrão para testes (Sem admin)
            _usuarioLogado = new Funcionario { Nome = "Usuário", Sobrenome = "Teste", EhAdministrador = false };
            ConfigurarDashboard();
        }

        private void ConfigurarDashboard()
        {
            try
            {
                if (lblBoasVindas != null) lblBoasVindas.Text = $"Bem-vindo(a), {_usuarioLogado.Nome}!";
                if (lblNomeUsuario != null) lblNomeUsuario.Text = $"{_usuarioLogado.Nome} {_usuarioLogado.Sobrenome}";

                // --- LÓGICA DE CARGO AQUI ---
                if (lblCargoUsuario != null)
                {
                    // Se for true -> Administrador, Se for false -> Funcionário
                    lblCargoUsuario.Text = _usuarioLogado.EhAdministrador ? "Administrador" : "Funcionário";
                }

                if (pbPerfil != null)
                {
                    pbPerfil.Image = null;
                    pbPerfil.FillColor = Color.FromArgb(0, 168, 150);
                }

                // Carrega a Home e já marca o botão certo
                CarregarHome();
            }
            catch (Exception ex)
            {
                // Se o mdNotifica não estiver visível neste contexto, usa MessageBox
                mdNotifica.Show($"Erro ao configurar dashboard: {ex.Message}");
            }
        }

        // --- MÉTODOS DE NAVEGAÇÃO E BOTÕES (IGUAIS AO SEU CÓDIGO) ---

        private void AtualizarBotoes(object botaoAtivo)
        {
            if (btnDashboard != null) ((dynamic)btnDashboard).Checked = false;
            if (btnNovoCandidato != null) ((dynamic)btnNovoCandidato).Checked = false;
            if (btnTriagem != null) ((dynamic)btnTriagem).Checked = false;
            if (btnEntrevistas != null) ((dynamic)btnEntrevistas).Checked = false;
            if (btnGestaoOngs != null) ((dynamic)btnGestaoOngs).Checked = false;
            if (btnRelatorios != null) ((dynamic)btnRelatorios).Checked = false;

            if (botaoAtivo != null)
            {
                ((dynamic)botaoAtivo).Checked = true;
            }
        }

        private void CarregarHome()
        {
            var viewHome = new ViewHome();
            viewHome.OnSolicitarNovoCandidato += (sender, e) =>
            {
                CarregarNovoCandidato();
            };

            NavegarPara(viewHome);
            AtualizarBotoes(btnDashboard);
        }

        private void CarregarNovoCandidato()
        {
            var viewNovo = new ViewNovoCandidato();
            NavegarPara(viewNovo);
            AtualizarBotoes(btnNovoCandidato);
        }

        private void NavegarPara(UserControl view)
        {
            if (pnlContent == null) return;
            try
            {
                view.Dock = DockStyle.Fill;
                pnlContent.Controls.Clear();
                pnlContent.Controls.Add(view);
                view.BringToFront();
            }
            catch (Exception ex)
            {
                mdNotifica.Show($"Erro ao navegar: {ex.Message}");
            }
        }

        // --- EVENTOS DE CLICK ---

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            CarregarHome();
        }

        private void btnNovoCandidato_Click(object sender, EventArgs e)
        {
            CarregarNovoCandidato();
        }

        private void btnTriagem_Click(object sender, EventArgs e)
        {
            // Atenção: A ViewTriagem precisa ser instanciada sem parâmetros aqui, 
            // pois ela começa vazia esperando o botão "Nova Triagem"
            NavegarPara(new ViewTriagem());
            AtualizarBotoes(sender);
        }

        private void btnEntrevistas_Click(object sender, EventArgs e)
        {
            NavegarPara(new ViewEntrevista());
            AtualizarBotoes(sender);
        }

        private void btnGestaoOngs_Click(object sender, EventArgs e)
        {
            NavegarPara(new ViewGestaoOngs());
            AtualizarBotoes(sender);
        }

        private void btnRelatorios_Click(object sender, EventArgs e)
        {
            NavegarPara(new ViewRelatorios());
            AtualizarBotoes(sender);
        }

        private void btnSair_Click(object sender, EventArgs e)
        {
            // Usando MessageBox padrão pois o mdNotifica pode não retornar DialogResult diretamente
            var confirmacao = mdSair.Show("Deseja realmente sair?");

            if (confirmacao == DialogResult.Yes)
            {
                frmLogin login = new frmLogin();
                login.Show();
                this.Close();
            }
        }
    }
}