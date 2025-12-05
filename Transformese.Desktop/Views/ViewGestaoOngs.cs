using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using Transformese.Domain.Entities;

namespace Transformese.Desktop.Views
{
    public partial class ViewGestaoOngs : UserControl
    {
        private readonly HttpClient _client;
        private List<UnidadeParceira> _listaCache = new List<UnidadeParceira>();

        // Guarda a ONG que está sendo editada no momento (se for null, é cadastro novo)
        private UnidadeParceira _unidadeEmEdicao = null;

        public ViewGestaoOngs()
        {
            InitializeComponent();

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;
            _client = new HttpClient(handler) { BaseAddress = new Uri("https://localhost:5001/") };
        }

        private void ViewGestaoOngs_Load(object sender, EventArgs e)
        {
            CarregarDadosDaApi();
        }

        // =============================================================
        // 1. CENTRALIZADOR DE MENSAGENS (MD)
        // =============================================================
        private void ExibirNotificacao(string msg)
        {
            if (mdNotifica != null)
            {
                if (this.ParentForm != null) mdNotifica.Parent = this.ParentForm;
                mdNotifica.Show(msg);
            }
            else mdNotifica.Show(msg);
        }

        private DialogResult ExibirConfirmacao(string msg)
        {
            // Se tiver um mdConfirmacao, use-o aqui. Senão, usa o padrão.
            return mdExcluir.Show(msg, "Confirmar Ação");
        }

        // =============================================================
        // 2. CARREGAR E PREENCHER GRID
        // =============================================================
        private async void CarregarDadosDaApi()
        {
            try
            {
                var resultado = await _client.GetFromJsonAsync<List<UnidadeParceira>>("api/unidades");

                if (resultado != null)
                {
                    _listaCache = resultado;
                    AtualizarGrid();
                }
            }
            catch (Exception ex)
            {
                ExibirNotificacao("Erro ao carregar dados: " + ex.Message);
            }
        }

        private void AtualizarGrid(string filtro = "")
        {
            if (dgvOngs == null) return;

            dgvOngs.Rows.Clear();

            var listaExibicao = _listaCache;
            if (!string.IsNullOrEmpty(filtro))
            {
                listaExibicao = _listaCache
                    .Where(u => u.NomeUnidade.ToLower().Contains(filtro.ToLower()) ||
                                u.Cidade.ToLower().Contains(filtro.ToLower()))
                    .ToList();
            }

            foreach (var u in listaExibicao)
            {
                // Adiciona linha visual
                int rowIndex = dgvOngs.Rows.Add(
                    u.NomeUnidade,
                    u.NomeResponsavel,
                    u.Email,
                    u.Cidade,
                    u.NomeVaga,
                    u.QuantidadeVagas
                );

                // TRUQUE: Guardamos o objeto INTEIRO na linha (escondido)
                // Isso permite recuperar o ID depois ao clicar
                dgvOngs.Rows[rowIndex].Tag = u;
            }
            dgvOngs.ClearSelection();
        }

        // =============================================================
        // 3. SELEÇÃO NO GRID (JOGA DADOS PARA A DIREITA)
        // =============================================================
        private void PreencherCampos(UnidadeParceira u)
        {
            txtResponsavel.Text = u.NomeUnidade; // (Ajuste se o nome do txt for diferente)
            txtResponsavelOng.Text = u.NomeResponsavel;
            txtEmailOng.Text = u.Email;
            txtCidadeOng.Text = u.Cidade;
            txtNomeVaga.Text = u.NomeVaga;
            if (numVagas != null) numVagas.Value = u.QuantidadeVagas;
        }

