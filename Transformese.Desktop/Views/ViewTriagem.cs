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
        // Ação para avisar a tela anterior que terminamos
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
        // 1. POPULAR COMBOS
        // =============================================================
        private void CarregarCombos()
        {
            if (cboONG != null) cboONG.Items.Clear();
            if (cboVaga != null) cboVaga.Items.Clear();

            if (cboONG != null)
                cboONG.Items.AddRange(new string[] { "GerandoFalcoes", "PontoEVirgula" });

            if (cboVaga != null)
                cboVaga.Items.AddRange(new string[] { "Analista de Qualidade", "Desenvolvedor FullStack", "Atendimento ao cliente" });

            if (cboStatus != null)
            {
                cboStatus.DataSource = Enum.GetValues(typeof(StatusCandidato));
            }
        }

        // =============================================================
        // 2. CARREGAR DADOS NA TELA
        // =============================================================
        public void CarregarDadosDoCandidato(Candidato c)
        {
            _candidatoAtual = c;

            // --- Dados Pessoais ---
            txtNomeCandidato.Text = c.NomeCompleto;
            txtCPF.Text = c.CPF;
            txtDataNasc.Text = c.DataNascimento.ToString("dd/MM/yyyy");

            txtGenero.Text = c.IdentidadeGenero;
            txtOrientacaoSexual.Text = c.OrientacaoSexual;
            txtEtnia.Text = c.RacaCor;
            txtDeficiencia.Text = c.Deficiencia;

            // --- Contato ---
            txtEmail.Text = c.Email;
            txtLinkedin.Text = c.PerfilLinkedin;
            txtCelular.Text = c.Telefone;
            txtCep.Text = c.CEP;
            txtCidade.Text = c.Cidade;
            txtUF.Text = c.Estado;

            // --- Socioeconômico ---
            txtEscolaridade.Text = c.Escolaridade;
            txtTrabalho.Text = BoolParaTexto(c.TrabalhaAtualmente);
            txtRendaFamiliar.Text = c.RendaFamiliar.ToString("C2");
            txtPessoasDomicilio.Text = c.PessoasNaCasa.ToString();
            txtTemComputador.Text = BoolParaTexto(c.PossuiComputador);
            txtTemInternet.Text = BoolParaTexto(c.PossuiInternet);

            // --- Interesse ---
            txtCursoInteresse.Text = c.CursoInteresse;
            txtTurno.Text = c.TurnoPreferido;
            txtIndicacao.Text = c.NomeIndicacao;
            txtEstudouOng.Text = BoolParaTexto(c.JaEstudouNaGF);

            // --- Avaliação ---
            txtAnotacoesRH.Clear();
            // Se já tiver observação salva, carrega ela:
            if (!string.IsNullOrEmpty(c.ObservacoesONG))
                txtAnotacoesRH.Text = c.ObservacoesONG;

            if(numPontuacao != null)
            {
                // Se o candidato tem pontuação (não é nulo), joga no campo.
                // Se for nulo, joga 0.
                numPontuacao.Value = c.Pontuacao.HasValue ? c.Pontuacao.Value : 0;
            }

            // Carrega Status, ONG e Vaga já salvos
            if (cboStatus != null) cboStatus.SelectedItem = c.Status;
            if (cboONG != null) cboONG.Text = c.NomeOngResponsavel;
            if (cboVaga != null) cboVaga.Text = c.VagaEncaminhada;
        }

        private string BoolParaTexto(bool valor) => valor ? "Sim" : "Não";

        private void ViewTriagem_Load(object sender, EventArgs e) { }

        // =============================================================
        // 3. SALVAR (CORRIGIDO)
        // =============================================================
        private async void btnSalvarCadastro_Click(object sender, EventArgs e)
        {
            if (_candidatoAtual == null)
            {
                MessageBox.Show("Nenhum candidato selecionado.");
                return;
            }

            try
            {
                // A. Salva os campos de texto e combos no objeto
                _candidatoAtual.NomeOngResponsavel = cboONG.Text;
                _candidatoAtual.VagaEncaminhada = cboVaga.Text;
                _candidatoAtual.ObservacoesONG = txtAnotacoesRH.Text;

                // LÓGICA DA PONTUAÇÃO (Adicionado aqui)
                if (numPontuacao != null)
                {
                    // Convertemos de Decimal (do componente) para int (do banco)
                    _candidatoAtual.Pontuacao = (int)numPontuacao.Value;
                }

                // B. Salva o Status
                if (cboStatus != null && cboStatus.SelectedItem != null)
                {
                    _candidatoAtual.Status = (StatusCandidato)cboStatus.SelectedItem;
                }
                else
                {
                    _candidatoAtual.Status = StatusCandidato.Entrevista;
                }

                // C. Envia para a API (PUT)
                var response = await _client.PutAsJsonAsync($"api/candidatos/{_candidatoAtual.Id}", _candidatoAtual);

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Triagem Salva!\nCandidato encaminhado para: {_candidatoAtual.Status}");

                    AoFechar?.Invoke();

                    this.Visible = false;
                    this.Parent?.Controls.Remove(this);
                }
                else
                {
                    var erro = await response.Content.ReadAsStringAsync();
                    MessageBox.Show($"Erro ao salvar: {erro}");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro de conexão: " + ex.Message);
            }
        }

        // =============================================================
        // 4. NOVA TRIAGEM (Modo Esteira)
        // =============================================================
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
                        var resp = MessageBox.Show(
                            $"Próximo da fila: {proximoCandidato.NomeCompleto}.\nDeseja iniciar?",
                            "Próximo", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                        if (resp == DialogResult.Yes)
                        {
                            CarregarDadosDoCandidato(proximoCandidato);
                            // Já sugere mudar o status visualmente
                            if (cboStatus != null) cboStatus.SelectedItem = StatusCandidato.Triagem;
                        }
                        else
                        {
                            VoltarParaEntrevistas();
                        }
                    }
                    else
                    {
                        MessageBox.Show("Fila zerada! Ninguém aguardando triagem.");
                        VoltarParaEntrevistas();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro: {ex.Message}");
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
    }
}