using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using Transformese.Domain.Entities;
using Transformese.Domain.Enums;

namespace Transformese.Desktop.Views
{
    public partial class ViewEntrevista : UserControl
    {
        private readonly HttpClient _client;
        private List<Candidato> _todosCandidatos = new List<Candidato>();

        public ViewEntrevista()
        {
            InitializeComponent();

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;
            _client = new HttpClient(handler) { BaseAddress = new Uri("https://localhost:5001/") };
        }

        private void ViewEntrevista_Load_1(object sender, EventArgs e)
        {
            if (dtpFiltroData != null) dtpFiltroData.Value = DateTime.Now;
            CarregarDadosDaApi();
        }

        // ==============================================================
        // MÉTODOS AUXILIARES PARA CENTRALIZAR OS DIÁLOGOS
        // ==============================================================

        private void ExibirNotificacao(string mensagem)
        {
            if (mdNotifica != null)
            {
                // Define o Formulário Principal como Pai para centralizar na tela inteira
                if (this.ParentForm != null) mdNotifica.Parent = this.ParentForm;
                mdNotifica.Show(mensagem);
            }
            else
            {
                MessageBox.Show(mensagem);
            }
        }

        private DialogResult ExibirPergunta(string mensagem)
        {
            if (mdIniciarTriagem != null)
            {
                if (this.ParentForm != null) mdIniciarTriagem.Parent = this.ParentForm;
                return mdIniciarTriagem.Show(mensagem);
            }
            else
            {
                return MessageBox.Show(mensagem, "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            }
        }

        // ==============================================================
        // CÓDIGO DA TELA
        // ==============================================================

        private async void CarregarDadosDaApi()
        {
            try
            {
                var lista = await _client.GetFromJsonAsync<List<Candidato>>("api/candidatos");

                if (lista != null)
                {
                    _todosCandidatos = lista;
                    FiltrarLista();
                }
            }
            catch (Exception ex)
            {
                ExibirNotificacao("Erro ao carregar dados: " + ex.Message); // USANDO O MÉTODO CENTRALIZADO
            }
        }

        private void dtpFiltroData_ValueChanged_1(object sender, EventArgs e)
        {
            FiltrarLista();
        }

        private void FiltrarLista()
        {
            if (_todosCandidatos == null || !_todosCandidatos.Any()) return;

            DateTime dataSelecionada = dtpFiltroData.Value.Date;

            var listaFiltrada = _todosCandidatos
                .Where(c => c.DataCadastro.Date == dataSelecionada)
                .OrderByDescending(c => c.DataCadastro)
                .ToList();

            RenderizarCards(listaFiltrada);
        }

        private void RenderizarCards(List<Candidato> lista)
        {
            flpEntrevistas.Controls.Clear();

            if (lista.Count == 0) return;

            foreach (var candidato in lista)
            {
                var card = CriarCardDinamico(candidato);
                flpEntrevistas.Controls.Add(card);
            }
        }

        private Panel CriarCardDinamico(Candidato c)
        {
            Color corStatus;
            string textoBotao;
            bool botaoHabilitado = true;

            switch (c.Status)
            {
                case StatusCandidato.Inscrito:
                    corStatus = Color.FromArgb(235, 100, 50); // Laranja
                    textoBotao = "Iniciar Triagem";
                    break;

                case StatusCandidato.Triagem:
                    corStatus = Color.FromArgb(233, 30, 99); // Rosa
                    textoBotao = "Ver Detalhes";
                    break;

                // --- AJUSTE: VERDE MAIS CLARO PARA ENTREVISTA ---
                case StatusCandidato.Entrevista:
                    corStatus = Color.MediumSeaGreen; // Verde Suave

                    // Lógica inteligente: Já tem data?
                    if (c.DataEntrevista.HasValue)
                    {
                        textoBotao = "Ver Detalhes Contato"; // Já agendado -> Ver ficha
                    }
                    else
                    {
                        textoBotao = "Agendar Entrevista"; // Pendente -> Agendar
                    }
                    break;

                case StatusCandidato.Aprovado:
                    corStatus = Color.FromArgb(25, 160, 90); // Verde Escuro
                    textoBotao = "Ver Detalhes Contato";
                    break;

                // --- AJUSTE: SEGUNDA CHANCE (Permitir ver dados) ---
                case StatusCandidato.Reprovado:
                    corStatus = Color.FromArgb(220, 53, 69); // Vermelho
                    textoBotao = "Ver Dados / Reavaliar";
                    break;

                case StatusCandidato.Desistente:
                    corStatus = Color.Gray; // Cinza
                    textoBotao = "Ver Dados / Reavaliar";
                    break;

                default:
                    corStatus = Color.Gray;
                    textoBotao = "Ver";
                    break;
            }

            // ... (Criação visual do painel continua igual) ...
            Panel pnlCard = new Panel();
            pnlCard.Size = new Size(280, 160);
            pnlCard.BackColor = Color.White;
            pnlCard.Margin = new Padding(10);
            pnlCard.BorderStyle = BorderStyle.FixedSingle;

            Panel pnlFaixa = new Panel();
            pnlFaixa.Dock = DockStyle.Left;
            pnlFaixa.Width = 6;
            pnlFaixa.BackColor = corStatus;
            pnlCard.Controls.Add(pnlFaixa);

            Label lblData = new Label();
            // Se tiver data de entrevista, mostra ela em destaque
            if (c.Status == StatusCandidato.Entrevista && c.DataEntrevista.HasValue)
            {
                lblData.Text = "Agendado: " + c.DataEntrevista.Value.ToString("dd/MM HH:mm");
                lblData.ForeColor = corStatus;
            }
            else
            {
                lblData.Text = "Cadastro: " + c.DataCadastro.ToString("dd MMM");
                lblData.ForeColor = Color.Gray;
            }
            lblData.Location = new Point(15, 30);
            lblData.AutoSize = true;
            lblData.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            pnlCard.Controls.Add(lblData);

            Label lblHora = new Label();
            lblHora.Text = c.DataCadastro.ToString("HH:mm");
            lblHora.Location = new Point(12, 10);
            lblHora.AutoSize = true;
            lblHora.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            pnlCard.Controls.Add(lblHora);

            Label lblNome = new Label();
            lblNome.Text = c.NomeCompleto;
            lblNome.Location = new Point(12, 55);
            lblNome.AutoSize = true;
            lblNome.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            pnlCard.Controls.Add(lblNome);

            Label lblVaga = new Label();
            string vaga = string.IsNullOrEmpty(c.CursoInteresse) ? "Geral" : c.CursoInteresse;
            lblVaga.Text = "Vaga: " + vaga;
            lblVaga.Location = new Point(15, 78);
            lblVaga.AutoSize = true;
            lblVaga.ForeColor = Color.Gray;
            lblVaga.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            pnlCard.Controls.Add(lblVaga);

            Button btnAcao = new Button();
            btnAcao.Text = textoBotao;
            btnAcao.Size = new Size(250, 35);
            btnAcao.Location = new Point(14, 115);
            btnAcao.FlatStyle = FlatStyle.Flat;
            btnAcao.FlatAppearance.BorderSize = 0;
            btnAcao.BackColor = corStatus;
            btnAcao.ForeColor = Color.White;
            btnAcao.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            btnAcao.Cursor = Cursors.Hand;
            btnAcao.Enabled = botaoHabilitado;

            btnAcao.Tag = c;
            btnAcao.Click += BtnAcao_Click;

            pnlCard.Controls.Add(btnAcao);

            return pnlCard;
        }

        private async void BtnAcao_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null || !(btn.Tag is Candidato candidato)) return;

            switch (candidato.Status)
            {
                case StatusCandidato.Inscrito:
                    var confirm = ExibirPergunta($"Iniciar a triagem de {candidato.NomeCompleto}?");
                    if (confirm == DialogResult.Yes)
                    {
                        candidato.Status = StatusCandidato.Triagem;
                        await _client.PutAsJsonAsync($"api/candidatos/{candidato.Id}", candidato);
                        CarregarDadosDaApi();
                    }
                    break;

                case StatusCandidato.Triagem:
                case StatusCandidato.Reprovado:  // Segunda chance -> Abre Triagem
                case StatusCandidato.Desistente: // Segunda chance -> Abre Triagem
                case StatusCandidato.Aprovado:   // Ver Detalhes -> Abre Triagem
                    AbrirTelaTriagem(candidato);
                    break;

                // --- AJUSTE AQUI: ENTREVISTA ---
                case StatusCandidato.Entrevista:
                    if (candidato.DataEntrevista.HasValue)
                    {
                        // Se JÁ tem data, o botão diz "Ver Detalhes", então abrimos a ficha
                        AbrirTelaTriagem(candidato);
                    }
                    else
                    {
                        // Se NÃO tem data, abrimos o agendamento
                        AbrirTelaAgendamento(candidato);
                    }
                    break;

                default:
                    ExibirNotificacao("Ação não implementada.");
                    break;
            }
        }

        private void AbrirTelaTriagem(Candidato c)
        {
            var telaTriagem = new ViewTriagem();
            telaTriagem.CarregarDadosDoCandidato(c);

            telaTriagem.AoFechar = () =>
            {
                if (this.Parent != null) this.Parent.Controls.Remove(telaTriagem);
                CarregarDadosDaApi();
            };

            ExibirTelaFilha(telaTriagem);
        }

        private void AbrirTelaAgendamento(Candidato c)
        {
            using (var frm = new frmAgendarEntrevista(c))
            {
                var resultado = frm.ShowDialog();

                if (resultado == DialogResult.OK)
                {
                    CarregarDadosDaApi();
                }
            }
        }

        private void ExibirTelaFilha(UserControl tela)
        {
            if (this.Parent != null)
            {
                tela.Dock = DockStyle.Fill;
                this.Parent.Controls.Add(tela);
                tela.BringToFront();
            }
            else
            {
                Form f = new Form();
                f.Size = new Size(1000, 700);
                tela.Dock = DockStyle.Fill;
                f.Controls.Add(tela);
                f.ShowDialog();
                CarregarDadosDaApi();
            }
        }
    }
}