        // =============================================================
        // 4. SALVAR (CRIA OU ATUALIZA)
        // =============================================================
        private async void btnSalvarOng_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtResponsavel.Text))
            {
                ExibirNotificacao("O nome da Unidade/ONG é obrigatório.");
                return;
            }

            try
            {
                // Monta o objeto com dados da tela
                var unidadeParaSalvar = new UnidadeParceira
                {
                    // Se estiver editando, mantém o ID. Se for novo, ID é 0.
                    Id = (_unidadeEmEdicao != null) ? _unidadeEmEdicao.Id : 0,
                    NomeUnidade = txtResponsavel.Text,
                    NomeResponsavel = txtResponsavelOng.Text,
                    Email = txtEmailOng.Text,
                    Cidade = txtCidadeOng.Text,
                    NomeVaga = txtNomeVaga.Text,
                    QuantidadeVagas = (int)numVagas.Value,
                    DataCadastro = (_unidadeEmEdicao != null) ? _unidadeEmEdicao.DataCadastro : DateTime.Now
                };

                HttpResponseMessage response;

                if (_unidadeEmEdicao == null)
                {
                    // MODO CRIAÇÃO (POST)
                    response = await _client.PostAsJsonAsync("api/unidades", unidadeParaSalvar);
                }
                else
                {
                    // MODO ATUALIZAÇÃO (PUT)
                    response = await _client.PutAsJsonAsync($"api/unidades/{unidadeParaSalvar.Id}", unidadeParaSalvar);
                }

                if (response.IsSuccessStatusCode)
                {
                    string acao = (_unidadeEmEdicao == null) ? "cadastrada" : "atualizada";
                    ExibirNotificacao($"Unidade {acao} com sucesso!");

                    LimparCampos(); // Reseta para modo criação
                    CarregarDadosDaApi(); // Atualiza o grid
                }
                else
                {
                    ExibirNotificacao("Erro na API ao salvar.");
                }
            }
            catch (Exception ex)
            {
                ExibirNotificacao("Erro: " + ex.Message);
            }
        }

        // =============================================================
        // 5. EXCLUIR
        // =============================================================
        private async void btnExcluir_Click(object sender, EventArgs e)
        {
            // Verifica se tem alguém selecionado
            if (_unidadeEmEdicao == null)
            {
                ExibirNotificacao("Selecione uma ONG na tabela para excluir.");
                return;
            }

            var resp = ExibirConfirmacao($"Tem certeza que deseja excluir a unidade '{_unidadeEmEdicao.NomeUnidade}'?\nEssa ação não pode ser desfeita.");

            if (resp == DialogResult.Yes)
            {
                try
                {
                    var response = await _client.DeleteAsync($"api/unidades/{_unidadeEmEdicao.Id}");

                    if (response.IsSuccessStatusCode)
                    {
                        ExibirNotificacao("Unidade excluída com sucesso!");
                        LimparCampos();
                        CarregarDadosDaApi();
                    }
                    else
                    {
                        ExibirNotificacao("Erro ao excluir na API.");
                    }
                }
                catch (Exception ex)
                {
                    ExibirNotificacao("Erro: " + ex.Message);
                }
            }
        }

        // =============================================================
        // 6. LIMPAR / CANCELAR EDIÇÃO
        // =============================================================
        private void LimparCampos()
        {
            _unidadeEmEdicao = null; // Reseta variavel de controle (Volta para modo Criar)

            txtResponsavel.Clear();
            txtResponsavelOng.Clear();
            txtEmailOng.Clear();
            txtCidadeOng.Clear();
            txtNomeVaga.Clear();
            if (numVagas != null) numVagas.Value = 0;

            txtResponsavel.Focus();
            dgvOngs.ClearSelection();
        }

        // Outros
        private void txtBuscarOng_TextChanged(object sender, EventArgs e)
        {
            AtualizarGrid(txtBuscarOng.Text);
        }

        private void btnNovaOng_Click(object sender, EventArgs e)
        {
            LimparCampos();
        }

        private void dgvOngs_CellClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            // Recupera o objeto guardado na Tag
            var linha = dgvOngs.Rows[e.RowIndex];
            if (linha.Tag is UnidadeParceira unidadeSelecionada)
            {
                _unidadeEmEdicao = unidadeSelecionada; // Entra em modo de Edição
                PreencherCampos(_unidadeEmEdicao);
            }
        }

        private async void RegistrarLog(string acao, string detalhes)
        {
            try
            {
                string nomeUsuario = "Sistema";
                if (SessaoSistema.UsuarioLogado != null)
                    nomeUsuario = SessaoSistema.UsuarioLogado.Nome;

                var log = new LogSistema
                {
                    Usuario = nomeUsuario,
                    Acao = acao,
                    Detalhes = detalhes,
                    DataHora = DateTime.Now
                };
                // Fire-and-forget (não espera resposta pra não travar tela)
                await _client.PostAsJsonAsync("api/logs", log);
            }
            catch { /* Falha silenciosa no log */ }
        }
    }
}