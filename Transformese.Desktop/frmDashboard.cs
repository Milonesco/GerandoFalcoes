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
            _usuarioLogado = new Funcionario { Nome = "Usuário", Sobrenome = "Teste" };
            ConfigurarDashboard();
        }

        private void ConfigurarDashboard()
        {
            try
            {
                if (lblBoasVindas != null) lblBoasVindas.Text = $"Bem-vindo(a), {_usuarioLogado.Nome}!";
                if (lblNomeUsuario != null) lblNomeUsuario.Text = $"{_usuarioLogado.Nome} {_usuarioLogado.Sobrenome}";
                if (lblCargoUsuario != null) lblCargoUsuario.Text = "Administrador";

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
                mdNotifica.Show($"Erro ao configurar dashboard: {ex.Message}");
            }
        }

        // --- NOVO MÉTODO: Gerencia visualmente os botões ---
        // Recebe o botão que deve ficar "aceso" e apaga os outros
        private void AtualizarBotoes(object botaoAtivo)
        {
            // 1. Desmarca TODOS primeiro (Reset)
            // O 'dynamic' permite acessar a propriedade Checked sem saber o tipo exato (GunaButton, etc)
            if (btnDashboard != null) ((dynamic)btnDashboard).Checked = false;
            if (btnNovoCandidato != null) ((dynamic)btnNovoCandidato).Checked = false;
            if (btnTriagem != null) ((dynamic)btnTriagem).Checked = false;
            if (btnEntrevistas != null) ((dynamic)btnEntrevistas).Checked = false;
            if (btnNovoCandidato != null) ((dynamic)btnNovoCandidato).Checked = false;
            if (btnGestaoOngs != null) ((dynamic)btnGestaoOngs).Checked = false;
            if (btnRelatorios != null) ((dynamic)btnRelatorios).Checked = false;

            // 2. Marca APENAS o botão que recebeu como parâmetro
            if (botaoAtivo != null)
            {
                ((dynamic)botaoAtivo).Checked = true;
            }
        }

        private void CarregarHome()
        {
            var viewHome = new ViewHome();

            // Assina o evento
            viewHome.OnSolicitarNovoCandidato += (sender, e) =>
            {
                CarregarNovoCandidato();
            };

            NavegarPara(viewHome);

            // ATUALIZAÇÃO VISUAL: Marca o botão Dashboard
            AtualizarBotoes(btnDashboard);
        }

        private void CarregarNovoCandidato()
        {
            var viewNovo = new ViewNovoCandidato();
            NavegarPara(viewNovo);

            // ATUALIZAÇÃO VISUAL: Marca o botão Novo Candidato automaticamente!
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

        // --- EVENTOS DE CLICK DO MENU ---
        // Agora todos chamam o AtualizarBotoes para garantir consistência

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            CarregarHome();
            // Não precisa chamar AtualizarBotoes aqui pq o método CarregarHome já chama
        }

        private void btnNovoCandidato_Click(object sender, EventArgs e)
        {
            CarregarNovoCandidato();
            // Não precisa chamar AtualizarBotoes aqui pq CarregarNovoCandidato já chama
        }

        private void btnInscricoes_Click(object sender, EventArgs e)
        {
            NavegarPara(new ViewInscricoes());
            AtualizarBotoes(sender); // 'sender' é o próprio botão clicado
        }

        private void btnTriagem_Click(object sender, EventArgs e)
        {
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
            var confirmacao = mdNotifica.Show("Deseja realmente sair?");
            if (confirmacao == DialogResult.Yes)
            {
                frmLogin login = new frmLogin();
                login.Show();
                this.Close();
            }
        }
    }
}