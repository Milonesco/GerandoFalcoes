using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using Transformese.Domain.Entities;
using Transformese.Domain.Enums;

namespace Transformese.Desktop.Views
{
    public partial class ViewTriagem : UserControl
    {
        public Action AoFechar { get; set; }

        private readonly HttpClient _client;
        private Candidato _candidatoAtual;

        public ViewTriagem()
        {
            InitializeComponent();

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;
            _client = new HttpClient(handler) { BaseAddress = new Uri("https://localhost:5001/") };

            CarregarCombos();
        }

        // =============================================================
        // MÉTODOS AUXILIARES PARA CENTRALIZAR OS DIÁLOGOS
        // =============================================================

        private void ExibirNotificacao(string mensagem)
        {
            if (mdNotifica != null)
            {
                if (this.ParentForm != null) mdNotifica.Parent = this.ParentForm;
                mdNotifica.Show(mensagem);
            }
            else MessageBox.Show(mensagem);
        }

        private DialogResult ExibirProximoFila(string mensagem)
        {
            if (mdProximoFila != null)
            {
                if (this.ParentForm != null) mdProximoFila.Parent = this.ParentForm;
                return mdProximoFila.Show(mensagem);
            }
            return MessageBox.Show(mensagem, "Próximo", MessageBoxButtons.YesNo);
        }

        // =============================================================
        // CÓDIGO DA TELA
        // =============================================================

        private async void CarregarCombos()
        {
            if (cboONG != null) cboONG.Items.Clear();
            if (cboVaga != null) cboVaga.Items.Clear();

            if (cboStatus != null) cboStatus.DataSource = Enum.GetValues(typeof(StatusCandidato));

            try
            {
                var listaUnidades = await _client.GetFromJsonAsync<List<UnidadeParceira>>("api/unidades");

                if (listaUnidades != null && listaUnidades.Any())
                {
                    var listaOngs = listaUnidades
                                    .Select(u => u.NomeUnidade)
                                    .Where(n => !string.IsNullOrEmpty(n))
                                    .Distinct().ToArray();

                    if (cboONG != null) cboONG.Items.AddRange(listaOngs);

                    var listaVagas = listaUnidades
                                     .Select(u => u.NomeVaga)
                                     .Where(n => !string.IsNullOrEmpty(n))
                                     .Distinct().ToArray();

                    if (cboVaga != null) cboVaga.Items.AddRange(listaVagas);
                }
            }
            catch (Exception ex)
            {
                if (cboONG != null) cboONG.Items.Add("Outra");
                if (cboVaga != null) cboVaga.Items.Add("Geral");
            }
        }

        public void CarregarDadosDoCandidato(Candidato c)
        {
            _candidatoAtual = c;

            txtNomeCandidato.Text = c.NomeCompleto;
            txtCPF.Text = c.CPF;
            txtDataNasc.Text = c.DataNascimento.ToString("dd/MM/yyyy");

            txtGenero.Text = c.IdentidadeGenero;
            txtOrientacaoSexual.Text = c.OrientacaoSexual;
            txtEtnia.Text = c.RacaCor;
            txtDeficiencia.Text = c.Deficiencia;

            txtEmail.Text = c.Email;
            txtLinkedin.Text = c.PerfilLinkedin;
            txtCelular.Text = c.Telefone;
            txtCep.Text = c.CEP;
            txtCidade.Text = c.Cidade;
            txtUF.Text = c.Estado;

            txtEscolaridade.Text = c.Escolaridade;
            txtTrabalho.Text = BoolParaTexto(c.TrabalhaAtualmente);
            txtRendaFamiliar.Text = c.RendaFamiliar.ToString("C2");
            txtPessoasDomicilio.Text = c.PessoasNaCasa.ToString();
            txtTemComputador.Text = BoolParaTexto(c.PossuiComputador);
            txtTemInternet.Text = BoolParaTexto(c.PossuiInternet);

            txtCursoInteresse.Text = c.CursoInteresse;
            txtTurno.Text = c.TurnoPreferido;
            txtIndicacao.Text = c.NomeIndicacao;
            txtEstudouOng.Text = BoolParaTexto(c.JaEstudouNaGF);

            txtAnotacoesRH.Clear();
            if (!string.IsNullOrEmpty(c.ObservacoesONG))
                txtAnotacoesRH.Text = c.ObservacoesONG;

            if (numPontuacao != null)
                numPontuacao.Value = c.Pontuacao.HasValue ? c.Pontuacao.Value : 0;

            if (cboStatus != null) cboStatus.SelectedItem = c.Status;
            if (cboONG != null && !string.IsNullOrEmpty(c.NomeOngResponsavel)) cboONG.Text = c.NomeOngResponsavel;
            if (cboVaga != null && !string.IsNullOrEmpty(c.VagaEncaminhada)) cboVaga.Text = c.VagaEncaminhada;
        }

