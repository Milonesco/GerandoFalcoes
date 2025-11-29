using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;

namespace Transformese.Desktop.Views
{
    public partial class ViewHome : UserControl
    {
        private Guna2FlowLayoutPanel flowPanelKPIs;

        public ViewHome()
        {
            InitializeComponent();
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.Dock = DockStyle.Fill;
            this.BackColor = Color.FromArgb(240, 240, 240);
            this.Padding = new Padding(15);

            // --- FlowLayoutPanel para KPIs (Responsividade) ---
            flowPanelKPIs = new Guna2FlowLayoutPanel();
            flowPanelKPIs.Dock = DockStyle.Top;
            flowPanelKPIs.Height = 150;
            flowPanelKPIs.WrapContents = true;
            flowPanelKPIs.AutoScroll = true;
            flowPanelKPIs.FillColor = Color.Transparent;
            this.Controls.Add(flowPanelKPIs);

            // Adiciona 5 Cards de KPI (vazios por enquanto)
            flowPanelKPIs.Controls.Add(CreateKpiCard("Inscritos Totais", 1500, AppTheme.AzulCiano));
            flowPanelKPIs.Controls.Add(CreateKpiCard("Em Triagem", 450, AppTheme.LaranjaTriagem));
            flowPanelKPIs.Controls.Add(CreateKpiCard("Entrevistas", 120, AppTheme.RosaVibrante));
            flowPanelKPIs.Controls.Add(CreateKpiCard("Aprovados", 85, AppTheme.VerdeAprovacao));
            flowPanelKPIs.Controls.Add(CreateKpiCard("Vagas Restantes", 15, AppTheme.CinzaEscuro));
        }

        // Método utilitário para criar os Cards de KPI
        private Guna2Panel CreateKpiCard(string title, int value, Color color)
        {
            Guna2Panel card = new Guna2Panel();
            card.FillColor = AppTheme.Branco;
            card.Size = new Size(250, 120);
            card.BorderRadius = 10;
            card.Margin = new Padding(10);
            card.ShadowDecoration.Enabled = true;

            Label lblTitle = new Label();
            lblTitle.Text = title;
            lblTitle.Font = new Font("Poppins", 10, FontStyle.Regular);
            lblTitle.Location = new Point(15, 15);
            card.Controls.Add(lblTitle);

            Label lblValue = new Label();
            lblValue.Text = value.ToString();
            lblValue.Font = new Font("Poppins", 24, FontStyle.Bold);
            lblValue.ForeColor = color;
            lblValue.Location = new Point(15, 45);
            lblValue.AutoSize = true;
            card.Controls.Add(lblValue);

            return card;
        }
    }
}