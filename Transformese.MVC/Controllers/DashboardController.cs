using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Transformese.Domain.Entities;
using Transformese.MVC.Services;
using Transformese.MVC.ViewModels;

namespace TransformeseMVC.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class DashboardController : Controller
    {
        private readonly IUsuarioApiClient _usuarioApi;
        private readonly IUnidadeApiClient _unidadeApi;

        public DashboardController(
                IUsuarioApiClient usuarioApi,
                IUnidadeApiClient unidadeApi)
        {
            _usuarioApi = usuarioApi;
            _unidadeApi = unidadeApi;
        }

        public async Task<IActionResult> Index()
        {
            var usuarios = await _usuarioApi.GetAllAsync();
            var unidades = await _unidadeApi.GetAllAsync();

            usuarios ??= Enumerable.Empty<Usuario>();
            unidades ??= Enumerable.Empty<Unidade>();

            var model = new DashboardViewModel
            {
                TotalUnidades = unidades.Count(),
                TotalCandidatos = usuarios.Count(u => u.TipoUsuario?.DescricaoTipoUsuario == "Candidato"),
                TotalAdministradores = usuarios.Count(u => u.TipoUsuario?.DescricaoTipoUsuario == "Administrador")
            };

            return View(model);
        }
    }
}
