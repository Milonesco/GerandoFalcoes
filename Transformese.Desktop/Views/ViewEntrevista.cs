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

        private void ViewEntrevista_Load(object sender, EventArgs e)
        {
            if (dtpFiltroData != null) dtpFiltroData.Value = DateTime.Now;
            CarregarDadosDaApi();
        }

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
                MessageBox.Show("Erro ao carregar dados: " + ex.Message);
            }
        }

        private void dtpFiltroData_ValueChanged(object sender, EventArgs e)
        {
            FiltrarLista();
        }

        private void FiltrarLista()
        {
            if (_todosCandidatos == null || !_todosCandidatos.Any()) return;

            DateTime dataSelecionada = dtpFiltroData.Value.Date;

            // Filtra pela data. Se quiser ver TODOS independente da data para testar, comente o .Where da data
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

        // ==============================================================
        // MÁQUINA DE CORES E STATUS
        // ==============================================================
        private Panel CriarCardDinamico(Candidato c)
        {
            // 1. Definição das Cores e Textos baseados no Status
            Color corStatus;
            string textoBotao;
            bool botaoHabilitado = true;

            switch (c.Status)
            {
                case StatusCandidato.Inscrito: // 1
                    corStatus = Color.FromArgb(235, 100, 50); // Laranja
                    textoBotao = "Iniciar Triagem";
                    break;

                case StatusCandidato.Triagem: // 2
                    corStatus = Color.FromArgb(233, 30, 99); // Rosa (Pink)
                    textoBotao = "Ver Detalhes"; // Ainda abre a triagem para editar
                    break;

                // Assumindo que "EncaminhadoONG" ou "Entrevista" seja o verde que você quer
                // Se no seu enum "Aprovado" for o status 5, use ele aqui.
                case StatusCandidato.Entrevista: // 4 (Ou Aprovado na Triagem)
                case StatusCandidato.Aprovado:   // 5
                    corStatus = Color.FromArgb(25, 160, 90); // Verde
                    textoBotao = "Agendar Entrevista";
                    break;

                case StatusCandidato.Reprovado: // 6
                    corStatus = Color.FromArgb(220, 53, 69); // Vermelho
                    textoBotao = "Reprovado";
                    botaoHabilitado = false; // Bloqueia clique? Ou deixa abrir pra ver motivo?
                    break;

                case StatusCandidato.Desistente: // 7
                    corStatus = Color.Gray; // Cinza
                    textoBotao = "Desistente";
                    botaoHabilitado = false;
                    break;

                default:
                    corStatus = Color.Gray;
                    textoBotao = "Ver";
                    break;
            }

            // 2. Construção do Card
            Panel pnlCard = new Panel();
            pnlCard.Size = new Size(280, 160);
            pnlCard.BackColor = Color.White;
            pnlCard.Margin = new Padding(10);
            pnlCard.BorderStyle = BorderStyle.FixedSingle;

            // Faixa Lateral (Usa a cor definida no switch)
            Panel pnlFaixa = new Panel();
            pnlFaixa.Dock = DockStyle.Left;
            pnlFaixa.Width = 6;
            pnlFaixa.BackColor = corStatus;
            pnlCard.Controls.Add(pnlFaixa);

            // Data
            Label lblData = new Label();
            lblData.Text = c.DataCadastro.ToString("dd MMM");
            lblData.Location = new Point(15, 30);
            lblData.AutoSize = true;
            lblData.ForeColor = Color.Gray;
            lblData.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            pnlCard.Controls.Add(lblData);

            // Hora
            Label lblHora = new Label();
            lblHora.Text = c.DataCadastro.ToString("HH:mm");
            lblHora.Location = new Point(12, 10);
            lblHora.AutoSize = true;
            lblHora.Font = new Font("Segoe UI", 12, FontStyle.Bold);
            pnlCard.Controls.Add(lblHora);

            // Nome
            Label lblNome = new Label();
            lblNome.Text = c.NomeCompleto;
            lblNome.Location = new Point(12, 55);
            lblNome.AutoSize = true;
            lblNome.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            pnlCard.Controls.Add(lblNome);

            // Vaga
            Label lblVaga = new Label();
            string vaga = string.IsNullOrEmpty(c.CursoInteresse) ? "Geral" : c.CursoInteresse;
            lblVaga.Text = "Vaga: " + vaga;
            lblVaga.Location = new Point(15, 78);
            lblVaga.AutoSize = true;
            lblVaga.ForeColor = Color.Gray;
            lblVaga.Font = new Font("Segoe UI", 8, FontStyle.Regular);
            pnlCard.Controls.Add(lblVaga);

            // Botão (Usa a cor e texto definidos no switch)
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

            // Se estiver desabilitado, deixa visualmente mais claro
            if (!botaoHabilitado) btnAcao.BackColor = ControlPaint.Light(corStatus);

            btnAcao.Tag = c; // Guarda o candidato
            btnAcao.Click += BtnAcao_Click; // Evento único que decide o que fazer

            pnlCard.Controls.Add(btnAcao);

            return pnlCard;
        }

        // ==============================================================
        // CENTRAL DE CLIQUES
        // ==============================================================
        private async void BtnAcao_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            if (btn == null || !(btn.Tag is Candidato candidato)) return;

            // Lógica baseada no Status Atual
            switch (candidato.Status)
            {
                case StatusCandidato.Inscrito:
                    // 1. De Inscrito -> Triagem (Muda cor para Rosa)
                    var confirm = MessageBox.Show($"Iniciar a triagem de {candidato.NomeCompleto}?", "Confirmar", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        candidato.Status = StatusCandidato.Triagem;

                        // Salva no Banco
                        await _client.PutAsJsonAsync($"api/candidatos/{candidato.Id}", candidato);

                        // Atualiza a tela (O card vai ficar Rosa automaticamente)
                        FiltrarLista();
                    }
                    break;

                case StatusCandidato.Triagem:
                    // 2. De Triagem -> Abre Tela de Detalhes
                    AbrirTelaTriagem(candidato);
                    break;

                case StatusCandidato.Entrevista: // Ou Aprovado
                    // 3. De Aprovado -> Agendar Entrevista
                    MessageBox.Show($"Abrir calendário para agendar com {candidato.NomeCompleto}...\n(Funcionalidade futura)");
                    break;

                default:
                    MessageBox.Show($"Ação para status {candidato.Status} não implementada.");
                    break;
            }
        }

        private void AbrirTelaTriagem(Candidato c)
        {
            var telaTriagem = new ViewTriagem();
            telaTriagem.CarregarDadosDoCandidato(c);

            if (this.Parent != null)
            {
                telaTriagem.Dock = DockStyle.Fill;
                this.Parent.Controls.Add(telaTriagem);
                telaTriagem.BringToFront();
            }
            else
            {
                // Fallback para testes
                Form f = new Form();
                f.Size = new Size(1000, 700);
                telaTriagem.Dock = DockStyle.Fill;
                f.Controls.Add(telaTriagem);
                f.ShowDialog();
            }
        }

        private void ViewEntrevista_Load_1(object sender, EventArgs e)
        {
            if (dtpFiltroData != null) dtpFiltroData.Value = DateTime.Now;
            CarregarDadosDaApi();
        }

        private void dtpFiltroData_ValueChanged_1(object sender, EventArgs e)
        {
            FiltrarLista();
        }
    }
}