using System;
using System.Net.Http;
using System.Net.Http.Json;
using System.Windows.Forms;
using Transformese.Domain.Entities;

namespace Transformese.Desktop
{
    public partial class frmAgendarEntrevista : Form
    {
        private readonly HttpClient _client;
        private Candidato _candidato;

        public frmAgendarEntrevista(Candidato c)
        {
            InitializeComponent();

            // Configura API
            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (m, cert, ch, e) => true;
            _client = new HttpClient(handler) { BaseAddress = new Uri("https://localhost:5001/") };

            _candidato = c;
            CarregarDados();
        }

        private void CarregarDados()
        {
            if (_candidato != null)
            {
                // Preenche o nome no Label (conforme sua imagem)
                if (lblNomeCandidato != null)
                    lblNomeCandidato.Text = _candidato.NomeCompleto;

                this.Text = $"Agendar: {_candidato.NomeCompleto}";

                // Se já tiver vaga encaminhada, preenche. Senão, tenta o curso.
                if (txtVagaCandidato != null)
                {
                    txtVagaCandidato.Text = !string.IsNullOrEmpty(_candidato.VagaEncaminhada)
                        ? _candidato.VagaEncaminhada
                        : _candidato.CursoInteresse;
                }

                // Se já tiver data salva no banco, preenche os componentes visualmente
                if (_candidato.DataEntrevista.HasValue)
                {
                    // Preenche a Data
                    if (dtpData != null)
                        dtpData.Value = _candidato.DataEntrevista.Value.Date;

                    // Preenche a Hora
                    if (dtpHora != null)
                        dtpHora.Value = _candidato.DataEntrevista.Value;
                }
            }
        }

        private async void btnSalvar_Click(object sender, EventArgs e)
        {
            try
            {
                // 1. COMBINAR DATA E HORA
                DateTime dataFinal = DateTime.Now;

                // Verifica se os componentes existem (caso tenha esquecido de renomear no designer)
                if (dtpData != null && dtpHora != null)
                {
                    // Pega apenas a parte do Dia/Mês/Ano do primeiro componente
                    DateTime dataParte = dtpData.Value.Date;

                    // Pega apenas a parte das Horas/Minutos do segundo componente
                    TimeSpan horaParte = dtpHora.Value.TimeOfDay;

                    // Soma os dois para criar a data completa (Ex: 03/12/2025 14:30)
                    dataFinal = dataParte + horaParte;
                }
                else
                {
                    // Fallback de segurança se algo estiver nulo
                    dataFinal = DateTime.Now.AddDays(1);
                }

                // 2. Atualiza o objeto Candidato
                _candidato.DataEntrevista = dataFinal;

                // Salva a vaga também, caso o recrutador tenha mudado o texto
                if (txtVagaCandidato != null)
                    _candidato.VagaEncaminhada = txtVagaCandidato.Text;

                // Garante que o status agora é oficialmente "Entrevista"
                _candidato.Status = Domain.Enums.StatusCandidato.Entrevista;

                // 3. Salva na API
                var response = await _client.PutAsJsonAsync($"api/candidatos/{_candidato.Id}", _candidato);

                if (response.IsSuccessStatusCode)
                {
                    mdNotifica.Show($"Agendado com sucesso para:\n{dataFinal:dd/MM/yyyy 'às' HH:mm}");

                    this.DialogResult = DialogResult.OK; // Retorna OK para a tela anterior atualizar o card verde
                    this.Close();
                }
                else
                {
                    var erro = await response.Content.ReadAsStringAsync();
                    mdNotifica.Show($"Erro na API: {erro}");
                }
            }
            catch (Exception ex)
            {
                mdNotifica.Show("Erro local: " + ex.Message);
            }
        }
        private void btnCancelarAgendamento_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}