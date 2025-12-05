using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using Transformese.Domain.Entities;
using Transformese.Domain.Enums;

namespace Transformese.Desktop.Views
{
    public partial class ViewNovoCandidato : UserControl
    {
        private readonly HttpClient _client;

        public ViewNovoCandidato()
        {
            InitializeComponent();

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;
            _client = new HttpClient(handler) { BaseAddress = new Uri("https://localhost:5001/") };
        }

        private void ViewNovoCandidato_Load(object sender, EventArgs e)
        {
            CarregarCombos();
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
        private void CarregarCombos()
        {
            // Limpa os itens existentes
            LimparItensCombos();

            // --- 1. LISTAS DE TEXTO ---
            if (cboGeneroCandidato != null)
                cboGeneroCandidato.Items.AddRange(new string[] { "Mulher Cisgênero", "Homem Cisgênero", "Mulher Transgênero", "Homem Transgênero", "Não-binário", "Prefiro não responder" });

            if (cboOrientacaoCandidato != null)
                cboOrientacaoCandidato.Items.AddRange(new string[] { "Heterossexual", "Homossexual", "Bissexual", "Outro" });

            if (cboEtniaCandidato != null)
                cboEtniaCandidato.Items.AddRange(new string[] { "Branca", "Preta", "Parda", "Amarela", "Indígena" });

            if (cboUF != null)
                cboUF.Items.AddRange(new string[] { "SP", "RJ", "MG", "ES", "RS", "BA", "PE", "PR", "SC" });

            if (cboEscolaridadeCandidato != null)
                cboEscolaridadeCandidato.Items.AddRange(new string[] { "Ensino Fundamental", "Ensino Médio Incompleto", "Ensino Médio Completo", "Superior Incompleto", "Superior Completo" });

            if (cboCursoCandidato != null)
                cboCursoCandidato.Items.AddRange(new string[] { "Desenvolvimento Web Fullstack", "Data Analytics", "Marketing Digital", "Design UX/UI" });

            if (cboTurnoCandidato != null)
                cboTurnoCandidato.Items.AddRange(new string[] { "Manhã", "Tarde", "Noite" });

            // --- 2. LISTAS DE SIM/NÃO ---
            string[] opcoesSimNao = { "Não", "Sim" };

            if (cboTrabalhoCandidato != null) cboTrabalhoCandidato.Items.AddRange(opcoesSimNao);
            if (cboTemComputador != null) cboTemComputador.Items.AddRange(opcoesSimNao);
            if (cboTemInternet != null) cboTemInternet.Items.AddRange(opcoesSimNao);
            if (cboEstudouOngCandidato != null) cboEstudouOngCandidato.Items.AddRange(opcoesSimNao);

            // CORREÇÃO 1: Forçar seleção vazia para não vir preenchido
            ResetarSelecaoCombos();
        }

        private void LimparItensCombos()
        {
            if (cboGeneroCandidato != null) cboGeneroCandidato.Items.Clear();
            if (cboOrientacaoCandidato != null) cboOrientacaoCandidato.Items.Clear();
            if (cboEtniaCandidato != null) cboEtniaCandidato.Items.Clear();
            if (cboUF != null) cboUF.Items.Clear();
            if (cboEscolaridadeCandidato != null) cboEscolaridadeCandidato.Items.Clear();
            if (cboCursoCandidato != null) cboCursoCandidato.Items.Clear();
            if (cboTurnoCandidato != null) cboTurnoCandidato.Items.Clear();
            if (cboTrabalhoCandidato != null) cboTrabalhoCandidato.Items.Clear();
            if (cboTemComputador != null) cboTemComputador.Items.Clear();
            if (cboTemInternet != null) cboTemInternet.Items.Clear();
            if (cboEstudouOngCandidato != null) cboEstudouOngCandidato.Items.Clear();
        }

        // Método auxiliar para tirar a seleção visual dos combos
        private void ResetarSelecaoCombos()
        {
            if (cboGeneroCandidato != null) cboGeneroCandidato.SelectedIndex = -1;
            if (cboOrientacaoCandidato != null) cboOrientacaoCandidato.SelectedIndex = -1;
            if (cboEtniaCandidato != null) cboEtniaCandidato.SelectedIndex = -1;
            if (cboUF != null) cboUF.SelectedIndex = -1;
            if (cboEscolaridadeCandidato != null) cboEscolaridadeCandidato.SelectedIndex = -1;
            if (cboCursoCandidato != null) cboCursoCandidato.SelectedIndex = -1;
            if (cboTurnoCandidato != null) cboTurnoCandidato.SelectedIndex = -1;
            if (cboTrabalhoCandidato != null) cboTrabalhoCandidato.SelectedIndex = -1;
            if (cboTemComputador != null) cboTemComputador.SelectedIndex = -1;
            if (cboTemInternet != null) cboTemInternet.SelectedIndex = -1;
            if (cboEstudouOngCandidato != null) cboEstudouOngCandidato.SelectedIndex = -1;
        }

        // CORREÇÃO 3: Método para limpar tudo APÓS salvar
        private void LimparFormulario()
        {
            // Limpa TextBoxes
            txtNomeCandidato.Clear();
            txtCpfCandidato.Clear();
            txtRenda.Clear(); // Assumindo que existe
            txtDeficienciaCandidato.Clear();
            txtEmailCandidato.Clear();
            txtTelefoneCandidato.Clear();
            txtLinkedinCandidato.Clear();
            txtCepCandidato.Clear();
            txtCidadeCandidato.Clear();
            txtIndicacaoCandidato.Clear();

            // Reseta Combos
            ResetarSelecaoCombos();

            // Reseta Checkbox (Termos)
            // IMPORTANTE: Verifique o nome do seu checkbox no Designer
            if (chkTermosCandidato != null) chkTermosCandidato.Checked = false;
        }

        // ========================================================
        // VIA CEP
        // ========================================================
        private async void txtCEP_Leave(object sender, EventArgs e)
        {
            string cep = txtCEP.Text.Replace("-", "").Trim();

            if (cep.Length == 8)
            {
                try
                {
                    using (var viaCepClient = new HttpClient())
                    {
                        var url = $"https://viacep.com.br/ws/{cep}/json/";
                        var resultado = await viaCepClient.GetFromJsonAsync<ViaCepDto>(url);

                        if (resultado != null && !resultado.erro)
                        {
                            txtCidade.Text = resultado.localidade;
                            if (cboUF.Items.Contains(resultado.uf))
                                cboUF.SelectedItem = resultado.uf;
                            else
                                cboUF.Text = resultado.uf;
                        }
                        else
                        {
                            ExibirMensagem("CEP não encontrado.");
                        }
                    }
                }
                catch (Exception ex)
                {
                    ExibirMensagem($"Erro ao buscar CEP: {ex.Message}");
                }
            }
        }

        // ========================================================
        // SALVAR
        // ========================================================
        private async void btnSalvarCandidato_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos()) return;

