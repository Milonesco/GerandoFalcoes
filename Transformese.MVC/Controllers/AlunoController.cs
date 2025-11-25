using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Transformese.Domain.Entities;
using Transformese.MVC.Services;

namespace TransformeSeMVC.Web.Controllers
{
    [Authorize(Roles = "Aluno")]
    public class AlunoController : Controller
    {
        private readonly ICursoApiClient _api;

        public AlunoController(ICursoApiClient api)
        {
            _api = api;
        }

        public async Task<IActionResult> Index()
        {
            var cursos = await _api.GetAllAsync();
            return View(cursos);
        }
    }
}
