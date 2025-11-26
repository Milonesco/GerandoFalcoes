using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Linq;
using System.Threading.Tasks;
using Transformese.Domain.Entities;
using Transformese.MVC.Services;
using Transformese.MVC.ViewModels;

namespace TransformeseMVC.Web.Controllers
{
    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {
        private readonly ICursoApiClient _cursoApi;
        private readonly IUsuarioApiClient _usuarioApi;

        public DashboardController(ICursoApiClient cursoApi, IUsuarioApiClient usuarioApi)
        {
            _cursoApi = cursoApi;
            _usuarioApi = usuarioApi;
        }

        public async Task<IActionResult> Index()
        {
            var cursos = await _cursoApi.GetAllAsync();
            var usuarios = await _usuarioApi.GetAllAsync();

            cursos ??= Enumerable.Empty<CursoDto>();
            usuarios ??= Enumerable.Empty<Usuario>();

            var model = new DashboardViewModel
            {
                TotalCursos = cursos.Count(),
                TotalAlunos = usuarios.Count(u => u.TipoUsuario?.DescricaoTipoUsuario == "Aluno"),
                TotalProfessores = usuarios.Count(u => u.TipoUsuario?.DescricaoTipoUsuario == "Professor"),
                TotalAdministradores = usuarios.Count(u => u.TipoUsuario?.DescricaoTipoUsuario == "Administrador")
            };

            return View(model);
        }

    }
}
