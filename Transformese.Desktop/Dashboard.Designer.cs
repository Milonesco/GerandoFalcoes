using System;
using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Transformese.Desktop.Views
{
    partial class Dashboard
    {
        private System.ComponentModel.IContainer components = null;

        // SIDEBAR
        private Guna2Panel pnlSidebar;
        private Label lblBrandTitle;
        private Label lblBrandSub;
        private Guna2Button btnDashboard;
        private Guna2Button btnInscricoes;
        private Guna2Button btnTriagem;
        private Guna2Button btnEntrevistas;
        private Guna2Button btnSelecao;
        private Guna2Button btnNovoCandidatoMenu;
        private Guna2Button btnGestaoOngs;
        private Guna2Button btnRelatorios;
        private Guna2Button btnSair;

        // HEADER
        private Panel headerPanel;
        private Label lblHeaderTitle;
        private Label lblHeaderSub;

        // MAIN WRAPPER
        private Panel mainWrapper;
        private FlowLayoutPanel flowKPI;
        private Guna2Panel pnlTable;
        private Guna2DataGridView gridInscricoes;
        private Guna2Panel pnlSearch;
        private Guna2Button btnNovoCandidatoBig;
        private Guna2Panel pnlFluxo;
        private Guna2Panel pnlAlertAsana;
        private Guna2Button btnSync;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();

            Color clrCiano = ColorTranslator.FromHtml("#00A89D");
            Color clrRosa = ColorTranslator.FromHtml("#EC2262");
            Color clrLaranja = ColorTranslator.FromHtml("#E96B2A");
            Color clrVerde = ColorTranslator.FromHtml("#16A34A");
            Color clrCinzaEscuro = ColorTranslator.FromHtml("#161616");
            Color clrFundo = ColorTranslator.FromHtml("#F3F4F6");
            Color clrTexto = ColorTranslator.FromHtml("#333333");

            this.SuspendLayout();
            this.Name = "DashboardForm";
            this.Text = "Gerando Falcões - Gestão";
            this.BackColor = clrFundo;
            this.ClientSize = new Size(1365, 820);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.Font = new Font("Poppins", 9F, FontStyle.Regular, GraphicsUnit.Point);

            // ====================
            // SIDEBAR
            // ====================
            pnlSidebar = new Guna2Panel
            {
                Name = "pnlSidebar",
                Size = new Size(280, this.ClientSize.Height),
                Location = new Point(0, 0),
                FillColor = clrCinzaEscuro,
                BorderRadius = 0,
                ShadowDecoration = { Enabled = true, Shadow = new Padding(6) },
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left,
                Padding = new Padding(18)
            };
            this.Controls.Add(pnlSidebar);

            lblBrandTitle = new Label
            {
                Text = "GERANDO FALCÕES",
                ForeColor = Color.White,
                Font = new Font("Protest Revolution", 18F, FontStyle.Regular, GraphicsUnit.Point),
                AutoSize = true,
                Location = new Point(20, 18)
            };
            pnlSidebar.Controls.Add(lblBrandTitle);

            lblBrandSub = new Label
            {
                Text = "GESTÃO TRANSFORME-SE",
                ForeColor = Color.FromArgb(190, 190, 190),
                Font = new Font("Poppins", 9F, FontStyle.Regular),
                AutoSize = true,
                Location = new Point(22, 48)
            };
            pnlSidebar.Controls.Add(lblBrandSub);

            int btnTop = 100, btnHeight = 46, btnWidth = 240, btnSpacing = 10;
            Func<string, Guna2Button> makeSideBtn = (text) =>
            {
                var b = new Guna2Button
                {
                    Size = new Size(btnWidth, btnHeight),
                    Location = new Point(18, btnTop),
                    Text = text,
                    Font = new Font("Poppins", 10F, FontStyle.Bold),
                    BorderRadius = 8,
                    FillColor = Color.FromArgb(23, 23, 28),
                    ForeColor = Color.FromArgb(210, 210, 210),
                    ShadowDecoration = { Enabled = false },
                    Animated = true,
                    Anchor = AnchorStyles.Top | AnchorStyles.Left
                };
                btnTop += btnHeight + btnSpacing;
                return b;
            };

            btnDashboard = makeSideBtn("Dashboard");
            pnlSidebar.Controls.Add(btnDashboard);
            btnInscricoes = makeSideBtn("Inscrições Recebidas");
            pnlSidebar.Controls.Add(btnInscricoes);
            btnTriagem = makeSideBtn("Triagem Inicial");
            pnlSidebar.Controls.Add(btnTriagem);
            btnEntrevistas = makeSideBtn("Entrevistas (ONGs)");
            pnlSidebar.Controls.Add(btnEntrevistas);
            btnSelecao = makeSideBtn("Seleção Final");
            pnlSidebar.Controls.Add(btnSelecao);
            btnNovoCandidatoMenu = makeSideBtn("Novo Candidato");
            pnlSidebar.Controls.Add(btnNovoCandidatoMenu);
            btnGestaoOngs = makeSideBtn("Gestão de ONGs");
            pnlSidebar.Controls.Add(btnGestaoOngs);
            btnRelatorios = makeSideBtn("Relatórios (Asana)");
            pnlSidebar.Controls.Add(btnRelatorios);

            btnSair = new Guna2Button
            {
                Text = "Sair do Sistema",
                Size = new Size(btnWidth, 40),
                Location = new Point(18, this.ClientSize.Height - 65),
                BorderRadius = 8,
                FillColor = Color.White,
                ForeColor = Color.FromArgb(196, 58, 58),
                Font = new Font("Poppins", 9F, FontStyle.Regular),
                Anchor = AnchorStyles.Bottom | AnchorStyles.Left
            };
            pnlSidebar.Controls.Add(btnSair);

            // ====================
            // HEADER
            // ====================
            headerPanel = new Panel
            {
                Name = "headerPanel",
                Size = new Size(this.ClientSize.Width - pnlSidebar.Width, 80),
                Location = new Point(pnlSidebar.Width, 0),
                BackColor = Color.White,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                Padding = new Padding(24, 16, 24, 16),
                BorderStyle = BorderStyle.None
            };
            this.Controls.Add(headerPanel);

            lblHeaderTitle = new Label
            {
                Text = "Dashboard",
                Font = new Font("Poppins", 20F, FontStyle.Bold),
                ForeColor = clrTexto,
                AutoSize = true,
                Location = new Point(12, 18)
            };
            headerPanel.Controls.Add(lblHeaderTitle);

            lblHeaderSub = new Label
            {
                Text = "Bem-vinda, " + Environment.UserName + "! Resumo de hoje.",
                Font = new Font("Poppins", 9F, FontStyle.Regular),
                ForeColor = Color.Gray,
                AutoSize = true,
                Location = new Point(16, 48)
            };
            headerPanel.Controls.Add(lblHeaderSub);

            // ====================
            // MAIN WRAPPER
            // ====================
            mainWrapper = new Panel
            {
                Location = new Point(pnlSidebar.Width + 18, headerPanel.Bottom + 6),
                Size = new Size(this.ClientSize.Width - pnlSidebar.Width - 36, this.ClientSize.Height - headerPanel.Height - 36),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right,
                AutoScroll = true,
                BackColor = clrFundo
            };
            this.Controls.Add(mainWrapper);

            // ====================
            // FlowKPI
            // ====================
            flowKPI = new FlowLayoutPanel
            {
                Name = "flowKPI",
                Location = new Point(8, 8),
                Size = new Size(mainWrapper.Width - 380, 140),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                AutoScroll = false,
                WrapContents = false,
                FlowDirection = FlowDirection.LeftToRight,
                Padding = new Padding(6)
            };
            mainWrapper.Controls.Add(flowKPI);

            // Placeholder KPI cards
            for (int i = 0; i < 5; i++)
            {
                var k = new Guna2Panel
                {
                    Size = new Size(240, 120),
                    FillColor = Color.White,
                    BorderRadius = 12,
                    ShadowDecoration = { Enabled = true, Color = Color.FromArgb(200, 200, 200) },
                    Margin = new Padding(6)
                };

                Label t = new Label { Text = i == 0 ? "TOTAL INSCRITOS" : (i == 1 ? "EM TRIAGEM" : (i == 2 ? "ENTREVISTAS" : (i == 3 ? "APROVADOS" : "VAGAS RESTANTES"))), Font = new Font("Poppins", 8F, FontStyle.Bold), ForeColor = Color.Gray, Location = new Point(12, 10), AutoSize = true };
                Label v = new Label { Text = i == 0 ? "1.250" : (i == 1 ? "150" : (i == 2 ? "80" : (i == 3 ? "60" : "340"))), Font = new Font("Poppins", 22F, FontStyle.Bold), ForeColor = clrTexto, Location = new Point(12, 34), AutoSize = true };
                Label s = new Label { Text = i == 0 ? "+12 hoje" : (i == 1 ? "Aguardando análise" : (i == 2 ? "Nesta semana" : (i == 3 ? "Prontos p/ Asana" : "Ciclo atual"))), Font = new Font("Poppins", 8F, FontStyle.Regular), ForeColor = Color.Gray, Location = new Point(12, 78), AutoSize = true };

                k.Controls.Add(t); k.Controls.Add(v); k.Controls.Add(s);

                Panel stripe = new Panel { Size = new Size(6, 80), Location = new Point(0, 20) };
                switch (i)
                {
                    case 0: stripe.BackColor = clrCiano; break;
                    case 1: stripe.BackColor = clrLaranja; break;
                    case 2: stripe.BackColor = clrRosa; break;
                    case 3: stripe.BackColor = clrVerde; break;
                    default: stripe.BackColor = Color.Gray; break;
                }
                k.Controls.Add(stripe);

                flowKPI.Controls.Add(k);
            }

            // ====================
            // PNL TABLE + GRID
            // ====================
            pnlTable = new Guna2Panel
            {
                Name = "pnlTable",
                Size = new Size(mainWrapper.Width - 420, 420),
                Location = new Point(8, flowKPI.Bottom + 14),
                FillColor = Color.White,
                BorderRadius = 12,
                ShadowDecoration = { Enabled = true, Color = Color.FromArgb(180, 180, 180) },
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };
            mainWrapper.Controls.Add(pnlTable);

            Label lblTableTitle = new Label { Text = "Últimas Inscrições", Font = new Font("Poppins", 16F, FontStyle.Bold), ForeColor = clrTexto, Location = new Point(18, 16), AutoSize = true };
            pnlTable.Controls.Add(lblTableTitle);

            gridInscricoes = new Guna2DataGridView
            {
                Name = "gridInscricoes",
                Location = new Point(12, 56),
                Size = new Size(pnlTable.Width - 28, pnlTable.Height - 74),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right | AnchorStyles.Bottom,
                Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Default,
                BackgroundColor = Color.White,
                BorderStyle = BorderStyle.None,
                AllowUserToAddRows = false,
                RowHeadersVisible = false,
                ColumnHeadersHeight = 42,
                RowTemplate = { Height = 45 },
                SelectionMode = DataGridViewSelectionMode.FullRowSelect,
                EnableHeadersVisualStyles = false,
                ColumnHeadersDefaultCellStyle = { BackColor = Color.FromArgb(235, 243, 255), ForeColor = Color.FromArgb(0, 74, 173) },
                DefaultCellStyle = { Font = new Font("Poppins", 10F), ForeColor = clrTexto }
            };

            gridInscricoes.Columns.Clear();
            gridInscricoes.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Nome", HeaderText = "CANDIDATO", AutoSizeMode = DataGridViewAutoSizeColumnMode.Fill });
            gridInscricoes.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Cidade", HeaderText = "CIDADE", Width = 220 });
            gridInscricoes.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Status", HeaderText = "STATUS", Width = 140 });
            gridInscricoes.Columns.Add(new DataGridViewTextBoxColumn() { Name = "Ong", HeaderText = "ONG RESP.", Width = 180 });
            var btnCol = new DataGridViewButtonColumn() { HeaderText = "AÇÃO", Text = "VER", UseColumnTextForButtonValue = true, Width = 90 };
            gridInscricoes.Columns.Add(btnCol);

            pnlTable.Controls.Add(gridInscricoes);

            // ====================
            // ALERT ASANA
            // ====================
            pnlAlertAsana = new Guna2Panel
            {
                Name = "pnlAlertAsana",
                Size = new Size(pnlTable.Width, 80),
                Location = new Point(8, pnlTable.Bottom + 12),
                FillColor = Color.FromArgb(240, 249, 255),
                BorderRadius = 10,
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                ShadowDecoration = { Enabled = false }
            };
            mainWrapper.Controls.Add(pnlAlertAsana);

            Label lblAlertTitle = new Label { Text = "Sincronização Pendente", Font = new Font("Poppins", 10F, FontStyle.Bold), ForeColor = Color.FromArgb(10, 67, 133), Location = new Point(12, 12), AutoSize = true };
            Label lblAlertSub = new Label { Text = "Existem 8 candidatos aprovados aguardando envio para o Asana.", Font = new Font("Poppins", 9F), ForeColor = Color.FromArgb(30, 98, 160), Location = new Point(12, 36), AutoSize = true };
            pnlAlertAsana.Controls.Add(lblAlertTitle);
            pnlAlertAsana.Controls.Add(lblAlertSub);

            btnSync = new Guna2Button
            {
                Text = "Sincronizar Agora",
                Size = new Size(160, 36),
                Location = new Point(pnlAlertAsana.Width - 176, 20),
                Anchor = AnchorStyles.Top | AnchorStyles.Right,
                BorderRadius = 8,
                FillColor = Color.FromArgb(0, 120, 210),
                ForeColor = Color.White,
                Font = new Font("Poppins", 9F, FontStyle.Bold)
            };
            pnlAlertAsana.Controls.Add(btnSync);

            this.ResumeLayout(false);
            this.PerformLayout();
        }
        #endregion
    }
}
