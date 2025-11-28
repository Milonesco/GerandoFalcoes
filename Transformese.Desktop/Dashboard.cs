using System;
using System.Windows.Forms;

namespace Transformese.Desktop.Views
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
            CarregarDadosTeste();
            ConectarEventos();
        }

        private void CarregarDadosTeste()
        {
            gridInscricoes.Rows.Clear();
            gridInscricoes.Rows.Add("Maria Silva", "Ferraz de Vasconcelos", "Novo", "--");
            gridInscricoes.Rows.Add("João Alves", "São Miguel", "Triagem", "ONG Líder");
            gridInscricoes.Rows.Add("Ana Souza", "Itaquera", "Aprovado", "ONG Aceleração");
        }

        private void ConectarEventos()
        {
            btnNovoCandidatoBig.Click += BtnNovoCandidatoBig_Click;
            btnSync.Click += BtnSync_Click;
            btnSair.Click += BtnSair_Click;
        }

        private void BtnNovoCandidatoBig_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Botão 'Novo Candidato' clicado!");
        }

        private void BtnSync_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Botão 'Sincronizar Agora' clicado!");
        }

        private void BtnSair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
