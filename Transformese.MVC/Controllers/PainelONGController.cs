using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Transformese.MVC.Services;
using Transformese.Domain.Enums;

namespace Transformese.MVC.Controllers
{
    [Authorize(Roles = "ONGAdmin")]
    public class PainelONGController : Controller
    {
        private readonly ICandidatoService _candidatoService;
        private readonly IUsuarioService _usuarioService;

        public PainelONGController(ICandidatoService candidatoService, IUsuarioService usuarioService)
        {
            _candidatoService = candidatoService;
            _usuarioService = usuarioService;
        }

        public async Task<IActionResult> Index()
        {
            // Pega o ID da Unidade do usuário logado (através do serviço auxiliar)
            var unidadeId = _usuarioService.GetUnidadeIdDoUsuario(User);

            // Busca apenas os pendentes daquela unidade
            var candidatos = await _candidatoService.GetPendentesPorUnidade(unidadeId);

            return View(candidatos);
        }

        [HttpPost]
        public async Task<IActionResult> Agendar(int id)
        {
            await _candidatoService.AtualizarStatus(id, StatusCandidato.EntrevistaAgendada);
            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> RegistrarResultado(int id, bool aprovado, string observacoes)
        {
            // AQUI ESTAVA O ERRO: Agora passamos o 'aprovado' (bool) direto para o serviço.
            await _candidatoService.RegistrarResultadoEntrevista(id, aprovado, observacoes);

            return RedirectToAction(nameof(Index));
        }
    }
}