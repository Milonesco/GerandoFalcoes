using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms; // Componentes Guna2
using Transformese.Desktop.Views; // Para ViewHome, ViewInscricoes, etc.

namespace Transformese.Desktop
{
    public partial class Dashboard : Form
    {
        // Componentes Fixos da Estrutura (Usando Guna2)
        private Guna2GradientPanel panelSidebar; // Use GradientPanel para um visual mais moderno
        private Guna2Panel panelHeader;
        private Guna2Panel panelContent;

        // Exemplo de botões de navegação
        private Guna2Button btnHome;
        private Guna2Button btnInscricoes;

        public Dashboard()
        {
            InitializeCustomComponents();

            // Conecta eventos de navegação
            btnHome.Click += (sender, e) => LoadView(new ViewHome());
            btnInscricoes.Click += (sender, e) => LoadView(new ViewInscricoes());

            // Carrega a primeira View ao iniciar
            this.Load += (sender, e) => LoadView(new ViewHome());
        }

        private void InitializeCustomComponents()
        {
            this.AutoScaleMode = AutoScaleMode.None;

            // --- 1. Configuração do Formulário Principal ---
            this.Text = "Transformese - Backoffice";
            this.Size = new Size(1600, 900);
            this.WindowState = FormWindowState.Maximized;
            this.BackColor = Color.FromArgb(240, 240, 240); // Cinza claro para o fundo

            // --- 2. Painel Content (Área Dinâmica) ---
            // É melhor adicionar o Content primeiro para que ele seja preenchido após a Sidebar e Header
            panelContent = new Guna2Panel();
            panelContent.BackColor = AppTheme.Branco; // Fundo branco para o conteúdo
            panelContent.Dock = DockStyle.Fill;
            this.Controls.Add(panelContent);

            // --- 3. Painel Header (Topo) ---
            panelHeader = new Guna2Panel();
            panelHeader.BackColor = AppTheme.Branco;
            panelHeader.Dock = DockStyle.Top;
            panelHeader.Height = 60;
            // O Guna2Panel pode ser arredondado/ter sombra, mas mantemos o design flat aqui
            this.Controls.Add(panelHeader);

            // Exemplo: Adicionar Título ao Header
            Label lblHeaderTitle = new Label();
            lblHeaderTitle.Text = "DASHBOARD DE GESTÃO";
            lblHeaderTitle.Font = new Font("Poppins", 14, FontStyle.Bold);
            lblHeaderTitle.Location = new Point(20, 15);
            lblHeaderTitle.AutoSize = true;
            panelHeader.Controls.Add(lblHeaderTitle);

            // --- 4. Painel Sidebar (Esquerda) ---
            // Usando Guna2GradientPanel para potencializar o estilo
            panelSidebar = new Guna2GradientPanel();
            panelSidebar.FillColor = AppTheme.CinzaEscuro;
            panelSidebar.FillColor2 = AppTheme.CinzaEscuro; // Cor sólida para o exemplo
            panelSidebar.Dock = DockStyle.Left;
            panelSidebar.Width = 220;
            this.Controls.Add(panelSidebar);

            // --- 5. Componentes da Sidebar (Navegação) ---

            // Botão Home
            btnHome = CreateSidebarButton("🏠 HOME", 100);
            panelSidebar.Controls.Add(btnHome);

            // Botão Inscrições
            btnInscricoes = CreateSidebarButton("📝 INSCRIÇÕES", 160);
            panelSidebar.Controls.Add(btnInscricoes);

            // Garante que a Sidebar fique por cima do Content
            panelSidebar.BringToFront();
        }

        // Método utilitário para criar botões de Sidebar (Guna2Button)
        private Guna2Button CreateSidebarButton(string text, int top)
        {
            Guna2Button btn = new Guna2Button();
            btn.Text = text;
            btn.FillColor = AppTheme.CinzaEscuro; // Cor de fundo da Sidebar
            btn.ForeColor = AppTheme.Branco;
            btn.Size = new Size(panelSidebar.Width, 50);
            btn.Location = new Point(0, top);
            btn.Font = new Font("Poppins", 10, FontStyle.Regular);
            btn.TextAlign = HorizontalAlignment.Left;
            btn.Padding = new Padding(15, 0, 0, 0);
            btn.HoverState.FillColor = Color.FromArgb(40, 40, 40); // Efeito hover
            return btn;
        }

        // Método principal para carregar o conteúdo dinâmico
        public void LoadView(UserControl newView)
        {
            if (newView == null) return;

            panelContent.Controls.Clear();
            newView.Dock = DockStyle.Fill;
            panelContent.Controls.Add(newView);
            newView.BringToFront();
        }

        private void Dashboard_Load(object sender, EventArgs e)
        {

        }

        private void btnIncricoes_Click(object sender, EventArgs e)
        {

        }
    }
}