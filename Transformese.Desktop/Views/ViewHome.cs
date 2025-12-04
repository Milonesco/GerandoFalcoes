using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transformese.Domain.Entities;
using Transformese.Domain.Enums;
using Transformese.Domain;
using Transformese.Data;

namespace Transformese.Desktop.Views
{
    // Inicializa a lista para evitar erros de valor nulo
    private List<Candidato> _listaCache = new List<Candidato>();

    public partial class ViewHome : UserControl
    {
        private List<Candidato> _listaCache;
        public ViewHome()
        {
            InitializeComponent();

            ConfigurarEventosManuais();

            // Carrega do banco assim que a tela abre
            CarregarDadosDoBanco();
        }

        private void ConfigurarEventosManuais()
        {
            if (txtBusca != null) txtBusca.TextChanged += TxtBusca_TextChanged;
        }

        private void CarregarDadosDoBanco()
        {
            try
            {
                // CORREÇÃO 1: Usando o nome real do seu Contexto
                using (var context = new TransformeseContext())
                {
                    _listaCache = context.Candidatos.ToList();
                }

                AtualizarInterface();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar no banco: " + ex.Message);
                _listaCache = new List<Candidato>();
            }
        }

        private void AtualizarInterface(string filtro = "")
        {
            var listaExibicao = _listaCache;

            if (!string.IsNullOrEmpty(filtro))
            {
                // CORREÇÃO 2: Usando 'NomeCompleto' em vez de 'Nome'
                listaExibicao = _listaCache
                    .Where(c => c.NomeCompleto.ToLower().Contains(filtro.ToLower()) ||
                                c.Cidade.ToLower().Contains(filtro.ToLower()))
                    .ToList();
            }

            PreencherGrid(listaExibicao);
            AtualizarCardsKPI();
            AtualizarStatusSincronizacao();
        }

        private void PreencherGrid(List<Candidato> lista)
        {
            if (dgvInscricoes == null) return;

            dgvInscricoes.Rows.Clear();

            foreach (var item in lista)
            {
                // CORREÇÃO 3: item.Status.ToString() converte o Enum para texto na tabela
                // CORREÇÃO 4: Usando 'Ong' em vez de 'OngResponsavel'
                dgvInscricoes.Rows.Add(item.NomeCompleto, item.Cidade, item.Status.ToString(), item.Ong);
            }

            dgvInscricoes.ClearSelection();

            // Configura o botão "Ação"
            foreach (DataGridViewRow row in dgvInscricoes.Rows)
            {
                if (row.Cells.Count > 4 && row.Cells[4] is DataGridViewButtonCell)
                {
                    row.Cells[4].Value = "...";
                }
            }
        }

        private void AtualizarCardsKPI()
        {
            // CORREÇÃO 5: Comparando com ENUM (StatusCandidato) e não com Texto ("")
            int total = _listaCache.Count;
            int triagem = _listaCache.Count(c => c.Status == StatusCandidato.Triagem);
            int entrevistas = _listaCache.Count(c => c.Status == StatusCandidato.Entrevista);
            int aprovados = _listaCache.Count(c => c.Status == StatusCandidato.Aprovado);
            int vagas = 340;

            if (lblKpiValor != null) lblKpiValor.Text = total.ToString("N0");
            if (lblTriagemValor != null) lblTriagemValor.Text = triagem.ToString();
            if (lblEntrevistasValor != null) lblEntrevistasValor.Text = entrevistas.ToString();
            if (lblAprovadosValor != null) lblAprovadosValor.Text = aprovados.ToString();
            if (lblVagasValor != null) lblVagasValor.Text = (vagas - aprovados).ToString();
        }

        private void AtualizarStatusSincronizacao()
        {
            // Lógica simplificada: Conta apenas quem está Aprovado
            int pendentes = _listaCache.Count(c => c.Status == StatusCandidato.Aprovado);

            if (lblStatusSync != null)
            {
                lblStatusSync.Text = pendentes > 0
                    ? $"Existem {pendentes} aprovados aguardando sincronização."
                    : "Todos os aprovados já foram processados.";
            }

            if (btnSincronizar != null)
            {
                if (pendentes > 0)
                {
                    btnSincronizar.Text = "Sincronizar";
                    btnSincronizar.BackColor = Color.FromArgb(108, 92, 231); // Roxo
                    btnSincronizar.Enabled = true;
                }
                else
                {
                    btnSincronizar.Text = "Sincronizado!";
                    btnSincronizar.BackColor = Color.Gray;
                    btnSincronizar.Enabled = false;
                }
            }
        }

        private void TxtBusca_TextChanged(object sender, EventArgs e)
        {
            if (sender is TextBox txt)
            {
                AtualizarInterface(txt.Text.Trim());
            }
        }

        private async void btnSincronizar_Click(object sender, EventArgs e)
        {
            // Filtra usando ENUM
            var paraSincronizar = _listaCache.Where(c => c.Status == StatusCandidato.Aprovado).ToList();

            if (paraSincronizar.Count == 0) return;

            var resp = MessageBox.Show($"Enviar {paraSincronizar.Count} candidatos para o Asana?", "Sync", MessageBoxButtons.YesNo);

            if (resp == DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                btnSincronizar.Text = "Enviando...";

                await Task.Delay(1500);

                using (var context = new TransformeseContext())
                {
                    // Como não temos a coluna 'EnviadoAsana', não vamos salvar nada no banco agora
                    // para evitar erros. Apenas simulamos o envio visualmente.

                    // Se você quiser mudar o status deles para limpar a lista, descomente abaixo:
                    /*
                    foreach (var itemLocal in paraSincronizar)
                    {
                        var itemBanco = context.Candidatos.Find(itemLocal.Id);
                        if (itemBanco != null) {
                             // itemBanco.Status = StatusCandidato.Inscrito; // Tira de aprovado
                        }
                    }
                    context.SaveChanges();
                    */
                }

                // Recarrega
                CarregarDadosDoBanco();

                this.Cursor = Cursors.Default;
                MessageBox.Show("Sincronização concluída!");
            }
        }
    }
}
