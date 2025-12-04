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

namespace Transformese.Desktop.Views
{
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

        // --- 1. CONEXÃO COM O BANCO DE DADOS ---
        private void CarregarDadosDoBanco()
        {
            try
            {
                // Instancia seu contexto (conexão)
                // Troque 'SeuAppDbContext' pelo nome da sua classe de contexto
                using (var context = new SeuAppDbContext())
                {
                    // Puxa todos os candidatos para a memória de uma vez
                    // Se sua tabela chama 'Inscricoes', use context.Inscricoes.ToList()
                    _listaCache = context.Candidatos.ToList();
                }

                // Agora atualiza a tela com esses dados
                AtualizarInterface();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao conectar no banco: " + ex.Message);
                _listaCache = new List<Candidato>(); // Evita erro de nulo se falhar
            }
        }

        // --- 2. ATUALIZAÇÃO DA TELA (KPIs e Tabela) ---
        private void AtualizarInterface(string filtro = "")
        {
            if (_listaCache == null) return;

            // Filtra na memória (muito rápido)
            var listaExibicao = _listaCache;

            if (!string.IsNullOrEmpty(filtro))
            {
                listaExibicao = _listaCache
                    .Where(c => c.Nome.ToLower().Contains(filtro.ToLower()) ||
                                c.Cidade.ToLower().Contains(filtro.ToLower()))
                    .ToList();
            }

            PreencherGrid(listaExibicao);
            AtualizarCardsKPI();         // Sempre usa os totais, independente do filtro
            AtualizarStatusSincronizacao();
        }

        private void PreencherGrid(List<Candidato> lista)
        {
            if (dgvInscricoes == null) return;

            dgvInscricoes.Rows.Clear();

            foreach (var item in lista)
            {
                // Ajuste os nomes das propriedades conforme sua classe real
                dgvInscricoes.Rows.Add(item.Nome, item.Cidade, item.Status, item.OngResponsavel);
            }

            dgvInscricoes.ClearSelection();

            // Garante que o botão de ação tenha texto "..." ou "Ver"
            foreach (DataGridViewRow row in dgvInscricoes.Rows)
            {
                // Se o botão for a 5ª coluna (indice 4)
                if (row.Cells.Count > 4 && row.Cells[4] is DataGridViewButtonCell)
                {
                    row.Cells[4].Value = "...";
                }
            }
        }

        private void AtualizarCardsKPI()
        {
            // Usa _listaCache para contar o total real do banco
            int total = _listaCache.Count;
            int triagem = _listaCache.Count(c => c.Status == "Triagem");
            int entrevistas = _listaCache.Count(c => c.Status == "Entrevista");
            int aprovados = _listaCache.Count(c => c.Status == "Aprovado");
            int vagas = 400; // Fixo ou vindo de config

            if (lblKpiValor != null) lblKpiValor.Text = total.ToString("N0");
            if (lblTriagemValor != null) lblTriagemValor.Text = triagem.ToString();
            if (lblEntrevistasValor != null) lblEntrevistasValor.Text = entrevistas.ToString();
            if (lblAprovadosValor != null) lblAprovadosValor.Text = aprovados.ToString();
            if (lblVagasValor != null) lblVagasValor.Text = (vagas - aprovados).ToString();
        }

        private void AtualizarStatusSincronizacao()
        {
            // Conta quantos estão aprovados mas ainda não foram enviados (supondo campo EnviadoAsana)
            // Se não tiver o campo boolean, baseie-se apenas no status
            int pendentes = _listaCache.Count(c => c.Status == "Aprovado" && c.EnviadoAsana == false);

            if (lblStatusSync != null)
            {
                lblStatusSync.Text = pendentes > 0
                    ? $"Existem {pendentes} aprovados aguardando sincronização."
                    : "Tudo atualizado.";
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

        // --- 3. EVENTOS ---

        private void TxtBusca_TextChanged(object sender, EventArgs e)
        {
            AtualizarInterface(txtBusca.Text.Trim());
        }

        private async void btnSincronizar_Click(object sender, EventArgs e)
        {
            // Filtra quem precisa ser enviado
            var paraSincronizar = _listaCache.Where(c => c.Status == "Aprovado" && c.EnviadoAsana == false).ToList();

            if (paraSincronizar.Count == 0) return;

            var resp = MessageBox.Show($"Enviar {paraSincronizar.Count} candidatos para o Asana?", "Sync", MessageBoxButtons.YesNo);

            if (resp == DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                btnSincronizar.Text = "Enviando...";

                // Simulação de delay da API
                await Task.Delay(1500);

                // --- SALVAR NO BANCO DE DADOS ---
                using (var context = new SeuAppDbContext())
                {
                    foreach (var itemLocal in paraSincronizar)
                    {
                        // 1. Atualiza na memória local
                        itemLocal.EnviadoAsana = true;

                        // 2. Busca o objeto real no banco para atualizar
                        var itemBanco = context.Candidatos.Find(itemLocal.Id);
                        if (itemBanco != null)
                        {
                            itemBanco.EnviadoAsana = true;
                            // Opcional: itemBanco.Status = "Integrado";
                        }
                    }
                    // Salva todas as alterações no SQL
                    context.SaveChanges();
                }

                this.Cursor = Cursors.Default;

                // Atualiza a tela (o botão vai ficar cinza pois agora EnviadoAsana é true)
                AtualizarInterface();
                MessageBox.Show("Sincronização concluída!");
            }
        }
    }
}