            try
            {
                decimal rendaDecimal = 0;
                string rendaLimpa = txtRenda.Text.Replace("R$", "").Replace(".", "").Trim();
                decimal.TryParse(rendaLimpa, out rendaDecimal);

                var novoCandidato = new Candidato
                {
                    NomeCompleto = txtNomeCandidato.Text,
                    CPF = txtCpfCandidato.Text.Replace(".", "").Replace("-", ""),
                    DataNascimento = dtpDataNascCandidato.Value,
                    IdentidadeGenero = cboGeneroCandidato.Text,
                    OrientacaoSexual = cboOrientacaoCandidato.Text,
                    RacaCor = cboEtniaCandidato.Text,
                    Deficiencia = txtDeficienciaCandidato.Text,
                    Email = txtEmailCandidato.Text,
                    Telefone = txtTelefoneCandidato.Text,
                    PerfilLinkedin = txtLinkedinCandidato.Text,
                    CEP = txtCepCandidato.Text,
                    Cidade = txtCidadeCandidato.Text,
                    Estado = cboUF.Text,
                    Escolaridade = cboEscolaridadeCandidato.Text,
                    RendaFamiliar = rendaDecimal,
                    PessoasNaCasa = (int)numPessoasDomicilio.Value,
                    TrabalhaAtualmente = ConverterSimNao(cboTrabalhoCandidato.Text),
                    PossuiComputador = ConverterSimNao(cboTemComputador.Text),
                    PossuiInternet = ConverterSimNao(cboTemInternet.Text),
                    CursoInteresse = cboCursoCandidato.Text,
                    TurnoPreferido = cboTurnoCandidato.Text,
                    NomeIndicacao = txtIndicacaoCandidato.Text,
                    JaEstudouNaGF = ConverterSimNao(cboEstudouOngCandidato.Text),

                    Status = StatusCandidato.Inscrito,
                    DataCadastro = DateTime.Now,
                    AceitouTermos = true, // Já validado no ValidarCampos
                    AceitouNovidades = false
                };

                var response = await _client.PostAsJsonAsync("api/candidatos", novoCandidato);

                if (response.IsSuccessStatusCode)
                {
                    ExibirMensagem("Candidato cadastrado com sucesso!");

                    // CORREÇÃO 3: Limpa tudo após o sucesso
                    LimparFormulario();
                }
                else
                {
                    var erro = await response.Content.ReadAsStringAsync();
                    ExibirMensagem($"Erro na API: {erro}");
                }
            }
            catch (Exception ex)
            {
                ExibirMensagem($"Erro de conexão: {ex.Message}");
            }
        }

        private bool ConverterSimNao(string valorCombo)
        {
            return string.Equals(valorCombo, "Sim", StringComparison.OrdinalIgnoreCase);
        }

        private bool ValidarCampos()
        {
            if (string.IsNullOrWhiteSpace(txtNomeCandidato.Text))
            {
                ExibirMensagem("Nome é obrigatório");
                txtNomeCandidato.Focus();
                return false;
            }

            if (txtCpfCandidato.Text.Length < 11)
            {
                ExibirMensagem("CPF inválido ou incompleto");
                txtCpfCandidato.Focus();
                return false;
            }

            if (string.IsNullOrWhiteSpace(cboCursoCandidato.Text))
            {
                ExibirMensagem("Selecione o curso");
                cboCursoCandidato.Focus();
                return false;
            }

            // CORREÇÃO 2: Validação dos Termos Obrigatórios
            // Certifique-se que o nome do seu checkbox no Designer é chkAceitoTermos
            if (chkTermosCandidato != null && !chkTermosCandidato.Checked)
            {
                ExibirMensagem("Você deve aceitar os termos de uso para continuar.");
                return false;
            }

            return true;
        }

        private void ExibirMensagem(string msg)
        {
            // CORREÇÃO 4: Centralizar MDNotifica
            // Como ViewNovoCandidato é um UserControl, ele não tem "CenterScreen" nativo.
            // Precisamos dizer ao dialog quem é o "Pai" (o Formulário onde esse controle está).

            if (mdNotifica != null)
            {
                // Tenta pegar o formulário pai para centralizar
                if (this.ParentForm != null)
                {
                    mdNotifica.Parent = this.ParentForm;
                }

                mdNotifica.Show(msg);
            }
            else
            {
                MessageBox.Show(msg);
            }
        }

        public class ViaCepDto { public string localidade { get; set; } public string uf { get; set; } public bool erro { get; set; } }
    }
}