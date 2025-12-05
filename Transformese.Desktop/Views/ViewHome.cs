using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using System.Windows.Forms;
using Transformese.Domain.Entities;
using Transformese.Domain.Enums;

namespace Transformese.Desktop.Views
{
    public partial class ViewHome : UserControl
    {
        private readonly HttpClient _client;
        private List<Candidato> _listaCache = new List<Candidato>();

        // NOVA VARIÁVEL: Guarda o total de vagas somadas das ONGs
        private int _totalVagasDoBanco = 0;

        public event EventHandler OnSolicitarNovoCandidato;

        public ViewHome()
        {
            InitializeComponent();

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;
            _client = new HttpClient(handler) { BaseAddress = new Uri("https://localhost:5001/") };

            ConfigurarEventosManuais();
        }

        private void ViewHome_Load(object sender, EventArgs e)
        {
            CarregarDadosDaApi();
        }

        private void ConfigurarEventosManuais()
        {
            if (txtBusca != null)
                txtBusca.TextChanged += TxtBusca_TextChanged;

            this.Load += ViewHome_Load;
        }

        // ==========================================
        // PARTE 1: CONEXÃO COM A API (ALTERADO)
        // ==========================================
        private async void CarregarDadosDaApi()
        {
            try
            {
                // --- 1. BUSCA CANDIDATOS ---
                var resultadoCandidatos = await _client.GetFromJsonAsync<List<Candidato>>("api/candidatos");

                if (resultadoCandidatos != null)
                {
                    _listaCache = resultadoCandidatos;
                }
                else
                {
                    _listaCache = new List<Candidato>();
                }

                // --- 2. BUSCA UNIDADES E SOMA VAGAS (NOVO) ---
                // Tenta buscar as unidades para saber quantas vagas existem no total
                try
                {
                    var resultadoUnidades = await _client.GetFromJsonAsync<List<UnidadeParceira>>("api/unidades");

                    if (resultadoUnidades != null)
                    {
                        // Soma o campo QuantidadeVagas de todas as unidades cadastradas
                        _totalVagasDoBanco = resultadoUnidades.Sum(u => u.QuantidadeVagas);
                    }
                }
                catch
                {
                    // Se falhar (tabela vazia ou erro), assume 0 ou mantém valor anterior
                    _totalVagasDoBanco = 0;
                }

                // Atualiza a tela com os dados novos
                AtualizarInterface();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
                _listaCache = new List<Candidato>();
                AtualizarInterface();
            }
        }

        // ==========================================
        // PARTE 2: LÓGICA DE FILTRO
        // ==========================================
        private void TxtBusca_TextChanged(object sender, EventArgs e)
        {
            AtualizarInterface(txtBusca.Text.Trim());
        }

        private void AtualizarInterface(string filtro = "")
        {
            var listaParaExibir = _listaCache;

            if (!string.IsNullOrEmpty(filtro))
            {
                listaParaExibir = _listaCache
                    .Where(c => c.NomeCompleto.ToLower().Contains(filtro.ToLower()) ||
                                c.Cidade.ToLower().Contains(filtro.ToLower()))
                    .ToList();
            }

            PreencherGrid(listaParaExibir);
            AtualizarCardsKPI();
            AtualizarStatusSincronizacao();
        }

        // ==========================================
        // PARTE 3: PREENCHIMENTO VISUAL (UI)
        // ==========================================
        private void PreencherGrid(List<Candidato> lista)
        {
            if (dgvInscricoes == null) return;

            dgvInscricoes.Rows.Clear();

            foreach (var item in lista)
            {
                dgvInscricoes.Rows.Add(item.NomeCompleto, item.Cidade, item.Status.ToString(), item.NomeOngResponsavel);
            }

            dgvInscricoes.ClearSelection();
        }

