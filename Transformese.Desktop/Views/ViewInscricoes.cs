using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using Transformese.Domain.Entities;
using Transformese.Domain.Enums;

namespace Transformese.Desktop.Views
{
    public partial class ViewInscricoes : UserControl
    {
        private Guna2DataGridView grid;
        private Guna2TextBox txtBusca;
        private List<Candidato> _todosCandidatos;

        public ViewInscricoes()
        {
            InitializeComponent();

            // 1. TAMANHO EXATO DA MOLDURA
            this.Size = new Size(1046, 638);
            this.Dock = DockStyle.Fill;
            this.BackColor = ColorTranslator.FromHtml("#f3f4f6");
            this.DoubleBuffered = true;

            ConstruirLayoutMilimetrico();
            CarregarDados();
        }

        private void ConstruirLayoutMilimetrico()
        {
            // TÍTULO (Fixo 60px altura)
            var pnlTitulo = new Panel { Dock = DockStyle.Top, Height = 60 };
            var lblTitulo = new Label { Text = "Inscrições Recebidas", Font = new Font("Segoe UI", 18, FontStyle.Bold), ForeColor = Color.FromArgb(51, 51, 51), Dock = DockStyle.Left, AutoSize = true, TextAlign = ContentAlignment.BottomLeft, Padding = new Padding(0, 15, 0, 0) };
            pnlTitulo.Controls.Add(lblTitulo);
            this.Controls.Add(pnlTitulo);

            // CARTÃO BRANCO (Preenche todo o resto: 578px)
            var cardBranco = new Guna2Panel
            {
                Dock = DockStyle.Fill,
                FillColor = Color.White,
                BorderRadius = 12
            };
            cardBranco.ShadowDecoration.Enabled = true;

            // Container interno do card (Padding 20px)
            var pnlInterno = new Panel { Dock = DockStyle.Fill, Padding = new Padding(20) };
            cardBranco.Controls.Add(pnlInterno);

            // Barra de Busca (Topo do Card)
            var pnlBusca = new Panel { Dock = DockStyle.Top, Height = 60 };
            txtBusca = new Guna2TextBox { PlaceholderText = "🔍 Buscar por nome...", BorderRadius = 8, Height = 40, Width = 400, Dock = DockStyle.Left };
            txtBusca.TextChanged += TxtBusca_TextChanged;
            pnlBusca.Controls.Add(txtBusca);
            pnlInterno.Controls.Add(pnlBusca);

            // Grid (Preenche o resto do Card)
            grid = new Guna2DataGridView { Dock = DockStyle.Fill, BackgroundColor = Color.White, BorderStyle = BorderStyle.None, Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default, RowHeadersVisible = false, AllowUserToAddRows = false, ColumnHeadersHeight = 45, RowTemplate = { Height = 45 }, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };
            grid.ColumnHeadersDefaultCellStyle.BackColor = Color.White; grid.ColumnHeadersDefaultCellStyle.ForeColor = Color.Gray; grid.ColumnHeadersDefaultCellStyle.Font = new Font("Segoe UI", 9, FontStyle.Bold); grid.DefaultCellStyle.SelectionBackColor = Color.FromArgb(240, 248, 255); grid.DefaultCellStyle.SelectionForeColor = Color.Black;

            grid.Columns.Add("Nome", "CANDIDATO");
            grid.Columns.Add("Cidade", "CIDADE");
            grid.Columns.Add("Status", "STATUS");
            grid.Columns.Add("Ong", "ONG RESP.");

            pnlInterno.Controls.Add(grid);
            grid.BringToFront();

            this.Controls.Add(cardBranco);
            cardBranco.BringToFront();
            pnlTitulo.SendToBack();
        }

        private void CarregarDados()
        {
            _todosCandidatos = new List<Candidato>
            {
                new Candidato("Maria Silva", "Ferraz", StatusCandidato.Inscrito),
                new Candidato("João Alves", "Itaquera", StatusCandidato.EmAnaliseGF) { NomeOngResponsavel = "ONG Líder" },
                new Candidato("Pedro Santos", "Poá", StatusCandidato.ReprovadoFinalGF),
            };
            AtualizarGrid(_todosCandidatos);
        }

        private void TxtBusca_TextChanged(object sender, EventArgs e)
        {
            string termo = txtBusca.Text.ToLower();
            AtualizarGrid(_todosCandidatos.Where(c => c.NomeCompleto.ToLower().Contains(termo)).ToList());
        }

        private void AtualizarGrid(List<Candidato> lista)
        {
            grid.Rows.Clear();
            foreach (var c in lista) grid.Rows.Add(c.NomeCompleto, c.Cidade, c.Status.ToString(), c.NomeOngResponsavel);
        }
    }
}