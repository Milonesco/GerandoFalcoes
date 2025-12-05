using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http; // Necessário para conectar na API
using System.Net.Http.Json; // Necessário para ler o JSON da API
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
        public event EventHandler OnSolicitarNovoCandidato;

        public ViewHome()
        {
            InitializeComponent();

            // 1. Configura o HttpClient (Ignorando SSL para localhost)
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;
            _client = new HttpClient(handler) { BaseAddress = new Uri("https://localhost:5001/") };

            ConfigurarEventosManuais();
        }

        // Evento Load: É aqui que carregamos os dados, não no construtor
        private void ViewHome_Load(object sender, EventArgs e)
        {
            CarregarDadosDaApi();
        }

        private void ConfigurarEventosManuais()
        {
            // Vincula o evento de digitação
            if (txtBusca != null)
                txtBusca.TextChanged += TxtBusca_TextChanged;

            // Vincula o Load do UserControl (caso não esteja feito pelo Designer)
            this.Load += ViewHome_Load;
        }

        // ==========================================
        // PARTE 1: CONEXÃO COM A API
        // ==========================================
        private async void CarregarDadosDaApi()
        {
            try
            {
                // UI: Mostra um cursor de espera se quiser
                // this.Cursor = Cursors.WaitCursor;

                // Chama a API: GET api/candidatos (ou api/inscricoes dependendo do seu controller)
                var resultado = await _client.GetFromJsonAsync<List<Candidato>>("api/candidatos");
                // Obs: Ajuste a rota "api/funcionarios/candidatos" para a rota real do seu Controller

                if (resultado != null)
                {
                    _listaCache = resultado;
                }
                else
                {
                    _listaCache = new List<Candidato>();
                }

                // Atualiza a tela com os dados novos
                AtualizarInterface();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Não foi possível buscar os dados da API.\nVerifique se ela está rodando.\n\nErro: " + ex.Message);
                _listaCache = new List<Candidato>(); // Evita tela travada com lista nula
                AtualizarInterface();
            }
            finally
            {
                this.Cursor = Cursors.Default;
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
                // Adiciona linha no Grid. O Status converte Enum pra String automaticamente.
                dgvInscricoes.Rows.Add(item.NomeCompleto, item.Cidade, item.Status.ToString(), "...", "...");
            }

            dgvInscricoes.ClearSelection();
        }

        private void AtualizarCardsKPI()
        {// 1. Cálculos Gerais
            int total = _listaCache.Count;
            int triagem = _listaCache.Count(c => c.Status == StatusCandidato.Triagem);
            int entrevistas = _listaCache.Count(c => c.Status == StatusCandidato.Entrevista);
            int aprovados = _listaCache.Count(c => c.Status == StatusCandidato.Aprovado);
            int vagasRestantes = 340 - aprovados;

            // 2. NOVO CÁLCULO: Filtra quem se cadastrou HOJE
            // DateTime.Today pega a data de hoje à meia-noite (00:00:00)
            // .Date remove o horário do cadastro para comparar apenas o dia
            int novosHoje = _listaCache.Count(c => c.DataCadastro.Date == DateTime.Today);

            // 3. Atualiza as Labels Principais (com proteção de thread e nulo)
            lblKpiValor?.Invoke((MethodInvoker)(() => lblKpiValor.Text = total.ToString("N0")));
            lblTriagemValor?.Invoke((MethodInvoker)(() => lblTriagemValor.Text = triagem.ToString()));
            lblEntrevistasValor?.Invoke((MethodInvoker)(() => lblEntrevistasValor.Text = entrevistas.ToString()));
            lblAprovadosValor?.Invoke((MethodInvoker)(() => lblAprovadosValor.Text = aprovados.ToString()));
            lblVagasValor?.Invoke((MethodInvoker)(() => lblVagasValor.Text = vagasRestantes.ToString()));

            // 4. ATUALIZA A LABEL "+12 hoje" (lblKpiStatus)
            if (lblKpiStatus != null)
            {
                lblKpiStatus.Invoke((MethodInvoker)(() =>
                {
                    // Atualiza o texto: "+5 hoje" ou "+0 hoje"
                    lblKpiStatus.Text = $"+{novosHoje} hoje";

                    // Opcional: Se quiser deixar verde quando tiver gente nova e cinza quando for zero
                    lblKpiStatus.ForeColor = novosHoje > 0 ? Color.FromArgb(0, 168, 150) : Color.Gray;
                }));
            }

            if (lblEntrevistasStatus != null)
            {
                lblEntrevistasStatus.Text = $"{entrevistas} agendados";

                // Lógica de cor: Se tiver entrevista pendente, fica Laranja ou Roxo, senão Cinza
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

                // Botão Roxo e Ativo
                btnSincronizar.Text = "Sincronizar Asana";
                btnSincronizar.BackColor = Color.FromArgb(108, 92, 231);
                btnSincronizar.Enabled = true;
            }
            else
            {
                lblStatusSync.Text = "Todos os aprovados já foram processados.";

                // Botão Cinza e Desativado
                btnSincronizar.Text = "Sincronizado!";
                btnSincronizar.BackColor = Color.Gray;
                btnSincronizar.Enabled = false;
            }
        }

        // ==========================================
        // PARTE 4: AÇÕES (BOTÃO SINCRONIZAR)
        // ==========================================
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

                // Simulação de delay (Futuramente aqui você chamaria: await _client.PostAsJsonAsync("api/sync", paraSincronizar))
                await Task.Delay(1500);

                this.Cursor = Cursors.Default;
                MessageBox.Show("Sincronização concluída com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Recarrega os dados da API para garantir que está tudo atualizado
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