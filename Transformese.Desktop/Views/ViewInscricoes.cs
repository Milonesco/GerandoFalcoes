using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms;
using System.Linq;

namespace Transformese.Desktop.Views
{
    public partial class ViewInscricoes : UserControl
    {
        private Guna2DataGridView dataGridCandidatos;

        public ViewInscricoes()
        {
            InitializeComponent();
            InitializeCustomComponents();
            // TODO: Chamar LoadCandidatos() aqui
        }

        private void InitializeCustomComponents()
        {
            this.Dock = DockStyle.Fill;
            this.Padding = new Padding(20);

            // --- Configuração da Tabela (Guna2DataGridView) ---
            dataGridCandidatos = new Guna2DataGridView();
            dataGridCandidatos.Dock = DockStyle.Fill;

            // Estilos visuais Guna2
            dataGridCandidatos.ThemeStyle.HeaderStyle.BackColor = AppTheme.CinzaEscuro;
            dataGridCandidatos.ThemeStyle.HeaderStyle.ForeColor = AppTheme.Branco;
            dataGridCandidatos.BackgroundColor = AppTheme.Branco;
            dataGridCandidatos.BorderStyle = BorderStyle.None;
            dataGridCandidatos.AllowUserToAddRows = false;

            // Remove a seleção de linha inteira e foca no design Guna
            dataGridCandidatos.CellBorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dataGridCandidatos.RowHeadersVisible = false;

            this.Controls.Add(dataGridCandidatos);
        }
    }
}