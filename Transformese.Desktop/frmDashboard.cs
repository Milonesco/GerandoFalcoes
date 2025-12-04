using System;
using System.Drawing; // Necessário para cores
using System.Windows.Forms;
using Transformese.Desktop.Views;
using Transformese.Domain.Entities;

namespace Transformese.Desktop
{
    public partial class frmDashboard : Form
    {
        private Funcionario _usuarioLogado;

        // --- CONSTRUTOR PRINCIPAL (COM LOGIN) ---
        public frmDashboard(Funcionario funcionario)
        {
            InitializeComponent();
            _usuarioLogado = funcionario;
            ConfigurarDashboard();
        }

        // --- CONSTRUTOR DE TESTE (SEM LOGIN) ---
        public frmDashboard()
        {
            InitializeComponent();
            // Usuário Fake para evitar erros visuais durante testes
            _usuarioLogado = new Funcionario { Nome = "Usuário", Sobrenome = "Teste" };
            ConfigurarDashboard();
        }

        // --- CONFIGURAÇÃO INICIAL DA TELA ---
        private void ConfigurarDashboard()
        {
            try
            {
                // 1. Saudação (Cabeçalho Esquerdo)
                // O sinal '?' evita erro se o label não existir (Null Check)
                if (lblBoasVindas != null)
                    lblBoasVindas.Text = $"Bem-vindo(a), {_usuarioLogado.Nome}!";

                // 2. Painel de Usuário (Canto Superior Direito)
                if (lblNomeUsuario != null)
                    lblNomeUsuario.Text = $"{_usuarioLogado.Nome} {_usuarioLogado.Sobrenome}";

                if (lblCargoUsuario != null)
                    lblCargoUsuario.Text = "Administrador"; // Valor fixo por enquanto

                // 3. Foto de Perfil (Bolinha Colorida)
                if (pbPerfil != null)
                {
                    pbPerfil.Image = null; // Limpa imagem anterior
                    pbPerfil.FillColor = Color.FromArgb(0, 168, 150); // Verde Turquesa do Logo
                }

                // 4. Carrega a Home
                NavegarPara(new ViewHome());

                // 5. Marca o botão visualmente
                if (btnDashboard != null) btnDashboard.Checked = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao configurar dashboard: {ex.Message}");
            }
        }

        // --- O CÉREBRO DA NAVEGAÇÃO ---
        private void NavegarPara(UserControl view)
        {
            // Verifica se o painel existe antes de tentar usar
            if (pnlContent == null) return;

            try
            {
                view.Dock = DockStyle.Fill;
                pnlContent.Controls.Clear(); // Limpa a tela anterior
                pnlContent.Controls.Add(view); // Adiciona a nova tela
                view.BringToFront(); // Garante que fique na frente de tudo
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro ao navegar: {ex.Message}");
            }
        }

        // --- EVENTOS DOS BOTÕES (CLICKS) ---
        // IMPORTANTE: Verifique se o "Raiozinho" ⚡ nas Propriedades está ligado a estes métodos!

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            NavegarPara(new ViewHome());
        }

        private void btnInscricoes_Click(object sender, EventArgs e)
        {
            NavegarPara(new ViewInscricoes());
        }

        private void btnTriagem_Click(object sender, EventArgs e)
        {
            NavegarPara(new ViewTriagem());
        }

        private void btnEntrevistas_Click(object sender, EventArgs e)
        {
            NavegarPara(new ViewEntrevista());
        }

        private void btnNovoCandidato_Click(object sender, EventArgs e)
        {
            NavegarPara(new ViewNovoCandidato());
        }

        private void btnGestaoOngs_Click(object sender, EventArgs e)
        {
            NavegarPara(new ViewGestaoOngs());
        }

        private void btnRelatorios_Click(object sender, EventArgs e)
        {
            NavegarPara(new ViewRelatorios());
        }

        // --- BOTÃO SAIR ---
        private void btnSair_Click(object sender, EventArgs e)
        {
            var confirmacao = MessageBox.Show("Deseja realmente sair?", "Sair",
                                              MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (confirmacao == DialogResult.Yes)
            {
                frmLogin login = new frmLogin();
                login.Show();
                this.Close();
            }
        }

        private void frmDashboard_Load(object sender, EventArgs e)
        {
            // this.WindowState = FormWindowState.Maximized; // Comentei para não maximizar

            // Opcional: Se quiser que abra no meio da tela
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}