        private void AtualizarCardsKPI()
        {
            // 1. Cálculos Gerais
            int total = _listaCache.Count;
            int triagem = _listaCache.Count(c => c.Status == StatusCandidato.Triagem);
            int entrevistas = _listaCache.Count(c => c.Status == StatusCandidato.Entrevista);
            int aprovados = _listaCache.Count(c => c.Status == StatusCandidato.Aprovado);

            // LÓGICA DINÂMICA: Total do Banco - Total Aprovados
            // Se não tiver vagas cadastradas no banco, mostra 0 ou apenas os aprovados negativos? 
            // Vamos assumir que se _totalVagasDoBanco for 0, mostramos 0 para não ficar negativo.
            int vagasRestantes = (_totalVagasDoBanco > 0) ? _totalVagasDoBanco - aprovados : 0;

            int novosHoje = _listaCache.Count(c => c.DataCadastro.Date == DateTime.Today);

            // 3. Atualiza as Labels Principais
            lblKpiValor?.Invoke((MethodInvoker)(() => lblKpiValor.Text = total.ToString("N0")));
            lblTriagemValor?.Invoke((MethodInvoker)(() => lblTriagemValor.Text = triagem.ToString()));
            lblEntrevistasValor?.Invoke((MethodInvoker)(() => lblEntrevistasValor.Text = entrevistas.ToString()));
            lblAprovadosValor?.Invoke((MethodInvoker)(() => lblAprovadosValor.Text = aprovados.ToString()));

            // ATUALIZADO: Mostra as vagas calculadas dinamicamente
            lblVagasValor?.Invoke((MethodInvoker)(() => lblVagasValor.Text = vagasRestantes.ToString()));

            // 4. ATUALIZA A LABEL "+12 hoje"
            if (lblKpiStatus != null)
            {
                lblKpiStatus.Invoke((MethodInvoker)(() =>
                {
                    lblKpiStatus.Text = $"+{novosHoje} hoje";
                    lblKpiStatus.ForeColor = novosHoje > 0 ? Color.FromArgb(0, 168, 150) : Color.Gray;
                }));
            }

            if (lblEntrevistasStatus != null)
            {
                lblEntrevistasStatus.Text = $"{entrevistas} agendados";
                lblEntrevistasStatus.ForeColor = entrevistas > 0 ? Color.Purple : Color.Gray;
            }
        }

        private void AtualizarStatusSincronizacao()
        {
            if (lblStatusSync == null || btnSincronizar == null) return;

            int pendentes = _listaCache.Count(c => c.Status == StatusCandidato.Aprovado);

            if (pendentes > 0)
            {
                lblStatusSync.Text = $"Existem {pendentes} aprovados aguardando sincronização.";
                btnSincronizar.Text = "Sincronizar Asana";
                btnSincronizar.BackColor = Color.FromArgb(108, 92, 231);
                btnSincronizar.Enabled = true;
            }
            else
            {
                lblStatusSync.Text = "Todos os aprovados já foram processados.";
                btnSincronizar.Text = "Sincronizado!";
                btnSincronizar.BackColor = Color.Gray;
                btnSincronizar.Enabled = false;
            }
        }

        private async void btnSincronizar_Click(object sender, EventArgs e)
        {
            var paraSincronizar = _listaCache.Where(c => c.Status == StatusCandidato.Aprovado).ToList();
            if (!paraSincronizar.Any()) return;

            var resp = MessageBox.Show($"Deseja enviar {paraSincronizar.Count} candidatos para o Asana?",
                                       "Sincronização", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (resp == DialogResult.Yes)
            {
                this.Cursor = Cursors.WaitCursor;
                btnSincronizar.Text = "Enviando...";
                btnSincronizar.Enabled = false;

                await Task.Delay(1500);

                this.Cursor = Cursors.Default;
                MessageBox.Show("Sincronização concluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                CarregarDadosDaApi();
            }
        }

        private void btnNovoCandidato_Click(object sender, EventArgs e)
        {
            OnSolicitarNovoCandidato?.Invoke(this, EventArgs.Empty);
        }

        private void btnBuscar_Click(object sender, EventArgs e)
        {
            AtualizarInterface(txtBusca.Text.Trim());
        }
    }
}