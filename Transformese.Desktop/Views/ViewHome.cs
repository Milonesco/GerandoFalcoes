using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Transformese.Desktop.Views
{
    public partial class ViewHome : UserControl
    {
        // Cores
        private readonly Color CorCiano = ColorTranslator.FromHtml("#00A89D");
        private readonly Color CorRosa = ColorTranslator.FromHtml("#EC2262");
        private readonly Color CorLaranja = ColorTranslator.FromHtml("#E96B2A");
        private readonly Color CorVerde = ColorTranslator.FromHtml("#16A34A");
        private readonly Color CorTexto = ColorTranslator.FromHtml("#333333");

        public ViewHome()
        {
            InitializeComponent();

            // 1. TAMANHO EXATO DA MOLDURA (1046 x 638)
            this.Size = new Size(1046, 638);
            this.Dock = DockStyle.Fill;
            this.BackColor = ColorTranslator.FromHtml("#f3f4f6");
            this.DoubleBuffered = true;

            ConstruirLayoutMilimetrico();
        }

        private void ConstruirLayoutMilimetrico()
        {
            // Grid Mestre para dividir Altura (Topo fixo / Resto preenche)
            var layoutVertical = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 1,
                RowCount = 2,
                Margin = new Padding(0)
            };
            // Linha 1: Altura 150px (Cards)
            layoutVertical.RowStyles.Add(new RowStyle(SizeType.Absolute, 150F));
            // Linha 2: 100% (Resto)
            layoutVertical.RowStyles.Add(new RowStyle(SizeType.Percent, 100F));

            this.Controls.Add(layoutVertical);

            // --- PARTE DE CIMA: CARDS (Linha 0) ---
            var gridKPIs = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 5,
                RowCount = 1,
                Margin = new Padding(0, 0, 0, 10) // Espacinho de 10px pra baixo
            };
            for (int i = 0; i < 5; i++) gridKPIs.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 20F));

            gridKPIs.Controls.Add(CriarCard("TOTAL", "1.250", CorCiano), 0, 0);
            gridKPIs.Controls.Add(CriarCard("EM ANÁLISE", "150", CorLaranja), 1, 0);
            gridKPIs.Controls.Add(CriarCard("ENTREVISTAS", "80", CorRosa), 2, 0);
            gridKPIs.Controls.Add(CriarCard("MATRICULADOS", "60", CorVerde), 3, 0);
            gridKPIs.Controls.Add(CriarCard("VAGAS", "340", Color.Gray), 4, 0);

            layoutVertical.Controls.Add(gridKPIs, 0, 0);

            // --- PARTE DE BAIXO: TABELA + BOTÃO (Linha 1) ---
            var gridBottom = new TableLayoutPanel
            {
                Dock = DockStyle.Fill,
                ColumnCount = 2,
                RowCount = 1
            };
            gridBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 70F)); // Tabela maior
            gridBottom.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 30F)); // Botão menor

            // Coluna Esquerda: Tabela
            var cardTabela = new Guna2Panel { Dock = DockStyle.Fill, FillColor = Color.White, BorderRadius = 12, Margin = new Padding(0, 10, 10, 0) };
            cardTabela.ShadowDecoration.Enabled = true;

            var lblTituloTab = new Label { Text = "Últimas Inscrições", Font = new Font("Segoe UI", 12, FontStyle.Bold), Location = new Point(20, 15), AutoSize = true };
            cardTabela.Controls.Add(lblTituloTab);

            var gridView = new Guna2DataGridView { Location = new Point(20, 50), Size = new Size(cardTabela.Width - 40, cardTabela.Height - 70), Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right, BackgroundColor = Color.White, BorderStyle = BorderStyle.None, Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default, RowHeadersVisible = false, AllowUserToAddRows = false, ColumnHeadersHeight = 40, RowTemplate = { Height = 45 } };
            gridView.ColumnHeadersDefaultCellStyle.BackColor = Color.White; gridView.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray; gridView.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold); gridView.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 248, 255); gridView.DefaultCellStyle.SelectionForeColor = Color.Black;

            gridView.Columns.Add("Nome", "CANDIDATO"); gridView.Columns.Add("Cidade", "CIDADE"); gridView.Columns.Add("Status", "STATUS");
            gridView.Rows.Add("Maria Silva", "Ferraz", "Novo");

            cardTabela.Controls.Add(gridView);
            gridBottom.Controls.Add(cardTabela, 0, 0);

            // Coluna Direita: Botão
            var pnlBotao = new Panel { Dock = DockStyle.Fill, Padding = new Padding(10, 10, 0, 0) };
            var btnNovo = new Guna2GradientButton { Text = "Novo Candidato", Dock = DockStyle.Top, Height = 80, BorderRadius = 12, FillColor = CorCiano, FillColor2 = Color.Teal, Font = new Font("Segoe UI", 14, FontStyle.Bold), Cursor = Cursors.Hand };
            pnlBotao.Controls.Add(btnNovo);
            gridBottom.Controls.Add(pnlBotao, 1, 0);

            layoutVertical.Controls.Add(gridBottom, 0, 1);
        }

        private Control CriarCard(string titulo, string valor, Color cor)
        {
            var pnlMargem = new Panel { Dock = DockStyle.Fill, Padding = new Padding(0, 0, 15, 0) };
            var card = new Guna2Panel { Dock = DockStyle.Fill, FillColor = Color.White, BorderRadius = 12 };
            card.ShadowDecoration.Enabled = true; card.ShadowDecoration.Shadow = new Padding(2);
            card.Controls.Add(new Panel { Dock = DockStyle.Left, Width = 5, BackColor = cor });
            card.Controls.Add(new Label { Text = titulo, ForeColor = Color.Gray, Font = new Font("Segoe UI", 8, FontStyle.Bold), Location = new Point(15, 15), AutoSize = true });
            card.Controls.Add(new Label { Text = valor, ForeColor = CorTexto, Font = new Font("Segoe UI", 24, FontStyle.Bold), Location = new Point(12, 40), AutoSize = true });
            pnlMargem.Controls.Add(card);
            return pnlMargem;
        }
    }
}