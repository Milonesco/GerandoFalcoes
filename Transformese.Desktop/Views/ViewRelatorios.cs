using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks; // Importante para Task
using System.Windows.Forms;
using ClosedXML.Excel;
using Transformese.Domain.Entities;

namespace Transformese.Desktop.Views
{
    public partial class ViewRelatorios : UserControl
    {
        private readonly HttpClient _client;

        public ViewRelatorios()
        {
            InitializeComponent();

            var handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (m, c, ch, e) => true;
            _client = new HttpClient(handler) { BaseAddress = new Uri("https://localhost:5001/") };
        }

        // =============================================================
        // 1. CARREGAR HISTÓRICO NO GRID
        // =============================================================
        private async void CarregarLogs()
        {
            try
            {
                var logs = await _client.GetFromJsonAsync<List<LogSistema>>("api/logs");

                if (logs != null && dgvLogs != null)
                {
                    dgvLogs.Rows.Clear();

                    foreach (var log in logs)
                    {
                        // Adiciona no Grid (Ajuste a ordem conforme suas colunas no Designer)
                        // Ex: Data/Hora | Usuário | Ação | Detalhes
                        dgvLogs.Rows.Add(
                            log.DataHora.ToString("dd/MM/yyyy HH:mm"),
                            log.Usuario,
                            log.Acao,
                            log.Detalhes
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                // Falha silenciosa ou log no console para não travar a tela
                Console.WriteLine("Erro ao buscar logs: " + ex.Message);
            }
        }

        // =============================================================
        // 2. MÉTODO AUXILIAR PARA REGISTRAR LOG (POST)
        // =============================================================
        private async Task RegistrarLog(string acao, string detalhes)
        {
            try
            {
                // Busca o nome real do usuário logado
                string nomeUsuario = "Sistema"; // Padrão

                if (SessaoSistema.UsuarioLogado != null)
                {
                    // Pode usar Nome ou Nome + Sobrenome
                    nomeUsuario = $"{SessaoSistema.UsuarioLogado.Nome} {SessaoSistema.UsuarioLogado.Sobrenome}".Trim();
                }

                var novoLog = new LogSistema
                {
                    Usuario = nomeUsuario, // <--- AQUI ESTÁ A MUDANÇA
                    Acao = acao,
                    Detalhes = detalhes,
                    DataHora = DateTime.Now
                };

                await _client.PostAsJsonAsync("api/logs", novoLog);

                // Recarrega o grid para mostrar a ação que acabou de acontecer
                CarregarLogs();
            }
            catch { /* Ignora erro de log para não parar o fluxo */ }
        }

        // =============================================================
        // 3. EXPORTAR EXCEL (COM LOG)
        // =============================================================
        private async void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                this.Cursor = Cursors.WaitCursor;
                btnExportar.Enabled = false;

                var candidatos = await _client.GetFromJsonAsync<List<Candidato>>("api/candidatos");

                if (candidatos == null || candidatos.Count == 0)
                {
                    ExibirMensagem("Não há dados para exportar.");
                    return;
                }

                using (var sfd = new SaveFileDialog())
                {
                    sfd.Filter = "Excel Workbook|*.xlsx";
                    sfd.FileName = $"Relatorio_Geral_{DateTime.Now:yyyyMMdd_HHmm}.xlsx";

                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        GerarArquivoExcel(candidatos, sfd.FileName);
                        ExibirMensagem("Relatório gerado com sucesso!");

                        // ---> AQUI: Registra no histórico que o Excel foi baixado
                        await RegistrarLog("Exportação Excel", $"Arquivo gerado: {System.IO.Path.GetFileName(sfd.FileName)}");
                    }
                }
            }
            catch (Exception ex)
            {
                ExibirMensagem($"Erro ao exportar: {ex.Message}");
            }
            finally
            {
                this.Cursor = Cursors.Default;
                btnExportar.Enabled = true;
            }
        }

        // ... (Seu método GerarArquivoExcel e ExibirMensagem continuam iguais) ...

        private void ExibirMensagem(string msg)
        {
            if (mdNotifica != null)
            {
                if (this.ParentForm != null) mdNotifica.Parent = this.ParentForm;
                mdNotifica.Show(msg);
            }
            else mdNotifica.Show(msg);
        }

        private void GerarArquivoExcel(List<Candidato> lista, string caminhoArquivo)
        {
            // ... (Seu código do Excel que já estava pronto) ...
            using (var workbook = new XLWorkbook())
            {
                var worksheet = workbook.Worksheets.Add("Candidatos");
                // ... resto do código do excel ...
                workbook.SaveAs(caminhoArquivo);
            }
        }

        private void ViewRelatorios_Load_1(object sender, EventArgs e)
        {
            CarregarLogs();
        }
    }
}