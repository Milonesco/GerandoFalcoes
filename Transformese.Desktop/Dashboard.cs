using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Transformese.Desktop.Views;

namespace Transformese.Desktop.Views
{
    public partial class Dashboard : Form
    {
        private Panel pnlContainerConteudo;
        private Guna2Button btnAtivo = null;

        // Cores
        private readonly Color CorSidebar = ColorTranslator.FromHtml("#161616");
        private readonly Color CorFundo = ColorTranslator.FromHtml("#f3f4f6");
        private readonly Color CorCiano = ColorTranslator.FromHtml("#00A89D");
        private readonly Color CorTextoMenu = ColorTranslator.FromHtml("#9ca3af");
        private readonly Color CorTextoMenuAtivo = Color.White;

        public Dashboard()
        {
            InitializeComponent();
            ConfigurarJanelaFixa(); // <--- MUDANÇA AQUI
            ConstruirLayoutFixo();

            Navegar(new ViewHome(), null);
        }

        private void ConfigurarJanelaFixa()
        {
            this.Text = "Portal Transforme-se";
            // TRAVA O TAMANHO (Não deixa redimensionar)
            this.Size = new Size(1366, 768);
            this.MaximumSize = new Size(1366, 768);
            this.MinimumSize = new Size(1366, 768);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
        }

        private void ConstruirLayoutFixo()
        {
            var borderless = new Guna2BorderlessForm(this.components);
            borderless.BorderRadius = 15;

            // 1. SIDEBAR (Fixa 260px)
            var pnlSidebar = new Panel { Dock = DockStyle.Left, Width = 260, BackColor = CorSidebar };
            this.Controls.Add(pnlSidebar);

            // Logo
            var pnlLogo = new Panel { Dock = DockStyle.Top, Height = 100 };
            pnlLogo.Controls.Add(new Label { Text = "GERANDO FALCÕES", ForeColor = Color.White, Font = new Font("Segoe UI", 14, FontStyle.Bold), Location = new Point(20, 30), AutoSize = true });
            pnlLogo.Controls.Add(new Label { Text = "GESTÃO", ForeColor = Color.Gray, Font = new Font("Segoe UI", 8), Location = new Point(22, 60), AutoSize = true });
            pnlSidebar.Controls.Add(pnlLogo);

            // Botão Sair
            var btnSair = new Guna2Button { Text = "Sair", Dock = DockStyle.Bottom, Height = 50, FillColor = Color.Transparent, ForeColor = Color.Red, TextAlign = HorizontalAlignment.Left, ImageOffset = new Point(10, 0), TextOffset = new Point(20, 0) };
            btnSair.Click += (s, e) => Application.Exit();
            pnlSidebar.Controls.Add(btnSair);

            // Menu
            var pnlMenu = new FlowLayoutPanel { Dock = DockStyle.Fill, FlowDirection = FlowDirection.TopDown, Padding = new Padding(0, 10, 0, 0) };
            pnlSidebar.Controls.Add(pnlMenu);
            pnlMenu.BringToFront();

            var btnHome = CriarBotaoMenu(pnlMenu, "Dashboard");
            btnHome.Click += (s, e) => Navegar(new ViewHome(), btnHome);

            var btnInscricoes = CriarBotaoMenu(pnlMenu, "Inscrições Recebidas");
            btnInscricoes.Click += (s, e) => Navegar(new ViewInscricoes(), btnInscricoes);

            CriarBotaoMenu(pnlMenu, "Triagem Inicial");
            CriarBotaoMenu(pnlMenu, "Entrevistas (ONGs)");
            CriarBotaoMenu(pnlMenu, "Novo Candidato");
            CriarBotaoMenu(pnlMenu, "Gestão de ONGs");

            // 2. CONTEÚDO (Preenche o resto fixo: 1106px)
            var pnlDireita = new Panel { Dock = DockStyle.Fill, BackColor = CorFundo };
            this.Controls.Add(pnlDireita);

            // Header
            var pnlHeader = new Panel { Dock = DockStyle.Top, Height = 70, BackColor = Color.White };
            pnlHeader.Controls.Add(new Label { Text = "Portal Transforme-se", Font = new Font("Segoe UI", 16, FontStyle.Bold), Location = new Point(30, 20), AutoSize = true });
            pnlHeader.Controls.Add(new Guna2CircleButton { Text = "M", FillColor = CorCiano, ForeColor = Color.White, Size = new Size(40, 40), Location = new Point(1030, 15) }); // Posição fixa no HD
            pnlDireita.Controls.Add(pnlHeader);

            // Container de Conteúdo (COM MOLDURA DE 30px)
            var pnlMoldura = new Panel { Dock = DockStyle.Fill, Padding = new Padding(30) }; // <--- MOLDURA AQUI
            pnlDireita.Controls.Add(pnlMoldura);
            pnlMoldura.BringToFront();

            pnlContainerConteudo = new Panel { Dock = DockStyle.Fill }; // As views preenchem a moldura
            pnlMoldura.Controls.Add(pnlContainerConteudo);
        }

        private void Navegar(UserControl novaTela, Guna2Button botao)
        {
            if (btnAtivo != null) { btnAtivo.FillColor = Color.Transparent; btnAtivo.ForeColor = CorTextoMenu; btnAtivo.Controls.Clear(); }
            if (botao != null) { btnAtivo = botao; btnAtivo.FillColor = Color.FromArgb(30, 30, 30); btnAtivo.ForeColor = CorTextoMenuAtivo; botao.Controls.Add(new Panel { Dock = DockStyle.Left, Width = 4, BackColor = CorCiano }); }

            pnlContainerConteudo.Controls.Clear();

            // FORÇA O TAMANHO DA TELA FILHA PARA CABER NA MOLDURA
            novaTela.Size = pnlContainerConteudo.Size;
            novaTela.Dock = DockStyle.Fill;

            pnlContainerConteudo.Controls.Add(novaTela);
        }

        private Guna2Button CriarBotaoMenu(Control parent, string texto)
        {
            var btn = new Guna2Button { Text = "    " + texto, Width = 260, Height = 50, FillColor = Color.Transparent, ForeColor = CorTextoMenu, Font = new Font("Segoe UI", 10, FontStyle.Bold), TextAlign = HorizontalAlignment.Left, Margin = new Padding(0), Cursor = Cursors.Hand };
            parent.Controls.Add(btn);
            return btn;
        }
    }
}