        private string BoolParaTexto(bool valor) => valor ? "Sim" : "Não";

        private void ViewTriagem_Load(object sender, EventArgs e) { }

        private async void btnSalvarCadastro_Click(object sender, EventArgs e)
        {
            if (_candidatoAtual == null)
            {
                ExibirNotificacao("Nenhum candidato selecionado."); // MÉTODO CENTRALIZADO
                return;
            }

            try
            {
                _candidatoAtual.NomeOngResponsavel = cboONG.Text;
                _candidatoAtual.VagaEncaminhada = cboVaga.Text;
                _candidatoAtual.ObservacoesONG = txtAnotacoesRH.Text;

                if (numPontuacao != null)
                    _candidatoAtual.Pontuacao = (int)numPontuacao.Value;

                if (cboStatus != null && cboStatus.SelectedItem != null)
                    _candidatoAtual.Status = (StatusCandidato)cboStatus.SelectedItem;
                else
                    _candidatoAtual.Status = StatusCandidato.Entrevista;

                var response = await _client.PutAsJsonAsync($"api/candidatos/{_candidatoAtual.Id}", _candidatoAtual);

                if (response.IsSuccessStatusCode)
                {
                    ExibirNotificacao($"Triagem Salva!\nCandidato encaminhado para: {_candidatoAtual.Status}"); // MÉTODO CENTRALIZADO

                    // --- LOG DE AUDITORIA ---
                    try
                    {
                        var log = new LogSistema
                        {
                            Usuario = "Administrador",
                            Acao = "Triagem Realizada",
                            Detalhes = $"Candidato: {_candidatoAtual.NomeCompleto} -> Status: {_candidatoAtual.Status}"
                        };
                        _client.PostAsJsonAsync("api/logs", log);
                    }
                    catch { }
                    // -----------------------

                    AoFechar?.Invoke();
                    this.Visible = false;
                    this.Parent?.Controls.Remove(this);
                }
                else
                {
                    var erro = await response.Content.ReadAsStringAsync();
                    ExibirNotificacao($"Erro ao salvar: {erro}"); // MÉTODO CENTRALIZADO
                }
            }
            catch (Exception ex)
            {
                ExibirNotificacao("Erro de conexão: " + ex.Message); // MÉTODO CENTRALIZADO
            }
        }

        private async void btnNovaTriagem_Click(object sender, EventArgs e)
        {
            try
            {
                Cursor = Cursors.WaitCursor;
                var lista = await _client.GetFromJsonAsync<List<Candidato>>("api/candidatos");

                if (lista != null)
                {
                    var proximoCandidato = lista
                        .Where(c => c.Status == StatusCandidato.Inscrito)
                        .OrderBy(c => c.DataCadastro)
                        .FirstOrDefault();

                    if (proximoCandidato != null)
                    {
                        // MÉTODO CENTRALIZADO PARA PERGUNTA
                        var resp = ExibirProximoFila($"Próximo da fila: {proximoCandidato.NomeCompleto}.\nDeseja iniciar?");

                        if (resp == DialogResult.Yes)
                        {
                            CarregarDadosDoCandidato(proximoCandidato);
                            if (cboStatus != null) cboStatus.SelectedItem = StatusCandidato.Triagem;
                        }
                        else
                        {
                            VoltarParaEntrevistas();
                        }
                    }
                    else
                    {
                        ExibirNotificacao("Fila zerada! Ninguém aguardando triagem."); // MÉTODO CENTRALIZADO
                        VoltarParaEntrevistas();
                    }
                }
            }
            catch (Exception ex)
            {
                ExibirNotificacao($"Erro: {ex.Message}"); // MÉTODO CENTRALIZADO
            }
            finally
            {
                Cursor = Cursors.Default;
            }
        }

        private void VoltarParaEntrevistas()
        {
            AoFechar?.Invoke();
            this.Visible = false;
            this.Parent?.Controls.Remove(this);
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