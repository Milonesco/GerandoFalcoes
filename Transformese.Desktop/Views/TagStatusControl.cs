using System.Drawing;
using System.Windows.Forms;
using Guna.UI2.WinForms; // Usando Guna2
using Transformese.Domain.Enums;

namespace Transformese.Desktop.Views
{
    public partial class TagStatusControl : UserControl
    {
        // ATENÇÃO: Se o erro persistir, você DEVE declarar o lblStatus aqui manualmente,
        // mas o ideal é que ele venha do designer.
        // private Guna2HtmlLabel lblStatus; 

        public TagStatusControl()
        {
            InitializeComponent();

            this.Size = new Size(120, 25);
            this.Font = new Font("Poppins", 8, FontStyle.Bold);

            // Assume que lblStatus é um Label ou Guna2HtmlLabel nomeado no designer
            // Se você usou um Label padrão, o código ainda funcionará.
            if (lblStatus != null)
            {
                lblStatus.Dock = DockStyle.Fill;
                lblStatus.TextAlign = ContentAlignment.MiddleCenter;
                lblStatus.ForeColor = AppTheme.Branco;
            }
        }

        public void SetStatus(StatusCandidato status)
        {
            if (lblStatus == null) return;

            switch (status)
            {
                case StatusCandidato.Pendente:
                    lblStatus.Text = "PENDENTE";
                    this.BackColor = AppTheme.CinzaEscuro;
                    break;
                case StatusCandidato.EmAnalise:
                    lblStatus.Text = "EM TRIAGEM";
                    this.BackColor = AppTheme.LaranjaTriagem;
                    break;
                case StatusCandidato.Aprovado:
                    lblStatus.Text = "APROVADO";
                    this.BackColor = AppTheme.VerdeAprovacao;
                    break;
                case StatusCandidato.Rejeitado:
                    lblStatus.Text = "REJEITADO";
                    this.BackColor = AppTheme.RosaVibrante;
                    break;
                default:
                    lblStatus.Text = "DESCONHECIDO";
                    this.BackColor = Color.Gray;
                    break;
            }
        }
    }
}